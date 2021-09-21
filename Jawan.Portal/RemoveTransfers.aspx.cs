using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KLTS.Data;
using System.Data;
using Jawan.Portal.DAL;
namespace Jawan.Portal
{
    public partial class RemoveTransfers : System.Web.UI.Page
    {
        AppConfiguration config = new AppConfiguration();
        GridViewExportUtil gve = new GridViewExportUtil();
        DataTable dt;
        //int oderid = 0;
        string EmpIDPrefix = "";
        string CmpIDPrefix = "";
        string BranchID = "";
        string Elength = "";
        string Clength = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            MessageLabel.Text = "";

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
                        LoadClientNames();
                        LoadClientList();
                    }
                    else
                    {
                        Response.Redirect("login.aspx");
                    }
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
                    //AddEmployeeLink.Visible = true;
                    //ModifyEmployeeLink.Visible = false;
                    //DeleteEmployeeLink.Visible = false;
                    //AssigningWorkerLink.Visible = false;
                    //AttendanceLink.Visible = false;
                    //LoanLink.Visible = false;
                    //PaymentLink.Visible = false;
                    //TrainingEmployeeLink.Visible = false;
                    //JobLeavingReasonsLink.Visible = false;
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
                    //AddEmployeeLink.Visible = false;
                    //ModifyEmployeeLink.Visible = false;
                    //DeleteEmployeeLink.Visible = false;
                    //AssigningWorkerLink.Visible = false;
                    //AttendanceLink.Visible = false;
                    //LoanLink.Visible = false;
                    //PaymentLink.Visible = false;
                    ////TrainingEmployeeLink.Visible = false;
                    //PostingOrderListLink.Visible = true;
                    ////JobLeavingReasonsLink.Visible = false;

                    ClientsLink.Visible = true;
                    CompanyInfoLink.Visible = true;
                    InventoryLink.Visible = true;
                    ReportsLink.Visible = true;
                    SettingsLink.Visible = true;
                    break;
                case 5:
                    //AddEmployeeLink.Visible = false;
                    //ModifyEmployeeLink.Visible = false;
                    //DeleteEmployeeLink.Visible = false;
                    //AssigningWorkerLink.Visible = false;
                    //LoanLink.Visible = false;
                    //PaymentLink.Visible = false;
                    ////TrainingEmployeeLink.Visible = true;
                    ////JobLeavingReasonsLink.Visible = false;
                    //AttendanceLink.Visible = true;

                    EmployeesLink.Visible = true;
                    ClientsLink.Visible = true;
                    CompanyInfoLink.Visible = false;
                    InventoryLink.Visible = false;
                    ReportsLink.Visible = true;
                    ReportsLink.Visible = true;
                    SettingsLink.Visible = true;

                    PostingOrderLink.Visible = false;
                    break;
                case 6:
                    //AddEmployeeLink.Visible = false;
                    //ModifyEmployeeLink.Visible = false;
                    //DeleteEmployeeLink.Visible = false;
                    //AssigningWorkerLink.Visible = false;
                    //AttendanceLink.Visible = true;
                    //LoanLink.Visible = true;
                    //PaymentLink.Visible = true;
                    ////TrainingEmployeeLink.Visible = false;
                    ////JobLeavingReasonsLink.Visible = false;

                    InventoryLink.Visible = false;
                    CompanyInfoLink.Visible = false;
                    ReportsLink.Visible = false;
                    SettingsLink.Visible = false;

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


        protected void Fillcname()
        {
            if (ddlUnit.SelectedIndex > 0)
            {
                ddlcname.SelectedValue = ddlUnit.SelectedValue;
            }
            else
            {
                ddlcname.SelectedIndex = 0;
            }
        }

