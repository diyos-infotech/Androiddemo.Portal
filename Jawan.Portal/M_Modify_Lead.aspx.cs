using System;
using System.Data;
using KLTS.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Jawan.Portal
{
    public partial class M_Modify_Lead : System.Web.UI.Page
    {
        Marketinghelper MH = new Marketinghelper();
        string CmpIDPrefix = "";
        string Username = "";
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {

                CmpIDPrefix = Session["CmpIDPrefix"].ToString();
                Username = Session["UserId"].ToString();

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
                    Loadsourceofhead();
                    LoadStatenames();
                    LoadSegments();
                    LoadStatus();
                    LoadStatenames();
                    LoadBranch();
                    BindGridview();
                    BindGridviewForExist();
                    LoadLeadGenratedby();
                    if (Request.QueryString["Leadid"] != null)
                    {

                        string Leadid = Request.QueryString["Leadid"].ToString();
                        LoadOldData(Leadid);

                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Your Session Expired.Please Login');", true);
               // Response.Redirect("~/Login.aspx");
            }
        }
        protected void LoadLeadGenratedby()
        {

            string sourcequery = "select Empid,(Empid+'-'+EmpFName) Name  from empdetails";

           // DataTable dtsourceofhead = SqlHelper.Instance.GetTableByQuery(sourcequery);
            DataTable dtsourceofhead = MH.ExecuteAdaptorAsyncWithQueryParams(sourcequery);
            if (dtsourceofhead.Rows.Count > 0)
            {
                ddlLeadGenratedby.DataValueField = "Empid";
                ddlLeadGenratedby.DataTextField = "Name";
                ddlLeadGenratedby.DataSource = dtsourceofhead;
                ddlLeadGenratedby.DataBind();
            }
            ddlLeadGenratedby.Items.Insert(0, "-Select-");
        }
        protected void LoadOldData(string Leadid)
        {
            string segmaent = "";
            string State = "";
            string CIty = "";
            string adress = "";
            string status = "";
            string sourceofhead = "";
            string Branch = "";
            string ExistingVendorName = "";
            string Remarks = "";


            string Sqlleads = "select  case convert(nvarchar(20),LeadGeneratedOn,103) when '01/01/1900' then '' else convert(nvarchar(20),LeadGeneratedOn,103) end LeadGenerated,case convert(nvarchar(20),ExpectedDate,103) when '01/01/1900' then '' else convert(nvarchar(20),ExpectedDate,103) end LeadExpectedDate,* from M_Leads where leadid = '" + Leadid + "'";

           

            DataTable dtleads = SqlHelper.Instance.GetTableByQuery(Sqlleads);
            if (dtleads.Rows.Count > 0)
            {
                txtleadId.Text = Leadid;
                txtownername.Text = dtleads.Rows[0]["LeadOwner"].ToString();
                txtleadname.Text = dtleads.Rows[0]["LeadName"].ToString();
                segmaent = dtleads.Rows[0]["Segment"].ToString();
                adress = dtleads.Rows[0]["Address"].ToString();

                if (dtleads.Rows[0]["LeadGeneratedBy"].ToString() != "0")
                {
                    ddlLeadGenratedby.SelectedValue = dtleads.Rows[0]["LeadGeneratedBy"].ToString();
                }
                else
                {
                    ddlLeadGenratedby.SelectedIndex = 0;
                }

                txtAddress.Text = adress;
                if (segmaent != "0")
                {
                    ddlsegment.SelectedValue = dtleads.Rows[0]["Segment"].ToString();
                }
                else
                {
                    ddlsegment.SelectedIndex = 0;
                }

                State = dtleads.Rows[0]["State"].ToString();
                if (State != "0")
                {
                    ddlpreStates.SelectedValue = (State);
                }
                else
                {
                    ddlpreStates.SelectedIndex = 0;
                }

                CIty = dtleads.Rows[0]["City"].ToString();
                if (CIty != "0")
                {
                    string query = "select CityId,City from cities where state='" + ddlpreStates.SelectedValue + "' order by City";
                    DataTable dt = SqlHelper.Instance.GetTableByQuery(query);
                    if (dt.Rows.Count > 0)
                    {
                        ddlpreCity.Enabled = true;
                        ddlpreCity.DataValueField = "CityId";
                        ddlpreCity.DataTextField = "City";
                        ddlpreCity.DataSource = dt;
                        ddlpreCity.DataBind();
                    }
                    ddlpreCity.Items.Insert(0, new ListItem("--Select--", "0"));


                    ddlpreCity.SelectedValue = dtleads.Rows[0]["City"].ToString();
                }
                else
                {
                    ddlpreCity.SelectedIndex = 0;
                }

                Branch = dtleads.Rows[0]["Branch"].ToString();
                if (Branch != "0")
                {
                    ddlBranch.SelectedValue = (Branch);
                }
                else
                {
                    ddlBranch.SelectedIndex = 0;
                }

                status = dtleads.Rows[0]["LeadStatus"].ToString();
                if (status != "0")
                {
                    ddlStatus.SelectedValue = dtleads.Rows[0]["LeadStatus"].ToString();
                }
                else
                {
                    ddlStatus.SelectedIndex = 0;
                }

                sourceofhead = dtleads.Rows[0]["SourceofHead"].ToString();
                if (sourceofhead != "0")
                {
                    ddlSource.SelectedValue = dtleads.Rows[0]["SourceofHead"].ToString();
                }
                else
                {
                    ddlSource.SelectedIndex = 0;
                }


                if (String.IsNullOrEmpty(dtleads.Rows[0]["LeadGenerated"].ToString()) == false)
                {

                    txtLeadGeneratedOn.Text = (dtleads.Rows[0]["LeadGenerated"].ToString());
                    if (txtLeadGeneratedOn.Text == "01/01/1900")
                    {
                        txtLeadGeneratedOn.Text = "";
                    }
                }
                else
                {
                    txtLeadGeneratedOn.Text = "";
                }

                if (String.IsNullOrEmpty(dtleads.Rows[0]["LeadExpectedDate"].ToString()) == false)
                {

                    txtexpecteddate.Text = (dtleads.Rows[0]["LeadExpectedDate"].ToString());
                    if (txtexpecteddate.Text == "01/01/1900")
                    {
                        txtexpecteddate.Text = "";
                    }
                }
                else
                {
                    txtexpecteddate.Text = "";
                }

                txtexistvendor.Text = dtleads.Rows[0]["ExistingVendorName"].ToString();
                txtRemarks.Text= dtleads.Rows[0]["Remarks"].ToString();

                string sqlEducationDetails = "select *,id as RowNumber,MobileNo as Mobile from M_LeadContactDetails where leadid = '" + Leadid + "' order by id ";
                DataTable dted = SqlHelper.Instance.GetTableByQuery(sqlEducationDetails);
                if (dted.Rows.Count > 0)
                {
                    GVleads.DataSource = dted;
                    GVleads.DataBind();

                    foreach (GridViewRow dr in GVleads.Rows)
                    {
                        if (dted.Rows.Count == dr.RowIndex)
                        {
                            break;
                        }

                        TextBox txtname = dr.FindControl("txtname") as TextBox;
                        TextBox txtdesgn = dr.FindControl("txtdesgn") as TextBox;
                        TextBox txtmobile = dr.FindControl("txtmobile") as TextBox;
                        TextBox txtemail = dr.FindControl("txtemail") as TextBox;


                        txtname.Text = dted.Rows[dr.RowIndex]["Name"].ToString();
                        txtdesgn.Text = dted.Rows[dr.RowIndex]["Designation"].ToString();
                        txtmobile.Text = dted.Rows[dr.RowIndex]["Mobile"].ToString();
                        txtemail.Text = dted.Rows[dr.RowIndex]["Email"].ToString();

                    }

                }
                else
                {
                    for (int i = 0; i < GVleads.Rows.Count; i++)
                    {

                        TextBox txtname = GVleads.Rows[i].FindControl("txtname") as TextBox;
                        TextBox txtdesgn = GVleads.Rows[i].FindControl("txtdesgn") as TextBox;
                        TextBox txtmobile = GVleads.Rows[i].FindControl("txtmobile") as TextBox;
                        TextBox txtemail = GVleads.Rows[i].FindControl("txtemail") as TextBox;

                        txtname.Text = "";
                        txtdesgn.Text = "";
                        txtmobile.Text = "";
                        txtemail.Text = "";

                    }

                }

                string sqlQuatationDetails = "select *,id as RowNumber from M_LeadQuatationDetails where leadid = '" + Leadid + "' order by id ";
                DataTable dtQuatation = SqlHelper.Instance.GetTableByQuery(sqlQuatationDetails);
                if (dtQuatation.Rows.Count > 0)
                {
                    GridView1.DataSource = dtQuatation;
                    GridView1.DataBind();

                    foreach (GridViewRow dr in GridView1.Rows)
                    {
                        if (dtQuatation.Rows.Count == dr.RowIndex)
                        {
                            break;
                        }

                        TextBox txtQuatation = dr.FindControl("txtQuatation") as TextBox;
                        TextBox txtdesgn = dr.FindControl("txtdesgn") as TextBox;
                        TextBox txtsalary = dr.FindControl("txtsalary") as TextBox;
                        TextBox txtHours = dr.FindControl("txtHours") as TextBox;
                        TextBox txtStrength = dr.FindControl("txtStrength") as TextBox;


                        txtQuatation.Text = dtQuatation.Rows[dr.RowIndex]["Quatation"].ToString();
                        txtdesgn.Text = dtQuatation.Rows[dr.RowIndex]["Designation"].ToString();
                        txtsalary.Text = dtQuatation.Rows[dr.RowIndex]["Salary"].ToString();
                        txtHours.Text = dtQuatation.Rows[dr.RowIndex]["Hours"].ToString();
                        txtStrength.Text = dtQuatation.Rows[dr.RowIndex]["Strength"].ToString();

                    }

                }
                else
                {
                    for (int i = 0; i < GridView1.Rows.Count; i++)
                    {

                        TextBox txtQuatation = GridView1.Rows[i].FindControl("txtQuatation") as TextBox;
                        TextBox txtdesgn = GridView1.Rows[i].FindControl("txtdesgn") as TextBox;
                        TextBox txtsalary = GridView1.Rows[i].FindControl("txtsalary") as TextBox;
                        TextBox txtHours = GridView1.Rows[i].FindControl("txtHours") as TextBox;
                        TextBox txtStrength = GridView1.Rows[i].FindControl("txtStrength") as TextBox;

                        txtQuatation.Text = "";
                        txtdesgn.Text = "";
                        txtsalary.Text = "";
                        txtStrength.Text = "";
                        txtHours.Text = "";

                    }

                }

                ViewState["LeadsTable1"] = dted;
                ViewState["LeadsTable11"] = dtQuatation;
            }
        }

        protected void LoadStatus()
        {
            string queryStatus = "select * from M_LeadStatusMaster";
           // DataTable Dtstatus = SqlHelper.Instance.GetTableByQuery(queryStatus);
            DataTable Dtstatus = MH.ExecuteAdaptorAsyncWithQueryParams(queryStatus);
            if (Dtstatus.Rows.Count > 0)
            {
                ddlStatus.DataValueField = "Id";
                ddlStatus.DataTextField = "LeadStatus";
                ddlStatus.DataSource = Dtstatus;
                ddlStatus.DataBind();
            }
            ddlStatus.Items.Insert(0, new ListItem("-Select-", "0"));
        }

        protected void LoadSegments()
        {
            DataTable DtSegments = GlobalData.Instance.LoadSegments();
            if (DtSegments.Rows.Count > 0)
            {
                ddlsegment.DataValueField = "segid";
                ddlsegment.DataTextField = "segname";
                ddlsegment.DataSource = DtSegments;
                ddlsegment.DataBind();
            }
            ddlsegment.Items.Insert(0, "-Select-");
        }

        protected void LoadBranch()
        {
            string query = "select * from BranchDetails";

            DataTable dtOldEmployeeIds = MH.ExecuteAdaptorAsyncWithQueryParams(query);
            if (dtOldEmployeeIds.Rows.Count > 0)
            {
                ddlBranch.DataValueField = "Branchid";
                ddlBranch.DataTextField = "BranchName";
                ddlBranch.DataSource = dtOldEmployeeIds;
                ddlBranch.DataBind();
            }

            ddlBranch.Items.Insert(0, new ListItem("-Select-", "0"));

        }

        protected void Loadsourceofhead()
        {

            string sourcequery = "select * from SourceofLeads";
            //DataTable dtsourceofhead = SqlHelper.Instance.GetTableByQuery(sourcequery);
            DataTable dtsourceofhead = MH.ExecuteAdaptorAsyncWithQueryParams(sourcequery);
            if (dtsourceofhead.Rows.Count > 0)
            {
                ddlSource.DataValueField = "SourceId";
                ddlSource.DataTextField = "SourceofLead";
                ddlSource.DataSource = dtsourceofhead;
                ddlSource.DataBind();
            }
            ddlSource.Items.Insert(0, "-Select-");
        }
        protected void PreviligeUsers(int previligerid)
        {
            switch (previligerid)
            {
                case 1:
                    break;
                case 2:
                    //AddClientLink.Visible = false;
                    //ModifyClientLink.Visible = false;
                    //DeleteClientLink.Visible = false;


                    ClientsLink.Visible = true;
                    CompanyInfoLink.Visible = false;
                    InventoryLink.Visible = false;
                    ReportsLink.Visible = true;
                    SettingsLink.Visible = true;
                    Response.Redirect("clientattendance.aspx");
                    break;

                case 3:
                    //AddClientLink.Visible = false;
                    //ModifyClientLink.Visible = false;
                    //DeleteClientLink.Visible = false;


                    ClientsLink.Visible = true;
                    CompanyInfoLink.Visible = false;
                    InventoryLink.Visible = false;
                    ReportsLink.Visible = true;
                    SettingsLink.Visible = true;
                    Response.Redirect("clientattendance.aspx");
                    break;
                case 4:

                    //AddClientLink.Visible = true;
                    //ModifyClientLink.Visible = true;
                    //DeleteClientLink.Visible = true;

                    ClientsLink.Visible = true;
                    CompanyInfoLink.Visible = true;
                    InventoryLink.Visible = true;
                    ReportsLink.Visible = true;
                    SettingsLink.Visible = true;
                    break;
                case 5:

                    //AddClientLink.Visible = false;
                    //ModifyClientLink.Visible = true;
                    //DeleteClientLink.Visible = false;

                    ClientsLink.Visible = true;
                    CompanyInfoLink.Visible = false;
                    InventoryLink.Visible = false;
                    ReportsLink.Visible = true;
                    ReportsLink.Visible = true;
                    SettingsLink.Visible = true;
                    Response.Redirect("ModifyClient.aspx");
                    break;
                case 6:
                    //AddClientLink.Visible = false;
                    //ModifyClientLink.Visible = false;
                    //DeleteClientLink.Visible = false;

                    CompanyInfoLink.Visible = false;
                    InventoryLink.Visible = false;
                    ReportsLink.Visible = false;
                    SettingsLink.Visible = false;
                    Response.Redirect("MaterialRequisitForm.aspx");

                    break;
                default:
                    break;


            }
        }

        protected void btnaddclint_Click(object sender, EventArgs e)
        {
            try
            {
                string leadId = "";
                string leadname = "";
                string leadowner = "";
                string leadSegment = "";
                string state = "";
                string City = "";
                int Region = 0;
                int Branch = 0;
                string ExistingVendorName = "";
                string Remarks = "";
                string SourceofHead = "0";
                string LeadGeneratedOn = "";
                string Leadstatus = "0";
                string LeadGeneratedBy = "0";
                string ExpectedDate = "";
                leadId = txtleadId.Text;
                leadname = txtleadname.Text;
                leadowner = txtownername.Text;
                if (ddlsegment.SelectedIndex == 0)
                {
                    leadSegment = "0";
                }
                else
                {
                    leadSegment = ddlsegment.SelectedValue;
                }


                if (ddlpreStates.SelectedIndex == 0)
                {
                    state = "0";
                }
                else
                {
                    state = ddlpreStates.SelectedValue;
                }


                if (ddlpreCity.SelectedIndex == 0)
                {
                    City = "0";
                }
                else
                {
                    City = ddlpreCity.SelectedValue;
                }

                if (ddlBranch.SelectedIndex == 0)
                {
                    Branch = 0;
                }

                else
                {
                    Branch = Convert.ToInt32(ddlBranch.SelectedValue);
                }

                if (txtLeadGeneratedOn.Text.Trim().Length != 0)
                {
                    LeadGeneratedOn = Timings.Instance.CheckDateFormat(txtLeadGeneratedOn.Text);
                }

                if (txtexpecteddate.Text.Trim().Length != 0)
                {
                    ExpectedDate = Timings.Instance.CheckDateFormat(txtexpecteddate.Text);
                }

                if (ddlLeadGenratedby.SelectedIndex == 0)
                {
                    LeadGeneratedBy = "0";
                }
                else
                {
                    LeadGeneratedBy = ddlLeadGenratedby.SelectedValue;
                }

                Region = ddlRegion.SelectedIndex;


                if (ddlSource.SelectedIndex == 0)
                {
                    SourceofHead = "0";
                }
                else
                {
                    SourceofHead = ddlSource.SelectedValue;
                }


                if (ddlStatus.SelectedIndex == 0)
                {
                    Leadstatus = "0";
                }
                else
                {
                    Leadstatus = ddlStatus.SelectedValue;
                }

                ExistingVendorName = txtexistvendor.Text;
                Remarks = txtRemarks.Text;

                int dtlead = 0;

                string linksave = "update M_Leads set LeadName='" + leadname + "',Segment='" + leadSegment + "',Address='" + txtAddress.Text + "',State='" + state + "',City='" + City + "',ExpectedDate='" + ExpectedDate + "'" +
                   ",Region='" + Region + "',Sourceofhead='" + SourceofHead + "',LeadGeneratedOn='" + LeadGeneratedOn + "',Leadstatus='" + Leadstatus + "',LeadGeneratedBy='" + LeadGeneratedBy + "',Branch='" + Branch + "',ExistingVendorName='" + ExistingVendorName + "',Remarks='" + Remarks + "' where leadid='" + leadId + "'";
                dtlead = SqlHelper.Instance.ExecuteDMLQry(linksave);

                AddLeadContactDetails();
                AddLeadQuatationDetails();

                if (dtlead > 0)
                {
                    lblMsg.Text = "Lead Details are updated sucessfully for Lead ID  :- " + txtleadId.Text + " ";
                }
                else
                {
                    lblMsg.Text = "Lead Details not updated sucessfully for Lead ID :- " + txtleadId.Text + " -: ";

                }


            }
            catch (Exception ex)
            {
            }
        }

        private void ClearleadsFieldsData()
        {
            //txtleadId.Text = "";
            txtleadname.Text = "";
            txtownername.Text = "";
            txtLeadGeneratedOn.Text = "";
            ddlsegment.SelectedIndex = 0;
            ddlSource.SelectedIndex = 0;
            ddlRegion.SelectedIndex = 0;
            ddlpreStates.SelectedIndex = 0;
            ddlpreCity.SelectedIndex = 0;
            ddlBranch.SelectedIndex = 0;
            txtexistvendor.Text = "";
            txtRemarks.Text = "";
        }

        protected void Imgaddexist_Click(object sender, ImageClickEventArgs e)
        {
            GvaddnewrowForExist();
        }

        protected void GvaddnewrowForExist()
        {

            int rowIndex = 0;

            if (ViewState["LeadsTable11"] != null)
            {
                DataTable dtleadTable = (DataTable)ViewState["LeadsTable11"];
                DataRow drleadRow = null;

                if (dtleadTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtleadTable.Rows.Count; i++)
                    {
                        TextBox txtdesgn = (TextBox)GridView1.Rows[rowIndex].Cells[1].FindControl("txtdesgn");
                        TextBox txtQuatation = (TextBox)GridView1.Rows[rowIndex].Cells[2].FindControl("txtQuatation");
                        TextBox txtsalary = (TextBox)GridView1.Rows[rowIndex].Cells[3].FindControl("txtsalary");
                        TextBox txtHours = (TextBox)GridView1.Rows[rowIndex].Cells[4].FindControl("txtHours");
                        TextBox txtStrength = (TextBox)GridView1.Rows[rowIndex].Cells[5].FindControl("txtStrength");

                        drleadRow = dtleadTable.NewRow();

                        drleadRow["RowNumber"] = i + 1;
                        dtleadTable.Rows[i - 1]["Designation"] = txtdesgn.Text;
                        dtleadTable.Rows[i - 1]["Quatation"] = txtQuatation.Text;
                        dtleadTable.Rows[i - 1]["Salary"] = txtsalary.Text;
                        dtleadTable.Rows[i - 1]["Hours"] = txtHours.Text;
                        dtleadTable.Rows[i - 1]["Strength"] = txtStrength.Text;

                        rowIndex++;


                    }
                    dtleadTable.Rows.Add(drleadRow);
                    ViewState["LeadsTable11"] = dtleadTable;
                    GridView1.DataSource = dtleadTable;
                    GridView1.DataBind();
                }
            }
            else
            {
                Response.Write("ViewState is null");
            }

            //Set Previous Data on Postbacks
            SetleadPreviousDataForExist();

        }

        private void SetleadPreviousDataForExist()
        {

            int rowIndex = 0;
            if (ViewState["LeadsTable11"] != null)
            {
                DataTable dt = (DataTable)ViewState["LeadsTable11"];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < GridView1.Rows.Count; i++)
                    {
                        TextBox txtdesgn = (TextBox)GridView1.Rows[rowIndex].Cells[1].FindControl("txtdesgn");
                        TextBox txtQuatation = (TextBox)GridView1.Rows[rowIndex].Cells[2].FindControl("txtQuatation");
                        TextBox txtsalary = (TextBox)GridView1.Rows[rowIndex].Cells[3].FindControl("txtsalary");
                        TextBox txtHours = (TextBox)GridView1.Rows[rowIndex].Cells[4].FindControl("txtHours");
                        TextBox txtStrength = (TextBox)GridView1.Rows[rowIndex].Cells[5].FindControl("txtStrength");

                        txtdesgn.Text = dt.Rows[i]["Designation"].ToString();
                        txtQuatation.Text = dt.Rows[i]["Quatation"].ToString();
                        txtsalary.Text = dt.Rows[i]["Salary"].ToString();
                        txtStrength.Text = dt.Rows[i]["Strength"].ToString();
                        txtHours.Text = dt.Rows[i]["Hours"].ToString();

                        rowIndex++;
                    }
                }
            }
        }


        protected void ddlpreStates_SelectedIndexChanged1(object sender, EventArgs e)
        {
            string query = "select CityId,City from cities where state='" + ddlpreStates.SelectedValue + "' order by City";
            DataTable dt = SqlHelper.Instance.GetTableByQuery(query);
            if (dt.Rows.Count > 0)
            {
                ddlpreCity.Enabled = true;
                ddlpreCity.DataValueField = "CityId";
                ddlpreCity.DataTextField = "City";
                ddlpreCity.DataSource = dt;
                ddlpreCity.DataBind();
                ddlpreCity.Items.Insert(0, new ListItem("--Select--", "0"));

            }
            else
            {
                ddlpreCity.Items.Insert(0, new ListItem("--Select--", "0"));
            }

        }

        protected void LoadStatenames()
        {
            string statequery = "select * from States";
           // DataTable DtStateNames = SqlHelper.Instance.GetTableByQuery(statequery);
            DataTable DtStateNames = MH.ExecuteAdaptorAsyncWithQueryParams(statequery);
            if (DtStateNames.Rows.Count > 0)
            {
                ddlpreStates.DataValueField = "StateID";
                ddlpreStates.DataTextField = "State";
                ddlpreStates.DataSource = DtStateNames;
                ddlpreStates.DataBind();
            }
            ddlpreStates.Items.Insert(0, new ListItem("-Select-", "0"));
        }


        protected void BindGridview()
        {

            DataTable dt = new DataTable();
            dt.Columns.Add("RowNumber", typeof(string));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Designation", typeof(string));
            dt.Columns.Add("Mobile", typeof(string));
            dt.Columns.Add("Email", typeof(string));

            DataRow dr = dt.NewRow();

            dr["RowNumber"] = "1";
            dr["Name"] = string.Empty;
            dr["Designation"] = string.Empty;
            dr["Mobile"] = string.Empty;
            dr["Email"] = string.Empty;
            dt.Rows.Add(dr);
            ViewState["LeadsTable1"] = dt;
            GVleads.DataSource = dt;
            GVleads.DataBind();
        }

        protected void BindGridviewForExist()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
            dt.Columns.Add("Designation", typeof(string));
            dt.Columns.Add("Quatation", typeof(string));
            dt.Columns.Add("Salary", typeof(string));
            dt.Columns.Add("Hours", typeof(string));
            dt.Columns.Add("Strength", typeof(string));

            DataRow dr = dt.NewRow();

            dr["RowNumber"] = "1";
            dr["Designation"] = string.Empty;
            dr["Quatation"] = string.Empty;
            dr["Salary"] = string.Empty;
            dr["Hours"] = string.Empty;
            dr["Strength"] = string.Empty;
            dt.Rows.Add(dr);
            ViewState["LeadsTable11"] = dt;
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        public void AddLeadContactDetails()
        {

            string SqlDelete = "Delete M_LeadContactDetails where Leadid='" + txtleadId.Text + "'";
            SqlHelper.Instance.ExecuteDMLQry(SqlDelete);
            string leadname = ""; string desgnation = ""; string mobile = "";
            string Email = "";

            for (int i = 0; i < GVleads.Rows.Count; i++)
            {
                string lblSno = GVleads.Rows[i].Cells[0].Text;
                TextBox txtname = GVleads.Rows[i].FindControl("txtname") as TextBox;
                TextBox txtdesgn = GVleads.Rows[i].FindControl("txtdesgn") as TextBox;
                TextBox txtmobile = GVleads.Rows[i].FindControl("txtmobile") as TextBox;
                TextBox txtemail = GVleads.Rows[i].FindControl("txtemail") as TextBox;

                if (txtname.Text != string.Empty || txtdesgn.Text != string.Empty || txtmobile.Text != string.Empty || txtemail.Text != string.Empty)
                {
                    if (txtname.Text.Length > 0)
                    {
                        leadname = txtname.Text;
                    }

                    if (txtdesgn.Text.Length > 0)
                    {
                        desgnation = txtdesgn.Text;
                    }

                    if (txtmobile.Text.Length > 0)
                    {
                        mobile = txtmobile.Text;
                    }

                    if (txtemail.Text.Length > 0)
                    {
                        Email = txtemail.Text;
                    }

                    string linksave = "insert into M_LeadContactDetails(ID,LeadID,Name,Designation,MobileNo,Email) values('" + lblSno + "','" + txtleadId.Text + "','" + leadname + "','" + desgnation + "','" + mobile + "','" + Email + "')";
                    int dtlead = SqlHelper.Instance.ExecuteDMLQry(linksave);

                }
                else
                {
                    txtname.Text = "";
                    txtdesgn.Text = "";
                    txtmobile.Text = "";
                    txtemail.Text = "";
                }
            }




        }

        public void AddLeadQuatationDetails()
        {

            string SqlDelete = "Delete M_LeadQuatationDetails where Leadid='" + txtleadId.Text + "'";
            SqlHelper.Instance.ExecuteDMLQry(SqlDelete);
            string Quatation = "0"; string designation = ""; string Salary = "0";
            string Strength = "0";string Hours = "0";

            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                string lblSno = GridView1.Rows[i].Cells[0].Text;
                TextBox txtQuatation = GridView1.Rows[i].FindControl("txtQuatation") as TextBox;
                TextBox txtdesgn = GridView1.Rows[i].FindControl("txtdesgn") as TextBox;
                TextBox txtsalary = GridView1.Rows[i].FindControl("txtsalary") as TextBox;
                TextBox txtHours = GridView1.Rows[i].FindControl("txtHours") as TextBox;
                TextBox txtStrength = GridView1.Rows[i].FindControl("txtStrength") as TextBox;

                if (txtQuatation.Text != string.Empty || txtdesgn.Text != string.Empty || txtsalary.Text != string.Empty || txtHours.Text != string.Empty || txtStrength.Text != string.Empty)
                {
                    if (txtQuatation.Text.Length > 0)
                    {
                        Quatation = txtQuatation.Text;
                    }

                    else
                    {
                        Quatation = "0";
                    }

                    if (txtdesgn.Text.Length > 0)
                    {
                        designation = txtdesgn.Text;
                    }
                    else
                    {
                        designation = "";
                    }

                    if (txtsalary.Text.Length > 0)
                    {
                        Salary = txtsalary.Text;
                    }
                    else
                    {
                        Salary = "0";
                    }

                    if (txtHours.Text.Length > 0)
                    {
                        Hours = txtHours.Text;
                    }
                    else
                    {
                        Hours = "0";
                    }

                    if (txtStrength.Text.Length > 0)
                    {
                        Strength = txtStrength.Text;
                    }
                    else
                    {
                        Strength = "0";
                    }

                    string linksave = "insert into M_LeadQuatationDetails(ID,LeadID,Quatation,Designation,Salary,Hours,Strength,Created_By,Created_On) values('" + lblSno + "','" + txtleadId.Text + "','" + Quatation + "','" + designation + "','" + Salary + "','" + Hours + "','" + Strength + "','" + Username + "','" + DateTime.Now + "')";
                    int dtlead = SqlHelper.Instance.ExecuteDMLQry(linksave);
                    txtQuatation.Text = "";
                    txtdesgn.Text = "";
                    txtsalary.Text = "";
                    txtStrength.Text = "";
                    txtHours.Text = "";
                }
                else
                {
                    txtQuatation.Text = "";
                    txtdesgn.Text = "";
                    txtsalary.Text = "";
                    txtStrength.Text = "";
                    txtHours.Text = "";
                }
            }




        }

        protected void Imgadd_Click(object sender, ImageClickEventArgs e)
        {
            Gvaddnewrow();
        }

        protected void Gvaddnewrow()
        {
            try
            {

                int rowIndex = 0;

                if (ViewState["LeadsTable1"] != null)
                {
                    DataTable dtleadTable = (DataTable)ViewState["LeadsTable1"];
                    DataRow drleadRow = null;

                    if (dtleadTable.Rows.Count > 0)
                    {
                        for (int i = 1; i <= dtleadTable.Rows.Count; i++)
                        {
                            TextBox txtname = (TextBox)GVleads.Rows[rowIndex].Cells[1].FindControl("txtname");
                            TextBox txtdesgn = (TextBox)GVleads.Rows[rowIndex].Cells[2].FindControl("txtdesgn");
                            TextBox txtmobile = (TextBox)GVleads.Rows[rowIndex].Cells[3].FindControl("txtmobile");
                            TextBox txtemail = (TextBox)GVleads.Rows[rowIndex].Cells[4].FindControl("txtemail");

                            drleadRow = dtleadTable.NewRow();

                            drleadRow["RowNumber"] = i + 1;
                            dtleadTable.Rows[i - 1]["Name"] = txtname.Text;
                            dtleadTable.Rows[i - 1]["Designation"] = txtdesgn.Text;
                            dtleadTable.Rows[i - 1]["Mobile"] = txtmobile.Text;
                            dtleadTable.Rows[i - 1]["Email"] = txtemail.Text;

                            rowIndex++;


                        }
                        dtleadTable.Rows.Add(drleadRow);
                        ViewState["LeadsTable1"] = dtleadTable;
                        GVleads.DataSource = dtleadTable;
                        GVleads.DataBind();
                    }
                }
                else
                {
                    Response.Write("ViewState is null");
                }

                //Set Previous Data on Postbacks
                SetleadPreviousData();
            }
            catch (Exception ex)
            {

            }

        }

        private void SetleadPreviousData()
        {

            int rowIndex = 0;
            if (ViewState["LeadsTable1"] != null)
            {
                DataTable dt = (DataTable)ViewState["LeadsTable1"];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < GVleads.Rows.Count; i++)
                    {
                        TextBox txtname = (TextBox)GVleads.Rows[rowIndex].Cells[1].FindControl("txtname");
                        TextBox txtdesgn = (TextBox)GVleads.Rows[rowIndex].Cells[2].FindControl("txtdesgn");
                        TextBox txtmobile = (TextBox)GVleads.Rows[rowIndex].Cells[3].FindControl("txtmobile");
                        TextBox txtemail = (TextBox)GVleads.Rows[rowIndex].Cells[4].FindControl("txtemail");

                        txtname.Text = dt.Rows[i]["Name"].ToString();
                        txtdesgn.Text = dt.Rows[i]["Designation"].ToString();
                        txtmobile.Text = dt.Rows[i]["Mobile"].ToString();
                        txtemail.Text = dt.Rows[i]["Email"].ToString();


                        rowIndex++;
                    }
                }
            }
        }

        private void SetRowData()
        {
            int rowIndex = 0;

            if (ViewState["LeadsTable1"] != null)
            {
                DataTable dt = (DataTable)ViewState["LeadsTable1"];
                DataRow drCurrentRow = null;
                if (dt.Rows.Count > 0)
                {
                    for (int i = 1; i <= dt.Rows.Count; i++)
                    {
                        TextBox txtname = (TextBox)GVleads.Rows[rowIndex].Cells[1].FindControl("txtname");
                        TextBox txtdesgn = (TextBox)GVleads.Rows[rowIndex].Cells[2].FindControl("txtdesgn");
                        TextBox txtmobile = (TextBox)GVleads.Rows[rowIndex].Cells[3].FindControl("txtmobile");
                        TextBox txtemail = (TextBox)GVleads.Rows[rowIndex].Cells[4].FindControl("txtemail");


                        drCurrentRow = dt.NewRow();
                        drCurrentRow["RowNumber"] = i + 1;
                        dt.Rows[i - 1]["Name"] = txtname.Text;
                        dt.Rows[i - 1]["Designation"] = txtdesgn.Text;
                        dt.Rows[i - 1]["Mobile"] = txtmobile.Text;
                        dt.Rows[i - 1]["Email"] = txtemail.Text;
                        rowIndex++;
                    }

                    ViewState["LeadsTable1"] = dt;
                }
            }
            else
            {
                Response.Write("ViewState is null");
            }
        }

        protected void imgdelete_Click(object sender, ImageClickEventArgs e)
        {

            SetRowData();

            ImageButton imgdelete = (ImageButton)sender;
            GridViewRow gvRow = (GridViewRow)imgdelete.NamingContainer;

            if (ViewState["LeadsTable1"] != null)
            {
                DataTable dt = (DataTable)ViewState["LeadsTable1"];
                DataRow drCurrentRow = null;
                int rowIndex = gvRow.RowIndex;
                if (dt.Rows.Count > 1)
                {
                    dt.Rows.Remove(dt.Rows[rowIndex]);
                    drCurrentRow = dt.NewRow();
                    ViewState["LeadsTable1"] = dt;
                    GVleads.DataSource = dt;
                    GVleads.DataBind();

                    for (int i = 0; i < GVleads.Rows.Count - 1; i++)
                    {
                        GVleads.Rows[i].Cells[0].Text = Convert.ToString(i + 1);
                    }
                    SetleadPreviousData();
                }
            }

        }

        private void SetRowDataExist()
        {
            int rowIndex = 0;

            if (ViewState["LeadsTable11"] != null)
            {
                DataTable dt = (DataTable)ViewState["LeadsTable11"];
                DataRow drCurrentRow = null;
                if (dt.Rows.Count > 0)
                {
                    for (int i = 1; i <= dt.Rows.Count; i++)
                    {
                        TextBox txtdesgn = (TextBox)GridView1.Rows[rowIndex].Cells[1].FindControl("txtdesgn");
                        TextBox txtQuatation = (TextBox)GridView1.Rows[rowIndex].Cells[2].FindControl("txtQuatation");
                        TextBox txtsalary = (TextBox)GridView1.Rows[rowIndex].Cells[3].FindControl("txtsalary");
                        TextBox txtHours = (TextBox)GridView1.Rows[rowIndex].Cells[4].FindControl("txtHours");
                        TextBox txtStrength = (TextBox)GridView1.Rows[rowIndex].Cells[5].FindControl("txtStrength");

                        drCurrentRow = dt.NewRow();

                        drCurrentRow["RowNumber"] = i + 1;
                        dt.Rows[i - 1]["Designation"] = txtdesgn.Text;
                        dt.Rows[i - 1]["Quatation"] = txtQuatation.Text;
                        dt.Rows[i - 1]["Salary"] = txtsalary.Text;
                        dt.Rows[i - 1]["Hours"] = txtHours.Text;
                        dt.Rows[i - 1]["Strength"] = txtStrength.Text;

                        rowIndex++;
                    }

                    ViewState["LeadsTable11"] = dt;
                }
            }
            else
            {
                Response.Write("ViewState is null");
            }
        }

        protected void imgdeleteexist_Click(object sender, ImageClickEventArgs e)
        {

            SetRowDataExist();

            ImageButton imgdelete = (ImageButton)sender;
            GridViewRow gvRow = (GridViewRow)imgdelete.NamingContainer;

            if (ViewState["LeadsTable11"] != null)
            {
                DataTable dt = (DataTable)ViewState["LeadsTable11"];
                DataRow drCurrentRow = null;
                int rowIndex = gvRow.RowIndex;
                if (dt.Rows.Count > 1)
                {
                    dt.Rows.Remove(dt.Rows[rowIndex]);
                    drCurrentRow = dt.NewRow();
                    ViewState["LeadsTable11"] = dt;
                    GridView1.DataSource = dt;
                    GridView1.DataBind();

                    for (int i = 0; i < GridView1.Rows.Count - 1; i++)
                    {
                        GridView1.Rows[i].Cells[0].Text = Convert.ToString(i + 1);
                    }
                    SetleadPreviousDataForExist();
                }
            }

        }
    }
}