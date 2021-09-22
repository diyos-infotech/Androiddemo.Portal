using System;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using KLTS.Data;
using System.IO;
using System.Data.OleDb;
using Jawan.Portal.DAL;
using System.Globalization;
using System.Collections.Generic;

namespace Jawan.Portal
{
    public partial class ModifyEmpLeaveSetUp : System.Web.UI.Page
    {
        AppConfiguration Config = new AppConfiguration();

        string EmpIDPrefix = string.Empty;
        string CmpIDPrefix = string.Empty;
        string UserName = string.Empty;
        string BranchID = string.Empty;
        string LeaveTypeId = string.Empty;
        int Type = 2;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                EmpIDPrefix = Session["EmpIDPrefix"].ToString();
                CmpIDPrefix = Session["CmpIDPrefix"].ToString();
                UserName = Session["UserId"].ToString();
                BranchID = Session["HomeBranch"].ToString();

                if (!IsPostBack)
                {
                    if (Session["UserId"] != null && Session["AccessLevel"] != null)
                    {
                    }
                    else
                    {
                        Response.Redirect("login.aspx");
                    }

                    if (Request.QueryString["LeaveTypeId"] != null)
                    {
                        LeaveTypeId = Request.QueryString["LeaveTypeId"].ToString();
                    }

                    LoadBranches();
                    Get_TypeOfLeaves();
                    // Get_CombineWith();
                    Get_LeaveSetUp_Data();
                    Btn_Edit.Visible = true;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Your Session Expired.Please Login');", true);
                Response.Redirect("~/Login.aspx");
            }
        }

        protected void LoadBranches()
        {
            //DataTable dtBranch = GlobalData.Instance.LoadBranchOnUserID(BranchID);
            //DataTable DtBranches = GlobalData.Instance.LoadLoginBranch(dtBranch);
            //if (DtBranches.Rows.Count > 0)
            //{
            //    ddlBranch.DataValueField = "BranchId";
            //    ddlBranch.DataTextField = "BranchName";
            //    ddlBranch.DataSource = DtBranches;
            //    ddlBranch.DataBind();
            //}
            //ddlBranch.Items.Insert(0, new ListItem("-Select-", "0"));
        }

        protected void Get_TypeOfLeaves()
        {
            DataTable DtTypeOfleave = GlobalData.Instance.LoadTypeOfLeave(Type);
            if (DtTypeOfleave.Rows.Count > 0)
            {
                ddlTypeOfleave.DataValueField = "Id";
                ddlTypeOfleave.DataTextField = "Name";
                ddlTypeOfleave.DataSource = DtTypeOfleave;
                ddlTypeOfleave.DataBind();
            }
            ddlTypeOfleave.Items.Insert(0, "--Select--");
            // ddlHolidayZones.Items.Insert(1, "All");

            DataTable DTCombineWith = GlobalData.Instance.LoadTypeOfLeave(Type);
            if (DTCombineWith.Rows.Count > 0)
            {
                LstCombinewith.DataValueField = "Id";
                LstCombinewith.DataTextField = "Name";
                LstCombinewith.DataSource = DTCombineWith;
                LstCombinewith.DataBind();
            }
            LstCombinewith.Items.Insert(0, "--Select--");
        }

