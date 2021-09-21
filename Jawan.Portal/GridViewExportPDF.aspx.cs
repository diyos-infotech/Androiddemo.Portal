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
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using System.Data;
using KLTS.Data;
using Jawan.Portal.DAL;
namespace Jawan.Portal
{
    public partial class GridViewExportPDF : System.Web.UI.Page
    {
        AppConfiguration config = new AppConfiguration();
        GridViewExportUtil gve = new GridViewExportUtil();
        protected void Page_Load(object sender, EventArgs e)
        {
            string selectquery = " select * from unitbillbreakup  where unitid ='1001'";
            DataTable dt = config.ExecuteAdaptorAsyncWithQueryParams(selectquery).Result;
            gvProducts.DataSource = dt;
            gvProducts.DataBind();

        }
        protected void lnkExport_Click(object sender, EventArgs e)
        {
            //remove unwanted controls
            PrepareForExport(gvProducts);
            //export to pdf without formatting
            ExportToPDF();
        }

        //Confirms that an HtmlForm control is rendered for the
        //specified ASP.NET server control at run time.
        public override void VerifyRenderingInServerForm(Control control)
        { }

        private void PrepareForExport(Control ctrl)
        {
            //iterate through all the grid controls
            foreach (Control childControl in ctrl.Controls)
            {
                //if the control type is link button, remove it
                //from the collection
                if (childControl.GetType() == typeof(LinkButton))
                {
                    ctrl.Controls.Remove(childControl);
                }
                //if the child control is not empty, repeat the process
                // for all its controls
                else if (childControl.HasControls())
                {
                    PrepareForExport(childControl);
                }
            }
        }
        private void ExportToPDF()
        {
            //set the cotent type to PDF
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=Products.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            //hide the link button column
            // gvProducts.Columns[4].Visible = false;

            //Outputs server control content to a provided System.Web.UI.HtmlTextWriter
            gvProducts.RenderControl(hw);

            //load the html content to the string reader
            StringReader sr = new StringReader(sw.ToString());

            //HTMLDocument
            //Document(Rectangle pageSize, float marginLeft, float marginRight, float marginTop, float marginBottom)
            Document document = new Document(PageSize.A4, 10f, 10f, 10f, 0f);

            //iText class that allows you to convert HTML to PDF
            HTMLWorker htmlWorker = new HTMLWorker(document);

            //When this PdfWriter is added to a certain PdfDocument,
            //the PDF representation of every Element added to this Document will be written to the outputstream.
            PdfWriter.GetInstance(document, Response.OutputStream);

            //open the document
            document.Open();

            htmlWorker.Parse(sr);

            //close the document stream
            document.Close();

            //write the content to the response stream
            Response.Write(document);
            Response.End();
        }

        protected void ExportToPDFWithFormatting()
        {
            //link button column is excluded from the list
            int colCount = gvProducts.Columns.Count - 1;

            //Create a table
            PdfPTable table = new PdfPTable(colCount);
            table.HorizontalAlignment = 0;

            //create an array to store column widths
            int[] colWidths = new int[gvProducts.Columns.Count];

            PdfPCell cell;
            string cellText;
            //create the header row
            for (int colIndex = 0; colIndex < colCount; colIndex++)
            {
                //set the column width
                colWidths[colIndex] = (int)gvProducts.Columns[colIndex].ItemStyle.Width.Value;

                //fetch the header text
                cellText = Server.HtmlDecode(gvProducts.HeaderRow.Cells[colIndex].Text);

                //create a new cell with header text
                cell = new PdfPCell(new Phrase(cellText));

                //set the background color for the header cell
                cell.BackgroundColor = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#d1dbe0"));

                //add the cell to the table. we dont need to create a row and add cells to the row
                //since we set the column count of the table to 4, it will automatically create row for
                //every 4 cells
                table.AddCell(cell);
            }

            //export rows from GridView to table
            for (int rowIndex = 0; rowIndex < gvProducts.Rows.Count; rowIndex++)
            {
                if (gvProducts.Rows[rowIndex].RowType == DataControlRowType.DataRow)
                {
                    for (int j = 0; j < gvProducts.Columns.Count - 1; j++)
                    {
                        //fetch the column value of the current row
                        cellText = Server.HtmlDecode(gvProducts.Rows[rowIndex].Cells[j].Text);

                        //create a new cell with column value
                        cell = new PdfPCell(new Phrase(cellText));

                        //Set Color of Alternating row
                        if (rowIndex % 2 != 0)
                        {
                            cell.BackgroundColor = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#9ab2ca"));
                        }
                        else
                        {
                            cell.BackgroundColor = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#f1f5f6"));
                        }
                        //add the cell to the table
                        table.AddCell(cell);
                    }
                }
            }

            //Create the PDF Document
            Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);

            PdfWriter.GetInstance(pdfDoc, Response.OutputStream);

            //open the stream
            pdfDoc.Open();

            //add the table to the document
            pdfDoc.Add(table);

            //close the document stream
            pdfDoc.Close();

            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;" + "filename=Product.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Write(pdfDoc);
            Response.End();
        }

    }
}