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
    public partial class NoAdAtPhReports : System.Web.UI.Page
    {
        AppConfiguration config = new AppConfiguration();
        GridViewExportUtil gve = new GridViewExportUtil();
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

                radioaddress_CheckedChanged(sender, e);
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
        }

        protected void ClearData()
        {
            GVListEmployees.DataSource = null;
            GVListEmployees.DataBind();
        }

        protected void DisplayData(string SqlQry)
        {
            DataTable dt = config.ExecuteAdaptorAsyncWithQueryParams(SqlQry).Result;
            if (dt.Rows.Count > 0)
            {
                GVListEmployees.DataSource = dt;
                GVListEmployees.DataBind();


                for (int i = 0; i < GVListEmployees.Rows.Count; i++)
                {

                    string dofbirth = GVListEmployees.Rows[i].Cells[4].Text;
                    string dofjoinh = GVListEmployees.Rows[i].Cells[5].Text;
                    string dofleave = GVListEmployees.Rows[i].Cells[6].Text;

                    if (dofbirth == "01/01/1990" || dofbirth == "01/01/1900")
                    {
                        GVListEmployees.Rows[i].Cells[4].Text = "";
                    }
                    if (dofjoinh == "01/01/1990" || dofjoinh == "01/01/1900")
                    {
                        GVListEmployees.Rows[i].Cells[5].Text = "";
                    }

                    if (dofleave == "01/01/1990" || dofleave == "01/01/1900")
                    {
                        GVListEmployees.Rows[i].Cells[6].Text = "";

                    }
                }


            }

            else
            {
                LblResult.Text = "No record found";
            }
        }

        protected void radioaddress_CheckedChanged(object sender, EventArgs e)
        {
            ClearData();

            string Sqlqry = "";
            if (radioaddress.Checked)
            {
                Sqlqry = "Select EmpId,EmpFName,EmpMName,EmpDesgn,EmpDtofBirth,EmpDtofJoining,EmpDtofLeaving,EmpPresentAddress  from empdetails where EmpPermanentAddress is null order by empid";
                DisplayData(Sqlqry);
                return;
            }
            if (radiophotos.Checked)
            {
                Sqlqry = "Select EmpId,EmpFName,EmpMName,EmpDesgn,EmpDtofBirth,EmpDtofJoining,EmpDtofLeaving,EmpPresentAddress   from empdetails where empphoto is null order by  empid";
                DisplayData(Sqlqry);
                return;
            }

            if (RadioNonpf.Checked)
            {
                Sqlqry = "Select EmpId,EmpFName,EmpMName,EmpDesgn,EmpDtofBirth,EmpDtofJoining,EmpDtofLeaving,EmpPresentAddress   from empdetails where EmpPFDeduct =0 order by  empid";
                DisplayData(Sqlqry);
                return;
            }
            if (RadioNonesi.Checked)
            {
                Sqlqry = "Select EmpId,EmpFName,EmpMName,EmpDesgn,EmpDtofBirth,EmpDtofJoining,EmpDtofLeaving,EmpPresentAddress   from empdetails where EmpESIDeduct = 0 order by  empid";
                DisplayData(Sqlqry);
                return;
            }
        }


        protected void lbtn_Export_Click(object sender, EventArgs e)
        {
            gve.Export("Noadpfesireports.xls", this.GVListEmployees);
        }
    }
}