using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KLTS.Data;
using System.Data;
using System.Globalization;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Script.Serialization;
using System.Web.Security;
using iTextSharp.text.html.simpleparser;
using System.Collections;
using Jawan.Portal.DAL;
namespace Jawan.Portal
{
    public partial class ClientForms : System.Web.UI.Page
    {
        AppConfiguration config = new AppConfiguration();
        GridViewExportUtil gve = new GridViewExportUtil();
        DataTable dt;
        string EmpIDPrefix = "";
        string CmpIDPrefix = "";
        string Elength = "";
        string Clength = "";
        string BranchID = "";
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
            BranchID = Session["BranchID"].ToString();
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

        }

        protected void LoadClientList()
        {
            DataTable DtClientNames = GlobalData.Instance.LoadCIds(CmpIDPrefix,BranchID);
            if (DtClientNames.Rows.Count > 0)
            {
                ddlclientid.DataValueField = "Clientid";
                ddlclientid.DataTextField = "Clientid";
                ddlclientid.DataSource = DtClientNames;
                ddlclientid.DataBind();
            }
            ddlclientid.Items.Insert(0, "-Select-");
        }

        protected void ddlcname_OnSelectedIndexChanged(object sender, EventArgs e)
        {

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

        protected void ddlForms_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlForms.SelectedIndex == 0)
            {
                lblclientid.Visible = false;
                lblclientname.Visible = false;
                lblDOJ.Visible = false;
                lblmonth.Visible = false;
                txtEmpDtofJoining.Visible = false;
                txtmonth.Visible = false;
                ddlclientid.Visible = false;
                ddlcname.Visible = false;
                lblfrom.Visible = false;
                lblto.Visible = false;
                txtfrom.Visible = false;
                txtto.Visible = false;

            }

            if (ddlForms.SelectedIndex == 1)
            {
                lblclientid.Visible = true;
                lblclientname.Visible = true;
                lblDOJ.Visible = true;
                lblmonth.Visible = true;
                txtEmpDtofJoining.Visible = true;
                txtmonth.Visible = true;
                ddlclientid.Visible = true;
                ddlcname.Visible = true;
                lblfrom.Visible = false;
                lblto.Visible = false;
                txtfrom.Visible = false;
                txtto.Visible = false;
            }

