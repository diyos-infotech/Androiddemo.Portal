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
using System.Data;
using KLTS.Data;
using System.Globalization;
using System.Text;
using System.Collections.Generic;
using Jawan.Portal.DAL;
namespace Jawan.Portal
{
    public partial class ItemWiseReport : System.Web.UI.Page
    {
        AppConfiguration config = new AppConfiguration();
        GridViewExportUtil gve = new GridViewExportUtil();
        protected void Page_Load(object sender, EventArgs e)
        {

            GetWebConfigdata();

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

                loadItemIDs();
                loaditemNames();
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
                    //EmployeeReportLink.Visible = true;
                    //ClientsReportLink.Visible = true;
                    //InventoryReportLink.Visible = true;

                    EmployeesLink.Visible = true;
                    ClientsLink.Visible = true;
                    CompanyInfoLink.Visible = false;
                    InventoryLink.Visible = false;
                    ReportsLink.Visible = true;
                    SettingsLink.Visible = true;
                    break;

                case 4:
                    //EmployeeReportLink.Visible = true;
                    //ClientsReportLink.Visible = true;
                    //InventoryReportLink.Visible = true;

                    EmployeesLink.Visible = true;
                    ClientsLink.Visible = true;
                    CompanyInfoLink.Visible = true;
                    InventoryLink.Visible = true;
                    ReportsLink.Visible = true;
                    SettingsLink.Visible = true;

                    break;
                case 5:
                    //EmployeeReportLink.Visible = true;
                    //ClientsReportLink.Visible = true;
                    //InventoryReportLink.Visible = true;
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
                    ClientsLink.Visible = false;
                    CompanyInfoLink.Visible = false;
                    InventoryLink.Visible = true;
                    ReportsLink.Visible = true;
                    SettingsLink.Visible = false;
                    //EmployeeReportLink.Visible = false;

                    break;
                case 7:
                    break;
                case 8:
                    EmployeesLink.Visible = false;
                    ClientsLink.Visible = false;
                    CompanyInfoLink.Visible = false;
                    ReportsLink.Visible = false;
                    SettingsLink.Visible = false;
                    break;
                default:
                    break;
            }
        }

        protected void GetWebConfigdata()
        {
            //EmpIDPrefix = Session["EmpIDPrefix"].ToString();
            //CmpIDPrefix = Session["CmpIDPrefix"].ToString();
            //GRVPrefix = Session["GRVPrefix"].ToString();
        }


        public void loadItemIDs()
        {
            string sqlqry = "Select itemId  from InvStockItemList";
            DataTable dt = config.ExecuteAdaptorAsyncWithQueryParams(sqlqry).Result;
            if (dt.Rows.Count > 0)
            {
                ddlItemID.DataValueField = "itemId";
                ddlItemID.DataTextField = "itemId";
                ddlItemID.DataSource = dt;
                ddlItemID.DataBind();
            }

            ddlItemID.Items.Insert(0, "-Select-");
        }

