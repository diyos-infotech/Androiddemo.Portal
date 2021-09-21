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
using Jawan.Portal.DAL;
namespace Jawan.Portal
{
    public partial class MaterialRequisitForm : System.Web.UI.Page
    {
        AppConfiguration config = new AppConfiguration();
        GridViewExportUtil gve = new GridViewExportUtil();
        DataTable dt;
        DropDownList bind_dropdownlist;
        //static int activeindex = 0;
        //static int activeindextwo = 0;
        //static int rowindexvisible = 0;
        string EmpIDPrefix = "";
        string CmpIDPrefix = "";
        string BranchID = "";
        string Elength = "";
        string Clength = "";

        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {

                GetWebConfigdata();
                if (!IsPostBack)
                {
                    Session["ContractsAIndex"] = 1;
                    if (Session["UserId"] != null && Session["AccessLevel"] != null)
                    {
                        lblDisplayUser.Text = Session["UserId"].ToString();
                        //PreviligeUsers(Convert.ToInt32(Session["AccessLevel"]));
                        lblcname.Text = SqlHelper.Instance.GetCompanyname();
                        switch (SqlHelper.Instance.GetCompanyValue())
                        {
                            case 0:// Write Frames Invisible Links
                                break;
                            case 1://Write KLTS Invisible Links
                                //  ReceiptsLink.Visible = true;
                                break;
                            default:
                                break;
                        }

                        PreviligeUsers(Convert.ToInt32(Session["AccessLevel"]));
                    }
                    else
                    {
                        Response.Redirect("login.aspx");
                    }
                    getMRFId();
                    LoadClientNames();
                    LoadClientList();
                }
            }
            catch (Exception ex)
            {

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
                ddlClients.DataValueField = "Clientid";
                ddlClients.DataTextField = "Clientid";
                ddlClients.DataSource = DtClientNames;
                ddlClients.DataBind();
            }
            ddlClients.Items.Insert(0, "-Select-");
            ddlClients.Items.Insert(1, "ALL");
        }

        protected void GetWebConfigdata()
        {
            EmpIDPrefix = Session["EmpIDPrefix"].ToString();
            CmpIDPrefix = Session["CmpIDPrefix"].ToString();
            BranchID = Session["BranchID"].ToString();
        }




        private void getMRFId()
        {
            int MRFId = 0;
            string selectqueryclientid = "select max(cast(MRFId as int )) as MRFId from MRF ";
            dt = config.ExecuteAdaptorAsyncWithQueryParams(selectqueryclientid).Result;
            if (dt.Rows.Count > 0)
            {
                if (String.IsNullOrEmpty(dt.Rows[0]["MRFId"].ToString()) == false)
                {
                    MRFId = (Convert.ToInt32(dt.Rows[0]["MRFId"].ToString())) + 1;
                    txtMRFId.Text = MRFId.ToString();
                }

                else
                {
                    txtMRFId.Text = "1";
                }
            }
        }

        protected void DisplayDefaultRow()
        {

            for (int i = 0; i < gvmrf.Rows.Count; i++)
            {
                if (i < 5)
                {
                    Session["ContractsAIndex"] = Convert.ToInt16(Session["ContractsAIndex"]) + 1;
                    gvmrf.Rows[i].Visible = true;
                }
                else
                    gvmrf.Rows[i].Visible = false;
            }
            Session["ContractsAIndex"] = 5;

        }

        protected void FillClientIdandName()
        {

            string selectclientid = "select clientid from clients  Order By cast(substring(clientid," + Clength + ", 4) as int)";
            string selectclientname = "select  clientid,Clientname from Clients    where clientstatus=1 order by Clientname";
            DataTable dtclientnames = null;
            dt = config.ExecuteAdaptorAsyncWithQueryParams(selectclientid).Result;
            dtclientnames = config.ExecuteAdaptorAsyncWithQueryParams(selectclientname).Result;
            ddlClients.Items.Clear();
            ddlcname.Items.Clear();
            if (dt.Rows.Count > 0)
            {
                ddlClients.DataTextField = "clientid";
                ddlClients.DataValueField = "clientid";
                ddlClients.DataSource = dt;
                ddlClients.DataBind();
                ddlClients.Items.Insert(0, "-Select-");
            }
            else
            {
                ddlClients.Items.Insert(0, "-Select-");
            }

            if (dtclientnames.Rows.Count > 0)
            {
                ddlcname.DataTextField = "Clientname";
                ddlcname.DataValueField = "clientid";
                ddlcname.DataSource = dtclientnames;
                ddlcname.DataBind();
                ddlcname.Items.Insert(0, "-Select-");
            }
            else
            {

                ddlcname.Items.Insert(0, "-Select-");
            }

        }

