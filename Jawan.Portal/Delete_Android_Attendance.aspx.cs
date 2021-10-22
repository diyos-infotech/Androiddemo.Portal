using System;
using System.Collections;
using System.Data;
using System.Web.UI;
using KLTS.Data;
using System.Globalization;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Jawan.Portal.DAL;


namespace Jawan.Portal
{
    public partial class Delete_Android_Attendance : System.Web.UI.Page
    {
        GridViewExportUtil GVUtil = new GridViewExportUtil();
        AppConfiguration config = new AppConfiguration();
        DataTable dt;
        string EmpIDPrefix = "";
        string CmpIDPrefix = "";
        string BranchID = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                GetWebConfigdata();
                if (!IsPostBack)
                {
                    if (Session["UserId"] != null && Session["AccessLevel"] != null)
                    {
                        lblDisplayUser.Text = Session["UserId"].ToString();
                        PreviligeUsers(Convert.ToInt32(Session["AccessLevel"]));
                        lblcname.Text = SqlHelper.Instance.GetCompanyname();
                        switch (SqlHelper.Instance.GetCompanyValue())
                        {
                            case 0:// Write Omulance Invisible Links
                                break;
                            case 1://Write KLTS Invisible Links
                                //ExpensesReportsLink.Visible = false;
                                break;
                            case 2://write Fames Link
                                //ExpensesReportsLink.Visible = true;
                                break;


                            default:
                                break;
                        }
                    }
                    else
                    {
                        Response.Redirect("login.aspx");
                    }

                    LoadClientList();
                    LoadClientNames();
                    LoadStaffIDs();

                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Show alert", "alert('Your Session Expired');", true);
                Response.Redirect("~/Login.aspx");
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
                    ClientsLink.Visible = false;
                    CompanyInfoLink.Visible = false;
                    InventoryLink.Visible = false;
                    SettingsLink.Visible = false;
                    ClientsReportLink.Visible = false;
                    InventoryReportLink.Visible = false;
                    ExpensesReportsLink.Visible = false;

                    break;

                case 4:
                    EmployeesLink.Visible = false;
                    CompanyInfoLink.Visible = false;
                    InventoryLink.Visible = false;
                    SettingsLink.Visible = false;
                    InventoryReportLink.Visible = false;
                    ExpensesReportsLink.Visible = false;

                    break;
                case 5:
                    ClientsLink.Visible = false;
                    CompanyInfoLink.Visible = false;
                    InventoryLink.Visible = false;
                    SettingsLink.Visible = false;
                    ClientsReportLink.Visible = false;
                    InventoryReportLink.Visible = false;
                    ExpensesReportsLink.Visible = false;

                    break;
                case 6:
                    EmployeesLink.Visible = false;
                    ClientsLink.Visible = false;
                    CompanyInfoLink.Visible = false;
                    InventoryLink.Visible = true;
                    ReportsLink.Visible = true;
                    SettingsLink.Visible = false;
                    EmployeeReportLink.Visible = false;

                    break;
                case 7:
                    ClientsLink.Visible = false;
                    CompanyInfoLink.Visible = false;
                    InventoryLink.Visible = false;
                    SettingsLink.Visible = false;
                    InventoryReportLink.Visible = false;
                    ExpensesReportsLink.Visible = false;
                    EmployeesLink.Visible = false;
                    break;
                case 8:
                    ClientsLink.Visible = false;
                    CompanyInfoLink.Visible = false;
                    InventoryLink.Visible = false;
                    SettingsLink.Visible = false;
                    ClientsReportLink.Visible = false;
                    InventoryReportLink.Visible = false;
                    ExpensesReportsLink.Visible = false;

                    break;
                default:
                    break;
            }
        }

        protected void GetWebConfigdata()
        {
            EmpIDPrefix = Session["EmpIDPrefix"].ToString();
            CmpIDPrefix = Session["CmpIDPrefix"].ToString();
            BranchID = Session["BranchID"].ToString();


        }

        protected void LoadClientNames()
        {
            DataTable DtClientids = GlobalData.Instance.LoadCNames(CmpIDPrefix, BranchID);
            if (DtClientids.Rows.Count > 0)
            {
                ddlCName.DataValueField = "Clientid";
                ddlCName.DataTextField = "clientname";
                ddlCName.DataSource = DtClientids;
                ddlCName.DataBind();
            }
            ddlCName.Items.Insert(0, "-Select-");
            ddlCName.Items.Insert(1, "ALL");

        }

        protected void LoadStaffIDs()
        {
            DataTable DtopmEmpsIDs = GlobalData.Instance.LoadStaffIDs();
            if (DtopmEmpsIDs.Rows.Count > 0)
            {
                ddlFOID.DataValueField = "EmpId";
                ddlFOID.DataTextField = "FullName";
                ddlFOID.DataSource = DtopmEmpsIDs;
                ddlFOID.DataBind();
            }
            ddlFOID.Items.Insert(0, "-Select-");
            ddlFOID.Items.Insert(1, "ALL");

        }

        protected void LoadClientList()
        {
            DataTable DtClientNames = GlobalData.Instance.LoadCIds(CmpIDPrefix, BranchID);
            if (DtClientNames.Rows.Count > 0)
            {
                ddlClientID.DataValueField = "Clientid";
                ddlClientID.DataTextField = "Clientid";
                ddlClientID.DataSource = DtClientNames;
                ddlClientID.DataBind();
            }
            ddlClientID.Items.Insert(0, "-Select-");
            ddlClientID.Items.Insert(1, "ALL");
        }

