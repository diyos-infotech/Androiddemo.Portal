using System;
using System.Collections;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using KLTS.Data;
using System.Globalization;
using Jawan.Portal.DAL;

namespace Jawan.Portal
{
    public partial class AddHolidaysList : System.Web.UI.Page
    {
        GridViewExportUtil GVUtil = new GridViewExportUtil();
        AppConfiguration config = new AppConfiguration();

        string EmpIDPrefix = string.Empty;
        string CmpIDPrefix = string.Empty;
        string UserName = string.Empty;
        string BranchID = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
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

                    GetHolidayZones();
                    //GetHolidaysList();
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void GetWebConfigdata()
        {
            EmpIDPrefix = Session["EmpIDPrefix"].ToString();
            CmpIDPrefix = Session["CmpIDPrefix"].ToString();
            UserName = Session["UserId"].ToString();
            BranchID = Session["HomeBranch"].ToString();
        }

        protected void LoadBranches()
        {
            //string querybranch = "select * from branchdetails order by branchid";
            //DataTable dtbranch = config.ExecuteAdaptorAsyncWithQueryParams(querybranch).Result;
            //if (dtbranch.Rows.Count > 0)
            //{
            //    ddlBranch.DataValueField = "branchid";
            //    ddlBranch.DataTextField = "branchname";
            //    ddlBranch.DataSource = dtbranch;
            //    ddlBranch.DataBind();
            //}
            //ddlBranch.Items.Insert(0, "--Select--");
            //ddlBranch.Items.Insert(1, "ALL");
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
            // ddlHolidayZones.Items.Insert(1, "All");
        }

        private void GetHolidaysList()
        {
            var ZoneID = ddlHolidayZones.SelectedValue;
            //var year = "01/01/1900";

            DataTable DtHolidayList = GlobalData.Instance.LoadHolidayList(ZoneID);
            if (DtHolidayList.Rows.Count > 0)
            {
                GvHolidayList.DataSource = DtHolidayList;
                GvHolidayList.Visible = true;
                GvHolidayList.DataBind();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Holiday List Not Avialable');", true);
                GvHolidayList.Visible = false;
                return;
            }
        }

        protected void GvHolidayList_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GvHolidayList.EditIndex = e.NewEditIndex;
            GetHolidaysList();
        }

