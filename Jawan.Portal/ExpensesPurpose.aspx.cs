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
    public partial class ExpensesPurpose : System.Web.UI.Page
    {
        AppConfiguration config = new AppConfiguration();
        GridViewExportUtil gve = new GridViewExportUtil();
        private void Displaydata()
        {
            Txt_ExpensesPurpose.Text = "";
            DataTable DtExpensesPurposeNames = GlobalData.Instance.LoadExpensesPurpose();
            if (DtExpensesPurposeNames.Rows.Count > 0)
            {
                Gv_Expenses_Purpose.DataSource = DtExpensesPurposeNames;
                Gv_Expenses_Purpose.DataBind();

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Expenses Purpose Names Are Not Avialable');", true);
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


        protected void Btn_ExpensesPurpose_Click(object sender, EventArgs e)
        {
            try
            {
                #region Begin Code For Validation as on [13-01-2013]
                if (Txt_ExpensesPurpose.Text.Trim().Length == 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Please Enter The Expense Purpose');", true);
                    return;
                }
                #endregion End Code For Validation as on [13-01-2013]

                #region Begin Code For Variable Declaration As on [13-01-2013]
                Hashtable HtExpensesPurpose = new Hashtable();
                var ExpensesPurposeName = "";
                var ProcedureName = "";
                var IRecordStatus = 0;
                #endregion End Code For Variable Declaration As on [13-01-2013]

                #region Begin Code  For Assgin Values To the Variables as on [13-01-2013]
                ProcedureName = "AMExpensesPurpose";
                ExpensesPurposeName = Txt_ExpensesPurpose.Text.ToUpper();
                #endregion End Code  For Assgin Values To the Variables as on [13-01-2013]

                #region Begin Code For Bind Values To the Hashtable as on [13-01-2013]
                HtExpensesPurpose.Add("@Name", ExpensesPurposeName);
                #endregion End Code For Bind Values To the Hashtable as on [13-01-2013]

                #region Begin Code For Calling Stored Procedure as on [13-01-2013]
                IRecordStatus =config.ExecuteNonQueryParamsAsync(ProcedureName, HtExpensesPurpose).Result;
                #endregion End Code For Calling Stored Procedure as on [13-01-2013]

                #region Begin code For Resulted Messages as on [13-01-2013]
                if (IRecordStatus > 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Record Added SucessFully.');", true);
                    Displaydata();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Record Not Added.');", true);
                }
                #endregion End code For Resulted Messages as on [13-01-2013]

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Your Session Time Expired');", true);
                Response.Redirect("login.aspx");
            }

        }

        protected void Gv_Expenses_Purpose_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

            try
            {


                #region Begin Code For Validation as on [13-01-2013]

                TextBox Gv_Txt_ExpensesPurpose = Gv_Expenses_Purpose.Rows[e.RowIndex].FindControl("txt_Expenses_Purpose_") as TextBox;
                Label Gv_lbl_Expenses_Purpose_id = Gv_Expenses_Purpose.Rows[e.RowIndex].FindControl("lbl_Expenses_Purpose_id") as Label;

                if (Gv_Txt_ExpensesPurpose.Text.Trim().Length == 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Please Enter The Expense Purpose');", true);
                    return;
                }
                #endregion End Code For Validation as on [13-01-2013]

                #region Begin Code For Variable Declaration As on [13-01-2013]
                Hashtable HtExpensesPurpose = new Hashtable();
                var ExpensesPurposeName = "";
                var ProcedureName = "";
                var IRecordStatus = 0;
                var ID = "";
                #endregion End Code For Variable Declaration As on [13-01-2013]

                #region Begin Code  For Assgin Values To the Variables as on [13-01-2013]
                ProcedureName = "AMExpensesPurpose";
                ExpensesPurposeName = Gv_Txt_ExpensesPurpose.Text.ToUpper();
                ID = Gv_lbl_Expenses_Purpose_id.Text;
                #endregion End Code  For Assgin Values To the Variables as on [13-01-2013]

                #region Begin Code For Bind Values To the Hashtable as on [13-01-2013]
                HtExpensesPurpose.Add("@Name", ExpensesPurposeName);
                HtExpensesPurpose.Add("@ID", ID);
                #endregion End Code For Bind Values To the Hashtable as on [13-01-2013]

                #region Begin Code For Calling Stored Procedure as on [13-01-2013]
                IRecordStatus =config.ExecuteNonQueryParamsAsync(ProcedureName, HtExpensesPurpose).Result;
                #endregion End Code For Calling Stored Procedure as on [13-01-2013]

                #region Begin code For Resulted Messages as on [13-01-2013]
                if (IRecordStatus > 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Record Modified SucessFully.');", true);
                    Gv_Expenses_Purpose.EditIndex = -1;
                    Displaydata();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Record Not Modify.');", true);
                }
                #endregion End code For Resulted Messages as on [13-01-2013]

            }

            catch (Exception ex)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Your Session Time Expired');", true);
                Response.Redirect("login.aspx");
            }


        }

        protected void Gv_Expenses_Purpose_RowEditing(object sender, GridViewEditEventArgs e)
        {

            Gv_Expenses_Purpose.EditIndex = e.NewEditIndex;
            Displaydata();
        }

        protected void Gv_Expenses_Purpose_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            Gv_Expenses_Purpose.EditIndex = -1;
            Displaydata();
        }

        protected void Gv_Expenses_Purpose_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Gv_Expenses_Purpose.PageIndex = e.NewPageIndex;
            Displaydata();
        }
    }
}