using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using KLTS.Data;
using System.Globalization;
using System.Collections;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Jawan.Portal.DAL;
namespace Jawan.Portal
{
    public partial class WageSheetReports : System.Web.UI.Page
    {
        AppConfiguration config = new AppConfiguration();
        GridViewExportUtil gve = new GridViewExportUtil();
        DataTable dt;
        string EmpIDPrefix = "";
        string CmpIDPrefix = "";
        string Elength = "";
        string Clength = "";
        string Zone = "";
        protected void Page_Load(object sender, EventArgs e)
        {

            try
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

                    LoadClientList();
                    LoadClientNames();

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
            EmpIDPrefix = Session["EmpIDPrefix"].ToString();
            CmpIDPrefix = Session["CmpIDPrefix"].ToString();
        }

        protected void LoadClientList()
        {
            string qry = "select Clientid from clients order by clientid ";
            DataTable dt = config.ExecuteAdaptorAsyncWithQueryParams(qry).Result;
            if (dt.Rows.Count > 0)
            {
                ddlclientid.DataValueField = "clientid";
                ddlclientid.DataTextField = "clientid";
                ddlclientid.DataSource = dt;
                ddlclientid.DataBind();
            }
            ddlclientid.Items.Insert(0, "--Select--");
            ddlclientid.Items.Insert(1, "ALL");


        }

        protected void LoadClientNames()
        {

            string qry = "select Clientid,Clientname from clients  order by clientname";
            DataTable dt = config.ExecuteAdaptorAsyncWithQueryParams(qry).Result;
            if (dt.Rows.Count > 0)
            {
                ddlcname.DataValueField = "clientid";
                ddlcname.DataTextField = "Clientname";
                ddlcname.DataSource = dt;
                ddlcname.DataBind();
            }
            ddlcname.Items.Insert(0, "--Select--");
            ddlcname.Items.Insert(1, "ALL");


        }

        protected void ddlcname_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            ClearData();
            if (ddlcname.SelectedIndex > 0)
            {
                txtmonth.Text = "";
                ddlclientid.SelectedValue = ddlcname.SelectedValue;
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
                txtmonth.Text = "";
                ddlcname.SelectedValue = ddlclientid.SelectedValue;
            }
            else
            {
                ddlcname.SelectedIndex = 0;
            }
        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            LblResult.Text = "";
            GVListEmployees.DataSource = null;
            GVListEmployees.DataBind();

            if (ddlclientid.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showlalert", "alert('Please Select Client Id/Name');", true);

                return;
            }

