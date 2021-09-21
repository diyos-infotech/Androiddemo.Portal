using System;
using System.Data;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using OfficeOpenXml;
/// <summary>
/// 
/// </summary>
public class GridViewExportUtil
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="gv"></param>
    public   void Export(string fileName, GridView gv)
    {
        GridViewExportUtil gve = new GridViewExportUtil();
        string style = @"<style> .text { mso-number-format:\@; } </style> ";
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.AddHeader(
            "content-disposition", string.Format("attachment; filename={0}", fileName));
        HttpContext.Current.Response.ContentType = "application/ms-excel";

        using (StringWriter sw = new StringWriter())
        {
            using (HtmlTextWriter htw = new HtmlTextWriter(sw))
            {
                //  Create a form to contain the grid
                Table table = new Table();
                table.BorderStyle = BorderStyle.Solid;
                table.GridLines = GridLines.Both;
                //  add the header row to the table
                if (gv.HeaderRow != null)
                {
                   gve .PrepareControlForExport(gv.HeaderRow);
                    table.Rows.Add(gv.HeaderRow);
                }

                //  add each of the data rows to the table
                foreach (GridViewRow row in gv.Rows)
                {
                    gve.PrepareControlForExport(row);
                    table.Rows.Add(row);
                }

                //  add the footer row to the table
                if (gv.FooterRow != null)
                {
                    gve.PrepareControlForExport(gv.FooterRow);
                    table.Rows.Add(gv.FooterRow);
                }

                //  render the table into the htmlwriter
                table.RenderControl(htw);

                //  render the htmlwriter into the response
                HttpContext.Current.Response.Write(style);
                HttpContext.Current.Response.Write(sw.ToString());
                HttpContext.Current.Response.End();
            }
        }
    }

    /// <summary>
    /// Replace any of the contained controls with literals
    /// </summary>
    /// <param name="control"></param>
    private  void PrepareControlForExport(Control control)
    {
        GridViewExportUtil gve = new GridViewExportUtil();
        for (int i = 0; i < control.Controls.Count; i++)
        {
            Control current = control.Controls[i];
            if (current is LinkButton)
            {
                control.Controls.Remove(current);
                control.Controls.AddAt(i, new LiteralControl((current as LinkButton).Text));
            }
            else if (current is ImageButton)
            {
                control.Controls.Remove(current);
                control.Controls.AddAt(i, new LiteralControl((current as ImageButton).AlternateText));
            }
            else if (current is HyperLink)
            {
                control.Controls.Remove(current);
                control.Controls.AddAt(i, new LiteralControl((current as HyperLink).Text));
            }
            else if (current is DropDownList)
            {
                control.Controls.Remove(current);
                control.Controls.AddAt(i, new LiteralControl((current as DropDownList).SelectedItem.Text));
            }
            else if (current is CheckBox)
            {
                control.Controls.Remove(current);
                control.Controls.AddAt(i, new LiteralControl((current as CheckBox).Checked ? "True" : "False"));
            }

            if (current.HasControls())
            {
                gve.PrepareControlForExport(current);
            }
        }
    }

    public  void ExportGrid(string fileName, HiddenField hidGridView)
    {
        string style = @"<style> .text { mso-number-format:\@; } </style> ";
        HttpContext.Current.Response.ClearContent();
        HttpContext.Current.Response.ContentType = "application/ms-excel";
        HttpContext.Current.Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", fileName));
        HttpContext.Current.Response.Charset = "";
        System.IO.StringWriter stringwriter = new System.IO.StringWriter();
        stringwriter.Write(System.Web.HttpUtility.HtmlDecode(hidGridView.Value));
        HttpContext.Current.Response.Write(style);
        HttpContext.Current.Response.Write(stringwriter.ToString());
        HttpContext.Current.Response.End();
    }

    public   void ExporttoExcel1(DataTable table, string line, string line1, string line2)
    {


        string filename = line2 + ".xls";
        string style = @"<style> .text { mso-number-format:\@; } </style> ";
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ClearContent();
        HttpContext.Current.Response.ClearHeaders();
        HttpContext.Current.Response.Buffer = true;
        HttpContext.Current.Response.ContentType = "application/ms-excel";
        HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
        HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename='" + line2 + "'.xls");

        HttpContext.Current.Response.Charset = "utf-8";
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");

        //sets font
        HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
        HttpContext.Current.Response.Write("<BR><BR><BR>");

        //sets the table border, cell spacing, border color, font of the text, background, foreground, font height
        HttpContext.Current.Response.Write("<Table border='1' bgColor='#ffffff' " +
          "borderColor='#000000' cellSpacing='0' cellPadding='0' " +
          "style='font-size:11.0pt; font-family:calibri; background:white;'>");

        //am getting my grid's column headers
        int columnscount = table.Columns.Count;

        HttpContext.Current.Response.Write("<TR valign='top'>");

        HttpContext.Current.Response.Write("<Td align='center' colspan= " + columnscount + ">");
        HttpContext.Current.Response.Write("<B>");
        HttpContext.Current.Response.Write(line);
        HttpContext.Current.Response.Write("</B>");

        HttpContext.Current.Response.Write("</Td>");
        HttpContext.Current.Response.Write("</TR>");


        HttpContext.Current.Response.Write("<TR valign='top'>");

        HttpContext.Current.Response.Write("<Td align='left' colspan= " + columnscount + ">");
        HttpContext.Current.Response.Write("<B>");
        HttpContext.Current.Response.Write(line1);
        HttpContext.Current.Response.Write("</B>");
        HttpContext.Current.Response.Write("</Td>");

        HttpContext.Current.Response.Write("</TR>");


        for (int j = 0; j < columnscount; j++)
        {
            //write in new column

            HttpContext.Current.Response.Write("<Td valign='middle'>");

            //Get column headers  and make it as bold in excel columns
            HttpContext.Current.Response.Write("<B>");
            HttpContext.Current.Response.Write(table.Columns[j].ToString());

            HttpContext.Current.Response.Write("</B>");
            HttpContext.Current.Response.Write("</Td>");
        }

        HttpContext.Current.Response.Write("</TR>");

        foreach (DataRow row in table.Rows)
        {//write in new row
            HttpContext.Current.Response.Write("<TR>");
            for (int i = 0; i < table.Columns.Count; i++)
            {
                HttpContext.Current.Response.Write("<Td>");
                HttpContext.Current.Response.Write(row[i].ToString());
                HttpContext.Current.Response.Write("</Td>");
                HttpContext.Current.Response.Write(style);
            }

            HttpContext.Current.Response.Write("</TR>");
        }
        HttpContext.Current.Response.Write("</Table>");
        HttpContext.Current.Response.Write("</font>");

        HttpContext.Current.Response.Flush();
        HttpContext.Current.Response.End();
    }

    public  void NewExport(string fileName, GridView gv)
    {


        DataTable dt = new DataTable();

        // add the columns to the datatable            
        if (gv.HeaderRow != null)
        {

            for (int i = 0; i < gv.HeaderRow.Cells.Count; i++)
            {
                dt.Columns.Add(gv.HeaderRow.Cells[i].Text);
            }

        }

        //  add each of the data rows to the table
        foreach (GridViewRow row in gv.Rows)
        {
            DataRow dr;
            dr = dt.NewRow();

            for (int i = 0; i < row.Cells.Count; i++)
            {
                dr[i] = row.Cells[i].Text;
            }
            dt.Rows.Add(dr);
        }



        //  add the footer row to the table
        if (gv.FooterRow != null)
        {
            DataRow dr;
            dr = dt.NewRow();

            for (int i = 0; i < gv.FooterRow.Cells.Count; i++)
            {
                dr[i] = gv.FooterRow.Cells[i].Text.Replace("&nbsp;", "");
            }


            dt.Rows.Add(dr);
        }

        var products = dt;
        ExcelPackage excel = new ExcelPackage();
        var workSheet = excel.Workbook.Worksheets.Add(fileName);
        var totalCols = products.Columns.Count;
        var totalRows = products.Rows.Count;

        for (var col = 1; col <= totalCols; col++)
        {
            workSheet.Cells[1, col].Value = products.Columns[col - 1].ColumnName;
            workSheet.Cells[1, col].Style.Font.Bold = true;

        }
        for (var row = 1; row <= totalRows; row++)
        {
            for (var col = 0; col < totalCols; col++)
            {
                workSheet.Cells[row + 1, col + 1].Value = products.Rows[row - 1][col];
            }
        }


        using (var memoryStream = new MemoryStream())
        {
            HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment ;filename=\"" + fileName + "\"");
            excel.SaveAs(memoryStream);
            memoryStream.WriteTo(HttpContext.Current.Response.OutputStream);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }

    }

    public void ExporttoExcelNewPaysheet(DataTable table, string line, string line2, string filename)
    {
        string style = @"<style> .text { mso-number-format:\@; } </style> ";
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ClearContent();
        HttpContext.Current.Response.ClearHeaders();
        HttpContext.Current.Response.Buffer = true;
        HttpContext.Current.Response.ContentType = "application/ms-excel";
        HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
        HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + filename + ".xls");

        HttpContext.Current.Response.Charset = "utf-8";
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");

        //sets font
        HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
        HttpContext.Current.Response.Write("<BR><BR><BR>");

        //sets the table border, cell spacing, border color, font of the text, background, foreground, font height
        HttpContext.Current.Response.Write("<Table border='1' bgColor='#ffffff' " +
          "borderColor='#000000' cellSpacing='0' cellPadding='0' " +
          "style='font-size:11.0pt; font-family:calibri; background:white;'>");

        //am getting my grid's column headers
        int columnscount = table.Columns.Count;

        HttpContext.Current.Response.Write("<TR valign='top'>");
        HttpContext.Current.Response.Write("<Td align='left' colspan= '" + columnscount + "'>");
        HttpContext.Current.Response.Write("<B>");
        HttpContext.Current.Response.Write(line);
        HttpContext.Current.Response.Write("</B>");
        HttpContext.Current.Response.Write("</Td>");
        HttpContext.Current.Response.Write("</TR>");

        HttpContext.Current.Response.Write("<TR valign='top'>");
        HttpContext.Current.Response.Write("<Td align='Left' colspan= '" + columnscount + "'>");
        HttpContext.Current.Response.Write("<B>");
        HttpContext.Current.Response.Write(line2);
        HttpContext.Current.Response.Write("</B>");
        HttpContext.Current.Response.Write("</Td>");
        HttpContext.Current.Response.Write("</TR>");

        columnscount = 24;
        HttpContext.Current.Response.Write("<TR valign='top'>");
        HttpContext.Current.Response.Write("<Td align='center' colspan= '" + columnscount + "'>");
        HttpContext.Current.Response.Write("<B>");
        HttpContext.Current.Response.Write("");
        HttpContext.Current.Response.Write("</B>");
        HttpContext.Current.Response.Write("</Td>");

        columnscount = 17;
        HttpContext.Current.Response.Write("<Td align='center' colspan= '" + columnscount + "'>");
        HttpContext.Current.Response.Write("<B>");
        HttpContext.Current.Response.Write("Presents Salaries");
        HttpContext.Current.Response.Write("</B>");
        HttpContext.Current.Response.Write("</Td>");

        columnscount = 2;
        HttpContext.Current.Response.Write("<Td align='center' colspan= '" + columnscount + "'>");
        HttpContext.Current.Response.Write("<B>");
        HttpContext.Current.Response.Write("Employer Contribution");
        HttpContext.Current.Response.Write("</B>");
        HttpContext.Current.Response.Write("</Td>");

        columnscount = 2;
        HttpContext.Current.Response.Write("<Td align='center' colspan= '" + columnscount + "'>");
        HttpContext.Current.Response.Write("<B>");
        HttpContext.Current.Response.Write("Employee Contribution");
        HttpContext.Current.Response.Write("</B>");
        HttpContext.Current.Response.Write("</Td>");

        columnscount = 9;
        HttpContext.Current.Response.Write("<Td align='left' colspan= '" + columnscount + "'>");
        HttpContext.Current.Response.Write("<B>");
        HttpContext.Current.Response.Write("");
        HttpContext.Current.Response.Write("</B>");
        HttpContext.Current.Response.Write("</Td>");

        HttpContext.Current.Response.Write("</TR>");

        columnscount = table.Columns.Count;
        for (int j = 0; j < columnscount; j++)
        {
            //write in new column

            HttpContext.Current.Response.Write("<Td valign='middle'>");

            //Get column headers  and make it as bold in excel columns
            HttpContext.Current.Response.Write("<B>");
            HttpContext.Current.Response.Write(table.Columns[j].ToString());

            HttpContext.Current.Response.Write("</B>");
            HttpContext.Current.Response.Write("</Td>");
        }

        HttpContext.Current.Response.Write("</TR>");

        foreach (DataRow row in table.Rows)
        {//write in new row
            HttpContext.Current.Response.Write("<TR>");
            for (int i = 0; i < table.Columns.Count; i++)
            {
                HttpContext.Current.Response.Write("<Td>");
                HttpContext.Current.Response.Write(row[i].ToString());
                HttpContext.Current.Response.Write("</Td>");
                HttpContext.Current.Response.Write(style);
            }

            HttpContext.Current.Response.Write("</TR>");
        }
        HttpContext.Current.Response.Write("</Table>");
        HttpContext.Current.Response.Write("</font>");

        HttpContext.Current.Response.Flush();
        HttpContext.Current.Response.End();
    }

    public void NewExportExcel(string fileName, DataTable dt)
    {

        var products = dt;
        ExcelPackage excel = new ExcelPackage();
        var workSheet = excel.Workbook.Worksheets.Add(fileName);
        var totalCols = products.Columns.Count;
        var totalRows = products.Rows.Count;

        for (var col = 1; col <= totalCols; col++)
        {
            workSheet.Cells[1, col].Value = products.Columns[col - 1].ColumnName;
            workSheet.Cells[1, col].Style.Font.Bold = true;

        }
        for (var row = 1; row <= totalRows; row++)
        {
            for (var col = 0; col < totalCols; col++)
            {
                workSheet.Cells[row + 1, col + 1].Value = products.Rows[row - 1][col];

            }
        }
        using (var memoryStream = new MemoryStream())
        {
            HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment ;filename=\"" + fileName + "\"");
            excel.SaveAs(memoryStream);
            memoryStream.WriteTo(HttpContext.Current.Response.OutputStream);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }
    }

}
