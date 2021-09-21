using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;
using System.Collections;
using System.Globalization;
using System.Data.OleDb;
using KLTS.Data;
using Jawan.Portal.DAL;
namespace Jawan.Portal
{
    public partial class ImportBankPfEsiNos : System.Web.UI.Page
    {
        AppConfiguration config = new AppConfiguration();
        GridViewExportUtil gve = new GridViewExportUtil();
        protected void Page_Load(object sender, EventArgs e)
        {
            // CmpIDPrefix = Session["CmpIDPrefix"].ToString();
            if (!IsPostBack)
            {
                if (Session["UserId"] != null && Session["AccessLevel"] != null)
                {
                    string PID = Session["AccessLevel"].ToString();
                    // PreviligeUsers(PID);
                    switch (SqlHelper.Instance.GetCompanyValue())
                    {
                        case 0:// Write Frames Invisible Links
                            break;
                        case 1://Write KLTS Invisible Links
                            // ReceiptsLink.Visible = true;
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    Response.Redirect("login.aspx");
                }

                string ImagesFolderPath = Server.MapPath("ImportDocuments");
                string[] filePaths = Directory.GetFiles(ImagesFolderPath);

                foreach (string file in filePaths)
                {
                    File.Delete(file);
                }

            }
        }

        public string GetExcelSheetNames()
        {
            string ExcelSheetname = "";
            OleDbConnection con = null;
            DataTable dt = null;
            string filename = Path.Combine(Server.MapPath("~/ImportDocuments"), Guid.NewGuid().ToString() + Path.GetExtension(fileupload1.PostedFile.FileName));
            fileupload1.PostedFile.SaveAs(filename);
            string extn = Path.GetExtension(fileupload1.PostedFile.FileName);
            string conStr = string.Empty;
            if (extn.ToLower() == ".xls")
            {
                conStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filename + ";Extended properties=\"excel 8.0;HDR=Yes;IMEX=2\"";
            }
            else if (extn.ToLower() == ".xlsx")
            {
                conStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filename + ";Extended properties=\"excel 12.0;HDR=Yes;IMEX=2\"";
            }

            con = new OleDbConnection(conStr);
            con.Open();
            dt = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

            if (dt == null)
            {
                return null;
            }
            ExcelSheetname = dt.Rows[0]["TABLE_NAME"].ToString();
            ////foreach (DataRow row in dt.Rows)
            ////{
            ////    ExcelSheetname = row["TABLE_NAME"].ToString();
            ////}

            return ExcelSheetname;
        }

        protected void btnImport_Click(object sender, EventArgs e)
        {
            try
            {

                string filename = Path.Combine(Server.MapPath("ImportDocuments"), Guid.NewGuid().ToString() + Path.GetExtension(fileupload1.PostedFile.FileName));
                fileupload1.PostedFile.SaveAs(filename);
                string extn = Path.GetExtension(fileupload1.PostedFile.FileName);
                string constring = string.Empty;

                if (extn.ToLower() == ".xls")
                {
                    constring = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source='" + filename + "';Extended Properties=\"excel 12.0;HDR=Yes;IMEX=2\"";
                }
                if (extn.ToLower() == ".xlsx")
                {
                    constring = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source='" + filename + "';Extended Properties=\"excel 12.0;HDR=Yes;IMEX=2\"";
                }
                string qry = "select [Emp Id],[Bank Account No],[PF No],[ESI No] from [" + GetExcelSheetNames() + "]" + "";
                OleDbConnection con = new OleDbConnection(constring);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                OleDbCommand cmd = new OleDbCommand(qry, con);
                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                da.Dispose();
                con.Close();
                con.Dispose();

                string empids = string.Empty;

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    string empid = string.Empty;
                    string bankAccountno = string.Empty;
                    string pfno = string.Empty;
                    string esino = string.Empty;

                    int status = 0;

                    empid = dr["Emp Id"].ToString();
                    bankAccountno = dr["Bank Account No"].ToString();
                    pfno = dr["PF No"].ToString();
                    esino = dr["ESI No"].ToString();

                    //ArrayList lstempids = new ArrayList();


                    if (empid.Length == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "show alert()", "alert('Please enter employee id');", true);
                        return;
                    }
                    if (bankAccountno.Length == 0 && pfno.Length == 0 && esino.Length == 0)
                    {
                        //lstempids.Add(empid);
                        empids += empid + ",";
                        //ScriptManager.RegisterStartupScript(this, GetType(), "show alert()", "alert('Please enter Bank A/c No or PF No or ESI No');", true);
                        //return;
                    }

                    string sqlempid = "select empid from empdetails where empid='" + empid + "'";
                    DataTable dtempid = config.ExecuteAdaptorAsyncWithQueryParams(sqlempid).Result;
                    if (dtempid.Rows.Count == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "show alert()", "alert('Please enter valid empid');", true);
                        //return;
                    }
                    else
                    {

                        string message = string.Empty;
                        Hashtable Addbankpfesi = new Hashtable();
                        string Addbankpfesisp = "InsertBankPfEsiNos";

                        Addbankpfesi.Add("@empid", empid);
                        Addbankpfesi.Add("@bankacno", bankAccountno);
                        Addbankpfesi.Add("@pfno", pfno);
                        Addbankpfesi.Add("@esino", esino);


                        DataTable dtproce =config.ExecuteAdaptorAsyncWithParams(Addbankpfesisp, Addbankpfesi).Result;
                        for (int i = 0; i < dtproce.Rows.Count; i++)
                        {
                            message = dtproce.Rows[i]["Message1"].ToString();
                        }
                        if (message.Length > 0)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "Show alert()", "alert('" + message + "');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "Show alert()", "alert('Records added sucessfully');", true);
                        }
                        //else
                        //{
                        //    ScriptManager.RegisterStartupScript(this, GetType(), "Show alert()", "alert('Records are not inserted sucessfully');", true);
                        //}
                    }

                }
                if (empids.Length > 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "show alert()", "alert('Please enter Bank A/c No or PF No or ESI No for " + empids.Remove(empids.Length - 1) + "');", true);
                }

            }
            catch (Exception ex)
            {

            }
        }
    }
}