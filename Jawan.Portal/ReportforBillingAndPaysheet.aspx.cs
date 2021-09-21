using System;
using System.Collections.Generic;
using System.Configuration;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KLTS.Data;
using System.Data;
using System.Data.SqlClient;
using Jawan.Portal.DAL;
namespace Jawan.Portal
{
    public partial class ReportforBillingAndPaysheet : System.Web.UI.Page
    {
        AppConfiguration config = new AppConfiguration();
        GridViewExportUtil gve = new GridViewExportUtil();
    string EmpIDPrefix = "";
    string CmpIDPrefix = "";
    string Elength = "";
    string Clength = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        //GetWebConfigdata();
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
        }
    }

    protected void GetWebConfigdata()
    {
        EmpIDPrefix = Session["EmpIDPrefix"].ToString();
        Elength = (EmpIDPrefix.Trim().Length + 1).ToString();
        CmpIDPrefix = Session["CmpIDPrefix"].ToString();
        Clength = (CmpIDPrefix.Trim().Length + 1).ToString();
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

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (txtmonth.Text.Trim().Length == 0)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Show alert", "alert('Please Select Month');", true);
            return;
        }
        
        DateTime date = Convert.ToDateTime(txtmonth.Text);
        string mon = string.Format("{0}{1:yy}", date.Month.ToString("00"), (date.Year - 2000).ToString());
        int month = Convert.ToInt32(mon);
        DateTime startDay = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        DateTime endDay = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));

        Hashtable References2 = new Hashtable();
        string SPReferencesName2 = "GetBillandPaysheet2";
        References2.Add("@month", month);
        DataTable dtbillandPaysheet2 = config.ExecuteAdaptorAsyncWithParams(SPReferencesName2, References2).Result;
        if (dtbillandPaysheet2.Rows.Count > 0)
        {
            GVListEmployees.DataSource = dtbillandPaysheet2;
            GVListEmployees.DataBind();

        }
        else 
        {
            ScriptManager.RegisterStartupScript(this,GetType(),"Showalert","alert('details are not available');",true);
            return;
        }
       
            Hashtable References = new Hashtable();
            string SPReferencesName = "GetBillandPaysheet";
            References.Add("@month", month);
            DataTable dtbillandPaysheet = config.ExecuteAdaptorAsyncWithParams(SPReferencesName, References).Result;
            txtBYes.Text = dtbillandPaysheet.Rows[0]["invoiceOne"].ToString();
            txtBNo.Text = dtbillandPaysheet.Rows[0]["invoiceZero"].ToString();
            txtPYes.Text = dtbillandPaysheet.Rows[0]["PaysheetOne"].ToString();
            txtPNo.Text = dtbillandPaysheet.Rows[0]["PaysheetZero"].ToString();
            txtBduties.Text = dtbillandPaysheet.Rows[0]["bildts"].ToString();
            txtPduties.Text = dtbillandPaysheet.Rows[0]["paydts"].ToString();

        }

    protected void lbtn_Export_Click(object sender, EventArgs e)
    {
        //GridViewExportUtil gve=new GridViewExportUtil();
        gve.Export("AllUnitsEsiReport.xls", this.GVListEmployees);
     }
   }
}
