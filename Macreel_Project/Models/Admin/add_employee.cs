using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Macreel_Project.Models.Admin
{
    public class employee_manage
    {
        public string id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string token { get; set; }
        public string Designation { get; set; }
        public string Contact_no { get; set; }
        public string employee_name { get; set; }
        public string isactive { get; set; }
        public string address { get; set; }
        public string join_date { get; set; }
        public string qualification { get; set; }
        public string reporting_manager { get; set; }
        public string emp_branch { get; set; }
        public string emp_type { get; set; }
        public string emp_id { get; set; }
        public string emp_mail { get; set; }
        public string gender { get; set; }
        public string emrgency_no { get; set; }
        public string emergency_PersonName { get; set; }
        public string dob { get; set; }
        public string adhar_no { get; set; }
        public string aadhar_img { get; set; }
        public string pan_no { get; set; }
        public string pan_img { get; set; }
        public string profile_img { get; set; }
        public string country { get; set; }
        public string state { get; set; }
        public string city { get; set; }
        public string emp_department { get; set; }
        public string exit_date { get; set; }
        public string current_status { get; set; }
        public string bankName { get; set; }
        public string Acc_HolderName { get; set; }
        public string Acc_Type { get; set; }
        public string Acc_No { get; set; }
        public string Ifsc_Code { get; set; }
        public string Bank_Address { get; set; }
        public string Bank_City { get; set; }
        public string Bank_State { get; set; }
        public string Bank_Zip { get; set; }
        public string EmployeeTaskPerformance { get; set; }
        public string EmployeeleavePerformance { get; set; }
        public double EmployeeAttendanceperformance { get; set; }
        public int EmployeePunctualityPerformance { get; set; }
        public int total_performance { get; set; }
        public string flag { get; set; }
        public DateTime date { get; set; }
        public HttpPostedFileBase profile_imgupload { get; set; }
        public HttpPostedFileBase aadhar_imgupload { get; set; }
        public HttpPostedFileBase pan_imgUpload { get; set; }
        public List<emp_list> emp_Lists { get; set; }
        public List<designation> designation_list { get; set; }
        public List<department> department_list { get; set; }
        public List<manage_branch> branchList { get; set; }
    }

    public class designation
    {
        public string designationId { get; set; }
        public string designation_name { get; set; }
    }

    public class Manage_notification
    {
        public string id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string employeeId { get; set; }
        public string created_date { get; set; }
    }

    public class department
    {
        public string departmentId { get; set; }
        public string department_name { get; set; }
    }

    public class manage_branch
    {
        public string BranchId { get; set; }
        public string branchName { get; set; }
    }

    public class question_model
    {
        public int id { get; set; }
        public int userid { get; set; }
        public string username { get; set; }
        public string questions { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
    }

    public class survey_question
    {
        public string id { get; set; }
        public string ques { get; set; }
    }

    public class surveyToEmp
    {
        public int id { get; set; }
        public int userId { get; set; }
        public string emp_name { get; set; }
        public string nodata { get; set; }
        public int survey_1 { get; set; }
        public int survey_2 { get; set; }
        public int survey_3 { get; set; }
        public int survey_4 { get; set; }
        public int survey_5 { get; set; }
        public int survey_6 { get; set; }
        public int survey_7 { get; set; }
        public int survey_8 { get; set; }
        public int survey_9 { get; set; }
        public int survey_10 { get; set; }
        public int emp_taskperformance { get; set; }
        public int emp_leaveperformance { get; set; }
        public int EmployeeAttendanceperformance { get; set; }
        public int emp_totalAbsent { get; set; }
        public int emp_totalPresent { get; set; }
        public int comp_workingDays { get; set; }
        public int weekOff { get; set; }
        public TimeSpan total_HrsWorking { get; set; }
        public TimeSpan total_LateIn { get; set; }
        public TimeSpan total_EarlyOut { get; set; }
        public int totalDays_InMonth { get; set; }
        public DateTime fromdate { get; set; }
        public DateTime todate { get; set; }
        public DateTime update_date { get; set; }
        public int flag { get; set; }
        //public List<Macreel_Project.Models.Employee_attendance> sheet { get; set; }
        public List<survey_question> survey_question { get; set; }
        public List<emp_list> emp_list { get; set; }


    }
}