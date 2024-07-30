using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Odbc;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Authentication
/// </summary>
public class Authentication
{
    OdbcConnection oconn = new OdbcConnection(ConfigurationManager.AppSettings["DB2"]);

    public Authentication()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public bool as400_login(string user_id, string passwrd)
    {
        bool result = false;
        string passwd = fix_f_password(passwrd);
        try
        {
            oconn.Open();

            string sql = "Select COUNT(*) from INTRANET.INTUSR where Userid = ? AND MAINPASS = ? AND  USRTYP  NOT IN ('A','O')";
            using (OdbcCommand com = new OdbcCommand(sql, oconn))
            {
                com.Parameters.Add("@uid", OdbcType.Char);
                com.Parameters.Add("@ps", OdbcType.Char);

                com.Parameters["@uid"].Value = user_id;
                com.Parameters["@ps"].Value = passwd;

                //com.Parameters.AddWithValue("", user_id); 
                //com.Parameters.AddWithValue("", passwrd); 

                int kk = Convert.ToInt32(com.ExecuteScalar());

                if (kk > 0)
                {
                    result = true;
                }
            }

        }
        catch (OdbcException ee)
        {
            string errorMsg = ee.ToString();
        }
        finally
        {
            oconn.Close();
        }
        return result;
    }

    public bool as400_user_exist(string user_id)
    {
        bool result = false;
        try
        {
            oconn.Open();

            string sql = "Select COUNT(*) from INTRANET.INTUSR where Userid = ?  AND  USRTYP  NOT IN ('A','O')";
            using (OdbcCommand com = new OdbcCommand(sql, oconn))
            {
                com.Parameters.AddWithValue("", user_id);
                int kk = Convert.ToInt32(com.ExecuteScalar());

                if (kk > 0)
                {
                    result = true;
                }
            }

        }
        catch (OdbcException ee)
        {
            string errorMsg = ee.ToString();
        }
        finally
        {
            oconn.Close();
        }
        return result;
    }

    public bool as400_authorise(string user_id, string sys_code, string fun_code)
    {

        #region AS400 connection
        bool ok = false;
        // string user_id = this.create_user_id(epf);



        try
        {
            oconn.Open();

            string sql = "Select COUNT(*) from INTRANET.USRAUTFUN where  Userid = ? AND Syscod = ? AND funcod = ?";
            //string sql = "Select COUNT(*) from INTRANET.USRAUTFUN where  Userid = ?";
            using (OdbcCommand com = new OdbcCommand(sql, oconn))
            {
                com.Parameters.AddWithValue("", user_id); // ("Uid", OdbcType.VarChar).Value = user_id;
                com.Parameters.AddWithValue("", sys_code); // ("scod", OdbcType.VarChar).Value = sys_code;
                com.Parameters.AddWithValue("", fun_code); // ("fcod", OdbcType.VarChar).Value = fun_code; 
                int kk = Convert.ToInt32(com.ExecuteScalar());

                if (kk > 0)
                {
                    ok = true;
                }
            }

        }
        catch (OdbcException ee)
        {
            string errorMsg = ee.ToString();
        }
        finally
        {
            oconn.Close();
        }
        return ok;
        #endregion
    }

    public bool as400_authorise(string user_id, string sys_code)
    {

        #region AS400 connection
        bool ok = false;
        // string user_id = this.create_user_id(epf);



        try
        {
            oconn.Open();

            string sql = "Select COUNT(*) from INTRANET.USRAUTFUN where  Userid = ? AND Syscod = ?";
            //string sql = "Select COUNT(*) from INTRANET.USRAUTFUN where  Userid = ?";
            using (OdbcCommand com = new OdbcCommand(sql, oconn))
            {
                com.Parameters.AddWithValue("", user_id); // ("Uid", OdbcType.VarChar).Value = user_id;
                com.Parameters.AddWithValue("", sys_code); // ("scod", OdbcType.VarChar).Value = sys_code;
                // com.Parameters.AddWithValue("", fun_code); // ("fcod", OdbcType.VarChar).Value = fun_code; 
                int kk = Convert.ToInt32(com.ExecuteScalar());

                if (kk > 0)
                {
                    ok = true;
                }
            }

        }
        catch (OdbcException ee)
        {
            string errorMsg = ee.ToString();
        }
        finally
        {
            oconn.Close();
        }
        return ok;
        #endregion
    }

