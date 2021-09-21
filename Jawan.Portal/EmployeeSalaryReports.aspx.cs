using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KLTS.Data;
using System.Data;
using System.Collections;
using System.Globalization;
using Jawan.Portal.DAL;
namespace Jawan.Portal
{
    public partial class EmployeeSalaryReports : System.Web.UI.Page
    {
        AppConfiguration config = new AppConfiguration();
        GridViewExportUtil gve = new GridViewExportUtil();
        string EmpIDPrefix = "";
        string BranchID = "";
        string CmpIDPrefix = "";
        string Elength = "";
        string Clength = "";
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
                        FillEmployeesList();
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


        protected void FillEmployeesList()
        {
            DataTable DtEmpIds = GlobalData.Instance.LoadEmpIds(EmpIDPrefix,BranchID);
            if (DtEmpIds.Rows.Count > 0)
            {
                ddlEmployee.DataValueField = "empid";
                ddlEmployee.DataTextField = "empid";
                ddlEmployee.DataSource = DtEmpIds;
                ddlEmployee.DataBind();
            }
            ddlEmployee.Items.Insert(0, "-Select-");
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

        protected void ddlEmployee_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cleardata();

            if (ddlEmployee.SelectedIndex > 0)
            {
                ddlempname.SelectedValue = ddlEmployee.SelectedValue;
            }
            else
            {
                ddlempname.SelectedIndex = 0;
            }

        }

        protected void ddlempname_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cleardata();

            if (ddlempname.SelectedIndex > 0)
            {
                ddlEmployee.SelectedValue = ddlempname.SelectedValue;
            }
            else
            {
                ddlEmployee.SelectedIndex = 0;
            }
        }


        protected void btnSearchSalaries_Click(object sender, EventArgs e)
        {

            Cleardata();


            #region  Begin New code  For Validatations As on [12-11-2013]
            if (txtMonth.Text.Trim().Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Please Select The Month');", true);
                return;
            }


            if (ddlsearchtype.SelectedIndex == 1)
            {
                if (ddlEmployee.SelectedIndex == 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Please Select The Employee ID/NAME');", true);
                    return;
                }
            }




            #endregion End  New code  For Validatations As on [12-11-2013]

            #region  Begin New code For Variable Declaration  As on [12-11-2013]
            var SqlQryforSalary = "";
            DataTable DtSalary = null;
            var SelectedMonth = "";
            var SelectedEmployeeID = "";
            Hashtable HtSalary = new Hashtable();
            var ProcedureName = "";
            var Month = "";
            var Year = "";

            //New Code as on 28/12/2013 for index of searchtype by venkat

            var index = 0;

            if (ddlsearchtype.SelectedIndex == 0)
            {
                index = 0;
            }
            if (ddlsearchtype.SelectedIndex == 1)
            {
                index = 1;
            }

            #endregion End New Code  For Variable Declaration  As on [12-11-2013]


            #region  Begin New Code  For Assign Values as on [12-11-2013]

            if (ddlsearchtype.SelectedIndex == 1)
            {
                SelectedEmployeeID = ddlEmployee.SelectedValue;
            }
            SelectedMonth = DateTime.Parse(txtMonth.Text.Trim(), CultureInfo.GetCultureInfo("en-gb")).ToString();
            Month = DateTime.Parse(SelectedMonth).Month.ToString();
            Year = DateTime.Parse(SelectedMonth).Year.ToString().Substring(2, 2);
            SelectedMonth = Month + Year;
            ProcedureName = "ReportForEmployeeSalaries";
            #endregion End New Code  For Assign Values as on [12-11-2013]

            #region Begin New Code For Assign Values to the Hashtable Parameters as on [12-11-2013]
            HtSalary.Add("@index", index);
            HtSalary.Add("@Month", SelectedMonth);
            HtSalary.Add("@empid", SelectedEmployeeID);
            #endregion End New Code For Assign Values to the Hashtable Parameters as on [12-11-2013]

            DtSalary =config.ExecuteAdaptorAsyncWithParams(ProcedureName, HtSalary).Result;


            if (DtSalary.Rows.Count > 0)
            {
                GVListEmployees.DataSource = DtSalary;
                GVListEmployees.DataBind();


            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Search Criteria Not Available');", true);
                return;
            }
        }

        protected void Cleardata()
        {
            GVListEmployees.DataSource = null;
            GVListEmployees.DataBind();
        }


