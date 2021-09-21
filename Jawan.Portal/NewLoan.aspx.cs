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
using System.IO;
using System.Data.OleDb;
using Jawan.Portal.DAL;
namespace Jawan.Portal
{
    public partial class NewLoan : System.Web.UI.Page
    {

        DataTable dt;
        //double loanid;
        string EmpIDPrefix = "";
        string CmpIDPrefix = "";
        string BranchID = "";
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
                        lblDisplayUser.Text = Session["UserId"].ToString();
                        PreviligeUsers(Convert.ToInt32(Session["AccessLevel"]));
                        lblcname.Text = SqlHelper.Instance.GetCompanyname();
                    }
                    else
                    {
                        Response.Redirect("login.aspx");
                    }

                    loanidauto();

                    GetEmpid();
                    GetEmpName();
                    
                    txtloanissuedate.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");


                    string ImagesFolderPath = Server.MapPath("ImportDocuments");
                    string[] filePaths = Directory.GetFiles(ImagesFolderPath);

                    foreach (string file in filePaths)
                    {
                        File.Delete(file);
                    }

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



       //protected void txtEmpid_TextChanged(object sender, EventArgs e)
       // {


       //     if (ddlEmpId.SelectedValue.Trim().Length > 0)
       //     {
       //         GetEmpName();
       //         GetEmployeeLoanDetails();
       //     }
       //     else
       //     {
       //         ClearData();
       //     }

       // }

       

