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
using System.Globalization;
using Jawan.Portal.DAL;
namespace Jawan.Portal
{
    public partial class LoanReports : System.Web.UI.Page
    {
        AppConfiguration config = new AppConfiguration();
        GridViewExportUtil gve = new GridViewExportUtil();
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            lbltmt.Text = "";
            DataTable DtDesignation;

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
                #region Begin old designation code commented
                /*string SelectQueryDesignation = "Select design from Designations  order by design";
            DtDesignation = SqlHelper.Instance.GetTableByQuery(SelectQueryDesignation);
            int DesignIndex;
            for (DesignIndex = 0; DesignIndex < DtDesignation.Rows.Count; DesignIndex++)
            {
                ddlDesignation.Items.Add(DtDesignation.Rows[DesignIndex][0].ToString());
            }*/
                #endregion End old designation code commented
            }
        }

        protected void PreviligeUsers(int previligerid)
        {
            switch (previligerid)
            {
                case 1:
                    break;
                case 2:
                    InventoryLink.Visible = false;
                    CompanyInfoLink.Visible = false;
                    ReportsLink.Visible = false;
                    SettingsLink.Visible = false;
                    break;

                case 3:
                    InventoryLink.Visible = false;
                    ReportsLink.Visible = false;
                    SettingsLink.Visible = false;
                    break;
                case 4:
                    InventoryLink.Visible = false;
                    CompanyInfoLink.Visible = false;
                    ReportsLink.Visible = false;
                    SettingsLink.Visible = false;
                    break;
                case 5:
                    InventoryLink.Visible = false;
                    CompanyInfoLink.Visible = false;
                    ReportsLink.Visible = false;
                    SettingsLink.Visible = false;
                    break;
                case 6:
                    InventoryLink.Visible = false;
                    CompanyInfoLink.Visible = false;
                    ReportsLink.Visible = false;
                    SettingsLink.Visible = false;
                    break;
                default:
                    break;
            }
        }

        protected string GetLoanType()
        {
            string loantype = string.Empty;
            switch (ddlLoanType.SelectedIndex)
            {
                case 1: loantype = " and typeofloan=0";
                    break;
                case 2: loantype = " and typeofloan=1";
                    break;
                case 3: loantype = " and typeofloan=2";
                    break;
                case 4: loantype = " and typeofloan=3";
                    break;

            }
            return loantype;
        }

        protected string GetdatesForCheck()
        {
            string Fromdate = txtStrtDate.Text.Trim();
            string Todate = txtEndDate.Text.Trim();
            return " and  EL.LoanDt Between '" + Fromdate + "' and '" + Todate + "'";
        }

        private void BindData(string ExecuteQuery)
        {
            LblResult.Text = "";
            GVListEmployees.DataSource = null;
            GVListEmployees.DataBind();
            dt = config.ExecuteAdaptorAsyncWithQueryParams(ExecuteQuery).Result;

            if (dt.Rows.Count > 0)
            {
                lbltmt.Visible = true;
                lbltamttext.Visible = true;
                GVListEmployees.DataSource = dt;
                GVListEmployees.DataBind();

                float Totalamount = 0;
                for (int i = 0; i < GVListEmployees.Rows.Count; i++)
                {
                    string tamt = ((Label)GVListEmployees.Rows[i].FindControl("lblloanamount")).Text;

                    string Empid = ((Label)GVListEmployees.Rows[i].FindControl("lblempid")).Text;
                    Label lbldueamount = (Label)GVListEmployees.Rows[i].FindControl("lbldueamount");
                    Label lblEmpDesignation = (Label)GVListEmployees.Rows[i].FindControl("lblEmpDesignation");
                    string sqlqrydueamount = "select dueamount from dueamount Where  empid='" + Empid + "'";
                    DataTable dtdueamount = config.ExecuteAdaptorAsyncWithQueryParams(sqlqrydueamount).Result;

                    if (dtdueamount.Rows.Count > 0)
                    {
                        lbldueamount.Text = dtdueamount.Rows[0][0].ToString();
                        lblEmpDesignation.Text = "InComplete";
                    }
                    else
                    {
                        lbldueamount.Text = "0.00";
                    }



                    Totalamount += float.Parse(tamt);

                }

                lbltmt.Text = Totalamount.ToString() + ".00";

                return;
            }
            else
            {
                lbltamttext.Visible = false;
                LblResult.Text = " No record found";
                return;
            }
        }

        protected void ddlDesignation_SelectedIndexChanged(object sender, EventArgs e)
        {
            // string SelectQueryForDesign = "Select * from EMPLoanMaster,empdetails where empdetails.empid= EmpLoanMaster.EmpId and EmpDetails.EmpDesgn='" + ddlDesignation.SelectedItem.Text + "'";


        }


        #region  Begin New code as on [09-11-2013]

        protected void ddlloanoperations_OnSelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddlloanoperations.SelectedIndex == 1)
            {

            }


        }


        protected void ddlissuedloans_OnSelectedIndexChanged(object sender, EventArgs e)
        {

        }


        protected void ddlempid_OnSelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlempname_OnSelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void Btn_Search_Loans_Click(object sender, EventArgs e)
        {

            #region Begin New Code for Variable Declaration as on [11-11-2013]
            var SqlQry = "";
            DataTable DtForLoans = null;
            Hashtable HtLoans = new Hashtable();
            HtLoans.Clear();
            var CaseNoofLoan = 0;
            string ProcedureName = "IMReportForEmployeeLoans";
            #endregion End New Code For Variable Declaration as on [11-11-2013]

            #region  Begin code For Validations as on [09-11-2013]

            if (ddlloanoperations.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Please Select The Loan Operations');", true);
                return;
            }

            if (ddlissuedloans.SelectedIndex == 0)
            {

                if (txtloanissue.Text.Trim().Length == 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Please Select The Month');", true);
                    return;
                }

            }

            var testDate = 0;
            #region Begin  Code For Month check Valid or Invalid as on 09-10-2013

            if (txtloanissue.Text.Trim().Length > 0)
            {
                testDate = GlobalData.Instance.CheckEnteredDate(txtloanissue.Text);
                if (testDate > 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('You Are Entered Invalid Month/Date.Date Format Should be [DD/MM/YYYY].Ex.01/01/1990');", true);
                    return;
                }
            }
            #endregion End   Code For Month check Valid or Invalid as on 09-10-2013
            #endregion end code For Validations as on [09-11-2013]

            string EntereMonth = DateTime.Parse(txtloanissue.Text.Trim(), CultureInfo.GetCultureInfo("en-gb")).ToString();

            #region Begin Code for  CASE ONE : Issued Loans &  MonthLy Wise & All Loans  as On [09-11-2013]

            if (ddlloanoperations.SelectedIndex == 0)
            {


                if (ddlloantypes.SelectedIndex == 0)
                {
                    CaseNoofLoan = ddlloantypes.SelectedIndex;
                }

                if (ddlloantypes.SelectedIndex == 1)
                {
                    CaseNoofLoan = ddlloantypes.SelectedIndex;

                }
                if (ddlloantypes.SelectedIndex == 2)
                {
                    CaseNoofLoan = ddlloantypes.SelectedIndex;

                }
                if (ddlloantypes.SelectedIndex == 3)
                {
                    CaseNoofLoan = ddlloantypes.SelectedIndex;
                }
            }
            string Month = DateTime.Parse(EntereMonth).Month.ToString();
            string year = DateTime.Parse(EntereMonth).Year.ToString();


            HtLoans.Add("@TypeofLoan", ddlloantypes.SelectedIndex);
            HtLoans.Add("@CaseNoofLoan", ddlloanoperations.SelectedIndex);
            HtLoans.Add("@selectedMonth", Month + year);

            System.Data.DataTable DtEmpLoans = config.ExecuteAdaptorAsyncWithParams(ProcedureName, HtLoans).Result;


            if (DtEmpLoans.Rows.Count > 0)
            {
                GVListEmployees.DataSource = DtEmpLoans;
                GVListEmployees.DataBind();
            }
            else
            {
                GVListEmployees.DataSource = null;
                GVListEmployees.DataBind();
            }

            #endregion End  Code for  CASE ONE : Issued Loans &  MonthLy Wise & All Loans  as On [09-11-2013]

        }

        #endregion


        protected void lbtn_Export_Click(object sender, EventArgs e)
        {
            gve.Export("AllLoansReport.xls", this.GVListEmployees);
        }

        decimal LoamAmt = 0, AmtTobededuct = 0, Amtdeduct = 0, DueAmt = 0;
        int NoOfinst = 0;
        protected void GVListEmployees_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LoamAmt += decimal.Parse(((Label)e.Row.FindControl("lblloanamount")).Text);
                NoOfinst += int.Parse(((Label)e.Row.FindControl("lblEmpLastName")).Text);
                AmtTobededuct += decimal.Parse(((Label)e.Row.FindControl("lblamounttobededucted")).Text);
                Amtdeduct += decimal.Parse(((Label)e.Row.FindControl("lblamountdeducted")).Text);
                DueAmt += decimal.Parse(((Label)e.Row.FindControl("lbldueamount")).Text);
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].Text = "Total";
                e.Row.Cells[6].Text = LoamAmt.ToString("N", CultureInfo.InvariantCulture);
                e.Row.Cells[7].Text = NoOfinst.ToString("N", CultureInfo.InvariantCulture);
                e.Row.Cells[8].Text = AmtTobededuct.ToString("N", CultureInfo.InvariantCulture);
                e.Row.Cells[9].Text = Amtdeduct.ToString("N", CultureInfo.InvariantCulture);
                e.Row.Cells[10].Text = DueAmt.ToString("N", CultureInfo.InvariantCulture);
            }
        }
    }
}