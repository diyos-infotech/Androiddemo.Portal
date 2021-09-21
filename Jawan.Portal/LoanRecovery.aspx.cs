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
    public partial class LoanRecovery : System.Web.UI.Page
    {
        DataTable dt;
        AppConfiguration config = new AppConfiguration();
        GridViewExportUtil gve = new GridViewExportUtil();
        string EmpIDPrefix = "";
        string BranchID = "";
        string CmpIDPrefix = "";

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

        protected void gvdesignation_RowEditing(object sender, GridViewEditEventArgs e)
        {


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

        protected void ClearData()
        {
            ddlEmpId.SelectedIndex = 0;
            // ddlempmname.SelectedIndex = 0;
            ddlfname.SelectedIndex = 0;
            ddlLoanTaken.Items.Clear();
            ddlLoanTaken.Items.Insert(0, "--Select--");
            gvRecovery.DataSource = null;
            gvRecovery.DataBind();

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
            if (ddlEmpId.SelectedIndex > 0)
            {

                string selectquery4 = "Select LoanNo from EmpLoanMaster where EmpId='" + ddlEmpId.SelectedItem.ToString().Trim() + "'";
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
            if (ddlLoanTaken.SelectedIndex > 0)
            {
                string selectquery = "Select loandt as loandt,LoanAmount from EmpLoanMaster where EmpId='" + ddlEmpId.SelectedValue + "' AND loanNo = '" + ddlLoanTaken.SelectedItem.ToString() + "'";
                DataTable data = config.ExecuteAdaptorAsyncWithQueryParams(selectquery).Result;
                if (data.Rows.Count > 0)
                {
                    string loandate = data.Rows[0]["LoanDt"].ToString();
                    txtStartingDate.Text = Convert.ToDateTime(loandate).ToString("dd/MM/yyyy");
                    txtLoanAmount.Text = data.Rows[0]["loanamount"].ToString();
                }
                selectquery = " select TransactionDt  as loandt,RecAmt From EmpLoanDetails where loanNo = '" + ddlLoanTaken.SelectedItem.ToString() + "'";
                dt = config.ExecuteAdaptorAsyncWithQueryParams(selectquery).Result;
                gvRecovery.DataSource = dt;
                gvRecovery.DataBind();
            }
        }
    }
}