        protected void PreviligeUsers(int previligerid)
        {
            switch (previligerid)
            {

                case 1:
                    break;
                case 2:
                    AddClientLink.Visible = true;
                    ModifyClientLink.Visible = false;
                    DeleteClientLink.Visible = false;
                    ContractLink.Visible = false;
                    //ClientAttenDanceLink.Visible = false;
                    Operationlink.Visible = false;
                    // BillingLink.Visible = false;
                    MRFLink.Visible = false;

                    CompanyInfoLink.Visible = false;
                    InventoryLink.Visible = false;
                    ReportsLink.Visible = false;
                    SettingsLink.Visible = false;

                    break;

                case 3:

                    AddClientLink.Visible = true;
                    ModifyClientLink.Visible = true;
                    DeleteClientLink.Visible = true;
                    ContractLink.Visible = true;
                    // ClientAttenDanceLink.Visible = true;
                    Operationlink.Visible = true;
                    // BillingLink.Visible = false;
                    MRFLink.Visible = false;


                    InventoryLink.Visible = false;
                    ReportsLink.Visible = false;
                    SettingsLink.Visible = false;
                    break;
                case 4:

                    AddClientLink.Visible = false;
                    ModifyClientLink.Visible = false;
                    DeleteClientLink.Visible = false;
                    ContractLink.Visible = false;
                    // ClientAttenDanceLink.Visible = false;
                    Operationlink.Visible = false;
                    // BillingLink.Visible = true;
                    MRFLink.Visible = false;
                    CompanyInfoLink.Visible = false;
                    InventoryLink.Visible = false;
                    ReportsLink.Visible = false;
                    SettingsLink.Visible = false;
                    break;
                case 5:

                    AddClientLink.Visible = false;
                    ModifyClientLink.Visible = true;
                    DeleteClientLink.Visible = false;
                    ContractLink.Visible = false;
                    ClientAttendanceLink.Visible = true;
                    Operationlink.Visible = false;
                    LicensesLink.Visible = true;
                    //BillingLink.Visible = false;
                    MRFLink.Visible = true;

                    EmployeesLink.Visible = true;
                    ClientsLink.Visible = true;
                    CompanyInfoLink.Visible = false;
                    InventoryLink.Visible = false;
                    ReportsLink.Visible = true;
                    ReportsLink.Visible = true;
                    SettingsLink.Visible = true;
                    break;
                case 6:
                    AddClientLink.Visible = false;
                    ModifyClientLink.Visible = false;
                    DeleteClientLink.Visible = false;
                    ContractLink.Visible = false;
                    //ClientAttenDanceLink.Visible = true;
                    EmployeesLink.Visible = false;
                    LicensesLink.Visible = false;
                    ClientAttendanceLink.Visible = false;
                    //ReceiptsLink.Visible = false;


                    Operationlink.Visible = false;
                    // BillingLink.Visible = false;
                    MRFLink.Visible = true;
                    ApproveMRFLink.Visible = true;
                    CompanyInfoLink.Visible = false;
                    InventoryLink.Visible = true;
                    ReportsLink.Visible = true;
                    SettingsLink.Visible = false;

                    break;
                default:
                    break;


            }
        }

