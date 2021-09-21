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
using Jawan.Portal.DAL;
namespace Jawan.Portal
{
    public partial class Employees : System.Web.UI.Page
    {
        AppConfiguration config = new AppConfiguration();
        GridViewExportUtil gve = new GridViewExportUtil();
        string EmpIDPrefix = "";
        string Branch = "";
        public void DisplayData()
        {
            int OrderBy = 1;

            DataTable DtEmployees = GlobalData.Instance.LoadActiveEmployeesOrderBy(EmpIDPrefix, Branch, OrderBy);
            if (DtEmployees.Rows.Count > 0)
            {
                gvemployee.DataSource = DtEmployees;
                gvemployee.DataBind();
            }
            else
            {
                gvemployee.DataSource = null;
                gvemployee.DataBind();


                ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('There is No employees . ');", true);
            }
        }

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
                    }
                    else
                    {
                        Response.Redirect("login.aspx");
                    }

                    DisplayData();
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void PreviligeUsers(int previligerid)
        {

            switch (previligerid)
            {

                case 1:
                    break;
                case 2:
                    //AddEmployeeLink.Visible = true;
                    //ModifyEmployeeLink.Visible = true;
                    //DeleteEmployeeLink.Visible = true;
                    AssigningWorkerLink.Visible = true;
                    AttendanceLink.Visible = true;
                    LoanLink.Visible = false;
                    PaymentLink.Visible = false;
                    //TrainingEmployeeLink.Visible = true;
                    PostingOrderListLink.Visible = true;
                    //JobLeavingReasonsLink.Visible = true;

                    ClientsLink.Visible = true;
                    CompanyInfoLink.Visible = false;
                    InventoryLink.Visible = false;
                    ReportsLink.Visible = true;
                    SettingsLink.Visible = true;
                    break;

                case 3:

                    InventoryLink.Visible = false;
                    ReportsLink.Visible = false;
                    SettingsLink.Visible = false;

                    break;

                case 4:

                    //AddEmployeeLink.Visible = true;
                    //ModifyEmployeeLink.Visible = false;
                    //DeleteEmployeeLink.Visible = false;
                    AssigningWorkerLink.Visible = false;
                    AttendanceLink.Visible = false;
                    LoanLink.Visible = false;
                    PaymentLink.Visible = false;
                    //TrainingEmployeeLink.Visible = false;
                    //JobLeavingReasonsLink.Visible = false;


                    CompanyInfoLink.Visible = false;
                    InventoryLink.Visible = false;
                    ReportsLink.Visible = false;
                    SettingsLink.Visible = false;


                    break;
                case 5:
                    //AddEmployeeLink.Visible = false;
                    //ModifyEmployeeLink.Visible = false;
                    //DeleteEmployeeLink.Visible = false;
                    AttendanceLink.Visible = true;
                    LoanLink.Visible = false;
                    PaymentLink.Visible = false;
                    //TrainingEmployeeLink.Visible = false;
                    //JobLeavingReasonsLink.Visible = false;

                    CompanyInfoLink.Visible = false;
                    InventoryLink.Visible = false;
                    ReportsLink.Visible = false;
                    SettingsLink.Visible = false;
                    break;
                case 6:
                    //AddEmployeeLink.Visible = false;
                    //ModifyEmployeeLink.Visible = false;
                    //DeleteEmployeeLink.Visible = false;
                    AttendanceLink.Visible = true;
                    LoanLink.Visible = false;
                    PaymentLink.Visible = false;
                    //TrainingEmployeeLink.Visible = false;
                    //JobLeavingReasonsLink.Visible = false;


                    CompanyInfoLink.Visible = false;
                    InventoryLink.Visible = false;
                    SettingsLink.Visible = false;

                    break;
                default:
                    break;


            }
        }


