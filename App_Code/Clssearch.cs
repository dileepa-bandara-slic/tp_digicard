using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Clssearch
/// </summary>
public class Clssearch
{
    OracleConnection conn = new OracleConnection(ConfigurationManager.AppSettings["DBConString"]);
    public DataTable dtpol = new DataTable();
    
    public ThirdPartySMSDTO getPolicyDetails(string PolicyNo)
    {
        ThirdPartySMSDTO third = new ThirdPartySMSDTO();
        third.PolicyNo = PolicyNo;




        DataTable dt = new DataTable();
        dt.Columns.Add("PolicyNo", typeof(string));
        dt.Columns.Add("BrNo", typeof(string));
        dt.Columns.Add("VeicleNo", typeof(string));
        dt.Columns.Add("Premium", typeof(string));
        dt.Columns.Add("Entry_Date", typeof(string));
        dt.Columns.Add("MobileNo", typeof(string));
        dt.Columns.Add("Name", typeof(string));

        try
        {

            string sql = "select  pi.pi_policyno, pi_bracode  ,trim(PI.pi_prostatus) || trim(pi.pi_proname1) || ' ' || trim(pi.pi_proname2),trim(p.vehprov||' '|| p.vehno) as vehno," +
                         " p.netprm, to_char(p.datcomm, 'yyyy/mm/dd')  as datcomm,pi.pi_teleno " +
                         " from thirdparty.personal_information  PI inner join  thirdparty.policy_information   P" +
                         " ON PI.pi_policyno = P.policyno" +
                         " WHERE PI.pi_policyno= :polno";


            conn.Open();

            OracleParameter PolNo = new OracleParameter();
            PolNo.Value = third.PolicyNo;
            PolNo.ParameterName = "polno";

            OracleCommand com = new OracleCommand(sql, conn);

            com.Parameters.Add(PolNo);

            OracleDataReader reader_1 = com.ExecuteReader();
            while (reader_1.Read())
            {
                string POL = "";
                string VEH_NO = "";
                var PREMIUM = 0.00;
                string ENTRYDATE = "";
                string MOBILENO = "";
                string NAME = "";
                var BRCODE = 0;

                if (reader_1 != null)
                {
                    POL = reader_1[0].ToString().Trim();
                    third.PolicyNo = POL;
                }
                if (reader_1 != null)
                {
                    BRCODE = int.Parse(reader_1[1].ToString().Trim());
                    third.Bracode = BRCODE;
                }
                if (reader_1 != null)
                {
                    NAME = reader_1[2].ToString().Trim();
                    third.Proname1 = NAME;
                }
                if (reader_1 != null)
                {
                    VEH_NO = reader_1[3].ToString().Trim();
                    third.vehNo = VEH_NO;
                }
                if (reader_1 != null)
                {
                    PREMIUM = double.Parse(reader_1[4].ToString().Trim());
                    third.netprm = PREMIUM;
                }
                if (reader_1 != null)
                {
                    ENTRYDATE = reader_1[5].ToString().Trim();
                    third.Datcomm = ENTRYDATE;
                }
                if (reader_1 != null)
                {
                    MOBILENO = reader_1[6].ToString().Trim();
                    third.MobiNo = MOBILENO;
                }


                //OracleCommand com = new OracleCommand(sql, conn);

                //odcom.Connection = conn;
                //OracleDataAdapter data = new OracleDataAdapter(sql, conn);
                //data.Fill(dtSMS);
                //GridView1.DataSource = reader_1;
                //GridView1.DataBind();

                dt.Rows.Add(third.PolicyNo,
                    third.Bracode,
                third.vehNo,
                third.netprm,
                third.Datcomm,
                third.MobiNo,
                third.Proname1);


                //third.MobiNo[0].Rows[i].ItemArray[6].ToString());

            }
            reader_1.Close();
            conn.Close();
            dtpol = dt;
        }



        catch (Exception ex)
        {

        }
        finally
        {
            conn.Close();
        }

        return third;


    }

