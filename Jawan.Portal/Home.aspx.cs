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
    public partial class Home : System.Web.UI.Page
    {
        AppConfiguration config = new AppConfiguration();
        GridViewExportUtil gve = new GridViewExportUtil();
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {

                lblerror.Text = "";
                if (!IsPostBack)
                {


                    Session["EmpIDPrefix"] = string.Empty;
                    Session["CmpIDPrefix"] = string.Empty;
                    Session["BillnoWithoutST"] = string.Empty;
                    Session["BillnoWithST"] = string.Empty;
                    Session["BillprefixWithST"] = string.Empty;
                    Session["BillprefixWithoutST"] = string.Empty;
                    Session["InvPrefix"] = string.Empty;
                    Session["POPrefix"] = string.Empty;
                    Session["GRVPrefix"] = string.Empty;
                    Session["DCPrefix"] = string.Empty;
                    Session["BranchID"] = string.Empty;

                    LoadAllCompanies();

                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/login.aspx");

            }

        }
        protected void btn_Click(object sender, EventArgs e)
        {



            #region  Prefix values of the Client,Employee
            if (ddlbranchnames.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showlalert", "alert('Please Select The Branch');", true);
                return;
            }
            else
            {
                string SqlQryBranchPrefix = "Select * from Branchdetails  Where branchid='" + ddlbranchnames.SelectedValue + "'";
                DataTable DtBranchPrefix = config.ExecuteAdaptorAsyncWithQueryParams(SqlQryBranchPrefix).Result;
                if (DtBranchPrefix.Rows.Count > 0)
                {
                    Session["EmpIDPrefix"] = DtBranchPrefix.Rows[0]["EmpPrefix"].ToString();
                    Session["CmpIDPrefix"] = DtBranchPrefix.Rows[0]["ClientIDPrefix"].ToString();
                    Session["BillnoWithST"] = DtBranchPrefix.Rows[0]["BillnoWithServicetax"].ToString();
                    Session["BillnoWithoutST"] = DtBranchPrefix.Rows[0]["BillNoWithoutServiceTax"].ToString();
                    Session["BillprefixWithST"] = DtBranchPrefix.Rows[0]["BillprefixWithST"].ToString();
                    Session["BillprefixWithoutST"] = DtBranchPrefix.Rows[0]["BillprefixWithoutST"].ToString();
                    Session["InvPrefix"] = DtBranchPrefix.Rows[0]["InvPrefix"].ToString();
                    Session["POPrefix"] = DtBranchPrefix.Rows[0]["POPrefix"].ToString();
                    Session["GRVPrefix"] = DtBranchPrefix.Rows[0]["GRVPrefix"].ToString();
                    Session["DCPrefix"] = DtBranchPrefix.Rows[0]["DCPrefix"].ToString();
                    Session["BranchID"] = DtBranchPrefix.Rows[0]["BranchID"].ToString();


                }
            }
            #endregion



            switch (int.Parse(Session["AccessLevel"].ToString()))
            {
                case 1: //Admin
                    Session["homepage"] = "Reminders.aspx";
                    Response.Redirect("Reminders.aspx");
                    // Response.Redirect("Home.aspx");

                    //AppendLog("Redirecting to Employees");
                    break;
                case 2: //RO
                    Session["homepage"] = "TemproryEmployeeTransferList.aspx";
                    Response.Redirect("TemproryEmployeeTransferList.aspx");
                    break;

                case 3: //Accounts
                    Session["homepage"] = "EmployeeAttendance.aspx";
                    Response.Redirect("EmployeeAttendance.aspx");
                    break;
                case 4: //AdminDept
                    Session["homepage"] = "PostingOrderList.aspx";
                    Response.Redirect("PostingOrderList.aspx");
                    break;
                case 5: //OPM
                    Session["homepage"] = "EmployeeAttendance.aspx";
                    Response.Redirect("EmployeeAttendance.aspx");
                    break;
                case 6: //InventoryManagers
                    Session["homepage"] = "MaterialRequisitForm.aspx";
                    Response.Redirect("MaterialRequisitForm.aspx");
                    break;
                default:
                    break;
            }

        }

        protected void LoadAllCompanies()
        {

            string Sqlqry = "select CompanyName,Branchid from Branchdetails Order By Branchname";
            DataTable dtbranchnames = config.ExecuteAdaptorAsyncWithQueryParams(Sqlqry).Result;
            if (dtbranchnames.Rows.Count > 0)
            {
                ddlbranchnames.DataTextField = "CompanyName";
                ddlbranchnames.DataValueField = "Branchid";
                ddlbranchnames.DataSource = dtbranchnames;
                ddlbranchnames.DataBind();
            }

            ddlbranchnames.Items.Insert(0, "--Select--");

        }
    }
}