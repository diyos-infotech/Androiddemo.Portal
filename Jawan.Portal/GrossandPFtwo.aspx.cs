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
    public partial class GrossandPFtwo : System.Web.UI.Page
    {
        AppConfiguration config = new AppConfiguration();
        GridViewExportUtil gve = new GridViewExportUtil();
        //DataTable dt;
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
            }
        }

        protected void GetWebConfigdata()
        {
            EmpIDPrefix = GlobalData.Instance.GetEmployeeIDPrefix();
            Elength = (EmpIDPrefix.Trim().Length + 1).ToString();
            CmpIDPrefix = GlobalData.Instance.GetClientIDPrefix();
            Clength = (CmpIDPrefix.Trim().Length + 1).ToString();
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
            //ClearData();

            string date = string.Empty;

            int pfstatus = 0;
            if (txtmonth.Text.Trim().Length == 0)
            {
                LblResult.Text = "Please Select The Date";
                return;
            }

            if (txtmonth.Text.Trim().Length > 0)
            {
                date = DateTime.Parse(txtmonth.Text.Trim(), CultureInfo.GetCultureInfo("en-gb")).ToString();
            }

            string month = DateTime.Parse(date).Month.ToString();
            string Year = DateTime.Parse(date).Year.ToString();

            string sqlqry = string.Empty;

            if (ddlpftype.SelectedIndex == 0)
            {
                sqlqry = "select eps.ClientId,c.ClientName , COUNT(eps.EmpId) as   strength,Isnull(SUM(  Eps.Basic),0)  as  Basic, " +
                    " Isnull(SUM( Eps.Basic +  Eps.DA),0) " +
                   " as  basicda, Isnull(SUM(  Eps.PF ),0) as PF,Isnull(SUM( Eps.otamt),0) as   otamt,  " +
                   " Isnull(SUM(  Eps.pfempr ),0) as  pfempr ," +
                  "( (Isnull(SUM(  Eps.PF ),0)) +( Isnull(SUM(  Eps.pfempr ),0) ) )   as   total  from EmpPaySheet as Eps   " +
                     "  INNER  JOIN  EmpDetails empd ON  empd.EmpId=eps.EmpId  " +
                     " inner  join   Clients C   " +
                  "   on  C.ClientId=eps.ClientId and  Eps.Month='" + month + Year.Substring(2, 2) + "' AND EPS.NoOfDuties<> 0   " +
                 "    and  Eps.PF>0 and  eps.ClientId=C.ClientId group by eps.ClientId,c.ClientName  order by eps.ClientId   ";
                pfstatus = 1;
            }
            else
            {

                sqlqry = "select eps.ClientId,c.ClientName , COUNT(eps.EmpId) as   strength,Isnull(SUM(  Eps.Basic),0)  as  Basic, " +
                      " Isnull(SUM( Eps.Basic +  Eps.DA),0) " +
                     " as  basicda, Isnull(SUM(  Eps.PF ),0) as PF,Isnull(SUM( Eps.otamt),0) as   otamt,  " +
                     " Isnull(SUM(  Eps.pfempr ),0) as  pfempr ," +
                    "( (Isnull(SUM(  Eps.PF ),0)) +( Isnull(SUM(  Eps.pfempr ),0) ) )   as   total  from EmpPaySheet as Eps   " +
                       "  INNER  JOIN  EmpDetails empd ON  empd.EmpId=eps.EmpId  " +
                       " inner  join   Clients C   " +
                    "   on  C.ClientId=eps.ClientId and  Eps.Month='" + month + Year.Substring(2, 2) + "' AND EPS.NoOfDuties<> 0   " +
                   "    and  Eps.PF<=0 and  eps.ClientId=C.ClientId group by eps.ClientId,c.ClientName  order by eps.ClientId   ";

            }

            BindData(sqlqry, pfstatus);
            // LoadCnamepfdata();
            // TotalAmount();
        }

        protected void LoadCnamepfdata()
        {
            string date = string.Empty;
            if (txtmonth.Text.Trim().Length > 0)
            {
                date = DateTime.Parse(txtmonth.Text.Trim(), CultureInfo.GetCultureInfo("en-gb")).ToString();
            }

            string month = DateTime.Parse(date).Month.ToString();
            string Year = DateTime.Parse(date).Year.ToString();

            string Sqlqry = "Select distinct(C.clientid),C.Clientname,Sum(EP.Emppf) as Emppf,Sum(EP.Emprpf) as Emprpf from Epfded EP " +
                "  inner join Empdetails E on E.empid=EP.empid Inner join Clients C on E.unitid=C.clientid  " +
                " and month='" + month + Year.Substring(2, 2) + "' GROUP BY C.Clientid,C.Clientname";
            DataTable datapfdetails = config.ExecuteAdaptorAsyncWithQueryParams(Sqlqry).Result;
            if (datapfdetails.Rows.Count > 0)
            {
                for (int j = 0; j < GVListEmployees.Rows.Count; j++)
                {
                    float empPf = 0;
                    float emprPf = 0;
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
                                empPf = float.Parse(datapfdetails.Rows[i]["emppf"].ToString());
                                emppf.Text = empPf.ToString("0.00");

                                Label emprf = GVListEmployees.Rows[j].FindControl("lblerpf") as Label;
                                emprPf = float.Parse(datapfdetails.Rows[i]["emprpf"].ToString());
                                emprf.Text = emprPf.ToString("0.00");
                                break;
                            }
                        }
                    }
                    Label Total = GVListEmployees.Rows[j].FindControl("lbltotal") as Label;
                    if (Total != null)
                    {
                        Total.Text = (empPf + emprPf).ToString("0.00");
                    }
                }
            }
        }

        protected void BindData(string Sqlqry, int pfstatus)
        {
            string date = string.Empty;
            if (txtmonth.Text.Trim().Length > 0)
            {
                date = DateTime.Parse(txtmonth.Text.Trim(), CultureInfo.GetCultureInfo("en-gb")).ToString();
            }

            string month = DateTime.Parse(date).Month.ToString();
            string Year = DateTime.Parse(date).Year.ToString();
            DataTable dt = config.ExecuteAdaptorAsyncWithQueryParams(Sqlqry).Result;
            if (dt.Rows.Count > 0)
            {
                //lbltamttext.Visible = true;
                lbtn_Export.Visible = true;
                GVListEmployees.DataSource = dt;
                GVListEmployees.DataBind();
                foreach (GridViewRow Gvrow in GVListEmployees.Rows)
                {
                    string UnitId = ((Label)Gvrow.FindControl("lblunitId")).Text;
                    Label lblstrengthtwo = (Label)Gvrow.FindControl("lblstrengthtwo");
                    Label lblBasictwo = (Label)Gvrow.FindControl("lblBasictwo");

                    Label lblbasicdatwo = (Label)Gvrow.FindControl("lblbasicdatwo");
                    Label lblotamttwo = (Label)Gvrow.FindControl("lblotamttwo");

                    Label lblpftwo = (Label)Gvrow.FindControl("lblpftwo");
                    Label lblPFeMPRtwo = (Label)Gvrow.FindControl("lblPFeMPRtwo");

                    Label lbltotaltwo = (Label)Gvrow.FindControl("lbltotaltwo");

                    string SUBQUERY2 = string.Empty;
                    string IndVSqlQry = "Select  Count(EP.Empid) as Strength," +
                    " Isnull(SUM(EP.Basic),0)  as  Basic , (Isnull(SUM(EP.Basic),0)+ Isnull(SUM(EP.da),0)) as BasicDa ," +
                    " Isnull(SUM(EP.otamt),0) as OtAmt, Isnull(SUM(EP.pf),0) as PF ," +
                    "Isnull(SUM(EP.PFEmpr),0)  as PFEmpr," +
                    "( Isnull(SUM(EP.pf),0) +  Isnull(SUM(EP.PFEmpr),0) ) as  pftotal  " +
                    "  from  emppaysheet EP " +
                    " inner join Clients C on  C.Clientid = EP.Clientid " +
                    " inner join  empdetails E on E.Empid=EP.Empid   " +
                    " inner join  Empattendance EA on EA.Empid=EP.Empid  " +
                    " inner join  EMPESICodes ESC on ESC.Empid=EP.Empid   " +
                    " inner join  EMPEPFCodes EPC on EPC.Empid=EP.Empid  " +
                    "  and    EP.Clientid='" + UnitId + "'  and  EP.noofduties<>0  and EP.month='" + month + Year.Substring(2, 2) +
                    " '  and  EP.month=EA.Month  and  EP.Clientid=EA.Clientid  ";

                    DataTable IndDt = null;
                    if (ddlpftype.SelectedIndex == 0)
                    {
                        SUBQUERY2 = IndVSqlQry + " and  EP.pf>0  ";
                    }
                    else
                    {
                        SUBQUERY2 = IndVSqlQry + " and  EP.pf<=0 ";

                    }
                    IndDt = config.ExecuteAdaptorAsyncWithQueryParams(SUBQUERY2).Result;

                    if (IndDt.Rows.Count > 0)
                    {
                        lblstrengthtwo.Text = (Math.Round(decimal.Parse(IndDt.Rows[0]["Strength"].ToString()))).ToString();
                        lblBasictwo.Text = Math.Round(decimal.Parse(IndDt.Rows[0]["Basic"].ToString())).ToString();
                        lblbasicdatwo.Text = Math.Round(decimal.Parse(IndDt.Rows[0]["BasicDa"].ToString())).ToString();
                        lblotamttwo.Text = Math.Round(decimal.Parse(IndDt.Rows[0]["OtAmt"].ToString())).ToString();
                        lblpftwo.Text = Math.Round(decimal.Parse(IndDt.Rows[0]["PF"].ToString())).ToString();
                        lblPFeMPRtwo.Text = Math.Round(decimal.Parse(IndDt.Rows[0]["PFEmpr"].ToString())).ToString();
                        lbltotaltwo.Text = Math.Round(decimal.Parse(IndDt.Rows[0]["pftotal"].ToString())).ToString();
                    }
                    float BASICTWO = float.Parse(lblBasictwo.Text);
                    totalTotalSBasic += BASICTWO;
                    Label lblbasic2 = (Label)GVListEmployees.FooterRow.FindControl("lblTotalBasictwo");
                    lblbasic2.Text = totalTotalSBasic.ToString();

                    float BASICDATWO = float.Parse(lblbasicdatwo.Text);
                    totalBasicDATwo += BASICDATWO;
                    Label lblbasicda2 = (Label)GVListEmployees.FooterRow.FindControl("lblTotalbasicdatwo");
                    lblbasicda2.Text = totalBasicDATwo.ToString();

                    float TOTALOTTWO = float.Parse(lblotamttwo.Text);
                    totalOTTwo += TOTALOTTWO;
                    Label lbltotalot2 = (Label)GVListEmployees.FooterRow.FindControl("lblTotalOTtwo");
                    lbltotalot2.Text = totalOTTwo.ToString();

                    float TOTALPETWO = float.Parse(lblpftwo.Text);
                    totalTotalPFTwo += TOTALPETWO;
                    Label lblTotalpftwo2 = (Label)GVListEmployees.FooterRow.FindControl("lblTotalPFTwo");
                    lblTotalpftwo2.Text = totalTotalPFTwo.ToString();

                    float TOTALPFEMPRTWO = float.Parse(lblPFeMPRtwo.Text);
                    totalPFEmprTwo = TOTALPFEMPRTWO;
                    Label lblTotalpfempr2 = (Label)GVListEmployees.FooterRow.FindControl("lblTotalPFemprTwo");
                    lblTotalpfempr2.Text = totalPFEmprTwo.ToString();

                    float TOTALTWO = float.Parse(lbltotaltwo.Text);
                    totalTotalTwo += TOTALTWO;
                    Label lblTotaltotal2 = (Label)GVListEmployees.FooterRow.FindControl("lblTotaltwo");
                    lblTotaltotal2.Text = totalTotalTwo.ToString();
                }
                // TotalAmount();

            }
            else
            {
                GVListEmployees.DataSource = null;
                GVListEmployees.DataBind();
                lbtn_Export.Visible = false;
                LblResult.Text = "There Is No Pf Deductions For The Selected  Date/Client";
                return;
            }
        }

        //protected void TotalAmount()
        ////{

        //    float tnoofemps = 0;
        //    float tgross = 0;
        //    float tbasicda = 0;
        //    float temppf = 0;
        //    float temprpf = 0;
        //    float total = 0;

        //    for (int i = 0; i < GVListEmployees.Rows.Count; i++)
        //    {

        //        //total no of employees
        //        string tempnoofemps = ((Label)GVListEmployees.Rows[i].FindControl("lblstrength")).Text;
        //        if (String.IsNullOrEmpty(tempnoofemps) != null)
        //        {
        //            if (tempnoofemps.Trim().Length != 0)
        //            {
        //                tnoofemps += float.Parse(tempnoofemps);
        //            }
        //        }
        //        //Total Gross
        //        string tempgross = GVListEmployees.Rows[i].Cells[3].Text;
        //        if (String.IsNullOrEmpty(tempgross) != null)
        //        {
        //            if (tempgross.Trim().Length != 0)
        //            {
        //                tgross += float.Parse(tempgross);
        //            }
        //        }
        //        //Total Basic Da
        //        string tempbasicda = GVListEmployees.Rows[i].Cells[4].Text;
        //        if (String.IsNullOrEmpty(tempbasicda) != null)
        //        {
        //            if (tempbasicda.Trim().Length != 0)
        //            {
        //                tbasicda += float.Parse(tempbasicda);
        //            }
        //        }
        //        //Total Emppf lblepf
        //        string tempemppf = ((Label)GVListEmployees.Rows[i].FindControl("lblepf")).Text;
        //        if (String.IsNullOrEmpty(tempemppf) != null)
        //        {
        //            if (tempemppf.Trim().Length != 0)
        //            {
        //                temppf += float.Parse(tempemppf);
        //            }
        //        }
        //        //Total Emprpf

        //        string tempemprpf = ((Label)GVListEmployees.Rows[i].FindControl("lblerpf")).Text;
        //        if (String.IsNullOrEmpty(tempemprpf) != null)
        //        {
        //            if (tempemprpf.Trim().Length != 0)
        //            {
        //                temprpf += float.Parse(tempemprpf);
        //            }
        //        }
        //        //  Total 
        //        string tempotal = ((Label)GVListEmployees.Rows[i].FindControl("lbltotal")).Text;
        //        if (String.IsNullOrEmpty(tempotal) != null)
        //        {
        //            if (tempotal.Trim().Length != 0)
        //            {
        //                total += float.Parse(tempotal);
        //            }
        //        }
        //    }
        //lblstrength.Text = tnoofemps.ToString();
        //lblgross.Text = tgross.ToString("0.00");
        //lblbasicda.Text = tbasicda.ToString("0.00");
        //lblpfemp.Text = temppf.ToString("0.00");
        //lblpfempr.Text = temprpf.ToString("0.00");
        //lbltotal.Text = total.ToString("0.00");



        //if (GVListEmployees.Rows.Count == 0)
        //{
        //    lblstrength.Text = "";
        //    lblgross.Text = "";
        //    lblbasicda.Text = "";
        //    lblpfemp.Text = "";
        //    lblpfempr.Text = "";
        //    lbltotal.Text = "";

        //}
        //}

        //protected void ClearAmontdetails()
        //{
        //    lbltamttext.Visible = false;
        //    //lbltmtemppf.Text = "";
        //    //lbltemprpf.Text = "";
        //}

        protected void ClearData()
        {
            GVListEmployees.DataSource = null;
            GVListEmployees.DataBind();
            //lbltamttext.Visible = false;
            //LblResult.Text = "";
            //lblstrength.Text = "";
            //lblgross.Text = "";
            //lblbasicda.Text = "";
            //lblpfemp.Text = "";
            //lblpfempr.Text = "";
            //lbltotal.Text = "";



        }

        protected void lbtn_Export_Click(object sender, EventArgs e)
        {
            gve.Export("AllUnitsPFReport.xls", this.GVListEmployees);
        }


        float totalBasic = 0;
        float totalTotalSBasic = 0;
        float totalbasicda = 0;
        float totalBasicDATwo = 0;
        float totalotamt = 0;
        float totalOTTwo = 0;
        float totalPF = 0;
        float totalTotalPFTwo = 0;
        float totalPFeMPR = 0;
        float totalPFEmprTwo = 0;
        float totalTotal = 0;
        float totalTotalTwo = 0;
        protected void GVListEmployees_RowDataBound(object sender, GridViewRowEventArgs e)
        {


            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                float Basic = float.Parse(((Label)e.Row.FindControl("lblBasic")).Text);
                totalBasic += Basic;
                //decimal SBasic = decimal.Parse(((Label)e.Row.FindControl("lblBasictwo")).Text);
                //totalTotalSBasic += SBasic;
                float basicda = float.Parse(((Label)e.Row.FindControl("lblbasicda")).Text);
                totalbasicda += basicda;
                //decimal BasicDaTwo = decimal.Parse(((Label)e.Row.FindControl("lblbasicdatwo")).Text);
                //totalBasicDATwo += BasicDaTwo;
                float otamt = float.Parse(((Label)e.Row.FindControl("lblotamt")).Text);
                totalotamt += otamt;
                //decimal OTTwo = decimal.Parse(((Label)e.Row.FindControl("lblotamttwo")).Text);
                //totalOTTwo += OTTwo;
                float PF = float.Parse(((Label)e.Row.FindControl("lblpf")).Text);
                totalPF += PF;
                //decimal PFTwo = decimal.Parse(((Label)e.Row.FindControl("lblPFTwo")).Text);
                //totalTotalPFTwo += PFTwo;
                float PFeMPR = float.Parse(((Label)e.Row.FindControl("lblPFeMPR")).Text);
                totalPFeMPR += PFeMPR;
                //decimal PFEmprTwo = decimal.Parse(((Label)e.Row.FindControl("lblPFeMPRtwo")).Text);
                //totalPFEmprTwo += PFEmprTwo;
                float Total = float.Parse(((Label)e.Row.FindControl("lbltotal")).Text);
                totalTotal += Total;
                //decimal TotalTwo = decimal.Parse(((Label)e.Row.FindControl("lbltotaltwo")).Text);
                //totalTotalTwo += TotalTwo;
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                ((Label)e.Row.FindControl("lblTotalBasic")).Text = totalBasic.ToString();
                //((Label)e.Row.FindControl("lblTotalBasictwo")).Text = totalTotalSBasic.ToString();
                ((Label)e.Row.FindControl("lblTotalbasicda")).Text = totalbasicda.ToString();
                //((Label)e.Row.FindControl("lblTotalbasicdatwo")).Text = totalBasicDATwo.ToString();
                ((Label)e.Row.FindControl("lblTotalotamt")).Text = totalotamt.ToString();
                //((Label)e.Row.FindControl("lblTotalOTtwo")).Text = totalOTTwo.ToString();
                ((Label)e.Row.FindControl("lblTotalpf")).Text = totalPF.ToString();
                //((Label)e.Row.FindControl("lblTotalPFTwo")).Text = totalTotalPFTwo.ToString();
                ((Label)e.Row.FindControl("lblTotalPFeMPR")).Text = totalPFeMPR.ToString();
                //((Label)e.Row.FindControl("lblTotalPFemprTwo")).Text = totalPFEmprTwo.ToString();
                ((Label)e.Row.FindControl("lblTotal")).Text = totalTotal.ToString();
                //((Label)e.Row.FindControl("lblTotaltwo")).Text = totalTotalTwo.ToString();
            }

        }
    }
}