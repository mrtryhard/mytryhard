using Npgsql;
using System.Data.Common;

namespace MyTryHard.Bll
{
    /// <summary>
    /// BaseContext is required for all additionnal context. 
    /// Each context group features under a category; 
    /// i.e. ArticlesContext regroups each operations appliable to Articles.
    /// </summary>
    public abstract class BaseContext
    {
        private readonly string _connStr;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connStr">Connection string.</param>
        public BaseContext(string connStr)
        {
            _connStr = connStr;
        }

        /// <summary>
        /// Opens a new connection. 
        /// Must be used within a using statement.
        /// 
        /// Garantees same type of connection is used (here, NpgsqlConnection).
        /// </summary>
        /// <returns>DbConnection handle.</returns>
        protected DbConnection OpenConnection()
        {
            DbConnection conn = new NpgsqlConnection(_connStr);
            conn.Open();
            return conn;
        }
    }
}
