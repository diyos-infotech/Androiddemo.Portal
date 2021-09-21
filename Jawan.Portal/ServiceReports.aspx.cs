using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using KLTS.Data;
using System.Collections;
using System.Globalization;
using Jawan.Portal.DAL;
namespace Jawan.Portal
{
    public partial class ServiceReports : System.Web.UI.Page
    {
        AppConfiguration config = new AppConfiguration();
        GridViewExportUtil gve = new GridViewExportUtil();
        string CmpIDPrefix = "";
        string BranchID = "";

        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {

                if (!IsPostBack)
                {
                    GetWebConfigdata();
                    if (Session["UserId"] != null && Session["AccessLevel"] != null)
                    {
                        PreviligeUsers(Convert.ToInt32(Session["AccessLevel"]));
                        lblDisplayUser.Text = Session["UserId"].ToString();
                        lblcname.Text = SqlHelper.Instance.GetCompanyname();
                    }
                    else
                    {
                        Response.Redirect("login.aspx");
                    }


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

        protected void GetWebConfigdata()
        {
            CmpIDPrefix = Session["CmpIDPrefix"].ToString();
            BranchID = Session["BranchID"].ToString();
        }

        protected void ddlcname_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            ClearData();
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
            ClearData();
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


        protected void PreviligeUsers(int previligerid)
        {
            switch (previligerid)
            {
                case 1:
                    break;
                case 2:

                    break;

                case 3:
                    EmployeesLink.Visible = true;
                    ClientsReportsLink.Visible = true;
                    InventoryReportsLink.Visible = true;
                    EmployeesLink.Visible = true;
                    ClientsLink.Visible = true;
                    CompanyInfoLink.Visible = false;
                    InventoryLink.Visible = false;
                    ReportsLink.Visible = true;
                    SettingsLink.Visible = true;

                    break;

                case 4:
                    ActiveEmployeesLink.Visible = true;
                    ClientsReportsLink.Visible = true;
                    InventoryReportsLink.Visible = true;

                    EmployeesLink.Visible = true;
                    ClientsLink.Visible = true;
                    CompanyInfoLink.Visible = true;
                    InventoryLink.Visible = true;
                    ReportsLink.Visible = true;
                    SettingsLink.Visible = true;

                    break;
                case 5:
                    ActiveEmployeesLink.Visible = true;
                    ClientsReportsLink.Visible = true;
                    InventoryReportsLink.Visible = true;

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
                    EmployeesLink.Visible = false;
                    ClientsReportsLink.Visible = false;
                    ExpensesReportsLink.Visible = false;

                    break;
                default:
                    break;


            }
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

            if (txtMonth.Text.Trim().Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Please Select The Month');", true);
                return;
            }

            #endregion End  Code  For Validation as on [16-11-2013]

            if (txtMonth.Text.Trim().Length > 0)
            {
                date = DateTime.Parse(txtMonth.Text.Trim(), CultureInfo.GetCultureInfo("en-gb")).ToString();
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
            // monthsindex = ddlmonth.SelectedIndex.ToString();
            clientidprefix = CmpIDPrefix;


            SPName = "ReportForAllServiceTax";
            Clientid = ddlClientId.SelectedValue;
            HtListOfEmployees.Add("@clientid", Clientid);
            HtListOfEmployees.Add("@month", Month);

            HtListOfEmployees.Add("@clientindex", clientindex);
            // HtListOfEmployees.Add("@monthsindex", monthsindex);
            HtListOfEmployees.Add("@clientidprefix", clientidprefix);

            #endregion End  Code For Assign Values to Variables  as on [16-11-2013]

            #region  Begin code For Calling Stored Procedue  and Data To Gridview  As on [16-11-2013]
            DtListOfEmployees = config.ExecuteAdaptorAsyncWithParams(SPName, HtListOfEmployees).Result;
            if (DtListOfEmployees.Rows.Count > 0)
            {
                gvlistofclients.DataSource = DtListOfEmployees;
                gvlistofclients.DataBind();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('The  Details Are Not Avaialable');", true);
            }

            #endregion End Code For Calling Stored Procedue and Data To Gridview  As on [16-11-2013]
        }

        protected void ClearData()
        {
            gvlistofclients.DataSource = null;
            gvlistofclients.DataBind();
        }

        float ServiceTaxtotal = 0, grandtotal = 0, servicetax = 0, CESS = 0, shecess = 0;
        protected void gvlistofclients_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ServiceTaxtotal += float.Parse(((Label)e.Row.FindControl("lblServiceTaxtotal")).Text);
                grandtotal += float.Parse(((Label)e.Row.FindControl("lblgrandtotal")).Text);
                servicetax += float.Parse(((Label)e.Row.FindControl("lblservicetax")).Text);
                CESS += float.Parse(((Label)e.Row.FindControl("lblCESS")).Text);
                shecess += float.Parse(((Label)e.Row.FindControl("lblshecess")).Text);
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].Text = "Total";

                e.Row.Cells[5].Text = ServiceTaxtotal.ToString("N", CultureInfo.InvariantCulture);
                e.Row.Cells[6].Text = grandtotal.ToString("N", CultureInfo.InvariantCulture);
                e.Row.Cells[7].Text = servicetax.ToString("N", CultureInfo.InvariantCulture);
                e.Row.Cells[8].Text = CESS.ToString("N", CultureInfo.InvariantCulture);
                e.Row.Cells[9].Text = shecess.ToString("N", CultureInfo.InvariantCulture);
            }
        }
    }
}