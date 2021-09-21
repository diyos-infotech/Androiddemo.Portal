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
    public partial class LicenceDetailsReports : System.Web.UI.Page
    {
        AppConfiguration config = new AppConfiguration();
        GridViewExportUtil gve = new GridViewExportUtil();
        DataTable dt;
        string EmpIDPrefix = "";
        string CmpIDPrefix = "";
        string BranchID = "";

        string Elength = "";
        string Clength = "";


        protected void Page_Load(object sender, EventArgs e)
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
                DataBinder();

            }



        }

        protected void LoadClientNames()
        {
            DataTable DtClientids = GlobalData.Instance.LoadCNames(CmpIDPrefix,BranchID);
            if (DtClientids.Rows.Count > 0)
            {
                ddlcname.DataValueField = "Clientid";
                ddlcname.DataTextField = "clientname";
                ddlcname.DataSource = DtClientids;
                ddlcname.DataBind();
            }
            ddlcname.Items.Insert(0, "-Select-");
            ddlcname.Items.Insert(1, "ALL");

        }

        protected void LoadClientList()
        {
            DataTable DtClientNames = GlobalData.Instance.LoadCIds(CmpIDPrefix,BranchID);
            if (DtClientNames.Rows.Count > 0)
            {
                ddlClientId.DataValueField = "Clientid";
                ddlClientId.DataTextField = "Clientid";
                ddlClientId.DataSource = DtClientNames;
                ddlClientId.DataBind();
            }
            ddlClientId.Items.Insert(0, "-Select-");
            ddlClientId.Items.Insert(1, "ALL");
        }

        protected void GetWebConfigdata()
        {

            EmpIDPrefix = Session["EmpIDPrefix"].ToString();
            CmpIDPrefix = Session["CmpIDPrefix"].ToString();
            BranchID = Session["BranchID"].ToString();

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

        protected void DataBinder()
        {
            LblResult.Text = "";
            LblResult.Visible = true;
            string sqlQry = "Select Licenses.UnitId,LicenseStartDate,LicenseEndDate,LicenseExpired," +
                "LicenseOfficeLoc,Clients.ClientName from Clients INNER JOIN Licenses ON " +
                "Licenses.UnitId=Clients.ClientId";
            DataTable data = config.ExecuteAdaptorAsyncWithQueryParams(sqlQry).Result;

            if (data.Rows.Count > 0)
            {
                dgLicExpire.DataSource = data;
                dgLicExpire.DataBind();
            }
            else
            {
                LblResult.Text = "There is no License Expiring Details";
            }
        }

        protected void btn_SubmitClick(object sender, EventArgs e)
        {
            LblResult.Text = "";
            LblResult.Visible = true;
            dgLicExpire.DataSource = null;
            dgLicExpire.DataBind();
            if (ddlClientId.SelectedIndex > 0)
            {
                LblResult.Text = "";
                LblResult.Visible = true;
                string sqlQry = "Select Licenses.UnitId,LicenseStartDate,LicenseEndDate,LicenseExpired," +
                    "LicenseOfficeLoc,Clients.ClientName from Clients INNER JOIN Licenses ON " +
                    "Licenses.UnitId=Clients.ClientId  and Licenses.UnitId='" + ddlClientId.SelectedValue + "'";
                DataTable data = config.ExecuteAdaptorAsyncWithQueryParams(sqlQry).Result;
                if (data.Rows.Count > 0)
                {
                    dgLicExpire.DataSource = data;
                    dgLicExpire.DataBind();
                }
                else
                {
                    LblResult.Text = "There IS No  License For The Selected Client";
                }
            }
            else
            {
                //LblResult.Text = "Plese Select The Client ID";
                DataBinder();
            }

        }

        protected void GridData()
        {
            LblResult.Text = "";
            dgLicExpire.DataSource = null;
            dgLicExpire.DataBind();
        }
        protected void ddlcname_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            GridData();
            if (ddlcname.SelectedIndex > 0)
            {

            }
            else
            {
                Cleardata();
            }
        }
        protected void ddlClientId_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridData();
            if (ddlClientId.SelectedIndex > 0)
            {

            }
            else
            {
                Cleardata();
            }
        }
        protected void Cleardata()
        {
            LblResult.Text = "";
            ddlClientId.SelectedIndex = 0;
            ddlcname.SelectedIndex = 0;
            dgLicExpire.DataSource = null;
            dgLicExpire.DataBind();

        }
    }
}