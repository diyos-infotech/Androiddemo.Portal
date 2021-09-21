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
using Jawan.Portal.DAL;
namespace Jawan.Portal
{
    public partial class ClentwiseEmployeesSalaryreports3 : System.Web.UI.Page
    {
        AppConfiguration config = new AppConfiguration();
        GridViewExportUtil gve = new GridViewExportUtil();
        DataTable dt;
        string EmpIDPrefix = "";
        string CmpIDPrefix = "";
        string Elength = "";
        string Clength = "";
        protected void Page_Load(object sender, EventArgs e)
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

                ddlcname.Items.Add("--Select--");
                LoadClientNames();
                FillClientList();
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
            EmpIDPrefix = GlobalData.Instance.GetEmployeeIDPrefix();
            Elength = (EmpIDPrefix.Trim().Length + 1).ToString();
            CmpIDPrefix = GlobalData.Instance.GetClientIDPrefix();
            Clength = (CmpIDPrefix.Trim().Length + 1).ToString();
        }

        protected void ddlcname_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            ClearData();
            if (ddlcname.SelectedIndex > 0)
            {
                txtmonth.Text = "";
                FillClientid();
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
                Fillcname();

            }
            else
            {
                ddlcname.SelectedIndex = 0;
            }
        }

        protected void Fillcname()
        {

            if (ddlclientid.SelectedIndex == 1)
            {
                ddlcname.SelectedIndex = 1;
                return;
            }
            string SqlQryForCname = "Select Clientname from Clients where clientid='" + ddlclientid.SelectedValue + "'";
            DataTable dtCname = config.ExecuteAdaptorAsyncWithQueryParams(SqlQryForCname).Result;
            if (dtCname.Rows.Count > 0)
                ddlcname.SelectedValue = dtCname.Rows[0]["Clientname"].ToString();
        }

        protected void FillClientid()
        {

            if (ddlcname.SelectedIndex == 1)
            {
                ddlclientid.SelectedIndex = 1;
                return;
            }


            string SqlQryForCid = "Select Clientid from Clients where clientname='" + ddlcname.SelectedValue + "'";
            DataTable dtCname = config.ExecuteAdaptorAsyncWithQueryParams(SqlQryForCid).Result;
            if (dtCname.Rows.Count > 0)
                ddlclientid.SelectedValue = dtCname.Rows[0]["Clientid"].ToString();
        }

        protected void LoadClientNames()
        {
            string selectquery = "select Clientname from Clients    where clientstatus=1 order by Clientname";
            DataTable dtable = config.ExecuteAdaptorAsyncWithQueryParams(selectquery).Result;
            for (int i = 0; i < dtable.Rows.Count; i++)
            {
                ddlcname.Items.Add(dtable.Rows[i]["Clientname"].ToString());
            }

            ddlcname.Items.Insert(1, "All");
        }

        protected void FillClientList()
        {
            string sqlQry = "Select ClientId from Clients Order By cast(substring(clientid," + Clength + ", 4) as int)";
            DataTable data = config.ExecuteAdaptorAsyncWithQueryParams(sqlQry).Result;
            ddlclientid.Items.Clear();
            ddlclientid.Items.Add("--Select--");
            for (int i = 0; i < data.Rows.Count; i++)
            {
                ddlclientid.Items.Add(data.Rows[i]["ClientId"].ToString());
            }

            ddlclientid.Items.Insert(1, "All");


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
            string month = DateTime.Parse(txtmonth.Text.Trim()).Month.ToString();
            string Year = DateTime.Parse(txtmonth.Text.Trim()).Year.ToString();
            string sqlqry = string.Empty;


            if (ddlclientid.SelectedIndex == 1)
            {
                sqlqry = "Select  EP.Empid,EP.Clientid,C.Clientname,(E.Empfname + E.EmpMname + E.emplname) as EmpMname ,EP.Desgn," +
                          " E.Empbankacno,E.EmpBankCardRef, EP.basic,EP.da,EP.noofduties,EA.ot,EP.Proftax, ESC.EmpESINo , EPC.empepfno, " +
                          " EP.totalgross,EP.otamt,EP.pf,EP.esi,EP.penalty,EP.actualamount,EP.owf,EP.saladvded,EP.CanteenAdv,EP.UniformDed,EP.OtherDed," +
                          "   (EP.totalgross*(Select  ESIEmployer from  TblOptions)/100) as  Empresi," +
                          "(EP.totalgross*(Select  PFEmployer from  TblOptions)/100) as  Emprpf,   " +
                          "((EP.totalgross*(Select  ESIEmployer from  TblOptions)/100)+EP.esi) as  Esitotal, " +
                          " ((EP.totalgross*(Select  ESIEmployer from  TblOptions)/100)+EP.pf) as  pftotal  " +
                          "  from  emppaysheet EP " +
                          " inner join Clients C on  C.Clientid = EP.Clientid " +
                          " inner join  empdetails E on E.Empid=EP.Empid   " +
                     " inner join  Empattendance EA on EA.Empid=EP.Empid  " +
                     " inner join  ESIded ESD on ESD.Empid=EP.Empid   " +
                     " inner join  Epfded EPD on EPD.Empid=EP.Empid  " +
                    " inner join  EMPESICodes ESC on ESC.Empid=EP.Empid   " +
                     " inner join  EMPEPFCodes EPC on EPC.Empid=EP.Empid  " +
                     "  and  EP.noofduties<>0  and EP.month='" + month + Year.Substring(2, 2) +
                     " '  and  EP.month=EA.Month  and  EP.Clientid=EA.Clientid and  EPD.Month=EP.month  and  ESD.Month=EP.month " +
                     "   order By   cast(substring(EP.Clientid," +
                     Clength + ", 4) as int) ,    cast(substring(EP.Empid," + Elength + ", 6) as int)";

                Bindata(sqlqry);
                return;

            }

            sqlqry = "Select  EP.Empid,EP.Clientid,C.Clientname,(E.Empfname +' ' + E.EmpMname + ' ' +  E.emplname) as EmpMname ,EP.Desgn," +
                       " E.Empbankacno,E.EmpBankCardRef, EP.basic,EP.da,EP.noofduties,EA.ot,EP.Proftax, ESC.EmpESINo , EPC.empepfno, " +
                       " EP.totalgross,EP.otamt,EP.pf,EP.esi,EP.penalty,EP.actualamount,EP.owf,EP.saladvded,EP.CanteenAdv,EP.UniformDed,EP.OtherDed," +
                       "   (EP.totalgross  * (Select  ESIEmployer from  TblOptions)/100) as  Empresi," +
                       "(EP.totalgross*(Select  PFEmployer from  TblOptions)/100) as  Emprpf,   " +
                       "((EP.totalgross*(Select  ESIEmployer from  TblOptions)/100)+EP.esi) as  Esitotal, " +
                       " ((EP.totalgross*(Select  ESIEmployer from  TblOptions)/100)+EP.pf) as  pftotal  " +
                       "  from  emppaysheet EP " +
                       " inner join Clients C on  C.Clientid = EP.Clientid " +
                       " inner join  empdetails E on E.Empid=EP.Empid   " +
                  " inner join  Empattendance EA on EA.Empid=EP.Empid  " +
                  " inner join  ESIded ESD on ESD.Empid=EP.Empid   " +
                  " inner join  Epfded EPD on EPD.Empid=EP.Empid  " +
                 " inner join  EMPESICodes ESC on ESC.Empid=EP.Empid   " +
                  " inner join  EMPEPFCodes EPC on EPC.Empid=EP.Empid  " +
                  "  and    EP.Clientid='" + ddlclientid.SelectedValue + "'  and  EP.noofduties<>0  and EP.month='" + month + Year.Substring(2, 2) +
                  " '  and  EP.month=EA.Month  and  EP.Clientid=EA.Clientid and  EPD.Month=EP.month  and  ESD.Month=EP.month   order By   cast(substring(EP.Clientid," +
                  Clength + ", 4) as int) ,    cast(substring(EP.Empid," + Elength + ", 6) as int)";


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
            string month = DateTime.Parse(txtmonth.Text.Trim()).Month.ToString();
            string Year = DateTime.Parse(txtmonth.Text.Trim()).Year.ToString();
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
                DataTable dtemprpfandesi =config.ExecuteAdaptorAsyncWithQueryParams(sqlqryforemprpfesi).Result;
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

        protected void GVListEmployees_RowDataBound(object sender, GridViewRowEventArgs e)
        {

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

        }
    }
}