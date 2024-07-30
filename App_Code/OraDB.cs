using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for OraDB
/// </summary>
public class OraDB
{
    OracleConnection conn = new OracleConnection(ConfigurationManager.AppSettings["DBConString"]);
    public OracleCommand odcom = new OracleCommand();

    public OraDB()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public void ConClose()
    {
        if (conn.State != ConnectionState.Closed)
        {
            conn.Close();
            conn.Dispose();
        }
    }

    public void ConOpen()
    {
        if (conn.State != ConnectionState.Open)
        {
            conn.Open();
            //conn.Dispose();
        }
    }

    public DataSet getrows(string sql, DataSet ds)
    {
        ds.Clear();
        try
        {
            conn.Open();
            odcom.Connection = conn;
            OracleDataAdapter data = new OracleDataAdapter(sql, conn);
            ds.Clear();
            data.Fill(ds);
        }
        catch
        {
        }
        finally
        {
            conn.Close();
        }
        return ds;
    }

    public bool isExists(string sql)

    {
        bool result = false;
        conn.Open();
        odcom.Connection = conn;
        odcom.CommandText = sql;
        OracleDataReader reader = odcom.ExecuteReader();
        while (reader.Read())
        {
            result = true;
            break;
        }
        conn.Close();
        return result;
    }
}