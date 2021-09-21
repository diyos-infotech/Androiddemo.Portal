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
    public partial class ReportForBankTransactions : System.Web.UI.Page
    {
        AppConfiguration config = new AppConfiguration();
        GridViewExportUtil gve = new GridViewExportUtil();
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                if (!IsPostBack)
                {
                    if (Session["UserId"] != null && Session["AccessLevel"] != null)
                    {
                        lblDisplayUser.Text = Session["UserId"].ToString();
                        PreviligeUsers(Convert.ToInt32(Session["AccessLevel"]));
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
                    LoadBankNames();
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Show alert", "alert('Your Session Expired');", true);
                Response.Redirect("~/Login.aspx");
            }
        }

        protected void PreviligeUsers(int previligerid)
        {
            switch (previligerid)
            {
                case 1:
                    break;
                case 2:

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
                    EmployeeReportLink.Visible = false;
                    break;
                default:
                    break;
            }
        }



        protected void LoadBankNames()
        {

            DataTable DtBankNames = GlobalData.Instance.LoadBankNames();
            if (DtBankNames.Rows.Count > 0)
            {
                Ddl_From_Bank.DataValueField = "bankid";
                Ddl_From_Bank.DataTextField = "bankname";
                Ddl_From_Bank.DataSource = DtBankNames;
                Ddl_From_Bank.DataBind();

            }
            Ddl_From_Bank.Items.Insert(0, "-Select-");
            Ddl_From_Bank.Items.Insert(1, "ALL");

        }


        protected void btnsearch_Click(object sender, EventArgs e)
        {

            string date = string.Empty;

            #region Begin Code  For Validation as on [16-11-2013]

            if (Ddl_From_Bank.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Please Select The Bank Name');", true);
                return;
            }

            if (Ddl_Report_Type.SelectedIndex > 0)
            {
                if (txtmonth.Text.Trim().Length == 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Please Select The Month');", true);
                    return;
                }

            }
            #endregion End  Code  For Validation as on [16-11-2013]

            #region  Begin Code For Variable Declaration   as on [16-11-2013]
            var SPNameForCredit = "";
            var SPNameForDebit = "";
            var GetDate = "";
            var ReportType = 0;
            var BankId = "0";
            DataTable DtBankTransactionsCredit = null;
            Hashtable HtBankTransactionsCredit = new Hashtable();

            DataTable DtBankTransactionsDebit = null;
            Hashtable HtBankTransactionsDebit = new Hashtable();


            #endregion End  Code For Variable Declaration  as on [16-11-2013]

            if (txtmonth.Text.Trim().Length > 0)
            {
                date = DateTime.Parse(txtmonth.Text.Trim(), CultureInfo.GetCultureInfo("en-gb")).ToString();
                GetDate = date;
            }

            #region  Begin Code For Assign Values to Variables   as on [16-11-2013]
            BankId = Ddl_From_Bank.SelectedValue;
            if (Ddl_From_Bank.SelectedIndex == 1)
            {
                BankId = "0";
            }

            ReportType = Ddl_Report_Type.SelectedIndex;
            HtBankTransactionsCredit.Add("@GetDate", GetDate);
            HtBankTransactionsCredit.Add("@ReportType", ReportType);
            HtBankTransactionsCredit.Add("@BankId", BankId);


            HtBankTransactionsDebit.Add("@GetDate", GetDate);
            HtBankTransactionsDebit.Add("@ReportType", ReportType);
            HtBankTransactionsDebit.Add("@BankId", BankId);



            SPNameForCredit = "ReportForBankTransactionForCredit";
            SPNameForDebit = "ReportForBankTransactionForDebit";

            #endregion End  Code For Assign Values to Variables  as on [16-11-2013]

            #region  Begin code For Calling Stored Procedue  and Data To Gridview  As on [16-11-2013]
            DtBankTransactionsCredit =config.ExecuteAdaptorAsyncWithParams(SPNameForCredit, HtBankTransactionsCredit).Result;
            DtBankTransactionsDebit = config.ExecuteAdaptorAsyncWithParams(SPNameForDebit, HtBankTransactionsDebit).Result;

            if (DtBankTransactionsCredit.Rows.Count > 0)
            {
                GV_BankTransactionsCredit.DataSource = DtBankTransactionsCredit;
                GV_BankTransactionsCredit.DataBind();

                lbtn_Export.Visible = true;
            }
            else
            {
                GV_BankTransactionsCredit.DataSource = null;
                GV_BankTransactionsCredit.DataBind();
                ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Credit Details Not Avaialable');", true);
                lbtn_Export.Visible = false;
            }



            if (DtBankTransactionsDebit.Rows.Count > 0)
            {
                GV_BankTransactionsDebit.DataSource = DtBankTransactionsDebit;
                GV_BankTransactionsDebit.DataBind();
            }
            else
            {
                GV_BankTransactionsDebit.DataSource = null;
                GV_BankTransactionsDebit.DataBind();
                ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Credit Details Not Avaialable');", true);
            }



            if (DtBankTransactionsCredit.Rows.Count == 0 && DtBankTransactionsDebit.Rows.Count == 0)
            {
                lbtn_Export.Visible = false;
            }
            else
            {
                lbtn_Export.Visible = true;
            }

            #endregion End Code For Calling Stored Procedue and Data To Gridview  As on [16-11-2013]

        }

        protected void lbtn_Export_Click(object sender, EventArgs e)
        {
            gve.Export("Expenses.xls", this.GV_BankTransactionsCredit);
        }
    }
}