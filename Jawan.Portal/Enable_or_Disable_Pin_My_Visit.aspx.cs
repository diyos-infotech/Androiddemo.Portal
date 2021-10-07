using System;
using System.Web.UI;
using KLTS.Data;
using Jawan.Portal.DAL;

namespace Jawan.Portal
{
    public partial class Enable_or_Disable_Pin_My_Visit : System.Web.UI.Page
    {
        AppConfiguration config = new AppConfiguration();
        GridViewExportUtil gve = new GridViewExportUtil();
        string EmpIDPrefix = "";
        string BranchID = "";
        string CmpIDPrefix = "";
        string Elength = "";
        string Clength = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            int i = 0;

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
                    }
                    else
                    {
                        Response.Redirect("login.aspx");
                    }
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
            BranchID = Session["BranchID"].ToString();
        }

        protected void PreviligeUsers(int previligerid)
        {

            switch (previligerid)
            {

                case 1:
                    break;
                case 2:
                    //CreateLoginLink.Visible = true;
                    //ChangePasswordLink.Visible = true;
                    //DepartmentLink.Visible = true;
                    //DesignationLink.Visible = true;
                    //BillingAndSalaryLink.Visible = true;
                    //activeEmployeeLink.Visible = true;
                    //SegmentLink.Visible = false;

                    ClientsLink.Visible = true;
                    CompanyInfoLink.Visible = false;
                    InventoryLink.Visible = false;
                    ReportsLink.Visible = true;
                    SettingsLink.Visible = true;
                    break;

                case 3:
                    ClientsLink.Visible = true;
                    CompanyInfoLink.Visible = false;
                    InventoryLink.Visible = false;
                    ReportsLink.Visible = true;
                    SettingsLink.Visible = true;
                    Response.Redirect("BillingAndSalary.aspx");
                    break;

                case 4:

                    EmployeesLink.Visible = true;
                    ClientsLink.Visible = true;
                    CompanyInfoLink.Visible = true;
                    InventoryLink.Visible = true;
                    ReportsLink.Visible = true;
                    SettingsLink.Visible = true;
                    Response.Redirect("Segment.aspx");
                    break;
                case 5:
                    //SettingsLink.Visible = true;
                    //CreateLoginLink.Visible = true;
                    //ChangePasswordLink.Visible = true;
                    //DepartmentLink.Visible = false;
                    //DesignationLink.Visible = false;
                    //SegmentLink.Visible = true;
                    //BillingAndSalaryLink.Visible = false;
                    //activeEmployeeLink.Visible = false;

                    EmployeesLink.Visible = true;
                    ClientsLink.Visible = true;
                    CompanyInfoLink.Visible = false;
                    InventoryLink.Visible = false;
                    ReportsLink.Visible = true;
                    ReportsLink.Visible = true;
                    SettingsLink.Visible = true;
                    break;
                case 6:
                    EmployeesLink.Visible = false;
                    ClientsLink.Visible = false;
                    CompanyInfoLink.Visible = false;
                    InventoryLink.Visible = false;
                    ReportsLink.Visible = false;
                    SettingsLink.Visible = false;
                    //CreateLoginLink.Visible = false;
                    //ChangePasswordLink.Visible = false;
                    //DesignationLink.Visible = false;
                    //SegmentLink.Visible = false;
                    //BillingAndSalaryLink.Visible = false;

                    //activeEmployeeLink.Visible = false;
                    break;
                default:
                    break;


            }
        }

        protected void btnsave_Click(object sender, EventArgs e)
        {
            var Autoapproval = ddlAutoApproval.SelectedValue;
            var Attendancedayspermitted = txtAttendancedayspermitted.Text;

            string query = "update AppFeature set Autoapproval='" + Autoapproval + "',Attendancedayspermitted='" + Attendancedayspermitted + "' where CompanyId=6";
            int result = config.PocketFameExecuteNonQueryWithQueryAsync(query).Result;
            if (result>0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('AppFeature Updated Successfully');", true);
                return;
            }
        }
    }
}