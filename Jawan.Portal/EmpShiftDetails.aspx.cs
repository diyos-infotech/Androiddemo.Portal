using System;
using System.Collections;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data;
using System.Data.SqlClient;
using KLTS.Data;
using System.IO;
using System.Globalization;
using System.Data.OleDb;
using System.Text;
using System.Web.Services;
using System.Collections.Generic;
using Jawan.Portal.DAL;
namespace Jawan.Portal
{
    public partial class EmpShiftDetails : System.Web.UI.Page
    {
        AppConfiguration config = new AppConfiguration();
        GridViewExportUtil gve = new GridViewExportUtil();
        DataTable dt;
        string EmpIDPrefix = "";
        string CmpIDPrefix = "";
        string BranchID = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            lblresult.Text = string.Empty;
            try
            {



                EmpIDPrefix = Session["EmpIDPrefix"].ToString();
                CmpIDPrefix = Session["CmpIDPrefix"].ToString();
                BranchID = Session["BranchID"].ToString();
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

                    LoadClientids();
                    Loadshifts();

                    if (Request.QueryString["Empid"] != null)
                    {

                        string username = Request.QueryString["Empid"].ToString();
                        txtemplyid.Text = username;
                        txtemplyid_TextChanged(sender, e);
                        GetData();

                    }

                    //if (Request.QueryString["Empid"] != null)
                    //{
                    //    string username = Request.QueryString["Empid"].ToString();
                    //    txtemplyid.Text = username;
                    //    txtemplyid_TextChanged(sender, e);

                    //}

                }
            }

