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
using System.Globalization;
using KLTS.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using System.IO;
using Jawan.Portal.DAL;
namespace Jawan.Portal
{
    public partial class ReportonInventoryDaily : System.Web.UI.Page
    {
        AppConfiguration config = new AppConfiguration();
        GridViewExportUtil gve = new GridViewExportUtil();
        DataTable dt;
        string EmpIDPrefix = "";
        string CmpIDPrefix = "";
        string Elength = "";
        string Clength = "";

        //Varialbles for Footer Total

        float TotalQty = 0;
        float TotalPrice = 0;
        float GrandTotal = 0;
        float TotalInflow = 0;
        float TotalOutflow = 0;

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

                if (ddlcname.SelectedIndex > 1)
                {
                    ddlclientid.SelectedValue = ddlcname.SelectedValue;
                }
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
                if (ddlclientid.SelectedIndex > 1)
                {
                    ddlcname.SelectedValue = ddlclientid.SelectedValue;
                }
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

        }

        protected void FillClientid()
        {
            string SqlQryForCid = "Select Clientid from Clients where Clientid='" + ddlcname.SelectedValue + "'";
            DataTable dtCname = config.ExecuteAdaptorAsyncWithQueryParams(SqlQryForCid).Result;

            dtCname = config.ExecuteAdaptorAsyncWithQueryParams(SqlQryForCid).Result;

            //if (dtCname.Rows.Count > 1)
            //{
            //    ddlclientid.SelectedValue = ddlcname.SelectedValue;
            //}
        }


        protected void ddlreporttype_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlItemid.Items.Clear();
            ddlItemname.Items.Clear();

            if (ddlreporttype.SelectedIndex == 3)
            {
                ddlItemid.Enabled = true;
                ddlItemname.Enabled = true;
                string sqlitem = "select  ItemId from StockItemList order by itemid";
                DataTable dtitem = config.ExecuteAdaptorAsyncWithQueryParams(sqlitem).Result;
                if (dtitem.Rows.Count > 0)
                {
                    ddlItemid.DataValueField = "itemid";
                    ddlItemid.DataTextField = "itemid";
                    ddlItemid.DataSource = dtitem;
                    ddlItemid.DataBind();
                }


                string sqlitemName = "select ItemId,itemname from StockItemList order by  itemname";
                DataTable dtitemname = config.ExecuteAdaptorAsyncWithQueryParams(sqlitemName).Result;
                if (dtitemname.Rows.Count > 0)
                {
                    ddlItemname.DataValueField = "itemid";
                    ddlItemname.DataTextField = "itemname";
                    ddlItemname.DataSource = dtitemname;
                    ddlItemname.DataBind();
                }

                ddlclientid.Enabled = false;
                ddlcname.Enabled = false;
                ddlclientid.SelectedIndex = 0;
                ddlcname.SelectedIndex = 0;
            }

            ddlItemid.Items.Insert(0, "--Select--");
            ddlItemname.Items.Insert(0, "--Select--");

            ddlItemid.Items.Insert(1, "ALL");
            ddlItemname.Items.Insert(1, "ALL");


