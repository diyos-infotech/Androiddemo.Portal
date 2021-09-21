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
    public partial class PfDeductionreports : System.Web.UI.Page
    {
        AppConfiguration config = new AppConfiguration();
        GridViewExportUtil gve = new GridViewExportUtil();
        protected void Page_Load(object sender, EventArgs e)
        {
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
                    break;
                default:
                    break;
            }
        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            //ClearAmontdetails();
            LblResult.Text = "";
            LblResult.Visible = true;
            GVListEmployees.DataSource = null;
            GVListEmployees.DataBind();

            string date = string.Empty;

            if (txtstartdate.Text.Trim().Length == 0)
            {
                LblResult.Text = "Please Select The Date";
                return;
            }
            if (txtstartdate.Text.Trim().Length > 0)
            {
                date = DateTime.Parse(txtstartdate.Text.Trim(), CultureInfo.GetCultureInfo("en-gb")).ToString();
            }
            string month = DateTime.Parse(date).Month.ToString();
            string Year = DateTime.Parse(date).Year.ToString();
            string SqlQry = "Select E.EmpId,E.EmpmName, EP.EmpPF,EP.EmprPF from EPFDed EP inner join Empdetails E on month='" +
                month + Year.Substring(2, 2) + "' and  EP.EMPID=E.EmpId";
            DataTable dt = config.ExecuteAdaptorAsyncWithQueryParams(SqlQry).Result;
            if (dt.Rows.Count > 0)
            {
                //lbltamttext.Visible = true;
                GVListEmployees.DataSource = dt;
                GVListEmployees.DataBind();
                //TotalAmount();
            }
            else
            {
                LblResult.Text = "There Is No Pf Deductions For The Selected  Date";
                return;
            }
        }

        //protected void TotalAmount()
        //{
        //    float totalemppf = 0;
        //    float totalemprpf = 0;

        //    for (int i = 0; i < GVListEmployees.Rows.Count; i++ )
        //    {
        //        string emppf = ((Label)GVListEmployees.Rows[i].FindControl("lblepf")).Text;
        //        string emprpf = ((Label)GVListEmployees.Rows[i].FindControl("lblerpf")).Text;


        //        totalemppf += float.Parse(emppf);
        //        totalemprpf += float.Parse(emprpf);

        //    }


        //    //lbltmtemppf.Text = totalemppf.ToString();
        //    //lbltemprpf.Text = totalemprpf.ToString();

        //}


        //protected void ClearAmontdetails()
        //{
        //    //lbltamttext.Visible = false;
        //    //lbltmtemppf.Text = "";
        //    //lbltemprpf.Text = "";
        //}


        protected void lbtn_Export_Click(object sender, EventArgs e)
        {
            gve.Export("PfDeductionreports.xls", this.GVListEmployees);
        }

        double totalPF = 0;
        double totalEMPRPF = 0;
        protected void GVListEmployees_RowDataBound(object sender, GridViewRowEventArgs e)
        {


            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                double PF = double.Parse(((Label)e.Row.FindControl("lblepf")).Text);
                totalPF += PF;
                double EmpReSI = double.Parse(((Label)e.Row.FindControl("lblerpf")).Text);
                totalEMPRPF += EmpReSI;
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                ((Label)e.Row.FindControl("lblPF")).Text = totalPF.ToString();
                ((Label)e.Row.FindControl("lblErPF")).Text = totalEMPRPF.ToString();

            }
        }
    }
}