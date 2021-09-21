using System;
using System.Collections.Generic;
using System.Linq;
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
    public partial class ViewReceipts : System.Web.UI.Page
    {
        string EmpIDPrefix = "";
        string CmpIDPrefix = "";
        GridViewExportUtil gve = new GridViewExportUtil();
        AppConfiguration config = new AppConfiguration();
        protected void Page_Load(object sender, EventArgs e)
        {
            int i = 0;
            try
            {
                GetWebConfigdata();
                if (!IsPostBack)
                {
                    if (Session["UserId"] != null && Session["AccessLevel"] != null)
                    {
                        lblDisplayUser.Text = Session["UserId"].ToString();
                        //  PreviligeUsers(Convert.ToInt32(Session["AccessLevel"]));
                        lblDisplayUser.Text = Session["UserId"].ToString();
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
                        PreviligeUsers(Convert.ToInt32(Session["AccessLevel"]));
                    }
                    else
                    {
                        Response.Redirect("login.aspx");
                    }

                    LoadClientIdAndName();

                    if (Request.QueryString["clientid"] != null || Request.QueryString["ReceiptNo"] != null)
                    {

                        string clientid = Request.QueryString["clientid"].ToString();
                        ddlClientID.SelectedValue = clientid;
                        ddlClientID_SelectedIndexChanged(sender, e);
                        string ReceiptNo = Request.QueryString["ReceiptNo"].ToString();
                        ddlRecieptNo.SelectedValue = ReceiptNo;
                        ddlRecieptNo_SelectedIndexChanged(sender, e);
                    }


                }
            }
            catch (Exception eX)
            {
                Response.Redirect("Login.aspx");
                return;
            }
        }

        protected void GetWebConfigdata()
        {
            //EmpIDPrefix = Session["EmpIDPrefix"].ToString();
            //Elength = (EmpIDPrefix.Trim().Length + 1).ToString();
            //CmpIDPrefix = Session["CmpIDPrefix"].ToString();
            //Clength = (CmpIDPrefix.Trim().Length + 1).ToString();
        }



        protected void LoadClientIdAndName()
        {
            string SqlqryForClientIdAndName = string.Empty;

            SqlqryForClientIdAndName = "Select clientid from clients   Order By Clientid";
            DataTable dtForClientIdAndName = config.ExecuteAdaptorAsyncWithQueryParams(SqlqryForClientIdAndName).Result;

            if (dtForClientIdAndName.Rows.Count > 0)
            {
                ddlClientID.DataTextField = "Clientid";
                ddlClientID.DataValueField = "Clientid";
                ddlClientID.DataSource = dtForClientIdAndName;
                ddlClientID.DataBind();
            }
            ddlClientID.Items.Insert(0, "-Select-");
            dtForClientIdAndName = null;

            SqlqryForClientIdAndName = "Select clientid,Clientname from clients where clientid like '" + CmpIDPrefix + "%' Order By Clientname";
            dtForClientIdAndName = config.ExecuteAdaptorAsyncWithQueryParams(SqlqryForClientIdAndName).Result;

            if (dtForClientIdAndName.Rows.Count > 0)
            {
                ddlCName.DataTextField = "Clientname";
                ddlCName.DataValueField = "Clientid";
                ddlCName.DataSource = dtForClientIdAndName;
                ddlCName.DataBind();
            }
            ddlCName.Items.Insert(0, "-Select-");
        }

        protected void PreviligeUsers(int previligerid)
        {

            switch (previligerid)
            {

                case 1:
                    break;
                case 2:
                    AddClientLink.Visible = true;
                    ModifyClientLink.Visible = false;
                    DeleteClientLink.Visible = false;
                    ContractLink.Visible = false;
                    //ClientAttenDanceLink.Visible = false;
                    Operationlink.Visible = false;
                    MRFLink.Visible = false;

                    CompanyInfoLink.Visible = false;
                    InventoryLink.Visible = false;
                    ReportsLink.Visible = false;
                    SettingsLink.Visible = false;

                    break;

                case 3:

                    AddClientLink.Visible = true;
                    ModifyClientLink.Visible = false;
                    DeleteClientLink.Visible = false;
                    ContractLink.Visible = false;
                    // ClientAttenDanceLink.Visible = true;
                    LicensesLink.Visible = false;
                    BillingLink.Visible = true;
                    Operationlink.Visible = false;
                    MRFLink.Visible = false;
                    CompanyInfoLink.Visible = false;

                    InventoryLink.Visible = true;
                    ReportsLink.Visible = false;
                    SettingsLink.Visible = true;
                    break;
                case 4:

                    AddClientLink.Visible = false;
                    ModifyClientLink.Visible = false;
                    DeleteClientLink.Visible = false;
                    ContractLink.Visible = false;
                    // ClientAttenDanceLink.Visible = false;
                    Operationlink.Visible = false;
                    MRFLink.Visible = false;
                    CompanyInfoLink.Visible = false;
                    InventoryLink.Visible = false;
                    ReportsLink.Visible = false;
                    SettingsLink.Visible = false;
                    break;
                case 5:

                    AddClientLink.Visible = false;
                    ModifyClientLink.Visible = true;
                    DeleteClientLink.Visible = false;
                    ContractLink.Visible = false;
                    ClientAttendanceLink.Visible = true;
                    Operationlink.Visible = false;
                    LicensesLink.Visible = true;
                    MRFLink.Visible = true;

                    EmployeesLink.Visible = true;
                    ClientsLink.Visible = true;
                    CompanyInfoLink.Visible = false;
                    InventoryLink.Visible = false;
                    ReportsLink.Visible = true;
                    ReportsLink.Visible = true;
                    SettingsLink.Visible = true;
                    break;
                case 6:
                    AddClientLink.Visible = false;
                    ModifyClientLink.Visible = false;
                    DeleteClientLink.Visible = false;
                    ContractLink.Visible = false;
                    //ClientAttenDanceLink.Visible = true;


                    EmployeesLink.Visible = false;
                    LicensesLink.Visible = false;
                    ClientAttendanceLink.Visible = false;
                    InventoryLink.Visible = true;
                    ReportsLink.Visible = true;
                    Operationlink.Visible = false;
                    MRFLink.Visible = true;

                    CompanyInfoLink.Visible = false;
                    SettingsLink.Visible = false;

                    break;
                default:
                    break;


            }
        }

        protected void ddlClientID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlClientID.SelectedIndex > 0)
            {
                ddlCName.SelectedValue = ddlClientID.SelectedValue;
                Clear();
                LoadAllReceipts();
            }
            else
            {
                ddlCName.SelectedIndex = 0;
            }
        }

        protected void ddlCName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCName.SelectedIndex > 0)
            {
                ddlClientID.SelectedValue = ddlCName.SelectedValue;
                Clear();
                LoadAllReceipts();
            }
            else
            {
                ddlClientID.SelectedIndex = 0;
            }
        }

        protected void LoadAllReceipts()
        {
            string qry = "select distinct ReceiptNo from Receiptdetails where clientid='" + ddlClientID.SelectedValue + "'";
            DataTable dt = config.ExecuteAdaptorAsyncWithQueryParams(qry).Result;
            if (dt.Rows.Count > 0)
            {
                ddlRecieptNo.DataTextField = "ReceiptNo";
                ddlRecieptNo.DataValueField = "ReceiptNo";
                ddlRecieptNo.DataSource = dt;
                ddlRecieptNo.DataBind();
            }

            ddlRecieptNo.Items.Insert(0, "-Select-");

        }

        protected void LoadAlltheBills()
        {
            Clear();
            var SPName = "LoadAllReceipts";
            Hashtable htnew = new Hashtable();
            htnew.Add("@ClientID", ddlClientID.SelectedValue);
            htnew.Add("@ReceiptNo", ddlRecieptNo.SelectedValue);
            DataTable DtForBills = config.ExecuteAdaptorAsyncWithParams(SPName, htnew).Result;

            if (DtForBills.Rows.Count > 0)
            {
                gvreciepts.DataSource = DtForBills;
                gvreciepts.DataBind();


                if (gvreciepts.Rows.Count > 0)
                {

                    txtAmount.Text = DtForBills.Rows[0]["Recievdamt"].ToString();
                    txtbankname.Text = DtForBills.Rows[0]["Bankname"].ToString();
                    txtddorcheckno.Text = DtForBills.Rows[0]["DDorCheckno"].ToString();
                    txtddorcheckdate.Text = DtForBills.Rows[0]["DDorCheckDate"].ToString();
                    ddlReciveMode.SelectedIndex = int.Parse(DtForBills.Rows[0]["RecievedMode"].ToString());
                    txtDate.Text = DtForBills.Rows[0]["RecievedDate"].ToString();
                    txtextraAmount.Text = DtForBills.Rows[0]["ExtraAmount"].ToString();


                }


            }
            else
            {
                gvreciepts.DataSource = null;
                gvreciepts.DataBind();
                ScriptManager.RegisterStartupScript(this, GetType(), "Show Alert", "alert('There is no bills for Selected Client')", true);
                return;
            }

        }




        protected void Clear()
        {
            ddlReciveMode.SelectedIndex = 0;
            ddlDDOrCheckstatus.SelectedIndex = 0;
            txtddorcheckdate.Text = "";
            txtAmount.Text = "";
            txtbankname.Text = "";
            txtDate.Text = "";
            txtddorcheckno.Text = "";
            txtextraAmount.Text = "";
            gvreciepts.DataSource = null;
            gvreciepts.DataBind();
        }


        string mvalue = "";
        string monthval = "";
        string yearvalue = "";
        decimal totalgrandtotal = 0;
        decimal totaltdsamt = 0;
        decimal totalnetpayable = 0;
        decimal totalpendingamt = 0;
        decimal totalreceivedamt = 0;
        decimal totaldueamt = 0;


        protected void gvreciepts_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                decimal grandtotal = decimal.Parse(((Label)e.Row.FindControl("lblgrandtotal")).Text);
                totalgrandtotal += grandtotal;

                decimal tdsamt = decimal.Parse(((Label)e.Row.FindControl("txttdsamt")).Text);
                totaltdsamt += tdsamt;

                decimal netpayable = decimal.Parse(((Label)e.Row.FindControl("lblnetpaybleamt")).Text);
                totalnetpayable += netpayable;

                decimal pendingamt = decimal.Parse(((Label)e.Row.FindControl("lblpendingamt")).Text);
                totalpendingamt += pendingamt;

                decimal receivedamt = decimal.Parse(((Label)e.Row.FindControl("txtrecievedamt")).Text);
                totalreceivedamt += receivedamt;

                decimal dueamt = decimal.Parse(((Label)e.Row.FindControl("lbldueamt")).Text);
                totaldueamt += dueamt;


            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                ((Label)e.Row.FindControl("lblTotalgrandtotal")).Text = totalgrandtotal.ToString();
                ((Label)e.Row.FindControl("lblTotalTDSAmt")).Text = totaltdsamt.ToString();
                ((Label)e.Row.FindControl("lblTotalNetPayable")).Text = totalnetpayable.ToString();
                ((Label)e.Row.FindControl("lblTotalpendingamt")).Text = totalpendingamt.ToString();
                ((Label)e.Row.FindControl("lblTotalReceivedAmt")).Text = totalreceivedamt.ToString();
                ((Label)e.Row.FindControl("lblTotalDueAmt")).Text = totaldueamt.ToString();

            }
        }

        protected void ddlRecieptNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadAlltheBills();
        }
    }
}