            if (ddlForms.SelectedIndex == 2)
            {
                lblclientid.Visible = true;
                lblclientname.Visible = true;
                lblDOJ.Visible = false;
                lblmonth.Visible = false;
                txtEmpDtofJoining.Visible = false;
                txtmonth.Visible = false;
                ddlclientid.Visible = true;
                ddlcname.Visible = true;
                lblfrom.Visible = true;
                lblto.Visible = true;
                txtfrom.Visible = true;
                txtto.Visible = true;
            }

        }



        protected void btnsearch_Click(object sender, EventArgs e)
        {
            if (ddlForms.SelectedIndex == 1)
            {
                BtnformXIII_Click(sender, e);
                return;
            }
            if (ddlForms.SelectedIndex == 2)
            {
                btnform12_Click(sender, e);
                return;
            }

        }



        protected void BtnformXIII_Click(object sender, EventArgs e)
        {


            int Fontsize = 11;
            string fontsyle = "verdana";

            #region Variable Declaration

            string Companyname = "";
            string Companyaddress = "";
            string Clientname = "";
            string ClientAddress = "";
            string Location = "";
            string Idno = "";
            string date = "";
            string designation = "";
            string name = "";
            string fathername = "";
            string dateofbirth = "";
            string maritalstatus = "";
            string Gender = "";
            string CellValue = "";
            string DtofLeaving = "";
            string prdoorno = "";
            string prstreet = "";
            string prarea = "";
            string prcity = "";
            string prLmark = "";
            string prDistrict = "";
            string prPincode = "";
            string prState = "";
            string prTaluka = "";
            string prTown = "";

            string peTaluka = "";
            string peTown = "";
            string pedoor = "";
            string pestreet = "";
            string pearea = "";
            string pecity = "";
            string pelmark = "";
            string peDistrict = "";
            string pePincode = "";
            string peState = "";


            string clientname = "";





            #endregion


            if (ddlclientid.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Show alert", "alert('Please select Client');", true);
                return;
            }

            #region  Begin  New Code






            #region Begin Variable Declaration

            string SPName = "";
            Hashtable HTEmpBiodata = new Hashtable();
            string Clientid = "";

            DataTable dtEmpdetails = null;

            #endregion end Variable Declaration


            #region Begin Assign Values to The Variables
            SPName = "ClientFormsPDF";
            Clientid = ddlclientid.SelectedValue;

            var DtofJoining = string.Empty;
            if (txtEmpDtofJoining.Text.Trim().Length != 0)
            {
                DtofJoining = Timings.Instance.CheckDateFormat(txtEmpDtofJoining.Text);

            }
            else
            {
                DtofJoining = "01/01/1900";
            }


            if (txtmonth.Text.Trim().Length > 0)
            {
                date = DateTime.Parse(txtmonth.Text.Trim(), CultureInfo.GetCultureInfo("en-gb")).ToString();
            }


            string month = DateTime.Parse(date).Month.ToString();
            string Year = DateTime.Parse(date).Year.ToString();

            #endregion End Assign values To the Variables
            int forms = 0;
            forms = ddlForms.SelectedIndex;
            #region Begin Pass values to the Hash table
            HTEmpBiodata.Add("@Clientid", Clientid);
            HTEmpBiodata.Add("@month", month + Year.Substring(2, 2));
            HTEmpBiodata.Add("@forms", forms);
            HTEmpBiodata.Add("@DtofJoining", DtofJoining);
            #endregion  end Pass values to the Hash table

            #region Begin  Call Stored Procedure
            dtEmpdetails = config.ExecuteAdaptorAsyncWithParams(SPName, HTEmpBiodata).Result;
            #endregion  End  Call Stored Procedure

            #endregion End New Code As on [31-05-2014]




            if (dtEmpdetails.Rows.Count > 0)
            {

                #region Assining data to Variables

                #endregion
                MemoryStream ms = new MemoryStream();
                Document document = new Document(PageSize.LEGAL.Rotate());
                // var output = new FileStream(fileheader2, FileMode., FileAccess.Write, FileShare.None);
                var writer = PdfWriter.GetInstance(document, ms);
                document.Open();
                BaseFont bf = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                string imagepath1 = Server.MapPath("images");
                #region

                PdfPTable MainTable = new PdfPTable(6);
                MainTable.TotalWidth = 950f;
                MainTable.LockedWidth = true;
                float[] widths = new float[] { 4f, 5f, 2f, 4f, 5f, 2f, };
                MainTable.SetWidths(widths);



                Companyname = dtEmpdetails.Rows[0]["CompanyName"].ToString();
                Companyaddress = dtEmpdetails.Rows[0]["CompanyAddress"].ToString();
                Clientname = dtEmpdetails.Rows[0]["ClientName"].ToString();
                ClientAddress = dtEmpdetails.Rows[0]["ClientAddress"].ToString();
                Location = dtEmpdetails.Rows[0]["Location"].ToString();



                PdfPCell cellspace = new PdfPCell(new Phrase("  ", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize - 2, Font.BOLD, BaseColor.BLACK)));
                cellspace.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                cellspace.Colspan = 6;
                cellspace.Border = 0;
                cellspace.PaddingTop = -5;


                PdfPCell cellHead = new PdfPCell(new Phrase("Form-XIII ", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize + 4, Font.BOLD, BaseColor.BLACK)));
                cellHead.HorizontalAlignment = 1;
                cellHead.Colspan = 6;
                cellHead.Border = 0;
                MainTable.AddCell(cellHead);

                PdfPCell cellreturn12a = new PdfPCell(new Phrase("REGISTER OF WORKMEN EMPLOYED BY CONTRACTOR", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, Font.BOLD, BaseColor.BLACK)));
                cellreturn12a.HorizontalAlignment = 1;
                cellreturn12a.Colspan = 6;
                cellreturn12a.Border = 0;
                MainTable.AddCell(cellreturn12a);

                PdfPCell cellRule75 = new PdfPCell(new Phrase(" [Rule 75]", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize + 3, Font.BOLD, BaseColor.BLACK)));
                cellRule75.HorizontalAlignment = 1;
                cellRule75.Colspan = 6;
                cellRule75.Border = 0;
                MainTable.AddCell(cellRule75);

                MainTable.AddCell(cellspace);
                MainTable.AddCell(cellspace);

                PdfPCell cellNamess40 = new PdfPCell(new Phrase("1. Name and address of contractor ", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
                cellNamess40.HorizontalAlignment = 0;
                cellNamess40.Border = 0;
                cellNamess40.PaddingLeft = 0;
                MainTable.AddCell(cellNamess40);

                PdfPCell cellNameaddress = new PdfPCell(new Phrase(Companyname + "\n" + Companyaddress, FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.BOLD, BaseColor.BLACK)));
                cellNameaddress.HorizontalAlignment = 0;
                cellNameaddress.Border = 0;
                cellNameaddress.PaddingLeft = 0;
                MainTable.AddCell(cellNameaddress);

                PdfPCell cellNamess43 = new PdfPCell(new Phrase(" ", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
                cellNamess43.HorizontalAlignment = 0;
                cellNamess43.Border = 0;
                cellNamess43.PaddingLeft = 0;
                MainTable.AddCell(cellNamess43);

                PdfPCell cellNamess41 = new PdfPCell(new Phrase("2. Name and address of establishment in/under which contract is carried on", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
                cellNamess41.HorizontalAlignment = 0;
                cellNamess41.Border = 0;
                cellNamess41.PaddingLeft = 0;
                MainTable.AddCell(cellNamess41);

                PdfPCell cellClientadd = new PdfPCell(new Phrase(Clientname + "\n" + ClientAddress, FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.BOLD, BaseColor.BLACK)));
                cellClientadd.HorizontalAlignment = 0;
                cellClientadd.Border = 0;
                cellClientadd.PaddingLeft = 0;
                MainTable.AddCell(cellClientadd);

                MainTable.AddCell(cellspace);

                PdfPCell cellNamess42 = new PdfPCell(new Phrase("3. Nature and location of work", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
                cellNamess42.HorizontalAlignment = 0;
                cellNamess42.Border = 0;
                cellNamess42.PaddingLeft = 0;
                MainTable.AddCell(cellNamess42);


                PdfPCell celllocation = new PdfPCell(new Phrase(Location, FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.BOLD, BaseColor.BLACK)));
                celllocation.HorizontalAlignment = 0;
                celllocation.Border = 0;
                celllocation.PaddingLeft = 0;
                MainTable.AddCell(celllocation);

                PdfPCell cellNamess44 = new PdfPCell(new Phrase(" ", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
                cellNamess44.HorizontalAlignment = 0;
                cellNamess44.Border = 0;
                cellNamess44.PaddingLeft = 0;
                MainTable.AddCell(cellNamess44);

                PdfPCell cellNamess45 = new PdfPCell(new Phrase("4. Name and address of principal employer", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
                cellNamess45.HorizontalAlignment = 0;
                cellNamess45.Border = 0;
                cellNamess45.PaddingLeft = 0;
                MainTable.AddCell(cellNamess45);


                PdfPCell cellPrincipalempl = new PdfPCell(new Phrase(ClientAddress, FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.BOLD, BaseColor.BLACK)));
                cellPrincipalempl.HorizontalAlignment = 0;
                cellPrincipalempl.Border = 0;
                cellPrincipalempl.PaddingLeft = 0;
                MainTable.AddCell(cellPrincipalempl);

                MainTable.AddCell(cellspace);
                MainTable.AddCell(cellspace);
                MainTable.AddCell(cellspace);

                document.Add(MainTable);

                PdfPTable tablenew = new PdfPTable(12);
                tablenew.TotalWidth = 950f;
                tablenew.HeaderRows = 2;
                tablenew.LockedWidth = true;
                float[] width = new float[] { 1f, 3f, 2.5f, 3f, 2f, 3f, 3f, 2.5f, 2f, 2f, 2f, 1.5f };
                tablenew.SetWidths(width);



                PdfPCell cellNamess46 = new PdfPCell(new Phrase("Sl.No. ", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.BOLD, BaseColor.BLACK)));
                cellNamess46.HorizontalAlignment = 1;
                cellNamess46.Colspan = 0;
                cellNamess46.Border = 0;
                cellNamess46.PaddingLeft = 0;
                cellNamess46.PaddingTop = 20;
                cellNamess46.PaddingBottom = 20;
                cellNamess46.BorderWidthLeft = 0.5f;
                cellNamess46.BorderWidthRight = 0.9f;
                cellNamess46.BorderWidthTop = 0.5f;
                cellNamess46.BorderWidthBottom = 0.5f;
                tablenew.AddCell(cellNamess46);

                PdfPCell cellNamess47 = new PdfPCell(new Phrase("Name and surname of workman", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.BOLD, BaseColor.BLACK)));
                cellNamess47.HorizontalAlignment = 1;
                cellNamess47.Colspan = 0;
                cellNamess47.Border = 0;
                cellNamess47.PaddingLeft = 0;
                cellNamess47.PaddingLeft = 0;
                cellNamess47.PaddingTop = 20;
                cellNamess47.PaddingBottom = 20;
                cellNamess47.BorderWidthLeft = 0;
                cellNamess47.BorderWidthRight = 0.9f;
                cellNamess47.BorderWidthTop = 0.5f;
                cellNamess47.BorderWidthBottom = 0.5f;
                tablenew.AddCell(cellNamess47);

                PdfPCell cellNamess48 = new PdfPCell(new Phrase("Age and Sex ", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.BOLD, BaseColor.BLACK)));
                cellNamess48.HorizontalAlignment = 1;
                cellNamess48.Colspan = 0;
                cellNamess48.Border = 0;
                cellNamess48.PaddingLeft = 0;
                cellNamess48.PaddingLeft = 0;
                cellNamess48.PaddingLeft = 0;
                cellNamess48.PaddingTop = 20;
                cellNamess48.PaddingBottom = 20;
                cellNamess48.BorderWidthLeft = 0;
                cellNamess48.BorderWidthRight = 0.9f;
                cellNamess48.BorderWidthTop = 0.5f;
                cellNamess48.BorderWidthBottom = 0.5f;
                tablenew.AddCell(cellNamess48);

                PdfPCell cellNamess49 = new PdfPCell(new Phrase("Father's/husband's name", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.BOLD, BaseColor.BLACK)));
                cellNamess49.HorizontalAlignment = 1;
                cellNamess49.Colspan = 0;
                cellNamess49.Border = 0;
                cellNamess49.PaddingLeft = 0;
                cellNamess49.PaddingLeft = 0;
                cellNamess49.PaddingLeft = 0;
                cellNamess49.PaddingTop = 20;
                cellNamess49.PaddingBottom = 20;
                cellNamess49.BorderWidthLeft = 0;
                cellNamess49.BorderWidthRight = 0.9f;
                cellNamess49.BorderWidthTop = 0.5f;
                cellNamess49.BorderWidthBottom = 0.5f;
                tablenew.AddCell(cellNamess49);

                PdfPCell cellNamess50 = new PdfPCell(new Phrase("Nature of employment/ Designation ", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.BOLD, BaseColor.BLACK)));
                cellNamess50.HorizontalAlignment = 1;
                cellNamess50.Colspan = 0;
                cellNamess50.Border = 0;
                cellNamess50.PaddingLeft = 0;
                cellNamess50.PaddingLeft = 0;
                cellNamess50.PaddingLeft = 0;
                cellNamess50.PaddingTop = 20;
                cellNamess50.PaddingBottom = 20;
                cellNamess50.BorderWidthLeft = 0;
                cellNamess50.BorderWidthRight = 0.9f;
                cellNamess50.BorderWidthTop = 0.5f;
                cellNamess50.BorderWidthBottom = 0.5f;
                tablenew.AddCell(cellNamess50);

                PdfPCell cellNamess51 = new PdfPCell(new Phrase("Permanent Home Address of workman (Village and Tahsil/Taluk and District", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.BOLD, BaseColor.BLACK)));
                cellNamess51.HorizontalAlignment = 1;
                cellNamess51.Colspan = 0;
                cellNamess51.Border = 0;
                cellNamess51.PaddingLeft = 0;
                cellNamess51.PaddingLeft = 0;
                cellNamess51.PaddingLeft = 0;
                cellNamess51.PaddingTop = 20;
                cellNamess51.PaddingBottom = 20;
                cellNamess51.BorderWidthLeft = 0;
                cellNamess51.BorderWidthRight = 0.9f;
                cellNamess51.BorderWidthTop = 0.5f;
                cellNamess51.BorderWidthBottom = 0.5f;
                tablenew.AddCell(cellNamess51);

                PdfPCell cellNamess52 = new PdfPCell(new Phrase("Local Address", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.BOLD, BaseColor.BLACK)));
                cellNamess52.HorizontalAlignment = 1;
                cellNamess52.Colspan = 0;
                cellNamess52.Border = 0;
                cellNamess52.PaddingLeft = 0;
                cellNamess52.PaddingLeft = 0;
                cellNamess52.PaddingLeft = 0;
                cellNamess52.PaddingTop = 20;
                cellNamess52.PaddingBottom = 20;
                cellNamess52.BorderWidthLeft = 0;
                cellNamess52.BorderWidthRight = 0.9f;
                cellNamess52.BorderWidthTop = 0.5f;
                cellNamess52.BorderWidthBottom = 0.5f;
                tablenew.AddCell(cellNamess52);

                PdfPCell cellNamess53 = new PdfPCell(new Phrase("Date of commencement of employment", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.BOLD, BaseColor.BLACK)));
                cellNamess53.HorizontalAlignment = 1;
                cellNamess53.Colspan = 0;
                cellNamess53.Border = 0;
                cellNamess53.PaddingLeft = 0;
                cellNamess53.PaddingLeft = 0;
                cellNamess53.PaddingLeft = 0;
                cellNamess53.PaddingTop = 20;
                cellNamess53.PaddingBottom = 20;
                cellNamess53.BorderWidthLeft = 0;
                cellNamess53.BorderWidthRight = 0.9f;
                cellNamess53.BorderWidthTop = 0.5f;
                cellNamess53.BorderWidthBottom = 0.5f;
                tablenew.AddCell(cellNamess53);

                PdfPCell cellNamess54 = new PdfPCell(new Phrase("Signature or thumb impression of workman", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.BOLD, BaseColor.BLACK)));
                cellNamess54.HorizontalAlignment = 1;
                cellNamess54.Colspan = 0;
                cellNamess54.Border = 0;
                cellNamess54.PaddingLeft = 0;
                cellNamess54.PaddingLeft = 0;
                cellNamess54.PaddingLeft = 0;
                cellNamess54.PaddingTop = 20;
                cellNamess54.PaddingBottom = 20;
                cellNamess54.BorderWidthLeft = 0;
                cellNamess54.BorderWidthRight = 0.9f;
                cellNamess54.BorderWidthTop = 0.5f;
                cellNamess54.BorderWidthBottom = 0.5f;
                tablenew.AddCell(cellNamess54);

                PdfPCell cellNamess55 = new PdfPCell(new Phrase("Date of termination of employment", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.BOLD, BaseColor.BLACK)));
                cellNamess55.HorizontalAlignment = 1;
                cellNamess55.Colspan = 0;
                cellNamess55.Border = 0;
                cellNamess55.PaddingLeft = 0;
                cellNamess55.PaddingTop = 20;
                cellNamess55.PaddingBottom = 20;
                cellNamess55.BorderWidthLeft = 0;
                cellNamess55.BorderWidthRight = 0.9f;
                cellNamess55.BorderWidthTop = 0.5f;
                cellNamess55.BorderWidthBottom = 0.5f;
                tablenew.AddCell(cellNamess55);

                PdfPCell cellNamess56 = new PdfPCell(new Phrase("Reasons for termination", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.BOLD, BaseColor.BLACK)));
                cellNamess56.HorizontalAlignment = 1;
                cellNamess56.Colspan = 0;
                cellNamess56.Border = 0;
                cellNamess56.PaddingLeft = 0;
                cellNamess56.PaddingTop = 20;
                cellNamess56.PaddingBottom = 20;
                cellNamess56.BorderWidthLeft = 0;
                cellNamess56.BorderWidthRight = 0.9f;
                cellNamess56.BorderWidthTop = 0.5f;
                cellNamess56.BorderWidthBottom = 0.5f;
                tablenew.AddCell(cellNamess56);

                PdfPCell cellNamess57 = new PdfPCell(new Phrase("Remarks", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.BOLD, BaseColor.BLACK)));
                cellNamess57.HorizontalAlignment = 1;
                cellNamess57.Colspan = 0;
                cellNamess57.Border = 0;
                cellNamess57.PaddingLeft = 0;
                cellNamess57.PaddingLeft = 0;
                cellNamess57.PaddingLeft = 0;
                cellNamess57.PaddingTop = 20;
                cellNamess57.PaddingBottom = 20;
                cellNamess57.BorderWidthLeft = 0;
                cellNamess57.BorderWidthRight = 0.9f;
                cellNamess57.BorderWidthTop = 0.5f;
                cellNamess57.BorderWidthBottom = 0.5f;
                tablenew.AddCell(cellNamess57);

                PdfPCell cellssNames47 = new PdfPCell(new Phrase("  1 ", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.BOLD, BaseColor.BLACK)));
                cellssNames47.HorizontalAlignment = 1;
                cellssNames47.Colspan = 0;
                cellssNames47.Border = 0;
                cellssNames47.PaddingLeft = 0;
                cellssNames47.PaddingLeft = 0;
                cellssNames47.PaddingTop = 10;
                cellssNames47.PaddingBottom = 10;
                cellssNames47.BorderWidthLeft = 0.9f;
                cellssNames47.BorderWidthRight = 0.9f;
                cellssNames47.BorderWidthTop = 0;
                cellssNames47.BorderWidthBottom = 0.5f;
                tablenew.AddCell(cellssNames47);

                PdfPCell cellsNamesss47 = new PdfPCell(new Phrase("  2", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.BOLD, BaseColor.BLACK)));
                cellsNamesss47.HorizontalAlignment = 1;
                cellsNamesss47.Colspan = 0;
                cellsNamesss47.Border = 0;
                cellsNamesss47.PaddingLeft = 0;
                cellsNamesss47.PaddingTop = 10;
                cellsNamesss47.PaddingBottom = 10;
                cellsNamesss47.BorderWidthLeft = 0;
                cellsNamesss47.BorderWidthRight = 0.9f;
                cellsNamesss47.BorderWidthTop = 0;
                cellsNamesss47.BorderWidthBottom = 0.5f;
                tablenew.AddCell(cellsNamesss47);

                PdfPCell cellsNamess48 = new PdfPCell(new Phrase("  3 ", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.BOLD, BaseColor.BLACK)));
                cellsNamess48.HorizontalAlignment = 1;
                cellsNamess48.Colspan = 0;
                cellsNamess48.Border = 0;
                cellsNamess48.PaddingLeft = 0;
                cellsNamess48.PaddingTop = 10;
                cellsNamess48.PaddingBottom = 10;
                cellsNamess48.BorderWidthLeft = 0;
                cellsNamess48.BorderWidthRight = 0.9f;
                cellsNamess48.BorderWidthTop = 0;
                cellsNamess48.BorderWidthBottom = 0.5f;
                tablenew.AddCell(cellsNamess48);

                PdfPCell cellsNamess49 = new PdfPCell(new Phrase("  4", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.BOLD, BaseColor.BLACK)));
                cellsNamess49.HorizontalAlignment = 1;
                cellsNamess49.Colspan = 0;
                cellsNamess49.Border = 0;
                cellsNamess49.PaddingLeft = 0;
                cellsNamess49.PaddingTop = 10;
                cellsNamess49.PaddingBottom = 10;
                cellsNamess49.BorderWidthLeft = 0;
                cellsNamess49.BorderWidthRight = 0.9f;
                cellsNamess49.BorderWidthTop = 0;
                cellsNamess49.BorderWidthBottom = 0.5f;
                tablenew.AddCell(cellsNamess49);

                PdfPCell cellsNamess50 = new PdfPCell(new Phrase("  5 ", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.BOLD, BaseColor.BLACK)));
                cellsNamess50.HorizontalAlignment = 1;
                cellsNamess50.Colspan = 0;
                cellsNamess50.Border = 0;
                cellsNamess50.PaddingLeft = 0;
                cellsNamess50.PaddingTop = 10;
                cellsNamess50.PaddingBottom = 10;
                cellsNamess50.BorderWidthLeft = 0;
                cellsNamess50.BorderWidthRight = 0.9f;
                cellsNamess50.BorderWidthTop = 0;
                cellsNamess50.BorderWidthBottom = 0.5f;
                tablenew.AddCell(cellsNamess50);

                PdfPCell cellsNamess51 = new PdfPCell(new Phrase("  6", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.BOLD, BaseColor.BLACK)));
                cellsNamess51.HorizontalAlignment = 1;
                cellsNamess51.Colspan = 0;
                cellsNamess51.Border = 0;
                cellsNamess51.PaddingLeft = 0;
                cellsNamess51.PaddingTop = 10;
                cellsNamess51.PaddingBottom = 10;
                cellsNamess51.BorderWidthLeft = 0;
                cellsNamess51.BorderWidthRight = 0.9f;
                cellsNamess51.BorderWidthTop = 0;
                cellsNamess51.BorderWidthBottom = 0.5f;
                tablenew.AddCell(cellsNamess51);

                PdfPCell cellsNamess52 = new PdfPCell(new Phrase("  7 ", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.BOLD, BaseColor.BLACK)));
                cellsNamess52.HorizontalAlignment = 1;
                cellsNamess52.Colspan = 0;
                cellsNamess52.Border = 0;
                cellsNamess52.PaddingLeft = 0;
                cellsNamess52.PaddingTop = 10;
                cellsNamess52.PaddingBottom = 10;
                cellsNamess52.BorderWidthLeft = 0;
                cellsNamess52.BorderWidthRight = 0.9f;
                cellsNamess52.BorderWidthTop = 0;
                cellsNamess52.BorderWidthBottom = 0.5f;
                tablenew.AddCell(cellsNamess52);

                PdfPCell cellsNamess53 = new PdfPCell(new Phrase("  8", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.BOLD, BaseColor.BLACK)));
                cellsNamess53.HorizontalAlignment = 1;
                cellsNamess53.Colspan = 0;
                cellsNamess53.PaddingLeft = 0;
                cellsNamess53.PaddingTop = 10;
                cellsNamess53.BorderWidthLeft = 0;
                cellsNamess53.BorderWidthRight = 0.9f;
                cellsNamess53.BorderWidthTop = 0;
                cellsNamess53.BorderWidthBottom = 0.5f;
                tablenew.AddCell(cellsNamess53);

                PdfPCell cellsNamess54 = new PdfPCell(new Phrase("  9", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.BOLD, BaseColor.BLACK)));
                cellsNamess54.HorizontalAlignment = 1;
                cellsNamess54.Colspan = 0;
                cellsNamess54.Border = 0;
                cellsNamess54.PaddingLeft = 0;
                cellsNamess54.PaddingTop = 10;
                cellsNamess54.PaddingBottom = 10;
                cellsNamess54.BorderWidthLeft = 0;
                cellsNamess54.BorderWidthRight = 0.9f;
                cellsNamess54.BorderWidthTop = 0;
                cellsNamess54.BorderWidthBottom = 0.5f;
                tablenew.AddCell(cellsNamess54);

                PdfPCell cellsNamess55 = new PdfPCell(new Phrase("  10", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.BOLD, BaseColor.BLACK)));
                cellsNamess55.HorizontalAlignment = 1;
                cellsNamess55.Colspan = 0;
                cellsNamess55.Border = 0;
                cellsNamess55.PaddingLeft = 0;
                cellsNamess55.PaddingTop = 10;
                cellsNamess55.PaddingBottom = 10;
                cellsNamess55.BorderWidthLeft = 0;
                cellsNamess55.BorderWidthRight = 0.9f;
                cellsNamess55.BorderWidthTop = 0;
                cellsNamess55.BorderWidthBottom = 0.5f;
                tablenew.AddCell(cellsNamess55);

                PdfPCell cellsNamess56 = new PdfPCell(new Phrase("  11", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.BOLD, BaseColor.BLACK)));
                cellsNamess56.HorizontalAlignment = 1;
                cellsNamess56.Colspan = 0;
                cellsNamess56.Border = 0;
                cellsNamess56.PaddingLeft = 0;
                cellsNamess56.PaddingTop = 10;
                cellsNamess56.PaddingBottom = 10;
                cellsNamess56.BorderWidthLeft = 0;
                cellsNamess56.BorderWidthRight = 0.9f;
                cellsNamess56.BorderWidthTop = 0;
                cellsNamess56.BorderWidthBottom = 0.5f;
                tablenew.AddCell(cellsNamess56);

                PdfPCell cellsNamess57 = new PdfPCell(new Phrase("  12", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.BOLD, BaseColor.BLACK)));
                cellsNamess57.HorizontalAlignment = 1;
                cellsNamess57.Colspan = 0;
                cellsNamess57.Border = 0;
                cellsNamess57.PaddingLeft = 0;
                cellsNamess57.PaddingTop = 10;
                cellsNamess57.PaddingBottom = 10;
                cellsNamess57.BorderWidthLeft = 0;
                cellsNamess57.BorderWidthRight = 0.9f;
                cellsNamess57.BorderWidthTop = 0;
                cellsNamess57.BorderWidthBottom = 0.5f;
                tablenew.AddCell(cellsNamess57);

                int j = 1;
                for (int i = 0; i < dtEmpdetails.Rows.Count; i++)
                {




                    Idno = dtEmpdetails.Rows[i]["Empid"].ToString();
                    designation = dtEmpdetails.Rows[i]["Designation"].ToString();
                    name = dtEmpdetails.Rows[i]["EmployeeName"].ToString();
                    fathername = dtEmpdetails.Rows[i]["EmpFatherName"].ToString();
                    dateofbirth = dtEmpdetails.Rows[i]["DtOfBirth"].ToString();
                    date = dtEmpdetails.Rows[i]["DtOfJoining"].ToString();
                    DtofLeaving = dtEmpdetails.Rows[i]["DtOfLeaving"].ToString(); ;
                    maritalstatus = dtEmpdetails.Rows[i]["MaritalStatus"].ToString();
                    Gender = dtEmpdetails.Rows[i]["Gender"].ToString();




                    prTaluka = dtEmpdetails.Rows[i]["prTaluka"].ToString();
                    prTown = dtEmpdetails.Rows[i]["prTown"].ToString();
                    prLmark = dtEmpdetails.Rows[i]["prLmark"].ToString();
                    prState = dtEmpdetails.Rows[i]["prState"].ToString();
                    prcity = dtEmpdetails.Rows[i]["prcity"].ToString();
                    prPincode = dtEmpdetails.Rows[i]["prPincode"].ToString();

                    pelmark = dtEmpdetails.Rows[i]["peLmark"].ToString();
                    peTaluka = dtEmpdetails.Rows[i]["peTaluka"].ToString();
                    peTown = dtEmpdetails.Rows[i]["peTown"].ToString();
                    pecity = dtEmpdetails.Rows[i]["pecity"].ToString();
                    pePincode = dtEmpdetails.Rows[i]["pePincode"].ToString();
                    peState = dtEmpdetails.Rows[i]["peState"].ToString();





                    PdfPCell cellEmpid = new PdfPCell(new Phrase(j.ToString(), FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
                    cellEmpid.HorizontalAlignment = 1;
                    cellEmpid.Colspan = 0;
                    cellEmpid.Border = 0;
                    cellEmpid.PaddingTop = 3;
                    cellEmpid.PaddingBottom = 3;
                    cellEmpid.BorderWidthLeft = 0.9f;
                    cellEmpid.BorderWidthRight = 0.9f;
                    cellEmpid.BorderWidthTop = 0;
                    cellEmpid.BorderWidthBottom = 0.5f;
                    tablenew.AddCell(cellEmpid);

                    PdfPCell cellName = new PdfPCell(new Phrase(name, FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
                    cellName.HorizontalAlignment = 0;
                    cellName.Colspan = 0;
                    cellName.Border = 0;
                    cellName.PaddingLeft = 0;
                    cellName.PaddingTop = 3;
                    cellName.PaddingBottom = 3;
                    cellName.BorderWidthLeft = 0;
                    cellName.BorderWidthRight = 0.9f;
                    cellName.BorderWidthTop = 0;
                    cellName.BorderWidthBottom = 0.5f;
                    tablenew.AddCell(cellName);


                    if (dateofbirth.Length > 0 && Gender.Length > 0)
                    {
                        CellValue = dateofbirth + " / " + Gender;
                    }
                    else if (dateofbirth.Length > 0 && Gender == "")
                    {
                        CellValue = dateofbirth;
                    }
                    else if (dateofbirth == "" && Gender.Length > 0)
                    {
                        CellValue = Gender;
                    }
                    else
                    {
                        CellValue = "";
                    }

                    PdfPCell cellDOBGender = new PdfPCell(new Phrase(CellValue, FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
                    cellDOBGender.HorizontalAlignment = 1;
                    cellDOBGender.Colspan = 0;
                    cellDOBGender.Border = 0;
                    cellDOBGender.PaddingLeft = 0;
                    cellDOBGender.PaddingTop = 3;
                    cellDOBGender.PaddingBottom = 3;
                    cellDOBGender.BorderWidthLeft = 0;
                    cellDOBGender.BorderWidthRight = 0.9f;
                    cellDOBGender.BorderWidthTop = 0;
                    cellDOBGender.BorderWidthBottom = 0.5f;
                    tablenew.AddCell(cellDOBGender);

                    PdfPCell cellFatherName = new PdfPCell(new Phrase(fathername, FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
                    cellFatherName.HorizontalAlignment = 0;
                    cellFatherName.Colspan = 0;
                    cellFatherName.Border = 0;
                    cellFatherName.PaddingLeft = 0;
                    cellFatherName.PaddingTop = 3;
                    cellFatherName.PaddingBottom = 3;
                    cellFatherName.BorderWidthLeft = 0;
                    cellFatherName.BorderWidthRight = 0.9f;
                    cellFatherName.BorderWidthTop = 0;
                    cellFatherName.BorderWidthBottom = 0.5f;
                    tablenew.AddCell(cellFatherName);

                    PdfPCell cellDesignation = new PdfPCell(new Phrase(designation, FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
                    cellDesignation.HorizontalAlignment = 1;
                    cellDesignation.Colspan = 0;
                    cellDesignation.Border = 0;
                    cellDesignation.PaddingLeft = 0;
                    cellDesignation.PaddingTop = 3;
                    cellDesignation.PaddingBottom = 3;
                    cellDesignation.BorderWidthLeft = 0;
                    cellDesignation.BorderWidthRight = 0.9f;
                    cellDesignation.BorderWidthTop = 0;
                    cellDesignation.BorderWidthBottom = 0.5f;
                    tablenew.AddCell(cellDesignation);

                    #region Permanent address String array

                    string[] PeAdress = new string[8];
                    if (peTaluka.Length > 0)
                    {
                        PeAdress[0] = peTaluka;
                    }
                    else
                    {
                        PeAdress[0] = "";
                    }
                    if (peTown.Length > 0)
                    {
                        PeAdress[1] = peTown;
                    }
                    else
                    {
                        PeAdress[1] = "";
                    }
                    if (pelmark.Length > 0)
                    {
                        PeAdress[2] = pelmark;
                    }
                    else
                    {
                        PeAdress[2] = "";
                    }
                    if (pearea.Length > 0)
                    {
                        PeAdress[3] = pearea;
                    }
                    else
                    {
                        PeAdress[3] = "";
                    }
                    if (pecity.Length > 0)
                    {
                        PeAdress[4] = pecity;
                    }
                    else
                    {
                        PeAdress[4] = "";
                    }
                    if (peDistrict.Length > 0)
                    {
                        PeAdress[5] = peDistrict;
                    }
                    else
                    {
                        PeAdress[5] = "";
                    }
                    if (pePincode.Length > 0)
                    {
                        PeAdress[6] = pePincode;
                    }
                    else
                    {
                        PeAdress[6] = "";
                    }
                    if (peState.Length > 0)
                    {
                        PeAdress[7] = peState;
                    }
                    else
                    {
                        PeAdress[7] = "";
                    }

                    string Address2 = string.Empty;

                    for (int k = 0; k < 8; k++)
                    {
                        Address2 += "  " + PeAdress[k];
                    }


                    #endregion

                    PdfPCell cellPermAddress = new PdfPCell(new Phrase(Address2.Trim(), FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
                    cellPermAddress.HorizontalAlignment = 0;
                    cellPermAddress.Colspan = 0;
                    cellPermAddress.Border = 0;
                    cellPermAddress.PaddingLeft = 0;
                    cellPermAddress.PaddingTop = 3;
                    cellPermAddress.PaddingBottom = 3;
                    cellPermAddress.BorderWidthLeft = 0;
                    cellPermAddress.BorderWidthRight = 0.9f;
                    cellPermAddress.BorderWidthTop = 0;
                    cellPermAddress.BorderWidthBottom = 0.5f;
                    tablenew.AddCell(cellPermAddress);

                    #region Present address String array

                    string[] PrAdress = new string[8];
                    if (prTaluka.Length > 0)
                    {
                        PrAdress[0] = prTaluka;
                    }
                    else
                    {
                        PrAdress[0] = "";
                    }
                    if (prTown.Length > 0)
                    {
                        PrAdress[1] = prTown;
                    }
                    else
                    {
                        PrAdress[1] = "";
                    }
                    if (prLmark.Length > 0)
                    {
                        PrAdress[2] = prLmark;
                    }
                    else
                    {
                        PrAdress[2] = "";
                    }
                    if (prarea.Length > 0)
                    {
                        PrAdress[3] = prarea;
                    }
                    else
                    {
                        PrAdress[3] = "";
                    }
                    if (prcity.Length > 0)
                    {
                        PrAdress[4] = prcity;
                    }
                    else
                    {
                        PrAdress[4] = "";
                    }
                    if (prDistrict.Length > 0)
                    {
                        PrAdress[5] = prDistrict;
                    }
                    else
                    {
                        PrAdress[5] = "";
                    }
                    if (prPincode.Length > 0)
                    {
                        PrAdress[6] = prPincode;
                    }
                    else
                    {
                        PrAdress[6] = "";
                    }
                    if (prState.Length > 0)
                    {
                        PrAdress[7] = prState;
                    }
                    else
                    {
                        PrAdress[7] = "";
                    }

                    string Address1 = string.Empty;

                    for (int k = 0; k < 8; k++)
                    {
                        Address1 += "  " + PrAdress[k];
                    }


                    #endregion
                    PdfPCell cellLocalAddress = new PdfPCell(new Phrase(Address1.Trim(), FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
                    cellLocalAddress.HorizontalAlignment = 0;
                    cellLocalAddress.Colspan = 0;
                    cellLocalAddress.Border = 0;
                    cellLocalAddress.PaddingLeft = 0;
                    cellLocalAddress.PaddingTop = 3;
                    cellLocalAddress.PaddingBottom = 3;
                    cellLocalAddress.BorderWidthLeft = 0;
                    cellLocalAddress.BorderWidthRight = 0.9f;
                    cellLocalAddress.BorderWidthTop = 0;
                    cellLocalAddress.BorderWidthBottom = 0.5f;
                    tablenew.AddCell(cellLocalAddress);

                    PdfPCell cellDtofJoin = new PdfPCell(new Phrase(date, FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
                    cellDtofJoin.HorizontalAlignment = 1;
                    cellDtofJoin.Colspan = 0;
                    cellDtofJoin.PaddingLeft = 0;
                    cellDtofJoin.PaddingTop = 3;
                    cellDtofJoin.BorderWidthLeft = 0;
                    cellDtofJoin.BorderWidthRight = 0.9f;
                    cellDtofJoin.BorderWidthTop = 0;
                    cellDtofJoin.BorderWidthBottom = 0.5f;
                    tablenew.AddCell(cellDtofJoin);

                    PdfPCell cellSign = new PdfPCell(new Phrase("  ", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
                    cellSign.HorizontalAlignment = 1;
                    cellSign.Colspan = 0;
                    cellSign.Border = 0;
                    cellSign.PaddingLeft = 0;
                    cellSign.PaddingTop = 3;
                    cellSign.PaddingBottom = 3;
                    cellSign.BorderWidthLeft = 0;
                    cellSign.BorderWidthRight = 0.9f;
                    cellSign.BorderWidthTop = 0;
                    cellSign.BorderWidthBottom = 0.5f;
                    tablenew.AddCell(cellSign);

                    PdfPCell cellDtofLeaving = new PdfPCell(new Phrase(DtofLeaving, FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
                    cellDtofLeaving.HorizontalAlignment = 1;
                    cellDtofLeaving.Colspan = 0;
                    cellDtofLeaving.Border = 0;
                    cellDtofLeaving.PaddingLeft = 0;
                    cellDtofLeaving.PaddingTop = 3;
                    cellDtofLeaving.PaddingBottom = 3;
                    cellDtofLeaving.BorderWidthLeft = 0;
                    cellDtofLeaving.BorderWidthRight = 0.9f;
                    cellDtofLeaving.BorderWidthTop = 0;
                    cellDtofLeaving.BorderWidthBottom = 0.5f;
                    tablenew.AddCell(cellDtofLeaving);

                    PdfPCell cellReason = new PdfPCell(new Phrase("  ", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
                    cellReason.HorizontalAlignment = 1;
                    cellReason.Colspan = 0;
                    cellReason.Border = 0;
                    cellReason.PaddingLeft = 0;
                    cellReason.PaddingTop = 3;
                    cellReason.PaddingBottom = 3;
                    cellReason.BorderWidthLeft = 0;
                    cellReason.BorderWidthRight = 0.9f;
                    cellReason.BorderWidthTop = 0;
                    cellReason.BorderWidthBottom = 0.5f;
                    tablenew.AddCell(cellReason);

                    PdfPCell cellremarks = new PdfPCell(new Phrase(" ", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
                    cellremarks.HorizontalAlignment = 1;
                    cellremarks.Colspan = 0;
                    cellremarks.Border = 0;
                    cellremarks.PaddingLeft = 0;
                    cellremarks.PaddingTop = 3;
                    cellremarks.PaddingBottom = 3;
                    cellremarks.BorderWidthLeft = 0;
                    cellremarks.BorderWidthRight = 0.9f;
                    cellremarks.BorderWidthTop = 0;
                    cellremarks.BorderWidthBottom = 0.5f;
                    tablenew.AddCell(cellremarks);

                    j++;
                }

                document.Add(tablenew);

                #endregion Basic Information of the Employee
                string filename = ddlcname.SelectedItem.Text + "FormXIII.pdf";

                document.Close();
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment;filename=" + filename);
                Response.Buffer = true;
                Response.Clear();
                Response.OutputStream.Write(ms.GetBuffer(), 0, ms.GetBuffer().Length);
                Response.OutputStream.Flush();
                Response.End();


            }
        }

        protected void btnform12_Click(object sender, EventArgs e)
        {

            int Fontsize = 13;
            string fontsyle = "verdana";

            #region Variable Declaration


            #endregion



            if (ddlclientid.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Show alert", "alert('Please select Client');", true);
                return;
            }

            #region  Begin  New Code

            #region Begin Variable Declaration


            string SPName = "";
            Hashtable HTEmpBiodata = new Hashtable();
            string Clientid = "";

            DataTable dtEmpdetails = null;

            #endregion end Variable Declaration


            #region Begin Assign Values to The Variables
            SPName = "ClientFormsPDF";
            Clientid = ddlclientid.SelectedValue;




            #endregion End Assign values To the Variables



            DateTime frmdate;
            string FromDate = "";
            string Frmonth = "";
            string FrYear = "";

            if (txtfrom.Text.Trim().Length > 0)
            {
                frmdate = DateTime.ParseExact(txtfrom.Text.Trim(), "MMM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
                Frmonth = frmdate.ToString("MM");
                FrYear = frmdate.ToString("yy");
            }

            FromDate = FrYear + Frmonth;
            DateTime tdate;
            string ToDate = "";
            string Tomonth = "";
            string ToYear = "";

            if (txtto.Text.Trim().Length > 0)
            {
                tdate = DateTime.ParseExact(txtto.Text.Trim(), "MMM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
                Tomonth = tdate.ToString("MM");
                ToYear = tdate.ToString("yy");

            }

            ToDate = ToYear + Tomonth;

            int forms = ddlForms.SelectedIndex;

            #region Begin Pass values to the Hash table
            HTEmpBiodata.Add("@Clientid", Clientid);
            HTEmpBiodata.Add("@forms", forms);
            HTEmpBiodata.Add("@fromdate", FromDate);
            HTEmpBiodata.Add("@todate", ToDate);
            //HTEmpBiodata.Add("@month", fromdate + todate);

            #endregion  end Pass values to the Hash table

            #region Begin  Call Stored Procedure
            dtEmpdetails = config.ExecuteAdaptorAsyncWithParams(SPName, HTEmpBiodata).Result;
            #endregion  End  Call Stored Procedure

            #endregion End New Code As on [31-05-2014]


            string Companyname = "";
            string Companyaddress = "";
            string pfwages = "";
            string pf = "";
            string pfempr = "";
            string noofemplys = "";

            if (dtEmpdetails.Rows.Count > 0)
            {

                Companyname = dtEmpdetails.Rows[0]["CompanyName"].ToString();
                Companyaddress = dtEmpdetails.Rows[0]["CompanyAddress"].ToString();
                pfwages = dtEmpdetails.Rows[0]["PFWages"].ToString();
                pf = dtEmpdetails.Rows[0]["PF"].ToString();
                pfempr = dtEmpdetails.Rows[0]["PFEmpr"].ToString();
                noofemplys = dtEmpdetails.Rows[0]["noemplys"].ToString();


            }

            string qry = "select PFEmployee from TblOptions ";
            DataTable dttblroptns = config.ExecuteAdaptorAsyncWithQueryParams(qry).Result;
            string pfemplyee = "";
            if (dttblroptns.Rows.Count > 0)
            {
                pfemplyee = dttblroptns.Rows[0]["PFEmployee"].ToString();

            }

            MemoryStream ms = new MemoryStream();

            Document document = new Document(PageSize.LEGAL.Rotate());

            // var output = new FileStream(fileheader2, FileMode., FileAccess.Write, FileShare.None);
            var writer = PdfWriter.GetInstance(document, ms);
            document.Open();
            BaseFont bf = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            string imagepath1 = Server.MapPath("images");
            #region

            PdfPTable tablenewc = new PdfPTable(9);
            tablenewc.TotalWidth = 900f;
            tablenewc.LockedWidth = true;
            float[] width = new float[] { 4f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f };
            tablenewc.SetWidths(width);

            PdfPCell cellspace = new PdfPCell(new Phrase("  ", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize - 2, Font.BOLD, BaseColor.BLACK)));
            cellspace.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
            cellspace.Colspan = 9;
            cellspace.Border = 0;
            cellspace.PaddingTop = 0;

            PdfPCell cellonlyun = new PdfPCell(new Phrase("(ONLY FOR UN-EXEMPTED ESTABLISHMENTS ", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize - 2, Font.BOLD, BaseColor.BLACK)));
            cellonlyun.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            cellonlyun.Colspan = 9;
            cellonlyun.Border = 0;
            tablenewc.AddCell(cellonlyun);

            PdfPCell provident = new PdfPCell(new Phrase("Employees' Provident Fund Organisation ", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, Font.BOLD, BaseColor.BLACK)));
            provident.HorizontalAlignment = 1;
            provident.Colspan = 9;
            provident.Border = 0;
            tablenewc.AddCell(provident);

            PdfPCell cellemp = new PdfPCell(new Phrase("THE EMPLOYEES' PROVIDENT FUND  AND  MISC.PROVISIONS ACT,1952- EMPLOYEES' PENSION SCHEME [PARA 20 (4) ]  ", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.BOLD, BaseColor.BLACK)));
            cellemp.HorizontalAlignment = 1;
            cellemp.Colspan = 9;
            cellemp.Border = 0;
            tablenewc.AddCell(cellemp);

            PdfPCell cellHead = new PdfPCell(new Phrase("Form 12-A ", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.BOLD, BaseColor.BLACK)));
            cellHead.HorizontalAlignment = 1;
            cellHead.Colspan = 9;
            cellHead.Border = 0;
            tablenewc.AddCell(cellHead);

            PdfPCell cellmonth = new PdfPCell(new Phrase("Name and address of  the Factory / Establishment:\n" + Companyname + "\n" + Companyaddress, FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize - 1, Font.NORMAL, BaseColor.BLACK)));
            cellmonth.HorizontalAlignment = 0;
            cellmonth.Colspan = 3;
            cellmonth.Border = 0;
            tablenewc.AddCell(cellmonth);

            PdfPCell nameaddress = new PdfPCell(new Phrase("Currency period from   " + FromDate + "   to  " + ToDate, FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize - 1, Font.NORMAL, BaseColor.BLACK)));
            nameaddress.HorizontalAlignment = 0;
            nameaddress.Colspan = 4;
            nameaddress.Border = 0;
            tablenewc.AddCell(nameaddress);

            PdfPCell cellcode = new PdfPCell(new Phrase(" ( To be filled by the EPFO)", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize - 1, Font.NORMAL, BaseColor.BLACK)));
            cellcode.HorizontalAlignment = 2;
            cellcode.Colspan = 2;
            cellcode.Border = 0;
            tablenewc.AddCell(cellcode);

            PdfPCell epty = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize - 1, Font.NORMAL, BaseColor.BLACK)));
            epty.HorizontalAlignment = 0;
            epty.Colspan = 3;
            epty.Border = 0;
            tablenewc.AddCell(epty);

            PdfPCell Statement = new PdfPCell(new Phrase("Statement of contribution for the month of", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize - 1, Font.NORMAL, BaseColor.BLACK)));
            Statement.HorizontalAlignment = 0;
            Statement.Colspan = 4;
            Statement.Border = 0;
            tablenewc.AddCell(Statement);

            PdfPCell Group = new PdfPCell(new Phrase("Group code :", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize - 1, Font.NORMAL, BaseColor.BLACK)));
            Group.HorizontalAlignment = 2;
            Group.Colspan = 2;
            Group.Border = 0;
            tablenewc.AddCell(Group);

            PdfPCell Establishment = new PdfPCell(new Phrase("Establishment Status :", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize - 1, Font.NORMAL, BaseColor.BLACK)));
            Establishment.HorizontalAlignment = 2;
            Establishment.Colspan = 9;
            Establishment.Border = 0;
            tablenewc.AddCell(Establishment);

            PdfPCell code1 = new PdfPCell(new Phrase("Group code :", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize - 1, Font.NORMAL, BaseColor.BLACK)));
            code1.HorizontalAlignment = 0;
            code1.Colspan = 4;
            code1.Border = 0;
            tablenewc.AddCell(code1);

            PdfPCell Statutory = new PdfPCell(new Phrase("Statutory rate of contribution   " + pfemplyee + ".00%", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize - 1, Font.NORMAL, BaseColor.BLACK)));
            Statutory.HorizontalAlignment = 0;
            Statutory.Colspan = 5;
            Statutory.Border = 0;
            tablenewc.AddCell(Statutory);



            #region
            PdfPCell empty6 = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
            empty6.HorizontalAlignment = 1;
            empty6.BorderWidthBottom = 0;
            empty6.BorderWidthLeft = .5f;
            empty6.BorderWidthRight = .5f;
            empty6.BorderWidthTop = .5f;
            tablenewc.AddCell(empty6);

            PdfPCell empty5 = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
            empty5.HorizontalAlignment = 1;
            empty5.BorderWidthBottom = 0;
            empty5.BorderWidthLeft = .5f;
            empty5.BorderWidthRight = .5f;
            empty5.BorderWidthTop = .5f;
            tablenewc.AddCell(empty5);

            PdfPCell empty4 = new PdfPCell(new Phrase("Amount of Contribution \n (3) ", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
            empty4.HorizontalAlignment = 1;
            empty4.Colspan = 2;
            tablenewc.AddCell(empty4);

            PdfPCell empty3 = new PdfPCell(new Phrase("Amount of Contribution remitted \n (4)", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
            empty3.HorizontalAlignment = 1;
            empty3.Colspan = 2;
            tablenewc.AddCell(empty3);

            PdfPCell empty2 = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
            empty2.HorizontalAlignment = 1;
            empty2.BorderWidthBottom = 0;
            empty2.BorderWidthLeft = .5f;
            empty2.BorderWidthRight = 0;
            empty2.BorderWidthTop = .5f;
            tablenewc.AddCell(empty2);

            PdfPCell empty1 = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
            empty1.HorizontalAlignment = 1;
            empty1.BorderWidthBottom = 0;
            empty1.BorderWidthLeft = .5f;
            empty1.BorderWidthRight = .5f;
            empty1.BorderWidthTop = .5f;
            tablenewc.AddCell(empty1);

            PdfPCell empty = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
            empty.HorizontalAlignment = 1;
            empty.BorderWidthBottom = 0;
            empty.BorderWidthLeft = .5f;
            empty.BorderWidthRight = .5f;
            empty.BorderWidthTop = .5f;
            tablenewc.AddCell(empty);



            #endregion

            PdfPCell celltable10 = new PdfPCell(new Phrase("Particulars \n (1)", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
            celltable10.HorizontalAlignment = 1;
            celltable10.Colspan = 0;
            celltable10.BorderWidthBottom = .5f;
            celltable10.BorderWidthLeft = .5f;
            celltable10.BorderWidthRight = .5f;
            celltable10.BorderWidthTop = 0;
            tablenewc.AddCell(celltable10);

            PdfPCell Account = new PdfPCell(new Phrase("Wages on  which contributions are payable \n (2)", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
            Account.HorizontalAlignment = 1;
            Account.Colspan = 0;
            Account.BorderWidthBottom = .5f;
            Account.BorderWidthLeft = .5f;
            Account.BorderWidthRight = .5f;
            Account.BorderWidthTop = 0;
            tablenewc.AddCell(Account);

            PdfPCell Name = new PdfPCell(new Phrase("Recovered from the Employees  ", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
            Name.HorizontalAlignment = 1;
            Name.Colspan = 0;
            tablenewc.AddCell(Name);

            PdfPCell Father = new PdfPCell(new Phrase("Payable from the Employeer", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
            Father.HorizontalAlignment = 1;
            Father.Colspan = 0;
            tablenewc.AddCell(Father);

            PdfPCell Date = new PdfPCell(new Phrase("Employee'Share", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
            Date.HorizontalAlignment = 1;
            Date.Colspan = 0;
            tablenewc.AddCell(Date);

            PdfPCell reson = new PdfPCell(new Phrase("Employer's Share", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
            reson.HorizontalAlignment = 1;
            reson.Colspan = 0;
            tablenewc.AddCell(reson);

            PdfPCell Amount = new PdfPCell(new Phrase("Amount ofadministrative charges due \n (5)", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
            Amount.HorizontalAlignment = 1;
            Amount.Colspan = 0;
            Amount.BorderWidthBottom = .5f;
            Amount.BorderWidthLeft = .5f;
            Amount.BorderWidthRight = .5f;
            Amount.BorderWidthTop = 0;
            tablenewc.AddCell(Amount);

            PdfPCell Amount1 = new PdfPCell(new Phrase("Amount ofadministrative charges due remitted \n (6)", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
            Amount1.HorizontalAlignment = 1;
            Amount1.Colspan = 0;
            Amount1.BorderWidthBottom = .5f;
            Amount1.BorderWidthLeft = .5f;
            Amount1.BorderWidthRight = .5f;
            Amount1.BorderWidthTop = 0;
            tablenewc.AddCell(Amount1);

            PdfPCell Date1 = new PdfPCell(new Phrase("Date of remittance (enclose triplicate copy of challan/s) \n (7)", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
            Date1.HorizontalAlignment = 1;
            Date1.Colspan = 0;
            Date1.BorderWidthBottom = .5f;
            Date1.BorderWidthLeft = .5f;
            Date1.BorderWidthRight = .5f;
            Date1.BorderWidthTop = 0;
            tablenewc.AddCell(Date1);

            PdfPCell cellepf1 = new PdfPCell(new Phrase("E.P.F. A/c No.01", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize - 1, Font.NORMAL, BaseColor.BLACK)));
            cellepf1.HorizontalAlignment = 0;
            cellepf1.Colspan = 0;
            tablenewc.AddCell(cellepf1);

            PdfPCell cellepf2 = new PdfPCell(new Phrase(pfwages, FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
            cellepf2.HorizontalAlignment = 1;
            cellepf2.Colspan = 0;
            tablenewc.AddCell(cellepf2);

            PdfPCell cellepf3 = new PdfPCell(new Phrase(pf, FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
            cellepf3.HorizontalAlignment = 1;
            cellepf3.Colspan = 0;
            tablenewc.AddCell(cellepf3);

            PdfPCell cellepf4 = new PdfPCell(new Phrase(pfempr, FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
            cellepf4.HorizontalAlignment = 1;
            cellepf4.Colspan = 0;
            tablenewc.AddCell(cellepf4);

            PdfPCell cellepf5 = new PdfPCell(new Phrase(pf, FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
            cellepf5.HorizontalAlignment = 1;
            cellepf5.Colspan = 0;
            tablenewc.AddCell(cellepf5);

            PdfPCell cellepf6 = new PdfPCell(new Phrase(pfempr, FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
            cellepf6.HorizontalAlignment = 1;
            cellepf6.Colspan = 0;
            tablenewc.AddCell(cellepf6);

            PdfPCell cellepf7 = new PdfPCell(new Phrase(" ", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
            cellepf7.HorizontalAlignment = 1;
            cellepf7.Colspan = 0;
            tablenewc.AddCell(cellepf7);

            PdfPCell cellepf8 = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
            cellepf8.HorizontalAlignment = 1;
            cellepf8.Colspan = 0;
            tablenewc.AddCell(cellepf8);

            PdfPCell cellepf9 = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
            cellepf9.HorizontalAlignment = 1;
            cellepf9.Colspan = 0;
            tablenewc.AddCell(cellepf9);

            PdfPCell cell2 = new PdfPCell(new Phrase("Pension Fund A/c.No.10", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize - 1, Font.NORMAL, BaseColor.BLACK)));
            cell2.HorizontalAlignment = 0;
            cell2.Colspan = 0;
            tablenewc.AddCell(cell2);

            PdfPCell pensionfund1 = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
            pensionfund1.HorizontalAlignment = 1;
            pensionfund1.Colspan = 0;
            tablenewc.AddCell(pensionfund1);

            PdfPCell pensionfund2 = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
            pensionfund2.HorizontalAlignment = 1;
            pensionfund2.Colspan = 0;
            tablenewc.AddCell(pensionfund2);

            PdfPCell pensionfund3 = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
            pensionfund3.HorizontalAlignment = 1;
            pensionfund3.Colspan = 0;
            tablenewc.AddCell(pensionfund3);

            PdfPCell pensionfund4 = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
            pensionfund4.HorizontalAlignment = 1;
            pensionfund4.Colspan = 0;
            tablenewc.AddCell(pensionfund4);

            PdfPCell pensionfund5 = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
            pensionfund5.HorizontalAlignment = 1;
            pensionfund5.Colspan = 0;
            tablenewc.AddCell(pensionfund5);

            PdfPCell pensionfund6 = new PdfPCell(new Phrase(" ", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
            pensionfund6.HorizontalAlignment = 1;
            pensionfund6.Colspan = 0;
            tablenewc.AddCell(pensionfund6);

            PdfPCell pensionfund7 = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
            pensionfund7.HorizontalAlignment = 1;
            pensionfund7.Colspan = 0;
            tablenewc.AddCell(pensionfund7);

            PdfPCell pensionfund8 = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
            pensionfund8.HorizontalAlignment = 1;
            pensionfund8.Colspan = 0;
            tablenewc.AddCell(pensionfund8);

            PdfPCell edlac1 = new PdfPCell(new Phrase("E.D.L.I.A/c.No.21", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize - 1, Font.NORMAL, BaseColor.BLACK)));
            edlac1.HorizontalAlignment = 0;
            edlac1.Colspan = 0;
            tablenewc.AddCell(edlac1);

            PdfPCell edlac2 = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
            edlac2.HorizontalAlignment = 1;
            edlac2.Colspan = 0;
            tablenewc.AddCell(edlac2);

            PdfPCell edlac3 = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
            edlac3.HorizontalAlignment = 1;
            edlac3.Colspan = 0;
            tablenewc.AddCell(edlac3);

            PdfPCell edlac4 = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
            edlac4.HorizontalAlignment = 1;
            edlac4.Colspan = 0;
            tablenewc.AddCell(edlac4);

            PdfPCell edlac5 = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
            edlac5.HorizontalAlignment = 1;
            edlac5.Colspan = 0;
            tablenewc.AddCell(edlac5);

            PdfPCell edlac6 = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
            edlac6.HorizontalAlignment = 1;
            edlac6.Colspan = 0;
            tablenewc.AddCell(edlac6);

            PdfPCell edlac7 = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
            edlac7.HorizontalAlignment = 1;
            edlac7.Colspan = 0;
            tablenewc.AddCell(edlac7);

            PdfPCell edlac8 = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
            edlac8.HorizontalAlignment = 1;
            edlac8.Colspan = 0;
            tablenewc.AddCell(edlac8);

            PdfPCell edlac9 = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
            edlac9.HorizontalAlignment = 1;
            edlac9.Colspan = 0;
            tablenewc.AddCell(edlac9);
            tablenewc.AddCell(cellspace);

            PdfPCell cellsno1 = new PdfPCell(new Phrase("Total No. of Employees :" + noofemplys, FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
            cellsno1.HorizontalAlignment = 0;
            cellsno1.Colspan = 4;
            cellsno1.Border = 0;
            tablenewc.AddCell(cellsno1);

            PdfPCell cellacc2 = new PdfPCell(new Phrase("Name and address of the bank in which the amount is remitted :", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
            cellacc2.HorizontalAlignment = 0;
            cellacc2.Colspan = 5;
            cellacc2.Border = 0;
            tablenewc.AddCell(cellacc2);

            PdfPCell cellname3 = new PdfPCell(new Phrase("(a) Contract :", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
            cellname3.HorizontalAlignment = 0;
            cellname3.Colspan = 9;
            cellname3.Border = 0;
            tablenewc.AddCell(cellname3);

            PdfPCell cellfather4 = new PdfPCell(new Phrase("(b) Rest :", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
            cellfather4.HorizontalAlignment = 0;
            cellfather4.Colspan = 9;
            cellfather4.Border = 0;
            tablenewc.AddCell(cellfather4);

            PdfPCell cellbirh5 = new PdfPCell(new Phrase("(c) Total :", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
            cellbirh5.HorizontalAlignment = 0;
            cellbirh5.Colspan = 9;
            cellbirh5.Border = 0;
            tablenewc.AddCell(cellbirh5);
            tablenewc.AddCell(cellspace);

            document.Add(tablenewc);


            PdfPTable tempTable2 = new PdfPTable(8);
            tempTable2.TotalWidth = 900f;
            tempTable2.LockedWidth = true;
            float[] tempWidth2 = new float[] { 7f, 2f, 2f, 2f, 2f, 2f, 2f, 2f };
            tempTable2.SetWidths(tempWidth2);

            PdfPTable childtable1 = new PdfPTable(4);
            childtable1.TotalWidth = 560f;
            childtable1.LockedWidth = true;
            float[] childtblewidth = new float[] { 8f, 1.5f, 2f, 2f };
            childtable1.SetWidths(childtblewidth);

            #region


            PdfPCell Details = new PdfPCell(new Phrase("Details of Subscriberss", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
            Details.HorizontalAlignment = 0;
            Details.Colspan = 1;
            Details.BorderWidthBottom = .5f;
            Details.BorderWidthLeft = 0;
            Details.BorderWidthRight = .5f;
            Details.BorderWidthTop = .5f;
            childtable1.AddCell(Details);

            PdfPCell EPF = new PdfPCell(new Phrase("EPF", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
            EPF.HorizontalAlignment = 0;
            EPF.Colspan = 1;
            childtable1.AddCell(EPF);

            PdfPCell EPS = new PdfPCell(new Phrase("EPS", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
            EPS.HorizontalAlignment = 0;
            EPS.Colspan = 1;
            childtable1.AddCell(EPS);

            PdfPCell EDLI = new PdfPCell(new Phrase("EDLI", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
            EDLI.HorizontalAlignment = 0;
            EDLI.Colspan = 1;
            childtable1.AddCell(EDLI);



            PdfPCell last = new PdfPCell(new Phrase(" No.of subscribers as per last month(vide Form 12 A)", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
            last.HorizontalAlignment = 0;
            last.Colspan = 1;
            last.BorderWidthBottom = 0;
            last.BorderWidthLeft = 0;
            last.BorderWidthRight = .5f;
            last.BorderWidthTop = .5f;
            childtable1.AddCell(last);

            PdfPCell noofEPF = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
            noofEPF.HorizontalAlignment = 0;
            noofEPF.Colspan = 1;
            noofEPF.BorderWidthBottom = 0;
            noofEPF.BorderWidthLeft = .5f;
            noofEPF.BorderWidthRight = .5f;
            noofEPF.BorderWidthTop = .5f;
            childtable1.AddCell(noofEPF);

            PdfPCell noofEPS = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
            noofEPS.HorizontalAlignment = 0;
            noofEPS.Colspan = 1;
            noofEPS.BorderWidthBottom = 0;
            noofEPS.BorderWidthLeft = 0;
            noofEPS.BorderWidthRight = .5f;
            noofEPS.BorderWidthTop = .5f;
            childtable1.AddCell(noofEPS);

            PdfPCell noofEDLI = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
            noofEDLI.HorizontalAlignment = 0;
            noofEDLI.Colspan = 1;
            noofEDLI.BorderWidthBottom = 0;
            noofEDLI.BorderWidthLeft = 0;
            noofEDLI.BorderWidthRight = .5f;
            noofEDLI.BorderWidthTop = .5f;
            childtable1.AddCell(noofEDLI);


            PdfPCell newsub = new PdfPCell(new Phrase(" No. of New subscribers (Vide Form 5)", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
            newsub.HorizontalAlignment = 0;
            newsub.Colspan = 1;
            newsub.BorderWidthBottom = 0;
            newsub.BorderWidthLeft = 0;
            newsub.BorderWidthRight = .5f;
            newsub.BorderWidthTop = 0;
            childtable1.AddCell(newsub);

            PdfPCell newsubepf = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
            newsubepf.HorizontalAlignment = 0;
            newsubepf.Colspan = 1;
            newsubepf.BorderWidthBottom = 0;
            newsubepf.BorderWidthLeft = .5f;
            newsubepf.BorderWidthRight = .5f;
            newsubepf.BorderWidthTop = 0;
            childtable1.AddCell(newsubepf);

            PdfPCell newsubEPS = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
            newsubEPS.HorizontalAlignment = 0;
            newsubEPS.Colspan = 1;
            newsubEPS.BorderWidthBottom = 0;
            newsubEPS.BorderWidthLeft = .5f;
            newsubEPS.BorderWidthRight = .5f;
            newsubEPS.BorderWidthTop = 0;
            childtable1.AddCell(newsubEPS);

            PdfPCell newsubEDLI = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
            newsubEDLI.HorizontalAlignment = 0;
            newsubEDLI.Colspan = 1;
            newsubEDLI.BorderWidthBottom = 0;
            newsubEDLI.BorderWidthLeft = .5f;
            newsubEDLI.BorderWidthRight = .5f;
            newsubEDLI.BorderWidthTop = 0;
            childtable1.AddCell(newsubEDLI);


            PdfPCell nofsubsub = new PdfPCell(new Phrase(" No.of subscribers left service (vide Form 10) ", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
            nofsubsub.HorizontalAlignment = 0;
            nofsubsub.Colspan = 1;
            nofsubsub.BorderWidthBottom = 0;
            nofsubsub.BorderWidthLeft = 0;
            nofsubsub.BorderWidthRight = .5f;
            nofsubsub.BorderWidthTop = 0;
            childtable1.AddCell(nofsubsub);

            PdfPCell nosubepf = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
            nosubepf.HorizontalAlignment = 0;
            nosubepf.Colspan = 1;
            nosubepf.BorderWidthBottom = 0;
            nosubepf.BorderWidthLeft = .5f;
            nosubepf.BorderWidthRight = .5f;
            nosubepf.BorderWidthTop = 0;
            childtable1.AddCell(nosubepf);

            PdfPCell nosubEPS = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
            nosubEPS.HorizontalAlignment = 0;
            nosubEPS.Colspan = 1;
            nosubEPS.BorderWidthBottom = 0;
            nosubEPS.BorderWidthLeft = .5f;
            nosubEPS.BorderWidthRight = .5f;
            nosubEPS.BorderWidthTop = 0;
            childtable1.AddCell(nosubEPS);

            PdfPCell nosubEDLI = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
            nosubEDLI.HorizontalAlignment = 0;
            nosubEDLI.Colspan = 1;
            nosubEDLI.BorderWidthBottom = 0;
            nosubEDLI.BorderWidthLeft = .5f;
            nosubEDLI.BorderWidthRight = .5f;
            nosubEDLI.BorderWidthTop = 0;
            childtable1.AddCell(nosubEDLI);



            PdfPCell netsubsub = new PdfPCell(new Phrase(" Net. No. of subscribers ", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
            netsubsub.HorizontalAlignment = 0;
            netsubsub.Colspan = 1;
            netsubsub.BorderWidthBottom = .5f;
            netsubsub.BorderWidthLeft = 0;
            netsubsub.BorderWidthRight = .5f;
            netsubsub.BorderWidthTop = 0;
            childtable1.AddCell(netsubsub);

            PdfPCell netsubepf = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
            netsubepf.HorizontalAlignment = 0;
            netsubepf.Colspan = 1;
            netsubepf.BorderWidthBottom = .5f;
            netsubepf.BorderWidthLeft = .5f;
            netsubepf.BorderWidthRight = .5f;
            netsubepf.BorderWidthTop = 0;
            childtable1.AddCell(netsubepf);

            PdfPCell netsubEPS = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
            netsubEPS.HorizontalAlignment = 0;
            netsubEPS.Colspan = 1;
            netsubEPS.BorderWidthBottom = .5f;
            netsubEPS.BorderWidthLeft = .5f;
            netsubEPS.BorderWidthRight = .5f;
            netsubEPS.BorderWidthTop = 0;
            childtable1.AddCell(netsubEPS);

            PdfPCell netsubEDLI = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
            netsubEDLI.HorizontalAlignment = 0;
            netsubEDLI.Colspan = 1;
            netsubEDLI.BorderWidthBottom = .5f;
            netsubEDLI.BorderWidthLeft = .5f;
            netsubEDLI.BorderWidthRight = .5f;
            netsubEDLI.BorderWidthTop = 0;
            childtable1.AddCell(netsubEDLI);
            #endregion

            PdfPCell endchildTable1 = new PdfPCell(childtable1);
            endchildTable1.HorizontalAlignment = 0;
            endchildTable1.Colspan = 4;
            tempTable2.AddCell(endchildTable1);

            PdfPTable childtable2 = new PdfPTable(4);
            childtable2.TotalWidth = 350f;
            childtable2.LockedWidth = true;
            float[] childtblewidth1 = new float[] { 2f, 2f, 2f, 2f };
            childtable2.SetWidths(childtblewidth1);


            PdfPCell sign = new PdfPCell(new Phrase(" Signature of the Employer(with office seal)", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
            sign.HorizontalAlignment = 2;
            sign.Colspan = 4;
            sign.Border = 0;
            sign.PaddingTop = 70;
            childtable2.AddCell(sign);


            PdfPCell endchildTable2 = new PdfPCell(childtable2);
            endchildTable2.Border = 0;
            endchildTable2.Colspan = 4;
            endchildTable2.HorizontalAlignment = 0;
            tempTable2.AddCell(endchildTable2);

            document.Add(tempTable2);

            #endregion Basic Information of the Employee

            document.NewPage();

            PdfPTable tblFingerprints = new PdfPTable(6);
            tblFingerprints.TotalWidth = 500f;
            tblFingerprints.LockedWidth = true;
            float[] widthfinger = new float[] { 2f, 1.5f, 2f, 2f, 1.5f, 2f };
            tblFingerprints.SetWidths(widthfinger);


            string filename = "FormXXII.pdf";

            document.Close();
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=" + filename);
            Response.Buffer = true;
            Response.Clear();
            Response.OutputStream.Write(ms.GetBuffer(), 0, ms.GetBuffer().Length);
            Response.OutputStream.Flush();
            Response.End();

            //}



            //else
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "Showalert", "alert('Duration expired');", true);
            //    return;
            //}
        }

    }
}