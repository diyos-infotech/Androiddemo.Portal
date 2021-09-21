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
using KLTS.Data;
using Jawan.Portal.DAL;
namespace Jawan.Portal
{
    public partial class AddStock : System.Web.UI.Page
    {
        AppConfiguration config = new AppConfiguration();
        GridViewExportUtil gve = new GridViewExportUtil();
        DataTable dt;
        DropDownList bind_dropdownlist;
        string selecteditem;

        //Display The Item Name When We Select Item Id
        protected void DDL_GetItemName(object sender, EventArgs e)
        {
            LblResult.Text = "";
            DropDownList ddl = sender as DropDownList;

            foreach (GridViewRow row in gvaddstock.Rows)
            {
                Control ctrl = row.FindControl("ddlItemID") as DropDownList;
                Control ctrlitemname = row.FindControl("ddlitemname") as DropDownList;
                if (ctrl != null)
                {
                    DropDownList ddl1 = (DropDownList)ctrl;
                    if (ddl.ClientID == ddl1.ClientID)
                    {
                        DropDownList itemname = (DropDownList)ctrlitemname;
                        Label qty = (Label)row.FindControl("lblitemQuantity");
                        Label lblitemunits = (Label)row.FindControl("lblitemunits");
                        TextBox txtsellingprice = (TextBox)row.FindControl("txtsellingprice");
                        TextBox txtbuyingprice = (TextBox)row.FindControl("txtbuyingprice");

                        if (ddl1.SelectedIndex > 0)
                        {
                            selecteditem = ddl1.SelectedValue;
                            //string selectquery = " select itemname,Price,ActualQuantity from Stockitemlist where itemid = " + selecteditem;
                            string selectquery = " select itemname,buyingPrice,Sellingprice,ActualQuantity,UnitMeasure from Stockitemlist where itemid = " + selecteditem;
                            DataTable dtitemanme = config.ExecuteAdaptorAsyncWithQueryParams(selectquery).Result;
                            if (dtitemanme.Rows.Count > 0)
                            {
                                if (itemname != null)
                                    itemname.SelectedValue = dtitemanme.Rows[0][0].ToString();
                                if (qty != null)
                                    qty.Text = dtitemanme.Rows[0]["ActualQuantity"].ToString();
                                if (txtsellingprice != null)
                                    txtsellingprice.Text = dtitemanme.Rows[0]["Sellingprice"].ToString();

                                if (txtbuyingprice != null)
                                    txtbuyingprice.Text = dtitemanme.Rows[0]["buyingPrice"].ToString();
                                if (lblitemunits != null)
                                    lblitemunits.Text = dtitemanme.Rows[0]["UnitMeasure"].ToString();
                            }
                        }
                        break;
                    }
                }
            }
        }

        protected void DDL_GetItemId(object sender, EventArgs e)
        {
            LblResult.Text = "";
            DropDownList ddl = sender as DropDownList;
            foreach (GridViewRow row in gvaddstock.Rows)
            {
                Control ctrl = row.FindControl("ddlitemname") as DropDownList;
                Control ctrlitemname = row.FindControl("ddlitemid") as DropDownList;
                if (ctrl != null)
                {
                    DropDownList ddl1 = (DropDownList)ctrl;
                    if (ddl.ClientID == ddl1.ClientID)
                    {
                        DropDownList itemname = (DropDownList)ctrlitemname;
                        Label qty = (Label)row.FindControl("lblitemQuantity");
                        Label lblitemunits = (Label)row.FindControl("lblitemunits");
                        TextBox txtsellingprice = (TextBox)row.FindControl("txtsellingprice");
                        TextBox txtbuyingprice = (TextBox)row.FindControl("txtbuyingprice");
                        if (ddl1.SelectedIndex > 0)
                        {
                            selecteditem = ddl1.SelectedValue;
                            // string selectquery = " select itemid,ActualQuantity,Price from Stockitemlist where itemname ='" + selecteditem + "'";
                            string selectquery = " select itemid,ActualQuantity,buyingPrice,Sellingprice,UnitMeasure from Stockitemlist where itemname ='" +
                                selecteditem + "'";
                            DataTable dtitemanme = config.ExecuteAdaptorAsyncWithQueryParams(selectquery).Result;

                            if (dtitemanme.Rows.Count > 0)
                            {
                                itemname.SelectedValue = dtitemanme.Rows[0][0].ToString();
                                if (qty != null)
                                    qty.Text = dtitemanme.Rows[0]["ActualQuantity"].ToString();
                                if (txtsellingprice != null)
                                    txtsellingprice.Text = dtitemanme.Rows[0]["Sellingprice"].ToString();
                                if (txtbuyingprice != null)
                                    txtbuyingprice.Text = dtitemanme.Rows[0]["buyingPrice"].ToString();
                                if (lblitemunits != null)
                                    lblitemunits.Text = dtitemanme.Rows[0]["UnitMeasure"].ToString();


                            }
                            break;
                        }
                        else
                        {
                            LblResult.Text = "Please Select Item Name";
                            return;
                        }
                    }
                }
            }
        }

