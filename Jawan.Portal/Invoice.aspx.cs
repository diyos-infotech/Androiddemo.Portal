using System;
using System.Collections;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using System.Data;
using KLTS.Data;
using Jawan.Portal.DAL;
namespace Jawan.Portal
{
    public partial class Invoice : System.Web.UI.Page
    {
        AppConfiguration config = new AppConfiguration();
        GridViewExportUtil gve = new GridViewExportUtil();
        DataTable dt;
        static double totalamount;

        static int i = 1;
        static int j = 1;
        static int k = 1;
        protected void Page_Load(object sender, EventArgs e)
        {

            //  lbltotalamount.Visible = false;
            // btninvoice.Visible = false;




            if (!IsPostBack)
            {
                if (Session["UserId"] != null && Session["AccessLevel"] != null)
                {
                    PreviligeUsers(Convert.ToInt32(Session["AccessLevel"]));
                    lblDisplayUser.Text = Session["UserId"].ToString();
                    lblcname.Text = SqlHelper.Instance.GetCompanyname();

                }
                else
                {
                    Response.Redirect("login.aspx");
                }


                string selectquerydesgn = "select design from designations";
                DataTable dtdesgn = config.ExecuteAdaptorAsyncWithQueryParams(selectquerydesgn).Result;

                int rowindx = 0;
                for (rowindx = 0; rowindx < dtdesgn.Rows.Count; rowindx++)
                {
                    ddldesgn.Items.Add(dtdesgn.Rows[rowindx]["design"].ToString());
                }




                int date = DateTime.Now.Month;
                int month = 1;
                for (month = 1; month < 13; month++)
                {
                    if (month <= date)
                    {
                        i++;

                    }
                }
                i--;

                for (k = i; j <= 1; j++)
                {
                    switch (k)
                    {
                        case 1:
                            ddlmonth.Items.Add("January");
                            break;
                        case 2: ddlmonth.Items.Add("Feburavary");
                            break;
                        case 3: ddlmonth.Items.Add("March");
                            break;

                        case 4: ddlmonth.Items.Add("April");
                            break;

                        case 5: ddlmonth.Items.Add("May");
                            break;
                        case 6:
                            ddlmonth.Items.Add("June");
                            break;
                        case 7: ddlmonth.Items.Add("July");
                            break;
                        case 8: ddlmonth.Items.Add("August");
                            break;

                        case 9: ddlmonth.Items.Add("September");
                            break;

                        case 10: ddlmonth.Items.Add("October");

                            break;
                        case 11: ddlmonth.Items.Add("Navamber");
                            break;

                        case 12: ddlmonth.Items.Add("December");

                            break;


                    }

                    k = k - 1;
                }

                string selectclientid = "select clientid from clients";
                dt = config.ExecuteAdaptorAsyncWithQueryParams(selectclientid).Result;
                int rowindex = 0;
                for (rowindex = 0; rowindex < dt.Rows.Count; rowindex++)
                {
                    ddlclientid.Items.Add(dt.Rows[rowindex]["clientid"].ToString());

                }

                // ddlmonth.Items.Add( DateTime.Now.Month.ToString);
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

                    break;
                default:
                    break;
            }
        }

        protected void btninvoice_Click(object sender, EventArgs e)
        {


            try
            {
                MemoryStream ms = new MemoryStream();
                string selectquery = "select * from companyinfo";
                dt = config.ExecuteAdaptorAsyncWithQueryParams(selectquery).Result;

                Document document = new Document(PageSize.LEGAL);
                PdfWriter writer = PdfWriter.GetInstance(document, ms);
                document.Open();
                BaseFont bf = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                document.AddTitle("K.L.TECHNICAL SERVICES");
                document.AddAuthor("APSFDC");
                document.AddSubject("Invoice");
                document.AddKeywords("Keyword1, keyword2, …");
                PdfPTable tablelogo = new PdfPTable(2);
                tablelogo.TotalWidth = 500f;
                tablelogo.LockedWidth = true;
                float[] widtlogo = new float[] { 2f, 2f };
                tablelogo.SetWidths(widtlogo);


                PdfPCell celll = new PdfPCell(new Paragraph(" ", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, Font.NORMAL, BaseColor.BLACK)));
                celll.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                celll.Border = 0;
                celll.Colspan = 2;
                tablelogo.AddCell(celll);
                document.Add(new Paragraph("                                           K.L.TECHNICAL SERVICES    ", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 15, Font.BOLD, BaseColor.BLACK)));
                tablelogo.AddCell(celll);

