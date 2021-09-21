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
    public partial class AssingingWorkers : System.Web.UI.Page
    {
        AppConfiguration config = new AppConfiguration();
        GridViewExportUtil gve = new GridViewExportUtil();
        DataTable dt;
        string EmpIDPrefix = "";
        string CmpIDPrefix = "";
        string Elength = "";
        string Clength = "";
        protected void Page_Load(object sender, EventArgs e)
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
                // displaydata();
                FillOpManagersIds();
                FillOpManagersNames();
            }
            lblresult.Text = "";
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

                    AddEmployeeLink.Visible = true;
                    ModifyEmployeeLink.Visible = true;
                    DeleteEmployeeLink.Visible = true;
                    AssigningWorkerLink.Visible = true;
                    LoanLink.Visible = true;
                    PaymentLink.Visible = true;
                    //TrainingEmployeeLink.Visible = true;
                    //JobLeavingReasonsLink.Visible = true;
                    AttendanceLink.Visible = true;

                    CompanyInfoLink.Visible = true;
                    ReportsLink.Visible = false;
                    InventoryLink.Visible = false;
                    SettingsLink.Visible = false;



                    break;

                case 4:

                    AddEmployeeLink.Visible = false;
                    ModifyEmployeeLink.Visible = false;
                    DeleteEmployeeLink.Visible = false;
                    AssigningWorkerLink.Visible = false;
                    LoanLink.Visible = true;
                    PaymentLink.Visible = true;
                    //TrainingEmployeeLink.Visible = false;
                    //JobLeavingReasonsLink.Visible = false;
                    AttendanceLink.Visible = false;

                    CompanyInfoLink.Visible = true;
                    ReportsLink.Visible = false;
                    InventoryLink.Visible = false;
                    SettingsLink.Visible = false;





                    break;
                case 5:

                    AddEmployeeLink.Visible = false;
                    ModifyEmployeeLink.Visible = false;
                    DeleteEmployeeLink.Visible = false;
                    AssigningWorkerLink.Visible = false;
                    LoanLink.Visible = true;
                    PaymentLink.Visible = true;
                    //TrainingEmployeeLink.Visible = false;
                    //JobLeavingReasonsLink.Visible = false;
                    AttendanceLink.Visible = true;

                    CompanyInfoLink.Visible = true;
                    ReportsLink.Visible = false;
                    InventoryLink.Visible = false;
                    SettingsLink.Visible = false;
                    break;
                case 6:

                    AddEmployeeLink.Visible = false;
                    ModifyEmployeeLink.Visible = false;
                    DeleteEmployeeLink.Visible = false;
                    AssigningWorkerLink.Visible = false;
                    LoanLink.Visible = false;
                    PaymentLink.Visible = false;
                    //TrainingEmployeeLink.Visible = false;
                    //JobLeavingReasonsLink.Visible = false;
                    AttendanceLink.Visible = true;

                    CompanyInfoLink.Visible = false;
                    ReportsLink.Visible = false;
                    InventoryLink.Visible = false;
                    SettingsLink.Visible = false;






                    break;
                default:
                    break;


            }
        }

        protected void GetWebConfigdata()
        {
            EmpIDPrefix = GlobalData.Instance.GetEmployeeIDPrefix();
            Elength = (EmpIDPrefix.Trim().Length + 1).ToString();
            CmpIDPrefix = GlobalData.Instance.GetClientIDPrefix();
            Clength = (CmpIDPrefix.Trim().Length + 1).ToString();
        }

        protected void FillOpManagersIds()
        {

            DataTable DtOpManagerIds = GlobalData.Instance.LoadOpManagerIds();
            if (DtOpManagerIds.Rows.Count > 0)
            {
                ddloperationalmanager.DataValueField = "empid";
                ddloperationalmanager.DataTextField = "empid";
                ddloperationalmanager.DataSource = DtOpManagerIds;
                ddloperationalmanager.DataBind();
            }
            ddloperationalmanager.Items.Insert(0, "-Select-");
        }

        protected void FillOpManagersNames()
        {

            DataTable DtOpManagerNames = GlobalData.Instance.LoadOpManagerNames();
            if (DtOpManagerNames.Rows.Count > 0)
            {
                ddloperationalmanagername.DataValueField = "empid";
                ddloperationalmanagername.DataTextField = "FullName";
                ddloperationalmanagername.DataSource = DtOpManagerNames;
                ddloperationalmanagername.DataBind();
            }
            ddloperationalmanagername.Items.Insert(0, "-Select-");
        }

        protected void FillOpManagersId()
        {

            if (ddloperationalmanagername.SelectedIndex > 0)
            {
                ddloperationalmanager.SelectedValue = ddloperationalmanagername.SelectedValue;
            }
            else
            {
                ddloperationalmanager.SelectedIndex = 0;
            }

        }

        protected void FillOpManagersName()
        {
            if (ddloperationalmanager.SelectedIndex > 0)
            {
                ddloperationalmanagername.SelectedValue = ddloperationalmanager.SelectedValue;
            }
            else
            {
                ddloperationalmanagername.SelectedIndex = 0;
            }
        }

        private void displaydata()
        {
            string opmDesig = GlobalData.Instance.GetOPMDesig();
            string selectquery = " select EmpId,(ISnull(Empfname,'')+' '+ Isnull(empmname,'')+' '+ isnull(Emplname,'') ) AS FullName,EmpDesgn from  EmpDetails where EmpDesgn <> '" + opmDesig +
                "' AND (OPMId IS NULL OR OPMId='') Order By right(empid,4)";
            dt = config.ExecuteAdaptorAsyncWithQueryParams(selectquery).Result;
            gvunemployeement.DataSource = dt;
            gvunemployeement.DataBind();

            if (ddloperationalmanager.SelectedIndex > 0)
            {
                selectquery = " select EmpId,(ISnull(Empfname,'')+' '+ Isnull(empmname,'')+' '+ isnull(Emplname,'') ) AS FullName,EmpDesgn from  EmpDetails where EmpDesgn <> '" + opmDesig + "' AND OPMId ='" +
                    ddloperationalmanager.SelectedValue + "' Order By right(empid,4) ";
                dt = config.ExecuteAdaptorAsyncWithQueryParams(selectquery).Result;
                gvemployeement.DataSource = dt;
                gvemployeement.DataBind();
            }
            else
            {
                DataTable dTable = new DataTable();
                gvemployeement.DataSource = dTable;
                gvemployeement.DataBind();
            }
            lblresult.Text = "";
        }

        protected void btnleft_click(object sender, EventArgs e)
        {
            if (ddloperationalmanager.SelectedIndex > 0)
            {
                for (int i = 0; i < gvunemployeement.Rows.Count; i++)
                {
                    CheckBox check = gvunemployeement.Rows[i].FindControl("checkempid") as CheckBox;
                    if (check.Checked)
                    {
                        Label label = gvunemployeement.Rows[i].FindControl("lblclientid") as Label;
                        if (label != null)
                        {
                            if (label.Text.Length > 0)
                            {
                                string sqlQry = "Update EmpDetails set OPMId='" + ddloperationalmanager.SelectedValue + "' where EmpId ='" + label.Text.Trim() + "'";
                                int status = config.ExecuteNonQueryWithQueryAsync(sqlQry).Result;
                            }
                        }
                    }
                }
                displaydata();
            }
            else
            {
                lblresult.Text = "Please Select Operational Manager";
            }
        }

        protected void btnright_click(object sender, EventArgs e)
        {
            if (ddloperationalmanager.SelectedIndex > 0)
            {
                for (int i = 0; i < gvemployeement.Rows.Count; i++)
                {
                    GridViewRow delrow = gvemployeement.Rows[i];
                    CheckBox check = gvemployeement.Rows[i].FindControl("checkdisselect") as CheckBox;
                    if (check.Checked)
                    {
                        Label label = gvemployeement.Rows[i].FindControl("lblclientid") as Label;
                        if (label != null)
                        {
                            if (label.Text.Length > 0)
                            {
                                string sqlQry = "Update EmpDetails set OPMId='" + null + "' where EmpId ='" + label.Text.Trim() + "'";
                                int status = config.ExecuteNonQueryWithQueryAsync(sqlQry).Result;
                            }
                        }
                    }
                }
                displaydata();
            }
            else
            {
                lblresult.Text = "Please Select Operational Manager";
            }
        }

        protected void ddloperationalmanager_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddloperationalmanager.SelectedIndex > 0)
            {
                FillOpManagersName();
                displaydata();
            }
            else
            {
                ddloperationalmanagername.SelectedIndex = 0;
                gvemployeement.DataSource = null;
                gvemployeement.DataBind();
            }
        }

        protected void ddloperationalmanagername_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddloperationalmanagername.SelectedIndex > 0)
            {
                FillOpManagersId();
                displaydata();
            }
            else
            {
                ddloperationalmanager.SelectedIndex = 0;
                gvemployeement.DataSource = null;
                gvemployeement.DataBind();
            }
        }
    }
}