        protected void Get_LeaveSetUp_Data()
        {
            try
            {
                string SP_GetLeaveSetUp = string.Empty;

                Hashtable HtGetData = new Hashtable();
                SP_GetLeaveSetUp = "GetLeaveSetUp";
                HtGetData.Add("@leaveTypeId", LeaveTypeId);

                DataTable DtLeaveSetUp = Config.ExecuteAdaptorAsyncWithParams(SP_GetLeaveSetUp, HtGetData).Result;

                if (DtLeaveSetUp.Rows.Count > 0)
                {
                    if (String.IsNullOrEmpty(DtLeaveSetUp.Rows[0]["updateDate"].ToString()) == false)
                    {
                        txtmonth.Text = DateTime.Parse(DtLeaveSetUp.Rows[0]["updateDate"].ToString()).ToString("dd/MM/yyyy");
                        if (txtmonth.Text == "01/01/1900")
                        {
                            txtmonth.Text = "";
                        }
                    }
                    else
                    {
                        txtmonth.Text = "";
                    }

                    //txtTypeOfleave.Text = DtLeaveSetUp.Rows[0]["leaveTypeName"].ToString();
                    txtShortName.Text = DtLeaveSetUp.Rows[0]["ShortName"].ToString();
                    txtNumOfLeaves.Text = DtLeaveSetUp.Rows[0]["updateNumber"].ToString();
                    txtMaxContDays.Text = DtLeaveSetUp.Rows[0]["maxContinousDays"].ToString();
                    txtMaxCashable.Text = DtLeaveSetUp.Rows[0]["maxCashable"].ToString();
                    txtMaxRollover.Text = DtLeaveSetUp.Rows[0]["maxRollover"].ToString();
                    txtMaxValue.Text = DtLeaveSetUp.Rows[0]["maxValue"].ToString();


                    if (DtLeaveSetUp.Rows[0]["leaveTypeName"].ToString() == "0")
                    {
                        ddlTypeOfleave.SelectedIndex = 0;
                    }
                    else
                    {
                        ddlTypeOfleave.SelectedValue = DtLeaveSetUp.Rows[0]["leaveTypeName"].ToString();
                    }

                    if (DtLeaveSetUp.Rows[0]["updateFrequency"].ToString() == "0")
                    {
                        ddlFrequency.SelectedIndex = 0;
                    }
                    else
                    {
                        ddlFrequency.SelectedValue = DtLeaveSetUp.Rows[0]["updateFrequency"].ToString();
                    }

                    for (int i = 0; i < LstCombinewith.Items.Count; i++)
                    {
                        foreach (string lgknown in DtLeaveSetUp.Rows[0]["combineWith"].ToString().Split(','))
                        {
                            if (lgknown != LstCombinewith.Items[i].Value) continue;
                            LstCombinewith.Items[i].Selected = true;

                        }
                    }


                    bool HolidayCombine = false;
                    if (String.IsNullOrEmpty(DtLeaveSetUp.Rows[0]["holidayCombine"].ToString()) == false)
                    {
                        HolidayCombine = Convert.ToBoolean(DtLeaveSetUp.Rows[0]["holidayCombine"].ToString());
                    }
                    if (HolidayCombine == true)
                    {
                        rbnHolidayCombineYes.Checked = true;
                    }
                    else
                    {
                        rbnHolidayCombineNo.Checked = true;
                    }

                    bool HolidayTrailing = false;
                    if (String.IsNullOrEmpty(DtLeaveSetUp.Rows[0]["holidayTrailing"].ToString()) == false)
                    {
                        HolidayTrailing = Convert.ToBoolean(DtLeaveSetUp.Rows[0]["holidayTrailing"].ToString());
                    }
                    if (HolidayTrailing == true)
                    {
                        rbnHolidayTrailingYes.Checked = true;
                    }
                    else
                    {
                        rbnHolidayTrailingNo.Checked = true;
                    }

                    bool WOCombine = false;
                    if (String.IsNullOrEmpty(DtLeaveSetUp.Rows[0]["woCombine"].ToString()) == false)
                    {
                        WOCombine = Convert.ToBoolean(DtLeaveSetUp.Rows[0]["woCombine"].ToString());
                    }
                    if (WOCombine == true)
                    {
                        rbnWOCombineYes.Checked = true;
                    }
                    else
                    {
                        rbnWOCombineNo.Checked = true;
                    }

                }

            }
            catch (Exception Ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Show Alert", "alert('Please, Contact Admin.');", true);
                return;
            }

        }

        protected void Btn_Edit_Click(object sender, EventArgs e)
        {
            //txtTypeOfleave.Enabled = true;
            txtShortName.Enabled = false;
            txtNumOfLeaves.Enabled = true;
            txtMaxContDays.Enabled = true;
            txtMaxCashable.Enabled = true;
            txtMaxRollover.Enabled = true;
            txtMaxValue.Enabled = true;

            ddlTypeOfleave.Enabled = false;
            ddlFrequency.Enabled = true;
            //ddlCombinewith.Enabled = true;
            LstCombinewith.Enabled = true;

            rbnHolidayCombineYes.Enabled = true;
            rbnHolidayCombineNo.Enabled = true;
            rbnWOCombineYes.Enabled = true;
            rbnWOCombineNo.Enabled = true;
            rbnHolidayTrailingYes.Enabled = true;
            rbnHolidayTrailingNo.Enabled = true;
            txtmonth.Enabled = true;

            Btn_Edit.Visible = false;
            Btn_UpdateLeaveSetUp.Visible = true;
            Btn_Cancel.Visible = true;
        }

