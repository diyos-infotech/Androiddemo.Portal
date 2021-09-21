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
using System.Data;
using KLTS.Data;
using System.Globalization;
using Jawan.Portal.DAL;
namespace Jawan.Portal
{
    public partial class AttendanceReport : System.Web.UI.Page
    {
        AppConfiguration config = new AppConfiguration();
        GridViewExportUtil gve = new GridViewExportUtil();
        string EmpIDPrefix = "";
        string BranchID = "";

        string CmpIDPrefix = "";
        string Elength = "";
        string Clength = "";

        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {

                GetWebConfigdata();
                if (!IsPostBack)
                {
                    if (Session["UserId"] != null && Session["AccessLevel"] != null)
                    {
                        PreviligeUsers(Convert.ToInt32(Session["AccessLevel"]));
                        lblDisplayUser.Text = Session["UserId"].ToString();
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
                    FillEmployeesList();
                    LoadNames();

                }

            }
            catch (Exception ex)
            {

            }
        }

        protected void GetWebConfigdata()
        {
            EmpIDPrefix = Session["EmpIDPrefix"].ToString();
            CmpIDPrefix = Session["CmpIDPrefix"].ToString();
            BranchID = Session["BranchID"].ToString();
        }

        protected void FillEmployeesList()
        {
            DataTable DtEmpIds = GlobalData.Instance.LoadEmpIds(EmpIDPrefix,BranchID);
            if (DtEmpIds.Rows.Count > 0)
            {
                ddlEmployee.DataValueField = "empid";
                ddlEmployee.DataTextField = "empid";
                ddlEmployee.DataSource = DtEmpIds;
                ddlEmployee.DataBind();
            }
            ddlEmployee.Items.Insert(0, "-Select-");
            ddlEmployee.Items.Insert(1, "All");
        }
        protected void LoadNames()
        {
            DataTable DtEmpNames = GlobalData.Instance.LoadEmpNames(EmpIDPrefix,BranchID);
            if (DtEmpNames.Rows.Count > 0)
            {
                ddlempname.DataValueField = "empid";
                ddlempname.DataTextField = "FullName";
                ddlempname.DataSource = DtEmpNames;
                ddlempname.DataBind();
            }
            ddlempname.Items.Insert(0, "-Select-");
            ddlempname.Items.Insert(1, "All");
        }


        protected void PreviligeUsers(int previligerid)
        {
            switch (previligerid)
            {
                case 1:
                    break;
                case 2:
                    InventoryLink.Visible = false;
                    CompanyInfoLink.Visible = false;
                    ReportsLink.Visible = false;
                    SettingsLink.Visible = false;
                    break;

                case 3:
                    InventoryLink.Visible = false;
                    ReportsLink.Visible = false;
                    SettingsLink.Visible = false;
                    break;
                case 4:
                    InventoryLink.Visible = false;
                    CompanyInfoLink.Visible = false;
                    ReportsLink.Visible = false;
                    SettingsLink.Visible = false;
                    break;
                case 5:
                    InventoryLink.Visible = false;
                    CompanyInfoLink.Visible = false;
                    ReportsLink.Visible = false;
                    SettingsLink.Visible = false;
                    break;
                case 6:
                    InventoryLink.Visible = false;
                    CompanyInfoLink.Visible = false;
                    ReportsLink.Visible = false;
                    SettingsLink.Visible = false;
                    break;
                default:
                    break;
            }
        }

        protected void ddlEmployee_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearData(); ddlsearchtype.SelectedIndex = 0; txtMonth.Text = "";
            if (ddlEmployee.SelectedIndex > 0)
            {
                ddlempname.SelectedValue = ddlEmployee.SelectedValue;
            }
            else
            {
                ddlempname.SelectedIndex = 0;
            }
            ddlsearchtype_SelectedIndexChanged(sender, e);
        }

