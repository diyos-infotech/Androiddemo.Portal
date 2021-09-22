using System;
using System.Collections;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using KLTS.Data;
using System.Web.UI.HtmlControls;
using System.Globalization;
using Jawan.Portal.DAL;

namespace Jawan.Portal
{
    public partial class LeaveHolidayListReport : System.Web.UI.Page
    {
        AppConfiguration Config = new AppConfiguration();
        GridViewExportUtil gve = new GridViewExportUtil();
        string CmpIDPrefix = "";
        string BranchID = "";
        string UserName = "";
        string EmpIDPrefix = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            CmpIDPrefix = Session["CmpIDPrefix"].ToString();
            BranchID = Session["HomeBranch"].ToString();
            UserName = Session["UserId"].ToString();

            if (!IsPostBack)
            {
                if (Session["UserId"] != null && Session["AccessLevel"] != null)
                {

                }
                else
                {
                    Response.Redirect("login.aspx");
                }

                if (this.Master != null)
                {
                    HtmlControl emplink = (HtmlControl)this.Master.Master.FindControl("ContentPlaceHolder1").FindControl("sli1");
                    if (emplink != null)
                    {
                        emplink.Attributes["class"] = "current";
                    }
                }

                GetHolidayZones();


                // ddlDateType_SelectedIndexChanged(sender, e);
            }
        }

        protected void GetWebConfigdata()
        {
            EmpIDPrefix = Session["EmpIDPrefix"].ToString();
            //CmpIDPrefix = Session["CmpIDPrefix"].ToString();
            //BranchID = Session["BranchID"].ToString();
            //Username = Session["UserId"].ToString();
        }

        private void GetHolidayZones()
        {
            DataTable DtHolidayZones = GlobalData.Instance.LoadHolidayZones();
            if (DtHolidayZones.Rows.Count > 0)
            {
                ddlHolidayZones.DataValueField = "ZoneId";
                ddlHolidayZones.DataTextField = "Zone";
                ddlHolidayZones.DataSource = DtHolidayZones;
                ddlHolidayZones.DataBind();
            }
            ddlHolidayZones.Items.Insert(0, "--Select--");
            ddlHolidayZones.Items.Insert(1, "All");
        }

        protected void Btn_Search_Click(object sender, EventArgs e)
        {
            try
            {
                GvHolidayList.DataSource = null;
                GvHolidayList.DataBind();

                string EmpId = string.Empty;

                //DataTable dts = GlobalData.Instance.LoadBranchOnUserID(BranchID);

                if (ddlHolidayZones.SelectedIndex == 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Please Select Zone');", true);
                    return;
                }

                var Zone = string.Empty;

                if (ddlHolidayZones.SelectedIndex == 1)
                {
                    Zone = "%";
                }
                else
                {
                    Zone = ddlHolidayZones.SelectedValue;
                }

                string SP_HolidayList = "";
                Hashtable HtLHolidayList = new Hashtable();

                SP_HolidayList = "GetHolidaysListReport";
                //HtLHolidayList.Add("@EmpId", EmpId);
                HtLHolidayList.Add("@ZoneID", Zone);

                DataTable DtLHL = Config.ExecuteAdaptorAsyncWithParams(SP_HolidayList, HtLHolidayList).Result;

                if (DtLHL.Rows.Count > 0)
                {
                    GvHolidayList.DataSource = DtLHL;
                    GvHolidayList.DataBind();
                    lbtn_Export.Visible = true;
                }
                else
                {
                    lbtn_Export.Visible = false;
                    GvHolidayList.DataSource = null;
                    GvHolidayList.DataBind();
                    ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('No Data Available for Selected Employee');", true);
                    return;
                }

            }
            catch (Exception Ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Please Contact Admin');", true);
                return;
            }
        }

        protected void lbtn_Export_Click(object sender, EventArgs e)
        {
            gve.Export("HolidayList.xls", this.GvHolidayList);
        }

        protected void ddlHolidayZones_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlHolidayZones.SelectedIndex == 0)
            {
                GvHolidayList.DataSource = null;
                GvHolidayList.DataBind();
            }
        }
    }
}