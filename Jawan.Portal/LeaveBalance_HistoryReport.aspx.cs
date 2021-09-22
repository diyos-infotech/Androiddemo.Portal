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
    public partial class LeaveBalance_HistoryReport : System.Web.UI.Page
    {
        AppConfiguration Config = new AppConfiguration();
        GridViewExportUtil gve = new GridViewExportUtil();
        string CmpIDPrefix = "";
        string BranchID = "";
        string UserName = "";
        string EmpIDPrefix = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            GetWebConfigdata();

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
                LoadEmpIds();
                LoadNames();
                FinancialYears();
            }
        }

        protected void GetWebConfigdata()
        {
            EmpIDPrefix = Session["EmpIDPrefix"].ToString();
            CmpIDPrefix = Session["CmpIDPrefix"].ToString();
            BranchID = Session["HomeBranch"].ToString();
            UserName = Session["UserId"].ToString();
        }

        public void FinancialYears()
        {
            DataTable DtFinancialYears = GlobalData.Instance.LoadYears();

            if (DtFinancialYears.Rows.Count > 0)
            {
                ddlyears.DataValueField = "FinancialYears";
                ddlyears.DataTextField = "FinancialYears";
                ddlyears.DataSource = DtFinancialYears;
                ddlyears.DataBind();
            }
            ddlyears.Items.Insert(0, "--Select--");
        }

        protected void LoadEmpIds()
        {
            int type = ddlReportType.SelectedIndex;
            DataTable dtBranch = GlobalData.Instance.LoadBranchOnUserID(BranchID);

            DataTable DtEmpIds = GlobalData.Instance.Load_LeaveEmpNames(EmpIDPrefix, dtBranch, type);
            if (DtEmpIds.Rows.Count > 0)
            {
                ddlEmpId.DataValueField = "EmpId";
                ddlEmpId.DataTextField = "EmpId";
                ddlEmpId.DataSource = DtEmpIds;
                ddlEmpId.DataBind();
            }
            ddlEmpId.Items.Insert(0, "-Select-");
            ddlEmpId.Items.Insert(1, "All");
        }

        protected void LoadNames()
        {
            int type = ddlReportType.SelectedIndex;
            DataTable dtBranch = GlobalData.Instance.LoadBranchOnUserID(BranchID);

            DataTable DtEmpNames = GlobalData.Instance.Load_LeaveEmpNames(EmpIDPrefix, dtBranch, type);
            if (DtEmpNames.Rows.Count > 0)
            {
                ddlEmpName.DataValueField = "EmpId";
                ddlEmpName.DataTextField = "FullName";
                ddlEmpName.DataSource = DtEmpNames;
                ddlEmpName.DataBind();
            }
            ddlEmpName.Items.Insert(0, "-Select-");
            ddlEmpName.Items.Insert(1, "All");
        }

        protected void ddlEmpId_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlEmpId.SelectedIndex > 0)
            {
                ddlEmpName.SelectedValue = ddlEmpId.SelectedValue;
            }
            else
            {
                ddlEmpName.SelectedIndex = 0;
            }

        }

        protected void ddlEmpName_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddlEmpName.SelectedIndex > 0)
            {
                ddlEmpId.SelectedValue = ddlEmpName.SelectedValue;
            }
            else
            {
                ddlEmpId.SelectedIndex = 0;
            }

        }

        protected void ClearEmpId_EmpName()
        {
            ddlEmpId.SelectedIndex = 0;
            ddlEmpName.SelectedIndex = 0;
        }

        protected void Btn_Search_Click(object sender, EventArgs e)
        {
            try
            {
                GvLeaveBalance.DataSource = null;
                GvLeaveBalance.DataBind();
                GvLeaveHistory.DataSource = null;
                GvLeaveHistory.DataBind();

                string FromDate = string.Empty;
                string ToDate = string.Empty;
                string EmpId = string.Empty;


                if (ddlReportType.SelectedIndex == 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Please Select The Report');", true);
                    return;
                }
                if (ddlEmpId.SelectedIndex == 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Please Select Employee ID');", true);
                    return;
                }

                if (ddlEmpId.SelectedIndex == 1)
                {
                    EmpId = "%";
                }
                else
                {
                    EmpId = ddlEmpId.SelectedValue;
                }

                if (ddlReportType.SelectedIndex == 1)
                {
                    if (ddlyears.SelectedIndex == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Please Select Year');", true);
                        return;
                    }

                    string SP_LeaveBalance = "";
                    Hashtable HtLBalance = new Hashtable();

                    SP_LeaveBalance = "GetLeaveBalanceReport";
                    HtLBalance.Add("@EmpId", EmpId);
                    HtLBalance.Add("@year", ddlyears.SelectedValue);
                    HtLBalance.Add("@BranchID", BranchID);

                    DataTable DtLBalance = Config.ExecuteAdaptorAsyncWithParams(SP_LeaveBalance, HtLBalance).Result;

                    if (DtLBalance.Rows.Count > 0)
                    {
                        GvLeaveBalance.DataSource = DtLBalance;
                        GvLeaveBalance.DataBind();
                        GvLeaveBalance.Visible = true;
                        lbtn_Export.Visible = true;
                    }
                    else
                    {
                        lbtn_Export.Visible = false;
                        GvLeaveBalance.Visible = false;
                        GvLeaveBalance.DataSource = null;
                        GvLeaveBalance.DataBind();
                        ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('No Data Available for Selected Employee');", true);
                        return;
                    }

                }
                else
                {
                    if (txtFromDate.Text.Trim().Length == 0 || txtToDate.Text.Trim().Length == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Please Select From Month and To Month');", true);
                        return;
                    }

                    if (txtFromDate.Text.Trim().Length > 0)
                    {
                        FromDate = DateTime.Parse(txtFromDate.Text.Trim(), CultureInfo.GetCultureInfo("en-gb")).ToString();
                    }
                    if (txtToDate.Text.Trim().Length > 0)
                    {
                        ToDate = DateTime.Parse(txtToDate.Text.Trim(), CultureInfo.GetCultureInfo("en-gb")).ToString();
                    }

                    string SP_LeaveHistory = "";
                    Hashtable HtLHistory = new Hashtable();

                    SP_LeaveHistory = "GetLeaveHistoryReport";
                    HtLHistory.Add("@EmpId", EmpId);
                    HtLHistory.Add("@FromDate", FromDate);
                    HtLHistory.Add("@ToDate", ToDate);
                    HtLHistory.Add("@BranchID", BranchID);

                    DataTable DtLHistory = Config.ExecuteAdaptorAsyncWithParams(SP_LeaveHistory, HtLHistory).Result;

                    if (DtLHistory.Rows.Count > 0)
                    {
                        GvLeaveHistory.DataSource = DtLHistory;
                        GvLeaveHistory.DataBind();
                        GvLeaveHistory.Visible = true;
                        lbtn_Export.Visible = true;
                    }
                    else
                    {
                        GvLeaveHistory.Visible = false;
                        lbtn_Export.Visible = false;
                        GvLeaveHistory.DataSource = null;
                        GvLeaveHistory.DataBind();
                        ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('No Data Available for Selected Employee');", true);
                        return;
                    }

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
            if (ddlReportType.SelectedIndex == 1)
            {
                gve.Export("LeaveBalance.xls", this.GvLeaveBalance);
            }
            else
            {
                gve.Export("LeaveHistory.xls", this.GvLeaveHistory);
            }
        }

        protected void ddlReportType_SelectedIndexChanged(object sender, EventArgs e)
        {

            ddlEmpId.Items.Clear();
            ddlEmpName.Items.Clear();
            LoadEmpIds();
            LoadNames();

            GvLeaveBalance.DataSource = null;
            GvLeaveBalance.DataBind();
            GvLeaveHistory.DataSource = null;
            GvLeaveHistory.DataBind();

            if (ddlReportType.SelectedIndex == 0)
            {
                ddlEmpId.SelectedIndex = 0;
                ddlEmpName.SelectedIndex = 0;
                ddlDateType.SelectedIndex = 0;
                ddlyears.SelectedIndex = 0;
                txtFromDate.Text = string.Empty;
                txtToDate.Text = string.Empty;
                ddlDateType.Visible = false;
                lblDateType.Visible = false;

                lblDateType.Visible = false;
                ddlDateType.Visible = false;
                lblYear.Visible = false;
                ddlyears.Visible = false;
                lblFromDate.Visible = false;
                txtFromDate.Visible = false;
                lblToDate.Visible = false;
                txtToDate.Visible = false;

                lbtn_Export.Visible = false;

            }
            else if (ddlReportType.SelectedIndex == 1)
            {
                ddlDateType.SelectedIndex = 0;
                ddlyears.SelectedIndex = 0;
                lblDateType.Visible = false;
                ddlDateType.Visible = false;
                lblYear.Visible = true;
                ddlyears.Visible = true;
                lblFromDate.Visible = false;
                txtFromDate.Visible = false;
                lblToDate.Visible = false;
                txtToDate.Visible = false;
            }
            else
            {
                ddlDateType.SelectedIndex = 1;
                txtFromDate.Text = string.Empty;
                txtToDate.Text = string.Empty;
                lblDateType.Visible = false;
                ddlDateType.Visible = false;
                lblYear.Visible = false;
                ddlyears.Visible = false;
                lblFromDate.Visible = true;
                txtFromDate.Visible = true;
                lblToDate.Visible = true;
                txtToDate.Visible = true;
            }


        }

        protected void ddlDateType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}