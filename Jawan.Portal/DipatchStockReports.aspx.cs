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
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using System.IO;
using System.Globalization;
using Jawan.Portal.DAL;
namespace Jawan.Portal
{
    public partial class DipatchStockReports : System.Web.UI.Page
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
            int i = 0;
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
                    LoadClientList();
                    LoadClientNames();
                }
                else
                {
                    Response.Redirect("login.aspx");
                }

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
                DdlClientId.DataValueField = "Clientid";
                DdlClientId.DataTextField = "Clientid";
                DdlClientId.DataSource = DtClientNames;
                DdlClientId.DataBind();
            }
            DdlClientId.Items.Insert(0, "-Select-");
            DdlClientId.Items.Insert(1, "ALL");
        }

        protected void GetWebConfigdata()
        {

            EmpIDPrefix = Session["EmpIDPrefix"].ToString();
            CmpIDPrefix = Session["CmpIDPrefix"].ToString();
            BranchID = Session["BranchID"].ToString();

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
                    ClientsLink.Visible = true;
                    CompanyInfoLink.Visible = false;
                    InventoryLink.Visible = true;
                    ReportsLink.Visible = true;
                    SettingsLink.Visible = false;

                    EmployeeReportLink.Visible = false;
                    ClientsReportLink.Visible = false;
                    ExpensesReportsLink.Visible = false;

                    break;
                default:
                    break;


            }
        }

        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            GVListOfItems.DataSource = null;
            GVListOfItems.DataBind();
            GVListOfItemsAll.DataSource = null;
            GVListOfItemsAll.DataBind();
            LblResult.Text = "";
            LblResult.Visible = true;

            #region New code as on 12/03/2014 by venkat

            string date = "";

            if (txtdate.Text.Trim().Length > 0)
            {
                date = Convert.ToDateTime(txtdate.Text.Trim(), CultureInfo.GetCultureInfo("en-gb")).ToString();
            }
            #endregion



            if (DdlClientId.SelectedIndex == 0)
            {
                LblResult.Text = "Please Select Either Client Id/Month";
                return;
            }

            if (DdlClientId.SelectedIndex == 1)
            {
                if (ddlmrfid.SelectedIndex == 1 && txtdate.Text.Trim().Length > 0)
                {
                    string SqlQry = "select MRF.ItemID,MRF.mrfid,Stockitemlist.ItemName,mrf.ClientId,c.ClientName,MRF.Quantity,MRF.ApprovedQty,MRF.SellingPrice, " +
                        " (MRF.ApprovedQty * MRF.SellingPrice)as SellingPricetotal,CONVERT(varchar(10),DispachedDate,103) as date" +
                        " from MRF,Stockitemlist,Clients c where DispatchStatus=1 and mrf.ClientId=c.ClientId and Stockitemlist.itemid=MRF.itemid  and CONVERT(varchar,month(DispachedDate))" +
                          "=month('" + date + "') order by mrf.mrfid";
                    BindDataAll(SqlQry);
                }
                if (ddlmrfid.SelectedIndex > 1 && txtdate.Text.Trim().Length > 0)
                {
                    string SqlQry = "select MRF.ItemID,MRF.mrfid,Stockitemlist.ItemName,MRF.Quantity,MRF.ApprovedQty,MRF.SellingPrice, " +
                        " (MRF.ApprovedQty * MRF.SellingPrice)as SellingPricetotal,CONVERT(varchar(10),DispachedDate,103) as date" +
                        " from MRF,Stockitemlist where DispatchStatus=1 and Stockitemlist.itemid=MRF.itemid  and CONVERT(varchar,month(DispachedDate))" +
                          "=month('" + date + "') and mrf.mrfid='" + ddlmrfid.SelectedValue + "' order by mrf.mrfid ";
                    BindData(SqlQry);
                }
                if (ddlmrfid.SelectedIndex == 1 && txtdate.Text.Trim().Length == 0)
                {
                    string SqlQry = "select MRF.ItemID,MRF.mrfid,Stockitemlist.ItemName,mrf.ClientId,c.ClientName,MRF.Quantity,MRF.ApprovedQty,MRF.SellingPrice, " +
                        " (MRF.ApprovedQty * MRF.SellingPrice)as SellingPricetotal,CONVERT(varchar(10),DispachedDate,103) as date" +
                        " from MRF,Stockitemlist,Clients c where DispatchStatus=1 and mrf.ClientId=c.ClientId and Stockitemlist.itemid=MRF.itemid  order by mrf.mrfid";
                    BindDataAll(SqlQry);
                }
                if (ddlmrfid.SelectedIndex > 1 && txtdate.Text.Trim().Length == 0)
                {
                    string SqlQry = "select MRF.ItemID,MRF.mrfid,Stockitemlist.ItemName,MRF.Quantity,MRF.ApprovedQty,MRF.SellingPrice, " +
                        " (MRF.ApprovedQty * MRF.SellingPrice)as SellingPricetotal,CONVERT(varchar(10),DispachedDate,103) as date" +
                        " from MRF,Stockitemlist where DispatchStatus=1 and Stockitemlist.itemid=MRF.itemid  and mrf.mrfid='" + ddlmrfid.SelectedValue + "' order by mrf.mrfid ";
                    BindData(SqlQry);
                }
                return;

            }



            if (DdlClientId.SelectedIndex > 1)
            {
                if (ddlmrfid.SelectedIndex == 1 && txtdate.Text.Trim().Length > 0)
                {
                    string SqlQry = "select  MRF.ItemID,MRF.mrfid,Stockitemlist.ItemName,MRF.Quantity,  " +
                        "MRF.ApprovedQty,(MRF.ApprovedQty * MRF.SellingPrice)as SellingPricetotal, " +
                        " MRF.SellingPrice,CONVERT(varchar(10),DispachedDate,103) as date from MRF,Stockitemlist where DispatchStatus=1" +
                        " and Stockitemlist.itemid=MRF.itemid and CONVERT(varchar,month(DispachedDate))=Month('" + date + "')" +
                        " and clientid = '" + DdlClientId.SelectedValue + "'   Order by MRFID";
                    BindData(SqlQry);
                }
                if (ddlmrfid.SelectedIndex > 1 && txtdate.Text.Trim().Length > 0)
                {
                    string SqlQry = "select  MRF.ItemID,MRF.mrfid,Stockitemlist.ItemName,MRF.Quantity,  " +
                       "MRF.ApprovedQty,(MRF.ApprovedQty * MRF.SellingPrice)as SellingPricetotal, " +
                       " MRF.SellingPrice,CONVERT(varchar(10),DispachedDate,103) as date from MRF,Stockitemlist where DispatchStatus=1" +
                       " and Stockitemlist.itemid=MRF.itemid and CONVERT(varchar,month(DispachedDate))=Month('" + date + "')" +
                       " and clientid = '" + DdlClientId.SelectedValue + "' and mrf.mrfid='" + ddlmrfid.SelectedValue + "'   Order by MRFID";
                    BindData(SqlQry);
                }
                if (ddlmrfid.SelectedIndex == 1 && txtdate.Text.Trim().Length == 0)
                {
                    string SqlQry = "select  MRF.ItemID,MRF.mrfid,Stockitemlist.ItemName,MRF.Quantity,  " +
                        "MRF.ApprovedQty,(MRF.ApprovedQty * MRF.SellingPrice)as SellingPricetotal, " +
                        " MRF.SellingPrice,CONVERT(varchar(10),DispachedDate,103) as date from MRF,Stockitemlist where DispatchStatus=1" +
                        " and Stockitemlist.itemid=MRF.itemid " +
                        " and clientid = '" + DdlClientId.SelectedValue + "'   Order by MRFID";
                    BindData(SqlQry);
                }
                if (ddlmrfid.SelectedIndex > 1 && txtdate.Text.Trim().Length == 0)
                {
                    string SqlQry = "select  MRF.ItemID,MRF.mrfid,Stockitemlist.ItemName,MRF.Quantity,  " +
                       "MRF.ApprovedQty,(MRF.ApprovedQty * MRF.SellingPrice)as SellingPricetotal, " +
                       " MRF.SellingPrice,CONVERT(varchar(10),DispachedDate,103) as date from MRF,Stockitemlist where DispatchStatus=1" +
                       " and Stockitemlist.itemid=MRF.itemid " +
                       " and clientid = '" + DdlClientId.SelectedValue + "' and mrf.mrfid='" + ddlmrfid.SelectedValue + "'   Order by MRFID";
                    BindData(SqlQry);
                }

            }

        }

        public void BindData(string SqlQry)
        {
            LblResult.Text = "";


            DataTable DtResult = null;
            DtResult =config.ExecuteAdaptorAsyncWithQueryParams(SqlQry).Result;
            if (DtResult.Rows.Count > 0)
            {
                GVListOfItems.DataSource = DtResult;
                GVListOfItems.DataBind();
                return;
            }
            else
            {
                LblResult.Text = "There Is No Stock For The Corrsponding Selection";
                return;
            }
        }

        #region New Code for bind all data as on 12/03/2014


        public void BindDataAll(string SqlQry)
        {
            LblResult.Text = "";


            DataTable DtResult = null;
            DtResult = config.ExecuteAdaptorAsyncWithQueryParams(SqlQry).Result;


            if (DtResult.Rows.Count > 0)
            {
                GVListOfItemsAll.DataSource = DtResult;
                GVListOfItemsAll.DataBind();
                return;
            }
            else
            {
                LblResult.Text = "There Is No Stock For The Corrsponding Selection";
                return;
            }
        }

        #endregion

        protected void Cleardata()
        {
            LblResult.Text = "";
            GVListOfItems.DataSource = null;
            GVListOfItems.DataBind();
            ddlcname.SelectedIndex = 0;
            DdlClientId.SelectedIndex = 0;
            DdlMonth.SelectedIndex = 0;
            ddlmrfid.Items.Clear();
            ddlmrfid.Items.Insert(0, "-Select-");

        }

        protected void ddlcname_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            GVListOfItems.DataSource = null;
            GVListOfItems.DataBind();
            LblResult.Text = "";
            if (ddlcname.SelectedIndex > 0)
            {
                FillClientid();
            }
            else
            {
                Cleardata();
            }
        }

        protected void btnPDF_Click(object sender, EventArgs e)
        {
            #region Code for when select clientid and MRF as on  12/03/2014

            if (GVListOfItems.Rows.Count > 0)
            {
                MemoryStream ms = new MemoryStream();
                Document document = new Document(PageSize.LEGAL);
                PdfWriter writer = PdfWriter.GetInstance(document, ms);
                document.Open();
                document.AddTitle("DIYOS");
                document.AddAuthor("DIYOS");
                document.AddSubject("Invoice");
                document.AddKeywords("Keyword1, keyword2, …");

                int columns = GVListOfItems.Columns.Count;
                int rows = GVListOfItems.Rows.Count;
                PdfPTable gvTable = new PdfPTable(columns);
                gvTable.TotalWidth = 600f;
                gvTable.LockedWidth = true;
                float[] widtlogo = { 1f, 1f, 2f, 4f, 2f, 2f, 2f, 2f }; //new float[columns]; 
                gvTable.SetWidths(widtlogo);

                uint FONT_SIZE = 8;

                PdfPCell c1 = new PdfPCell(new Phrase("Dispatch Stock Report", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, Font.BOLD, BaseColor.BLACK)));
                c1.Border = 0;
                c1.HorizontalAlignment = 1;
                c1.Colspan = columns;
                gvTable.AddCell(c1);

                string Monthname = "";
                string year = "";
                string GetMonth = "";
                if (txtdate.Text.Trim().Length > 0)
                {
                    string date = "";

                    if (txtdate.Text.Trim().Length > 0)
                    {
                        date = Convert.ToDateTime(txtdate.Text.Trim(), CultureInfo.GetCultureInfo("en-gb")).ToString();
                    }

                    Monthname = DateTime.Parse(date).Month.ToString();
                    year = DateTime.Parse(date).Year.ToString();
                    GetMonth = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(int.Parse(Monthname));
                }



                PdfPCell cBlank = new PdfPCell(new Phrase(" ", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, Font.BOLD, BaseColor.BLACK)));
                cBlank.Border = 0;
                cBlank.HorizontalAlignment = 1;
                cBlank.Colspan = columns;
                gvTable.AddCell(cBlank);

                PdfPCell cName = new PdfPCell(new Phrase("Client ID/Name : " + ddlcname.SelectedValue + "/" + ddlcname.SelectedItem, FontFactory.GetFont(FontFactory.TIMES_ROMAN, 9, Font.NORMAL, BaseColor.BLACK)));
                cName.Border = 0;
                cName.HorizontalAlignment = 0;
                cName.Colspan = 4;
                gvTable.AddCell(cName);

                if (txtdate.Text.Trim().Length > 0)
                {
                    PdfPCell cMonth = new PdfPCell(new Phrase("For the month of : " + GetMonth.Substring(0, 3) + " / " + year, FontFactory.GetFont(FontFactory.TIMES_ROMAN, 9, Font.NORMAL, BaseColor.BLACK)));
                    cMonth.Border = 0;
                    cMonth.HorizontalAlignment = 2;
                    cMonth.Colspan = 4;
                    gvTable.AddCell(cMonth);
                }
                else
                {
                    PdfPCell cMonth = new PdfPCell(new Phrase(" ", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 9, Font.NORMAL, BaseColor.BLACK)));
                    cMonth.Border = 0;
                    cMonth.HorizontalAlignment = 2;
                    cMonth.Colspan = 4;
                    gvTable.AddCell(cMonth);
                }

                PdfPCell space = new PdfPCell(new Phrase(" ", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 9, Font.NORMAL, BaseColor.BLACK)));
                space.Border = 0;
                space.HorizontalAlignment = 2;
                space.Colspan = 8;
                gvTable.AddCell(space);

                PdfPCell cell;
                string cellText = "";

                for (int i = 0; i < columns; i++)
                {
                    widtlogo[i] = (int)GVListOfItems.Columns[i].ItemStyle.Width.Value;
                    //fetch the header text
                    cellText = Server.HtmlDecode(GVListOfItems.HeaderRow.Cells[i].Text);
                    cell = new PdfPCell(new Phrase(cellText, FontFactory.GetFont(FontFactory.TIMES_ROMAN, 9, Font.BOLD, BaseColor.BLACK)));
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.HorizontalAlignment = 1;
                    gvTable.AddCell(cell);
                }

                float TotalQty = 0;
                float TotalPrice = 0;
                float GTotal = 0;
                for (int rowCounter = 0; rowCounter < rows; rowCounter++)
                {
                    if (GVListOfItems.Rows[rowCounter].RowType == DataControlRowType.DataRow)
                    {

                        cellText = ((Label)GVListOfItems.Rows[rowCounter].FindControl("lblsno")).Text;
                        cell = new PdfPCell(new Phrase(cellText, FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                        gvTable.AddCell(cell);

                        cellText = GVListOfItems.Rows[rowCounter].Cells[1].Text;
                        cell = new PdfPCell(new Phrase(cellText, FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                        gvTable.AddCell(cell);

                        cellText = GVListOfItems.Rows[rowCounter].Cells[2].Text;
                        cell = new PdfPCell(new Phrase(cellText, FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                        gvTable.AddCell(cell);

                        cellText = GVListOfItems.Rows[rowCounter].Cells[3].Text;
                        cell = new PdfPCell(new Phrase(cellText, FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                        gvTable.AddCell(cell);


                        cellText = GVListOfItems.Rows[rowCounter].Cells[4].Text;
                        cell = new PdfPCell(new Phrase(cellText, FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                        gvTable.AddCell(cell);

                        cellText = GVListOfItems.Rows[rowCounter].Cells[5].Text;
                        if (cellText.Length > 0)
                        {
                            TotalQty += float.Parse(cellText.ToString());
                        }
                        cell = new PdfPCell(new Phrase(cellText, FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                        gvTable.AddCell(cell);

                        cellText = GVListOfItems.Rows[rowCounter].Cells[6].Text;
                        if (cellText.Length > 0)
                        {
                            TotalPrice += float.Parse(cellText.ToString());
                        }
                        cell = new PdfPCell(new Phrase(cellText, FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                        gvTable.AddCell(cell);

                        cellText = GVListOfItems.Rows[rowCounter].Cells[7].Text;
                        if (cellText.Length > 0)
                        {
                            GTotal += float.Parse(cellText.ToString());
                        }
                        cell = new PdfPCell(new Phrase(cellText, FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                        gvTable.AddCell(cell);
                    }
                }
                cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                cell.Border = 0;
                gvTable.AddCell(cell);

                cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                cell.Border = 0;
                gvTable.AddCell(cell);

                cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                cell.Border = 0;
                gvTable.AddCell(cell);

                cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                cell.Border = 0;
                gvTable.AddCell(cell);


                cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                cell.Border = 0;
                gvTable.AddCell(cell);


                cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                cell.Border = 0;
                gvTable.AddCell(cell);

                cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                cell.Border = 0;
                gvTable.AddCell(cell);

                cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                cell.Border = 0;
                gvTable.AddCell(cell);

                cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                cell.Border = 0;
                gvTable.AddCell(cell);

                cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                cell.Border = 0;
                gvTable.AddCell(cell);


                cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                cell.Border = 0;
                gvTable.AddCell(cell);


                cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                cell.Border = 0;
                gvTable.AddCell(cell);

                cellText = "Total";
                cell = new PdfPCell(new Phrase(cellText, FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                //cell.Border = 0;
                gvTable.AddCell(cell);

                cell = new PdfPCell(new Phrase(TotalQty.ToString(), FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                gvTable.AddCell(cell);


                cell = new PdfPCell(new Phrase(TotalPrice.ToString(), FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                gvTable.AddCell(cell);

                cellText = "";
                cell = new PdfPCell(new Phrase(GTotal.ToString(), FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                gvTable.AddCell(cell);




                document.Add(gvTable);
                document.NewPage();
                document.Close();
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment;filename=StockInHand.pdf");
                Response.Buffer = true;
                Response.Clear();
                Response.OutputStream.Write(ms.GetBuffer(), 0, ms.GetBuffer().Length);
                Response.OutputStream.Flush();
                Response.End();
            }

            #endregion

            #region Code for when select all Mrf's as on  12/03/2014

            if (GVListOfItemsAll.Rows.Count > 0)
            {
                MemoryStream ms = new MemoryStream();
                Document document = new Document(PageSize.LEGAL);
                PdfWriter writer = PdfWriter.GetInstance(document, ms);
                document.Open();
                document.AddTitle("DIYOS");
                document.AddAuthor("DIYOS");
                document.AddSubject("Invoice");
                document.AddKeywords("Keyword1, keyword2, …");

                int columns = GVListOfItemsAll.Columns.Count;
                int rows = GVListOfItemsAll.Rows.Count;
                PdfPTable gvTable = new PdfPTable(columns);
                gvTable.TotalWidth = 500f;
                gvTable.LockedWidth = true;
                float[] widtlogo = { 0.8f, 1.2f, 1.2f, 3.5f, 1.5f, 4.5f, 1.5f, 1f, 1f, 1f }; //new float[columns]; 
                gvTable.SetWidths(widtlogo);

                uint FONT_SIZE = 8;

                float TotalQty = 0;
                float TotalPrice = 0;
                float GTotal = 0;

                PdfPCell c1 = new PdfPCell(new Phrase("Dispatch Stock Report", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 14, Font.BOLD, BaseColor.BLACK)));
                c1.Border = 0;
                c1.HorizontalAlignment = 1;
                c1.Colspan = columns;
                gvTable.AddCell(c1);
                PdfPCell cBlank = new PdfPCell(new Phrase(" ", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 13, Font.BOLD, BaseColor.BLACK)));
                cBlank.Border = 0;
                cBlank.HorizontalAlignment = 1;
                cBlank.Colspan = columns;
                gvTable.AddCell(cBlank);

                PdfPCell cell;
                string cellText = "";

                for (int i = 0; i < columns; i++)
                {
                    widtlogo[i] = (int)GVListOfItemsAll.Columns[i].ItemStyle.Width.Value;
                    //fetch the header text
                    cellText = Server.HtmlDecode(GVListOfItemsAll.HeaderRow.Cells[i].Text);
                    cell = new PdfPCell(new Phrase(cellText, FontFactory.GetFont(FontFactory.TIMES_ROMAN, 8, Font.BOLD, BaseColor.BLACK)));
                    cell.HorizontalAlignment = 1;
                    gvTable.AddCell(cell);
                }

                for (int rowCounter = 0; rowCounter < rows; rowCounter++)
                {
                    if (GVListOfItemsAll.Rows[rowCounter].RowType == DataControlRowType.DataRow)
                    {

                        cellText = ((Label)GVListOfItemsAll.Rows[rowCounter].FindControl("lblsno1")).Text;
                        cell = new PdfPCell(new Phrase(cellText, FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                        gvTable.AddCell(cell);

                        cellText = GVListOfItemsAll.Rows[rowCounter].Cells[1].Text;
                        cell = new PdfPCell(new Phrase(cellText, FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                        gvTable.AddCell(cell);

                        cellText = GVListOfItemsAll.Rows[rowCounter].Cells[2].Text;
                        cell = new PdfPCell(new Phrase(cellText, FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                        gvTable.AddCell(cell);

                        cellText = GVListOfItemsAll.Rows[rowCounter].Cells[3].Text;
                        cell = new PdfPCell(new Phrase(cellText, FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                        gvTable.AddCell(cell);

                        cellText = GVListOfItemsAll.Rows[rowCounter].Cells[4].Text;
                        cell = new PdfPCell(new Phrase(cellText, FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                        gvTable.AddCell(cell);

                        cellText = GVListOfItemsAll.Rows[rowCounter].Cells[5].Text;
                        cell = new PdfPCell(new Phrase(cellText, FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                        gvTable.AddCell(cell);

                        cellText = GVListOfItemsAll.Rows[rowCounter].Cells[6].Text;
                        cell = new PdfPCell(new Phrase(cellText, FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                        gvTable.AddCell(cell);

                        cellText = GVListOfItemsAll.Rows[rowCounter].Cells[7].Text;
                        if (cellText.Length > 0)
                        {
                            TotalQty += float.Parse(cellText.ToString());
                        }
                        cell = new PdfPCell(new Phrase(cellText, FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                        gvTable.AddCell(cell);

                        cellText = GVListOfItemsAll.Rows[rowCounter].Cells[8].Text;
                        if (cellText.Length > 0)
                        {
                            TotalPrice += float.Parse(cellText.ToString());
                        }
                        cell = new PdfPCell(new Phrase(cellText, FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                        gvTable.AddCell(cell);

                        cellText = GVListOfItemsAll.Rows[rowCounter].Cells[9].Text;
                        if (cellText.Length > 0)
                        {
                            GTotal += float.Parse(cellText.ToString());
                        }
                        cell = new PdfPCell(new Phrase(cellText, FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                        gvTable.AddCell(cell);
                    }

                }
                cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                cell.Border = 0;
                gvTable.AddCell(cell);

                cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                cell.Border = 0;
                gvTable.AddCell(cell);

                cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                cell.Border = 0;
                gvTable.AddCell(cell);

                cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                cell.Border = 0;
                gvTable.AddCell(cell);


                cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                cell.Border = 0;
                gvTable.AddCell(cell);


                cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                cell.Border = 0;
                gvTable.AddCell(cell);

                cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                cell.Border = 0;
                gvTable.AddCell(cell);

                cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                cell.Border = 0;
                gvTable.AddCell(cell);


                cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                cell.Border = 0;
                gvTable.AddCell(cell);


                cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                cell.Border = 0;
                gvTable.AddCell(cell);

                cellText = "";
                cell = new PdfPCell(new Phrase(cellText, FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                cell.Border = 0;
                gvTable.AddCell(cell);

                cellText = "";
                cell = new PdfPCell(new Phrase(cellText, FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                cell.Border = 0;
                gvTable.AddCell(cell);

                cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                cell.Border = 0;
                gvTable.AddCell(cell);

                cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                cell.Border = 0;
                gvTable.AddCell(cell);


                cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                cell.Border = 0;
                gvTable.AddCell(cell);


                cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                cell.Border = 0;
                gvTable.AddCell(cell);

                cellText = "Total";
                cell = new PdfPCell(new Phrase(cellText, FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                //cell.Border = 0;
                gvTable.AddCell(cell);

                cell = new PdfPCell(new Phrase(TotalQty.ToString(), FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                gvTable.AddCell(cell);


                cell = new PdfPCell(new Phrase(TotalPrice.ToString(), FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                gvTable.AddCell(cell);

                cellText = "";
                cell = new PdfPCell(new Phrase(GTotal.ToString(), FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                gvTable.AddCell(cell);



                document.Add(gvTable);
                document.NewPage();
                document.Close();
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment;filename=DispatchStock.pdf");
                Response.Buffer = true;
                Response.Clear();
                Response.OutputStream.Write(ms.GetBuffer(), 0, ms.GetBuffer().Length);
                Response.OutputStream.Flush();
                Response.End();
            }


            #endregion
        }

        protected void ddlsitename_OnSelectedIndexChanged(object sender, EventArgs e)
        {

            GVListOfItems.DataSource = null;
            GVListOfItems.DataBind();
            // FillClientIdandName();
        }



        protected void ddlmrfid_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            //GVListOfItems.DataSource = null;
            //GVListOfItems.DataBind();
            //LblResult.Text = "";
            //if (ddlcname.SelectedIndex > 0)
            //{
            //    FillClientid();
            //}
            //else
            //{
            //    Cleardata();
            //}
        }

        protected void DdlClientId_SelectedIndexChanged(object sender, EventArgs e)
        {

            GVListOfItems.DataSource = null;
            GVListOfItems.DataBind();
            LblResult.Text = "";
            if (DdlClientId.SelectedIndex > 1)
            {
                Fillcname();
                LoadMrfids();
            }
            if (DdlClientId.SelectedIndex == 1)
            {
                ddlcname.SelectedIndex = 1;
                LoadMrfids();
            }
            if (DdlClientId.SelectedIndex == 0)
            {
                Cleardata();
            }
        }

        protected void FillClientid()
        {

            if (ddlcname.SelectedIndex > 0)
            {
                DdlClientId.SelectedValue = ddlcname.SelectedValue;
            }
            else
            {
                DdlClientId.SelectedIndex = 0;
            }

        }

        protected void Fillcname()
        {
            string SqlQryForCname = "Select clientid from Clients where clientid='" + DdlClientId.SelectedValue + "'";
            DataTable dtCname = null;


            dtCname = config.ExecuteAdaptorAsyncWithQueryParams(SqlQryForCname).Result;

            if (dtCname.Rows.Count > 0)
                ddlcname.SelectedValue = dtCname.Rows[0]["clientid"].ToString();

        }

        protected void LoadMrfids()
        {
            ddlmrfid.Items.Clear();

            DataTable dt = null;



            if (DdlClientId.SelectedIndex > 1)
            {
                string SqlQry = "select distinct MRFid from MRF   Where DispatchStatus=1  and Clientid='" + DdlClientId.SelectedValue + "'";

                dt = config.ExecuteAdaptorAsyncWithQueryParams(SqlQry).Result;

                //DataTable dt = SqlHelper.Instance.GetTableByQuery(SqlQry);
                if (dt.Rows.Count > 0)
                {
                    ddlmrfid.DataValueField = "mrfid";
                    ddlmrfid.DataTextField = "mrfid";
                    ddlmrfid.DataSource = dt;
                    ddlmrfid.DataBind();
                }
                ddlmrfid.Items.Insert(0, "-Select-");
                ddlmrfid.Items.Insert(1, "All");
            }
            if (DdlClientId.SelectedIndex == 1)
            {
                string SqlQry = "select distinct MRFid from MRF   Where DispatchStatus=1 order by MRFid";

                dt = config.ExecuteAdaptorAsyncWithQueryParams(SqlQry).Result;


                //DataTable dt = SqlHelper.Instance.GetTableByQuery(SqlQry);
                if (dt.Rows.Count > 0)
                {
                    ddlmrfid.DataValueField = "mrfid";
                    ddlmrfid.DataTextField = "mrfid";
                    ddlmrfid.DataSource = dt;
                    ddlmrfid.DataBind();
                }
                ddlmrfid.Items.Insert(0, "-Select-");
                ddlmrfid.Items.Insert(1, "All");

            }

            if (DdlClientId.SelectedIndex == 0)
            {
                ddlmrfid.Items.Insert(0, "-Select-");
                ddlmrfid.SelectedIndex = 0;
                DdlClientId.Items.Clear();
            }
        }
    }
}