using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiForShiTiKu.Helper
{

    /// Title  ：MySqlHelper
    /// Author ：WinterT
    /// Date   ：2015-1-8 08:12:54
    /// Description：
    ///       ExecuteNonQuery
    ///       ExecuteScalar
    ///       ExecuteReader
    ///       ExecuteTable
    /// </summary>
    public static class MySqlHelper
    {
        /// <summary>
        /// 返回配置文件中指定的连接
        /// </summary>
        /// <returns>配置文件中指定的连接</returns>
        private static MySqlConnection GetConnection()
        {
            string connString = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            return new MySqlConnection(connString);
        }
        #region ExecuteNonQuery
        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns>受影响行数</returns>
        public static int ExecuteNonQuery(string sql)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                using (MySqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    return cmd.ExecuteNonQuery();
                }
            }
        }
        /// <summary>
        /// 根据给定连接，执行带参数的SQL语句
        /// </summary>
        /// <param name="conn">连接、使用前确保连接以打开。</param>
        /// <param name="sql">带参数的sql语句</param>
        /// <param name="paras">参数</param>
        /// <returns>受影响行数</returns>
        public static int ExecuteNonQuery
            (MySqlConnection conn, string sql, params MySqlParameter[] paras)
        {
            conn.Open();
            using (MySqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = sql;
                cmd.Parameters.AddRange(paras);
                return cmd.ExecuteNonQuery();
            }
        }
        /// <summary>
        /// 执行带参数的SQL语句
        /// </summary>
        /// <param name="sql">带参数的sql语句</param>
        /// <param name="paras">参数</param>
        /// <returns>受影响行数</returns>
        public static int ExecuteNonQuery
            (string sql, params MySqlParameter[] paras)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                return ExecuteNonQuery(conn, sql, paras);
            }
        }
        #endregion
        #region ExecuteScalar
        /// <summary>
        /// 执行sql语句，返回第一行第一列
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns>第一行第一列</returns>
        public static Object ExecuteScalar(string sql)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                using (MySqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    return cmd.ExecuteScalar();
                }
            }
        }
        /// <summary>
        /// 根据Connection对象，执行带参数的sql语句，返回第一行第一列
        /// </summary>
        /// <param name="conn">连接</param>
        /// <param name="sql">sql语句</param>
        /// <param name="paras">参数</param>
        /// <returns>返回第一行第一列</returns>
        public static object ExecuteScalar
            (MySqlConnection conn, string sql, MySqlParameter[] paras)
        {
            conn.Open();
            using (MySqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = sql;
                cmd.Parameters.AddRange(paras);
                return cmd.ExecuteScalar();
            }
        }
        /// <summary>
        /// 执行带参数的sql语句，返回第一行第一列
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="paras">参数</param>
        /// <returns>返回第一行第一列</returns>
        public static object ExecuteScalar
            (string sql, MySqlParameter[] paras)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                return ExecuteScalar(conn, sql, paras);
            }
        }
        #endregion
        #region ExecuteReader
        /// <summary>
        /// 执行sql语句，返回一个MySqlDataReader
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns>一个MySqlDataReader对象</returns>
        public static MySqlDataReader ExecuteReader(string sql)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                using (MySqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    conn.Open();
                    return cmd.ExecuteReader(CommandBehavior.CloseConnection);
                }
            }
        }
        /// <summary>
        /// 根据指定的连接，执行带参数的sql语句，返回一个Reader对象
        /// </summary>
        /// <param name="conn">连接</param>
        /// <param name="sql">sql语句</param>
        /// <param name="paras">参数</param>
        /// <returns>一个MySqlDataReader对象</returns>
        public static MySqlDataReader ExecuteReader
            (MySqlConnection conn, string sql, params MySqlParameter[] paras)
        {
            conn.Open();
            using (MySqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = sql;
                cmd.Parameters.AddRange(paras);
                return cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
        }
        /// <summary>
        /// 执行带参数的sql语句，返回一个Reader对象
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="paras">参数</param>
        /// <returns>一个MySqlDataReader对象</returns>
        public static MySqlDataReader ExecuteReader
            (string sql, params MySqlParameter[] paras)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                using (MySqlCommand cmd = conn.CreateCommand())
                {
                    return ExecuteReader(conn, sql, paras);
                }
            }
        }
        #endregion
        #region ExecuteTable
        /// <summary>
        /// 执行sql语句，返回一个DataTable
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns>DataTable</returns>
        public static DataTable ExecuteTable(string sql)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                using (MySqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        DataTable table = new DataTable();
                        table.Load(reader);
                        return table;
                    }
                }
            }
        }
        /// <summary>
        /// 根据连接，执行带参数的sql语句，返回一个DataTable
        /// </summary>
        /// <param name="conn">连接，切记连接已打开</param>
        /// <param name="sql">sql语句</param>
        /// <param name="paras">参数</param>
        /// <returns>DataTable</returns>
        public static DataTable ExecuteTable
            (MySqlConnection conn, string sql, params MySqlParameter[] paras)
        {
            conn.Open();
            using (MySqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = sql;
                cmd.Parameters.AddRange(paras);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    DataTable table = new DataTable();
                    table.Load(reader);
                    return table;
                }
            }
        }
        /// <summary>
        /// 执行带参数的sql语句
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="paras">参数</param>
        /// <returns>DataTable</returns>
        public static DataTable ExecuteTable
            (string sql, params MySqlParameter[] paras)
        {
            using (MySqlConnection conn = GetConnection())
            {
                return ExecuteTable(conn, sql, paras);
            }
        }

        #endregion
    }
}