        protected void ddlCName_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearData();
            if (ddlCName.SelectedIndex > 0)
            {
                txtmonth.Text = "";
                ddlClientID.SelectedValue = ddlCName.SelectedValue;

            }
            else
            {
                ddlClientID.SelectedIndex = 0;
            }
        }

        protected void ddlClientID_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearData();

            if (ddlClientID.SelectedIndex > 0)
            {
                txtmonth.Text = "";
                ddlCName.SelectedValue = ddlClientID.SelectedValue;
            }
            else
            {
                ddlCName.SelectedIndex = 0;
            }
        }

        protected void Getdata()
        {
            GvDayWiseAttendance.DataSource = null;
            GvDayWiseAttendance.DataBind();


            if (ddlOption.SelectedIndex == 0)
            {
                if (ddlClientID.SelectedIndex == 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showlalert", "alert('Please Select Client Id/Name');", true);

                    return;
                }
            }

            if (ddlOption.SelectedIndex == 1)
            {
                if (ddlFOID.SelectedIndex == 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showlalert", "alert('Please Select FO ID');", true);

                    return;
                }
            }

            if (txtmonth.Text.Trim().Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showlalert", "alert('Please Select Month');", true);

                return;
            }
            string date = string.Empty;

            if (txtmonth.Text.Trim().Length > 0)
            {
                date = DateTime.Parse(txtmonth.Text.Trim(), CultureInfo.GetCultureInfo("en-gb")).ToString("yyyy/MM/dd");
            }
            string Type = "0";
            string clientid = "";
            string FOID = "";

            if (ddlClientID.SelectedIndex == 1)
            {
                clientid = "%";
            }
            else
            {
                clientid = ddlClientID.SelectedValue;
            }

            if (ddlFOID.SelectedIndex == 1)
            {
                FOID = "%";
            }
            else
            {
                FOID = ddlFOID.SelectedValue;
            }

            string month = DateTime.Parse(date).Month.ToString();
            string Year = DateTime.Parse(date).Year.ToString();

            string AttMonth = month + Year.Substring(2, 2);

            string spname = "";
            DataTable dtBP = null;
            Hashtable HashtableBP = new Hashtable();

            spname = "GetAndroidAttendanceData";
            HashtableBP.Add("@Date", date);
            HashtableBP.Add("@clientid", clientid);
            HashtableBP.Add("@Type", ddltypes.SelectedIndex);
            HashtableBP.Add("@Month", AttMonth);
            HashtableBP.Add("@Option", ddlOption.SelectedIndex);
            HashtableBP.Add("@FOID", FOID);



            dtBP = config.ExecuteAdaptorAsyncWithParams(spname, HashtableBP).Result;
            if (dtBP.Rows.Count > 0)
            {
                GvDayWiseAttendance.DataSource = dtBP;
                GvDayWiseAttendance.DataBind();
                lbtn_Export.Visible = true;
            }
            else
            {
                GvDayWiseAttendance.DataSource = null;
                GvDayWiseAttendance.DataBind();
                lbtn_Export.Visible = false;
            }

        }

        protected void ClearData()
        {
            GvDayWiseAttendance.DataSource = null;
            GvDayWiseAttendance.DataBind();
            lbtn_Export.Visible = false;
        }

        protected void lbtn_Export_Click(object sender, EventArgs e)
        {
            GVUtil.Export("GetAndroidAttendanceData.xls", this.GvDayWiseAttendance);

        }

        protected void ddltypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            GvDayWiseAttendance.DataSource = null;
            GvDayWiseAttendance.DataBind();

            if (ddlOption.SelectedIndex == 0)
            {
                lblclientid.Visible = true;
                ddlClientID.Visible = true;
                lblclientname.Visible = true;
                ddlCName.Visible = true;
                lblFOId.Visible = false;
                ddlFOID.Visible = false;
                ddlClientID.SelectedIndex = 0;
                ddlCName.SelectedIndex = 0;

            }
            else
            {
                lblclientid.Visible = false;
                ddlClientID.Visible = false;
                lblclientname.Visible = false;
                ddlCName.Visible = false;
                lblFOId.Visible = true;
                ddlFOID.Visible = true;
                ddlFOID.SelectedIndex = 0;
            }
        }

        protected void txtmonth_TextChanged(object sender, EventArgs e)
        {
            Getdata();
        }

        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            string sqlqry = string.Empty;
            string date = string.Empty;

            if (txtmonth.Text.Trim().Length > 0)
            {
                date = DateTime.Parse(txtmonth.Text.Trim(), CultureInfo.GetCultureInfo("en-gb")).ToString("yyyy/MM/dd");
            }

            var list = new List<Tuple<int, string>>();
            if (GvDayWiseAttendance.Rows.Count > 0)
            {
                for (int i = 0; i < GvDayWiseAttendance.Rows.Count; i++)
                {
                    CheckBox chkclientid = GvDayWiseAttendance.Rows[i].FindControl("chkindividual") as CheckBox;
                    Label lblclientid = GvDayWiseAttendance.Rows[i].FindControl("lblclientid") as Label;
                    Label lblEmpID = GvDayWiseAttendance.Rows[i].FindControl("lblEmpID") as Label;

                    if (chkclientid.Checked == true)
                    {
                        var Clientid = lblclientid.Text;
                        var EmpID = lblEmpID.Text;

                        string query = "Delete from DailyAttendance_Android where clientid='" + Clientid + "' and EmpID='" + EmpID + "' and cast(CheckInDateTime as date)='" + date + "'";
                        int Result = config.ExecuteNonQueryWithQueryAsync(query).Result;

                        if (Result>0)
                        {
                            Getdata();
                        }
                    }

                }
            }
        }
    }
}