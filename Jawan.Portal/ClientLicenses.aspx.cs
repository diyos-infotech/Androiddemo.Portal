using System;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using KLTS.Data;
using Jawan.Portal.DAL;
namespace Jawan.Portal
{
    public partial class ClientLicenses : System.Web.UI.Page
    {
        AppConfiguration config = new AppConfiguration();
        GridViewExportUtil gve = new GridViewExportUtil();
        DataTable dt;
        string EmpIDPrefix = "";
        string CmpIDPrefix = "";
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
                        case 0:// Write Frames Invisible Links
                            break;
                        case 1://Write KLTS Invisible Links
                            ReceiptsLink.Visible = true;
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


        protected void GetWebConfigdata()
        {
            EmpIDPrefix = GlobalData.Instance.GetEmployeeIDPrefix();
            Elength = (EmpIDPrefix.Trim().Length + 1).ToString();
            CmpIDPrefix = GlobalData.Instance.GetClientIDPrefix();
            Clength = (CmpIDPrefix.Trim().Length + 1).ToString();
        }

        protected void DataBinder()
        {
            string sqlQry = "Select Licenses.UnitId,LicenseStartDate,LicenseEndDate,LicenseExpired,LicenseOfficeLoc,Clients.ClientName from Clients INNER JOIN Licenses ON Licenses.UnitId=Clients.ClientId AND LicenseEndDate>='"
                + DateTime.Now.ToString("MM/dd/yyyy") + "' AND LicenseEndDate<='" + DateTime.Now.AddMonths(1).ToString("MM/dd/yyyy") + "'";
            DataTable data = config.ExecuteAdaptorAsyncWithQueryParams(sqlQry).Result;
            dgLicExpire.DataSource = data;
            dgLicExpire.DataBind();
        }

        protected void PreviligeUsers(int previligerid)
        {

            switch (previligerid)
            {

                case 1:
                    break;
                case 2:
                    AddClientLink.Visible = true;
                    ModifyClientLink.Visible = false;
                    DeleteClientLink.Visible = false;
                    ContractLink.Visible = false;
                    ClientAttendanceLink.Visible = false;
                    Operationlink.Visible = false;
                    BillingLink.Visible = false;
                    MRFLink.Visible = false;

                    CompanyInfoLink.Visible = false;
                    InventoryLink.Visible = false;
                    ReportsLink.Visible = false;
                    SettingsLink.Visible = false;

                    break;

                case 3:

                    AddClientLink.Visible = true;
                    ModifyClientLink.Visible = true;
                    DeleteClientLink.Visible = true;
                    ContractLink.Visible = true;
                    ClientAttendanceLink.Visible = true;
                    Operationlink.Visible = true;
                    BillingLink.Visible = false;
                    MRFLink.Visible = false;


                    InventoryLink.Visible = false;
                    ReportsLink.Visible = false;
                    SettingsLink.Visible = false;
                    break;
                case 4:

                    AddClientLink.Visible = true;
                    ModifyClientLink.Visible = true;
                    DeleteClientLink.Visible = true;
                    ContractLink.Visible = true;
                    LicensesLink.Visible = true;
                    ClientAttendanceLink.Visible = false;
                    Operationlink.Visible = true;
                    BillingLink.Visible = false;
                    MRFLink.Visible = false;

                    EmployeesLink.Visible = true;
                    ClientsLink.Visible = true;
                    CompanyInfoLink.Visible = true;
                    InventoryLink.Visible = true;
                    ReportsLink.Visible = true;
                    SettingsLink.Visible = true;
                    break;
                case 5:

                    AddClientLink.Visible = false;
                    ModifyClientLink.Visible = true;
                    DeleteClientLink.Visible = false;
                    ContractLink.Visible = false;
                    ClientAttendanceLink.Visible = true;
                    Operationlink.Visible = false;
                    LicensesLink.Visible = true;
                    BillingLink.Visible = false;
                    MRFLink.Visible = true;

                    EmployeesLink.Visible = true;
                    ClientsLink.Visible = true;
                    CompanyInfoLink.Visible = false;
                    InventoryLink.Visible = false;
                    ReportsLink.Visible = true;
                    ReportsLink.Visible = true;
                    SettingsLink.Visible = true;
                    break;
                case 6:
                    AddClientLink.Visible = false;
                    ModifyClientLink.Visible = false;
                    DeleteClientLink.Visible = false;
                    ContractLink.Visible = false;
                    ClientAttendanceLink.Visible = true;
                    Operationlink.Visible = false;
                    BillingLink.Visible = false;
                    MRFLink.Visible = true;

                    CompanyInfoLink.Visible = false;
                    InventoryLink.Visible = false;
                    ReportsLink.Visible = false;
                    SettingsLink.Visible = false;

                    break;
                default:
                    break;


            }
        }

        protected void btnaddclint_Click(object sender, EventArgs e)
        {
            if (ddlClientId.SelectedIndex == 0)
            {
                lblresult.Text = "Please Select Client Id";
                return;
            }

            if (ddlClientId.SelectedIndex > 0)
            {
                string licNo = txtLicenseNo.Text.Trim();
                string licOfficeLoc = txtLicOffLoc.Text.Trim();
                string sLic = txtLicStart.Text.ToString().Trim();
                string eLic = txtLicEnd.Text.ToString().Trim();
                if (licNo.Length == 0)
                {
                    lblresult.Text = "Enter License No";
                    return;
                }
                if (sLic.Length == 0)
                {
                    lblresult.Text = "Enter License Start Date";
                    return;
                }
                if (eLic.Length == 0)
                {
                    lblresult.Text = "Enter License End Date";
                    return;
                }

                try
                {
                    string insertquery = string.Format("insert into Licenses(LicenseNo,UnitId,LicenseStartDate,LicenseEndDate,LicenseOfficeLoc) values('{0}','{1}','{2}','{3}','{4}')",
                    licNo, ddlClientId.SelectedValue, sLic, eLic, licOfficeLoc);
                    int status =config.ExecuteNonQueryWithQueryAsync(insertquery).Result;
                    if (status != 0)
                    {
                        lblresult.Visible = true;
                        lblresult.Text = "Record Inserted  Successfully";
                    }
                    else
                    {
                        lblresult.Visible = true;
                        lblresult.Text = "Record not  Inserted ";
                    }
                    DataBinder();
                }
                catch (Exception ex)
                {
                    lblresult.Visible = true;
                    lblresult.Text = "Plz Fill The Details";
                }
            }
        }

        protected void ddlClientId_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlClientId.SelectedIndex > 0)
            {
                string sqlQry = "Select ClientName from Clients where ClientId='" + ddlClientId.SelectedValue + "'";
                DataTable data = config.ExecuteAdaptorAsyncWithQueryParams(sqlQry).Result;
                if (data.Rows.Count > 0)
                {
                    ddlCname.SelectedValue = data.Rows[0]["clientname"].ToString();
                }
            }
            else
            {
                ddlCname.SelectedIndex = 0;
            }
        }


        protected void ClearAll()
        {
            txtLicenseNo.Text = "";
            txtLicOffLoc.Text = "";
            txtLicStart.Text = DateTime.Now.ToString();
            txtLicEnd.Text = DateTime.Now.ToString();

        }

        protected void btncancel_Click(object sender, EventArgs e)
        {
            ClearAll();
        }
        protected void ddlCname_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCname.SelectedIndex > 0)
            {

            }
            else
            {
                ddlClientId.SelectedIndex = 0;
            }
        }
    }
}