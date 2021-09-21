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
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using Jawan.Portal.DAL;
namespace Jawan.Portal
{
    public partial class ReportForStock : System.Web.UI.Page
    {
        AppConfiguration config = new AppConfiguration();
        GridViewExportUtil gve = new GridViewExportUtil();
        string EmpIDPrefix = "";
        string CmpIDPrefix = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            int i = 0;
            GetWebConfigdata();
            if (!IsPostBack)
            {
                if (Session["UserId"] != null && Session["AccessLevel"] != null)
                {
                    //PreviligeUsers(Convert.ToInt32(Session["AccessLevel"]));
                    string PID = Session["AccessLevel"].ToString();
                    //PreviligeUsers(PID);
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
            CmpIDPrefix = Session["CmpIDPrefix"].ToString();
        }

        protected void lbtn_Export_Click(object sender, EventArgs e)
        {
            gve.Export("StockDetails.xls", this.GVListOfItems);
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
                float[] widtlogo = { 1f, 2f, 4f, 2f, 2f, 2f, 2f, 2f }; //new float[columns]; 
                gvTable.SetWidths(widtlogo);

                uint FONT_SIZE = 9;
                string Fromdate = Txt_From_Date.Text;
                string Todate = Txt_ToDate.Text;

                PdfPCell c1 = new PdfPCell(new Phrase("STOCK DETAILS From " + Fromdate + " to " + Todate, FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, Font.BOLD, BaseColor.BLACK)));
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
                        cellText = ((Label)GVListOfItems.Rows[rowCounter].FindControl("lblSno")).Text;
                        cell = new PdfPCell(new Phrase(cellText, FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                        cell.HorizontalAlignment = 1;
                        gvTable.AddCell(cell);

                        cellText = ((Label)GVListOfItems.Rows[rowCounter].FindControl("lblItemId")).Text;
                        cell = new PdfPCell(new Phrase(cellText, FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                        cell.HorizontalAlignment = 1;
                        gvTable.AddCell(cell);

                        cellText = ((Label)GVListOfItems.Rows[rowCounter].FindControl("lblItemName")).Text;
                        cell = new PdfPCell(new Phrase(cellText, FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                        gvTable.AddCell(cell);

                        cellText = ((Label)GVListOfItems.Rows[rowCounter].FindControl("lblOpeningstock")).Text;
                        cell = new PdfPCell(new Phrase(cellText, FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                        cell.HorizontalAlignment = 2;
                        gvTable.AddCell(cell);

                        cellText = ((Label)GVListOfItems.Rows[rowCounter].FindControl("lblInflowStock")).Text;
                        cell = new PdfPCell(new Phrase(cellText, FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                        cell.HorizontalAlignment = 2;
                        gvTable.AddCell(cell);

                        cellText = ((Label)GVListOfItems.Rows[rowCounter].FindControl("lblQuantity")).Text;
                        cell = new PdfPCell(new Phrase(cellText, FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                        cell.HorizontalAlignment = 2;
                        gvTable.AddCell(cell);

                        cellText = ((Label)GVListOfItems.Rows[rowCounter].FindControl("lblResourceReturned")).Text;
                        cell = new PdfPCell(new Phrase(cellText, FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                        cell.HorizontalAlignment = 2;
                        gvTable.AddCell(cell);

                        cellText = ((Label)GVListOfItems.Rows[rowCounter].FindControl("lblClosingStock")).Text;
                        cell = new PdfPCell(new Phrase(cellText, FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                        cell.HorizontalAlignment = 2;
                        gvTable.AddCell(cell);

                        //cellText = ((Label)GVListOfItems.Rows[rowCounter].FindControl("lblIBPrice")).Text;
                        //cell = new PdfPCell(new Phrase(cellText, FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                        //gvTable.AddCell(cell);

                        //cellText = ((Label)GVListOfItems.Rows[rowCounter].FindControl("lblISPrice")).Text;
                        //cell = new PdfPCell(new Phrase(cellText, FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                        //gvTable.AddCell(cell);

                        //cellText = ((Label)GVListOfItems.Rows[rowCounter].FindControl("lblDSPrice")).Text;
                        //cell = new PdfPCell(new Phrase(cellText, FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                        //gvTable.AddCell(cell);
                    }
                }
                document.Add(gvTable);
                document.NewPage();
                document.Close();
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment;filename=StockReport.pdf");
                Response.Buffer = true;
                Response.Clear();
                Response.OutputStream.Write(ms.GetBuffer(), 0, ms.GetBuffer().Length);
                Response.OutputStream.Flush();
                Response.End();
            }

        }
        protected void Btn_Submit_OnClick(object sender, EventArgs e)
        {

            #region Begin Variable Declarations
            var testDate = 0;
            var FromDate = "";
            var ToDate = "";
            var SPName = "";
            var branchtype = "";

            Hashtable HTStockReport = new Hashtable();
            DataTable DtStockReport = null;
            var InventoryType = 0;
            var ReportType = 0;
            #endregion End variable Declarations

            #region Begin Validations
            if (Txt_From_Date.Text.Trim().Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Please Select the From Date');", true);
                return;
            }
            else
            {
                testDate = GlobalData.Instance.CheckEnteredDate(Txt_From_Date.Text);
                if (testDate > 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('You Are Entered Invalid ORDER DATE.Date Format Should be [DD/MM/YYYY].Ex.01/01/1990');", true);
                    return;
                }
                FromDate = Timings.Instance.CheckDateFormat(Txt_From_Date.Text);
            }
            if (Txt_ToDate.Text.Trim().Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Please Select the To Date');", true);
                return;
            }
            else
            {
                testDate = GlobalData.Instance.CheckEnteredDate(Txt_ToDate.Text);
                if (testDate > 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('You Are Entered Invalid ORDER DATE.Date Format Should be [DD/MM/YYYY].Ex.01/01/1990');", true);
                    return;
                }
                ToDate = Timings.Instance.CheckDateFormat(Txt_ToDate.Text);
            }
            #endregion End Validations

            #region Begin Assign Values to the Variables
            SPName = "ReportForStockInflowAndOutFlows";
            InventoryType = Ddl_Inventory_type.SelectedIndex;
            ReportType = Ddl_Report_Type.SelectedIndex;
            branchtype = ddlbranch.SelectedValue;
            #endregion End Assign values to the Variables

            #region Begin Pass Values to the  Hash Table
            HTStockReport.Add("@ClientIdPrefix", CmpIDPrefix);
            HTStockReport.Add("@FromDate", FromDate);
            HTStockReport.Add("@Todate", ToDate);
            HTStockReport.Add("@ReportType", ReportType);
            HTStockReport.Add("@InventoryType", InventoryType);
            HTStockReport.Add("@branchprefix", branchtype);
            #endregion End Pass Values to the  Hash Table

            #region Begin Call Stored Procedure
            DtStockReport =config.ExecuteAdaptorAsyncWithParams(SPName, HTStockReport).Result;
            #endregion End Call Stored Procedure

            #region Begin Assign Data To Gridview
            if (DtStockReport.Rows.Count > 0)
            {
                GVListOfItems.DataSource = DtStockReport;
                GVListOfItems.DataBind();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Data Not Avaialable');", true);
            }
            #endregion End Assign Data To GridView
        }
    }
}