using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using KLTS.Data;
using Jawan.Portal.DAL;
namespace Jawan.Portal
{
    public partial class ESIBranches : System.Web.UI.Page
    {
        AppConfiguration config = new AppConfiguration();
        GridViewExportUtil gve = new GridViewExportUtil();
        private void Displaydata()
        {
            txtEsibranchname.Text = "";
            DataTable DtEsibranches = GlobalData.Instance.LoadEsibranches();
            if (DtEsibranches.Rows.Count > 0)
            {
                gvEsibranches.DataSource = DtEsibranches;
                gvEsibranches.DataBind();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('ESI Branches Are Not Avialable');", true);
                return;
            }

        }

        protected void PreviligeUsers(int previligerid)
        {

            switch (previligerid)
            {

                case 1:
                    break;
                case 2:
                    EmployeesLink.Visible = false;
                    ClientsLink.Visible = false;
                    CompanyInfoLink.Visible = false;
                    InventoryLink.Visible = false;
                    ReportsLink.Visible = false;
                    SettingsLink.Visible = false;

                    break;

                case 3:
                    EmployeesLink.Visible = false;
                    ClientsLink.Visible = false;
                    CompanyInfoLink.Visible = false;
                    InventoryLink.Visible = false;
                    ReportsLink.Visible = false;
                    SettingsLink.Visible = false;

                    break;

                case 4:
                    EmployeesLink.Visible = true;
                    ClientsLink.Visible = true;
                    CompanyInfoLink.Visible = true;
                    InventoryLink.Visible = true;
                    ReportsLink.Visible = true;
                    SettingsLink.Visible = true;


                    break;
                case 5:
                    SettingsLink.Visible = true;


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

                    break;
                default:
                    break;


            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            try
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

                    Displaydata();
                }

            }
            catch (Exception ex)
            {

            }
        }

        protected void btnAddesibranch_Click(object sender, EventArgs e)
        {
            try
            {

                var EsiBranchName = string.Empty;
                var IRecordStatus = 0;
                Hashtable HtSPParameters = new Hashtable();
                var ProcedureName = "";

                if (txtEsibranchname.Text.Trim().Length == 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Please Enter ESI Branch Name.');", true);
                    return;
                }

                EsiBranchName = txtEsibranchname.Text.Trim().ToUpper();

                ProcedureName = "AddEsibranches";

                HtSPParameters.Add("@Esibranchname", EsiBranchName);

                IRecordStatus = config.ExecuteNonQueryParamsAsync(ProcedureName, HtSPParameters).Result;

                if (IRecordStatus > 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('ESI Branch Name Added SucessFully.');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('ESI Branch Not  Added.Because  The Name Already Exist.');", true);
                }

                Displaydata();


            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert(' Please Contact Your Admin..');", true);
                return;
            }

        }

        protected void gvEsibranches_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                var ESIbranchname = string.Empty;
                var ProcedureName = string.Empty;
                var IRecordStatus = 0;
                Hashtable HtSPParameters = new Hashtable();

                Label Esibranchid = gvEsibranches.Rows[e.RowIndex].FindControl("lblBranchid") as Label;
                TextBox Esibranchname = gvEsibranches.Rows[e.RowIndex].FindControl("txtEsibranchname") as TextBox;


                if (Esibranchname.Text.Trim().Length == 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Please Enter the ESI Branch  Name');", true);
                    return;
                }


                ESIbranchname = Esibranchname.Text.Trim().ToUpper();
                ProcedureName = "ModifyEsibranches";

                HtSPParameters.Add("@Esibranchname", ESIbranchname);
                HtSPParameters.Add("@Esibranchid", Esibranchid.Text);

                IRecordStatus = config.ExecuteNonQueryParamsAsync(ProcedureName, HtSPParameters).Result;


                if (IRecordStatus > 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('ESI Branch Name  Updated  SucessFully.');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('ESI Branch Name Not  Updated.Because  The Name Already Exist. NOTE:Bank Names Are UNIQUE');", true);
                }

                gvEsibranches.EditIndex = -1;
                Displaydata();


            }

            catch (Exception ex)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Please Contact Admin.');", true);
                return;
            }

        }

        protected void gvEsibranches_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvEsibranches.EditIndex = e.NewEditIndex;
            Displaydata();
        }
        protected void gvEsibranches_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvEsibranches.EditIndex = -1;
            Displaydata();
        }
        protected void gvEsibranches_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvEsibranches.PageIndex = e.NewPageIndex;
            Displaydata();
        }
    }
}