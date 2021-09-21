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
using System.Globalization;
using KLTS.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Text;
using System.Collections.Generic;
using OfficeOpenXml;
using Jawan.Portal.DAL;


namespace Jawan.Portal
{
    public partial class PFDetailsReport : System.Web.UI.Page
    {
        string EmpIDPrefix = "";
        string CmpIDPrefix = "";
        string Elength = "";
        string Clength = "";
        AppConfiguration Config = new AppConfiguration();
        GridViewExportUtil GVUtill = new GridViewExportUtil();

        protected void Page_Load(object sender, EventArgs e)
        {
            GetWebConfigdata();
            LblResult.Visible = true;
            LblResult.Text = "";
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

        protected void GetWebConfigdata()
        {
            EmpIDPrefix = Session["EmpIDPrefix"].ToString();
            Elength = (EmpIDPrefix.Trim().Length + 1).ToString();
            CmpIDPrefix = Session["CmpIDPrefix"].ToString();
            Clength = (CmpIDPrefix.Trim().Length + 1).ToString();
        }

        protected void ClearGridData()
        {
            LblResult.Text = "";
            GVListOfClients.DataSource = null;
            GVListOfClients.DataBind();
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
                    EmployeesLink.Visible = true;
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
                    EmployeesLink.Visible = true;
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

        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            GVListOfClients.DataSource = null;
            GVListOfClients.DataBind();
            LblResult.Text = "";
            DataTable dt = null;
            String Sqlqry = "";

            if (txtmonth.Text.Trim().Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showlalert", "alert('Please Select Month');", true);
                return;
            }

            string month = DateTime.Parse(txtmonth.Text.Trim(), CultureInfo.GetCultureInfo("en-gb")).Month.ToString();
            string Year = DateTime.Parse(txtmonth.Text.Trim(), CultureInfo.GetCultureInfo("en-gb")).Year.ToString();
            DateTime date = DateTime.Now;


            DateTime firstday = DateTime.Now;
            firstday = GlobalData.Instance.GetFirstDayMonth(int.Parse(Year), int.Parse(month));
            string fday = firstday.ToShortDateString();



            #region for oldcode

            //Sqlqry = "select ed.EmpId,rtrim(ISNULL(ed.EmpFName, '') + ' ' + ISNULL(ed.empmname, '') + ' ' + ISNULL(ed.emplname, '')) as Fullname, round(sum(eps.Gross),0) as GrossWAGES,round(sum(eps.PFWAGES),0) as PFWAGES,case when round(sum(eps.PFWAGES),0) >15000 then 15000 else round(sum(eps.PFWAGES),0) end  as EDLIWAGES,case when DATEDIFF(year, EmpDtofBirth, GETDATE())< 58 then round(sum(eps.PFWAGES),0) else '0' end EPSWAGES,case when DATEDIFF(year, EmpDtofBirth, GETDATE())< 58 then case when round(sum(eps.PFWAGES),0) >15000 then 15000 else round(sum(eps.PFWAGES),0) end else '0' end EPSWAGESNEW,(round(sum(eps.PF),0) -case when DATEDIFF(year, EmpDtofBirth, GETDATE()) < 58 then case when round(sum(eps.PFWAGES),0) >15000 then round(15000*8.33/100,0) else  round(sum(eps.PFWages * 8.33 / 100),0) end else '' end) as PFDiffnew," +
            //         "round(sum(eps.PF),0) as PF, round(sum(eps.PFEmpr),0) as PFEmpr, case when SUM(eps.NoOfDuties) > NoofDaysFromContracts then '0' else  (isnull(NoofDaysFromContracts,0)-SUM(eps.NoOfDuties)) end as NCPDAYS, 0 as ADVREF, 0 as ARREAREPF, 0 as ARREAREPFEE,0 as ARREAREPFER, 0 as ARREAREPS, case when DATEDIFF(year, EmpDtofBirth, GETDATE()) < 58 then round(sum(eps.PFWages * 8.33 / 100),0) else '' end EPSDue,case when DATEDIFF(year, EmpDtofBirth, GETDATE()) < 58 then case when round(sum(eps.PFWAGES),0) >15000 then round(15000*8.33/100,0) else  round(sum(eps.PFWages * 8.33 / 100),0) end else '' end EPSDuenew, round(sum(eps.PF),0) as PF, round(sum(eps.PFEmpr),0) as PFEmpr, sum(eps.NoOfDuties) as NoOfDuties, case when(ed.EmpDtofJoining >= '" + fday + "') then 'F' else '' end as relation, (round(sum(eps.PF),0) - (case when DATEDIFF(year, EmpDtofBirth, GETDATE()) < 58 then round(sum(eps.PFWages * 8.33 / 100),0) else '' end)), " +
            //         "case when(ed.EmpDtofJoining >= '" + fday + "') then case convert(varchar(10), ed.EmpDtofBirth, 103) when '01/01/1900' then '' else convert(varchar(10), ed.EmpDtofBirth, 103) end else '' end EmpDtofBirth,case convert(varchar(10), ed.empdtofleaving, 103) when '01/01/1900' then '' else  convert(varchar(10), ed.empdtofleaving, 103) end empdtofleaving,case when(convert(varchar(10), ed.empdtofleaving, 103) <> '01/01/1900' and convert(varchar(10), ed.empdtofleaving, 103) <> '')  then 'C' else '' end Reason," +
            //         "case when(ed.EmpDtofJoining >= '" + fday + "') then ed.EmpSex else '' end as EmpSex  ,case convert(varchar(10), ed.EmpDtofJoining, 103) when '01/01/1900' then '' else convert(varchar(10), ed.EmpDtofJoining, 103) end EmpDtofJoining,  case when(ed.EmpDtofJoining >= '" + fday + "') then ed.EmpFatherName else '' end EmpFatherName,case when(ed.EmpDtofJoining >= '" + fday + "') then case convert(varchar(10),ed.EmpDtofJoining, 103) when '01/01/1900' then '' else convert(varchar(10), ed.EmpDtofJoining, 103) end else '' end EmpPFEnrolDt," +
            //         " ltrim(rtrim(epf.EmpEpfNo)) EmpEpfNo,EmpUANNumber,round(sum(round(eps.PFWAGES, 0))*3.67/100,0) as EdliContribution from EmpPaySheet Eps  inner join EmpDetails ed on ed.EmpId = eps.EmpId inner join clients c on c.clientid = eps.clientid left outer join EMPEPFCodes epf on ed.EmpId = epf.Empid " +
            //         " where  eps.month = '" + month + Year.Substring(2, 2) + "' and epf.empepfno <> '' and len(epf.empepfno) > 0 group by ed.EmpId, ed.EmpFatherName,NoofDaysFromContracts, ed.EmpDtofJoining, ed.EmpDtofBirth, ed.EmpFName, ed.empmname, ed.emplname, ed.EmpSex, epf.EmpEpfNo, ed.empdtofleaving,EmpUANNumber " +
            //         " union " +
            //       " (select ed.EmpId, rtrim(ISNULL(ed.EmpFName, '') + ' ' + ISNULL(ed.empmname, '') + ' ' + ISNULL(ed.emplname, '')) as Fullname,0 as GrossWAGES, 0 as PFWAGES,0 as EDLIWAGES, 0 EPSWAGES,0 as EPSWAGESNEW,0 as pfdiffnew, " +
            //       " 0 as PF, 0 as PFEmpr, 0 EPSDue,0 as ESPDueNew, 0 as PF, 0 as PFEmpr, 0 as NCPDAYS, 0 as ADVREF, 0 as ARREAREPF, 0 as ARREAREPFER,0 as ARREAREPFEE, 0 as ARREAREPS, 0 NoOfDuties, case when(ed.EmpDtofJoining >= '" + fday + "') then 'F' else '' end as relation, 0 as PfDiff," +
            //       " case when(ed.EmpDtofJoining >= '" + fday + "') then case convert(varchar(10), ed.EmpDtofBirth, 103) when '01/01/1900' then '' else convert(varchar(10), ed.EmpDtofBirth, 103) end else '' end EmpDtofBirth,case convert(varchar(10), ed.empdtofleaving, 103) when '01/01/1900' then '' else  convert(varchar(10), ed.empdtofleaving, 103) end empdtofleaving,case when(convert(varchar(10), ed.empdtofleaving, 103) <> '01/01/1900' and convert(varchar(10), ed.empdtofleaving, 103) <> '')  then 'C' else '' end Reason," +
            //       " case when(ed.EmpDtofJoining >= '" + fday + "') then ed.EmpSex else '' end as EmpSex,case convert(varchar(10), ed.EmpDtofJoining, 103) when '01/01/1900' then '' else convert(varchar(10), ed.EmpDtofJoining, 103) end EmpDtofJoining,  case when(ed.EmpDtofJoining >= '" + fday + "') then ed.EmpFatherName else '' end EmpFatherName,case when(ed.EmpDtofJoining >= '" + fday + "') then case convert(varchar(10)," +
            //        "ed.EmpDtofJoining, 103) when '01/01/1900' then '' else convert(varchar(10), ed.EmpDtofJoining, 103) end else '' end EmpPFEnrolDt, " +
            //        "ltrim(rtrim(epf.EmpEpfNo)) EmpEpfNo,EmpUANNumber,0 as EdliContribution from EmpDetails ed left outer join EMPEPFCodes epf on ed.EmpId = epf.Empid " +
            //       " where MONTH(ed.EmpDtofLeaving) = '" + month + "' and YEAR(ed.EmpDtofLeaving) = '" + Year + "' and epf.empepfno <> '' and len(epf.empepfno) > 0 and ed.empid not in (select empid from emppaysheet where month = '" + month + Year.Substring(2, 2) + "' )" +
            //       " group by ed.EmpId, ed.EmpFatherName, ed.EmpDtofJoining, ed.EmpDtofBirth, ed.EmpFName, ed.empmname, ed.emplname, ed.EmpSex, epf.EmpEpfNo, ed.empdtofleaving,EmpUANNumber)" +
            //      " union " +
            //       " (select ed.EmpId, rtrim(ISNULL(ed.EmpFName, '') + ' ' + ISNULL(ed.empmname, '') + ' ' + ISNULL(ed.emplname, '')) as Fullname,0 as GrossWAGES, 0 as PFWAGES,0 as EDLIWAGES, 0 EPSWAGES,0 as EPSWAGESNEW,0 as pfdiffnew," +
            //       " 0 as PF, 0 as PFEmpr, 0 EPSDue, 0 as ESPDueNew,0 as PF, 0 as PFEmpr, 0 as NCPDAYS, 0 as ADVREF, 0 as ARREAREPF, 0 as ARREAREPFER,0 as ARREAREPFEE, 0 as ARREAREPS, 0 NoOfDuties, case when(ed.EmpDtofJoining >= '" + fday + "') then 'F' else '' end as relation, 0 as PfDiff," +
            //       " case when(ed.EmpDtofJoining >= '" + fday + "') then case convert(varchar(10), ed.EmpDtofBirth, 103) when '01/01/1900' then '' else convert(varchar(10), ed.EmpDtofBirth, 103) end else '' end EmpDtofBirth,case convert(varchar(10), ed.empdtofleaving, 103) when '01/01/1900' then '' else  convert(varchar(10), ed.empdtofleaving, 103) end empdtofleaving,case when(convert(varchar(10), ed.empdtofleaving, 103) <> '01/01/1900' and convert(varchar(10), ed.empdtofleaving, 103) <> '')  then 'C' else '' end Reason," +
            //       " case when(ed.EmpDtofJoining >= '" + fday + "') then ed.EmpSex else '' end as EmpSex,case convert(varchar(10), ed.EmpDtofJoining, 103) when '01/01/1900' then '' else convert(varchar(10), ed.EmpDtofJoining, 103) end EmpDtofJoining,  case when(ed.EmpDtofJoining >= '" + fday + "') then ed.EmpFatherName else '' end EmpFatherName,case when(ed.EmpDtofJoining >= '" + fday + "') then case convert(varchar(10)," +
            //        "ed.EmpDtofJoining, 103) when '01/01/1900' then '' else convert(varchar(10), ed.EmpDtofJoining, 103) end else '' end EmpPFEnrolDt, " +
            //        "ltrim(rtrim(epf.EmpEpfNo)) EmpEpfNo,EmpUANNumber,0 as EdliContribution from EmpDetails ed left outer join EMPEPFCodes epf on ed.EmpId = epf.Empid " +
            //       " where MONTH(ed.EmpDtofJoining) = '" + month + "' and YEAR(ed.EmpDtofJoining) = '" + Year + "' and epf.empepfno <> '' and len(epf.empepfno) > 0 and ed.empid not in (select empid from emppaysheet where month = '" + month + Year.Substring(2, 2) + "' )" +
            //       " group by ed.EmpId, ed.EmpFatherName, ed.EmpDtofJoining, ed.EmpDtofBirth, ed.EmpFName, ed.empmname, ed.emplname, ed.EmpSex, epf.EmpEpfNo, ed.empdtofleaving,EmpUANNumber)";

            #endregion

            var list = new List<string>();

            string clientname = "";

            if (GVListEmployees.Rows.Count > 0)
            {

                for (int i = 0; i < GVListEmployees.Rows.Count; i++)
                {
                    CheckBox chkclientid = GVListEmployees.Rows[i].FindControl("chkindividual") as CheckBox;
                    if (chkclientid.Checked == true)
                    {
                        Label lblclientid = GVListEmployees.Rows[i].FindControl("lblclientid") as Label;
                        Label lblclientname = GVListEmployees.Rows[i].FindControl("lblclientname") as Label;

                        if (chkclientid.Checked == true)
                        {
                            list.Add("" + lblclientid.Text + "");
                        }

                    }

                }


            }

            string Clientids = string.Join(",", list.ToArray());


            DataTable dtClientList = new DataTable();
            dtClientList.Columns.Add("Clientid");
            if (list.Count != 0)
            {
                foreach (string s in list)
                {
                    DataRow row = dtClientList.NewRow();
                    row["Clientid"] = s;
                    dtClientList.Rows.Add(row);
                }
            }
            string type = "PF";

            string SPName = "PFESIDetailsReport";
            Hashtable ht = new Hashtable();
            ht.Add("@month", month + Year.Substring(2, 2));
            ht.Add("@CurrentMonth", month);
            ht.Add("@CurrentYear", Year);
            ht.Add("@Date", fday);
            ht.Add("@ClientIDList", dtClientList);
            ht.Add("@type", type);

            dt = Config.ExecuteAdaptorAsyncWithParams(SPName, ht).Result;

            if (dt.Rows.Count > 0)
            {
                GVListOfClients.DataSource = dt;
                GVListOfClients.DataBind();
            }
        }

        protected void lbtn_Export_Click(object sender, EventArgs e)
        {
            GVUtill.Export("PFReport.xls", this.GVListOfClients);
        }

        protected void lbtn_Export_PDF_Click(object sender, EventArgs e)
        {



            uint Fontsize = 10;

            DataTable dt = null;
            String Sqlqry = "";

            if (txtmonth.Text.Trim().Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showlalert", "alert('Please Select Month');", true);
                return;
            }
            string strQry = "Select * from CompanyInfo  where   ClientidPrefix='" + CmpIDPrefix + "'";
            DataTable compInfo = Config.ExecuteReaderWithQueryAsync(strQry).Result;
            string companyName = "Your Company Name";
            string companyAddress = "Your Company Address";
            if (compInfo.Rows.Count > 0)
            {
                companyName = compInfo.Rows[0]["CompanyName"].ToString();
                companyAddress = compInfo.Rows[0]["Address"].ToString();
            }
            string month = DateTime.Parse(txtmonth.Text.Trim(), CultureInfo.GetCultureInfo("en-gb")).Month.ToString();
            string Year = DateTime.Parse(txtmonth.Text.Trim(), CultureInfo.GetCultureInfo("en-gb")).Year.ToString();

            DateTime firstday = DateTime.Now;
            firstday = GlobalData.Instance.GetFirstDayMonth(int.Parse(Year), int.Parse(month));
            string fday = firstday.ToShortDateString();

            var list = new List<string>();

            if (GVListEmployees.Rows.Count > 0)
            {
                for (int i = 0; i < GVListEmployees.Rows.Count; i++)
                {
                    CheckBox chkclientid = GVListEmployees.Rows[i].FindControl("chkindividual") as CheckBox;
                    Label lblclientid = GVListEmployees.Rows[i].FindControl("lblclientid") as Label;

                    if (chkclientid.Checked == true)
                    {
                        list.Add("'" + lblclientid.Text + "'");
                    }

                }
            }

            string Clientids = string.Join(",", list.ToArray());

            Sqlqry = "select ed.EmpId,rtrim(ISNULL(ed.EmpFName, '') + ' ' + ISNULL(ed.empmname, '') + ' ' + ISNULL(ed.emplname, '')) as Fullname,EmpUANNumber, sum(round(eps.PFWAGES, 0)) as PFWAGES,case when DATEDIFF(year, EmpDtofBirth, GETDATE())< 58 then sum(round(eps.PFWAGES, 0)) else '0' end EPSWAGES,case when DATEDIFF(year, EmpDtofBirth, GETDATE()) < 58 then case when sum(round(eps.PFWAGES, 0)) >15000 then round(15000*8.33/100,0) else  sum(round((eps.PFWages * 8.33 / 100), 0)) end else '' end EPSDuenew,round(sum(eps.PF) -case when DATEDIFF(year, EmpDtofBirth, GETDATE()) < 58 then case when sum(round(eps.PFWAGES, 0)) >15000 then round(15000*8.33/100,0) else  sum(round((eps.PFWages * 8.33 / 100), 0)) end else '' end,0) as PFDiffnew," +
                     "sum(round(eps.PF, 0)) as PF, sum(round(eps.PFEmpr, 0)) as PFEmpr, case when DATEDIFF(year, EmpDtofBirth, GETDATE()) < 58 then sum(round((eps.PFWages * 8.33 / 100), 0)) else '' end EPSDue, sum(round(eps.PF, 0)) as PF, sum(round(eps.PFEmpr, 0)) as PFEmpr, sum(eps.NoOfDuties) as NoOfDuties, case when(ed.EmpDtofJoining >= '" + fday + "') then 'F' else '' end as relation, sum(round(eps.PF - (case when DATEDIFF(year, EmpDtofBirth, GETDATE()) < 58 then((eps.PFWAGES * 8.33 / 100)) else '' end), 0)) as PfDiff, " +
                     "case when(ed.EmpDtofJoining >= '" + fday + "') then case convert(varchar(10), ed.EmpDtofBirth, 103) when '01/01/1900' then '' else convert(varchar(10), ed.EmpDtofBirth, 103) end else '' end EmpDtofBirth,case convert(varchar(10), ed.empdtofleaving, 103) when '01/01/1900' then '' else  convert(varchar(10), ed.empdtofleaving, 103) end empdtofleaving,case when(convert(varchar(10), ed.empdtofleaving, 103) <> '01/01/1900' and convert(varchar(10), ed.empdtofleaving, 103) <> '')  then 'C' else '' end Reason," +
                     "case when(ed.EmpDtofJoining >= '" + fday + "') then ed.EmpSex else '' end as EmpSex  ,case convert(varchar(10), ed.EmpDtofJoining, 103) when '01/01/1900' then '' else convert(varchar(10), ed.EmpDtofJoining, 103) end EmpDtofJoining,  case when(ed.EmpDtofJoining >= '" + fday + "') then ed.EmpFatherName else '' end EmpFatherName,case when(ed.EmpDtofJoining >= '" + fday + "') then case convert(varchar(10),ed.EmpDtofJoining, 103) when '01/01/1900' then '' else convert(varchar(10), ed.EmpDtofJoining, 103) end else '' end EmpPFEnrolDt," +
                     " ltrim(rtrim(epf.EmpEpfNo)) EmpEpfNo from EmpPaySheet Eps  inner join EmpDetails ed on ed.EmpId = eps.EmpId inner join clients c on c.clientid = eps.clientid left outer join EMPEPFCodes epf on ed.EmpId = epf.Empid " +
                     " where  eps.month = '" + month + Year.Substring(2, 2) + "' and eps.clientid in (" + Clientids + ") and epf.empepfno <> '' and len(epf.empepfno) > 0 group by ed.EmpId, ed.EmpFatherName, ed.EmpDtofJoining, ed.EmpDtofBirth, ed.EmpFName, ed.empmname, ed.emplname, ed.EmpSex, epf.EmpEpfNo, ed.empdtofleaving,EmpUANNumber ";
            dt = SqlHelper.Instance.GetTableByQuery(Sqlqry);

            int j = 1;
            string pfno = "";
            string UANNO = "";
            string Name = "";
            decimal pfwages = 0;
            decimal pf = 0;
            decimal diff = 0;
            decimal pension = 0;
            double Totalpfwages = 0;
            decimal Totalpf = 0;
            decimal Totaldiff = 0;
            decimal Totalpension = 0;
            decimal epswages = 0;
            decimal Totalepswages = 0;

            if (dt.Rows.Count > 0)
            {

                MemoryStream ms = new MemoryStream();
                Document document = new Document(PageSize.A4);

                var writer = PdfWriter.GetInstance(document, ms);
                document.Open();
                BaseFont bf = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                string imagepath1 = Server.MapPath("images");


                PdfPTable table = new PdfPTable(8);
                table.TotalWidth = 500f;
                table.HeaderRows = 5;
                table.LockedWidth = true;
                float[] width = new float[] { 1f, 3f, 6f, 3f, 4f, 2f, 2f, 2f };
                table.SetWidths(width);

                PdfPCell cellspace = new PdfPCell(new Phrase("  ", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.BOLD, BaseColor.BLACK)));
                cellspace.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                cellspace.Colspan = 8;
                cellspace.Border = 0;
                cellspace.PaddingTop = 0;


                PdfPCell cellcompanyname = new PdfPCell(new Phrase(companyName, FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize + 2, Font.BOLD, BaseColor.BLACK)));
                cellcompanyname.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                cellcompanyname.Colspan = 4;
                cellcompanyname.Border = 0;
                cellcompanyname.PaddingTop = 0;
                table.AddCell(cellcompanyname);

                PdfPCell EmplPF = new PdfPCell(new Phrase("Employer PF No.:", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize + 2, Font.BOLD, BaseColor.BLACK)));
                EmplPF.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                EmplPF.Colspan = 4;
                EmplPF.Border = 0;
                EmplPF.PaddingTop = 0;
                table.AddCell(EmplPF);