        public void loaditemNames()
        {
            string sqlqry = "Select itemId,itemname  from InvStockItemList";
            DataTable dt = config.ExecuteAdaptorAsyncWithQueryParams(sqlqry).Result;
            if (dt.Rows.Count > 0)
            {
                ddlItemname.DataValueField = "itemId";
                ddlItemname.DataTextField = "itemname";
                ddlItemname.DataSource = dt;
                ddlItemname.DataBind();
            }

            ddlItemname.Items.Insert(0, "-Select-");
        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {

            GVPODetails.DataSource = null;
            GVPODetails.DataBind();



            var FromDate = DateTime.Now;
            var ToDate = DateTime.Now;


            if (Txt_From_Date.Text.Trim().Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showlalert", "alert('Please Fill the From Date');", true);
                return;
            }
            if (Txt_To_Date.Text.Trim().Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showlalert", "alert('Please Fill the To Date');", true);
                return;
            }

            FromDate = DateTime.Parse(Txt_From_Date.Text.Trim(), CultureInfo.GetCultureInfo("en-GB"));
            ToDate = DateTime.Parse(Txt_To_Date.Text.Trim(), CultureInfo.GetCultureInfo("en-GB"));


            string spname = "";
            DataTable dtIOM = null;
            DataTable dt = null;

            Hashtable HashtableIOM = new Hashtable();


            string OpStockQry = "select isnull(openingstock,0) 'OpeningStock' from invstockitemlist isil where  ItemId='" + ddlItemID.SelectedValue + "' ";
            DataTable dtstck = config.ExecuteAdaptorAsyncWithQueryParams(OpStockQry).Result;

            float openingstock = 0;
            if (dtstck.Rows.Count > 0)
            {
                openingstock = float.Parse(dtstck.Rows[0]["OpeningStock"].ToString());
            }

            txtOpeningStock.Text = openingstock.ToString();

            string qry = "((select distinct inflowdate as date from InvInflowMaster im where ItemId='" + ddlItemID.SelectedValue + "' " +
                       " and inflowdate between '" + FromDate + "' and '" + ToDate + "'  group by inflowdate having sum(DeliveredQty)>0))  " +
                        " union  " +
                       " (select distinct date as date from InvOutflowMaster im where  ItemId='" + ddlItemID.SelectedValue + "' " +
                       " and date between '" + FromDate + "' and '" + ToDate + "' group by DATE having sum(qty)>0)  ";

            dt = config.ExecuteAdaptorAsyncWithQueryParams(qry).Result;

            float TotalInflows = 0, TotalOutflows = 0;


            if (dt.Rows.Count > 0)
            {
                GVPODetails.DataSource = dt;
                GVPODetails.DataBind();
                lbtn_Export.Visible = true;
                foreach (GridViewRow gvrow in GVPODetails.Rows)
                {
                    Label lblItemid = (Label)gvrow.FindControl("lblItemid");
                    Label lbldate = (Label)gvrow.FindControl("lbldateold");
                    Label lblItemname = (Label)gvrow.FindControl("lblItemname");
                    Label lblInflows = (Label)gvrow.FindControl("lblInflows");
                    lblInflows.Text = "0";
                    Label lblOutflows = (Label)gvrow.FindControl("lblOutflows");
                    lblOutflows.Text = "0";

                    qry = " select Itemid,itemname," +
                       " (isnull(openingstock,0)+(select isnull(SUM(DeliveredQty),0) from  " +
                        " InvInflowMaster where inflowdate< '" + lbldate.Text + "' and InvInflowMaster.ItemId=isil.ItemId) " +
                         "-(select isnull(SUM(qty),0) from InvOutflowMaster where date < '" + lbldate.Text + "'" +
                        " and InvOutflowMaster.ItemId=isil.ItemId)) as 'OpeningStock', " +
                    "'" + lbldate.Text + "' as date,(select isnull(round(sum(DeliveredQty),2),0) from invinflowmaster im where im.ItemId=isil.ItemId " +
                       " and inflowdate = '" + lbldate.Text + "' and  ItemId='" + ddlItemID.SelectedValue + "' ) Inflows, " +
                      " (select isnull(round(sum(qty),2),0) from InvOutflowMaster om where om.ItemId=isil.ItemId and date = '" + lbldate.Text + "' and  ItemId='" + ddlItemID.SelectedValue + "') " +
                      "  Outflows from invstockitemlist isil where ItemId='" + ddlItemID.SelectedValue + "' ";

                    DataTable DtOM = config.ExecuteAdaptorAsyncWithQueryParams(qry).Result;


                    if (DtOM.Rows.Count > 0)
                    {
                        lblItemid.Text = DtOM.Rows[0]["itemid"].ToString();
                        lblItemname.Text = DtOM.Rows[0]["itemname"].ToString();
                        lblInflows.Text = float.Parse(DtOM.Rows[0]["Inflows"].ToString()).ToString();
                        TotalInflows += (float.Parse(lblInflows.Text));
                        lblOutflows.Text = float.Parse(DtOM.Rows[0]["Outflows"].ToString()).ToString();
                        TotalOutflows += (float.Parse(lblOutflows.Text));
                    }

                    Label GlblInflows = GVPODetails.FooterRow.FindControl("GlblInflows") as Label;
                    GlblInflows.Text = Math.Round(TotalInflows).ToString();

                    Label GlblOutflows = GVPODetails.FooterRow.FindControl("GlblOutflows") as Label;
                    GlblOutflows.Text = Math.Round(TotalOutflows).ToString();


                }
            }
            else
            {
                GVPODetails.DataSource = null;
                GVPODetails.DataBind();
                lbtn_Export.Visible = false;

            }



        }

        protected void GetItemID()
        {


            string sqlqry = "select ItemId from InvStockItemList where ItemId =  '" + ddlItemname.SelectedValue + "'  ";

            DataTable dt = config.ExecuteAdaptorAsyncWithQueryParams(sqlqry).Result;
            if (dt.Rows.Count > 0)
            {
                try
                {
                    ddlItemID.SelectedValue = dt.Rows[0]["ItemId"].ToString();

                }
                catch (Exception ex)
                {
                    // MessageLabel.Text = ex.Message;
                }
            }
            else
            {
                ddlItemID.SelectedIndex = 0;
            }

        }

        protected void ddlItemID_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            GetItemName();
            //loadItemIDs();
            //loaditemNames();

        }
        protected void ddlItemname_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            GetItemID();
            //loaditemNames();

        }

        protected void GetItemName()
        {
            string sqlqry = "Select ItemName ,itemid from InvStockItemList where ItemID = '" + ddlItemID.SelectedValue + "'  ";
            DataTable dt = config.ExecuteAdaptorAsyncWithQueryParams(sqlqry).Result;
            if (dt.Rows.Count > 0)
            {
                try
                {
                    ddlItemname.SelectedValue = dt.Rows[0]["itemid"].ToString();

                }
                catch (Exception ex)
                {
                    // MessageLabel.Text = ex.Message;
                }
            }
            else
            {
                ddlItemname.SelectedIndex = 0;
            }


        }

        protected void lbtn_Export_Click(object sender, EventArgs e)
        {
            gve.ExportGrid("ItemWiseReportReport.xls", hidGridView);

        }

        protected void ClearData()
        {

            GVPODetails.DataSource = null;
            GVPODetails.DataBind();
        }

        public string GetMonthName()
        {
            string monthname = string.Empty;
            int payMonth = 0;
            DateTimeFormatInfo mfi = new DateTimeFormatInfo();


            //DateTime date = Convert.ToDateTime(ddlPONo.SelectedValue, CultureInfo.GetCultureInfo("en-gb"));
            //monthname = mfi.GetMonthName(date.Month).ToString();
            //payMonth = GetMonth(monthname);

            return monthname;
        }

        protected void GVPODetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {


        }
    }
}