        protected void GetEmpid()
        {
            string SelectQuery = "select Empid from empdetails where Branch="+BranchID +" ";
            DataTable dt = config.ExecuteAdaptorAsyncWithQueryParams(SelectQuery).Result;

            if (dt.Rows.Count > 0)
            {
                try
                {
                    ddlEmpId.DataSource = dt;
                    ddlEmpId.DataTextField = "EmpId";
                    ddlEmpId.DataValueField = "EmpId";
                    ddlEmpId.DataBind();

                    ddlEmpId.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                catch (Exception ex)
                {
                }
            }
            else
            {

            }
        }

        protected void GetEmpName()
        {
            string SelectQuery = "select empid, (empfname+' '+empmname+' '+emplname) as empname from empdetails";
            DataTable dt = config.ExecuteAdaptorAsyncWithQueryParams(SelectQuery).Result;

            if (dt.Rows.Count > 0)
            {
                try
                {
                    ddlEmpName.DataSource = dt;
                    ddlEmpName.DataTextField = "empname";
                    ddlEmpName.DataValueField = "empname";
                    ddlEmpName.DataBind();

                    ddlEmpName.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                catch (Exception ex)
                {

                }
            }
            else
            {

            }
        }


        protected void GetEmpNameByEmpId( string empid)
        {
            string EMpname = "";
            string Sqlqry = "select (empfname+' '+empmname+' '+emplname) as empname,EmpDesgn from empdetails where empid='" + empid + "'";
            DataTable dt = config.ExecuteAdaptorAsyncWithQueryParams(Sqlqry).Result;
            if (dt.Rows.Count > 0)
            {
                try
                {
                    ddlEmpName.SelectedValue = dt.Rows[0]["empname"].ToString();
                }
                catch (Exception ex)
                {
                    // MessageLabel.Text = ex.Message;
                }
            }
            else
            {
                // MessageLabel.Text = "There Is No Name For The Selected Employee";
            }
        }

        protected void GetEmpidByEmpName( string empname)
        {
            #region  Old Code
            string Sqlqry = "select Empid from empdetails where (empfname+' '+empmname+' '+emplname)  like '" +empname + "' ";
            DataTable dt = config.ExecuteAdaptorAsyncWithQueryParams(Sqlqry).Result;

            if (dt.Rows.Count > 0)
            {
                try
                {
                    ddlEmpId.SelectedValue = dt.Rows[0]["Empid"].ToString();

                }
                catch (Exception ex)
                {
                    // MessageLabel.Text = ex.Message;
                }
            }
            else
            {
                // MessageLabel.Text = "There Is No Name For The Selected Employee";
            }
            #endregion // End Old Code
        }


        //protected void txtName_TextChanged(object sender, EventArgs e)
        //{


        //    if (ddlEmpName.SelectedValue.Trim().Length > 0)
        //    {
        //        GetEmpid();
        //        GetEmployeeLoanDetails();
        //    }
        //    else
        //    {
        //        ClearData();
        //    }

        //}

        protected void PreviligeUsers(int previligerid)
        {
            switch (previligerid)
            {

                case 1:
                    break;
                case 2:
                    AddEmployeeLink.Visible = true;
                    ModifyEmployeeLink.Visible = false;
                    DeleteEmployeeLink.Visible = false;
                    AssigningWorkerLink.Visible = false;
                    LoanLink.Visible = false;
                    PaymentLink.Visible = false;
                    //TrainingEmployeeLink.Visible = false;
                    //JobLeavingReasonsLink.Visible = false;
                    AttendanceLink.Visible = false;

                    CompanyInfoLink.Visible = false;
                    ReportsLink.Visible = false;
                    InventoryLink.Visible = false;
                    SettingsLink.Visible = false;

                    break;

                case 3:
                    AddEmployeeLink.Visible = false;
                    ModifyEmployeeLink.Visible = false;
                    DeleteEmployeeLink.Visible = false;
                    AssigningWorkerLink.Visible = false;
                    AttendanceLink.Visible = true;
                    LoanLink.Visible = true;
                    PaymentLink.Visible = true;
                    //TrainingEmployeeLink.Visible = false;
                    PostingOrderListLink.Visible = true;
                    //JobLeavingReasonsLink.Visible = false;

                    ClientsLink.Visible = true;
                    CompanyInfoLink.Visible = false;
                    InventoryLink.Visible = false;
                    ReportsLink.Visible = true;
                    SettingsLink.Visible = true;
                    break;

                case 4:

                    employeeslink.Disabled = true;
                    AddEmployeeLink.Visible = false;
                    ModifyEmployeeLink.Visible = false;
                    DeleteEmployeeLink.Visible = false;
                    AssigningWorkerLink.Visible = false;
                    LoanLink.Visible = true;
                    PaymentLink.Visible = true;
                    //TrainingEmployeeLink.Visible = false;
                    //JobLeavingReasonsLink.Visible = false;
                    AttendanceLink.Visible = false;

                    CompanyInfoLink.Visible = true;
                    ReportsLink.Visible = false;
                    InventoryLink.Visible = false;
                    SettingsLink.Visible = false;





                    break;
                case 5:

                    AddEmployeeLink.Visible = false;
                    ModifyEmployeeLink.Visible = false;
                    DeleteEmployeeLink.Visible = false;
                    AssigningWorkerLink.Visible = false;
                    LoanLink.Visible = true;
                    PaymentLink.Visible = true;
                    //TrainingEmployeeLink.Visible = false;
                    //JobLeavingReasonsLink.Visible = false;
                    AttendanceLink.Visible = true;

                    CompanyInfoLink.Visible = true;
                    ReportsLink.Visible = false;
                    InventoryLink.Visible = false;
                    SettingsLink.Visible = false;
                    break;
                case 6:

                    AddEmployeeLink.Visible = false;
                    ModifyEmployeeLink.Visible = false;
                    DeleteEmployeeLink.Visible = false;
                    AssigningWorkerLink.Visible = false;
                    LoanLink.Visible = false;
                    PaymentLink.Visible = false;
                    //TrainingEmployeeLink.Visible = false;
                    //JobLeavingReasonsLink.Visible = false;
                    AttendanceLink.Visible = true;

                    CompanyInfoLink.Visible = false;
                    ReportsLink.Visible = false;
                    InventoryLink.Visible = false;
                    SettingsLink.Visible = false;






                    break;
                default:
                    break;


            }
        }

        private void GetLoanData()
        {
            string selectquery = "select LoanNo as LoanId,LoanAmount,case TypeOfLoan when 0 then 'Sal.Adv' when 1 then 'Uniform' when 2 then 'Security Deposit' when 3 then 'Loan' when 4 then 'ATM' when 5 then 'Others' when 6 then 'ID card deduction' when 7 then 'Telephone Bill' when 8 then 'PF & ESI Contribution' when 9 then 'TDS Ded' end as TypeOfLoan, " +
                " NoInstalments,LoanDt,LoanStatus from EmpLoanMaster where EmpId='" +
                ddlEmpId.SelectedValue + "' ORDER BY LoanDt DESC";



            dt = config.ExecuteAdaptorAsyncWithQueryParams(selectquery).Result;
            if (dt.Rows.Count > 0)
            {
                gvNewLoan.DataSource = dt;
                gvNewLoan.DataBind();

                foreach (GridViewRow gvrow in gvNewLoan.Rows)
                {
                    string LoanID = ((Label)gvrow.FindControl("lblLoanid")).Text;
                    string LoanAmount = ((Label)gvrow.FindControl("lblLoanAmount")).Text;
                    Label DueAmount = (Label)gvrow.FindControl("lblDueAmount");
                    Label TypeOfLoan = (Label)gvrow.FindControl("TypeOfLoan");

                    string SqlqRyDueAmount = "Select Sum(isnull(RecAmt,0)) as Paid  From emploandetails Where Loanno='" + LoanID + "'";
                    DataTable dtDueAmount = config.ExecuteAdaptorAsyncWithQueryParams(SqlqRyDueAmount).Result;
                    if (dtDueAmount.Rows.Count > 0)
                    {
                        if (String.IsNullOrEmpty(dtDueAmount.Rows[0]["Paid"].ToString()) == false)

                            DueAmount.Text = (double.Parse(LoanAmount) - double.Parse(dtDueAmount.Rows[0]["Paid"].ToString())).ToString();
                        else
                            DueAmount.Text = LoanAmount;
                    }
                    else
                    {
                        DueAmount.Text = LoanAmount;
                    }



                }



            }
            else
            {
                gvNewLoan.DataSource = null;
                gvNewLoan.DataBind();
            }
        }

        protected void ClearData()
        {
            //ddlempmname.SelectedIndex = 0;
           // ddlEmpId.SelectedValue = "";
            //txtLastName.Text = "";
            txtnoofinstall.Text = "";
            txtDescripition.Text = "";
            txtNewLoan.Text = "";
           // ddlEmpName.SelectedValue = "";
            ddlLoanType.SelectedIndex = 0;
            gvNewLoan.DataSource = null;
            gvNewLoan.DataBind();
            txtLoanDate.Text = "";// DateTime.Parse("01/01/0001");
            txtloanissuedate.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");

        }

        private void loanidauto()
        {
            //getloandata();
            int loanid;
            string selectqueryclientid = "select max(cast(LoanNo as int )) as Loanno from EmpLoanMaster ";
            dt = config.ExecuteAdaptorAsyncWithQueryParams(selectqueryclientid).Result;

            if (dt.Rows.Count > 0)
            {
                //  DtEmpId = dtempid.Rows[dtempid.Rows.Count - 1][0].ToString();
                if (String.IsNullOrEmpty(dt.Rows[0]["LoanNo"].ToString()) == false)
                {
                    loanid = Convert.ToInt32(dt.Rows[0]["LoanNo"].ToString()) + 1;
                    txtloanid.Text = loanid.ToString();
                }
                else
                {
                    loanid = int.Parse("1");
                    txtloanid.Text = loanid.ToString();
                }
            }
        }

        protected void GetEmployeeLoanDetails()
        {
            loanidauto();
            GetLoanData();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                int loanStatus = 0;
                string loantype = txtDescripition.Text;
                var loandate = "01/01/1900";
                var loanissuedate = "01/01/1900";



                if (ddlEmpId.SelectedValue == "")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Select Employee ID');", true);
                    return;
                }
                string empid = ddlEmpId.SelectedValue.ToString();
                string loanamount = txtNewLoan.Text.Trim();
                if (loanamount.Length <= 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Please fill Loan Amount');", true);
                    return;
                }
                string noofinstallements = txtnoofinstall.Text.Trim();
                if (noofinstallements.Length <= 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Enter no. of installments');", true);
                    return;
                }



                int testDate = 0;

                if (txtLoanDate.Text.Trim().Length == 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Please fill Loan  cutting month');", true);
                    return;
                }


                if (txtLoanDate.Text.Trim().Length > 0)
                {
                    testDate = GlobalData.Instance.CheckEnteredDate(txtLoanDate.Text);
                    if (testDate > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(),
                            "show alert", "alert('You Are Entered Invalid Loan  Cutting Month.Date Format Should be [DD/MM/YYYY].Ex.01/01/1990');", true);
                        return;
                    }

                    loandate = Timings.Instance.CheckDateFormat(txtLoanDate.Text);

                }


                if (txtloanissuedate.Text.Trim().Length == 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Please fill Loan  Issue Date');", true);
                    return;
                }


                if (txtloanissuedate.Text.Trim().Length > 0)
                {
                    testDate = GlobalData.Instance.CheckEnteredDate(txtloanissuedate.Text);
                    if (testDate > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(),
                            "show alert", "alert('You Are Entered Invalid Loan  Issue Date.Date Format Should be [DD/MM/YYYY].Ex.01/01/1990');", true);
                        return;
                    }

                    //loanissuedate = DateTime.Parse(txtloanissuedate.Text, CultureInfo.GetCultureInfo("en-gb")).ToString();
                    loanissuedate = Timings.Instance.CheckDateFormat(txtloanissuedate.Text);

                }



