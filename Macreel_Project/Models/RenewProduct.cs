using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using static Macreel_Project.Models.Bussiness;

namespace Macreel_Project.Models
{
    public class RenewProduct
    {
        static RenewProduct()
        {
            Thread renew = new Thread(new ThreadStart(AllRenewProduct));
            renew.Start();
        }
        static void AllRenewProduct()
        {
            DataAccess db1 = new DataAccess();
            while (true)
            {
                string time = System.DateTime.Now.ToString("hh:mm:ss tt");
                if (time == "08:04:60 PM")
                {
                    List<performa> LIST = new List<performa>();
                    LIST = db1.GetProductForRenew();
                    foreach (var item in LIST)
                    {
                        int count = 0;
                        string No = "";
                        string Months = "";
                        No = db1.GetId();
                        Months = db1.GetMonths();
                        item.RenePINo = item.InvoiceNo + No + Months;
                        count = db1.PIRenewProduct(item.RenePINo, item.Services1, item.ServicesName1, item.Duration1, item.DurationTime1, System.DateTime.Now.ToString("dd-MM-yyy"), item.Amount1, item.Description1, item.CompanyId, item.ProjectId, item.PINo);
                    }
                    System.Threading.Tasks.Task.Delay(24 * 60 * 60 * 1000);
                }
            }
        }
    }
}