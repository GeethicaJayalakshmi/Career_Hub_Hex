using System;
using Career_Hub.My_Exception;  
using Microsoft.Data.SqlClient;  
using Career_Hub.Util;  

namespace Career_Hub.Util  
{
    public static class DBConnUtil
    {
        public static SqlConnection GetConnection()
        {
            var connStr = Property.GetConnectionString();  
            var conn = new SqlConnection(connStr);
            conn.Open();
            return conn;
        }
    }
}
