using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DocumentEntryDTO
/// </summary>
public class DocumentEntryDTO
{
    public string BusinessType { get; set; }
    public string DocumentType { get; set; }
    public string BranchCode { get; set; }
    public string ReceiptNo { get; set; }
    public string ReceiptDate { get; set; }
    public string RefrenceType { get; set; }
    public string EntryUser { get; set; }
    public string AppType { get; set; }
    public string MobileNum { get; set; }

    public string SMSUrgent { get; set; }
}