    public bool as400_authorise_lower(string user_id)
    {

        #region AS400 connection
        bool ok = false;




        try
        {
            oconn.Open();

            //string sql = "Select COUNT(*) from INTRANET.USRAUTFUN where  Userid = ? AND Syscod = ? AND funcod = ?";
            string sql = "Select COUNT(*) from INTRANET.USRAUTFUN where  Userid = ?";
            using (OdbcCommand com = new OdbcCommand(sql, oconn))
            {

                com.Parameters.Add("Uid", OdbcType.VarChar).Value = user_id;
                //com.Parameters.Add("scod", OdbcType.VarChar).Value = "SLINO";
                //com.Parameters.Add("fcod", OdbcType.VarChar).Value = "FUNC01"; 
                int kk = Convert.ToInt32(com.ExecuteScalar());

                if (kk > 0)
                {
                    ok = true;
                }
            }

        }
        catch (OdbcException ee)
        {
            string error = ee.ToString();
        }
        finally
        {
            oconn.Close();
        }
        return ok;
        #endregion
    }

    private string fix_f_password(string passwd)
    {
        string result = passwd;

        char[] arr = new char[12];
        int len = passwd.Length;

        for (int i = 0; i < arr.Length; i++)
        {
            if (i < passwd.Length)
            {
                arr[i] = passwd[i];

            }
            else
            {
                arr[i] = ' ';
            }
        }

        result = arr[10].ToString().Trim() + arr[11].ToString().Trim() + arr[6].ToString().Trim() + arr[7].ToString().Trim() + arr[2].ToString().Trim() + arr[3].ToString().Trim() + arr[8].ToString().Trim() + arr[9].ToString().Trim() + arr[4].ToString().Trim() + arr[5].ToString().Trim() + arr[0].ToString().Trim() + arr[1].ToString().Trim();

        return result;
    }

    public void as400_get_usrInfo(string user_id, out string epf_numbr, out string user_brCode, out string user_name)
    {
        epf_numbr = "";
        user_brCode = "";
        user_name = "";

        string result = "";

        try
        {
            oconn.Open();
            string sql = "Select Epfnum, brnach, usrname from INTRANET.INTUSR where Userid = ?";
            using (OdbcCommand com = new OdbcCommand(sql, oconn))
            {
                com.Parameters.Add("@uid", OdbcType.Char);
                com.Parameters["@uid"].Value = user_id;

                OdbcDataReader reader = com.ExecuteReader();

                while (reader.Read())
                {
                    epf_numbr = reader[0].ToString();
                    user_brCode = reader[1].ToString();
                    user_name = reader[2].ToString();
                }
            }
        }
        catch
        {

        }
        finally
        {
            oconn.Close();
        }

    }


    #region Get Emp Name

    public void as400_get_EmpName(string user_id, out string user_name)
    {

        user_name = "";
        user_id = user_id.Trim();


        while (user_id.Length < 5)
        {
            user_id = "0" + user_id;
        }

        string result = "";

        try
        {
            oconn.Open();
            string sql = "Select usrname from INTRANET.INTUSR where EPFNum = ?";
            using (OdbcCommand com = new OdbcCommand(sql, oconn))
            {
                com.Parameters.Add("@uid", OdbcType.Char);
                com.Parameters["@uid"].Value = user_id;

                OdbcDataReader reader = com.ExecuteReader();

                while (reader.Read())
                {
                    user_name = reader[0].ToString();
                }
            }
        }
        catch
        {

        }
        finally
        {
            oconn.Close();
        }

    }

    #endregion


    #region Get Emp func code

    public void as400_get_FuncCode(string user_id, string syscode, out string func_code)
    {

        func_code = "";
        user_id = user_id.Trim();


        while (user_id.Length < 5)
        {
            user_id = "0" + user_id;
        }

        string result = "";

        try
        {
            oconn.Open();
            string sql = "Select funcod from INTRANET.USRAUTFUN where Userid = ? AND Syscod = ?";
            using (OdbcCommand com = new OdbcCommand(sql, oconn))
            {
                com.Parameters.Add("@uid", OdbcType.Char);
                com.Parameters.Add("@uid2", OdbcType.Char);
                com.Parameters["@uid"].Value = user_id;
                com.Parameters["@uid2"].Value = syscode;

                OdbcDataReader reader = com.ExecuteReader();

                while (reader.Read())
                {
                    func_code = reader[0].ToString();
                }
            }
        }
        catch
        {

        }
        finally
        {
            oconn.Close();
        }

    }

    #endregion

    #region Get Emp Mobile no

    public string as400_get_MobNo(string EPF)
    {
        string mobNo = "";

        EPF = EPF.Trim();


        while (EPF.Length < 5)
        {
            EPF = "0" + EPF;
        }


        try
        {
            oconn.Open();
            string sql = "Select mobileno from INVLIB.ISSUEDINFO where USEREPF = ?";
            using (OdbcCommand com = new OdbcCommand(sql, oconn))
            {
                com.Parameters.Add("@uid", OdbcType.Char);
                com.Parameters["@uid"].Value = EPF;

                OdbcDataReader reader = com.ExecuteReader();

                while (reader.Read())
                {
                    mobNo = reader[0].ToString();
                }
            }
        }
        catch
        {

        }
        finally
        {
            oconn.Close();


        }
        return mobNo;

    }

    #endregion
}