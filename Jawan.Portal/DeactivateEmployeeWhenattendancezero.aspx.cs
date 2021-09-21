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
    public partial class DeactivateEmployeeWhenattendancezero : System.Web.UI.Page
    {
        AppConfiguration config = new AppConfiguration();
        GridViewExportUtil gve = new GridViewExportUtil();
        DataTable dt;
        string EmpIDPrefix = "";
        string CmpIDPrefix = "";
        string Elength = "";
        string Clength = "";
        string BranchID = "";

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
                    switch (SqlHelper.Instance.GetCompanyValue())
                    {
                        case 0:// Write Omulance Invisible Links
                            break;
                        case 1://Write KLTS Invisible Links
                            ExpensesReportsLink.Visible = false;
                            break;
                        case 2://write Fames Link
                            ExpensesReportsLink.Visible = true;
                            break;


                        default:
                            break;
                    }
                }
                else
                {
                    Response.Redirect("login.aspx");
                }

                ddlcname.Items.Add("--Select--");
                LoadClientNames();
                LoadClientList();
            }



        }

        protected void PreviligeUsers(int previligerid)
        {
            switch (previligerid)
            {
                case 1:
                    break;
                case 2:

                    break;

                case 3:
                    EmployeeReportLink.Visible = true;
                    ClientsReportLink.Visible = true;
                    InventoryReportLink.Visible = true;

                    EmployeesLink.Visible = true;
                    ClientsLink.Visible = true;
                    CompanyInfoLink.Visible = false;
                    InventoryLink.Visible = false;
                    ReportsLink.Visible = true;
                    SettingsLink.Visible = true;

                    break;

                case 4:
                    EmployeeReportLink.Visible = true;
                    ClientsReportLink.Visible = true;
                    InventoryReportLink.Visible = true;

                    EmployeesLink.Visible = true;
                    ClientsLink.Visible = true;
                    CompanyInfoLink.Visible = true;
                    InventoryLink.Visible = true;
                    ReportsLink.Visible = true;
                    SettingsLink.Visible = true;

                    break;
                case 5:
                    EmployeeReportLink.Visible = true;
                    ClientsReportLink.Visible = true;
                    InventoryReportLink.Visible = true;
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
                    InventoryLink.Visible = true;
                    ReportsLink.Visible = true;
                    SettingsLink.Visible = false;
                    EmployeeReportLink.Visible = false;


                    break;
                default:
                    break;


            }
        }

        protected void GetWebConfigdata()
        {

            EmpIDPrefix = Session["EmpIDPrefix"].ToString();
            CmpIDPrefix = Session["CmpIDPrefix"].ToString();
            BranchID = Session["BranchID"].ToString();
        }

        protected void ddlcname_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            ClearData();

            if (ddlcname.SelectedIndex > 0)
            {
                txtmonth.Text = "";

                if (ddlcname.SelectedIndex == 1)
                {
                    ddlclientid.SelectedIndex = 1;
                    return;
                }
                ddlclientid.SelectedValue = ddlcname.SelectedValue;
            }
            else
            {

                ddlclientid.SelectedIndex = 0;
            }

        }

        protected void ddlclientid_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearData();

            if (ddlclientid.SelectedIndex > 0)
            {
                if (ddlclientid.SelectedIndex == 1)
                {
                    ddlcname.SelectedIndex = 1;
                    return;
                }

                txtmonth.Text = "";
                ddlcname.SelectedValue = ddlclientid.SelectedValue;

            }
            else
            {
                ddlcname.SelectedIndex = 0;
            }
        }


        protected void LoadClientNames()
        {
            DataTable DtClientids = GlobalData.Instance.LoadCNames(CmpIDPrefix,BranchID);
            if (DtClientids.Rows.Count > 0)
            {
                ddlcname.DataValueField = "Clientid";
                ddlcname.DataTextField = "clientname";
                ddlcname.DataSource = DtClientids;
                ddlcname.DataBind();
            }
            ddlcname.Items.Insert(0, "-Select-");
            ddlcname.Items.Insert(1, "ALL");

        }

        protected void LoadClientList()
        {
            DataTable DtClientNames = GlobalData.Instance.LoadCIds(CmpIDPrefix,BranchID);
            if (DtClientNames.Rows.Count > 0)
            {
                ddlclientid.DataValueField = "Clientid";
                ddlclientid.DataTextField = "Clientid";
                ddlclientid.DataSource = DtClientNames;
                ddlclientid.DataBind();
            }
            ddlclientid.Items.Insert(0, "-Select-");
            ddlclientid.Items.Insert(1, "ALL");
        }

        protected void Bindata(string Sqlqry)
        {
            DataTable dt = config.ExecuteAdaptorAsyncWithQueryParams(Sqlqry).Result;
            if (dt.Rows.Count > 0)
            {
                GVListEmployees.DataSource = dt;
                GVListEmployees.DataBind();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showlalert", "alert('There Is No Salary Details For The Selected client');", true);

            }
        }

        protected void ClearData()
        {
            LblResult.Text = "";
            GVListEmployees.DataSource = null;
            GVListEmployees.DataBind();
        }

        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            LblResult.Text = "";

            if (ddlclientid.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showlalert", "alert('Please Select Client ID/Name');", true);
                return;
            }

            if (txtmonth.Text.Trim().Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showlalert", "alert('Please Select Month');", true);

                return;
            }
            string month = DateTime.Parse(txtmonth.Text.Trim()).Month.ToString();
            string Year = DateTime.Parse(txtmonth.Text.Trim()).Year.ToString();
            string sqlqry = string.Empty;


            if (ddlclientid.SelectedIndex > 0 && txtmonth.Text.Trim().Length != 0)
            {

                sqlqry = " select EA.ClientId,C.Clientname, E.Empid, E.EmpmName, EA.NoOfDuties," +
                       "EA.DutyHrs,EA.OT from EmpAttendance EA Inner join" +
                        " EmpDetails E on  E.EmpId=EA.Empid  inner join  clients C  on EA.ClientId=C.ClientId  and EA.Month=" +
                        month + Year.Substring(2, 2).ToString() + " and EA.NoOfDuties=0  and EA.OT=0    And EA.ClientId='" + ddlclientid.SelectedValue + "'";
                Bindata(sqlqry);
                return;
            }

        }


        protected void lbtn_Deactivate_Click(object sender, EventArgs e)
        {

            if (GVListEmployees.Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showlalert", "alert('There Is No Employees  For The Selected client');", true);
                return;
            }

            else
            {
                int i = 0;
                for (i = 0; i < GVListEmployees.Rows.Count; i++)
                {
                    string empid = GVListEmployees.Rows[i].Cells[2].Text;
                    updateEmpStatus(empid, 0);
                }

                if (i != 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showlalert", "alert('Deactivaeted Employees  Sucessfully');", true);
                    GVListEmployees.DataSource = null;
                    GVListEmployees.DataBind();
                    return;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showlalert", "alert('There Is No Employees  For The Selected client');", true);

                }
            }

        }


        protected void updateEmpStatus(string id, int status)
        {
            if (status == 0)
            {

                //string strQry = "Update EmpDetails set EmpStatus = " + status + " where EmpId = '" + id + "'";
                //int status1 = SqlHelper.Instance.ExecuteDMLQry(strQry);
                //strQry = "INSERT INTO InEmpDetails SELECT * FROM EmpDetails WHERE EmpId = '" + id + "'";
                //status1 = SqlHelper.Instance.ExecuteDMLQry(strQry);
                //strQry = "Delete EmpDetails where EmpId = '" + id + "'";
                //status1 = SqlHelper.Instance.ExecuteDMLQry(strQry);

                string strQry = "Delete from empattendance Where  Empid = '" + id + "'  and  Clientid='" + ddlclientid.SelectedValue + "'";
                int status1 = config.ExecuteNonQueryWithQueryAsync(strQry).Result;





            }
        }

    }
}