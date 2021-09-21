using System;
using System.Collections;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data;
using System.Data.SqlClient;
using KLTS.Data;
using System.IO;
using System.Globalization;
using System.Collections.Generic;
using Jawan.Portal.DAL;
namespace Jawan.Portal
{
    public partial class ViewClient : System.Web.UI.Page
    {
        AppConfiguration config = new AppConfiguration();
        GridViewExportUtil gve = new GridViewExportUtil();
        string CmpIDPrefix = "";

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

                    if (Request.QueryString["ClientId"] != null)
                    {

                        string username = Request.QueryString["ClientId"].ToString();
                        DisplayData(username);

                    }

                }
            }
            catch (Exception ex)
            {

            }
        }
        protected void GetWebConfigdata()
        {
            CmpIDPrefix = Session["CmpIDPrefix"].ToString();
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
                    ContractLink.Visible = false;
                    LicensesLink.Visible = false;
                    ClientAttendanceLink.Visible = true;
                    Operationlink.Visible = false;
                    BillingLink.Visible = false;
                    MRFLink.Visible = false;

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
                    ContractLink.Visible = false;
                    LicensesLink.Visible = false;
                    ClientAttendanceLink.Visible = true;
                    Operationlink.Visible = false;
                    BillingLink.Visible = true;
                    MRFLink.Visible = false;

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

                    //AddClientLink.Visible = false;
                    //ModifyClientLink.Visible = true;
                    //DeleteClientLink.Visible = false;
                    ContractLink.Visible = false;
                    ClientAttendanceLink.Visible = true;
                    Operationlink.Visible = false;
                    LicensesLink.Visible = true;
                    BillingLink.Visible = false;
                    MRFLink.Visible = true;

                    EmployeesLink.Visible = true;
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
                    ContractLink.Visible = false;
                    ClientsLink.Visible = true;
                    //ClientAttenDanceLink.Visible = true;
                    Operationlink.Visible = false;
                    BillingLink.Visible = false;
                    MRFLink.Visible = true;

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



        public void DisplayData(string clientid)
        {


            string SqlQrySearch = " select clientid,clientname,ClientAddrCity, ClientId, ClientShortName, ClientAddrHno, ClientAddrColony," +
            " ClientAddrArea, ClientAddrState, ClientAddrPin, ClientSegment, ClientContactPerson, ClientPersonDesgn, ClientPhonenumbers, ClientFax, " +
            " ClientEmail, ClientDesc, OurContactPersonId, ClientStatus, CASE SubUnitStatus WHEN 'True' then 'Yes' when 'False' then 'No' END SubUnitStatus, " +
            " CASE MainUnitStatus WHEN 1 then 'Yes' when 0 then 'No' END MainUnitStatus, CASE Invoice WHEN 'True' then 'Yes' when 'False' then 'No' END Invoice, " +
            " CASE Paysheet WHEN 'True' then 'Yes' when 'False' then 'No' END Paysheet, MAinunitStatus,ClientAddrStreet from  clients  where  clientid='" + clientid + "'";



            DataTable dtSearch = config.ExecuteAdaptorAsyncWithQueryParams(SqlQrySearch).Result;

            lblClientid.Text = dtSearch.Rows[0]["clientid"].ToString();
            lblShortname.Text = dtSearch.Rows[0]["ClientShortName"].ToString();
            lblContact.Text = dtSearch.Rows[0]["ClientContactPerson"].ToString();
            lblPhone.Text = dtSearch.Rows[0]["ClientPhonenumbers"].ToString();
            lblEmail.Text = dtSearch.Rows[0]["ClientEmail"].ToString();
            lblNameclient.Text = dtSearch.Rows[0]["clientname"].ToString();
            lblSegment.Text = dtSearch.Rows[0]["ClientSegment"].ToString();

            lblDesig.Text = dtSearch.Rows[0]["ClientPersonDesgn"].ToString();
            lblLand.Text = dtSearch.Rows[0]["ClientAddrPin"].ToString();
            lblFaxno.Text = dtSearch.Rows[0]["ClientFax"].ToString();

            txtchno.Text = dtSearch.Rows[0]["ClientAddrHno"].ToString();
            txtarea.Text = dtSearch.Rows[0]["ClientAddrArea"].ToString();
            txtcity.Text = dtSearch.Rows[0]["ClientAddrCity"].ToString();
            txtdescription.Text = dtSearch.Rows[0]["ClientDesc"].ToString();


            txtstreet.Text = dtSearch.Rows[0]["ClientAddrStreet"].ToString();
            txtcolony.Text = dtSearch.Rows[0]["ClientAddrColony"].ToString();

            txtstate.Text = dtSearch.Rows[0]["ClientAddrState"].ToString();
            chkSubUnit.Text = dtSearch.Rows[0]["SubUnitStatus"].ToString();
            radioyesmu.Text = dtSearch.Rows[0]["MainUnitStatus"].ToString();
            radioinvoiceyes.Text = dtSearch.Rows[0]["Invoice"].ToString();
            radiopaysheetyes.Text = dtSearch.Rows[0]["Paysheet"].ToString();


        }
    }
}