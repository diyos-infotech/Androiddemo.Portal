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
    public partial class LoanDeductionReports : System.Web.UI.Page
    {
        AppConfiguration config = new AppConfiguration();
        GridViewExportUtil gve = new GridViewExportUtil();
        protected void Page_Load(object sender, EventArgs e)
        {
            lbltamttext.Visible = false;
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

                    break;

                case 3:
                    EmployeesLink.Visible = true;
                    ClientsReportsLink.Visible = true;
                    InventoryReportsLink.Visible = true;

                    EmployeesLink.Visible = true;
                    ClientsLink.Visible = true;
                    CompanyInfoLink.Visible = false;
                    InventoryLink.Visible = false;
                    ReportsLink.Visible = true;
                    SettingsLink.Visible = true;

                    break;

                case 4:
                    EmployeesLink.Visible = true;
                    ClientsReportsLink.Visible = true;
                    InventoryReportsLink.Visible = true;

                    EmployeesLink.Visible = true;
                    ClientsLink.Visible = true;
                    CompanyInfoLink.Visible = true;
                    InventoryLink.Visible = true;
                    ReportsLink.Visible = true;
                    SettingsLink.Visible = true;

                    break;
                case 5:
                    EmployeesLink.Visible = true;
                    ClientsReportsLink.Visible = true;
                    InventoryReportsLink.Visible = true;

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

        protected void btn_SubmitClick(object sender, EventArgs e)
        {
            LblResult.Text = "";
            LblResult.Visible = true;
            DataTable dt = null;
            GVLoanRecoveryReports.DataSource = dt;
            GVLoanRecoveryReports.DataBind();

            if (ddlmonth.SelectedIndex == 0)
            {
                string SqlQry = "select E.Empid,E.EmpmName,L.Loanno,L.loandt, L.LoanAmount,LD.RecAmt," +
                 "LD.TransactionDt,L.loanamount/L.noinstalments as CuttingAmount," +

                  "L.LoanAmount-Sum(isnull(LD.RecAmt,0)) as Balance  from  EmpDetails E Inner join EmpLoanDetails " +
                  "LD Inner join EmpLoanMaster L on L.LoanNo=LD.LoanNo  on E.EmpId=L.EmpId";

                dt = config.ExecuteAdaptorAsyncWithQueryParams(SqlQry).Result;
                if (dt.Rows.Count > 0)
                {
                    GVLoanRecoveryReports.DataSource = dt;
                    GVLoanRecoveryReports.DataBind();

                    foreach (GridViewRow gvrow in GVLoanRecoveryReports.Rows)
                    {
                        string LoanID = gvrow.Cells[2].Text;
                        string LoanAmount = gvrow.Cells[3].Text;

                        string SqlqRyDueAmount = "Select Sum(isnull(RecAmt,0)) as Paid  From emploandetails Where Loanno='" + LoanID + "'";
                        DataTable dtDueAmount = config.ExecuteAdaptorAsyncWithQueryParams(SqlqRyDueAmount).Result;
                        if (dtDueAmount.Rows.Count > 0)
                        {
                            if (String.IsNullOrEmpty(dtDueAmount.Rows[0]["Paid"].ToString()) == false)

                                gvrow.Cells[7].Text = (double.Parse(LoanAmount) - double.Parse(dtDueAmount.Rows[0]["Paid"].ToString())).ToString();
                            else
                                gvrow.Cells[7].Text = LoanAmount;
                        }
                        else
                        {
                            gvrow.Cells[7].Text = LoanAmount;
                        }
                    }

                    GetTotal();
                    return;
                }
                else
                {
                    ClearAmtData();
                    return;
                }

            }
            else
            {
                ClearAmtData();

                if (txtyear.Text.Trim().Length == 0)
                {
                    LblResult.Text = "Please fill the year ";
                    return;
                }

                if (txtyear.Text.Trim().Length != 4)
                {
                    LblResult.Text = "Invalid Selected Year  ";
                    return;
                }

                string SqlQry = "select E.Empid,E.EmpmName,L.Loanno,L.loandt, L.LoanAmount,LD.RecAmt,LD.TransactionDt" +
                    ",L.loanamount/L.noinstalments as CuttingAmount," +

                     "L.LoanAmount-LD.RecAmt as Balance  from  EmpDetails E Inner join EmpLoanDetails " +
                     "LD Inner join EmpLoanMaster L on L.LoanNo=LD.LoanNo  on E.EmpId=L.EmpId and datepart(mm,LD.TransactionDt)=" + ddlmonth.SelectedIndex +
                     " and  datepart(yy,LD.TransactionDt)=" + txtyear.Text.Trim();


                dt = config.ExecuteAdaptorAsyncWithQueryParams(SqlQry).Result;
                if (dt.Rows.Count > 0)
                {
                    GVLoanRecoveryReports.DataSource = dt;
                    GVLoanRecoveryReports.DataBind();


                    foreach (GridViewRow gvrow in GVLoanRecoveryReports.Rows)
                    {
                        string LoanID = gvrow.Cells[2].Text;
                        string LoanAmount = gvrow.Cells[3].Text;
                        string SqlqRyDueAmount = "Select Sum(isnull(RecAmt,0)) as Paid  From emploandetails Where Loanno='" + LoanID + "'";
                        DataTable dtDueAmount = config.ExecuteAdaptorAsyncWithQueryParams(SqlqRyDueAmount).Result;
                        if (dtDueAmount.Rows.Count > 0)
                        {
                            if (String.IsNullOrEmpty(dtDueAmount.Rows[0]["Paid"].ToString()) == false)

                                gvrow.Cells[7].Text = (double.Parse(LoanAmount) - double.Parse(dtDueAmount.Rows[0]["Paid"].ToString())).ToString();
                            else
                                gvrow.Cells[7].Text = LoanAmount;
                        }
                        else
                        {
                            gvrow.Cells[7].Text = LoanAmount;
                        }
                    }
                    GetTotal();
                    return;
                }
                else
                {
                    ClearAmtData();
                    return;
                }
            }

        }

        protected void ClearAmtData()
        {
            lbltamttext.Visible = false;
            lbltmtc.Text = "";
            lbltmtb.Text = "";
            LblResult.Text = "No record found";
        }

        protected void GetTotal()
        {
            lbltamttext.Visible = true;
            LblResult.Text = "";
            float Totalamtc = 0;
            float Totalamtb = 0;
            for (int i = 0; i < GVLoanRecoveryReports.Rows.Count; i++)
            {
                string totalcuttingamt = GVLoanRecoveryReports.Rows[i].Cells[6].Text;
                string totalbalanceamt = GVLoanRecoveryReports.Rows[i].Cells[7].Text;

                Totalamtc += float.Parse(totalcuttingamt);
                Totalamtb += float.Parse(totalbalanceamt);

            }
            lbltmtc.Text = Totalamtc.ToString();
            lbltmtb.Text = Totalamtb.ToString();


        }
    }
}