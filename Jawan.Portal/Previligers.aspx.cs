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
    public partial class Previligers : System.Web.UI.Page
    {
        DropDownList bind_dropdownlist;
        AppConfiguration config = new AppConfiguration();
        GridViewExportUtil gve = new GridViewExportUtil();
        private void Displaydata()
        {
            Txt_Previliger.Text = "";

            DataTable DtPreviligers = GlobalData.Instance.LoadPreviligers();
            if (DtPreviligers.Rows.Count > 0)
            {
                gvpreviligers.DataSource = DtPreviligers;
                gvpreviligers.DataBind();


                foreach (GridViewRow grdRow in gvpreviligers.Rows)
                {
                    bind_dropdownlist = (DropDownList)(gvpreviligers.Rows[grdRow.RowIndex].Cells[0].FindControl("ddlPriority"));

                    string sqlselect = "select PreviligerId from Previligers";
                    DataTable dtPriority =config.ExecuteAdaptorAsyncWithQueryParams(sqlselect).Result;


                    bind_dropdownlist.DataValueField = "PreviligerId";
                    bind_dropdownlist.DataTextField = "PreviligerId";
                    bind_dropdownlist.DataSource = dtPriority;
                    bind_dropdownlist.DataBind();
                    bind_dropdownlist.Items.Insert(0, "-Select-");

                    Label lblpreviligerid = (Label)(gvpreviligers.Rows[grdRow.RowIndex].Cells[0].FindControl("lblpreviligerid"));
                    string sqlqry = "select Priority from   Previligers where PreviligerId = " + lblpreviligerid.Text;
                    DataTable dtPriority1 = config.ExecuteAdaptorAsyncWithQueryParams(sqlqry).Result;

                    if (dtPriority1.Rows.Count > 0)
                    {
                        bind_dropdownlist.SelectedValue = dtPriority1.Rows[0][0].ToString();

                    }
                }

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Previliger Names Are Not Avialable');", true);
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



        protected void Btn_Previliger_Click(object sender, EventArgs e)
        {
            try
            {
                #region Begin Code For  Validations   as [12-10-2013]
                if (Txt_Previliger.Text.Trim().Length == 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Please Enter Previliger Name.');", true);
                    return;
                }
                #endregion Begin Code For  Validations as [12-10-2013]

                #region Begin Code For Variable Declaration as [12-10-2013]
                var PreviligerName = string.Empty;
                var IRecordStatus = 0;

                #endregion Begin Code For Variable Declaration as [12-10-2013]

                #region Begin Code For  Assign Values to Variable  as [12-10-2013]
                PreviligerName = Txt_Previliger.Text.Trim().ToUpper();
                #endregion Begin Code For Assign Values to Variable as [12-10-2013]


                #region  Begin Code For Stored Procedure Parameters  as on [12-10-2013]
                Hashtable HtSPParameters = new Hashtable();
                var ProcedureName = "AddPreviligers";
                #endregion End Code For Stored Procedure Parameters  as on [12-10-2013]

                #region  Begin Code For Assign Values to the Stored Procedure Parameters as on [12-10-2013]
                HtSPParameters.Add("@PreviligerName", PreviligerName);
                #endregion  End  Code For Assign Values to the Stored Procedure Parameters as on [12-10-2013]

                #region  Begin Code For Calling Stored Procedure As on [12-10-2013]
                IRecordStatus =config.ExecuteNonQueryParamsAsync(ProcedureName, HtSPParameters).Result;
                #endregion  End Code For Calling Stored Procedure As on [12-10-2013]

                #region  Begin Code For Display Status Of the Record as on [12-10-2013]
                if (IRecordStatus > 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Previliger Name Added SucessFully.');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Previliger Name Not  Added.Because  The Name Already Exist. NOTE:Previliger Names Are UNIQUE');", true);
                }
                #endregion  End Code For Display Status Of the Record as on [12-10-2013]

                #region  Begin Code For Re-Call All the Departments As on [12-10-2013]
                Displaydata();
                #endregion End Code For Re-Call All the Departments As on [12-10-2013]

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert(' Please Contact Your Admin..');", true);
                return;
            }

        }

        protected void gvpreviligers_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

            try
            {

                #region  Begin  Code  for  Retrive  Data From  Gridview as on [14-10-2013]
                Label prid = gvpreviligers.Rows[e.RowIndex].FindControl("lblpreviligerid") as Label;
                TextBox prname = gvpreviligers.Rows[e.RowIndex].FindControl("txtpreviligerName") as TextBox;

                DropDownList ddlPrioriy = gvpreviligers.Rows[e.RowIndex].FindControl("ddlPriority") as DropDownList;

                #endregion End  Code  for  Retrive  Data From  Gridview as on [14-10-2013]


                #region  Begin  Code  for  validaton as on [14-10-2013]
                if (prname.Text.Trim().Length == 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Please Enter the Previliger  Name');", true);
                    return;
                }
                #endregion End  Code  for  validaton as on [14-10-2013]


                #region  Begin  Code  for  Variable  Declaration as on [14-10-2013]
                var Previligername = string.Empty;
                var Previligerid = string.Empty;
                var ProcedureName = string.Empty;
                var IRecordStatus = 0;

                var Priority = string.Empty;
                if (ddlPrioriy.SelectedIndex > 0)
                {
                    Priority = ddlPrioriy.SelectedValue;
                }
                else
                {
                    Priority = "0";
                }

                Hashtable HtSPParameters = new Hashtable();
                #endregion End  Code  for  Variable  Declaration as on [14-10-2013]

                #region  Begin  Code  for  Assign Values to  Variables as on [14-10-2013]
                Previligername = prname.Text.Trim().ToUpper();
                Previligerid = prid.Text;
                ProcedureName = "ModifyPreviligers";
                #endregion End  Code  for  Assign Values to  Variables as on [14-10-2013]

                #region  Begin  Code  for  Assign Values to  SP Parameters as on [14-10-2013]
                HtSPParameters.Add("@PreviligerName", Previligername);
                HtSPParameters.Add("@PreviligerId", Previligerid);
                HtSPParameters.Add("@priority", Priority);
                #endregion End  Code  for  Assign Values to  SP Parameters as on [14-10-2013]

                #region  Begin Code For Calling Stored Procedure As on [14-10-2013]
                IRecordStatus = config.ExecuteNonQueryParamsAsync(ProcedureName, HtSPParameters).Result;
                #endregion  End Code For Calling Stored Procedure As on [14-10-2013]

                #region  Begin Code For Display Status Of the Record as on [14-10-2013]
                if (IRecordStatus > 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Previliger Name  Updated  SucessFully.');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Previliger Name Not  Updated.Because  The Name Already Exist. NOTE:Previliger Names Are UNIQUE');", true);
                }
                #endregion  End Code For Display Status Of the Record as on [14-10-2013]

                #region  Begin Code For Re-Call All the Departments As on [14-10-2013]
                gvpreviligers.EditIndex = -1;
                Displaydata();
                #endregion End Code For Re-Call All the Departments As on [14-10-2013]

            }

            catch (Exception ex)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Please Contact Admin.');", true);
                return;
            }


        }

        protected void gvpreviligers_RowEditing(object sender, GridViewEditEventArgs e)
        {

            gvpreviligers.EditIndex = e.NewEditIndex;
            Displaydata();
        }

        protected void gvpreviligers_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvpreviligers.EditIndex = -1;
            Displaydata();
        }

        protected void gvpreviligers_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvpreviligers.PageIndex = e.NewPageIndex;
            Displaydata();
        }
    }
}