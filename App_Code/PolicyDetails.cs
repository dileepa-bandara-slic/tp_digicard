using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for PolicyDetails
/// </summary>
public class PolicyDetails
{
    OracleConnection conn = new OracleConnection(ConfigurationManager.AppSettings["DBConString"]);

    public async System.Threading.Tasks.Task<Dictionary<string, int>> sendSMSAsync(GridView gv, string userEPF, string ip, int branchNo)
    {
        
        Dictionary<string, int> results = new Dictionary<string, int>();
        

        int i = 0;
        try
        {

            string rowid = "";
            
            conn.Open();
            foreach (GridViewRow row in gv.Rows)
            {
                int resultCode = 0;
                CheckBox chck = ((CheckBox)row.FindControl("chkSMS"));
                if (chck.Checked)
                {
                    try
                    {
                        Label txtrow = ((Label)row.FindControl("lblRowNumber"));
                        rowid = txtrow.Text.Trim();
                        string pol = row.Cells[1].Text.Trim();
                        string branch = row.Cells[2].Text.Trim();
                        string mobileNo = "";
                        TextBox txtb = ((TextBox)row.FindControl("txtMobileE"));
                        string comDate = row.Cells[5].Text.Trim();

                        if (Regex.IsMatch(txtb.Text, @"^(?:7|0|(?:\+94))[0-9]{9,10}$"))
                        {
                            mobileNo = txtb.Text.Trim();
                            int j = 0;
                            j = this.EnterValues(pol, userEPF, branchNo, ip, mobileNo, conn);
                            if (j == 1)
                            {
                                APICaller api = new APICaller();
                                DocumentEntryDTO doc = new DocumentEntryDTO();
                                doc.DocumentType = "MTP";
                                doc.BusinessType = "G";
                                doc.BranchCode = branch; //Get from the users
                                doc.ReceiptNo = pol; // policyNo
                                doc.ReceiptDate = comDate; // Commencement Date in yyyy/mm/dd
                                doc.RefrenceType = "";
                                doc.EntryUser = userEPF; //signed in user
                                doc.AppType = "DOCSHARE";
                                doc.MobileNum = mobileNo; //Get from the etxt box
                                doc.SMSUrgent = "Y";
                                //populate other
                                resultCode = await api.PostDoc(doc);
                                //api.PostDoc();
                            }
                        }

                        results.Add(rowid, resultCode);
                    }
                    catch
                    { }
                }

                
            }
        }
        catch { }
        finally
        {
            conn.Close();
        }

        return results;
    }



    public int EnterValues(string policyNo, string userEPF, int branchCode, string ip, string mobileNo, OracleConnection oconn)
    {
        int ref_num = 0;
        string sql = "slic_net.THIRD_PARTY_PROC";

        try
        {
            //conn.Open();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = oconn;
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("p_policy_no", OracleType.VarChar).Value = policyNo;
            cmd.Parameters.Add("p_epf", OracleType.VarChar).Value = userEPF;
            cmd.Parameters.Add("p_branch", OracleType.Int32).Value = branchCode;
            cmd.Parameters.Add("p_ip", OracleType.VarChar).Value = ip;
            cmd.Parameters.Add("mobileNo", OracleType.VarChar).Value = mobileNo;
            
            cmd.Parameters.Add("ret", OracleType.Int32, 2000).Direction = ParameterDirection.Output;

            int count = cmd.ExecuteNonQuery();
            ref_num = Convert.ToInt32( cmd.Parameters["ret"].Value.ToString());

        }
        catch (Exception e)
        {
            //result = ref_num;
        }
        finally
        {
            //conn.Close();
        }
        //0 fail
        //1 in hand amount less than refund amount
        //4 not found in the table
        //100 successfull
        return ref_num;
    }
}