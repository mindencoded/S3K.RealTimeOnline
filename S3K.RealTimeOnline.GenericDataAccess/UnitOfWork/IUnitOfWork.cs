﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using S3K.RealTimeOnline.GenericDataAccess.Repositories;

namespace S3K.RealTimeOnline.GenericDataAccess.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        void BeginTransaction();

        void Commit();

        void Close();

        void Open();

        Task OpenAsync();

        IRepository<T> Repository<T>() where T : class;

        object Repository(Type type);

        void Register(IRepository repository);

        T ExecuteQuery<T>(string statement, params object[] values);

        T ExecuteQuery<T>(string commandText, IDictionary<string, object> values);

        T ExecuteFunction<T>(string commandText, params object[] values);

        T ExecuteFunction<T>(string commandText, IDictionary<string, object> values);

        int ExecuteNonQuery(string commandText, params object[] values);

        int ExecuteNonQuery(string commandText, IDictionary<string, object> values);

        Tuple<int, int> ExecuteNonQuery(string commandText, string returnParameterName,
            string outputParameterName, params object[] values);
    }
}