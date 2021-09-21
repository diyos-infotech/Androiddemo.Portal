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
using System.Data.SqlClient;
using System.IO;
using System.Globalization;
using Jawan.Portal.DAL;
namespace Jawan.Portal
{
    public partial class ModifyEmployee : System.Web.UI.Page

    {
        AppConfiguration config = new AppConfiguration();
        GridViewExportUtil gve = new GridViewExportUtil();
        string EmpIDPrefix = "";
        string CmpIDPrefix = "";
        string BranchID = "";
        string Elength = "";
        //string Clength = "";
        //static Int64 time = DateTime.Now.Ticks;

        protected void Page_Load(object sender, EventArgs e)
        {
            //GlobalData.Instance.AppendLog("In Page_Load of ModifyEmployee " + (DateTime.Now.Ticks- time).ToString());
            //time = DateTime.Now.Ticks;

            try
            {
                GetWebConfigdata();
                if (!IsPostBack)
                {

                    //Session["MyTime"] = time;
                    //GlobalData.Instance.AppendLog("After GetWebConfigdata " + (DateTime.Now.Ticks - Convert.ToInt64(Session["MyTime"])).ToString());
                    //Session["MyTime"] = DateTime.Now.Ticks;
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

                    TabContainer1.ActiveTabIndex = 0;
                    SetInitialRow();
                    LoadBloodGroups();
                    LoadBanknames();
                    LoadDesignations();
                    SetInitialRowEducation();
                    SetInitialRowPrevExp();
                    LoadDivisions();
                    LoadDepartments();
                    LoadBranches();
                    LoadReportingManager();
                    LoadClientids();
                    LoadStatenames();
                    Loadshifts();
                    //GlobalData.Instance.AppendLog("After Filling Designations " + (DateTime.Now.Ticks - Convert.ToInt64(Session["MyTime"])).ToString());
                    //Session["MyTime"] = DateTime.Now.Ticks;
                    string ImagesFolderPath = Server.MapPath("Images");
                    string[] filePaths = Directory.GetFiles(ImagesFolderPath);

                    foreach (string file in filePaths)
                    {
                        File.Delete(file);
                    }
                    //GlobalData.Instance.AppendLog("After Deleting all files " + (DateTime.Now.Ticks - Convert.ToInt64(Session["MyTime"])).ToString());
                    //Session["MyTime"] = DateTime.Now.Ticks;
                    ClearAllControlsDataFromThePage();
                    if (Request.QueryString["Empid"] != null)
                    {

                        string username = Request.QueryString["Empid"].ToString();
                        LoadPersonalInfo(username);

                    }

                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/login.aspx");

            }
        }
        protected void Loadshifts()
        {
            string query = "select * from shifts";

            DataTable dtshifts = config.ExecuteAdaptorAsyncWithQueryParams(query).Result;
            if (dtshifts.Rows.Count > 0)
            {
                ddlShift.DataValueField = "Shift";
                ddlShift.DataTextField = "Shift";
                ddlShift.DataSource = dtshifts;
                ddlShift.DataBind();
            }

            ddlShift.Items.Insert(0, new ListItem("-Select-", "0"));

        }
        #region New Code for Load Blood Groups dynamically from database as on 23/12/2013 by venkat

        protected void LoadBloodGroups()
        {
            DataTable dtBloodgroup = GlobalData.Instance.LoadBloodGroupNames();
            if (dtBloodgroup.Rows.Count > 0)
            {
                ddlBloodGroup.DataValueField = "BloodGroupId";
                ddlBloodGroup.DataTextField = "BloodGroupName";
                ddlBloodGroup.DataSource = dtBloodgroup;
                ddlBloodGroup.DataBind();
            }
            ddlBloodGroup.Items.Insert(0, new ListItem("-Select-", "0"));
        }

        #endregion

        protected void LoadClientids()
        {
            #region New Code for Prefered Units as on 24/12/2013 by venkat

            DataTable dt = GlobalData.Instance.LoadCNames(CmpIDPrefix, BranchID);
            if (dt.Rows.Count > 0)
            {
                DdlPreferedUnit.DataValueField = "clientid";
                DdlPreferedUnit.DataTextField = "clientname";
                DdlPreferedUnit.DataSource = dt;
                DdlPreferedUnit.DataBind();
            }
            DdlPreferedUnit.Items.Insert(0, new ListItem("-Select-", "0"));

            #endregion

        }

        protected void LoadStatenames()
        {

            DataTable DtStateNames = GlobalData.Instance.LoadStateNames();
            if (DtStateNames.Rows.Count > 0)
            {
                ddlpreStates.DataValueField = "StateID";
                ddlpreStates.DataTextField = "State";
                ddlpreStates.DataSource = DtStateNames;
                ddlpreStates.DataBind();


                DdlStates.DataValueField = "StateID";
                DdlStates.DataTextField = "State";
                DdlStates.DataSource = DtStateNames;
                DdlStates.DataBind();

                //ddlbirthstate.DataValueField = "StateID";
                //ddlbirthstate.DataTextField = "State";
                //ddlbirthstate.DataSource = DtStateNames;
                //ddlbirthstate.DataBind();


            }
            ddlpreStates.Items.Insert(0, new ListItem("-Select-", "0"));
            DdlStates.Items.Insert(0, new ListItem("-Select-", "0"));
            // ddlbirthstate.Items.Insert(0, new ListItem("-Select-", "0"));
        }

        protected void LoadDivisions()
        {

            DataTable DtDivision = GlobalData.Instance.LoadDivision();
            if (DtDivision.Rows.Count > 0)
            {
                ddlDivision.DataValueField = "DivisionId";
                ddlDivision.DataTextField = "DivisionName";
                ddlDivision.DataSource = DtDivision;
                ddlDivision.DataBind();
            }
            ddlDivision.Items.Insert(0, new ListItem("-Select-", "0"));
        }

        protected void LoadBranches()
        {

            string query = "select * from BranchDetails";

            DataTable dtOldEmployeeIds = config.ExecuteAdaptorAsyncWithQueryParams(query).Result;
            if (dtOldEmployeeIds.Rows.Count > 0)
            {
                ddlBranch.DataValueField = "Branchid";
                ddlBranch.DataTextField = "BranchName";
                ddlBranch.DataSource = dtOldEmployeeIds;
                ddlBranch.DataBind();
            }

            ddlBranch.Items.Insert(0, new ListItem("-Select-", "0"));

        }

        protected void LoadDepartments()
        {

            DataTable DtDepartments = GlobalData.Instance.LoadDepartments();
            if (DtDepartments.Rows.Count > 0)
            {
                ddldepartment.DataValueField = "DeptId";
                ddldepartment.DataTextField = "DeptName";
                ddldepartment.DataSource = DtDepartments;
                ddldepartment.DataBind();
            }
            ddldepartment.Items.Insert(0, new ListItem("-Select-", "0"));
        }

        protected void LoadReportingManager()
        {
            #region New Code for Prefered Units as on 24/12/2013 by venkat

            string Query = "Select Empid,(EmpFname+' '+EmpMname+' '+EmpLname) as Empname from Empdetails where EmployeeType='S'";
            DataTable dt = config.ExecuteAdaptorAsyncWithQueryParams(Query).Result;
            if (dt.Rows.Count > 0)
            {
                ddlReportingMgr.DataValueField = "Empid";
                ddlReportingMgr.DataTextField = "Empname";
                ddlReportingMgr.DataSource = dt;
                ddlReportingMgr.DataBind();
            }
            ddlReportingMgr.Items.Insert(0, new ListItem("-Select-", "0"));

            #endregion

        }

        protected void LoadBanknames()
        {
            DataTable DtBankNames = GlobalData.Instance.LoadBankNames();
            if (DtBankNames.Rows.Count > 0)
            {
                ddlbankname.DataValueField = "bankid";
                ddlbankname.DataTextField = "banKname";
                ddlbankname.DataSource = DtBankNames;
                ddlbankname.DataBind();
                ddlbankname.Items.Insert(0, new ListItem("-Select-", "0"));
            }
            else
            {
                ddlbankname.Items.Insert(0, new ListItem("-Select-", "0"));
            }
        }

        private void SetInitialRow()
        {
            string username = "";
            if (Request.QueryString["Empid"] != null)
            {
                username = Request.QueryString["Empid"].ToString();
            }

            string query = "select RName,RType,convert(varchar(10),DOfBirth,103) as DOfBirth,age,Roccupation,RResidence,RPlace,PFNominee,ESINominee,RPhone,RGMail from EmpRelationships where empid='" + username + "' order by id";
            DataTable dtcount = config.ExecuteAdaptorAsyncWithQueryParams(query).Result;
            if (dtcount.Rows.Count > 0)
            {

                ViewState["CurrentTable"] = dtcount;
                gvFamilyDetails.DataSource = dtcount;
                gvFamilyDetails.DataBind();


            }

            else
            {
                DataTable dt = new DataTable();
                DataRow dr = null;
                dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
                dt.Columns.Add(new DataColumn("RName", typeof(string)));
                dt.Columns.Add(new DataColumn("DOfBirth", typeof(string)));
                dt.Columns.Add(new DataColumn("age", typeof(string)));
                dt.Columns.Add(new DataColumn("RType", typeof(string)));
                dt.Columns.Add(new DataColumn("ROccupation", typeof(string)));
                dt.Columns.Add(new DataColumn("RPhone", typeof(string)));
                dt.Columns.Add(new DataColumn("RGMail", typeof(string)));
                dt.Columns.Add(new DataColumn("PFNominee", typeof(string)));
                dt.Columns.Add(new DataColumn("ESINominee", typeof(string)));
                dt.Columns.Add(new DataColumn("RResidence", typeof(string)));
                dt.Columns.Add(new DataColumn("RPlace", typeof(string)));

                for (int i = 1; i < 11; i++)
                {

                    dr = dt.NewRow();
                    dr["RowNumber"] = 1;
                    dr["RName"] = string.Empty;
                    dr["DOfBirth"] = string.Empty;
                    dr["age"] = string.Empty;
                    dr["RType"] = string.Empty;
                    dr["ROccupation"] = string.Empty;
                    dr["RPhone"] = string.Empty;
                    dr["RGMail"] = string.Empty;
                    dr["PFNominee"] = string.Empty;
                    dr["ESINominee"] = string.Empty;
                    dr["RResidence"] = string.Empty;
                    dr["RPlace"] = string.Empty;

                    dt.Rows.Add(dr);

                }


                //Store the DataTable in ViewState
                ViewState["CurrentTable"] = dt;

                gvFamilyDetails.DataSource = dt;
                gvFamilyDetails.DataBind();
            }
        }

        private void SetInitialRowEducation()
        {
            string username = "";
            if (Request.QueryString["Empid"] != null)
            {
                username = Request.QueryString["Empid"].ToString();
            }

            string query = "select *  from EmpEducationDetails where empid='" + username + "' order by id";
            DataTable dtcount = config.ExecuteAdaptorAsyncWithQueryParams(query).Result;
            if (dtcount.Rows.Count > 0)
            {

                ViewState["EducationTable"] = dtcount;
                GvEducationDetails.DataSource = dtcount;
                GvEducationDetails.DataBind();


            }
            else
            {
                DataTable dt = new DataTable();
                DataRow dr = null;
                dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
                dt.Columns.Add(new DataColumn("Qualification", typeof(string)));
                dt.Columns.Add(new DataColumn("Description", typeof(string)));
                dt.Columns.Add(new DataColumn("NameOfSchoolClg", typeof(string)));
                dt.Columns.Add(new DataColumn("BoardorUniversity", typeof(string)));
                dt.Columns.Add(new DataColumn("YrOfStudy", typeof(string)));
                dt.Columns.Add(new DataColumn("PassOrFail", typeof(string)));
                dt.Columns.Add(new DataColumn("PercentageOfmarks", typeof(string)));

                for (int i = 1; i < 5; i++)
                {
                    dr = dt.NewRow();
                    dr["RowNumber"] = 1;
                    dr["Qualification"] = string.Empty;
                    dr["Description"] = string.Empty;
                    dr["NameOfSchoolClg"] = string.Empty;
                    dr["BoardorUniversity"] = string.Empty;
                    dr["YrOfStudy"] = string.Empty;
                    dr["PassOrFail"] = string.Empty;
                    dr["PercentageOfmarks"] = string.Empty;
                    dt.Rows.Add(dr);

                }



                //Store the DataTable in ViewState
                ViewState["EducationTable"] = dt;

                GvEducationDetails.DataSource = dt;
                GvEducationDetails.DataBind();
            }
        }

        private void SetInitialRowPrevExp()
        {
            string username = "";
            if (Request.QueryString["Empid"] != null)
            {
                username = Request.QueryString["Empid"].ToString();
            }

            string query = "select *,convert(varchar(10),DateofResign,103) as DateofResign1  from EmpPrevExperience where empid='" + username + "' order by id";
            DataTable dtcount = config.ExecuteAdaptorAsyncWithQueryParams(query).Result;
            if (dtcount.Rows.Count > 0)
            {

                ViewState["PrevExpTable"] = dtcount;
                GvPreviousExperience.DataSource = dtcount;
                GvPreviousExperience.DataBind();


            }
            else
            {
                DataTable dt = new DataTable();
                DataRow dr = null;
                dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
                dt.Columns.Add(new DataColumn("RegionCode", typeof(string)));
                dt.Columns.Add(new DataColumn("EmployerCode", typeof(string)));
                dt.Columns.Add(new DataColumn("Extension", typeof(string)));
                dt.Columns.Add(new DataColumn("Designation", typeof(string)));
                dt.Columns.Add(new DataColumn("CompAddress", typeof(string)));
                dt.Columns.Add(new DataColumn("YrOfExp", typeof(string)));
                dt.Columns.Add(new DataColumn("PFNo", typeof(string)));
                dt.Columns.Add(new DataColumn("ESINo", typeof(string)));
                dt.Columns.Add(new DataColumn("DateofResign1", typeof(string)));

                for (int i = 1; i < 5; i++)
                {
                    dr = dt.NewRow();
                    dr["RowNumber"] = 1;
                    dr["RowNumber"] = 1;
                    dr["RegionCode"] = string.Empty;
                    dr["EmployerCode"] = string.Empty;
                    dr["Extension"] = string.Empty;
                    dr["Designation"] = string.Empty;
                    dr["CompAddress"] = string.Empty;
                    dr["YrOfExp"] = string.Empty;
                    dr["PFNo"] = string.Empty;
                    dr["ESINo"] = string.Empty;
                    dr["DateofResign1"] = string.Empty;
                    dt.Rows.Add(dr);

                }


                //Store the DataTable in ViewState
                ViewState["PrevExpTable"] = dt;

                GvPreviousExperience.DataSource = dt;
                GvPreviousExperience.DataBind();
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
                    break;
                case 2:
                    //AddEmployeeLink.Visible = true;
                    //ModifyEmployeeLink.Visible = true;
                    //DeleteEmployeeLink.Visible = true;
                    AssigningWorkerLink.Visible = true;
                    AttendanceLink.Visible = true;
                    LoanLink.Visible = false;
                    //PaymentLink.Visible = false;
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
                    //AddEmployeeLink.Visible = false;
                    //ModifyEmployeeLink.Visible = false;
                    //DeleteEmployeeLink.Visible = false;
                    AssigningWorkerLink.Visible = false;
                    AttendanceLink.Visible = false;
                    LoanLink.Visible = true;
                    // PaymentLink.Visible = true;
                    //TrainingEmployeeLink.Visible = false;
                    //JobLeavingReasonsLink.Visible = false;
                    InventoryLink.Visible = false;
                    CompanyInfoLink.Visible = false;
                    ReportsLink.Visible = false;
                    SettingsLink.Visible = false;
                    break;
                case 5:
                    //AddEmployeeLink.Visible = false;
                    //ModifyEmployeeLink.Visible = false;
                    //DeleteEmployeeLink.Visible = false;
                    AssigningWorkerLink.Visible = false;
                    AttendanceLink.Visible = true;
                    LoanLink.Visible = false;
                    //PaymentLink.Visible = false;
                    //TrainingEmployeeLink.Visible = false;
                    //JobLeavingReasonsLink.Visible = false;
                    InventoryLink.Visible = false;
                    CompanyInfoLink.Visible = false;
                    ReportsLink.Visible = false;
                    SettingsLink.Visible = false;
                    break;

                case 6:
                    //AddEmployeeLink.Visible = false;
                    //AddEmployeeLink.Visible = false;
                    //ModifyEmployeeLink.Visible = false;
                    //DeleteEmployeeLink.Visible = false;
                    AssigningWorkerLink.Visible = false;
                    AttendanceLink.Visible = true;
                    LoanLink.Visible = false;
                    //PaymentLink.Visible = false;
                    //TrainingEmployeeLink.Visible = false;
                    //JobLeavingReasonsLink.Visible = false;

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
            BranchID = Session["BranchID"].ToString();
        }

        protected static void ClearControls(Control Parent)
        {
            //GlobalData.Instance.AppendLog("before Clearing Controls " + (DateTime.Now.Millisecond - Convert.ToInt32(Session["MyTime"])).ToString());
            if (Parent is TextBox)
            {
                (Parent as TextBox).Text = string.Empty;
            }
            else
            {
                foreach (Control c in Parent.Controls)
                    ClearControls(c);
            }
            //GlobalData.Instance.AppendLog("After Clearing Controls " + (DateTime.Now.Millisecond - Convert.ToInt32(Session["MyTime"])).ToString());
        }



        protected void Btn_Save_Personal_Tab_Click(object sender, EventArgs e)//Modify   personal Save button  
        {
            lblMsg.Text = "";

            try
            {
                if (txtEmpFName.Text.Trim().Length == 0)
                {
                    lblMsg.Text = "Please fill First Name!";
                    return;
                }

                //if (txtEmpmiName.Text.Trim().Length == 0)
                //{
                //    lblMsg.Text = "Please fill Middle Name!";
                //    return;
                //}

                if (Rdb_Male.Checked == false && Rdb_Female.Checked == false)
                {
                    lblMsg.Text = "Please Select The gender";
                    return;
                }

                if (rdbsingle.Checked == false && rdbmarried.Checked == false && rdbdivorcee.Checked == false && rdbWidower.Checked == false)
                {
                    lblMsg.Text = "Please Select The Marital Status";
                    return;
                }

                if (ddlDesignation.SelectedIndex == 0)
                {
                    lblMsg.Text = "Please Select the Designation";
                    return;
                }

                //if (txtRegCode.Text.Trim().Length == 0)
                //{
                //    lblMsg.Text = "Please Enter Region Code!";
                //    return;
                //}

                if (txtEmpDtofBirth.Text.Trim().Length == 0)
                {
                    lblMsg.Text = "Please Fill Date Of Birth!";
                    return;

                }

                if (txtEmpDtofJoining.Text.Trim().Length == 0)
                {
                    lblMsg.Text = "Please Fill Date Of Joining!";
                    return;

                }

                if (txtFatherName.Text.Trim().Length == 0)
                {
                    lblMsg.Text = "Please Fill Father Name!";
                    return;

                }

                if (ddlBranch.SelectedIndex == 0)
                {

                    lblMsg.Text = "Please Select the Branch";
                    return;
                }

                //if (txtAadharCard.Text.Trim().Length == 0)
                //{
                //    lblMsg.Text = "Please Fill Aadhar Number!";
                //    return;
                //}

                var Empbankname = string.Empty;
                var EmpBankAcNo = string.Empty;
                var Empbankbranchname = string.Empty;
                var EmpIFSCcode = string.Empty;
                var EmpBranchCode = string.Empty;


                var EmpBankCode = string.Empty;
                var EmpBankAppNo = string.Empty;
                var EmpRegionCode = string.Empty;
                var EmpInsNominee = string.Empty;
                var EmpBankCardRef = string.Empty;


                var EmpNomineeDtofBirth = "01/01/1900";
                var EmpNomineeRel = string.Empty;
                var EmpInsCover = string.Empty;
                var EmpInsDedAmt = string.Empty;
                var EmpUANNumber = string.Empty;



                var EmpEpfNo = string.Empty;
                var EmpNominee = string.Empty;
                var EmpPFEnrolDt = "01/01/1900";
                var CmpShortName = string.Empty;
                var EmpRelation = string.Empty;


                var EmpESINo = string.Empty;
                var EmpESINominee = string.Empty;
                var EmpESIDispName = string.Empty;
                var aadhaarid = string.Empty;
                var EmpESIRelation = string.Empty;

                var IRecordStatus = 0;
                var testDate = 0;




                EmpBankAcNo = txtBankAccNum.Text;
                Empbankbranchname = txtbranchname.Text;
                EmpIFSCcode = txtIFSCcode.Text;
                EmpBranchCode = txtBranchCode.Text;
                if (ddlbankname.SelectedIndex == 0)
                {
                    Empbankname = "0";
                }
                else
                {
                    Empbankname = ddlbankname.SelectedValue;
                }

                EmpBankCode = txtBankCodenum.Text;
                EmpBankAppNo = txtBankAppNum.Text;
                EmpRegionCode = txtRegCode.Text;
                EmpInsNominee = txtEmpInsNominee.Text;
                EmpBankCardRef = txtBankCardRef.Text;

                if (txtNomDoB.Text == "0")
                {
                    lblMsg.Text = "You Are Entered Invalid Nominee Date of Birth.Date Format Should be [DD/MM/YYYY].Ex.01/01/1990";
                    return;
                }

                if (txtNomDoB.Text.Trim().Length != 0)
                {
                    EmpNomineeDtofBirth = DateTime.Parse(txtNomDoB.Text, CultureInfo.GetCultureInfo("en-gb")).ToString();
                    if (testDate > 0)
                    {
                        lblMsg.Text = "You Are Entered Invalid Nominee Date of Birth.Date Format Should be [DD/MM/YYYY].Ex.01/01/1990";
                        return;
                    }
                    else
                    {
                        EmpNomineeDtofBirth = Timings.Instance.CheckDateFormat(txtNomDoB.Text);
                    }
                }

                EmpNomineeRel = txtEmpNomRel.Text;
                EmpInsCover = txtInsCover.Text;
                EmpInsDedAmt = txtInsDeb.Text;
                EmpUANNumber = txtUANNumber.Text;
                EmpEpfNo = txtEmpPFNumber.Text;
                EmpNominee = txtPFNominee.Text;

                if (txtPFEnrollDate.Text == "0")
                {
                    lblMsg.Text = "You Are Entered Invalid Date of PF Enroll.Date Format Should be [DD/MM/YYYY].Ex.01/01/1990";
                    return;
                }

                if (txtPFEnrollDate.Text.Trim().Length != 0)
                {

                    testDate = GlobalData.Instance.CheckEnteredDate(txtPFEnrollDate.Text);
                    if (testDate > 0)
                    {
                        lblMsg.Text = "You Are Entered Invalid Date Of PF Enrollment.Date Format Should be [DD/MM/YYYY].Ex.01/01/1990";
                        return;
                    }
                    else
                    {
                        EmpPFEnrolDt = Timings.Instance.CheckDateFormat(txtPFEnrollDate.Text);
                    }
                }


                CmpShortName = txtCmpShortName.Text;
                EmpRelation = txtPFNomineeRel.Text;


                EmpESINo = txtESINum.Text;
                EmpESINominee = txtESINominee.Text;
                EmpESIDispName = txtESIDiSName.Text;
                aadhaarid = txtaadhaar.Text;
                EmpESIRelation = txtESINomRel.Text;

                if (txtEmpDtofInterview.Text == "0")
                {
                    lblMsg.Text = "You Are Entered Invalid Date of Interview.Date Format Should be [DD/MM/YYYY].Ex.01/01/1990";
                    return;
                }
                if (txtEmpDtofJoining.Text == "0")
                {
                    lblMsg.Text = "You Are Entered Invalid Date of Joining.Date Format Should be [DD/MM/YYYY].Ex.01/01/1990";
                    return;
                }


                //if (txtEmpDtofInterview.SelectedDate.ToString() != "1/1/0001 12:00:00 AM" && txtEmpDtofJoining.SelectedDate.ToString() != "1/1/0001 12:00:00 AM")
                if (txtEmpDtofInterview.Text.Trim().Length != 0 && txtEmpDtofJoining.Text.Trim().Length != 0)
                {
                    string strDate = txtEmpDtofInterview.Text;
                    string EndDate = txtEmpDtofJoining.Text;
                    DateTime dt1;
                    DateTime dt2;

                    //  DateTime dt = DateTime.Parse(strDate, CultureInfo.GetCultureInfo("en-gb"));

                    dt1 = DateTime.Parse(strDate, CultureInfo.GetCultureInfo("en-gb"));
                    dt2 = DateTime.Parse(EndDate, CultureInfo.GetCultureInfo("en-gb"));
                    DateTime date1 = new DateTime(dt1.Year, dt1.Month, dt1.Day);
                    DateTime date2 = new DateTime(dt2.Year, dt2.Month, dt2.Day);

                    int result = DateTime.Compare(date1, date2);
                    if (result > 0)
                    {
                        lblMsg.Text = "Invalid Joining Date!";
                        return;
                    }
                }


                if (txtEmpDtofInterview.Text.Trim().Length > 0)
                {
                    testDate = GlobalData.Instance.CheckEnteredDate(txtEmpDtofInterview.Text);
                    if (testDate > 0)
                    {
                        lblMsg.Text = "You Are Entered Invalid Date Of Interview.Date Format Should be [DD/MM/YYYY].Ex.01/01/1990";
                        return;
                    }
                }

                if (txtEmpDtofJoining.Text.Trim().Length > 0)
                {
                    testDate = GlobalData.Instance.CheckEnteredDate(txtEmpDtofJoining.Text);
                    if (testDate > 0)
                    {
                        lblMsg.Text = "You Are Entered Invalid Date Of Joining.Date Format Should be [DD/MM/YYYY].Ex.01/01/1990";
                        return;
                    }
                }


                if (txtEmpDtofBirth.Text == "0")
                {
                    lblMsg.Text = "You Are Entered Invalid Date of Birth.Date Format Should be [DD/MM/YYYY].Ex.01/01/1990";
                    return;
                }

                if (txtEmpDtofBirth.Text.Trim().Length > 0)
                {
                    testDate = GlobalData.Instance.CheckEnteredDate(txtEmpDtofBirth.Text);
                    if (testDate > 0)
                    {
                        lblMsg.Text = "You Are Entered Invalid Date Of Birth.Date Format Should be [DD/MM/YYYY].Ex.01/01/1990";
                        return;
                    }
                }

                if (txtDofleaving.Text == "0")
                {
                    lblMsg.Text = "You Are Entered Invalid Nominee Date of Birth.Date Format Should be [DD/MM/YYYY].Ex.01/01/1990";
                    return;
                }

                if (txtDofleaving.Text.Trim().Length > 0)
                {
                    testDate = GlobalData.Instance.CheckEnteredDate(txtDofleaving.Text);
                    if (testDate > 0)
                    {
                        lblMsg.Text = "You Are Entered Invalid Date Of Leaving.Date Format Should be [DD/MM/YYYY].Ex.01/01/1990";
                        return;
                    }
                }
                if (txtEmpDtofBirth.Text.Trim().Length != 0)
                {
                    DateTime dayStart = DateTime.Parse(DateTime.Now.ToString());
                    DateTime dateEnd = DateTime.Parse(txtEmpDtofBirth.Text, CultureInfo.GetCultureInfo("en-gb"));

                    TimeSpan ts = dayStart - dateEnd;
                    int years = ts.Days / 365;

                    if (years < 18)
                    {

                        txtEmpDtofBirth.Text = "";
                        lblMsg.Text = "Age Should be above 18 years!";
                        return;
                    }

                }


                if (txtPhone.Text.Trim().Length == 0)
                {
                    lblMsg.Text = "Please fill Phone No.";
                    return;
                }
                if (txtPhone.Text.Trim().Length > 0)
                    if (txtPhone.Text.Trim().Length < 8)
                    {
                        lblMsg.Text = "Please enter a valid Phone Number!";
                        return;
                    }


                if (txtEmergContNo.Text.Trim().Length > 0)
                    if (txtEmergContNo.Text.Trim().Length < 8)
                    {
                        lblMsg.Text = "Please enter a valid Emergency Contact Number!";
                        return;
                    }

                if (rdbResigned.Checked == true)
                {
                    if (txtDofleaving.Text == " ")
                    {
                        lblMsg.Text = "Please fill a Date of Leaving";
                        return;
                    }

                }


                #region esi no


                string ESINo = txtESINum.Text;
                string ChEmpESINo = "";
                int esino = 0;

                if (txtESINum.Text.Trim().Length != 0)
                {
                    string SelBankacno = "select EmpESINo,EMPESICodes.Empid from EMPESICodes inner join empdetails ed on ed.empid=EMPESICodes.empid where ed.empid!='" + txtEmpid.Text + "' and  ed.empstatus=1";
                    DataTable dtBAcno = config.ExecuteReaderWithQueryAsync(SelBankacno).Result;

                    for (int i = 0; i < dtBAcno.Rows.Count; i++)
                    {
                        if (ESINo == dtBAcno.Rows[i]["EmpESINo"].ToString())
                        {
                            ChEmpESINo = dtBAcno.Rows[i]["Empid"].ToString();
                            esino = 1;
                            break;
                        }
                        else
                        {
                            esino = 0;
                        }
                    }

                }
                if (esino == 1)
                {
                    lblMsg.Text = "Employee details not saved because ESI Number '" + ESINo + "'already exists for Employee '" + ChEmpESINo + "'";
                    txtESINum.Text = "";
                    // ScriptManager.RegisterStartupScript(this, GetType(), "Show alert", "alert('Bank \"" + EmpBankAcNo + "\"Number already Exist for Employee \"" + EmpbankAcNoEmpid + "\"');", true);
                    return;
                }



                #endregion

                #region  for validations of PF,ESI ,BANK AC NO ,AADHAR NO,UAN NO by sharada on 26/01/2017

                #region validations of bankac no
                //Duplicate Bank a/c no 
                string ChEmpBankAcNo = txtBankAccNum.Text;
                string EmpbankAcNoEmpid = "";
                int Bankno = 0;

                if (txtBankAccNum.Text.Trim().Length != 0)
                {
                    string SelBankacno = "select EmpBankAcNo,Empid from EmpDetails  where empid!='" + txtEmpid.Text + "' and   empstatus=1";
                    DataTable dtBAcno = config.ExecuteReaderWithQueryAsync(SelBankacno).Result;

                    for (int i = 0; i < dtBAcno.Rows.Count; i++)
                    {
                        if (ChEmpBankAcNo == dtBAcno.Rows[i]["EmpBankAcNo"].ToString())
                        {
                            EmpbankAcNoEmpid = dtBAcno.Rows[i]["Empid"].ToString();
                            Bankno = 1;
                            break;
                        }
                        else
                        {
                            Bankno = 0;
                        }
                    }

                }
                if (Bankno == 1)
                {
                    lblMsg.Text = "Employee details not saved because Bank A/c Number '" + ChEmpBankAcNo + "'already exists for Employee '" + EmpbankAcNoEmpid + "'";
                    txtBankAccNum.Text = "";
                    // ScriptManager.RegisterStartupScript(this, GetType(), "Show alert", "alert('Bank \"" + EmpBankAcNo + "\"Number already Exist for Employee \"" + EmpbankAcNoEmpid + "\"');", true);
                    return;
                }
                #endregion of bank ac no

                #region validation of Aadhar no
                string AadharCardNo = txtAadharCard.Text;
                string eaadhaaridno = "";
                int aadhaaridno = 0;
                if (txtAadharCard.Text.Trim().Length != 0)
                {


                    string Selaadhaarid = "select AadharCardNo,EmpProofDetails.Empid from EmpProofDetails inner join empdetails ed on ed.empid=EmpProofDetails.empid where ed.empid!='" + txtEmpid.Text + "' and   ed.empstatus=1";
                    DataTable dtaadhaarid = config.ExecuteReaderWithQueryAsync(Selaadhaarid).Result;

                    for (int i = 0; i < dtaadhaarid.Rows.Count; i++)
                    {
                        if (AadharCardNo == dtaadhaarid.Rows[i]["AadharCardNo"].ToString())
                        {
                            eaadhaaridno = dtaadhaarid.Rows[i]["Empid"].ToString();
                            aadhaaridno = 1;
                            break;
                        }
                        else
                        {
                            aadhaaridno = 0;
                        }
                    }

                }
                if (aadhaaridno == 1)
                {
                    lblMsg.Text = "Employee details not saved because Aadhar Card Number '" + AadharCardNo + "'already exists for Employee '" + eaadhaaridno + "'";
                    txtAadharCard.Text = "";
                    // ScriptManager.RegisterStartupScript(this, GetType(), "Show alert", "alert('Bank \"" + EmpBankAcNo + "\"Number already Exist for Employee \"" + EmpbankAcNoEmpid + "\"');", true);
                    return;
                }

                #endregion  of aadhar no

                #region pf no


                string PFNo = txtEmpPFNumber.Text;
                string EmpPFNo = "";
                int pfno = 0;

                if (txtEmpPFNumber.Text.Trim().Length != 0)
                {
                    string SelBankacno = "select EmpEpfNo,EMPEPFCodes.Empid from EMPEPFCodes inner join empdetails ed on ed.empid=EMPEPFCodes.empid where ed.empid!='" + txtEmpid.Text + "' and   ed.empstatus=1";
                    DataTable dtBAcno = config.ExecuteReaderWithQueryAsync(SelBankacno).Result;

                    for (int i = 0; i < dtBAcno.Rows.Count; i++)
                    {
                        if (PFNo == dtBAcno.Rows[i]["EmpEpfNo"].ToString())
                        {
                            EmpPFNo = dtBAcno.Rows[i]["Empid"].ToString();
                            pfno = 1;
                            break;
                        }
                        else
                        {
                            pfno = 0;
                        }
                    }

                }
                if (pfno == 1)
                {
                    lblMsg.Text = "Employee details not saved because PF Number '" + PFNo + "'already exists for Employee '" + EmpPFNo + "'";
                    txtEmpPFNumber.Text = "";
                    // ScriptManager.RegisterStartupScript(this, GetType(), "Show alert", "alert('Bank \"" + EmpBankAcNo + "\"Number already Exist for Employee \"" + EmpbankAcNoEmpid + "\"');", true);
                    return;
                }



                #endregion


                #region validations of UAN no
                //Duplicate Bank a/c no 
                string EmpUANNo = txtUANNumber.Text;
                string EmpUANNoEmpid = "";
                int uanno = 0;

                if (txtUANNumber.Text.Trim().Length != 0)
                {
                    string SelBankacno = "select EmpUANNumber,Empid from EmpDetails  where empid!='" + txtEmpid.Text + "' and   empstatus=1";
                    DataTable dtBAcno = config.ExecuteReaderWithQueryAsync(SelBankacno).Result;

                    for (int i = 0; i < dtBAcno.Rows.Count; i++)
                    {
                        if (EmpUANNo == dtBAcno.Rows[i]["EmpUANNumber"].ToString())
                        {
                            EmpUANNoEmpid = dtBAcno.Rows[i]["Empid"].ToString();
                            uanno = 1;
                            break;
                        }
                        else
                        {
                            uanno = 0;
                        }
                    }

                }
                if (uanno == 1)
                {
                    lblMsg.Text = "Employee details not saved because UAN Number '" + EmpUANNo + "'already exists for Employee '" + EmpUANNoEmpid + "'";
                    txtUANNumber.Text = "";
                    // ScriptManager.RegisterStartupScript(this, GetType(), "Show alert", "alert('Bank \"" + EmpBankAcNo + "\"Number already Exist for Employee \"" + EmpbankAcNoEmpid + "\"');", true);
                    return;
                }
                #endregion of UAN no




                #endregion


                var empstatus = 1;
                if (rdbactive.Checked)
                    empstatus = 1;
                else
                    empstatus = 0;

                var empid = txtEmpid.Text;
                var firstName = txtEmpFName.Text;
                var middlename = txtEmpmiName.Text;

                var LastName = txtEmplname.Text;
                var MaritalStatus = "M";
                var gender = "M";
                var Designation = ddlDesignation.SelectedValue;
                var Unitid = DdlPreferedUnit.SelectedValue;
                int DateStatus = 0;
                var DateOfInterview = "01/01/1900";
                var DateOfJoining = "01/01/1900";
                var DateOfBirth = "01/01/1900";
                var DateOFLeaving = "01/01/1900";
                var IdCardValid = "01/01/1900";
                var IdCardIssued = "01/01/1900";
                string Image = "";
                string path = "";
                string StrPassBookFrontType = "";
                string PassBookFrontImage = "";
                string PassBookFrontImagepath = "";
                Image = txtEmpid.Text + "Photo.jpg";

                if (FileUploadImage.HasFile)
                {

                    path = Path.GetFileName(FileUploadImage.PostedFile.FileName);
                    FileUploadImage.PostedFile.SaveAs(Server.MapPath("~/assets/EmpPhotos/") + Image);
                    Image1.ImageUrl = ("/assets/EmpPhotos/") + Image;

                }
                else
                {
                    if (Image1.ImageUrl != null && Image1.ImageUrl != "")
                    {
                        Image1.ImageUrl = ("/assets/EmpPhotos/") + Image;
                    }
                    else
                    {
                        Image = "";
                    }

                }

                string Sign = "";
                string pathSign = "";
                Sign = txtEmpid.Text + "Sign.jpg";
                PassBookFrontImage = txtEmpid.Text + "PassBookFrontPhoto.jpg";
                //if (FileUploadSign.HasFile)
                //{

                //    pathSign = Path.GetFileName(FileUploadSign.PostedFile.FileName);
                //    FileUploadSign.PostedFile.SaveAs(Server.MapPath("~/assets/EmpSign/") + Sign);
                //    Image2.ImageUrl = ("/assets/EmpSign/") + Sign;

                //}
                //else
                //{
                //    if (Image2.ImageUrl != null && Image2.ImageUrl != "")
                //    {
                //        Image2.ImageUrl = ("/assets/EmpSign/") + Sign;
                //    }
                //    else
                //    {
                //        Sign = "";
                //    }

                //}

                if (FileUploadPassBookFront.HasFile)
                {
                    if (FileUploadPassBookFront.PostedFile.ContentLength < 1000000)
                    {
                        string StrExtension = System.IO.Path.GetExtension(FileUploadPassBookFront.FileName);
                        StrPassBookFrontType = StrExtension;
                        if (StrExtension.ToLower() != ".pdf")
                        {
                            PassBookFrontImage = txtEmpid.Text + "PassBookFrontPhoto.jpg";
                            PassBookFrontImagepath = Path.GetFileName(FileUploadPassBookFront.PostedFile.FileName);
                            FileUploadPassBookFront.PostedFile.SaveAs(Server.MapPath("~/assets/EmpPhotos/") + PassBookFrontImage);
                            //AadharFrontImg.ImageUrl = ("/assets/EmpPhotos/") + AadhaFrontImage;
                        }

                        else
                        {
                            PassBookFrontImage = txtEmpid.Text + "PassBookFrontPhoto.pdf";
                            StrPassBookFrontType = StrExtension;
                            //string filename = System.IO.Path.GetFileName(FileUploadAadharFront.FileName);
                            PassBookFrontImagepath = Path.GetFileName(FileUploadPassBookFront.PostedFile.FileName);
                            FileUploadPassBookFront.PostedFile.SaveAs(Server.MapPath("~/assets/EmpPhotos/") + PassBookFrontImage);
                        }
                    }
                    else
                    {
                        lblMsg.Text = "Plz Select  file Photo/pdf < 1 Mb";
                        return;
                    }
                }
                else
                {

                    if (ImgPassBookFront.ImageUrl != null && ImgPassBookFront.ImageUrl != "")
                    {
                        ImgPassBookFront.ImageUrl = ("/assets/EmpPhotos/") + PassBookFrontImage;
                    }
                    else
                    {
                        //lblMsg.Text = "Please file Photo/Pdf..!";
                        //return;
                        PassBookFrontImage = "";
                    }
                }



                if (txtEmpDtofInterview.Text.Trim().Length != 0)
                {
                    DateOfInterview = Timings.Instance.CheckDateFormat(txtEmpDtofInterview.Text);
                }

                if (txtEmpDtofJoining.Text.Trim().Length != 0)
                {
                    DateOfJoining = Timings.Instance.CheckDateFormat(txtEmpDtofJoining.Text);

                }

                if (txtEmpDtofBirth.Text.Trim().Length != 0)
                {
                    DateOfBirth = Timings.Instance.CheckDateFormat(txtEmpDtofBirth.Text);

                }


                if (txtDofleaving.Text.Trim().Length != 0)
                {
                    DateOFLeaving = Timings.Instance.CheckDateFormat(txtDofleaving.Text);
                }

                var EmpFatherName = txtFatherName.Text;
                var EmpFatherOccupation = txtfatheroccupation.Text;
                var EmpSpouseName = txtSpousName.Text;
                var EmpMotherName = txtMotherName.Text;
                var Qualification = txtQualification.Text;
                var PhoneNumber = txtPhone.Text;
                var Oldempid = txoldempid.Text;
                var mtounge = txtmtongue.Text;
                var nationality = txtnationality.Text;
                var Religion = txtreligion.Text;
                var PreviousEmployer = txtPreEmp.Text;
                var EmpLanguagesKnown = txtLangKnown.Text;
                var EmergContNo = txtEmergContNo.Text;
                var FormNo = txtForm.Text;
                var birthplace = txtbirthplace.Text;
                if (TxtIDCardIssuedDt.Text.Trim().Length != 0)
                {
                    IdCardIssued = Timings.Instance.CheckDateFormat(TxtIDCardIssuedDt.Text);

                }

                if (TxtIdCardValid.Text.Trim().Length != 0)
                {
                    IdCardValid = Timings.Instance.CheckDateFormat(TxtIdCardValid.Text);
                }

                var Branch = ddlBranch.SelectedValue;
                var ReportingManager = ddlReportingMgr.SelectedValue;
                var Division = ddlDivision.SelectedValue;
                var Department = ddldepartment.SelectedValue;

                var OldEmpid = txoldempid.Text;
                var URecordStatus = 0;

                //#region old Education start

                ////School variables 
                //string sscschool = txtschool.Text;
                //string sscbduniversity = txtbrd.Text;
                //string sscstdyear = txtyear.Text;
                //string sscpassfail = txtpsfi.Text;
                //string sscmarks = txtpmarks.Text;
                ////Intermediate
                //string imschool = txtimschool.Text;
                //string imbduniversity = txtimbrd.Text;
                //string imstdyear = txtimyear.Text;
                //string impassfail = txtimpsfi.Text;
                //string immarks = txtimpmarks.Text;
                ////Degree
                //string dgschool = txtdgschool.Text;
                //string dgbduniversity = txtdgbrd.Text;
                //string dgstdyear = txtdgyear.Text;
                //string dgpassfail = txtdgpsfi.Text;
                //string dgmarks = txtdgpmarks.Text;
                ////PG
                //string pgschool = txtpgschool.Text;
                //string pgbduniversity = txtpgbrd.Text;
                //string pgstdyear = txtpgyear.Text;
                //string pgpassfail = txtpgpsfi.Text;
                //string pgmarks = txtpgpmarks.Text; ;
                ////other variable declaration 

                //#endregion old Education end

                string EmpId = txtEmpid.Text;
                string refaddress1 = txtREfAddr1.Text;
                string refphnno1 = txtrefphnno.Text;
                string refaddress2 = txtREfAddr2.Text;
                string refphnno2 = txtrefno2.Text;
                string refmail1 = txtref1mail.Text;
                string refmail2 = txtref2mail.Text;
                string BloodGroup = "0";
                if (ddlBloodGroup.SelectedIndex > 0)
                {
                    BloodGroup = ddlBloodGroup.SelectedValue;
                }

                string empremarks = txtEmpRemarks.Text;
                string physicalremarks = txtPhyRem.Text;
                string idmark1 = txtImark1.Text;
                string idmark2 = txtImark2.Text;
                string EmpHeight = txtheight.Text;
                string EmpWeight = txtweight.Text;
                string EmpChestunex = txtcheunexpan.Text;
                string EmpChestExp = txtcheexpan.Text;
                string Haircolor = txthaircolour.Text;
                string EyesColor = txtEyeColour.Text;

                // string EmpFamilyDetails = txtFamDetails.Text;
                //string prDoorno = txtPrdoor.Text;
                //string prStreet = txtstreet.Text;
                //string prLmark = txtlmark.Text;
                //string prArea = txtarea.Text;
                //string prDistrict = txtdistrictt.Text;
                //string prPincode = txtpin.Text;
                // string PresentAddress = txtPresentAddress.Text;
                string prCity = ddlpreCity.SelectedValue;
                string prState = ddlpreStates.SelectedValue;
                string prphone = txtmobile.Text;
                string prperiodofstay = txtprPeriodofStay.Text;
                var prResidingDate = "";

                if (txtprResidingDate.Text.Trim().Length != 0)
                {
                    prResidingDate = Timings.Instance.CheckDateFormat(txtprResidingDate.Text);

                }
                var prpolicestation = string.Empty;
                var prtaluka = string.Empty;
                var prtown = string.Empty;
                var prLmark = string.Empty;
                var prPincode = string.Empty;
                var prPostOffice = string.Empty;


                prPostOffice = txtprPostOffice.Text;
                prpolicestation = txtprPoliceStation.Text;
                prtown = txtprvillage.Text;
                prtaluka = txtprtaluka.Text;
                prLmark = txtprLandmark.Text;
                prPincode = txtprpin.Text;


                //string pedoor = txtdoor1.Text;
                //string peStreet = txtstreet2.Text;
                //string pelmark = txtlmark3.Text;
                //string peArea = txtarea4.Text;
                //string peDistrict = txtPDist.Text;
                //string pePincode = txtpin7.Text;
                //string PermantAddress = txtPermanentAddress.Text;
                string peState = DdlStates.SelectedValue;
                string peCity = ddlcity.SelectedValue;
                string pephone = txtmobile9.Text;
                string periodofstay = txtPeriodofStay.Text;
                var ResidingDate = "";

                if (txtResidingDate.Text.Trim().Length != 0)
                {
                    ResidingDate = Timings.Instance.CheckDateFormat(txtResidingDate.Text);

                }

                var pepolicestation = string.Empty;
                var petaluka = string.Empty;
                var petown = string.Empty;
                var pelmark = string.Empty;
                var pePostOffice = string.Empty;
                var pePincode = string.Empty;


                petaluka = txtpeTaluka.Text;
                pepolicestation = txtpePoliceStattion.Text;
                petown = txtpevillage.Text;
                pelmark = txtpeLandmark.Text;
                pePostOffice = txtpePostOffice.Text;
                pePincode = txtpePin.Text;

                if (rdbsingle.Checked)
                    MaritalStatus = "S";
                else if (rdbWidower.Checked)
                    MaritalStatus = "W";
                else if (rdbdivorcee.Checked)
                    MaritalStatus = "D";
                else
                    MaritalStatus = "M";

                if (Rdb_Male.Checked)
                    gender = "M";
                else if (Rdb_Female.Checked)
                    gender = "F";
                else
                    gender = "T";



                int ESIDeduct;
                if (ChkESIDed.Checked)
                    ESIDeduct = 1;
                else
                    ESIDeduct = 0;

                int PFDeduct;
                if (ChkPFDed.Checked)
                    PFDeduct = 1;
                else
                    PFDeduct = 0;

                int PTDeduct;
                if (ChkPTDed.Checked)
                    PTDeduct = 1;
                else
                    PTDeduct = 0;


                int ExService;
                if (ChkExService.Checked)
                    ExService = 1;
                else
                    ExService = 0;


                #region for New Fields fo Salary Structure

                var Basic = "0";
                var DA = "0";
                var HRA = "0";
                var Conveyance = "0";
                var CCA = "0";
                var Bonus = "0";
                var Gratuity = "0";
                var WA = "0";
                var NFHs = "0";
                var MedicalAllw = "0";
                var MobileAllw = "0";
                var SplAllowance = "0";
                var LA = "0";
                var OA = "0";
                var RC = "0";
                var CS = "0";
                var NoofDays = "0";
                var NoOfOts = "0";
                var OTRate = "0";
                var TravellingAllowance = "0";
                var PerformanceAllowance = "0";
                var PFPayrate = "0";
                var ESIPayrate = "0";
                var EducationAllw = "0";
                var PFVoluntary = "0";

                Basic = TxtBasic.Text;
                DA = txtda.Text;
                HRA = txthra.Text;
                Conveyance = txtConveyance.Text;
                CCA = txtcca.Text;
                Bonus = txtbonus.Text;
                Gratuity = txtgratuty.Text;
                WA = txtwa.Text;
                NFHs = txtNfhs1.Text;
                MedicalAllw = txtMedicalAllw.Text;
                MobileAllw = txtMobileAllowance.Text;
                SplAllowance = txtSplAllw.Text;
                LA = txtleaveamount.Text;
                OA = txtoa.Text;
                RC = Txtrc.Text;
                CS = TxtCs.Text;
                OTRate = TxtOTRate.Text;
                TravellingAllowance = txtTravellingAllowance.Text;
                PerformanceAllowance = txtPerformanceAllowance.Text;
                PFPayrate = TxtPFPayRate.Text;
                ESIPayrate = TxtESIPayRate.Text;
                EducationAllw = txtEducationAllowance.Text;
                PFVoluntary = TxtPFVoluntary.Text;


                if (ddlNoOfDaysWages.SelectedIndex == 0)
                {
                    NoofDays = "0";
                }
                if (ddlNoOfDaysWages.SelectedIndex == 1)
                {
                    NoofDays = "1";
                }
                if (ddlNoOfDaysWages.SelectedIndex == 2)
                {
                    NoofDays = "2";
                }
                if (ddlNoOfDaysWages.SelectedIndex == 3)
                {
                    NoofDays = "3";
                }
                if (ddlNoOfDaysWages.SelectedIndex == 4)
                {
                    NoofDays = "4";
                }

                if (ddlNoOfDaysWages.SelectedIndex > 4)
                {
                    NoofDays = ddlNoOfDaysWages.SelectedValue;
                }

                if (ddlNoOfOtsPaysheet.SelectedIndex == 0)
                {
                    NoOfOts = "0";
                }
                if (ddlNoOfOtsPaysheet.SelectedIndex == 1)
                {
                    NoOfOts = "1";
                }
                if (ddlNoOfOtsPaysheet.SelectedIndex == 2)
                {
                    NoOfOts = "2";
                }
                if (ddlNoOfOtsPaysheet.SelectedIndex == 3)
                {
                    NoOfOts = "3";
                }
                if (ddlNoOfOtsPaysheet.SelectedIndex == 4)
                {
                    NoOfOts = "4";
                }

                if (ddlNoOfOtsPaysheet.SelectedIndex > 4)
                {
                    NoOfOts = ddlNoOfOtsPaysheet.SelectedValue;
                }


                string pfnods = "0";

                if (ddlPFNoOfDaysForWages.SelectedIndex == 0)
                {
                    pfnods = "0";
                }
                if (ddlPFNoOfDaysForWages.SelectedIndex == 1)
                {
                    pfnods = "1";
                }
                if (ddlPFNoOfDaysForWages.SelectedIndex == 2)
                {
                    pfnods = "2";
                }
                if (ddlNoOfOtsPaysheet.SelectedIndex == 3)
                {
                    pfnods = "3";
                }
                if (ddlNoOfOtsPaysheet.SelectedIndex == 4)
                {
                    pfnods = "4";
                }

                if (ddlPFNoOfDaysForWages.SelectedIndex > 4)
                {
                    pfnods = ddlPFNoOfDaysForWages.SelectedValue;
                }



                #endregion for New Fields fo Salary Structure


                #region Begining of Declaring variables and assigning values to that variables

                string empId = txtEmpid.Text;
                string ServiceNumber = txtServiceNum.Text;
                string Rank = txtRank.Text;
                string Crops = txtCrops.Text;
                string Trade = txtTrade.Text;


                string DateOfEnrollment = "01/01/1900";

                if (txtDOfEnroll.Text == "0")
                {
                    lblMsg.Text = "You Are Entered Invalid Date of Enroll .Date Format Should be [DD/MM/YYYY].Ex.01/01/1990";
                    return;
                }

                if (txtDOfEnroll.Text.Trim().Length != 0)
                {
                    // DateOfEnrollment = DateTime.Parse(txtDOfEnroll.Text, CultureInfo.GetCultureInfo("en-gb")).ToString();
                    DateOfEnrollment = Timings.Instance.CheckDateFormat(txtDOfEnroll.Text);
                }


                string DateofDischarge = "01/01/1900";

                if (txtDofDischarge.Text == "0")
                {
                    lblMsg.Text = "You Are Entered Invalid Date of Discharge.Date Format Should be [DD/MM/YYYY].Ex.01/01/1990";
                    return;
                }

                if (txtDofDischarge.Text.Trim().Length != 0)
                {
                    //  DateofDischarge = DateTime.Parse(txtDOFDischarge.Text, CultureInfo.GetCultureInfo("en-gb")).ToString();
                    DateofDischarge = Timings.Instance.CheckDateFormat(txtDofDischarge.Text);

                }

                string MedicalBloodGroup = txtMCategory.Text;
                string ReasonOfDischarge = TxtROfDischarge.Text;
                string Conduct = txtConduct.Text;
                string Addlamount = txtaddlamt.Text;
                string FoodAllowance = txtfoodallowance.Text;

                var Exservice = 0;
                #endregion Ending of Declaring variables and assigning values to that variables

                #region for birth Details,other fields on 10-12-2015

                var BirthDistrict = "0";
                var BirthCountry = "";
                var BirthVillage = "";
                var BirthState = "0";
                var ApplicantCategory = "";
                var SpeciallyAbled = 0;
                var Title = "";
                var Gross = " ";

                // BirthDistrict = ddlBirthDistrict.SelectedValue;
                // BirthCountry = txtBirthCountry.Text;
                // BirthVillage = txtBirthVillage.Text;
                // BirthState = ddlbirthstate.SelectedValue;
                ApplicantCategory = ddlAppCategory.SelectedValue;
                if (ChkSpeciallyAbled.Checked)
                    SpeciallyAbled = 1;
                Title = ddlTitle.SelectedValue;
                Gross = txtGrossSalary.Text;

                #endregion for birth Details


                #region for Proof Details,other fields on 10-12-2015

                var AadharCard = "N";
                AadharCardNo = "";
                var AadharCardName = "";
                var drivingLicense = "N";
                var drivingLicenseNo = "";
                var drivingLicenseName = "";
                var VoterID = "N";
                var VoterIDNo = "";
                var VoterIDName = "";
                var RationCard = "N";
                var RationCardNo = "";
                var RationCardName = "";
                var PanCard = "N";
                var PanCardNo = "";
                var PanCardName = "";
                var BankPassbook = "N";
                var BankPassbookNo = "";
                var BankPassbookName = "";
                var ElectricityBill = "N";
                var ElectricityBillNo = "";
                var ElectricityBillName = "";
                var Other = "N";
                var Othertext = "";
                var Othertextname = "";
                var ESICName = "";
                var ESICCardNo = "";
                var ESICCard = "N";
                var SSCCertificate = "N";
                var SSCCertificateNo = "";

                var aadharcardstatus = "N";
                var Drivingstatus = "N";
                var Voterstatus = "N";
                var Rationcardstatus = "N";
                var Pancardstatus = "N";
                var Bankpassbookstatus = "N";
                var Electricitystatus = "N";
                var ESICstatus = "N";
                var Othersstatus = "N";
                var SSCStatus = "N";

                var aadharcardCopystatus = "";
                var DrivingCopystatus = "";
                var VoterCopystatus = "";
                var RationcardCopystatus = "";
                var PancardCopystatus = "";
                var BankpassbookCopystatus = "";
                var ElectricityCopystatus = "";
                var ESICCopystatus = "";
                var OthersCopystatus = "";
                var SSCCopyStatus = "";

                if (ChkAadharCard.Checked)
                    AadharCard = "Y";
                AadharCardNo = txtAadharCard.Text;
                AadharCardName = txtAadharName.Text;
                if (RbnNotsubmitted.Checked == true)
                {
                    aadharcardstatus = "N";
                }
                else
                {
                    aadharcardstatus = "Y";
                }
                if (Rbnsubmitted.Checked == true)
                {
                    if (RbnXerox.Checked == true)
                    {
                        aadharcardCopystatus = "Xerox";
                    }
                    else
                    {
                        aadharcardCopystatus = "Original";
                    }
                }
                if (ChkdrivingLicense.Checked)
                    drivingLicense = "Y";
                drivingLicenseNo = txtDrivingLicense.Text;
                drivingLicenseName = txtDrivingLicenseName.Text;
                if (RbnNotsubmitted1.Checked == true)
                {
                    Drivingstatus = "N";
                }
                else
                {
                    Drivingstatus = "Y";
                }
                if (Rbnsubmitted1.Checked == true)
                {
                    if (RbnXerox1.Checked == true)
                    {
                        DrivingCopystatus = "Xerox";
                    }
                    else
                    {
                        DrivingCopystatus = "Original";
                    }
                }
                if (ChkVoterID.Checked)
                    VoterID = "Y";
                VoterIDNo = txtVoterID.Text;
                VoterIDName = txtVoterName.Text;

                if (RbnNotsubmitted2.Checked == true)
                {
                    Voterstatus = "N";
                }
                else
                {
                    Voterstatus = "Y";
                }
                if (Rbnsubmitted2.Checked == true)
                {
                    if (RbnXerox2.Checked == true)
                    {
                        VoterCopystatus = "Xerox";
                    }
                    else
                    {
                        VoterCopystatus = "Original";
                    }
                }
                if (ChkRationCard.Checked)
                    RationCard = "Y";
                RationCardNo = txtRationCard.Text;
                RationCardName = txtRationCardName.Text;
                if (RbnNotsubmitted3.Checked == true)
                {
                    Rationcardstatus = "N";
                }
                else
                {
                    Rationcardstatus = "Y";
                }
                if (Rbnsubmitted3.Checked == true)
                {
                    if (RbnXerox3.Checked == true)
                    {
                        RationcardCopystatus = "Xerox";
                    }
                    else
                    {
                        RationcardCopystatus = "Original";
                    }
                }
                if (ChkPanCard.Checked)
                    PanCard = "Y";
                PanCardNo = txtPanCard.Text;
                PanCardName = txtPanCardName.Text;
                if (RbnNotsubmitted5.Checked == true)
                {
                    Pancardstatus = "N";
                }
                else
                {
                    Pancardstatus = "Y";
                }
                if (Rbnsubmitted5.Checked == true)
                {
                    if (RbnXerox5.Checked == true)
                    {
                        PancardCopystatus = "Xerox";
                    }
                    else
                    {
                        PancardCopystatus = "Original";
                    }
                }
                if (ChkBankPassbook.Checked)
                    BankPassbook = "Y";
                BankPassbookNo = txtBankPassbook.Text;
                BankPassbookName = txtBankPassBookName.Text;
                if (RbnNotsubmitted6.Checked == true)
                {
                    Bankpassbookstatus = "N";
                }
                else
                {
                    Bankpassbookstatus = "Y";
                }
                if (Rbnsubmitted6.Checked == true)
                {
                    if (RbnXerox6.Checked == true)
                    {
                        BankpassbookCopystatus = "Xerox";
                    }
                    else
                    {
                        BankpassbookCopystatus = "Original";
                    }
                }

                if (ChkElectricityBill.Checked)
                    ElectricityBill = "Y";
                ElectricityBillNo = txtElectricityBill.Text;
                ElectricityBillName = txtElecBillname.Text;
                if (RbnNotsubmitted7.Checked == true)
                {
                    Electricitystatus = "N";
                }
                else
                {
                    Electricitystatus = "Y";
                }
                if (Rbnsubmitted7.Checked == true)
                {
                    if (RbnXerox7.Checked == true)
                    {
                        ElectricityCopystatus = "Xerox";
                    }
                    else
                    {
                        ElectricityCopystatus = "Original";
                    }
                }
                if (ChkESICCard.Checked)
                    ESICCard = "Y";
                ESICCardNo = txtESICCardNo.Text;
                ESICName = txtESICName.Text;
                if (RbnNotsubmitted8.Checked == true)
                {
                    ESICstatus = "N";
                }
                else
                {
                    ESICstatus = "Y";
                }
                if (Rbnsubmitted8.Checked == true)
                {
                    if (RbnXerox8.Checked == true)
                    {
                        ESICCopystatus = "Xerox";
                    }
                    else
                    {
                        ESICCopystatus = "Original";
                    }

                }
                if (Chkother.Checked)
                    Other = "Y";
                Othertext = txtOther.Text;
                Othertextname = txtOtherName.Text;

                if (RbnNotsubmitted4.Checked == true)
                {
                    Othersstatus = "N";
                }
                else
                {
                    Othersstatus = "Y";
                }
                if (Rbnsubmitted4.Checked == true)
                {
                    if (RbnXerox4.Checked == true)
                    {
                        OthersCopystatus = "Xerox";
                    }
                    else
                    {
                        OthersCopystatus = "Original";
                    }
                }

                if (ChkSSC.Checked)
                    SSCCertificate = "Y";
                SSCCertificateNo = txtsscno.Text;

                if (RbnNotSubmitted9.Checked == true)
                {
                    SSCStatus = "N";
                }
                else
                {
                    SSCStatus = "Y";
                }
                if (RbnSubmitted9.Checked == true)
                {
                    if (RbnXerox9.Checked == true)
                    {
                        SSCCopyStatus = "Xerox";
                    }
                    else
                    {
                        SSCCopyStatus = "Original";
                    }

                }

                string AadhaImage = "";
                string AadhaImagepath = "";
                AadhaImage = txtEmpid.Text + "AadharFrontPhoto.jpg";

                if (FileUploadAadharImage.HasFile)
                {

                    AadhaImagepath = Path.GetFileName(FileUploadAadharImage.PostedFile.FileName);
                    FileUploadAadharImage.PostedFile.SaveAs(Server.MapPath("~/assets/EmpPhotos/") + AadhaImage);
                    AadharImg.ImageUrl = ("/assets/EmpPhotos/") + AadhaImage;

                }
                else
                {
                    if (AadharImg.ImageUrl != null && AadharImg.ImageUrl != "")
                    {
                        AadharImg.ImageUrl = ("/assets/EmpPhotos/") + AadhaImage;
                    }
                    else
                    {
                        AadhaImage = "";
                    }

                }

                #endregion for Proof Details,other fields on 10-12-2015

                #region for policerecord on 11-12-2015

                var CriminalOffCName = "";
                var CriminalOffcaseNo = "";
                var CriminalOff = "";
                var CriminalProCName = "";
                var CriminalProCaseNo = "";
                var CriminalProOffence = "";
                var CriminalArrestCName = "";
                var CriminalArrestCaseNo = "";
                var CriminalArrestOffence = "";
                var PoliceVerCode = "";
                var PoliceverificationCheck = "N";
                var CriminalOffCheck = "N";
                var CriminalProCheck = "N";
                var CriminalArrestCheck = "N";
                var PsaraEmpCode = "";
                var Email = "";
                var NearestPoliceStation = "";

                if (rdbVerified.Checked == true)
                {
                    PoliceverificationCheck = "Y";
                }

                if (ChkCrimalArrest.Checked == true)
                {
                    CriminalArrestCheck = "Y";
                }

                if (ChkCriminalOff.Checked == true)
                {
                    CriminalOffCheck = "Y";
                }

                if (ChkCriminalProc.Checked == true)
                {
                    CriminalProCheck = "Y";
                }

                CriminalOffCName = txtCriminalOffCName.Text;
                CriminalOffcaseNo = txtCriminalOffcaseNo.Text;
                CriminalOff = txtCriminalOff.Text;
                CriminalProCName = txtCriminalProCName.Text;
                CriminalProCaseNo = txtCriminalProCaseNo.Text;
                CriminalProOffence = txtCriminalProOffence.Text;
                CriminalArrestCName = txtCriminalArrestCName.Text;
                CriminalArrestCaseNo = txtCriminalArrestCaseNo.Text;
                CriminalArrestOffence = txtCriminalArrestOffence.Text;

                Email = txtemail.Text;
                PoliceVerCode = txtPoliceVerificationNo.Text;
                PsaraEmpCode = txtpsaraempcode.Text;
                NearestPoliceStation = txtPoliceStation.Text;

                #endregion for policerecord on 11-12-2015

                #region ShitDetails
                var Shift = "";
                var Shiftstarttime = "";
                var Shiftendtime = "";
                var Woff1 = 0;
                var Woff2 = 0;

                Shift = ddlShift.SelectedValue;
                Shiftstarttime = txtShiftstarttime.Text;
                Shiftendtime = txtShiftEndtime.Text;
                Woff1 = ddlWoff1.SelectedIndex;
                Woff2 = ddlWoff2.SelectedIndex;


                #endregion

                #region    Begin Code For Stored Procedure Parameters
                Hashtable ModifyEmployeeDetails = new Hashtable();
                string AddEmpPersonalDetails = "ModifyEmpDetails";
                #region passing paramers to stored procedure
                ModifyEmployeeDetails.Add("@Empstatus", empstatus);
                ModifyEmployeeDetails.Add("@EmpId", empid);
                ModifyEmployeeDetails.Add("@EmpFName", firstName);
                ModifyEmployeeDetails.Add("@EmpMName", middlename);
                ModifyEmployeeDetails.Add("@EmpLName", LastName);
                ModifyEmployeeDetails.Add("@Empsex", gender);
                ModifyEmployeeDetails.Add("@EmpDesgn", Designation);
                ModifyEmployeeDetails.Add("@EmpDtofInterview", DateOfInterview);
                ModifyEmployeeDetails.Add("@EmpDtofJoining", DateOfJoining);
                ModifyEmployeeDetails.Add("@EmpDtofLeaving", DateOFLeaving);
                ModifyEmployeeDetails.Add("@EmpDtofBirth", DateOfBirth);
                ModifyEmployeeDetails.Add("@EmpFatherName", EmpFatherName);
                ModifyEmployeeDetails.Add("@EmpFatherOccupation", EmpFatherOccupation);
                ModifyEmployeeDetails.Add("@OldEmpid", OldEmpid);
                ModifyEmployeeDetails.Add("@EmpSpouseName", EmpSpouseName);
                ModifyEmployeeDetails.Add("@EmpMotherName", EmpMotherName);
                ModifyEmployeeDetails.Add("@EmpQualification", Qualification);
                ModifyEmployeeDetails.Add("@EmpMaritalStatus", MaritalStatus);
                ModifyEmployeeDetails.Add("@EmpPhone", PhoneNumber);
                ModifyEmployeeDetails.Add("@EmpEmergContNo", EmergContNo);
                ModifyEmployeeDetails.Add("@FormNo", FormNo);
                ModifyEmployeeDetails.Add("@BirthPlace", birthplace);
                ModifyEmployeeDetails.Add("@EmpPFDeduct", PFDeduct);
                ModifyEmployeeDetails.Add("@EmpESIDeduct", ESIDeduct);
                ModifyEmployeeDetails.Add("@EmpExservice", ExService);
                ModifyEmployeeDetails.Add("@EmpPTDeduct", PTDeduct);
                ModifyEmployeeDetails.Add("@MotherTongue", mtounge);
                ModifyEmployeeDetails.Add("@Nationality", nationality);
                ModifyEmployeeDetails.Add("@EmpLanguagesKnown", EmpLanguagesKnown);
                ModifyEmployeeDetails.Add("@EmpPreviousExp", PreviousEmployer);
                ModifyEmployeeDetails.Add("@Branch", Branch);
                ModifyEmployeeDetails.Add("@Department", Department);
                ModifyEmployeeDetails.Add("@Division", Division);
                ModifyEmployeeDetails.Add("@ReportingManager", ReportingManager);
                ModifyEmployeeDetails.Add("@IDCardIssued", IdCardIssued);
                ModifyEmployeeDetails.Add("@IDCardValid", IdCardValid);
                ModifyEmployeeDetails.Add("@Image", Image);
                ModifyEmployeeDetails.Add("@Empsign", PassBookFrontImage);
                ModifyEmployeeDetails.Add("@unitid", Unitid);
                ModifyEmployeeDetails.Add("@PassBookFrontImage", StrPassBookFrontType);

                ModifyEmployeeDetails.Add("@Shift", Shift);
                ModifyEmployeeDetails.Add("@Shiftstarttime", Shiftstarttime);
                ModifyEmployeeDetails.Add("@Shiftendtime", Shiftendtime);
                ModifyEmployeeDetails.Add("@Woff1", Woff1);
                ModifyEmployeeDetails.Add("@Woff2", Woff2);

                #region for salary structure

                ModifyEmployeeDetails.Add("@Basic", Basic);
                ModifyEmployeeDetails.Add("@DA", DA);
                ModifyEmployeeDetails.Add("@HRA", HRA);
                ModifyEmployeeDetails.Add("@Conveyance", Conveyance);
                ModifyEmployeeDetails.Add("@CCA", CCA);
                ModifyEmployeeDetails.Add("@Bonus", Bonus);
                ModifyEmployeeDetails.Add("@Gratuity", Gratuity);
                ModifyEmployeeDetails.Add("@WashAllownce", WA);
                ModifyEmployeeDetails.Add("@NFHs", NFHs);
                ModifyEmployeeDetails.Add("@MedicalAllw", MedicalAllw);
                ModifyEmployeeDetails.Add("@MobileAllw", MobileAllw);
                ModifyEmployeeDetails.Add("@LA", LA);
                ModifyEmployeeDetails.Add("@OtherAllowance", OA);
                ModifyEmployeeDetails.Add("@RC", RC);
                ModifyEmployeeDetails.Add("@CS", CS);
                ModifyEmployeeDetails.Add("@SplAllowance", SplAllowance);
                ModifyEmployeeDetails.Add("@NoofDays", NoofDays);
                ModifyEmployeeDetails.Add("@NoOfOts", NoOfOts);
                ModifyEmployeeDetails.Add("@pfnods", pfnods);
                ModifyEmployeeDetails.Add("@OTRate", OTRate);
                ModifyEmployeeDetails.Add("@TravellingAllowance", TravellingAllowance);
                ModifyEmployeeDetails.Add("@PerformanceAllowance", PerformanceAllowance);
                ModifyEmployeeDetails.Add("@PFPayrate", PFPayrate);
                ModifyEmployeeDetails.Add("@ESIPayrate", ESIPayrate);
                ModifyEmployeeDetails.Add("@PFVoluntary", PFVoluntary);
                ModifyEmployeeDetails.Add("@EducationAllw", EducationAllw);

                #endregion for Salary structure

                #region passing paramers to stored procedure

                ModifyEmployeeDetails.Add("@EmpRefAddr1", refaddress1);
                ModifyEmployeeDetails.Add("@Refphoneno1", refphnno1);
                ModifyEmployeeDetails.Add("@EmpRefAddr2", refaddress2);
                ModifyEmployeeDetails.Add("@Refphoneno2", refphnno2);
                ModifyEmployeeDetails.Add("@EmpBloodGroup", BloodGroup);
                ModifyEmployeeDetails.Add("@EmpRemarks", empremarks);
                ModifyEmployeeDetails.Add("@EmpPhysicalRemarks", physicalremarks);
                ModifyEmployeeDetails.Add("@EmpIdMark1", idmark1);
                ModifyEmployeeDetails.Add("@EmpIdMark2", idmark2);
                ModifyEmployeeDetails.Add("@EmpWeight", EmpWeight);
                ModifyEmployeeDetails.Add("@EmpHeight", EmpHeight);
                ModifyEmployeeDetails.Add("@Refmail1", refmail1);
                ModifyEmployeeDetails.Add("@Refmail2", refmail2);
                //ModifyEmployeeDetails.Add("@prDoorno", prDoorno);
                //ModifyEmployeeDetails.Add("@prStreet", prStreet);
                //ModifyEmployeeDetails.Add("@prLmark", prLmark);
                //ModifyEmployeeDetails.Add("@prArea", prArea);
                //ModifyEmployeeDetails.Add("@prDistrict", prDistrict);
                //ModifyEmployeeDetails.Add("@prPincode", prPincode);
                //ModifyEmployeeDetails.Add("@pedoor", pedoor);
                //ModifyEmployeeDetails.Add("@peStreet", peStreet);
                //ModifyEmployeeDetails.Add("@peDistrict", peDistrict);
                //ModifyEmployeeDetails.Add("@pelmark", pelmark);
                //ModifyEmployeeDetails.Add("@peArea", peArea);
                //ModifyEmployeeDetails.Add("@pePincode", pePincode);
                // ModifyEmployeeDetails.Add("@EmpPresentAddress", PresentAddress);
                ModifyEmployeeDetails.Add("@prCity", prCity);
                ModifyEmployeeDetails.Add("@prState", prState);
                ModifyEmployeeDetails.Add("@prphone", prphone);
                ModifyEmployeeDetails.Add("@prResidingDate", prResidingDate);
                ModifyEmployeeDetails.Add("@prperiodofstay", prperiodofstay);
                ModifyEmployeeDetails.Add("@prtaluka", prtaluka);
                ModifyEmployeeDetails.Add("@prtown", prtown);
                ModifyEmployeeDetails.Add("@prpolicestation", prpolicestation);
                ModifyEmployeeDetails.Add("@prLmark", prLmark);
                ModifyEmployeeDetails.Add("@prPostOffice", prPostOffice);
                ModifyEmployeeDetails.Add("@prPincode", prPincode);
                // ModifyEmployeeDetails.Add("@EmpPermanentAddress", PermantAddress);
                ModifyEmployeeDetails.Add("@peCity", peCity);
                ModifyEmployeeDetails.Add("@peState", peState);
                ModifyEmployeeDetails.Add("@pephone", pephone);
                ModifyEmployeeDetails.Add("@periodofstay", periodofstay);
                ModifyEmployeeDetails.Add("@ResidingDate", ResidingDate);
                ModifyEmployeeDetails.Add("@petaluka", petaluka);
                ModifyEmployeeDetails.Add("@petown", petown);
                ModifyEmployeeDetails.Add("@pepolicestation", pepolicestation);
                ModifyEmployeeDetails.Add("@pelmark", pelmark);
                ModifyEmployeeDetails.Add("@pePostOffice", pePostOffice);
                ModifyEmployeeDetails.Add("@pePincode", pePincode);
                ModifyEmployeeDetails.Add("@EmpChestunex", EmpChestunex);
                ModifyEmployeeDetails.Add("@EmpChestExp", EmpChestExp);

                #endregion passing parameters to stored procedure

                #region End 1-5 Bank Name to Branch Code

                ModifyEmployeeDetails.Add("@empbankname", Empbankname);
                ModifyEmployeeDetails.Add("@empbankacno", EmpBankAcNo);
                ModifyEmployeeDetails.Add("@Empbankbrabchname", Empbankbranchname); //dought
                ModifyEmployeeDetails.Add("@empifsccode", EmpIFSCcode);
                ModifyEmployeeDetails.Add("@empbranchcode", EmpBranchCode);

                #endregion End 1-5 Bank Name to Branch Code

                #region End 6-10  Bank CodeNo to Branch Card Reference
                ModifyEmployeeDetails.Add("@empbankcode", EmpBankCode);
                ModifyEmployeeDetails.Add("@empbankappno", EmpBankAppNo);
                ModifyEmployeeDetails.Add("@empregioncode", EmpRegionCode);
                ModifyEmployeeDetails.Add("@empinsnominee", EmpInsNominee);
                ModifyEmployeeDetails.Add("@empbankcardref", EmpBankCardRef);

                #endregion End 6-10  Bank CodeNo to Branch Card Reference

                #region Begin 11-15  Nominee Date of Borth to SS No.

                ModifyEmployeeDetails.Add("@empnomineedtofbirth", EmpNomineeDtofBirth);
                ModifyEmployeeDetails.Add("@empnomineerel", EmpNomineeRel);
                ModifyEmployeeDetails.Add("@empinscover", EmpInsCover);
                ModifyEmployeeDetails.Add("@empinsdedamt", EmpInsDedAmt);
                ModifyEmployeeDetails.Add("@empUANnumber", EmpUANNumber);

                #endregion Begin 11-15  Nominee Date of Borth to SS No.

                #region Begin 16-20  EPFNo to EF Nominee Relation

                ModifyEmployeeDetails.Add("@empepfno", EmpEpfNo);
                ModifyEmployeeDetails.Add("@empnominee", EmpNominee);
                ModifyEmployeeDetails.Add("@emppfenroldt", EmpPFEnrolDt);
                ModifyEmployeeDetails.Add("@cmpshortname", CmpShortName);
                ModifyEmployeeDetails.Add("@emprelation", EmpRelation);

                #endregion Begin 16-20  EPFNo to EF Nominee Relation

                #region Begin 21-26  ESINo to EmpId

                ModifyEmployeeDetails.Add("@empesino", EmpESINo);
                ModifyEmployeeDetails.Add("@empesinominee", EmpESINominee);
                ModifyEmployeeDetails.Add("@empesidispname", EmpESIDispName);
                ModifyEmployeeDetails.Add("@aadhaarid", aadhaarid);
                ModifyEmployeeDetails.Add("@empesirelation", EmpESIRelation);
                #endregion Begin 21-26  ESINo to EmpId




                ModifyEmployeeDetails.Add("@ServiceNo", ServiceNumber);
                ModifyEmployeeDetails.Add("@Rank", Rank);
                ModifyEmployeeDetails.Add("@Crops", Crops);
                ModifyEmployeeDetails.Add("@Trade", Trade);
                ModifyEmployeeDetails.Add("@dtofenroment", DateOfEnrollment);
                ModifyEmployeeDetails.Add("@daofdischarge", DateofDischarge);
                ModifyEmployeeDetails.Add("@medicalcategorybloodgroup", MedicalBloodGroup);
                ModifyEmployeeDetails.Add("@ReasonsofDischarge", ReasonOfDischarge);
                ModifyEmployeeDetails.Add("@Conduct", Conduct);
                ModifyEmployeeDetails.Add("@AddlAmount", Addlamount);
                ModifyEmployeeDetails.Add("@FoodAllowance", FoodAllowance);

                #region for police record

                ModifyEmployeeDetails.Add("@CriminalOffCName", CriminalOffCName);
                ModifyEmployeeDetails.Add("@CriminalOffcaseNo", CriminalOffcaseNo);
                ModifyEmployeeDetails.Add("@CriminalOff", CriminalOff);
                ModifyEmployeeDetails.Add("@CriminalProCName", CriminalProCName);
                ModifyEmployeeDetails.Add("@CriminalProCaseNo", CriminalProCaseNo);
                ModifyEmployeeDetails.Add("@CriminalProOffence", CriminalProOffence);
                ModifyEmployeeDetails.Add("@CriminalArrestCName", CriminalArrestCName);
                ModifyEmployeeDetails.Add("@CriminalArrestCaseNo", CriminalArrestCaseNo);
                ModifyEmployeeDetails.Add("@CriminalArrestOffence", CriminalArrestOffence);
                ModifyEmployeeDetails.Add("@CriminalProCheck", CriminalProCheck);
                ModifyEmployeeDetails.Add("@CriminalArrestCheck", CriminalArrestCheck);
                ModifyEmployeeDetails.Add("@CriminalOffCheck", CriminalOffCheck);
                ModifyEmployeeDetails.Add("@PoliceverificationCheck", PoliceverificationCheck);
                ModifyEmployeeDetails.Add("@NearestPoliceStation", NearestPoliceStation);

                #endregion for police record


                #region for Birth Details, other fields

                ModifyEmployeeDetails.Add("@BirthDistrict", BirthDistrict);
                ModifyEmployeeDetails.Add("@BirthCountry", BirthCountry);
                ModifyEmployeeDetails.Add("@BirthVillage", BirthVillage);
                ModifyEmployeeDetails.Add("@BirthState", BirthState);
                ModifyEmployeeDetails.Add("@ApplicantCategory", ApplicantCategory);
                ModifyEmployeeDetails.Add("@SpeciallyAbled", SpeciallyAbled);
                ModifyEmployeeDetails.Add("@Title", Title);
                ModifyEmployeeDetails.Add("@Gross", Gross);
                ModifyEmployeeDetails.Add("@Email", Email);
                ModifyEmployeeDetails.Add("@PoliceVerCode", PoliceVerCode);
                ModifyEmployeeDetails.Add("@PsaraEmpCode", PsaraEmpCode);
                ModifyEmployeeDetails.Add("@Haircolor", Haircolor);
                ModifyEmployeeDetails.Add("@EyesColor", EyesColor);

                #endregion for Birth Details, other fields


                #region for Proofs Submitted

                ModifyEmployeeDetails.Add("@AadharCardNo", AadharCardNo);
                ModifyEmployeeDetails.Add("@AadharCardName", AadharCardName);
                ModifyEmployeeDetails.Add("@drivingLicenseNo", drivingLicenseNo);
                ModifyEmployeeDetails.Add("@drivingLicenseName", drivingLicenseName);
                ModifyEmployeeDetails.Add("@VoterIDNo", VoterIDNo);
                ModifyEmployeeDetails.Add("@VoterIDName", VoterIDName);
                ModifyEmployeeDetails.Add("@RationCardNo", RationCardNo);
                ModifyEmployeeDetails.Add("@RationCardName", RationCardName);
                ModifyEmployeeDetails.Add("@PanCardNo", PanCardNo);
                ModifyEmployeeDetails.Add("@PanCardName", PanCardName);
                ModifyEmployeeDetails.Add("@BankPassbookNo", BankPassbookNo);
                ModifyEmployeeDetails.Add("@BankPassbookName", BankPassbookName);
                ModifyEmployeeDetails.Add("@ElectricityBillNo", ElectricityBillNo);
                ModifyEmployeeDetails.Add("@ElectricityBillName", ElectricityBillName);
                ModifyEmployeeDetails.Add("@Othertext", Othertext);
                ModifyEmployeeDetails.Add("@Othertextname", Othertextname);
                ModifyEmployeeDetails.Add("@ESICCard ", ESICCard);
                ModifyEmployeeDetails.Add("@ESICCardNo ", ESICCardNo);
                ModifyEmployeeDetails.Add("@ESICName ", ESICName);
                ModifyEmployeeDetails.Add("@SSCNo", SSCCertificateNo);
                ModifyEmployeeDetails.Add("@SSC", SSCCertificate);
                ModifyEmployeeDetails.Add("@SSCStatus", SSCStatus);
                ModifyEmployeeDetails.Add("@SSCCopyStatus", SSCCopyStatus);
                ModifyEmployeeDetails.Add("@AadharCardImg", AadhaImage);


                ModifyEmployeeDetails.Add("@Aadharstatus ", aadharcardstatus);
                ModifyEmployeeDetails.Add("@Drivingstatus ", Drivingstatus);
                ModifyEmployeeDetails.Add("@VoterIdstatus ", Voterstatus);
                ModifyEmployeeDetails.Add("@Rationstatus ", Rationcardstatus);
                ModifyEmployeeDetails.Add("@PanCardstatus ", Pancardstatus);
                ModifyEmployeeDetails.Add("@Passbookstatus ", Bankpassbookstatus);
                ModifyEmployeeDetails.Add("@Othersstatus ", Othersstatus);
                ModifyEmployeeDetails.Add("@Electricitystatus ", Electricitystatus);
                ModifyEmployeeDetails.Add("@ESICstatus ", ESICstatus);


                ModifyEmployeeDetails.Add("@AadharCopystatus ", aadharcardCopystatus);
                ModifyEmployeeDetails.Add("@DrivingCopystatus ", DrivingCopystatus);
                ModifyEmployeeDetails.Add("@VoterIdCopystatus ", VoterCopystatus);
                ModifyEmployeeDetails.Add("@RationCopystatus ", RationcardCopystatus);
                ModifyEmployeeDetails.Add("@PanCardCopystatus ", PancardCopystatus);
                ModifyEmployeeDetails.Add("@PassbookCopystatus ", BankpassbookCopystatus);
                ModifyEmployeeDetails.Add("@OthersCopystatus ", OthersCopystatus);
                ModifyEmployeeDetails.Add("@ElectricityCopystatus ", ElectricityCopystatus);
                ModifyEmployeeDetails.Add("@ESICCopystatus ", ESICCopystatus);



                ModifyEmployeeDetails.Add("@AadharCard", AadharCard);
                ModifyEmployeeDetails.Add("@drivingLicense", drivingLicense);
                ModifyEmployeeDetails.Add("@VoterID", VoterID);
                ModifyEmployeeDetails.Add("@RationCard", RationCard);
                ModifyEmployeeDetails.Add("@PanCard", PanCard);
                ModifyEmployeeDetails.Add("@BankPassbook", BankPassbook);
                ModifyEmployeeDetails.Add("@ElectricityBill", ElectricityBill);
                ModifyEmployeeDetails.Add("@Other", Other);

                #endregion for Proofs Submitted



                #endregion passing parameters to stored procedure
                #endregion End Code For Stored Procedure Parameters

                #region   Begin Code For Calling Stored Procedure
                URecordStatus = config.ExecuteNonQueryParamsAsync(AddEmpPersonalDetails, ModifyEmployeeDetails).Result;
                #endregion End   Code For Calling Stored Procedure

                #region   Begin Code For Resulted Messages as on [19-09-2013]
                string ExactEmpid = string.Empty;
                ExactEmpid = txtEmpid.Text;
                if (URecordStatus > 0)
                {
                    modifyfamilydetails();
                    modifyeducationdetails();
                    modifyPreviousExperience();
                    lblMsg.Text = "Employee Details Modified Sucessfully With ID NO  :- " + ExactEmpid + "  -:";
                    LoadPersonalInfo(ExactEmpid);
                }
                else
                {
                    lblMsg.Text = "Employee Details Not Modified Sucessfully With ID NO  :- " + ExactEmpid + "  -:  , Please Check The Details.";
                }


                Btn_Save_Personal_Tab.Visible = false;
                Btn_Cancel_Personal_Tab.Visible = false;
                btnNext.Visible = false;

                PnlEmployeeInfo.Enabled = false;
                Pnlpersonal.Enabled = false;
                pnlimages.Enabled = false;
                pnlphysicalstandard2.Enabled = false;
                pnlphysicalstandard.Enabled = false;
                PnlPFDetails.Enabled = false;
                PnlBankDetails.Enabled = false;
                PnlESIDetails.Enabled = false;
                PnlSalaryDetails.Enabled = false;
                PnlProofsSubmitted.Enabled = false;
                PnlExService.Enabled = false;
                pnlfamilydetails.Enabled = false;
                pnlEducationDetails.Enabled = false;
                pnlPreviousExpereince.Enabled = false;
                PnlPaySheet.Enabled = false;
                pnlGroupBox.Enabled = false;
                PnlCriminalProceeding.Enabled = false;

                Btnedit.Visible = true;
                #endregion  End Code For Resulted Messages as on [19-09-2013]


                FameService fs = new FameService();
                fs.UpdateEmpDataTable();


            }
            catch (Exception ex)
            {
                lblMsg.Text = "Your Session Expired. Please Login";
            }
        }

        protected void Btn_Cancel_Personal_Tab_Click(object sender, EventArgs e)  //modify personal Cancel button
        {
            ClearDataFromPersonalInfoTabFields();
        }

        protected void ClearDataFromPersonalInfoTabFields()
        {
            txtEmpDtofInterview.Text = txtEmpDtofJoining.Text = txtEmpDtofBirth.Text = txtDofleaving.Text = "";
            txtEmpFName.Text = txtEmpmiName.Text = txtEmplname.Text = txtShiftstarttime.Text = txtShiftEndtime.Text =
            txtQualification.Text = txtPreEmp.Text = txtfatheroccupation.Text = txtbirthplace.Text =
            // txtEmpFatherName.Text = txtFaocccu.Text = txtFaSpRelation.Text = txtFAge.Text =
            //txtmname.Text = txtmoccupation.Text = 
            txtmtongue.Text = txtPhone.Text = txtnationality.Text = txtreligion.Text = txtLangKnown.Text = txtREfAddr1.Text = txtREfAddr2.Text = txtPhyRem.Text = txtEmpRemarks.Text = txtImark1.Text = txtImark2.Text =
            txtheight.Text = txtweight.Text = txtcheexpan.Text = txtcheunexpan.Text = txtPhone.Text = txtFamDetails.Text = txoldempid.Text = txtref2mail.Text = txtref1mail.Text =
            //txtPrdoor.Text = txtstreet.Text = txtlmark.Text =
            //txtarea.Text = txtcity.Text = txtdistrictt.Text = txtpin.Text = txtstate.Text = 
            // txtdoor1.Text = txtstreet2.Text = txtlmark3.Text = txtarea4.Text = txtcity5.Text = txtPDist.Text = txtpin7.Text = txtstate8.Text =
            txtmobile.Text = txtmobile9.Text = txtNomDoB.Text = txtPFEnrollDate.Text = "";
            txtBankAccNum.Text = txtbranchname.Text = txtIFSCcode.Text = txtBranchCode.Text = txtBankCodenum.Text = txtBankAppNum.Text = txtRegCode.Text =
            txtEmpInsNominee.Text = txtBankCardRef.Text = txtUANNumber.Text = txtInsDeb.Text = txtEmpNomRel.Text =
            txtEmpPFNumber.Text = txtPFNominee.Text = txtCmpShortName.Text = txtPFNomineeRel.Text = txtESINum.Text = txtESINominee.Text =
            txtESIDiSName.Text = txtaadhaar.Text = txtESINomRel.Text = txtInsCover.Text = txtEmergContNo.Text = string.Empty; txtForm.Text = string.Empty;
            //txtschool.Text = txtbrd.Text = txtyear.Text = txtpsfi.Text =
            //txtpmarks.Text = txtimschool.Text = txtimbrd.Text = txtimyear.Text = txtimpsfi.Text = txtimpmarks.Text = txtdgschool.Text = txtdgbrd.Text =
            //txtdgyear.Text = txtdgpsfi.Text = txtdgpmarks.Text = txtpgschool.Text = txtpgbrd.Text = txtpgyear.Text = txtpgpsfi.Text = txtpgpmarks.Text = 
            Rdb_Male.Checked = Rdb_Female.Checked = rdbsingle.Checked = rdbmarried.Checked = false;
            txtServiceNum.Text = txtRank.Text = txtCrops.Text = txtTrade.Text = txtMCategory.Text = TxtROfDischarge.Text = txtConduct.Text = string.Empty;
            txtDOfEnroll.Text = "";
            //txtDOFDischarge.Text = "";
            ddlDesignation.SelectedIndex = 0;
            ddlBloodGroup.SelectedIndex = 0;
            ddlbankname.SelectedIndex = 0;
            ddlShift.SelectedIndex = DdlPreferedUnit.SelectedIndex = ddlWoff1.SelectedIndex = ddlWoff2.SelectedIndex = 0;
            ChkESIDed.Checked = ChkPFDed.Checked = ChkExService.Checked = ChkPTDed.Checked = false;
            rdur.Checked = true;

        }

        protected void ClearAllControlsDataFromThePage()
        {
            ClearDataFromPersonalInfoTabFields();


        }

        public void modifyfamilydetails()
        {
            string age = "0";
            string SqlDelete = "Delete EmpRelationships where EmpId='" + txtEmpid.Text + "'";
            int del = config.ExecuteNonQueryWithQueryAsync(SqlDelete).Result;
            foreach (GridViewRow dr in gvFamilyDetails.Rows)
            {
                TextBox txtEmpRName = dr.FindControl("txtEmpName") as TextBox;
                DropDownList ddlRelationtype = dr.FindControl("ddlRelation") as DropDownList;
                TextBox txtDOFBirth = dr.FindControl("txtRelDtofBirth") as TextBox;
                TextBox txtAge = dr.FindControl("txtAge") as TextBox;
                TextBox txtoccupation = dr.FindControl("txtReloccupation") as TextBox;
                TextBox txtrelphone = dr.FindControl("txtRelphnno") as TextBox;
                TextBox txtrelmail = dr.FindControl("txtRelmail") as TextBox;
                DropDownList ddlrelresidence = dr.FindControl("ddlresidence") as DropDownList;
                TextBox txtRelplace = dr.FindControl("txtplace") as TextBox;
                CheckBox chkpfnominee = dr.FindControl("ChkPFNominee") as CheckBox;
                CheckBox chkesinominee = dr.FindControl("ChkESINominee") as CheckBox;
                string DOFBirth = "";
                if (txtEmpRName.Text != string.Empty || ddlRelationtype.SelectedIndex > 0 || txtoccupation.Text != string.Empty || txtRelplace.Text != string.Empty || ddlrelresidence.SelectedIndex > 0)
                {
                    var testDate = 0;
                    #region Begin Validating Date Format

                    if (txtDOFBirth.Text.Trim().Length > 0)
                    {
                        testDate = GlobalData.Instance.CheckEnteredDate(txtDOFBirth.Text);
                        if (testDate > 0)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('You Are Entered Invalid Date Of Birth Date.Date Format Should be [DD/MM/YYYY].Ex.01/01/1990');", true);
                            return;
                        }
                    }
                    #endregion End Validating Date Format


                    if (txtDOFBirth.Text.Trim().Length != 0)
                    {
                        DOFBirth = Timings.Instance.CheckDateFormat(txtDOFBirth.Text);
                    }
                    else
                    {
                        DOFBirth = "01/01/1900";
                    }



                    // #region Begin Getmax Id from DB
                    int RelationId = 0;
                    string selectquerycomppanyid = "select max(cast(Id as int )) as Id from EmpRelationships where EmpId='" + txtEmpid.Text + "'";
                    DataTable dt = config.ExecuteAdaptorAsyncWithQueryParams(selectquerycomppanyid).Result;

                    if (dt.Rows.Count > 0)
                    {
                        if (String.IsNullOrEmpty(dt.Rows[0]["Id"].ToString()) == false)
                        {
                            RelationId = Convert.ToInt32(dt.Rows[0]["Id"].ToString()) + 1;
                        }
                        else
                        {
                            RelationId = int.Parse("1");
                        }
                    }


                    if (txtAge.Text.Trim().Length > 0)
                    {
                        age = txtAge.Text;
                    }
                    else
                    {
                        age = "0";
                    }

                    string Occupation = "";
                    if (txtoccupation.Text.Length == 0)
                    {
                        Occupation = "";
                    }
                    else
                    {
                        Occupation = txtoccupation.Text;
                    }
                    string relphone = "";
                    if (txtrelphone.Text.Length == 0)
                    {
                        relphone = "";
                    }
                    else
                    {
                        relphone = txtrelphone.Text;
                    }
                    string relmail = "";
                    if (txtrelmail.Text.Length == 0)
                    {
                        relmail = "";
                    }
                    else
                    {
                        relmail = txtrelmail.Text;
                    }

                    string Relplace = "";
                    if (txtRelplace.Text.Length == 0)
                    {
                        Relplace = "";
                    }
                    else
                    {
                        Relplace = txtRelplace.Text;
                    }

                    string relationtype = "";
                    if (ddlRelationtype.SelectedIndex == 0)
                    {
                        relationtype = string.Empty;
                    }
                    if (ddlRelationtype.SelectedIndex > 0)
                    {
                        relationtype = ddlRelationtype.SelectedValue;
                    }
                    string relationresidence = "";
                    if (ddlrelresidence.SelectedIndex == 0)
                    {
                        relationresidence = string.Empty;
                    }
                    if (ddlrelresidence.SelectedIndex > 0)
                    {
                        relationresidence = ddlrelresidence.SelectedValue;
                    }

                    string pfnominee = "N"; string esinominee = "N";
                    if (chkpfnominee.Checked)
                    {
                        pfnominee = "Y";
                    }
                    else
                    {
                        pfnominee = "N";
                    }

                    if (chkesinominee.Checked)
                    {
                        esinominee = "Y";
                    }
                    else
                    {
                        esinominee = "N";
                    }

                    string linksave = "insert into EmpRelationships values('" + txtEmpid.Text + "','" + txtEmpRName.Text + "','" + relationtype + "','" + DOFBirth + "','" + RelationId + "','" + age + "','" + Occupation + "','" + relationresidence + "','" + Relplace + "','" + pfnominee + "','" + esinominee + "','" + relphone + "','" + relmail + "')";
                    int Getbyresult = config.ExecuteNonQueryWithQueryAsync(linksave).Result;
                }
            }
        }

        public void modifyPreviousExperience()
        {

            int Getbyresult = 0; string RegionCode = ""; string EmpCode = ""; string Extension = ""; string DateofResign = ""; string Designation = ""; string CompAddress = ""; string yearofExp = ""; string PFNo = " "; string ESINo = "";
            string SqlDelete = "Delete EmpPrevExperience where EmpId='" + txtEmpid.Text + "'";
            int des = config.ExecuteNonQueryWithQueryAsync(SqlDelete).Result;
            foreach (GridViewRow dr in GvPreviousExperience.Rows)
            {
                TextBox txtregioncode = dr.FindControl("txtregioncode") as TextBox;
                TextBox txtempcode = dr.FindControl("txtempcode") as TextBox;
                TextBox txtExtension = dr.FindControl("txtExtension") as TextBox;
                TextBox txtPrevDesignation = dr.FindControl("txtPrevDesignation") as TextBox;
                TextBox txtCompAddress = dr.FindControl("txtCompAddress") as TextBox;
                TextBox txtyearofexp = dr.FindControl("txtyearofexp") as TextBox;
                TextBox txtPFNo = dr.FindControl("txtPFNo") as TextBox;
                TextBox txtESINo = dr.FindControl("txtESINo") as TextBox;
                TextBox txtDtofResigned = dr.FindControl("txtDtofResigned") as TextBox;


                if (txtregioncode.Text != string.Empty || txtDtofResigned.Text != string.Empty || txtempcode.Text != string.Empty || txtPrevDesignation.Text != string.Empty || txtCompAddress.Text != string.Empty || txtyearofexp.Text != string.Empty || txtPFNo.Text != string.Empty || txtESINo.Text != string.Empty)
                {

                    var testDate = 0;
                    #region Begin Validating Date Format

                    if (txtDtofResigned.Text.Trim().Length > 0)
                    {
                        testDate = GlobalData.Instance.CheckEnteredDate(txtDtofResigned.Text);
                        if (testDate > 0)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('You Are Entered Invalid Date Of Birth Date.Date Format Should be [DD/MM/YYYY].Ex.01/01/1990');", true);
                            return;
                        }
                    }
                    #endregion End Validating Date Format

                    if (txtDtofResigned.Text.Trim().Length != 0)
                    {
                        DateofResign = Timings.Instance.CheckDateFormat(txtDtofResigned.Text);
                    }
                    else
                    {
                        DateofResign = "01/01/1900";
                    }


                    #region Begin Getmax Id from DB
                    int PrevExpId = 0;
                    string selectquery = "select max(cast(Id as int )) as Id from EmpPrevExperience where EmpId='" + txtEmpid.Text + "'";
                    DataTable dt = config.ExecuteAdaptorAsyncWithQueryParams(selectquery).Result;

                    if (dt.Rows.Count > 0)
                    {
                        if (String.IsNullOrEmpty(dt.Rows[0]["Id"].ToString()) == false)
                        {
                            PrevExpId = Convert.ToInt32(dt.Rows[0]["Id"].ToString()) + 1;
                        }
                        else
                        {
                            PrevExpId = int.Parse("1");
                        }
                    }
                    #endregion End Getmax Id from DB

                    if (txtregioncode.Text.Length == 0)
                    {
                        RegionCode = "";
                    }
                    else
                    {
                        RegionCode = txtregioncode.Text;
                    }

                    if (txtempcode.Text.Length == 0)
                    {
                        EmpCode = "";
                    }
                    else
                    {
                        EmpCode = txtempcode.Text;
                    }

                    if (txtExtension.Text.Length == 0)
                    {
                        Extension = "";
                    }
                    else
                    {
                        Extension = txtExtension.Text;
                    }

                    if (txtPrevDesignation.Text.Trim().Length > 0)
                    {
                        Designation = txtPrevDesignation.Text;
                    }
                    else
                    {
                        Designation = " ";
                    }
                    if (txtCompAddress.Text.Length == 0)
                    {
                        CompAddress = "";
                    }
                    else
                    {
                        CompAddress = txtCompAddress.Text;
                    }
                    if (txtyearofexp.Text.Length == 0)
                    {
                        yearofExp = "";
                    }
                    else
                    {
                        yearofExp = txtyearofexp.Text;
                    }
                    if (txtPFNo.Text.Length == 0)
                    {
                        PFNo = "";
                    }
                    else
                    {
                        PFNo = txtPFNo.Text;
                    }
                    if (txtESINo.Text.Length == 0)
                    {
                        ESINo = "";
                    }
                    else
                    {
                        ESINo = txtESINo.Text;
                    }



                    string linksave = "insert into EmpPrevExperience (Empid,RegionCode,EmployerCode,Extension,Designation,CompAddress,YrOfExp,PFNo,ESINo,DateofResign,id) values('" + txtEmpid.Text + "','" + RegionCode + "','" + EmpCode + "','" + Extension + "','" + Designation + "','" + CompAddress + "','" + yearofExp + "','" + PFNo + "','" + ESINo + "','" + DateofResign + "','" + PrevExpId + "')";
                    Getbyresult = config.ExecuteNonQueryWithQueryAsync(linksave).Result;



                }

            }

        }

