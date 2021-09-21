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
    public partial class Reminders : System.Web.UI.Page
    {
        AppConfiguration config = new AppConfiguration();
        GridViewExportUtil gve = new GridViewExportUtil();
        string CmpIDPrefix = "";
        string BranchID = "";
        string Emp_id = "";
        protected void Page_Load(object sender, EventArgs e)
        {

            CmpIDPrefix = Session["CmpIDPrefix"].ToString();
            BranchID = Session["BranchID"].ToString();
            Emp_id= Session["Emp_Id"].ToString();
            if (Emp_id=="1" || Emp_id == "3")
            {
                LinkMarketting.Visible = true;
                LinkLogout.Visible = false;
            }
            string SqlQry = "select isnull(empfname,'')+' '+ isnull(empMname,'')+' ' + isnull(empLname,'') + '<br/>[  Ph.No:' +isnull(EmpPhone,'NA') + ']' + ' '  as Name  " +
                            "  From Empdetails   Where EmpDtofBirth='" + DateTime.Now.Date.ToString("MM/dd/yyyy") + "' and Branch="+ BranchID + " ";
            DataTable Dt = config.ExecuteAdaptorAsyncWithQueryParams(SqlQry).Result;
            if (Dt.Rows.Count > 0)
            {

                //for (int i = 0; i < Dt.Rows.Count; i++)
                //{
                //    lblGVBirthday.Text += Dt.Rows[i]["Name"].ToString() + "<br/><br/>";
                //}
                GVBirthday.DataSource = Dt;
                GVBirthday.DataBind();
                GVBirthday_Viewmore.DataSource = Dt;
                GVBirthday_Viewmore.DataBind();

            }



            //Code for Latest 5 Bill generations as on 20/12/2013 by venkat

            SqlQry = "select top(5) '<b>Bill No:</b> '+convert(varchar,BillNo)+'   Generated to ' +c.ClientName+'  '+UnitId+' on  ' " +
                " +CONVERT(varchar(10),Timings,103) +'  at  '+right(convert(varchar(30),Timings,22),11)+'   " +
                " Grand Total Amount Rs.' +CONVERT(varchar,grandtotal) as BillStaus from UnitBill inner join Clients c on " +
                " unitbill.UnitId=c.ClientId where  unitbill.UnitId like '" + CmpIDPrefix + "%'and c.Branch=" + BranchID + " order by Timings desc";
            Dt = config.ExecuteAdaptorAsyncWithQueryParams(SqlQry).Result;
            if (Dt.Rows.Count > 0)
            {
                gvLatestbills.DataSource = Dt;
                gvLatestbills.DataBind();
                gvLatestbills_Viewmore.DataSource = Dt;
                gvLatestbills_Viewmore.DataBind();

            }

            //Code for Latest 5 Payment generations as on 20/12/2013 by venkat

            SqlQry = "select top(5) 'Paysheet Generated to ' +c.ClientName+'  '+e.ClientId+' on  ' +CONVERT(varchar(10),e.Timings,103) " +
                " +'  at  '+right(convert(varchar(30),e.Timings,22),11)+'  Grand Total Amount Rs.' +CONVERT(varchar,SUM(e.ActualAmount)) " +
            " as Paysheetstatus from EmpPaySheet e inner join Clients c on e.ClientId=c.ClientId where e.clientid like '" + CmpIDPrefix + "%' and c.Branch="+ BranchID+ " group by c.ClientName,e.ClientId,e.Timings " +
            " order by Timings desc ";
            Dt = config.ExecuteAdaptorAsyncWithQueryParams(SqlQry).Result;
            if (Dt.Rows.Count > 0)
            {
                gvlatesPaysheet.DataSource = Dt;
                gvlatesPaysheet.DataBind();
                gvlatesPaysheet_Viewmore.DataSource = Dt;
                gvlatesPaysheet_Viewmore.DataBind();
            }

            //Code for Latest 5 Reciepts as on 20/12/2013 by venkat

            SqlQry = "select Top(5) 'Received   Rs.'+CONVERT(varchar,RecievedAmt)  + ' from '+R.Clientid+ ' on '+CONVERT(varchar(10),Timings,103)+ '" +
                " at ' +right(convert(varchar(30),Timings,22),11)+'  ( Reciept No. '+RecieptNo+ ')' as Reciepts from ReceiptMaster R inner join Clients C on C.ClientId=R.ClientId  where R.clientid like '" + CmpIDPrefix + "%' and C.Branch="+BranchID+" order by Timings desc";
            Dt = config.ExecuteAdaptorAsyncWithQueryParams(SqlQry).Result;
            if (Dt.Rows.Count > 0)
            {
                gvReciepts.DataSource = Dt;
                gvReciepts.DataBind();
                gvReciepts.DataSource = Dt;
                gvReciepts.DataBind();
            }



            Hashtable ht = new Hashtable();
            string ProcedureName = "BillingDetails";
            ht.Add("@clientidprefix", CmpIDPrefix);
            ht.Add("@Branch", BranchID);
            System.Data.DataTable DtInActiveClients = config.ExecuteAdaptorAsyncWithParams(ProcedureName, ht).Result;

            if (DtInActiveClients.Rows.Count > 0)
            {
                gvBills.DataSource = DtInActiveClients;
                gvBills.DataBind();
                gvBills_Viewmore.DataSource = DtInActiveClients;
                gvBills_Viewmore.DataBind();
            }


            ProcedureName = "EmpPaysheetDetails";
            ht = new Hashtable();
            ht.Add("@clientidprefix", CmpIDPrefix);
            ht.Add("@Branch", BranchID);
            DtInActiveClients =config.ExecuteAdaptorAsyncWithParams(ProcedureName, ht).Result;

            if (DtInActiveClients.Rows.Count > 0)
            {
                gvPaysheets.DataSource = DtInActiveClients;
                gvPaysheets.DataBind();
                gvPaysheets_Viewmore.DataSource = DtInActiveClients;
                gvPaysheets_Viewmore.DataBind();

            }

            ProcedureName = "GetExpiredContacts";
            ht = new Hashtable();
            ht.Add("@clientidprefix", CmpIDPrefix);
            ht.Add("@Branch", BranchID);
            DtInActiveClients = config.ExecuteAdaptorAsyncWithParams(ProcedureName, ht).Result;

            if (DtInActiveClients.Rows.Count > 0)
            {
                GVContract.DataSource = DtInActiveClients;
                GVContract.DataBind();
                GVContract_Viewmore.DataSource = DtInActiveClients;
                GVContract_Viewmore.DataBind();
            }



        }
    }
}