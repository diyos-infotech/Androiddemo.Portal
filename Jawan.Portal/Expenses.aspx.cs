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
    public partial class Expenses : System.Web.UI.Page
    {
        AppConfiguration config = new AppConfiguration();
        GridViewExportUtil gve = new GridViewExportUtil();
        string EmpIDPrefix = "";
        string CmpIDPrefix = "";
        string BranchID = "";
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {

                Pnl_Search.Visible = true;
                Ddl_Voucher_No.Visible = false;

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
                            case 0:// Write Frames Invisible Links
                                break;
                            case 1://Write KLTS Invisible Links
                                ExpensesLink.Visible = false;
                                break;
                            default:
                                break;
                        }

                    }
                    else
                    {
                        Response.Redirect("login.aspx");
                    }
                    VocherIDAutoGenrate();
                    LoadEmpidsExpensesapprovedBy();
                    LoadBankNames();
                    LoadExpensesPurpose();
                    LoadPayment_Mode();
                    LoadClientList();
                    LoadClientNames();
                    txtdate.Text = DateTime.Now.Date.ToShortDateString();


                    Lbl_Client_Id.Visible = false;
                    Ddl_ClientId.Visible = false;

                    Lbl_Client_Name.Visible = false;
                    Ddl_Cname.Visible = false;

                    Lbl_Month.Visible = false;
                    Txt_Month_Selected_Salary.Visible = false;
                    Txt_Month_Selected_Salary.Text = "";
                    LoadTop5Vouchers();
                }

            }
            catch (Exception Ex)
            {
                Response.Redirect("login.aspx");
            }
        }

        protected void LoadClientNames()
        {
            DataTable DtClientids = GlobalData.Instance.LoadCNames(CmpIDPrefix,BranchID);
            if (DtClientids.Rows.Count > 0)
            {
                Ddl_Cname.DataValueField = "Clientid";
                Ddl_Cname.DataTextField = "clientname";
                Ddl_Cname.DataSource = DtClientids;
                Ddl_Cname.DataBind();
            }
            Ddl_Cname.Items.Insert(0, "-Select-");

        }

        protected void LoadClientList()
        {
            DataTable DtClientNames = GlobalData.Instance.LoadCIds(CmpIDPrefix,BranchID);
            if (DtClientNames.Rows.Count > 0)
            {
                Ddl_ClientId.DataValueField = "Clientid";
                Ddl_ClientId.DataTextField = "Clientid";
                Ddl_ClientId.DataSource = DtClientNames;
                Ddl_ClientId.DataBind();
            }
            Ddl_ClientId.Items.Insert(0, "-Select-");
        }

        protected void GetWebConfigdata()
        {

            EmpIDPrefix = Session["EmpIDPrefix"].ToString();
            CmpIDPrefix = Session["CmpIDPrefix"].ToString();
            BranchID = Session["BranchID"].ToString();
        }

        protected void PreviligeUsers(int previligerid)
        {

            switch (previligerid)
            {
                case 1:
                    break;
                case 2:
                    AddCompanyInfoLink.Visible = true;
                    ModifyCompanyInfoLink.Visible = false;
                    DeleteCompanyInfoLink.Visible = false;
                    InventoryLink.Visible = false;
                    ReportsLink.Visible = false;
                    SettingsLink.Visible = false;

                    break;

                case 3:

                    InventoryLink.Visible = false;
                    ReportsLink.Visible = false;
                    SettingsLink.Visible = false;
                    break;
                case 4:
                    AddCompanyInfoLink.Visible = true;
                    ModifyCompanyInfoLink.Visible = true;
                    DeleteCompanyInfoLink.Visible = true;

                    EmployeesLink.Visible = true;
                    ClientsLink.Visible = true;
                    CompanyInfoLink.Visible = true;
                    InventoryLink.Visible = true;
                    ReportsLink.Visible = true;
                    SettingsLink.Visible = true;
                    break;
                case 5:
                    CompanyInfoLink.Visible = false;
                    AddCompanyInfoLink.Visible = false;
                    ModifyCompanyInfoLink.Visible = false;
                    DeleteCompanyInfoLink.Visible = false;
                    break;
                case 6:

                    CompanyInfoLink.Visible = false;
                    AddCompanyInfoLink.Visible = false;
                    ModifyCompanyInfoLink.Visible = false;
                    DeleteCompanyInfoLink.Visible = false;

                    InventoryLink.Visible = false;
                    ReportsLink.Visible = false;
                    SettingsLink.Visible = false;

                    break;
                default:
                    break;


            }
        }

        protected void LoadEmpidsExpensesapprovedBy()
        {

            DataTable DtEmpNames = GlobalData.Instance.LoadExpensesapprovedBy();
            if (DtEmpNames.Rows.Count > 0)
            {
                Ddl_Approved_By_Empids.DataValueField = "empid";
                Ddl_Approved_By_Empids.DataTextField = "EmpidandName";
                Ddl_Approved_By_Empids.DataSource = DtEmpNames;
                Ddl_Approved_By_Empids.DataBind();
            }
            Ddl_Approved_By_Empids.Items.Insert(0, "-Select-");

        }

        protected void LoadBankNames()
        {

            DataTable DtBankNames = GlobalData.Instance.LoadBankNames();
            if (DtBankNames.Rows.Count > 0)
            {
                Ddl_From_Bank.DataValueField = "bankid";
                Ddl_From_Bank.DataTextField = "bankname";
                Ddl_From_Bank.DataSource = DtBankNames;
                Ddl_From_Bank.DataBind();



                Ddl_Modified_Bank_Name.DataValueField = "bankid";
                Ddl_Modified_Bank_Name.DataTextField = "bankname";
                Ddl_Modified_Bank_Name.DataSource = DtBankNames;
                Ddl_Modified_Bank_Name.DataBind();

            }
            Ddl_From_Bank.Items.Insert(0, "-Select-");
            Ddl_Modified_Bank_Name.Items.Insert(0, "-Select-");

        }

        protected void LoadExpensesPurpose()
        {

            DataTable DtExpensesPurpose = GlobalData.Instance.LoadExpensesPurpose();
            if (DtExpensesPurpose.Rows.Count > 0)
            {
                Ddl_Purpose.DataValueField = "id";
                Ddl_Purpose.DataTextField = "name";
                Ddl_Purpose.DataSource = DtExpensesPurpose;
                Ddl_Purpose.DataBind();
            }
            Ddl_Purpose.Items.Insert(0, "-Select-");

        }

        protected void LoadPayment_Mode()
        {

            DataTable DtExpensesPurpose = GlobalData.Instance.LoadPayment_Mode();
            if (DtExpensesPurpose.Rows.Count > 0)
            {
                Ddl_Payment_Mode.DataValueField = "id";
                Ddl_Payment_Mode.DataTextField = "name";
                Ddl_Payment_Mode.DataSource = DtExpensesPurpose;
                Ddl_Payment_Mode.DataBind();
            }
            Ddl_Payment_Mode.Items.Insert(0, "-Select-");

        }

        protected void Btn_Add_Voucher_Click(object sender, EventArgs e)
        {

            try
            {
                int testDate = 0;
                #region  Begin code For Validations as on [13-01/2013]
                if (txtpaidto.Text.Trim().Length == 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Please Enter the PAID TO ');", true);
                    return;
                }

                if (txtamount.Text.Trim().Length == 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Please Enter the Amount ');", true);
                    return;
                }

                if (txtdate.Text.Trim().Length == 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Please Select The  Date ');", true);
                    return;
                }
                else
                {
                    testDate = GlobalData.Instance.CheckEnteredDate(txtdate.Text);
                    if (testDate > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('You Are Entered Invalid Date.Date Format Should be [DD/MM/YYYY].Ex.01/01/1990');", true);
                        return;
                    }
                }


                if (Ddl_Purpose.SelectedIndex == 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Please Select The Purpose ');", true);
                    return;
                }

                if (Ddl_Approved_By_Empids.SelectedIndex == 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Please Select The Approved By ');", true);
                    return;
                }

                if (Ddl_Payment_Mode.SelectedIndex == 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Please Select The PaymentMode');", true);
                    return;
                }


                if (Ddl_Payment_Mode.SelectedValue == "2" || Ddl_Payment_Mode.SelectedValue == "3" || Ddl_Payment_Mode.SelectedValue == "4")
                {


                    if (Txt_DD_CheQue_Transaction_Date.Text.Trim().Length == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Please Select The DD/Cheque/Transaction Date');", true);
                        return;
                    }
                    else
                    {
                        testDate = GlobalData.Instance.CheckEnteredDate(Txt_DD_CheQue_Transaction_Date.Text);
                        if (testDate > 0)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('You Are Entered Invalid Date Of Interview.Date Format Should be [DD/MM/YYYY].Ex.01/01/1990');", true);
                            return;
                        }
                    }

                    if (Txt_Dd_CheQue_N0.Text.Trim().Length == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Please Select The DD/Cheque/Transaction No.');", true);
                        return;
                    }

                }

                if (Ddl_Purpose.SelectedValue == "4")
                {
                    if (Ddl_ClientId.SelectedIndex == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Please Select The Client Id.');", true);
                        return;
                    }

                    if (Txt_Month_Selected_Salary.Text.Trim().Length == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Please Select The Month.');", true);
                        return;
                    }
                    else
                    {
                        testDate = GlobalData.Instance.CheckEnteredDate(Txt_Month_Selected_Salary.Text);
                        if (testDate > 0)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('You Are Entered Invalid Check Date.Date Format Should be [DD/MM/YYYY].Ex.01/01/1990');", true);
                            return;
                        }
                    }
                }


                int TestStatus = CheckBalanceinTheSelectedBank();
                if (TestStatus == 1)
                {
                    return;
                }

                #endregion End code For Validations as on [13-01/2013]

                #region  Begin code For Variable Declaration as on [13-01/2013]
                var VoucherNo = "";
                var Date = "";
                var Amount = "0";
                var ModifiedAmount = "0";
                var Paidto = "";
                var Purpose = "0";

                var ApprovedBy = "0";
                var FromBank = "0";
                var ToBank = "0";
                var PaymentMode = "0";
                var DDorCheQueorTranSactioDate = "";
                var DDorCheQueorTranSactioNo = "";
                var ProcedureName = "";
                var Remarks = "";
                var InsertRecordStatus = 0;
                Hashtable HtExpenses = new Hashtable();
                #endregion End code For Variable Declaration as on [13-01/2013]

                #region  Begin code For  Assign Values to  Variable  as on [13-01/2013]
                VoucherNo = txtvocherno.Text;
                // EmpDtofInterview = DateTime.Parse(txtdate.Text, CultureInfo.GetCultureInfo("en-gb")).ToString();
                Date = DateTime.Parse(txtdate.Text, CultureInfo.GetCultureInfo("en-gb")).ToString();
                Amount = txtamount.Text;
                ModifiedAmount = Txt_Modified_Amount.Text;
                Paidto = txtpaidto.Text;
                Purpose = Ddl_Purpose.SelectedValue; ;

                ApprovedBy = Ddl_Approved_By_Empids.SelectedValue;
                FromBank = Ddl_From_Bank.SelectedValue;
                //  ToBank = Ddl_To_Bank.SelectedValue;
                PaymentMode = Ddl_Payment_Mode.SelectedValue;
                if (Txt_DD_CheQue_Transaction_Date.Text.Trim().Length > 0)
                {
                    DDorCheQueorTranSactioDate = DateTime.Parse(Txt_DD_CheQue_Transaction_Date.Text, CultureInfo.GetCultureInfo("en-gb")).ToString();
                }
                DDorCheQueorTranSactioNo = Txt_Dd_CheQue_N0.Text;
                Remarks = Txt_Remarks.Text;


                #region Begin Code For Check Validation as on [07-02-2014]
                if (Ddl_Purpose.SelectedValue == "4")
                {
                    float TotalSalaryAmt = 0;
                    if (Gv_salaryDetails_SiteWise.Rows.Count > 0)
                    {
                        foreach (GridViewRow Gvsalary in Gv_salaryDetails_SiteWise.Rows)
                        {
                            float IndSalaryAmt = 0;
                            TextBox Txt_Net_Amount = (TextBox)Gvsalary.FindControl("Txt_Net_Amount");
                            CheckBox Chk_Select = (CheckBox)Gvsalary.FindControl("chkEmp");
                            if (Chk_Select.Checked)
                            {
                                if (Txt_Net_Amount.Text.Trim().Length == 0)
                                {
                                    Txt_Net_Amount.Text = "0";
                                }
                                IndSalaryAmt = float.Parse(Txt_Net_Amount.Text);
                                TotalSalaryAmt += IndSalaryAmt;
                            }
                        }
                        #region Begin Code  For Modify Expenses Details Validations as on [19-02-2014]

                        if (Txt_Modified_Amount.Text.Trim().Length == 0 || float.Parse(Txt_Modified_Amount.Text) == 0)
                        {

                            if (TotalSalaryAmt == 0)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Please Select Atleast one Employee');", true);
                                return;
                            }


                            if (float.Parse(Amount) > TotalSalaryAmt)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('You Are Entered Amount Greate Than To The All The Employees Salary Amount.Please Check');", true);
                                return;
                            }
                            if (float.Parse(Amount) < TotalSalaryAmt)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('You Are Entered Amount Less Than To The All The Employees Salary Amount.Please Check');", true);
                                return;
                            }
                        }
                        else
                        {
                            if (TotalSalaryAmt == 0)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Please Select Atleast one Employee');", true);
                                return;
                            }


                            if (float.Parse(ModifiedAmount) > TotalSalaryAmt)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('You Are Entered Amount Greate Than To The All The Employees Salary Amount.Please Check');", true);
                                return;
                            }
                            if (float.Parse(ModifiedAmount) < TotalSalaryAmt)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('You Are Entered Amount Less Than To The All The Employees Salary Amount.Please Check');", true);
                                return;
                            }

                        }


                        #endregion

                    }


                }
                #endregion End Code For Check Validation as on [07-02-2014]

                #endregion End code For Assign Values to  Variable as on [13-01/2013]

                #region  Begin code For  Assign Values to  Hash Table  as on [13-01/2013]
                ProcedureName = "AddExpensesVoucher";
                HtExpenses.Add("@Voucherno", VoucherNo);
                HtExpenses.Add("@Date", Date);
                HtExpenses.Add("@Amount", Amount);
                HtExpenses.Add("@ModifiedAmount", ModifiedAmount);
                HtExpenses.Add("@Paidto", Paidto);
                HtExpenses.Add("@Purpose", Purpose);

                HtExpenses.Add("@ApprovedBy", ApprovedBy);
                HtExpenses.Add("@FromBank", FromBank);
                HtExpenses.Add("@ToBank", ToBank);
                HtExpenses.Add("@PaymentMode", PaymentMode);
                HtExpenses.Add("@DDorchequeortransactionDate", DDorCheQueorTranSactioDate);
                HtExpenses.Add("@DDorchequeortransactionNo", DDorCheQueorTranSactioNo);
                HtExpenses.Add("@Remarks", Remarks);



                #endregion End code For Assign Values to   Hash Table  as on [13-01/2013]

                #region Begin Code For Calling Stored Procedure as on [13-01-2013]
                InsertRecordStatus =config.ExecuteNonQueryParamsAsync(ProcedureName, HtExpenses).Result;
                #endregion End Code For Calling Stored Procedure as on [13-01-2013]

                #region Begin code For Resulted Messages as on [13-01-2013]
                if (InsertRecordStatus > 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Record Added SucessFully.');", true);

                    #region Begin Code For Check Validation as on [07-02-2014]
                    if (Ddl_Purpose.SelectedValue == "4")
                    {
                        float TotalSalaryAmt = 0;
                        if (Gv_salaryDetails_SiteWise.Rows.Count > 0)
                        {
                            foreach (GridViewRow Gvsalary in Gv_salaryDetails_SiteWise.Rows)
                            {
                                float IndSalaryAmt = 0;
                                TextBox Txt_Net_Amount = (TextBox)Gvsalary.FindControl("Txt_Net_Amount");
                                Label Lbl_Empid = (Label)Gvsalary.FindControl("Lbl_Empid");
                                CheckBox Chk_Select = (CheckBox)Gvsalary.FindControl("chkEmp");
                                TextBox Txt_RemarksFromSalary = (TextBox)Gvsalary.FindControl("Txt_Remarks");

                                if (Txt_Net_Amount.Text.Trim().Length == 0)
                                {
                                    Txt_Net_Amount.Text = "0";
                                }

                                if (Chk_Select.Checked)
                                {
                                    #region Begin Code For Declare & Assign  Variables  as on [07-02-2014]

                                    var ProcedureNameSalary = "ExpensesAMDSalaryTransactions";
                                    var Empid = Lbl_Empid.Text;
                                    var NetAmt = Txt_Net_Amount.Text;
                                    var Status = 1;
                                    var ClientId = Ddl_ClientId.SelectedValue;
                                    Date = DateTime.Parse(Txt_Month_Selected_Salary.Text, CultureInfo.GetCultureInfo("en-gb")).ToString();
                                    var Month = DateTime.Parse(Date).Month.ToString() + DateTime.Parse(Date).Year.ToString().Substring(2, 2);

                                    var RemarksFromSalary = Txt_RemarksFromSalary.Text;
                                    Hashtable HtEmpids = new Hashtable();
                                    HtEmpids.Clear();

                                    HtEmpids.Add("@Voucherno", VoucherNo);
                                    HtEmpids.Add("@ClientId", ClientId);
                                    HtEmpids.Add("@Month", Month);
                                    HtEmpids.Add("@Empid", Empid);
                                    HtEmpids.Add("@Status", Status);

                                    HtEmpids.Add("@ActualAmount", NetAmt);
                                    HtEmpids.Add("@Remarks", RemarksFromSalary);
                                    //HtEmpids.Add("@ModifiedAmount",);

                                    int StatusTransaction = config.ExecuteNonQueryParamsAsync(ProcedureNameSalary, HtEmpids).Result;


                                    #endregion End Code For Declare & Assign  Variables  as on [07-02-2014]
                                }
                                else
                                {
                                    if (Txt_Modified_Amount.Text.Trim().Length == 0 && float.Parse(Txt_Modified_Amount.Text) > 0)
                                    {
                                        #region Begin Code For Declare & Assign  Variables  as on [07-02-2014]

                                        var ProcedureNameSalary = "ExpensesAMDSalaryTransactions";
                                        var Empid = Lbl_Empid.Text;
                                        var NetAmt = Txt_Net_Amount.Text;
                                        var Status = 1;
                                        var ClientId = Ddl_ClientId.SelectedValue;
                                        Date = DateTime.Parse(Txt_Month_Selected_Salary.Text, CultureInfo.GetCultureInfo("en-gb")).ToString();
                                        var Month = DateTime.Parse(Date).Month.ToString() + DateTime.Parse(Date).Year.ToString().Substring(2, 2);

                                        var RemarksFromSalary = Txt_RemarksFromSalary.Text;
                                        Hashtable HtEmpids = new Hashtable();
                                        HtEmpids.Clear();

                                        Status = 0;
                                        HtEmpids.Add("@Voucherno", VoucherNo);
                                        HtEmpids.Add("@ClientId", ClientId);
                                        HtEmpids.Add("@Month", Month);
                                        HtEmpids.Add("@Empid", Empid);
                                        HtEmpids.Add("@Status", Status);

                                        HtEmpids.Add("@ActualAmount", NetAmt);
                                        HtEmpids.Add("@Remarks", RemarksFromSalary);
                                        //HtEmpids.Add("@ModifiedAmount",);

                                        int StatusTransaction =config.ExecuteNonQueryParamsAsync(ProcedureNameSalary, HtEmpids).Result;


                                        #endregion End Code For Declare & Assign  Variables  as on [07-02-2014]
                                    }

                                }

                            }

                        }
                    }
                    #endregion End Code For Check Validation as on [07-02-2014]


                    VocherIDAutoGenrate();
                    ClearData();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Record Not Added.');", true);
                }
                #endregion End code For Resulted Messages as on [13-01-2013]


            }
            catch (Exception ex)
            {

            }
        }

        private void ClearData()
        {
            Txt_DD_CheQue_Transaction_Date.Text = txtdate.Text = DateTime.Now.Date.ToString("MM/dd/yyyy");
            txtamount.Text = txtpaidto.Text = Txt_Remarks.Text = string.Empty;
            Ddl_Purpose.SelectedIndex = Ddl_Approved_By_Empids.SelectedIndex =
            Ddl_From_Bank.SelectedIndex = Ddl_Payment_Mode.SelectedIndex = 0;
            Txt_Dd_CheQue_N0.Text = "";



            Txt_Month_Selected_Salary.Visible = false;
            Lbl_Month.Visible = false;

            Lbl_Client_Id.Visible = false;
            Lbl_Client_Name.Visible = false;

            Ddl_ClientId.Visible = false;
            Ddl_Cname.Visible = false;

            Gv_salaryDetails_SiteWise.DataSource = null;
            Gv_salaryDetails_SiteWise.DataBind();


            Lbl_Modified_Amount.Visible = false;
            Txt_Modified_Amount.Text = "0";
            Txt_Modified_Amount.Visible = false;

            Lbl_Modified_Bank_Name.Visible = false;
            Ddl_Modified_Bank_Name.SelectedIndex = 0;

            Ddl_Modified_Bank_Name.Visible = false;



        }

        private void VocherIDAutoGenrate()
        {
            int vocherid = 0;
            string selectqueryclientid = "select  max(cast(Voucherno as int )) Voucherno from Expenses ";
            DataTable dt = config.ExecuteAdaptorAsyncWithQueryParams(selectqueryclientid).Result;
            if (dt.Rows.Count > 0)
            {
                if (String.IsNullOrEmpty(dt.Rows[0]["Voucherno"].ToString()) == false)
                {
                    vocherid = (Convert.ToInt32(dt.Rows[0]["Voucherno"].ToString())) + 1;
                    txtvocherno.Text = vocherid.ToString();
                }
                else
                {
                    txtvocherno.Text = "1";
                }
            }
            else
            {
                txtvocherno.Text = "1";
            }
        }

        protected void txtamount_TextChanged(object sender, EventArgs e)
        {

            #region  Begin code For Validation as on [16-01-2013]

            if (txtamount.Text.Trim().Length > 0)
            {
                if (Ddl_From_Bank.SelectedIndex == 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Please  You have to select the  FROM BANK.');", true);
                    return;
                }
            }

            #endregion End code For Validation as on [16-01-2013]
            int TestStatus = CheckBalanceinTheSelectedBank();
            if (TestStatus == 1)
            {
                return;
            }

        }

        protected void Ddl_Purpose_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (Ddl_Purpose.SelectedIndex > 0)
            {
                if (int.Parse(Ddl_Purpose.SelectedValue) == 4)
                {
                    Lbl_Month.Visible = true;
                    Txt_Month_Selected_Salary.Visible = true;

                    Lbl_Client_Id.Visible = true;
                    Lbl_Client_Name.Visible = true;

                    Ddl_ClientId.Visible = true;
                    Ddl_Cname.Visible = true;

                }
                else
                {
                    Gv_salaryDetails_SiteWise.DataSource = null;
                    Gv_salaryDetails_SiteWise.DataBind();

                    Txt_Month_Selected_Salary.Visible = false;
                    Lbl_Month.Visible = false;

                    Lbl_Client_Id.Visible = false;
                    Lbl_Client_Name.Visible = false;

                    Ddl_ClientId.Visible = false;
                    Ddl_Cname.Visible = false;
                }
            }
        }

        protected void Txt_Month_Selected_Salary_OnTextChanged(object sender, EventArgs e)
        {

            if (Ddl_ClientId.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Please Select the Cient Id/Name');", true);
                return;
            }


            if (Txt_Month_Selected_Salary.Text.Trim().Length > 0)
            {

                // string DtMonth = DateTime.Parse(Txt_Month_Selected_Salary.Text).ToString("MM/dd/yyyy");
                string DtMonth = DateTime.Parse(Txt_Month_Selected_Salary.Text, CultureInfo.GetCultureInfo("en-gb")).ToString();

                string Month = DateTime.Parse(DtMonth).Month.ToString() + DateTime.Parse(DtMonth).Year.ToString().Substring(2, 2);
                LoadEmployeesDAtaSiteWise(Month, Ddl_ClientId.SelectedValue);
            }
            else
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Please  Select The Month');", true);
                return;

            }
        }

        protected void Ddl_ClientId_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (Ddl_ClientId.SelectedIndex > 0)
            {
                Ddl_Cname.SelectedValue = Ddl_ClientId.SelectedValue;
            }
            else
            {
                Ddl_Cname.SelectedIndex = 0;
                Txt_Month_Selected_Salary.Text = "";

            }
        }

        protected void Ddl_Cname_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (Ddl_Cname.SelectedIndex > 0)
            {
                Ddl_ClientId.SelectedValue = Ddl_Cname.SelectedValue;

            }
            else
            {
                Ddl_ClientId.SelectedIndex = 0;
                Txt_Month_Selected_Salary.Text = "";
            }
        }

        protected void LoadEmployeesDAtaSiteWise(string Month, string Clientid)
        {

            #region Begin Code For Retriving The Employee Salary Details
            var SPName = "PendingSalaryDetailsOrNewSalaryDetailsBasedONclientidANDmonth";
            Hashtable HTSpname = new Hashtable();
            HTSpname.Add("@Month", Month);
            HTSpname.Add("@ClientId", Clientid);
            DataTable DtNames =config.ExecuteAdaptorAsyncWithParams(SPName, HTSpname).Result;

            #endregion


            if (DtNames.Rows.Count > 0)
            {
                Gv_salaryDetails_SiteWise.DataSource = DtNames;
                Gv_salaryDetails_SiteWise.DataBind();
            }
            else
            {
                Gv_salaryDetails_SiteWise.DataSource = null;
                Gv_salaryDetails_SiteWise.DataBind();

                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "show alert('Details Not  available');", true);
            }
        }

        protected void Ddl_Voucher_No_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            int OperationType = 1;
            DataTable DtForVoucherNos = GlobalData.Instance.LoadExpensesVoucherNos(OperationType);

            if (Ddl_Voucher_No.SelectedIndex > 0)
            {





            }
        }

        protected void Btn_Modify_Voucher_Click(object sender, EventArgs e)
        {
            txtvocherno.Visible = false;
            Ddl_Voucher_No.Visible = true;

            Ddl_Modified_Bank_Name.Visible = true;
            Lbl_Modified_Bank_Name.Visible = true;


            Lbl_Modified_Amount.Visible = true;
            Txt_Modified_Amount.Visible = true;

            if (Ddl_Voucher_No.SelectedIndex == 0)
            {

                int OperationType = 0;
                DataTable DtForVoucherNos = GlobalData.Instance.LoadExpensesVoucherNos(OperationType);

                if (DtForVoucherNos.Rows.Count > 0)
                {
                    Ddl_Voucher_No.DataValueField = "Voucherno";
                    Ddl_Voucher_No.DataTextField = "Voucherno";
                    Ddl_Voucher_No.DataSource = DtForVoucherNos;
                    Ddl_Voucher_No.DataBind();

                    Ddl_Voucher_No.Items.Insert(0, "--Select-");
                }
                else
                {
                    Ddl_Voucher_No.Visible = false;
                    txtvocherno.Visible = true;

                    Ddl_Modified_Bank_Name.Visible = false;
                    Lbl_Modified_Bank_Name.Visible = false;


                    Lbl_Modified_Amount.Visible = false;
                    Txt_Modified_Amount.Visible = false;



                    ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Vouchers Are Not Avaialable');", true);
                    return;
                }


            }
        }

        protected void Btn_Search_Voucher_Click(object sender, EventArgs e)
        {

        }

        protected int CheckBalanceinTheSelectedBank()
        {


            #region  Begin code For Variable Declaration  as on [16-01-2013]
            var TestStatus = 0;

            var BankID = "0";
            var EnteredAmount = "0";
            Hashtable HtVoucher = new Hashtable();
            var SPName = "";
            DataTable DtVoucher = null;
            var Balance = 0.00;
            var ActualBalance = 0.00;
            #endregion End code For Variable Declaration as on [16-01-2013]


            #region  Begin code For Assing Values To the  Variable   as on [16-01-2013]
            BankID = Ddl_From_Bank.SelectedValue;
            EnteredAmount = txtamount.Text;

            if (Txt_Modified_Amount.Text.Trim().Length > 0 && float.Parse(Txt_Modified_Amount.Text) > 0)
            {
                EnteredAmount = Txt_Modified_Amount.Text;
            }

            if (Ddl_From_Bank.SelectedIndex > 0)
            {
                EnteredAmount = Txt_Modified_Amount.Text;
                BankID = Ddl_From_Bank.SelectedValue;
            }



            SPName = "CheckBalance";

            #endregion End code For Assing Values To the  Variable as on [16-01-2013]


            #region  Begin code For Assing Values To the  HT-Parameters   as on [16-01-2013]
            HtVoucher.Add("@BankId", BankID);
            HtVoucher.Add("@EnteredAmount", EnteredAmount);
            #endregion End code For Assing Values To the   HT-Parameters  as on [16-01-2013]

            #region  Begin code For Calling Stored Procedure   as on [16-01-2013]
            DtVoucher =config.ExecuteAdaptorAsyncWithParams(SPName, HtVoucher).Result;
            #endregion End code For  Calling Stored Procedure  as on [16-01-2013]

            #region Begin code For Resulted Messages as on [16-01-2013]
            if (DtVoucher.Rows.Count > 0)
            {
                Balance = double.Parse(DtVoucher.Rows[0]["TotalBalance"].ToString());
                ActualBalance = double.Parse(DtVoucher.Rows[0]["ActualBalance"].ToString());
                if (Balance <= 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('You  Have Not Have The Sufficient Amount From Your Selected Bank.  The Available Balance IS  :" + ActualBalance + " Rupees ');", true);
                    TestStatus = 1;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Transactions Not Avaialable  From Your Selected Bank.  The Available Balance IS  :" + ActualBalance + " Rupees ');", true);
                TestStatus = 1;
            }
            return TestStatus;

            #endregion End code For Resulted Messages as on [16-01-2013]


        }

        protected void chkboxSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ChkBoxHeader = (CheckBox)Gv_salaryDetails_SiteWise.HeaderRow.FindControl("chkboxSelectAll");
            foreach (GridViewRow row in Gv_salaryDetails_SiteWise.Rows)
            {
                CheckBox ChkBoxRows = (CheckBox)row.FindControl("chkEmp");
                if (ChkBoxHeader.Checked == true)
                {
                    ChkBoxRows.Checked = true;
                }
                else
                {
                    ChkBoxRows.Checked = false;
                }
            }
        }

        protected void LoadTop5Vouchers()
        {

            #region Begin Code For variable Declaration as on [10-02-2014]
            var SPName = "";
            var operationType = "0";
            Hashtable HTTop5Vouchers = new Hashtable();
            #endregion End Code For variable Declaration as on [10-02-2014]

            #region Begin Code For  Assign Values To the variable as on [10-02-2014]
            SPName = "ExpensesViewTop5VouchersAndSearchVoucher";
            HTTop5Vouchers.Add("@operationType", operationType);
            #endregion End Code For Assign Values To the variable  as on [10-02-2014]

            #region Begin Code For Call Stored Procedure As on [10-02-2014]
            DataTable DtTop5Vouchers =config.ExecuteAdaptorAsyncWithParams(SPName, HTTop5Vouchers).Result;
            #endregion End Code For Call Stored Procedure  As on [10-02-2014]

            #region Begin Code For Assign The Retrived Value to The Gridview  as on [10-02-2014]
            if (DtTop5Vouchers.Rows.Count > 0)
            {

                Gv_Last5_Transactions.DataSource = DtTop5Vouchers;
                Gv_Last5_Transactions.DataBind();
            }
            else
            {
                Gv_Last5_Transactions.DataSource = null;
                Gv_Last5_Transactions.DataBind();
            }
            #endregion  End Code For Assign The Retrived Value to  The Gridview as on [10-02-2014]


        }

        protected void Btn_Search_Voucher_purpose_Click(object sender, EventArgs e)
        {

            #region Begin Code For variable Declaration as on [10-02-2014]
            var SPName = "";
            var operationType = "0";
            Hashtable HTTop5Vouchers = new Hashtable();
            #endregion End Code For variable Declaration as on [10-02-2014]

            #region Begin Code For  Assign Values To the variable as on [10-02-2014]
            SPName = "ExpensesViewTop5VouchersAndSearchVoucher";

            if (Ddl_Search_Mode.SelectedIndex == 0)
            {
                operationType = "1";
                HTTop5Vouchers.Add("@operationType", operationType);
                HTTop5Vouchers.Add("@VoucherNo", Txt_Search_Mode.Text);
            }

            if (Ddl_Search_Mode.SelectedIndex == 1)
            {
                operationType = "2";
                HTTop5Vouchers.Add("@operationType", operationType);
                HTTop5Vouchers.Add("@PurposeType", Txt_Search_Mode.Text);
            }

            #endregion End Code For Assign Values To the variable  as on [10-02-2014]

            #region Begin Code For Call Stored Procedure As on [10-02-2014]
            DataTable DtTop5Vouchers =config.ExecuteAdaptorAsyncWithParams(SPName, HTTop5Vouchers).Result;
            #endregion End Code For Call Stored Procedure  As on [10-02-2014]

            #region Begin Code For Assign The Retrived Value to The Gridview  as on [10-02-2014]
            if (DtTop5Vouchers.Rows.Count > 0)
            {

                Gv_Last5_Transactions.DataSource = DtTop5Vouchers;
                Gv_Last5_Transactions.DataBind();
            }
            else
            {
                Gv_Last5_Transactions.DataSource = null;
                Gv_Last5_Transactions.DataBind();
            }
            #endregion  End Code For Assign The Retrived Value to  The Gridview as on [10-02-2014]

        }

        protected void Lbl_View_More_OnClick(object sender, EventArgs e)
        {
            GridViewRow Gv_Row_View_More = (GridViewRow)((LinkButton)sender).NamingContainer;
            string Voucher_Id = ((Label)Gv_Row_View_More.FindControl("Lbl_VoucherNo")).Text;

            #region Begin Code For Declare Variables as on [10-02-2014]
            var SPName = "";
            Hashtable Ht_View_More_Salary_Details = new Hashtable();

            #endregion End Code For Declare Variables as on [10-02-2014]

            #region Begin Code For Assign Values To the  Variables as on [10-02-2014]
            SPName = "Expenses_View_More_Salary_Details";
            Ht_View_More_Salary_Details.Add("@voucherno", Voucher_Id);
            #endregion End Code For Assign Values To the  Variables as on [10-02-2014]

            #region Begin Code For Calling Stored Procedure as on [11-02-2014]
            DataTable Dt_View_More_Salary_Details = config.ExecuteAdaptorAsyncWithParams(SPName, Ht_View_More_Salary_Details).Result;
            #endregion End Code For Calling Stored Procedure as on [11-02-2014]

            #region Begin Code For As Retriving Data  and Assign the Values to the Gridview as on [11-02-2014]
            if (Dt_View_More_Salary_Details.Rows.Count > 0)
            {
                Gv_View_More_Details_Salary_DEtails.DataSource = Dt_View_More_Salary_Details;
                Gv_View_More_Details_Salary_DEtails.DataBind();
            }
            else
            {
                Gv_View_More_Details_Salary_DEtails.DataSource = null;
                Gv_View_More_Details_Salary_DEtails.DataBind();
                ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Salary Details Are Not Avaialable');", true);
            }
            #endregion  End Code For As Retriving Data and Assign the Values to the Gridview as on [11-02-2014]

            this.ccl.Show();
        }

        protected void Link_Modify_OnClick(object sender, EventArgs e)
        {
            GridViewRow Gv_Row_View_More = (GridViewRow)((LinkButton)sender).NamingContainer;
            string Voucher_Id = ((Label)Gv_Row_View_More.FindControl("Lbl_VoucherNo")).Text;

            Gv_Last5_Transactions.DataSource = null;
            Gv_Last5_Transactions.DataBind();
            Gv_View_More_Details_Salary_DEtails.DataSource = null;
            Gv_View_More_Details_Salary_DEtails.DataBind();

            #region Begin Code For Declare Variables as on [10-02-2014]
            var SPName = "";
            var operationType = "1";
            Hashtable Ht_Retrive_Voucher_Details_For_Modify = new Hashtable();

            #endregion End Code For Declare Variables as on [10-02-2014]

            #region Begin Code For Assign Values To the  Variables as on [10-02-2014]
            SPName = "ExpensesViewTop5VouchersAndSearchVoucher";
            Ht_Retrive_Voucher_Details_For_Modify.Add("@voucherno", Voucher_Id);
            Ht_Retrive_Voucher_Details_For_Modify.Add("@operationType", operationType);

            #endregion End Code For Assign Values To the  Variables as on [10-02-2014]

            #region Begin Code For Calling Stored Procedure as on [11-02-2014]
            DataTable Dt_Retrive_Voucher_Details_For_Modify =config.ExecuteAdaptorAsyncWithParams(SPName, Ht_Retrive_Voucher_Details_For_Modify).Result;
            #endregion End Code For Calling Stored Procedure as on [11-02-2014]

            #region Begin Code For As Retriving Data  and Assign the Values to the Gridview as on [11-02-2014]
            if (Dt_Retrive_Voucher_Details_For_Modify.Rows.Count > 0)
            {
                txtvocherno.Text = Dt_Retrive_Voucher_Details_For_Modify.Rows[0]["voucherno"].ToString();   //Voucher No
                txtpaidto.Text = Dt_Retrive_Voucher_Details_For_Modify.Rows[0]["paidto"].ToString();     //Pait To
                txtdate.Text = Dt_Retrive_Voucher_Details_For_Modify.Rows[0]["date"].ToString();       //Voucher Entered Date
                Ddl_Purpose.SelectedValue = Dt_Retrive_Voucher_Details_For_Modify.Rows[0]["PurposeId"].ToString(); //Expenses Purpose ID

                Ddl_Payment_Mode.SelectedValue = Dt_Retrive_Voucher_Details_For_Modify.Rows[0]["PaymentModeId"].ToString(); //Payment Mode i.e Cheque/DD/Online Transaction
                Txt_DD_CheQue_Transaction_Date.Text = Dt_Retrive_Voucher_Details_For_Modify.Rows[0]["DDorchequeortransactionDate"].ToString(); //DD/Cheque/Transaction Id
                Txt_Dd_CheQue_N0.Text = Dt_Retrive_Voucher_Details_For_Modify.Rows[0]["DDorchequeortransactionNo"].ToString(); // DD/cheque/Transaction No
                Ddl_From_Bank.SelectedValue = Dt_Retrive_Voucher_Details_For_Modify.Rows[0]["FromBank"].ToString();   // Actual Bank Which is alredy Withdraw amount

                Ddl_Approved_By_Empids.SelectedValue = Dt_Retrive_Voucher_Details_For_Modify.Rows[0]["ApprovedBy"].ToString(); // Who Approve the Voucher [ Empid/Name]
                //Ddl_Modified_Bank_Name.SelectedValue = Dt_Retrive_Voucher_Details_For_Modify.Rows[0]["FromBank"].ToString();  // Non-Operational Amount Should Deposit to Other Bank For Make a another Transaction

                txtamount.Text = Dt_Retrive_Voucher_Details_For_Modify.Rows[0]["Amount"].ToString();//Actual Amount or Total Amount For This Voucher
                Txt_Remarks.Text = Dt_Retrive_Voucher_Details_For_Modify.Rows[0]["Remarks"].ToString();//Remarks of the Voucher
                Txt_Modified_Amount.Visible = true;
                Ddl_Modified_Bank_Name.Visible = true;

                Lbl_Modified_Amount.Visible = true;
                Lbl_Modified_Bank_Name.Visible = true;


                //Txt_Modified_Amount.Text = Dt_Retrive_Voucher_Details_For_Modify.Rows[0][""].ToString();//Modified Voucher Amount

                if (Ddl_Purpose.SelectedValue == "4")
                {

                    #region Begin Code For Calling Stored Procedure To retrive Salary Details Based on the Month and Client Id as on [11-02-2014]
                    var SPName_For_Salary_Details = "Expenses_Retrive_Salary_Details_By_VoucherNo";
                    Hashtable Ht_For_Salary_Details = new Hashtable();
                    Ht_For_Salary_Details.Add("@VoucherNo", Voucher_Id);
                    DataTable Dt_For_Salary_Details =config.ExecuteAdaptorAsyncWithParams(SPName_For_Salary_Details, Ht_For_Salary_Details).Result;
                    #endregion  End Code For Calling Stored Procedure To retrive Salary Details Based on the Month and Client Id as on [11-02-2014]


                    Lbl_Client_Id.Visible = true;
                    lblcname.Visible = true;
                    Ddl_ClientId.Visible = true;
                    Ddl_Cname.Visible = true;
                    Lbl_Month.Visible = true;
                    Txt_Month_Selected_Salary.Visible = true;
                    Txt_Month_Selected_Salary.Enabled = false;


                    Ddl_ClientId.Enabled = false;
                    Ddl_Cname.Enabled = false;
                    txtamount.Enabled = false;
                    Ddl_Purpose.Enabled = false;
                    Ddl_Approved_By_Empids.Enabled = false;
                    Ddl_From_Bank.Enabled = false;
                    Ddl_ClientId.SelectedValue = Dt_For_Salary_Details.Rows[0]["clientid"].ToString(); // Assign the Client Id When The purpose is Salary Details
                    Ddl_Cname.SelectedValue = Dt_For_Salary_Details.Rows[0]["clientid"].ToString(); // Assign the Client Name When The purpose is Salary Details
                    Txt_Month_Selected_Salary.Text = LoadDateBasedOnTheMonth(Dt_For_Salary_Details.Rows[0]["Month"].ToString()); // Month Of The Salary


                    Gv_salaryDetails_SiteWise.DataSource = Dt_For_Salary_Details;
                    Gv_salaryDetails_SiteWise.DataBind();

                }
                else
                {
                    Lbl_Client_Id.Visible = false;
                    lblcname.Visible = false;
                    Ddl_ClientId.Visible = false;
                    Ddl_Cname.Visible = false;
                    Lbl_Month.Visible = false;
                    Txt_Month_Selected_Salary.Visible = false;
                    Txt_Month_Selected_Salary.Enabled = true;
                    Gv_salaryDetails_SiteWise.DataSource = null;
                    Gv_salaryDetails_SiteWise.DataBind();

                    Ddl_ClientId.Enabled = true;
                    Ddl_Cname.Enabled = true;
                    txtamount.Enabled = true;
                    Ddl_Purpose.Enabled = true;

                    Ddl_Approved_By_Empids.Enabled = true;
                    Ddl_From_Bank.Enabled = true;
                }
            }
            else
            {

                Lbl_Client_Id.Visible = false;
                lblcname.Visible = false;
                Ddl_ClientId.Visible = false;
                Ddl_Cname.Visible = false;


                Lbl_Month.Visible = false;
                Txt_Month_Selected_Salary.Visible = false;
                Txt_Month_Selected_Salary.Enabled = true;
                Gv_salaryDetails_SiteWise.DataSource = null;
                Gv_salaryDetails_SiteWise.DataBind();

                Ddl_ClientId.Enabled = true;
                Ddl_Cname.Enabled = true;
                txtamount.Enabled = true;
                Ddl_Purpose.Enabled = true;
                Ddl_Approved_By_Empids.Enabled = true;
                Ddl_From_Bank.Enabled = true;
                Txt_Modified_Amount.Visible = false;
                Ddl_Modified_Bank_Name.Visible = false;
                Ddl_Modified_Bank_Name.SelectedIndex = 0;


                Lbl_Modified_Amount.Visible = false;
                Lbl_Modified_Bank_Name.Visible = false;


                ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Salary Details Are Not Avaialable');", true);
            }
            #endregion  End Code For As Retriving Data and Assign the Values to the Gridview as on [11-02-2014]

        }

        protected void Btn_New_Voucher_Click(object sender, EventArgs e)
        {
            ClearData();
            VocherIDAutoGenrate();
            LoadTop5Vouchers();
        }

        public string LoadDateBasedOnTheMonth(string Month)
        {
            string TargetDate = "";
            string TargetMonth = "";
            string TargetYear = "";
            if (Month.Length == 3)
            {
                TargetMonth = Month.Substring(0, 1);
                TargetYear = Month.Substring(1, 2);
            }

            if (Month.Length == 4)
            {
                TargetMonth = Month.Substring(0, 2);
                TargetYear = Month.Substring(2, 2);
            }


            switch (TargetMonth)
            {
                case "1": TargetDate = "01/01";
                    break;
                case "2": TargetDate = "01/02";
                    break;
                case "3": TargetDate = "01/03";
                    break;
                case "4": TargetDate = "01/04";
                    break;
                case "5": TargetDate = "01/05";
                    break;

                case "6": TargetDate = "01/06";
                    break;
                case "7": TargetDate = "01/07";
                    break;
                case "8": TargetDate = "01/08";
                    break;
                case "9": TargetDate = "01/09";
                    break;
                case "10": TargetDate = "01/10";
                    break;

                case "11": TargetDate = "01/11";
                    break;
                case "12": TargetDate = "01/12";
                    break;
            }




            return TargetDate + "/20" + TargetYear;
        }
    }
}