        //Display The Data In Gridview
        private void displaydata()
        {
            string selectquery = " select ItemId from Stockitemlist Order By (cast(Itemid as int))";
            dt = config.ExecuteAdaptorAsyncWithQueryParams(selectquery).Result;
            gvaddstock.DataSource = dt;
            gvaddstock.DataBind();
            foreach (GridViewRow grdRow in gvaddstock.Rows)
            {
                bind_dropdownlist = (DropDownList)(gvaddstock.Rows[grdRow.RowIndex].Cells[0].FindControl("ddlitemid"));
                bind_dropdownlist.DataSource = dt;
                bind_dropdownlist.DataValueField = "ItemId";
                bind_dropdownlist.DataTextField = "ItemId";
                bind_dropdownlist.DataBind();
                bind_dropdownlist.Items.Insert(0, "--Select--");
                bind_dropdownlist = (DropDownList)(gvaddstock.Rows[grdRow.RowIndex].Cells[0].FindControl("ddlitemname"));

                string Sqlmsureunits = "Select itemname from  Stockitemlist   order by itemname";
                DataTable Dtmesurements = config.ExecuteAdaptorAsyncWithQueryParams(Sqlmsureunits).Result;
                bind_dropdownlist.DataSource = Dtmesurements;
                bind_dropdownlist.DataValueField = "itemname";
                bind_dropdownlist.DataTextField = "itemname";
                bind_dropdownlist.DataBind();
                bind_dropdownlist.Items.Insert(0, "--Select--");
                break;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["ContractsAIndex"] = 1;
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
                displaydata();
                DisplayDefaultRow(sender, e);
            }
        }

