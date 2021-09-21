using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KLTS.Data;
using System.Data;
using System.Globalization;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Script.Serialization;
using System.Web.Security;
using iTextSharp.text.html.simpleparser;
using System.Collections;
using System.Data.OleDb;
using Jawan.Portal.DAL;
namespace Jawan.Portal
{
    public partial class ImportEmpLoanDetails : System.Web.UI.Page
    {
        AppConfiguration config = new AppConfiguration();
        GridViewExportUtil gve = new GridViewExportUtil();
        string EmpIDPrefix = "";
        string CmpIDPrefix = "";
        string UserID = "";
        string Username = "";
        string Elength = "";
        string Clength = "";
        string Fontstyle = "";
        string BranchID = "";


        protected void GetWebConfigdata()
        {

            EmpIDPrefix = Session["EmpIDPrefix"].ToString();
            CmpIDPrefix = Session["CmpIDPrefix"].ToString();
            UserID = Session["UserId"].ToString();

        }



        protected void Page_Load(object sender, EventArgs e)
        {
            sampleGrid();

            try
            {

                GetWebConfigdata();
                if (!IsPostBack)
                {
                    if (Session["UserId"] != null && Session["AccessLevel"] != null)
                    {
                        PreviligeUsers(Convert.ToInt32(Session["AccessLevel"]));
                        lblDisplayUser.Text = Session["UserId"].ToString();
                        lblcname.Text = SqlHelper.Instance.GetCompanyname();
                        switch (SqlHelper.Instance.GetCompanyValue())
                        {
                            case 0:// Write Omulance Invisible Links
                                break;
                            case 1://Write KLTS Invisible Links
                                ExpensesReportsLink.Visible = false;
                                break;
                            case 2://write Fames Link
                                ExpensesReportsLink.Visible = true;
                                break;


                            default:
                                break;
                        }

                    }
                    else
                    {
                        Response.Redirect("login.aspx");
                    }

                    ExcelNos();
                }
            }
            catch (Exception ex)
            {

            }

        }

        public void ExcelNos()
        {
            string qry = "select distinct excel_no from emploanmaster order by excel_no desc";
            DataTable dt = config.ExecuteAdaptorAsyncWithQueryParams(qry).Result;
            if (dt.Rows.Count > 0)
            {
                ddlExcelNo.DataValueField = "excel_no";
                ddlExcelNo.DataTextField = "excel_no";
                ddlExcelNo.DataSource = dt;
                ddlExcelNo.DataBind();

            }

            ddlExcelNo.Items.Insert(0, "-Select-");

        }

        protected void PreviligeUsers(int previligerid)
        {
            switch (previligerid)
            {
                case 1:
                    break;
                case 2:
                    EmployeeReportLink.Visible = true;
                    ClientsReportLink.Visible = false;
                    InventoryReportLink.Visible = false;

                    ClientsLink.Visible = true;
                    CompanyInfoLink.Visible = false;
                    InventoryLink.Visible = false;
                    ReportsLink.Visible = true;
                    SettingsLink.Visible = true;
                    break;

                case 3:
                    EmployeeReportLink.Visible = true;
                    ClientsReportLink.Visible = true;
                    InventoryReportLink.Visible = true;

                    EmployeesLink.Visible = true;
                    ClientsLink.Visible = true;
                    CompanyInfoLink.Visible = false;
                    InventoryLink.Visible = false;
                    ReportsLink.Visible = true;
                    SettingsLink.Visible = true;

                    break;

                case 4:
                    EmployeeReportLink.Visible = true;
                    ClientsReportLink.Visible = true;
                    InventoryReportLink.Visible = true;

                    EmployeesLink.Visible = true;
                    ClientsLink.Visible = true;
                    CompanyInfoLink.Visible = true;
                    InventoryLink.Visible = true;
                    ReportsLink.Visible = true;
                    SettingsLink.Visible = true;
                    break;
                case 5:
                    EmployeeReportLink.Visible = true;
                    ClientsReportLink.Visible = true;
                    InventoryReportLink.Visible = true;

                    EmployeesLink.Visible = true;
                    ClientsLink.Visible = true;
                    CompanyInfoLink.Visible = false;
                    InventoryLink.Visible = false;
                    ReportsLink.Visible = true;
                    ReportsLink.Visible = true;
                    SettingsLink.Visible = true;

                    break;
                case 6:
                    EmployeesLink.Visible = false;
                    ClientsLink.Visible = false;
                    CompanyInfoLink.Visible = false;
                    InventoryLink.Visible = true;
                    ReportsLink.Visible = true;
                    SettingsLink.Visible = false;
                    break;
                default:
                    break;


            }
        }









