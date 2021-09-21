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
    public partial class EmpForms : System.Web.UI.Page
    {
        string EmpIDPrefix = "";
        string CmpIDPrefix = "";
        string Elength = "";
        string Clength = "";
        string Fontstyle = "";
        string FontStyle = "Tahoma";
        AppConfiguration config = new AppConfiguration();
        GridViewExportUtil gve = new GridViewExportUtil();
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

                    }
                    else
                    {
                        Response.Redirect("login.aspx");
                    }
                }
            }
            catch (Exception ex)
            {

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

        protected void ddlForms_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlForms.SelectedIndex == 0)
            {


                txtEmpDtofJoining.Visible = false;

                lblDOJ.Visible = false;

            }
            if (ddlForms.SelectedIndex == 1)
            {
                lblDOJ.Visible = true;
                txtEmpDtofJoining.Visible = true;


            }

            if (ddlForms.SelectedIndex == 2)
            {
                lblDOJ.Visible = true;
                txtEmpDtofJoining.Visible = true;


            }
        }


        protected void btnForms_Click(object sender, EventArgs e)
        {

            if (ddlForms.SelectedIndex == 1)
            {
                btnform5_Click(sender, e);
                return;
            }

            if (ddlForms.SelectedIndex == 2)
            {
                btnform10_Click(sender, e);
                return;
            }
        }

        protected void btnform5_Click(object sender, EventArgs e)
        {


            int Fontsize = 10;
            string fontsyle = "verdana";

            #region Variable Declaration

            string contactno = "";
            string Idno = "";
            string date = "";
            string postappliedfor = "";
            string name = "";
            string fathername = "";






            #endregion









            string Gender = "";
            string Companyname = "";
            string CompanyAddress = "";
            string remarks = "";
            string accno = "";
            string Empdateofbirth = "";
            string pfNo = "";
            int j = 1;

            DateTime tdate;
            string ToDate = "";
            string Tomonth = "";
            string ToYear = "";
            string cmpnypfno = "";

            if (txtEmpDtofJoining.Text.Trim().Length > 0)
            {
                tdate = DateTime.ParseExact(txtEmpDtofJoining.Text.Trim(), "MMM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
                Tomonth = tdate.ToString("MM");
                ToYear = tdate.ToString("yyyy");

            }

            ToDate = ToYear + Tomonth;

            string query = "Select epf.empepfno,(ed.empfname+''+ed.empmname+''+ed.emplname) as empname,ed.empfathername,case ed.empsex when 'M' then 'Male' when 'F' then 'Female' else 'Transgender' end Gender,case convert(varchar(10),ed.empdtofjoining,103) when '01/01/1900' then '' else convert(varchar(10),ed.empdtofjoining,103) end empdtofjoining,case convert(varchar(10),ed.empdtofbirth,103) when '01/01/1900' then '' else convert(varchar(10),ed.empdtofbirth,103) end empdtofbirth,ed.empremarks from empdetails ed left join empepfcodes epf on epf.empid=ed.empid  WHERE MONTH(EmpDtofJoining)='" + Tomonth + "' AND YEAR(EmpDtofJoining)='" + ToYear + "' and epf.empepfno<>'' and len(epf.empepfno)>0";
            DataTable dtEmpdetails = config.ExecuteAdaptorAsyncWithQueryParams(query).Result;


            if (dtEmpdetails.Rows.Count > 0)
            {


                string qry = "select * from CompanyInfo ";
                DataTable dttblroptns = config.ExecuteAdaptorAsyncWithQueryParams(qry).Result;
                if (dttblroptns.Rows.Count > 0)
                {
                    Companyname = dttblroptns.Rows[0]["CompanyName"].ToString();
                    CompanyAddress = dttblroptns.Rows[0]["Address"].ToString();
                    cmpnypfno = dttblroptns.Rows[0]["PFNo"].ToString();
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
                float[] width = new float[] { 1.5f, 4f, 5f, 5f, 3f, 2f, 3f, 7f, 3.5f };
                tablenewc.SetWidths(width);

                PdfPCell cellspace = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize - 2, Font.BOLD, BaseColor.BLACK)));
                cellspace.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                cellspace.Colspan = 11;
                cellspace.Border = 0;
                cellspace.PaddingTop = 0;

                PdfPCell cellHead = new PdfPCell(new Phrase("Form-5 ", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize + 2, Font.BOLD, BaseColor.BLACK)));
                cellHead.HorizontalAlignment = 1;
                cellHead.Colspan = 11;
                cellHead.Border = 0;
                tablenewc.AddCell(cellHead);

                PdfPCell provident = new PdfPCell(new Phrase("Employees' Provident Fund Organisation", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize + 2, Font.BOLD, BaseColor.BLACK)));
                provident.HorizontalAlignment = 1;
                provident.Colspan = 11;
                provident.Border = 0;
                tablenewc.AddCell(provident);

                PdfPCell cellemp = new PdfPCell(new Phrase("THE EMPLOYEES' PROVIDENT FUND SCHEME,1952[Para 36 (2) (a)] AND THE EMPLOYEES' PENSION SCHEME,1995 [Para 20 (4) ] ", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize + 2, Font.BOLD, BaseColor.BLACK)));
                cellemp.HorizontalAlignment = 1;
                cellemp.Colspan = 11;
                cellemp.Border = 0;
                tablenewc.AddCell(cellemp);

                PdfPCell cellreturn = new PdfPCell(new Phrase("Return of Employees' qualifying for membership of the Employees' Provident Fund , Employees' Pension Fund & Employees' Deposit Linked Insurance Fund for the first time ", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize + 2, Font.NORMAL, BaseColor.BLACK)));
                cellreturn.HorizontalAlignment = 0;
                cellreturn.Colspan = 11;
                cellreturn.Border = 0;
                tablenewc.AddCell(cellreturn);

                PdfPCell cellmonth = new PdfPCell(new Phrase("During the month of  : ", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize + 2, Font.NORMAL, BaseColor.BLACK)));
                cellmonth.HorizontalAlignment = 0;
                cellmonth.Colspan = 11;
                cellmonth.Border = 0;
                tablenewc.AddCell(cellmonth);

                PdfPCell nameaddress = new PdfPCell(new Phrase("Name and address of the Factory / Est.:" + Companyname + ' ' + CompanyAddress, FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize + 2, Font.NORMAL, BaseColor.BLACK)));
                nameaddress.HorizontalAlignment = 0;
                nameaddress.Colspan = 11;
                nameaddress.Border = 0;
                tablenewc.AddCell(nameaddress);

                PdfPCell cellcode = new PdfPCell(new Phrase("Code No. of the Factory / Est.:" + cmpnypfno, FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize + 2, Font.NORMAL, BaseColor.BLACK)));
                cellcode.HorizontalAlignment = 0;
                cellcode.Colspan = 11;
                cellcode.Border = 0;
                tablenewc.AddCell(cellcode);

                tablenewc.AddCell(cellspace);

                PdfPCell celltable10 = new PdfPCell(new Phrase("SL.No. ", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.BOLD, BaseColor.BLACK)));
                celltable10.HorizontalAlignment = 1;
                celltable10.Colspan = 0;
                tablenewc.AddCell(celltable10);

                PdfPCell Account = new PdfPCell(new Phrase("Account No.", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.BOLD, BaseColor.BLACK)));
                Account.HorizontalAlignment = 1;
                Account.Colspan = 0;
                tablenewc.AddCell(Account);

                PdfPCell Name = new PdfPCell(new Phrase("Name Of the Member (In the  block capitals) ", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.BOLD, BaseColor.BLACK)));
                Name.HorizontalAlignment = 1;
                Name.Colspan = 0;
                tablenewc.AddCell(Name);

                PdfPCell Father = new PdfPCell(new Phrase("Father Name (or husband's name in case of married women)", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.BOLD, BaseColor.BLACK)));
                Father.HorizontalAlignment = 1;
                Father.Colspan = 0;
                tablenewc.AddCell(Father);

                PdfPCell birth = new PdfPCell(new Phrase("Date of birth ", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.BOLD, BaseColor.BLACK)));
                birth.HorizontalAlignment = 1;
                birth.Colspan = 0;
                tablenewc.AddCell(birth);

                PdfPCell Sex = new PdfPCell(new Phrase("Sex", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.BOLD, BaseColor.BLACK)));
                Sex.HorizontalAlignment = 1;
                Sex.Colspan = 0;
                tablenewc.AddCell(Sex);

                PdfPCell Date = new PdfPCell(new Phrase("Date of Joining the Fund", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.BOLD, BaseColor.BLACK)));
                Date.HorizontalAlignment = 1;
                Date.Colspan = 0;
                tablenewc.AddCell(Date);

                PdfPCell Total = new PdfPCell(new Phrase("Total period of previous service as on the date of joining the Fund (Enclose scheme Certificate if applicable)", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.BOLD, BaseColor.BLACK)));
                Total.HorizontalAlignment = 1;
                Total.Colspan = 0;
                tablenewc.AddCell(Total);

                PdfPCell Remarks = new PdfPCell(new Phrase("Remarks", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.BOLD, BaseColor.BLACK)));
                Remarks.HorizontalAlignment = 1;
                Remarks.Colspan = 0;
                tablenewc.AddCell(Remarks);

                PdfPCell cell1 = new PdfPCell(new Phrase("1 ", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
                cell1.HorizontalAlignment = 1;
                cell1.Colspan = 0;
                tablenewc.AddCell(cell1);

                PdfPCell cell2 = new PdfPCell(new Phrase("2", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
                cell2.HorizontalAlignment = 1;
                cell2.Colspan = 0;
                tablenewc.AddCell(cell2);

                PdfPCell cell3 = new PdfPCell(new Phrase("3 ", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
                cell3.HorizontalAlignment = 1;
                cell3.Colspan = 0;
                tablenewc.AddCell(cell3);

                PdfPCell cell4 = new PdfPCell(new Phrase("4", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
                cell4.HorizontalAlignment = 1;
                cell4.Colspan = 0;
                tablenewc.AddCell(cell4);

                PdfPCell cell5 = new PdfPCell(new Phrase("5 ", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
                cell5.HorizontalAlignment = 1;
                cell5.Colspan = 0;
                tablenewc.AddCell(cell5);

                PdfPCell cell6 = new PdfPCell(new Phrase("6", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
                cell6.HorizontalAlignment = 1;
                cell6.Colspan = 0;
                tablenewc.AddCell(cell6);

                PdfPCell cell7 = new PdfPCell(new Phrase("7", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
                cell7.HorizontalAlignment = 1;
                cell7.Colspan = 0;
                tablenewc.AddCell(cell7);

                PdfPCell cell8 = new PdfPCell(new Phrase("8", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
                cell8.HorizontalAlignment = 1;
                cell8.Colspan = 0;
                tablenewc.AddCell(cell8);

                PdfPCell cell9 = new PdfPCell(new Phrase("9", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
                cell9.HorizontalAlignment = 1;
                cell9.Colspan = 0;
                tablenewc.AddCell(cell9);

                for (int k = 0; k < dtEmpdetails.Rows.Count; k++)
                {

                    name = dtEmpdetails.Rows[k]["empname"].ToString();
                    fathername = dtEmpdetails.Rows[k]["empfathername"].ToString();
                    date = dtEmpdetails.Rows[k]["empdtofjoining"].ToString();
                    Empdateofbirth = dtEmpdetails.Rows[k]["empdtofbirth"].ToString();
                    pfNo = dtEmpdetails.Rows[k]["empepfno"].ToString();
                    Gender = dtEmpdetails.Rows[k]["gender"].ToString();
                    remarks = dtEmpdetails.Rows[k]["EmpRemarks"].ToString();

                    PdfPCell cellsno1 = new PdfPCell(new Phrase(j.ToString(), FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
                    cellsno1.HorizontalAlignment = 1;
                    cellsno1.Colspan = 0;
                    tablenewc.AddCell(cellsno1);


                    PdfPCell cellacc2 = new PdfPCell(new Phrase(pfNo, FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
                    cellacc2.HorizontalAlignment = 1;
                    cellacc2.Colspan = 0;
                    tablenewc.AddCell(cellacc2);

                    PdfPCell cellname3 = new PdfPCell(new Phrase(name, FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
                    cellname3.HorizontalAlignment = 0;
                    cellname3.Colspan = 0;
                    tablenewc.AddCell(cellname3);

                    PdfPCell cellfather4 = new PdfPCell(new Phrase(fathername, FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
                    cellfather4.HorizontalAlignment = 0;
                    cellfather4.Colspan = 0;
                    tablenewc.AddCell(cellfather4);

                    PdfPCell cellbirh5 = new PdfPCell(new Phrase(date, FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
                    cellbirh5.HorizontalAlignment = 1;
                    cellbirh5.Colspan = 0;
                    tablenewc.AddCell(cellbirh5);

                    PdfPCell cellsex6 = new PdfPCell(new Phrase(Gender, FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
                    cellsex6.HorizontalAlignment = 1;
                    cellsex6.Colspan = 0;
                    tablenewc.AddCell(cellsex6);

                    PdfPCell celldatejoin7 = new PdfPCell(new Phrase(date, FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
                    celldatejoin7.HorizontalAlignment = 1;
                    celldatejoin7.Colspan = 0;
                    tablenewc.AddCell(celldatejoin7);

                    PdfPCell celltotal8 = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
                    celltotal8.HorizontalAlignment = 1;
                    celltotal8.Colspan = 0;
                    tablenewc.AddCell(celltotal8);

                    PdfPCell cellremarks9 = new PdfPCell(new Phrase(remarks, FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
                    cellremarks9.HorizontalAlignment = 1;
                    cellremarks9.Colspan = 0;
                    cellremarks9.FixedHeight = 25f;
                    tablenewc.AddCell(cellremarks9);

                    j++;
                }

                tablenewc.AddCell(cellspace);
                tablenewc.AddCell(cellspace);
                tablenewc.AddCell(cellspace);
                tablenewc.AddCell(cellspace);
                tablenewc.AddCell(cellspace);
                tablenewc.AddCell(cellspace);

                PdfPCell empty = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.BOLD | Font.UNDERLINE, BaseColor.BLACK)));
                empty.HorizontalAlignment = 0;
                empty.Colspan = 11;
                empty.Border = 0;
                tablenewc.AddCell(empty);



                PdfPCell footerdate = new PdfPCell(new Phrase("Date :", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
                footerdate.HorizontalAlignment = 0;
                footerdate.Colspan = 2;
                footerdate.BorderWidthTop = .5f;
                footerdate.BorderWidthRight = 0;
                footerdate.BorderWidthLeft = 0;
                footerdate.BorderWidthBottom = 0;
                tablenewc.AddCell(footerdate);

                PdfPCell stamp = new PdfPCell(new Phrase("Stamp of the Factory / Est.", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
                stamp.HorizontalAlignment = 0;
                stamp.Colspan = 2;
                stamp.BorderWidthTop = .5f;
                stamp.BorderWidthRight = 0;
                stamp.BorderWidthLeft = 0;
                stamp.BorderWidthBottom = 0;
                tablenewc.AddCell(stamp);

                PdfPCell sig = new PdfPCell(new Phrase("Signature of the employer or other authorised officer of the Factory / Establishment", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
                sig.HorizontalAlignment = 2;
                sig.Colspan = 7;
                sig.BorderWidthTop = .5f;
                sig.BorderWidthRight = 0;
                sig.BorderWidthLeft = 0;
                sig.BorderWidthBottom = 0;
                tablenewc.AddCell(sig);



                document.Add(tablenewc);

                #endregion Basic Information of the Employee




                string filename = "Form5.pdf";

                document.Close();
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment;filename=" + filename);
                Response.Buffer = true;
                Response.Clear();
                Response.OutputStream.Write(ms.GetBuffer(), 0, ms.GetBuffer().Length);
                Response.OutputStream.Flush();
                Response.End();
            }

            else
            {

            }
        }

        protected void btnform10_Click(object sender, EventArgs e)
        {


            int Fontsize = 10;
            string fontsyle = "verdana";

            #region Variable Declaration

            string contactno = "";
            string Idno = "";
            string date = "";
            string postappliedfor = "";
            string name = "";
            string fathername = "";
            string dateofbirth = "";
            string educationqualification = "";
            string TechnicalQualification = "";
            string previouspost = "";
            string nationality = "";
            string community = "";
            string maritalstatus = "";
            string height = "";
            string weight = "";
            string chest = "";
            string bloodgroup = "";
            string identificationmark1 = "";
            string identificationmark2 = "";

            string prdoorno = "";
            string prstreet = "";
            string prarea = "";
            string prcity = "";
            string prLmark = "";
            string prDistrict = "";
            string prPincode = "";
            string prState = "";


            string pedoor = "";
            string pestreet = "";
            string pearea = "";
            string pecity = "";
            string pelmark = "";
            string peDistrict = "";
            string pePincode = "";
            string peState = "";

            string refaddress1 = "";
            string refaddress2 = "";

            string sscschool = "";
            string sscbduniversity = "";
            string sscstdyear = "";

            string imschool = "";
            string imbduniversity = "";
            string imstdyear = "";

            string dgschool = "";
            string dgbduniversity = "";
            string dgstdyear = "";

            string pgschool = "";
            string pgbduniversity = "";
            string pgstdyear = "";
            string EmpCertfDet1 = "";

            float EmpsecurityDeposit = 0;
            string Referedby = "";
            string clientname = "";


            string relationName = "";
            string relationAge = "";
            string relationType = "";


            string EmpCertfDet2 = "";
            string EmpCertfDet3 = "";
            string EmpCertfDet4 = "";

            string Original1 = "";
            string Original2 = "";
            string Original3 = "";
            string Original4 = "";

            string Xerox1 = "";
            string Xerox2 = "";
            string Xerox3 = "";
            string Xerox4 = "";

            string Ref1Phone1 = "";
            string Ref1Phone2 = "";
            string Ref2Phone1 = "";
            string Ref2Phone2 = "";

            string ReplacementFor = "";
            string PlaceofBirth = "";
            string Haircolour = "";
            string eyecolour = "";
            string Complexion = "";
            string Languagesknown = "";
            string EmergencyPhone = "";
            string Fname = "";
            string Fage = "";
            string Mname = "";
            string Mage = "";
            string relationoccupation = "";
            string relationresidence = "";
            string relationplace = "";
            string Esino = "";
            string prphone = "";
            string pephone = "";

            #endregion

            string Gender = "";
            string Companyname = "";
            string CompanyAddress = "";
            string remarks = "";
            string accno = "";
            string Empdateofbirth = "";
            string pfNo = "";
            DateTime tdate;
            string ToDate = "";
            string Tomonth = "";
            string ToYear = "";
            string cmpnypfno = "";

            if (txtEmpDtofJoining.Text.Trim().Length > 0)
            {
                tdate = DateTime.ParseExact(txtEmpDtofJoining.Text.Trim(), "MMM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
                Tomonth = tdate.ToString("MM");
                ToYear = tdate.ToString("yyyy");

            }

            ToDate = ToYear + Tomonth;

            string query = "Select epf.empepfno,(ed.empfname+''+ed.empmname+''+ed.emplname) as empname,ed.empfathername,case ed.empsex when 'M' then 'Male' when 'F' then 'Female' else 'Transgender' end Gender,case convert(varchar(10),ed.empdtofLeaving,103) when '01/01/1900' then '' else convert(varchar(10),ed.empdtofLeaving,103) end empdtofLeaving,case convert(varchar(10),ed.empdtofbirth,103) when '01/01/1900' then '' else convert(varchar(10),ed.empdtofbirth,103) end empdtofbirth,ed.empremarks from empdetails ed left join empepfcodes epf on epf.empid=ed.empid  WHERE MONTH(empdtofLeaving)='" + Tomonth + "' AND YEAR(empdtofLeaving)='" + ToYear + "' and epf.empepfno<>'' and len(epf.empepfno)>0 ";
            DataTable dtEmpdetails = config.ExecuteAdaptorAsyncWithQueryParams(query).Result;



            if (dtEmpdetails.Rows.Count > 0)
            {


                string qry = "select * from CompanyInfo ";
                DataTable dttblroptns = config.ExecuteAdaptorAsyncWithQueryParams(qry).Result;
                if (dttblroptns.Rows.Count > 0)
                {
                    Companyname = dttblroptns.Rows[0]["CompanyName"].ToString();
                    CompanyAddress = dttblroptns.Rows[0]["Address"].ToString();
                    cmpnypfno = dttblroptns.Rows[0]["PFNo"].ToString();
                }





                MemoryStream ms = new MemoryStream();

                Document document = new Document(PageSize.LEGAL.Rotate());

                // var output = new FileStream(fileheader2, FileMode., FileAccess.Write, FileShare.None);
                var writer = PdfWriter.GetInstance(document, ms);
                document.Open();
                BaseFont bf = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                string imagepath1 = Server.MapPath("images");
                #region

                PdfPTable tablenewc = new PdfPTable(7);
                tablenewc.TotalWidth = 900f;
                tablenewc.LockedWidth = true;
                float[] width = new float[] { 0.6f, 2f, 4f, 4f, 2f, 2f, 2f };
                tablenewc.SetWidths(width);

                PdfPCell cellspace = new PdfPCell(new Phrase("  ", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize - 2, Font.BOLD, BaseColor.BLACK)));
                cellspace.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                cellspace.Colspan = 7;
                cellspace.Border = 0;
                cellspace.PaddingTop = 0;

                PdfPCell cellHead = new PdfPCell(new Phrase("Form-10 ", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize + 2, Font.BOLD, BaseColor.BLACK)));
                cellHead.HorizontalAlignment = 1;
                cellHead.Colspan = 7;
                cellHead.Border = 0;
                tablenewc.AddCell(cellHead);

                PdfPCell provident = new PdfPCell(new Phrase("Employees' Provident Fund Organisation ", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize + 2, Font.BOLD, BaseColor.BLACK)));
                provident.HorizontalAlignment = 1;
                provident.Colspan = 7;
                provident.Border = 0;
                tablenewc.AddCell(provident);

                PdfPCell cellemp = new PdfPCell(new Phrase("THE EMPLOYEES' PROVIDENT FUND SCHEME,1952[Para 36 (2) (a) & (b) ] THE EMPLOYEES' PENSION SCHEME,1995 [Para 20 (2) ] ", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize + 2, Font.BOLD, BaseColor.BLACK)));
                cellemp.HorizontalAlignment = 1;
                cellemp.Colspan = 7;
                cellemp.Border = 0;
                tablenewc.AddCell(cellemp);


                PdfPCell cellmonth = new PdfPCell(new Phrase("Return of members leaving service during the month   :", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize + 2, Font.NORMAL, BaseColor.BLACK)));
                cellmonth.HorizontalAlignment = 0;
                cellmonth.Colspan = 7;
                cellmonth.Border = 0;
                tablenewc.AddCell(cellmonth);

                PdfPCell nameaddress = new PdfPCell(new Phrase("Name and address of the Factory / Establishment.:" + Companyname + ' ' + CompanyAddress, FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize + 2, Font.NORMAL, BaseColor.BLACK)));
                nameaddress.HorizontalAlignment = 0;
                nameaddress.Colspan = 7;
                nameaddress.Border = 0;
                tablenewc.AddCell(nameaddress);


                PdfPCell cellcode = new PdfPCell(new Phrase("Code No.:" + cmpnypfno, FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize + 2, Font.NORMAL, BaseColor.BLACK)));
                cellcode.HorizontalAlignment = 0;
                cellcode.Colspan = 7;
                cellcode.Border = 0;
                tablenewc.AddCell(cellcode);

                tablenewc.AddCell(cellspace);


                PdfPCell celltable10 = new PdfPCell(new Phrase("SL.No. ", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.BOLD, BaseColor.BLACK)));
                celltable10.HorizontalAlignment = 1;
                celltable10.Colspan = 0;
                tablenewc.AddCell(celltable10);

                PdfPCell Account = new PdfPCell(new Phrase("Account No.", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.BOLD, BaseColor.BLACK)));
                Account.HorizontalAlignment = 1;
                Account.Colspan = 0;
                tablenewc.AddCell(Account);

                PdfPCell Name = new PdfPCell(new Phrase("Name of the Member (In the  block letters) ", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.BOLD, BaseColor.BLACK)));
                Name.HorizontalAlignment = 1;
                Name.Colspan = 0;
                tablenewc.AddCell(Name);


                PdfPCell Father = new PdfPCell(new Phrase("Father Name (or husband's name in case of married women)", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.BOLD, BaseColor.BLACK)));
                Father.HorizontalAlignment = 1;
                Father.Colspan = 0;
                tablenewc.AddCell(Father);


                PdfPCell Date = new PdfPCell(new Phrase("Date of Leaving Service", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.BOLD, BaseColor.BLACK)));
                Date.HorizontalAlignment = 1;
                Date.Colspan = 0;
                tablenewc.AddCell(Date);


                PdfPCell reson = new PdfPCell(new Phrase("Reason for leaving Service*", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.BOLD, BaseColor.BLACK)));
                reson.HorizontalAlignment = 1;
                reson.Colspan = 0;
                tablenewc.AddCell(reson);

                PdfPCell Remarks = new PdfPCell(new Phrase("Remarks", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.BOLD, BaseColor.BLACK)));
                Remarks.HorizontalAlignment = 1;
                Remarks.Colspan = 0;
                tablenewc.AddCell(Remarks);

                PdfPCell cell1 = new PdfPCell(new Phrase("1 ", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
                cell1.HorizontalAlignment = 1;
                cell1.Colspan = 0;
                tablenewc.AddCell(cell1);

                PdfPCell cell2 = new PdfPCell(new Phrase("2", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
                cell2.HorizontalAlignment = 1;
                cell2.Colspan = 0;
                tablenewc.AddCell(cell2);

                PdfPCell cell3 = new PdfPCell(new Phrase("3 ", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
                cell3.HorizontalAlignment = 1;
                cell3.Colspan = 0;
                tablenewc.AddCell(cell3);

                PdfPCell cell4 = new PdfPCell(new Phrase("4", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
                cell4.HorizontalAlignment = 1;
                cell4.Colspan = 0;
                tablenewc.AddCell(cell4);

                PdfPCell cell5 = new PdfPCell(new Phrase("5 ", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
                cell5.HorizontalAlignment = 1;
                cell5.Colspan = 0;
                tablenewc.AddCell(cell5);

                PdfPCell cell6 = new PdfPCell(new Phrase("6", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
                cell6.HorizontalAlignment = 1;
                cell6.Colspan = 0;
                tablenewc.AddCell(cell6);

                PdfPCell cell7 = new PdfPCell(new Phrase("7", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
                cell7.HorizontalAlignment = 1;
                cell7.Colspan = 0;
                tablenewc.AddCell(cell7);

                int l = 1;
                for (int k = 0; k < dtEmpdetails.Rows.Count; k++)
                {

                    name = dtEmpdetails.Rows[k]["empname"].ToString();
                    fathername = dtEmpdetails.Rows[k]["empfathername"].ToString();
                    date = dtEmpdetails.Rows[k]["empdtofLeaving"].ToString();
                    Empdateofbirth = dtEmpdetails.Rows[k]["empdtofbirth"].ToString();
                    pfNo = dtEmpdetails.Rows[k]["empepfno"].ToString();
                    Gender = dtEmpdetails.Rows[k]["gender"].ToString();
                    remarks = dtEmpdetails.Rows[k]["EmpRemarks"].ToString();


                    PdfPCell cellsno1 = new PdfPCell(new Phrase(l.ToString(), FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
                    cellsno1.HorizontalAlignment = 1;
                    cellsno1.Colspan = 0;
                    tablenewc.AddCell(cellsno1);

                    PdfPCell cellacc2 = new PdfPCell(new Phrase(pfNo, FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
                    cellacc2.HorizontalAlignment = 1;
                    cellacc2.Colspan = 0;
                    tablenewc.AddCell(cellacc2);

                    PdfPCell cellname3 = new PdfPCell(new Phrase(name, FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
                    cellname3.HorizontalAlignment = 0;
                    cellname3.Colspan = 0;
                    tablenewc.AddCell(cellname3);

                    PdfPCell cellfather4 = new PdfPCell(new Phrase(fathername, FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
                    cellfather4.HorizontalAlignment = 0;
                    cellfather4.Colspan = 0;
                    tablenewc.AddCell(cellfather4);

                    PdfPCell cellbirh5 = new PdfPCell(new Phrase(date, FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
                    cellbirh5.HorizontalAlignment = 1;
                    cellbirh5.Colspan = 0;
                    tablenewc.AddCell(cellbirh5);

                    PdfPCell cellsex6 = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
                    cellsex6.HorizontalAlignment = 1;
                    cellsex6.Colspan = 0;
                    tablenewc.AddCell(cellsex6);

                    PdfPCell celldatejoin7 = new PdfPCell(new Phrase(remarks, FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize, Font.NORMAL, BaseColor.BLACK)));
                    celldatejoin7.HorizontalAlignment = 1;
                    celldatejoin7.Colspan = 0;
                    celldatejoin7.FixedHeight = 25f;
                    tablenewc.AddCell(celldatejoin7);

                    l++;

                }

                tablenewc.AddCell(cellspace);
                tablenewc.AddCell(cellspace);
                tablenewc.AddCell(cellspace);




                PdfPCell footerdate = new PdfPCell(new Phrase("Date :", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize + 1, Font.NORMAL, BaseColor.BLACK)));
                footerdate.HorizontalAlignment = 0;
                footerdate.Colspan = 2;
                footerdate.Border = 0;
                tablenewc.AddCell(footerdate);

                PdfPCell stamp = new PdfPCell(new Phrase("Signature of the employer or other authorised officer of the Factory / Establishment", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize + 1, Font.NORMAL, BaseColor.BLACK)));
                stamp.HorizontalAlignment = 2;
                stamp.Colspan = 5;
                stamp.Border = 0;
                tablenewc.AddCell(stamp);

                tablenewc.AddCell(cellspace);
                tablenewc.AddCell(cellspace);

                PdfPCell declaration = new PdfPCell(new Phrase("* Please state whether the member is (a) retiring according to para (69) (1) (a) or (b) of the scheme (b) of the scheme (b) leaving india for permanent settlement abroad (c) retrenchment (d) Pt. & total disablement due to employment injury (e) discharged (f) resigning from or leaving service (g) taking up employement elsewhere (The name & address of the Employees should be stated (h) death (i) attained the age of 58 years.", FontFactory.GetFont(FontFactory.TIMES_ROMAN, Fontsize + 1, Font.NORMAL, BaseColor.BLACK)));
                declaration.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                declaration.Colspan = 7;
                declaration.Border = 0;
                tablenewc.AddCell(declaration);



                document.Add(tablenewc);

                #endregion Basic Information of the Employee

                document.NewPage();

                PdfPTable tblFingerprints = new PdfPTable(6);
                tblFingerprints.TotalWidth = 500f;
                tblFingerprints.LockedWidth = true;
                float[] widthfinger = new float[] { 2f, 1.5f, 2f, 2f, 1.5f, 2f };
                tblFingerprints.SetWidths(widthfinger);


                string filename = "FormX.pdf";

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
}