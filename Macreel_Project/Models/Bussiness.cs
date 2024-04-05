using Macreel_Project.Models.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Macreel_Project.Models
{
    public class Bussiness
    {
        public class TaskManage
        {
            public string id { get; set; }
            public string s_no { get; set; }
            public string selectedValues { get; set; }
            public string title { get; set; }
            public string description { get; set; }
            public string complete_date { get; set; }
            public string current_date { get; set; }
            public string attachment1 { get; set; }
            public string attachment2 { get; set; }
            public string attachment3 { get; set; }
            public string attachment4 { get; set; }
            public string attachment5 { get; set; }
            public string assigned_emp { get; set; }
            public string assigned_empId { get; set; }
            public string userid { get; set; }
            public string username { get; set; }
            public string task_status { get; set; }
            public string emp_status { get; set; }
            public string updatedDateEmp { get; set; }
            public string check { get; set; }
            public string select_grp { get; set; }
            public string assigned_by { get; set; }
            public string commentTask { get; set; }
            public string commentByOther { get; set; }
            public HttpPostedFileBase fileupload1 { get; set; }
            public HttpPostedFileBase fileupload2 { get; set; }
            public HttpPostedFileBase fileupload3 { get; set; }
            public HttpPostedFileBase fileupload4 { get; set; }
            public HttpPostedFileBase fileupload5 { get; set; }
            public List<emp_list> emp_Lists { get; set; }
            public List<Manage_Group> grp_list { get; set; }

        }

        public class emp_list
        {
            public string emp_id { get; set; }
            public string emp_name { get; set; }
            public string designation { get; set; }
            public string department { get; set; }
        }

        public class filter_report
        {
            public string emp_name { get; set; }
            public string empid { get; set; }
            public string assigned_date { get; set; }
            public string toassigned_date { get; set; }
            public string status { get; set; }

        }

        public class Notification
        {
            public int Id { get; set; }
            public string Message { get; set; }
            public string EmployeeId { get; set; }
            public bool IsRead { get; set; }

            public virtual Employee Employee { get; set; }
        }

        public class Login
        {
            public int EmpId { get; set; }
            public string Type { get; set; }
            public string UserName { get; set; }
            public string Password { get; set; }
            public string Image { get; set; }
            public string EmployeeName { get; set; }
            public int ClientId { get; set; }
            public string CompanyName { get; set; }
            public string Department { get; set; }
            public int Id { get;set; }
        }
        public class Deprtment
        {
            public int DeptId { get; set; }
            public int DesId { get; set; }
            public string DepartmentName { get; set; }
            public string DesignationName { get; set; }
            public string Type { get; set; }
        }
        public class designation
        {
            public int DId { get; set; }
            public string Name { get; set; }
            public string Type { get; set; }
        }
        public class Employee
        {
            public int Id { get; set; }
            public string EmployeeCode { get; set; }
            public string EmployeeName { get; set; }
            public string Department { get; set; }
            public string Designation { get; set; }
            public string EmailId { get; set; }
            public string DOJ { get; set; }
            public string PanNo { get; set; }
            public string Passport { get; set; }
            public string adharno { get; set; }
            public string Mobile { get; set; }
            public string UserName { get; set; }
            public string Password { get; set; }
            public string BankName { get; set; }
            public string AccountNo { get; set; }
            public string IFSC { get; set; }
            public string BankBranch { get; set; }
            public string BloodGroup { get; set; }
            public string IdMarks { get; set; }
            public string DOB { get; set; }
            public string Age { get; set; }
            public string Sex { get; set; }
            public string Nationality { get; set; }
            public string Religion { get; set; }
            public string MaritalStatus { get; set; }
            public string PresentAddress { get; set; }
            public string State { get; set; }
            public string City { get; set; }
            public string Pin { get; set; }
            public string EmergencyContactPerson { get; set; }
            public string EmergencyContactNumber { get; set; }
            public string EmergencyContectAddress { get; set; }
            public string ReferencesName { get; set; }
            public string ContactNo { get; set; }
            public string CompanyName { get; set; }
            public string CemailId { get; set; }
            public string PreviousEmployer { get; set; }
            public string From { get; set; }
            public string To { get; set; }
            public string Degree { get; set; }
            public string ProfessionalInstitution { get; set; }
            public string ProfessionalPassingYear { get; set; }
            public string ProfessionalSpecilization { get; set; }
            public string Board10 { get; set; }
            public string Institution10 { get; set; }
            public string PassingYear10 { get; set; }
            public string Specilization10 { get; set; }
            public string Board12 { get; set; }
            public string Institution12 { get; set; }
            public string PassingYear12 { get; set; }
            public string Specilization12 { get; set; }
            public string ImagePath { get; set; }
            public HttpPostedFileBase Image { get; set; }
            public string UserId { get; set; }
            public string StateId { get; set; }
            public string ReportingManager { get; set; }
            public DateTime Date { get; set; }
            public string EmployeeTaskPerformance { get; set; }
        }
        public class Client
        {
            public int Id { get; set; }
            public string CompanyName { get; set; }
            public string ContactPerson { get; set; }
            public string ContactNo { get; set; }
            public string GSTNo { get; set; }
            public string State { get; set; }
            public string City { get; set; }
            public string StateName { get; set; }
            public string CityName { get; set; }
            public string Address { get; set; }
            public string Statecode { get; set; }
            public string Pincode { get; set; }
            public string PanNo { get; set; }
            public string EmailId { get; set; }
            public string Designation { get; set; }
            public string UserName { get; set; }
            public string Password { get; set; }
        }
        public class Project
        {
            public int Id { get; set; }
            public string CompanyId { get; set; }
            public string CompanyName { get; set; }
            public string ClientName { get; set; }
            public string ProjectName { get; set; }
            public string ProjectCode { get; set; }
            public string ProjectStartingDate { get; set; }
            public string CompletionDate { get; set; }
            public string ProjectDeliveryDate { get; set; }
            public decimal TotalAmount { get; set; }
            public string ProjectStatus { get; set; }
            public string EmployeeId { get; set; }
            public string EmployeeName { get; set; }
            public string[] Employee { get; set; }
            public string Document { get; set; }
            public HttpPostedFileBase[] Documentation { get; set; }
            public string Description { get; set; }
            public string YourDescription { get; set; }
            public string AssignDate { get; set; }
            public string Date { get; set; }
            public string Message { get; set; }
        }
        public class LeaveTypes
        {
            public int Id { get; set; }
            public string LeaveType { get; set; }
            public string Description { get; set; }
        }
        public class Assignleave
        {
            public string EmployeeId { get; set; }
            public string EmployeeName { get; set; }
            public string[] NoOfLeave { get; set; }
            public string[] LeaveType { get; set; }
            public string Leave { get; set; }
            public string Type { get; set; }
            public string Year { get; set; }

        }
        public class ApplyLeave
        {
            public int Id { get; set; }
            public string EmployeeName { get; set; }
            public string EmployeeId { get; set; }
            public string FromDate { get; set; }
            public string ToDate { get; set; }
            public string LeaveType { get; set; }
            public string Description { get; set; }
            public int LeaveCount { get; set; }
            public string AppliedDate { get; set; }
            public string AdminDescription { get; set; }
            public string Status { get; set; }
            public int NoOfLeave { get; set; }
            public int Remain { get; set; }
            public int RejectLeaveCount { get; set; }
        }
        public class AssignProject
        {
            public int Id { get; set; }
            public string EmployeeId { get; set; }
            public string EmployeeName { get; set; }
            public string ProjectCode { get; set; }
            public string ProjectName { get; set; }
            public string Description { get; set; }
            public string AssignDate { get; set; }
        }
        public class TaskSheets
        {
            public int Id { get; set; }
            public string[] Date { get; set; }
            public string[] Project { get; set; }
            public string[] Task { get; set; }
            public string[] Hour { get; set; }
            public string[] Description { get; set; }
            public string[] Status { get; set; }
            public string[] Remark { get; set; }
            public string Date1 { get; set; }
            public string Project1 { get; set; }
            public string Task1 { get; set; }
            public string Hour1 { get; set; }
            public string Description1 { get; set; }
            public string Status1 { get; set; }
            public string Remark1 { get; set; }
            public string EmpName { get; set; }
            public string DateStatus { get; set; }
        }
        public class Quatation
        {
            public int Id { get; set; }
            public string CompanyId { get; set; }
        }
        public class services
        {
            public int Id { get; set; }
            public string Code { get; set; }
            public string Name { get; set; }
        }
        public class calculation
        {
            public decimal Total { get; set; }
            public decimal FinalTotal { get; set; }
        }
        public class quatation
        {
            public int Id { get; set; }
            public string QuatationNo { get; set; }
            public string CompanyId { get; set; }
            public string CompanyName { get; set; }
            public string ProjectId { get; set; }
            public string ProjectName { get; set; }
            public string[] Services { get; set; }
            public string[] Duration { get; set; }
            public string[] ServicesName { get; set; }
            public string[] DurationTime { get; set; }
            public string[] Date { get; set; }
            public decimal[] Amount { get; set; }
            public string[] Description { get; set; }
            public string WorkScope { get; set; }
            public decimal TotalAmount { get; set; }
            public decimal DiscountQty { get; set; }
            public decimal DiscountAmount { get; set; }
            public decimal AfterDiscountAmount { get; set; }
            public decimal ProjectAmount { get; set; }
            public int Status { get; set; }
            public string Services1 { get; set; }
            public string Duration1 { get; set; }
            public string Date1 { get; set; }
            public decimal Amount1 { get; set; }
            public string Description1 { get; set; }
            public string ClientName { get; set; }
            public string Address { get; set; }
            public string ClientEmail { get; set; }
            public string ClientPhone { get; set; }
            public string ClientGST { get; set; }
            public string ClientPAN { get; set; }
            public string ClientStatecode { get; set; }
            public string Amountword { get; set; }
            public string PINo { get; set; }
            public decimal Tax { get; set; }
            public decimal TaxAmount { get; set; }
            public decimal AfterTaxAmount { get; set; }
            public decimal IgstAmount { get; set; }
            public decimal CgstAmount { get; set; }
            public decimal SgstAmount { get; set; }
            public int IGST { get; set; }
            public int SGST { get; set; }
            public int CGST { get; set; }
            public string Taxtype { get; set; }
            public string[] SoftwareName { get; set; }
            public decimal[] SAmount { get; set; }
            public int[] SUnit { get; set; }
            public string[] SDescription { get; set; }
            public string[] STotal { get; set; }
            public string[] HardWareId { get; set; }
            public string[] HardWareName { get; set; }
            public string[] HDescription { get; set; }
            public string[] HAmount { get; set; }
            public string[] HUnit { get; set; }
            public string[] HTotal { get; set; }
            public string Type { get; set; }
            public string[] SSServices { get; set; }
            public string[] SSDuration { get; set; }
            public string[] SSServicesName { get; set; }
            public string[] SSDurationTime { get; set; }
            public decimal[] SSAmount { get; set; }
            public string[] SSDescription { get; set; }
            public string[] Naration { get; set; }
            public string ProductName { get; set; }
            public string HNaration { get; set; }
            public string HUnitAmount { get; set; }
            public string HwUnit { get; set; }
            public string HwTotal { get; set; }
            public string SoftName { get; set; }
            public string SoftAmount { get; set; }
            public string SoftUnit { get; set; }
            public string SoftDescription { get; set; }
            public string SoftTotal { get; set; }
            public string Servicesnm { get; set; }
            public string Durationtm { get; set; }
            public string ContactNo { get; set; }
        }
        public class Company
        {
            public int Id { get; set; }
            public string CompanyName { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }
            public string GSTNo { get; set; }
            public string TINNO { get; set; }
            public string PanNo { get; set; }
            public string AreaCode { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public string Address { get; set; }
            public string StateCode { get; set; }
            public string RegistrationNo { get; set; }
            public string Image { get; set; }
        }
        public class performa
        {
            public int Id { get; set; }
            public string QuatationNo { get; set; }
            public string CompanyId { get; set; }
            public string CompanyName { get; set; }
            public string ProjectId { get; set; }
            public string ProjectName { get; set; }
            public string[] Services { get; set; }
            public string[] Duration { get; set; }
            public string[] ServicesName { get; set; }
            public string[] DurationTime { get; set; }
            public string[] Date { get; set; }
            public decimal[] Amount { get; set; }
            public string[] Description { get; set; }
            public decimal TotalAmount { get; set; }
            public decimal ProjectAmount { get; set; }
            public int Status { get; set; }
            public string Services1 { get; set; }
            public string ServicesName1 { get; set; }
            public string Duration1 { get; set; }
            public string DurationTime1 { get; set; }
            public string Date1 { get; set; }
            public decimal Amount1 { get; set; }
            public string Description1 { get; set; }
            public string ClientName { get; set; }
            public string Address { get; set; }
            public string ClientEmail { get; set; }
            public string ClientPhone { get; set; }
            public string ClientGST { get; set; }
            public string ClientPAN { get; set; }
            public string ClientStatecode { get; set; }
            public string Amountword { get; set; }
            public string PINo { get; set; }
            public decimal Tax { get; set; }
            public decimal TaxAmount { get; set; }
            public decimal AfterTaxAmount { get; set; }
            public decimal IgstAmount { get; set; }
            public decimal CgstAmount { get; set; }
            public decimal SgstAmount { get; set; }
            public int IGST { get; set; }
            public int SGST { get; set; }
            public int CGST { get; set; }
            public string Taxtype { get; set; }
            public string TaxInvoiceNo { get; set; }

            public int TaxInvoiceStatus { get; set; }
            public string TaxInvoiceDate { get; set; }
            public string RenePINo { get; set; }
            public string Type { get; set; }
            public string InvoiceNo { get; set; }
            public string[] SoftwareName { get; set; }
            public decimal[] SAmount { get; set; }
            public int[] SUnit { get; set; }
            public string[] SDescription { get; set; }
            public string[] STotal { get; set; }
            public string[] HardWareId { get; set; }
            public string[] HardWareName { get; set; }
            public string[] HDescription { get; set; }
            public string[] HAmount { get; set; }
            public string[] HUnit { get; set; }
            public string[] HTotal { get; set; }
            public string[] SSServices { get; set; }
            public string[] SSDuration { get; set; }
            public string[] SSServicesName { get; set; }
            public string[] SSDurationTime { get; set; }
            public decimal[] SSAmount { get; set; }
            public string[] SSDescription { get; set; }
            public string[] Naration { get; set; }
            public string ProductName { get; set; }
            public string HNaration { get; set; }
            public string HUnitAmount { get; set; }
            public string HwUnit { get; set; }
            public string HwTotal { get; set; }
            public string SoftName { get; set; }
            public string SoftAmount { get; set; }
            public string SoftUnit { get; set; }
            public string SoftDescription { get; set; }
            public string SoftTotal { get; set; }
            public string Servicesnm { get; set; }
            public string Durationtm { get; set; }
            public int Ladger { get; set; }
        }
        public class Purchase
        {
            public int Id { get; set; }
            public string Date { get; set; }
            public string CompanyName { get; set; }
            public string InvoiceNo { get; set; }
            public string AmtBeforeTax { get; set; }
            public string Address { get; set; }
            public string PANNo { get; set; }
            public string TaxType { get; set; }
            public string Tax { get; set; }
            public string AmtAfterTax { get; set; }
            public string UploadInvoicePath { get; set; }
            public HttpPostedFileBase UploadInvoice { get; set; }
            public int IGST { get; set; }
            public int SGST { get; set; }
            public int CGST { get; set; }
            public string GSTNo { get; set; }
            public string TaxAmount { get; set; }
            public string IGSTAmount { get; set; }
            public string CGSTAmount { get; set; }
            public string SGSTAmount { get; set; }
        }
        public class Payments
        {
            public string CompanyId { get; set; }
            public string CompanyName { get; set; }
            public string ProjectId { get; set; }
            public string ProjectName { get; set; }
            public decimal TotalAmount { get; set; }
            public decimal Payment { get; set; }
            public decimal RemainingAmount { get; set; }
            public string PaymentMode { get; set; }
            public string BankName { get; set; }
            public string Branch { get; set; }
            public string ChequeNo { get; set; }
            public string chequeDate { get; set; }
            public string UTRNO { get; set; }
            public string Remark { get; set; }
            public string Date { get; set; }
            public string Status { get; set; }
            public string PINo { get; set; }
        }
        public class Lead
        {
            public int Id { get; set; }
            public string LeadType { get; set; }
            public string LeadDescription { get; set; }
            public string ClientName { get; set; }
            public string ClientEmail { get; set; }
            public string MobileNo { get; set; }
            public string Address { get; set; }
            public string EmployeeId { get; set; }
            public string EmployeeName { get; set; }
            public string LeadId { get; set; }
            public string[] Leads { get; set; }
            public int Status { get; set; }
            public string LeadStatus { get; set; }
            public string ContectPerson { get; set; }
            public string AssignDate { get; set; }
            public string AssignBy { get; set; }
            public string EmpDescription { get; set; }
            public string MeetingDate { get; set; }
        }
        public class TokenManagement
        {
            public int Id { get; set; }
            public string To { get; set; }
            public string Subject { get; set; }
            public string Message { get; set; }
            public string UserId { get; set; }
            public string TokenId { get; set; }
            public string Status { get; set; }
            public string FilePath { get; set; }
            public HttpPostedFileBase[] File { get; set; }
            public string From { get; set; }
            public string Date { get; set; }
            public string Department { get; set; }
            public string Periority { get; set; }
            public string CompanyName { get; set; }
            public string ContactNo { get; set; }
            public string Comment { get; set; }
            public string EmailId { get; set; }
            public string Comments { get; set; }
            public string email { get; set; }
        }
        public class state
        {
            public int Id { get; set; } 
            public string State_Name { get; set; } 

        }
        public class city
        {
            public int Id { get; set; }
            public string City_Name { get; set; }
            public int state_id { get; set; }

        }
    }
}