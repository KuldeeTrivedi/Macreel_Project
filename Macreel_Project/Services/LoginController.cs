using Macreel_Project.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.UI.WebControls;

namespace Macreel_Project.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class LoginController : ApiController
    {
        SqlConnection con;
        SqlCommand cmd;
        DataAccess db = new DataAccess();
        public LoginController()
        {
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["myconn"].ConnectionString);
        }
        public class CheckLoginController : ApiController
        {
            SqlCommand cmd;
            DataAccess db = new DataAccess();
            public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myconn"].ConnectionString);
            [System.Web.Http.HttpPost]
            public IHttpActionResult Login()
            {
                HttpRequest httpRequest = HttpContext.Current.Request;
                string UserName = httpRequest.Form.Get("UserName");
                string Password = httpRequest.Form.Get("Password");
                if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Password))
                {
                    return BadRequest("UserName and Password are required fields");
                }
                var ad = new Bussiness.Login
                {
                    UserName = UserName,
                    Password = Password
                };
                // Instantiate the data access layer
                Macreel_Project.Models.Bussiness.Login response = db.CheckLogin(UserName, Password);
                return Ok(response);
            }
        }


    }
}
