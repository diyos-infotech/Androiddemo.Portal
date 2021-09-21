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
    public partial class EmpSummary : System.Web.UI.Page
    {
        AppConfiguration config = new AppConfiguration();
        GridViewExportUtil gve = new GridViewExportUtil();
        string EmpIDPrefix = "";
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
                        //LoadEmpIds();
                        // LoadNames();
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
        }
        protected void lbtn_Export_Click(object sender, EventArgs e)
        {
            gve.Export("EmpSummary.xls", this.GVListEmployees);
        }


        //protected void LoadEmpIds()
        //{
        //    DataTable DtEmpIds = GlobalData.Instance.LoadEmpIds(EmpIDPrefix);
        //    if (DtEmpIds.Rows.Count > 0)
        //    {
        //        ddlempid.DataValueField = "empid";
        //        ddlempid.DataTextField = "empid";
        //        ddlempid.DataSource = DtEmpIds;
        //        ddlempid.DataBind();
        //    }
        //    ddlempid.Items.Insert(0, "Select");
        //}


        //protected void LoadNames()
        //{
        //    DataTable DtEmpNames = GlobalData.Instance.LoadEmpNames(EmpIDPrefix);
        //    if (DtEmpNames.Rows.Count > 0)
        //    {
        //        ddlempname.DataValueField = "empid";
        //        ddlempname.DataTextField = "FullName";
        //        ddlempname.DataSource = DtEmpNames;
        //        ddlempname.DataBind();
        //    }
        //    ddlempname.Items.Insert(0, "Select");
        //}


        //protected void ddlempid_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    ClearData();

        //    if (ddlempid.SelectedIndex > 0)
        //    {
        //        ddlempname.SelectedValue = ddlempid.SelectedValue;
        //    }
        //    else
        //    {
        //        txtStrtDate.Text = "";
        //        txtEndDate.Text = "";
        //        ddlempname.SelectedIndex = 0;
        //    }

        //}

        //protected void ddlempname_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    ClearData();
        //    if (ddlempname.SelectedIndex > 0)
        //    {
        //        ddlempid.SelectedValue = ddlempname.SelectedValue;
        //    }
        //    else
        //    {
        //        txtStrtDate.Text = "";
        //        txtEndDate.Text = "";
        //        ddlempid.SelectedIndex = 0;
        //    }
        //}

        protected void ClearData()
        {
            LblResult.Text = "";
            GVListEmployees.DataSource = null;
            GVListEmployees.DataBind();
        }

        public void loadbalance()
        {

            string qry = "";
            DataTable dt = null;
            string balance = "";
            if (ddlloantype.SelectedIndex == 1)
            {
                qry = "select( (select isnull(SUM(isnull(LoanAmount,0)),0) from EmpLoanMaster where EmpId='" + txtEmpid.Text + "' and typeofloan=0)-(select isnull(SUM(isnull(recamt,0)),0) from EmpLoanDetails where Empid='" + txtEmpid.Text + "' and loantype=0) ) as balance ";
                dt = config.ExecuteAdaptorAsyncWithQueryParams(qry).Result;
            }

            else if (ddlloantype.SelectedIndex == 2)
            {
                qry = "select( (select isnull(SUM(isnull(LoanAmount,0)),0) from EmpLoanMaster where EmpId='" + txtEmpid.Text + "' and typeofloan=1)-(select isnull(SUM(isnull(recamt,0)),0) from EmpLoanDetails where Empid='" + txtEmpid.Text + "' and loantype=1) ) as balance ";
                dt = config.ExecuteAdaptorAsyncWithQueryParams(qry).Result;
            }
            else if (ddlloantype.SelectedIndex == 3)
            {
                qry = "select( (select isnull(SUM(isnull(LoanAmount,0)),0) from EmpLoanMaster where EmpId='" + txtEmpid.Text + "' and typeofloan=2)-(select isnull(SUM(isnull(recamt,0)),0) from EmpLoanDetails where Empid='" + txtEmpid.Text + "' and loantype=2) ) as balance ";
                dt = config.ExecuteAdaptorAsyncWithQueryParams(qry).Result;
            }
            else if (ddlloantype.SelectedIndex == 4)
            {
                qry = "select( (select isnull(SUM(isnull(LoanAmount,0)),0) from EmpLoanMaster where EmpId='" + txtEmpid.Text + "' and typeofloan=3)-(select isnull(SUM(isnull(recamt,0)),0) from EmpLoanDetails where Empid='" + txtEmpid.Text + "' and loantype=3) ) as balance ";
                dt = config.ExecuteAdaptorAsyncWithQueryParams(qry).Result;
            }
            else if (ddlloantype.SelectedIndex == 5)
            {
                qry = "select( (select isnull(SUM(isnull(LoanAmount,0)),0) from EmpLoanMaster where EmpId='" + txtEmpid.Text + "' and typeofloan=4)-(select isnull(SUM(isnull(recamt,0)),0) from EmpLoanDetails where Empid='" + txtEmpid.Text + "' and loantype=4) ) as balance ";
                dt = config.ExecuteAdaptorAsyncWithQueryParams(qry).Result;
            }
            else if (ddlloantype.SelectedIndex == 6)
            {
                qry = "select( (select isnull(SUM(isnull(LoanAmount,0)),0) from EmpLoanMaster where EmpId='" + txtEmpid.Text + "' and typeofloan=5)-(select isnull(SUM(isnull(recamt,0)),0) from EmpLoanDetails where Empid='" + txtEmpid.Text + "' and loantype=5) ) as balance ";
                dt = config.ExecuteAdaptorAsyncWithQueryParams(qry).Result;
            }

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0][0].ToString() == "" || dt.Rows[0][0].ToString() == "0")
                {
                    lblbalance.Visible = false;
                    txtbalance.Visible = false;
                    lblloanissued.Visible = false;
                    lblloandeducted.Visible = false;

                    //  ScriptManager.RegisterStartupScript(this, GetType(), "Show alert", "alert('No loan details found');", true);

                }
                else
                {
                    lblloanissued.Visible = true;
                    lblloandeducted.Visible = true;
                    lblbalance.Visible = true;
                    txtbalance.Visible = true;
                    balance = dt.Rows[0]["balance"].ToString();
                    txtbalance.Text = balance;
                }


            }
            else
            {
                lblbalance.Visible = false;
                txtbalance.Visible = false;
                lblloanissued.Visible = false;
                lblloandeducted.Visible = false;

                ScriptManager.RegisterStartupScript(this, GetType(), "Show alert", "alert('No loan details found');", true);

            }
        }

        protected void btn_Submit_Click(object sender, EventArgs e)
        {


            LblResult.Visible = true;
            ClearData();

            if (txtEmpid.Text.Trim() == "")
            {
                LblResult.Text = "Please Select Employee Id/Employee Name";
                return;
            }
            string startdate = string.Empty;
            string enddate = string.Empty;
            string qry = string.Empty;
            DataTable dtloan = null;
            if (ddlloantype.SelectedIndex == 0)
            {

                lbtn_Export.Visible = true;
                pnlloans.Visible = false;
                string Sqlqry = "select ep.clientid,clientname,ep.empid,(empfname+' '+empmname+' '+emplname) as empname,month,d.design,Desgn,NoOfDuties,ep.ots,ep.nhs,(ep.gross+ep.Incentivs+ep.Gratuity+ep.LeaveEncashAmt+ep.WOAmt+ep.nhsamt+ep.Npotsamt+ep.OtAmt) as gross,ep.LoanDed,ep.otamt,ep.saladvded,ep.uniformded,ep.securitydepded,ep.penalty as penalty,ep.canteenadv,ep.Generalded,ep.otherded,ep.pf,ep.esi,ep.proftax,(EP.Pf+ep.esi+EP.ProfTax+EP.SalAdvDed+EP.UniformDed+EP.OtherDed+EP.CanteenAdv+EP.Penalty+ISNULL(ep.SecurityDepDed,0)+ISNULL(GeneralDed,0)) as TotalDeductions,((EP.Gross+EP.OTAmt+ep.Incentivs+ep.Gratuity+ep.LeaveEncashAmt+ep.WOAmt+ep.nhsamt+ep.Npotsamt)-(EP.Pf+EP.Esi+EP.ProfTax+EP.SalAdvDed+EP.UniformDed +ISNULL(ep.SecurityDepDed,0)+ISNULL(GeneralDed,0)+ EP.OtherDed+EP.CanteenAdv+EP.Penalty)) as ActualAmount from emppaysheet ep inner join clients c on c.clientid=ep.clientid inner join empdetails e on e.empid=ep.empid inner join Designations d on d.Designid=ep.Desgn where ep.empid ='" + txtEmpid.Text + "' ";
                DataTable dt = config.ExecuteAdaptorAsyncWithQueryParams(Sqlqry).Result;
                if (dt.Rows.Count > 0)
                {
                    GVListEmployees.DataSource = dt;
                    GVListEmployees.DataBind();
                }
                else
                {
                    GVListEmployees.DataSource = null;
                    GVListEmployees.DataBind();
                }
            }


            if (ddlloantype.SelectedIndex == 1)
            {
                lbtn_Export.Visible = false;
                pnlloans.Visible = true;
                qry = "select loanno,(case TypeOfLoan when 0 then 'Salary Adv' when 1 then 'Uniform Ded' when 2 then 'Security Dep'  when 3 then 'Loan Ded' when 4 then 'Other Ded'  end) as TypeOfLoan, LoanAmount,cast(NoInstalments as nvarchar(5)) +  ' / ' + ('Remarks : '+isnull(Loantype,' ') ) NoInstalments,(case LoanStatus when 1 then 'Completed' when 0 then 'InComplete' end) as LoanStatus ,convert(varchar(10),LoanIssuedDate,103) as LoanIssuedDate from EmpLoanMaster  where empid='" + txtEmpid.Text + "' and TypeOfLoan=0  order by loanno";
                dtloan = config.ExecuteAdaptorAsyncWithQueryParams(qry).Result;

                if (dtloan.Rows.Count > 0)
                {
                    GVLoanIssued.DataSource = dtloan;
                    GVLoanIssued.DataBind();
                }
                else
                {
                    GVLoanIssued.DataSource = null;
                    GVLoanIssued.DataBind();
                }

                qry = "select (eld.clientid+ '-' + c.clientname) as clientid,clientname,loanno,(case LoanType when 0 then 'Salary Adv' when 1 then 'Uniform Ded' when 2 then 'Security Dep'  when 3 then 'Loan Ded' when 4 then 'Other Ded'  end) as LoanType,RecAmt,eld.ClientID,LoanCuttingMonth from EmpLoanDetails eld  inner join clients c on c.clientid=eld.clientid where empid='" + txtEmpid.Text + "' and LoanType=0 order by loanno";
                dtloan =config.ExecuteAdaptorAsyncWithQueryParams(qry).Result;
                if (dtloan.Rows.Count > 0)
                {
                    GVLoanDeducted.DataSource = dtloan;
                    GVLoanDeducted.DataBind();
                }
                else
                {
                    GVLoanDeducted.DataSource = null;
                    GVLoanDeducted.DataBind();
                }

                loadbalance();


            }


            if (ddlloantype.SelectedIndex == 2)
            {
                lbtn_Export.Visible = false;
                pnlloans.Visible = true;
                qry = "select loanno,(case TypeOfLoan when 0 then 'Salary Adv' when 1 then 'Uniform Ded' when 2 then 'Security Dep'  when 3 then 'Loan Ded' when 4 then 'Other Ded'  end) as TypeOfLoan, LoanAmount,cast(NoInstalments as nvarchar(5)) +  ' / ' + ('Remarks : '+isnull(Loantype,' ') ) NoInstalments,(case LoanStatus when 1 then 'Completed' when 0 then 'InComplete' end) as LoanStatus ,convert(varchar(10),LoanIssuedDate,103) as LoanIssuedDate from EmpLoanMaster  where empid='" + txtEmpid.Text + "' and TypeOfLoan=1  order by loanno";
                dtloan = config.ExecuteAdaptorAsyncWithQueryParams(qry).Result;

                if (dtloan.Rows.Count > 0)
                {
                    GVLoanIssued.DataSource = dtloan;
                    GVLoanIssued.DataBind();
                }
                else
                {
                    GVLoanIssued.DataSource = null;
                    GVLoanIssued.DataBind();
                }

                qry = "select (eld.clientid+ '-' + c.clientname) as clientid,clientname,loanno,(case LoanType when 0 then 'Salary Adv' when 1 then 'Uniform Ded' when 2 then 'Security Dep'  when 3 then 'Loan Ded' when 4 then 'Other Ded'  end) as LoanType,RecAmt,eld.ClientID,LoanCuttingMonth from EmpLoanDetails eld  inner join clients c on c.clientid=eld.clientid where empid='" + txtEmpid.Text + "' and LoanType=1 order by loanno";
                dtloan = config.ExecuteAdaptorAsyncWithQueryParams(qry).Result;
                if (dtloan.Rows.Count > 0)
                {
                    GVLoanDeducted.DataSource = dtloan;
                    GVLoanDeducted.DataBind();
                }
                else
                {
                    GVLoanDeducted.DataSource = null;
                    GVLoanDeducted.DataBind();
                }

                loadbalance();

            }



            if (ddlloantype.SelectedIndex == 3)
            {
                lbtn_Export.Visible = false;
                pnlloans.Visible = true;
                qry = "select loanno,(case TypeOfLoan when 0 then 'Salary Adv' when 1 then 'Uniform Ded' when 2 then 'Security Dep'  when 3 then 'Loan Ded' when 4 then 'Other Ded'  end) as TypeOfLoan,  LoanAmount,cast(NoInstalments as nvarchar(5)) +  ' / ' + ('Remarks : '+isnull(Loantype,' ') ) NoInstalments,(case LoanStatus when 1 then 'Completed' when 0 then 'InComplete' end) as LoanStatus ,convert(varchar(10),LoanIssuedDate,103) as LoanIssuedDate from EmpLoanMaster  where empid='" + txtEmpid.Text + "' and TypeOfLoan=2 order by loanno";
                dtloan = config.ExecuteAdaptorAsyncWithQueryParams(qry).Result;

                if (dtloan.Rows.Count > 0)
                {
                    GVLoanIssued.DataSource = dtloan;
                    GVLoanIssued.DataBind();
                }
                else
                {
                    GVLoanIssued.DataSource = null;
                    GVLoanIssued.DataBind();
                }

                qry = "select (eld.clientid+ '-' + c.clientname) as clientid,clientname,loanno,(case LoanType when 0 then 'Salary Adv' when 1 then 'Uniform Ded' when 2 then 'Security Dep'  when 3 then 'Loan Ded' when 4 then 'Other Ded'  end) as LoanType,RecAmt,eld.ClientID,LoanCuttingMonth from EmpLoanDetails eld  inner join clients c on c.clientid=eld.clientid where empid='" + txtEmpid.Text + "' and LoanType=2 order by loanno";
                dtloan = config.ExecuteAdaptorAsyncWithQueryParams(qry).Result;
                if (dtloan.Rows.Count > 0)
                {
                    GVLoanDeducted.DataSource = dtloan;
                    GVLoanDeducted.DataBind();
                }
                else
                {
                    GVLoanDeducted.DataSource = null;
                    GVLoanDeducted.DataBind();
                }

                loadbalance();

            }

            if (ddlloantype.SelectedIndex == 4)
            {
                lbtn_Export.Visible = false;
                pnlloans.Visible = true;
                qry = "select loanno,(case TypeOfLoan when 0 then 'Salary Adv' when 1 then 'Uniform Ded' when 2 then 'Security Dep'  when 3 then 'loan Ded' when 4 then 'Other Ded' end) as TypeOfLoan,LoanAmount,cast(NoInstalments as nvarchar(5)) +  ' / ' + ('Remarks : '+isnull(Loantype,' ') ) NoInstalments,(case LoanStatus when 1 then 'Completed' when 0 then 'InComplete' end) as LoanStatus ,convert(varchar(10),LoanIssuedDate,103) as LoanIssuedDate from EmpLoanMaster  where empid='" + txtEmpid.Text + "' and TypeOfLoan=3 order by loanno";
                dtloan = config.ExecuteAdaptorAsyncWithQueryParams(qry).Result;

                if (dtloan.Rows.Count > 0)
                {
                    GVLoanIssued.DataSource = dtloan;
                    GVLoanIssued.DataBind();
                }
                else
                {
                    GVLoanIssued.DataSource = null;
                    GVLoanIssued.DataBind();

                }

                qry = "select (eld.clientid+ '-' + c.clientname) as clientid,clientname,loanno,(case LoanType when 0 then 'Salary Adv' when 1 then 'Uniform Ded' when 2 then 'Security Dep'  when 3 then 'Loan Ded'  when 4 then 'Other Ded'  end) as LoanType,RecAmt,eld.ClientID,LoanCuttingMonth from EmpLoanDetails eld  inner join clients c on c.clientid=eld.clientid where empid='" + txtEmpid.Text + "' and LoanType=3 order by loanno";
                dtloan = config.ExecuteAdaptorAsyncWithQueryParams(qry).Result;
                if (dtloan.Rows.Count > 0)
                {
                    GVLoanDeducted.DataSource = dtloan;
                    GVLoanDeducted.DataBind();
                }
                else
                {
                    GVLoanDeducted.DataSource = null;
                    GVLoanDeducted.DataBind();
                }

                loadbalance();


            }

            if (ddlloantype.SelectedIndex == 5)
            {
                lbtn_Export.Visible = false;
                pnlloans.Visible = true;
                qry = "select loanno,(case TypeOfLoan when 0 then 'Salary Adv' when 1 then 'Uniform Ded' when 2 then 'Security Dep'  when 3 then 'Other Ded'  end) as TypeOfLoan,  LoanAmount,cast(NoInstalments as nvarchar(5)) +  ' / ' + ('Remarks : '+isnull(Loantype,' ') ) NoInstalments,(case LoanStatus when 1 then 'Completed' when 0 then 'InComplete' end) as LoanStatus ,convert(varchar(10),LoanIssuedDate,103) as LoanIssuedDate from EmpLoanMaster  where empid='" + txtEmpid.Text + "' and TypeOfLoan=4 order by loanno";
                dtloan = config.ExecuteAdaptorAsyncWithQueryParams(qry).Result;

                if (dtloan.Rows.Count > 0)
                {
                    GVLoanIssued.DataSource = dtloan;
                    GVLoanIssued.DataBind();
                }
                else
                {
                    GVLoanIssued.DataSource = null;
                    GVLoanIssued.DataBind();
                }

                qry = "select (eld.clientid+ '-' + c.clientname) as clientid,clientname,loanno,(case LoanType when 0 then 'Salary Adv' when 1 then 'Uniform Ded' when 2 then 'Security Dep'  when 3 then 'Other Ded'  end) as LoanType,RecAmt,eld.ClientID,LoanCuttingMonth from EmpLoanDetails eld  inner join clients c on c.clientid=eld.clientid where empid='" + txtEmpid.Text + "' and LoanType=4 order by loanno";
                dtloan = config.ExecuteAdaptorAsyncWithQueryParams(qry).Result;
                if (dtloan.Rows.Count > 0)
                {
                    GVLoanDeducted.DataSource = dtloan;
                    GVLoanDeducted.DataBind();
                }
                else
                {
                    GVLoanDeducted.DataSource = null;
                    GVLoanDeducted.DataBind();
                }

                loadbalance();

            }

            //if (ddlloantype.SelectedIndex == 5)
            //{
            //    lbtn_Export.Visible = false;
            //    pnlloans.Visible = true;
            //    qry = "select loanno,(case TypeOfLoan when 0 then 'Salary Adv' when 1 then 'Uniform Ded' when 2 then 'Security Dep'  when 3 then 'Other Ded'  when 4 then 'Room Rent'  end) as TypeOfLoan,LoanAmount,cast(NoInstalments as nvarchar(5)) +  ' / ' + ('Remarks : '+isnull(Loantype,' ') ) NoInstalments,(case LoanStatus when 1 then 'Completed' when 0 then 'InComplete' end) as LoanStatus ,convert(varchar(10),LoanIssuedDate,103) as LoanIssuedDate from EmpLoanMaster  where empid='" + txtEmpid.Text + "' and TypeOfLoan=4 order by loanno";
            //    dtloan = SqlHelper.Instance.GetTableByQuery(qry);

            //    if (dtloan.Rows.Count > 0)
            //    {
            //        GVLoanIssued.DataSource = dtloan;
            //        GVLoanIssued.DataBind();

            //    }
            //    else
            //    {
            //        GVLoanIssued.DataSource = null;
            //        GVLoanIssued.DataBind();
            //    }

            //    qry = "select (eld.clientid+ '-' + c.clientname) as clientid,clientname,loanno,(case LoanType when 0 then 'Salary Adv' when 1 then 'Uniform Ded' when 2 then 'Security Dep'  when 3 then 'Other Ded'  when 4 then 'Room Rent'  end) as LoanType,RecAmt,eld.ClientID,LoanCuttingMonth from EmpLoanDetails eld  inner join clients c on c.clientid=eld.clientid where empid='" + txtEmpid.Text + "' and LoanType=4 order by loanno";
            //    dtloan = SqlHelper.Instance.GetTableByQuery(qry);
            //    if (dtloan.Rows.Count > 0)
            //    {
            //        GVLoanDeducted.DataSource = dtloan;
            //        GVLoanDeducted.DataBind();
            //        lbtn_Export.Visible = false;
            //    }
            //    else
            //    {
            //        GVLoanDeducted.DataSource = null;
            //        GVLoanDeducted.DataBind();
            //        lbtn_Export.Visible = false;
            //    }

            //    loadbalance();


            //}

        }
        protected void GetEmpid()
        {

            #region  Old Code
            string Sqlqry = "select Empid from empdetails where (empfname+' '+empmname+' '+emplname)  like '" + txtName.Text + "' ";
            DataTable dt = config.ExecuteAdaptorAsyncWithQueryParams(Sqlqry).Result;
            if (dt.Rows.Count > 0)
            {
                try
                {
                    txtEmpid.Text = dt.Rows[0]["Empid"].ToString();

                }
                catch (Exception ex)
                {
                    // MessageLabel.Text = ex.Message;
                }
            }
            else
            {
                // MessageLabel.Text = "There Is No Name For The Selected Employee";
            }
            #endregion // End Old Code




        }


        protected void txtName_TextChanged(object sender, EventArgs e)
        {
            GetEmpid();
        }
        protected void txtEmpid_TextChanged(object sender, EventArgs e)
        {
            GetEmpName();
        }
        float totalDeductedAmount = 0;

        protected void GVLoanDeducted_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                // e.Row.Cells[1].Attributes.Add("class", "Text");

                Label month = (((Label)e.Row.FindControl("Lblmonth")));
                mvalue = month.Text;


                if (mvalue.Length == 3)
                {
                    monthval = mvalue.Substring(0, 1);

                    if (monthval == "1")
                    {
                        monthval = "Jan -";
                    }
                    if (monthval == "2")
                    {
                        monthval = "Feb -";
                    }
                    if (monthval == "3")
                    {
                        monthval = "Mar -";
                    }
                    if (monthval == "4")
                    {
                        monthval = "Apr -";
                    }
                    if (monthval == "5")
                    {
                        monthval = "May -";
                    }
                    if (monthval == "6")
                    {
                        monthval = "Jun -";
                    }
                    if (monthval == "7")
                    {
                        monthval = "Jul -";
                    }
                    if (monthval == "8")
                    {
                        monthval = "Aug -";
                    }
                    if (monthval == "9")
                    {
                        monthval = "Sep -";
                    }
                }
                else
                {
                    monthval = mvalue.Substring(0, 2);

                    if (monthval == "10")
                    {
                        monthval = "Oct -";
                    }

                    if (monthval == "11")
                    {
                        monthval = "Nov -";
                    }
                    if (monthval == "12")
                    {
                        monthval = "Dec -";
                    }
                }


                if (mvalue.Length == 3)
                {
                    yearvalue = mvalue.Substring(1, 2);
                }
                else
                {

                    yearvalue = mvalue.Substring(2, 2);
                }
                ((Label)e.Row.FindControl("Lblmonth")).Text = monthval + yearvalue;


                float DeductedAmount = float.Parse(((Label)e.Row.FindControl("LblDeductedAmount")).Text);
                totalDeductedAmount += DeductedAmount;
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                ((Label)e.Row.FindControl("lblTotalDeductedAmount")).Text = totalDeductedAmount.ToString();

            }


        }


        protected void GetEmpName()
        {
            string Sqlqry = "select (empfname+' '+empmname+' '+emplname) as empname,EmpDesgn from empdetails where empid='" + txtEmpid.Text + "' ";
            DataTable dt = config.ExecuteAdaptorAsyncWithQueryParams(Sqlqry).Result;
            if (dt.Rows.Count > 0)
            {
                try
                {
                    txtName.Text = dt.Rows[0]["empname"].ToString();

                }
                catch (Exception ex)
                {
                    // MessageLabel.Text = ex.Message;
                }
            }
            else
            {
                // MessageLabel.Text = "There Is No Name For The Selected Employee";
            }


        }
        string mvalue = "";
        string monthval = "";
        string Yr = "";
        string yearvalue = "";
        //string Length = "";
        protected void GVListEmployees_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                // e.Row.Cells[1].Attributes.Add("class", "Text");

                Label month = (((Label)e.Row.FindControl("lblmonthn")));
                mvalue = month.Text.ToString();


                if (mvalue.Length == 3)
                {
                    monthval = mvalue.Substring(0, 1);

                    if (monthval == "1")
                    {
                        monthval = "Jan -";
                    }
                    if (monthval == "2")
                    {
                        monthval = "Feb -";
                    }
                    if (monthval == "3")
                    {
                        monthval = "Mar -";
                    }
                    if (monthval == "4")
                    {
                        monthval = "Apr -";
                    }
                    if (monthval == "5")
                    {
                        monthval = "May -";
                    }
                    if (monthval == "6")
                    {
                        monthval = "Jun -";
                    }
                    if (monthval == "7")
                    {
                        monthval = "Jul -";
                    }
                    if (monthval == "8")
                    {
                        monthval = "Aug -";
                    }
                    if (monthval == "9")
                    {
                        monthval = "Sep -";
                    }
                }
                else
                {
                    monthval = mvalue.Substring(0, 2);

                    if (monthval == "10")
                    {
                        monthval = "Oct -";
                    }

                    if (monthval == "11")
                    {
                        monthval = "Nov -";
                    }
                    if (monthval == "12")
                    {
                        monthval = "Dec -";
                    }
                }


                if (mvalue.Length == 3)
                {
                    yearvalue = mvalue.Substring(1, 2);
                }
                else
                {

                    yearvalue = mvalue.Substring(2, 2);
                }
                ((Label)e.Row.FindControl("lblmonth")).Text = monthval + "" + yearvalue;
                e.Row.Cells[3].Attributes.Add("class", "text");

            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                //    ((Label)e.Row.FindControl("lblTotalDeductedAmount")).Text = totalDeductedAmount.ToString();

            }


        }

        float totalissuedAmount = 0;
        protected void GVLoanIssued_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                float IssuedAmount = float.Parse(((Label)e.Row.FindControl("lblissuedAmount")).Text);
                totalissuedAmount += IssuedAmount;
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                ((Label)e.Row.FindControl("lblTotalissuedAmount")).Text = totalissuedAmount.ToString();
            }

        }
    }
}