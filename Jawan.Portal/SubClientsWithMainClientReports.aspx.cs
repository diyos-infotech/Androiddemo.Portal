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
using Jawan.Portal.DAL;
namespace Jawan.Portal
{
    public partial class SubClientsWithMainClientReports : System.Web.UI.Page
    {
        AppConfiguration config = new AppConfiguration();
        GridViewExportUtil gve = new GridViewExportUtil();
        string EmpIDPrefix = "";
        string CmpIDPrefix = "";
        string Elength = "";
        string Clength = "";
        string Branch = "";
        string BranchId = "";
        protected void Page_Load(object sender, EventArgs e)
        {

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

                    FillClientList();
                    FillClientNameList();
                }
            }
            catch (Exception EX)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Show alert", "alert('Your Session Expired');", true);
                Response.Redirect("~/Login.aspx");
            }


        }

        protected void GetWebConfigdata()
        {
            CmpIDPrefix = Session["CmpIDPrefix"].ToString();
            BranchId = Session["BranchID"].ToString();
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
                    EmployeesLink.Visible = true;
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
                    EmployeesLink.Visible = true;
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

        protected void btn_Submit_Click(object sender, EventArgs e)
        {

            #region Begin Code  For Validation as on [16-11-2013]
            if (ddlclient.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Please Select The client');", true);
                return;
            }
            #endregion End  Code  For Validation as on [16-11-2013]

            #region  Begin Code For Variable Declaration   as on [16-11-2013]
            var SPName = "";
            var Condition = 0;

            DataTable DtListOfClients = null;
            Hashtable HtListOfClients = new Hashtable();

            #endregion End  Code For Variable Declaration  as on [16-11-2013]


            #region  Begin Code For Assign Values to Variables   as on [16-11-2013]
            SPName = "ReportForLoadSubclients";

            HtListOfClients.Add("@clientid", ddlclient.SelectedValue);
            HtListOfClients.Add("@clientidprefix", CmpIDPrefix);


            #endregion End  Code For Assign Values to Variables  as on [16-11-2013]

            #region  Begin code For Calling Stored Procedue  and Data To Gridview  As on [16-11-2013]
            DtListOfClients = config.ExecuteAdaptorAsyncWithParams(SPName, HtListOfClients).Result;
            if (DtListOfClients.Rows.Count > 0)
            {
                GVListOfClients.DataSource = DtListOfClients;
                GVListOfClients.DataBind();
            }
            else
            {

                GVListOfClients.DataSource = null;
                GVListOfClients.DataBind();

                ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('The Client Details Are Not Avaialable');", true);
            }

            #endregion End Code For Calling Stored Procedue and Data To Gridview  As on [16-11-2013]
        }

        protected void ddlclient_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlclient.SelectedIndex > 0)
            {
                ddlcname.SelectedValue = ddlclient.SelectedValue;
            }
            else
            {
                ddlcname.SelectedIndex = 0;
            }

        }

        protected void ddlcname_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlcname.SelectedIndex > 0)
            {
                ddlclient.SelectedValue = ddlcname.SelectedValue;

            }
            else
            {
                ddlclient.SelectedIndex = 0;
            }
        }


        protected void FillClientList()
        {
            DataTable dt = GlobalData.Instance.LoadMainunitCIds(CmpIDPrefix, BranchId);
            if (dt.Rows.Count > 0)
            {
                ddlclient.DataValueField = "clientid";
                ddlclient.DataTextField = "clientid";
                ddlclient.DataSource = dt;
                ddlclient.DataBind();
            }
            ddlclient.Items.Insert(0, "--Select--");

        }

        protected void FillClientNameList()
        {
            DataTable dt = GlobalData.Instance.LoadMainunitCNames(CmpIDPrefix);
            if (dt.Rows.Count > 0)
            {
                ddlcname.DataValueField = "clientid";
                ddlcname.DataTextField = "Clientname";
                ddlcname.DataSource = dt;
                ddlcname.DataBind();
            }
            ddlcname.Items.Insert(0, "--Select--");

        }
     
    }
}