                document.Add(new Paragraph("                                                                              #1-8-304 to 307/9, Road No.1,Pattigada road Secundrabad-3   ", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 8, Font.BOLD, BaseColor.BLACK)));
                tablelogo.AddCell(celll);
                tablelogo.AddCell(celll);
                tablelogo.AddCell(celll);
                tablelogo.AddCell(celll);
                tablelogo.AddCell(celll);
                tablelogo.AddCell(celll);
                tablelogo.AddCell(celll);
                tablelogo.AddCell(celll);
                tablelogo.AddCell(celll);
                tablelogo.AddCell(celll);
                tablelogo.AddCell(celll);
                tablelogo.AddCell(celll);
                tablelogo.AddCell(celll);
                tablelogo.AddCell(celll);
                tablelogo.AddCell(celll);
                tablelogo.AddCell(celll);
                tablelogo.AddCell(celll);
                tablelogo.AddCell(celll);
                document.Add(new Paragraph("                                                                     INVOICE ", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, Font.BOLD, BaseColor.BLACK)));
                //PdfPCell celldate = new PdfPCell(new Phrase("INVOICE", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, Font.NORMAL, BaseColor.BLACK)));
                // celldate.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                //celldate.Border = 0;

                //tablelogo.AddCell(celldate);
                //tablelogo.AddCell(celll);
                //document.Add(celll);
                //document.Add(celll);
                //document.Add(celll);

                PdfPTable address = new PdfPTable(2);
                address.TotalWidth = 500f;
                address.LockedWidth = true;
                float[] addreslogo = new float[] { 1f, 1f };
                address.SetWidths(addreslogo);

                PdfPCell cell11 = new PdfPCell(new Paragraph("To,", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, Font.NORMAL, BaseColor.BLACK)));
                cell11.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                cell11.Border = 0;
                address.AddCell(cell11);
                string selectclientaddress = "select * from clients where clientid= '" + ddlclientid.SelectedItem.ToString() + "'";

                DataTable dtclientaddress = config.ExecuteAdaptorAsyncWithQueryParams(selectclientaddress).Result;




                // PdfPCell cell12 = new PdfPCell( new  Paragraph( )  )

                PdfPCell cell12 = new PdfPCell(new Paragraph("Invcoice No:K.L/1213/HYD " + DateTime.Now.Year, FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, Font.NORMAL, BaseColor.BLACK)));
                cell12.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                cell12.Border = 0;
                // cell12.Colspan = 2;
                address.AddCell(cell12);


                PdfPCell clientname = new PdfPCell(new Paragraph(dtclientaddress.Rows[0]["clientname"].ToString(), FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, Font.BOLD, BaseColor.BLACK)));
                clientname.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                clientname.Colspan = 0;
                clientname.Border = 0;
                address.AddCell(clientname);

                PdfPCell cell13 = new PdfPCell(new Paragraph("Date: " + DateTime.Now.ToString("MMMM") + " " + DateTime.Now.Day + " - " + DateTime.Now.Year, FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, Font.NORMAL, BaseColor.BLACK)));
                cell13.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                cell13.Border = 0;
                cell13.Colspan = 2;
                address.AddCell(cell13);

                PdfPCell clientaddrhno = new PdfPCell(new Paragraph(dtclientaddress.Rows[0]["ClientAddrHno"].ToString(), FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, Font.BOLD, BaseColor.BLACK)));
                clientaddrhno.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                clientaddrhno.Colspan = 0;
                clientaddrhno.Border = 0;
                address.AddCell(clientaddrhno);


                PdfPCell cell14 = new PdfPCell(new Paragraph("For Month: " + DateTime.Now.Day + " - " + DateTime.Now.Year + "      ", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, Font.NORMAL, BaseColor.BLACK)));
                cell14.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                cell14.Border = 0;
                cell14.Colspan = 2;
                address.AddCell(cell14);

                PdfPCell clientstreet = new PdfPCell(new Paragraph(dtclientaddress.Rows[0]["ClientAddrStreet"].ToString(), FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, Font.BOLD, BaseColor.BLACK)));
                clientstreet.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                clientstreet.Colspan = 0;
                clientstreet.Border = 0;
                address.AddCell(clientstreet);



                PdfPCell cell15 = new PdfPCell(new Paragraph("Due Date:           ", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, Font.NORMAL, BaseColor.BLACK)));
                cell15.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                cell15.Border = 0;
                cell15.Colspan = 2;
                address.AddCell(cell15);


                PdfPCell clientcolony = new PdfPCell(new Paragraph(dtclientaddress.Rows[0]["ClientAddrColony"].ToString(), FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, Font.BOLD, BaseColor.BLACK)));
                clientcolony.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                clientcolony.Colspan = 2;
                clientcolony.Border = 0;
                address.AddCell(clientcolony);

                PdfPCell clientcity = new PdfPCell(new Paragraph(dtclientaddress.Rows[0]["ClientAddrcity"].ToString(), FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, Font.BOLD, BaseColor.BLACK)));
                clientcity.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                clientcity.Colspan = 2;
                clientcity.Border = 0;
                address.AddCell(clientcity);

                PdfPCell clientstate = new PdfPCell(new Paragraph(dtclientaddress.Rows[0]["ClientAddrstate"].ToString(), FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, Font.BOLD, BaseColor.BLACK)));
                clientstate.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                clientstate.Colspan = 2;
                clientstate.Border = 0;
                address.AddCell(clientstate);


                PdfPCell clietnpin = new PdfPCell(new Paragraph(dtclientaddress.Rows[0]["ClientAddrpin"].ToString(), FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, Font.BOLD, BaseColor.BLACK)));
                clietnpin.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                clietnpin.Colspan = 2;
                clietnpin.Border = 0;
                address.AddCell(clietnpin);
                address.AddCell(celll);

                document.Add(address);

                PdfPTable bodytablelogo = new PdfPTable(1);
                bodytablelogo.TotalWidth = 600f;
                tablelogo.LockedWidth = true;
                float[] widthlogo = new float[] { 2f };
                bodytablelogo.SetWidths(widthlogo);


                PdfPCell cell9 = new PdfPCell(new Phrase("Unit Name :" + dtclientaddress.Rows[0]["clientname"].ToString(), FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, Font.NORMAL, BaseColor.BLACK)));
                cell9.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                cell9.Colspan = 0;
                cell9.Border = 0;
                bodytablelogo.AddCell(cell9);
                // bodytablelogo.AddCell(celll);

                PdfPCell cell10 = new PdfPCell(new Phrase("Bill From : " + txtfromdate.Text + "  to  " + "30/07/2012" + " ", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, Font.NORMAL, BaseColor.BLACK)));
                cell10.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                cell10.Colspan = 2;
                cell10.Border = 0;
                bodytablelogo.AddCell(cell10);
                // bodytablelogo.AddCell(celll);
                PdfPCell cell20 = new PdfPCell(new Phrase(" We are presenting our bill for the House Keeping Services Provided at your establishment. Kindly Release the payment as earliest. ", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, Font.NORMAL, BaseColor.BLACK)));
                cell20.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                cell20.Colspan = 0;
                cell20.Border = 0;
                bodytablelogo.AddCell(cell20);
                PdfPCell cell21 = new PdfPCell(new Phrase(" The Details are given below ", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, Font.NORMAL, BaseColor.BLACK)));
                cell21.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                cell21.Colspan = 0;
                cell21.Border = 0;
                bodytablelogo.AddCell(cell21);

                bodytablelogo.AddCell(celll);
                document.Add(bodytablelogo);


                //link button column is excluded from the list
                int colCount = gvclientpayment.Columns.Count;


                //Create a table
                PdfPTable table = new PdfPTable(colCount);
                table.HorizontalAlignment = 1;

                //create an array to store column widths
                int[] colWidths = new int[gvclientpayment.Columns.Count];


                PdfPCell cell;
                string cellText;
                //create the header row
                for (int colIndex = 0; colIndex < colCount; colIndex++)
                {
                    //set the column width
                    colWidths[colIndex] = (int)gvclientpayment.Columns[colIndex].ItemStyle.Width.Value;
                    //fetch the header text
                    cellText = Server.HtmlDecode(gvclientpayment.HeaderRow.Cells[colIndex].Text);

                    //create a new cell with header text
                    cell = new PdfPCell(new Phrase(cellText));

                    //set the background color for the header cell
                    // cell.BackgroundColor = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#d1dbe0"));

                    //add the cell to the table. we dont need to create a row and add cells to the row
                    //since we set the column count of the table to size of the no of columns, it will automatically create row for
                    //every 4 cells
                    //cell.Width = 50;
                    table.AddCell(cell);

                }
                //export rows from GridView to table
                for (int rowIndex = 0; rowIndex < gvclientpayment.Rows.Count; rowIndex++)
                {
                    if (gvclientpayment.Rows[rowIndex].RowType == DataControlRowType.DataRow)
                    {
                        for (int j = 0; j < gvclientpayment.Columns.Count; j++)
                        {
                            //fetch the column value of the current row
                            if (j < 5)
                                cellText = Server.HtmlDecode(gvclientpayment.Rows[rowIndex].Cells[j].Text);
                            else
                            {
                                Label label1 = (Label)(gvclientpayment.Rows[rowIndex].FindControl("lblamount"));
                                cellText = label1.Text;
                            }

                            //create a new cell with column value
                            cell = new PdfPCell(new Phrase(cellText));

                            //Set Color of Alternating row
                            //if (rowIndex % 2 != 0)
                            //{
                            //    cell.BackgroundColor = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#9ab2ca"));
                            //}
                            //else
                            //{
                            //    cell.BackgroundColor = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#f1f5f6"));
                            //}
                            //add the cell to the table
                            table.AddCell(cell);
                        }
                    }
                }


                document.Add(table);
                tablelogo.AddCell(celll);

                PdfPTable tabled = new PdfPTable(6);
                tabled.TotalWidth = 432f;
                tabled.LockedWidth = true;
                float[] widthd = new float[] { 1f, 1f, 1f, 1f, 1f, 1f };
                tabled.SetWidths(widthd);

                PdfPCell celldz1 = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, Font.NORMAL, BaseColor.BLACK)));
                celldz1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                //celld7.Border = 1;
                celldz1.Colspan = 4;
                tabled.AddCell(celldz1);

                PdfPCell celldz2 = new PdfPCell(new Phrase("Total@ ", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, Font.NORMAL, BaseColor.BLACK)));
                celldz2.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                //celld8.Border = 1;
                tabled.AddCell(celldz2);


                PdfPCell celldz4 = new PdfPCell(new Phrase(" " + totalamount.ToString("0.00"), FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, Font.NORMAL, BaseColor.BLACK)));
                celldz4.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                tabled.AddCell(celldz4);

                PdfPCell celldd1 = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, Font.NORMAL, BaseColor.BLACK)));
                celldd1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                //celld7.Border = 1;
                celldd1.Colspan = 3;
                // celldd1.BackgroundColor = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#9ab2ca"));

                tabled.AddCell(celldd1);


                PdfPCell celldc2 = new PdfPCell(new Phrase("Service Charges@ ", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, Font.NORMAL, BaseColor.BLACK)));
                celldc2.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                //celld8.Border = 1;
                //  celldc2.BackgroundColor = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#9ab2ca"));

                tabled.AddCell(celldc2);


                PdfPCell celldc3 = new PdfPCell(new Phrase("10%", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, Font.NORMAL, BaseColor.BLACK)));
                celldc3.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                //s celldc3.Border = 1;
                //  celldc3.BackgroundColor = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#9ab2ca"));

                tabled.AddCell(celldc3);


                //double tota = totalamount * (10.0 / 100);

                PdfPCell celldc4 = new PdfPCell(new Phrase("" + (totalamount * (10.0 / 100)).ToString("0.00"), FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, Font.NORMAL, BaseColor.BLACK)));
                celldc4.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                //celldc4.BackgroundColor = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#9ab2ca"));


                tabled.AddCell(celldc4);


                PdfPCell celldc1 = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, Font.NORMAL, BaseColor.BLACK)));
                celldc1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                //celld7.Border = 1;
                celldc1.Colspan = 3;

                //celldc1.BackgroundColor = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#9ab2ca"));

                tabled.AddCell(celldc1);

                PdfPCell celldd2 = new PdfPCell(new Phrase("Service Tax@ ", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, Font.NORMAL, BaseColor.BLACK)));
                celldd2.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                //celld8.Border = 1;
                tabled.AddCell(celldd2);



                PdfPCell celldd3 = new PdfPCell(new Phrase("12%", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, Font.NORMAL, BaseColor.BLACK)));
                celldd3.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                //s celldc3.Border = 1;


                tabled.AddCell(celldd3);



                PdfPCell celldd4 = new PdfPCell(new Phrase("" + ((totalamount) * (12.0 / 100)).ToString("0.00"), FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, Font.NORMAL, BaseColor.BLACK)));
                celldd4.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right

                tabled.AddCell(celldd4);




                PdfPCell cellde1 = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, Font.NORMAL, BaseColor.BLACK)));
                cellde1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                //celld7.Border = 1;
                cellde1.Colspan = 3;
                // cellde1.BackgroundColor = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#9ab2ca"));

                tabled.AddCell(cellde1);


                PdfPCell cellde2 = new PdfPCell(new Phrase("CESS@ ", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, Font.NORMAL, BaseColor.BLACK)));
                cellde2.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                //celld8.Border = 1;
                //   cellde2.BackgroundColor = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#9ab2ca"));

                tabled.AddCell(cellde2);


                PdfPCell cellde3 = new PdfPCell(new Phrase("2%", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, Font.NORMAL, BaseColor.BLACK)));
                cellde3.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                //s celldc3.Border = 1;
                // cellde3.BackgroundColor = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#9ab2ca"));

                tabled.AddCell(cellde3);
                PdfPCell cellde4 = new PdfPCell(new Phrase("" + ((totalamount) * (2.0 / 100)).ToString("0.00"), FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, Font.NORMAL, BaseColor.BLACK)));
                cellde4.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                // cellde4.BackgroundColor = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#9ab2ca"));

                tabled.AddCell(cellde4);




                PdfPCell celldf1 = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, Font.NORMAL, BaseColor.BLACK)));
                celldf1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                //celld7.Border = 1;
                celldf1.Colspan = 3;
                tabled.AddCell(celldf1);

                PdfPCell celldf2 = new PdfPCell(new Phrase("S&H Ed.CESS@ ", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, Font.NORMAL, BaseColor.BLACK)));
                celldf2.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                //celld8.Border = 1;
                //celldf2.Width = 3f;
                tabled.AddCell(celldf2);


                PdfPCell celldf3 = new PdfPCell(new Phrase("1%", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, Font.NORMAL, BaseColor.BLACK)));
                celldf3.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                //s celldc3.Border = 1;
                tabled.AddCell(celldf3);
                PdfPCell celldf4 = new PdfPCell(new Phrase("" + ((totalamount) * (1.0 / 100)).ToString("0.00"), FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, Font.NORMAL, BaseColor.BLACK)));
                celldf4.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                tabled.AddCell(celldf4);




                PdfPCell celldg1 = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, Font.NORMAL, BaseColor.BLACK)));
                celldg1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                //celld7.Border = 1;
                celldg1.Colspan = 3;

                // celldg1.BackgroundColor = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#9ab2ca"));

                tabled.AddCell(celldg1);
                PdfPCell celldg2 = new PdfPCell(new Phrase("Discount@ ", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, Font.NORMAL, BaseColor.BLACK)));
                celldg2.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                //celld8.Border = 1;
                // celldg2.BackgroundColor = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#9ab2ca"));

                tabled.AddCell(celldg2);

                PdfPCell celldg3 = new PdfPCell(new Phrase("12.36%", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, Font.NORMAL, BaseColor.BLACK)));
                celldg3.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                //s celldc3.Border = 1;
                // celldg3.BackgroundColor = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#9ab2ca"));

                tabled.AddCell(celldg3);
                PdfPCell celldg4 = new PdfPCell(new Phrase("" + ((totalamount) * (12.36 / 100)).ToString("0.00"), FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, Font.NORMAL, BaseColor.BLACK)));
                celldg4.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right

                // celldg4.BackgroundColor = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#9ab2ca"));

                tabled.AddCell(celldg4);

                PdfPCell celldg5 = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, Font.NORMAL, BaseColor.BLACK)));
                celldg5.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                //celld7.Border = 1;
                celldg5.Colspan = 4;
                tabled.AddCell(celldg5);



                PdfPCell celldg6 = new PdfPCell(new Phrase("Grand Total(Rs:)", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, Font.NORMAL, BaseColor.BLACK)));
                celldg6.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                //celld8.Border = 1;
                tabled.AddCell(celldg6);
                double grandtotal = (totalamount) * (10 / 100) + (totalamount) * (12.0 / 100) + (totalamount) * (2 / 100) + (totalamount) * (1 / 100) + (totalamount) * (12.36 / 100);

                PdfPCell celldg8 = new PdfPCell(new Phrase("" + grandtotal.ToString("0.00"), FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, Font.NORMAL, BaseColor.BLACK)));
                celldg8.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                tabled.AddCell(celldg8);


                document.Add(tabled);



                PdfPTable tablecon = new PdfPTable(2);
                tablecon.TotalWidth = 500f;
                tablecon.LockedWidth = true;
                float[] widthcon = new float[] { 2f, 2f };
                tablecon.SetWidths(widthcon);

                PdfPCell cellBreak = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 20, Font.NORMAL, BaseColor.BLACK)));
                cellBreak.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                cellBreak.Colspan = 2;
                cellBreak.Border = 0;
                tablecon.AddCell(cellBreak);

                PdfPCell cellc2 = new PdfPCell(new Phrase("In Words Rupees One Thousand One Hundred Twenty Four Only.", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, Font.NORMAL, BaseColor.BLACK)));
                cellc2.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                cellc2.Colspan = 0;
                cellc2.Border = 0;
                tablecon.AddCell(cellc2);
                tablecon.AddCell(cellBreak);
                tablecon.AddCell(cellBreak);
                tablecon.AddCell(cellBreak);
                tablecon.AddCell(cellBreak);
                tablecon.AddCell(cellBreak);

                PdfPCell cellc3 = new PdfPCell(new Phrase("For K.L.TECHNICAL SERVICES", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, Font.BOLD, BaseColor.BLACK)));
                cellc3.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                cellc3.Colspan = 2;
                cellc3.Border = 0;
                tablecon.AddCell(cellc3);
                tablecon.AddCell(cellBreak);
                tablecon.AddCell(cellBreak);
                tablecon.AddCell(cellBreak);

                PdfPCell cellc4 = new PdfPCell(new Phrase("Authorised Signatory", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, Font.BOLD, BaseColor.BLACK)));
                cellc4.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                cellc4.Colspan = 7;
                cellc4.Border = 0;
                tablecon.AddCell(cellc4);


                document.Add(tablecon);

                document.NewPage();
                document.Close();
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment;filename=Invoice.pdf");
                Response.Buffer = true;
                Response.Clear();
                Response.OutputStream.Write(ms.GetBuffer(), 0, ms.GetBuffer().Length);
                Response.OutputStream.Flush();
                Response.End();
            }
            catch (Exception ex)
            {

            }

        }

        protected void ddlmonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = 0;
            string selectquery = " select UnitBillBreakup.UnitId, UnitBillBreakup.Designation,UnitBillBreakup.NoofEmps,Round(UnitBillBreakup.PayRate,2) as PayRate,UnitBillBreakup.DutyHours  from UnitBillBreakup where UnitBillBreakup.unitid ='" + ddlclientid.SelectedItem.ToString() + "'";//and UnitBillBreakup.unitid ='" + ddlclientid.SelectedItem.ToString() + "'";
            dt = config.ExecuteAdaptorAsyncWithQueryParams(selectquery).Result;

            gvclientpayment.DataSource = dt;
            gvclientpayment.DataBind();
            for (index = 0; index < dt.Rows.Count; index++)
            {
                totalamount = totalamount + double.Parse(dt.Rows[index]["PayRate"].ToString());
            }
            int rowindex;
            for (rowindex = 0; rowindex < gvclientpayment.Rows.Count; rowindex++)
            {
                float noofems = float.Parse(dt.Rows[rowindex]["noofemps"].ToString());
                float payrate = float.Parse(dt.Rows[rowindex]["payrate"].ToString());
                float amount = noofems * payrate;
                Label totalamount = gvclientpayment.Rows[rowindex].FindControl("lblamount") as Label;
                totalamount.Text = amount.ToString();

            }
            //gvclientpayment.DataSource = dt;
            //    gvclientpayment.DataBind();
        }

        protected void ddlclientid_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string selectstartingdate = "select ContractStartDate,ContractEndDate from contracts where clientid = '" + ddlclientid.SelectedItem.ToString() + "'";
                dt = config.ExecuteAdaptorAsyncWithQueryParams(selectstartingdate).Result;
                txtfromdate.Text = Convert.ToDateTime(dt.Rows[0]["ContractStartDate"].ToString()).ToShortDateString();
                txttodate.Text = Convert.ToDateTime(dt.Rows[0]["ContractEndDate"].ToString()).ToShortDateString();
            }
            catch (Exception ex)
            {
            }
        }

        //private void PrepareForExport(Control ctrl)
        //{
        //    //iterate through all the grid controls
        //    foreach (Control childControl in ctrl.Controls)
        //    {
        //        //if the control type is link button, remove it
        //        //from the collection
        //        if (childControl.GetType() == typeof(LinkButton))
        //        {
        //            ctrl.Controls.Remove(childControl);
        //        }
        //        //if the child control is not empty, repeat the process
        //        // for all its controls
        //        else if (childControl.HasControls())
        //        {
        //            PrepareForExport(childControl);
        //        }
        //    }
        //}
        //protected void ExportToPDFWithFormatting()
        //{
        //    //link button column is excluded from the list
        //    int colCount = gvclientpayment.Columns.Count - 1;

        //    //Create a table
        //    PdfPTable table = new PdfPTable(colCount);
        //    table.HorizontalAlignment = 0;

        //    //create an array to store column widths
        //    int[] colWidths = new int[gvclientpayment.Columns.Count];

        //    PdfPCell cell;
        //    string cellText;
        //    //create the header row
        //    for (int colIndex = 0; colIndex < colCount; colIndex++)
        //    {
        //        //set the column width
        //        colWidths[colIndex] = (int)gvclientpayment.Columns[colIndex].ItemStyle.Width.Value;

        //        //fetch the header text
        //        cellText = Server.HtmlDecode(gvclientpayment.HeaderRow.Cells[colIndex].Text);

        //        //create a new cell with header text
        //        cell = new PdfPCell(new Phrase(cellText));

        //        //set the background color for the header cell
        //        cell.BackgroundColor = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#d1dbe0"));

        //        //add the cell to the table. we dont need to create a row and add cells to the row
        //        //since we set the column count of the table to 4, it will automatically create row for
        //        //every 4 cells
        //        table.AddCell(cell);
        //    }

        //    //export rows from GridView to table
        //    for (int rowIndex = 0; rowIndex < gvclientpayment.Rows.Count; rowIndex++)
        //    {
        //        if (gvclientpayment.Rows[rowIndex].RowType == DataControlRowType.DataRow)
        //        {
        //            for (int j = 0; j < gvclientpayment.Columns.Count - 1; j++)
        //            {
        //                //fetch the column value of the current row
        //                cellText = Server.HtmlDecode(gvclientpayment.Rows[rowIndex].Cells[j].Text);

        //                //create a new cell with column value
        //                cell = new PdfPCell(new Phrase(cellText));

        //                //Set Color of Alternating row
        //                if (rowIndex % 2 != 0)
        //                {
        //                    cell.BackgroundColor = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#9ab2ca"));
        //                }
        //                else
        //                {
        //                    cell.BackgroundColor = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#f1f5f6"));
        //                }
        //                //add the cell to the table
        //                table.AddCell(cell);
        //            }
        //        }
        //    }

        //    //Create the PDF Document
        //    Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);

        //    PdfWriter.GetInstance(pdfDoc, Response.OutputStream);

        //    //open the stream
        //    pdfDoc.Open();

        //    //add the table to the document
        //    pdfDoc.Add(table);

        //    //close the document stream
        //    pdfDoc.Close();

        //    Response.ContentType = "application/pdf";
        //    Response.AddHeader("content-disposition", "attachment;" + "filename=Product.pdf");
        //    Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //    Response.Write(pdfDoc);
        //    Response.End();
        //}

        protected void gvclientpayment_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
    }
}