        protected void DisplayDefaultRow(object sender, EventArgs e)
        {
            for (int i = 0; i < gvaddstock.Rows.Count; i++)
            {
                if (i < 10)
                {
                    Session["ContractsAIndex"] = Convert.ToInt16(Session["ContractsAIndex"]) + 1;
                    gvaddstock.Rows[i].Visible = true;
                    string selectquery = "Select itemid from StockItemList Order By (cast(Itemid as int)) ";
                    DataTable DtDesignation = config.ExecuteAdaptorAsyncWithQueryParams(selectquery).Result;

                    DropDownList ddldrow = gvaddstock.Rows[i].FindControl("ddlitemid") as DropDownList;

                    ddldrow.DataSource = DtDesignation;
                    ddldrow.DataValueField = "Itemid";
                    ddldrow.DataTextField = "Itemid";
                    ddldrow.DataBind();
                    ddldrow.Items.Insert(0, "--Select--");

                    //bind itemnames
                    // string selectqueryitemanme = "Select itemname from StockItemList Order By (cast(Itemid as int)) ";
                    string selectqueryitemanme = "Select itemname from StockItemList Order By itemname ";
                    DataTable Dtitemaname = config.ExecuteAdaptorAsyncWithQueryParams(selectqueryitemanme).Result;

                    DropDownList ddldrowitemname = gvaddstock.Rows[i].FindControl("ddlitemname") as DropDownList;

                    ddldrowitemname.DataSource = Dtitemaname;
                    ddldrowitemname.DataValueField = "Itemname";
                    ddldrowitemname.DataTextField = "Itemname";
                    ddldrowitemname.DataBind();
                    ddldrowitemname.Items.Insert(0, "--Select--");

                    Label lblsno = gvaddstock.Rows[i].FindControl("lblsno") as Label;
                    lblsno.Text = (i + 1).ToString();

                }
                else
                    gvaddstock.Rows[i].Visible = false;
            }
            Session["ContractsAIndex"] = 10;
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

        protected void Button1_Click(object sender, EventArgs e)
        {
        }

        protected void gvaddstock_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        protected void gvaddstock_RowEditing(object sender, GridViewEditEventArgs e)
        {
        }

        protected void gvaddstock_RowCommand(object sender, GridViewCommandEventArgs e)
        {
        }

        protected void gvaddstock_RowDataBound(object sender, GridViewRowEventArgs e)
        {
        }

        protected void BtnAddItem_Click(object sender, EventArgs e)
        {
            int designCount = Convert.ToInt16(Session["ContractsAIndex"]);
            if (designCount < gvaddstock.Rows.Count)
            {
                gvaddstock.Rows[designCount].Visible = true;
                //bind Item Id

                string selectquery = "Select itemid from StockItemList Order By (cast(Itemid as int)) ";
                DataTable DtDesignation = config.ExecuteAdaptorAsyncWithQueryParams(selectquery).Result;

                DropDownList ddldrow = gvaddstock.Rows[designCount].FindControl("ddlitemid") as DropDownList;

                ddldrow.DataSource = DtDesignation;
                ddldrow.DataValueField = "Itemid";
                ddldrow.DataTextField = "Itemid";
                ddldrow.DataBind();
                ddldrow.Items.Insert(0, "--Select--");

                //bind itemnames
                // string selectqueryitemanme = "Select itemname from StockItemList Order By (cast(Itemid as int)) ";
                string selectqueryitemanme = "Select itemname from StockItemList Order By itemname ";
                DataTable Dtitemaname = config.ExecuteAdaptorAsyncWithQueryParams(selectqueryitemanme).Result;

                DropDownList ddldrowitemname = gvaddstock.Rows[designCount].FindControl("ddlitemname") as DropDownList;

                ddldrowitemname.DataSource = Dtitemaname;
                ddldrowitemname.DataValueField = "Itemname";
                ddldrowitemname.DataTextField = "Itemname";
                ddldrowitemname.DataBind();
                ddldrowitemname.Items.Insert(0, "--Select--");
                Label lblsno = gvaddstock.Rows[designCount].FindControl("lblsno") as Label;
                designCount = designCount + 1;
                lblsno.Text = (designCount).ToString();

                Session["ContractsAIndex"] = designCount;
            }
            else
            {
                LblResult.Text = "There are no more items";
            }
        }

        protected void Button1_Click1(object sender, EventArgs e)
        {
            int CheckRowIndex = 0;
            int Status = 0;

            try
            {
                foreach (GridViewRow dr in gvaddstock.Rows)
                {
                    string ItemId = ((DropDownList)dr.FindControl("ddlitemid")).Text;
                    DropDownList ddlitemid = dr.FindControl("ddlitemid") as DropDownList;
                    DropDownList ddlitemname = dr.FindControl("ddlitemname") as DropDownList;
                    TextBox txtprice = dr.FindControl("txtbuyingprice") as TextBox;
                    TextBox txtSellingprice = dr.FindControl("txtsellingprice") as TextBox;
                    TextBox txtquantity = dr.FindControl("txtquantity") as TextBox;

                    if (ddlitemid.SelectedIndex <= 0)
                    {
                        //return;
                        break;
                    }

                    string Price = string.Empty;
                    string sellingPrice = string.Empty;
                    string Quantity = string.Empty;

                    Price = ((TextBox)dr.FindControl("txtbuyingprice")).Text;
                    if (Price.Trim().Length == 0)
                    {
                        Price = "0";
                    }

                    if (Price == "0")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showlalert", "alert('Please Enter Price');", true);
                        return;
                    }

                    sellingPrice = ((TextBox)dr.FindControl("txtsellingprice")).Text;
                    if (sellingPrice.Trim().Length == 0)
                    {
                        sellingPrice = "0";
                    }
                    if (sellingPrice == "0")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showlalert", "alert('Please Enter Selling Price');", true);
                        return;
                    }

                    Quantity = ((TextBox)dr.FindControl("txtquantity")).Text;

                    if (Quantity.Trim().Length == 0)
                    {
                        Quantity = "0";
                    }
                    if (Quantity == "0")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showlalert", "alert('Please Enter Quantity');", true);
                        return;
                    }

                    string Transactionid = DateTime.Now.Month + "/" + DateTime.Now.Year + "/" + 0/*TransactionId.ToString()*/;
                    string date = DateTime.Now.Month.ToString() + "/" + DateTime.Now.Day.ToString() + "/" + DateTime.Now.Year.ToString();
                    string insertQuery = string.Format("insert into inflowdetails(itemid,Quantity,Price," +
                        " Sellingprice,Date,TransactionId) values({0},{1},{2},'{3}','{4}','{5}')",
                        ItemId, Quantity, Price, sellingPrice, date, Transactionid);

                    Status =config.ExecuteNonQueryWithQueryAsync(insertQuery).Result;
                    //string UpdateQuery = " update StockItemlist set ActualQuantity = ActualQuantity + " + 
                    //Quantity + ",price=" + Price + " where itemid = '" + ItemId + "'";

                    string UpdateQuery = " update StockItemlist set ActualQuantity = ActualQuantity + " +
                        Quantity + ",buyingprice=" + Price + ",sellingprice='" + sellingPrice + "' where itemid = '" + ItemId + "'";
                   int upda=config.ExecuteNonQueryWithQueryAsync(UpdateQuery).Result;
                    CheckRowIndex++;
                    //if (Status != 0)
                    //{
                    //   // LblResult.Text = " Record Inserted Successfully";
                    //}
                    //else
                    //    LblResult.Text = "Record Not Inserted";

                    txtquantity.Text = string.Empty;
                    txtSellingprice.Text = string.Empty;
                    txtprice.Text = string.Empty;
                    ddlitemid.SelectedIndex = 0;
                    ddlitemname.SelectedIndex = 0;
                }

                if (Status != 0)
                {

                    //  ScriptManager.RegisterStartupScript(this, GetType(), "Showalert()", "alert(' Record Inserted Successfully')", true);
                    ScriptManager.RegisterStartupScript(this, GetType(), "showlalert", "alert('Stock Added  Sucessfully');", true);

                    Session["ContractsAIndex"] = 1;
                    displaydata();
                    DisplayDefaultRow(sender, e);
                }

            }
            catch (Exception ex)
            {
                LblResult.Text = ex.Message;
            }
            //Session["ContractsAIndex"] = 0;
        }
    }
}