        protected void GetWebConfigdata()
        {
            EmpIDPrefix = Session["EmpIDPrefix"].ToString();
            Branch = Session["BranchID"].ToString();

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {

                if (txtsearch.Text.Trim().Length == 0)
                {
                    lblMsg.Text = "Please Enter The Employee ID/Name. Whatever You Want To Search";
                    return;
                }
                // Begin Code For Variable Declaration as on [18-10-2013]
                Hashtable HtSearchEmployee = new Hashtable();
                DataTable DtSearchEmployee = null;
                var SPName = "";
                var SearchedEmpIdOrName = "";

                //  Begin Code For Assign Values to The Variables as on [18-10-2013]
                SearchedEmpIdOrName = txtsearch.Text;
                SPName = "IMSearchEmployeeIdorName";

                //  Begin Code For SP Parameters as on [18-10-2013]
                HtSearchEmployee.Add("@EmpidorName", SearchedEmpIdOrName);
                HtSearchEmployee.Add("@empidprefix", EmpIDPrefix);
                HtSearchEmployee.Add("@Branch", Branch);


                //  Begin Code For Calling Stored Procedure as on [18-10-2013]
                DtSearchEmployee = config.ExecuteAdaptorAsyncWithParams(SPName, HtSearchEmployee).Result;


                //  Begin code For Assing Data to Gridview Whatever Retrived As on[18-10-2010]

                if (DtSearchEmployee.Rows.Count > 0)
                {
                    gvemployee.DataSource = DtSearchEmployee;
                    gvemployee.DataBind();
                }
                else
                {
                    gvemployee.DataSource = null;
                    gvemployee.DataBind();
                    lblMsg.Text = "There are no Employees";
                }


            }
            catch (Exception ex)
            {
                lblMsg.Text = "Your Session Expired . Please Login";
            }


        }

        protected void gvDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            lblresult.Visible = true;
            Label empid = gvemployee.Rows[e.RowIndex].FindControl("lblempid") as Label;
            string deletequery = "Update Empdetails set empstatus=0  where EmpId ='" + empid.Text.Trim() + "'";
            int status = config.ExecuteNonQueryWithQueryAsync(deletequery).Result;
            if (status != 0)
            {
                lblSuc.Text = "Employee Inactivated Successfully";
            }
            else
            {
                lblMsg.Text = "Employee Not Inactivated ";
            }

            DisplayData();
        }


        protected void lbtn_Select_Click(object sender, EventArgs e)
        {
            try
            {

                ImageButton thisTextBox = (ImageButton)sender;
                GridViewRow thisGridViewRow = (GridViewRow)thisTextBox.Parent.Parent;
                Label lblid = (Label)thisGridViewRow.FindControl("lblempid");
                Response.Redirect("ViewEmployee.aspx?Empid=" + lblid.Text, false);

            }
            catch (Exception ex)
            {
                lblMsg.Text = "Your Session Expired . Please Login";
            }

        }

        protected void lbtn_Edit_Click(object sender, EventArgs e)
        {
            try
            {
                ImageButton thisTextBox = (ImageButton)sender;
                GridViewRow thisGridViewRow = (GridViewRow)thisTextBox.Parent.Parent;
                Label lblid = (Label)thisGridViewRow.FindControl("lblempid");
                Response.Redirect("ModifyEmployee.aspx?Empid=" + lblid.Text, false);
            }
            catch (Exception ex)
            {
                lblMsg.Text = "Your Session Expired . Please Login";

            }

        }

        protected void gvemployee_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvemployee.PageIndex = e.NewPageIndex;
            DisplayData();
        }

        protected void gvemployee_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label status = (Label)e.Row.FindControl("lblempGen") as Label;
                if (status.Text == "INACTIVE")
                {
                    e.Row.ForeColor = System.Drawing.Color.Red;
                    //ImageButton edit = (ImageButton)e.Row.FindControl("lbtn_Edit") as ImageButton;
                    //edit.Enabled = false;


                }

            }
        }

        protected void lbtn_clntman_Click(object sender, ImageClickEventArgs e)
        {

            try
            {
                ImageButton thisTextBox = (ImageButton)sender;
                GridViewRow thisGridViewRow = (GridViewRow)thisTextBox.Parent.Parent;
                Label lblid = (Label)thisGridViewRow.FindControl("lblempid");
                Response.Redirect("EmpShiftDetails.aspx?Empid=" + lblid.Text, false);
            }
            catch (Exception ex)
            {
                lblMsg.Text = "Your Session Expired . Please Login";
            }

        }
    }
}