            if (ddlreporttype.SelectedIndex == 2)
            {
                ddlclientid.Enabled = false;
                ddlcname.Enabled = false;
                ddlclientid.SelectedIndex = 0;
                ddlcname.SelectedIndex = 0;
                GVListOfItems.DataSource = null;
                GVListOfItems.DataBind();
                GVitemlist.DataSource = null;
                GVitemlist.DataBind();
                ddlItemid.Enabled = false;
                ddlItemname.Enabled = false;
                ddlItemid.SelectedIndex = 0;
                ddlItemname.SelectedIndex = 0;
            }
            if (ddlreporttype.SelectedIndex == 1)
            {

                ddlclientid.Enabled = true;
                ddlcname.Enabled = true;
                GVListOfItemsInflow.DataSource = null;
                GVListOfItemsInflow.DataBind();
                GVitemlist.DataSource = null;
                GVitemlist.DataBind();
                ddlItemid.Enabled = false;
                ddlItemname.Enabled = false;
                ddlItemid.SelectedIndex = 0;
                ddlItemname.SelectedIndex = 0;
            }
        }

        protected void btn_Submit_Click(object Sender, EventArgs e)
        {
            Displaydata();
        }

        protected void Displaydata()
        {
            GVListOfItems.DataSource = null;
            GVListOfItems.DataBind();
            GVListOfItemsInflow.DataSource = null;
            GVListOfItemsInflow.DataBind();
            GVitemlist.DataSource = null;
            GVitemlist.DataBind();
            DataTable dt = null;
            string Inflow = "";

            #region New code as on 07/01/2014 by venkat

            string date = "";

            if (txtdate.Text.Trim().Length > 0)
            {
                date = Convert.ToDateTime(txtdate.Text.Trim(), CultureInfo.GetCultureInfo("en-gb")).ToString();
            }

            #endregion

            #region When Select Dispatch [25/11/2013] by venkat

            if (ddlreporttype.SelectedIndex == 1)
            {

                if (ddldaytype.SelectedIndex == 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showlalert", "alert('Please Select Day type');", true);
                    return;
                }
                if (txtdate.Text.Trim().Length == 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showlalert", "alert('Please Select date');", true);
                    return;
                }

                if (ddldaytype.SelectedIndex == 1 && ddlclientid.SelectedIndex == 1)
                {
                    string Dispatch = "select m.ItemId,si.ItemName,c.ClientId,c.ClientName, m.DispachedDate as date,m.Quantity,m.SellingPrice," +
                            "(m.Quantity*m.SellingPrice) as Totalcost,month(DispachedDate) from MRF as M inner join " +
                                " Clients c on c.ClientId=m.ClientId inner join StockItemList  as Si on m.ItemId=si.ItemId " +
                                " where CONVERT(varchar,MONTH(DispachedDate))=MONTH('" + date + "') order by CAST(m.ItemId AS Numeric(10,0))";
                    BindData(Dispatch);
                }
                if (ddldaytype.SelectedIndex == 2 && ddlclientid.SelectedIndex == 1)
                {
                    string Dispatch = "select m.ItemId,si.ItemName,c.ClientId,c.ClientName, m.DispachedDate as date,m.Quantity,m.SellingPrice," +
                            "(m.Quantity*m.SellingPrice) as Totalcost,month(DispachedDate) from MRF as M inner join " +
                                " Clients c on c.ClientId=m.ClientId inner join StockItemList  as Si on m.ItemId=si.ItemId " +
                                " where CONVERT(varchar,Day(DispachedDate))=Day('" + date + "') and CONVERT(varchar,MONTH(DispachedDate))=MONTH('" + date + "') order by CAST(m.ItemId AS Numeric(10,0)) ";
                    BindData(Dispatch);
                }
                if (ddldaytype.SelectedIndex == 1 && ddlclientid.SelectedIndex > 1)
                {
                    string Dispatch = "select m.ItemId,si.ItemName,c.ClientId,c.ClientName, m.DispachedDate as date,m.Quantity,m.SellingPrice," +
                           "(m.Quantity*m.SellingPrice) as Totalcost,month(DispachedDate) from MRF as M inner join " +
                               " Clients c on c.ClientId=m.ClientId inner join StockItemList  as Si on m.ItemId=si.ItemId " +
                               " where CONVERT(varchar,MONTH(DispachedDate))=MONTH('" + date + "') and m.ClientId='" + ddlclientid.SelectedValue + "' order by CAST(m.ItemId AS Numeric(10,0)) ";
                    BindData(Dispatch);
                }
                if (ddldaytype.SelectedIndex == 2 && ddlclientid.SelectedIndex > 1)
                {
                    string Dispatch = "select m.ItemId,si.ItemName,c.ClientId,c.ClientName, m.DispachedDate as date,m.Quantity,m.SellingPrice," +
                            "(m.Quantity*m.SellingPrice) as Totalcost,month(DispachedDate) from MRF as M inner join " +
                                " Clients c on c.ClientId=m.ClientId inner join StockItemList  as Si on m.ItemId=si.ItemId " +
                                " where CONVERT(varchar,Day(DispachedDate))=Day('" + date + "') " +
                                " and CONVERT(varchar,MONTH(DispachedDate))=MONTH('" + date + "') and m.ClientId='" + ddlclientid.SelectedValue + "' order by CAST(m.ItemId AS Numeric(10,0)) ";
                    BindData(Dispatch);
                }
            }

            #endregion

            #region When Select Inflow [25/11/2013] by venkat



            if (ddlreporttype.SelectedIndex == 2)
            {

                if (ddldaytype.SelectedIndex == 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showlalert", "alert('Please Select Day type');", true);
                    return;
                }
                if (txtdate.Text.Trim().Length == 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showlalert", "alert('Please Select date');", true);
                    return;
                }



                string day = DateTime.Parse(date).Day.ToString();
                string Monthname = DateTime.Parse(date).Month.ToString();
                string year = DateTime.Parse(date).Year.ToString();
                string GetMonth = "";

                if (ddldaytype.SelectedIndex == 1)
                {
                    //Inflow = "select  id.ItemId, si.ItemName,sum(id.Quantity) as Quantity,round(id.Price,2) as Price ," +
                    //    " sum(id.Quantity*round(id.Price,2))  as Totalcost from InflowDetails id   inner join StockItemList " +
                    //    " si on id.ItemId=si.ItemId  where   CONVERT(varchar,Month(date))=MONTH('" + date + "')" +
                    //    " group by id.ItemId,si.ItemName,id.Price order by  CAST(id.ItemId AS Numeric(10,0))";

                    Inflow = "select  id.ItemId, si.ItemName,id.Quantity as Quantity,round(id.Price,2) as Price ," +
                        " id.Quantity*round(id.Price,2)  as Totalcost,CONVERT(varchar(10),id.date,103) as month from InflowDetails id   inner join StockItemList " +
                        " si on id.ItemId=si.ItemId  where   CONVERT(varchar,Month(date))=MONTH('" + date + "')" +
                        " order by  CAST(id.ItemId AS Numeric(10,0))";

                    dt = config.ExecuteAdaptorAsyncWithQueryParams(Inflow).Result;
                    GVListOfItemsInflow.DataSource = dt;
                    GVListOfItemsInflow.DataBind();
                    if (GVListOfItemsInflow.Rows.Count > 0)
                    {
                        for (int i = 0; i < GVListOfItemsInflow.Rows.Count; i++)
                        {
                            Label lbldate = GVListOfItemsInflow.Rows[i].FindControl("lbldate") as Label;
                            GetMonth = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(int.Parse(Monthname));
                            lbldate.Text = GetMonth.Substring(0, 3) + " / " + year;
                        }
                    }
                    return;

                }
                if (ddldaytype.SelectedIndex == 2)
                {
                    Inflow = "select id.ItemId,si.ItemName,id.Quantity,round(id.Price,2) as Price,(id.Quantity*round(id.Price,2)) as Totalcost" +
                            ",CONVERT(varchar(10),id.date,103) as month from InflowDetails id inner join StockItemList si on id.ItemId=si.ItemId  where " +
                            " CONVERT(varchar,day(date))=day('" + date + "') and CONVERT(varchar,Month(date))=MONTH('" + date + "') order by CAST(id.ItemId AS Numeric(10,0))";

                    dt = config.ExecuteAdaptorAsyncWithQueryParams(Inflow).Result;
                    GVListOfItemsInflow.DataSource = dt;
                    GVListOfItemsInflow.DataBind();
                    if (GVListOfItemsInflow.Rows.Count > 0)
                    {
                        for (int i = 0; i < GVListOfItemsInflow.Rows.Count; i++)
                        {
                            Label lbldate = GVListOfItemsInflow.Rows[i].FindControl("lbldate") as Label;
                            GetMonth = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(int.Parse(Monthname));
                            lbldate.Text = day + " / " + GetMonth.Substring(0, 3) + " / " + year;
                            //lbldate.Text = Convert.ToDateTime(txtdate.Text.Trim(), CultureInfo.GetCultureInfo("en-gb")).ToString();
                        }
                    }
                    return;

                }

            }

            #endregion

            #region When select ItemWise [25/11/2013] by venkat

            if (ddlreporttype.SelectedIndex == 3)
            {
                Displayinflowoutflow();
            }

            #endregion

        }

        protected void Displayinflowoutflow()
        {


            try
            {

                if (txtdate.Text.Trim().Length == 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showlalert", "alert('Please Select date');", true);
                    return;
                }

                if (ddldaytype.SelectedIndex == 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showlalert", "alert('Please Select Day type');", true);
                    return;
                }


                #region Begin New Code For Declaring Variables as on [08-01-2013]
                var ItemIDFromApp = "";
                var GetDate = "";
                var itemType = 0;
                var SearchedDateType = "0";
                #endregion End New Code For Declaring Variables As on [08-01-2013]

                #region Begin New Code For Assign Values To  Variables as on [08-01-2013]
                if (ddlItemid.SelectedIndex > 1)
                {
                    ItemIDFromApp = ddlItemid.SelectedValue;
                }

                if (ddldaytype.SelectedIndex == 2)
                {
                    SearchedDateType = "1";
                }

                if (ddlItemid.SelectedIndex > 1)
                {
                    itemType = 1;
                }

                if (ddldaytype.SelectedIndex > 0)
                {
                    GetDate = Convert.ToDateTime(txtdate.Text.Trim(), CultureInfo.GetCultureInfo("en-gb")).ToString();
                }

                #endregion End New Code For Assign Values To  Variables As on [08-01-2013]

                string ProcedureName = "GetInflowOutFlowStock";
                Hashtable HtInflowOutFlow = new Hashtable();
                HtInflowOutFlow.Add("@ItemIDFromApp", ItemIDFromApp);
                HtInflowOutFlow.Add("@GetDate", GetDate);
                HtInflowOutFlow.Add("@itemType", itemType);
                HtInflowOutFlow.Add("@SearchedDateType", SearchedDateType);
                DataTable DtInflowOutFlow = config.ExecuteAdaptorAsyncWithParams(ProcedureName, HtInflowOutFlow).Result;

                if (DtInflowOutFlow.Rows.Count > 0)
                {
                    GVitemlist.DataSource = DtInflowOutFlow;
                    GVitemlist.DataBind();
                }
                else
                {
                    GVitemlist.DataSource = null;
                    GVitemlist.DataBind();

                    ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('There Is No Stock Details For The Selected Items');", true);
                }

            }

            catch (Exception ex)
            {

            }

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

        protected void BindInflow(string SqlQry)
        {
            DataTable DtResult = null;

            if (DtResult.Rows.Count > 0)
            {
                GVitemlist.DataSource = DtResult;
                GVitemlist.DataBind();

                return;
            }
            else
            {

                return;
            }
        }

        protected void btnPDF_Click(object Sender, EventArgs e)
        {
            #region Pdf for Dispatch Detail as on 04/12/2013 add by Venkat

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
                float[] widtlogo = { 1.2f, 1.8f, 4f, 2.5f, 6f, 1.8f, 1.3f, 1.5f, 1.5f }; //new float[columns]; 
                gvTable.SetWidths(widtlogo);

                uint FONT_SIZE = 10;

                float TotalQty = 0;
                float TotalPrice = 0;
                float GTotal = 0;

                PdfPCell c1 = new PdfPCell(new Phrase("Inflow Details Report", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 15, Font.BOLD, BaseColor.BLACK)));
                c1.Border = 0;
                c1.HorizontalAlignment = 1;
                c1.Colspan = columns;
                gvTable.AddCell(c1);
                PdfPCell cBlank = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 13, Font.BOLD, BaseColor.BLACK)));
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
                    cell = new PdfPCell(new Phrase(cellText, FontFactory.GetFont(FontFactory.TIMES_ROMAN, 11, Font.BOLD, BaseColor.BLACK)));
                    cell.HorizontalAlignment = 1;
                    gvTable.AddCell(cell);
                }

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
                        cell = new PdfPCell(new Phrase(cellText, FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                        gvTable.AddCell(cell);

                        cellText = ((Label)GVListOfItems.Rows[rowCounter].FindControl("lblqty1")).Text;
                        TotalQty += float.Parse(cellText.ToString());
                        cell = new PdfPCell(new Phrase(cellText, FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                        gvTable.AddCell(cell);

                        cellText = ((Label)GVListOfItems.Rows[rowCounter].FindControl("lblPrice1")).Text;
                        TotalPrice += float.Parse(cellText.ToString());
                        cell = new PdfPCell(new Phrase(cellText, FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                        gvTable.AddCell(cell);

                        cellText = ((Label)GVListOfItems.Rows[rowCounter].FindControl("lblTotal1")).Text;
                        GTotal += float.Parse(cellText.ToString());
                        cell = new PdfPCell(new Phrase(cellText, FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                        gvTable.AddCell(cell);
                    }

                }
                cellText = "";
                cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                cell.Border = 0;
                gvTable.AddCell(cell);

                cellText = "";
                cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                cell.Border = 0;
                gvTable.AddCell(cell);

                cellText = "";
                cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                cell.Border = 0;
                gvTable.AddCell(cell);

                cellText = "";
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

                cellText = "";
                cell = new PdfPCell(new Phrase(cellText, FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                cell.Border = 0;
                gvTable.AddCell(cell);

                cellText = "Total";
                cell = new PdfPCell(new Phrase(cellText, FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                gvTable.AddCell(cell);


                cell = new PdfPCell(new Phrase(TotalQty.ToString(), FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                gvTable.AddCell(cell);


                cell = new PdfPCell(new Phrase(TotalPrice.ToString(), FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                gvTable.AddCell(cell);

                cell = new PdfPCell(new Phrase(GTotal.ToString(), FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                gvTable.AddCell(cell);


                document.Add(gvTable);
                document.NewPage();
                document.Close();
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment;filename=Dispatch.pdf");
                Response.Buffer = true;
                Response.Clear();
                Response.OutputStream.Write(ms.GetBuffer(), 0, ms.GetBuffer().Length);
                Response.OutputStream.Flush();
                Response.End();
            }

            #endregion

            #region Pdf for Inflow Detail as on 04/12/2013 add by Venkat

            if (GVListOfItemsInflow.Rows.Count > 0)
            {
                MemoryStream ms = new MemoryStream();
                Document document = new Document(PageSize.LEGAL);
                PdfWriter writer = PdfWriter.GetInstance(document, ms);
                document.Open();
                document.AddTitle("DIYOS");
                document.AddAuthor("DIYOS");
                document.AddSubject("Invoice");
                document.AddKeywords("Keyword1, keyword2, …");

                int columns = GVListOfItemsInflow.Columns.Count;
                int rows = GVListOfItemsInflow.Rows.Count;
                PdfPTable gvTable = new PdfPTable(columns);
                gvTable.TotalWidth = 500f;
                gvTable.LockedWidth = true;
                float[] widtlogo = { 1f, 1.5f, 6f, 3f, 1.6f, 1.8f, 1.8f }; //new float[columns]; 
                gvTable.SetWidths(widtlogo);

                uint FONT_SIZE = 10;

                float TotalQty = 0;
                float TotalPrice = 0;
                float GTotal = 0;

                PdfPCell c1 = new PdfPCell(new Phrase("Inflow Details Report", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 15, Font.BOLD, BaseColor.BLACK)));
                c1.Border = 0;
                c1.HorizontalAlignment = 1;
                c1.Colspan = columns;
                gvTable.AddCell(c1);
                PdfPCell cBlank = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 13, Font.BOLD, BaseColor.BLACK)));
                cBlank.Border = 0;
                cBlank.HorizontalAlignment = 1;
                cBlank.Colspan = columns;
                gvTable.AddCell(cBlank);

                PdfPCell cell;
                string cellText = "";

                for (int i = 0; i < columns; i++)
                {
                    widtlogo[i] = (int)GVListOfItemsInflow.Columns[i].ItemStyle.Width.Value;
                    //fetch the header text
                    cellText = Server.HtmlDecode(GVListOfItemsInflow.HeaderRow.Cells[i].Text);
                    cell = new PdfPCell(new Phrase(cellText, FontFactory.GetFont(FontFactory.TIMES_ROMAN, 11, Font.BOLD, BaseColor.BLACK)));
                    cell.HorizontalAlignment = 1;
                    gvTable.AddCell(cell);
                }

                for (int rowCounter = 0; rowCounter < rows; rowCounter++)
                {
                    if (GVListOfItemsInflow.Rows[rowCounter].RowType == DataControlRowType.DataRow)
                    {
                        cellText = ((Label)GVListOfItemsInflow.Rows[rowCounter].FindControl("lblsno1")).Text;
                        cell = new PdfPCell(new Phrase(cellText, FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                        gvTable.AddCell(cell);

                        cellText = GVListOfItemsInflow.Rows[rowCounter].Cells[1].Text;
                        cell = new PdfPCell(new Phrase(cellText, FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                        gvTable.AddCell(cell);

                        cellText = GVListOfItemsInflow.Rows[rowCounter].Cells[2].Text;
                        cell = new PdfPCell(new Phrase(cellText, FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                        gvTable.AddCell(cell);

                        cellText = ((Label)GVListOfItemsInflow.Rows[rowCounter].FindControl("lbldate")).Text;
                        cell = new PdfPCell(new Phrase(cellText, FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                        gvTable.AddCell(cell);

                        cellText = ((Label)GVListOfItemsInflow.Rows[rowCounter].FindControl("lblqty")).Text;
                        TotalQty += float.Parse(cellText.ToString());
                        cell = new PdfPCell(new Phrase(cellText, FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                        gvTable.AddCell(cell);

                        cellText = ((Label)GVListOfItemsInflow.Rows[rowCounter].FindControl("lblPrice")).Text;
                        TotalPrice += float.Parse(cellText.ToString());
                        cell = new PdfPCell(new Phrase(cellText, FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                        gvTable.AddCell(cell);

                        cellText = ((Label)GVListOfItemsInflow.Rows[rowCounter].FindControl("lblTotal")).Text;
                        GTotal += float.Parse(cellText.ToString());
                        cell = new PdfPCell(new Phrase(cellText, FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                        gvTable.AddCell(cell);
                    }

                }
                cellText = "";
                cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                cell.Border = 0;
                gvTable.AddCell(cell);

                cellText = "";
                cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                cell.Border = 0;
                gvTable.AddCell(cell);

                cellText = "";
                cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                cell.Border = 0;
                gvTable.AddCell(cell);

                cellText = "";
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

                cellText = "";
                cell = new PdfPCell(new Phrase(cellText, FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
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

                cellText = "Total";
                cell = new PdfPCell(new Phrase(cellText, FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                gvTable.AddCell(cell);


                cell = new PdfPCell(new Phrase(TotalQty.ToString(), FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                gvTable.AddCell(cell);


                cell = new PdfPCell(new Phrase(TotalPrice.ToString(), FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                gvTable.AddCell(cell);

                cell = new PdfPCell(new Phrase(GTotal.ToString(), FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                gvTable.AddCell(cell);


                document.Add(gvTable);
                document.NewPage();
                document.Close();
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment;filename=Inflow.pdf");
                Response.Buffer = true;
                Response.Clear();
                Response.OutputStream.Write(ms.GetBuffer(), 0, ms.GetBuffer().Length);
                Response.OutputStream.Flush();
                Response.End();
            }

            #endregion


            #region Pdf for Inflow Detail as on 04/12/2013 add by Venkat

            if (GVitemlist.Rows.Count > 0)
            {
                MemoryStream ms = new MemoryStream();
                Document document = new Document(PageSize.LEGAL);
                PdfWriter writer = PdfWriter.GetInstance(document, ms);
                document.Open();
                document.AddTitle("DIYOS");
                document.AddAuthor("DIYOS");
                document.AddSubject("Invoice");
                document.AddKeywords("Keyword1, keyword2, …");

                int columns = GVitemlist.Columns.Count;
                int rows = GVitemlist.Rows.Count;
                PdfPTable gvTable = new PdfPTable(columns);
                gvTable.TotalWidth = 500f;
                gvTable.LockedWidth = true;
                float[] widtlogo = { 1f, 1f, 6f, 1f, 1.5f, 2f }; //new float[columns]; 
                gvTable.SetWidths(widtlogo);

                uint FONT_SIZE = 10;

                float TotalInflow = 0;
                float TotalOutflow = 0;
                float GTotal = 0;

                PdfPCell c1 = new PdfPCell(new Phrase("Itemwise Report", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 15, Font.BOLD, BaseColor.BLACK)));
                c1.Border = 0;
                c1.HorizontalAlignment = 1;
                c1.Colspan = columns;
                gvTable.AddCell(c1);
                PdfPCell cBlank = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 13, Font.BOLD, BaseColor.BLACK)));
                cBlank.Border = 0;
                cBlank.HorizontalAlignment = 1;
                cBlank.Colspan = columns;
                gvTable.AddCell(cBlank);

                PdfPCell cell;
                string cellText = "";

                for (int i = 0; i < columns; i++)
                {
                    widtlogo[i] = (int)GVitemlist.Columns[i].ItemStyle.Width.Value;
                    //fetch the header text
                    cellText = Server.HtmlDecode(GVitemlist.HeaderRow.Cells[i].Text);
                    cell = new PdfPCell(new Phrase(cellText, FontFactory.GetFont(FontFactory.TIMES_ROMAN, 11, Font.BOLD, BaseColor.BLACK)));
                    cell.HorizontalAlignment = 1;
                    gvTable.AddCell(cell);
                }

                for (int rowCounter = 0; rowCounter < rows; rowCounter++)
                {
                    if (GVitemlist.Rows[rowCounter].RowType == DataControlRowType.DataRow)
                    {
                        cellText = ((Label)GVitemlist.Rows[rowCounter].FindControl("lblsno2")).Text;
                        cell = new PdfPCell(new Phrase(cellText, FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                        gvTable.AddCell(cell);

                        cellText = ((Label)GVitemlist.Rows[rowCounter].FindControl("lblItemId")).Text;
                        cell = new PdfPCell(new Phrase(cellText, FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                        gvTable.AddCell(cell);

                        cellText = ((Label)GVitemlist.Rows[rowCounter].FindControl("lblItemname")).Text;
                        cell = new PdfPCell(new Phrase(cellText, FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                        gvTable.AddCell(cell);

                        cellText = ((Label)GVitemlist.Rows[rowCounter].FindControl("lblInflow")).Text;
                        if (cellText.Length > 0)
                        {
                            TotalInflow += float.Parse(cellText.ToString());
                        }
                        cell = new PdfPCell(new Phrase(cellText, FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                        gvTable.AddCell(cell);

                        cellText = ((Label)GVitemlist.Rows[rowCounter].FindControl("lbloutflow")).Text;
                        if (cellText.Length > 0)
                        {
                            TotalOutflow += float.Parse(cellText.ToString());
                        }
                        cell = new PdfPCell(new Phrase(cellText, FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                        gvTable.AddCell(cell);

                        cellText = ((Label)GVitemlist.Rows[rowCounter].FindControl("lbldate")).Text;
                        cell = new PdfPCell(new Phrase(cellText, FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                        gvTable.AddCell(cell);
                    }

                }
                cellText = "";
                cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                cell.Border = 0;
                gvTable.AddCell(cell);

                cellText = "";
                cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                cell.Border = 0;
                gvTable.AddCell(cell);

                cellText = "";
                cell = new PdfPCell(new Phrase("", FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                cell.Border = 0;
                gvTable.AddCell(cell);

                cellText = "";
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

                cellText = "Total";
                cell = new PdfPCell(new Phrase(cellText, FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                //cell.Border = 0;
                gvTable.AddCell(cell);

                cell = new PdfPCell(new Phrase(TotalInflow.ToString(), FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                gvTable.AddCell(cell);


                cell = new PdfPCell(new Phrase(TotalOutflow.ToString(), FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                gvTable.AddCell(cell);

                cellText = "";
                cell = new PdfPCell(new Phrase(cellText, FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.BOLD, BaseColor.BLACK)));
                gvTable.AddCell(cell);



                document.Add(gvTable);
                document.NewPage();
                document.Close();
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment;filename=Itemwise.pdf");
                Response.Buffer = true;
                Response.Clear();
                Response.OutputStream.Write(ms.GetBuffer(), 0, ms.GetBuffer().Length);
                Response.OutputStream.Flush();
                Response.End();
            }

            #endregion


        }

        protected void ddlItemid_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddlItemid.SelectedIndex == 1)
            {
                ddlItemname.SelectedIndex = ddlItemid.SelectedIndex;
            }

            if (ddlItemid.SelectedIndex > 1)
            {
                ddlItemname.SelectedValue = ddlItemid.SelectedValue;
            }
            if (ddlItemid.SelectedIndex == 0)
            {
                ddlItemname.SelectedIndex = 0;
            }
        }

        protected void ddlItemname_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddlItemname.SelectedIndex == 1)
            {
                ddlItemid.SelectedIndex = ddlItemname.SelectedIndex;
            }

            if (ddlItemname.SelectedIndex > 1)
            {
                ddlItemid.SelectedValue = ddlItemname.SelectedValue;
            }
            if (ddlItemname.SelectedIndex == 0)
            {
                ddlItemid.SelectedIndex = 0;
            }
        }

        protected void GVListOfItemsInflow_RowDataBound(object sender, GridViewRowEventArgs e)
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

        protected void GVListOfItemsInflow_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GVListOfItemsInflow.PageIndex = e.NewPageIndex;
            Displaydata();
        }

        protected void GVListOfItems_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblqty = e.Row.FindControl("lblqty1") as Label;
                TotalQty += float.Parse(lblqty.Text);
                Label lblprice = e.Row.FindControl("lblPrice1") as Label;
                TotalPrice += float.Parse(lblprice.Text);
                Label lblTotal = e.Row.FindControl("lblTotal1") as Label;
                GrandTotal += float.Parse(lblTotal.Text);
            }

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblTotalQty = e.Row.FindControl("lblTotalQty1") as Label;
                lblTotalQty.Text = TotalQty.ToString();
                Label lblTotalprice = e.Row.FindControl("lblTotalPrice1") as Label;
                lblTotalprice.Text = TotalPrice.ToString();
                Label lblGTotal = e.Row.FindControl("lblGTotal1") as Label;
                lblGTotal.Text = GrandTotal.ToString();
            }
        }

        protected void GVListOfItems_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GVListOfItems.PageIndex = e.NewPageIndex;
            Displaydata();
        }

        protected void GVitemlist_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    Label lblInflow = e.Row.FindControl("lblInflow") as Label;
            //    if (lblInflow.Text.Trim().Length > 0)
            //    {
            //        TotalInflow += float.Parse(lblInflow.Text);
            //    }
            //    Label lblOutflow = e.Row.FindControl("lbloutflow") as Label;
            //    if (lblOutflow.Text.Trim().Length > 0)
            //    {
            //        TotalOutflow += float.Parse(lblOutflow.Text);
            //    }

            //}

            //if (e.Row.RowType == DataControlRowType.Footer)
            //{
            //    Label lblTotalinflow = e.Row.FindControl("lblTotalInflow") as Label;
            //    lblTotalinflow.Text = TotalInflow.ToString();
            //    Label lblTotalOutflow = e.Row.FindControl("lblTotaloutflow") as Label;
            //    lblTotalOutflow.Text = TotalOutflow.ToString();

            //}

        }

        protected void GVitemlist_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GVitemlist.PageIndex = e.NewPageIndex;
            Displaydata();
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