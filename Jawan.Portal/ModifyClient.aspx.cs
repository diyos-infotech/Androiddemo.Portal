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
using System.Data.SqlClient;
using System.Globalization;
using Jawan.Portal.DAL;
namespace Jawan.Portal
{
    public partial class ModifyClient : System.Web.UI.Page
    {
        DataTable dt;
        string CmpIDPrefix = "";
        string EmpIDPrefix = "";
        string BranchID = "";
        AppConfiguration config = new AppConfiguration();
        GridViewExportUtil gve = new GridViewExportUtil();

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
                    LoadBranch();
                    LoadSegments();
                    LoadDesignations();
                    LoadClients();
                    LoadopmEmpsIDs();
                    LoadClientids();
                    LoadOurGSTNos();
                    //LoadEsibranches();
                    LoadStatenames();
                    LoadShipStatenames();
                    //LoadDivisions();
                    // LoadDepartments();
                    LoadStaffIDs();

                    if (Request.QueryString["clientid"] != null)
                    {
                        string username = Request.QueryString["clientid"].ToString();
                        txtclientid.Text = username;
                        TxtClient_OnTextChanged(sender, e);

                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        protected void LoadBranch()
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
        protected void LoadEsibranches()
        {

            DataTable DtDivision = GlobalData.Instance.LoadEsibranches();
            if (DtDivision.Rows.Count > 0)
            {
                ddlESIBranch.DataValueField = "EsiBranchid";
                ddlESIBranch.DataTextField = "EsiBranchname";
                ddlESIBranch.DataSource = DtDivision;
                ddlESIBranch.DataBind();
            }
            ddlESIBranch.Items.Insert(0, new ListItem("-Select-", "0"));
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

        protected void LoadStatenames()
        {

            DataTable DtStateNames = GlobalData.Instance.LoadStateNames();
            if (DtStateNames.Rows.Count > 0)
            {
                ddlstate.DataValueField = "StateID";
                ddlstate.DataTextField = "State";
                ddlstate.DataSource = DtStateNames;
                ddlstate.DataBind();

                ddlPTState.DataValueField = "StateID";
                ddlPTState.DataTextField = "State";
                ddlPTState.DataSource = DtStateNames;
                ddlPTState.DataBind();


                ddlStateCode.DataValueField = "StateID";
                ddlStateCode.DataTextField = "GSTStateCode";
                ddlStateCode.DataSource = DtStateNames;
                ddlStateCode.DataBind();
            }
            ddlstate.Items.Insert(0, new ListItem("-Select-", "0"));
            ddlStateCode.Items.Insert(0, new ListItem("-Select-", "0"));
            ddlPTState.Items.Insert(0, new ListItem("-Select-", "0"));


        }

        protected void LoadShipStatenames()
        {

            DataTable DtStateNames = GlobalData.Instance.LoadStateNames();
            if (DtStateNames.Rows.Count > 0)
            {
                ddlShipToSate.DataValueField = "StateID";
                ddlShipToSate.DataTextField = "State";
                ddlShipToSate.DataSource = DtStateNames;
                ddlShipToSate.DataBind();


                ddlShipToStateCode.DataValueField = "StateID";
                ddlShipToStateCode.DataTextField = "GSTStateCode";
                ddlShipToStateCode.DataSource = DtStateNames;
                ddlShipToStateCode.DataBind();
            }
            ddlShipToSate.Items.Insert(0, new ListItem("-Select-", "0"));
            ddlShipToStateCode.Items.Insert(0, new ListItem("-Select-", "0"));

        }

        protected void LoadStaffIDs()
        {
            DataTable DtopmEmpsIDs = GlobalData.Instance.LoadStaffIDs();
            if (DtopmEmpsIDs.Rows.Count > 0)
            {
                ddlstaffid.DataValueField = "EmpId";
                ddlstaffid.DataTextField = "FullName";
                ddlstaffid.DataSource = DtopmEmpsIDs;
                ddlstaffid.DataBind();
            }
            ddlstaffid.Items.Insert(0, "-Select-");
        }

        private void LoadOurGSTNos()
        {
            DataTable DtGSTNos = GlobalData.Instance.LoadGSTNumbers(BranchID);
            if (DtGSTNos.Rows.Count > 0)
            {
                ddlOurGSTIN.DataValueField = "Id";
                ddlOurGSTIN.DataTextField = "GSTNo";
                ddlOurGSTIN.DataSource = DtGSTNos;
                ddlOurGSTIN.DataBind();
            }
        }
        protected void ddlstate_SelectedIndexChanged(object sender, EventArgs e)
        {
            string qry = "select GSTstatecode,stateid from states where stateid='" + ddlstate.SelectedValue + "'";
            DataTable dt = config.ExecuteAdaptorAsyncWithQueryParams(qry).Result;
            if (dt.Rows.Count > 0)
            {

                if (dt.Rows[0]["stateid"].ToString() != "0")
                {
                    ddlStateCode.SelectedValue = dt.Rows[0]["stateid"].ToString();
                }
                else
                {
                    ddlStateCode.SelectedIndex = 0;
                }

            }
            else
            {
                ddlStateCode.SelectedIndex = 0;

            }
        }

        protected void ddlShipToSate_SelectedIndexChanged(object sender, EventArgs e)
        {
            string qry = "select GSTstatecode,stateid from states where stateid='" + ddlShipToSate.SelectedValue + "'";
            DataTable dt = config.ExecuteAdaptorAsyncWithQueryParams(qry).Result;
            if (dt.Rows.Count > 0)
            {

                if (dt.Rows[0]["stateid"].ToString() != "0")
                {
                    ddlShipToStateCode.SelectedValue = dt.Rows[0]["stateid"].ToString();
                }
                else
                {
                    ddlShipToStateCode.SelectedIndex = 0;
                }
            }
            else
            {
                ddlShipToStateCode.SelectedIndex = 0;

            }
        }
        protected void LoadClients()
        {
            DataTable DtCnames = GlobalData.Instance.LoadCNames(CmpIDPrefix, BranchID);
            if (DtCnames.Rows.Count > 0)
            {
                ddlUnits.DataValueField = "clientid";
                ddlUnits.DataTextField = "clientname";
                ddlUnits.DataSource = DtCnames;
                ddlUnits.DataBind();



            }
            ddlUnits.Items.Insert(0, "-Select-");

        }

        protected void LoadClientids()
        {
            DataTable DtIds = GlobalData.Instance.LoadCIds(CmpIDPrefix, BranchID);
            if (DtIds.Rows.Count > 0)
            {
                ddlcid.DataValueField = "clientid";
                ddlcid.DataTextField = "clientid";
                ddlcid.DataSource = DtIds;
                ddlcid.DataBind();
            }
            ddlcid.Items.Insert(0, "-Select-");
        }

        protected void LoadopmEmpsIDs()
        {
            DataTable DtopmEmpsIDs = GlobalData.Instance.LoadOpManagerIds();
            if (DtopmEmpsIDs.Rows.Count > 0)
            {
                ddlEmpId.DataValueField = "EmpId";
                ddlEmpId.DataTextField = "EmpId";
                ddlEmpId.DataSource = DtopmEmpsIDs;
                ddlEmpId.DataBind();
            }
            ddlEmpId.Items.Insert(0, "-Select-");
        }

        protected void LoadDesignations()
        {
            DataTable DtDesignations = GlobalData.Instance.LoadDesigns();
            if (DtDesignations.Rows.Count > 0)
            {
                ddldesgn.DataValueField = "Designid";
                ddldesgn.DataTextField = "Design";
                ddldesgn.DataSource = DtDesignations;
                ddldesgn.DataBind();
            }
            ddldesgn.Items.Insert(0, "-Select-");
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

        protected void PreviligeUsers(int previligerid)
        {
            switch (previligerid)
            {
                case 1:
                    break;
                case 2:
                    //AddClientLink.Visible = true;
                    //ModifyClientLink.Visible = false;
                    //DeleteClientLink.Visible = false;
                    ContractLink.Visible = false;
                    //ClientAttenDanceLink.Visible = false;
                    // Operationlink.Visible = false;
                    BillingLink.Visible = false;
                    // MRFLink.Visible = false;

                    CompanyInfoLink.Visible = false;
                    InventoryLink.Visible = false;
                    ReportsLink.Visible = false;
                    SettingsLink.Visible = false;

                    break;

                case 3:

                    //AddClientLink.Visible = true;
                    //ModifyClientLink.Visible = true;
                    //DeleteClientLink.Visible = true;
                    ContractLink.Visible = true;
                    //  ClientAttenDanceLink.Visible = true;
                    // Operationlink.Visible = true;
                    BillingLink.Visible = false;
                    // MRFLink.Visible = false;


                    InventoryLink.Visible = false;
                    ReportsLink.Visible = false;
                    SettingsLink.Visible = false;
                    break;
                case 4:

                    //AddClientLink.Visible = true;
                    //ModifyClientLink.Visible = true;
                    //DeleteClientLink.Visible = true;
                    ContractLink.Visible = true;
                    LicensesLink.Visible = true;
                    ClientAttendanceLink.Visible = false;
                    // Operationlink.Visible = true;
                    BillingLink.Visible = false;
                    //MRFLink.Visible = false;

                    EmployeesLink.Visible = true;
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
                    ContractLink.Visible = false;
                    ClientAttendanceLink.Visible = true;
                    // Operationlink.Visible = false;
                    LicensesLink.Visible = true;
                    BillingLink.Visible = false;
                    // MRFLink.Visible = true;

                    EmployeesLink.Visible = true;
                    ClientsLink.Visible = true;
                    CompanyInfoLink.Visible = false;
                    InventoryLink.Visible = false;
                    ReportsLink.Visible = true;
                    ReportsLink.Visible = true;
                    SettingsLink.Visible = true;
                    break;
                case 6:
                    //AddClientLink.Visible = false;
                    //ModifyClientLink.Visible = false;
                    //DeleteClientLink.Visible = false;
                    ContractLink.Visible = false;
                    //  ClientAttenDanceLink.Visible = true;
                    // Operationlink.Visible = false;
                    BillingLink.Visible = false;
                    // MRFLink.Visible = true;

                    CompanyInfoLink.Visible = false;
                    InventoryLink.Visible = false;
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

        protected void btnaddclint_Click(object sender, EventArgs e)
        {
            try
            {

                lblMsg.Text = "";
                lblSuc.Text = "";
                #region  Begin  Check Validations as on  [19-09-2013]

                #region     Begin Check Client id Selected or  ?
                //if (ddlcid.SelectedIndex == 0)
                //{
                //    ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert(' Please Select Client ID ');", true);
                //    return;
                //}


                if (txtclientid.Text.Trim().Length == 0)
                {
                    lblMsg.Text = "Please  Enter the Client ID";
                    //ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert(' Please  Enter the Client ID ');", true);
                    return;
                }


                #endregion  End CCheck Client id Selected or  ?

                #region     Begin Check Client Name is  Empty or ?
                if (txtCname.Text.Trim().Length == 0)
                {
                    lblMsg.Text = "Please Enter The Client Name";
                    //ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert(' Please Enter The Client Name ');", true);
                    return;
                }
                #endregion  End Check Client Name is  Empty or ?

                #region   Begin Check   Contact Person   Name
                if (txtcontactperson.Text.Trim().Length == 0)
                {
                    lblMsg.Text = "Please fill Contact Person name";
                    //ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert(' Please fill Contact Person name ');", true);
                    return;
                }
                #endregion  Begin Check   Contact Person   Name

                #region  Begin Check Designation Selected or ?
                if (ddldesgn.SelectedIndex == 0)
                {
                    lblMsg.Text = "Please Select Designation";
                    //ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert(' Please Select Designation ');", true);
                    return;
                }
                #endregion End Check Designation Selected or ?

                #region Begin Check Phone Number Entered or ?
                if (txtphonenumbers.Text.Trim().Length == 0)
                {
                    lblMsg.Text = "Please Enter the Phone No.";
                    //ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert(' Please Enter the Phone No.');", true);
                    return;
                }
                if (txtphonenumbers.Text.Trim().Length < 8)
                {
                    lblMsg.Text = "Please enter a valid Phone No.";
                    //ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert(' Please enter a valid Phone No.');", true);
                    return;
                }
                #endregion  End Check Phone Number Entered or ?

                #region  Begin Check if Sub unit Check then Should be Select MAin unit ID
                if (chkSubUnit.Checked)
                {
                    if (ddlUnits.SelectedIndex == 0)
                    {
                        lblMsg.Text = "Please select Main unit Id";
                        //ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert(' Please select Main unit Id');", true);
                        return;
                    }
                }
                #endregion End Check if Sub unit Check then Should be Select MAin unit ID

                //if (rdbkeonicsNo.Checked)
                //{
                //    if (ddlLinkedClient.SelectedIndex == 0)
                //    {
                //        lblMsg.Text = "Please select KEONICS Client Id";
                //        //ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert(' Please select Main unit Id');", true);
                //        return;
                //    }
                //}

                #region  Begin Check Invoice  Selected or ?
                if (radioinvoiceyes.Checked == false && radioinvoiceno.Checked == false)
                {
                    lblMsg.Text = "Please Select the Invoice Mode";
                    //ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert(' Please Select the Invoice Mode ');", true);
                    return;
                }
                #endregion End Check Invoice Selected or ?

                #region Begin Check Paysheet Selected  or ?
                if (radiopaysheetyes.Checked == false && radiopaysheetno.Checked == false)
                {
                    lblMsg.Text = "Please Select the  Paysheet Mode";
                    //ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert(' Please Select the  Paysheet Mode');", true);
                    return;
                }

                #endregion  End Check Paysheet Selected  Entered or ?

                #region Begin Check Main Unit  Selected  or ?
                if (radioyesmu.Checked == false && radionomu.Checked == false)
                {
                    lblMsg.Text = "Please Select the  Client Is Main Unit (YES/NO)";
                    //ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert(' Please Select the  Client Is Main Unit (YES/NO)');", true);
                    return;
                }
                #endregion  End Check  Main Unit Selected  Entered or ?

                #region for already Linked Client Check

                if (ddlLinkedClient.SelectedIndex > 0)
                {

                    string CheckLinkedClient = "select Linkedclient,clientid from clients where linkedclient='" + ddlLinkedClient.SelectedValue + "'";
                    DataTable dtLiClient = config.ExecuteAdaptorAsyncWithQueryParams(CheckLinkedClient).Result;

                    string LiClient = "";
                    string KeClient = "";
                    if (dtLiClient.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtLiClient.Rows.Count; i++)
                        {
                            LiClient = dtLiClient.Rows[i]["Linkedclient"].ToString();
                            KeClient = dtLiClient.Rows[i]["clientid"].ToString();

                            if (LiClient != "0")
                            {
                                if (ddlLinkedClient.SelectedValue == LiClient && txtclientid.Text != KeClient)
                                {
                                    lblMsg.Text = "" + LiClient + " is already linked to " + KeClient + "";
                                    //ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert(' Please select Main unit Id');", true);
                                    return;
                                }
                            }
                        }
                    }
                }

                #endregion for already Linked Client Check

                #endregion End   Check Validations as on  [19-09-2013]

                var ShiptoLine1 = "";
                var ShiptoLine2 = "";
                var ShiptoLine3 = "";
                var ShiptoLine4 = "";
                var ShiptoLine5 = "";
                var ShiptoLine6 = "";
                var ShipToState = "0";
                var ShipToStateCode = "0";
                var ShipToGSTIN = "";
                var Division = "0";
                var ESIBranch = "0";

                var Department = "0";
                var KEONICS = "N";

                #region     Begin Declare Variables as on [19-09-2013]
                #region     Begin Code Client-id to  Contact Person
                var ClientId = string.Empty;
                var ClientName = string.Empty;
                var ClientShortName = string.Empty;
                var ClientSegment = string.Empty;
                var ClientContactPerson = string.Empty;
                #endregion  End Code Client-id to Contact Person

                #region  Begin Code  Person-Designation To PIN-No
                var ClientPersonDesgn = string.Empty;
                var ClientPhonenumbers = string.Empty;
                var ClientFax = string.Empty;
                var ClientEmail = string.Empty;
                var ClientAddrPin = string.Empty;
                var Branch = "0";
                var State = "0";
                var StateCode = "0";
                var GSTIN = "";
                var OurGSTIN = "";

                #endregion  End Code  Person-Designation To PIN-No

                #region  Begin Code  Line-One To Line-Five
                var ClientAddrHno = string.Empty;
                var ClientAddrStreet = string.Empty;
                var ClientAddrArea = string.Empty;
                var ClientAddrCity = string.Empty;
                var ClientAddrColony = string.Empty;
                #endregion  End Code Line-One To Line-Five

                #region Begin Code Line Six To PaySheet
                var ClientAddrState = string.Empty;
                var SubUnitStatus = string.Empty;
                var MainUnitId = string.Empty;
                var MAinunitStatus = 0;
                var Invoice = 0;
                var Paysheet = 0;
                var ClientDesc = string.Empty;
                var LinkedClient = "0";

                #endregion End Code Line Six To PaySheet

                #region   Begin Extra Varibles for This Event   As on [20-09-2013]
                var URecordStatus = 0;
                #endregion End Extra Varibles for This Event   As on [20-09-2013]

                var Location = "";

                #endregion  End Declare Variables as on [19-09-2013]


                #region    Begin Code For Assign Values Into Declared Variables as on [19-09-2013]
                #region    Begin Code Client-id to  Contact Person
                //ClientId =ddlcid.SelectedValue;
                ClientId = txtclientid.Text;
                ClientName = txtCname.Text;
                ClientShortName = txtshortname.Text;
                if (ddlsegment.SelectedIndex == 0)
                {
                    ClientSegment = "0";
                }
                else
                {
                    ClientSegment = ddlsegment.SelectedValue;
                }

                ClientContactPerson = txtcontactperson.Text;
                #endregion  End Code Client-id to Contact Person

                #region  Begin Code  Person-Designation To PIN-No
                ClientPersonDesgn = ddldesgn.SelectedValue;
                ClientPhonenumbers = txtphonenumbers.Text;
                ClientFax = txtfaxno.Text;
                ClientEmail = txtemailid.Text;
                ClientAddrPin = txtpin.Text;
                if (ddlBranch.SelectedIndex > 0)
                {
                    Branch = ddlBranch.SelectedValue;
                }

                if (ddlstate.SelectedIndex > 0)
                {
                    State = ddlstate.SelectedValue;
                }

                if (ddlStateCode.SelectedIndex > 0)
                {
                    StateCode = ddlStateCode.SelectedValue;
                }


                GSTIN = txtGSTUniqueID.Text;
                OurGSTIN = ddlOurGSTIN.SelectedValue;
                #endregion  End Code  Person-Designation To PIN-No

                #region  Begin Code  Line-One To Line-Five
                ClientAddrHno = txtchno.Text;
                ClientAddrStreet = txtstreet.Text;
                ClientAddrArea = txtarea.Text;
                ClientAddrCity = txtcity.Text;
                ClientAddrColony = txtcolony.Text;
                #endregion  End Code Line-One To Line-Five

                #region Begin Code Line Six To PaySheet
                ClientAddrState = txtstate.Text;
                if (chkSubUnit.Checked)
                {
                    SubUnitStatus = "1";
                    MainUnitId = ddlUnits.SelectedValue;
                }
                else
                {
                    MainUnitId = "0";
                }

                if (radioyesmu.Checked)
                {
                    MAinunitStatus = 1;
                }
                if (radioinvoiceyes.Checked)
                {
                    Invoice = 1;
                }
                if (radiopaysheetyes.Checked)
                {
                    Paysheet = 1;
                }
                ClientDesc = txtdescription.Text;


                if (ddlLinkedClient.SelectedIndex == 0)
                {
                    LinkedClient = "0";
                }
                else
                {
                    LinkedClient = ddlLinkedClient.SelectedValue;

                }

                string BuyersOrderNo = "";
                BuyersOrderNo = txtBuyerOrderNo.Text;




                var PTState = "0";

                if (ddlPTState.SelectedIndex > 0)
                {
                    PTState = ddlPTState.SelectedValue;
                }


                #endregion End Code Line Six To PaySheet

                if (txtLocation.Text.Trim().Length > 0)
                {
                    Location = txtLocation.Text;
                }

                if (txtShipToLine1.Text.Trim().Length > 0)
                {
                    ShiptoLine1 = txtShipToLine1.Text;
                }


                if (txtShipToLine2.Text.Trim().Length > 0)
                {
                    ShiptoLine2 = txtShipToLine2.Text;
                }

                if (txtShipToLine3.Text.Trim().Length > 0)
                {
                    ShiptoLine3 = txtShipToLine3.Text;
                }


                if (txtShipToLine4.Text.Trim().Length > 0)
                {
                    ShiptoLine4 = txtShipToLine4.Text;
                }

                if (txtShipToLine5.Text.Trim().Length > 0)
                {
                    ShiptoLine5 = txtShipToLine5.Text;
                }

                if (txtShipToLine6.Text.Trim().Length > 0)
                {
                    ShiptoLine6 = txtShipToLine6.Text;
                }

                if (txtShipToGSTIN.Text.Trim().Length > 0)
                {
                    ShipToGSTIN = txtShipToGSTIN.Text;
                }

                ShipToState = ddlShipToSate.SelectedValue;
                ShipToStateCode = ddlShipToStateCode.SelectedValue;

                Division = ddlDivision.SelectedValue;
                ESIBranch = ddlESIBranch.SelectedValue;

                Department = ddldepartment.SelectedValue;

                if (rdbkeonicsyes.Checked)
                {
                    KEONICS = "Y";
                }

                if (rdbkeonicsNo.Checked)
                {
                    KEONICS = "N";
                }

                string FOID = "";
                FOID = ddlstaffid.SelectedValue;

                #endregion   End Code For Assign Values Into Declared Variables as on [19-09-2013]


                #region    Begin Code For Stored Procedure Parameters as on [20-09-2013]
                Hashtable ModifyClientDetails = new Hashtable();
                string ModifyClientDetailsPName = "ModifyClientDetails";

                #region     Begin Code Client-id to  Contact Person

                ModifyClientDetails.Add("@ClientId", ClientId);
                ModifyClientDetails.Add("@ClientName", ClientName);
                ModifyClientDetails.Add("@ClientShortName", ClientShortName);
                ModifyClientDetails.Add("@ClientSegment", ClientSegment);
                ModifyClientDetails.Add("@ClientContactPerson", ClientContactPerson);

                #endregion  End Code Client-id to Contact Person


                #region  Begin Code  Person-Designation To PIN-No

                ModifyClientDetails.Add("@ClientPersonDesgn", ClientPersonDesgn);
                ModifyClientDetails.Add("@ClientPhonenumbers", ClientPhonenumbers);
                ModifyClientDetails.Add("@ClientFax", ClientFax);
                ModifyClientDetails.Add("@ClientEmail", ClientEmail);
                ModifyClientDetails.Add("@ClientAddrPin", ClientAddrPin);
                ModifyClientDetails.Add("@Branch", Branch);
                ModifyClientDetails.Add("@state", State);
                ModifyClientDetails.Add("@StateCode", StateCode);
                ModifyClientDetails.Add("@GSTIN", GSTIN);
                ModifyClientDetails.Add("@OurGSTIN", OurGSTIN);
                ModifyClientDetails.Add("@FOID", FOID);

                #endregion  End Code  Person-Designation To PIN-No


                #region  Begin Code  Line-One To Line-Five

                ModifyClientDetails.Add("@ClientAddrHno", ClientAddrHno);
                ModifyClientDetails.Add("@ClientAddrStreet", ClientAddrStreet);
                ModifyClientDetails.Add("@ClientAddrArea", ClientAddrArea);
                ModifyClientDetails.Add("@ClientAddrCity", ClientAddrCity);
                ModifyClientDetails.Add("@ClientAddrColony", ClientAddrColony);

                #endregion  End Code Line-One To Line-Five

                #region Begin Code Line Six To PaySheet

                ModifyClientDetails.Add("@ClientAddrState", ClientAddrState);
                ModifyClientDetails.Add("@SubUnitStatus", SubUnitStatus);
                ModifyClientDetails.Add("@MainUnitId", MainUnitId);
                ModifyClientDetails.Add("@MAinunitStatus", MAinunitStatus);
                ModifyClientDetails.Add("@Invoice", Invoice);
                ModifyClientDetails.Add("@Paysheet", Paysheet);
                ModifyClientDetails.Add("@ClientDesc", ClientDesc);
                ModifyClientDetails.Add("@LinkedClient", LinkedClient);


                #endregion End Code Line Six To PaySheet

                ModifyClientDetails.Add("@Location", Location);
                ModifyClientDetails.Add("@ShiptoLine1", ShiptoLine1);
                ModifyClientDetails.Add("@ShiptoLine2", ShiptoLine2);
                ModifyClientDetails.Add("@ShiptoLine3", ShiptoLine3);
                ModifyClientDetails.Add("@ShiptoLine4", ShiptoLine4);
                ModifyClientDetails.Add("@ShiptoLine5", ShiptoLine5);
                ModifyClientDetails.Add("@ShiptoLine6", ShiptoLine6);
                ModifyClientDetails.Add("@ShipToState", ShipToState);
                ModifyClientDetails.Add("@ShipToStateCode", ShipToStateCode);
                ModifyClientDetails.Add("@ShipToGSTIN", ShipToGSTIN);
                ModifyClientDetails.Add("@Department", Department);
                ModifyClientDetails.Add("@Division", Division);
                ModifyClientDetails.Add("@ESIBranch", ESIBranch);
                ModifyClientDetails.Add("@PTState", PTState);
                ModifyClientDetails.Add("@BuyersOrderNo", BuyersOrderNo);

                #endregion End Code For Stored Procedure Parameters as on [20-09-2013]


                #region     Begin Code For Calling Stored Procedure as on [20-09-2013]
                URecordStatus = config.ExecuteNonQueryParamsAsync(ModifyClientDetailsPName, ModifyClientDetails).Result;
                #endregion   End   Code For Calling Stored Procedure as on [20-09-2013]


                #region     Begin Code For Status/Resulted Message of the Inserted Record as on [20-09-2013]

                if (URecordStatus > 0)
                {
                    lblSuc.Text = "Client Details Modified Sucessfully.  With  Client Id   :- " + ClientId + " ";
                    //ScriptManager.RegisterStartupScript(this, GetType(), "Show Alert", "alert('Client Details Modified Sucessfully.  With  Client Id   :- " + ClientId + " -: ');", true);
                    //ClearClientsFieldsData();
                    return;
                }
                else
                {
                    lblMsg.Text = "Client Details Not  Added Sucessfully  With  Client Id   :- " + ClientId + " ";
                    //ScriptManager.RegisterStartupScript(this, GetType(), "Show Alert", "alert('Client Details Not  Added Sucessfully  With  Client Id   :- " + ClientId + " -: ');", true);
                    return;
                }
                #endregion  End Code For Status/Resulted Message of the Inserted Record as on [20-09-2013]




            }

            catch (Exception ex)
            {

            }

        }

        private void ClearClientsFieldsData()
        {

            txtCname.Text = txtshortname.Text = txtcontactperson.Text = txtphonenumbers.Text = txtfaxno.Text = txtemailid.Text =
            txtpin.Text = txtchno.Text = txtstreet.Text = txtarea.Text = txtcity.Text = txtcolony.Text =
            txtstate.Text = txtdescription.Text = txtclientid.Text = string.Empty;

            ddlsegment.SelectedIndex = ddldesgn.SelectedIndex = ddlUnits.SelectedIndex = ddlcid.SelectedIndex = ddlLinkedClient.SelectedIndex = 0;
            ddlUnits.Visible = false;

            chkSubUnit.Checked = false;

            radioinvoiceyes.Checked = radioinvoiceno.Checked = radiopaysheetyes.Checked = radiopaysheetno.Checked = radioyesmu.Checked = radionomu.Checked = rdbkeonicsyes.Checked = false;
            txtLocation.Text = string.Empty;
        }

        protected void ddlcid_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlcid.SelectedIndex > 0)
            {
                LoadClientDetails();
            }
            else
            {
                ClearClientsFieldsData();
            }

        }

        protected void LoadClientDetails()
        {

            try
            {
                #region    Begin Code For Declare/Assign Values Variables as on [20-09-2013]
                var Clientid = txtclientid.Text;
                var SPName = "GetCilientsInfo";
                var DdlValue = "0";
                #endregion  End Code For Declare/Assign  Variables as on [20-09-2013]

                #region   Begin Code for Calling Stored Procedure as on [20-09-2013]
                Hashtable HTSpParameters = new Hashtable();
                HTSpParameters.Add("@Clientid", Clientid);
                DataTable DtClientInfo = config.ExecuteAdaptorAsyncWithParams(SPName, HTSpParameters).Result;
                #endregion End Code for Calling Stored Procedure as on [20-09-2013]

                #region  Begin Code for Check Records Are available for the Entered text as  on [09-10-2013]
                if (DtClientInfo.Rows.Count == 0)
                {
                    lblMsg.Text = "Client Details Are Not Available For the Entered Client ID/NAME";
                    //ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Client Details Are Not Available For the Entered Client ID/NAME');", true);
                    ClearClientsFieldsData();
                    return;
                }
                #endregion End Code for Check Records Are available for the Entered text as  on [09-10-2013]


                #region    Begin Code For Assign  Data Column Values to Controls as on [20-09-2013]
                #region    Begin Code Client-Name to  Contact Person
                txtCname.Text = DtClientInfo.Rows[0]["ClientName"].ToString();
                txtshortname.Text = DtClientInfo.Rows[0]["ClientShortName"].ToString();
                DdlValue = DtClientInfo.Rows[0]["ClientSegment"].ToString();
                if (DdlValue != "0")
                {
                    ddlsegment.SelectedValue = DdlValue;
                }
                else
                {
                    ddlsegment.SelectedIndex = 0;
                }

                txtcontactperson.Text = DtClientInfo.Rows[0]["ClientContactPerson"].ToString();
                #endregion  End Code Client-Name  to Contact Person

                #region  Begin Code  Person-Designation To PIN-No
                DdlValue = DtClientInfo.Rows[0]["ClientPersonDesgn"].ToString();
                if (DdlValue != "0")
                {
                    ddldesgn.SelectedValue = DdlValue;
                }
                else
                {
                    ddldesgn.SelectedIndex = 0;
                }
                txtphonenumbers.Text = DtClientInfo.Rows[0]["ClientPhonenumbers"].ToString();
                txtfaxno.Text = DtClientInfo.Rows[0]["ClientFax"].ToString();
                txtemailid.Text = DtClientInfo.Rows[0]["ClientEmail"].ToString();
                txtpin.Text = DtClientInfo.Rows[0]["ClientAddrPin"].ToString();


                if (DtClientInfo.Rows[0]["UnitIncharge"].ToString() != "0")
                {
                    ddlstaffid.SelectedValue = DtClientInfo.Rows[0]["UnitIncharge"].ToString();
                }
                else
                {
                    ddlstaffid.SelectedIndex = 0;
                }


                if (DtClientInfo.Rows[0]["Branch"].ToString() != "0")
                {
                    ddlBranch.SelectedValue = DtClientInfo.Rows[0]["Branch"].ToString();
                }
                else
                {
                    ddlBranch.SelectedIndex = 0;

                }

                if (DtClientInfo.Rows[0]["state"].ToString() != "0")
                {
                    ddlstate.SelectedValue = DtClientInfo.Rows[0]["state"].ToString();
                }
                else
                {
                    ddlstate.SelectedIndex = 0;

                }
                if (DtClientInfo.Rows[0]["statecode"].ToString() != "0")
                {
                    ddlStateCode.SelectedValue = DtClientInfo.Rows[0]["statecode"].ToString();
                }
                else
                {
                    ddlStateCode.SelectedIndex = 0;
                }

                ddlOurGSTIN.SelectedValue = DtClientInfo.Rows[0]["OurGSTIN"].ToString();
                txtGSTUniqueID.Text = DtClientInfo.Rows[0]["GSTIN"].ToString();

                #endregion  End Code  Person-Designation To PIN-No

                #region  Begin Code  Line-One To Line-Five
                txtchno.Text = DtClientInfo.Rows[0]["ClientAddrHno"].ToString();
                txtstreet.Text = DtClientInfo.Rows[0]["ClientAddrStreet"].ToString();
                txtarea.Text = DtClientInfo.Rows[0]["ClientAddrArea"].ToString();
                txtcity.Text = DtClientInfo.Rows[0]["ClientAddrCity"].ToString();
                txtcolony.Text = DtClientInfo.Rows[0]["ClientAddrColony"].ToString();
                #endregion  End Code Line-One To Line-Five

                #region Begin Code Line Six To PaySheet
                txtstate.Text = DtClientInfo.Rows[0]["ClientAddrState"].ToString();
                DdlValue = DtClientInfo.Rows[0]["SubUnitStatus"].ToString();
                if (bool.Parse(DdlValue) == true)
                {
                    chkSubUnit.Checked = true;
                    ddlUnits.SelectedValue = DtClientInfo.Rows[0]["MainUnitId"].ToString();
                    ddlUnits.Visible = true;
                }
                else
                {

                    chkSubUnit.Checked = false;
                    ddlUnits.Visible = false;
                    ddlUnits.SelectedIndex = 0;
                }

                DdlValue = DtClientInfo.Rows[0]["MainUnitId"].ToString();
                if (DdlValue != "0")
                {
                    ddlUnits.SelectedValue = DdlValue;
                }
                else
                {
                    ddlUnits.SelectedIndex = 0;
                }

                DdlValue = DtClientInfo.Rows[0]["MAinunitStatus"].ToString();
                if (bool.Parse(DdlValue) == true)
                {
                    radioyesmu.Checked = true;
                    radionomu.Checked = false;
                }
                else
                {
                    radioyesmu.Checked = false;
                    radionomu.Checked = true;
                }


                DdlValue = DtClientInfo.Rows[0]["Invoice"].ToString();
                if (bool.Parse(DdlValue) == true)
                {
                    radioinvoiceyes.Checked = true;
                    radioinvoiceno.Checked = false;
                }
                else
                {
                    radioinvoiceyes.Checked = false;
                    radioinvoiceno.Checked = true;
                }


                DdlValue = DtClientInfo.Rows[0]["Paysheet"].ToString();
                if (bool.Parse(DdlValue) == true)
                {
                    radiopaysheetyes.Checked = true;
                    radiopaysheetno.Checked = false;
                }
                else
                {
                    radiopaysheetyes.Checked = false;
                    radiopaysheetno.Checked = true;
                }

                txtdescription.Text = DtClientInfo.Rows[0]["ClientDesc"].ToString();
                #endregion End Code Line Six To PaySheet

                txtLocation.Text = DtClientInfo.Rows[0]["Location"].ToString();
                txtShipToLine1.Text = DtClientInfo.Rows[0]["ShiptoLine1"].ToString();
                txtShipToLine2.Text = DtClientInfo.Rows[0]["ShiptoLine2"].ToString();
                txtShipToLine3.Text = DtClientInfo.Rows[0]["ShiptoLine3"].ToString();
                txtShipToLine4.Text = DtClientInfo.Rows[0]["ShiptoLine4"].ToString();
                txtShipToLine5.Text = DtClientInfo.Rows[0]["ShiptoLine5"].ToString();
                txtShipToLine6.Text = DtClientInfo.Rows[0]["ShiptoLine6"].ToString();
                txtShipToGSTIN.Text = DtClientInfo.Rows[0]["ShipToGSTIN"].ToString();


                if (DtClientInfo.Rows[0]["ShipToState"].ToString() != "0")
                {
                    ddlShipToSate.SelectedValue = DtClientInfo.Rows[0]["ShipToState"].ToString();
                }
                else
                {
                    ddlShipToSate.SelectedIndex = 0;

                }
                if (DtClientInfo.Rows[0]["ShipToStateCode"].ToString() != "0")
                {
                    ddlShipToStateCode.SelectedValue = DtClientInfo.Rows[0]["ShipToStateCode"].ToString();
                }
                else
                {
                    ddlShipToStateCode.SelectedIndex = 0;
                }

                if (DtClientInfo.Rows[0]["ptstate"].ToString() != "0")
                {
                    ddlPTState.SelectedValue = DtClientInfo.Rows[0]["ptstate"].ToString();
                }
                else
                {
                    ddlPTState.SelectedIndex = 0;

                }


                txtBuyerOrderNo.Text = DtClientInfo.Rows[0]["BuyersOrderNo"].ToString();



                #endregion End   Code For Assign  Data Column Values to Controls as on [20-09-2013]

            }

            catch (Exception ex)
            {


            }

        }

        protected void btncancel_Click(object sender, EventArgs e)
        {

            ClearClientsFieldsData();

        }

        protected void chkSubUnit_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSubUnit.Checked)
            {
                ddlUnits.Visible = true;
            }
            else
            {
                ddlUnits.Visible = false;
            }
        }

        protected void TxtClient_OnTextChanged(object sender, EventArgs e)
        {
            if (txtclientid.Text.Trim().Length == 0)
            {
                lblMsg.Text = "Please Enter the Client ID";
                //ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Please Enter the Client ID');", true);
            }
            LoadClientDetails();

        }

        protected void rdbkeonicsyes_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbkeonicsNo.Checked)
            {
                lblKeonicsClientID.Visible = true;
                ddlLinkedClient.Visible = true;
            }
            else
            {
                lblKeonicsClientID.Visible = false;
                ddlLinkedClient.Visible = false;
            }
        }
    }
}