        protected void GvHolidayList_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GvHolidayList.EditIndex = -1;
            GetHolidaysList();
        }

        protected void GvHolidayList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GvHolidayList.PageIndex = e.NewPageIndex;
            GvHolidayList.DataBind();
        }

        protected void GvHolidayList_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                Label lblId = GvHolidayList.Rows[e.RowIndex].FindControl("lblId") as Label;
                Label lblZoneID = GvHolidayList.Rows[e.RowIndex].FindControl("lblZoneID") as Label;
                TextBox txtDate = GvHolidayList.Rows[e.RowIndex].FindControl("txtDate") as TextBox;
                TextBox txtNameOfHoliday = GvHolidayList.Rows[e.RowIndex].FindControl("txtNameOfHoliday") as TextBox;

                string HolidayDate = "01/01/1900";
                if (txtDate.Text.Trim().Length > 0)
                {
                    HolidayDate = DateTime.Parse(txtDate.Text.Trim(), CultureInfo.GetCultureInfo("en-gb")).ToString("yyyy/MM/dd");
                }


                var Deptname = string.Empty;
                var ProcedureName = string.Empty;
                var IRecordStatus = 0;
                var ZoneId = ddlHolidayZones.SelectedValue;

                if (lblZoneID.Text.Trim().Length > 0 && txtNameOfHoliday.Text.Trim().Length != 0 && txtDate.Text.Trim().Length != 0)
                {
                    var SP_CheckDuplicates = string.Empty;
                    Hashtable HtGetHolidayListData = new Hashtable();

                    SP_CheckDuplicates = "CheckDuplicate_HolidaysList";
                    HtGetHolidayListData.Add("@ZoneID", lblZoneID.Text);
                    HtGetHolidayListData.Add("@Date", HolidayDate);
                    HtGetHolidayListData.Add("@HolidayText", txtNameOfHoliday.Text);

                    DataTable Dt_HolidayList = config.ExecuteAdaptorAsyncWithParams(SP_CheckDuplicates, HtGetHolidayListData).Result;
                    if (Dt_HolidayList.Rows.Count > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Holiday Alredy Exist.');", true);
                        return;
                    }
                    else
                    {
                        var Update_On = DateTime.Now;
                        var Update_By = UserName;

                        Hashtable HtSPParameters = new Hashtable();
                        ProcedureName = "AddUpd_HolidaysList";

                        HtSPParameters.Add("@Id", lblId.Text);
                        HtSPParameters.Add("@Date", HolidayDate);
                        HtSPParameters.Add("@HolidayText", txtNameOfHoliday.Text);
                        HtSPParameters.Add("@UpdatedOn", Update_On);
                        HtSPParameters.Add("@UpdatedBy", Update_By);
                        //HtSPParameters.Add("@ZoneID", ZoneId);

                        IRecordStatus = config.ExecuteNonQueryParamsAsync(ProcedureName, HtSPParameters).Result;

                        if (IRecordStatus > 0)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Holiday  Updated  SucessFully.');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Contact Admin.');", true);
                        }

                        GvHolidayList.EditIndex = -1;
                        GetHolidaysList();
                    }
                }
            }

            catch (Exception ex)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Error Occured..');", true);
                return;
            }
        }

        protected void Btn_AddHolidayList_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlHolidayZones.SelectedIndex == 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showlalert", "alert('Please Select Zone');", true);
                    return;
                }
                if (txtmonth.Text.Trim().Length == 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showlalert", "alert('Please Select Month');", true);
                    return;
                }
                if (txtHolidayName.Text.Trim().Length == 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showlalert", "alert('Please Enter Holiday Text');", true);
                    return;
                }

                string HolidayDate = "01/01/1900";

                if (txtmonth.Text.Trim().Length > 0)
                {
                    HolidayDate = DateTime.Parse(txtmonth.Text.Trim(), CultureInfo.GetCultureInfo("en-gb")).ToString("yyyy/MM/dd");
                }

                var Created_On = DateTime.Now;
                var Created_By = UserName;

                var HolidayText = string.Empty;
                var ZoneId = string.Empty;
                var IRecordStatus = 0;

                HolidayText = txtHolidayName.Text;

                //if (ddlHolidayZones.SelectedIndex==1)
                //{
                //    ZoneId = "%";
                //}
                //else
                //{
                ZoneId = ddlHolidayZones.SelectedValue;
                //}

                if (ddlHolidayZones.SelectedIndex > 0 && txtHolidayName.Text.Trim().Length != 0 && txtmonth.Text.Trim().Length != 0)
                {
                    var SP_CheckDuplicates = string.Empty;
                    Hashtable HtGetHolidayListData = new Hashtable();
                    SP_CheckDuplicates = "CheckDuplicate_HolidaysList";
                    HtGetHolidayListData.Add("@ZoneID", ZoneId);
                    HtGetHolidayListData.Add("@Date", HolidayDate);
                    HtGetHolidayListData.Add("@HolidayText", txtHolidayName.Text);

                    DataTable Dt_HolidayList = config.ExecuteAdaptorAsyncWithParams(SP_CheckDuplicates, HtGetHolidayListData).Result;
                    if (Dt_HolidayList.Rows.Count > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Holiday Alredy Exist.');", true);
                        return;
                    }
                    else
                    {
                        Hashtable HtSPParameters = new Hashtable();
                        var ProcedureName = "AddUpd_HolidaysList";

                        HtSPParameters.Add("@Id", 0);
                        HtSPParameters.Add("@Date", HolidayDate);
                        HtSPParameters.Add("@HolidayText", HolidayText);
                        HtSPParameters.Add("@ZoneID", ZoneId);
                        HtSPParameters.Add("@CreatedOn", Created_On);
                        HtSPParameters.Add("@CreatedBy", Created_By);

                        IRecordStatus = config.ExecuteNonQueryParamsAsync(ProcedureName, HtSPParameters).Result;

                        if (IRecordStatus > 0)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Holiday Added SucessFully.');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Contact to Admin..');", true);
                        }

                        GetHolidaysList();
                        Clear();
                    }
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Error Occured..');", true);
                return;
            }
        }

        protected void Clear()
        {
            ddlHolidayZones.SelectedIndex = 0;
            txtmonth.Text = string.Empty;
            txtHolidayName.Text = string.Empty;
        }

        protected void ddlHolidayZones_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetHolidaysList();
        }
    }
}