        private void displaydata()
        {
            string selectquery = "Select ItemId,ItemName from StockItemList Order by (cast(itemid as int))";
            dt = config.ExecuteAdaptorAsyncWithQueryParams(selectquery).Result;


            gvmrf.DataSource = dt;
            gvmrf.DataBind();
            //ii++;
            DataRow row = dt.NewRow();
            row["ItemId"] = "--Select--";
            row["ItemName"] = "";
            dt.Rows.InsertAt(row, 0);

            foreach (GridViewRow grdRow in gvmrf.Rows)
            {


                bind_dropdownlist = (DropDownList)(gvmrf.Rows[grdRow.RowIndex].Cells[0].FindControl("ddlItemID"));
                bind_dropdownlist.Items.Insert(0, "--Select--");
                bind_dropdownlist.DataSource = dt;
                bind_dropdownlist.DataValueField = "itemid";
                bind_dropdownlist.DataTextField = "itemid";
                bind_dropdownlist.DataBind();
                bind_dropdownlist = (DropDownList)(gvmrf.Rows[grdRow.RowIndex].Cells[0].FindControl("ddlitemname"));

                string Sqlmsureunits = "Select itemname,ItemId from  Stockitemlist";
                DataTable Dtmesurements = config.ExecuteAdaptorAsyncWithQueryParams(Sqlmsureunits).Result;
                bind_dropdownlist.DataSource = Dtmesurements;
                bind_dropdownlist.DataValueField = "ItemId";
                bind_dropdownlist.DataTextField = "itemname";
                bind_dropdownlist.DataBind();
                bind_dropdownlist.Items.Insert(0, "--Select--");
                if (grdRow.RowIndex > 4)
                    break;
            }
        }

        protected void getitemname(object sender, GridViewUpdateEventArgs e)
        {
        }

        protected void btnaddnewitem_Click(object sender, EventArgs e)
        {
            int designCount = Convert.ToInt16(Session["ContractsAIndex"]);

            if (designCount < gvmrf.Rows.Count)
            {
                string selectquery = "Select ItemId,ItemName from StockItemList Order By (Cast(Itemid as int))";
                DataTable DtDesignation = config.ExecuteAdaptorAsyncWithQueryParams(selectquery).Result;

                //ii++;

                DataRow row = DtDesignation.NewRow();
                row["ItemId"] = "--Select--";
                row["ItemName"] = "";
                DtDesignation.Rows.InsertAt(row, 0);

                gvmrf.Rows[designCount].Visible = true;

                DropDownList ddldrow = gvmrf.Rows[designCount].FindControl("ddlItemID") as DropDownList;

                ddldrow.DataSource = DtDesignation;
                ddldrow.DataValueField = "itemid";
                ddldrow.DataTextField = "itemid";
                ddldrow.DataBind();

                string selectqueryitemanme = "Select itemid, itemname from StockItemList Order By itemname ";
                DataTable Dtitemaname = config.ExecuteAdaptorAsyncWithQueryParams(selectqueryitemanme).Result;

                DropDownList ddldrowitemname = gvmrf.Rows[designCount].FindControl("ddlitemname") as DropDownList;

                ddldrowitemname.DataSource = Dtitemaname;
                ddldrowitemname.DataValueField = "itemid";
                ddldrowitemname.DataTextField = "Itemname";
                ddldrowitemname.DataBind();
                ddldrowitemname.Items.Insert(0, "--Select--");
                Label Sno = gvmrf.Rows[designCount].FindControl("lblsno") as Label;
                designCount = designCount + 1;
                Sno.Text = designCount.ToString();

                Session["ContractsAIndex"] = designCount;
            }
            else
            {
                lblresult.Text = "Theres is No more Items";
            }
        }

