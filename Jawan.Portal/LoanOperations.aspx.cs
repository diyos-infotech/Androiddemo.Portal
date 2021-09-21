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
using KLTS.Data;
using System.Globalization;
using Jawan.Portal.DAL;
namespace Jawan.Portal
{
    public partial class LoanOperations : System.Web.UI.Page
    {
        AppConfiguration config = new AppConfiguration();
        GridViewExportUtil gve = new GridViewExportUtil();
        protected void Page_Load(object sender, EventArgs e)
        {
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
            }
        }

        protected void PreviligeUsers(int previligerid)
        {
            switch (previligerid)
            {
                case 1:
                    break;
                case 2:
                    InventoryLink.Visible = false;
                    CompanyInfoLink.Visible = false;
                    ReportsLink.Visible = false;
                    SettingsLink.Visible = false;
                    break;

                case 3:
                    InventoryLink.Visible = false;
                    ReportsLink.Visible = false;
                    SettingsLink.Visible = false;
                    break;
                case 4:
                    InventoryLink.Visible = false;
                    CompanyInfoLink.Visible = false;
                    ReportsLink.Visible = false;
                    SettingsLink.Visible = false;
                    break;
                case 5:
                    InventoryLink.Visible = false;
                    CompanyInfoLink.Visible = false;
                    ReportsLink.Visible = false;
                    SettingsLink.Visible = false;
                    break;
                case 6:
                    InventoryLink.Visible = false;
                    CompanyInfoLink.Visible = false;
                    ReportsLink.Visible = false;
                    SettingsLink.Visible = false;
                    break;
                default:
                    break;
            }
        }

