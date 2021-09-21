using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Globalization;
using System.Data.SqlClient;
using System.IO;
using System.Data.OleDb;
using KLTS.Data;
using Jawan.Portal.DAL;
namespace Jawan.Portal
{
    public partial class ImportEmpAttendance : System.Web.UI.Page
    {
        AppConfiguration config = new AppConfiguration();
        GridViewExportUtil gve = new GridViewExportUtil();
        int oderid = 0;
        string EmpIDPrefix = "";
        string CmpIDPrefix = "";
        string BranchID = "";

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
                        lblcname.Text = SqlHelper.Instance.GetCompanyname();
                        switch (SqlHelper.Instance.GetCompanyValue())
                        {
                            case 0:// Write Frames Invisible Links
                                break;
                            case 1://Write KLTS Invisible Links
                                // ReceiptsLink.Visible = true;
                                break;
                            default:
                                break;
                        }

                    }
                    else
                    {
                        Response.Redirect("login.aspx");
                    }

                    FillClientNameList();
                    FillClientList();
                    FillMonthList();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Show alert", "alert('Your Session Expired');", true);
                Response.Redirect("~/Login.aspx");
            }
        }

        protected void GetWebConfigdata()
        {
            EmpIDPrefix = Session["EmpIDPrefix"].ToString();
            CmpIDPrefix = Session["CmpIDPrefix"].ToString();
            BranchID = Session["BranchID"].ToString();
        }

        protected void FillClientList()
        {
            DataTable dt = GlobalData.Instance.LoadCIds(CmpIDPrefix,BranchID);
            int rowno1 = 0;
            //ddlClientID.Items.Add("--Select--");

            if (dt.Rows.Count > 0)
            {
                ddlClientID.DataValueField = "clientid";
                ddlClientID.DataTextField = "clientid";
                ddlClientID.DataSource = dt;
                ddlClientID.DataBind();
            }
            ddlClientID.Items.Insert(0, "--Select--");

        }

        protected void FillClientNameList()
        {
            // string selectclientid = "select ClientName from clients   where  clientid like '" + CmpIDPrefix + "%'";

            DataTable dt = GlobalData.Instance.LoadCNames(CmpIDPrefix,BranchID);
            if (dt.Rows.Count > 0)
            {
                ddlCName.DataValueField = "clientid";
                ddlCName.DataTextField = "Clientname";
                ddlCName.DataSource = dt;
                ddlCName.DataBind();
            }
            ddlCName.Items.Insert(0, "--Select--");

        }

        protected void FillMonthList()
        {
            //month
            var formatInfoinfo = new DateTimeFormatInfo();
            string[] monthName = formatInfoinfo.MonthNames;
            string month = monthName[DateTime.Now.Month - 1];
            string LastMonth = "";
            ddlMonth.Items.Add(month);
            try
            {
                month = monthName[DateTime.Now.Month - 2];
            }
            catch (IndexOutOfRangeException ex)
            {
                month = monthName[11];
            }
            try
            {
                LastMonth = monthName[DateTime.Now.Month - 3];
            }
            catch (IndexOutOfRangeException ex)
            {
                LastMonth = monthName[12 - (3 - DateTime.Now.Month)];
            }

            ddlMonth.Items.Add(month);
            ddlMonth.Items.Add(LastMonth);

            ddlMonth.Items.Insert(0, "--Select--");

        }


        private void displaydata()
        {
            ddlMonth.SelectedIndex = 0;
            GridView1.DataSource = null;
            GridView1.DataBind();

            if (ddlClientID.SelectedIndex > 0)
            {
                string selectclientdata = "select Clientid,clientname,clientphonenumbers,OurContactPersonId " +
                    " from clients Where Clientid='" + ddlClientID.SelectedValue + "'";
                DataTable dtdata =config.ExecuteAdaptorAsyncWithQueryParams(selectclientdata).Result;
                if (dtdata.Rows.Count > 0)
                {
                    ddlCName.SelectedValue = dtdata.Rows[0]["Clientid"].ToString();
                }
                else
                {
                    ddlCName.SelectedIndex = 0;
                }
                /*** Getting list of employees working for this client for this month*/
            }
            else
            {

            }
        }

        protected void FillAttendanceGrid()
        {


            if (ddlClientID.SelectedIndex > 0)
            {
                // int month = 0, year = 2000;
                // GlobalData.Instance.GetMonthAndYear(ddlMonth.SelectedValue, ddlMonth.SelectedIndex, out month, out year);
                DataTable data = new DataTable();
                // if (radioindividual.Checked)
                {

                    #region Begin  Variable declaration
                    var ClientID = "";
                    var Month = 0;
                    var LastDay = DateTime.Now.Date;
                    Hashtable HTEPAttendance = new Hashtable();
                    var SPName = "";
                    DataTable DTEPAttendance = new DataTable();
                    #endregion End  Variable declaration

                    #region Begin  Assign Values To Variable
                    ClientID = ddlClientID.SelectedValue;
                    //Month = Timings.Instance.GetIdForSelectedMonth(ddlMonth.SelectedIndex);
                    Month = GetMonthBasedOnSelectionDateorMonth();
                    //if (Chk_Month.Checked == false)
                    {
                        LastDay = Timings.Instance.GetLastDayForSelectedMonth(ddlMonth.SelectedIndex);
                    }
                    //if (Chk_Month.Checked == true)
                    {
                        //  LastDay = DateTime.Parse(Txt_Month.Text, CultureInfo.GetCultureInfo("en-gb"));
                    }
                    SPName = "IMPaysheetattendance";
                    #endregion End Assign Values To Variable

                    #region Begin Assign Values To Hash Table
                    HTEPAttendance.Add("@Clientid", ClientID);
                    HTEPAttendance.Add("@Month", Month);
                    HTEPAttendance.Add("@LastDay", LastDay);
                    #endregion End Assign Values To Hash Table

                    #region Begin Calling Stored Procedure
                    DTEPAttendance =config.ExecuteAdaptorAsyncWithParams(SPName, HTEPAttendance).Result;
                    #endregion End Calling Stored Procedure
                    if (DTEPAttendance.Rows.Count > 0)
                    {
                        GridView1.DataSource = DTEPAttendance;
                        GridView1.DataBind();

                    }
                    else
                    {
                        GridView1.DataSource = null;
                        GridView1.DataBind();
                        ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Attendance Not Avaialable for  this month of the Selected client');", true);
                    }
                }

            }
        }


        private void displaydataFormClientName()
        {
            if (ddlCName.SelectedIndex > 0)
            {
                int month = 0, year = 2000;
                GlobalData.Instance.GetMonthAndYear(ddlMonth.SelectedValue, ddlMonth.SelectedIndex, out month, out year);

                string selectclientdata = "select  Clientid,clientphonenumbers,OurContactPersonId from clients Where Clientid='" + ddlCName.SelectedValue + "'";
                DataTable dtdata = config.ExecuteAdaptorAsyncWithQueryParams(selectclientdata).Result;
                if (dtdata.Rows.Count > 0)
                {
                    ddlClientID.SelectedValue = dtdata.Rows[0]["ClientId"].ToString();
                }
                else
                {
                    ddlCName.SelectedIndex = 0;
                }
                /*** Getting list of employees working for this client for this month*/
                FillAttendanceGrid();
            }
            else
            {
            }
        }

        protected void ddlClientID_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlClientID.SelectedIndex > 0)
                {
                    displaydata();
                    //BindData();
                }
                else
                {
                    ClearData();
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "IsPostBack", "var isPostBack = false;", true);
            }
            lblTotalDuties.Text = string.Empty;
            lblTotalOts.Text = string.Empty;
            if (ddlMonth.SelectedIndex > 0)
            {
                FillAttendanceGrid();
            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();

            }
        }


        protected string GetEmpDesignation(string empId)
        {
            string desig = null;

            string sqlQry = "Select EmpDesgn from EmpDetails where EmpId='" + empId + "'";
            DataTable data = config.ExecuteAdaptorAsyncWithQueryParams(sqlQry).Result;
            if (data.Rows.Count > 0)
            {
                desig = data.Rows[0][0].ToString();
            }
            return desig;
        }

        protected int GetEmpDutyType(string empdesign)
        {
            int type = -1;

            string sqlQry = "Select DutyType from Designations where Design='" + empdesign + "'";
            DataTable data = config.ExecuteAdaptorAsyncWithQueryParams(sqlQry).Result;
            if (data.Rows.Count > 0)
            {
                bool str = Convert.ToBoolean(data.Rows[0][0].ToString());

                type = Convert.ToInt32(str);
            }
            return type;
        }

        protected string GetEmpName(string empId)
        {
            string name = null;

            string sqlQry = "Select EmpFName,EmpMName from EmpDetails where EmpId='" + empId + "'";
            DataTable data = config.ExecuteAdaptorAsyncWithQueryParams(sqlQry).Result;
            if (data.Rows.Count > 0)
            {
                name = data.Rows[0]["EmpFName"].ToString() + " " + data.Rows[0]["EmpMName"].ToString();
            }
            return name;
        }

        protected void btn_Save_AttenDanceClick(object sender, EventArgs e)
        {
            if (ddlClientID.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showlalert", "alert('Please Select Client Id');", true);
                return;
            }
            // int month = GlobalData.Instance.GetMonth(ddlMonth.SelectedIndex);
            int month = GetMonthBasedOnSelectionDateorMonth();
            var LastDate = DateTime.Now.Date;

            // if (Chk_Month.Checked == false)
            {
                LastDate = Timings.Instance.GetLastDayForSelectedMonth(ddlMonth.SelectedIndex);
            }
            //if (Chk_Month.Checked == true)
            {
                //  LastDate = DateTime.Parse(Txt_Month.Text, CultureInfo.GetCultureInfo("en-gb"));
            }


            var ContractID = "";
            Hashtable HtGetContractID = new Hashtable();
            var SPNameForGetContractID = "GetContractIDBasedOnthMonth";
            HtGetContractID.Add("@clientid", ddlClientID.SelectedValue);
            HtGetContractID.Add("@LastDay", LastDate);
            DataTable DTContractID = config.ExecuteAdaptorAsyncWithParams(SPNameForGetContractID, HtGetContractID).Result;

            if (DTContractID.Rows.Count > 0)
            {
                ContractID = DTContractID.Rows[0]["contractid"].ToString();
            }
            else
            {
                return;
            }


            string totaldesignationlist = string.Empty;
            // if (radioindividual.Checked)
            {
                float totalDuties = 0;
                float totalOTs = 0;
                int statusatte = 0;
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    Label lblEmpId = GridView1.Rows[i].FindControl("lblEmpid") as Label;
                    Label lbldesign = GridView1.Rows[i].FindControl("lblDesg") as Label;
                    TextBox txtDuties = GridView1.Rows[i].FindControl("txtDuties") as TextBox;
                    TextBox txtOTs = GridView1.Rows[i].FindControl("txtOTs") as TextBox;
                    TextBox txtPenalty = GridView1.Rows[i].FindControl("txtPenalty") as TextBox;
                    TextBox txtCanteenAdv = GridView1.Rows[i].FindControl("txtCanAdv") as TextBox;
                    TextBox txtIncentivs = GridView1.Rows[i].FindControl("txtIncentivs") as TextBox;


                    TextBox txtwos = GridView1.Rows[i].FindControl("txtwos") as TextBox;
                    TextBox txtnhs = GridView1.Rows[i].FindControl("txtnhs") as TextBox;
                    TextBox txtnpots = GridView1.Rows[i].FindControl("txtnpots") as TextBox;


                    float ots = 0;
                    float duties = 0;
                    float penalty = 0;
                    float CanteenAdv = 0;
                    float Incentivs = 0;

                    float Wos = 0;
                    float Nhs = 0;
                    float Npots = 0;


                    if (txtOTs.Text.Trim().Length > 0)
                    {
                        if (ddlOTType.SelectedIndex == 0)
                        {
                            ots = Convert.ToSingle(txtOTs.Text);
                        }
                        else
                        {
                            ots = Convert.ToSingle(txtOTs.Text) / (float)8;
                        }
                    }
                    if (txtDuties.Text.Trim().Length > 0)
                    {
                        duties = Convert.ToSingle(txtDuties.Text);
                    }
                    if (txtPenalty.Text.Trim().Length > 0)
                    {
                        penalty = Convert.ToSingle(txtPenalty.Text);
                    }
                    if (txtCanteenAdv.Text.Trim().Length > 0)
                    {
                        CanteenAdv = Convert.ToSingle(txtCanteenAdv.Text);
                    }

                    if (txtIncentivs.Text.Trim().Length > 0)
                    {
                        Incentivs = Convert.ToSingle(txtIncentivs.Text);
                    }

                    if (txtwos.Text.Trim().Length > 0)
                    {
                        Wos = Convert.ToSingle(txtwos.Text);
                    }

                    if (txtnhs.Text.Trim().Length > 0)
                    {
                        Nhs = Convert.ToSingle(txtnhs.Text);
                    }

                    if (txtnpots.Text.Trim().Length > 0)
                    {
                        Npots = Convert.ToSingle(txtnpots.Text);
                    }
                    totalDuties += duties;
                    totalOTs += ots;


                    string sqlqry = "Select * from  Empattendance  Where Empid='" + lblEmpId.Text + "' and month='" + month +
                        "'  and ClientId='" + ddlClientID.SelectedValue + "'    and  Design='" + lbldesign.Text + "'  and   contractid='" + ContractID + "'";

                    DataTable dtattestemp = config.ExecuteAdaptorAsyncWithQueryParams(sqlqry).Result;
                    int Status = 0;
                    if (dtattestemp.Rows.Count > 0)
                    {
                        string design = dtattestemp.Rows[0]["design"].ToString();
                        string updatequery = string.Format("update EmpAttendance set NoofDuties={0},OT={1},Penalty={2}," +
                            " CanteenAdv={6},Incentivs={7},Design='{8}',WO='{9}',NHS='{10}',NPOTS='{11}' " +
                                                    " Where empid='{3}' And ClientId='{4}' And Month={5}  and  Design='" + lbldesign.Text +
                                                    "'  and contractid='" + ContractID + "'",
                                                     duties, ots, penalty, lblEmpId.Text, ddlClientID.SelectedValue,
                                                     month, CanteenAdv, Incentivs, lbldesign.Text, Wos, Nhs, Npots);
                        Status =config.ExecuteNonQueryWithQueryAsync(updatequery).Result;
                    }
                    else
                    {

                        string insertquery = string.Format("insert  EmpAttendance(NoofDuties,OT,Penalty,CanteenAdv,Design," +
                            "WO,NHS,NPOTS,empid,clientid,month,Incentivs,contractid)" +
                            " values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}' ) ",

                                                  duties, ots, penalty, CanteenAdv, lbldesign.Text, Wos, Nhs, Npots,
                                                  lblEmpId.Text, ddlClientID.SelectedValue, month,
                                                  Incentivs, ContractID);
                        Status =config.ExecuteNonQueryWithQueryAsync(insertquery).Result;

                    }

                    if (Status != 0)
                    {
                        if (statusatte == 0)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "showlalert", "alert('Attendance Added Successfully');", true);
                            statusatte = 1;
                        }

                    }
                }

            }

        }

        protected void Btn_Cancel_AttenDance_Click(object sender, EventArgs e)
        {
            ClearData();
        }

        private void ClearData()
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
            ddlClientID.SelectedIndex = 0;
            ddlCName.SelectedIndex = 0;
            ddlMonth.SelectedIndex = 0;
            ddlOTType.SelectedIndex = 0;
            lblTotalDuties.Text = "";
            lblTotalOts.Text = "";

        }

        protected void ddlCName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlCName.SelectedIndex > 0)
                {
                    displaydataFormClientName();
                }
                else
                {
                    ClearData();
                }
            }
            catch (Exception ex)
            {

            }
        }


        protected void txtDutiesInHours_textChanged(object sender, EventArgs e)
        {
            if (sender != null)
            {
                GridViewRow row = ((GridViewRow)((TextBox)sender).NamingContainer);
                if (row != null)
                {
                    TextBox duties = (TextBox)row.FindControl("txtDuties");
                    if (duties != null)
                    {
                        TextBox dutyHours = (TextBox)sender;
                        if (dutyHours != null)
                        {
                            string hours = dutyHours.Text;
                            if (hours.Trim().Length > 0)
                            {
                                float dHours = Convert.ToSingle(hours);
                                //duties.Text = (dHours / 8).ToString("0.00");
                                duties.Text = (dHours).ToString("0.00");

                            }
                        }
                    }
                }
            }
        }

        public string GetExcelSheetNames()
        {
            string ExcelSheetname = "";
            OleDbConnection con = null;
            DataTable dt = null;
            string filename = Path.Combine(Server.MapPath("~/ImportDocuments"), Guid.NewGuid().ToString() + Path.GetExtension(fileupload1.PostedFile.FileName));
            fileupload1.PostedFile.SaveAs(filename);
            string extn = Path.GetExtension(fileupload1.PostedFile.FileName);
            string conStr = string.Empty;
            if (extn.ToLower() == ".xls")
            {
                conStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filename + ";Extended properties=\"excel 8.0;HDR=Yes;IMEX=2\"";
            }
            else if (extn.ToLower() == ".xlsx")
            {
                conStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filename + ";Extended properties=\"excel 12.0;HDR=Yes;IMEX=2\"";
            }

            con = new OleDbConnection(conStr);
            con.Open();
            dt = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

            if (dt == null)
            {
                return null;
            }
            ExcelSheetname = dt.Rows[0]["TABLE_NAME"].ToString();
            ////foreach (DataRow row in dt.Rows)
            ////{
            ////    ExcelSheetname = row["TABLE_NAME"].ToString();
            ////}

            return ExcelSheetname;
        }

        protected void btnImport_Click(object sender, EventArgs e)
        {
            int month = 0;
            btnExport.Visible = false;


            if (ddlClientID.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Show alert", "alert('Please Select Client Id');", true);
                return;
            }
            if (ddlMonth.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Show alert", "alert('Please Select Month');", true);
                return;
            }
            month = Timings.Instance.GetIdForSelectedMonth(ddlMonth.SelectedIndex);

            try
            {
                string filename = Path.Combine(Server.MapPath("ImportDocuments"), Guid.NewGuid().ToString() + Path.GetExtension(fileupload1.PostedFile.FileName));
                fileupload1.PostedFile.SaveAs(filename);
                string extn = Path.GetExtension(fileupload1.PostedFile.FileName);
                string constring = "";
                if (extn.ToLower() == ".xls")
                {
                    constring = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filename + ";Extended properties=\"excel 12.0;HDR=Yes;IMEX=2\"";
                }
                else if (extn.ToLower() == ".xlsx")
                {
                    constring = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filename + ";Extended properties=\"excel 12.0;HDR=Yes;IMEX=2\"";
                }

                string Sheetname = string.Empty;

                string qry = "select [Client Id],[Emp Id],[Designation],[Duties],[OTs],[WOs],[Canteen Advance],[Penalty]," +
                " [Incentives],[NHs] from  [" + GetExcelSheetNames() + "]" + "";
                OleDbConnection con = new OleDbConnection(constring);
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                OleDbCommand cmd = new OleDbCommand(qry, con);
                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                da.Dispose();
                con.Close();
                con.Dispose();


                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    string Remark = string.Empty;


                    #region Variables for Excel Values

                    string empid = string.Empty;
                    string clientid = string.Empty;
                    string design = string.Empty;
                    string Month = string.Empty;
                    float duties = 0;
                    float ots = 0;
                    float penalty = 0;
                    float incentives = 0;
                    float canteenadvance = 0;
                    float Wos = 0;
                    float Nhs = 0;
                    float Npots = 0;

                    #endregion

                    #region Variables for Posting order Table data and EmpAttendance(Default Values)

                    int orderid = 0;

                    string PrevUnitid = string.Empty;
                    string Dutyhrs = string.Empty;
                    DateTime Orderdate = DateTime.Now;
                    DateTime Joiningdate = DateTime.Now;
                    DateTime Releivingdate = DateTime.Now;
                    string IssuedAuthority = string.Empty;
                    string Remarks = string.Empty;
                    int TransferType = 1;

                    string AttString = string.Empty;
                    string HrsString = string.Empty;
                    float TotalHours = 0;
                    float OTHours = 0;
                    float NHDays = 0;
                    float CL = 0;
                    float PL = 0;
                    float UL = 0;

                    #endregion



                    #region Values Assign from excel

                    clientid = dr["Client Id"].ToString();

                    if (clientid == ddlClientID.SelectedValue)
                    {


                        empid = dr["Emp Id"].ToString();
                        if (empid.Length > 0)
                        {
                            if (empid.Length == 1)
                            {
                                empid = "00000" + empid;
                            }
                            if (empid.Length == 2)
                            {
                                empid = "0000" + empid;
                            }
                            if (empid.Length == 3)
                            {
                                empid = "000" + empid;
                            }
                            if (empid.Length == 4)
                            {
                                empid = "00" + empid;
                            }
                            if (empid.Length == 5)
                            {
                                empid = "0" + empid;
                            }

                            design = dr["Designation"].ToString();
                            // Month = dr["Month"].ToString();

                            duties = float.Parse(dr["Duties"].ToString());
                            if (String.IsNullOrEmpty(dr["OTs"].ToString()) == false)
                            {
                                ots = float.Parse(dr["OTs"].ToString());
                            }
                            if (String.IsNullOrEmpty(dr["Wos"].ToString()) == false)
                            {
                                Wos = float.Parse(dr["WOs"].ToString());
                            }
                            if (String.IsNullOrEmpty(dr["Canteen Advance"].ToString()) == false)
                            {
                                canteenadvance = float.Parse(dr["Canteen Advance"].ToString());
                            }
                            if (String.IsNullOrEmpty(dr["Penalty"].ToString()) == false)
                            {
                                penalty = float.Parse(dr["Penalty"].ToString());
                            }
                            if (String.IsNullOrEmpty(dr["Incentives"].ToString()) == false)
                            {
                                incentives = float.Parse(dr["Incentives"].ToString());
                            }
                            if (String.IsNullOrEmpty(dr["NHs"].ToString()) == false)
                            {
                                Nhs = float.Parse(dr["NHs"].ToString());
                            }
                            //if (String.IsNullOrEmpty(dr["NPOTs"].ToString()) == false)
                            //{
                            //    Npots = float.Parse(dr["NPOTs"].ToString());
                            //}


                    #endregion


                            #region Check data for Designation is matching or not

                            string sqldesign = "select Design from Designations where Design='" + design + "'";
                            DataTable dtdesign = config.ExecuteAdaptorAsyncWithQueryParams(sqldesign).Result;

                            #endregion

                            if (dtdesign.Rows.Count == 0)
                            {

                                string selNotinsertdata = "select * from Notinsertdata where  clientid='" + ddlClientID.SelectedValue + "'" +
                                    " and month='" + month + "' and empid='" + empid + "'";
                                DataTable dtNotinsert = config.ExecuteAdaptorAsyncWithQueryParams(selNotinsertdata).Result;
                                if (dtNotinsert.Rows.Count > 0)
                                {

                                    string DeleteQuery = "Delete from Notinsertdata where clientid ='" + ddlClientID.SelectedValue + "'" +
                                        " and month ='" + month + "' and empid='" + empid + "'";
                                int del=config.ExecuteNonQueryWithQueryAsync(DeleteQuery).Result;
                                }
                            }
                            if (dtdesign.Rows.Count != 0)
                            {
                                int deletestatus = 0;

                                string selNotinsertdata = "select * from Notinsertdata where  clientid='" + ddlClientID.SelectedValue + "'" +
                                  " and month='" + month + "' and empid='" + empid + "' and design='" + design + "'";
                                DataTable dtNotinsert = config.ExecuteAdaptorAsyncWithQueryParams(selNotinsertdata).Result;
                                if (dtNotinsert.Rows.Count > 0)
                                {

                                    string DeleteQuery = "Delete from Notinsertdata where clientid ='" + ddlClientID.SelectedValue + "'" +
                                        " and month ='" + month + "' and empid='" + empid + "'";
                                    deletestatus = config.ExecuteNonQueryWithQueryAsync(DeleteQuery).Result;
                                }

                            }


                            #region When check designation

                            int insertsate = 0;
                            if (dtdesign.Rows.Count > 0 && empid.Length > 0)
                            {


                                #region Check code for data is available in Posting order table or not

                                string sqlPOcheck = "select * from EmpPostingOrder where Tounitid='" + ddlClientID.SelectedValue + "' and " +
                                    " empid='" + empid + "' and Desgn='" + design + "'";
                                DataTable dtPostingorder = config.ExecuteAdaptorAsyncWithQueryParams(sqlPOcheck).Result;

                                int PoStatus = 0;

                                if (dtPostingorder.Rows.Count == 0)
                                {
                                    string insertPOrder = string.Format("insert into EmpPostingOrder(OrderId,EmpId,PrevUnitId,ToUnitId," +
                                        "Desgn,DutyHrs,OrderDt,JoiningDt,RelieveDt,IssuedAuthority,Remarks,TransferType) values('{0}'," +
                                        "'{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}')", orderid, empid, PrevUnitid,
                                        ddlClientID.SelectedValue, design, Dutyhrs, Orderdate, Joiningdate, Releivingdate, IssuedAuthority,
                                        Remarks, TransferType);
                                    PoStatus = config.ExecuteNonQueryWithQueryAsync(insertPOrder).Result;

                                }
                                if (dtPostingorder.Rows.Count != 0)
                                {
                                    PoStatus = 1;
                                }

                                #endregion

                                int AttStatus = 0;

                                if (PoStatus > 0)
                                {
                                    string sqlempid = "Select * from  Empattendance  Where Empid='" + empid + "' and month='" + month +
                                        "'  and ClientId='" + ddlClientID.SelectedValue + "'    and  Design='" + design + "'";
                                    DataTable dtempid = config.ExecuteAdaptorAsyncWithQueryParams(sqlempid).Result;
                                    if (dtempid.Rows.Count != 0)
                                    {
                                        string updatedata = "update Empattendance set  NoofDuties= '" + duties + "',OT= '" + ots + "'," +
                                            " Penalty='" + penalty + "',CanteenAdv ='" + canteenadvance + "',Incentivs= '" + incentives + "'" +
                                          " where empid='" + empid + "' and ClientId='" + ddlClientID.SelectedValue + "' and Month='" + month + "' and Design='" + design + "'";
                                        AttStatus = config.ExecuteNonQueryWithQueryAsync(updatedata).Result;
                                    }

                                    if (dtempid.Rows.Count == 0)
                                    {
                                        string inserEmpatten = string.Format("insert into EmpAttendance(Month,EmpId,ClientId,Design," +
                                            " DutyHrs,NoOfDuties,OT,WO,CanteenAdv,AttString,HrsString,TotalHours,OTHours,NHDays,CL," +
                                            " PL,Penalty,UL,Incentivs) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}'," +
                                            "'{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}')", month, empid,
                                            ddlClientID.SelectedValue, design, Dutyhrs, duties, ots, Wos, canteenadvance, AttString,
                                            HrsString, TotalHours, OTHours, NHDays, CL, PL, penalty, UL, incentives);
                                        AttStatus =config.ExecuteNonQueryWithQueryAsync(inserEmpatten).Result;
                                    }
                                }
                                if (AttStatus > 0)
                                {
                                    lblMessage.Text = "Record Added successfull";
                                }
                            }
                            #endregion

                            #region When check empid

                            else if (empid.Length == 0)
                            {
                                Remark = "Empid is Not Entered";

                                string sqlinsert = string.Format("insert into Notinsertdata(Month,EmpId,ClientId,Design,NoOfDuties," +
                                                        "OT,Penalty,CanteenAdv,Incentivs,Remark) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}')",
                                                        month, empid, ddlClientID.SelectedValue, design, duties, ots, penalty, canteenadvance, incentives, Remark);
                                insertsate =config.ExecuteNonQueryWithQueryAsync(sqlinsert).Result;
                            }

                            #endregion

                            else if (dtdesign.Rows.Count == 0)
                            {
                                Remark = "Designation Error";

                                string sqlinsert = string.Format("insert into Notinsertdata(Month,EmpId,ClientId,Design,NoOfDuties," +
                                    "OT,Penalty,CanteenAdv,Incentivs,Remark) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}')",
                                    month, empid, ddlClientID.SelectedValue, design, duties, ots, penalty, canteenadvance, incentives, Remark);
                                insertsate =config.ExecuteNonQueryWithQueryAsync(sqlinsert).Result;

                                if (insertsate > 0)
                                {
                                    ScriptManager.RegisterStartupScript(this, GetType(), "Show alert", "alert('Some Modificatons are required in Click on Unsaved Button');", true);
                                    btnExport.Visible = true;
                                }

                            }

                        }
                    }
                }


            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Show alert", "alert('Please Upload Valid Data');", true);

            }


            FillAttendanceGrid();
            DismatchDesignation();
            //importunsaveddata();

        }

        protected void DismatchDesignation()
        {
            int month = 0;
            if (ddlMonth.SelectedIndex == 0)
            {
                return;
            }
            month = Timings.Instance.GetIdForSelectedMonth(ddlMonth.SelectedIndex);
            if (ddlClientID.SelectedIndex > 0)
            {
                string selqry = "select * from notinsertdata where clientid='" + ddlClientID.SelectedValue + "' and month='" + month + "'";
                DataTable dtselect = config.ExecuteAdaptorAsyncWithQueryParams(selqry).Result;
                if (dtselect.Rows.Count > 0)
                {
                    GridView2.DataSource = dtselect;
                    GridView2.DataBind();
                }
                else
                {
                    GridView2.DataSource = null;
                    GridView2.DataBind();
                }
            }
            else
            {

            }
        }


        protected void btnClear_Click(object sender, EventArgs e)
        {
            int month = 0;
            if (ddlMonth.SelectedIndex == 0)
            {
                return;
            }
            month = Timings.Instance.GetIdForSelectedMonth(ddlMonth.SelectedIndex);

            if (ddlClientID.SelectedIndex > 0 && ddlMonth.SelectedIndex > 0)
            {

                string sqldeleteempattendance = "delete empattendance where clientid='" + ddlClientID.SelectedValue + "' and month='" + month + "'";
                int status =config.ExecuteNonQueryWithQueryAsync(sqldeleteempattendance).Result;
                if (status > 0)
                {
                    GridView1.DataSource = null;
                    GridView1.DataBind();
                    gvAttendancestatus.DataSource = null;
                    gvAttendancestatus.DataBind();
                    GridView2.DataSource = null;
                    GridView2.DataBind();
                    lblMessage.Text = string.Empty;
                }


            }
        }

        #region Begin New code for Old attendance on 21/03/2014 by venkat

        protected void Chk_Month_OnCheckedChanged(object sender, EventArgs e)
        {

            //#region Validation

            //if (ddlClientID.SelectedIndex == 0)
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Please Select The client Id');", true);
            //    Chk_Month.Checked = false;
            //    return;
            //}

            //#endregion

            //gvfromcontracts.DataSource = null;
            //gvfromcontracts.DataBind();
            //GridView1.DataSource = null;
            //GridView1.DataBind();
            //Txt_Month.Text = string.Empty;
            //ddlMonth.SelectedIndex = 0;
            //lblTotalDuties.Text = string.Empty;
            //lblTotalOts.Text = string.Empty;

            //if (Chk_Month.Checked)
            //{
            //    Txt_Month.Visible = true;
            //    ddlMonth.SelectedIndex = 0;
            //    ddlMonth.Visible = false;

            //}
            //else
            //{
            //    Txt_Month.Visible = false;
            //    Txt_Month.Text = "";
            //    ddlMonth.SelectedIndex = 0;
            //    ddlMonth.Visible = true;
            //}
        }

        protected void Txt_Month_OnTextChanged(object sender, EventArgs e)
        {
            lblTotalDuties.Text = string.Empty;
            lblTotalOts.Text = string.Empty;

            // if (Txt_Month.Text.Trim().Length > 0)
            {
                FillAttendanceGrid();
            }
            //else
            {
                //  gvfromcontracts.DataSource = null;
                // gvfromcontracts.DataBind();
                GridView1.DataSource = null;
                GridView1.DataBind();

            }
        }


        public int GetMonthBasedOnSelectionDateorMonth()
        {

            var testDate = 0;
            string EnteredDate = "";

            #region Validation

            // if (Txt_Month.Text.Trim().Length > 0)
            {

                try
                {

                    //       testDate = GlobalData.Instance.CheckEnteredDate(Txt_Month.Text);
                    //     if (testDate > 0)
                    {
                        //       ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('You Are Entered Invalid  DATE.Date Format Should be [DD/MM/YYYY].Ex.01/01/1990');", true);
                        //     return 0;
                    }
                    //EnteredDate = DateTime.Parse(Txt_Month.Text, CultureInfo.GetCultureInfo("en-gb")).ToString();
                }
                catch (Exception ex)
                {
                    //ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('You Are Entered Invalid  DATE.Date Format Should be [DD/MM/YYYY].Ex.01/01/1990');", true);
                    //return 0;
                }
            }
            #endregion


            #region  Month Get Based on the Control Selection
            int month = 0;
            //if (Chk_Month.Checked == false)
            {
                month = Timings.Instance.GetIdForSelectedMonth(ddlMonth.SelectedIndex);
                //return month;
            }
            //if (Chk_Month.Checked == true)
            {
                //  DateTime date = DateTime.Parse(Txt_Month.Text, CultureInfo.GetCultureInfo("en-gb"));
                //month = Timings.Instance.GetIdForEnteredMOnth(date);
                //return month;
            }
            return month;
            #endregion

        }

        public string GetMonthOfYear()
        {
            string MonthYear = "";

            int month = GetMonthBasedOnSelectionDateorMonth();
            if (month.ToString().Length == 4)
            {
                MonthYear = "20" + month.ToString().Substring(2, 2);
            }
            if (month.ToString().Length == 3)
            {
                MonthYear = "20" + month.ToString().Substring(1, 2);

            }
            return MonthYear;
        }

        //protected void btnSubmit_Click(object sender, EventArgs e)
        //{


        //    var password = string.Empty;
        //    var SPName = string.Empty;
        //    password = txtPassword.Text.Trim();
        //    string sqlPassword = "select password from IouserDetails where password='" + txtPassword.Text + "'";
        //    DataTable dtpassword = SqlHelper.Instance.GetTableByQuery(sqlPassword);
        //    if (dtpassword.Rows.Count == 0)
        //    {
        //        ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Invalid Password');", true);
        //        return;
        //    }

        //    #region Validation

        //    if (ddlClientID.SelectedIndex == 0)
        //    {
        //        ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Please Select The client Id');", true);
        //        Chk_Month.Checked = false;
        //        return;
        //    }

        //    #endregion
        //    Chk_Month.Checked = true;
        //    gvfromcontracts.DataSource = null;
        //    gvfromcontracts.DataBind();
        //    GridView1.DataSource = null;
        //    GridView1.DataBind();
        //    Txt_Month.Text = string.Empty;
        //    ddlMonth.SelectedIndex = 0;
        //    lblTotalDuties.Text = string.Empty;
        //    lblTotalOts.Text = string.Empty;

        //    if (Chk_Month.Checked)
        //    {
        //        Txt_Month.Visible = true;
        //        ddlMonth.SelectedIndex = 0;
        //        ddlMonth.Visible = false;

        //    }

        //}

        //protected void btnClose_Click(object sender, EventArgs e)
        //{
        //    modelLogindetails.Hide();
        //    Chk_Month.Checked = false;
        //    gvfromcontracts.DataSource = null;
        //    gvfromcontracts.DataBind();
        //    GridView1.DataSource = null;
        //    GridView1.DataBind();
        //    Txt_Month.Text = string.Empty;
        //    ddlMonth.SelectedIndex = 0;
        //    lblTotalDuties.Text = string.Empty;
        //    lblTotalOts.Text = string.Empty;
        //    if (Chk_Month.Checked == false)
        //    {
        //        Txt_Month.Visible = false;
        //        Txt_Month.Text = "";
        //        ddlMonth.SelectedIndex = 0;
        //        ddlMonth.Visible = true;
        //    }
        //}

        #endregion


        protected void btnExport_Click(object sender, EventArgs e)
        {
            if (GridView2.Rows.Count > 0)
            {
                gve.Export("UnsavedData.xls", this.GridView2);
            }

        }
        protected void btnClearAll_Click(object sender, EventArgs e)
        {
            int month = 0;
            if (ddlMonth.SelectedIndex == 1)
            {
                month = GlobalData.Instance.GetIDForNextMonth();
            }
            else
            {
                if (ddlMonth.SelectedIndex == 2)
                {
                    month = GlobalData.Instance.GetIDForThisMonth();
                }
                if (ddlMonth.SelectedIndex == 3)
                {
                    month = GlobalData.Instance.GetIDForPrviousMonth();
                }
            }

            if (ddlClientID.SelectedIndex > 0 && ddlMonth.SelectedIndex > 0)
            {

                string sqldeleteempattendance = "delete empattendance where clientid='" + ddlClientID.SelectedValue + "' and month='" + month + "'";
                int status =config.ExecuteNonQueryWithQueryAsync(sqldeleteempattendance).Result;

                if (status > 0)
                {
                    GridView1.DataSource = null;
                    GridView1.DataBind();
                    gvAttendancestatus.DataSource = null;
                    gvAttendancestatus.DataBind();
                    GridView2.DataSource = null;
                    GridView2.DataBind();
                    lblMessage.Text = string.Empty;

                }


            }
        }




    }
}