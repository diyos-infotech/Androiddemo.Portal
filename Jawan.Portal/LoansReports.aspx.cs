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
    public partial class LoansReports : System.Web.UI.Page
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

        //protected void btn_SubmitClick(object sender, EventArgs e)
        //{
        //    LblResult.Text = "";
        //    LblResult.Visible = true;
        //    DataTable dt = null;
        //    GVLoanRecoveryReports.DataSource = dt;
        //    GVLoanRecoveryReports.DataBind();

        //    if (ddlmonth.SelectedIndex == 0)
        //    {
        //        string SqlQry = "select E.Empid,E.EmpmName,L.Loanno,L.loandt, L.LoanAmount,LD.RecAmt," +
        //         "LD.TransactionDt,L.loanamount/L.noinstalments as CuttingAmount," +

        //          "L.LoanAmount-Sum(isnull(LD.RecAmt,0)) as Balance  from  EmpDetails E Inner join EmpLoanDetails " +
        //          "LD Inner join EmpLoanMaster L on L.LoanNo=LD.LoanNo  on E.EmpId=L.EmpId";

        //        dt = SqlHelper.Instance.GetTableByQuery(SqlQry);
        //        if (dt.Rows.Count > 0)
        //        {
        //            GVLoanRecoveryReports.DataSource = dt;
        //            GVLoanRecoveryReports.DataBind();

        //            foreach (GridViewRow gvrow in GVLoanRecoveryReports.Rows)
        //            {
        //                string LoanID = gvrow.Cells[2].Text;
        //                string LoanAmount = gvrow.Cells[3].Text;

        //                string SqlqRyDueAmount = "Select Sum(isnull(RecAmt,0)) as Paid  From emploandetails Where Loanno='" + LoanID + "'";
        //                DataTable dtDueAmount = SqlHelper.Instance.GetTableByQuery(SqlqRyDueAmount);
        //                if (dtDueAmount.Rows.Count > 0)
        //                {
        //                    if (String.IsNullOrEmpty(dtDueAmount.Rows[0]["Paid"].ToString()) == false)

        //                        gvrow.Cells[7].Text = (double.Parse(LoanAmount) - double.Parse(dtDueAmount.Rows[0]["Paid"].ToString())).ToString();
        //                    else
        //                        gvrow.Cells[7].Text = LoanAmount;
        //                }
        //                else
        //                {
        //                    gvrow.Cells[7].Text = LoanAmount;
        //                }
        //            }

        //            GetTotal();
        //            return;
        //        }
        //        else
        //        {
        //            ClearAmtData();
        //            return;
        //        }

        //    }
        //    else
        //    {
        //        ClearAmtData();

        //        if (txtyear.Text.Trim().Length == 0)
        //        {
        //            LblResult.Text = "Please fill the year ";
        //            return;
        //        }

        //        if (txtyear.Text.Trim().Length != 4)
        //        {
        //            LblResult.Text = "Invalid Selected Year  ";
        //            return;
        //        }

        //        string SqlQry = "select E.Empid,E.EmpmName,L.Loanno,L.loandt, L.LoanAmount,LD.RecAmt,LD.TransactionDt" +
        //            ",L.loanamount/L.noinstalments as CuttingAmount," +

        //             "L.LoanAmount-LD.RecAmt as Balance  from  EmpDetails E Inner join EmpLoanDetails " +
        //             "LD Inner join EmpLoanMaster L on L.LoanNo=LD.LoanNo  on E.EmpId=L.EmpId and datepart(mm,LD.TransactionDt)=" + ddlmonth.SelectedIndex +
        //             " and  datepart(yy,LD.TransactionDt)="+txtyear.Text.Trim();


        //        dt = SqlHelper.Instance.GetTableByQuery(SqlQry);
        //        if (dt.Rows.Count > 0)
        //        {
        //            GVLoanRecoveryReports.DataSource = dt;
        //            GVLoanRecoveryReports.DataBind();
        //        }
        //        else
        //        {
        //            ClearAmtData();
        //            return;
        //        }
        //    }

        //}

        protected void btn_SubmitClick(object sender, EventArgs e)
        {
            DataTable dt = null;
            string sqlqry = string.Empty;

            GVLoanRecoveryReports.DataSource = dt;
            GVLoanRecoveryReports.DataBind();

            string Fromdate = string.Empty;
            string Frommonth = "";
            string FromYear = "";

            string TOdate = string.Empty;
            string TOmonth = "";
            string TOYear = "";

            string date = string.Empty;
            string month = "";
            string Year = "";

            if (ddloptions.SelectedIndex == 1)
            {
                #region for Issued Loan By Anil Reddy on 28-05-2017
                if (txtfrom.Text.Trim().Length > 0)
                {
                    Fromdate = DateTime.Parse(txtfrom.Text.Trim(), CultureInfo.GetCultureInfo("en-gb")).ToString();
                }

                Frommonth = DateTime.Parse(Fromdate).Month.ToString();
                FromYear = DateTime.Parse(Fromdate).Year.ToString();


                if (txtto.Text.Trim().Length > 0)
                {
                    TOdate = DateTime.Parse(txtto.Text.Trim(), CultureInfo.GetCultureInfo("en-gb")).ToString();
                }

                TOmonth = DateTime.Parse(TOdate).Month.ToString();
                TOYear = DateTime.Parse(TOdate).Year.ToString();

                sqlqry = "select elm.Empid,elm.LoanNo, elm.LoanAmount,sum(isnull(eld.RecAmt,0)) RecAmt,isnull(ED.EmpmName,'') EmpmName ,elm.LoanIssuedDate Loandt, " +
                             " (elm.LoanAmount-sum(isnull(eld.RecAmt,0))) as Balance from EmpLoanMaster elm " +
                             " left join EmpDetails ed on ed.EmpId=elm.EmpId " +
                             " left join EmpLoanDetails eld on eld.LoanNo=elm.LoanNo " +
                             " where LoanIssuedDate between '" + Fromdate + "' and '" + TOdate + "'  " +
                             " group by elm.Empid,elm.LoanNo, elm.LoanAmount,Ed.EmpmName,elm.LoanIssuedDate,elm.LoanDt order by elm.LoanNo ";

                dt = config.ExecuteAdaptorAsyncWithQueryParams(sqlqry).Result;
                if (dt.Rows.Count > 0)
                {
                    GVLoanRecoveryReports.DataSource = dt;
                    GVLoanRecoveryReports.DataBind();
                    lbtn_Export.Visible = true;
                    GVLoanRecoveryReports.Visible = true;
                    GvOSreport.Visible = false;
                    GVLoanDed.Visible = false;
                }
                else
                {
                    GVLoanRecoveryReports.Visible = false;
                    GvOSreport.Visible = false;
                    GVLoanDed.Visible = false;
                    ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('The Details Are Not Avaialable');", true);
                }
                #endregion for Issued Loan By Anil Reddy on 28-05-2017
            }
            if (ddloptions.SelectedIndex == 2)
            {
                #region for Ded Rep ort By Anil Reddy on 28-05-2017
                if (txtmonth.Text.Trim().Length > 0)
                {
                    date = DateTime.Parse(txtmonth.Text.Trim(), CultureInfo.GetCultureInfo("en-gb")).ToString();
                }

                month = DateTime.Parse(date).Month.ToString();
                Year = DateTime.Parse(date).Year.ToString();


                sqlqry = " select epd.Empid,(EmpFName+''+EmpMName+''+EmpLName) EmpmName,epd.LoanNo,c.ClientName,elm.LoanAmount,RecAmt,(LoanAmount-RecAmt) Balance from EmpLoanDetails epd " +
                        " inner join EmpLoanMaster elm on elm.LoanNo=epd.LoanNo" +
                        " inner join EmpDetails ed on ed.EmpId=epd.Empid" +
                        " inner join Clients c on c.ClientId=epd.ClientID" +
                        " where LoanCuttingMonth='" + month + Year.Substring(2, 2) + "' --group by LoanNo";

                dt = config.ExecuteAdaptorAsyncWithQueryParams(sqlqry).Result;
                if (dt.Rows.Count > 0)
                {
                    GVLoanDed.DataSource = dt;
                    GVLoanDed.DataBind();
                    lbtn_Export.Visible = true;
                    GVLoanDed.Visible = true;
                    GvOSreport.Visible = false;
                    GVLoanRecoveryReports.Visible = false;

                }
                else
                {
                    GvOSreport.Visible = false;
                    GVLoanRecoveryReports.Visible = false;
                    ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('The Details Are Not Avaialable');", true);
                }
                #endregion for Ded Report By Anil Reddy on 28-05-2017
            }


        }

        protected void lbtn_Export_Click(object sender, EventArgs e)
        {
            if (ddloptions.SelectedIndex == 1)
            {
                gve.Export("IsuuedLoanFReport.xls", this.GVLoanRecoveryReports);

            }
            if (ddloptions.SelectedIndex == 2)
            {
                gve.Export("LoanDeductionReport.xls", this.GVLoanDed);

            }
            if (ddloptions.SelectedIndex == 3)
            {
                gve.Export("LoanO/sReport.xls", this.GvOSreport);

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

        protected void ddloptions_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddloptions.SelectedIndex == 1)
            {
                lblFromMonth.Visible = true;
                txtfrom.Visible = true;
                lbltoMonth.Visible = true;
                txtto.Visible = true;
                lblMonth.Visible = false;
                txtmonth.Visible = false;
                btn_Submit.Visible = true;
                GVLoanDed.Visible = false;
                GvOSreport.Visible = false;
            }

            if (ddloptions.SelectedIndex == 2)
            {
                lblFromMonth.Visible = false;
                txtfrom.Visible = false;
                lbltoMonth.Visible = false;
                txtto.Visible = false;
                lblMonth.Visible = true;
                txtmonth.Visible = true;
                btn_Submit.Visible = true;
                GVLoanRecoveryReports.Visible = false;
                GvOSreport.Visible = false;
            }


            if (ddloptions.SelectedIndex == 3)
            {
                lblFromMonth.Visible = false;
                txtfrom.Visible = false;
                lbltoMonth.Visible = false;
                txtto.Visible = false;
                lblMonth.Visible = false;
                txtmonth.Visible = false;
                btn_Submit.Visible = false;


                string sqlqry = " select elm.EmpId,(isnull(EmpFName,'')+''+isnull(EmpMName,'')+''+isnull(EmpLName,'')) EmpmName, " +
                             " elm.LoanNo,case isnull(LoanIssuedDate,'01/01/1900') when '01/01/1900' then ''  else  elm.LoanIssuedDate end LoanIssuedDate " +
                             " ,elm.LoanAmount,SUM(isnull(eld.RecAmt,0)) RecAmt,(elm.LoanAmount-SUM(isnull(eld.RecAmt,0))) Balance " +
                             " from EmpLoanMaster elm " +
                             " left join EmpDetails ed on ed.EmpId=elm.Empid " +
                             " left join EmpLoanDetails eld on eld.LoanNo=elm.LoanNo " +
                             " where LoanStatus=0 group by elm.EmpId,EmpFName,EmpMName,EmpLName,elm.LoanAmount,elm.LoanNo,elm.LoanIssuedDate order by LoanNo";

                DataTable dt = config.ExecuteAdaptorAsyncWithQueryParams(sqlqry).Result;
                if (dt.Rows.Count > 0)
                {
                    GvOSreport.DataSource = dt;
                    GvOSreport.DataBind();
                    lbtn_Export.Visible = true;
                    GvOSreport.Visible = true;
                    GVLoanDed.Visible = false;
                    GVLoanRecoveryReports.Visible = false;
                }
                else
                {
                    GVLoanDed.Visible = false;
                    GVLoanRecoveryReports.Visible = false;
                    ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('The Details Are Not Avaialable');", true);
                }




            }
        }

        string mvalue = "";
        string monthval = "";
        string yearvalue = "";

        decimal LoanAmount = 0;
        decimal LoanDedAmount = 0;
        decimal Balance = 0;


        protected void GVLoanRecoveryReports_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //e.Row.Cells[0].Attributes.Add("class", "text");

                LoanAmount += decimal.Parse(e.Row.Cells[5].Text);
                LoanDedAmount += decimal.Parse(e.Row.Cells[6].Text);
                //string month = e.Row.Cells[5].Text;
                //mvalue = month.ToString();

                //#region
                //if (mvalue.Length > 0)
                //{


                //    if (mvalue.Length == 3)
                //    {
                //        monthval = mvalue.Substring(0, 1);

                //        if (monthval == "1")
                //        {
                //            monthval = "January - 20";
                //        }
                //        if (monthval == "2")
                //        {
                //            monthval = "February - 20";
                //        }
                //        if (monthval == "3")
                //        {
                //            monthval = "March - 20";
                //        }
                //        if (monthval == "4")
                //        {
                //            monthval = "April - 20";
                //        }
                //        if (monthval == "5")
                //        {
                //            monthval = "May - 20";
                //        }
                //        if (monthval == "6")
                //        {
                //            monthval = "June - 20";
                //        }
                //        if (monthval == "7")
                //        {
                //            monthval = "July - 20";
                //        }
                //        if (monthval == "8")
                //        {
                //            monthval = "August - 20";
                //        }
                //        if (monthval == "9")
                //        {
                //            monthval = "September - 20";
                //        }
                //    }
                //    else
                //    {
                //        monthval = mvalue.Substring(0, 2);

                //        if (monthval == "10")
                //        {
                //            monthval = "October - 20";
                //        }

                //        if (monthval == "11")
                //        {
                //            monthval = "November - 20";
                //        }
                //        if (monthval == "12")
                //        {
                //            monthval = "December - 20";
                //        }
                //    }

                //    if (mvalue.Length == 3)
                //    {
                //        yearvalue = mvalue.Substring(1, 2);
                //    }
                //    else
                //    {

                //        yearvalue = mvalue.Substring(2, 2);
                //    }
                //#endregion
                //    e.Row.Cells[5].Text = monthval + "" + yearvalue;
                //    if (e.Row.Cells[5].Text == "&nbs")
                //    {
                //        e.Row.Cells[5].Text = "";
                //    }
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].Text = "Total";
                e.Row.Cells[5].Text = LoanAmount.ToString();
                e.Row.Cells[6].Text = LoanDedAmount.ToString();
            }
        }

        protected void GVLoanDed_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LoanAmount += decimal.Parse(e.Row.Cells[5].Text);
                LoanDedAmount += decimal.Parse(e.Row.Cells[6].Text);
                Balance += decimal.Parse(e.Row.Cells[7].Text);

            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].Text = "Total";
                e.Row.Cells[5].Text = LoanAmount.ToString();
                e.Row.Cells[6].Text = LoanDedAmount.ToString();
                e.Row.Cells[7].Text = Balance.ToString();

            }
        }

        protected void GvOSreport_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[4].Text == "01/01/1900")
                {
                    e.Row.Cells[4].Text = "";
                }
                LoanAmount += decimal.Parse(e.Row.Cells[5].Text);
                LoanDedAmount += decimal.Parse(e.Row.Cells[6].Text);
                Balance += decimal.Parse(e.Row.Cells[7].Text);

            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].Text = "Total";
                e.Row.Cells[5].Text = LoanAmount.ToString();
                e.Row.Cells[6].Text = LoanDedAmount.ToString();
                e.Row.Cells[7].Text = Balance.ToString();

            }
        }
    }
}