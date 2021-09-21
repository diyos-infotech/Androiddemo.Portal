using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KLTS.Data;
using System.Data;
using Jawan.Portal.DAL;
namespace Jawan.Portal
{
    public partial class DeleteStock : System.Web.UI.Page
    {
        AppConfiguration config = new AppConfiguration();
        GridViewExportUtil gve = new GridViewExportUtil();
        DataTable dt;
        private void displaydata()
        {
            string selectquery = " select ItemId,ItemName,MinimumQty,BuyingPrice,ActualQuantity from StockItemList Order By cast(itemid as int)";
            dt = config.ExecuteAdaptorAsyncWithQueryParams(selectquery).Result;
            gvstock.DataSource = dt;
            gvstock.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
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
                displaydata();
                FillItemIds();
            }

        }

        protected void FillItemIds()
        {
            string selectquery = " select ItemId from Stockitemlist order by (cast(itemid as int))";
            DataTable dtable = config.ExecuteAdaptorAsyncWithQueryParams(selectquery).Result;
            ddlItems.Items.Clear();
            ddlItems.Items.Add("--Select--");
            for (int i = 0; i < dtable.Rows.Count; i++)
            {
                ddlItems.Items.Add(dtable.Rows[i]["ItemId"].ToString());
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

        protected void ddlItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtItemName.Text = "";
            lblresult.Text = "";
            if (ddlItems.SelectedIndex > 0)
            {
                string selectquery = " select ItemName from Stockitemlist where ItemId='" + ddlItems.SelectedValue + "'";
                DataTable dtable = config.ExecuteAdaptorAsyncWithQueryParams(selectquery).Result;
                if (dtable.Rows.Count > 0)
                {
                    txtItemName.Text = dtable.Rows[0][0].ToString();
                }
            }
            displaydata();
        }

        protected void Delete_Click(object sender, EventArgs e)
        {
            /****/
            //Deleted stock record at to be maintained in some table
            //Not yet done
            //Need to complete it.
            /************/
            if (ddlItems.SelectedIndex > 0)
            {
                if (txtDelQty.Text.Trim().Length <= 0)
                {
                    lblresult.Text = "Please enter delete quantity";
                    return;
                }
                //if (txtRemarks.Text.Trim().Length <= 0)
                //{
                //    lblresult.Text = "Please enter delete remarks";
                //    return;
                //}
                int qty = Convert.ToInt32(txtDelQty.Text.Trim());
                string strQry = "select ActualQuantity from Stockitemlist where Itemid = '" + ddlItems.SelectedValue + "'";
                DataTable dtable = config.ExecuteAdaptorAsyncWithQueryParams(strQry).Result;
                if (dtable.Rows.Count > 0)
                {
                    int actualQty = Convert.ToInt32(dtable.Rows[0][0].ToString());
                    if (actualQty >= qty)
                    {
                        int nowQty = actualQty - qty;
                        strQry = "Update Stockitemlist set ActualQuantity = " + nowQty + " where Itemid = '" + ddlItems.SelectedValue + "'";
                        int status1 = config.ExecuteNonQueryWithQueryAsync(strQry).Result;
                        lblresult.Text = "Delete stock quantity done";
                    }
                    else
                    {
                        lblresult.Text = "Delete qty should be less than actual stock";
                        displaydata();
                        return;
                    }
                }
            }
            displaydata();
        }

        protected void gvstock_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvstock.PageIndex = e.NewPageIndex;
            displaydata();
        }


        protected void btnsave_Click(object sender, EventArgs e)
        {

            gvstock.DataSource = null;
            gvstock.DataBind();

            string Sqlqry = "select * from StockItemList  where  itemname  like '%" + txtsearchitem.Text.Trim() + "%'";
            DataTable dt = config.ExecuteAdaptorAsyncWithQueryParams(Sqlqry).Result;
            if (dt.Rows.Count > 0)
            {
                gvstock.DataSource = dt;
                gvstock.DataBind();
            }

            else
            {
                lblresult.Text = "There Is No Items Which Is You Write ";
                return;
            }

        }
    }
}