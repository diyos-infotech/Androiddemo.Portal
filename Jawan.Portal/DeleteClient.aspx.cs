using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using KLTS.Data;
using Jawan.Portal.DAL;
namespace Jawan.Portal
{
    public partial class DeleteClient : System.Web.UI.Page
    {
        AppConfiguration config = new AppConfiguration();
        GridViewExportUtil gve = new GridViewExportUtil();
        private void LoadAllClientsData()
        {
            #region   Begin Code for Calling Stored Procedure  to Retrive Data /Bind To GridView  as on [20-09-2013]
            var SPName = "GetAllClientsData";
            DataTable DtAllClientsInfo = config.ExecuteAdaptorAsyncWithQueryParams(SPName).Result;
            if (DtAllClientsInfo.Rows.Count > 0)
            {
                gvdeleteclient.DataSource = DtAllClientsInfo;
                gvdeleteclient.DataBind();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Show Alert", "alert('There Is No Clients Data')", true);
            }
            #endregion End Code for Calling Stored Procedure to Retrive Data /Bind To GridView   as on [20-09-2013]
        }

        protected void PreviligeUsers(int previligerid)
        {

            switch (previligerid)
            {

                case 1:
                    break;
                case 2:
                    AddClientLink.Visible = true;
                    ModifyClientLink.Visible = false;
                    DeleteClientLink.Visible = false;
                    ContractLink.Visible = false;
                    //ClientAttenDanceLink.Visible = false;
                    Operationlink.Visible = false;
                    BillingLink.Visible = false;
                    MRFLink.Visible = false;

                    CompanyInfoLink.Visible = false;
                    InventoryLink.Visible = false;
                    ReportsLink.Visible = false;
                    SettingsLink.Visible = false;

                    break;

                case 3:

                    AddClientLink.Visible = true;
                    ModifyClientLink.Visible = true;
                    DeleteClientLink.Visible = true;
                    ContractLink.Visible = true;
                    // ClientAttenDanceLink.Visible = true;
                    Operationlink.Visible = true;
                    BillingLink.Visible = false;
                    MRFLink.Visible = false;


                    InventoryLink.Visible = false;
                    ReportsLink.Visible = false;
                    SettingsLink.Visible = false;
                    break;
                case 4:

                    AddClientLink.Visible = true;
                    ModifyClientLink.Visible = true;
                    DeleteClientLink.Visible = true;
                    ContractLink.Visible = true;
                    LicensesLink.Visible = true;
                    ClientAttendanceLink.Visible = false;
                    Operationlink.Visible = true;
                    BillingLink.Visible = false;
                    MRFLink.Visible = false;

                    EmployeesLink.Visible = true;
                    ClientsLink.Visible = true;
                    CompanyInfoLink.Visible = true;
                    InventoryLink.Visible = true;
                    ReportsLink.Visible = true;
                    SettingsLink.Visible = true;
                    break;
                case 5:

                    AddClientLink.Visible = false;
                    ModifyClientLink.Visible = false;
                    DeleteClientLink.Visible = false;
                    ContractLink.Visible = false;
                    // ClientAttenDanceLink.Visible = true;
                    Operationlink.Visible = false;
                    BillingLink.Visible = false;
                    MRFLink.Visible = true;

                    CompanyInfoLink.Visible = false;
                    InventoryLink.Visible = false;
                    ReportsLink.Visible = false;
                    SettingsLink.Visible = false;



                    break;
                case 6:
                    AddClientLink.Visible = false;
                    ModifyClientLink.Visible = false;
                    DeleteClientLink.Visible = false;
                    ContractLink.Visible = false;
                    // ClientAttenDanceLink.Visible = true;
                    Operationlink.Visible = false;
                    BillingLink.Visible = false;
                    MRFLink.Visible = true;

                    CompanyInfoLink.Visible = false;
                    InventoryLink.Visible = false;
                    ReportsLink.Visible = false;
                    SettingsLink.Visible = false;

                    break;
                default:
                    break;


            }
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
                    switch (SqlHelper.Instance.GetCompanyValue())
                    {
                        case 0:// Write Frames Invisible Links
                            break;
                        case 1://Write KLTS Invisible Links
                            ReceiptsLink.Visible = true;
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    Response.Redirect("login.aspx");
                }

                LoadAllClientsData();
            }
        }

        protected void gvdeleteclient_RowDeleting1(object sender, GridViewDeleteEventArgs e)
        {
            Label lblclientid1 = gvdeleteclient.Rows[e.RowIndex].FindControl("lblclientid") as Label;
            try
            {
                string deletequery = " delete from clients where ClientId = '" + lblclientid1.Text + "'";
                int status = config.ExecuteNonQueryWithQueryAsync(deletequery).Result;
                if (status != 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Client deleted successfully');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Record not found');", true);
                }
                LoadAllClientsData();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Client not deleted');", true);
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            #region Begin Code For Check Validation/Become Zero Data  as on  [20-09-2013]
            gvdeleteclient.DataSource = null;
            gvdeleteclient.DataBind();

            if (txtsearch.Text.Trim().Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Show Alert", "alert('Please Enter Either Client ID Nor Client Name');", true);
                return;
            }

            #endregion End Code For Check Validation/Become Zero Data  as on  [20-09-2013]

            #region Begin Code For Declaration/Assign Values as on [20-09-2013]
            Hashtable HTSpParameters = new Hashtable();
            var SPName = "SearchIndClientIfo";
            var SearchedValue = txtsearch.Text;
            HTSpParameters.Add("@ClientidorName", SearchedValue);
            #endregion End  Code For Declaration/Assign Values as on [20-09-2013]

            #region Begin code For Calling Stored Procedure And Retrived Data/Bind To The Gridview  What user Searched    as on [20-09-2013]
            DataTable DtIndClientInfo =config.ExecuteAdaptorAsyncWithParams(SPName, HTSpParameters).Result;
            if (DtIndClientInfo.Rows.Count > 0)
            {
                gvdeleteclient.DataSource = DtIndClientInfo;
                gvdeleteclient.DataBind();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('There Is No Clients.What you are Entered');", true);
            }
            #endregion  End   code For Calling Stored Procedure And Retrived Data/Bind To The Gridview  What user Searched    as on [20-09-2013]

        }

        protected void gvdeleteclient_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvdeleteclient.PageIndex = e.NewPageIndex;
            LoadAllClientsData();
        }
    }
}