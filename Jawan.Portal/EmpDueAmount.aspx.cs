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
    public partial class EmpDueAmount : System.Web.UI.Page
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

            if (ddlempid.SelectedIndex > 0)
            {
                ddlempname.SelectedValue = ddlempid.SelectedValue;
                LoandLoanDetails();
            }
            else
            {
                ddlempname.SelectedIndex = 0;
            }

        }

        protected void ddlempname_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddlempname.SelectedIndex > 0)
            {
                ddlempid.SelectedValue = ddlempname.SelectedValue;
                LoandLoanDetails();
            }
            else
            {
                ddlempid.SelectedIndex = 0;
            }
        }



        protected void LoandLoanDetails()
        {


            string Sqlqry = " SELECT  LoanType, LoanAmount, LoanNo " +
                          "  FROM EmpLoanMaster  where  " +
                          "  EmpLoanMaster.EmpId ='" + ddlempid.SelectedValue + "'  and loanamount>0  Order by LoanNo Desc";

            DataTable dt = config.ExecuteAdaptorAsyncWithQueryParams(Sqlqry).Result;
            if (dt.Rows.Count > 0)
            {
                GVListEmployees.DataSource = dt;
                GVListEmployees.DataBind();

                if (GVListEmployees.Rows.Count > 0)
                {
                    float loanrecamount = 0;
                    float Dueamount = 0;
                    float recamt = 0;
                    foreach (GridViewRow gvrow in GVListEmployees.Rows)
                    {
                        string loanno = gvrow.Cells[0].Text;
                        string loanamount = gvrow.Cells[2].Text;
                        //lblrecamt,lblDueamt
                        Label lblrecamt = (Label)gvrow.FindControl("lblrecamt");
                        Label lblDueamt = (Label)gvrow.FindControl("lblDueamt");
                        string SqlqryRecamount = "select Sum(isnull(RecAmt,0)) as recamt from emploandetails Where loanno='" + loanno + "'";
                        DataTable DtRecamount = config.ExecuteAdaptorAsyncWithQueryParams(SqlqryRecamount).Result;
                        if (DtRecamount.Rows.Count > 0)
                        {
                            lblrecamt.Text = DtRecamount.Rows[0][0].ToString();

                            if (lblrecamt.Text.Trim().Length != 0)
                            {
                                recamt = float.Parse(lblrecamt.Text);
                            }
                            else
                            {
                                lblrecamt.Text = "0";
                            }
                            lblDueamt.Text = ((float.Parse(loanamount)) - recamt).ToString();
                        }
                        else
                        {
                            lblrecamt.Text = "0";
                            lblDueamt.Text = "0";
                        }

                    }
                }
            }
            else
            {
                GVListEmployees.DataSource = null;
                GVListEmployees.DataBind();
            }

        }
    }
}