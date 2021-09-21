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
using System.Xml.Linq;
using System.Globalization;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using KLTS.Data;
using Jawan.Portal.DAL;
namespace Jawan.Portal
{
    public partial class PaySheetPrintOuts : System.Web.UI.Page
    {
        AppConfiguration config = new AppConfiguration();
        GridViewExportUtil gve = new GridViewExportUtil();
        string EmpIDPrefix = "";
        string CmpIDPrefix = "";
        string BranchID = "";
        string Elength = "";
        string Clength = "";
        string Fontstyle = "";
        string CFontstyle = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            //lblpayment.Text = "";
            GetWebConfigdata();
            if (!IsPostBack)
            {
                if (Session["UserId"] != null && Session["AccessLevel"] != null)
                {
                    lblDisplayUser.Text = Session["UserId"].ToString();
                    PreviligeUsers(Convert.ToInt32(Session["AccessLevel"]));
                    lblcname.Text = SqlHelper.Instance.GetCompanyname();
                }
                else
                {
                    Response.Redirect("login.aspx");
                }

                LoadClientNames();
                LoadClientList();
                //month
            }
        }


        protected void PreviligeUsers(int previligerid)
        {
            switch (previligerid)
            {
                case 1:
                    break;
                case 2:
                    //AddEmployeeLink.Visible = true;
                    //ModifyEmployeeLink.Visible = false;
                    //DeleteEmployeeLink.Visible = false;
                    //AssigningWorkerLink.Visible = false;
                    //LoanLink.Visible = false;
                    //PaymentLink.Visible = false;
                    ////TrainingEmployeeLink.Visible = false;
                    ////JobLeavingReasonsLink.Visible = false;
                    //AttendanceLink.Visible = false;

                    CompanyInfoLink.Visible = false;
                    ReportsLink.Visible = false;
                    InventoryLink.Visible = false;
                    SettingsLink.Visible = false;

                    break;

                case 3:
                    //AddEmployeeLink.Visible = false;
                    //ModifyEmployeeLink.Visible = false;
                    //DeleteEmployeeLink.Visible = false;
                    //AssigningWorkerLink.Visible = false;
                    //AttendanceLink.Visible = true;
                    //LoanLink.Visible = true;
                    //PaymentLink.Visible = true;
                    ////TrainingEmployeeLink.Visible = false;
                    //PostingOrderListLink.Visible = true;
                    ////JobLeavingReasonsLink.Visible = false;

                    ClientsLink.Visible = true;
                    CompanyInfoLink.Visible = false;
                    InventoryLink.Visible = false;
                    ReportsLink.Visible = true;
                    SettingsLink.Visible = true;
                    break;

                case 4:

                    //AddEmployeeLink.Visible = false;
                    //ModifyEmployeeLink.Visible = false;
                    //DeleteEmployeeLink.Visible = false;
                    //AssigningWorkerLink.Visible = false;
                    //LoanLink.Visible = true;
                    //PaymentLink.Visible = true;
                    ////TrainingEmployeeLink.Visible = false;
                    ////JobLeavingReasonsLink.Visible = false;
                    //AttendanceLink.Visible = false;

                    CompanyInfoLink.Visible = true;
                    ReportsLink.Visible = false;
                    InventoryLink.Visible = false;
                    SettingsLink.Visible = false;
                    break;
                case 5:

                    //AddEmployeeLink.Visible = false;
                    //ModifyEmployeeLink.Visible = false;
                    //DeleteEmployeeLink.Visible = false;
                    //AssigningWorkerLink.Visible = false;
                    //LoanLink.Visible = true;
                    //PaymentLink.Visible = true;
                    ////TrainingEmployeeLink.Visible = false;
                    ////JobLeavingReasonsLink.Visible = false;
                    //AttendanceLink.Visible = true;

                    CompanyInfoLink.Visible = true;
                    ReportsLink.Visible = false;
                    InventoryLink.Visible = false;
                    SettingsLink.Visible = false;
                    break;
                case 6:

                    //AddEmployeeLink.Visible = false;
                    //ModifyEmployeeLink.Visible = false;
                    //DeleteEmployeeLink.Visible = false;
                    //AssigningWorkerLink.Visible = false;
                    //LoanLink.Visible = false;
                    //PaymentLink.Visible = false;
                    ////TrainingEmployeeLink.Visible = false;
                    ////JobLeavingReasonsLink.Visible = false;
                    //AttendanceLink.Visible = true;

                    CompanyInfoLink.Visible = false;
                    ReportsLink.Visible = false;
                    InventoryLink.Visible = false;
                    SettingsLink.Visible = false;
                    break;
                default:
                    break;
            }
        }

        protected void LoadClientNames()
        {
            DataTable DtClientids = GlobalData.Instance.LoadCNames(CmpIDPrefix,BranchID);
            if (DtClientids.Rows.Count > 0)
            {
                ddlcname.DataValueField = "Clientid";
                ddlcname.DataTextField = "clientname";
                ddlcname.DataSource = DtClientids;
                ddlcname.DataBind();
            }
            ddlcname.Items.Insert(0, "-Select-");
            ddlcname.Items.Insert(1, "ALL");

        }

        protected void LoadClientList()
        {
            DataTable DtClientNames = GlobalData.Instance.LoadCIds(CmpIDPrefix,BranchID);
            if (DtClientNames.Rows.Count > 0)
            {
                ddlclient.DataValueField = "Clientid";
                ddlclient.DataTextField = "Clientid";
                ddlclient.DataSource = DtClientNames;
                ddlclient.DataBind();
            }
            ddlclient.Items.Insert(0, "-Select-");
            ddlclient.Items.Insert(1, "ALL");
        }

        protected void GetWebConfigdata()
        {

            EmpIDPrefix = Session["EmpIDPrefix"].ToString();
            CmpIDPrefix = Session["CmpIDPrefix"].ToString();
            BranchID = Session["BranchID"].ToString();

        }



        protected void ddlclient_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlclient.SelectedIndex > 0)
            {
                ddlcname.SelectedValue = ddlclient.SelectedValue;

                // DisplayData();
            }
            else
            {
                // Cleardata();
            }
        }

        protected void ddlcname_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlcname.SelectedIndex > 0)
            {
                ddlclient.SelectedValue = ddlcname.SelectedValue;
                // DisplayData();
            }
            else
            {
                // Cleardata();
            }
        }

        protected void btnwithotsheet_Click(object sender, EventArgs e)
        {
            int titleofdocumentindex = 0;
            if (ddlclient.SelectedIndex <= 0)
            {
                return;
            }

            string month = Getmonth();
            if (month.Trim().Length == 0)
            {
                return;
            }
            string selectmonth = string.Empty;

            if (ddlnoofattendance.SelectedIndex == 0)
            {
                selectmonth = "select Eps.EmpId,Eps.Desgn,TempGross as salaryrate, Eps.NoOfDuties,Eps.Basic,Eps.DA,Eps.HRA,Eps.CCA, " +
                    " Eps.Conveyance,Eps.WashAllowance,Eps.OtherAllowance,Eps.PF,Eps.ESI,Eps.OTAmt,Eps.ProfTax,Eps.Bonus," +
               "Eps.SalAdvDed,Eps.CanteenAdv,Eps.UniformDed,Eps.Penalty,Eps.OtherDed,Eps.Gross,Eps.Deductions,Eps.ActualAmount, " +
               " Eps.OWF,EmpDetails.EmpFName,EmpDetails.EmpMName," +
               "EmpDetails.UnitId, EmpDetails.EmpBankAppNo,EmpDetails.EmpBankAcNo, EmpDetails.EmpBankCardRef,EmpAttendance.OT from  " +
               " EmpPaySheet as Eps INNER JOIN EmpDetails ON Eps.EmpId = EmpDetails.EmpId INNER JOIN EmpAttendance ON EmpAttendance.EmpId=Eps.EmpId  " +
               " And  Eps.NoOfDuties>10   AND EmpAttendance.ClientId=Eps.ClientId AND EmpAttendance.Month=Eps.Month AND Eps.ClientId='" +
               ddlclient.SelectedValue + "' AND Eps.Month=" + month + "  and EmpAttendance.Design=Eps.Desgn     Order by Right(Eps.EmpId,6)";
            }

            if (ddlnoofattendance.SelectedIndex == 1)
            {
                selectmonth = "select Eps.EmpId,Eps.Desgn,TempGross as salaryrate, Eps.NoOfDuties,Eps.Basic,Eps.DA,Eps.HRA,Eps.CCA, " +
                     " Eps.Conveyance,Eps.WashAllowance,Eps.OtherAllowance,Eps.PF,Eps.ESI,Eps.OTAmt,Eps.ProfTax,Eps.Bonus," +
                "Eps.SalAdvDed,Eps.CanteenAdv,Eps.UniformDed,Eps.Penalty,Eps.OtherDed,Eps.Gross,Eps.Deductions,Eps.ActualAmount, " +
                " Eps.OWF,EmpDetails.EmpFName,EmpDetails.EmpMName," +
                "EmpDetails.UnitId, EmpDetails.EmpBankAppNo,EmpDetails.EmpBankAcNo, EmpDetails.EmpBankCardRef,EmpAttendance.OT from  " +
                " EmpPaySheet as Eps INNER JOIN EmpDetails ON Eps.EmpId = EmpDetails.EmpId INNER JOIN EmpAttendance ON EmpAttendance.EmpId=Eps.EmpId  " +
                " And  Eps.NoOfDuties<=10   AND EmpAttendance.ClientId=Eps.ClientId AND EmpAttendance.Month=Eps.Month AND Eps.ClientId='" +
                ddlclient.SelectedValue + "' AND Eps.Month=" + month + " and EmpAttendance.Design=Eps.Desgn    Order by Right(Eps.EmpId,6)";
            }
            if (ddlnoofattendance.SelectedIndex == 2)
            {
                selectmonth = "select Eps.EmpId,Eps.Desgn,TempGross as salaryrate, Eps.NoOfDuties,Eps.Basic,Eps.DA,Eps.HRA,Eps.CCA, " +
                     " Eps.Conveyance,Eps.WashAllowance,Eps.OtherAllowance,Eps.PF,Eps.ESI,Eps.OTAmt,Eps.ProfTax,Eps.Bonus," +
                "Eps.SalAdvDed,Eps.CanteenAdv,Eps.UniformDed,Eps.Penalty,Eps.OtherDed,Eps.Gross,Eps.Deductions,Eps.ActualAmount, " +
                " Eps.OWF,EmpDetails.EmpFName,EmpDetails.EmpMName," +
                "EmpDetails.UnitId, EmpDetails.EmpBankAppNo,EmpDetails.EmpBankAcNo, EmpDetails.EmpBankCardRef,EmpAttendance.OT from  " +
                " EmpPaySheet as Eps INNER JOIN EmpDetails ON Eps.EmpId = EmpDetails.EmpId INNER JOIN EmpAttendance ON EmpAttendance.EmpId=Eps.EmpId  " +
                "    AND EmpAttendance.ClientId=Eps.ClientId AND EmpAttendance.Month=Eps.Month AND Eps.ClientId='" +
                ddlclient.SelectedValue + "' AND Eps.Month=" + month + "  and EmpAttendance.Design=Eps.Desgn    Order by Right(Eps.EmpId,6)";
            }
            DataTable dt = config.ExecuteAdaptorAsyncWithQueryParams(selectmonth).Result;

            MemoryStream ms = new MemoryStream();
            if (dt.Rows.Count > 0)
            {
                Document document = new Document(PageSize.LEGAL.Rotate());
                PdfWriter writer = PdfWriter.GetInstance(document, ms);
                document.Open();
                document.AddTitle("FaMS");
                document.AddAuthor("WebWonders");
                document.AddSubject("Wage Sheet");
                document.AddKeywords("Keyword1, keyword2, …");//
                float forConvert;
                string strQry = "Select * from CompanyInfo";
                DataTable compInfo = config.ExecuteAdaptorAsyncWithQueryParams(strQry).Result;
                string companyName1 = "Your Company Name";
                string companyAddress = "Your Company Address";
                if (compInfo.Rows.Count > 0)
                {
                    companyName1 = compInfo.Rows[0]["CompanyName"].ToString();
                    companyAddress = compInfo.Rows[0]["Address"].ToString();
                }

                int tableCells = 27;
                #region variables for total
                float totalActualamount = 0;
                float totalDuties = 0;
                float totalOts = 0;
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
                float totalDed = 0;
                float totalSalAdv = 0;
                float totalUniforDed = 0;
                float totalCanteenAdv = 0;
                float totalOwf = 0;
                float totalPenalty = 0;
                float totalbonus = 0;

                #endregion


                #region variables for  Grand  total
                float GrandtotalActualamount = 0;
                float GrandtotalDuties = 0;
                float GrandtotalOts = 0;
                float GrandtotalBasic = 0;
                float GrandtotalDA = 0;
                float GrandtotalHRA = 0;
                float GrandtotalCCA = 0;
                float GrandtotalConveyance = 0;
                float GrandtotalWA = 0;
                float GrandtotalOA = 0;
                float GrandtotalGrass = 0;
                float GrandtotalOTAmount = 0;
                float GrandtotalPF = 0;
                float GrandtotalESI = 0;
                float GrandtotalProfTax = 0;
                float GrandtotalDed = 0;
                float GrandtotalSalAdv = 0;
                float GrandtotalUniforDed = 0;
                float GrandtotalCanteenAdv = 0;
                float GrandtotalOwf = 0;
                float GrandtotalPenalty = 0;
                float Grandtotalbonus = 0;

                #endregion

                int nextpagerecordscount = 0;
                int targetpagerecors = 10;
                int secondpagerecords = targetpagerecors + 3;
                bool nextpagehasPages = false;
                int j = 0;
                PdfPTable SecondtablecheckbyFooter = null;
                PdfPTable SecondtableFooter = null;
                PdfPTable SecondtableGrandtotalFooter = null;
                for (int nextpagei = 0; nextpagei < dt.Rows.Count; nextpagei++)
                {
                    nextpagehasPages = true;


                    #region Titles of Document
                    PdfPTable Maintable = new PdfPTable(tableCells);
                    Maintable.TotalWidth = 950f;
                    Maintable.LockedWidth = true;
                    float[] width = new float[] { 2f, 2f, 2f, 2f, 2f, 2f, 2.5f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f };
                    Maintable.SetWidths(width);
                    uint FONT_SIZE = 8;

                    //Company Name & vage act details

                    PdfPCell cellemp = new PdfPCell(new Phrase("  ", FontFactory.GetFont(Fontstyle, 10, Font.BOLD, BaseColor.BLACK)));
                    cellemp.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    cellemp.Colspan = tableCells;
                    cellemp.Border = 0;

                    PdfPCell Heading = new PdfPCell(new Phrase("Form - XVII REGISTER OF WAGES", FontFactory.GetFont(Fontstyle, 20, Font.BOLD, BaseColor.BLACK)));
                    Heading.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    Heading.Colspan = tableCells;
                    Heading.Border = 0;
                    Maintable.AddCell(Heading);



                    PdfPCell actDetails = new PdfPCell(new Phrase("(vide Rule 78(1) a(i) of Contract Labour (Reg. & abolition) Central & A.P. Rules)", FontFactory.GetFont(Fontstyle, 15, Font.BOLD, BaseColor.BLACK)));
                    actDetails.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    actDetails.Colspan = tableCells;
                    actDetails.Border = 0;// 15;
                    Maintable.AddCell(actDetails);

                    Maintable.AddCell(cellemp);
                    #endregion


                    #region Table Headings
                    PdfPCell companyName = new PdfPCell(new Phrase(companyName1, FontFactory.GetFont("Arial Black", 20, Font.BOLD, BaseColor.BLACK)));
                    companyName.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    companyName.Colspan = tableCells;
                    companyName.Border = 0;// 15;
                    Maintable.AddCell(companyName);

                    PdfPCell paySheet = new PdfPCell(new Phrase("Pay Sheet", FontFactory.GetFont(Fontstyle, 10, Font.BOLD | Font.UNDERLINE, BaseColor.BLACK)));
                    paySheet.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    paySheet.Colspan = tableCells;
                    paySheet.Border = 0;// 15;
                    Maintable.AddCell(paySheet);

                    PdfPCell CClient = new PdfPCell(new Phrase("Client ID :   " + ddlclient.SelectedValue, FontFactory.GetFont(Fontstyle, 10, Font.NORMAL, BaseColor.BLACK)));
                    CClient.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    CClient.Colspan = 3;
                    CClient.Border = 0;// 15;
                    Maintable.AddCell(CClient);

                    PdfPCell CClientName = new PdfPCell(new Phrase("Client Name :   " + ddlcname.SelectedItem, FontFactory.GetFont(Fontstyle, 10, Font.NORMAL, BaseColor.BLACK)));
                    CClientName.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    CClientName.Colspan = 10;
                    CClientName.Border = 0;// 15;
                    Maintable.AddCell(CClientName);

                    PdfPCell CDates = new PdfPCell(new Phrase("Payment for the period of : " + GetPaymentPeriod(ddlclient.SelectedValue), FontFactory.GetFont(Fontstyle, 9, Font.NORMAL, BaseColor.BLACK)));
                    CDates.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    CDates.Colspan = 6;
                    CDates.Border = 0;// 15;
                    Maintable.AddCell(CDates);

                    // PdfPCell CPaySheetDate = new PdfPCell(new Phrase("Pay Sheet Date :  " + DateTime.Now.Date.ToString("dd/MM/yyyy"), FontFactory.GetFont(Fontstyle, 10, Font.NORMAL, BaseColor.BLACK)));
                    PdfPCell CPaySheetDate = new PdfPCell(new Phrase(" ", FontFactory.GetFont(Fontstyle, 10, Font.NORMAL, BaseColor.BLACK)));
                    CPaySheetDate.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    CPaySheetDate.Colspan = 4;
                    CPaySheetDate.Border = 0;// 15;
                    Maintable.AddCell(CPaySheetDate);

                    PdfPCell CPayMonth = new PdfPCell(new Phrase("For the month of :   " + DateTime.Parse(txtmonth.Text, CultureInfo.GetCultureInfo("en-gb")).ToString("MMMM"), FontFactory.GetFont(Fontstyle, 10, Font.NORMAL, BaseColor.BLACK)));
                    CPayMonth.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    CPayMonth.Colspan = 4;
                    CPayMonth.Border = 0;// 15;
                    Maintable.AddCell(CPayMonth);

                    Maintable.AddCell(cellemp);
                    //document.Add(Maintable);

                    if (titleofdocumentindex == 0)
                    {
                        document.Add(Maintable);
                        titleofdocumentindex = 1;
                    }
                    PdfPTable SecondtableHeadings = new PdfPTable(tableCells);
                    SecondtableHeadings.TotalWidth = 950f;
                    SecondtableHeadings.LockedWidth = true;
                    float[] SecondHeadingsWidth = new float[] { 2f, 2f, 6f, 3f, 2f, 1.5f, 1.8f, 1.8f, 1.5f, 1.5f, 1.5f, 1.5f, 1.8f, 1.5f, 1.5f, 2f, 2f, 1.5f, 1.5f, 2f, 2f, 1.5f, 1.5f, 1.5f, 2f, 2f, 6f };
                    SecondtableHeadings.SetWidths(SecondHeadingsWidth);

                    //Cell Headings
                    //1
                    PdfPCell sNo = new PdfPCell(new Phrase("S.No ", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    sNo.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                    //sNo.Colspan = 1;
                    sNo.Border = 15;// 15;
                    SecondtableHeadings.AddCell(sNo);
                    //2
                    PdfPCell CEmpId = new PdfPCell(new Phrase("Emp Id", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    CEmpId.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    CEmpId.Border = 15;// 15;
                    SecondtableHeadings.AddCell(CEmpId);
                    //3
                    PdfPCell CEmpName = new PdfPCell(new Phrase("Emp Name", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    CEmpName.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    CEmpName.Border = 15;// 15;
                    SecondtableHeadings.AddCell(CEmpName);
                    //4
                    PdfPCell CDesgn = new PdfPCell(new Phrase("Desgn", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    CDesgn.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    CDesgn.Border = 15;
                    SecondtableHeadings.AddCell(CDesgn);
                    //5
                    PdfPCell CDuties = new PdfPCell(new Phrase("Dts", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    CDuties.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    CDuties.Border = 15;
                    SecondtableHeadings.AddCell(CDuties);
                    //6
                    PdfPCell COTs = new PdfPCell(new Phrase("Ots", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    COTs.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    COTs.Border = 15;
                    SecondtableHeadings.AddCell(COTs);
                    //7

                    PdfPCell CBasic = new PdfPCell(new Phrase("Basic", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    CBasic.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    CBasic.Border = 15;
                    SecondtableHeadings.AddCell(CBasic);
                    //8
                    PdfPCell CDa = new PdfPCell(new Phrase("DA", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    CBasic.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    CBasic.Border = 15;
                    SecondtableHeadings.AddCell(CDa);

                    //9

                    PdfPCell CHRa = new PdfPCell(new Phrase("HRA", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    CHRa.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    CHRa.Border = 15;
                    SecondtableHeadings.AddCell(CHRa);

                    //10
                    PdfPCell Cconveyance = new PdfPCell(new Phrase("Conv", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    Cconveyance.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    Cconveyance.Border = 15;
                    SecondtableHeadings.AddCell(Cconveyance);
                    //11
                    PdfPCell CCca = new PdfPCell(new Phrase("CCA", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    CCca.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    CCca.Border = 15;
                    SecondtableHeadings.AddCell(CCca);
                    //12
                    PdfPCell Cwa = new PdfPCell(new Phrase("WA", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    Cwa.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    Cwa.Border = 15;
                    SecondtableHeadings.AddCell(Cwa);
                    //13
                    PdfPCell COa = new PdfPCell(new Phrase("OA", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    COa.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    COa.Border = 15;
                    SecondtableHeadings.AddCell(COa);

                    //14
                    PdfPCell CBonus = new PdfPCell(new Phrase("Bonus", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    CBonus.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    CBonus.Border = 15;
                    SecondtableHeadings.AddCell(CBonus);

                    //15
                    PdfPCell Cotamt = new PdfPCell(new Phrase("OTamt", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    Cotamt.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    Cotamt.Border = 15;
                    SecondtableHeadings.AddCell(Cotamt);
                    //16

                    PdfPCell CGross = new PdfPCell(new Phrase("Gross", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    CGross.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    CGross.Border = 15;
                    SecondtableHeadings.AddCell(CGross);
                    //17
                    PdfPCell CPF = new PdfPCell(new Phrase("PF", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    CPF.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    CPF.Border = 15;
                    SecondtableHeadings.AddCell(CPF);
                    //18
                    PdfPCell CESI = new PdfPCell(new Phrase("ESI", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    CESI.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    CESI.Border = 15;
                    SecondtableHeadings.AddCell(CESI);
                    //19
                    PdfPCell CPT = new PdfPCell(new Phrase("PT", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    CPT.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    CPT.Border = 15;
                    SecondtableHeadings.AddCell(CPT);
                    //20
                    PdfPCell CSalAdv = new PdfPCell(new Phrase("Sal Adv", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    CSalAdv.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    CSalAdv.Border = 15;
                    SecondtableHeadings.AddCell(CSalAdv);
                    //21
                    PdfPCell CUnifDed = new PdfPCell(new Phrase("Unif. Ded", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    CUnifDed.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    CUnifDed.Border = 15;
                    SecondtableHeadings.AddCell(CUnifDed);

                    //22
                    PdfPCell Ccanteended = new PdfPCell(new Phrase("Mess. Ded", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    Ccanteended.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    Ccanteended.Border = 15;
                    SecondtableHeadings.AddCell(Ccanteended);

                    //23
                    PdfPCell COWF = new PdfPCell(new Phrase("OWF", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                    COWF.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    COWF.Border = 15;
                    SecondtableHeadings.AddCell(COWF);
                    //24
                    PdfPCell COtherDed = new PdfPCell(new Phrase("Other Ded", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    COtherDed.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    COtherDed.Border = 15;
                    SecondtableHeadings.AddCell(COtherDed);
                    //25
                    PdfPCell CTotDed = new PdfPCell(new Phrase("Tot Ded", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    CTotDed.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    CTotDed.Border = 15;
                    SecondtableHeadings.AddCell(CTotDed);
                    //26
                    PdfPCell CNetPay = new PdfPCell(new Phrase("Net Pay", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    CNetPay.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    CNetPay.Border = 15;
                    SecondtableHeadings.AddCell(CNetPay);
                    //27
                    PdfPCell CSignature = new PdfPCell(new Phrase("Bank A/c No./ Signature", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    CSignature.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    CSignature.Border = 15;
                    SecondtableHeadings.AddCell(CSignature);
                    #endregion


                    PdfPTable Secondtable = new PdfPTable(tableCells);
                    Secondtable.TotalWidth = 950f;
                    Secondtable.LockedWidth = true;
                    float[] SecondWidth = new float[] { 2f, 2f, 6f, 3f, 2f, 1.5f, 1.8f, 1.8f, 1.5f, 1.5f, 1.5f, 1.5f, 1.8f, 1.5f, 1.5f, 2f, 2f, 1.5f, 1.5f, 2f, 2f, 1.5f, 1.5f, 1.5f, 2f, 2f, 6f };
                    Secondtable.SetWidths(SecondWidth);


                    #region Table Data
                    int rowCount = 0;
                    int pageCount = 0;
                    int i = nextpagei;

                    // for (int i = 0, j = 0; i < dt.Rows.Count; i++)
                    {
                        forConvert = 0;
                        if (dt.Rows[i]["NoOfDuties"].ToString().Trim().Length > 0)
                            forConvert = Convert.ToSingle(dt.Rows[i]["NoOfDuties"].ToString());
                        if (forConvert > 0)
                        {

                            if (nextpagerecordscount == 0)
                            {
                                document.Add(SecondtableHeadings);
                            }

                            nextpagerecordscount++;
                            //1
                            PdfPCell CSNo = new PdfPCell(new Phrase((++j).ToString() + "\n \n \n \n", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                            CSNo.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            CSNo.Border = 15;
                            Secondtable.AddCell(CSNo);
                            //2
                            PdfPCell CEmpId1 = new PdfPCell(new Phrase(dt.Rows[i]["EmpId"].ToString(), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                            CEmpId1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            CEmpId1.Border = 15;
                            Secondtable.AddCell(CEmpId1);
                            //3
                            PdfPCell CEmpName1 = new PdfPCell(new Phrase(dt.Rows[i]["EmpFName"].ToString() + " " + dt.Rows[i]["EmpMName"].ToString(), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                            CEmpName1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            CEmpName1.Border = 15;
                            Secondtable.AddCell(CEmpName1);
                            //4
                            PdfPCell CEmpDesgn = new PdfPCell(new Phrase(dt.Rows[i]["Desgn"].ToString(), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                            CEmpDesgn.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            CEmpDesgn.Border = 15;
                            Secondtable.AddCell(CEmpDesgn);
                            //5
                            forConvert = 0;
                            if (dt.Rows[i]["NoOfDuties"].ToString().Trim().Length > 0)
                                forConvert = Convert.ToSingle(dt.Rows[i]["NoOfDuties"].ToString());
                            totalDuties += forConvert;
                            GrandtotalDuties += forConvert;

                            PdfPCell CNoOfDuties = new PdfPCell(new Phrase(forConvert.ToString("0.00"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                            CNoOfDuties.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            CNoOfDuties.Border = 15;
                            Secondtable.AddCell(CNoOfDuties);
                            //6
                            if (dt.Rows[i]["ot"].ToString().Trim().Length > 0)
                                forConvert = Convert.ToSingle(dt.Rows[i]["ot"].ToString());
                            totalOts += forConvert;
                            GrandtotalOts += forConvert;
                            PdfPCell CNoOfots = new PdfPCell(new Phrase(forConvert.ToString("0.00"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                            CNoOfots.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            CNoOfots.Border = 15;
                            Secondtable.AddCell(CNoOfots);

                            //7
                            forConvert = 0;
                            if (dt.Rows[i]["Basic"].ToString().Trim().Length > 0)
                                forConvert = (float)Math.Round(Convert.ToSingle(dt.Rows[i]["Basic"].ToString()));
                            totalBasic += forConvert;
                            GrandtotalBasic += forConvert;
                            PdfPCell CBasic1 = new PdfPCell(new Phrase(forConvert.ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                            CBasic1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            CBasic1.Border = 15;
                            Secondtable.AddCell(CBasic1);
                            //8

                            forConvert = 0;

                            if (dt.Rows[i]["DA"].ToString().Trim().Length > 0)
                                forConvert = (float)Math.Round(Convert.ToSingle(dt.Rows[i]["DA"].ToString()));
                            totalDA += forConvert;
                            GrandtotalDA += forConvert;

                            PdfPCell CDa1 = new PdfPCell(new Phrase(forConvert.ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                            CDa1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            CDa1.Border = 15;
                            Secondtable.AddCell(CDa1);

                            //9

                            forConvert = 0;
                            if (dt.Rows[i]["HRA"].ToString().Trim().Length > 0)
                                forConvert = (float)Math.Round(Convert.ToSingle(dt.Rows[i]["HRA"].ToString()));
                            totalHRA += forConvert;
                            GrandtotalHRA += forConvert;

                            PdfPCell CHRA1 = new PdfPCell(new Phrase(forConvert.ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                            CHRA1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            CHRA1.Border = 15;
                            Secondtable.AddCell(CHRA1);

                            //10
                            forConvert = 0;
                            if (dt.Rows[i]["Conveyance"].ToString().Trim().Length > 0)
                                forConvert = (float)Math.Round(Convert.ToSingle(dt.Rows[i]["Conveyance"].ToString()));
                            totalConveyance += forConvert;
                            GrandtotalConveyance += forConvert;



                            PdfPCell CConveyance = new PdfPCell(new Phrase(forConvert.ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                            CConveyance.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            CConveyance.Border = 15;
                            Secondtable.AddCell(CConveyance);

                            //11
                            forConvert = 0;
                            if (dt.Rows[i]["cca"].ToString().Trim().Length > 0)
                                forConvert = (float)Math.Round(Convert.ToSingle(dt.Rows[i]["cca"].ToString()));
                            totalCCA += forConvert;

                            GrandtotalCCA += forConvert;

                            PdfPCell Ccca = new PdfPCell(new Phrase(forConvert.ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                            Ccca.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            Ccca.Border = 15;
                            Secondtable.AddCell(Ccca);
                            //12
                            forConvert = 0;
                            if (dt.Rows[i]["washallowance"].ToString().Trim().Length > 0)
                                forConvert = (float)Math.Round(Convert.ToSingle(dt.Rows[i]["washallowance"].ToString()));
                            totalWA += forConvert;


                            GrandtotalWA += forConvert;

                            PdfPCell CWa = new PdfPCell(new Phrase(forConvert.ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                            CWa.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            CWa.Border = 15;
                            Secondtable.AddCell(CWa);

                            //13
                            forConvert = 0;
                            if (dt.Rows[i]["OtherAllowance"].ToString().Trim().Length > 0)
                                forConvert = (float)Math.Round(Convert.ToSingle(dt.Rows[i]["OtherAllowance"].ToString()));
                            totalOA += forConvert;
                            GrandtotalOA += forConvert;
                            PdfPCell COA = new PdfPCell(new Phrase(forConvert.ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                            COA.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            COA.Border = 15;
                            Secondtable.AddCell(COA);

                            //14
                            forConvert = 0;
                            if (dt.Rows[i]["bonus"].ToString().Trim().Length > 0)
                                forConvert = (float)Math.Round(Convert.ToSingle(dt.Rows[i]["bonus"].ToString()));
                            totalbonus += forConvert;

                            Grandtotalbonus += forConvert;
                            PdfPCell Cbonus = new PdfPCell(new Phrase(forConvert.ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                            Cbonus.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            Cbonus.Border = 15;
                            Secondtable.AddCell(Cbonus);


                            //15
                            forConvert = 0;
                            if (dt.Rows[i]["otamt"].ToString().Trim().Length > 0)
                                forConvert = (float)Math.Round(Convert.ToSingle(dt.Rows[i]["otamt"].ToString()));
                            totalOTAmount += forConvert;

                            Grandtotalbonus += forConvert;

                            PdfPCell Cottamt = new PdfPCell(new Phrase(forConvert.ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                            Cottamt.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            Cottamt.Border = 15;
                            Secondtable.AddCell(Cottamt);
                            //16
                            forConvert = 0;
                            if (dt.Rows[i]["Gross"].ToString().Trim().Length > 0)
                                forConvert = (float)Math.Round(Convert.ToSingle(dt.Rows[i]["Gross"].ToString()));
                            totalGrass += forConvert;
                            GrandtotalGrass += forConvert;

                            PdfPCell CGross1 = new PdfPCell(new Phrase(forConvert.ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                            CGross1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            CGross1.Border = 15;
                            Secondtable.AddCell(CGross1);
                            //17
                            forConvert = 0;
                            if (dt.Rows[i]["PF"].ToString().Trim().Length > 0)
                                forConvert = (float)Math.Round(Convert.ToSingle(dt.Rows[i]["PF"].ToString()));
                            totalPF += forConvert;
                            GrandtotalPF += forConvert;

                            PdfPCell CPF1 = new PdfPCell(new Phrase(forConvert.ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                            CPF1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            CPF1.Border = 15;
                            Secondtable.AddCell(CPF1);
                            //18
                            forConvert = 0;
                            if (dt.Rows[i]["ESI"].ToString().Trim().Length > 0)
                                forConvert = (float)Math.Round(Convert.ToSingle(dt.Rows[i]["ESI"].ToString()));
                            totalESI += forConvert;

                            GrandtotalESI += forConvert;

                            PdfPCell CESI1 = new PdfPCell(new Phrase(forConvert.ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                            CESI1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            CESI1.Border = 15;
                            Secondtable.AddCell(CESI1);
                            //19
                            forConvert = 0;
                            if (dt.Rows[i]["ProfTax"].ToString().Trim().Length > 0)
                                forConvert = (float)Math.Round(Convert.ToSingle(dt.Rows[i]["ProfTax"].ToString()));
                            totalProfTax += forConvert;
                            GrandtotalProfTax += forConvert;

                            PdfPCell CProTax1 = new PdfPCell(new Phrase(forConvert.ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                            CProTax1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            CProTax1.Border = 15;
                            Secondtable.AddCell(CProTax1);
                            //20
                            forConvert = 0;
                            if (dt.Rows[i]["SalAdvDed"].ToString().Trim().Length > 0)
                                forConvert = (float)Math.Round(Convert.ToSingle(dt.Rows[i]["SalAdvDed"].ToString()));
                            totalSalAdv += forConvert;
                            GrandtotalSalAdv += forConvert;

                            PdfPCell CSalAdv1 = new PdfPCell(new Phrase(forConvert.ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                            CSalAdv1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            CSalAdv1.Border = 15;
                            Secondtable.AddCell(CSalAdv1);
                            //21
                            forConvert = 0;
                            if (dt.Rows[i]["UniformDed"].ToString().Trim().Length > 0)
                                forConvert = (float)Math.Round(Convert.ToSingle(dt.Rows[i]["UniformDed"].ToString()));
                            totalUniforDed += forConvert;
                            GrandtotalUniforDed += forConvert;

                            PdfPCell CUnifDed1 = new PdfPCell(new Phrase(forConvert.ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                            CUnifDed1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            CUnifDed1.Border = 15;
                            Secondtable.AddCell(CUnifDed1);
                            //22

                            forConvert = 0;
                            if (dt.Rows[i]["CanteenAdv"].ToString().Trim().Length > 0)
                                forConvert = (float)Math.Round(Convert.ToSingle(dt.Rows[i]["CanteenAdv"].ToString()));
                            totalCanteenAdv += forConvert;
                            GrandtotalCanteenAdv += forConvert;

                            PdfPCell CCanteended = new PdfPCell(new Phrase(forConvert.ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                            CCanteended.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            CCanteended.Border = 15;
                            Secondtable.AddCell(CCanteended);

                            //23
                            if (dt.Rows[i]["OWF"].ToString().Trim().Length > 0)
                                forConvert = (float)Math.Round(Convert.ToSingle(dt.Rows[i]["OWF"].ToString()));
                            totalOwf += forConvert;
                            GrandtotalOwf += forConvert;

                            PdfPCell COwf1 = new PdfPCell(new Phrase(forConvert.ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                            COwf1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            COwf1.Border = 15;
                            Secondtable.AddCell(COwf1);
                            //24
                            forConvert = 0;


                            if (dt.Rows[i]["Penalty"].ToString().Trim().Length > 0)
                                forConvert = (float)Math.Round(Convert.ToSingle(dt.Rows[i]["Penalty"].ToString()));
                            totalPenalty += forConvert;
                            GrandtotalPenalty += forConvert;

                            PdfPCell COtherDed1 = new PdfPCell(new Phrase(forConvert.ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                            COtherDed1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            COtherDed1.Border = 15;
                            Secondtable.AddCell(COtherDed1);
                            //25
                            forConvert = 0;
                            if (dt.Rows[i]["Deductions"].ToString().Trim().Length > 0)
                                forConvert = (float)Math.Round(Convert.ToSingle(dt.Rows[i]["Deductions"].ToString()));
                            totalDed += forConvert;
                            GrandtotalDed += forConvert;

                            PdfPCell CTotDed1 = new PdfPCell(new Phrase(forConvert.ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                            CTotDed1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            CTotDed1.Border = 15;
                            Secondtable.AddCell(CTotDed1);//OtherDed,Eps.Gross,Eps.Deductions,Eps.ActualAmount
                            //26
                            forConvert = 0;
                            if (dt.Rows[i]["ActualAmount"].ToString().Trim().Length > 0)
                                forConvert = (float)Math.Round(Convert.ToSingle(dt.Rows[i]["ActualAmount"].ToString()));
                            totalActualamount += forConvert;
                            GrandtotalActualamount += forConvert;
                            PdfPCell CNetPay1 = new PdfPCell(new Phrase(forConvert.ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                            CNetPay1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            CNetPay1.Border = 15;
                            Secondtable.AddCell(CNetPay1);
                            //27
                            string EmpBankAcNo = dt.Rows[i]["EmpBankAcNo"].ToString();
                            PdfPCell CSignature1 = new PdfPCell(new Phrase(EmpBankAcNo, FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                            CSignature1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            CSignature1.Border = 15;
                            CSignature1.MinimumHeight = 25;
                            Secondtable.AddCell(CSignature1);
                        }


                    }
                    #endregion


                    SecondtableFooter = new PdfPTable(tableCells);
                    SecondtableFooter.TotalWidth = 950f;
                    SecondtableFooter.LockedWidth = true;
                    float[] SecondFooterWidth = new float[] { 2f, 2f, 6f, 3f, 2f, 1.5f, 1.8f, 1.8f, 1.5f, 1.5f, 1.5f, 1.5f, 1.8f, 1.5f, 1.5f, 2f, 2f, 1.5f, 1.5f, 2f, 2f, 1.5f, 1.5f, 1.5f, 2f, 2f, 6f };
                    SecondtableFooter.SetWidths(SecondFooterWidth);
                    #region Table Footer
                    //1
                    PdfPCell FCSNo = new PdfPCell(new Phrase("", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                    FCSNo.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCSNo.Border = 15;
                    SecondtableFooter.AddCell(FCSNo);
                    //2
                    PdfPCell FCEmpId1 = new PdfPCell(new Phrase("", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                    FCEmpId1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCEmpId1.Border = 15;
                    SecondtableFooter.AddCell(FCEmpId1);
                    //3
                    PdfPCell FCEmpName1 = new PdfPCell(new Phrase(" Total : ", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    FCEmpName1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCEmpName1.Border = 15;
                    SecondtableFooter.AddCell(FCEmpName1);
                    //4
                    PdfPCell FCEmpDesgn = new PdfPCell(new Phrase("", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                    FCEmpDesgn.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCEmpDesgn.Border = 15;
                    //FCEmpDesgn.Colspan = 4;
                    SecondtableFooter.AddCell(FCEmpDesgn);
                    //5
                    PdfPCell FCNoOfDuties = new PdfPCell(new Phrase(totalDuties.ToString("0.00"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    FCNoOfDuties.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCNoOfDuties.Border = 15;
                    SecondtableFooter.AddCell(FCNoOfDuties);
                    //6
                    PdfPCell FCNoOfots = new PdfPCell(new Phrase(totalOts.ToString("0.00"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    FCNoOfots.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCNoOfots.Border = 15;
                    SecondtableFooter.AddCell(FCNoOfots);

                    //7
                    PdfPCell FCBasic1 = new PdfPCell(new Phrase(Math.Round(totalBasic).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    FCBasic1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCBasic1.Border = 15;
                    SecondtableFooter.AddCell(FCBasic1);


                    //8
                    PdfPCell FDa = new PdfPCell(new Phrase(Math.Round(totalDA).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    FDa.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FDa.Border = 15;
                    SecondtableFooter.AddCell(FDa);


                    //9

                    forConvert = 0;
                    PdfPCell FCHRA1 = new PdfPCell(new Phrase(Math.Round(totalHRA).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    FCHRA1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCHRA1.Border = 15;
                    SecondtableFooter.AddCell(FCHRA1);

                    //10
                    PdfPCell FCConveyance = new PdfPCell(new Phrase(Math.Round(totalConveyance).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    FCConveyance.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCConveyance.Border = 15;
                    SecondtableFooter.AddCell(FCConveyance);

                    //11
                    PdfPCell FCcca = new PdfPCell(new Phrase(Math.Round(totalCCA).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    FCcca.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCcca.Border = 15;
                    SecondtableFooter.AddCell(FCcca);
                    //12
                    PdfPCell FCWa = new PdfPCell(new Phrase(Math.Round(totalWA).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    FCWa.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCWa.Border = 15;
                    SecondtableFooter.AddCell(FCWa);

                    //13
                    PdfPCell FCOa = new PdfPCell(new Phrase(Math.Round(totalOA).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    FCOa.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCOa.Border = 15;
                    SecondtableFooter.AddCell(FCOa);

                    //14
                    PdfPCell Fbonus = new PdfPCell(new Phrase(Math.Round(totalbonus).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    Fbonus.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    Fbonus.Border = 15;
                    SecondtableFooter.AddCell(Fbonus);

                    //15
                    PdfPCell FCottamt = new PdfPCell(new Phrase(Math.Round(totalOTAmount).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    FCottamt.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCottamt.Border = 15;
                    SecondtableFooter.AddCell(FCottamt);
                    //16
                    PdfPCell FCGross1 = new PdfPCell(new Phrase(Math.Round(totalGrass).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    FCGross1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCGross1.Border = 15;
                    SecondtableFooter.AddCell(FCGross1);
                    //17
                    PdfPCell FCPF1 = new PdfPCell(new Phrase(Math.Round(totalPF).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    FCPF1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCPF1.Border = 15;
                    SecondtableFooter.AddCell(FCPF1);
                    //18
                    PdfPCell FCESI1 = new PdfPCell(new Phrase(Math.Round(totalESI).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    FCESI1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCESI1.Border = 15;
                    SecondtableFooter.AddCell(FCESI1);
                    //19
                    PdfPCell FCProTax1 = new PdfPCell(new Phrase(Math.Round(totalProfTax).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    FCProTax1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCProTax1.Border = 15;
                    SecondtableFooter.AddCell(FCProTax1);
                    //20
                    PdfPCell FCSalAdv1 = new PdfPCell(new Phrase(Math.Round(totalSalAdv).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    FCSalAdv1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCSalAdv1.Border = 15;
                    SecondtableFooter.AddCell(FCSalAdv1);
                    //21
                    PdfPCell FCUnifDed1 = new PdfPCell(new Phrase(Math.Round(totalUniforDed).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    FCUnifDed1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCUnifDed1.Border = 15;
                    SecondtableFooter.AddCell(FCUnifDed1);
                    //22
                    PdfPCell FCCanteended = new PdfPCell(new Phrase(Math.Round(totalCanteenAdv).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    FCCanteended.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCCanteended.Border = 15;
                    SecondtableFooter.AddCell(FCCanteended);
                    //23
                    PdfPCell FCOwf1 = new PdfPCell(new Phrase(Math.Round(totalOwf).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    FCOwf1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCOwf1.Border = 15;
                    SecondtableFooter.AddCell(FCOwf1);
                    //24
                    PdfPCell FCOtherDed1 = new PdfPCell(new Phrase(Math.Round(totalPenalty).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    FCOtherDed1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCOtherDed1.Border = 15;
                    SecondtableFooter.AddCell(FCOtherDed1);
                    //25
                    PdfPCell FCTotDed1 = new PdfPCell(new Phrase(Math.Round(totalDed).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    FCTotDed1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCTotDed1.Border = 15;
                    SecondtableFooter.AddCell(FCTotDed1);//OtherDed,Eps.Gross,Eps.Deductions,Eps.ActualAmount
                    //26
                    PdfPCell FCNetPay1 = new PdfPCell(new Phrase(Math.Round(totalActualamount).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    FCNetPay1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCNetPay1.Border = 15;
                    //FCNetPay1.Colspan = 2;
                    SecondtableFooter.AddCell(FCNetPay1);
                    //27
                    PdfPCell FCSignature1 = new PdfPCell(new Phrase(" ", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                    FCSignature1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCSignature1.Border = 15;
                    //FCSignature1.MinimumHeight = 25;
                    SecondtableFooter.AddCell(FCSignature1);
                    #endregion


                    SecondtableGrandtotalFooter = new PdfPTable(tableCells);
                    SecondtableGrandtotalFooter.TotalWidth = 950f;
                    SecondtableGrandtotalFooter.LockedWidth = true;
                    float[] SecondGrandtotalFooterWidth = new float[] { 2f, 2f, 6f, 3f, 2f, 1.5f, 1.8f, 1.8f, 1.5f, 1.5f, 1.5f, 1.5f, 1.8f, 1.5f, 1.5f, 2f, 2f, 1.5f, 1.5f, 2f, 2f, 1.5f, 1.5f, 1.5f, 2f, 2f, 6f };
                    SecondtableGrandtotalFooter.SetWidths(SecondGrandtotalFooterWidth);

                    SecondtablecheckbyFooter = new PdfPTable(tableCells);
                    SecondtablecheckbyFooter.TotalWidth = 950f;
                    SecondtablecheckbyFooter.LockedWidth = true;
                    float[] SecondcheckbyFooterWidth = new float[] { 2f, 2f, 6f, 3f, 2f, 1.5f, 1.8f, 1.8f, 1.5f, 1.5f, 1.5f, 1.5f, 1.8f, 1.5f, 1.5f, 2f, 2f, 1.5f, 1.5f, 2f, 2f, 1.5f, 1.5f, 1.5f, 2f, 2f, 6f };
                    SecondtablecheckbyFooter.SetWidths(SecondcheckbyFooterWidth);


                    #region   Footer Headings

                    #region Table   Grand   Total  Footer
                    //1
                    PdfPCell GFCSNo = new PdfPCell(new Phrase("", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                    GFCSNo.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCSNo.Border = 15;
                    SecondtablecheckbyFooter.AddCell(GFCSNo);
                    //2
                    PdfPCell GFCEmpId1 = new PdfPCell(new Phrase("", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                    GFCEmpId1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCEmpId1.Border = 15;
                    SecondtablecheckbyFooter.AddCell(GFCEmpId1);
                    //3
                    PdfPCell GFCEmpName1 = new PdfPCell(new Phrase("Grand  Totals: ", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    GFCEmpName1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCEmpName1.Border = 15;
                    SecondtablecheckbyFooter.AddCell(GFCEmpName1);
                    //4
                    PdfPCell GFCEmpDesgn = new PdfPCell(new Phrase("", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                    GFCEmpDesgn.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCEmpDesgn.Border = 15;
                    //FCEmpDesgn.Colspan = 4;
                    SecondtablecheckbyFooter.AddCell(GFCEmpDesgn);
                    //5
                    PdfPCell GFCNoOfDuties = new PdfPCell(new Phrase(GrandtotalDuties.ToString("0.00"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    GFCNoOfDuties.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCNoOfDuties.Border = 15;
                    SecondtablecheckbyFooter.AddCell(GFCNoOfDuties);
                    //6
                    PdfPCell GFCNoOfots = new PdfPCell(new Phrase(GrandtotalOts.ToString("0.00"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    GFCNoOfots.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCNoOfots.Border = 15;
                    SecondtablecheckbyFooter.AddCell(GFCNoOfots);

                    //7
                    PdfPCell GFCBasic1 = new PdfPCell(new Phrase(Math.Round(GrandtotalBasic).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    GFCBasic1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCBasic1.Border = 15;
                    SecondtablecheckbyFooter.AddCell(GFCBasic1);


                    //8
                    PdfPCell GFDa = new PdfPCell(new Phrase(Math.Round(GrandtotalDA).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    GFDa.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFDa.Border = 15;
                    SecondtablecheckbyFooter.AddCell(GFDa);


                    //9

                    forConvert = 0;
                    PdfPCell GFCHRA1 = new PdfPCell(new Phrase(Math.Round(GrandtotalHRA).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    GFCHRA1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCHRA1.Border = 15;
                    SecondtablecheckbyFooter.AddCell(GFCHRA1);

                    //10
                    PdfPCell GFCConveyance = new PdfPCell(new Phrase(Math.Round(GrandtotalConveyance).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    GFCConveyance.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCConveyance.Border = 15;
                    SecondtablecheckbyFooter.AddCell(GFCConveyance);

                    //11
                    PdfPCell GFCcca = new PdfPCell(new Phrase(Math.Round(GrandtotalCCA).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    GFCcca.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCcca.Border = 15;
                    SecondtablecheckbyFooter.AddCell(GFCcca);
                    //12
                    PdfPCell GFCWa = new PdfPCell(new Phrase(Math.Round(GrandtotalWA).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    GFCWa.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCWa.Border = 15;
                    SecondtablecheckbyFooter.AddCell(GFCWa);

                    //13
                    PdfPCell GFCOa = new PdfPCell(new Phrase(Math.Round(GrandtotalOA).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    GFCOa.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCOa.Border = 15;
                    SecondtablecheckbyFooter.AddCell(GFCOa);

                    //14
                    PdfPCell GFbonus = new PdfPCell(new Phrase(Math.Round(Grandtotalbonus).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    GFbonus.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFbonus.Border = 15;
                    SecondtablecheckbyFooter.AddCell(GFbonus);

                    //15
                    PdfPCell GFCottamt = new PdfPCell(new Phrase(Math.Round(GrandtotalOTAmount).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    GFCottamt.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCottamt.Border = 15;
                    SecondtablecheckbyFooter.AddCell(GFCottamt);
                    //16
                    PdfPCell GFCGross1 = new PdfPCell(new Phrase(Math.Round(GrandtotalGrass).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    GFCGross1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCGross1.Border = 15;
                    SecondtablecheckbyFooter.AddCell(GFCGross1);
                    //17
                    PdfPCell GFCPF1 = new PdfPCell(new Phrase(Math.Round(GrandtotalPF).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    GFCPF1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCPF1.Border = 15;
                    SecondtablecheckbyFooter.AddCell(GFCPF1);
                    //18
                    PdfPCell GFCESI1 = new PdfPCell(new Phrase(Math.Round(GrandtotalESI).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    GFCESI1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCESI1.Border = 15;
                    SecondtablecheckbyFooter.AddCell(GFCESI1);
                    //19
                    PdfPCell GFCProTax1 = new PdfPCell(new Phrase(Math.Round(GrandtotalProfTax).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    GFCProTax1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCProTax1.Border = 15;
                    SecondtablecheckbyFooter.AddCell(GFCProTax1);
                    //20
                    PdfPCell GFCSalAdv1 = new PdfPCell(new Phrase(Math.Round(GrandtotalSalAdv).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    GFCSalAdv1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCSalAdv1.Border = 15;
                    SecondtablecheckbyFooter.AddCell(GFCSalAdv1);
                    //21
                    PdfPCell GFCUnifDed1 = new PdfPCell(new Phrase(Math.Round(GrandtotalUniforDed).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    GFCUnifDed1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCUnifDed1.Border = 15;
                    SecondtablecheckbyFooter.AddCell(GFCUnifDed1);
                    //22
                    PdfPCell GFCCanteended = new PdfPCell(new Phrase(Math.Round(GrandtotalCanteenAdv).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    GFCCanteended.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCCanteended.Border = 15;
                    SecondtablecheckbyFooter.AddCell(GFCCanteended);
                    //23
                    PdfPCell GFCOwf1 = new PdfPCell(new Phrase(Math.Round(GrandtotalOwf).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    GFCOwf1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCOwf1.Border = 15;
                    SecondtablecheckbyFooter.AddCell(GFCOwf1);
                    //24
                    PdfPCell GFCOtherDed1 = new PdfPCell(new Phrase(Math.Round(GrandtotalPenalty).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    GFCOtherDed1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCOtherDed1.Border = 15;
                    SecondtablecheckbyFooter.AddCell(GFCOtherDed1);
                    //25
                    PdfPCell GFCTotDed1 = new PdfPCell(new Phrase(Math.Round(GrandtotalDed).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    GFCTotDed1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCTotDed1.Border = 15;
                    SecondtablecheckbyFooter.AddCell(GFCTotDed1);//OtherDed,Eps.Gross,Eps.Deductions,Eps.ActualAmount
                    //26
                    PdfPCell GFCNetPay1 = new PdfPCell(new Phrase(Math.Round(GrandtotalActualamount).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    GFCNetPay1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCNetPay1.Border = 15;
                    //FCNetPay1.Colspan = 2;
                    SecondtablecheckbyFooter.AddCell(GFCNetPay1);
                    //27
                    PdfPCell GFCSignature1 = new PdfPCell(new Phrase(" ", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                    GFCSignature1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCSignature1.Border = 15;
                    //FCSignature1.MinimumHeight = 25;
                    SecondtablecheckbyFooter.AddCell(GFCSignature1);
                    #endregion

                    //1
                    PdfPCell FHCheckedbybr1 = new PdfPCell(new Phrase("", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                    FHCheckedbybr1.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    FHCheckedbybr1.Border = 0;
                    FHCheckedbybr1.Rowspan = 0;
                    FHCheckedbybr1.Colspan = 14;
                    SecondtablecheckbyFooter.AddCell(FHCheckedbybr1);
                    //2
                    PdfPCell FHApprovedbr2 = new PdfPCell(new Phrase("  ", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                    FHApprovedbr2.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    FHApprovedbr2.Border = 0;
                    FHApprovedbr2.Colspan = 13;

                    SecondtablecheckbyFooter.AddCell(FHApprovedbr2);
                    SecondtablecheckbyFooter.AddCell(FHCheckedbybr1);
                    SecondtablecheckbyFooter.AddCell(FHApprovedbr2);


                    //1
                    PdfPCell FHCheckedby = new PdfPCell(new Phrase("Checked By", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                    FHCheckedby.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    FHCheckedby.Border = 0;
                    FHCheckedby.Rowspan = 0;
                    FHCheckedby.Colspan = 14;
                    SecondtablecheckbyFooter.AddCell(FHCheckedby);
                    //2
                    PdfPCell FHApprovedBy = new PdfPCell(new Phrase("Gross  Approved By", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                    FHApprovedBy.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    FHApprovedBy.Border = 0;
                    FHApprovedBy.Colspan = 13;
                    SecondtablecheckbyFooter.AddCell(FHApprovedBy);



                    #endregion

                    document.Add(Secondtable);

                    if (nextpagerecordscount == targetpagerecors)
                    {
                        targetpagerecors = secondpagerecords;
                        document.Add(SecondtableFooter);
                        document.NewPage();
                        nextpagerecordscount = 0;
                        #region  Zero variables

                        totalActualamount = 0;
                        totalDuties = 0;
                        totalOts = 0;
                        totalBasic = 0;
                        totalDA = 0;
                        totalHRA = 0;
                        totalCCA = 0;
                        totalConveyance = 0;
                        totalWA = 0;
                        totalOA = 0;
                        totalGrass = 0;
                        totalOTAmount = 0;
                        totalPF = 0;
                        totalESI = 0;
                        totalProfTax = 0;
                        totalDed = 0;
                        totalSalAdv = 0;
                        totalUniforDed = 0;
                        totalCanteenAdv = 0;
                        totalOwf = 0;
                        totalPenalty = 0;
                        totalbonus = 0;

                        #endregion
                    }
                }

                if (nextpagerecordscount >= 0)
                {
                    document.Add(SecondtableFooter);
                    document.Add(SecondtableGrandtotalFooter);
                    document.Add(SecondtablecheckbyFooter);
                }
                document.Close();
                if (nextpagehasPages)
                {

                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=Wage Sheet.pdf");
                    Response.Buffer = true;
                    Response.Clear();
                    Response.OutputStream.Write(ms.GetBuffer(), 0, ms.GetBuffer().Length);
                    Response.OutputStream.Flush();
                    Response.End();
                }
            }
        }


        protected void btnwithotsheet2_Click(object sender, EventArgs e)
        {
            int titleofdocumentindex = 0;
            if (ddlclient.SelectedIndex <= 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showlalert", "alert('Please Select Client ID/Name');", true);
                return;
            }

            string month = Getmonth();
            if (month.Trim().Length == 0)
            {
                return;
            }
            string selectmonth = string.Empty;
            if (ddlnoofattendance.SelectedIndex == 0)
            {

                selectmonth = "select Eps.EmpId,Eps.Desgn,TempGross as salaryrate, Eps.NoOfDuties,Eps.Basic,Eps.DA,Eps.HRA,Eps.CCA, " +
                    " Eps.Conveyance,Eps.WashAllowance,Eps.OtherAllowance,Eps.PF,Eps.ESI,Eps.OTAmt,Eps.ProfTax,Eps.Bonus,Eps.Incentivs," +
               "Eps.SalAdvDed,Eps.CanteenAdv,Eps.UniformDed,Eps.Penalty,Eps.OtherDed,Eps.Gross,Eps.Deductions,Eps.ActualAmount,Eps.OWF, " +
               " EmpDetails.EmpFName,EmpDetails.EmpMName," +
               "EmpDetails.UnitId, EmpDetails.EmpBankAppNo,EmpDetails.EmpBankAcNo, EmpDetails.EmpBankCardRef,EmpAttendance.OT from  " +
               " EmpPaySheet as Eps INNER JOIN EmpDetails ON Eps.EmpId = EmpDetails.EmpId INNER JOIN EmpAttendance ON EmpAttendance.EmpId=Eps.EmpId  " +
               " And  Eps.NoOfDuties>10 AND EmpAttendance.ClientId=Eps.ClientId AND EmpAttendance.Month=Eps.Month AND Eps.ClientId='" +
               ddlclient.SelectedValue + "' AND Eps.Month=" + month + " and EmpAttendance.Design=Eps.Desgn     Order by Right(Eps.EmpId,6)";
            }
            if (ddlnoofattendance.SelectedIndex == 1)
            {
                selectmonth = "select Eps.EmpId,Eps.Desgn,TempGross as salaryrate, Eps.NoOfDuties,Eps.Basic,Eps.DA,Eps.HRA,Eps.CCA, " +
               " Eps.Conveyance,Eps.WashAllowance,Eps.OtherAllowance,Eps.PF,Eps.ESI,Eps.OTAmt,Eps.ProfTax,Eps.Bonus,Eps.Incentivs," +
          "Eps.SalAdvDed,Eps.CanteenAdv,Eps.UniformDed,Eps.Penalty,Eps.OtherDed,Eps.Gross,Eps.Deductions,Eps.ActualAmount,Eps.OWF, " +
          " EmpDetails.EmpFName,EmpDetails.EmpMName," +
          "EmpDetails.UnitId, EmpDetails.EmpBankAppNo,EmpDetails.EmpBankAcNo, EmpDetails.EmpBankCardRef,EmpAttendance.OT from  " +
          " EmpPaySheet as Eps INNER JOIN EmpDetails ON Eps.EmpId = EmpDetails.EmpId INNER JOIN EmpAttendance ON EmpAttendance.EmpId=Eps.EmpId  " +
          " And  Eps.NoOfDuties<=10 AND EmpAttendance.ClientId=Eps.ClientId AND EmpAttendance.Month=Eps.Month AND Eps.ClientId='" +
          ddlclient.SelectedValue + "' AND Eps.Month=" + month + "  and EmpAttendance.Design=Eps.Desgn    Order by Right(Eps.EmpId,6)";
            }


            if (ddlnoofattendance.SelectedIndex == 2)
            {
                selectmonth = "select Eps.EmpId,Eps.Desgn,TempGross as salaryrate, Eps.NoOfDuties,Eps.Basic,Eps.DA,Eps.HRA,Eps.CCA, " +
               " Eps.Conveyance,Eps.WashAllowance,Eps.OtherAllowance,Eps.PF,Eps.ESI,Eps.OTAmt,Eps.ProfTax,Eps.Bonus,Eps.Incentivs," +
          "Eps.SalAdvDed,Eps.CanteenAdv,Eps.UniformDed,Eps.Penalty,Eps.OtherDed,Eps.Gross,Eps.Deductions,Eps.ActualAmount,Eps.OWF, " +
          " EmpDetails.EmpFName,EmpDetails.EmpMName," +
          "EmpDetails.UnitId, EmpDetails.EmpBankAppNo,EmpDetails.EmpBankAcNo, EmpDetails.EmpBankCardRef,EmpAttendance.OT from  " +
          " EmpPaySheet as Eps INNER JOIN EmpDetails ON Eps.EmpId = EmpDetails.EmpId INNER JOIN EmpAttendance ON EmpAttendance.EmpId=Eps.EmpId  " +
          " AND EmpAttendance.ClientId=Eps.ClientId AND EmpAttendance.Month=Eps.Month AND Eps.ClientId='" +
          ddlclient.SelectedValue + "' AND Eps.Month=" + month + " and EmpAttendance.Design=Eps.Desgn     Order by Right(Eps.EmpId,6)";
            }
            DataTable dt = config.ExecuteAdaptorAsyncWithQueryParams(selectmonth).Result;


            MemoryStream ms = new MemoryStream();
            if (dt.Rows.Count > 0)
            {
                Document document = new Document(PageSize.LEGAL.Rotate());
                PdfWriter writer = PdfWriter.GetInstance(document, ms);
                document.Open();
                document.AddTitle("FaMS");
                document.AddAuthor("WebWonders");
                document.AddSubject("Wage Sheet");
                document.AddKeywords("Keyword1, keyword2, …");//
                float forConvert;
                string strQry = "Select * from CompanyInfo";
                DataTable compInfo = config.ExecuteAdaptorAsyncWithQueryParams(strQry).Result;
                string companyName1 = "Your Company Name";
                string companyAddress = "Your Company Address";
                if (compInfo.Rows.Count > 0)
                {
                    companyName1 = compInfo.Rows[0]["CompanyName"].ToString();
                    companyAddress = compInfo.Rows[0]["Address"].ToString();
                }

                int tableCells = 28;
                #region variables for total
                float totalActualamount = 0;
                float totalDuties = 0;
                float totalOts = 0;
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
                float totalDed = 0;
                float totalSalAdv = 0;
                float totalUniforDed = 0;
                float totalCanteenAdv = 0;
                float totalOwf = 0;
                float totalPenalty = 0;
                float totalbonus = 0;
                float totalIncentivs = 0;

                #endregion


                #region variables for  Grand  total
                float GrandtotalActualamount = 0;
                float GrandtotalDuties = 0;
                float GrandtotalOts = 0;
                float GrandtotalBasic = 0;
                float GrandtotalDA = 0;
                float GrandtotalHRA = 0;
                float GrandtotalCCA = 0;
                float GrandtotalConveyance = 0;
                float GrandtotalWA = 0;
                float GrandtotalOA = 0;
                float GrandtotalGrass = 0;
                float GrandtotalOTAmount = 0;
                float GrandtotalPF = 0;
                float GrandtotalESI = 0;
                float GrandtotalProfTax = 0;
                float GrandtotalDed = 0;
                float GrandtotalSalAdv = 0;
                float GrandtotalUniforDed = 0;
                float GrandtotalCanteenAdv = 0;
                float GrandtotalOwf = 0;
                float GrandtotalPenalty = 0;
                float Grandtotalbonus = 0;
                float GrandtotalIncentivs = 0;

                #endregion

                int nextpagerecordscount = 0;
                int targetpagerecors = 10;
                int secondpagerecords = targetpagerecors + 3;
                bool nextpagehasPages = false;
                int j = 0;
                PdfPTable SecondtablecheckbyFooter = null;
                PdfPTable SecondtableFooter = null;
                PdfPTable SecondtableGrandtotalFooter = null;
                for (int nextpagei = 0; nextpagei < dt.Rows.Count; nextpagei++)
                {
                    nextpagehasPages = true;

                    #region Titles of Document
                    PdfPTable Maintable = new PdfPTable(tableCells);
                    Maintable.TotalWidth = 950f;
                    Maintable.LockedWidth = true;
                    float[] width = new float[] { 2f, 2f, 2f, 2f, 2f, 2f, 2.5f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f };
                    Maintable.SetWidths(width);
                    uint FONT_SIZE = 8;

                    //Company Name & vage act details

                    PdfPCell cellemp = new PdfPCell(new Phrase("  ", FontFactory.GetFont(Fontstyle, 10, Font.BOLD, BaseColor.BLACK)));
                    cellemp.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    cellemp.Colspan = tableCells;
                    cellemp.Border = 0;

                    PdfPCell Heading = new PdfPCell(new Phrase("Form - XVII REGISTER OF WAGES", FontFactory.GetFont(Fontstyle, 20, Font.BOLD, BaseColor.BLACK)));
                    Heading.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    Heading.Colspan = tableCells;
                    Heading.Border = 0;
                    Maintable.AddCell(Heading);

                    PdfPCell actDetails = new PdfPCell(new Phrase("(vide Rule 78(1) a(i) of Contract Labour (Reg. & abolition) Central & A.P. Rules)", FontFactory.GetFont(Fontstyle, 15, Font.BOLD, BaseColor.BLACK)));
                    actDetails.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    actDetails.Colspan = tableCells;
                    actDetails.Border = 0;// 15;
                    Maintable.AddCell(actDetails);

                    Maintable.AddCell(cellemp);
                    #endregion

                    #region Table Headings
                    PdfPCell companyName = new PdfPCell(new Phrase(companyName1, FontFactory.GetFont("Arial Black", 20, Font.BOLD, BaseColor.BLACK)));
                    companyName.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    companyName.Colspan = tableCells;
                    companyName.Border = 0;// 15;
                    Maintable.AddCell(companyName);

                    PdfPCell paySheet = new PdfPCell(new Phrase("Pay Sheet", FontFactory.GetFont(Fontstyle, 10, Font.BOLD | Font.UNDERLINE, BaseColor.BLACK)));
                    paySheet.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    paySheet.Colspan = tableCells;
                    paySheet.Border = 0;// 15;
                    Maintable.AddCell(paySheet);

                    PdfPCell CClient = new PdfPCell(new Phrase("Client ID :   " + ddlclient.SelectedValue, FontFactory.GetFont(Fontstyle, 10, Font.NORMAL, BaseColor.BLACK)));
                    CClient.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    CClient.Colspan = 3;
                    CClient.Border = 0;// 15;
                    Maintable.AddCell(CClient);

                    PdfPCell CClientName = new PdfPCell(new Phrase("Client Name :   " + ddlcname.SelectedItem, FontFactory.GetFont(Fontstyle, 10, Font.NORMAL, BaseColor.BLACK)));
                    CClientName.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    CClientName.Colspan = 10;
                    CClientName.Border = 0;// 15;
                    Maintable.AddCell(CClientName);

                    PdfPCell CDates = new PdfPCell(new Phrase("Payment for the period of : " + GetPaymentPeriod(ddlclient.SelectedValue), FontFactory.GetFont(Fontstyle, 9, Font.NORMAL, BaseColor.BLACK)));
                    CDates.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    CDates.Colspan = 6;
                    CDates.Border = 0;// 15;
                    Maintable.AddCell(CDates);


                    //PdfPCell CPaySheetDate = new PdfPCell(new Phrase("Pay Sheet Date :  " + DateTime.Now.Date.ToString("dd/MM/yyyy"), FontFactory.GetFont(Fontstyle, 10, Font.NORMAL, BaseColor.BLACK)));
                    PdfPCell CPaySheetDate = new PdfPCell(new Phrase(" ", FontFactory.GetFont(Fontstyle, 10, Font.NORMAL, BaseColor.BLACK)));
                    CPaySheetDate.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    CPaySheetDate.Colspan = 4;
                    CPaySheetDate.Border = 0;// 15;
                    Maintable.AddCell(CPaySheetDate);

                    PdfPCell CPayMonth = new PdfPCell(new Phrase("For the month of :   " + DateTime.Parse(txtmonth.Text, CultureInfo.GetCultureInfo("en-gb")).ToString("MMMM"), FontFactory.GetFont(Fontstyle, 10, Font.NORMAL, BaseColor.BLACK)));
                    CPayMonth.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    CPayMonth.Colspan = 5;
                    CPayMonth.Border = 0;// 15;
                    Maintable.AddCell(CPayMonth);

                    Maintable.AddCell(cellemp);
                    //document.Add(Maintable);

                    if (titleofdocumentindex == 0)
                    {
                        document.Add(Maintable);
                        titleofdocumentindex = 1;
                    }
                    PdfPTable SecondtableHeadings = new PdfPTable(tableCells);
                    SecondtableHeadings.TotalWidth = 950f;
                    SecondtableHeadings.LockedWidth = true;
                    float[] SecondHeadingsWidth = new float[] { 2f, 2f, 6f, 3f, 2f, 1.5f, 1.8f, 1.8f, 1.5f, 1.5f, 1.5f, 1.5f, 1.8f, 1.5f, 1.5f, 1.5f, 2f, 2f, 1.5f, 1.5f, 2f, 2f, 1.5f, 1.5f, 1.5f, 2f, 2f, 6f };
                    SecondtableHeadings.SetWidths(SecondHeadingsWidth);

                    //Cell Headings
                    //1
                    PdfPCell sNo = new PdfPCell(new Phrase("S.No ", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    sNo.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                    //sNo.Colspan = 1;
                    sNo.Border = 15;// 15;
                    SecondtableHeadings.AddCell(sNo);
                    //2
                    PdfPCell CEmpId = new PdfPCell(new Phrase("Emp Id", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    CEmpId.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    CEmpId.Border = 15;// 15;
                    SecondtableHeadings.AddCell(CEmpId);
                    //3
                    PdfPCell CEmpName = new PdfPCell(new Phrase("Emp Name", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    CEmpName.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    CEmpName.Border = 15;// 15;
                    SecondtableHeadings.AddCell(CEmpName);
                    //4
                    PdfPCell CDesgn = new PdfPCell(new Phrase("Desgn", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    CDesgn.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    CDesgn.Border = 15;
                    SecondtableHeadings.AddCell(CDesgn);
                    //5
                    PdfPCell CDuties = new PdfPCell(new Phrase("Dts", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    CDuties.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    CDuties.Border = 15;
                    SecondtableHeadings.AddCell(CDuties);
                    //6
                    PdfPCell COTs = new PdfPCell(new Phrase("Ots", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    COTs.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    COTs.Border = 15;
                    SecondtableHeadings.AddCell(COTs);
                    //7

                    PdfPCell CBasic = new PdfPCell(new Phrase("Basic", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    CBasic.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    CBasic.Border = 15;
                    SecondtableHeadings.AddCell(CBasic);
                    //8
                    PdfPCell CDa = new PdfPCell(new Phrase("DA", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    CBasic.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    CBasic.Border = 15;
                    SecondtableHeadings.AddCell(CDa);

                    //9

                    PdfPCell CHRa = new PdfPCell(new Phrase("HRA", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    CHRa.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    CHRa.Border = 15;
                    SecondtableHeadings.AddCell(CHRa);

                    //10
                    PdfPCell Cconveyance = new PdfPCell(new Phrase("Conv", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    Cconveyance.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    Cconveyance.Border = 15;
                    SecondtableHeadings.AddCell(Cconveyance);
                    //11
                    PdfPCell CCca = new PdfPCell(new Phrase("CCA", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    CCca.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    CCca.Border = 15;
                    SecondtableHeadings.AddCell(CCca);
                    //12
                    PdfPCell Cwa = new PdfPCell(new Phrase("WA", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    Cwa.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    Cwa.Border = 15;
                    SecondtableHeadings.AddCell(Cwa);
                    //13
                    PdfPCell COa = new PdfPCell(new Phrase("OA", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    COa.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    COa.Border = 15;
                    SecondtableHeadings.AddCell(COa);

                    //14
                    PdfPCell CBonus = new PdfPCell(new Phrase("Bonus", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    CBonus.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    CBonus.Border = 15;
                    SecondtableHeadings.AddCell(CBonus);

                    //15
                    PdfPCell Cotamt = new PdfPCell(new Phrase("OTamt", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    Cotamt.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    Cotamt.Border = 15;
                    SecondtableHeadings.AddCell(Cotamt);
                    //16
                    PdfPCell CIncentivs = new PdfPCell(new Phrase("Incentivs", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    CIncentivs.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    CIncentivs.Border = 15;
                    SecondtableHeadings.AddCell(CIncentivs);
                    //17
                    PdfPCell CGross = new PdfPCell(new Phrase("Gross", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    CGross.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    CGross.Border = 15;
                    SecondtableHeadings.AddCell(CGross);
                    //18
                    PdfPCell CPF = new PdfPCell(new Phrase("PF", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    CPF.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    CPF.Border = 15;
                    SecondtableHeadings.AddCell(CPF);
                    //19
                    PdfPCell CESI = new PdfPCell(new Phrase("ESI", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    CESI.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    CESI.Border = 15;
                    SecondtableHeadings.AddCell(CESI);
                    //20
                    PdfPCell CPT = new PdfPCell(new Phrase("PT", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    CPT.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    CPT.Border = 15;
                    SecondtableHeadings.AddCell(CPT);
                    //21
                    PdfPCell CSalAdv = new PdfPCell(new Phrase("Sal Adv", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    CSalAdv.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    CSalAdv.Border = 15;
                    SecondtableHeadings.AddCell(CSalAdv);
                    //22
                    PdfPCell CUnifDed = new PdfPCell(new Phrase("Unif. Ded", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    CUnifDed.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    CUnifDed.Border = 15;
                    SecondtableHeadings.AddCell(CUnifDed);

                    //23
                    PdfPCell Ccanteended = new PdfPCell(new Phrase("Mess. Ded", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    Ccanteended.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    Ccanteended.Border = 15;
                    SecondtableHeadings.AddCell(Ccanteended);

                    //24
                    PdfPCell COWF = new PdfPCell(new Phrase("OWF", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                    COWF.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    COWF.Border = 15;
                    SecondtableHeadings.AddCell(COWF);
                    //25
                    PdfPCell COtherDed = new PdfPCell(new Phrase("Other Ded", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    COtherDed.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    COtherDed.Border = 15;
                    SecondtableHeadings.AddCell(COtherDed);
                    //26
                    PdfPCell CTotDed = new PdfPCell(new Phrase("Tot Ded", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    CTotDed.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    CTotDed.Border = 15;
                    SecondtableHeadings.AddCell(CTotDed);
                    //27
                    PdfPCell CNetPay = new PdfPCell(new Phrase("Net Pay", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    CNetPay.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    CNetPay.Border = 15;
                    SecondtableHeadings.AddCell(CNetPay);
                    //28
                    PdfPCell CSignature = new PdfPCell(new Phrase("Bank A/c No./ Signature", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    CSignature.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    CSignature.Border = 15;
                    SecondtableHeadings.AddCell(CSignature);
                    #endregion


                    PdfPTable Secondtable = new PdfPTable(tableCells);
                    Secondtable.TotalWidth = 950f;
                    Secondtable.LockedWidth = true;
                    float[] SecondWidth = new float[] { 2f, 2f, 6f, 3f, 2f, 1.5f, 1.8f, 1.8f, 1.5f, 1.5f, 1.5f, 1.5f, 1.8f, 1.5f, 1.5f, 1.5f, 2f, 2f, 1.5f, 1.5f, 2f, 2f, 1.5f, 1.5f, 1.5f, 2f, 2f, 6f };
                    Secondtable.SetWidths(SecondWidth);

                    int rowCount = 0;
                    int pageCount = 0;
                    int i = nextpagei;
                    #region Table Data


                    // for (int i = 0, j = 0; i < dt.Rows.Count; i++)
                    {
                        forConvert = 0;
                        if (dt.Rows[i]["NoOfDuties"].ToString().Trim().Length > 0)
                            forConvert = Convert.ToSingle(dt.Rows[i]["NoOfDuties"].ToString());
                        if (forConvert > 0)
                        {

                            if (nextpagerecordscount == 0)
                            {
                                document.Add(SecondtableHeadings);
                            }

                            nextpagerecordscount++;
                            //1
                            PdfPCell CSNo = new PdfPCell(new Phrase((++j).ToString() + "\n \n \n \n", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                            CSNo.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            CSNo.Border = 15;
                            Secondtable.AddCell(CSNo);
                            //2
                            PdfPCell CEmpId1 = new PdfPCell(new Phrase(dt.Rows[i]["EmpId"].ToString(), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                            CEmpId1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            CEmpId1.Border = 15;
                            Secondtable.AddCell(CEmpId1);
                            //3
                            PdfPCell CEmpName1 = new PdfPCell(new Phrase(dt.Rows[i]["EmpFName"].ToString() + " " + dt.Rows[i]["EmpMName"].ToString(), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                            CEmpName1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            CEmpName1.Border = 15;
                            Secondtable.AddCell(CEmpName1);
                            //4
                            PdfPCell CEmpDesgn = new PdfPCell(new Phrase(dt.Rows[i]["Desgn"].ToString(), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                            CEmpDesgn.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            CEmpDesgn.Border = 15;
                            Secondtable.AddCell(CEmpDesgn);
                            //5
                            forConvert = 0;
                            if (dt.Rows[i]["NoOfDuties"].ToString().Trim().Length > 0)
                                forConvert = Convert.ToSingle(dt.Rows[i]["NoOfDuties"].ToString());
                            totalDuties += forConvert;
                            GrandtotalDuties += forConvert;

                            PdfPCell CNoOfDuties = new PdfPCell(new Phrase(forConvert.ToString("0.00"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                            CNoOfDuties.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            CNoOfDuties.Border = 15;
                            Secondtable.AddCell(CNoOfDuties);
                            //6
                            if (dt.Rows[i]["ot"].ToString().Trim().Length > 0)
                                forConvert = Convert.ToSingle(dt.Rows[i]["ot"].ToString());
                            totalOts += forConvert;
                            GrandtotalOts += forConvert;
                            PdfPCell CNoOfots = new PdfPCell(new Phrase(forConvert.ToString("0.00"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                            CNoOfots.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            CNoOfots.Border = 15;
                            Secondtable.AddCell(CNoOfots);

                            //7
                            forConvert = 0;
                            if (dt.Rows[i]["Basic"].ToString().Trim().Length > 0)
                                forConvert = (float)Math.Round(Convert.ToSingle(dt.Rows[i]["Basic"].ToString()));
                            totalBasic += forConvert;
                            GrandtotalBasic += forConvert;
                            PdfPCell CBasic1 = new PdfPCell(new Phrase(forConvert.ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                            CBasic1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            CBasic1.Border = 15;
                            Secondtable.AddCell(CBasic1);
                            //8

                            forConvert = 0;

                            if (dt.Rows[i]["DA"].ToString().Trim().Length > 0)
                                forConvert = (float)Math.Round(Convert.ToSingle(dt.Rows[i]["DA"].ToString()));
                            totalDA += forConvert;
                            GrandtotalDA += forConvert;

                            PdfPCell CDa1 = new PdfPCell(new Phrase(forConvert.ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                            CDa1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            CDa1.Border = 15;
                            Secondtable.AddCell(CDa1);

                            //9

                            forConvert = 0;
                            if (dt.Rows[i]["HRA"].ToString().Trim().Length > 0)
                                forConvert = (float)Math.Round(Convert.ToSingle(dt.Rows[i]["HRA"].ToString()));
                            totalHRA += forConvert;
                            GrandtotalHRA += forConvert;

                            PdfPCell CHRA1 = new PdfPCell(new Phrase(forConvert.ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                            CHRA1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            CHRA1.Border = 15;
                            Secondtable.AddCell(CHRA1);

                            //10
                            forConvert = 0;
                            if (dt.Rows[i]["Conveyance"].ToString().Trim().Length > 0)
                                forConvert = (float)Math.Round(Convert.ToSingle(dt.Rows[i]["Conveyance"].ToString()));
                            totalConveyance += forConvert;
                            GrandtotalConveyance += forConvert;



                            PdfPCell CConveyance = new PdfPCell(new Phrase(forConvert.ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                            CConveyance.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            CConveyance.Border = 15;
                            Secondtable.AddCell(CConveyance);

                            //11
                            forConvert = 0;
                            if (dt.Rows[i]["cca"].ToString().Trim().Length > 0)
                                forConvert = (float)Math.Round(Convert.ToSingle(dt.Rows[i]["cca"].ToString()));
                            totalCCA += forConvert;

                            GrandtotalCCA += forConvert;

                            PdfPCell Ccca = new PdfPCell(new Phrase(forConvert.ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                            Ccca.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            Ccca.Border = 15;
                            Secondtable.AddCell(Ccca);
                            //12
                            forConvert = 0;
                            if (dt.Rows[i]["washallowance"].ToString().Trim().Length > 0)
                                forConvert = (float)Math.Round(Convert.ToSingle(dt.Rows[i]["washallowance"].ToString()));
                            totalWA += forConvert;


                            GrandtotalWA += forConvert;

                            PdfPCell CWa = new PdfPCell(new Phrase(forConvert.ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                            CWa.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            CWa.Border = 15;
                            Secondtable.AddCell(CWa);

                            //13
                            forConvert = 0;
                            if (dt.Rows[i]["OtherAllowance"].ToString().Trim().Length > 0)
                                forConvert = (float)Math.Round(Convert.ToSingle(dt.Rows[i]["OtherAllowance"].ToString()));
                            totalOA += forConvert;
                            GrandtotalOA += forConvert;
                            PdfPCell COA = new PdfPCell(new Phrase(forConvert.ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                            COA.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            COA.Border = 15;
                            Secondtable.AddCell(COA);

                            //14
                            forConvert = 0;
                            if (dt.Rows[i]["bonus"].ToString().Trim().Length > 0)
                                forConvert = (float)Math.Round(Convert.ToSingle(dt.Rows[i]["bonus"].ToString()));
                            totalbonus += forConvert;

                            Grandtotalbonus += forConvert;
                            PdfPCell Cbonus = new PdfPCell(new Phrase(forConvert.ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                            Cbonus.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            Cbonus.Border = 15;
                            Secondtable.AddCell(Cbonus);


                            //15
                            forConvert = 0;
                            if (dt.Rows[i]["otamt"].ToString().Trim().Length > 0)
                                forConvert = (float)Math.Round(Convert.ToSingle(dt.Rows[i]["otamt"].ToString()));
                            totalOTAmount += forConvert;

                            Grandtotalbonus += forConvert;

                            PdfPCell Cottamt = new PdfPCell(new Phrase(forConvert.ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                            Cottamt.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            Cottamt.Border = 15;
                            Secondtable.AddCell(Cottamt);



                            //16
                            forConvert = 0;
                            if (dt.Rows[i]["Incentivs"].ToString().Trim().Length > 0)
                                forConvert = (float)Math.Round(Convert.ToSingle(dt.Rows[i]["Incentivs"].ToString()));
                            totalIncentivs += forConvert;
                            GrandtotalIncentivs += forConvert;

                            PdfPCell Cincentivs = new PdfPCell(new Phrase(forConvert.ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                            Cincentivs.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            Cincentivs.Border = 15;
                            Secondtable.AddCell(Cincentivs);

                            //17
                            forConvert = 0;
                            if (dt.Rows[i]["Gross"].ToString().Trim().Length > 0)
                                forConvert = (float)Math.Round(Convert.ToSingle(dt.Rows[i]["Gross"].ToString()));
                            totalGrass += forConvert;
                            GrandtotalGrass += forConvert;

                            PdfPCell CGross1 = new PdfPCell(new Phrase(forConvert.ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                            CGross1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            CGross1.Border = 15;
                            Secondtable.AddCell(CGross1);
                            //18
                            forConvert = 0;
                            if (dt.Rows[i]["PF"].ToString().Trim().Length > 0)
                                forConvert = (float)Math.Round(Convert.ToSingle(dt.Rows[i]["PF"].ToString()));
                            totalPF += forConvert;
                            GrandtotalPF += forConvert;

                            PdfPCell CPF1 = new PdfPCell(new Phrase(forConvert.ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                            CPF1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            CPF1.Border = 15;
                            Secondtable.AddCell(CPF1);
                            //19
                            forConvert = 0;
                            if (dt.Rows[i]["ESI"].ToString().Trim().Length > 0)
                                forConvert = (float)Math.Round(Convert.ToSingle(dt.Rows[i]["ESI"].ToString()));
                            totalESI += forConvert;

                            GrandtotalESI += forConvert;

                            PdfPCell CESI1 = new PdfPCell(new Phrase(forConvert.ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                            CESI1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            CESI1.Border = 15;
                            Secondtable.AddCell(CESI1);
                            //20
                            forConvert = 0;
                            if (dt.Rows[i]["ProfTax"].ToString().Trim().Length > 0)
                                forConvert = (float)Math.Round(Convert.ToSingle(dt.Rows[i]["ProfTax"].ToString()));
                            totalProfTax += forConvert;
                            GrandtotalProfTax += forConvert;

                            PdfPCell CProTax1 = new PdfPCell(new Phrase(forConvert.ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                            CProTax1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            CProTax1.Border = 15;
                            Secondtable.AddCell(CProTax1);
                            //21
                            forConvert = 0;
                            if (dt.Rows[i]["SalAdvDed"].ToString().Trim().Length > 0)
                                forConvert = (float)Math.Round(Convert.ToSingle(dt.Rows[i]["SalAdvDed"].ToString()));
                            totalSalAdv += forConvert;
                            GrandtotalSalAdv += forConvert;

                            PdfPCell CSalAdv1 = new PdfPCell(new Phrase(forConvert.ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                            CSalAdv1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            CSalAdv1.Border = 15;
                            Secondtable.AddCell(CSalAdv1);
                            //22
                            forConvert = 0;
                            if (dt.Rows[i]["UniformDed"].ToString().Trim().Length > 0)
                                forConvert = (float)Math.Round(Convert.ToSingle(dt.Rows[i]["UniformDed"].ToString()));
                            totalUniforDed += forConvert;
                            GrandtotalUniforDed += forConvert;

                            PdfPCell CUnifDed1 = new PdfPCell(new Phrase(forConvert.ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                            CUnifDed1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            CUnifDed1.Border = 15;
                            Secondtable.AddCell(CUnifDed1);
                            //23

                            forConvert = 0;
                            if (dt.Rows[i]["CanteenAdv"].ToString().Trim().Length > 0)
                                forConvert = (float)Math.Round(Convert.ToSingle(dt.Rows[i]["CanteenAdv"].ToString()));
                            totalCanteenAdv += forConvert;
                            GrandtotalCanteenAdv += forConvert;

                            PdfPCell CCanteended = new PdfPCell(new Phrase(forConvert.ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                            CCanteended.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            CCanteended.Border = 15;
                            Secondtable.AddCell(CCanteended);

                            //24
                            if (dt.Rows[i]["OWF"].ToString().Trim().Length > 0)
                                forConvert = (float)Math.Round(Convert.ToSingle(dt.Rows[i]["OWF"].ToString()));
                            totalOwf += forConvert;
                            GrandtotalOwf += forConvert;

                            PdfPCell COwf1 = new PdfPCell(new Phrase(forConvert.ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                            COwf1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            COwf1.Border = 15;
                            Secondtable.AddCell(COwf1);
                            //25
                            forConvert = 0;


                            if (dt.Rows[i]["Penalty"].ToString().Trim().Length > 0)
                                forConvert = (float)Math.Round(Convert.ToSingle(dt.Rows[i]["Penalty"].ToString()));
                            totalPenalty += forConvert;
                            GrandtotalPenalty += forConvert;

                            PdfPCell COtherDed1 = new PdfPCell(new Phrase(forConvert.ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                            COtherDed1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            COtherDed1.Border = 15;
                            Secondtable.AddCell(COtherDed1);
                            //26
                            forConvert = 0;
                            if (dt.Rows[i]["Deductions"].ToString().Trim().Length > 0)
                                forConvert = (float)Math.Round(Convert.ToSingle(dt.Rows[i]["Deductions"].ToString()));
                            totalDed += forConvert;
                            GrandtotalDed += forConvert;

                            PdfPCell CTotDed1 = new PdfPCell(new Phrase(forConvert.ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                            CTotDed1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            CTotDed1.Border = 15;
                            Secondtable.AddCell(CTotDed1);//OtherDed,Eps.Gross,Eps.Deductions,Eps.ActualAmount
                            //27
                            forConvert = 0;
                            if (dt.Rows[i]["ActualAmount"].ToString().Trim().Length > 0)
                                forConvert = (float)Math.Round(Convert.ToSingle(dt.Rows[i]["ActualAmount"].ToString()));
                            totalActualamount += forConvert;
                            GrandtotalActualamount += forConvert;
                            PdfPCell CNetPay1 = new PdfPCell(new Phrase(forConvert.ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                            CNetPay1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            CNetPay1.Border = 15;
                            Secondtable.AddCell(CNetPay1);
                            //28
                            string EmpBankAcNo = dt.Rows[i]["EmpBankAcNo"].ToString();
                            PdfPCell CSignature1 = new PdfPCell(new Phrase(EmpBankAcNo, FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                            CSignature1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            CSignature1.Border = 15;
                            CSignature1.MinimumHeight = 25;
                            Secondtable.AddCell(CSignature1);
                        }


                    }
                    #endregion


                    SecondtableFooter = new PdfPTable(tableCells);
                    SecondtableFooter.TotalWidth = 950f;
                    SecondtableFooter.LockedWidth = true;
                    float[] SecondFooterWidth = new float[] { 2f, 2f, 6f, 3f, 2f, 1.5f, 1.8f, 1.8f, 1.5f, 1.5f, 1.5f, 1.5f, 1.8f, 1.5f, 1.5f, 1.5f, 2f, 2f, 1.5f, 1.5f, 2f, 2f, 1.5f, 1.5f, 1.5f, 2f, 2f, 6f };
                    SecondtableFooter.SetWidths(SecondFooterWidth);
                    #region Table Footer
                    //1
                    PdfPCell FCSNo = new PdfPCell(new Phrase("", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                    FCSNo.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCSNo.Border = 15;
                    SecondtableFooter.AddCell(FCSNo);
                    //2
                    PdfPCell FCEmpId1 = new PdfPCell(new Phrase("", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                    FCEmpId1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCEmpId1.Border = 15;
                    SecondtableFooter.AddCell(FCEmpId1);
                    //3
                    PdfPCell FCEmpName1 = new PdfPCell(new Phrase(" Total : ", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    FCEmpName1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCEmpName1.Border = 15;
                    SecondtableFooter.AddCell(FCEmpName1);
                    //4
                    PdfPCell FCEmpDesgn = new PdfPCell(new Phrase("", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                    FCEmpDesgn.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCEmpDesgn.Border = 15;
                    //FCEmpDesgn.Colspan = 4;
                    SecondtableFooter.AddCell(FCEmpDesgn);
                    //5
                    PdfPCell FCNoOfDuties = new PdfPCell(new Phrase(totalDuties.ToString("0.00"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    FCNoOfDuties.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCNoOfDuties.Border = 15;
                    SecondtableFooter.AddCell(FCNoOfDuties);
                    //6
                    PdfPCell FCNoOfots = new PdfPCell(new Phrase(totalOts.ToString("0.00"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    FCNoOfots.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCNoOfots.Border = 15;
                    SecondtableFooter.AddCell(FCNoOfots);

                    //7
                    PdfPCell FCBasic1 = new PdfPCell(new Phrase(Math.Round(totalBasic).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    FCBasic1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCBasic1.Border = 15;
                    SecondtableFooter.AddCell(FCBasic1);


                    //8
                    PdfPCell FDa = new PdfPCell(new Phrase(Math.Round(totalDA).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    FDa.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FDa.Border = 15;
                    SecondtableFooter.AddCell(FDa);


                    //9

                    forConvert = 0;
                    PdfPCell FCHRA1 = new PdfPCell(new Phrase(Math.Round(totalHRA).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    FCHRA1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCHRA1.Border = 15;
                    SecondtableFooter.AddCell(FCHRA1);

                    //10
                    PdfPCell FCConveyance = new PdfPCell(new Phrase(Math.Round(totalConveyance).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    FCConveyance.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCConveyance.Border = 15;
                    SecondtableFooter.AddCell(FCConveyance);

                    //11
                    PdfPCell FCcca = new PdfPCell(new Phrase(Math.Round(totalCCA).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    FCcca.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCcca.Border = 15;
                    SecondtableFooter.AddCell(FCcca);
                    //12
                    PdfPCell FCWa = new PdfPCell(new Phrase(Math.Round(totalWA).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    FCWa.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCWa.Border = 15;
                    SecondtableFooter.AddCell(FCWa);

                    //13
                    PdfPCell FCOa = new PdfPCell(new Phrase(Math.Round(totalOA).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    FCOa.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCOa.Border = 15;
                    SecondtableFooter.AddCell(FCOa);

                    //14
                    PdfPCell Fbonus = new PdfPCell(new Phrase(Math.Round(totalbonus).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    Fbonus.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    Fbonus.Border = 15;
                    SecondtableFooter.AddCell(Fbonus);

                    //15
                    PdfPCell FCottamt = new PdfPCell(new Phrase(Math.Round(totalOTAmount).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    FCottamt.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCottamt.Border = 15;
                    SecondtableFooter.AddCell(FCottamt);
                    //16
                    PdfPCell FCIncentivs = new PdfPCell(new Phrase(Math.Round(totalIncentivs).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    FCIncentivs.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCIncentivs.Border = 15;
                    SecondtableFooter.AddCell(FCIncentivs);



                    //17
                    PdfPCell FCGross1 = new PdfPCell(new Phrase(Math.Round(totalGrass).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    FCGross1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCGross1.Border = 15;
                    SecondtableFooter.AddCell(FCGross1);
                    //18
                    PdfPCell FCPF1 = new PdfPCell(new Phrase(Math.Round(totalPF).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    FCPF1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCPF1.Border = 15;
                    SecondtableFooter.AddCell(FCPF1);
                    //19
                    PdfPCell FCESI1 = new PdfPCell(new Phrase(Math.Round(totalESI).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    FCESI1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCESI1.Border = 15;
                    SecondtableFooter.AddCell(FCESI1);
                    //20
                    PdfPCell FCProTax1 = new PdfPCell(new Phrase(Math.Round(totalProfTax).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    FCProTax1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCProTax1.Border = 15;
                    SecondtableFooter.AddCell(FCProTax1);
                    //21
                    PdfPCell FCSalAdv1 = new PdfPCell(new Phrase(Math.Round(totalSalAdv).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    FCSalAdv1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCSalAdv1.Border = 15;
                    SecondtableFooter.AddCell(FCSalAdv1);
                    //22
                    PdfPCell FCUnifDed1 = new PdfPCell(new Phrase(Math.Round(totalUniforDed).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    FCUnifDed1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCUnifDed1.Border = 15;
                    SecondtableFooter.AddCell(FCUnifDed1);
                    //23
                    PdfPCell FCCanteended = new PdfPCell(new Phrase(Math.Round(totalCanteenAdv).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    FCCanteended.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCCanteended.Border = 15;
                    SecondtableFooter.AddCell(FCCanteended);
                    //24
                    PdfPCell FCOwf1 = new PdfPCell(new Phrase(Math.Round(totalOwf).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    FCOwf1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCOwf1.Border = 15;
                    SecondtableFooter.AddCell(FCOwf1);
                    //25
                    PdfPCell FCOtherDed1 = new PdfPCell(new Phrase(Math.Round(totalPenalty).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    FCOtherDed1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCOtherDed1.Border = 15;
                    SecondtableFooter.AddCell(FCOtherDed1);
                    //26
                    PdfPCell FCTotDed1 = new PdfPCell(new Phrase(Math.Round(totalDed).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    FCTotDed1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCTotDed1.Border = 15;
                    SecondtableFooter.AddCell(FCTotDed1);//OtherDed,Eps.Gross,Eps.Deductions,Eps.ActualAmount
                    //27
                    PdfPCell FCNetPay1 = new PdfPCell(new Phrase(Math.Round(totalActualamount).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    FCNetPay1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCNetPay1.Border = 15;
                    //FCNetPay1.Colspan = 2;
                    SecondtableFooter.AddCell(FCNetPay1);
                    //28
                    PdfPCell FCSignature1 = new PdfPCell(new Phrase(" ", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                    FCSignature1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCSignature1.Border = 15;
                    //FCSignature1.MinimumHeight = 25;
                    SecondtableFooter.AddCell(FCSignature1);
                    #endregion


                    SecondtableGrandtotalFooter = new PdfPTable(tableCells);
                    SecondtableGrandtotalFooter.TotalWidth = 950f;
                    SecondtableGrandtotalFooter.LockedWidth = true;
                    float[] SecondGrandtotalFooterWidth = new float[] { 2f, 2f, 6f, 3f, 2f, 1.5f, 1.8f, 1.8f, 1.5f, 1.5f, 1.5f, 1.5f, 1.8f, 1.5f, 1.5f, 1.5f, 2f, 2f, 1.5f, 1.5f, 2f, 2f, 1.5f, 1.5f, 1.5f, 2f, 2f, 6f };
                    SecondtableGrandtotalFooter.SetWidths(SecondGrandtotalFooterWidth);



                    SecondtablecheckbyFooter = new PdfPTable(tableCells);
                    SecondtablecheckbyFooter.TotalWidth = 950f;
                    SecondtablecheckbyFooter.LockedWidth = true;
                    float[] SecondcheckbyFooterWidth = new float[] { 2f, 2f, 6f, 3f, 2f, 1.5f, 1.8f, 1.8f, 1.5f, 1.5f, 1.5f, 1.5f, 1.8f, 1.5f, 1.5f, 1.5f, 2f, 2f, 1.5f, 1.5f, 2f, 2f, 1.5f, 1.5f, 1.5f, 2f, 2f, 6f };
                    SecondtablecheckbyFooter.SetWidths(SecondcheckbyFooterWidth);


                    #region   Footer Headings

                    #region Table   Grand   Total  Footer
                    //1
                    PdfPCell GFCSNo = new PdfPCell(new Phrase("", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                    GFCSNo.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCSNo.Border = 15;
                    SecondtablecheckbyFooter.AddCell(GFCSNo);
                    //2
                    PdfPCell GFCEmpId1 = new PdfPCell(new Phrase("", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                    GFCEmpId1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCEmpId1.Border = 15;
                    SecondtablecheckbyFooter.AddCell(GFCEmpId1);
                    //3
                    PdfPCell GFCEmpName1 = new PdfPCell(new Phrase("Grand  Totals: ", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    GFCEmpName1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCEmpName1.Border = 15;
                    SecondtablecheckbyFooter.AddCell(GFCEmpName1);
                    //4
                    PdfPCell GFCEmpDesgn = new PdfPCell(new Phrase("", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                    GFCEmpDesgn.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCEmpDesgn.Border = 15;
                    //FCEmpDesgn.Colspan = 4;
                    SecondtablecheckbyFooter.AddCell(GFCEmpDesgn);
                    //5
                    PdfPCell GFCNoOfDuties = new PdfPCell(new Phrase(GrandtotalDuties.ToString("0.00"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    GFCNoOfDuties.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCNoOfDuties.Border = 15;
                    SecondtablecheckbyFooter.AddCell(GFCNoOfDuties);
                    //6
                    PdfPCell GFCNoOfots = new PdfPCell(new Phrase(GrandtotalOts.ToString("0.00"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    GFCNoOfots.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCNoOfots.Border = 15;
                    SecondtablecheckbyFooter.AddCell(GFCNoOfots);

                    //7
                    PdfPCell GFCBasic1 = new PdfPCell(new Phrase(Math.Round(GrandtotalBasic).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    GFCBasic1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCBasic1.Border = 15;
                    SecondtablecheckbyFooter.AddCell(GFCBasic1);


                    //8
                    PdfPCell GFDa = new PdfPCell(new Phrase(Math.Round(GrandtotalDA).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    GFDa.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFDa.Border = 15;
                    SecondtablecheckbyFooter.AddCell(GFDa);


                    //9

                    forConvert = 0;
                    PdfPCell GFCHRA1 = new PdfPCell(new Phrase(Math.Round(GrandtotalHRA).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    GFCHRA1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCHRA1.Border = 15;
                    SecondtablecheckbyFooter.AddCell(GFCHRA1);

                    //10
                    PdfPCell GFCConveyance = new PdfPCell(new Phrase(Math.Round(GrandtotalConveyance).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    GFCConveyance.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCConveyance.Border = 15;
                    SecondtablecheckbyFooter.AddCell(GFCConveyance);

                    //11
                    PdfPCell GFCcca = new PdfPCell(new Phrase(Math.Round(GrandtotalCCA).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    GFCcca.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCcca.Border = 15;
                    SecondtablecheckbyFooter.AddCell(GFCcca);
                    //12
                    PdfPCell GFCWa = new PdfPCell(new Phrase(Math.Round(GrandtotalWA).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    GFCWa.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCWa.Border = 15;
                    SecondtablecheckbyFooter.AddCell(GFCWa);

                    //13
                    PdfPCell GFCOa = new PdfPCell(new Phrase(Math.Round(GrandtotalOA).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    GFCOa.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCOa.Border = 15;
                    SecondtablecheckbyFooter.AddCell(GFCOa);

                    //14
                    PdfPCell GFbonus = new PdfPCell(new Phrase(Math.Round(Grandtotalbonus).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    GFbonus.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFbonus.Border = 15;
                    SecondtablecheckbyFooter.AddCell(GFbonus);

                    //15
                    PdfPCell GFCottamt = new PdfPCell(new Phrase(Math.Round(GrandtotalOTAmount).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    GFCottamt.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCottamt.Border = 15;
                    SecondtablecheckbyFooter.AddCell(GFCottamt);


                    //16
                    PdfPCell GFCIncentivs = new PdfPCell(new Phrase(Math.Round(GrandtotalIncentivs).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    GFCIncentivs.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCIncentivs.Border = 15;
                    SecondtablecheckbyFooter.AddCell(GFCIncentivs);

                    //17
                    PdfPCell GFCGross1 = new PdfPCell(new Phrase(Math.Round(GrandtotalGrass).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    GFCGross1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCGross1.Border = 15;
                    SecondtablecheckbyFooter.AddCell(GFCGross1);
                    //18
                    PdfPCell GFCPF1 = new PdfPCell(new Phrase(Math.Round(GrandtotalPF).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    GFCPF1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCPF1.Border = 15;
                    SecondtablecheckbyFooter.AddCell(GFCPF1);
                    //19
                    PdfPCell GFCESI1 = new PdfPCell(new Phrase(Math.Round(GrandtotalESI).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    GFCESI1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCESI1.Border = 15;
                    SecondtablecheckbyFooter.AddCell(GFCESI1);
                    //20
                    PdfPCell GFCProTax1 = new PdfPCell(new Phrase(Math.Round(GrandtotalProfTax).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    GFCProTax1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCProTax1.Border = 15;
                    SecondtablecheckbyFooter.AddCell(GFCProTax1);
                    //21
                    PdfPCell GFCSalAdv1 = new PdfPCell(new Phrase(Math.Round(GrandtotalSalAdv).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    GFCSalAdv1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCSalAdv1.Border = 15;
                    SecondtablecheckbyFooter.AddCell(GFCSalAdv1);
                    //22
                    PdfPCell GFCUnifDed1 = new PdfPCell(new Phrase(Math.Round(GrandtotalUniforDed).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    GFCUnifDed1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCUnifDed1.Border = 15;
                    SecondtablecheckbyFooter.AddCell(GFCUnifDed1);
                    //23
                    PdfPCell GFCCanteended = new PdfPCell(new Phrase(Math.Round(GrandtotalCanteenAdv).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    GFCCanteended.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCCanteended.Border = 15;
                    SecondtablecheckbyFooter.AddCell(GFCCanteended);
                    //24
                    PdfPCell GFCOwf1 = new PdfPCell(new Phrase(Math.Round(GrandtotalOwf).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    GFCOwf1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCOwf1.Border = 15;
                    SecondtablecheckbyFooter.AddCell(GFCOwf1);
                    //25
                    PdfPCell GFCOtherDed1 = new PdfPCell(new Phrase(Math.Round(GrandtotalPenalty).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    GFCOtherDed1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCOtherDed1.Border = 15;
                    SecondtablecheckbyFooter.AddCell(GFCOtherDed1);
                    //26
                    PdfPCell GFCTotDed1 = new PdfPCell(new Phrase(Math.Round(GrandtotalDed).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    GFCTotDed1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCTotDed1.Border = 15;
                    SecondtablecheckbyFooter.AddCell(GFCTotDed1);//OtherDed,Eps.Gross,Eps.Deductions,Eps.ActualAmount
                    //27
                    PdfPCell GFCNetPay1 = new PdfPCell(new Phrase(Math.Round(GrandtotalActualamount).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    GFCNetPay1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCNetPay1.Border = 15;
                    //FCNetPay1.Colspan = 2;
                    SecondtablecheckbyFooter.AddCell(GFCNetPay1);
                    //28
                    PdfPCell GFCSignature1 = new PdfPCell(new Phrase(" ", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                    GFCSignature1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCSignature1.Border = 15;
                    //FCSignature1.MinimumHeight = 25;
                    SecondtablecheckbyFooter.AddCell(GFCSignature1);
                    #endregion


                    //1
                    PdfPCell FHCheckedbybr1 = new PdfPCell(new Phrase("", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                    FHCheckedbybr1.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    FHCheckedbybr1.Border = 0;
                    FHCheckedbybr1.Rowspan = 0;
                    FHCheckedbybr1.Colspan = 14;
                    SecondtablecheckbyFooter.AddCell(FHCheckedbybr1);
                    //2
                    PdfPCell FHApprovedbr2 = new PdfPCell(new Phrase("  ", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                    FHApprovedbr2.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    FHApprovedbr2.Border = 0;
                    FHApprovedbr2.Colspan = 14;

                    SecondtablecheckbyFooter.AddCell(FHApprovedbr2);
                    SecondtablecheckbyFooter.AddCell(FHCheckedbybr1);
                    SecondtablecheckbyFooter.AddCell(FHApprovedbr2);


                    //1
                    PdfPCell FHCheckedby = new PdfPCell(new Phrase("Checked By", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                    FHCheckedby.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    FHCheckedby.Border = 0;
                    FHCheckedby.Rowspan = 0;
                    FHCheckedby.Colspan = 14;
                    SecondtablecheckbyFooter.AddCell(FHCheckedby);
                    //2
                    PdfPCell FHApprovedBy = new PdfPCell(new Phrase("Gross  Approved By", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                    FHApprovedBy.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    FHApprovedBy.Border = 0;
                    FHApprovedBy.Colspan = 14;
                    SecondtablecheckbyFooter.AddCell(FHApprovedBy);



                    #endregion

                    document.Add(Secondtable);


                    if (nextpagerecordscount == targetpagerecors)
                    {
                        targetpagerecors = secondpagerecords;
                        document.Add(SecondtableFooter);
                        document.NewPage();
                        nextpagerecordscount = 0;
                        #region  Zero variables

                        totalActualamount = 0;
                        totalDuties = 0;
                        totalOts = 0;
                        totalBasic = 0;
                        totalDA = 0;
                        totalHRA = 0;
                        totalCCA = 0;
                        totalConveyance = 0;
                        totalWA = 0;
                        totalOA = 0;
                        totalGrass = 0;
                        totalOTAmount = 0;
                        totalPF = 0;
                        totalESI = 0;
                        totalProfTax = 0;
                        totalDed = 0;
                        totalSalAdv = 0;
                        totalUniforDed = 0;
                        totalCanteenAdv = 0;
                        totalOwf = 0;
                        totalPenalty = 0;
                        totalbonus = 0;

                        #endregion
                    }
                }

                if (nextpagerecordscount >= 0)
                {
                    document.Add(SecondtableFooter);
                    document.Add(SecondtableGrandtotalFooter);
                    document.Add(SecondtablecheckbyFooter);
                }
                document.Close();
                if (nextpagehasPages)
                {

                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=Wage Sheet(2).pdf");
                    Response.Buffer = true;
                    Response.Clear();
                    Response.OutputStream.Write(ms.GetBuffer(), 0, ms.GetBuffer().Length);
                    Response.OutputStream.Flush();
                    Response.End();
                }
            }
        }


        protected void btnEmployeeWageSheet_Click(object sender, EventArgs e)
        {
            int titleofdocumentindex = 0;
            if (ddlclient.SelectedIndex <= 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showlalert", "alert('Please Select Client ID/Name');", true);
                return;
            }

            string month = Getmonth();
            if (month.Trim().Length == 0)
            {
                return;
            }

            string selectmonth = string.Empty;

            if (ddlnoofattendance.SelectedIndex == 0)
            {
                selectmonth = "select Eps.EmpId,Eps.Desgn,TempGross as salaryrate, Eps.NoOfDuties,Eps.Basic,Eps.DA,Eps.HRA,Eps.CCA,Eps.Conveyance,Eps.Bonus, " +
                    "   Eps.WashAllowance,Eps.OtherAllowance,Eps.PF,Eps.ESI,Eps.OTAmt,Eps.ProfTax,Eps.Deductions," +
                    "Eps.SalAdvDed,Eps.CanteenAdv,Eps.UniformDed,Eps.Penalty,Eps.OtherDed,Eps.Gross,Eps.Deductions,Eps.ActualAmount,Eps.OWF,  " +
                    " EmpDetails.EmpFName,EmpDetails.EmpMName," +
                    "EmpDetails.UnitId, EmpDetails.EmpBankAcNo, EmpDetails.EmpBankCardRef,EmpAttendance.OT from EmpPaySheet as Eps INNER JOIN    " +
                    " EmpDetails ON Eps.EmpId = EmpDetails.EmpId INNER JOIN EmpAttendance ON EmpAttendance.EmpId=Eps.EmpId AND  " +
                    " EmpAttendance.ClientId=Eps.ClientId  And  Eps.NoOfDuties>10  AND EmpAttendance.Month=Eps.Month AND Eps.ClientId='" +
                    ddlclient.SelectedValue + "' AND Eps.Month=" + month + "  and EmpAttendance.Design=Eps.Desgn  Order by Right(Eps.EmpId,6)";

            }
            if (ddlnoofattendance.SelectedIndex == 1)
            {
                selectmonth = "select Eps.EmpId,Eps.Desgn,TempGross as salaryrate, Eps.NoOfDuties,Eps.Basic,Eps.DA,Eps.HRA,Eps.CCA,Eps.Conveyance,Eps.Bonus, " +
                 "   Eps.WashAllowance,Eps.OtherAllowance,Eps.PF,Eps.ESI,Eps.OTAmt,Eps.ProfTax,Eps.Deductions," +
                 "Eps.SalAdvDed,Eps.CanteenAdv,Eps.UniformDed,Eps.Penalty,Eps.OtherDed,Eps.Gross,Eps.Deductions,Eps.ActualAmount,Eps.OWF,  " +
                 " EmpDetails.EmpFName,EmpDetails.EmpMName," +
                 "EmpDetails.UnitId, EmpDetails.EmpBankAcNo, EmpDetails.EmpBankCardRef,EmpAttendance.OT from EmpPaySheet as Eps INNER JOIN    " +
                 " EmpDetails ON Eps.EmpId = EmpDetails.EmpId INNER JOIN EmpAttendance ON EmpAttendance.EmpId=Eps.EmpId AND  " +
                 " EmpAttendance.ClientId=Eps.ClientId  And  Eps.NoOfDuties<=10  AND EmpAttendance.Month=Eps.Month AND Eps.ClientId='" +
                 ddlclient.SelectedValue + "' AND Eps.Month=" + month + "  and EmpAttendance.Design=Eps.Desgn  Order by Right(Eps.EmpId,6)";
            }

            if (ddlnoofattendance.SelectedIndex == 2)
            {
                selectmonth = "select Eps.EmpId,Eps.Desgn,TempGross as salaryrate, Eps.NoOfDuties,Eps.Basic,Eps.DA,Eps.HRA,Eps.CCA,Eps.Conveyance,Eps.Bonus, " +
                 "   Eps.WashAllowance,Eps.OtherAllowance,Eps.PF,Eps.ESI,Eps.OTAmt,Eps.ProfTax,Eps.Deductions," +
                 "Eps.SalAdvDed,Eps.CanteenAdv,Eps.UniformDed,Eps.Penalty,Eps.OtherDed,Eps.Gross,Eps.Deductions,Eps.ActualAmount,Eps.OWF,  " +
                 " EmpDetails.EmpFName,EmpDetails.EmpMName," +
                 "EmpDetails.UnitId, EmpDetails.EmpBankAcNo, EmpDetails.EmpBankCardRef,EmpAttendance.OT from EmpPaySheet as Eps INNER JOIN    " +
                 " EmpDetails ON Eps.EmpId = EmpDetails.EmpId INNER JOIN EmpAttendance ON EmpAttendance.EmpId=Eps.EmpId AND  " +
                 " EmpAttendance.ClientId=Eps.ClientId   AND EmpAttendance.Month=Eps.Month AND Eps.ClientId='" +
                 ddlclient.SelectedValue + "' AND Eps.Month=" + month + " and EmpAttendance.Design=Eps.Desgn  Order by Right(Eps.EmpId,6)";
            }


            DataTable dt = config.ExecuteAdaptorAsyncWithQueryParams(selectmonth).Result;

            MemoryStream ms = new MemoryStream();
            if (dt.Rows.Count > 0)
            {
                Document document = new Document(PageSize.LEGAL.Rotate());
                PdfWriter writer = PdfWriter.GetInstance(document, ms);
                document.Open();
                document.AddTitle("FaMS");
                document.AddAuthor("WebWonders");
                document.AddSubject("Wage Sheet for only Duties");
                document.AddKeywords("Keyword1, keyword2, …");//
                float forConvert;
                string strQry = "Select * from CompanyInfo";
                DataTable compInfo = config.ExecuteAdaptorAsyncWithQueryParams(strQry).Result;
                string companyName1 = "Your Company Name";
                string companyAddress = "Your Company Address";
                if (compInfo.Rows.Count > 0)
                {
                    companyName1 = compInfo.Rows[0]["CompanyName"].ToString();
                    companyAddress = compInfo.Rows[0]["Address"].ToString();
                }

                int tableCells = 21;

                #region variables for total




                float totalActualamount = 0;
                float totalDuties = 0;
                float totalOts = 0;
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
                float totalDed = 0;
                float totalSalAdv = 0;
                float totalUniforDed = 0;
                float totalCanteenAdv = 0;
                float totalOwf = 0;
                float totalPenalty = 0;
                float totalGross = 0;
                float totalbonus = 0;










                #endregion




                #region variables for  Grand total
                float GrandtotalActualamount = 0;
                float GrandtotalDuties = 0;
                // float GrandtotalOts = 0;
                float GrandtotalBasic = 0;
                float GrandtotalDA = 0;
                float GrandtotalHRA = 0;
                //float GrandtotalCCA = 0;
                // float GrandtotalConveyance = 0;
                float GrandtotalWA = 0;
                float GrandtotalOA = 0;
                float GrandtotalGrass = 0;
                // float GrandtotalOTAmount = 0;
                float GrandtotalPF = 0;
                float GrandtotalESI = 0;
                float GrandtotalProfTax = 0;
                float GrandtotalDed = 0;
                float GrandtotalSalAdv = 0;
                float GrandtotalUniforDed = 0;
                float GrandtotalCanteenAdv = 0;
                float GrandtotalOwf = 0;
                float GrandtotalPenalty = 0;
                float GrandtotalGross = 0;
                float Grandtotalbonus = 0;

                #endregion



                int nextpagerecordscount = 0;
                int targetpagerecors = 10;
                int secondpagerecords = targetpagerecors + 3;
                bool nextpagehasPages = false;
                int j = 0;
                titleofdocumentindex = 0;
                PdfPTable SecondtablecheckedbyFooter = null;
                PdfPTable SecondtableGrandTotalFooter = null;
                PdfPTable SecondtableFooter = null;
                for (int nextpagei = 0; nextpagei < dt.Rows.Count; nextpagei++)
                {
                    nextpagehasPages = true;

                    #region Titles of Document
                    tableCells = 21;
                    PdfPTable Maintable = new PdfPTable(tableCells);
                    Maintable.TotalWidth = 950f;
                    Maintable.LockedWidth = true;
                    float[] width = new float[] { 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f };
                    Maintable.SetWidths(width);
                    uint FONT_SIZE = 8;

                    //Company Name & vage act details
                    PdfPCell cellemp = new PdfPCell(new Phrase("  ", FontFactory.GetFont(Fontstyle, 10, Font.BOLD, BaseColor.BLACK)));
                    cellemp.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    cellemp.Colspan = tableCells;
                    cellemp.Border = 0;

                    PdfPCell Heading = new PdfPCell(new Phrase("Form - XVII REGISTER OF WAGES", FontFactory.GetFont(Fontstyle, 20, Font.BOLD, BaseColor.BLACK)));
                    Heading.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    Heading.Colspan = tableCells;
                    Heading.Border = 0;
                    Maintable.AddCell(Heading);

                    PdfPCell actDetails = new PdfPCell(new Phrase("(vide Rule 78(1) a(i) of Contract Labour (Reg. & abolition) Central & A.P. Rules)", FontFactory.GetFont(Fontstyle, 15, Font.BOLD, BaseColor.BLACK)));
                    actDetails.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    actDetails.Colspan = tableCells;
                    actDetails.Border = 0;// 15;
                    Maintable.AddCell(actDetails);

                    Maintable.AddCell(cellemp);
                    #endregion

                    #region Table Headings
                    //1
                    PdfPCell companyName = new PdfPCell(new Phrase(companyName1, FontFactory.GetFont(CFontstyle, 20, Font.BOLD, BaseColor.BLACK)));
                    companyName.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    companyName.Colspan = tableCells;
                    companyName.Border = 0;// 15;
                    Maintable.AddCell(companyName);
                    //2
                    PdfPCell paySheet = new PdfPCell(new Phrase("Pay Sheet", FontFactory.GetFont(Fontstyle, 10, Font.BOLD | Font.UNDERLINE, BaseColor.BLACK)));
                    paySheet.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    paySheet.Colspan = tableCells;
                    paySheet.Border = 0;// 15;
                    Maintable.AddCell(paySheet);
                    //3
                    PdfPCell CClient = new PdfPCell(new Phrase("Client ID :   " + ddlclient.SelectedValue, FontFactory.GetFont(Fontstyle, 10, Font.BOLD, BaseColor.BLACK)));
                    CClient.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    CClient.Colspan = 3;
                    CClient.Border = 0;// 15;
                    Maintable.AddCell(CClient);
                    //4
                    PdfPCell CClientName = new PdfPCell(new Phrase("Client Name :   " + ddlcname.SelectedItem, FontFactory.GetFont(Fontstyle, 10, Font.BOLD, BaseColor.BLACK)));
                    CClientName.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    CClientName.Colspan = 5;
                    CClientName.Border = 0;// 15;
                    Maintable.AddCell(CClientName);
                    //5
                    PdfPCell CDates = new PdfPCell(new Phrase("Payment for the period of : " + GetPaymentPeriod(ddlclient.SelectedValue), FontFactory.GetFont(Fontstyle, 10, Font.BOLD, BaseColor.BLACK)));
                    CDates.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    CDates.Colspan = 6;
                    CDates.Border = 0;// 15;
                    Maintable.AddCell(CDates);
                    //6
                    // PdfPCell CPaySheetDate = new PdfPCell(new Phrase("Pay Sheet Date :  " + DateTime.Now.Date.ToString("dd/MM/yyyy"), FontFactory.GetFont(Fontstyle, 10, Font.BOLD, BaseColor.BLACK)));
                    PdfPCell CPaySheetDate = new PdfPCell(new Phrase("", FontFactory.GetFont(Fontstyle, 10, Font.BOLD, BaseColor.BLACK)));
                    CPaySheetDate.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    CPaySheetDate.Colspan = 4;
                    CPaySheetDate.Border = 0;// 15;
                    Maintable.AddCell(CPaySheetDate);
                    //7
                    PdfPCell CPayMonth = new PdfPCell(new Phrase("For the month of :   " + DateTime.Parse(txtmonth.Text, CultureInfo.GetCultureInfo("en-gb")).ToString("MMMM"), FontFactory.GetFont(Fontstyle, 10, Font.BOLD, BaseColor.BLACK)));
                    CPayMonth.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    CPayMonth.Colspan = 3;
                    CPayMonth.Border = 0;// 15;
                    Maintable.AddCell(CPayMonth);

                    Maintable.AddCell(cellemp);
                    if (titleofdocumentindex == 0)
                    {
                        document.Add(Maintable);
                        titleofdocumentindex = 1;
                    }

                    tableCells = 23;
                    PdfPTable SecondtableHeadings = new PdfPTable(tableCells);
                    SecondtableHeadings.TotalWidth = 950f;
                    SecondtableHeadings.LockedWidth = true;
                    float[] SecondHeadingsWidth = new float[] { 1.5f, 2f, 4f, 3f, 1.8f, 2f, 2f, 1.8f, 1.5f, 1.5f, 1.6f, 2f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.8f, 2f, 7f };
                    SecondtableHeadings.SetWidths(SecondHeadingsWidth);

                    //Cell Headings
                    //1
                    PdfPCell sNo = new PdfPCell(new Phrase("S.No", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    sNo.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                    //sNo.Colspan = 1;
                    sNo.Border = 15;// 15;
                    SecondtableHeadings.AddCell(sNo);
                    //2
                    PdfPCell CEmpId = new PdfPCell(new Phrase("Emp Id", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    CEmpId.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    CEmpId.Border = 15;// 15;
                    SecondtableHeadings.AddCell(CEmpId);
                    //3
                    PdfPCell CEmpName = new PdfPCell(new Phrase("Emp Name", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    CEmpName.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    CEmpName.Border = 15;// 15;
                    SecondtableHeadings.AddCell(CEmpName);
                    //4
                    PdfPCell CDesgn = new PdfPCell(new Phrase("Desgn", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    CDesgn.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    CDesgn.Border = 15;
                    SecondtableHeadings.AddCell(CDesgn);
                    //5
                    PdfPCell CDuties = new PdfPCell(new Phrase("Dts", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    CDuties.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    CDuties.Border = 15;
                    SecondtableHeadings.AddCell(CDuties);
                    //6
                    PdfPCell CSalaryRate = new PdfPCell(new Phrase("S.Rate", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    CSalaryRate.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    CSalaryRate.Border = 15;
                    SecondtableHeadings.AddCell(CSalaryRate);
                    //7
                    PdfPCell CBasic = new PdfPCell(new Phrase("Basic", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    CBasic.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    CBasic.Border = 15;
                    SecondtableHeadings.AddCell(CBasic);
                    //8
                    PdfPCell CDa = new PdfPCell(new Phrase("DA", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    CDa.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    CDa.Border = 15;
                    SecondtableHeadings.AddCell(CDa);
                    //9
                    PdfPCell CHRa = new PdfPCell(new Phrase("HRA", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    CHRa.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    CHRa.Border = 15;
                    SecondtableHeadings.AddCell(CHRa);
                    //10
                    PdfPCell COa = new PdfPCell(new Phrase("OA", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    COa.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    COa.Border = 15;
                    SecondtableHeadings.AddCell(COa);

                    //11
                    PdfPCell CBonus = new PdfPCell(new Phrase("Bonus", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    CBonus.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    CBonus.Border = 15;
                    SecondtableHeadings.AddCell(CBonus);

                    //12
                    PdfPCell CGross = new PdfPCell(new Phrase("Gross", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    CGross.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    CGross.Border = 15;
                    SecondtableHeadings.AddCell(CGross);
                    //13
                    PdfPCell CPF = new PdfPCell(new Phrase("PF", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    CPF.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    CPF.Border = 15;
                    SecondtableHeadings.AddCell(CPF);
                    //14
                    PdfPCell CESI = new PdfPCell(new Phrase("ESI", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    CESI.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    CESI.Border = 15;
                    SecondtableHeadings.AddCell(CESI);
                    //15
                    PdfPCell CPT = new PdfPCell(new Phrase("PT", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    CPT.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    CPT.Border = 15;
                    SecondtableHeadings.AddCell(CPT);
                    //16
                    PdfPCell CSalAdv = new PdfPCell(new Phrase("Sal Adv", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    CSalAdv.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    CSalAdv.Border = 15;
                    SecondtableHeadings.AddCell(CSalAdv);
                    //17
                    PdfPCell CUnifDed = new PdfPCell(new Phrase("Unif. Ded", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    CUnifDed.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    CUnifDed.Border = 15;
                    SecondtableHeadings.AddCell(CUnifDed);
                    //18
                    PdfPCell COWF = new PdfPCell(new Phrase("OWF", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    COWF.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    COWF.Border = 15;
                    SecondtableHeadings.AddCell(COWF);

                    //19
                    PdfPCell COtherDed = new PdfPCell(new Phrase("Other Ded", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    COtherDed.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    COtherDed.Border = 15;
                    SecondtableHeadings.AddCell(COtherDed);
                    //20

                    PdfPCell Ccantadv = new PdfPCell(new Phrase("Cant Adv", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    Ccantadv.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    Ccantadv.Border = 15;
                    SecondtableHeadings.AddCell(Ccantadv);
                    //21
                    PdfPCell CTotDed = new PdfPCell(new Phrase("Tot Ded", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    CTotDed.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    CTotDed.Border = 15;
                    SecondtableHeadings.AddCell(CTotDed);
                    //22
                    PdfPCell CNetPay = new PdfPCell(new Phrase("Net Pay", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    CNetPay.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    CNetPay.Border = 15;
                    SecondtableHeadings.AddCell(CNetPay);
                    //23
                    PdfPCell CSignature = new PdfPCell(new Phrase("Bank A/c No./ Signature", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    CSignature.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    CSignature.Border = 15;
                    SecondtableHeadings.AddCell(CSignature);


                    #endregion

                    PdfPTable Secondtable = new PdfPTable(tableCells);
                    Secondtable.TotalWidth = 950f;
                    Secondtable.LockedWidth = true;
                    float[] SecondWidth = new float[] { 1.5f, 2f, 4f, 3f, 1.8f, 2f, 2f, 1.8f, 1.5f, 1.5f, 1.6f, 2f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.8f, 2f, 7f };

                    Secondtable.SetWidths(SecondWidth);

                    #region Table Data
                    int rowCount = 0;
                    int pageCount = 0;
                    int i = nextpagei;
                    // for (int i = 0, j = 0; i < dt.Rows.Count; i++)
                    {
                        forConvert = 0;
                        if (dt.Rows[i]["NoOfDuties"].ToString().Trim().Length > 0)
                            forConvert = Convert.ToSingle(dt.Rows[i]["NoOfDuties"].ToString());

                        if (dt.Rows[i]["PF"].ToString().Trim().Length > 0)
                            forConvert = (float)Math.Round(Convert.ToSingle(dt.Rows[i]["PF"].ToString()));

                        if (dt.Rows[i]["ESI"].ToString().Trim().Length > 0)
                            forConvert = (float)Math.Round(Convert.ToSingle(dt.Rows[i]["ESI"].ToString()));
                        if (forConvert > 0)
                        {
                            if (nextpagerecordscount == 0)
                            {
                                document.Add(SecondtableHeadings);
                            }


                            nextpagerecordscount++;
                            //1
                            PdfPCell CSNo = new PdfPCell(new Phrase((++j).ToString() + "\n \n \n  ", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                            CSNo.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            CSNo.Border = 15;
                            Secondtable.AddCell(CSNo);
                            //2
                            PdfPCell CEmpId1 = new PdfPCell(new Phrase(dt.Rows[i]["EmpId"].ToString(), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                            CEmpId1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            CEmpId1.Border = 15;
                            Secondtable.AddCell(CEmpId1);
                            //3
                            PdfPCell CEmpName1 = new PdfPCell(new Phrase(dt.Rows[i]["EmpFName"].ToString() + " " + dt.Rows[i]["EmpMName"].ToString(), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                            CEmpName1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            CEmpName1.Border = 15;
                            Secondtable.AddCell(CEmpName1);
                            //4
                            PdfPCell CEmpDesgn = new PdfPCell(new Phrase(dt.Rows[i]["Desgn"].ToString(), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                            CEmpDesgn.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            CEmpDesgn.Border = 15;
                            Secondtable.AddCell(CEmpDesgn);
                            //5

                            forConvert = 0;
                            if (dt.Rows[i]["NoOfDuties"].ToString().Trim().Length > 0)
                                forConvert = Convert.ToSingle(dt.Rows[i]["NoOfDuties"].ToString());
                            totalDuties += forConvert;
                            GrandtotalDuties += forConvert;
                            PdfPCell CNoOfDuties = new PdfPCell(new Phrase(forConvert.ToString("0.00"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                            CNoOfDuties.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            CNoOfDuties.Border = 15;
                            Secondtable.AddCell(CNoOfDuties);

                            //6
                            forConvert = 0;
                            if (dt.Rows[i]["salaryrate"].ToString().Trim().Length > 0)
                                forConvert = Convert.ToSingle(dt.Rows[i]["salaryrate"].ToString());
                            PdfPCell CSalRate = new PdfPCell(new Phrase(forConvert.ToString("0.00"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                            CSalRate.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            CSalRate.Border = 15;
                            Secondtable.AddCell(CSalRate);

                            //7
                            forConvert = 0;
                            if (dt.Rows[i]["Basic"].ToString().Trim().Length > 0)
                                forConvert = (float)Math.Round(Convert.ToSingle(dt.Rows[i]["Basic"].ToString()));
                            totalBasic += forConvert;
                            GrandtotalBasic += forConvert;


                            PdfPCell CBasic1 = new PdfPCell(new Phrase(forConvert.ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                            CBasic1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            CBasic1.Border = 15;
                            Secondtable.AddCell(CBasic1);

                            //8
                            forConvert = 0;
                            if (dt.Rows[i]["DA"].ToString().Trim().Length > 0)
                                forConvert = (float)Math.Round(Convert.ToSingle(dt.Rows[i]["DA"].ToString()));
                            totalDA += forConvert;
                            GrandtotalDA += forConvert;
                            PdfPCell CDA1 = new PdfPCell(new Phrase(forConvert.ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                            CDA1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            CDA1.Border = 15;
                            Secondtable.AddCell(CDA1);

                            //9
                            forConvert = 0;
                            if (dt.Rows[i]["HRA"].ToString().Trim().Length > 0)
                                forConvert = (float)Math.Round(Convert.ToSingle(dt.Rows[i]["HRA"].ToString()));
                            totalHRA += forConvert;
                            GrandtotalHRA += forConvert;

                            PdfPCell CHRA1 = new PdfPCell(new Phrase(forConvert.ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                            CHRA1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            CHRA1.Border = 15;
                            Secondtable.AddCell(CHRA1);

                            //10
                            forConvert = 0;
                            if (dt.Rows[i]["OtherAllowance"].ToString().Trim().Length > 0)
                                forConvert = (float)Math.Round(Convert.ToSingle(dt.Rows[i]["OtherAllowance"].ToString()));
                            totalOA += forConvert;
                            GrandtotalOA += forConvert;
                            PdfPCell COA1 = new PdfPCell(new Phrase(forConvert.ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                            COA1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            COA1.Border = 15;
                            Secondtable.AddCell(COA1);

                            //11


                            forConvert = 0;
                            if (dt.Rows[i]["Bonus"].ToString().Trim().Length > 0)
                                forConvert = (float)Math.Round(Convert.ToSingle(dt.Rows[i]["Bonus"].ToString()));
                            totalbonus += forConvert;
                            Grandtotalbonus += forConvert;

                            PdfPCell CdBonus = new PdfPCell(new Phrase(forConvert.ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                            CdBonus.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            CdBonus.Border = 15;
                            Secondtable.AddCell(CdBonus);


                            //12
                            forConvert = 0;
                            if (dt.Rows[i]["Gross"].ToString().Trim().Length > 0)
                                forConvert = (float)Math.Round(Convert.ToSingle(dt.Rows[i]["Gross"].ToString()));
                            totalGrass += forConvert;
                            GrandtotalGrass += forConvert;
                            PdfPCell CGross1 = new PdfPCell(new Phrase(forConvert.ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                            CGross1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            CGross1.Border = 15;
                            Secondtable.AddCell(CGross1);
                            //13

                            forConvert = 0;
                            if (dt.Rows[i]["PF"].ToString().Trim().Length > 0)
                                forConvert = (float)Math.Round(Convert.ToSingle(dt.Rows[i]["PF"].ToString()));
                            totalPF += forConvert;
                            GrandtotalPF += forConvert;
                            PdfPCell CPF1 = new PdfPCell(new Phrase(forConvert.ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                            CPF1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            CPF1.Border = 15;
                            Secondtable.AddCell(CPF1);
                            //14

                            forConvert = 0;
                            if (dt.Rows[i]["ESI"].ToString().Trim().Length > 0)
                                forConvert = (float)Math.Round(Convert.ToSingle(dt.Rows[i]["ESI"].ToString()));
                            totalESI += forConvert;
                            GrandtotalESI += forConvert;
                            PdfPCell CESI1 = new PdfPCell(new Phrase(forConvert.ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                            CESI1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            CESI1.Border = 15;
                            Secondtable.AddCell(CESI1);

                            //15
                            forConvert = 0;
                            if (dt.Rows[i]["ProfTax"].ToString().Trim().Length > 0)
                                forConvert = (float)Math.Round(Convert.ToSingle(dt.Rows[i]["ProfTax"].ToString()));
                            totalProfTax += forConvert;
                            GrandtotalProfTax += forConvert;
                            PdfPCell CProTax1 = new PdfPCell(new Phrase(forConvert.ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                            CProTax1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            CProTax1.Border = 15;
                            Secondtable.AddCell(CProTax1);

                            //16
                            forConvert = 0;
                            if (dt.Rows[i]["SalAdvDed"].ToString().Trim().Length > 0)
                                forConvert = (float)Math.Round(Convert.ToSingle(dt.Rows[i]["SalAdvDed"].ToString()));
                            totalSalAdv += forConvert;
                            GrandtotalSalAdv += forConvert;
                            PdfPCell CSalAdv1 = new PdfPCell(new Phrase(forConvert.ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                            CSalAdv1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            CSalAdv1.Border = 15;
                            Secondtable.AddCell(CSalAdv1);
                            forConvert = 0;
                            //17
                            if (dt.Rows[i]["UniformDed"].ToString().Trim().Length > 0)
                                forConvert = (float)Math.Round(Convert.ToSingle(dt.Rows[i]["UniformDed"].ToString()));
                            totalUniforDed += forConvert;
                            GrandtotalUniforDed += forConvert;

                            PdfPCell CUnifDed1 = new PdfPCell(new Phrase(forConvert.ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                            CUnifDed1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            CUnifDed1.Border = 15;
                            Secondtable.AddCell(CUnifDed1);
                            //18
                            forConvert = 0;

                            if (dt.Rows[i]["OWF"].ToString().Trim().Length > 0)
                                forConvert = (float)Math.Round(Convert.ToSingle(dt.Rows[i]["OWF"].ToString()));
                            totalOwf += forConvert;
                            GrandtotalOwf += forConvert;

                            PdfPCell COwf1 = new PdfPCell(new Phrase(forConvert.ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                            COwf1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            COwf1.Border = 15;
                            Secondtable.AddCell(COwf1);




                            //19
                            forConvert = 0;
                            if (dt.Rows[i]["Penalty"].ToString().Trim().Length > 0)
                                forConvert = (float)Math.Round(Convert.ToSingle(dt.Rows[i]["Penalty"].ToString()));
                            totalPenalty += forConvert;
                            GrandtotalPenalty += forConvert;

                            PdfPCell COtherDed1 = new PdfPCell(new Phrase(forConvert.ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                            COtherDed1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            COtherDed1.Border = 15;
                            Secondtable.AddCell(COtherDed1);
                            //20
                            forConvert = 0;
                            if (dt.Rows[i]["CanteenAdv"].ToString().Trim().Length > 0)
                                forConvert = (float)Math.Round(Convert.ToSingle(dt.Rows[i]["CanteenAdv"].ToString()));
                            totalCanteenAdv += forConvert;
                            GrandtotalCanteenAdv += forConvert;

                            PdfPCell CCanteenAdv = new PdfPCell(new Phrase(forConvert.ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                            CCanteenAdv.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            CCanteenAdv.Border = 15;
                            Secondtable.AddCell(CCanteenAdv);

                            //21
                            forConvert = 0;
                            float totaldeductions = 0;
                            if (dt.Rows[i]["Deductions"].ToString().Trim().Length > 0)
                            {
                                forConvert = (float)Math.Round(Convert.ToSingle(dt.Rows[i]["Deductions"].ToString()));
                                totaldeductions = forConvert;


                            }
                            totalDed += forConvert;
                            GrandtotalDed += forConvert;

                            PdfPCell CTotDed1 = new PdfPCell(new Phrase(forConvert.ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                            CTotDed1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            CTotDed1.Border = 15;
                            Secondtable.AddCell(CTotDed1);//OtherDed,Eps.Gross,Eps.Deductions,Eps.ActualAmount

                            //22
                            forConvert = 0;
                            if (dt.Rows[i]["gross"].ToString().Trim().Length > 0)
                                forConvert = (float)Math.Round(Convert.ToSingle(dt.Rows[i]["gross"].ToString()));

                            forConvert = forConvert - totaldeductions;

                            if (forConvert < 0)
                            {
                                forConvert = 0;
                            }

                            totalActualamount += forConvert;
                            GrandtotalActualamount += forConvert;

                            PdfPCell CNetPay1 = new PdfPCell(new Phrase(forConvert.ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                            CNetPay1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            CNetPay1.Border = 15;
                            Secondtable.AddCell(CNetPay1);
                            //23
                            forConvert = 0;
                            string EmpBankAcNo = dt.Rows[i]["EmpBankAcNo"].ToString();
                            PdfPCell CSignature1 = new PdfPCell(new Phrase(EmpBankAcNo, FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                            CSignature1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            CSignature1.Border = 15;
                            CSignature1.MinimumHeight = 25;
                            Secondtable.AddCell(CSignature1);
                        }

                    }
                    #endregion


                    SecondtableFooter = new PdfPTable(tableCells);
                    SecondtableFooter.TotalWidth = 950f;
                    SecondtableFooter.LockedWidth = true;
                    float[] SecondFooterWidth = new float[] { 1.5f, 2f, 4f, 3f, 1.8f, 2f, 2f, 1.8f, 1.5f, 1.5f, 1.6f, 2f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.8f, 2f, 7f };
                    SecondtableFooter.SetWidths(SecondFooterWidth);


                    #region Table Footer

                    //1
                    PdfPCell FCSNo = new PdfPCell(new Phrase("", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                    FCSNo.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCSNo.Border = 15;
                    SecondtableFooter.AddCell(FCSNo);
                    //2
                    PdfPCell FCEmpId1 = new PdfPCell(new Phrase("", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                    FCEmpId1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCEmpId1.Border = 15;
                    SecondtableFooter.AddCell(FCEmpId1);
                    //3
                    PdfPCell FCEmpName1 = new PdfPCell(new Phrase(" ", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                    FCEmpName1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCEmpName1.Border = 15;
                    SecondtableFooter.AddCell(FCEmpName1);
                    //4
                    PdfPCell FCEmpDesgn = new PdfPCell(new Phrase("", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                    FCEmpDesgn.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCEmpDesgn.Border = 15;
                    //FCEmpDesgn.Colspan = 4;
                    SecondtableFooter.AddCell(FCEmpDesgn);
                    //5
                    PdfPCell FCNoOfDuties = new PdfPCell(new Phrase(totalDuties.ToString("0.00"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    FCNoOfDuties.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCNoOfDuties.Border = 15;
                    SecondtableFooter.AddCell(FCNoOfDuties);
                    //6
                    PdfPCell FCNoOfots = new PdfPCell(new Phrase("", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    FCNoOfots.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCNoOfots.Border = 15;
                    SecondtableFooter.AddCell(FCNoOfots);

                    //7
                    PdfPCell FCBasic1 = new PdfPCell(new Phrase(Math.Round(totalBasic).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    FCBasic1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCBasic1.Border = 15;
                    SecondtableFooter.AddCell(FCBasic1);
                    //8

                    forConvert = 0;
                    PdfPCell FCHRA1 = new PdfPCell(new Phrase(Math.Round(totalDA).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    FCHRA1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCHRA1.Border = 15;
                    SecondtableFooter.AddCell(FCHRA1);

                    //9
                    PdfPCell FCConveyance = new PdfPCell(new Phrase(Math.Round(totalHRA).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    FCConveyance.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCConveyance.Border = 15;
                    SecondtableFooter.AddCell(FCConveyance);

                    //10
                    PdfPCell FCcca = new PdfPCell(new Phrase(Math.Round(totalOA).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    FCcca.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCcca.Border = 15;
                    SecondtableFooter.AddCell(FCcca);

                    //11
                    PdfPCell FBasic = new PdfPCell(new Phrase(Math.Round(totalbonus).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    FBasic.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FBasic.Border = 15;
                    SecondtableFooter.AddCell(FBasic);

                    //12
                    PdfPCell FCGross1 = new PdfPCell(new Phrase(Math.Round(totalGrass).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    FCGross1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCGross1.Border = 15;
                    SecondtableFooter.AddCell(FCGross1);
                    //13
                    PdfPCell FCPF1 = new PdfPCell(new Phrase(Math.Round(totalPF).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    FCPF1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCPF1.Border = 15;
                    SecondtableFooter.AddCell(FCPF1);
                    //14
                    PdfPCell FCESI1 = new PdfPCell(new Phrase(Math.Round(totalESI).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    FCESI1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCESI1.Border = 15;
                    SecondtableFooter.AddCell(FCESI1);
                    //15
                    PdfPCell FCProTax1 = new PdfPCell(new Phrase(Math.Round(totalProfTax).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    FCProTax1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCProTax1.Border = 15;
                    SecondtableFooter.AddCell(FCProTax1);
                    //16
                    PdfPCell FCSalAdv1 = new PdfPCell(new Phrase(Math.Round(totalSalAdv).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    FCSalAdv1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCSalAdv1.Border = 15;
                    SecondtableFooter.AddCell(FCSalAdv1);
                    //17
                    PdfPCell FCUnifDed1 = new PdfPCell(new Phrase(Math.Round(totalUniforDed).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    FCUnifDed1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCUnifDed1.Border = 15;
                    SecondtableFooter.AddCell(FCUnifDed1);

                    //18
                    PdfPCell FCOwf1 = new PdfPCell(new Phrase(Math.Round(totalOwf).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    FCOwf1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCOwf1.Border = 15;
                    SecondtableFooter.AddCell(FCOwf1);
                    //19
                    PdfPCell FCOtherDed1 = new PdfPCell(new Phrase(Math.Round(totalPenalty).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    FCOtherDed1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCOtherDed1.Border = 15;
                    SecondtableFooter.AddCell(FCOtherDed1);
                    //20
                    PdfPCell FCTotDed1 = new PdfPCell(new Phrase(Math.Round(totalCanteenAdv).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    FCTotDed1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCTotDed1.Border = 15;
                    SecondtableFooter.AddCell(FCTotDed1);//OtherDed,Eps.Gross,Eps.Deductions,Eps.ActualAmount
                    //21
                    PdfPCell FCNetPay1 = new PdfPCell(new Phrase(Math.Round(totalDed).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    FCNetPay1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCNetPay1.Border = 15;
                    //FCNetPay1.Colspan = 2;
                    SecondtableFooter.AddCell(FCNetPay1);
                    //22
                    PdfPCell FCSignature1 = new PdfPCell(new Phrase(Math.Round(totalActualamount).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    FCSignature1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCSignature1.Border = 15;
                    //FCSignature1.MinimumHeight = 25;
                    SecondtableFooter.AddCell(FCSignature1);

                    //23
                    PdfPCell FCSignature12 = new PdfPCell(new Phrase(" ", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                    FCSignature12.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCSignature12.Border = 15;
                    //FCSignature1.MinimumHeight = 25;
                    SecondtableFooter.AddCell(FCSignature12);

                    #endregion

                    SecondtablecheckedbyFooter = new PdfPTable(tableCells);
                    SecondtablecheckedbyFooter.TotalWidth = 950f;
                    SecondtablecheckedbyFooter.LockedWidth = true;
                    float[] SecondcheckedbyFooterWidth = new float[] { 1.5f, 2f, 4f, 3f, 1.8f, 2f, 2f, 1.8f, 1.5f, 1.5f, 1.6f, 2f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.8f, 2f, 7f };
                    SecondtablecheckedbyFooter.SetWidths(SecondcheckedbyFooterWidth);


                    #region Table  Grand  Total  Footer

                    //1
                    PdfPCell GFCSNo = new PdfPCell(new Phrase("", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                    GFCSNo.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCSNo.Border = 15;
                    SecondtablecheckedbyFooter.AddCell(GFCSNo);
                    //2
                    PdfPCell GFCEmpId1 = new PdfPCell(new Phrase("", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                    GFCEmpId1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCEmpId1.Border = 15;
                    SecondtablecheckedbyFooter.AddCell(GFCEmpId1);
                    //3
                    PdfPCell GFCEmpName1 = new PdfPCell(new Phrase("Grand  Total: ", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                    GFCEmpName1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCEmpName1.Border = 15;
                    SecondtablecheckedbyFooter.AddCell(GFCEmpName1);
                    //4
                    PdfPCell GFCEmpDesgn = new PdfPCell(new Phrase("", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                    GFCEmpDesgn.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCEmpDesgn.Border = 15;
                    //FCEmpDesgn.Colspan = 4;
                    SecondtablecheckedbyFooter.AddCell(GFCEmpDesgn);
                    //5
                    PdfPCell GFCNoOfDuties = new PdfPCell(new Phrase(GrandtotalDuties.ToString("0.00"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    GFCNoOfDuties.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCNoOfDuties.Border = 15;
                    SecondtablecheckedbyFooter.AddCell(GFCNoOfDuties);
                    //6
                    PdfPCell GFCNoOfots = new PdfPCell(new Phrase("", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    GFCNoOfots.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCNoOfots.Border = 15;
                    SecondtablecheckedbyFooter.AddCell(GFCNoOfots);

                    //7
                    PdfPCell GFCBasic1 = new PdfPCell(new Phrase(Math.Round(GrandtotalBasic).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    GFCBasic1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCBasic1.Border = 15;
                    SecondtablecheckedbyFooter.AddCell(GFCBasic1);
                    //8

                    forConvert = 0;
                    PdfPCell GFCHRA1 = new PdfPCell(new Phrase(Math.Round(GrandtotalDA).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    GFCHRA1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCHRA1.Border = 15;
                    SecondtablecheckedbyFooter.AddCell(GFCHRA1);

                    //9
                    PdfPCell GFCConveyance = new PdfPCell(new Phrase(Math.Round(GrandtotalHRA).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    GFCConveyance.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCConveyance.Border = 15;
                    SecondtablecheckedbyFooter.AddCell(GFCConveyance);

                    //10
                    PdfPCell GFCcca = new PdfPCell(new Phrase(Math.Round(GrandtotalOA).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    GFCcca.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCcca.Border = 15;
                    SecondtablecheckedbyFooter.AddCell(GFCcca);

                    //11
                    PdfPCell GFBasic = new PdfPCell(new Phrase(Math.Round(Grandtotalbonus).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    GFBasic.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFBasic.Border = 15;
                    SecondtablecheckedbyFooter.AddCell(GFBasic);

                    //12
                    PdfPCell GFCGross1 = new PdfPCell(new Phrase(Math.Round(GrandtotalGrass).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    GFCGross1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCGross1.Border = 15;
                    SecondtablecheckedbyFooter.AddCell(GFCGross1);
                    //13
                    PdfPCell GFCPF1 = new PdfPCell(new Phrase(Math.Round(GrandtotalPF).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    GFCPF1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCPF1.Border = 15;
                    SecondtablecheckedbyFooter.AddCell(GFCPF1);
                    //14
                    PdfPCell GFCESI1 = new PdfPCell(new Phrase(Math.Round(GrandtotalESI).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    GFCESI1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCESI1.Border = 15;
                    SecondtablecheckedbyFooter.AddCell(GFCESI1);
                    //15
                    PdfPCell GFCProTax1 = new PdfPCell(new Phrase(Math.Round(GrandtotalProfTax).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    GFCProTax1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCProTax1.Border = 15;
                    SecondtablecheckedbyFooter.AddCell(GFCProTax1);
                    //16
                    PdfPCell GFCSalAdv1 = new PdfPCell(new Phrase(Math.Round(GrandtotalSalAdv).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    GFCSalAdv1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCSalAdv1.Border = 15;
                    SecondtablecheckedbyFooter.AddCell(GFCSalAdv1);
                    //17
                    PdfPCell GFCUnifDed1 = new PdfPCell(new Phrase(Math.Round(GrandtotalUniforDed).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    GFCUnifDed1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCUnifDed1.Border = 15;
                    SecondtablecheckedbyFooter.AddCell(GFCUnifDed1);

                    //18
                    PdfPCell GFCOwf1 = new PdfPCell(new Phrase(Math.Round(GrandtotalOwf).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    GFCOwf1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCOwf1.Border = 15;
                    SecondtablecheckedbyFooter.AddCell(GFCOwf1);
                    //19
                    PdfPCell GFCOtherDed1 = new PdfPCell(new Phrase(Math.Round(GrandtotalPenalty).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    GFCOtherDed1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCOtherDed1.Border = 15;
                    SecondtablecheckedbyFooter.AddCell(GFCOtherDed1);
                    //20
                    PdfPCell GFCTotDed1 = new PdfPCell(new Phrase(Math.Round(GrandtotalCanteenAdv).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    GFCTotDed1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCTotDed1.Border = 15;
                    SecondtablecheckedbyFooter.AddCell(GFCTotDed1);//OtherDed,Eps.Gross,Eps.Deductions,Eps.ActualAmount
                    //21
                    PdfPCell GFCNetPay1 = new PdfPCell(new Phrase(Math.Round(GrandtotalDed).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    GFCNetPay1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCNetPay1.Border = 15;
                    //FCNetPay1.Colspan = 2;
                    SecondtablecheckedbyFooter.AddCell(GFCNetPay1);
                    //22
                    PdfPCell GFCSignature1 = new PdfPCell(new Phrase(Math.Round(GrandtotalActualamount).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    GFCSignature1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCSignature1.Border = 15;
                    //FCSignature1.MinimumHeight = 25;
                    SecondtablecheckedbyFooter.AddCell(GFCSignature1);

                    //23
                    PdfPCell GFCSignature12 = new PdfPCell(new Phrase(" ", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                    GFCSignature12.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCSignature12.Border = 15;
                    //FCSignature1.MinimumHeight = 25;
                    SecondtablecheckedbyFooter.AddCell(GFCSignature12);

                    #endregion


                    #region   Footer Headings

                    //1
                    PdfPCell FHCheckedbybr1 = new PdfPCell(new Phrase("", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                    FHCheckedbybr1.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    FHCheckedbybr1.Border = 0;
                    FHCheckedbybr1.Rowspan = 0;
                    FHCheckedbybr1.Colspan = 12;
                    SecondtablecheckedbyFooter.AddCell(FHCheckedbybr1);
                    //2
                    PdfPCell FHApprovedbr2 = new PdfPCell(new Phrase("  ", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                    FHApprovedbr2.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    FHApprovedbr2.Border = 0;
                    FHApprovedbr2.Colspan = 11;
                    SecondtablecheckedbyFooter.AddCell(FHApprovedbr2);
                    SecondtablecheckedbyFooter.AddCell(FHCheckedbybr1);
                    SecondtablecheckedbyFooter.AddCell(FHApprovedbr2);
                    //1
                    PdfPCell FHCheckedby = new PdfPCell(new Phrase("Checked By", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                    FHCheckedby.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    FHCheckedby.Border = 0;
                    FHCheckedby.Rowspan = 0;
                    FHCheckedby.Colspan = 12;
                    SecondtablecheckedbyFooter.AddCell(FHCheckedby);
                    //2
                    PdfPCell FHApprovedBy = new PdfPCell(new Phrase("Gross  Approved By", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                    FHApprovedBy.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    FHApprovedBy.Border = 0;
                    FHApprovedBy.Colspan = 11;
                    SecondtablecheckedbyFooter.AddCell(FHApprovedBy);

                    #endregion

                    document.Add(Secondtable);

                    if (nextpagerecordscount == targetpagerecors)
                    {
                        targetpagerecors = secondpagerecords;
                        document.Add(SecondtableFooter);
                        document.NewPage();
                        nextpagerecordscount = 0;

                        totalActualamount = 0;
                        totalDuties = 0;
                        totalOts = 0;
                        totalBasic = 0;
                        totalDA = 0;
                        totalHRA = 0;
                        totalCCA = 0;
                        totalConveyance = 0;
                        totalWA = 0;
                        totalOA = 0;
                        totalGrass = 0;
                        totalOTAmount = 0;
                        totalPF = 0;
                        totalESI = 0;
                        totalProfTax = 0;
                        totalDed = 0;
                        totalSalAdv = 0;
                        totalUniforDed = 0;
                        totalCanteenAdv = 0;
                        totalOwf = 0;
                        totalPenalty = 0;
                        totalGross = 0;
                        totalbonus = 0;
                    }

                }


                if (nextpagerecordscount >= 0)
                {
                    document.Add(SecondtablecheckedbyFooter);
                }

                if (nextpagehasPages)
                {
                    document.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=Wage Sheet for Duties.pdf");
                    Response.Buffer = true;
                    Response.Clear();
                    Response.OutputStream.Write(ms.GetBuffer(), 0, ms.GetBuffer().Length);
                    Response.OutputStream.Flush();
                    Response.End();
                }
            }
        }

        protected void btnonlyots_Click(object sender, EventArgs e)
        {
            if (ddlclient.SelectedIndex <= 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showlalert", "alert('Please Select Client ID/Name');", true);
                return;
            }

            string month = Getmonth();
            if (month.Trim().Length == 0)
            {
                return;
            }
            string selectmonth = string.Empty;
            if (ddlnoofattendance.SelectedIndex == 0)
            {
                selectmonth = "select Eps.EmpId,Eps.Desgn,TempGross as salaryrate, Eps.NoOfDuties,Eps.Basic,Eps.DA,Eps.HRA,Eps.CCA," +
                     " Eps.Conveyance,Eps.WashAllowance,Eps.OtherAllowance,Eps.PF,Eps.ESI,Eps.OTAmt,Eps.ProfTax," +
                   "Eps.SalAdvDed,Eps.CanteenAdv,Eps.UniformDed,Eps.Penalty,Eps.OtherDed,Eps.Gross,Eps.Deductions,Eps.ActualAmount,Eps.OWF, " +
                   " EmpDetails.EmpFName,EmpDetails.EmpMName," +
                   "EmpDetails.UnitId, EmpDetails.EmpBankAppNo,Empdetails.EmpBankAcNo, EmpDetails.EmpBankCardRef,EmpAttendance.OT from EmpPaySheet as Eps  " +
                   " INNER JOIN EmpDetails ON Eps.EmpId = EmpDetails.EmpId INNER JOIN EmpAttendance ON EmpAttendance.EmpId=Eps.EmpId AND  " +
                   " EmpAttendance.ClientId=Eps.ClientId  And  Eps.NoOfDuties>10 AND EmpAttendance.Month=Eps.Month AND Eps.ClientId='" +
                    // ddlClients.SelectedValue + "' AND Eps.Month=" + month + "  Order by   case when EmpDetails.EmpBankAcNo is null then 1 else 0 end, Eps.EmpId   "; 
                   ddlclient.SelectedValue + "'  And  Eps.PF<>0  And Eps.Esi<>0 AND Eps.Month=" + month + "  and EmpAttendance.Design=Eps.Desgn  Order by Right(Eps.EmpId,6)";
            }
            if (ddlnoofattendance.SelectedIndex == 1)
            {
                selectmonth = "select Eps.EmpId,Eps.Desgn,TempGross as salaryrate, Eps.NoOfDuties,Eps.Basic,Eps.DA,Eps.HRA,Eps.CCA," +
                        " Eps.Conveyance,Eps.WashAllowance,Eps.OtherAllowance,Eps.PF,Eps.ESI,Eps.OTAmt,Eps.ProfTax," +
                      "Eps.SalAdvDed,Eps.CanteenAdv,Eps.UniformDed,Eps.Penalty,Eps.OtherDed,Eps.Gross,Eps.Deductions,Eps.ActualAmount,Eps.OWF, " +
                      " EmpDetails.EmpFName,EmpDetails.EmpMName," +
                      "EmpDetails.UnitId, EmpDetails.EmpBankAppNo,Empdetails.EmpBankAcNo, EmpDetails.EmpBankCardRef,EmpAttendance.OT from EmpPaySheet as Eps  " +
                      " INNER JOIN EmpDetails ON Eps.EmpId = EmpDetails.EmpId INNER JOIN EmpAttendance ON EmpAttendance.EmpId=Eps.EmpId AND  " +
                      " EmpAttendance.ClientId=Eps.ClientId  And  Eps.NoOfDuties<=10 AND EmpAttendance.Month=Eps.Month AND Eps.ClientId='" +
                    // ddlClients.SelectedValue + "' AND Eps.Month=" + month + "  Order by   case when EmpDetails.EmpBankAcNo is null then 1 else 0 end, Eps.EmpId   "; 
                      ddlclient.SelectedValue + "'  And  Eps.PF<>0  And Eps.Esi<>0 AND Eps.Month=" + month + "  and EmpAttendance.Design=Eps.Desgn  Order by Right(Eps.EmpId,6)";
            }

            if (ddlnoofattendance.SelectedIndex == 2)
            {
                selectmonth = "select Eps.EmpId,Eps.Desgn,TempGross as salaryrate, Eps.NoOfDuties,Eps.Basic,Eps.DA,Eps.HRA,Eps.CCA," +
                        " Eps.Conveyance,Eps.WashAllowance,Eps.OtherAllowance,Eps.PF,Eps.ESI,Eps.OTAmt,Eps.ProfTax," +
                      "Eps.SalAdvDed,Eps.CanteenAdv,Eps.UniformDed,Eps.Penalty,Eps.OtherDed,Eps.Gross,Eps.Deductions,Eps.ActualAmount,Eps.OWF, " +
                      " EmpDetails.EmpFName,EmpDetails.EmpMName," +
                      "EmpDetails.UnitId, EmpDetails.EmpBankAppNo,Empdetails.EmpBankAcNo, EmpDetails.EmpBankCardRef,EmpAttendance.OT from EmpPaySheet as Eps  " +
                      " INNER JOIN EmpDetails ON Eps.EmpId = EmpDetails.EmpId INNER JOIN EmpAttendance ON EmpAttendance.EmpId=Eps.EmpId AND  " +
                      " EmpAttendance.ClientId=Eps.ClientId   AND EmpAttendance.Month=Eps.Month AND Eps.ClientId='" +
                    // ddlClients.SelectedValue + "' AND Eps.Month=" + month + "  Order by   case when EmpDetails.EmpBankAcNo is null then 1 else 0 end, Eps.EmpId   "; 
                      ddlclient.SelectedValue + "'  And  Eps.PF<>0  And Eps.Esi<>0 AND Eps.Month=" + month + " and EmpAttendance.Design=Eps.Desgn   Order by Right(Eps.EmpId,6)";
            }
            DataTable dt = config.ExecuteAdaptorAsyncWithQueryParams(selectmonth).Result;

            MemoryStream ms = new MemoryStream();
            if (dt.Rows.Count > 0)
            {
                Document document = new Document(PageSize.LEGAL.Rotate());
                PdfWriter writer = PdfWriter.GetInstance(document, ms);
                document.Open();
                document.AddTitle("FaMS");
                document.AddAuthor("WebWonders");
                document.AddSubject("Only OTs");
                document.AddKeywords("Keyword1, keyword2, …");//
                float forConvert;
                string strQry = "Select * from CompanyInfo";
                DataTable compInfo = config.ExecuteAdaptorAsyncWithQueryParams(strQry).Result;
                string companyName1 = "Your Company Name";
                string companyAddress = "Your Company Address";
                if (compInfo.Rows.Count > 0)
                {
                    companyName1 = compInfo.Rows[0]["CompanyName"].ToString();
                    companyAddress = compInfo.Rows[0]["Address"].ToString();
                }

                int tableCells = 21;
                #region variables for total
                float totalOts = 0;
                float totalOtHours = 0;
                float totalOTAmount = 0;
                #endregion
                #region Titles of Document
                PdfPTable Maintable = new PdfPTable(tableCells);
                Maintable.TotalWidth = 950f;
                Maintable.LockedWidth = true;
                float[] width = new float[] { 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f };
                Maintable.SetWidths(width);
                uint FONT_SIZE = 10;

                //Company Name & vage act details
                PdfPCell cellemp = new PdfPCell(new Phrase("  ", FontFactory.GetFont(Fontstyle, 10, Font.BOLD, BaseColor.BLACK)));
                cellemp.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                cellemp.Colspan = tableCells;
                cellemp.Border = 0;

                PdfPCell Heading = new PdfPCell(new Phrase("Form - XVII REGISTER OF WAGES", FontFactory.GetFont(Fontstyle, 20, Font.BOLD, BaseColor.BLACK)));
                Heading.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                Heading.Colspan = tableCells;
                Heading.Border = 0;
                Maintable.AddCell(Heading);

                PdfPCell actDetails = new PdfPCell(new Phrase("(vide Rule 78(1) a(i) of Contract Labour (Reg. & abolition) Central & A.P. Rules)",
                    FontFactory.GetFont(Fontstyle, 15, Font.BOLD, BaseColor.BLACK)));
                actDetails.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                actDetails.Colspan = tableCells;
                actDetails.Border = 0;// 15;
                Maintable.AddCell(actDetails);

                Maintable.AddCell(cellemp);
                #endregion
                #region Table Headings
                PdfPCell companyName = new PdfPCell(new Phrase(companyName1, FontFactory.GetFont(Fontstyle, 20, Font.BOLD, BaseColor.BLACK)));
                companyName.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                companyName.Colspan = tableCells;
                companyName.Border = 0;// 15;
                Maintable.AddCell(companyName);

                PdfPCell paySheet = new PdfPCell(new Phrase("Pay Sheet", FontFactory.GetFont(Fontstyle, 15, Font.BOLD | Font.UNDERLINE, BaseColor.BLACK)));
                paySheet.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                paySheet.Colspan = tableCells;
                paySheet.Border = 0;// 15;
                Maintable.AddCell(paySheet);

                PdfPCell CClient = new PdfPCell(new Phrase("Client ID :   " + ddlclient.SelectedValue, FontFactory.GetFont(Fontstyle, 9, Font.NORMAL, BaseColor.BLACK)));
                CClient.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                CClient.Colspan = 3;
                CClient.Border = 0;// 15;
                Maintable.AddCell(CClient);

                PdfPCell CClientName = new PdfPCell(new Phrase("Client Name :   " + ddlcname.SelectedItem, FontFactory.GetFont(Fontstyle, 9, Font.NORMAL, BaseColor.BLACK)));
                CClientName.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                CClientName.Colspan = 6;
                CClientName.Border = 0;// 15;
                Maintable.AddCell(CClientName);

                PdfPCell CDates = new PdfPCell(new Phrase("Payment for the period of : " + GetPaymentPeriod(ddlclient.SelectedValue), FontFactory.GetFont(Fontstyle, 9, Font.NORMAL, BaseColor.BLACK)));
                CDates.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                CDates.Colspan = 6;
                CDates.Border = 0;// 15;
                Maintable.AddCell(CDates);

                //PdfPCell CPaySheetDate = new PdfPCell(new Phrase("Pay Sheet Date :  " + DateTime.Now.Date.ToString("dd/MM/yyyy"), FontFactory.GetFont(Fontstyle, 9, Font.NORMAL, BaseColor.BLACK)));
                PdfPCell CPaySheetDate = new PdfPCell(new Phrase("", FontFactory.GetFont(Fontstyle, 9, Font.NORMAL, BaseColor.BLACK)));
                CPaySheetDate.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                CPaySheetDate.Colspan = 3;
                CPaySheetDate.Border = 0;// 15;
                Maintable.AddCell(CPaySheetDate);

                PdfPCell CPayMonth = new PdfPCell(new Phrase("For the month of :   " + DateTime.Parse(txtmonth.Text, CultureInfo.GetCultureInfo("en-gb")).ToString("MMMM"), FontFactory.GetFont(Fontstyle, 9, Font.NORMAL, BaseColor.BLACK)));
                CPayMonth.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                CPayMonth.Colspan = 3;
                CPayMonth.Border = 0;// 15;
                Maintable.AddCell(CPayMonth);

                Maintable.AddCell(cellemp);
                document.Add(Maintable);
                tableCells = 9;

                PdfPTable Secondtable = new PdfPTable(tableCells);
                Secondtable.TotalWidth = 950f;
                //Secondtable.
                Secondtable.LockedWidth = true;
                float[] SecondWidth = new float[] { 0.8f, 1.3f, 3f, 2f, 1f, 1f, 1f, 1f, 4f };
                Secondtable.SetWidths(SecondWidth);

                //Cell Headings
                PdfPCell sNo = new PdfPCell(new Phrase("S . No.", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                sNo.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                //sNo.Colspan = 1;
                sNo.Border = 15;// 15;

                Secondtable.AddCell(sNo);

                PdfPCell CEmpId = new PdfPCell(new Phrase("Emp Id", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                CEmpId.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                CEmpId.Border = 15;// 15;
                Secondtable.AddCell(CEmpId);

                PdfPCell CEmpName = new PdfPCell(new Phrase("Emp Name", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                CEmpName.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                CEmpName.Border = 15;// 15;
                Secondtable.AddCell(CEmpName);

                PdfPCell CDesgn = new PdfPCell(new Phrase("Desgn", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                CDesgn.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                CDesgn.Border = 15;
                Secondtable.AddCell(CDesgn);



                PdfPCell COTs = new PdfPCell(new Phrase("OTs", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                COTs.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                COTs.Border = 15;
                Secondtable.AddCell(COTs);



                PdfPCell COThrs = new PdfPCell(new Phrase("OThrs", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                COThrs.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                COThrs.Border = 15;
                Secondtable.AddCell(COThrs);



                PdfPCell CSalaryRate = new PdfPCell(new Phrase("S.Rate", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                CSalaryRate.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                CSalaryRate.Border = 15;
                Secondtable.AddCell(CSalaryRate);



                PdfPCell COTamt = new PdfPCell(new Phrase("OTAmt", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                COTamt.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                COTamt.Border = 15;
                Secondtable.AddCell(COTamt);

                PdfPCell CSignature = new PdfPCell(new Phrase("Bank A/c No./ Signature", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                CSignature.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                CSignature.Border = 15;
                Secondtable.AddCell(CSignature);
                #endregion
                #region Table Data
                //int rowCount = 0;
                //int pageCount = 0;
                for (int i = 0, j = 0; i < dt.Rows.Count; i++)
                {
                    forConvert = 0;


                    if (dt.Rows[i]["OT"].ToString().Trim().Length > 0)
                        forConvert = Convert.ToSingle(dt.Rows[i]["OT"].ToString());


                    if (forConvert > 0)
                    {
                        PdfPCell CSNo = new PdfPCell(new Phrase((++j).ToString(), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                        CSNo.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                        CSNo.Border = 15;
                        Secondtable.AddCell(CSNo);

                        PdfPCell CEmpId1 = new PdfPCell(new Phrase(dt.Rows[i]["EmpId"].ToString(), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                        CEmpId1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                        CEmpId1.Border = 15;
                        Secondtable.AddCell(CEmpId1);

                        PdfPCell CEmpName1 = new PdfPCell(new Phrase(dt.Rows[i]["EmpFName"].ToString() + " " + dt.Rows[i]["EmpMName"].ToString(), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                        CEmpName1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                        CEmpName1.Border = 15;
                        Secondtable.AddCell(CEmpName1);

                        PdfPCell CEmpDesgn = new PdfPCell(new Phrase(dt.Rows[i]["Desgn"].ToString(), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                        CEmpDesgn.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                        CEmpDesgn.Border = 15;
                        Secondtable.AddCell(CEmpDesgn);

                        forConvert = 0;
                        if (dt.Rows[i]["OT"].ToString().Trim().Length > 0)
                            forConvert = Convert.ToSingle(dt.Rows[i]["OT"].ToString());
                        totalOts += forConvert;
                        PdfPCell CNoOfots = new PdfPCell(new Phrase(forConvert.ToString("0.0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                        CNoOfots.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                        CNoOfots.Border = 15;
                        Secondtable.AddCell(CNoOfots);


                        float tothrs = forConvert * 8;
                        totalOtHours += tothrs;
                        PdfPCell COthrs = new PdfPCell(new Phrase(tothrs.ToString("0.0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                        COthrs.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                        COthrs.Border = 15;
                        Secondtable.AddCell(COthrs);


                        if (dt.Rows[i]["salaryrate"].ToString().Trim().Length > 0)
                            forConvert = Convert.ToSingle(dt.Rows[i]["salaryrate"].ToString());
                        PdfPCell CSalRate = new PdfPCell(new Phrase(forConvert.ToString("0.0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                        CSalRate.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                        CSalRate.Border = 15;
                        Secondtable.AddCell(CSalRate);

                        forConvert = 0;
                        forConvert = (float)Math.Round(Convert.ToSingle(dt.Rows[i]["otamt"].ToString()));
                        totalOTAmount += forConvert;
                        PdfPCell CSalotamt = new PdfPCell(new Phrase(forConvert.ToString("0.0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                        CSalotamt.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                        CSalotamt.Border = 15;
                        Secondtable.AddCell(CSalotamt);
                        string EmpBankAcNo = dt.Rows[i]["EmpBankAcNo"].ToString();

                        PdfPCell CEmpBankAcNo = new PdfPCell(new Phrase(EmpBankAcNo, FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                        CEmpBankAcNo.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                        CEmpBankAcNo.Border = 15;
                        CEmpBankAcNo.MinimumHeight = 25;
                        Secondtable.AddCell(CEmpBankAcNo);
                    }
                }
                #endregion
                #region Table Footer
                //1
                PdfPCell FCSNo = new PdfPCell(new Phrase("", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                FCSNo.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                FCSNo.Border = 15;
                Secondtable.AddCell(FCSNo);
                //2
                PdfPCell FCEmpId1 = new PdfPCell(new Phrase("", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                FCEmpId1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                FCEmpId1.Border = 15;
                Secondtable.AddCell(FCEmpId1);
                //3
                PdfPCell FCEmpName1 = new PdfPCell(new Phrase(" ", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                FCEmpName1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                FCEmpName1.Border = 15;
                Secondtable.AddCell(FCEmpName1);
                //4
                PdfPCell FCEmpDesgn = new PdfPCell(new Phrase("", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                FCEmpDesgn.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                FCEmpDesgn.Border = 15;
                //FCEmpDesgn.Colspan = 4;
                Secondtable.AddCell(FCEmpDesgn);
                //5
                PdfPCell FCNoOfDuties = new PdfPCell(new Phrase(totalOts.ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                FCNoOfDuties.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                FCNoOfDuties.Border = 15;
                Secondtable.AddCell(FCNoOfDuties);
                //6
                PdfPCell FCNoOfots = new PdfPCell(new Phrase(totalOtHours.ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                FCNoOfots.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                FCNoOfots.Border = 15;
                Secondtable.AddCell(FCNoOfots);

                //7
                PdfPCell FCBasic1 = new PdfPCell(new Phrase("", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                FCBasic1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                FCBasic1.Border = 15;
                Secondtable.AddCell(FCBasic1);
                //8

                forConvert = 0;
                PdfPCell FCHRA1 = new PdfPCell(new Phrase(Math.Round(totalOTAmount).ToString(), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                FCHRA1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                FCHRA1.Border = 15;
                Secondtable.AddCell(FCHRA1);
                //9
                PdfPCell FCSignature1 = new PdfPCell(new Phrase(" ", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                FCSignature1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                FCSignature1.Border = 15;
                //FCSignature1.MinimumHeight = 25;
                Secondtable.AddCell(FCSignature1);
                #endregion

                #region   Footer Headings


                //1
                PdfPCell FHCheckedbybr1 = new PdfPCell(new Phrase("", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                FHCheckedbybr1.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                FHCheckedbybr1.Border = 0;
                FHCheckedbybr1.Rowspan = 0;
                FHCheckedbybr1.Colspan = 11;
                Secondtable.AddCell(FHCheckedbybr1);
                //2
                PdfPCell FHApprovedbr2 = new PdfPCell(new Phrase("", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                FHApprovedbr2.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                FHApprovedbr2.Border = 0;
                FHApprovedbr2.Colspan = 10;
                Secondtable.AddCell(FHApprovedbr2);

                //1
                PdfPCell FHCheckedby = new PdfPCell(new Phrase("Checked By", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                FHCheckedby.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                FHCheckedby.Border = 0;
                FHCheckedby.Rowspan = 0;
                FHCheckedby.Colspan = 11;
                Secondtable.AddCell(FHCheckedby);
                //2

                PdfPCell FHApprovedBy1 = new PdfPCell(new Phrase("                                                                                                                                                                                                                                                                                                                                                                                                           ", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                FHApprovedBy1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                FHApprovedBy1.Border = 0;
                FHApprovedBy1.Colspan = 10;
                Secondtable.AddCell(FHApprovedBy1);
                Secondtable.AddCell(FHApprovedBy1);
                Secondtable.AddCell(FHApprovedBy1);

                PdfPCell FHApprovedBy = new PdfPCell(new Phrase("                                                      Checked By                                                                                                                                                                                                                                                                                                                                              Gross  Approved By", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                FHApprovedBy.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                FHApprovedBy.Border = 0;
                FHApprovedBy.Colspan = 10;
                Secondtable.AddCell(FHApprovedBy);



                #endregion
                document.Add(Secondtable);
                document.Close();

                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment;filename=Wage slip for OTs.pdf");
                Response.Buffer = true;
                Response.Clear();
                Response.OutputStream.Write(ms.GetBuffer(), 0, ms.GetBuffer().Length);
                Response.OutputStream.Flush();
                Response.End();
            }
        }

        protected void btnonlyduties2_Click(object sender, EventArgs e)
        {
            int titleofdocumentindex = 0;
            if (ddlclient.SelectedIndex <= 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showlalert", "alert('Please Select Client ID/Name');", true);
                return;
            }

            string month = Getmonth();
            if (month.Trim().Length == 0)
            {
                return;
            }

            string selectmonth = string.Empty;
            if (ddlnoofattendance.SelectedIndex == 0)
            {
                selectmonth = "select Eps.EmpId,Eps.Desgn,TempGross as salaryrate, Eps.NoOfDuties,Eps.Basic,Eps.DA,Eps.HRA,Eps.CCA, " +
                     " Eps.Conveyance,Eps.WashAllowance,Eps.OtherAllowance,Eps.PF,Eps.ESI,Eps.OTAmt,Eps.ProfTax,Eps.Bonus," +
                "Eps.SalAdvDed,Eps.CanteenAdv,Eps.UniformDed,Eps.Penalty,Eps.OtherDed,Eps.Gross,Eps.Deductions,Eps.ActualAmount, " +
                " Eps.OWF,EmpDetails.EmpFName,EmpDetails.EmpMName," +
                "EmpDetails.UnitId, EmpDetails.EmpBankAppNo,EmpDetails.EmpBankAcNo, EmpDetails.EmpBankCardRef,EmpAttendance.OT from EmpPaySheet as Eps  " +
                " INNER JOIN EmpDetails ON Eps.EmpId = EmpDetails.EmpId INNER JOIN EmpAttendance ON EmpAttendance.EmpId=Eps.EmpId AND  " +
                " EmpAttendance.ClientId=Eps.ClientId AND EmpAttendance.Month=Eps.Month   And  Eps.NoOfDuties>10 AND Eps.ClientId='" +
                    //ddlClients.SelectedValue + "' AND Eps.Month=" + month + "  Order by  case when EmpDetails.EmpBankAcNo is null then 1 else 0 end, Eps.EmpId "; ;
                ddlclient.SelectedValue + "' AND Eps.Month=" + month + " and EmpAttendance.Design=Eps.Desgn   Order by Right(Eps.EmpId,6)";

            }
            if (ddlnoofattendance.SelectedIndex == 1)
            {

                selectmonth = "select Eps.EmpId,Eps.Desgn,TempGross as salaryrate, Eps.NoOfDuties,Eps.Basic,Eps.DA,Eps.HRA,Eps.CCA, " +
              " Eps.Conveyance,Eps.WashAllowance,Eps.OtherAllowance,Eps.PF,Eps.ESI,Eps.OTAmt,Eps.ProfTax,Eps.Bonus," +
         "Eps.SalAdvDed,Eps.CanteenAdv,Eps.UniformDed,Eps.Penalty,Eps.OtherDed,Eps.Gross,Eps.Deductions,Eps.ActualAmount, " +
         " Eps.OWF,EmpDetails.EmpFName,EmpDetails.EmpMName," +
         "EmpDetails.UnitId, EmpDetails.EmpBankAppNo,EmpDetails.EmpBankAcNo, EmpDetails.EmpBankCardRef,EmpAttendance.OT from EmpPaySheet as Eps  " +
         " INNER JOIN EmpDetails ON Eps.EmpId = EmpDetails.EmpId INNER JOIN EmpAttendance ON EmpAttendance.EmpId=Eps.EmpId AND  " +
         " EmpAttendance.ClientId=Eps.ClientId AND EmpAttendance.Month=Eps.Month   And  Eps.NoOfDuties<=10 AND Eps.ClientId='" +
                    //ddlClients.SelectedValue + "' AND Eps.Month=" + month + "  Order by  case when EmpDetails.EmpBankAcNo is null then 1 else 0 end, Eps.EmpId "; ;
         ddlclient.SelectedValue + "' AND Eps.Month=" + month + " and EmpAttendance.Design=Eps.Desgn   Order by Right(Eps.EmpId,6)";
            }

            if (ddlnoofattendance.SelectedIndex == 2)
            {

                selectmonth = "select Eps.EmpId,Eps.Desgn,TempGross as salaryrate, Eps.NoOfDuties,Eps.Basic,Eps.DA,Eps.HRA,Eps.CCA, " +
              " Eps.Conveyance,Eps.WashAllowance,Eps.OtherAllowance,Eps.PF,Eps.ESI,Eps.OTAmt,Eps.ProfTax,Eps.Bonus," +
         "Eps.SalAdvDed,Eps.CanteenAdv,Eps.UniformDed,Eps.Penalty,Eps.OtherDed,Eps.Gross,Eps.Deductions,Eps.ActualAmount, " +
         " Eps.OWF,EmpDetails.EmpFName,EmpDetails.EmpMName," +
         "EmpDetails.UnitId, EmpDetails.EmpBankAppNo,EmpDetails.EmpBankAcNo, EmpDetails.EmpBankCardRef,EmpAttendance.OT from EmpPaySheet as Eps  " +
         " INNER JOIN EmpDetails ON Eps.EmpId = EmpDetails.EmpId INNER JOIN EmpAttendance ON EmpAttendance.EmpId=Eps.EmpId AND  " +
         " EmpAttendance.ClientId=Eps.ClientId AND EmpAttendance.Month=Eps.Month   AND Eps.ClientId='" +
                    //ddlClients.SelectedValue + "' AND Eps.Month=" + month + "  Order by  case when EmpDetails.EmpBankAcNo is null then 1 else 0 end, Eps.EmpId "; ;
         ddlclient.SelectedValue + "' AND Eps.Month=" + month + " and EmpAttendance.Design=Eps.Desgn   Order by Right(Eps.EmpId,6)";
            }

            DataTable dt = config.ExecuteAdaptorAsyncWithQueryParams(selectmonth).Result;

            MemoryStream ms = new MemoryStream();
            if (dt.Rows.Count > 0)
            {
                Document document = new Document(PageSize.LEGAL.Rotate());
                PdfWriter writer = PdfWriter.GetInstance(document, ms);
                document.Open();
                document.AddTitle("FaMS");
                document.AddAuthor("DIYOS");
                document.AddSubject("Wage Sheet");
                document.AddKeywords("Keyword1, keyword2, …");//
                float forConvert;
                string strQry = "Select * from CompanyInfo";
                DataTable compInfo = config.ExecuteAdaptorAsyncWithQueryParams(strQry).Result;
                string companyName1 = "Your Company Name";
                string companyAddress = "Your Company Address";
                if (compInfo.Rows.Count > 0)
                {
                    companyName1 = compInfo.Rows[0]["CompanyName"].ToString();
                    companyAddress = compInfo.Rows[0]["Address"].ToString();
                }

                int tableCells = 25;
                #region variables for total
                float totalActualamount = 0;
                float totalDuties = 0;
                float totalOts = 0;
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
                float totalDed = 0;
                float totalSalAdv = 0;
                float totalUniforDed = 0;
                float totalCanteenAdv = 0;
                float totalOwf = 0;
                float totalPenalty = 0;
                float totalbonus = 0;

                #endregion

                #region variables for  Grand  total
                float GrandtotalActualamount = 0;
                float GrandtotalDuties = 0;
                float GrandtotalOts = 0;
                float GrandtotalBasic = 0;
                float GrandtotalDA = 0;
                float GrandtotalHRA = 0;
                float GrandtotalCCA = 0;
                float GrandtotalConveyance = 0;
                float GrandtotalWA = 0;
                float GrandtotalOA = 0;
                float GrandtotalGrass = 0;
                float GrandtotalOTAmount = 0;
                float GrandtotalPF = 0;
                float GrandtotalESI = 0;
                float GrandtotalProfTax = 0;
                float GrandtotalDed = 0;
                float GrandtotalSalAdv = 0;
                float GrandtotalUniforDed = 0;
                float GrandtotalCanteenAdv = 0;
                float GrandtotalOwf = 0;
                float GrandtotalPenalty = 0;
                float Grandtotalbonus = 0;

                #endregion

                int nextpagerecordscount = 0;
                int targetpagerecors = 10;
                int secondpagerecords = targetpagerecors + 3;
                bool nextpagehasPages = false;
                int j = 0;
                PdfPTable SecondtableFooter = null;
                PdfPTable SecondtablecheckedbyFooter = null;

                for (int nextpagei = 0; nextpagei < dt.Rows.Count; nextpagei++)
                {
                    nextpagehasPages = true;
                    #region Titles of Document
                    PdfPTable Maintable = new PdfPTable(tableCells);
                    Maintable.TotalWidth = 950f;
                    Maintable.LockedWidth = true;
                    float[] width = new float[] { 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f };
                    Maintable.SetWidths(width);
                    uint FONT_SIZE = 8;

                    //Company Name & vage act details

                    PdfPCell cellemp = new PdfPCell(new Phrase("  ", FontFactory.GetFont(Fontstyle, 10, Font.BOLD, BaseColor.BLACK)));
                    cellemp.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    cellemp.Colspan = tableCells;
                    cellemp.Border = 0;

                    PdfPCell Heading = new PdfPCell(new Phrase("Form - XVII REGISTER OF WAGES", FontFactory.GetFont(Fontstyle, 20, Font.BOLD, BaseColor.BLACK)));
                    Heading.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    Heading.Colspan = tableCells;
                    Heading.Border = 0;
                    Maintable.AddCell(Heading);

                    PdfPCell actDetails = new PdfPCell(new Phrase("(vide Rule 78(1) a(i) of Contract Labour (Reg. & abolition) Central & A.P. Rules)", FontFactory.GetFont(Fontstyle, 15, Font.BOLD, BaseColor.BLACK)));
                    actDetails.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    actDetails.Colspan = tableCells;
                    actDetails.Border = 0;// 15;
                    Maintable.AddCell(actDetails);

                    Maintable.AddCell(cellemp);
                    #endregion

                    #region Table Headings
                    PdfPCell companyName = new PdfPCell(new Phrase(companyName1, FontFactory.GetFont("Arial Black", 20, Font.BOLD, BaseColor.BLACK)));
                    companyName.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    companyName.Colspan = tableCells;
                    companyName.Border = 0;// 15;
                    Maintable.AddCell(companyName);

                    PdfPCell paySheet = new PdfPCell(new Phrase("Pay Sheet", FontFactory.GetFont(Fontstyle, 10, Font.BOLD | Font.UNDERLINE, BaseColor.BLACK)));
                    paySheet.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    paySheet.Colspan = tableCells;
                    paySheet.Border = 0;// 15;
                    Maintable.AddCell(paySheet);

                    PdfPCell CClient = new PdfPCell(new Phrase("Client ID : " + ddlclient.SelectedValue, FontFactory.GetFont(Fontstyle, 10, Font.BOLD, BaseColor.BLACK)));
                    CClient.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    CClient.Colspan = 3;
                    CClient.Border = 0;// 15;
                    Maintable.AddCell(CClient);

                    PdfPCell CClientName = new PdfPCell(new Phrase("Client Name : " + ddlcname.SelectedItem, FontFactory.GetFont(Fontstyle, 10, Font.BOLD, BaseColor.BLACK)));
                    CClientName.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    CClientName.Colspan = 7;
                    CClientName.Border = 0;// 15;
                    Maintable.AddCell(CClientName);

                    PdfPCell CDates = new PdfPCell(new Phrase("Payment for the period of : " + GetPaymentPeriod(ddlclient.SelectedValue), FontFactory.GetFont(Fontstyle, 10, Font.BOLD, BaseColor.BLACK)));
                    CDates.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    CDates.Colspan = 7;
                    CDates.Border = 0;// 15;
                    Maintable.AddCell(CDates);

                    //PdfPCell CPaySheetDate = new PdfPCell(new Phrase("Pay Sheet Date :  " + DateTime.Now.Date.ToString("dd/MM/yyyy"), FontFactory.GetFont(Fontstyle, 10, Font.BOLD, BaseColor.BLACK)));

                    PdfPCell CPaySheetDate = new PdfPCell(new Phrase(" ", FontFactory.GetFont(Fontstyle, 10, Font.BOLD, BaseColor.BLACK)));
                    CPaySheetDate.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    CPaySheetDate.Colspan = 4;
                    CPaySheetDate.Border = 0;// 15;
                    Maintable.AddCell(CPaySheetDate);

                    PdfPCell CPayMonth = new PdfPCell(new Phrase("For the month of :   " + DateTime.Parse(txtmonth.Text, CultureInfo.GetCultureInfo("en-gb")).ToString("MMMM"), FontFactory.GetFont(Fontstyle, 10, Font.BOLD, BaseColor.BLACK)));
                    CPayMonth.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    CPayMonth.Colspan = 6;
                    CPayMonth.Border = 0;// 15;
                    Maintable.AddCell(CPayMonth);
                    Maintable.AddCell(cellemp);

                    if (titleofdocumentindex == 0)
                    {
                        document.Add(Maintable);
                        titleofdocumentindex = 1;
                    }
                    PdfPTable SecondtableHeadings = new PdfPTable(tableCells);
                    SecondtableHeadings.TotalWidth = 950f;
                    SecondtableHeadings.LockedWidth = true;
                    float[] SecondHeadingsWidth = new float[] { 1.5f, 2f, 6f, 3f, 1.8f, 2f, 1.8f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 2f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 2f, 2f, 6f };
                    SecondtableHeadings.SetWidths(SecondHeadingsWidth);

                    //Cell Headings
                    //1
                    PdfPCell sNo = new PdfPCell(new Phrase("S . No.", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    sNo.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                    //sNo.Colspan = 1;
                    sNo.Border = 15;// 15;
                    SecondtableHeadings.AddCell(sNo);
                    //2
                    PdfPCell CEmpId = new PdfPCell(new Phrase("Emp Id", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    CEmpId.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    CEmpId.Border = 15;// 15;
                    SecondtableHeadings.AddCell(CEmpId);
                    //3
                    PdfPCell CEmpName = new PdfPCell(new Phrase("Emp Name", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    CEmpName.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    CEmpName.Border = 15;// 15;
                    SecondtableHeadings.AddCell(CEmpName);
                    //4
                    PdfPCell CDesgn = new PdfPCell(new Phrase("Desgn", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    CDesgn.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    CDesgn.Border = 15;
                    SecondtableHeadings.AddCell(CDesgn);
                    //5
                    PdfPCell CDuties = new PdfPCell(new Phrase("Dts", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    CDuties.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    CDuties.Border = 15;
                    SecondtableHeadings.AddCell(CDuties);
                    //6
                    PdfPCell COTs = new PdfPCell(new Phrase("Ots", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    COTs.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    COTs.Border = 15;
                    // Secondtable.AddCell(COTs);
                    //7
                    PdfPCell CBasic = new PdfPCell(new Phrase("Basic", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    CBasic.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    CBasic.Border = 15;
                    SecondtableHeadings.AddCell(CBasic);
                    //8
                    PdfPCell CDa = new PdfPCell(new Phrase("DA", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    CBasic.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    CBasic.Border = 15;
                    SecondtableHeadings.AddCell(CDa);

                    //9

                    PdfPCell CHRa = new PdfPCell(new Phrase("HRA", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    CHRa.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    CHRa.Border = 15;
                    SecondtableHeadings.AddCell(CHRa);

                    //10
                    PdfPCell Cconveyance = new PdfPCell(new Phrase("Conv", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    Cconveyance.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    Cconveyance.Border = 15;
                    SecondtableHeadings.AddCell(Cconveyance);
                    //11
                    PdfPCell CCca = new PdfPCell(new Phrase("CCA", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    CCca.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    CCca.Border = 15;
                    SecondtableHeadings.AddCell(CCca);
                    //12
                    PdfPCell Cwa = new PdfPCell(new Phrase("WA", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    Cwa.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    Cwa.Border = 15;
                    SecondtableHeadings.AddCell(Cwa);
                    //13
                    PdfPCell COa = new PdfPCell(new Phrase("OA", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    COa.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    COa.Border = 15;
                    SecondtableHeadings.AddCell(COa);


                    //14
                    PdfPCell Cbonus = new PdfPCell(new Phrase("Bonus", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    Cbonus.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    Cbonus.Border = 15;
                    SecondtableHeadings.AddCell(Cbonus);


                    //15
                    PdfPCell Cotamt = new PdfPCell(new Phrase("OTamt", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    Cotamt.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    Cotamt.Border = 15;
                    // Secondtable.AddCell(Cotamt);
                    //16

                    PdfPCell CGross = new PdfPCell(new Phrase("Gross", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    CGross.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    CGross.Border = 15;
                    SecondtableHeadings.AddCell(CGross);
                    //17
                    PdfPCell CPF = new PdfPCell(new Phrase("PF", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    CPF.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    CPF.Border = 15;
                    SecondtableHeadings.AddCell(CPF);
                    //18
                    PdfPCell CESI = new PdfPCell(new Phrase("ESI", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    CESI.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    CESI.Border = 15;
                    SecondtableHeadings.AddCell(CESI);
                    //19
                    PdfPCell CPT = new PdfPCell(new Phrase("PT", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    CPT.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    CPT.Border = 15;
                    SecondtableHeadings.AddCell(CPT);
                    //20
                    PdfPCell CSalAdv = new PdfPCell(new Phrase("Sal Adv", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    CSalAdv.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    CSalAdv.Border = 15;
                    SecondtableHeadings.AddCell(CSalAdv);
                    //21
                    PdfPCell CUnifDed = new PdfPCell(new Phrase("Unif. Ded", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    CUnifDed.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    CUnifDed.Border = 15;
                    SecondtableHeadings.AddCell(CUnifDed);

                    //22
                    PdfPCell Ccanteended = new PdfPCell(new Phrase("Mess. Ded", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    Ccanteended.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    Ccanteended.Border = 15;
                    SecondtableHeadings.AddCell(Ccanteended);

                    //3
                    PdfPCell COWF = new PdfPCell(new Phrase("OWF", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                    COWF.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    COWF.Border = 15;
                    SecondtableHeadings.AddCell(COWF);
                    //24
                    PdfPCell COtherDed = new PdfPCell(new Phrase("Other Ded", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                    COtherDed.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    COtherDed.Border = 15;
                    SecondtableHeadings.AddCell(COtherDed);
                    //25
                    PdfPCell CTotDed = new PdfPCell(new Phrase("Tot Ded", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                    CTotDed.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    CTotDed.Border = 15;
                    SecondtableHeadings.AddCell(CTotDed);
                    //26
                    PdfPCell CNetPay = new PdfPCell(new Phrase("Net Pay", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    CNetPay.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    CNetPay.Border = 15;
                    SecondtableHeadings.AddCell(CNetPay);
                    //27
                    PdfPCell CSignature = new PdfPCell(new Phrase("Bank A/c No./ Signature", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                    CSignature.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    CSignature.Border = 15;
                    SecondtableHeadings.AddCell(CSignature);




                    #endregion

                    PdfPTable Secondtable = new PdfPTable(tableCells);
                    Secondtable.TotalWidth = 950f;
                    Secondtable.LockedWidth = true;
                    float[] SecondWidth = new float[] { 1.5f, 2f, 6f, 3f, 1.8f, 2f, 1.8f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 2f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 2f, 2f, 6f };
                    Secondtable.SetWidths(SecondWidth);

                    #region Table Data
                    int rowCount = 0;
                    //int pageCount = 0;
                    int slipsCount = 0;
                    int i = nextpagei;

                    //if (int i=nextpagei)
                    {
                        forConvert = 0;
                        if (dt.Rows[i]["NoOfDuties"].ToString().Trim().Length > 0)
                            forConvert = Convert.ToSingle(dt.Rows[i]["NoOfDuties"].ToString());

                        if (forConvert > 0)
                        {
                            if (nextpagerecordscount == 0)
                            {
                                document.Add(SecondtableHeadings);
                            }

                            nextpagerecordscount++;
                            //1
                            PdfPCell CSNo = new PdfPCell(new Phrase((++j).ToString() + " \n \n \n ", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                            CSNo.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            CSNo.Border = 15;
                            Secondtable.AddCell(CSNo);
                            //2
                            PdfPCell CEmpId1 = new PdfPCell(new Phrase(dt.Rows[i]["EmpId"].ToString(), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                            CEmpId1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            CEmpId1.Border = 15;
                            Secondtable.AddCell(CEmpId1);
                            //3
                            PdfPCell CEmpName1 = new PdfPCell(new Phrase(dt.Rows[i]["EmpFName"].ToString() + " " + dt.Rows[i]["EmpMName"].ToString(), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                            CEmpName1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            CEmpName1.Border = 15;
                            Secondtable.AddCell(CEmpName1);
                            //4
                            PdfPCell CEmpDesgn = new PdfPCell(new Phrase(dt.Rows[i]["Desgn"].ToString(), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                            CEmpDesgn.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            CEmpDesgn.Border = 15;
                            Secondtable.AddCell(CEmpDesgn);
                            //5
                            forConvert = 0;
                            if (dt.Rows[i]["NoOfDuties"].ToString().Trim().Length > 0)
                                forConvert = Convert.ToSingle(dt.Rows[i]["NoOfDuties"].ToString());
                            totalDuties += forConvert;
                            GrandtotalDuties += forConvert;
                            PdfPCell CNoOfDuties = new PdfPCell(new Phrase(forConvert.ToString("0.0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                            CNoOfDuties.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            CNoOfDuties.Border = 15;
                            Secondtable.AddCell(CNoOfDuties);
                            //6
                            if (dt.Rows[i]["ot"].ToString().Trim().Length > 0)
                                forConvert = Convert.ToSingle(dt.Rows[i]["ot"].ToString());
                            totalOts += forConvert;
                            GrandtotalOts += forConvert;
                            PdfPCell CNoOfots = new PdfPCell(new Phrase(forConvert.ToString("0.0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                            CNoOfots.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            CNoOfots.Border = 15;
                            //Secondtable.AddCell(CNoOfots);

                            //7
                            forConvert = 0;
                            if (dt.Rows[i]["Basic"].ToString().Trim().Length > 0)
                                forConvert = (float)Math.Round(Convert.ToSingle(dt.Rows[i]["Basic"].ToString()));
                            totalBasic += forConvert;
                            GrandtotalBasic += forConvert;
                            PdfPCell CBasic1 = new PdfPCell(new Phrase(forConvert.ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                            CBasic1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            CBasic1.Border = 15;
                            Secondtable.AddCell(CBasic1);
                            //8

                            forConvert = 0;

                            if (dt.Rows[i]["DA"].ToString().Trim().Length > 0)
                                forConvert = (float)Math.Round(Convert.ToSingle(dt.Rows[i]["DA"].ToString()));
                            totalDA += forConvert;
                            GrandtotalDA += forConvert;
                            PdfPCell CDa1 = new PdfPCell(new Phrase(forConvert.ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                            CDa1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            CDa1.Border = 15;
                            Secondtable.AddCell(CDa1);

                            //9

                            forConvert = 0;
                            if (dt.Rows[i]["HRA"].ToString().Trim().Length > 0)
                                forConvert = (float)Math.Round(Convert.ToSingle(dt.Rows[i]["HRA"].ToString()));
                            totalHRA += forConvert;
                            GrandtotalHRA += forConvert;
                            PdfPCell CHRA1 = new PdfPCell(new Phrase(forConvert.ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                            CHRA1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            CHRA1.Border = 15;
                            Secondtable.AddCell(CHRA1);

                            //10
                            forConvert = 0;
                            if (dt.Rows[i]["Conveyance"].ToString().Trim().Length > 0)
                                forConvert = (float)Math.Round(Convert.ToSingle(dt.Rows[i]["Conveyance"].ToString()));
                            totalConveyance += forConvert;
                            GrandtotalConveyance += forConvert;
                            PdfPCell CConveyance = new PdfPCell(new Phrase(forConvert.ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                            CConveyance.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            CConveyance.Border = 15;
                            Secondtable.AddCell(CConveyance);

                            //11
                            forConvert = 0;
                            if (dt.Rows[i]["cca"].ToString().Trim().Length > 0)
                                forConvert = (float)Math.Round(Convert.ToSingle(dt.Rows[i]["cca"].ToString()));
                            totalCCA += forConvert;
                            GrandtotalCCA += forConvert;
                            PdfPCell Ccca = new PdfPCell(new Phrase(forConvert.ToString(), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                            Ccca.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            Ccca.Border = 15;
                            Secondtable.AddCell(Ccca);
                            //12
                            forConvert = 0;
                            if (dt.Rows[i]["washallowance"].ToString().Trim().Length > 0)
                                forConvert = (float)Math.Round(Convert.ToSingle(dt.Rows[i]["washallowance"].ToString()));
                            totalWA += forConvert;
                            GrandtotalWA += forConvert;
                            PdfPCell CWa = new PdfPCell(new Phrase(forConvert.ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                            CWa.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            CWa.Border = 15;
                            Secondtable.AddCell(CWa);
                            //13
                            forConvert = 0;
                            if (dt.Rows[i]["OtherAllowance"].ToString().Trim().Length > 0)
                                forConvert = (float)Math.Round(Convert.ToSingle(dt.Rows[i]["OtherAllowance"].ToString()));
                            totalOA += forConvert;
                            GrandtotalOA += forConvert;
                            PdfPCell COA = new PdfPCell(new Phrase(forConvert.ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                            COA.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            COA.Border = 15;
                            Secondtable.AddCell(COA);


                            //14
                            forConvert = 0;
                            if (dt.Rows[i]["Bonus"].ToString().Trim().Length > 0)
                                forConvert = (float)Math.Round(Convert.ToSingle(dt.Rows[i]["Bonus"].ToString()));
                            totalOA += forConvert;
                            GrandtotalOA += forConvert;
                            PdfPCell CBonus = new PdfPCell(new Phrase(forConvert.ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                            CBonus.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            CBonus.Border = 15;
                            Secondtable.AddCell(CBonus);

                            //15

                            forConvert = 0;
                            if (dt.Rows[i]["otamt"].ToString().Trim().Length > 0)
                                forConvert = (float)Math.Round(Convert.ToSingle(dt.Rows[i]["otamt"].ToString()));
                            totalOTAmount += forConvert;

                            //16
                            forConvert = 0;
                            if (dt.Rows[i]["Gross"].ToString().Trim().Length > 0)
                                forConvert = (float)Math.Round(Convert.ToSingle(dt.Rows[i]["Gross"].ToString()));
                            totalGrass += forConvert;
                            GrandtotalGrass += forConvert;
                            PdfPCell CGross1 = new PdfPCell(new Phrase(forConvert.ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                            CGross1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            CGross1.Border = 15;
                            Secondtable.AddCell(CGross1);
                            //17
                            forConvert = 0;
                            if (dt.Rows[i]["PF"].ToString().Trim().Length > 0)
                                forConvert = (float)Math.Round(Convert.ToSingle(dt.Rows[i]["PF"].ToString()));
                            totalPF += forConvert;
                            GrandtotalPF += forConvert;
                            PdfPCell CPF1 = new PdfPCell(new Phrase(forConvert.ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                            CPF1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            CPF1.Border = 15;
                            Secondtable.AddCell(CPF1);
                            //18
                            forConvert = 0;
                            if (dt.Rows[i]["ESI"].ToString().Trim().Length > 0)
                                forConvert = (float)Math.Round(Convert.ToSingle(dt.Rows[i]["ESI"].ToString()));
                            totalESI += forConvert;
                            GrandtotalESI += forConvert;
                            PdfPCell CESI1 = new PdfPCell(new Phrase(forConvert.ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                            CESI1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            CESI1.Border = 15;
                            Secondtable.AddCell(CESI1);
                            //19
                            forConvert = 0;
                            if (dt.Rows[i]["ProfTax"].ToString().Trim().Length > 0)
                                forConvert = (float)Math.Round(Convert.ToSingle(dt.Rows[i]["ProfTax"].ToString()));
                            totalProfTax += forConvert;
                            GrandtotalProfTax += forConvert;
                            PdfPCell CProTax1 = new PdfPCell(new Phrase(forConvert.ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                            CProTax1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            CProTax1.Border = 15;
                            Secondtable.AddCell(CProTax1);
                            //20
                            forConvert = 0;
                            if (dt.Rows[i]["SalAdvDed"].ToString().Trim().Length > 0)
                                forConvert = (float)Math.Round(Convert.ToSingle(dt.Rows[i]["SalAdvDed"].ToString()));
                            totalSalAdv += forConvert;
                            GrandtotalSalAdv += forConvert;
                            PdfPCell CSalAdv1 = new PdfPCell(new Phrase(forConvert.ToString(), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                            CSalAdv1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            CSalAdv1.Border = 15;
                            Secondtable.AddCell(CSalAdv1);
                            //21
                            forConvert = 0;
                            if (dt.Rows[i]["UniformDed"].ToString().Trim().Length > 0)
                                forConvert = (float)Math.Round(Convert.ToSingle(dt.Rows[i]["UniformDed"].ToString()));
                            totalUniforDed += forConvert;
                            GrandtotalUniforDed += forConvert;
                            PdfPCell CUnifDed1 = new PdfPCell(new Phrase(forConvert.ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                            CUnifDed1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            CUnifDed1.Border = 15;
                            Secondtable.AddCell(CUnifDed1);
                            //22

                            forConvert = 0;
                            if (dt.Rows[i]["CanteenAdv"].ToString().Trim().Length > 0)
                                forConvert = (float)Math.Round(Convert.ToSingle(dt.Rows[i]["CanteenAdv"].ToString()));
                            totalCanteenAdv += forConvert;
                            GrandtotalCanteenAdv += forConvert;

                            PdfPCell CCanteended = new PdfPCell(new Phrase(forConvert.ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                            CCanteended.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            CCanteended.Border = 15;
                            Secondtable.AddCell(CCanteended);

                            //23
                            if (dt.Rows[i]["OWF"].ToString().Trim().Length > 0)
                                forConvert = (float)Math.Round(Convert.ToSingle(dt.Rows[i]["OWF"].ToString()));
                            totalOwf += forConvert;
                            GrandtotalOwf += forConvert;
                            PdfPCell COwf1 = new PdfPCell(new Phrase(forConvert.ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                            COwf1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            COwf1.Border = 15;
                            Secondtable.AddCell(COwf1);
                            //24
                            forConvert = 0;


                            if (dt.Rows[i]["Penalty"].ToString().Trim().Length > 0)
                                forConvert = (float)Math.Round(Convert.ToSingle(dt.Rows[i]["Penalty"].ToString()));
                            totalPenalty += forConvert;
                            GrandtotalPenalty += forConvert;
                            PdfPCell COtherDed1 = new PdfPCell(new Phrase(forConvert.ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                            COtherDed1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            COtherDed1.Border = 15;
                            Secondtable.AddCell(COtherDed1);
                            //25
                            forConvert = 0;
                            float totaldeductions = 0;
                            if (dt.Rows[i]["Deductions"].ToString().Trim().Length > 0)
                            {
                                forConvert = (float)Math.Round(Convert.ToSingle(dt.Rows[i]["Deductions"].ToString()));
                                totaldeductions = forConvert;
                            }
                            totalDed += forConvert;
                            GrandtotalDed += forConvert;
                            PdfPCell CTotDed1 = new PdfPCell(new Phrase(forConvert.ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                            CTotDed1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            CTotDed1.Border = 15;
                            Secondtable.AddCell(CTotDed1);//OtherDed,Eps.Gross,Eps.Deductions,Eps.ActualAmount
                            //26
                            forConvert = 0;

                            if (dt.Rows[i]["gross"].ToString().Trim().Length > 0)
                                forConvert = (float)Math.Round(Convert.ToSingle(dt.Rows[i]["gross"].ToString()));


                            if (forConvert < 0)
                            {
                                forConvert = 0;
                            }

                            forConvert = forConvert - totaldeductions;
                            totalActualamount += forConvert;
                            GrandtotalActualamount += forConvert;
                            PdfPCell CNetPay1 = new PdfPCell(new Phrase(forConvert.ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                            CNetPay1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            CNetPay1.Border = 15;
                            Secondtable.AddCell(CNetPay1);
                            //27
                            string EmpBankAcNo = dt.Rows[i]["EmpBankAcNo"].ToString();
                            PdfPCell CSignature1 = new PdfPCell(new Phrase(EmpBankAcNo, FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                            CSignature1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            CSignature1.Border = 15;
                            CSignature1.MinimumHeight = 25;
                            Secondtable.AddCell(CSignature1);
                        }

                    }

                    #endregion

                    #region Comment the foote code

                    SecondtableFooter = new PdfPTable(tableCells);
                    SecondtableFooter.TotalWidth = 950f;
                    SecondtableFooter.LockedWidth = true;
                    float[] SecondFooterWidth = new float[] { 1.5f, 2f, 6f, 3f, 1.8f, 2f, 1.8f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 2f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 2f, 2f, 6f };
                    SecondtableFooter.SetWidths(SecondFooterWidth);

                    #region Table Footer
                    //1
                    PdfPCell FCSNo = new PdfPCell(new Phrase("", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                    FCSNo.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCSNo.Border = 15;
                    SecondtableFooter.AddCell(FCSNo);
                    //2
                    PdfPCell FCEmpId1 = new PdfPCell(new Phrase("", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                    FCEmpId1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCEmpId1.Border = 15;
                    SecondtableFooter.AddCell(FCEmpId1);
                    //3
                    PdfPCell FCEmpName1 = new PdfPCell(new Phrase("Total : ", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                    FCEmpName1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCEmpName1.Border = 15;
                    SecondtableFooter.AddCell(FCEmpName1);
                    //4
                    PdfPCell FCEmpDesgn = new PdfPCell(new Phrase("", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                    FCEmpDesgn.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCEmpDesgn.Border = 15;
                    //FCEmpDesgn.Colspan = 4;
                    SecondtableFooter.AddCell(FCEmpDesgn);
                    //5
                    PdfPCell FCNoOfDuties = new PdfPCell(new Phrase(totalDuties.ToString("0.00"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    FCNoOfDuties.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCNoOfDuties.Border = 15;
                    SecondtableFooter.AddCell(FCNoOfDuties);
                    //6
                    PdfPCell FCNoOfots = new PdfPCell(new Phrase(totalOts.ToString("0.00"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    FCNoOfots.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCNoOfots.Border = 15;
                    //Secondtable.AddCell(FCNoOfots);

                    //7
                    PdfPCell FCBasic1 = new PdfPCell(new Phrase(Math.Round(totalBasic).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    FCBasic1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCBasic1.Border = 15;
                    SecondtableFooter.AddCell(FCBasic1);


                    //8
                    PdfPCell FDa = new PdfPCell(new Phrase(Math.Round(totalDA).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    FDa.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FDa.Border = 15;
                    SecondtableFooter.AddCell(FDa);


                    //9

                    forConvert = 0;
                    PdfPCell FCHRA1 = new PdfPCell(new Phrase(Math.Round(totalHRA).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    FCHRA1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCHRA1.Border = 15;
                    SecondtableFooter.AddCell(FCHRA1);

                    //10
                    PdfPCell FCConveyance = new PdfPCell(new Phrase(Math.Round(totalConveyance).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    FCConveyance.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCConveyance.Border = 15;
                    SecondtableFooter.AddCell(FCConveyance);

                    //11
                    PdfPCell FCcca = new PdfPCell(new Phrase(Math.Round(totalCCA).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    FCcca.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCcca.Border = 15;
                    SecondtableFooter.AddCell(FCcca);
                    //12
                    PdfPCell FCWa = new PdfPCell(new Phrase(Math.Round(totalWA).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    FCWa.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCWa.Border = 15;
                    SecondtableFooter.AddCell(FCWa);

                    //13
                    PdfPCell FCOa = new PdfPCell(new Phrase(Math.Round(totalOA).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    FCOa.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCOa.Border = 15;
                    SecondtableFooter.AddCell(FCOa);

                    //14
                    PdfPCell Fbonus = new PdfPCell(new Phrase(Math.Round(totalOA).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    Fbonus.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    Fbonus.Border = 15;
                    SecondtableFooter.AddCell(Fbonus);

                    //15
                    PdfPCell FCottamt = new PdfPCell(new Phrase(Math.Round(totalOTAmount).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    FCottamt.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCottamt.Border = 15;
                    // Secondtable.AddCell(FCottamt);
                    //16
                    PdfPCell FCGross1 = new PdfPCell(new Phrase(Math.Round(totalGrass).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    FCGross1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCGross1.Border = 15;
                    SecondtableFooter.AddCell(FCGross1);
                    //17
                    PdfPCell FCPF1 = new PdfPCell(new Phrase(Math.Round(totalPF).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    FCPF1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCPF1.Border = 15;
                    SecondtableFooter.AddCell(FCPF1);
                    //18
                    PdfPCell FCESI1 = new PdfPCell(new Phrase(Math.Round(totalESI).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    FCESI1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCESI1.Border = 15;
                    SecondtableFooter.AddCell(FCESI1);
                    //19
                    PdfPCell FCProTax1 = new PdfPCell(new Phrase(Math.Round(totalProfTax).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    FCProTax1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCProTax1.Border = 15;
                    SecondtableFooter.AddCell(FCProTax1);
                    //20
                    PdfPCell FCSalAdv1 = new PdfPCell(new Phrase(Math.Round(totalSalAdv).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    FCSalAdv1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCSalAdv1.Border = 15;
                    SecondtableFooter.AddCell(FCSalAdv1);
                    //21
                    PdfPCell FCUnifDed1 = new PdfPCell(new Phrase(Math.Round(totalUniforDed).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    FCUnifDed1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCUnifDed1.Border = 15;
                    SecondtableFooter.AddCell(FCUnifDed1);
                    //22
                    PdfPCell FCCanteended = new PdfPCell(new Phrase(Math.Round(totalCanteenAdv).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    FCCanteended.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCCanteended.Border = 15;
                    SecondtableFooter.AddCell(FCCanteended);
                    //23
                    PdfPCell FCOwf1 = new PdfPCell(new Phrase(Math.Round(totalOwf).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    FCOwf1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCOwf1.Border = 15;
                    SecondtableFooter.AddCell(FCOwf1);
                    //24
                    PdfPCell FCOtherDed1 = new PdfPCell(new Phrase(Math.Round(totalPenalty).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    FCOtherDed1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCOtherDed1.Border = 15;
                    SecondtableFooter.AddCell(FCOtherDed1);
                    //25
                    PdfPCell FCTotDed1 = new PdfPCell(new Phrase(Math.Round(totalDed).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    FCTotDed1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCTotDed1.Border = 15;
                    SecondtableFooter.AddCell(FCTotDed1);//OtherDed,Eps.Gross,Eps.Deductions,Eps.ActualAmount
                    //26
                    PdfPCell FCNetPay1 = new PdfPCell(new Phrase(Math.Round(totalActualamount).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    FCNetPay1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCNetPay1.Border = 15;
                    //FCNetPay1.Colspan = 2;
                    SecondtableFooter.AddCell(FCNetPay1);
                    //27
                    PdfPCell FCSignature1 = new PdfPCell(new Phrase(" ", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                    FCSignature1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCSignature1.Border = 15;
                    //FCSignature1.MinimumHeight = 25;

                    SecondtableFooter.AddCell(FCSignature1);
                    #endregion



                    SecondtablecheckedbyFooter = new PdfPTable(tableCells);
                    SecondtablecheckedbyFooter.TotalWidth = 950f;
                    SecondtablecheckedbyFooter.LockedWidth = true;
                    float[] SecondcheckedFooterWidth = new float[] { 1.5f, 2f, 6f, 3f, 1.8f, 2f, 1.8f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 2f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 2f, 2f, 6f };
                    SecondtablecheckedbyFooter.SetWidths(SecondcheckedFooterWidth);



                    #region Table  Grand  Total   Footer
                    //1
                    PdfPCell GFCSNo = new PdfPCell(new Phrase("", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                    GFCSNo.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCSNo.Border = 15;
                    SecondtablecheckedbyFooter.AddCell(GFCSNo);
                    //2
                    PdfPCell GFCEmpId1 = new PdfPCell(new Phrase("", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                    GFCEmpId1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCEmpId1.Border = 15;
                    SecondtablecheckedbyFooter.AddCell(GFCEmpId1);
                    //3
                    PdfPCell GFCEmpName1 = new PdfPCell(new Phrase(" Grand  Total: ", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                    GFCEmpName1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCEmpName1.Border = 15;
                    SecondtablecheckedbyFooter.AddCell(GFCEmpName1);
                    //4
                    PdfPCell GFCEmpDesgn = new PdfPCell(new Phrase("", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                    GFCEmpDesgn.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCEmpDesgn.Border = 15;
                    //FCEmpDesgn.Colspan = 4;
                    SecondtablecheckedbyFooter.AddCell(GFCEmpDesgn);
                    //5
                    PdfPCell GFCNoOfDuties = new PdfPCell(new Phrase(GrandtotalDuties.ToString("0.00"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    GFCNoOfDuties.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCNoOfDuties.Border = 15;
                    SecondtablecheckedbyFooter.AddCell(GFCNoOfDuties);
                    //6
                    PdfPCell GFCNoOfots = new PdfPCell(new Phrase(GrandtotalOts.ToString("0.00"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    GFCNoOfots.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCNoOfots.Border = 15;
                    //Secondtable.AddCell(GFCNoOfots);

                    //7
                    PdfPCell GFCBasic1 = new PdfPCell(new Phrase(Math.Round(GrandtotalBasic).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    GFCBasic1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCBasic1.Border = 15;
                    SecondtablecheckedbyFooter.AddCell(GFCBasic1);


                    //8
                    PdfPCell GFDa = new PdfPCell(new Phrase(Math.Round(GrandtotalDA).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    GFDa.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFDa.Border = 15;
                    SecondtablecheckedbyFooter.AddCell(GFDa);


                    //9

                    forConvert = 0;
                    PdfPCell GFCHRA1 = new PdfPCell(new Phrase(Math.Round(GrandtotalHRA).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    GFCHRA1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCHRA1.Border = 15;
                    SecondtablecheckedbyFooter.AddCell(GFCHRA1);

                    //10
                    PdfPCell GFCConveyance = new PdfPCell(new Phrase(Math.Round(GrandtotalConveyance).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    GFCConveyance.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCConveyance.Border = 15;
                    SecondtablecheckedbyFooter.AddCell(GFCConveyance);

                    //11
                    PdfPCell GFCcca = new PdfPCell(new Phrase(Math.Round(GrandtotalCCA).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    GFCcca.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCcca.Border = 15;
                    SecondtablecheckedbyFooter.AddCell(GFCcca);
                    //12
                    PdfPCell GFCWa = new PdfPCell(new Phrase(Math.Round(GrandtotalWA).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    GFCWa.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCWa.Border = 15;
                    SecondtablecheckedbyFooter.AddCell(GFCWa);

                    //13
                    PdfPCell GFCOa = new PdfPCell(new Phrase(Math.Round(GrandtotalOA).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    GFCOa.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCOa.Border = 15;
                    SecondtablecheckedbyFooter.AddCell(GFCOa);

                    //14
                    PdfPCell GFbonus = new PdfPCell(new Phrase(Math.Round(GrandtotalOA).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    GFbonus.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFbonus.Border = 15;
                    SecondtablecheckedbyFooter.AddCell(GFbonus);

                    //15
                    PdfPCell GFCottamt = new PdfPCell(new Phrase(Math.Round(GrandtotalOTAmount).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    GFCottamt.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCottamt.Border = 15;
                    // Secondtable.AddCell(GFCottamt);
                    //16
                    PdfPCell GFCGross1 = new PdfPCell(new Phrase(Math.Round(GrandtotalGrass).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    GFCGross1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCGross1.Border = 15;
                    SecondtablecheckedbyFooter.AddCell(GFCGross1);
                    //17
                    PdfPCell GFCPF1 = new PdfPCell(new Phrase(Math.Round(GrandtotalPF).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    GFCPF1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCPF1.Border = 15;
                    SecondtablecheckedbyFooter.AddCell(GFCPF1);
                    //18
                    PdfPCell GFCESI1 = new PdfPCell(new Phrase(Math.Round(GrandtotalESI).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    GFCESI1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCESI1.Border = 15;
                    SecondtablecheckedbyFooter.AddCell(GFCESI1);
                    //19
                    PdfPCell GFCProTax1 = new PdfPCell(new Phrase(Math.Round(GrandtotalProfTax).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    GFCProTax1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCProTax1.Border = 15;
                    SecondtablecheckedbyFooter.AddCell(GFCProTax1);
                    //20
                    PdfPCell GFCSalAdv1 = new PdfPCell(new Phrase(Math.Round(GrandtotalSalAdv).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    GFCSalAdv1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCSalAdv1.Border = 15;
                    SecondtablecheckedbyFooter.AddCell(GFCSalAdv1);
                    //21
                    PdfPCell GFCUnifDed1 = new PdfPCell(new Phrase(Math.Round(GrandtotalUniforDed).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    GFCUnifDed1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCUnifDed1.Border = 15;
                    SecondtablecheckedbyFooter.AddCell(GFCUnifDed1);
                    //22
                    PdfPCell GFCCanteended = new PdfPCell(new Phrase(Math.Round(GrandtotalCanteenAdv).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    GFCCanteended.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCCanteended.Border = 15;
                    SecondtablecheckedbyFooter.AddCell(GFCCanteended);
                    //23
                    PdfPCell GFCOwf1 = new PdfPCell(new Phrase(Math.Round(GrandtotalOwf).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    GFCOwf1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCOwf1.Border = 15;
                    SecondtablecheckedbyFooter.AddCell(GFCOwf1);
                    //24
                    PdfPCell GFCOtherDed1 = new PdfPCell(new Phrase(Math.Round(GrandtotalPenalty).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    GFCOtherDed1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCOtherDed1.Border = 15;
                    SecondtablecheckedbyFooter.AddCell(GFCOtherDed1);
                    //25
                    PdfPCell GFCTotDed1 = new PdfPCell(new Phrase(Math.Round(GrandtotalDed).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    GFCTotDed1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCTotDed1.Border = 15;
                    SecondtablecheckedbyFooter.AddCell(GFCTotDed1);//OtherDed,Eps.Gross,Eps.Deductions,Eps.ActualAmount
                    //26
                    PdfPCell GFCNetPay1 = new PdfPCell(new Phrase(Math.Round(GrandtotalActualamount).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    GFCNetPay1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCNetPay1.Border = 15;
                    //FCNetPay1.Colspan = 2;
                    SecondtablecheckedbyFooter.AddCell(GFCNetPay1);
                    //27
                    PdfPCell GFCSignature1 = new PdfPCell(new Phrase(" ", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                    GFCSignature1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCSignature1.Border = 15;
                    //FCSignature1.MinimumHeight = 25;

                    SecondtablecheckedbyFooter.AddCell(GFCSignature1);
                    #endregion




                    #region   Footer Headings


                    //1
                    PdfPCell FHCheckedbybr1 = new PdfPCell(new Phrase("", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                    FHCheckedbybr1.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    FHCheckedbybr1.Border = 0;
                    FHCheckedbybr1.Rowspan = 0;
                    FHCheckedbybr1.Colspan = 25;
                    SecondtablecheckedbyFooter.AddCell(FHCheckedbybr1);
                    //2
                    PdfPCell FHApprovedbr2 = new PdfPCell(new Phrase("  ", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                    FHApprovedbr2.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    FHApprovedbr2.Border = 0;
                    FHApprovedbr2.Colspan = 25;
                    SecondtablecheckedbyFooter.AddCell(FHApprovedbr2);


                    SecondtablecheckedbyFooter.AddCell(FHCheckedbybr1);
                    SecondtablecheckedbyFooter.AddCell(FHApprovedbr2);

                    //1
                    PdfPCell FHCheckedby = new PdfPCell(new Phrase("Checked By  ", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                    FHCheckedby.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    FHCheckedby.Border = 0;
                    FHCheckedby.Rowspan = 0;
                    FHCheckedby.Colspan = 13;
                    SecondtablecheckedbyFooter.AddCell(FHCheckedby);
                    //2
                    PdfPCell FHApprovedBy = new PdfPCell(new Phrase("Gross  Approved By   ", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                    FHApprovedBy.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    FHApprovedBy.Border = 0;
                    FHApprovedBy.Colspan = 12;
                    SecondtablecheckedbyFooter.AddCell(FHApprovedBy);

                    #endregion

                    #endregion
                    document.Add(Secondtable);
                    //#region    Pdf New page and  all the totals are zero
                    if (nextpagerecordscount == targetpagerecors)
                    {
                        targetpagerecors = secondpagerecords;
                        document.Add(SecondtableFooter);
                        document.NewPage();
                        nextpagerecordscount = 0;
                        #region  Zero variables

                        totalActualamount = 0;
                        totalDuties = 0;
                        totalOts = 0;
                        totalBasic = 0;
                        totalDA = 0;
                        totalHRA = 0;
                        totalCCA = 0;
                        totalConveyance = 0;
                        totalWA = 0;
                        totalOA = 0;
                        totalGrass = 0;
                        totalOTAmount = 0;
                        totalPF = 0;
                        totalESI = 0;
                        totalProfTax = 0;
                        totalDed = 0;
                        totalSalAdv = 0;
                        totalUniforDed = 0;
                        totalCanteenAdv = 0;
                        totalOwf = 0;
                        totalPenalty = 0;
                        totalbonus = 0;

                        #endregion
                    }
                }

                if (nextpagerecordscount >= 0)
                {
                    document.Add(SecondtableFooter);
                    document.Add(SecondtablecheckedbyFooter);

                }

                //#endregion  
                if (nextpagehasPages)
                {
                    document.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=Only  Duties2.pdf");
                    Response.Buffer = true;
                    Response.Clear();
                    Response.OutputStream.Write(ms.GetBuffer(), 0, ms.GetBuffer().Length);
                    Response.OutputStream.Flush();
                    Response.End();
                }
            }
        }

        protected void btnnopfesi_Click(object sender, EventArgs e)
        {

            if (ddlclient.SelectedIndex <= 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showlalert", "alert('Please Select Client ID/Name');", true);
                return;
            }
            string month = Getmonth();
            if (month.Trim().Length == 0)
            {
                return;
            }
            string selectmonth = string.Empty;

            if (ddlnoofattendance.SelectedIndex == 0)
            {
                selectmonth = "select Eps.EmpId,Eps.Desgn,TempGross as salaryrate, Eps.NoOfDuties,Eps.Basic,Eps.DA,Eps.HRA,Eps.CCA,Eps.Conveyance,Eps.Bonus, " +
                    "   Eps.WashAllowance,Eps.OtherAllowance,Eps.PF,Eps.ESI,Eps.OTAmt,Eps.ProfTax,Eps.Deductions," +
                    "Eps.SalAdvDed,Eps.CanteenAdv,Eps.UniformDed,Eps.Penalty,Eps.OtherDed,Eps.Gross,Eps.Deductions,Eps.ActualAmount,Eps.OWF,  " +
                    " EmpDetails.EmpFName,EmpDetails.EmpMName," +
                    "EmpDetails.UnitId, EmpDetails.EmpBankAcNo, EmpDetails.EmpBankCardRef,EmpAttendance.OT from EmpPaySheet as Eps INNER JOIN    " +
                    " EmpDetails ON Eps.EmpId = EmpDetails.EmpId INNER JOIN EmpAttendance ON EmpAttendance.EmpId=Eps.EmpId AND  " +
                    " EmpAttendance.ClientId=Eps.ClientId AND EmpAttendance.Month=Eps.Month  And  Eps.NoOfDuties>10 AND Eps.ClientId='" +
                    // ddlClients.SelectedValue + "' AND Eps.Month=" + month + "  Order by   case when EmpDetails.EmpBankAcNo is null then 1 else 0 end, Eps.EmpId   ";
                    ddlclient.SelectedValue + "'   And (Eps.PF=0  or  Eps.ESI=0) AND Eps.Month=" + month + " and EmpAttendance.Design=Eps.Desgn  Order by Right(Eps.EmpId,6)";

            }
            if (ddlnoofattendance.SelectedIndex == 1)
            {
                selectmonth = "select Eps.EmpId,Eps.Desgn,TempGross as salaryrate, Eps.NoOfDuties,Eps.Basic,Eps.DA,Eps.HRA,Eps.CCA,Eps.Conveyance,Eps.Bonus, " +
                     "   Eps.WashAllowance,Eps.OtherAllowance,Eps.PF,Eps.ESI,Eps.OTAmt,Eps.ProfTax,Eps.Deductions," +
                     "Eps.SalAdvDed,Eps.CanteenAdv,Eps.UniformDed,Eps.Penalty,Eps.OtherDed,Eps.Gross,Eps.Deductions,Eps.ActualAmount,Eps.OWF,  " +
                     " EmpDetails.EmpFName,EmpDetails.EmpMName," +
                     "EmpDetails.UnitId, EmpDetails.EmpBankAcNo, EmpDetails.EmpBankCardRef,EmpAttendance.OT from EmpPaySheet as Eps INNER JOIN    " +
                     " EmpDetails ON Eps.EmpId = EmpDetails.EmpId INNER JOIN EmpAttendance ON EmpAttendance.EmpId=Eps.EmpId AND  " +
                     " EmpAttendance.ClientId=Eps.ClientId AND EmpAttendance.Month=Eps.Month  And  Eps.NoOfDuties<=10 AND Eps.ClientId='" +
                    // ddlClients.SelectedValue + "' AND Eps.Month=" + month + "  Order by   case when EmpDetails.EmpBankAcNo is null then 1 else 0 end, Eps.EmpId   ";
                     ddlclient.SelectedValue + "'   And (Eps.PF=0  or  Eps.ESI=0) AND Eps.Month=" + month + " and EmpAttendance.Design=Eps.Desgn   Order by Right(Eps.EmpId,6)";

            }
            if (ddlnoofattendance.SelectedIndex == 2)
            {
                selectmonth = "select Eps.EmpId,Eps.Desgn,TempGross as salaryrate, Eps.NoOfDuties,Eps.Basic,Eps.DA,Eps.HRA,Eps.CCA,Eps.Conveyance,Eps.Bonus, " +
                     "   Eps.WashAllowance,Eps.OtherAllowance,Eps.PF,Eps.ESI,Eps.OTAmt,Eps.ProfTax,Eps.Deductions," +
                     "Eps.SalAdvDed,Eps.CanteenAdv,Eps.UniformDed,Eps.Penalty,Eps.OtherDed,Eps.Gross,Eps.Deductions,Eps.ActualAmount,Eps.OWF,  " +
                     " EmpDetails.EmpFName,EmpDetails.EmpMName," +
                     "EmpDetails.UnitId, EmpDetails.EmpBankAcNo, EmpDetails.EmpBankCardRef,EmpAttendance.OT from EmpPaySheet as Eps INNER JOIN    " +
                     " EmpDetails ON Eps.EmpId = EmpDetails.EmpId INNER JOIN EmpAttendance ON EmpAttendance.EmpId=Eps.EmpId AND  " +
                     " EmpAttendance.ClientId=Eps.ClientId AND EmpAttendance.Month=Eps.Month  AND Eps.ClientId='" +
                    // ddlClients.SelectedValue + "' AND Eps.Month=" + month + "  Order by   case when EmpDetails.EmpBankAcNo is null then 1 else 0 end, Eps.EmpId   ";
                     ddlclient.SelectedValue + "'   And (Eps.PF=0  or  Eps.ESI=0) AND Eps.Month=" + month + " and EmpAttendance.Design=Eps.Desgn   Order by Right(Eps.EmpId,6)";

            }
            DataTable dt = config.ExecuteAdaptorAsyncWithQueryParams(selectmonth).Result;

            MemoryStream ms = new MemoryStream();
            if (dt.Rows.Count > 0)
            {
                Document document = new Document(PageSize.LEGAL.Rotate());
                PdfWriter writer = PdfWriter.GetInstance(document, ms);
                document.Open();
                document.AddTitle("FaMS");
                document.AddAuthor("WebWonders");
                document.AddSubject("Wage Sheet for only Duties");
                document.AddKeywords("Keyword1, keyword2, …");//
                float forConvert;
                string strQry = "Select * from CompanyInfo";
                DataTable compInfo = config.ExecuteAdaptorAsyncWithQueryParams(strQry).Result;
                string companyName1 = "Your Company Name";
                string companyAddress = "Your Company Address";
                if (compInfo.Rows.Count > 0)
                {
                    companyName1 = compInfo.Rows[0]["CompanyName"].ToString();
                    companyAddress = compInfo.Rows[0]["Address"].ToString();
                }

                int tableCells = 21;

                #region variables for total
                float totalActualamount = 0;
                float totalDuties = 0;
                float totalOts = 0;
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
                float totalDed = 0;
                float totalSalAdv = 0;
                float totalUniforDed = 0;
                float totalCanteenAdv = 0;
                float totalOwf = 0;
                float totalPenalty = 0;
                float totalGross = 0;
                float totalbonus = 0;
                #endregion

                #region Titles of Document
                PdfPTable Maintable = new PdfPTable(tableCells);
                Maintable.TotalWidth = 950f;
                Maintable.LockedWidth = true;
                float[] width = new float[] { 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f };
                Maintable.SetWidths(width);
                uint FONT_SIZE = 8;

                //Company Name & vage act details
                PdfPCell cellemp = new PdfPCell(new Phrase("  ", FontFactory.GetFont(Fontstyle, 10, Font.BOLD, BaseColor.BLACK)));
                cellemp.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                cellemp.Colspan = tableCells;
                cellemp.Border = 0;

                PdfPCell Heading = new PdfPCell(new Phrase("Form - XVII REGISTER OF WAGES", FontFactory.GetFont(Fontstyle, 20, Font.BOLD, BaseColor.BLACK)));
                Heading.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                Heading.Colspan = tableCells;
                Heading.Border = 0;
                Maintable.AddCell(Heading);

                PdfPCell actDetails = new PdfPCell(new Phrase("(vide Rule 78(1) a(i) of Contract Labour (Reg. & abolition) Central & A.P. Rules)", FontFactory.GetFont(Fontstyle, 15, Font.BOLD, BaseColor.BLACK)));
                actDetails.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                actDetails.Colspan = tableCells;
                actDetails.Border = 0;// 15;
                Maintable.AddCell(actDetails);

                Maintable.AddCell(cellemp);
                #endregion

                #region Table Headings
                //1
                PdfPCell companyName = new PdfPCell(new Phrase(companyName1, FontFactory.GetFont(Fontstyle, 12, Font.BOLD, BaseColor.BLACK)));
                companyName.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                companyName.Colspan = tableCells;
                companyName.Border = 0;// 15;
                Maintable.AddCell(companyName);
                //2
                PdfPCell paySheet = new PdfPCell(new Phrase("Pay Sheet", FontFactory.GetFont(Fontstyle, 9, Font.BOLD | Font.UNDERLINE, BaseColor.BLACK)));
                paySheet.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                paySheet.Colspan = tableCells;
                paySheet.Border = 0;// 15;
                Maintable.AddCell(paySheet);
                //3
                PdfPCell CClient = new PdfPCell(new Phrase("Client ID :   " + ddlclient.SelectedValue, FontFactory.GetFont(Fontstyle, 9, Font.NORMAL, BaseColor.BLACK)));
                CClient.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                CClient.Colspan = 3;
                CClient.Border = 0;// 15;
                Maintable.AddCell(CClient);
                //4
                PdfPCell CClientName = new PdfPCell(new Phrase("Client Name :   " + ddlcname.SelectedItem, FontFactory.GetFont(Fontstyle, 9, Font.NORMAL, BaseColor.BLACK)));
                CClientName.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                CClientName.Colspan = 6;
                CClientName.Border = 0;// 15;
                Maintable.AddCell(CClientName);
                //5
                PdfPCell CDates = new PdfPCell(new Phrase("Payment for the period of : " + GetPaymentPeriod(ddlclient.SelectedValue), FontFactory.GetFont(Fontstyle, 9, Font.NORMAL, BaseColor.BLACK)));
                CDates.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                CDates.Colspan = 6;
                CDates.Border = 0;// 15;
                Maintable.AddCell(CDates);
                //6


                //PdfPCell CPaySheetDate = new PdfPCell(new Phrase("Pay Sheet Date :  " + DateTime.Now.Date.ToString("dd/MM/yyyy"), FontFactory.GetFont(Fontstyle, 9, Font.NORMAL, BaseColor.BLACK)));

                PdfPCell CPaySheetDate = new PdfPCell(new Phrase(" ", FontFactory.GetFont(Fontstyle, 9, Font.NORMAL, BaseColor.BLACK)));
                CPaySheetDate.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                CPaySheetDate.Colspan = 3;
                CPaySheetDate.Border = 0;// 15;
                Maintable.AddCell(CPaySheetDate);
                //7
                PdfPCell CPayMonth = new PdfPCell(new Phrase("For the month of :   " + DateTime.Parse(txtmonth.Text, CultureInfo.GetCultureInfo("en-gb")).ToString("MMMM"), FontFactory.GetFont(Fontstyle, 9, Font.NORMAL, BaseColor.BLACK)));
                CPayMonth.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                CPayMonth.Colspan = 3;
                CPayMonth.Border = 0;// 15;
                Maintable.AddCell(CPayMonth);

                Maintable.AddCell(cellemp);
                document.Add(Maintable);

                tableCells = 25;
                PdfPTable Secondtable = new PdfPTable(tableCells);
                Secondtable.TotalWidth = 950f;
                Secondtable.LockedWidth = true;
                float[] SecondWidth = new float[] { 1.2f, 2f, 4f, 3f, 1f, 1.5f, 1.5f, 1.5f, 1f, 1f, 1.5f, 1.5f, 1.6f, 1.5f, 1.5f, 1.3f, 1.4f, 1f, 1.3f, 1.3f, 1.5f, 1.5f, 1.5f, 1.5f, 7f };
                Secondtable.SetWidths(SecondWidth);

                //Cell Headings
                //1
                PdfPCell sNo = new PdfPCell(new Phrase("S.No", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                sNo.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                //sNo.Colspan = 1;
                sNo.Border = 15;// 15;
                Secondtable.AddCell(sNo);
                //2
                PdfPCell CEmpId = new PdfPCell(new Phrase("Emp Id", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                CEmpId.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                CEmpId.Border = 15;// 15;
                Secondtable.AddCell(CEmpId);
                //3
                PdfPCell CEmpName = new PdfPCell(new Phrase("Emp Name", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                CEmpName.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                CEmpName.Border = 15;// 15;
                Secondtable.AddCell(CEmpName);
                //4
                PdfPCell CDesgn = new PdfPCell(new Phrase("Desgn", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                CDesgn.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                CDesgn.Border = 15;
                Secondtable.AddCell(CDesgn);
                //5
                PdfPCell CDuties = new PdfPCell(new Phrase("Dts", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                CDuties.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                CDuties.Border = 15;
                Secondtable.AddCell(CDuties);

                //6
                PdfPCell COts = new PdfPCell(new Phrase("OTs", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                COts.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                COts.Border = 15;
                Secondtable.AddCell(COts);


                //7
                PdfPCell CSalaryRate = new PdfPCell(new Phrase("S.Rate", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                CSalaryRate.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                CSalaryRate.Border = 15;
                Secondtable.AddCell(CSalaryRate);
                //8
                PdfPCell CBasic = new PdfPCell(new Phrase("Basic", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                CBasic.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                CBasic.Border = 15;
                Secondtable.AddCell(CBasic);
                //9
                PdfPCell CDa = new PdfPCell(new Phrase("DA", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                CDa.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                CDa.Border = 15;
                Secondtable.AddCell(CDa);
                //10
                PdfPCell CHRa = new PdfPCell(new Phrase("HRA", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                CHRa.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                CHRa.Border = 15;
                Secondtable.AddCell(CHRa);
                //11
                PdfPCell COa = new PdfPCell(new Phrase("OA", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                COa.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                COa.Border = 15;
                Secondtable.AddCell(COa);

                //12
                PdfPCell CBonus = new PdfPCell(new Phrase("Bonus", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                CBonus.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                CBonus.Border = 15;
                Secondtable.AddCell(CBonus);
                //13
                PdfPCell CGross = new PdfPCell(new Phrase("Gross", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                CGross.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                CGross.Border = 15;
                Secondtable.AddCell(CGross);
                //14
                PdfPCell COtAmt = new PdfPCell(new Phrase("OT Amt", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                COtAmt.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                COtAmt.Border = 15;
                Secondtable.AddCell(COtAmt);
                //15
                PdfPCell CPF = new PdfPCell(new Phrase("PF", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                CPF.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                CPF.Border = 15;
                Secondtable.AddCell(CPF);
                //16
                PdfPCell CESI = new PdfPCell(new Phrase("ESI", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                CESI.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                CESI.Border = 15;
                Secondtable.AddCell(CESI);
                //17
                PdfPCell CPT = new PdfPCell(new Phrase("PT", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                CPT.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                CPT.Border = 15;
                Secondtable.AddCell(CPT);
                //18
                PdfPCell CSalAdv = new PdfPCell(new Phrase("Sal Adv", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                CSalAdv.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                CSalAdv.Border = 15;
                Secondtable.AddCell(CSalAdv);
                //19
                PdfPCell CUnifDed = new PdfPCell(new Phrase("Unif. Ded", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                CUnifDed.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                CUnifDed.Border = 15;
                Secondtable.AddCell(CUnifDed);
                //20
                PdfPCell COWF = new PdfPCell(new Phrase("OWF", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                COWF.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                COWF.Border = 15;
                Secondtable.AddCell(COWF);
                //21
                PdfPCell COtherDed = new PdfPCell(new Phrase("Other Ded", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                COtherDed.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                COtherDed.Border = 15;
                Secondtable.AddCell(COtherDed);
                //22

                PdfPCell Ccantadv = new PdfPCell(new Phrase("Cant Adv", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                Ccantadv.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                Ccantadv.Border = 15;
                Secondtable.AddCell(Ccantadv);
                //23
                PdfPCell CTotDed = new PdfPCell(new Phrase("Tot Ded", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                CTotDed.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                CTotDed.Border = 15;
                Secondtable.AddCell(CTotDed);
                //24
                PdfPCell CNetPay = new PdfPCell(new Phrase("Net Pay", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                CNetPay.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                CNetPay.Border = 15;
                Secondtable.AddCell(CNetPay);
                //25
                PdfPCell CSignature = new PdfPCell(new Phrase("Bank A/c No./ Signature", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                CSignature.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                CSignature.Border = 15;
                Secondtable.AddCell(CSignature);
                #endregion

                #region Table Data
                int rowCount = 0;
                int pageCount = 0;
                int PFAmt = 0;
                int ESIAmt = 0;
                int TotAmt = 0;
                for (int i = 0, j = 0; i < dt.Rows.Count; i++)
                {
                    forConvert = 0;
                    float forConvertDuties = 0;
                    if (dt.Rows[i]["NoOfDuties"].ToString().Trim().Length > 0)
                        forConvertDuties = Convert.ToSingle(dt.Rows[i]["NoOfDuties"].ToString());

                    if (dt.Rows[i]["PF"].ToString().Trim().Length > 0)
                        forConvert = (float)Math.Round(Convert.ToSingle(dt.Rows[i]["PF"].ToString()));
                    if (forConvert > 0)
                    {
                        PFAmt = 1;
                    }

                    if (dt.Rows[i]["ESI"].ToString().Trim().Length > 0)
                        forConvert = (float)Math.Round(Convert.ToSingle(dt.Rows[i]["ESI"].ToString()));
                    if (forConvert > 0)
                    {
                        ESIAmt = 1;
                    }
                    if (PFAmt == 1 && ESIAmt == 1)
                    {
                        TotAmt = 1;
                    }
                    if (forConvertDuties > 0 && TotAmt == 0)
                    {
                        //1
                        PdfPCell CSNo = new PdfPCell(new Phrase((++j).ToString(), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                        CSNo.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                        CSNo.Border = 15;
                        Secondtable.AddCell(CSNo);
                        //2
                        PdfPCell CEmpId1 = new PdfPCell(new Phrase(dt.Rows[i]["EmpId"].ToString(), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                        CEmpId1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                        CEmpId1.Border = 15;
                        Secondtable.AddCell(CEmpId1);
                        //3
                        PdfPCell CEmpName1 = new PdfPCell(new Phrase(dt.Rows[i]["EmpFName"].ToString() + " " + dt.Rows[i]["EmpMName"].ToString(), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                        CEmpName1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                        CEmpName1.Border = 15;
                        Secondtable.AddCell(CEmpName1);
                        //4
                        PdfPCell CEmpDesgn = new PdfPCell(new Phrase(dt.Rows[i]["Desgn"].ToString(), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                        CEmpDesgn.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                        CEmpDesgn.Border = 15;
                        Secondtable.AddCell(CEmpDesgn);
                        //5

                        forConvert = 0;
                        if (dt.Rows[i]["NoOfDuties"].ToString().Trim().Length > 0)
                            forConvert = Convert.ToSingle(dt.Rows[i]["NoOfDuties"].ToString());
                        totalDuties += forConvert;
                        PdfPCell CNoOfDuties = new PdfPCell(new Phrase(forConvert.ToString("0.00"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                        CNoOfDuties.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                        CNoOfDuties.Border = 15;
                        Secondtable.AddCell(CNoOfDuties);


                        //6
                        forConvert = 0;
                        if (dt.Rows[i]["ot"].ToString().Trim().Length > 0)
                            forConvert = Convert.ToSingle(dt.Rows[i]["ot"].ToString());
                        totalOts += forConvert;
                        PdfPCell CNoOfots = new PdfPCell(new Phrase(forConvert.ToString(), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                        CNoOfots.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                        CNoOfots.Border = 15;
                        Secondtable.AddCell(CNoOfots);


                        //7
                        forConvert = 0;
                        if (dt.Rows[i]["salaryrate"].ToString().Trim().Length > 0)
                            forConvert = Convert.ToSingle(dt.Rows[i]["salaryrate"].ToString());
                        PdfPCell CSalRate = new PdfPCell(new Phrase(forConvert.ToString("0.00"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                        CSalRate.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                        CSalRate.Border = 15;
                        Secondtable.AddCell(CSalRate);

                        //8
                        forConvert = 0;
                        if (dt.Rows[i]["Basic"].ToString().Trim().Length > 0)
                            forConvert = (float)Math.Round(Convert.ToSingle(dt.Rows[i]["Basic"].ToString()));
                        totalBasic += forConvert;
                        PdfPCell CBasic1 = new PdfPCell(new Phrase(forConvert.ToString("0.00"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                        CBasic1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                        CBasic1.Border = 15;
                        Secondtable.AddCell(CBasic1);

                        //9
                        forConvert = 0;
                        if (dt.Rows[i]["DA"].ToString().Trim().Length > 0)
                            forConvert = (float)Math.Round(Convert.ToSingle(dt.Rows[i]["DA"].ToString()));
                        totalDA += forConvert;
                        PdfPCell CDA1 = new PdfPCell(new Phrase(forConvert.ToString("0.00"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                        CDA1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                        CDA1.Border = 15;
                        Secondtable.AddCell(CDA1);

                        //10
                        forConvert = 0;
                        if (dt.Rows[i]["HRA"].ToString().Trim().Length > 0)
                            forConvert = (float)Math.Round(Convert.ToSingle(dt.Rows[i]["HRA"].ToString()));
                        totalHRA += forConvert;
                        PdfPCell CHRA1 = new PdfPCell(new Phrase(forConvert.ToString("0.00"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                        CHRA1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                        CHRA1.Border = 15;
                        Secondtable.AddCell(CHRA1);

                        //11
                        forConvert = 0;
                        if (dt.Rows[i]["OtherAllowance"].ToString().Trim().Length > 0)
                            forConvert = (float)Math.Round(Convert.ToSingle(dt.Rows[i]["OtherAllowance"].ToString()));
                        totalOA += forConvert;
                        PdfPCell COA1 = new PdfPCell(new Phrase(forConvert.ToString("0.00"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                        COA1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                        COA1.Border = 15;
                        Secondtable.AddCell(COA1);

                        //12

                        forConvert = 0;
                        if (dt.Rows[i]["bonus"].ToString().Trim().Length > 0)
                            forConvert = (float)Math.Round(Convert.ToSingle(dt.Rows[i]["bonus"].ToString()));
                        totalbonus += forConvert;
                        PdfPCell Cbonus = new PdfPCell(new Phrase(forConvert.ToString("0.00"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                        Cbonus.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                        Cbonus.Border = 15;
                        Secondtable.AddCell(Cbonus);

                        //13

                        forConvert = 0;
                        if (dt.Rows[i]["Gross"].ToString().Trim().Length > 0)
                            forConvert = (float)Math.Round(Convert.ToSingle(dt.Rows[i]["Gross"].ToString()));
                        totalGrass += forConvert;
                        PdfPCell CGross1 = new PdfPCell(new Phrase(forConvert.ToString("0.00"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                        CGross1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                        CGross1.Border = 15;
                        Secondtable.AddCell(CGross1);


                        //14
                        forConvert = 0;
                        if (dt.Rows[i]["otamt"].ToString().Trim().Length > 0)
                            forConvert = (float)Math.Round(Convert.ToSingle(dt.Rows[i]["otamt"].ToString()));
                        totalOTAmount += forConvert;
                        PdfPCell CTOtamt = new PdfPCell(new Phrase(forConvert.ToString("0.00"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                        CTOtamt.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                        CTOtamt.Border = 15;
                        Secondtable.AddCell(CTOtamt);


                        //15
                        forConvert = 0;
                        if (dt.Rows[i]["PF"].ToString().Trim().Length > 0)
                            forConvert = (float)Math.Round(Convert.ToSingle(dt.Rows[i]["PF"].ToString()));
                        totalPF += forConvert;

                        PdfPCell CPF1 = new PdfPCell(new Phrase(forConvert.ToString("0.00"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                        CPF1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                        CPF1.Border = 15;
                        Secondtable.AddCell(CPF1);

                        //16
                        forConvert = 0;
                        if (dt.Rows[i]["ESI"].ToString().Trim().Length > 0)
                            forConvert = (float)Math.Round(Convert.ToSingle(dt.Rows[i]["ESI"].ToString()));
                        totalESI += forConvert;
                        PdfPCell CESI1 = new PdfPCell(new Phrase(forConvert.ToString("0.00"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                        CESI1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                        CESI1.Border = 15;
                        Secondtable.AddCell(CESI1);

                        //17
                        forConvert = 0;
                        if (dt.Rows[i]["ProfTax"].ToString().Trim().Length > 0)
                            forConvert = (float)Math.Round(Convert.ToSingle(dt.Rows[i]["ProfTax"].ToString()));
                        totalProfTax += forConvert;
                        PdfPCell CProTax1 = new PdfPCell(new Phrase(forConvert.ToString("0.00"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                        CProTax1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                        CProTax1.Border = 15;
                        Secondtable.AddCell(CProTax1);

                        //18
                        forConvert = 0;
                        if (dt.Rows[i]["SalAdvDed"].ToString().Trim().Length > 0)
                            forConvert = (float)Math.Round(Convert.ToSingle(dt.Rows[i]["SalAdvDed"].ToString()));
                        totalSalAdv += forConvert;
                        PdfPCell CSalAdv1 = new PdfPCell(new Phrase(forConvert.ToString("0.00"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                        CSalAdv1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                        CSalAdv1.Border = 15;
                        Secondtable.AddCell(CSalAdv1);
                        forConvert = 0;
                        //19
                        if (dt.Rows[i]["UniformDed"].ToString().Trim().Length > 0)
                            forConvert = (float)Math.Round(Convert.ToSingle(dt.Rows[i]["UniformDed"].ToString()));
                        totalUniforDed += forConvert;
                        PdfPCell CUnifDed1 = new PdfPCell(new Phrase(forConvert.ToString("0.00"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                        CUnifDed1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                        CUnifDed1.Border = 15;
                        Secondtable.AddCell(CUnifDed1);

                        forConvert = 0;
                        //20
                        if (dt.Rows[i]["OWF"].ToString().Trim().Length > 0)
                            forConvert = (float)Math.Round(Convert.ToSingle(dt.Rows[i]["OWF"].ToString()));
                        totalOwf += forConvert;
                        PdfPCell COwf1 = new PdfPCell(new Phrase(forConvert.ToString("0.00"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                        COwf1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                        COwf1.Border = 15;
                        Secondtable.AddCell(COwf1);
                        //21
                        forConvert = 0;
                        if (dt.Rows[i]["Penalty"].ToString().Trim().Length > 0)
                            forConvert = (float)Math.Round(Convert.ToSingle(dt.Rows[i]["Penalty"].ToString()));
                        totalPenalty += forConvert;
                        PdfPCell COtherDed1 = new PdfPCell(new Phrase(forConvert.ToString("0.00"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                        COtherDed1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                        COtherDed1.Border = 15;
                        Secondtable.AddCell(COtherDed1);
                        //22
                        forConvert = 0;
                        if (dt.Rows[i]["CanteenAdv"].ToString().Trim().Length > 0)
                            forConvert = (float)Math.Round(Convert.ToSingle(dt.Rows[i]["CanteenAdv"].ToString()));
                        totalCanteenAdv += forConvert;
                        PdfPCell CCanteenAdv = new PdfPCell(new Phrase(forConvert.ToString(), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                        CCanteenAdv.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                        CCanteenAdv.Border = 15;
                        Secondtable.AddCell(CCanteenAdv);

                        //23
                        forConvert = 0;
                        float totaldeductions = 0;
                        if (dt.Rows[i]["Deductions"].ToString().Trim().Length > 0)
                        {
                            forConvert = (float)Math.Round(Convert.ToSingle(dt.Rows[i]["Deductions"].ToString()));
                            totaldeductions = forConvert;
                        }
                        totalDed += forConvert;
                        PdfPCell CTotDed1 = new PdfPCell(new Phrase(forConvert.ToString("0.00"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                        CTotDed1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                        CTotDed1.Border = 15;
                        Secondtable.AddCell(CTotDed1);//OtherDed,Eps.Gross,Eps.Deductions,Eps.ActualAmount

                        //24
                        forConvert = 0;
                        if (dt.Rows[i]["ActualAmount"].ToString().Trim().Length > 0)
                            forConvert = (float)Math.Round(Convert.ToSingle(dt.Rows[i]["ActualAmount"].ToString()));
                        // totalActualamount += forConvert;

                        //forConvert = forConvert - totaldeductions;
                        //if (dt.Rows[i]["OTAmt"].ToString().Trim().Length > 0)
                        //    forConvert = (float)Math.Round(Convert.ToSingle(dt.Rows[i]["OTAmt"].ToString())) + forConvert ;


                        if (forConvert < 0)
                        {
                            forConvert = 0;
                        }

                        // forConvert = forConvert - totaldeductions;

                        if (forConvert < 0)
                        {
                            forConvert = 0;
                        }

                        totalActualamount += forConvert;


                        PdfPCell CNetPay1 = new PdfPCell(new Phrase(forConvert.ToString("0.00"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                        CNetPay1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                        CNetPay1.Border = 15;
                        Secondtable.AddCell(CNetPay1);
                        //25
                        forConvert = 0;
                        string EmpBankAcNo = dt.Rows[i]["EmpBankAcNo"].ToString();
                        PdfPCell CSignature1 = new PdfPCell(new Phrase(EmpBankAcNo, FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                        CSignature1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                        CSignature1.Border = 15;
                        CSignature1.MinimumHeight = 25;
                        Secondtable.AddCell(CSignature1);
                    }

                }
                #endregion
                #region Table Footer
                //1
                PdfPCell FCSNo = new PdfPCell(new Phrase("", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                FCSNo.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                FCSNo.Border = 15;
                Secondtable.AddCell(FCSNo);
                //2
                PdfPCell FCEmpId1 = new PdfPCell(new Phrase("", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                FCEmpId1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                FCEmpId1.Border = 15;
                Secondtable.AddCell(FCEmpId1);
                //3
                PdfPCell FCEmpName1 = new PdfPCell(new Phrase(" ", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                FCEmpName1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                FCEmpName1.Border = 15;
                Secondtable.AddCell(FCEmpName1);
                //4
                PdfPCell FCEmpDesgn = new PdfPCell(new Phrase("", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                FCEmpDesgn.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                FCEmpDesgn.Border = 15;
                //FCEmpDesgn.Colspan = 4;
                Secondtable.AddCell(FCEmpDesgn);
                //5
                PdfPCell FCNoOfDuties = new PdfPCell(new Phrase(totalDuties.ToString("0.00"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                FCNoOfDuties.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                FCNoOfDuties.Border = 15;
                Secondtable.AddCell(FCNoOfDuties);



                PdfPCell FCNoOfOts = new PdfPCell(new Phrase(totalOts.ToString("0.00"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                FCNoOfOts.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                FCNoOfOts.Border = 15;
                Secondtable.AddCell(FCNoOfOts);

                //6
                PdfPCell FCNoOfots = new PdfPCell(new Phrase("", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                FCNoOfots.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                FCNoOfots.Border = 15;
                Secondtable.AddCell(FCNoOfots);

                //7
                PdfPCell FCBasic1 = new PdfPCell(new Phrase(Math.Round(totalBasic).ToString("0.00"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                FCBasic1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                FCBasic1.Border = 15;
                Secondtable.AddCell(FCBasic1);
                //8

                forConvert = 0;
                PdfPCell FCHRA1 = new PdfPCell(new Phrase(Math.Round(totalDA).ToString("0.00"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                FCHRA1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                FCHRA1.Border = 15;
                Secondtable.AddCell(FCHRA1);

                //9
                PdfPCell FCConveyance = new PdfPCell(new Phrase(Math.Round(totalHRA).ToString("0.00"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                FCConveyance.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                FCConveyance.Border = 15;
                Secondtable.AddCell(FCConveyance);

                //10
                PdfPCell FCcca = new PdfPCell(new Phrase(Math.Round(totalOA).ToString("0.00"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                FCcca.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                FCcca.Border = 15;
                Secondtable.AddCell(FCcca);



                //11
                PdfPCell Fbonus = new PdfPCell(new Phrase(Math.Round(totalbonus).ToString("0.00"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                Fbonus.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                Fbonus.Border = 15;
                Secondtable.AddCell(Fbonus);

                //12
                PdfPCell FCGross1 = new PdfPCell(new Phrase(Math.Round(totalGrass).ToString("0.00"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                FCGross1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                FCGross1.Border = 15;
                Secondtable.AddCell(FCGross1);

                //13


                PdfPCell FCtototamt = new PdfPCell(new Phrase(Math.Round(totalOTAmount).ToString("0.00"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                FCtototamt.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                FCtototamt.Border = 15;
                Secondtable.AddCell(FCtototamt);


                //14
                PdfPCell FCPF1 = new PdfPCell(new Phrase(Math.Round(totalPF).ToString("0.00"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                FCPF1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                FCPF1.Border = 15;
                Secondtable.AddCell(FCPF1);
                //15
                PdfPCell FCESI1 = new PdfPCell(new Phrase(Math.Round(totalESI).ToString("0.00"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                FCESI1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                FCESI1.Border = 15;
                Secondtable.AddCell(FCESI1);
                //16
                PdfPCell FCProTax1 = new PdfPCell(new Phrase(Math.Round(totalProfTax).ToString("0.00"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                FCProTax1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                FCProTax1.Border = 15;
                Secondtable.AddCell(FCProTax1);
                //17
                PdfPCell FCSalAdv1 = new PdfPCell(new Phrase(Math.Round(totalSalAdv).ToString("0.00"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                FCSalAdv1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                FCSalAdv1.Border = 15;
                Secondtable.AddCell(FCSalAdv1);
                //18
                PdfPCell FCUnifDed1 = new PdfPCell(new Phrase(Math.Round(totalUniforDed).ToString("0.00"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                FCUnifDed1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                FCUnifDed1.Border = 15;
                Secondtable.AddCell(FCUnifDed1);

                //19
                PdfPCell FCOwf1 = new PdfPCell(new Phrase(Math.Round(totalOwf).ToString("0.00"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                FCOwf1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                FCOwf1.Border = 15;
                Secondtable.AddCell(FCOwf1);
                //20
                PdfPCell FCOtherDed1 = new PdfPCell(new Phrase(Math.Round(totalPenalty).ToString("0.00"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                FCOtherDed1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                FCOtherDed1.Border = 15;
                Secondtable.AddCell(FCOtherDed1);
                //21
                PdfPCell FCTotDed1 = new PdfPCell(new Phrase(Math.Round(totalCanteenAdv).ToString("0.00"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                FCTotDed1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                FCTotDed1.Border = 15;
                Secondtable.AddCell(FCTotDed1);//OtherDed,Eps.Gross,Eps.Deductions,Eps.ActualAmount
                //22
                PdfPCell FCNetPay1 = new PdfPCell(new Phrase(Math.Round(totalDed).ToString("0.00"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                FCNetPay1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                FCNetPay1.Border = 15;
                //FCNetPay1.Colspan = 2;
                Secondtable.AddCell(FCNetPay1);
                //23
                PdfPCell FCSignature1 = new PdfPCell(new Phrase(Math.Round(totalActualamount).ToString("0.00"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                FCSignature1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                FCSignature1.Border = 15;
                //FCSignature1.MinimumHeight = 25;
                Secondtable.AddCell(FCSignature1);

                //24
                PdfPCell FCSignature12 = new PdfPCell(new Phrase(" ", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                FCSignature12.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                FCSignature12.Border = 15;
                //FCSignature1.MinimumHeight = 25;
                Secondtable.AddCell(FCSignature12);

                #endregion


                #region   Footer Headings


                //1
                PdfPCell FHCheckedbybr1 = new PdfPCell(new Phrase("", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                FHCheckedbybr1.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                FHCheckedbybr1.Border = 0;
                FHCheckedbybr1.Rowspan = 0;
                FHCheckedbybr1.Colspan = 13;
                Secondtable.AddCell(FHCheckedbybr1);
                //2
                PdfPCell FHApprovedbr2 = new PdfPCell(new Phrase("  ", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                FHApprovedbr2.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                FHApprovedbr2.Border = 0;
                FHApprovedbr2.Colspan = 12;
                Secondtable.AddCell(FHApprovedbr2);


                Secondtable.AddCell(FHCheckedbybr1);
                Secondtable.AddCell(FHApprovedbr2);





                //1
                PdfPCell FHCheckedby = new PdfPCell(new Phrase("Checked By", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                FHCheckedby.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                FHCheckedby.Border = 0;
                FHCheckedby.Rowspan = 0;
                FHCheckedby.Colspan = 13;
                Secondtable.AddCell(FHCheckedby);
                //2
                PdfPCell FHApprovedBy = new PdfPCell(new Phrase("Gross  Approved By", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                FHApprovedBy.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                FHApprovedBy.Border = 0;
                FHApprovedBy.Colspan = 12;
                Secondtable.AddCell(FHApprovedBy);
                #endregion

                document.Add(Secondtable);
                document.Close();

                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment;filename=Wage Sheet for Duties.pdf");
                Response.Buffer = true;
                Response.Clear();
                Response.OutputStream.Write(ms.GetBuffer(), 0, ms.GetBuffer().Length);
                Response.OutputStream.Flush();
                Response.End();

            }

        }


        protected void btnonlyduties3_Click(object sender, EventArgs e)
        {
            string FoNtStyle = "impact";

            int titleofdocumentindex = 0;
            if (ddlclient.SelectedIndex <= 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showlalert", "alert('Please select Client ID to generate wage sheet');", true);

                return;
            }
            string month = Getmonth();
            if (month.Trim().Length == 0)
            {
                return;
            }
            string selectmonth = string.Empty;

            if (ddlnoofattendance.SelectedIndex == 0)
            {
                selectmonth = "select Eps.EmpId,Eps.Desgn,TempGross as salaryrate, Eps.NoOfDuties,Eps.Basic,Eps.DA,Eps.HRA,Eps.CCA,Eps.OWFAmt, " +
                    " Eps.Conveyance,Eps.WashAllowance,Eps.OtherAllowance,Eps.PF,Eps.ESI,Eps.OTAmt,Eps.ProfTax,Eps.Bonus," +
               "Eps.SalAdvDed,Eps.CanteenAdv,Eps.UniformDed,Eps.Penalty,Eps.OtherDed,Eps.Gross,Eps.Deductions," +
               "  Eps.ActualAmount,Eps.OWF,EmpDetails.EmpFName,(EmpDetails.EmpFName +' '+ EmpDetails.EmpMName +' '+ EmpDetails.EmpLName  )   as     EmpMName," +
               "EmpDetails.UnitId, EmpDetails.EmpBankAppNo,EmpDetails.EmpBankAcNo,  " +
               " EmpDetails.EmpBankCardRef,EmpAttendance.OT from EmpPaySheet as Eps INNER JOIN EmpDetails " +
               "  ON Eps.EmpId = EmpDetails.EmpId INNER JOIN EmpAttendance ON   " +
               " EmpAttendance.EmpId=Eps.EmpId  And  Eps.NoOfDuties>10 AND EmpAttendance.ClientId=Eps.ClientId AND EmpAttendance.Month=Eps.Month AND Eps.ClientId='" +
                    //ddlClients.SelectedValue + "' AND Eps.Month=" + month + "  Order by  case when EmpDetails.EmpBankAcNo is null then 1 else 0 end, Eps.EmpId "; ;
               ddlclient.SelectedValue + "' AND Eps.Month=" + month + " and  EmpAttendance.Design=Eps.Desgn   Order by Right(Eps.EmpId,6)";

            }
            if (ddlnoofattendance.SelectedIndex == 1)
            {
                selectmonth = "select Eps.EmpId,Eps.Desgn,TempGross as salaryrate, Eps.NoOfDuties,Eps.Basic,Eps.DA,Eps.HRA,Eps.CCA,Eps.OWFAmt, " +
                  " Eps.Conveyance,Eps.WashAllowance,Eps.OtherAllowance,Eps.PF,Eps.ESI,Eps.OTAmt,Eps.ProfTax,Eps.Bonus," +
             "Eps.SalAdvDed,Eps.CanteenAdv,Eps.UniformDed,Eps.Penalty,Eps.OtherDed,Eps.Gross,Eps.Deductions," +
             "  Eps.ActualAmount,Eps.OWF,EmpDetails.EmpFName,(EmpDetails.EmpFName +' '+ EmpDetails.EmpMName +' '+ EmpDetails.EmpLName  )   as     EmpMName," +
             "EmpDetails.UnitId, EmpDetails.EmpBankAppNo,EmpDetails.EmpBankAcNo,  " +
             " EmpDetails.EmpBankCardRef,EmpAttendance.OT from EmpPaySheet as Eps INNER JOIN EmpDetails " +
             "  ON Eps.EmpId = EmpDetails.EmpId INNER JOIN EmpAttendance ON   " +
             " EmpAttendance.EmpId=Eps.EmpId  And  Eps.NoOfDuties<=10 AND EmpAttendance.ClientId=Eps.ClientId AND EmpAttendance.Month=Eps.Month AND Eps.ClientId='" +
                    //ddlClients.SelectedValue + "' AND Eps.Month=" + month + "  Order by  case when EmpDetails.EmpBankAcNo is null then 1 else 0 end, Eps.EmpId "; ;
             ddlclient.SelectedValue + "' AND Eps.Month=" + month + " and  EmpAttendance.Design=Eps.Desgn  Order by Right(Eps.EmpId,6)";
            }

            if (ddlnoofattendance.SelectedIndex == 2)
            {
                selectmonth = "select Eps.EmpId,Eps.Desgn,TempGross as salaryrate, Eps.NoOfDuties,Eps.Basic,Eps.DA,Eps.HRA,Eps.CCA,Eps.OWFAmt, " +
                  " Eps.Conveyance,Eps.WashAllowance,Eps.OtherAllowance,Eps.PF,Eps.ESI,Eps.OTAmt,Eps.ProfTax,Eps.Bonus," +
             "Eps.SalAdvDed,Eps.CanteenAdv,Eps.UniformDed,Eps.Penalty,Eps.OtherDed,Eps.Gross,Eps.Deductions," +
             "  Eps.ActualAmount,Eps.OWF,EmpDetails.EmpFName,(EmpDetails.EmpFName +' '+ EmpDetails.EmpMName +' '+ EmpDetails.EmpLName  )   as     EmpMName," +
             "EmpDetails.UnitId, EmpDetails.EmpBankAppNo,EmpDetails.EmpBankAcNo,  " +
             " EmpDetails.EmpBankCardRef,EmpAttendance.OT from EmpPaySheet as Eps INNER JOIN EmpDetails " +
             "  ON Eps.EmpId = EmpDetails.EmpId INNER JOIN EmpAttendance ON   " +
             " EmpAttendance.EmpId=Eps.EmpId  AND EmpAttendance.ClientId=Eps.ClientId AND EmpAttendance.Month=Eps.Month AND Eps.ClientId='" +
                    //ddlClients.SelectedValue + "' AND Eps.Month=" + month + "  Order by  case when EmpDetails.EmpBankAcNo is null then 1 else 0 end, Eps.EmpId "; ;
             ddlclient.SelectedValue + "' AND Eps.Month=" + month + " and  EmpAttendance.Design=Eps.Desgn  Order by Right(Eps.EmpId,6)";
            }
            DataTable dt = config.ExecuteAdaptorAsyncWithQueryParams(selectmonth).Result;

            MemoryStream ms = new MemoryStream();
            if (dt.Rows.Count > 0)
            {
                Document document = new Document(PageSize.LEGAL.Rotate());
                PdfWriter writer = PdfWriter.GetInstance(document, ms);
                document.Open();
                document.AddTitle("FaMS");
                document.AddAuthor("DIYOS");
                document.AddSubject("Wage Sheet");
                document.AddKeywords("Keyword1, keyword2, …");//
                float forConvert;
                string strQry = "Select * from CompanyInfo";
                DataTable compInfo = config.ExecuteAdaptorAsyncWithQueryParams(strQry).Result;
                string companyName1 = "Your Company Name";
                string companyAddress = "Your Company Address";
                if (compInfo.Rows.Count > 0)
                {
                    companyName1 = compInfo.Rows[0]["CompanyName"].ToString();
                    companyAddress = compInfo.Rows[0]["Address"].ToString();
                }

                int tableCells = 24;
                #region variables for total
                float totalActualamount = 0;
                float totalDuties = 0;
                float totalOts = 0;
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
                float totalDed = 0;
                float totalSalAdv = 0;
                float totalUniforDed = 0;
                float totalCanteenAdv = 0;
                float totalOwf = 0;
                float totalPenalty = 0;
                float totalbonus = 0;
                float totaltotalpay = 0;
                float totalroundoff = 0;
                float totalnetpay = 0;
                #endregion

                #region variables for  Grand  total
                float GrandtotalActualamount = 0;
                float GrandtotalDuties = 0;
                float GrandtotalOts = 0;
                float GrandtotalBasic = 0;
                float GrandtotalDA = 0;
                float GrandtotalHRA = 0;
                float GrandtotalCCA = 0;
                float GrandtotalConveyance = 0;
                float GrandtotalWA = 0;
                float GrandtotalOA = 0;
                float GrandtotalGrass = 0;
                float GrandtotalOTAmount = 0;
                float GrandtotalPF = 0;
                float GrandtotalESI = 0;
                float GrandtotalProfTax = 0;
                float GrandtotalDed = 0;
                float GrandtotalSalAdv = 0;
                float GrandtotalUniforDed = 0;
                float GrandtotalCanteenAdv = 0;
                float GrandtotalOwf = 0;
                float GrandtotalPenalty = 0;
                float Grandtotalbonus = 0;
                float Grandtotaltotalpay = 0;
                float Grandtotalroundoff = 0;
                float Grandtotalnetpay = 0;

                #endregion

                int nextpagerecordscount = 0;
                int targetpagerecors = 11;
                int secondpagerecords = targetpagerecors + 3;
                bool nextpagehasPages = false;
                int j = 0;
                PdfPTable SecondtableFooter = null;
                PdfPTable SecondtablecheckedbyFooter = null;

                for (int nextpagei = 0; nextpagei < dt.Rows.Count; nextpagei++)
                {
                    nextpagehasPages = true;
                    #region Titles of Document
                    PdfPTable Maintable = new PdfPTable(tableCells);
                    Maintable.TotalWidth = 950f;
                    Maintable.LockedWidth = true;
                    float[] width = new float[] { 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f };
                    Maintable.SetWidths(width);
                    uint FONT_SIZE = 8;

                    //Company Name & vage act details

                    PdfPCell cellemp = new PdfPCell(new Phrase("  ", FontFactory.GetFont(Fontstyle, 10, Font.BOLD, BaseColor.BLACK)));
                    cellemp.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    cellemp.Colspan = tableCells;
                    cellemp.Border = 0;

                    PdfPCell Heading = new PdfPCell(new Phrase("Form - XVII REGISTER OF WAGES", FontFactory.GetFont(Fontstyle, 20, Font.BOLD, BaseColor.BLACK)));
                    Heading.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    Heading.Colspan = 11;
                    Heading.Border = 0;
                    Maintable.AddCell(Heading);



                    PdfPCell Heading1 = new PdfPCell(new Phrase("          Agency Name: " + companyName1, FontFactory.GetFont(Fontstyle, 12, Font.BOLD, BaseColor.BLACK)));
                    Heading1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    Heading1.Colspan = 13;
                    Heading1.Border = 0;
                    Maintable.AddCell(Heading1);

                    //Label lblcmpname = new Label();
                    //lblcmpname.Style.Add("Font-Face", "impact");
                    //lblcmpname.Text = companyName1;

                    //PdfPCell Heading2 = new PdfPCell(new Phrase(companyName1, FontFactory.GetFont("Impact", 15, Font.NORMAL, BaseColor.BLACK)));
                    //Heading2.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    //Heading2.Colspan = 10;
                    //Heading2.Border = 0;
                    //Maintable.AddCell(Heading2);



                    PdfPCell actDetails = new PdfPCell(new Phrase("(vide Rule 78(1) a(i) of Contract Labour (Reg. & abolition) Central & A.P. Rules)", FontFactory.GetFont(Fontstyle, 15, Font.BOLD, BaseColor.BLACK)));
                    actDetails.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    actDetails.Colspan = 11;
                    actDetails.Border = 0;// 15;
                    Maintable.AddCell(actDetails);

                    PdfPCell actDetails1 = new PdfPCell(new Phrase("          Client Name/Id: " + ddlcname.SelectedItem + "/" + ddlclient.SelectedValue, FontFactory.GetFont(Fontstyle, 12, Font.BOLD, BaseColor.BLACK)));
                    actDetails1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    actDetails1.Colspan = 13;
                    actDetails1.Border = 0;// 15;
                    Maintable.AddCell(actDetails1);

                    //PdfPCell actDetails2 = new PdfPCell(new Phrase( ddlcname.SelectedItem + "/" + ddlClients.SelectedValue, FontFactory.GetFont(Fontstyle, 15, Font.NORMAL, BaseColor.BLACK)));
                    //actDetails2.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    //actDetails2.Colspan = 10;
                    //actDetails2.Border = 0;// 15;
                    //Maintable.AddCell(actDetails2);

                    Maintable.AddCell(cellemp);
                    #endregion

                    #region Table Headings
                    PdfPCell companyName = new PdfPCell(new Phrase(companyName1, FontFactory.GetFont("Impact", 20, Font.BOLD, BaseColor.BLACK)));
                    companyName.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    companyName.Colspan = tableCells;
                    companyName.Border = 0;// 15;
                    //Maintable.AddCell(companyName);

                    PdfPCell paySheet = new PdfPCell(new Phrase("Pay Sheet", FontFactory.GetFont(Fontstyle, 10, Font.BOLD | Font.UNDERLINE, BaseColor.BLACK)));
                    paySheet.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    paySheet.Colspan = tableCells;
                    paySheet.Border = 0;// 15;
                    Maintable.AddCell(paySheet);

                    PdfPCell CClient = new PdfPCell(new Phrase("Client ID : " + ddlclient.SelectedValue, FontFactory.GetFont(Fontstyle, 10, Font.BOLD, BaseColor.BLACK)));
                    CClient.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    CClient.Colspan = 3;
                    CClient.Border = 0;// 15;
                    // Maintable.AddCell(CClient);

                    PdfPCell CClientName = new PdfPCell(new Phrase("Client Name : " + ddlcname.SelectedItem, FontFactory.GetFont(Fontstyle, 10, Font.BOLD, BaseColor.BLACK)));
                    CClientName.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    CClientName.Colspan = 7;
                    CClientName.Border = 0;// 15;
                    //Maintable.AddCell(CClientName);

                    PdfPCell CDates = new PdfPCell(new Phrase("Payment for the period of : " + GetPaymentPeriod(ddlclient.SelectedValue), FontFactory.GetFont(Fontstyle, 10, Font.BOLD, BaseColor.BLACK)));
                    CDates.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    CDates.Colspan = 7;
                    CDates.Border = 0;// 15;
                    Maintable.AddCell(CDates);

                    //PdfPCell CPaySheetDate = new PdfPCell(new Phrase("Pay Sheet Date :  " + DateTime.Now.Date.ToString("dd/MM/yyyy"), FontFactory.GetFont(Fontstyle, 10, Font.BOLD, BaseColor.BLACK)));

                    PdfPCell CPaySheetDate = new PdfPCell(new Phrase(" ", FontFactory.GetFont(Fontstyle, 10, Font.BOLD, BaseColor.BLACK)));
                    CPaySheetDate.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    CPaySheetDate.Colspan = 4;
                    CPaySheetDate.Border = 0;// 15;
                    Maintable.AddCell(CPaySheetDate);

                    PdfPCell CPayMonth = new PdfPCell(new Phrase("For the month of :   " + DateTime.Parse(txtmonth.Text, CultureInfo.GetCultureInfo("en-gb")).ToString("MMMM"), FontFactory.GetFont(Fontstyle, 10, Font.BOLD, BaseColor.BLACK)));
                    CPayMonth.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    CPayMonth.Colspan = 6;
                    CPayMonth.Border = 0;// 15;
                    Maintable.AddCell(CPayMonth);
                    Maintable.AddCell(cellemp);

                    if (titleofdocumentindex == 0)
                    {
                        document.Add(Maintable);
                        titleofdocumentindex = 1;
                    }
                    PdfPTable SecondtableHeadings = new PdfPTable(tableCells);
                    SecondtableHeadings.TotalWidth = 950f;
                    SecondtableHeadings.LockedWidth = true;
                    float[] SecondHeadingsWidth = new float[] { 1.5f, 2f, 5f, 3f, 1.8f, 2f, 1.8f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 2f, 1.5f, 1.5f, 1.5f, 2f, 2f, 1.5f, 2f, 6f };
                    SecondtableHeadings.SetWidths(SecondHeadingsWidth);

                    //Cell Headings
                    //1
                    PdfPCell sNo = new PdfPCell(new Phrase("S.No", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    sNo.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                    //sNo.Colspan = 1;
                    sNo.Border = 15;// 15;
                    SecondtableHeadings.AddCell(sNo);
                    //2
                    PdfPCell CEmpId = new PdfPCell(new Phrase("Emp Id", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    CEmpId.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    CEmpId.Border = 15;// 15;
                    SecondtableHeadings.AddCell(CEmpId);
                    //3
                    PdfPCell CEmpName = new PdfPCell(new Phrase("Emp Name", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    CEmpName.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    CEmpName.Border = 15;// 15;
                    SecondtableHeadings.AddCell(CEmpName);
                    //4
                    PdfPCell CDesgn = new PdfPCell(new Phrase("Desgn", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    CDesgn.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    CDesgn.Border = 15;
                    SecondtableHeadings.AddCell(CDesgn);
                    //5
                    PdfPCell CDuties = new PdfPCell(new Phrase("Dts", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    CDuties.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    CDuties.Border = 15;
                    SecondtableHeadings.AddCell(CDuties);
                    //6
                    PdfPCell COTs = new PdfPCell(new Phrase("Ots", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    COTs.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    COTs.Border = 15;
                    // Secondtable.AddCell(COTs);
                    //7
                    PdfPCell CBasic = new PdfPCell(new Phrase("Basic", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    CBasic.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    CBasic.Border = 15;
                    SecondtableHeadings.AddCell(CBasic);
                    //8
                    PdfPCell CDa = new PdfPCell(new Phrase("DA", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    CBasic.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    CBasic.Border = 15;
                    SecondtableHeadings.AddCell(CDa);

                    //9

                    PdfPCell CHRa = new PdfPCell(new Phrase("HRA", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    CHRa.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    CHRa.Border = 15;
                    SecondtableHeadings.AddCell(CHRa);

                    //10
                    PdfPCell Cconveyance = new PdfPCell(new Phrase("Conv", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    Cconveyance.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    Cconveyance.Border = 15;
                    SecondtableHeadings.AddCell(Cconveyance);
                    //11
                    PdfPCell CCca = new PdfPCell(new Phrase("CCA", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    CCca.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    CCca.Border = 15;
                    SecondtableHeadings.AddCell(CCca);
                    //12
                    PdfPCell Cwa = new PdfPCell(new Phrase("WA", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    Cwa.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    Cwa.Border = 15;
                    SecondtableHeadings.AddCell(Cwa);
                    //13
                    PdfPCell COa = new PdfPCell(new Phrase("OA", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    COa.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    COa.Border = 15;
                    // SecondtableHeadings.AddCell(COa);


                    //14
                    PdfPCell Cbonus = new PdfPCell(new Phrase("Bonus", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    Cbonus.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    Cbonus.Border = 15;
                    // SecondtableHeadings.AddCell(Cbonus);


                    //15
                    PdfPCell Cotamt = new PdfPCell(new Phrase("OTamt", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    Cotamt.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    Cotamt.Border = 15;
                    // Secondtable.AddCell(Cotamt);
                    //16

                    PdfPCell CGross = new PdfPCell(new Phrase("Gross", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    CGross.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    CGross.Border = 15;
                    SecondtableHeadings.AddCell(CGross);
                    //17
                    PdfPCell CPF = new PdfPCell(new Phrase("PF", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    CPF.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    CPF.Border = 15;
                    SecondtableHeadings.AddCell(CPF);
                    //18
                    PdfPCell CESI = new PdfPCell(new Phrase("ESI", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    CESI.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    CESI.Border = 15;
                    SecondtableHeadings.AddCell(CESI);
                    //19
                    PdfPCell CPT = new PdfPCell(new Phrase("PT", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    CPT.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    CPT.Border = 15;
                    SecondtableHeadings.AddCell(CPT);
                    //20
                    PdfPCell CSalAdv = new PdfPCell(new Phrase("Sal Adv", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    CSalAdv.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    CSalAdv.Border = 15;
                    SecondtableHeadings.AddCell(CSalAdv);
                    //21
                    PdfPCell CUnifDed = new PdfPCell(new Phrase("Unif. Ded", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    CUnifDed.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    CUnifDed.Border = 15;
                    SecondtableHeadings.AddCell(CUnifDed);

                    //22
                    PdfPCell Ccanteended = new PdfPCell(new Phrase("Mess. Ded", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    Ccanteended.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    Ccanteended.Border = 15;
                    // SecondtableHeadings.AddCell(Ccanteended);

                    //23
                    PdfPCell COWF = new PdfPCell(new Phrase("A.R", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                    COWF.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    COWF.Border = 15;
                    SecondtableHeadings.AddCell(COWF);
                    //24
                    PdfPCell COtherDed = new PdfPCell(new Phrase("Other Ded", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                    COtherDed.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    COtherDed.Border = 15;
                    SecondtableHeadings.AddCell(COtherDed);
                    //25
                    PdfPCell CTotDed = new PdfPCell(new Phrase("Tot Ded", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                    CTotDed.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    CTotDed.Border = 15;
                    SecondtableHeadings.AddCell(CTotDed);
                    //26
                    PdfPCell CNetPay = new PdfPCell(new Phrase("Total Pay", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    CNetPay.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    CNetPay.Border = 15;
                    SecondtableHeadings.AddCell(CNetPay);

                    //27
                    PdfPCell CRoundoff = new PdfPCell(new Phrase("Round Off", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    CRoundoff.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    CRoundoff.Border = 15;
                    SecondtableHeadings.AddCell(CRoundoff);
                    //28
                    PdfPCell CNetPay2 = new PdfPCell(new Phrase("Net Pay", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                    CNetPay2.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    CNetPay2.Border = 15;
                    SecondtableHeadings.AddCell(CNetPay2);

                    //29
                    PdfPCell CSignature = new PdfPCell(new Phrase("Bank A/c No./ Signature", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                    CSignature.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    CSignature.Border = 15;
                    SecondtableHeadings.AddCell(CSignature);



                    #endregion

                    PdfPTable Secondtable = new PdfPTable(tableCells);
                    Secondtable.TotalWidth = 950f;
                    Secondtable.LockedWidth = true;
                    float[] SecondWidth = new float[] { 1.5f, 2f, 5f, 3f, 1.8f, 2f, 1.8f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 2f, 1.5f, 1.5f, 1.5f, 2f, 2f, 1.5f, 2f, 6f };
                    Secondtable.SetWidths(SecondWidth);

                    #region Table Data
                    int rowCount = 0;
                    //int pageCount = 0;
                    int slipsCount = 0;
                    int i = nextpagei;

                    //if (int i=nextpagei)
                    {
                        forConvert = 0;
                        if (dt.Rows[i]["NoOfDuties"].ToString().Trim().Length > 0)
                            forConvert = Convert.ToSingle(dt.Rows[i]["NoOfDuties"].ToString());

                        if (forConvert > 0)
                        {
                            if (nextpagerecordscount == 0)
                            {
                                document.Add(SecondtableHeadings);
                            }

                            nextpagerecordscount++;
                            //1
                            PdfPCell CSNo = new PdfPCell(new Phrase((++j).ToString() + " \n \n \n ", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                            CSNo.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            CSNo.Border = 15;
                            Secondtable.AddCell(CSNo);
                            //2
                            PdfPCell CEmpId1 = new PdfPCell(new Phrase(dt.Rows[i]["EmpId"].ToString(), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                            CEmpId1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            CEmpId1.Border = 15;
                            Secondtable.AddCell(CEmpId1);
                            //3
                            PdfPCell CEmpName1 = new PdfPCell(new Phrase(dt.Rows[i]["EmpMName"].ToString(), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                            CEmpName1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            CEmpName1.Border = 15;
                            Secondtable.AddCell(CEmpName1);
                            //4
                            PdfPCell CEmpDesgn = new PdfPCell(new Phrase(dt.Rows[i]["Desgn"].ToString(), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                            CEmpDesgn.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            CEmpDesgn.Border = 15;
                            Secondtable.AddCell(CEmpDesgn);
                            //5
                            forConvert = 0;
                            if (dt.Rows[i]["NoOfDuties"].ToString().Trim().Length > 0)
                                forConvert = Convert.ToSingle(dt.Rows[i]["NoOfDuties"].ToString());
                            totalDuties += forConvert;
                            GrandtotalDuties += forConvert;
                            PdfPCell CNoOfDuties = new PdfPCell(new Phrase(forConvert.ToString("0.00"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                            CNoOfDuties.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            CNoOfDuties.Border = 15;
                            Secondtable.AddCell(CNoOfDuties);
                            //6
                            if (dt.Rows[i]["ot"].ToString().Trim().Length > 0)
                                forConvert = Convert.ToSingle(dt.Rows[i]["ot"].ToString());
                            totalOts += forConvert;
                            GrandtotalOts += forConvert;
                            PdfPCell CNoOfots = new PdfPCell(new Phrase(forConvert.ToString("0.0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                            CNoOfots.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            CNoOfots.Border = 15;
                            //Secondtable.AddCell(CNoOfots);

                            //7
                            forConvert = 0;
                            if (dt.Rows[i]["Basic"].ToString().Trim().Length > 0)
                                forConvert = (float)Math.Round(Convert.ToSingle(dt.Rows[i]["Basic"].ToString()));
                            totalBasic += forConvert;
                            GrandtotalBasic += forConvert;
                            PdfPCell CBasic1 = new PdfPCell(new Phrase(forConvert.ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                            CBasic1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            CBasic1.Border = 15;
                            Secondtable.AddCell(CBasic1);
                            //8

                            forConvert = 0;

                            if (dt.Rows[i]["DA"].ToString().Trim().Length > 0)
                                forConvert = (float)Math.Round(Convert.ToSingle(dt.Rows[i]["DA"].ToString()));
                            totalDA += forConvert;
                            GrandtotalDA += forConvert;
                            PdfPCell CDa1 = new PdfPCell(new Phrase(forConvert.ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                            CDa1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            CDa1.Border = 15;
                            Secondtable.AddCell(CDa1);

                            //9

                            forConvert = 0;
                            if (dt.Rows[i]["HRA"].ToString().Trim().Length > 0)
                                forConvert = (float)Math.Round(Convert.ToSingle(dt.Rows[i]["HRA"].ToString()));
                            totalHRA += forConvert;
                            GrandtotalHRA += forConvert;
                            PdfPCell CHRA1 = new PdfPCell(new Phrase(forConvert.ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                            CHRA1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            CHRA1.Border = 15;
                            Secondtable.AddCell(CHRA1);

                            //10
                            forConvert = 0;
                            if (dt.Rows[i]["Conveyance"].ToString().Trim().Length > 0)
                                forConvert = (float)Math.Round(Convert.ToSingle(dt.Rows[i]["Conveyance"].ToString()));
                            totalConveyance += forConvert;
                            GrandtotalConveyance += forConvert;
                            PdfPCell CConveyance = new PdfPCell(new Phrase(forConvert.ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                            CConveyance.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            CConveyance.Border = 15;
                            Secondtable.AddCell(CConveyance);

                            //11
                            forConvert = 0;
                            if (dt.Rows[i]["cca"].ToString().Trim().Length > 0)
                                forConvert = (float)Math.Round(Convert.ToSingle(dt.Rows[i]["cca"].ToString()));
                            totalCCA += forConvert;
                            GrandtotalCCA += forConvert;
                            PdfPCell Ccca = new PdfPCell(new Phrase(forConvert.ToString(), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                            Ccca.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            Ccca.Border = 15;
                            Secondtable.AddCell(Ccca);
                            //12
                            forConvert = 0;
                            if (dt.Rows[i]["washallowance"].ToString().Trim().Length > 0)
                                forConvert = (float)Math.Round(Convert.ToSingle(dt.Rows[i]["washallowance"].ToString()));
                            totalWA += forConvert;
                            GrandtotalWA += forConvert;
                            PdfPCell CWa = new PdfPCell(new Phrase(forConvert.ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                            CWa.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            CWa.Border = 15;
                            Secondtable.AddCell(CWa);
                            //13
                            forConvert = 0;
                            if (dt.Rows[i]["OtherAllowance"].ToString().Trim().Length > 0)
                                forConvert = (float)Math.Round(Convert.ToSingle(dt.Rows[i]["OtherAllowance"].ToString()));
                            totalOA += forConvert;
                            GrandtotalOA += forConvert;
                            PdfPCell COA = new PdfPCell(new Phrase(forConvert.ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                            COA.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            COA.Border = 15;
                            //  Secondtable.AddCell(COA);


                            //14
                            forConvert = 0;
                            if (dt.Rows[i]["Bonus"].ToString().Trim().Length > 0)
                                forConvert = (float)Math.Round(Convert.ToSingle(dt.Rows[i]["Bonus"].ToString()));
                            totalOA += forConvert;
                            GrandtotalOA += forConvert;
                            PdfPCell CBonus = new PdfPCell(new Phrase(forConvert.ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                            CBonus.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            CBonus.Border = 15;
                            //Secondtable.AddCell(CBonus);

                            //15

                            forConvert = 0;
                            if (dt.Rows[i]["otamt"].ToString().Trim().Length > 0)
                                forConvert = (float)Math.Round(Convert.ToSingle(dt.Rows[i]["otamt"].ToString()));
                            totalOTAmount += forConvert;

                            //16
                            forConvert = 0;
                            if (dt.Rows[i]["Gross"].ToString().Trim().Length > 0)
                                forConvert = (float)Math.Round(Convert.ToSingle(dt.Rows[i]["Gross"].ToString()));
                            totalGrass += forConvert;
                            GrandtotalGrass += forConvert;
                            PdfPCell CGross1 = new PdfPCell(new Phrase(forConvert.ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                            CGross1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            CGross1.Border = 15;
                            Secondtable.AddCell(CGross1);
                            //17
                            forConvert = 0;
                            if (dt.Rows[i]["PF"].ToString().Trim().Length > 0)
                                forConvert = (float)Math.Round(Convert.ToSingle(dt.Rows[i]["PF"].ToString()));
                            totalPF += forConvert;
                            GrandtotalPF += forConvert;
                            PdfPCell CPF1 = new PdfPCell(new Phrase(forConvert.ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                            CPF1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            CPF1.Border = 15;
                            Secondtable.AddCell(CPF1);
                            //18
                            forConvert = 0;
                            if (dt.Rows[i]["ESI"].ToString().Trim().Length > 0)
                                forConvert = (float)Math.Round(Convert.ToSingle(dt.Rows[i]["ESI"].ToString()));
                            totalESI += forConvert;
                            GrandtotalESI += forConvert;
                            PdfPCell CESI1 = new PdfPCell(new Phrase(forConvert.ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                            CESI1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            CESI1.Border = 15;
                            Secondtable.AddCell(CESI1);
                            //19
                            forConvert = 0;
                            if (dt.Rows[i]["ProfTax"].ToString().Trim().Length > 0)
                                forConvert = (float)Math.Round(Convert.ToSingle(dt.Rows[i]["ProfTax"].ToString()));
                            totalProfTax += forConvert;
                            GrandtotalProfTax += forConvert;
                            PdfPCell CProTax1 = new PdfPCell(new Phrase(forConvert.ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                            CProTax1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            CProTax1.Border = 15;
                            Secondtable.AddCell(CProTax1);
                            //20
                            forConvert = 0;
                            if (dt.Rows[i]["SalAdvDed"].ToString().Trim().Length > 0)
                                forConvert = (float)Math.Round(Convert.ToSingle(dt.Rows[i]["SalAdvDed"].ToString()));
                            totalSalAdv += forConvert;
                            GrandtotalSalAdv += forConvert;
                            PdfPCell CSalAdv1 = new PdfPCell(new Phrase(forConvert.ToString(), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                            CSalAdv1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            CSalAdv1.Border = 15;
                            Secondtable.AddCell(CSalAdv1);
                            //21
                            forConvert = 0;
                            if (dt.Rows[i]["UniformDed"].ToString().Trim().Length > 0)
                                forConvert = (float)Math.Round(Convert.ToSingle(dt.Rows[i]["UniformDed"].ToString()));
                            totalUniforDed += forConvert;
                            GrandtotalUniforDed += forConvert;
                            PdfPCell CUnifDed1 = new PdfPCell(new Phrase(forConvert.ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                            CUnifDed1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            CUnifDed1.Border = 15;
                            Secondtable.AddCell(CUnifDed1);
                            //22

                            forConvert = 0;
                            float individualCantAdv = 0;
                            if (dt.Rows[i]["CanteenAdv"].ToString().Trim().Length > 0)
                                forConvert = (float)Math.Round(Convert.ToSingle(dt.Rows[i]["CanteenAdv"].ToString()));
                            totalCanteenAdv += forConvert;
                            individualCantAdv = forConvert;
                            GrandtotalCanteenAdv += forConvert;

                            PdfPCell CCanteended = new PdfPCell(new Phrase(forConvert.ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                            CCanteended.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            CCanteended.Border = 15;
                            // Secondtable.AddCell(CCanteended);

                            //23
                            if (dt.Rows[i]["OWFAmt"].ToString().Trim().Length > 0)
                                forConvert = (float)Math.Round(Convert.ToSingle(dt.Rows[i]["OWFAmt"].ToString()));
                            totalOwf += forConvert;
                            GrandtotalOwf += forConvert;
                            PdfPCell COwf1 = new PdfPCell(new Phrase(forConvert.ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                            COwf1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            COwf1.Border = 15;
                            Secondtable.AddCell(COwf1);
                            //24
                            forConvert = 0;


                            if (dt.Rows[i]["Penalty"].ToString().Trim().Length > 0)
                                forConvert = (float)Math.Round(Convert.ToSingle(dt.Rows[i]["Penalty"].ToString()));
                            totalPenalty += (forConvert + individualCantAdv);
                            GrandtotalPenalty += forConvert;
                            PdfPCell COtherDed1 = new PdfPCell(new Phrase(forConvert.ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                            COtherDed1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            COtherDed1.Border = 15;
                            Secondtable.AddCell(COtherDed1);
                            //25
                            forConvert = 0;
                            float totaldeductions = 0;
                            if (dt.Rows[i]["Deductions"].ToString().Trim().Length > 0)
                            {
                                forConvert = (float)Math.Round(Convert.ToSingle(dt.Rows[i]["Deductions"].ToString()));
                                totaldeductions = forConvert;
                            }
                            totalDed += forConvert;
                            GrandtotalDed += forConvert;
                            PdfPCell CTotDed1 = new PdfPCell(new Phrase(forConvert.ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                            CTotDed1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            CTotDed1.Border = 15;
                            Secondtable.AddCell(CTotDed1);//OtherDed,Eps.Gross,Eps.Deductions,Eps.ActualAmount
                            //26
                            forConvert = 0;


                            float Remainder = 0;
                            float totalpay = 0;

                            if (dt.Rows[i]["gross"].ToString().Trim().Length > 0)
                            {
                                forConvert = (float)Math.Round(Convert.ToSingle(dt.Rows[i]["gross"].ToString()));

                            }
                            if (forConvert < 0)
                            {
                                forConvert = 0;
                            }

                            if (forConvert > 0)
                            {
                                forConvert = forConvert - totaldeductions;
                                Remainder = forConvert % 100;
                            }

                            totalActualamount += forConvert;
                            GrandtotalActualamount += forConvert;

                            totalpay = forConvert;
                            PdfPCell CNetPay1 = new PdfPCell(new Phrase(totalpay.ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                            CNetPay1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            CNetPay1.Border = 15;
                            Secondtable.AddCell(CNetPay1);
                            //27


                            totalroundoff += Remainder;
                            Grandtotalroundoff += (Remainder);


                            if (Remainder > 0 && Remainder <= 50)
                            {
                                forConvert = forConvert - Remainder;
                            }

                            if (Remainder > 50 && Remainder <= 99)
                            {
                                forConvert = forConvert - Remainder;
                                forConvert = forConvert + 100;
                            }

                            totalnetpay += forConvert;
                            Grandtotalnetpay += forConvert;

                            Remainder = forConvert - totalpay;
                            PdfPCell Croundoff = new PdfPCell(new Phrase(Remainder.ToString(), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                            Croundoff.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            Croundoff.Border = 15;
                            Secondtable.AddCell(Croundoff);
                            //28

                            PdfPCell Cnetpayr = new PdfPCell(new Phrase(forConvert.ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                            Cnetpayr.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            Cnetpayr.Border = 15;
                            Cnetpayr.Border = PdfPCell.BOTTOM_BORDER;
                            Secondtable.AddCell(Cnetpayr);

                            //29
                            string EmpBankAcNo = dt.Rows[i]["EmpBankAcNo"].ToString();
                            PdfPCell CSignature1 = new PdfPCell(new Phrase(EmpBankAcNo, FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                            CSignature1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            //CSignature1.Border = 
                            CSignature1.MinimumHeight = 25;
                            Secondtable.AddCell(CSignature1);
                        }

                    }

                    #endregion

                    #region Comment the foote code

                    SecondtableFooter = new PdfPTable(tableCells);
                    SecondtableFooter.TotalWidth = 950f;
                    SecondtableFooter.LockedWidth = true;
                    float[] SecondFooterWidth = new float[] { 1.5f, 2f, 5f, 3f, 1.8f, 2f, 1.8f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 2f, 1.5f, 1.5f, 1.5f, 2f, 2f, 1.5f, 2f, 6f };
                    SecondtableFooter.SetWidths(SecondFooterWidth);

                    #region Table Footer
                    //1
                    PdfPCell FCSNo = new PdfPCell(new Phrase("", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                    FCSNo.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCSNo.Border = 15;
                    SecondtableFooter.AddCell(FCSNo);
                    //2
                    PdfPCell FCEmpId1 = new PdfPCell(new Phrase("", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                    FCEmpId1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCEmpId1.Border = 15;
                    SecondtableFooter.AddCell(FCEmpId1);
                    //3
                    PdfPCell FCEmpName1 = new PdfPCell(new Phrase("Total : ", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                    FCEmpName1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCEmpName1.Border = 15;
                    SecondtableFooter.AddCell(FCEmpName1);
                    //4
                    PdfPCell FCEmpDesgn = new PdfPCell(new Phrase("", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                    FCEmpDesgn.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCEmpDesgn.Border = 15;
                    //FCEmpDesgn.Colspan = 4;
                    SecondtableFooter.AddCell(FCEmpDesgn);
                    //5
                    PdfPCell FCNoOfDuties = new PdfPCell(new Phrase(totalDuties.ToString("0.00"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    FCNoOfDuties.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCNoOfDuties.Border = 15;
                    SecondtableFooter.AddCell(FCNoOfDuties);
                    //6
                    PdfPCell FCNoOfots = new PdfPCell(new Phrase(totalOts.ToString("0.00"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    FCNoOfots.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCNoOfots.Border = 15;
                    //Secondtable.AddCell(FCNoOfots);

                    //7
                    PdfPCell FCBasic1 = new PdfPCell(new Phrase(Math.Round(totalBasic).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    FCBasic1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCBasic1.Border = 15;
                    SecondtableFooter.AddCell(FCBasic1);


                    //8
                    PdfPCell FDa = new PdfPCell(new Phrase(Math.Round(totalDA).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    FDa.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FDa.Border = 15;
                    SecondtableFooter.AddCell(FDa);


                    //9

                    forConvert = 0;
                    PdfPCell FCHRA1 = new PdfPCell(new Phrase(Math.Round(totalHRA).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    FCHRA1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCHRA1.Border = 15;
                    SecondtableFooter.AddCell(FCHRA1);

                    //10
                    PdfPCell FCConveyance = new PdfPCell(new Phrase(Math.Round(totalConveyance).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    FCConveyance.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCConveyance.Border = 15;
                    SecondtableFooter.AddCell(FCConveyance);

                    //11
                    PdfPCell FCcca = new PdfPCell(new Phrase(Math.Round(totalCCA).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    FCcca.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCcca.Border = 15;
                    SecondtableFooter.AddCell(FCcca);
                    //12
                    PdfPCell FCWa = new PdfPCell(new Phrase(Math.Round(totalWA).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    FCWa.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCWa.Border = 15;
                    SecondtableFooter.AddCell(FCWa);

                    //13
                    PdfPCell FCOa = new PdfPCell(new Phrase(Math.Round(totalOA).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    FCOa.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCOa.Border = 15;
                    // SecondtableFooter.AddCell(FCOa);

                    //14
                    PdfPCell Fbonus = new PdfPCell(new Phrase(Math.Round(totalbonus).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    Fbonus.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    Fbonus.Border = 15;
                    //  SecondtableFooter.AddCell(Fbonus);

                    //15
                    PdfPCell FCottamt = new PdfPCell(new Phrase(Math.Round(totalOTAmount).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    FCottamt.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCottamt.Border = 15;
                    // Secondtable.AddCell(FCottamt);
                    //16
                    PdfPCell FCGross1 = new PdfPCell(new Phrase(Math.Round(totalGrass).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    FCGross1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCGross1.Border = 15;
                    SecondtableFooter.AddCell(FCGross1);
                    //17
                    PdfPCell FCPF1 = new PdfPCell(new Phrase(Math.Round(totalPF).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    FCPF1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCPF1.Border = 15;
                    SecondtableFooter.AddCell(FCPF1);
                    //18
                    PdfPCell FCESI1 = new PdfPCell(new Phrase(Math.Round(totalESI).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    FCESI1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCESI1.Border = 15;
                    SecondtableFooter.AddCell(FCESI1);
                    //19
                    PdfPCell FCProTax1 = new PdfPCell(new Phrase(Math.Round(totalProfTax).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    FCProTax1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCProTax1.Border = 15;
                    SecondtableFooter.AddCell(FCProTax1);
                    //20
                    PdfPCell FCSalAdv1 = new PdfPCell(new Phrase(Math.Round(totalSalAdv).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    FCSalAdv1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCSalAdv1.Border = 15;
                    SecondtableFooter.AddCell(FCSalAdv1);
                    //21
                    PdfPCell FCUnifDed1 = new PdfPCell(new Phrase(Math.Round(totalUniforDed).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    FCUnifDed1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCUnifDed1.Border = 15;
                    SecondtableFooter.AddCell(FCUnifDed1);
                    //22
                    PdfPCell FCCanteended = new PdfPCell(new Phrase(Math.Round(totalCanteenAdv).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    FCCanteended.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCCanteended.Border = 15;
                    //SecondtableFooter.AddCell(FCCanteended);
                    //23
                    PdfPCell FCOwf1 = new PdfPCell(new Phrase(Math.Round(totalOwf).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    FCOwf1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCOwf1.Border = 15;
                    SecondtableFooter.AddCell(FCOwf1);
                    //24
                    PdfPCell FCOtherDed1 = new PdfPCell(new Phrase(Math.Round(totalPenalty).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    FCOtherDed1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCOtherDed1.Border = 15;
                    SecondtableFooter.AddCell(FCOtherDed1);
                    //25
                    PdfPCell FCTotDed1 = new PdfPCell(new Phrase(Math.Round(totalDed).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    FCTotDed1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCTotDed1.Border = 15;
                    SecondtableFooter.AddCell(FCTotDed1);//OtherDed,Eps.Gross,Eps.Deductions,Eps.ActualAmount
                    //26
                    PdfPCell FCNetPay1 = new PdfPCell(new Phrase(Math.Round(totalActualamount).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    FCNetPay1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCNetPay1.Border = 15;
                    //FCNetPay1.Colspan = 2;
                    SecondtableFooter.AddCell(FCNetPay1);
                    //27
                    PdfPCell FCroundoff = new PdfPCell(new Phrase(Math.Round(totalActualamount).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    FCroundoff.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCroundoff.Border = 15;
                    //FCNetPay1.Colspan = 2;
                    SecondtableFooter.AddCell(FCroundoff);


                    //28
                    PdfPCell FNetpayr = new PdfPCell(new Phrase(Math.Round(totalActualamount).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    FNetpayr.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FNetpayr.Border = 15;
                    //FCNetPay1.Colspan = 2;
                    SecondtableFooter.AddCell(FNetpayr);



                    //29
                    PdfPCell FCSignature1 = new PdfPCell(new Phrase(" ", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                    FCSignature1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    FCSignature1.Border = 15;
                    //FCSignature1.MinimumHeight = 25;

                    SecondtableFooter.AddCell(FCSignature1);
                    #endregion



                    SecondtablecheckedbyFooter = new PdfPTable(tableCells);
                    SecondtablecheckedbyFooter.TotalWidth = 950f;
                    SecondtablecheckedbyFooter.LockedWidth = true;
                    float[] SecondcheckedFooterWidth = new float[] { 1.5f, 2f, 5f, 3f, 1.8f, 2f, 1.8f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 2f, 1.5f, 1.5f, 1.5f, 2f, 2f, 1.5f, 2f, 6f };
                    SecondtablecheckedbyFooter.SetWidths(SecondcheckedFooterWidth);



                    #region Table  Grand  Total   Footer
                    //1
                    PdfPCell GFCSNo = new PdfPCell(new Phrase("", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                    GFCSNo.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCSNo.Border = 15;
                    SecondtablecheckedbyFooter.AddCell(GFCSNo);
                    //2
                    PdfPCell GFCEmpId1 = new PdfPCell(new Phrase("", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                    GFCEmpId1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCEmpId1.Border = 15;
                    SecondtablecheckedbyFooter.AddCell(GFCEmpId1);
                    //3
                    PdfPCell GFCEmpName1 = new PdfPCell(new Phrase(" Grand  Total: ", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                    GFCEmpName1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCEmpName1.Border = 15;
                    SecondtablecheckedbyFooter.AddCell(GFCEmpName1);
                    //4
                    PdfPCell GFCEmpDesgn = new PdfPCell(new Phrase("", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                    GFCEmpDesgn.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCEmpDesgn.Border = 15;
                    //FCEmpDesgn.Colspan = 4;
                    SecondtablecheckedbyFooter.AddCell(GFCEmpDesgn);
                    //5
                    PdfPCell GFCNoOfDuties = new PdfPCell(new Phrase(GrandtotalDuties.ToString("0.00"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    GFCNoOfDuties.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCNoOfDuties.Border = 15;
                    SecondtablecheckedbyFooter.AddCell(GFCNoOfDuties);
                    //6
                    PdfPCell GFCNoOfots = new PdfPCell(new Phrase(GrandtotalOts.ToString("0.00"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    GFCNoOfots.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCNoOfots.Border = 15;
                    //Secondtable.AddCell(GFCNoOfots);

                    //7
                    PdfPCell GFCBasic1 = new PdfPCell(new Phrase(Math.Round(GrandtotalBasic).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    GFCBasic1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCBasic1.Border = 15;
                    SecondtablecheckedbyFooter.AddCell(GFCBasic1);


                    //8
                    PdfPCell GFDa = new PdfPCell(new Phrase(Math.Round(GrandtotalDA).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    GFDa.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFDa.Border = 15;
                    SecondtablecheckedbyFooter.AddCell(GFDa);


                    //9

                    forConvert = 0;
                    PdfPCell GFCHRA1 = new PdfPCell(new Phrase(Math.Round(GrandtotalHRA).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    GFCHRA1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCHRA1.Border = 15;
                    SecondtablecheckedbyFooter.AddCell(GFCHRA1);

                    //10
                    PdfPCell GFCConveyance = new PdfPCell(new Phrase(Math.Round(GrandtotalConveyance).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    GFCConveyance.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCConveyance.Border = 15;
                    SecondtablecheckedbyFooter.AddCell(GFCConveyance);

                    //11
                    PdfPCell GFCcca = new PdfPCell(new Phrase(Math.Round(GrandtotalCCA).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    GFCcca.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCcca.Border = 15;
                    SecondtablecheckedbyFooter.AddCell(GFCcca);
                    //12
                    PdfPCell GFCWa = new PdfPCell(new Phrase(Math.Round(GrandtotalWA).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    GFCWa.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCWa.Border = 15;
                    SecondtablecheckedbyFooter.AddCell(GFCWa);

                    //13
                    PdfPCell GFCOa = new PdfPCell(new Phrase(Math.Round(GrandtotalOA).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    GFCOa.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCOa.Border = 15;
                    //SecondtablecheckedbyFooter.AddCell(GFCOa);

                    //14
                    PdfPCell GFbonus = new PdfPCell(new Phrase(Math.Round(GrandtotalOA).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    GFbonus.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFbonus.Border = 15;
                    //SecondtablecheckedbyFooter.AddCell(GFbonus);

                    //15
                    PdfPCell GFCottamt = new PdfPCell(new Phrase(Math.Round(GrandtotalOTAmount).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    GFCottamt.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCottamt.Border = 15;
                    // Secondtable.AddCell(GFCottamt);
                    //16
                    PdfPCell GFCGross1 = new PdfPCell(new Phrase(Math.Round(GrandtotalGrass).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    GFCGross1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCGross1.Border = 15;
                    SecondtablecheckedbyFooter.AddCell(GFCGross1);
                    //17
                    PdfPCell GFCPF1 = new PdfPCell(new Phrase(Math.Round(GrandtotalPF).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    GFCPF1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCPF1.Border = 15;
                    SecondtablecheckedbyFooter.AddCell(GFCPF1);
                    //18
                    PdfPCell GFCESI1 = new PdfPCell(new Phrase(Math.Round(GrandtotalESI).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    GFCESI1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCESI1.Border = 15;
                    SecondtablecheckedbyFooter.AddCell(GFCESI1);
                    //19
                    PdfPCell GFCProTax1 = new PdfPCell(new Phrase(Math.Round(GrandtotalProfTax).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    GFCProTax1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCProTax1.Border = 15;
                    SecondtablecheckedbyFooter.AddCell(GFCProTax1);
                    //20
                    PdfPCell GFCSalAdv1 = new PdfPCell(new Phrase(Math.Round(GrandtotalSalAdv).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    GFCSalAdv1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCSalAdv1.Border = 15;
                    SecondtablecheckedbyFooter.AddCell(GFCSalAdv1);
                    //21
                    PdfPCell GFCUnifDed1 = new PdfPCell(new Phrase(Math.Round(GrandtotalUniforDed).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    GFCUnifDed1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCUnifDed1.Border = 15;
                    SecondtablecheckedbyFooter.AddCell(GFCUnifDed1);
                    //22
                    PdfPCell GFCCanteended = new PdfPCell(new Phrase(Math.Round(GrandtotalCanteenAdv).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    GFCCanteended.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCCanteended.Border = 15;
                    //SecondtablecheckedbyFooter.AddCell(GFCCanteended);
                    //23
                    PdfPCell GFCOwf1 = new PdfPCell(new Phrase(Math.Round(GrandtotalOwf).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    GFCOwf1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCOwf1.Border = 15;
                    SecondtablecheckedbyFooter.AddCell(GFCOwf1);
                    //24
                    PdfPCell GFCOtherDed1 = new PdfPCell(new Phrase(Math.Round(GrandtotalPenalty).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    GFCOtherDed1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCOtherDed1.Border = 15;
                    SecondtablecheckedbyFooter.AddCell(GFCOtherDed1);
                    //25
                    PdfPCell GFCTotDed1 = new PdfPCell(new Phrase(Math.Round(GrandtotalDed).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    GFCTotDed1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCTotDed1.Border = 15;
                    SecondtablecheckedbyFooter.AddCell(GFCTotDed1);//OtherDed,Eps.Gross,Eps.Deductions,Eps.ActualAmount
                    //26
                    PdfPCell GFCNetPay1 = new PdfPCell(new Phrase(Math.Round(GrandtotalActualamount).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    GFCNetPay1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCNetPay1.Border = 15;
                    //FCNetPay1.Colspan = 2;
                    SecondtablecheckedbyFooter.AddCell(GFCNetPay1);
                    //27


                    Grandtotalroundoff = Grandtotalnetpay - GrandtotalActualamount;

                    //
                    PdfPCell GFCroundoff = new PdfPCell(new Phrase(Math.Round(Grandtotalroundoff).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    GFCroundoff.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCroundoff.Border = 15;
                    //FCNetPay1.Colspan = 2;
                    SecondtablecheckedbyFooter.AddCell(GFCroundoff);

                    //28
                    PdfPCell Gnetpayr = new PdfPCell(new Phrase(Math.Round(Grandtotalnetpay).ToString("0"), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                    Gnetpayr.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    Gnetpayr.Border = 15;
                    //FCNetPay1.Colspan = 2;
                    SecondtablecheckedbyFooter.AddCell(Gnetpayr);

                    //29
                    PdfPCell GFCSignature1 = new PdfPCell(new Phrase(" ", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                    GFCSignature1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    GFCSignature1.Border = 15;
                    //FCSignature1.MinimumHeight = 25;

                    SecondtablecheckedbyFooter.AddCell(GFCSignature1);
                    #endregion




                    #region   Footer Headings


                    //1
                    PdfPCell FHCheckedbybr1 = new PdfPCell(new Phrase("", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                    FHCheckedbybr1.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    FHCheckedbybr1.Border = 0;
                    FHCheckedbybr1.Rowspan = 0;
                    FHCheckedbybr1.Colspan = 24;
                    SecondtablecheckedbyFooter.AddCell(FHCheckedbybr1);
                    //2
                    PdfPCell FHApprovedbr2 = new PdfPCell(new Phrase("  ", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                    FHApprovedbr2.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    FHApprovedbr2.Border = 0;
                    FHApprovedbr2.Colspan = 24;
                    SecondtablecheckedbyFooter.AddCell(FHApprovedbr2);


                    SecondtablecheckedbyFooter.AddCell(FHCheckedbybr1);
                    SecondtablecheckedbyFooter.AddCell(FHApprovedbr2);

                    //1
                    PdfPCell FHCheckedby = new PdfPCell(new Phrase("Checked By  ", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                    FHCheckedby.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    FHCheckedby.Border = 0;
                    FHCheckedby.Rowspan = 0;
                    FHCheckedby.Colspan = 12;
                    SecondtablecheckedbyFooter.AddCell(FHCheckedby);
                    //2
                    PdfPCell FHApprovedBy = new PdfPCell(new Phrase("Gross  Approved By   ", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                    FHApprovedBy.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    FHApprovedBy.Border = 0;
                    FHApprovedBy.Colspan = 12;
                    SecondtablecheckedbyFooter.AddCell(FHApprovedBy);

                    #endregion

                    #endregion
                    document.Add(Secondtable);
                    //#region    Pdf New page and  all the totals are zero
                    if (nextpagerecordscount == targetpagerecors)
                    {
                        targetpagerecors = secondpagerecords;
                        // document.Add(SecondtableFooter);
                        document.NewPage();
                        nextpagerecordscount = 0;
                        #region  Zero variables

                        totalActualamount = 0;
                        totalDuties = 0;
                        totalOts = 0;
                        totalBasic = 0;
                        totalDA = 0;
                        totalHRA = 0;
                        totalCCA = 0;
                        totalConveyance = 0;
                        totalWA = 0;
                        totalOA = 0;
                        totalGrass = 0;
                        totalOTAmount = 0;
                        totalPF = 0;
                        totalESI = 0;
                        totalProfTax = 0;
                        totalDed = 0;
                        totalSalAdv = 0;
                        totalUniforDed = 0;
                        totalCanteenAdv = 0;
                        totalOwf = 0;
                        totalPenalty = 0;
                        totalbonus = 0;
                        totalnetpay = 0;
                        totalroundoff = 0;

                        #endregion
                    }
                }

                if (nextpagerecordscount >= 0)
                {
                    // document.Add(SecondtableFooter);
                    document.Add(SecondtablecheckedbyFooter);

                }

                //#endregion  
                if (nextpagehasPages)
                {
                    document.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=Only  Duties2.pdf");
                    Response.Buffer = true;
                    Response.Clear();
                    Response.OutputStream.Write(ms.GetBuffer(), 0, ms.GetBuffer().Length);
                    Response.OutputStream.Flush();
                    Response.End();
                }
            }
        }


        protected void btndownloadpdffile_Click(object sender, EventArgs e)
        {

            if (ddlpaymenttype.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showlalert", "alert('Please Select Payment Options ');", true);
                return;
            }

            if (ddlpaymenttype.SelectedIndex == 1)
            {
                btnwithotsheet_Click(sender, e);
                return;
            }

            if (ddlpaymenttype.SelectedIndex == 2)
            {
                btnwithotsheet2_Click(sender, e);
                return;
            }
            if (ddlpaymenttype.SelectedIndex == 3)
            {
                btnEmployeeWageSheet_Click(sender, e);
                return;
            }

            if (ddlpaymenttype.SelectedIndex == 4)
            {
                btnonlyots_Click(sender, e);
                return;
            }

            if (ddlpaymenttype.SelectedIndex == 5)
            {
                btnonlyduties2_Click(sender, e);
                return;
            }

            if (ddlpaymenttype.SelectedIndex == 6)
            {
                btnnopfesi_Click(sender, e);
                return;
            }

            if (ddlpaymenttype.SelectedIndex == 7)
            {
                //btnnopfesi_Click(sender, e);
                btnonlyduties3_Click(sender, e);
                return;
            }

            // if (ddlpaymenttype.SelectedIndex == 8)
            //{
            //    btnbellowtenattendance_Click(sender, e);
            //    return;
            //}

        }

        protected int GetMonth(string NameOfmonth)
        {
            int month = -1;
            var formatInfoinfo = new DateTimeFormatInfo();
            string[] monthName = formatInfoinfo.MonthNames;
            for (int i = 0; i < monthName.Length; i++)
            {
                if (monthName[i].CompareTo(NameOfmonth) == 0)
                {
                    month = i + 1;
                    break;
                }
            }
            return month;
        }




        protected string GetPaymentPeriod(string Clientid)
        {
            string period = "";
            string fromDate = "";
            string toDate = "";
            string selectstartingdate = "select ContractStartDate,ContractEndDate,BillDates from contracts where clientid = '" +
                ddlclient.SelectedValue + "'";
            DataTable dt = config.ExecuteAdaptorAsyncWithQueryParams(selectstartingdate).Result;

            if (dt.Rows.Count > 0)
            {
                string ContractStartDate = dt.Rows[0]["ContractStartDate"].ToString();
                string ContractEndDate = dt.Rows[0]["ContractEndDate"].ToString();
                string strBillDates = dt.Rows[0]["BillDates"].ToString();
                //bool bBillDates = false;
                //if (strBillDates.Length > 0)
                //{
                //    bool temp1 = Convert.ToBoolean(strBillDates);
                //    if (temp1 == true)
                //    {
                //        bBillDates = temp1;
                //    }
                //}

                int bBillDates = 0;
                if (strBillDates.Length > 0)
                {
                    int temp1 = Convert.ToInt32(strBillDates);
                    if (temp1 == 1)
                    {
                        bBillDates = temp1;
                    }
                }

                DateTime CSdate = DateTime.Parse(ContractStartDate);
                DateTime CurrentDate = DateTime.Parse(txtmonth.Text, CultureInfo.GetCultureInfo("en-gb"));
                DateTime lastDay = DateTime.Parse(txtmonth.Text, CultureInfo.GetCultureInfo("en-gb"));

                DateTime CEdate = DateTime.Parse(ContractEndDate);
                if (CSdate <= lastDay)
                {
                    if (bBillDates == 1)
                    {

                        if (CSdate.Day <= 15)
                        {
                            fromDate = (new DateTime(CurrentDate.Year, CurrentDate.Month, CSdate.Day).ToString("dd/MM/yyyy"));
                            DateTime tempDate = CurrentDate.AddMonths(1);

                            if (CSdate.Day == 1)
                            {
                                fromDate = (new DateTime(tempDate.Year, tempDate.Month, CSdate.Day /* - 1*/).ToString("dd/MM/yyyy"));
                            }
                            else
                            {
                                fromDate = (new DateTime(tempDate.Year, tempDate.Month, CSdate.Day).ToString("dd/MM/yyyy"));

                            }
                        }
                        else
                        {

                            if (CSdate.Month <= CurrentDate.Month - 1)
                            {
                                if (CurrentDate.Month == 1)
                                {
                                    fromDate = (new DateTime(CurrentDate.Year, CurrentDate.Month + 11, CSdate.Day).ToString("dd/MM/yyyy"));
                                }
                                else
                                {
                                    fromDate = (new DateTime(CurrentDate.Year, CurrentDate.Month - 1, CSdate.Day).ToString("dd/MM/yyyy"));

                                }
                                DateTime tempDate = CurrentDate.AddMonths(0);

                                if (CurrentDate.Day == 1)
                                {
                                    toDate = (new DateTime(tempDate.Year, tempDate.Month, CSdate.Day).ToString("dd/MM/yyyy"));
                                }
                                else
                                {
                                    toDate = (new DateTime(tempDate.Year, tempDate.Month, CSdate.Day - 1).ToString("dd/MM/yyyy"));

                                }
                            }

                            if (CSdate.Month != 0)
                            {
                                if (CurrentDate.Month == 1)
                                {
                                    fromDate = (new DateTime(CurrentDate.Year, CurrentDate.Month + 11, CSdate.Day).ToString("dd/MM/yyyy"));
                                }
                                else
                                {
                                    fromDate = (new DateTime(CurrentDate.Year, CurrentDate.Month - 1, CSdate.Day).ToString("dd/MM/yyyy"));
                                }
                                DateTime tempDate = CurrentDate.AddMonths(0);
                                //toDate = (new DateTime(tempDate.Year, tempDate.Month, CSdate.Day /* - 1*/).ToString("dd/MM/yyyy"));

                                if (CurrentDate.Day == 1)
                                {
                                    toDate = (new DateTime(tempDate.Year, tempDate.Month, CSdate.Day).ToString("dd/MM/yyyy"));
                                }
                                else
                                {
                                    toDate = (new DateTime(tempDate.Year, tempDate.Month, CSdate.Day - 1).ToString("dd/MM/yyyy"));

                                }


                            }
                        }

                    }
                    else
                    {
                        fromDate = (new DateTime(CurrentDate.Year, CurrentDate.Month/* - 1*/, 1).ToString("dd/MM/yyyy"));
                        toDate = (new DateTime(CurrentDate.Year, CurrentDate.Month/* - 1*/, DateTime.DaysInMonth(CurrentDate.Year, CurrentDate.Month)).ToString("dd/MM/yyyy"));
                    }
                    if (CurrentDate.Month/* - 1*/ == CSdate.Month && CurrentDate.Year == CSdate.Year)
                    {
                        DateTime date = new DateTime(CurrentDate.Year, CurrentDate.Month/* - 1*/, CSdate.Day);
                        //fromDate = date.ToString("MMM/dd/yyyy");
                        fromDate = date.ToString("dd/MM/yyyy");

                    }
                    if (CurrentDate.Month/* - 1*/ == CEdate.Month && CurrentDate.Year == CEdate.Year)
                    {
                        DateTime date = new DateTime(CurrentDate.Year, CurrentDate.Month/* - 1*/, CEdate.Day);
                        // toDate = date.ToString("MMM/dd/yyyy");
                        toDate = date.ToString("dd/MM/yyyy");
                    }
                }
                else
                {
                }
            }
            else
            {
            }
            if (fromDate.Length > 0 && toDate.Length > 0)
            {
                period = fromDate + " to " + toDate;
            }
            return period;
        }


        protected string Getmonth()
        {

            string Month = string.Empty;
            string Year = string.Empty;
            string date = "";

            if (txtmonth.Text.Trim().Length < 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showlalert", "alert('Please Fill Month');", true);
                return "";
            }

            if (txtmonth.Text.Trim().Length > 0)
            {
                date = DateTime.Parse(txtmonth.Text.Trim(), CultureInfo.GetCultureInfo("en-gb")).ToString();
            }

            Month = DateTime.Parse(date).Month.ToString();
            Year = DateTime.Parse(date).Year.ToString();

            string MonthandYear = Month + Year.Substring(2, 2);
            return MonthandYear;

        }

    }
}