using System;
using System.Web.UI;
using KLTS.Data;
using System.Data;
using System.Globalization;
using Jawan.Portal.DAL;
using System.Collections;
using System.Web.UI.WebControls;
namespace Jawan.Portal
{
    public partial class LeadsReport : System.Web.UI.Page
    {
        AppConfiguration config = new AppConfiguration();
        GridViewExportUtil gve = new GridViewExportUtil();
        string EmpIDPrefix = "";
        string CmpIDPrefix = "";
        string Elength = "";
        string Clength = "";
        string Fontstyle = "";
        string CFontstyle = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (!IsPostBack)
                {
                    if (Session["UserId"] != null && Session["AccessLevel"] != null)
                    {
                        lblDisplayUser.Text = Session["UserId"].ToString();
                        // PreviligeUsers(Convert.ToInt32(Session["AccessLevel"]));
                        lblcname.Text = SqlHelper.Instance.GetCompanyname();
                    }
                    else
                    {
                        Response.Redirect("login.aspx");
                    }


                    var formatInfoinfo = new DateTimeFormatInfo();
                    string[] monthName = formatInfoinfo.MonthNames;
                    string month = monthName[DateTime.Now.Month - 1];

                    LoadStatus();
                }
               
            }
            catch (Exception ex)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "Show alert", "alert('Your Session Expired');", true);
                Response.Redirect("~/Login.aspx");
            }
        }

        protected void LoadStatus()
        {
            string queryStatus = "select * from M_LeadStatusMaster";
            DataTable Dtstatus = SqlHelper.Instance.GetTableByQuery(queryStatus);
            if (Dtstatus.Rows.Count > 0)
            {
                ddlStatus.DataValueField = "Id";
                ddlStatus.DataTextField = "LeadStatus";
                ddlStatus.DataSource = Dtstatus;
                ddlStatus.DataBind();
            }
            ddlStatus.Items.Insert(0,"-Select-");
            ddlStatus.Items.Insert(1,"ALL");
        }



        protected void btndownload_Click(object sender, EventArgs e)
        {

            var FromDate = DateTime.Now;
            var ToDate = DateTime.Now;

            if (txtfrom.Text.Trim().Length > 0)
            {
                FromDate = DateTime.Parse(txtfrom.Text.Trim(), CultureInfo.GetCultureInfo("en-GB"));
            }

            if (txtto.Text.Trim().Length > 0)
            {
                ToDate = DateTime.Parse(txtto.Text.Trim(), CultureInfo.GetCultureInfo("en-GB"));
            }


            var LeadStatus = "";
            var SPName = "LeadsReport";
            if (ddlStatus.SelectedIndex == 1)
            {
                LeadStatus = "%";
            }
            else
            {
                LeadStatus = ddlStatus.SelectedValue;
            }
            Hashtable HtSPParameters = new Hashtable();
            HtSPParameters.Add("@LeadStatus", LeadStatus);
            HtSPParameters.Add("@FromDate", FromDate);
            HtSPParameters.Add("@ToDate", ToDate);
            DataTable dt = SqlHelper.Instance.ExecuteStoredProcedureWithParams(SPName, HtSPParameters);
            if (dt.Rows.Count > 0)
            {
                gvattendancezero.DataSource = dt;
                gvattendancezero.DataBind();

            }
            else
            {
                gvattendancezero.DataSource = null;
                gvattendancezero.DataBind();
            }
        }
        protected void lbtn_Export_Click(object sender, EventArgs e)
        {
            gve.Export("LeadsReport.xls", this.gvattendancezero);
        }
    }
}