        protected void Btn_UpdateLeaveSetUp_Click(object sender, EventArgs e)
        {
            try
            {
                LeaveTypeId = Request.QueryString["LeaveTypeId"].ToString();
                string ResetDate = "01/01/1900";

                if (txtmonth.Text.Trim().Length > 0)
                {
                    ResetDate = DateTime.Parse(txtmonth.Text.Trim(), CultureInfo.GetCultureInfo("en-gb")).ToString("yyyy/MM/dd");
                }

                var UpdatedOn = DateTime.Now;
                var UpdatedBy = UserName;

                var TypeOfleave = "0";
                var ShortName = string.Empty;
                var NumOfLeaves = string.Empty;
                var MaxContDays = string.Empty;
                var MaxCashable = string.Empty;
                var MaxRollover = string.Empty;
                var MaxValue = string.Empty;
                var Frequency = "0";
                var HolidayCombine = "0";
                var WOCombine = "0";
                var HolidayTrailing = "0";
                var CombineWith = string.Empty;

                ShortName = txtShortName.Text;
                NumOfLeaves = txtNumOfLeaves.Text;
                MaxContDays = txtMaxContDays.Text;
                MaxCashable = txtMaxCashable.Text;
                MaxRollover = txtMaxRollover.Text;
                MaxValue = txtMaxValue.Text;

                if (ddlTypeOfleave.SelectedIndex == 0)
                {
                    TypeOfleave = "0";
                }
                else
                {
                    TypeOfleave = ddlTypeOfleave.SelectedValue;
                }

                var list1 = new List<string>();
                var CombinewithList = string.Empty;

                for (int i = 0; i < LstCombinewith.Items.Count; i++)
                {
                    if (LstCombinewith.Items[i].Selected == true)
                    {
                        list1.Add(LstCombinewith.Items[i].Value);
                    }
                }

                CombinewithList = string.Join(",", list1.ToArray());


                if (ddlFrequency.SelectedIndex == 0)
                {
                    Frequency = "0";
                }
                else
                {
                    Frequency = ddlFrequency.SelectedValue;
                }

                if (rbnHolidayCombineYes.Checked)
                {
                    HolidayCombine = "1";
                }
                else
                {
                    HolidayCombine = "0";
                }

                if (rbnWOCombineYes.Checked)
                {
                    WOCombine = "1";
                }
                else
                {
                    WOCombine = "0";
                }

                if (rbnHolidayTrailingYes.Checked)
                {
                    HolidayTrailing = "1";
                }
                else
                {
                    HolidayTrailing = "0";
                }

                var IRecordStatus = 0;
                string SP_ModifyLeaveSetUp = string.Empty;

                Hashtable ModifyLeaveSetUpDt = new Hashtable();
                SP_ModifyLeaveSetUp = "ModifyLeaveSetUp";

                ModifyLeaveSetUpDt.Add("@leaveTypeId", LeaveTypeId);
                ModifyLeaveSetUpDt.Add("@leaveTypeName", TypeOfleave);
                ModifyLeaveSetUpDt.Add("@ShortName", ShortName);
                ModifyLeaveSetUpDt.Add("@updateNumber", NumOfLeaves);
                ModifyLeaveSetUpDt.Add("@maxContinousDays", MaxContDays);
                ModifyLeaveSetUpDt.Add("@CombineWith", CombinewithList);
                ModifyLeaveSetUpDt.Add("@updateDate", ResetDate);
                ModifyLeaveSetUpDt.Add("@maxCashable", MaxCashable);
                ModifyLeaveSetUpDt.Add("@maxRollover", MaxRollover);
                ModifyLeaveSetUpDt.Add("@maxValue", MaxValue);
                ModifyLeaveSetUpDt.Add("@updateFrequency", Frequency);
                ModifyLeaveSetUpDt.Add("@holidayCombine", HolidayCombine);
                ModifyLeaveSetUpDt.Add("@woCombine", WOCombine);
                ModifyLeaveSetUpDt.Add("@holidayTrailing", HolidayTrailing);

                ModifyLeaveSetUpDt.Add("@UpdatedOn", UpdatedOn);
                ModifyLeaveSetUpDt.Add("@UpdatedBy", UpdatedBy);

                IRecordStatus = Config.ExecuteNonQueryParamsAsync(SP_ModifyLeaveSetUp, ModifyLeaveSetUpDt).Result;

                if (IRecordStatus > 0)
                {
                    string query = "update Leave_TypeMaster set status=1 where id='" + ddlTypeOfleave.SelectedValue + "'";
                    int status = Config.ExecuteNonQueryWithQueryAsync(query).Result;

                    ScriptManager.RegisterStartupScript(this, GetType(), "Show Alert", "alert('Leave Details are Modified Sucessfully.');", true);

                    //Get_LeaveSetUp_Data();
                    DataDisable();
                    Btn_Edit.Visible = true;
                    Btn_UpdateLeaveSetUp.Visible = false;
                    Btn_Cancel.Visible = false;
                    return;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Show Alert", "alert('Leave Details are Not Modified Sucessfull. ');", true);
                    return;
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Show Alert", "alert('Please, Contact Admin.');", true);
                return;
            }
        }

        protected void Btn_Cancel_Click(object sender, EventArgs e)
        {
            Get_LeaveSetUp_Data();
            DataDisable();
            Btn_Edit.Visible = true;
            Btn_UpdateLeaveSetUp.Visible = false;
            Btn_Cancel.Visible = false;
        }

        protected void ClearData()
        {
            ////txtTypeOfleave.Text = string.Empty;
            //txtShortName.Text = string.Empty;
            //txtNumOfLeaves.Text = string.Empty;
            //txtMaxContDays.Text = string.Empty;
            //txtMaxCashable.Text = string.Empty;
            //txtMaxRollover.Text = string.Empty;
            //txtMaxValue.Text = string.Empty;
            //txtmonth.Text = string.Empty;

            //ddlTypeOfleave.SelectedIndex = 0;
            //ddlFrequency.SelectedIndex = 0;
            ////ddlCombinewith.SelectedIndex = 0;
            ////LstCombinewith.Items.Clear();

            //rbnHolidayCombineYes.Checked = false;
            //rbnHolidayCombineNo.Checked = false;
            //rbnWOCombineYes.Checked = false;
            //rbnWOCombineNo.Checked = false;
            //rbnHolidayTrailingYes.Checked = false;
            //rbnHolidayTrailingNo.Checked = false;
        }

        protected void DataDisable()
        {
            txtShortName.Enabled = false;
            txtNumOfLeaves.Enabled = false;
            txtMaxContDays.Enabled = false;
            txtMaxCashable.Enabled = false;
            txtMaxRollover.Enabled = false;
            txtMaxValue.Enabled = false;

            ddlTypeOfleave.Enabled = false;
            ddlFrequency.Enabled = false;
            LstCombinewith.Enabled = false;

            rbnHolidayCombineYes.Enabled = false;
            rbnHolidayCombineNo.Enabled = false;
            rbnWOCombineYes.Enabled = false;
            rbnWOCombineNo.Enabled = false;
            rbnHolidayTrailingYes.Enabled = false;
            rbnHolidayTrailingNo.Enabled = false;
            txtmonth.Enabled = false;
        }

        protected void ddlTypeOfleave_SelectedIndexChanged(object sender, EventArgs e)
        {
            //DataTable DtCombinewith = GlobalData.Instance.LoadTypeOfLeave(Type);
            //if (DtCombinewith.Rows.Count > 0)
            //{
            //    LstCombinewith.DataValueField = "ID";
            //    LstCombinewith.DataTextField = "Name";
            //    LstCombinewith.DataSource = DtCombinewith;
            //    LstCombinewith.DataBind();
            //}
            //LstCombinewith.Items.Remove(ddlTypeOfleave.SelectedItem);
            ////LstCombinewith.Items.Insert(0, new ListItem("-Select-", "0"));
        }

        public void Get_CombineWith()
        {
            DataTable DtCombinewith = GlobalData.Instance.LoadTypeOfLeave(Type);
            if (DtCombinewith.Rows.Count > 0)
            {
                LstCombinewith.DataValueField = "Id";
                LstCombinewith.DataTextField = "Name";
                LstCombinewith.DataSource = DtCombinewith;
                LstCombinewith.DataBind();
            }
            //// LstCombinewith.Items.Remove(ddlTypeOfleave.SelectedItem);
            //LstCombinewith.Items.Insert(0, new ListItem("--Select--", "0"));
            LstCombinewith.Items.Insert(0, "--Select--");

        }
    }
}