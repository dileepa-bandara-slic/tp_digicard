using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ThirdPartySMSDTO
/// </summary>
public class ThirdPartySMSDTO
{
    public string PolicyNo { get; set; }
    public int Bracode { get; set; }
    public string Proname1 { get; set; }
    //public string Proname2 { get; set; }
    public string vehNo { get; set; }
    public double netprm { get; set; }
    public string entdate { get; set; }
    public string MobiNo { get; set; }
    public string fromDate { get; set; }
    public string toDate { get; set; }
    public string Datcomm { get; set; }




    public DataTable dtSMS = new DataTable();
}