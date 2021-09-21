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
using Jawan.Portal.DAL;
namespace Jawan.Portal
{
    public partial class PFAvailableandnonavailableReports : System.Web.UI.Page
    {
        AppConfiguration config = new AppConfiguration();
        GridViewExportUtil gve = new GridViewExportUtil();
        string CmpIDPrefix = "";
        string EmpIDPrefix = "";
        string Elength = "";
        string Clength = "";
        protected void Page_Load(object sender, EventArgs e)
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

                    EmployeeReportLink.Visible = false;
                    ClientsReportLink.Visible = false;
                    ExpensesReportsLink.Visible = false;


                    //ActiveEmployeeReportLink.Visible = false;
                    //LoanReportsLink.Visible = false;
                    //LoanDeductionReports.Visible = false;
                    //AttReportLink.Visible = false;
                    //NoAdAtPhReportsPhReportsLink.Visible = false;
                    //NoAttendanceReportsLink.Visible = false;
                    //EmpTransferDetailReports.Visible = false;
                    //ESIDeductionReportsLink.Visible = false;
                    //PfDeductionreportsLink.Visible = false;
                    //ProfTaxDeductionReportsLink.Visible = false;

                    Server.Transfer("ListOfItemsReports.aspx");
                    break;
                default:
                    break;


            }
        }

        bool EmpStatus = false;

        protected void GetWebConfigdata()
        {
            EmpIDPrefix = GlobalData.Instance.GetEmployeeIDPrefix();
            Elength = (EmpIDPrefix.Trim().Length + 1).ToString();
            CmpIDPrefix = GlobalData.Instance.GetClientIDPrefix();
            Clength = (CmpIDPrefix.Trim().Length + 1).ToString();
        }

        protected void ddlpfesioptions_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sqlqry = string.Empty;
            if (ddlpfesioptions.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showlalert", "alert('Please  Select PF/ESI');", true);
                return;
            }

            else
            {
                if (ddlavailbleunavailbleoptions.SelectedIndex == 0)
                {
                    sqlqry = "select   E.Empid,(E.EmpFname+ E.Empmname + E.Emplname) as name, E.Empdesgn as design,EPF.EmpEpfno " +
                        " from Empdetails E  inner join  Empepfcodes EPF  on E.Empid=EPF.Empid  and   EPF.EmpEpfno<>'0' " +
                        "   Order By  empid";
                }

                else
                {
                    sqlqry = "select  E.Empid,(E.EmpFname+ E.Empmname + E.Emplname) as name, E.Empdesgn as design,EPF.EmpEpfno " +
                     " from Empdetails E  inner join  Empepfcodes EPF  on E.Empid=EPF.Empid  and  EPF.EmpEpfno='0' " +
                     " Order By  empid";
                }

                DataTable dt = config.ExecuteAdaptorAsyncWithQueryParams(sqlqry).Result;
                if (dt.Rows.Count > 0)
                {
                    GVpfesiemployees.DataSource = dt;
                    GVpfesiemployees.DataBind();
                }
                else
                {
                    GVpfesiemployees.DataSource = null;
                    GVpfesiemployees.DataBind();

                    ScriptManager.RegisterStartupScript(this, GetType(), "showlalert", "alert('No  Records Found');", true);

                }

            }

        }

        protected void lbtn_Export_Click(object sender, EventArgs e)
        {
            gve.Export("Employeeslist.xls", this.GVpfesiemployees);
        }
    }
}