    public DataTable datfrom = new DataTable();
    public DataTable todat = new DataTable();
    public ThirdPartySMSDTO getPolicyDetails(string fromDt, string toDt)
    {
        ThirdPartySMSDTO third = new ThirdPartySMSDTO();
        third.fromDate = fromDt;
        third.toDate = toDt;




        DataTable dt = new DataTable();
        dt.Columns.Add("PolicyNo", typeof(string));
        dt.Columns.Add("BrNo", typeof(string));
        dt.Columns.Add("VeicleNo", typeof(string));
        dt.Columns.Add("Premium", typeof(string));
        dt.Columns.Add("Entry_Date", typeof(string));
        dt.Columns.Add("MobileNo", typeof(string));
        dt.Columns.Add("Name", typeof(string));

        try
        {

            string sql = "select  pi.pi_policyno, pi_bracode  ,trim(PI.pi_prostatus) || trim(pi.pi_proname1) || ' ' || trim(pi.pi_proname2)," +
                         " trim(p.vehprov||' '|| p.vehno) as vehno ,p.netprm,to_char(p.datcomm, 'yyyy/mm/dd') as datcomm, pi.pi_teleno" +
                         " from thirdparty.personal_information   PI inner join  thirdparty.policy_information   P" +
                         " ON PI.pi_policyno = P.policyno" +
                         " WHERE to_char(p.datcomm,'yyyy/mm/dd')>= :fromDt" +
                         " AND to_char(p.datcomm,'yyyy/mm/dd')<= :todat";

            conn.Open();

            OracleParameter DatComm = new OracleParameter();
            DatComm.Value = third.fromDate;
            DatComm.ParameterName = "fromDt";

            OracleParameter ToDate = new OracleParameter();
            ToDate.Value = third.toDate;
            ToDate.ParameterName = "todat";

            OracleCommand com = new OracleCommand(sql, conn);

            com.Parameters.Add(DatComm);
            com.Parameters.Add(ToDate);

            OracleDataReader reader_1 = com.ExecuteReader();
            while (reader_1.Read())
            {

                string POL = "";
                string VEH_NO = "";
                var PREMIUM = 0.00;
                string CommenceDT = "";
                string MOBILENO = "";
                string NAME = "";
                var BRCODE = 0;

                if (reader_1[0] != null)
                {
                    POL = reader_1[0].ToString().Trim();
                    third.PolicyNo = POL;
                }
                if (reader_1[1] != null)
                {
                    BRCODE = int.Parse(reader_1[1].ToString().Trim());
                    third.Bracode = BRCODE;
                }
                if (reader_1[2] != null)
                {
                    NAME = reader_1[2].ToString().Trim();
                    third.Proname1 = NAME;
                }
                if (reader_1[3] != null)
                {
                    VEH_NO = reader_1[3].ToString().Trim();
                    third.vehNo = VEH_NO;
                }
                if (reader_1[4] != null)
                {
                    PREMIUM = double.Parse(reader_1[4].ToString().Trim());
                    third.netprm = PREMIUM;
                }
                if (reader_1[5] != null)
                {
                    CommenceDT = reader_1[5].ToString().Trim();
                    third.Datcomm = CommenceDT;
                }

                dt.Rows.Add(third.PolicyNo,
                    third.Bracode,
                                third.vehNo,
                                third.netprm,
                                third.Datcomm,
                                third.MobiNo,
                                third.Proname1);


                // todat = dt;

                //OracleCommand com = new OracleCommand(sql, conn);

                //odcom.Connection = conn;
                //OracleDataAdapter data = new OracleDataAdapter(sql, conn);
                //data.Fill(dtSMS);
                //GridView1.DataSource = reader_1;
                //GridView1.DataBind();


            
            }
            reader_1.Close();
            conn.Close();
            dtpol = dt;

        }



        catch (Exception ex)
        {

        }
        finally
        {
            conn.Close();
        }

        return third;


    }
}