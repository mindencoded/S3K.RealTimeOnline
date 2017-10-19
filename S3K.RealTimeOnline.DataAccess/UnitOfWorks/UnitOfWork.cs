﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using S3K.RealTimeOnline.DataAccess.Repositories;
using S3K.RealTimeOnline.DataAccess.Tools;
using Serilog;

namespace S3K.RealTimeOnline.DataAccess.UnitOfWorks
{
    public abstract class UnitOfWork : IUnitOfWork
    {
        protected readonly SqlConnection Connection;
        protected readonly SqlTransaction Transaction;
        protected bool IsCommited;
        protected bool IsDisposed;
        protected IDictionary<Type, object> Repositories = new Dictionary<Type, object>();

        protected UnitOfWork(SqlConnection connection)
        {
            Connection = connection;
            Connection.FireInfoMessageEventOnUserErrors = true;
            Connection.InfoMessage += OnInfoMessage;
            Connection.StateChange += OnStateChange;
            Transaction = Connection.BeginTransaction();
            if (Connection.State == ConnectionState.Closed)
            {
                Connection.Open();
            }
        }

        public void Commit()
        {
            if (Transaction != null)
            {
                Transaction.Commit();
                IsCommited = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IRepository<T> Repository<T>() where T : class
        {
            if (!Repositories.Keys.Contains(typeof(IRepository<T>)))
                Repositories.Add(typeof(IRepository<T>), new Repository<T>(Connection, Transaction));
            return Repositories[typeof(IRepository<T>)] as IRepository<T>;
        }

        public object Repository(Type type)
        {
            if (!typeof(IRepository).IsAssignableFrom(type))
                throw new InvalidOperationException(string.Format("Type {0} not implement IRepository", type.Name));

            if (!Repositories.ContainsKey(type))
            {
                object instance = Activator.CreateInstance(type, Connection, Transaction);
                Repositories.Add(type, instance);
            }
            return Repositories[type];
        }

        public void Register(IRepository repository)
        {
            repository.SetSqlConnection(Connection);
            repository.SetSqlTransaction(Transaction);
            if (!Repositories.ContainsKey(repository.GetType()))
                Repositories.Add(repository.GetType(), repository);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!IsDisposed)
            {
                if (disposing)
                    if (Connection != null)
                    {
                        if (!IsCommited)
                            Transaction.Rollback();

                        Connection.Close();
                    }
                IsDisposed = true;
            }
        }

        private static void OnInfoMessage(object sender, SqlInfoMessageEventArgs args)
        {
            foreach (SqlError err in args.Errors)
            {
                Log.Information(
                    "The {0} has received a severity {1}, state {2} error number {3}\n" +
                    "on line {4} of procedure {5} on server {6}:\n{7}",
                    err.Source, err.Class, err.State, err.Number, err.LineNumber,
                    err.Procedure, err.Server, err.Message);
            }
        }

        private static void OnStateChange(object sender, StateChangeEventArgs args)
        {
            Log.Information(
                "The current Connection state has changed from {0} to {1}.",
                args.OriginalState, args.CurrentState);
        }

        public T ExecuteQuery<T>(string commandText, params object[] values)
        {
            SqlCommand command = new SqlCommand
            {
                Connection = Connection,
                Transaction = Transaction,
                CommandText = commandText,
                CommandType = CommandType.Text
            };

            if (values != null && values.Length > 0)
            {
                SetQueryParameters(command, values);
            }

            return ExecuteCommand<T>(command);
        }

        public T ExecuteQuery<T>(string commandText, IDictionary<string, object> values)
        {
            SqlCommand command = new SqlCommand
            {
                Connection = Connection,
                Transaction = Transaction,
                CommandText = commandText,
                CommandType = CommandType.Text
            };

            if (values != null && values.Count > 0)
            {
                SetParameters(command, values);
            }

            return ExecuteCommand<T>(command);
        }

        public T ExecuteFunction<T>(string commandText, params object[] values)
        {
            SqlCommand command = new SqlCommand
            {
                Connection = Connection,
                CommandText = commandText,
                Transaction = Transaction,
                CommandType = CommandType.StoredProcedure
            };

            if (values != null && values.Length > 0)
            {
                SetFunctionParameters(command, values);
            }

            return ExecuteCommand<T>(command);
        }

        public T ExecuteFunction<T>(string commandText, IDictionary<string, object> values)
        {
            SqlCommand command = new SqlCommand
            {
                Connection = Connection,
                CommandText = commandText,
                Transaction = Transaction,
                CommandType = CommandType.StoredProcedure
            };

            if (values != null && values.Count > 0)
            {
                SetParameters(command, values);
            }

            return ExecuteCommand<T>(command);
        }

        public int ExecuteNonQuery(string commandText, params object[] values)
        {
            SqlCommand command = new SqlCommand
            {
                Connection = Connection,
                CommandText = commandText,
                Transaction = Transaction,
                CommandType = CommandType.StoredProcedure
            };

            if (values != null && values.Length > 0)
            {
                SetFunctionParameters(command, values);
            }

            return command.ExecuteNonQuery();
        }

        public int ExecuteNonQuery(string commandText, IDictionary<string, object> values)
        {
            SqlCommand command = new SqlCommand
            {
                Connection = Connection,
                CommandText = commandText,
                Transaction = Transaction,
                CommandType = CommandType.StoredProcedure
            };

            if (values != null && values.Count > 0)
            {
                SetParameters(command, values);
            }

            return command.ExecuteNonQuery();
        }

        public Tuple<int, int> ExecuteNonQuery(string commandText, string returnParameterName,
            string outputParameterName, params object[] values)
        {
            if (returnParameterName == null)
            {
                throw new ArgumentNullException(nameof(returnParameterName));
            }

            if (outputParameterName == null)
            {
                throw new ArgumentNullException(nameof(outputParameterName));
            }

            SqlCommand command = new SqlCommand
            {
                Connection = Connection,
                CommandText = commandText,
                Transaction = Transaction,
                CommandType = CommandType.StoredProcedure
            };

            if (values != null && values.Length > 0)
            {
                SetFunctionParameters(command, values);
            }

            SqlParameter returnSqlParameter = command.Parameters.Add(returnParameterName, SqlDbType.Int);
            returnSqlParameter.Direction = ParameterDirection.ReturnValue;

            SqlParameter outputSqlParameter = command.Parameters.Add(outputParameterName, SqlDbType.Int);
            outputSqlParameter.Direction = ParameterDirection.Output;

            command.ExecuteNonQuery();

            int returnValue = (int)command.Parameters[returnParameterName].Value;
            int outPutValue = (int)command.Parameters[outputParameterName].Value;

            return new Tuple<int, int>(returnValue, outPutValue);
        }

        private T ExecuteCommand<T>(SqlCommand command)
        {
            Type returnType = typeof(T);
            T returnInstance;

            if (returnType.IsGenericType &&
                returnType.GetGenericTypeDefinition() == typeof(IList<>) ||
                returnType.GetGenericTypeDefinition() == typeof(IEnumerable<>) ||
                returnType.GetGenericTypeDefinition() == typeof(ICollection<>))
            {
                SqlDataReader reader = command.ExecuteReader();
                Type entityType = returnType.GetGenericArguments()[0];
                Type sqlDataReaderType = typeof(SqlDataReaderExtensions);
                MethodInfo convertToEntityMethod =
                    sqlDataReaderType.GetMethod("ConvertToEntity", BindingFlags.Public | BindingFlags.Static);
                MethodInfo convertToEntityGenericMethod = convertToEntityMethod.MakeGenericMethod(entityType);
                Type listType = typeof(List<>);
                Type constructedListType = listType.MakeGenericType(entityType);
                IList listInstance = (IList)Activator.CreateInstance(constructedListType, null);
                while (reader.Read())
                {
                    object entity = convertToEntityGenericMethod.Invoke(reader, new object[] { reader });
                    listInstance.Add(entity);
                }
                returnInstance = (T)listInstance;
            }
            else
            {
                returnInstance = (T)command.ExecuteScalar();
            }

            return returnInstance;
        }

        private void SetFunctionParameters(SqlCommand command, object[] values)
        {
            SqlCommandBuilder.DeriveParameters(command);
            SqlParameterCollection sqlParameterCollection = command.Parameters;

            if (sqlParameterCollection.Count > 0)
            {
                int index = 0;
                foreach (SqlParameter sqlParameter in sqlParameterCollection)
                {
                    if (sqlParameter.Direction == ParameterDirection.Input ||
                        sqlParameter.Direction == ParameterDirection.InputOutput)
                    {
                        sqlParameter.Value = values[index];
                        index++;
                    }
                }
            }
        }

        private void SetQueryParameters(SqlCommand command, object[] values)
        {
            string[] parameters = Regex.Matches(command.CommandText, @"\@\w+").Cast<Match>()
                .Select(m => m.Value)
                .ToArray();
            if (parameters.Length > 0)
            {
                for (int i = 0; i < parameters.Length; i++)
                {
                    SqlParameter sqlParameter = new SqlParameter
                    {
                        ParameterName = parameters[i],
                        SqlDbType = TypeConvertor.ToSqlDbType(values[i].GetType()),
                        Value = values[i]
                    };
                    command.Parameters.Add(sqlParameter);
                }
            }
        }

        private void SetParameters(SqlCommand command, IDictionary<string, object> values)
        {
            foreach (KeyValuePair<string, object> entry in values)
            {
                SqlParameter sqlParameter = new SqlParameter
                {
                    ParameterName = entry.Key,
                    SqlDbType = TypeConvertor.ToSqlDbType(entry.Value.GetType()),
                    Value = entry.Value
                };
                command.Parameters.Add(sqlParameter);
            }
        }
    }
}