        public void modifyeducationdetails()
        {

            int Getbyresult = 0; string Qualification = ""; string Description = ""; string NameOfschoolClg = ""; string Board = ""; string year = ""; string PassFail = " "; string Percentage = "";

            string SqlDelete = "Delete EmpEducationDetails where EmpId='" + txtEmpid.Text + "'";
            int deq = config.ExecuteNonQueryWithQueryAsync(SqlDelete).Result;
            foreach (GridViewRow dr in GvEducationDetails.Rows)
            {
                DropDownList ddlQualification = dr.FindControl("ddlQualification") as DropDownList;
                TextBox txtQualification = dr.FindControl("txtEdLevel") as TextBox;
                TextBox txtNameofSchoolColg = dr.FindControl("txtNameofSchoolColg") as TextBox;
                TextBox txtBoard = dr.FindControl("txtBoard") as TextBox;
                TextBox txtyear = dr.FindControl("txtyear") as TextBox;
                TextBox txtPassFail = dr.FindControl("txtPassFail") as TextBox;
                TextBox txtPercentage = dr.FindControl("txtPercentage") as TextBox;

                if (txtQualification.Text != string.Empty || txtNameofSchoolColg.Text != string.Empty || txtBoard.Text != string.Empty || txtyear.Text != string.Empty || txtPassFail.Text != string.Empty || txtPercentage.Text != string.Empty || ddlQualification.SelectedIndex > 0)
                {

                    #region Begin Getmax Id from DB
                    int EduId = 0;
                    string selectquery = "select max(cast(Id as int )) as Id from EmpEducationDetails where EmpId='" + txtEmpid.Text + "'";
                    DataTable dt = config.ExecuteAdaptorAsyncWithQueryParams(selectquery).Result;

                    if (dt.Rows.Count > 0)
                    {
                        if (String.IsNullOrEmpty(dt.Rows[0]["Id"].ToString()) == false)
                        {
                            EduId = Convert.ToInt32(dt.Rows[0]["Id"].ToString()) + 1;
                        }
                        else
                        {
                            EduId = int.Parse("1");
                        }
                    }
                    #endregion End Getmax Id from DB

                    if (ddlQualification.SelectedIndex > 0)
                    {
                        Qualification = ddlQualification.SelectedValue;
                    }
                    else
                    {
                        Qualification = "0";
                    }

                    if (txtQualification.Text.Length == 0)
                    {
                        Description = "";
                    }
                    else
                    {
                        Description = txtQualification.Text;
                    }
                    if (txtNameofSchoolColg.Text.Trim().Length > 0)
                    {
                        NameOfschoolClg = txtNameofSchoolColg.Text;
                    }
                    else
                    {
                        NameOfschoolClg = " ";
                    }
                    if (txtBoard.Text.Length == 0)
                    {
                        Board = "";
                    }
                    else
                    {
                        Board = txtBoard.Text;
                    }
                    if (txtyear.Text.Length == 0)
                    {
                        year = "";
                    }
                    else
                    {
                        year = txtyear.Text;
                    }
                    if (txtPercentage.Text.Length == 0)
                    {
                        Percentage = "";
                    }
                    else
                    {
                        Percentage = txtPercentage.Text;
                    }
                    if (txtPassFail.Text.Length == 0)
                    {
                        PassFail = "";
                    }
                    else
                    {
                        PassFail = txtPassFail.Text;
                    }



                    string linksave = "insert into EmpEducationDetails (Empid,Qualification,Description,NameOfSchoolClg,BoardorUniversity,YrOfStudy,PassOrFail,PercentageOfmarks,id) values('" + txtEmpid.Text + "','" + Qualification + "','" + Description + "','" + NameOfschoolClg + "','" + Board + "','" + year + "','" + PassFail + "','" + Percentage + "','" + EduId + "')";
                    Getbyresult = config.ExecuteNonQueryWithQueryAsync(linksave).Result;



                }

            }

        }

