using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using KLTS.Data;
using System.Collections;
using System.Globalization;
using Jawan.Portal.DAL;
namespace Jawan.Portal
{
    public partial class UnitwiseEsiReports : System.Web.UI.Page
    {
        DataTable dt;
        string EmpIDPrefix = "";
        string CmpIDPrefix = "";
        string BranchID = "";

        string Elength = "";
        string Clength = "";
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
                        // lblDisplayUser.Text = Session["UserId"].ToString();
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

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            ClearData();

            string date = "";

            #region Begin Code  For Validation as on [16-11-2013]

            if (ddlClientId.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Please Select the client Id /ALL');", true);
                return;
            }

            if (txtmonth.Text.Trim().Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Please Select The Month');", true);
                return;
            }

            #endregion End  Code  For Validation as on [16-11-2013]

            if (txtmonth.Text.Trim().Length > 0)
            {
                date = DateTime.Parse(txtmonth.Text.Trim(), CultureInfo.GetCultureInfo("en-gb")).ToString();
            }

            string month = DateTime.Parse(txtmonth.Text.Trim()).Month.ToString();
            string Year = DateTime.Parse(txtmonth.Text.Trim()).Year.ToString();


            string sqlqry = string.Empty;
            if (ddlAttendance.SelectedIndex == 0)
            {
                if (ddlesitype.SelectedIndex == 0)
                {

                    sqlqry = "select eps.ClientId,c.ClientName , COUNT(eps.EmpId) as   strength,Isnull(SUM(  Eps.gross),0)  as  gross, " +
                             "  Isnull(SUM( Eps.gross -  Eps.WashAllowance),0) " +
                             "  as  WashAllowance, Isnull(SUM( Eps.ESI ),0) as esi,Isnull(SUM( Eps.otamt),0) as   otamt, " +
                             "  Isnull(SUM(  Eps.esiempr ),0)  ESIEmpr " +
                             "  ,( (Isnull(SUM(  Eps.ESI ),0)) +(Isnull(SUM(Eps.esiempr ),0)  )  )  as   total  from EmpPaySheet as Eps   " +
                             "  INNER  JOIN  EmpDetails empd ON  empd.EmpId=eps.EmpId  " +
                             "  inner  join   Clients C  " +
                             "  on  C.ClientId=eps.ClientId and  Eps.Month='" + month + Year.Substring(2, 2) + "' AND EPS.NoOfDuties<> 0   " +
                             "  and  Eps.ESI>0   and  eps.ClientId=C.ClientId group by eps.ClientId,c.ClientName  order by eps.ClientId  ";

                }

                else
                {
                    sqlqry = "select eps.ClientId,c.ClientName , COUNT(eps.EmpId) as   strength,Isnull(SUM(  Eps.gross),0)  as  gross, " +
                             " Isnull(SUM( Eps.gross -  Eps.WashAllowance),0) " +
                             " as  WashAllowance, Isnull(SUM( Eps.ESI ),0) as esi,Isnull(SUM( Eps.otamt),0) as   otamt, " +
                             " Isnull(SUM(  Eps.esiempr ),0)  ESIEmpr " +
                             " ,( (Isnull(SUM(  Eps.ESI ),0)) +(Isnull(SUM(Eps.esiempr ),0)  )  )  as   total  from EmpPaySheet as Eps   " +
                             " INNER  JOIN  EmpDetails empd ON  empd.EmpId=eps.EmpId  " +
                             " inner  join   Clients C  " +
                             " on  C.ClientId=eps.ClientId and  Eps.Month='" + month + Year.Substring(2, 2) + "' AND EPS.NoOfDuties<> 0   " +
                             " and  Eps.ESI<=0   and  eps.ClientId=C.ClientId group by eps.ClientId,c.ClientName  order by eps.ClientId  ";

                }
                BindData(sqlqry);
            }

            #region Code added for attendance wise added by venkat on 12/11/2013



