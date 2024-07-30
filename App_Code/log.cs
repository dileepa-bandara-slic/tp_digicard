using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for log
/// </summary>
public class log
{
    public void write_log(string msg)
    {


        try
        {
            var file = @"D:\\WebLogs\\MotorTP_API.log";
            var fs = File.Open(file, FileMode.Append, FileAccess.Write, FileShare.Read);
            var sw = new StreamWriter(fs);
            sw.AutoFlush = true;
            sw.Write(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss tt"));
            sw.Write(" : ");
            sw.WriteLine(msg);
            //sw.WriteLine("**********************************************************");

            sw.Close();
        }
        catch
        {

        }

    }
}