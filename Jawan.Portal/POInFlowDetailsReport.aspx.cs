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
using System.Globalization;
using System.Text;
using Jawan.Portal.DAL;

namespace Jawan.Portal
{
    public partial class POInFlowDetailsReport : System.Web.UI.Page
    {
        AppConfiguration config = new AppConfiguration();
        GridViewExportUtil gve = new GridViewExportUtil();
        DataTable dt;
        string GRVPrefix = "";
        string EmpIDPrefix = "";
        string CmpIDPrefix = "";
        string Elength = "";
        string Clength = "";

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
                    CompanyInfoLink.Visible = false;
                    ReportsLink.Visible = true;
                    SettingsLink.Visible = false;
                    InventoryLink.Visible = true;
                    ClientsLink.Visible = false;

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
                default:
                    break;
            }
        }

        protected void GetWebConfigdata()
        {
            if (Session.Keys.Count > 0)
            {
                EmpIDPrefix = Session["EmpIDPrefix"].ToString();
                CmpIDPrefix = Session["CmpIDPrefix"].ToString();
                GRVPrefix = Session["GRVPrefix"].ToString();
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }


        protected void lbtn_Export_Click(object sender, EventArgs e)
        {
            gve.Export("PODetailsReport.xls", this.GVPOInFlowDetails);
        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {

            GVPOInFlowDetails.DataSource = null;
            GVPOInFlowDetails.DataBind();
            lbtn_Export.Visible = false;

            if (txtFromDate.Text.Trim().Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showlalert", "alert('Please Select From Date');", true);
                return;
            }

            if (txtFromDate.Text.Trim().Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showlalert", "alert('Please Select From Date');", true);
                return;
            }

            string date = string.Empty;
            string Month = string.Empty;
            string clientid = string.Empty;
            string FromDate = "";
            string ToDate = "";

            if (txtFromDate.Text.Trim().Length > 0)
            {
                FromDate = DateTime.Parse(txtFromDate.Text.Trim(), CultureInfo.GetCultureInfo("en-gb")).ToString();
            }

            if (txtToDate.Text.Trim().Length > 0)
            {
                ToDate = DateTime.Parse(txtToDate.Text.Trim(), CultureInfo.GetCultureInfo("en-gb")).ToString();
            }

            string spname = "";
            DataTable dtIOM = null;
            Hashtable HashtableIOM = new Hashtable();

            spname = "POInFlowDetailsReport";

            HashtableIOM.Add("@fromdate", FromDate);
            HashtableIOM.Add("@todate", ToDate);


            dtIOM = config.ExecuteAdaptorAsyncWithParams(spname, HashtableIOM).Result;
            if (dtIOM.Rows.Count > 0)
            {
                lbtn_Export.Visible = true;
                GVPOInFlowDetails.DataSource = dtIOM;
                GVPOInFlowDetails.DataBind();
            }
            else
            {
                lbtn_Export.Visible = false;
                GVPOInFlowDetails.DataSource = null;
                GVPOInFlowDetails.DataBind();
                ScriptManager.RegisterStartupScript(this, GetType(), "showlalert", "alert('There Is No Details For This Client');", true);
            }
        }

        protected void ClearData()
        {

            GVPOInFlowDetails.DataSource = null;
            GVPOInFlowDetails.DataBind();
        }

        public string GetMonthName()
        {
            string monthname = string.Empty;
            int payMonth = 0;
            DateTimeFormatInfo mfi = new DateTimeFormatInfo();
            return monthname;
        }

        float totalQty = 0;
        float totalBuyingpriceUnit = 0;
        float totalBuyingprice = 0;
        float totalVat5Per = 0;
        float totalVat14Per = 0;
        float totalGrandTotal = 0;

        float totalinflowamt = 0;
        float totalvat5 = 0;
        float totalvat14 = 0;
        float totalinflowGrandTotal = 0;


        protected void GVPOInFlowDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                float lblbuyingpriceunit = float.Parse(((Label)e.Row.FindControl("lbltotalprice")).Text);
                totalBuyingpriceUnit += lblbuyingpriceunit;
                float lblvat5 = float.Parse(((Label)e.Row.FindControl("lblvat5")).Text);
                totalVat5Per += lblvat5;
                float lblvat14 = float.Parse(((Label)e.Row.FindControl("lblvat14")).Text);
                totalVat14Per += lblvat14;
                float lbltotal = float.Parse(((Label)e.Row.FindControl("lbltotal")).Text);
                totalGrandTotal += lbltotal;
                float lbltotalAmt = float.Parse(((Label)e.Row.FindControl("lbltotalAmt")).Text);
                totalinflowamt += lbltotalAmt;
                float lblInflowvat5 = float.Parse(((Label)e.Row.FindControl("lblInflowvat5")).Text);
                totalvat5 += lblInflowvat5;
                float lblInflowvat14 = float.Parse(((Label)e.Row.FindControl("lblInflowvat14")).Text);
                totalvat14 += lblInflowvat14;
                float lblInflowtotal = float.Parse(((Label)e.Row.FindControl("lblInflowtotal")).Text);
                totalinflowGrandTotal += lblInflowtotal;
            }

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                ((Label)e.Row.FindControl("lblTotalBuyingPrice")).Text = totalBuyingpriceUnit.ToString();
                ((Label)e.Row.FindControl("lblTotalvat5")).Text = totalVat5Per.ToString();
                ((Label)e.Row.FindControl("lblTotalvat14")).Text = totalVat14Per.ToString();
                ((Label)e.Row.FindControl("lblTotaamount")).Text = totalGrandTotal.ToString();
                ((Label)e.Row.FindControl("lblInFlowTotalBuyingPrice")).Text = totalinflowamt.ToString();
                ((Label)e.Row.FindControl("lblInflowTotalvat5")).Text = totalvat5.ToString();
                ((Label)e.Row.FindControl("lblInflowTotalvat14")).Text = totalvat14.ToString();
                ((Label)e.Row.FindControl("lblInflowTotalamount")).Text = totalinflowGrandTotal.ToString();


            }
        }

    }
}