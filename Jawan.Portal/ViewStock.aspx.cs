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
    public partial class ViewStock : System.Web.UI.Page
    {
        DataTable dt;
        DataTable dtname;
        AppConfiguration config = new AppConfiguration();
        GridViewExportUtil gve = new GridViewExportUtil();
        string EmpIDPrefix = "";
        string CmpIDPrefix = "";
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                GetWebConfigdata();
                if (!IsPostBack)
                {
                    if (Session["UserId"] != null && Session["AccessLevel"] != null)
                    {
                        //PreviligeUsers(Convert.ToInt32(Session["AccessLevel"]));
                        string PID = Session["AccessLevel"].ToString();
                        //PreviligeUsers(PID);
                        lblDisplayUser.Text = Session["UserId"].ToString();
                        lblcname.Text = SqlHelper.Instance.GetCompanyname();
                    }
                    else
                    {
                        Response.Redirect("login.aspx");
                    }

                    LoadAllinventoryItems();
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void LoadAllinventoryItems()
        {

            #region  Begin Variable Declaration
            string SPName = "";
            Hashtable HTListOfinventoryItems = new Hashtable();
            DataTable DtListOfinventoryItems = null;
            #endregion End Variable Declaration

            #region  Begin Assign Values To Variable
            SPName = "ReportForListOfItms";
            #endregion End Assign Values To Variable

            #region Begin Pass values to The Hash Table
            HTListOfinventoryItems.Add("@Clintidprefix", CmpIDPrefix);
            #endregion End Pass values to The Hash Table

            #region Begin Calling Stored Procedure
            DtListOfinventoryItems = config.ExecuteAdaptorAsyncWithParams(SPName, HTListOfinventoryItems).Result;
            #endregion End Calling Stored Procedure

            #region Begin Assign Data To Gridview
            if (DtListOfinventoryItems.Rows.Count > 0)
            {
                gvstock.DataSource = DtListOfinventoryItems;
                gvstock.DataBind();
            }

            #endregion  End Assign Data To Gridview

        }

        protected void GetWebConfigdata()
        {

            EmpIDPrefix = Session["EmpIDPrefix"].ToString();
            CmpIDPrefix = Session["CmpIDPrefix"].ToString();
        }

        protected void gvstock_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvstock.PageIndex = e.NewPageIndex;
            LoadAllinventoryItems();
        }

        protected void btnsave_Click(object sender, EventArgs e)
        {
            gvstock.DataSource = null;
            gvstock.DataBind();
            lblresult.Text = "";

            #region  Begin Variable Declaration
            string SPName = "";
            Hashtable HTListOfinventoryItems = new Hashtable();
            DataTable DtListOfinventoryItems = null;
            #endregion End Variable Declaration

            #region  Begin Assign Values To Variable
            SPName = "SearchItemIdOrName";
            #endregion End Assign Values To Variable

            #region Begin Pass values to The Hash Table
            HTListOfinventoryItems.Add("@Clintidprefix", CmpIDPrefix);
            HTListOfinventoryItems.Add("@@ItemidAndName", txtitemname.Text);
            #endregion End Pass values to The Hash Table

            #region Begin Calling Stored Procedure
            DtListOfinventoryItems = config.ExecuteAdaptorAsyncWithParams(SPName, HTListOfinventoryItems).Result;
            #endregion End Calling Stored Procedure

            #region Begin Assign Data To Gridview
            if (DtListOfinventoryItems.Rows.Count > 0)
            {
                gvstock.DataSource = DtListOfinventoryItems;
                gvstock.DataBind();
            }
            else
            {
                gvstock.DataSource = null;
                gvstock.DataBind();
                lblresult.Text = "Item Was not found.";
            }

            #endregion  End Assign Data To Gridview


        }

    }
}