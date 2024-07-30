using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SessionTrans : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        //Session["EPFNum"] = "7886";
        //Session["brcode"] = "10";
        //Session["UserId"] = "SEC7886";

        string EpfReqEncodedReq = Request.QueryString["Epf"];
        string polNoEncodedReq = Request.QueryString["PolNo"];


        if (String.IsNullOrEmpty(EpfReqEncodedReq) && String.IsNullOrEmpty(polNoEncodedReq))
        {
            Response.Redirect("nosession.html");
        }
        else
        {

            byte[] bytesToDecodePol = Convert.FromBase64String(polNoEncodedReq);
            string decodedPolNo = Encoding.UTF8.GetString(bytesToDecodePol);

            byte[] bytesToDecodeEpf = Convert.FromBase64String(EpfReqEncodedReq);
            string decodedEpf = Encoding.UTF8.GetString(bytesToDecodeEpf);

            Session["EPFNum"] = decodedEpf;
            Session["brcode"] = "999";
            Session["UserId"] = "SEC" + decodedEpf;
            Session["PolNo"] = decodedPolNo;
        }

        //for (int i = 0; i < Request.Form.Count; i++)
        //{
        //    Session[Request.Form.GetKey(i)] = Request.Form[i].ToString();
        //}

        Server.Transfer("Default.aspx");
    }
}