        //protected void ddlbirthstate_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    string query = "select CityId,City from cities where state='" + ddlbirthstate.SelectedValue + "' order by City";
        //    DataTable dt = SqlHelper.Instance.GetTableByQuery(query);
        //    if (dt.Rows.Count > 0)
        //    {
        //        ddlBirthDistrict.Enabled = true;
        //        ddlBirthDistrict.Enabled = true;
        //        ddlBirthDistrict.DataValueField = "CityId";
        //        ddlBirthDistrict.DataTextField = "City";
        //        ddlBirthDistrict.DataSource = dt;
        //        ddlBirthDistrict.DataBind();

        //    }

        //    ddlBirthDistrict.Items.Insert(0, new ListItem("--Select--", "0"));

        //}




        //load personalinforamation and qualification tab
        protected void LoadPersonalInfo(string empid)
        {
            try
            {


                string EmpIDPrefix = GlobalData.Instance.GetEmployeeIDPrefix();

                string query = "select Empstatus,EmpId,Title,EmpFName,EmpMName,EmpLName,EmpSex,EmpMaritalStatus,EmpDtofBirth,EmpQualification,EmpDesgn,EmpFatherName,EmpFatherOccupation,EmpSpouseName,EmpMotherName,EmpDtofInterview,EmpDtofJoining," +
                    "EmpDtofLeaving,EmpPhone,EmpLanguagesKnown,MotherTongue,isnull(Formno,0) as FormNo,isnull(placeofbirth,'') as BirthPlace,Nationality,Religion,EmpPreviousExp,community,PsaraEmpCode,EmailId,IDCardIssued,DocImageType, " +
                    "IDCardValid,UnitId,Branch,Department,Division,ReportingManager,Gross,EmpExservice,EmpPFDeduct,EmpESIDeduct,EmpPTDeduct,Image," +
                    "BirthVillage,C1.City as BirthDistrict,BirthState,BirthCountry,EmpRefAddr1, EmpRefAddr2,isnull(refphnno1,'') as RefPhnno1,isnull(refphnno2,'') as RefPhnno2,isnull(refgmail1,'') as refgmail1,isnull(refgmail2,'') as refgmail2,isnull(EmpBloodGroup,0) as EmpBloodGroup,EmpRemarks,EmpPhysicalRemarks,EmpIdMark1,EmpIdMark2," +
                    "EmpHeight,EmpWeight,EmpChestExp,EmpChestunex,EmpEyesColor,EmpHairColor,SpeciallyAbled,ApplicantCategory,prPoliceStation,prTown,prTaluka,pePoliceStation,peTown,peTaluka," +
                    "prState,C2.City as prCity,prphone,EmpPresentAddress,peState,C.City as peCity,pephone,prPostoffice,prLmark,prPincode,pePostoffice,peLmark,pepincode, empbankname,empbankacno,Empbankbranchname,empifsccode,empbranchcode,empbankcode," +
                    "empbankappno,empregioncode,empinsnominee,empbankcardref,empnomineedtofbirth,empnomineerel,empinscover,empinsdedamt,empUANnumber,aadhaarid, AddlAmount," +
                    " FoodAllowance,isnull(Oldempid,'') as Oldempid,prResidingDate,prperiodofstay,ResidingDate,periodofstay,EmployeeType,EmpSign,isnull(EmergencyContactNo,'') as EmergencyContactNo ,Shift,ShiftStartTime,ShiftEndTime,isnull(Woff1,0)Woff1,isnull(Woff2,0)Woff2 from EmpDetails " +
                    " left join Cities C on C.cityID=EmpDetails.pecity " +
                    " left join Cities C1 on C1.cityID=EmpDetails.BirthDistrict " +
                    " left join Cities C2 on C2.cityID=EmpDetails.prCity " +
                    " where empid='" + empid + "'";
                DataTable dt = config.ExecuteAdaptorAsyncWithQueryParams(query).Result;

                bool c = false;
                if (String.IsNullOrEmpty(dt.Rows[0]["EmpStatus"].ToString()) == false)
                {
                    c = Convert.ToBoolean(dt.Rows[0]["EmpStatus"].ToString());
                }
                if (c == true)
                {
                    rdbactive.Checked = true;

                }
                else
                {
                    rdbResigned.Checked = true;

                }
                txtEmpid.Text = dt.Rows[0]["Empid"].ToString();
                txtmodifyempid.Text = " <i> Emp ID/Name: <b>" + dt.Rows[0]["Empid"].ToString() + " - " + dt.Rows[0]["EmpFName"].ToString() + " " + dt.Rows[0]["EmpMName"].ToString() + " " + dt.Rows[0]["EmpLName"].ToString() + "</b></i>";

                string Employeetype = dt.Rows[0]["Employeetype"].ToString();
                if (Employeetype == "G")
                {
                    rdbGeneral.Checked = true;
                }
                else
                {
                    rdbStaff.Checked = true;
                }
                ddlTitle.SelectedValue = dt.Rows[0]["Title"].ToString();
                txtEmpFName.Text = dt.Rows[0]["EmpFName"].ToString();
                txtEmpmiName.Text = dt.Rows[0]["EmpMName"].ToString();
                txtEmplname.Text = dt.Rows[0]["EmpLName"].ToString();
                string MaritalStatus = dt.Rows[0]["EmpMaritalStatus"].ToString();
                if (MaritalStatus == "M")
                {
                    rdbmarried.Checked = true;
                }
                else if (MaritalStatus == "S")
                {
                    rdbsingle.Checked = true;
                }
                else if (MaritalStatus == "W")
                {
                    rdbWidower.Checked = true;
                }
                else
                {
                    rdbdivorcee.Checked = true;
                }
                #region Shift Details
                ddlShift.SelectedValue = dt.Rows[0]["Shift"].ToString();
                txtShiftstarttime.Text = dt.Rows[0]["ShiftStartTime"].ToString();
                txtShiftEndtime.Text = dt.Rows[0]["ShiftEndTime"].ToString();
                ddlWoff1.SelectedIndex = int.Parse(dt.Rows[0]["Woff1"].ToString());
                ddlWoff2.SelectedIndex = int.Parse(dt.Rows[0]["Woff2"].ToString());
                #endregion
                txtQualification.Text = dt.Rows[0]["EmpQualification"].ToString();
                txtPhone.Text = dt.Rows[0]["EmpPhone"].ToString();
                txoldempid.Text = dt.Rows[0]["Oldempid"].ToString();
                txtmtongue.Text = dt.Rows[0]["MotherTongue"].ToString();
                txtnationality.Text = dt.Rows[0]["Nationality"].ToString();
                txtreligion.Text = dt.Rows[0]["Religion"].ToString();
                txtLangKnown.Text = dt.Rows[0]["EmpLanguagesKnown"].ToString();
                txtemail.Text = dt.Rows[0]["Emailid"].ToString();
                txtpsaraempcode.Text = dt.Rows[0]["psaraempcode"].ToString();
                txtSpousName.Text = dt.Rows[0]["EmpSpouseName"].ToString();
                txtMotherName.Text = dt.Rows[0]["EmpMotherName"].ToString();
                txtFatherName.Text = dt.Rows[0]["EmpFatherName"].ToString();
                txtfatheroccupation.Text = dt.Rows[0]["EmpFatherOccupation"].ToString();
                txtEmergContNo.Text = dt.Rows[0]["EmergencyContactNo"].ToString();
                txtForm.Text = dt.Rows[0]["FormNo"].ToString();
                txtbirthplace.Text = dt.Rows[0]["BirthPlace"].ToString();

                if (dt.Rows[0]["Division"].ToString() == "0")
                {
                    ddlDivision.SelectedIndex = 0;
                }
                else
                {
                    ddlDivision.SelectedValue = dt.Rows[0]["Division"].ToString();
                }

                if (dt.Rows[0]["Department"].ToString() == "0")
                {
                    ddldepartment.SelectedIndex = 0;
                }
                else
                {
                    ddldepartment.SelectedValue = dt.Rows[0]["Department"].ToString();
                }

                if (dt.Rows[0]["ReportingManager"].ToString() == "0")
                {
                    ddlReportingMgr.SelectedIndex = 0;
                }
                else
                {
                    ddlReportingMgr.SelectedValue = dt.Rows[0]["ReportingManager"].ToString();
                }

                if (dt.Rows[0]["Branch"].ToString() == "0")
                {
                    ddlBranch.SelectedIndex = 0;
                }
                else
                {
                    ddlBranch.SelectedValue = dt.Rows[0]["Branch"].ToString();
                }

                txtGrossSalary.Text = dt.Rows[0]["gross"].ToString();

                if (dt.Rows[0]["EmpDesgn"].ToString() == "0")
                {
                    ddlDesignation.SelectedIndex = 0;
                }
                else
                {
                    ddlDesignation.SelectedValue = dt.Rows[0]["EmpDesgn"].ToString();
                }

                txtQualification.Text = dt.Rows[0]["EmpQualification"].ToString();
                txtPreEmp.Text = dt.Rows[0]["EmpPreviousExp"].ToString();

                string Empsex = dt.Rows[0]["EmpSex"].ToString();
                if (Empsex == "M")
                {
                    Rdb_Male.Checked = true;
                }
                else if (Empsex == "F")
                {
                    Rdb_Female.Checked = true;
                }
                else
                {
                    rdbTransgender.Checked = true;
                }
                bool ex = false;
                if (String.IsNullOrEmpty(dt.Rows[0]["EmpExservice"].ToString()) == false)
                {
                    ex = Convert.ToBoolean(dt.Rows[0]["EmpExservice"].ToString());
                }
                if (ex == true)
                    ChkExService.Checked = true;
                else
                    ChkExService.Checked = false;

                bool pf = false;
                if (String.IsNullOrEmpty(dt.Rows[0]["EmpPFDeduct"].ToString()) == false)
                {
                    pf = Convert.ToBoolean(dt.Rows[0]["EmpPFDeduct"].ToString());
                }
                if (pf == true)
                    ChkPFDed.Checked = true;
                else
                    ChkPFDed.Checked = false;

                bool ESI = false;
                if (String.IsNullOrEmpty(dt.Rows[0]["EmpESIDeduct"].ToString()) == false)
                {
                    ESI = Convert.ToBoolean(dt.Rows[0]["EmpESIDeduct"].ToString());
                }
                if (ESI == true)
                    ChkESIDed.Checked = true;
                else
                    ChkESIDed.Checked = false;


                bool PT = false;
                if (String.IsNullOrEmpty(dt.Rows[0]["EmpPTDeduct"].ToString()) == false)
                {
                    PT = Convert.ToBoolean(dt.Rows[0]["EmpPTDeduct"].ToString());
                }
                if (PT == true)
                    ChkPTDed.Checked = true;
                else
                    ChkPTDed.Checked = false;


                DdlPreferedUnit.SelectedValue = dt.Rows[0]["Unitid"].ToString();
                txtLangKnown.Text = dt.Rows[0]["EmpLanguagesKnown"].ToString();
                if (String.IsNullOrEmpty(dt.Rows[0]["EmpDtofLeaving"].ToString()) == false)
                {

                    txtDofleaving.Text = DateTime.Parse(dt.Rows[0]["EmpDtofLeaving"].ToString()).ToString("dd/MM/yyyy");
                    if (txtDofleaving.Text == "01/01/1900")
                    {
                        txtDofleaving.Text = "";
                    }
                }
                else
                {
                    txtDofleaving.Text = "";

                }


                if (String.IsNullOrEmpty(dt.Rows[0]["EmpDtofInterview"].ToString()) == false)
                {

                    txtEmpDtofInterview.Text = DateTime.Parse(dt.Rows[0]["EmpDtofInterview"].ToString()).ToString("dd/MM/yyyy");
                    if (txtEmpDtofInterview.Text == "01/01/1900")
                    {
                        txtEmpDtofInterview.Text = "";
                    }

                }
                else
                {
                    txtEmpDtofInterview.Text = "";

                }

                if (String.IsNullOrEmpty(dt.Rows[0]["EmpDtofJoining"].ToString()) == false)
                {
                    txtEmpDtofJoining.Text = DateTime.Parse(dt.Rows[0]["EmpDtofJoining"].ToString()).ToString("dd/MM/yyyy");
                    if (txtEmpDtofJoining.Text == "01/01/1900")
                    {
                        txtEmpDtofJoining.Text = "";
                    }
                }
                else
                {
                    //txtEmpDtofJoining.Text = "01/01/1900";
                    txtEmpDtofJoining.Text = "";
                }
                if (String.IsNullOrEmpty(dt.Rows[0]["EmpDtofBirth"].ToString()) == false)
                {

                    txtEmpDtofBirth.Text = DateTime.Parse(dt.Rows[0]["EmpDtofBirth"].ToString()).ToString("dd/MM/yyyy");
                    if (txtEmpDtofBirth.Text == "01/01/1900")
                    {
                        txtEmpDtofBirth.Text = "";
                    }

                }
                else
                {
                    txtEmpDtofBirth.Text = "";
                }

                if (String.IsNullOrEmpty(dt.Rows[0]["IDCardIssued"].ToString()) == false)
                {

                    TxtIDCardIssuedDt.Text = DateTime.Parse(dt.Rows[0]["IDCardIssued"].ToString()).ToString("dd/MM/yyyy");
                    if (TxtIDCardIssuedDt.Text == "01/01/1900")
                    {
                        TxtIDCardIssuedDt.Text = "";
                    }

                }
                else
                {
                    TxtIDCardIssuedDt.Text = "";

                }

                if (String.IsNullOrEmpty(dt.Rows[0]["IDCardValid"].ToString()) == false)
                {

                    TxtIdCardValid.Text = DateTime.Parse(dt.Rows[0]["IDCardValid"].ToString()).ToString("dd/MM/yyyy");
                    if (TxtIdCardValid.Text == "01/01/1900")
                    {
                        TxtIdCardValid.Text = "";
                    }

                }
                else
                {
                    TxtIdCardValid.Text = "";

                }


                if (dt.Rows[0]["Image"].ToString() != "")
                {
                    Image1.ImageUrl = ("/assets/EmpPhotos/") + dt.Rows[0]["Image"].ToString();
                }

                //if (dt.Rows[0]["EmpSign"].ToString() != "")
                //{
                //    Image2.ImageUrl = ("/assets/Empsign/") + dt.Rows[0]["EmpSign"].ToString();
                //}

                if (dt.Rows[0]["EmpSign"].ToString().Length > 0)
                {
                    if (dt.Rows[0]["DocImageType"].ToString() == ".pdf")
                    {
                        string URL = ("/assets/EmpPhotos/") + dt.Rows[0]["EmpSign"].ToString();
                        string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"200px\" height=\"150px\">";
                        embed += "If you are unable to view file, you can download from <a href = \"{0}\">here</a>";
                        embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
                        embed += "</object>";
                        ltbankpassbookfront.Text = string.Format(embed, ResolveUrl(URL));
                        ImgPassBookFront.ImageUrl = ("/assets/EmpPhotos/") + dt.Rows[0]["EmpSign"].ToString();
                    }
                    else
                    {
                        string URL = ("/assets/EmpPhotos/") + dt.Rows[0]["EmpSign"].ToString();
                        ltbankpassbookfront.Text = "<img id='Image3' src='" + URL + "' alt=' ' height='100' width='100' />";
                        //AadharFrontImg.ImageUrl = ("/assets/EmpPhotos/") + dtpd.Rows[0]["AadharCardImg"].ToString() + "?" + DateTime.Now.Ticks.ToString();
                        ImgPassBookFront.ImageUrl = ("/assets/EmpPhotos/") + dt.Rows[0]["EmpSign"].ToString();
                    }
                }
                else
                {
                    //AadharFrontImg.ImageUrl = null;
                    ImgPassBookFront.ImageUrl = null;
                }
                //txtBirthDistrict.Text = dt.Rows[0]["BirthDistrict"].ToString();
                //txtBirthCountry.Text = dt.Rows[0]["BirthCountry"].ToString();
                //txtBirthVillage.Text = dt.Rows[0]["BirthVillage"].ToString();


                //if (dt.Rows[0]["BirthState"].ToString() == "0")
                //{
                //    ddlbirthstate.SelectedIndex = 0;
                //}
                //else
                //{
                //    ddlbirthstate.SelectedValue = dt.Rows[0]["BirthState"].ToString();
                //}

                //string Birthcity = "select CityID,City from cities where state='" + dt.Rows[0]["BirthState"].ToString() + "' order by City";
                //DataTable dtbirthcity = SqlHelper.Instance.GetTableByQuery(Birthcity);
                //if (dtbirthcity.Rows.Count > 0)
                //{
                //    ddlBirthDistrict.Enabled = true;
                //    ddlBirthDistrict.DataValueField = "CityID";
                //    ddlBirthDistrict.DataTextField = "City";
                //    ddlBirthDistrict.DataSource = dtbirthcity;
                //    ddlBirthDistrict.DataBind();
                //    ddlBirthDistrict.Items.Insert(0, new ListItem("--Select--", "0"));
                //}



                //if (dt.Rows[0]["BirthDistrict"].ToString() == "0" || dt.Rows[0]["BirthDistrict"].ToString() == "")
                //{
                //    ddlBirthDistrict.SelectedIndex = 0;

                //}
                //else
                //{
                //    ddlBirthDistrict.Enabled = true;
                //    ddlBirthDistrict.Items.FindByText(dt.Rows[0]["BirthDistrict"].ToString()).Selected = true;

                //}



                ddlAppCategory.SelectedValue = dt.Rows[0]["ApplicantCategory"].ToString();
                bool a = false;
                if (String.IsNullOrEmpty(dt.Rows[0]["SpeciallyAbled"].ToString()) == false)
                {
                    a = Convert.ToBoolean(dt.Rows[0]["SpeciallyAbled"].ToString());
                }
                if (a == true)
                {
                    ChkSpeciallyAbled.Checked = true;

                }
                else
                {
                    ChkSpeciallyAbled.Checked = false;

                }
                txtEyeColour.Text = dt.Rows[0]["EmpEyesColor"].ToString();
                txthaircolour.Text = dt.Rows[0]["EmpHairColor"].ToString();

                if (dt.Rows[0]["EmpBloodGroup"].ToString() == "0" || dt.Rows[0]["EmpBloodGroup"].ToString() == "Choose The Blood Group")
                {
                    ddlBloodGroup.SelectedIndex = 0;
                }
                else
                {
                    ddlBloodGroup.SelectedValue = dt.Rows[0]["EmpBloodGroup"].ToString();
                }

                txtPhyRem.Text = dt.Rows[0]["EmpPhysicalRemarks"].ToString();

                txtImark1.Text = dt.Rows[0]["EmpIdMark1"].ToString();
                txtImark2.Text = dt.Rows[0]["EmpIdMark2"].ToString();
                // txtpermAddr.Text = dt.Rows[0]["EmpPermanentAddress"].ToString();
                //  txtPDist.Text = dt.Rows[0]["EmpPermanentDistrict"].ToString();
                //  txtprsntAddr.Text = dt.Rows[0]["EmpPresentAddress"].ToString();
                txtREfAddr1.Text = dt.Rows[0]["EmpRefAddr1"].ToString();
                txtrefphnno.Text = dt.Rows[0]["RefPhnno1"].ToString();
                txtrefno2.Text = dt.Rows[0]["RefPhnno2"].ToString();
                txtref1mail.Text = dt.Rows[0]["refgmail1"].ToString();
                txtref2mail.Text = dt.Rows[0]["refgmail2"].ToString();
                txtREfAddr2.Text = dt.Rows[0]["EmpRefAddr2"].ToString();
                txtEmpRemarks.Text = dt.Rows[0]["EmpRemarks"].ToString();

                //txtPrdoor.Text = dt.Rows[0]["prDoorno"].ToString();

                //txtstreet.Text = dt.Rows[0]["prStreet"].ToString();
                //txtlmark.Text = dt.Rows[0]["prLmark"].ToString();
                //txtarea.Text = dt.Rows[0]["prArea"].ToString();

                //txtdistrictt.Text = dt.Rows[0]["prDistrict"].ToString();
                //txtpin.Text = dt.Rows[0]["prPincode"].ToString();
                //txtstate.Text = dt.Rows[0]["prState"].ToString();



                if (dt.Rows[0]["prState"].ToString() == "0")
                {
                    ddlpreStates.SelectedIndex = 0;
                }
                else
                {
                    ddlpreStates.SelectedValue = dt.Rows[0]["prState"].ToString();
                }

                string Cityquery = "select CityID,City from cities where State='" + dt.Rows[0]["prstate"].ToString() + "' order by City";
                DataTable CityDt = config.ExecuteAdaptorAsyncWithQueryParams(Cityquery).Result;
                if (CityDt.Rows.Count > 0)
                {
                    ddlpreCity.Enabled = true;
                    ddlpreCity.DataValueField = "CityID";
                    ddlpreCity.DataTextField = "City";
                    ddlpreCity.DataSource = CityDt;
                    ddlpreCity.DataBind();
                    ddlpreCity.Items.Insert(0, new ListItem("--Select--", "0"));

                }

                if (dt.Rows[0]["prcity"].ToString() == "0" || dt.Rows[0]["prcity"].ToString() == "")
                {
                    ddlpreCity.SelectedIndex = 0;

                }
                else
                {
                    ddlpreCity.Enabled = true;
                    ddlpreCity.Items.FindByText(dt.Rows[0]["prcity"].ToString()).Selected = true;

                }



                txtmobile.Text = dt.Rows[0]["prphone"].ToString();
                txtheight.Text = dt.Rows[0]["EmpHeight"].ToString();
                txtweight.Text = dt.Rows[0]["EmpWeight"].ToString();
                txtcheunexpan.Text = dt.Rows[0]["EmpChestunex"].ToString();
                txtcheexpan.Text = dt.Rows[0]["EmpChestExp"].ToString();

                //txtdoor1.Text = dt.Rows[0]["pedoor"].ToString();
                //txtstreet2.Text = dt.Rows[0]["peStreet"].ToString();
                //txtlmark3.Text = dt.Rows[0]["pelmark"].ToString();
                // txtarea4.Text = dt.Rows[0]["peArea"].ToString();
                //txtcity5.Text = dt.Rows[0]["peCity"].ToString();
                //txtPDist.Text = dt.Rows[0]["peDistrict"].ToString();
                //txtpin7.Text = dt.Rows[0]["pePincode"].ToString();
                //txtstate8.Text = dt.Rows[0]["peState"].ToString();

                //txtPresentAddress.Text = dt.Rows[0]["EmpPresentAddress"].ToString();
                txtprPeriodofStay.Text = dt.Rows[0]["prperiodofstay"].ToString();
                txtPeriodofStay.Text = dt.Rows[0]["periodofstay"].ToString();
                txtprPoliceStation.Text = dt.Rows[0]["prPoliceStation"].ToString();
                txtprvillage.Text = dt.Rows[0]["prTown"].ToString();
                txtprtaluka.Text = dt.Rows[0]["prTaluka"].ToString();
                txtprPostOffice.Text = dt.Rows[0]["prPostoffice"].ToString();
                txtprLandmark.Text = dt.Rows[0]["prLmark"].ToString();
                txtprpin.Text = dt.Rows[0]["prPincode"].ToString();


                if (String.IsNullOrEmpty(dt.Rows[0]["ResidingDate"].ToString()) == false)
                {

                    txtResidingDate.Text = DateTime.Parse(dt.Rows[0]["ResidingDate"].ToString()).ToString("dd/MM/yyyy");
                    if (txtResidingDate.Text == "01/01/1900")
                    {
                        txtResidingDate.Text = "";
                    }

                }
                else
                {
                    txtResidingDate.Text = "";

                }


                if (String.IsNullOrEmpty(dt.Rows[0]["prResidingDate"].ToString()) == false)
                {

                    txtprResidingDate.Text = DateTime.Parse(dt.Rows[0]["prResidingDate"].ToString()).ToString("dd/MM/yyyy");
                    if (txtprResidingDate.Text == "01/01/1900")
                    {
                        txtprResidingDate.Text = "";
                    }

                }
                else
                {
                    txtprResidingDate.Text = "";

                }
                //txtPermanentAddress.Text = dt.Rows[0]["EmpPermanentAddress"].ToString();

                if (dt.Rows[0]["peState"].ToString() == "0")
                {
                    DdlStates.SelectedIndex = 0;
                }
                else
                {
                    DdlStates.SelectedValue = dt.Rows[0]["peState"].ToString();
                }

                string Pecity = "select CityID,City from cities where state='" + dt.Rows[0]["peState"].ToString() + "' order by City";
                DataTable dtpecity = config.ExecuteAdaptorAsyncWithQueryParams(Pecity).Result;
                if (dtpecity.Rows.Count > 0)
                {
                    ddlcity.Enabled = true;
                    ddlcity.DataValueField = "CityID";
                    ddlcity.DataTextField = "City";
                    ddlcity.DataSource = dtpecity;
                    ddlcity.DataBind();
                    ddlcity.Items.Insert(0, new ListItem("--Select--", "0"));
                }



                if (dt.Rows[0]["peCity"].ToString() == "0" || dt.Rows[0]["peCity"].ToString() == "")
                {
                    ddlcity.SelectedIndex = 0;

                }
                else
                {
                    ddlcity.Enabled = true;
                    ddlcity.Items.FindByText(dt.Rows[0]["peCity"].ToString()).Selected = true;

                }


                txtmobile9.Text = dt.Rows[0]["pephone"].ToString();
                txtpePoliceStattion.Text = dt.Rows[0]["pePoliceStation"].ToString();
                txtpevillage.Text = dt.Rows[0]["peTown"].ToString();
                txtpeTaluka.Text = dt.Rows[0]["peTaluka"].ToString();
                txtpePostOffice.Text = dt.Rows[0]["pePostoffice"].ToString();
                txtpeLandmark.Text = dt.Rows[0]["peLmark"].ToString();
                txtpePin.Text = dt.Rows[0]["pepincode"].ToString();


                if (dt.Rows.Count > 0)
                {
                    //ddlbankname.SelectedValue = dt.Rows[0]["Empbankname"].ToString();

                    int value = 0;

                    ddlbankname.SelectedIndex = 0;
                    if (String.IsNullOrEmpty(dt.Rows[0]["Empbankname"].ToString()) == false)
                    {

                        value = int.Parse(dt.Rows[0]["Empbankname"].ToString());
                        if (value != 0)
                        {
                            ddlbankname.SelectedIndex = value;
                        }
                    }

                    txtbranchname.Text = dt.Rows[0]["Empbankbranchname"].ToString();
                    txtIFSCcode.Text = dt.Rows[0]["EmpIFSCcode"].ToString();
                    txtBankAppNum.Text = dt.Rows[0]["EmpBankAppNo"].ToString();
                    txtBankCardRef.Text = dt.Rows[0]["EmpBankCardRef"].ToString();
                    txtRegCode.Text = dt.Rows[0]["EmpRegionCode"].ToString();
                    txtBranchCode.Text = dt.Rows[0]["EmpBranchCode"].ToString();
                    txtInsDeb.Text = dt.Rows[0]["EmpInsDedAmt"].ToString();

                    txtBankAccNum.Text = dt.Rows[0]["EmpBankAcNo"].ToString();
                    txtBankCodenum.Text = dt.Rows[0]["EmpBankCode"].ToString();

                    txtEmpInsNominee.Text = dt.Rows[0]["EmpInsNominee"].ToString();
                    txtEmpNomRel.Text = dt.Rows[0]["EmpNomineeRel"].ToString();
                    txtaadhaar.Text = dt.Rows[0]["aadhaarid"].ToString();
                    txtInsCover.Text = dt.Rows[0]["EmpInsCover"].ToString();
                    txtUANNumber.Text = dt.Rows[0]["EmpUANNumber"].ToString();

                    if (String.IsNullOrEmpty(dt.Rows[0]["EmpNomineeDtofBirth"].ToString()) == false)
                    {

                        txtNomDoB.Text = DateTime.Parse(dt.Rows[0]["EmpNomineeDtofBirth"].ToString()).ToString("dd/MM/yyyy");
                        if (txtNomDoB.Text == "01/01/1900")
                        {
                            txtNomDoB.Text = "";
                        }

                    }
                    else
                    {
                        txtNomDoB.Text = "";
                    }


                }

                txtaddlamt.Text = dt.Rows[0]["AddlAmount"].ToString();
                txtfoodallowance.Text = dt.Rows[0]["FoodAllowance"].ToString();

                string selectquery5 = " select * from EMPEPFCodes where EmpID = '" + empid + "'";
                DataTable dt5 = config.ExecuteAdaptorAsyncWithQueryParams(selectquery5).Result;
                txtEmpPFNumber.Text = "0";

                if (dt5.Rows.Count > 0)
                {
                    txtEmpPFNumber.Text = dt5.Rows[0]["EmpEpfNo"].ToString();
                    txtPFNominee.Text = dt5.Rows[0]["EmpNominee"].ToString();
                    txtPFNomineeRel.Text = dt5.Rows[0]["EmpRelation"].ToString();

                    if (String.IsNullOrEmpty(dt5.Rows[0]["EmpPFEnrolDt"].ToString()) == false)
                    {

                        txtPFEnrollDate.Text = DateTime.Parse(dt5.Rows[0]["EmpPFEnrolDt"].ToString()).ToString("dd/MM/yyyy");
                        if (txtPFEnrollDate.Text == "01/01/1900")
                        {
                            txtPFEnrollDate.Text = "";
                        }

                    }
                    else
                    {
                        txtPFEnrollDate.Text = "";
                    }
                }

                #region for Salary Structure

                string QuerySalStructure = "select * from EmpSalaryStructure where EmpID = '" + empid + "'";
                DataTable dtSalStructure = config.ExecuteAdaptorAsyncWithQueryParams(QuerySalStructure).Result;
                if (dtSalStructure.Rows.Count > 0)
                {
                    TxtBasic.Text = dtSalStructure.Rows[0]["Basic"].ToString();
                    txtda.Text = dtSalStructure.Rows[0]["DA"].ToString();
                    txthra.Text = dtSalStructure.Rows[0]["HRA"].ToString();
                    txtConveyance.Text = dtSalStructure.Rows[0]["Conveyance"].ToString();
                    txtcca.Text = dtSalStructure.Rows[0]["CCA"].ToString();
                    txtbonus.Text = dtSalStructure.Rows[0]["Bonus"].ToString();
                    txtgratuty.Text = dtSalStructure.Rows[0]["Gratuity"].ToString();
                    txtwa.Text = dtSalStructure.Rows[0]["WashAllowance"].ToString();
                    txtNfhs1.Text = dtSalStructure.Rows[0]["NFHS"].ToString();
                    txtMedicalAllw.Text = dtSalStructure.Rows[0]["MedicalAllowance"].ToString();
                    txtMobileAllowance.Text = dtSalStructure.Rows[0]["MobileAllowance"].ToString();
                    txtSplAllw.Text = dtSalStructure.Rows[0]["SplAllowance"].ToString();
                    txtleaveamount.Text = dtSalStructure.Rows[0]["LeaveAmount"].ToString();
                    txtoa.Text = dtSalStructure.Rows[0]["OtherAllowance"].ToString();
                    Txtrc.Text = dtSalStructure.Rows[0]["RC"].ToString();
                    TxtCs.Text = dtSalStructure.Rows[0]["CS"].ToString();
                    TxtOTRate.Text = dtSalStructure.Rows[0]["OTRate"].ToString();
                    txtTravellingAllowance.Text = dtSalStructure.Rows[0]["TravellingAllowance"].ToString();
                    txtPerformanceAllowance.Text = dtSalStructure.Rows[0]["PerformanceAllowance"].ToString();
                    TxtPFPayRate.Text = dtSalStructure.Rows[0]["PFPayrate"].ToString();
                    TxtESIPayRate.Text = dtSalStructure.Rows[0]["ESIPayrate"].ToString();
                    txtEducationAllowance.Text = dtSalStructure.Rows[0]["EducationAllowance"].ToString();
                    TxtPFVoluntary.Text = dtSalStructure.Rows[0]["PFVoluntary"].ToString();


                    if (String.IsNullOrEmpty(dtSalStructure.Rows[0]["Nots"].ToString()) != false)
                    {
                        ddlNoOfOtsPaysheet.SelectedIndex = 0;
                    }
                    else
                    {
                        if (dtSalStructure.Rows[0]["Nots"].ToString().Trim().Length > 0)
                        {
                            int noofdays = int.Parse(dtSalStructure.Rows[0]["Nots"].ToString());
                            if (noofdays == 0)
                            {
                                ddlNoOfOtsPaysheet.SelectedIndex = 0;
                            }

                            else if (noofdays == 1)
                            {
                                ddlNoOfOtsPaysheet.SelectedIndex = 1;
                            }
                            else if (noofdays == 2)
                            {
                                ddlNoOfOtsPaysheet.SelectedIndex = 2;
                            }
                            else if (noofdays == 3)
                            {
                                ddlNoOfOtsPaysheet.SelectedIndex = 3;
                            }
                            else if (noofdays == 4)
                            {
                                ddlNoOfOtsPaysheet.SelectedIndex = 4;
                            }

                            else
                            {
                                ddlNoOfOtsPaysheet.SelectedValue = dtSalStructure.Rows[0]["Nots"].ToString();
                            }
                        }

                    }


                    if (String.IsNullOrEmpty(dtSalStructure.Rows[0]["NoOfDays"].ToString()) != false)
                    {
                        ddlNoOfDaysWages.SelectedIndex = 0;
                    }
                    else
                    {
                        if (dtSalStructure.Rows[0]["NoOfDays"].ToString().Trim().Length > 0)
                        {
                            float noofdays = float.Parse(dtSalStructure.Rows[0]["NoOfDays"].ToString());
                            if (noofdays == 0)
                            {
                                ddlNoOfDaysWages.SelectedIndex = 0;
                            }

                            else if (noofdays == 1)
                            {
                                ddlNoOfDaysWages.SelectedIndex = 1;
                            }
                            else if (noofdays == 2)
                            {
                                ddlNoOfDaysWages.SelectedIndex = 2;
                            }
                            else if (noofdays == 3)
                            {
                                ddlNoOfDaysWages.SelectedIndex = 3;
                            }
                            else if (noofdays == 4)
                            {
                                ddlNoOfDaysWages.SelectedIndex = 4;
                            }

                            else
                            {
                                ddlNoOfDaysWages.SelectedValue = dtSalStructure.Rows[0]["NoOfDays"].ToString();
                            }
                        }

                    }


                    if (String.IsNullOrEmpty(dtSalStructure.Rows[0]["PFNoOfDays"].ToString()) != false)
                    {
                        ddlNoOfDaysWages.SelectedIndex = 0;
                    }
                    else
                    {
                        if (dtSalStructure.Rows[0]["PFNoOfDays"].ToString().Trim().Length > 0)
                        {
                            float noofpfdays = float.Parse(dtSalStructure.Rows[0]["PFNoOfDays"].ToString());
                            if (noofpfdays == 0)
                            {
                                ddlPFNoOfDaysForWages.SelectedIndex = 0;
                            }

                            else if (noofpfdays == 1)
                            {
                                ddlPFNoOfDaysForWages.SelectedIndex = 1;
                            }
                            else if (noofpfdays == 2)
                            {
                                ddlPFNoOfDaysForWages.SelectedIndex = 2;
                            }
                            else if (noofpfdays == 3)
                            {
                                ddlPFNoOfDaysForWages.SelectedIndex = 3;
                            }
                            else if (noofpfdays == 4)
                            {
                                ddlPFNoOfDaysForWages.SelectedIndex = 4;
                            }

                            else
                            {
                                ddlPFNoOfDaysForWages.SelectedValue = dtSalStructure.Rows[0]["PFNoOfDays"].ToString();
                            }
                        }

                    }


                    #endregion for Salary Structure

                    string selectquery6 = "select * from EMPESICodes where EmpID = '" + empid + "'";
                    DataTable dt6 = config.ExecuteAdaptorAsyncWithQueryParams(selectquery6).Result;
                    txtESINum.Text = txtESIDiSName.Text = txtESINominee.Text = txtESINomRel.Text = "0";
                    if (dt6.Rows.Count > 0)
                    {
                        txtESINum.Text = dt6.Rows[0]["EmpESINo"].ToString();
                        txtESIDiSName.Text = dt6.Rows[0]["EmpESIDispName"].ToString();
                        txtESINominee.Text = dt6.Rows[0]["EmpESINominee"].ToString();
                        txtESINomRel.Text = dt6.Rows[0]["EMPESIRelation"].ToString();
                    }

                    string selectquery7 = "select * from EmpExservice where EmpID = '" + empid + "'";
                    DataTable dt7 = config.ExecuteAdaptorAsyncWithQueryParams(selectquery7).Result;

                    txtServiceNum.Text = txtCrops.Text = txtMCategory.Text = txtConduct.Text = txtRank.Text =
                    txtTrade.Text = TxtROfDischarge.Text = "0";
                    if (dt7.Rows.Count > 0)
                    {
                        txtServiceNum.Text = dt7.Rows[0]["ServiceNo"].ToString();

                        txtCrops.Text = dt7.Rows[0]["Crops"].ToString();
                        txtMCategory.Text = dt7.Rows[0]["MedcalCategoryBloodGroup"].ToString();
                        txtConduct.Text = dt7.Rows[0]["Conduct"].ToString();
                        txtRank.Text = dt7.Rows[0]["Rank"].ToString();

                        txtTrade.Text = dt7.Rows[0]["Trade"].ToString();
                        TxtROfDischarge.Text = dt7.Rows[0]["ReasonsofDischarge"].ToString();

                        if (String.IsNullOrEmpty(dt7.Rows[0]["DtofEnrolment"].ToString()) == false)
                        {

                            txtDOfEnroll.Text = DateTime.Parse(dt7.Rows[0]["DtofEnrolment"].ToString()).ToString("dd/MM/yyyy");
                            if (txtDOfEnroll.Text == "01/01/1900")
                            {
                                txtDOfEnroll.Text = "";
                            }

                        }
                        else
                        {
                            txtDOfEnroll.Text = "";
                        }


                        //if (String.IsNullOrEmpty(dt7.Rows[0]["DtofDischarge"].ToString()) == false)
                        //{

                        //    txtDOFDischarge.Text = DateTime.Parse(dt7.Rows[0]["DtofDischarge"].ToString()).ToString("dd/MM/yyyy");
                        //    if (txtDOFDischarge.Text == "01/01/1900")
                        //    {
                        //        txtDOFDischarge.Text = "";
                        //    }

                        //}
                        //else
                        //{
                        //    txtDOFDischarge.Text = "";
                        //}

                        string SqlPoliceRecord = "Select * from EmpPoliceRecord where empid='" + empid + "'";
                        DataTable dtpr = config.ExecuteAdaptorAsyncWithQueryParams(SqlPoliceRecord).Result;

                        var CriminalOffCheck = "N";
                        var CriminalProCheck = "N";
                        var CriminalArrestCheck = "N";
                        var PoliceVerificationCheck = "N";

                        if (dtpr.Rows.Count > 0)
                        {

                            PoliceVerificationCheck = dtpr.Rows[0]["PoliceVerificationCheck"].ToString();
                            if (PoliceVerificationCheck == "Y")
                            {
                                rdbVerified.Checked = true;
                                txtPoliceVerificationNo.Text = dtpr.Rows[0]["PoliceVerificationNo"].ToString();
                                txtPoliceVerificationNo.Enabled = true;
                                rdbNotVerified.Checked = false;
                            }
                            else
                            {
                                rdbNotVerified.Checked = true;
                                rdbVerified.Checked = false;
                                txtPoliceVerificationNo.Enabled = false;
                            }

                            CriminalOffCheck = dtpr.Rows[0]["CriminalOffCheck"].ToString();
                            if (CriminalOffCheck == "Y")
                            {
                                ChkCriminalOff.Checked = true;
                                txtCriminalOffcaseNo.Enabled = true;
                                txtCriminalOffcaseNo.Text = dtpr.Rows[0]["CriminalOffcaseNo"].ToString();
                                txtCriminalOffCName.Enabled = true;
                                txtCriminalOffCName.Text = dtpr.Rows[0]["CriminalOffCName"].ToString();
                                txtCriminalOff.Enabled = true;
                                txtCriminalOff.Text = dtpr.Rows[0]["CriminalOff"].ToString();
                            }
                            else
                            {
                                ChkCriminalOff.Checked = false;
                                txtCriminalOffcaseNo.Enabled = false;
                                txtCriminalOffCName.Enabled = false;
                                txtCriminalOff.Enabled = false;
                            }

                            CriminalProCheck = dtpr.Rows[0]["CriminalProCheck"].ToString();
                            if (CriminalProCheck == "Y")
                            {
                                ChkCriminalProc.Checked = true;
                                txtCriminalProCaseNo.Enabled = true;
                                txtCriminalProCaseNo.Text = dtpr.Rows[0]["CriminalProCaseNo"].ToString();
                                txtCriminalProCName.Enabled = true;
                                txtCriminalProCName.Text = dtpr.Rows[0]["CriminalProCName"].ToString();
                                txtCriminalProOffence.Enabled = true;
                                txtCriminalProOffence.Text = dtpr.Rows[0]["CriminalProOffence"].ToString();

                            }
                            else
                            {
                                ChkCriminalProc.Checked = false;
                                txtCriminalProCaseNo.Enabled = false;
                                txtCriminalProCName.Enabled = false;
                                txtCriminalProOffence.Enabled = false;
                            }

                            CriminalArrestCheck = dtpr.Rows[0]["CriminalArrestCheck"].ToString();
                            if (CriminalArrestCheck == "Y")
                            {
                                ChkCrimalArrest.Checked = true;
                                txtCriminalArrestCaseNo.Enabled = true;
                                txtCriminalArrestCaseNo.Text = dtpr.Rows[0]["CriminalArrestCaseNo"].ToString();
                                txtCriminalArrestCName.Enabled = true;
                                txtCriminalArrestCName.Text = dtpr.Rows[0]["CriminalArrestCName"].ToString();
                                txtCriminalArrestOffence.Enabled = true;
                                txtCriminalArrestOffence.Text = dtpr.Rows[0]["CriminalArrestOffence"].ToString();

                            }
                            else
                            {
                                ChkCrimalArrest.Checked = false;
                                txtCriminalArrestCaseNo.Enabled = false;
                                txtCriminalArrestCName.Enabled = false;
                                txtCriminalArrestOffence.Enabled = false;
                            }


                        }



                        string SqlProofDetails = "Select * from EmpProofDetails where empid='" + empid + "'";
                        DataTable dtpd = config.ExecuteAdaptorAsyncWithQueryParams(SqlProofDetails).Result;

                        var AadharCard = "N";
                        var drivingLicense = "N";
                        var VoterID = "N";
                        var ElectricityBill = "N";
                        var BankPassbook = "N";
                        var RationCard = "N";
                        var PanCard = "N";
                        var ESICCard = "N";
                        var Other = "N";
                        var aadharcardstatus = "N";
                        var Drivingstatus = "N";
                        var Voterstatus = "N";
                        var Rationcardstatus = "N";
                        var Pancardstatus = "N";
                        var Bankpassbookstatus = "N";
                        var Electricitystatus = "N";
                        var ESICstatus = "N";
                        var Othersstatus = "N";
                        var SSC = "N";
                        var SSCStatus = "N";

                        var aadharcardCopystatus = "";
                        var DrivingCopystatus = "";
                        var VoterCopystatus = "";
                        var RationcardCopystatus = "";
                        var PancardCopystatus = "";
                        var BankpassbookCopystatus = "";
                        var ElectricityCopystatus = "";
                        var ESICCopystatus = "";
                        var OthersCopystatus = "";
                        var SSCCopystatus = "";

                        if (dtpd.Rows.Count > 0)
                        {
                            AadharCard = dtpd.Rows[0]["AadharCard"].ToString();
                            txtAadharCard.Text = dtpd.Rows[0]["AadharCardNo"].ToString();
                            txtAadharName.Text = dtpd.Rows[0]["AadharCardName"].ToString();

                            aadharcardstatus = dtpd.Rows[0]["Aadharstatus"].ToString();
                            aadharcardCopystatus = dtpd.Rows[0]["AadharCopystatus"].ToString();

                            if (aadharcardstatus == "N")
                            {
                                RbnNotsubmitted.Checked = true;
                            }
                            if (aadharcardstatus == "Y")
                            {
                                Rbnsubmitted.Checked = true;
                                RbnXerox.Visible = true;
                                RbnOriginal.Visible = true;
                            }

                            if (aadharcardCopystatus == "Xerox")
                            {
                                RbnXerox.Checked = true;
                            }
                            if (aadharcardCopystatus == "Original")
                            {
                                RbnXerox.Checked = false;
                                RbnOriginal.Checked = true;
                            }

                            if (dtpd.Rows[0]["AadharCardImg"].ToString().Length > 0)
                            {
                                AadharImg.ImageUrl = ("/assets/EmpPhotos/") + dtpd.Rows[0]["AadharCardImg"].ToString() + "?" + DateTime.Now.Ticks.ToString();


                            }
                            else
                            {
                                AadharImg.ImageUrl = null;
                            }


                            drivingLicense = dtpd.Rows[0]["drivingLicense"].ToString();
                            txtDrivingLicense.Text = dtpd.Rows[0]["drivingLicenseNo"].ToString();
                            txtDrivingLicenseName.Text = dtpd.Rows[0]["DrivingLicenseName"].ToString();
                            Drivingstatus = dtpd.Rows[0]["Drivingstatus"].ToString();
                            DrivingCopystatus = dtpd.Rows[0]["DrivingCopystatus"].ToString();


                            if (Drivingstatus == "N")
                            {
                                RbnNotsubmitted1.Checked = true;
                            }
                            if (Drivingstatus == "Y")
                            {
                                Rbnsubmitted1.Checked = true;
                                RbnXerox1.Visible = true;
                                RbnOriginal1.Visible = true;
                            }

                            if (DrivingCopystatus == "Xerox")
                            {
                                RbnXerox1.Checked = true;
                            }
                            if (DrivingCopystatus == "Original")
                            {
                                RbnXerox1.Checked = false;
                                RbnOriginal1.Checked = true;
                            }


                            VoterID = dtpd.Rows[0]["VoterID"].ToString();
                            txtVoterID.Text = dtpd.Rows[0]["VoterIDNo"].ToString();
                            txtVoterName.Text = dtpd.Rows[0]["VoterIDName"].ToString();
                            Voterstatus = dtpd.Rows[0]["VoterIdstatus"].ToString();
                            VoterCopystatus = dtpd.Rows[0]["VoterIdCopystatus"].ToString();


                            if (Voterstatus == "N")
                            {
                                RbnNotsubmitted2.Checked = true;
                            }
                            if (Voterstatus == "Y")
                            {
                                Rbnsubmitted2.Checked = true;
                                RbnXerox2.Visible = true;
                                RbnOriginal2.Visible = true;
                            }

                            if (VoterCopystatus == "Xerox")
                            {
                                RbnXerox2.Checked = true;
                            }

                            if (VoterCopystatus == "Original")
                            {
                                RbnXerox2.Checked = false;
                                RbnOriginal2.Checked = true;
                            }


                            RationCard = dtpd.Rows[0]["RationCard"].ToString();
                            txtRationCard.Text = dtpd.Rows[0]["RationCardNo"].ToString();
                            txtRationCardName.Text = dtpd.Rows[0]["RationCardName"].ToString();
                            Rationcardstatus = dtpd.Rows[0]["Rationstatus"].ToString();
                            RationcardCopystatus = dtpd.Rows[0]["RationCopystatus"].ToString();

                            if (Rationcardstatus == "N")
                            {
                                RbnNotsubmitted3.Checked = true;
                            }
                            if (Rationcardstatus == "Y")
                            {
                                Rbnsubmitted3.Checked = true;
                                RbnXerox3.Visible = true;
                                RbnOriginal3.Visible = true;
                            }

                            if (RationcardCopystatus == "Xerox")
                            {
                                RbnXerox3.Checked = true;
                            }
                            if (RationcardCopystatus == "Original")
                            {
                                RbnXerox3.Checked = false;
                                RbnOriginal3.Checked = true;
                            }



                            PanCard = dtpd.Rows[0]["PanCard"].ToString();
                            txtPanCard.Text = dtpd.Rows[0]["PanCardNo"].ToString();
                            txtPanCardName.Text = dtpd.Rows[0]["PanCardName"].ToString();
                            Pancardstatus = dtpd.Rows[0]["Pancardstatus"].ToString();
                            PancardCopystatus = dtpd.Rows[0]["PancardCopystatus"].ToString();


                            if (Pancardstatus == "N")
                            {
                                RbnNotsubmitted5.Checked = true;
                            }
                            if (Pancardstatus == "Y")
                            {
                                Rbnsubmitted5.Checked = true;
                                RbnXerox5.Visible = true;
                                RbnOriginal5.Visible = true;
                            }

                            if (PancardCopystatus == "Xerox")
                            {
                                RbnXerox5.Checked = true;
                            }
                            if (PancardCopystatus == "Original")
                            {
                                RbnXerox5.Checked = false;
                                RbnOriginal5.Checked = true;
                            }



                            BankPassbook = dtpd.Rows[0]["Passbook"].ToString();
                            txtBankPassbook.Text = dtpd.Rows[0]["PassbookNo"].ToString();
                            txtBankPassBookName.Text = dtpd.Rows[0]["PassBookName"].ToString();
                            Bankpassbookstatus = dtpd.Rows[0]["Passbookstatus"].ToString();
                            BankpassbookCopystatus = dtpd.Rows[0]["PassbookCopystatus"].ToString();

                            if (Bankpassbookstatus == "N")
                            {
                                RbnNotsubmitted6.Checked = true;
                            }
                            if (Bankpassbookstatus == "Y")
                            {
                                Rbnsubmitted6.Checked = true;
                                RbnXerox6.Visible = true;
                                RbnOriginal6.Visible = true;
                            }

                            if (BankpassbookCopystatus == "Xerox")
                            {
                                RbnXerox6.Checked = true;
                            }
                            if (BankpassbookCopystatus == "Original")
                            {
                                RbnXerox6.Checked = false;
                                RbnOriginal6.Checked = true;
                            }



                            ElectricityBill = dtpd.Rows[0]["ElectricityBill"].ToString();
                            txtElectricityBill.Text = dtpd.Rows[0]["ElectricityBillNo"].ToString();
                            txtElecBillname.Text = dtpd.Rows[0]["ElectricityBillName"].ToString();
                            Electricitystatus = dtpd.Rows[0]["Electricitystatus"].ToString();
                            ElectricityCopystatus = dtpd.Rows[0]["ElectricityCopystatus"].ToString();

                            if (Electricitystatus == "N")
                            {
                                RbnNotsubmitted7.Checked = true;
                            }
                            if (Electricitystatus == "Y")
                            {
                                Rbnsubmitted7.Checked = true;
                                RbnXerox7.Visible = true;
                                RbnOriginal7.Visible = true;
                            }

                            if (ElectricityCopystatus == "Xerox")
                            {
                                RbnXerox7.Checked = true;
                            }
                            if (ElectricityCopystatus == "Original")
                            {
                                RbnXerox7.Checked = false;
                                RbnOriginal7.Checked = true;
                            }


                            ESICCard = dtpd.Rows[0]["ESICCard"].ToString();
                            txtESICCardNo.Text = dtpd.Rows[0]["ESICCardNo"].ToString();
                            txtESICName.Text = dtpd.Rows[0]["ESICCardName"].ToString();
                            ESICstatus = dtpd.Rows[0]["ESICstatus"].ToString();
                            ESICCopystatus = dtpd.Rows[0]["ESICCopystatus"].ToString();

                            if (ESICstatus == "N")
                            {
                                RbnNotsubmitted8.Checked = true;
                            }
                            if (ESICstatus == "Y")
                            {
                                Rbnsubmitted8.Checked = true;
                                RbnXerox8.Visible = true;
                                RbnOriginal8.Visible = true;
                            }

                            if (ESICCopystatus == "Xerox")
                            {
                                RbnXerox8.Checked = true;
                            }
                            if (ESICCopystatus == "Original")
                            {
                                RbnXerox8.Checked = false;
                                RbnOriginal8.Checked = true;
                            }


                            Other = dtpd.Rows[0]["Others"].ToString();
                            txtOther.Text = dtpd.Rows[0]["OtherType"].ToString();
                            txtOtherName.Text = dtpd.Rows[0]["OtherTypeName"].ToString();
                            Othersstatus = dtpd.Rows[0]["Othersstatus"].ToString();
                            OthersCopystatus = dtpd.Rows[0]["OthersCopystatus"].ToString();

                            if (Othersstatus == "N")
                            {
                                RbnNotsubmitted4.Checked = true;
                            }
                            if (Othersstatus == "Y")
                            {
                                Rbnsubmitted4.Checked = true;
                                RbnXerox4.Visible = true;
                                RbnOriginal4.Visible = true;
                            }

                            if (OthersCopystatus == "Xerox")
                            {
                                RbnXerox4.Checked = true;
                            }
                            if (OthersCopystatus == "Original")
                            {
                                RbnXerox4.Checked = false;
                                RbnOriginal4.Checked = true;
                            }

                            SSC = dtpd.Rows[0]["SSC"].ToString();
                            txtsscno.Text = dtpd.Rows[0]["SSCNo"].ToString();

                            SSCStatus = dtpd.Rows[0]["SSCStatus"].ToString();
                            SSCCopystatus = dtpd.Rows[0]["SSCCopyStatus"].ToString();

                            if (SSCStatus == "N")
                            {
                                RbnNotSubmitted9.Checked = true;
                                RbnXerox9.Visible = false;
                                RbnOriginal9.Visible = false;
                            }
                            if (SSCStatus == "Y")
                            {
                                RbnSubmitted9.Checked = true;
                                RbnXerox9.Visible = true;
                                RbnOriginal9.Visible = true;
                            }

                            if (SSCCopystatus == "Xerox")
                            {
                                RbnXerox9.Checked = true;
                                RbnOriginal9.Checked = false;
                            }
                            if (SSCCopystatus == "Original")
                            {
                                RbnXerox9.Checked = false;
                                RbnOriginal9.Checked = true;
                            }


                            if (AadharCard == "Y")
                            {
                                ChkAadharCard.Checked = true;
                                txtAadharCard.Enabled = true;
                                txtAadharName.Enabled = true;
                            }
                            else
                            {
                                ChkAadharCard.Checked = false;
                                txtAadharCard.Enabled = false;
                                txtAadharName.Enabled = false;

                            }

                            if (drivingLicense == "Y")
                            {
                                ChkdrivingLicense.Checked = true;
                                txtDrivingLicense.Enabled = true;
                                txtDrivingLicenseName.Enabled = true;
                            }
                            else
                            {
                                ChkdrivingLicense.Checked = false;
                                txtDrivingLicense.Enabled = false;
                                txtDrivingLicenseName.Enabled = false;

                            }

                            if (VoterID == "Y")
                            {
                                ChkVoterID.Checked = true;
                                txtVoterID.Enabled = true;
                                txtVoterName.Enabled = true;
                            }
                            else
                            {
                                ChkVoterID.Checked = false;
                                txtVoterID.Enabled = false;
                                txtVoterName.Enabled = false;

                            }

                            if (RationCard == "Y")
                            {
                                ChkRationCard.Checked = true;
                                txtRationCard.Enabled = true;
                                txtRationCardName.Enabled = true;
                            }
                            else
                            {
                                ChkRationCard.Checked = false;
                                txtRationCard.Enabled = false;
                                txtRationCardName.Enabled = false;
                            }

                            if (PanCard == "Y")
                            {
                                ChkPanCard.Checked = true;
                                txtPanCard.Enabled = true;
                                txtPanCardName.Enabled = true;
                            }
                            else
                            {
                                ChkPanCard.Checked = false;
                                txtPanCard.Enabled = false;
                                txtPanCardName.Enabled = false;
                            }


                            if (BankPassbook == "Y")
                            {
                                ChkBankPassbook.Checked = true;
                                txtBankPassbook.Enabled = true;
                                txtBankPassBookName.Enabled = true;
                            }
                            else
                            {
                                ChkBankPassbook.Checked = false;
                                txtBankPassbook.Enabled = false;
                                txtBankPassBookName.Enabled = false;

                            }

                            if (ElectricityBill == "Y")
                            {
                                ChkElectricityBill.Checked = true;
                                txtElectricityBill.Enabled = true;
                                txtElecBillname.Enabled = true;
                            }
                            else
                            {
                                ChkElectricityBill.Checked = false;
                                txtElectricityBill.Enabled = false;
                                txtElecBillname.Enabled = false;
                            }

                            if (ESICCard == "Y")
                            {
                                ChkESICCard.Checked = true;
                                txtESICCardNo.Enabled = true;
                                txtESICName.Enabled = true;
                            }
                            else
                            {
                                ChkESICCard.Checked = false;
                                txtESICCardNo.Enabled = false;
                                txtESICName.Enabled = false;
                            }

                            if (Other == "Y")
                            {
                                Chkother.Checked = true;
                                txtOther.Enabled = true;
                                txtOtherName.Enabled = true;
                            }
                            else
                            {
                                Chkother.Checked = false;
                                txtOther.Enabled = false;
                                txtOtherName.Enabled = false;

                            }

                            if (SSC == "Y")
                            {
                                ChkSSC.Checked = true;
                                txtsscno.Enabled = true;
                            }
                            else
                            {
                                ChkSSC.Checked = false;
                                txtsscno.Enabled = false;
                            }


                        }

                        string sqlFamilyDetails = "select ER.RName,ER.RType,ER.EmpId,Convert(nvarchar(10),ER.DOfBirth,103) as DOfBirth,ER.pfnominee,ER.Esinominee,ER.age,ER.ROccupation,ER.RResidence,ER.RPlace,ER.RPhone,ER.RGMail from EmpRelationships as ER join EmpDetails as ED on ER.EmpId=ED.EmpId where ED.EmpID = '" + empid + "' order by id ";
                        DataTable dtfm = config.ExecuteAdaptorAsyncWithQueryParams(sqlFamilyDetails).Result;
                        if (dtfm.Rows.Count > 0)
                        {
                            gvFamilyDetails.DataSource = dtfm;
                            gvFamilyDetails.DataBind();

                            foreach (GridViewRow dr in gvFamilyDetails.Rows)
                            {
                                if (dtfm.Rows.Count == dr.RowIndex)
                                {
                                    break;
                                }
                                TextBox txtEmpRName = dr.FindControl("txtEmpName") as TextBox;
                                DropDownList ddlRelationtype = dr.FindControl("ddlRelation") as DropDownList;
                                TextBox txtDOFBirth = dr.FindControl("txtRelDtofBirth") as TextBox;
                                TextBox txtAge = dr.FindControl("txtAge") as TextBox;
                                TextBox txtoccupation = dr.FindControl("txtReloccupation") as TextBox;
                                TextBox txtRelphnno = dr.FindControl("txtRelphnno") as TextBox;
                                TextBox txtrelmail = dr.FindControl("txtRelmail") as TextBox;
                                DropDownList ddlrelresidence = dr.FindControl("ddlresidence") as DropDownList;
                                TextBox txtRelplace = dr.FindControl("txtplace") as TextBox;
                                CheckBox ChkPfNominee = dr.FindControl("ChkPFNominee") as CheckBox;
                                CheckBox ChkESINominee = dr.FindControl("ChkESINominee") as CheckBox;

                                txtEmpRName.Text = dtfm.Rows[dr.RowIndex]["RName"].ToString();
                                ddlRelationtype.SelectedValue = dtfm.Rows[dr.RowIndex]["RType"].ToString();
                                txtDOFBirth.Text = dtfm.Rows[dr.RowIndex]["DOfBirth"].ToString();

                                if (txtDOFBirth.Text == "01/01/1900")
                                {
                                    txtDOFBirth.Text = "";
                                }
                                txtAge.Text = dtfm.Rows[dr.RowIndex]["Age"].ToString();
                                txtoccupation.Text = dtfm.Rows[dr.RowIndex]["ROccupation"].ToString();
                                txtRelphnno.Text = dtfm.Rows[dr.RowIndex]["RPhone"].ToString();
                                txtrelmail.Text = dtfm.Rows[dr.RowIndex]["RGMail"].ToString();
                                ddlrelresidence.SelectedValue = dtfm.Rows[dr.RowIndex]["RResidence"].ToString();
                                txtRelplace.Text = dtfm.Rows[dr.RowIndex]["RPlace"].ToString();
                                string PFNominee = dtfm.Rows[dr.RowIndex]["PfNominee"].ToString();
                                if (PFNominee == "Y")
                                {
                                    ChkPfNominee.Checked = true;

                                }
                                else
                                {
                                    ChkPfNominee.Checked = false;
                                }
                                string ESINominee = dtfm.Rows[dr.RowIndex]["EsiNominee"].ToString();
                                if (ESINominee == "Y")
                                {
                                    ChkESINominee.Checked = true;

                                }
                                else
                                {
                                    ChkESINominee.Checked = false;
                                }
                            }
                        }
                        else
                        {
                            for (int i = 0; i < gvFamilyDetails.Rows.Count; i++)
                            {
                                TextBox txtEmpRName = gvFamilyDetails.Rows[i].FindControl("txtEmpName") as TextBox;
                                DropDownList ddlRelationtype = gvFamilyDetails.Rows[i].FindControl("ddlRelation") as DropDownList;
                                // TextBox txtDOFBirth = gvFamilyDetails.Rows[i].FindControl("txtRelDtofBirth") as TextBox;
                                TextBox txtAge = gvFamilyDetails.Rows[i].FindControl("txtAge") as TextBox;
                                TextBox txtoccupation = gvFamilyDetails.Rows[i].FindControl("txtReloccupation") as TextBox;
                                TextBox txtrelphone = gvFamilyDetails.Rows[i].FindControl("txtRelphnno") as TextBox;
                                TextBox txtrelmail = gvFamilyDetails.Rows[i].FindControl("txtRelmail") as TextBox;
                                DropDownList ddlrelresidence = gvFamilyDetails.Rows[i].FindControl("ddlresidence") as DropDownList;
                                TextBox txtRelplace = gvFamilyDetails.Rows[i].FindControl("txtplace") as TextBox;
                                CheckBox ChkPfNominee = gvFamilyDetails.Rows[i].FindControl("ChkPFNominee") as CheckBox;
                                CheckBox ChkESINominee = gvFamilyDetails.Rows[i].FindControl("ChkESINominee") as CheckBox;



                                txtEmpRName.Text = "";
                                ddlRelationtype.SelectedIndex = 0;
                                txtrelphone.Text = "";
                                //  txtDOFBirth.Text = "";
                                txtAge.Text = "";
                                txtoccupation.Text = "";
                                ddlrelresidence.SelectedIndex = 0;
                                txtRelplace.Text = "";
                                ChkPfNominee.Checked = false;
                                ChkESINominee.Checked = false;
                            }
                        }
                        string sqlEducationDetails = "select * from EmpEducationDetails where EmpID = '" + empid + "' order by id ";
                        DataTable dted = config.ExecuteAdaptorAsyncWithQueryParams(sqlEducationDetails).Result;
                        if (dted.Rows.Count > 0)
                        {
                            GvEducationDetails.DataSource = dted;
                            GvEducationDetails.DataBind();

                            foreach (GridViewRow dr in GvEducationDetails.Rows)
                            {
                                if (dted.Rows.Count == dr.RowIndex)
                                {
                                    break;
                                }

                                DropDownList ddlQualification = dr.FindControl("ddlQualification") as DropDownList;
                                TextBox txtEdLevel = dr.FindControl("txtEdLevel") as TextBox;
                                TextBox txtNameofSchoolColg = dr.FindControl("txtNameofSchoolColg") as TextBox;
                                TextBox txtBoard = dr.FindControl("txtBoard") as TextBox;
                                TextBox txtyear = dr.FindControl("txtyear") as TextBox;
                                TextBox txtPassFail = dr.FindControl("txtPassFail") as TextBox;
                                TextBox txtPercentage = dr.FindControl("txtPercentage") as TextBox;

                                ddlQualification.SelectedValue = dted.Rows[dr.RowIndex]["Qualification"].ToString();
                                txtEdLevel.Text = dted.Rows[dr.RowIndex]["Description"].ToString();
                                txtNameofSchoolColg.Text = dted.Rows[dr.RowIndex]["NameOfSchoolClg"].ToString();
                                txtBoard.Text = dted.Rows[dr.RowIndex]["BoardorUniversity"].ToString();
                                txtyear.Text = dted.Rows[dr.RowIndex]["YrOfStudy"].ToString();
                                txtPassFail.Text = dted.Rows[dr.RowIndex]["PassOrFail"].ToString();
                                txtPercentage.Text = dted.Rows[dr.RowIndex]["PercentageOfmarks"].ToString();

                            }

                        }
                        else
                        {
                            for (int i = 0; i < GvEducationDetails.Rows.Count; i++)
                            {
                                DropDownList ddlQualification = GvEducationDetails.Rows[i].FindControl("ddlQualification") as DropDownList;
                                TextBox txtEdLevel = GvEducationDetails.Rows[i].FindControl("txtEdLevel") as TextBox;
                                TextBox txtNameofSchoolColg = GvEducationDetails.Rows[i].FindControl("txtNameofSchoolColg") as TextBox;
                                TextBox txtBoard = GvEducationDetails.Rows[i].FindControl("txtBoard") as TextBox;
                                TextBox txtyear = GvEducationDetails.Rows[i].FindControl("txtyear") as TextBox;
                                TextBox txtPassFail = GvEducationDetails.Rows[i].FindControl("txtPassFail") as TextBox;
                                TextBox txtPercentage = GvEducationDetails.Rows[i].FindControl("txtPercentage") as TextBox;

                                txtEdLevel.Text = "";
                                txtPassFail.Text = "";
                                txtPercentage.Text = "";
                                txtyear.Text = "";
                                txtNameofSchoolColg.Text = "";
                                ddlQualification.SelectedIndex = 0;


                            }

                        }


                        string sqlprevExpDetails = "select *,Convert(nvarchar(10),DateofResign,103) as DateofResign1 from EmpPrevExperience where EmpID = '" + empid + "' order by id ";
                        DataTable dtped = config.ExecuteAdaptorAsyncWithQueryParams(sqlprevExpDetails).Result;
                        if (dtped.Rows.Count > 0)
                        {
                            GvPreviousExperience.DataSource = dtped;
                            GvPreviousExperience.DataBind();

                            foreach (GridViewRow dr in GvPreviousExperience.Rows)
                            {
                                if (dtped.Rows.Count == dr.RowIndex)
                                {
                                    break;
                                }

                                TextBox txtregioncode = dr.FindControl("txtregioncode") as TextBox;
                                TextBox txtempcode = dr.FindControl("txtempcode") as TextBox;
                                TextBox txtExtension = dr.FindControl("txtExtension") as TextBox;
                                TextBox txtPrevDesignation = dr.FindControl("txtPrevDesignation") as TextBox;
                                TextBox txtCompAddress = dr.FindControl("txtCompAddress") as TextBox;
                                TextBox txtyearofexp = dr.FindControl("txtyearofexp") as TextBox;
                                TextBox txtPFNo = dr.FindControl("txtPFNo") as TextBox;
                                TextBox txtESINo = dr.FindControl("txtESINo") as TextBox;
                                TextBox txtDtofResigned = dr.FindControl("txtDtofResigned") as TextBox;


                                txtregioncode.Text = dtped.Rows[dr.RowIndex]["RegionCode"].ToString();
                                txtempcode.Text = dtped.Rows[dr.RowIndex]["EmployerCode"].ToString();
                                txtExtension.Text = dtped.Rows[dr.RowIndex]["Extension"].ToString();
                                txtPrevDesignation.Text = dtped.Rows[dr.RowIndex]["Designation"].ToString();
                                txtCompAddress.Text = dtped.Rows[dr.RowIndex]["CompAddress"].ToString();
                                txtyearofexp.Text = dtped.Rows[dr.RowIndex]["YrOfExp"].ToString();
                                txtPFNo.Text = dtped.Rows[dr.RowIndex]["PFNo"].ToString();
                                txtESINo.Text = dtped.Rows[dr.RowIndex]["ESINo"].ToString();
                                txtDtofResigned.Text = dtped.Rows[dr.RowIndex]["DateofResign1"].ToString();
                                if (txtDtofResigned.Text == "01/01/1900")
                                {
                                    txtDtofResigned.Text = "";
                                }

                            }

                        }
                        else
                        {
                            for (int i = 0; i < GvPreviousExperience.Rows.Count; i++)
                            {
                                TextBox txtregioncode = GvPreviousExperience.Rows[i].FindControl("txtregioncode") as TextBox;
                                TextBox txtempcode = GvPreviousExperience.Rows[i].FindControl("txtempcode") as TextBox;
                                TextBox txtExtension = GvPreviousExperience.Rows[i].FindControl("txtExtension") as TextBox;
                                TextBox txtPrevDesignation = GvPreviousExperience.Rows[i].FindControl("txtPrevDesignation") as TextBox;
                                TextBox txtCompAddress = GvPreviousExperience.Rows[i].FindControl("txtCompAddress") as TextBox;
                                TextBox txtyearofexp = GvPreviousExperience.Rows[i].FindControl("txtyearofexp") as TextBox;
                                TextBox txtPFNo = GvPreviousExperience.Rows[i].FindControl("txtPFNo") as TextBox;
                                TextBox txtESINo = GvPreviousExperience.Rows[i].FindControl("txtESINo") as TextBox;
                                TextBox txtDtofResigned = GvPreviousExperience.Rows[i].FindControl("txtDtofResigned") as TextBox;

                                txtregioncode.Text = "";
                                txtDtofResigned.Text = "";
                                txtESINo.Text = "";
                                txtPFNo.Text = "";
                                txtyearofexp.Text = "";
                                txtCompAddress.Text = "";
                                txtPrevDesignation.Text = "";
                                txtempcode.Text = "";



                            }

                        }


                    }

                }
            }
            catch (Exception ex)
            {


            }
        }

