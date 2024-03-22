//using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Macreel_Project.Models.Admin
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

    //public class NotificationHub : Hub
    //{
    //    // Hub methods
    //    //public void BroadcastMessage(string message)
    //    //{
    //    //    Clients.All.notify(message);
    //    //}

    //    //// Additional hub methods can be added for specific functionalities
    //    //// For example, a method to send a message to a specific user
    //    //public void SendMessageToUser(string userId, string message)
    //    //{
    //    //    Clients.User(userId).notify(message);
    //    //}
    //}
}