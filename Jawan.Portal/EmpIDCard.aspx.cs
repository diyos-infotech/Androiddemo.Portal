using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using KLTS.Data;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Collections.Generic;
using System.Text;
using Jawan.Portal.DAL;


namespace Jawan.Portal
{
    public partial class EmpIDCard : System.Web.UI.Page
    {
        DataTable dt;
        string EmpIDPrefix = "";
        string fontsyle = "verdana";
        string CmpIDPrefix = "";

        AppConfiguration config = new AppConfiguration();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                GetWebConfigdata();
                if (!IsPostBack)
                {
                    if (Session["UserId"] != null && Session["AccessLevel"] != null)
                    {
                        //PreviligeUsers(Convert.ToInt32(Session["AccessLevel"]));
                        string PID = Session["AccessLevel"].ToString();
                        // DisplayLinks(PID);
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
                                //EmployeeReportLink.Visible = true;
                                //ClientsReportLink.Visible = true;
                                InventoryReportsLink.Visible = false;
                                ClientsLink.Visible = false;
                                CompanyInfoLink.Visible = false;
                                InventoryLink.Visible = false;
                                ReportsLink.Visible = true;
                                SettingsLink.Visible = true;
                                ExpensesReportsLink.Visible = false;
                                break;


                            default:
                                break;
                        }

                        BindData();




                    }
                    else
                    {
                        Response.Redirect("login.aspx");
                    }


                }
            }
            catch (Exception ex)
            {
                GoToLoginPage();
            }


        }

        protected void BindData()
        {

            string Qry = "select empid,(empid+' - '+empfname+' '+empmname+' '+emplname) as empname from empdetails";
            DataTable dt = config.ExecuteAdaptorAsyncWithQueryParams(Qry).Result;

            if (dt.Rows.Count > 0)
            {
                lstEmpIdName.DataSource = dt;
                lstEmpIdName.DataTextField = "empname";
                lstEmpIdName.DataValueField = "empid";
                lstEmpIdName.DataBind();
            }
        }

        public void GoToLoginPage()
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Your Session Expired.Please Login');", true);
            Response.Redirect("~/login.aspx");
        }

        protected void GetWebConfigdata()
        {
            EmpIDPrefix = Session["EmpIDPrefix"].ToString();
            CmpIDPrefix = Session["CmpIDPrefix"].ToString();
        }

        protected void GvSearchEmp_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GvSearchEmp.PageIndex = e.NewPageIndex;

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            int Fontsize = 10;
            int Fontsize2 = 10;
            string fontstyle = "Calibri";

            List<String> EmpId_list = new List<string>();

            int totalfonts = FontFactory.RegisterDirectory("c:\\WINDOWS\\fonts");
            StringBuilder sa = new StringBuilder();
            foreach (string fontname in FontFactory.RegisteredFonts)
            {
                sa.Append(fontname + "\n");
            }


            Font FontStyle1 = FontFactory.GetFont("Perpetua Titling MT", BaseFont.CP1252, BaseFont.EMBEDDED, Fontsize - 3, Font.BOLD, BaseColor.BLACK);


            var list = new List<string>();

            for (int i = 0; i < lstEmpIdName.Items.Count; i++)
            {


                if (lstEmpIdName.Items[i].Selected == true)
                {
                    list.Add("'" + lstEmpIdName.Items[i].Value + "'");
                }
            }


            string empids = string.Join(",", list.ToArray());



            #region for Variable Declaration

            string Title = "";
            string Empid = "";
            string Name = "";
            string Designation = "";
            string IDcardIssued = "";
            string IDcardvalid = "";
            string BloodGroup = "";
            string prTown = "";
            string prPostOffice = "";
            string prTaluka = "";
            string statessndcity = "";
            string prPoliceStation = "";
            string prcity = "";
            string prphone = "";
            string prlmark = "";
            string prLmark = "";
            string prPincode = "";
            string prState = "";
            string State = "";
            string address1 = "";
            string Image = "";
            string EmpSign = "";
            string empdob = "";
            string empdoj = "";

            #endregion for Variable Declaration


            string QueryCompanyInfo = "select * from companyinfo";
            DataTable DtCompanyInfo = config.ExecuteAdaptorAsyncWithQueryParams(QueryCompanyInfo).Result;

            string CompanyName = "";
            string Address = "";
            string address = "";
            string companyinfo = "";
            string EmpDtofLeaving = "";
            string IDCardValid = "";
            string peTaluka = "";
            string peTown = "";
            string peLmark = "";
            string pearea = "";
            string pecity = "";
            string peDistrict = "";
            string pePincode = "";
            string addres1 = "";
            string peState = "";
            string pelmark = "";
            string branch = "";
            string pestreet = "";
            string pePostOffice = "";
            string pephone = "";
            string pePoliceStation = "";
            string Emailid = "";
            string Website = "";
            string comphone = "";
            string empsex = "";
            if (DtCompanyInfo.Rows.Count > 0)
            {
                CompanyName = DtCompanyInfo.Rows[0]["CompanyName"].ToString();
                Address = DtCompanyInfo.Rows[0]["Address"].ToString();
                companyinfo = DtCompanyInfo.Rows[0]["CompanyInfo"].ToString();
                Emailid = DtCompanyInfo.Rows[0]["Emailid"].ToString();
                Website = DtCompanyInfo.Rows[0]["Website"].ToString();
                comphone = DtCompanyInfo.Rows[0]["Phoneno"].ToString();

            }

            string query = "";
            DataTable dt = new DataTable();




            query = "select Empid,(EmpFName+' '+EmpMName+''+EmpLName) as Fullname,D.Design as EmpDesgn,empsex,prPostOffice,prPincode,(States.State+Cities.City) as statessndcity,(prTaluka+prPostOffice) as address1,EmpDetails.prLmark,prphone,prState,prcity,EmpDetails.prTaluka,EmpDetails.prTown,States.State,Cities.City,EmpDetails.prPincode,EmpPermanentAddress,(EmpDetails.prcity+EmpDetails.prLmark+EmpDetails.prTaluka+EmpDetails.prTown+States.State+Cities.City+EmpDetails.prPincode+EmpDetails.EmpPresentAddress) as address ," +
                "case convert(varchar(10),EmpDtofBirth,103) when '01/01/1900' then '' else convert(varchar(10),EmpDtofBirth,103) end EmpDtofBirth ," +
                "case convert(varchar(10),EmpDtofJoining,103) when '01/01/1900' then '' else convert(varchar(10),EmpDtofJoining,103) end EmpDtofJoining ," +
                "case convert(varchar(10),EmpDtofLeaving,103) when '01/01/1900' then '' else convert(varchar(10),EmpDtofLeaving,103) end EmpDtofLeaving ," +
                "case convert(varchar(10),IDCardIssued,103) when '01/01/1900' then '' else convert(varchar(10),IDCardIssued,103) end IDCardIssued ," +
                "case convert(varchar(10),IDCardValid,103) when '01/01/1900' then '' else convert(varchar(10),IDCardValid,103) end IDCardValid ," +
                "Image,EmpSign,BN.BloodGroupName as EmpBloodGroup from EmpDetails " +
                         " inner join designations D on D.Designid=EmpDetails.EmpDesgn " +
                         " left join BloodGroupNames BN on BN.BloodGroupId=EmpDetails.EmpBloodGroup left join Cities on  Cities.CityID= EmpDetails.prCity       LEFT JOIN States on States.StateID=EmpDetails.prState      " +
                         "left join branch b on b.branchid=empdetails.branch" +
                         " where empid  in (" + empids + ")  order by empid";
            dt = config.ExecuteAdaptorAsyncWithQueryParams(query).Result;

            if (dt.Rows.Count > 0)
            {
                MemoryStream ms = new MemoryStream();

                BaseFont bf = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                string imagepath1 = Server.MapPath("assets/EmpPhotos/");
                string imagepath2 = Server.MapPath("assets/Images/sign.jpg");
                string imagepath5 = Server.MapPath("assets/" + CmpIDPrefix + "IDlogo.png");
                string imagepath6 = Server.MapPath("assets/EmpPhotos/");

                Document document = new Document(PageSize.A4);
                var writer = PdfWriter.GetInstance(document, ms);
                document.Open();

                #region for range ID Card Display


                for (int k = 0; k < dt.Rows.Count; k++)
                {
                    prlmark = "";
                    prTaluka = "";
                    prTown = "";
                    prphone = "";
                    prcity = "";
                    prPincode = "";
                    peState = "";
                    prPostOffice = "";

                    Empid = dt.Rows[k]["Empid"].ToString();
                    Name = dt.Rows[k]["Fullname"].ToString();
                    empsex = dt.Rows[k]["empsex"].ToString();
                    if (empsex == "M")
                    {
                        Title = "Mr";
                    }
                    else
                    {
                        Title = "Ms";
                    }

                    Designation = dt.Rows[k]["EmpDesgn"].ToString();
                    IDcardIssued = dt.Rows[k]["IDCardIssued"].ToString();
                    IDcardvalid = dt.Rows[k]["IDCardValid"].ToString();
                    BloodGroup = dt.Rows[k]["EmpBloodGroup"].ToString();
                    Image = dt.Rows[k]["Image"].ToString();
                    EmpSign = dt.Rows[k]["EmpSign"].ToString();
                    empdob = dt.Rows[k]["EmpDtofBirth"].ToString();
                    empdoj = dt.Rows[k]["EmpDtofJoining"].ToString();
                    address = dt.Rows[k]["address"].ToString();
                    prlmark = dt.Rows[k]["prLmark"].ToString();
                    prTaluka = dt.Rows[k]["prTaluka"].ToString();
                    prTown = dt.Rows[k]["prTown"].ToString();
                    prphone = dt.Rows[k]["prphone"].ToString();
                    prcity = dt.Rows[k]["City"].ToString();
                    prPincode = dt.Rows[k]["prPincode"].ToString();
                    peState = dt.Rows[k]["State"].ToString();
                    prPostOffice = dt.Rows[k]["prPostOffice"].ToString();
                    Emailid = DtCompanyInfo.Rows[0]["Emailid"].ToString();
                    Website = DtCompanyInfo.Rows[0]["Website"].ToString();
                    address1 = dt.Rows[k]["address1"].ToString();
                    State = dt.Rows[k]["State"].ToString();
                    prPincode = dt.Rows[k]["prPincode"].ToString();
                    EmpDtofLeaving = dt.Rows[k]["EmpDtofLeaving"].ToString();



                    PdfPTable IDCarddetails = new PdfPTable(10);
                    IDCarddetails.TotalWidth = 380f;
                    IDCarddetails.LockedWidth = true;
                    float[] width = new float[] { 5f, 2f, 2f, 2f, 0.2f, 5f, 2f, 2f, 2f, 2.4f };
                    IDCarddetails.SetWidths(width);

                    #region for sub table

                    PdfPTable IDCardTemptable1 = new PdfPTable(4);
                    IDCardTemptable1.TotalWidth = 180f;
                    IDCardTemptable1.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
                    IDCardTemptable1.DefaultCell.VerticalAlignment = Element.ALIGN_TOP;
                    //IDCardTemptable1.LockedWidth = true;
                    float[] width1 = new float[] { 2.4f, 2.4f, 2.4f, 2.4f };

                    //float[] width1 = new float[] { 5f, 2f, 2f, 2f };
                    IDCardTemptable1.SetWidths(width1);
                    #region For 1st


                    BaseColor color = new BaseColor(0, 0, 0);



                    if (File.Exists(imagepath5))
                    {
                        iTextSharp.text.Image srflogo = iTextSharp.text.Image.GetInstance(imagepath5);
                        srflogo.ScaleAbsolute(80f, 50f);
                        PdfPCell companylogo = new PdfPCell();
                        Paragraph cmplogo = new Paragraph();
                        cmplogo.Add(new Chunk(srflogo, 50f, 0f));
                        companylogo.AddElement(cmplogo);
                        companylogo.HorizontalAlignment = 0;
                        companylogo.Colspan = 4;
                        companylogo.PaddingLeft = -15;
                        companylogo.Border = 0;
                        companylogo.PaddingTop = 20;
                        IDCardTemptable1.AddCell(companylogo);
                    }
                    else
                    {
                        PdfPCell companylogo = new PdfPCell();
                        companylogo.HorizontalAlignment = 0;
                        companylogo.Colspan = 4;
                        companylogo.Border = 0;
                        companylogo.FixedHeight = 45;
                        IDCardTemptable1.AddCell(companylogo);
                    }
                    #region commented code for address

                    Font FontStyle2 = FontFactory.GetFont("Perpetua Titling MT", BaseFont.CP1252, BaseFont.EMBEDDED, Fontsize - 3, Font.BOLD, BaseColor.BLACK);

                    PdfPCell cellCertification = new PdfPCell(new Phrase(CompanyName, FontFactory.GetFont(fontstyle, Fontsize - 1, Font.BOLD, color)));
                    cellCertification.HorizontalAlignment = 1;
                    cellCertification.Border = 0;
                    cellCertification.Colspan = 4;
                    cellCertification.PaddingLeft = -5f;
                    IDCardTemptable1.AddCell(cellCertification);





                    #endregion commented code for address

                    if (Image.Length > 0)
                    {
                        iTextSharp.text.Image Empphoto = iTextSharp.text.Image.GetInstance(imagepath1 + Image);
                        //Empphoto.ScalePercent(25f);
                        Empphoto.ScaleAbsolute(70f, 80f);
                        PdfPCell EmpImage = new PdfPCell();
                        Paragraph Emplogo = new Paragraph();
                        Emplogo.Add(new Chunk(Empphoto, 50f, 0));
                        EmpImage.AddElement(Emplogo);
                        EmpImage.HorizontalAlignment = 1;
                        EmpImage.Colspan = 4;
                        EmpImage.Border = 0;
                        EmpImage.PaddingLeft = -15;
                        IDCardTemptable1.AddCell(EmpImage);
                    }
                    else
                    {
                        PdfPCell EmpImage = new PdfPCell();
                        EmpImage.HorizontalAlignment = 2;
                        EmpImage.Colspan = 4;
                        EmpImage.Border = 0;
                        EmpImage.FixedHeight = 68;
                        IDCardTemptable1.AddCell(EmpImage);

                    }

                    PdfPCell cellEmpNameval = new PdfPCell(new Phrase(Title + ". " + Name, FontFactory.GetFont(fontstyle, Fontsize2 + 2, Font.BOLD, color)));
                    cellEmpNameval.HorizontalAlignment = 1;
                    cellEmpNameval.Border = 0;
                    cellEmpNameval.Colspan = 4;
                    // cellEmpNameval.PaddingLeft = -25f;
                    IDCardTemptable1.AddCell(cellEmpNameval);

                    PdfPCell cellempcode = new PdfPCell(new Phrase("ID No	          ", FontFactory.GetFont(fontstyle, Fontsize2, Font.NORMAL, color)));
                    cellempcode.HorizontalAlignment = 0;
                    cellempcode.Border = 0;
                    cellempcode.Colspan = 2;
                    IDCardTemptable1.AddCell(cellempcode);

                    PdfPCell cellempcodeval = new PdfPCell(new Phrase(": " + Empid, FontFactory.GetFont(fontstyle, Fontsize2, Font.NORMAL, color)));
                    cellempcodeval.HorizontalAlignment = 0;
                    cellempcodeval.Border = 0;
                    cellempcodeval.Colspan = 2;
                    cellempcodeval.PaddingLeft = -20f;
                    IDCardTemptable1.AddCell(cellempcodeval);

                    PdfPCell celldesgn = new PdfPCell(new Phrase("Designation  ", FontFactory.GetFont(fontstyle, Fontsize2, Font.NORMAL, color)));
                    celldesgn.HorizontalAlignment = 0;
                    celldesgn.Border = 0;
                    celldesgn.Colspan = 2;
                    IDCardTemptable1.AddCell(celldesgn);

                    PdfPCell celldesgnval = new PdfPCell(new Phrase(": " + Designation, FontFactory.GetFont(fontstyle, Fontsize2, Font.NORMAL, color)));
                    celldesgnval.HorizontalAlignment = 0;
                    celldesgnval.Border = 0;
                    celldesgnval.Colspan = 2;
                    celldesgnval.PaddingLeft = -20f;
                    IDCardTemptable1.AddCell(celldesgnval);

                    PdfPCell dobv = new PdfPCell(new Phrase("DOB	               ", FontFactory.GetFont(fontstyle, Fontsize2, Font.NORMAL, color)));
                    dobv.HorizontalAlignment = 0;
                    dobv.Border = 0;
                    dobv.Colspan = 2;
                    IDCardTemptable1.AddCell(dobv);

                    PdfPCell dobval = new PdfPCell(new Phrase(": " + empdob, FontFactory.GetFont(fontstyle, Fontsize2, Font.NORMAL, color)));
                    dobval.HorizontalAlignment = 0;
                    dobval.Border = 0;
                    dobval.Colspan = 2;
                    dobval.PaddingLeft = -20f;
                    IDCardTemptable1.AddCell(dobval);

                    PdfPCell space = new PdfPCell(new Phrase("IDCard From	               ", FontFactory.GetFont(fontstyle, Fontsize2, Font.NORMAL, color)));
                    space.HorizontalAlignment = 0;
                    space.Border = 0;
                    space.Colspan = 2;
                    IDCardTemptable1.AddCell(space);

                    PdfPCell spaceval = new PdfPCell(new Phrase(": " + IDcardIssued, FontFactory.GetFont(fontstyle, Fontsize2, Font.NORMAL, color)));
                    spaceval.HorizontalAlignment = 0;
                    spaceval.Border = 0;
                    spaceval.Colspan = 2;
                    spaceval.PaddingLeft = -20f;
                    IDCardTemptable1.AddCell(spaceval);

                    PdfPCell cellBloodGroup = new PdfPCell(new Phrase("IDCard To	              ", FontFactory.GetFont(fontstyle, Fontsize2, Font.NORMAL, color)));
                    cellBloodGroup.HorizontalAlignment = 0;
                    cellBloodGroup.Border = 0;
                    cellBloodGroup.Colspan = 2;
                    IDCardTemptable1.AddCell(cellBloodGroup);

                    PdfPCell cellBloodGroupval = new PdfPCell(new Phrase(": " + IDcardvalid, FontFactory.GetFont(fontstyle, Fontsize2, Font.NORMAL, color)));
                    cellBloodGroupval.HorizontalAlignment = 0;
                    cellBloodGroupval.Border = 0;
                    cellBloodGroupval.Colspan = 2;
                    cellBloodGroupval.PaddingLeft = -20f;
                    IDCardTemptable1.AddCell(cellBloodGroupval);

                    //if (EmpSign.Length > 0)
                    //{
                    //    iTextSharp.text.Image Sign = iTextSharp.text.Image.GetInstance(imagepath6 + EmpSign);
                    //    Sign.ScalePercent(8f);
                    //    Sign.ScaleAbsolute(60f, 15f);
                    //    PdfPCell Signature = new PdfPCell();
                    //    Paragraph signlogo = new Paragraph();
                    //    signlogo.Add(new Chunk(Sign, 23f, -7f));
                    //    Signature.AddElement(signlogo);
                    //    Signature.HorizontalAlignment = 0;
                    //    Signature.Colspan = 2;
                    //    Signature.PaddingTop = 5;
                    //    Signature.PaddingLeft = -16f;
                    //    Signature.Border = 0;
                    //    IDCardTemptable1.AddCell(Signature);
                    //}
                    //else
                    //{

                    //    PdfPCell Signature = new PdfPCell();
                    //    Signature.HorizontalAlignment = 0;
                    //    Signature.Colspan = 2;
                    //    Signature.PaddingTop = 5;
                    //    Signature.Border = 0;
                    //    Signature.PaddingLeft = -10f;
                    //    Signature.FixedHeight = 20;
                    //    IDCardTemptable1.AddCell(Signature);

                    //}

                    iTextSharp.text.Image IssuingAuth = iTextSharp.text.Image.GetInstance(imagepath2);
                    // IssuingAuth.ScalePercent(50f);
                    IssuingAuth.ScaleAbsolute(40f, 20f);
                    PdfPCell Authority = new PdfPCell();
                    Paragraph Authoritylogo = new Paragraph();
                    Authoritylogo.Add(new Chunk(IssuingAuth, 45f, -4f));
                    Authority.AddElement(Authoritylogo);
                    //Authority.HorizontalAlignment = 1;
                    Authority.HorizontalAlignment = Element.ALIGN_CENTER;
                    Authority.Colspan = 4;
                    Authority.Border = 0;
                    Authority.PaddingLeft = 0;
                    //Authority.PaddingTop = -12;
                    IDCardTemptable1.AddCell(Authority);

                    // PdfPCell cellempsignature = new PdfPCell(new Phrase("Employee Signature ", FontFactory.GetFont(fontstyle, Fontsize -2, Font.NORMAL, color)));
                    // cellempsignature.HorizontalAlignment = 0;
                    // cellempsignature.Border = 0;
                    //// cellempsignature.PaddingTop = 5;
                    // //cellempsignature.PaddingLeft = -7f;
                    // cellempsignature.Colspan = 2;
                    // IDCardTemptable1.AddCell(cellempsignature);

                    PdfPCell cellAuthority = new PdfPCell(new Phrase("Authorised Sign", FontFactory.GetFont(fontstyle, Fontsize - 2, Font.NORMAL, color)));
                    // cellAuthority.HorizontalAlignment = 1;
                    cellAuthority.HorizontalAlignment = Element.ALIGN_CENTER;
                    cellAuthority.Border = 0;
                    cellAuthority.Colspan = 4;
                    cellAuthority.PaddingLeft = 0;
                    // cellAuthority.PaddingLeft = 70;
                    IDCardTemptable1.AddCell(cellAuthority);

                    #endregion
                    PdfPCell childTable1 = new PdfPCell(IDCardTemptable1);
                    childTable1.HorizontalAlignment = 0;
                    childTable1.Colspan = 4;
                    childTable1.PaddingLeft = 10;
                    IDCarddetails.AddCell(childTable1);
                    #endregion
                    PdfPTable IDCardTemptable41 = new PdfPTable(1);
                    IDCardTemptable41.TotalWidth = 1f;
                    IDCardTemptable41.LockedWidth = true;
                    float[] width41 = new float[] { 0.5f };
                    IDCardTemptable41.SetWidths(width41);
                    #region subtable2

                    PdfPCell cellempcell1 = new PdfPCell(new Phrase("", FontFactory.GetFont(fontstyle, Fontsize, Font.NORMAL, BaseColor.BLACK)));
                    cellempcell1.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                    cellempcell1.Border = 0;
                    cellempcell1.Colspan = 1;
                    IDCardTemptable41.AddCell(cellempcell1);
                    #endregion
                    PdfPCell childTable4 = new PdfPCell(IDCardTemptable41);
                    childTable4.HorizontalAlignment = 0;
                    childTable4.Colspan = 1;
                    childTable4.Border = 0;
                    IDCarddetails.AddCell(childTable4);

                    PdfPTable IDCardTemptable2 = new PdfPTable(4);
                    IDCardTemptable2.TotalWidth = 190f;
                    //IDCardTemptable2.LockedWidth = true;
                    IDCardTemptable2.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
                    IDCardTemptable2.DefaultCell.VerticalAlignment = Element.ALIGN_TOP;
                    float[] width2 = new float[] { 2.3f, 2.3f, 2.3f, 2.3f };
                    IDCardTemptable2.SetWidths(width2);

                    #region for subtable3


                    #region Present address String array

                    string[] PrAdress = new string[8];

                    if (prlmark.Length > 0)
                    {
                        PrAdress[0] = prlmark + ", ";
                    }
                    else
                    {
                        PrAdress[0] = "";
                    }
                    if (prTown.Length > 0)
                    {
                        PrAdress[1] = prTown + ", ";
                    }
                    else
                    {
                        PrAdress[1] = "";
                    }

                    if (prPostOffice.Length > 0)
                    {
                        PrAdress[2] = prPostOffice + ", ";
                    }
                    else
                    {
                        PrAdress[2] = "";
                    }
                    if (prTaluka.Length > 0)
                    {
                        PrAdress[3] = prTaluka + ", ";
                    }
                    else
                    {
                        PrAdress[3] = " ";
                    }
                    if (prPoliceStation.Length > 0)
                    {
                        PrAdress[4] = prPoliceStation + ", ";
                    }
                    else
                    {
                        PrAdress[4] = " ";
                    }
                    if (prcity.Length > 0)
                    {
                        PrAdress[5] = prcity + ", ";
                    }
                    else
                    {
                        PrAdress[5] = " ";
                    }

                    if (prState.Length > 0)
                    {
                        PrAdress[6] = prState + " ";
                    }
                    else
                    {
                        PrAdress[6] = ".";
                    }


                    if (prPincode.Length > 0)
                    {
                        PrAdress[7] = prPincode + ".";
                    }
                    else
                    {
                        PrAdress[7] = "";
                    }

                    string Address2 = string.Empty;

                    for (int i = 0; i < 8; i++)
                    {
                        address += PrAdress[i];
                    }


                    #endregion

                    PdfPCell Instructions = new PdfPCell(new Phrase("INSTRUCTIONS:", FontFactory.GetFont(fontstyle, Fontsize, Font.UNDERLINE | Font.BOLD, color)));
                    Instructions.HorizontalAlignment = 1;
                    Instructions.Border = 0;
                    Instructions.Colspan = 4;
                    Instructions.PaddingLeft = -20;
                    Instructions.PaddingTop = 20;
                    IDCardTemptable2.AddCell(Instructions);

                    PdfPCell Instructions1 = new PdfPCell(new Phrase("1) This card is a property of Blue i\n    Enterprises\n" +
                    "2) This card cannot be transferred\n" +
                    "3) Misuse of this card is prohibited\n" +
                    "4) If card found please contact the\n     Below address:", FontFactory.GetFont(fontstyle, Fontsize - 1, Font.NORMAL, color)));
                    Instructions1.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                    Instructions1.Border = 0;
                    Instructions1.Colspan = 4;
                    Instructions1.PaddingLeft = -15f;
                    Instructions1.SetLeading(0f, 1.2f);
                    IDCardTemptable2.AddCell(Instructions1);



                    PdfPCell cellbloodgrp = new PdfPCell(new Phrase("", FontFactory.GetFont(fontstyle, Fontsize, Font.BOLD, color)));
                    cellbloodgrp.HorizontalAlignment = 0;
                    cellbloodgrp.Border = 0;
                    cellbloodgrp.Colspan = 4;
                    cellbloodgrp.FixedHeight = 115f;
                    IDCardTemptable2.AddCell(cellbloodgrp);

                    PdfPCell cellcccomp = new PdfPCell(new Phrase(CompanyName, FontFactory.GetFont(fontstyle, Fontsize - 1, Font.BOLD, color)));
                    cellcccomp.HorizontalAlignment = 1;
                    cellcccomp.Border = 0;
                    cellcccomp.Colspan = 4;
                    cellcccomp.PaddingLeft = -15f;
                    IDCardTemptable2.AddCell(cellcccomp);

                    PdfPCell cellcccompadd = new PdfPCell(new Phrase(Address, FontFactory.GetFont(fontstyle, Fontsize - 3, Font.NORMAL, color)));
                    cellcccompadd.HorizontalAlignment = 1;
                    cellcccompadd.Border = 0;
                    cellcccompadd.Colspan = 4;
                    cellcccompadd.PaddingLeft = -21f;
                    IDCardTemptable2.AddCell(cellcccompadd);

                    //PdfPCell cellDtIssued = new PdfPCell(new Phrase(Emailid, FontFactory.GetFont(fontstyle, Fontsize - 1, Font.NORMAL, color)));
                    //cellDtIssued.HorizontalAlignment = 1;
                    //cellDtIssued.Border = 0;
                    //cellDtIssued.Colspan = 4;
                    ////cellDtIssued.PaddingTop = 5;
                    //cellDtIssued.PaddingLeft = -15f;
                    //IDCardTemptable2.AddCell(cellDtIssued);

                    PdfPCell cellDtIssuedval = new PdfPCell(new Phrase(Website, FontFactory.GetFont(fontstyle, Fontsize - 1, Font.NORMAL, color)));
                    cellDtIssuedval.HorizontalAlignment = 1;
                    cellDtIssuedval.Border = 0;
                    cellDtIssuedval.Colspan = 4;
                    //cellDtIssuedval.PaddingTop = 10;
                    cellDtIssuedval.PaddingLeft = -15f;
                    IDCardTemptable2.AddCell(cellDtIssuedval);


                    #endregion for sub table

                    PdfPCell childTable2 = new PdfPCell(IDCardTemptable2);
                    childTable2.HorizontalAlignment = 0;
                    childTable2.Colspan = 4;
                    childTable2.PaddingLeft = 20;
                    IDCarddetails.AddCell(childTable2);


                    PdfPTable IDCardTemptable31 = new PdfPTable(1);
                    IDCardTemptable31.TotalWidth = 2f;
                    IDCardTemptable31.LockedWidth = true;
                    float[] width31 = new float[] { 1f };
                    IDCardTemptable31.SetWidths(width31);

                    PdfPCell cellempcell = new PdfPCell(new Phrase("", FontFactory.GetFont(fontstyle, Fontsize, Font.NORMAL, BaseColor.BLACK)));
                    cellempcell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                    cellempcell.Border = 0;
                    cellempcell.Colspan = 1;
                    cellempcell.PaddingBottom = 80;
                    IDCardTemptable31.AddCell(cellempcell);

                    PdfPCell childTable3 = new PdfPCell(IDCardTemptable31);
                    childTable3.HorizontalAlignment = 0;
                    childTable3.Colspan = 1;
                    childTable3.Border = 0;
                    childTable3.PaddingBottom = 30;
                    IDCarddetails.AddCell(childTable3);

                    PdfPCell childTable6 = new PdfPCell();
                    childTable6.HorizontalAlignment = 0;
                    childTable6.Colspan = 10;
                    childTable6.Border = 0;
                    childTable3.PaddingBottom = 10;
                    //childTable6.PaddingBottom = 10;
                    IDCarddetails.AddCell(childTable6);


                    PdfPCell empcellnew1 = new PdfPCell();
                    empcellnew1.HorizontalAlignment = 0;
                    empcellnew1.Colspan = 10;
                    empcellnew1.Border = 0;
                    IDCarddetails.AddCell(empcellnew1);



                    #endregion for range ID Card Display



                    document.Add(IDCarddetails);


                }

                document.Close();

                //document.Add(MainIDCarddetails);
                //document.Close();

                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment;filename=IDCard.pdf");
                Response.Buffer = true;
                Response.Clear();
                Response.OutputStream.Write(ms.GetBuffer(), 0, ms.GetBuffer().Length);
                Response.OutputStream.Flush();
                Response.End();
            }

        }



        protected void btnIDCardNew_Click(object sender, EventArgs e)
        {
            int Fontsize = 10;
            int Fontsize2 = 10;
            string fontstyle = "TimesNewRoman";
            string fontstyle1 = "Arial";

            List<String> EmpId_list = new List<string>();

            int totalfonts = FontFactory.RegisterDirectory("c:\\WINDOWS\\fonts");
            StringBuilder sa = new StringBuilder();
            foreach (string fontname in FontFactory.RegisteredFonts)
            {
                sa.Append(fontname + "\n");
            }


            Font FontStyle1 = FontFactory.GetFont("Perpetua Titling MT", BaseFont.CP1252, BaseFont.EMBEDDED, Fontsize - 3, Font.BOLD, BaseColor.BLACK);


            var list = new List<string>();

            for (int i = 0; i < lstEmpIdName.Items.Count; i++)
            {


                if (lstEmpIdName.Items[i].Selected == true)
                {
                    list.Add("'" + lstEmpIdName.Items[i].Value + "'");
                }
            }


            string empids = string.Join(",", list.ToArray());



            #region for Variable Declaration

            string Title = "";
            string Empid = "";
            string Name = "";
            string Designation = "";
            string IDcardIssued = "";
            string IDcardvalid = "";
            string BloodGroup = "";
            string prTown = "";
            string prPostOffice = "";
            string prTaluka = "";
            string statessndcity = "";
            string prPoliceStation = "";
            string prcity = "";
            string prphone = "";
            string prlmark = "";
            string prLmark = "";
            string prPincode = "";
            string prState = "";
            string State = "";
            string address1 = "";
            string Image = "";
            string EmpSign = "";
            string empdob = "";
            string empdoj = "";
            string EmergencyContNo = "";

            #endregion for Variable Declaration


            string QueryCompanyInfo = "select * from companyinfo";
            DataTable DtCompanyInfo = config.ExecuteAdaptorAsyncWithQueryParams(QueryCompanyInfo).Result;

            string CompanyName = "";
            string Address = "";
            string address = "";
            string companyinfo = "";
            string EmpDtofLeaving = "";
            string peState = "";
            string Emailid = "";
            string Website = "";
            string comphone = "";
            string empsex = "";
            string EmpFatherName = "";
            string EmpPhone = "";
            if (DtCompanyInfo.Rows.Count > 0)
            {
                CompanyName = DtCompanyInfo.Rows[0]["CompanyName"].ToString();
                Address = DtCompanyInfo.Rows[0]["Address"].ToString();
                companyinfo = DtCompanyInfo.Rows[0]["CompanyInfo"].ToString();
                Emailid = DtCompanyInfo.Rows[0]["Emailid"].ToString();
                Website = DtCompanyInfo.Rows[0]["Website"].ToString();
                comphone = DtCompanyInfo.Rows[0]["Phoneno"].ToString();

            }

            string query = "";
            DataTable dt = new DataTable();




            query = "select Empid,(EmpFName) as Fullname,D.Design as EmpDesgn,empsex,EmpFatherName,isnull(EmpPhone,'') as EmpPhone,prPostOffice,prPincode,EmpDetails.prLmark,prphone,prState,prcity,EmpDetails.prTaluka,EmpDetails.prTown,States.State,EmpDetails.prPincode,EmpPermanentAddress," +
                "case convert(varchar(10),EmpDtofBirth,103) when '01/01/1900' then '' else convert(varchar(10),EmpDtofBirth,103) end EmpDtofBirth ," +
                "case convert(varchar(10),EmpDtofJoining,103) when '01/01/1900' then '' else convert(varchar(10),EmpDtofJoining,103) end EmpDtofJoining ," +
                "case convert(varchar(10),EmpDtofLeaving,103) when '01/01/1900' then '' else convert(varchar(10),EmpDtofLeaving,103) end EmpDtofLeaving ," +
                "case convert(varchar(10),IDCardIssued,103) when '01/01/1900' then '' else convert(varchar(10),IDCardIssued,103) end IDCardIssued ," +
                "case convert(varchar(10),IDCardValid,103) when '01/01/1900' then '' else convert(varchar(10),IDCardValid,103) end IDCardValid ," +
                "Image,EmpSign,BN.BloodGroupName as EmpBloodGroup,isnull(EmergencyContNo,'') as EmergencyContNo from EmpDetails " +
                         " inner join designations D on D.Designid=EmpDetails.EmpDesgn " +
                         " left join BloodGroupNames BN on BN.BloodGroupId=EmpDetails.EmpBloodGroup  LEFT JOIN States on States.StateID=EmpDetails.prState      " +
                         "left join branch b on b.branchid=empdetails.branch" +
                         " where empid  in (" + empids + ")  order by empid";
            dt = config.ExecuteAdaptorAsyncWithQueryParams(query).Result;


            if (dt.Rows.Count > 0)
            {
                MemoryStream ms = new MemoryStream();

                BaseFont bf = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                string imagepath1 = Server.MapPath("assets/EmpPhotos/");
                string imagepath2 = Server.MapPath("assets/Images/sign.jpg");
                string imagepath5 = Server.MapPath("assets/" + CmpIDPrefix + "Billlogo.png");
                string imagepath6 = Server.MapPath("assets/Stamp.png");
                string imagepath7 = Server.MapPath("assets/NoPhoto.jpg");

                Document document = new Document(PageSize.A4.Rotate());
                var writer = PdfWriter.GetInstance(document, ms);
                document.Open();



                PdfPTable IDCarddetails = new PdfPTable(5);
                IDCarddetails.TotalWidth = 600f;//830f
                IDCarddetails.LockedWidth = true;
                float[] width = new float[] { 4f, 4f, 1f, 4f, 4f };
                IDCarddetails.SetWidths(width);


                PdfPTable IDCarddetails2 = new PdfPTable(5);
                IDCarddetails2.TotalWidth = 600f;
                IDCarddetails2.LockedWidth = true;
                float[] widths = new float[] { 4f, 4f, 1f, 4f, 4f };
                IDCarddetails2.SetWidths(widths);

                BaseColor color = new BaseColor(0, 0, 0);

                #region for first part

                for (int k = 0; k < dt.Rows.Count; k++)
                {

                    if (k % 2 == 0)
                    {
                        PdfPCell empcellnew11 = new PdfPCell();
                        empcellnew11.HorizontalAlignment = 0;
                        empcellnew11.Colspan = 5;
                        empcellnew11.FixedHeight = 7;
                        empcellnew11.Border = 0;
                        empcellnew11.Rotation = 90;
                        IDCarddetails.AddCell(empcellnew11);
                    }


                    prlmark = "";
                    prTaluka = "";
                    prTown = "";
                    prphone = "";
                    prcity = "";
                    prPincode = "";
                    peState = "";
                    prPostOffice = "";

                    Empid = dt.Rows[k]["Empid"].ToString();
                    Name = dt.Rows[k]["Fullname"].ToString();
                    empsex = dt.Rows[k]["empsex"].ToString();
                    if (empsex == "M")
                    {
                        Title = "Mr";
                    }
                    else
                    {
                        Title = "Ms";
                    }

                    Designation = dt.Rows[k]["EmpDesgn"].ToString().Substring(0, 1) + dt.Rows[k]["EmpDesgn"].ToString().Substring(1).ToLower();
                    IDcardIssued = dt.Rows[k]["IDCardIssued"].ToString();
                    IDcardvalid = dt.Rows[k]["IDCardValid"].ToString();
                    BloodGroup = dt.Rows[k]["EmpBloodGroup"].ToString();
                    Image = dt.Rows[k]["Image"].ToString();
                    EmpSign = dt.Rows[k]["EmpSign"].ToString();
                    empdob = dt.Rows[k]["EmpDtofBirth"].ToString();
                    empdoj = dt.Rows[k]["EmpDtofJoining"].ToString();
                    prlmark = dt.Rows[k]["prLmark"].ToString();
                    prTaluka = dt.Rows[k]["prTaluka"].ToString();
                    prTown = dt.Rows[k]["prTown"].ToString();
                    prphone = dt.Rows[k]["prphone"].ToString();
                    prcity = dt.Rows[k]["prCity"].ToString();
                    prPincode = dt.Rows[k]["prPincode"].ToString();
                    peState = dt.Rows[k]["State"].ToString();
                    prPostOffice = dt.Rows[k]["prPostOffice"].ToString();
                    Emailid = DtCompanyInfo.Rows[0]["Emailid"].ToString();
                    Website = DtCompanyInfo.Rows[0]["Website"].ToString();
                    //address1 = dt.Rows[k]["address1"].ToString();
                    State = dt.Rows[k]["State"].ToString();
                    prPincode = dt.Rows[k]["prPincode"].ToString();
                    EmpDtofLeaving = dt.Rows[k]["EmpDtofLeaving"].ToString();
                    EmpFatherName = dt.Rows[k]["EmpFatherName"].ToString();
                    EmpPhone = dt.Rows[k]["EmpPhone"].ToString();

                    PdfPTable IDCardTemptable1 = new PdfPTable(5);
                    IDCardTemptable1.TotalWidth = 300f;
                    IDCardTemptable1.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
                    IDCardTemptable1.DefaultCell.VerticalAlignment = Element.ALIGN_TOP;
                    float[] width1 = new float[] { 2f, 2f, 2f, 2f, 2f };
                    IDCardTemptable1.SetWidths(width1);

                    if (File.Exists(imagepath5))
                    {
                        iTextSharp.text.Image srflogo = iTextSharp.text.Image.GetInstance(imagepath5);
                        srflogo.ScaleAbsolute(55f, 45f);//85f,70f
                        PdfPCell companylogo = new PdfPCell();
                        Paragraph cmplogo = new Paragraph();
                        cmplogo.Add(new Chunk(srflogo, -3f, 0f));//srflogo,50f,0f
                        companylogo.AddElement(cmplogo);
                        companylogo.HorizontalAlignment = 0;
                        companylogo.Colspan = 1;
                        companylogo.Border = 0;
                        companylogo.Rotation = 90;
                        IDCardTemptable1.AddCell(companylogo);

                    }
                    else
                    {
                        PdfPCell companylogo = new PdfPCell();
                        companylogo.HorizontalAlignment = 0;
                        companylogo.Colspan = 1;
                        companylogo.Border = 0;
                        companylogo.FixedHeight = 45;
                        companylogo.Rotation = 90;
                        IDCardTemptable1.AddCell(companylogo);
                    }

                    #region commented code for address

                    Font FontStyle2 = FontFactory.GetFont("Perpetua Titling MT", BaseFont.CP1252, BaseFont.EMBEDDED, Fontsize - 3, Font.BOLD, BaseColor.BLACK);


                    PdfPCell cellCompName = new PdfPCell(new Phrase(CompanyName, FontFactory.GetFont(fontstyle1, Fontsize, Font.BOLD, BaseColor.RED)));
                    cellCompName.HorizontalAlignment = 0;
                    cellCompName.Border = 0;
                    cellCompName.Colspan = 4;
                    cellCompName.PaddingLeft = 10f;
                    cellCompName.Rotation = 90;
                    IDCardTemptable1.AddCell(cellCompName);

                    PdfPCell cellCompAddress = new PdfPCell(new Phrase("", FontFactory.GetFont(fontstyle1, Fontsize, Font.BOLD, BaseColor.BLACK)));
                    cellCompAddress.HorizontalAlignment = 0;
                    cellCompAddress.Border = 0;
                    cellCompAddress.Colspan = 1;
                    cellCompAddress.PaddingTop = -70f;
                    cellCompAddress.Rotation = 90;
                    IDCardTemptable1.AddCell(cellCompAddress);

                    cellCompAddress = new PdfPCell(new Phrase(Address + "" + Emailid, FontFactory.GetFont(fontstyle1, Fontsize - 3, Font.NORMAL, BaseColor.BLACK)));
                    cellCompAddress.HorizontalAlignment = 0;
                    cellCompAddress.Border = 0;
                    cellCompAddress.Colspan = 4;
                    cellCompAddress.PaddingLeft = 10f;
                    cellCompAddress.PaddingTop = -32f;
                    cellCompAddress.Rotation = 90;
                    //    cellCompAddress.PaddingLeft = 2f;
                    IDCardTemptable1.AddCell(cellCompAddress);


                    #endregion commented code for address


                    PdfPTable childtable1 = new PdfPTable(1);
                    childtable1.TotalWidth = 100f;
                    childtable1.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
                    childtable1.DefaultCell.VerticalAlignment = Element.ALIGN_TOP;
                    float[] widthch1 = new float[] { 2f };
                    childtable1.SetWidths(widthch1);





                    if (Image.Length > 0)
                    {
                        iTextSharp.text.Image Empphoto = iTextSharp.text.Image.GetInstance(imagepath1 + Image);
                        //Empphoto.ScalePercent(25f);
                        Empphoto.ScaleAbsolute(50f, 60f);//50f, 60f
                        PdfPCell EmpImage = new PdfPCell();
                        Paragraph Emplogo = new Paragraph();
                        Emplogo.Add(new Chunk(Empphoto, 0f, 0));
                        EmpImage.AddElement(Emplogo);
                        EmpImage.HorizontalAlignment = 0;
                        EmpImage.Colspan = 1;
                        EmpImage.Border = 0;
                        EmpImage.Rotation = 90;
                        //EmpImage.PaddingTop = -10f;
                        //   EmpImage.PaddingLeft = -20;
                        childtable1.AddCell(EmpImage);
                    }
                    else
                    {
                        iTextSharp.text.Image Empphoto = iTextSharp.text.Image.GetInstance(imagepath7);
                        //Empphoto.ScalePercent(25f);
                        Empphoto.ScaleAbsolute(50f, 60f);
                        PdfPCell EmpImage = new PdfPCell();
                        Paragraph Emplogo = new Paragraph();
                        Emplogo.Add(new Chunk(Empphoto, 0f, 0f));//Empphoto,60f,0f
                        EmpImage.AddElement(Emplogo);
                        EmpImage.HorizontalAlignment = 0;
                        EmpImage.Colspan = 1;
                        EmpImage.Border = 0;
                        EmpImage.Rotation = 90;
                        // EmpImage.PaddingTop = -10f;
                        //   EmpImage.PaddingLeft = -20;
                        //  EmpImage.PaddingTop = 2;
                        childtable1.AddCell(EmpImage);

                    }



                    PdfPCell childTable11 = new PdfPCell(childtable1);
                    childTable11.HorizontalAlignment = 0;
                    childTable11.Colspan = 1;
                    childTable11.Border = 0;
                    //childTable11.PaddingLeft = 10;
                    IDCardTemptable1.AddCell(childTable11);



                    PdfPTable childtable2 = new PdfPTable(4);
                    childtable2.TotalWidth = 200f;
                    childtable2.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
                    childtable2.DefaultCell.VerticalAlignment = Element.ALIGN_TOP;
                    float[] widthch2 = new float[] { 2.2f, 1f, 2f, 2f };
                    childtable2.SetWidths(widthch2);


                    PdfPCell EmpData = new PdfPCell(new Phrase("Emp. Code", FontFactory.GetFont(fontstyle, Fontsize2 - 1, Font.BOLD, color)));
                    EmpData.HorizontalAlignment = 0;
                    EmpData.Colspan = 1;
                    EmpData.Border = 0;
                    // EmpData.PaddingTop = -5f;
                    // EmpData.SetLeading(0f, 1.4f);
                    childtable2.AddCell(EmpData);


                    EmpData = new PdfPCell(new Phrase(" : " + Empid, FontFactory.GetFont(fontstyle, Fontsize2 - 1, Font.BOLD, color)));
                    EmpData.HorizontalAlignment = 0;
                    EmpData.Colspan = 3;
                    EmpData.Border = 0;
                    // EmpData.PaddingTop = -5f;
                    // EmpData.SetLeading(0f, 1.4f);
                    childtable2.AddCell(EmpData);


                    EmpData = new PdfPCell(new Phrase("Name", FontFactory.GetFont(fontstyle, Fontsize2 - 1, Font.BOLD, color)));
                    EmpData.HorizontalAlignment = 0;
                    EmpData.Colspan = 1;
                    EmpData.Border = 0;
                    // EmpData.PaddingTop = -5f;
                    // EmpData.SetLeading(0f, 1.4f);
                    childtable2.AddCell(EmpData);


                    EmpData = new PdfPCell(new Phrase(" : " + Name, FontFactory.GetFont(fontstyle, Fontsize2 - 1, Font.BOLD, color)));
                    EmpData.HorizontalAlignment = 0;
                    EmpData.Colspan = 3;
                    EmpData.Border = 0;
                    // EmpData.PaddingTop = -5f;
                    // EmpData.SetLeading(0f, 1.4f);
                    childtable2.AddCell(EmpData);



                    EmpData = new PdfPCell(new Phrase("S/o.", FontFactory.GetFont(fontstyle, Fontsize2 - 1, Font.BOLD, color)));
                    EmpData.HorizontalAlignment = 0;
                    EmpData.Colspan = 1;
                    EmpData.Border = 0;
                    // EmpData.PaddingTop = -5f;
                    // EmpData.SetLeading(0f, 1.4f);
                    childtable2.AddCell(EmpData);


                    EmpData = new PdfPCell(new Phrase(" : " + EmpFatherName, FontFactory.GetFont(fontstyle, Fontsize2 - 1, Font.BOLD, color)));
                    EmpData.HorizontalAlignment = 0;
                    EmpData.Colspan = 3;
                    EmpData.Border = 0;
                    // EmpData.PaddingTop = -5f;
                    // EmpData.SetLeading(0f, 1.4f);
                    childtable2.AddCell(EmpData);


                    EmpData = new PdfPCell(new Phrase("Designation", FontFactory.GetFont(fontstyle, Fontsize2 - 1, Font.BOLD, color)));
                    EmpData.HorizontalAlignment = 0;
                    EmpData.Colspan = 1;
                    EmpData.Border = 0;
                    // EmpData.PaddingTop = -5f;
                    // EmpData.SetLeading(0f, 1.4f);
                    childtable2.AddCell(EmpData);


                    EmpData = new PdfPCell(new Phrase(" : " + Designation, FontFactory.GetFont(fontstyle, Fontsize2 - 1, Font.BOLD, color)));
                    EmpData.HorizontalAlignment = 0;
                    EmpData.Colspan = 3;
                    EmpData.Border = 0;
                    // EmpData.PaddingTop = -5f;
                    // EmpData.SetLeading(0f, 1.4f);
                    childtable2.AddCell(EmpData);


                    EmpData = new PdfPCell(new Phrase("Date of Birth", FontFactory.GetFont(fontstyle, Fontsize2 - 1, Font.BOLD, color)));
                    EmpData.HorizontalAlignment = 0;
                    EmpData.Colspan = 1;
                    EmpData.Border = 0;
                    // EmpData.PaddingTop = -5f;
                    // EmpData.SetLeading(0f, 1.4f);
                    childtable2.AddCell(EmpData);


                    EmpData = new PdfPCell(new Phrase(" : " + empdob, FontFactory.GetFont(fontstyle, Fontsize2 - 1, Font.BOLD, color)));
                    EmpData.HorizontalAlignment = 0;
                    EmpData.Colspan = 3;
                    EmpData.Border = 0;
                    // EmpData.PaddingTop = -5f;
                    // EmpData.SetLeading(0f, 1.4f);
                    childtable2.AddCell(EmpData);


                    PdfPCell childTable12 = new PdfPCell(childtable2);
                    childTable12.HorizontalAlignment = 0;
                    childTable12.Colspan = 4;
                    childTable12.Border = 0;
                    //childTable11.PaddingLeft = 10;
                    IDCardTemptable1.AddCell(childTable12);



                    PdfPCell cellAuthority = new PdfPCell(new Phrase("Issued By", FontFactory.GetFont(fontstyle, Fontsize - 2, Font.NORMAL, color)));
                    cellAuthority.HorizontalAlignment = 0;
                    cellAuthority.Border = 0;
                    cellAuthority.Colspan = 5;
                    cellAuthority.PaddingBottom = 16;
                    IDCardTemptable1.AddCell(cellAuthority);

                    cellAuthority = new PdfPCell(new Phrase("", FontFactory.GetFont(fontstyle, Fontsize - 2, Font.NORMAL, color)));
                    cellAuthority.HorizontalAlignment = 0;
                    cellAuthority.Border = 0;
                    cellAuthority.Colspan = 5;
                    //cellAuthority.MinimumHeight = 10f;
                    // cellAuthority.PaddingLeft = 25;
                    IDCardTemptable1.AddCell(cellAuthority);


                    PdfPCell childTable1 = new PdfPCell(IDCardTemptable1);
                    childTable1.HorizontalAlignment = 0;
                    childTable1.Colspan = 2;
                    childTable1.PaddingLeft = 10;
                    IDCarddetails.AddCell(childTable1);



                    #region subtable2

                    PdfPTable IDCardTemptable41 = new PdfPTable(1);
                    IDCardTemptable41.TotalWidth = 2f;
                    IDCardTemptable41.LockedWidth = true;
                    float[] width41 = new float[] { 1f };
                    IDCardTemptable41.SetWidths(width41);


                    PdfPCell cellempcell1 = new PdfPCell(new Phrase("", FontFactory.GetFont(fontstyle, Fontsize, Font.NORMAL, BaseColor.BLACK)));
                    cellempcell1.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                    cellempcell1.Border = 0;
                    cellempcell1.Colspan = 1;
                    IDCardTemptable41.AddCell(cellempcell1);

                    PdfPCell childTable4 = new PdfPCell(IDCardTemptable41);
                    childTable4.HorizontalAlignment = 0;
                    childTable4.Colspan = 1;
                    childTable4.Border = 0;
                    childTable4.PaddingTop = 30;
                    IDCarddetails.AddCell(childTable4);

                    #endregion


                }


                PdfPCell empcellnew = new PdfPCell();
                empcellnew.HorizontalAlignment = 0;
                empcellnew.Colspan = 5;
                empcellnew.Border = 0;
                IDCarddetails.AddCell(empcellnew);

                empcellnew = new PdfPCell();
                empcellnew.HorizontalAlignment = 0;
                empcellnew.Colspan = 5;
                empcellnew.Border = 0;
                IDCarddetails.AddCell(empcellnew);

                IDCarddetails.AddCell(empcellnew);
                IDCarddetails.AddCell(empcellnew);

                #endregion for first part


                document.Add(IDCarddetails);


                //document.NewPage();

                for (int k = 0; k < dt.Rows.Count; k++)
                {

                    PdfPTable IDCardTemptable2 = new PdfPTable(5);
                    IDCardTemptable2.HorizontalAlignment = Element.ALIGN_LEFT;
                    IDCardTemptable2.TotalWidth = 300f;
                    //IDCardTemptable2.LockedWidth = true;
                    float[] width2 = new float[] { 3.2f, 0.2f, 0.8f, 2f, 2f };
                    IDCardTemptable2.SetWidths(width2);

                    if (k % 2 == 0)
                    {

                        PdfPCell empcellnew11 = new PdfPCell();
                        empcellnew11.HorizontalAlignment = 0;
                        empcellnew11.Colspan = 5;
                        empcellnew11.FixedHeight = 7;
                        empcellnew11.Border = 0;
                        IDCarddetails2.AddCell(empcellnew11);
                    }




                    IDcardIssued = dt.Rows[k]["IDCardIssued"].ToString();
                    BloodGroup = dt.Rows[k]["EmpBloodGroup"].ToString();
                    prcity = dt.Rows[k]["prCity"].ToString();
                    EmpSign = dt.Rows[k]["EmpSign"].ToString();
                    empdob = dt.Rows[k]["EmpDtofBirth"].ToString();
                    //     address = dt.Rows[k]["address"].ToString();
                    EmpFatherName = dt.Rows[k]["EmpFatherName"].ToString();
                    EmpPhone = dt.Rows[k]["EmpPhone"].ToString();



                    prlmark = dt.Rows[k]["prLmark"].ToString();
                    prTaluka = dt.Rows[k]["prTaluka"].ToString();
                    prTown = dt.Rows[k]["prTown"].ToString();
                    prphone = dt.Rows[k]["prphone"].ToString();
                    prcity = dt.Rows[k]["prCity"].ToString();
                    prPincode = dt.Rows[k]["prPincode"].ToString();
                    peState = dt.Rows[k]["State"].ToString();
                    prPostOffice = dt.Rows[k]["prPostOffice"].ToString();
                    State = dt.Rows[k]["State"].ToString();
                    prPincode = dt.Rows[k]["prPincode"].ToString();
                    EmergencyContNo = dt.Rows[k]["EmergencyContNo"].ToString();



                    #region for subtable3

                    PdfPCell EmpDataPart2 = new PdfPCell(new Phrase("Issue Date ", FontFactory.GetFont(fontstyle, Fontsize - 1, Font.BOLD, color)));
                    EmpDataPart2.HorizontalAlignment = 0;
                    EmpDataPart2.Border = 0;
                    EmpDataPart2.Colspan = 1;
                    //  EmpDataPart2.PaddingLeft = -20;
                    EmpDataPart2.PaddingTop = 20;
                    IDCardTemptable2.AddCell(EmpDataPart2);

                    EmpDataPart2 = new PdfPCell(new Phrase(": ", FontFactory.GetFont(fontstyle, Fontsize - 1, Font.BOLD, color)));
                    EmpDataPart2.HorizontalAlignment = 0;
                    EmpDataPart2.Border = 0;
                    EmpDataPart2.Colspan = 1;
                    //  EmpDataPart2.PaddingLeft = -20;
                    EmpDataPart2.PaddingTop = 20;
                    IDCardTemptable2.AddCell(EmpDataPart2);

                    EmpDataPart2 = new PdfPCell(new Phrase(IDcardIssued, FontFactory.GetFont(fontstyle, Fontsize - 1, Font.BOLD, color)));
                    EmpDataPart2.HorizontalAlignment = 0;
                    EmpDataPart2.Border = 0;
                    EmpDataPart2.Colspan = 3;
                    //  EmpDataPart2.PaddingLeft = -20;
                    EmpDataPart2.PaddingTop = 20;
                    IDCardTemptable2.AddCell(EmpDataPart2);

                    EmpDataPart2 = new PdfPCell(new Phrase("Residence Address ", FontFactory.GetFont(fontstyle, Fontsize - 1, Font.BOLD, color)));
                    EmpDataPart2.HorizontalAlignment = 0;
                    EmpDataPart2.Border = 0;
                    EmpDataPart2.Colspan = 1;
                    //  EmpDataPart2.PaddingLeft = -20;
                    EmpDataPart2.PaddingTop = 5;
                    EmpDataPart2.PaddingBottom = 7;
                    IDCardTemptable2.AddCell(EmpDataPart2);

                    EmpDataPart2 = new PdfPCell(new Phrase(": ", FontFactory.GetFont(fontstyle, Fontsize - 1, Font.BOLD, color)));
                    EmpDataPart2.HorizontalAlignment = 0;
                    EmpDataPart2.Border = 0;
                    EmpDataPart2.Colspan = 1;
                    //  EmpDataPart2.PaddingLeft = -20;
                    EmpDataPart2.PaddingTop = 5;
                    EmpDataPart2.PaddingBottom = 7;
                    IDCardTemptable2.AddCell(EmpDataPart2);

                    EmpDataPart2 = new PdfPCell(new Phrase(prlmark, FontFactory.GetFont(fontstyle, Fontsize - 1, Font.BOLD, color)));
                    EmpDataPart2.HorizontalAlignment = 0;
                    EmpDataPart2.Border = 0;
                    EmpDataPart2.Colspan = 3;
                    //  EmpDataPart2.PaddingLeft = -20;
                    EmpDataPart2.PaddingTop = 5;
                    EmpDataPart2.PaddingBottom = 7;
                    IDCardTemptable2.AddCell(EmpDataPart2);

                    EmpDataPart2 = new PdfPCell(new Phrase("Emergency Contact No. ", FontFactory.GetFont(fontstyle, Fontsize - 1, Font.BOLD, color)));
                    EmpDataPart2.HorizontalAlignment = 0;
                    EmpDataPart2.Border = 0;
                    EmpDataPart2.Colspan = 1;
                    //  EmpDataPart2.PaddingLeft = -20;
                    // EmpDataPart2.PaddingTop = 20;
                    IDCardTemptable2.AddCell(EmpDataPart2);

                    EmpDataPart2 = new PdfPCell(new Phrase(": ", FontFactory.GetFont(fontstyle, Fontsize - 1, Font.BOLD, color)));
                    EmpDataPart2.HorizontalAlignment = 0;
                    EmpDataPart2.Border = 0;
                    EmpDataPart2.Colspan = 1;
                    //  EmpDataPart2.PaddingLeft = -20;
                    // EmpDataPart2.PaddingTop = 20;
                    IDCardTemptable2.AddCell(EmpDataPart2);

                    EmpDataPart2 = new PdfPCell(new Phrase(EmergencyContNo, FontFactory.GetFont(fontstyle, Fontsize - 1, Font.BOLD, color)));
                    EmpDataPart2.HorizontalAlignment = 0;
                    EmpDataPart2.Border = 0;
                    EmpDataPart2.Colspan = 3;
                    //  EmpDataPart2.PaddingLeft = -20;
                    // EmpDataPart2.PaddingTop = 20;
                    IDCardTemptable2.AddCell(EmpDataPart2);

                    EmpDataPart2 = new PdfPCell(new Phrase("Blood Group : " + BloodGroup, FontFactory.GetFont(fontstyle, Fontsize - 1, Font.BOLD, color)));
                    EmpDataPart2.HorizontalAlignment = 0;
                    EmpDataPart2.Colspan = 2;
                    EmpDataPart2.Border = 0;
                    IDCardTemptable2.AddCell(EmpDataPart2);

                    EmpDataPart2 = new PdfPCell(new Phrase("Employee Signature : " + EmpSign, FontFactory.GetFont(fontstyle, Fontsize - 1, Font.BOLD, color)));
                    EmpDataPart2.HorizontalAlignment = 0;
                    EmpDataPart2.Border = 0;
                    EmpDataPart2.Colspan = 3;
                    IDCardTemptable2.AddCell(EmpDataPart2);

                    EmpDataPart2 = new PdfPCell(new Phrase("", FontFactory.GetFont(fontstyle, Fontsize - 1, Font.BOLD, BaseColor.RED)));
                    EmpDataPart2.HorizontalAlignment = 0;
                    //EmpDataPart2.Border = 1;
                    EmpDataPart2.BorderWidthTop = 0;
                    EmpDataPart2.BorderWidthLeft = 0;
                    EmpDataPart2.BorderWidthRight = 0;
                    EmpDataPart2.Colspan = 5;
                    IDCardTemptable2.AddCell(EmpDataPart2);



                    PdfPCell Instructions = new PdfPCell(new Phrase("INSTRUCTIONS:", FontFactory.GetFont(fontstyle, Fontsize, Font.BOLD, color)));
                    Instructions.HorizontalAlignment = 0;
                    Instructions.Border = 0;
                    Instructions.Colspan = 5;
                    //Instructions.PaddingLeft = -20;
                    //Instructions.PaddingTop = 20;
                    IDCardTemptable2.AddCell(Instructions);



                    PdfPCell Instructions1 = new PdfPCell(new Phrase("1. This Card related to the identity of the employee described.\n" + "2. Penalty for loss Rs. 100/- .\n" + "3. Card is valid for 1 year from date of  issue.", FontFactory.GetFont(fontstyle, Fontsize - 1, Font.NORMAL, color)));
                    Instructions1.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                    Instructions1.Border = 0;
                    Instructions1.Colspan = 5;
                    //     Instructions1.PaddingLeft = -10f;
                    Instructions1.SetLeading(0f, 1.2f);
                    IDCardTemptable2.AddCell(Instructions1);

                    #endregion for sub table

                    PdfPCell childTable2 = new PdfPCell(IDCardTemptable2);
                    childTable2.HorizontalAlignment = 0;
                    childTable2.Colspan = 2;
                    //childTable2.PaddingLeft = 20;
                    IDCarddetails2.AddCell(childTable2);


                    PdfPTable IDCardTemptable31 = new PdfPTable(1);
                    IDCardTemptable31.TotalWidth = 2f;
                    float[] width31 = new float[] { 1f };
                    IDCardTemptable31.SetWidths(width31);

                    PdfPCell cellempcell = new PdfPCell(new Phrase("", FontFactory.GetFont(fontstyle, Fontsize, Font.NORMAL, BaseColor.BLACK)));
                    cellempcell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                    cellempcell.Border = 0;
                    cellempcell.Colspan = 1;
                    cellempcell.Rotation = 90;
                    // cellempcell.PaddingBottom = 80;
                    IDCardTemptable31.AddCell(cellempcell);

                    PdfPCell childTable3 = new PdfPCell(IDCardTemptable31);
                    childTable3.HorizontalAlignment = 0;
                    childTable3.Colspan = 1;
                    childTable3.Border = 0;
                    childTable3.PaddingBottom = 30;
                    IDCarddetails2.AddCell(childTable3);


                }


                PdfPCell empcellnew1 = new PdfPCell();
                empcellnew1.HorizontalAlignment = 0;
                empcellnew1.Colspan = 5;
                empcellnew1.Border = 0;
                IDCarddetails2.AddCell(empcellnew1);

                empcellnew1 = new PdfPCell();
                empcellnew1.HorizontalAlignment = 0;
                empcellnew1.Colspan = 5;
                empcellnew1.Border = 0;
                IDCarddetails2.AddCell(empcellnew1);
                IDCarddetails2.AddCell(empcellnew1);
                IDCarddetails2.AddCell(empcellnew1);

                document.Add(IDCarddetails2);



                document.Close();

                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment;filename=IDCard.pdf");
                Response.Buffer = true;
                Response.Clear();
                Response.OutputStream.Write(ms.GetBuffer(), 0, ms.GetBuffer().Length);
                Response.OutputStream.Flush();
                Response.End();
            }
        }

        protected void BtnIDNewCard_Click(object sender, EventArgs e)
        {
            int Fontsize = 7;

            string fontstyle = "Calibri";


            List<String> EmpId_list = new List<string>();




            Font FontStyle1 = FontFactory.GetFont("Perpetua Titling MT", BaseFont.CP1252, BaseFont.EMBEDDED, Fontsize - 3, Font.BOLD, BaseColor.BLACK);


            var list = new List<string>();

            for (int i = 0; i < lstEmpIdName.Items.Count; i++)
            {


                if (lstEmpIdName.Items[i].Selected == true)
                {
                    list.Add("'" + lstEmpIdName.Items[i].Value + "'");
                }
            }


            string empids = string.Join(",", list.ToArray());



            #region for Variable Declaration

            string Title = "";
            string Empid = "";
            string Name = "";
            string Designation = "";
            string IDcardIssued = "";
            string IDcardvalid = "";
            string BloodGroup = "";
            string prTown = "";
            string prPostOffice = "";
            string prTaluka = "";
            string statessndcity = "";
            string prPoliceStation = "";
            string prcity = "";
            string prphone = "";
            string prlmark = "";
            string prLmark = "";
            string prPincode = "";
            string prState = "";
            string State = "";
            string address1 = "";
            string Image = "";
            string EmpSign = "";
            string empdob = "";
            string empdoj = "";
            string EmpIdMark = "";

            #endregion for Variable Declaration


            string QueryCompanyInfo = "Select * from CompanyInfo ";
            DataTable DtCompanyInfo = SqlHelper.Instance.GetTableByQuery(QueryCompanyInfo);

            string CompanyName = "";
            string Address = "";
            string address = "";
            string companyinfo = "";
            string EmpDtofLeaving = "";
            string peState = "";
            string Emailid = "";
            string Website = "";
            string comphone = "";
            string empsex = "";
            if (DtCompanyInfo.Rows.Count > 0)
            {
                CompanyName = DtCompanyInfo.Rows[0]["CompanyName"].ToString();
                Address = DtCompanyInfo.Rows[0]["Address"].ToString();
                companyinfo = DtCompanyInfo.Rows[0]["CompanyInfo"].ToString();
                Emailid = DtCompanyInfo.Rows[0]["Emailid"].ToString();
                Website = DtCompanyInfo.Rows[0]["Website"].ToString();
                comphone = DtCompanyInfo.Rows[0]["Phoneno"].ToString();

            }

            string query = "";
            DataTable dt = new DataTable();




            query = "select Empid,(EmpFName) as Fullname,D.Design as EmpDesgn,empsex,prPostOffice,prPincode,(States.State+Cities.City) as statessndcity,(prTaluka+prPostOffice) as address1,EmpDetails.prLmark,prphone,prState,prcity,EmpDetails.prTaluka,EmpDetails.prTown,States.State,Cities.City,EmpDetails.prPincode,EmpPermanentAddress,(EmpDetails.prcity+EmpDetails.prLmark+EmpDetails.prTaluka+EmpDetails.prTown+States.State+Cities.City+EmpDetails.prPincode+EmpDetails.EmpPresentAddress) as address ," +
                "case convert(varchar(10),EmpDtofBirth,103) when '01/01/1900' then '' else convert(varchar(10),EmpDtofBirth,103) end EmpDtofBirth ," +
                "case convert(varchar(10),EmpDtofJoining,103) when '01/01/1900' then '' else convert(varchar(10),EmpDtofJoining,103) end EmpDtofJoining ," +
                "case convert(varchar(10),EmpDtofLeaving,103) when '01/01/1900' then '' else convert(varchar(10),EmpDtofLeaving,103) end EmpDtofLeaving ," +
                "case convert(varchar(10),IDCardIssued,103) when '01/01/1900' then '' else convert(varchar(10),IDCardIssued,103) end IDCardIssued ," +
                "case convert(varchar(10),IDCardValid,103) when '01/01/1900' then '' else convert(varchar(10),IDCardValid,103) end IDCardValid ," +
                "Image,EmpSign,BN.BloodGroupName as EmpBloodGroup,isnull(EmpIdMark1,'') as EmpIdMark from EmpDetails " +
                         " inner join designations D on D.Designid=EmpDetails.EmpDesgn " +
                         " left join BloodGroupNames BN on BN.BloodGroupId=EmpDetails.EmpBloodGroup left join Cities on  Cities.CityID= EmpDetails.prCity       LEFT JOIN States on States.StateID=EmpDetails.prState      " +
                         "left join branch b on b.branchid=empdetails.branch" +
                         " where empid  in (" + empids + ")  order by empid";
            dt = SqlHelper.Instance.GetTableByQuery(query);

            if (dt.Rows.Count > 0)
            {
                MemoryStream ms = new MemoryStream();

                BaseFont bf = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                string imagepath1 = Server.MapPath("~/assets/EmpPhotos/");
                string imagepath2 = Server.MapPath("~/assets/Images/signature.png");
                string imagepath5 = Server.MapPath("~/assets/" + CmpIDPrefix + "BillLogo.png");

                string imagepath6 = Server.MapPath("~/assets/Stamp.png");
                string imagepath7 = Server.MapPath("~/assets/NoPhoto.jpg");

                Document document = new Document(PageSize.A4);
                var writer = PdfWriter.GetInstance(document, ms);
                document.Open();







                BaseColor color = new BaseColor(153, 0, 0);

                #region for first part

                for (int k = 0; k < dt.Rows.Count; k++)
                {





                    prlmark = "";
                    prTaluka = "";
                    prTown = "";
                    prphone = "";
                    prcity = "";
                    prPincode = "";
                    peState = "";
                    prPostOffice = "";

                    Empid = dt.Rows[k]["Empid"].ToString();
                    Name = dt.Rows[k]["Fullname"].ToString();
                    empsex = dt.Rows[k]["empsex"].ToString();
                    if (empsex == "M")
                    {
                        Title = "Mr";
                    }
                    else
                    {
                        Title = "Ms";
                    }

                    Designation = dt.Rows[k]["EmpDesgn"].ToString().Substring(0, 1) + dt.Rows[k]["EmpDesgn"].ToString().Substring(1).ToLower();
                    IDcardIssued = dt.Rows[k]["IDCardIssued"].ToString();
                    IDcardvalid = dt.Rows[k]["IDCardValid"].ToString();
                    BloodGroup = dt.Rows[k]["EmpBloodGroup"].ToString();
                    Image = dt.Rows[k]["Image"].ToString();
                    EmpSign = dt.Rows[k]["EmpSign"].ToString();
                    empdob = dt.Rows[k]["EmpDtofBirth"].ToString();
                    empdoj = dt.Rows[k]["EmpDtofJoining"].ToString();
                    address = dt.Rows[k]["address"].ToString();
                    prlmark = dt.Rows[k]["prLmark"].ToString();
                    prTaluka = dt.Rows[k]["prTaluka"].ToString();
                    prTown = dt.Rows[k]["prTown"].ToString();
                    prphone = dt.Rows[k]["prphone"].ToString();
                    prcity = dt.Rows[0]["City"].ToString();
                    prPincode = dt.Rows[0]["prPincode"].ToString();
                    peState = dt.Rows[k]["State"].ToString();
                    prPostOffice = dt.Rows[k]["prPostOffice"].ToString();
                    Emailid = DtCompanyInfo.Rows[0]["Emailid"].ToString();
                    Website = DtCompanyInfo.Rows[0]["Website"].ToString();
                    address1 = dt.Rows[k]["address1"].ToString();
                    State = dt.Rows[k]["State"].ToString();
                    prPincode = dt.Rows[k]["prPincode"].ToString();
                    EmpDtofLeaving = dt.Rows[k]["EmpDtofLeaving"].ToString();
                    EmpIdMark = dt.Rows[k]["EmpIdMark"].ToString();


                    PdfPTable IDCarddetails = new PdfPTable(10);
                    IDCarddetails.TotalWidth = 364f;   //364f
                    IDCarddetails.LockedWidth = false; //true
                    float[] width = new float[] { 5f, 2f, 2f, 2f, 0.2f, 5f, 2f, 2f, 2f, 2.4f };
                    IDCarddetails.SetWidths(width);

                    PdfPTable IDCardTemptable1 = new PdfPTable(4);
                    IDCardTemptable1.TotalWidth = 200f;            //167f
                    float[] width1 = new float[] { 2.4f, 2.4f, 2.4f, 2.4f };
                    IDCardTemptable1.SetWidths(width1);

                    if (File.Exists(imagepath5))
                    {
                        iTextSharp.text.Image srflogo = iTextSharp.text.Image.GetInstance(imagepath5);
                        srflogo.ScaleAbsolute(50f, 40f);
                        PdfPCell companylogo = new PdfPCell();
                        Paragraph cmplogo = new Paragraph();
                        cmplogo.Add(new Chunk(srflogo, 70f, 0f, true));
                        companylogo.AddElement(cmplogo);
                        companylogo.HorizontalAlignment = 1;
                        companylogo.Colspan = 4;
                        //companylogo.PaddingLeft = -25;
                        companylogo.Border = 0;
                        IDCardTemptable1.AddCell(companylogo);
                    }
                    else
                    {
                        PdfPCell companylogo = new PdfPCell();
                        companylogo.HorizontalAlignment = 1;
                        companylogo.Colspan = 4;
                        companylogo.Border = 0;
                        companylogo.FixedHeight = 45;
                        IDCardTemptable1.AddCell(companylogo);
                    }

                    #region commented code for address

                    Font FontStyle2 = FontFactory.GetFont("Perpetua Titling MT", BaseFont.CP1252, BaseFont.EMBEDDED, Fontsize - 3, Font.BOLD, BaseColor.BLACK);

                    PdfPCell cellCertification = new PdfPCell(new Phrase("", FontFactory.GetFont(fontstyle, Fontsize - 2, Font.BOLD, color)));
                    cellCertification.HorizontalAlignment = 1;
                    cellCertification.Border = 0;
                    cellCertification.Colspan = 4;
                    cellCertification.PaddingTop = 10f;
                    //IDCardTemptable1.AddCell(cellCertification);


                    #endregion commented code for address

                    if (Image.Length > 0)
                    {
                        iTextSharp.text.Image Empphoto = iTextSharp.text.Image.GetInstance(imagepath1 + Image);
                        //Empphoto.ScalePercent(25f);
                        Empphoto.ScaleAbsolute(50f, 60f);
                        PdfPCell EmpImage = new PdfPCell();
                        Paragraph Emplogo = new Paragraph();
                        Emplogo.Add(new Chunk(Empphoto, 67f, 0));
                        EmpImage.AddElement(Emplogo);
                        EmpImage.HorizontalAlignment = 1;
                        EmpImage.Colspan = 4;
                        EmpImage.Border = 0;
                        //EmpImage.PaddingLeft = -20;
                        EmpImage.PaddingTop = 2;
                        IDCardTemptable1.AddCell(EmpImage);
                    }
                    else
                    {
                        iTextSharp.text.Image Empphoto = iTextSharp.text.Image.GetInstance(imagepath7);
                        //Empphoto.ScalePercent(25f);
                        Empphoto.ScaleAbsolute(50f, 60f);
                        PdfPCell EmpImage = new PdfPCell();
                        Paragraph Emplogo = new Paragraph();
                        Emplogo.Add(new Chunk(Empphoto, 67f, 0));
                        EmpImage.AddElement(Emplogo);
                        EmpImage.HorizontalAlignment = 1;
                        EmpImage.Colspan = 4;
                        EmpImage.Border = 0;
                        //EmpImage.PaddingLeft = -20;
                        EmpImage.PaddingTop = 2;
                        IDCardTemptable1.AddCell(EmpImage);

                    }



                    PdfPCell Formcell = new PdfPCell(new Phrase("Form IX", FontFactory.GetFont(fontstyle, Fontsize, Font.BOLD, color)));
                    Formcell.HorizontalAlignment = 1;
                    Formcell.Border = 0;
                    Formcell.Colspan = 4;
                    Formcell.PaddingTop = 1;
                    IDCardTemptable1.AddCell(Formcell);

                    PdfPCell ACTcell = new PdfPCell(new Phrase("(AS PER PSAR ACT 2005)", FontFactory.GetFont(fontstyle, Fontsize, Font.BOLD, color)));
                    ACTcell.HorizontalAlignment = 1;
                    ACTcell.Border = 0;
                    ACTcell.Colspan = 4;
                    ACTcell.PaddingTop = 1;
                    IDCardTemptable1.AddCell(ACTcell);

                    PdfPCell PSGScell = new PdfPCell(new Phrase("Photo-ID card for Private Security Guard/Supervisor", FontFactory.GetFont(fontstyle, Fontsize, Font.BOLD, BaseColor.BLACK)));
                    PSGScell.HorizontalAlignment = 1;
                    PSGScell.Border = 0;
                    PSGScell.Colspan = 4;
                    PSGScell.PaddingTop = 1;
                    PSGScell.PaddingBottom = 10;
                    IDCardTemptable1.AddCell(PSGScell);

                    PdfPCell PSANamecell = new PdfPCell(new Phrase("Name of the Private Security  Agency  " + " : ", FontFactory.GetFont(fontstyle, Fontsize, Font.NORMAL, BaseColor.BLACK)));
                    PSANamecell.HorizontalAlignment = 1;
                    PSANamecell.Border = 0;
                    PSANamecell.Colspan = 4;
                    PSANamecell.PaddingTop = 2;
                    IDCardTemptable1.AddCell(PSANamecell);

                    PSANamecell = new PdfPCell(new Phrase(CompanyName, FontFactory.GetFont(fontstyle, Fontsize, Font.BOLD, BaseColor.BLACK)));
                    PSANamecell.HorizontalAlignment = 0;
                    PSANamecell.Border = 0;
                    PSANamecell.Colspan = 4;
                    PSANamecell.PaddingTop = 10;
                    // cellName.PaddingLeft = -40;
                    IDCardTemptable1.AddCell(PSANamecell);

                    PdfPCell cellName = new PdfPCell(new Phrase("Name of the Private Security  Guard/Supervisor ", FontFactory.GetFont(fontstyle, Fontsize, Font.NORMAL, BaseColor.BLACK)));
                    cellName.HorizontalAlignment = 0;
                    cellName.Border = 0;
                    cellName.Colspan = 2;
                    cellName.PaddingTop = 2;
                    // cellName.PaddingLeft = 13;
                    IDCardTemptable1.AddCell(cellName);

                    cellName = new PdfPCell(new Phrase(": " + Name, FontFactory.GetFont(fontstyle, Fontsize, Font.BOLD, BaseColor.BLACK)));
                    cellName.HorizontalAlignment = 0;
                    cellName.Border = 0;
                    cellName.Colspan = 2;
                    cellName.PaddingTop = 10;
                    // cellName.PaddingLeft = -40;
                    IDCardTemptable1.AddCell(cellName);

                    PdfPCell cellRank = new PdfPCell(new Phrase("Official Designation", FontFactory.GetFont(fontstyle, Fontsize, Font.NORMAL, BaseColor.BLACK)));
                    cellRank.HorizontalAlignment = 0;
                    cellRank.Border = 0;
                    cellRank.Colspan = 2;
                    cellRank.PaddingTop = 1;
                    //  cellRank.PaddingLeft = 13;
                    //  cellRank.FixedHeight = 20;
                    IDCardTemptable1.AddCell(cellRank);

                    cellRank = new PdfPCell(new Phrase(": " + Designation, FontFactory.GetFont(fontstyle, Fontsize, Font.BOLD, BaseColor.BLACK)));
                    cellRank.HorizontalAlignment = 0;
                    cellRank.Border = 0;
                    cellRank.Colspan = 2;
                    cellRank.PaddingTop = 1;
                    // cellRank.PaddingLeft = -40;
                    // cellRank.FixedHeight = 20;
                    IDCardTemptable1.AddCell(cellRank);


                    PdfPCell cellIDNo = new PdfPCell(new Phrase("ID No ", FontFactory.GetFont(fontstyle, Fontsize, Font.NORMAL, BaseColor.BLACK)));
                    cellIDNo.HorizontalAlignment = 0;
                    cellIDNo.Border = 0;
                    cellIDNo.Colspan = 2;
                    cellIDNo.PaddingTop = 1;
                    // cellIDNo.PaddingLeft = 13;
                    IDCardTemptable1.AddCell(cellIDNo);

                    cellIDNo = new PdfPCell(new Phrase(": " + Empid, FontFactory.GetFont(fontstyle, Fontsize, Font.BOLD, BaseColor.BLACK)));
                    cellIDNo.HorizontalAlignment = 0;
                    cellIDNo.Border = 0;
                    cellIDNo.Colspan = 2;
                    cellIDNo.PaddingTop = 1;
                    //    cellIDNo.PaddingLeft = -40;
                    IDCardTemptable1.AddCell(cellIDNo);

                    PdfPCell CellBloodgroup = new PdfPCell(new Phrase("Blood Group", FontFactory.GetFont(fontstyle, Fontsize, Font.NORMAL, BaseColor.BLACK)));
                    CellBloodgroup.HorizontalAlignment = 0;
                    CellBloodgroup.Border = 0;
                    CellBloodgroup.Colspan = 2;
                    CellBloodgroup.PaddingTop = 1;
                    IDCardTemptable1.AddCell(CellBloodgroup);

                    CellBloodgroup = new PdfPCell(new Phrase(": " + BloodGroup, FontFactory.GetFont(fontstyle, Fontsize, Font.BOLD, BaseColor.BLACK)));
                    CellBloodgroup.HorizontalAlignment = 0;
                    CellBloodgroup.Border = 0;
                    CellBloodgroup.Colspan = 2;
                    CellBloodgroup.PaddingTop = 1;
                    //   CellBloodgroup.PaddingLeft = -12;
                    IDCardTemptable1.AddCell(CellBloodgroup);

                    PdfPCell CellDOI = new PdfPCell(new Phrase("Date of Issue", FontFactory.GetFont(fontstyle, Fontsize, Font.NORMAL, BaseColor.BLACK)));
                    CellDOI.HorizontalAlignment = 0;
                    CellDOI.Border = 0;
                    CellDOI.Colspan = 2;
                    CellDOI.PaddingTop = 1;
                    IDCardTemptable1.AddCell(CellDOI);

                    CellDOI = new PdfPCell(new Phrase(": " + IDcardIssued, FontFactory.GetFont(fontstyle, Fontsize, Font.BOLD, BaseColor.BLACK)));
                    CellDOI.HorizontalAlignment = 0;
                    CellDOI.Border = 0;
                    CellDOI.Colspan = 2;
                    CellDOI.PaddingTop = 1;
                    //   CellBloodgroup.PaddingLeft = -12;
                    IDCardTemptable1.AddCell(CellDOI);

                    PdfPCell CellVUpto = new PdfPCell(new Phrase("Valid up to", FontFactory.GetFont(fontstyle, Fontsize, Font.NORMAL, BaseColor.BLACK)));
                    CellVUpto.HorizontalAlignment = 0;
                    CellVUpto.Border = 0;
                    CellVUpto.Colspan = 2;
                    CellVUpto.PaddingTop = 1;
                    IDCardTemptable1.AddCell(CellVUpto);

                    CellVUpto = new PdfPCell(new Phrase(": " + IDcardvalid, FontFactory.GetFont(fontstyle, Fontsize, Font.BOLD, BaseColor.BLACK)));
                    CellVUpto.HorizontalAlignment = 0;
                    CellVUpto.Border = 0;
                    CellVUpto.Colspan = 2;
                    CellVUpto.PaddingTop = 1;
                    //   CellBloodgroup.PaddingLeft = -12;
                    IDCardTemptable1.AddCell(CellVUpto);

                    PdfPCell CellEmpSign = new PdfPCell(new Phrase("Signature of the Cardholder", FontFactory.GetFont(fontstyle, Fontsize, Font.NORMAL, BaseColor.BLACK)));
                    CellEmpSign.HorizontalAlignment = 0;
                    CellEmpSign.Border = 0;
                    CellEmpSign.Colspan = 2;
                    CellEmpSign.PaddingTop = 2;
                    IDCardTemptable1.AddCell(CellEmpSign);

                    //if (File.Exists(imagepath3 + EmpSign) && EmpSign.Length > 0)
                    //{
                    //    iTextSharp.text.Image Sign = iTextSharp.text.Image.GetInstance(imagepath3);
                    //    //Sign.ScalePercent(10f);
                    //    Sign.ScaleAbsolute(70f, 15f);
                    //    PdfPCell Signature = new PdfPCell();
                    //    Paragraph signlogo = new Paragraph();
                    //    signlogo.Add(new Chunk(Sign, 0, 0));
                    //    Signature.AddElement(signlogo);
                    //    Signature.HorizontalAlignment = 0;
                    //    Signature.Colspan = 2;
                    //    Signature.PaddingTop = 2;
                    //    Signature.Border = 0;
                    //    IDCardTemptable1.AddCell(Signature);
                    //}
                    //else
                    //{

                    //    PdfPCell Signature = new PdfPCell();
                    //    Signature.HorizontalAlignment = 0;
                    //    Signature.Colspan = 2;
                    //    Signature.PaddingTop = 2;
                    //    Signature.Border = 0;
                    //    IDCardTemptable1.AddCell(Signature);

                    //}

                    PdfPCell CellAuthority = new PdfPCell(new Phrase("", FontFactory.GetFont(fontstyle, Fontsize, Font.NORMAL, BaseColor.BLACK)));
                    CellAuthority.HorizontalAlignment = 0;
                    CellAuthority.Border = 0;
                    CellAuthority.Colspan = 2;
                    CellAuthority.PaddingTop = 1;
                    CellAuthority.PaddingBottom = 30;
                    IDCardTemptable1.AddCell(CellAuthority);

                    PdfPCell CellAuthoritys = new PdfPCell(new Phrase("", FontFactory.GetFont(fontstyle, Fontsize, Font.NORMAL, BaseColor.BLACK)));
                    CellAuthoritys.HorizontalAlignment = 0;
                    CellAuthoritys.Border = 0;
                    CellAuthoritys.Colspan = 2;
                    CellAuthoritys.PaddingTop = 1;
                    CellAuthoritys.PaddingBottom = 30;
                    IDCardTemptable1.AddCell(CellAuthoritys);

                    if (File.Exists(imagepath2))
                    {
                        iTextSharp.text.Image IssuingAuth = iTextSharp.text.Image.GetInstance(imagepath2);
                        IssuingAuth.ScalePercent(30f);
                        IssuingAuth.SetAbsolutePosition(200f, 20f);
                        PdfPCell Authority = new PdfPCell();
                        Paragraph Authoritylogo = new Paragraph();
                        Authoritylogo.Add(new Chunk(IssuingAuth, -15, -10));
                        Authority.AddElement(Authoritylogo);
                        Authority.HorizontalAlignment = 2;
                        Authority.Colspan = 2;
                        Authority.Border = 0;
                        Authority.PaddingTop = 1;
                        Authority.PaddingLeft = 60;
                        IDCardTemptable1.AddCell(Authority);
                    }

                    else
                    {
                        PdfPCell Signature = new PdfPCell();
                        Signature.HorizontalAlignment = 2;
                        Signature.Colspan = 2;
                        Signature.PaddingTop = 2;
                        Signature.Border = 0;
                        IDCardTemptable1.AddCell(Signature);
                    }

                    PdfPCell CellSeal = new PdfPCell(new Phrase("Official Seal", FontFactory.GetFont(fontstyle, Fontsize, Font.BOLD, BaseColor.BLACK)));
                    CellSeal.HorizontalAlignment = 0;
                    CellSeal.Border = 0;
                    CellSeal.Colspan = 2;
                    CellSeal.PaddingTop = 1;
                    IDCardTemptable1.AddCell(CellSeal);

                    PdfPCell CellAutSign = new PdfPCell(new Phrase("Signature of the\nIssuing Authority", FontFactory.GetFont(fontstyle, Fontsize, Font.BOLD, BaseColor.BLACK)));
                    CellAutSign.HorizontalAlignment = 2;
                    CellAutSign.Border = 0;
                    CellAutSign.Colspan = 2;
                    CellAutSign.PaddingTop = 1;
                    IDCardTemptable1.AddCell(CellAutSign);

                    PdfPCell childTable1 = new PdfPCell(IDCardTemptable1);
                    childTable1.HorizontalAlignment = 0;
                    childTable1.Colspan = 4;
                    IDCarddetails.AddCell(childTable1);

                    PdfPTable IDCardTemptable41 = new PdfPTable(1);
                    IDCardTemptable41.TotalWidth = 1f;
                    float[] width41 = new float[] { 0.5f };
                    IDCardTemptable41.SetWidths(width41);

                    PdfPCell cellempcell1 = new PdfPCell(new Phrase("", FontFactory.GetFont(fontstyle, Fontsize, Font.NORMAL, BaseColor.BLACK)));
                    cellempcell1.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                    cellempcell1.Border = 0;
                    cellempcell1.Colspan = 1;
                    IDCardTemptable41.AddCell(cellempcell1);


                    PdfPCell childTable4 = new PdfPCell(IDCardTemptable41);
                    childTable4.HorizontalAlignment = 0;
                    childTable4.Colspan = 1;
                    childTable4.Border = 0;
                    IDCarddetails.AddCell(childTable4);

                    PdfPTable IDCardTemptable2 = new PdfPTable(4);
                    IDCardTemptable2.TotalWidth = 200f;            //167f
                    float[] width2 = new float[] { 0.5f, 4f, 2f, 2.3f };     //2.3f,2.3f,2.3f,2.3f
                    IDCardTemptable2.SetWidths(width2);

                    #region

                    PdfPCell CellDOB = new PdfPCell(new Phrase("Date of Birth", FontFactory.GetFont(fontstyle, Fontsize, Font.BOLD, BaseColor.BLACK)));
                    CellDOB.HorizontalAlignment = 0;
                    CellDOB.Border = 0;
                    CellDOB.Colspan = 2;
                    CellDOB.PaddingTop = 10;
                    IDCardTemptable2.AddCell(CellDOB);

                    CellDOB = new PdfPCell(new Phrase(empdob, FontFactory.GetFont(fontstyle, Fontsize, Font.NORMAL, BaseColor.BLACK)));
                    CellDOB.HorizontalAlignment = 0;
                    CellDOB.Border = 0;
                    CellDOB.Colspan = 2;
                    CellDOB.PaddingTop = 10;
                    // CellDOB.PaddingLeft = -12;
                    IDCardTemptable2.AddCell(CellDOB);

                    PdfPCell CellEmpIDMark = new PdfPCell(new Phrase("IDENTIFICATION MARK", FontFactory.GetFont(fontstyle, Fontsize, Font.BOLD | Font.UNDERLINE, color)));
                    CellEmpIDMark.HorizontalAlignment = 1;
                    CellEmpIDMark.Border = 0;
                    CellEmpIDMark.Colspan = 4;
                    CellEmpIDMark.PaddingTop = 10;
                    IDCardTemptable2.AddCell(CellEmpIDMark);

                    PdfPCell CellEmpIDMark1 = new PdfPCell(new Phrase(EmpIdMark, FontFactory.GetFont(fontstyle, Fontsize, Font.NORMAL, color)));
                    CellEmpIDMark1.HorizontalAlignment = 0;
                    CellEmpIDMark1.Border = 0;
                    CellEmpIDMark1.Colspan = 4;
                    CellEmpIDMark1.PaddingTop = 2;
                    IDCardTemptable2.AddCell(CellEmpIDMark1);

                    PdfPCell CellFound = new PdfPCell(new Phrase("IF FOUND PLEASE RETURN TO :-", FontFactory.GetFont(fontstyle, Fontsize, Font.BOLD | Font.UNDERLINE, color)));
                    CellFound.HorizontalAlignment = 1;
                    CellFound.Border = 0;
                    CellFound.Colspan = 4;
                    CellFound.PaddingTop = 4;
                    IDCardTemptable2.AddCell(CellFound);


                    PdfPCell CellOffSpace = new PdfPCell(new Phrase("", FontFactory.GetFont(fontstyle, Fontsize, Font.BOLD | Font.UNDERLINE, BaseColor.BLACK)));
                    CellOffSpace.HorizontalAlignment = 1;
                    CellOffSpace.Border = 0;
                    CellOffSpace.Colspan = 4;
                    CellOffSpace.PaddingTop = 4;
                    IDCardTemptable2.AddCell(CellOffSpace);

                    PdfPCell CellOffAdd = new PdfPCell(new Phrase("Office Address: ", FontFactory.GetFont(fontstyle, Fontsize, Font.BOLD | Font.UNDERLINE, BaseColor.BLACK)));
                    CellOffAdd.HorizontalAlignment = 1;
                    CellOffAdd.Border = 0;
                    CellOffAdd.Colspan = 4;
                    CellOffAdd.PaddingTop = 3;
                    IDCardTemptable2.AddCell(CellOffAdd);


                    PdfPCell CellOffAdd1 = new PdfPCell(new Phrase(CompanyName, FontFactory.GetFont(fontstyle, Fontsize, Font.BOLD, BaseColor.BLACK)));
                    CellOffAdd1.HorizontalAlignment = 1;
                    CellOffAdd1.Border = 0;
                    CellOffAdd1.Colspan = 4;
                    CellOffAdd1.PaddingTop = 1;
                    IDCardTemptable2.AddCell(CellOffAdd1);






                    PdfPCell CellOffAdd2 = new PdfPCell(new Phrase(Address, FontFactory.GetFont(fontstyle, Fontsize, Font.NORMAL, BaseColor.BLACK)));
                    CellOffAdd2.HorizontalAlignment = 1;
                    CellOffAdd2.Border = 0;
                    CellOffAdd2.Colspan = 4;
                    CellOffAdd2.PaddingTop = 1;
                    IDCardTemptable2.AddCell(CellOffAdd2);

                    PdfPCell CellOffAdd3 = new PdfPCell(new Phrase("", FontFactory.GetFont(fontstyle, Fontsize, Font.NORMAL, BaseColor.BLACK)));
                    CellOffAdd3.HorizontalAlignment = 1;
                    CellOffAdd3.Border = 0;
                    CellOffAdd3.Colspan = 4;
                    CellOffAdd3.PaddingTop = 1;
                    IDCardTemptable2.AddCell(CellOffAdd3);

                    PdfPCell CellOffAdd4 = new PdfPCell(new Phrase("Ph.: " + comphone, FontFactory.GetFont(fontstyle, Fontsize, Font.NORMAL, BaseColor.BLACK)));
                    CellOffAdd4.HorizontalAlignment = 1;
                    CellOffAdd4.Border = 0;
                    CellOffAdd4.Colspan = 4;
                    CellOffAdd4.PaddingTop = 1;
                    IDCardTemptable2.AddCell(CellOffAdd4);

                    PdfPCell CellOffAdd5 = new PdfPCell(new Phrase(" EMAIL: " + Emailid, FontFactory.GetFont(fontstyle, Fontsize, Font.NORMAL, BaseColor.BLACK)));
                    CellOffAdd5.HorizontalAlignment = 1;
                    CellOffAdd5.Border = 0;
                    CellOffAdd5.Colspan = 4;
                    CellOffAdd5.PaddingTop = 1;
                    IDCardTemptable2.AddCell(CellOffAdd5);

                    //PdfPCell CellOffAdd6= new PdfPCell(new Phrase("Email : contact@3psecuritas.com", FontFactory.GetFont(fontstyle, Fontsize, Font.NORMAL, BaseColor.BLACK)));
                    //CellOffAdd6.HorizontalAlignment = 1;
                    //CellOffAdd6.Border = 0;
                    //CellOffAdd6.Colspan = 4;
                    //CellOffAdd6.PaddingTop = 1;
                    //IDCardTemptable2.AddCell(CellOffAdd6);

                    PdfPCell CellOffAdd7 = new PdfPCell(new Phrase("", FontFactory.GetFont(fontstyle, Fontsize, Font.NORMAL, BaseColor.BLACK)));
                    CellOffAdd7.HorizontalAlignment = 1;
                    CellOffAdd7.Border = 0;
                    CellOffAdd7.Colspan = 4;
                    CellOffAdd7.PaddingTop = 1;
                    IDCardTemptable2.AddCell(CellOffAdd7);

                    PdfPCell CellOffAdd8 = new PdfPCell(new Phrase(Website, FontFactory.GetFont(fontstyle, Fontsize, Font.NORMAL, BaseColor.BLACK)));
                    CellOffAdd8.HorizontalAlignment = 1;
                    CellOffAdd8.Border = 0;
                    CellOffAdd8.Colspan = 4;
                    CellOffAdd8.PaddingTop = 1;
                    IDCardTemptable2.AddCell(CellOffAdd8);

                    PdfPCell CellNote = new PdfPCell(new Phrase("Note : ", FontFactory.GetFont(fontstyle, Fontsize, Font.BOLD, BaseColor.BLACK)));
                    CellNote.HorizontalAlignment = 0;
                    CellNote.Border = 0;
                    CellNote.Colspan = 4;
                    CellNote.PaddingTop = 20;
                    IDCardTemptable2.AddCell(CellNote);

                    PdfPCell CellNote1 = new PdfPCell(new Phrase("1. The Identity Card should be carried by you at all times.", FontFactory.GetFont(fontstyle, Fontsize, Font.NORMAL, BaseColor.BLACK)));
                    CellNote1.HorizontalAlignment = 0;
                    CellNote1.Border = 0;
                    CellNote1.Colspan = 4;
                    CellNote1.PaddingTop = 3;
                    IDCardTemptable2.AddCell(CellNote1);

                    PdfPCell CellNote2 = new PdfPCell(new Phrase("2. Please report loss of card immediately to head office.", FontFactory.GetFont(fontstyle, Fontsize, Font.NORMAL, BaseColor.BLACK)));
                    CellNote2.HorizontalAlignment = 0;
                    CellNote2.Border = 0;
                    CellNote2.Colspan = 4;
                    CellNote2.PaddingTop = 1;
                    IDCardTemptable2.AddCell(CellNote2);

                    PdfPCell CellNote3 = new PdfPCell(new Phrase("3. Re-issuance of Identity card will happen after proper investigation @ INR 500.", FontFactory.GetFont(fontstyle, Fontsize, Font.NORMAL, BaseColor.BLACK)));
                    CellNote3.HorizontalAlignment = 0;
                    CellNote3.Border = 0;
                    CellNote3.Colspan = 4;
                    CellNote3.PaddingTop = 1;
                    IDCardTemptable2.AddCell(CellNote3);

                    PdfPCell CellPolice = new PdfPCell(new Phrase("", FontFactory.GetFont(fontstyle, Fontsize, Font.NORMAL, BaseColor.BLACK)));
                    CellPolice.HorizontalAlignment = 0;
                    CellPolice.Border = 0;
                    CellPolice.Colspan = 1;
                    CellPolice.PaddingTop = 10;
                    IDCardTemptable2.AddCell(CellPolice);

                    PdfPCell CellPolice1 = new PdfPCell(new Phrase("POLICE", FontFactory.GetFont(fontstyle, Fontsize, Font.BOLD, BaseColor.BLACK)));
                    CellPolice1.HorizontalAlignment = 0;
                    CellPolice1.Border = 0;
                    CellPolice1.Colspan = 1;
                    CellPolice1.PaddingTop = 10;
                    IDCardTemptable2.AddCell(CellPolice1);

                    PdfPCell CellPolice11 = new PdfPCell(new Phrase(" : 100", FontFactory.GetFont(fontstyle, Fontsize, Font.BOLD, BaseColor.BLACK)));
                    CellPolice11.HorizontalAlignment = 0;
                    CellPolice11.Border = 0;
                    CellPolice11.Colspan = 2;
                    CellPolice11.PaddingTop = 10;
                    IDCardTemptable2.AddCell(CellPolice11);

                    PdfPCell CellFire = new PdfPCell(new Phrase("", FontFactory.GetFont(fontstyle, Fontsize, Font.NORMAL, BaseColor.BLACK)));
                    CellFire.HorizontalAlignment = 0;
                    CellFire.Border = 0;
                    CellFire.Colspan = 1;
                    CellFire.PaddingTop = 1;
                    IDCardTemptable2.AddCell(CellFire);

                    PdfPCell CellFire1 = new PdfPCell(new Phrase("FIRE", FontFactory.GetFont(fontstyle, Fontsize, Font.BOLD, BaseColor.BLACK)));
                    CellFire1.HorizontalAlignment = 0;
                    CellFire1.Border = 0;
                    CellFire1.Colspan = 1;
                    CellFire1.PaddingTop = 1;
                    IDCardTemptable2.AddCell(CellFire1);

                    PdfPCell CellFire11 = new PdfPCell(new Phrase(" : 101", FontFactory.GetFont(fontstyle, Fontsize, Font.BOLD, BaseColor.BLACK)));
                    CellFire11.HorizontalAlignment = 0;
                    CellFire11.Border = 0;
                    CellFire11.Colspan = 2;
                    CellFire11.PaddingTop = 1;
                    IDCardTemptable2.AddCell(CellFire11);

                    PdfPCell CellAmbulance = new PdfPCell(new Phrase("", FontFactory.GetFont(fontstyle, Fontsize, Font.NORMAL, BaseColor.BLACK)));
                    CellAmbulance.HorizontalAlignment = 0;
                    CellAmbulance.Border = 0;
                    CellAmbulance.Colspan = 1;
                    CellAmbulance.PaddingTop = 1;
                    IDCardTemptable2.AddCell(CellAmbulance);

                    PdfPCell CellAmbulance1 = new PdfPCell(new Phrase("AMBULANCE", FontFactory.GetFont(fontstyle, Fontsize, Font.BOLD, BaseColor.BLACK)));
                    CellAmbulance1.HorizontalAlignment = 0;
                    CellAmbulance1.Border = 0;
                    CellAmbulance1.Colspan = 1;
                    CellAmbulance1.PaddingTop = 1;
                    IDCardTemptable2.AddCell(CellAmbulance1);

                    PdfPCell CellAmbulance11 = new PdfPCell(new Phrase(" : 102", FontFactory.GetFont(fontstyle, Fontsize, Font.BOLD, BaseColor.BLACK)));
                    CellAmbulance11.HorizontalAlignment = 0;
                    CellAmbulance11.Border = 0;
                    CellAmbulance11.Colspan = 2;
                    CellAmbulance11.PaddingTop = 1;
                    IDCardTemptable2.AddCell(CellAmbulance11);


                    PdfPCell childTable2 = new PdfPCell(IDCardTemptable2);
                    childTable2.HorizontalAlignment = 0;
                    childTable2.Colspan = 4;
                    childTable2.PaddingLeft = 20;

                    IDCarddetails.AddCell(childTable2);

                    #endregion

                    PdfPTable IDCardTemptable31 = new PdfPTable(1);
                    IDCardTemptable31.TotalWidth = 2f;
                    IDCardTemptable31.LockedWidth = true;
                    float[] width31 = new float[] { 1f };
                    IDCardTemptable31.SetWidths(width31);

                    PdfPCell cellempcell = new PdfPCell(new Phrase("", FontFactory.GetFont(fontstyle, Fontsize, Font.NORMAL, BaseColor.BLACK)));
                    cellempcell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                    cellempcell.Border = 0;
                    cellempcell.Colspan = 1;
                    IDCardTemptable31.AddCell(cellempcell);

                    PdfPCell childTable3 = new PdfPCell(IDCardTemptable31);
                    childTable3.HorizontalAlignment = 0;
                    childTable3.Colspan = 1;
                    childTable3.Border = 0;
                    IDCarddetails.AddCell(childTable3);


                    ///


                    PdfPCell empcellnew = new PdfPCell();
                    empcellnew.HorizontalAlignment = 0;
                    empcellnew.Colspan = 10;
                    empcellnew.MinimumHeight = 10;
                    empcellnew.Border = 0;
                    IDCarddetails.AddCell(empcellnew);

                    document.Add(IDCarddetails);
                    #endregion

                }

                document.Close();

                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment;filename=IDCard.pdf");
                Response.Buffer = true;
                Response.Clear();
                Response.OutputStream.Write(ms.GetBuffer(), 0, ms.GetBuffer().Length);
                Response.OutputStream.Flush();
                Response.End();



            }
        }

        protected void BtnIDCard1_Click(object sender, EventArgs e)
        {
            int Fontsize = 10;
            int Fontsize2 = 10;
            string fontstyle = "Calibri";

            List<String> EmpId_list = new List<string>();

            int totalfonts = FontFactory.RegisterDirectory("c:\\WINDOWS\\fonts");
            StringBuilder sa = new StringBuilder();
            foreach (string fontname in FontFactory.RegisteredFonts)
            {
                sa.Append(fontname + "\n");
            }


            Font FontStyle1 = FontFactory.GetFont("Perpetua Titling MT", BaseFont.CP1252, BaseFont.EMBEDDED, Fontsize - 3, Font.BOLD, BaseColor.BLACK);


            var list = new List<string>();

            for (int i = 0; i < lstEmpIdName.Items.Count; i++)
            {


                if (lstEmpIdName.Items[i].Selected == true)
                {
                    list.Add("'" + lstEmpIdName.Items[i].Value + "'");
                }
            }


            string empids = string.Join(",", list.ToArray());



            #region for Variable Declaration

            string Title = "";
            string Empid = "";
            string Name = "";
            string Designation = "";
            string IDcardIssued = "";
            string IDcardvalid = "";
            string BloodGroup = "";
            string prTown = "";
            string prPostOffice = "";
            string prTaluka = "";
            string statessndcity = "";
            string prPoliceStation = "";
            string prcity = "";
            string prphone = "";
            string prlmark = "";
            string prLmark = "";
            string prPincode = "";
            string prState = "";
            string State = "";
            string address1 = "";
            string Image = "";
            string EmpSign = "";
            string empdob = "";
            string empdoj = "";
            string address2 = "";

            #endregion for Variable Declaration


            string QueryCompanyInfo = "select * from companyinfo";
            DataTable DtCompanyInfo = config.ExecuteAdaptorAsyncWithQueryParams(QueryCompanyInfo).Result;

            string CompanyName = "";
            string Address = "";
            string address = "";
            string companyinfo = "";
            string EmpDtofLeaving = "";
            string IDCardValid = "";
            string peTaluka = "";
            string peTown = "";
            string peLmark = "";
            string pearea = "";
            string pecity = "";
            string peDistrict = "";
            string pePincode = "";
            string addres1 = "";
            string peState = "";
            string pelmark = "";
            string branch = "";
            string pestreet = "";
            string pePostOffice = "";
            string pephone = "";
            string pePoliceStation = "";
            string Emailid = "";
            string Website = "";
            string comphone = "";
            string empsex = "";
            string EmergencyContNo = "";
            if (DtCompanyInfo.Rows.Count > 0)
            {
                CompanyName = DtCompanyInfo.Rows[0]["CompanyName"].ToString();
                Address = DtCompanyInfo.Rows[0]["Address"].ToString();
                companyinfo = DtCompanyInfo.Rows[0]["CompanyInfo"].ToString();
                Emailid = DtCompanyInfo.Rows[0]["Emailid"].ToString();
                Website = DtCompanyInfo.Rows[0]["Website"].ToString();
                comphone = DtCompanyInfo.Rows[0]["Phoneno"].ToString();

            }

            string query = "";
            DataTable dt = new DataTable();




            query = "select Empid,(EmpFName+' '+EmpMName+''+EmpLName) as Fullname,D.Design as EmpDesgn,empsex,prPostOffice,prPincode,(States.State+Cities.City) as statessndcity,(prTaluka+prPostOffice) as address1,EmpDetails.prLmark,prphone,prState,prcity,EmpDetails.prTaluka,EmpDetails.prTown,States.State,Cities.City,EmpDetails.prPincode,EmpPermanentAddress,(EmpDetails.prcity+EmpDetails.prLmark+EmpDetails.prTaluka+EmpDetails.prTown+States.State+Cities.City+EmpDetails.prPincode+EmpDetails.EmpPresentAddress) as address ," +
                "case convert(varchar(10),EmpDtofBirth,103) when '01/01/1900' then '' else convert(varchar(10),EmpDtofBirth,103) end EmpDtofBirth ," +
                "case convert(varchar(10),EmpDtofJoining,103) when '01/01/1900' then '' else convert(varchar(10),EmpDtofJoining,103) end EmpDtofJoining ," +
                "case convert(varchar(10),EmpDtofLeaving,103) when '01/01/1900' then '' else convert(varchar(10),EmpDtofLeaving,103) end EmpDtofLeaving ," +
                "case convert(varchar(10),IDCardIssued,103) when '01/01/1900' then '' else convert(varchar(10),IDCardIssued,103) end IDCardIssued ," +
                "case convert(varchar(10),IDCardValid,103) when '01/01/1900' then '' else convert(varchar(10),IDCardValid,103) end IDCardValid ," +
                "Image,EmpSign,BN.BloodGroupName as EmpBloodGroup,isnull(EmergencyContNo,'') as EmergencyContNo from EmpDetails " +
                         " inner join designations D on D.Designid=EmpDetails.EmpDesgn " +
                         " left join BloodGroupNames BN on BN.BloodGroupId=EmpDetails.EmpBloodGroup left join Cities on  Cities.CityID= EmpDetails.prCity       LEFT JOIN States on States.StateID=EmpDetails.prState      " +
                         "left join branch b on b.branchid=empdetails.branch" +
                         " where empid  in (" + empids + ")  order by empid";
            dt = config.ExecuteAdaptorAsyncWithQueryParams(query).Result;

            if (dt.Rows.Count > 0)
            {
                MemoryStream ms = new MemoryStream();

                BaseFont bf = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                string imagepath1 = Server.MapPath("assets/EmpPhotos/");
                string imagepath2 = Server.MapPath("assets/Images/sign.jpg");
                string imagepath5 = Server.MapPath("assets/" + CmpIDPrefix + "logo.jpg");
                string imagepath6 = Server.MapPath("assets/EmpPhotos/");
                string imagepath7 = Server.MapPath("assets/Front Broder.jpg");
                string imagepath8 = Server.MapPath("assets/Back Broder.jpg");
                string imagepath9 = Server.MapPath("assets/Back Water Mark.jpg");

                Document document = new Document(PageSize.A4.Rotate());
                var writer = PdfWriter.GetInstance(document, ms);

                document.Open();



                #region for range ID Card Display


                for (int k = 0; k < dt.Rows.Count; k++)
                {
                    prlmark = "";
                    prTaluka = "";
                    prTown = "";
                    prphone = "";
                    prcity = "";
                    prPincode = "";
                    peState = "";
                    prPostOffice = "";

                    Empid = dt.Rows[k]["Empid"].ToString();
                    Name = dt.Rows[k]["Fullname"].ToString();
                    empsex = dt.Rows[k]["empsex"].ToString();
                    if (empsex == "M")
                    {
                        Title = "Mr";
                    }
                    else
                    {
                        Title = "Ms";
                    }

                    Designation = dt.Rows[k]["EmpDesgn"].ToString();
                    IDcardIssued = dt.Rows[k]["IDCardIssued"].ToString();
                    IDcardvalid = dt.Rows[k]["IDCardValid"].ToString();
                    BloodGroup = dt.Rows[k]["EmpBloodGroup"].ToString();
                    Image = dt.Rows[k]["Image"].ToString();
                    EmpSign = dt.Rows[k]["EmpSign"].ToString();
                    empdob = dt.Rows[k]["EmpDtofBirth"].ToString();
                    empdoj = dt.Rows[k]["EmpDtofJoining"].ToString();
                    address = dt.Rows[k]["address"].ToString();
                    prlmark = dt.Rows[k]["prLmark"].ToString();
                    prTaluka = dt.Rows[k]["prTaluka"].ToString();
                    prTown = dt.Rows[k]["prTown"].ToString();
                    prphone = dt.Rows[k]["prphone"].ToString();
                    prcity = dt.Rows[k]["City"].ToString();
                    prPincode = dt.Rows[k]["prPincode"].ToString();
                    peState = dt.Rows[k]["State"].ToString();
                    prPostOffice = dt.Rows[k]["prPostOffice"].ToString();
                    Emailid = DtCompanyInfo.Rows[0]["Emailid"].ToString();
                    Website = DtCompanyInfo.Rows[0]["Website"].ToString();
                    address1 = dt.Rows[k]["address1"].ToString();
                    State = dt.Rows[k]["State"].ToString();
                    prPincode = dt.Rows[k]["prPincode"].ToString();
                    EmpDtofLeaving = dt.Rows[k]["EmpDtofLeaving"].ToString();
                    address2 = dt.Rows[k]["address"].ToString();

                    EmergencyContNo = dt.Rows[k]["EmergencyContNo"].ToString();


                    PdfPTable IDCarddetails = new PdfPTable(10);
                    IDCarddetails.TotalWidth = 370f;
                    IDCarddetails.LockedWidth = true;
                    float[] width = new float[] { 5f, 2f, 2f, 2f, 0.5f, 5f, 2f, 2f, 2f, 2.4f };
                    IDCarddetails.SetWidths(width);

                    #region for sub table

                    PdfPTable IDCardTemptable1 = new PdfPTable(4);
                    IDCardTemptable1.TotalWidth = 200f;
                    IDCardTemptable1.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
                    IDCardTemptable1.DefaultCell.VerticalAlignment = Element.ALIGN_TOP;
                    //IDCardTemptable1.LockedWidth = true;
                    float[] width1 = new float[] { 2.4f, 2.4f, 2.4f, 2.4f };

                    //float[] width1 = new float[] { 5f, 2f, 2f, 2f };
                    IDCardTemptable1.SetWidths(width1);
                    #region For 1st


                    BaseColor color = new BaseColor(0, 0, 0);



                    if (File.Exists(imagepath5))
                    {
                        iTextSharp.text.Image srflogo = iTextSharp.text.Image.GetInstance(imagepath5);
                        srflogo.ScaleAbsolute(90f, 50f); //80,50
                        PdfPCell companylogo = new PdfPCell();
                        Paragraph cmplogo = new Paragraph();
                        cmplogo.Add(new Chunk(srflogo, 50f, 0f));
                        companylogo.AddElement(cmplogo);
                        companylogo.HorizontalAlignment = 0;
                        companylogo.Colspan = 4;
                        companylogo.PaddingLeft = -18;
                        companylogo.Border = 0;
                        companylogo.PaddingTop = 20;
                        IDCardTemptable1.AddCell(companylogo);
                    }
                    else
                    {
                        PdfPCell companylogo = new PdfPCell();
                        companylogo.HorizontalAlignment = 0;
                        companylogo.Colspan = 4;
                        companylogo.Border = 0;
                        companylogo.FixedHeight = 45;
                        IDCardTemptable1.AddCell(companylogo);
                    }
                    #region commented code for address


                    #endregion commented code for address

                    if (Image.Length > 0)
                    {
                        iTextSharp.text.Image Empphoto = iTextSharp.text.Image.GetInstance(imagepath1 + Image);
                        //Empphoto.ScalePercent(25f);
                        Empphoto.ScaleAbsolute(70f, 80f);
                        PdfPCell EmpImage = new PdfPCell();
                        Paragraph Emplogo = new Paragraph();
                        Emplogo.Add(new Chunk(Empphoto, 50f, 0));
                        EmpImage.AddElement(Emplogo);
                        EmpImage.HorizontalAlignment = 1;
                        EmpImage.Colspan = 4;
                        EmpImage.Border = 0;
                        EmpImage.PaddingLeft = -15;
                        IDCardTemptable1.AddCell(EmpImage);
                    }
                    else
                    {
                        PdfPCell EmpImage = new PdfPCell();
                        EmpImage.HorizontalAlignment = 2;
                        EmpImage.Colspan = 4;
                        EmpImage.Border = 0;
                        EmpImage.FixedHeight = 84;
                        IDCardTemptable1.AddCell(EmpImage);

                    }

                    PdfPCell cellempname = new PdfPCell(new Phrase("Name  ", FontFactory.GetFont(fontstyle, Fontsize2, Font.NORMAL, color)));
                    cellempname.HorizontalAlignment = 0;
                    cellempname.Border = 0;
                    cellempname.PaddingTop = 2f;
                    cellempname.Colspan = 2;
                    IDCardTemptable1.AddCell(cellempname);

                    PdfPCell cellEmpNameval = new PdfPCell(new Phrase(" :" + "  " + Name, FontFactory.GetFont(fontstyle, Fontsize2, Font.NORMAL, color)));
                    cellEmpNameval.HorizontalAlignment = 0;
                    cellEmpNameval.Border = 0;
                    cellEmpNameval.PaddingTop = 2f;
                    cellEmpNameval.Colspan = 2;
                    cellEmpNameval.PaddingLeft = -25f;
                    IDCardTemptable1.AddCell(cellEmpNameval);



                    PdfPCell celldesgn = new PdfPCell(new Phrase("Designation   ", FontFactory.GetFont(fontstyle, Fontsize2, Font.NORMAL, color)));
                    celldesgn.HorizontalAlignment = 0;
                    celldesgn.Border = 0;
                    celldesgn.Colspan = 2;
                    IDCardTemptable1.AddCell(celldesgn);

                    PdfPCell celldesgnval = new PdfPCell(new Phrase(" : " + " " + Designation, FontFactory.GetFont(fontstyle, Fontsize2, Font.NORMAL, color)));
                    celldesgnval.HorizontalAlignment = 0;
                    celldesgnval.Border = 0;
                    celldesgnval.Colspan = 2;
                    celldesgnval.PaddingLeft = -25f;
                    IDCardTemptable1.AddCell(celldesgnval);

                    PdfPCell dobv = new PdfPCell(new Phrase("Blood Group	                ", FontFactory.GetFont(fontstyle, Fontsize2, Font.NORMAL, color)));
                    dobv.HorizontalAlignment = 0;
                    dobv.Border = 0;
                    dobv.Colspan = 2;
                    IDCardTemptable1.AddCell(dobv);

                    PdfPCell dobval = new PdfPCell(new Phrase(" : " + " " + BloodGroup, FontFactory.GetFont(fontstyle, Fontsize2, Font.NORMAL, color)));
                    dobval.HorizontalAlignment = 0;
                    dobval.Border = 0;
                    dobval.Colspan = 2;
                    dobval.PaddingLeft = -25f;
                    IDCardTemptable1.AddCell(dobval);
                    PdfPCell cellempcode = new PdfPCell(new Phrase("Emp ID	           ", FontFactory.GetFont(fontstyle, Fontsize2, Font.NORMAL, color)));
                    cellempcode.HorizontalAlignment = 0;
                    cellempcode.Border = 0;
                    cellempcode.Colspan = 2;
                    IDCardTemptable1.AddCell(cellempcode);

                    PdfPCell cellempcodeval = new PdfPCell(new Phrase(" : " + " " + Empid, FontFactory.GetFont(fontstyle, Fontsize2, Font.NORMAL, color)));
                    cellempcodeval.HorizontalAlignment = 0;
                    cellempcodeval.Border = 0;
                    cellempcodeval.Colspan = 2;
                    cellempcodeval.PaddingLeft = -25f;
                    IDCardTemptable1.AddCell(cellempcodeval);




                    PdfPCell Signature = new PdfPCell(new Phrase("   ", FontFactory.GetFont(fontstyle, Fontsize2, Font.NORMAL, color)));
                    Signature.HorizontalAlignment = 0;
                    Signature.Colspan = 4;
                    //Signature.PaddingTop =2;
                    Signature.Border = 0;
                    // Signature.PaddingLeft = -10f;
                    Signature.FixedHeight = 4;
                    //IDCardTemptable1.AddCell(Signature);


                    #region comment signature

                    if (EmpSign.Length > 0)
                    {
                        iTextSharp.text.Image Sign = iTextSharp.text.Image.GetInstance(imagepath6 + EmpSign);
                        Sign.ScalePercent(8f);
                        Sign.ScaleAbsolute(60f, 15f);
                        Signature = new PdfPCell();
                        Paragraph signlogo = new Paragraph();
                        signlogo.Add(new Chunk(Sign, 23f, -7f));
                        Signature.AddElement(signlogo);
                        Signature.HorizontalAlignment = 0;
                        Signature.Colspan = 2;
                        // Signature.PaddingTop = 1;
                        Signature.PaddingLeft = -10f;
                        Signature.Border = 0;
                        IDCardTemptable1.AddCell(Signature);
                    }
                    else
                    {

                        Signature = new PdfPCell();
                        Signature.HorizontalAlignment = 0;
                        Signature.Colspan = 2;
                        Signature.PaddingTop = 3;
                        Signature.Border = 0;
                        Signature.PaddingLeft = -10f;
                        Signature.FixedHeight = 20;
                        IDCardTemptable1.AddCell(Signature);

                    }

                    iTextSharp.text.Image IssuingAuth = iTextSharp.text.Image.GetInstance(imagepath2);
                    // IssuingAuth.ScalePercent(50f);
                    IssuingAuth.ScaleAbsolute(40f, 20f);
                    PdfPCell Authority = new PdfPCell();
                    Paragraph Authoritylogo = new Paragraph();
                    Authoritylogo.Add(new Chunk(IssuingAuth, 45f, -4f));
                    Authority.AddElement(Authoritylogo);
                    //Authority.HorizontalAlignment = 1;
                    Authority.HorizontalAlignment = Element.ALIGN_CENTER;
                    Authority.Colspan = 4;
                    Authority.Border = 0;
                    Authority.PaddingLeft = -16;
                    Authority.PaddingTop = -1;
                    IDCardTemptable1.AddCell(Authority);
                    #endregion

                    PdfPCell cellAuthority = new PdfPCell(new Phrase("Signature of Authorised", FontFactory.GetFont(fontstyle, Fontsize - 1, Font.NORMAL, color)));
                    //cellAuthority.HorizontalAlignment = 1;
                    cellAuthority.HorizontalAlignment = Element.ALIGN_RIGHT;
                    cellAuthority.Border = 0;
                    cellAuthority.Colspan = 4;
                    cellAuthority.PaddingLeft = 2;
                    cellAuthority.PaddingTop = -5;
                    // cellAuthority.PaddingBottom =0;
                    // cellAuthority.PaddingLeft = 70;
                    IDCardTemptable1.AddCell(cellAuthority);

                    if (File.Exists(imagepath7))
                    {
                        iTextSharp.text.Image Sign1 = iTextSharp.text.Image.GetInstance(imagepath7);
                        Sign1.ScalePercent(8f);
                        Sign1.ScaleAbsolute(163f, 12f);//170f,15f
                        PdfPCell Signature1 = new PdfPCell();
                        Paragraph signlogo1 = new Paragraph();
                        signlogo1.Add(new Chunk(Sign1, 0f, 0f));
                        Signature1.AddElement(signlogo1);
                        Signature1.HorizontalAlignment = 0;
                        Signature1.Colspan = 4;
                        Signature1.PaddingTop = -1f;
                        Signature1.PaddingLeft = -10;

                        Signature1.PaddingBottom = -0.5f; ;
                        Signature1.Border = 0;
                        Signature1.BorderWidthBottom = 0;
                        IDCardTemptable1.AddCell(Signature1);

                    }
                    else
                    {

                        Signature = new PdfPCell();
                        Signature.HorizontalAlignment = 0;
                        Signature.Colspan = 4;
                        Signature.PaddingTop = 5;
                        Signature.Border = 0;
                        Signature.PaddingTop = 6;
                        Signature.PaddingLeft = -10f;
                        Signature.BorderWidthBottom = 0;
                        Signature.FixedHeight = 12;
                        IDCardTemptable1.AddCell(Signature);

                    }

                    #endregion
                    PdfPCell childTable1 = new PdfPCell(IDCardTemptable1);
                    childTable1.HorizontalAlignment = 0;
                    childTable1.Colspan = 4;
                    childTable1.PaddingLeft = 10;
                    IDCarddetails.AddCell(childTable1);
                    #endregion
                    PdfPTable IDCardTemptable41 = new PdfPTable(1);
                    IDCardTemptable41.TotalWidth = 2f; //1f
                    IDCardTemptable41.LockedWidth = true;
                    float[] width41 = new float[] { 2f }; //modified
                    IDCardTemptable41.SetWidths(width41);
                    #region subtable2

                    PdfPCell cellempcell1 = new PdfPCell(new Phrase("", FontFactory.GetFont(fontstyle, Fontsize, Font.NORMAL, BaseColor.BLACK)));
                    cellempcell1.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                    cellempcell1.Border = 0;
                    cellempcell1.Colspan = 1;
                    IDCardTemptable41.AddCell(cellempcell1);
                    #endregion
                    PdfPCell childTable4 = new PdfPCell(IDCardTemptable41);
                    childTable4.HorizontalAlignment = 0;
                    childTable4.Colspan = 1;
                    childTable4.Border = 0;
                    IDCarddetails.AddCell(childTable4);

                    PdfPTable IDCardTemptable2 = new PdfPTable(4);
                    IDCardTemptable2.TotalWidth = 160f; //190
                    //IDCardTemptable2.LockedWidth = true;
                    IDCardTemptable2.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
                    IDCardTemptable2.DefaultCell.VerticalAlignment = Element.ALIGN_TOP;
                    float[] width2 = new float[] { 2.3f, 2.3f, 2.3f, 2.3f };
                    IDCardTemptable2.SetWidths(width2);

                    #region for subtable3


                    #region Present address String array

                    string[] PrAdress = new string[8];

                    if (prlmark.Length > 0)
                    {
                        PrAdress[0] = prlmark + ", ";
                    }
                    else
                    {
                        PrAdress[0] = "";
                    }
                    if (prTown.Length > 0)
                    {
                        PrAdress[1] = prTown + ", ";
                    }
                    else
                    {
                        PrAdress[1] = "";
                    }

                    if (prPostOffice.Length > 0)
                    {
                        PrAdress[2] = prPostOffice + ", ";
                    }
                    else
                    {
                        PrAdress[2] = "";
                    }
                    if (prTaluka.Length > 0)
                    {
                        PrAdress[3] = prTaluka + ", ";
                    }
                    else
                    {
                        PrAdress[3] = " ";
                    }
                    if (prPoliceStation.Length > 0)
                    {
                        PrAdress[4] = prPoliceStation + ", ";
                    }
                    else
                    {
                        PrAdress[4] = " ";
                    }
                    if (prcity.Length > 0)
                    {
                        PrAdress[5] = prcity + ", ";
                    }
                    else
                    {
                        PrAdress[5] = " ";
                    }

                    if (prState.Length > 0)
                    {
                        PrAdress[6] = prState + " ";
                    }
                    else
                    {
                        PrAdress[6] = ".";
                    }


                    if (prPincode.Length > 0)
                    {
                        PrAdress[7] = prPincode + ".";
                    }
                    else
                    {
                        PrAdress[7] = "";
                    }

                    string Address2 = string.Empty;

                    for (int i = 0; i < 8; i++)
                    {
                        address += PrAdress[i];
                    }


                    #endregion


                    //iTextSharp.text.Image watermark = iTextSharp.text.Image.GetInstance(imagepath9);
                    //// IssuingAuth.ScalePercent(50f);
                    //watermark.ScaleAbsolute(100f, 200f);
                    //PdfPCell Authority1 = new PdfPCell();
                    //Paragraph Authoritylogo1 = new Paragraph();
                    //Authoritylogo1.Add(new Chunk(watermark, 45f, -4f));
                    //Authority1.AddElement(Authoritylogo1);
                    ////Authority.HorizontalAlignment = 1;
                    //Authority1.HorizontalAlignment = Element.ALIGN_CENTER;
                    //Authority1.Colspan = 4;
                    //Authority1.PaddingTop = 20f;
                    //Authority1.Border = 0;
                    //Authority1.PaddingLeft = -10;
                    ////Authority.PaddingTop = -12;
                    //IDCardTemptable2.AddCell(Authority1);


                    PdfPCell cellcaddress = new PdfPCell(new Phrase("Emergency No" + ":" + EmergencyContNo, FontFactory.GetFont(fontstyle, Fontsize - 1, Font.NORMAL, color)));
                    cellcaddress.HorizontalAlignment = 1;
                    cellcaddress.Border = 0;
                    cellcaddress.Colspan = 4;
                    cellcaddress.PaddingLeft = -15f;
                    cellcaddress.PaddingTop = 135f;
                    IDCardTemptable2.AddCell(cellcaddress);

                    PdfPCell cellcccomp = new PdfPCell(new Phrase(CompanyName, FontFactory.GetFont(fontstyle, Fontsize - 1, Font.BOLD, color)));
                    cellcccomp.HorizontalAlignment = 1;
                    cellcccomp.Border = 0;
                    cellcccomp.Colspan = 4;
                    cellcccomp.PaddingLeft = -15f;
                    IDCardTemptable2.AddCell(cellcccomp);

                    PdfPCell cellcccompadd = new PdfPCell(new Phrase(Address, FontFactory.GetFont(fontstyle, Fontsize - 3, Font.NORMAL, color)));
                    cellcccompadd.HorizontalAlignment = 1;
                    cellcccompadd.Border = 0;
                    cellcccompadd.Colspan = 4;
                    cellcccompadd.PaddingLeft = -21f;
                    IDCardTemptable2.AddCell(cellcccompadd);

                    PdfPCell cellDtIssuedval = new PdfPCell(new Phrase(Website, FontFactory.GetFont(fontstyle, Fontsize, Font.NORMAL, color)));
                    cellDtIssuedval.HorizontalAlignment = 1;
                    cellDtIssuedval.Border = 0;
                    cellDtIssuedval.Colspan = 4;
                    //cellDtIssuedval.PaddingTop = 10;
                    cellDtIssuedval.PaddingLeft = -15f;
                    cellDtIssuedval.PaddingBottom = 12.5f;
                    IDCardTemptable2.AddCell(cellDtIssuedval);
                    if (File.Exists(imagepath7))
                    {
                        iTextSharp.text.Image Sign1 = iTextSharp.text.Image.GetInstance(imagepath8);
                        Sign1.ScalePercent(8f);
                        Sign1.ScaleAbsolute(163f, 12f);
                        PdfPCell Signature2 = new PdfPCell();
                        Paragraph signlogo2 = new Paragraph();
                        signlogo2.Add(new Chunk(Sign1, 0f, 0f));
                        Signature2.AddElement(signlogo2);
                        Signature2.HorizontalAlignment = 0;
                        Signature2.Colspan = 4;
                        //Signature1.PaddingTop = 5;
                        Signature2.PaddingTop = 15f;
                        Signature2.PaddingLeft = -20;
                        //Signature2.PaddingBottom = 0;
                        Signature2.Border = 0;
                        Signature2.PaddingBottom = -0.5f;
                        Signature2.BorderWidthBottom = 0;
                        IDCardTemptable2.AddCell(Signature2);
                    }
                    else
                    {

                        Signature = new PdfPCell();
                        Signature.HorizontalAlignment = 0;
                        Signature.Colspan = 4;
                        Signature.PaddingTop = 5;
                        Signature.Border = 0;
                        Signature.PaddingTop = 16f;
                        Signature.PaddingLeft = -10f;
                        Signature.BorderWidthBottom = 0;
                        Signature.FixedHeight = 12;
                        IDCardTemptable2.AddCell(Signature);

                    }

                    #endregion for sub table

                    PdfPCell childTable2 = new PdfPCell(IDCardTemptable2);
                    childTable2.HorizontalAlignment = 0;
                    childTable2.Colspan = 4;
                    childTable2.PaddingLeft = 20;
                    IDCarddetails.AddCell(childTable2);


                    PdfPTable IDCardTemptable31 = new PdfPTable(1);
                    IDCardTemptable31.TotalWidth = 2f;
                    IDCardTemptable31.LockedWidth = true;
                    float[] width31 = new float[] { 0.5f };
                    IDCardTemptable31.SetWidths(width31);

                    PdfPCell cellempcell = new PdfPCell(new Phrase("", FontFactory.GetFont(fontstyle, Fontsize, Font.NORMAL, BaseColor.BLACK)));
                    cellempcell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                    cellempcell.Border = 0;
                    cellempcell.Colspan = 1;
                    cellempcell.PaddingBottom = 80;
                    IDCardTemptable31.AddCell(cellempcell);

                    PdfPCell childTable3 = new PdfPCell(IDCardTemptable31);
                    childTable3.HorizontalAlignment = 0;
                    childTable3.Colspan = 1;
                    childTable3.Border = 0;
                    childTable3.PaddingBottom = 30;
                    IDCarddetails.AddCell(childTable3);

                    PdfPCell childTable6 = new PdfPCell();
                    childTable6.HorizontalAlignment = 0;
                    childTable6.Colspan = 10;
                    childTable6.Border = 0;
                    childTable3.PaddingBottom = 10;
                    //childTable6.PaddingBottom = 10;
                    IDCarddetails.AddCell(childTable6);


                    PdfPCell empcellnew1 = new PdfPCell();
                    empcellnew1.HorizontalAlignment = 0;
                    empcellnew1.Colspan = 10;
                    empcellnew1.Border = 0;
                    IDCarddetails.AddCell(empcellnew1);



                    #endregion for range ID Card Display



                    document.Add(IDCarddetails);


                }

                document.Close();
                string filename = "IDCard.pdf";
                //document.Add(MainIDCarddetails);
                //document.Close();


                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment;" + filename);
                Response.Buffer = true;
                Response.Clear();
                Response.OutputStream.Write(ms.GetBuffer(), 0, ms.GetBuffer().Length);
                Response.OutputStream.Flush();
                Response.End();
            }
        }

       

        protected void BtnIDCard_Click(object sender, EventArgs e)
        {
            int Fontsize = 10;
            int Fontsize2 = 10;
            string fontstyle = "Calibri";


            List<String> EmpId_list = new List<string>();

            int totalfonts = FontFactory.RegisterDirectory("c:\\WINDOWS\\fonts");
            StringBuilder sa = new StringBuilder();
            foreach (string fontname in FontFactory.RegisteredFonts)
            {
                sa.Append(fontname + "\n");
            }


            Font FontStyle1 = FontFactory.GetFont("Perpetua Titling MT", BaseFont.CP1252, BaseFont.EMBEDDED, Fontsize - 3, Font.BOLD, BaseColor.BLACK);

            #region for List

            var list = new List<string>();

            for (int i = 0; i < lstEmpIdName.Items.Count; i++)
            {


                if (lstEmpIdName.Items[i].Selected == true)
                {
                    list.Add("'" + lstEmpIdName.Items[i].Value + "'");
                }
            }


            string empids = string.Join(",", list.ToArray());

            #endregion for List

            #region for Variable Declaration

            string Title = "";
            string Empid = "";
            string Name = "";
            string Designation = "";
            string IDcardIssued = "";
            string IDcardvalid = "";
            string BloodGroup = "           ";
            string prTown = "";
            string prPostOffice = "";
            string prTaluka = "";
            string statessndcity = "";
            string prPoliceStation = "";
            string prcity = "";
            string prphone = "";
            string prlmark = "";
            string prLmark = "";
            string prPincode = "";
            string prState = "";
            string State = "";
            string address1 = "";
            string Image = "";
            string EmpSign = "";
            string empdob = "";
            string empdoj = "";
            string address2 = "";

            #endregion for Variable Declaration

            #region

            string QueryCompanyInfo = "select * from companyinfo";
            DataTable DtCompanyInfo = config.ExecuteAdaptorAsyncWithQueryParams(QueryCompanyInfo).Result;

            string CompanyName = "";
            string Address = "";
            string address = "";
            string companyinfo = "";
            string EmpDtofLeaving = "";
            string IDCardValid = "";
            string peTaluka = "";
            string peTown = "";
            string peLmark = "";
            string pearea = "";
            string pecity = "";
            string peDistrict = "";
            string pePincode = "";
            string addres1 = "";
            string peState = "";
            string pelmark = "";
            string branch = "";
            string pestreet = "";
            string pePostOffice = "";
            string pephone = "";
            string pePoliceStation = "";
            string Emailid = "";
            string Website = "";
            string comphone = "";
            string empsex = "";
            string EmergencyContNo = "";
            if (DtCompanyInfo.Rows.Count > 0)
            {
                CompanyName = DtCompanyInfo.Rows[0]["CompanyName"].ToString();
                Address = DtCompanyInfo.Rows[0]["address"].ToString();
                companyinfo = DtCompanyInfo.Rows[0]["CompanyInfo"].ToString();
                Emailid = DtCompanyInfo.Rows[0]["Emailid"].ToString();
                Website = DtCompanyInfo.Rows[0]["Website"].ToString();
                comphone = DtCompanyInfo.Rows[0]["Phoneno"].ToString();

            }

            #endregion

            string query = "";
            DataTable dt = new DataTable();

            query = "select Empid,(EmpFName+' '+EmpMName+''+EmpLName) as Fullname,D.Design as EmpDesgn,empsex,prPostOffice,prPincode,(States.State+Cities.City) as statessndcity,(prTaluka+prPostOffice) as address1,EmpDetails.prLmark,prphone,prState,prcity,EmpDetails.prTaluka,EmpDetails.prTown,States.State,Cities.City,EmpDetails.prPincode,EmpPermanentAddress,(EmpDetails.prcity+EmpDetails.prLmark+EmpDetails.prTaluka+EmpDetails.prTown+States.State+Cities.City+EmpDetails.prPincode+EmpDetails.EmpPresentAddress) as address ," +
                "case convert(varchar(10),EmpDtofBirth,103) when '01/01/1900' then '' else convert(varchar(10),EmpDtofBirth,103) end EmpDtofBirth ," +
                "case convert(varchar(10),EmpDtofJoining,103) when '01/01/1900' then '' else convert(varchar(10),EmpDtofJoining,103) end EmpDtofJoining ," +
                "case convert(varchar(10),EmpDtofLeaving,103) when '01/01/1900' then '' else convert(varchar(10),EmpDtofLeaving,103) end EmpDtofLeaving ," +
                "case convert(varchar(10),IDCardIssued,103) when '01/01/1900' then '' else convert(varchar(10),IDCardIssued,103) end IDCardIssued ," +
                "case convert(varchar(10),IDCardValid,103) when '01/01/1900' then '' else convert(varchar(10),IDCardValid,103) end IDCardValid ," +
                "Image,EmpSign,BN.BloodGroupName as EmpBloodGroup,isnull(EmergencyContNo,'') as EmergencyContNo from EmpDetails " +
                         " inner join designations D on D.Designid=EmpDetails.EmpDesgn " +
                         " left join BloodGroupNames BN on BN.BloodGroupId=EmpDetails.EmpBloodGroup left join Cities on  Cities.CityID= EmpDetails.prCity       LEFT JOIN States on States.StateID=EmpDetails.prState      " +
                         "left join branch b on b.branchid=empdetails.branch" +
                         " where empid  in (" + empids + ")  order by empid";
            dt = config.ExecuteAdaptorAsyncWithQueryParams(query).Result;

            if (dt.Rows.Count > 0)
            {
                MemoryStream ms = new MemoryStream();

                BaseFont bf = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                string imagepath1 = Server.MapPath("assets/EmpPhotos/");
                string imagepath2 = Server.MapPath("assets/Images/sign.jpg");
                string imagepath5 = Server.MapPath("assets/" + CmpIDPrefix + "logo.jpg");
                string imagepath6 = Server.MapPath("assets/EmpPhotos/");
                string imagepath7 = Server.MapPath("assets/Front Broder.jpg");
                string imagepath8 = Server.MapPath("assets/Back Broder.jpg");
                string imagepath9 = Server.MapPath("assets/Back Water Mark.jpg");

                Document document = new Document(new Rectangle(595, 842), 15, 50, 5, 0);
                var writer = PdfWriter.GetInstance(document, ms);
                document.Open();



                #region for range ID Card Display


                for (int k = 0; k < dt.Rows.Count; k++)
                {

                    #region for variable assigning 

                    prlmark = "";
                    prTaluka = "";
                    prTown = "";
                    prphone = "";
                    prcity = "";
                    prPincode = "";
                    peState = "";
                    prPostOffice = "";

                    Empid = dt.Rows[k]["Empid"].ToString();
                    Name = dt.Rows[k]["Fullname"].ToString();
                    empsex = dt.Rows[k]["empsex"].ToString();
                    if (empsex == "M")
                    {
                        Title = "Mr";
                    }
                    else
                    {
                        Title = "Ms";
                    }

                    Designation = dt.Rows[k]["EmpDesgn"].ToString();
                    IDcardIssued = dt.Rows[k]["IDCardIssued"].ToString();
                    IDcardvalid = dt.Rows[k]["IDCardValid"].ToString();
                    BloodGroup = dt.Rows[k]["EmpBloodGroup"].ToString();
                    Image = dt.Rows[k]["Image"].ToString();
                    EmpSign = dt.Rows[k]["EmpSign"].ToString();
                    empdob = dt.Rows[k]["EmpDtofBirth"].ToString();
                    empdoj = dt.Rows[k]["EmpDtofJoining"].ToString();
                    address = dt.Rows[k]["address"].ToString();
                    prlmark = dt.Rows[k]["prLmark"].ToString();
                    prTaluka = dt.Rows[k]["prTaluka"].ToString();
                    prTown = dt.Rows[k]["prTown"].ToString();
                    prphone = dt.Rows[k]["prphone"].ToString();
                    prcity = dt.Rows[k]["City"].ToString();
                    prPincode = dt.Rows[k]["prPincode"].ToString();
                    peState = dt.Rows[k]["State"].ToString();
                    prPostOffice = dt.Rows[k]["prPostOffice"].ToString();
                    Emailid = DtCompanyInfo.Rows[0]["Emailid"].ToString();
                    Website = DtCompanyInfo.Rows[0]["Website"].ToString();
                    address1 = dt.Rows[k]["address1"].ToString();
                    State = dt.Rows[k]["State"].ToString();
                    prPincode = dt.Rows[k]["prPincode"].ToString();
                    EmpDtofLeaving = dt.Rows[k]["EmpDtofLeaving"].ToString();
                    address2 = dt.Rows[k]["address"].ToString();
                    EmergencyContNo = dt.Rows[k]["EmergencyContNo"].ToString();


                    #endregion for variable assigning 


                    //PdfPTable IDCarddetails = new PdfPTable(10);
                    //IDCarddetails.TotalWidth = 370f;
                    //IDCarddetails.LockedWidth = true;
                    //float[] width = new float[] { 1f, 1f, 1f, 1f, 0.5f, 1f, 1f, 1f, 1f , 2.4f };
                    //IDCarddetails.SetWidths(width);

                   

                    PdfPTable IDCardTemptable1 = new PdfPTable(10);
                    IDCardTemptable1.TotalWidth = 150f;
                    float[] width1 = new float[] { 2f,2f, 0.3f, 0.5f, 0.5f, 0.5f, 0.5f,1f,1f,1f };
                    IDCardTemptable1.SetWidths(width1);

                    #region For 1st


                    BaseColor color = new BaseColor(0, 0, 0);

                    //iTextSharp.text.Image watermark = iTextSharp.text.Image.GetInstance(Server.MapPath("assets/watermark.jpg"));
                    //watermark.SetAbsolutePosition(120, 790);
                    //document.Add(watermark);

                    if (File.Exists(imagepath5))
                    {
                        iTextSharp.text.Image srflogo = iTextSharp.text.Image.GetInstance(imagepath5);
                        srflogo.ScaleAbsolute(90f, 50f); //80,50
                        PdfPCell companylogo = new PdfPCell();
                        Paragraph cmplogo = new Paragraph();
                        cmplogo.Add(new Chunk(srflogo, 50f, 0f));
                        companylogo.AddElement(cmplogo);
                        companylogo.HorizontalAlignment = 1;
                        companylogo.Border = 0;
                        companylogo.Rotation = 90;
                        companylogo.Rowspan = 1;
                        IDCardTemptable1.AddCell(companylogo);
                    }
                    else
                    {
                        PdfPCell companylogo = new PdfPCell();
                        companylogo.VerticalAlignment = Element.ALIGN_CENTER;
                        companylogo.HorizontalAlignment = Element.ALIGN_CENTER;
                        companylogo.Border = 0;
                        companylogo.FixedHeight = 45;
                        companylogo.Rotation = 90;
                        companylogo.Rowspan = 1;
                        IDCardTemptable1.AddCell(companylogo);
                    }

                   

                    if (Image.Length > 0)
                    {
                        iTextSharp.text.Image Empphoto = iTextSharp.text.Image.GetInstance(imagepath1 + Image);
                        Empphoto.ScaleAbsolute(70f, 80f);
                        PdfPCell EmpImage = new PdfPCell();
                        Paragraph Emplogo = new Paragraph();
                        Emplogo.Add(new Chunk(Empphoto, 50f, 0));
                        EmpImage.AddElement(Emplogo);
                        EmpImage.HorizontalAlignment = 1;
                        EmpImage.Border = 0;
                        EmpImage.Rotation = 90;
                        EmpImage.PaddingLeft = -30;
                        EmpImage.Rowspan = 1;
                        IDCardTemptable1.AddCell(EmpImage);
                    }
                    else
                    {
                        PdfPCell EmpImage = new PdfPCell();
                        EmpImage.HorizontalAlignment = 2;
                        EmpImage.Border = 0;
                        EmpImage.FixedHeight = 84;
                        EmpImage.Rotation = 90;
                        EmpImage.Rowspan = 1;
                        EmpImage.PaddingLeft = -30;
                        IDCardTemptable1.AddCell(EmpImage);

                    }


                    //var phrase = new Phrase();
                    ////phrase.Add(new Chunk("Name            ", FontFactory.GetFont(fontstyle, Fontsize2, Font.NORMAL, color)));
                    //phrase.Add(new Chunk(Name, FontFactory.GetFont(fontstyle, Fontsize2, Font.BOLD, color)));
                    //PdfPCell cellempname = new PdfPCell();
                    //cellempname.AddElement(phrase);
                    //cellempname.Border = 0;
                    //cellempname.Rotation = 90;
                    //cellempname.Rowspan = 1;
                    //cellempname.PaddingLeft = -40;
                    //cellempname.HorizontalAlignment = Element.ALIGN_CENTER;
                    //IDCardTemptable1.AddCell(cellempname);


                    PdfPCell cellEmpNameval = new PdfPCell(new Phrase(Name, FontFactory.GetFont(fontstyle, Fontsize2, Font.BOLD, color)));
                    cellEmpNameval.HorizontalAlignment = 1;
                    cellEmpNameval.Border = 0;
                    // cellEmpNameval.PaddingTop = 2f;
                    cellEmpNameval.Rowspan = 1;
                    cellEmpNameval.PaddingLeft = -40f;
                    cellEmpNameval.Rotation = 90;
                    IDCardTemptable1.AddCell(cellEmpNameval);


                    var phrase1 = new Phrase();
                    phrase1.Add(new Chunk("Designation  ", FontFactory.GetFont(fontstyle, Fontsize2, Font.NORMAL, color)));
                    phrase1.Add(new Chunk("  :  " + Designation, FontFactory.GetFont(fontstyle, Fontsize2, Font.NORMAL, color)));
                    PdfPCell celldesgn = new PdfPCell();
                    celldesgn.AddElement(phrase1);
                    celldesgn.Border = 0;
                    celldesgn.Rotation = 90;
                    celldesgn.Rowspan = 1;
                    celldesgn.PaddingLeft = -40;
                    celldesgn.HorizontalAlignment = 1;
                    IDCardTemptable1.AddCell(celldesgn);



                    // PdfPCell celldesgnval = new PdfPCell(new Phrase("", FontFactory.GetFont(fontstyle, Fontsize2, Font.NORMAL, color)));
                    // celldesgnval.HorizontalAlignment = 0;
                    // celldesgnval.Border = 0;
                    // celldesgnval.Colspan = 2;
                    // celldesgnval.PaddingLeft = -25f;
                    // celldesgnval.Rotation = 90;
                    // celldesgnval.Rowspan = 1;
                    //// IDCardTemptable1.AddCell(celldesgnval);

                    var phrase2 = new Phrase();
                    phrase2.Add(new Chunk("Blood Group ", FontFactory.GetFont(fontstyle, Fontsize2, Font.NORMAL, color)));
                    phrase2.Add(new Chunk(" :  " + BloodGroup, FontFactory.GetFont(fontstyle, Fontsize2, Font.NORMAL, color)));
                    PdfPCell dobv = new PdfPCell();
                    dobv.AddElement(phrase2);
                    dobv.Border = 0;
                    dobv.Rotation = 90;
                    dobv.Rowspan = 1;
                    dobv.PaddingLeft = -50;
                    dobv.HorizontalAlignment = 1;
                    IDCardTemptable1.AddCell(dobv);

                    //   PdfPCell dobval = new PdfPCell(new Phrase("", FontFactory.GetFont(fontstyle, Fontsize2, Font.NORMAL, color)));
                    //   dobval.HorizontalAlignment = 0;
                    //   dobval.Border = 0;
                    //   //dobval.Colspan = 2;
                    //   dobval.PaddingLeft = -25f;
                    //   dobval.Rotation = 90;
                    //   dobval.Rowspan = 1;
                    ////   IDCardTemptable1.AddCell(dobval);

                    var phrase3 = new Phrase();
                    phrase3.Add(new Chunk("Emp ID           ", FontFactory.GetFont(fontstyle, Fontsize2, Font.NORMAL, color)));
                    phrase3.Add(new Chunk(" :  " + Empid, FontFactory.GetFont(fontstyle, Fontsize2, Font.NORMAL, color)));
                    PdfPCell cellempcode = new PdfPCell();
                    cellempcode.AddElement(phrase3);
                    cellempcode.Border = 0;
                    cellempcode.Rotation = 90;
                    cellempcode.Rowspan = 1;
                    cellempcode.PaddingLeft = -60;
                    cellempcode.HorizontalAlignment = 1;
                    IDCardTemptable1.AddCell(cellempcode);

                    PdfPCell cellempcodeval = new PdfPCell(new Phrase("", FontFactory.GetFont(fontstyle, Fontsize2, Font.NORMAL, color)));
                    cellempcodeval.Border = 0;
                    cellempcodeval.Rotation = 90;
                    cellempcodeval.Rowspan = 1;
                    cellempcodeval.VerticalAlignment = Element.ALIGN_CENTER;
                    cellempcodeval.HorizontalAlignment = Element.ALIGN_LEFT;
                   // IDCardTemptable1.AddCell(cellempcodeval);




                    PdfPCell Signature = new PdfPCell(new Phrase("   ", FontFactory.GetFont(fontstyle, Fontsize2, Font.NORMAL, color)));
                    Signature.HorizontalAlignment = 0;
                    Signature.Colspan = 4;
                    //Signature.PaddingTop =2;
                    Signature.Border = 0;
                    // Signature.PaddingLeft = -10f;
                    Signature.FixedHeight = 4;
                    //IDCardTemptable1.AddCell(Signature);


                    #region comment signature

                    if (EmpSign.Length > 0)
                    {
                        iTextSharp.text.Image Sign = iTextSharp.text.Image.GetInstance(imagepath6 + EmpSign);
                        Sign.ScalePercent(8f);
                        Sign.ScaleAbsolute(60f, 15f);
                        Signature = new PdfPCell();
                        Paragraph signlogo = new Paragraph();
                        signlogo.Add(new Chunk(Sign, 5f, 5f));
                        Signature.AddElement(signlogo);
                        Signature.HorizontalAlignment = 0;
                        Signature.Border = 0;
                        Signature.Rotation = 90;
                        Signature.Rowspan = 1;
                        IDCardTemptable1.AddCell(Signature);
                    }
                    else
                    {

                        Signature = new PdfPCell();
                        Signature.HorizontalAlignment = 0;
                        Signature.PaddingTop = 3;
                        Signature.Border = 0;
                        Signature.FixedHeight = 20;
                        Signature.Rotation = 90;
                        Signature.Rowspan = 1;
                        IDCardTemptable1.AddCell(Signature);

                    }

                    iTextSharp.text.Image IssuingAuth = iTextSharp.text.Image.GetInstance(imagepath2);
                    IssuingAuth.ScaleAbsolute(40f, 20f);
                    PdfPCell Authority = new PdfPCell();
                    Paragraph Authoritylogo = new Paragraph();
                    Authoritylogo.Add(new Chunk(IssuingAuth, 100f, 0f));
                    Authority.AddElement(Authoritylogo);
                    Authority.HorizontalAlignment = 1;
                    Authority.Border = 0;
                    Authority.Rotation = 90;
                    Authority.Rowspan = 1;
                    Authority.PaddingLeft = -80;
                    IDCardTemptable1.AddCell(Authority);

                    #endregion

                    PdfPCell cellAuthority = new PdfPCell(new Phrase("Signature of Authorised", FontFactory.GetFont(fontstyle, Fontsize - 1, Font.NORMAL, color)));
                    //cellAuthority.HorizontalAlignment = 1;
                    cellAuthority.HorizontalAlignment = Element.ALIGN_RIGHT;
                    cellAuthority.Border = 0;
                    cellAuthority.Rotation = 90;
                    cellAuthority.Rowspan = 1;
                    cellAuthority.PaddingLeft = -100;
                    IDCardTemptable1.AddCell(cellAuthority);

                    if (File.Exists(imagepath7))
                    {
                        iTextSharp.text.Image Sign1 = iTextSharp.text.Image.GetInstance(imagepath7);
                        Sign1.ScalePercent(8f);
                        Sign1.ScaleAbsolute(163f, 12f);//170f,15f
                        PdfPCell Signature1 = new PdfPCell();
                        Paragraph signlogo1 = new Paragraph();
                        signlogo1.Add(new Chunk(Sign1, 0f, 0f));
                        Signature1.AddElement(signlogo1);
                        Signature1.HorizontalAlignment = 0;
                        Signature1.Rotation = 90;
                        Signature1.Border = 0;
                        Signature1.BorderWidthBottom = 0;
                        Signature1.PaddingLeft = -130;
                        IDCardTemptable1.AddCell(Signature1);

                    }
                    else
                    {

                        Signature = new PdfPCell();
                        Signature.HorizontalAlignment = 0;
                        Signature.Border = 0;
                        Signature.BorderWidthBottom = 0;
                        Signature.FixedHeight = 12;
                        Signature.Rotation = 90;
                        Signature.PaddingLeft = -130;
                        IDCardTemptable1.AddCell(Signature);

                    }


                    document.Add(IDCardTemptable1);

                    #endregion
                    //PdfPCell childTable1 = new PdfPCell(IDCardTemptable1);
                    //childTable1.HorizontalAlignment = 0;
                    //childTable1.Colspan = 4;
                    //childTable1.PaddingLeft = 10;
                    //IDCarddetails.AddCell(childTable1);

                    #endregion

                    PdfPTable IDCardTemptable41 = new PdfPTable(1);
                    IDCardTemptable41.TotalWidth = 100f; //1f
                    float[] width41 = new float[] { 20f }; //modified
                    IDCardTemptable41.SetWidths(width41);

                    PdfPCell cellempcell1 = new PdfPCell(new Phrase("", FontFactory.GetFont(fontstyle, Fontsize, Font.NORMAL, BaseColor.BLACK)));
                    cellempcell1.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                    cellempcell1.Border = 0;
                    cellempcell1.Rotation = 90;
                    cellempcell1.Rowspan = 1;
                    IDCardTemptable41.AddCell(cellempcell1);

                    document.Add(IDCardTemptable41);

                    

                    PdfPTable IDCardTemptable2 = new PdfPTable(11);
                    IDCardTemptable2.TotalWidth = 200f; 
                    IDCardTemptable2.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
                    IDCardTemptable2.DefaultCell.VerticalAlignment = Element.ALIGN_TOP;
                    float[] width2 = new float[] { 4.5f,1f, 1f, 1f, 1f ,1f,1f,1f,1f,1f,1f};
                    IDCardTemptable2.SetWidths(width2);

                


                    #region Present address String array

                    string[] PrAdress = new string[8];

                    if (prlmark.Length > 0)
                    {
                        PrAdress[0] = prlmark + ", ";
                    }
                    else
                    {
                        PrAdress[0] = "";
                    }
                    if (prTown.Length > 0)
                    {
                        PrAdress[1] = prTown + ", ";
                    }
                    else
                    {
                        PrAdress[1] = "";
                    }

                    if (prPostOffice.Length > 0)
                    {
                        PrAdress[2] = prPostOffice + ", ";
                    }
                    else
                    {
                        PrAdress[2] = "";
                    }
                    if (prTaluka.Length > 0)
                    {
                        PrAdress[3] = prTaluka + ", ";
                    }
                    else
                    {
                        PrAdress[3] = " ";
                    }
                    if (prPoliceStation.Length > 0)
                    {
                        PrAdress[4] = prPoliceStation + ", ";
                    }
                    else
                    {
                        PrAdress[4] = " ";
                    }
                    if (prcity.Length > 0)
                    {
                        PrAdress[5] = prcity + ", ";
                    }
                    else
                    {
                        PrAdress[5] = " ";
                    }

                    if (prState.Length > 0)
                    {
                        PrAdress[6] = prState + " ";
                    }
                    else
                    {
                        PrAdress[6] = ".";
                    }


                    if (prPincode.Length > 0)
                    {
                        PrAdress[7] = prPincode + ".";
                    }
                    else
                    {
                        PrAdress[7] = "";
                    }

                    string Address2 = string.Empty;

                    for (int i = 0; i < 8; i++)
                    {
                        address += PrAdress[i];
                    }


                    #endregion


                    //iTextSharp.text.Image watermark = iTextSharp.text.Image.GetInstance(imagepath9);
                    //// IssuingAuth.ScalePercent(50f);
                    //watermark.ScaleAbsolute(100f, 200f);
                    //PdfPCell Authority1 = new PdfPCell();
                    //Paragraph Authoritylogo1 = new Paragraph();
                    //Authoritylogo1.Add(new Chunk(watermark, 45f, -4f));
                    //Authority1.AddElement(Authoritylogo1);
                    ////Authority.HorizontalAlignment = 1;
                    //Authority1.HorizontalAlignment = Element.ALIGN_CENTER;
                    //Authority1.Colspan = 4;
                    //Authority1.PaddingTop = 20f;
                    //Authority1.Border = 0;
                    //Authority1.PaddingLeft = -10;
                    ////Authority.PaddingTop = -12;
                    //IDCardTemptable2.AddCell(Authority1);


                    PdfPCell cellemp = new PdfPCell(new Phrase("Instructions", FontFactory.GetFont(fontstyle, Fontsize - 1, Font.BOLD|Font.UNDERLINE, color)));
                    cellemp.Border = 0;
                    cellemp.Rotation = 90;
                    cellemp.Rowspan = 1;
                    cellemp.VerticalAlignment = Element.ALIGN_CENTER;
                    cellemp.HorizontalAlignment = Element.ALIGN_CENTER;
                    IDCardTemptable2.AddCell(cellemp);

                    cellemp = new PdfPCell(new Phrase("1. If this card is lost please call "+EmergencyContNo+ " \n and inform", FontFactory.GetFont(fontstyle, Fontsize - 1, Font.NORMAL, color)));
                    cellemp.Border = 0;
                    cellemp.Rotation = 90;
                   cellemp.PaddingLeft = -110f;
                    cellemp.Rowspan = 1;
                    cellemp.HorizontalAlignment = 0;
                    IDCardTemptable2.AddCell(cellemp);

                    cellemp = new PdfPCell(new Phrase("", FontFactory.GetFont(fontstyle, Fontsize - 1, Font.NORMAL, color)));
                    cellemp.Border = 0;
                    cellemp.Rotation = 90;
                    cellemp.PaddingLeft = -200f;
                    cellemp.Rowspan = 1;
                    cellemp.HorizontalAlignment = 0;
                   // IDCardTemptable2.AddCell(cellemp);

                    cellemp = new PdfPCell(new Phrase("2. This card related to the identity of \n the employee. ", FontFactory.GetFont(fontstyle, Fontsize - 1, Font.NORMAL, color)));
                    cellemp.Border = 0;
                    cellemp.Rotation = 90;
                    cellemp.Rowspan = 1;
                   cellemp.PaddingLeft = -110f;
                    cellemp.HorizontalAlignment = 0;
                    IDCardTemptable2.AddCell(cellemp);

                    cellemp = new PdfPCell(new Phrase("3. Card is valid for 1 year from the date \n of issue.", FontFactory.GetFont(fontstyle, Fontsize - 1, Font.NORMAL, color)));
                    cellemp.Border = 0;
                    cellemp.Rotation = 90;
                    cellemp.Rowspan = 1;
                    cellemp.PaddingLeft = -120f;
                    cellemp.HorizontalAlignment = 0;
                    IDCardTemptable2.AddCell(cellemp);

                    cellemp = new PdfPCell(new Phrase("4. This Card is non-transferable and must be \n surrendered immediately at the time \n of Resignation/Termination. ", FontFactory.GetFont(fontstyle, Fontsize - 1, Font.NORMAL, color)));
                    cellemp.Border = 0;
                    cellemp.Rotation = 90;
                    cellemp.Rowspan = 1;
                    cellemp.PaddingLeft = -125f;
                    cellemp.HorizontalAlignment = 0;
                    IDCardTemptable2.AddCell(cellemp);

                    cellemp = new PdfPCell(new Phrase("5. The card should always be displayed by the \n holder while on duty. ", FontFactory.GetFont(fontstyle, Fontsize - 1, Font.NORMAL, color)));
                    cellemp.Border = 0;
                    cellemp.Rotation = 90;
                    cellemp.PaddingLeft = -120f;
                    cellemp.Rowspan = 1;
                    cellemp.HorizontalAlignment = 0;
                    IDCardTemptable2.AddCell(cellemp);

                    PdfPCell cellcaddress = new PdfPCell(new Phrase("Emergency No" + ":" + EmergencyContNo, FontFactory.GetFont(fontstyle, Fontsize - 1, Font.NORMAL, color)));
                    cellcaddress.Border = 0;
                    cellcaddress.Rotation = 90;
                    cellcaddress.Rowspan = 1;
                    cellcaddress.HorizontalAlignment = 1;
                   cellcaddress.PaddingLeft = -100;
                   IDCardTemptable2.AddCell(cellcaddress);

                    PdfPCell cellcccomp = new PdfPCell(new Phrase(CompanyName, FontFactory.GetFont(fontstyle, Fontsize - 1, Font.BOLD, color)));
                    cellcccomp.HorizontalAlignment = 1;
                    cellcccomp.Border = 0;
                    cellcccomp.Rowspan = 1;
                    cellcccomp.Rotation = 90;
                   cellcccomp.PaddingLeft = -150;
                   IDCardTemptable2.AddCell(cellcccomp);

                    PdfPCell cellcccompadd = new PdfPCell(new Phrase(Address, FontFactory.GetFont(fontstyle, Fontsize - 3, Font.NORMAL, color)));
                    cellcccompadd.HorizontalAlignment = 1;
                    cellcccompadd.Border = 0;
                    cellcccompadd.Rowspan = 1;
                    cellcccompadd.Rotation = 90;
                   cellcccompadd.PaddingLeft = -130;
                    IDCardTemptable2.AddCell(cellcccompadd);

                    PdfPCell cellDtIssuedval = new PdfPCell(new Phrase(Website, FontFactory.GetFont(fontstyle, Fontsize, Font.NORMAL, color)));
                    cellDtIssuedval.VerticalAlignment = Element.ALIGN_CENTER;
                    cellDtIssuedval.HorizontalAlignment = Element.ALIGN_CENTER;
                    cellDtIssuedval.Border = 0;
                    cellDtIssuedval.Rowspan = 1;
                    cellDtIssuedval.Rotation = 90;
                   cellDtIssuedval.PaddingLeft = -130;
                    IDCardTemptable2.AddCell(cellDtIssuedval);

                    if (File.Exists(imagepath7))
                    {
                        iTextSharp.text.Image Sign1 = iTextSharp.text.Image.GetInstance(imagepath8);
                        Sign1.ScalePercent(8f);
                        Sign1.ScaleAbsolute(163f, 12f);
                        PdfPCell Signature2 = new PdfPCell();
                        Paragraph signlogo2 = new Paragraph();
                        signlogo2.Add(new Chunk(Sign1, 0f, 0f));
                        Signature2.AddElement(signlogo2);
                        Signature2.HorizontalAlignment = 0;
                        //Signature2.Colspan = 4;
                        Signature2.Border = 0;
                        Signature2.BorderWidthBottom = 0;
                        Signature2.Rowspan = 1;
                        Signature2.Rotation = 90;
                        Signature2.PaddingLeft = -145;
                        IDCardTemptable2.AddCell(Signature2);
                    }
                    else
                    {

                        Signature = new PdfPCell();
                        Signature.HorizontalAlignment = 0;
                        //Signature.Colspan = 4;
                        Signature.PaddingTop = 5;
                        Signature.Border = 0;
                        Signature.PaddingTop = 16f;
                        Signature.PaddingLeft = -10f;
                        Signature.BorderWidthBottom = 0;
                        Signature.FixedHeight = 12;
                        Signature.Rowspan = 1;
                        Signature.Rotation = 90;
                        Signature.PaddingLeft = -130;
                        IDCardTemptable2.AddCell(Signature);

                    }


                    document.Add(IDCardTemptable2);

                    // #endregion for sub table

                    // PdfPCell childTable2 = new PdfPCell(IDCardTemptable2);
                    // childTable2.HorizontalAlignment = 0;
                    // childTable2.Colspan = 4;
                    // childTable2.PaddingLeft = 20;
                    // IDCarddetails.AddCell(childTable2);


                    // PdfPTable IDCardTemptable31 = new PdfPTable(1);
                    // IDCardTemptable31.TotalWidth = 2f;
                    // IDCardTemptable31.LockedWidth = true;
                    // float[] width31 = new float[] { 0.5f };
                    // IDCardTemptable31.SetWidths(width31);

                    // PdfPCell cellempcell = new PdfPCell(new Phrase("", FontFactory.GetFont(fontstyle, Fontsize, Font.NORMAL, BaseColor.BLACK)));
                    // cellempcell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                    // cellempcell.Border = 0;
                    // cellempcell.Colspan = 1;
                    // cellempcell.PaddingBottom = 80;
                    // IDCardTemptable31.AddCell(cellempcell);

                    // PdfPCell childTable3 = new PdfPCell(IDCardTemptable31);
                    // childTable3.HorizontalAlignment = 0;
                    // childTable3.Colspan = 1;
                    // childTable3.Border = 0;
                    // childTable3.PaddingBottom = 30;
                    // IDCarddetails.AddCell(childTable3);

                    // PdfPCell childTable6 = new PdfPCell();
                    // childTable6.HorizontalAlignment = 0;
                    // childTable6.Colspan = 10;
                    // childTable6.Border = 0;
                    // childTable3.PaddingBottom = 10;
                    // //childTable6.PaddingBottom = 10;
                    // IDCarddetails.AddCell(childTable6);


                    // PdfPCell empcellnew1 = new PdfPCell();
                    // empcellnew1.HorizontalAlignment = 0;
                    // empcellnew1.Colspan = 10;
                    // empcellnew1.Border = 0;
                    // IDCarddetails.AddCell(empcellnew1);



                    // #endregion for range ID Card Display



                    // document.Add(IDCarddetails);


                }

                document.Close();



                //byte[] bytes = ms.ToArray();
                //PdfReader reader = new PdfReader(bytes);
                //MemoryStream myMemoryStream = new MemoryStream();
                //Document doc = new Document();

                //var writer1 = PdfWriter.GetInstance(doc, myMemoryStream);
                //doc.Open();
                //PdfContentByte cb1 = writer1.DirectContent;
                //for (int i = 1; i <= 1; i++)
                //{
                //    doc.SetPageSize(reader.GetPageSizeWithRotation(1));
                //    doc.NewPage();
                //    PdfImportedPage page =
                //     writer.GetImportedPage(reader, i);
                //    int rotation = (int)page.ro

                //    cb1.AddTemplate(page, 0, -1f, 1f, 0,
                //    0,
                //    reader.GetPageSizeWithRotation(i).Height);
                //}



                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment;filename=IDCard.pdf");
                Response.Buffer = true;
                Response.Clear();
                Response.OutputStream.Write(ms.GetBuffer(), 0, ms.GetBuffer().Length);
                Response.OutputStream.Flush();
                Response.End();
            }
        }




    }


}