            if (txtmonth.Text.Trim().Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showlalert", "alert('Please Select Month');", true);

                return;
            }
            DisplayData();
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
                GVListEmployees.DataSource = null;
                GVListEmployees.DataBind();
                ScriptManager.RegisterStartupScript(this, GetType(), "showlalert", "alert('There Is No Salary Details For The Selected client');", true);

            }
        }

        protected void ClearData()
        {
            LblResult.Text = "";
            GVListEmployees.DataSource = null;
            GVListEmployees.DataBind();
            // lbtn_Export.Visible = false;
        }

        //protected void lbtn_Export_Click(object sender, EventArgs e)
        //{

        //    //if (ddlclientid.SelectedIndex == 1)
        //    //{
        //    //    string date = string.Empty;

        //    //    if (txtmonth.Text.Trim().Length > 0)
        //    //    {
        //    //        date = DateTime.Parse(txtmonth.Text.Trim(), CultureInfo.GetCultureInfo("en-gb")).ToString();
        //    //    }

        //    //    string month = DateTime.Parse(date).Month.ToString();
        //    //    string Year = DateTime.Parse(date).Year.ToString();
        //    //    string sqlqry = string.Empty;

        //    //    DataTable dtClients = GlobalData.Instance.LoadZonesOnUserID(Zone);

        //    //    var options = 1;
        //    //    string clientid = ddlclientid.SelectedValue;

        //    //    if (ddlclientid.SelectedIndex == 1)
        //    //    {
        //    //        options = 0;
        //    //        clientid = "%";
        //    //    }


        //    //    var SPName = "";
        //    //    Hashtable HTPaysheet = new Hashtable();
        //    //    SPName = "WageSheetReport";

        //    //    HTPaysheet.Add("@Zone", dtClients);
        //    //    HTPaysheet.Add("@month", month + Year.Substring(2, 2));
        //    //    HTPaysheet.Add("@Options", options);
        //    //    HTPaysheet.Add("@clientid", clientid);

        //    //    DataTable dt = SqlHelper.Instance.ExecuteStoredProcedureWithParams(SPName, HTPaysheet);

        //    //    if (dt.Rows.Count > 0)
        //    //    {
        //    //        ExporttoExcel(dt);

        //    //    }
        //    //}

        //}

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

        protected void GVListEmployees_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GVListEmployees.PageIndex = e.NewPageIndex;
        }

        public string GetMonthName()
        {
            string monthname = string.Empty;
            int payMonth = 0;
            DateTimeFormatInfo mfi = new DateTimeFormatInfo();


            DateTime date = Convert.ToDateTime(txtmonth.Text, CultureInfo.GetCultureInfo("en-gb"));
            monthname = mfi.GetMonthName(date.Month).ToString();
            //payMonth = GetMonth(monthname);

            return monthname;
        }

        public int GetMonthBasedOnSelectionDateorMonth()
        {

            var testDate = 0;
            string EnteredDate = "";

            #region Validation

            if (txtmonth.Text.Trim().Length > 0)
            {

                try
                {

                    testDate = GlobalData.Instance.CheckEnteredDate(txtmonth.Text);
                    if (testDate > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('You Are Entered Invalid  DATE.Date Format Should be [DD/MM/YYYY].Ex.01/01/1990');", true);
                        return 0;
                    }
                    EnteredDate = DateTime.Parse(txtmonth.Text, CultureInfo.GetCultureInfo("en-gb")).ToString();
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('You Are Entered Invalid  DATE.Date Format Should be [DD/MM/YYYY].Ex.01/01/1990');", true);
                    return 0;
                }
            }
            #endregion


            #region  Month Get Based on the Control Selection
            int month = 0;

            DateTime date = DateTime.Parse(txtmonth.Text, CultureInfo.GetCultureInfo("en-gb"));
            month = Timings.Instance.GetIdForEnteredMOnth(date);
            return month;


            #endregion
        }

        public string GetMonthOfYear()
        {
            string MonthYear = "";

            int month = GetMonthBasedOnSelectionDateorMonth();
            if (month.ToString().Length == 4)
            {
                MonthYear = "20" + month.ToString().Substring(2, 2);
            }
            if (month.ToString().Length == 3)
            {
                MonthYear = "20" + month.ToString().Substring(1, 2);

            }
            return MonthYear;
        }

        float totalActualamount = 0;
        float totalDuties = 0;
        float totalOts = 0;
        float totalwo = 0;
        float totalnhs = 0;
        float totalnpots = 0;
        float totaltempgross = 0;
        float totalBasic = 0;
        float totalDA = 0;
        float totalHRA = 0;
        float totalCCA = 0;
        float totalConveyance = 0;
        float totalWA = 0;
        float totalOA = 0;
        float totalGrass = 0;
        float totalOTAmount = 0;
        float totalPF = 0;
        float totalESI = 0;
        float totalProfTax = 0;
        float totalSalAdv = 0;
        float totalUniformDed = 0;
        float totalAdvDed = 0;
        float totalWCDed = 0;
        float totalCanteenAdv = 0;
        float totalLeaveEncashAmt = 0;
        float totalGratuity = 0;
        float totalBonus = 0;
        float totalnfhs = 0;
        float totalDed = 0;
        float totalOtherDed = 0;
        float totalIncentivs = 0;
        float totalWoAmt = 0;
        float totalNhsAmt = 0;
        float totalNpotsAmt = 0;
        float totalPenalty = 0;
        float totalRC = 0;
        float totalCS = 0;
        float totalOWF = 0;
        float totalSecDepDed = 0;
        float totalloanded = 0;
        float totalGenDed = 0;


        float totalAttBonus = 0;
        float totalTravelAllw = 0;
        float totalNightShiftAllw = 0;
        float totalFoodAllowance = 0;
        float totalmedicalallowance = 0;
        float totalUniformAllw = 0;

        float totalAdv4Ded = 0;
        float totalNightRoundDed = 0;
        float totalManpowerMobDed = 0;
        float totalMobileusageDed = 0;
        float totalMediClaimDed = 0;
        float totalCrisisDed = 0;
        float totalMobInstDed = 0;
        float totalTDSDed = 0;


        float totalSpecialAllowance = 0;
        float totalMobileAllowance = 0;
        float totalNPCl25Per = 0;
        float totalTransport6Per = 0;
        float totalTransport = 0;

        float totalRentDed = 0;
        float totalMedicalDed = 0;
        float totalMLWFDed = 0;
        float totalFoodDed = 0;
        float totalAddlAmount = 0;


        float totalElectricityDed = 0;
        float totalTransportDed = 0;
        float totalDccDed = 0;
        float totalLeaveDed = 0;
        float totalLicenseDed = 0;

        float totalDiv = 0;
        float totalArea = 0;
        float totalTelephoneBillDed = 0;
        float totalPFESIContribution = 0;
        
        protected void DisplayData()
        {
            if (ddlclientid.SelectedIndex > 0)
            {
                try
                {
                    string date = string.Empty;

                    if (txtmonth.Text.Trim().Length > 0)
                    {
                        date = DateTime.Parse(txtmonth.Text.Trim(), CultureInfo.GetCultureInfo("en-gb")).ToString();
                    }

                    string month = DateTime.Parse(date).Month.ToString();
                    string Year = DateTime.Parse(date).Year.ToString();
                    string sqlqry = string.Empty;

                    var options = 1;
                    string clientid = ddlclientid.SelectedValue;

                    if (ddlclientid.SelectedIndex == 1)
                    {
                        clientid = CmpIDPrefix;
                    }
                    else
                    {
                        clientid = ddlclientid.SelectedValue;
                    }



                    var SPName = "";
                    Hashtable HTPaysheet = new Hashtable();
                    SPName = "IMEEmpWageSheetReport";

                    HTPaysheet.Add("@month", month + Year.Substring(2, 2));
                    HTPaysheet.Add("@clientid", clientid);

                    DataTable dt =config.ExecuteAdaptorAsyncWithParams(SPName, HTPaysheet).Result;

                    if (dt.Rows.Count > 0)
                    {
                        GVListEmployees.DataSource = dt;
                        GVListEmployees.DataBind();
                        lbtn_Export.Visible = true;

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            float actAmount = 0;
                            string actualAmount = dt.Rows[i]["ActualAmount"].ToString();
                            if (actualAmount.Trim().Length > 0)
                            {
                                actAmount = Convert.ToSingle(actualAmount);
                            }
                            if (actAmount >= 0)
                            {
                                #region
                                totalActualamount += actAmount;

                                string Div = dt.Rows[i]["DivisionName"].ToString();
                                if (Div.Trim().Length > 0)
                                {
                                    totalDiv += Convert.ToSingle(Div);
                                }
                                string Area = dt.Rows[i]["Location"].ToString();
                                if (Area.Trim().Length > 0)
                                {
                                    totalArea += Convert.ToSingle(Area);
                                }

                                string duties = dt.Rows[i]["NoOfDuties"].ToString();
                                if (duties.Trim().Length > 0)
                                {
                                    totalDuties += Convert.ToSingle(duties);
                                }
                                string ots = dt.Rows[i]["OTs"].ToString();
                                if (ots.Trim().Length > 0)
                                {
                                    totalOts += Convert.ToSingle(ots);
                                }

                                string wos = dt.Rows[i]["wo"].ToString();
                                if (wos.Trim().Length > 0)
                                {
                                    totalwo += Convert.ToSingle(wos);
                                }
                                string nhs = dt.Rows[i]["nhs"].ToString();
                                if (nhs.Trim().Length > 0)
                                {
                                    totalnhs += Convert.ToSingle(nhs);
                                }
                                string npots = dt.Rows[i]["npots"].ToString();
                                if (npots.Trim().Length > 0)
                                {
                                    totalnpots += Convert.ToSingle(npots);
                                }
                                string ntempgross = dt.Rows[i]["tempgross"].ToString();
                                if (ntempgross.Trim().Length > 0)
                                {
                                    totaltempgross += Convert.ToSingle(ntempgross);
                                }

                                string strBasic = dt.Rows[i]["Basic"].ToString();
                                if (strBasic.Trim().Length > 0)
                                {
                                    totalBasic += Convert.ToSingle(strBasic);
                                }
                                string strDA = dt.Rows[i]["DA"].ToString();
                                if (strDA.Trim().Length > 0)
                                {
                                    totalDA += Convert.ToSingle(strDA);
                                }
                                string strhHRA = dt.Rows[i]["HRA"].ToString();
                                if (strhHRA.Trim().Length > 0)
                                {
                                    totalHRA += Convert.ToSingle(strhHRA);
                                }
                                string strCCA = dt.Rows[i]["CCA"].ToString();
                                if (strCCA.Trim().Length > 0)
                                {
                                    totalCCA += Convert.ToSingle(strCCA);
                                }
                                string strConveyance = dt.Rows[i]["Conveyance"].ToString();
                                if (strConveyance.Trim().Length > 0)
                                {
                                    totalConveyance += Convert.ToSingle(strConveyance);
                                }
                                string strWA = dt.Rows[i]["WashAllowance"].ToString();
                                if (strWA.Trim().Length > 0)
                                {
                                    totalWA += Convert.ToSingle(strWA);
                                }
                                string strOA = dt.Rows[i]["OtherAllowance"].ToString();
                                if (strOA.Trim().Length > 0)
                                {
                                    totalOA += Convert.ToSingle(strOA);
                                }

                                string strLeaveEncashAmt = dt.Rows[i]["LeaveEncashAmt"].ToString();
                                if (strCCA.Trim().Length > 0)
                                {
                                    totalLeaveEncashAmt += Convert.ToSingle(strLeaveEncashAmt);
                                }
                                string strGratuity = dt.Rows[i]["Gratuity"].ToString();
                                if (strGratuity.Trim().Length > 0)
                                {
                                    totalGratuity += Convert.ToSingle(strGratuity);
                                }
                                string strBonus = dt.Rows[i]["Bonus"].ToString();
                                if (strBonus.Trim().Length > 0)
                                {
                                    totalBonus += Convert.ToSingle(strBonus);
                                }
                                string strNfhs = dt.Rows[i]["Nfhs"].ToString();
                                if (strNfhs.Trim().Length > 0)
                                {
                                    totalnfhs += Convert.ToSingle(strNfhs);
                                }

                                string strAddlAmount = dt.Rows[i]["AddlAmount"].ToString();
                                if (strAddlAmount.Trim().Length > 0)
                                {
                                    totalAddlAmount += Convert.ToSingle(strAddlAmount);
                                }

                                string strGross = dt.Rows[i]["Gross"].ToString();
                                if (strGross.Trim().Length > 0)
                                {
                                    totalGrass += Convert.ToSingle(strGross);
                                }


                                string strIncentivs = dt.Rows[i]["Incentivs"].ToString();
                                if (strIncentivs.Trim().Length > 0)
                                {
                                    totalIncentivs += Convert.ToSingle(strIncentivs);
                                }

                                string strOTAmount = dt.Rows[i]["OTAmt"].ToString();
                                if (strOTAmount.Trim().Length > 0)
                                {
                                    totalOTAmount += Convert.ToSingle(strOTAmount);
                                }
                                string strSpecialAllowance = dt.Rows[i]["SpecialAllowance"].ToString();
                                if (strSpecialAllowance.Trim().Length > 0)
                                {
                                    totalSpecialAllowance += Convert.ToSingle(strSpecialAllowance);
                                }
                                string strMobileAllowance = dt.Rows[i]["MobileAllw"].ToString();
                                if (strMobileAllowance.Trim().Length > 0)
                                {
                                    totalMobileAllowance += Convert.ToSingle(strMobileAllowance);
                                }
                                string strNPCl25Per = dt.Rows[i]["pay1"].ToString();
                                if (strNPCl25Per.Trim().Length > 0)
                                {
                                    totalNPCl25Per += Convert.ToSingle(strNPCl25Per);
                                }
                                string strTransport6Per = dt.Rows[i]["pay2"].ToString();
                                if (strTransport6Per.Trim().Length > 0)
                                {
                                    totalTransport6Per += Convert.ToSingle(strTransport6Per);
                                }
                                string strTransport = dt.Rows[i]["pay3"].ToString();
                                if (strTransport.Trim().Length > 0)
                                {
                                    totalTransport += Convert.ToSingle(strTransport);
                                }


                                string strPF = dt.Rows[i]["PF"].ToString();
                                if (strPF.Trim().Length > 0)
                                {
                                    totalPF += Convert.ToSingle(strPF);
                                }
                                string strESI = dt.Rows[i]["ESI"].ToString();
                                if (strESI.Trim().Length > 0)
                                {
                                    totalESI += Convert.ToSingle(strESI);
                                }
                                string strProfTax = dt.Rows[i]["ProfTax"].ToString();
                                if (strProfTax.Trim().Length > 0)
                                {
                                    totalProfTax += Convert.ToSingle(strProfTax);
                                }

                                string strSalAdv = dt.Rows[i]["SalAdvDed"].ToString();
                                if (strSalAdv.Trim().Length > 0)
                                {
                                    totalSalAdv += Convert.ToSingle(strSalAdv);
                                }

                                string strAdvDed = dt.Rows[i]["ADVDed"].ToString();
                                if (strAdvDed.Trim().Length > 0)
                                {
                                    totalAdvDed += Convert.ToSingle(strAdvDed);
                                }

                                string strWCDed = dt.Rows[i]["WCDed"].ToString();
                                if (strWCDed.Trim().Length > 0)
                                {
                                    totalWCDed += Convert.ToSingle(strWCDed);
                                }


                                string strUniformDed = dt.Rows[i]["UniformDed"].ToString();
                                if (strUniformDed.Trim().Length > 0)
                                {
                                    totalUniformDed += Convert.ToSingle(strUniformDed);
                                }


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
                                string strRentDed = dt.Rows[i]["atmDed"].ToString();
                                if (strRentDed.Trim().Length > 0)
                                {
                                    totalRentDed += Convert.ToSingle(strRentDed);
                                }
                                string strMedicalDed = dt.Rows[i]["MedicalDed"].ToString();
                                if (strMedicalDed.Trim().Length > 0)
                                {
                                    totalMedicalDed += Convert.ToSingle(strMedicalDed);
                                }
                                string strMLWFDed = dt.Rows[i]["MLWFDed"].ToString();
                                if (strMLWFDed.Trim().Length > 0)
                                {
                                    totalMLWFDed += Convert.ToSingle(strMLWFDed);
                                }
                                string strFoodDed = dt.Rows[i]["FoodDed"].ToString();
                                if (strFoodDed.Trim().Length > 0)
                                {
                                    totalFoodDed += Convert.ToSingle(strFoodDed);
                                }

                                string strElectricityDed = dt.Rows[i]["IDCardDed"].ToString();
                                if (strElectricityDed.Trim().Length > 0)
                                {
                                    totalElectricityDed += Convert.ToSingle(strElectricityDed);
                                }


                                string strTransportDed = dt.Rows[i]["RentDed1"].ToString();
                                if (strTransportDed.Trim().Length > 0)
                                {
                                    totalTransportDed += Convert.ToSingle(strTransportDed);
                                }
                                string strDccDed = dt.Rows[i]["Finesded1"].ToString();
                                if (strDccDed.Trim().Length > 0)
                                {
                                    totalDccDed += Convert.ToSingle(strDccDed);
                                }


                                string strLeaveDed = dt.Rows[i]["LeaveDed"].ToString();
                                if (strLeaveDed.Trim().Length > 0)
                                {
                                    totalLeaveDed += Convert.ToSingle(strLeaveDed);
                                }
                                string strLicenseDed = dt.Rows[i]["LicenseDed"].ToString();
                                if (strLicenseDed.Trim().Length > 0)
                                {
                                    totalLicenseDed += Convert.ToSingle(strLicenseDed);
                                }

                                //
                                string strAdv4Ded = dt.Rows[i]["Adv4Ded"].ToString();
                                if (strAdv4Ded.Trim().Length > 0)
                                {
                                    totalAdv4Ded += Convert.ToSingle(strAdv4Ded);
                                }
                                string strNightRoundDed = dt.Rows[i]["Extra"].ToString();
                                if (strNightRoundDed.Trim().Length > 0)
                                {
                                    totalNightRoundDed += Convert.ToSingle(strNightRoundDed);
                                }

                                string strManpowerMobDed = dt.Rows[i]["ManpowerMobDed"].ToString();
                                if (strManpowerMobDed.Trim().Length > 0)
                                {
                                    totalManpowerMobDed += Convert.ToSingle(strManpowerMobDed);
                                }

                                string strMobileusageDed = dt.Rows[i]["MobileusageDed"].ToString();
                                if (strMobileusageDed.Trim().Length > 0)
                                {
                                    totalMobileusageDed += Convert.ToSingle(strMobileusageDed);
                                }

                                string strMediClaimDed = dt.Rows[i]["MediClaimDed"].ToString();
                                if (strMediClaimDed.Trim().Length > 0)
                                {
                                    totalMediClaimDed += Convert.ToSingle(strMediClaimDed);
                                }

                                string strCrisisDed = dt.Rows[i]["CrisisDed"].ToString();
                                if (strCrisisDed.Trim().Length > 0)
                                {
                                    totalCrisisDed += Convert.ToSingle(strCrisisDed);
                                }

                                string strMobInstDed = dt.Rows[i]["MobInstDed"].ToString();
                                if (strMobInstDed.Trim().Length > 0)
                                {
                                    totalMobInstDed += Convert.ToSingle(strMobInstDed);
                                }

                                string strTDSDed = dt.Rows[i]["TDSDed"].ToString();
                                if (strTDSDed.Trim().Length > 0)
                                {
                                    totalTDSDed += Convert.ToSingle(strTDSDed);
                                }

                                //




                                string strDed = dt.Rows[i]["TotalDeductions"].ToString();
                                if (strDed.Trim().Length > 0)
                                {
                                    totalDed += Convert.ToSingle(strDed);
                                }



                                string strWoAmt = dt.Rows[i]["WOAmt"].ToString();
                                if (strWoAmt.Trim().Length > 0)
                                {
                                    totalWoAmt += Convert.ToSingle(strWoAmt);
                                }

                                string strNhsAmt = dt.Rows[i]["Nhsamt"].ToString();
                                if (strNhsAmt.Trim().Length > 0)
                                {
                                    totalNhsAmt += Convert.ToSingle(strNhsAmt);
                                }

                                string strNpotsAmt = dt.Rows[i]["Npotsamt"].ToString();
                                if (strNpotsAmt.Trim().Length > 0)
                                {
                                    totalNpotsAmt += Convert.ToSingle(strNpotsAmt);
                                }

                                string strPenalty = dt.Rows[i]["Penalty"].ToString();
                                if (strPenalty.Trim().Length > 0)
                                {
                                    totalPenalty += Convert.ToSingle(strPenalty);
                                }

                                string strRC = dt.Rows[i]["RC"].ToString();
                                if (strRC.Trim().Length > 0)
                                {
                                    totalRC += Convert.ToSingle(strRC);
                                }

                                string strCS = dt.Rows[i]["CS"].ToString();
                                if (strCS.Trim().Length > 0)
                                {
                                    totalCS += Convert.ToSingle(strCS);
                                }

                                string strOWF = dt.Rows[i]["OWF"].ToString();
                                if (strOWF.Trim().Length > 0)
                                {
                                    totalOWF += Convert.ToSingle(strOWF);
                                }

                                string strSecDep = dt.Rows[i]["SecurityDepDed"].ToString();
                                if (strSecDep.Trim().Length > 0)
                                {
                                    totalSecDepDed += Convert.ToSingle(strSecDep);
                                }

                                string strLoanDed = dt.Rows[i]["LoanDed"].ToString();
                                if (strLoanDed.Trim().Length > 0)
                                {
                                    totalloanded += Convert.ToSingle(strLoanDed);
                                }

                                string strGeneralDed = dt.Rows[i]["GeneralDed"].ToString();
                                if (strGeneralDed.Trim().Length > 0)
                                {
                                    totalGenDed += Convert.ToSingle(strGeneralDed);
                                }

                                string strAttBonus = dt.Rows[i]["AttBonus"].ToString();
                                if (strAttBonus.Trim().Length > 0)
                                {
                                    totalAttBonus += Convert.ToSingle(strAttBonus);
                                }

                                string strTravelAllw = dt.Rows[i]["TravelAllw"].ToString();
                                if (strTravelAllw.Trim().Length > 0)
                                {
                                    totalTravelAllw += Convert.ToSingle(strTravelAllw);
                                }

                                string strNightShiftAllw = dt.Rows[i]["PerformanceAllw"].ToString();
                                if (strNightShiftAllw.Trim().Length > 0)
                                {
                                    totalNightShiftAllw += Convert.ToSingle(strNightShiftAllw);
                                }

                                string strFoodAllowance = dt.Rows[i]["FoodAllowance"].ToString();
                                if (strFoodAllowance.Trim().Length > 0)
                                {
                                    totalFoodAllowance += Convert.ToSingle(strFoodAllowance);
                                }

                                string strmedicalallowance = dt.Rows[i]["medicalallowance"].ToString();
                                if (strmedicalallowance.Trim().Length > 0)
                                {
                                    totalmedicalallowance += Convert.ToSingle(strmedicalallowance);
                                }

                                string strUniformAllw = dt.Rows[i]["UniformAllw"].ToString();
                                if (strUniformAllw.Trim().Length > 0)
                                {
                                    totalUniformAllw += Convert.ToSingle(strUniformAllw);
                                }

                                string strTelephoneBillDed = dt.Rows[i]["TelephoneBillDed"].ToString();
                                if (strTelephoneBillDed.Trim().Length > 0)
                                {
                                    totalTelephoneBillDed += Convert.ToSingle(strTelephoneBillDed);
                                }
                                string strPFESIContribution = dt.Rows[i]["PFESIContribution"].ToString();
                                if (strPFESIContribution.Trim().Length > 0)
                                {
                                    totalPFESIContribution += Convert.ToSingle(strPFESIContribution);
                                }

                                #endregion
                            }
                        }
                        #region for total

                        Label tot = GVListEmployees.FooterRow.FindControl("lblTotalNetAmount") as Label;
                        tot.Text = Math.Round(totalActualamount).ToString();

                        Label lblTotalDuties = GVListEmployees.FooterRow.FindControl("lblTotalDuties") as Label;
                        lblTotalDuties.Text = Math.Round(totalDuties).ToString();


                        Label lblTotalAreaName = GVListEmployees.FooterRow.FindControl("lblTotalAreaName") as Label;
                        lblTotalAreaName.Text = Math.Round(totalArea).ToString();

                        if (totalArea > 0)
                        {
                            GVListEmployees.Columns[3].Visible = true;
                        }
                        else
                        {
                            GVListEmployees.Columns[3].Visible = false;

                        }
                        Label lblTotalZoneName = GVListEmployees.FooterRow.FindControl("lblTotalZoneName") as Label;
                        lblTotalZoneName.Text = Math.Round(totalDiv).ToString();

                        if (totalDiv > 0)
                        {
                            GVListEmployees.Columns[4].Visible = true;
                        }
                        else
                        {
                            GVListEmployees.Columns[4].Visible = false;
                        }


                        Label lblTotalOTs = GVListEmployees.FooterRow.FindControl("lblTotalOTs") as Label;
                        lblTotalOTs.Text = Math.Round(totalOts).ToString();

                        if (totalOts > 0)
                        {
                            GVListEmployees.Columns[11].Visible = true;
                        }
                        else
                        {
                            GVListEmployees.Columns[11].Visible = false;

                        }
                        Label lblTotalwos = GVListEmployees.FooterRow.FindControl("lblTotalwos") as Label;

                        lblTotalwos.Text = Math.Round(totalwo).ToString();

                        if (totalwo > 0)
                        {
                            GVListEmployees.Columns[12].Visible = true;
                        }
                        else
                        {
                            GVListEmployees.Columns[12].Visible = false;

                        }

                        Label lblTotalNhs = GVListEmployees.FooterRow.FindControl("lblTotalNhs") as Label;
                        lblTotalNhs.Text = Math.Round(totalnhs).ToString();

                        if (totalnhs > 0)
                        {
                            GVListEmployees.Columns[13].Visible = true;
                        }
                        else
                        {
                            GVListEmployees.Columns[13].Visible = false;

                        }



                        Label lblTotalNpots = GVListEmployees.FooterRow.FindControl("lblTotalNpots") as Label;
                        lblTotalNpots.Text = Math.Round(totalnpots).ToString();

                        if (totalnpots > 0)
                        {
                            GVListEmployees.Columns[14].Visible = true;
                        }
                        else
                        {
                            GVListEmployees.Columns[14].Visible = false;

                        }

                        Label lblTotaltempgross = GVListEmployees.FooterRow.FindControl("lblTotaltempgross") as Label;
                        lblTotaltempgross.Text = Math.Round(totaltempgross).ToString();
                        if (totaltempgross > 0)
                        {
                            GVListEmployees.Columns[15].Visible = true;
                        }
                        else
                        {
                            GVListEmployees.Columns[15].Visible = false;

                        }
                        Label lblTotalBasic = GVListEmployees.FooterRow.FindControl("lblTotalBasic") as Label;
                        lblTotalBasic.Text = Math.Round(totalBasic).ToString();
                        if (totalBasic > 0)
                        {
                            GVListEmployees.Columns[16].Visible = true;
                        }
                        else
                        {
                            GVListEmployees.Columns[16].Visible = false;

                        }
                        Label lblTotalDA = GVListEmployees.FooterRow.FindControl("lblTotalDA") as Label;
                        lblTotalDA.Text = Math.Round(totalDA).ToString();

                        if (totalDA > 0)
                        {
                            GVListEmployees.Columns[17].Visible = true;
                        }
                        else
                        {
                            GVListEmployees.Columns[17].Visible = false;

                        }

                        Label lblTotalHRA = GVListEmployees.FooterRow.FindControl("lblTotalHRA") as Label;
                        lblTotalHRA.Text = Math.Round(totalHRA).ToString();

                        if (totalHRA > 0)
                        {
                            GVListEmployees.Columns[18].Visible = true;
                        }
                        else
                        {
                            GVListEmployees.Columns[18].Visible = false;

                        }

                        Label lblTotalCCA = GVListEmployees.FooterRow.FindControl("lblTotalCCA") as Label;
                        lblTotalCCA.Text = Math.Round(totalCCA).ToString();

                        if (totalCCA > 0)
                        {
                            GVListEmployees.Columns[19].Visible = true;
                        }
                        else
                        {
                            GVListEmployees.Columns[19].Visible = false;

                        }

                        Label lblTotalConveyance = GVListEmployees.FooterRow.FindControl("lblTotalConveyance") as Label;
                        lblTotalConveyance.Text = Math.Round(totalConveyance).ToString();

                        if (totalConveyance > 0)
                        {
                            GVListEmployees.Columns[20].Visible = true;
                        }
                        else
                        {
                            GVListEmployees.Columns[20].Visible = false;

                        }

                        Label lblTotalWA = GVListEmployees.FooterRow.FindControl("lblTotalWA") as Label;
                        lblTotalWA.Text = Math.Round(totalWA).ToString();

                        if (totalWA > 0)
                        {
                            GVListEmployees.Columns[21].Visible = true;
                        }
                        else
                        {
                            GVListEmployees.Columns[21].Visible = false;

                        }

                        Label lblTotalOA = GVListEmployees.FooterRow.FindControl("lblTotalOA") as Label;
                        lblTotalOA.Text = Math.Round(totalOA).ToString();

                        if (totalOA > 0)
                        {
                            GVListEmployees.Columns[22].Visible = true;
                        }
                        else
                        {
                            GVListEmployees.Columns[22].Visible = false;

                        }

                        Label lblTotalSpecialAllowance = GVListEmployees.FooterRow.FindControl("lblTotalSpecialAllowance") as Label;
                        lblTotalSpecialAllowance.Text = Math.Round(totalSpecialAllowance).ToString();

                        if (totalSpecialAllowance > 0)
                        {
                            GVListEmployees.Columns[23].Visible = true;
                        }
                        else
                        {
                            GVListEmployees.Columns[23].Visible = false;

                        }

                        Label lblTotalUniformAllw = GVListEmployees.FooterRow.FindControl("lblTotalUniformAllw") as Label;
                        lblTotalUniformAllw.Text = Math.Round(totalUniformAllw).ToString();

                        if (totalUniformAllw > 0)
                        {
                            GVListEmployees.Columns[24].Visible = true;
                        }
                        else
                        {
                            GVListEmployees.Columns[24].Visible = false;
                        }


                        Label lblTotalMobileAllowance = GVListEmployees.FooterRow.FindControl("lblTotalMobileAllowance") as Label;
                        lblTotalMobileAllowance.Text = Math.Round(totalMobileAllowance).ToString();

                        if (totalMobileAllowance > 0)
                        {
                            GVListEmployees.Columns[25].Visible = true;
                        }
                        else
                        {
                            GVListEmployees.Columns[25].Visible = false;

                        }


                        Label lblTotalmedicalallowance = GVListEmployees.FooterRow.FindControl("lblTotalmedicalallowance") as Label;
                        lblTotalmedicalallowance.Text = Math.Round(totalmedicalallowance).ToString();

                        if (totalmedicalallowance > 0)
                        {
                            GVListEmployees.Columns[26].Visible = true;
                        }
                        else
                        {
                            GVListEmployees.Columns[26].Visible = false;
                        }



                        Label lblTotalFoodAllowance = GVListEmployees.FooterRow.FindControl("lblTotalFoodAllowance") as Label;
                        lblTotalFoodAllowance.Text = Math.Round(totalFoodAllowance).ToString();

                        if (totalFoodAllowance > 0)
                        {
                            GVListEmployees.Columns[27].Visible = true;
                        }
                        else
                        {
                            GVListEmployees.Columns[27].Visible = false;
                        }

                        Label lblTotalNightShiftAllw = GVListEmployees.FooterRow.FindControl("lblTotalNightShiftAllw") as Label;
                        lblTotalNightShiftAllw.Text = Math.Round(totalNightShiftAllw).ToString();

                        if (totalNightShiftAllw > 0)
                        {
                            GVListEmployees.Columns[28].Visible = true;
                        }
                        else
                        {
                            GVListEmployees.Columns[28].Visible = false;
                        }


                        Label lblTotalTravelAllw = GVListEmployees.FooterRow.FindControl("lblTotalTravelAllw") as Label;
                        lblTotalTravelAllw.Text = Math.Round(totalTravelAllw).ToString();

                        if (totalTravelAllw > 0)
                        {
                            GVListEmployees.Columns[29].Visible = true;
                        }
                        else
                        {
                            GVListEmployees.Columns[29].Visible = false;
                        }

                        Label lblTotalLeaveEncashAmt = GVListEmployees.FooterRow.FindControl("lblTotalLeaveEncashAmt") as Label;
                        lblTotalLeaveEncashAmt.Text = Math.Round(totalLeaveEncashAmt).ToString();

                        if (totalLeaveEncashAmt > 0)
                        {
                            GVListEmployees.Columns[30].Visible = true;
                        }
                        else
                        {
                            GVListEmployees.Columns[30].Visible = false;

                        }
                        Label lblTotalGratuity = GVListEmployees.FooterRow.FindControl("lblTotalGratuity") as Label;
                        lblTotalGratuity.Text = Math.Round(totalGratuity).ToString();

                        if (totalGratuity > 0)
                        {
                            GVListEmployees.Columns[31].Visible = true;
                        }
                        else
                        {
                            GVListEmployees.Columns[31].Visible = false;

                        }

                        Label lblTotalBonus = GVListEmployees.FooterRow.FindControl("lblTotalBonus") as Label;
                        lblTotalBonus.Text = Math.Round(totalBonus).ToString();


                        if (totalBonus > 0)
                        {
                            GVListEmployees.Columns[32].Visible = true;
                        }
                        else
                        {
                            GVListEmployees.Columns[32].Visible = false;

                        }

                        Label lblTotalNfhs = GVListEmployees.FooterRow.FindControl("lblTotalNfhs") as Label;
                        lblTotalNfhs.Text = Math.Round(totalnfhs).ToString();

                        if (totalnfhs > 0)
                        {
                            GVListEmployees.Columns[33].Visible = true;
                        }
                        else
                        {
                            GVListEmployees.Columns[33].Visible = false;

                        }

                        Label lblTotalrc = GVListEmployees.FooterRow.FindControl("lblTotalrc") as Label;
                        lblTotalrc.Text = Math.Round(totalRC).ToString();

                        if (totalRC > 0)
                        {
                            GVListEmployees.Columns[34].Visible = true;
                        }
                        else
                        {
                            GVListEmployees.Columns[34].Visible = false;

                        }

                        Label lblTotalcs = GVListEmployees.FooterRow.FindControl("lblTotalcs") as Label;
                        lblTotalcs.Text = Math.Round(totalCS).ToString();

                        if (totalCS > 0)
                        {
                            GVListEmployees.Columns[35].Visible = true;
                        }
                        else
                        {
                            GVListEmployees.Columns[35].Visible = false;

                        }


                        Label lblTotalIncentivs = GVListEmployees.FooterRow.FindControl("lblTotalIncentivs") as Label;
                        lblTotalIncentivs.Text = Math.Round(totalIncentivs).ToString();
                        if (totalIncentivs > 0)
                        {
                            GVListEmployees.Columns[36].Visible = true;
                        }
                        else
                        {
                            GVListEmployees.Columns[36].Visible = false;

                        }
                        Label lblTotalWOAmount = GVListEmployees.FooterRow.FindControl("lblTotalWOAmount") as Label;
                        lblTotalWOAmount.Text = Math.Round(totalWoAmt).ToString();

                        if (totalWoAmt > 0)
                        {
                            GVListEmployees.Columns[37].Visible = true;
                        }
                        else
                        {
                            GVListEmployees.Columns[37].Visible = false;

                        }

                        Label lblTotalNhsAmount = GVListEmployees.FooterRow.FindControl("lblTotalNhsAmount") as Label;
                        lblTotalNhsAmount.Text = Math.Round(totalNhsAmt).ToString();

                        if (totalNhsAmt > 0)
                        {
                            GVListEmployees.Columns[38].Visible = true;
                        }
                        else
                        {
                            GVListEmployees.Columns[38].Visible = false;

                        }


                        Label lblTotalNpotsAmount = GVListEmployees.FooterRow.FindControl("lblTotalNpotsAmount") as Label;
                        lblTotalNpotsAmount.Text = Math.Round(totalNpotsAmt).ToString();

                        if (totalNpotsAmt > 0)
                        {
                            GVListEmployees.Columns[39].Visible = true;
                        }
                        else
                        {
                            GVListEmployees.Columns[39].Visible = false;

                        }

                        Label lblTotalAttBonus = GVListEmployees.FooterRow.FindControl("lblTotalAttBonus") as Label;
                        lblTotalAttBonus.Text = Math.Round(totalAttBonus).ToString();

                        if (totalAttBonus > 0)
                        {
                            GVListEmployees.Columns[40].Visible = true;
                        }
                        else
                        {
                            GVListEmployees.Columns[40].Visible = false;

                        }

                        Label lblTotalNPCl25Per = GVListEmployees.FooterRow.FindControl("lblTotalNPCl25Per") as Label;
                        lblTotalNPCl25Per.Text = Math.Round(totalNPCl25Per).ToString();

                        if (totalNPCl25Per > 0)
                        {
                            GVListEmployees.Columns[41].Visible = true;
                        }
                        else
                        {
                            GVListEmployees.Columns[41].Visible = false;

                        }

                        Label lblTotalTransport6Per = GVListEmployees.FooterRow.FindControl("lblTotalTransport6Per") as Label;
                        lblTotalTransport6Per.Text = Math.Round(totalTransport6Per).ToString();

                        if (totalTransport6Per > 0)
                        {
                            GVListEmployees.Columns[42].Visible = true;
                        }
                        else
                        {
                            GVListEmployees.Columns[42].Visible = false;

                        }

                        Label lblTotalTransport = GVListEmployees.FooterRow.FindControl("lblTotalTransport") as Label;
                        lblTotalTransport.Text = Math.Round(totalTransport).ToString();

                        if (totalTransport > 0)
                        {
                            GVListEmployees.Columns[43].Visible = true;
                        }
                        else
                        {
                            GVListEmployees.Columns[43].Visible = false;

                        }

                        Label lblTotalOTAmount = GVListEmployees.FooterRow.FindControl("lblTotalOTAmount") as Label;
                        lblTotalOTAmount.Text = Math.Round(totalOTAmount).ToString();

                        if (totalOTAmount > 0)
                        {
                            GVListEmployees.Columns[44].Visible = true;
                        }
                        else
                        {
                            GVListEmployees.Columns[44].Visible = false;

                        }

                        Label lbltTotalAddlAmount = GVListEmployees.FooterRow.FindControl("lbltTotalAddlAmount") as Label;
                        lbltTotalAddlAmount.Text = Math.Round(totalAddlAmount).ToString();

                        if (totalAddlAmount > 0)
                        {
                            GVListEmployees.Columns[45].Visible = true;
                        }
                        else
                        {
                            GVListEmployees.Columns[45].Visible = false;

                        }


                        Label lblTotalGross = GVListEmployees.FooterRow.FindControl("lblTotalGross") as Label;
                        lblTotalGross.Text = Math.Round(totalGrass).ToString();
                        if (totalGrass > 0)
                        {
                            GVListEmployees.Columns[46].Visible = true;
                        }
                        else
                        {
                            GVListEmployees.Columns[46].Visible = false;

                        }

                        Label lblTotalPF = GVListEmployees.FooterRow.FindControl("lblTotalPF") as Label;
                        lblTotalPF.Text = Math.Round(totalPF).ToString();

                        Label lblTotalESI = GVListEmployees.FooterRow.FindControl("lblTotalESI") as Label;
                        lblTotalESI.Text = Math.Round(totalESI).ToString();

                        Label lblTotalProfTax = GVListEmployees.FooterRow.FindControl("lblTotalProfTax") as Label;
                        lblTotalProfTax.Text = Math.Round(totalProfTax).ToString();
                        if (totalProfTax > 0)
                        {
                            GVListEmployees.Columns[49].Visible = true;
                        }
                        else
                        {
                            GVListEmployees.Columns[49].Visible = false;

                        }


                        Label lblTotalsaladv = GVListEmployees.FooterRow.FindControl("lblTotalsaladv") as Label;
                        lblTotalsaladv.Text = Math.Round(totalSalAdv).ToString();

                        if (totalSalAdv > 0)
                        {
                            GVListEmployees.Columns[50].Visible = true;
                        }
                        else
                        {
                            GVListEmployees.Columns[50].Visible = false;

                        }

                        Label lblTotaladvded = GVListEmployees.FooterRow.FindControl("lblTotaladvded") as Label;
                        lblTotaladvded.Text = Math.Round(totalAdvDed).ToString();
                        if (totalAdvDed > 0)
                        {
                            GVListEmployees.Columns[51].Visible = true;
                        }
                        else
                        {
                            GVListEmployees.Columns[51].Visible = false;

                        }

                        Label lblTotalwcded = GVListEmployees.FooterRow.FindControl("lblTotalwcded") as Label;
                        lblTotalwcded.Text = Math.Round(totalWCDed).ToString();
                        if (totalWCDed > 0)
                        {
                            GVListEmployees.Columns[52].Visible = true;
                        }
                        else
                        {
                            GVListEmployees.Columns[52].Visible = false;

                        }

                        Label lblTotalUniformDed = GVListEmployees.FooterRow.FindControl("lblTotalUniformDed") as Label;
                        lblTotalUniformDed.Text = Math.Round(totalUniformDed).ToString();

                        if (totalUniformDed > 0)
                        {
                            GVListEmployees.Columns[53].Visible = true;
                        }
                        else
                        {
                            GVListEmployees.Columns[53].Visible = false;

                        }


                        Label lblTotalOtherDed = GVListEmployees.FooterRow.FindControl("lblTotalOtherDed") as Label;
                        lblTotalOtherDed.Text = Math.Round(totalOtherDed).ToString();

                        if (totalOtherDed > 0)
                        {
                            GVListEmployees.Columns[54].Visible = true;
                        }
                        else
                        {
                            GVListEmployees.Columns[54].Visible = false;

                        }

                        Label lbltotalloanded = GVListEmployees.FooterRow.FindControl("lblTotaltotalloanded") as Label;
                        lbltotalloanded.Text = Math.Round(totalloanded).ToString();

                        if (totalloanded > 0)
                        {
                            GVListEmployees.Columns[55].Visible = true;
                        }
                        else
                        {
                            GVListEmployees.Columns[55].Visible = false;

                        }

                        Label lblTotalCanteenAdv = GVListEmployees.FooterRow.FindControl("lblTotalcantadv") as Label;
                        lblTotalCanteenAdv.Text = Math.Round(totalCanteenAdv).ToString();

                        if (totalCanteenAdv > 0)
                        {
                            GVListEmployees.Columns[56].Visible = true;
                        }
                        else
                        {
                            GVListEmployees.Columns[56].Visible = false;

                        }


                        Label lblTotalSecDepDed = GVListEmployees.FooterRow.FindControl("lblTotalSecDepDed") as Label;
                        lblTotalSecDepDed.Text = Math.Round(totalSecDepDed).ToString();


                        if (totalSecDepDed > 0)
                        {
                            GVListEmployees.Columns[57].Visible = true;
                        }
                        else
                        {
                            GVListEmployees.Columns[57].Visible = false;

                        }



                        Label lblTotalGeneralDed = GVListEmployees.FooterRow.FindControl("lblTotalGeneralDed") as Label;
                        lblTotalGeneralDed.Text = Math.Round(totalGenDed).ToString();


                        if (totalGenDed > 0)
                        {
                            GVListEmployees.Columns[58].Visible = true;
                        }
                        else
                        {
                            GVListEmployees.Columns[58].Visible = false;

                        }

                        Label lblTotalowf = GVListEmployees.FooterRow.FindControl("lblTotalowf") as Label;
                        lblTotalowf.Text = Math.Round(totalOWF).ToString();

                        if (totalOWF > 0)
                        {
                            GVListEmployees.Columns[59].Visible = true;
                        }
                        else
                        {
                            GVListEmployees.Columns[59].Visible = false;

                        }

                        Label lblTotalPenalty = GVListEmployees.FooterRow.FindControl("lblTotalPenalty") as Label;
                        lblTotalPenalty.Text = Math.Round(totalPenalty).ToString();

                        if (totalPenalty > 0)
                        {
                            GVListEmployees.Columns[60].Visible = true;
                        }
                        else
                        {
                            GVListEmployees.Columns[60].Visible = false;

                        }


                        Label lblTotalRentDed = GVListEmployees.FooterRow.FindControl("lblTotalRentDed") as Label;
                        lblTotalRentDed.Text = Math.Round(totalRentDed).ToString();

                        if (totalRentDed > 0)
                        {
                            GVListEmployees.Columns[61].Visible = true;
                        }
                        else
                        {
                            GVListEmployees.Columns[61].Visible = false;

                        }


                        Label lblTotalMedicalDed = GVListEmployees.FooterRow.FindControl("lblTotalMedicalDed") as Label;
                        lblTotalMedicalDed.Text = Math.Round(totalMedicalDed).ToString();

                        if (totalMedicalDed > 0)
                        {
                            GVListEmployees.Columns[62].Visible = true;
                        }
                        else
                        {
                            GVListEmployees.Columns[62].Visible = false;

                        }

                        Label lblTotalMLWFDed = GVListEmployees.FooterRow.FindControl("lblTotalMLWFDed") as Label;
                        lblTotalMLWFDed.Text = Math.Round(totalMLWFDed).ToString();

                        if (totalMLWFDed > 0)
                        {
                            GVListEmployees.Columns[63].Visible = true;
                        }
                        else
                        {
                            GVListEmployees.Columns[63].Visible = false;

                        }

                        Label lblTotalFoodDed = GVListEmployees.FooterRow.FindControl("lblTotalFoodDed") as Label;
                        lblTotalFoodDed.Text = Math.Round(totalFoodDed).ToString();

                        if (totalFoodDed > 0)
                        {
                            GVListEmployees.Columns[64].Visible = true;
                        }
                        else
                        {
                            GVListEmployees.Columns[64].Visible = false;

                        }


                        ///

                        Label lblTotalElectricityDed = GVListEmployees.FooterRow.FindControl("lblTotalElectricityDed") as Label;
                        lblTotalElectricityDed.Text = Math.Round(totalElectricityDed).ToString();

                        if (totalElectricityDed > 0)
                        {
                            GVListEmployees.Columns[65].Visible = true;
                        }
                        else
                        {
                            GVListEmployees.Columns[65].Visible = false;
                        }

                        Label lblTotalTransportDed = GVListEmployees.FooterRow.FindControl("lblTotalTransportDed") as Label;
                        lblTotalTransportDed.Text = Math.Round(totalTransportDed).ToString();

                        if (totalTransportDed > 0)
                        {
                            GVListEmployees.Columns[66].Visible = true;
                        }
                        else
                        {
                            GVListEmployees.Columns[66].Visible = false;
                        }


                        Label lblTotalDccDed = GVListEmployees.FooterRow.FindControl("lblTotalDccDed") as Label;
                        lblTotalDccDed.Text = Math.Round(totalDccDed).ToString();

                        if (totalDccDed > 0)
                        {
                            GVListEmployees.Columns[67].Visible = true;
                        }
                        else
                        {
                            GVListEmployees.Columns[67].Visible = false;
                        }

                        Label lblTotalLeaveDed = GVListEmployees.FooterRow.FindControl("lblTotalLeaveDed") as Label;
                        lblTotalLeaveDed.Text = Math.Round(totalLeaveDed).ToString();

                        if (totalLeaveDed > 0)
                        {
                            GVListEmployees.Columns[68].Visible = true;
                        }
                        else
                        {
                            GVListEmployees.Columns[68].Visible = false;
                        }

                        Label lblTotalLicenseDed = GVListEmployees.FooterRow.FindControl("lblTotalLicenseDed") as Label;
                        lblTotalLicenseDed.Text = Math.Round(totalLicenseDed).ToString();

                        if (totalLicenseDed > 0)
                        {
                            GVListEmployees.Columns[69].Visible = true;
                        }
                        else
                        {
                            GVListEmployees.Columns[69].Visible = false;
                        }


                        ///

                        Label lblTotalAdv4Ded = GVListEmployees.FooterRow.FindControl("lblTotalAdv4Ded") as Label;
                        lblTotalAdv4Ded.Text = Math.Round(totalAdv4Ded).ToString();
                        if (totalAdv4Ded > 0)
                        {
                            GVListEmployees.Columns[70].Visible = true;
                        }
                        else
                        {
                            GVListEmployees.Columns[70].Visible = false;

                        }

                        Label lblTotalNightRoundDed = GVListEmployees.FooterRow.FindControl("lblTotalNightRoundDed") as Label;
                        lblTotalNightRoundDed.Text = Math.Round(totalNightRoundDed).ToString();
                        if (totalNightRoundDed > 0)
                        {
                            GVListEmployees.Columns[71].Visible = true;
                        }
                        else
                        {
                            GVListEmployees.Columns[71].Visible = false;

                        }

                        Label lblTotalManpowerMobDed = GVListEmployees.FooterRow.FindControl("lblTotalManpowerMobDed") as Label;
                        lblTotalManpowerMobDed.Text = Math.Round(totalManpowerMobDed).ToString();
                        if (totalManpowerMobDed > 0)
                        {
                            GVListEmployees.Columns[72].Visible = true;
                        }
                        else
                        {
                            GVListEmployees.Columns[72].Visible = false;

                        }


                        Label lblTotalMobileusageDed = GVListEmployees.FooterRow.FindControl("lblTotalMobileusageDed") as Label;
                        lblTotalMobileusageDed.Text = Math.Round(totalMobileusageDed).ToString();
                        if (totalMobileusageDed > 0)
                        {
                            GVListEmployees.Columns[73].Visible = true;
                        }
                        else
                        {
                            GVListEmployees.Columns[73].Visible = false;

                        }

                        Label lblTotalMediClaimDed = GVListEmployees.FooterRow.FindControl("lblTotalMediClaimDed") as Label;
                        lblTotalMediClaimDed.Text = Math.Round(totalMediClaimDed).ToString();
                        if (totalMediClaimDed > 0)
                        {
                            GVListEmployees.Columns[74].Visible = true;
                        }
                        else
                        {
                            GVListEmployees.Columns[74].Visible = false;

                        }


                        Label lblTotalCrisisDed = GVListEmployees.FooterRow.FindControl("lblTotalCrisisDed") as Label;
                        lblTotalCrisisDed.Text = Math.Round(totalCrisisDed).ToString();
                        if (totalCrisisDed > 0)
                        {
                            GVListEmployees.Columns[75].Visible = true;
                        }
                        else
                        {
                            GVListEmployees.Columns[75].Visible = false;

                        }

                        //Label lblTotalMobInstDed = GVListEmployees.FooterRow.FindControl("lblTotalMobInstDed") as Label;
                        //lblTotalMobInstDed.Text = Math.Round(totalMobInstDed).ToString();
                        //if (totalMobInstDed > 0)
                        //{
                        //    GVListEmployees.Columns[76].Visible = true;
                        //}
                        //else
                        //{
                        //    GVListEmployees.Columns[76].Visible = false;

                        //}                     


                        Label lblTotalTelephoneBillDed = GVListEmployees.FooterRow.FindControl("lblTotalTelephoneBillDed") as Label;
                        lblTotalTelephoneBillDed.Text = Math.Round(totalTelephoneBillDed).ToString();
                        if (totalTelephoneBillDed > 0)
                        {
                            GVListEmployees.Columns[78].Visible = true;
                        }
                        else
                        {
                            GVListEmployees.Columns[78].Visible = false;

                        }


                        Label lblTotalPFESIContribution = GVListEmployees.FooterRow.FindControl("lblTotalPFESIContribution") as Label;
                        lblTotalPFESIContribution.Text = Math.Round(totalPFESIContribution).ToString();
                        if (totalPFESIContribution > 0)
                        {
                            GVListEmployees.Columns[79].Visible = true;
                        }
                        else
                        {
                            GVListEmployees.Columns[79].Visible = false;

                        }

                        Label lblTotalTDSDed = GVListEmployees.FooterRow.FindControl("lblTotalTDSDed") as Label;
                        lblTotalTDSDed.Text = Math.Round(totalTDSDed).ToString();
                        if (totalTDSDed > 0)
                        {
                            GVListEmployees.Columns[80].Visible = true;
                        }
                        else
                        {
                            GVListEmployees.Columns[80].Visible = false;

                        }

                        Label lblTotalDeductions = GVListEmployees.FooterRow.FindControl("lblTotalDeductions") as Label;
                        lblTotalDeductions.Text = Math.Round(totalDed).ToString();
                        if (totalDed > 0)
                        {
                            GVListEmployees.Columns[81].Visible = true;
                        }
                        else
                        {
                            GVListEmployees.Columns[81].Visible = false;

                        }

                        //New code add as on 24/12/2013 by venkat

                        #endregion


                    }
                }
                catch (Exception ex)
                {
                }
            }
        }

        protected void GVListEmployees_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[5].Attributes.Add("class", "text");
                e.Row.Cells[7].Attributes.Add("class", "text");
                e.Row.Cells[85].Attributes.Add("class", "text");
                e.Row.Cells[83].Attributes.Add("class", "text");
                e.Row.Cells[84].Attributes.Add("class", "text");
                ((Label)e.Row.FindControl("lblmonth")).Text = txtmonth.Text;
            }
        }
    }
}