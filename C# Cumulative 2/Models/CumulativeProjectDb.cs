using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace C__Cumulative_2.Models
{
    public class CumulativeProjectDb
    {
        
        
        private static string User { get { return "root"; } }

        private static string Password { get { return "root"; } }

        private static string Database { get { return "librarydb"; } }

        private static string Server { get { return "localhost"; } }

        private static string Port { get { return "3306"; } }


       
        protected static string ConnectionString
        {
            get
            {
                return "server = " + Server
                    + "; user = " + User
                    + "; database = " + Database
                    + "; Port = " + Port
                    + "; Password = " + Password;
            }
        }

        //This method we use to connect to the databse.
        public MySqlConnection AccessDatabase()
        {
            //instantiating class.
            return new MySqlConnection(ConnectionString);
        }
    }
}