                int TypeOfLoan = 0;
                if (ddlLoanType.SelectedIndex == 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showlalert", "alert('Select Loan Type');", true);
                    return;
                }
                else
                {
                    TypeOfLoan = ddlLoanType.SelectedIndex - 1;
                }

                string insertquery = string.Format(" insert into EmpLoanMaster(loandt,empid,loantype,loanamount,NoInstalments,  " +
                    " LoanStatus,TypeOfLoan,LoanIssuedDate) values( '{0}','{1}','{2}','{3}',{4},{5},'{6}','{7}') ",
                     loandate, empid, loantype,
                    loanamount, noofinstallements, loanStatus, TypeOfLoan, loanissuedate);
                int status =config.ExecuteNonQueryWithQueryAsync(insertquery).Result;
                if (status != 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showlalert", "alert('New Loan Generated Successfuly');", true);
                    loanidauto();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showlalert", "alert('Loan not Generated');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showlalert", "alert('Invalid Data');", true);
            }
            ClearData();
        }

        protected void ddlLoanType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlLoanType.SelectedIndex == 4)
            {
                txtnoofinstall.Text = "1";
                txtnoofinstall.Enabled = false;
            }
            else
            {
                txtnoofinstall.Text = "";
                txtnoofinstall.Enabled = true;
            }
        }


        #region Begin New Code for Import loan data from Excel as on 22/02/2014 by venkat

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

        protected void btnImportData_Click(object sender, EventArgs e)
        {
            ClearData();

            try
            {
                string filename = Path.Combine(Server.MapPath("ImportDocuments"), Guid.NewGuid().ToString() + Path.GetExtension(fileupload1.PostedFile.FileName));
                fileupload1.PostedFile.SaveAs(filename);
                string extn = Path.GetExtension(fileupload1.PostedFile.FileName);
                string constring = string.Empty;
                if (extn.ToLower() == ".xls")
                {
                    constring = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filename + ";Extended properties=\"excel 12.0;HDR=Yes;IMEX=2\"";
                }
                if (extn.ToLower() == ".xlsx")
                {
                    constring = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filename + ";Extended properties=\"excel 12.0;HDR=Yes;IMEX=2\"";
                }
                string Sheetname = string.Empty;

                string qry = "select [Emp Id],[Loan Amount],[Loan Type],[Loan Date],[Loan cutting Month],[Loan Installment]" +
                   "  from  [" + GetExcelSheetNames() + "]" + "";
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
                    string empid = string.Empty;
                    string loancuttingmonth = string.Empty; ;
                    string loanissuedate = string.Empty;
                    float loanamount = 0;
                    int loanstatus = 0;
                    int Noofinstallments = 1;
                    int loantype = 0;
                    string description = string.Empty;

                    int loanid = 1;
                    string selectqueryclientid = "select max(cast(LoanNo as int )) as Loanno from EmpLoanMaster ";
                    dt = config.ExecuteAdaptorAsyncWithQueryParams(selectqueryclientid).Result ;

                    if (dt.Rows.Count > 0)
                    {
                        //  DtEmpId = dtempid.Rows[dtempid.Rows.Count - 1][0].ToString();
                        if (String.IsNullOrEmpty(dt.Rows[0]["LoanNo"].ToString()) == false)
                        {
                            loanid = Convert.ToInt32(dt.Rows[0]["LoanNo"].ToString()) + 1;
                        }
                        else
                        {
                            loanid = int.Parse("1");
                        }
                    }


                    int testDate = 0;



                    empid = dr["Emp Id"].ToString();
                    // description = dr["Description"].ToString();
                    loancuttingmonth = dr["Loan cutting Month"].ToString();
                    loanissuedate = dr["Loan Date"].ToString();

                    if (empid.Length == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Enter employee id');", true);
                        return;
                    }

                    if (empid.Length > 5 || empid.Length < 5)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Empid is should enter 5 letters');", true);
                        return;
                    }

                    if (String.IsNullOrEmpty(dr["Loan Amount"].ToString()) == false)
                    {
                        loanamount = float.Parse(dr["Loan Amount"].ToString());
                    }
                    if (loanamount == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Loan amount should be enter');", true);
                        return;
                    }

                    if (String.IsNullOrEmpty(dr["Loan Type"].ToString()) == false)
                    {
                        loantype = int.Parse(dr["Loan Type"].ToString());
                    }


                    if (loantype > 3)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Loan type should be enter');", true);
                        return;
                    }

                    if (String.IsNullOrEmpty(dr["Loan Installment"].ToString()) == false)
                    {
                        Noofinstallments = int.Parse(dr["Loan Installment"].ToString());
                    }


                    if (loancuttingmonth.Length > 0)
                    {
                        testDate = GlobalData.Instance.CheckEnteredDate(loancuttingmonth);
                        if (testDate > 0)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(),
                                "show alert", "alert('You Are Entered Invalid Loan  Issue Date.Date Format Should be [DD/MM/YYYY].Ex.01/01/1990 for " + empid + "');", true);
                            return;
                        }
                        loancuttingmonth = DateTime.Parse(loancuttingmonth, CultureInfo.GetCultureInfo("en-gb")).ToString();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Enter Loan cutting month');", true);
                        return;
                    }
                    if (loanissuedate.Length > 0)
                    {
                        testDate = GlobalData.Instance.CheckEnteredDate(loanissuedate);
                        if (testDate > 0)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(),
                                "show alert", "alert('You Are Entered Invalid Loan  Issue Date.Date Format Should be [DD/MM/YYYY].Ex.01/01/1990 for " + empid + "');", true);
                            return;
                        }
                        loanissuedate = DateTime.Parse(loanissuedate, CultureInfo.GetCultureInfo("en-gb")).ToString();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Enter Loan issue date');", true);
                        return;
                    }

                    string sqlverifyemp = "select empid from EmpLoanMaster where LoanIssuedDate='" + loanissuedate + "' and empid='" + empid + "'";
                    DataTable dtemp = config.ExecuteAdaptorAsyncWithQueryParams(sqlverifyemp).Result;
                    if (dtemp.Rows.Count == 0)
                    {

                        string insertquery = string.Format(" insert into EmpLoanMaster(loandt,empid,loantype,loanamount,NoInstalments,  " +
                            " LoanStatus,TypeOfLoan,LoanIssuedDate) values( {0},'{1}','{2}','{3}',{4},{5},{6},{7}) ",
                              loanissuedate, empid, loantype,
                            loanamount, Noofinstallments, loanstatus, loantype, loanissuedate);
                        int status = config.ExecuteNonQueryWithQueryAsync(insertquery).Result;
                        if (status != 0)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "showlalert", "alert('New Loans Generated Successfuly');", true);
                            loanidauto();
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "showlalert", "alert('Loan not Generated for '" + empid + "'');", true);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Show alert", "alert('Please Upload Valid Data');", true);
            }

        }

        #endregion

        protected void ddlEmpId_SelectedIndexChanged(object sender, EventArgs e)
        {
            string empid="";

            if (ddlEmpId.SelectedValue.Trim().Length > 0)
            {
                empid = ddlEmpId.SelectedItem.ToString();
                GetEmpNameByEmpId(empid);
                GetEmployeeLoanDetails();
            }
            else
            {
                ClearData();
            }
        }

        protected void ddlEmpName_SelectedIndexChanged(object sender, EventArgs e)
        {
            string empname = "";

            if (ddlEmpName.SelectedValue.Trim().Length > 0)
            {
                empname = ddlEmpName.SelectedItem.ToString();
                GetEmpidByEmpName(empname);
                GetEmployeeLoanDetails();
            }
            else
            {
                ClearData();
            }
        }
    }
}