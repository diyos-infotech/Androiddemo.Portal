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
using System.Globalization;
using System.Data.SqlClient;
using KLTS.Data;
using Jawan.Portal.DAL;
namespace Jawan.Portal
{
    public partial class ModifyLoan : System.Web.UI.Page
    {
        AppConfiguration config = new AppConfiguration();
        GridViewExportUtil gve = new GridViewExportUtil();

        string EmpIDPrefix = "";
        string CmpIDPrefix = "";
        string UserID = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            // lblpayment.Text = "";

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
            UserID = Session["UserId"].ToString();
        }

        protected void Fillempname()
        {
            string SqlQryForCname = "Select ed.EmpId,d.Design as EmpDesgn,(empfname+' '+empmname+' '+emplname) as Name from empdetails ed inner join Designations d on ed.EmpDesgn=d.DesignId  where empid='" + txtEmpid.Text + "'";
            DataTable dtCname = config.ExecuteAdaptorAsyncWithQueryParams(SqlQryForCname).Result;
            if (dtCname.Rows.Count > 0)
            {
                txtName.Text = dtCname.Rows[0]["Name"].ToString();
                txtdesignation.Text = dtCname.Rows[0]["EmpDesgn"].ToString();

            }
            else
            {

            }

        }

        protected void Fillempid()
        {
            string SqlQryForCid = "Select ed.EmpId,d.Design as EmpDesgn from empdetails ed inner join Designations d on ed.EmpDesgn=d.DesignId where (empfname+' '+empmname+' '+emplname)  like '" + txtName.Text + "'";
            DataTable dtCname = config.ExecuteAdaptorAsyncWithQueryParams(SqlQryForCid).Result;
            if (dtCname.Rows.Count > 0)
            {
                txtEmpid.Text = dtCname.Rows[0]["empid"].ToString();
                txtdesignation.Text = dtCname.Rows[0]["EmpDesgn"].ToString();
            }
        }


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

        //protected void getempdetails()
        //{

        //    string month = DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString().Substring(2, 2);

        //    string strQry = "Select DISTINCT Ea.EmpId,Epo.Desgn as Design , " +
        //                   " Ed.empfname + Ed.empmname +  Ed.Emplname  as name,EL.Loanamount,ELD.Recamt ,(EL.Loanamount-ELD.Recamt) as dueamt, "+
        //                   "  EL.loanno, round(EL.Loanamount/EL.Noinstalments,0)  as  instamt,EL.Noinstalments " +
        //                   " from EmpAttendance as Ea INNER JOIN EmpDetails as Ed ON Ea.EmpId=Ed.EmpId  "+
        //                   " inner join Emploanmaster EL  on EL.Empid=Ea.EmpId   inner join EmpLoanDetails ELD  on EL.loanno=ELD.loanno  " +
        //                   " Inner join Emppostingorder as  Epo on Ed.Empid=Epo.Empid And Epo.Tounitid='" + ddlempid.SelectedValue + "'" +
        //                   " AND Ea.ClientId = '" + ddlempid.SelectedValue + "' and  EL.loanstatus=0   AND Ea.Month=" + month;
        //    DataTable empList = SqlHelper.Instance.GetTableByQuery(strQry);

        //    if (empList.Rows.Count > 0)
        //    {
        //        gvNewLoan.DataSource = empList;
        //        gvNewLoan.DataBind();
        //    }
        //    else
        //    {
        //        gvNewLoan.DataSource = null;
        //        gvNewLoan.DataBind();
        //        ScriptManager.RegisterStartupScript(this, GetType(), "showlalert", "alert('There is no employees for this client');", true);

        //    }

        //}

        protected void txtEmpid_TextChanged(object sender, EventArgs e)
        {
            gvNewLoan.DataSource = null;
            gvNewLoan.DataBind();
            txtMonth.Text = "";
            txtdesignation.Text = "";


            if (txtEmpid.Text.Length > 0)
            {
                Fillempname();
            }
            else
            {
                Cleardata();
            }
        }

