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
using System.Data;
using Jawan.Portal.DAL;
namespace Jawan.Portal
{
    public partial class ProfitInvoiceReports : System.Web.UI.Page
    {
        AppConfiguration config = new AppConfiguration();
        GridViewExportUtil gve = new GridViewExportUtil();
        //DataTable dt;
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


                LoadClientNames();
                FillClientList();
            }
        }

        protected void GetWebConfigdata()
        {
            EmpIDPrefix = GlobalData.Instance.GetEmployeeIDPrefix();
            Elength = (EmpIDPrefix.Trim().Length + 1).ToString();
            CmpIDPrefix = GlobalData.Instance.GetClientIDPrefix();
            Clength = (CmpIDPrefix.Trim().Length + 1).ToString();
        }

        protected void Fillcname()
        {
            string SqlQryForCname = "Select Clientname from Clients where clientid='" + ddlClientId.SelectedValue + "'";
            DataTable dtCname = config.ExecuteAdaptorAsyncWithQueryParams(SqlQryForCname).Result;
            if (dtCname.Rows.Count > 0)
                ddlcname.SelectedValue = dtCname.Rows[0]["Clientname"].ToString();
        }

        protected void FillClientid()
        {
            string SqlQryForCid = "Select Clientid from Clients where clientname='" + ddlcname.SelectedValue + "'";
            DataTable dtCname = config.ExecuteAdaptorAsyncWithQueryParams(SqlQryForCid).Result;
            if (dtCname.Rows.Count > 0)
                ddlClientId.SelectedValue = dtCname.Rows[0]["Clientid"].ToString();
        }

        protected void LoadClientNames()
        {
            ddlcname.Items.Clear();
            string selectquery = "select Clientname from Clients   where clientstatus=1 order by Clientname";
            DataTable dtable = config.ExecuteAdaptorAsyncWithQueryParams(selectquery).Result;

            if (dtable.Rows.Count > 0)
            {

                for (int i = 0; i < dtable.Rows.Count; i++)
                {
                    ddlcname.Items.Add(dtable.Rows[i]["Clientname"].ToString());
                }

                ddlcname.Items.Insert(0, "--Select--");
                ddlcname.Items.Insert(1, "ALL");
            }
            else
            {
                ddlcname.Items.Insert(0, "--Select--");
            }
        }

        protected void FillClientList()
        {
            string sqlQry = "Select ClientId from Clients Order By cast(substring(clientid," + Clength + ", 4) as int)";
            DataTable data = config.ExecuteAdaptorAsyncWithQueryParams(sqlQry).Result;
            ddlClientId.Items.Clear();

            if (data.Rows.Count > 0)
            {

                for (int i = 0; i < data.Rows.Count; i++)
                {
                    ddlClientId.Items.Add(data.Rows[i]["ClientId"].ToString());
                }

                ddlClientId.Items.Insert(0, "--Select--");
                ddlClientId.Items.Insert(1, "ALL");
            }
            else
            {
                ddlClientId.Items.Insert(0, "--Select--");
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
                    EmployeeReportLink.Visible = false;
                    break;
                default:
                    break;
            }
        }

        protected void ddlcname_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            ClearData();
            txtmonth.Text = "";
            if (ddlcname.SelectedIndex > 0)
            {
                FillClientid();
            }
            else
            {
                ddlClientId.SelectedIndex = 0;
            }
        }

        protected void ddlClientId_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearData();
            txtmonth.Text = "";
            if (ddlClientId.SelectedIndex > 0)
            {
                Fillcname();
            }
            else
            {
                ddlcname.SelectedIndex = 0;
            }
        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            ClearData();
            if (ddlClientId.SelectedIndex == 0)
            {

                return;
            }

            if (txtmonth.Text.Trim().Length == 0)
            {
                LblResult.Text = "Please Select The Date";
                return;
            }
            string month = DateTime.Parse(txtmonth.Text.Trim()).Month.ToString();
            string Year = DateTime.Parse(txtmonth.Text.Trim()).Year.ToString();
            string sqlqry = "select C.clientid,C.clientname,E.empid,E.empmname,EP.EmpPF,EP.EmprPF " +
                " from EPFDed EP inner join  Empdetails E  on EP.Empid=E.Empid  join Clients C on C.clientid=E.Unitid and " +
                "    C.clientid ='" + ddlClientId.SelectedValue + "'  and  EP.EmpPF<>0   and month='" + month + Year.Substring(2, 2) + "'";
            BindData(sqlqry);
        }

        protected void BindData(string Sqlqry)
        {
            DataTable dt = config.ExecuteAdaptorAsyncWithQueryParams(Sqlqry).Result;
            if (dt.Rows.Count > 0)
            {
                lbltamttext.Visible = true;
                GVListEmployees.DataSource = dt;
                GVListEmployees.DataBind();
                lbtn_Export.Visible = true;
            }
            else
            {
                lbtn_Export.Visible = false;
                LblResult.Text = "There Is No Pf Deductions For The Selected  Date/Client";
                return;
            }
        }

        protected void lbtn_Export_Click(object sender, EventArgs e)
        {
            gve.Export("PFReport.xls", this.GVListEmployees);
        }

        protected void ClearData()
        {
            GVListEmployees.DataSource = null;
            GVListEmployees.DataBind();
            lbltamttext.Visible = false;
            lbltemprpf.Text = "";
            lbltmtemppf.Text = "";

            LblResult.Text = "";
        }

    }
}