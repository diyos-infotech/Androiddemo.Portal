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
using System.Globalization;
using KLTS.Data;
using Jawan.Portal.DAL;
namespace Jawan.Portal
{
    public partial class ReportforPfdetails : System.Web.UI.Page
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
                LoadClientIdAndName();
            }
        }

        protected void GetWebConfigdata()
        {
            EmpIDPrefix = Session["EmpIDPrefix"].ToString();
            Elength = (EmpIDPrefix.Trim().Length + 1).ToString();
            CmpIDPrefix = Session["CmpIDPrefix"].ToString();
            Clength = (CmpIDPrefix.Trim().Length + 1).ToString();
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
            DataTable dt = null;
            String Sqlqry = "";
            if (ddlclient.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showlalert", "alert('Please Select Client Id');", true);
                return;
            }
            if (txtmonth.Text.Trim().Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showlalert", "alert('Please Select Month');", true);
                return;
            }

            string month = DateTime.Parse(txtmonth.Text.Trim(), CultureInfo.GetCultureInfo("en-gb")).Month.ToString();
            string Year = DateTime.Parse(txtmonth.Text.Trim(), CultureInfo.GetCultureInfo("en-gb")).Year.ToString();
            DateTime date = DateTime.Now;
            if (Chkmonth.Checked)
            {
                if (ddlclient.SelectedIndex == 1)
                {
                    if (radiooldpfdetails.Checked)
                    {
                        Sqlqry = "select distinct ed.EmpId,ISNULL(ed.EmpFName,'')+' '+ISNULL(ed.empmname,'')+' '" +
                            "  +ISNULL(ed.emplname,'') as Fullname, eps.clientid, c.clientname,round(eps.PFWAGES,0) as PFWAGES,round(eps.PF,0) as PF,eps.NoOfDuties," +
                            " round(eps.PFEmpr,0) as PFEmpr,round(eps.PFEmpr-eps.PF,0) as PfDiff,ed.EmpDtofBirth,ed.EmpSex," +
                            " ed.EmpFatherSpouseName,epf.EmpPFEnrolDt,epf.EmpEpfNo,ed.EmpDtofJoining from EmpPaySheet Eps inner join EmpDetails " +
                            " ed on ed.EmpId=eps.EmpId left outer join EMPEPFCodes epf on ed.EmpId=epf.Empid inner join clients c on c.clientid=eps.clientid where " +
                            " ed.EmpDtofJoining between '08/01/2013' and '" + date + "' order by ed.empid ";
                    }
                    else
                    {

                        Sqlqry = " select Tab.EmpId, SUM(NoOfDuties) as  NoOfDuties, Fullname, eps.clientid, c.clientname,Sum(PFWAGES) as PFWAGES , Sum(PF) as PF, " +
                                  "  Sum(PFEmpr) as PFEmpr , Sum(PfDiff) as PfDiff ,Tab.EmpDtofBirth,Tab.EmpSex, " +
                                  "Tab.EmpDtofJoining,Tab.EmpFatherSpouseName,Tab.EmpPFEnrolDt,Tab.EmpEpfNo      " +
                                  " From ( select distinct ed.EmpId,ISNULL(ed.EmpFName,'')+' '+ISNULL(ed.empmname,'')+' '" +
                                    "  +ISNULL(ed.emplname,'') as Fullname,round(eps.PFWAGES,0) as PFWAGES,round(eps.PF,0) as PF,eps.NoOfDuties," +
                                    " round(eps.PFEmpr,0) as PFEmpr,round(eps.PFEmpr-eps.PF,0) as PfDiff,ed.EmpDtofBirth,ed.EmpSex," +
                                    " ed.EmpFatherSpouseName,epf.EmpPFEnrolDt,epf.EmpEpfNo,ed.EmpDtofJoining from EmpPaySheet Eps inner join EmpDetails inner join clients c on c.clientid=eps.clientid  " +
                                    " ed on ed.EmpId=eps.EmpId left outer join EMPEPFCodes epf on ed.EmpId=epf.Empid where " +
                                    " ed.EmpDtofJoining between '08/01/2013' and '" + date + "'    )  as Tab  group by  Tab.EmpId ," +
                              "Fullname,PFWAGES,PF,PFEmpr,PfDiff,EmpDtofBirth, EmpSex,EmpDtofJoining,EmpFatherSpouseName,EmpPFEnrolDt,EmpEpfNo" +
                              "  order by Tab.EmpId";
                    }
                }
                if (ddlclient.SelectedIndex > 1)
                {
                    if (radiooldpfdetails.Checked)
                    {
                        Sqlqry = "select distinct ed.EmpId,ISNULL(ed.EmpFName,'')+' '+ISNULL(ed.empmname,'')+' '" +
                            "  ISNULL(ed.emplname,'') as Fullname,eps.Clientid, c.clientname,round(eps.PFWAGES,0) as PFWAGES,round(eps.PF,0) as PF,eps.NoOfDuties," +
                            " round(eps.PFEmpr,0) as PFEmpr,round(eps.PFEmpr-eps.PF,0) as PfDiff,ed.EmpDtofBirth,ed.EmpSex," +
                            " ed.EmpFatherSpouseName,epf.EmpPFEnrolDt,epf.EmpEpfNo,ed.EmpDtofJoining from EmpPaySheet Eps inner join EmpDetails inner join clients c on c.clientid=eps.clientid  " +
                            " ed on ed.EmpId=eps.EmpId left outer join EMPEPFCodes epf on ed.EmpId=epf.Empid where " +
                            " ed.EmpDtofJoining between '08/01/2013' and '" + date + "' and eps.clientid='" + ddlclient.SelectedValue + "' order by ed.empid";
                    }
                    else
                    {
                        Sqlqry = " select Tab.EmpId, SUM(NoOfDuties) as  NoOfDuties, Fullname, eps.Clientid, c.clientname,Sum(PFWAGES) as PFWAGES , Sum(PF) as PF, " +
                                "  Sum(PFEmpr) as PFEmpr , Sum(PfDiff) as PfDiff ,Tab.EmpDtofBirth,Tab.EmpSex, " +
                                  "Tab.EmpDtofJoining,Tab.EmpFatherSpouseName,Tab.EmpPFEnrolDt,Tab.EmpEpfNo      " +
                                  " From ( select distinct ed.EmpId,ISNULL(ed.EmpFName,'')+' '+ISNULL(ed.empmname,'')+' '" +
                       "  ISNULL(ed.emplname,'') as Fullname,round(eps.PFWAGES,0) as PFWAGES,round(eps.PF,0) as PF,eps.NoOfDuties," +
                       " round(eps.PFEmpr,0) as PFEmpr,round(eps.PFEmpr-eps.PF,0) as PfDiff,ed.EmpDtofBirth,ed.EmpSex," +
                       " ed.EmpFatherSpouseName,epf.EmpPFEnrolDt,epf.EmpEpfNo,ed.EmpDtofJoining from EmpPaySheet Eps inner join EmpDetails inner join clients c on c.clientid=eps.clientid  " +
                       " ed on ed.EmpId=eps.EmpId left outer join EMPEPFCodes epf on ed.EmpId=epf.Empid where " +
                       " ed.EmpDtofJoining between '08/01/2013' and '" + date + "' and eps.clientid='" + ddlclient.SelectedValue + "'    )  as Tab  group by  Tab.EmpId ," +
                              "Fullname,PFWAGES,PF,PFEmpr,PfDiff,EmpDtofBirth, EmpSex,EmpDtofJoining,EmpFatherSpouseName,EmpPFEnrolDt,EmpEpfNo" +
                              "  order by Tab.EmpId";
                    }
                }
            }
            else
            {
                if (ddlclient.SelectedIndex == 1)
                {

                    if (radiooldpfdetails.Checked)
                    {

                        Sqlqry = "   select ed.EmpId,ISNULL(ed.EmpFName,'')+' '+ISNULL(ed.empmname,'')+' '+ISNULL(ed.emplname,'') as Fullname," +
                                     " eps.Clientid, c.clientname,round(eps.PFWAGES,0) as PFWAGES,round(eps.PF,0) as PF,round(eps.PFEmpr,0) as PFEmpr,eps.NoOfDuties," +
                                     " round(eps.PFEmpr-eps.PF,0) as PfDiff,ed.EmpDtofBirth,ed.EmpSex,ed.EmpDtofJoining," +
                                     " ed.EmpFatherSpouseName,epf.EmpPFEnrolDt,epf.EmpEpfNo   from EmpPaySheet Eps inner join EmpDetails ed on" +
                                     " ed.EmpId=eps.EmpId inner join clients c on c.clientid=eps.clientid  left outer join EMPEPFCodes epf on ed.EmpId=epf.Empid where " +
                                     " eps.month='" + month + Year.Substring(2, 2) + "' order by ed.empid";

                    }
                    else
                    {
                        Sqlqry = "  select Tab.EmpId, SUM(NoOfDuties) as  NoOfDuties, Fullname, Sum(PFWAGES) as PFWAGES , Sum(PF) as PF, " +
                                 "  eps.Clientid, c.clientname,Sum(PFEmpr) as PFEmpr , Sum(PfDiff) as PfDiff ,Tab.EmpDtofBirth,Tab.EmpSex, " +
                                 "  Tab.EmpDtofJoining,Tab.EmpFatherSpouseName,Tab.EmpPFEnrolDt,Tab.EmpEpfNo      " +
                                 "  From (  select ed.EmpId,ISNULL(ed.EmpFName,'')+' '+ISNULL(ed.empmname,'')+' '+ISNULL(ed.emplname,'') as Fullname," +
                                 "  round(eps.PFWAGES,0) as PFWAGES,round(eps.PF,0) as PF,round(eps.PFEmpr,0) as PFEmpr,eps.NoOfDuties," +
                                 "  round(eps.PFEmpr-eps.PF,0) as PfDiff,ed.EmpDtofBirth,ed.EmpSex,ed.EmpDtofJoining," +
                                 "  ed.EmpFatherSpouseName,epf.EmpPFEnrolDt,epf.EmpEpfNo   from EmpPaySheet Eps inner join EmpDetails ed on" +
                                 "  ed.EmpId=eps.EmpId inner join clients c on c.clientid=eps.clientid  left outer join EMPEPFCodes epf on ed.EmpId=epf.Empid where " +
                                 "  eps.month='" + month + Year.Substring(2, 2) + "'    )  as Tab  group by  Tab.EmpId ," +
                                 "  Fullname,PFWAGES,PF,PFEmpr,PfDiff,EmpDtofBirth, EmpSex,EmpDtofJoining,EmpFatherSpouseName,EmpPFEnrolDt,EmpEpfNo" +
                                "   order by Tab.EmpId";
                    }
                }
                if (ddlclient.SelectedIndex > 1)
                {

                    if (radiooldpfdetails.Checked)
                    {
                        Sqlqry = "select ed.EmpId,ISNULL(ed.EmpFName,'')+' '+ISNULL(ed.empmname,'')+' '+ISNULL(ed.emplname,'') as Fullname," +
                                     " eps.Clientid, c.clientname,round(eps.PFWAGES,0) as PFWAGES,round(eps.PF,0) as PF,round(eps.PFEmpr,0) as PFEmpr,eps.NoOfDuties," +
                                     " round(eps.PFEmpr-eps.PF,0) as PfDiff,ed.EmpDtofBirth,ed.EmpSex,ed.EmpDtofJoining," +
                                     " ed.EmpFatherSpouseName,epf.EmpPFEnrolDt,epf.EmpEpfNo from EmpPaySheet Eps inner join EmpDetails ed on" +
                                     " ed.EmpId=eps.EmpId inner join clients c on c.clientid=eps.clientid  left outer join EMPEPFCodes epf on ed.EmpId=epf.Empid where " +
                                     " eps.clientid='" + ddlclient.SelectedValue + "' and eps.month='" + month + Year.Substring(2, 2) + "' order by ed.empid";


                    }
                    else
                    {

                        Sqlqry = " select Tab.EmpId, SUM(NoOfDuties) as  NoOfDuties, Fullname, Sum(PFWAGES) as PFWAGES , Sum(PF) as PF, " +
                                "  eps.Clientid, c.clientname,Sum(PFEmpr) as PFEmpr , Sum(PfDiff) as PfDiff ,Tab.EmpDtofBirth,Tab.EmpSex, " +
                                  "Tab.EmpDtofJoining,Tab.EmpFatherSpouseName,Tab.EmpPFEnrolDt,Tab.EmpEpfNo      " +
                                  " From (  select ed.EmpId,ISNULL(ed.EmpFName,'')+' '+ISNULL(ed.empmname,'')+' '+ISNULL(ed.emplname,'') as Fullname," +
                                 " round(eps.PFWAGES,0) as PFWAGES,round(eps.PF,0) as PF,round(eps.PFEmpr,0) as PFEmpr,eps.NoOfDuties," +
                                 " round(eps.PFEmpr-eps.PF,0) as PfDiff,ed.EmpDtofBirth,ed.EmpSex,ed.EmpDtofJoining," +
                                 " ed.EmpFatherSpouseName,epf.EmpPFEnrolDt,epf.EmpEpfNo from EmpPaySheet Eps inner join EmpDetails ed on" +
                                 " ed.EmpId=eps.EmpId inner join clients c on c.clientid=eps.clientid  left outer join EMPEPFCodes epf on ed.EmpId=epf.Empid where " +
                                 " eps.clientid='" + ddlclient.SelectedValue + "' and eps.month='" + month + Year.Substring(2, 2) + "'    )  as Tab  group by  Tab.EmpId ," +
                                 "Fullname,PFWAGES,PF,PFEmpr,PfDiff,EmpDtofBirth, EmpSex,EmpDtofJoining,EmpFatherSpouseName,EmpPFEnrolDt,EmpEpfNo" +
                                "  order by Tab.EmpId";

                    }


                }
            }
            dt = config.ExecuteAdaptorAsyncWithQueryParams(Sqlqry).Result;
            GVListOfClients.DataSource = dt;
            GVListOfClients.DataBind();
            for (int i = 0; i < GVListOfClients.Rows.Count; i++)
            {

                if (dt.Rows.Count > 0)
                {
                    string Gender = "";

                    Label lblGender = GVListOfClients.Rows[i].FindControl("lblGender") as Label;
                    //bool Gender = false;
                    if (String.IsNullOrEmpty(dt.Rows[i]["EmpSex"].ToString()) == false)
                    {
                        Gender = (dt.Rows[i]["EmpSex"].ToString());
                    }
                    if (Gender == "M")
                    {
                        lblGender.Text = "Male";
                    }
                    else if (Gender == "F")
                    {
                        lblGender.Text = "Female";
                    }
                    else
                    {
                        lblGender.Text = "Transgender";
                    }

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
            gve.Export("PFReport.xls", this.GVListOfClients);
        }
    }
}