using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Macreel_Project.Models.Admin
{

    public class Manage_Group
    {
        public string Id { get; set; }
        public string grp_name { get; set; }

        // Navigation property for group members
        public string grp_members { get; set; }
        public List<emp_list> emp_list { get; set; }
    }

    public class Employee
    {
        public int Id { get; set; }
        public string EmployeeName { get; set; }
    }

    public class GroupViewModel
    {
        public string GroupName { get; set; }
        public List<int> SelectedEmployeeIds { get; set; }
        public List<Employee> Employees { get; set; }
    }
}