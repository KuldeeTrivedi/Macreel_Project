using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Macreel_Project.Models
{
    public class DataFactory
    {

        public SqlCommand cmd;
        public SqlConnection con;
        public DataFactory()
        {
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["myconn"].ConnectionString);
        }
    }
}