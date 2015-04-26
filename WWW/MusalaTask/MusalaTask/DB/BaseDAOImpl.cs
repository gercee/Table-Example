using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MusalaTask.Models;
using MySql.Data.MySqlClient;

namespace MusalaTask.DB
{
    /// <summary>
    /// Base DAO Implementation which will be used for all DAO implementation as Super class.
    /// </summary>
    public class BaseDAOImpl
    {

        /// <summary>
        /// The connection string initialized from the web.config on App_Start in Global.asax.
        /// </summary>
        public static string connectionString { get; set; }
       
        /// <summary>
        /// Method that check for String attribute in the MySql reader.
        /// </summary>
        /// <param name="SqlFieldName">Name of the attribute.</param>
        /// <param name="Reader">MySqlDataReader object.</param>
        /// <returns>if the value is null return empty string else return the value of the String</returns>
        protected string getDBString(string SqlFieldName, MySqlDataReader Reader)
        {
            return Reader[SqlFieldName].Equals(DBNull.Value) ? String.Empty : Reader.GetString(SqlFieldName);
        }

        /// <summary>
        /// Method that check for Integer attribute in the MySql reader.
        /// </summary>
        /// <param name="SqlFieldName">Name of the attribute.</param>
        /// <param name="Reader">MySqlDataReader object.</param>
        /// <returns>Int value for the specified attribute</returns>
        protected int getDBInteger(string SqlFieldName, MySqlDataReader Reader)
        {
            return Reader.GetInt32(SqlFieldName);
        }


        /// <summary>
        /// Method that check for DateTime attribute in the MySql reader.
        /// </summary>
        /// <param name="SqlFieldName">Name of the attribute.</param>
        /// <param name="Reader">MySqlDataReader object.</param>
        /// <returns>DateTime value for the specified attribute</returns>
        protected DateTime getDBDateTime(string SqlFieldName, MySqlDataReader Reader)
        {
            return Reader.GetDateTime(SqlFieldName);
        }

    }
}