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
    public partial class LicenceExpireDetailsReports : System.Web.UI.Page
    {
        AppConfiguration config = new AppConfiguration();
        GridViewExportUtil gve = new GridViewExportUtil();
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
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
                DataBinder();
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
                    break;
                default:
                    break;


            }
        }

        protected void DataBinder()
        {
            LblResult.Text = "";
            LblResult.Visible = true;//SELECT * FROM (SELECT * FROM topten ORDER BY datetime DESC) tmp GROUP BY home
            //string sqlQry = "Select Licenses.UnitId,LicenseStartDate,LicenseEndDate,LicenseExpired," +
            //    "LicenseOfficeLoc,Clients.ClientName from Clients Inner JOIN  Licenses ON " +
            //    "Licenses.UnitId=Clients.ClientId and LicenseEndDate<='" + DateTime.Now.AddMonths(1).ToShortDateString() + "'";
            string sqlQry = "Select Licenses.UnitId,LicenseStartDate,LicenseEndDate,LicenseExpired," +
                "LicenseOfficeLoc,Clients.ClientName from clients Inner JOIN  Licenses ON " +
                "Licenses.UnitId=Clients.ClientId and LicenseEndDate<='" + DateTime.Now.AddMonths(1).ToShortDateString() + "'";

            DataTable data = config.ExecuteAdaptorAsyncWithQueryParams(sqlQry).Result;
            sqlQry = "Select ClientId,ClientName from Clients";
            DataTable clientTable = config.ExecuteAdaptorAsyncWithQueryParams(sqlQry).Result;
            DateTime date = new DateTime();
            for (int i = 0; i < clientTable.Rows.Count; i++)
            {
                sqlQry = "Select UnitId from Licenses where UnitId='" + clientTable.Rows[i]["ClientId"].ToString() + "'";
                DataTable noLicTable = config.ExecuteAdaptorAsyncWithQueryParams(sqlQry).Result;
                if (noLicTable.Rows.Count == 0)
                {
                    DataRow row = data.NewRow();
                    row["UnitId"] = clientTable.Rows[i]["ClientId"].ToString();
                    row["ClientName"] = clientTable.Rows[i]["ClientName"].ToString();
                    row["LicenseStartDate"] = date.Date.ToString();
                    row["LicenseEndDate"] = date.Date.ToString();
                    row["LicenseExpired"] = "True";
                    row["LicenseOfficeLoc"] = "License not taken yet";
                    data.Rows.Add(row);
                }
            }
            if (data.Rows.Count > 0)
            {

                dgLicExpire.DataSource = data;
                dgLicExpire.DataBind();
            }
            else
            {
                LblResult.Text = "There is no Licenses Expiring till next month";
            }
        }



        protected void btncancel_Click(object sender, EventArgs e)
        {
            // ClearAll();
        }
        protected void dgLicExpire_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgLicExpire.PageIndex = e.NewPageIndex;
            DataBinder();
        }
    }
}