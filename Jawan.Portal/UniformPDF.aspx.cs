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
using System.IO;
using System.Globalization;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Web.Script.Services;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Collections.Generic;
using System.Text;
using Jawan.Portal.DAL;
namespace Jawan.Portal
{
    public partial class UniformPDF : System.Web.UI.Page
    {
        AppConfiguration config = new AppConfiguration();
        GridViewExportUtil gve = new GridViewExportUtil();
        DataTable dt;
        string EmpIDPrefix = "";
        string Fontstyle = "verdana";
        string CmpIDPrefix = "";


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
                        string PID = Session["AccessLevel"].ToString();
                        // DisplayLinks(PID);
                        // PreviligeUsers(Convert.ToInt32(Session["AccessLevel"]));
                        lblDisplayUser.Text = Session["UserId"].ToString();
                        lblcname.Text = SqlHelper.Instance.GetCompanyname();
                        switch (SqlHelper.Instance.GetCompanyValue())
                        {
                            case 0:// Write Omulance Invisible Links
                                break;
                            case 1://Write KLTS Invisible Links
                                //ExpensesReportsLink.Visible = false;
                                break;
                            case 2://write Fames Link
                                // ExpensesReportsLink.Visible = true;
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
                GoToLoginPage();
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

                    break;

                case 4:

                    break;
                case 5:
                    ClientsReportsLink.Visible = false;
                    InventoryReportsLink.Visible = false;
                    ExpensesReportsLink.Visible = false;
                    ActiveEmployeesLink.Visible = false;

                    ClientsLink.Visible = false;
                    CompanyInfoLink.Visible = false;
                    InventoryLink.Visible = true;
                    SettingsLink.Visible = false;


                    break;
                case 6:

                    break;
                default:
                    break;


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

        protected void txtEmpid_TextChanged(object sender, EventArgs e)
        {
            GetEmpName();
            GetLoanNos();
        }

        protected void GetEmpName()
        {
            string Sqlqry = "select (empfname+' '+empmname+' '+emplname) as empname,EmpDesgn from empdetails where empid='" + txtEmpid.Text + "' ";
            DataTable dt = config.ExecuteAdaptorAsyncWithQueryParams(Sqlqry).Result;
            if (dt.Rows.Count > 0)
            {
                try
                {
                    txtName.Text = dt.Rows[0]["empname"].ToString();

                }
                catch (Exception ex)
                {
                    // MessageLabel.Text = ex.Message;
                }
            }
            else
            {
                // MessageLabel.Text = "There Is No Name For The Selected Employee";
            }


        }

        protected void GetEmpid()
        {

            #region  Old Code
            string Sqlqry = "select Empid from empdetails where (empfname+' '+empmname+' '+emplname)  like '" + txtName.Text + "' ";
            DataTable dt = config.ExecuteAdaptorAsyncWithQueryParams(Sqlqry).Result;
            if (dt.Rows.Count > 0)
            {
                try
                {
                    txtEmpid.Text = dt.Rows[0]["Empid"].ToString();

                }
                catch (Exception ex)
                {
                    // MessageLabel.Text = ex.Message;
                }
            }
            else
            {
                // MessageLabel.Text = "There Is No Name For The Selected Employee";
            }
            #endregion // End Old Code


        }

        protected void txtName_TextChanged(object sender, EventArgs e)
        {
            GetEmpid();
            GetLoanNos();
        }

        public void GetLoanNos()
        {
            ddlLoanNos.Items.Clear();
            string qry = "select distinct loanno from EmpResourceDetails  where empid='" + txtEmpid.Text + "'";
            DataTable dt = config.ExecuteAdaptorAsyncWithQueryParams(qry).Result;

            if (dt.Rows.Count > 0)
            {
                ddlLoanNos.DataValueField = "loanno";
                ddlLoanNos.DataTextField = "loanno";
                ddlLoanNos.DataSource = dt;
                ddlLoanNos.DataBind();
            }
            ddlLoanNos.Items.Insert(0, "--Select--");
        }

        protected void ddlLoanNos_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        string Loanno = "";
        protected void btndownload_Click(object sender, EventArgs e)
        {

            if (txtEmpid.Text.Length > 0)
            {

                Loanno = ddlLoanNos.SelectedValue;
                string UniformID = "";



                string qry = "select distinct erd.loanno,erd.price ,erd.qty,r.Price as itemrate,sil.itemname,elm.LoanAmount,elm.PaidAmnt,elm.NoInstalments,erd.uniformid,convert(varchar(10),elm.LoanIssuedDate,103) as LoanIssuedDate from EmpResourceDetails erd inner join invStockItemList sil on erd.ResourceId=sil.itemid inner join EmpLoanMaster elm on elm.empid=erd.empid and elm.LoanNo=erd.loanno inner join Resources R on erd.ResourceId=R.ResourceID where erd.empid='" + txtEmpid.Text + "' and erd.loanno='" + Loanno + "'";
                DataTable dt = config.ExecuteAdaptorAsyncWithQueryParams(qry).Result;
              
                string LoanIssuedDate = "";
                int slipsCount = 0;
                    if (dt.Rows.Count > 0)
                    {


                        MemoryStream ms = new MemoryStream();

                        Document document = new Document(PageSize.A4);
                        PdfWriter writer = PdfWriter.GetInstance(document, ms);
                        document.Open();
                        document.AddTitle("FaMS");
                        document.AddAuthor("DIYOS");
                        document.AddSubject("Wage Slips");
                        document.AddKeywords("Keyword1, keyword2, …");//
                        string strQry = "Select * from CompanyInfo  where   ClientidPrefix='" + CmpIDPrefix + "'";
                        DataTable compInfo = config.ExecuteAdaptorAsyncWithQueryParams(strQry).Result;
                        string companyName = "Your Company Name";
                        string companyAddress = "Your Company Address";
                        if (compInfo.Rows.Count > 0)
                        {
                            companyName = compInfo.Rows[0]["CompanyName"].ToString();
                            companyAddress = compInfo.Rows[0]["Address"].ToString();
                        }


                    for (int k = 0; k < 2; k++)
                    {

                        PdfPTable Maintable = new PdfPTable(4);
                        Maintable.TotalWidth = 500f;
                        Maintable.LockedWidth = true;
                        float[] width = new float[] { 1.5f, 2f, 2.5f, 1f };
                        Maintable.SetWidths(width);
                        uint FONT_SIZE = 10;
                    #region  Table Headings

                        LoanIssuedDate = dt.Rows[0]["LoanIssuedDate"].ToString();
                        string imagepath1 = Server.MapPath("~/assets/JGSlogoBW.jpg");
                        // PdfPCell Heading = new PdfPCell(new Phrase(companyName, FontFactory.GetFont(Fontstyle, FONT_SIZE+6, Font.BOLD, BaseColor.BLACK)));
                        // Heading.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                        // Heading.Border = 0;
                        // Heading.Colspan = 4;
                        // Heading.PaddingLeft = 30;
                        // //Heading.PaddingTop = -40;
                        // // Heading.SetLeading(0f, 1.2f);
                        // Maintablehead.AddCell(Heading);

                        // if (File.Exists(imagepath1))
                        // {
                        //     //iTextSharp.text.Image gif2 = iTextSharp.text.Image.GetInstance(imagepath1);

                        //     //gif2.Alignment = (iTextSharp.text.Image.ALIGN_LEFT | iTextSharp.text.Image.UNDERLYING);
                        //     //// gif2.SpacingBefore = 50;
                        //     //gif2.ScalePercent(10f);
                        //     //gif2.SetAbsolutePosition(50f, 780f);

                        //     //document.Add(new Paragraph(" "));
                        //     //document.Add(gif2);

                        //     iTextSharp.text.Image icici = iTextSharp.text.Image.GetInstance(imagepath1);
                        //     icici.Alignment = (iTextSharp.text.Image.ALIGN_RIGHT | iTextSharp.text.Image.UNDERLYING);
                        //     icici.SpacingAfter = 50;
                        //     icici.ScaleAbsolute(100f, 70f);
                        //   //  icici.SetAbsolutePosition(50f, 780f);
                        //     PdfPCell companylogo = new PdfPCell();
                        //     Paragraph cmplogo = new Paragraph();
                        //     cmplogo.Add(new Chunk(icici, 0, 0));
                        //     document.Add(new Paragraph(" "));
                        //     document.Add(icici);
                        //    // companylogo.PaddingLeft = 220;
                        //    //// companylogo.PaddingTop = -5;
                        //    // companylogo.AddElement(cmplogo);
                        //    // companylogo.HorizontalAlignment = 2;
                        //    // companylogo.Colspan = 2;
                        //    // companylogo.Border = 0;
                        //    // Maintablehead.AddCell(companylogo);
                        // }

                        // Heading = new PdfPCell(new Phrase(companyAddress, FontFactory.GetFont(Fontstyle, FONT_SIZE-1, Font.NORMAL, BaseColor.BLACK)));
                        // Heading.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                        // Heading.Colspan = 4;
                        // Heading.Border = 0;
                        // Heading.PaddingLeft =0;
                        // Heading.PaddingTop = 5;
                        // // Heading.SetLeading(0f, 1.2f);
                        // Maintablehead.AddCell(Heading);


                        // //string imagepath = Server.MapPath("~/assets/BillLogo1.png");

                        // // Heading.PaddingTop = 5;
                        // // Heading.SetLeading(0f, 1.2f);
                        //// Maintablehead.AddCell(Heading);


                        // document.Add(Maintablehead);

                        PdfPTable table = new PdfPTable(6);
                        table.TotalWidth = 570f;
                        table.LockedWidth = true;
                        float[] width1 = new float[] { 1.5f, 2f, 2f, 2f, 1.5f, 2f };
                        table.SetWidths(width1);

                        PdfPCell cellspace = new PdfPCell(new Phrase("  ", FontFactory.GetFont(Fontstyle, FONT_SIZE - 2, Font.BOLD, BaseColor.BLACK)));
                        cellspace.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                        cellspace.Colspan = 6;
                        cellspace.Border = 0;
                        cellspace.PaddingTop = -5;

                        PdfPCell cellHead = new PdfPCell(new Phrase(companyName, FontFactory.GetFont(Fontstyle, FONT_SIZE +6, Font.BOLD, BaseColor.BLACK)));
                        cellHead.HorizontalAlignment = 0;
                        cellHead.Colspan = 4;
                        cellHead.Border = 0;
                        cellHead.SetLeading(0f, 1.0f);
                        cellHead.PaddingTop = 10;
                        cellHead.PaddingLeft =38;
                        table.AddCell(cellHead);


                        iTextSharp.text.Image icici = iTextSharp.text.Image.GetInstance(imagepath1);
                        icici.ScalePercent(8f);
                        //icici.Alignment = (iTextSharp.text.Image.ALIGN_RIGHT | iTextSharp.text.Image.UNDERLYING);
                        icici.ScaleAbsolute(150f, 90f);
                        PdfPCell companylogo = new PdfPCell();
                        Paragraph cmplogo = new Paragraph();
                        cmplogo.Add(new Chunk(icici, 0, 0));
                        companylogo.PaddingLeft = -10;
                        companylogo.AddElement(cmplogo);
                        companylogo.HorizontalAlignment = 0;
                        companylogo.Colspan = 2;
                        companylogo.Border = 0;
                        table.AddCell(companylogo);



                        cellHead = new PdfPCell(new Phrase(companyAddress, FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                        cellHead.HorizontalAlignment = 1;
                        cellHead.Colspan = 6;
                        cellHead.Border = 0;
                        cellHead.SetLeading(0f, 1.2f);
                        cellHead.PaddingTop = -60;
                        cellHead.PaddingLeft = -170;
                        //cellHead.PaddingLeft =124;
                        table.AddCell(cellHead);
                        document.Add(table);

                        PdfPCell employerAddress1 = new PdfPCell(new Phrase("", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                        employerAddress1.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                        employerAddress1.Border = 0;
                        employerAddress1.Colspan = 4;
                        // employerAddress1.PaddingBottom = 10;
                        Maintable.AddCell(employerAddress1);
                        Maintable.AddCell(employerAddress1);

                       PdfPCell  Heading1 = new PdfPCell(new Phrase("UNIFORM ISSUES", FontFactory.GetFont(Fontstyle, 10, Font.BOLD, BaseColor.BLACK)));
                        Heading1.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                        Heading1.Colspan = 4;
                        Heading1.Border = 0;// 15;
                                           // Heading.PaddingTop =-15;
                        Heading1.PaddingLeft =60;
                        Maintable.AddCell(Heading1);

                        PdfPCell cell = new PdfPCell(new Phrase(" ", FontFactory.GetFont(Fontstyle, 10, Font.BOLD, BaseColor.BLACK)));
                        cell.Colspan = 5;
                        cell.Border = 0;
                        Maintable.AddCell(cell);

                        PdfPCell empcode = new PdfPCell(new Phrase("Emp Code            : ", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                        empcode.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                        empcode.Colspan = 1;
                        empcode.Border = 0;
                        Maintable.AddCell(empcode);

                        PdfPCell empcode1 = new PdfPCell(new Phrase(txtEmpid.Text, FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                        empcode1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                        empcode1.Colspan = 1;
                        empcode1.Border = 0;
                        Maintable.AddCell(empcode1);

                        PdfPCell IssueRefNo = new PdfPCell(new Phrase("     Issue Ref No : ", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                        IssueRefNo.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                        IssueRefNo.Colspan = 1;
                        IssueRefNo.Border = 0;
                        Maintable.AddCell(IssueRefNo);


                        UniformID = dt.Rows[0]["uniformid"].ToString();

                        PdfPCell IssueRefNo1 = new PdfPCell(new Phrase(UniformID, FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                        IssueRefNo1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                        IssueRefNo1.Colspan = 1;
                        IssueRefNo1.Border = 0;
                        Maintable.AddCell(IssueRefNo1);

                        PdfPCell EmployeeName = new PdfPCell(new Phrase("Employee Name  : ", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                        EmployeeName.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                        EmployeeName.Colspan = 1;
                        EmployeeName.Border = 0;
                        Maintable.AddCell(EmployeeName);

                        PdfPCell EmployeeName1 = new PdfPCell(new Phrase(txtName.Text, FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                        EmployeeName1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                        EmployeeName1.Colspan = 1;
                        EmployeeName1.Border = 0;
                        Maintable.AddCell(EmployeeName1);

                        PdfPCell IssueDate = new PdfPCell(new Phrase("Issue Date  : ", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                        IssueDate.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right 
                        IssueDate.Colspan = 1;
                        IssueDate.Border = 0;
                        Maintable.AddCell(IssueDate);

                        PdfPCell IssueDate1 = new PdfPCell(new Phrase(LoanIssuedDate, FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                        IssueDate1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right 
                        IssueDate1.Colspan = 1;
                        IssueDate1.Border = 0;
                        Maintable.AddCell(IssueDate1);


                        PdfPCell cspace = new PdfPCell(new Phrase("", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                        cspace.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right 
                        cspace.Colspan = 2;
                        cspace.Border = 0;
                        Maintable.AddCell(cspace);
                        Maintable.AddCell(cspace);
                        Maintable.AddCell(cspace);


                        document.Add(Maintable);

                        #endregion

                        #region Table Data

                        PdfPTable DetailsTable = new PdfPTable(5);
                        DetailsTable.TotalWidth = 500f;
                        DetailsTable.LockedWidth = true;
                        float[] DetailsWidth = new float[] { 0.5f, 2f, 0.5f, 0.5f, 0.5f };
                        DetailsTable.SetWidths(DetailsWidth);


                        PdfPCell Series = new PdfPCell(new Phrase("S.No", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                        Series.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                        DetailsTable.AddCell(Series);


                        PdfPCell ItemCode = new PdfPCell(new Phrase("Item Code ", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                        ItemCode.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                                                          // DetailsTable.AddCell(ItemCode);


                        PdfPCell ItemDesc = new PdfPCell(new Phrase("Item Description ", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                        ItemDesc.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                        DetailsTable.AddCell(ItemDesc);



                        PdfPCell ItemRate = new PdfPCell(new Phrase("Item Rate ", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                        ItemRate.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                        DetailsTable.AddCell(ItemRate);


                        PdfPCell Quantity = new PdfPCell(new Phrase("Quantity", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                        Quantity.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                        DetailsTable.AddCell(Quantity);



                        PdfPCell LineAmt = new PdfPCell(new Phrase("Amount", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                        LineAmt.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                        DetailsTable.AddCell(LineAmt);



                        int j = 1;
                        string ItemDescription = "";
                        float Itemrate = 0;
                        float quantity = 0;
                        float Lineamt = 0;
                        float TotalLineAmt = 0;

                        float TotalAmountreceived = 0;
                        float TotalAmountdue = 0;
                        float NoOfinstalments = 0;

                        float Amountreceived = 0;
                        float Amountdue = 0;

                        string InWords = "";


                        for (int i = 0; i < dt.Rows.Count; i++)
                        {

                            ItemDescription = dt.Rows[i]["itemname"].ToString();
                            Itemrate = Convert.ToSingle(dt.Rows[i]["itemrate"].ToString());
                            quantity = Convert.ToSingle(dt.Rows[i]["qty"].ToString());
                            Lineamt = (Convert.ToSingle(dt.Rows[i]["price"].ToString()) * Convert.ToSingle(dt.Rows[i]["qty"].ToString()));


                            Amountreceived = Convert.ToSingle(dt.Rows[i]["LoanAmount"].ToString());
                            Amountdue = Convert.ToSingle(dt.Rows[i]["PaidAmnt"].ToString());
                            NoOfinstalments = Convert.ToSingle(dt.Rows[i]["NoInstalments"].ToString());


                            InWords = NumberToEnglish.Instance.changeNumericToWords(Amountreceived.ToString());

                            PdfPCell Series1 = new PdfPCell(new Phrase(j.ToString(), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                            Series1.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                            DetailsTable.AddCell(Series1);


                            PdfPCell ItemCode1 = new PdfPCell(new Phrase("", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                            ItemCode1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                                                               //DetailsTable.AddCell(ItemCode1);


                            PdfPCell ItemDesc1 = new PdfPCell(new Phrase(ItemDescription, FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                            ItemDesc1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            DetailsTable.AddCell(ItemDesc1);



                            PdfPCell ItemRate1 = new PdfPCell(new Phrase(Itemrate.ToString(), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                            ItemRate1.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                            DetailsTable.AddCell(ItemRate1);


                            PdfPCell Quantity1 = new PdfPCell(new Phrase(quantity.ToString(), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                            Quantity1.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                            DetailsTable.AddCell(Quantity1);



                            PdfPCell LineAmt1 = new PdfPCell(new Phrase(Lineamt.ToString(), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                            LineAmt1.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                            DetailsTable.AddCell(LineAmt1);


                            TotalLineAmt += Lineamt;
                            // TotalAmountreceived += Amountreceived;
                            //TotalAmountdue += Amountdue;

                            j++;
                        }


                        #endregion

                        document.Add(DetailsTable);


                        PdfPTable DetailsTable2 = new PdfPTable(4);
                        DetailsTable2.TotalWidth = 500f;
                        DetailsTable2.LockedWidth = true;
                        float[] DetailsWidth2 = new float[] { 2f, 2f, 3f, 1f };
                        DetailsTable2.SetWidths(DetailsWidth2);


                        PdfPCell Noofinstalments = new PdfPCell(new Phrase("No.of Instalments:   " + NoOfinstalments.ToString(), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                        Noofinstalments.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                        Noofinstalments.Border = 0;
                        Noofinstalments.Colspan = 4;
                        // DetailsTable2.AddCell(Noofinstalments);


                        //DetailsTable1.AddCell(cellemp);
                        PdfPCell totalamt = new PdfPCell(new Phrase("Total Amount :", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                        totalamt.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                        totalamt.Border = 0;
                        totalamt.Colspan = 3;
                        DetailsTable2.AddCell(totalamt);

                        PdfPCell totalamts1 = new PdfPCell(new Phrase(TotalLineAmt.ToString(), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                        totalamts1.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                        totalamts1.Border = 0;
                        totalamts1.Colspan = 1;
                        DetailsTable2.AddCell(totalamts1);



                        PdfPCell AmountReceived = new PdfPCell(new Phrase("Advance Paid :", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                        AmountReceived.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                        AmountReceived.Border = 0;
                        AmountReceived.Colspan = 3;
                        // AmountReceived.PaddingLeft = 10;
                        DetailsTable2.AddCell(AmountReceived);


                        PdfPCell totalamt1 = new PdfPCell(new Phrase(Amountdue.ToString(), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                        totalamt1.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                        totalamt1.Border = 0;
                        totalamt1.Colspan = 1;
                        DetailsTable2.AddCell(totalamt1);

                        PdfPCell AmountDue = new PdfPCell(new Phrase("Amount Due :", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                        AmountDue.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                        AmountDue.Border = 0;
                        AmountDue.Colspan = 3;
                        // AmountDue.PaddingLeft = 10;
                        DetailsTable2.AddCell(AmountDue);

                        PdfPCell AmountReceived1 = new PdfPCell(new Phrase(Amountreceived.ToString(), FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                        AmountReceived1.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                        AmountReceived1.Border = 0;
                        AmountReceived1.Colspan = 1;
                        DetailsTable2.AddCell(AmountReceived1);


                        document.Add(DetailsTable2);

                        PdfPTable DetailsTable1 = new PdfPTable(5);
                        DetailsTable1.TotalWidth = 500f;
                        DetailsTable1.LockedWidth = true;
                        float[] DetailsWidth1 = new float[] { 1f, 1f, 1f, 1f, 1f };
                        DetailsTable1.SetWidths(DetailsWidth1);


                        PdfPCell AmountDue1 = new PdfPCell(new Phrase("", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                        AmountDue1.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                        AmountDue1.Border = 0;
                        AmountDue1.Colspan = 5;
                        DetailsTable1.AddCell(AmountDue1);

                        PdfPCell Amountinwords = new PdfPCell(new Phrase("Amount in Words: " + InWords.Trim() + " Only \n\n\n", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                        Amountinwords.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                        Amountinwords.Border = 0;
                        Amountinwords.Colspan = 6;
                        DetailsTable1.AddCell(Amountinwords);


                        PdfPCell PreparedBy = new PdfPCell(new Phrase("Prepared by\n\n\n\n\n", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                        PreparedBy.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                        PreparedBy.Border = 0;
                        PreparedBy.Colspan = 2;
                        //DetailsTable1.AddCell(PreparedBy);


                        PdfPCell IssuedBy = new PdfPCell(new Phrase("Issued by\n\n\n\n\n", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                        IssuedBy.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                        IssuedBy.Border = 0;
                        IssuedBy.Colspan = 4;
                        DetailsTable1.AddCell(IssuedBy);


                        PdfPCell ReceivedBy = new PdfPCell(new Phrase("Received by", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                        ReceivedBy.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                        ReceivedBy.Border = 0;
                        ReceivedBy.Colspan = 2;
                        DetailsTable1.AddCell(ReceivedBy);


                        PdfPCell SRActionedBy = new PdfPCell(new Phrase("S.R.Actioned by", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                        SRActionedBy.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                        SRActionedBy.Border = 0;
                        SRActionedBy.Colspan = 3;
                        // DetailsTable1.AddCell(SRActionedBy);

                        PdfPCell CAuthority = new PdfPCell(new Phrase("Recovery Actioned in Sys. by ", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                        CAuthority.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                        CAuthority.Border = 0;
                        CAuthority.Colspan = 3;
                        //DetailsTable1.AddCell(CAuthority);
                       PdfPCell Ccell = new PdfPCell(new Phrase(" ", FontFactory.GetFont(Fontstyle, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                        Ccell.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                        Ccell.Border = 0;
                        Ccell.FixedHeight = 30;
                        Ccell.Colspan = 5;
                        DetailsTable1.AddCell(Ccell);


                        document.Add(DetailsTable1);
                        //if (chkpdf.Checked == true)
                        //{
                        //    document.NewPage();
                        //}
                        if(dt.Rows.Count>4)
                        {
                            document.NewPage();
                        }
                    }
                       

                        document.Close();
                    
                        Response.ContentType = "application/pdf";
                        Response.AddHeader("content-disposition", "attachment;filename=UniformPDF.pdf");
                        Response.Buffer = true;
                        Response.Clear();
                        Response.OutputStream.Write(ms.GetBuffer(), 0, ms.GetBuffer().Length);
                        Response.OutputStream.Flush();
                        Response.End();


                    }
                }
            
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Please select employee to download Uniform Resources statement');", true);

            }
        }

   
    }
}