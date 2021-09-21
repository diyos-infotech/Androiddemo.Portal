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
    public partial class DummyTransfer : System.Web.UI.Page
    {
        int oderid = 0;
        AppConfiguration config = new AppConfiguration();
        GridViewExportUtil gve = new GridViewExportUtil();
        string EmpIDPrefix = "";
        string CmpIDPrefix = "";
        string Elength = "";
        string Clength = "";
        string BranchID = "";

        private void OrderId()
        {
            string selectqueryoderid = "select max(cast(OrderId as int)) as  OrderId from EmpPostingOrder ";
            DataTable dtable = config.ExecuteAdaptorAsyncWithQueryParams(selectqueryoderid).Result;
            if (dtable.Rows.Count > 0)
            {
                if (String.IsNullOrEmpty(dtable.Rows[0]["OrderId"].ToString()) == false)
                {
                    oderid = (Convert.ToInt32(dtable.Rows[0]["OrderId"].ToString())) + 1;
                    txtorderid.Text = oderid.ToString();
                }
                else
                {
                    txtorderid.Text = "1";
                }
            }

        }

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
                    }
                    else
                    {
                        Response.Redirect("login.aspx");
                    }
                    // ddlempid.Items.Add("--select--");
                    OrderId();
                    BindData();
                    LoadClientNames();
                    LoadClientList();
                    LoadNames();
                    LoadEmpIds();
                    LoadDesignations();
                    txtorderdate.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
                    txtjoindate.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
                }
            }
            catch (Exception ex)
            {

            }
        }



        protected void GetWebConfigdata()
        {

            EmpIDPrefix = Session["EmpIDPrefix"].ToString();
            CmpIDPrefix = Session["CmpIDPrefix"].ToString();
            BranchID = Session["BranchID"].ToString();
        }


        protected void LoadDesignations()
        {
            DataTable DtDesignations = GlobalData.Instance.LoadDesigns();
            if (DtDesignations.Rows.Count > 0)
            {
                ddlDesignation.DataValueField = "Designid";
                ddlDesignation.DataTextField = "Design";
                ddlDesignation.DataSource = DtDesignations;
                ddlDesignation.DataBind();
            }
            ddlDesignation.Items.Insert(0, "-Select-");
        }

        protected void Fillcname()
        {
            if (ddlUnit.SelectedIndex > 0)
            {
                ddlcname.SelectedValue = ddlUnit.SelectedValue;
            }
            else
            {
                ddlcname.SelectedIndex = 0;
            }
        }

        protected void FillClientid()
        {
            if (ddlcname.SelectedIndex > 0)
            {
                ddlUnit.SelectedValue = ddlcname.SelectedValue;
            }
            else
            {
                ddlUnit.SelectedIndex = 0;
            }
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

        }

        protected void LoadClientList()
        {
            DataTable DtClientNames = GlobalData.Instance.LoadCIds(CmpIDPrefix,BranchID);
            if (DtClientNames.Rows.Count > 0)
            {
                ddlUnit.DataValueField = "Clientid";
                ddlUnit.DataTextField = "Clientid";
                ddlUnit.DataSource = DtClientNames;
                ddlUnit.DataBind();
            }
            ddlUnit.Items.Insert(0, "-Select-");
        }

        protected void BindData()
        {

            #region  Old Coding

            //string selectquery = "select empid from empdetails Order By ( cast(substring(empid,4, 6) as int)) ";
            //DataTable empTable = SqlHelper.Instance.GetTableByQuery(selectquery);
            //ddlempname.SelectedIndex = 0;
            //ddlempid.SelectedIndex = 0;
            //ddlDesignation.SelectedIndex = 0;
            //if (ddlUnit.SelectedIndex > 0)
            //{
            //    DataTable data = null;
            //    int type = -1;
            //    DateTime startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            //    DateTime endDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, GlobalData.Instance.GetNoOfDaysForNextMonth());
            //    //string strQry = "select EmpPostingOrder.EmpId,EmpDetails.EmpmName as Name,EmpDetails.EmpDesgn as Designation " +
            //    //    " from EmpDetails INNER JOIN EmpPostingOrder ON EmpPostingOrder.EmpId=EmpDetails.EmpId AND EmpPostingOrder.ToUnitId='" +
            //    //    ddlUnit.SelectedValue + "' AND EmpPostingOrder.TransferType =" + type + " Order By Cast (substring(EmpPostingOrder.Empid," + Elength + ", 6) as int)";
            //    string strQry = "select EmpPostingOrder.EmpId,EmpDetails.EmpmName as Name,EmpPostingOrder.Desgn as Designation from EmpPostingOrder INNER JOIN EmpDetails ON EmpPostingOrder.EmpId=EmpDetails.EmpId AND EmpPostingOrder.ToUnitId='" +
            //        ddlUnit.SelectedValue + "' AND EmpPostingOrder.TransferType =" + type + " AND JoiningDt>='" + startDate + "' AND JoiningDt<='" + endDate + "' Order By Cast (substring(EmpPostingOrder.Empid," + Elength + ", 6) as int)";
            //    DataTable dTable = SqlHelper.Instance.GetTableByQuery(strQry);
            //    if (dTable.Rows.Count > 0)
            //    {
            //        string selectQuery = "select empid,(ISNULL(empfname,'' )+' '+ ISNULL(empmname,'') +' '+ISNULL(Emplname,'')) as Name, EmpDesgn as Designation from EmpDetails where UnitId='" +
            //            ddlUnit.SelectedItem.ToString() + "' Order By (cast(substring(Empid," + Elength + ", 6) as int)) ";
            //        data = SqlHelper.Instance.GetTableByQuery(selectQuery);

            //        bool match;
            //        string empid;
            //        string id;
            //        for (int i = 0; i < dTable.Rows.Count; i++)
            //        {
            //            match = false;
            //            empid = dTable.Rows[i]["EmpId"].ToString();
            //            for (int j = 0; j < data.Rows.Count; j++)
            //            {
            //                id = data.Rows[j]["EmpId"].ToString();
            //                if (id.CompareTo(empid) == 0)
            //                {
            //                    match = true;
            //                    break;
            //                }
            //            }
            //            if (match == false)
            //            {
            //                DataRow dr = data.NewRow();
            //                dr["empid"] = dTable.Rows[i]["EmpId"].ToString();
            //                dr["Name"] = dTable.Rows[i]["Name"].ToString();
            //                dr["Designation"] = dTable.Rows[i]["Designation"].ToString();
            //                data.Rows.Add(dr);
            //            }
            //        }
            //    }
            //    gvemppostingorder.DataSource = data;
            //    gvemppostingorder.DataBind();
            //}
            //MessageLabel.Text = "";

            #endregion  // End Old Coding
        }


        protected void TrnasferedEmployeesBindData()
        {
            //string selectquery = "select empid,UnitId from empdetails Order By  cast(substring(empid,4, 6) as int)";
            //DataTable empTable = SqlHelper.Instance.GetTableByQuery(selectquery);
            ddlempname.SelectedIndex = 0;
            ddlempid.SelectedIndex = 0;
            ddlDesignation.SelectedIndex = 0;
            if (ddlUnit.SelectedIndex > 0)
            {
                DataTable data = new DataTable();
                string selectQuery = "Select ed.EmpId,(ISNULL( ed.empfname,'' )+' '+ ISNULL(ed.empmname,'') +' '+ISNULL(ed.Emplname,'')) as  Name," +
                     " D.design  as Desgn,EPO.Desgn as Designid ,(ISNULL(EPO.PF,0)) as pf ,(ISNULL(EPO.ESI,0)) as esi ,(ISNULL(EPO.PT,0)) as pt from Empdetails ed, " +
                     " Emppostingorder EPO inner join Designations D on D.designid=EPO.Desgn  where ed.Empid=EPO.Empid  and tounitid='" +
                     ddlUnit.SelectedValue + "'    Order by right(isnull(EPO.Empid,0),6)";


                data = config.ExecuteAdaptorAsyncWithQueryParams(selectQuery).Result;

                if (data.Rows.Count > 0)
                {
                    gvemppostingorder.DataSource = data;
                    gvemppostingorder.DataBind();
                }
                else
                {
                    gvemppostingorder.DataSource = null;
                    gvemppostingorder.DataBind();
                }
            }

            LoadEmployeeTransfertype();
            MessageLabel.Text = "";
        }

        protected void LoadEmployeeTransfertype()
        {
            DateTime startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime endDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, GlobalData.Instance.GetNoOfDaysForNextMonth());

            for (int empindex = 0; empindex < gvemppostingorder.Rows.Count; empindex++)
            {
                string empid = gvemppostingorder.Rows[empindex].Cells[0].Text;
                //string Design = gvemppostingorder.Rows[empindex].Cells[2].Text;
                string Design = ((Label)gvemppostingorder.Rows[empindex].FindControl("ddldesignid")).Text;

                string sqlqry = "Select TransferType from EmpPostingOrder where Empid='" + empid + "'  and  Desgn='" + Design + "'" +

                     " and tounitid='" + ddlUnit.SelectedValue + "'";

                DataTable dt = config.ExecuteAdaptorAsyncWithQueryParams(sqlqry).Result;
                Label transfertype = gvemppostingorder.Rows[empindex].FindControl("lbltransfertype") as Label;

                if (dt.Rows.Count > 0)
                {

                    if (dt.Rows[0]["TransferType"].ToString() == "0")
                    {
                        transfertype.Text = "Temp. Transfer";
                    }
                    if (dt.Rows[0]["TransferType"].ToString() == "1")
                    {
                        transfertype.Text = "Posting Order";
                    }
                    if (dt.Rows[0]["TransferType"].ToString() == "-1")
                    {
                        transfertype.Text = "Dummy transfer";
                    }
                }
            }
        }


        protected void btntransfer_Click(object sender, EventArgs e)
        {
            int transfertype = -1;
            #region Check Validations
            DateTime joindate = new DateTime();
            DateTime relivedate = new DateTime(1900, 01, 01);
            DateTime orddate = new DateTime();

            string unitid = null;
            if (ddlUnit.SelectedIndex > 0)
                unitid = ddlUnit.SelectedItem.ToString();
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showlalert", "alert('Please Select UnitId');", true);

                return;
            }
            string empid = null;
            if (ddlempid.SelectedIndex > 0)
                empid = ddlempid.SelectedItem.ToString();
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showlalert", "alert('Please Select Employee Id');", true);
                return;
            }

            var testDate = 0;
            if (txtjoindate.Text.Trim().Length > 0)
            {
                testDate = GlobalData.Instance.CheckEnteredDate(txtjoindate.Text);
                if (testDate > 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('You Are Entered Invalid JOINING DATE.Date Format Should be [DD/MM/YYYY].Ex.01/01/1990');", true);
                    return;
                }
                joindate = DateTime.Parse(txtjoindate.Text, CultureInfo.GetCultureInfo("en-gb"));
                relivedate = joindate;

            }



            if (txtorderdate.Text.Trim().Length > 0)
            {
                testDate = GlobalData.Instance.CheckEnteredDate(txtorderdate.Text);
                if (testDate > 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('You Are Entered Invalid ORDER DATE.Date Format Should be [DD/MM/YYYY].Ex.01/01/1990');", true);
                    return;
                }
                orddate = DateTime.Parse(txtorderdate.Text, CultureInfo.GetCultureInfo("en-gb"));
            }


            string orderid = txtorderid.Text.Trim();
            string remarks = txtremarks.Text.Trim();

            if (orddate > joindate)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showlalert", "alert('Joining Date should be greater than Order Date');", true);
                return;
            }

            string designation = "";
            if (ddlDesignation.SelectedIndex > 0)
                designation = ddlDesignation.SelectedValue;
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showlalert", "alert('Please select Designation for transfer');", true);
                return;
            }

            #endregion  // End Validation s

            #region PF,ESI,PT Check Validations

            int PF = 1;

            if (chkpf.Checked)
            {
                PF = 1;
            }
            else
            {
                PF = 0;
            }

            int ESI = 1;

            if (chkesi.Checked)
            {
                ESI = 1;
            }
            else
            {
                ESI = 0;
            }

            int PT = 1;

            if (chkpt.Checked)
            {
                PT = 1;
            }
            else
            {
                PT = 0;
            }
            #endregion
            string insertquery;
            orddate = DateTime.Now.Date;
            joindate = DateTime.Now.Date;
            relivedate = DateTime.Now.Date;
            try
            {
                #region Check Employee Transfer or not
                string strQry = "Select * from EmpPostingOrder where empid='" + empid + "' AND tounitid='" + unitid + "'";
                DataTable potable = config.ExecuteAdaptorAsyncWithQueryParams(strQry).Result;
                if (potable.Rows.Count > 0)
                {
                    string oldDesig = potable.Rows[0]["desgn"].ToString();
                    if (oldDesig.CompareTo(designation) == 0)
                    {
                        strQry = "update    EmpPostingOrder   set pf=" + PF + ",esi=" +
                            ESI + ",Pt=" + PT + " Where   desgn='" + oldDesig + "'  and empid='" +
                            empid + "' and  transfertype=" + transfertype + "  and tounitid='" + unitid + "'";
                      int re=config.ExecuteNonQueryWithQueryAsync(strQry).Result;

                        ScriptManager.RegisterStartupScript(this, GetType(), "showlalert",
                            "alert('Employee already working for this Client.PF/ESI/PT Details Are updated for the selected Client. " +
                            " If  You Want To Transfer This Employee Please Change The Designation');", true);
                        TrnasferedEmployeesBindData();
                        return;
                    }
                    else
                    {
                        strQry = "Select * from EmpPostingOrder where empid='" + empid + "' AND tounitid='" + unitid + "'  and transfertype=" + transfertype;
                        DataTable DtCheckEmpTransfer = config.ExecuteAdaptorAsyncWithQueryParams(strQry).Result;
                        if (DtCheckEmpTransfer.Rows.Count > 0)
                        {
                            strQry = "Delete from EmpPostingOrder where empid='" + empid + "' AND tounitid='" + unitid + "'  and transfertype=" + transfertype;
                            DataTable del = config.ExecuteAdaptorAsyncWithQueryParams(strQry).Result;

                            strQry = string.Format("insert into  EmpPostingOrder(orderid,empid,tounitid,desgn,orderdt,joiningdt, " +
                                " relievedt,TransferType,IssuedAuthority,Remarks,PrevUnitId,pf,esi,pt) values('{0}','{1}','{2}','{3}','{4}','{5}'," +
                                " '{6}',{7},'{8}','{9}','{10}','{11}','{12}','{13}')", orderid, empid, unitid, designation, orddate,
                                joindate, relivedate, transfertype,
                                "OpM000", remarks, "NA", PF, ESI, PT);
                           DataTable dtin= config.ExecuteAdaptorAsyncWithQueryParams(strQry).Result;
                            ScriptManager.RegisterStartupScript(this, GetType(), "showlalert", "alert('Employee transfered Sucessfully ');", true);
                            TrnasferedEmployeesBindData();
                            return;
                        }
                        else
                        {

                            strQry = string.Format("insert into  EmpPostingOrder(orderid,empid,tounitid,desgn,orderdt,joiningdt, " +
                                " relievedt,TransferType,IssuedAuthority,Remarks,PrevUnitId,pf,esi,pt) values('{0}','{1}','{2}','{3}','{4}','{5}'," +
                                " '{6}',{7},'{8}','{9}','{10}','{11}','{12}','{13}')",
                                orderid, empid, unitid, designation, orddate, joindate, relivedate, transfertype,
                                "OpM000", remarks, "NA", PF, ESI, PT);
                           int dtaas=config.ExecuteNonQueryWithQueryAsync(strQry).Result;
                            ScriptManager.RegisterStartupScript(this, GetType(), "showlalert", "alert('Employee transfered Sucessfully ');", true);
                            TrnasferedEmployeesBindData();
                        }
                        LoadEmployeeTransfertype();
                        return;
                    }
                }
                else
                {
                    strQry = string.Format("insert into  EmpPostingOrder(orderid,empid,tounitid,desgn,orderdt,joiningdt, " +
                                 " relievedt,TransferType,IssuedAuthority,Remarks,PrevUnitId,pf,esi,pt) values('{0}','{1}','{2}','{3}','{4}','{5}'," +
                                 " '{6}',{7},'{8}','{9}','{10}','{11}','{12}','{13}')",
                                 orderid, empid, unitid, designation, orddate, joindate, relivedate, transfertype,
                                 "OpM000", remarks, "NA", PF, ESI, PT);
                  int hyd=config.ExecuteNonQueryWithQueryAsync(strQry).Result;
                    ScriptManager.RegisterStartupScript(this, GetType(), "showlalert", "alert('Employee transfered Sucessfully ');", true);
                    TrnasferedEmployeesBindData();
                    LoadEmployeeTransfertype();
                }

                #endregion
            }
            catch (Exception ex)
            {
            }
        }

        protected void PreviligeUsers(int previligerid)
        {
            switch (previligerid)
            {
                case 1:
                    break;
                case 2:
                    //AddEmployeeLink.Visible = true;
                    //ModifyEmployeeLink.Visible = false;
                    //DeleteEmployeeLink.Visible = false;
                    //AssigningWorkerLink.Visible = false;
                    //AttendanceLink.Visible = false;
                    //LoanLink.Visible = false;
                    //PaymentLink.Visible = false;
                    ////TrainingEmployeeLink.Visible = false;
                    ////JobLeavingReasonsLink.Visible = false;
                    InventoryLink.Visible = false;
                    CompanyInfoLink.Visible = false;
                    ReportsLink.Visible = false;
                    SettingsLink.Visible = false;
                    break;

                case 3:

                    //AddEmployeeLink.Visible = false;
                    //ModifyEmployeeLink.Visible = false;
                    //DeleteEmployeeLink.Visible = false;
                    //AssigningWorkerLink.Visible = false;
                    //AttendanceLink.Visible = true;
                    //LoanLink.Visible = true;
                    //PaymentLink.Visible = true;
                    ////TrainingEmployeeLink.Visible = false;
                    //PostingOrderListLink.Visible = true;
                    ////JobLeavingReasonsLink.Visible = false;

                    ClientsLink.Visible = true;
                    CompanyInfoLink.Visible = false;
                    InventoryLink.Visible = false;
                    ReportsLink.Visible = true;
                    SettingsLink.Visible = true;

                    PostingOrderLink.Visible = false;
                    TempTransferLink.Visible = false;
                    break;

                case 4:
                    //AddEmployeeLink.Visible = false;
                    //ModifyEmployeeLink.Visible = false;
                    //DeleteEmployeeLink.Visible = false;
                    //AssigningWorkerLink.Visible = false;
                    //AttendanceLink.Visible = false;
                    //LoanLink.Visible = false;
                    //PaymentLink.Visible = false;
                    ////TrainingEmployeeLink.Visible = false;
                    //PostingOrderListLink.Visible = true;
                    ////JobLeavingReasonsLink.Visible = false;

                    ClientsLink.Visible = true;
                    CompanyInfoLink.Visible = true;
                    InventoryLink.Visible = true;
                    ReportsLink.Visible = true;
                    SettingsLink.Visible = true;
                    break;
                case 5:
                    //AddEmployeeLink.Visible = false;
                    //ModifyEmployeeLink.Visible = false;
                    //DeleteEmployeeLink.Visible = false;
                    //AssigningWorkerLink.Visible = false;
                    //LoanLink.Visible = false;
                    //PaymentLink.Visible = false;
                    ////TrainingEmployeeLink.Visible = true;
                    ////JobLeavingReasonsLink.Visible = false;
                    //AttendanceLink.Visible = true;

                    EmployeesLink.Visible = true;
                    ClientsLink.Visible = true;
                    CompanyInfoLink.Visible = false;
                    InventoryLink.Visible = false;
                    ReportsLink.Visible = true;
                    ReportsLink.Visible = true;
                    SettingsLink.Visible = true;

                    PostingOrderLink.Visible = false;
                    break;
                case 6:
                    //AddEmployeeLink.Visible = false;
                    //ModifyEmployeeLink.Visible = false;
                    //DeleteEmployeeLink.Visible = false;
                    //AssigningWorkerLink.Visible = false;
                    //AttendanceLink.Visible = true;
                    //LoanLink.Visible = true;
                    //PaymentLink.Visible = true;
                    ////TrainingEmployeeLink.Visible = false;
                    ////JobLeavingReasonsLink.Visible = false;

                    InventoryLink.Visible = false;
                    CompanyInfoLink.Visible = false;
                    ReportsLink.Visible = false;
                    SettingsLink.Visible = false;
                    break;
                default:
                    break;
            }
        }

        protected void ClearData()
        {
            txtorderdate.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
            txtjoindate.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
            txtremarks.Text = "";
            gvemppostingorder.DataSource = null;
            gvemppostingorder.DataBind();
            ddlUnit.SelectedIndex = 0;
            ddlcname.SelectedIndex = 0;
            ddlempid.SelectedIndex = 0;
            ddlempname.SelectedIndex = 0;
            ddlDesignation.SelectedIndex = 0;
        }

        protected void ddlempid_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddlempid.SelectedIndex > 0)
            {
                GetEmpName();
            }
            else
            {
                ddlempname.SelectedIndex = 0;
                //MessageLabel.Text = "Please Select Emp Id";
                //return;
            }
        }

        protected void ddlUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlUnit.SelectedIndex > 0)
            {
                Fillcname();
                TrnasferedEmployeesBindData();
            }
            else
            {
                ClearData();
            }
        }


        protected void ddlcname_OnSelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddlcname.SelectedIndex > 0)
            {
                FillClientid();
                TrnasferedEmployeesBindData();
            }
            else
            {
                ClearData();
            }
        }

        protected void LoadEmpIds()
        {
            DataTable DtEmpIds = GlobalData.Instance.LoadEmpIds(EmpIDPrefix,BranchID);
            if (DtEmpIds.Rows.Count > 0)
            {
                ddlempid.DataValueField = "empid";
                ddlempid.DataTextField = "empid";
                ddlempid.DataSource = DtEmpIds;
                ddlempid.DataBind();
            }
            ddlempid.Items.Insert(0, "-Select-");
        }


        protected void LoadNames()
        {
            DataTable DtEmpNames = GlobalData.Instance.LoadEmpNames(EmpIDPrefix,BranchID);
            if (DtEmpNames.Rows.Count > 0)
            {
                ddlempname.DataValueField = "empid";
                ddlempname.DataTextField = "FullName";
                ddlempname.DataSource = DtEmpNames;
                ddlempname.DataBind();
            }
            ddlempname.Items.Insert(0, "-Select-");
        }

        protected void GetEmpName()
        {


            #region   //Begin Old code
            string Sqlqry = "select empid,EmpDesgn from empdetails  where empid='" + ddlempid.SelectedValue + "'";
            DataTable dt = config.ExecuteAdaptorAsyncWithQueryParams(Sqlqry).Result;
            if (dt.Rows.Count > 0)
            {
                try
                {
                    ddlempname.SelectedValue = dt.Rows[0]["empid"].ToString();
                    if (dt.Rows[0]["EmpDesgn"].ToString() == "0")
                    {
                        ddlDesignation.SelectedIndex = 0;
                    }
                    else
                    {
                        ddlDesignation.SelectedValue = dt.Rows[0]["EmpDesgn"].ToString();
                    }

                }
                catch (Exception ex)
                {
                    MessageLabel.Text = ex.Message;
                }
            }
            else
            {
                MessageLabel.Text = "There Is No Name For The Selected Employee";
            }

            #endregion  //End Old code
            //if (ddlempid.SelectedIndex > 0)
            //{
            //    ddlempname.SelectedValue = ddlempid.SelectedValue;
            //}
            //else
            //{
            //    ddlempname.SelectedIndex = 0;
            //}

        }

        protected void GetEmpid()
        {

            #region  //Begin Old Code
            string Sqlqry = "select Empid,EmpDesgn from empdetails  where Empid='" + ddlempname.SelectedValue + "'";
            DataTable dt = config.ExecuteAdaptorAsyncWithQueryParams(Sqlqry).Result;
            if (dt.Rows.Count > 0)
            {
                try
                {
                    ddlempid.SelectedValue = dt.Rows[0]["Empid"].ToString();
                    if (dt.Rows[0]["EmpDesgn"].ToString() == "0")
                    {
                        ddlDesignation.SelectedIndex = 0;
                    }
                    else
                    {
                        ddlDesignation.SelectedValue = dt.Rows[0]["EmpDesgn"].ToString();
                    }

                }
                catch (Exception ex)
                {
                    MessageLabel.Text = ex.Message;
                }
            }
            else
            {
                MessageLabel.Text = "There Is No Name For The Selected Employee";
            }

            #endregion  //End  Old code
            //if (ddlempname.SelectedIndex > 0)
            //{
            //    ddlempid.SelectedValue = ddlempname.SelectedValue;
            //}
            //else
            //{
            //    ddlempid.SelectedIndex = 0;
            //}
        }

        protected void ddlempname_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlempname.SelectedIndex > 0)
            {
                GetEmpid();
            }
            else
            {
                ddlempid.SelectedIndex = 0;
            }
        }
    }
}