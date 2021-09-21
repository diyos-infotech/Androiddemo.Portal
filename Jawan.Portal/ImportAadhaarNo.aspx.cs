using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Globalization;
using System.IO;
using System.Data.OleDb;
using KLTS.Data;
using System.Data;
using System.Data.SqlClient;
using Jawan.Portal.DAL;
namespace Jawan.Portal
{
    public partial class ImportAadhaarNo : System.Web.UI.Page
    {
        AppConfiguration config = new AppConfiguration();
        GridViewExportUtil gve = new GridViewExportUtil();
        DataTable dt;
        string CmpIDPrefix = "";
        string EmpIDPrefix = "";
        string Elength = "";
        string Clength = "";
        protected void Page_Load(object sender, EventArgs e)
        {

            GetWebConfigdata();

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

                SampleExport();

            }

        }

        bool EmpStatus = false;

        protected void GetWebConfigdata()
        {
            EmpIDPrefix = Session["EmpIDPrefix"].ToString();
            CmpIDPrefix = Session["CmpIDPrefix"].ToString();
        }




        protected void Cleardata()
        {
            gvlistofemp.DataSource = null;
            gvlistofemp.DataBind();


        }

        public void SampleExport()
        {

            string query = "Select top 1 '' as EmpID,'' as AadharCardNo,''  from EmpProofDetails";
            DataTable dt = config.ExecuteAdaptorAsyncWithQueryParams(query).Result;
            gvlistofemp.DataSource = dt;
            gvlistofemp.DataBind();

        }

        protected void lnkSample_Click(object sender, EventArgs e)
        {
            gve.Export("SampleAadharNo.xls", this.gvlistofemp);

        }

        public void NotInsertGridDisplay()
        {

            string SelectQry = "select * from NotInsertDataAadhaarNo ";
            DataTable dtQry = config.ExecuteAdaptorAsyncWithQueryParams(SelectQry).Result;

            if (dtQry.Rows.Count > 0)
            {
                GvNotInsertedlist.Visible = true;
                GvNotInsertedlist.DataSource = dtQry;
                GvNotInsertedlist.DataBind();
            }

        }


        protected void BtnSave_Click(object sender, EventArgs e)
        {
            string deleteQry = "";
            deleteQry = "delete from NotInsertDataAadhaarNo ";
            int del=config.ExecuteNonQueryWithQueryAsync(deleteQry).Result;

            GvNotInsertedlist.DataSource = null;
            GvNotInsertedlist.DataBind();
            BtnUnSave.Visible = false;

            int result = 0;
            string ExcelSheetname = "";
            string FileName = FlUploadAadhaarNo.FileName;
            string path = Path.Combine(Server.MapPath("~/ImportDocuments"), Guid.NewGuid().ToString() + Path.GetExtension(FlUploadAadhaarNo.PostedFile.FileName));
            FlUploadAadhaarNo.PostedFile.SaveAs(path);

            OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=Excel 12.0;");
            con.Open();
            dt = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            ExcelSheetname = dt.Rows[0]["TABLE_NAME"].ToString();

            OleDbCommand cmd = new OleDbCommand("Select [Emp ID],[Aadhaar No] from [" + ExcelSheetname + "]", con);
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            DataTable ds = new DataTable();
            da.Fill(ds);
            da.Dispose();
            con.Close();
            con.Dispose();
            GC.Collect();

            using (SqlConnection sqlcon = new SqlConnection(ConfigurationManager.ConnectionStrings["KLTSConnectionString"].ConnectionString))
            {
                sqlcon.Open();

                string EmpID = ""; string AadhaarNO = "";


                for (int i = 0; i < ds.Rows.Count; i++)
                {
                    EmpID = ds.Rows[i]["Emp ID"].ToString();
                    AadhaarNO = ds.Rows[i]["Aadhaar No"].ToString();

                    if (EmpID.Length > 0)
                    {
                        string QryCheck = "select empid from empdetails where empid='" + EmpID + "'";
                        DataTable dtQryCheck = config.ExecuteAdaptorAsyncWithQueryParams(QryCheck).Result;


                        string SelectQry = "select empid,AadharCardNo from EmpProofDetails where AadharCardNo='" + AadhaarNO + "' and AadharCardNo!=''";
                        DataTable dtQry = config.ExecuteAdaptorAsyncWithQueryParams(SelectQry).Result;


                        if (dtQryCheck.Rows.Count > 0)
                        {

                            deleteQry = "delete from NotInsertDataAadhaarNo where empid='" + EmpID + "'";
                            result =config.ExecuteNonQueryWithQueryAsync(deleteQry).Result;

                            if (result > 0)
                            {
                                GvNotInsertedlist.DataSource = null;
                                GvNotInsertedlist.DataBind();
                                BtnUnSave.Visible = false;
                            }

                            //if (empidE.Length>0)
                            //{
                            if (dtQry.Rows.Count > 0)
                            {
                                string empidE = dtQry.Rows[0]["Empid"].ToString();

                                if (AadhaarNO == dtQry.Rows[0]["AadharCardNo"].ToString() && EmpID == empidE)
                                {
                                    string UpdatQry = "update EmpProofDetails set AadharCardNo='" + AadhaarNO + "',AadharCard='Y' where empid='" + EmpID + "'";
                                    result =config.ExecuteNonQueryWithQueryAsync(UpdatQry).Result;
                                    if (result > 0)
                                    {
                                        ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Employee Aadhaar NOs Updated Successfully');", true);

                                    }

                                }
                                else
                                {
                                    string Remark = "AadharCardNo already exists for empid " + empidE + "";
                                    string InsertQrys = "insert into NotInsertDataAadhaarNo (Empid,AadharCardNo,Remark) values ('" + EmpID + "','" + AadhaarNO + "','" + Remark + "')";
                                    result =config.ExecuteNonQueryWithQueryAsync(InsertQrys).Result;
                                    BtnUnSave.Visible = true;
                                }
                            }
                            else
                            {
                                string UpdatQry = "update EmpProofDetails set AadharCardNo='" + AadhaarNO + "',AadharCard='Y' where empid='" + EmpID + "'";
                                result = config.ExecuteNonQueryWithQueryAsync(UpdatQry).Result;
                                if (result > 0)
                                {
                                    ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Employee Aadhaar NOs Updated Successfully');", true);
                                }
                            }
                        }
                        else
                        {
                            string Remark = "Empid " + EmpID + " doesnt exists ";

                            string InsertQryDuples = "insert into NotInsertDataAadhaarNo (Empid,AadharCardNo,Remark) values ('" + EmpID + "','" + AadhaarNO + "','" + Remark + "')";
                            result =config.ExecuteNonQueryWithQueryAsync(InsertQryDuples).Result;
                            BtnUnSave.Visible = true;
                        }


                    }

                    NotInsertGridDisplay();
                }
            }
        }

        protected void BtnUnSave_Click(object sender, EventArgs e)
        {
            if (GvNotInsertedlist.Rows.Count > 0)
            {

                gve.Export("UnSavedAadhaarIdData.xls", this.GvNotInsertedlist);
            }
        }
    }
}