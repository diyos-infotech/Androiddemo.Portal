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
using Jawan.Portal.DAL;
namespace Jawan.Portal
{
    public partial class InvoieReports : System.Web.UI.Page
    {
        string CmpIDPrefix = "";
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

                    FillClientList();
                    FillClientNameList();
                    LoadSegments();
                }
            }
            catch (Exception ex)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "Show alert", "alert('Your Session Expired');", true);
                Response.Redirect("~/Login.aspx");
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
                    break;
                default:
                    break;


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
            ddlsegment.Items.Insert(0, "All");
        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {

            try
            {


                LblResult.Text = "";
                DataTable DtNull = null;
                GVInvoiceBills.DataSource = DtNull;
                GVInvoiceBills.DataBind();
                Hashtable HtGetInvoiceAlldata = new Hashtable();
                string SPName = "GetInvoiceAlldata";


                if (ddlClientId.SelectedIndex == 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Showalert", "alert('Please Select ClientId');", true);
                    return;
                }
                else
                {
                    if (ddlClientId.SelectedIndex == 1)
                    {
                        HtGetInvoiceAlldata.Add("@ClientStatus", 0);

                    }
                    if (ddlClientId.SelectedIndex > 1)
                    {
                        HtGetInvoiceAlldata.Add("@ClientId", ddlClientId.SelectedValue);
                        HtGetInvoiceAlldata.Add("@ClientStatus", 1);


                    }
                }


                //if (ddlsegment.SelectedIndex == 0)
                //{
                //    HtGetInvoiceAlldata.Add("@segmentStatus", 0);

                //}
                //if (ddlsegment.SelectedIndex > 0)
                //{
                //    HtGetInvoiceAlldata.Add("@segment", ddlsegment.SelectedValue);
                //    HtGetInvoiceAlldata.Add("@segmentStatus", 1);


                //}



                if (txtEndDate.Text.Trim().Length == 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showlalert", "alert('Dont Leave Empty  End Date ');", true);
                    return;
                }
                string SelectQueryByDate = string.Empty;
                string month = DateTime.Parse(txtEndDate.Text.Trim(), CultureInfo.GetCultureInfo("en-GB")).Month.ToString();
                string Year = DateTime.Parse(txtEndDate.Text.Trim(), CultureInfo.GetCultureInfo("en-GB")).Year.ToString();

                string monthval = month + Year.Substring(2, 2);

                string Segment = ddlsegment.SelectedValue;

                if (ddlsegment.SelectedIndex == 0)
                {
                    Segment = "%";
                }

                HtGetInvoiceAlldata.Add("@month", monthval);
                HtGetInvoiceAlldata.Add("@Segment", Segment);

                #region  Begin code for All as on [08-02-2014]

                if (ddlbilltype.SelectedIndex == 0 && ddlinvoicetype.SelectedIndex == 0)
                {
                    HtGetInvoiceAlldata.Add("@Billtype", 0);
                    HtGetInvoiceAlldata.Add("@Invoicetype", 0);
                }
                if (ddlbilltype.SelectedIndex == 0 && ddlinvoicetype.SelectedIndex == 1)
                {
                    return;
                }
                if (ddlbilltype.SelectedIndex == 0 && ddlinvoicetype.SelectedIndex == 2)
                {
                    return;
                }
                if (ddlbilltype.SelectedIndex == 1 && ddlinvoicetype.SelectedIndex == 0)
                {

                    HtGetInvoiceAlldata.Add("@Billtype", 1);
                    HtGetInvoiceAlldata.Add("@Invoicetype", 0);

                }
                if (ddlbilltype.SelectedIndex == 2 && ddlinvoicetype.SelectedIndex == 0)
                {
                    HtGetInvoiceAlldata.Add("@Billtype", 2);
                    HtGetInvoiceAlldata.Add("@Invoicetype", 0);
                }

                if (ddlbilltype.SelectedIndex == 1 && ddlinvoicetype.SelectedIndex == 1)
                {

                    HtGetInvoiceAlldata.Add("@Billtype", 1);
                    HtGetInvoiceAlldata.Add("@Invoicetype", 1);

                }
                if (ddlbilltype.SelectedIndex == 2 && ddlinvoicetype.SelectedIndex == 1)
                {
                    HtGetInvoiceAlldata.Add("@Billtype", 2);
                    HtGetInvoiceAlldata.Add("@Invoicetype", 1);

                }
                if (ddlbilltype.SelectedIndex == 1 && ddlinvoicetype.SelectedIndex == 2)
                {

                    HtGetInvoiceAlldata.Add("@Billtype", 1);
                    HtGetInvoiceAlldata.Add("@Invoicetype", 2);

                }
                if (ddlbilltype.SelectedIndex == 2 && ddlinvoicetype.SelectedIndex == 2)
                {
                    HtGetInvoiceAlldata.Add("@Billtype", 2);
                    HtGetInvoiceAlldata.Add("@Invoicetype", 2);

                }
                #endregion End code for All as on [08-02-2014]

                DataTable DtSqlData =config.ExecuteAdaptorAsyncWithParams(SPName, HtGetInvoiceAlldata).Result;
                if (DtSqlData.Rows.Count > 0)
                {
                    GVInvoiceBills.DataSource = DtSqlData;
                    GVInvoiceBills.DataBind();


                }
                else
                {
                    GVInvoiceBills.DataSource = null;
                    GVInvoiceBills.DataBind();
                    ScriptManager.RegisterStartupScript(this, GetType(), "Showalert", "alert('There is no data for selected client');", true);
                    return;
                }
            }
            catch (Exception ex)
            {

            }
        }
        private void BindData(string SqlQury)
        {
            ClearAmountdetails();
            DataTable dt = config.ExecuteAdaptorAsyncWithQueryParams(SqlQury).Result;
            if (dt.Rows.Count > 0)
            {
                lbltamttext.Visible = true;
                if (ddlbilltype.SelectedIndex > 0)
                {
                    GVInvoiceBills.DataSource = dt;
                    GVInvoiceBills.DataBind();
                }
            }
            else
            {
                LblResult.Text = " There is No bills  Between The Selected Dates ";
            }
        }
        protected void ClearAmountdetails()
        {
            lbltamttext.Visible = false;
            lbltmtinvoice.Text = "";
        }
        /*protected void btnsearch_Click(object sender, EventArgs e)
        {
            ClearData();

            string date = "";

            #region Begin Code  For Validation as on [16-11-2013]

            if (ddlClientId.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Please Select the client Id /ALL');", true);
                return;
            }

            if (txtEndDate.Text.Trim().Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Please Select The Month');", true);
                return;
            }

            if (txtEndDate.Text.Trim().Length > 0)
            {
                date = DateTime.Parse(txtEndDate.Text.Trim(), CultureInfo.GetCultureInfo("en-gb")).ToString();
            }

            #endregion End  Code  For Validation as on [16-11-2013]

            #region  Begin Code For Variable Declaration   as on [16-11-2013]
            var SPName = "";
            var Clientid = "";
            var Month = "";
            var Year = "";
            var clientindex = "";
            var billtype = "";
            var invoicetype = "";
            var clientidprefix = "";

            DataTable DtListOfEmployees = null;
            Hashtable HtListOfEmployees = new Hashtable();

            #endregion End  Code For Variable Declaration  as on [16-11-2013]


            #region  Begin Code For Assign Values to Variables   as on [16-11-2013]

            Month = DateTime.Parse(date).Month.ToString();
            Year = DateTime.Parse(date).Year.ToString();

            Month = Month + Year.Substring(2, 2);
            clientindex = ddlClientId.SelectedIndex.ToString();
            clientidprefix = CmpIDPrefix;
            billtype = ddlbilltype.SelectedIndex.ToString();
            invoicetype = ddlinvoicetype.SelectedIndex.ToString();


            SPName = "ReportForInvoicesBasedonClients";
            Clientid = ddlClientId.SelectedValue;
            HtListOfEmployees.Add("@clientid", Clientid);
            HtListOfEmployees.Add("@month", Month);
            HtListOfEmployees.Add("@clientindex", clientindex);
         HtListOfEmployees.Add("@clientidprefix", clientidprefix);

            HtListOfEmployees.Add("@billtype",billtype );
            HtListOfEmployees.Add("@invoicetype", invoicetype);
        
            #endregion End  Code For Assign Values to Variables  as on [16-11-2013]

            #region  Begin code For Calling Stored Procedue  and Data To Gridview  As on [16-11-2013]
            DtListOfEmployees = SqlHelper.Instance.ExecuteStoredProcedureWithParams(SPName, HtListOfEmployees);
            if (DtListOfEmployees.Rows.Count > 0)
            {
                GVInvoiceBills.DataSource = DtListOfEmployees;
                GVInvoiceBills.DataBind();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('The  Details Are Not Avaialable');", true);
            }

            #endregion End Code For Calling Stored Procedue and Data To Gridview  As on [16-11-2013]


        }*/

        protected void ddlcname_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            ClearData();
            if (ddlcname.SelectedIndex > 0)
            {
                ddlClientId.SelectedValue = ddlcname.SelectedValue;
            }
            else
            {

                ddlClientId.SelectedIndex = 0;
            }

        }

        protected void ddlClientId_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearData();
            if (ddlClientId.SelectedIndex > 0)
            {
                ddlcname.SelectedValue = ddlClientId.SelectedValue;
            }
            else
            {
                ddlcname.SelectedIndex = 0;
            }
        }

        protected void FillClientList()
        {
            DataTable dt = GlobalData.Instance.LoadCIds(CmpIDPrefix,BranchID);
            if (dt.Rows.Count > 0)
            {
                ddlClientId.DataValueField = "clientid";
                ddlClientId.DataTextField = "clientid";
                ddlClientId.DataSource = dt;
                ddlClientId.DataBind();
            }
            ddlClientId.Items.Insert(0, "--Select--");
            ddlClientId.Items.Insert(1, "ALL");


        }

        protected void FillClientNameList()
        {

            DataTable dt = GlobalData.Instance.LoadCNames(CmpIDPrefix,BranchID);
            if (dt.Rows.Count > 0)
            {
                ddlcname.DataValueField = "clientid";
                ddlcname.DataTextField = "Clientname";
                ddlcname.DataSource = dt;
                ddlcname.DataBind();
            }
            ddlcname.Items.Insert(0, "--Select--");
            ddlcname.Items.Insert(1, "ALL");


        }

        protected void GetWebConfigdata()
        {
            CmpIDPrefix = Session["CmpIDPrefix"].ToString();
            BranchID = Session["BranchID"].ToString();
        }


        protected void txtviewbillno_OnTextChanged(object sender, EventArgs e)
        {
            string sqlqry = "Select Clients.Clientname,unitbill.unitid from unitbill  " +
                "  inner join  clients  on Clients.clientid=unitbill.unitid Where billno='" + txtviewbillno.Text.Trim() + "'";
            DataTable dt = config.ExecuteAdaptorAsyncWithQueryParams(sqlqry).Result;
            if (dt.Rows.Count > 0)
            {
                txtclientidview.Text = dt.Rows[0][1].ToString();
                txtclientnameview.Text = dt.Rows[0][0].ToString();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showlalert", "alert('invalid Bill  no');", true);

            }


        }


        protected void lbtn_Export_Click(object sender, EventArgs e)
        {
            gve.Export("invoicebills.xls", this.GVInvoiceBills);
        }


        protected void ClearData()
        {
            GVInvoiceBills.DataSource = null;
            GVInvoiceBills.DataBind();
        }
        float totalAmt = 0;
        float totalServiceTaxTotal = 0;
        float totalServicetacs = 0;
        float totalServiceTaxtf = 0;
        float GrandTotal = 0;
        float totalServiceTax = 0;
        float totalCESS = 0;
        float totalShecess = 0;
        float totalServiveCharg = 0;



        decimal total = 0, ServiceTaxtotal = 0, servicetaxsf = 0, servicetaxtf = 0, grandtotal = 0, servicetax = 0, CESS = 0, shecess = 0,
        sbcess = 0, kkcess = 0, ServiceChrg = 0, TotalComponents = 0, TotalDiscount = 0, others = 0, ServiceTax = 0;

        protected void GVInvoiceBills_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                total += decimal.Parse(e.Row.Cells[4].Text);
                others += decimal.Parse(e.Row.Cells[5].Text);
                ServiceChrg += decimal.Parse(e.Row.Cells[6].Text);
                ServiceTaxtotal += decimal.Parse(e.Row.Cells[7].Text);
                sbcess += decimal.Parse(e.Row.Cells[8].Text);
                kkcess += decimal.Parse(e.Row.Cells[9].Text);
                grandtotal += decimal.Parse(e.Row.Cells[10].Text);

                //CESS += decimal.Parse(e.Row.Cells[10].Text);
                //shecess += decimal.Parse(e.Row.Cells[11].Text);
                //ServiceChrg += decimal.Parse(e.Row.Cells[12].Text);
                //TotalComponents += decimal.Parse(((Label)e.Row.FindControl("lblElectricalChrg")).Text) +
                //                    decimal.Parse(((Label)e.Row.FindControl("lblMachinaryCost")).Text) +
                //                    decimal.Parse(((Label)e.Row.FindControl("lblMaterialCost")).Text) +
                //                    decimal.Parse(((Label)e.Row.FindControl("lblExtraAmtTwo")).Text) +
                //                    decimal.Parse(((Label)e.Row.FindControl("lblExtraAmtone")).Text) +
                //                    decimal.Parse(((Label)e.Row.FindControl("lblServiceChrg")).Text);
                //TotalDiscount += decimal.Parse(((Label)e.Row.FindControl("lblDiscount")).Text) +
                //                decimal.Parse(((Label)e.Row.FindControl("lblDiscounttwo")).Text);


            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].Text = "Total";
                e.Row.Cells[4].Text = total.ToString();
                e.Row.Cells[5].Text = others.ToString();
                e.Row.Cells[6].Text = ServiceChrg.ToString();
                e.Row.Cells[7].Text = ServiceTaxtotal.ToString();
                e.Row.Cells[8].Text = sbcess.ToString();
                e.Row.Cells[9].Text = kkcess.ToString();
                e.Row.Cells[10].Text = grandtotal.ToString();
                //e.Row.Cells[11].Text = shecess.ToString();
                //e.Row.Cells[12].Text = ServiceChrg.ToString();
                //e.Row.Cells[13].Text = TotalComponents.ToString();
                //e.Row.Cells[14].Text = TotalDiscount.ToString();
            }
        }
    }
}