        protected void txoldempid_TextChanged(object sender, EventArgs e)
        {

        }



        protected void ddlpreStates_SelectedIndexChanged1(object sender, EventArgs e)
        {
            string query = "select CityId,City from cities where state='" + ddlpreStates.SelectedValue + "' order by City";
            DataTable dt = config.ExecuteAdaptorAsyncWithQueryParams(query).Result;

            if (dt.Rows.Count > 0)
            {
                ddlpreCity.Enabled = true;
                ddlpreCity.DataValueField = "CityId";
                ddlpreCity.DataTextField = "City";
                ddlpreCity.DataSource = dt;
                ddlpreCity.DataBind();
                ddlpreCity.Items.Insert(0, new ListItem("-Select-", "0"));

            }
            else
            {
                ddlpreCity.Items.Insert(0, new ListItem("-Select-", "0"));
            }

        }

        protected void chkSame_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSame.Checked == true)
            {
                //txtPermanentAddress.Text = txtPresentAddress.Text;
                txtmobile9.Text = txtmobile.Text;
                txtpeTaluka.Text = txtprtaluka.Text;
                txtpePoliceStattion.Text = txtprPoliceStation.Text;
                txtpevillage.Text = txtprvillage.Text;
                txtpeLandmark.Text = txtprLandmark.Text;
                txtpePostOffice.Text = txtprPostOffice.Text;
                txtpePin.Text = txtprpin.Text;
                txtPeriodofStay.Text = txtprPeriodofStay.Text;
                txtResidingDate.Text = txtprResidingDate.Text;

                string Statequery = "select StateID,State from States where StateID='" + ddlpreStates.SelectedValue + "' order by State";
                DataTable dt = config.ExecuteAdaptorAsyncWithQueryParams(Statequery).Result;
                //write code here for fetching data
                if (dt.Rows.Count > 0)
                {
                    DdlStates.DataValueField = "StateId";
                    DdlStates.DataTextField = "State";
                    DdlStates.DataSource = dt;
                    DdlStates.DataBind();

                }


                string Cityquery = "select CityID,City from Cities where CityID='" + ddlpreCity.SelectedValue + "' order by city";
                DataTable dtCity = config.ExecuteAdaptorAsyncWithQueryParams(Cityquery).Result;
                //write code here for fetching data
                if (dtCity.Rows.Count > 0)
                {
                    ddlcity.DataValueField = "CityID";
                    ddlcity.DataTextField = "City";
                    ddlcity.DataSource = dtCity;
                    ddlcity.DataBind();
                }

            }
            else
            {
                //txtPermanentAddress.Text = "";
                txtmobile9.Text = "";
                txtpevillage.Text = "";
                txtpeTaluka.Text = "";
                txtpePoliceStattion.Text = "";
                txtpePostOffice.Text = "";
                txtpePin.Text = "";
                txtResidingDate.Text = "";
                txtPeriodofStay.Text = "";
                txtpeLandmark.Text = "";

                DataTable DtStateNames = GlobalData.Instance.LoadStateNames();
                if (DtStateNames.Rows.Count > 0)
                {
                    DdlStates.DataValueField = "StateId";
                    DdlStates.DataTextField = "State";
                    DdlStates.DataSource = DtStateNames;
                    DdlStates.DataBind();
                    DdlStates.Items.Insert(0, new ListItem("--Select--", "0"));
                }

                ddlcity.Items.Clear();


            }
        }
        protected void DdlStates_SelectedIndexChanged(object sender, EventArgs e)
        {
            string query = "select CityId,City from cities where state='" + DdlStates.SelectedValue + "' order by City";
            DataTable dt = config.ExecuteAdaptorAsyncWithQueryParams(query).Result;
            if (dt.Rows.Count > 0)
            {
                ddlcity.Enabled = true;
                ddlcity.DataValueField = "CityId";
                ddlcity.DataTextField = "City";
                ddlcity.DataSource = dt;
                ddlcity.DataBind();
                ddlcity.Items.Insert(0, new ListItem("--Select--", "0"));

            }
            else
            {
                ddlcity.Items.Insert(0, new ListItem("--Select--", "0"));
            }
        }
        protected void rdbResigned_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbResigned.Checked == true)
            {
                txtDofleaving.Enabled = true;
            }
            else
            {
                txtDofleaving.Enabled = false;
            }
        }
        protected void ChkAadharCard_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkAadharCard.Checked == true)
            {
                txtAadharCard.Enabled = true;
                txtAadharName.Enabled = true;

            }
            else
            {
                txtAadharCard.Enabled = false;
                txtAadharName.Enabled = false;
            }
        }
        protected void ChkPanCard_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkPanCard.Checked == true)
            {
                txtPanCard.Enabled = true;
                txtPanCardName.Enabled = true;
            }
            else
            {
                txtPanCard.Enabled = false;
                txtPanCardName.Enabled = false;

            }

        }
        protected void ChkdrivingLicense_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkdrivingLicense.Checked == true)
            {
                txtDrivingLicense.Enabled = true;
                txtDrivingLicenseName.Enabled = true;

            }
            else
            {
                txtDrivingLicense.Enabled = false;
                txtDrivingLicenseName.Enabled = false;

            }


        }
        protected void ChkBankPassbook_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkBankPassbook.Checked == true)
            {
                txtBankPassbook.Enabled = true;
                txtBankPassBookName.Enabled = true;

            }
            else
            {
                txtBankPassbook.Enabled = false;
                txtBankPassBookName.Enabled = false;

            }

        }
        protected void ChkVoterID_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkVoterID.Checked == true)
            {
                txtVoterID.Enabled = true;
                txtVoterName.Enabled = true;
            }
            else
            {
                txtVoterID.Enabled = false;
                txtVoterName.Enabled = false;

            }

        }
        protected void ChkElectricityBill_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkElectricityBill.Checked == true)
            {
                txtElectricityBill.Enabled = true;
                txtElecBillname.Enabled = true;

            }
            else
            {
                txtElectricityBill.Enabled = false;
                txtElecBillname.Enabled = false;

            }
        }
        protected void ChkRationCard_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkRationCard.Checked == true)
            {
                txtRationCard.Enabled = true;
                txtRationCardName.Enabled = true;

            }
            else
            {
                txtRationCard.Enabled = false;
                txtRationCardName.Enabled = false;

            }
        }
        protected void Chkother_CheckedChanged(object sender, EventArgs e)
        {
            if (Chkother.Checked == true)
            {
                txtOther.Enabled = true;
                txtOtherName.Enabled = true;
            }
            else
            {
                txtOther.Enabled = false;
                txtOtherName.Enabled = false;

            }

        }

        protected void ChkESICCard_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkESICCard.Checked == true)
            {
                txtESICCardNo.Enabled = true;
                txtESICName.Enabled = true;
            }
            else
            {
                txtESICCardNo.Enabled = false;
                txtESICName.Enabled = false;

            }
        }

        protected void ChkCriminalOff_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkCriminalOff.Checked == true)
            {
                txtCriminalOff.Enabled = true;
                txtCriminalOffcaseNo.Enabled = true;
                txtCriminalOffCName.Enabled = true;

            }
            else
            {
                txtCriminalOff.Enabled = false;
                txtCriminalOffcaseNo.Enabled = false;
                txtCriminalOffCName.Enabled = false;
            }
        }

        protected void ChkCrimalArrest_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkCrimalArrest.Checked == true)
            {
                txtCriminalArrestCaseNo.Enabled = true;
                txtCriminalArrestCName.Enabled = true;
                txtCriminalArrestOffence.Enabled = true;

            }
            else
            {
                txtCriminalArrestCaseNo.Enabled = false;
                txtCriminalArrestCName.Enabled = false;
                txtCriminalArrestOffence.Enabled = false;

            }
        }

        protected void ChkCriminalProc_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkCriminalProc.Checked == true)
            {
                txtCriminalProCaseNo.Enabled = true;
                txtCriminalProCName.Enabled = true;
                txtCriminalProOffence.Enabled = true;

            }
            else
            {
                txtCriminalProCaseNo.Enabled = false;
                txtCriminalProCName.Enabled = false;
                txtCriminalProOffence.Enabled = false;


            }
        }

        protected void rdbactive_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbactive.Checked == true)
            {
                txtDofleaving.Enabled = false;
            }

        }

        protected void rdbNotVerified_CheckedChanged(object sender, EventArgs e)
        {
            txtPoliceVerificationNo.Enabled = false;

        }
        protected void rdbVerified_CheckedChanged(object sender, EventArgs e)
        {
            txtPoliceVerificationNo.Enabled = true;

        }

        protected void ddlTitle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTitle.SelectedIndex == 0)
            {
                Rdb_Male.Checked = true;
            }
            else if (ddlTitle.SelectedIndex == 1)
            {
                Rdb_Female.Checked = true;
            }
            else
            {
                Rdb_Female.Checked = true;
            }
        }

        protected void btnadd_Click(object sender, EventArgs e)
        {
            AddNewRowToGrid();
        }

        protected void ddlShift_SelectedIndexChanged(object sender, EventArgs e)
        {
            string query = "select ShiftStartTime,ShiftEndTime from shifts where shift='" + ddlShift.SelectedValue + "'";
            DataTable dt = config.ExecuteAdaptorAsyncWithQueryParams(query).Result;
            if (dt.Rows.Count > 0)
            {
                txtShiftstarttime.Text = dt.Rows[0]["ShiftStartTime"].ToString();
                txtShiftEndtime.Text = dt.Rows[0]["ShiftEndTime"].ToString();
            }
        }


        private void AddNewRowToGrid()
        {

            int rowIndex = 0;

            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                DataRow drCurrentRow = null;
                //DataRow drCurrentRow1 = null;


                if (dtCurrentTable.Rows.Count > 0)
                {


                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {
                        //extract the TextBox values
                        //TextBox lblSno = (TextBox)gvFamilyDetails.Rows[rowIndex].Cells[0].FindControl("lblSno");
                        TextBox txtEmpName = (TextBox)gvFamilyDetails.Rows[rowIndex].Cells[1].FindControl("txtEmpName");
                        TextBox txtRelDtofBirth = (TextBox)gvFamilyDetails.Rows[rowIndex].Cells[2].FindControl("txtRelDtofBirth");
                        TextBox txtAge = (TextBox)gvFamilyDetails.Rows[rowIndex].Cells[3].FindControl("txtAge");
                        DropDownList ddlRelation = (DropDownList)gvFamilyDetails.Rows[rowIndex].Cells[4].FindControl("ddlRelation");
                        TextBox txtReloccupation = (TextBox)gvFamilyDetails.Rows[rowIndex].Cells[5].FindControl("txtReloccupation");
                        TextBox txtRelphnno = (TextBox)gvFamilyDetails.Rows[rowIndex].Cells[6].FindControl("txtRelphnno");
                        TextBox txtRelmail = (TextBox)gvFamilyDetails.Rows[rowIndex].Cells[7].FindControl("txtRelmail");
                        CheckBox ChkPFNominee = (CheckBox)gvFamilyDetails.Rows[rowIndex].Cells[8].FindControl("ChkPFNominee");
                        CheckBox ChkESINominee = (CheckBox)gvFamilyDetails.Rows[rowIndex].Cells[9].FindControl("ChkESINominee");
                        DropDownList ddlresidence = (DropDownList)gvFamilyDetails.Rows[rowIndex].Cells[10].FindControl("ddlresidence");
                        TextBox txtplace = (TextBox)gvFamilyDetails.Rows[rowIndex].Cells[11].FindControl("txtplace");


                        drCurrentRow = dtCurrentTable.NewRow();

                        var PFNominee = "N";
                        var ESINominee = "N";


                        //dtCurrentTable.Rows[i - 1]["Region"] = lblSno.Text;
                        dtCurrentTable.Rows[i - 1]["RName"] = txtEmpName.Text;
                        dtCurrentTable.Rows[i - 1]["DOfBirth"] = txtRelDtofBirth.Text;
                        dtCurrentTable.Rows[i - 1]["age"] = txtAge.Text;
                        dtCurrentTable.Rows[i - 1]["RType"] = ddlRelation.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["ROccupation"] = txtReloccupation.Text;
                        dtCurrentTable.Rows[i - 1]["RPhone"] = txtRelphnno.Text;
                        dtCurrentTable.Rows[i - 1]["RGMail"] = txtRelmail.Text;
                        dtCurrentTable.Rows[i - 1]["RResidence"] = ddlresidence.SelectedValue;

                        if (ChkPFNominee.Checked == true)
                        {
                            PFNominee = "Y";
                        }
                        if (ChkESINominee.Checked == true)
                        {
                            ESINominee = "Y";
                        }

                        dtCurrentTable.Rows[i - 1]["PFNominee"] = PFNominee;
                        dtCurrentTable.Rows[i - 1]["ESINominee"] = ESINominee;
                        dtCurrentTable.Rows[i - 1]["RPlace"] = txtplace.Text;

                        rowIndex++;

                    }
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["CurrentTable"] = dtCurrentTable;
                    gvFamilyDetails.DataSource = dtCurrentTable;
                    gvFamilyDetails.DataBind();
                }
            }
            else
            {
                Response.Write("ViewState is null");
            }

            //Set Previous Data on Postbacks
            SetPreviousData();
        }

        private void SetPreviousData()
        {
            int rowIndex = 0;
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable"];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < gvFamilyDetails.Rows.Count; i++)
                    {

                        TextBox txtEmpName = (TextBox)gvFamilyDetails.Rows[rowIndex].Cells[1].FindControl("txtEmpName");
                        TextBox txtRelDtofBirth = (TextBox)gvFamilyDetails.Rows[rowIndex].Cells[2].FindControl("txtRelDtofBirth");
                        TextBox txtAge = (TextBox)gvFamilyDetails.Rows[rowIndex].Cells[3].FindControl("txtAge");
                        DropDownList ddlRelation = (DropDownList)gvFamilyDetails.Rows[rowIndex].Cells[4].FindControl("ddlRelation");
                        TextBox txtReloccupation = (TextBox)gvFamilyDetails.Rows[rowIndex].Cells[5].FindControl("txtReloccupation");
                        TextBox txtrelphone = (TextBox)gvFamilyDetails.Rows[rowIndex].Cells[6].FindControl("txtRelphnno");
                        TextBox txtrelmail = (TextBox)gvFamilyDetails.Rows[rowIndex].Cells[7].FindControl("txtRelmail");
                        CheckBox ChkPFNominee = (CheckBox)gvFamilyDetails.Rows[rowIndex].Cells[8].FindControl("ChkPFNominee");
                        CheckBox ChkESINominee = (CheckBox)gvFamilyDetails.Rows[rowIndex].Cells[9].FindControl("ChkESINominee");
                        DropDownList ddlresidence = (DropDownList)gvFamilyDetails.Rows[rowIndex].Cells[10].FindControl("ddlresidence");

                        var PFNominee = "N";
                        PFNominee = dt.Rows[i]["PFNominee"].ToString();
                        if (PFNominee == "Y")
                        {
                            ChkPFNominee.Checked = true;
                        }
                        else
                        {
                            ChkPFNominee.Checked = false;
                        }

                        var ESINominee = "Y";
                        ESINominee = dt.Rows[i]["ESINominee"].ToString();
                        if (ESINominee == "Y")
                        {
                            ChkESINominee.Checked = true;
                        }
                        else
                        {
                            ChkESINominee.Checked = false;
                        }
                        TextBox txtplace = (TextBox)gvFamilyDetails.Rows[rowIndex].Cells[11].FindControl("txtplace");


                        txtEmpName.Text = dt.Rows[i]["RName"].ToString();
                        txtRelDtofBirth.Text = dt.Rows[i]["DOfBirth"].ToString();
                        txtAge.Text = dt.Rows[i]["age"].ToString();
                        ddlRelation.SelectedValue = dt.Rows[i]["RType"].ToString();
                        txtReloccupation.Text = dt.Rows[i]["ROccupation"].ToString();
                        txtrelphone.Text = dt.Rows[i]["RPhone"].ToString();
                        ddlresidence.SelectedValue = dt.Rows[i]["RResidence"].ToString();
                        txtplace.Text = dt.Rows[i]["RPlace"].ToString();


                        rowIndex++;
                    }
                }
            }
        }

        protected void btnEduAdd_Click(object sender, EventArgs e)
        {
            AddEduNewRowToGrid();
        }

        private void AddEduNewRowToGrid()
        {

            int rowIndex = 0;

            if (ViewState["EducationTable"] != null)
            {
                DataTable dtEducationTable = (DataTable)ViewState["EducationTable"];
                DataRow drEducationRow = null;
                //DataRow drCurrentRow1 = null;


                if (dtEducationTable.Rows.Count > 0)
                {


                    for (int i = 1; i <= dtEducationTable.Rows.Count; i++)
                    {
                        //extract the TextBox values
                        //TextBox lblSno = (TextBox)gvFamilyDetails.Rows[rowIndex].Cells[0].FindControl("lblSno");
                        DropDownList ddlQualification = (DropDownList)GvEducationDetails.Rows[rowIndex].Cells[1].FindControl("ddlQualification");
                        TextBox txtEdLevel = (TextBox)GvEducationDetails.Rows[rowIndex].Cells[2].FindControl("txtEdLevel");
                        TextBox txtNameofSchoolColg = (TextBox)GvEducationDetails.Rows[rowIndex].Cells[3].FindControl("txtNameofSchoolColg");
                        TextBox txtBoard = (TextBox)GvEducationDetails.Rows[rowIndex].Cells[4].FindControl("txtBoard");
                        TextBox txtyear = (TextBox)GvEducationDetails.Rows[rowIndex].Cells[5].FindControl("txtyear");
                        TextBox txtPassFail = (TextBox)GvEducationDetails.Rows[rowIndex].Cells[6].FindControl("txtPassFail");
                        TextBox txtPercentage = (TextBox)GvEducationDetails.Rows[rowIndex].Cells[7].FindControl("txtPercentage");


                        drEducationRow = dtEducationTable.NewRow();


                        //dtCurrentTable.Rows[i - 1]["Region"] = lblSno.Text;
                        dtEducationTable.Rows[i - 1]["Qualification"] = ddlQualification.SelectedValue;
                        dtEducationTable.Rows[i - 1]["Description"] = txtEdLevel.Text;
                        dtEducationTable.Rows[i - 1]["NameOfSchoolClg"] = txtNameofSchoolColg.Text;
                        dtEducationTable.Rows[i - 1]["BoardorUniversity"] = txtBoard.Text;
                        dtEducationTable.Rows[i - 1]["YrOfStudy"] = txtyear.Text;
                        dtEducationTable.Rows[i - 1]["PassOrFail"] = txtPassFail.Text;
                        dtEducationTable.Rows[i - 1]["PercentageOfmarks"] = txtPercentage.Text;
                        rowIndex++;


                    }
                    dtEducationTable.Rows.Add(drEducationRow);
                    ViewState["EducationTable"] = dtEducationTable;
                    GvEducationDetails.DataSource = dtEducationTable;
                    GvEducationDetails.DataBind();
                }
            }
            else
            {
                Response.Write("ViewState is null");
            }

            //Set Previous Data on Postbacks
            SetEduPreviousData();
        }

        private void SetEduPreviousData()
        {
            int rowIndex = 0;
            if (ViewState["EducationTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["EducationTable"];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < GvEducationDetails.Rows.Count; i++)
                    {

                        DropDownList ddlQualification = (DropDownList)GvEducationDetails.Rows[rowIndex].Cells[1].FindControl("ddlQualification");
                        TextBox txtEdLevel = (TextBox)GvEducationDetails.Rows[rowIndex].Cells[2].FindControl("txtEdLevel");
                        TextBox txtNameofSchoolColg = (TextBox)GvEducationDetails.Rows[rowIndex].Cells[3].FindControl("txtNameofSchoolColg");
                        TextBox txtBoard = (TextBox)GvEducationDetails.Rows[rowIndex].Cells[4].FindControl("txtBoard");
                        TextBox txtyear = (TextBox)GvEducationDetails.Rows[rowIndex].Cells[5].FindControl("txtyear");
                        TextBox txtPassFail = (TextBox)GvEducationDetails.Rows[rowIndex].Cells[6].FindControl("txtPassFail");
                        TextBox txtPercentage = (TextBox)GvEducationDetails.Rows[rowIndex].Cells[7].FindControl("txtPercentage");


                        ddlQualification.SelectedValue = dt.Rows[i]["Qualification"].ToString();
                        txtEdLevel.Text = dt.Rows[i]["Description"].ToString();
                        txtNameofSchoolColg.Text = dt.Rows[i]["NameOfSchoolClg"].ToString();
                        txtBoard.Text = dt.Rows[i]["BoardorUniversity"].ToString();
                        txtyear.Text = dt.Rows[i]["YrOfStudy"].ToString();
                        txtPassFail.Text = dt.Rows[i]["PassOrFail"].ToString();
                        txtPercentage.Text = dt.Rows[i]["PercentageOfmarks"].ToString();


                        rowIndex++;
                    }
                }
            }
        }

        protected void btnPrevExpAdd_Click(object sender, EventArgs e)
        {
            AddPrevExpNewRowToGrid();
        }

        private void AddPrevExpNewRowToGrid()
        {

            int rowIndex = 0;

            if (ViewState["PrevExpTable"] != null)
            {
                DataTable dtPrevExpTable = (DataTable)ViewState["PrevExpTable"];
                DataRow drPrevExpTableRow = null;
                //DataRow drCurrentRow1 = null;


                if (dtPrevExpTable.Rows.Count > 0)
                {


                    for (int i = 1; i <= dtPrevExpTable.Rows.Count; i++)
                    {
                        //extract the TextBox values
                        //TextBox lblSno = (TextBox)gvFamilyDetails.Rows[rowIndex].Cells[0].FindControl("lblSno");
                        TextBox txtregioncode = (TextBox)GvPreviousExperience.Rows[rowIndex].Cells[1].FindControl("txtregioncode");
                        TextBox txtempcode = (TextBox)GvPreviousExperience.Rows[rowIndex].Cells[2].FindControl("txtempcode");
                        TextBox txtExtension = (TextBox)GvPreviousExperience.Rows[rowIndex].Cells[3].FindControl("txtExtension");
                        TextBox txtPrevDesignation = (TextBox)GvPreviousExperience.Rows[rowIndex].Cells[4].FindControl("txtPrevDesignation");
                        TextBox txtCompAddress = (TextBox)GvPreviousExperience.Rows[rowIndex].Cells[5].FindControl("txtCompAddress");
                        TextBox txtyearofexp = (TextBox)GvPreviousExperience.Rows[rowIndex].Cells[6].FindControl("txtyearofexp");
                        TextBox txtPFNo = (TextBox)GvPreviousExperience.Rows[rowIndex].Cells[7].FindControl("txtPFNo");
                        TextBox txtESINo = (TextBox)GvPreviousExperience.Rows[rowIndex].Cells[8].FindControl("txtESINo");
                        TextBox txtDtofResigned = (TextBox)GvPreviousExperience.Rows[rowIndex].Cells[9].FindControl("txtDtofResigned");


                        drPrevExpTableRow = dtPrevExpTable.NewRow();


                        //dtCurrentTable.Rows[i - 1]["Region"] = lblSno.Text;
                        dtPrevExpTable.Rows[i - 1]["RegionCode"] = txtregioncode.Text;
                        dtPrevExpTable.Rows[i - 1]["EmployerCode"] = txtempcode.Text;
                        dtPrevExpTable.Rows[i - 1]["Extension"] = txtExtension.Text;
                        dtPrevExpTable.Rows[i - 1]["Designation"] = txtPrevDesignation.Text;
                        dtPrevExpTable.Rows[i - 1]["CompAddress"] = txtCompAddress.Text;
                        dtPrevExpTable.Rows[i - 1]["YrOfExp"] = txtyearofexp.Text;
                        dtPrevExpTable.Rows[i - 1]["PFNo"] = txtPFNo.Text;
                        dtPrevExpTable.Rows[i - 1]["ESINo"] = txtESINo.Text;
                        dtPrevExpTable.Rows[i - 1]["DateofResign1"] = txtDtofResigned.Text;

                        rowIndex++;



                    }
                    dtPrevExpTable.Rows.Add(drPrevExpTableRow);
                    ViewState["PrevExpTable"] = dtPrevExpTable;
                    GvPreviousExperience.DataSource = dtPrevExpTable;
                    GvPreviousExperience.DataBind();
                }
            }
            else
            {
                Response.Write("ViewState is null");
            }

            //Set Previous Data on Postbacks
            SetPrevExpPreviousData();
        }

        private void SetPrevExpPreviousData()
        {
            int rowIndex = 0;
            if (ViewState["PrevExpTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["PrevExpTable"];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < GvPreviousExperience.Rows.Count; i++)
                    {


                        TextBox txtregioncode = (TextBox)GvPreviousExperience.Rows[rowIndex].Cells[1].FindControl("txtregioncode");
                        TextBox txtempcode = (TextBox)GvPreviousExperience.Rows[rowIndex].Cells[2].FindControl("txtempcode");
                        TextBox txtExtension = (TextBox)GvPreviousExperience.Rows[rowIndex].Cells[3].FindControl("txtExtension");
                        TextBox txtPrevDesignation = (TextBox)GvPreviousExperience.Rows[rowIndex].Cells[4].FindControl("txtPrevDesignation");
                        TextBox txtCompAddress = (TextBox)GvPreviousExperience.Rows[rowIndex].Cells[5].FindControl("txtCompAddress");
                        TextBox txtyearofexp = (TextBox)GvPreviousExperience.Rows[rowIndex].Cells[6].FindControl("txtyearofexp");
                        TextBox txtPFNo = (TextBox)GvPreviousExperience.Rows[rowIndex].Cells[7].FindControl("txtPFNo");
                        TextBox txtESINo = (TextBox)GvPreviousExperience.Rows[rowIndex].Cells[8].FindControl("txtESINo");
                        TextBox txtDtofResigned = (TextBox)GvPreviousExperience.Rows[rowIndex].Cells[9].FindControl("txtDtofResigned");


                        txtregioncode.Text = dt.Rows[i]["RegionCode"].ToString();
                        txtempcode.Text = dt.Rows[i]["EmployerCode"].ToString();
                        txtExtension.Text = dt.Rows[i]["Extension"].ToString();
                        txtPrevDesignation.Text = dt.Rows[i]["Designation"].ToString();
                        txtCompAddress.Text = dt.Rows[i]["CompAddress"].ToString();
                        txtyearofexp.Text = dt.Rows[i]["YrOfExp"].ToString();
                        txtPFNo.Text = dt.Rows[i]["PFNo"].ToString();
                        txtESINo.Text = dt.Rows[i]["ESINo"].ToString();
                        txtDtofResigned.Text = dt.Rows[i]["DateofResign1"].ToString();


                        rowIndex++;
                    }
                }
            }
        }


        protected void TxtIDCardIssuedDt_TextChanged(object sender, EventArgs e)
        {
            DateTime dt = DateTime.ParseExact(TxtIDCardIssuedDt.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime end = dt.AddYears(1).AddDays(-1);
            TxtIdCardValid.Text = end.ToString("dd/MM/yyyy");
        }

        protected void Btnedit_Click(object sender, EventArgs e)
        {

            PnlEmployeeInfo.Enabled = true;
            Pnlpersonal.Enabled = true;
            pnlimages.Enabled = true;
            pnlphysicalstandard2.Enabled = true;
            pnlphysicalstandard.Enabled = true;
            PnlPFDetails.Enabled = true;
            PnlBankDetails.Enabled = true;
            PnlESIDetails.Enabled = true;
            PnlSalaryDetails.Enabled = true;
            PnlProofsSubmitted.Enabled = true;
            PnlExService.Enabled = true;
            pnlfamilydetails.Enabled = true;
            pnlEducationDetails.Enabled = true;
            pnlPreviousExpereince.Enabled = true;
            PnlPaySheet.Enabled = true;
            pnlGroupBox.Enabled = true;
            PnlCriminalProceeding.Enabled = true;
            Btn_Save_Personal_Tab.Visible = true;
            Btn_Cancel_Personal_Tab.Visible = true;
            btnNext.Visible = true;
            Btnedit.Visible = false;

        }

        protected void txtEmpDtofJoining_TextChanged(object sender, EventArgs e)
        {
            TxtIDCardIssuedDt.Text = txtEmpDtofJoining.Text;
            DateTime dt = DateTime.ParseExact(TxtIDCardIssuedDt.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime end = dt.AddYears(1).AddDays(-1);
            TxtIdCardValid.Text = end.ToString("dd/MM/yyyy");
        }
        protected void RbnNotsubmitted_CheckedChanged(object sender, EventArgs e)
        {
            if (RbnNotsubmitted.Checked == true)
            {
                RbnOriginal.Visible = false;
                RbnXerox.Visible = false;
            }
        }

        protected void Rbnsubmitted_CheckedChanged(object sender, EventArgs e)
        {
            if (Rbnsubmitted.Checked == true)
            {
                RbnOriginal.Visible = true;
                RbnXerox.Visible = true;
            }

        }

        protected void RbnNotsubmitted1_CheckedChanged(object sender, EventArgs e)
        {
            if (RbnNotsubmitted1.Checked == true)
            {
                RbnOriginal1.Visible = false;
                RbnXerox1.Visible = false;
            }
        }

        protected void Rbnsubmitted1_CheckedChanged(object sender, EventArgs e)
        {
            if (Rbnsubmitted1.Checked == true)
            {
                RbnOriginal1.Visible = true;
                RbnXerox1.Visible = true;
            }
        }

        protected void RbnNotsubmitted2_CheckedChanged(object sender, EventArgs e)
        {
            if (RbnNotsubmitted2.Checked == true)
            {
                RbnOriginal2.Visible = false;
                RbnXerox2.Visible = false;
            }
        }

        protected void Rbnsubmitted2_CheckedChanged(object sender, EventArgs e)
        {
            if (Rbnsubmitted2.Checked == true)
            {
                RbnOriginal2.Visible = true;
                RbnXerox2.Visible = true;
            }
        }

        protected void RbnNotsubmitted3_CheckedChanged(object sender, EventArgs e)
        {
            if (RbnNotsubmitted3.Checked == true)
            {
                RbnOriginal3.Visible = false;
                RbnXerox3.Visible = false;
            }
        }

        protected void Rbnsubmitted3_CheckedChanged(object sender, EventArgs e)
        {
            if (Rbnsubmitted3.Checked == true)
            {
                RbnOriginal3.Visible = true;
                RbnXerox3.Visible = true;
            }
        }

        protected void RbnNotsubmitted4_CheckedChanged(object sender, EventArgs e)
        {
            if (RbnNotsubmitted4.Checked == true)
            {
                RbnOriginal4.Visible = false;
                RbnXerox4.Visible = false;
            }
        }

        protected void Rbnsubmitted4_CheckedChanged(object sender, EventArgs e)
        {
            if (Rbnsubmitted4.Checked == true)
            {
                RbnOriginal4.Visible = true;
                RbnXerox4.Visible = true;
            }
        }

        protected void RbnNotsubmitted5_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void Rbnsubmitted5_CheckedChanged(object sender, EventArgs e)
        {
            if (Rbnsubmitted5.Checked == true)
            {
                RbnOriginal5.Visible = true;
                RbnXerox5.Visible = true;
            }
        }

        protected void RbnNotsubmitted6_CheckedChanged(object sender, EventArgs e)
        {
            if (RbnNotsubmitted6.Checked == true)
            {
                RbnOriginal6.Visible = false;
                RbnXerox6.Visible = false;
            }
        }

        protected void Rbnsubmitted6_CheckedChanged(object sender, EventArgs e)
        {
            if (Rbnsubmitted6.Checked == true)
            {
                RbnOriginal6.Visible = true;
                RbnXerox6.Visible = true;
            }
        }

        protected void RbnNotsubmitted7_CheckedChanged(object sender, EventArgs e)
        {
            if (RbnNotsubmitted7.Checked == true)
            {
                RbnOriginal7.Visible = false;
                RbnXerox7.Visible = false;
            }
        }

        protected void Rbnsubmitted7_CheckedChanged(object sender, EventArgs e)
        {
            if (Rbnsubmitted7.Checked == true)
            {
                RbnOriginal7.Visible = true;
                RbnXerox7.Visible = true;
            }
        }

        protected void RbnNotsubmitted8_CheckedChanged(object sender, EventArgs e)
        {
            if (RbnNotsubmitted8.Checked == true)
            {
                RbnOriginal8.Visible = false;
                RbnXerox8.Visible = false;
            }
        }

        protected void Rbnsubmitted8_CheckedChanged(object sender, EventArgs e)
        {
            if (Rbnsubmitted8.Checked == true)
            {
                RbnOriginal8.Visible = true;
                RbnXerox8.Visible = true;
            }
        }

        protected void ChkSSC_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkSSC.Checked == true)
            {
                txtsscno.Enabled = true;

            }
            else
            {
                txtsscno.Enabled = false;

            }
        }

        protected void RbnNotSubmitted9_CheckedChanged(object sender, EventArgs e)
        {
            if (RbnNotSubmitted9.Checked == true)
            {
                RbnOriginal9.Visible = false;
                RbnXerox9.Visible = false;
            }
        }

        protected void RbnSubmitted9_CheckedChanged(object sender, EventArgs e)
        {
            if (RbnSubmitted9.Checked == true)
            {
                RbnOriginal9.Visible = true;
                RbnXerox9.Visible = true;
            }
        }
    }
}