        public void sampleGrid()
        {

            string query = "select top 1 '' as 'ID NO','' as 'Loan Type', '' as 'Amount', '' as 'NoofInstalments','' as 'LoanIssuedDate', '' as 'LoanCuttingFrom' from Emploandetails";

            DataTable dt = config.ExecuteAdaptorAsyncWithQueryParams(query).Result;
            if (dt.Rows.Count > 0)
            {
                GvInputEmpLoanDetails.DataSource = dt;
                GvInputEmpLoanDetails.DataBind();
            }
            else
            {
                GvInputEmpLoanDetails.DataSource = null;
                GvInputEmpLoanDetails.DataBind();
            }

        }


        protected void LinkSample_Click(object sender, EventArgs e)
        {
            gve.Export("SampleLoanDetailsSheet.xls", this.GvInputEmpLoanDetails);
        }

        public string GetExcelSheetNames()
        {
            string ExcelSheetname = "";
            OleDbConnection con = null;
            DataTable dt = null;
            string filename = Path.Combine(Server.MapPath("~/ImportDocuments"), Guid.NewGuid().ToString() + Path.GetExtension(FlUploadLoanDetails.PostedFile.FileName));
            FlUploadLoanDetails.PostedFile.SaveAs(filename);
            string extn = Path.GetExtension(FlUploadLoanDetails.PostedFile.FileName);
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


            return ExcelSheetname;
        }

        public void LoanImportedData(string ExcelNo)
        {

            GvLoansImported.DataSource = null;
            GvLoansImported.DataBind();

            string qry = "select emploanmaster.empid,(empfname+''+empmname+''+emplname) as Empname,LoanAmount,NoInstalments,convert(varchar(10),LoanIssuedDate,103) as LoanIssuedDate,convert(varchar(10),LoanDt,103) as LoanDt,case typeofloan when 0 then 'Sal adv'  when 1 then 'Uniform' when 2 then 'Security Dep' when 3 then 'Loan' when 4 then 'Other Ded'   " +
                         " end as Loantype,typeofloan, emploanmaster.Created_by,Datetime as created_On from emploanmaster  inner join empdetails ed on ed.empid=emploanmaster.empid where Excel_No='" + ExcelNo + "' and Excel_No is not null and Excel_No<>'' order by typeofloan";
            DataTable dt = config.ExecuteAdaptorAsyncWithQueryParams(qry).Result;

            if (dt.Rows.Count > 0)
            {
                GvLoansImported.Visible = true;
                GvLoansImported.DataSource = dt;
                GvLoansImported.DataBind();
            }
            else
            {
                GvLoansImported.Visible = false;
                GvLoansImported.DataSource = null;
                GvLoansImported.DataBind();

            }
        }

        protected void btnsave_Click(object sender, EventArgs e)
        {
            string filename = Path.Combine(Server.MapPath("~/ImportDocuments"), Guid.NewGuid().ToString() + Path.GetExtension(FlUploadLoanDetails.PostedFile.FileName));
            FlUploadLoanDetails.PostedFile.SaveAs(filename);
            string extn = Path.GetExtension(FlUploadLoanDetails.PostedFile.FileName);
            string constring = "";
            if (extn.ToLower() == ".xls")
            {
                //constring = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filename + ";Extended properties=\"excel 8.0;HDR=Yes;IMEX=2\"";
                constring = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filename + ";Extended properties=\"excel 12.0;HDR=Yes;IMEX=2\"";
            }
            else if (extn.ToLower() == ".xlsx")
            {
                constring = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filename + ";Extended properties=\"excel 12.0;HDR=Yes;IMEX=2\"";
            }

            string Sheetname = string.Empty;




            string qry = "select [ID NO],[Loan Type],[Amount],[NoofInstalments],[LoanIssuedDate],[LoanCuttingFrom]" +
            "  from  [" + GetExcelSheetNames() + "]" + "";


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

            #region Begin Getmax Id from DB
            int ExcelNo = 0;
            string selectquerycomppanyid = "select max(cast(Excel_No as int )) as Id from emploanmaster";
            DataTable dt = config.ExecuteAdaptorAsyncWithQueryParams(selectquerycomppanyid).Result;

            if (dt.Rows.Count > 0)
            {
                if (String.IsNullOrEmpty(dt.Rows[0]["Id"].ToString()) == false)
                {
                    ExcelNo = Convert.ToInt32(dt.Rows[0]["Id"].ToString()) + 1;
                }
                else
                {
                    ExcelNo = int.Parse("1");
                }
            }
            #endregion End Getmax Id from DB

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                string Empid = string.Empty;
                string Loantype = string.Empty;
                String Amount = "0";
                int NoOfInstalments = 1;
                string LoanIssuedDate = "";
                string LoanCuttingMonth = "";
                int loanStatus = 0;
                string TypeOfLoan = "0";
                string Date = "";





                Empid = ds.Tables[0].Rows[i]["ID NO"].ToString();
                TypeOfLoan = ds.Tables[0].Rows[i]["Loan Type"].ToString();
                Amount = ds.Tables[0].Rows[i]["Amount"].ToString();
                if (ds.Tables[0].Rows[i]["NoofInstalments"].ToString().Length > 0)
                {
                    NoOfInstalments = int.Parse(ds.Tables[0].Rows[i]["NoofInstalments"].ToString());
                }

                LoanIssuedDate = ds.Tables[0].Rows[i]["LoanIssuedDate"].ToString();
                LoanCuttingMonth = ds.Tables[0].Rows[i]["LoanCuttingFrom"].ToString();

                if (LoanIssuedDate.Length > 0)
                {
                    string db1 = Convert.ToDateTime(LoanIssuedDate).ToString("dd/MM/yyyy");

                    LoanIssuedDate = Timings.Instance.CheckDateFormat(db1);
                }


                if (LoanCuttingMonth.Length > 0)
                {
                    string db2 = Convert.ToDateTime(LoanCuttingMonth).ToString("dd/MM/yyyy");

                    LoanCuttingMonth = Timings.Instance.CheckDateFormat(db2);
                }

                Date = DateTime.Now.ToString("dd/MM/yyyy");
                Date = Timings.Instance.CheckDateFormat(Date);


                if (Empid.Length > 0)
                {

                    string insertquery = string.Format(" insert into EmpLoanMaster(loandt,empid,loantype,loanamount,NoInstalments,  " +
                  " LoanStatus,TypeOfLoan,LoanIssuedDate,Created_By,Created_On,Excel_No) values( '{0}','{1}','{2}','{3}',{4},{5},'{6}','{7}','{8}','{9}','{10}') ",
                  LoanCuttingMonth, Empid, Loantype,
                  Amount, NoOfInstalments, loanStatus, TypeOfLoan, LoanIssuedDate, UserID, Date, ExcelNo);
                    int status =config.ExecuteNonQueryWithQueryAsync(insertquery).Result;
                    if (status != 0)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showlalert", "alert('New Loans Generated Successfuly');", true);

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showlalert", "alert('Loan not Generated for '" + Empid + "'');", true);
                    }
                }