        protected void FillClientid()
        {
            if (ddlcname.SelectedIndex > 0)
            {
                ddlUnit.SelectedValue = ddlcname.SelectedValue;
            }
            else
            {
                ddlUnit.SelectedIndex = 0;
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

        }

        protected void LoadClientList()
        {
            DataTable DtClientNames = GlobalData.Instance.LoadCIds(CmpIDPrefix,BranchID);
            if (DtClientNames.Rows.Count > 0)
            {
                ddlUnit.DataValueField = "Clientid";
                ddlUnit.DataTextField = "Clientid";
                ddlUnit.DataSource = DtClientNames;
                ddlUnit.DataBind();
            }
            ddlUnit.Items.Insert(0, "-Select-");
        }

        protected void ddlUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlUnit.SelectedIndex > 0)
            {
                Fillcname();
                TrnasferedEmployeesBindData();
            }
            else
            {
                ClearData();
            }
        }

        protected void ddlcname_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlcname.SelectedIndex > 0)
            {
                FillClientid();
                TrnasferedEmployeesBindData();
            }
            else
            {
                ClearData();
            }
        }

        protected void ClearData()
        {
            ddlcname.SelectedIndex = 0;
            ddlUnit.SelectedIndex = 0;
            gvemployees.DataSource = null;
            gvemployees.DataBind();
        }

        protected void gvemployees_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            DateTime startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime endDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, GlobalData.Instance.GetNoOfDaysForNextMonth());

            if (ddlUnit.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showlalert", "alert('Please select client Id/Name');", true);
                return;
            }

            string empid = gvemployees.Rows[e.RowIndex].Cells[2].Text;
            string TransferType = ((Label)gvemployees.Rows[e.RowIndex].FindControl("lbltransfertype")).Text;

            int TransferID = 0;

            if ("Temp. Transfer" == TransferType)
            {
                TransferID = 0;
            }

            if ("Posting Order" == TransferType)
            {
                TransferID = 1;
            }

            if ("Dummy transfer" == TransferType)
            {
                TransferID = -1;
            }

            bool chek = ((CheckBox)gvemployees.Rows[e.RowIndex].Cells[0].FindControl("chkempid")).Checked;

            if (chek == true)
            {
                string sqlqry = "delete  from Emppostingorder Where Empid='" + empid + "' and tounitid='" +
                    ddlUnit.SelectedValue + "'";
                int status = config.ExecuteNonQueryWithQueryAsync(sqlqry).Result;
                if (status != 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showlalert", "alert('Employee deleted successfully');", true);

                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showlalert", "alert('Please select employee');", true);

            }
            TrnasferedEmployeesBindData();
        }

        protected void gvemployees_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvemployees.PageIndex = e.NewPageIndex;
            TrnasferedEmployeesBindData();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {

            if (ddlUnit.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showlalert", "alert('Please  Select Client ID/Name');", true);
                return;
            }

            if (txtsearch.Text.Trim().Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showlalert", "alert('Please Enter the Employee Id');", true);
                return;
            }

            string selectQuery = "Select ed.EmpId,(ISNULL( ed.empfname,'' )+' '+ ISNULL(ed.empmname,'') +' '+ISNULL(ed.Emplname,'')) as  Name," +
                  " D.design as Desgn,EPO.Desgn as Designid  from Empdetails ed,Emppostingorder EPO  inner join Designations D on D.designid=EPO.Desgn   where ed.Empid=EPO.Empid  and tounitid='" +
                  ddlUnit.SelectedValue + "'  and  EPO.Empid like '%" + txtsearch.Text.Trim() + "'";
            DataTable data = config.ExecuteAdaptorAsyncWithQueryParams(selectQuery).Result;
            if (data.Rows.Count > 0)
            {
                gvemployees.DataSource = data;
                gvemployees.DataBind();
            }

            else
            {
                gvemployees.DataSource = data;
                gvemployees.DataBind();
            }


        }

        protected void BindData()
        {
            string selectquery = "select empid,UnitId from empdetails Order By cast(substring(empid," + Elength + ", 6) as int)";
            DataTable empTable = config.ExecuteAdaptorAsyncWithQueryParams(selectquery).Result;

            if (ddlUnit.SelectedIndex > 0)
            {
                DataTable data = new DataTable();

                string selectQuery = "Select distinct ed.EmpId, (ed.empfname+' '+ ed.empmname +' '+ ed.emplname ) as name," +
                    " po.Desgn From EmpDetails as ed Inner Join EmpPostingOrder as po ON "
                 + "  ed.EmpId=po.EmpId AND (TransferType=1 or TransferType=0  or TransferType=-1 ) AND po.ToUnitId='" + ddlUnit.SelectedValue +
                 "' AND po.OrderDt = ( Select Top 1 epo.OrderDt From EmpPostingOrder as epo "
                 + "Where epo.ToUnitId='" + ddlUnit.SelectedValue + "' AND epo.EmpId=ed.EmpId AND (TransferType=1  or  TransferType=0 or TransferType=-1) " +
                 " ORDER BY OrderDt DESC) Order by  right(isnull(epo.Empid,0),6)";
                data = config.ExecuteAdaptorAsyncWithQueryParams(selectQuery).Result;

                gvemployees.DataSource = data;
                gvemployees.DataBind();
            }
            MessageLabel.Text = "";
            // txtPrevUnitId.Text = "";
        }

        protected void LoadEmployeeTransfertype()
        {
            DateTime startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime endDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, GlobalData.Instance.GetNoOfDaysForNextMonth());

            for (int empindex = 0; empindex < gvemployees.Rows.Count; empindex++)
            {
                string empid = gvemployees.Rows[empindex].Cells[1].Text;
                //string Design = gvemployees.Rows[empindex].Cells[3].Text;
                string Design = ((Label)gvemployees.Rows[empindex].FindControl("ddldesignid")).Text;

                string sqlqry = "Select TransferType from EmpPostingOrder where Empid='" + empid + "'  and  Desgn='" + Design + "'" +

                     " and tounitid='" + ddlUnit.SelectedValue + "'";

                DataTable dt = config.ExecuteAdaptorAsyncWithQueryParams(sqlqry).Result;
                Label transfertype = gvemployees.Rows[empindex].FindControl("lbltransfertype") as Label;

                if (dt.Rows.Count > 0)
                {

                    if (dt.Rows[0]["TransferType"].ToString() == "0")
                    {
                        transfertype.Text = "Temp. Transfer";
                    }
                    if (dt.Rows[0]["TransferType"].ToString() == "1")
                    {
                        transfertype.Text = "Posting Order";
                    }
                    if (dt.Rows[0]["TransferType"].ToString() == "-1")
                    {
                        transfertype.Text = "Dummy transfer";
                    }
                }
            }
        }

        protected void TrnasferedEmployeesBindData()
        {
            //string selectquery = "select empid,UnitId from empdetails Order By  cast(substring(empid,4, 6) as int)";
            //DataTable empTable = SqlHelper.Instance.GetTableByQuery(selectquery);
            if (ddlUnit.SelectedIndex > 0)
            {
                DataTable data = new DataTable();
                string selectQuery = "Select ed.EmpId,(ISNULL( ed.empfname,'' )+' '+ ISNULL(ed.empmname,'') +' '+ISNULL(ed.Emplname,'')) as  Name," +
                     "D.design as Desgn,EPO.Desgn as Designid  from Empdetails ed,Emppostingorder EPO inner join Designations D on D.designid=EPO.Desgn   where ed.Empid=EPO.Empid  and tounitid='" +
                     ddlUnit.SelectedValue + "'    Order by  right(isnull(EPO.Empid,0),6)";

                data = config.ExecuteAdaptorAsyncWithQueryParams(selectQuery).Result;

                if (data.Rows.Count > 0)
                {
                    gvemployees.DataSource = data;
                    gvemployees.DataBind();
                }
                else
                {
                    gvemployees.DataSource = null;
                    gvemployees.DataBind();
                }
            }
            LoadEmployeeTransfertype();
            MessageLabel.Text = "";
        }

    }
}