            if (ddlAttendance.SelectedIndex == 1)
            {
                if (ddlesitype.SelectedIndex == 0)
                {

                    sqlqry = "select eps.ClientId,c.ClientName , COUNT(eps.EmpId) as   strength,Isnull(SUM(  Eps.gross),0)  as  gross, " +
                             "  Isnull(SUM( Eps.gross -  Eps.WashAllowance),0) " +
                             "  as  WashAllowance, Isnull(SUM( Eps.ESI ),0) as esi,Isnull(SUM( Eps.otamt),0) as   otamt, " +
                             "  Isnull(SUM(  Eps.esiempr ),0)  ESIEmpr " +
                             "  ,( (Isnull(SUM(  Eps.ESI ),0)) +(Isnull(SUM(Eps.esiempr ),0)  )  )  as   total  from EmpPaySheet as Eps   " +
                             "  INNER  JOIN  EmpDetails empd ON  empd.EmpId=eps.EmpId  " +
                             "  inner  join   Clients C  " +
                             "  on  C.ClientId=eps.ClientId and  Eps.Month='" + month + Year.Substring(2, 2) + "' AND EPS.NoOfDuties>=10   " +
                             "  and  Eps.ESI>0   and  eps.ClientId=C.ClientId group by eps.ClientId,c.ClientName  order by eps.ClientId  ";

                }

                else
                {
                    sqlqry = "select eps.ClientId,c.ClientName , COUNT(eps.EmpId) as   strength,Isnull(SUM(  Eps.gross),0)  as  gross, " +
                             " Isnull(SUM( Eps.gross -  Eps.WashAllowance),0) " +
                             " as  WashAllowance, Isnull(SUM( Eps.ESI ),0) as esi,Isnull(SUM( Eps.otamt),0) as   otamt, " +
                             " Isnull(SUM(  Eps.esiempr ),0)  ESIEmpr " +
                             " ,( (Isnull(SUM(  Eps.ESI ),0)) +(Isnull(SUM(Eps.esiempr ),0)  )  )  as   total  from EmpPaySheet as Eps   " +
                             " INNER  JOIN  EmpDetails empd ON  empd.EmpId=eps.EmpId  " +
                             " inner  join   Clients C  " +
                             " on  C.ClientId=eps.ClientId and  Eps.Month='" + month + Year.Substring(2, 2) + "' AND EPS.NoOfDuties>=10   " +
                             " and  Eps.ESI<=0   and  eps.ClientId=C.ClientId group by eps.ClientId,c.ClientName  order by eps.ClientId  ";

                }
                BindData(sqlqry);
            }

            if (ddlAttendance.SelectedIndex == 2)
            {
                if (ddlesitype.SelectedIndex == 0)
                {

                    sqlqry = "select eps.ClientId,c.ClientName , COUNT(eps.EmpId) as   strength,Isnull(SUM(  Eps.gross),0)  as  gross, " +
                             "  Isnull(SUM( Eps.gross -  Eps.WashAllowance),0) " +
                             "  as  WashAllowance, Isnull(SUM( Eps.ESI ),0) as esi,Isnull(SUM( Eps.otamt),0) as   otamt, " +
                             "  Isnull(SUM(  Eps.esiempr ),0)  ESIEmpr " +
                             "  ,( (Isnull(SUM(  Eps.ESI ),0)) +(Isnull(SUM(Eps.esiempr ),0)  )  )  as   total  from EmpPaySheet as Eps   " +
                             "  INNER  JOIN  EmpDetails empd ON  empd.EmpId=eps.EmpId  " +
                             "  inner  join   Clients C  " +
                             "  on  C.ClientId=eps.ClientId and  Eps.Month='" + month + Year.Substring(2, 2) + "' AND EPS.NoOfDuties<10   " +
                             "  and  Eps.ESI>0   and  eps.ClientId=C.ClientId group by eps.ClientId,c.ClientName  order by eps.ClientId  ";

                }

                else
                {
                    sqlqry = "select eps.ClientId,c.ClientName , COUNT(eps.EmpId) as   strength,Isnull(SUM(  Eps.gross),0)  as  gross, " +
                             " Isnull(SUM( Eps.gross -  Eps.WashAllowance),0) " +
                             " as  WashAllowance, Isnull(SUM( Eps.ESI ),0) as esi,Isnull(SUM( Eps.otamt),0) as   otamt, " +
                             " Isnull(SUM(  Eps.esiempr ),0)  ESIEmpr " +
                             " ,( (Isnull(SUM(  Eps.ESI ),0)) +(Isnull(SUM(Eps.esiempr ),0)  )  )  as   total  from EmpPaySheet as Eps   " +
                             " INNER  JOIN  EmpDetails empd ON  empd.EmpId=eps.EmpId  " +
                             " inner  join   Clients C  " +
                             " on  C.ClientId=eps.ClientId and  Eps.Month='" + month + Year.Substring(2, 2) + "' AND EPS.NoOfDuties<10   " +
                             " and  Eps.ESI<=0   and  eps.ClientId=C.ClientId group by eps.ClientId,c.ClientName  order by eps.ClientId  ";

                }
                BindData(sqlqry);
            }
            #endregion
        }

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

