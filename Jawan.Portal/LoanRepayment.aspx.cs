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
    public partial class LoanRepayment : System.Web.UI.Page
    {
        AppConfiguration config = new AppConfiguration();
        GridViewExportUtil gve = new GridViewExportUtil();
        DataTable dt;
        string EmpIDPrefix = "";
        string BranchID = "";
        string CmpIDPrefix = "";
        string Elength = "";
        string Clength = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                GetWebConfigdata();
                if (!IsPostBack)
                {
                    if (Session["UserId"] != null && Session["AccessLevel"] != null)
                    {
                        PreviligeUsers(Convert.ToInt32(Session["AccessLevel"]));
                        lblDisplayUser.Text = Session["UserId"].ToString();
                        lblcname.Text = SqlHelper.Instance.GetCompanyname();
                    }
                    else
                    {
                        Response.Redirect("login.aspx");
                    }
                    FillEmpList();
                    FillEmpfnames();
                    ddlLoanTaken.Items.Insert(0, "--Select--");
                }
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

                    break;

                case 3:
                    break;

                case 4:
                    break;
                case 5:

                    break;
                default:
                    break;
            }
        }

        protected void GetWebConfigdata()
        {

            EmpIDPrefix = Session["EmpIDPrefix"].ToString();
            Elength = (EmpIDPrefix.Trim().Length + 1).ToString();
            CmpIDPrefix = Session["CmpIDPrefix"].ToString();
            Clength = (CmpIDPrefix.Trim().Length + 1).ToString();
            BranchID = Session["BranchID"].ToString();
        }

        protected void ClearData()
        {
            ddlEmpId.SelectedIndex = 0;
            ddlempmname.SelectedIndex = 0;
            ddlfname.SelectedIndex = 0;
            gvRecovery.DataSource = null;
            gvRecovery.DataBind();
            ddlLoanTaken.Items.Clear();
            ddlLoanTaken.Items.Insert(0, "--Select--");
            txtStartingDate.Text = "";
            txtLoanAmount.Text = "";
            txtRemAmount.Text = "";
            txtPayingAmount.Text = "";
            lblresult.Text = "";
        }
        protected void FillEmpList()
        {
            DataTable DtEmpIds = GlobalData.Instance.LoadEmpIds(EmpIDPrefix,BranchID);
            if (DtEmpIds.Rows.Count > 0)
            {
                ddlEmpId.DataValueField = "empid";
                ddlEmpId.DataTextField = "empid";
                ddlEmpId.DataSource = DtEmpIds;
                ddlEmpId.DataBind();
            }
            ddlEmpId.Items.Insert(0, "-Select-");
        }

        protected void FillEmpfnames()
        {
            DataTable DtEmpNames = GlobalData.Instance.LoadEmpNames(EmpIDPrefix,BranchID);
            if (DtEmpNames.Rows.Count > 0)
            {
                ddlfname.DataValueField = "empid";
                ddlfname.DataTextField = "FullName";
                ddlfname.DataSource = DtEmpNames;
                ddlfname.DataBind();
            }
            ddlfname.Items.Insert(0, "-Select-");
        }

        protected void GetEmpname()
        {
            if (ddlEmpId.SelectedIndex > 0)
            {
                ddlfname.SelectedValue = ddlEmpId.SelectedValue;
            }
            else
            {
                ddlfname.SelectedIndex = 0;
            }
        }

        protected void GetEmpid()
        {
            if (ddlfname.SelectedIndex > 0)
            {
                ddlEmpId.SelectedValue = ddlfname.SelectedValue;
            }
            else
            {
                ddlEmpId.SelectedIndex = 0;
            }
        }

        protected void gvdesignation_RowEditing(object sender, GridViewEditEventArgs e)
        {
        }

        protected void ddlEmpId_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddlEmpId.SelectedIndex > 0)
            {
                GetEmpname();
                GetEmployeeLoanDetails();
            }

            else
            {
                ClearData();
            }

        }

        protected void ddlempmname_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlfname_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlfname.SelectedIndex > 0)
            {
                GetEmpid();
                GetEmployeeLoanDetails();

            }

            else
            {
                ClearData();
            }
        }

        protected void GetEmployeeLoanDetails()
        {
            txtStartingDate.Text = "";
            txtLoanAmount.Text = "";
            txtRemAmount.Text = "";
            txtPayingAmount.Text = "";
            lblresult.Text = "";
            if (ddlEmpId.SelectedIndex > 0)
            {
                string selectquery4 = "Select LoanNo from EmpLoanMaster where EmpId='" + ddlEmpId.SelectedItem.ToString().Trim() +
                    "' Order By (cast(LoanNo as int))";
                ddlLoanTaken.Items.Clear();
                ddlLoanTaken.Items.Add("--Select--");
                DataTable dt4 = config.ExecuteAdaptorAsyncWithQueryParams(selectquery4).Result;
                for (int i = 0; i < dt4.Rows.Count; i++)
                {
                    ddlLoanTaken.Items.Add(dt4.Rows[i]["LoanNo"].ToString());
                }
            }
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtStartingDate.Text = "";
            txtLoanAmount.Text = "";
            txtRemAmount.Text = "";
            txtPayingAmount.Text = "";
            lblresult.Text = "";
            DisplayData();
            //if (ddlLoanTaken.SelectedIndex > 0)
            //{
            //    string selectquery = "Select CONVERT(VARCHAR(10),loandt,111) as loandt,LoanAmount from EmpLoanMaster where EmpId='" + ddlEmpId.SelectedValue + "' AND loanNo = '" + ddlLoanTaken.SelectedItem.ToString() + "'";
            //    DataTable data = SqlHelper.Instance.GetTableByQuery(selectquery);
            //    if (data.Rows.Count > 0)
            //    {
            //        string loandate = data.Rows[0]["LoanDt"].ToString();
            //        txtStartingDate.Text = Convert.ToDateTime(loandate).ToShortDateString();
            //        txtLoanAmount.Text = data.Rows[0]["loanamount"].ToString();
            //        float loanAmount = 0;
            //        if(txtLoanAmount.Text.Trim().Length>0)
            //        {
            //            loanAmount = Convert.ToSingle(txtLoanAmount.Text);
            //        }
            //        selectquery = " select CONVERT(VARCHAR(10),TransactionDt,111) as loandt,RecAmt From EmpLoanDetails where loanNo = '" + ddlLoanTaken.SelectedItem.ToString() + "'";
            //        dt = SqlHelper.Instance.GetTableByQuery(selectquery);
            //        gvRecovery.DataSource = dt;
            //        gvRecovery.DataBind();
            //        float RecAmount = 0;
            //        if (dt.Rows.Count > 0)
            //        {
            //            for(int i=0;i<dt.Rows.Count;i++)
            //            {
            //                string strAmount = dt.Rows[i]["RecAmt"].ToString();
            //                if (strAmount.Trim().Length > 0)
            //                {
            //                    float amount = Convert.ToSingle(strAmount);
            //                    RecAmount += amount;
            //                }
            //            }
            //        }
            //        if (loanAmount - RecAmount >= 0)
            //            txtRemAmount.Text = (loanAmount - RecAmount).ToString("0.00");
            //        else
            //            txtRemAmount.Text = "0";
            //    }
            //}
        }

        protected void DisplayData()
        {
            if (ddlLoanTaken.SelectedIndex > 0)
            {
                string selectquery = "Select CONVERT(VARCHAR(10),loandt,111) as loandt,LoanAmount from EmpLoanMaster where EmpId='" + ddlEmpId.SelectedValue + "' AND loanNo = '" + ddlLoanTaken.SelectedItem.ToString() + "'";
                DataTable data = config.ExecuteAdaptorAsyncWithQueryParams(selectquery).Result;
                if (data.Rows.Count > 0)
                {
                    string loandate = data.Rows[0]["LoanDt"].ToString();
                    txtStartingDate.Text = Convert.ToDateTime(loandate).ToString("dd/MM/yyyy");
                    txtLoanAmount.Text = data.Rows[0]["loanamount"].ToString();
                    float loanAmount = 0;
                    if (txtLoanAmount.Text.Trim().Length > 0)
                    {
                        loanAmount = Convert.ToSingle(txtLoanAmount.Text);
                    }
                    selectquery = " select TransactionDt as loandt,RecAmt From EmpLoanDetails where loanNo = '" + ddlLoanTaken.SelectedItem.ToString() + "'";
                    dt = config.ExecuteAdaptorAsyncWithQueryParams(selectquery).Result;
                    gvRecovery.DataSource = dt;
                    gvRecovery.DataBind();
                    float RecAmount = 0;
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            string strAmount = dt.Rows[i]["RecAmt"].ToString();
                            if (strAmount.Trim().Length > 0)
                            {
                                float amount = Convert.ToSingle(strAmount);
                                RecAmount += amount;
                            }
                        }
                    }
                    if (loanAmount - RecAmount >= 0)
                        txtRemAmount.Text = (loanAmount - RecAmount).ToString("0.00");
                    else
                        txtRemAmount.Text = "0";
                }
            }
        }

        protected void btnPay_Click(object sender, EventArgs e)
        {
            if (ddlEmpId.SelectedIndex <= 0)
            {
                lblresult.Text = "Select Employee Id";
                return;
            }
            if (ddlLoanTaken.SelectedIndex <= 0)
            {
                lblresult.Text = "Select Loan ID";
                return;
            }
            float RemAmount = 0;
            if (txtRemAmount.Text.Length > 0)
            {
                RemAmount = Convert.ToSingle(txtRemAmount.Text);
                if (RemAmount <= 0)
                {
                    lblresult.Text = "Loan Amount is already recovered";
                    DisplayData();
                    return;
                }
            }
            if (txtPayingAmount.Text.Trim().Length > 0)
            {
                float amount = Convert.ToSingle(txtPayingAmount.Text.Trim());
                if (amount > 0)
                {
                    string date = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString();
                    string strQry = "Insert into EmpLoanDetails(LoanNo,TransactionDt,RecAmt,PaymentType) values('" + ddlLoanTaken.SelectedValue + "','" +
                        date + "'," + amount + ", 1)";
                    int status =config.ExecuteNonQueryWithQueryAsync(strQry).Result;
                    if (status != 0)
                    {
                        lblresult.Text = "Loan Repaid successfully";
                    }
                    else
                    {
                        //lblresult.Text = "Problem while repayment";
                        lblresult.Text = "Loan repayment not done";

                    }

                    if (amount >= RemAmount)
                    {
                        strQry = "update EmpLoanMaster set LoanStatus = 1 where LoanNo='" + ddlLoanTaken.SelectedValue + "'";
                        status = config.ExecuteNonQueryWithQueryAsync(strQry).Result;
                    }
                }
                DisplayData();
            }
        }
    }
}