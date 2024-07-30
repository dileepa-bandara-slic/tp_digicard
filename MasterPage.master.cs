using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage : System.Web.UI.MasterPage
{
    public string LogEPF = "";
    public string UserId = "";
    Authentication auth = new Authentication();

    protected override void OnInit(EventArgs e)
    {
        LogEPF = (string)Session["EPFNum"];
        UserId = (string)Session["UserId"];
        if (String.IsNullOrEmpty(LogEPF) && String.IsNullOrEmpty(UserId))
        {
            Response.Redirect("nosession.html");
        }
        else
        {
            //if (auth.as400_authorise(UserId, "TPSMS", "TPCSHR"))
            //{

            //}
            //else
            //{
            //    Response.Redirect("noauth.html");
            //}
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }
}
