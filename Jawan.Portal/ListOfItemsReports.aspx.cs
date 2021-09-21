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
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using System.IO;
using KLTS.Data;
using Jawan.Portal.DAL;
namespace Jawan.Portal
{
    public partial class ListOfItemsReports : System.Web.UI.Page
    {
        AppConfiguration config = new AppConfiguration();
        GridViewExportUtil gve = new GridViewExportUtil();
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
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
                dt = config.ExecuteAdaptorAsyncWithQueryParams("select itemid,upper(itemname) as itemname," +
                    " UnitMeasure,Manifactureby,buyingprice  as price,ActualQuantity,(buyingprice*ActualQuantity) as Total " +
                    " from Stockitemlist Order By cast(Itemid as int)").Result;

                if (dt.Rows.Count > 0)
                {
                    GVListOfItems.DataSource = dt;
                    GVListOfItems.DataBind();
                }
                else
                {
                    LblResult.Text = "There Is No Items";
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
                    ClientsLink.Visible = true;
                    CompanyInfoLink.Visible = false;
                    InventoryLink.Visible = true;
                    ReportsLink.Visible = true;
                    SettingsLink.Visible = false;
                    EmployeeReportLink.Visible = false;
                    ClientsReportLink.Visible = false;
                    ExpensesReportsLink.Visible = false;
                    StockInHandReports.Visible = true;
                    DipatchStockReports.Visible = true;


                    break;
                default:
                    break;


            }
        }

        protected void btnPDF_Click(object sender, EventArgs e)
        {
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
                gvTable.TotalWidth = 500f;
                gvTable.LockedWidth = true;
                float[] widtlogo = { 2f, 2f, 2f, 2f, 2f, 2f }; //new float[columns]; 
                gvTable.SetWidths(widtlogo);

                uint FONT_SIZE = 8;

                PdfPCell c1 = new PdfPCell(new Phrase("List of Items Report", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, Font.BOLD, BaseColor.BLACK)));
                c1.Border = 0;
                c1.HorizontalAlignment = 1;
                c1.Colspan = columns;
                gvTable.AddCell(c1);
                PdfPCell cBlank = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, Font.BOLD, BaseColor.BLACK)));
                cBlank.Border = 0;
                cBlank.HorizontalAlignment = 1;
                cBlank.Colspan = columns;
                gvTable.AddCell(cBlank);

                PdfPCell cell;
                string cellText = "";

                for (int i = 0; i < columns; i++)
                {
                    widtlogo[i] = (int)GVListOfItems.Columns[i].ItemStyle.Width.Value;
                    //fetch the header text
                    cellText = Server.HtmlDecode(GVListOfItems.HeaderRow.Cells[i].Text);
                    cell = new PdfPCell(new Phrase(cellText, FontFactory.GetFont(FontFactory.TIMES_ROMAN, 9, Font.BOLD, BaseColor.BLACK)));
                    gvTable.AddCell(cell);
                }

                for (int rowCounter = 0; rowCounter < rows; rowCounter++)
                {
                    if (GVListOfItems.Rows[rowCounter].RowType == DataControlRowType.DataRow)
                    {
                        cellText = ((Label)GVListOfItems.Rows[rowCounter].FindControl("lblItemid")).Text;
                        cell = new PdfPCell(new Phrase(cellText, FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                        gvTable.AddCell(cell);

                        cellText = ((Label)GVListOfItems.Rows[rowCounter].FindControl("lblItemName")).Text;
                        cell = new PdfPCell(new Phrase(cellText, FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                        gvTable.AddCell(cell);

                        cellText = ((Label)GVListOfItems.Rows[rowCounter].FindControl("lblUnits")).Text;
                        cell = new PdfPCell(new Phrase(cellText, FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                        gvTable.AddCell(cell);

                        cellText = ((Label)GVListOfItems.Rows[rowCounter].FindControl("lblManifacturedBy")).Text;
                        cell = new PdfPCell(new Phrase(cellText, FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                        gvTable.AddCell(cell);

                        cellText = ((Label)GVListOfItems.Rows[rowCounter].FindControl("lblMinQty")).Text;
                        cell = new PdfPCell(new Phrase(cellText, FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                        gvTable.AddCell(cell);

                        cellText = ((Label)GVListOfItems.Rows[rowCounter].FindControl("lblCPrice")).Text;
                        cell = new PdfPCell(new Phrase(cellText, FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                        gvTable.AddCell(cell);
                    }
                }
                document.Add(gvTable);
                document.NewPage();
                document.Close();
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment;filename=ListOfItems.pdf");
                Response.Buffer = true;
                Response.Clear();
                Response.OutputStream.Write(ms.GetBuffer(), 0, ms.GetBuffer().Length);
                Response.OutputStream.Flush();
                Response.End();
            }
        }

        float Totalqty = 0;
        float Totalprice = 0;
        float GrandTotal = 0;

        protected void GVListOfItems_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblqty = e.Row.FindControl("lblMinQty") as Label;
                Totalqty += float.Parse(lblqty.Text);
                Label lblprice = e.Row.FindControl("lblCPrice") as Label;
                Totalprice += float.Parse(lblprice.Text);
                Label lblTotal = e.Row.FindControl("lblcTotal") as Label;
                GrandTotal += float.Parse(lblTotal.Text);
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblTotalqty = e.Row.FindControl("lblTotalqty") as Label;
                Label lblTotalPrice = e.Row.FindControl("lblTotalPrice") as Label;
                Label lblGrandTotal = e.Row.FindControl("lblGrandTotal") as Label;

                lblTotalqty.Text = Totalqty.ToString();
                lblTotalPrice.Text = Totalprice.ToString();
                lblGrandTotal.Text = GrandTotal.ToString();
            }
        }
    }
}