            catch (Exception ex)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Your Session Expired.Please Login');", true);
                Response.Redirect("~/login.aspx");
            }
        }

        protected void GetEmpName()
        {
            string Sqlqry = "select (empfname+' '+empmname+' '+emplname) as empname from empdetails where empid='" + txtemplyid.Text + "' ";
            DataTable dt = config.ExecuteAdaptorAsyncWithQueryParams(Sqlqry).Result;
            if (dt.Rows.Count > 0)
            {
                try
                {
                    txtFname.Text = dt.Rows[0]["empname"].ToString();

                }
                catch (Exception ex)
                {
                    // MessageLabel.Text = ex.Message;
                }
            }
            else
            {
                // MessageLabel.Text = "There Is No Name For The Selected Employee";
            }


        }

        protected void txtFname_TextChanged(object sender, EventArgs e)
        {
            Getempid();
            GetData();
        }

        protected void Getempid()
        {

            string Sqlqry = "select  empid from empdetails where empfname+' '+empmname+' '+emplname like '%" + txtFname.Text + "%' ";
            DataTable dt = config.ExecuteAdaptorAsyncWithQueryParams(Sqlqry).Result;
            if (dt.Rows.Count > 0)
            {
                try
                {
                    txtemplyid.Text = dt.Rows[0]["empid"].ToString();

                }
                catch (Exception ex)
                {
                    // MessageLabel.Text = ex.Message;
                }
            }
            else
            {
                // MessageLabel.Text = "There Is No Name For The Selected Employee";
            }

        }

        protected void Loadshifts()
        {
            string query = "select * from shifts";

            DataTable dtshifts = config.ExecuteAdaptorAsyncWithQueryParams(query).Result;
            if (dtshifts.Rows.Count > 0)
            {
                ddlShift.DataValueField = "Shift";
                ddlShift.DataTextField = "Shift";
                ddlShift.DataSource = dtshifts;
                ddlShift.DataBind();
            }

            ddlShift.Items.Insert(0, new ListItem("-Select-", "0"));

        }

        protected void LoadClientids()
        {
            #region New Code for Prefered Units as on 24/12/2013 by venkat

            DataTable dt = GlobalData.Instance.LoadCNames(CmpIDPrefix, BranchID);
            if (dt.Rows.Count > 0)
            {
                DdlPreferedUnit.DataValueField = "clientid";
                DdlPreferedUnit.DataTextField = "clientname";
                DdlPreferedUnit.DataSource = dt;
                DdlPreferedUnit.DataBind();
            }
            DdlPreferedUnit.Items.Insert(0, new ListItem("-Select-", "0"));

            #endregion

        }

        protected void PreviligeUsers(int previligerid)
        {
            switch (previligerid)
            {
                case 1: //Admin
                    break;
                case 2: // RO
                    //AddEmployeeLink.Visible = false;
                    //ModifyEmployeeLink.Visible = false;
                    //DeleteEmployeeLink.Visible = false;
                    AssigningWorkerLink.Visible = true;
                    AttendanceLink.Visible = true;
                    LoanLink.Visible = false;
                    // //PaymentLink.Visible = false;
                    // TrainingEmployeeLink.Visible = true;
                    PostingOrderListLink.Visible = true;
                    //JobLeavingReasonsLink.Visible = true;

                    ClientsLink.Visible = true;
                    CompanyInfoLink.Visible = false;
                    InventoryLink.Visible = false;
                    ReportsLink.Visible = true;
                    SettingsLink.Visible = true;
                    break;

                case 3://Accounts

                    //AddEmployeeLink.Visible = false;
                    //ModifyEmployeeLink.Visible = false;
                    //DeleteEmployeeLink.Visible = false;
                    AssigningWorkerLink.Visible = false;
                    AttendanceLink.Visible = true;
                    LoanLink.Visible = true;
                    //PaymentLink.Visible = true;
                    //TrainingEmployeeLink.Visible = false;
                    PostingOrderListLink.Visible = true;
                    //JobLeavingReasonsLink.Visible = false;

                    ClientsLink.Visible = true;
                    CompanyInfoLink.Visible = false;
                    InventoryLink.Visible = false;
                    ReportsLink.Visible = true;
                    SettingsLink.Visible = true;
                    break;

                case 4:
                    //AddEmployeeLink.Visible = false;
                    //ModifyEmployeeLink.Visible = false;
                    //DeleteEmployeeLink.Visible = false;
                    AssigningWorkerLink.Visible = false;
                    AttendanceLink.Visible = false;
                    LoanLink.Visible = false;
                    //PaymentLink.Visible = false;
                    //TrainingEmployeeLink.Visible = false;
                    PostingOrderListLink.Visible = true;
                    //JobLeavingReasonsLink.Visible = false;

                    ClientsLink.Visible = true;
                    CompanyInfoLink.Visible = true;
                    InventoryLink.Visible = true;
                    ReportsLink.Visible = true;
                    SettingsLink.Visible = true;
                    Response.Redirect("PostingOrderList.aspx");

                    break;
                case 5:
                    //AddEmployeeLink.Visible = false;
                    //ModifyEmployeeLink.Visible = false;
                    //DeleteEmployeeLink.Visible = false;
                    AssigningWorkerLink.Visible = false;
                    LoanLink.Visible = false;
                    //PaymentLink.Visible = false;
                    //TrainingEmployeeLink.Visible = true;
                    //JobLeavingReasonsLink.Visible = false;
                    AttendanceLink.Visible = true;

                    EmployeesLink.Visible = true;
                    ClientsLink.Visible = true;
                    CompanyInfoLink.Visible = false;
                    InventoryLink.Visible = false;
                    ReportsLink.Visible = true;
                    ReportsLink.Visible = true;
                    SettingsLink.Visible = true;
                    Response.Redirect("EmployeeAttendance.aspx");
                    break;
                case 6:

                    EmployeesLink.Visible = false;
                    ClientsLink.Visible = false;
                    CompanyInfoLink.Visible = false;
                    //AddEmployeeLink.Visible = false;
                    //ModifyEmployeeLink.Visible = false;
                    //DeleteEmployeeLink.Visible = false;
                    AssigningWorkerLink.Visible = false;
                    AttendanceLink.Visible = false;
                    LoanLink.Visible = false;
                    //PaymentLink.Visible = false;
                    //TrainingEmployeeLink.Visible = false;
                    //JobLeavingReasonsLink.Visible = false;
                    InventoryLink.Visible = true;
                    CompanyInfoLink.Visible = false;
                    ReportsLink.Visible = false;
                    SettingsLink.Visible = false;

                    break;
                default:
                    break;


            }
        }

        public void GetData()
        {
            string query = "select isnull(Shift,0) Shift,ShiftStartTime,ShiftEndTime,isnull(Woff1,0) Woff1,isnull(Woff2,0) Woff2,isnull(SitePostedTo,0) SitePostedTo,Address,Name from EmpDetails where empid='" + txtemplyid.Text + "'";
            DataTable dt = config.ExecuteAdaptorAsyncWithQueryParams(query).Result;
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["SitePostedTo"].ToString() != "0")
                {
                    DdlPreferedUnit.SelectedValue = dt.Rows[0]["SitePostedTo"].ToString();
                }
                else
                {
                    DdlPreferedUnit.SelectedIndex = 0;
                }

                txtEmpFName.Text = dt.Rows[0]["Name"].ToString();
                if (dt.Rows[0]["Shift"].ToString() != "0")
                {
                    ddlShift.SelectedValue = dt.Rows[0]["Shift"].ToString();
                }
                else
                {
                    ddlShift.SelectedIndex = 0;
                }
                txtShiftstarttime.Text = dt.Rows[0]["ShiftStartTime"].ToString();
                txtShiftEndtime.Text = dt.Rows[0]["ShiftEndTime"].ToString();

                if (dt.Rows[0]["Woff1"].ToString() != "0")
                {
                    ddlWoff1.SelectedIndex = int.Parse(dt.Rows[0]["Woff1"].ToString());
                }
                else
                {
                    ddlWoff1.SelectedIndex = 0;
                }

                if (dt.Rows[0]["Woff2"].ToString() != "0")
                {
                    ddlWoff2.SelectedIndex = int.Parse(dt.Rows[0]["Woff2"].ToString());
                }
                else
                {
                    ddlWoff2.SelectedIndex = 0;
                }
                txtAddress.Text = dt.Rows[0]["Address"].ToString();
            }
            else
            {
                DdlPreferedUnit.SelectedIndex = 0;
                txtEmpFName.Text = "";
                ddlShift.SelectedIndex = 0;
                txtShiftstarttime.Text = "";
                txtShiftEndtime.Text = "";
                ddlWoff1.SelectedIndex = 0;
                ddlWoff2.SelectedIndex = 0;
                txtAddress.Text = "";
            }
        }

        protected void txtemplyid_TextChanged(object sender, EventArgs e)
        {
            GetEmpName();
            GetData();
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            var Sitepostedto = "";
            var Name = "";
            var Shift = "";
            var Shiftstarttime = "";
            var Shiftendtime = "";
            var Woff1 = 0;
            var Woff2 = 0;
            var Address = "";
            var Empid = txtemplyid.Text;
            Sitepostedto = DdlPreferedUnit.SelectedValue;
            Name = txtEmpFName.Text;
            Shift = ddlShift.SelectedValue;
            Shiftstarttime = txtShiftstarttime.Text;
            Shiftendtime = txtShiftEndtime.Text;
            Woff1 = ddlWoff1.SelectedIndex;
            Woff2 = ddlWoff2.SelectedIndex;
            Address = txtAddress.Text;
            string query = "Update empdetails set SitePostedTo='" + Sitepostedto + "',Unitid='" + Sitepostedto + "',Name='" + Name + "',Shift='" + Shift + "',Shiftstarttime='" + Shiftstarttime + "',Shiftendtime='" + Shiftendtime + "',Woff1='" + Woff1 + "',Woff2='" + Woff2 + "',Address='" + Address + "' where empid='" + Empid + "'";
            int result = config.ExecuteNonQueryWithQueryAsync(query).Result;
            if (result > 0)
            {
                lblresult.Text = "Employee Shift Details Updated Sucessfully";
            }
        }

        protected void ddlShift_SelectedIndexChanged(object sender, EventArgs e)
        {
            string query = "select ShiftStartTime,ShiftEndTime from shifts where shift='" + ddlShift.SelectedValue + "'";
            DataTable dt = config.ExecuteAdaptorAsyncWithQueryParams(query).Result;
            if (dt.Rows.Count > 0)
            {
                txtShiftstarttime.Text = dt.Rows[0]["ShiftStartTime"].ToString();
                txtShiftEndtime.Text = dt.Rows[0]["ShiftEndTime"].ToString();
            }
        }
    }
}