using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using static Macreel_Project.Models.Bussiness;
using System.Web.Mvc;
using System.Configuration;
using System.Net.Mail;
using System.Net;
using System.Globalization;
using System.Text;
using System.Data.SqlTypes;

namespace Macreel_Project.Models
{
    public class DataAccess : DataFactory
    {
        public SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["myconn"].ConnectionString);

        public object Session { get; private set; }

        #region
        public Login CheckLogin(string UserName = "", string Password = "")
        {
            Login obj = new Login();
            try
            {
                cmd = new SqlCommand("Sp_Login", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "Login");
                cmd.Parameters.AddWithValue("@UserName", UserName);
                cmd.Parameters.AddWithValue("@Password", Password);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        obj = new Login();
                        if (rd["EmpId"] != DBNull.Value)
                        {
                            obj.EmpId = Convert.ToInt32(rd["EmpId"]);
                        }
                        if (rd["UserName"] != DBNull.Value)
                        {
                            obj.UserName = rd["UserName"].ToString();
                        }
                        if (rd["Password"] != DBNull.Value)
                        {
                            obj.Password = rd["Password"].ToString();
                        }
                        if (rd["Type"] != DBNull.Value)
                        {
                            obj.Type = rd["Type"].ToString();
                        }
                        if (rd["Image"] != DBNull.Value)
                        {
                            obj.Image = rd["Image"].ToString();
                        }
                        if (rd["EmployeeName"] != DBNull.Value)
                        {
                            obj.EmployeeName = rd["EmployeeName"].ToString();
                        }
                        if (rd["CompanyName"] != DBNull.Value)
                        {
                            obj.CompanyName = rd["CompanyName"].ToString();
                        }
                        //if (rd["ClientId"] != DBNull.Value)
                        //{
                        //    obj.ClientId = Convert.ToInt32(rd["ClientId"]);
                        //}

                        if (rd["Department"] != DBNull.Value)
                        {
                            obj.Department = rd["Department"].ToString();
                        }

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
            return obj;
        }
        public int InsertDepartment(Deprtment obj)
        {
            int row = 0;
            try
            {
                cmd = new SqlCommand("Sp_DeptAndDesignation", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "InsertDepartment");
                con.Open();
                cmd.Parameters.AddWithValue("@Name", obj.DepartmentName);
                row = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return row;
        }
        public int UpdateDepartment(Deprtment obj)
        {
            int row = 0;
            try
            {
                cmd = new SqlCommand("Sp_DeptAndDesignation", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "UpdateDepartment");
                con.Open();
                cmd.Parameters.AddWithValue("@Name", obj.DepartmentName);
                cmd.Parameters.AddWithValue("@Id", obj.DeptId);
                row = cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return row;
        }
        public List<Deprtment> BindDepartment()
        {
            List<Deprtment> list = new List<Deprtment>();
            Deprtment obj = new Deprtment();
            try
            {
                cmd = new SqlCommand("Sp_DeptAndDesignation", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "BindDepartment");
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        obj = new Deprtment();
                        obj.DeptId = Convert.ToInt32(rd["Id"]);
                        obj.DepartmentName = rd["Name"].ToString();
                        obj.Type = "Dept";
                        list.Add(obj);
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
            return list;
        }
        //Bind Reporting Manager
        public List<Login> BindReportingManager()
        {
            List<Login> list = new List<Login>();
            Login obj = new Login();
            try
            {
                cmd = new SqlCommand("Sp_Login", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "BindUserName");
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.HasRows)
                    {
                        obj = new Login();
                        obj.UserName = rd["UserName"].ToString();
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
            return list;
        }
        public int InsertDesignation(designation obj)
        {
            int row = 0;
            try
            {
                cmd = new SqlCommand("Sp_DeptAndDesignation", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "InsertDesignation");
                con.Open();
                cmd.Parameters.AddWithValue("@Name", obj.Name);
                row = cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return row;
        }
        public int UpdateDesignation(designation obj)
        {
            int row = 0;
            try
            {
                cmd = new SqlCommand("Sp_DeptAndDesignation", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "UpdateDesignation");
                con.Open();
                cmd.Parameters.AddWithValue("@Name", obj.Name);
                cmd.Parameters.AddWithValue("@Id", obj.DId);
                row = cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return row;
        }
        public List<designation> BindDesignation()
        {
            List<designation> list = new List<designation>();
            designation obj = new designation();
            try
            {
                cmd = new SqlCommand("Sp_DeptAndDesignation", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "BindDesignation");
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        obj = new designation();
                        obj.DId = Convert.ToInt32(rd["Id"]);
                        obj.Name = rd["Name"].ToString();
                        obj.Type = "Desig";
                        list.Add(obj);
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
            return list;
        }
        public int DeleteDepartment(int Id = 0)
        {
            int row = 0;
            try
            {
                cmd = new SqlCommand("Sp_DeptAndDesignation", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "DeleteDepartment");
                con.Open();
                cmd.Parameters.AddWithValue("@Id", Id);
                row = cmd.ExecuteNonQuery();
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
            return row;
        }
        public int DeleteDesignation(int Id = 0)
        {
            int row = 0;
            try
            {
                cmd = new SqlCommand("Sp_DeptAndDesignation", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "DeleteDesignation");
                cmd.Parameters.AddWithValue("@Id", Id);
                con.Open();
                row = cmd.ExecuteNonQuery();
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
            return row;
        }
        public Deprtment EditDepartment(int Id = 0)
        {
            Deprtment obj = new Deprtment();
            try
            {
                cmd = new SqlCommand("Sp_DeptAndDesignation", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "EditDepartment");
                cmd.Parameters.AddWithValue("@Id", Id);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        obj = new Deprtment();
                        obj.DeptId = Convert.ToInt32(rd["Id"]);
                        obj.DepartmentName = rd["Name"].ToString();
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
            return obj;
        }
        public Deprtment EditDesignation(int Id = 0)
        {
            Deprtment obj = new Deprtment();
            try
            {
                cmd = new SqlCommand("Sp_DeptAndDesignation", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "EditDesignation");
                cmd.Parameters.AddWithValue("@Id", Id);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        obj = new Deprtment();
                        obj.DesId = Convert.ToInt32(rd["Id"]);
                        obj.DesignationName = rd["Name"].ToString();
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
            return obj;
        }
        public List<SelectListItem> GetDepartment()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            SelectListItem select = new SelectListItem();
            try
            {
                cmd = new SqlCommand("Sp_Employee", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "BindDepartment");
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        select = new SelectListItem();
                        select.Value = rd["Id"].ToString();
                        select.Text = rd["Name"].ToString();
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
            return list;
        }

        //get LoginData
        public List<Login> LoginData()
        {
            List<Login> list = new List<Login>();
            Login obj = new Login();
            try
            {
                cmd = new SqlCommand("Sp_Login", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "BindUserName");
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        obj = new Login();
                        obj.EmpId = Convert.ToInt32(rd["Id"].ToString());
                        obj.UserName = rd["UserName"].ToString();
                        list.Add(obj);
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
            return list;
        }
        public List<SelectListItem> GetDesgnation()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            SelectListItem select = new SelectListItem();
            try
            {
                cmd = new SqlCommand("Sp_Employee", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "BindDesignation");
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        select = new SelectListItem();
                        select.Value = rd["Id"].ToString();
                        select.Text = rd["Name"].ToString();
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
            return list;
        }
        public List<SelectListItem> GetState()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            SelectListItem select = new SelectListItem();
            try
            {
                cmd = new SqlCommand("Sp_Employee", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "State");
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        select = new SelectListItem();
                        select.Value = rd["ID"].ToString();
                        select.Text = rd["Name"].ToString();
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
            return list;
        }
        public List<SelectListItem> GetCity(string StateId = "")
        {
            List<SelectListItem> list = new List<SelectListItem>();
            SelectListItem select = new SelectListItem();
            try
            {
                cmd = new SqlCommand("Sp_Employee", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "City");
                cmd.Parameters.AddWithValue("@StateId", StateId);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        select = new SelectListItem();
                        select.Value = rd["ID"].ToString();
                        select.Text = rd["Name"].ToString();
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
            return list;
        }
        public int InsertEmployee(Employee obj)
        {
            int row = 0;
            try
            {
                cmd = new SqlCommand("Sp_Employee", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "Insert");
                con.Open();
                cmd.Parameters.AddWithValue("@Id", obj.Id);
                cmd.Parameters.AddWithValue("@EmployeeCode", obj.EmployeeCode);
                cmd.Parameters.AddWithValue("@EmployeeName", obj.EmployeeName);
                cmd.Parameters.AddWithValue("@Department", obj.Department);
                cmd.Parameters.AddWithValue("@Designation", obj.Designation);
                cmd.Parameters.AddWithValue("@EmailId", obj.EmailId);
                cmd.Parameters.AddWithValue("@DOJ", obj.DOJ);
                cmd.Parameters.AddWithValue("@PanNo", obj.PanNo);
                cmd.Parameters.AddWithValue("@AdharNo", obj.adharno);
                cmd.Parameters.AddWithValue("@Passport", obj.Passport);
                cmd.Parameters.AddWithValue("@Mobile", obj.Mobile);
                cmd.Parameters.AddWithValue("@UserName", obj.UserName);
                cmd.Parameters.AddWithValue("@Password", obj.Password);
                cmd.Parameters.AddWithValue("@BankName", obj.BankName);
                cmd.Parameters.AddWithValue("@AccountNo", obj.AccountNo);
                cmd.Parameters.AddWithValue("@IFSC", obj.IFSC);
                cmd.Parameters.AddWithValue("@BankBranch", obj.BankBranch);
                cmd.Parameters.AddWithValue("@BloodGroup", obj.BloodGroup);
                cmd.Parameters.AddWithValue("@IdMarks", obj.IdMarks);
                cmd.Parameters.AddWithValue("@DOB", obj.DOB);
                cmd.Parameters.AddWithValue("@Age", obj.Age);
                cmd.Parameters.AddWithValue("@Sex", obj.Sex);
                cmd.Parameters.AddWithValue("@Nationality", obj.Nationality);
                cmd.Parameters.AddWithValue("@Religion", obj.Religion);
                cmd.Parameters.AddWithValue("@MaritalStatus", obj.MaritalStatus);
                cmd.Parameters.AddWithValue("@PresentAddress", obj.PresentAddress);
                cmd.Parameters.AddWithValue("@State", obj.State);
                cmd.Parameters.AddWithValue("@City", obj.City);
                cmd.Parameters.AddWithValue("@Pin", obj.Pin);
                cmd.Parameters.AddWithValue("@EmergencyContactPerson", obj.EmergencyContactPerson);
                cmd.Parameters.AddWithValue("@EmergencyContactNumber", obj.EmergencyContactNumber);
                cmd.Parameters.AddWithValue("@EmergencyContectAddress", obj.EmergencyContectAddress);
                cmd.Parameters.AddWithValue("@ReferencesName", obj.ReferencesName);
                cmd.Parameters.AddWithValue("@ContactNo", obj.ContactNo);
                cmd.Parameters.AddWithValue("@CompanyName", obj.CompanyName);
                cmd.Parameters.AddWithValue("@CemailId", obj.CemailId);
                cmd.Parameters.AddWithValue("@PreviousEmployer", obj.PreviousEmployer);
                cmd.Parameters.AddWithValue("@From", obj.From);
                cmd.Parameters.AddWithValue("@To", obj.To);
                cmd.Parameters.AddWithValue("@Degree", obj.Degree);
                cmd.Parameters.AddWithValue("@ProfessionalInstitution", obj.ProfessionalInstitution);
                cmd.Parameters.AddWithValue("@ProfessionalPassingYear", obj.ProfessionalPassingYear);
                cmd.Parameters.AddWithValue("@ProfessionalSpecilization", obj.ProfessionalSpecilization);
                cmd.Parameters.AddWithValue("@Board10", obj.Board10);
                cmd.Parameters.AddWithValue("@Institution10", obj.Institution10);
                cmd.Parameters.AddWithValue("@PassingYear10", obj.PassingYear10);
                cmd.Parameters.AddWithValue("@Specilization10", obj.Specilization10);
                cmd.Parameters.AddWithValue("@Board12", obj.Board12);
                cmd.Parameters.AddWithValue("@Institution12", obj.Institution12);
                cmd.Parameters.AddWithValue("@PassingYear12", obj.PassingYear12);
                cmd.Parameters.AddWithValue("@Specilization12", obj.Specilization12);
                cmd.Parameters.AddWithValue("@Image", obj.ImagePath);
                cmd.Parameters.AddWithValue("@UserId", obj.UserId);
                cmd.Parameters.AddWithValue("@StateId", obj.StateId);
                cmd.Parameters.AddWithValue("@ReportingManager", obj.ReportingManager);
                row = cmd.ExecuteNonQuery();
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
            return row;
        }
        public int UpdateEmployee(Employee obj)
        {
            int row = 0;
            try
            {
                cmd = new SqlCommand("Sp_Employee", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "Update");
                con.Open();
                cmd.Parameters.AddWithValue("@Id", obj.Id);
                cmd.Parameters.AddWithValue("@EmployeeCode", obj.EmployeeCode);
                cmd.Parameters.AddWithValue("@EmployeeName", obj.EmployeeName);
                cmd.Parameters.AddWithValue("@Department", obj.Department);
                cmd.Parameters.AddWithValue("@Designation", obj.Designation);
                cmd.Parameters.AddWithValue("@EmailId", obj.EmailId);
                cmd.Parameters.AddWithValue("@DOJ", obj.DOJ);
                cmd.Parameters.AddWithValue("@PanNo", obj.PanNo);
                cmd.Parameters.AddWithValue("@Passport", obj.Passport);
                cmd.Parameters.AddWithValue("@Mobile", obj.Mobile);
                cmd.Parameters.AddWithValue("@UserName", obj.UserName);
                cmd.Parameters.AddWithValue("@Password", obj.Password);
                cmd.Parameters.AddWithValue("@BankName", obj.BankName);
                cmd.Parameters.AddWithValue("@AccountNo", obj.AccountNo);
                cmd.Parameters.AddWithValue("@IFSC", obj.IFSC);
                cmd.Parameters.AddWithValue("@BankBranch", obj.BankBranch);
                cmd.Parameters.AddWithValue("@BloodGroup", obj.BloodGroup);
                cmd.Parameters.AddWithValue("@IdMarks", obj.IdMarks);
                cmd.Parameters.AddWithValue("@DOB", obj.DOB);
                cmd.Parameters.AddWithValue("@Age", obj.Age);
                cmd.Parameters.AddWithValue("@Sex", obj.Sex);
                cmd.Parameters.AddWithValue("@Nationality", obj.Nationality);
                cmd.Parameters.AddWithValue("@Religion", obj.Religion);
                cmd.Parameters.AddWithValue("@MaritalStatus", obj.MaritalStatus);
                cmd.Parameters.AddWithValue("@PresentAddress", obj.PresentAddress);
                cmd.Parameters.AddWithValue("@State", obj.State);
                cmd.Parameters.AddWithValue("@City", obj.City);
                cmd.Parameters.AddWithValue("@Pin", obj.Pin);
                cmd.Parameters.AddWithValue("@EmergencyContactPerson", obj.EmergencyContactPerson);
                cmd.Parameters.AddWithValue("@EmergencyContactNumber", obj.EmergencyContactNumber);
                cmd.Parameters.AddWithValue("@EmergencyContectAddress", obj.EmergencyContectAddress);
                cmd.Parameters.AddWithValue("@ReferencesName", obj.ReferencesName);
                cmd.Parameters.AddWithValue("@ContactNo", obj.ContactNo);
                cmd.Parameters.AddWithValue("@CompanyName", obj.CompanyName);
                cmd.Parameters.AddWithValue("@CemailId", obj.CemailId);
                cmd.Parameters.AddWithValue("@PreviousEmployer", obj.PreviousEmployer);
                cmd.Parameters.AddWithValue("@From", obj.From);
                cmd.Parameters.AddWithValue("@To", obj.To);
                cmd.Parameters.AddWithValue("@Degree", obj.Degree);
                cmd.Parameters.AddWithValue("@ProfessionalInstitution", obj.ProfessionalInstitution);
                cmd.Parameters.AddWithValue("@ProfessionalPassingYear", obj.ProfessionalPassingYear);
                cmd.Parameters.AddWithValue("@ProfessionalSpecilization", obj.ProfessionalSpecilization);
                cmd.Parameters.AddWithValue("@Board10", obj.Board10);
                cmd.Parameters.AddWithValue("@Institution10", obj.Institution10);
                cmd.Parameters.AddWithValue("@PassingYear10", obj.PassingYear10);
                cmd.Parameters.AddWithValue("@Specilization10", obj.Specilization10);
                cmd.Parameters.AddWithValue("@Board12", obj.Board12);
                cmd.Parameters.AddWithValue("@Institution12", obj.Institution12);
                cmd.Parameters.AddWithValue("@PassingYear12", obj.PassingYear12);
                cmd.Parameters.AddWithValue("@Specilization12", obj.Specilization12);
                cmd.Parameters.AddWithValue("@Image", obj.ImagePath);
                cmd.Parameters.AddWithValue("@StateId", obj.StateId);
                cmd.Parameters.AddWithValue("@ReportingManager", obj.ReportingManager);

                row = cmd.ExecuteNonQuery();
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
            return row;
        }
        public string CheckUserId(string UserName = "")
        {
            string UserId = "";
            try
            {
                cmd = new SqlCommand("Sp_Employee", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "CheckUserName");
                cmd.Parameters.AddWithValue("@UserName", UserName);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        UserId = rd["UserName"].ToString();
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
            return UserId;
        }
        public List<Employee> GetEmployyeList()
        {
            List<Employee> getlist = new List<Employee>();
            Employee obj = new Employee();
            try
            {
                cmd = new SqlCommand("Sp_Employee", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "BindEmp");
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        obj = new Employee();
                        obj.Id = Convert.ToInt32(rd["Id"]);
                        obj.EmployeeCode = rd["EmployeeCode"].ToString();
                        obj.EmployeeName = rd["EmployeeName"].ToString();
                        obj.Designation = rd["Designation"].ToString();
                        obj.EmailId = rd["EmailId"].ToString();
                        obj.ContactNo = rd["Mobile"].ToString();
                        obj.ImagePath = rd["Image"].ToString();
                        getlist.Add(obj);
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
            return getlist;
        }
        public List<Employee> GetEmployyeListByRe()
        {
            var userid = ((Login)HttpContext.Current.Session["Login"]).EmpId;
            List<Employee> getlist = new List<Employee>();
            Employee obj = new Employee();
            try
            {
                cmd = new SqlCommand("sp_TaskManage", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "show_empList");
                cmd.Parameters.AddWithValue("@id", userid);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        obj = new Employee();
                        obj.Id = Convert.ToInt32(rd["Id"]);
                        obj.EmployeeCode = rd["EmployeeCode"].ToString();
                        obj.EmployeeName = rd["EmployeeName"].ToString();
                        obj.Designation = rd["Designation"].ToString();
                        obj.EmailId = rd["EmailId"].ToString();
                        obj.ContactNo = rd["Mobile"].ToString();
                        obj.ImagePath = rd["Image"].ToString();
                        getlist.Add(obj);
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
            return getlist;
        }
        public int DeleteEmployee(int Id = 0)
        {
            int row = 0;
            try
            {
                cmd = new SqlCommand("Sp_Employee", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "DeleteEmp");
                con.Open();
                cmd.Parameters.AddWithValue("@Id", Id);
                row = cmd.ExecuteNonQuery();
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
            return row;
        }
        public Employee GetEmployeeProfile(int Id = 0)
        {
            Employee obj = new Employee();
            try
            {
                cmd = new SqlCommand("Sp_Employee", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "EmpProfile");
                cmd.Parameters.AddWithValue("@Id", Id);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        obj = new Employee();
                        obj.Id = Convert.ToInt32(rd["Id"]);
                        obj.EmployeeCode = rd["EmployeeCode"].ToString();
                        obj.EmployeeName = rd["EmployeeName"].ToString();
                        obj.Department = rd["Department"].ToString();
                        obj.Designation = rd["Designation"].ToString();
                        obj.ReportingManager = rd[0].ToString();
                        obj.EmailId = rd["EmailId"].ToString();
                        obj.DOJ = rd["DOJ"].ToString();
                        obj.PanNo = rd["PanNo"].ToString();
                        obj.adharno = rd["adharno"].ToString();
                        obj.Passport = rd["Passport"].ToString();
                        obj.Mobile = rd["Mobile"].ToString();
                        obj.UserName = rd[11].ToString();
                        obj.Password = rd["Password"].ToString();
                        obj.BankName = rd["BankName"].ToString();
                        obj.AccountNo = rd["AccountNo"].ToString();
                        obj.IFSC = rd["IFSC"].ToString();
                        obj.BankBranch = rd["BankBranch"].ToString();
                        obj.BloodGroup = rd["BloodGroup"].ToString();
                        obj.IdMarks = rd["IdMarks"].ToString();
                        obj.DOB = rd["DOB"].ToString();
                        obj.Age = rd["Age"].ToString();
                        obj.Sex = rd["Sex"].ToString();
                        obj.Nationality = rd["Nationality"].ToString();
                        obj.Religion = rd["Religion"].ToString();
                        obj.MaritalStatus = rd["MaritalStatus"].ToString();
                        obj.PresentAddress = rd["PresentAddress"].ToString();
                        obj.State = rd["State"].ToString();
                        obj.City = rd["City"].ToString();
                        obj.Pin = rd["Pin"].ToString();
                        obj.EmergencyContactPerson = rd["EmergencyContactPerson"].ToString();
                        obj.EmergencyContactNumber = rd["EmergencyContactNumber"].ToString();
                        obj.EmergencyContectAddress = rd["EmergencyContectAddress"].ToString();
                        obj.ReferencesName = rd["ReferencesName"].ToString();
                        obj.ContactNo = rd["ContactNo"].ToString();
                        obj.CompanyName = rd["CompanyName"].ToString();
                        obj.CemailId = rd["CemailId"].ToString();
                        obj.PreviousEmployer = rd["PreviousEmployer"].ToString();
                        obj.From = rd["FromT"].ToString();
                        obj.To = rd["ToT"].ToString();
                        obj.Degree = rd["Degree"].ToString();
                        obj.ProfessionalInstitution = rd["ProfessionalInstitution"].ToString();
                        obj.ProfessionalPassingYear = rd["ProfessionalPassingYear"].ToString();
                        obj.ProfessionalSpecilization = rd["ProfessionalSpecilization"].ToString();
                        obj.Board10 = rd["Board10"].ToString();
                        obj.Institution10 = rd["Institution10"].ToString();
                        obj.PassingYear10 = rd["PassingYear10"].ToString();
                        obj.Specilization10 = rd["Specilization10"].ToString();
                        obj.Board12 = rd["Board12"].ToString();
                        obj.Institution12 = rd["Institution12"].ToString();
                        obj.PassingYear12 = rd["PassingYear12"].ToString();
                        obj.Specilization12 = rd["Specilization12"].ToString();
                        obj.ImagePath = rd["Image"].ToString();
                        obj.StateId = rd["StateId"].ToString();
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
            return obj;
        }
        public int InsertClient(Client obj)
        {
            int row = 0;
            try
            {
                cmd = new SqlCommand("Sp_Client", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "Insert");
                con.Open();
                cmd.Parameters.AddWithValue("@CompanyName", obj.CompanyName);
                cmd.Parameters.AddWithValue("@ContactPerson", obj.ContactPerson);
                cmd.Parameters.AddWithValue("@ContactNo", obj.ContactNo);
                cmd.Parameters.AddWithValue("@GSTNo", obj.GSTNo);
                cmd.Parameters.AddWithValue("@State", obj.State);
                cmd.Parameters.AddWithValue("@City", obj.City);
                cmd.Parameters.AddWithValue("@StateName", obj.StateName);
                cmd.Parameters.AddWithValue("@CityName", obj.CityName);
                cmd.Parameters.AddWithValue("@Address", obj.Address);
                cmd.Parameters.AddWithValue("@Statecode", obj.Statecode);
                cmd.Parameters.AddWithValue("@Pincode", obj.Pincode);
                cmd.Parameters.AddWithValue("@PanNo", obj.PanNo);
                cmd.Parameters.AddWithValue("@EmailId", obj.EmailId);
                cmd.Parameters.AddWithValue("@Designation", obj.Designation);
                row = cmd.ExecuteNonQuery();
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
            return row;
        }
        public int UpdateClient(Client obj)
        {
            int row = 0;
            try
            {
                cmd = new SqlCommand("Sp_Client", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "Update");
                con.Open();
                cmd.Parameters.AddWithValue("@Id", obj.Id);
                cmd.Parameters.AddWithValue("@CompanyName", obj.CompanyName);
                cmd.Parameters.AddWithValue("@ContactPerson", obj.ContactPerson);
                cmd.Parameters.AddWithValue("@ContactNo", obj.ContactNo);
                cmd.Parameters.AddWithValue("@GSTNo", obj.GSTNo);
                cmd.Parameters.AddWithValue("@State", obj.State);
                cmd.Parameters.AddWithValue("@City", obj.City);
                cmd.Parameters.AddWithValue("@StateName", obj.StateName);
                cmd.Parameters.AddWithValue("@CityName", obj.CityName);
                cmd.Parameters.AddWithValue("@Address", obj.Address);
                cmd.Parameters.AddWithValue("@Statecode", obj.Statecode);
                cmd.Parameters.AddWithValue("@Pincode", obj.Pincode);
                cmd.Parameters.AddWithValue("@PanNo", obj.PanNo);
                cmd.Parameters.AddWithValue("@EmailId", obj.EmailId);
                cmd.Parameters.AddWithValue("@Designation", obj.Designation);
                row = cmd.ExecuteNonQuery();
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
            return row;
        }
        public int DeleteClient(int Id = 0)
        {
            int row = 0;
            try
            {
                cmd = new SqlCommand("Sp_Client", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "Delete");
                con.Open();
                cmd.Parameters.AddWithValue("@Id", Id);
                row = cmd.ExecuteNonQuery();
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
            return row;
        }
        public List<Client> BindClient()
        {
            List<Client> gtlst = new List<Client>();
            Client obj = new Client();
            try
            {
                cmd = new SqlCommand("Sp_Client", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "Bind");
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        obj = new Client();
                        obj.Id = Convert.ToInt32(rd["Id"]);
                        obj.CompanyName = rd["CompanyName"].ToString();
                        obj.ContactPerson = rd["ContactPerson"].ToString();
                        obj.ContactNo = rd["ContactNo"].ToString();
                        obj.EmailId = rd["EmailId"].ToString();
                        obj.CityName = rd["CityName"].ToString();
                        gtlst.Add(obj);
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
            return gtlst;
        }
        public Client EditClient(int Id = 0)
        {
            Client obj = new Client();
            try
            {
                cmd = new SqlCommand("Sp_Client", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "Edit");
                cmd.Parameters.AddWithValue("@Id", Id);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        obj = new Client();
                        obj.Id = Convert.ToInt32(rd["Id"]);
                        obj.CompanyName = rd["CompanyName"].ToString();
                        obj.ContactPerson = rd["ContactPerson"].ToString();
                        obj.ContactNo = rd["ContactNo"].ToString();
                        obj.GSTNo = rd["GSTNo"].ToString();
                        obj.State = rd["State"].ToString();
                        obj.City = rd["City"].ToString();
                        obj.StateName = rd["StateName"].ToString();
                        obj.CityName = rd["CityName"].ToString();
                        obj.Address = rd["Address"].ToString();
                        obj.Statecode = rd["Statecode"].ToString();
                        obj.Pincode = rd["Pincode"].ToString();
                        obj.PanNo = rd["PanNo"].ToString();
                        obj.EmailId = rd["EmailId"].ToString();
                        obj.Designation = rd["Designation"].ToString();
                        //obj.UserName = rd["UserId"].ToString();
                        //obj.Password = rd["Password"].ToString();
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
            return obj;
        }
        public List<SelectListItem> BindClientForProject()
        {
            List<SelectListItem> gtlst = new List<SelectListItem>();
            SelectListItem obj = new SelectListItem();
            try
            {
                cmd = new SqlCommand("Sp_Client", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "Bind");
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        obj = new SelectListItem();
                        obj.Value = (rd["Id"]).ToString();
                        obj.Text = rd["CompanyName"].ToString();
                        gtlst.Add(obj);
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
            return gtlst;
        }
        public List<SelectListItem> BindEmployyeForProject()
        {
            List<SelectListItem> getlist = new List<SelectListItem>();
            SelectListItem obj = new SelectListItem();
            try
            {
                cmd = new SqlCommand("Sp_Employee", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "BindEmp");
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        obj = new SelectListItem();
                        obj.Value = rd["Id"].ToString();
                        obj.Text = rd["EmployeeName"].ToString();
                        getlist.Add(obj);
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
            return getlist;
        }
        public Client GetClientDetails(string ClientId = "")
        {
            Client obj = new Client();
            try
            {
                cmd = new SqlCommand("Sp_Project", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "BindClient");
                cmd.Parameters.AddWithValue("@ClientId", ClientId);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        obj = new Client();
                        obj.Id = Convert.ToInt32(rd["Id"]);
                        obj.CompanyName = rd["CompanyName"].ToString();
                        obj.ContactPerson = rd["ContactPerson"].ToString();
                        obj.ContactNo = rd["ContactNo"].ToString();
                        obj.EmailId = rd["EmailId"].ToString();
                        obj.StateName = rd["StateName"].ToString();
                        obj.CityName = rd["CityName"].ToString();
                        obj.GSTNo = rd["GSTNo"].ToString();

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
            return obj;
        }
        public int InsertProject(Project obj)
        {
            int row = 0;
            try
            {
                cmd = new SqlCommand("Sp_Project", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "Insert");
                con.Open();
                cmd.Parameters.AddWithValue("@CompanyId", obj.CompanyId);
                cmd.Parameters.AddWithValue("@CompanyName", obj.CompanyName);
                cmd.Parameters.AddWithValue("@ClientName", obj.ClientName);
                cmd.Parameters.AddWithValue("@ProjectName", obj.ProjectName);
                cmd.Parameters.AddWithValue("@ProjectCode", obj.ProjectCode);
                cmd.Parameters.AddWithValue("@ProjectStartingDate", obj.ProjectStartingDate);
                cmd.Parameters.AddWithValue("@CompletionDate", obj.CompletionDate);
                cmd.Parameters.AddWithValue("@ProjectDeliveryDate", obj.ProjectDeliveryDate);
                cmd.Parameters.AddWithValue("@TotalAmount", obj.TotalAmount);
                cmd.Parameters.AddWithValue("@ProjectStatus", obj.ProjectStatus);
                row = cmd.ExecuteNonQuery();
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
            return row;
        }
        public int UpdateProject(Project obj)
        {
            int row = 0;
            try
            {
                cmd = new SqlCommand("Sp_Project", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "Update");
                con.Open();
                cmd.Parameters.AddWithValue("@Id", obj.Id);
                cmd.Parameters.AddWithValue("@CompanyId", obj.CompanyId);
                cmd.Parameters.AddWithValue("@CompanyName", obj.CompanyName);
                cmd.Parameters.AddWithValue("@ClientName", obj.ClientName);
                cmd.Parameters.AddWithValue("@ProjectName", obj.ProjectName);
                cmd.Parameters.AddWithValue("@ProjectStartingDate", obj.ProjectStartingDate);
                cmd.Parameters.AddWithValue("@CompletionDate", obj.CompletionDate);
                cmd.Parameters.AddWithValue("@ProjectDeliveryDate", obj.ProjectDeliveryDate);
                cmd.Parameters.AddWithValue("@TotalAmount", obj.TotalAmount);
                cmd.Parameters.AddWithValue("@ProjectStatus", obj.ProjectStatus);
                row = cmd.ExecuteNonQuery();
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
            return row;
        }
        public int InsertDocument(Project obj)
        {
            int row = 0;
            try
            {
                cmd = new SqlCommand("Sp_Project", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@Action", "InsertDocument");
                cmd.Parameters.AddWithValue("@ProjectCode", obj.ProjectCode);
                cmd.Parameters.AddWithValue("@Document", obj.Document);
                row = cmd.ExecuteNonQuery();
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
            return row;
        }
        public int DeleteDocument(string ProjectCode = "")
        {
            int row = 0;
            try
            {
                cmd = new SqlCommand("Sp_Project", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "deleteDocument");
                con.Open();
                cmd.Parameters.AddWithValue("@ProjectCode", ProjectCode);
                row = cmd.ExecuteNonQuery();
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
            return row;
        }
        public int DeleteProject(int Id = 0)
        {
            int row = 0;
            try
            {
                cmd = new SqlCommand("Sp_Project", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "Delete");
                con.Open();
                cmd.Parameters.AddWithValue("@Id", Id);
                row = cmd.ExecuteNonQuery();
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
            return row;
        }
        public List<Project> GetProjectList()
        {
            List<Project> gtlst = new List<Project>();
            Project obj = new Project();
            try
            {
                cmd = new SqlCommand("Sp_Project", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "Bind");
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        obj = new Project();
                        obj.Id = Convert.ToInt32(rd["Id"]);
                        obj.CompanyName = rd["CompanyName"].ToString();
                        obj.ProjectName = rd["ProjectName"].ToString();
                        obj.ProjectStartingDate = rd["ProjectStartingDate"].ToString();
                        obj.CompletionDate = rd["CompletionDate"].ToString();
                        obj.ProjectDeliveryDate = rd["ProjectDeliveryDate"].ToString();
                        obj.TotalAmount = Convert.ToDecimal(rd["TotalAmount"]);
                        obj.ProjectCode = rd["ProjectCode"].ToString();
                        gtlst.Add(obj);
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
            return gtlst;
        }
        public Project ProjectView(int Id = 0)
        {
            Project obj = new Project();
            try
            {
                cmd = new SqlCommand("Sp_Project", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "ProjectView");
                cmd.Parameters.AddWithValue("@Id", Id);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        obj = new Project();
                        obj.Id = Convert.ToInt32(rd["Id"]);
                        obj.CompanyId = rd["CompanyId"].ToString();
                        obj.CompanyName = rd["CompanyName"].ToString();
                        obj.ClientName = rd["ClientName"].ToString();
                        obj.ProjectName = rd["ProjectName"].ToString();
                        obj.ProjectCode = rd["ProjectCode"].ToString();
                        obj.ProjectStartingDate = rd["ProjectStartingDate"].ToString();
                        obj.CompletionDate = rd["CompletionDate"].ToString();
                        obj.ProjectDeliveryDate = rd["ProjectDeliveryDate"].ToString();
                        obj.TotalAmount = Convert.ToDecimal(rd["TotalAmount"]);
                        obj.ProjectStatus = rd["ProjectStatus"].ToString();
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
            return obj;
        }
        public int AddLeaveType(LeaveTypes obj)
        {
            int row = 0;
            try
            {
                cmd = new SqlCommand("Sp_LeaveManagement", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@Action", "InsertLeaveType");
                cmd.Parameters.AddWithValue("@LeaveType", obj.LeaveType);
                cmd.Parameters.AddWithValue("@Description", obj.Description);
                row = cmd.ExecuteNonQuery();
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
            return row;
        }
        public int UpdateLeaveType(LeaveTypes obj)
        {
            int row = 0;
            try
            {
                cmd = new SqlCommand("Sp_LeaveManagement", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "UpdateLeaveType");
                con.Open();
                cmd.Parameters.AddWithValue("@Id", obj.Id);
                cmd.Parameters.AddWithValue("@LeaveType", obj.LeaveType);
                cmd.Parameters.AddWithValue("@Description", obj.Description);
                row = cmd.ExecuteNonQuery();
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
            return row;
        }
        public List<LeaveTypes> bindlevetype()
        {
            List<LeaveTypes> list = new List<LeaveTypes>();
            LeaveTypes obj = new LeaveTypes();
            try
            {
                cmd = new SqlCommand("Sp_LeaveManagement", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "SelectLeveType");
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        obj = new LeaveTypes();
                        obj.Id = Convert.ToInt32(rd["Id"]);
                        obj.LeaveType = rd["LeaveType"].ToString();
                        obj.Description = rd["Description"].ToString();
                        list.Add(obj);
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
            return list;
        }
        public LeaveTypes Editlevetype(int Id = 0)
        {
            LeaveTypes obj = new LeaveTypes();
            try
            {
                cmd = new SqlCommand("Sp_LeaveManagement", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "EditLeveType");
                cmd.Parameters.AddWithValue("@Id", Id);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        obj = new LeaveTypes();
                        obj.Id = Convert.ToInt32(rd["Id"]);
                        obj.LeaveType = rd["LeaveType"].ToString();
                        obj.Description = rd["Description"].ToString();
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
            return obj;
        }
        public int Deletelevetype(int Id = 0)
        {
            int row = 0;
            try
            {
                cmd = new SqlCommand("Sp_LeaveManagement", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "deleteLeveType");
                con.Open();
                cmd.Parameters.AddWithValue("@Id", Id);
                row = cmd.ExecuteNonQuery();
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
            return row;
        }
        public List<SelectListItem> GetEmployyeListfORlEAVEaSSIGN()
        {
            List<SelectListItem> getlist = new List<SelectListItem>();
            SelectListItem obj = new SelectListItem();
            try
            {
                cmd = new SqlCommand("Sp_Employee", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "BindEmp");
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        obj = new SelectListItem();
                        obj.Value = Convert.ToInt32(rd["Id"]).ToString();
                        obj.Text = rd["EmployeeName"].ToString();
                        getlist.Add(obj);
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
            return getlist;
        }
        public int InsertAssignLeave(string EmployeeId = "", string EmployeeName = "", string NoOfLeave = "", string LeaveType = "")
        {
            int row = 0;
            string CurrentYear = DateTime.Now.Year.ToString();
            try
            {
                cmd = new SqlCommand("Sp_LeaveManagement", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "InsertAssignLeave");
                con.Open();
                cmd.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                cmd.Parameters.AddWithValue("@EmployeeName", EmployeeName);
                cmd.Parameters.AddWithValue("@LeaveType", LeaveType);
                cmd.Parameters.AddWithValue("@NoOfLeave", NoOfLeave);
                cmd.Parameters.AddWithValue("@Year", CurrentYear);
                row = cmd.ExecuteNonQuery();
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
            return row;
        }
        public int CountEmployee()
        {
            int Count = 0;
            try
            {
                cmd = new SqlCommand("Sp_Dashboard", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "TotalEmployee");
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        Count = Convert.ToInt32(rd["Total"]);
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
            return Count;
        }
        public int CountAssignEmployee()
        {
            int Count = 0;
            int EmpId = 0;
            EmpId = ((Login)HttpContext.Current.Session["Login"]).EmpId;
            try
            {
                cmd = new SqlCommand("Sp_Dashboard", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "TotalAssignEmployee");
                cmd.Parameters.AddWithValue("@id", EmpId);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        Count = Convert.ToInt32(rd["TotalEmployeeCount"]);
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
            return Count;
        }
        public int CountAssignProject1()
        {
            int Count = 0;
            int EmpId = 0;
            EmpId = ((Login)HttpContext.Current.Session["Login"]).EmpId;
            try
            {
                cmd = new SqlCommand("Sp_Dashboard", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "TotalAssignProject");
                cmd.Parameters.AddWithValue("@EmployeeId", EmpId);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        Count = Convert.ToInt32(rd["TotalProjects"]);
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
            return Count;
        }
        public int CountPendingTask()
        {
            int Count = 0;
            int EmpId = 0;
            EmpId = ((Login)HttpContext.Current.Session["Login"]).EmpId;
            try
            {
                cmd = new SqlCommand("Sp_Dashboard", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "PendingTaskByEmp");
                cmd.Parameters.AddWithValue("@userid", EmpId);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        Count = Convert.ToInt32(rd["Pending"]);
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
            return Count;
        }
        public int CountTotalLeave()
        {
            int Count = 0;
            int EmpId = 0;
            EmpId = ((Login)HttpContext.Current.Session["Login"]).EmpId;
            try
            {
                cmd = new SqlCommand("Sp_Dashboard", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "TotalLeave");
                cmd.Parameters.AddWithValue("@EmployeeId", EmpId);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        if (rd["TotalLeave"] != DBNull.Value)
                        {
                            Count = Convert.ToInt32(rd["TotalLeave"]);
                        }
                       
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
            return Count;
        }
        public int CountTotalPendingLeave()
        {
            int Count = 0;
            int EmpId = 0;
            EmpId = ((Login)HttpContext.Current.Session["Login"]).EmpId;
            try
            {
                cmd = new SqlCommand("Sp_Dashboard", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "TotalPendingLeave");
                cmd.Parameters.AddWithValue("@EmployeeId", EmpId);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        if (rd["TotalPending"] != DBNull.Value)
                        {
                            Count = Convert.ToInt32(rd["TotalPending"]);
                        }

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
            return Count;
        }
        public int CountTotalRejectedLeave()
        {
            int Count = 0;
            int EmpId = 0;
            EmpId = ((Login)HttpContext.Current.Session["Login"]).EmpId;
            try
            {
                cmd = new SqlCommand("Sp_Dashboard", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "TotalRejectedLeave");
                cmd.Parameters.AddWithValue("@EmployeeId", EmpId);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        if (rd["TotalRejected"] != DBNull.Value)
                        {
                            Count = Convert.ToInt32(rd["TotalRejected"]);
                        }

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
            return Count;
        }
        public int CountTotalApprovedLeave()
        {
            int Count = 0;
            int EmpId = 0;
            EmpId = ((Login)HttpContext.Current.Session["Login"]).EmpId;
            try
            {
                cmd = new SqlCommand("Sp_Dashboard", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "TotalApprovedLeave");
                cmd.Parameters.AddWithValue("@EmployeeId", EmpId);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        if (rd["TotalApproved"] != DBNull.Value)
                        {
                            Count = Convert.ToInt32(rd["TotalApproved"]);
                        }

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
            return Count;
        }
        public int CL()
        {
            int Count = 0;
            int EmpId = 0;
            EmpId = ((Login)HttpContext.Current.Session["Login"]).EmpId;
            try
            {
                cmd = new SqlCommand("Sp_Dashboard", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "CLByEmp");
                cmd.Parameters.AddWithValue("@EmployeeId", EmpId);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        if (rd["RemainingLeave"] != DBNull.Value)
                        {
                            Count = Convert.ToInt32(rd["RemainingLeave"]);
                        }
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
            return Count;
        }
        public int TotalCLApproved()
        {
            int Count = 0;
            int EmpId = 0;
            EmpId = ((Login)HttpContext.Current.Session["Login"]).EmpId;
            try
            {
                cmd = new SqlCommand("Sp_Dashboard", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "TotalApprovedCL");
                cmd.Parameters.AddWithValue("@EmployeeId", EmpId);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        if (rd["ApprovedLeave"] != DBNull.Value)
                        {
                            Count = Convert.ToInt32(rd["ApprovedLeave"]);
                        }
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
            return Count;
        }
        public int TotalMLApproved()
        {
            int Count = 0;
            int EmpId = 0;
            EmpId = ((Login)HttpContext.Current.Session["Login"]).EmpId;
            try
            {
                cmd = new SqlCommand("Sp_Dashboard", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "TotalApprovedML");
                cmd.Parameters.AddWithValue("@EmployeeId", EmpId);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        if (rd["ApprovedLeave"] != DBNull.Value)
                        {
                            Count = Convert.ToInt32(rd["ApprovedLeave"]);
                        }
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
            return Count;
        }
        public int TotalELApproved()
        {
            int Count = 0;
            int EmpId = 0;
            EmpId = ((Login)HttpContext.Current.Session["Login"]).EmpId;
            try
            {
                cmd = new SqlCommand("Sp_Dashboard", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "TotalApprovedEL");
                cmd.Parameters.AddWithValue("@EmployeeId", EmpId);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        if (rd["ApprovedLeave"] != DBNull.Value)
                        {
                            Count = Convert.ToInt32(rd["ApprovedLeave"]);
                        }
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
            return Count;
        }
        public int TotalLWPApproved()
        {
            int Count = 0;
            int EmpId = 0;
            EmpId = ((Login)HttpContext.Current.Session["Login"]).EmpId;
            try
            {
                cmd = new SqlCommand("Sp_Dashboard", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "TotalApprovedLWP");
                cmd.Parameters.AddWithValue("@EmployeeId", EmpId);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        if (rd["ApprovedLeave"] != DBNull.Value)
                        {
                            Count = Convert.ToInt32(rd["ApprovedLeave"]);
                        }
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
            return Count;
        }
        public int TotalCL()
        {
            int Count = 0;
            int EmpId = 0;
            EmpId = ((Login)HttpContext.Current.Session["Login"]).EmpId;
            try
            {
                cmd = new SqlCommand("Sp_Dashboard", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "TotalCL");
                cmd.Parameters.AddWithValue("@EmployeeId", EmpId);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        if (rd["TotalAssignedCL"] != DBNull.Value)
                        {
                            Count = Convert.ToInt32(rd["TotalAssignedCL"]);
                        }
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
            return Count;
        }
        public int TotalML()
        {
            int Count = 0;
            int EmpId = 0;
            EmpId = ((Login)HttpContext.Current.Session["Login"]).EmpId;
            try
            {
                cmd = new SqlCommand("Sp_Dashboard", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "TotalML");
                cmd.Parameters.AddWithValue("@EmployeeId", EmpId);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        if (rd["TotalAssignedML"] != DBNull.Value)
                        {
                            Count = Convert.ToInt32(rd["TotalAssignedML"]);
                        }

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
            return Count;
        }
        public int TotalEL()
        {
            int Count = 0;
            int EmpId = 0;
            EmpId = ((Login)HttpContext.Current.Session["Login"]).EmpId;
            try
            {
                cmd = new SqlCommand("Sp_Dashboard", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "TotalEL");
                cmd.Parameters.AddWithValue("@EmployeeId", EmpId);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        if (rd["TotalAssignedEL"] != DBNull.Value)
                        {
                            Count = Convert.ToInt32(rd["TotalAssignedEL"]);
                        }
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
            return Count;
        }
        public int TotalLWP()
        {
            int Count = 0;
            int EmpId = 0;
            EmpId = ((Login)HttpContext.Current.Session["Login"]).EmpId;
            try
            {
                cmd = new SqlCommand("Sp_Dashboard", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "TotalLWP");
                cmd.Parameters.AddWithValue("@EmployeeId", EmpId);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        if (rd["TotalAssignedLWP"] != DBNull.Value)
                        {
                            Count = Convert.ToInt32(rd["TotalAssignedLWP"]);
                        }
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
            return Count;
        }
        public int ML()
        {
            int Count = 0;
            int EmpId = 0;
            EmpId = ((Login)HttpContext.Current.Session["Login"]).EmpId;
            try
            {
                cmd = new SqlCommand("Sp_Dashboard", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "MLByEmp");
                cmd.Parameters.AddWithValue("@EmployeeId", EmpId);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        if (rd["RemainingLeave"] != DBNull.Value)
                        {
                            Count = Convert.ToInt32(rd["RemainingLeave"]);
                        }
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
            return Count;
        }
        public int EL()
        {
            int Count = 0;
            int EmpId = 0;
            EmpId = ((Login)HttpContext.Current.Session["Login"]).EmpId;
            try
            {
                cmd = new SqlCommand("Sp_Dashboard", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "ELByEmp");
                cmd.Parameters.AddWithValue("@EmployeeId", EmpId);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        if (rd["RemainingLeave"] != DBNull.Value)
                        {
                            Count = Convert.ToInt32(rd["RemainingLeave"]);
                        }
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
            return Count;
        }
        public int LWP()
        {
            int Count = 0;
            int EmpId = 0;
            EmpId = ((Login)HttpContext.Current.Session["Login"]).EmpId;
            try
            {
                cmd = new SqlCommand("Sp_Dashboard", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "LWPByEmp");
                cmd.Parameters.AddWithValue("@EmployeeId", EmpId);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        if (rd["RemainingLeave"] != DBNull.Value)
                        {
                            Count = Convert.ToInt32(rd["RemainingLeave"]);
                        }
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
            return Count;
        }
        public int CountCompleteTask()
        {
            int Count = 0;
            int EmpId = 0;
            EmpId = ((Login)HttpContext.Current.Session["Login"]).EmpId;
            try
            {
                cmd = new SqlCommand("Sp_Dashboard", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "CompleteTaskByEmp");
                cmd.Parameters.AddWithValue("@userid", EmpId);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        Count = Convert.ToInt32(rd["Complete"]);
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
            return Count;
        }
        public int CountClient()
        {
            int Count = 0;
            try
            {
                cmd = new SqlCommand("Sp_Dashboard", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "TotalClient");
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        Count = Convert.ToInt32(rd["Total"]);
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
            return Count;
        }
        public int CountProject()
        {
            int Count = 0;
            try
            {
                cmd = new SqlCommand("Sp_Dashboard", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "TotalProject");
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        Count = Convert.ToInt32(rd["Total"]);
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
            return Count;
        }
        public int CountAssignProject()
        {
            int Count = 0;
            try
            {
                cmd = new SqlCommand("Sp_Dashboard", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "CountAssignProject");
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        Count = Convert.ToInt32(rd["Total"]);
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
            return Count;
        }
        public int CountQuatation()
        {
            int Count = 0;
            try
            {
                cmd = new SqlCommand("Sp_Dashboard", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "Quatation");
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        Count = Convert.ToInt32(rd["Total"]);
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
            return Count;
        }
        public int CountPI()
        {
            int Count = 0;
            try
            {
                cmd = new SqlCommand("Sp_Dashboard", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "PI");
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        Count = Convert.ToInt32(rd["Total"]);
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
            return Count;
        }
        public int CountTax()
        {
            int Count = 0;
            try
            {
                cmd = new SqlCommand("Sp_Dashboard", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "Tax");
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        Count = Convert.ToInt32(rd["Total"]);
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
            return Count;
        }
        public int CountPurchase()
        {
            int Count = 0;
            try
            {
                cmd = new SqlCommand("Sp_Dashboard", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "Purchase");
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        Count = Convert.ToInt32(rd["Total"]);
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
            return Count;
        }
        public int CountTaskSheet()
        {
            int Count = 0;
            try
            {
                cmd = new SqlCommand("Sp_Dashboard", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "TaskSheet");
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        Count = Convert.ToInt32(rd["Total"]);
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
            return Count;
        }
        public List<SelectListItem> BindLeveTypeForEmployee()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            SelectListItem obj = new SelectListItem();
            try
            {
                cmd = new SqlCommand("Sp_LeaveManagement", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "SelectLeveType");
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        obj = new SelectListItem();
                        obj.Value = Convert.ToInt32(rd["Id"]).ToString();
                        obj.Text = rd["LeaveType"].ToString();
                        list.Add(obj);
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
            return list;
        }
        public int InsertApplyLeave(ApplyLeave obj)
        {
            int row = 0;
            int EmpId = 0;
            string EmpName = "";
            EmpId = ((Login)HttpContext.Current.Session["Login"]).EmpId;
            EmpName = ((Login)HttpContext.Current.Session["Login"]).EmployeeName;
            try
            {
                cmd = new SqlCommand("Sp_LeaveManagement", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "InsertApplyEmployeeLeave");
                con.Open();
                cmd.Parameters.AddWithValue("@ToDate", obj.ToDate);
                cmd.Parameters.AddWithValue("@FromDate", obj.FromDate);
                cmd.Parameters.AddWithValue("@LeaveType", obj.LeaveType);
                cmd.Parameters.AddWithValue("@Description", obj.Description);
                cmd.Parameters.AddWithValue("@EmployeeId", EmpId);
                cmd.Parameters.AddWithValue("@EmployeeName", EmpName);
                row = cmd.ExecuteNonQuery();
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
            return row;
        }
        public int UpdateApplyLeave(ApplyLeave obj)
        {
            int row = 0;
            try
            {
                cmd = new SqlCommand("Sp_LeaveManagement", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "UpdateApplyLeave");
                con.Open();
                cmd.Parameters.AddWithValue("@Id", obj.Id);
                cmd.Parameters.AddWithValue("@ToDate", obj.ToDate);
                cmd.Parameters.AddWithValue("@FromDate", obj.FromDate);
                cmd.Parameters.AddWithValue("@LeaveType", obj.LeaveType);
                cmd.Parameters.AddWithValue("@Description", obj.Description);
                row = cmd.ExecuteNonQuery();
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
            return row;
        }
        public List<ApplyLeave> BindApplyLeave()
        {
            List<ApplyLeave> gtlst = new List<ApplyLeave>();
            ApplyLeave obj = new ApplyLeave();
            int EmpId = 0;
            EmpId = ((Login)HttpContext.Current.Session["Login"]).EmpId;
            try
            {
                cmd = new SqlCommand("Sp_LeaveManagement", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "SelectApplyEmployeeLeave");
                cmd.Parameters.AddWithValue("@EmployeeId", EmpId);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        obj = new ApplyLeave();
                        obj.Id = Convert.ToInt32(rd["Id"]);
                        obj.EmployeeName = rd["EmployeeName"].ToString();
                        obj.ToDate = rd["ToDate"].ToString();
                        obj.FromDate = rd["FromDate"].ToString();
                        obj.LeaveType = rd["LeaveType"].ToString();
                        obj.Description = rd["Description"].ToString();
                        obj.AppliedDate = rd["AppliedDate"].ToString();
                        obj.AdminDescription = rd["AdminDescription"].ToString();
                        obj.LeaveCount = Convert.ToInt32(rd["LeaveCount"]);
                        obj.Status = rd["Status"].ToString();
                        gtlst.Add(obj);
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
            return gtlst;
        }
        public ApplyLeave EditApplyLeave(int Id = 0)
        {
            ApplyLeave obj = new ApplyLeave();
            try
            {
                cmd = new SqlCommand("Sp_LeaveManagement", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "EditApplyEmployeeLeave");
                cmd.Parameters.AddWithValue("@Id", Id);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        obj = new ApplyLeave();
                        obj.Id = Convert.ToInt32(rd["Id"]);
                        obj.EmployeeName = rd["EmployeeName"].ToString();
                        obj.ToDate = rd["ToDate"].ToString();
                        obj.FromDate = rd["FromDate"].ToString();
                        obj.LeaveType = rd["LeaveType"].ToString();
                        obj.Description = rd["Description"].ToString();
                        obj.AppliedDate = rd["AppliedDate"].ToString();
                        obj.AdminDescription = rd["AdminDescription"].ToString();
                        obj.LeaveCount = Convert.ToInt32(rd["LeaveCount"]);
                        obj.Status = rd["Status"].ToString();

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
            return obj;
        }
        public List<ApplyLeave> GetApplyListForAdmin()
        {

            List<ApplyLeave> getlist = new List<ApplyLeave>();
            ApplyLeave obj = new ApplyLeave();
            try
            {
                cmd = new SqlCommand("Sp_LeaveManagement", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "ApplyLeaveForAdmin");
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        obj = new ApplyLeave();
                        obj.Id = Convert.ToInt32(rd["Id"]);
                        obj.EmployeeId = rd["EmployeeId"].ToString();
                        obj.EmployeeName = rd["EmployeeName"].ToString();
                        obj.ToDate = rd["ToDate"].ToString();
                        obj.FromDate = rd["FromDate"].ToString();
                        obj.LeaveCount =Convert.ToInt32( rd["LeaveDuration"].ToString());
                        obj.AppliedDate = rd["AppliedDate"].ToString();
                        obj.LeaveType = rd["LeaveType"].ToString();
                        obj.Description = rd["Description"].ToString();
                        obj.AdminDescription = rd["AdminDescription"].ToString();
                        obj.Status = rd["Status"].ToString();
                        getlist.Add(obj);
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
            return getlist;
        }
        public List<Macreel_Project.Models.Bussiness.Employee> getemployeePerformance(DateTime currentDate)
        {
            List<Macreel_Project.Models.Bussiness.Employee> emp = new List<Macreel_Project.Models.Bussiness.Employee>();
            con.Open();
            SqlCommand cmd = new SqlCommand("[Sp_Employee]", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "select_empPerformance");
            cmd.Parameters.AddWithValue("@from_date", currentDate);
            SqlDataReader rd = cmd.ExecuteReader();
            Macreel_Project.Models.Bussiness.Employee obj;
            if (rd.HasRows)
            {
                while (rd.Read())
                {
                    obj = new Macreel_Project.Models.Bussiness.Employee();
                    obj.Id = Convert.ToInt32(rd["Id"]);
                    obj.EmployeeCode = rd["EmployeeCode"].ToString();
                    obj.EmployeeName = rd["EmployeeName"].ToString();
                    obj.Department = rd["Department"].ToString();
                    obj.Designation = rd["Designation"].ToString();
                    obj.ReportingManager = rd["ReportingManager"].ToString();
                    obj.EmailId = rd["EmailId"].ToString();
                    obj.DOJ = rd["DOJ"].ToString();
                    obj.PanNo = rd["PanNo"].ToString();
                    obj.Passport = rd["Passport"].ToString();
                    obj.Mobile = rd["Mobile"].ToString();
                    obj.UserName = rd["UserName"].ToString();
                    obj.Password = rd["Password"].ToString();
                    obj.BankName = rd["BankName"].ToString();
                    obj.AccountNo = rd["AccountNo"].ToString();
                    obj.IFSC = rd["IFSC"].ToString();
                    obj.BankBranch = rd["BankBranch"].ToString();
                    obj.BloodGroup = rd["BloodGroup"].ToString();
                    obj.IdMarks = rd["IdMarks"].ToString();
                    obj.DOB = rd["DOB"].ToString();
                    obj.Age = rd["Age"].ToString();
                    obj.Sex = rd["Sex"].ToString();
                    obj.Nationality = rd["Nationality"].ToString();
                    obj.Religion = rd["Religion"].ToString();
                    obj.MaritalStatus = rd["MaritalStatus"].ToString();
                    obj.PresentAddress = rd["PresentAddress"].ToString();
                    obj.State = rd["State"].ToString();
                    obj.City = rd["City"].ToString();
                    obj.Pin = rd["Pin"].ToString();
                    obj.EmergencyContactPerson = rd["EmergencyContactPerson"].ToString();
                    obj.EmergencyContactNumber = rd["EmergencyContactNumber"].ToString();
                    obj.EmergencyContectAddress = rd["EmergencyContectAddress"].ToString();
                    obj.ReferencesName = rd["ReferencesName"].ToString();
                    obj.ContactNo = rd["ContactNo"].ToString();
                    obj.CompanyName = rd["CompanyName"].ToString();
                    obj.CemailId = rd["CemailId"].ToString();
                    obj.PreviousEmployer = rd["PreviousEmployer"].ToString();
                    obj.From = rd["FromT"].ToString();
                    obj.To = rd["ToT"].ToString();
                    obj.Degree = rd["Degree"].ToString();
                    obj.ProfessionalInstitution = rd["ProfessionalInstitution"].ToString();
                    obj.ProfessionalPassingYear = rd["ProfessionalPassingYear"].ToString();
                    obj.ProfessionalSpecilization = rd["ProfessionalSpecilization"].ToString();
                    obj.Board10 = rd["Board10"].ToString();
                    obj.Institution10 = rd["Institution10"].ToString();
                    obj.PassingYear10 = rd["PassingYear10"].ToString();
                    obj.Specilization10 = rd["Specilization10"].ToString();
                    obj.Board12 = rd["Board12"].ToString();
                    obj.Institution12 = rd["Institution12"].ToString();
                    obj.PassingYear12 = rd["PassingYear12"].ToString();
                    obj.Specilization12 = rd["Specilization12"].ToString();
                    obj.ImagePath = rd["Image"].ToString();
                    obj.StateId = rd["StateId"].ToString();
                    obj.ReportingManager = rd["ReportingManager"].ToString();
                    //obj.current_status = rd["current_status"].ToString();
                    obj.Date = currentDate;
                    obj.EmployeeTaskPerformance = rd["EmployeeTaskPerformance"].ToString();
                    emp.Add(obj);
                }
            }
            con.Close();
            return emp;
        }

        public Macreel_Project.Models.Bussiness.Employee getemployeeById(string id, DateTime? date)
        {
            Macreel_Project.Models.Bussiness.Employee emp = new Macreel_Project.Models.Bussiness.Employee();
            if (id != null && id != "")
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("[Sp_Employee]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "getTask_PerformanceById");
                cmd.Parameters.AddWithValue("@userId", id);
                cmd.Parameters.AddWithValue("@from_date", date);
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    sdr.Read();
                    emp.Id = Convert.ToInt32(sdr["Id"].ToString());
                    emp.EmployeeName = sdr["EmployeeName"].ToString();
                    emp.Designation = sdr["Designation"].ToString();
                    emp.Department = sdr["Department"].ToString();
                    emp.ImagePath = sdr["Image"].ToString();
                    emp.State = sdr["State"].ToString();
                    emp.City = sdr["City"].ToString();
                    emp.ContactNo = sdr["ContactNo"].ToString();
                    emp.EmailId = sdr["EmailId"].ToString();
                    emp.PresentAddress = sdr["PresentAddress"].ToString();
                    emp.EmployeeTaskPerformance = sdr["EmployeeTaskPerformance"].ToString();
                    emp.Date = (DateTime)date;
                }
                sdr.Close();
                con.Close();
            }
            return emp;
        }
        //Apply Leave For Reporting Manager
        public List<ApplyLeave> GetApplyListForReportingManager()
        {
            var EmpId = ((Login)HttpContext.Current.Session["Login"]).EmpId;
            List<ApplyLeave> getlist = new List<ApplyLeave>();
            ApplyLeave obj = new ApplyLeave();
            try
            {
                cmd = new SqlCommand("Sp_LeaveManagement", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "ApplyLeaveForReporting");
                cmd.Parameters.AddWithValue("@Id", EmpId);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        obj = new ApplyLeave();
                        obj.Id = Convert.ToInt32(rd["LeaveId"]);
                        obj.EmployeeId = rd["EmployeeId"].ToString();
                        obj.EmployeeName = rd["EmployeeName"].ToString();
                        obj.ToDate = rd["ToDate"].ToString();
                        obj.FromDate = rd["FromDate"].ToString();
                        obj.LeaveCount =Convert.ToInt32( rd["LeaveDuration"].ToString());
                        obj.AppliedDate = rd["AppliedDate"].ToString();
                        obj.LeaveType = rd["LeaveType"].ToString();
                        obj.Description = rd["Description"].ToString();
                        obj.AdminDescription = rd["AdminDescription"].ToString();
                        obj.Status = rd["Status"].ToString();
                        getlist.Add(obj);
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
            return getlist;
        }
        public int UpdateLeaveStatus(int Id = 0, string Status = "", int LeaveCount = 0, string Description = "")
        {
            int row = 0;
            try
            {
                cmd = new SqlCommand("Sp_LeaveManagement", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "UpdateLeaveStatus");
                con.Open();
                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.Parameters.AddWithValue("@STATUS", Status);
                cmd.Parameters.AddWithValue("@AdminDescription", Description);
                cmd.Parameters.AddWithValue("@LeaveCount", LeaveCount);
                row = cmd.ExecuteNonQuery();
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
            return row;
        }
        //update leave status by reporting manager
        public int UpdateLeaveStatusbyReporting(int Id = 0, string Status = "", int LeaveCount = 0, string Description = "")
        {
            int row = 0;
            try
            {
                cmd = new SqlCommand("Sp_LeaveManagement", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "UpdateLeaveStatusbyReporting");
                con.Open();
                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.Parameters.AddWithValue("@STATUS", Status);
                cmd.Parameters.AddWithValue("@AdminDescription", Description);
                cmd.Parameters.AddWithValue("@LeaveCount", LeaveCount);
                row = cmd.ExecuteNonQuery();
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
            return row;
        }
        public int DeleteApplyLeave(int Id = 0)
        {
            int row = 0;
            try
            {
                cmd = new SqlCommand("Sp_LeaveManagement", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "DeleteApplyLeave");
                con.Open();
                cmd.Parameters.AddWithValue("@Id", Id);
                row = cmd.ExecuteNonQuery();
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
            return row;
        }
        public int DeleteApplyLeaveByEMP(int Id = 0)
        {
            int row = 0;
            try
            {
                cmd = new SqlCommand("Sp_LeaveManagement", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "deleteLeaveByemp");
                con.Open();
                cmd.Parameters.AddWithValue("@Id", Id);
                row = cmd.ExecuteNonQuery();
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
            return row;
        }
        public List<ApplyLeave> GetUserDashboardLeave()
        {
            List<ApplyLeave> getlist = new List<ApplyLeave>();
            ApplyLeave obj = new ApplyLeave();

            int EmpId = 0;
            EmpId = ((Login)HttpContext.Current.Session["Login"]).EmpId;
            try
            {
                cmd = new SqlCommand("Sp_LeaveManagement", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "UserDashboardLeave");
                cmd.Parameters.AddWithValue("@EmployeeId", EmpId);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        obj = new ApplyLeave();
                        obj.EmployeeName = rd["EmployeeName"].ToString();
                        obj.LeaveType = rd["LeaveType"].ToString();
                        obj.NoOfLeave = Convert.ToInt32(rd["NoOfLeave"]);
                        if (rd["LeaveCount"] != DBNull.Value)
                        {
                            obj.LeaveCount = Convert.ToInt32(rd["LeaveCount"]);
                        }
                        obj.Remain = obj.NoOfLeave - obj.LeaveCount;
                        getlist.Add(obj);
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
            return getlist;
        }
        public List<ApplyLeave> GetUserDashboardLeaveForAdmin(int Id = 0)
        {
            List<ApplyLeave> getlist = new List<ApplyLeave>();
            ApplyLeave obj = new ApplyLeave();

            try
            {
                cmd = new SqlCommand("Sp_LeaveManagement", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "UserDashboardLeave");
                cmd.Parameters.AddWithValue("@EmployeeId", Id);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        obj = new ApplyLeave();
                        obj.EmployeeName = rd["EmployeeName"].ToString();
                        obj.LeaveType = rd["LeaveType"].ToString();
                        obj.NoOfLeave = Convert.ToInt32(rd["NoOfLeave"]);
                        if (rd["LeaveCount"] != DBNull.Value)
                        {
                            obj.LeaveCount = Convert.ToInt32(rd["LeaveCount"]);
                        }
                        obj.Remain = obj.NoOfLeave - obj.LeaveCount;
                        getlist.Add(obj);
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
            return getlist;
        }
        public List<SelectListItem> BindProject()
        {
            List<SelectListItem> getlist = new List<SelectListItem>();
            SelectListItem obj = new SelectListItem();
            try
            {
                cmd = new SqlCommand("Sp_Project", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@Action", "Bind");
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        obj = new SelectListItem();
                        obj.Value = rd["ProjectCode"].ToString();
                        obj.Text = rd["ProjectName"].ToString();
                        getlist.Add(obj);
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
            return getlist;
        }
        public Project GetProjectDetail(string ProjcetCode = "")
        {
            Project obj = new Project();
            try
            {
                cmd = new SqlCommand("Sp_Project", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "GetProjectDetail");
                cmd.Parameters.AddWithValue("@ProjectCode", ProjcetCode);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        obj = new Project();
                        obj.Id = Convert.ToInt32(rd["Id"]);
                        obj.CompanyId = rd["CompanyId"].ToString();
                        obj.CompanyName = rd["CompanyName"].ToString();
                        obj.ClientName = rd["ClientName"].ToString();
                        obj.ProjectName = rd["ProjectName"].ToString();
                        obj.ProjectCode = rd["ProjectCode"].ToString();
                        obj.ProjectStartingDate = rd["ProjectStartingDate"].ToString();
                        obj.CompletionDate = rd["CompletionDate"].ToString();
                        obj.ProjectDeliveryDate = rd["ProjectDeliveryDate"].ToString();
                        obj.TotalAmount = Convert.ToDecimal(rd["TotalAmount"]);
                        obj.ProjectStatus = rd["ProjectStatus"].ToString();
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
            return obj;
        }
        public int InsertEmployeeProject(string EmployeeId = "", string ProjectCode = "", string EmployeeName = "", string ProjectName = "", string Description = "", string AssignDate = "")
        {
            int row = 0;
            try
            {
                cmd = new SqlCommand("Sp_Project", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@Action", "InsertEmployee");
                cmd.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                cmd.Parameters.AddWithValue("@EmployeeName", EmployeeName);
                cmd.Parameters.AddWithValue("@ProjectCode", ProjectCode);
                cmd.Parameters.AddWithValue("@ProjectName", ProjectName);
                cmd.Parameters.AddWithValue("@Description", Description);
                //     cmd.Parameters.AddWithValue("@AssignDate", AssignDate); replace with @ProjectStartingDate
                cmd.Parameters.AddWithValue("@ProjectStartingDate", AssignDate);
                row = cmd.ExecuteNonQuery();
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
            return row;
        }
        public string ProjectEmployee(string ProjectCode = "")
        {
            string Employee = "";
            try
            {
                cmd = new SqlCommand("Sp_Project", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "ProjectEmployee");
                con.Open();
                cmd.Parameters.AddWithValue("@ProjectCode", ProjectCode);
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        if (Employee != "")
                        {
                            Employee = Employee + "," + rd["EmployeeName"].ToString();

                        }
                        else
                        {
                            Employee = rd["EmployeeName"].ToString();
                        }
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
            return Employee;
        }
        public string ProjectEmployeeDescription(string ProjectCode = "")
        {
            string Description = "";
            try
            {
                cmd = new SqlCommand("Sp_Project", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "AssignProjectDescription");
                con.Open();
                cmd.Parameters.AddWithValue("@ProjectCode", ProjectCode);
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        Description = rd["Description"].ToString();
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
            return Description;
        }
        public List<Project> getDocumentaion(string ProjectCode = "")
        {
            List<Project> getlist = new List<Project>();
            Project obj = new Project();
            try
            {
                cmd = new SqlCommand("Sp_Project", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "GetProjectDocumentation");
                cmd.Parameters.AddWithValue("@ProjectCode", ProjectCode);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        obj = new Project();
                        obj.Document = rd["DocumentPath"].ToString();
                        getlist.Add(obj);
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
            return getlist;
        }
        public List<Project> GetAssignProjectList()
        {
            List<Project> gtlst = new List<Project>();
            Project obj = new Project();
            DataAccess dc = new DataAccess();
            try
            {
                cmd = new SqlCommand("Sp_Project", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "AssignBind");
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        obj = new Project();
                        obj.Id = Convert.ToInt32(rd["Id"]);
                        obj.ProjectName = rd["ProjectName"].ToString();
                        obj.CompanyName = rd["CompanyName"].ToString();
                        obj.ProjectStartingDate = rd["ProjectStartingDate"].ToString();
                        obj.CompletionDate = rd["CompletionDate"].ToString();
                        obj.ProjectDeliveryDate = rd["ProjectDeliveryDate"].ToString();
                        obj.TotalAmount = Convert.ToDecimal(rd["TotalAmount"]);
                        obj.ProjectCode = rd["ProjectCode"].ToString();
                        obj.ProjectStatus = rd["ProjectStatus"].ToString();
                        //obj.AssignDate = rd["AssignDate"].ToString();
                        obj.AssignDate = rd["ProjectStartingDate"].ToString();
                        obj.EmployeeName = dc.ProjectEmployee(obj.ProjectCode);
                        gtlst.Add(obj);
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
            return gtlst;
        }
        public List<SelectListItem> BindProjectForAssignProject()
        {
            List<SelectListItem> getlist = new List<SelectListItem>();
            SelectListItem obj = new SelectListItem();
            try
            {
                cmd = new SqlCommand("Sp_Project", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@Action", "ProjectBindForAssign");
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        obj = new SelectListItem();
                        obj.Value = rd["ProjectCode"].ToString();
                        obj.Text = rd["ProjectName"].ToString();
                        getlist.Add(obj);
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
            return getlist;
        }
        public List<Project> GetEmployeeProjectList()
        {
            List<Project> gtlst = new List<Project>();
            Project obj = new Project();
            int EmpId = 0;
            EmpId = ((Login)HttpContext.Current.Session["Login"]).EmpId;
            try
            {
                cmd = new SqlCommand("Sp_Project", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "EmployeeProject");
                cmd.Parameters.AddWithValue("@EmployeeId", EmpId);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        obj = new Project();
                        if (rd["Id"] != DBNull.Value)
                        {
                            obj.Id = Convert.ToInt32(rd["Id"]);
                        }
                        if (rd["ProjectName"] != DBNull.Value)
                        {
                            obj.ProjectName = rd["ProjectName"].ToString();
                        }
                        if (rd["ProjectName"] != DBNull.Value)
                        {
                            obj.ProjectName = rd["ProjectName"].ToString();
                        }
                        if (rd["CompanyName"] != DBNull.Value)
                        {
                            obj.CompanyName = rd["CompanyName"].ToString();
                        }
                        if (rd["ProjectStartingDate"] != DBNull.Value)
                        {
                            obj.ProjectStartingDate = rd["ProjectStartingDate"].ToString();
                        }
                        if (rd["CompletionDate"] != DBNull.Value)
                        {
                            obj.CompletionDate = rd["CompletionDate"].ToString();
                        }
                        if (rd["ProjectDeliveryDate"] != DBNull.Value)
                        {
                            obj.ProjectDeliveryDate = rd["ProjectDeliveryDate"].ToString();
                        }
                        if (rd["TotalAmount"] != DBNull.Value)
                        {
                            obj.TotalAmount = Convert.ToDecimal(rd["TotalAmount"]);
                        }
                        if (rd["ProjectCode"] != DBNull.Value)
                        {
                            obj.ProjectCode = rd["ProjectCode"].ToString();
                        }
                        if (rd["ProjectStatus"] != DBNull.Value)
                        {
                            obj.ProjectStatus = rd["ProjectStatus"].ToString();
                        }
                        gtlst.Add(obj);
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
            return gtlst;
        }

        public List<TaskManage> ViewApprovedTask()
        {
            var userid = ((Login)HttpContext.Current.Session["Login"]).EmpId;
            List<TaskManage> task_list = new List<TaskManage>();
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_TaskManage", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "select_approvaltask1");
            cmd.Parameters.AddWithValue("@id", userid);
            SqlDataReader sdr = cmd.ExecuteReader();
            TaskManage pro;

            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    pro = new TaskManage();
                    pro.id = sdr["id"].ToString();
                    pro.s_no = sdr["S_no"].ToString();
                    pro.title = sdr["title"].ToString();
                    pro.description = sdr["description"].ToString();
                    pro.complete_date = sdr["completion_date"].ToString();
                    pro.current_date = sdr["curent_date"].ToString();
                    pro.attachment1 = sdr["attachment1"].ToString();
                    pro.attachment2 = sdr["attachment2"].ToString();
                    pro.attachment3 = sdr["attachment3"].ToString();
                    pro.attachment4 = sdr["attachment4"].ToString();
                    pro.attachment5 = sdr["attachment5"].ToString();
                    pro.assigned_emp = sdr["assign_emp"].ToString();
                    pro.task_status = sdr["task_status"].ToString();
                    pro.emp_status = sdr["emp_status"].ToString();
                    pro.updatedDateEmp = sdr["updatedDateEmp"].ToString();
                    task_list.Add(pro);
                }
            }
            con.Close();


            return task_list;
        }
        public List<TaskManage> ViewTask()
        {
            var userid = ((Login)HttpContext.Current.Session["Login"]).EmpId;
            List<TaskManage> task_list = new List<TaskManage>();
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_TaskManage", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "select_task1");
            cmd.Parameters.AddWithValue("@id", userid);
            SqlDataReader sdr = cmd.ExecuteReader();
            TaskManage pro;
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    pro = new TaskManage();
                    pro.id = sdr["id"].ToString();
                    pro.s_no = sdr["S_no"].ToString();
                    pro.title = sdr["title"].ToString();
                    pro.description = sdr["description"].ToString();
                    pro.complete_date = sdr["completion_date"].ToString();
                    pro.current_date = sdr["curent_date"].ToString();
                    pro.attachment1 = sdr["attachment1"].ToString();
                    pro.attachment2 = sdr["attachment2"].ToString();
                    pro.attachment3 = sdr["attachment3"].ToString();
                    pro.attachment4 = sdr["attachment4"].ToString();
                    pro.attachment5 = sdr["attachment5"].ToString();
                    pro.assigned_emp = sdr["assign_emp"].ToString();
                    pro.task_status = sdr["task_status"].ToString();
                    pro.commentTask = sdr["commentTask"].ToString();
                    task_list.Add(pro);
                }
            }
            con.Close();
            return task_list;
        }
        public List<emp_list> InsertTaskByReportingManager()
        {
            var userid = ((Login)HttpContext.Current.Session["Login"]).EmpId;
            TaskManage task = new TaskManage();
            List<emp_list> emp_list = new List<emp_list>();
            con.Open();
            SqlCommand cm = new SqlCommand("sp_TaskManage", con);
            cm.CommandType = CommandType.StoredProcedure;
            cm.Parameters.AddWithValue("@Action", "select_empList1");
            cm.Parameters.AddWithValue("@id", userid);
            SqlDataReader sd = cm.ExecuteReader();
            emp_list pr;
            if (sd.HasRows)
            {
                while (sd.Read())
                {
                    pr = new emp_list();
                    pr.emp_id = sd["id"].ToString();
                    pr.emp_name = sd["EmployeeName"].ToString();
                    emp_list.Add(pr);
                }
            }
            con.Close();
            return emp_list;
        }
        public int InsertTask(string Date = "", string Project = "", string Task = "", string Hour = "", string Description = "", string Status = "", string Remark = "")
        {
            int row = 0;
            int EmpId = 0;
            string EmpName = "";
            EmpId = ((Login)HttpContext.Current.Session["Login"]).EmpId;
            EmpName = ((Login)HttpContext.Current.Session["Login"]).EmployeeName;
            try
            {
                cmd = new SqlCommand("Sp_TaskSheet", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "Insert");
                con.Open();
                cmd.Parameters.AddWithValue("@Date", Date);
                cmd.Parameters.AddWithValue("@Project", Project);
                cmd.Parameters.AddWithValue("@Task", Task);
                cmd.Parameters.AddWithValue("@Hours", Hour);
                cmd.Parameters.AddWithValue("@Description", Description);
                cmd.Parameters.AddWithValue("@Status", Status);
                cmd.Parameters.AddWithValue("@Remark", Remark);
                cmd.Parameters.AddWithValue("@EmployeeId", EmpId);
                cmd.Parameters.AddWithValue("@EmployeeName", EmpName);
                row = cmd.ExecuteNonQuery();
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
            return row;
        }
        public List<TaskSheets> GetTasklistForEmployee()
        {
            List<TaskSheets> getlist = new List<TaskSheets>();
            TaskSheets obj = new TaskSheets();
            int EmpId = 0;
            EmpId = ((Login)HttpContext.Current.Session["Login"]).EmpId;
            string Today = "";
            try
            {
                cmd = new SqlCommand("Sp_TaskSheet", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@Action", "SelectForEmp");
                cmd.Parameters.AddWithValue("@EmployeeId", EmpId);
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        obj = new TaskSheets();
                        Today = DateTime.Today.ToString("dd-MM-yyyy");
                        obj.Date1 = rd["Date"].ToString();
                        if (Today == obj.Date1)
                        {
                            obj.DateStatus = "S";
                        }
                        else
                        {
                            obj.DateStatus = "F";
                        }
                        obj.Project1 = rd["Project"].ToString();
                        obj.Task1 = rd["Task"].ToString();
                        obj.Hour1 = rd["Hours"].ToString();
                        obj.Description1 = rd["Description"].ToString();
                        obj.Status1 = rd["Status"].ToString();
                        obj.Remark1 = rd["Remark"].ToString();
                        getlist.Add(obj);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return getlist;
        }
        public List<TaskSheets> GetTasklist()
        {
            List<TaskSheets> getlist = new List<TaskSheets>();
            TaskSheets obj = new TaskSheets();
            try
            {
                cmd = new SqlCommand("Sp_TaskSheet", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@Action", "Select");
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        obj = new TaskSheets();
                        obj.Date1 = rd["Date"].ToString();
                        obj.Project1 = rd["ProjectName"].ToString();
                        obj.Task1 = rd["Task"].ToString();
                        obj.Hour1 = rd["Hours"].ToString();
                        obj.Description1 = rd["Description"].ToString();
                        obj.Status1 = rd["Status"].ToString();
                        obj.Remark1 = rd["Remark"].ToString();
                        obj.EmpName = rd["EmployeeName"].ToString();
                        getlist.Add(obj);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return getlist;
        }
        public List<SelectListItem> BindProjectForQuatation(string CompanyId = "")
        {
            List<SelectListItem> getlist = new List<SelectListItem>();
            SelectListItem obj = new SelectListItem();
            try
            {
                cmd = new SqlCommand("Sp_Quatation", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@Action", "BindProject");
                cmd.Parameters.AddWithValue("@CompanyId", CompanyId);
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        obj = new SelectListItem();
                        obj.Value = rd["ProjectCode"].ToString();
                        obj.Text = rd["ProjectName"].ToString();
                        getlist.Add(obj);
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
            return getlist;
        }
        public int InsertServices(services obj)
        {
            int row = 0;
            try
            {
                cmd = new SqlCommand("Sp_Services", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "Insert");
                con.Open();
                cmd.Parameters.AddWithValue("@Code", obj.Code);
                cmd.Parameters.AddWithValue("@Name", obj.Name);
                row = cmd.ExecuteNonQuery();
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
            return row;
        }
        public int UpdateServices(services obj)
        {
            int row = 0;
            try
            {
                cmd = new SqlCommand("Sp_Services", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "Update");
                con.Open();
                cmd.Parameters.AddWithValue("@Id", obj.Id);
                cmd.Parameters.AddWithValue("@Name", obj.Name);
                row = cmd.ExecuteNonQuery();
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
            return row;
        }
        public List<services> BindServices()
        {
            List<services> getlist = new List<services>();
            services obj = new services();
            try
            {
                cmd = new SqlCommand("Sp_Services", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "Bind");
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        obj = new services();
                        obj.Id = Convert.ToInt32(rd["Id"]);
                        obj.Name = rd["Name"].ToString();
                        getlist.Add(obj);
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
            return getlist;
        }
        public services EditServices(int Id = 0)
        {

            services obj = new services();
            try
            {
                cmd = new SqlCommand("Sp_Services", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "select");
                cmd.Parameters.AddWithValue("@Id", Id);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        obj = new services();
                        obj.Id = Convert.ToInt32(rd["Id"]);
                        obj.Name = rd["Name"].ToString();

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
            return obj;
        }
        public int DeleteService(int Id = 0)
        {
            int row = 0;
            try
            {
                cmd = new SqlCommand("Sp_Services", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "Delete");
                con.Open();
                cmd.Parameters.AddWithValue("@Id", Id);
                row = cmd.ExecuteNonQuery();
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
            return row;
        }
        public List<SelectListItem> BindServicesForQuatation()
        {
            List<SelectListItem> getlist = new List<SelectListItem>();
            SelectListItem obj = new SelectListItem();
            try
            {
                cmd = new SqlCommand("Sp_Services", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "Bind");
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        obj = new SelectListItem();
                        obj.Value = rd["Code"].ToString();
                        obj.Text = rd["Name"].ToString();
                        getlist.Add(obj);
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
            return getlist;
        }

        public string GetMaxQuatationNo()
        {
            string row = "";
            cmd = new SqlCommand("GetQuatationNo", con);
            con.Open();
            row = cmd.ExecuteScalar().ToString();
            con.Close();
            return row;
        }
        public string GetMaxPINo()
        {
            string row = "";
            cmd = new SqlCommand("GetPINo", con);
            con.Open();
            row = cmd.ExecuteScalar().ToString();
            con.Close();
            return row;
        }
        public string GetMaxTaxInvoiceNo()
        {
            string row = "";
            cmd = new SqlCommand("GetTaxInvoiceNo", con);
            con.Open();
            row = cmd.ExecuteScalar().ToString();
            con.Close();
            return row;
        }
        public int InsertQuatation(quatation obj)
        {
            int row = 0;
            try
            {
                cmd = new SqlCommand("Sp_Quatation", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@Action", "InsertQuatation");
                cmd.Parameters.AddWithValue("@QuatationNo", obj.QuatationNo);
                cmd.Parameters.AddWithValue("@CompanyId", obj.CompanyId);
                cmd.Parameters.AddWithValue("@CompanyName", obj.CompanyName);
                cmd.Parameters.AddWithValue("@ProjectId", obj.ProjectId);
                cmd.Parameters.AddWithValue("@ProjectName", obj.ProjectName);
                cmd.Parameters.AddWithValue("@TotalAmount", obj.TotalAmount);
                cmd.Parameters.AddWithValue("@DiscountQty", obj.DiscountQty);
                cmd.Parameters.AddWithValue("@DiscountAmount", obj.DiscountAmount);
                cmd.Parameters.AddWithValue("@AfterDiscountAmount", obj.AfterDiscountAmount);
                cmd.Parameters.AddWithValue("@ProjectAmount", obj.ProjectAmount);
                cmd.Parameters.AddWithValue("@WorkScope", obj.WorkScope);
                cmd.Parameters.AddWithValue("@QuatationType", obj.Type);
                cmd.Parameters.AddWithValue("@ServiceId", obj.ServicesId);
              //  cmd.Parameters.AddWithValue("@AMC", obj.AMC);
              //  cmd.Parameters.AddWithValue("@Renewable", obj.Renewable);
                //cmd.Parameters.AddWithValue("@AMCAmount", obj.amcPopupData.Amount);
                //cmd.Parameters.AddWithValue("@AMCStartDate", obj.amcPopupData.StartDate);
                //cmd.Parameters.AddWithValue("@RenewableAmount", obj.renewablePopupData.Amount);
                //cmd.Parameters.AddWithValue("@RenewableStartDate", obj.renewablePopupData.StartDate);

                row = cmd.ExecuteNonQuery();
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
            return row;
        }
        public int QuatationProduct(string QuatationNo = "", string Services = "", string ServicesName = "", string Duration = "", string DurationTime = "", decimal Amount = 0.00M, string Description = "")
        {
            int row = 0;
            try
            {
                if (Services != null)
                {
                    cmd = new SqlCommand("Sp_Quatation", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.Parameters.AddWithValue("@Action", "InsertProduct");
                    cmd.Parameters.AddWithValue("@QuatationNo", QuatationNo);
                    cmd.Parameters.AddWithValue("@Services", Services);
                    cmd.Parameters.AddWithValue("@ServicesName", ServicesName);
                    cmd.Parameters.AddWithValue("@Duration", Duration);
                    cmd.Parameters.AddWithValue("@DurationTime", DurationTime);
                    //cmd.Parameters.AddWithValue("@StartDate", StartDate);
                    cmd.Parameters.AddWithValue("@Amount", Amount);
                    cmd.Parameters.AddWithValue("@Description", Description);
                    row = cmd.ExecuteNonQuery();
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
            return row;
        }
        public List<quatation> GetQuatationList()
        {
            List<quatation> gtlst = new List<quatation>();
            quatation obj = new quatation();
            try
            {
                cmd = new SqlCommand("Sp_Quatation", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "BindQuatation");
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        obj = new quatation();
                        obj.QuatationNo = rd["QuatationNo"].ToString();
                        obj.ProjectName = rd["ProjectName"].ToString();
                        obj.CompanyName = rd["CompanyName"].ToString();
                        //obj.ContactNo = rd["ContectNo"].ToString();
                        obj.TotalAmount = Convert.ToDecimal(rd["TotalAmount"]);
                        obj.DiscountQty = Convert.ToDecimal(rd["DiscountQty"]);
                        obj.DiscountAmount = Convert.ToDecimal(rd["DiscountAmount"]);
                        obj.AfterDiscountAmount = Convert.ToDecimal(rd["AfterDiscountAmount"]);
                        obj.Status = Convert.ToInt32(rd["Status"]);
                        obj.Type = rd["QuatationType"].ToString();
                        obj.WorkScope = rd["WorkScope"].ToString();
                        obj.ServicesId = rd["service"].ToString();
                        gtlst.Add(obj);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return gtlst;
        }
        public int DeleteQuatation(string QuatationNo = "")
        {
            int row = 0;
            try
            {
                cmd = new SqlCommand("Sp_Quatation", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "DeleteQuatation");
                con.Open();
                cmd.Parameters.AddWithValue("@QuatationNo", QuatationNo);
                row = cmd.ExecuteNonQuery();
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
            return row;
        }
        public int DeleteQuatationProduct(string QuatationNo = "")
        {
            int row = 0;
            try
            {
                cmd = new SqlCommand("Sp_Quatation", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "DeleteQuatationProduct");
                con.Open();
                cmd.Parameters.AddWithValue("@QuatationNo", QuatationNo);
                row = cmd.ExecuteNonQuery();
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
            return row;
        }
        public quatation EditQuatation(string Quatation = "")
        {
            quatation obj = new quatation();
            try
            {
                cmd = new SqlCommand("Sp_Quatation", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@Action", "EditQuatation");
                cmd.Parameters.AddWithValue("@QuatationNo", Quatation);
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        obj = new quatation();
                        obj.QuatationNo = rd["QuatationNo"].ToString();
                        obj.CompanyId = rd["CompanyId"].ToString();
                        // obj.ProjectId = rd["ProjectId"].ToString();
                        obj.ProjectName = rd["ProjectName"].ToString();
                        obj.ProjectAmount = Convert.ToDecimal(rd["ProjectAmount"]);
                        obj.TotalAmount = Convert.ToDecimal(rd["TotalAmount"]);
                        obj.DiscountQty = Convert.ToDecimal(rd["DiscountQty"]);
                        obj.DiscountAmount = Convert.ToDecimal(rd["DiscountAmount"]);
                        obj.AfterDiscountAmount = Convert.ToDecimal(rd["AfterDiscountAmount"]);
                        obj.WorkScope = rd["WorkScope"].ToString();
                        obj.Type = rd["QuatationType"].ToString();
                        obj.ServicesId = rd["ServiceId"].ToString();
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
            return obj;
        }
        public List<quatation> EditQuatationProduct(string Quatation = "")
        {
            quatation obj = new quatation();
            List<quatation> list = new List<quatation>();
            try
            {
                cmd = new SqlCommand("Sp_Quatation", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@Action", "EditQuatationProduct");
                cmd.Parameters.AddWithValue("@QuatationNo", Quatation);
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        obj = new quatation();
                        if (rd["Services"] != DBNull.Value)
                        {
                            obj.Services1 = rd["Services"].ToString();
                        }
                        if (rd["Duration"] != DBNull.Value)
                        {
                            obj.Duration1 = rd["Duration"].ToString();
                        }
                        if (rd["Amount"] != DBNull.Value)
                        {
                            obj.Amount1 = Convert.ToDecimal(rd["Amount"]);
                        }
                        if (rd["Description"] != DBNull.Value)
                        {
                            obj.Description1 = rd["Description"].ToString();
                        }
                        list.Add(obj);
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
            return list;
        }

        //edit quatation for software
        public List<quatation> EditQuatationProductSoftware(string Quatation = "")
        {
            quatation obj = new quatation();
            List<quatation> list = new List<quatation>();
            try
            {
                cmd = new SqlCommand("Sp_Quatation", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@Action", "EditQuatationSProduct");
                cmd.Parameters.AddWithValue("@QuatationNo", Quatation);
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        obj = new quatation();

                        if (rd["SoftwareName"] != DBNull.Value)
                        {
                            obj.SoftName = rd["SoftwareName"].ToString();
                        }
                        if (rd["Amount"] != DBNull.Value)
                        {
                            obj.SoftAmount = rd["Amount"].ToString();
                        }
                        if (rd["Unit"] != DBNull.Value)
                        {
                            obj.SoftUnit = rd["Unit"].ToString();
                        }
                        if (rd["Description"] != DBNull.Value)
                        {
                            obj.SoftDescription = rd["Description"].ToString();
                        }
                        if (rd["TotalAmount"] != DBNull.Value)
                        {
                            obj.SoftTotal = rd["TotalAmount"].ToString();
                        }
                        list.Add(obj);
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
            return list;
        }
        //edit quatation for hardware
        public List<quatation> EditQuatationProductHardware(string Quatation = "")
        {
            quatation obj = new quatation();
            List<quatation> list = new List<quatation>();
            try
            {
                cmd = new SqlCommand("Sp_Quatation", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@Action", "EditQuatationHProduct");
                cmd.Parameters.AddWithValue("@QuatationNo", Quatation);
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        obj = new quatation();
                        if (rd["ProjectName"] != DBNull.Value)
                        {
                            obj.ProductName = rd["ProjectName"].ToString();
                        }
                        if (rd["HDescription"] != DBNull.Value)
                        {
                            obj.HNaration = rd["HDescription"].ToString();
                        }
                        if (rd["Amount"] != DBNull.Value)
                        {
                            obj.HUnitAmount = rd["Amount"].ToString();
                        }
                        if (rd["Unit"] != DBNull.Value)
                        {
                            obj.HwUnit = rd["Unit"].ToString();
                        }
                        if (rd["TotalAmount"] != DBNull.Value)
                        {
                            obj.HwTotal = rd["TotalAmount"].ToString();
                        }
                        if (rd["AMC"] != DBNull.Value)
                        {
                            obj.Amc1 = rd["AMC"].ToString();
                        }
                        if (rd["Renewable"] != DBNull.Value)
                        {
                            obj.Renew = rd["Renewable"].ToString();
                        }
                        if (rd["amc_amount"] != DBNull.Value)
                        {
                            obj.AMC_amount = Convert.ToDecimal(rd["amc_amount"].ToString());
                        }
                        if (rd["amc_date"] != DBNull.Value)
                        {
                            obj.AMC_date = rd["amc_date"].ToString();
                        }
                        if (rd["rew_amount"] != DBNull.Value)
                        {
                            obj.REW_amount = Convert.ToDecimal(rd["rew_amount"].ToString());
                        }
                        if (rd["rew_date"] != DBNull.Value)
                        {
                            obj.REW_date = rd["rew_date"].ToString();
                        }
                        list.Add(obj);
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
            return list;
        }
        public int UpdateQuatation(quatation obj)
        {
            int row = 0;
            try
            {
                cmd = new SqlCommand("Sp_Quatation", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@Action", "UpdateQuatation");
                cmd.Parameters.AddWithValue("@QuatationNo", obj.QuatationNo);
                cmd.Parameters.AddWithValue("@CompanyId", obj.CompanyId);
                cmd.Parameters.AddWithValue("@CompanyName", obj.CompanyName);
                cmd.Parameters.AddWithValue("@ProjectId", obj.ProjectId);
                cmd.Parameters.AddWithValue("@ProjectName", obj.ProjectName);
                cmd.Parameters.AddWithValue("@TotalAmount", obj.TotalAmount);
                cmd.Parameters.AddWithValue("@DiscountQty", obj.DiscountQty);
                cmd.Parameters.AddWithValue("@DiscountAmount", obj.DiscountAmount);
                cmd.Parameters.AddWithValue("@AfterDiscountAmount", obj.AfterDiscountAmount);
                cmd.Parameters.AddWithValue("@ProjectAmount", obj.ProjectAmount);
                cmd.Parameters.AddWithValue("@WorkScope", obj.WorkScope);
                cmd.Parameters.AddWithValue("@QuatationType", obj.Type);
                cmd.Parameters.AddWithValue("@ServiceId", obj.ServicesId);
                row = cmd.ExecuteNonQuery();
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
            return row;
        }
        public List<Company> GetCompanyDetail()
        {
            List<Company> gtlst = new List<Company>();
            Company obj = new Company();
            try
            {
                cmd = new SqlCommand("Sp_Invoice", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "CompanyDetail");
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        obj = new Company();
                        obj.Id = Convert.ToInt32(rd["Id"]);
                        obj.CompanyName = rd["CompanyName"].ToString();
                        obj.Email = rd["Email"].ToString();
                        obj.Phone = rd["Phone"].ToString();
                        obj.GSTNo = rd["GSTNo"].ToString();
                        obj.TINNO = rd["TINNO"].ToString();
                        obj.PanNo = rd["PanNo"].ToString();
                        obj.AreaCode = rd["AreaCode"].ToString();
                        obj.City = rd["City"].ToString();
                        obj.State = rd["State"].ToString();
                        obj.Address = rd["Address"].ToString();
                        obj.StateCode = rd["StateCode"].ToString();
                        obj.RegistrationNo = rd["RegistrationNo"].ToString();
                        obj.Image = rd["CompanyName"].ToString();
                        gtlst.Add(obj);
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
            return gtlst;
        }
        public quatation GetQuatationPreview(string QuatationNo = "")
        {
            quatation obj = new quatation();
            try
            {
                cmd = new SqlCommand("Sp_Invoice", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "QuatationDetail");
                cmd.Parameters.AddWithValue("@QuatationNo", QuatationNo);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        obj = new quatation();
                        obj.DiscountQty = Convert.ToDecimal(rd["DiscountQty"]);
                        obj.DiscountAmount = Convert.ToDecimal(rd["DiscountAmount"]);
                        obj.AfterDiscountAmount = Convert.ToDecimal(rd["AfterDiscountAmount"]);
                        obj.ProjectAmount = Convert.ToDecimal(rd["ProjectAmount"]);
                        obj.TotalAmount = Convert.ToDecimal(rd["TotalAmount"]);
                        obj.ClientName = rd["CompanyName"].ToString();
                        obj.Address = rd["Address"].ToString();
                        obj.ClientEmail = rd["EmailId"].ToString();
                        obj.ClientPhone = rd["ContactNo"].ToString();
                        if (rd["GSTNo"] != DBNull.Value)
                        {
                            obj.ClientGST = rd["GSTNo"].ToString();
                        }
                        else
                        {
                            obj.ClientGST = "N/A";
                        }
                        obj.ClientPAN = rd["PANNo"].ToString();
                        obj.ClientStatecode = rd["Statecode"].ToString();
                        obj.QuatationNo = rd["QuatationNo"].ToString();
                        obj.Date1 = rd["Date"].ToString();
                        obj.WorkScope = rd["WorkScope"].ToString();
                        obj.Type = rd["QuatationType"].ToString();
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
            return obj;
        }
        public List<quatation> GetQuatationProduct(string QuatationNo = "")
        {
            List<quatation> getproduct = new List<quatation>();
            quatation obj = new quatation();
            try
            {
                cmd = new SqlCommand("Sp_Invoice", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "QuatationProduct");
                cmd.Parameters.AddWithValue("@QuatationNo", QuatationNo);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        obj = new quatation();
                        if (rd["ServicesName"] != DBNull.Value)
                        {
                            obj.Services1 = rd["ServicesName"].ToString();
                        }
                        if (rd["DurationTime"] != DBNull.Value)
                        {
                            obj.Duration1 = rd["DurationTime"].ToString();
                        }
                        if (rd["Amount"] != DBNull.Value)
                        {
                            obj.Amount1 = Convert.ToDecimal(rd["Amount"]);
                        }
                        if (rd["Description"] != DBNull.Value)
                        {
                            obj.Description1 = rd["Description"].ToString();
                        }

                        //if (rd["ProductName"] != DBNull.Value)
                        //{

                        //    obj.ProductName = rd["ProductName"].ToString();
                        //}
                        //if (rd["Naration"] != DBNull.Value)
                        //{
                        //    obj.HNaration = rd["Naration"].ToString();

                        //}
                        //if (rd["HAmount"] != DBNull.Value)
                        //{
                        //    obj.HUnitAmount = rd["HAmount"].ToString();
                        //}
                        //if (rd["HUnit"] != DBNull.Value)
                        //{
                        //    obj.HwUnit = rd["HUnit"].ToString();
                        //}
                        //if (rd["HTotal"] != DBNull.Value)
                        //{
                        //    obj.HwTotal = rd["HTotal"].ToString();
                        //}

                        //if (rd["SoftwareName"] != DBNull.Value)
                        //{
                        //    obj.SoftName = rd["SoftwareName"].ToString();
                        //}
                        //if (rd["Amount"] != DBNull.Value)
                        //{
                        //    obj.SoftAmount = rd["Amount"].ToString();
                        //}
                        //if (rd["Unit"] != DBNull.Value)
                        //{
                        //    obj.SoftUnit = rd["Unit"].ToString();
                        //}
                        //if (rd["Description"] != DBNull.Value)
                        //{
                        //    obj.SoftDescription = rd["Description"].ToString();
                        //}
                        //if (rd["Total"] != DBNull.Value)
                        //{
                        //    obj.SoftTotal = rd["TotalAmount"].ToString();
                        //}
                        getproduct.Add(obj);
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
            return getproduct;
        }
        //get software produts list
        public List<quatation> GetQuatationProductsoftware(string QuatationNo = "")
        {
            List<quatation> getproduct = new List<quatation>();
            quatation obj = new quatation();
            try
            {
                cmd = new SqlCommand("Sp_Invoice", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "QuatationProductsoftware");
                cmd.Parameters.AddWithValue("@QuatationNo", QuatationNo);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        obj = new quatation();

                        if (rd["SoftwareName"] != DBNull.Value)
                        {
                            obj.SoftName = rd["SoftwareName"].ToString();
                        }
                        if (rd["Amount"] != DBNull.Value)
                        {
                            obj.SoftAmount = rd["Amount"].ToString();
                        }
                        if (rd["Unit"] != DBNull.Value)
                        {
                            obj.SoftUnit = rd["Unit"].ToString();
                        }
                        if (rd["Description"] != DBNull.Value)
                        {
                            obj.SoftDescription = rd["Description"].ToString();
                        }
                        if (rd["TotalAmount"] != DBNull.Value)
                        {
                            obj.SoftTotal = rd["TotalAmount"].ToString();
                        }
                        getproduct.Add(obj);
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
            return getproduct;
        }
        //get hardware products list
        public List<quatation> GetQuatationProductHardware(string QuatationNo = "")
        {
            List<quatation> getproduct = new List<quatation>();
            quatation obj = new quatation();
            try
            {
                cmd = new SqlCommand("Sp_Invoice", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "QuatationProductHardware");
                cmd.Parameters.AddWithValue("@QuatationNo", QuatationNo);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        obj = new quatation();

                        if (rd["ProjectName"] != DBNull.Value)
                        {
                            obj.ProductName = rd["ProjectName"].ToString();
                        }
                        if (rd["HDescription"] != DBNull.Value)
                        {
                            obj.HNaration = rd["HDescription"].ToString();

                        }
                        if (rd["Amount"] != DBNull.Value)
                        {
                            obj.HUnitAmount = rd["Amount"].ToString();
                        }
                        if (rd["Unit"] != DBNull.Value)
                        {
                            obj.HwUnit = rd["Unit"].ToString();
                        }
                        if (rd["TotalAmount"] != DBNull.Value)
                        {
                            obj.HwTotal = rd["TotalAmount"].ToString();
                        }
                        getproduct.Add(obj);
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
            return getproduct;
        }

        public int InsertPI(quatation obj)
        {
            int row = 0;
            try
            {
                cmd = new SqlCommand("Sp_PIMaster", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@Action", "InsertPI");
                cmd.Parameters.AddWithValue("@QuatationNo", obj.QuatationNo);
                cmd.Parameters.AddWithValue("@PINo", obj.PINo);
                cmd.Parameters.AddWithValue("@CompanyId", obj.CompanyId);
                cmd.Parameters.AddWithValue("@CompanyName", obj.CompanyName);
                cmd.Parameters.AddWithValue("@ProjectId", obj.ProjectId);
                cmd.Parameters.AddWithValue("@ProjectName", obj.ProjectName);
                cmd.Parameters.AddWithValue("@TotalAmount", obj.TotalAmount);
                cmd.Parameters.AddWithValue("@Tax", obj.Tax);
                cmd.Parameters.AddWithValue("@TaxAmount", obj.TaxAmount);
                cmd.Parameters.AddWithValue("@AfterTaxAmount", obj.AfterTaxAmount);
                cmd.Parameters.AddWithValue("@ProjectAmount", obj.ProjectAmount);
                cmd.Parameters.AddWithValue("@IGST", obj.IGST);
                cmd.Parameters.AddWithValue("@CGST", obj.CGST);
                cmd.Parameters.AddWithValue("@SGST", obj.SGST);
                cmd.Parameters.AddWithValue("@IgstAmount", obj.IgstAmount);
                cmd.Parameters.AddWithValue("@CgstAmount", obj.CgstAmount);
                cmd.Parameters.AddWithValue("@SgstAmount", obj.SgstAmount);
                cmd.Parameters.AddWithValue("@Taxtype", obj.Taxtype);
                cmd.Parameters.AddWithValue("@Type", obj.Type);
                row = cmd.ExecuteNonQuery();
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
            return row;
        }
        public int PIProduct(string PINo = "", string Services = "", string ServicesName = "", string Duration = "", string DurationTime = "", string StartDate = "", decimal Amount = 0.00M, string Description = "", string CompanyId = "", string ProjectId = "")
        {
            int row = 0;
            try
            {
                cmd = new SqlCommand("Sp_PIMaster", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@Action", "InsertProduct");
                cmd.Parameters.AddWithValue("@PINo", PINo);
                cmd.Parameters.AddWithValue("@Services", Services);
                cmd.Parameters.AddWithValue("@ServicesName", ServicesName);
                cmd.Parameters.AddWithValue("@Duration", Duration);
                cmd.Parameters.AddWithValue("@DurationTime", DurationTime);
                cmd.Parameters.AddWithValue("@StartDate", StartDate);
                cmd.Parameters.AddWithValue("@Amount", Amount);
                cmd.Parameters.AddWithValue("@Description", Description);
                cmd.Parameters.AddWithValue("@CompanyId", CompanyId);
                cmd.Parameters.AddWithValue("@ProjectId", ProjectId);
                row = cmd.ExecuteNonQuery();
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
            return row;
        }
        public List<performa> GetPIList()
        {
            List<performa> list = new List<performa>();
            performa obj = new performa();
            try
            {
                cmd = new SqlCommand("Sp_PIMaster", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "SelectPI");
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        obj = new performa();
                        obj.PINo = rd["PINo"].ToString();
                        obj.CompanyId = rd["CompanyId"].ToString();
                        obj.ProjectId = rd["ProjectId"].ToString();
                        obj.ProjectName = rd["ProjectName"].ToString();
                        obj.CompanyName = rd["CompanyName"].ToString();
                        obj.TotalAmount = Convert.ToDecimal(rd["TotalAmount"]);
                        obj.AfterTaxAmount = Convert.ToDecimal(rd["AfterTaxAmount"]);
                        obj.Status = Convert.ToInt32(rd["Status"]);
                        //obj.Ladger = Convert.ToInt32(rd["Ladger"]);
                        list.Add(obj);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return list;
        }
        public int DeletePI(string PINo = "")
        {
            int row = 0;
            try
            {
                cmd = new SqlCommand("Sp_PIMaster", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "DeletePI");
                con.Open();
                cmd.Parameters.AddWithValue("@PINo", PINo);
                row = cmd.ExecuteNonQuery();
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
            return row;
        }
        public int DeletePIProduct(string PINo = "")
        {
            int row = 0;
            try
            {
                cmd = new SqlCommand("Sp_PIMaster", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "DeletePIProduct");
                con.Open();
                cmd.Parameters.AddWithValue("@PINo", PINo);
                row = cmd.ExecuteNonQuery();
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
            return row;
        }
        public List<quatation> GetPIProduct(string PINo = "")
        {
            List<quatation> getproduct = new List<quatation>();
            quatation obj = new quatation();
            try
            {
                cmd = new SqlCommand("Sp_PIMaster", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "PIProduct");
                cmd.Parameters.AddWithValue("@PINo", PINo);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        obj = new quatation();
                        if (rd["Services"] != DBNull.Value)
                        {
                            obj.Services1 = rd["Services"].ToString();
                        }
                        if (rd["ServicesName"] != DBNull.Value)
                        {
                            obj.Servicesnm = rd["ServicesName"].ToString();
                        }
                        if (rd["Duration"] != DBNull.Value)
                        {
                            obj.Duration1 = rd["Duration"].ToString();
                        }
                        if (rd["DurationTime"] != DBNull.Value)
                        {
                            obj.Durationtm = rd["DurationTime"].ToString();
                        }
                        if (rd["Amount"] != DBNull.Value)
                        {
                            obj.Amount1 = Convert.ToDecimal(rd["Amount"]);
                        }
                        if (rd["Description"] != DBNull.Value)
                        {
                            obj.Description1 = rd["Description"].ToString();
                        }
                        if (rd["StartDate"] != DBNull.Value)
                        {
                            obj.Date1 = rd["StartDate"].ToString();
                        }
                        //if (rd["ProductName"] != DBNull.Value)
                        //{
                        //    obj.ProductName = rd["ProductName"].ToString();
                        //}
                        //if (rd["Naration"] != DBNull.Value)
                        //{
                        //    obj.HNaration = rd["Naration"].ToString();

                        //}
                        //if (rd["HAmount"] != DBNull.Value)
                        //{
                        //    obj.HUnitAmount = rd["HAmount"].ToString();
                        //}
                        //if (rd["HUnit"] != DBNull.Value)
                        //{
                        //    obj.HwUnit = rd["HUnit"].ToString();
                        //}
                        //if (rd["HTotal"] != DBNull.Value)
                        //{
                        //    obj.HwTotal = rd["HTotal"].ToString();
                        //}

                        //if (rd["SoftwareName"] != DBNull.Value)
                        //{
                        //    obj.SoftName = rd["SoftwareName"].ToString();
                        //}
                        //if (rd["SAmount"] != DBNull.Value)
                        //{
                        //    obj.SoftAmount = rd["SAmount"].ToString();
                        //}
                        //if (rd["SUnit"] != DBNull.Value)
                        //{
                        //    obj.SoftUnit = rd["SUnit"].ToString();
                        //}
                        //if (rd["SDescription"] != DBNull.Value)
                        //{
                        //    obj.SoftDescription = rd["SDescription"].ToString();
                        //}
                        //if (rd["STotal"] != DBNull.Value)
                        //{
                        //    obj.SoftTotal = rd["STotal"].ToString();
                        //}
                        getproduct.Add(obj);
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
            return getproduct;
        }
        //get piproduct for software
        public List<quatation> GetPIProductS(string PINo = "")
        {
            List<quatation> getproduct = new List<quatation>();
            quatation obj = new quatation();
            try
            {
                cmd = new SqlCommand("[Sp_PIMaster]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "PIProductSoftware");
                cmd.Parameters.AddWithValue("@PINo", PINo);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        obj = new quatation();
                        if (rd["SoftwareName"] != DBNull.Value)
                        {
                            obj.SoftName = rd["SoftwareName"].ToString();
                        }
                        if (rd["Amount"] != DBNull.Value)
                        {
                            obj.SoftAmount = rd["Amount"].ToString();
                        }
                        if (rd["Unit"] != DBNull.Value)
                        {
                            obj.SoftUnit = rd["Unit"].ToString();
                        }
                        if (rd["Description"] != DBNull.Value)
                        {
                            obj.SoftDescription = rd["Description"].ToString();
                        }
                        if (rd["TotalAmount"] != DBNull.Value)
                        {
                            obj.SoftTotal = rd["TotalAmount"].ToString();
                        }
                        getproduct.Add(obj);
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
            return getproduct;
        }
        //gtepi product for hardware
        public List<quatation> GetPIProductH(string PINo = "")
        {
            List<quatation> getproduct = new List<quatation>();
            quatation obj = new quatation();
            try
            {
                cmd = new SqlCommand("[Sp_PIMaster]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "PIProductHardware");
                cmd.Parameters.AddWithValue("@PINo", PINo);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        obj = new quatation();
                        if (rd["ProductName"] != DBNull.Value)
                        {
                            obj.ProductName = rd["ProductName"].ToString();
                        }
                        if (rd["HDescription"] != DBNull.Value)
                        {
                            obj.HNaration = rd["HDescription"].ToString();

                        }
                        if (rd["Amount"] != DBNull.Value)
                        {
                            obj.HUnitAmount = rd["Amount"].ToString();
                        }
                        if (rd["Unit"] != DBNull.Value)
                        {
                            obj.HwUnit = rd["Unit"].ToString();
                        }
                        if (rd["TotalAmount"] != DBNull.Value)
                        {
                            obj.HwTotal = rd["TotalAmount"].ToString();
                        }

                        getproduct.Add(obj);
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
            return getproduct;
        }

        //get performa invoice for software products
        public List<quatation> GetPIProductSoftware(string PINo = "")
        {
            List<quatation> getproduct = new List<quatation>();
            quatation obj = new quatation();
            try
            {
                cmd = new SqlCommand("Sp_PIMaster", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "PIProductSoftware");
                cmd.Parameters.AddWithValue("@PINo", PINo);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        obj = new quatation();
                        if (rd["SoftwareName"] != DBNull.Value)
                        {
                            obj.SoftName = rd["SoftwareName"].ToString();
                        }
                        if (rd["Amount"] != DBNull.Value)
                        {
                            obj.SoftAmount = rd["Amount"].ToString();
                        }
                        if (rd["Unit"] != DBNull.Value)
                        {
                            obj.SoftUnit = rd["Unit"].ToString();
                        }
                        if (rd["Description"] != DBNull.Value)
                        {
                            obj.SoftDescription = rd["Description"].ToString();
                        }
                        if (rd["TotalAmount"] != DBNull.Value)
                        {
                            obj.SoftTotal = rd["TotalAmount"].ToString();
                        }
                        getproduct.Add(obj);
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
            return getproduct;
        }
        //get PI for Hardware Products
        public List<quatation> GetPIProductHardware(string PINo = "")
        {
            List<quatation> getproduct = new List<quatation>();
            quatation obj = new quatation();
            try
            {
                cmd = new SqlCommand("Sp_PIMaster", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "PIProductHardware");
                cmd.Parameters.AddWithValue("@PINo", PINo);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        obj = new quatation();

                        if (rd["ProductName"] != DBNull.Value)
                        {
                            obj.ProductName = rd["ProductName"].ToString();
                        }
                        if (rd["HDescription"] != DBNull.Value)
                        {
                            obj.HNaration = rd["HDescription"].ToString();

                        }
                        if (rd["Amount"] != DBNull.Value)
                        {
                            obj.HUnitAmount = rd["Amount"].ToString();
                        }
                        if (rd["Unit"] != DBNull.Value)
                        {
                            obj.HwUnit = rd["Unit"].ToString();
                        }
                        if (rd["TotalAmount"] != DBNull.Value)
                        {
                            obj.HwTotal = rd["TotalAmount"].ToString();
                        }
                        getproduct.Add(obj);
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
            return getproduct;
        }
        public quatation GetPIPreview(string PINo = "")
        {
            quatation obj = new quatation();
            try
            {
                cmd = new SqlCommand("Sp_Invoice", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "PIDetail");
                cmd.Parameters.AddWithValue("@PINo", PINo);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        obj = new quatation();
                        obj.Tax = Convert.ToDecimal(rd["Tax"]);
                        obj.TaxAmount = Convert.ToDecimal(rd["TaxAmount"]);
                        obj.AfterTaxAmount = Convert.ToDecimal(rd["AfterTaxAmount"]);
                        obj.ProjectAmount = Convert.ToDecimal(rd["ProjectAmount"]);
                        obj.TotalAmount = Convert.ToDecimal(rd["TotalAmount"]);
                        obj.ClientName = rd["CompanyName"].ToString();
                        obj.Address = rd["Address"].ToString();
                        obj.ClientEmail = rd["EmailId"].ToString();
                        obj.ClientPhone = rd["ContactNo"].ToString();
                        if (rd["GSTNo"] != DBNull.Value)
                        {
                            obj.ClientGST = rd["GSTNo"].ToString();
                        }
                        else
                        {
                            obj.ClientGST = "N/A";
                        }
                        obj.ClientPAN = rd["PANNo"].ToString();
                        obj.ClientStatecode = rd["Statecode"].ToString();
                        obj.PINo = rd["PINo"].ToString();
                        obj.Date1 = rd["Date"].ToString();
                        obj.IGST = Convert.ToInt32(rd["IGST"]);
                        obj.IgstAmount = Convert.ToDecimal(rd["IgstAmount"]);
                        obj.CGST = Convert.ToInt32(rd["CGST"]);
                        obj.CgstAmount = Convert.ToDecimal(rd["CgstAmount"]);
                        obj.SGST = Convert.ToInt32(rd["SGST"]);
                        obj.SgstAmount = Convert.ToDecimal(rd["SgstAmount"]);
                        obj.Taxtype = rd["Taxtype"].ToString();
                        obj.Type = rd["Type"].ToString();
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
            return obj;
        }
        public int UpdatePI(quatation obj)
        {
            int row = 0;
            try
            {
                cmd = new SqlCommand("Sp_PIMaster", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@Action", "UpdatePI");
                cmd.Parameters.AddWithValue("@PINo", obj.PINo);
                cmd.Parameters.AddWithValue("@CompanyId", obj.CompanyId);
                cmd.Parameters.AddWithValue("@CompanyName", obj.CompanyName);
                cmd.Parameters.AddWithValue("@ProjectId", obj.ProjectId);
                cmd.Parameters.AddWithValue("@ProjectName", obj.ProjectName);
                cmd.Parameters.AddWithValue("@TotalAmount", obj.TotalAmount);
                cmd.Parameters.AddWithValue("@Tax", obj.Tax);
                cmd.Parameters.AddWithValue("@TaxAmount", obj.TaxAmount);
                cmd.Parameters.AddWithValue("@AfterTaxAmount", obj.AfterTaxAmount);
                cmd.Parameters.AddWithValue("@ProjectAmount", obj.ProjectAmount);
                cmd.Parameters.AddWithValue("@IGST", obj.IGST);
                cmd.Parameters.AddWithValue("@CGST", obj.CGST);
                cmd.Parameters.AddWithValue("@SGST", obj.SGST);
                cmd.Parameters.AddWithValue("@IgstAmount", obj.IgstAmount);
                cmd.Parameters.AddWithValue("@CgstAmount", obj.CgstAmount);
                cmd.Parameters.AddWithValue("@SgstAmount", obj.SgstAmount);
                cmd.Parameters.AddWithValue("@Taxtype", obj.Taxtype);
                row = cmd.ExecuteNonQuery();
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
            return row;
        }
        public quatation EditPI(string PINo = "")
        {
            quatation obj = new quatation();
            try
            {
                cmd = new SqlCommand("Sp_PIMaster", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@Action", "EDITPI");
                cmd.Parameters.AddWithValue("@PINo", PINo);
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        obj = new quatation();
                        obj.PINo = rd["PINo"].ToString();
                        obj.CompanyId = rd["CompanyId"].ToString();
                        obj.ProjectId = rd["ProjectId"].ToString();
                        obj.ProjectAmount = Convert.ToDecimal(rd["ProjectAmount"]);
                        obj.TotalAmount = Convert.ToDecimal(rd["TotalAmount"]);
                        obj.Tax = Convert.ToInt32(rd["Tax"]);
                        obj.TaxAmount = Convert.ToDecimal(rd["TaxAmount"]);
                        obj.AfterTaxAmount = Convert.ToDecimal(rd["AfterTaxAmount"]);
                        obj.Taxtype = rd["Taxtype"].ToString();
                        obj.Type = rd["Type"].ToString();
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
            return obj;
        }
        public int UpdatePerformaInvoice(performa obj)
        {
            int row = 0;
            try
            {
                cmd = new SqlCommand("Sp_Invoice", con);
                cmd.CommandType = CommandType.StoredProcedure;
                if (obj.Status == 1)
                {
                    cmd.Parameters.AddWithValue("@Action", "UpdatePerformaInvoice");
                    con.Open();
                    cmd.Parameters.AddWithValue("@Status", obj.Status);
                    cmd.Parameters.AddWithValue("@PINo", obj.PINo);
                    cmd.Parameters.AddWithValue("@TotalAmount", obj.TotalAmount);
                    cmd.Parameters.AddWithValue("@CompanyId", obj.CompanyId);
                    cmd.Parameters.AddWithValue("@CompanyName", obj.CompanyName);
                    cmd.Parameters.AddWithValue("@ProjectId", obj.ProjectId);
                    cmd.Parameters.AddWithValue("@ProjectName", obj.ProjectName);
                    row = cmd.ExecuteNonQuery();
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Action", "UpdatePerformaInvoiceReject");
                    con.Open();
                    cmd.Parameters.AddWithValue("@Status", obj.Status);
                    cmd.Parameters.AddWithValue("@PINo", obj.PINo);
                    row = cmd.ExecuteNonQuery();
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
            return row;
        }
        public List<performa> GetGanratedInvoiceList()
        {
            List<performa> list = new List<performa>();
            performa obj = new performa();
            try
            {
                cmd = new SqlCommand("Sp_InvoiceMaster", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "GanratedPIList");
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        obj = new performa();
                        obj.InvoiceNo = rd["InvoiceNo"].ToString();
                        obj.CompanyName = rd["CompanyName"].ToString();
                        obj.TotalAmount = Convert.ToDecimal(rd["TotalAmount"]);
                        obj.AfterTaxAmount = Convert.ToDecimal(rd["AfterTaxAmount"]);
                        list.Add(obj);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return list;
        }
        public performa TaxInvoiceDetails(string PINo = "")
        {
            performa obj = new performa();
            try
            {
                cmd = new SqlCommand("Sp_Invoice", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "TaxInvoiceDetail");
                cmd.Parameters.AddWithValue("@PINo", PINo);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        obj = new performa();
                        obj.Tax = Convert.ToDecimal(rd["Tax"]);
                        obj.TaxAmount = Convert.ToDecimal(rd["TaxAmount"]);
                        obj.AfterTaxAmount = Convert.ToDecimal(rd["AfterTaxAmount"]);
                        obj.ProjectAmount = Convert.ToDecimal(rd["ProjectAmount"]);
                        obj.TotalAmount = Convert.ToDecimal(rd["TotalAmount"]);
                        obj.ClientName = rd["CompanyName"].ToString();
                        obj.Address = rd["Address"].ToString();
                        obj.ClientEmail = rd["EmailId"].ToString();
                        obj.ClientPhone = rd["ContactNo"].ToString();
                        obj.ClientGST = rd["GSTNo"].ToString();
                        obj.ClientPAN = rd["PANNo"].ToString();
                        obj.ClientStatecode = rd["Statecode"].ToString();
                        obj.PINo = rd["PINo"].ToString();
                        obj.Date1 = rd["Date"].ToString();
                        obj.IGST = Convert.ToInt32(rd["IGST"]);
                        obj.IgstAmount = Convert.ToDecimal(rd["IgstAmount"]);
                        obj.CGST = Convert.ToInt32(rd["CGST"]);
                        obj.CgstAmount = Convert.ToDecimal(rd["CgstAmount"]);
                        obj.SGST = Convert.ToInt32(rd["SGST"]);
                        obj.SgstAmount = Convert.ToDecimal(rd["SgstAmount"]);
                        obj.Taxtype = rd["Taxtype"].ToString();
                        obj.TaxInvoiceNo = rd["TaxInvoiceNo"].ToString();
                        obj.TaxInvoiceDate = rd["TaxInvoiceDate"].ToString();
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
            return obj;
        }
        public int InsertTaxInvoice(string PINo = "", string TaxInvoiceNo = "")
        {
            int row = 0;
            try
            {
                cmd = new SqlCommand("Sp_Invoice", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "InsertTaxInvoice");
                con.Open();
                cmd.Parameters.AddWithValue("@PINo", PINo);
                cmd.Parameters.AddWithValue("@TaxInvoiceNo", TaxInvoiceNo);
                row = cmd.ExecuteNonQuery();
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
            return row;
        }
        public int InsertPurchase(Purchase obj)
        {
            int row = 0;
            try
            {
                cmd = new SqlCommand("Sp_Purchase", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "Insert");
                con.Open();
                cmd.Parameters.AddWithValue("@CompanyName", obj.CompanyName);
                cmd.Parameters.AddWithValue("@Date", obj.Date);
                cmd.Parameters.AddWithValue("@InvoiceNo", obj.InvoiceNo);
                cmd.Parameters.AddWithValue("@AmtBeforeTax", obj.AmtBeforeTax);
                cmd.Parameters.AddWithValue("@Address", obj.Address);
                cmd.Parameters.AddWithValue("@GSTNo", obj.GSTNo);
                cmd.Parameters.AddWithValue("@PANNo", obj.PANNo);
                cmd.Parameters.AddWithValue("@TaxType", obj.TaxType);
                cmd.Parameters.AddWithValue("@Tax", obj.Tax);
                cmd.Parameters.AddWithValue("@AmtAfterTax", obj.AmtAfterTax);
                cmd.Parameters.AddWithValue("@UploadInvoicePath", obj.UploadInvoicePath);
                cmd.Parameters.AddWithValue("@IGST", obj.IGST);
                cmd.Parameters.AddWithValue("@CGST", obj.CGST);
                cmd.Parameters.AddWithValue("@SGST", obj.SGST);
                cmd.Parameters.AddWithValue("@CGSTAmount", obj.CGSTAmount);
                cmd.Parameters.AddWithValue("@SGSTAmount", obj.SGSTAmount);
                cmd.Parameters.AddWithValue("@IGSTAmount", obj.IGSTAmount);
                cmd.Parameters.AddWithValue("@TaxAmount", obj.TaxAmount);
                row = cmd.ExecuteNonQuery();
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
            return row;
        }
        public int UpdatePurchase(Purchase obj)
        {
            int row = 0;
            try
            {
                cmd = new SqlCommand("Sp_Purchase", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "Update");
                con.Open();
                cmd.Parameters.AddWithValue("@Id", obj.Id);
                cmd.Parameters.AddWithValue("@CompanyName", obj.CompanyName);
                cmd.Parameters.AddWithValue("@Date", obj.Date);
                cmd.Parameters.AddWithValue("@InvoiceNo", obj.InvoiceNo);
                cmd.Parameters.AddWithValue("@AmtBeforeTax", obj.AmtBeforeTax);
                cmd.Parameters.AddWithValue("@Address", obj.Address);
                cmd.Parameters.AddWithValue("@GSTNo", obj.GSTNo);
                cmd.Parameters.AddWithValue("@PANNo", obj.PANNo);
                cmd.Parameters.AddWithValue("@TaxType", obj.TaxType);
                cmd.Parameters.AddWithValue("@Tax", obj.Tax);
                cmd.Parameters.AddWithValue("@AmtAfterTax", obj.AmtAfterTax);
                cmd.Parameters.AddWithValue("@UploadInvoicePath", obj.UploadInvoicePath);
                cmd.Parameters.AddWithValue("@IGST", obj.IGST);
                cmd.Parameters.AddWithValue("@CGST", obj.CGST);
                cmd.Parameters.AddWithValue("@SGST", obj.SGST);
                cmd.Parameters.AddWithValue("@CGSTAmount", obj.CGSTAmount);
                cmd.Parameters.AddWithValue("@SGSTAmount", obj.SGSTAmount);
                cmd.Parameters.AddWithValue("@IGSTAmount", obj.IGSTAmount);
                cmd.Parameters.AddWithValue("@TaxAmount", obj.TaxAmount);
                //cmd.Parameters.AddWithValue("@GSTNo", obj.GSTNo);
                row = cmd.ExecuteNonQuery();
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
            return row;
        }
        public List<Purchase> GetPurchaseList()
        {
            List<Purchase> gtlst = new List<Purchase>();
            Purchase obj = new Purchase();
            try
            {
                cmd = new SqlCommand("Sp_Purchase", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "select");
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        obj = new Purchase();
                        obj.Id = Convert.ToInt32(rd["Id"]);
                        obj.CompanyName = rd["CompanyName"].ToString();
                        obj.Date = rd["Date"].ToString();
                        obj.InvoiceNo = rd["InvoiceNo"].ToString();
                        obj.AmtBeforeTax = rd["AmtBeforeTax"].ToString();
                        obj.Address = rd["Address"].ToString();
                        obj.GSTNo = rd["GSTNo"].ToString();
                        obj.PANNo = rd["PANNo"].ToString();
                        obj.TaxType = rd["TaxType"].ToString();
                        obj.Tax = rd["Tax"].ToString();
                        obj.AmtAfterTax = rd["AmtAfterTax"].ToString();
                        obj.UploadInvoicePath = rd["UploadInvoicePath"].ToString();
                        gtlst.Add(obj);
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
            return gtlst;
        }
        public Purchase EditPurchase(int Id = 0)
        {

            Purchase obj = new Purchase();
            try
            {
                cmd = new SqlCommand("Sp_Purchase", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "edit");
                cmd.Parameters.AddWithValue("@Id", Id);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        obj = new Purchase();
                        obj.Id = Convert.ToInt32(rd["Id"]);
                        obj.CompanyName = rd["CompanyName"].ToString();
                        obj.Date = rd["Date"].ToString();
                        obj.InvoiceNo = rd["InvoiceNo"].ToString();
                        obj.AmtBeforeTax = rd["AmtBeforeTax"].ToString();
                        obj.Address = rd["Address"].ToString();
                        obj.GSTNo = rd["GSTNo"].ToString();
                        obj.PANNo = rd["PANNo"].ToString();
                        obj.TaxType = rd["TaxType"].ToString();
                        obj.Tax = rd["Tax"].ToString();
                        obj.AmtAfterTax = rd["AmtAfterTax"].ToString();
                        obj.UploadInvoicePath = rd["UploadInvoicePath"].ToString();
                        obj.IGST = Convert.ToInt32(rd["IGST"]);
                        obj.CGST = Convert.ToInt32(rd["CGST"]);
                        obj.SGST = Convert.ToInt32(rd["SGST"]);
                        obj.TaxAmount = rd["TaxAmount"].ToString();

                        obj.CGSTAmount = rd["CGSTAmount"].ToString();
                        obj.SGSTAmount = rd["SGSTAmount"].ToString();
                        obj.IGSTAmount = rd["IGSTAmount"].ToString();
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
            return obj;
        }
        public int DeletePurchase(int Id = 0)
        {
            int row = 0;
            try
            {
                cmd = new SqlCommand("Sp_Purchase", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "delete");
                con.Open();
                cmd.Parameters.AddWithValue("@Id", Id);
                row = cmd.ExecuteNonQuery();
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
            return row;
        }
        public int AddPayment(Payments obj)
        {
            int row = 0;
            try
            {
                cmd = new SqlCommand("Sp_PaymentMaster", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "Insert");
                con.Open();
                cmd.Parameters.AddWithValue("@CompanyId", obj.CompanyId);
                cmd.Parameters.AddWithValue("@CompanyName", obj.CompanyName);
                cmd.Parameters.AddWithValue("@ProjectId", obj.ProjectId);
                cmd.Parameters.AddWithValue("@ProjectName", obj.ProjectName);
                cmd.Parameters.AddWithValue("@TotalAmount", obj.TotalAmount);
                cmd.Parameters.AddWithValue("@Payment", obj.Payment);
                cmd.Parameters.AddWithValue("@RemainingAmount", obj.RemainingAmount);
                cmd.Parameters.AddWithValue("@PaymentMode", obj.PaymentMode);
                cmd.Parameters.AddWithValue("@BankName", obj.BankName);
                cmd.Parameters.AddWithValue("@BankBranch", obj.Branch);
                cmd.Parameters.AddWithValue("@ChequeNo", obj.ChequeNo);
                cmd.Parameters.AddWithValue("@chequeDate", obj.chequeDate);
                cmd.Parameters.AddWithValue("@UTRNO", obj.UTRNO);
                cmd.Parameters.AddWithValue("@Remark", obj.Remark);
                cmd.Parameters.AddWithValue("@Date", obj.Date);
                row = cmd.ExecuteNonQuery();
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
            return row;
        }

        public List<Purchase> DateWiseGetPurchaseList(string Date1 = "", string Date2 = "")
        {
            List<Purchase> gtlst = new List<Purchase>();
            Purchase obj = new Purchase();
            try
            {
                cmd = new SqlCommand("Sp_Purchase", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "DateWiseSearch");
                cmd.Parameters.AddWithValue("@Date1", Date1);
                cmd.Parameters.AddWithValue("@Date2", Date2);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        obj = new Purchase();
                        obj.Id = Convert.ToInt32(rd["Id"]);
                        obj.CompanyName = rd["CompanyName"].ToString();
                        obj.Date = rd["Date"].ToString();
                        obj.InvoiceNo = rd["InvoiceNo"].ToString();
                        obj.AmtBeforeTax = rd["AmtBeforeTax"].ToString();
                        obj.Address = rd["Address"].ToString();
                        obj.PANNo = rd["PANNo"].ToString();
                        obj.TaxType = rd["TaxType"].ToString();
                        obj.Tax = rd["Tax"].ToString();
                        obj.AmtAfterTax = rd["AmtAfterTax"].ToString();
                        obj.UploadInvoicePath = rd["UploadInvoicePath"].ToString();
                        gtlst.Add(obj);
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
            return gtlst;
        }
        public Purchase TotalPurchaseAmt(string Date1 = "", string Date2 = "")
        {

            Purchase obj = new Purchase();
            try
            {
                cmd = new SqlCommand("Sp_Purchase", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "DateWiseTotalPurchase");
                cmd.Parameters.AddWithValue("@Date1", Date1);
                cmd.Parameters.AddWithValue("@Date2", Date2);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        obj = new Purchase();
                        obj.AmtBeforeTax = rd["AmtBeforeTax"].ToString();

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
            return obj;
        }
        public Purchase TotalBeforeAmt()
        {

            Purchase obj = new Purchase();
            try
            {
                cmd = new SqlCommand("Sp_Purchase", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "TotalAmountBefore");
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        obj = new Purchase();
                        obj.AmtBeforeTax = rd["AmtBeforeTax"].ToString();
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
            return obj;
        }
        public Purchase TotalAftereAmt()
        {

            Purchase obj = new Purchase();
            try
            {
                cmd = new SqlCommand("Sp_Purchase", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "TotalAmountAfter");
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        obj = new Purchase();
                        obj.AmtAfterTax = rd["AmtAfterTax"].ToString();
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
            return obj;
        }
        public string TotalBeforeAmtDate(string Date1 = "", string Date2 = "")
        {

            string AmtBeforeTax = "";
            try
            {
                cmd = new SqlCommand("Sp_Purchase", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "TotalAmountBeforeDate");
                cmd.Parameters.AddWithValue("@Date1", Date1);
                cmd.Parameters.AddWithValue("@Date2", Date2);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        AmtBeforeTax = rd["AmtBeforeTax"].ToString();
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
            return AmtBeforeTax;
        }
        public string TotalAfterAmtDate(string Date1 = "", string Date2 = "")
        {

            string AmtAfterTax = "";
            try
            {
                cmd = new SqlCommand("Sp_Purchase", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "TotalAmountAfterDate");
                cmd.Parameters.AddWithValue("@Date1", Date1);
                cmd.Parameters.AddWithValue("@Date2", Date2);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        AmtAfterTax = rd["AmtAfterTax"].ToString();
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
            return AmtAfterTax;
        }
        public List<Payments> GetDuePaymentList()
        {
            List<Payments> gtlst = new List<Payments>();
            Payments obj = new Payments();
            try
            {
                cmd = new SqlCommand("Sp_PaymentMaster", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "DuePaymentList");
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        obj = new Payments();
                        obj.CompanyName = rd["CompanyName"].ToString();
                        //obj.ProjectName = rd["ProjectName"].ToString();
                        obj.TotalAmount = Convert.ToDecimal(rd["TotalAmount"]);
                        if (rd["PaymentAmount"] != DBNull.Value)
                        {
                            obj.Payment = Convert.ToDecimal(rd["PaymentAmount"]);
                        }
                        else
                        {
                            obj.Payment = 0;
                        }
                        obj.RemainingAmount = obj.TotalAmount - obj.Payment;
                        if (obj.RemainingAmount != 0)
                        {
                            gtlst.Add(obj);
                        }
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
            return gtlst;
        }
        public List<Payments> ClientPaymentForLadgerAccount(int CompanyId)
        {
            List<Payments> gtlst = new List<Payments>();
            Payments obj = new Payments();
            try
            {
                cmd = new SqlCommand("Sp_PaymentMaster", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "ShowPayment");
                cmd.Parameters.AddWithValue("@CompanyId", CompanyId);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        obj = new Payments();
                        obj.Date = rd["Date"].ToString();
                        obj.TotalAmount = Convert.ToDecimal(rd["TotalAmount"]);
                        obj.Payment = Convert.ToDecimal(rd["Payment"]);
                        obj.RemainingAmount = Convert.ToDecimal(rd["RemainingAmount"]);
                        gtlst.Add(obj);
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
            return gtlst;
        }
        public List<TaskSheets> GetTasklistByDate(string Date1 = "", string Date2 = "", string EmployeeId = "")
        {
            List<TaskSheets> getlist = new List<TaskSheets>();
            TaskSheets obj = new TaskSheets();

            try
            {
                cmd = new SqlCommand("Sp_TaskSheet", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@Action", "SelectDateWise");
                cmd.Parameters.AddWithValue("@Date1", Date1);
                cmd.Parameters.AddWithValue("@Date2", Date2);
                cmd.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        obj = new TaskSheets();
                        obj.Date1 = rd["Date"].ToString();
                        obj.Project1 = rd["ProjectName"].ToString();
                        obj.Task1 = rd["Task"].ToString();
                        obj.Hour1 = rd["Hours"].ToString();
                        obj.Description1 = rd["Description"].ToString();
                        obj.Status1 = rd["Status"].ToString();
                        obj.Remark1 = rd["Remark"].ToString();
                        obj.EmpName = rd["EmployeeName"].ToString();
                        getlist.Add(obj);
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
            return getlist;
        }
        public int UpdateProjectStatus(Project obj)
        {
            int EmpId = 0;
            string EmpName = "";
            EmpId = ((Login)HttpContext.Current.Session["Login"]).EmpId;
            EmpName = ((Login)HttpContext.Current.Session["Login"]).EmployeeName;
            int row = 0;
            try
            {
                cmd = new SqlCommand("Sp_Project", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "InsertProjectStatus");
                con.Open();
                cmd.Parameters.AddWithValue("@EmployeeId", EmpId);
                cmd.Parameters.AddWithValue("@EmployeeName", EmpName);
                cmd.Parameters.AddWithValue("@ProjectCode", obj.ProjectCode);
                cmd.Parameters.AddWithValue("@ProjectName", obj.ProjectName);
                cmd.Parameters.AddWithValue("@ProjectStatus", obj.ProjectStatus);
                cmd.Parameters.AddWithValue("@Description", obj.Description);
                cmd.Parameters.AddWithValue("@Date", obj.Date);
                row = cmd.ExecuteNonQuery();
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
            return row;
        }
        //For Api Use
        public int UpdateProjectStatus1(Project obj)
        {
            int row = 0;
            try
            {
                cmd = new SqlCommand("Sp_Project", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "InsertProjectStatus");
                con.Open();
                cmd.Parameters.AddWithValue("@EmployeeId", obj.EmployeeId);
                cmd.Parameters.AddWithValue("@EmployeeName", obj.EmployeeName);
                cmd.Parameters.AddWithValue("@ProjectCode", obj.ProjectCode);
                cmd.Parameters.AddWithValue("@ProjectName", obj.ProjectName);
                cmd.Parameters.AddWithValue("@ProjectStatus", obj.ProjectStatus);
                cmd.Parameters.AddWithValue("@Description", obj.Description);
                cmd.Parameters.AddWithValue("@Date", obj.Date);
                row = cmd.ExecuteNonQuery();
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
            return row;
        }
        public List<TaskManage> ViewCompleteTask()
        {
            int EmpId = 0;
            EmpId = ((Login)HttpContext.Current.Session["Login"]).EmpId;
            List<TaskManage> task_list = new List<TaskManage>();
            connection.Open();
            SqlCommand cmd = new SqlCommand("sp_TaskManage", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "selectCompleteTask");
            cmd.Parameters.AddWithValue("@userid", EmpId);
            SqlDataReader sdr = cmd.ExecuteReader();
            TaskManage pro;
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    pro = new TaskManage();
                    pro.id = sdr["id"].ToString();
                    pro.s_no = sdr["S_no"].ToString();
                    pro.title = sdr["title"].ToString();
                    pro.description = sdr["description"].ToString();
                    pro.complete_date = sdr["completion_date"].ToString();
                    pro.current_date = sdr["curent_date"].ToString();
                    pro.attachment1 = sdr["attachment1"].ToString();
                    pro.attachment2 = sdr["attachment2"].ToString();
                    pro.attachment3 = sdr["attachment3"].ToString();
                    pro.attachment4 = sdr["attachment4"].ToString();
                    pro.attachment5 = sdr["attachment5"].ToString();
                    pro.updatedDateEmp = sdr["updatedDateEmp"].ToString();
                    pro.emp_status = sdr["emp_status"].ToString();
                    pro.commentTask = sdr["commentTask"].ToString();
                    task_list.Add(pro);
                }
            }
            connection.Close();
            return task_list;
        }
        public List<Assignleave> ViewTotalLeave()
        {
            int EmpId = 0;
            EmpId = ((Login)HttpContext.Current.Session["Login"]).EmpId;
            List<Assignleave> task_list = new List<Assignleave>();
            connection.Open();
            SqlCommand cmd = new SqlCommand("Sp_LeaveManagement", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "TotalLeaveList");
            cmd.Parameters.AddWithValue("@EmployeeId", EmpId);
            SqlDataReader sdr = cmd.ExecuteReader();
            Assignleave pro;
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    pro = new Assignleave();
                    pro.EmployeeName= sdr["EmployeeName"].ToString();
                    pro.Leave= sdr["LeaveType"].ToString();
                    pro.Type = sdr["NoOfLeave"].ToString();
                    pro.Year= sdr["Date"].ToString();
                    task_list.Add(pro);
                }
            }
            connection.Close();
            return task_list;
        }
        public string ProjectYourEmployeeDescription(string ProjectCode = "")
        {
            string Description = "";
            int EmpId = 0;
            EmpId = ((Login)HttpContext.Current.Session["Login"]).EmpId;
            try
            {
                cmd = new SqlCommand("Sp_Project", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "ProjectYourEmployeeDescription");
                con.Open();
                cmd.Parameters.AddWithValue("@ProjectCode", ProjectCode);
                cmd.Parameters.AddWithValue("@EmployeeId", EmpId);
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        Description = rd["Description"].ToString();
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
            return Description;
        }
        public List<Project> ProjectStatusDetail(string ProjectCode = "")
        {
            List<Project> list = new List<Project>();
            Project obj = new Project();
            try
            {
                cmd = new SqlCommand("Sp_Project", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "ProjectYourEmployeeDescriptionAdmin");
                con.Open();
                cmd.Parameters.AddWithValue("@ProjectCode", ProjectCode);
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        obj = new Project();
                        obj.EmployeeName = rd["EmployeeName"].ToString();
                        obj.ProjectName = rd["ProjectName"].ToString();
                        obj.ProjectStatus = rd["ProjectStatus"].ToString();
                        obj.Description = rd["Description"].ToString();
                        obj.Date = rd["Date"].ToString();
                        list.Add(obj);
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
            return list;
        }
        public List<ApplyLeave> GetUserDashboardLeaveForAdmins(int Id = 0)
        {
            List<ApplyLeave> getlist = new List<ApplyLeave>();
            ApplyLeave obj = new ApplyLeave();

            try
            {
                cmd = new SqlCommand("Sp_LeaveManagement", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "UserDashboardLeaveAdmin");
                cmd.Parameters.AddWithValue("@EmployeeId", Id);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        obj = new ApplyLeave();
                        obj.EmployeeName = rd["EmployeeName"].ToString();
                        obj.LeaveType = rd["LeaveType"].ToString();
                        obj.NoOfLeave = Convert.ToInt32(rd["NoOfLeave"]);
                        if (rd["EffectiveLeaveCount"] != DBNull.Value)
                        {
                            obj.LeaveCount = Convert.ToInt32(rd["EffectiveLeaveCount"]);
                        }
                        obj.Remain = obj.NoOfLeave - obj.LeaveCount;
                        if (rd["RejectLeaveCount"] != DBNull.Value)
                        {
                            obj.RejectLeaveCount = Convert.ToInt32(rd["RejectLeaveCount"]);
                        }
                        else
                        {
                            obj.RejectLeaveCount = 0;
                        }
                        getlist.Add(obj);
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
            return getlist;
        }
        public List<ApplyLeave> GetUserDashboardLeaveForReportingManager(int Id = 0)
        {
            List<ApplyLeave> getlist = new List<ApplyLeave>();
            ApplyLeave obj = new ApplyLeave();

            try
            {
                cmd = new SqlCommand("Sp_LeaveManagement", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "UserDashboardLeaveAdmin");
                cmd.Parameters.AddWithValue("@EmployeeId",Id);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        obj = new ApplyLeave();
                        obj.EmployeeName = rd["EmployeeName"].ToString();
                        obj.FromDate = rd["FromDate"].ToString();
                        obj.ToDate = rd["ToDate"].ToString();
                        obj.LeaveType = rd["LeaveType"].ToString();
                        obj.NoOfLeave = Convert.ToInt32(rd["NoOfLeave"]);
                        if (rd["EffectiveLeaveCount"] != DBNull.Value)
                        {
                            obj.LeaveCount = Convert.ToInt32(rd["EffectiveLeaveCount"]);
                        }
                        obj.Remain = obj.NoOfLeave - obj.LeaveCount;
                        if (rd["RejectLeaveCount"] != DBNull.Value)
                        {
                            obj.RejectLeaveCount = Convert.ToInt32(rd["RejectLeaveCount"]);
                        }
                        else
                        {
                            obj.RejectLeaveCount = 0;
                        }
                        getlist.Add(obj);
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
            return getlist;
        }
        public List<ApplyLeave> GetClEmp()
        {
            int EmpId = 0;

            List<ApplyLeave> getlist = new List<ApplyLeave>();
            ApplyLeave obj = new ApplyLeave();
            EmpId = ((Login)HttpContext.Current.Session["Login"]).EmpId;
            try
            {
                cmd = new SqlCommand("Sp_LeaveManagement", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "CLLeaveRemaining");
                cmd.Parameters.AddWithValue("@EmployeeId", EmpId);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        obj = new ApplyLeave();
                        obj.FromDate = rd["FromDate"].ToString();
                        obj.ToDate = rd["FromDate"].ToString();
                        obj.NoOfLeave = Convert.ToInt32(rd["TotalAssignedLeave"]);
                        if (rd["ApprovedLeaveCount"] != DBNull.Value)
                        {
                            obj.LeaveCount = Convert.ToInt32(rd["ApprovedLeaveCount"]);
                        }
                        obj.Remain = obj.NoOfLeave - obj.LeaveCount;
                        var rem = obj.Remain;
                        // HttpContext.Current.Session["CLRe"] = obj.Remain;
                        if (rd["RejectedLeaveCount"] != DBNull.Value)
                        {
                            obj.RejectLeaveCount = Convert.ToInt32(rd["RejectedLeaveCount"]);
                        }
                        else
                        {
                            obj.RejectLeaveCount = 0;
                        }
                        getlist.Add(obj);
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

            return getlist;
        }
        public List<ApplyLeave> GetMLEmp()
        {
            int EmpId = 0;

            List<ApplyLeave> getlist = new List<ApplyLeave>();
            ApplyLeave obj = new ApplyLeave();
            EmpId = ((Login)HttpContext.Current.Session["Login"]).EmpId;
            try
            {
                cmd = new SqlCommand("Sp_LeaveManagement", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "MLLeaveRemaining");
                cmd.Parameters.AddWithValue("@EmployeeId", EmpId);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        obj = new ApplyLeave();
                        obj.FromDate = rd["FromDate"].ToString();
                        obj.ToDate = rd["ToDate"].ToString();
                        obj.NoOfLeave = Convert.ToInt32(rd["TotalAssignedLeave"]);
                        if (rd["ApprovedLeaveCount"] != DBNull.Value)
                        {
                            obj.LeaveCount = Convert.ToInt32(rd["ApprovedLeaveCount"]);
                        }
                        obj.Remain = obj.NoOfLeave - obj.LeaveCount;
                        // HttpContext.Current.Session["CLRe"] = obj.Remain;
                        if (rd["RejectedLeaveCount"] != DBNull.Value)
                        {
                            obj.RejectLeaveCount = Convert.ToInt32(rd["RejectedLeaveCount"]);
                        }
                        else
                        {
                            obj.RejectLeaveCount = 0;
                        }
                        getlist.Add(obj);
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
            return getlist;
        }
        public List<ApplyLeave> GetELEmp()
        {
            int EmpId = 0;

            List<ApplyLeave> getlist = new List<ApplyLeave>();
            ApplyLeave obj = new ApplyLeave();
            EmpId = ((Login)HttpContext.Current.Session["Login"]).EmpId;
            try
            {
                cmd = new SqlCommand("Sp_LeaveManagement", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "ELLeaveRemaining");
                cmd.Parameters.AddWithValue("@EmployeeId", EmpId);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        obj = new ApplyLeave();
                        obj.FromDate = rd["FromDate"].ToString();
                        obj.ToDate = rd["ToDate"].ToString();
                        obj.NoOfLeave = Convert.ToInt32(rd["TotalAssignedLeave"]);
                        if (rd["ApprovedLeaveCount"] != DBNull.Value)
                        {
                            obj.LeaveCount = Convert.ToInt32(rd["ApprovedLeaveCount"]);
                        }
                        obj.Remain = obj.NoOfLeave - obj.LeaveCount;
                        // HttpContext.Current.Session["CLRe"] = obj.Remain;
                        if (rd["RejectedLeaveCount"] != DBNull.Value)
                        {
                            obj.RejectLeaveCount = Convert.ToInt32(rd["RejectedLeaveCount"]);
                        }
                        else
                        {
                            obj.RejectLeaveCount = 0;
                        }
                        getlist.Add(obj);
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
            return getlist;
        }
        public List<ApplyLeave> GetWPLEmp()
        {
            int EmpId = 0;

            List<ApplyLeave> getlist = new List<ApplyLeave>();
            ApplyLeave obj = new ApplyLeave();
            EmpId = ((Login)HttpContext.Current.Session["Login"]).EmpId;
            try
            {
                cmd = new SqlCommand("Sp_LeaveManagement", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "LWPLeaveRemaining");
                cmd.Parameters.AddWithValue("@EmployeeId", EmpId);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        obj = new ApplyLeave();
                        obj.FromDate = rd["FromDate"].ToString();
                        obj.ToDate = rd["ToDate"].ToString();
                        obj.NoOfLeave = Convert.ToInt32(rd["TotalAssignedLeave"]);
                        if (rd["ApprovedLeaveCount"] != DBNull.Value)
                        {
                            obj.LeaveCount = Convert.ToInt32(rd["ApprovedLeaveCount"]);
                        }
                        obj.Remain = obj.NoOfLeave - obj.LeaveCount;
                        // HttpContext.Current.Session["CLRe"] = obj.Remain;
                        if (rd["RejectedLeaveCount"] != DBNull.Value)
                        {
                            obj.RejectLeaveCount = Convert.ToInt32(rd["RejectedLeaveCount"]);
                        }
                        else
                        {
                            obj.RejectLeaveCount = 0;
                        }
                        getlist.Add(obj);
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
            return getlist;
        }
        public List<performa> GetRenewProductList()
        {
            List<performa> GetList = new List<performa>();
            performa obj = new performa();
            try
            {
                cmd = new SqlCommand("", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("", "");
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        obj = new performa();
                        obj.PINo = rd["PINo"].ToString();
                        obj.Services1 = rd["Services"].ToString();
                        obj.ServicesName1 = rd["ServicesName"].ToString();
                        obj.Duration1 = rd["Duration"].ToString();
                        obj.DurationTime1 = rd["DurationTime"].ToString();
                        obj.Date1 = rd["StartDate1"].ToString();
                        obj.Amount1 = Convert.ToDecimal(rd["Amount"]);
                        obj.Description1 = rd["Description"].ToString();
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
            return GetList;
        }
        public int InsertLead(Lead obj)
        {
            int row = 0;
            string EmpName = "";
            EmpName = ((Login)HttpContext.Current.Session["Login"]).EmployeeName;
            if (EmpName == null)
            {
                EmpName = "Admin";
            }
            try
            {
                cmd = new SqlCommand("Sp_LeadManagement", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "Insert");
                con.Open();
                cmd.Parameters.AddWithValue("@LeadType", obj.LeadType);
                cmd.Parameters.AddWithValue("@LeadDescription", obj.LeadDescription);
                cmd.Parameters.AddWithValue("@ClientName", obj.ClientName);
                cmd.Parameters.AddWithValue("@ClientEmail", obj.ClientEmail);
                cmd.Parameters.AddWithValue("@MobileNo", obj.MobileNo);
                cmd.Parameters.AddWithValue("@Address", obj.Address);
                cmd.Parameters.AddWithValue("@ContectPerson", obj.ContectPerson);
                //cmd.Parameters.AddWithValue("@AssignBy", obj.AssignBy);
                row = cmd.ExecuteNonQuery();
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
            return row;
        }
        public int UpdateLead(Lead obj)
        {
            int row = 0;
            try
            {
                cmd = new SqlCommand("Sp_LeadManagement", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "Update");
                con.Open();
                cmd.Parameters.AddWithValue("@Id", obj.Id);
                cmd.Parameters.AddWithValue("@LeadType", obj.LeadType);
                cmd.Parameters.AddWithValue("@LeadDescription", obj.LeadDescription);
                cmd.Parameters.AddWithValue("@ClientName", obj.ClientName);
                cmd.Parameters.AddWithValue("@ClientEmail", obj.ClientEmail);
                cmd.Parameters.AddWithValue("@MobileNo", obj.MobileNo);
                cmd.Parameters.AddWithValue("@Address", obj.Address);
                cmd.Parameters.AddWithValue("@ContectPerson", obj.ContectPerson);
                row = cmd.ExecuteNonQuery();
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
            return row;
        }
        public Lead EditLead(int Id = 0)
        {
            Lead obj = new Lead();
            try
            {
                cmd = new SqlCommand("Sp_LeadManagement", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "Edit");
                cmd.Parameters.AddWithValue("@Id", Id);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        obj = new Lead();
                        obj.Id = Convert.ToInt32(rd["Id"]);
                        obj.LeadType = rd["LeadType"].ToString();
                        obj.LeadDescription = rd["LeadDescription"].ToString();
                        obj.ClientName = rd["ClientName"].ToString();
                        obj.ClientEmail = rd["ClientEmail"].ToString();
                        obj.MobileNo = rd["MobileNo"].ToString();
                        obj.Address = rd["Address"].ToString();
                        obj.ContectPerson = rd["ContectPerson"].ToString();
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
            return obj;
        }
        public List<Lead> AllLead()
        {
            Lead obj = new Lead();
            List<Lead> list = new List<Lead>();
            try
            {
                cmd = new SqlCommand("Sp_LeadManagement", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "Select");
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        obj = new Lead();
                        obj.Id = Convert.ToInt32(rd["Id"]);
                        obj.LeadType = rd["LeadType"].ToString();
                        obj.LeadDescription = rd["LeadDescription"].ToString();
                        obj.ClientName = rd["ClientName"].ToString();
                        obj.ClientEmail = rd["ClientEmail"].ToString();
                        obj.MobileNo = rd["MobileNo"].ToString();
                        obj.Address = rd["Address"].ToString();
                        obj.Status = Convert.ToInt32(rd["Status"]);
                        obj.ContectPerson = rd["ContectPerson"].ToString();
                        list.Add(obj);
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
            return list;
        }
        public int DeleteLead(int Id = 0)
        {
            int row = 0;
            try
            {
                cmd = new SqlCommand("Sp_LeadManagement", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "Delete");
                con.Open();
                cmd.Parameters.AddWithValue("@Id", Id);
                row = cmd.ExecuteNonQuery();
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
            return row;
        }
        public int InsertAssignLead(string Leads = "", string EmployeeId = "", string EmployeeName = "", string AssignDate = "")
        {
            int row = 0;
            try
            {

                cmd = new SqlCommand("Sp_LeadManagement", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "InsertAssignedLead");
                con.Open();
                cmd.Parameters.AddWithValue("@LeadId", Leads);
                cmd.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                cmd.Parameters.AddWithValue("@EmployeeName", EmployeeName);
                cmd.Parameters.AddWithValue("@AssignDate", AssignDate);
                row = cmd.ExecuteNonQuery();
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
            return row;
        }
        public List<Lead> AllNoAssignLead()
        {
            Lead obj = new Lead();
            List<Lead> list = new List<Lead>();
            try
            {
                cmd = new SqlCommand("Sp_LeadManagement", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "SelectNoAssign");
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        obj = new Lead();
                        obj.Id = Convert.ToInt32(rd["Id"]);
                        obj.LeadType = rd["LeadType"].ToString();
                        obj.LeadDescription = rd["LeadDescription"].ToString();
                        obj.ClientName = rd["ClientName"].ToString();
                        obj.ClientEmail = rd["ClientEmail"].ToString();
                        obj.MobileNo = rd["MobileNo"].ToString();
                        obj.Address = rd["Address"].ToString();
                        obj.ContectPerson = rd["ContectPerson"].ToString();
                        list.Add(obj);
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
            return list;
        }
        public List<Lead> AllAssignLead()
        {
            Lead obj = new Lead();
            List<Lead> list = new List<Lead>();
            try
            {
                cmd = new SqlCommand("Sp_LeadManagement", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "SelectAssign");
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        obj = new Lead();
                        obj.Id = Convert.ToInt32(rd["Id"]);
                        obj.LeadType = rd["LeadType"].ToString();
                        obj.LeadDescription = rd["LeadDescription"].ToString();
                        obj.ClientName = rd["ClientName"].ToString();
                        obj.ClientEmail = rd["ClientEmail"].ToString();
                        obj.MobileNo = rd["MobileNo"].ToString();
                        obj.Address = rd["Address"].ToString();
                        obj.EmployeeName = rd["EmployeeName"].ToString();
                        obj.LeadStatus = rd["LeadStatus"].ToString();
                        obj.AssignDate = rd["AssignDate"].ToString();
                        obj.ContectPerson = rd["ContectPerson"].ToString();
                        //obj.EmpDescription = rd["EmpDescription"].ToString();
                        //obj.AssignBy = rd["AssignBy"].ToString();
                        list.Add(obj);
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
            return list;
        }
        public int DeleteAssignLead(int Id = 0)
        {
            int row = 0;
            try
            {
                cmd = new SqlCommand("Sp_LeadManagement", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "AssignDelete");
                con.Open();
                cmd.Parameters.AddWithValue("@Id", Id);
                row = cmd.ExecuteNonQuery();
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
            return row;
        }
        public List<Lead> AllLeadForEmployee()
        {
            Lead obj = new Lead();
            List<Lead> list = new List<Lead>();
            int EmpId = 0;
            EmpId = ((Login)HttpContext.Current.Session["Login"]).EmpId;
            try
            {
                cmd = new SqlCommand("Sp_LeadManagement", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "SelectAssignEmployeeLead");
                cmd.Parameters.AddWithValue("@EmployeeId", EmpId);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        obj = new Lead();
                        obj.Id = Convert.ToInt32(rd["Id"]);
                        obj.LeadType = rd["LeadType"].ToString();
                        obj.LeadDescription = rd["LeadDescription"].ToString();
                        obj.ClientName = rd["ClientName"].ToString();
                        obj.ClientEmail = rd["ClientEmail"].ToString();
                        obj.MobileNo = rd["MobileNo"].ToString();
                        obj.Address = rd["Address"].ToString();
                        obj.LeadStatus = rd["LeadStatus"].ToString();
                        obj.EmployeeName = rd["EmployeeName"].ToString();
                        obj.ContectPerson = rd["ContectPerson"].ToString();
                        //obj.EmpDescription = rd["EmpDescription"].ToString();
                        obj.AssignDate = rd["AssignDate"].ToString();
                        //obj.AssignBy = rd["AssignBy"].ToString();
                        list.Add(obj);
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
            return list;
        }
        public int InsertEmpLead(Lead obj)
        {
            int row = 0;
            int EmpId = 0;
            string EmpName = "";
            EmpId = ((Login)HttpContext.Current.Session["Login"]).EmpId;
            EmpName = ((Login)HttpContext.Current.Session["Login"]).EmployeeName;
            try
            {
                cmd = new SqlCommand("Sp_LeadManagement", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "InsertEmpLead");
                con.Open();
                cmd.Parameters.AddWithValue("@LeadType", obj.LeadType);
                cmd.Parameters.AddWithValue("@LeadDescription", obj.LeadDescription);
                cmd.Parameters.AddWithValue("@ClientName", obj.ClientName);
                cmd.Parameters.AddWithValue("@ClientEmail", obj.ClientEmail);
                cmd.Parameters.AddWithValue("@MobileNo", obj.MobileNo);
                cmd.Parameters.AddWithValue("@Address", obj.Address);
                cmd.Parameters.AddWithValue("@EmployeeId", EmpId);
                cmd.Parameters.AddWithValue("@EmployeeName", EmpName);
                cmd.Parameters.AddWithValue("@ContectPerson", obj.ContectPerson);
                row = cmd.ExecuteNonQuery();
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
            return row;
        }
        public int UpdateEmpLead(Lead obj)
        {
            int row = 0;
            try
            {
                cmd = new SqlCommand("Sp_LeadManagement", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "Update");
                con.Open();
                cmd.Parameters.AddWithValue("@Id", obj.Id);
                cmd.Parameters.AddWithValue("@LeadType", obj.LeadType);
                cmd.Parameters.AddWithValue("@LeadDescription", obj.LeadDescription);
                cmd.Parameters.AddWithValue("@ClientName", obj.ClientName);
                cmd.Parameters.AddWithValue("@ClientEmail", obj.ClientEmail);
                cmd.Parameters.AddWithValue("@MobileNo", obj.MobileNo);
                cmd.Parameters.AddWithValue("@Address", obj.Address);
                cmd.Parameters.AddWithValue("@ContectPerson", obj.ContectPerson);
                row = cmd.ExecuteNonQuery();
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
            return row;
        }
        public int UpdateLeadStatus(Lead obj)
        {
            int row = 0;
            try
            {
                cmd = new SqlCommand("Sp_LeadManagement", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "UpdateLeadStatus");
                con.Open();
                cmd.Parameters.AddWithValue("@Id", obj.LeadId);
                cmd.Parameters.AddWithValue("@Status", obj.LeadStatus);
                cmd.Parameters.AddWithValue("@EmpDescription", obj.EmpDescription);
                row = cmd.ExecuteNonQuery();
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
            return row;
        }
        public List<performa> GetproductList()
        {
            List<performa> list = new List<performa>();
            performa obj = new performa();
            try
            {
                cmd = new SqlCommand("Sp_RenewProduct", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "selectRenewProduct");
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        obj = new performa();
                        obj.InvoiceNo = rd["InvoiceNo"].ToString();
                        obj.ServicesName1 = rd["ServicesName"].ToString();
                        obj.DurationTime1 = rd["DurationTime"].ToString();
                        obj.Amount1 = Convert.ToDecimal(rd["Amount"]);
                        obj.CompanyId = rd["CompanyId"].ToString();
                        obj.CompanyName = rd["CompanyName"].ToString();
                        obj.ProjectName = rd["ProjectName"].ToString();
                        list.Add(obj);
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
            return list;
        }
        public List<performa> GetproductListForInvoice(string InvoiceNo = "")
        {
            List<performa> list = new List<performa>();
            performa obj = new performa();
            try
            {
                cmd = new SqlCommand("Sp_RenewProduct", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "RenewProduct");
                cmd.Parameters.AddWithValue("@InvoiceNo", InvoiceNo);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        obj = new performa();
                        obj.InvoiceNo = rd["InvoiceNo"].ToString();
                        obj.ServicesName1 = rd["ServicesName"].ToString();
                        obj.DurationTime1 = rd["DurationTime"].ToString();
                        obj.Amount1 = Convert.ToDecimal(rd["Amount"]);
                        obj.CompanyName = rd["CompanyName"].ToString();
                        obj.ProjectName = rd["ProjectName"].ToString();
                        list.Add(obj);
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
            return list;
        }
        public List<Client> ClientDetal(string CompanyId = "")
        {
            Client obj = new Client();
            List<Client> list = new List<Client>();
            try
            {
                cmd = new SqlCommand("Sp_Client", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "Edit");
                cmd.Parameters.AddWithValue("@Id", CompanyId);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        obj = new Client();
                        obj.Id = Convert.ToInt32(rd["Id"]);
                        obj.CompanyName = rd["CompanyName"].ToString();
                        obj.ContactPerson = rd["ContactPerson"].ToString();
                        obj.ContactNo = rd["ContactNo"].ToString();
                        obj.GSTNo = rd["GSTNo"].ToString();
                        obj.State = rd["State"].ToString();
                        obj.City = rd["City"].ToString();
                        obj.StateName = rd["StateName"].ToString();
                        obj.CityName = rd["CityName"].ToString();
                        obj.Address = rd["Address"].ToString();
                        obj.Statecode = rd["Statecode"].ToString();
                        obj.Pincode = rd["Pincode"].ToString();
                        obj.PanNo = rd["PanNo"].ToString();
                        obj.EmailId = rd["EmailId"].ToString();
                        obj.Designation = rd["Designation"].ToString();
                        list.Add(obj);
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
            return list;
        }
        public performa GetAmount(string InvoiceNo = "")
        {
            performa obj = new performa();
            try
            {
                cmd = new SqlCommand("Sp_RenewProduct", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "RenewProduct");
                cmd.Parameters.AddWithValue("@InvoiceNo", InvoiceNo);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        obj = new performa();
                        obj.InvoiceNo = rd["InvoiceNo"].ToString();
                        obj.TotalAmount = Convert.ToDecimal(rd["Amount"]);
                        obj.Date1 = rd["Date"].ToString();
                        obj.CompanyId = rd["CompanyId"].ToString();
                        obj.ProjectId = rd["ProjectId"].ToString();
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
            return obj;
        }
        public string GetTaxinvoiceNo(string PINo = "")
        {
            string InvoiceNo = "";
            try
            {
                cmd = new SqlCommand("Sp_Invoice", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "GetInvoice");
                cmd.Parameters.AddWithValue("@PINo", PINo);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        InvoiceNo = rd["TaxInvoiceNo"].ToString();
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
            return InvoiceNo;
        }
        public int InsertPaymentRenewProduct(performa obj)
        {
            int row = 0;
            try
            {
                cmd = new SqlCommand("Sp_Invoice", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "InsertRenewProduct");
                con.Open();
                cmd.Parameters.AddWithValue("@CompanyId", obj.CompanyId);
                cmd.Parameters.AddWithValue("@ProjectId", obj.ProjectId);
                cmd.Parameters.AddWithValue("@Date", obj.Date1);
                cmd.Parameters.AddWithValue("@AfterTaxAmount", obj.TotalAmount);
                row = cmd.ExecuteNonQuery();
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
            return row;
        }
        public List<performa> GetProductForRenew()
        {
            List<performa> getlist = new List<performa>();
            performa obj = new performa();
            try
            {
                cmd = new SqlCommand("Sp_RenewProduct", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "Select");
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        obj = new performa();
                        obj.InvoiceNo = rd["InvoiceNo"].ToString();
                        obj.Services1 = rd["Services"].ToString();
                        obj.ServicesName1 = rd["ServicesName"].ToString();
                        obj.Duration1 = rd["Duration"].ToString();
                        obj.DurationTime1 = rd["DurationTime"].ToString();
                        obj.Date1 = rd["StartDate"].ToString();
                        obj.Amount1 = Convert.ToDecimal(rd["Amount"]);
                        obj.Description1 = rd["Description"].ToString();
                        obj.ProjectId = rd["ProjectId"].ToString();
                        obj.CompanyId = rd["CompanyId"].ToString();
                        getlist.Add(obj);
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
            return getlist;
        }
        public int PIRenewProduct(string RenePINo = "", string Services = "", string ServicesName = "", string Duration = "", string DurationTime = "", string StartDate = "", decimal Amount = 0.00M, string Description = "", string CompanyId = "", string ProjectId = "", string InvoiceNo = "")
        {
            int row = 0;
            try
            {
                cmd = new SqlCommand("Sp_RenewProduct", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@Action", "InsertProduct");
                cmd.Parameters.AddWithValue("@RenePINo", RenePINo);
                cmd.Parameters.AddWithValue("@Services", Services);
                cmd.Parameters.AddWithValue("@ServicesName", ServicesName);
                cmd.Parameters.AddWithValue("@Duration", Duration);
                cmd.Parameters.AddWithValue("@DurationTime", DurationTime);
                cmd.Parameters.AddWithValue("@StartDate", StartDate);
                cmd.Parameters.AddWithValue("@Amount", Amount);
                cmd.Parameters.AddWithValue("@Description", Description);
                cmd.Parameters.AddWithValue("@CompanyId", CompanyId);
                cmd.Parameters.AddWithValue("@ProjectId", ProjectId);
                cmd.Parameters.AddWithValue("@InvoiceNo", InvoiceNo);
                row = cmd.ExecuteNonQuery();
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
            return row;
        }
        public string GetId()
        {
            string No = "";
            try
            {
                cmd = new SqlCommand("Sp_RenewProduct", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@Action", "GetIdForPINo");
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        No = rd["Id"].ToString();
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
            return No;
        }
        public string GetMonths()
        {
            string Months = "";
            try
            {
                cmd = new SqlCommand("Sp_RenewProduct", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@Action", "CurrentMonths");
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        Months = rd["Months"].ToString();
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
            return Months;
        }
        public int InsertClientProblem(TokenManagement obj)
        {
            int row = 0;
            string UserId = "";
            UserId = ((Login)HttpContext.Current.Session["Login"]).UserName;
            try
            {
                cmd = new SqlCommand("Sp_TokenManagement", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "Insert");
                con.Open();
                cmd.Parameters.AddWithValue("@UserTo", obj.To);
                cmd.Parameters.AddWithValue("@Subject", obj.Subject);
                cmd.Parameters.AddWithValue("@Message", obj.Message);
                cmd.Parameters.AddWithValue("@Files", obj.FilePath);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@TokenId", obj.TokenId);
                cmd.Parameters.AddWithValue("@Department", obj.Department);
                cmd.Parameters.AddWithValue("@Periority", obj.Periority);
                row = cmd.ExecuteNonQuery();
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
            return row;
        }
        public int InsertAttachFile(TokenManagement obj)
        {
            int row = 0;
            string UserId = "";
            UserId = ((Login)HttpContext.Current.Session["Login"]).UserName;
            try
            {
                cmd = new SqlCommand("Sp_TokenManagement", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@Action", "InsertAttachFile");
                cmd.Parameters.AddWithValue("@Files", obj.FilePath);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@TokenId", obj.TokenId);
                row = cmd.ExecuteNonQuery();
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
            return row;
        }
        public List<TokenManagement> GetInbox()
        {
            List<TokenManagement> getlist = new List<TokenManagement>();
            TokenManagement obj = new TokenManagement();
            string UserName = "";
            UserName = ((Login)HttpContext.Current.Session["Login"]).UserName;
            try
            {
                cmd = new SqlCommand("Sp_TokenManagement", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@Action", "Inbox");
                cmd.Parameters.AddWithValue("@UserId", UserName);
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        obj = new TokenManagement();
                        obj.Id = Convert.ToInt32(rd["Id"]);
                        obj.Subject = rd["Subject"].ToString();
                        obj.Message = rd["Message"].ToString();
                        obj.FilePath = rd["Files"].ToString();
                        obj.UserId = rd["UserId"].ToString();
                        obj.TokenId = rd["TokenId"].ToString();
                        obj.Status = rd["Status"].ToString();
                        obj.Date = rd["Date"].ToString();
                        obj.From = rd["Froms"].ToString();
                        obj.Department = rd["Department"].ToString();
                        //obj.Priority = rd["Priority"].ToString();
                        //obj.CompanyName = rd["CompanyName"].ToString();
                        //obj.ContactNo = rd["ContactNo"].ToString();
                        //if (Int.TryParse(rd["Comment"] != DBNull.Value))
                        //{
                        //    obj.Comment = Convert.ToInt32(rd["Comment"]);
                        //}
                        //else
                        //{
                        //    obj.Comment = 0;
                        //}
                        getlist.Add(obj);
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
            return getlist;
        }
        public List<TokenManagement> GetInboxForAdmin()
        {
            List<TokenManagement> getlist = new List<TokenManagement>();
            TokenManagement obj = new TokenManagement();
            string UserName = "";
            UserName = ((Login)HttpContext.Current.Session["Login"]).UserName;
            try
            {
                cmd = new SqlCommand("Sp_TokenManagement", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@Action", "Inbox");
                cmd.Parameters.AddWithValue("@UserId", UserName);
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        obj = new TokenManagement();
                        obj.Id = Convert.ToInt32(rd["Id"]);
                        obj.Subject = rd["Subject"].ToString();
                        obj.Message = rd["Message"].ToString();
                        obj.FilePath = rd["Files"].ToString();
                        obj.UserId = rd["UserId"].ToString();
                        obj.TokenId = rd["TokenId"].ToString();
                        obj.Status = rd["Status"].ToString();
                        obj.Date = rd["Date"].ToString();
                        obj.From = rd["Froms"].ToString();
                        obj.Department = rd["Department"].ToString();
                        //obj.Priority = rd["Priority"].ToString();
                        //obj.CompanyName = rd["CompanyName"].ToString();
                        //obj.ContactNo = rd["ContactNo"].ToString();
                        //if (rd["Comment"] != DBNull.Value)
                        //{
                        //    obj.Comment = Convert.ToInt32(rd["Comment"]);
                        //}
                        //else
                        //{
                        //    obj.Comment = 0;
                        //}
                        getlist.Add(obj);
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
            return getlist;
        }
        public List<SelectListItem> BindProduct()
        {
            List<SelectListItem> gtlst = new List<SelectListItem>();
            SelectListItem obj = new SelectListItem();
            try
            {
                cmd = new SqlCommand("Sp_Product", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "Product");
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        obj = new SelectListItem();
                        obj.Value = rd["Id"].ToString();
                        obj.Text = rd["Name"].ToString();
                        gtlst.Add(obj);
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
            return gtlst;
        }
        public string GetHSNNo(int ProductId = 0)
        {
            string HSNNo = "";
            try
            {
                cmd = new SqlCommand("Sp_Product", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "HSNNo");
                cmd.Parameters.AddWithValue("@Id", ProductId);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        HSNNo = rd["HSNNo"].ToString();
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
            return HSNNo;
        }
        public int QuatationSoftware(string QuatationNo = "", string SoftwareName = "", decimal SAmount = 0.00M, int SUnit = 0, string SDescription = "", string STotal = "")
        {
            int row = 0;
            try
            {
                if (SoftwareName != null)
                {
                    cmd = new SqlCommand("Sp_Quatation", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.Parameters.AddWithValue("@Action", "UpdateSProduct");
                    cmd.Parameters.AddWithValue("@QuatationNo", QuatationNo);
                    cmd.Parameters.AddWithValue("@SoftwareName", SoftwareName);
                    cmd.Parameters.AddWithValue("@Amount", SAmount);
                    cmd.Parameters.AddWithValue("@Unit", SUnit);
                    cmd.Parameters.AddWithValue("@Description", SDescription);
                    cmd.Parameters.AddWithValue("@TotalAmount", STotal);
                    row = cmd.ExecuteNonQuery();
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
            return row;
        }

        //quatation for software insert
        public int QuatationSoftwareIns(string QuatationNo = "", string SoftwareName = "", decimal SAmount = 0.00M, int SUnit = 0, string SDescription = "", string STotal = "")
        {
            int row = 0;
            try
            {
                if (SoftwareName != null)
                {
                    cmd = new SqlCommand("Sp_Quatation", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.Parameters.AddWithValue("@Action", "InsertSProduct");
                    cmd.Parameters.AddWithValue("@QuatationNo", QuatationNo);
                    cmd.Parameters.AddWithValue("@SoftwareName", SoftwareName);
                    cmd.Parameters.AddWithValue("@Amount", SAmount);
                    cmd.Parameters.AddWithValue("@Unit", SUnit);
                    cmd.Parameters.AddWithValue("@Description", SDescription);
                    cmd.Parameters.AddWithValue("@TotalAmount", STotal);
                    row = cmd.ExecuteNonQuery();
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
            return row;
        }
        public int QuatationHardware(string QuatationNo = "", string HardwareName = "", string HDescription = "", string HAmount = "", string HUnit = "", string HTotal = "", decimal AMCAmount = 0.00M, string AMCStartDate = "", decimal REWAmount = 0.00M , string REWStartDate = "", string AMC = "", string Renewable = "")
        {
            int row = 0;
            try
            {
                cmd = new SqlCommand("Sp_Quatation", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@Action", "InsertHProduct");
                cmd.Parameters.AddWithValue("@QuatationNo", QuatationNo);
                cmd.Parameters.AddWithValue("@ProjectName", HardwareName);
                cmd.Parameters.AddWithValue("@HDescription", HDescription);
                cmd.Parameters.AddWithValue("@Amount", HAmount);
                cmd.Parameters.AddWithValue("@Unit", HUnit);
                cmd.Parameters.AddWithValue("@TotalAmount", HTotal);
                cmd.Parameters.AddWithValue("@AMC", AMC);
                cmd.Parameters.AddWithValue("@Renewable", Renewable);
                cmd.Parameters.AddWithValue("@AMCAmount", AMCAmount);
                cmd.Parameters.AddWithValue("@AMCStartDate", AMCStartDate);
                cmd.Parameters.AddWithValue("@RenewableAmount", REWAmount);
                cmd.Parameters.AddWithValue("@RenewableStartDate", REWStartDate);
                row = cmd.ExecuteNonQuery();
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
            return row;
        }
        //quatation for hardware insert
        public int QuatationHardwareIns(string QuatationNo = "", string HardwareName = "", string HDescription = "", string HAmount = "", string HUnit = "", string HTotal = "", decimal AMCAmount = 0.00M, string AMCStartDate = "", decimal REWAmount = 0.00M , string REWStartDate = "", string AMC = "", string Renewable = "")
        {
            int row = 0;
            try
            {
                cmd = new SqlCommand("Sp_Quatation", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@Action", "InsertHProduct");
                cmd.Parameters.AddWithValue("@QuatationNo", QuatationNo);
                cmd.Parameters.AddWithValue("@ProjectName", HardwareName);
                cmd.Parameters.AddWithValue("@HDescription", HDescription);
                cmd.Parameters.AddWithValue("@Amount", HAmount);
                cmd.Parameters.AddWithValue("@Unit", HUnit);
                cmd.Parameters.AddWithValue("@TotalAmount", HTotal);
                cmd.Parameters.AddWithValue("@AMC", AMC);
                cmd.Parameters.AddWithValue("@Renewable", Renewable);
                cmd.Parameters.AddWithValue("@AMCAmount",AMCAmount);
                cmd.Parameters.AddWithValue("@AMCStartDate", AMCStartDate);
                cmd.Parameters.AddWithValue("@RenewableAmount", REWAmount);
                cmd.Parameters.AddWithValue("@RenewableStartDate", REWStartDate);

                row = cmd.ExecuteNonQuery();
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
            return row;
        }
        public int QuatationSoftwareServices(string QuatationNo = "", string SSServices = "", string SSServicesName = "", string SSDuration = "", string SSDurationTime = "", decimal SSAmount = 0.00M, string SSDescription = "")
        {
            int row = 0;
            try
            {
                if (SSServices != null)
                {
                    cmd = new SqlCommand("Sp_Quatation", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.Parameters.AddWithValue("@Action", "InsertSoftwareServices");
                    cmd.Parameters.AddWithValue("@QuatationNo", QuatationNo);
                    cmd.Parameters.AddWithValue("@Services", SSServices);
                    cmd.Parameters.AddWithValue("@ServicesName", SSServicesName);
                    cmd.Parameters.AddWithValue("@Duration", SSDuration);
                    cmd.Parameters.AddWithValue("@DurationTime", SSDurationTime);
                    cmd.Parameters.AddWithValue("@Amount", SSAmount);
                    cmd.Parameters.AddWithValue("@Description", SSDescription);
                    row = cmd.ExecuteNonQuery();
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
            return row;
        }
        public int PIHardware(string PINo = "", string HardwareName = "", string HDescription = "", string HAmount = "", string HUnit = "", string HTotal = "")
        {
            int row = 0;
            try
            {
                cmd = new SqlCommand("Sp_PIMaster", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@Action", "InsertHProduct");
                cmd.Parameters.AddWithValue("@PINo", PINo);
                cmd.Parameters.AddWithValue("@ProductName", HardwareName);
                cmd.Parameters.AddWithValue("@HDescription", HDescription);
                cmd.Parameters.AddWithValue("@Amount", HAmount);
                cmd.Parameters.AddWithValue("@Unit", HUnit);
                cmd.Parameters.AddWithValue("@TotalAmount", HTotal);
                row = cmd.ExecuteNonQuery();
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
            return row;
        }
        public int PISoftware(string PINo = "", string SoftwareName = "", decimal SAmount = 0.00M, int SUnit = 0, string SDescription = "", string STotal = "")
        {
            int row = 0;
            try
            {
                if (SoftwareName != null)
                {
                    cmd = new SqlCommand("Sp_PIMaster", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.Parameters.AddWithValue("@Action", "InsertSProduct");
                    cmd.Parameters.AddWithValue("@PINo", PINo);
                    cmd.Parameters.AddWithValue("@SoftwareName", SoftwareName);
                    cmd.Parameters.AddWithValue("@Amount", SAmount);
                    cmd.Parameters.AddWithValue("@Unit", SUnit);
                    cmd.Parameters.AddWithValue("@Description", SDescription);
                    cmd.Parameters.AddWithValue("@TotalAmount", STotal);
                    row = cmd.ExecuteNonQuery();
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
            return row;
        }
        public performa EditPIThrowPI(string PINo = "")
        {
            performa obj = new performa();
            try
            {
                cmd = new SqlCommand("Sp_InvoiceMaster", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@Action", "EDITPI");
                cmd.Parameters.AddWithValue("@PINo", PINo);
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        obj = new performa();
                        obj.PINo = rd["PINo"].ToString();
                        obj.CompanyId = rd["CompanyId"].ToString();
                        obj.ProjectId = rd["ProjectId"].ToString();
                        obj.ProjectAmount = Convert.ToDecimal(rd["ProjectAmount"]);
                        obj.TotalAmount = Convert.ToDecimal(rd["TotalAmount"]);
                        obj.Tax = Convert.ToInt32(rd["Tax"]);
                        obj.TaxAmount = Convert.ToDecimal(rd["TaxAmount"]);
                        obj.AfterTaxAmount = Convert.ToDecimal(rd["AfterTaxAmount"]);
                        obj.Taxtype = rd["Taxtype"].ToString();
                        obj.Type = rd["Type"].ToString();
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
            return obj;
        }
        public int InsertTaxInvoice(performa obj)
        {
            int row = 0;
            try
            {
                cmd = new SqlCommand("[Sp_Invoice]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@Action", "InsertInvoice");
                cmd.Parameters.AddWithValue("@InvoiceNo", obj.InvoiceNo);
                cmd.Parameters.AddWithValue("@PINo", obj.PINo);
                cmd.Parameters.AddWithValue("@CompanyId", obj.CompanyId);
                cmd.Parameters.AddWithValue("@CompanyName", obj.CompanyName);
                cmd.Parameters.AddWithValue("@ProjectId", obj.ProjectId);
                cmd.Parameters.AddWithValue("@ProjectName", obj.ProjectName);
                cmd.Parameters.AddWithValue("@TotalAmount", obj.TotalAmount);
                cmd.Parameters.AddWithValue("@Tax", obj.Tax);
                cmd.Parameters.AddWithValue("@TaxAmount", obj.TaxAmount);
                cmd.Parameters.AddWithValue("@AfterTaxAmount", obj.AfterTaxAmount);
                cmd.Parameters.AddWithValue("@IGST", obj.IGST);
                cmd.Parameters.AddWithValue("@CGST", obj.CGST);
                cmd.Parameters.AddWithValue("@SGST", obj.SGST);
                cmd.Parameters.AddWithValue("@IgstAmount", obj.IgstAmount);
                cmd.Parameters.AddWithValue("@CgstAmount", obj.CgstAmount);
                cmd.Parameters.AddWithValue("@SgstAmount", obj.SgstAmount);
                cmd.Parameters.AddWithValue("@Taxtype", obj.Taxtype);
                cmd.Parameters.AddWithValue("@Type", obj.Type);
                row = cmd.ExecuteNonQuery();
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
            return row;
        }
        public int TAXINVOICESoftware(string InvoiceNo = "", string SoftwareName = "", decimal SAmount = 0.00M, int SUnit = 0, string SDescription = "", string STotal = "")
        {
            int row = 0;
            try
            {
                if (SoftwareName != null)
                {
                    cmd = new SqlCommand("Sp_InvoiceMaster", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.Parameters.AddWithValue("@Action", "InsertSProduct");
                    cmd.Parameters.AddWithValue("@InvoiceNo", InvoiceNo);
                    cmd.Parameters.AddWithValue("@SoftwareName", SoftwareName);
                    cmd.Parameters.AddWithValue("@Amount", SAmount);
                    cmd.Parameters.AddWithValue("@Unit", SUnit);
                    cmd.Parameters.AddWithValue("@DescriptionS", SDescription);
                    cmd.Parameters.AddWithValue("@TotalAmount", STotal);
                    row = cmd.ExecuteNonQuery();
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
            return row;
        }
        public int InvoiceHardware(string InvoiceNo = "", string HardwareName = "", string HDescription = "", string HAmount = "", string HUnit = "", string HTotal = "")
        {
            int row = 0;
            try
            {
                cmd = new SqlCommand("[Sp_InvoiceMaster]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@Action", "InsertHProduct");
                cmd.Parameters.AddWithValue("@InvoiceNo", InvoiceNo);
                cmd.Parameters.AddWithValue("@ProjectName", HardwareName);
                cmd.Parameters.AddWithValue("@HDescription", HDescription);
                cmd.Parameters.AddWithValue("@HAmount", HAmount);
                cmd.Parameters.AddWithValue("@HUnit", HUnit);
                cmd.Parameters.AddWithValue("@HTotalAmount", HTotal);
                row = cmd.ExecuteNonQuery();
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
            return row;
        }
        public int InvoiceProduct(string InvoiceNo = "", string Services = "", string ServicesName = "", string Duration = "", string DurationTime = "", string StartDate = "", decimal Amount = 0.00M, string Description = "", string CompanyId = "", string ProjectId = "")
        {
            int row = 0;
            try
            {
                cmd = new SqlCommand("Sp_InvoiceMaster", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@Action", "InsertProduct");
                cmd.Parameters.AddWithValue("@InvoiceNo", InvoiceNo);
                cmd.Parameters.AddWithValue("@Services", Services);
                cmd.Parameters.AddWithValue("@ServicesName", ServicesName);
                cmd.Parameters.AddWithValue("@Duration", Duration);
                cmd.Parameters.AddWithValue("@DurationTime", DurationTime);
                cmd.Parameters.AddWithValue("@StartDate", StartDate);
                cmd.Parameters.AddWithValue("@Amount", Amount);
                cmd.Parameters.AddWithValue("@Description", Description);
                cmd.Parameters.AddWithValue("@CompanyId", CompanyId);
                cmd.Parameters.AddWithValue("@ProjectId", ProjectId);
                row = cmd.ExecuteNonQuery();
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
            return row;
        }
        public int UpdateTaxInvoice(performa obj)
        {
            int row = 0;
            try
            {
                cmd = new SqlCommand("Sp_InvoiceMaster", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();

                cmd.Parameters.AddWithValue("@Action", "UpdateTaxInvoice");
                cmd.Parameters.AddWithValue("@InvoiceNo", obj.InvoiceNo);
                cmd.Parameters.AddWithValue("@CompanyId", obj.CompanyId);
                cmd.Parameters.AddWithValue("@CompanyName", obj.CompanyName);
                cmd.Parameters.AddWithValue("@TotalAmount", obj.TotalAmount);
                cmd.Parameters.AddWithValue("@Tax", obj.Tax);
                cmd.Parameters.AddWithValue("@TaxAmount", obj.TaxAmount);
                cmd.Parameters.AddWithValue("@AfterTaxAmount", obj.AfterTaxAmount);
                cmd.Parameters.AddWithValue("@IGST", obj.IGST);
                cmd.Parameters.AddWithValue("@CGST", obj.CGST);
                cmd.Parameters.AddWithValue("@SGST", obj.SGST);
                cmd.Parameters.AddWithValue("@IgstAmount", obj.IgstAmount);
                cmd.Parameters.AddWithValue("@CgstAmount", obj.CgstAmount);
                cmd.Parameters.AddWithValue("@SgstAmount", obj.SgstAmount);
                cmd.Parameters.AddWithValue("@Taxtype", obj.Taxtype);
                row = cmd.ExecuteNonQuery();
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
            return row;
        }
        public int DeleteTaxProduct(string InvoiceNo = "")
        {
            int row = 0;
            try
            {
                cmd = new SqlCommand("Sp_InvoiceMaster", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "DeleteTaxProduct");
                con.Open();
                cmd.Parameters.AddWithValue("@InvoiceNo", InvoiceNo);
                row = cmd.ExecuteNonQuery();
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
            return row;
        }
        public performa EditTax(string InvoiceNo = "")
        {
            performa obj = new performa();
            try
            {
                cmd = new SqlCommand("Sp_InvoiceMaster", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@Action", "EDITInvoice");
                cmd.Parameters.AddWithValue("@InvoiceNo", InvoiceNo);
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        obj = new performa();
                        obj.InvoiceNo = rd["InvoiceNo"].ToString();
                        obj.CompanyId = rd["CompanyId"].ToString();
                        obj.TotalAmount = Convert.ToDecimal(rd["TotalAmount"]);
                        obj.Tax = Convert.ToInt32(rd["Tax"]);
                        obj.TaxAmount = Convert.ToDecimal(rd["TaxAmount"]);
                        obj.AfterTaxAmount = Convert.ToDecimal(rd["AfterTaxAmount"]);
                        obj.Taxtype = rd["Taxtype"].ToString();
                        obj.Type = rd["Type"].ToString();
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
            return obj;
        }
        public List<performa> GetTaxProduct(string InvoiceNo = "")
        {
            List<performa> getproduct = new List<performa>();
            performa obj = new performa();
            try
            {
                cmd = new SqlCommand("Sp_InvoiceMaster", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "TaxProduct");
                cmd.Parameters.AddWithValue("@InvoiceNo", InvoiceNo);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        obj = new performa();
                        if (rd["Services"] != DBNull.Value)
                        {
                            obj.Services1 = rd["Services"].ToString();
                        }
                        if (rd["ServicesName"] != DBNull.Value)
                        {
                            obj.Servicesnm = rd["ServicesName"].ToString();
                        }
                        if (rd["Duration"] != DBNull.Value)
                        {
                            obj.Duration1 = rd["Duration"].ToString();
                        }
                        if (rd["DurationTime"] != DBNull.Value)
                        {
                            obj.Durationtm = rd["DurationTime"].ToString();
                        }
                        if (rd["Amount"] != DBNull.Value)
                        {
                            obj.Amount1 = Convert.ToDecimal(rd["Amount"]);
                        }
                        if (rd["Description"] != DBNull.Value)
                        {
                            obj.Description1 = rd["Description"].ToString();
                        }
                        if (rd["StartDate"] != DBNull.Value)
                        {
                            obj.Date1 = rd["StartDate"].ToString();
                        }

                        getproduct.Add(obj);
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
            return getproduct;
        }
        //get tax product for software
        public List<performa> GetTaxProductSoftware(string InvoiceNo = "")
        {
            List<performa> getproduct = new List<performa>();
            performa obj = new performa();
            try
            {
                cmd = new SqlCommand("Sp_InvoiceMaster", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "TaxSProduct");
                cmd.Parameters.AddWithValue("@InvoiceNo", InvoiceNo);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        obj = new performa();
                        if (rd["SoftwareName"] != DBNull.Value)
                        {
                            obj.SoftName = rd["SoftwareName"].ToString();
                        }
                        if (rd["Amount"] != DBNull.Value)
                        {
                            obj.SoftAmount = rd["Amount"].ToString();
                        }
                        if (rd["Unit"] != DBNull.Value)
                        {
                            obj.SoftUnit = rd["Unit"].ToString();
                        }
                        if (rd["DescriptionS"] != DBNull.Value)
                        {
                            obj.SoftDescription = rd["DescriptionS"].ToString();
                        }
                        if (rd["TotalAmount"] != DBNull.Value)
                        {
                            obj.SoftTotal = rd["TotalAmount"].ToString();
                        }
                        getproduct.Add(obj);
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
            return getproduct;
        }

        //get tax product for hardware
        public List<performa> GetTaxProductHardware(string InvoiceNo = "")
        {
            List<performa> getproduct = new List<performa>();
            performa obj = new performa();
            try
            {
                cmd = new SqlCommand("Sp_InvoiceMaster", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "TaxHProduct");
                cmd.Parameters.AddWithValue("@InvoiceNo", InvoiceNo);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        obj = new performa();

                        if (rd["ProjectName"] != DBNull.Value)
                        {
                            obj.ProductName = rd["ProjectName"].ToString();
                        }
                        if (rd["HDescription"] != DBNull.Value)
                        {
                            obj.HNaration = rd["HDescription"].ToString();

                        }
                        if (rd["HAmount"] != DBNull.Value)
                        {
                            obj.HUnitAmount = rd["HAmount"].ToString();
                        }
                        if (rd["HUnit"] != DBNull.Value)
                        {
                            obj.HwUnit = rd["HUnit"].ToString();
                        }
                        if (rd["HTotalAmount"] != DBNull.Value)
                        {
                            obj.HwTotal = rd["HTotalAmount"].ToString();
                        }
                        getproduct.Add(obj);
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
            return getproduct;
        }
        public int DeleteTaxInvoice(string InvoiceNo = "")
        {
            int row = 0;
            try
            {
                cmd = new SqlCommand("Sp_InvoiceMaster", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "DeleteTaxInvoice");
                con.Open();
                cmd.Parameters.AddWithValue("@InvoiceNo", InvoiceNo);
                row = cmd.ExecuteNonQuery();
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
            return row;
        }
        public performa GetInvoicePreview(string InvoiceNo = "")
        {
            performa obj = new performa();
            try
            {
                cmd = new SqlCommand("Sp_InvoiceMaster", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "InvoiceDetail");
                cmd.Parameters.AddWithValue("@InvoiceNo", InvoiceNo);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        obj = new performa();
                        obj.Tax = Convert.ToDecimal(rd["Tax"]);
                        obj.TaxAmount = Convert.ToDecimal(rd["TaxAmount"]);
                        obj.AfterTaxAmount = Convert.ToDecimal(rd["AfterTaxAmount"]);
                        obj.TotalAmount = Convert.ToDecimal(rd["TotalAmount"]);
                        obj.ClientName = rd["CompanyName"].ToString();
                        obj.Address = rd["Address"].ToString();
                        obj.ClientEmail = rd["EmailId"].ToString();
                        obj.ClientPhone = rd["ContactNo"].ToString();
                        if (rd["GSTNo"] != DBNull.Value)
                        {
                            obj.ClientGST = rd["GSTNo"].ToString();
                        }
                        else
                        {
                            obj.ClientGST = "N/A";
                        }
                        obj.ClientPAN = rd["PANNo"].ToString();
                        obj.ClientStatecode = rd["Statecode"].ToString();
                        obj.PINo = rd["PINo"].ToString();
                        obj.Date1 = rd["StartDate"].ToString();
                        obj.IGST = Convert.ToInt32(rd["IGST"]);
                        obj.IgstAmount = Convert.ToDecimal(rd["IgstAmount"]);
                        obj.CGST = Convert.ToInt32(rd["CGST"]);
                        obj.CgstAmount = Convert.ToDecimal(rd["CgstAmount"]);
                        obj.SGST = Convert.ToInt32(rd["SGST"]);
                        obj.SgstAmount = Convert.ToDecimal(rd["SgstAmount"]);
                        obj.Taxtype = rd["Taxtype"].ToString();
                        obj.Type = rd["Type"].ToString();
                        obj.InvoiceNo = InvoiceNo;
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
            return obj;
        }
        public int ReassignLead(Lead obj)
        {
            int row = 0;
            string EmpName = "";
            EmpName = ((Login)HttpContext.Current.Session["Login"]).EmployeeName;
            if (EmpName == null)
            {
                EmpName = "Admin";
            }
            try
            {
                cmd = new SqlCommand("Sp_LeadManagement", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@Action", "ReAssignLead");
                //cmd.Parameters.AddWithValue("@LeadId", obj.LeadId);
                cmd.Parameters.AddWithValue("@EmployeeId", obj.EmployeeId);
                cmd.Parameters.AddWithValue("@EmployeeName", obj.EmployeeName);
                cmd.Parameters.AddWithValue("@AssignDate", obj.AssignDate);
                cmd.Parameters.AddWithValue("@LeadId", obj.LeadId);
                //cmd.Parameters.AddWithValue("@AssignBy", EmpName);
                row = cmd.ExecuteNonQuery();
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
            return row;
        }
        public int InsertEmpLeadStatus(Lead obj)
        {
            int row = 0;
            int EmpId = 0;
            string EmpName = "";
            EmpId = ((Login)HttpContext.Current.Session["Login"]).EmpId;
            EmpName = ((Login)HttpContext.Current.Session["Login"]).EmployeeName;
            try
            {
                cmd = new SqlCommand("Sp_LeadManagement", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "InsertStatushISTORY");
                con.Open();
                cmd.Parameters.AddWithValue("@LeadDescription", obj.EmpDescription);
                cmd.Parameters.AddWithValue("@LeadId", obj.LeadId);
                cmd.Parameters.AddWithValue("@Status", obj.LeadStatus);
                if (obj.LeadStatus == "FollowUp")
                {
                    cmd.Parameters.AddWithValue("@MeetingDate", obj.MeetingDate);
                }
                else if (obj.LeadStatus == "Meeting")
                {
                    cmd.Parameters.AddWithValue("@MeetingDate", obj.MeetingDate);
                }
                cmd.Parameters.AddWithValue("@EmployeeId", EmpId);
                cmd.Parameters.AddWithValue("@EmployeeName", EmpName);
                row = cmd.ExecuteNonQuery();
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
            return row;
        }
        public List<Lead> GetStatusHistory(int LeadId = 0)
        {
            List<Lead> list = new List<Lead>();
            Lead obj = new Lead();
            try
            {
                cmd = new SqlCommand("Sp_LeadManagement", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "StatusHistory");
                cmd.Parameters.AddWithValue("@LeadId", LeadId);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        obj = new Lead();
                        obj.LeadId = rd["LeadId"].ToString();
                        obj.EmployeeName = rd["EmployeeName"].ToString();
                        obj.LeadDescription = rd["LeadDescription"].ToString();
                        obj.LeadStatus = rd["Status"].ToString();
                        obj.MeetingDate = rd["MeetingDate"].ToString();
                        list.Add(obj);
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
            return list;
        }
        public List<Lead> TodayLeadShowForEmp()
        {
            List<Lead> list = new List<Lead>();
            Lead obj = new Lead();
            int EmpId = 0;
            EmpId = ((Login)HttpContext.Current.Session["Login"]).EmpId;
            try
            {
                cmd = new SqlCommand("Sp_LeadManagement", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "ShowHistoryToady");
                cmd.Parameters.AddWithValue("@EmployeeId", EmpId);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        obj = new Lead();
                        //obj.LeadId = rd["LeadId"].ToString();
                        //obj.EmployeeName = rd["EmployeeName"].ToString();
                        obj.LeadDescription = rd["LeadDescription"].ToString();
                        obj.LeadStatus = rd["LeadStatus"].ToString();
                        obj.MeetingDate = rd["AssignDate"].ToString();
                        obj.ClientName = rd["ClientName"].ToString();
                        obj.LeadType = rd["LeadType"].ToString();
                        list.Add(obj);
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
            return list;
        }
        public List<Lead> TommorowLeadShowForEmp()
        {
            List<Lead> list = new List<Lead>();
            Lead obj = new Lead();
            int EmpId = 0;
            EmpId = ((Login)HttpContext.Current.Session["Login"]).EmpId;
            try
            {
                cmd = new SqlCommand("Sp_LeadManagement", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "ShowHistoryTommorow");
                cmd.Parameters.AddWithValue("@EmployeeId", EmpId);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        obj = new Lead();
                        obj.LeadId = rd["LeadId"].ToString();
                        obj.EmployeeName = rd["EmployeeName"].ToString();
                        obj.LeadDescription = rd["Description"].ToString();
                        obj.LeadStatus = rd["Status"].ToString();
                        obj.MeetingDate = rd["MeetingDate"].ToString();
                        obj.ClientName = rd["ClientName"].ToString();
                        obj.LeadType = rd["LeadType"].ToString();
                        list.Add(obj);
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
            return list;
        }
        public int AddLadger(Payments obj)
        {
            int row = 0;
            try
            {
                cmd = new SqlCommand("Sp_PaymentMaster", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "AddLadger");
                con.Open();
                cmd.Parameters.AddWithValue("@CompanyId", obj.CompanyId);
                cmd.Parameters.AddWithValue("@CompanyName", obj.CompanyName);
                cmd.Parameters.AddWithValue("@TotalAmount", obj.TotalAmount);
                cmd.Parameters.AddWithValue("@Payment", obj.Payment);
                cmd.Parameters.AddWithValue("@RemainingAmount", obj.RemainingAmount);
                cmd.Parameters.AddWithValue("@PaymentMode", obj.PaymentMode);
                cmd.Parameters.AddWithValue("@BankName", obj.BankName);
                cmd.Parameters.AddWithValue("@ChequeNo", obj.ChequeNo);
                cmd.Parameters.AddWithValue("@chequeDate", obj.chequeDate);
                cmd.Parameters.AddWithValue("@UTRNO", obj.UTRNO);
                cmd.Parameters.AddWithValue("@Remark", obj.Remark);
                cmd.Parameters.AddWithValue("@Date", obj.Date);
                cmd.Parameters.AddWithValue("@PINo", obj.PINo);
                row = cmd.ExecuteNonQuery();
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
            return row;
        }
        public int AddTaxInvoiceLadger(string CompanyId, string CompanyName, decimal TotalAmount, string Payment, string RemainingAmount, string InvoiceNo)
        {
            int row = 0;
            try
            {
                cmd = new SqlCommand("Sp_PaymentMaster", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "AddTaxLadger");
                con.Open();
                cmd.Parameters.AddWithValue("@CompanyId", CompanyId);
                cmd.Parameters.AddWithValue("@CompanyName", CompanyName);
                cmd.Parameters.AddWithValue("@TotalAmount", TotalAmount);
                cmd.Parameters.AddWithValue("@Payment", Payment);
                cmd.Parameters.AddWithValue("@RemainingAmount", RemainingAmount);
                cmd.Parameters.AddWithValue("@PINo", InvoiceNo);
                row = cmd.ExecuteNonQuery();
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
            return row;
        }

        public List<Payments> GetClientLadger(int CompanyId)
        {
            List<Payments> getlist = new List<Payments>();
            Payments obj = new Payments();
            try
            {
                cmd = new SqlCommand("Sp_PaymentMaster", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "ShowLadger");
                cmd.Parameters.AddWithValue("@CompanyId", CompanyId);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        obj.PINo = rd["PINo"].ToString();
                        obj.CompanyName = rd["CompanyName"].ToString();
                        obj.TotalAmount = Convert.ToDecimal(rd["TotalAmount"]);
                        obj.Payment = Convert.ToDecimal(rd["Payment"]);
                        obj.RemainingAmount = Convert.ToDecimal(rd["RemainingAmount"]);
                        obj.Date = rd["Date"].ToString();
                        getlist.Add(obj);
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
            return getlist;
        }
        public Payments GetPaymentsfORpAYMENT(int CompanyId = 0)
        {
            Payments obj = new Payments();
            try
            {
                cmd = new SqlCommand("Sp_PaymentMaster", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "TotalAmount");
                con.Open();
                cmd.Parameters.AddWithValue("@CompanyId", CompanyId);
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        obj = new Payments();
                        obj.TotalAmount = Convert.ToDecimal(rd["TotalAmount"]);
                        if (rd["PaymentAmount"] != DBNull.Value)
                        {
                            obj.Payment = Convert.ToDecimal(rd["PaymentAmount"]);
                        }
                        else
                        {
                            obj.Payment = 0;
                        }
                        obj.RemainingAmount = obj.TotalAmount - obj.Payment;
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
            return obj;
        }
        public TokenManagement GetInboxDetail(int Id = 0)
        {

            TokenManagement obj = new TokenManagement();
            try
            {
                cmd = new SqlCommand("Sp_TokenManagement", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@Action", "SelectDetail");
                cmd.Parameters.AddWithValue("@Id", Id);
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        obj = new TokenManagement();
                        obj.Id = Convert.ToInt32(rd["Id"]);
                        obj.Subject = rd["Subject"].ToString();
                        obj.Message = rd["Message"].ToString();
                        obj.FilePath = rd["Files"].ToString();
                        obj.UserId = rd["UserId"].ToString();
                        obj.TokenId = rd["TokenId"].ToString();
                        obj.Status = rd["Status"].ToString();
                        obj.Date = rd["Date"].ToString();
                        obj.From = rd["Froms"].ToString();
                        obj.Department = rd["Department"].ToString();
                        obj.Periority = rd["Periority"].ToString();
                        //obj.CompanyName = rd["CompanyName"].ToString();
                        //obj.ContactNo = rd["ContactNo"].ToString();
                        //obj.EmailId = rd["EmailId"].ToString();
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
            return obj;
        }
        public int InsertComment(TokenManagement obj)
        {
            int row = 0;
            string UserId = "";
            UserId = ((Login)HttpContext.Current.Session["Login"]).UserName;
            try
            {
                cmd = new SqlCommand("Sp_TokenManagement", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@Action", "insertcomment");
                cmd.Parameters.AddWithValue("@Id", obj.Id);
                cmd.Parameters.AddWithValue("@Comment", obj.Comments);
                cmd.Parameters.AddWithValue("@Status", obj.Status);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                row = cmd.ExecuteNonQuery();
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
            return row;
        }
        public List<TokenManagement> Getcomment(int Id = 0)
        {
            List<TokenManagement> list = new List<TokenManagement>();
            TokenManagement obj = new TokenManagement();
            try
            {
                cmd = new SqlCommand("Sp_TokenManagement", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@Action", "BindComment");
                cmd.Parameters.AddWithValue("@Id", Id);
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        obj = new TokenManagement();
                        obj.Comment = rd["Comment"].ToString();
                        obj.Status = rd["Status"].ToString();
                        //obj.Date = rd["Date"].ToString();
                        //if (rd["ContactPerson"] != DBNull.Value)
                        //{
                        //    obj.CompanyName = rd["ContactPerson"].ToString();
                        //}
                        //else
                        //{
                        //    obj.CompanyName = "Macreel Team";
                        //}
                        list.Add(obj);
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
            return list;
        }
        public List<TokenManagement> GetTransh()
        {
            List<TokenManagement> getlist = new List<TokenManagement>();
            TokenManagement obj = new TokenManagement();
            string UserName = "";
            UserName = ((Login)HttpContext.Current.Session["Login"]).UserName;
            try
            {
                cmd = new SqlCommand("Sp_TokenManagement", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@Action", "Transh");
                cmd.Parameters.AddWithValue("@UserId", UserName);
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        obj = new TokenManagement();
                        obj.Id = Convert.ToInt32(rd["Id"]);
                        obj.Subject = rd["Subject"].ToString();
                        obj.Message = rd["Message"].ToString();
                        obj.FilePath = rd["Files"].ToString();
                        obj.UserId = rd["UserId"].ToString();
                        obj.TokenId = rd["TokenId"].ToString();
                        obj.Status = rd["Status"].ToString();
                        obj.Date = rd["Date"].ToString();
                        obj.From = rd["Froms"].ToString();
                        obj.Department = rd["Department"].ToString();
                        obj.Periority = rd["Priority"].ToString();
                        obj.CompanyName = rd["CompanyName"].ToString();
                        obj.ContactNo = rd["ContactNo"].ToString();
                        //if (rd["Comment"] != DBNull.Value)
                        //{
                        //    obj.Comment = Convert.ToInt32(rd["Comment"]);
                        //}
                        //else
                        //{
                        //    obj.Comment = 0;
                        //}
                        getlist.Add(obj);
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
            return getlist;
        }
        public int DeleteToken(string Id)
        {
            int row = 0;
            try
            {
                cmd = new SqlCommand("Sp_TokenManagement", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@Action", "delete");
                cmd.Parameters.AddWithValue("@Id", Id);
                row = cmd.ExecuteNonQuery();
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
            return row;
        }
        public int AddClientAccount(Client obj)
        {
            int row = 0;
            try
            {
                cmd = new SqlCommand("Sp_Client", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@Action", "AddClientAccount");
                cmd.Parameters.AddWithValue("@Id", obj.Id);
                cmd.Parameters.AddWithValue("@UserName", obj.UserName);
                cmd.Parameters.AddWithValue("@Password", obj.Password);
                row = cmd.ExecuteNonQuery();
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
            return row;
        }
        public List<Project> GetClientProjectList()
        {
            List<Project> gtlst = new List<Project>();
            Project obj = new Project();
            int ClientId = 0;
            ClientId = ((Login)HttpContext.Current.Session["Login"]).ClientId;
            try
            {
                cmd = new SqlCommand("Sp_Project", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "ClientProject");
                cmd.Parameters.AddWithValue("@Id", ClientId);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        obj = new Project();
                        obj.Id = Convert.ToInt32(rd["Id"]);
                        //obj.CompanyName = rd["CompanyName"].ToString();
                        obj.ProjectName = rd["ProjectName"].ToString();
                        //obj.ProjectStartingDate = rd["ProjectStartingDate"].ToString();
                        //obj.CompletionDate = rd["CompletionDate"].ToString();
                        obj.ProjectDeliveryDate = rd["ProjectDeliveryDate"].ToString();
                        obj.TotalAmount = Convert.ToDecimal(rd["TotalAmount"]);
                        //obj.ProjectCode = rd["ProjectCode"].ToString();
                        gtlst.Add(obj);
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
            return gtlst;
        }
        public List<performa> GetClientGanratedInvoiceList()
        {
            List<performa> list = new List<performa>();
            performa obj = new performa();
            int ClientId = 0;
            ClientId = ((Login)HttpContext.Current.Session["Login"]).ClientId;
            try
            {
                cmd = new SqlCommand("Sp_InvoiceMaster", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "GanratedPIList");
                cmd.Parameters.AddWithValue("@CompanyId", ClientId);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        obj = new performa();
                        obj.InvoiceNo = rd["InvoiceNo"].ToString();
                        obj.CompanyName = rd["CompanyName"].ToString();
                        obj.TotalAmount = Convert.ToDecimal(rd["TotalAmount"]);
                        obj.AfterTaxAmount = Convert.ToDecimal(rd["AfterTaxAmount"]);
                        list.Add(obj);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return list;
        }
        public int UpdateRenewProduct(performa obj)
        {
            int row = 0;
            try
            {
                cmd = new SqlCommand("Sp_RenewProduct", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                if (obj.Status == 1)
                {
                    cmd.Parameters.AddWithValue("@Action", "InsertTaxInvoiceOfRenewProduct");
                    cmd.Parameters.AddWithValue("@CompanyId", obj.CompanyId);
                    cmd.Parameters.AddWithValue("@CompanyName", obj.CompanyName);
                    cmd.Parameters.AddWithValue("@InvoiceNo", obj.InvoiceNo);
                    cmd.Parameters.AddWithValue("@TotalAmount", obj.TotalAmount);
                    cmd.Parameters.AddWithValue("@AfterTaxAmount", obj.TotalAmount);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Action", "RejectRenewProduct");
                    cmd.Parameters.AddWithValue("@Status", obj.Status);
                }
                row = cmd.ExecuteNonQuery();
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
            return row;
        }
        public int DeleteAssignEmployee(string ProjectCode = "")
        {
            int row = 0;
            try
            {
                cmd = new SqlCommand("Sp_Project", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "DeleteEmployee");
                con.Open();
                cmd.Parameters.AddWithValue("@ProjectCode", ProjectCode);
                row = cmd.ExecuteNonQuery();
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
            return row;
        }
        public List<SelectListItem> BindProjectForTaskSheet()
        {
            List<SelectListItem> getlist = new List<SelectListItem>();
            SelectListItem obj = new SelectListItem();
            int EmpId = 0;
            EmpId = ((Login)HttpContext.Current.Session["Login"]).EmpId;
            try
            {
                cmd = new SqlCommand("Sp_Project", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@Action", "BindProject");
                cmd.Parameters.AddWithValue("@EmployeeId", EmpId);
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        obj = new SelectListItem();
                        //obj.Value = rd["ProjectCode"].ToString();
                        obj.Text = rd["ProjectName"].ToString();
                        getlist.Add(obj);
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
            return getlist;
        }
        public List<TaskSheets> CheckTaskSheetDate(string Date = "")
        {
            List<TaskSheets> list = new List<TaskSheets>();
            TaskSheets obj = new TaskSheets();
            int EmpId = 0;
            EmpId = ((Login)HttpContext.Current.Session["Login"]).EmpId;
            try
            {
                cmd = new SqlCommand("Sp_TaskSheet", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "ChkDate");
                cmd.Parameters.AddWithValue("@EmployeeId", EmpId);
                cmd.Parameters.AddWithValue("@Date", Date);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        obj = new TaskSheets();
                        obj.Date1 = rd["Date"].ToString();
                        list.Add(obj);
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
            return list;
        }
        public List<TaskSheets> EditTaskSheet(string Date = "")
        {
            TaskSheets obj = new TaskSheets();
            List<TaskSheets> list = new List<TaskSheets>();
            int EmpId = 0;
            EmpId = ((Login)HttpContext.Current.Session["Login"]).EmpId;
            try
            {
                cmd = new SqlCommand("Sp_TaskSheet", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@Action", "EditTask");
                cmd.Parameters.AddWithValue("@EmployeeId", EmpId);
                cmd.Parameters.AddWithValue("@Date", Date);
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        obj = new TaskSheets();
                        obj.Date1 = rd["Date"].ToString();
                        obj.Project1 = rd["Project"].ToString();
                        obj.Task1 = rd["Task"].ToString();
                        obj.Hour1 = rd["Hours"].ToString();
                        obj.Description1 = rd["Description"].ToString();
                        obj.Status1 = rd["Status"].ToString();
                        obj.Remark1 = rd["Remark"].ToString();
                        list.Add(obj);
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
            return list;
        }
        public int DeleteTask()
        {
            int row = 0;
            string Today = DateTime.Today.ToString("dd-MM-yyyy");
            int EmpId = 0;
            EmpId = ((Login)HttpContext.Current.Session["Login"]).EmpId;
            try
            {
                cmd = new SqlCommand("Sp_TaskSheet", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.Parameters.AddWithValue("@Action", "deleteTask");
                cmd.Parameters.AddWithValue("@EmployeeId", EmpId);
                cmd.Parameters.AddWithValue("@Date", Today);
                row = cmd.ExecuteNonQuery();
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
            return row;
        }
        public List<ApplyLeave> PemdingApplyLeave()
        {
            List<ApplyLeave> gtlst = new List<ApplyLeave>();
            ApplyLeave obj = new ApplyLeave();
            int EmpId = 0;
            EmpId = ((Login)HttpContext.Current.Session["Login"]).EmpId;
            try
            {
                cmd = new SqlCommand("Sp_LeaveManagement", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "PendingEmpLeave");
                cmd.Parameters.AddWithValue("@EmployeeId", EmpId);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        obj = new ApplyLeave();
                        obj.Id = Convert.ToInt32(rd["Id"]);
                        obj.EmployeeName = rd["EmployeeName"].ToString();
                        obj.ToDate = rd["ToDate"].ToString();
                        obj.FromDate = rd["FromDate"].ToString();
                        obj.LeaveType = rd["LeaveType"].ToString();
                        obj.Description = rd["Description"].ToString();
                        obj.AppliedDate = rd["AppliedDate"].ToString();
                        obj.AdminDescription = rd["AdminDescription"].ToString();
                        obj.LeaveCount = Convert.ToInt32(rd["LeaveCount"]);
                        obj.Status = rd["Status"].ToString();
                        gtlst.Add(obj);
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
            return gtlst;
        }
        public List<ApplyLeave> RejectApplyLeave()
        {
            List<ApplyLeave> gtlst = new List<ApplyLeave>();
            ApplyLeave obj = new ApplyLeave();
            int EmpId = 0;
            EmpId = ((Login)HttpContext.Current.Session["Login"]).EmpId;
            try
            {
                cmd = new SqlCommand("Sp_LeaveManagement", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "RejectEmpLeave");
                cmd.Parameters.AddWithValue("@EmployeeId", EmpId);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        obj = new ApplyLeave();
                        obj.Id = Convert.ToInt32(rd["Id"]);
                        obj.EmployeeName = rd["EmployeeName"].ToString();
                        obj.ToDate = rd["ToDate"].ToString();
                        obj.FromDate = rd["FromDate"].ToString();
                        obj.LeaveType = rd["LeaveType"].ToString();
                        obj.Description = rd["Description"].ToString();
                        obj.AppliedDate = rd["AppliedDate"].ToString();
                        obj.AdminDescription = rd["AdminDescription"].ToString();
                        obj.LeaveCount = Convert.ToInt32(rd["LeaveCount"]);
                        obj.Status = rd["Status"].ToString();
                        gtlst.Add(obj);
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
            return gtlst;
        }
        public List<ApplyLeave> ApprovedApplyLeave()
        {
            List<ApplyLeave> gtlst = new List<ApplyLeave>();
            ApplyLeave obj = new ApplyLeave();
            int EmpId = 0;
            EmpId = ((Login)HttpContext.Current.Session["Login"]).EmpId;
            try
            {
                cmd = new SqlCommand("Sp_LeaveManagement", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "ApprovedEmpLeave");
                cmd.Parameters.AddWithValue("@EmployeeId", EmpId);
                con.Open();
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        obj = new ApplyLeave();
                        obj.Id = Convert.ToInt32(rd["Id"]);
                        obj.EmployeeName = rd["EmployeeName"].ToString();
                        obj.ToDate = rd["ToDate"].ToString();
                        obj.FromDate = rd["FromDate"].ToString();
                        obj.LeaveType = rd["LeaveType"].ToString();
                        obj.Description = rd["Description"].ToString();
                        obj.AppliedDate = rd["AppliedDate"].ToString();
                        obj.AdminDescription = rd["AdminDescription"].ToString();
                        obj.LeaveCount = Convert.ToInt32(rd["LeaveCount"]);
                        obj.Status = rd["Status"].ToString();
                        gtlst.Add(obj);
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
            return gtlst;
        }
        //compose mail message 
        public void sendemail(string email, string messagebody)
        {
            //compose mail message and set all properties
            MailMessage message = new MailMessage("macreel@gmail.com", email);
            message.Subject = "Registration Successfull";
            message.Body = messagebody;
            message.IsBodyHtml = true;
            //send mailmessage by using smtpclient
            SmtpClient smtp = new SmtpClient();
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.Host = "smtp.gmail.com";
            smtp.Credentials = new NetworkCredential("macreel@gmail.com", "ryhnhbptuymbsscw");
            smtp.Send(message);

        }
        public Macreel_Project.Models.Bussiness.TaskManage gettaskById(string id)
        {
            Macreel_Project.Models.Bussiness.TaskManage task = new Macreel_Project.Models.Bussiness.TaskManage();

            if (id != null && id != "")
            {

                con.Open();
                SqlCommand cmd = new SqlCommand("sp_TaskManage", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Action", "select_taskById");
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader sdr = cmd.ExecuteReader();

                if (sdr.HasRows)
                {
                    sdr.Read();
                    task.id = sdr["id"].ToString();
                    task.title = sdr["title"].ToString();
                    task.description = sdr["description"].ToString();
                    task.complete_date = sdr["completion_date"].ToString();
                    task.current_date = sdr["curent_date"].ToString();
                    task.attachment1 = sdr["attachment1"].ToString();
                    task.attachment2 = sdr["attachment2"].ToString();
                    task.attachment3 = sdr["attachment3"].ToString();
                    task.attachment4 = sdr["attachment4"].ToString();
                    task.attachment5 = sdr["attachment5"].ToString();
                    task.assigned_emp = sdr["assigned_employee"].ToString();
                    task.task_status = sdr["task_status"].ToString();
                    task.emp_status = sdr["emp_status"].ToString();
                    task.updatedDateEmp = sdr["updatedDateEmp"].ToString();
                    task.s_no = sdr["s_no"].ToString();

                }
                sdr.Close();
                con.Close();
            }


            return task;
        }

        #endregion
        public DataTable download_report(Macreel_Project.Models.Bussiness.filter_report filter_report)
        {
            DataTable dt = new DataTable();

            var sts = HttpContext.Current.Session["Status"];

            string conditions = "where task_status='" + sts + "'";
            if (filter_report.emp_name != null || filter_report.assigned_date != null)
            {
                if (filter_report.assigned_date != null && filter_report.assigned_date != "")
                {
                    try
                    {
                        if (conditions != "")
                        {
                            conditions = conditions + " and assigned_date BETWEEN '" + filter_report.assigned_date.Split('/')[2] + "-" + filter_report.assigned_date.Split('/')[1] + "-" + filter_report.assigned_date.Split('/')[0] + "' and '" + filter_report.toassigned_date.Split('/')[2] + "-" + filter_report.toassigned_date.Split('/')[1] + "-" + filter_report.toassigned_date.Split('/')[0] + "'";
                        }
                    }
                    catch
                    {

                    }
                }

                if (filter_report.emp_name != null && filter_report.emp_name != "")
                {

                    if (conditions != "")
                    {
                        conditions = conditions + " and EmployeeName ='" + filter_report.emp_name.ToString().Replace("'", "").ToLower() + "'";
                    }
                    else
                    {
                        conditions = conditions + " EmployeeName ='" + filter_report.emp_name.ToString().Replace("'", "").ToLower() + "'";
                    }
                }
                if (conditions != "")
                {
                    conditions = "" + conditions + "";
                }
            }
            string sql = "SELECT emp.EmployeeName, emp.id AS emp_id, task.* FROM tbl_taskManage AS task LEFT JOIN TblEmployee AS emp ON task.assigned_employee = emp.id " + conditions + "";
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    dt.Load(sdr); // Load the result into the DataTable
                }
            }
            catch
            {
            }
            con.Close();
            return dt;
        }
        public DataTable download_reportByReportingManager(Macreel_Project.Models.Bussiness.filter_report filter_report)
        {
            DataTable dt = new DataTable();

            // Assuming you have a mechanism to safely cast or retrieve these session values
            var sts = HttpContext.Current.Session["Status"]?.ToString();
            var login = HttpContext.Current.Session["Login"] as Login;
            var userid = login?.EmpId;
            // Adjust according to your actual login object's property

            List<SqlParameter> parameters = new List<SqlParameter>();
            string whereClause = " WHERE emp.ReportingManager = @UserId"; // Initial WHERE clause

            parameters.Add(new SqlParameter("@UserId", userid ?? SqlInt32.Null)); // Assuming userid is an int. Use SqlInt32.Null if userid is null

            if (!string.IsNullOrEmpty(sts))
            {
                whereClause += " AND task.task_status = @Status";
                parameters.Add(new SqlParameter("@Status", sts));
            }

            if (!string.IsNullOrEmpty(filter_report.assigned_date) && !string.IsNullOrEmpty(filter_report.toassigned_date))
            {
                DateTime startDate = DateTime.ParseExact(filter_report.assigned_date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime endDate = DateTime.ParseExact(filter_report.toassigned_date, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                whereClause += " AND assigned_date BETWEEN @StartDate AND @EndDate";
                parameters.Add(new SqlParameter("@StartDate", startDate));
                parameters.Add(new SqlParameter("@EndDate", endDate));
            }

            if (!string.IsNullOrEmpty(filter_report.emp_name))
            {
                whereClause += " AND LOWER(emp.EmployeeName) = LOWER(@EmpName)";
                parameters.Add(new SqlParameter("@EmpName", filter_report.emp_name));
            }

            string sql = $"SELECT emp.EmployeeName, emp.id AS emp_id, task.* FROM tbl_taskManage AS task LEFT JOIN TblEmployee AS emp ON task.assigned_employee = emp.id{whereClause}";

            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddRange(parameters.ToArray());
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    dt.Load(sdr); // Load the result into the DataTable
                }
            }
            catch (Exception ex)
            {
                // Handle exception (log or throw)
            }
            finally
            {
                con.Close();
            }

            return dt;

        }
        public DataTable download_reportForReportingManager(Macreel_Project.Models.Bussiness.filter_report filter_report)
        {
            DataTable dt = new DataTable();

            // Assuming you have a mechanism to safely cast or retrieve these session values
            var sts = HttpContext.Current.Session["Status"]?.ToString();
            var login = HttpContext.Current.Session["Login"] as Login;
            var userid = login?.EmpId;
            // Adjust according to your actual login object's property

            List<SqlParameter> parameters = new List<SqlParameter>();
            string whereClause = " WHERE emp.ReportingManager = @UserId"; // Initial WHERE clause

            parameters.Add(new SqlParameter("@UserId", userid ?? SqlInt32.Null)); // Assuming userid is an int. Use SqlInt32.Null if userid is null

            if (!string.IsNullOrEmpty(sts))
            {
                whereClause += " AND task.task_status = @Status";
                parameters.Add(new SqlParameter("@Status", sts));
            }

            if (!string.IsNullOrEmpty(filter_report.assigned_date) && !string.IsNullOrEmpty(filter_report.toassigned_date))
            {
                DateTime startDate = DateTime.ParseExact(filter_report.assigned_date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime endDate = DateTime.ParseExact(filter_report.toassigned_date, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                whereClause += " AND assigned_date BETWEEN @StartDate AND @EndDate";
                parameters.Add(new SqlParameter("@StartDate", startDate));
                parameters.Add(new SqlParameter("@EndDate", endDate));
            }

            if (!string.IsNullOrEmpty(filter_report.emp_name))
            {
                whereClause += " AND LOWER(emp.EmployeeName) = LOWER(@EmpName)";
                parameters.Add(new SqlParameter("@EmpName", filter_report.emp_name));
            }

            string sql = $"SELECT emp.EmployeeName, emp.id AS emp_id, task.* FROM tbl_taskManage AS task LEFT JOIN TblEmployee AS emp ON task.assigned_employee = emp.id{whereClause}";

            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddRange(parameters.ToArray());
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    dt.Load(sdr); // Load the result into the DataTable
                }
            }
            catch (Exception ex)
            {
                // Handle exception (log or throw)
            }
            finally
            {
                con.Close();
            }

            return dt;

        }


    }
}