                LoanImportedData(ExcelNo.ToString());
                ExcelNos();
            }

        }

        int subTotalRowIndex = 0;
        decimal TotalLoanAmt = 0;
        string currentId = "";

        protected void GvLoansImported_RowCreated(object sender, GridViewRowEventArgs e)
        {
            TotalLoanAmt = 0;

            DataTable dt = new DataTable();
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                if (e.Row.DataItem is DataRowView
                       && (e.Row.DataItem as DataRowView).DataView.Table != null)
                {
                    dt = (e.Row.DataItem as DataRowView).DataView.Table;
                    string orderId = (dt.Rows[e.Row.RowIndex]["typeofloan"].ToString());

                    if (orderId != currentId)
                    {
                        if (e.Row.RowIndex > 0)
                        {
                            for (int i = subTotalRowIndex; i < e.Row.RowIndex; i++)
                                TotalLoanAmt += Convert.ToDecimal(GvLoansImported.Rows[i].Cells[4].Text);

                            this.AddTotalRow("Total", TotalLoanAmt.ToString("N2"));

                        }
                        subTotalRowIndex = e.Row.RowIndex;

                        currentId = orderId;
                    }

                }
            }
        }

        private void AddTotalRow(string labelText, string LoanAmount)
        {
            GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal);


            row.Cells.AddRange(new TableCell[11] { new TableCell {CssClass="SubTotalRowStyle"}, //Empty Cell
                                        new TableCell {CssClass="SubTotalRowStyle"},
                                        new TableCell {CssClass="SubTotalRowStyle"},
                                        new TableCell { Text = labelText, HorizontalAlign = HorizontalAlign.Right,CssClass="SubTotalRowStyle"},
                                        new TableCell { Text = LoanAmount, HorizontalAlign = HorizontalAlign.Right,CssClass="SubTotalRowStyle" },
                                        new TableCell {CssClass="SubTotalRowStyle"},
                                        new TableCell {CssClass="SubTotalRowStyle"},
                                        new TableCell {CssClass="SubTotalRowStyle"},
                                        new TableCell {CssClass="SubTotalRowStyle"},
                                        new TableCell {CssClass="SubTotalRowStyle"},
                                         new TableCell {CssClass="SubTotalRowStyle"}



        });

            GvLoansImported.Controls[0].Controls.Add(row);


        }

        protected void GvLoansImported_DataBound(object sender, EventArgs e)
        {
            if (GvLoansImported.Rows.Count > 0)
            {

                for (int i = subTotalRowIndex; i < GvLoansImported.Rows.Count; i++)
                    TotalLoanAmt += Convert.ToDecimal(GvLoansImported.Rows[i].Cells[4].Text);


                this.AddTotalRow("Total", TotalLoanAmt.ToString("N2"));
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string Excelno = ddlExcelNo.SelectedValue;
            LoanImportedData(Excelno);
        }
    }
}