using Macreel_Project.Models.Admin;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Macreel_Project.Services
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class RegistrationController : ApiController
    {
        SqlConnection con;
        SqlCommand cmd;
        public RegistrationController()
        {
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["myconn"].ConnectionString);
        }
        [HttpPost]
        public IHttpActionResult Employee_Register()
        {
            var httpRequest = HttpContext.Current.Request;
            try
            {
                var empobj = new Models.Bussiness.Employee
                {
                    Id = Convert.ToInt32(httpRequest.Form.Get("Id")),
                    EmployeeCode=httpRequest.Form.Get("EmployeeCode"),
                    EmployeeName = httpRequest.Form.Get("EmployeeName"),
                    Department = httpRequest.Form.Get("Department"),
                    Designation = httpRequest.Form.Get("Designation"),
                    EmailId = httpRequest.Form.Get("EmailId"),
                    DOJ = httpRequest.Form.Get("DOJ"),
                    PanNo = httpRequest.Form.Get("PanNo"),
                    Passport = httpRequest.Form.Get("Passport"),
                    Mobile = httpRequest.Form.Get("Mobile"),
                    UserName = httpRequest.Form.Get("UserName"),
                    Password = httpRequest.Form.Get("Password"),
                    BankName = httpRequest.Form.Get("BankName"),
                    AccountNo = httpRequest.Form.Get("AccountNo"),
                    IFSC = httpRequest.Form.Get("IFSC"),
                    BankBranch = httpRequest.Form.Get("BankBranch"),
                    BloodGroup = httpRequest.Form.Get("BloodGroup"),
                    IdMarks = httpRequest.Form.Get("IdMarks"),
                    DOB = httpRequest.Form.Get("DOB"),
                    Age = httpRequest.Form.Get("Age"),
                    Sex = httpRequest.Form.Get("Sex"),
                    Nationality = httpRequest.Form.Get("Nationality"),
                    Religion = httpRequest.Form.Get("Religion"),
                    MaritalStatus = httpRequest.Form.Get("MaritalStatus"),
                    PresentAddress = httpRequest.Form.Get("PresentAddress"),
                    State = httpRequest.Form.Get("State"),
                    City = httpRequest.Form.Get("City"),
                    Pin = httpRequest.Form.Get("Pin"),
                    EmergencyContactPerson = httpRequest.Form.Get("EmergencyContactPerson"),
                    EmergencyContactNumber = httpRequest.Form.Get("EmergencyContactNumber"),
                    EmergencyContectAddress = httpRequest.Form.Get("EmergencyContectAddress"),
                    ReferencesName = httpRequest.Form.Get("ReferencesName"),
                    ContactNo = httpRequest.Form.Get("ContactNo"),
                    CompanyName = httpRequest.Form.Get("CompanyName"),
                    CemailId = httpRequest.Form.Get("CemailId"),
                    PreviousEmployer = httpRequest.Form.Get("PreviousEmployer"),
                    From = httpRequest.Form.Get("FromT"),
                    To = httpRequest.Form.Get("ToT"),
                    Degree = httpRequest.Form.Get("Degree"),
                    ProfessionalInstitution = httpRequest.Form.Get("ProfessionalInstitution"),
                    ProfessionalPassingYear = httpRequest.Form.Get("ProfessionalPassingYear"),
                    ProfessionalSpecilization = httpRequest.Form.Get("ProfessionalSpecilization"),
                    Board10 = httpRequest.Form.Get("Board10"),
                    Institution10 = httpRequest.Form.Get("Institution10"),
                    PassingYear10 = httpRequest.Form.Get("PassingYear10"),
                    Specilization10 = httpRequest.Form.Get("Specilization10"),
                    Board12 = httpRequest.Form.Get("Board12"),
                    Institution12 = httpRequest.Form.Get("Institution12"),
                    PassingYear12 = httpRequest.Form.Get("PassingYear12"),
                    Specilization12 = httpRequest.Form.Get("Specilization12"),
                    adharno = httpRequest.Form.Get("AdharNo"),
                    //Image = httpRequest.Files["Image"],
                    ImagePath = httpRequest.Form.Get("ImagePath"),
                    ReportingManager = httpRequest.Form.Get("ReportingManager"),
                };
                if (httpRequest.Files.Count > 0)
                {
                    var PostedFile = httpRequest.Files[0];
                    string FilePath = Path.Combine(HttpContext.Current.Server.MapPath("/Upload/"), PostedFile.FileName);
                    PostedFile.SaveAs(FilePath);
                    empobj.ImagePath = "/Upload/" + PostedFile.FileName;//save the filepath in the database
                }
                cmd = new SqlCommand("[Sp_Employee]", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "Insert");
                cmd.Parameters.AddWithValue("@Id", empobj.Id);
                cmd.Parameters.AddWithValue("@EmployeeCode", empobj.EmployeeCode);
                cmd.Parameters.AddWithValue("@EmployeeName", empobj.EmployeeName);
                cmd.Parameters.AddWithValue("@Department", empobj.Department);
                cmd.Parameters.AddWithValue("@Designation", empobj.Designation);
                cmd.Parameters.AddWithValue("@EmailId", empobj.EmailId);
                cmd.Parameters.AddWithValue("@DOJ", empobj.DOJ);
                cmd.Parameters.AddWithValue("@PanNo", empobj.PanNo);
                cmd.Parameters.AddWithValue("@AdharNo", empobj.adharno);
                cmd.Parameters.AddWithValue("@Passport", empobj.Passport);
                cmd.Parameters.AddWithValue("@Mobile", empobj.Mobile);
                cmd.Parameters.AddWithValue("@UserName", empobj.UserName);
                cmd.Parameters.AddWithValue("@Password", empobj.Password);
                cmd.Parameters.AddWithValue("@BankName", empobj.BankName);
                cmd.Parameters.AddWithValue("@AccountNo", empobj.AccountNo);
                cmd.Parameters.AddWithValue("@IFSC", empobj.IFSC);
                cmd.Parameters.AddWithValue("@BankBranch", empobj.BankBranch);
                cmd.Parameters.AddWithValue("@BloodGroup", empobj.BloodGroup);
                cmd.Parameters.AddWithValue("@IdMarks", empobj.IdMarks);
                cmd.Parameters.AddWithValue("@DOB", empobj.DOB);
                cmd.Parameters.AddWithValue("@Age", empobj.Age);
                cmd.Parameters.AddWithValue("@Sex", empobj.Sex);
                cmd.Parameters.AddWithValue("@Nationality", empobj.Nationality);
                cmd.Parameters.AddWithValue("@Religion", empobj.Religion);
                cmd.Parameters.AddWithValue("@MaritalStatus", empobj.MaritalStatus);
                cmd.Parameters.AddWithValue("@PresentAddress", empobj.PresentAddress);
                cmd.Parameters.AddWithValue("@State", empobj.State);
                cmd.Parameters.AddWithValue("@City", empobj.City);
                cmd.Parameters.AddWithValue("@Pin", empobj.Pin);
                cmd.Parameters.AddWithValue("@EmergencyContactPerson", empobj.EmergencyContactPerson);
                cmd.Parameters.AddWithValue("@EmergencyContactNumber", empobj.EmergencyContactNumber);
                cmd.Parameters.AddWithValue("@EmergencyContectAddress", empobj.EmergencyContectAddress);
                cmd.Parameters.AddWithValue("@ReferencesName", empobj.ReferencesName);
                cmd.Parameters.AddWithValue("@ContactNo", empobj.ContactNo);
                cmd.Parameters.AddWithValue("@CompanyName", empobj.CompanyName);
                cmd.Parameters.AddWithValue("@CemailId", empobj.CemailId);
                cmd.Parameters.AddWithValue("@PreviousEmployer", empobj.PreviousEmployer);
                cmd.Parameters.AddWithValue("@From", empobj.From);
                cmd.Parameters.AddWithValue("@To", empobj.To);
                cmd.Parameters.AddWithValue("@Degree", empobj.Degree);
                cmd.Parameters.AddWithValue("@ProfessionalInstitution", empobj.ProfessionalInstitution);
                cmd.Parameters.AddWithValue("@ProfessionalPassingYear", empobj.ProfessionalPassingYear);
                cmd.Parameters.AddWithValue("@ProfessionalSpecilization", empobj.ProfessionalSpecilization);
                cmd.Parameters.AddWithValue("@Board10", empobj.Board10);
                cmd.Parameters.AddWithValue("@Institution10", empobj.Institution10);
                cmd.Parameters.AddWithValue("@PassingYear10", empobj.PassingYear10);
                cmd.Parameters.AddWithValue("@Specilization10", empobj.Specilization10);
                cmd.Parameters.AddWithValue("@Board12", empobj.Board12);
                cmd.Parameters.AddWithValue("@Institution12", empobj.Institution12);
                cmd.Parameters.AddWithValue("@PassingYear12", empobj.PassingYear12);
                cmd.Parameters.AddWithValue("@Specilization12", empobj.Specilization12);
                cmd.Parameters.AddWithValue("@Image", empobj.ImagePath);
                cmd.Parameters.AddWithValue("@UserId", empobj.UserId);
                cmd.Parameters.AddWithValue("@StateId", empobj.StateId);
                cmd.Parameters.AddWithValue("@ReportingManager", empobj.ReportingManager);
                if (con.State == System.Data.ConnectionState.Closed)
                    con.Open();
                int count = cmd.ExecuteNonQuery();
                if (count > 0)
                {
                    using (SmtpClient smtpClient = new SmtpClient("smtp.gmail.com"))
                    {
                        smtpClient.Port = 587;
                        smtpClient.Credentials = new NetworkCredential("macreel@gmail.com", "ryhnhbptuymbsscw");
                        smtpClient.EnableSsl = true;
                        //create html email Template
                        string message = $@"
<html>
<head>
    <style>
        h3 {{ color: red; }}
        table {{
            border-collapse: collapse;
            width: 50%;
            margin: auto; /* Center the table */
        }}
        th, td {{
            border: 1px solid #dddddd;
            text-align: left;
            padding: 8px;
        }}
    </style>
</head>
<body>
    <p>Welcome To,</p>
    <h3>Macreel Infosoft PVT. LTD.</h3>
    <table>
        <tr>
            <th>Your Registration has been Succesfully</th>
            <th></th>
        </tr>
        <tr>
            <td>UserName:</td>
            <td><strong>{empobj.UserName}</strong></td>
        </tr>
        <tr>
            <td>Password:</td>
            <td><strong>{empobj.Password}</strong></td>
        </tr>
    </table>
    <p>From Macreel Infosoft Pvt Ltd..</p>
    <img src=''/>
</body>
</html>";
                        MailMessage mailMessage = new MailMessage
                        {
                            From = new MailAddress("macreel@gmail.com"),
                            Subject = "Macreel Infosoft Pvt Ltd || Registration",
                            IsBodyHtml = true,
                            Body = message,
                        };
                        mailMessage.To.Add(empobj.EmailId);
                        smtpClient.Send(mailMessage);
                    }
                    return Ok("Sucess");
                }
                else
                {
                    return Ok("Already Exist");
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                con.Close();
            }
        }
        [System.Web.Http.HttpGet]
        public IHttpActionResult Get_Emp()
        {
            List<Models.Bussiness.Employee> emplist = new List<Models.Bussiness.Employee>();
            Models.Bussiness.Employee empobj = new Models.Bussiness.Employee();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("[Sp_Employee]", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "BindEmp");
                cmd.Parameters.AddWithValue("@Id", empobj.Id);
                cmd.Parameters.AddWithValue("@EmployeeCode", empobj.EmployeeCode);
                cmd.Parameters.AddWithValue("@EmployeeName", empobj.EmployeeName);
                cmd.Parameters.AddWithValue("@Department", empobj.Department);
                cmd.Parameters.AddWithValue("@Designation", empobj.Designation);
                cmd.Parameters.AddWithValue("@EmailId", empobj.EmailId);
                cmd.Parameters.AddWithValue("@DOJ", empobj.DOJ);
                cmd.Parameters.AddWithValue("@PanNo", empobj.PanNo);
                cmd.Parameters.AddWithValue("@AdharNo", empobj.adharno);
                cmd.Parameters.AddWithValue("@Passport", empobj.Passport);
                cmd.Parameters.AddWithValue("@Mobile", empobj.Mobile);
                cmd.Parameters.AddWithValue("@UserName", empobj.UserName);
                cmd.Parameters.AddWithValue("@Password", empobj.Password);
                cmd.Parameters.AddWithValue("@BankName", empobj.BankName);
                cmd.Parameters.AddWithValue("@AccountNo", empobj.AccountNo);
                cmd.Parameters.AddWithValue("@IFSC", empobj.IFSC);
                cmd.Parameters.AddWithValue("@BankBranch", empobj.BankBranch);
                cmd.Parameters.AddWithValue("@BloodGroup", empobj.BloodGroup);
                cmd.Parameters.AddWithValue("@IdMarks", empobj.IdMarks);
                cmd.Parameters.AddWithValue("@DOB", empobj.DOB);
                cmd.Parameters.AddWithValue("@Age", empobj.Age);
                cmd.Parameters.AddWithValue("@Sex", empobj.Sex);
                cmd.Parameters.AddWithValue("@Nationality", empobj.Nationality);
                cmd.Parameters.AddWithValue("@Religion", empobj.Religion);
                cmd.Parameters.AddWithValue("@MaritalStatus", empobj.MaritalStatus);
                cmd.Parameters.AddWithValue("@PresentAddress", empobj.PresentAddress);
                cmd.Parameters.AddWithValue("@State", empobj.State);
                cmd.Parameters.AddWithValue("@City", empobj.City);
                cmd.Parameters.AddWithValue("@Pin", empobj.Pin);
                cmd.Parameters.AddWithValue("@EmergencyContactPerson", empobj.EmergencyContactPerson);
                cmd.Parameters.AddWithValue("@EmergencyContactNumber", empobj.EmergencyContactNumber);
                cmd.Parameters.AddWithValue("@EmergencyContectAddress", empobj.EmergencyContectAddress);
                cmd.Parameters.AddWithValue("@ReferencesName", empobj.ReferencesName);
                cmd.Parameters.AddWithValue("@ContactNo", empobj.ContactNo);
                cmd.Parameters.AddWithValue("@CompanyName", empobj.CompanyName);
                cmd.Parameters.AddWithValue("@CemailId", empobj.CemailId);
                cmd.Parameters.AddWithValue("@PreviousEmployer", empobj.PreviousEmployer);
                cmd.Parameters.AddWithValue("@From", empobj.From);
                cmd.Parameters.AddWithValue("@To", empobj.To);
                cmd.Parameters.AddWithValue("@Degree", empobj.Degree);
                cmd.Parameters.AddWithValue("@ProfessionalInstitution", empobj.ProfessionalInstitution);
                cmd.Parameters.AddWithValue("@ProfessionalPassingYear", empobj.ProfessionalPassingYear);
                cmd.Parameters.AddWithValue("@ProfessionalSpecilization", empobj.ProfessionalSpecilization);
                cmd.Parameters.AddWithValue("@Board10", empobj.Board10);
                cmd.Parameters.AddWithValue("@Institution10", empobj.Institution10);
                cmd.Parameters.AddWithValue("@PassingYear10", empobj.PassingYear10);
                cmd.Parameters.AddWithValue("@Specilization10", empobj.Specilization10);
                cmd.Parameters.AddWithValue("@Board12", empobj.Board12);
                cmd.Parameters.AddWithValue("@Institution12", empobj.Institution12);
                cmd.Parameters.AddWithValue("@PassingYear12", empobj.PassingYear12);
                cmd.Parameters.AddWithValue("@Specilization12", empobj.Specilization12);
                cmd.Parameters.AddWithValue("@Image", empobj.ImagePath);
                cmd.Parameters.AddWithValue("@UserId", empobj.UserId);
                cmd.Parameters.AddWithValue("@StateId", empobj.StateId);
                cmd.Parameters.AddWithValue("@ReportingManager", empobj.ReportingManager);
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        empobj = new Models.Bussiness.Employee();
                        empobj.Id = Convert.ToInt32(sdr["Id"].ToString());
                        empobj.EmployeeCode = sdr["EmployeeCode"].ToString();
                        empobj.EmployeeName = sdr["EmployeeName"].ToString();
                        empobj.Department = sdr["Department"].ToString();
                        empobj.Designation = sdr["Designation"].ToString();
                        empobj.EmailId = sdr["EmailId"].ToString();
                        empobj.DOJ = sdr["DOJ"].ToString();
                        empobj.PanNo = sdr["PanNo"].ToString();
                        empobj.adharno = sdr["AdharNo"].ToString();
                        empobj.Passport = sdr["Passport"].ToString();
                        empobj.Mobile = sdr["Mobile"].ToString();
                        empobj.UserName = sdr["UserName"].ToString();
                        empobj.Password = sdr["Password"].ToString();
                        empobj.BankName = sdr["BankName"].ToString();
                        empobj.AccountNo = sdr["AccountNo"].ToString();
                        empobj.IFSC = sdr["IFSC"].ToString();
                        empobj.BankBranch = sdr["BankBranch"].ToString();
                        empobj.BloodGroup = sdr["BloodGroup"].ToString();
                        empobj.IdMarks = sdr["IdMarks"].ToString();
                        empobj.IdMarks = sdr["IdMarks"].ToString();
                        empobj.DOB = sdr["DOB"].ToString();
                        empobj.Age = sdr["Age"].ToString();
                        empobj.Sex = sdr["Sex"].ToString();
                        empobj.Nationality = sdr["Nationality"].ToString();
                        empobj.Religion = sdr["Religion"].ToString();
                        empobj.MaritalStatus = sdr["MaritalStatus"].ToString();
                        empobj.PresentAddress = sdr["PresentAddress"].ToString();
                        empobj.State = sdr["State"].ToString();
                        empobj.City = sdr["City"].ToString();
                        empobj.Pin = sdr["Pin"].ToString();
                        empobj.EmergencyContactPerson = sdr["EmergencyContactPerson"].ToString();
                        empobj.EmergencyContactNumber = sdr["EmergencyContactNumber"].ToString();
                        empobj.EmergencyContectAddress = sdr["EmergencyContectAddress"].ToString();
                        empobj.ReferencesName = sdr["ReferencesName"].ToString();
                        empobj.ContactNo = sdr["ContactNo"].ToString();
                        empobj.CompanyName = sdr["CompanyName"].ToString();
                        empobj.CemailId = sdr["CemailId"].ToString();
                        empobj.PreviousEmployer = sdr["PreviousEmployer"].ToString();
                        empobj.From = sdr["FromT"].ToString();
                        empobj.To = sdr["ToT"].ToString();
                        empobj.Degree = sdr["Degree"].ToString();
                        empobj.ProfessionalInstitution = sdr["ProfessionalInstitution"].ToString();
                        empobj.ProfessionalPassingYear = sdr["ProfessionalPassingYear"].ToString();
                        empobj.ProfessionalSpecilization = sdr["ProfessionalSpecilization"].ToString();
                        empobj.Board10 = sdr["Board10"].ToString();
                        empobj.Institution10 = sdr["Institution10"].ToString();
                        empobj.PassingYear10 = sdr["PassingYear10"].ToString();
                        empobj.Specilization10 = sdr["Specilization10"].ToString();
                        empobj.Board12 = sdr["Board12"].ToString();
                        empobj.Institution12 = sdr["Institution12"].ToString();
                        empobj.PassingYear12 = sdr["PassingYear12"].ToString();
                        empobj.Specilization12 = sdr["Specilization12"].ToString();
                        empobj.UserId = sdr["UserId"].ToString();
                        empobj.StateId = sdr["StateId"].ToString();
                        empobj.ImagePath = sdr["Image"].ToString();
                        empobj.ReportingManager = sdr["ReportingManager"].ToString();
                        emplist.Add(empobj);

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
            }
            return Ok(emplist);

        }
        [System.Web.Http.HttpGet]
        public IHttpActionResult Get_Emp(string Id)
        {
            List<Models.Bussiness.Employee> emplist = new List<Models.Bussiness.Employee>();
            Models.Bussiness.Employee empobj = new Models.Bussiness.Employee();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("[Sp_Employee]", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "EmpProfile");
                cmd.Parameters.AddWithValue("@Id",Id);
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        empobj = new Models.Bussiness.Employee();
                        empobj.Id = Convert.ToInt32(sdr["Id"].ToString());
                        empobj.EmployeeCode = sdr["EmployeeCode"].ToString();
                        empobj.EmployeeName = sdr["EmployeeName"].ToString();
                        empobj.Department = sdr["Department"].ToString();
                        empobj.Designation = sdr["Designation"].ToString();
                        empobj.EmailId = sdr["EmailId"].ToString();
                        empobj.DOJ = sdr["DOJ"].ToString();
                        empobj.PanNo = sdr["PanNo"].ToString();
                        empobj.adharno = sdr["AdharNo"].ToString();
                        empobj.Passport = sdr["Passport"].ToString();
                        empobj.Mobile = sdr["Mobile"].ToString();
                        empobj.UserName = sdr["UserName"].ToString();
                        empobj.Password = sdr["Password"].ToString();
                        empobj.BankName = sdr["BankName"].ToString();
                        empobj.AccountNo = sdr["AccountNo"].ToString();
                        empobj.IFSC = sdr["IFSC"].ToString();
                        empobj.BankBranch = sdr["BankBranch"].ToString();
                        empobj.BloodGroup = sdr["BloodGroup"].ToString();
                        empobj.IdMarks = sdr["IdMarks"].ToString();
                        empobj.IdMarks = sdr["IdMarks"].ToString();
                        empobj.DOB = sdr["DOB"].ToString();
                        empobj.Age = sdr["Age"].ToString();
                        empobj.Sex = sdr["Sex"].ToString();
                        empobj.Nationality = sdr["Nationality"].ToString();
                        empobj.Religion = sdr["Religion"].ToString();
                        empobj.MaritalStatus = sdr["MaritalStatus"].ToString();
                        empobj.PresentAddress = sdr["PresentAddress"].ToString();
                        empobj.State = sdr["State"].ToString();
                        empobj.City = sdr["City"].ToString();
                        empobj.Pin = sdr["Pin"].ToString();
                        empobj.EmergencyContactPerson = sdr["EmergencyContactPerson"].ToString();
                        empobj.EmergencyContactNumber = sdr["EmergencyContactNumber"].ToString();
                        empobj.EmergencyContectAddress = sdr["EmergencyContectAddress"].ToString();
                        empobj.ReferencesName = sdr["ReferencesName"].ToString();
                        empobj.ContactNo = sdr["ContactNo"].ToString();
                        empobj.CompanyName = sdr["CompanyName"].ToString();
                        empobj.CemailId = sdr["CemailId"].ToString();
                        empobj.PreviousEmployer = sdr["PreviousEmployer"].ToString();
                        empobj.From = sdr["FromT"].ToString();
                        empobj.To = sdr["ToT"].ToString();
                        empobj.Degree = sdr["Degree"].ToString();
                        empobj.ProfessionalInstitution = sdr["ProfessionalInstitution"].ToString();
                        empobj.ProfessionalPassingYear = sdr["ProfessionalPassingYear"].ToString();
                        empobj.ProfessionalSpecilization = sdr["ProfessionalSpecilization"].ToString();
                        empobj.Board10 = sdr["Board10"].ToString();
                        empobj.Institution10 = sdr["Institution10"].ToString();
                        empobj.PassingYear10 = sdr["PassingYear10"].ToString();
                        empobj.Specilization10 = sdr["Specilization10"].ToString();
                        empobj.Board12 = sdr["Board12"].ToString();
                        empobj.Institution12 = sdr["Institution12"].ToString();
                        empobj.PassingYear12 = sdr["PassingYear12"].ToString();
                        empobj.Specilization12 = sdr["Specilization12"].ToString();
                        empobj.UserId = sdr["UserId"].ToString();
                        empobj.StateId = sdr["StateId"].ToString();
                        empobj.StateId = sdr["StateId"].ToString();
                        empobj.ImagePath = sdr["Image"].ToString();
                        emplist.Add(empobj);

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
            }
            return Ok(emplist);
        }
        [System.Web.Http.HttpDelete]
        public IHttpActionResult Delete_Emp(string Id)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("[Sp_Employee]", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "DeleteEmp");
                cmd.Parameters.AddWithValue("@Id",Id);
                int count = cmd.ExecuteNonQuery();
                if (count > 0)
                {
                    return Ok("Data Deleted Successfully..");
                }
                else
                {
                    return Ok("Data Not Deleted..");
                }
            }catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
        }
        [System.Web.Http.HttpPost]
        public IHttpActionResult update_Employee(string id)
        {
            var httpRequest = HttpContext.Current.Request;
            try
            {
                var empobj = new Models.Bussiness.Employee
                {
                    Id = Convert.ToInt32(httpRequest.Form.Get("Id")),
                    EmployeeName = httpRequest.Form.Get("EmployeeName"),
                    Department = httpRequest.Form.Get("Department"),
                    Designation = httpRequest.Form.Get("Designation"),
                    EmailId = httpRequest.Form.Get("EmailId"),
                    DOJ = httpRequest.Form.Get("DOJ"),
                    PanNo = httpRequest.Form.Get("PanNo"),
                    Passport = httpRequest.Form.Get("Passport"),
                    Mobile = httpRequest.Form.Get("Mobile"),
                    UserName = httpRequest.Form.Get("UserName"),
                    Password = httpRequest.Form.Get("Password"),
                    BankName = httpRequest.Form.Get("BankName"),
                    AccountNo = httpRequest.Form.Get("AccountNo"),
                    IFSC = httpRequest.Form.Get("IFSC"),
                    BankBranch = httpRequest.Form.Get("BankBranch"),
                    BloodGroup = httpRequest.Form.Get("BloodGroup"),
                    IdMarks = httpRequest.Form.Get("IdMarks"),
                    DOB = httpRequest.Form.Get("DOB"),
                    Age = httpRequest.Form.Get("Age"),
                    Sex = httpRequest.Form.Get("Sex"),
                    Nationality = httpRequest.Form.Get("Nationality"),
                    Religion = httpRequest.Form.Get("Religion"),
                    MaritalStatus = httpRequest.Form.Get("MaritalStatus"),
                    PresentAddress = httpRequest.Form.Get("PresentAddress"),
                    State = httpRequest.Form.Get("State"),
                    City = httpRequest.Form.Get("City"),
                    Pin = httpRequest.Form.Get("Pin"),
                    EmergencyContactPerson = httpRequest.Form.Get("EmergencyContactPerson"),
                    EmergencyContactNumber = httpRequest.Form.Get("EmergencyContactNumber"),
                    EmergencyContectAddress = httpRequest.Form.Get("EmergencyContectAddress"),
                    ReferencesName = httpRequest.Form.Get("ReferencesName"),
                    ContactNo = httpRequest.Form.Get("ContactNo"),
                    CompanyName = httpRequest.Form.Get("CompanyName"),
                    CemailId = httpRequest.Form.Get("CemailId"),
                    PreviousEmployer = httpRequest.Form.Get("PreviousEmployer"),
                    From = httpRequest.Form.Get("FromT"),
                    To = httpRequest.Form.Get("ToT"),
                    Degree = httpRequest.Form.Get("Degree"),
                    ProfessionalInstitution = httpRequest.Form.Get("ProfessionalInstitution"),
                    ProfessionalPassingYear = httpRequest.Form.Get("ProfessionalPassingYear"),
                    ProfessionalSpecilization = httpRequest.Form.Get("ProfessionalSpecilization"),
                    Board10 = httpRequest.Form.Get("Board10"),
                    Institution10 = httpRequest.Form.Get("Institution10"),
                    PassingYear10 = httpRequest.Form.Get("PassingYear10"),
                    Specilization10 = httpRequest.Form.Get("Specilization10"),
                    Board12 = httpRequest.Form.Get("Board12"),
                    Institution12 = httpRequest.Form.Get("Institution12"),
                    PassingYear12 = httpRequest.Form.Get("PassingYear12"),
                    Specilization12 = httpRequest.Form.Get("Specilization12"),
                    adharno = httpRequest.Form.Get("AdharNo"),
                    ReportingManager = httpRequest.Form.Get("ReportingManager")
                };
                //add other form fields as needed
                if (httpRequest.Files.Count > 0)
                {
                    var PostedFile = httpRequest.Files[0];
                    string FilePath = Path.Combine(HttpContext.Current.Server.MapPath("/Upload/"), PostedFile.FileName);
                    PostedFile.SaveAs(FilePath);
                    empobj.ImagePath = "/Upload/" + PostedFile.FileName;//save the filepath in the database
                }
                SqlCommand cmd = new SqlCommand("[Sp_Employee]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "Update");
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@EmployeeCode", empobj.EmployeeCode);
                cmd.Parameters.AddWithValue("@EmployeeName", empobj.EmployeeName);
                cmd.Parameters.AddWithValue("@Department", empobj.Department);
                cmd.Parameters.AddWithValue("@Designation", empobj.Designation);
                cmd.Parameters.AddWithValue("@EmailId", empobj.EmailId);
                cmd.Parameters.AddWithValue("@DOJ", empobj.DOJ);
                cmd.Parameters.AddWithValue("@PanNo", empobj.PanNo);
                cmd.Parameters.AddWithValue("@AdharNo", empobj.adharno);
                cmd.Parameters.AddWithValue("@Passport", empobj.Passport);
                cmd.Parameters.AddWithValue("@Mobile", empobj.Mobile);
                cmd.Parameters.AddWithValue("@UserName", empobj.UserName);
                cmd.Parameters.AddWithValue("@Password", empobj.Password);
                cmd.Parameters.AddWithValue("@BankName", empobj.BankName);
                cmd.Parameters.AddWithValue("@AccountNo", empobj.AccountNo);
                cmd.Parameters.AddWithValue("@IFSC", empobj.IFSC);
                cmd.Parameters.AddWithValue("@BankBranch", empobj.BankBranch);
                cmd.Parameters.AddWithValue("@BloodGroup", empobj.BloodGroup);
                cmd.Parameters.AddWithValue("@IdMarks", empobj.IdMarks);
                cmd.Parameters.AddWithValue("@DOB", empobj.DOB);
                cmd.Parameters.AddWithValue("@Age", empobj.Age);
                cmd.Parameters.AddWithValue("@Sex", empobj.Sex);
                cmd.Parameters.AddWithValue("@Nationality", empobj.Nationality);
                cmd.Parameters.AddWithValue("@Religion", empobj.Religion);
                cmd.Parameters.AddWithValue("@MaritalStatus", empobj.MaritalStatus);
                cmd.Parameters.AddWithValue("@PresentAddress", empobj.PresentAddress);
                cmd.Parameters.AddWithValue("@State", empobj.State);
                cmd.Parameters.AddWithValue("@City", empobj.City);
                cmd.Parameters.AddWithValue("@Pin", empobj.Pin);
                cmd.Parameters.AddWithValue("@EmergencyContactPerson", empobj.EmergencyContactPerson);
                cmd.Parameters.AddWithValue("@EmergencyContactNumber", empobj.EmergencyContactNumber);
                cmd.Parameters.AddWithValue("@EmergencyContectAddress", empobj.EmergencyContectAddress);
                cmd.Parameters.AddWithValue("@ReferencesName", empobj.ReferencesName);
                cmd.Parameters.AddWithValue("@ContactNo", empobj.ContactNo);
                cmd.Parameters.AddWithValue("@CompanyName", empobj.CompanyName);
                cmd.Parameters.AddWithValue("@CemailId", empobj.CemailId);
                cmd.Parameters.AddWithValue("@PreviousEmployer", empobj.PreviousEmployer);
                cmd.Parameters.AddWithValue("@From", empobj.From);
                cmd.Parameters.AddWithValue("@To", empobj.To);
                cmd.Parameters.AddWithValue("@Degree", empobj.Degree);
                cmd.Parameters.AddWithValue("@ProfessionalInstitution", empobj.ProfessionalInstitution);
                cmd.Parameters.AddWithValue("@ProfessionalPassingYear", empobj.ProfessionalPassingYear);
                cmd.Parameters.AddWithValue("@ProfessionalSpecilization", empobj.ProfessionalSpecilization);
                cmd.Parameters.AddWithValue("@Board10", empobj.Board10);
                cmd.Parameters.AddWithValue("@Institution10", empobj.Institution10);
                cmd.Parameters.AddWithValue("@PassingYear10", empobj.PassingYear10);
                cmd.Parameters.AddWithValue("@Specilization10", empobj.Specilization10);
                cmd.Parameters.AddWithValue("@Board12", empobj.Board12);
                cmd.Parameters.AddWithValue("@Institution12", empobj.Institution12);
                cmd.Parameters.AddWithValue("@PassingYear12", empobj.PassingYear12);
                cmd.Parameters.AddWithValue("@Specilization12", empobj.Specilization12);
                cmd.Parameters.AddWithValue("@Image", empobj.ImagePath);
                cmd.Parameters.AddWithValue("@UserId", empobj.UserId);
                cmd.Parameters.AddWithValue("@StateId", empobj.StateId);
                cmd.Parameters.AddWithValue("@ReportingManager", empobj.ReportingManager);
                if (con.State == ConnectionState.Closed)
                    con.Open();
                int i = cmd.ExecuteNonQuery();
                con.Close();
                return Ok("Updated Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest("Error Update Data:" + ex.Message);
            }
        }
    }
}
