using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using KLTS.Data;
using System.Globalization;
using Jawan.Portal.DAL;
namespace Jawan.Portal
{
    public partial class ClientWiseEmployeeNetpayReports : System.Web.UI.Page
    {
        AppConfiguration config = new AppConfiguration();
        GridViewExportUtil gve = new GridViewExportUtil();
        DataTable dt;
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

                ddlcname.Items.Add("--Select--");
                LoadClientNames();

                FillClientList();
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

        protected void GetWebConfigdata()
        {
            EmpIDPrefix = GlobalData.Instance.GetEmployeeIDPrefix();
            Elength = (EmpIDPrefix.Trim().Length + 1).ToString();
            CmpIDPrefix = GlobalData.Instance.GetClientIDPrefix();
            Clength = (CmpIDPrefix.Trim().Length + 1).ToString();
        }

        protected void ddlcname_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            ClearData();

            if (ddlcname.SelectedIndex > 0)
            {
                txtmonth.Text = "";

                if (ddlcname.SelectedIndex == 1)
                {
                    ddlclientid.SelectedIndex = 1;
                    return;
                }
                FillClientid();

            }
            else
            {

                ddlclientid.SelectedIndex = 0;
            }

        }

        protected void ddlclientid_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearData();

            if (ddlclientid.SelectedIndex > 0)
            {
                if (ddlclientid.SelectedIndex == 1)
                {
                    ddlcname.SelectedIndex = 1;
                    return;
                }

                txtmonth.Text = "";
                Fillcname();

            }
            else
            {
                ddlcname.SelectedIndex = 0;
            }
        }

        protected void Fillcname()
        {
            string SqlQryForCname = "Select Clientname from Clients where clientid='" + ddlclientid.SelectedValue + "'";
            DataTable dtCname = config.ExecuteAdaptorAsyncWithQueryParams(SqlQryForCname).Result;
            if (dtCname.Rows.Count > 0)
                ddlcname.SelectedValue = dtCname.Rows[0]["Clientname"].ToString();

        }

        protected void FillClientid()
        {
            string SqlQryForCid = "Select Clientid from Clients where clientname='" + ddlcname.SelectedValue + "'";
            DataTable dtCname = config.ExecuteAdaptorAsyncWithQueryParams(SqlQryForCid).Result;
            if (dtCname.Rows.Count > 0)
                ddlclientid.SelectedValue = dtCname.Rows[0]["Clientid"].ToString();


        }

        protected void LoadClientNames()
        {
            string selectquery = "select Clientname from Clients   where clientstatus=1 order by Clientname";
            DataTable dtable = config.ExecuteAdaptorAsyncWithQueryParams(selectquery).Result;
            for (int i = 0; i < dtable.Rows.Count; i++)
            {
                ddlcname.Items.Add(dtable.Rows[i]["Clientname"].ToString());
            }

            ddlcname.Items.Insert(1, "ALL");

        }

        protected void FillClientList()
        {
            string sqlQry = "Select ClientId from Clients Order By Right(clientid,4)";
            DataTable data = config.ExecuteAdaptorAsyncWithQueryParams(sqlQry).Result;
            ddlclientid.Items.Clear();
            ddlclientid.Items.Add("--Select--");
            for (int i = 0; i < data.Rows.Count; i++)
            {
                ddlclientid.Items.Add(data.Rows[i]["ClientId"].ToString());
            }

            ddlclientid.Items.Insert(1, "ALL");
        }

        protected void Bindata(string Sqlqry)
        {
            DataTable dt = config.ExecuteAdaptorAsyncWithQueryParams(Sqlqry).Result;
            if (dt.Rows.Count > 0)
            {
                GVListEmployees.DataSource = dt;
                GVListEmployees.DataBind();
            }
            else
            {
                LblResult.Text = "There Is No Salary Details For The Selected client";
                GVListEmployees.DataSource = null;
                GVListEmployees.DataBind();
            }
        }


        protected void ClearData()
        {
            LblResult.Text = "";
            GVListEmployees.DataSource = null;
            GVListEmployees.DataBind();
        }

        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            //LblResult.Text = "";

            //if (txtmonth.Text.Trim().Length == 0)
            //{
            //    LblResult.Text = "Please Select Month";
            //    return;
            //}

            //string date = "";
            //if (txtmonth.Text.Trim().Length > 0)
            //{
            //    date = DateTime.Parse(txtmonth.Text.Trim(), CultureInfo.GetCultureInfo("en-gb")).ToString();
            //}

            //string month = DateTime.Parse(date).Month.ToString();
            //string Year = DateTime.Parse(date).Year.ToString();


            //if (ddlclientid.SelectedIndex == 0 && txtmonth.Text.Trim().Length != 0)
            //{
            //    string sqlqry = "select Clients.Clientname, Emppaysheet.Clientid, sum(basic)  as basic,sum(da) as da,sum(hra) as hra," +
            //           "sum(cca) as cca,sum(conveyance) as conveyance,sum(washallowance) as washallowance," +
            //           "sum(otherallowance) as otherallowance,sum(leaveencashamt) as leaveencashamt,sum(otamt) as otamt," +
            //           "sum(clplamt) as clplamt,sum(woamt) as woamt,sum(gross) as totalgross,sum(pf) as pf,Sum(esi)as esi,Sum(ProfTax)as ProfTax," +
            //           "sum(penalty) as penalty,sum(saladvded) as saladvded,sum(UniformDed) as UniformDed,sum(OtherDed) as OtherDed," +
            //           "sum(CanteenAdv)  as CanteenAdv,sum(TotalDeductions) as TotalDeductions,sum(ActualAmount) as ActualAmount From emppaysheet inner join  Clients on Emppaysheet.clientid=Clients.clientid and month='" +
            //           month + Year.Substring(2, 2) + "'  group by Emppaysheet.clientid,Clients.Clientname ";
            //    Bindata(sqlqry);
            //    return;
            //}
            //if (ddlclientid.SelectedIndex == 0)
            //{
            //    LblResult.Text = "Please Select Client ID/Name";
            //    return;
            //}

            //if (ddlclientid.SelectedIndex > 1)
            //{
            //    string sqlqry = "select Clients.Clientname, Emppaysheet.Clientid,sum(basic)  as basic,sum(da) as da,sum(hra) as hra," +
            //        "sum(cca) as cca,sum(conveyance) as conveyance,sum(washallowance) as washallowance," +
            //        "sum(otherallowance) as otherallowance,sum(leaveencashamt) as leaveencashamt,sum(otamt) as otamt," +
            //        "sum(clplamt) as clplamt,sum(woamt) as woamt,sum(gross) as totalgross,sum(pf) as pf,Sum(esi)as esi,Sum(ProfTax)as ProfTax," +
            //        "sum(penalty) as penalty,sum(saladvded) as saladvded,sum(UniformDed) as UniformDed,sum(OtherDed) as OtherDed," +
            //        "sum(CanteenAdv)  as CanteenAdv,sum(TotalDeductions) as TotalDeductions,sum(ActualAmount) as ActualAmount From emppaysheet inner join  Clients on Emppaysheet.clientid=Clients.clientid and month='" +
            //        month + Year.Substring(2, 2) + "' and Emppaysheet.clientid='" + ddlclientid.SelectedValue + "' group by Emppaysheet.clientid,Clients.Clientname";
            //    Bindata(sqlqry);

            //    return;
            //}

            //if (ddlclientid.SelectedIndex == 1)
            //{
            //    string sqlqry = "select Clients.Clientname, Emppaysheet.Clientid,sum(basic)  as basic,sum(da) as da,sum(hra) as hra," +
            //        "sum(cca) as cca,sum(conveyance) as conveyance,sum(washallowance) as washallowance," +
            //        "sum(otherallowance) as otherallowance,sum(leaveencashamt) as leaveencashamt,sum(otamt) as otamt," +
            //        "sum(clplamt) as clplamt,sum(woamt) as woamt,sum(gross) as totalgross,sum(pf) as pf,Sum(esi)as esi,Sum(ProfTax)as ProfTax," +
            //        "sum(penalty) as penalty,sum(saladvded) as saladvded,sum(UniformDed) as UniformDed,sum(OtherDed) as OtherDed," +
            //        "sum(CanteenAdv)  as CanteenAdv,sum(TotalDeductions) as TotalDeductions,sum(ActualAmount) as ActualAmount From emppaysheet inner join  Clients on Emppaysheet.clientid=Clients.clientid and month='" +
            //        month + Year.Substring(2, 2) + "'  group by Emppaysheet.clientid,Clients.Clientname";
            //    Bindata(sqlqry);

            //    return;
            //}
            DisplayData();

        }


        protected void lbtn_Export_Click(object sender, EventArgs e)
        {
            if (ddlclientid.SelectedIndex == 1)
            {
                gve.ExportGrid("Netpay-(" + txtmonth.Text + ")" + ".xls", hidGridView);
            }
            else
            {
                gve.ExportGrid(ddlcname.SelectedItem.Text + "(" + ddlclientid.SelectedValue + ")" + ".xls", hidGridView);
            }

        }


        float totalBasic = 0;
        float totalDA = 0;
        float totalHRA = 0;
        float totalCCA = 0;
        float totalConveyance = 0;
        float totalWashAllowance = 0;
        float totalOtheeAllowance = 0;
        float leaveencashamt = 0;
        float totalPF = 0;
        float totalESI = 0;
        float totalLEA = 0;
        float totalOTAmt = 0;
        float totalWOAmt = 0;
        float totalGross = 0;
        float totalProfTax = 0;
        float totalPenality = 0;
        float totalSalAdvDed = 0;
        float totalUniFormDed = 0;
        float totalINSDED = 0;
        float totalOtherDed = 0;
        float totalCanteenAdv = 0;
        float totalTotalDeductions = 0;
        float totalActualAmount = 0;
        float totalPFESIContribution = 0;
        float totalTDSDed =0;
        protected void DisplayData()
        {
            if (txtmonth.Text.Trim().Length == 0)
            {
                LblResult.Text = "Please Select Month";
                return;
            }

            string date = "";
            if (txtmonth.Text.Trim().Length > 0)
            {
                date = DateTime.Parse(txtmonth.Text.Trim(), CultureInfo.GetCultureInfo("en-gb")).ToString();
            }

            string month = DateTime.Parse(date).Month.ToString();
            string Year = DateTime.Parse(date).Year.ToString();
            string sqlqry = string.Empty;

            if (ddlclientid.SelectedIndex == 0 && txtmonth.Text.Trim().Length != 0)
            {
                sqlqry = "select Clients.Clientname, Emppaysheet.Clientid, sum(basic)  as basic,sum(da) as da,sum(hra) as hra," +
                      "sum(cca) as cca,sum(conveyance) as conveyance,sum(washallowance) as washallowance," +
                      "sum(otherallowance) as otherallowance,sum(leaveencashamt) as leaveencashamt,sum(otamt) as otamt," +
                      "sum(clplamt) as clplamt,sum(woamt) as woamt,sum(gross) as totalgross,sum(pf) as pf,Sum(esi)as esi,Sum(ProfTax)as ProfTax," +
                      "sum(penalty) as penalty,sum(saladvded) as saladvded,sum(UniformDed) as UniformDed,sum(OtherDed) as OtherDed,sum(TDSDed) as TDSDed," +
                      "sum(CanteenAdv)  as CanteenAdv,ISNULL(sum(PFESIContribution),0) as PFESIContribution,sum(TotalDeductions) as TotalDeductions,sum(ActualAmount) as ActualAmount From emppaysheet inner join  Clients on Emppaysheet.clientid=Clients.clientid and month='" +
                      month + Year.Substring(2, 2) + "'  group by Emppaysheet.clientid,Clients.Clientname";

                //return;
            }
            if (ddlclientid.SelectedIndex == 0)
            {
                LblResult.Text = "Please Select Client ID/Name";
                return;
            }

            if (ddlclientid.SelectedIndex > 1)
            {
                sqlqry = "select Clients.Clientname, Emppaysheet.Clientid,sum(basic)  as basic,sum(da) as da,sum(hra) as hra," +
                   "sum(cca) as cca,sum(conveyance) as conveyance,sum(washallowance) as washallowance," +
                   "sum(otherallowance) as otherallowance,sum(leaveencashamt) as leaveencashamt,sum(otamt) as otamt," +
                   "sum(clplamt) as clplamt,sum(woamt) as woamt,sum(gross) as totalgross,sum(pf) as pf,Sum(esi)as esi,Sum(ProfTax)as ProfTax," +
                   "sum(penalty) as penalty,sum(saladvded) as saladvded,sum(UniformDed) as UniformDed,sum(OtherDed) as OtherDed,sum(TDSDed) as TDSDed," +
                   "sum(CanteenAdv)  as CanteenAdv,sum(TotalDeductions) as TotalDeductions,ISNULL(sum(PFESIContribution),0) as PFESIContribution,sum(ActualAmount) as ActualAmount From emppaysheet inner join  Clients on Emppaysheet.clientid=Clients.clientid and month='" +
                   month + Year.Substring(2, 2) + "' and Emppaysheet.clientid='" + ddlclientid.SelectedValue + "' group by Emppaysheet.clientid,Clients.Clientname";


                // return;
            }

            if (ddlclientid.SelectedIndex == 1)
            {
                sqlqry = "select Clients.Clientname, Emppaysheet.Clientid,sum(basic)  as basic,sum(da) as da,sum(hra) as hra," +
                   "sum(cca) as cca,sum(conveyance) as conveyance,sum(washallowance) as washallowance," +
                   "sum(otherallowance) as otherallowance,sum(leaveencashamt) as leaveencashamt,sum(otamt) as otamt," +
                   "sum(clplamt) as clplamt,sum(woamt) as woamt,sum(gross) as totalgross,sum(pf) as pf,Sum(esi)as esi,Sum(ProfTax)as ProfTax," +
                   "sum(penalty) as penalty,sum(saladvded) as saladvded,sum(UniformDed) as UniformDed,sum(OtherDed) as OtherDed,sum(TDSDed) as TDSDed," +
                   "sum(CanteenAdv)  as CanteenAdv,ISNULL(sum(PFESIContribution),0) as PFESIContribution,sum(TotalDeductions) as TotalDeductions,sum(ActualAmount) as ActualAmount From emppaysheet inner join  Clients on Emppaysheet.clientid=Clients.clientid and month='" +
                   month + Year.Substring(2, 2) + "'  group by Emppaysheet.clientid,Clients.Clientname";


                //return;
            }

            DataTable dt = config.ExecuteAdaptorAsyncWithQueryParams(sqlqry).Result;
            if (dt.Rows.Count > 0)
            {
                GVListEmployees.DataSource = dt;
                GVListEmployees.DataBind();
                lbtn_Export.Visible = true;




                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //float actAmount = 0;
                    //string actualAmount = dt.Rows[i]["ActualAmount"].ToString();
                    //if (actualAmount.Trim().Length > 0)
                    //{
                    //    actAmount = Convert.ToSingle(actualAmount);
                    //}
                    //if (actAmount >= 0)
                    //{
                    //    totalActualamount += actAmount;
                    string strbasic = dt.Rows[i]["basic"].ToString();
                    if (strbasic.Trim().Length > 0)
                    {
                        totalBasic += Convert.ToSingle(strbasic);
                    }
                    string strda = dt.Rows[i]["da"].ToString();
                    if (strda.Trim().Length > 0)
                    {
                        totalDA += Convert.ToSingle(strda);
                    }

                    string strhra = dt.Rows[i]["hra"].ToString();
                    if (strhra.Trim().Length > 0)
                    {
                        totalHRA += Convert.ToSingle(strhra);
                    }
                    string strcca = dt.Rows[i]["cca"].ToString();
                    if (strcca.Trim().Length > 0)
                    {
                        totalCCA += Convert.ToSingle(strcca);
                    }
                    string strConveyance = dt.Rows[i]["Conveyance"].ToString();
                    if (strConveyance.Trim().Length > 0)
                    {
                        totalConveyance += Convert.ToSingle(strConveyance);
                    }
                    string strWashAllowance = dt.Rows[i]["WashAllowance"].ToString();
                    if (strWashAllowance.Trim().Length > 0)
                    {
                        totalWashAllowance += Convert.ToSingle(strWashAllowance);
                    }

                    string strleaveencashamt = dt.Rows[i]["leaveencashamt"].ToString();
                    if (strleaveencashamt.Trim().Length > 0)
                    {
                        totalLEA += Convert.ToSingle(strleaveencashamt);
                    }
                    string strOTAmt = dt.Rows[i]["OTAmt"].ToString();
                    if (strOTAmt.Trim().Length > 0)
                    {
                        totalOTAmt += Convert.ToSingle(strOTAmt);
                    }
                    string strWOAmt = dt.Rows[i]["WOAmt"].ToString();
                    if (strWOAmt.Trim().Length > 0)
                    {
                        totalWOAmt += Convert.ToSingle(strWOAmt);
                    }
                    string strtotalGross = dt.Rows[i]["totalGross"].ToString();
                    if (strtotalGross.Trim().Length > 0)
                    {
                        totalGross += Convert.ToSingle(strtotalGross);
                    }

                    string strpf = dt.Rows[i]["pf"].ToString();
                    if (strpf.Trim().Length > 0)
                    {
                        totalPF += Convert.ToSingle(strpf);
                    }


                    string stresi = dt.Rows[i]["esi"].ToString();
                    if (stresi.Trim().Length > 0)
                    {
                        totalESI += Convert.ToSingle(stresi);
                    }

                    string strProfTax = dt.Rows[i]["ProfTax"].ToString();
                    if (strProfTax.Trim().Length > 0)
                    {
                        totalProfTax += Convert.ToSingle(strProfTax);
                    }



                    string strsaladvded = dt.Rows[i]["saladvded"].ToString();
                    if (strsaladvded.Trim().Length > 0)
                    {
                        totalSalAdvDed += Convert.ToSingle(strsaladvded);
                    }

                    string strUniformDed = dt.Rows[i]["UniformDed"].ToString();
                    if (strUniformDed.Trim().Length > 0)
                    {
                        totalUniFormDed += Convert.ToSingle(strUniformDed);
                    }


                    //string strInsDed = dt.Rows[i]["InsDed"].ToString();
                    //if (strInsDed.Trim().Length > 0)
                    //{
                    //    totalINSDED += Convert.ToSingle(strInsDed);
                    //}
                    string strOtherDed = dt.Rows[i]["OtherDed"].ToString();
                    if (strOtherDed.Trim().Length > 0)
                    {
                        totalOtherDed += Convert.ToSingle(strOtherDed);
                    }

                    string strCanteenAdv = dt.Rows[i]["CanteenAdv"].ToString();
                    if (strCanteenAdv.Trim().Length > 0)
                    {
                        totalCanteenAdv += Convert.ToSingle(strCanteenAdv);
                    }

                    string strPFESIContribution = dt.Rows[i]["PFESIContribution"].ToString();
                    if (strPFESIContribution.Trim().Length > 0)
                    {
                        totalPFESIContribution += Convert.ToSingle(strPFESIContribution);
                    }

                    string strTDSDed = dt.Rows[i]["TDSDed"].ToString();
                    if (strTDSDed.Trim().Length > 0)
                    {
                        totalTDSDed   += Convert.ToSingle(strTDSDed);
                    }


                    //New code add as on 24/12/2013 by venkat

                    string strTotalDeductions = dt.Rows[i]["TotalDeductions"].ToString();
                    if (strTotalDeductions.Trim().Length > 0)
                    {
                        totalTotalDeductions += Convert.ToSingle(strTotalDeductions);
                    }

                    string stractualamount = dt.Rows[i]["actualamount"].ToString();
                    if (stractualamount.Trim().Length > 0)
                    {
                        totalActualAmount += Convert.ToSingle(stractualamount);
                    }



                }
            }



            Label lblTotalactualamount = GVListEmployees.FooterRow.FindControl("lblTotalactualamount") as Label;
            lblTotalactualamount.Text = Math.Round(totalActualAmount).ToString();





            Label lblTotalBasic = GVListEmployees.FooterRow.FindControl("lblTotalBasic") as Label;
            lblTotalBasic.Text = Math.Round(totalBasic).ToString();

            if (totalBasic > 0)
            {
                GVListEmployees.Columns[3].Visible = true;
            }
            else
            {
                GVListEmployees.Columns[3].Visible = false;

            }
            Label lblTotalDA = GVListEmployees.FooterRow.FindControl("lblTotalDA") as Label;

            lblTotalDA.Text = Math.Round(totalDA).ToString();

            if (totalDA > 0)
            {
                GVListEmployees.Columns[4].Visible = true;
            }
            else
            {
                GVListEmployees.Columns[4].Visible = false;

            }

            Label lblTotalHRA = GVListEmployees.FooterRow.FindControl("lblTotalHRA") as Label;
            lblTotalHRA.Text = Math.Round(totalHRA).ToString();

            if (totalHRA > 0)
            {
                GVListEmployees.Columns[5].Visible = true;
            }
            else
            {
                GVListEmployees.Columns[5].Visible = false;

            }



            Label lblTotalCCA = GVListEmployees.FooterRow.FindControl("lblTotalCCA") as Label;
            lblTotalCCA.Text = Math.Round(totalCCA).ToString();
            if (totalCCA > 0)
            {
                GVListEmployees.Columns[6].Visible = true;
            }
            else
            {
                GVListEmployees.Columns[6].Visible = false;

            }
            Label lblTotalConveyance = GVListEmployees.FooterRow.FindControl("lblTotalConveyance") as Label;
            lblTotalConveyance.Text = Math.Round(totalConveyance).ToString();
            if (totalConveyance > 0)
            {
                GVListEmployees.Columns[7].Visible = true;
            }
            else
            {
                GVListEmployees.Columns[7].Visible = false;

            }
            Label lbltotalWashAllowance = GVListEmployees.FooterRow.FindControl("lbltotalWashAllowance") as Label;
            lbltotalWashAllowance.Text = Math.Round(totalWashAllowance).ToString();

            if (totalWashAllowance > 0)
            {
                GVListEmployees.Columns[8].Visible = true;
            }
            else
            {
                GVListEmployees.Columns[8].Visible = false;

            }

            Label lblTotalOtherAllowance = GVListEmployees.FooterRow.FindControl("lblTotalOtherAllowance") as Label;
            lblTotalOtherAllowance.Text = Math.Round(totalOtheeAllowance).ToString();

            if (totalOtheeAllowance > 0)
            {
                GVListEmployees.Columns[9].Visible = true;
            }
            else
            {
                GVListEmployees.Columns[9].Visible = false;

            }

            Label lblTotalLeaveEncashamt = GVListEmployees.FooterRow.FindControl("lblTotalLeaveEncashamt") as Label;
            lblTotalLeaveEncashamt.Text = Math.Round(totalLEA).ToString();

            if (totalLEA > 0)
            {
                GVListEmployees.Columns[10].Visible = true;
            }
            else
            {
                GVListEmployees.Columns[10].Visible = false;

            }

            Label lblTotalOTAmt = GVListEmployees.FooterRow.FindControl("lblTotalOTAmt") as Label;
            lblTotalOTAmt.Text = Math.Round(totalOTAmt).ToString();

            if (totalOTAmt > 0)
            {
                GVListEmployees.Columns[11].Visible = true;
            }
            else
            {
                GVListEmployees.Columns[11].Visible = false;

            }

            Label lblTotalWOAmt = GVListEmployees.FooterRow.FindControl("lblTotalWOAmt") as Label;
            lblTotalWOAmt.Text = Math.Round(totalWOAmt).ToString();

            if (totalWOAmt > 0)
            {
                GVListEmployees.Columns[12].Visible = true;
            }
            else
            {
                GVListEmployees.Columns[12].Visible = false;

            }




            Label lblTotaltotalGross = GVListEmployees.FooterRow.FindControl("lblTotaltotalGross") as Label;
            lblTotaltotalGross.Text = Math.Round(totalGross).ToString();

            if (totalGross > 0)
            {
                GVListEmployees.Columns[13].Visible = true;
            }
            else
            {
                GVListEmployees.Columns[13].Visible = false;

            }
            Label lblTotalPF = GVListEmployees.FooterRow.FindControl("lblTotalPF") as Label;
            lblTotalPF.Text = Math.Round(totalPF).ToString();

            if (totalPF > 0)
            {
                GVListEmployees.Columns[14].Visible = true;
            }
            else
            {
                GVListEmployees.Columns[14].Visible = false;

            }

            Label lblTotalESI = GVListEmployees.FooterRow.FindControl("lblTotalESI") as Label;
            lblTotalESI.Text = Math.Round(totalESI).ToString();


            if (totalESI > 0)
            {
                GVListEmployees.Columns[15].Visible = true;
            }
            else
            {
                GVListEmployees.Columns[15].Visible = false;

            }

            Label lblTotalPT = GVListEmployees.FooterRow.FindControl("lblTotalPT") as Label;
            lblTotalPT.Text = Math.Round(totalProfTax).ToString();

            if (totalProfTax > 0)
            {
                GVListEmployees.Columns[16].Visible = true;
            }
            else
            {
                GVListEmployees.Columns[16].Visible = false;

            }

            Label lblTotalSALADVDED = GVListEmployees.FooterRow.FindControl("lblTotalSALADVDED") as Label;
            lblTotalSALADVDED.Text = Math.Round(totalSalAdvDed).ToString();

            if (totalSalAdvDed > 0)
            {
                GVListEmployees.Columns[17].Visible = true;
            }
            else
            {
                GVListEmployees.Columns[17].Visible = false;

            }

            Label lblTotalUniformded = GVListEmployees.FooterRow.FindControl("lblTotalUniformded") as Label;
            lblTotalUniformded.Text = Math.Round(totalUniFormDed).ToString();

            if (totalUniFormDed > 0)
            {
                GVListEmployees.Columns[18].Visible = true;
            }
            else
            {
                GVListEmployees.Columns[18].Visible = false;

            }


            //Label lblTotalInsDed = GVListEmployees.FooterRow.FindControl("lblTotalInsDed") as Label;
            //lblTotalInsDed.Text = Math.Round(totalINSDED).ToString();
            //if (totalINSDED > 0)
            //{
            //    GVListEmployees.Columns[19].Visible = true;
            //}
            //else
            //{
            //    GVListEmployees.Columns[19].Visible = false;

            //}
            Label lblTotalOtherded = GVListEmployees.FooterRow.FindControl("lblTotalOtherded") as Label;
            lblTotalOtherded.Text = Math.Round(totalOtherDed).ToString();

            if (totalOtherDed > 0)
            {
                GVListEmployees.Columns[19].Visible = true;
            }
            else
            {
                GVListEmployees.Columns[19].Visible = false;

            }

            Label lblTotalCanteenAdv = GVListEmployees.FooterRow.FindControl("lblTotalCanteenAdv") as Label;
            lblTotalCanteenAdv.Text = Math.Round(totalCanteenAdv).ToString();

            if (totalCanteenAdv > 0)
            {
                GVListEmployees.Columns[20].Visible = true;
            }
            else
            {
                GVListEmployees.Columns[20].Visible = false;

            }

            Label lblTotalPFESIContribution = GVListEmployees.FooterRow.FindControl("lblTotalPFESIContribution") as Label;
            lblTotalPFESIContribution.Text = Math.Round(totalPFESIContribution).ToString();

            if (totalPFESIContribution > 0)
            {
                GVListEmployees.Columns[21].Visible = true;
            }
            else
            {
                GVListEmployees.Columns[21].Visible = false;

            }

            Label lblTotalTDSDed = GVListEmployees.FooterRow.FindControl("lblTotalTDSDed") as Label;
            lblTotalTDSDed.Text = Math.Round(totalTDSDed ).ToString();

            if (totalTDSDed  > 0)
            {
                GVListEmployees.Columns[22].Visible = true;
            }
            else
            {
                GVListEmployees.Columns[22].Visible = false;

            }


            Label lblTotalTotalDeductions = GVListEmployees.FooterRow.FindControl("lblTotalTotalDeductions") as Label;
            lblTotalTotalDeductions.Text = Math.Round(totalTotalDeductions).ToString();

            if (totalTotalDeductions > 0)
            {
                GVListEmployees.Columns[23].Visible = true;
            }
            else
            {
                GVListEmployees.Columns[23].Visible = false;

            }


            //    if (e.Row.RowType == DataControlRowType.DataRow)
            //    {
            //        float Basic = float.Parse(((Label)e.Row.FindControl("lblBasic")).Text);
            //        totalBasic += Basic;
            //        float DA = float.Parse(((Label)e.Row.FindControl("lblDA")).Text);
            //        totalDA += DA;
            //        float HRA = float.Parse(((Label)e.Row.FindControl("lblHRA")).Text);
            //        totalHRA += HRA;
            //        float CCA = float.Parse(((Label)e.Row.FindControl("lblCCA")).Text);
            //        totalCCA += CCA;
            //        float Conveyance = float.Parse(((Label)e.Row.FindControl("lblConveyance")).Text);
            //        totalConveyance += Conveyance;
            //        float WashAllowance = float.Parse(((Label)e.Row.FindControl("lblWAs")).Text);
            //        totalWashAllowance += WashAllowance;
            //        float OtheeAllowance = float.Parse(((Label)e.Row.FindControl("lblOA")).Text);
            //        totalOtheeAllowance += OtheeAllowance;
            //        float LEA = float.Parse(((Label)e.Row.FindControl("lblLEA")).Text);
            //        totalLEA += LEA;
            //        float OTAmt = float.Parse(((Label)e.Row.FindControl("lblOTAmt")).Text);
            //        totalOTAmt+= OTAmt;
            //        float WoAmt = float.Parse(((Label)e.Row.FindControl("lblWOAmt")).Text);
            //        totalWOAmt += WoAmt;
            //        float Gross = float.Parse(((Label)e.Row.FindControl("lblOA")).Text);
            //        totalGross += Gross;
            //        float PF = float.Parse(((Label)e.Row.FindControl("lblPF")).Text);
            //        totalPF += PF;
            //        float ESI = float.Parse(((Label)e.Row.FindControl("lblESI")).Text);
            //        totalESI += ESI;
            //        float ProfTax = float.Parse(((Label)e.Row.FindControl("lblProfTax")).Text);
            //        totalProfTax += ProfTax;
            //        float Penality = float.Parse(((Label)e.Row.FindControl("lblPenalty")).Text);
            //        totalPenality += Penality;
            //        float SalAdvDed = float.Parse(((Label)e.Row.FindControl("lblsaladvded")).Text);
            //        totalSalAdvDed += SalAdvDed;
            //        float UnifromDed = float.Parse(((Label)e.Row.FindControl("lblUniformDed")).Text);
            //        totalUniFormDed += UnifromDed;
            //        float INSDED = float.Parse(((Label)e.Row.FindControl("lblinsded")).Text);
            //        totalINSDED += INSDED;
            //        float OtherDed = float.Parse(((Label)e.Row.FindControl("lblOtherDed")).Text);
            //        totalOtherDed += OtherDed;
            //        float CanteenAdv = float.Parse(((Label)e.Row.FindControl("lblCanteenAdv")).Text);
            //        totalCanteenAdv += CanteenAdv;
            //        float TotalDeductions = float.Parse(((Label)e.Row.FindControl("lblTotalDeductions")).Text);
            //        totalTotalDeductions += TotalDeductions;
            //        float ActualAmount = float.Parse(((Label)e.Row.FindControl("lblActualAmount")).Text);
            //        totalActualAmount += ActualAmount;
            //    }
            //    if (e.Row.RowType == DataControlRowType.Footer)
            //    {
            //        ((Label)e.Row.FindControl("lblTotalBasic")).Text = totalBasic.ToString();
            //        ((Label)e.Row.FindControl("lblTotalDA")).Text = totalDA.ToString();
            //        ((Label)e.Row.FindControl("lblTotalHRA")).Text = totalHRA.ToString();
            //        ((Label)e.Row.FindControl("lblTotalCCA")).Text = totalCCA.ToString();
            //        ((Label)e.Row.FindControl("lblTotalConveyance")).Text = totalConveyance.ToString();
            //        ((Label)e.Row.FindControl("lblTotalWAs")).Text = totalWashAllowance.ToString();
            //        ((Label)e.Row.FindControl("lblTotalOA")).Text = totalOtheeAllowance.ToString();
            //        ((Label)e.Row.FindControl("lblTotalLEA")).Text = totalLEA.ToString();
            //        ((Label)e.Row.FindControl("lblTotalOTAmt")).Text = totalOTAmt.ToString();
            //        ((Label)e.Row.FindControl("lblTotalWOAmt")).Text = totalWOAmt.ToString();
            //        ((Label)e.Row.FindControl("lblTotalGross")).Text = totalGross.ToString();
            //        ((Label)e.Row.FindControl("lblTotalpf")).Text = totalPF.ToString();
            //        ((Label)e.Row.FindControl("lblTotalESI")).Text = totalESI.ToString();
            //        ((Label)e.Row.FindControl("lblTotalProfTax")).Text = totalProfTax.ToString();
            //        ((Label)e.Row.FindControl("lblTotalPenalty")).Text = totalPenality.ToString();
            //        ((Label)e.Row.FindControl("lblTotalsaladvded")).Text = totalSalAdvDed.ToString();
            //        ((Label)e.Row.FindControl("lblTotalUniformDed")).Text = totalUniFormDed.ToString();
            //        ((Label)e.Row.FindControl("lblTotalInsed")).Text = totalINSDED.ToString();
            //        ((Label)e.Row.FindControl("lblTotalOtherDed")).Text = totalOtherDed.ToString();
            //        ((Label)e.Row.FindControl("lblTotalCanteenAdv")).Text = totalCanteenAdv.ToString();
            //        ((Label)e.Row.FindControl("lblTotalDeductions")).Text = totalTotalDeductions.ToString();
            //        ((Label)e.Row.FindControl("lblTotalactualamount")).Text = totalActualAmount.ToString();

            //    }
            //}
        }
        protected void GVListEmployees_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
    }
}