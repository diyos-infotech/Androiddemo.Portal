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
    public partial class EmployeeLeaveSetUp : System.Web.UI.Page
    {
        AppConfiguration Config = new AppConfiguration();

        string EmpIDPrefix = string.Empty;
        string CmpIDPrefix = string.Empty;
        string UserName = string.Empty;
        string BranchID = string.Empty;
        int Type = 1;

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
                    Get_TypeOfLeaves();
                    Get_CombineWith();
                    LoadBranches();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Your Session Expired.Please Login');", true);
                Response.Redirect("~/Login.aspx");
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

        public void Get_TypeOfLeaves()
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

        }

        protected void Btn_AddLeaveSetUp_Click(object sender, EventArgs e)
        {
            try
            {
                string ResetDate = "01/01/1900";

                if (txtmonth.Text.Trim().Length > 0)
                {
                    ResetDate = DateTime.Parse(txtmonth.Text.Trim(), CultureInfo.GetCultureInfo("en-gb")).ToString("yyyy/MM/dd");
                }

                var CreatedOn = DateTime.Now;
                var CreatedBy = UserName;

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
                string SP_AddLeaveSetUp = string.Empty;

                Hashtable AddLeaveSetUpDetails = new Hashtable();
                SP_AddLeaveSetUp = "AddLeaveSetUp";

                //AddLeaveSetUpDetails.Add("@BranchID", BranchID);
                AddLeaveSetUpDetails.Add("@leaveTypeName", TypeOfleave);
                AddLeaveSetUpDetails.Add("@ShortName", ShortName);
                AddLeaveSetUpDetails.Add("@updateNumber", NumOfLeaves);
                AddLeaveSetUpDetails.Add("@maxContinousDays", MaxContDays);
                AddLeaveSetUpDetails.Add("@CombineWith", CombinewithList);
                AddLeaveSetUpDetails.Add("@updateDate", ResetDate);
                AddLeaveSetUpDetails.Add("@maxCashable", MaxCashable);
                AddLeaveSetUpDetails.Add("@maxRollover", MaxRollover);
                AddLeaveSetUpDetails.Add("@maxValue", MaxValue);
                AddLeaveSetUpDetails.Add("@updateFrequency", Frequency);
                AddLeaveSetUpDetails.Add("@holidayCombine", HolidayCombine);
                AddLeaveSetUpDetails.Add("@woCombine", WOCombine);
                AddLeaveSetUpDetails.Add("@holidayTrailing", HolidayTrailing);
                AddLeaveSetUpDetails.Add("@CreatedOn", CreatedOn);
                AddLeaveSetUpDetails.Add("@CreatedBy", CreatedBy);

                IRecordStatus = Config.ExecuteNonQueryParamsAsync(SP_AddLeaveSetUp, AddLeaveSetUpDetails).Result;

                if (IRecordStatus > 0)
                {
                    string query = "update Leave_TypeMaster set status=1 where id='" + ddlTypeOfleave.SelectedValue + "'";
                    int status = Config.ExecuteNonQueryWithQueryAsync(query).Result;
                    Get_TypeOfLeaves();

                    ScriptManager.RegisterStartupScript(this, GetType(), "Show Alert", "alert('Leave Details Added Sucessfull.');", true);
                    ClearData();
                    return;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Show Alert", "alert('Leave Details Not Added Sucessfull. ');", true);
                    return;
                }

            }
            catch (Exception ex)
            {
            }
        }

        private void ClearData()
        {
            txtShortName.Text = string.Empty;
            txtNumOfLeaves.Text = string.Empty;
            txtMaxContDays.Text = string.Empty;
            txtMaxCashable.Text = string.Empty;
            txtMaxRollover.Text = string.Empty;
            txtMaxValue.Text = string.Empty;
            txtmonth.Text = string.Empty;

            ddlTypeOfleave.SelectedIndex = 0;
            ddlFrequency.SelectedIndex = 0;
            LstCombinewith.Items.Clear();

            rbnHolidayCombineYes.Checked = false;
            rbnHolidayCombineNo.Checked = false;
            rbnWOCombineYes.Checked = false;
            rbnWOCombineNo.Checked = false;
            rbnHolidayTrailingYes.Checked = false;
            rbnHolidayTrailingNo.Checked = false;
        }

        protected void Btn_Cancel_Click(object sender, EventArgs e)
        {
            ClearData();
        }

        protected void ddlTypeOfleave_SelectedIndexChanged(object sender, EventArgs e)
        {
            //DataTable DtCombinewith = GlobalData.Instance.LoadTypeOfLeave();
            //if (DtCombinewith.Rows.Count > 0)
            //{
            //    LstCombinewith.DataValueField = "Id";
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
            // LstCombinewith.Items.Remove(ddlTypeOfleave.SelectedItem);
            LstCombinewith.Items.Insert(0, new ListItem("--Select--", "0"));
        }
    }
}