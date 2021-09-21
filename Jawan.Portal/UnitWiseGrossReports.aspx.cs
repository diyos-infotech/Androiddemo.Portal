using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using KLTS.Data;
using System.Collections;
using System.Globalization;
using Jawan.Portal.DAL;
namespace Jawan.Portal
{
    public partial class UnitWiseGrossReports : System.Web.UI.Page
    {//DataTable dt;
        AppConfiguration config = new AppConfiguration();
        GridViewExportUtil gve = new GridViewExportUtil();
        string EmpIDPrefix = "";
        string CmpIDPrefix = "";
        string BranchID = "";
        string Elength = "";
        string Clength = "";

        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {

                GetWebConfigdata();
                if (!IsPostBack)
                {
                    if (Session["UserId"] != null && Session["AccessLevel"] != null)
                    {
                        // lblDisplayUser.Text = Session["UserId"].ToString();
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
                    FillClientList();
                    FillClientNameList();
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

        protected void GetWebConfigdata()
        {
            CmpIDPrefix = Session["CmpIDPrefix"].ToString();
            BranchID = Session["BranchID"].ToString();
        }


        protected void ddlcname_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            //ClearData();
            if (ddlcname.SelectedIndex > 0)
            {
                ddlClientId.SelectedValue = ddlcname.SelectedValue;
            }
            if (ddlcname.SelectedIndex == 1)
            {
                ddlClientId.SelectedIndex = 1;
            }
            if (ddlcname.SelectedIndex == 0)
            {
                ddlClientId.SelectedIndex = 0;
            }

        }

        protected void ddlClientId_SelectedIndexChanged(object sender, EventArgs e)
        {
            // ClearData();
            if (ddlClientId.SelectedIndex > 1)
            {
                ddlcname.SelectedValue = ddlClientId.SelectedValue;
            }
            if (ddlClientId.SelectedIndex == 1)
            {
                ddlcname.SelectedIndex = 1;
            }

            if (ddlClientId.SelectedIndex == 0)
            {
                ddlcname.SelectedIndex = 0;
            }
        }

        protected void FillClientList()
        {
            DataTable dt = GlobalData.Instance.LoadCIds(CmpIDPrefix,BranchID);
            if (dt.Rows.Count > 0)
            {
                ddlClientId.DataValueField = "clientid";
                ddlClientId.DataTextField = "clientid";
                ddlClientId.DataSource = dt;
                ddlClientId.DataBind();
            }
            ddlClientId.Items.Insert(0, "--Select--");
            ddlClientId.Items.Insert(1, "ALL");

        }

        protected void FillClientNameList()
        {

            DataTable dt = GlobalData.Instance.LoadCNames(CmpIDPrefix,BranchID);
            if (dt.Rows.Count > 0)
            {
                ddlcname.DataValueField = "clientid";
                ddlcname.DataTextField = "Clientname";
                ddlcname.DataSource = dt;
                ddlcname.DataBind();
            }
            ddlcname.Items.Insert(0, "--Select--");
            ddlcname.Items.Insert(1, "ALL");

        }


        protected void btnsearch_Click(object sender, EventArgs e)
        {

            ClearData();

            string date = string.Empty;

            #region Begin Code  For Validation as on [16-11-2013]

            if (ddlClientId.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Please Select the client Id /ALL');", true);
                return;
            }

            if (txtmonth.Text.Trim().Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Please Select The Month');", true);
                return;
            }

            #endregion End  Code  For Validation as on [16-11-2013]

            if (txtmonth.Text.Trim().Length > 0)
            {
                date = DateTime.Parse(txtmonth.Text.Trim(), CultureInfo.GetCultureInfo("en-gb")).ToString();
            }

            #region  Begin Code For Variable Declaration   as on [16-11-2013]
            var SPName = "";
            var Clientid = "";
            var Month = "";
            var Year = "";
            var clientindex = "";
            var monthsindex = "";
            var clientidprefix = "";

            DataTable DtListOfEmployees = null;
            Hashtable HtListOfEmployees = new Hashtable();

            #endregion End  Code For Variable Declaration  as on [16-11-2013]


            #region  Begin Code For Assign Values to Variables   as on [16-11-2013]

            Month = DateTime.Parse(date).Month.ToString();
            Year = DateTime.Parse(date).Year.ToString();

            Month = Month + Year.Substring(2, 2);

            clientindex = ddlClientId.SelectedIndex.ToString();
            monthsindex = ddlmonth.SelectedIndex.ToString();
            clientidprefix = CmpIDPrefix;


            SPName = "RepotForPFDetailsofclients";
            Clientid = ddlClientId.SelectedValue;
            HtListOfEmployees.Add("@clientid", Clientid);
            HtListOfEmployees.Add("@month", Month);

            HtListOfEmployees.Add("@clientindex", clientindex);
            HtListOfEmployees.Add("@monthsindex", monthsindex);
            HtListOfEmployees.Add("@clientidprefix", clientidprefix);

            #endregion End  Code For Assign Values to Variables  as on [16-11-2013]

            #region  Begin code For Calling Stored Procedue  and Data To Gridview  As on [16-11-2013]
            DtListOfEmployees =config.ExecuteAdaptorAsyncWithParams(SPName, HtListOfEmployees).Result;
            if (DtListOfEmployees.Rows.Count > 0)
            {
                GVListEmployees.DataSource = DtListOfEmployees;
                GVListEmployees.DataBind();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('The  Details Are Not Avaialable');", true);
            }

            #endregion End Code For Calling Stored Procedue and Data To Gridview  As on [16-11-2013]
        }


        protected void BindData(string Sqlqry)
        {
            DataTable dt = config.ExecuteAdaptorAsyncWithQueryParams(Sqlqry).Result;
            if (dt.Rows.Count > 0)
            {
                lbltamttext.Visible = true;
                lbtn_Export.Visible = true;
                GVListEmployees.DataSource = dt;
                GVListEmployees.DataBind();
                // TotalAmount();
            }
            else
            {

                return;
            }
        }



        protected void ClearData()
        {
            GVListEmployees.DataSource = null;
            GVListEmployees.DataBind();

        }

        protected void lbtn_Export_Click(object sender, EventArgs e)
        {
            gve.Export("AllUnitsPFReport.xls", this.GVListEmployees);
        }

        float totalPFs = 0;
        float totalPFWAGES = 0;
        float totalPF = 0;
        float totalPFEmpr = 0;
        protected void GVListEmployees_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                float PFWAGES = float.Parse(((Label)e.Row.FindControl("lblPFWAGES")).Text);
                totalPFWAGES += PFWAGES;
                float PF = float.Parse(((Label)e.Row.FindControl("lblPF")).Text);
                totalPF += PF;
                float PFEmpr = float.Parse(((Label)e.Row.FindControl("lblPFEmpr")).Text);
                totalPFEmpr += PFEmpr;
                float PFs = float.Parse(((Label)e.Row.FindControl("lblTotalPF")).Text);
                totalPFs += PFs;
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                ((Label)e.Row.FindControl("lblTotalPFWAGES")).Text = totalPFWAGES.ToString();
                ((Label)e.Row.FindControl("lblTotalPF")).Text = totalPF.ToString();
                ((Label)e.Row.FindControl("lblTotalPFEmpr")).Text = totalPFEmpr.ToString();
                ((Label)e.Row.FindControl("lblTotalTotalPF")).Text = totalPFs.ToString();
            }
        }
    }
}