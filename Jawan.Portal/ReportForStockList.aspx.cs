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
using Jawan.Portal.DAL;
namespace Jawan.Portal
{
    public partial class ReportForStockList : System.Web.UI.Page
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
                GetData();

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

        protected void GetData()
        {
            string sqlqry = "Select ItemId,ItemName,(isnull(openingstock,0)+ActualQuantity) as ActualQuantity from InvStockItemList  ";
            DataTable dt = config.ExecuteAdaptorAsyncWithQueryParams(sqlqry).Result;
            if (dt.Rows.Count > 0)
            {
                GVPODetails.DataSource = dt;
                GVPODetails.DataBind();
            }
            else
            {
                GVPODetails.DataSource = null;
                GVPODetails.DataBind();
                ScriptManager.RegisterStartupScript(this, GetType(), "showlalert", "alert('There Is No Details For This Client');", true);

            }
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

        protected void lbtn_Export_Click(object sender, EventArgs e)
        {
            gve.Export("StockinHand.xls", this.GVPODetails);
        }

        float totalGrandTotal = 0;

        protected void GVPODetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {



                float lbltotal = float.Parse(((Label)e.Row.FindControl("lblStock")).Text);
                totalGrandTotal += lbltotal;
            }

            if (e.Row.RowType == DataControlRowType.Footer)
            {

                ((Label)e.Row.FindControl("lbltotalStock")).Text = totalGrandTotal.ToString();

            }
        }
    }
}