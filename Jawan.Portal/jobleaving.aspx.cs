using System;
using System.Collections;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data;
using System.Data.SqlClient;
using KLTS.Data;
using System.IO;
using System.Collections.Generic;
using Jawan.Portal.DAL;
namespace Jawan.Portal
{
    public partial class jobleaving : System.Web.UI.Page
    {
        AppConfiguration config = new AppConfiguration();
        GridViewExportUtil gve = new GridViewExportUtil();
        DataTable dt;
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
                    lblDisplayUser.Text = Session["UserId"].ToString();
                    PreviligeUsers(Convert.ToInt32(Session["AccessLevel"]));
                    lblcname.Text = SqlHelper.Instance.GetCompanyname();
                }
                else
                {
                    Response.Redirect("login.aspx");
                }

                string selectquery = "Select EmpID from EmpDetails Order By cast(substring(Empid," + Elength + ", 6) as int)";

                DataTable dt = config.ExecuteAdaptorAsyncWithQueryParams(selectquery).Result;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ddlEmpId.Items.Add(dt.Rows[i][0].ToString());
                }

            }
            details();
            GvJobLeving.Visible = true;
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
                    AddEmployeeLink.Visible = true;
                    ModifyEmployeeLink.Visible = true;
                    DeleteEmployeeLink.Visible = true;
                    AssigningWorkerLink.Visible = true;
                    AttendanceLink.Visible = true;
                    LoanLink.Visible = false;
                    PaymentLink.Visible = false;
                    //TrainingEmployeeLink.Visible = true;
                    PostingOrderListLink.Visible = true;
                    //JobLeavingReasonsLink.Visible = true;

                    ClientsLink.Visible = true;
                    CompanyInfoLink.Visible = false;
                    InventoryLink.Visible = false;
                    ReportsLink.Visible = true;
                    SettingsLink.Visible = true;
                    break;

                case 3:

                    AddEmployeeLink.Visible = true;
                    ModifyEmployeeLink.Visible = true;
                    DeleteEmployeeLink.Visible = true;
                    AssigningWorkerLink.Visible = true;
                    LoanLink.Visible = true;
                    PaymentLink.Visible = true;
                    //TrainingEmployeeLink.Visible = true;
                    //JobLeavingReasonsLink.Visible = true;
                    AttendanceLink.Visible = true;

                    CompanyInfoLink.Visible = true;
                    ReportsLink.Visible = false;
                    InventoryLink.Visible = false;
                    SettingsLink.Visible = false;
                    break;

                case 4:

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
        public void details()
        {
            DataTable Reasons = new DataTable();
            DataRow[] dr1 = new DataRow[9];
            Reasons.Columns.Add(new DataColumn("SNo", typeof(string)));
            Reasons.Columns.Add(new DataColumn("REASONS", typeof(string)));
            Reasons.Columns.Add(new DataColumn("YES", typeof(string)));
            Reasons.Columns.Add(new DataColumn("NO", typeof(string)));
            dr1[0] = Reasons.NewRow();
            //Reasons.Rows.Add(dr);
            for (int index = 0; index < 8; index++)
            {
                dr1[index + 1] = Reasons.NewRow();


                dr1[index + 1]["SNo"] = (index + 1).ToString();
                dr1[index + 1]["REASONS"] = "";
                dr1[index + 1]["YES"] = index.ToString();
                dr1[index + 1]["NO"] = index.ToString();
                Reasons.Rows.Add(dr1[index + 1]);

            }
            Reasons.Rows[0][1] = "Heavy Work Load";
            Reasons.Rows[1][1] = "Hassle Because of BCS & PC Clearance";
            Reasons.Rows[2][1] = "Personal Reason";
            Reasons.Rows[3][1] = "Unsatisfactory duty allocation";
            Reasons.Rows[4][1] = "Unsatisfactory Salary";
            Reasons.Rows[5][1] = "Problems with the Supervisor/Manager";
            Reasons.Rows[6][1] = "Shift related problems";
            Reasons.Rows[7][1] = "Not clearly informed about the duty before joining";
            GvJobLeving.DataSource = Reasons;
            GvJobLeving.DataBind();
        }
        protected void ddlEmpId_SelectedIndexChanged(object sender, EventArgs e)
        {
            string SelectQuery1 = "Select  * From EmpDetails where EmpId='" + ddlEmpId.SelectedItem.Text + "'";
            DataTable dt = config.ExecuteAdaptorAsyncWithQueryParams(SelectQuery1).Result;
            if (dt.Rows.Count > 0)
            {

                ddlEmpId.SelectedItem.Text = dt.Rows[0]["EmpId"].ToString();
                txtEmpName.Text = dt.Rows[0]["EmpmName"].ToString();
                txtDtOfJoining.Text = dt.Rows[0]["EmpDtofJoining"].ToString();
                txtDtOfLeaving.Text = dt.Rows[0]["EmpDtofLeaving"].ToString();
                ddlDesignation.SelectedItem.Text = dt.Rows[0]["EmpDesgn"].ToString();

            }
        }
        protected void GvJobLeving_RowDataBound(object sender, GridViewRowEventArgs e)
        {
        }
        protected void Btn_JobLeaving_Resons_Click(object sender, EventArgs e)
        {
            int[] Reason = new int[9];
            int ReasonIndex = 0;
            for (ReasonIndex = 0; ReasonIndex < Reason.Length; ReasonIndex++)
            {
                RadioButton RbChecked1 = GvJobLeving.Rows[Reason[ReasonIndex]].FindControl("rbYes") as RadioButton;
                if (RbChecked1.Checked == true)
                {
                    Reason[ReasonIndex] = 1;
                }
                else
                {
                    Reason[ReasonIndex] = 0;
                }
            }
            string InsertResonsQuery = string.Format("Insert Into EmpExitInterview values( '{0}',{1},{2},{3},{4},{5},{6},{7},{8},'{9}')", ddlEmpId.SelectedItem.ToString(), Reason[0], Reason[1], Reason[2], Reason[3], Reason[4], Reason[5], Reason[6], Reason[7], txtRemarks.Text);
            int status = config.ExecuteNonQueryWithQueryAsync(InsertResonsQuery).Result;
            LblResult.Visible = true;
            if (status != 0)
            {
                LblResult.Text = "Record Inserted Successfully";
            }
            else
            {
                LblResult.Text = "Record Not Inserted";

            }





        }
    }
}