        protected void gvmrf_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }

        protected void gvmrf_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            lblresult.Text = "";
            lblresult.Visible = true;
            try
            {
                if (ddlClients.SelectedIndex > 0)
                {
                    foreach (GridViewRow dr in gvmrf.Rows)
                    {
                        Label Sno = (Label)dr.FindControl("lblsno");
                        DropDownList ddlItem = (DropDownList)dr.FindControl("ddlitemid");
                        if (ddlItem.SelectedIndex > 0)
                        {
                            string itemid = ddlItem.SelectedItem.Text;
                            string closingstock = ((TextBox)dr.FindControl("txtClosingStock")).Text;
                            if (closingstock.Length <= 0)
                            {
                                lblresult.Text = "Please enter closing stock";
                                return;
                            }
                            string Quantity = ((TextBox)dr.FindControl("txtQuantity")).Text;
                            float enteredqty = 0;
                            enteredqty = float.Parse(Quantity);

                            //if (Quantity.Length <= 0)
                            //{
                            //    ScriptManager.RegisterStartupScript(this, GetType(), "showlalert", "alert('Please enter Req. Quantity of stock');", true);
                            //    return;
                            //}
                            if (enteredqty == 0)
                            {
                                lblresult.Text = "Please enter Req. Quantity of stock";
                                return;
                            }

                            string SellingPrice = ((TextBox)dr.FindControl("txtsellingprice")).Text;

                            //Code added for selling Price checking when it is Zero as on 06/12/2013 by venkat
                            if (SellingPrice.Length <= 0)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "showlalert", "alert('Please enter The Selling Price');", true);
                                return;
                            }
                            float Price = 0;
                            Price = float.Parse(SellingPrice);
                            if (Price == 0)
                            {
                                lblresult.Text = "Please enter The Selling Price";
                                return;
                            }


                            string empId = "";

                            string insertquey = string.Format("insert into mrf(MRFId,EmpId,ClientId,ItemId,Quantity,ClosingStock,Date," +
                                " DispatchStatus,SellingPrice,Sno) values('{0}','{1}','{2}','{3}',{4},{5},'{6}',{7},'{8}','{9}')",
                                txtMRFId.Text, empId, ddlClients.SelectedValue, itemid, Convert.ToInt32(Quantity),
                                Convert.ToInt32(closingstock), DateTime.Now.ToString("MM/dd/yyyy"), 0, SellingPrice, Sno.Text);
                            int status = 0;
                            status =config.ExecuteNonQueryWithQueryAsync(insertquey).Result;
                            if (status != 0)
                            {
                                lblresult.Text = "Material Added Successfully";
                            }
                            else
                            {
                                lblresult.Text = "Material Not Added ";
                            }
                        }
                    }
                    ddlClients.SelectedIndex = 0;
                    ddlcname.SelectedIndex = 0;

                }
                else
                {
                    lblresult.Text = "Select Client Id";
                    return;
                }
            }
            catch (Exception ex)
            {

            }
            //lblresult.Text = "";
        }

        protected void DropDownList_DataBound(object sender, EventArgs e)
        {
        }
        //static int ii = 0;
        double TOTALAMOUNT = 0;
        protected void gvmrf_RowDataBound1(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gvmrf_Init(object sender, GridViewUpdatedEventArgs e)
        {
            //if (ii == 0)
            {
                //ii = ii + 1;
                //displaydata();
            }
        }

        protected void contractdetails()
        {
            string sqlqry = "Select MaterialCostPerMonth,MachinaryCostPerMonth from contracts Where Clientid='" + ddlClients.SelectedValue + "'";

            DataTable dt = config.ExecuteAdaptorAsyncWithQueryParams(sqlqry).Result;


            if (dt.Rows.Count > 0)
            {
                txtmcpm.Text = dt.Rows[0]["MaterialCostPerMonth"].ToString();
                txtmecpm.Text = dt.Rows[0]["MachinaryCostPerMonth"].ToString();
            }
            else
            {
                txtmcpm.Text = "0";
                txtmecpm.Text = "0";
            }
        }

        protected void ddlClients_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblresult.Text = "";
            if (ddlClients.SelectedIndex > 0)
            {
                ddlcname.SelectedValue = ddlClients.SelectedValue;
                contractdetails();
                displaydata();
                getMRFId();
                DisplayDefaultRow();
            }
            else
            {
                ddlcname.SelectedIndex = 0;
                contractdetails();
            }
        }

        protected void ddlcname_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            lblresult.Text = "";
            if (ddlcname.SelectedIndex > 0)
            {
                ddlClients.SelectedValue = ddlcname.SelectedValue;
                contractdetails();
                displaydata();
                getMRFId();
                DisplayDefaultRow();
            }
            else
            {
                ddlClients.SelectedIndex = 0;
                contractdetails();
            }
        }

        //string selecteditem;

        //Display The Item Name When We Select Item Id
        protected void DDL_GetItemName(object sender, EventArgs e)
        {
            lblresult.Text = "";
            DropDownList ddl = sender as DropDownList;
            GridViewRow row = null;
            if (ddl == null)
                return;

            row = (GridViewRow)ddl.NamingContainer;
            if (row == null)
                return;
            DropDownList ddlItemName = row.FindControl("ddlitemname") as DropDownList;
            DropDownList ddlItemId = row.FindControl("ddlitemid") as DropDownList;
            TextBox closingstock = row.FindControl("txtClosingStock") as TextBox;
            if (ddlItemName == null)
                return;

            if (ddl.SelectedIndex == 0)
                ddlItemName.SelectedIndex = 0;
            else
            {
                string selectquery = " select Itemid,Buyingprice,ActualQuantity from Stockitemlist where itemid = " + ddl.SelectedValue;
                DataTable dtitemanme = config.ExecuteAdaptorAsyncWithQueryParams(selectquery).Result;
                if (dtitemanme.Rows.Count > 0)
                {
                    ddlItemName.SelectedValue = ddlItemId.SelectedValue;
                    closingstock.Text = dtitemanme.Rows[0][2].ToString();
                }
            }
        }

        protected void DDL_GetItemId(object sender, EventArgs e)
        {
            lblresult.Text = "";
            DropDownList ddl = sender as DropDownList;
            GridViewRow row = null;
            if (ddl == null)
                return;

            row = (GridViewRow)ddl.NamingContainer;
            if (row == null)
                return;
            DropDownList ddlItemId = row.FindControl("ddlitemid") as DropDownList;
            DropDownList ddlItemName = row.FindControl("ddlitemname") as DropDownList;
            TextBox closingstock = row.FindControl("txtClosingStock") as TextBox;
            if (ddlItemId == null)
                return;

            if (ddl.SelectedIndex == 0)
                ddlItemId.SelectedIndex = 0;
            else
            {
                string selectquery = " select itemid,ActualQuantity from Stockitemlist where itemname = '" + ddl.SelectedValue + "'";
                DataTable dtitemanme = config.ExecuteAdaptorAsyncWithQueryParams(selectquery).Result;
                if (dtitemanme.Rows.Count > 0)
                {

                    ddlItemId.SelectedValue = ddlItemName.SelectedValue;
                    closingstock.Text = dtitemanme.Rows[0][1].ToString();
                }
            }

        }

        protected void txtQuantity_ValueChanged(object sender, EventArgs e)
        {
            lblresult.Text = "";
            TextBox quantity = sender as TextBox;
            GridViewRow row = null;
            if (quantity == null)
                return;
            //  DropDownList ddl = sender as DropDownList;
            row = (GridViewRow)quantity.NamingContainer;
            if (row == null)
                return;
            TextBox totalquantity = row.FindControl("txtQuantity") as TextBox;
            TextBox totalamount = row.FindControl("txttotalamount") as TextBox;
            TextBox sellingprice = row.FindControl("txtsellingprice") as TextBox;
            if (totalquantity == null)
                return;

            if (totalquantity.Text.Trim().Length == 0)
            {
                totalamount.Text = "0.00";
                sellingprice.Text = "0.00";
                return;
            }
            DropDownList ddlItemId = row.FindControl("ddlitemid") as DropDownList;
            if (ddlItemId == null)
                return;

            if (ddlItemId.SelectedIndex == 0)
            {
                ddlItemId.SelectedIndex = 0;
                totalamount.Text = "0.00";
                sellingprice.Text = "0.00";
                quantity.Text = "0";
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please select itemid/Name');", true);
                return;
            }
            else
            {
                string sqlqry = "Select isnull(Sellingprice,0) as sellingprice,isnull(buyingprice,0) as buyingprice, " +
                " isnull(actualquantity,0) as actualquantity from stockitemlist Where itemid= '" + ddlItemId.SelectedValue + "'";
                // string selectquery = " select itemid from Stockitemlist where itemname = '" + ddlItemId.SelectedValue + "'";
                DataTable dtitemanme = config.ExecuteAdaptorAsyncWithQueryParams(sqlqry).Result;


                if (dtitemanme.Rows.Count > 0)
                {

                    int actualquantity = int.Parse(dtitemanme.Rows[0]["actualquantity"].ToString());
                    int enteredquantity = int.Parse(totalquantity.Text.Trim());

                    //New code add for compare the Closing stock and Entered Quantity as on 04/01/2014 by venkat

                    if (enteredquantity > actualquantity)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Show alert", "alert('Ordered Quantity should be less than or equal to Closing Stock');", true);
                        DDL_GetItemId(sender, e);
                        totalquantity.Text = "0";
                        return;
                    }

                    float selllingprice = float.Parse(dtitemanme.Rows[0]["sellingprice"].ToString());
                    float TotalAmount = (float.Parse(dtitemanme.Rows[0]["sellingprice"].ToString())) * float.Parse(enteredquantity.ToString());
                    totalamount.Text = TotalAmount.ToString();
                    sellingprice.Text = selllingprice.ToString();
                    TextBox closingstock = row.FindControl("txtClosingStock") as TextBox;
                    closingstock.Text = ((float.Parse(dtitemanme.Rows[0][2].ToString())) - (float.Parse(totalquantity.Text.ToString()))).ToString();

                }
            }
            Totalmeterialcost();

        }

        protected void showprices()
        {

        }

        protected void txtsellingprice_ValueChanged(object sender, EventArgs e)
        {
            lblresult.Text = "";
            TextBox quantity = sender as TextBox;
            GridViewRow row = null;
            if (quantity == null)
                return;
            //  DropDownList ddl = sender as DropDownList;
            row = (GridViewRow)quantity.NamingContainer;
            if (row == null)
                return;
            TextBox totalquantity = row.FindControl("txtQuantity") as TextBox;
            TextBox totalamount = row.FindControl("txttotalamount") as TextBox;
            TextBox sellingprice = row.FindControl("txtsellingprice") as TextBox;
            if (totalquantity == null)
                return;

            if (totalquantity.Text.Trim().Length == 0)
            {
                totalamount.Text = "0.00";
                sellingprice.Text = "0.00";
                return;
            }
            DropDownList ddlItemId = row.FindControl("ddlitemid") as DropDownList;
            if (ddlItemId == null)
                return;

            if (ddlItemId.SelectedIndex == 0)
            {
                ddlItemId.SelectedIndex = 0;
                totalamount.Text = "0.00";
                sellingprice.Text = "0.00";
                quantity.Text = "0";
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please select itemid/Name');", true);
                return;
            }
            else
            {
                string sqlqry = "Select isnull(Sellingprice,0) as sellingprice,isnull(buyingprice,0) as buyingprice,isnull(actualquantity,0) as actualquantity from stockitemlist Where itemid= '" + ddlItemId.SelectedValue + "'";
                // string selectquery = " select itemid from Stockitemlist where itemname = '" + ddlItemId.SelectedValue + "'";
                DataTable dtitemanme = config.ExecuteAdaptorAsyncWithQueryParams(sqlqry).Result;
                if (dtitemanme.Rows.Count > 0)
                {

                    int actualquantity = int.Parse(dtitemanme.Rows[0]["actualquantity"].ToString());
                    int enteredquantity = int.Parse(totalquantity.Text.Trim());
                    float selllingprice = 0;

                    float TotalAmount = (float.Parse(dtitemanme.Rows[0]["sellingprice"].ToString())) * float.Parse(enteredquantity.ToString());
                    totalamount.Text = TotalAmount.ToString();
                    sellingprice.Text = sellingprice.Text.ToString();

                    if (totalquantity.Text.Length != 0 && sellingprice.Text.Length != 0)
                    {
                        TotalAmount = (float.Parse(sellingprice.Text.ToString())) * float.Parse(enteredquantity.ToString());
                        totalamount.Text = TotalAmount.ToString();
                    }

                }
                Totalmeterialcost();
            }
        }

        public void Totalmeterialcost()
        {
            float Total = 0;
            float ClosingStockTotal = 0;
            foreach (GridViewRow dr in gvmrf.Rows)
            {
                DropDownList ddlItem = (DropDownList)dr.FindControl("ddlitemid");

                if (ddlItem.SelectedIndex > 0)
                {
                    string ClosingStock = ((TextBox)dr.FindControl("txtQuantity")).Text;
                    string Totalamnt = ((TextBox)dr.FindControl("txttotalamount")).Text;

                    if (ClosingStock.Trim().Length != 0)
                    {
                        ClosingStockTotal += float.Parse(ClosingStock.ToString());

                    }

                    if (Totalamnt.Trim().Length != 0)
                    {
                        Total += float.Parse(Totalamnt.ToString());
                    }
                }


            }

            lblbtotal.Text = ClosingStockTotal.ToString("0.00");
            lblstotal.Text = Total.ToString("0.00");
        }

        protected void ddlsitename_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            FillClientIdandName();

        }
    }
}