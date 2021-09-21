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
using Jawan.Portal.DAL;
namespace Jawan.Portal
{
    public partial class ReportforEsiDetails : System.Web.UI.Page
    {
        AppConfiguration config = new AppConfiguration();
        GridViewExportUtil gve = new GridViewExportUtil();
        string EmpIDPrefix = "";
        string CmpIDPrefix = "";
        string Elength = "";
        string Clength = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            GetWebConfigdata();
            LblResult.Visible = true;
            LblResult.Text = "";
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
                //LoadClientIdAndName();
                LoadEsibranches();
            }
        }


        protected void GetWebConfigdata()
        {
            EmpIDPrefix = Session["EmpIDPrefix"].ToString();
            CmpIDPrefix = Session["CmpIDPrefix"].ToString();
        }

        protected void Fillcname()
        {
            string SqlQryForCname = "Select Clientid from Clients where clientid='" + ddlclient.SelectedValue + "'";
            DataTable dtCname = config.ExecuteAdaptorAsyncWithQueryParams(SqlQryForCname).Result;
            if (dtCname.Rows.Count > 0)
                ddlcname.SelectedValue = dtCname.Rows[0]["Clientid"].ToString();

        }

        protected void FillClientid()
        {
            string SqlQryForCid = "Select Clientid from Clients where Clientid='" + ddlcname.SelectedValue + "'";
            DataTable dtCname = config.ExecuteAdaptorAsyncWithQueryParams(SqlQryForCid).Result;
            if (dtCname.Rows.Count > 0)
                ddlclient.SelectedValue = dtCname.Rows[0]["Clientid"].ToString();


        }

        protected void LoadClientIdAndName()
        {
            string selectquery;
            string cmpidprefix = "01/";
            if (CmpIDPrefix == cmpidprefix)
            {
                selectquery = "select clientid from clients  order by  clientid";
            }
            else
            {
                selectquery = "select clientid from clients  Where Clientid like '%" + CmpIDPrefix + "%' order by clientid";
            }



            DataTable dtForClientIdAndName = config.ExecuteAdaptorAsyncWithQueryParams(selectquery).Result;

            if (dtForClientIdAndName.Rows.Count > 0)
            {
                ddlclient.DataTextField = "Clientid";
                ddlclient.DataValueField = "Clientid";
                ddlclient.DataSource = dtForClientIdAndName;
                ddlclient.DataBind();
            }
            ddlclient.Items.Insert(0, "-Select-");
            ddlclient.Items.Insert(1, "All");
            dtForClientIdAndName = null;

            if (CmpIDPrefix == cmpidprefix)
            {
                selectquery = "select clientid,Clientname from clients  order by  Clientname";
            }
            else
            {
                selectquery = "select clientid,Clientname from clients  Where Clientid like '%" + CmpIDPrefix + "%' order by  Clientname";
            }


            dtForClientIdAndName = config.ExecuteAdaptorAsyncWithQueryParams(selectquery).Result;

            if (dtForClientIdAndName.Rows.Count > 0)
            {
                ddlcname.DataTextField = "Clientname";
                ddlcname.DataValueField = "Clientid";
                ddlcname.DataSource = dtForClientIdAndName;
                ddlcname.DataBind();
            }
            ddlcname.Items.Insert(0, "-Select-");
            ddlcname.Items.Insert(1, "All");
        }

        protected void ClearGridData()
        {
            LblResult.Text = "";
            GVListOfClients.DataSource = null;
            GVListOfClients.DataBind();
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
                    EmployeesLink.Visible = true;
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
                    EmployeesLink.Visible = true;
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
                    EmployeesLink.Visible = true;
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

        protected void ddlclient_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClearGridData();
            if (ddlclient.SelectedIndex == 1)
            {
                ddlcname.SelectedIndex = 1;
            }
            else if (ddlclient.SelectedIndex > 1)
            {
                Fillcname();
            }
            else
            {
                ddlcname.SelectedIndex = 0;
            }

        }

        protected void ddlcname_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            ClearGridData();
            if (ddlcname.SelectedIndex == 1)
            {
                ddlclient.SelectedIndex = 1;
            }
            else if (ddlcname.SelectedIndex > 1)
            {
                FillClientid();
            }
            else
            {
                ddlclient.SelectedIndex = 0;
            }
        }


        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            GVListOfClients.DataSource = null;
            GVListOfClients.DataBind();
            LblResult.Text = "";
            DataTable dtone = null;
            DataTable dttwo = null;
            string Sqlqry1 = "";
            string Sqlqry2 = "";
            //if (ddlclient.SelectedIndex == 0)
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "showlalert", "alert('Please Select Client Id');", true);
            //    return;
            //}
            if (txtmonth.Text.Trim().Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showlalert", "alert('Please Select Month');", true);
                return;
            }

            string month = DateTime.Parse(txtmonth.Text.Trim()).Month.ToString();
            string Year = DateTime.Parse(txtmonth.Text.Trim()).Year.ToString();
            DateTime date = DateTime.Now;

            #region Begin Old Code as on 19/08/2014 by Venkat



            //        if (Chkmonth.Checked)
            //        {
            //            if (ddlclient.SelectedIndex == 1)
            //            {
            //                #region Old code on 19/08/2014


            //     //           Sqlqry1 = "select esi.EmpESINo as EmpESINo,ed.empid,ed.EmpDtofJoining,ISNULL(ed.EmpFName,'')+' '+ISNULL(ed.empmname,'')+' " +
            //     //                " '+ISNULL(ed.emplname,'') as Fullname,round(sum((eps.NoOfDuties+isnull(eps.ots,'')+ " +
            //     //                " ISNULL(eps.NHS,'')+ISNULL(eps.Npots,''))),0) as NoOfDuties,round(sum(eps.ESIWAGES),0) as ESIWAGES," +
            //     //                " round(sum(isnull(eps.OTAmt,'')+ISNULL(eps.Nhsamt,'')+ISNULL(eps.Npotsamt,'')),0) as Otamt," +
            //     //                "round(sum( isnull( eps.ESIWAGES,0) +isnull(eps.OTAmt,'')+ISNULL(eps.Nhsamt,'')+ISNULL(eps.Npotsamt,'')),0) as TotalMonthlywages,"+
            //     //                " cts.ESIonOT, "+

            //     //"  EsiCuttingBranch  = case " +
            //     // " When   ed.ESICuttingBranch ='0' Then  'NA' " +
            //     //  "When    ed.ESICuttingBranch ='1'Then  ECBD.EsiBranchname " +
            //     //  "When    ed.ESICuttingBranch ='2' Then   ECBD.EsiBranchname " +

            //     //  "When   ed.ESICuttingBranch ='3' Then  ECBD.EsiBranchname " +
            //     //  "When    ed.ESICuttingBranch ='4'Then  ECBD.EsiBranchname " +
            //     //  "When    ed.ESICuttingBranch ='5' Then   ECBD.EsiBranchname " +

            //     //   "When  ed.ESICuttingBranch ='6' Then  ECBD.EsiBranchname " +
            //     //  "When    ed.ESICuttingBranch ='7'Then  ECBD.EsiBranchname " +
            //     //  "When    ed.ESICuttingBranch ='8' Then   ECBD.EsiBranchname " +

            //     //   "When   ed.ESICuttingBranch ='9' Then  ECBD.EsiBranchname " +
            //     //  "When    ed.ESICuttingBranch ='10'Then  ECBD.EsiBranchname " +
            //     //  "When    ed.ESICuttingBranch ='11' Then   ECBD.EsiBranchname " +
            //     //  "When    ed.ESICuttingBranch ='12' Then   ECBD.EsiBranchname " +
            //     // " When    ed.ESICuttingBranch ='13' Then   ECBD.EsiBranchname " +

            //     //  " end " +
            //     //                " from EmpPaySheet Eps inner join EmpDetails " +
            //     //                " Ed on ed.EmpId=eps.EmpId inner join EMPESICodes Esi on ed.EmpId=esi.Empid inner join "+
            //     //                " Contracts Cts on eps.ClientId=cts.ClientId  inner join EsiBranchDetails  ECBD on  ECBD.EsiBranchid=ed.ESICuttingBranch " +
            //     //                "  where ed.EmpDtofJoining between '08/01/2013' " +
            //     //                " and '" + date + "' and cts.ESIonOT=1 and ed.EmpESIDeduct=1 and eps.ESI>0  "+
            //     //                " group by esi.EmpESINo,ed.EmpFName,ed.EmpMName," +
            //                //                " ed.EmpLName,cts.ESIonOT,ed.EmpId,ed.EmpDtofJoining, ed.ESICuttingBranch ,ECBD.EsiBranchname   order by ed.empid ";

            //                #endregion

            //                #region Old code on 19/08/2014

            //     //           Sqlqry2 = "select esi.EmpESINo as EmpESINo,ed.empid,ed.EmpDtofJoining,ISNULL(ed.EmpFName,'')+' '+ISNULL(ed.empmname,'')+' " +
            //     //                " '+ISNULL(ed.emplname,'') as Fullname,round(sum(eps.NoOfDuties),0) as NoOfDuties,"+
            //     //                " round(sum(eps.ESIWAGES),0) as ESIWAGES,round(sum(isnull(eps.OTAmt,'')+ISNULL(eps.Nhsamt,'')+ISNULL(eps.Npotsamt,'')),0) as OTAmt," +
            //     //                " round(sum( isnull( eps.ESIWAGES,0) +isnull(eps.OTAmt,'')+ISNULL(eps.Nhsamt,'')+ISNULL(eps.Npotsamt,'')),0) as TotalMonthlywages, "+
            //     //                "cts.ESIonOT, " +

            //     //"  EsiCuttingBranch  = case " +
            //     // " When   ed.ESICuttingBranch ='0' Then  'NA' " +
            //     //  "When    ed.ESICuttingBranch ='1'Then  ECBD.EsiBranchname " +
            //     //  "When    ed.ESICuttingBranch ='2' Then   ECBD.EsiBranchname " +

            //     //  "When   ed.ESICuttingBranch ='3' Then  ECBD.EsiBranchname " +
            //     //  "When    ed.ESICuttingBranch ='4'Then  ECBD.EsiBranchname " +
            //     //  "When    ed.ESICuttingBranch ='5' Then   ECBD.EsiBranchname " +

            //     //   "When  ed.ESICuttingBranch ='6' Then  ECBD.EsiBranchname " +
            //     //  "When    ed.ESICuttingBranch ='7'Then  ECBD.EsiBranchname " +
            //     //  "When    ed.ESICuttingBranch ='8' Then   ECBD.EsiBranchname " +

            //     //   "When   ed.ESICuttingBranch ='9' Then  ECBD.EsiBranchname " +
            //     //  "When    ed.ESICuttingBranch ='10'Then  ECBD.EsiBranchname " +
            //     //  "When    ed.ESICuttingBranch ='11' Then   ECBD.EsiBranchname " +
            //     //  "When    ed.ESICuttingBranch ='12' Then   ECBD.EsiBranchname " +
            //     // " When    ed.ESICuttingBranch ='13' Then   ECBD.EsiBranchname " +

            //     //  "end " +
            //     //                " from EmpPaySheet Eps inner join EmpDetails " +
            //     //                " Ed on ed.EmpId=eps.EmpId inner join EMPESICodes Esi on ed.EmpId=esi.Empid inner join " +
            //     //                " Contracts Cts on eps.ClientId=cts.ClientId   inner join EsiBranchDetails  ECBD on  ECBD.EsiBranchid=ed.ESICuttingBranch" +
            //     //                "  where ed.EmpDtofJoining between '08/01/2013'" +
            //     //                " and '" + date + "' and cts.ESIonOT=0 and ed.EmpESIDeduct=1 and eps.ESI>0  group by esi.EmpESINo,ed.EmpFName,ed.EmpMName," +
            //     //                " ed.EmpLName,cts.ESIonOT,ed.EmpId,ed.EmpDtofJoining,ed.ESICuttingBranch ,ECBD.EsiBranchname  order by ed.empid ";

            //                #endregion

            //            }
            //            if (ddlclient.SelectedIndex > 1)
            //            {
            //                #region Old code on 19/08/2014

            //     //           Sqlqry1 = "select esi.EmpESINo as EmpESINo,ed.empid,ed.EmpDtofJoining,ISNULL(ed.EmpFName,'')+' '+ISNULL(ed.empmname,'')+' " +
            //     //                " '+ISNULL(ed.emplname,'') as Fullname,round(sum((eps.NoOfDuties+isnull(eps.ots,'')+ " +
            //     //                " ISNULL(eps.NHS,'')+ISNULL(eps.Npots,''))),0) as NoOfDuties,round(sum(eps.ESIWAGES),0) as ESIWAGES," +
            //     //                " round(sum(isnull(eps.OTAmt,'')+ISNULL(eps.Nhsamt,'')+ISNULL(eps.Npotsamt,'')),0) as Otamt," +
            //     //                " round(sum(eps.ESIWAGES+isnull(eps.OTAmt,'')+ISNULL(eps.Nhsamt,'')+ISNULL(eps.Npotsamt,'')),0) as TotalMonthlywages, "+
            //     //                "cts.ESIonOT, "+


            //     //"  EsiCuttingBranch  = case " +
            //     // " When   ed.ESICuttingBranch ='0' Then  'NA' " +
            //     //  "When    ed.ESICuttingBranch ='1'Then  ECBD.EsiBranchname " +
            //     //  "When    ed.ESICuttingBranch ='2' Then   ECBD.EsiBranchname " +

            //     //  "When   ed.ESICuttingBranch ='3' Then  ECBD.EsiBranchname " +
            //     //  "When    ed.ESICuttingBranch ='4'Then  ECBD.EsiBranchname " +
            //     //  "When    ed.ESICuttingBranch ='5' Then   ECBD.EsiBranchname " +

            //     //   "When  ed.ESICuttingBranch ='6' Then  ECBD.EsiBranchname " +
            //     //  "When    ed.ESICuttingBranch ='7'Then  ECBD.EsiBranchname " +
            //     //  "When    ed.ESICuttingBranch ='8' Then   ECBD.EsiBranchname " +

            //     //   "When   ed.ESICuttingBranch ='9' Then  ECBD.EsiBranchname " +
            //     //  "When    ed.ESICuttingBranch ='10'Then  ECBD.EsiBranchname " +
            //     //  "When    ed.ESICuttingBranch ='11' Then   ECBD.EsiBranchname " +
            //     //  "When    ed.ESICuttingBranch ='12' Then   ECBD.EsiBranchname " +
            //     // " When    ed.ESICuttingBranch ='13' Then   ECBD.EsiBranchname " +

            //     //  " end " +

            //     //                " from EmpPaySheet Eps inner join EmpDetails " +
            //     //                " Ed on ed.EmpId=eps.EmpId inner join EMPESICodes Esi on ed.EmpId=esi.Empid inner join " +
            //     //                " Contracts Cts on eps.ClientId=cts.ClientId "+

            //     //                "  inner join EsiBranchDetails  ECBD on  ECBD.EsiBranchid=ed.ESICuttingBranch  " +

            //     //                "  where ed.EmpDtofJoining between '08/01/2013'" +
            //     //                " and '" + date + "' and cts.ESIonOT=1 and eps.clientid='" + ddlclient.SelectedValue + "'"+
            //     //                "  and ed.EmpESIDeduct=1  and eps.ESI>0 group by esi.EmpESINo,"+
            //     //                " ed.EmpFName,ed.EmpMName,ed.EmpLName,cts.ESIonOT,ed.EmpId,ed.EmpDtofJoining,ed.ESICuttingBranch ,ECBD.EsiBranchname  order by ed.empid";


            //     //           Sqlqry2 = "select esi.EmpESINo as EmpESINo,ed.empid,ed.EmpDtofJoining,ISNULL(ed.EmpFName,'')+' '+ISNULL(ed.empmname,'')+' " +
            //     //                " '+ISNULL(ed.emplname,'') as Fullname,round(sum(eps.NoOfDuties),0) as NoOfDuties,"+
            //     //                "round(sum(eps.ESIWAGES),0) as ESIWAGES,round(sum( isnull(eps.OTAmt,'')+ISNULL(eps.Nhsamt,'')+ISNULL(eps.Npotsamt,'')),0) as OTAmt," +
            //     //                "round(sum( isnull( eps.ESIWAGES,0) +isnull(eps.OTAmt,'')+ISNULL(eps.Nhsamt,'')+ISNULL(eps.Npotsamt,'')),0) as TotalMonthlywages,cts.ESIonOT,  " +


            //     //"  EsiCuttingBranch  = case " +
            //     // " When   ed.ESICuttingBranch ='0' Then  'NA' " +
            //     //  "When    ed.ESICuttingBranch ='1'Then  ECBD.EsiBranchname " +
            //     //  "When    ed.ESICuttingBranch ='2' Then   ECBD.EsiBranchname " +

            //     //  "When   ed.ESICuttingBranch ='3' Then  ECBD.EsiBranchname " +
            //     //  "When    ed.ESICuttingBranch ='4'Then  ECBD.EsiBranchname " +
            //     //  "When    ed.ESICuttingBranch ='5' Then   ECBD.EsiBranchname " +

            //     //   "When  ed.ESICuttingBranch ='6' Then  ECBD.EsiBranchname " +
            //     //  "When    ed.ESICuttingBranch ='7'Then  ECBD.EsiBranchname " +
            //     //  "When    ed.ESICuttingBranch ='8' Then   ECBD.EsiBranchname " +

            //     //   "When   ed.ESICuttingBranch ='9' Then  ECBD.EsiBranchname " +
            //     //  "When    ed.ESICuttingBranch ='10'Then  ECBD.EsiBranchname " +
            //     //  "When    ed.ESICuttingBranch ='11' Then   ECBD.EsiBranchname " +
            //     //  "When    ed.ESICuttingBranch ='12' Then   ECBD.EsiBranchname " +
            //     // " When    ed.ESICuttingBranch ='13' Then   ECBD.EsiBranchname " +

            //     //  " end " +

            //     //                " from EmpPaySheet Eps inner join EmpDetails " +
            //     //                " Ed on ed.EmpId=eps.EmpId inner join EMPESICodes Esi on ed.EmpId=esi.Empid inner join " +
            //     //                " Contracts Cts on eps.ClientId=cts.ClientId "+

            //     //                " inner join EsiBranchDetails  ECBD on  ECBD.EsiBranchid=ed.ESICuttingBranch " +
            //     //                " where ed.EmpDtofJoining between '08/01/2013' " +
            //     //                " and '" + date + "' and cts.ESIonOT=0 and eps.clientid='" + ddlclient.SelectedValue + "' "+
            //     //                " and ed.EmpESIDeduct=1  and eps.ESI>0 group by esi.EmpESINo,ed.EmpFName,ed.EmpMName,"+
            //     //                " ed.EmpLName,cts.ESIonOT,ed.EmpId,ed.EmpDtofJoining,ed.ESICuttingBranch ,ECBD.EsiBranchname  order by ed.empid";
            //           }

            //        }
            //        else
            //        {
            //            if (ddlclient.SelectedIndex == 1)
            //            {
            //                      string CompPrefix = "01/";
            //                      if (CmpIDPrefix == CompPrefix)
            //                      {




            //      //                    Sqlqry1 = "select esi.EmpESINo as EmpESINo,ed.empid, ed.EmpDtofJoining,ISNULL(ed.EmpFName,'')+' '+ISNULL(ed.empmname,'')+' " +
            //      //                         " '+ISNULL(ed.emplname,'') as Fullname, eps.NoOfDuties,   " +
            //      //                         " sum((isnull(eps.ots,'')+ ISNULL(eps.NHS,'')+ISNULL(eps.Npots,'')))  as TotalOts ,round(sum(eps.ESIWAGES),0) as ESIWAGES," +
            //      //                         " round(sum(isnull(eps.OTAmt,0)+ISNULL(eps.Nhsamt,0)+ISNULL(eps.Npotsamt,0)),0) as Otamt," +
            //      //                         " round(sum(eps.ESIWAGES+isnull(eps.OTAmt,0)+ISNULL(eps.Nhsamt,0)+ISNULL(eps.Npotsamt,0)),0) as TotalMonthlywages," +
            //      //                         " cts.ESIonOT,"+
            //      //                          "  EsiCuttingBranch  = case " +
            //      //" When   ed.ESICuttingBranch ='0' Then  'NA' " +
            //      // "When    ed.ESICuttingBranch ='1'Then  ECBD.EsiBranchname " +
            //      // "When    ed.ESICuttingBranch ='2' Then   ECBD.EsiBranchname " +

            //      // "When   ed.ESICuttingBranch ='3' Then  ECBD.EsiBranchname " +
            //      // "When    ed.ESICuttingBranch ='4'Then  ECBD.EsiBranchname " +
            //      // "When    ed.ESICuttingBranch ='5' Then   ECBD.EsiBranchname " +

            //      //  "When  ed.ESICuttingBranch ='6' Then  ECBD.EsiBranchname " +
            //      // "When    ed.ESICuttingBranch ='7'Then  ECBD.EsiBranchname " +
            //      // "When    ed.ESICuttingBranch ='8' Then   ECBD.EsiBranchname " +

            //      //  "When   ed.ESICuttingBranch ='9' Then  ECBD.EsiBranchname " +
            //      // "When    ed.ESICuttingBranch ='10'Then  ECBD.EsiBranchname " +
            //      // "When    ed.ESICuttingBranch ='11' Then   ECBD.EsiBranchname " +
            //      // "When    ed.ESICuttingBranch ='12' Then   ECBD.EsiBranchname " +
            //      //" When    ed.ESICuttingBranch ='13' Then   ECBD.EsiBranchname " +

            //      // "end " +

            //      //                         " from EmpPaySheet Eps inner join EmpDetails " +
            //      //                         " Ed on ed.EmpId=eps.EmpId inner join EMPESICodes Esi on ed.EmpId=esi.Empid inner join " +
            //      //                         " Contracts Cts on eps.ClientId=cts.ClientId " +
            //      //                         " inner join EsiBranchDetails  ECBD on  ECBD.EsiBranchid=ed.ESICuttingBranch " +
            //      //                         "  where " +
            //      //                         " eps.month='" + month + Year.Substring(2, 2) + "' and cts.ESIonOT=1 and eps.ESI>0  group by esi.EmpESINo," +
            //      //                         " ed.EmpFName,ed.EmpMName,ed.EmpLName,cts.ESIonOT,ed.EmpId,ed.EmpDtofJoining,ed.ESICuttingBranch ,ECBD.EsiBranchname  order by ed.empid";

            //#endregion

            //                      }
            //                      else
            //                      {

            //                  #region Old code on 19/08/2014

            //     //                     Sqlqry1 = "select esi.EmpESINo as EmpESINo,ed.empid, ed.EmpDtofJoining,ISNULL(ed.EmpFName,'')+' '+ISNULL(ed.empmname,'')+' " +
            //     //                          " '+ISNULL(ed.emplname,'') as Fullname,round(sum((eps.NoOfDuties+isnull(eps.ots,'')+ " +
            //     //                          " ISNULL(eps.NHS,'')+ISNULL(eps.Npots,''))),0) as NoOfDuties,round(sum(eps.ESIWAGES),0) as ESIWAGES," +
            //     //                          " round(sum(isnull(eps.OTAmt,0)+ISNULL(eps.Nhsamt,0)+ISNULL(eps.Npotsamt,0)),0) as Otamt," +
            //     //                          " round(sum(eps.ESIWAGES+isnull(eps.OTAmt,0)+ISNULL(eps.Nhsamt,'')+ISNULL(eps.Npotsamt,0)),0) as TotalMonthlywages," +
            //     //                          " cts.ESIonOT,  "+

            //     //"  EsiCuttingBranch  = case " +
            //     // " When   ed.ESICuttingBranch ='0' Then  'NA' " +
            //     //  "When    ed.ESICuttingBranch ='1'Then  ECBD.EsiBranchname " +
            //     //  "When    ed.ESICuttingBranch ='2' Then   ECBD.EsiBranchname " +

            //     //  "When   ed.ESICuttingBranch ='3' Then  ECBD.EsiBranchname " +
            //     //  "When    ed.ESICuttingBranch ='4'Then  ECBD.EsiBranchname " +
            //     //  "When    ed.ESICuttingBranch ='5' Then   ECBD.EsiBranchname " +

            //     //   "When  ed.ESICuttingBranch ='6' Then  ECBD.EsiBranchname " +
            //     //  "When    ed.ESICuttingBranch ='7'Then  ECBD.EsiBranchname " +
            //     //  "When    ed.ESICuttingBranch ='8' Then   ECBD.EsiBranchname " +

            //     //   "When   ed.ESICuttingBranch ='9' Then  ECBD.EsiBranchname " +
            //     //  "When    ed.ESICuttingBranch ='10'Then  ECBD.EsiBranchname " +
            //     //  "When    ed.ESICuttingBranch ='11' Then   ECBD.EsiBranchname " +
            //     //  "When    ed.ESICuttingBranch ='12' Then   ECBD.EsiBranchname " +
            //     // " When    ed.ESICuttingBranch ='13' Then   ECBD.EsiBranchname " +

            //     //  "end " +


            //     //                          " from EmpPaySheet Eps inner join EmpDetails " +
            //     //                          " Ed on ed.EmpId=eps.EmpId inner join EMPESICodes Esi on ed.EmpId=esi.Empid inner join " +
            //     //                          " Contracts Cts on eps.ClientId=cts.ClientId "+
            //     //                          " inner join EsiBranchDetails  ECBD on  ECBD.EsiBranchid=ed.ESICuttingBranch " +
            //     //                          "  where " +
            //     //                          " eps.month='" + month + Year.Substring(2, 2) + "' and cts.ESIonOT=1 and eps.ESI>0 and eps.Clientid like '" + CmpIDPrefix + "%'  group by esi.EmpESINo," +
            //     //                          " ed.EmpFName,ed.EmpMName,ed.EmpLName,cts.ESIonOT,ed.EmpId,ed.EmpDtofJoining,ed.ESICuttingBranch ,ECBD.EsiBranchname  order by ed.empid";
            //#endregion

            //                      }
            //            }
            //            if (ddlclient.SelectedIndex > 1)
            //            {
            //                  #region Old code on 19/08/2014


            //     //                  Sqlqry1 = "select esi.EmpESINo as EmpESINo,ed.empid,ed.EmpDtofJoining,ISNULL(ed.EmpFName,'')+' '+ISNULL(ed.empmname,'')+' " +
            //     //                       " '+ISNULL(ed.emplname,'') as Fullname,round(sum((eps.NoOfDuties+isnull(eps.ots,'')+ " +
            //     //                       " ISNULL(eps.NHS,'')+ISNULL(eps.Npots,0))),0) as NoOfDuties,round(sum(eps.ESIWAGES),0) as ESIWAGES," +
            //     //                       " round(sum(isnull(eps.OTAmt,0)+ISNULL(eps.Nhsamt,0)+ISNULL(eps.Npotsamt,0)),0) as Otamt," +
            //     //                       " round(sum(eps.ESIWAGES+isnull(eps.OTAmt,0)+ISNULL(eps.Nhsamt,0)+ISNULL(eps.Npotsamt,'')),0) as TotalMonthlywages," +
            //     //                       " cts.ESIonOT,"+


            //     //"  EsiCuttingBranch  = case " +
            //     // " When   ed.ESICuttingBranch ='0' Then  'NA' " +
            //     //  "When    ed.ESICuttingBranch ='1'Then  ECBD.EsiBranchname " +
            //     //  "When    ed.ESICuttingBranch ='2' Then   ECBD.EsiBranchname " +

            //     //  "When   ed.ESICuttingBranch ='3' Then  ECBD.EsiBranchname " +
            //     //  "When    ed.ESICuttingBranch ='4'Then  ECBD.EsiBranchname " +
            //     //  "When    ed.ESICuttingBranch ='5' Then   ECBD.EsiBranchname " +

            //     //   "When  ed.ESICuttingBranch ='6' Then  ECBD.EsiBranchname " +
            //     //  "When    ed.ESICuttingBranch ='7'Then  ECBD.EsiBranchname " +
            //     //  "When    ed.ESICuttingBranch ='8' Then   ECBD.EsiBranchname " +

            //     //   "When   ed.ESICuttingBranch ='9' Then  ECBD.EsiBranchname " +
            //     //  "When    ed.ESICuttingBranch ='10'Then  ECBD.EsiBranchname " +
            //     //  "When    ed.ESICuttingBranch ='11' Then   ECBD.EsiBranchname " +
            //     //  "When    ed.ESICuttingBranch ='12' Then   ECBD.EsiBranchname " +
            //     // " When    ed.ESICuttingBranch ='13' Then   ECBD.EsiBranchname " +

            //     //  "end " +


            //     //                       " from EmpPaySheet Eps inner join EmpDetails " +
            //     //                       " Ed on ed.EmpId=eps.EmpId inner join EMPESICodes Esi on ed.EmpId=esi.Empid inner join " +
            //     //                       " Contracts Cts on eps.ClientId=cts.ClientId "+
            //     //                       " inner join EsiBranchDetails  ECBD on  ECBD.EsiBranchid=ed.ESICuttingBranch " +
            //     //                       " where cts.ESIonOT=1 and" +
            //     //                       " eps.clientid='" + ddlclient.SelectedValue + "' and eps.month='" + month + Year.Substring(2, 2) + "'" +
            //     //                       " and ed.EmpESIDeduct=1 and eps.ESI>0  group by esi.EmpESINo,ed.EmpFName,ed.EmpMName," +
            //     //                       " ed.EmpLName,cts.ESIonOT,ed.EmpId,ed.EmpDtofJoining,ed.ESICuttingBranch ,ECBD.EsiBranchname   order by ed.empid";


            //#endregion
            //            }
            //        }

            #endregion

            #region Begin New Code as on 19/08/2014 by Venkat

            if (ddlEsibranch.SelectedIndex == 0)
            {
                //Sqlqry1 = "select EmpESINo=case esi.EmpESINo when '0' then 'NA' else esi.EmpESINo end,ed.empid,"+
                //"EmpDtofJoining=case CONVERT(VARCHAR(10), ed.EmpDtofJoining, 103)  when '01/01/1900' then ' ' else CONVERT(VARCHAR(10), ed.EmpDtofJoining, 103)  end," +
                //" ISNULL(ed.EmpFName,'')+' '+ISNULL(ed.empmname,'')+'  '+ISNULL(ed.emplname,'') as Fullname,"+
                //" round(sum((eps.NoOfDuties+isnull(eps.ots,'')+  ISNULL(eps.NHS,'')+ISNULL(eps.Npots,''))),0) as NoOfDuties,"+
                //" round(sum(eps.ESIWAGES),0) as ESIWAGES, round(sum(isnull(eps.OTAmt,0)+ISNULL(eps.Nhsamt,0)+ISNULL(eps.Npotsamt,0)),0) as Otamt,"+
                //"round(sum(eps.ESIWAGES+isnull(eps.OTAmt,0)+ISNULL(eps.Nhsamt,'')+ISNULL(eps.Npotsamt,0)),0) as TotalMonthlywages,cts.ESIonOT,"+
                //" ECBD.EsiBranchname from EmpPaySheet Eps inner join EmpDetails  Ed on ed.EmpId=eps.EmpId inner join EMPESICodes Esi on ed.EmpId=esi.Empid "+
                //" inner join  Contracts Cts on eps.ClientId=cts.ClientId  inner join EsiBranchDetails  ECBD on ECBD.EsiBranchid=cts.esibranch  "+
                //" where  eps.month='" + month + Year.Substring(2, 2) + "'  and eps.Clientid like '" + CmpIDPrefix + "%' group by esi.EmpESINo, " +
                //" ed.EmpFName,ed.EmpMName,ed.EmpLName,cts.ESIonOT,ed.EmpId,ed.EmpDtofJoining,ed.ESICuttingBranch ,ECBD.EsiBranchname  order by ed.empid";

                Sqlqry1 = "select ed.empid,ISNULL(ed.EmpFName,'')+' '+ISNULL(ed.empmname,'')+'  '+ISNULL(ed.emplname,'') as Fullname," +
                        "EmpESINo=case esi.EmpESINo when '0' then 'NA' else esi.EmpESINo end,c.clientid,c.clientname,EmpDtofJoining=case CONVERT(VARCHAR(10), " +
                        " ed.EmpDtofJoining, 103)  when '01/01/1900' then ' ' else CONVERT(VARCHAR(10), ed.EmpDtofJoining, 103)  end, " +
                        " round(sum((eps.NoOfDuties)),0) as NoOfDuties,round(sum((eps.ots)),0) as ots," +
                        " (round(sum(isnull(eps.gross,0)),0)-round(sum(isnull(eps.WashAllowance,0)),0)) as grossamt," +
                        "round(sum(isnull(eps.OTAmt,0)),0) as Otamt,round(sum(eps.ESIWAGES),0) as ESIWAGES,round(sum(isnull(eps.ESIEmpr,0)),0) as Esiempr," +
                        " round(sum(isnull(eps.ESI,0)),0) as esiemp,(round(sum(isnull(eps.ESIEmpr,0)),0)+round(sum(isnull(eps.ESI,0)),0)) as TotalMonthlywages," +
                         "cts.ESIonOT, ECBD.EsiBranchname from EmpPaySheet Eps inner join Clients c on  eps.ClientId = c.ClientId inner join EmpDetails  Ed on ed.EmpId=eps.EmpId inner join EMPESICodes Esi " +
                         "on ed.EmpId=esi.Empid inner join  Contracts Cts on eps.ClientId=cts.ClientId  left join EsiBranchDetails  ECBD on ECBD.EsiBranchid=cts.esibranch  " +
               " where  eps.month='" + month + Year.Substring(2, 2) + "'  and eps.Clientid like '" + CmpIDPrefix + "%' group by esi.EmpESINo, " +
               " ed.EmpFName,ed.EmpMName,ed.EmpLName,cts.ESIonOT,ed.EmpId,c.clientid,c.clientname,ed.EmpDtofJoining,ed.ESICuttingBranch ,ECBD.EsiBranchname  order by ed.empid";

            }
            if (ddlEsibranch.SelectedIndex > 0)
            {
                Sqlqry1 = "select ed.empid,ISNULL(ed.EmpFName,'')+' '+ISNULL(ed.empmname,'')+'  '+ISNULL(ed.emplname,'') as Fullname," +
                        "EmpESINo=case esi.EmpESINo when '0' then 'NA' else esi.EmpESINo end,c.clientid,c.clientname,EmpDtofJoining=case CONVERT(VARCHAR(10), " +
                        " ed.EmpDtofJoining, 103)  when '01/01/1900' then ' ' else CONVERT(VARCHAR(10), ed.EmpDtofJoining, 103)  end, " +
                        " round(sum((eps.NoOfDuties)),0) as NoOfDuties,round(sum((eps.ots)),0) as ots," +
                        " (round(sum(isnull(eps.gross,0)),0)-round(sum(isnull(eps.WashAllowance,0)),0)) as grossamt," +
                        "round(sum(isnull(eps.OTAmt,0)),0) as Otamt,round(sum(eps.ESIWAGES),0) as ESIWAGES,round(sum(isnull(eps.ESIEmpr,0)),0) as Esiempr," +
                        " round(sum(isnull(eps.ESI,0)),0) as esiemp,(round(sum(isnull(eps.ESIEmpr,0)),0)+round(sum(isnull(eps.ESI,0)),0)) as TotalMonthlywages," +
                         "cts.ESIonOT, ECBD.EsiBranchname  from EmpPaySheet Eps inner join Clients c on  eps.ClientId = c.ClientId inner join EmpDetails  Ed on ed.EmpId=eps.EmpId left outer join EMPESICodes Esi on ed.EmpId=esi.Empid " +
               " inner join  Contracts Cts on eps.ClientId=cts.ClientId  left join EsiBranchDetails  ECBD on ECBD.EsiBranchid=cts.esibranch  " +
               " where  eps.month='" + month + Year.Substring(2, 2) + "'  and eps.Clientid like '" + CmpIDPrefix + "%' and cts.Esibranch='" + ddlEsibranch.SelectedValue + "' group by esi.EmpESINo, " +
               " ed.EmpFName,ed.EmpMName,ed.EmpLName,cts.ESIonOT,ed.EmpId,c.clientid,c.clientname,ed.EmpDtofJoining,ed.ESICuttingBranch ,ECBD.EsiBranchname  order by ed.empid";
            }

            #endregion


            dtone = config.ExecuteAdaptorAsyncWithQueryParams(Sqlqry1).Result;
            //dttwo = SqlHelper.Instance.GetTableByQuery(Sqlqry2);
            //dtone.Merge(dttwo);
            GVListOfClients.DataSource = dtone;
            GVListOfClients.DataBind();
            for (int i = 0; i < GVListOfClients.Rows.Count; i++)
            {

                if (dtone.Rows.Count > 0)
                //if (dtone.Rows.Count > 0 && dttwo.Rows.Count > 0)
                {
                    //string Gender = "";

                    //Label lblOtamt = GVListOfClients.Rows[i].FindControl("lblOTamt") as Label;
                    //bool Esionot = false;
                    //if (String.IsNullOrEmpty(dtone.Rows[i]["ESIonOT"].ToString()) == false)
                    //{
                    //    Esionot = bool.Parse(dtone.Rows[i]["ESIonOT"].ToString());
                    //}
                    //if (Esionot == false)
                    //{
                    //    lblOtamt.Text = "0";
                    //}
                }
                else
                {
                    GVListOfClients.DataSource = null;
                    GVListOfClients.DataBind();
                }
            }
        }

        protected void lbtn_Export_Click(object sender, EventArgs e)
        {
            gve.Export("ESIReport.xls", this.GVListOfClients);
        }

        protected void LoadEsibranches()
        {
            DataTable dtEsibranches = GlobalData.Instance.LoadEsibranches();
            if (dtEsibranches.Rows.Count > 0)
            {
                ddlEsibranch.DataValueField = "EsiBranchid";
                ddlEsibranch.DataTextField = "EsiBranchname";
                ddlEsibranch.DataSource = dtEsibranches;
                ddlEsibranch.DataBind();
            }
            ddlEsibranch.Items.Insert(0, "All");
        }
    }
}