                PdfPCell PFReport = new PdfPCell(new Phrase("PF report for the month of " + GetMonthName() + "/" + GetMonthOfYear(), FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize + 2, Font.BOLD, BaseColor.BLACK)));
                PFReport.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                PFReport.Colspan = 8;
                PFReport.Border = 0;
                PFReport.PaddingTop = 20;
                PFReport.PaddingBottom = 20;
                PFReport.PaddingTop = 0;
                table.AddCell(PFReport);



                PdfPCell CellName1 = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.BOLD, BaseColor.BLACK)));
                CellName1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                CellName1.BorderWidthLeft = 0;
                CellName1.BorderWidthRight = 0;
                CellName1.BorderWidthBottom = 0;
                CellName1.PaddingTop = 0;
                CellName1.Colspan = 2;
                table.AddCell(CellName1);



                PdfPCell CellPFContribution1 = new PdfPCell(new Phrase("Employee Contribution", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.BOLD, BaseColor.BLACK)));
                CellPFContribution1.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                CellPFContribution1.Colspan = 3;
                CellPFContribution1.BorderWidthLeft = 0;
                CellPFContribution1.BorderWidthRight = 0;
                CellPFContribution1.BorderWidthBottom = 0;
                CellPFContribution1.PaddingTop = 0;
                table.AddCell(CellPFContribution1);

                PdfPCell CellPFDiff1 = new PdfPCell(new Phrase("Employer Contribution", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.BOLD, BaseColor.BLACK)));
                CellPFDiff1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                CellPFDiff1.BorderWidthLeft = 0;
                CellPFDiff1.BorderWidthRight = 0;
                CellPFDiff1.BorderWidthBottom = 0;
                CellPFDiff1.Colspan = 3;
                CellPFDiff1.PaddingTop = 0;
                table.AddCell(CellPFDiff1);

                /////

                PdfPCell CellSlNo = new PdfPCell(new Phrase("Sl.No.", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.BOLD, BaseColor.BLACK)));
                CellSlNo.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                CellSlNo.Border = 0;
                CellSlNo.PaddingTop = 0;
                table.AddCell(CellSlNo);


                PdfPCell CellPFNo = new PdfPCell(new Phrase("PF A/C No.", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.BOLD, BaseColor.BLACK)));
                CellPFNo.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                CellPFNo.Border = 0;
                CellPFNo.PaddingTop = 0;
                table.AddCell(CellPFNo);

                PdfPCell CellName = new PdfPCell(new Phrase("Name of Member", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.BOLD, BaseColor.BLACK)));
                CellName.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                CellName.Border = 0;
                CellName.PaddingTop = 0;
                table.AddCell(CellName);

                PdfPCell CellUANNO = new PdfPCell(new Phrase("UAN No", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.BOLD, BaseColor.BLACK)));
                CellUANNO.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                CellUANNO.Border = 0;
                CellUANNO.PaddingTop = 0;
                table.AddCell(CellUANNO);


                PdfPCell CellPFEarnings = new PdfPCell(new Phrase("PF Earnings", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.BOLD, BaseColor.BLACK)));
                CellPFEarnings.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                CellPFEarnings.Border = 0;
                CellPFEarnings.PaddingTop = 0;
                table.AddCell(CellPFEarnings);


                PdfPCell CellPFContribution = new PdfPCell(new Phrase("Contribution EPF", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.BOLD, BaseColor.BLACK)));
                CellPFContribution.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                CellPFContribution.Border = 0;
                CellPFContribution.PaddingTop = 0;
                table.AddCell(CellPFContribution);

                PdfPCell CellPFDiff = new PdfPCell(new Phrase("EPF Difference", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.BOLD, BaseColor.BLACK)));
                CellPFDiff.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                CellPFDiff.Border = 0;
                CellPFDiff.PaddingTop = 0;
                table.AddCell(CellPFDiff);

                PdfPCell CellPension = new PdfPCell(new Phrase("Pension 8.33%", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.BOLD, BaseColor.BLACK)));
                CellPension.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                CellPension.Border = 0;
                CellPension.PaddingTop = 0;
                table.AddCell(CellPension);


                //////


                PdfPCell CellSlNo2 = new PdfPCell(new Phrase("(1)", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.BOLD, BaseColor.BLACK)));
                CellSlNo2.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                CellSlNo2.BorderWidthLeft = 0;
                CellSlNo2.BorderWidthRight = 0;
                CellSlNo2.BorderWidthTop = 0;
                CellSlNo2.PaddingTop = 0;
                table.AddCell(CellSlNo2);


                PdfPCell CellPFNo2 = new PdfPCell(new Phrase("(2)", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.BOLD, BaseColor.BLACK)));
                CellPFNo2.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                CellPFNo2.BorderWidthLeft = 0;
                CellPFNo2.BorderWidthRight = 0;
                CellPFNo2.BorderWidthTop = 0;
                CellPFNo2.PaddingTop = 0;
                table.AddCell(CellPFNo2);

                PdfPCell CellName2 = new PdfPCell(new Phrase("(3)", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.BOLD, BaseColor.BLACK)));
                CellName2.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                CellName2.BorderWidthLeft = 0;
                CellName2.BorderWidthRight = 0;
                CellName2.BorderWidthTop = 0;
                CellName2.PaddingTop = 0;
                table.AddCell(CellName2);


                PdfPCell CellUANNO2 = new PdfPCell(new Phrase("(4)", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.BOLD, BaseColor.BLACK)));
                CellUANNO2.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                CellUANNO2.BorderWidthLeft = 0;
                CellUANNO2.BorderWidthRight = 0;
                CellUANNO2.BorderWidthTop = 0;
                CellUANNO2.PaddingTop = 0;
                table.AddCell(CellUANNO2);


                PdfPCell CellPFEarnings2 = new PdfPCell(new Phrase("(5)", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.BOLD, BaseColor.BLACK)));
                CellPFEarnings2.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                CellPFEarnings2.BorderWidthLeft = 0;
                CellPFEarnings2.BorderWidthRight = 0;
                CellPFEarnings2.BorderWidthTop = 0;
                CellPFEarnings2.PaddingTop = 0;
                table.AddCell(CellPFEarnings2);


                PdfPCell CellPFContribution2 = new PdfPCell(new Phrase("(6)", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.BOLD, BaseColor.BLACK)));
                CellPFContribution2.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                CellPFContribution2.BorderWidthLeft = 0;
                CellPFContribution2.BorderWidthRight = 0;
                CellPFContribution2.PaddingTop = 0;
                CellPFContribution2.BorderWidthTop = 0;
                table.AddCell(CellPFContribution2);

                PdfPCell CellPFDiff2 = new PdfPCell(new Phrase("(7)", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.BOLD, BaseColor.BLACK)));
                CellPFDiff2.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                CellPFDiff2.BorderWidthLeft = 0;
                CellPFDiff2.BorderWidthRight = 0;
                CellPFDiff2.BorderWidthTop = 0;
                CellPFDiff2.PaddingTop = 0;
                table.AddCell(CellPFDiff2);

                PdfPCell CellPension2 = new PdfPCell(new Phrase("(8)", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.BOLD, BaseColor.BLACK)));
                CellPension2.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                CellPension2.BorderWidthLeft = 0;
                CellPension2.BorderWidthRight = 0;
                CellPension2.PaddingTop = 0;
                CellPension2.BorderWidthTop = 0;
                table.AddCell(CellPension2);

                for (int k = 0; k < dt.Rows.Count; k++)
                {

                    pfno = dt.Rows[k]["EmpEpfNo"].ToString();
                    Name = dt.Rows[k]["Fullname"].ToString();
                    pfwages = Convert.ToDecimal(dt.Rows[k]["PFWAGES"].ToString());
                    pf = Convert.ToDecimal(dt.Rows[k]["PF"].ToString());
                    diff = Convert.ToDecimal(dt.Rows[k]["PFDiff"].ToString());
                    pension = Convert.ToDecimal(dt.Rows[k]["EPSDue"].ToString());
                    epswages = Convert.ToDecimal(dt.Rows[k]["EPSWAGES"].ToString());
                    UANNO = dt.Rows[k]["EmpUANNumber"].ToString();

                    PdfPCell CellSlNo3 = new PdfPCell(new Phrase(j.ToString(), FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
                    CellSlNo3.HorizontalAlignment = 1; //0=Left, 1=Centre, 3=Right
                    CellSlNo3.Border = 0;
                    CellSlNo3.PaddingTop = 0;
                    table.AddCell(CellSlNo3);


                    PdfPCell CellPFNo3 = new PdfPCell(new Phrase(pfno, FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
                    CellPFNo3.HorizontalAlignment = 1; //0=Left, 1=Centre, 3=Right
                    CellPFNo3.Border = 0;
                    CellPFNo3.PaddingTop = 0;
                    table.AddCell(CellPFNo3);

                    PdfPCell CellName3 = new PdfPCell(new Phrase(Name, FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
                    CellName3.HorizontalAlignment = 0; //0=Left, 1=Centre, 3=Right
                    CellName3.Border = 0;
                    CellName3.BorderWidthRight = 0;
                    CellName3.PaddingTop = 0;
                    table.AddCell(CellName3);

                    PdfPCell CellUANNO3 = new PdfPCell(new Phrase(UANNO, FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
                    CellUANNO3.HorizontalAlignment = 0; //0=Left, 1=Centre, 3=Right
                    CellUANNO3.Border = 0;
                    CellUANNO3.BorderWidthRight = 0;
                    CellUANNO3.PaddingTop = 0;
                    table.AddCell(CellUANNO3);


                    PdfPCell CellPFEarnings3 = new PdfPCell(new Phrase(pfwages.ToString("0.00"), FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
                    CellPFEarnings3.HorizontalAlignment = 1; //0=Left, 1=Centre, 3=Right
                    CellPFEarnings3.Border = 0;
                    CellPFEarnings3.PaddingTop = 0;
                    table.AddCell(CellPFEarnings3);
                    Totalpfwages += Convert.ToDouble(pfwages);

                    PdfPCell CellPFContribution3 = new PdfPCell(new Phrase(pf.ToString("0.00"), FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
                    CellPFContribution3.HorizontalAlignment = 1; //0=Left, 1=Centre, 3=Right
                    CellPFContribution3.Border = 0;
                    CellPFContribution3.PaddingTop = 0;
                    table.AddCell(CellPFContribution3);
                    Totalpf += pf;

                    PdfPCell CellPFDiff3 = new PdfPCell(new Phrase(diff.ToString("0.00"), FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
                    CellPFDiff3.HorizontalAlignment = 1; //0=Left, 1=Centre, 3=Right
                    CellPFDiff3.Border = 0;
                    CellPFDiff3.PaddingTop = 0;
                    table.AddCell(CellPFDiff3);
                    Totaldiff += diff;

                    PdfPCell CellPension3 = new PdfPCell(new Phrase(pension.ToString("0.00"), FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
                    CellPension3.HorizontalAlignment = 1; //0=Left, 1=Centre, 3=Right
                    CellPension3.Border = 0;
                    CellPension3.PaddingTop = 0;
                    table.AddCell(CellPension3);
                    Totalpension += pension;

                    Totalepswages += epswages;
                    j++;
                }



                PdfPCell CellSlNo4 = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.BOLD, BaseColor.BLACK)));
                CellSlNo4.HorizontalAlignment = 1; //0=Left, 1=Centre, 4=Right
                CellSlNo4.BorderWidthLeft = 0;
                CellSlNo4.BorderWidthRight = 0;
                CellSlNo4.PaddingTop = 0;
                table.AddCell(CellSlNo4);


                PdfPCell CellPFNo4 = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.BOLD, BaseColor.BLACK)));
                CellPFNo4.HorizontalAlignment = 1; //0=Left, 1=Centre, 4=Right
                CellPFNo4.BorderWidthLeft = 0;
                CellPFNo4.BorderWidthRight = 0;
                CellPFNo4.PaddingTop = 0;
                table.AddCell(CellPFNo4);

                PdfPCell CellName4 = new PdfPCell(new Phrase(" ", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.BOLD, BaseColor.BLACK)));
                CellName4.HorizontalAlignment = 2; //0=Left, 1=Centre, 4=Right
                CellName4.BorderWidthLeft = 0;
                CellName4.BorderWidthRight = 0;
                CellName4.PaddingTop = 0;
                table.AddCell(CellName4);

                PdfPCell CellUANNO4 = new PdfPCell(new Phrase("TOTAL\n\n", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.BOLD, BaseColor.BLACK)));
                CellUANNO4.HorizontalAlignment = 2; //0=Left, 1=Centre, 4=Right
                CellUANNO4.BorderWidthLeft = 0;
                CellUANNO4.BorderWidthRight = 0;
                CellUANNO4.PaddingTop = 0;
                table.AddCell(CellUANNO4);

                PdfPCell CellPFEarnings4 = new PdfPCell(new Phrase(Totalpfwages.ToString("0.00"), FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.BOLD, BaseColor.BLACK)));
                CellPFEarnings4.HorizontalAlignment = 1; //0=Left, 1=Centre, 4=Right
                CellPFEarnings4.BorderWidthLeft = 0;
                CellPFEarnings4.BorderWidthRight = 0;
                CellPFEarnings4.PaddingTop = 0;
                table.AddCell(CellPFEarnings4);


                PdfPCell CellPFContribution4 = new PdfPCell(new Phrase(Totalpf.ToString("0.00"), FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.BOLD, BaseColor.BLACK)));
                CellPFContribution4.HorizontalAlignment = 1; //0=Left, 1=Centre, 4=Right
                CellPFContribution4.BorderWidthLeft = 0;
                CellPFContribution4.BorderWidthRight = 0;
                CellPFContribution4.PaddingTop = 0;
                table.AddCell(CellPFContribution4);

                PdfPCell CellPFDiff4 = new PdfPCell(new Phrase(Totaldiff.ToString("0.00"), FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.BOLD, BaseColor.BLACK)));
                CellPFDiff4.HorizontalAlignment = 1; //0=Left, 1=Centre, 4=Right
                CellPFDiff4.BorderWidthLeft = 0;
                CellPFDiff4.BorderWidthRight = 0;
                CellPFDiff4.PaddingTop = 0;
                table.AddCell(CellPFDiff4);

                PdfPCell CellPension4 = new PdfPCell(new Phrase(Totalpension.ToString("0.00"), FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.BOLD, BaseColor.BLACK)));
                CellPension4.HorizontalAlignment = 1; //0=Left, 1=Centre, 4=Right
                CellPension4.BorderWidthLeft = 0;
                CellPension4.BorderWidthRight = 0;
                CellPension4.PaddingTop = 0;
                table.AddCell(CellPension4);

                //////////
                document.Add(table);

                PdfPTable table1 = new PdfPTable(6);
                table1.TotalWidth = 500f;
                table1.LockedWidth = true;
                float[] width1 = new float[] { 3f, 3f, 3f, 7f, 0.5f, 3f };
                table1.SetWidths(width1);

                PdfPCell CellSlNo5 = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.BOLD, BaseColor.BLACK)));
                CellSlNo5.HorizontalAlignment = 1; //0=Left, 1=Centre, 5=Right
                CellSlNo5.Border = 0;
                CellSlNo5.BorderWidthRight = 0;
                CellSlNo5.PaddingTop = 0;
                CellSlNo5.Colspan = 2;
                table1.AddCell(CellSlNo5);

                PdfPCell CellPFEarnings5 = new PdfPCell(new Phrase("Account No:01 ", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.BOLD, BaseColor.BLACK)));
                CellPFEarnings5.HorizontalAlignment = 0; //0=Left, 1=Centre, 5=Right
                CellPFEarnings5.Border = 0;
                CellPFEarnings5.BorderWidthRight = 0;
                CellPFEarnings5.PaddingTop = 0;
                CellPFEarnings5.Colspan = 1;
                table1.AddCell(CellPFEarnings5);

                PdfPCell cellcoloum = new PdfPCell(new Phrase("(Column Nos.5+6) ", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.BOLD, BaseColor.BLACK)));
                cellcoloum.HorizontalAlignment = 0; //0=Left, 1=Centre, 5=Right
                cellcoloum.Border = 0;
                cellcoloum.BorderWidthRight = 0;
                cellcoloum.PaddingTop = 0;
                cellcoloum.Colspan = 1;
                table1.AddCell(cellcoloum);


                PdfPCell CellPFval5 = new PdfPCell(new Phrase(" = ", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.BOLD, BaseColor.BLACK)));
                CellPFval5.HorizontalAlignment = 1; //0=Left, 1=Centre, 5=Right
                CellPFval5.Border = 0;
                CellPFval5.BorderWidthRight = 0;
                CellPFval5.PaddingTop = 0;
                CellPFval5.Colspan = 1;
                table1.AddCell(CellPFval5);


                PdfPCell CellPFContribution5 = new PdfPCell(new Phrase((Totalpf + Totaldiff).ToString("0.00"), FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.BOLD, BaseColor.BLACK)));
                CellPFContribution5.HorizontalAlignment = 2; //0=Left, 1=Centre, 5=Right
                CellPFContribution5.Border = 0;
                CellPFContribution5.BorderWidthRight = 0;
                CellPFContribution5.PaddingTop = 0;
                table1.AddCell(CellPFContribution5);


                ///////


                PdfPCell CellSlNo6 = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.BOLD, BaseColor.BLACK)));
                CellSlNo6.HorizontalAlignment = 1; //0=Left, 1=Centre, 6=Right
                CellSlNo6.Border = 0;
                CellSlNo6.BorderWidthRight = 0;
                CellSlNo6.PaddingTop = 0;
                CellSlNo6.Colspan = 2;
                table1.AddCell(CellSlNo6);




                PdfPCell CellPFEarnings6 = new PdfPCell(new Phrase("Account No:02", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.BOLD, BaseColor.BLACK)));
                CellPFEarnings6.HorizontalAlignment = 0; //0=Left, 1=Centre, 6=Right
                CellPFEarnings6.Border = 0;
                CellPFEarnings6.BorderWidthRight = 0;
                CellPFEarnings6.PaddingTop = 0;
                CellPFEarnings6.Colspan = 1;
                table1.AddCell(CellPFEarnings6);

                PdfPCell cellcolomn1 = new PdfPCell(new Phrase("(0.65000% of Column No.4)", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.BOLD, BaseColor.BLACK)));
                cellcolomn1.HorizontalAlignment = 0; //0=Left, 1=Centre, 6=Right
                cellcolomn1.Border = 0;
                cellcolomn1.BorderWidthRight = 0;
                cellcolomn1.PaddingTop = 0;
                cellcolomn1.Colspan = 1;
                table1.AddCell(cellcolomn1);

                PdfPCell cellequal = new PdfPCell(new Phrase(" = ", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.BOLD, BaseColor.BLACK)));
                cellequal.HorizontalAlignment = 1; //0=Left, 1=Centre, 6=Right
                cellequal.Border = 0;
                cellequal.BorderWidthRight = 0;
                cellequal.PaddingTop = 0;
                cellequal.Colspan = 1;
                table1.AddCell(cellequal);


                PdfPCell CellPFContribution6 = new PdfPCell(new Phrase((Totalpfwages * 0.65 / 100).ToString("#") + ".00", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.BOLD, BaseColor.BLACK)));
                CellPFContribution6.HorizontalAlignment = 2; //0=Left, 1=Centre, 6=Right
                CellPFContribution6.Border = 0;
                CellPFContribution6.BorderWidthRight = 0;
                CellPFContribution6.PaddingTop = 0;
                table1.AddCell(CellPFContribution6);

                /////


                PdfPCell CellSlNo7 = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.BOLD, BaseColor.BLACK)));
                CellSlNo7.HorizontalAlignment = 1; //0=Left, 1=Centre, 7=Right
                CellSlNo7.Border = 0;
                CellSlNo7.BorderWidthRight = 0;
                CellSlNo7.PaddingTop = 0;
                CellSlNo7.Colspan = 2;
                table1.AddCell(CellSlNo7);




                PdfPCell CellPFEarnings7 = new PdfPCell(new Phrase("Account No:10 ", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.BOLD, BaseColor.BLACK)));
                CellPFEarnings7.HorizontalAlignment = 0; //0=Left, 1=Centre, 7=Right
                CellPFEarnings7.Border = 0;
                CellPFEarnings7.PaddingTop = 0;
                CellPFEarnings7.Colspan = 1;
                table1.AddCell(CellPFEarnings7);


                PdfPCell CellPFEarnings71 = new PdfPCell(new Phrase("(Column No. 7)   ", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.BOLD, BaseColor.BLACK)));
                CellPFEarnings71.HorizontalAlignment = 0; //0=Left, 1=Centre, 7=Right
                CellPFEarnings71.Border = 0;
                CellPFEarnings71.PaddingTop = 0;
                CellPFEarnings71.Colspan = 1;
                table1.AddCell(CellPFEarnings71);



                PdfPCell cellequal1 = new PdfPCell(new Phrase(" =", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.BOLD, BaseColor.BLACK)));
                cellequal1.HorizontalAlignment = 1; //0=Left, 1=Centre, 7=Right
                cellequal1.Border = 0;
                cellequal1.PaddingTop = 0;
                cellequal1.Colspan = 1;
                table1.AddCell(cellequal1);


                PdfPCell CellPFContribution7 = new PdfPCell(new Phrase(Totalpension.ToString("0.00"), FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.BOLD, BaseColor.BLACK)));
                CellPFContribution7.HorizontalAlignment = 2; //0=Left, 1=Centre, 7=Right
                CellPFContribution7.Border = 0;
                CellPFContribution7.PaddingTop = 0;
                table1.AddCell(CellPFContribution7);


                ///



                PdfPCell CellSlNo8 = new PdfPCell(new Phrase("E D L I Wages :  ", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.BOLD, BaseColor.BLACK)));
                CellSlNo8.HorizontalAlignment = 0; //0=Left, 1=Centre, 8=Right
                CellSlNo8.Border = 0;
                CellSlNo8.PaddingTop = 0;
                CellSlNo8.Colspan = 1;
                table1.AddCell(CellSlNo8);

                PdfPCell CellSlNo81 = new PdfPCell(new Phrase(Totalpfwages.ToString("0.00"), FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.BOLD, BaseColor.BLACK)));
                CellSlNo81.HorizontalAlignment = 0; //0=Left, 1=Centre, 8=Right
                CellSlNo81.Border = 0;
                CellSlNo81.PaddingTop = 0;
                CellSlNo81.Colspan = 1;
                table1.AddCell(CellSlNo81);




                PdfPCell CellPFEarnings8 = new PdfPCell(new Phrase("Account No:21 ", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.BOLD, BaseColor.BLACK)));
                CellPFEarnings8.HorizontalAlignment = 0; //0=Left, 1=Centre, 8=Right
                CellPFEarnings8.Border = 0;
                CellPFEarnings8.PaddingTop = 0;
                CellPFEarnings8.Colspan = 1;
                table1.AddCell(CellPFEarnings8);

                PdfPCell CellPFEarnings81 = new PdfPCell(new Phrase("E D L I WAGES * 0.50000% ", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.BOLD, BaseColor.BLACK)));
                CellPFEarnings81.HorizontalAlignment = 0; //0=Left, 1=Centre, 8=Right
                CellPFEarnings81.Border = 0;
                CellPFEarnings81.PaddingTop = 0;
                CellPFEarnings81.Colspan = 1;
                table1.AddCell(CellPFEarnings81);

                PdfPCell CellPFEarnings812 = new PdfPCell(new Phrase(" = ", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.BOLD, BaseColor.BLACK)));
                CellPFEarnings812.HorizontalAlignment = 1; //0=Left, 1=Centre, 8=Right
                CellPFEarnings812.Border = 0;
                CellPFEarnings812.PaddingTop = 0;
                CellPFEarnings812.Colspan = 1;
                table1.AddCell(CellPFEarnings812);


                PdfPCell CellPFContribution8 = new PdfPCell(new Phrase((Totalpfwages * 0.5 / 100).ToString("#") + ".00", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.BOLD, BaseColor.BLACK)));
                CellPFContribution8.HorizontalAlignment = 2; //0=Left, 1=Centre, 8=Right
                CellPFContribution8.Border = 0;
                CellPFContribution8.PaddingTop = 0;
                table1.AddCell(CellPFContribution8);


                ///



                PdfPCell CellSlNo9 = new PdfPCell(new Phrase("Pension Wages:    ", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.BOLD, BaseColor.BLACK)));
                CellSlNo9.HorizontalAlignment = 0; //0=Left, 1=Centre, 9=Right
                CellSlNo9.Border = 0;
                CellSlNo9.PaddingTop = 0;
                CellSlNo9.Colspan = 1;
                table1.AddCell(CellSlNo9);


                PdfPCell celltotalpfwages = new PdfPCell(new Phrase(Totalepswages.ToString("0.00"), FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.BOLD, BaseColor.BLACK)));
                celltotalpfwages.HorizontalAlignment = 0; //0=Left, 1=Centre, 9=Right
                celltotalpfwages.Border = 0;
                celltotalpfwages.PaddingTop = 0;
                celltotalpfwages.Colspan = 1;
                table1.AddCell(celltotalpfwages);




                PdfPCell CellPFEarnings9 = new PdfPCell(new Phrase("Account No:22", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.BOLD, BaseColor.BLACK)));
                CellPFEarnings9.HorizontalAlignment = 0; //0=Left, 1=Centre, 9=Right
                CellPFEarnings9.Border = 0;
                CellPFEarnings9.PaddingTop = 0;
                CellPFEarnings9.Colspan = 1;
                table1.AddCell(CellPFEarnings9);


                PdfPCell CellPFEarnings91 = new PdfPCell(new Phrase("E D L I WAGES * 0.01000% ", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.BOLD, BaseColor.BLACK)));
                CellPFEarnings91.HorizontalAlignment = 0; //0=Left, 1=Centre, 9=Right
                CellPFEarnings91.Border = 0;
                CellPFEarnings91.PaddingTop = 0;
                CellPFEarnings91.Colspan = 1;
                table1.AddCell(CellPFEarnings91);


                PdfPCell CellPFEarnings92 = new PdfPCell(new Phrase(" = ", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.BOLD, BaseColor.BLACK)));
                CellPFEarnings92.HorizontalAlignment = 1; //0=Left, 1=Centre, 9=Right
                CellPFEarnings92.Border = 0;
                CellPFEarnings92.PaddingTop = 0;
                CellPFEarnings92.Colspan = 1;
                table1.AddCell(CellPFEarnings92);


                PdfPCell CellPFContribution9 = new PdfPCell(new Phrase((Totalpfwages * 0.01 / 100).ToString("#") + ".00", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.BOLD, BaseColor.BLACK)));
                CellPFContribution9.HorizontalAlignment = 2; //0=Left, 1=Centre, 9=Right
                CellPFContribution9.Border = 0;
                CellPFContribution9.PaddingTop = 0;
                table1.AddCell(CellPFContribution9);

                ////



                PdfPCell CellSlNo10 = new PdfPCell(new Phrase("\n\n", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.BOLD, BaseColor.BLACK)));
                CellSlNo10.HorizontalAlignment = 1; //0=Left, 1=Centre, 10=Right
                CellSlNo10.Border = 0;
                CellSlNo10.BorderWidthBottom = 0.5f;
                CellSlNo10.BorderWidthTop = 0.5f;
                CellSlNo10.PaddingTop = 0;
                CellSlNo10.Colspan = 2;
                table1.AddCell(CellSlNo10);




                PdfPCell CellPFEarnings10 = new PdfPCell(new Phrase("TOTAL\n", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.BOLD, BaseColor.BLACK)));
                CellPFEarnings10.HorizontalAlignment = 1; //0=Left, 1=Centre, 10=Right
                CellPFEarnings10.BorderWidthLeft = 0;
                CellPFEarnings10.BorderWidthRight = 0;
                CellPFEarnings10.BorderWidthBottom = 0.5f;
                CellPFEarnings10.PaddingTop = 0;
                CellPFEarnings10.Colspan = 2;
                table1.AddCell(CellPFEarnings10);


                PdfPCell CellPFContribution10 = new PdfPCell(new Phrase(((Totalpfwages * 0.01 / 100) + (Totalpfwages * 0.5 / 100) + Convert.ToDouble(Totalpension) + (Totalpfwages * 0.65 / 100) + Convert.ToDouble(Totalpf) + Convert.ToDouble(Totaldiff)).ToString("#") + ".00", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.BOLD, BaseColor.BLACK)));
                CellPFContribution10.HorizontalAlignment = 2; //0=Left, 1=Centre, 10=Right
                CellPFContribution10.BorderWidthLeft = 0;
                CellPFContribution10.BorderWidthRight = 0;
                CellPFContribution10.PaddingTop = 0;
                CellPFContribution10.Colspan = 2;
                CellPFContribution10.BorderWidthBottom = 0.5f;
                table1.AddCell(CellPFContribution10);

                document.Add(table1);


                document.Close();
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment;filename=Consolidated.pdf");
                Response.Buffer = true;
                Response.Clear();
                Response.OutputStream.Write(ms.GetBuffer(), 0, ms.GetBuffer().Length);
                Response.OutputStream.Flush();
                Response.End();

            }

        }

        protected void lbtn_Export_Text_Click(object sender, EventArgs e)
        {

            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=gvtocsv.txt");
            Response.Charset = "";
            Response.ContentType = "application/text";
            StringBuilder sBuilder = new System.Text.StringBuilder();
            //for (int index = 0; index < GVListOfClients.Columns.Count; index++)
            //{
            //    sBuilder.Append(GVListOfClients.Columns[index].HeaderText + ',');
            //}
            //sBuilder.Append("\r\n");
            for (int i = 0; i < GVListOfClients.Rows.Count; i++)
            {
                for (int k = 1; k < GVListOfClients.HeaderRow.Cells.Count; k++)
                {
                    sBuilder.Append(GVListOfClients.Rows[i].Cells[k].Text.TrimEnd().Replace(",", "#~#") + "#~#");
                }
                sBuilder.Remove(sBuilder.Length - 1, 1);
                sBuilder.Remove(sBuilder.Length - 1, 1);
                sBuilder.Remove(sBuilder.Length - 1, 1);
                sBuilder.Append("\r\n");
            }
            Response.Output.Write(sBuilder.ToString());
            Response.Flush();
            Response.End();
        }

        protected void GVListOfClients_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Attributes.Add("class", "text");
                e.Row.Cells[1].Attributes.Add("class", "text");

            }
        }

        protected void btn_GetClients_Click(object sender, EventArgs e)
        {

            string date = string.Empty;

            if (txtmonth.Text.Trim().Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Please Select The Month');", true);
                return;
            }

            if (txtmonth.Text.Trim().Length > 0)
            {
                date = DateTime.Parse(txtmonth.Text.Trim(), CultureInfo.GetCultureInfo("en-gb")).ToString();
            }

            string month = DateTime.Parse(date).Month.ToString();
            string Year = DateTime.Parse(date).Year.ToString().Substring(2, 2);

            string query = "select distinct(eps.clientid),clientname,sum(pf) as PF,sum(pfempr) as PFEmpr,(sum(pf)+sum(pfempr)) as TotalPF from emppaysheet eps inner join clients C on C.Clientid=eps.clientid where month='" + month + Year + "' and eps.clientid like '%" + CmpIDPrefix + "%' group by eps.clientid,clientname ";
            DataTable dtClients = SqlHelper.Instance.GetTableByQuery(query);

            GVListEmployees.DataSource = dtClients;
            GVListEmployees.DataBind();

        }
    }
}