        protected void ddlempname_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearData(); ddlsearchtype.SelectedIndex = 0; txtMonth.Text = "";
            if (ddlempname.SelectedIndex > 0)
            {
                ddlEmployee.SelectedValue = ddlempname.SelectedValue;
            }
            else
            {
                ddlEmployee.SelectedIndex = 0;
            }
            ddlsearchtype_SelectedIndexChanged(sender, e);
        }



        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            ClearData();
            #region  Begin New code  For Validatations As on [12-11-2013]
            if (ddlEmployee.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Please Select The Employee ID/NAME');", true);
                return;
            }
            if (ddlsearchtype.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Sholwalert", "alert('Please select ServiceType');", true);
                return;
            }
            else
            {
                if (ddlsearchtype.SelectedIndex != 1)
                {
                    if (txtMonth.Text.Trim().Length == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Please Select The Month');", true);
                        return;
                    }
                }
            }

            #endregion End  New code  For Validatations As on [12-11-2013]

            #region  Begin New code For Variable Declaration  As on [12-11-2013]
            var SqlQryforAttendance = "";
            DataTable DtAttenDance = null;
            var SelectedMonth = "";
            var SelectedEmployeeID = "";
            Hashtable HtAttendance = new Hashtable();
            var ProcedureName = "";
            var Month = "";
            var Year = "";
            #endregion End New Code  For Variable Declaration  As on [12-11-2013]


            #region  Begin New Code  For Assign Values as on [12-11-2013]
            if (ddlsearchtype.SelectedIndex > 0)
            {
                int SearchType = ddlsearchtype.SelectedIndex;
            }
            if (txtMonth.Text.Length > 0)
            {
                SelectedMonth = DateTime.Parse(txtMonth.Text.Trim(), CultureInfo.GetCultureInfo("en-gb")).ToString();
                Month = DateTime.Parse(SelectedMonth).Month.ToString();
                Year = DateTime.Parse(SelectedMonth).Year.ToString().Substring(2, 2);
                SelectedMonth = Month + Year;
            }
            ProcedureName = "ReportForEmployeeAttendance";
            #endregion End New Code  For Assign Values as on [12-11-2013]

            #region Begin New Code For Assign Values to the Hashtable Parameters as on [12-11-2013]
            //Case1 When Empid-->All,SearchType-->monthly
            if (ddlEmployee.SelectedIndex == 1 && ddlsearchtype.SelectedIndex > 1)
            {
                HtAttendance.Add("@month", SelectedMonth);
                HtAttendance.Add("@EmpIdindex", 0);
            }
            else if (ddlEmployee.SelectedIndex > 1 && ddlsearchtype.SelectedIndex > 1)
            {
                HtAttendance.Add("@Empid", ddlEmployee.SelectedValue);
                HtAttendance.Add("@month", SelectedMonth);
                HtAttendance.Add("@EmpIdindex", 1);
            }
            else
            {
                HtAttendance.Add("@Empid", ddlEmployee.SelectedValue);
                HtAttendance.Add("@EmpIdindex", 1);
            }
            #endregion End New Code For Assign Values to the Hashtable Parameters as on [12-11-2013]

            DtAttenDance =config.ExecuteAdaptorAsyncWithParams(ProcedureName, HtAttendance).Result;


            if (DtAttenDance.Rows.Count > 0)
            {
                GVListEmployees.DataSource = DtAttenDance;
                GVListEmployees.DataBind();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Showalert", "alert('There is no data available to your selection');", true);
                return;
            }
        }
        protected void ClearData()
        {
            GVListEmployees.DataSource = null;
            GVListEmployees.DataBind();
            /*ddlEmployee.SelectedIndex = 0; ddlempname.SelectedIndex = 0; txtMonth.Text = "";
            ddlsearchtype.SelectedIndex = 0;*/
        }
        float Totalduties = 0;
        float Totalots = 0;
        float TotalNHS = 0;
        float TotalWO = 0;
        float TotalNpots = 0;
        float TotalTotal = 0;

        protected void GVListEmployees_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                float Duties = float.Parse(((Label)e.Row.FindControl("lblduties")).Text);
                Totalduties += Duties;
                float OTs = float.Parse(((Label)e.Row.FindControl("lblots")).Text);
                Totalots += OTs;
                float NHs = float.Parse(((Label)e.Row.FindControl("lblNHS")).Text);
                TotalNHS += NHs;
                float WO = float.Parse(((Label)e.Row.FindControl("lblWO")).Text);
                TotalWO += WO;
                float NPOTs = float.Parse(((Label)e.Row.FindControl("lblNpots")).Text);
                TotalNpots += NPOTs;
                float Total = float.Parse(((Label)e.Row.FindControl("lbltotal")).Text);
                TotalTotal += Total;
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].Text = "Total";
                ((Label)e.Row.FindControl("lblTotalduties")).Text = Totalduties.ToString();
                ((Label)e.Row.FindControl("lblTotalots")).Text = Totalots.ToString();
                ((Label)e.Row.FindControl("lblTotalNHS")).Text = TotalNHS.ToString();
                ((Label)e.Row.FindControl("lblTotalWO")).Text = TotalWO.ToString();
                ((Label)e.Row.FindControl("lblTotalNpots")).Text = TotalNpots.ToString();
                ((Label)e.Row.FindControl("lblTotalTotal")).Text = TotalTotal.ToString();
            }
        }

        protected void ddlsearchtype_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearData();
            if (ddlsearchtype.SelectedIndex == 1 && ddlEmployee.SelectedIndex == 1)
            {
                ddlEmployee.SelectedIndex = 0; ddlsearchtype.SelectedIndex = 0; ddlempname.SelectedIndex = 0;
                ScriptManager.RegisterStartupScript(this, GetType(), "Showalert", "alert('Employee ID must be Unique');", true);
                return;
            }
            else
            {
                if (ddlsearchtype.SelectedIndex == 1)
                {
                    lblMonth.Visible = false;
                    txtMonth.Visible = false;
                    monthspanid.Visible = false;
                }
                else
                {
                    if (ddlsearchtype.SelectedIndex > 1)
                    {
                        lblMonth.Visible = true;
                        txtMonth.Visible = true;
                        monthspanid.Visible = true;
                    }
                }
            }
        }
    
    }
}