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
    public partial class DeleteEmployee : System.Web.UI.Page
    {
        AppConfiguration config = new AppConfiguration();
        GridViewExportUtil gve = new GridViewExportUtil();
        string EmpIDPrefix = "";
        public void DisplayData()
        {
            DataTable DtEmployees = GlobalData.Instance.LoadActiveEmployees(EmpIDPrefix);
            if (DtEmployees.Rows.Count > 0)
            {
                gvdeleteemployee.DataSource = DtEmployees;
                gvdeleteemployee.DataBind();
            }
            else
            {
                gvdeleteemployee.DataSource = null;
                gvdeleteemployee.DataBind();
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
                    AddEmployeeLink.Visible = true;
                    ModifyEmployeeLink.Visible = true;
                    DeleteEmployeeLink.Visible = true;
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

                    AddEmployeeLink.Visible = true;
                    ModifyEmployeeLink.Visible = false;
                    DeleteEmployeeLink.Visible = false;
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
                    AddEmployeeLink.Visible = false;
                    ModifyEmployeeLink.Visible = false;
                    DeleteEmployeeLink.Visible = false;
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
                    AddEmployeeLink.Visible = false;
                    ModifyEmployeeLink.Visible = false;
                    DeleteEmployeeLink.Visible = false;
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
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {

                #region  Begin Code For Check Validations as on [18-10-2013]
                if (txtsearch.Text.Trim().Length == 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('Please Enter The Employee ID/Name. Whatever You Want To Search');", true);
                    return;
                }
                #endregion  End  Code For Check Validations as on [18-10-2013]

                #region  Begin Code For Variable Declaration as on [18-10-2013]
                Hashtable HtSearchEmployee = new Hashtable();
                DataTable DtSearchEmployee = null;
                var SPName = "";
                var SearchedEmpIdOrName = "";
                #endregion  End  Code For Variable Declaration  as on [18-10-2013]

                #region  Begin Code For Assign Values to The Variables as on [18-10-2013]
                SearchedEmpIdOrName = txtsearch.Text;
                SPName = "GetEmployeesSearchBase";
                #endregion  End  Code For Assign Values to The Variables as on [18-10-2013]

                #region  Begin Code For SP Parameters as on [18-10-2013]
                HtSearchEmployee.Add("@EmpidorName", SearchedEmpIdOrName);
                HtSearchEmployee.Add("@empidprefix", EmpIDPrefix);
                #endregion  End  Code For SP Parameters as on [18-10-2013]

                #region  Begin Code For Calling Stored Procedure as on [18-10-2013]
                DtSearchEmployee = config.ExecuteAdaptorAsyncWithParams(SPName, HtSearchEmployee).Result;
                #endregion  End  Code For Calling Stored Procedure as on [18-10-2013]

                #region  Begin code For Assing Data to Gridview Whatever Retrived As on[18-10-2010]

                if (DtSearchEmployee.Rows.Count > 0)
                {
                    gvdeleteemployee.DataSource = DtSearchEmployee;
                    gvdeleteemployee.DataBind();
                }
                else
                {
                    gvdeleteemployee.DataSource = null;
                    gvdeleteemployee.DataBind();
                    ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('There are no Employees ');", true);
                }

                #endregion End  code For Assing Data to Gridview Whatever Retrived As on[18-10-2010]
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Please Contact Admin');", true);
            }


        }

        protected void gvdeleteemployee_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvdeleteemployee.PageIndex = e.NewPageIndex;
            DisplayData();
        }
    }
}