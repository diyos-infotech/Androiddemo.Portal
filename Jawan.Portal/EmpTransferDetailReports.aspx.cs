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
using System.Globalization;
using KLTS.Data;
using Jawan.Portal.DAL;
namespace Jawan.Portal
{
    public partial class EmpTransferDetailReports : System.Web.UI.Page
    {
        string EmpIDPrefix = "";
        string BranchID = "";
        string CmpIDPrefix = "";
        string Elength = "";
        string Clength = "";
        AppConfiguration config = new AppConfiguration();
        GridViewExportUtil gve = new GridViewExportUtil();

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
                        LoadEmpIds();
                        LoadNames();
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
                case 1:
                    break;
                case 2:
                    EmployeeReportLink.Visible = true;
                    ClientsReportLink.Visible = false;
                    InventoryReportLink.Visible = false;

                    ClientsLink.Visible = true;
                    CompanyInfoLink.Visible = false;
                    InventoryLink.Visible = false;
                    ReportsLink.Visible = true;
                    SettingsLink.Visible = true;
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

        protected void GetWebConfigdata()
        {
            EmpIDPrefix = Session["EmpIDPrefix"].ToString();
            CmpIDPrefix = Session["CmpIDPrefix"].ToString();
            BranchID = Session["BranchID"].ToString();
        }


        protected void LoadEmpIds()
        {
            DataTable DtEmpIds = GlobalData.Instance.LoadEmpIds(EmpIDPrefix,BranchID);
            if (DtEmpIds.Rows.Count > 0)
            {
                ddlempid.DataValueField = "empid";
                ddlempid.DataTextField = "empid";
                ddlempid.DataSource = DtEmpIds;
                ddlempid.DataBind();
            }
            ddlempid.Items.Insert(0, "-Select-");
        }


        protected void LoadNames()
        {
            DataTable DtEmpNames = GlobalData.Instance.LoadEmpNames(EmpIDPrefix,BranchID);
            if (DtEmpNames.Rows.Count > 0)
            {
                ddlempname.DataValueField = "empid";
                ddlempname.DataTextField = "FullName";
                ddlempname.DataSource = DtEmpNames;
                ddlempname.DataBind();
            }
            ddlempname.Items.Insert(0, "-Select-");
        }


        protected void ddlempid_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearData();

            if (ddlempid.SelectedIndex > 0)
            {
                ddlempname.SelectedValue = ddlempid.SelectedValue;
            }
            else
            {
                txtStrtDate.Text = "";
                txtEndDate.Text = "";
                ddlempname.SelectedIndex = 0;
            }

        }

        protected void ddlempname_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearData();
            if (ddlempname.SelectedIndex > 0)
            {
                ddlempid.SelectedValue = ddlempname.SelectedValue;
            }
            else
            {
                txtStrtDate.Text = "";
                txtEndDate.Text = "";
                ddlempid.SelectedIndex = 0;
            }
        }

        protected void ClearData()
        {
            LblResult.Text = "";
            GVListEmployees.DataSource = null;
            GVListEmployees.DataBind();
        }

        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            LblResult.Visible = true;
            ClearData();

            if (ddlempid.SelectedIndex == 0)
            {
                LblResult.Text = "Please Select Employee Id/Employee Name";
                return;
            }
            string startdate = string.Empty;
            string enddate = string.Empty;


            if (txtStrtDate.Text.Trim().Length == 0)
            {
                LblResult.Text = "Please fill start date ";
                return;
            }
            if (txtEndDate.Text.Trim().Length == 0)
            {
                LblResult.Text = "Please fill end date";
                return;
            }

            if (txtStrtDate.Text.Trim().Length > 0)
            {
                startdate = DateTime.Parse(txtStrtDate.Text.Trim(), CultureInfo.GetCultureInfo("en-gb")).ToString();
            }
            if (txtEndDate.Text.Trim().Length > 0)
            {
                enddate = DateTime.Parse(txtEndDate.Text.Trim(), CultureInfo.GetCultureInfo("en-gb")).ToString();
            }


            string Sqlqry = "Select * from EmppostingOrder Where Empid='" + ddlempid.SelectedValue + "' and transfertype=1";
            DataTable dt = config.ExecuteAdaptorAsyncWithQueryParams(Sqlqry).Result;
            if (dt.Rows.Count > 0)
            {
                GVListEmployees.DataSource = dt;
                GVListEmployees.DataBind();

                for (int i = 0; i < GVListEmployees.Rows.Count; i++)
                {
                    string orderdt = GVListEmployees.Rows[i].Cells[3].Text;
                    string joiningdt = GVListEmployees.Rows[i].Cells[4].Text;
                    string relievedt = GVListEmployees.Rows[i].Cells[5].Text;

                    if (orderdt == "01/01/1990" || orderdt == "01/01/1900")
                    {
                        GVListEmployees.Rows[i].Cells[3].Text = "";
                    }
                    if (joiningdt == "01/01/1990" || joiningdt == "01/01/1900")
                    {
                        GVListEmployees.Rows[i].Cells[4].Text = "";
                    }

                    if (relievedt == "01/01/1990" || relievedt == "01/01/1900")
                    {
                        GVListEmployees.Rows[i].Cells[5].Text = "";
                    }
                }

            }
            else
            {
                LblResult.Text = "No record found";
            }


        }
    }
}