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
    public partial class AssigningClients : System.Web.UI.Page
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
                //   displaydata();
                if (Session["UserId"] != null && Session["AccessLevel"] != null)
                {
                    lblDisplayUser.Text = Session["UserId"].ToString();
                    PreviligeUsers(Convert.ToInt32(Session["AccessLevel"]));
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

                FillOpManagers();
                FillOpManagersNames();
            }
        }

        protected void GetWebConfigdata()
        {
            EmpIDPrefix = GlobalData.Instance.GetEmployeeIDPrefix();
            Elength = (EmpIDPrefix.Trim().Length + 1).ToString();
            CmpIDPrefix = GlobalData.Instance.GetClientIDPrefix();
            Clength = (CmpIDPrefix.Trim().Length + 1).ToString();
        }

        protected void FillOpManagers()
        {
            ddloperationalmanager.Items.Clear();
            ddloperationalmanager.Items.Add("--Select--");

            string opmDesig = GlobalData.Instance.GetOPMDesig();
            //string selectquery = " select EmpId from  EmpDetails where EmpDesgn = 'Op. Manager' Order By (cast(substring(Empid," + Elength + ", 6) as int))";
            string selectquery = " select EmpId from  EmpDetails where EmpDesgn = '" + opmDesig + "' Order By (cast(substring(Empid," + Elength + ", 6) as int))";
            dt = config.ExecuteAdaptorAsyncWithQueryParams(selectquery).Result;
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ddloperationalmanager.Items.Add(dt.Rows[i][0].ToString());
                }
            }
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
                    //ClientAttenDanceLink.Visible = true;
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
                    //ClientAttenDanceLink.Visible = true;
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
                    //ClientAttenDanceLink.Visible = true;
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

        private void displaydata()
        {
            string selectquery = " select ClientId,ClientName,ClientSegment from " +
                "  Clients where OurContactPersonId IS NULL OR OurContactPersonId='' " +
                " Order By (cast(substring(clientid," + Clength + ", 4) as int))";
            dt = config.ExecuteAdaptorAsyncWithQueryParams(selectquery).Result;
            gvunasclient.DataSource = dt;
            gvunasclient.DataBind();
            //gvasclient.DataSource = dt;
            //gvasclient.DataBind();
            if (ddloperationalmanager.SelectedIndex > 0)
            {
                selectquery = " select ClientId,ClientName,ClientSegment from  Clients where OurContactPersonId ='" + ddloperationalmanager.SelectedValue +
                    "' Order By (cast(substring(clientid," + Clength + ", 4) as int))";
                dt = config.ExecuteAdaptorAsyncWithQueryParams(selectquery).Result;
                gvasclient.DataSource = dt;
                gvasclient.DataBind();
            }
            else
            {
                DataTable dTable = new DataTable();
                gvasclient.DataSource = dTable;
                gvasclient.DataBind();
            }
            lblresult.Text = "";
        }
        int i = 0;
        protected void btnleft_Click(object sender, EventArgs e)
        {
            if (ddloperationalmanager.SelectedIndex > 0)
            {
                for (int i = 0; i < gvunasclient.Rows.Count; i++)
                {
                    CheckBox check = gvunasclient.Rows[i].FindControl("checkunclient") as CheckBox;
                    if (check.Checked)
                    {
                        Label label = gvunasclient.Rows[i].FindControl("lblclientid") as Label;
                        if (label != null)
                        {
                            if (label.Text.Length > 0)
                            {
                                string sqlQry = "Update Clients set OurContactPersonId='" + ddloperationalmanager.SelectedValue + "' where ClientId ='" + label.Text.Trim() + "'";
                                int status =config.ExecuteNonQueryWithQueryAsync(sqlQry).Result;
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

        protected void btnright_Click(object sender, EventArgs e)
        {
            if (ddloperationalmanager.SelectedIndex > 0)
            {
                for (int i = 0; i < gvasclient.Rows.Count; i++)
                {
                    GridViewRow delrow = gvasclient.Rows[i];
                    CheckBox check = gvasclient.Rows[i].FindControl("checkunclient") as CheckBox;
                    if (check.Checked)
                    {
                        Label label = gvasclient.Rows[i].FindControl("lblclientid") as Label;
                        if (label != null)
                        {
                            if (label.Text.Length > 0)
                            {
                                string sqlQry = "Update Clients set OurContactPersonId='" + null + "' where ClientId ='" + label.Text.Trim() + "'";
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
                gvasclient.DataSource = null;
                gvasclient.DataBind();
                ddloperationalmanagername.SelectedIndex = 0;
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

                gvasclient.DataSource = null;
                gvasclient.DataBind();
                ddloperationalmanager.SelectedIndex = 0;
            }
        }

        protected void FillOpManagersIds()
        {
            string opmDesig = GlobalData.Instance.GetOPMDesig();
            string selectquery = " select EmpId from  EmpDetails where EmpDesgn = '" + opmDesig + "' " +
                " Order By cast(substring(Empid," + Elength + ", 6) as int)  ";
            dt = config.ExecuteAdaptorAsyncWithQueryParams(selectquery).Result;
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ddloperationalmanager.Items.Add(dt.Rows[i][0].ToString());
                }
            }
        }

        protected void FillOpManagersNames()
        {
            ddloperationalmanagername.Items.Insert(0, "--Select--");

            string opmDesig = GlobalData.Instance.GetOPMDesig();
            string selectquery = " select Empmname from  EmpDetails where EmpDesgn = '" + opmDesig + "' " +
                " Order By cast(substring(Empid," + Elength + ", 6) as int)  ";
            dt = config.ExecuteAdaptorAsyncWithQueryParams(selectquery).Result;
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ddloperationalmanagername.Items.Add(dt.Rows[i][0].ToString());
                }
            }
        }

        protected void FillOpManagersId()
        {
            string opmDesig = GlobalData.Instance.GetOPMDesig();
            string selectquery = " select EmpId from  EmpDetails where EmpDesgn = '" + opmDesig + "' " +
                " and  Empmname='" + ddloperationalmanagername.SelectedValue + "'";
            dt = config.ExecuteAdaptorAsyncWithQueryParams(selectquery).Result;
            if (dt.Rows.Count > 0)
            {
                ddloperationalmanager.SelectedValue = dt.Rows[0]["EmpId"].ToString();
            }
        }

        protected void FillOpManagersName()
        {
            string opmDesig = GlobalData.Instance.GetOPMDesig();
            string selectquery = " select Empmname from  EmpDetails where EmpDesgn = '" + opmDesig + "' " +
                 " and  Empid='" + ddloperationalmanager.SelectedValue + "'";
            dt = config.ExecuteAdaptorAsyncWithQueryParams(selectquery).Result;
            if (dt.Rows.Count > 0)
            {
                ddloperationalmanagername.SelectedValue = dt.Rows[0]["Empmname"].ToString();
            }
        }
    

    }
}