        float totalDuties = 0;
        float totalOTs = 0;
        float totalNHs = 0;
        float totalWOs = 0;
        float totalNPOTs = 0;
        float totalOTsAMT = 0;
        float totalNPOTSAmt = 0;
        float totalTotalSalary = 0;
        float totalpf = 0;
        float totalesi = 0;
        float totalPT = 0;
        float totalPenalty = 0;
        float totalCanteenadv = 0;
        float totalsaladvded = 0;
        float totalUniformDed = 0;
        float totalTotaldeductions = 0;
        float totalactualamount = 0;
        float totalOtherDed = 0;
        float totalDutiesAmt = 0;
        float totalOTsAmt = 0;
        float totalNHsAmt = 0;
        float totalWOsAmt = 0;
        protected void GVListEmployees_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            int totalItems = 0;
            double total = 0;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                float duties = float.Parse(((Label)e.Row.FindControl("lblduties")).Text);
                totalDuties += duties;
                float dutiesAmt = float.Parse(((Label)e.Row.FindControl("lblDutyamt")).Text);
                totalDutiesAmt += dutiesAmt;
                float Ots = float.Parse(((Label)e.Row.FindControl("lblots")).Text);
                totalOTs += Ots;
                float OtsAmt = float.Parse(((Label)e.Row.FindControl("lblotamt")).Text);
                totalOTsAmt += OtsAmt;
                float NHs = float.Parse(((Label)e.Row.FindControl("lblnhs")).Text);
                totalNHs += NHs;
                float NHsAmt = float.Parse(((Label)e.Row.FindControl("lblNhsamt")).Text);
                totalNHsAmt += NHsAmt;
                float WOs = float.Parse(((Label)e.Row.FindControl("lblwos")).Text);
                totalWOs += WOs;
                float WOsAmt = float.Parse(((Label)e.Row.FindControl("lblWOAmt")).Text);
                totalWOsAmt += WOsAmt;
                float NPOTs = float.Parse(((Label)e.Row.FindControl("lblnpots")).Text);
                totalNPOTs += NPOTs;
                float NPOTSAmt = float.Parse(((Label)e.Row.FindControl("lblNPOTSAmt")).Text);
                totalNPOTSAmt += NPOTSAmt;
                float TotalSalary = float.Parse(((Label)e.Row.FindControl("lblTotalSalary")).Text);
                totalTotalSalary += TotalSalary;
                float pf = float.Parse(((Label)e.Row.FindControl("lblpf")).Text);
                totalpf += pf;
                float esi = float.Parse(((Label)e.Row.FindControl("lblesi")).Text);
                totalesi += esi;
                float PT = float.Parse(((Label)e.Row.FindControl("lblPT")).Text);
                totalPT += PT;
                float Penalty = float.Parse(((Label)e.Row.FindControl("lblPenalty")).Text);
                totalPenalty += Penalty;
                float Canteenadv = float.Parse(((Label)e.Row.FindControl("lblCanteenadv")).Text);
                totalCanteenadv += Canteenadv;
                float saladvded = float.Parse(((Label)e.Row.FindControl("lblsaladvded")).Text);
                totalsaladvded += saladvded;
                float UniformDed = float.Parse(((Label)e.Row.FindControl("lblUniformDed")).Text);
                totalUniformDed += UniformDed;
                float OtherDed = float.Parse(((Label)e.Row.FindControl("lblOtherDed")).Text);
                totalOtherDed += OtherDed;
                float Totaldeductions = float.Parse(((Label)e.Row.FindControl("lblTotaldeductions")).Text);
                totalTotaldeductions += Totaldeductions;
                float actualamount = float.Parse(((Label)e.Row.FindControl("lblactualamount")).Text);
                totalactualamount += actualamount;

            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                ((Label)e.Row.FindControl("lblTotalDuties")).Text = totalDuties.ToString();
                ((Label)e.Row.FindControl("lblDutyTotalAmt")).Text = totalDutiesAmt.ToString();
                ((Label)e.Row.FindControl("lblTotalOTs")).Text = totalOTs.ToString();
                ((Label)e.Row.FindControl("lblOTsTotalAmt")).Text = totalOTsAmt.ToString();
                ((Label)e.Row.FindControl("lblTotalNHs")).Text = totalNHs.ToString();
                ((Label)e.Row.FindControl("lblNhsamtTotalAmt")).Text = totalNHsAmt.ToString();
                ((Label)e.Row.FindControl("lblTotalWOs")).Text = totalWOs.ToString();
                ((Label)e.Row.FindControl("lblWOAmtTotalAmt")).Text = totalWOsAmt.ToString();
                ((Label)e.Row.FindControl("lblTotalNPOTs")).Text = totalNPOTs.ToString();
                ((Label)e.Row.FindControl("lblNPOTSAmtTotalAmt")).Text = totalNPOTSAmt.ToString();
                ((Label)e.Row.FindControl("lblTotalSalaryTotalAmt")).Text = totalTotalSalary.ToString();
                ((Label)e.Row.FindControl("lblpfTotalAmt")).Text = totalpf.ToString();
                ((Label)e.Row.FindControl("lblesiTotalAmt")).Text = totalesi.ToString();
                ((Label)e.Row.FindControl("lblPTTotalAmt")).Text = totalPT.ToString();
                ((Label)e.Row.FindControl("lblPenaltyTotalAmt")).Text = totalPenalty.ToString();
                ((Label)e.Row.FindControl("lblCanteenadvTotalAmt")).Text = totalCanteenadv.ToString();
                ((Label)e.Row.FindControl("lblsaladvdedTotalAmt")).Text = totalsaladvded.ToString();
                ((Label)e.Row.FindControl("lblUniformDedAmt")).Text = totalUniformDed.ToString();
                ((Label)e.Row.FindControl("lblOtherDedAmt")).Text = totalOtherDed.ToString();
                ((Label)e.Row.FindControl("lblTotaldeductionsAmt")).Text = totalTotaldeductions.ToString();
                ((Label)e.Row.FindControl("lblactualamountTotalAmt")).Text = totalactualamount.ToString();


                //((Label)e.Row.FindControl("lblTotalAmt")).Text = totalOTsAMT.ToString();
            }


        }
        protected void lbtn_Export_Click(object sender, EventArgs e)
        {
            gve.Export("EmpSalaryReport.xls", this.GVListEmployees);
        }
        protected void GVListEmployees_RowDataBound1(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                e.Row.Cells[1].Attributes.Add("class", "text");
                e.Row.Cells[2].Attributes.Add("class", "text");
            }
        }
    }
}