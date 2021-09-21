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
using System.Data;
using KLTS.Data;
using System.Globalization;
using Jawan.Portal.DAL;
namespace Jawan.Portal
{
    public partial class WageSheetReportsonlyots : System.Web.UI.Page
    {
        AppConfiguration config = new AppConfiguration();
        GridViewExportUtil gve = new GridViewExportUtil();
        DataTable dt;
        string EmpIDPrefix = "";
        string CmpIDPrefix = "";
        string BranchID = "";
        string Elength = "";
        string Clength = "";
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

                    LoadClientList();
                    LoadClientNames();

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
                    EmployeeReportLink.Visible = false;

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
            ddlcname.Items.Insert(1, "ALL");

        }

        protected void LoadClientList()
        {
            DataTable DtClientNames = GlobalData.Instance.LoadCIds(CmpIDPrefix,BranchID);
            if (DtClientNames.Rows.Count > 0)
            {
                ddlclientid.DataValueField = "Clientid";
                ddlclientid.DataTextField = "Clientid";
                ddlclientid.DataSource = DtClientNames;
                ddlclientid.DataBind();
            }
            ddlclientid.Items.Insert(0, "-Select-");
            ddlclientid.Items.Insert(1, "ALL");
        }

        protected void ddlcname_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            ClearData();
            if (ddlcname.SelectedIndex > 0)
            {
                txtmonth.Text = "";
                ddlclientid.SelectedValue = ddlcname.SelectedValue;

            }
            else
            {
                ddlclientid.SelectedIndex = 0;
            }
        }

        protected void ddlclientid_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearData();

            if (ddlclientid.SelectedIndex > 0)
            {
                txtmonth.Text = "";
                ddlcname.SelectedValue = ddlclientid.SelectedValue;
            }
            else
            {
                ddlcname.SelectedIndex = 0;
            }
        }


        protected void btnsearch_Click(object sender, EventArgs e)
        {
            LblResult.Text = "";
            GVListEmployees.DataSource = null;
            GVListEmployees.DataBind();

            if (ddlclientid.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showlalert", "alert('Please Select Client Id/Name');", true);

                return;
            }

            if (txtmonth.Text.Trim().Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showlalert", "alert('Please Select Month');", true);

                return;
            }
            loadallsalarydetails();
        }

        protected void loadallsalarydetails()
        {
            string Clientid = ddlclientid.SelectedValue;

            string date = string.Empty;

            if (txtmonth.Text.Trim().Length > 0)
            {
                date = DateTime.Parse(txtmonth.Text.Trim(), CultureInfo.GetCultureInfo("en-gb")).ToString();
            }

            string month = DateTime.Parse(date).Month.ToString();
            string Year = DateTime.Parse(date).Year.ToString();
            string sqlqry = string.Empty;


            if (ddlclientid.SelectedIndex == 1)
            {
                sqlqry = "Select  EP.Empid,EP.Clientid,C.Clientname,(E.Empfname + E.EmpMname + E.emplname) as EmpMname ,EP.NoOfDuties," +
                          " ( rtrim(ltrim(E.Empbankacno)) ) as Empbankacno,E.EmpBankCardRef, EP.basic,EP.da,EP.noofduties,EP.ots,EP.Nhs,EP.Proftax, " +
                          " EP.gross as totalgross,EP.otamt,PFONOT as PF,EP.ESIONOT as ESI,EP.penalty,EP.owf,EP.saladvded,EP.CanteenAdv,EP.UniformDed,EP.OtherDed, " +
                          " EP.HRA,EP.CCA,EP.Conveyance,EP.OtherDed,EP.WashAllowance as  wa,d.Design,(EP.OTAmt-(EP.PFONOT+EP.ESIONOT)) as Netpay,  " +
                          " EP.OtherAllowance as  oa,EP.LeaveEncashAmt,EP.CLPLAmt,EP.WOAmt,EP.TotalDeductions   from  emppaysheet EP " +
                          " inner join Clients C on  C.Clientid = EP.Clientid " +
                          " inner join  empdetails E on E.Empid=EP.Empid   " +
                          " inner join  Empattendance EA on EA.Empid=EP.Empid  " +
                          " inner join Designations d on EP.Desgn=d.DesignId " +
                          "  and  EP.noofduties<>0  and EP.month='" + month + Year.Substring(2, 2) +
                          " '  and  EP.month=EA.Month  and  EP.Desgn=EA.Design  and   EP.Clientid=EA.Clientid and  EP.ots!=0 " +
                          "   order By   Right(EP.Clientid,4),   EP.empid ";

                Bindata(sqlqry);
                return;

            }
            sqlqry = "Select  EP.Empid,EP.Clientid,C.Clientname,(E.Empfname + E.EmpMname + E.emplname) as EmpMname ," +
                       " ( rtrim(ltrim(E.Empbankacno)) ) as Empbankacno,E.EmpBankCardRef, EP.basic,EP.da,EP.HRA,d.Design, " +
                       " EP.CCA,EP.Conveyance,EP.Washallowance,EP.OtherAllowance,EP.noofduties,EP.ots,EP.nhs,EP.Proftax, " +
                       " EP.gross as totalgross,EP.otamt,PFONOT as PF,EP.ESIONOT as ESI,EP.penalty,EP.owf,EP.saladvded,EP.CanteenAdv,EP.UniformDed,EP.OtherDed, " +
                       " EP.HRA,EP.CCA,EP.Conveyance,EP.OtherDed,EP.WashAllowance as  wa,(EP.OTAmt-(EP.PFONOT+EP.ESIONOT)) as Netpay,  " +
                       " EP.OtherAllowance as  oa,EP.LeaveEncashAmt,EP.CLPLAmt,EP.WOAmt,EP.TotalDeductions   from  emppaysheet EP " +
                       " inner join Clients C on  C.Clientid = EP.Clientid " +
                       " inner join  empdetails E on E.Empid=EP.Empid   " +
                  " inner join  Empattendance EA on EA.Empid=EP.Empid  " +
                  " inner join Designations d on EP.Desgn=d.DesignId " +
                  " and    EP.Clientid='" + ddlclientid.SelectedValue + "'   and    EP.noofduties<>0  and EP.month='" + month + Year.Substring(2, 2) +
                  " '  and  EP.month=EA.Month  and  EP.Clientid=EA.Clientid   and  EP.Desgn=EA.Design and  EP.ots!=0  " +
                  "   order By   Right(EP.Clientid,4) ,EP.empid";



            Bindata(sqlqry);

        }

        protected void Bindata(string Sqlqry)
        {
            DataTable dt = config.ExecuteAdaptorAsyncWithQueryParams(Sqlqry).Result;
            if (dt.Rows.Count > 0)
            {
                GVListEmployees.DataSource = dt;
                GVListEmployees.DataBind();
                // Fillpfandesidetails();
                lbtn_Export.Visible = true;
            }
            else
            {
                GVListEmployees.DataSource = null;
                GVListEmployees.DataBind();
                ScriptManager.RegisterStartupScript(this, GetType(), "showlalert", "alert('There Is No Salary Details For The Selected client');", true);

            }
        }

        protected void Fillpfandesidetails()
        {
            string Clientid = ddlclientid.SelectedValue;
            string date = string.Empty;

            if (txtmonth.Text.Trim().Length > 0)
            {
                date = DateTime.Parse(txtmonth.Text.Trim(), CultureInfo.GetCultureInfo("en-gb")).ToString();
            }

            string month = DateTime.Parse(date).Month.ToString();
            string Year = DateTime.Parse(date).Year.ToString();
            for (int i = 0; i < GVListEmployees.Rows.Count; i++)
            {
                string empidforempname = GVListEmployees.Rows[i].Cells[2].Text;
                string Sqlqry = "Select E.Empmname,EA.ot from Empdetails E inner join Empattendance   " +
                       "  EA on E.empid=EA.empid and   E.Empid='" + empidforempname + "' and EA.Clientid='" +
                        ddlclientid.SelectedValue + "' and month='" + month + Year.Substring(2, 2)
                        + "' and  EA.ClientId='" + ddlclientid.SelectedValue + "'";
                DataTable dtempname = config.ExecuteAdaptorAsyncWithQueryParams(Sqlqry).Result;

                if (dtempname.Rows.Count > 0)
                {

                }

                string sqlqryforemprpfesi = "select pf.emprpf,esi.empresi from emppaysheet EP inner join Epfded pf" +
                    " on pf.empid=EP.Empid  inner join Esided esi on esi.empid=EP.empid  and EP.clientid='" + ddlclientid.SelectedValue + "'";
                DataTable dtemprpfandesi = config.ExecuteAdaptorAsyncWithQueryParams(sqlqryforemprpfesi).Result;
                if (dtemprpfandesi.Rows.Count > 0)
                {
                    Label emprpf = GVListEmployees.Rows[i].FindControl("lblpfempr") as Label;
                    emprpf.Text = dtemprpfandesi.Rows[0]["emprpf"].ToString();

                    float temprpf = 0;
                    if (emprpf.Text.Trim().Length != 0)
                    {
                        temprpf = float.Parse(emprpf.Text);
                    }

                    string Epf = GVListEmployees.Rows[i].Cells[10].Text;
                    float emppf = 0;
                    if (String.IsNullOrEmpty(Epf.Trim()) != null)
                    {
                        if (Epf.Trim().Length != 0)
                        {
                            emppf = float.Parse(Epf);
                        }
                    }

                    float totalpf = temprpf + emppf;
                    Label ttotalpf = GVListEmployees.Rows[i].FindControl("lblpftotal") as Label;
                    ttotalpf.Text = totalpf.ToString();

                    Label empresi = GVListEmployees.Rows[i].FindControl("lblempresi") as Label;
                    empresi.Text = dtemprpfandesi.Rows[0]["empresi"].ToString();

                    float tempresi = 0;
                    if (empresi.Text.Trim().Length != 0)
                    {
                        tempresi = float.Parse(empresi.Text);
                    }
                    string Eesi = GVListEmployees.Rows[i].Cells[14].Text;
                    float empesi = 0;
                    if (String.IsNullOrEmpty(Eesi.Trim()) != null)
                    {
                        if (Eesi.Trim().Length != 0)
                        {
                            empesi = float.Parse(Eesi);
                        }
                    }
                    float ttotalesi = tempresi + empesi;
                    Label totalesi = GVListEmployees.Rows[i].FindControl("lblesitotal") as Label;
                    totalesi.Text = ttotalesi.ToString();
                }

                string sqlqryforesipfacno = "Select  EMPEPFCodes.EmpEpfNo,   EMPESICodes.EmpESINo, Empdetails.EmpBankAcNo   FRom Empdetails,EMPEPFCodes,EMPESICodes where   Empdetails.empid='" + empidforempname + "'";
                DataTable dtforpfesinos = config.ExecuteAdaptorAsyncWithQueryParams(sqlqryforesipfacno).Result;

                if (dtforpfesinos.Rows.Count > 0)
                {
                    Label pfno = GVListEmployees.Rows[i].FindControl("lblpfno") as Label;
                    pfno.Text = dtforpfesinos.Rows[0]["EmpEpfNo"].ToString();
                    Label esino = GVListEmployees.Rows[i].FindControl("lblpfno") as Label;
                    esino.Text = dtforpfesinos.Rows[0]["EmpESINo"].ToString();
                    Label acno = GVListEmployees.Rows[i].FindControl("lblacno") as Label;
                    acno.Text = dtforpfesinos.Rows[0]["EmpBankAcNo"].ToString();
                }
            }
        }

        protected void ClearData()
        {
            LblResult.Text = "";
            GVListEmployees.DataSource = null;
            GVListEmployees.DataBind();
            lbtn_Export.Visible = false;
        }

        protected void lbtn_Export_Click(object sender, EventArgs e)
        {
            gve.Export("AllUnitsEsiReport.xls", this.GVListEmployees);
        }

        protected void GVListEmployees_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GVListEmployees.PageIndex = e.NewPageIndex;
            loadallsalarydetails();
        }


        #region  Begin  Code for Declare Grand Total Variables as on [11-01-2013]

        float totalDuties = 0;
        float totalOTs = 0;
        float totalBasic = 0;
        float totalDA = 0;
        float totalHRA = 0;
        float totalCCA = 0;
        float totalConveyance = 0;
        float totalWA = 0;
        float totalOA = 0;
        float totalLeaveEncashAmt = 0;
        float totalLEA = 0;
        float totalOTAmt = 0;
        float totalCLPLAmt = 0;
        float totalWOAmt = 0;
        float totalGross = 0;
        float totalPF = 0;
        float totalESI = 0;
        float totalPT = 0;
        float totalSalAdvDed = 0;
        float totalUniFormDed = 0;
        float totalwamt = 0;
        float totalWAmt = 0;
        float totalOtherDed = 0;
        float totalCanteenAdv = 0;
        float totalPenalty = 0;
        float totalDED = 0;
        float totalActualAmount = 0;


        #endregion end code For Declare  Grand Total Variables as on [11-01-2013]


        protected void GVListEmployees_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {


            }



            // string empno = GVListEmployees.Rows[e.Row.RowIndex].Cells[0].Text;


            //if(IsPostBack)
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{

            //    string empno = e.Row.Cells[2].Text;
            //    Label lblpfno = (Label)e.Row.FindControl("lblpfno");
            //    Label lblesino = (Label)e.Row.FindControl("lblesino");
            //    string sqlqry = " Select  EP.empepfno,ES.EmpEsino from EMPESICodes as ES,EMPEPFCodes as  EP where ES.Empid='" + empno +
            //        "' or EP.Empid='" + empno + "'";


            //    DataTable dtpfesinos = SqlHelper.Instance.GetTableByQuery(sqlqry);
            //    if (dtpfesinos.Rows.Count > 0)
            //    {
            //        lblpfno.Text = dtpfesinos.Rows[0]["empepfno"].ToString();
            //        lblesino.Text = dtpfesinos.Rows[0]["EmpEsino"].ToString();
            //    }
            //    else
            //    {
            //        lblpfno.Text = "NA";
            //        lblesino.Text = "NA";

            //    }
            //}
            #region Old Code



            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    float Duties = float.Parse(((Label)e.Row.FindControl("lblNoOfDuties")).Text);
            //    totalDuties += Duties;
            //    float OTs = float.Parse(((Label)e.Row.FindControl("lblot")).Text);
            //    totalOTs += OTs;
            //    float Basic = float.Parse(((Label)e.Row.FindControl("lblbasic")).Text);
            //    totalBasic += Basic;
            //    float DA = float.Parse(((Label)e.Row.FindControl("lblda")).Text);
            //    totalDA += DA;
            //    float HRA = float.Parse(((Label)e.Row.FindControl("lblHRA")).Text);
            //    totalHRA += HRA;
            //    float CCA = float.Parse(((Label)e.Row.FindControl("lblCCA")).Text);
            //    totalCCA += CCA;
            //    float Conveyance = float.Parse(((Label)e.Row.FindControl("lblConveyance")).Text);
            //    totalConveyance += Conveyance;
            //    float WA = float.Parse(((Label)e.Row.FindControl("lblWA")).Text);
            //    totalWA += WA;
            //    float OA = float.Parse(((Label)e.Row.FindControl("lblOA")).Text);
            //    totalOA += OA;
            //    float LeaveEncashAmt = float.Parse(((Label)e.Row.FindControl("lblLeaveEncashAmt")).Text);
            //    totalLeaveEncashAmt += LeaveEncashAmt;
            //    float LEA = float.Parse(((Label)e.Row.FindControl("lblLEA")).Text);
            //    totalLEA += LEA;
            //    float OTAmt = float.Parse(((Label)e.Row.FindControl("lblOTAmt")).Text);
            //    totalDA += OTAmt;
            //    //float CLPLAmt = float.Parse(((Label)e.Row.FindControl("lblclplamt")).Text);
            //    //totalCLPLAmt += CLPLAmt;
            //    float WOAmt = float.Parse(((Label)e.Row.FindControl("lblwoamt")).Text);
            //    totalWOAmt += WOAmt;
            //    //float Gross = float.Parse(((Label)e.Row.FindControl("lblgross")).Text);
            //    //totalGross += Gross;
            //    float PF = float.Parse(((Label)e.Row.FindControl("lblpf")).Text);
            //    totalPF += PF;
            //    float ESI = float.Parse(((Label)e.Row.FindControl("lblesi")).Text);
            //    totalESI += ESI;
            //    float PT = float.Parse(((Label)e.Row.FindControl("lblpt")).Text);
            //    totalPT += PT;
            //    float SalAdvDed = float.Parse(((Label)e.Row.FindControl("lblsaladvded")).Text);
            //    totalSalAdvDed += SalAdvDed;
            //    float UniFormDed = float.Parse(((Label)e.Row.FindControl("lblUniformDed")).Text);
            //    totalUniFormDed += UniFormDed;
            //    float wamt = float.Parse(((Label)e.Row.FindControl("lblwamts")).Text);
            //    totalwamt += wamt;
            //    float WAmt = float.Parse(((Label)e.Row.FindControl("lblWAmt")).Text);
            //    totalWAmt += WAmt;
            //    float OtherDed = float.Parse(((Label)e.Row.FindControl("lblOtherDed")).Text);
            //    totalOtherDed += OtherDed;
            //    float CanteenAdv = float.Parse(((Label)e.Row.FindControl("lblCanteenAdv")).Text);
            //    totalCanteenAdv += CanteenAdv;
            //    float Penalty = float.Parse(((Label)e.Row.FindControl("lblpenalty")).Text);
            //    totalPenalty += Penalty;
            //    float Deductions = float.Parse(((Label)e.Row.FindControl("lblDeductions")).Text);
            //    totalDED += Deductions;
            //    float ActualAmount = float.Parse(((Label)e.Row.FindControl("lblactualamount")).Text);
            //    totalActualAmount += ActualAmount;

            //}
            //if (e.Row.RowType == DataControlRowType.Footer)
            //{
            //    ((Label)e.Row.FindControl("lblTotalNoOfDuties")).Text = totalDuties.ToString();
            //    ((Label)e.Row.FindControl("lblTotalot")).Text = totalOTs.ToString();
            //    ((Label)e.Row.FindControl("lblTotalbasic")).Text = totalBasic.ToString();
            //    ((Label)e.Row.FindControl("lblTotalda")).Text = totalDA.ToString();
            //    ((Label)e.Row.FindControl("lblTotalHRA")).Text = totalHRA.ToString();
            //    ((Label)e.Row.FindControl("lblTotalCCA")).Text = totalCCA.ToString();
            //    ((Label)e.Row.FindControl("lblTotalConveyance")).Text = totalConveyance.ToString();
            //    ((Label)e.Row.FindControl("lblTotalWA")).Text = totalWA.ToString();
            //    ((Label)e.Row.FindControl("lblTotalOA")).Text = totalOA.ToString();
            //    ((Label)e.Row.FindControl("lblTotalLeaveEncashAmt")).Text = totalLeaveEncashAmt.ToString();
            //    ((Label)e.Row.FindControl("lblTotalLEA")).Text = totalLEA.ToString();
            //    ((Label)e.Row.FindControl("lblTotalOTAmt")).Text = totalOTAmt.ToString();
            //    //((Label)e.Row.FindControl("lblTotalclplamt")).Text = totalCLPLAmt.ToString();
            //    ((Label)e.Row.FindControl("lblTotalwoamt")).Text = totalWOAmt.ToString();
            //    //((Label)e.Row.FindControl("lblTotalgross")).Text = totalGross.ToString();
            //    ((Label)e.Row.FindControl("lblTotalpf")).Text = totalPF.ToString();
            //    ((Label)e.Row.FindControl("lblTotalesi")).Text = totalESI.ToString();
            //    ((Label)e.Row.FindControl("lblTotalpt")).Text = totalPT.ToString();
            //    ((Label)e.Row.FindControl("lblTotalsaladvded")).Text = totalSalAdvDed.ToString();
            //    ((Label)e.Row.FindControl("lblTotalUniformDed")).Text = totalUniFormDed.ToString();
            //    ((Label)e.Row.FindControl("lblTotalwamts")).Text = totalwamt.ToString();
            //    ((Label)e.Row.FindControl("lblTotalWAmt")).Text = totalWAmt.ToString();
            //    ((Label)e.Row.FindControl("lblTotalOtherDed")).Text = totalOtherDed.ToString();
            //    ((Label)e.Row.FindControl("lblTotalCanteenAdv")).Text = totalCanteenAdv.ToString();
            //    ((Label)e.Row.FindControl("lblTotalpenalty")).Text = totalPenalty.ToString();
            //    ((Label)e.Row.FindControl("lblTotalDeductions")).Text = totalDED.ToString();
            //    ((Label)e.Row.FindControl("lblTotalactualamount")).Text = totalActualAmount.ToString();
            //}



            #endregion

        }
    }
}