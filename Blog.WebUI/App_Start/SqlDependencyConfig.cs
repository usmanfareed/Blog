using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Caching;

namespace Blog.WebUI.App_Start
{
    public class SqlDependencyConfig
    {
        public static string connection
        {
            get { return ConfigurationManager.ConnectionStrings["BlogConnection"].ConnectionString; }
        }

        public static void SqlDependecyStart()
        {
            SqlCacheDependencyAdmin.EnableTableForNotifications(connection, "Posts");
            System.Data.SqlClient.SqlDependency.Start(connection);
        }        public static void SqlDependecyStop()
        {
            System.Data.SqlClient.SqlDependency.Stop(connection);
        }
    }
}