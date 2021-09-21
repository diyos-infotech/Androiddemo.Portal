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
using Jawan.Portal.DAL;
namespace Jawan.Portal
{
    public partial class ReportforBulkClientbillings : System.Web.UI.Page
    {
        AppConfiguration config = new AppConfiguration();
        GridViewExportUtil gve = new GridViewExportUtil();
        DataTable dt;
        string EmpIDPrefix = "";
        string CmpIDPrefix = "";
        string Elength = "";
        string Clength = "";
        string FontStyle = "Tahoma";


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


        protected void btnsearch_Click(object sender, EventArgs e)
        {
            LblResult.Text = "";
            GVListEmployees.DataSource = null;
            GVListEmployees.DataBind();

            if (txtmonth.Text.Trim().Length == 0)
            {
                LblResult.Text = "Please Select Month";
                return;
            }

            var testDate = 0;
            if (txtmonth.Text.Trim().Length > 0)
            {
                testDate = GlobalData.Instance.CheckEnteredDate(txtmonth.Text);
                if (testDate > 0)
                {
                    LblResult.Text = "You Are Entered Invalid Date.Date Format Should be [DD/MM/YYYY].Ex.01/01/1990";
                    return;
                }

            }
            string date = DateTime.Parse(txtmonth.Text.Trim(), CultureInfo.GetCultureInfo("en-gb")).ToString();
            string month = DateTime.Parse(date).Month.ToString();
            string Year = DateTime.Parse(date).Year.ToString();


            #region Begin Variable Declaration as on [04-07-2014]
            var Month = "";
            var SPName = "";
            var clientids = "";
            DataTable DtClientListBasedonSelectedMonth = null;
            Hashtable HtClientListBasedonSelectedMonth = new Hashtable();
            #endregion End  Variable Declaration as on [04-07-2014]

            #region Begin Assign Values To Vriables as on [04-07-2014]
            Month = month + Year.Substring(2, 2);

            SPName = "GetClientsbulkbillprint";
            #endregion  End Assing Values to The Variables as on [04-07-2014]

            #region Begin  Pass Values To the Hash table as on [04-07-2014]
            HtClientListBasedonSelectedMonth.Add("@month", Month);
            //HtClientListBasedonSelectedMonth.Add("@clientids", clientids);
            #endregion end Pass Values To the Hash table as on [04-07-2014]

            #region  Begin Call Sp on [04-07-2014]
            DtClientListBasedonSelectedMonth =config.ExecuteAdaptorAsyncWithParams(SPName, HtClientListBasedonSelectedMonth).Result;
            GVListEmployees.DataSource = DtClientListBasedonSelectedMonth;
            GVListEmployees.DataBind();
            #endregion  end Call Sp on [04-07-2014]
        }