        protected void ddlLoanOperation_OnSelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnsave_OnClick(object sender, EventArgs e)
        {
            gvLoanOperations.DataSource = null;
            gvLoanOperations.DataBind();

            gvLoanOperations2.DataSource = null;
            gvLoanOperations2.DataBind();
            #region Begin Performing  a validation
            #region Begin Validating LoanOperation,FromDate and ToDate
            if (ddlLoanOperation.SelectedIndex == 0 || txtFromDate.Text.Trim().Length == 0 || txtToDate.Text.Trim().Length == 0)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "showlalert", "alert('You should select the following:LoanOperation,Fromdate and ToDate');", true);
                return;
            }

            #endregion End Validating LoanOperation,FromDate and ToDate
            #region Begin Validating Date Format for FormDate and Todate
            var testFromDate = 0; var testToDate = 0;
            if (txtFromDate.Text.Trim().Length > 0 || txtToDate.Text.Trim().Length > 0)
            {
                testFromDate = GlobalData.Instance.CheckEnteredDate(txtFromDate.Text);
                testToDate = GlobalData.Instance.CheckEnteredDate(txtToDate.Text.Trim());
                if (testFromDate > 0 || testToDate > 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('You are entered invalid FormDate/ToDate format it should be [DD/MM/YYYY].Ex.01/01/1990');", true);
                    return;
                }
            }
            #endregion End Validating Date Format for FormDate and Todate
            #endregion End performing a validation
            DateTime Fromdate = DateTime.Parse(txtFromDate.Text, CultureInfo.GetCultureInfo("en-gb"));
            DateTime Todate = DateTime.Parse(txtToDate.Text, CultureInfo.GetCultureInfo("en-gb"));
            string Fromdate1 = Fromdate.ToString().Remove(10);
            string Todate1 = Todate.ToString().Remove(10);
            if (ddlLoanOperation.SelectedIndex == 1)
            {

                string sqlretrieve = "select  distinct MLM.Loanno,case ELM.TypeOfLoan when '0' then 'Sal.Adv' when '1' then 'Uniform' else 'Others' End as LoanType, MLM.Empid,ISNULL(ED.EmpFName,'')+''+ISNULL(ED.EmpMName,'')+''+ISNULL(ED.EmpLName,'') as EmpName,MLM.ModifiedLoanAmt as LoanAmt,Convert(Varchar(10),ELM.LoanIssuedDate,103) as LoanIssuedDate,Convert(Varchar(10),MLM.ModifidedLoanCutMon,103) as ModifidedLoanCutMon,MLM.NoInstalments as NoOfInst,Convert(varchar(10),MLM.ModifiedTime,103) as dt " +
                "from ModifidedLoanMaster as MLM join EmpLoanMaster as ELM on MLM.Loanno=ELM.LoanNo join EmpDetails ED on ELM.EmpId=ED.EmpId and convert(smalldatetime,convert(varchar(15),MLM.ModifiedTime,101),101) Between convert(smalldatetime,'" + Fromdate1.Trim() + "',101) and convert(smalldatetime,'" + Todate1.Trim() + "',101)";

                DataTable dtRetrieve = config.ExecuteAdaptorAsyncWithQueryParams(sqlretrieve).Result;

                if (dtRetrieve.Rows.Count > 0)
                {
                    gvLoanOperations.DataSource = dtRetrieve;
                    gvLoanOperations.DataBind();
                }
                else
                {
                    gvLoanOperations.DataSource = null;
                    gvLoanOperations.DataBind();
                    ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('There is no ModifiedLoans between FromDate and ToDate');", true);
                    return;
                }
            }
            if (ddlLoanOperation.SelectedIndex == 2)
            {
                /*string sqlretrieve = "select  distinct DL.Loanno,case DL.TypeOfLoan when '0' then 'Sal.Adv' when '1' then 'Uniform' else 'Others' End as LoanType, DL.Empid,ISNULL(ED.EmpFName,'')+''+ISNULL(ED.EmpMName,'')+''+ISNULL(ED.EmpLName,'') as EmpName,DL.LoanAmount as LoanAmt,Convert(Varchar(10),DL.LoanIssuedDate,103) as LoanIssuedDate,Convert(Varchar(10),ELM.LoanDt,103) as ModifidedLoanCutMon,DL.NoInstalments as NoOfInst,Convert(varchar(10),DL.DeletedTime,103) as dtd " +
                " from DeletedLoan as DL join EmpLoanMaster as ELM on DL.Loanno=ELM.LoanNo join EmpDetails ED on ELM.EmpId=ED.EmpId and convert(varchar(15),DL.DeletedTime,101) Between '" + Fromdate1.Trim() + "' and '" + Todate1.Trim() + "'";*/
                string sqlretrieve = "select  distinct DL.Loanno,case DL.TypeOfLoan when '0' then 'Sal.Adv' when '1' then 'Uniform' else 'Others' End as LoanType, DL.Empid," +
                "ISNULL(ED.EmpFName,'')+''+ISNULL(ED.EmpMName,'')+''+ISNULL(ED.EmpLName,'') as EmpName,DL.LoanAmount as LoanAmt,Convert(Varchar(10)," +
                "DL.LoanIssuedDate,103) as LoanIssuedDate,Convert(Varchar(10),DL.LoanDt,103) as ModifidedLoanCutMon,DL.NoInstalments as NoOfInst," +
                "Convert(varchar(10),DL.DeletedTime,103) as dtd  from DeletedLoan as DL  " +
                "join EmpDetails ED on DL.EmpId=ED.EmpId and convert(smalldatetime,convert(varchar(15),DL.DeletedTime,101),101) Between convert(smalldatetime,'" + Fromdate1.Trim() + "',101) and convert(smalldatetime,'" + Todate1.Trim() + "',101)";




                DataTable dtRetrieve = config.ExecuteAdaptorAsyncWithQueryParams(sqlretrieve).Result;
                if (dtRetrieve.Rows.Count > 0)
                {
                    gvLoanOperations2.DataSource = dtRetrieve;
                    gvLoanOperations2.DataBind();
                }
                else
                {
                    gvLoanOperations2.DataSource = null;
                    gvLoanOperations2.DataBind();
                    ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('There is no DeletedLoans between FromDate and ToDate');", true);
                    return;
                }

            }


            txtFromDate.Text = "";
            txtToDate.Text = "";
        }
    }
}