        protected void txtMonth_TextChanged(object sender, EventArgs e)
        {
            if (txtMonth.Text.Length > 0)
            {
                LoadLoanDetailsoftheindividualemployee();
            }
            else
            {
                Cleardata();
            }
        }

        protected void txtName_TextChanged(object sender, EventArgs e)
        {
            gvNewLoan.DataSource = null;
            gvNewLoan.DataBind();
            txtMonth.Text = "";
            txtdesignation.Text = "";


            if (txtName.Text.Length > 0)
            {
                Fillempid();
            }
            else
            {
                Cleardata();
            }
        }



        protected void Cleardata()
        {
            txtEmpid.Text = "";
            txtName.Text = "";
            gvNewLoan.DataSource = null;
            gvNewLoan.DataBind();
            lblresult.Text = "";
        }


        protected void btnedit_OnClick(object sender, EventArgs e)
        {
            if (txtEmpid.Text == "" || txtName.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Showalert", "alert('Please select Empid or EmpName');", true);
                return;
            }
            for (int i = 0; i < gvNewLoan.Rows.Count; i++)
            {
                TextBox txtLoanAmt = gvNewLoan.Rows[i].FindControl("txtLoanAmt") as TextBox;
                TextBox txtNoInst = gvNewLoan.Rows[i].FindControl("txtNoInst") as TextBox;
                TextBox txtLoancut = gvNewLoan.Rows[i].FindControl("txtLoancut") as TextBox;
                txtLoanAmt.ReadOnly = false;
                txtNoInst.ReadOnly = false;
                txtLoancut.ReadOnly = false;
            }
            // btnsavemodifyloan.Visible = true;
            btnedit.Visible = false;
        }
        protected void btnsavemodifyloan_Click(object sender, EventArgs e)
        {

            string sqlqry = "";
            string clientid = "";


            lblresult.Text = "";
            int status = 0;
            for (int i = 0; i < gvNewLoan.Rows.Count; i++)
            {

                //TextBox txtloanamount = (TextBox)gvNewLoan.Rows[i].FindControl("txtdueamt");
                TextBox txtloanamount = (TextBox)gvNewLoan.Rows[i].FindControl("txtLoanAmt");
                TextBox txtNoInst = (TextBox)gvNewLoan.Rows[i].FindControl("txtNoInst");
                TextBox txtLoancut = (TextBox)gvNewLoan.Rows[i].FindControl("txtLoancut");

                #region Begin Validating Date Format
                var testDate = 0;
                if (txtLoancut.Text.Trim().Length > 0)
                {
                    testDate = GlobalData.Instance.CheckEnteredDate(txtLoancut.Text);
                    if (testDate > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('You Are Entered Invalid Loan Date.Date Format Should be [MM/DD/YYYY].Ex.01/01/1990');", true);
                        return;
                    }
                }
                #endregion End Validating Date Format

                if (txtloanamount.Text.Trim().Length == 0 || txtloanamount.Text.Trim().Length < 0)
                {
                    txtloanamount.Text = "0";
                }

                string empid = gvNewLoan.Rows[i].Cells[1].Text;
                string loanno = gvNewLoan.Rows[i].Cells[1].Text;

                float loanamount = float.Parse(txtloanamount.Text.Trim());
                //if (chk.Checked)
                //{

                #region    //Gettting Details About the Loan Type
                int loantype = 0;
                sqlqry = "SElect TypeOfLoan from emploanmaster  Where Loanno='" +
                loanno + "'";
                DataTable dtLoanType = config.ExecuteAdaptorAsyncWithQueryParams(sqlqry).Result;
                if (dtLoanType.Rows.Count > 0)
                {
                    loantype = int.Parse(dtLoanType.Rows[0][0].ToString());
                }

                #endregion

                /*sqlqry = "SElect Loanno from Modifyloandetails  Where Loanno='" +
                        loanno+"' and LoanMY='"+month+"'";*/
                sqlqry = "select LoanNo from Emploanmaster where LoanNo='" + loanno + "'";
                DataTable dt = config.ExecuteAdaptorAsyncWithQueryParams(sqlqry).Result;
                if (dt.Rows.Count > 0)
                {

                    /*sqlqry = "Update Modifyloandetails set LoanAmount='" + txtloanamount.Text +
                        "', Loantype='"+loantype+"' Where Loanno='" + loanno + "'  and  LoanMY='" + month + "'";*/
                    sqlqry = "Update Emploanmaster set LoanAmount='" + txtloanamount.Text + "',NoInstalments='" + txtNoInst.Text + "',LoanDt='" + txtLoancut.Text + "'" +
                        "where LoanNo='" + loanno + "'";
                    status = config.ExecuteNonQueryWithQueryAsync(sqlqry).Result;

                    sqlqry = string.Format("insert into Modifyloandetails(Empid,Loanno,LoanAmount,Loantype,Modified_By,Modified_On) " +
                       "  values('{0}','{1}','{2}','{3}','{4}','{5}','{6}')", txtEmpid.Text, loanno, loanamount, loantype, UserID, DateTime.Now);
                    status = config.ExecuteNonQueryWithQueryAsync(sqlqry).Result;

                }
                /*else
                {
                    sqlqry = string.Format("insert into Modifyloandetails(Empid,Loanno,LoanAmount,LoanMY,Loantype) " +
                        "  values('{0}','{1}','{2}','{3}','{4}')", ddlempid.SelectedValue, loanno, loanamount, month, loantype);
                    status = SqlHelper.Instance.ExecuteDMLQry(sqlqry);
                }
                */
                //}
            }
            for (int i = 0; i < gvNewLoan.Rows.Count; i++)
            {
                TextBox txtLoanAmt = gvNewLoan.Rows[i].FindControl("txtLoanAmt") as TextBox;
                TextBox txtNoInst = gvNewLoan.Rows[i].FindControl("txtNoInst") as TextBox;
                TextBox txtLoancut = gvNewLoan.Rows[i].FindControl("txtLoancut") as TextBox;
                txtLoanAmt.ReadOnly = true;
                txtNoInst.ReadOnly = true;
                txtLoancut.ReadOnly = true;
            }
            btnedit.Visible = true;
            btnsavemodifyloan.Visible = false;
            if (status != 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Showalert", "alert('Loan Modified Successfully');", true);
                return;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Showalert", "alert('No loans Loan ');", true);
                return;
            }

            Cleardata();

        }