        protected void btnDownload_Click(object sender, EventArgs e)
        {
            int fontsize = 10;
            var Month = "";

            if (txtmonth.Text.Trim().Length == 0)
            {
                LblResult.Text = "Please Select Month";
                return;
            }

            var testDate = 0;
            if (txtmonth.Text.Trim().Length > 0)
            {
                testDate = GlobalData.Instance.CheckEnteredDate(txtmonth.Text);
                if (testDate > 0)
                {
                    LblResult.Text = "You Are Entered Invalid Date.Date Format Should be [DD/MM/YYYY].Ex.01/01/1990";
                    return;
                }

            }

            if (ddlOptions.SelectedIndex == 0)
            {


                string date = DateTime.Parse(txtmonth.Text.Trim(), CultureInfo.GetCultureInfo("en-gb")).ToString();
                string month = DateTime.Parse(date).Month.ToString();
                string Year = DateTime.Parse(date).Year.ToString();
                Month = month + Year.Substring(2, 2);
                var list = new List<string>();

                MemoryStream ms = new MemoryStream();
                try
                {
                    if (GVListEmployees.Rows.Count > 0)
                    {
                        for (int i = 0; i < GVListEmployees.Rows.Count; i++)
                        {
                            CheckBox chkclientid = GVListEmployees.Rows[i].FindControl("chkindividual") as CheckBox;
                            Label lblclientid = GVListEmployees.Rows[i].FindControl("lblclientid") as Label;

                            if (chkclientid.Checked == true)
                            {
                                list.Add(lblclientid.Text);
                            }
                        }
                    }
                    Document document = new Document(PageSize.A4);
                    PdfWriter writer = PdfWriter.GetInstance(document, ms);
                    document.Open();
                    BaseFont bf = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                    document.AddTitle("Webwonders");
                    document.AddAuthor("DIYOS");
                    document.AddSubject("Invoice");
                    document.AddKeywords("Keyword1, keyword2, …");
                    string imagepath = Server.MapPath("~/assets/BillLogo.png");

                    if (list.Count == 0)
                    {
                        LblResult.Text = "Please select atleast one client";
                        return;
                    }

                    if (list.Count != 0)
                    {
                        foreach (string clientid in list)
                        {

                            string companyName = "Your Company Name";
                            string companyAddress = "Your Company Address";
                            string companyaddressline = " ";
                            string Phoneno = "";
                            string Faxno = "";
                            string Emailid = "";
                            string Website = "";

                            string ServicetaxNo = string.Empty;
                            string PANNO = string.Empty;
                            string PFNo = string.Empty;
                            string Esino = string.Empty;

                            var ContractID = "";

                            string ServiceCharge = "0";
                            string strSCType = "";
                            string strDescription = "We are presenting our bill for the House Keeping Services Provided at your establishment. Kindly Release the payment as earliest";
                            bool bSCType = false;
                            string strIncludeST = "";
                            string strST75 = "";
                            bool bIncludeST = false;
                            bool bST75 = false;

                            string strStaxonservicecharge = "";
                            bool bStaxonservicecharge = false;

                            string BillNo = "";
                            DateTime BillDate;


                            #region Variables for data Fields as on 11/03/2014 by venkat


                            float servicecharge = 0;
                            float servicetax = 0;
                            float cess = 0;
                            float shecess = 0;
                            float totalamount = 0;
                            float Grandtotal = 0;

                            float ServiceTax75 = 0;
                            float ServiceTax25 = 0;

                            float machinarycost = 0;
                            float materialcost = 0;
                            float maintenancecost = 0;
                            float extraonecost = 0;
                            float extratwocost = 0;
                            float discountone = 0;
                            float discounttwo = 0;

                            string machinarycosttitle = "";
                            string materialcosttitle = "";
                            string maintenancecosttitle = "";
                            string extraonecosttitle = "";
                            string extratwocosttitle = "";
                            string discountonetitle = "";
                            string discounttwotitle = "";

                            bool Extradatacheck = false;
                            bool ExtraDataSTcheck = false;

                            bool STMachinary = false;
                            bool STMaterial = false;
                            bool STMaintenance = false;
                            bool STExtraone = false;
                            bool STExtratwo = false;

                            string strExtradatacheck = "";

                            string strExtrastcheck = "";
                            string strMachinary = "";
                            string strMaterial = "";
                            string strMaintenance = "";
                            string strExtraone = "";
                            string strExtratwo = "";

                            bool SCMachinary = false;
                            bool SCMaterial = false;
                            bool SCMaintenance = false;
                            bool SCExtraone = false;
                            bool SCExtratwo = false;

                            string strSCMachinary = "";
                            string strSCMaterial = "";
                            string strSCMaintenance = "";
                            string strSCExtraone = "";
                            string strSCExtratwo = "";

                            float SCamtonMachinary = 0;
                            float SCamtonMaintenance = 0;
                            float SCamtonMaterial = 0;
                            float SCamtonExtraone = 0;
                            float SCamtonExtratwo = 0;

                            string strStaxonExtradataservicecharges = "";
                            bool bStaxonExtradataservicecharges = false;

                            string strSTDiscountone = "";
                            string strSTDiscounttwo = "";

                            bool STDiscountone = false;
                            bool STDiscounttwo = false;


                            #endregion

                            string SubunitName = "";



                            string SPName = "";
                            Hashtable htbilling = new Hashtable();
                            SPName = "GetpdfsformonthlywiseInvoces";
                            htbilling.Add("@clientid", clientid);
                            htbilling.Add("@month", Month);
                            htbilling.Add("@Clientidprefix", CmpIDPrefix);

                            DataTable DtBilling =config.ExecuteAdaptorAsyncWithParams(SPName, htbilling).Result;

                            if (DtBilling.Rows.Count > 0)
                            {
                                companyName = DtBilling.Rows[0]["CompanyName"].ToString();
                                companyAddress = DtBilling.Rows[0]["Address"].ToString();
                                companyaddressline = DtBilling.Rows[0]["Addresslineone"].ToString();
                                Phoneno = DtBilling.Rows[0]["Phoneno"].ToString();
                                Faxno = DtBilling.Rows[0]["Faxno"].ToString();
                                Emailid = DtBilling.Rows[0]["Emailid"].ToString();
                                Website = DtBilling.Rows[0]["Website"].ToString();

                                ServicetaxNo = DtBilling.Rows[0]["BillNotes"].ToString();
                                PANNO = DtBilling.Rows[0]["Labourrule"].ToString();
                                PFNo = DtBilling.Rows[0]["PFNo"].ToString();
                                Esino = DtBilling.Rows[0]["ESINo"].ToString();

                                ContractID = DtBilling.Rows[0]["contractid"].ToString();

                                if (String.IsNullOrEmpty(DtBilling.Rows[0]["ServiceCharge"].ToString()) == false)
                                {
                                    ServiceCharge = DtBilling.Rows[0]["ServiceCharge"].ToString();
                                }
                                if (String.IsNullOrEmpty(DtBilling.Rows[0]["ServiceChargeType"].ToString()) == false)
                                {
                                    strSCType = DtBilling.Rows[0]["ServiceChargeType"].ToString();
                                }
                                string tempDescription = DtBilling.Rows[0]["Description"].ToString();
                                if (tempDescription.Trim().Length > 0)
                                {
                                    strDescription = tempDescription;
                                }
                                if (strSCType.Length > 0)
                                {
                                    bSCType = Convert.ToBoolean(strSCType);
                                }
                                strIncludeST = DtBilling.Rows[0]["IncludeST"].ToString();
                                strST75 = DtBilling.Rows[0]["ServiceTax75"].ToString();
                                if (strIncludeST == "True")
                                {
                                    bIncludeST = true;
                                }
                                if (strST75 == "True")
                                {
                                    bST75 = true;
                                }

                                strStaxonservicecharge = DtBilling.Rows[0]["Staxonservicecharge"].ToString();
                                if (strStaxonservicecharge == "True")
                                {
                                    bStaxonservicecharge = true;
                                }


                                BillNo = DtBilling.Rows[0]["billno"].ToString();
                                BillDate = Convert.ToDateTime(DtBilling.Rows[0]["billdt"].ToString());

                                #region Begin New code for values taken from database as on 11/03/2014 by venkat

                                if (String.IsNullOrEmpty(DtBilling.Rows[0]["dutiestotalamount"].ToString()) == false)
                                {
                                    totalamount = float.Parse(DtBilling.Rows[0]["dutiestotalamount"].ToString());
                                }
                                if (String.IsNullOrEmpty(DtBilling.Rows[0]["ServiceChrg"].ToString()) == false)
                                {
                                    servicecharge = float.Parse(DtBilling.Rows[0]["ServiceChrg"].ToString());
                                }
                                if (String.IsNullOrEmpty(DtBilling.Rows[0]["ServiceTax"].ToString()) == false)
                                {
                                    servicetax = float.Parse(DtBilling.Rows[0]["ServiceTax"].ToString());
                                }
                                if (String.IsNullOrEmpty(DtBilling.Rows[0]["CESS"].ToString()) == false)
                                {
                                    cess = float.Parse(DtBilling.Rows[0]["CESS"].ToString());
                                }
                                if (String.IsNullOrEmpty(DtBilling.Rows[0]["SHECess"].ToString()) == false)
                                {
                                    shecess = float.Parse(DtBilling.Rows[0]["SHECess"].ToString());
                                }
                                if (String.IsNullOrEmpty(DtBilling.Rows[0]["GrandTotal"].ToString()) == false)
                                {
                                    Grandtotal = float.Parse(DtBilling.Rows[0]["GrandTotal"].ToString());
                                }

                                if (String.IsNullOrEmpty(DtBilling.Rows[0]["MachinaryCost"].ToString()) == false)
                                {
                                    machinarycost = float.Parse(DtBilling.Rows[0]["MachinaryCost"].ToString());
                                }
                                if (String.IsNullOrEmpty(DtBilling.Rows[0]["MaterialCost"].ToString()) == false)
                                {
                                    materialcost = float.Parse(DtBilling.Rows[0]["MaterialCost"].ToString());
                                }
                                if (String.IsNullOrEmpty(DtBilling.Rows[0]["ElectricalChrg"].ToString()) == false)
                                {
                                    maintenancecost = float.Parse(DtBilling.Rows[0]["ElectricalChrg"].ToString());
                                }
                                if (String.IsNullOrEmpty(DtBilling.Rows[0]["ExtraAmtone"].ToString()) == false)
                                {
                                    extraonecost = float.Parse(DtBilling.Rows[0]["ExtraAmtone"].ToString());
                                }
                                if (String.IsNullOrEmpty(DtBilling.Rows[0]["ExtraAmtTwo"].ToString()) == false)
                                {
                                    extratwocost = float.Parse(DtBilling.Rows[0]["ExtraAmtTwo"].ToString());
                                }
                                if (String.IsNullOrEmpty(DtBilling.Rows[0]["Discount"].ToString()) == false)
                                {
                                    discountone = float.Parse(DtBilling.Rows[0]["Discount"].ToString());
                                }
                                if (String.IsNullOrEmpty(DtBilling.Rows[0]["Discounttwo"].ToString()) == false)
                                {
                                    discounttwo = float.Parse(DtBilling.Rows[0]["Discounttwo"].ToString());
                                }

                                machinarycosttitle = DtBilling.Rows[0]["Machinarycosttitle"].ToString();
                                materialcosttitle = DtBilling.Rows[0]["Materialcosttitle"].ToString();
                                maintenancecosttitle = DtBilling.Rows[0]["Maintanancecosttitle"].ToString();
                                extraonecosttitle = DtBilling.Rows[0]["Extraonetitle"].ToString();
                                extratwocosttitle = DtBilling.Rows[0]["Extratwotitle"].ToString();
                                discountonetitle = DtBilling.Rows[0]["Discountonetitle"].ToString();
                                discounttwotitle = DtBilling.Rows[0]["Discounttwotitle"].ToString();

                                if (String.IsNullOrEmpty(DtBilling.Rows[0]["Extradatacheck"].ToString()) == false)
                                {
                                    strExtradatacheck = DtBilling.Rows[0]["Extradatacheck"].ToString();
                                    if (strExtradatacheck == "True")
                                    {
                                        Extradatacheck = true;
                                    }
                                }

                                if (String.IsNullOrEmpty(DtBilling.Rows[0]["ExtraDataSTcheck"].ToString()) == false)
                                {
                                    strExtrastcheck = DtBilling.Rows[0]["ExtraDataSTcheck"].ToString();
                                    if (strExtrastcheck == "True")
                                    {
                                        ExtraDataSTcheck = true;
                                    }
                                }



                                if (String.IsNullOrEmpty(DtBilling.Rows[0]["STMachinary"].ToString()) == false)
                                {
                                    strMachinary = DtBilling.Rows[0]["STMachinary"].ToString();
                                    if (strMachinary == "True")
                                    {
                                        STMachinary = true;
                                    }
                                }

                                if (String.IsNullOrEmpty(DtBilling.Rows[0]["STMaterial"].ToString()) == false)
                                {
                                    strMaterial = DtBilling.Rows[0]["STMaterial"].ToString();
                                    if (strMaterial == "True")
                                    {
                                        STMaterial = true;
                                    }
                                }

                                if (String.IsNullOrEmpty(DtBilling.Rows[0]["STMaintenance"].ToString()) == false)
                                {
                                    strMaintenance = DtBilling.Rows[0]["STMaintenance"].ToString();
                                    if (strMaintenance == "True")
                                    {
                                        STMaintenance = true;
                                    }
                                }

                                if (String.IsNullOrEmpty(DtBilling.Rows[0]["STExtraone"].ToString()) == false)
                                {
                                    strExtraone = DtBilling.Rows[0]["STExtraone"].ToString();
                                    if (strExtraone == "True")
                                    {
                                        STExtraone = true;
                                    }
                                }

                                if (String.IsNullOrEmpty(DtBilling.Rows[0]["STExtratwo"].ToString()) == false)
                                {
                                    strExtratwo = DtBilling.Rows[0]["STExtratwo"].ToString();
                                    if (strExtratwo == "True")
                                    {
                                        STExtratwo = true;
                                    }
                                }

                                if (String.IsNullOrEmpty(DtBilling.Rows[0]["ServiceTax75forbill"].ToString()) == false)
                                {
                                    ServiceTax75 = float.Parse(DtBilling.Rows[0]["ServiceTax75forbill"].ToString());
                                }

                                if (String.IsNullOrEmpty(DtBilling.Rows[0]["ServiceTax25"].ToString()) == false)
                                {
                                    ServiceTax25 = float.Parse(DtBilling.Rows[0]["ServiceTax25"].ToString());
                                }



                                if (String.IsNullOrEmpty(DtBilling.Rows[0]["SCMachinary"].ToString()) == false)
                                {
                                    strSCMachinary = DtBilling.Rows[0]["SCMachinary"].ToString();
                                    if (strSCMachinary == "True")
                                    {
                                        SCMachinary = true;
                                    }
                                }

                                if (String.IsNullOrEmpty(DtBilling.Rows[0]["SCMaintenance"].ToString()) == false)
                                {
                                    strSCMaintenance = DtBilling.Rows[0]["SCMaintenance"].ToString();
                                    if (strSCMaintenance == "True")
                                    {
                                        SCMaintenance = true;
                                    }
                                }

                                if (String.IsNullOrEmpty(DtBilling.Rows[0]["SCMaterial"].ToString()) == false)
                                {
                                    strSCMaterial = DtBilling.Rows[0]["SCMaterial"].ToString();
                                    if (strSCMaterial == "True")
                                    {
                                        SCMaterial = true;
                                    }
                                }

                                if (String.IsNullOrEmpty(DtBilling.Rows[0]["SCExtraone"].ToString()) == false)
                                {
                                    strSCExtraone = DtBilling.Rows[0]["SCExtraone"].ToString();
                                    if (strSCExtraone == "True")
                                    {
                                        SCExtraone = true;
                                    }
                                }

                                if (String.IsNullOrEmpty(DtBilling.Rows[0]["SCExtratwo"].ToString()) == false)
                                {
                                    strSCExtratwo = DtBilling.Rows[0]["SCExtratwo"].ToString();
                                    if (strSCExtratwo == "True")
                                    {
                                        SCExtratwo = true;
                                    }
                                }

                                if (String.IsNullOrEmpty(DtBilling.Rows[0]["SCamtonMachinary"].ToString()) == false)
                                {
                                    SCamtonMachinary = float.Parse(DtBilling.Rows[0]["SCamtonMachinary"].ToString());
                                }

                                if (String.IsNullOrEmpty(DtBilling.Rows[0]["SCamtonMaintenance"].ToString()) == false)
                                {
                                    SCamtonMaintenance = float.Parse(DtBilling.Rows[0]["SCamtonMaintenance"].ToString());
                                }

                                if (String.IsNullOrEmpty(DtBilling.Rows[0]["SCamtonMaterial"].ToString()) == false)
                                {
                                    SCamtonMaterial = float.Parse(DtBilling.Rows[0]["SCamtonMaterial"].ToString());
                                }

                                if (String.IsNullOrEmpty(DtBilling.Rows[0]["SCamtonExtraone"].ToString()) == false)
                                {
                                    SCamtonExtraone = float.Parse(DtBilling.Rows[0]["SCamtonExtraone"].ToString());
                                }

                                if (String.IsNullOrEmpty(DtBilling.Rows[0]["SCamtonExtratwo"].ToString()) == false)
                                {
                                    SCamtonExtratwo = float.Parse(DtBilling.Rows[0]["SCamtonExtratwo"].ToString());
                                }

                                if (String.IsNullOrEmpty(DtBilling.Rows[0]["StaxonExtradataservicecharges"].ToString()) == false)
                                {
                                    strStaxonExtradataservicecharges = DtBilling.Rows[0]["StaxonExtradataservicecharges"].ToString();
                                    if (strStaxonExtradataservicecharges == "True")
                                    {
                                        bStaxonExtradataservicecharges = true;
                                    }
                                }


                                if (String.IsNullOrEmpty(DtBilling.Rows[0]["STDiscountone"].ToString()) == false)
                                {
                                    strSTDiscountone = DtBilling.Rows[0]["STDiscountone"].ToString();
                                    if (strSTDiscountone == "True")
                                    {
                                        STDiscountone = true;
                                    }
                                }

                                if (String.IsNullOrEmpty(DtBilling.Rows[0]["STDiscounttwo"].ToString()) == false)
                                {
                                    strSTDiscounttwo = DtBilling.Rows[0]["STDiscounttwo"].ToString();
                                    if (strSTDiscounttwo == "True")
                                    {
                                        STDiscounttwo = true;
                                    }
                                }

                                #endregion

                                SubunitName = DtBilling.Rows[0]["Subunitname"].ToString();

                            }

                            else
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "showlalert", "alert('Please Generate The Bill Once Again');", true);
                                return;
                            }

                            document.AddTitle(companyName);
                            document.AddAuthor("DIYOS");
                            document.AddSubject("Invoice");
                            document.AddKeywords("Keyword1, keyword2, …");
                            if (File.Exists(imagepath))
                            {
                                iTextSharp.text.Image gif2 = iTextSharp.text.Image.GetInstance(imagepath);

                                gif2.Alignment = (iTextSharp.text.Image.ALIGN_LEFT | iTextSharp.text.Image.UNDERLYING);
                                gif2.ScalePercent(60f);
                                gif2.BackgroundColor = BaseColor.BLUE;
                                document.Add(new Paragraph(" "));
                                document.Add(gif2);
                            }

                            PdfPTable tablelogo = new PdfPTable(2);
                            tablelogo.TotalWidth = 380f;
                            tablelogo.LockedWidth = true;
                            tablelogo.HorizontalAlignment = Element.ALIGN_RIGHT;
                            float[] widtlogo = new float[] { 0.8f, 3.2f };
                            tablelogo.SetWidths(widtlogo);

                            PdfPCell celll = new PdfPCell(new Paragraph(" ", FontFactory.GetFont(FontStyle, 12, Font.NORMAL, BaseColor.BLACK)));
                            celll.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            celll.Border = 0;
                            celll.Colspan = 2;
                            tablelogo.AddCell(celll);

                            PdfPCell CCompName = new PdfPCell(new Paragraph(companyName, FontFactory.GetFont(FontStyle, 16, Font.BOLD, BaseColor.BLACK)));
                            CCompName.HorizontalAlignment = 0;
                            CCompName.Border = 0;
                            CCompName.Colspan = 2;
                            tablelogo.AddCell(CCompName);

                            PdfPCell CCompAddress = new PdfPCell(new Paragraph(companyAddress, FontFactory.GetFont(FontStyle, fontsize - 1, Font.NORMAL, BaseColor.BLACK)));
                            CCompAddress.HorizontalAlignment = 0;
                            CCompAddress.Border = 0;
                            CCompAddress.Colspan = 2;
                            tablelogo.AddCell(CCompAddress);

                            PdfPCell CCompPhoneno = new PdfPCell(new Paragraph("Phone No", FontFactory.GetFont(FontStyle, fontsize - 1, Font.NORMAL, BaseColor.BLACK)));
                            CCompPhoneno.HorizontalAlignment = 0;
                            CCompPhoneno.Border = 0;
                            //CCompPhoneno.Colspan = 2;
                            tablelogo.AddCell(CCompPhoneno);

                            PdfPCell CCompPhoneno1 = new PdfPCell(new Paragraph(": " + Phoneno, FontFactory.GetFont(FontStyle, fontsize - 1, Font.NORMAL, BaseColor.BLACK)));
                            CCompPhoneno1.HorizontalAlignment = 0;
                            CCompPhoneno1.Border = 0;
                            //CCompPhoneno1.Colspan = 2;
                            tablelogo.AddCell(CCompPhoneno1);

                            PdfPCell CCompFaxno = new PdfPCell(new Paragraph("Fax No", FontFactory.GetFont(FontStyle, fontsize - 1, Font.NORMAL, BaseColor.BLACK)));
                            CCompFaxno.HorizontalAlignment = 0;
                            CCompFaxno.Border = 0;
                            //CCompFaxno.Colspan = 2;
                            tablelogo.AddCell(CCompFaxno);

                            PdfPCell CCompFaxno1 = new PdfPCell(new Paragraph(": " + Faxno, FontFactory.GetFont(FontStyle, fontsize - 1, Font.NORMAL, BaseColor.BLACK)));
                            CCompFaxno1.HorizontalAlignment = 0;
                            CCompFaxno1.Border = 0;
                            //CCompFaxno1.Colspan = 2;
                            tablelogo.AddCell(CCompFaxno1);

                            PdfPCell CCompEmailid = new PdfPCell(new Paragraph("E-mail", FontFactory.GetFont(FontStyle, fontsize - 1, Font.NORMAL, BaseColor.BLACK)));
                            CCompEmailid.HorizontalAlignment = 0;
                            CCompEmailid.Border = 0;
                            //CCompEmailid.Colspan = 2;
                            tablelogo.AddCell(CCompEmailid);

                            PdfPCell CCompEmailid1 = new PdfPCell(new Paragraph(": " + Emailid, FontFactory.GetFont(FontStyle, fontsize - 1, Font.NORMAL, BaseColor.BLACK)));
                            CCompEmailid1.HorizontalAlignment = 0;
                            CCompEmailid1.Border = 0;
                            //CCompEmailid1.Colspan = 2;
                            tablelogo.AddCell(CCompEmailid1);

                            PdfPCell CCompWebsite = new PdfPCell(new Paragraph("Website", FontFactory.GetFont(FontStyle, fontsize - 1, Font.NORMAL, BaseColor.BLACK)));
                            CCompWebsite.HorizontalAlignment = 0;
                            CCompWebsite.Border = 0;
                            //CCompEmailid.Colspan = 2;
                            tablelogo.AddCell(CCompWebsite);

                            PdfPCell CCompWebsite1 = new PdfPCell(new Paragraph(": " + Website, FontFactory.GetFont(FontStyle, fontsize - 1, Font.NORMAL, BaseColor.BLACK)));
                            CCompWebsite1.HorizontalAlignment = 0;
                            CCompWebsite1.Border = 0;
                            //CCompEmailid1.Colspan = 2;
                            tablelogo.AddCell(CCompWebsite1);


                            tablelogo.AddCell(celll);

                            document.Add(tablelogo);


                            PdfPTable tempTable1 = new PdfPTable(4);
                            tempTable1.TotalWidth = 520f;
                            tempTable1.LockedWidth = true;
                            //tempTable1.HorizontalAlignment = Element.ALIGN_LEFT;
                            float[] tempWidth1 = new float[] { 3f, 2f, 1f, 3f };
                            tempTable1.SetWidths(tempWidth1);

                            int sno = 0;

                            float totaldts = 0;
                            float totalmanpower = 0;

                            PdfPCell CInvoice = new PdfPCell(new Paragraph("INVOICE", FontFactory.GetFont(FontStyle, 16, Font.BOLD, BaseColor.BLACK)));
                            CInvoice.HorizontalAlignment = 1;
                            CInvoice.Border = 0;
                            CInvoice.PaddingTop = -6;
                            //CInvoice.PaddingRight = -10;
                            CInvoice.Colspan = 4;
                            tempTable1.AddCell(CInvoice);

                            PdfPCell CInvoicespace = new PdfPCell(new Paragraph(" ", FontFactory.GetFont(FontStyle, 16, Font.BOLD, BaseColor.BLACK)));
                            CInvoicespace.HorizontalAlignment = 1;
                            CInvoicespace.Border = 0;
                            CInvoicespace.PaddingTop = -10;
                            CInvoicespace.Colspan = 4;
                            tempTable1.AddCell(CInvoicespace);

                            string addressData = "";

                            addressData = DtBilling.Rows[0]["ClientName"].ToString();
                            if (addressData.Trim().Length > 0)
                            {
                                PdfPCell clientname = new PdfPCell(new Paragraph("     " + addressData, FontFactory.GetFont(FontStyle, fontsize, Font.BOLD, BaseColor.BLACK)));
                                clientname.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                clientname.Colspan = 2;
                                clientname.Border = 5;
                                tempTable1.AddCell(clientname);
                            }
                            else
                            {
                                PdfPCell clientname = new PdfPCell(new Paragraph(" ", FontFactory.GetFont(FontStyle, fontsize, Font.BOLD, BaseColor.BLACK)));
                                clientname.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                clientname.Colspan = 2;
                                clientname.Border = 5;
                                tempTable1.AddCell(clientname);
                            }

                            PdfPCell cellBillno = new PdfPCell(new Paragraph("Bill No: ", FontFactory.GetFont(FontStyle, fontsize, Font.BOLD, BaseColor.BLACK)));
                            cellBillno.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            cellBillno.Border = 5;
                            tempTable1.AddCell(cellBillno);

                            PdfPCell cellBillno1 = new PdfPCell(new Paragraph(BillNo, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                            cellBillno1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            cellBillno1.Border = 9;
                            tempTable1.AddCell(cellBillno1);


                            addressData = DtBilling.Rows[0]["ClientAddrHno"].ToString();
                            if (addressData.Trim().Length > 0)
                            {
                                PdfPCell clientaddrhno = new PdfPCell(new Paragraph("     " + addressData, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                clientaddrhno.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                clientaddrhno.Colspan = 2;
                                clientaddrhno.Border = 4;
                                clientaddrhno.PaddingRight = -60;
                                tempTable1.AddCell(clientaddrhno);
                            }
                            else
                            {
                                PdfPCell clientaddrhno = new PdfPCell(new Paragraph(" ", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                clientaddrhno.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                clientaddrhno.Colspan = 2;
                                clientaddrhno.Border = 4;
                                clientaddrhno.PaddingRight = -60;
                                tempTable1.AddCell(clientaddrhno);
                            }

                            PdfPCell cellspace = new PdfPCell(new Paragraph(" ", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                            cellspace.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            cellspace.Border = 12;
                            cellspace.Colspan = 2;
                            tempTable1.AddCell(cellspace);

                            addressData = DtBilling.Rows[0]["ClientAddrStreet"].ToString();
                            if (addressData.Trim().Length > 0)
                            {
                                PdfPCell clientstreet = new PdfPCell(new Paragraph("     " + addressData, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                clientstreet.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                clientstreet.Border = 4;
                                clientstreet.Colspan = 2;
                                clientstreet.PaddingRight = -60;
                                tempTable1.AddCell(clientstreet);
                            }
                            else
                            {
                                PdfPCell clientstreet = new PdfPCell(new Paragraph(" ", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                clientstreet.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                clientstreet.Border = 4;
                                clientstreet.Colspan = 2;
                                clientstreet.PaddingRight = -60;
                                tempTable1.AddCell(clientstreet);
                            }



                            PdfPCell cellBilldate = new PdfPCell(new Paragraph("Bill Date: ", FontFactory.GetFont(FontStyle, fontsize, Font.BOLD, BaseColor.BLACK)));
                            cellBilldate.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            cellBilldate.Border = 4;
                            tempTable1.AddCell(cellBilldate);

                            PdfPCell cellBilldate1 = new PdfPCell(new Paragraph(BillDate.Day.ToString("00") + "." + BillDate.Month.ToString("00") + "." + BillDate.Year, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                            cellBilldate1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            cellBilldate1.Border = 8;
                            tempTable1.AddCell(cellBilldate1);


                            addressData = DtBilling.Rows[0]["ClientAddrArea"].ToString();
                            if (addressData.Trim().Length > 0)
                            {
                                PdfPCell clientstreet = new PdfPCell(new Paragraph("     " + addressData, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                clientstreet.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                clientstreet.Border = 4;
                                clientstreet.Colspan = 2;
                                clientstreet.PaddingRight = -60;
                                tempTable1.AddCell(clientstreet);
                            }
                            else
                            {
                                PdfPCell clientstreet = new PdfPCell(new Paragraph(" ", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                clientstreet.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                clientstreet.Border = 4;
                                clientstreet.Colspan = 2;
                                clientstreet.PaddingRight = -60;
                                tempTable1.AddCell(clientstreet);
                            }


                            tempTable1.AddCell(cellspace);

                            addressData = DtBilling.Rows[0]["ClientAddrColony"].ToString();
                            if (addressData.Trim().Length > 0)
                            {
                                PdfPCell clientcolony = new PdfPCell(new Paragraph("     " + addressData, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                clientcolony.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                clientcolony.Colspan = 2;
                                clientcolony.Border = 4;
                                clientcolony.PaddingRight = -60;
                                tempTable1.AddCell(clientcolony);
                            }
                            else
                            {
                                PdfPCell clientcolony = new PdfPCell(new Paragraph(" ", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                clientcolony.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                clientcolony.Colspan = 2;
                                clientcolony.Border = 4;
                                clientcolony.PaddingRight = -60;
                                tempTable1.AddCell(clientcolony);
                            }


                            PdfPCell cellSubunit = new PdfPCell(new Paragraph("Sub Unit: ", FontFactory.GetFont(FontStyle, fontsize, Font.BOLD, BaseColor.BLACK)));
                            cellSubunit.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            cellSubunit.Border = 4;
                            tempTable1.AddCell(cellSubunit);


                            PdfPCell cellSubunit1 = new PdfPCell(new Paragraph(SubunitName, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                            cellSubunit1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            cellSubunit1.Border = 8;
                            tempTable1.AddCell(cellSubunit1);



                            addressData = DtBilling.Rows[0]["ClientAddrcity"].ToString();
                            if (addressData.Trim().Length > 0)
                            {
                                PdfPCell clientcity = new PdfPCell(new Paragraph("     " + addressData, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                clientcity.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                clientcity.Colspan = 2;
                                clientcity.Border = 4;
                                clientcity.PaddingRight = -60;
                                tempTable1.AddCell(clientcity);
                            }
                            else
                            {
                                PdfPCell clientcity = new PdfPCell(new Paragraph(" ", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                clientcity.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                clientcity.Colspan = 2;
                                clientcity.Border = 4;
                                clientcity.PaddingRight = -60;
                                tempTable1.AddCell(clientcity);
                            }



                            tempTable1.AddCell(cellspace);

                            addressData = DtBilling.Rows[0]["ClientAddrstate"].ToString();
                            if (addressData.Trim().Length > 0)
                            {
                                PdfPCell clientstate = new PdfPCell(new Paragraph("     " + addressData, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                clientstate.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                clientstate.Colspan = 2;
                                clientstate.Border = 4;
                                clientstate.PaddingRight = -60;
                                tempTable1.AddCell(clientstate);
                            }
                            else
                            {
                                PdfPCell clientstate = new PdfPCell(new Paragraph(" ", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                clientstate.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                clientstate.Colspan = 2;
                                clientstate.Border = 4;
                                clientstate.PaddingRight = -60;
                                tempTable1.AddCell(clientstate);
                            }


                            string Location = DtBilling.Rows[0]["Location"].ToString();

                            PdfPCell celllocation = new PdfPCell(new Paragraph("Location: ", FontFactory.GetFont(FontStyle, fontsize, Font.BOLD, BaseColor.BLACK)));
                            celllocation.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            celllocation.Border = 4;
                            tempTable1.AddCell(celllocation);


                            PdfPCell celllocation1 = new PdfPCell(new Paragraph(Location, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                            celllocation1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            celllocation1.Border = 8;
                            tempTable1.AddCell(celllocation1);


                            //tempTable1.AddCell(cellspace);

                            addressData = DtBilling.Rows[0]["ClientAddrpin"].ToString();
                            if (addressData.Trim().Length > 0)
                            {
                                PdfPCell clietnpin = new PdfPCell(new Paragraph("     " + addressData, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                clietnpin.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                clietnpin.Colspan = 2;
                                clietnpin.Border = 4;
                                clietnpin.PaddingRight = -60;
                                tempTable1.AddCell(clietnpin);
                            }
                            else
                            {
                                PdfPCell clietnpin = new PdfPCell(new Paragraph(" ", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                clietnpin.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                clietnpin.Colspan = 2;
                                clietnpin.Border = 4;
                                clietnpin.PaddingRight = -60;
                                tempTable1.AddCell(clietnpin);
                            }

                            tempTable1.AddCell(cellspace);

                            PdfPCell Cellpono = new PdfPCell(new Paragraph("     " + "PO No :", FontFactory.GetFont(FontStyle, fontsize, Font.BOLD, BaseColor.BLACK)));
                            Cellpono.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            Cellpono.Colspan = 2;
                            Cellpono.Border = 4;
                            tempTable1.AddCell(Cellpono);

                            tempTable1.AddCell(cellspace);

                            PdfPCell cell14 = new PdfPCell(new Paragraph("     " + "Invoice for the Month of    " + GetMonthName() + "' " + GetMonthOfYear() + "      ", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                            cell14.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            cell14.Border = 6;
                            cell14.Colspan = 2;
                            tempTable1.AddCell(cell14);

                            PdfPCell cellspace1 = new PdfPCell(new Paragraph(" ", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                            cellspace1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            cellspace1.Border = 14;
                            cellspace1.Colspan = 2;
                            tempTable1.AddCell(cellspace1);

                            PdfPCell cellspace2 = new PdfPCell(new Paragraph(" ", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                            cellspace2.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            cellspace2.Border = 0;
                            cellspace2.Colspan = 4;
                            tempTable1.AddCell(cellspace2);


                            document.Add(tempTable1);



                            int colCount = 6;// gvClientBilling.Columns.Count;
                            //Create a table

                            #region Code For Data table


                            PdfPTable table = new PdfPTable(colCount);
                            table.TotalWidth = 520f;
                            table.LockedWidth = true;
                            table.HorizontalAlignment = 1;
                            //create an array to store column widths
                            // int[] colWidths = new int[5];
                            float[] colWidths = new float[] { 1f, 4.5f, 1f, 1f, 2f, 2f };
                            table.SetWidths(colWidths);



                            PdfPCell cell;
                            string cellText;
                            //create the header row
                            for (int colIndex = 0; colIndex < 6; colIndex++)
                            {
                                if (colIndex == 0)
                                {
                                    PdfPCell CSNo = new PdfPCell(new Phrase("SL.NO", FontFactory.GetFont(FontStyle, fontsize, Font.BOLD, BaseColor.BLACK)));
                                    CSNo.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                                    //CSNo.Border = 15;
                                    table.AddCell(CSNo);
                                }

                                if (colIndex == 1)
                                {
                                    cell = new PdfPCell(new Phrase("DESCRIPTION / DETAILS", FontFactory.GetFont(FontStyle, fontsize, Font.BOLD, BaseColor.BLACK)));
                                    //set the background color for the header cell
                                    cell.HorizontalAlignment = 1;
                                    table.AddCell(cell);
                                }
                                if (colIndex == 2)
                                {

                                    cell = new PdfPCell(new Phrase("DUTY", FontFactory.GetFont(FontStyle, fontsize, Font.BOLD, BaseColor.BLACK)));
                                    //set the background color for the header cell
                                    cell.HorizontalAlignment = 1;
                                    table.AddCell(cell);
                                }
                                if (colIndex == 3)
                                {
                                    cell = new PdfPCell(new Phrase("MAN", FontFactory.GetFont(FontStyle, fontsize, Font.BOLD, BaseColor.BLACK)));
                                    //set the background color for the header cell
                                    cell.HorizontalAlignment = 1;
                                    table.AddCell(cell);
                                }

                                if (colIndex == 4)
                                {

                                    cell = new PdfPCell(new Phrase("RATE", FontFactory.GetFont(FontStyle, fontsize, Font.BOLD, BaseColor.BLACK)));
                                    //set the background color for the header cell
                                    cell.HorizontalAlignment = 1;
                                    table.AddCell(cell);
                                }
                                if (colIndex == 5)
                                {
                                    cell = new PdfPCell(new Phrase("AMOUNT (Rs)", FontFactory.GetFont(FontStyle, fontsize, Font.BOLD, BaseColor.BLACK)));
                                    //set the background color for the header cell
                                    cell.HorizontalAlignment = 1;
                                    table.AddCell(cell);
                                }

                            }
                            ////export rows from GridView to table

                            string spUnitbillbreakup = "GetpdfforClientbillingfromunitbill";
                            Hashtable htunitbillbreakup = new Hashtable();
                            htunitbillbreakup.Add("@Clientid", @clientid);
                            htunitbillbreakup.Add("@month", Month);
                            DataTable dtunitbillbreakup = config.ExecuteAdaptorAsyncWithParams(spUnitbillbreakup, htunitbillbreakup).Result;

                            for (int rowIndex = 0; rowIndex < dtunitbillbreakup.Rows.Count; rowIndex++)
                            {
                                string lblamount = dtunitbillbreakup.Rows[rowIndex]["Dutiesamount"].ToString();
                                if (lblamount != string.Empty)
                                {
                                    float amount = 0;
                                    if (lblamount.Length > 0)
                                        amount = Convert.ToSingle(lblamount);
                                    if (amount >= 0)
                                    {
                                        for (int j = 0; j < 6; j++)
                                        {
                                            if (j == 0)
                                            {
                                                PdfPCell CSNo = new PdfPCell(new Phrase((++sno).ToString() + "\n", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                                CSNo.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                                                CSNo.Border = 15;
                                                table.AddCell(CSNo);
                                            }

                                            //fetch the column value of the current row
                                            if (j == 1)
                                            {
                                                string design = dtunitbillbreakup.Rows[rowIndex]["Designation"].ToString();

                                                //string summaryQry = "select summary from contractdetails " +
                                                //    "  where clientid='" + ddlclientid.SelectedValue + "' and Designations='" + design.Text + "'";
                                                //DataTable dt = SqlHelper.Instance.GetTableByQuery(summaryQry);
                                                cellText = design;
                                                //if (dt.Rows.Count > 0)
                                                //{
                                                //    if (dt.Rows[0]["summary"].ToString().Trim().Length > 0)
                                                //        cellText += " (" + dt.Rows[0]["summary"].ToString() + ")";
                                                //}

                                                //create a new cell with column value
                                                cell = new PdfPCell(new Phrase(cellText, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                                cell.HorizontalAlignment = 0;
                                                table.AddCell(cell);
                                            }
                                            if (j == 2)
                                            {
                                                string Noofduties = dtunitbillbreakup.Rows[rowIndex]["Duties"].ToString();
                                                if (Noofduties == "0")
                                                {
                                                    cellText = "";
                                                }
                                                else
                                                {
                                                    cellText = Noofduties;

                                                }
                                                if (cellText.Length > 0)
                                                {
                                                    totaldts += float.Parse(cellText);
                                                }
                                                //create a new cell with column value
                                                cell = new PdfPCell(new Phrase(cellText, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                                cell.HorizontalAlignment = 2;
                                                table.AddCell(cell);
                                            }
                                            if (j == 3)
                                            {
                                                string Noofemps = dtunitbillbreakup.Rows[rowIndex]["Noofemps"].ToString();
                                                cellText = Noofemps;
                                                if (Noofemps == "0")
                                                {
                                                    cellText = "";
                                                }
                                                else
                                                {
                                                    cellText = Noofemps;
                                                }
                                                if (cellText.Length > 0)
                                                {
                                                    totalmanpower += float.Parse(cellText);
                                                }
                                                //create a new cell with column value
                                                cell = new PdfPCell(new Phrase(cellText, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                                cell.HorizontalAlignment = 2;
                                                table.AddCell(cell);
                                            }
                                            if (j == 4)
                                            {
                                                string payrate = dtunitbillbreakup.Rows[rowIndex]["payrate"].ToString();
                                                string payratetype = dtunitbillbreakup.Rows[rowIndex]["Payratetype"].ToString();
                                                cellText = payrate;
                                                //create a new cell with column value
                                                cell = new PdfPCell(new Phrase(cellText + " " + payratetype, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                                cell.HorizontalAlignment = 2;
                                                table.AddCell(cell);
                                            }
                                            if (j == 5)
                                            {
                                                string dutiesamount = dtunitbillbreakup.Rows[rowIndex]["Dutiesamount"].ToString();
                                                cellText = dutiesamount;
                                                //create a new cell with column value
                                                float Payments = float.Parse(cellText);
                                                cell = new PdfPCell(new Phrase(Payments.ToString("#,#.00"), FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                                cell.HorizontalAlignment = 2;
                                                table.AddCell(cell);
                                            }
                                        }
                                    }
                                }
                            }
                            document.Add(table);

                            tablelogo.AddCell(celll);

                            PdfPTable tabled = new PdfPTable(colCount);
                            tabled.TotalWidth = 520;//432f;
                            tabled.LockedWidth = true;
                            float[] widthd = new float[] { 1f, 4.5f, 1f, 1f, 2f, 2f };
                            tabled.SetWidths(widthd);

                            PdfPCell cellsp1 = new PdfPCell(new Phrase(" ", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                            cellsp1.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                            tabled.AddCell(cellsp1);

                            PdfPCell cellsp2 = new PdfPCell(new Phrase(" ", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                            cellsp2.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                            tabled.AddCell(cellsp2);

                            PdfPCell celldz1 = new PdfPCell(new Phrase(totaldts.ToString("0.00"), FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                            celldz1.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                            tabled.AddCell(celldz1);

                            PdfPCell cellsp3 = new PdfPCell(new Phrase(totalmanpower.ToString("0.00"), FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                            cellsp3.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                            tabled.AddCell(cellsp3);

                            PdfPCell cellsp4 = new PdfPCell(new Phrase(" ", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                            cellsp4.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                            tabled.AddCell(cellsp4);

                            PdfPCell cellsp5 = new PdfPCell(new Phrase(" ", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                            cellsp5.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                            tabled.AddCell(cellsp5);

                            PdfPCell celltot1 = new PdfPCell(new Phrase(" ", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                            celltot1.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                            celltot1.Border = 0;
                            tabled.AddCell(celltot1);

                            PdfPCell celltot2 = new PdfPCell(new Phrase(" ", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                            celltot2.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                            celltot2.Border = 0;
                            tabled.AddCell(celltot2);

                            PdfPCell celltot3 = new PdfPCell(new Phrase("Total ", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                            celltot3.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            tabled.AddCell(celltot3);


                            PdfPCell celldz4 = new PdfPCell(new Phrase(" " + (totalamount.ToString("#,##0.00")), FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                            celldz4.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                            celldz4.Colspan = 3;
                            tabled.AddCell(celldz4);

                            #endregion


                            document.Add(tabled);

                            PdfPTable tableServicetax = new PdfPTable(4);
                            tableServicetax.TotalWidth = 520;//432f;
                            tableServicetax.LockedWidth = true;
                            //tableServicetax.HorizontalAlignment = Element.ALIGN_RIGHT;
                            float[] widthdservicetax = new float[] { 1f, 1f, 2f, 1f };
                            tableServicetax.SetWidths(widthdservicetax);

                            string SqlQryForTaxes = "select * from  Tbloptions ";
                            DataTable DtTaxes = config.ExecuteAdaptorAsyncWithQueryParams(SqlQryForTaxes).Result;
                            string SCPersent = "";
                            if (DtTaxes.Rows.Count > 0)
                            {
                                SCPersent = DtTaxes.Rows[0]["ServiceTaxSeparate"].ToString();
                            }
                            else
                            {
                                LblResult.Text = "There Is No Tax Values For Generating Bills ";
                                return;
                            }


                            int pfstatus = 0;
                            int esistatus = 0;
                            int panstatus = 0;
                            int servicetaxstatus = 0;

                            #region Code for Service Charge


                            if (ServiceCharge.Length > 0)//bSCType == true)
                            {
                                float scharge = Convert.ToSingle(ServiceCharge);
                                if (scharge > 0)
                                {
                                    //if (servicecharge > 0 && ExtraDataSTcheck == false)
                                    //{
                                    if (PFNo.Trim().Length > 0)
                                    {
                                        pfstatus = 1;

                                        PdfPCell Pfno = new PdfPCell(new Phrase("EPF NO:", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        Pfno.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        //Pfno.Colspan = 2;
                                        Pfno.Border = 0;
                                        tableServicetax.AddCell(Pfno);


                                        PdfPCell Pfno1 = new PdfPCell(new Phrase(PFNo, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        Pfno1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        Pfno1.Border = 0;
                                        tableServicetax.AddCell(Pfno1);


                                    }

                                    else if (Esino.Trim().Length > 0)
                                    {
                                        esistatus = 1;

                                        PdfPCell ESino = new PdfPCell(new Phrase("ESI NO:", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        ESino.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        ESino.Border = 0;
                                        tableServicetax.AddCell(ESino);


                                        PdfPCell ESino1 = new PdfPCell(new Phrase(Esino, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        ESino1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        ESino1.Border = 0;
                                        tableServicetax.AddCell(ESino1);

                                    }
                                    else if (PANNO.Trim().Length > 0)
                                    {
                                        panstatus = 1;

                                        PdfPCell panno = new PdfPCell(new Phrase("PAN NO:", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        panno.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        panno.Border = 0;
                                        tableServicetax.AddCell(panno);

                                        PdfPCell panno1 = new PdfPCell(new Phrase(PANNO, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        panno1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        panno1.Border = 0;
                                        tableServicetax.AddCell(panno1);
                                    }
                                    else if (ServicetaxNo.Trim().Length > 0)
                                    {
                                        servicetaxstatus = 1;

                                        PdfPCell Staxno = new PdfPCell(new Phrase("Service Tax No:", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        Staxno.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        Staxno.Border = 0;
                                        tableServicetax.AddCell(Staxno);

                                        PdfPCell Staxno1 = new PdfPCell(new Phrase(ServicetaxNo, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        Staxno1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        Staxno1.Border = 0;
                                        tableServicetax.AddCell(Staxno1);
                                    }

                                    else
                                    {
                                        PdfPCell space = new PdfPCell(new Phrase(" ", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        space.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        space.Border = 0;
                                        space.Colspan = 2;
                                        tableServicetax.AddCell(space);
                                    }


                                    string SCharge = "";
                                    if (bSCType == false)
                                    {
                                        SCharge = ServiceCharge + " %";
                                    }
                                    else
                                    {
                                        SCharge = ServiceCharge;
                                    }
                                    PdfPCell celldc3 = new PdfPCell(new Phrase("Service Charges@ " + SCharge, FontFactory.GetFont(FontStyle, fontsize, Font.BOLD, BaseColor.BLACK)));
                                    celldc3.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                                    celldc3.Border = 0;
                                    // celldc3.PaddingLeft = -30;
                                    tableServicetax.AddCell(celldc3);

                                    PdfPCell celldc4 = new PdfPCell(new Phrase(servicecharge.ToString("#,##0.00"), FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                    celldc4.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                                    celldc4.Border = 0;
                                    // celldc4.PaddingLeft = -30;
                                    tableServicetax.AddCell(celldc4);
                                }
                            }

                            #endregion

                            #region New code for Service charge amount on Extradata if Service Tax on Service Charge as on 04/04/2014 by venkat

                            if (Extradatacheck == true && bStaxonExtradataservicecharges == true)
                            {
                                #region Extradata SCmachinary

                                if (SCMachinary == true && SCamtonMachinary > 0)
                                {

                                    if (PFNo.Trim().Length > 0 && pfstatus == 0)
                                    {
                                        pfstatus = 1;

                                        PdfPCell Pfno = new PdfPCell(new Phrase("EPF NO:", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        Pfno.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        //Pfno.Colspan = 2;
                                        Pfno.Border = 0;
                                        tableServicetax.AddCell(Pfno);


                                        PdfPCell Pfno1 = new PdfPCell(new Phrase(PFNo, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        Pfno1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        Pfno1.Border = 0;
                                        tableServicetax.AddCell(Pfno1);


                                    }

                                    else if (Esino.Trim().Length > 0 && esistatus == 0)
                                    {
                                        esistatus = 1;

                                        PdfPCell ESino = new PdfPCell(new Phrase("ESI NO:", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        ESino.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        ESino.Border = 0;
                                        tableServicetax.AddCell(ESino);


                                        PdfPCell ESino1 = new PdfPCell(new Phrase(Esino, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        ESino1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        ESino1.Border = 0;
                                        tableServicetax.AddCell(ESino1);

                                    }
                                    else if (PANNO.Trim().Length > 0 && panstatus == 0)
                                    {
                                        panstatus = 1;

                                        PdfPCell panno = new PdfPCell(new Phrase("PAN NO:", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        panno.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        panno.Border = 0;
                                        tableServicetax.AddCell(panno);

                                        PdfPCell panno1 = new PdfPCell(new Phrase(PANNO, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        panno1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        panno1.Border = 0;
                                        tableServicetax.AddCell(panno1);
                                    }
                                    else if (ServicetaxNo.Trim().Length > 0 && servicetaxstatus == 0)
                                    {
                                        servicetaxstatus = 1;

                                        PdfPCell Staxno = new PdfPCell(new Phrase("Service Tax No:", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        Staxno.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        Staxno.Border = 0;
                                        tableServicetax.AddCell(Staxno);

                                        PdfPCell Staxno1 = new PdfPCell(new Phrase(ServicetaxNo, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        Staxno1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        Staxno1.Border = 0;
                                        tableServicetax.AddCell(Staxno1);
                                    }

                                    else
                                    {
                                        PdfPCell space = new PdfPCell(new Phrase(" ", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        space.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        space.Border = 0;
                                        space.Colspan = 2;
                                        tableServicetax.AddCell(space);
                                    }

                                    PdfPCell celldcst1 = new PdfPCell(new Phrase("Service Charge on " + machinarycosttitle, FontFactory.GetFont(FontStyle, fontsize, Font.BOLD, BaseColor.BLACK)));
                                    celldcst1.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                                    celldcst1.Border = 0;
                                    tableServicetax.AddCell(celldcst1);

                                    PdfPCell celldcst2 = new PdfPCell(new Phrase(SCamtonMachinary.ToString("#,##0.00"), FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                    celldcst2.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                                    celldcst2.Border = 0;
                                    tableServicetax.AddCell(celldcst2);

                                }
                                #endregion

                                #region Extradata SCmaterial

                                if (SCMaterial == true && SCamtonMaterial > 0)
                                {

                                    if (PFNo.Trim().Length > 0 && pfstatus == 0)
                                    {
                                        pfstatus = 1;

                                        PdfPCell Pfno = new PdfPCell(new Phrase("EPF NO:", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        Pfno.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        //Pfno.Colspan = 2;
                                        Pfno.Border = 0;
                                        tableServicetax.AddCell(Pfno);


                                        PdfPCell Pfno1 = new PdfPCell(new Phrase(PFNo, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        Pfno1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        Pfno1.Border = 0;
                                        tableServicetax.AddCell(Pfno1);


                                    }

                                    else if (Esino.Trim().Length > 0 && esistatus == 0)
                                    {
                                        esistatus = 1;

                                        PdfPCell ESino = new PdfPCell(new Phrase("ESI NO:", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        ESino.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        ESino.Border = 0;
                                        tableServicetax.AddCell(ESino);


                                        PdfPCell ESino1 = new PdfPCell(new Phrase(Esino, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        ESino1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        ESino1.Border = 0;
                                        tableServicetax.AddCell(ESino1);

                                    }
                                    else if (PANNO.Trim().Length > 0 && panstatus == 0)
                                    {
                                        panstatus = 1;

                                        PdfPCell panno = new PdfPCell(new Phrase("PAN NO:", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        panno.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        panno.Border = 0;
                                        tableServicetax.AddCell(panno);

                                        PdfPCell panno1 = new PdfPCell(new Phrase(PANNO, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        panno1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        panno1.Border = 0;
                                        tableServicetax.AddCell(panno1);
                                    }
                                    else if (ServicetaxNo.Trim().Length > 0 && servicetaxstatus == 0)
                                    {
                                        servicetaxstatus = 1;

                                        PdfPCell Staxno = new PdfPCell(new Phrase("Service Tax No:", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        Staxno.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        Staxno.Border = 0;
                                        tableServicetax.AddCell(Staxno);

                                        PdfPCell Staxno1 = new PdfPCell(new Phrase(ServicetaxNo, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        Staxno1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        Staxno1.Border = 0;
                                        tableServicetax.AddCell(Staxno1);
                                    }

                                    else
                                    {
                                        PdfPCell space = new PdfPCell(new Phrase(" ", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        space.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        space.Border = 0;
                                        space.Colspan = 2;
                                        tableServicetax.AddCell(space);
                                    }

                                    PdfPCell celldcst1 = new PdfPCell(new Phrase("Service Charge on " + materialcosttitle, FontFactory.GetFont(FontStyle, fontsize, Font.BOLD, BaseColor.BLACK)));
                                    celldcst1.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                                    celldcst1.Border = 0;
                                    tableServicetax.AddCell(celldcst1);

                                    PdfPCell celldcst2 = new PdfPCell(new Phrase(SCamtonMaterial.ToString("#,##0.00"), FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                    celldcst2.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                                    celldcst2.Border = 0;
                                    tableServicetax.AddCell(celldcst2);

                                }
                                #endregion

                                #region Extradata SCmaintenance

                                if (SCMaintenance == true && SCamtonMaintenance > 0)
                                {

                                    if (PFNo.Trim().Length > 0 && pfstatus == 0)
                                    {
                                        pfstatus = 1;

                                        PdfPCell Pfno = new PdfPCell(new Phrase("EPF NO:", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        Pfno.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        //Pfno.Colspan = 2;
                                        Pfno.Border = 0;
                                        tableServicetax.AddCell(Pfno);


                                        PdfPCell Pfno1 = new PdfPCell(new Phrase(PFNo, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        Pfno1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        Pfno1.Border = 0;
                                        tableServicetax.AddCell(Pfno1);


                                    }

                                    else if (Esino.Trim().Length > 0 && esistatus == 0)
                                    {
                                        esistatus = 1;

                                        PdfPCell ESino = new PdfPCell(new Phrase("ESI NO:", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        ESino.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        ESino.Border = 0;
                                        tableServicetax.AddCell(ESino);


                                        PdfPCell ESino1 = new PdfPCell(new Phrase(Esino, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        ESino1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        ESino1.Border = 0;
                                        tableServicetax.AddCell(ESino1);

                                    }
                                    else if (PANNO.Trim().Length > 0 && panstatus == 0)
                                    {
                                        panstatus = 1;

                                        PdfPCell panno = new PdfPCell(new Phrase("PAN NO:", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        panno.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        panno.Border = 0;
                                        tableServicetax.AddCell(panno);

                                        PdfPCell panno1 = new PdfPCell(new Phrase(PANNO, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        panno1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        panno1.Border = 0;
                                        tableServicetax.AddCell(panno1);
                                    }
                                    else if (ServicetaxNo.Trim().Length > 0 && servicetaxstatus == 0)
                                    {
                                        servicetaxstatus = 1;

                                        PdfPCell Staxno = new PdfPCell(new Phrase("Service Tax No:", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        Staxno.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        Staxno.Border = 0;
                                        tableServicetax.AddCell(Staxno);

                                        PdfPCell Staxno1 = new PdfPCell(new Phrase(ServicetaxNo, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        Staxno1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        Staxno1.Border = 0;
                                        tableServicetax.AddCell(Staxno1);
                                    }

                                    else
                                    {
                                        PdfPCell space = new PdfPCell(new Phrase(" ", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        space.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        space.Border = 0;
                                        space.Colspan = 2;
                                        tableServicetax.AddCell(space);
                                    }

                                    PdfPCell celldcst1 = new PdfPCell(new Phrase("Service Charge on " + maintenancecosttitle, FontFactory.GetFont(FontStyle, fontsize, Font.BOLD, BaseColor.BLACK)));
                                    celldcst1.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                                    celldcst1.Border = 0;
                                    tableServicetax.AddCell(celldcst1);

                                    PdfPCell celldcst2 = new PdfPCell(new Phrase(SCamtonMaintenance.ToString("#,##0.00"), FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                    celldcst2.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                                    celldcst2.Border = 0;
                                    tableServicetax.AddCell(celldcst2);

                                }
                                #endregion

                                #region Extradata SCmaintenance

                                if (SCExtraone == true && SCamtonExtraone > 0)
                                {

                                    if (PFNo.Trim().Length > 0 && pfstatus == 0)
                                    {
                                        pfstatus = 1;

                                        PdfPCell Pfno = new PdfPCell(new Phrase("EPF NO:", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        Pfno.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        //Pfno.Colspan = 2;
                                        Pfno.Border = 0;
                                        tableServicetax.AddCell(Pfno);


                                        PdfPCell Pfno1 = new PdfPCell(new Phrase(PFNo, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        Pfno1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        Pfno1.Border = 0;
                                        tableServicetax.AddCell(Pfno1);


                                    }

                                    else if (Esino.Trim().Length > 0 && esistatus == 0)
                                    {
                                        esistatus = 1;

                                        PdfPCell ESino = new PdfPCell(new Phrase("ESI NO:", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        ESino.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        ESino.Border = 0;
                                        tableServicetax.AddCell(ESino);


                                        PdfPCell ESino1 = new PdfPCell(new Phrase(Esino, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        ESino1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        ESino1.Border = 0;
                                        tableServicetax.AddCell(ESino1);

                                    }
                                    else if (PANNO.Trim().Length > 0 && panstatus == 0)
                                    {
                                        panstatus = 1;

                                        PdfPCell panno = new PdfPCell(new Phrase("PAN NO:", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        panno.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        panno.Border = 0;
                                        tableServicetax.AddCell(panno);

                                        PdfPCell panno1 = new PdfPCell(new Phrase(PANNO, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        panno1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        panno1.Border = 0;
                                        tableServicetax.AddCell(panno1);
                                    }
                                    else if (ServicetaxNo.Trim().Length > 0 && servicetaxstatus == 0)
                                    {
                                        servicetaxstatus = 1;

                                        PdfPCell Staxno = new PdfPCell(new Phrase("Service Tax No:", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        Staxno.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        Staxno.Border = 0;
                                        tableServicetax.AddCell(Staxno);

                                        PdfPCell Staxno1 = new PdfPCell(new Phrase(ServicetaxNo, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        Staxno1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        Staxno1.Border = 0;
                                        tableServicetax.AddCell(Staxno1);
                                    }

                                    else
                                    {
                                        PdfPCell space = new PdfPCell(new Phrase(" ", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        space.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        space.Border = 0;
                                        space.Colspan = 2;
                                        tableServicetax.AddCell(space);
                                    }

                                    PdfPCell celldcst1 = new PdfPCell(new Phrase("Service Charge on " + extraonecosttitle, FontFactory.GetFont(FontStyle, fontsize, Font.BOLD, BaseColor.BLACK)));
                                    celldcst1.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                                    celldcst1.Border = 0;
                                    tableServicetax.AddCell(celldcst1);

                                    PdfPCell celldcst2 = new PdfPCell(new Phrase(SCamtonExtraone.ToString("#,##0.00"), FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                    celldcst2.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                                    celldcst2.Border = 0;
                                    tableServicetax.AddCell(celldcst2);

                                }
                                #endregion

                                #region Extradata SCmaintenance

                                if (SCExtratwo == true && SCamtonExtratwo > 0)
                                {

                                    if (PFNo.Trim().Length > 0 && pfstatus == 0)
                                    {
                                        pfstatus = 1;

                                        PdfPCell Pfno = new PdfPCell(new Phrase("EPF NO:", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        Pfno.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        //Pfno.Colspan = 2;
                                        Pfno.Border = 0;
                                        tableServicetax.AddCell(Pfno);


                                        PdfPCell Pfno1 = new PdfPCell(new Phrase(PFNo, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        Pfno1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        Pfno1.Border = 0;
                                        tableServicetax.AddCell(Pfno1);


                                    }

                                    else if (Esino.Trim().Length > 0 && esistatus == 0)
                                    {
                                        esistatus = 1;

                                        PdfPCell ESino = new PdfPCell(new Phrase("ESI NO:", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        ESino.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        ESino.Border = 0;
                                        tableServicetax.AddCell(ESino);


                                        PdfPCell ESino1 = new PdfPCell(new Phrase(Esino, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        ESino1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        ESino1.Border = 0;
                                        tableServicetax.AddCell(ESino1);

                                    }
                                    else if (PANNO.Trim().Length > 0 && panstatus == 0)
                                    {
                                        panstatus = 1;

                                        PdfPCell panno = new PdfPCell(new Phrase("PAN NO:", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        panno.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        panno.Border = 0;
                                        tableServicetax.AddCell(panno);

                                        PdfPCell panno1 = new PdfPCell(new Phrase(PANNO, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        panno1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        panno1.Border = 0;
                                        tableServicetax.AddCell(panno1);
                                    }
                                    else if (ServicetaxNo.Trim().Length > 0 && servicetaxstatus == 0)
                                    {
                                        servicetaxstatus = 1;

                                        PdfPCell Staxno = new PdfPCell(new Phrase("Service Tax No:", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        Staxno.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        Staxno.Border = 0;
                                        tableServicetax.AddCell(Staxno);

                                        PdfPCell Staxno1 = new PdfPCell(new Phrase(ServicetaxNo, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        Staxno1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        Staxno1.Border = 0;
                                        tableServicetax.AddCell(Staxno1);
                                    }

                                    else
                                    {
                                        PdfPCell space = new PdfPCell(new Phrase(" ", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        space.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        space.Border = 0;
                                        space.Colspan = 2;
                                        tableServicetax.AddCell(space);
                                    }

                                    PdfPCell celldcst1 = new PdfPCell(new Phrase("Service Charge on " + extratwocosttitle, FontFactory.GetFont(FontStyle, fontsize, Font.BOLD, BaseColor.BLACK)));
                                    celldcst1.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                                    celldcst1.Border = 0;
                                    tableServicetax.AddCell(celldcst1);

                                    PdfPCell celldcst2 = new PdfPCell(new Phrase(SCamtonExtratwo.ToString("#,##0.00"), FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                    celldcst2.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                                    celldcst2.Border = 0;
                                    tableServicetax.AddCell(celldcst2);

                                }
                                #endregion

                            }

                            #endregion

                            #region When Extra data is checked and STcheck is true


                            if (Extradatacheck == true)
                            {
                                #region Extradata Stmachinary


                                if (machinarycost > 0 && STMachinary == true)
                                {
                                    if (PFNo.Trim().Length > 0 && pfstatus == 0)
                                    {
                                        pfstatus = 1;

                                        PdfPCell Pfno = new PdfPCell(new Phrase("EPF NO:", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        Pfno.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        //Pfno.Colspan = 2;
                                        Pfno.Border = 0;
                                        tableServicetax.AddCell(Pfno);


                                        PdfPCell Pfno1 = new PdfPCell(new Phrase(PFNo, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        Pfno1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        Pfno1.Border = 0;
                                        tableServicetax.AddCell(Pfno1);


                                    }

                                    else if (Esino.Trim().Length > 0 && esistatus == 0)
                                    {
                                        esistatus = 1;

                                        PdfPCell ESino = new PdfPCell(new Phrase("ESI NO:", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        ESino.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        ESino.Border = 0;
                                        tableServicetax.AddCell(ESino);


                                        PdfPCell ESino1 = new PdfPCell(new Phrase(Esino, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        ESino1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        ESino1.Border = 0;
                                        tableServicetax.AddCell(ESino1);

                                    }
                                    else if (PANNO.Trim().Length > 0 && panstatus == 0)
                                    {
                                        panstatus = 1;

                                        PdfPCell panno = new PdfPCell(new Phrase("PAN NO:", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        panno.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        panno.Border = 0;
                                        tableServicetax.AddCell(panno);

                                        PdfPCell panno1 = new PdfPCell(new Phrase(PANNO, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        panno1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        panno1.Border = 0;
                                        tableServicetax.AddCell(panno1);
                                    }
                                    else if (ServicetaxNo.Trim().Length > 0 && servicetaxstatus == 0)
                                    {
                                        servicetaxstatus = 1;

                                        PdfPCell Staxno = new PdfPCell(new Phrase("Service Tax No:", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        Staxno.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        Staxno.Border = 0;
                                        tableServicetax.AddCell(Staxno);

                                        PdfPCell Staxno1 = new PdfPCell(new Phrase(ServicetaxNo, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        Staxno1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        Staxno1.Border = 0;
                                        tableServicetax.AddCell(Staxno1);
                                    }

                                    else
                                    {
                                        PdfPCell space = new PdfPCell(new Phrase(" ", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        space.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        space.Border = 0;
                                        space.Colspan = 2;
                                        tableServicetax.AddCell(space);
                                    }

                                    PdfPCell celldcst1 = new PdfPCell(new Phrase(machinarycosttitle, FontFactory.GetFont(FontStyle, fontsize, Font.BOLD, BaseColor.BLACK)));
                                    celldcst1.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                                    celldcst1.Border = 0;
                                    tableServicetax.AddCell(celldcst1);

                                    PdfPCell celldcst2 = new PdfPCell(new Phrase(machinarycost.ToString("#,##0.00"), FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                    celldcst2.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                                    celldcst2.Border = 0;
                                    tableServicetax.AddCell(celldcst2);

                                }

                                #endregion

                                #region Extradata StMaterial

                                if (materialcost > 0 && STMaterial == true)
                                {

                                    if (PFNo.Trim().Length > 0 && pfstatus == 0)
                                    {
                                        pfstatus = 1;

                                        PdfPCell Pfno = new PdfPCell(new Phrase("EPF NO:", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        Pfno.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        //Pfno.Colspan = 2;
                                        Pfno.Border = 0;
                                        tableServicetax.AddCell(Pfno);


                                        PdfPCell Pfno1 = new PdfPCell(new Phrase(PFNo, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        Pfno1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        Pfno1.Border = 0;
                                        tableServicetax.AddCell(Pfno1);


                                    }

                                    else if (Esino.Trim().Length > 0 && esistatus == 0)
                                    {
                                        esistatus = 1;

                                        PdfPCell ESino = new PdfPCell(new Phrase("ESI NO:", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        ESino.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        ESino.Border = 0;
                                        tableServicetax.AddCell(ESino);


                                        PdfPCell ESino1 = new PdfPCell(new Phrase(Esino, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        ESino1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        ESino1.Border = 0;
                                        tableServicetax.AddCell(ESino1);

                                    }
                                    else if (PANNO.Trim().Length > 0 && panstatus == 0)
                                    {
                                        panstatus = 1;

                                        PdfPCell panno = new PdfPCell(new Phrase("PAN NO:", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        panno.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        panno.Border = 0;
                                        tableServicetax.AddCell(panno);

                                        PdfPCell panno1 = new PdfPCell(new Phrase(PANNO, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        panno1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        panno1.Border = 0;
                                        tableServicetax.AddCell(panno1);
                                    }

                                    else if (ServicetaxNo.Trim().Length > 0 && servicetaxstatus == 0)
                                    {
                                        servicetaxstatus = 1;

                                        PdfPCell Staxno = new PdfPCell(new Phrase("Service Tax No:", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        Staxno.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        Staxno.Border = 0;
                                        tableServicetax.AddCell(Staxno);

                                        PdfPCell Staxno1 = new PdfPCell(new Phrase(ServicetaxNo, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        Staxno1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        Staxno1.Border = 0;
                                        tableServicetax.AddCell(Staxno1);
                                    }

                                    else
                                    {
                                        PdfPCell space = new PdfPCell(new Phrase(" ", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        space.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        space.Border = 0;
                                        space.Colspan = 2;
                                        tableServicetax.AddCell(space);
                                    }

                                    PdfPCell celldcst1 = new PdfPCell(new Phrase(materialcosttitle, FontFactory.GetFont(FontStyle, fontsize, Font.BOLD, BaseColor.BLACK)));
                                    celldcst1.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                                    celldcst1.Border = 0;
                                    tableServicetax.AddCell(celldcst1);

                                    PdfPCell celldcst2 = new PdfPCell(new Phrase(materialcost.ToString("#,##0.00"), FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                    celldcst2.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                                    celldcst2.Border = 0;
                                    tableServicetax.AddCell(celldcst2);

                                }

                                #endregion

                                #region Extradata Stmaintenance



                                if (maintenancecost > 0 && STMaintenance == true)
                                {

                                    if (PFNo.Trim().Length > 0 && pfstatus == 0)
                                    {
                                        pfstatus = 1;

                                        PdfPCell Pfno = new PdfPCell(new Phrase("EPF NO:", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        Pfno.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        //Pfno.Colspan = 2;
                                        Pfno.Border = 0;
                                        tableServicetax.AddCell(Pfno);


                                        PdfPCell Pfno1 = new PdfPCell(new Phrase(PFNo, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        Pfno1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        Pfno1.Border = 0;
                                        tableServicetax.AddCell(Pfno1);


                                    }

                                    else if (Esino.Trim().Length > 0 && esistatus == 0)
                                    {
                                        esistatus = 1;

                                        PdfPCell ESino = new PdfPCell(new Phrase("ESI NO:", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        ESino.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        ESino.Border = 0;
                                        tableServicetax.AddCell(ESino);


                                        PdfPCell ESino1 = new PdfPCell(new Phrase(Esino, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        ESino1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        ESino1.Border = 0;
                                        tableServicetax.AddCell(ESino1);

                                    }
                                    else if (PANNO.Trim().Length > 0 && panstatus == 0)
                                    {
                                        panstatus = 1;

                                        PdfPCell panno = new PdfPCell(new Phrase("PAN NO:", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        panno.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        panno.Border = 0;
                                        tableServicetax.AddCell(panno);

                                        PdfPCell panno1 = new PdfPCell(new Phrase(PANNO, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        panno1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        panno1.Border = 0;
                                        tableServicetax.AddCell(panno1);
                                    }

                                    else if (ServicetaxNo.Trim().Length > 0 && servicetaxstatus == 0)
                                    {
                                        servicetaxstatus = 1;

                                        PdfPCell Staxno = new PdfPCell(new Phrase("Service Tax No:", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        Staxno.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        Staxno.Border = 0;
                                        tableServicetax.AddCell(Staxno);

                                        PdfPCell Staxno1 = new PdfPCell(new Phrase(ServicetaxNo, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        Staxno1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        Staxno1.Border = 0;
                                        tableServicetax.AddCell(Staxno1);
                                    }

                                    else
                                    {
                                        PdfPCell space = new PdfPCell(new Phrase(" ", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        space.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        space.Border = 0;
                                        space.Colspan = 2;
                                        tableServicetax.AddCell(space);
                                    }

                                    PdfPCell celldcst1 = new PdfPCell(new Phrase(maintenancecosttitle, FontFactory.GetFont(FontStyle, fontsize, Font.BOLD, BaseColor.BLACK)));
                                    celldcst1.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                                    celldcst1.Border = 0;
                                    tableServicetax.AddCell(celldcst1);

                                    PdfPCell celldcst2 = new PdfPCell(new Phrase(maintenancecost.ToString("#,##0.00"), FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                    celldcst2.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                                    celldcst2.Border = 0;
                                    tableServicetax.AddCell(celldcst2);

                                }

                                #endregion

                                #region Extradata Stextraone



                                if (extraonecost > 0 && STExtraone == true)
                                {

                                    if (PFNo.Trim().Length > 0 && pfstatus == 0)
                                    {
                                        pfstatus = 1;

                                        PdfPCell Pfno = new PdfPCell(new Phrase("EPF NO:", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        Pfno.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        //Pfno.Colspan = 2;
                                        Pfno.Border = 0;
                                        tableServicetax.AddCell(Pfno);


                                        PdfPCell Pfno1 = new PdfPCell(new Phrase(PFNo, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        Pfno1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        Pfno1.Border = 0;
                                        tableServicetax.AddCell(Pfno1);


                                    }


                                    else if (Esino.Trim().Length > 0 && esistatus == 0)
                                    {
                                        esistatus = 1;

                                        PdfPCell ESino = new PdfPCell(new Phrase("ESI NO:", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        ESino.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        ESino.Border = 0;
                                        tableServicetax.AddCell(ESino);


                                        PdfPCell ESino1 = new PdfPCell(new Phrase(Esino, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        ESino1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        ESino1.Border = 0;
                                        tableServicetax.AddCell(ESino1);

                                    }
                                    else if (PANNO.Trim().Length > 0 && panstatus == 0)
                                    {
                                        panstatus = 1;

                                        PdfPCell panno = new PdfPCell(new Phrase("PAN NO:", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        panno.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        panno.Border = 0;
                                        tableServicetax.AddCell(panno);

                                        PdfPCell panno1 = new PdfPCell(new Phrase(PANNO, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        panno1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        panno1.Border = 0;
                                        tableServicetax.AddCell(panno1);
                                    }

                                    else if (ServicetaxNo.Trim().Length > 0 && servicetaxstatus == 0)
                                    {
                                        servicetaxstatus = 1;

                                        PdfPCell Staxno = new PdfPCell(new Phrase("Service Tax No:", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        Staxno.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        Staxno.Border = 0;
                                        tableServicetax.AddCell(Staxno);

                                        PdfPCell Staxno1 = new PdfPCell(new Phrase(ServicetaxNo, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        Staxno1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        Staxno1.Border = 0;
                                        tableServicetax.AddCell(Staxno1);
                                    }

                                    else
                                    {
                                        PdfPCell space = new PdfPCell(new Phrase(" ", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        space.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        space.Border = 0;
                                        space.Colspan = 2;
                                        tableServicetax.AddCell(space);
                                    }

                                    PdfPCell celldcst1 = new PdfPCell(new Phrase(extraonecosttitle, FontFactory.GetFont(FontStyle, fontsize, Font.BOLD, BaseColor.BLACK)));
                                    celldcst1.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                                    celldcst1.Border = 0;
                                    tableServicetax.AddCell(celldcst1);

                                    PdfPCell celldcst2 = new PdfPCell(new Phrase(extraonecost.ToString("#,##0.00"), FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                    celldcst2.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                                    celldcst2.Border = 0;
                                    tableServicetax.AddCell(celldcst2);

                                }


                                #endregion

                                #region Extradata Stextratwo



                                if (extratwocost > 0 && STExtratwo == true)
                                {

                                    if (PFNo.Trim().Length > 0 && pfstatus == 0)
                                    {
                                        pfstatus = 1;

                                        PdfPCell Pfno = new PdfPCell(new Phrase("EPF NO:", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        Pfno.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        //Pfno.Colspan = 2;
                                        Pfno.Border = 0;
                                        tableServicetax.AddCell(Pfno);


                                        PdfPCell Pfno1 = new PdfPCell(new Phrase(PFNo, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        Pfno1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        Pfno1.Border = 0;
                                        tableServicetax.AddCell(Pfno1);


                                    }

                                    else if (Esino.Trim().Length > 0 && esistatus == 0)
                                    {
                                        esistatus = 1;

                                        PdfPCell ESino = new PdfPCell(new Phrase("ESI NO:", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        ESino.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        ESino.Border = 0;
                                        tableServicetax.AddCell(ESino);


                                        PdfPCell ESino1 = new PdfPCell(new Phrase(Esino, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        ESino1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        ESino1.Border = 0;
                                        tableServicetax.AddCell(ESino1);

                                    }
                                    else if (PANNO.Trim().Length > 0 && panstatus == 0)
                                    {
                                        panstatus = 1;

                                        PdfPCell panno = new PdfPCell(new Phrase("PAN NO:", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        panno.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        panno.Border = 0;
                                        tableServicetax.AddCell(panno);

                                        PdfPCell panno1 = new PdfPCell(new Phrase(PANNO, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        panno1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        panno1.Border = 0;
                                        tableServicetax.AddCell(panno1);
                                    }

                                    else if (ServicetaxNo.Trim().Length > 0 && servicetaxstatus == 0)
                                    {
                                        servicetaxstatus = 1;

                                        PdfPCell Staxno = new PdfPCell(new Phrase("Service Tax No:", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        Staxno.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        Staxno.Border = 0;
                                        tableServicetax.AddCell(Staxno);

                                        PdfPCell Staxno1 = new PdfPCell(new Phrase(ServicetaxNo, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        Staxno1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        Staxno1.Border = 0;
                                        tableServicetax.AddCell(Staxno1);
                                    }

                                    else
                                    {
                                        PdfPCell space = new PdfPCell(new Phrase(" ", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        space.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        space.Border = 0;
                                        space.Colspan = 2;
                                        tableServicetax.AddCell(space);
                                    }

                                    PdfPCell celldcst1 = new PdfPCell(new Phrase(extratwocosttitle, FontFactory.GetFont(FontStyle, fontsize, Font.BOLD, BaseColor.BLACK)));
                                    celldcst1.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                                    celldcst1.Border = 0;
                                    tableServicetax.AddCell(celldcst1);

                                    PdfPCell celldcst2 = new PdfPCell(new Phrase(extratwocost.ToString("#,##0.00"), FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                    celldcst2.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                                    celldcst2.Border = 0;
                                    tableServicetax.AddCell(celldcst2);

                                }
                                #endregion

                                #region Discountone

                                if (discountone > 0 && STDiscountone == true)
                                {

                                    if (PFNo.Trim().Length > 0 && pfstatus == 0)
                                    {
                                        pfstatus = 1;

                                        PdfPCell Pfno = new PdfPCell(new Phrase("EPF NO:", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        Pfno.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        //Pfno.Colspan = 2;
                                        Pfno.Border = 0;
                                        tableServicetax.AddCell(Pfno);


                                        PdfPCell Pfno1 = new PdfPCell(new Phrase(PFNo, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        Pfno1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        Pfno1.Border = 0;
                                        tableServicetax.AddCell(Pfno1);


                                    }

                                    else if (Esino.Trim().Length > 0 && esistatus == 0)
                                    {
                                        esistatus = 1;

                                        PdfPCell ESino = new PdfPCell(new Phrase("ESI NO:", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        ESino.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        ESino.Border = 0;
                                        tableServicetax.AddCell(ESino);


                                        PdfPCell ESino1 = new PdfPCell(new Phrase(Esino, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        ESino1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        ESino1.Border = 0;
                                        tableServicetax.AddCell(ESino1);

                                    }
                                    else if (PANNO.Trim().Length > 0 && panstatus == 0)
                                    {
                                        panstatus = 1;

                                        PdfPCell panno = new PdfPCell(new Phrase("PAN NO:", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        panno.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        panno.Border = 0;
                                        tableServicetax.AddCell(panno);

                                        PdfPCell panno1 = new PdfPCell(new Phrase(PANNO, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        panno1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        panno1.Border = 0;
                                        tableServicetax.AddCell(panno1);
                                    }

                                    else if (ServicetaxNo.Trim().Length > 0 && servicetaxstatus == 0)
                                    {
                                        servicetaxstatus = 1;

                                        PdfPCell Staxno = new PdfPCell(new Phrase("Service Tax No:", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        Staxno.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        Staxno.Border = 0;
                                        tableServicetax.AddCell(Staxno);

                                        PdfPCell Staxno1 = new PdfPCell(new Phrase(ServicetaxNo, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        Staxno1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        Staxno1.Border = 0;
                                        tableServicetax.AddCell(Staxno1);
                                    }

                                    else
                                    {
                                        PdfPCell space = new PdfPCell(new Phrase(" ", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        space.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        space.Border = 0;
                                        space.Colspan = 2;
                                        tableServicetax.AddCell(space);
                                    }


                                    PdfPCell celldMt1 = new PdfPCell(new Phrase(discountonetitle, FontFactory.GetFont(FontStyle, fontsize, Font.BOLD, BaseColor.BLACK)));
                                    celldMt1.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                                    celldMt1.Border = 0;
                                    tableServicetax.AddCell(celldMt1);

                                    PdfPCell celldMt3 = new PdfPCell(new Phrase(discountone.ToString("#,##0.00"), FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                    celldMt3.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                                    celldMt3.Border = 0;
                                    tableServicetax.AddCell(celldMt3);
                                }

                                #endregion

                                #region Discounttwo


                                if (discounttwo > 0 && STDiscounttwo == true)
                                {
                                    if (PFNo.Trim().Length > 0 && pfstatus == 0)
                                    {
                                        pfstatus = 1;

                                        PdfPCell Pfno = new PdfPCell(new Phrase("EPF NO:", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        Pfno.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        //Pfno.Colspan = 2;
                                        Pfno.Border = 0;
                                        tableServicetax.AddCell(Pfno);


                                        PdfPCell Pfno1 = new PdfPCell(new Phrase(PFNo, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        Pfno1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        Pfno1.Border = 0;
                                        tableServicetax.AddCell(Pfno1);


                                    }

                                    else if (Esino.Trim().Length > 0 && esistatus == 0)
                                    {
                                        esistatus = 1;

                                        PdfPCell ESino = new PdfPCell(new Phrase("ESI NO:", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        ESino.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        ESino.Border = 0;
                                        tableServicetax.AddCell(ESino);


                                        PdfPCell ESino1 = new PdfPCell(new Phrase(Esino, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        ESino1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        ESino1.Border = 0;
                                        tableServicetax.AddCell(ESino1);

                                    }
                                    else if (PANNO.Trim().Length > 0 && panstatus == 0)
                                    {
                                        panstatus = 1;

                                        PdfPCell panno = new PdfPCell(new Phrase("PAN NO:", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        panno.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        panno.Border = 0;
                                        tableServicetax.AddCell(panno);

                                        PdfPCell panno1 = new PdfPCell(new Phrase(PANNO, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        panno1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        panno1.Border = 0;
                                        tableServicetax.AddCell(panno1);
                                    }

                                    else if (ServicetaxNo.Trim().Length > 0 && servicetaxstatus == 0)
                                    {
                                        servicetaxstatus = 1;

                                        PdfPCell Staxno = new PdfPCell(new Phrase("Service Tax No:", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        Staxno.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        Staxno.Border = 0;
                                        tableServicetax.AddCell(Staxno);

                                        PdfPCell Staxno1 = new PdfPCell(new Phrase(ServicetaxNo, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        Staxno1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        Staxno1.Border = 0;
                                        tableServicetax.AddCell(Staxno1);
                                    }

                                    else
                                    {
                                        PdfPCell space = new PdfPCell(new Phrase(" ", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        space.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        space.Border = 0;
                                        space.Colspan = 2;
                                        tableServicetax.AddCell(space);
                                    }


                                    PdfPCell celldMt1 = new PdfPCell(new Phrase(discounttwotitle, FontFactory.GetFont(FontStyle, fontsize, Font.BOLD, BaseColor.BLACK)));
                                    celldMt1.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                                    celldMt1.Border = 0;
                                    tableServicetax.AddCell(celldMt1);

                                    PdfPCell celldMt3 = new PdfPCell(new Phrase(discounttwo.ToString("#,##0.00"), FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                    celldMt3.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                                    celldMt3.Border = 0;
                                    tableServicetax.AddCell(celldMt3);
                                }
                                #endregion

                            }

                            #endregion

                            #region Code for Service Tax

                            if (!bIncludeST)
                            {

                                string scpercent = "";
                                if (bST75)
                                {
                                    scpercent = "3";
                                }
                                else
                                {
                                    scpercent = SCPersent;
                                }

                                if (PFNo.Trim().Length > 0 && pfstatus == 0)
                                {
                                    pfstatus = 1;

                                    PdfPCell Pfno = new PdfPCell(new Phrase("EPF NO:", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                    Pfno.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                    //Pfno.Colspan = 2;
                                    Pfno.Border = 0;
                                    tableServicetax.AddCell(Pfno);


                                    PdfPCell Pfno1 = new PdfPCell(new Phrase(PFNo, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                    Pfno1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                    Pfno1.Border = 0;
                                    tableServicetax.AddCell(Pfno1);


                                }

                                else if (Esino.Trim().Length > 0 && esistatus == 0)
                                {
                                    esistatus = 1;

                                    PdfPCell ESino = new PdfPCell(new Phrase("ESI NO:", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                    ESino.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                    ESino.Border = 0;
                                    tableServicetax.AddCell(ESino);


                                    PdfPCell ESino1 = new PdfPCell(new Phrase(Esino, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                    ESino1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                    ESino1.Border = 0;
                                    tableServicetax.AddCell(ESino1);

                                }
                                else if (PANNO.Trim().Length > 0 && panstatus == 0)
                                {
                                    panstatus = 1;

                                    PdfPCell panno = new PdfPCell(new Phrase("PAN NO:", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                    panno.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                    panno.Border = 0;
                                    tableServicetax.AddCell(panno);

                                    PdfPCell panno1 = new PdfPCell(new Phrase(PANNO, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                    panno1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                    panno1.Border = 0;
                                    tableServicetax.AddCell(panno1);
                                }

                                else if (ServicetaxNo.Trim().Length > 0 && servicetaxstatus == 0)
                                {
                                    servicetaxstatus = 1;

                                    PdfPCell Staxno = new PdfPCell(new Phrase("Service Tax No:", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                    Staxno.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                    Staxno.Border = 0;
                                    tableServicetax.AddCell(Staxno);

                                    PdfPCell Staxno1 = new PdfPCell(new Phrase(ServicetaxNo, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                    Staxno1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                    Staxno1.Border = 0;
                                    tableServicetax.AddCell(Staxno1);
                                }

                                else
                                {
                                    PdfPCell space = new PdfPCell(new Phrase(" ", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                    space.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                    space.Border = 0;
                                    space.Colspan = 2;
                                    tableServicetax.AddCell(space);
                                }


                                PdfPCell celldd3 = new PdfPCell(new Phrase("Service Tax@ " + scpercent + "%", FontFactory.GetFont(FontStyle, fontsize, Font.BOLD, BaseColor.BLACK)));
                                celldd3.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                                celldd3.Border = 0;
                                //celldd3.PaddingRight = -90;
                                tableServicetax.AddCell(celldd3);

                                PdfPCell celldd4 = new PdfPCell(new Phrase(servicetax.ToString("#,##0.00"), FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                celldd4.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                                celldd4.Border = 0;
                                // celldd4.PaddingRight = -30;
                                tableServicetax.AddCell(celldd4);

                                string CESSPresent = DtTaxes.Rows[0]["Cess"].ToString();


                                if (PFNo.Trim().Length > 0 && pfstatus == 0)
                                {
                                    pfstatus = 1;

                                    PdfPCell Pfno = new PdfPCell(new Phrase("EPF NO:", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                    Pfno.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                    //Pfno.Colspan = 2;
                                    Pfno.Border = 0;
                                    tableServicetax.AddCell(Pfno);


                                    PdfPCell Pfno1 = new PdfPCell(new Phrase(PFNo, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                    Pfno1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                    Pfno1.Border = 0;
                                    tableServicetax.AddCell(Pfno1);


                                }

                                else if (Esino.Trim().Length > 0 && esistatus == 0)
                                {
                                    esistatus = 1;

                                    PdfPCell ESino = new PdfPCell(new Phrase("ESI NO:", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                    ESino.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                    ESino.Border = 0;
                                    tableServicetax.AddCell(ESino);


                                    PdfPCell ESino1 = new PdfPCell(new Phrase(Esino, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                    ESino1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                    ESino1.Border = 0;
                                    tableServicetax.AddCell(ESino1);

                                }
                                else if (PANNO.Trim().Length > 0 && panstatus == 0)
                                {
                                    panstatus = 1;

                                    PdfPCell panno = new PdfPCell(new Phrase("PAN NO:", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                    panno.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                    panno.Border = 0;
                                    tableServicetax.AddCell(panno);

                                    PdfPCell panno1 = new PdfPCell(new Phrase(PANNO, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                    panno1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                    panno1.Border = 0;
                                    tableServicetax.AddCell(panno1);
                                }

                                else if (ServicetaxNo.Trim().Length > 0 && servicetaxstatus == 0)
                                {
                                    servicetaxstatus = 1;

                                    PdfPCell Staxno = new PdfPCell(new Phrase("Service Tax No:", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                    Staxno.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                    Staxno.Border = 0;
                                    tableServicetax.AddCell(Staxno);

                                    PdfPCell Staxno1 = new PdfPCell(new Phrase(ServicetaxNo, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                    Staxno1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                    Staxno1.Border = 0;
                                    tableServicetax.AddCell(Staxno1);
                                }

                                else
                                {
                                    PdfPCell space = new PdfPCell(new Phrase(" ", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                    space.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                    space.Border = 0;
                                    space.Colspan = 2;
                                    tableServicetax.AddCell(space);
                                }


                                PdfPCell cellde3 = new PdfPCell(new Phrase("Education CESS@  " + CESSPresent + "%", FontFactory.GetFont(FontStyle, fontsize, Font.BOLD, BaseColor.BLACK)));
                                cellde3.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                                //V1 = V1 + float.Parse(lblCESS.Text);
                                // cellde3.PaddingRight = -35;
                                cellde3.Border = 0;
                                tableServicetax.AddCell(cellde3);

                                PdfPCell cellde4 = new PdfPCell(new Phrase(cess.ToString("#,##0.00"), FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                cellde4.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                                cellde4.Border = 0;
                                //cellde4.PaddingRight = -30;
                                tableServicetax.AddCell(cellde4);

                                string SHECESSPresent = DtTaxes.Rows[0]["shecess"].ToString();

                                if (PFNo.Trim().Length > 0 && pfstatus == 0)
                                {
                                    pfstatus = 1;

                                    PdfPCell Pfno = new PdfPCell(new Phrase("EPF NO:", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                    Pfno.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                    //Pfno.Colspan = 2;
                                    Pfno.Border = 0;
                                    tableServicetax.AddCell(Pfno);


                                    PdfPCell Pfno1 = new PdfPCell(new Phrase(PFNo, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                    Pfno1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                    Pfno1.Border = 0;
                                    tableServicetax.AddCell(Pfno1);


                                }

                                else if (Esino.Trim().Length > 0 && esistatus == 0)
                                {
                                    esistatus = 1;

                                    PdfPCell ESino = new PdfPCell(new Phrase("ESI NO:", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                    ESino.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                    ESino.Border = 0;
                                    tableServicetax.AddCell(ESino);


                                    PdfPCell ESino1 = new PdfPCell(new Phrase(Esino, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                    ESino1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                    ESino1.Border = 0;
                                    tableServicetax.AddCell(ESino1);

                                }
                                else if (PANNO.Trim().Length > 0 && panstatus == 0)
                                {
                                    panstatus = 1;

                                    PdfPCell panno = new PdfPCell(new Phrase("PAN NO:", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                    panno.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                    panno.Border = 0;
                                    tableServicetax.AddCell(panno);

                                    PdfPCell panno1 = new PdfPCell(new Phrase(PANNO, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                    panno1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                    panno1.Border = 0;
                                    tableServicetax.AddCell(panno1);
                                }

                                else if (ServicetaxNo.Trim().Length > 0 && servicetaxstatus == 0)
                                {
                                    servicetaxstatus = 1;

                                    PdfPCell Staxno = new PdfPCell(new Phrase("Service Tax No:", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                    Staxno.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                    Staxno.Border = 0;
                                    tableServicetax.AddCell(Staxno);

                                    PdfPCell Staxno1 = new PdfPCell(new Phrase(ServicetaxNo, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                    Staxno1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                    Staxno1.Border = 0;
                                    tableServicetax.AddCell(Staxno1);
                                }

                                else
                                {
                                    PdfPCell space = new PdfPCell(new Phrase(" ", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                    space.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                    space.Border = 0;
                                    space.Colspan = 2;
                                    tableServicetax.AddCell(space);
                                }


                                PdfPCell celldf3 = new PdfPCell(new Phrase("Higher Education CESS@  " + SHECESSPresent + "%", FontFactory.GetFont(FontStyle, fontsize, Font.BOLD, BaseColor.BLACK)));
                                celldf3.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                                celldf3.Border = 0;
                                //cellde3.PaddingRight = -60;
                                tableServicetax.AddCell(celldf3);

                                PdfPCell celldf4 = new PdfPCell(new Phrase((servicetax * (double.Parse(SHECESSPresent) / 100)).ToString("0.00"),
                                    FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                celldf4.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                                celldf4.Border = 0;
                                tableServicetax.AddCell(celldf4);
                            }


                            #endregion

                            #region New code for Service charge amount on Extradata if Service Tax on Service Charge as on 04/04/2014 by venkat

                            if (Extradatacheck == true && bStaxonExtradataservicecharges == false)
                            {
                                #region Extradata SCmachinary

                                if (SCMachinary == true && SCamtonMachinary > 0)
                                {

                                    if (PFNo.Trim().Length > 0 && pfstatus == 0)
                                    {
                                        pfstatus = 1;

                                        PdfPCell Pfno = new PdfPCell(new Phrase("EPF NO:", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        Pfno.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        //Pfno.Colspan = 2;
                                        Pfno.Border = 0;
                                        tableServicetax.AddCell(Pfno);


                                        PdfPCell Pfno1 = new PdfPCell(new Phrase(PFNo, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        Pfno1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        Pfno1.Border = 0;
                                        tableServicetax.AddCell(Pfno1);


                                    }

                                    else if (Esino.Trim().Length > 0 && esistatus == 0)
                                    {
                                        esistatus = 1;

                                        PdfPCell ESino = new PdfPCell(new Phrase("ESI NO:", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        ESino.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        ESino.Border = 0;
                                        tableServicetax.AddCell(ESino);


                                        PdfPCell ESino1 = new PdfPCell(new Phrase(Esino, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        ESino1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        ESino1.Border = 0;
                                        tableServicetax.AddCell(ESino1);

                                    }
                                    else if (PANNO.Trim().Length > 0 && panstatus == 0)
                                    {
                                        panstatus = 1;

                                        PdfPCell panno = new PdfPCell(new Phrase("PAN NO:", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        panno.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        panno.Border = 0;
                                        tableServicetax.AddCell(panno);

                                        PdfPCell panno1 = new PdfPCell(new Phrase(PANNO, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        panno1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        panno1.Border = 0;
                                        tableServicetax.AddCell(panno1);
                                    }
                                    else if (ServicetaxNo.Trim().Length > 0 && servicetaxstatus == 0)
                                    {
                                        servicetaxstatus = 1;

                                        PdfPCell Staxno = new PdfPCell(new Phrase("Service Tax No:", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        Staxno.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        Staxno.Border = 0;
                                        tableServicetax.AddCell(Staxno);

                                        PdfPCell Staxno1 = new PdfPCell(new Phrase(ServicetaxNo, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        Staxno1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        Staxno1.Border = 0;
                                        tableServicetax.AddCell(Staxno1);
                                    }

                                    else
                                    {
                                        PdfPCell space = new PdfPCell(new Phrase(" ", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        space.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        space.Border = 0;
                                        space.Colspan = 2;
                                        tableServicetax.AddCell(space);
                                    }

                                    PdfPCell celldcst1 = new PdfPCell(new Phrase("Service Charge on " + machinarycosttitle, FontFactory.GetFont(FontStyle, fontsize, Font.BOLD, BaseColor.BLACK)));
                                    celldcst1.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                                    celldcst1.Border = 0;
                                    tableServicetax.AddCell(celldcst1);

                                    PdfPCell celldcst2 = new PdfPCell(new Phrase(SCamtonMachinary.ToString("#,##0.00"), FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                    celldcst2.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                                    celldcst2.Border = 0;
                                    tableServicetax.AddCell(celldcst2);

                                }
                                #endregion

                                #region Extradata SCmaterial

                                if (SCMaterial == true && SCamtonMaterial > 0)
                                {

                                    if (PFNo.Trim().Length > 0 && pfstatus == 0)
                                    {
                                        pfstatus = 1;

                                        PdfPCell Pfno = new PdfPCell(new Phrase("EPF NO:", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        Pfno.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        //Pfno.Colspan = 2;
                                        Pfno.Border = 0;
                                        tableServicetax.AddCell(Pfno);


                                        PdfPCell Pfno1 = new PdfPCell(new Phrase(PFNo, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        Pfno1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        Pfno1.Border = 0;
                                        tableServicetax.AddCell(Pfno1);


                                    }

                                    else if (Esino.Trim().Length > 0 && esistatus == 0)
                                    {
                                        esistatus = 1;

                                        PdfPCell ESino = new PdfPCell(new Phrase("ESI NO:", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        ESino.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        ESino.Border = 0;
                                        tableServicetax.AddCell(ESino);


                                        PdfPCell ESino1 = new PdfPCell(new Phrase(Esino, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        ESino1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        ESino1.Border = 0;
                                        tableServicetax.AddCell(ESino1);

                                    }
                                    else if (PANNO.Trim().Length > 0 && panstatus == 0)
                                    {
                                        panstatus = 1;

                                        PdfPCell panno = new PdfPCell(new Phrase("PAN NO:", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        panno.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        panno.Border = 0;
                                        tableServicetax.AddCell(panno);

                                        PdfPCell panno1 = new PdfPCell(new Phrase(PANNO, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        panno1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        panno1.Border = 0;
                                        tableServicetax.AddCell(panno1);
                                    }
                                    else if (ServicetaxNo.Trim().Length > 0 && servicetaxstatus == 0)
                                    {
                                        servicetaxstatus = 1;

                                        PdfPCell Staxno = new PdfPCell(new Phrase("Service Tax No:", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        Staxno.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        Staxno.Border = 0;
                                        tableServicetax.AddCell(Staxno);

                                        PdfPCell Staxno1 = new PdfPCell(new Phrase(ServicetaxNo, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        Staxno1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        Staxno1.Border = 0;
                                        tableServicetax.AddCell(Staxno1);
                                    }

                                    else
                                    {
                                        PdfPCell space = new PdfPCell(new Phrase(" ", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        space.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        space.Border = 0;
                                        space.Colspan = 2;
                                        tableServicetax.AddCell(space);
                                    }

                                    PdfPCell celldcst1 = new PdfPCell(new Phrase("Service Charge on " + materialcosttitle, FontFactory.GetFont(FontStyle, fontsize, Font.BOLD, BaseColor.BLACK)));
                                    celldcst1.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                                    celldcst1.Border = 0;
                                    tableServicetax.AddCell(celldcst1);

                                    PdfPCell celldcst2 = new PdfPCell(new Phrase(SCamtonMaterial.ToString("#,##0.00"), FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                    celldcst2.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                                    celldcst2.Border = 0;
                                    tableServicetax.AddCell(celldcst2);

                                }
                                #endregion

                                #region Extradata SCmaintenance

                                if (SCMaintenance == true && SCamtonMaintenance > 0)
                                {

                                    if (PFNo.Trim().Length > 0 && pfstatus == 0)
                                    {
                                        pfstatus = 1;

                                        PdfPCell Pfno = new PdfPCell(new Phrase("EPF NO:", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        Pfno.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        //Pfno.Colspan = 2;
                                        Pfno.Border = 0;
                                        tableServicetax.AddCell(Pfno);


                                        PdfPCell Pfno1 = new PdfPCell(new Phrase(PFNo, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        Pfno1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        Pfno1.Border = 0;
                                        tableServicetax.AddCell(Pfno1);


                                    }

                                    else if (Esino.Trim().Length > 0 && esistatus == 0)
                                    {
                                        esistatus = 1;

                                        PdfPCell ESino = new PdfPCell(new Phrase("ESI NO:", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        ESino.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        ESino.Border = 0;
                                        tableServicetax.AddCell(ESino);


                                        PdfPCell ESino1 = new PdfPCell(new Phrase(Esino, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        ESino1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        ESino1.Border = 0;
                                        tableServicetax.AddCell(ESino1);

                                    }
                                    else if (PANNO.Trim().Length > 0 && panstatus == 0)
                                    {
                                        panstatus = 1;

                                        PdfPCell panno = new PdfPCell(new Phrase("PAN NO:", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        panno.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        panno.Border = 0;
                                        tableServicetax.AddCell(panno);

                                        PdfPCell panno1 = new PdfPCell(new Phrase(PANNO, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        panno1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        panno1.Border = 0;
                                        tableServicetax.AddCell(panno1);
                                    }
                                    else if (ServicetaxNo.Trim().Length > 0 && servicetaxstatus == 0)
                                    {
                                        servicetaxstatus = 1;

                                        PdfPCell Staxno = new PdfPCell(new Phrase("Service Tax No:", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        Staxno.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        Staxno.Border = 0;
                                        tableServicetax.AddCell(Staxno);

                                        PdfPCell Staxno1 = new PdfPCell(new Phrase(ServicetaxNo, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        Staxno1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        Staxno1.Border = 0;
                                        tableServicetax.AddCell(Staxno1);
                                    }

                                    else
                                    {
                                        PdfPCell space = new PdfPCell(new Phrase(" ", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        space.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        space.Border = 0;
                                        space.Colspan = 2;
                                        tableServicetax.AddCell(space);
                                    }

                                    PdfPCell celldcst1 = new PdfPCell(new Phrase("Service Charge on " + maintenancecosttitle, FontFactory.GetFont(FontStyle, fontsize, Font.BOLD, BaseColor.BLACK)));
                                    celldcst1.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                                    celldcst1.Border = 0;
                                    tableServicetax.AddCell(celldcst1);

                                    PdfPCell celldcst2 = new PdfPCell(new Phrase(SCamtonMaintenance.ToString("#,##0.00"), FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                    celldcst2.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                                    celldcst2.Border = 0;
                                    tableServicetax.AddCell(celldcst2);

                                }
                                #endregion

                                #region Extradata SCmaintenance

                                if (SCExtraone == true && SCamtonExtraone > 0)
                                {

                                    if (PFNo.Trim().Length > 0 && pfstatus == 0)
                                    {
                                        pfstatus = 1;

                                        PdfPCell Pfno = new PdfPCell(new Phrase("EPF NO:", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        Pfno.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        //Pfno.Colspan = 2;
                                        Pfno.Border = 0;
                                        tableServicetax.AddCell(Pfno);


                                        PdfPCell Pfno1 = new PdfPCell(new Phrase(PFNo, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        Pfno1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        Pfno1.Border = 0;
                                        tableServicetax.AddCell(Pfno1);


                                    }

                                    else if (Esino.Trim().Length > 0 && esistatus == 0)
                                    {
                                        esistatus = 1;

                                        PdfPCell ESino = new PdfPCell(new Phrase("ESI NO:", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        ESino.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        ESino.Border = 0;
                                        tableServicetax.AddCell(ESino);


                                        PdfPCell ESino1 = new PdfPCell(new Phrase(Esino, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        ESino1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        ESino1.Border = 0;
                                        tableServicetax.AddCell(ESino1);

                                    }
                                    else if (PANNO.Trim().Length > 0 && panstatus == 0)
                                    {
                                        panstatus = 1;

                                        PdfPCell panno = new PdfPCell(new Phrase("PAN NO:", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        panno.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        panno.Border = 0;
                                        tableServicetax.AddCell(panno);

                                        PdfPCell panno1 = new PdfPCell(new Phrase(PANNO, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        panno1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        panno1.Border = 0;
                                        tableServicetax.AddCell(panno1);
                                    }
                                    else if (ServicetaxNo.Trim().Length > 0 && servicetaxstatus == 0)
                                    {
                                        servicetaxstatus = 1;

                                        PdfPCell Staxno = new PdfPCell(new Phrase("Service Tax No:", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        Staxno.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        Staxno.Border = 0;
                                        tableServicetax.AddCell(Staxno);

                                        PdfPCell Staxno1 = new PdfPCell(new Phrase(ServicetaxNo, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        Staxno1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        Staxno1.Border = 0;
                                        tableServicetax.AddCell(Staxno1);
                                    }

                                    else
                                    {
                                        PdfPCell space = new PdfPCell(new Phrase(" ", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        space.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        space.Border = 0;
                                        space.Colspan = 2;
                                        tableServicetax.AddCell(space);
                                    }

                                    PdfPCell celldcst1 = new PdfPCell(new Phrase("Service Charge on " + extraonecosttitle, FontFactory.GetFont(FontStyle, fontsize, Font.BOLD, BaseColor.BLACK)));
                                    celldcst1.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                                    celldcst1.Border = 0;
                                    tableServicetax.AddCell(celldcst1);

                                    PdfPCell celldcst2 = new PdfPCell(new Phrase(SCamtonExtraone.ToString("#,##0.00"), FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                    celldcst2.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                                    celldcst2.Border = 0;
                                    tableServicetax.AddCell(celldcst2);

                                }
                                #endregion

                                #region Extradata SCmaintenance

                                if (SCExtratwo == true && SCamtonExtratwo > 0)
                                {

                                    if (PFNo.Trim().Length > 0 && pfstatus == 0)
                                    {
                                        pfstatus = 1;

                                        PdfPCell Pfno = new PdfPCell(new Phrase("EPF NO:", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        Pfno.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        //Pfno.Colspan = 2;
                                        Pfno.Border = 0;
                                        tableServicetax.AddCell(Pfno);


                                        PdfPCell Pfno1 = new PdfPCell(new Phrase(PFNo, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        Pfno1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        Pfno1.Border = 0;
                                        tableServicetax.AddCell(Pfno1);


                                    }

                                    else if (Esino.Trim().Length > 0 && esistatus == 0)
                                    {
                                        esistatus = 1;

                                        PdfPCell ESino = new PdfPCell(new Phrase("ESI NO:", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        ESino.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        ESino.Border = 0;
                                        tableServicetax.AddCell(ESino);


                                        PdfPCell ESino1 = new PdfPCell(new Phrase(Esino, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        ESino1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        ESino1.Border = 0;
                                        tableServicetax.AddCell(ESino1);

                                    }
                                    else if (PANNO.Trim().Length > 0 && panstatus == 0)
                                    {
                                        panstatus = 1;

                                        PdfPCell panno = new PdfPCell(new Phrase("PAN NO:", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        panno.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        panno.Border = 0;
                                        tableServicetax.AddCell(panno);

                                        PdfPCell panno1 = new PdfPCell(new Phrase(PANNO, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        panno1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        panno1.Border = 0;
                                        tableServicetax.AddCell(panno1);
                                    }
                                    else if (ServicetaxNo.Trim().Length > 0 && servicetaxstatus == 0)
                                    {
                                        servicetaxstatus = 1;

                                        PdfPCell Staxno = new PdfPCell(new Phrase("Service Tax No:", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        Staxno.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        Staxno.Border = 0;
                                        tableServicetax.AddCell(Staxno);

                                        PdfPCell Staxno1 = new PdfPCell(new Phrase(ServicetaxNo, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        Staxno1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        Staxno1.Border = 0;
                                        tableServicetax.AddCell(Staxno1);
                                    }

                                    else
                                    {
                                        PdfPCell space = new PdfPCell(new Phrase(" ", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        space.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        space.Border = 0;
                                        space.Colspan = 2;
                                        tableServicetax.AddCell(space);
                                    }

                                    PdfPCell celldcst1 = new PdfPCell(new Phrase("Service Charge on " + extratwocosttitle, FontFactory.GetFont(FontStyle, fontsize, Font.BOLD, BaseColor.BLACK)));
                                    celldcst1.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                                    celldcst1.Border = 0;
                                    tableServicetax.AddCell(celldcst1);

                                    PdfPCell celldcst2 = new PdfPCell(new Phrase(SCamtonExtratwo.ToString("#,##0.00"), FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                    celldcst2.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                                    celldcst2.Border = 0;
                                    tableServicetax.AddCell(celldcst2);

                                }
                                #endregion

                            }

                            #endregion

                            #region When Extradata check is True and STcheck is false


                            if (Extradatacheck == true)
                            {
                                #region Machinary Cost

                                if (machinarycost > 0 && STMachinary == false)
                                {
                                    if (PFNo.Trim().Length > 0 && pfstatus == 0)
                                    {
                                        pfstatus = 1;

                                        PdfPCell Pfno = new PdfPCell(new Phrase("EPF NO:", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        Pfno.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        //Pfno.Colspan = 2;
                                        Pfno.Border = 0;
                                        tableServicetax.AddCell(Pfno);


                                        PdfPCell Pfno1 = new PdfPCell(new Phrase(PFNo, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        Pfno1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        Pfno1.Border = 0;
                                        tableServicetax.AddCell(Pfno1);


                                    }

                                    else if (Esino.Trim().Length > 0 && esistatus == 0)
                                    {
                                        esistatus = 1;

                                        PdfPCell ESino = new PdfPCell(new Phrase("ESI NO:", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        ESino.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        ESino.Border = 0;
                                        tableServicetax.AddCell(ESino);


                                        PdfPCell ESino1 = new PdfPCell(new Phrase(Esino, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        ESino1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        ESino1.Border = 0;
                                        tableServicetax.AddCell(ESino1);

                                    }
                                    else if (PANNO.Trim().Length > 0 && panstatus == 0)
                                    {
                                        panstatus = 1;

                                        PdfPCell panno = new PdfPCell(new Phrase("PAN NO:", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        panno.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        panno.Border = 0;
                                        tableServicetax.AddCell(panno);

                                        PdfPCell panno1 = new PdfPCell(new Phrase(PANNO, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        panno1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        panno1.Border = 0;
                                        tableServicetax.AddCell(panno1);
                                    }

                                    else if (ServicetaxNo.Trim().Length > 0 && servicetaxstatus == 0)
                                    {
                                        servicetaxstatus = 1;

                                        PdfPCell Staxno = new PdfPCell(new Phrase("Service Tax No:", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        Staxno.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        Staxno.Border = 0;
                                        tableServicetax.AddCell(Staxno);

                                        PdfPCell Staxno1 = new PdfPCell(new Phrase(ServicetaxNo, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        Staxno1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        Staxno1.Border = 0;
                                        tableServicetax.AddCell(Staxno1);
                                    }

                                    else
                                    {
                                        PdfPCell space = new PdfPCell(new Phrase(" ", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        space.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        space.Border = 0;
                                        space.Colspan = 2;
                                        tableServicetax.AddCell(space);
                                    }


                                    PdfPCell celldMeci1 = new PdfPCell(new Phrase(machinarycosttitle, FontFactory.GetFont(FontStyle, fontsize, Font.BOLD, BaseColor.BLACK)));
                                    celldMeci1.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                                    celldMeci1.Border = 0;
                                    tableServicetax.AddCell(celldMeci1);

                                    PdfPCell celldMeci3 = new PdfPCell(new Phrase(machinarycost.ToString("#,##0.00"), FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                    celldMeci3.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                                    celldMeci3.Border = 0;
                                    tableServicetax.AddCell(celldMeci3);
                                }

                                #endregion

                                #region Material Cost

                                if (materialcost > 0 && STMaterial == false)
                                {
                                    if (PFNo.Trim().Length > 0 && pfstatus == 0)
                                    {
                                        pfstatus = 1;

                                        PdfPCell Pfno = new PdfPCell(new Phrase("EPF NO:", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        Pfno.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        //Pfno.Colspan = 2;
                                        Pfno.Border = 0;
                                        tableServicetax.AddCell(Pfno);


                                        PdfPCell Pfno1 = new PdfPCell(new Phrase(PFNo, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        Pfno1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        Pfno1.Border = 0;
                                        tableServicetax.AddCell(Pfno1);


                                    }

                                    else if (Esino.Trim().Length > 0 && esistatus == 0)
                                    {
                                        esistatus = 1;

                                        PdfPCell ESino = new PdfPCell(new Phrase("ESI NO:", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        ESino.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        ESino.Border = 0;
                                        tableServicetax.AddCell(ESino);


                                        PdfPCell ESino1 = new PdfPCell(new Phrase(Esino, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        ESino1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        ESino1.Border = 0;
                                        tableServicetax.AddCell(ESino1);

                                    }
                                    else if (PANNO.Trim().Length > 0 && panstatus == 0)
                                    {
                                        panstatus = 1;

                                        PdfPCell panno = new PdfPCell(new Phrase("PAN NO:", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        panno.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        panno.Border = 0;
                                        tableServicetax.AddCell(panno);

                                        PdfPCell panno1 = new PdfPCell(new Phrase(PANNO, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        panno1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        panno1.Border = 0;
                                        tableServicetax.AddCell(panno1);
                                    }

                                    else if (ServicetaxNo.Trim().Length > 0 && servicetaxstatus == 0)
                                    {
                                        servicetaxstatus = 1;

                                        PdfPCell Staxno = new PdfPCell(new Phrase("Service Tax No:", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        Staxno.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        Staxno.Border = 0;
                                        tableServicetax.AddCell(Staxno);

                                        PdfPCell Staxno1 = new PdfPCell(new Phrase(ServicetaxNo, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        Staxno1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        Staxno1.Border = 0;
                                        tableServicetax.AddCell(Staxno1);
                                    }

                                    else
                                    {
                                        PdfPCell space = new PdfPCell(new Phrase(" ", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        space.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        space.Border = 0;
                                        space.Colspan = 2;
                                        tableServicetax.AddCell(space);
                                    }


                                    PdfPCell celldMt1 = new PdfPCell(new Phrase(materialcosttitle, FontFactory.GetFont(FontStyle, fontsize, Font.BOLD, BaseColor.BLACK)));
                                    celldMt1.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                                    celldMt1.Border = 0;
                                    tableServicetax.AddCell(celldMt1);

                                    PdfPCell celldMt3 = new PdfPCell(new Phrase(materialcost.ToString("#,##0.00"), FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                    celldMt3.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                                    celldMt3.Border = 0;
                                    tableServicetax.AddCell(celldMt3);
                                }

                                #endregion

                                #region Maintenance Cost

                                if (maintenancecost > 0 && STMaintenance == false)
                                {
                                    if (PFNo.Trim().Length > 0 && pfstatus == 0)
                                    {
                                        pfstatus = 1;

                                        PdfPCell Pfno = new PdfPCell(new Phrase("EPF NO:", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        Pfno.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        //Pfno.Colspan = 2;
                                        Pfno.Border = 0;
                                        tableServicetax.AddCell(Pfno);


                                        PdfPCell Pfno1 = new PdfPCell(new Phrase(PFNo, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        Pfno1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        Pfno1.Border = 0;
                                        tableServicetax.AddCell(Pfno1);


                                    }

                                    else if (Esino.Trim().Length > 0 && esistatus == 0)
                                    {
                                        esistatus = 1;

                                        PdfPCell ESino = new PdfPCell(new Phrase("ESI NO:", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        ESino.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        ESino.Border = 0;
                                        tableServicetax.AddCell(ESino);


                                        PdfPCell ESino1 = new PdfPCell(new Phrase(Esino, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        ESino1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        ESino1.Border = 0;
                                        tableServicetax.AddCell(ESino1);

                                    }
                                    else if (PANNO.Trim().Length > 0 && panstatus == 0)
                                    {
                                        panstatus = 1;

                                        PdfPCell panno = new PdfPCell(new Phrase("PAN NO:", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        panno.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        panno.Border = 0;
                                        tableServicetax.AddCell(panno);

                                        PdfPCell panno1 = new PdfPCell(new Phrase(PANNO, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        panno1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        panno1.Border = 0;
                                        tableServicetax.AddCell(panno1);
                                    }

                                    else if (ServicetaxNo.Trim().Length > 0 && servicetaxstatus == 0)
                                    {
                                        servicetaxstatus = 1;

                                        PdfPCell Staxno = new PdfPCell(new Phrase("Service Tax No:", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        Staxno.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        Staxno.Border = 0;
                                        tableServicetax.AddCell(Staxno);

                                        PdfPCell Staxno1 = new PdfPCell(new Phrase(ServicetaxNo, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        Staxno1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        Staxno1.Border = 0;
                                        tableServicetax.AddCell(Staxno1);
                                    }

                                    else
                                    {
                                        PdfPCell space = new PdfPCell(new Phrase(" ", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        space.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        space.Border = 0;
                                        space.Colspan = 2;
                                        tableServicetax.AddCell(space);
                                    }


                                    PdfPCell celldMt1 = new PdfPCell(new Phrase(maintenancecosttitle, FontFactory.GetFont(FontStyle, fontsize, Font.BOLD, BaseColor.BLACK)));
                                    celldMt1.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                                    celldMt1.Border = 0;
                                    tableServicetax.AddCell(celldMt1);

                                    PdfPCell celldMt3 = new PdfPCell(new Phrase(maintenancecost.ToString("#,##0.00"), FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                    celldMt3.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                                    celldMt3.Border = 0;
                                    tableServicetax.AddCell(celldMt3);
                                }

                                #endregion

                                #region Extra amount two

                                if (extraonecost > 0 && STExtraone == false)
                                {

                                    if (PFNo.Trim().Length > 0 && pfstatus == 0)
                                    {
                                        pfstatus = 1;

                                        PdfPCell Pfno = new PdfPCell(new Phrase("EPF NO:", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        Pfno.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        //Pfno.Colspan = 2;
                                        Pfno.Border = 0;
                                        tableServicetax.AddCell(Pfno);


                                        PdfPCell Pfno1 = new PdfPCell(new Phrase(PFNo, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        Pfno1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        Pfno1.Border = 0;
                                        tableServicetax.AddCell(Pfno1);


                                    }

                                    else if (Esino.Trim().Length > 0 && esistatus == 0)
                                    {
                                        esistatus = 1;

                                        PdfPCell ESino = new PdfPCell(new Phrase("ESI NO:", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        ESino.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        ESino.Border = 0;
                                        tableServicetax.AddCell(ESino);


                                        PdfPCell ESino1 = new PdfPCell(new Phrase(Esino, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        ESino1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        ESino1.Border = 0;
                                        tableServicetax.AddCell(ESino1);

                                    }
                                    else if (PANNO.Trim().Length > 0 && panstatus == 0)
                                    {
                                        panstatus = 1;

                                        PdfPCell panno = new PdfPCell(new Phrase("PAN NO:", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        panno.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        panno.Border = 0;
                                        tableServicetax.AddCell(panno);

                                        PdfPCell panno1 = new PdfPCell(new Phrase(PANNO, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        panno1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        panno1.Border = 0;
                                        tableServicetax.AddCell(panno1);
                                    }

                                    else if (ServicetaxNo.Trim().Length > 0 && servicetaxstatus == 0)
                                    {
                                        servicetaxstatus = 1;

                                        PdfPCell Staxno = new PdfPCell(new Phrase("Service Tax No:", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        Staxno.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        Staxno.Border = 0;
                                        tableServicetax.AddCell(Staxno);

                                        PdfPCell Staxno1 = new PdfPCell(new Phrase(ServicetaxNo, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        Staxno1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        Staxno1.Border = 0;
                                        tableServicetax.AddCell(Staxno1);
                                    }

                                    else
                                    {
                                        PdfPCell space = new PdfPCell(new Phrase(" ", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        space.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        space.Border = 0;
                                        space.Colspan = 2;
                                        tableServicetax.AddCell(space);
                                    }


                                    PdfPCell celldMt1 = new PdfPCell(new Phrase(extraonecosttitle, FontFactory.GetFont(FontStyle, fontsize, Font.BOLD, BaseColor.BLACK)));
                                    celldMt1.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                                    celldMt1.Border = 0;
                                    tableServicetax.AddCell(celldMt1);

                                    PdfPCell celldMt3 = new PdfPCell(new Phrase(extraonecost.ToString("#,##0.00"), FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                    celldMt3.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                                    celldMt3.Border = 0;
                                    tableServicetax.AddCell(celldMt3);
                                }

                                #endregion

                                #region Extraamount two

                                if (extratwocost > 0 && STExtratwo == false)
                                {
                                    if (PFNo.Trim().Length > 0 && pfstatus == 0)
                                    {
                                        pfstatus = 1;

                                        PdfPCell Pfno = new PdfPCell(new Phrase("EPF NO:", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        Pfno.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        //Pfno.Colspan = 2;
                                        Pfno.Border = 0;
                                        tableServicetax.AddCell(Pfno);


                                        PdfPCell Pfno1 = new PdfPCell(new Phrase(PFNo, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        Pfno1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        Pfno1.Border = 0;
                                        tableServicetax.AddCell(Pfno1);


                                    }

                                    else if (Esino.Trim().Length > 0 && esistatus == 0)
                                    {
                                        esistatus = 1;

                                        PdfPCell ESino = new PdfPCell(new Phrase("ESI NO:", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        ESino.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        ESino.Border = 0;
                                        tableServicetax.AddCell(ESino);


                                        PdfPCell ESino1 = new PdfPCell(new Phrase(Esino, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        ESino1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        ESino1.Border = 0;
                                        tableServicetax.AddCell(ESino1);

                                    }
                                    else if (PANNO.Trim().Length > 0 && panstatus == 0)
                                    {
                                        panstatus = 1;

                                        PdfPCell panno = new PdfPCell(new Phrase("PAN NO:", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        panno.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        panno.Border = 0;
                                        tableServicetax.AddCell(panno);

                                        PdfPCell panno1 = new PdfPCell(new Phrase(PANNO, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        panno1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        panno1.Border = 0;
                                        tableServicetax.AddCell(panno1);
                                    }

                                    else if (ServicetaxNo.Trim().Length > 0 && servicetaxstatus == 0)
                                    {
                                        servicetaxstatus = 1;

                                        PdfPCell Staxno = new PdfPCell(new Phrase("Service Tax No:", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        Staxno.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        Staxno.Border = 0;
                                        tableServicetax.AddCell(Staxno);

                                        PdfPCell Staxno1 = new PdfPCell(new Phrase(ServicetaxNo, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        Staxno1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        Staxno1.Border = 0;
                                        tableServicetax.AddCell(Staxno1);
                                    }

                                    else
                                    {
                                        PdfPCell space = new PdfPCell(new Phrase(" ", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        space.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        space.Border = 0;
                                        space.Colspan = 2;
                                        tableServicetax.AddCell(space);
                                    }


                                    PdfPCell celldMt1 = new PdfPCell(new Phrase(extratwocosttitle, FontFactory.GetFont(FontStyle, fontsize, Font.BOLD, BaseColor.BLACK)));
                                    celldMt1.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                                    celldMt1.Border = 0;
                                    tableServicetax.AddCell(celldMt1);

                                    PdfPCell celldMt3 = new PdfPCell(new Phrase(extratwocost.ToString("#,##0.00"), FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                    celldMt3.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                                    celldMt3.Border = 0;
                                    tableServicetax.AddCell(celldMt3);
                                }

                                #endregion

                                #region Discountone

                                if (discountone > 0 && STDiscountone == false)
                                {

                                    if (PFNo.Trim().Length > 0 && pfstatus == 0)
                                    {
                                        pfstatus = 1;

                                        PdfPCell Pfno = new PdfPCell(new Phrase("EPF NO:", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        Pfno.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        //Pfno.Colspan = 2;
                                        Pfno.Border = 0;
                                        tableServicetax.AddCell(Pfno);


                                        PdfPCell Pfno1 = new PdfPCell(new Phrase(PFNo, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        Pfno1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        Pfno1.Border = 0;
                                        tableServicetax.AddCell(Pfno1);


                                    }

                                    else if (Esino.Trim().Length > 0 && esistatus == 0)
                                    {
                                        esistatus = 1;

                                        PdfPCell ESino = new PdfPCell(new Phrase("ESI NO:", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        ESino.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        ESino.Border = 0;
                                        tableServicetax.AddCell(ESino);


                                        PdfPCell ESino1 = new PdfPCell(new Phrase(Esino, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        ESino1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        ESino1.Border = 0;
                                        tableServicetax.AddCell(ESino1);

                                    }
                                    else if (PANNO.Trim().Length > 0 && panstatus == 0)
                                    {
                                        panstatus = 1;

                                        PdfPCell panno = new PdfPCell(new Phrase("PAN NO:", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        panno.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        panno.Border = 0;
                                        tableServicetax.AddCell(panno);

                                        PdfPCell panno1 = new PdfPCell(new Phrase(PANNO, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        panno1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        panno1.Border = 0;
                                        tableServicetax.AddCell(panno1);
                                    }

                                    else if (ServicetaxNo.Trim().Length > 0 && servicetaxstatus == 0)
                                    {
                                        servicetaxstatus = 1;

                                        PdfPCell Staxno = new PdfPCell(new Phrase("Service Tax No:", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        Staxno.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        Staxno.Border = 0;
                                        tableServicetax.AddCell(Staxno);

                                        PdfPCell Staxno1 = new PdfPCell(new Phrase(ServicetaxNo, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        Staxno1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        Staxno1.Border = 0;
                                        tableServicetax.AddCell(Staxno1);
                                    }

                                    else
                                    {
                                        PdfPCell space = new PdfPCell(new Phrase(" ", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        space.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        space.Border = 0;
                                        space.Colspan = 2;
                                        tableServicetax.AddCell(space);
                                    }


                                    PdfPCell celldMt1 = new PdfPCell(new Phrase(discountonetitle, FontFactory.GetFont(FontStyle, fontsize, Font.BOLD, BaseColor.BLACK)));
                                    celldMt1.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                                    celldMt1.Border = 0;
                                    tableServicetax.AddCell(celldMt1);

                                    PdfPCell celldMt3 = new PdfPCell(new Phrase(discountone.ToString("#,##0.00"), FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                    celldMt3.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                                    celldMt3.Border = 0;
                                    tableServicetax.AddCell(celldMt3);
                                }

                                #endregion

                                #region Discounttwo


                                if (discounttwo > 0 && STDiscounttwo == false)
                                {
                                    if (PFNo.Trim().Length > 0 && pfstatus == 0)
                                    {
                                        pfstatus = 1;

                                        PdfPCell Pfno = new PdfPCell(new Phrase("EPF NO:", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        Pfno.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        //Pfno.Colspan = 2;
                                        Pfno.Border = 0;
                                        tableServicetax.AddCell(Pfno);


                                        PdfPCell Pfno1 = new PdfPCell(new Phrase(PFNo, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        Pfno1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        Pfno1.Border = 0;
                                        tableServicetax.AddCell(Pfno1);


                                    }

                                    else if (Esino.Trim().Length > 0 && esistatus == 0)
                                    {
                                        esistatus = 1;

                                        PdfPCell ESino = new PdfPCell(new Phrase("ESI NO:", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        ESino.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        ESino.Border = 0;
                                        tableServicetax.AddCell(ESino);


                                        PdfPCell ESino1 = new PdfPCell(new Phrase(Esino, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        ESino1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        ESino1.Border = 0;
                                        tableServicetax.AddCell(ESino1);

                                    }
                                    else if (PANNO.Trim().Length > 0 && panstatus == 0)
                                    {
                                        panstatus = 1;

                                        PdfPCell panno = new PdfPCell(new Phrase("PAN NO:", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        panno.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        panno.Border = 0;
                                        tableServicetax.AddCell(panno);

                                        PdfPCell panno1 = new PdfPCell(new Phrase(PANNO, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        panno1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        panno1.Border = 0;
                                        tableServicetax.AddCell(panno1);
                                    }

                                    else if (ServicetaxNo.Trim().Length > 0 && servicetaxstatus == 0)
                                    {
                                        servicetaxstatus = 1;

                                        PdfPCell Staxno = new PdfPCell(new Phrase("Service Tax No:", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        Staxno.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        Staxno.Border = 0;
                                        tableServicetax.AddCell(Staxno);

                                        PdfPCell Staxno1 = new PdfPCell(new Phrase(ServicetaxNo, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        Staxno1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        Staxno1.Border = 0;
                                        tableServicetax.AddCell(Staxno1);
                                    }

                                    else
                                    {
                                        PdfPCell space = new PdfPCell(new Phrase(" ", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                        space.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                        space.Border = 0;
                                        space.Colspan = 2;
                                        tableServicetax.AddCell(space);
                                    }


                                    PdfPCell celldMt1 = new PdfPCell(new Phrase(discounttwotitle, FontFactory.GetFont(FontStyle, fontsize, Font.BOLD, BaseColor.BLACK)));
                                    celldMt1.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                                    celldMt1.Border = 0;
                                    tableServicetax.AddCell(celldMt1);

                                    PdfPCell celldMt3 = new PdfPCell(new Phrase(discounttwo.ToString("#,##0.00"), FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                    celldMt3.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                                    celldMt3.Border = 0;
                                    tableServicetax.AddCell(celldMt3);
                                }
                                #endregion

                            }




                            #endregion

                            #region New Code for only we have pfno,esno,panno and servicetax no

                            if (PFNo.Trim().Length > 0 && pfstatus == 0)
                            {
                                pfstatus = 1;

                                PdfPCell Pfno = new PdfPCell(new Phrase("EPF NO:", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                Pfno.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                //Pfno.Colspan = 2;
                                Pfno.Border = 0;
                                tableServicetax.AddCell(Pfno);


                                PdfPCell Pfno1 = new PdfPCell(new Phrase(PFNo, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                Pfno1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                Pfno1.Border = 0;
                                tableServicetax.AddCell(Pfno1);


                                PdfPCell space = new PdfPCell(new Phrase(" ", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                space.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                space.Border = 0;
                                space.Colspan = 2;
                                tableServicetax.AddCell(space);

                            }

                            if (Esino.Trim().Length > 0 && esistatus == 0)
                            {
                                esistatus = 1;

                                PdfPCell ESino = new PdfPCell(new Phrase("ESI NO:", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                ESino.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                ESino.Border = 0;
                                tableServicetax.AddCell(ESino);


                                PdfPCell ESino1 = new PdfPCell(new Phrase(Esino, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                ESino1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                ESino1.Border = 0;
                                tableServicetax.AddCell(ESino1);

                                PdfPCell space = new PdfPCell(new Phrase(" ", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                space.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                space.Border = 0;
                                space.Colspan = 2;
                                tableServicetax.AddCell(space);

                            }
                            if (PANNO.Trim().Length > 0 && panstatus == 0)
                            {
                                panstatus = 1;

                                PdfPCell panno = new PdfPCell(new Phrase("PAN NO:", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                panno.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                panno.Border = 0;
                                tableServicetax.AddCell(panno);

                                PdfPCell panno1 = new PdfPCell(new Phrase(PANNO, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                panno1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                panno1.Border = 0;
                                tableServicetax.AddCell(panno1);

                                PdfPCell space = new PdfPCell(new Phrase(" ", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                space.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                space.Border = 0;
                                space.Colspan = 2;
                                tableServicetax.AddCell(space);

                            }

                            if (ServicetaxNo.Trim().Length > 0 && servicetaxstatus == 0)
                            {
                                servicetaxstatus = 1;

                                PdfPCell Staxno = new PdfPCell(new Phrase("Service Tax No:", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                Staxno.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                Staxno.Border = 0;
                                tableServicetax.AddCell(Staxno);

                                PdfPCell Staxno1 = new PdfPCell(new Phrase(ServicetaxNo, FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                Staxno1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                Staxno1.Border = 0;
                                tableServicetax.AddCell(Staxno1);


                                PdfPCell space = new PdfPCell(new Phrase(" ", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                                space.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                space.Border = 0;
                                space.Colspan = 2;
                                tableServicetax.AddCell(space);

                            }

                            #endregion


                            document.Add(tableServicetax);

                            PdfPTable tableFooter = new PdfPTable(3);
                            tableFooter.TotalWidth = 520;//432f;
                            tableFooter.LockedWidth = true;
                            float[] widthdfooter = new float[] { 2f, 2f, 2f };
                            tableFooter.SetWidths(widthdfooter);

                            PdfPCell cellcat = new PdfPCell(new Phrase("Category of Service - Security Services", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                            cellcat.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            cellcat.Colspan = 3;
                            cellcat.Border = 0;
                            tableFooter.AddCell(cellcat);

                            PdfPCell cellGspace = new PdfPCell(new Phrase(" ", FontFactory.GetFont(FontStyle, fontsize, Font.BOLD, BaseColor.BLACK)));
                            cellGspace.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                            cellGspace.Border = 7;
                            tableFooter.AddCell(cellGspace);

                            PdfPCell celldg6 = new PdfPCell(new Phrase("Grand Total", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                            celldg6.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                            celldg6.Border = 3;
                            celldg6.MinimumHeight = 18;
                            tableFooter.AddCell(celldg6);

                            PdfPCell celldg8 = new PdfPCell(new Phrase(Grandtotal.ToString("#,##0.00"), FontFactory.GetFont(FontStyle, fontsize, Font.BOLD, BaseColor.BLACK)));
                            celldg8.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                            celldg8.Border = 11;
                            celldg8.MinimumHeight = 18;
                            tableFooter.AddCell(celldg8);


                            PdfPCell cellBreak = new PdfPCell(new Phrase(" ", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                            cellBreak.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                            cellBreak.Colspan = 3;
                            cellBreak.Border = 0;
                            tableFooter.AddCell(cellBreak);

                            PdfPCell cellcamt = new PdfPCell(new Phrase("Amount In Words: ", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                            cellcamt.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            cellcamt.Border = 7;
                            cellcamt.MinimumHeight = 18;
                            tableFooter.AddCell(cellcamt);

                            //lblamtinwords.Text.Trim()
                            PdfPCell cellcamt1 = new PdfPCell(new Phrase(NumberToEnglish.Instance.changeNumericToWords(Grandtotal.ToString()) + "Only", FontFactory.GetFont(FontStyle, fontsize, Font.BOLD, BaseColor.BLACK)));
                            cellcamt1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            cellcamt1.Colspan = 2;
                            cellcamt1.Border = 11;
                            cellcamt1.MinimumHeight = 18;
                            cellcamt1.PaddingLeft = -80;
                            tableFooter.AddCell(cellcamt1);

                            tableFooter.AddCell(cellBreak);
                            //tableFooter.AddCell(cellBreak);

                            PdfPCell cellPayment = new PdfPCell(new Phrase("Payment ::", FontFactory.GetFont(FontStyle, fontsize, Font.NORMAL, BaseColor.BLACK)));
                            cellPayment.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            cellPayment.Border = 0;
                            tableFooter.AddCell(cellPayment);

                            PdfPCell cellPayment1 = new PdfPCell(new Phrase("IMMEDIATE", FontFactory.GetFont(FontStyle, fontsize, Font.BOLD, BaseColor.BLACK)));
                            cellPayment1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            cellPayment1.Border = 0;
                            cellPayment1.Colspan = 2;
                            cellPayment1.PaddingLeft = -120;
                            tableFooter.AddCell(cellPayment1);

                            PdfPCell cellPayment2 = new PdfPCell(new Phrase("Payment through bank DD or Cheque favouring 'Knight Watch Security Ltd'\nIn cace of overdue, interest or expense" +
                                " @ 2% per month will be charged\nAll disputes subject to New Delhi jurisdiction only", FontFactory.GetFont(FontStyle, fontsize - 2, Font.NORMAL, BaseColor.BLACK)));
                            cellPayment2.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            cellPayment2.Border = 0;
                            cellPayment2.Colspan = 3;
                            tableFooter.AddCell(cellPayment2);

                            PdfPCell cellc3 = new PdfPCell(new Phrase(companyName, FontFactory.GetFont(FontStyle, fontsize, Font.BOLD, BaseColor.BLACK)));
                            cellc3.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                            cellc3.Colspan = 3;
                            cellc3.Border = 0;
                            tableFooter.AddCell(cellc3);

                            tableFooter.AddCell(cellBreak);
                            tableFooter.AddCell(cellBreak);
                            ////tableFooter.AddCell(cellBreak);
                            ////tableFooter.AddCell(cellBreak);
                            //tableFooter.AddCell(cellBreak);
                            //tableFooter.AddCell(cellBreak);
                            //tableFooter.AddCell(cellBreak);


                            PdfPCell cellPreparedby = new PdfPCell(new Phrase("Prepared By ", FontFactory.GetFont(FontStyle, fontsize, Font.BOLD, BaseColor.BLACK)));
                            cellPreparedby.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            cellPreparedby.Border = 0;
                            tableFooter.AddCell(cellPreparedby);

                            PdfPCell cellc5 = new PdfPCell(new Phrase("Checked By ", FontFactory.GetFont(FontStyle, fontsize, Font.BOLD, BaseColor.BLACK)));
                            cellc5.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                            cellc5.Border = 0;
                            tableFooter.AddCell(cellc5);

                            PdfPCell cellc4 = new PdfPCell(new Phrase("Authorised Signatory", FontFactory.GetFont(FontStyle, fontsize, Font.BOLD, BaseColor.BLACK)));
                            cellc4.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                            cellc4.Border = 0;
                            tableFooter.AddCell(cellc4);

                            document.Add(tableFooter);
                            document.NewPage();
                        }
                        document.Close();
                    }

                }
                catch (Exception ex)
                {

                }

                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment;filename=Invoice.pdf");
                Response.Buffer = true;
                Response.Clear();
                Response.OutputStream.Write(ms.GetBuffer(), 0, ms.GetBuffer().Length);
                Response.OutputStream.Flush();
                Response.End();
            }
            else
            {

                return;
            }
        }

        public string GetMonthOfYear()
        {
            string MonthYear = "";

            var Month = "";

            string date = DateTime.Parse(txtmonth.Text.Trim(), CultureInfo.GetCultureInfo("en-gb")).ToString();
            string month = DateTime.Parse(date).Month.ToString();
            string Year = DateTime.Parse(date).Year.ToString();
            Month = month + Year.Substring(2, 2);

            if (Month.ToString().Length == 4)
            {

                MonthYear = "20" + Month.ToString().Substring(2, 2);

            }
            if (Month.ToString().Length == 3)
            {

                MonthYear = "20" + Month.ToString().Substring(1, 2);

            }
            return MonthYear;
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
    }
}