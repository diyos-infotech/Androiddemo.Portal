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
    public partial class ApproveMRF : System.Web.UI.Page
    {
        AppConfiguration config = new AppConfiguration();
        GridViewExportUtil gve = new GridViewExportUtil();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowStockPanel.Visible = false;
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
                            //ReceiptsLink.Visible = true;
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
                FillMRFIds();
                displaydata();

            }
        }


        private void getMRFId()
        {
            int MRFId = 0;
            string selectqueryclientid = "select Max(cast(MRFId as int)) as MRfID from MRF";
            DataTable dt = config.ExecuteAdaptorAsyncWithQueryParams(selectqueryclientid).Result;
            if (dt.Rows.Count > 0)
            {

                if (String.IsNullOrEmpty(dt.Rows[0]["mrfid"].ToString()) == false)
                {
                    MRFId = (Convert.ToInt32(dt.Rows[0]["mrfid"].ToString())) + 1;
                    //txtMRFId.Text = MRFId.ToString();
                }
                else
                {
                    MRFId = 1;
                }
            }
        }


        protected void FillMRFIds()
        {

            string selectclientid = "select DISTINCT MRFId from MRF where Status=-1";
            DataTable dt = config.ExecuteAdaptorAsyncWithQueryParams(selectclientid).Result;
            ddlMRFs.Items.Clear();
            ddlMRFs.Items.Add("--Select--");
            for (int rowno1 = 0; rowno1 < dt.Rows.Count; rowno1++)
            {
                ddlMRFs.Items.Add(dt.Rows[rowno1][0].ToString());
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
                    InventoryLink.Visible = true;
                    ReportsLink.Visible = true;
                    Operationlink.Visible = false;
                    MRFLink.Visible = true;

                    CompanyInfoLink.Visible = false;
                    SettingsLink.Visible = false;

                    break;
                default:
                    break;


            }
        }

        private void displaydata()
        {
            string selectquery = "Select DISTINCT MRFId,ClientId,Date from MRF where Status=-1";
            DataTable dt = null;
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

        protected void Approve_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlMRFs.SelectedIndex > 0)
                {
                    for (int i = 0; i < gvMRFDetails.Rows.Count; i++)
                    {
                        int qty = 0;
                        TextBox txtBox = (TextBox)gvMRFDetails.Rows[i].FindControl("txtApprovedQty");
                        Label itemId = (Label)gvMRFDetails.Rows[i].FindControl("lblItemId");
                        Label lblclosingstock = (Label)gvMRFDetails.Rows[i].FindControl("lblclosingstock");
                        if (txtBox != null)
                        {
                            if (txtBox.Text.Length > 0)
                            {
                                qty = Convert.ToInt16(txtBox.Text);
                            }
                            string strQry = "Update MRF set Status = 1,ApprovedQty=" + qty +
                                ",DispachedDate='" + DateTime.Now.ToString("MM/dd/yyyy") + "',CSBDispatch='" + lblclosingstock.Text +
                                "'    where MRFId = '" + ddlMRFs.SelectedValue +
                                "' AND ItemId='" + itemId.Text + "'";
                            int status = 0;// SqlHelper.Instance.ExecuteDMLQry(strQry);

                            status = config.ExecuteNonQueryWithQueryAsync(strQry).Result;

                            if (status == 1)
                            {
                                lblresult.Text = "MRF Approved";
                            }
                            else
                            {

                            }
                        }
                    }
                }
                else
                {
                    lblresult.Text = "Select MRF Id";
                    return;
                }
                displaydata();
                FillMRFIds();
            }
            catch (Exception ex)
            {

            }
            //lblresult.Text = "";
        }

        protected void Reject_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlMRFs.SelectedIndex > 0)
                {
                    string strQry = "Update MRF set Status = 0 where MRFId = '" + ddlMRFs.SelectedValue + "'";
                    int status = 0;// SqlHelper.Instance.ExecuteDMLQry(strQry);
                    status =config.ExecuteNonQueryWithQueryAsync(strQry).Result;

                    if (status == 1)
                    {
                        lblresult.Text = "MRF Rejected";
                    }
                    else
                    {

                    }
                    displaydata();
                    FillMRFIds();
                }
                else
                {
                    lblresult.Text = "Select MRF Id";
                    return;
                }
            }
            catch (Exception ex)
            {

            }
            //lblresult.Text = "";
        }

        protected void ddlMRFs_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblresult.Text = "";
            txtClient.Text = "";
            if (ddlMRFs.SelectedIndex > 0)
            {
                string sqlQry = "Select MRF.Sno, MRF.MRFId,MRF.ClientId,MRF.ItemId,MRF.Quantity,MRF.ClosingStock  from  " +
                    " MRF Where  MRF.MRFId = '"
                    + ddlMRFs.SelectedValue + "'";

                DataTable dTable = null;// SqlHelper.Instance.GetTableByQuery(sqlQry);
                dTable = config.ExecuteAdaptorAsyncWithQueryParams(sqlQry).Result;
                if (dTable.Rows.Count > 0)
                {
                    gvMRFDetails.DataSource = dTable;
                    gvMRFDetails.DataBind();


                    foreach (GridViewRow gvrow in gvMRFDetails.Rows)
                    {
                        // lblItemId,lblItemName,lblclosingstock
                        Label lblItemId = (Label)gvrow.FindControl("lblItemId");
                        Label lblItemName = (Label)gvrow.FindControl("lblItemName");
                        lblItemName.Text = Getitemname(lblItemId.Text);
                        Label lblclosingstock = (Label)gvrow.FindControl("lblclosingstock");
                        lblclosingstock.Text = Getclosingstock(lblItemId.Text);
                    }

                }
                else
                {
                    gvMRFDetails.DataSource = null;
                    gvMRFDetails.DataBind();
                }


                //get Item Names

                #region
                foreach (GridViewRow row in gvMRFDetails.Rows)
                {
                    Label lblItemId = (Label)row.FindControl("lblItemId");
                    Label lblItemName = (Label)row.FindControl("lblItemName");
                    lblItemName.Text = Getitemname(lblItemId.Text);
                    Label lblclosingstock = (Label)row.FindControl("lblclosingstock");
                    lblclosingstock.Text = Getclosingstock(lblItemId.Text);
                }

                #endregion

                if (dTable.Rows.Count > 0)
                {

                    string Cid = "";
                    txtClient.Text = dTable.Rows[0]["ClientId"].ToString();
                    Cid = dTable.Rows[0]["ClientId"].ToString();
                    string SqlQryForCName = "Select ClientName From Clients where clientid='" + Cid + "'";
                    DataTable DtCname = null;
                    DtCname = config.ExecuteAdaptorAsyncWithQueryParams(SqlQryForCName).Result;
                    if (DtCname.Rows.Count > 0)
                        txtCname.Text = DtCname.Rows[0]["ClientName"].ToString();
                }
            }
            else
            {
                displaydata();
                gvMRFDetails.DataSource = null;
                gvMRFDetails.DataBind();
            }
        }

        protected void gvMRFDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TextBox txtAppQty = (TextBox)e.Row.FindControl("txtApprovedQty");
                if (txtAppQty != null)
                {
                    Label lblQty = (Label)e.Row.FindControl("lblQty");
                    if (lblQty != null)
                    {
                        txtAppQty.Text = lblQty.Text;
                    }
                }
            }
        }

        protected void ShowStockList_CheckedChanged(object sender, EventArgs e)
        {
            if (ShowStockList.Checked == true)
            {
                //if (ddlMRFs.SelectedIndex <= 0)
                //{
                //    lblresult.Text = "Select MRF ID to show stock";
                //    ShowStockList.Checked = false;
                //}
                //else
                //{
                ShowStockPanel.Visible = true;
                gvShowStock.DataSource = null;
                gvShowStock.DataBind();
                //}
            }
            else
            {
                ShowStockPanel.Visible = false;
            }
        }

        protected void btnShowStockList_Click(object sender, EventArgs e)
        {
            string fromDate = txtFromDate.Text.Trim();
            string toDate = txtToDate.Text.Trim();
            if (fromDate.Length <= 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showlalert", "alert('Please select From Date');", true);

                return;
            }
            if (toDate.Length <= 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showlalert", "alert('Please select To Date');", true);
                return;
            }
            DateTime fDate = Convert.ToDateTime(fromDate);
            DateTime tDate = Convert.ToDateTime(toDate);
            if (fDate > tDate)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showlalert", "alert('From Date should not greater than To Date');", true);
                return;
            }

            string clientID = txtClient.Text;
            //if (clientID.Length > 0)
            //{
            //string strQry = "Select DISTINCT MRF.ItemId, sil.ItemName, Sum(MRF.ApprovedQty) as Quantity,sil.Price, "+
            //    " (Sum(MRF.ApprovedQty)*sil.Price) as cost from MRF INNER JOIN StockItemList as sil  "+
            //    " ON MRF.ItemId=sil.ItemId where MRF.status=1 AND MRF.Date>='" + fDate +
            //    "' AND MRF.Date<='" + tDate + "' AND MRF.ClientId='" + clientID + "' Group by MRF.ItemId,sil.ItemName,sil.Price";

            //string strQry = "Select DISTINCT MRF.ItemId, Sum(MRF.ApprovedQty) as Quantity " +
            //"  from MRF  " +
            //" where MRF.status=1 AND MRF.Date>='" + fDate +
            //"' AND MRF.Date<='" + tDate + "' AND MRF.ClientId='" + clientID + "' Group by MRF.ItemId";
            //DataTable dt = null;

            //New code as on 07/01/2014 for dispatch stock between the dates add by venkat

            string strQry = "Select DISTINCT MRF.ItemId, Sum(MRF.ApprovedQty) as Quantity,mrfid " +
               "  from MRF  " +
               " where MRF.status=1 AND MRF.DispachedDate>='" + fDate +
               "' AND MRF.DispachedDate<='" + tDate + "' Group by MRF.ItemId,mrfid";
            DataTable dt = null;

            dt = config.ExecuteAdaptorAsyncWithQueryParams(strQry).Result;

            if (dt.Rows.Count > 0)
            {
                gvShowStock.DataSource = dt;
                gvShowStock.DataBind();

                foreach (GridViewRow gvrow in gvShowStock.Rows)
                {
                    Label lblMrfId = (Label)gvrow.FindControl("lblMrfId");
                    Label lblItemId = (Label)gvrow.FindControl("lblItemId");
                    Label lblItemName = (Label)gvrow.FindControl("lblItemName");
                    Label lblQty = (Label)gvrow.FindControl("lblQty");
                    Label lblprice = (Label)gvrow.FindControl("lblPrice");
                    Label lblCost = (Label)gvrow.FindControl("lblCost");
                    lblItemName.Text = Getitemname(lblItemId.Text);
                    lblprice.Text = Getitemprice(lblItemId.Text);

                    float qty = 0;
                    float price = 0;
                    if (lblQty.Text.Trim().Length > 0)
                    {
                        qty = float.Parse(lblQty.Text);
                    }

                    if (lblprice.Text.Trim().Length > 0)
                    {
                        price = float.Parse(lblprice.Text);
                    }

                    if (lblQty.Text.Trim().Length != 0)
                    {
                        lblCost.Text = (qty * price).ToString();
                    }

                }
            }
            else
            {
                gvShowStock.DataSource = null;
                gvShowStock.DataBind();
            }
            //}
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



        protected string Getitemprice(string itemid)
        {

            string sqlqry = "Select sellingprice from stockitemlist Where itemid='" + itemid + "'";
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
    }
}