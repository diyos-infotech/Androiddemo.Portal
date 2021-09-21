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
    public partial class DeleteItem : System.Web.UI.Page
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

                    break;

                case 3:
                    break;

                case 4:
                    break;
                case 5:

                    break;
                case 6:
                    EmployeesLink.Visible = false;
                    ClientsLink.Visible = true;
                    CompanyInfoLink.Visible = false;
                    InventoryLink.Visible = true;
                    ReportsLink.Visible = true;
                    SettingsLink.Visible = false;
                    break;
                default:
                    break;
            }
        }

        public void displaydata()
        {
            string selectquery = " select * from Stockitemlist Order By (cast(Itemid as int))";
            dt = config.ExecuteAdaptorAsyncWithQueryParams(selectquery).Result;
            gvitems.DataSource = dt;
            gvitems.DataBind();

        }

        protected void gvitems_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                Label itemid = gvitems.Rows[e.RowIndex].FindControl("lblitemid") as Label;

                string deletequery = "delete from Stockitemlist where itemid = " + itemid.Text;
                int status =config.ExecuteNonQueryWithQueryAsync(deletequery).Result;


                if (status != 0)
                    lblresult.Text = "Record Deleted Successfully";
                else
                    lblresult.Text = "Record Not Deleted";

                displaydata();

            }
            catch (Exception ex)
            {
                lblresult.Text = "Record are Not Available";
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            lblresult.Text = "";
            lblresult.Visible = true;
            gvitems.DataSource = null;
            gvitems.DataBind();
            string SqlQrySearch = "select * from Stockitemlist  Where ItemName Like '%" + txtsearch.Text.Trim() + "%'";
            DataTable dtSearch = config.ExecuteAdaptorAsyncWithQueryParams(SqlQrySearch).Result;
            if (dtSearch.Rows.Count > 0)
            {
                gvitems.DataSource = dtSearch;
                gvitems.DataBind();
            }
            else
            {
                lblresult.Text = "There Is No  Items";
            }
        }

        protected void gvitems_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvitems.PageIndex = e.NewPageIndex;
            displaydata();
        }
    }
}