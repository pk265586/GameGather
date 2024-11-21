using System;
using Microsoft.Data.Sqlite;
using Dapper;

namespace GameGather.DataAccess.Provider
{
    public static class DapperExt
    {
        public static async Task<T[]> GetDataTableAsync<T>(string connectionString, string sqlCmd, object? parameters = null)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                return await GetDataTableAsync<T>(connection, sqlCmd, parameters);
            }
        }

        public static async Task<T[]> GetDataTableAsync<T>(SqliteConnection connection, string sqlCmd, object? parameters = null)
        {
            var result = await connection.QueryAsync<T>(sqlCmd, parameters);
            return result.ToArray();
        }

        public static async Task ExecAsync(string connectionString, string sqlCmd, object? parameters = null)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                await ExecAsync(connection, sqlCmd, parameters);
            }
        }

        public static async Task ExecAsync(SqliteConnection connection, string sqlCmd, object? parameters = null)
        {
            await connection.ExecuteAsync(sqlCmd, parameters);
        }
    }
}
