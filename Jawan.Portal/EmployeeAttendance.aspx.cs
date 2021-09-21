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
using System.Globalization;
using System.IO;
using Jawan.Portal.DAL;

namespace Jawan.Portal
{
    public partial class EmployeeAttendance : System.Web.UI.Page
    {
        AppConfiguration config = new AppConfiguration();
        GridViewExportUtil util = new GridViewExportUtil();

        string EmpIDPrefix = "";
        string CmpIDPrefix = "";
        int oderid = 0;
        string BranchID = "";

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
                    LoadClientList();
                    LoadClientNames();
                    FillMonthList();
                    LoadDesignations();

                    txtorderdate.Text = DateTime.Now.ToString("MM/dd/yyyy");
                    txtjoindate.Text = DateTime.Now.ToString("MM/dd/yyyy");
                    OrderId();

                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Show alert", "alert('Your Session Expired');", true);
                Response.Redirect("~/Login.aspx");
            }

        }

        protected void GetWebConfigdata()
        {
            EmpIDPrefix = Session["EmpIDPrefix"].ToString();
            CmpIDPrefix = Session["CmpIDPrefix"].ToString();
            BranchID = Session["BranchID"].ToString();
        }

        protected void LoadClientList()
        {
            DataTable dt = GlobalData.Instance.LoadCIds(CmpIDPrefix,BranchID);
            if (dt.Rows.Count > 0)
            {
                ddlClientID.DataValueField = "clientid";
                ddlClientID.DataTextField = "clientid";
                ddlClientID.DataSource = dt;
                ddlClientID.DataBind();
            }
            ddlClientID.Items.Insert(0, "--Select--");


        }

        protected void LoadClientNames()
        {

            DataTable dt = GlobalData.Instance.LoadCNames(CmpIDPrefix,BranchID);
            if (dt.Rows.Count > 0)
            {
                ddlCName.DataValueField = "clientid";
                ddlCName.DataTextField = "Clientname";
                ddlCName.DataSource = dt;
                ddlCName.DataBind();
            }
            ddlCName.Items.Insert(0, "--Select--");

        }

        protected void ddlClientID_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlClientID.SelectedIndex > 0)
                {
                    displaydata();
                }
                else
                {
                    ClearData();
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void ddlCName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlCName.SelectedIndex > 0)
                {
                    displaydataFormClientName();
                }
                else
                {
                    ClearData();
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (IsPostBack)
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "IsPostBack", "var isPostBack = false;", true);
            }

            lblTotalDuties.Text = string.Empty;
            lblTotalOts.Text = string.Empty;
            if (ddlMonth.SelectedIndex > 0)
            {
                FillAttendanceGrid();
            }
            else
            {

                GridView1.DataSource = null;
                GridView1.DataBind();

            }
        }

        protected void txtmonthOnTextChanged(object sender, EventArgs e)
        {


            lblTotalDuties.Text = string.Empty;
            lblTotalOts.Text = string.Empty;
            if (txtmonth.Text.Length > 0)
            {
                FillAttendanceGrid();
            }
            else
            {

                GridView1.DataSource = null;
                GridView1.DataBind();

            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            var password = string.Empty;
            var SPName = string.Empty;
            password = txtPassword.Text.Trim();
            string sqlPassword = "select password from IouserDetails where password='" + txtPassword.Text + "'";
            DataTable dtpassword = config.ExecuteReaderWithQueryAsync(sqlPassword).Result;

            if (dtpassword.Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Invalid Password');", true);
                return;
            }

            #region Validation

            GridView1.DataSource = null;
            GridView1.DataBind();
            txtmonth.Text = string.Empty;
            ddlMonth.SelectedIndex = 0;

            if (ddlClientID.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Please Select The client Id');", true);
                Chk_Month.Checked = false;
                return;
            }

            #endregion

            Chk_Month.Checked = true;

            if (Chk_Month.Checked)
            {
                txtmonth.Visible = true;
                ddlMonth.SelectedIndex = 0;
                ddlMonth.Visible = false;

            }

        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            modelLogindetails.Hide();
            Chk_Month.Checked = false;
            GridView1.DataSource = null;
            GridView1.DataBind();
            txtmonth.Text = string.Empty;
            ddlMonth.SelectedIndex = 0;
            if (Chk_Month.Checked == false)
            {
                txtmonth.Visible = false;
                txtmonth.Text = "";
                ddlMonth.SelectedIndex = 0;
                ddlMonth.Visible = true;
            }
        }

        protected void FillMonthList()
        {
            //month
            var formatInfoinfo = new DateTimeFormatInfo();
            string[] monthName = formatInfoinfo.MonthNames;
            string month = monthName[DateTime.Now.Month - 1];
            string LastMonth = "";
            ddlMonth.Items.Add(month);
            try
            {
                month = monthName[DateTime.Now.Month - 2];
            }
            catch (IndexOutOfRangeException ex)
            {
                month = monthName[11];
            }
            try
            {
                LastMonth = monthName[DateTime.Now.Month - 3];
            }
            catch (IndexOutOfRangeException ex)
            {
                LastMonth = monthName[12 - (3 - DateTime.Now.Month)];
            }

            ddlMonth.Items.Add(month);
            ddlMonth.Items.Add(LastMonth);

            ddlMonth.Items.Insert(0, "--Select--");

        }

        private void displaydata()
        {
            cleartransferdata();
            ddlMonth.SelectedIndex = 0;
            GridView1.DataSource = null;
            GridView1.DataBind();

            if (ddlClientID.SelectedIndex > 0)
            {
                string selectclientdata = "select Clientid,clientname,clientphonenumbers,OurContactPersonId " +
                    " from clients Where Clientid='" + ddlClientID.SelectedValue + "'";
                DataTable dtdata = config.ExecuteAdaptorAsyncWithQueryParams(selectclientdata).Result;
                if (dtdata.Rows.Count > 0)
                {
                    ddlCName.SelectedValue = dtdata.Rows[0]["Clientid"].ToString();
                    txtphonenumbers.Text = dtdata.Rows[0]["clientphonenumbers"].ToString();
                    txtocp.Text = dtdata.Rows[0]["OurContactPersonId"].ToString();
                }
                else
                {
                    ddlCName.SelectedIndex = 0;
                    txtphonenumbers.Text = "";
                    txtocp.Text = "";
                }
                /*** Getting list of employees working for this client for this month*/
            }
            else
            {

            }
        }

        private void displaydataFormClientName()
        {
            if (ddlCName.SelectedIndex > 0)
            {
                int month = 0, year = 2000;
                GlobalData.Instance.GetMonthAndYear(ddlMonth.SelectedValue, ddlMonth.SelectedIndex, out month, out year);

                string selectclientdata = "select  Clientid,clientphonenumbers,OurContactPersonId from clients Where Clientid='" + ddlCName.SelectedValue + "'";
                DataTable dtdata = config.ExecuteAdaptorAsyncWithQueryParams(selectclientdata).Result;
                if (dtdata.Rows.Count > 0)
                {
                    ddlClientID.SelectedValue = dtdata.Rows[0]["ClientId"].ToString();
                    txtphonenumbers.Text = dtdata.Rows[0]["clientphonenumbers"].ToString();
                    txtocp.Text = dtdata.Rows[0]["OurContactPersonId"].ToString();
                }
                else
                {
                    ddlCName.SelectedIndex = 0;
                    txtphonenumbers.Text = "";
                    txtocp.Text = "";
                }
                /*** Getting list of employees working for this client for this month*/
                // FillAttendanceGrid();
                ddlMonth.SelectedIndex = 0;
                GridView1.DataSource = null;
                GridView1.DataBind();
            }
            else
            {
            }
        }

        protected void LoadDesignations()
        {
            DataTable DtDesignations = GlobalData.Instance.LoadDesigns();
            if (DtDesignations.Rows.Count > 0)
            {
                ddlDesignation.DataValueField = "Designid";
                ddlDesignation.DataTextField = "Design";
                ddlDesignation.DataSource = DtDesignations;
                ddlDesignation.DataBind();
            }
            ddlDesignation.Items.Insert(0, "-Select-");
        }

        protected void PreviligeUsers(int previligerid)
        {
            switch (previligerid)
            {
                case 1:
                    AddEmployeeLink.Visible = false;
                    ModifyEmployeeLink.Visible = false;
                    DeleteEmployeeLink.Visible = false;
                    AssigningWorkerLink.Visible = false;
                    PostingOrderListLink.Visible = false;
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
                    AddEmployeeLink.Visible = false;
                    ModifyEmployeeLink.Visible = false;
                    DeleteEmployeeLink.Visible = false;
                    AssigningWorkerLink.Visible = false;
                    AttendanceLink.Visible = true;
                    LoanLink.Visible = true;
                    PaymentLink.Visible = true;
                    //TrainingEmployeeLink.Visible = false;
                    PostingOrderListLink.Visible = true;
                    //JobLeavingReasonsLink.Visible = false;

                    ClientsLink.Visible = true;
                    CompanyInfoLink.Visible = false;
                    InventoryLink.Visible = false;
                    ReportsLink.Visible = true;
                    SettingsLink.Visible = true;
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
                    LoanLink.Visible = false;
                    PaymentLink.Visible = false;
                    //TrainingEmployeeLink.Visible = true;
                    //JobLeavingReasonsLink.Visible = false;
                    AttendanceLink.Visible = true;

                    EmployeesLink.Visible = true;
                    ClientsLink.Visible = true;
                    CompanyInfoLink.Visible = false;
                    InventoryLink.Visible = false;
                    ReportsLink.Visible = true;
                    ReportsLink.Visible = true;
                    SettingsLink.Visible = true;
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
                case 8:
                    RemindersLink.Visible = false;
                    break;
                case 9:
                    AddEmployeeLink.Visible = false;
                    ModifyEmployeeLink.Visible = false;
                    DeleteEmployeeLink.Visible = false;
                    AssigningWorkerLink.Visible = false;
                    PostingOrderListLink.Visible = false;
                    RemindersLink.Visible = false;
                    CompanyInfoLink.Visible = false;
                    InventoryLink.Visible = false;
                    SettingsLink.Visible = false;
                    break;
                case 10:
                    RemindersLink.Visible = false;
                    break;

                default:
                    break;
            }
        }

        private void ClearData()
        {

            ddlClientID.SelectedIndex = 0;
            ddlCName.SelectedIndex = 0;
        }

        protected void cleartransferdata()
        {
            ddlTransfertype.SelectedIndex = 0;
            ddlDesignation.SelectedIndex = 0;
        }

        protected void btntransfer_Click(object sender, EventArgs e)
        {

            if (IsPostBack)
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "IsPostBack", "var isPostBack = true;", true);
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "IsPostBack", "var isPostBack = false;", true);
            }


            #region Declarations

            int transfertype = 1;
            DateTime joindate = new DateTime();
            DateTime relivedate = new DateTime(1900, 01, 01);
            DateTime orddate = new DateTime();

            string joindt = string.Empty;
            string relievedt = string.Empty;
            string orderdt = string.Empty;
            #endregion

            #region Check Validation
            string unitid = null;
            if (ddlClientID.SelectedIndex > 0)
                unitid = ddlClientID.SelectedValue;
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showlalert", "alert('Please Select UnitId');", true);

                return;
            }
            string empid = null;
            if (TxtEmpid.Text.Length > 0)
                empid = TxtEmpid.Text;
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showlalert", "alert('Please Select Employee Id');", true);

                return;
            }
            string designation = "";
            if (ddlDesignation.SelectedIndex > 0)
                designation = ddlDesignation.SelectedValue;
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showlalert", "alert('Please select Designation for transfer');", true);
                return;
            }

            #region PF,ESI,PT Check Validations

            int PF = 1;

            if (chkpf.Checked)
            {
                PF = 1;
            }
            else
            {
                PF = 0;
            }

            int ESI = 1;

            if (chkesi.Checked)
            {
                ESI = 1;
            }
            else
            {
                ESI = 0;
            }

            int PT = 1;

            if (chkpt.Checked)
            {
                PT = 1;
            }
            else
            {
                PT = 0;
            }
            #endregion

            #endregion   //End Validation

            orderdt = Timings.Instance.CheckDateFormat(DateTime.Now.ToString("dd/MM/yyyy"));
            joindt = Timings.Instance.CheckDateFormat(DateTime.Now.ToString("dd/MM/yyyy"));

            int postedmonth = GetMonthBasedOnSelectionDateorMonth();

            string strQry = "Select * from EmpPostingOrder where empid='" + empid + "' AND tounitid='" + unitid + "' and desgn='" + designation + "' and RelieveMonth is null ";
            DataTable potable = config.ExecuteAdaptorAsyncWithQueryParams(strQry).Result;
            if (potable.Rows.Count > 0)
            {
                string oldDesig = potable.Rows[0]["desgn"].ToString();
                string oldOrderid = potable.Rows[0]["orderid"].ToString();
                if (oldDesig.CompareTo(designation) == 0)
                {

                    strQry = "Update EmpPostingOrder set relievemonth='',postedmonth='" + postedmonth + "',transferstatus=1, pf=" + PF + ",esi=" + ESI + ",pt=" + PT + ",orderdt='" + orderdt + "' where orderid='" +
                      oldOrderid + "'  and  transfertype=" + transfertype + " and  desgn='" + designation + "'   and  empid='" + empid + "'";
                    int status = config.ExecuteNonQueryWithQueryAsync(strQry).Result;


                    FillAttendanceGrid();
                    return;
                }
                else
                {


                    strQry = "Update EmpPostingOrder set postedmonth='" + postedmonth + "',desgn='" + designation + "',pf=" + PF + ",esi=" + ESI + ",pt=" + PT + ",orderdt='" + orderdt + "' where orderid='" +
                        oldOrderid + "'  and  transfertype=" + transfertype + "   and  Empid='" + empid + "'";
                    int status = config.ExecuteNonQueryWithQueryAsync(strQry).Result;
                    ScriptManager.RegisterStartupScript(this, GetType(), "showlalert", "alert('Employee already working and Designation updated successfully');", true);

                    OrderId();
                    //BindData();
                    return;
                }
            }
           

                string orderid = txtorderid.Text.Trim();
                string remarks = txtremarks.Text.Trim();
                string prevUnitId = txtPrevUnitId.Text.Trim();




                string insertquery;
                try
                {
                    insertquery = string.Format("insert into  EmpPostingOrder(orderid,empid,tounitid,desgn,orderdt,joiningdt, " +
                        " relievedt,TransferType,IssuedAuthority,Remarks,PrevUnitId,pf,esi,pt,TransferStatus,postedmonth) values('{0}','{1}','{2}','{3}','{4}','{5}'," +
                        " '{6}',{7},'{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}')", orderid, empid, unitid, designation, orderdt, joindt, relivedate, transfertype,
                        "OpM000", remarks, prevUnitId, PF, ESI, PT, 1, postedmonth);
                    int status = config.ExecuteNonQueryWithQueryAsync(insertquery).Result;
                }
                catch (Exception ex)
                {
                }
                OrderId();
                FillAttendanceGrid();
                cleartransferdata();
            
        }

        private void OrderId()
        {
            string selectqueryoderid = "select max(cast(OrderId as int)) as OrderId from EmpPostingOrder ";
            DataTable dtable = config.ExecuteAdaptorAsyncWithQueryParams(selectqueryoderid).Result;
            if (dtable.Rows.Count > 0)
            {
                if (String.IsNullOrEmpty(dtable.Rows[0]["OrderId"].ToString()) == false)
                {
                    oderid = (Convert.ToInt32(dtable.Rows[0]["OrderId"].ToString())) + 1;
                    txtorderid.Text = oderid.ToString();
                }

                else
                {
                    txtorderid.Text = "1";
                }
            }
        }

        protected string GetOrderID()
        {
            string id = "1";
            string selectqueryoderid = "select max(cast(OrderId as int)) as OrderId from EmpPostingOrder ";
            DataTable dtable = config.ExecuteAdaptorAsyncWithQueryParams(selectqueryoderid).Result;
            if (dtable.Rows.Count > 0)
            {
                if (String.IsNullOrEmpty(dtable.Rows[0]["OrderId"].ToString()) == false)
                {
                    oderid = (Convert.ToInt32(dtable.Rows[0]["OrderId"].ToString())) + 1;
                    id = oderid.ToString();
                }
            }
            return id;
        }

        public int GetMonthBasedOnSelectionDateorMonth()
        {

            var testDate = 0;
            string EnteredDate = "";

            #region Validation

            if (txtmonth.Text.Trim().Length > 0)
            {

                try
                {

                    testDate = GlobalData.Instance.CheckEnteredDate(txtmonth.Text);
                    if (testDate > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('You Are Entered Invalid  DATE.Date Format Should be [DD/MM/YYYY].Ex.01/01/1990');", true);
                        return 0;
                    }
                    EnteredDate = DateTime.Parse(txtmonth.Text, CultureInfo.GetCultureInfo("en-gb")).ToString();
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('You Are Entered Invalid  DATE.Date Format Should be [DD/MM/YYYY].Ex.01/01/1990');", true);
                    return 0;
                }
            }
            #endregion


            #region  Month Get Based on the Control Selection
            int month = 0;
            if (Chk_Month.Checked == false)
            {
                month = Timings.Instance.GetIdForSelectedMonth(ddlMonth.SelectedIndex);
                //return month;
            }
            if (Chk_Month.Checked == true)
            {
                DateTime date = DateTime.Parse(txtmonth.Text, CultureInfo.GetCultureInfo("en-gb"));
                month = Timings.Instance.GetIdForEnteredMOnth(date);
                //return month;
            }
            return month;

            #endregion
        }

        protected void Chk_Month_OnCheckedChanged(object sender, EventArgs e)
        {


        }

        protected void FillAttendanceGrid()
        {

            if (ddlClientID.SelectedIndex > 0)
            {
                try
                {
                    GridView1.DataSource = null;
                    GridView1.DataBind();
                    string monthv = "";
                    string Yearv = "";

                    int month = GetMonthBasedOnSelectionDateorMonth();
                    DataTable data = new DataTable();

                    var ContractID = "";
                    DateTime LastDate = DateTime.Now;
                    if (Chk_Month.Checked == false)
                    {
                        LastDate = Timings.Instance.GetLastDayForSelectedMonth(ddlMonth.SelectedIndex);
                    }
                    if (Chk_Month.Checked == true)
                    {
                        LastDate = DateTime.Parse(txtmonth.Text, CultureInfo.GetCultureInfo("en-gb"));
                    }
                    Hashtable HtGetContractID = new Hashtable();
                    var SPNameForGetContractID = "GetContractIDBasedOnthMonth";
                    HtGetContractID.Add("@clientid", ddlClientID.SelectedValue);
                    HtGetContractID.Add("@LastDay", LastDate);
                    DataTable DTContractID = config.ExecuteAdaptorAsyncWithParams(SPNameForGetContractID, HtGetContractID).Result;

                    if (DTContractID.Rows.Count > 0)
                    {
                        ContractID = DTContractID.Rows[0]["contractid"].ToString();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('Contract Details Are Not Avaialable For This Client.');", true);
                        return;
                    }

                    var SPName = "";
                    Hashtable HTPaysheet = new Hashtable();
                    SPName = "GetindividualAttendance";

                    HTPaysheet.Add("@month", month);
                    HTPaysheet.Add("@clientid", ddlClientID.SelectedValue);

                    DataTable dt = config.ExecuteAdaptorAsyncWithParams(SPName, HTPaysheet).Result;

                    if (dt.Rows.Count > 0)
                    {
                        GridView1.DataSource = dt;
                        GridView1.DataBind();


                        if (month.ToString().Length == 4)
                        {
                            monthv = month.ToString().Substring(0, 2);
                            Yearv = "20" + month.ToString().Substring(2, 2);
                        }
                        else
                        {
                            monthv = month.ToString().Substring(0, 1);
                            Yearv = "20" + month.ToString().Substring(1, 2);
                        }

                        int days = GlobalData.Instance.GetNoOfDaysOfThisMonth(int.Parse(Yearv.Trim()), int.Parse(monthv.Trim()));


                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            string Empid = dt.Rows[i]["Empid"].ToString();
                            string desgnID = dt.Rows[i]["desgnID"].ToString();

                            string qry = "select * from empattendance where empid='" + Empid + "' and month='" + month + "' and design='" + desgnID + "' and clientid='" + ddlClientID.SelectedValue + "'  ";
                            DataTable dtry = config.ExecuteReaderWithQueryAsync(qry).Result;
                            if (dtry.Rows.Count > 0)
                            {
                                for (int j = 0; j < dtry.Rows.Count; j++)
                                {
                                    if (Empid == dtry.Rows[j]["EmpId"].ToString())
                                    {
                                        CheckBox chkcheck = GridView1.Rows[i].FindControl("chkattdendance") as CheckBox;
                                        chkcheck.Enabled = false;
                                    }
                                }
                            }
                            else
                            {
                                CheckBox chkcheck = GridView1.Rows[i].FindControl("chkattdendance") as CheckBox;
                                chkcheck.Enabled = true;
                            }
                            if (dtry.Rows.Count == 0)
                            {


                                string day1 = dt.Rows[i]["day1"].ToString();
                                string day2 = dt.Rows[i]["day2"].ToString();
                                string day3 = dt.Rows[i]["day3"].ToString();
                                string day4 = dt.Rows[i]["day4"].ToString();
                                string day5 = dt.Rows[i]["day5"].ToString();
                                string day6 = dt.Rows[i]["day6"].ToString();
                                string day7 = dt.Rows[i]["day7"].ToString();
                                string day8 = dt.Rows[i]["day8"].ToString();
                                string day9 = dt.Rows[i]["day9"].ToString();
                                string day10 = dt.Rows[i]["day10"].ToString();
                                string day11 = dt.Rows[i]["day11"].ToString();
                                string day12 = dt.Rows[i]["day12"].ToString();
                                string day13 = dt.Rows[i]["day13"].ToString();
                                string day14 = dt.Rows[i]["day14"].ToString();
                                string day15 = dt.Rows[i]["day15"].ToString();
                                string day16 = dt.Rows[i]["day16"].ToString();
                                string day17 = dt.Rows[i]["day17"].ToString();
                                string day18 = dt.Rows[i]["day18"].ToString();
                                string day19 = dt.Rows[i]["day19"].ToString();
                                string day20 = dt.Rows[i]["day20"].ToString();
                                string day21 = dt.Rows[i]["day21"].ToString();
                                string day22 = dt.Rows[i]["day22"].ToString();
                                string day23 = dt.Rows[i]["day23"].ToString();
                                string day24 = dt.Rows[i]["day24"].ToString();
                                string day25 = dt.Rows[i]["day25"].ToString();
                                string day26 = dt.Rows[i]["day26"].ToString();
                                string day27 = dt.Rows[i]["day27"].ToString();
                                string day28 = dt.Rows[i]["day28"].ToString();
                                string day29 = dt.Rows[i]["day29"].ToString();
                                string day30 = dt.Rows[i]["day30"].ToString();
                                string day31 = dt.Rows[i]["day31"].ToString();

                                TextBox txtsday1 = (TextBox)GridView1.Rows[i].FindControl("txtday1");
                                TextBox txtsday2 = (TextBox)GridView1.Rows[i].FindControl("txtday2");
                                TextBox txtsday3 = (TextBox)GridView1.Rows[i].FindControl("txtday3");
                                TextBox txtsday4 = (TextBox)GridView1.Rows[i].FindControl("txtday4");
                                TextBox txtsday5 = (TextBox)GridView1.Rows[i].FindControl("txtday5");
                                TextBox txtsday6 = (TextBox)GridView1.Rows[i].FindControl("txtday6");
                                TextBox txtsday7 = (TextBox)GridView1.Rows[i].FindControl("txtday7");
                                TextBox txtsday8 = (TextBox)GridView1.Rows[i].FindControl("txtday8");
                                TextBox txtsday9 = (TextBox)GridView1.Rows[i].FindControl("txtday9");
                                TextBox txtsday10 = (TextBox)GridView1.Rows[i].FindControl("txtday10");
                                TextBox txtsday11 = (TextBox)GridView1.Rows[i].FindControl("txtday11");
                                TextBox txtsday12 = (TextBox)GridView1.Rows[i].FindControl("txtday12");
                                TextBox txtsday13 = (TextBox)GridView1.Rows[i].FindControl("txtday13");
                                TextBox txtsday14 = (TextBox)GridView1.Rows[i].FindControl("txtday14");
                                TextBox txtsday15 = (TextBox)GridView1.Rows[i].FindControl("txtday15");
                                TextBox txtsday16 = (TextBox)GridView1.Rows[i].FindControl("txtday16");
                                TextBox txtsday17 = (TextBox)GridView1.Rows[i].FindControl("txtday17");
                                TextBox txtsday18 = (TextBox)GridView1.Rows[i].FindControl("txtday18");
                                TextBox txtsday19 = (TextBox)GridView1.Rows[i].FindControl("txtday19");
                                TextBox txtsday20 = (TextBox)GridView1.Rows[i].FindControl("txtday20");
                                TextBox txtsday21 = (TextBox)GridView1.Rows[i].FindControl("txtday21");
                                TextBox txtsday22 = (TextBox)GridView1.Rows[i].FindControl("txtday22");
                                TextBox txtsday23 = (TextBox)GridView1.Rows[i].FindControl("txtday23");
                                TextBox txtsday24 = (TextBox)GridView1.Rows[i].FindControl("txtday24");
                                TextBox txtsday25 = (TextBox)GridView1.Rows[i].FindControl("txtday25");
                                TextBox txtsday26 = (TextBox)GridView1.Rows[i].FindControl("txtday26");
                                TextBox txtsday27 = (TextBox)GridView1.Rows[i].FindControl("txtday27");
                                TextBox txtsday28 = (TextBox)GridView1.Rows[i].FindControl("txtday28");
                                TextBox txtsday29 = (TextBox)GridView1.Rows[i].FindControl("txtday29");
                                TextBox txtsday30 = (TextBox)GridView1.Rows[i].FindControl("txtday30");
                                TextBox txtsday31 = (TextBox)GridView1.Rows[i].FindControl("txtday31");

                                txtsday1.Text = day1.ToString();
                                txtsday2.Text = day2.ToString();
                                txtsday3.Text = day3.ToString();
                                txtsday4.Text = day4.ToString();
                                txtsday5.Text = day5.ToString();
                                txtsday6.Text = day6.ToString();
                                txtsday7.Text = day7.ToString();
                                txtsday8.Text = day8.ToString();
                                txtsday9.Text = day9.ToString();
                                txtsday10.Text = day10.ToString();
                                txtsday11.Text = day11.ToString();
                                txtsday12.Text = day12.ToString();
                                txtsday13.Text = day13.ToString();
                                txtsday14.Text = day14.ToString();
                                txtsday15.Text = day15.ToString();
                                txtsday16.Text = day16.ToString();
                                txtsday17.Text = day17.ToString();
                                txtsday18.Text = day18.ToString();
                                txtsday19.Text = day19.ToString();
                                txtsday20.Text = day20.ToString();
                                txtsday21.Text = day21.ToString();
                                txtsday22.Text = day22.ToString();
                                txtsday23.Text = day23.ToString();
                                txtsday24.Text = day24.ToString();
                                txtsday25.Text = day25.ToString();
                                txtsday26.Text = day26.ToString();
                                txtsday27.Text = day27.ToString();
                                txtsday28.Text = day28.ToString();
                                txtsday29.Text = day29.ToString();
                                txtsday30.Text = day30.ToString();
                                txtsday31.Text = day31.ToString();

                            }


                        }

                    }
                    else
                    {

                        int prevmonth = 0;

                        if (Chk_Month.Checked == false)
                        {

                            if (ddlMonth.SelectedIndex == 1)
                            {
                                prevmonth = Timings.Instance.GetIdForPreviousMonth();
                            }

                            if (ddlMonth.SelectedIndex == 2)
                            {
                                prevmonth = Timings.Instance.GetIdForPreviousOneMonth();
                            }

                            if (ddlMonth.SelectedIndex == 3)
                            {
                                prevmonth = Timings.Instance.GetIdForPreviousTwoMonth();
                            }

                            if (prevmonth.ToString().Length == 4)
                            {
                                monthv = prevmonth.ToString().Substring(0, 2);
                                Yearv = "20" + prevmonth.ToString().Substring(2, 2);
                            }
                            else
                            {
                                monthv = prevmonth.ToString().Substring(0, 1);
                                Yearv = "20" + prevmonth.ToString().Substring(1, 2);
                            }
                        }
                        else
                        {
                            string date = string.Empty;

                            if (txtmonth.Text.Trim().Length > 0)
                            {
                                date = DateTime.Parse(txtmonth.Text.Trim(), CultureInfo.GetCultureInfo("en-gb")).ToString();
                            }



                            monthv = DateTime.Parse(date).Month.ToString();
                            Yearv = DateTime.Parse(date).Year.ToString();

                            prevmonth = int.Parse(monthv + Yearv.Substring(2, 2));

                        }





                        int days = GlobalData.Instance.GetNoOfDaysOfThisMonth(int.Parse(Yearv), int.Parse(monthv));


                        var SPName1 = "";
                        Hashtable HTPaysheet1 = new Hashtable();
                        SPName1 = "GetPreviousmonthindividualAttendance";

                        HTPaysheet1.Add("@prevmonth", prevmonth);
                        HTPaysheet1.Add("@clientid", ddlClientID.SelectedValue);

                        DataTable dt1 = config.ExecuteAdaptorAsyncWithParams(SPName1, HTPaysheet1).Result;

                        GridView1.DataSource = dt1;
                        GridView1.DataBind();

                        for (int i = 0; i < dt1.Rows.Count; i++)
                        {
                            string Empid = dt1.Rows[i]["Empid"].ToString();
                            string day1 = dt1.Rows[i]["day1"].ToString();
                            string day2 = dt1.Rows[i]["day2"].ToString();
                            string day3 = dt1.Rows[i]["day3"].ToString();
                            string day4 = dt1.Rows[i]["day4"].ToString();
                            string day5 = dt1.Rows[i]["day5"].ToString();
                            string day6 = dt1.Rows[i]["day6"].ToString();
                            string day7 = dt1.Rows[i]["day7"].ToString();
                            string day8 = dt1.Rows[i]["day8"].ToString();
                            string day9 = dt1.Rows[i]["day9"].ToString();
                            string day10 = dt1.Rows[i]["day10"].ToString();
                            string day11 = dt1.Rows[i]["day11"].ToString();
                            string day12 = dt1.Rows[i]["day12"].ToString();
                            string day13 = dt1.Rows[i]["day13"].ToString();
                            string day14 = dt1.Rows[i]["day14"].ToString();
                            string day15 = dt1.Rows[i]["day15"].ToString();
                            string day16 = dt1.Rows[i]["day16"].ToString();
                            string day17 = dt1.Rows[i]["day17"].ToString();
                            string day18 = dt1.Rows[i]["day18"].ToString();
                            string day19 = dt1.Rows[i]["day19"].ToString();
                            string day20 = dt1.Rows[i]["day20"].ToString();
                            string day21 = dt1.Rows[i]["day21"].ToString();
                            string day22 = dt1.Rows[i]["day22"].ToString();
                            string day23 = dt1.Rows[i]["day23"].ToString();
                            string day24 = dt1.Rows[i]["day24"].ToString();
                            string day25 = dt1.Rows[i]["day25"].ToString();
                            string day26 = dt1.Rows[i]["day26"].ToString();
                            string day27 = dt1.Rows[i]["day27"].ToString();
                            string day28 = dt1.Rows[i]["day28"].ToString();
                            string day29 = dt1.Rows[i]["day29"].ToString();
                            string day30 = dt1.Rows[i]["day30"].ToString();
                            string day31 = dt1.Rows[i]["day31"].ToString();

                            TextBox txtsday1 = (TextBox)GridView1.Rows[i].FindControl("txtday1");
                            TextBox txtsday2 = (TextBox)GridView1.Rows[i].FindControl("txtday2");
                            TextBox txtsday3 = (TextBox)GridView1.Rows[i].FindControl("txtday3");
                            TextBox txtsday4 = (TextBox)GridView1.Rows[i].FindControl("txtday4");
                            TextBox txtsday5 = (TextBox)GridView1.Rows[i].FindControl("txtday5");
                            TextBox txtsday6 = (TextBox)GridView1.Rows[i].FindControl("txtday6");
                            TextBox txtsday7 = (TextBox)GridView1.Rows[i].FindControl("txtday7");
                            TextBox txtsday8 = (TextBox)GridView1.Rows[i].FindControl("txtday8");
                            TextBox txtsday9 = (TextBox)GridView1.Rows[i].FindControl("txtday9");
                            TextBox txtsday10 = (TextBox)GridView1.Rows[i].FindControl("txtday10");
                            TextBox txtsday11 = (TextBox)GridView1.Rows[i].FindControl("txtday11");
                            TextBox txtsday12 = (TextBox)GridView1.Rows[i].FindControl("txtday12");
                            TextBox txtsday13 = (TextBox)GridView1.Rows[i].FindControl("txtday13");
                            TextBox txtsday14 = (TextBox)GridView1.Rows[i].FindControl("txtday14");
                            TextBox txtsday15 = (TextBox)GridView1.Rows[i].FindControl("txtday15");
                            TextBox txtsday16 = (TextBox)GridView1.Rows[i].FindControl("txtday16");
                            TextBox txtsday17 = (TextBox)GridView1.Rows[i].FindControl("txtday17");
                            TextBox txtsday18 = (TextBox)GridView1.Rows[i].FindControl("txtday18");
                            TextBox txtsday19 = (TextBox)GridView1.Rows[i].FindControl("txtday19");
                            TextBox txtsday20 = (TextBox)GridView1.Rows[i].FindControl("txtday20");
                            TextBox txtsday21 = (TextBox)GridView1.Rows[i].FindControl("txtday21");
                            TextBox txtsday22 = (TextBox)GridView1.Rows[i].FindControl("txtday22");
                            TextBox txtsday23 = (TextBox)GridView1.Rows[i].FindControl("txtday23");
                            TextBox txtsday24 = (TextBox)GridView1.Rows[i].FindControl("txtday24");
                            TextBox txtsday25 = (TextBox)GridView1.Rows[i].FindControl("txtday25");
                            TextBox txtsday26 = (TextBox)GridView1.Rows[i].FindControl("txtday26");
                            TextBox txtsday27 = (TextBox)GridView1.Rows[i].FindControl("txtday27");
                            TextBox txtsday28 = (TextBox)GridView1.Rows[i].FindControl("txtday28");
                            TextBox txtsday29 = (TextBox)GridView1.Rows[i].FindControl("txtday29");
                            TextBox txtsday30 = (TextBox)GridView1.Rows[i].FindControl("txtday30");
                            TextBox txtsday31 = (TextBox)GridView1.Rows[i].FindControl("txtday31");

                            txtsday1.Text = day1.ToString();
                            txtsday2.Text = day2.ToString();
                            txtsday3.Text = day3.ToString();
                            txtsday4.Text = day4.ToString();
                            txtsday5.Text = day5.ToString();
                            txtsday6.Text = day6.ToString();
                            txtsday7.Text = day7.ToString();
                            txtsday8.Text = day8.ToString();
                            txtsday9.Text = day9.ToString();
                            txtsday10.Text = day10.ToString();
                            txtsday11.Text = day11.ToString();
                            txtsday12.Text = day12.ToString();
                            txtsday13.Text = day13.ToString();
                            txtsday14.Text = day14.ToString();
                            txtsday15.Text = day15.ToString();
                            txtsday16.Text = day16.ToString();
                            txtsday17.Text = day17.ToString();
                            txtsday18.Text = day18.ToString();
                            txtsday19.Text = day19.ToString();
                            txtsday20.Text = day20.ToString();
                            txtsday21.Text = day21.ToString();
                            txtsday22.Text = day22.ToString();
                            txtsday23.Text = day23.ToString();
                            txtsday24.Text = day24.ToString();
                            txtsday25.Text = day25.ToString();
                            txtsday26.Text = day26.ToString();
                            txtsday27.Text = day27.ToString();
                            txtsday28.Text = day28.ToString();
                            txtsday29.Text = day29.ToString();
                            txtsday30.Text = day30.ToString();
                            txtsday31.Text = day31.ToString();

                        }
                    }

                }
                catch (Exception ex)
                {
                }
            }

        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            DateTime LastDate = DateTime.Now;
            if (Chk_Month.Checked == false)
            {
                LastDate = Timings.Instance.GetLastDayForSelectedMonth(ddlMonth.SelectedIndex);
            }
            if (Chk_Month.Checked == true)
            {
                LastDate = DateTime.Parse(txtmonth.Text, CultureInfo.GetCultureInfo("en-gb"));
            }

            int Month = (LastDate).Month;
            int Year = (LastDate).Year;

            int noofdays = GlobalData.Instance.GetNoOfDaysOfThisMonth(Year, Month);


            if (e.Row.RowType == DataControlRowType.Header)
            {

                if (noofdays == 28)
                {

                    e.Row.Cells[34].Visible = false;
                    e.Row.Cells[35].Visible = false;
                    e.Row.Cells[36].Visible = false;


                }

                if (noofdays == 29)
                {


                  
                    e.Row.Cells[35].Visible = false;
                    e.Row.Cells[36].Visible = false;

                }

                if (noofdays == 30)
                {
                    e.Row.Cells[36].Visible = false;

                }
            }


            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                if (noofdays == 28)
                {
                    TextBox txtday29 = (TextBox)e.Row.FindControl("txtday29");
                    txtday29.Text = "";
                    TextBox txtday30 = (TextBox)e.Row.FindControl("txtday30");
                    txtday30.Text = "";
                    TextBox txtday31 = (TextBox)e.Row.FindControl("txtday31");
                    txtday31.Text = "";

                    e.Row.Cells[34].Visible = false;
                    e.Row.Cells[35].Visible = false;
                    e.Row.Cells[36].Visible = false;


                }

                if (noofdays == 29)
                {

                    TextBox txtday30 = (TextBox)e.Row.FindControl("txtday30");
                    txtday30.Text = "";
                    TextBox txtday31 = (TextBox)e.Row.FindControl("txtday31");
                    txtday31.Text = "";
                    e.Row.Cells[35].Visible = false;
                    e.Row.Cells[36].Visible = false;


                }

                if (noofdays == 30)
                {
                    TextBox txtday31 = (TextBox)e.Row.FindControl("txtday31");
                    txtday31.Text = "";
                    e.Row.Cells[36].Visible = false;

                }


            }

        }

        protected void btn_Save_AttenDanceClick(object sender, EventArgs e)
        {
            if (ddlClientID.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showlalert", "alert('Please Select Client Id');", true);
                return;
            }

            int month = 0;
            var days = 0;
            var G_Sdays = 0;
            LblResult.Text = "";
            LblResult.Visible = true;

            month = GetMonthBasedOnSelectionDateorMonth();

            var ContractID = "";
            var bPaySheetDates = 0;



            DateTime LastDate = DateTime.Now;
            if (Chk_Month.Checked == false)
            {
                LastDate = Timings.Instance.GetLastDayForSelectedMonth(ddlMonth.SelectedIndex);
            }
            if (Chk_Month.Checked == true)
            {
                LastDate = DateTime.Parse(txtmonth.Text, CultureInfo.GetCultureInfo("en-gb"));
            }

            Hashtable HtGetContractID = new Hashtable();
            var SPNameForGetContractID = "GetContractIDBasedOnthMonth";
            HtGetContractID.Add("@clientid", ddlClientID.SelectedValue);
            HtGetContractID.Add("@LastDay", LastDate);
            DataTable DTContractID = config.ExecuteAdaptorAsyncWithParams(SPNameForGetContractID, HtGetContractID).Result;

            if (DTContractID.Rows.Count > 0)
            {
                ContractID = DTContractID.Rows[0]["contractid"].ToString();
                bPaySheetDates = int.Parse(DTContractID.Rows[0]["PaySheetDates"].ToString());
            }
            else
            {
                return;
            }

            if (Chk_Month.Checked == false)
            {
                days = Timings.Instance.GetNoofDaysForSelectedMonth(ddlMonth.SelectedIndex, bPaySheetDates);
            }

            if (Chk_Month.Checked == true)
            {
                DateTime mGendays = DateTime.Now;
                DateTime date = DateTime.Parse(txtmonth.Text, CultureInfo.GetCultureInfo("en-gb"));
                mGendays = DateTime.Parse(date.ToString());
                days = Timings.Instance.GetNoofDaysForEnteredMonth(mGendays, bPaySheetDates);
            }

            G_Sdays = Timings.Instance.Get_GS_Days(month, days);



            string totaldesignationlist = string.Empty;
            if (radioindividual.Checked)
            {
                float totalDuties = 0;
                float totalOTs = 0;
                int statusatte = 0;
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    Label lblEmpId = GridView1.Rows[i].FindControl("lblEmpid") as Label;
                    Label lbldesign = GridView1.Rows[i].FindControl("lbldesignname") as Label;
                    TextBox txtDuties = GridView1.Rows[i].FindControl("txtDuties") as TextBox;
                    TextBox txtOTs = GridView1.Rows[i].FindControl("txtOTs") as TextBox;
                    TextBox txtNhs = GridView1.Rows[i].FindControl("txtNhs") as TextBox;
                    TextBox txtWos = GridView1.Rows[i].FindControl("txtWos") as TextBox;
                    TextBox txtCanteenAdv = GridView1.Rows[i].FindControl("txtCanAdv") as TextBox;
                    TextBox txtPenalty = GridView1.Rows[i].FindControl("txtPenalty") as TextBox;
                    TextBox txtIncentivs = GridView1.Rows[i].FindControl("txtIncentivs") as TextBox;
                    TextBox txttds = GridView1.Rows[i].FindControl("txttds") as TextBox;
                    TextBox txtmiscded = GridView1.Rows[i].FindControl("txtmiscded") as TextBox;
                    TextBox txtxlsheet = GridView1.Rows[i].FindControl("txtxlsheet") as TextBox;
                    TextBox txtatmded = GridView1.Rows[i].FindControl("txtatmded") as TextBox;


                    #region New Code added by venkat on 16/10/2013 for Emp Attendance for daywise

                    TextBox txtday1 = GridView1.Rows[i].FindControl("txtday1") as TextBox;
                    TextBox txtday2 = GridView1.Rows[i].FindControl("txtday2") as TextBox;
                    TextBox txtday3 = GridView1.Rows[i].FindControl("txtday3") as TextBox;
                    TextBox txtday4 = GridView1.Rows[i].FindControl("txtday4") as TextBox;
                    TextBox txtday5 = GridView1.Rows[i].FindControl("txtday5") as TextBox;
                    TextBox txtday6 = GridView1.Rows[i].FindControl("txtday6") as TextBox;
                    TextBox txtday7 = GridView1.Rows[i].FindControl("txtday7") as TextBox;
                    TextBox txtday8 = GridView1.Rows[i].FindControl("txtday8") as TextBox;
                    TextBox txtday9 = GridView1.Rows[i].FindControl("txtday9") as TextBox;
                    TextBox txtday10 = GridView1.Rows[i].FindControl("txtday10") as TextBox;
                    TextBox txtday11 = GridView1.Rows[i].FindControl("txtday11") as TextBox;
                    TextBox txtday12 = GridView1.Rows[i].FindControl("txtday12") as TextBox;
                    TextBox txtday13 = GridView1.Rows[i].FindControl("txtday13") as TextBox;
                    TextBox txtday14 = GridView1.Rows[i].FindControl("txtday14") as TextBox;
                    TextBox txtday15 = GridView1.Rows[i].FindControl("txtday15") as TextBox;
                    TextBox txtday16 = GridView1.Rows[i].FindControl("txtday16") as TextBox;
                    TextBox txtday17 = GridView1.Rows[i].FindControl("txtday17") as TextBox;
                    TextBox txtday18 = GridView1.Rows[i].FindControl("txtday18") as TextBox;
                    TextBox txtday19 = GridView1.Rows[i].FindControl("txtday19") as TextBox;
                    TextBox txtday20 = GridView1.Rows[i].FindControl("txtday20") as TextBox;
                    TextBox txtday21 = GridView1.Rows[i].FindControl("txtday21") as TextBox;
                    TextBox txtday22 = GridView1.Rows[i].FindControl("txtday22") as TextBox;
                    TextBox txtday23 = GridView1.Rows[i].FindControl("txtday23") as TextBox;
                    TextBox txtday24 = GridView1.Rows[i].FindControl("txtday24") as TextBox;
                    TextBox txtday25 = GridView1.Rows[i].FindControl("txtday25") as TextBox;
                    TextBox txtday26 = GridView1.Rows[i].FindControl("txtday26") as TextBox;
                    TextBox txtday27 = GridView1.Rows[i].FindControl("txtday27") as TextBox;
                    TextBox txtday28 = GridView1.Rows[i].FindControl("txtday28") as TextBox;
                    TextBox txtday29 = GridView1.Rows[i].FindControl("txtday29") as TextBox;
                    TextBox txtday30 = GridView1.Rows[i].FindControl("txtday30") as TextBox;
                    TextBox txtday31 = GridView1.Rows[i].FindControl("txtday31") as TextBox;

                    TextBox txtday1ot = GridView1.Rows[i].FindControl("txtday1ot") as TextBox;
                    TextBox txtday2ot = GridView1.Rows[i].FindControl("txtday2ot") as TextBox;
                    TextBox txtday3ot = GridView1.Rows[i].FindControl("txtday3ot") as TextBox;
                    TextBox txtday4ot = GridView1.Rows[i].FindControl("txtday4ot") as TextBox;
                    TextBox txtday5ot = GridView1.Rows[i].FindControl("txtday5ot") as TextBox;
                    TextBox txtday6ot = GridView1.Rows[i].FindControl("txtday6ot") as TextBox;
                    TextBox txtday7ot = GridView1.Rows[i].FindControl("txtday7ot") as TextBox;
                    TextBox txtday8ot = GridView1.Rows[i].FindControl("txtday8ot") as TextBox;
                    TextBox txtday9ot = GridView1.Rows[i].FindControl("txtday9ot") as TextBox;
                    TextBox txtday10ot = GridView1.Rows[i].FindControl("txtday10ot") as TextBox;
                    TextBox txtday11ot = GridView1.Rows[i].FindControl("txtday11ot") as TextBox;
                    TextBox txtday12ot = GridView1.Rows[i].FindControl("txtday12ot") as TextBox;
                    TextBox txtday13ot = GridView1.Rows[i].FindControl("txtday13ot") as TextBox;
                    TextBox txtday14ot = GridView1.Rows[i].FindControl("txtday14ot") as TextBox;
                    TextBox txtday15ot = GridView1.Rows[i].FindControl("txtday15ot") as TextBox;
                    TextBox txtday16ot = GridView1.Rows[i].FindControl("txtday16ot") as TextBox;
                    TextBox txtday17ot = GridView1.Rows[i].FindControl("txtday17ot") as TextBox;
                    TextBox txtday18ot = GridView1.Rows[i].FindControl("txtday18ot") as TextBox;
                    TextBox txtday19ot = GridView1.Rows[i].FindControl("txtday19ot") as TextBox;
                    TextBox txtday20ot = GridView1.Rows[i].FindControl("txtday20ot") as TextBox;
                    TextBox txtday21ot = GridView1.Rows[i].FindControl("txtday21ot") as TextBox;
                    TextBox txtday22ot = GridView1.Rows[i].FindControl("txtday22ot") as TextBox;
                    TextBox txtday23ot = GridView1.Rows[i].FindControl("txtday23ot") as TextBox;
                    TextBox txtday24ot = GridView1.Rows[i].FindControl("txtday24ot") as TextBox;
                    TextBox txtday25ot = GridView1.Rows[i].FindControl("txtday25ot") as TextBox;
                    TextBox txtday26ot = GridView1.Rows[i].FindControl("txtday26ot") as TextBox;
                    TextBox txtday27ot = GridView1.Rows[i].FindControl("txtday27ot") as TextBox;
                    TextBox txtday28ot = GridView1.Rows[i].FindControl("txtday28ot") as TextBox;
                    TextBox txtday29ot = GridView1.Rows[i].FindControl("txtday29ot") as TextBox;
                    TextBox txtday30ot = GridView1.Rows[i].FindControl("txtday30ot") as TextBox;
                    TextBox txtday31ot = GridView1.Rows[i].FindControl("txtday31ot") as TextBox;

                    #endregion New Code added by venkat on 16/10/2013 for Emp Attendance for daywise


                    float ots = 0;
                    float duties = 0;
                    float penalty = 0;
                    float CanteenAdv = 0;
                    float Incentivs = 0;
                    float WOs = 0;
                    float NHs = 0;
                    float LDays = 0;
                    float dayots = 0;

                    float tds = 0;
                    float miscded = 0;
                    float xlsheet = 0;
                    float atmded = 0;

                    #region New Code added by venkat on 16/10/2013 for Emp Attendance for daywise


                    string day1 = "0"; string day2 = "0"; string day3 = "0"; string day4 = "0"; string day5 = "0"; string day6 = "0"; string day7 = "0";
                    string day8 = "0"; string day9 = "0"; string day10 = "0"; string day11 = "0"; string day12 = "0"; string day13 = "0"; string day14 = "0";
                    string day15 = "0"; string day16 = "0"; string day17 = "0"; string day18 = "0"; string day19 = "0"; string day20 = "0"; string day21 = "0";
                    string day22 = "0"; string day23 = "0"; string day24 = "0"; string day25 = "0"; string day26 = "0"; string day27 = "0"; string day28 = "0";
                    string day29 = "0"; string day30 = "0"; string day31 = "0";


                    string day1ot = "0"; string day2ot = "0"; string day3ot = "0"; string day4ot = "0"; string day5ot = "0"; string day6ot = "0"; string day7ot = "0";
                    string day8ot = "0"; string day9ot = "0"; string day10ot = "0"; string day11ot = "0"; string day12ot = "0"; string day13ot = "0"; string day14ot = "0";
                    string day15ot = "0"; string day16ot = "0"; string day17ot = "0"; string day18ot = "0"; string day19ot = "0"; string day20ot = "0"; string day21ot = "0";
                    string day22ot = "0"; string day23ot = "0"; string day24ot = "0"; string day25ot = "0"; string day26ot = "0"; string day27ot = "0"; string day28ot = "0";
                    string day29ot = "0"; string day30ot = "0"; string day31ot = "0";

                    float dt1, dt2, dt3, dt4, dt5, dt6, dt7, dt8, dt9, dt10, dt11, dt12, dt13, dt14, dt15, dt16, dt17, dt18, dt19, dt20, dt21,
                                     dt22, dt23, dt24, dt25, dt26, dt27, dt28, dt29, dt30, dt31;

                    dt1 = dt2 = dt3 = dt4 = dt5 = dt6 = dt7 = dt8 = dt9 = dt10 = dt11 = dt12 = dt13 = dt14 = dt15 = dt16 = dt17 = dt18 = dt19 = dt20 = dt21 =
                                        dt22 = dt23 = dt24 = dt25 = dt26 = dt27 = dt28 = dt29 = dt30 = dt31 = 0;

                    float ot1, ot2, ot3, ot4, ot5, ot6, ot7, ot8, ot9, ot10, ot11, ot12, ot13, ot14, ot15, ot16, ot17, ot18, ot19, ot20, ot21,
                                        ot22, ot23, ot24, ot25, ot26, ot27, ot28, ot29, ot30, ot31, otP21,
                                        otP22, otP23, otP24, otP25, otP26, otP27, otP28, otP29, otP30, otP31;


                    ot1 = ot2 = ot3 = ot4 = ot5 = ot6 = ot7 = ot8 = ot9 = ot10 = ot11 = ot12 = ot13 = ot14 = ot15 = ot16 = ot17 = ot18 = ot19 = ot20 = ot21 =
                        ot22 = ot23 = ot24 = ot25 = ot26 = ot27 = ot28 = ot29 = ot30 = ot31 = otP21 =
                        otP22 = otP23 = otP24 = otP25 = otP26 = otP27 = otP28 = otP29 = otP30 = otP31 = 0;


                    float Wo1, Wo2, Wo3, Wo4, Wo5, Wo6, Wo7, Wo8, Wo9, Wo10, Wo11, Wo12, Wo13, Wo14, Wo15, Wo16, Wo17, Wo18, Wo19, Wo20, Wo21,
                                     Wo22, Wo23, Wo24, Wo25, Wo26, Wo27, Wo28, Wo29, Wo30, Wo31, WoP21,
                                     WoP22, WoP23, WoP24, WoP25, WoP26, WoP27, WoP28, WoP29, WoP30, WoP31;

                    Wo1 = Wo2 = Wo3 = Wo4 = Wo5 = Wo6 = Wo7 = Wo8 = Wo9 = Wo10 = Wo11 = Wo12 = Wo13 = Wo14 = Wo15 = Wo16 = Wo17 = Wo18 = Wo19 = Wo20 = Wo21 =
                                        Wo22 = Wo23 = Wo24 = Wo25 = Wo26 = Wo27 = Wo28 = Wo29 = Wo30 = Wo31 = WoP21 =
                                        WoP22 = WoP23 = WoP24 = WoP25 = WoP26 = WoP27 = WoP28 = WoP29 = WoP30 = WoP31 = 0;

                    float NHs1, NHs2, NHs3, NHs4, NHs5, NHs6, NHs7, NHs8, NHs9, NHs10, NHs11, NHs12, NHs13, NHs14, NHs15, NHs16, NHs17, NHs18, NHs19, NHs20, NHs21,
                                    NHs22, NHs23, NHs24, NHs25, NHs26, NHs27, NHs28, NHs29, NHs30, NHs31, NHsP21,
                                    NHsP22, NHsP23, NHsP24, NHsP25, NHsP26, NHsP27, NHsP28, NHsP29, NHsP30, NHsP31;

                    NHs1 = NHs2 = NHs3 = NHs4 = NHs5 = NHs6 = NHs7 = NHs8 = NHs9 = NHs10 = NHs11 = NHs12 = NHs13 = NHs14 = NHs15 = NHs16 = NHs17 = NHs18 = NHs19 = NHs20 = NHs21 =
                                        NHs22 = NHs23 = NHs24 = NHs25 = NHs26 = NHs27 = NHs28 = NHs29 = NHs30 = NHs31 = NHsP21 =
                                        NHsP22 = NHsP23 = NHsP24 = NHsP25 = NHsP26 = NHsP27 = NHsP28 = NHsP29 = NHsP30 = NHsP31 = 0;

                    float LDay1, LDay2, LDay3, LDay4, LDay5, LDay6, LDay7, LDay8, LDay9, LDay10, LDay11, LDay12, LDay13, LDay14, LDay15, LDay16, LDay17, LDay18, LDay19, LDay20, LDay21,
                                   LDay22, LDay23, LDay24, LDay25, LDay26, LDay27, LDay28, LDay29, LDay30, LDay31, LDayp21,
                                   LDayp22, LDayp23, LDayp24, LDayp25, LDayp26, LDayp27, LDayp28, LDayp29, LDayp30, LDayp31;

                    LDay1= LDay2= LDay3= LDay4= LDay5= LDay6= LDay7= LDay8= LDay9= LDay10= LDay11= LDay12= LDay13= LDay14= LDay15=LDay16= LDay17=LDay18= LDay19= LDay20= LDay21=
                                   LDay22= LDay23= LDay24= LDay25= LDay26= LDay27= LDay28= LDay29= LDay30= LDay31 =LDayp21=
                                   LDayp22= LDayp23= LDayp24= LDayp25= LDayp26= LDayp27= LDayp28= LDayp29= LDayp30= LDayp31 = 0;


                    #region for duties

                    #region for Duties Alpha

                    if (txtday1.Text.Trim().Length > 0)
                    {
                        day1 = txtday1.Text;
                    }

                    if (txtday2.Text.Trim().Length > 0)
                    {
                        day2 = txtday2.Text;
                    }
                    if (txtday3.Text.Trim().Length > 0)
                    {
                        day3 = txtday3.Text;
                    }
                    if (txtday4.Text.Trim().Length > 0)
                    {
                        day4 = txtday4.Text;
                    }
                    if (txtday5.Text.Trim().Length > 0)
                    {
                        day5 = txtday5.Text;
                    }
                    if (txtday6.Text.Trim().Length > 0)
                    {
                        day6 = txtday6.Text;
                    }
                    if (txtday7.Text.Trim().Length > 0)
                    {
                        day7 = txtday7.Text;
                    }
                    if (txtday8.Text.Trim().Length > 0)
                    {
                        day8 = txtday8.Text;
                    }
                    if (txtday9.Text.Trim().Length > 0)
                    {
                        day9 = txtday9.Text;
                    }
                    if (txtday10.Text.Trim().Length > 0)
                    {
                        day10 = txtday10.Text;
                    }
                    if (txtday11.Text.Trim().Length > 0)
                    {
                        day11 = txtday11.Text;
                    }
                    if (txtday12.Text.Trim().Length > 0)
                    {
                        day12 = txtday12.Text;
                    }
                    if (txtday13.Text.Trim().Length > 0)
                    {
                        day13 = txtday13.Text;
                    }
                    if (txtday14.Text.Trim().Length > 0)
                    {
                        day14 = txtday14.Text;
                    }
                    if (txtday15.Text.Trim().Length > 0)
                    {
                        day15 = txtday15.Text;
                    }
                    if (txtday16.Text.Trim().Length > 0)
                    {
                        day16 = txtday16.Text;
                    }
                    if (txtday17.Text.Trim().Length > 0)
                    {
                        day17 = txtday17.Text;
                    }
                    if (txtday18.Text.Trim().Length > 0)
                    {
                        day18 = txtday18.Text;
                    }
                    if (txtday19.Text.Trim().Length > 0)
                    {
                        day19 = txtday19.Text;
                    }
                    if (txtday20.Text.Trim().Length > 0)
                    {
                        day20 = txtday20.Text;
                    }
                    if (txtday21.Text.Trim().Length > 0)
                    {
                        day21 = txtday21.Text;
                    }
                    if (txtday22.Text.Trim().Length > 0)
                    {
                        day22 = txtday22.Text;
                    }
                    if (txtday23.Text.Trim().Length > 0)
                    {
                        day23 = txtday23.Text;
                    }
                    if (txtday24.Text.Trim().Length > 0)
                    {
                        day24 = txtday24.Text;
                    }
                    if (txtday25.Text.Trim().Length > 0)
                    {
                        day25 = txtday25.Text;
                    }
                    if (txtday26.Text.Trim().Length > 0)
                    {
                        day26 = txtday26.Text;
                    }
                    if (txtday27.Text.Trim().Length > 0)
                    {
                        day27 = txtday27.Text;
                    }
                    if (txtday28.Text.Trim().Length > 0)
                    {
                        day28 = txtday28.Text;
                    }
                    if (txtday29.Text.Trim().Length > 0)
                    {
                        day29 = txtday29.Text;
                    }
                    if (txtday30.Text.Trim().Length > 0)
                    {
                        day30 = txtday30.Text;
                    }
                    if (txtday31.Text.Trim().Length > 0)
                    {
                        day31 = txtday31.Text;
                    }

                    #endregion

                    #region Values for Duties

                    //1

                    if (day1.Trim() == "P" || day1.Trim() == "p" )
                    {
                        dt1 = 1;
                    }
                    if (day1.Trim() == "E" || day1.Trim() == "e" || day1.Trim() == "C" || day1.Trim() == "c" || day1.Trim() == "CO" || day1.Trim() == "co" || day1.Trim() == "HL" || day1.Trim() == "hl")
                    {
                        dt1 = 1;
                    }
                    if (day1.Trim() == "H" || day1.Trim() == "h")
                    {
                        dt1 = 1f;
                    }

                    if (day1.Trim() == "W" || day1.Trim() == "w")
                    {
                        Wo1 = 1;
                    }
                    if (day1.Trim() == "L" || day1.Trim() == "l")
                    {
                        LDay1 = 1;
                    }

                    if (day1.Trim() == "A" || day1.Trim() == "a" || day1.Trim() == "E" || day1.Trim() == "e")
                    {
                        dt1 = 0;
                    }
                    if (day1.Trim() == "N" || day1.Trim() == "n" || day1.Trim() == "D" || day1.Trim() == "d")
                    {
                        NHs1 = 1;
                    }
                  


                    //2
                    if (day2.Trim() == "P" || day2.Trim() == "p" )
                    {
                        dt2 = 1;
                    }
                    if (day2.Trim() == "E" || day2.Trim() == "e" || day2.Trim() == "C" || day2.Trim() == "c" || day2.Trim() == "C" || day2.Trim() == "c" || day2.Trim() == "H" || day2.Trim() == "h")
                    {
                        dt2 = 1;
                    }
                    if (day2.Trim() == "H" || day2.Trim() == "h")
                    {
                        dt2 = 1f;
                    }
                    if (day2.Trim() == "W" || day2.Trim() == "w")
                    {
                        Wo2 = 1;
                    }
                    if (day2.Trim() == "L" || day2.Trim() == "l")
                    {
                        LDay2 = 1;
                    }
                    if (day2.Trim() == "A" || day2.Trim() == "a" || day2.Trim() == "E" || day2.Trim() == "e")
                    {
                        dt2 = 0;
                    }
                    if (day2.Trim() == "N" || day2.Trim() == "n" || day2.Trim() == "D" || day2.Trim() == "d")
                    {
                        NHs2 = 1;
                    }



                    //3
                    if (day3.Trim() == "P" || day3.Trim() == "p" )
                    {
                        dt3 = 1;
                    }
                    if (day3.Trim() == "E" || day3.Trim() == "e" || day3.Trim() == "C" || day3.Trim() == "c" || day3.Trim() == "CO" || day3.Trim() == "co" || day3.Trim() == "HL" || day3.Trim() == "hl")
                    {
                        dt3 = 1;
                    }
                    if (day3.Trim() == "H" || day3.Trim() == "h")
                    {
                        dt3 = 1f;
                    }

                    if (day3.Trim() == "W" || day3.Trim() == "w")
                    {
                        Wo3 = 1;
                    }
                    if (day3.Trim() == "L" || day3.Trim() == "l")
                    {
                        LDay3 = 1;
                    }
                    if (day3.Trim() == "A" || day3.Trim() == "a" || day3.Trim() == "E" || day3.Trim() == "e")
                    {
                        dt3 = 0;
                    }
                    if (day3.Trim() == "N" || day3.Trim() == "n" || day3.Trim() == "D" || day3.Trim() == "d")
                    {
                        NHs3 = 1;
                    }



                    //4
                    if (day4.Trim() == "P" || day4.Trim() == "p" )
                    {
                        dt4 = 1;
                    }
                    if (day4.Trim() == "E" || day4.Trim() == "e" || day4.Trim() == "C" || day4.Trim() == "c" || day4.Trim() == "CO" || day4.Trim() == "co" || day4.Trim() == "HL" || day4.Trim() == "hl")
                    {
                        dt4 = 1;
                    }
                    if (day4.Trim() == "H" || day4.Trim() == "h")
                    {
                        dt4 = 1f;
                    }

                    if (day4.Trim() == "W" || day4.Trim() == "w")
                    {
                        Wo4 = 1;
                    }
                    if (day4.Trim() == "L" || day4.Trim() == "l")
                    {
                        LDay4 = 1;
                    }
                    if (day4.Trim() == "A" || day4.Trim() == "a" || day4.Trim() == "E" || day4.Trim() == "e")
                    {
                        dt4 = 0;
                    }
                    if (day4.Trim() == "N" || day4.Trim() == "n" || day4.Trim() == "D" || day4.Trim() == "d")
                    {
                        NHs4 = 1;
                    }

                    //5
                    if (day5.Trim() == "P" || day5.Trim() == "p"  )
                    {
                        dt5 = 1;
                    }
                    if (day5.Trim() == "E" || day5.Trim() == "e" || day5.Trim() == "C" || day5.Trim() == "c" || day5.Trim() == "CO" || day5.Trim() == "co" || day5.Trim() == "HL" || day5.Trim() == "hl")
                    {
                        dt5 = 1;
                    }
                    if (day5.Trim() == "H" || day5.Trim() == "h")
                    {
                        dt5 = 1f;
                    }
                    if (day5.Trim() == "W" || day5 == "w")
                    {
                        Wo5 = 1;
                    }
                    if (day5.Trim() == "L" || day5.Trim() == "l")
                    {
                        LDay5 = 1;
                    }
                    if (day5.Trim() == "A" || day5.Trim() == "a" || day5.Trim() == "E" || day5.Trim() == "e")
                    {
                        dt5 = 0;
                    }
                    if (day5.Trim() == "N" || day5.Trim() == "n" || day5.Trim() == "D" || day5.Trim() == "d")
                    {
                        NHs5 = 1;
                    }

                    //6
                    if (day6.Trim() == "P" || day6.Trim() == "p" )
                    {
                        dt6 = 1;
                    }
                    if (day6.Trim() == "E" || day6.Trim() == "e" || day6.Trim() == "C" || day6.Trim() == "c" || day6.Trim() == "CO" || day6.Trim() == "co" || day6.Trim() == "HL" || day6.Trim() == "hl")
                    {
                        dt6 = 1;
                    }
                    if (day6.Trim() == "H" || day6.Trim() == "h")
                    {
                        dt6 = 1f;
                    }

                    if (day6.Trim() == "W" || day6.Trim() == "w")
                    {
                        Wo6 = 1;
                    }
                    if (day6.Trim() == "L" || day6.Trim() == "l")
                    {
                        LDay6 = 1;
                    }
                    if (day6.Trim() == "A" || day6.Trim() == "a" || day6.Trim() == "E" || day6.Trim() == "e")
                    {
                        dt6 = 0;
                    }
                    if (day6.Trim() == "N" || day6.Trim() == "n" || day6.Trim() == "D" || day6.Trim() == "d")
                    {
                        NHs6 = 1;
                    }

                    //7
                    if (day7.Trim() == "P" || day7.Trim() == "p" )
                    {
                        dt7 = 1;
                    }
                    if (day7.Trim() == "E" || day7.Trim() == "e" || day7.Trim() == "C" || day7.Trim() == "c" || day7.Trim() == "CO" || day7.Trim() == "co" || day7.Trim() == "HL" || day7.Trim() == "hl")
                    {
                        dt7 = 1;
                    }
                    if (day7.Trim() == "H" || day7.Trim() == "h")
                    {
                        dt7 = 1f;
                    }
                    if (day7.Trim() == "W" || day7.Trim() == "w")
                    {
                        Wo7 = 1;
                    }
                    if (day7.Trim() == "L" || day7.Trim() == "l")
                    {
                        LDay7 = 1;
                    }
                    if (day7.Trim() == "A" || day7.Trim() == "a" || day7.Trim() == "E" || day7.Trim() == "e")
                    {
                        dt7 = 0;
                    }
                    if (day7.Trim() == "N" || day7.Trim() == "n" || day7.Trim() == "D" || day7.Trim() == "d")
                    {
                        NHs7 = 1;
                    }
                    //8
                    if (day8.Trim() == "P" || day8.Trim() == "p" )
                    {
                        dt8 = 1;
                    }
                    if (day8.Trim() == "E" || day8.Trim() == "e" || day8.Trim() == "C" || day8.Trim() == "c" || day8.Trim() == "CO" || day8.Trim() == "co" || day8.Trim() == "HL" || day8.Trim() == "hl")
                    {
                        dt8 = 1;
                    }
                    if (day8.Trim() == "H" || day8.Trim() == "h")
                    {
                        dt8 = 1f;
                    }
                    if (day8.Trim() == "W" || day8.Trim() == "w")
                    {
                        Wo8 = 1;
                    }
                    if (day8.Trim() == "L" || day8.Trim() == "l")
                    {
                        LDay8 = 1;
                    }
                    if (day8.Trim() == "A" || day8.Trim() == "a" || day8.Trim() == "E" || day8.Trim() == "e")
                    {
                        dt8 = 0;
                    }
                    if (day8.Trim() == "N" || day8.Trim() == "n" || day8.Trim() == "D" || day8.Trim() == "d")
                    {
                        NHs8 = 1;
                    }

                    //9
                    if (day9.Trim() == "P" || day9.Trim() == "p" )
                    {
                        dt9 = 1;
                    }
                    if (day9.Trim() == "E" || day9.Trim() == "e" || day9.Trim() == "C" || day9.Trim() == "c" || day9.Trim() == "CO" || day9.Trim() == "co" || day9.Trim() == "HL" || day9.Trim() == "hl")
                    {
                        dt9 = 1;
                    }
                    if (day9.Trim() == "H" || day9.Trim() == "h")
                    {
                        dt9 = 1f;
                    }

                    if (day9.Trim() == "W" || day9.Trim() == "w")
                    {
                        Wo9 = 1;
                    }
                    if (day9.Trim() == "L" || day9.Trim() == "l")
                    {
                        LDay9 = 1;
                    }
                    if (day9.Trim() == "A" || day9.Trim() == "a" || day9.Trim() == "E" || day9.Trim() == "e")
                    {
                        dt9 = 0;
                    }
                    if (day9.Trim() == "N" || day9.Trim() == "n" || day9.Trim() == "D" || day9.Trim() == "d")
                    {
                        NHs9 = 1;
                    }

                    //10
                    if (day10.Trim() == "P" || day10.Trim() == "p" )
                    {
                        dt10 = 1;
                    }
                    if (day10.Trim() == "E" || day10.Trim() == "e" || day10.Trim() == "C" || day10.Trim() == "c" || day10.Trim() == "CO" || day10.Trim() == "co" || day10.Trim() == "HL" || day10.Trim() == "hl")
                    {
                        dt10 = 1;
                    }
                    if (day10.Trim() == "H" || day10.Trim() == "h")
                    {
                        dt10 = 1f;
                    }
                    if (day10.Trim() == "W" || day10.Trim() == "w")
                    {
                        Wo10 = 1;
                    }
                    if (day10.Trim() == "L" || day10.Trim() == "l")
                    {
                        LDay10 = 1;
                    }
                    if (day10.Trim() == "A" || day10.Trim() == "a" || day10.Trim() == "E" || day10.Trim() == "e")
                    {
                        dt10 = 0;
                    }
                    if (day10.Trim() == "N" || day10.Trim() == "n" || day10.Trim() == "D" || day10.Trim() == "d")
                    {
                        NHs10 = 1;
                    }
                    //11

                    if (day11.Trim() == "P" || day11.Trim() == "p" )
                    {
                        dt11 = 1;
                    }
                    if (day11.Trim() == "E" || day11.Trim() == "e" || day11.Trim() == "C" || day11.Trim() == "c" || day11.Trim() == "CO" || day11.Trim() == "co" || day11.Trim() == "HL" || day11.Trim() == "hl")
                    {
                        dt11 = 1;
                    }
                    if (day11.Trim() == "H" || day11.Trim() == "h")
                    {
                        dt11 = 1f;
                    }
                    if (day11.Trim() == "W" || day11.Trim() == "w")
                    {
                        Wo11 = 1;
                    }
                    if (day11.Trim() == "L" || day11.Trim() == "l")
                    {
                        LDay11 = 1;
                    }
                    if (day11.Trim() == "A" || day11.Trim() == "a" || day11.Trim() == "E" || day11.Trim() == "e")
                    {
                        dt11 = 0;
                    }
                    if (day11.Trim() == "N" || day11.Trim() == "n" || day11.Trim() == "D" || day11.Trim() == "d")
                    {
                        NHs11 = 1;
                    }
                    //12
                    if (day12.Trim() == "P" || day12.Trim() == "p" )
                    {
                        dt12 = 1;
                    }
                    if (day12.Trim() == "E" || day12.Trim() == "e" || day12.Trim() == "C" || day12.Trim() == "c" || day12.Trim() == "CO" || day12.Trim() == "co" || day12.Trim() == "HL" || day12.Trim() == "hl")
                    {
                        dt12 = 1;
                    }
                    if (day12.Trim() == "H" || day12.Trim() == "h")
                    {
                        dt12 = 1f;
                    }
                    if (day12.Trim() == "W" || day12.Trim() == "w")
                    {
                        Wo12 = 1;
                    }
                    if (day12.Trim() == "L" || day12.Trim() == "l")
                    {
                        LDay12 = 1;
                    }
                    if (day12.Trim() == "A" || day12.Trim() == "a" || day12.Trim() == "E" || day12.Trim() == "e")
                    {
                        dt12 = 0;
                    }
                    if (day12.Trim() == "N" || day12.Trim() == "n" || day12.Trim() == "D" || day12.Trim() == "d")
                    {
                        NHs12 = 1;
                    }
                    //13
                    if (day13.Trim() == "P" || day13.Trim() == "p" )
                    {
                        dt13 = 1;
                    }
                    if (day13.Trim() == "E" || day13.Trim() == "e" || day13.Trim() == "C" || day13.Trim() == "c" || day13.Trim() == "CO" || day13.Trim() == "co" || day13.Trim() == "HL" || day13.Trim() == "hl")
                    {
                        dt13 = 1;
                    }
                    if (day13.Trim() == "H" || day13.Trim() == "h")
                    {
                        dt13 = 1f;
                    }
                    if (day13.Trim() == "W" || day13.Trim() == "w")
                    {
                        Wo13 = 1;
                    }
                    if (day13.Trim() == "L" || day13.Trim() == "l")
                    {
                        LDay13 = 1;
                    }
                    if (day13.Trim() == "A" || day13.Trim() == "a" || day13.Trim() == "E" || day13.Trim() == "e")
                    {
                        dt13 = 0;
                    }
                    if (day13 == "N" || day13 == "n" || day13.Trim() == "D" || day13.Trim() == "d")
                    {
                        NHs13 = 1;
                    }
                    //14
                    if (day14.Trim() == "P" || day14.Trim() == "p" )
                    {
                        dt14 = 1;
                    }
                    if (day14.Trim() == "E" || day14.Trim() == "e" || day14.Trim() == "C" || day14.Trim() == "c" || day14.Trim() == "CO" || day14.Trim() == "co" || day14.Trim() == "HL" || day14.Trim() == "hl")
                    {
                        dt14 = 1;
                    }
                    if (day14.Trim() == "H" || day14.Trim() == "h")
                    {
                        dt14 = 1f;
                    }
                    if (day14.Trim() == "W" || day14.Trim() == "w")
                    {
                        Wo14 = 1;
                    }
                    if (day14.Trim() == "L" || day14.Trim() == "l")
                    {
                        LDay14 = 1;
                    }
                    if (day14.Trim() == "A" || day14.Trim() == "a" || day14.Trim() == "E" || day14.Trim() == "e")
                    {
                        dt14 = 0;
                    }
                    if (day14.Trim() == "N" || day14.Trim() == "n" || day14.Trim() == "D" || day14.Trim() == "d")
                    {
                        NHs14 = 1;
                    }
                    //15
                    if (day15.Trim() == "P" || day15.Trim() == "p" )
                    {
                        dt15 = 1;
                    }
                    if (day15.Trim() == "E" || day15.Trim() == "e" || day15.Trim() == "C" || day15.Trim() == "c" || day15.Trim() == "CO" || day15.Trim() == "co" || day15.Trim() == "HL" || day15.Trim() == "hl")
                    {
                        dt15 = 1;
                    }
                    if (day15.Trim() == "H" || day15.Trim() == "h")
                    {
                        dt15 = 1f;
                    }
                    if (day15.Trim() == "W" || day15.Trim() == "w")
                    {
                        Wo15 = 1;
                    }
                    if (day15.Trim() == "L" || day15.Trim() == "l")
                    {
                        LDay15 = 1;
                    }
                    if (day15.Trim() == "A" || day15.Trim() == "a" || day15.Trim() == "E" || day15.Trim() == "e")
                    {
                        dt15 = 0;
                    }
                    if (day15.Trim() == "N" || day15.Trim() == "n" || day15.Trim() == "D" || day15.Trim() == "d")
                    {
                        NHs15 = 1;
                    }
                    //16
                    if (day16.Trim() == "P" || day16.Trim() == "p" )
                    {
                        dt16 = 1;
                    }
                    if (day16.Trim() == "E" || day16.Trim() == "e" || day16.Trim() == "C" || day16.Trim() == "c" || day16.Trim() == "CO" || day16.Trim() == "co" || day16.Trim() == "HL" || day16.Trim() == "hl")
                    {
                        dt16 = 1;
                    }
                    if (day16.Trim() == "H" || day16.Trim() == "h")
                    {
                        dt16 = 1f;
                    }
                    if (day16.Trim() == "W" || day16.Trim() == "w")
                    {
                        Wo16 = 1;
                    }
                    if (day16.Trim() == "L" || day16.Trim() == "l")
                    {
                        LDay16 = 1;
                    }
                    if (day16.Trim() == "A" || day16.Trim() == "a" || day16.Trim() == "E" || day16.Trim() == "e")
                    {
                        dt16 = 0;
                    }
                    if (day16.Trim() == "N" || day16.Trim() == "n" || day16.Trim() == "D" || day16.Trim() == "d")
                    {
                        NHs16 = 1;
                    }
                    //17
                    if (day17.Trim() == "P" || day17.Trim() == "p" )
                    {
                        dt17 = 1;
                    }
                    if (day17.Trim() == "E" || day17.Trim() == "e" || day17.Trim() == "C" || day17.Trim() == "c" || day17.Trim() == "CO" || day17.Trim() == "co" || day17.Trim() == "HL" || day17.Trim() == "hl")
                    {
                        dt17 = 1;
                    }
                    if (day17.Trim() == "H" || day17.Trim() == "h")
                    {
                        dt17 = 1f;
                    }
                    if (day17.Trim() == "W" || day17.Trim() == "w")
                    {
                        Wo17 = 1;
                    }
                    if (day17.Trim() == "L" || day17.Trim() == "l")
                    {
                        LDay17 = 1;
                    }
                    if (day17.Trim() == "A" || day17.Trim() == "a" || day17.Trim() == "E" || day17.Trim() == "e")
                    {
                        dt17 = 0;
                    }
                    if (day17.Trim() == "N" || day17.Trim() == "n" || day17.Trim() == "D" || day17.Trim() == "d")
                    {
                        NHs17 = 1;
                    }
                    //18
                    if (day18.Trim() == "P" || day18.Trim() == "p" )
                    {
                        dt18 = 1;
                    }
                    if (day18.Trim() == "E" || day18.Trim() == "e" || day18.Trim() == "C" || day18.Trim() == "c" || day18.Trim() == "CO" || day18.Trim() == "co" || day18.Trim() == "HL" || day18.Trim() == "hl")
                    {
                        dt18 = 1;
                    }
                    if (day18.Trim() == "H" || day18.Trim() == "h")
                    {
                        dt18 = 1f;
                    }
                    if (day18.Trim() == "W" || day18.Trim() == "w")
                    {
                        Wo18 = 1;
                    }
                    if (day18.Trim() == "L" || day18.Trim() == "l")
                    {
                        LDay18 = 1;
                    }
                    if (day18.Trim() == "A" || day18.Trim() == "a" || day18.Trim() == "E" || day18.Trim() == "e")
                    {
                        dt18 = 0;
                    }
                    if (day18.Trim() == "N" || day18.Trim() == "n" || day18.Trim() == "D" || day18.Trim() == "d")
                    {
                        NHs18 = 1;
                    }
                    //19
                    if (day19.Trim() == "P" || day19.Trim() == "p" )
                    {
                        dt19 = 1;
                    }
                    if (day19.Trim() == "E" || day19.Trim() == "e" || day19.Trim() == "C" || day19.Trim() == "c" || day19.Trim() == "CO" || day19.Trim() == "co" || day19.Trim() == "HL" || day19.Trim() == "hl")
                    {
                        dt19 = 1;
                    }
                    if (day19.Trim() == "H" || day19.Trim() == "h")
                    {
                        dt19 = 1f;
                    }
                    if (day19.Trim() == "W" || day19.Trim() == "w")
                    {
                        Wo19 = 1;
                    }
                    if (day19.Trim() == "L" || day19.Trim() == "l")
                    {
                        LDay19 = 1;
                    }
                    if (day19.Trim() == "A" || day19.Trim() == "a" || day19.Trim() == "E" || day19.Trim() == "e")
                    {
                        dt19 = 0;
                    }
                    if (day19.Trim() == "N" || day19.Trim() == "n" || day19.Trim() == "D" || day19.Trim() == "d")
                    {
                        NHs19 = 1;
                    }
                    //20
                    if (day20.Trim() == "P" || day20.Trim() == "p" )
                    {
                        dt20 = 1;
                    }
                    if (day20.Trim() == "E" || day20.Trim() == "e" || day20.Trim() == "C" || day20.Trim() == "c" || day20.Trim() == "CO" || day20.Trim() == "co" || day20.Trim() == "HL" || day20.Trim() == "hl")
                    {
                        dt20 = 1;
                    }
                    if (day20.Trim() == "H" || day20.Trim() == "h")
                    {
                        dt20 = 1f;
                    }
                    if (day20.Trim() == "W" || day20.Trim() == "w")
                    {
                        Wo20 = 1;
                    }
                    if (day20.Trim() == "L" || day20.Trim() == "l")
                    {
                        LDay20 = 1;
                    }
                    if (day20.Trim() == "A" || day20.Trim() == "a" || day20.Trim() == "E" || day20.Trim() == "e")
                    {
                        dt20 = 0;
                    }
                    if (day20.Trim() == "N" || day20.Trim() == "n" || day20.Trim() == "D" || day20.Trim() == "d")
                    {
                        NHs20 = 1;
                    }
                    //21
                    if (day21.Trim() == "P" || day21.Trim() == "p" )
                    {
                        dt21 = 1;
                    }
                    if (day21.Trim() == "E" || day21.Trim() == "e" || day21.Trim() == "C" || day21.Trim() == "c" || day21.Trim() == "CO" || day21.Trim() == "co" || day21.Trim() == "HL" || day21.Trim() == "hl")
                    {
                        dt21 = 1;
                    }
                    if (day21.Trim() == "H" || day21.Trim() == "h")
                    {
                        dt21 = 1f;
                    }
                    if (day21.Trim() == "W" || day21.Trim() == "w")
                    {
                        Wo21 = 1;
                    }
                    if (day21.Trim() == "L" || day21.Trim() == "l")
                    {
                        LDay21 = 1;
                    }
                    if (day21.Trim() == "A" || day21.Trim() == "a" || day21.Trim() == "E" || day21.Trim() == "e")
                    {
                        dt21 = 0;
                    }
                    if (day21.Trim() == "N" || day21.Trim() == "n" || day21.Trim() == "D" || day21.Trim() == "d")
                    {
                        NHs21 = 1;
                    }
                    //22
                    if (day22.Trim() == "P" || day22.Trim() == "p" )
                    {
                        dt22 = 1;
                    }
                    if (day22.Trim() == "E" || day22.Trim() == "e" || day22.Trim() == "C" || day22.Trim() == "c" || day22.Trim() == "CO" || day22.Trim() == "co" || day22.Trim() == "HL" || day22.Trim() == "hl")
                    {
                        dt22 = 1;
                    }
                    if (day22.Trim() == "H" || day22.Trim() == "h")
                    {
                        dt22 = 1f;
                    }
                    if (day22.Trim() == "W" || day22.Trim() == "w")
                    {
                        Wo22 = 1;
                    }
                    if (day22.Trim() == "L" || day22.Trim() == "l")
                    {
                        LDay22 = 1;
                    }
                    if (day22.Trim() == "A" || day22.Trim() == "a" || day22.Trim() == "E" || day22.Trim() == "e")
                    {
                        dt22 = 0;
                    }
                    if (day22.Trim() == "N" || day22.Trim() == "n" || day22.Trim() == "D" || day22.Trim() == "d")
                    {
                        NHs22 = 1;
                    }
                    //23
                    if (day23.Trim() == "P" || day23.Trim() == "p" )
                    {
                        dt23 = 1;
                    }
                    if (day23.Trim() == "E" || day23.Trim() == "e" || day23.Trim() == "C" || day23.Trim() == "c" || day23.Trim() == "CO" || day23.Trim() == "co" || day23.Trim() == "HL" || day23.Trim() == "hl")
                    {
                        dt23 = 1;
                    }
                    if (day23.Trim() == "H" || day23.Trim() == "h")
                    {
                        dt23 = 1f;
                    }
                    if (day23.Trim() == "W" || day23.Trim() == "w")
                    {
                        Wo23 = 1;
                    }
                    if (day23.Trim() == "L" || day23.Trim() == "l")
                    {
                        LDay23 = 1;
                    }
                    if (day23.Trim() == "A" || day23.Trim() == "a" || day23.Trim() == "E" || day23.Trim() == "e")
                    {
                        dt23 = 0;
                    }
                    if (day23.Trim() == "N" || day23.Trim() == "n" || day23.Trim() == "D" || day23.Trim() == "d")
                    {
                        NHs23 = 1;
                    }
                    //24
                    if (day24.Trim() == "P" || day24.Trim() == "p")
                    {
                        dt24 = 1;
                    }
                    if (day24.Trim() == "E" || day24.Trim() == "e" || day24.Trim() == "C" || day24.Trim() == "c" || day24.Trim() == "CO" || day24.Trim() == "co" || day24.Trim() == "HL" || day24.Trim() == "hl")
                    {
                        dt24 = 1;
                    }
                    if (day24.Trim() == "H" || day24.Trim() == "h")
                    {
                        dt24 = 1f;
                    }
                    if (day24.Trim() == "W" || day24.Trim() == "w")
                    {
                        Wo24 = 1;
                    }
                    if (day24.Trim() == "L" || day24.Trim() == "l")
                    {
                        LDay24 = 1;
                    }
                    if (day24.Trim() == "A" || day24.Trim() == "a" || day24.Trim() == "E" || day24.Trim() == "e")
                    {
                        dt24 = 0;
                    }
                    if (day24.Trim() == "N" || day24.Trim() == "n" || day24.Trim() == "D" || day24.Trim() == "d")
                    {
                        NHs24 = 1;
                    }
                    //25
                    if (day25.Trim() == "P" || day25.Trim() == "p" )
                    {
                        dt25 = 1;
                    }
                    if (day25.Trim() == "E" || day25.Trim() == "e" || day25.Trim() == "C" || day25.Trim() == "c" || day25.Trim() == "CO" || day25.Trim() == "co" || day25.Trim() == "HL" || day25.Trim() == "hl")
                    {
                        dt25 = 1;
                    }
                    if (day25.Trim() == "H" || day25.Trim() == "h")
                    {
                        dt25 = 1f;
                    }
                    if (day25.Trim() == "W" || day25.Trim() == "w")
                    {
                        Wo25 = 1;
                    }
                    if (day25.Trim() == "L" || day25.Trim() == "l")
                    {
                        LDay25 = 1;
                    }
                    if (day25.Trim() == "A" || day25.Trim() == "a" || day25.Trim() == "E" || day25.Trim() == "e")
                    {
                        dt25 = 0;
                    }
                    if (day25.Trim() == "N" || day25.Trim() == "n" || day25.Trim() == "D" || day25.Trim() == "d")
                    {
                        NHs25 = 1;
                    }
                    //26
                    if (day26.Trim() == "P" || day26.Trim() == "p" )
                    {
                        dt26 = 1;
                    }
                    if (day26.Trim() == "E" || day26.Trim() == "e" || day26.Trim() == "C" || day26.Trim() == "c" || day26.Trim() == "CO" || day26.Trim() == "co" || day26.Trim() == "HL" || day26.Trim() == "hl")
                    {
                        dt26 = 1;
                    }
                    if (day26.Trim() == "H" || day26.Trim() == "h")
                    {
                        dt26 = 1f;
                    }
                    if (day26.Trim() == "W" || day26.Trim() == "w")
                    {
                        Wo26 = 1;
                    }
                    if (day26.Trim() == "L" || day26.Trim() == "l")
                    {
                        LDay26 = 1;
                    }
                    if (day26.Trim() == "A" || day26.Trim() == "a" || day26.Trim() == "E" || day26.Trim() == "e")
                    {
                        dt26 = 0;
                    }
                    if (day26.Trim() == "N" || day26.Trim() == "n" || day26.Trim() == "D" || day26.Trim() == "d")
                    {
                        NHs26 = 1;
                    }
                    //27
                    if (day27.Trim() == "P" || day27.Trim() == "p" )
                    {
                        dt27 = 1;
                    }
                    if (day27.Trim() == "E" || day27.Trim() == "e" || day27.Trim() == "C" || day27.Trim() == "c" || day27.Trim() == "CO" || day27.Trim() == "co" || day27.Trim() == "HL" || day27.Trim() == "hl")
                    {
                        dt27 = 1;
                    }
                    if (day27.Trim() == "H" || day27.Trim() == "h")
                    {
                        dt27 = 1f;
                    }
                    if (day27.Trim() == "W" || day27.Trim() == "w")
                    {
                        Wo27 = 1;
                    }
                    if (day27.Trim() == "L" || day27.Trim() == "l")
                    {
                        LDay27= 1;
                    }
                    if (day27.Trim() == "A" || day27.Trim() == "a" || day27.Trim() == "E" || day27.Trim() == "e")
                    {
                        dt27 = 0;
                    }
                    if (day27.Trim() == "N" || day27.Trim() == "n" || day27.Trim() == "D" || day27.Trim() == "d")
                    {
                        NHs27 = 1;
                    }
                    //28
                    if (day28.Trim() == "P" || day28.Trim() == "p" )
                    {
                        dt28 = 1;
                    }
                    if (day28.Trim() == "E" || day28.Trim() == "e" || day28.Trim() == "C" || day28.Trim() == "c" || day28.Trim() == "CO" || day28.Trim() == "co" || day28.Trim() == "HL" || day28.Trim() == "hl")
                    {
                        dt28 = 1;
                    }
                    if (day28.Trim() == "H" || day28.Trim() == "h")
                    {
                        dt28 = 1f;
                    }
                    if (day28.Trim() == "W" || day28.Trim() == "w")
                    {
                        Wo28 = 1;
                    }
                    if (day28.Trim() == "L" || day28.Trim() == "l")
                    {
                        LDay28 = 1;
                    }
                    if (day28.Trim() == "A" || day28.Trim() == "a" || day28.Trim() == "E" || day28.Trim() == "e")
                    {
                        dt28 = 0;
                    }
                    if (day28.Trim() == "N" || day28.Trim() == "n" || day28.Trim() == "D" || day28.Trim() == "d")
                    {
                        NHs28 = 1;
                    }
                    //29
                    if (day29.Trim() == "P" || day29.Trim() == "p")
                    {
                        dt29 = 1;
                    }
                    if (day29.Trim() == "E" || day29.Trim() == "e" || day29.Trim() == "C" || day29.Trim() == "c" || day29.Trim() == "CO" || day29.Trim() == "co" || day29.Trim() == "HL" || day29.Trim() == "hl")
                    {
                        dt29 = 1;
                    }
                    if (day29.Trim() == "H" || day29.Trim() == "h")
                    {
                        dt29 = 1f;
                    }
                    if (day29.Trim() == "W" || day29.Trim() == "w")
                    {
                        Wo29 = 1;
                    }
                    if (day29.Trim() == "L" || day29.Trim() == "l")
                    {
                        LDay29 = 1;
                    }
                    if (day29.Trim() == "A" || day29.Trim() == "a" || day29.Trim() == "E" || day29.Trim() == "e")
                    {
                        dt29 = 0;
                    }
                    if (day29.Trim() == "N" || day29.Trim() == "n" || day29.Trim() == "D" || day29.Trim() == "d")
                    {
                        NHs29 = 1;
                    }
                    //30
                    if (day30.Trim() == "P" || day30.Trim() == "p" )
                    {
                        dt30 = 1;
                    }
                    if (day30.Trim() == "E" || day30.Trim() == "e" || day30.Trim() == "C" || day30.Trim() == "c" || day30.Trim() == "CO" || day30.Trim() == "co" || day30.Trim() == "HL" || day30.Trim() == "hl")
                    {
                        dt30 = 1;
                    }
                    if (day30.Trim() == "H" || day30.Trim() == "h")
                    {
                        dt30 = 1f;
                    }
                    if (day30.Trim() == "W" || day30.Trim() == "w")
                    {
                        Wo30 = 1;
                    }
                    if (day30.Trim() == "L" || day30.Trim() == "l")
                    {
                        LDay30 = 1;
                    }
                    if (day30.Trim() == "A" || day30.Trim() == "a" || day30.Trim() == "E" || day30.Trim() == "e")
                    {
                        dt30 = 0;
                    }
                    if (day30.Trim() == "N" || day30.Trim() == "n" || day30.Trim() == "D" || day30.Trim() == "d")
                    {
                        NHs30 = 1;
                    }
                    //31
                    if (day31.Trim() == "P" || day31.Trim() == "p" )
                    {
                        dt31 = 1;
                    }
                    if (day31.Trim() == "E" || day31.Trim() == "e" || day31.Trim() == "C" || day31.Trim() == "c" || day31.Trim() == "CO" || day31.Trim() == "co" || day31.Trim() == "HL" || day31.Trim() == "hl")
                    {
                        dt31 = 1;
                    }
                    if (day31.Trim() == "H" || day31.Trim() == "h")
                    {
                        dt31 = 1f;
                    }
                    if (day31.Trim() == "W" || day31.Trim() == "w")
                    {
                        Wo31 = 1;
                    }
                    if (day31.Trim() == "L" || day31.Trim() == "l")
                    {
                        LDay31 = 1;
                    }
                    if (day31.Trim() == "A" || day31.Trim() == "a" || day31.Trim() == "E" || day31.Trim() == "e")
                    {
                        dt31 = 0;
                    }
                    if (day31.Trim() == "N" || day31.Trim() == "n" || day31.Trim() == "D" || day31.Trim() == "d")
                    {
                        NHs31 = 1;
                    }


                    #endregion Values for Duties

                    #endregion for duties

                    #region for OTs



                    #region

                    if (txtday1ot.Text.Trim().Length > 0)
                    {
                        day1ot = txtday1ot.Text;
                    }
                    if (txtday2ot.Text.Trim().Length > 0)
                    {
                        day2ot = txtday2ot.Text;
                    }
                    if (txtday3ot.Text.Trim().Length > 0)
                    {
                        day3ot = txtday3ot.Text;
                    }
                    if (txtday4ot.Text.Trim().Length > 0)
                    {
                        day4ot = txtday4ot.Text;
                    }
                    if (txtday5ot.Text.Trim().Length > 0)
                    {
                        day5ot = txtday5ot.Text;
                    }
                    if (txtday6ot.Text.Trim().Length > 0)
                    {
                        day6ot = txtday6ot.Text;
                    }
                    if (txtday7ot.Text.Trim().Length > 0)
                    {
                        day7ot = txtday7ot.Text;
                    }
                    if (txtday8ot.Text.Trim().Length > 0)
                    {
                        day8ot = txtday8ot.Text;
                    }
                    if (txtday9ot.Text.Trim().Length > 0)
                    {
                        day9ot = txtday9ot.Text;
                    }
                    if (txtday10ot.Text.Trim().Length > 0)
                    {
                        day10ot = txtday10ot.Text;
                    }
                    if (txtday11ot.Text.Trim().Length > 0)
                    {
                        day11ot = txtday11ot.Text;
                    }
                    if (txtday12ot.Text.Trim().Length > 0)
                    {
                        day12ot = txtday12ot.Text;
                    }
                    if (txtday13ot.Text.Trim().Length > 0)
                    {
                        day13ot = txtday13ot.Text;
                    }
                    if (txtday14ot.Text.Trim().Length > 0)
                    {
                        day14ot = txtday14ot.Text;
                    }
                    if (txtday15ot.Text.Trim().Length > 0)
                    {
                        day15ot = txtday15ot.Text;
                    }
                    if (txtday16ot.Text.Trim().Length > 0)
                    {
                        day16ot = txtday16ot.Text;
                    }
                    if (txtday17ot.Text.Trim().Length > 0)
                    {
                        day17ot = txtday17ot.Text;
                    }
                    if (txtday18ot.Text.Trim().Length > 0)
                    {
                        day18ot = txtday18ot.Text;
                    }
                    if (txtday19ot.Text.Trim().Length > 0)
                    {
                        day19ot = txtday19ot.Text;
                    }
                    if (txtday20ot.Text.Trim().Length > 0)
                    {
                        day20ot = txtday20ot.Text;
                    }
                    if (txtday21ot.Text.Trim().Length > 0)
                    {
                        day21ot = txtday21ot.Text;
                    }
                    if (txtday22ot.Text.Trim().Length > 0)
                    {
                        day22ot = txtday22ot.Text;
                    }
                    if (txtday23ot.Text.Trim().Length > 0)
                    {
                        day23ot = txtday23ot.Text;
                    }
                    if (txtday24ot.Text.Trim().Length > 0)
                    {
                        day24ot = txtday24ot.Text;
                    }
                    if (txtday25ot.Text.Trim().Length > 0)
                    {
                        day25ot = txtday25ot.Text;
                    }
                    if (txtday26ot.Text.Trim().Length > 0)
                    {
                        day26ot = txtday26ot.Text;
                    }
                    if (txtday27ot.Text.Trim().Length > 0)
                    {
                        day27ot = txtday27ot.Text;
                    }
                    if (txtday28ot.Text.Trim().Length > 0)
                    {
                        day28ot = txtday28ot.Text;
                    }
                    if (txtday29ot.Text.Trim().Length > 0)
                    {
                        day29ot = txtday29ot.Text;
                    }
                    if (txtday30ot.Text.Trim().Length > 0)
                    {
                        day30ot = txtday30ot.Text;
                    }
                    if (txtday31ot.Text.Trim().Length > 0)
                    {
                        day31ot = txtday31ot.Text;
                    }

                    #endregion


                    dayots = float.Parse(day1ot) + float.Parse(day2ot) + float.Parse(day3ot) + float.Parse(day4ot) + float.Parse(day5ot) + float.Parse(day6ot) + float.Parse(day7ot) +
                             float.Parse(day8ot) + float.Parse(day8ot) + float.Parse(day10ot) + float.Parse(day11ot) + float.Parse(day12ot) + float.Parse(day13ot) + float.Parse(day14ot) +
                             float.Parse(day15ot) + float.Parse(day16ot) + float.Parse(day17ot) + float.Parse(day18ot) + float.Parse(day19ot) + float.Parse(day20ot) + float.Parse(day21ot) +
                             float.Parse(day22ot) + float.Parse(day23ot) + float.Parse(day24ot) + float.Parse(day25ot) + float.Parse(day26ot) + float.Parse(day27ot) + float.Parse(day28ot) +
                             float.Parse(day29ot) + float.Parse(day30ot) + float.Parse(day31ot);

                    ots = dayots;

                    #endregion for OTs

                    #endregion New Code added by venkat on 16/10/2013 for Emp Attendance for daywise

                    if (txtOTs.Text.Trim().Length > 0)
                    {
                        ots = Convert.ToSingle(txtOTs.Text);
                    }

                    if (txtPenalty.Text.Trim().Length > 0)
                    {
                        penalty = Convert.ToSingle(txtPenalty.Text);
                    }
                    if (txtCanteenAdv.Text.Trim().Length > 0)
                    {
                        CanteenAdv = Convert.ToSingle(txtCanteenAdv.Text);
                    }

                    if (txtIncentivs.Text.Trim().Length > 0)
                    {
                        Incentivs = Convert.ToSingle(txtIncentivs.Text);
                    }


                    totalDuties += duties;
                    totalOTs += ots;


                    duties = dt1 + dt2 + dt3 + dt4 + dt5 + dt6 + dt7 + dt8 + dt9 + dt10 + dt11 + dt12 + dt13 + dt14 + dt15 + dt16 + dt17 + dt18 + dt19 + dt20 + dt21 +
                                 dt22 + dt23 + dt24 + dt25 + dt26 + dt27 + dt28 + dt29 + dt30 + dt31;

                    //ots = ot1 + ot2 + ot3 + ot4 + ot5 + ot6 + ot7 + ot8 + ot9 + ot10 + ot11 + ot12 + ot13 + ot14 + ot15 + ot16 + ot17 + ot18 + ot19 + ot20 + ot21 +
                    //           ot22 + ot23 + ot24 + ot25 + ot26 + ot27 + ot28 + ot29 + ot30 + ot31 + otP21 +
                    //           otP22 + otP23 + otP24 + otP25 + otP26 + otP27 + otP28 + otP29 + otP30 + otP31;

                    WOs = Wo1 + Wo2 + Wo3 + Wo4 + Wo5 + Wo6 + Wo7 + Wo8 + Wo9 + Wo10 + Wo11 + Wo12 + Wo13 + Wo14 + Wo15 + Wo16 + Wo17 + Wo18 + Wo19 + Wo20 + Wo21 +
                               Wo22 + Wo23 + Wo24 + Wo25 + Wo26 + Wo27 + Wo28 + Wo29 + Wo30 + Wo31 + WoP21 +
                               WoP22 + WoP23 + WoP24 + WoP25 + WoP26 + WoP27 + WoP28 + WoP29 + WoP30 + WoP31;

                    LDays = LDay1 + LDay2 + LDay3 + LDay4 + LDay5 + LDay6 + LDay7 + LDay8 + LDay9 + LDay10 + LDay11 + LDay12 + LDay13 + LDay14 + LDay15 + LDay16 + LDay17 + LDay18 + LDay19 + LDay20 + LDay21 +
                             LDay22 + LDay23 + LDay24 + LDay25 + LDay26 + LDay27 + LDay28 + LDay29 + LDay30 + LDay31 + LDayp21 +
                             LDayp22 + LDayp23 + LDayp24 + LDayp25 + LDayp26 + LDayp27 + LDayp28 + LDayp29 + LDayp30 + LDayp31;

                    NHs = NHs1 + NHs2 + NHs3 + NHs4 + NHs5 + NHs6 + NHs7 + NHs8 + NHs9 + NHs10 + NHs11 + NHs12 + NHs13 + NHs14 + NHs15 + NHs16 + NHs17 + NHs18 + NHs19 + NHs20 + NHs21 +
                              NHs22 + NHs23 + NHs24 + NHs25 + NHs26 + NHs27 + NHs28 + NHs29 + NHs30 + NHs31 + NHsP21 +
                              NHsP22 + NHsP23 + NHsP24 + NHsP25 + NHsP26 + NHsP27 + NHsP28 + NHsP29 + NHsP30 + NHsP31;



                    string sqlqry = "Select * from  Empattendance  Where Empid='" + lblEmpId.Text + "' and month='" + month +
                        "'  and ClientId='" + ddlClientID.SelectedValue + "'    and  Design='" + lbldesign.Text + "'";

                    DataTable dtattestemp = config.ExecuteAdaptorAsyncWithQueryParams(sqlqry).Result;
                    int Status = 0;
                    string updatequery = "";

                    if (dtattestemp.Rows.Count > 0)
                    {
                        string design = dtattestemp.Rows[0]["design"].ToString();

                        updatequery = string.Format("update EmpAttendance set NoofDuties={0},OT={1},Penalty={2}," +
                           " CanteenAdv={6},Incentivs={7},Design='{8}',day1='{9}',day2='{10}',day3='{11}',day4='{12}',day5='{13}',day6='{14}',day7='{15}',day8='{16}',day9='{17}',day10='{18}'," +
                               "day11='{19}',day12='{20}',day13='{21}',day14='{22}',day15='{23}',day16='{24}',day17='{25}',day18='{26}',day19='{27}',day20='{28}',day21='{29}',day22='{30}',day23='{31}'," +
                               "day24='{32}',day25='{33}',day26='{34}',day27='{35}',day28='{36}',day29='{37}',day30='{38}',day31='{39}',day1ot='{40}',day2ot='{41}',day3ot='{42}',day4ot='{43}',day5ot='{44}'," +
                               " day6ot='{45}',day7ot='{46}',day8ot='{47}',day9ot='{48}',day10ot='{49}'," +
                               "day11ot='{50}',day12ot='{51}',day13ot='{52}',day14ot='{53}',day15ot='{54}',day16ot='{55}',day17ot='{56}',day18ot='{57}'," +
                               " day19ot='{58}',day20ot='{59}',day21ot='{60}',day22ot='{61}',day23ot='{62}'," +
                               "day24ot='{63}',day25ot='{64}',day26ot='{65}',day27ot='{66}',day28ot='{67}',day29ot='{68}',day30ot='{69}',day31ot='{70}',Modify_On='{71}',Modify_By='{72}',WO='{73}',Nhs='{74}',LDays='{79}' " +
                                                   " Where empid='{3}' And ClientId='{4}' And Month={5}  and  Design='" + lbldesign.Text + "'",
                                                    duties+ LDays, ots, penalty, lblEmpId.Text, ddlClientID.SelectedValue,
                                                    month, CanteenAdv, Incentivs, lbldesign.Text, day1, day2, day3, day4, day5, day6, day7, day8, day9, day10, day11, day12,
                                                    day13, day14, day15, day16, day17, day18, day19, day20, day21, day22, day23, day24, day25, day26, day27, day28, day29, day30, day31,
                                                    day1ot, day2ot, day3ot, day4ot, day5ot, day6ot, day7ot, day8ot, day9ot, day10ot, day11ot, day12ot,
                                                    day13ot, day14ot, day15ot, day16ot, day17ot, day18ot, day19ot, day20ot, day21ot, day22ot,
                                                    day23ot, day24ot, day25ot, day26ot, day27ot, day28ot, day29ot, day30ot, day31ot, DateTime.Now.ToString("MM/dd/yyyy"), EmpIDPrefix, WOs, NHs, tds, miscded, xlsheet, atmded, LDays);


                        Status = config.ExecuteNonQueryWithQueryAsync(updatequery).Result;
                    }
                    else
                    {
                        if ((duties > 0 || LDays > 0) || NHs > 0 || WOs > 0 || ots > 0 || penalty > 0 || CanteenAdv > 0 ||  Incentivs > 0 )
                        {

                            string insertquery = "insert  EmpAttendance(NoofDuties,OT,Penalty,empid,clientid,month,CanteenAdv,Incentivs,Design,WO,Nhs,day1,day2,day3,day4,day5,day6,day7,day8,day9," +
                               "day10,day11,day12,day13,day14,day15,day16,day17,day18,day19,day20,day21,day22,day23,day24,day25,day26,day27,day28,day29,day30,day31," +
                               " day1ot,day2ot,day3ot,day4ot,day5ot,day6ot,day7ot,day8ot,day9ot,day10ot,day11ot,day12ot,day13ot,day14ot,day15ot,day16ot,day17ot,day18ot,day19ot," +
                               " day20ot,day21ot,day22ot,day23ot,day24ot,day25ot,day26ot,day27ot,day28ot,day29ot,day30ot,day31ot,Created_By,ContractId,DateCreated,LDays)  " +
                               " values('" + duties + LDays + "','" + ots + "','" + penalty + "','" + lblEmpId.Text + "','" + ddlClientID.SelectedValue + "','" + month + "','" + CanteenAdv + "', " +
                               "'" + Incentivs + "','" + lbldesign.Text + "','" + WOs + "','" + NHs + "','" + day1 + "','" + day2 + "','" + day3 + "','" + day4 + "','" + day5 + "', " +
                               "'" + day6 + "','" + day7 + "','" + day8 + "','" + day9 + "','" + day10 + "','" + day11 + "','" + day12 + "','" + day13 + "','" + day14 + "', " +
                               "'" + day15 + "','" + day16 + "','" + day17 + "','" + day18 + "','" + day19 + "','" + day20 + "','" + day21 + "','" + day22 + "', " +
                               "'" + day23 + "','" + day24 + "','" + day25 + "','" + day26 + "','" + day27 + "','" + day28 + "','" + day29 + "','" + day30 + "','" + day31 + "', " +
                               "'" + day1ot + "','" + day2ot + "','" + day3ot + "','" + day4ot + "','" + day5ot + "','" + day6ot + "','" + day7ot + "','" + day8ot + "', " +
                               "'" + day9ot + "','" + day10ot + "','" + day11ot + "','" + day12ot + "','" + day13ot + "','" + day14ot + "', " +
                               "'" + day15ot + "','" + day16ot + "','" + day17ot + "','" + day18ot + "','" + day19ot + "','" + day20ot + "','" + day21ot + "', " +
                               "'" + day22ot + "','" + day23ot + "','" + day24ot + "','" + day25ot + "','" + day26ot + "','" + day27ot + "', " +
                               "'" + day28ot + "','" + day29ot + "','" + day30ot + "','" + day31ot + "','" + EmpIDPrefix + "','" + ContractID + "','" + DateTime.Now.ToString("MM/dd/yyyy") + "','" + LDays + "')";


                            Status = config.ExecuteNonQueryWithQueryAsync(insertquery).Result;

                        }
                    }

                    if (Status != 0)
                    {
                        if (statusatte == 0)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "showlalert", "alert('Attendance Added Successfully');", true);
                            statusatte = 1;
                        }

                    }
                }

            }
            else
            {
            }
            FillAttendanceGrid();
        }

        protected void radioindividual_CheckedChanged(object sender, EventArgs e)
        {
            lblTotalDuties.Text = "";
            lblTotalOts.Text = "";
            if (radioindividual.Checked)
            {
                displaydata();
            }
        }

        protected void Btn_Cancel_AttenDance_Click(object sender, EventArgs e)
        {
            ClearData();
        }

        protected void lbtn_Export_Click(object sender, EventArgs e)
        {
            if (ddlClientID.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showlalert", "alert('Please Select UnitId');", true);
                return;
            }

            if (Chk_Month.Checked == false)
            {
                if (ddlMonth.SelectedIndex == 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showlalert", "alert('Please Select Month');", true);
                    return;
                }
            }
            else
            {
                if (txtmonth.Text.Length == 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showlalert", "alert('Please Select Month');", true);
                    return;
                }
            }

            int month = GetMonthBasedOnSelectionDateorMonth();

            string qry = "select * from empattendance where clientid='" + ddlClientID.SelectedValue + "' and month='" + month + "'";
            DataTable dt = config.ExecuteAdaptorAsyncWithQueryParams(qry).Result;
            if (dt.Rows.Count > 0)
            {
                SampleGrid.DataSource = dt;
                SampleGrid.DataBind();
            }
            else
            {
                SampleGrid.DataSource = null;
                SampleGrid.DataBind();

            }


            util.Export(ddlClientID.SelectedValue + " Attendance.xls", this.SampleGrid);
        }

        protected void SampleGrid_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            var ContractID = "";
            var bPaySheetDates = 0;



            DateTime LastDate = DateTime.Now;
            if (Chk_Month.Checked == false)
            {
                LastDate = Timings.Instance.GetLastDayForSelectedMonth(ddlMonth.SelectedIndex);
            }
            if (Chk_Month.Checked == true)
            {
                LastDate = DateTime.Parse(txtmonth.Text, CultureInfo.GetCultureInfo("en-gb"));
            }

            Hashtable HtGetContractID = new Hashtable();
            var SPNameForGetContractID = "GetContractIDBasedOnthMonth";
            HtGetContractID.Add("@clientid", ddlClientID.SelectedValue);
            HtGetContractID.Add("@LastDay", LastDate);
            DataTable DTContractID = config.ExecuteAdaptorAsyncWithParams(SPNameForGetContractID, HtGetContractID).Result;

            if (DTContractID.Rows.Count > 0)
            {
                ContractID = DTContractID.Rows[0]["contractid"].ToString();
                bPaySheetDates = int.Parse(DTContractID.Rows[0]["PaySheetDates"].ToString());
            }


            if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.Footer || e.Row.RowType == DataControlRowType.DataRow)
            {



                if (bPaySheetDates == 0)
                {

                    e.Row.Cells[4].Visible = false;
                    e.Row.Cells[5].Visible = false;
                    e.Row.Cells[6].Visible = false;
                    e.Row.Cells[7].Visible = false;
                    e.Row.Cells[8].Visible = false;
                    e.Row.Cells[9].Visible = false;
                    e.Row.Cells[10].Visible = false;
                    e.Row.Cells[11].Visible = false;
                    e.Row.Cells[12].Visible = false;
                    e.Row.Cells[13].Visible = false;
                    e.Row.Cells[14].Visible = false;


                }

                if (bPaySheetDates == 2)
                {

                    e.Row.Cells[4].Visible = false;
                    e.Row.Cells[5].Visible = false;
                    e.Row.Cells[6].Visible = false;
                    e.Row.Cells[7].Visible = false;
                    e.Row.Cells[8].Visible = false;
                    e.Row.Cells[40].Visible = false;
                    e.Row.Cells[41].Visible = false;
                    e.Row.Cells[42].Visible = false;
                    e.Row.Cells[43].Visible = false;
                    e.Row.Cells[44].Visible = false;
                    e.Row.Cells[45].Visible = false;

                }

                if (bPaySheetDates == 3)
                {

                    e.Row.Cells[35].Visible = false;
                    e.Row.Cells[36].Visible = false;
                    e.Row.Cells[37].Visible = false;
                    e.Row.Cells[38].Visible = false;
                    e.Row.Cells[39].Visible = false;
                    e.Row.Cells[40].Visible = false;
                    e.Row.Cells[41].Visible = false;
                    e.Row.Cells[42].Visible = false;
                    e.Row.Cells[43].Visible = false;
                    e.Row.Cells[44].Visible = false;
                    e.Row.Cells[45].Visible = false;

                }
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[1].Attributes.Add("class", "text");

            }
        }

        protected void TxtEmpid_TextChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "IsPostBack", "var isPostBack = true;", true);
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "IsPostBack", "var isPostBack = false;", true);
            }

            txtrelivingdate.Text = "";
            txtremarks.Text = "";
            if (TxtEmpid.Text.Length > 0)
            {

                GetEmpName();
            }
            else
            {
                cleartransferdata();
            }
        }

        protected void TxtEmpName_TextChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "IsPostBack", "var isPostBack = true;", true);
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "IsPostBack", "var isPostBack = false;", true);
            }

            if (TxtEmpName.Text.Length > 0)
            {
                GetEmpid();
            }
            else
            {
                cleartransferdata();
                // MessageLabel.Text = "Plese Select Employee Name";
            }
        }

        protected void GetEmpName()
        {

            string Sqlqry = "select Empid,EmpDesgn,(empfname+' '+empmname+' '+emplname) as Name from empdetails  where empid='" + TxtEmpid.Text + "'";
            DataTable dt = config.ExecuteAdaptorAsyncWithQueryParams(Sqlqry).Result;
            if (dt.Rows.Count > 0)
            {
                try
                {
                    TxtEmpName.Text = dt.Rows[0]["Name"].ToString();
                    ddlDesignation.SelectedValue = dt.Rows[0]["EmpDesgn"].ToString();

                }
                catch (Exception ex)
                {
                    // MessageLabel.Text = ex.Message;
                }
            }
            else
            {
                // MessageLabel.Text = "There Is No Name For The Selected Employee";
            }

        }

        protected void GetEmpid()
        {

            #region  Old Code
            string Sqlqry = "select Empid,EmpDesgn,(empfname+' '+empmname+' '+emplname) as Name  from empdetails  where (empfname+' '+empmname+' '+emplname) ='" + TxtEmpName.Text + "'";
            DataTable dt = config.ExecuteAdaptorAsyncWithQueryParams(Sqlqry).Result;
            if (dt.Rows.Count > 0)
            {
                try
                {
                    TxtEmpid.Text = dt.Rows[0]["Empid"].ToString();
                    ddlDesignation.SelectedValue = dt.Rows[0]["EmpDesgn"].ToString();

                }
                catch (Exception ex)
                {
                    // MessageLabel.Text = ex.Message;
                }
            }
            else
            {
                // MessageLabel.Text = "There Is No Name For The Selected Employee";
            }
            #endregion // End Old Code

        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (ddlClientID.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showlalert", "alert('Please Select Client Id');", true);
                return;
            }

            int month = 0;
            month = GetMonthBasedOnSelectionDateorMonth();

            Label Empid = GridView1.Rows[e.RowIndex].FindControl("lblEmpid") as Label;
            Label DesginId = GridView1.Rows[e.RowIndex].FindControl("lbldesignname") as Label;

            string deletequery = "delete EmpAttendance where ClientId='" + ddlClientID.SelectedValue + "' and empid='" + Empid.Text + "' And Design='" + DesginId.Text + "' and MONTH='" + month + "'";
            int deletestatus = config.ExecuteNonQueryWithQueryAsync(deletequery).Result;
            var updatequery = "update EmpPostingOrder set RelieveMonth = " + month + " where EmpId = '" + Empid.Text + "' and ToUnitId = '" + ddlClientID.SelectedValue + "' and Desgn = '" + DesginId.Text + "'";
            int Updatestatus = config.ExecuteNonQueryWithQueryAsync(updatequery).Result;

            if (deletestatus != 0)
            {
            }
            else
            {
            }
            FillAttendanceGrid();
        }

        protected void chkattdendance_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkattendance = sender as CheckBox;
            GridViewRow row = null;
            if (chkattendance.Checked == true)
            {
                row = (GridViewRow)chkattendance.NamingContainer;
                if (row == null)
                    return;
                TextBox txtsday1 = row.FindControl("txtday1") as TextBox;
                TextBox txtsday2 = row.FindControl("txtday2") as TextBox;
                TextBox txtsday3 = row.FindControl("txtday3") as TextBox;
                TextBox txtsday4 = row.FindControl("txtday4") as TextBox;
                TextBox txtsday5 = row.FindControl("txtday5") as TextBox;
                TextBox txtsday6 = row.FindControl("txtday6") as TextBox;
                TextBox txtsday7 = row.FindControl("txtday7") as TextBox;
                TextBox txtsday8 = row.FindControl("txtday8") as TextBox;
                TextBox txtsday9 = row.FindControl("txtday9") as TextBox;
                TextBox txtsday10 = row.FindControl("txtday10") as TextBox;
                TextBox txtsday11 = row.FindControl("txtday11") as TextBox;
                TextBox txtsday12 = row.FindControl("txtday12") as TextBox;
                TextBox txtsday13 = row.FindControl("txtday13") as TextBox;
                TextBox txtsday14 = row.FindControl("txtday14") as TextBox;
                TextBox txtsday15 = row.FindControl("txtday15") as TextBox;
                TextBox txtsday16 = row.FindControl("txtday16") as TextBox;
                TextBox txtsday17 = row.FindControl("txtday17") as TextBox;
                TextBox txtsday18 = row.FindControl("txtday18") as TextBox;
                TextBox txtsday19 = row.FindControl("txtday19") as TextBox;
                TextBox txtsday20 = row.FindControl("txtday20") as TextBox;
                TextBox txtsday21 = row.FindControl("txtday21") as TextBox;
                TextBox txtsday22 = row.FindControl("txtday22") as TextBox;
                TextBox txtsday23 = row.FindControl("txtday23") as TextBox;
                TextBox txtsday24 = row.FindControl("txtday24") as TextBox;
                TextBox txtsday25 = row.FindControl("txtday25") as TextBox;
                TextBox txtsday26 = row.FindControl("txtday26") as TextBox;
                TextBox txtsday27 = row.FindControl("txtday27") as TextBox;
                TextBox txtsday28 = row.FindControl("txtday28") as TextBox;
                TextBox txtsday29 = row.FindControl("txtday29") as TextBox;
                TextBox txtsday30 = row.FindControl("txtday30") as TextBox;
                TextBox txtsday31 = row.FindControl("txtday31") as TextBox;
                TextBox txtduties = row.FindControl("txtDuties") as TextBox;
                TextBox txtots = row.FindControl("txtOTs") as TextBox;
                TextBox txtnhss = row.FindControl("txtNhs") as TextBox;
                TextBox txtwos = row.FindControl("txtWos") as TextBox;
                TextBox txtxls = row.FindControl("txtxlsheet") as TextBox;
                TextBox txttduties = row.FindControl("txttotduties") as TextBox;
                TextBox txtcanteenadv = row.FindControl("txtCanAdv") as TextBox;
                TextBox txtPenalty = row.FindControl("txtPenalty") as TextBox;
                TextBox txtIncentivs = row.FindControl("txtIncentivs") as TextBox;
                TextBox txttds = row.FindControl("txttds") as TextBox;
                TextBox txtmiscded = row.FindControl("txtmiscded") as TextBox;
                TextBox txtatmded = row.FindControl("txtatmded") as TextBox;

                txtsday1.Text = txtsday2.Text = txtsday3.Text = txtsday4.Text = txtsday5.Text = txtsday6.Text = txtsday7.Text = txtsday8.Text = txtsday9.Text = txtsday10.Text = txtsday11.Text = txtsday12.Text = txtsday12.Text =
                    txtsday13.Text = txtsday14.Text = txtsday15.Text = txtsday16.Text = txtsday17.Text = txtsday18.Text = txtsday19.Text = txtsday20.Text = txtsday21.Text = txtsday22.Text = txtsday23.Text = txtsday24.Text = txtsday25.Text = txtsday26.Text = txtsday27.Text = txtsday28.Text = txtsday29.Text = txtsday30.Text = txtsday31.Text = "0";
                txtduties.Text = txttduties.Text = txtcanteenadv.Text = txtPenalty.Text = txtIncentivs.Text = txttds.Text = txtmiscded.Text = txtatmded.Text = "0";
            }

            else
            {
                row = (GridViewRow)chkattendance.NamingContainer;
                //if (row == null)
                //    return;
                TextBox txtsday1 = row.FindControl("txtday1") as TextBox;
                TextBox txtsday2 = row.FindControl("txtday2") as TextBox;
                TextBox txtsday3 = row.FindControl("txtday3") as TextBox;
                TextBox txtsday4 = row.FindControl("txtday4") as TextBox;
                TextBox txtsday5 = row.FindControl("txtday5") as TextBox;
                TextBox txtsday6 = row.FindControl("txtday6") as TextBox;
                TextBox txtsday7 = row.FindControl("txtday7") as TextBox;
                TextBox txtsday8 = row.FindControl("txtday8") as TextBox;
                TextBox txtsday9 = row.FindControl("txtday9") as TextBox;
                TextBox txtsday10 = row.FindControl("txtday10") as TextBox;
                TextBox txtsday11 = row.FindControl("txtday11") as TextBox;
                TextBox txtsday12 = row.FindControl("txtday12") as TextBox;
                TextBox txtsday13 = row.FindControl("txtday13") as TextBox;
                TextBox txtsday14 = row.FindControl("txtday14") as TextBox;
                TextBox txtsday15 = row.FindControl("txtday15") as TextBox;
                TextBox txtsday16 = row.FindControl("txtday16") as TextBox;
                TextBox txtsday17 = row.FindControl("txtday17") as TextBox;
                TextBox txtsday18 = row.FindControl("txtday18") as TextBox;
                TextBox txtsday19 = row.FindControl("txtday19") as TextBox;
                TextBox txtsday20 = row.FindControl("txtday20") as TextBox;
                TextBox txtsday21 = row.FindControl("txtday21") as TextBox;
                TextBox txtsday22 = row.FindControl("txtday22") as TextBox;
                TextBox txtsday23 = row.FindControl("txtday23") as TextBox;
                TextBox txtsday24 = row.FindControl("txtday24") as TextBox;
                TextBox txtsday25 = row.FindControl("txtday25") as TextBox;
                TextBox txtsday26 = row.FindControl("txtday26") as TextBox;
                TextBox txtsday27 = row.FindControl("txtday27") as TextBox;
                TextBox txtsday28 = row.FindControl("txtday28") as TextBox;
                TextBox txtsday29 = row.FindControl("txtday29") as TextBox;
                TextBox txtsday30 = row.FindControl("txtday30") as TextBox;
                TextBox txtsday31 = row.FindControl("txtday31") as TextBox;
                TextBox txtduties = row.FindControl("txtDuties") as TextBox;
                Label txtempid = row.FindControl("lblEmpid") as Label;

                TextBox txtots = row.FindControl("txtOTs") as TextBox;
                TextBox txtnhss = row.FindControl("txtNhs") as TextBox;
                TextBox txtwos = row.FindControl("txtWos") as TextBox;
                TextBox txtxls = row.FindControl("txtxlsheet") as TextBox;
                TextBox txttduties = row.FindControl("txttotduties") as TextBox;
                TextBox txtcanteenadv = row.FindControl("txtCanAdv") as TextBox;
                TextBox txtPenalty = row.FindControl("txtPenalty") as TextBox;
                TextBox txtIncentivs = row.FindControl("txtIncentivs") as TextBox;
                TextBox txttds = row.FindControl("txttds") as TextBox;
                TextBox txtmiscded = row.FindControl("txtmiscded") as TextBox;
                TextBox txtatmded = row.FindControl("txtatmded") as TextBox;
                //Label txtempid = row.FindControl("lblEmpid") as Label;
                Label txtdesignation = row.FindControl("lbldesignname") as Label;
                int month = GetMonthBasedOnSelectionDateorMonth();
                var SPName1 = "";
                string monthv = "";
                string Yearv = "";
                string empid = txtempid.Text;

                if (month.ToString().Length == 4)
                {
                    monthv = month.ToString().Substring(0, 2);
                    Yearv = "20" + month.ToString().Substring(2, 2);
                }
                else
                {
                    monthv = month.ToString().Substring(0, 1);
                    Yearv = "20" + month.ToString().Substring(1, 2);
                }

                int days = GlobalData.Instance.GetNoOfDaysOfThisMonth(int.Parse(Yearv.Trim()), int.Parse(monthv.Trim()));
                Hashtable HTPaysheet1 = new Hashtable();
                SPName1 = "GetindividualAttendanceEmployeeWise";

                HTPaysheet1.Add("@month", month);
                HTPaysheet1.Add("@clientid", ddlClientID.SelectedValue);
                HTPaysheet1.Add("@EmpId", empid);
                HTPaysheet1.Add("@Designation", txtdesignation.Text);
                DataTable dt = config.ExecuteAdaptorAsyncWithParams(SPName1, HTPaysheet1).Result;
                if(dt.Rows.Count>0)
                {
                    string empid1 = dt.Rows[0]["empid"].ToString();
                    string day1 = dt.Rows[0]["day1"].ToString();
                    string day2 = dt.Rows[0]["day2"].ToString();
                    string day3 = dt.Rows[0]["day3"].ToString();
                    string day4 = dt.Rows[0]["day4"].ToString();
                    string day5 = dt.Rows[0]["day5"].ToString();
                    string day6 = dt.Rows[0]["day6"].ToString();
                    string day7 = dt.Rows[0]["day7"].ToString();
                    string day8 = dt.Rows[0]["day8"].ToString();
                    string day9 = dt.Rows[0]["day9"].ToString();
                    string day10 = dt.Rows[0]["day10"].ToString();
                    string day11 = dt.Rows[0]["day11"].ToString();
                    string day12 = dt.Rows[0]["day12"].ToString();
                    string day13 = dt.Rows[0]["day13"].ToString();
                    string day14 = dt.Rows[0]["day14"].ToString();
                    string day15 = dt.Rows[0]["day15"].ToString();
                    string day16 = dt.Rows[0]["day16"].ToString();
                    string day17 = dt.Rows[0]["day17"].ToString();
                    string day18 = dt.Rows[0]["day18"].ToString();
                    string day19 = dt.Rows[0]["day19"].ToString();
                    string day20 = dt.Rows[0]["day20"].ToString();
                    string day21 = dt.Rows[0]["day21"].ToString();
                    string day22 = dt.Rows[0]["day22"].ToString();
                    string day23 = dt.Rows[0]["day23"].ToString();
                    string day24 = dt.Rows[0]["day24"].ToString();
                    string day25 = dt.Rows[0]["day25"].ToString();
                    string day26 = dt.Rows[0]["day26"].ToString();
                    string day27 = dt.Rows[0]["day27"].ToString();
                    string day28 = dt.Rows[0]["day28"].ToString();
                    string day29 = dt.Rows[0]["day29"].ToString();
                    string day30 = dt.Rows[0]["day30"].ToString();
                    string day31 = dt.Rows[0]["day31"].ToString();

                    float duties=Convert.ToSingle(dt.Rows[0]["NoOfDuties"].ToString());
                    float totalduties= Convert.ToSingle(dt.Rows[0]["TotalDuties"].ToString());
                    float ots=Convert.ToSingle( dt.Rows[0]["OT"].ToString());
                    float nhs = Convert.ToSingle(dt.Rows[0]["NHS"].ToString());
                    float Wo = Convert.ToSingle(dt.Rows[0]["WO"].ToString());
                    float canteenadv = Convert.ToSingle(dt.Rows[0]["CanteenAdv"].ToString());
                    float penalty = Convert.ToSingle(dt.Rows[0]["Penalty"].ToString());
                    float Incetives = Convert.ToSingle(dt.Rows[0]["Incentivs"].ToString());
                    float TDS = Convert.ToSingle(dt.Rows[0]["TDS"].ToString());
                    float Miscded = Convert.ToSingle(dt.Rows[0]["MiscDed"].ToString());
                    float ATMDed = Convert.ToSingle(dt.Rows[0]["ATMDed"].ToString());
                    float XLSheet = Convert.ToSingle(dt.Rows[0]["XLSheet"].ToString());

                    string StPName = "FindWeekDay";
                    Hashtable htname = new Hashtable();
                    htname.Add("@empid", empid);
                    DataTable dtqry = config.ExecuteAdaptorAsyncWithParams(StPName, htname).Result;

                    string Weekday = "";

                    if (dtqry.Rows.Count > 0)
                    {
                        Weekday = dtqry.Rows[0]["weekday"].ToString();
                    }


                    #region Date 1 check

                    string proc1 = "FindAllWeekDays";
                    Hashtable htsun1 = new Hashtable();
                    htsun1.Add("@date", "" + monthv + "/01/" + Yearv + "");
                    DataTable dtsun1 = config.ExecuteAdaptorAsyncWithParams(proc1, htsun1).Result;
                    string date1 = "";
                    if (dtsun1.Rows.Count > 0)
                    {
                        date1 = dtsun1.Rows[0]["weekday"].ToString();
                    }

                    if (Weekday == date1)
                    {
                        day1 = "W";
                    }

                    #endregion Date 1 check

                    #region Date 2 check

                    string proc2 = "FindAllWeekDays";
                    Hashtable htsun2 = new Hashtable();
                    htsun2.Add("@date", "" + monthv + "/02/" + Yearv + "");
                    DataTable dtsun2 = config.ExecuteAdaptorAsyncWithParams(proc2, htsun2).Result;
                    string date2 = "";
                    if (dtsun2.Rows.Count > 0)
                    {
                        date2 = dtsun2.Rows[0]["weekday"].ToString();
                    }

                    if (Weekday == date2)
                    {
                        day2 = "W";
                    }

                    #endregion Date 2 check

                    #region Date 3 check

                    string proc3 = "FindAllWeekDays";
                    Hashtable htsun3 = new Hashtable();
                    htsun3.Add("@date", "" + monthv + "/03/" + Yearv + "");
                    DataTable dtsun3 = config.ExecuteAdaptorAsyncWithParams(proc3, htsun3).Result;
                    string date3 = "";
                    if (dtsun3.Rows.Count > 0)
                    {
                        date3 = dtsun3.Rows[0]["weekday"].ToString();
                    }

                    if (Weekday == date3)
                    {
                        day3 = "W";
                    }

                    #endregion Date 3 check


                    #region Date 4 check

                    string proc4 = "FindAllWeekDays";
                    Hashtable htsun4 = new Hashtable();
                    htsun4.Add("@date", "" + monthv + "/04/" + Yearv + "");
                    DataTable dtsun4 = config.ExecuteAdaptorAsyncWithParams(proc4, htsun4).Result;
                    string date4 = "";
                    if (dtsun4.Rows.Count > 0)
                    {
                        date4 = dtsun4.Rows[0]["weekday"].ToString();
                    }

                    if (Weekday == date4)
                    {
                        day4 = "W";
                    }

                    #endregion Date 4 check

                    #region Date 5 check

                    string proc5 = "FindAllWeekDays";
                    Hashtable htsun5 = new Hashtable();
                    htsun5.Add("@date", "" + monthv + "/05/" + Yearv + "");
                    DataTable dtsun5 = config.ExecuteAdaptorAsyncWithParams(proc5, htsun5).Result;
                    string date5 = "";
                    if (dtsun5.Rows.Count > 0)
                    {
                        date5 = dtsun5.Rows[0]["weekday"].ToString();
                    }

                    if (Weekday == date5)
                    {
                        day5 = "W";
                    }

                    #endregion Date 5 check

                    #region Date 6 check

                    string proc6 = "FindAllWeekDays";
                    Hashtable htsun6 = new Hashtable();
                    htsun6.Add("@date", "" + monthv + "/06/" + Yearv + "");
                    DataTable dtsun6 = config.ExecuteAdaptorAsyncWithParams(proc6, htsun6).Result;
                    string date6 = "";
                    if (dtsun6.Rows.Count > 0)
                    {
                        date6 = dtsun6.Rows[0]["weekday"].ToString();
                    }

                    if (Weekday == date6)
                    {
                        day6 = "W";
                    }

                    #endregion Date 6 check

                    #region Date 7 check

                    string proc7 = "FindAllWeekDays";
                    Hashtable htsun7 = new Hashtable();
                    htsun7.Add("@date", "" + monthv + "/07/" + Yearv + "");
                    DataTable dtsun7 = config.ExecuteAdaptorAsyncWithParams(proc7, htsun7).Result;
                    string date7 = "";
                    if (dtsun7.Rows.Count > 0)
                    {
                        date7 = dtsun7.Rows[0]["weekday"].ToString();
                    }

                    if (Weekday == date7)
                    {
                        day7 = "W";
                    }

                    #endregion Date 7 check


                    #region Date 8 check

                    string proc8 = "FindAllWeekDays";
                    Hashtable htsun8 = new Hashtable();
                    htsun8.Add("@date", "" + monthv + "/08/" + Yearv + "");
                    DataTable dtsun8 = config.ExecuteAdaptorAsyncWithParams(proc8, htsun8).Result;
                    string date8 = "";
                    if (dtsun8.Rows.Count > 0)
                    {
                        date8 = dtsun8.Rows[0]["weekday"].ToString();
                    }

                    if (Weekday == date8)
                    {
                        day8 = "W";
                    }

                    #endregion Date 8 check

                    #region Date 9 check

                    string proc9 = "FindAllWeekDays";
                    Hashtable htsun9 = new Hashtable();
                    htsun9.Add("@date", "" + monthv + "/09/" + Yearv + "");
                    DataTable dtsun9 = config.ExecuteAdaptorAsyncWithParams(proc9, htsun9).Result;
                    string date9 = "";
                    if (dtsun9.Rows.Count > 0)
                    {
                        date9 = dtsun9.Rows[0]["weekday"].ToString();
                    }

                    if (Weekday == date9)
                    {
                        day9 = "W";
                    }

                    #endregion Date 9 check


                    #region Date 10 check

                    string proc10 = "FindAllWeekDays";
                    Hashtable htsun10 = new Hashtable();
                    htsun10.Add("@date", "" + monthv + "/10/" + Yearv + "");
                    DataTable dtsun10 = config.ExecuteAdaptorAsyncWithParams(proc10, htsun10).Result;
                    string date10 = "";
                    if (dtsun10.Rows.Count > 0)
                    {
                        date10 = dtsun10.Rows[0]["weekday"].ToString();
                    }

                    if (Weekday == date10)
                    {
                        day10 = "W";
                    }

                    #endregion Date 10 check

                    #region Date 11 check

                    string proc11 = "FindAllWeekDays";
                    Hashtable htsun11 = new Hashtable();
                    htsun11.Add("@date", "" + monthv + "/11/" + Yearv + "");
                    DataTable dtsun11 = config.ExecuteAdaptorAsyncWithParams(proc11, htsun11).Result;
                    string date11 = "";
                    if (dtsun11.Rows.Count > 0)
                    {
                        date11 = dtsun11.Rows[0]["weekday"].ToString();
                    }

                    if (Weekday == date11)
                    {
                        day11 = "W";
                    }

                    #endregion Date 11 check

                    #region Date 12 check

                    string proc12 = "FindAllWeekDays";
                    Hashtable htsun12 = new Hashtable();
                    htsun12.Add("@date", "" + monthv + "/12/" + Yearv + "");
                    DataTable dtsun12 = config.ExecuteAdaptorAsyncWithParams(proc12, htsun12).Result;
                    string date12 = "";
                    if (dtsun12.Rows.Count > 0)
                    {
                        date12 = dtsun12.Rows[0]["weekday"].ToString();
                    }

                    if (Weekday == date12)
                    {
                        day12 = "W";
                    }

                    #endregion Date 12 check

                    #region Date 13 check

                    string proc13 = "FindAllWeekDays";
                    Hashtable htsun13 = new Hashtable();
                    htsun13.Add("@date", "" + monthv + "/13/" + Yearv + "");
                    DataTable dtsun13 = config.ExecuteAdaptorAsyncWithParams(proc13, htsun13).Result;
                    string date13 = "";
                    if (dtsun13.Rows.Count > 0)
                    {
                        date13 = dtsun13.Rows[0]["weekday"].ToString();
                    }

                    if (Weekday == date13)
                    {
                        day13 = "W";
                    }

                    #endregion Date 13 check


                    #region Date 14 check

                    string proc14 = "FindAllWeekDays";
                    Hashtable htsun14 = new Hashtable();
                    htsun14.Add("@date", "" + monthv + "/14/" + Yearv + "");
                    DataTable dtsun14 = config.ExecuteAdaptorAsyncWithParams(proc14, htsun14).Result;
                    string date14 = "";
                    if (dtsun14.Rows.Count > 0)
                    {
                        date14 = dtsun14.Rows[0]["weekday"].ToString();
                    }

                    if (Weekday == date14)
                    {
                        day14 = "W";
                    }

                    #endregion Date 14 check


                    #region Date 15 check

                    string proc15 = "FindAllWeekDays";
                    Hashtable htsun15 = new Hashtable();
                    htsun15.Add("@date", "" + monthv + "/15/" + Yearv + "");
                    DataTable dtsun15 = config.ExecuteAdaptorAsyncWithParams(proc15, htsun15).Result;
                    string date15 = "";
                    if (dtsun15.Rows.Count > 0)
                    {
                        date15 = dtsun15.Rows[0]["weekday"].ToString();
                    }

                    if (Weekday == date15)
                    {
                        day15 = "W";
                    }

                    #endregion Date 15 check


                    #region Date 16 check

                    string proc16 = "FindAllWeekDays";
                    Hashtable htsun16 = new Hashtable();
                    htsun16.Add("@date", "" + monthv + "/16/" + Yearv + "");
                    DataTable dtsun16 = config.ExecuteAdaptorAsyncWithParams(proc16, htsun16).Result;
                    string date16 = "";
                    if (dtsun16.Rows.Count > 0)
                    {
                        date16 = dtsun16.Rows[0]["weekday"].ToString();
                    }

                    if (Weekday == date16)
                    {
                        day16 = "W";
                    }

                    #endregion Date 16 check

                    #region Date 17 check

                    string proc17 = "FindAllWeekDays";
                    Hashtable htsun17 = new Hashtable();
                    htsun17.Add("@date", "" + monthv + "/17/" + Yearv + "");
                    DataTable dtsun17 = config.ExecuteAdaptorAsyncWithParams(proc17, htsun17).Result;
                    string date17 = "";
                    if (dtsun17.Rows.Count > 0)
                    {
                        date17 = dtsun17.Rows[0]["weekday"].ToString();
                    }

                    if (Weekday == date17)
                    {
                        day17 = "W";
                    }

                    #endregion Date 17 check

                    #region Date 18 check

                    string proc18 = "FindAllWeekDays";
                    Hashtable htsun18 = new Hashtable();
                    htsun18.Add("@date", "" + monthv + "/18/" + Yearv + "");
                    DataTable dtsun18 = config.ExecuteAdaptorAsyncWithParams(proc18, htsun18).Result;
                    string date18 = "";
                    if (dtsun18.Rows.Count > 0)
                    {
                        date18 = dtsun18.Rows[0]["weekday"].ToString();
                    }

                    if (Weekday == date18)
                    {
                        day18 = "W";
                    }

                    #endregion Date 18 check

                    #region Date 19 check

                    string proc19 = "FindAllWeekDays";
                    Hashtable htsun19 = new Hashtable();
                    htsun19.Add("@date", "" + monthv + "/19/" + Yearv + "");
                    DataTable dtsun19 = config.ExecuteAdaptorAsyncWithParams(proc19, htsun19).Result;
                    string date19 = "";
                    if (dtsun19.Rows.Count > 0)
                    {
                        date19 = dtsun19.Rows[0]["weekday"].ToString();
                    }

                    if (Weekday == date19)
                    {
                        day19 = "W";
                    }

                    #endregion Date 19 check

                    #region Date 20 check

                    string proc20 = "FindAllWeekDays";
                    Hashtable htsun20 = new Hashtable();
                    htsun20.Add("@date", "" + monthv + "/20/" + Yearv + "");
                    DataTable dtsun20 = config.ExecuteAdaptorAsyncWithParams(proc20, htsun20).Result;
                    string date20 = "";
                    if (dtsun20.Rows.Count > 0)
                    {
                        date20 = dtsun20.Rows[0]["weekday"].ToString();
                    }

                    if (Weekday == date20)
                    {
                        day20 = "W";
                    }

                    #endregion Date 20 check

                    #region Date 21 check

                    string proc21 = "FindAllWeekDays";
                    Hashtable htsun21 = new Hashtable();
                    htsun21.Add("@date", "" + monthv + "/21/" + Yearv + "");
                    DataTable dtsun21 = config.ExecuteAdaptorAsyncWithParams(proc21, htsun21).Result;
                    string date21 = "";
                    if (dtsun21.Rows.Count > 0)
                    {
                        date21 = dtsun21.Rows[0]["weekday"].ToString();
                    }

                    if (Weekday == date21)
                    {
                        day21 = "W";
                    }

                    #endregion Date 21 check

                    #region Date 22 check

                    string proc22 = "FindAllWeekDays";
                    Hashtable htsun22 = new Hashtable();
                    htsun22.Add("@date", "" + monthv + "/22/" + Yearv + "");
                    DataTable dtsun22 = config.ExecuteAdaptorAsyncWithParams(proc22, htsun22).Result;
                    string date22 = "";
                    if (dtsun22.Rows.Count > 0)
                    {
                        date22 = dtsun22.Rows[0]["weekday"].ToString();
                    }

                    if (Weekday == date22)
                    {
                        day22 = "W";
                    }

                    #endregion Date 22 check

                    #region Date 23 check

                    string proc23 = "FindAllWeekDays";
                    Hashtable htsun23 = new Hashtable();
                    htsun23.Add("@date", "" + monthv + "/23/" + Yearv + "");
                    DataTable dtsun23 = config.ExecuteAdaptorAsyncWithParams(proc23, htsun23).Result;
                    string date23 = "";
                    if (dtsun23.Rows.Count > 0)
                    {
                        date23 = dtsun23.Rows[0]["weekday"].ToString();
                    }

                    if (Weekday == date23)
                    {
                        day23 = "W";
                    }

                    #endregion Date 23 check

                    #region Date 24 check

                    string proc24 = "FindAllWeekDays";
                    Hashtable htsun24 = new Hashtable();
                    htsun24.Add("@date", "" + monthv + "/24/" + Yearv + "");
                    DataTable dtsun24 = config.ExecuteAdaptorAsyncWithParams(proc24, htsun24).Result;
                    string date24 = "";
                    if (dtsun24.Rows.Count > 0)
                    {
                        date24 = dtsun24.Rows[0]["weekday"].ToString();
                    }

                    if (Weekday == date24)
                    {
                        day24 = "W";
                    }

                    #endregion Date 24 check

                    #region Date 25 check

                    string proc25 = "FindAllWeekDays";
                    Hashtable htsun25 = new Hashtable();
                    htsun25.Add("@date", "" + monthv + "/25/" + Yearv + "");
                    DataTable dtsun25 = config.ExecuteAdaptorAsyncWithParams(proc25, htsun25).Result;
                    string date25 = "";
                    if (dtsun25.Rows.Count > 0)
                    {
                        date25 = dtsun25.Rows[0]["weekday"].ToString();
                    }

                    if (Weekday == date25)
                    {
                        day25 = "W";
                    }

                    #endregion Date 25 check

                    #region Date 26 check

                    string proc26 = "FindAllWeekDays";
                    Hashtable htsun26 = new Hashtable();
                    htsun26.Add("@date", "" + monthv + "/26/" + Yearv + "");
                    DataTable dtsun26 = config.ExecuteAdaptorAsyncWithParams(proc26, htsun26).Result;
                    string date26 = "";
                    if (dtsun26.Rows.Count > 0)
                    {
                        date26 = dtsun26.Rows[0]["weekday"].ToString();
                    }

                    if (Weekday == date26)
                    {
                        day26 = "W";
                    }

                    #endregion Date 26 check

                    #region Date 27 check

                    string proc27 = "FindAllWeekDays";
                    Hashtable htsun27 = new Hashtable();
                    htsun27.Add("@date", "" + monthv + "/27/" + Yearv + "");
                    DataTable dtsun27 = config.ExecuteAdaptorAsyncWithParams(proc27, htsun27).Result;
                    string date27 = "";
                    if (dtsun27.Rows.Count > 0)
                    {
                        date27 = dtsun27.Rows[0]["weekday"].ToString();
                    }

                    if (Weekday == date27)
                    {
                        day27 = "W";
                    }

                    #endregion Date 27 check


                    #region Date 28 check

                    string proc28 = "FindAllWeekDays";
                    Hashtable htsun28 = new Hashtable();
                    htsun28.Add("@date", "" + monthv + "/28/" + Yearv + "");
                    DataTable dtsun28 = config.ExecuteAdaptorAsyncWithParams(proc28, htsun28).Result;
                    string date28 = "";
                    if (dtsun28.Rows.Count > 0)
                    {
                        date28 = dtsun28.Rows[0]["weekday"].ToString();
                    }

                    if (Weekday == date28)
                    {
                        day28 = "W";
                    }

                    #endregion Date 28 check

                    if (days == 30)
                    {
                        #region Date 29 check

                        string proc29 = "FindAllWeekDays";
                        Hashtable htsun29 = new Hashtable();
                        htsun29.Add("@date", "" + monthv + "/29/" + Yearv + "");
                        DataTable dtsun29 = config.ExecuteAdaptorAsyncWithParams(proc29, htsun29).Result;
                        string date29 = "";
                        if (dtsun29.Rows.Count > 0)
                        {
                            date29 = dtsun29.Rows[0]["weekday"].ToString();
                        }

                        if (Weekday == date29)
                        {
                            day29 = "W";
                        }

                        #endregion Date 29 check

                        #region Date 30 check

                        string proc30 = "FindAllWeekDays";
                        Hashtable htsun30 = new Hashtable();
                        htsun30.Add("@date", "" + monthv + "/30/" + Yearv + "");
                        DataTable dtsun30 = config.ExecuteAdaptorAsyncWithParams(proc30, htsun30).Result;
                        string date30 = "";
                        if (dtsun30.Rows.Count > 0)
                        {
                            date30 = dtsun30.Rows[0]["weekday"].ToString();
                        }

                        if (Weekday == date30)
                        {
                            day30 = "W";
                        }

                        #endregion Date 30 check
                    }


                    if (days == 31)
                    {

                        #region Date 29 check

                        string proc29 = "FindAllWeekDays";
                        Hashtable htsun29 = new Hashtable();
                        htsun29.Add("@date", "" + monthv + "/29/" + Yearv + "");
                        DataTable dtsun29 = config.ExecuteAdaptorAsyncWithParams(proc29, htsun29).Result;
                        string date29 = "";
                        if (dtsun29.Rows.Count > 0)
                        {
                            date29 = dtsun29.Rows[0]["weekday"].ToString();
                        }

                        if (Weekday == date29)
                        {
                            day29 = "W";
                        }

                        #endregion Date 29 check

                        #region Date 30 check

                        string proc30 = "FindAllWeekDays";
                        Hashtable htsun30 = new Hashtable();
                        htsun30.Add("@date", "" + monthv + "/30/" + Yearv + "");
                        DataTable dtsun30 = config.ExecuteAdaptorAsyncWithParams(proc30, htsun30).Result;
                        string date30 = "";
                        if (dtsun30.Rows.Count > 0)
                        {
                            date30 = dtsun30.Rows[0]["weekday"].ToString();
                        }

                        if (Weekday == date30)
                        {
                            day30 = "W";
                        }

                        #endregion Date 30 check

                        #region Date 31 check

                        string proc31 = "FindAllWeekDays";
                        Hashtable htsun31 = new Hashtable();
                        htsun31.Add("@date", "" + monthv + "/31/" + Yearv + "");
                        DataTable dtsun31 = config.ExecuteAdaptorAsyncWithParams(proc31, htsun31).Result;
                        string date31 = "";
                        if (dtsun31.Rows.Count > 0)
                        {
                            date31 = dtsun31.Rows[0]["weekday"].ToString();
                        }

                        if (Weekday == date31)
                        {
                            day31 = "W";
                        }

                        #endregion Date 31 check
                    }
                    txtsday1.Text = day1;
                    txtsday2.Text = day2;
                    txtsday3.Text = day3;
                    txtsday4.Text = day4;
                    txtsday5.Text = day5;
                    txtsday6.Text = day6; txtsday7.Text = day7;
                    txtsday8.Text =day8 ; txtsday9.Text = day9; txtsday10.Text = day10; txtsday11.Text = day11;
                    txtsday12.Text = day12;
                    txtsday13.Text = day13; txtsday14.Text = day14; txtsday15.Text = day15; txtsday16.Text = day16; txtsday17.Text = day17;
                    txtsday18.Text = day18; txtsday19.Text = day19; txtsday20.Text = day20; txtsday21.Text = day21; txtsday22.Text = day22; txtsday23.Text = day23;
                    txtsday24.Text = day24; txtsday25.Text = day25; txtsday26.Text = day26; txtsday27.Text = day27; txtsday28.Text = day28; txtsday29.Text = day29;
                    txtsday30.Text = day30; txtsday31.Text = day31;
                    txtduties.Text = duties.ToString(); txttduties.Text = totalduties.ToString();
                    txtcanteenadv.Text = canteenadv.ToString();
                    txtPenalty.Text = penalty.ToString();
                    txtIncentivs.Text = Incetives.ToString();
                    txttds.Text = TDS.ToString(); txtmiscded.Text =Miscded.ToString() ; txtatmded.Text =ATMDed.ToString();
                }
            }
        }
        
        }
    }
