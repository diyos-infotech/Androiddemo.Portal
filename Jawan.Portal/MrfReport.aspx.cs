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
    public partial class MrfReport : System.Web.UI.Page
    {
        DataTable dt;
        AppConfiguration config = new AppConfiguration();
        GridViewExportUtil gve = new GridViewExportUtil();
        string EmpIDPrefix = "";
        string CmpIDPrefix = "";
        string Elength = "";
        string Clength = "";


        //Varialbles for Footer Total

        float TotalQty = 0;
        float TotalPrice = 0;
        float GrandTotal = 0;

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
                }
                else
                {
                    Response.Redirect("login.aspx");
                }
                FillClientIdandName();
            }

        }
        protected void GetWebConfigdata()
        {
            EmpIDPrefix = GlobalData.Instance.GetEmployeeIDPrefix();
            Elength = (EmpIDPrefix.Trim().Length + 1).ToString();
            CmpIDPrefix = GlobalData.Instance.GetClientIDPrefix();
            Clength = (CmpIDPrefix.Trim().Length + 1).ToString();
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


        protected void ddlcname_OnSelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddlcname.SelectedIndex > 1)
            {
                FillClientid();
            }
            if (ddlcname.SelectedIndex == 1)
            {
                ddlclientid.SelectedIndex = 1;
            }
        }

        protected void DdlClientId_SelectedIndexChanged(object sender, EventArgs e)
        {


            if (ddlclientid.SelectedIndex > 1)
            {
                Fillcname();
            }
            if (ddlclientid.SelectedIndex == 1)
            {
                ddlcname.SelectedIndex = 1;
            }
        }

        protected void FillClientIdandName()
        {

            string selectclientid = "select clientid from clients  Order By Right(clientid,6)";
            string selectclientname = "select  clientid,Clientname from Clients    where clientstatus=1 order by Clientname";
            DataTable dtclientnames = null;

            dt = config.ExecuteAdaptorAsyncWithQueryParams(selectclientid).Result;
            dtclientnames = config.ExecuteAdaptorAsyncWithQueryParams(selectclientname).Result;

            ddlclientid.Items.Clear();
            ddlcname.Items.Clear();


            if (dt.Rows.Count > 0)
            {
                ddlclientid.DataTextField = "clientid";
                ddlclientid.DataValueField = "clientid";
                ddlclientid.DataSource = dt;
                ddlclientid.DataBind();
                ddlclientid.Items.Insert(0, "-Select-");
                ddlclientid.Items.Insert(1, "All");

            }
            else
            {
                ddlclientid.Items.Insert(0, "-Select-");
            }

            if (dtclientnames.Rows.Count > 0)
            {
                ddlcname.DataTextField = "Clientname";
                ddlcname.DataValueField = "clientid";
                ddlcname.DataSource = dtclientnames;
                ddlcname.DataBind();
                ddlcname.Items.Insert(0, "-Select-");
                ddlcname.Items.Insert(1, "All");
            }
            else
            {

                ddlcname.Items.Insert(0, "-Select-");
            }

        }

        protected void Fillcname()
        {
            string SqlQryForCname = "Select clientid from Clients where clientid='" + ddlclientid.SelectedValue + "'";
            DataTable dtCname = null;
            dtCname = config.ExecuteAdaptorAsyncWithQueryParams(SqlQryForCname).Result;
            if (dtCname.Rows.Count > 0)
                ddlcname.SelectedValue = dtCname.Rows[0]["clientid"].ToString();

        }

        protected void FillClientid()
        {
            string SqlQryForCid = "Select Clientid from Clients where Clientid='" + ddlcname.SelectedValue + "'";
            DataTable dtCname = config.ExecuteAdaptorAsyncWithQueryParams(SqlQryForCid).Result;
            dtCname = config.ExecuteAdaptorAsyncWithQueryParams(SqlQryForCid).Result;


            if (dtCname.Rows.Count > 0)
                ddlclientid.SelectedValue = dtCname.Rows[0]["Clientid"].ToString();

        }

        public void BindData(string SqlQry)
        {
            DataTable DtResult = null;

            DtResult = config.ExecuteAdaptorAsyncWithQueryParams(SqlQry).Result;

            if (DtResult.Rows.Count > 0)
            {
                GVListOfItems.DataSource = DtResult;
                GVListOfItems.DataBind();

                return;
            }
            else
            {

                return;
            }
        }

        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            Displaydate();
        }

        protected void Displaydate()
        {
            GVListOfItems.DataSource = null;
            GVListOfItems.DataBind();
            DataTable dt = null;
            string Mrfitems = "0";

            #region New code as on 07/01/2014 by venkat

            string date = "";

            if (txtdate.Text.Trim().Length > 0)
            {
                date = Convert.ToDateTime(txtdate.Text.Trim(), CultureInfo.GetCultureInfo("en-gb")).ToString();
            }


            #endregion


            if (ddlclientid.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showlalert", "alert('Please Select Client');", true);
                return;
            }
            if (ddlreporttype.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showlalert", "alert('Please Select Report Type');", true);
                return;
            }
            if (ddldaytype.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showlalert", "alert('Please Select Day Type');", true);
                return;
            }
            if (ddlclientid.SelectedIndex > 1)
            {
                if (ddlreporttype.SelectedIndex == 1 && ddldaytype.SelectedIndex == 1)
                {
                    Mrfitems = "select distinct(mrf.ItemId),si.ItemName,mrf.MRFId,mrf.Quantity,mrf.ClientId,c.clientname,mrf.date," +
                        " mrf.SellingPrice,mrf.Quantity*mrf.SellingPrice as Total from MRF inner join Clients " +
                        " c on mrf.ClientId=c.ClientId inner join StockItemList si on mrf.ItemId=si.ItemId where Status =-1 and mrf.clientid ='" +
                        ddlclientid.SelectedValue + "' and CONVERT(varchar,Month(date))=MONTH('" + date + "') order by mrf.MRFId ";
                }
                if (ddlreporttype.SelectedIndex == 1 && ddldaytype.SelectedIndex == 2)
                {
                    Mrfitems = "select distinct(mrf.ItemId),si.ItemName,mrf.MRFId,mrf.Quantity,mrf.ClientId,c.clientname,mrf.date," +
                        " mrf.SellingPrice,mrf.Quantity*mrf.SellingPrice as Total from MRF inner join Clients c on" +
                        " mrf.ClientId=c.ClientId inner join StockItemList si on mrf.ItemId=si.ItemId where Status =-1 and mrf.clientid ='" +
                        ddlclientid.SelectedValue + "' and CONVERT(varchar,Month(date))" +
                    " =MONTH('" + date + "') and CONVERT(varchar,day(date))=day('" + date + "') order by mrf.MRFId";
                }
                if (ddlreporttype.SelectedIndex == 2 && ddldaytype.SelectedIndex == 1)
                {
                    Mrfitems = "select distinct(mrf.ItemId),si.ItemName,mrf.MRFId,mrf.Quantity,mrf.ClientId,c.clientname,mrf.DispachedDate as date," +
                        " mrf.SellingPrice,mrf.Quantity*mrf.SellingPrice as Total from MRF inner join Clients c" +
                        " on mrf.ClientId=c.ClientId inner join StockItemList si on mrf.ItemId=si.ItemId where Status =1 and mrf.clientid ='"
                        + ddlclientid.SelectedValue + "' and CONVERT(varchar,Month(DispachedDate))=MONTH('" + date + "') order by mrf.MRFId";
                }
                if (ddlreporttype.SelectedIndex == 2 && ddldaytype.SelectedIndex == 2)
                {
                    Mrfitems = "select distinct(mrf.ItemId),si.ItemName,mrf.MRFId,mrf.Quantity,mrf.ClientId,c.clientname,mrf.DispachedDate as date," +
                        " mrf.SellingPrice,mrf.Quantity*mrf.SellingPrice as Total from MRF inner join Clients c " +
                        " on mrf.ClientId=c.ClientId inner join StockItemList si on mrf.ItemId=si.ItemId where Status =1 and mrf.clientid ='" +
                        ddlclientid.SelectedValue + "' and CONVERT(varchar,Month(DispachedDate)) =MONTH('" +
                        date + "') and CONVERT(varchar,day(DispachedDate))=day('" + date + "') order by mrf.MRFId";
                }
            }
            if (ddlclientid.SelectedIndex == 1)
            {

                if (ddlreporttype.SelectedIndex == 1 && ddldaytype.SelectedIndex == 1)
                {
                    Mrfitems = "select distinct(mrf.ItemId),si.ItemName,mrf.MRFId,mrf.Quantity,mrf.ClientId,c.clientname,mrf.date," +
                        " mrf.SellingPrice,mrf.Quantity*mrf.SellingPrice as Total from MRF inner join Clients " +
                        " c on mrf.ClientId=c.ClientId inner join StockItemList si on mrf.ItemId=si.ItemId where Status =-1" +
                        " and CONVERT(varchar,Month(date))=MONTH('" + date + "') order by mrf.MRFId ";
                }
                if (ddlreporttype.SelectedIndex == 1 && ddldaytype.SelectedIndex == 2)
                {
                    Mrfitems = "select distinct(mrf.ItemId),si.ItemName,mrf.MRFId,mrf.Quantity,mrf.ClientId,c.clientname,mrf.date," +
                        " mrf.SellingPrice,mrf.Quantity*mrf.SellingPrice as Total from MRF inner join Clients c on" +
                        " mrf.ClientId=c.ClientId inner join StockItemList si on mrf.ItemId=si.ItemId where Status =-1 " +
                        " and CONVERT(varchar,Month(date))" +
                    " =MONTH('" + date + "') and CONVERT(varchar,day(date))=day('" + date + "') order by mrf.MRFId";
                }
                if (ddlreporttype.SelectedIndex == 2 && ddldaytype.SelectedIndex == 1)
                {
                    Mrfitems = "select distinct(mrf.ItemId),si.ItemName,mrf.MRFId,mrf.Quantity,mrf.ClientId,c.clientname,mrf.DispachedDate as date," +
                        " mrf.SellingPrice,mrf.Quantity*mrf.SellingPrice as Total from MRF inner join Clients c" +
                        " on mrf.ClientId=c.ClientId inner join StockItemList si on mrf.ItemId=si.ItemId where Status =1 " +
                        " and CONVERT(varchar,Month(DispachedDate))=MONTH('" + date + "') order by mrf.MRFId";
                }
                if (ddlreporttype.SelectedIndex == 2 && ddldaytype.SelectedIndex == 2)
                {
                    Mrfitems = "select distinct(mrf.ItemId),si.ItemName,mrf.MRFId,mrf.Quantity,mrf.ClientId,c.clientname,mrf.DispachedDate as date," +
                        " mrf.SellingPrice,mrf.Quantity*mrf.SellingPrice as Total from MRF inner join Clients c " +
                        " on mrf.ClientId=c.ClientId inner join StockItemList si on mrf.ItemId=si.ItemId where Status =1 " +
                        " and CONVERT(varchar,Month(DispachedDate)) =MONTH('" +
                        date + "') and CONVERT(varchar,day(DispachedDate))=day('" + date + "') order by mrf.MRFId";
                }

            }
            BindData(Mrfitems);

        }

        protected void GVListOfItems_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblqty = e.Row.FindControl("lblqty") as Label;
                TotalQty += float.Parse(lblqty.Text);
                Label lblprice = e.Row.FindControl("lblPrice") as Label;
                TotalPrice += float.Parse(lblprice.Text);
                Label lblTotal = e.Row.FindControl("lblTotal") as Label;
                GrandTotal += float.Parse(lblTotal.Text);
            }

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblTotalQty = e.Row.FindControl("lblTotalQty") as Label;
                lblTotalQty.Text = TotalQty.ToString();
                Label lblTotalprice = e.Row.FindControl("lblTotalPrice") as Label;
                lblTotalprice.Text = TotalPrice.ToString();
                Label lblGTotal = e.Row.FindControl("lblGTotal") as Label;
                lblGTotal.Text = GrandTotal.ToString();
            }
        }

        protected void GVListOfItems_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GVListOfItems.PageIndex = e.NewPageIndex;
            Displaydate();
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
                float[] widtlogo = { 0.8f, 1.2f, 1f, 3.5f, 1.3f, 4.5f, 1f, 1f, 1f, 1.4f }; //new float[columns]; 
                gvTable.SetWidths(widtlogo);

                uint FONT_SIZE = 8;

                float TotalQty = 0;
                float TotalPrice = 0;
                float GTotal = 0;

                PdfPCell c1 = new PdfPCell(new Phrase("MRF Status Report", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 14, Font.BOLD, BaseColor.BLACK)));
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
                    widtlogo[i] = (int)GVListOfItems.Columns[i].ItemStyle.Width.Value;
                    //fetch the header text
                    cellText = Server.HtmlDecode(GVListOfItems.HeaderRow.Cells[i].Text);
                    cell = new PdfPCell(new Phrase(cellText, FontFactory.GetFont(FontFactory.TIMES_ROMAN, 8, Font.BOLD, BaseColor.BLACK)));
                    cell.HorizontalAlignment = 1;
                    gvTable.AddCell(cell);
                }

                for (int rowCounter = 0; rowCounter < rows; rowCounter++)
                {
                    if (GVListOfItems.Rows[rowCounter].RowType == DataControlRowType.DataRow)
                    {

                        cellText = ((Label)GVListOfItems.Rows[rowCounter].FindControl("lblsno1")).Text;
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
                        cell = new PdfPCell(new Phrase(cellText, FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                        gvTable.AddCell(cell);

                        cellText = ((Label)GVListOfItems.Rows[rowCounter].FindControl("lblqty")).Text;
                        if (cellText.Length > 0)
                        {
                            TotalQty += float.Parse(cellText.ToString());
                        }
                        cell = new PdfPCell(new Phrase(cellText, FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                        gvTable.AddCell(cell);

                        cellText = ((Label)GVListOfItems.Rows[rowCounter].FindControl("lblPrice")).Text;
                        if (cellText.Length > 0)
                        {
                            TotalPrice += float.Parse(cellText.ToString());
                        }
                        cell = new PdfPCell(new Phrase(cellText, FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                        gvTable.AddCell(cell);

                        cellText = ((Label)GVListOfItems.Rows[rowCounter].FindControl("lblTotal")).Text;
                        if (cellText.Length > 0)
                        {
                            GTotal += float.Parse(cellText.ToString());
                        }
                        cell = new PdfPCell(new Phrase(cellText, FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                        gvTable.AddCell(cell);

                        cellText = GVListOfItems.Rows[rowCounter].Cells[9].Text;
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

                cellText = "Total";
                cell = new PdfPCell(new Phrase(cellText, FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                cell.Border = 0;
                gvTable.AddCell(cell);


                cell = new PdfPCell(new Phrase(TotalQty.ToString(), FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                //cell.Border = 0;
                gvTable.AddCell(cell);

                cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                gvTable.AddCell(cell);


                cell = new PdfPCell(new Phrase(GTotal.ToString(), FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                gvTable.AddCell(cell);


                cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                gvTable.AddCell(cell);



                document.Add(gvTable);
                document.NewPage();
                document.Close();
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment;filename=MrfStatus.pdf");
                Response.Buffer = true;
                Response.Clear();
                Response.OutputStream.Write(ms.GetBuffer(), 0, ms.GetBuffer().Length);
                Response.OutputStream.Flush();
                Response.End();

            }
        }

        #region New code as on 07/01/2014 by venkat for getting klts stores database

        //protected string GetKlstores(string database)
        //{
        //    if (System.Configuration.ConfigurationManager.AppSettings["Klstores"] != null)
        //    {
        //        string db = System.Configuration.ConfigurationManager.AppSettings["Klstores"].ToString();
        //        if (db.Length > 0)
        //            database = db;
        //    }

        //    return database;
        //}


        #endregion
    }
}