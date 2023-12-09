﻿using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vido.Model.Model.Comon;

namespace Pos.Application.Extensions.Helper
{
    public static class ContextHelper
    {
        public static IDbConnection AutoConnect(this IDbConnection cnn)
        {
            if (cnn.State == ConnectionState.Closed)
            {
                cnn = new SqlConnection(Const.POS_CONNECTION_STRING);
                cnn.Open();
            }
            return cnn;
        }

        public static T SqlFirstOrDefault<T>(this IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            T result;
            try
            {
                result = cnn.QueryFirstOrDefault<T>(sql, param, transaction, commandTimeout, commandType);
                //cnn.Close();
            }
            catch
            {
                cnn.Close();
                throw;
            }
            return result;
        }

        public static async Task<T> SqlFirstOrDefaultAsync<T>(this IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            T result;
            try
            {
                result = await cnn.QueryFirstOrDefaultAsync<T>(sql, param, transaction, commandTimeout, commandType);
                //cnn.Close();
            }
            catch
            {
                cnn.Close();
                throw;
            }
            return result;
        }

        public static IEnumerable<T> SqlQuery<T>(this IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            IEnumerable<T> result;
            try
            {
                result = cnn.Query<T>(sql, param, transaction, buffered, commandTimeout, commandType);
                //cnn.Close();
            }
            catch
            {
                cnn.Close();
                throw;
            }
            return result;
        }
        
        public static Task<IEnumerable<T>> SqlQueryAsync<T>(this IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            Task<IEnumerable<T>> result;
            try
            {
                result = cnn.QueryAsync<T>(sql, param, transaction, commandTimeout, commandType);
                //cnn.Close();
            }
            catch
            {
                cnn.Close();
                throw;
            }
            return result;
        }

        public static int SqlExecute(this IDbConnection cnn, string sql, object? param = null, IDbTransaction? transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            int result;
            try
            {
                result = cnn.Execute(sql, param, transaction, commandTimeout, commandType);
                //cnn.Close();
            }
            catch
            {
                cnn.Close();
                throw;
            }
            return result;
        }
        public static async Task<int> SqlExecuteAsync(this IDbConnection cnn, string sql, object? param = null, IDbTransaction? transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            int result;
            try
            {
                result = await cnn.ExecuteAsync(sql, param, transaction, commandTimeout, commandType);
                //cnn.Close();
            }
            catch
            {
                cnn.Close();
                throw;
            }
            return result;
        }
    }
}
