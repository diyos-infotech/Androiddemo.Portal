using System;
using System.Data;
using KLTS.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Jawan.Portal
{
    public partial class M_Add_Lead : System.Web.UI.Page
    {
        DataTable dt;
        string CmpIDPrefix = "";
        string Username = "";

        Marketinghelper MH = new Marketinghelper();

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
                    LoadStatenames();
                    LoadSegments();
                    leadids();
                    LoadBranch();
                    LoadsourceofLead();
                    BindGridview();
                    BindGridviewForExist();
                    LoadStatus();
                    LoadLeadGenratedby();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Your Session Expired.Please Login');", true);
                Response.Redirect("~/Login.aspx");
            }
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

        protected void LoadsourceofLead()
        {

            string sourcequery = "select * from SourceofLeads";

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

        protected void LoadLeadGenratedby()
        {

            string sourcequery = "select Empid,(Empid+'-'+EmpFName) Name  from empdetails";

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

        protected void LoadStatenames()
        {
            string statequery = "select * from States";
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

        protected void LoadStatus()
        {
            string queryStatus = "select * from M_LeadStatusMaster";
            DataTable Dtstatus = SqlHelper.Instance.GetTableByQuery(queryStatus);
            if (Dtstatus.Rows.Count > 0)
            {
                ddlStatus.DataValueField = "Id";
                ddlStatus.DataTextField = "LeadStatus";
                ddlStatus.DataSource = Dtstatus;
                ddlStatus.DataBind();
            }
            ddlStatus.Items.Insert(0, new ListItem("-Select-", "0"));
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

        private void ClearleadsFieldsData()
        {

            txtleadname.Text = "";
            txtownername.Text = "";
            txtLeadGeneratedOn.Text = "";
            ddlsegment.SelectedIndex = 0;
            ddlSource.SelectedIndex = 0;
            ddlRegion.SelectedIndex = 0;
            ddlpreStates.SelectedIndex = 0;
            ddlBranch.SelectedIndex = 0;
            txtexistvendor.Text = "";
            txtRemarks.Text = "";
            ddlpreCity.SelectedIndex = 0;
            txtAddress.Text = string.Empty;
            txtexpecteddate.Text = string.Empty;
            ddlStatus.SelectedIndex = 0;
            BindGridview();
            BindGridviewForExist();
        }



        protected void ddlpreStates_SelectedIndexChanged1(object sender, EventArgs e)
        {
            string query = "select CityId,City from cities where state='" + ddlpreStates.SelectedValue + "' order by City";
            DataTable dt = MH.ExecuteAdaptorAsyncWithQueryParams(query);
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



        protected void Imgadd_Click(object sender, ImageClickEventArgs e)
        {
            Gvaddnewrow();
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
                        txtHours.Text= dt.Rows[i]["Hours"].ToString();
                        txtStrength.Text = dt.Rows[i]["Strength"].ToString();


                        rowIndex++;
                    }
                }
            }
        }


        protected void Gvaddnewrow()
        {

            int rowIndex = 0;

            if (ViewState["LeadsTable1"] != null)
            {
                DataTable dtleadTable1 = (DataTable)ViewState["LeadsTable1"];
                DataRow drleadRow1 = null;

                if (dtleadTable1.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtleadTable1.Rows.Count; i++)
                    {
                        TextBox txtname = (TextBox)GVleads.Rows[rowIndex].Cells[1].FindControl("txtname");
                        TextBox txtdesgn = (TextBox)GVleads.Rows[rowIndex].Cells[2].FindControl("txtdesgn");
                        TextBox txtmobile = (TextBox)GVleads.Rows[rowIndex].Cells[3].FindControl("txtmobile");
                        TextBox txtemail = (TextBox)GVleads.Rows[rowIndex].Cells[4].FindControl("txtemail");

                        drleadRow1 = dtleadTable1.NewRow();

                        drleadRow1["RowNumber"] = i + 1;
                        dtleadTable1.Rows[i - 1]["Name"] = txtname.Text;
                        dtleadTable1.Rows[i - 1]["Designation"] = txtdesgn.Text;
                        dtleadTable1.Rows[i - 1]["Mobile"] = txtmobile.Text;
                        dtleadTable1.Rows[i - 1]["Email"] = txtemail.Text;

                        rowIndex++;


                    }
                    dtleadTable1.Rows.Add(drleadRow1);
                    ViewState["LeadsTable1"] = dtleadTable1;
                    GVleads.DataSource = dtleadTable1;
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

        public void AddLeadContactDetails()
        {

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

                    string linksave = "insert into M_LeadContactDetails(ID,LeadID,Name,Designation,MobileNo,Email,Created_By,Created_On) values('" + lblSno + "','" + txtleadId.Text + "','" + leadname + "','" + desgnation + "','" + mobile + "','" + Email + "','" + Username + "','" + DateTime.Now + "')";
                    int dtlead = SqlHelper.Instance.ExecuteDMLQry(linksave);
                    txtname.Text = "";
                    txtdesgn.Text = "";
                    txtmobile.Text = "";
                    txtemail.Text = "";
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

            string Quatation = "0"; string designation = ""; string Salary = "0";
            string Strength = "0"; string Hours = "0";

            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                string lblSno = GridView1.Rows[i].Cells[0].Text;
                TextBox txtQuatation = GridView1.Rows[i].FindControl("txtQuatation") as TextBox;
                TextBox txtdesgn = GridView1.Rows[i].FindControl("txtdesgn") as TextBox;
                TextBox txtsalary = GridView1.Rows[i].FindControl("txtsalary") as TextBox;
                TextBox txtHours = GridView1.Rows[i].FindControl("txtHours") as TextBox;
                TextBox txtStrength = GridView1.Rows[i].FindControl("txtStrength") as TextBox;

                if (txtQuatation.Text != string.Empty || txtdesgn.Text != string.Empty || txtsalary.Text != string.Empty|| txtHours.Text != string.Empty || txtStrength.Text != string.Empty)
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

                    if (txtStrength.Text.Length > 0)
                    {
                        Strength = txtStrength.Text;
                    }
                    else
                    {
                        Strength = "0";
                    }

                    if (txtHours.Text.Length > 0)
                    {
                        Hours = txtHours.Text;
                    }
                    else
                    {
                        Hours = "0";
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

        private void leadids()
        {
            CmpIDPrefix = "LE";

            txtleadId.Text = MH.LoadMaxleadid(CmpIDPrefix);
        }

        protected void BindGridview()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
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
            dt.Columns.Add("Designation",typeof(string));
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

        protected void btnsavedetails_Click(object sender, EventArgs e)
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
                var LeadGeneratedBy = "";
                string SourceofHead = "0";
                string LeadGeneratedOn = "";
                string adress = "";
                int Branch = 0;
                string ExistingVendorName = "";
                string Remarks = "";
                leadId = txtleadId.Text;

                leadname = txtleadname.Text;
                leadowner = txtownername.Text;
                adress = txtAddress.Text;
                if (txtleadname.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Select LeadName')", true);
                    return;
                }
                //if (txtownername.Text == "")
                //{
                //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Select OwnerName')", true);
                //    return;
                //}
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

                var ExpectedDate = "01/01/1900";

                if (txtexpecteddate.Text.Trim().Length != 0)
                {
                    ExpectedDate = Timings.Instance.CheckDateFormat(txtexpecteddate.Text);
                }

                if (txtLeadGeneratedOn.Text.Trim().Length != 0)
                {
                    LeadGeneratedOn = Timings.Instance.CheckDateFormat(txtLeadGeneratedOn.Text);
                }

                Region = ddlRegion.SelectedIndex;

                if (ddlLeadGenratedby.SelectedIndex == 0)
                {
                    LeadGeneratedBy = "0";
                }
                else
                {
                    LeadGeneratedBy = ddlLeadGenratedby.SelectedValue;
                }

                if (ddlSource.SelectedIndex == 0)
                {
                    SourceofHead = "0";
                }
                else
                {
                    SourceofHead = ddlSource.SelectedValue;
                }

                if(ddlBranch.SelectedIndex==0)
                {
                    Branch = 0;
                }

                else
                {
                    Branch =Convert.ToInt32(ddlBranch.SelectedValue);
                }

                string Leadstatus = ddlStatus.SelectedValue;
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

                DateTime statusDate = DateTime.Now;

                int dtlead = 0;
                string linksave = "insert into M_Leads(LeadID,LeadName,LeadOwner,Segment,State,City,Region,Sourceofhead,LeadStatus,Address,Statusdate,ExpectedDate,Created_On,Created_By,LeadGeneratedOn,LeadGeneratedBy,Branch,Remarks,ExistingVendorName) values('" + leadId + "','" + leadname + "','" + leadowner + "','" + leadSegment + "','" + state + "','" + City + "','" + Region + "','" + SourceofHead + "','" + Leadstatus + "','" + adress + "','" + statusDate + "','" + statusDate + "','" + DateTime.Now + "','" + Username + "','" + LeadGeneratedOn + "','" + LeadGeneratedBy + "','" + Branch + "','" + Remarks + "','" + ExistingVendorName + "')";
                dtlead = SqlHelper.Instance.ExecuteDMLQry(linksave);

                AddLeadContactDetails();
                AddLeadStatusDetails();
                AddLeadQuatationDetails();

                if (dtlead > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Lead Details are added sucessfully with Lead ID :- " + txtleadId.Text + " ')", true);
                    leadids();
                    ClearleadsFieldsData();
                    return;
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Lead Details are not added sucessfully with Lead ID :- " + txtleadId.Text + " ')", true);
                    return;
                }

            }
            catch (Exception ex)
            {
            }
        }



        protected void AddLeadStatusDetails()
        {
            string leadid = txtleadId.Text;
            int status = int.Parse(ddlStatus.SelectedValue);
            DateTime Createdon = DateTime.Now;

            string linksave = "insert into M_LeadStatusDetails(LeadId,Status,CreatedOn,CreatedBy) values('" + leadid + "','" + status + "','" + Createdon + "','" + Username + "')";
            int dtLeadStatusDetails = SqlHelper.Instance.ExecuteDMLQry(linksave);


        }
    }
}