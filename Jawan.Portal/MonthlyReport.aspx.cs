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
using System.Data;
using KLTS.Data;
using System.Globalization;
using OfficeOpenXml;
using Jawan.Portal.DAL;

namespace Jawan.Portal
{
    public partial class MonthlyReport : System.Web.UI.Page
    {
        DataTable dt;
        string EmpIDPrefix = "";
        string CmpIDPrefix = "";
        string Elength = "";
        string Clength = "";
        AppConfiguration config = new AppConfiguration();
        GridViewExportUtil grvutil = new GridViewExportUtil();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                GetWebConfigdata();
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
                                ExpensesReportsLink.Visible = false;
                                break;


                            default:
                                break;
                        }
                        FinancialYears();
                    }
                    else
                    {
                        Response.Redirect("login.aspx");
                    }



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
                case 1: //Admin

                    break;
                case 2://Acconunts
                    CompanyInfoLink.Visible = false;
                    InventoryLink.Visible = false;
                    SettingsLink.Visible = false;

                    break;

                case 3://HRD
                    CompanyInfoLink.Visible = false;
                    InventoryLink.Visible = false;
                    SettingsLink.Visible = false;

                    break;

                case 4://Stores
                    CompanyInfoLink.Visible = false;
                    SettingsLink.Visible = false;
                    ClientsLink.Visible = false;
                    ClientsReportLink.Visible = false;
                    InventoryLink.Visible = true;

                    break;
                case 5://enrolment
                    CompanyInfoLink.Visible = false;
                    InventoryLink.Visible = false;
                    SettingsLink.Visible = false;
                    ClientsLink.Visible = false;
                    ClientsLink.Visible = false;

                    break;
                case 6://Rec
                    CompanyInfoLink.Visible = false;
                    InventoryLink.Visible = false;
                    SettingsLink.Visible = false;


                    break;
                case 7:

                    break;
                default:
                    break;
            }
        }

        protected void GetWebConfigdata()
        {
            EmpIDPrefix = Session["EmpIDPrefix"].ToString();
            CmpIDPrefix = Session["CmpIDPrefix"].ToString();
        }

        protected void ClearData()
        {

            GVListEmployees.DataSource = null;
            GVListEmployees.DataBind();

        }

        protected void GVListEmployees_RowDataBound(object sender, GridViewRowEventArgs e)
        {


            if (ddlFinancialYears.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Please Select Financial Year');", true);
                return;
            }

            string FinancialYears = "";
            string CurrentYear = "";
            string NextYear = "";

            FinancialYears = ddlFinancialYears.SelectedValue;
            CurrentYear = FinancialYears.Substring(0, 4);
            NextYear = FinancialYears.Substring(7, 4);

            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[2].Text = "Apr - " + CurrentYear;
                e.Row.Cells[3].Text = "May - " + CurrentYear;
                e.Row.Cells[4].Text = "Jun - " + CurrentYear;
                e.Row.Cells[5].Text = "Jul - " + CurrentYear;
                e.Row.Cells[6].Text = "Aug - " + CurrentYear;
                e.Row.Cells[7].Text = "Sept - " + CurrentYear;
                e.Row.Cells[8].Text = "Oct - " + CurrentYear;
                e.Row.Cells[9].Text = "Nov - " + CurrentYear;
                e.Row.Cells[10].Text = "Dec - " + CurrentYear;
                e.Row.Cells[11].Text = "Jan - " + NextYear;
                e.Row.Cells[12].Text = "Feb - " + NextYear;
                e.Row.Cells[13].Text = "Mar - " + NextYear;


            }


        }

        protected void lbtn_Export_Click(object sender, EventArgs e)
        {
            grvutil.Export("MonthlyReport.xls", this.GVListEmployees);
        }

        protected void btn_Submit_Click(object sender, EventArgs e)
        {

            if (ddlFinancialYears.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Please Select Financial Year');", true);
                return;
            }

            string FinancialYears = "";
            string CurrentYear = "";
            string NextYear = "";

            FinancialYears = ddlFinancialYears.SelectedValue;
            CurrentYear = FinancialYears.Substring(0, 4);
            NextYear = FinancialYears.Substring(7, 4);

            int option = 0;

            var SPName = "";
            Hashtable HTPaysheet = new Hashtable();
            SPName = "MonthlyReport";

            HTPaysheet.Add("@year", CurrentYear);
            HTPaysheet.Add("@NextYear", NextYear);
            HTPaysheet.Add("@year1", CurrentYear.Substring(2,2));
            HTPaysheet.Add("@NextYear1", NextYear.Substring(2, 2));

            DataTable dt = config.ExecuteAdaptorAsyncWithParams(SPName, HTPaysheet).Result;
            if (dt.Rows.Count > 0)
            {
                GVListEmployees.DataSource = dt;
                GVListEmployees.DataBind();
                lbtn_Export.Visible = true;
            }
            else
            {
                GVListEmployees.DataSource = null;
                GVListEmployees.DataBind();
                lbtn_Export.Visible = false;
            }
        }

        public void FinancialYears()
        {
            DataTable DtFinancialYears = GlobalData.Instance.LoadFinancialYears();

            if (DtFinancialYears.Rows.Count > 0)
            {
                ddlFinancialYears.DataValueField = "FinancialYears";
                ddlFinancialYears.DataTextField = "FinancialYears";
                ddlFinancialYears.DataSource = DtFinancialYears;
                ddlFinancialYears.DataBind();
            }
            ddlFinancialYears.Items.Insert(0, "--Select--");
        }
    }
}