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
using KLTS.Data;
using Jawan.Portal.DAL;
namespace Jawan.Portal
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        AppConfiguration config = new AppConfiguration();
        GridViewExportUtil gve = new GridViewExportUtil();
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {

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

                if (Convert.ToInt32(Session["AccessLevel"]) == 1)
                {
                    lbloldpassword.Visible = false;
                    txtoldpassword.Visible = false;
                }
                string selectquery = "select emp_id,UserName from logindetails Order By Emp_Id ";
                dt =config.ExecuteAdaptorAsyncWithQueryParams(selectquery).Result;
                int i = 0;

                //if (!IsPostBack)
                {
                    for (i = 0; i < dt.Rows.Count; i++)
                    {
                        ddlempid.Items.Add(dt.Rows[i][1].ToString());
                    }
                }
            }
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

                case 4:
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
                case 5:
                    SettingsLink.Visible = true;
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

        protected void Button2_Click(object sender, EventArgs e)
        {
            lblresult.Visible = true;
            try
            {
                if (ddlempid.SelectedIndex <= 0)
                {
                    lblresult.Text = "Select Employee Id";
                    return;
                }
                if (txtnewpassword.Text.Trim().Length <= 0)
                {
                    lblresult.Text = "New Password should not be empty";
                    return;
                }
                string selectquery = "select Password from logindetails where UserName = '" + ddlempid.SelectedItem.Text + "'";
                dt = config.ExecuteAdaptorAsyncWithQueryParams(selectquery).Result;
                if (Convert.ToInt32(Session["AccessLevel"]) != 1)
                {
                    if (txtoldpassword.Text.Trim() == dt.Rows[0][0].ToString())
                    {
                        if (txtnewpassword.Text.Trim() == txtconfirmpassword.Text.Trim())
                        {
                            string updatequery = "update logindetails set password = '" + txtnewpassword.Text + "' Where UserName='" + ddlempid.SelectedItem.Text + "'";
                            int status =config.ExecuteNonQueryWithQueryAsync(updatequery).Result;
                            if (status != 0)
                            {
                                lblresult.Text = "Password Changed Successfully";
                                ddlempid.SelectedIndex = 0;
                            }
                            else
                            {
                                lblresult.Text = "Password Not Changed ";
                            }
                        }
                        else
                        {
                            lblresult.Text = "Password Not Match With Confirm Password";
                        }
                    }
                    else
                    {
                        lblresult.Text = "You Are Entered Wrong Password";
                    }
                }
                else
                {
                    if (txtnewpassword.Text.Trim() == txtconfirmpassword.Text.Trim())
                    {
                        string updatequery = "update logindetails set password = '" + txtnewpassword.Text + "' Where UserName='" + ddlempid.SelectedItem.Text + "'";
                        int status = config.ExecuteNonQueryWithQueryAsync(updatequery).Result;
                        if (status != 0)
                        {
                            lblresult.Text = "Password Changed Successfully";
                            ddlempid.SelectedIndex = 0;
                        }
                        else
                        {
                            lblresult.Text = "Password Not Changed ";
                        }
                    }
                    else
                    {
                        lblresult.Text = "Password Not Match With Confirm Password";
                    }
                }
            }
            catch (Exception ex)
            {
                lblresult.Visible = true;

                lblresult.Text = "Incorrect Data";
            }
        }
    }
}