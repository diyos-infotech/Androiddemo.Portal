using KLTS.Data;
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Security;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Text;
using System.Collections.Generic;
using Jawan.Portal.DAL;
namespace Jawan.Portal
{
    public partial class Index : System.Web.UI.Page
    {
        AppConfiguration config = new AppConfiguration();
        GridViewExportUtil gve = new GridViewExportUtil();
        StringBuilder str = new StringBuilder();

        protected void Page_Load(object sender, EventArgs e)
        {
            GetBillGenratedData();
            GetPaysheetGenratedData();
            //GetProfitMarginData();
        }


        public void GetBillGenratedData()
        {
            int Currentmonth = DateTime.Now.Month;
            string Currentyear = DateTime.Now.Year.ToString();
            string monthval = (Currentmonth - 1) + Currentyear.Substring(2, 2);
            string monthname = "";
            DateTimeFormatInfo mfi = new DateTimeFormatInfo();

            DateTime date = DateTime.Now;
            monthname = mfi.GetMonthName(date.Month).ToString();

            string UnitBillQuery = "select COUNT(*)  from UnitBill where month='" + monthval + "'";
            DataTable DtUnitBill = config.ExecuteAdaptorAsyncWithQueryParams(UnitBillQuery).Result;

            int TotalBills = 0;

            if (DtUnitBill.Rows.Count > 0)
            {

                TotalBills = int.Parse(DtUnitBill.Rows[0][0].ToString());
            }

            string ClientsQuery = " select COUNT(*) from Clients where Invoice=1 ";
            DataTable DtClients = config.ExecuteAdaptorAsyncWithQueryParams(ClientsQuery).Result;

            int TotalClients = 0;

            if (DtClients.Rows.Count > 0)
            {

                TotalClients = int.Parse(DtClients.Rows[0][0].ToString());
            }

            lblBilldescription.Text = "Bills Generated For the <br> month of " + monthname.ToString() + " - " + Currentyear;
            lblbillOutput.Text = TotalBills.ToString() + " / " + TotalClients.ToString();

        }

        public void GetPaysheetGenratedData()
        {
            int Currentmonth = DateTime.Now.Month;
            string Currentyear = DateTime.Now.Year.ToString();
            string monthval = (Currentmonth - 1) + Currentyear.Substring(2, 2);
            string monthname = "";
            DateTimeFormatInfo mfi = new DateTimeFormatInfo();

            DateTime date = DateTime.Now;
            monthname = mfi.GetMonthName(date.Month).ToString();

            string EmpPaysheetQuery = "select count(distinct(ClientId)) from EmpPaySheet where month='" + monthval + "'";
            DataTable DtEmpPaysheet = config.ExecuteAdaptorAsyncWithQueryParams(EmpPaysheetQuery).Result;

            int TotalPaysheets = 0;

            if (DtEmpPaysheet.Rows.Count > 0)
            {

                TotalPaysheets = int.Parse(DtEmpPaysheet.Rows[0][0].ToString());
            }

            string ClientsQuery = " select COUNT(*) from clients where Paysheet=1 ";
            DataTable DtClients = config.ExecuteAdaptorAsyncWithQueryParams(ClientsQuery).Result;

            int TotalClients = 0;

            if (DtClients.Rows.Count > 0)
            {

                TotalClients = int.Parse(DtClients.Rows[0][0].ToString());
            }

            lblPaysheetdescription.Text = "Paysheets Generated For the <br> month of " + monthname.ToString() + " - " + Currentyear;
            lblPaysheetOutput.Text = TotalPaysheets.ToString() + " / " + TotalClients.ToString();

        }

        //public void GetProfitMarginData()
        //{
        //    int Currentmonth = DateTime.Now.Month;
        //    string Currentyear = DateTime.Now.Year.ToString();
        //    string monthval = (Currentmonth - 1) + Currentyear.Substring(2, 2);
        //    string monthname = "";
        //    DateTimeFormatInfo mfi = new DateTimeFormatInfo();

        //    DateTime date = DateTime.Now;
        //    monthname = mfi.GetMonthName(date.Month).ToString();

        //    string ProfitMarginQuery = "select (select (SUM(GrandTotal)-(sum(ServiceTax)+ sum(CESS)+sum(SHECess))) from UnitBill where month='" + monthval + "') as BillTotal, " +
        //                              "(select (SUM(isnull(PFEmpr,0))+sum(isnull(Gross,0)) +  sum(isnull(Otamt,0))+ sum(isnull(NHSAMT,0))+sum(isnull(Npotsamt,0))+SUM(isnull(esiempr,0))) from EmpPaySheet where month='" + monthval + "') as PaysheetTotal ";
        //    DataTable DtProfitMargin = SqlHelper.Instance.GetTableByQuery(ProfitMarginQuery);