        protected void LoadCnamepfdata()
        {
            string month = DateTime.Parse(txtmonth.Text.Trim()).Month.ToString();
            string Year = DateTime.Parse(txtmonth.Text.Trim()).Year.ToString();
            float totEmpEsi = 0;
            float totEmprEsi = 0;
            //string Sqlqry = "Select distinct(C.clientid),C.Clientname,Es.Empesi,Es.Empresi from esided Es " +
            //    "  inner join Empdetails E on E.empid=Es.empid join Clients C on E.unitid=C.clientid  " +
            //    " and month='" + month + Year.Substring(2, 2) + "' and E.Unitid in(Select c.Clientid from Clients c)";
            string Sqlqry = "Select C.clientid,C.Clientname,Sum(Es.Empesi) as Empesi,Sum(Es.Empresi) as Empresi from esided Es " +
                "  inner join Empdetails E on E.empid=Es.empid Inner join Clients C on E.unitid=C.clientid  " +
                " and Es.month='" + month + Year.Substring(2, 2) + "' GROUP BY C.Clientid,C.Clientname";// and E.Unitid in(Select c.Clientid from Clients c)";
            DataTable datapfdetails = config.ExecuteAdaptorAsyncWithQueryParams(Sqlqry).Result;
            if (datapfdetails.Rows.Count > 0)
            {
                for (int j = 0; j < GVListEmployees.Rows.Count; j++)
                {
                    float empEsi = 0;
                    float emprEsi = 0;
                    for (int i = 0; i < datapfdetails.Rows.Count; i++)
                    {
                        string Clientid = ((Label)GVListEmployees.Rows[j].FindControl("lblunitId")).Text.Trim();
                        if (Clientid == datapfdetails.Rows[i]["clientid"].ToString())
                        {
                            Label clientname = GVListEmployees.Rows[j].FindControl("lblclientname") as Label;
                            clientname.Text = datapfdetails.Rows[i]["clientname"].ToString();
                            string noofemps = ((Label)GVListEmployees.Rows[j].FindControl("lblstrength")).Text;

                            if (noofemps.ToString().Trim().Length != 0)
                            {
                                Label emppf = GVListEmployees.Rows[j].FindControl("lblepf") as Label;
                                empEsi = float.Parse(datapfdetails.Rows[i]["empesi"].ToString());
                                totEmpEsi += empEsi;
                                emppf.Text = empEsi.ToString("0.00");

                                Label emprf = GVListEmployees.Rows[j].FindControl("lblerpf") as Label;
                                emprEsi = float.Parse(datapfdetails.Rows[i]["empresi"].ToString());
                                totEmprEsi += emprEsi;
                                emprf.Text = emprEsi.ToString("0.00");
                                break;
                            }
                        }
                    }
                    Label Total = GVListEmployees.Rows[j].FindControl("lbltotal") as Label;
                    if (Total != null)
                    {
                        Total.Text = (emprEsi + empEsi).ToString("0.00");
                    }
                }


                //    for (int j = 0; j < GVListEmployees.Rows.Count; j++)
                //    {
                //        Label Total = GVListEmployees.Rows[j].FindControl("lbltotal") as Label;

                //        //double gross = double.Parse(GVListEmployees.Rows[j].Cells[3].Text);
                //        //double basicda = double.Parse(GVListEmployees.Rows[j].Cells[4].Text);

                //        Label emppf = GVListEmployees.Rows[j].FindControl("lblepf") as Label;
                //        double temppf = 0;
                //        if (String.IsNullOrEmpty(emppf.Text) != null)
                //        {
                //            if (emppf.Text.Trim().Length != 0)
                //            {
                //                temppf = double.Parse(emppf.Text);
                //            }
                //        }
                //        Label emprf = GVListEmployees.Rows[j].FindControl("lblerpf") as Label;
                //        double temprf = 0;
                //        if (String.IsNullOrEmpty(emprf.Text) != null)
                //        {
                //            if (emprf.Text.Trim().Length != 0)
                //            {
                //                temprf = double.Parse(emprf.Text);
                //            }
                //        }

                //        double total = temppf + temprf;
                //        Total.Text = total.ToString();
                //    }
            }
        }


        protected void BindData(string Sqlqry)
        {
            DataTable dt = config.ExecuteAdaptorAsyncWithQueryParams(Sqlqry).Result;
            if (dt.Rows.Count > 0)
            {
                lbltamttext.Visible = true;
                lbtn_Export.Visible = true;
                GVListEmployees.DataSource = dt;
                GVListEmployees.DataBind();
            }
            else
            {
                return;
            }
        }

        protected void ClearData()
        {
            GVListEmployees.DataSource = null;
            GVListEmployees.DataBind();
            lbltamttext.Visible = false;

        }

        protected void lbtn_Export_Click(object sender, EventArgs e)
        {
            gve.Export("AllUnitsEsiReport.xls", this.GVListEmployees);
        }


        float totalESIs = 0;
        float totalESIWAGES = 0;
        float totalESI = 0;
        float totalESIEmpr = 0;
        protected void GVListEmployees_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //    if (e.Row.RowType == DataControlRowType.DataRow)
            //    {
            //        float ESIWAGES = float.Parse(((Label)e.Row.FindControl("lblESIWAGES")).Text);
            //        totalESIWAGES += ESIWAGES;
            //        float ESI = float.Parse(((Label)e.Row.FindControl("lblESI")).Text);
            //        totalESI += ESI;
            //        float ESIEmpr = float.Parse(((Label)e.Row.FindControl("lblESIEmpr")).Text);
            //        totalESIEmpr += ESIEmpr;
            //        float ESIs = float.Parse(((Label)e.Row.FindControl("lblTotalESI")).Text);
            //        totalESIs += ESIs;
            //    }
            //    if (e.Row.RowType == DataControlRowType.Footer)
            //    {
            //        ((Label)e.Row.FindControl("lblTotalESIWAGES")).Text = totalESIWAGES.ToString();
            //        ((Label)e.Row.FindControl("lblTotalESI")).Text = totalESI.ToString();
            //        ((Label)e.Row.FindControl("lblTotalESIEmpr")).Text = totalESIEmpr.ToString();
            //        ((Label)e.Row.FindControl("lblTotalTotalESI")).Text = totalESIs.ToString();
            //    }
        }
    }
}