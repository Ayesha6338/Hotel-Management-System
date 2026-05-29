using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;

namespace HotelManagementSystem.Database
{
    public abstract class BaseRepository
    {
        protected SQLiteConnection GetConnection()
        {
            return DbConnection.GetConnection();
        }

        protected int ExecuteNonQuery(string sql, SQLiteParameter[] parameters = null)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    if (parameters != null) cmd.Parameters.AddRange(parameters);
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        protected object ExecuteScalar(string sql, SQLiteParameter[] parameters = null)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    if (parameters != null) cmd.Parameters.AddRange(parameters);
                    return cmd.ExecuteScalar();
                }
            }
        }

        protected T ExecuteSingle<T>(string sql, Func<SQLiteDataReader, T> mapper,
            SQLiteParameter[] parameters = null)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    if (parameters != null) cmd.Parameters.AddRange(parameters);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read()) return mapper(reader);
                        return default(T);
                    }
                }
            }
        }

        protected List<T> ExecuteList<T>(string sql, Func<SQLiteDataReader, T> mapper,
            SQLiteParameter[] parameters = null)
        {
            var results = new List<T>();
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    if (parameters != null) cmd.Parameters.AddRange(parameters);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read()) results.Add(mapper(reader));
                    }
                }
            }
            return results;
        }

        protected string GetString(SQLiteDataReader reader, string col)
        {
            int i = reader.GetOrdinal(col);
            return reader.IsDBNull(i) ? string.Empty : reader.GetString(i);
        }

        protected int GetInt(SQLiteDataReader reader, string col)
        {
            int i = reader.GetOrdinal(col);
            return reader.IsDBNull(i) ? 0 : reader.GetInt32(i);
        }
    }
}