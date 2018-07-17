﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Northwind.Shared;
using Northwind.WebRole.Domain;

namespace Northwind.WebRole.Tools
{
    public class DbInformation
    {
        private readonly SqlConnection _connection;
        private readonly SqlTransaction _transaction;

        public DbInformation(SqlConnection connection)
        {
            _connection = connection;
        }

        public DbInformation(SqlConnection connection, SqlTransaction transaction)
        {
            _connection = connection;
            _transaction = transaction;
        }

        public IList<ColumnInfo> ColumnSchema<T>() where T : class
        {
            string query = @"
                SELECT 
                    COLUMN_NAME,
                    ORDINAL_POSITION, 
                    IS_NULLABLE,
                    DATA_TYPE, 
                    CHARACTER_MAXIMUM_LENGTH, 
                    NUMERIC_PRECISION, 
                    NUMERIC_PRECISION_RADIX, 
                    NUMERIC_SCALE, 
                    DATETIME_PRECISION
                FROM 
                    information_schema.columns
                WHERE 
                    Table_Name = '" + EntityUtils.GetTableName<T>() + "'";
            IList<ColumnInfo> results;
            SqlCommand command = _transaction != null
                ? new SqlCommand(query, _connection, _transaction)
                : new SqlCommand(query, _connection);
            using (command)
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    results = reader.ConvertToList<ColumnInfo>();
                }
            }

            return results;
        }

        public ColumnInfo ColumnSchema<T>(string columnName) where T : class
        {
            string query = @"
                SELECT 
                    COLUMN_NAME,
                    ORDINAL_POSITION, 
                    IS_NULLABLE,
                    DATA_TYPE, 
                    CHARACTER_MAXIMUM_LENGTH, 
                    NUMERIC_PRECISION, 
                    NUMERIC_PRECISION_RADIX, 
                    NUMERIC_SCALE, 
                    DATETIME_PRECISION
                FROM 
                    information_schema.columns
                WHERE 
                    Table_Name = '" + EntityUtils.GetTableName<T>() + "' AND Column_Name = '" + columnName + "'";
            ColumnInfo columnInfo;
            SqlCommand command = _transaction != null
                ? new SqlCommand(query, _connection, _transaction)
                : new SqlCommand(query, _connection);
            using (command)
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    columnInfo = reader.ConvertToList<ColumnInfo>().FirstOrDefault();
                }
            }

            return columnInfo;
        }

        public string Version()
        {
            string serverVersion = _connection.ServerVersion;
            if (serverVersion != null)
            {
                string[] serverVersionDetails = serverVersion.Split(new[] {"."}, StringSplitOptions.None);

                int versionNumber = int.Parse(serverVersionDetails[0]);

                switch (versionNumber)
                {
                    case 8:
                        return "SQL Server 2000";
                    case 9:
                        return "SQL Server 2005";
                    case 10:
                        return "SQL Server 2008";
                    default:
                        return string.Format("SQL Server {0}", versionNumber);
                }
            }

            throw new Exception("Invalid Server Version");
        }
    }
}