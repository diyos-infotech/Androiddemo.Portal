using System;
using System.Collections;
using System.Linq;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using KLTS.Data;

namespace Jawan.Portal
{
    public partial class M_Leads : System.Web.UI.Page
    {
        DataTable dt;
        string CmpIDPrefix = "";



        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                GetWebConfigdata();
                if (!IsPostBack)
                {
                    DisplayData();
                }
            }
            catch (Exception ex)
            {

            }
        }

        public void DisplayData()
        {
            gvclient.DataSource = null;
            gvclient.DataBind();
            var SPName = "";

            DataTable DtListOfClients = null;
            Hashtable HtListOfClients = new Hashtable();

            SPName = "LeadDetailsSearchBase";

            HtListOfClients.Add("@clientidprefix", CmpIDPrefix);

            DtListOfClients = SqlHelper.Instance.ExecuteStoredProcedureWithParams(SPName, HtListOfClients);
            if (DtListOfClients.Rows.Count > 0)
            {
                gvclient.DataSource = DtListOfClients;
                gvclient.DataBind();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('The Client Details Are Not Avaialable');", true);
            }

        }

        protected void GetWebConfigdata()
        {
            //CmpIDPrefix = Session["CmpIDPrefix"].ToString();
        }


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gvclient.DataSource = null;
            gvclient.DataBind();

            if (txtsearch.Text.Trim().Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Show Alert", "alert('Please Enter Either Client ID Nor Client Name');", true);
                return;
            }


            Hashtable HTSpParameters = new Hashtable();
            var SPName = "SearchIndleadIfo";
            var SearchedValue = txtsearch.Text;
            HTSpParameters.Add("@leadorName", SearchedValue);

            DataTable DtIndClientInfo = SqlHelper.Instance.ExecuteStoredProcedureWithParams(SPName, HTSpParameters);
            if (DtIndClientInfo.Rows.Count > 0)
            {
                gvclient.DataSource = DtIndClientInfo;
                gvclient.DataBind();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Search not Found');", true);
            }
        }

        protected void gvDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            lblresult.Visible = true;
            Label ClientId = gvclient.Rows[e.RowIndex].FindControl("lblclientid") as Label;
            string deletequery = "delete from Leads where leadid ='" + ClientId.Text.Trim() + "'";
            int status = SqlHelper.Instance.ExecuteDMLQry(deletequery);
            if (status != 0)
            {
                lblresult.Text = "Lead Deleted Successfully";
            }
            else
            {
                lblresult.Text = "Lead Not Deleted ";
            }

            DisplayData();
        }

        protected void lbtn_Select_Click(object sender, EventArgs e)
        {
            try
            {

                ImageButton thisTextBox = (ImageButton)sender;
                GridViewRow thisGridViewRow = (GridViewRow)thisTextBox.Parent.Parent;
                Label lblid = (Label)thisGridViewRow.FindControl("lblclientid");
                Response.Redirect("Viewlead.aspx?leadid=" + lblid.Text, false);

            }
            catch (Exception ex)
            {
            }

        }

        protected void gvclient_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvclient.PageIndex = e.NewPageIndex;
            DisplayData();
        }


        protected void IbModify_Click(object sender, EventArgs e)
        {
            try
            {
                ImageButton thisTextBox = (ImageButton)sender;
                GridViewRow thisGridViewRow = (GridViewRow)thisTextBox.Parent.Parent;
                Label lblid = (Label)thisGridViewRow.FindControl("lblclientid");
                Response.Redirect("M_Modify_Lead.aspx?LeadID=" + lblid.Text, false);
            }
            catch (Exception ex)
            {

            }

        }


        protected void IbLeadReq_Click(object sender, ImageClickEventArgs e)
        {

            ImageButton thisTextBox = (ImageButton)sender;
            GridViewRow thisGridViewRow = (GridViewRow)thisTextBox.Parent.Parent;
            Label lblid = (Label)thisGridViewRow.FindControl("lblclientid");
            Response.Redirect("M_Add_Lead_Requirement.aspx?LeadID=" + lblid.Text, false);
        }

        protected void IbActionLog_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton thisTextBox = (ImageButton)sender;
            GridViewRow thisGridViewRow = (GridViewRow)thisTextBox.Parent.Parent;
            Label lblid = (Label)thisGridViewRow.FindControl("lblclientid");
            Response.Redirect("M_Action_Log.aspx?LeadID=" + lblid.Text, false);
        }



        protected void lnkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("M_Add_Lead.aspx");
        }

        protected void imgCal_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("M_All_Action_Log_Scheduler.aspx");
        }
    }
}