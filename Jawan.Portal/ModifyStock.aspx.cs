using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Globalization;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using KLTS.Data;
using Jawan.Portal.DAL;
namespace Jawan.Portal
{
    public partial class ModifyStock : System.Web.UI.Page
    {
        AppConfiguration config = new AppConfiguration();
        GridViewExportUtil gve = new GridViewExportUtil();
        DataTable dt;
        DropDownList bind_dropdownlist;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
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

                FillMRFIds();
                displaydata();

            }
        }

        private void displaydata()
        {
            string selectquery = "Select DISTINCT MRFId,MRF.ClientId,Clients.Clientname,Date from MRF, Clients " +
            "  where MRF.Clientid=Clients.Clientid and  Status=1 AND (DispatchStatus is null OR DispatchStatus=0) ";
            dt = null;// SqlHelper.Instance.GetTableByQuery(selectquery);


            dt = config.ExecuteAdaptorAsyncWithQueryParams(selectquery).Result;

            if (dt.Rows.Count > 0)
            {
                gvMRF.DataSource = dt;
                gvMRF.DataBind();
            }
            else
            {
                gvMRF.DataSource = null;
                gvMRF.DataBind();
            }
        }

        protected void FillMRFIds()
        {
            string selectclientid = "select DISTINCT MRFId from MRF where Status=1 AND DispatchStatus<>1 Order By mrfid";
            dt = null;


            dt = config.ExecuteAdaptorAsyncWithQueryParams(selectclientid).Result;

            ddlMRF.Items.Clear();
            ddlMRF.Items.Add("--Select--");
            for (int rowno1 = 0; rowno1 < dt.Rows.Count; rowno1++)
            {
                ddlMRF.Items.Add(dt.Rows[rowno1][0].ToString());
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
                    EmployeesLink.Visible = true;
                    ClientsLink.Visible = true;
                    CompanyInfoLink.Visible = true;
                    InventoryLink.Visible = true;
                    ReportsLink.Visible = true;
                    SettingsLink.Visible = true;
                    ViewItemslink.Visible = false;
                    ViewStockLink.Visible = true;
                    AddStockLink.Visible = false;
                    ModifyStockLink.Visible = true;
                    DeleteStockLink.Visible = false;
                    break;
                case 5:

                    break;
                case 6:
                    EmployeesLink.Visible = false;
                    ClientsLink.Visible = true;
                    CompanyInfoLink.Visible = false;
                    InventoryLink.Visible = true;
                    ReportsLink.Visible = true;
                    SettingsLink.Visible = false;
                    break;
                default:
                    break;
            }
        }

        protected void ddlMRF_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtclientid.Text = "";
            lblclientname.Text = "";
            lblresult.Text = "";
            if (ddlMRF.SelectedIndex > 0)
            {
                Session["checkdownloadstatus"] = "0";

                string sqlQry = "Select MRF.sno, MRF.MRFId,MRF.ClientId,Clients.Clientname,MRF.ItemId,MRF.Quantity," +
                    "  MRF.ApprovedQty from MRF,Clients where  MRF.MRFId='" + ddlMRF.SelectedValue +
                    "'  and MRF.ClientId=Clients.ClientId  Order by Cast(Itemid as int)";
                DataTable dTable = null;

                dTable = config.ExecuteAdaptorAsyncWithQueryParams(sqlQry).Result;

                if (dTable.Rows.Count > 0)
                {
                    gvMRFDetails.DataSource = dTable;
                    gvMRFDetails.DataBind();

                    foreach (GridViewRow gvrow in gvMRFDetails.Rows)
                    {
                        string itemid = ((Label)gvrow.FindControl("lblitemid")).Text;
                        Label lblitemname = (Label)gvrow.FindControl("lblitemname");
                        lblitemname.Text = Getitemname(itemid);
                        Label lblActQty = (Label)gvrow.FindControl("lblActQty");
                        lblActQty.Text = Getclosingstock(itemid);
                    }
                }
                else
                {
                    gvMRFDetails.DataSource = null;
                    gvMRFDetails.DataBind();
                }

                if (dTable.Rows.Count > 0)
                {
                    txtclientid.Text = dTable.Rows[0]["ClientId"].ToString();
                    lblclientname.Text = dTable.Rows[0]["Clientname"].ToString();
                }
            }
            else
            {
                displaydata();
                gvMRFDetails.DataSource = null;
                gvMRFDetails.DataBind();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (Session["checkdownloadstatus"].ToString() == "1")
            {
                gvMRF.DataSource = null;
                gvMRF.DataBind();
                //gvMRFDetails.DataSource = null;
                //gvMRFDetails.DataBind();
                ddlMRF.SelectedIndex = 0;
                ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Please Select the Other MrfId');", true);
                return;
            }

            if (ddlMRF.SelectedIndex > 0)
            {
                for (int i = 0; i < gvMRFDetails.Rows.Count; i++)
                {
                    Label reqQty = gvMRFDetails.Rows[i].FindControl("lblQty") as Label;
                    if (reqQty != null)
                    {
                        Label ActQty = gvMRFDetails.Rows[i].FindControl("lblActQty") as Label;
                        if (ActQty != null)
                        {
                            int rQty = 0;
                            if (reqQty.Text.Length > 0)
                            {
                                rQty = Convert.ToInt32(reqQty.Text);
                            }
                            int aQty = 0;
                            if (ActQty.Text.Length > 0)
                            {
                                aQty = Convert.ToInt32(ActQty.Text);
                            }
                            if (rQty > aQty)
                            {
                                lblresult.Text = "Actual Quantity shoud be greater than Required Quantity for dispatch";
                                return;
                            }
                        }
                    }
                }

                //string sqlQry = "Select MRF.MRFId,MRF.ClientId,MRF.ItemId,MRF.ApprovedQty,MRF.ClosingStock,StockItemList.ItemName, "+
                //    " StockItemList.ActualQuantity from StockItemList INNER JOIN MRF ON MRF.ItemID = StockItemList.ItemID AND MRF.MRFId = '"
                //    + ddlMRF.SelectedValue + "'";
                string sqlQry = " Select MRF.MRFId,MRF.ClientId,MRF.ItemId,MRF.ApprovedQty from MRF  where MRF.MRFId ='" + ddlMRF.SelectedValue + "'";
                DataTable dTable = null;// SqlHelper.Instance.GetTableByQuery(sqlQry);


                dTable = config.ExecuteAdaptorAsyncWithQueryParams(sqlQry).Result;

                for (int i = 0; i < dTable.Rows.Count; i++)
                {
                    string itemId = dTable.Rows[i]["ItemId"].ToString();
                    string sqlqry = "select  Actualquantity from Stockitemlist Where itemid='" + itemId + "'";
                    DataTable dtstock = config.ExecuteAdaptorAsyncWithQueryParams(sqlqry).Result;

                    int reqQty = 0;
                    int actQty = 0;


                    if (dTable.Rows[i]["ApprovedQty"].ToString().Length > 0)
                    {
                        reqQty = Convert.ToInt32(dTable.Rows[i]["ApprovedQty"].ToString());
                    }
                    if (dtstock.Rows[0]["ActualQuantity"].ToString().Length > 0)
                    {
                        actQty = Convert.ToInt32(dtstock.Rows[0]["ActualQuantity"].ToString());
                    }

                    if (actQty >= reqQty)
                        actQty -= reqQty;
                    sqlQry = "Update Stockitemlist set ActualQuantity = " + actQty + " where Itemid = '" + itemId + "'";
                    int status1 = config.ExecuteNonQueryWithQueryAsync(sqlQry).Result;

                    //Dipatch  Status
                    string sqlQryDispatch = "Update MRF Set DispatchStatus=1 Where itemid=" + itemId + "  and MRFID='" + ddlMRF.SelectedValue + "'";


                    DataTable upds2 = config.ExecuteAdaptorAsyncWithQueryParams(sqlQryDispatch).Result;

                }


                int status = 5;
                if (status > 0)
                {

                    lblresult.Text = "MRF Dispatched";

                    //Print data about dispatced items

                    Session["checkdownloadstatus"] = 1;

                    #region   PDf Download code

                    MemoryStream ms = new MemoryStream();


                    Document document = new Document(PageSize.LEGAL);
                    PdfWriter writer = PdfWriter.GetInstance(document, ms);
                    document.Open();
                    document.AddTitle("FaMS");
                    document.AddAuthor("WebWonders");
                    document.AddSubject("Dispatched Items");
                    document.AddKeywords("Keyword1, keyword2, …");//
                    string strQrycompanyinfo = "Select * from CompanyInfo";
                    // DataTable compInfo = SqlHelper.Instance.GetTableByQuery(strQrycompanyinfo);
                    DataTable compInfo = null;
                    compInfo = config.ExecuteAdaptorAsyncWithQueryParams(strQrycompanyinfo).Result;
                    string companyName = "Your Company Name";
                    string companyAddress = "Your Company Address";
                    if (compInfo.Rows.Count > 0)
                    {
                        companyName = compInfo.Rows[0]["CompanyName"].ToString();
                        companyAddress = compInfo.Rows[0]["Address"].ToString();
                    }

                    document.AddTitle(companyName);
                    document.AddAuthor("DIYOS");
                    document.AddSubject("Invoice");
                    document.AddKeywords("Keyword1, keyword2, …");
                    string imagepath = Server.MapPath("~/assets/BillLogo.png");
                    if (File.Exists(imagepath))
                    {
                        iTextSharp.text.Image gif2 = iTextSharp.text.Image.GetInstance(imagepath);

                        gif2.Alignment = (iTextSharp.text.Image.ALIGN_LEFT | iTextSharp.text.Image.UNDERLYING);
                        gif2.ScalePercent(70f);
                        document.Add(new Paragraph(" "));
                        // document.Add(gif2);
                    }

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
                    PdfPCell CCompName = new PdfPCell(new Paragraph(companyName, FontFactory.GetFont(FontFactory.TIMES_ROMAN, 15, Font.BOLD, BaseColor.BLACK)));
                    CCompName.HorizontalAlignment = 1;
                    CCompName.Border = 0;
                    CCompName.Colspan = 2;
                    tablelogo.AddCell(CCompName);
                    PdfPCell CCompAddress = new PdfPCell(new Paragraph(companyAddress, FontFactory.GetFont(FontFactory.TIMES_ROMAN, 8, Font.BOLD, BaseColor.BLACK)));
                    CCompAddress.HorizontalAlignment = 1;
                    CCompAddress.Border = 0;
                    CCompAddress.Colspan = 2;
                    tablelogo.AddCell(CCompAddress);
                    document.Add(tablelogo);
                    //Client address


                    PdfPTable address = new PdfPTable(2);
                    address.TotalWidth = 500f;
                    address.LockedWidth = true;
                    float[] addreslogo = new float[] { 2f, 2f };
                    address.SetWidths(addreslogo);

                    PdfPTable tempTable1 = new PdfPTable(1);
                    tempTable1.TotalWidth = 250f;
                    tempTable1.LockedWidth = true;
                    float[] tempWidth1 = new float[] { 1f };
                    tempTable1.SetWidths(tempWidth1);

                    string selectclientaddress = "select * from clients where clientid= '" + txtclientid.Text.Trim() + "'";
                    DataTable dtclientaddress = null;
                    dtclientaddress = config.ExecuteAdaptorAsyncWithQueryParams(selectclientaddress).Result;


                    PdfPCell cell11 = new PdfPCell(new Paragraph("Customer's Name,", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, Font.BOLD, BaseColor.BLACK)));
                    cell11.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    cell11.Border = 0;
                    tempTable1.AddCell(cell11);

                    string addressData = "";
                    if (dtclientaddress.Rows.Count > 0)
                        addressData = dtclientaddress.Rows[0]["ClientAddrHno"].ToString();
                    if (addressData.Trim().Length > 0)
                    {

                        PdfPCell clientaddrhno = new PdfPCell(new Paragraph(addressData, FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, Font.BOLD, BaseColor.BLACK)));
                        clientaddrhno.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                        //clientaddrhno.Colspan = 0;
                        clientaddrhno.Border = 0;
                        tempTable1.AddCell(clientaddrhno);
                    }
                    if (dtclientaddress.Rows.Count > 0)
                        addressData = dtclientaddress.Rows[0]["ClientAddrStreet"].ToString();
                    if (addressData.Trim().Length > 0)
                    {
                        PdfPCell clientstreet = new PdfPCell(new Paragraph(addressData, FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, Font.BOLD, BaseColor.BLACK)));
                        clientstreet.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                        clientstreet.Border = 0;
                        tempTable1.AddCell(clientstreet);
                    }
                    if (dtclientaddress.Rows.Count > 0)
                        addressData = dtclientaddress.Rows[0]["ClientAddrColony"].ToString();
                    if (addressData.Trim().Length > 0)
                    {
                        PdfPCell clientcolony = new PdfPCell(new Paragraph(addressData, FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, Font.BOLD, BaseColor.BLACK)));
                        clientcolony.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                        clientcolony.Colspan = 2;
                        clientcolony.Border = 0;
                        tempTable1.AddCell(clientcolony);
                    }
                    if (dtclientaddress.Rows.Count > 0)
                        addressData = dtclientaddress.Rows[0]["ClientAddrcity"].ToString();
                    if (addressData.Trim().Length > 0)
                    {
                        PdfPCell clientcity = new PdfPCell(new Paragraph(addressData, FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, Font.BOLD, BaseColor.BLACK)));
                        clientcity.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                        clientcity.Colspan = 2;
                        clientcity.Border = 0;
                        tempTable1.AddCell(clientcity);
                    }
                    if (dtclientaddress.Rows.Count > 0)
                        addressData = dtclientaddress.Rows[0]["ClientAddrstate"].ToString();
                    if (addressData.Trim().Length > 0)
                    {
                        PdfPCell clientstate = new PdfPCell(new Paragraph(addressData, FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, Font.BOLD, BaseColor.BLACK)));
                        clientstate.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                        clientstate.Colspan = 2;
                        clientstate.Border = 0;
                        tempTable1.AddCell(clientstate);
                    }
                    if (dtclientaddress.Rows.Count > 0)
                        addressData = dtclientaddress.Rows[0]["ClientAddrpin"].ToString();
                    if (addressData.Trim().Length > 0)
                    {
                        PdfPCell clietnpin = new PdfPCell(new Paragraph(addressData, FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, Font.BOLD, BaseColor.BLACK)));
                        clietnpin.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                        clietnpin.Colspan = 2;
                        clietnpin.Border = 0;
                        tempTable1.AddCell(clietnpin);
                    }
                    PdfPCell childTable1 = new PdfPCell(tempTable1);
                    childTable1.Border = 0;
                    childTable1.HorizontalAlignment = 0;
                    address.AddCell(childTable1);

                    PdfPTable tempTable2 = new PdfPTable(1);
                    tempTable2.TotalWidth = 250f;
                    tempTable2.LockedWidth = true;
                    float[] tempWidth2 = new float[] { 1f };
                    tempTable2.SetWidths(tempWidth2);


                    PdfPCell cell13 = new PdfPCell(new Paragraph("Date: " + DateTime.Now.Date.ToShortDateString(), FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, Font.NORMAL, BaseColor.BLACK)));
                    cell13.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                    cell13.Border = 0;
                    tempTable2.AddCell(cell13);

                    PdfPCell childTable2 = new PdfPCell(tempTable2);
                    childTable2.Border = 0;
                    childTable2.HorizontalAlignment = 0;
                    address.AddCell(childTable2);
                    address.AddCell(celll);
                    document.Add(address);
                    int colCount = 6;
                    #region Table Headings

                    uint FONT_SIZE = 8;
                    int tableCells = 6;

                    PdfPTable Secondtable = new PdfPTable(tableCells);
                    Secondtable.TotalWidth = 500f;
                    Secondtable.LockedWidth = true;
                    float[] SecondWidth = new float[] { 0.3f, 2.2f, 1f, 1f, 1f, 1f };
                    Secondtable.SetWidths(SecondWidth);

                    //Cell Headings
                    //1
                    PdfPCell sNo = new PdfPCell(new Phrase("S.No.", FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                    sNo.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    //sNo.Colspan = 1;
                    sNo.Border = 15;// 15;
                    Secondtable.AddCell(sNo);
                    //2
                    PdfPCell Item = new PdfPCell(new Phrase("Item", FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                    Item.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    Item.Border = 15;// 15;
                    Secondtable.AddCell(Item);
                    //3
                    PdfPCell Unit = new PdfPCell(new Phrase("Unit", FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                    Unit.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    Unit.Border = 15;// 15;
                    Secondtable.AddCell(Unit);
                    //4
                    PdfPCell Qty = new PdfPCell(new Phrase("Qty", FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                    Qty.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    Qty.Border = 15;
                    Secondtable.AddCell(Qty);
                    //5
                    PdfPCell Rate = new PdfPCell(new Phrase("Rate", FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                    Rate.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    Rate.Border = 15;
                    Secondtable.AddCell(Rate);
                    //6
                    PdfPCell Value = new PdfPCell(new Phrase("Value", FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                    Value.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    Value.Border = 15;
                    Secondtable.AddCell(Value);

                    #endregion


                    #region Table Data
                    //int rowCount = 0;
                    //int pageCount = 0;


                    //if (ddlMRF.SelectedIndex == 0)
                    //{
                    //    return;
                    //}

                    //string sqlQryformrf = "Select MRF.ApprovedQty,StockItemList.ItemName,StockItemList.UnitMeasure,"+
                    //    "(StockItemList.Sellingprice/StockItemList.ActualQuantity) as Rate," +
                    //    "(MRF.ApprovedQty*(StockItemList.Sellingprice/StockItemList.ActualQuantity)) " +
                    //    " as value from StockItemList INNER JOIN MRF ON MRF.ItemID = StockItemList.ItemID"+
                    //    " AND MRF.MRFId = '"+ ddlMRF.SelectedValue + "'";


                    string sqlQryformrf = "Select MRF.ItemID , MRF.ApprovedQty,MRF.SellingPrice   from MRF  Where MRF.MRFId = '" + ddlMRF.SelectedValue + "'";
                    DataTable dTablemrf = null;// SqlHelper.Instance.GetTableByQuery(sqlQryformrf);


                    dTablemrf = config.ExecuteAdaptorAsyncWithQueryParams(sqlQryformrf).Result;

                    if (dTablemrf.Rows.Count == 0)
                    {
                        return;
                    }

                    float totalprice = 0;
                    for (int i = 0; i < dTablemrf.Rows.Count; i++)
                    {
                        string MRFItemid = dTablemrf.Rows[i]["ItemID"].ToString();
                        int actualquantity = int.Parse(dTablemrf.Rows[i]["ApprovedQty"].ToString());
                        if (actualquantity > 0)
                        {
                            PdfPCell CSNo = new PdfPCell(new Phrase((i + 1).ToString(), FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                            CSNo.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            CSNo.Border = 15;
                            Secondtable.AddCell(CSNo);
                            #region  Get Details from Stock itemlist

                            string sqlqrymrfdetails = "Select * from Stockitemlist where itemid='" + MRFItemid + "'";
                            DataTable dtmrfdetails = config.ExecuteAdaptorAsyncWithQueryParams(sqlqrymrfdetails).Result;


                            #endregion
                            PdfPCell ItemName = new PdfPCell(new Phrase(dtmrfdetails.Rows[0]["ItemName"].ToString(), FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                            ItemName.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            ItemName.Border = 15;
                            Secondtable.AddCell(ItemName);

                            PdfPCell UnitMeasure = new PdfPCell(new Phrase("1" + dtmrfdetails.Rows[0]["UnitMeasure"].ToString(), FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                            UnitMeasure.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            UnitMeasure.Border = 15;
                            Secondtable.AddCell(UnitMeasure);

                            PdfPCell ApprovedQty = new PdfPCell(new Phrase(dTablemrf.Rows[i]["ApprovedQty"].ToString(), FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                            ApprovedQty.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            ApprovedQty.Border = 15;
                            Secondtable.AddCell(ApprovedQty);

                            float forcovert = float.Parse(dTablemrf.Rows[i]["SellingPrice"].ToString());

                            PdfPCell Rateperitem = new PdfPCell(new Phrase(forcovert.ToString("0.00"), FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                            Rateperitem.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            Rateperitem.Border = 15;
                            Secondtable.AddCell(Rateperitem);

                            forcovert = forcovert * actualquantity;  // float.Parse(dTablemrf.Rows[i]["Value"].ToString());

                            PdfPCell Valuefortotalitems = new PdfPCell(new Phrase(forcovert.ToString("0.00"), FontFactory.GetFont(FontFactory.TIMES_ROMAN, FONT_SIZE, Font.NORMAL, BaseColor.BLACK)));
                            Valuefortotalitems.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                            Valuefortotalitems.Border = 15;
                            Valuefortotalitems.MinimumHeight = 25;
                            Secondtable.AddCell(Valuefortotalitems);
                            totalprice = totalprice + forcovert;
                        }

                    }
                    #endregion



                    document.Add(Secondtable);

                    //PdfPTable tabled = new PdfPTable(colCount);
                    //tabled.TotalWidth = 500;//432f;
                    //tabled.LockedWidth = true;
                    //float[] widthd = new float[] { 1f, 1f, 1f, 1f, 1f,1f };
                    //tabled.SetWidths(widthd);

                    //PdfPCell celldg6 = new PdfPCell(new Phrase("Grand Total(Rs:)", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, Font.BOLD, BaseColor.BLACK)));
                    //celldg6.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                    //tabled.AddCell(celldg6);

                    //PdfPCell celldg8 = new PdfPCell(new Phrase("Hai", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, Font.BOLD, BaseColor.BLACK)));
                    //celldg8.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                    //tabled.AddCell(celldg8);
                    //document.Add(tabled);

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
                    string rupeesinwords = NumberToEnglish.Instance.changeNumericToWords(totalprice.ToString("0.00"));


                    PdfPCell cellTotal1 = new PdfPCell(new Phrase("Total:", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, Font.BOLD, BaseColor.BLACK)));
                    cellTotal1.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                    // cellTotal.Colspan = 2;
                    cellTotal1.Border = 0;
                    cellTotal1.PaddingRight = -130;
                    tablecon.AddCell(cellTotal1);

                    PdfPCell cellTota2 = new PdfPCell(new Phrase(totalprice.ToString("0.00"), FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, Font.BOLD, BaseColor.BLACK)));
                    cellTota2.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    // cellTotal.Colspan = 2;
                    cellTota2.Border = 0;
                    cellTota2.PaddingRight = -130;
                    tablecon.AddCell(cellTota2);

                    rupeesinwords = rupeesinwords.Replace("point", " and  ");
                    PdfPCell cellcamt = new PdfPCell(new Phrase(" (In Words Rupees " + rupeesinwords + "Only)", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, Font.BOLD, BaseColor.BLACK)));
                    cellcamt.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    cellcamt.Colspan = 2;
                    cellcamt.Border = 0;
                    tablecon.AddCell(cellcamt);

                    tablecon.AddCell(cellBreak);
                    tablecon.AddCell(cellBreak);
                    tablecon.AddCell(cellBreak);
                    tablecon.AddCell(cellBreak);

                    PdfPCell cellc3 = new PdfPCell(new Phrase("For " + companyName/*dtclientaddress.Rows[0]["clientname"].ToString()*/, FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, Font.BOLD, BaseColor.BLACK)));
                    cellc3.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                    cellc3.Colspan = 7;
                    tablecon.AddCell(cellc3);
                    tablecon.AddCell(cellBreak);
                    tablecon.AddCell(cellBreak);
                    tablecon.AddCell(cellBreak);

                    PdfPCell cellc4 = new PdfPCell(new Phrase("Authorised Signatory", FontFactory.GetFont(FontFactory.TIMES_ROMAN, 12, Font.BOLD, BaseColor.BLACK)));
                    cellc4.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                    cellc4.Colspan = 7;
                    cellc4.Border = 0;
                    tablecon.AddCell(cellc4);
                    tablecon.AddCell(cellBreak);

                    document.Add(tablecon);

                    //displaydata();
                    //FillMRFIds();

                    //ddlsitename.SelectedIndex = 0;

                    //Completed pdf document


                    document.NewPage();
                    document.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=Dispatchditems.pdf");
                    Response.Buffer = true;
                    Response.Clear();
                    Response.OutputStream.Write(ms.GetBuffer(), 0, ms.GetBuffer().Length);
                    Response.OutputStream.Flush();

                    Response.End();

                    #endregion


                }

                else
                {

                }

            }
            else
            {
                lblresult.Text = "Select MRF to Dispatch";
            }



        }

        protected void btncancel_Click(object sender, EventArgs e)
        {
            FillMRFIds();
            displaydata();
        }



        protected string Getclosingstock(string itemid)
        {

            string sqlqry = "Select ActualQuantity from stockitemlist Where itemid='" + itemid + "'";
            DataTable dt = config.ExecuteAdaptorAsyncWithQueryParams(sqlqry).Result;

            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0]["ActualQuantity"].ToString();
            }
            else
            {
                return "";
            }

        }

        protected string Getitemname(string itemid)
        {

            string sqlqry = "Select itemname from stockitemlist Where itemid='" + itemid + "'";
            DataTable dt = config.ExecuteAdaptorAsyncWithQueryParams(sqlqry).Result;

            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0]["itemname"].ToString();
            }
            else
            {
                return "";
            }

        }

    }
}