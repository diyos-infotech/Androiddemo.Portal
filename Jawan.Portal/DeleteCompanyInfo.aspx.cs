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
    public partial class DeleteCompanyInfo : System.Web.UI.Page
    {
        DataTable dt;
        AppConfiguration config = new AppConfiguration();
        GridViewExportUtil gve = new GridViewExportUtil();


        private void displaydata()
        {
            string selectquery = " select * from companyinfo";
            dt = config.ExecuteAdaptorAsyncWithQueryParams(selectquery).Result;
            gvdeletecompanyinfo.DataSource = dt;
            gvdeletecompanyinfo.DataBind();
        }
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
                displaydata();

            }
        }

        protected void PreviligeUsers(int previligerid)
        {

            switch (previligerid)
            {

                case 1:
                    break;
                case 2:
                    AddCompanyInfoLink.Visible = true;
                    ModifyCompanyInfoLink.Visible = false;
                    DeleteCompanyInfoLink.Visible = false;
                    InventoryLink.Visible = false;
                    ReportsLink.Visible = false;
                    SettingsLink.Visible = false;

                    break;

                case 3:

                    InventoryLink.Visible = false;
                    ReportsLink.Visible = false;
                    SettingsLink.Visible = false;
                    break;
                case 4:
                    AddCompanyInfoLink.Visible = true;
                    ModifyCompanyInfoLink.Visible = true;
                    DeleteCompanyInfoLink.Visible = true;

                    EmployeesLink.Visible = true;
                    ClientsLink.Visible = true;
                    CompanyInfoLink.Visible = true;
                    InventoryLink.Visible = true;
                    ReportsLink.Visible = true;
                    SettingsLink.Visible = true;
                    break;
                case 5:
                    CompanyInfoLink.Visible = false;
                    AddCompanyInfoLink.Visible = false;
                    ModifyCompanyInfoLink.Visible = false;
                    DeleteCompanyInfoLink.Visible = false;
                    break;
                case 6:

                    CompanyInfoLink.Visible = false;
                    AddCompanyInfoLink.Visible = false;
                    ModifyCompanyInfoLink.Visible = false;
                    DeleteCompanyInfoLink.Visible = false;

                    InventoryLink.Visible = false;
                    ReportsLink.Visible = false;
                    SettingsLink.Visible = false;




                    break;
                default:
                    break;


            }
        }
        protected void gvdeletecompanyinfo_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            try
            {
                Label companyinfo = gvdeletecompanyinfo.Rows[e.RowIndex].FindControl("lblcompanyname") as Label;
                string deletequery = "delete from companyinfo where companyname = '" + companyinfo.Text + "'";
                int status =config.ExecuteNonQueryWithQueryAsync(deletequery).Result;
                if (status != 0)
                {
                    lblresult.Visible = true;
                    lblresult.Text = "Record Deleted Successfully";
                }
                else
                {
                    lblresult.Visible = true;
                    lblresult.Text = "Record Not Deleted";
                }
                displaydata();
            }
            catch (Exception ex)
            {
                lblresult.Visible = true;
                lblresult.Text = "Incorrect Option Please Check";

            }







        }
    }
}