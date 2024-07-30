using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.getbranchList();

            try
            {
                DropDownBranch.Items.FindByValue("999").Selected = true;
                txtpolno.Text = (string)Session["PolNo"];
            }
            catch { }
        }
        else
        {

        }
    }

    protected void check_eithr(object source, ServerValidateEventArgs args)
    {
        
        if (String.IsNullOrEmpty(txtpolno.Text))
        {
            #region
            if (String.IsNullOrEmpty(Txtboxfrom.Text.Trim()) && String.IsNullOrEmpty(Txtboxfrom.Text.Trim()))
            {
                args.IsValid = false;
                CustomValidator1.ErrorMessage = "Please Enter a Date Range.";
            }
            else
            {
                args.IsValid = true;
            }
            #endregion
        }
        else
        {
            args.IsValid = true;
        }
    }

    public void getbranchList()
    {
        DataSet ds = new DataSet();

        OraDB db = new OraDB();

        string sql = "select brcod, brnam from genpay.branch order by brnam ";
        ds = db.getrows(sql, ds);
        DropDownBranch.DataSource = ds.Tables[0];
        DropDownBranch.DataTextField = "brnam";
        DropDownBranch.DataValueField = "brcod";
        DropDownBranch.DataBind();

        ds.Clear();

        sql = "select province_name from MCOMP.vehicle_province order by province_name desc";
        ds = db.getrows(sql, ds);
        DropDownProvince.DataSource = ds.Tables[0];
        DropDownProvince.DataTextField = "province_name";
        DropDownProvince.DataValueField = "province_name";
        DropDownProvince.DataBind();

        //DropDownBranch.SelectedValue = int.Parse(hdfbr.Value).ToString();
        //DropDownBranch.DataBind();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            Clssearch objSer = new Clssearch();
            string sql1 = "";
            txterror.Text = "";
            var polno = "";
            var fromdate = "";
            var todate = "";
            

            if (!string.IsNullOrEmpty(txtpolno.Text))
            {

            }
            else if ((!string.IsNullOrEmpty(Txtboxfrom.Text)) && (!string.IsNullOrEmpty(TxtboxTo.Text)))
            {
                fromdate = Txtboxfrom.Text;
                todate = TxtboxTo.Text;

            }
            else
            {
                txterror.Text = "Please enter Either Policy No or Date Range";
            }

            LoadData_ToGridVw();

        }

    }

    protected void LoadData_ToGridVw()
    {
        try
        {
            OraDB db = new OraDB();
            DataSet ds = new DataSet();

            /*
            string sql = "select  pi.pi_policyno, pi_bracode  ,trim(PI.pi_prostatus) || trim(pi.pi_proname1) || ' ' || trim(pi.pi_proname2) as Name,trim(p.vehprov||' '|| p.vehno) as vehno,  " +
            " p.netprm, to_char(p.datfrom, 'yyyy/mm/dd') as datcomm,(case when  PT.conno_mo is not null then PT.conno_mo else pi.pi_teleno   end) as pi_teleno,   " +
            " case when exists(select * " +
            " from slic_net.thirdpartyinfo " +
            " where policyno = pi.pi_policyno and commencementdate = p.datfrom) " +
            "   then 'Already Sent' " +
            "   end as rec_exists ,  nvl(p.entdate, p.upddate) as ENTDATE, (case when P.covernote = '1' then 'Yes' else 'No' end) as covernote, cnreason " +
            " from thirdparty.personal_information  PI " +
            " inner join thirdparty.policy_information P ON PI.pi_policyno = P.policyno " +
            " LEFT OUTER join  POSTOFFICE.POLICY_TRANSACTIONS  PT  on PI.pi_policyno = PT.policy_no and PT.period_insu1 = p.datcomm " +
            " WHERE deletetag = 0 and (receiptno is  not null and receiptno > 0) ";
            */

            string sql = "select  pi.pi_policyno, pi_bracode  ,trim(PI.pi_prostatus) || trim(pi.pi_proname1) || ' ' || trim(pi.pi_proname2) as Name, pc.nic_no, trim(p.vehprov||' '|| p.vehno) as vehno,  " +
            " p.netprm, to_char(p.datfrom, 'yyyy/mm/dd') as datcomm,(case when  PT.conno_mo is not null then PT.conno_mo else pi.pi_teleno end) as pi_teleno, " +
            " case when exists(select * from slic_net.thirdpartyinfo " +
            " where policyno = pi.pi_policyno and commencementdate = p.datfrom) then 'Already Sent' " +
            " end as rec_exists, nvl(p.entdate, p.upddate) as ENTDATE, (case when P.covernote = '1' then 'Yes' else 'No' end) as covernote, cnreason " +
            " from thirdparty.personal_information PI " +
            " inner join thirdparty.policy_information P " +
            " ON PI.pi_policyno = P.policyno " +
            " LEFT OUTER join  POSTOFFICE.POLICY_TRANSACTIONS PT " +
            " on PI.pi_policyno = PT.policy_no and PT.period_insu1 = p.datcomm " +
            " LEFT OUTER JOIN CLIENTDB.PERSONAL_CUSTOMER PC " +
            " ON PC.CUSTOMER_ID = PI.CUSTOMER_ID " +
            " WHERE deletetag = 0 and (receiptno is  not null and receiptno > 0) ";


            if (DDCoverNote.SelectedValue.Trim() == "0")
            {

            }
            else
            {
                if (DDCoverNote.SelectedValue.Trim() == "NoCN")
                {
                    sql = sql + " and P.covernote != '1' ";
                }
                else if (DDCoverNote.SelectedValue.Trim() == "YesCN")
                {
                    sql = sql + " and P.covernote = '1' ";
                }
            }

            if (DDNIC.SelectedValue.Trim() == "0")
            {

            }
            else
            {
                if (DDNIC.SelectedValue.Trim() == "1")
                {
                    sql = sql + " and nic_no is not null ";
                }
                else
                {
                    sql = sql + " and nic_no is null ";
                }
            }

            if ((!string.IsNullOrEmpty(Txtboxfrom.Text)) && (!string.IsNullOrEmpty(TxtboxTo.Text)))
            {
                sql = sql + " and ((((p.ENTDATE BETWEEN to_date('" + Txtboxfrom.Text.Trim() + "', 'yyyy/mm/dd') and to_date('" + TxtboxTo.Text.Trim() + "', 'yyyy/mm/dd')) and updrwal is null " + ((DropDownBranch.SelectedValue.Trim() != "0") ? " and PI_bracode = " + DropDownBranch.SelectedValue : "") +
                    "    )  or" +
                    " ((p.UPDDATE BETWEEN to_date('" + Txtboxfrom.Text.Trim() + "', 'yyyy/mm/dd') and to_date('" + TxtboxTo.Text.Trim() + "', 'yyyy/mm/dd')) and updrwal = 1 " + ((DropDownBranch.SelectedValue.Trim() != "0") ? " and PI_edtbracode = " + DropDownBranch.SelectedValue : "") + ")))";
            }
            else
            {

                if (DropDownBranch.SelectedValue.Trim() != "0")
                {
                    sql = sql + " and ( (updrwal is null " + ((DropDownBranch.SelectedValue.Trim() != "0") ? " and PI_bracode = " + DropDownBranch.SelectedValue : "") +
                    ")      or" +
                    " ( updrwal = 1 " + ((DropDownBranch.SelectedValue.Trim() != "0") ? " and PI_edtbracode = " + DropDownBranch.SelectedValue : "") + "))";
                }
            }

            if ((!string.IsNullOrEmpty(txtpolno.Text)))
            {
                sql = sql + " and PI.pi_policyno = '" + txtpolno.Text.Trim() + "'";
            }
            //if ((!string.IsNullOrEmpty(Txtboxfrom.Text)) && (!string.IsNullOrEmpty(TxtboxTo.Text)))
            //{
            //    sql = sql + " and (p.ENTDATE BETWEEN to_date('" + Txtboxfrom.Text.Trim() + "', 'yyyy/mm/dd') and to_date('" + TxtboxTo.Text.Trim() + "', 'yyyy/mm/dd') + 1)";
            //}

            if (DropDownProvince.SelectedValue.Trim() != "0")
            {
                sql = sql + " and p.vehprov = '" + DropDownProvince.SelectedValue + "'";
            }

            if ((!string.IsNullOrEmpty(TxtVehicleNo_1.Text)) && (!string.IsNullOrEmpty(TxtVehicleNo_2.Text)))
            {
                sql = sql + " and p.vehno like '%" + TxtVehicleNo_1.Text.Trim().ToUpper() + "%" + TxtVehicleNo_2.Text.Trim().ToUpper() + "%' ";
            }

            if (DDMobile.SelectedValue.Trim() != "0")
            {
                if (DDMobile.SelectedValue.Trim() == "Invalid")
                {
                    sql = "select * from ( " + sql + ")";
                    sql = sql + " where  (pi_teleno is null or pi_teleno = '0' or (pi_teleno not like '07%' and pi_teleno not like '7%') or ((pi_teleno like '07%' and length(pi_teleno) !=10) or  (pi_teleno like '7%' and length(pi_teleno) !=9) ) ) order by pi_teleno";
                }
                else if (DDMobile.SelectedValue.Trim() == "Valid")
                {
                    sql = "select pi_policyno,  pi_bracode, Name, nic_no, vehno, netprm, datcomm, (case when length(pi_teleno) = 9 then '0'||pi_teleno else pi_teleno end) as  pi_teleno, rec_exists, ENTDATE , covernote, cnreason " +
                        "from ( " + sql + ")";
                    sql = sql + "  where  ((length (pi_teleno) = 10 and pi_teleno like '07%' ) or (length (pi_teleno) = 9 and pi_teleno like '7%' ) ) order by pi_teleno";
                }
            }
            else
            {
                sql = "select pi_policyno,  pi_bracode, Name, nic_no, vehno, netprm, datcomm, (case when length(pi_teleno) = 9 and substr(pi_teleno,0,1) = '7' then '0'||pi_teleno else pi_teleno end) as  pi_teleno, rec_exists, ENTDATE, covernote, cnreason " +
                        "from ( " + sql + ")";
                sql = sql + "   order by pi_teleno";

            }

            //lbl.Text = sql;
            //Response.Write(sql);

            if (db.isExists(sql))
            {
                ph_grid.Visible = true;
                ds = db.getrows(sql, ds);
                GridView1.DataSource = ds.Tables[0];
                GridView1.DataBind();
                txterror.Text = ds.Tables[0].Rows.Count.ToString() + " Records Found.";
            }
            else
            {
                ph_grid.Visible = false;
                txterror.Text = "No records found";
            }

        }
        catch (Exception ex)
        {
            txterror.Text = ex.Message;
        }

    }

    protected async void Button2_Click(object sender, EventArgs e)
    {
        string ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        PolicyDetails pol = new PolicyDetails();
        var results = await pol.sendSMSAsync(GridView1, (string)Session["EPFNum"].ToString(), ip, Convert.ToInt32((string)Session["brcode"]));

        //foreach (KeyValuePair<string, int> entry in results)
        //{
            foreach (GridViewRow row in GridView1.Rows)
            {
                Label lblRowNumber = ((Label)row.FindControl("lblRowNumber"));
                string rowid = lblRowNumber.Text.Trim();
                Label txtb = ((Label)row.FindControl("lblMsgE"));

                if (results.ContainsKey(rowid))
                {
                    int i = results[rowid];
                    if (i==1)
                        txtb.Text = "SMS Sent";
                    else
                        txtb.Text = "Not Sent";
                }
                else
                {
                    txtb.Text = "Not Sent";
                }
                
            }
        //}
    }

    protected void btn_nxt_Click(object sender, EventArgs e)
    {
        int i = 0;
        foreach (GridViewRow gr in GridView1.Rows)
        {
            Label lbl = (Label)gr.FindControl("lblMsgE");
            if (String.IsNullOrEmpty(lbl.Text))
            {
                i++;
                if (i > 100)
                    break;
                else
                { 
                    CheckBox chk = (CheckBox)gr.FindControl("chkSMS");
                    chk.Checked = true;
                }
            }

        }
    }

    protected void btn_nxt0_Click(object sender, EventArgs e)
    {

    }

    protected void btn_non_Click(object sender, EventArgs e)
    {
        int i = 0;
        foreach (GridViewRow gr in GridView1.Rows)
        {
            CheckBox chk = (CheckBox)gr.FindControl("chkSMS");
            chk.Checked = false;
        }
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GridView1.PageIndex = e.NewPageIndex;
            LoadData_ToGridVw();
        }
        catch(Exception ex)
        {
            txterror.Text = ex.Message;
        }
    }
}