        protected void gvNewLoan_RowDataBound(object sender, GridViewRowEventArgs e)
        {


            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TextBox txtloanamount = (TextBox)e.Row.FindControl("txtdueamt");
                string empid = e.Row.Cells[1].Text;


                //string sqlqry = "select Checkloan,Loanamount  from modifyloandetails Where empid='" + empid + "'  and Clientid='" +
                //    ddlempid.SelectedValue + "' ";
                //DataTable dt = SqlHelper.Instance.GetTableByQuery(sqlqry);
                //if (dt.Rows.Count > 0)
                //{
                //    chkloan.Checked = bool.Parse(dt.Rows[0]["Checkloan"].ToString());
                //    txtloanamount.Text = dt.Rows[0]["LoanAmount"].ToString();
                //}

            }
        }

        protected void gvNewLoan_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvNewLoan.PageIndex = e.NewPageIndex;
            LoadLoanDetailsoftheindividualemployee();
        }



        protected void LoadLoanDetailsoftheindividualemployee()
        {
            string sqlqry = string.Empty;
            DataTable dt = null;


            string month = "";
            string Year = "";

            if (txtMonth.Text.Length > 0)
            {

                month = DateTime.Parse(txtMonth.Text.Trim(), CultureInfo.GetCultureInfo("en-GB")).Month.ToString();
                Year = DateTime.Parse(txtMonth.Text.Trim(), CultureInfo.GetCultureInfo("en-GB")).Year.ToString();

            }

            //sqlqry = "Select  ELM.LoanNo,ELM.LoanAmount,ELM.NoInstalments,(ELM.NoInstalments-(count(ELD.loanno))) as RInst,"+
            //    " (ELM.LoanAmount/ELM.NoInstalments) as Instamt " +
            //    " From Emploanmaster ELM Inner Join " +
            //         " EmploanDetails ELD on ELM.Loanno=ELD.Loanno Where ELM.Empid='" + ddlempid.SelectedValue +
            //         "' and  ELM.loanstatus=0   Group By ELM.LoanNo,ELM.LoanAmount,ELM.NoInstalments " +
            //         "  union  SElect ELM.Loanno,ELM.loanamount,ELM.NoInstalments,ELM.NoInstalments as RInst, (ELM.LoanAmount/ELM.NoInstalments) as Instamt " +
            //         " From Emploanmaster  ELM   inner join  EmploanDetails ELD on ELM.Loanno<>ELD.Loanno  Where loanstatus=0 and  Empid='" + 
            //         ddlempid.SelectedValue + "'";

            sqlqry = "SElect ELM.Loanno,ELM.loanamount,ELM.NoInstalments,ELM.NoInstalments as RInst, (ELM.LoanAmount/ELM.NoInstalments) as Instamt,CONVERT(VARCHAR(10),ELM.LoanDt,103) as LoanDt," +
                " Isnull(LoanCount,'0') as LoanCount,case TypeOfLoan when 0 then 'Sal.Adv' when 1 then 'Uniform' when 2 then 'Security Deposit'  when 3 then 'Loan' when 4 then 'ATM'  when 5 then 'Others' else '' End as TypeOfLoan, " +
                " (select isnull(sum(recamt),0) from emploandetails ed where ed.loanno=ELM.loanno  ) as Recamt, " +
                " (select isnull((recamt),0) from emploandetails ed where ed.loanno=ELM.loanno  and loancuttingmonth='" + month + Year.Substring(2, 2) + "' ) as CurMonthRecamt " +
                " From Emploanmaster  ELM  Where  Empid='" + txtEmpid.Text + "'  and month(loandt)='" + month + "' and year(loandt)='" + Year + "' ";


            dt = config.ExecuteAdaptorAsyncWithQueryParams(sqlqry).Result;
            if (dt.Rows.Count > 0)
            {
                gvNewLoan.DataSource = dt;
                gvNewLoan.DataBind();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string loanno = dt.Rows[i][0].ToString();
                    string text =
                    sqlqry = "Select Loanamount from Emploanmaster  Where Loanno='" + loanno + "' ";
                    DataTable dtml = config.ExecuteAdaptorAsyncWithQueryParams(sqlqry).Result;
                    if (dtml.Rows.Count > 0)
                    {
                        gvNewLoan.Rows[i].Cells[8].Text = "RS." + dtml.Rows[0]["Loanamount"].ToString();
                    }
                    else
                    {
                        gvNewLoan.Rows[i].Cells[8].Text = "RS.0";
                    }

                }

            }
            else
            {
                gvNewLoan.DataSource = null;
                gvNewLoan.DataBind();
                ScriptManager.RegisterStartupScript(this, GetType(), "showlalert", "alert('There Is No Loans For The Selected Employee ');", true);
            }
        }


        protected void gvNewLoan_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string sqlqry = "", sqlqry1 = "";
            // string clientid = "";
            string sqlinsert;
            lblresult.Text = "";
            int status = 0, count = 0;

            //TextBox txtloanamount = (TextBox)gvNewLoan.Rows[e.RowIndex].FindControl("txtdueamt");
            TextBox txtloanamount = (TextBox)gvNewLoan.Rows[e.RowIndex].FindControl("txtLoanAmt");
            TextBox txtNoInst = (TextBox)gvNewLoan.Rows[e.RowIndex].FindControl("txtNoInst");
            TextBox txtLoancut = (TextBox)gvNewLoan.Rows[e.RowIndex].FindControl("txtLoancut");

            string txtLoancut1 = DateTime.Parse(txtLoancut.Text, CultureInfo.GetCultureInfo("en-gb")).ToString();

            #region Begin Validating Date Format
            var testDate = 0;
            if (txtLoancut.Text.Trim().Length > 0)
            {
                testDate = GlobalData.Instance.CheckEnteredDate(txtLoancut.Text);
                if (testDate > 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('You Are Entered Invalid Loan Date.Date Format Should be [DD/MM/YYYY].Ex.01/01/1990');", true);
                    return;
                }
            }
            #endregion End Validating Date Format

            if (txtloanamount.Text.Trim().Length == 0 || txtNoInst.Text.Trim().Length == 0 || txtLoancut.Text.Trim().Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Showalert", "alert('please fill following Loanamt,N.Inst,LoanCutMonth');", true);
                return;
                txtloanamount.Text = "0";
                txtNoInst.Text = "0";
                txtLoancut.Text = "1/1/1990";
            }

            Label lblLoanNo1 = (Label)gvNewLoan.Rows[e.RowIndex].FindControl("lblLoanNo1");
            string loanno = lblLoanNo1.Text;


            string sqlcheck = "select * from ModifidedLoanMaster where LoanNo='" + loanno + "'";
            DataTable dtcheck = config.ExecuteAdaptorAsyncWithQueryParams(sqlcheck).Result;
            if (dtcheck.Rows.Count == 0)
            {
                sqlinsert = "insert into ModifidedLoanMaster(Loanno,Empid,LoanActAmt,LoanCutMon,LoanNoInstalments) select LoanNo,EmpId,LoanAmount,LoanDt,noinstalments from EmpLoanMaster where LoanNo='" + loanno + "'";
              int sin= config.ExecuteNonQueryWithQueryAsync(sqlinsert).Result;
            }
            int LoanCount;
            sqlqry = "select LoanNo,LoanCount from Emploanmaster where LoanNo='" + loanno + "'";
            DataTable dt = config.ExecuteAdaptorAsyncWithQueryParams(sqlqry).Result;
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["LoanCount"].ToString().Length == 0)
                {
                    LoanCount = 0; LoanCount++;
                    sqlqry = "Update Emploanmaster set LoanAmount='" + txtloanamount.Text + "',NoInstalments='" + txtNoInst.Text + "',LoanDt='" + txtLoancut1 + "'" +
                        ", LoanCount='" + LoanCount + "' where LoanNo='" + loanno + "'";
                    status =config.ExecuteNonQueryWithQueryAsync(sqlqry).Result;
                }
                else
                {
                    LoanCount = int.Parse(dt.Rows[0]["LoanCount"].ToString());
                    LoanCount++;
                    sqlqry = "Update Emploanmaster set LoanAmount='" + txtloanamount.Text + "',NoInstalments='" + txtNoInst.Text + "',LoanDt='" + txtLoancut1 + "'" +
                        ", LoanCount='" + LoanCount + "' where LoanNo='" + loanno + "'";
                    status = config.ExecuteNonQueryWithQueryAsync(sqlqry).Result;
                }
                sqlinsert = "update ModifidedLoanMaster set ModifiedLoanAmt='" + txtloanamount.Text + "',ModifidedLoanCutMon='" + txtLoancut1.ToString().Trim() + "',ModifiedTime='" + DateTime.Now + "',NoInstalments='" + txtNoInst.Text + "',ModifiedBy='" + UserID + "' where Loanno='" + loanno + "'";
                int updf = config.ExecuteNonQueryWithQueryAsync(sqlinsert).Result;
            }

            gvNewLoan.EditIndex = -1;
            LoadLoanDetailsoftheindividualemployee();
            if (status != 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Showalert", "alert('Loan Modified Successfully');", true);
                return;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Showalert", "alert('Noloans Loan ');", true);
                return;
            }

            // Cleardata();
        }
        protected void gvNewLoan_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvNewLoan.EditIndex = -1;
            GridViewRow row = gvNewLoan.Rows[e.RowIndex];
            row.BackColor = System.Drawing.Color.LightYellow;
            LoadLoanDetailsoftheindividualemployee();
        }
        protected void gvNewLoan_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvNewLoan.EditIndex = e.NewEditIndex;
            LoadLoanDetailsoftheindividualemployee();
        }
    }
}