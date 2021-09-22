using System;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;
using KLTS.Data;
using System.Data;
using Jawan.Portal.DAL;

namespace Jawan.Portal
{
    public partial class LeaveSetUpGrid : System.Web.UI.Page
    {
        AppConfiguration config = new AppConfiguration();
        GridViewExportUtil gve = new GridViewExportUtil();
        string CmpIDPrefix = "";
        string BranchID = "";
        string EmpIDPrefix = "";

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
                    Get_LeaveSetUpGridData();
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void GetWebConfigdata()
        {
            CmpIDPrefix = Session["CmpIDPrefix"].ToString();
            BranchID = Session["BranchID"].ToString();
        }

        public void Get_LeaveSetUpGridData()
        {
            GvLeaveSetUp.DataSource = null;
            GvLeaveSetUp.DataBind();
            var SP_LeaveSetUpGrid = string.Empty;

            var SearchEmp = string.Empty;

            //if (txtsearch.Text.Length > 0)
            //{
            //    SearchEmp = txtsearch.Text;
            //}
            //else
            //{
            //    SearchEmp = string.Empty;
            //}


            Hashtable HtGvLeaveSetUpGrid = new Hashtable();

            SP_LeaveSetUpGrid = "GetLeaveSetUpFroGrid";
            //HtGvLeaveSetUpGrid.Add("@EmpID", SearchEmp);
            DataTable dtBranch = GlobalData.Instance.LoadBranchOnUserID(BranchID);

            //HtGvLeaveSetUpGrid.Add("@Branch", dtBranch);

            DataTable DtGetGvData = config.ExecuteAdaptorAsyncWithParams(SP_LeaveSetUpGrid, HtGvLeaveSetUpGrid).Result;

            if (DtGetGvData.Rows.Count > 0)
            {
                GvLeaveSetUp.DataSource = DtGetGvData;
                GvLeaveSetUp.DataBind();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('The Employee Details Are Not Avaialable');", true);
            }

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                //if (txtsearch.Text.Trim().Length == 0)
                //{
                //    lblMsg.Text = "Please Enter The Employee ID/Name. Whatever You Want To Search";
                //    return;
                //}
                //else
                //{
                Get_LeaveSetUpGridData();
                //}
            }
            catch (Exception ex)
            {
                lblMsg.Text = "Your Session Expired. Please Login";
            }

        }

        protected void lbtn_Edit_Click(object sender, EventArgs e)
        {
            try
            {
                ImageButton thisTextBox = (ImageButton)sender;
                GridViewRow thisGridViewRow = (GridViewRow)thisTextBox.Parent.Parent;
                Label lblleaveTypeId = (Label)thisGridViewRow.FindControl("lblleaveTypeId");
                Response.Redirect("ModifyEmpLeaveSetUp.aspx?LeaveTypeId=" + lblleaveTypeId.Text, false);
            }
            catch (Exception ex)
            {

            }

        }

        protected void GvLeaveSetUp_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GvLeaveSetUp.PageIndex = e.NewPageIndex;
            Get_LeaveSetUpGridData();
        }
    }
}