        //    decimal TotalPaysheets = 0; decimal TotalBills = 0; decimal TotalProfitLossPer = 0; decimal TotalProfitLoss = 0;

        //    if (DtProfitMargin.Rows.Count > 0)
        //    {

        //        TotalPaysheets = Convert.ToDecimal(DtProfitMargin.Rows[0][1].ToString());
        //        TotalBills = Convert.ToDecimal(DtProfitMargin.Rows[0][0].ToString());
        //        TotalProfitLossPer = (TotalPaysheets / TotalBills) * 100;
        //        TotalProfitLoss = (100 - TotalProfitLossPer);
        //    }


        //    lblProfitMargindescription.Text = "Net Profit % For the <br> month of " + monthname.ToString() + " - " + Currentyear;
        //    lblProfitMarginOutput.Text = TotalProfitLoss.ToString("0.00") + " % ";

        //}

        private DataTable GetPaysheetGeneratedData()
        {
            string cmd = "(select 'PaysheetGenerated' as Name,count(distinct(ClientId)) as Value from EmpPaySheet where MONTH='" + ddlmonth.SelectedValue + "' ) " +
                         " union all " +
                            "(select 'TotalPaysheets' as Name,COUNT(*) as Value from clients where Paysheet=1) " +
                            " union all " +
                            "(select 'BillsGenerated' as Name,COUNT(*) as Value from UnitBill where MONTH='" + ddlmonth.SelectedValue + "') " +
                            "union all " +
                            "(select 'TotalBills' as Name,COUNT(*) as Value from clients where Invoice=1) ";
            DataTable dt = config.ExecuteAdaptorAsyncWithQueryParams(cmd).Result;
            return dt;

        }

        private void BindChart2()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = GetPaysheetGeneratedData();
                str.Append(@"<script type=*text/javascript*> google.load( *visualization*, *1*, {packages:[*corechart*]});
                       google.setOnLoadCallback(drawChart);
                       function drawChart() {
                                    var data = new google.visualization.DataTable();
                                    data.addColumn('string', 'Name');
                                    data.addColumn('number', 'Value');
                                    data.addRows(" + dt.Rows.Count + ");");

                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    str.Append("data.setValue( " + i + "," + 0 + "," + "'" + dt.Rows[i]["Name"].ToString() + "');");
                    str.Append("data.setValue(" + i + "," + 1 + "," + dt.Rows[i]["Value"].ToString() + ") ;");

                }

                str.Append(" var chart = new google.visualization.PieChart(document.getElementById('chart_div'));");
                str.Append(" chart.draw(data, {width:1300, height: 500, title: 'Billing and Paysheet',is3D:true");

                str.Append("}); }");
                str.Append("</script>");
                lt.Text = str.ToString().Replace('*', '"');
            }
            catch
            { }
        }


        private DataTable GetData()
        {

            string cmd = "select eps.month,sum(ActualAmount) as ActualAmount,GrandTotal,c.ClientName from emppaysheet eps inner join unitbill ub on eps.clientid=ub.unitid and eps.month=ub.month inner join Clients C on C.ClientId=eps.ClientId where eps.month='" + ddlmonth.SelectedValue + "' group by eps.month,eps.ClientId,ub.UnitId,ub.GrandTotal,c.ClientName";
            DataTable dt = config.ExecuteAdaptorAsyncWithQueryParams(cmd).Result;
            return dt;
        }

        private void BindChart()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = GetData();
                str.Append(@"<script type=*text/javascript*> google.load( *visualization*, *1*, {packages:[*corechart*]});
                       google.setOnLoadCallback(drawChart);
                       function drawChart() {
                                    var data = new google.visualization.DataTable();
                                    data.addColumn('string', 'ClientName');
                                    data.addColumn('number', 'Billing');
                                    data.addColumn('number', 'Paysheet');      
 
                                    data.addRows(" + dt.Rows.Count + ");");

                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    str.Append("data.setValue( " + i + "," + 0 + "," + "'" + dt.Rows[i]["ClientName"].ToString() + "');");
                    str.Append("data.setValue(" + i + "," + 1 + "," + dt.Rows[i]["GrandTotal"].ToString() + ") ;");
                    str.Append("data.setValue(" + i + "," + 2 + "," + dt.Rows[i]["ActualAmount"].ToString() + ") ;");
                }

                str.Append(" var chart = new google.visualization.ColumnChart(document.getElementById('chart_div'));");
                str.Append(" chart.draw(data, {width:1300, height: 500, title: 'Billing Vs Paysheet',");
                str.Append("hAxis: {title: 'Clients', titleTextStyle: {color: 'green'}}");
                str.Append("}); }");
                str.Append("</script>");
                lt.Text = str.ToString().Replace('*', '"');
            }
            catch
            { }
        }




        protected void Button1_Click(object sender, EventArgs e)
        {
            BindChart();
        }
    }
}