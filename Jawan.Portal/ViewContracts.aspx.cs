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
    public partial class ViewContracts : System.Web.UI.Page
    {
        string CmpIDPrefix = "";
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
                    //AddClientLink.Visible = true;
                    //ModifyClientLink.Visible = false;
                    //DeleteClientLink.Visible = false;
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
                    //AddClientLink.Visible = true;
                    //ModifyClientLink.Visible = true;
                    //DeleteClientLink.Visible = true;
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
                    //ModifyClientLink.Visible = false;
                    //DeleteClientLink.Visible = false;
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
                    //AddClientLink.Visible = false;
                    //ModifyClientLink.Visible = false;
                    //DeleteClientLink.Visible = false;
                    ContractLink.Visible = false;
                    // ClientAttenDanceLink.Visible = true;
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


        public void DisplayData(string clientid)
        {


            string SqlQrySearch = "select ClientId,  convert(varchar(20),ContractStartDate,101) as ContractStartDate, convert(varchar(20),ContractEndDate,101) as ContractEndDate, BGAmount," +
             " CASE BillDates WHEN 0 then '1st To 1st' when 1 then 'Start Date To One Month' when 3 then '26 To 25' when 4 then '20 To 19' END BillDates,CASE PaymentType WHEN 0 then 'Lumpsum' WHEN 1 then 'Man Power' END PaymentType," +
             " CASE WageType WHEN 1 then 'Client' WHEN 2 then 'Prof. Tax' WHEN 3 then 'Prof. Tax' WHEN 4 then 'SP PT' END WageType, ContractId, convert(varchar(20),ValidityDate,101) as ValidityDate, MaterialCostPerMonth, MachinaryCostPerMonth," +
             " CASE ServiceCharge WHEN 0 then 'No' WHEN 1 then 'Yes' END ServiceCharge, ContractDescription, " +
             " CASE ServiceTaxType WHEN 0 then 'No' WHEN 1 then 'Yes' END ServiceTaxType, Description, CASE PFFROM WHEN 0 then 'Basic' WHEN 1 then 'Basic+DA' when 2 then 'Basic+DA+HRA+WA' END PFFROM, " +
             " PFonOT, ESI, ESIfrom, ESIonOT,Pflimit,Esilimit,OTPersent,CASE OTAmounttype WHEN 0 then 'Regular' WHEN 1 then 'Special' END OTAmounttype from contracts where clientid='" + clientid + "'";

            DataTable dtSearch = config.ExecuteAdaptorAsyncWithQueryParams(SqlQrySearch).Result;

            lblClientid.Text = dtSearch.Rows[0]["ClientId"].ToString();
            lblStartdate.Text = dtSearch.Rows[0]["ContractStartDate"].ToString();
            lblEnddate.Text = dtSearch.Rows[0]["ContractEndDate"].ToString();
            lblBgAmount.Text = dtSearch.Rows[0]["BGAmount"].ToString();
            lblBillingdates.Text = dtSearch.Rows[0]["BillDates"].ToString();

            lblPayments.Text = dtSearch.Rows[0]["PaymentType"].ToString();
            lblWages.Text = dtSearch.Rows[0]["WageType"].ToString();
            lblContractid.Text = dtSearch.Rows[0]["ContractId"].ToString();
            lblValidity.Text = dtSearch.Rows[0]["ValidityDate"].ToString();
            lblMaterialcost.Text = dtSearch.Rows[0]["MaterialCostPerMonth"].ToString();

            lblMachinerycost.Text = dtSearch.Rows[0]["MachinaryCostPerMonth"].ToString();
            lblServicecharge.Text = dtSearch.Rows[0]["ServiceCharge"].ToString();
            lblContractdesc.Text = dtSearch.Rows[0]["ContractDescription"].ToString();
            lblServicetax.Text = dtSearch.Rows[0]["ServiceTaxType"].ToString();
            lblInvoicedesc.Text = dtSearch.Rows[0]["Description"].ToString();

            lblPf.Text = dtSearch.Rows[0]["PFFROM"].ToString();
            lblEsi.Text = dtSearch.Rows[0]["ESI"].ToString();
            lblPflimit.Text = dtSearch.Rows[0]["Pflimit"].ToString();
            lblEsilimit.Text = dtSearch.Rows[0]["Esilimit"].ToString();
            lblOt.Text = dtSearch.Rows[0]["OTPersent"].ToString();

            lblOtamount.Text = dtSearch.Rows[0]["OTAmounttype"].ToString();
            string SqlQrySearch1 = "select Clientname from clients where clientid='" + clientid + "'";

            DataTable dtSearch1 = config.ExecuteAdaptorAsyncWithQueryParams(SqlQrySearch1).Result;

            lblClientname.Text = dtSearch1.Rows[0]["Clientname"].ToString();

            //string SqlQrySearch2 = "select ClientId,Contractid,Designations,Quantity,basic,da,hra," +
            //    " conveyance,washallownce,OtherAllowance,Summary,Amount,pffrom,esifrom,pf," +
            //     " esi,cca,leaveamount,bonus,gratuity,PayType,NoOfDays,nfhs,nots,rc,cs from ContractDetails where clientid='" + clientid + "'";
            //DataTable dtSearch2 = SqlHelper.Instance.GetTableByQuery(SqlQrySearch2);

            //lblDesign.Text = dtSearch2.Rows[0]["Designations"].ToString();
            //lblAmount.Text = dtSearch2.Rows[0]["Amount"].ToString();


            string SqlQry = "select c.ClientId,c.Contractid,Ds.Design  as Designations,c.Quantity,c.basic,c.da,c.hra," +
            " c.conveyance,c.washallownce,c.OtherAllowance,c.Summary,c.Amount,c.pffrom,c.esifrom,c.pf," +
            " c.esi,c.cca,c.leaveamount,c.bonus,c.gratuity,CASE PayType WHEN 0 then 'P.M' when 1 then 'P.D' when 2 then 'P.Hr' when 3 then 'P.Sft' when 4 then 'FIXED' END PayType, " +
            " CASE NoOfDays WHEN 0 then 'Gen' when 1 then 'G-S' when 2 then '24' when 3 then '25' when 4 then '26' when 5 then '27' when 6 then '30' when 7 then '31'  END NoOfDays, " +
             " CASE nots WHEN 0 then 'Gen' when 1 then '24' when 2 then '25' when 3 then '26' when 4 then '27' when 5 then '30' when 6 then '31'  END nots, " +
            " c.nfhs,c.rc,c.cs from ContractDetails c inner join designations Ds on Ds.Designid=Designations where clientid='" + clientid + "'";
            DataTable Dt = config.ExecuteAdaptorAsyncWithQueryParams(SqlQry).Result;
            Dt = config.ExecuteAdaptorAsyncWithQueryParams(SqlQry).Result;

            gvHrs.DataSource = Dt;
            gvHrs.DataBind();




        }

    }
}