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
    public partial class MaterialSentReports : System.Web.UI.Page
    {
        AppConfiguration config = new AppConfiguration();
        GridViewExportUtil gve = new GridViewExportUtil();
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
                //DataBinder();
                ddlcname.Items.Add("--Select--");
                LoadClientNames();
                LoadClientList();
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

        protected void btn_SubmitClick(object sender, EventArgs e)
        {
            LblResult.Text = "";
            LblResult.Visible = true;
            gvShowStock.DataSource = null;
            gvShowStock.DataBind();
            if (ddlClientId.SelectedIndex > 0)
            {
                string strFromDate = txtFromDate.Text.Trim();
                string strToDate = txtToDate.Text.Trim();
                string clientID = ddlClientId.SelectedValue;

                DataTable data = new DataTable();
                if (strFromDate.Length > 0 && strToDate.Length > 0)
                {
                    DateTime fDate = Convert.ToDateTime(strFromDate);
                    DateTime tDate = Convert.ToDateTime(strToDate);
                    if (fDate > tDate)
                    {
                        LblResult.Text = "From Date should not greater than To Date";
                        return;
                    }

                    string strQry = "Select DISTINCT MRF.ItemId, sil.ItemName, Sum(MRF.ApprovedQty) as Quantity,sil.Price,(Sum(MRF.ApprovedQty)*sil.Price) as cost from MRF INNER JOIN StockItemList as sil ON MRF.ItemId=sil.ItemId where MRF.status=1 AND MRF.Date>='" + fDate +
                        "' AND MRF.Date<='" + tDate + "' AND MRF.ClientId='" + clientID + "' Group by MRF.ItemId,sil.ItemName,sil.Price";
                    data = config.ExecuteAdaptorAsyncWithQueryParams(strQry).Result;
                }
                else
                {
                    string strQry = "Select DISTINCT MRF.ItemId, sil.ItemName, Sum(MRF.ApprovedQty) as Quantity,sil.Price,(Sum(MRF.ApprovedQty)*sil.Price) as cost from MRF INNER JOIN StockItemList as sil ON MRF.ItemId=sil.ItemId where MRF.status=1 AND MRF.ClientId='"
                        + clientID + "' Group by MRF.ItemId,sil.ItemName,sil.Price";
                    data = config.ExecuteAdaptorAsyncWithQueryParams(strQry).Result;
                }
                if (data.Rows.Count > 0)
                {
                    gvShowStock.DataSource = data;
                    gvShowStock.DataBind();
                }
                else
                {
                    LblResult.Text = "There is no Material sent details available for this Client";
                }
            }
            else
            {
                LblResult.Text = "Plese Select Client ";
                //DataBinder();
            }
        }

        protected void GridData()
        {
            LblResult.Text = "";
            gvShowStock.DataSource = null;
            gvShowStock.DataBind();
        }

        protected void ddlcname_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            LblResult.Text = "";
            GridData();
            if (ddlcname.SelectedIndex > 0)
            {
                ddlClientId.SelectedValue = ddlcname.SelectedValue;
            }
            else
                ddlClientId.SelectedIndex = 0;
        }

        protected void ddlClientId_SelectedIndexChanged(object sender, EventArgs e)
        {
            LblResult.Text = "";
            GridData();
            if (ddlClientId.SelectedIndex > 0)
            {
                ddlcname.SelectedValue = ddlClientId.SelectedValue;

            }
            else
                ddlcname.SelectedIndex = 0;
        }
    }
}