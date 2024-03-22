using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Configuration;
using System.Web.Mvc;
using static Macreel_Project.Models.Bussiness;
using System.Web.Http.Cors;

namespace Macreel_Project.Services
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class GetStateController : ApiController
    {
        SqlCommand cmd;
        public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myconn"].ConnectionString);
        [System.Web.Http.HttpGet]
        public IHttpActionResult view_state()
        {
            List<state> list = new List<state>();
            try
            {
                cmd = new SqlCommand("Sp_Employee", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "State");
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                state select;
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        select= new state();    
                        select.Id = Convert.ToInt32(rd["ID"].ToString());
                        select.State_Name = rd["Name"].ToString();
                        list.Add(select);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
                cmd.Dispose();
            }
            return Ok(list);
        }
    }
}
