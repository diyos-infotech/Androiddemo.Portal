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
    public partial class ResourceAllocationEmp : System.Web.UI.Page
    {
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
                        string PID = Session["AccessLevel"].ToString();
                        DisplayLinks(PID);
                        lblcname.Text = SqlHelper.Instance.GetCompanyname();
                        GetEmpIds();
                        GetEmpNames();
                        LoadResourcedetails();
                        loanidauto();
                        BindReferedby();
                    }
                    else
                    {
                        Response.Redirect("Login.aspx");
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        public void DisplayLinks(string PID)
        {
            int value = 0;
            bool PreviligerStatus = int.TryParse(PID, out value);
            if (PreviligerStatus == true && value != 0)
            {
                switch (value)
                {
                    case 1:
                        break;
                    case 2:
                        Employeeslink.Visible = true;
                        ClientsLink.Visible = false;
                        CompanyInfoLink.Visible = false;
                        InventoryLink.Visible = true;
                        ReportsLink.Visible = true;

                        SettingsLink.Visible = false;
                        LogOutLink.Visible = true;
                        AddEmployeeLink.Visible = true;
                        ModifyEmployeeLink.Visible = true;
                        DeleteEmployeeLink.Visible = false;

                        AssigningWorkerLink.Visible = false;
                        AttendanceLink.Visible = true;
                        LoanLink.Visible = true;
                        PaymentLink.Visible = false;
                        PostingOrderListLink.Visible = true;

                        NewLoanLink.Visible = true;
                        ModifyLoanLink.Visible = true;
                        LoanRecoveryLink.Visible = false;
                        LoanRepaymentLink.Visible = false;
                        ResourceAllocationEmpLink.Visible = true;

                        break;

                    case 3:

                        break;
                    case 4:

                        break;
                    case 5:

                        break;
                    case 6:

                        break;
                    case 7:

                        break;
                    case 8:

                        break;
                }
            }
            else
            {
                GoToLoginPage();
            }
        }

        public void GoToLoginPage()
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Your Session Expired.Please Login');", true);
            Response.Redirect("~/login.aspx");
        }

        protected void GetWebConfigdata()
        {

            EmpIDPrefix = Session["EmpIDPrefix"].ToString();
            CmpIDPrefix = Session["CmpIDPrefix"].ToString();
            BranchID = Session["BranchID"].ToString();
        }

        protected void BindReferedby()
        {
            string SqlReferedby = "select EmpId,Empid + ' - ' + ISNULL(EmpFName,'')+''+ISNULL(EmpMName,'')+''+ISNULL(EmpLName,'') " +
                "  as EmpNameReplace from EmpDetails where EmpId like EmpId or empfname " +
                " like empfname or  empMname like empMname or empLname like empLname order by empfname";

            DataTable dtReplacefor = config.ExecuteAdaptorAsyncWithQueryParams(SqlReferedby).Result;

            if (dtReplacefor.Rows.Count > 0)
            {
                ddlreferedby.DataValueField = "EmpId";
                ddlreferedby.DataTextField = "EmpNameReplace";
                ddlreferedby.DataSource = dtReplacefor;
                ddlreferedby.DataBind();
            }
            ddlreferedby.Items.Insert(0, "--Select--");
        }


        protected void GetEmpIds()
        {
            DataTable dt = GlobalData.Instance.LoadEmpIds(EmpIDPrefix,BranchID);
            if (dt.Rows.Count > 0)
            {
                ddlEmpID.DataValueField = "empid";
                ddlEmpID.DataTextField = "empid";
                ddlEmpID.DataSource = dt;
                ddlEmpID.DataBind();
            }
            ddlEmpID.Items.Insert(0, "--Select--");
        }

        protected void GetEmpNames()
        {
            DataTable dt = GlobalData.Instance.LoadEmpNames(EmpIDPrefix,BranchID);
            if (dt.Rows.Count > 0)
            {
                ddlempmname.DataValueField = "empid";
                ddlempmname.DataTextField = "FullName";
                ddlempmname.DataSource = dt;
                ddlempmname.DataBind();
            }
            ddlempmname.Items.Insert(0, "--Select--");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            #region check employee id
            if (ddlEmpID.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "show alert()", "alert('Select Employee Id');", true);
                return;
            }
            #endregion

            #region check loandate
            if (txtloandate.Text.Trim().Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Please  fill the  loan date');", true);
                return;
            }

            var LoanDate = "01/01/1900";
            var testDate = 0;

            if (txtloandate.Text.Trim().Length > 0)
            {
                testDate = GlobalData.Instance.CheckEnteredDate(txtloandate.Text);
                if (testDate > 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(),
                        "show alert", "alert('You have Entered Invalid Loan Date.Date Format Should be [DD/MM/YYYY].Ex.01/01/1990');", true);
                    return;
                }
                LoanDate = DateTime.Parse(txtloandate.Text, CultureInfo.GetCultureInfo("en-gb")).ToString("yyyy-MM-dd hh:mm:ss");
            }
            #endregion

            loanidauto();

            #region check issue mode
            int issuemode = 0;

            if (ddlFreepaid.SelectedIndex == 0)
            {
                issuemode = 0;
            }

            if (ddlFreepaid.SelectedIndex == 1)
            {
                issuemode = 1;
            }
            #endregion

            #region check no of installment depend on issuemode

            int NoofInstallments = 0;

            if (txtnoofinstallments.Text.Trim().Length > 0)
            {
                NoofInstallments = int.Parse(txtnoofinstallments.Text);

            }

            if (issuemode == 0)
            {
                if (NoofInstallments == 0 || txtnoofinstallments.Text == string.Empty)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "show alert()", "alert('Please  fill No of Instalments');", true);
                    return;
                }
            }

            if (issuemode == 1)
            {
                NoofInstallments = 1;
            }


            #endregion

            int currentrowindex = 0;
            int CheckAtleastOne = 0;
            int InsertStatus = 0;

            string @TotalTransactionID = "";
            string Empid = ddlEmpID.SelectedValue;
            string SqlqryForResourceAlloc = string.Empty;
            string ResourceID = string.Empty;
            string loanno = txtloanid.Text;
            string Referredby = ddlreferedby.SelectedValue;
            string LoanIssuedDate = DateTime.Now.Date.ToString("yyyy-MM-dd hh:mm:ss");
            //DateTime.Parse(DateTime.Now.Date.ToShortDateString(), CultureInfo.GetCultureInfo("en-gb")).ToString(); 
            string AllResourceNames = string.Empty;

            float amount = 0;

            double sum = 0;
            double loanamnt = 0;

            DataTable DtAddResource = null;

            Hashtable HTInserResource = new Hashtable();
            string SPName = "AddResourcesBranchwise";

            #region For Each for Gridview Indvidual Rows
            if (gvresources.Rows.Count > 0)
            {
                foreach (GridViewRow gvr in gvresources.Rows)
                {
                    CheckBox cbcheck = sender as CheckBox;
                    Control ctrlone = gvr.FindControl("CbChecked") as CheckBox;
                    CheckBox chkresource = (CheckBox)ctrlone;
                    if (chkresource != null)
                    {
                        if (chkresource.Checked)
                        {
                            #region Begin Individual Resource Details of the employee

                            int Qty = 0;
                            float TotalPrice = 0;

                            CheckAtleastOne = 1;
                            TextBox tb = (TextBox)gvr.FindControl("txtresourceprice");
                            Label resourcename = (Label)gvr.FindControl("lblresourcename");
                            Label lblresourceid = (Label)gvr.FindControl("lblresourceid");
                            TextBox txtQty = (TextBox)gvr.FindControl("txtQty");

                            Qty = int.Parse(txtQty.Text);

                            ResourceID = lblresourceid.Text;
                            amount = float.Parse(tb.Text);
                            TotalPrice = Qty * amount;
                            sum += TotalPrice;

                            #region Begin New code for Insert Resource Details as on 19/07/2014

                            HTInserResource.Clear();
                            HTInserResource.Add("@Empid", Empid);
                            HTInserResource.Add("@Resourceid", ResourceID);
                            HTInserResource.Add("@Qty", Qty);
                            HTInserResource.Add("@Price", TotalPrice);
                            HTInserResource.Add("@ClientIDPrefix", CmpIDPrefix);
                            HTInserResource.Add("@TotalTransactionID", @TotalTransactionID);
                            HTInserResource.Add("@currentrowindex", currentrowindex + 1);
                            HTInserResource.Add("@LoanNo", txtloanid.Text);
                            HTInserResource.Add("@LoanType", 'N');
                            // HTInserResource.Add();

                            DtAddResource = config.ExecuteAdaptorAsyncWithParams(SPName, HTInserResource).Result;

                            if (DtAddResource.Rows.Count > 0)
                            {
                                if (currentrowindex == 0)
                                {
                                    @TotalTransactionID = DtAddResource.Rows[0]["transactionid"].ToString();
                                }
                            }

                            #endregion End New code for Insert Resource Details as on 19/07/2014

                            #region Begin Old code for Insert Resource Details as on 19/07/2014

                            //SqlqryForResourceAlloc = "Select isnull(ActualQuantity,0) As ActualQuantity  From Stockitemlist Where itemid='" + ResourceID + "'";
                            //DataTable DtForResourceAlloc = SqlHelper.Instance.GetTableByQuery(SqlqryForResourceAlloc);
                            //if (DtForResourceAlloc.Rows.Count > 0)
                            //{
                            //    if (String.IsNullOrEmpty(DtForResourceAlloc.Rows[0]["ActualQuantity"].ToString()) == false)
                            //    {
                            //        if (int.Parse(DtForResourceAlloc.Rows[0]["ActualQuantity"].ToString()) > 0)
                            //        {
                            //            SqlqryForResourceAlloc = string.Format("insert into EmpResourceDetails(EmpID,ResourceId,Price,Qty) values('{0}','{1}','{2}','{3}')",
                            //                Empid, ResourceID, TotalPrice, Qty);
                            //            InsertStatus = SqlHelper.Instance.ExecuteDMLQry(SqlqryForResourceAlloc);


                            //            if (InsertStatus != 0)
                            //            {
                            //                SqlqryForResourceAlloc = string.Format("Update Stockitemlist set ActualQuantity=ActualQuantity-'" + Qty + "' Where Itemid='{0}'",
                            //                                                    ResourceID);
                            //                InsertStatus = SqlHelper.Instance.ExecuteDMLQry(SqlqryForResourceAlloc);
                            //            }
                            //            sum += TotalPrice;
                            //        }
                            //        else
                            //        {
                            //            AllResourceNames = "\n  " + resourcename.Text + ",";
                            //        }
                            //    }
                            //}


                            #endregion End Old code for Insert Resource Details as on 19/07/2014

                            currentrowindex++;

                            #endregion  //End  Individual Resource Details of the employee
                        }
                    }
                }
            #endregion  //End For Each for Gridview Indvidual Rows

                //Label1.Text = sum.ToString();
                if (CheckAtleastOne == 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "ShowAlert", "alert('Please Select Atleast One Resource');", true);
                    return;
                }
                else
                {
                    if (issuemode == 0 && !string.IsNullOrEmpty(txtPaidAmnt.Text.Trim()) && float.Parse(txtPaidAmnt.Text.Trim()) > sum)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "ShowAlert", "alert('Paid Amount should be equal or lower than the total loan amount. Please check and enter correct amount.');", true);
                    }
                    else// is if paid amount is less than or equal to loan amount
                    {
                        #region  //Begin  Else block for the Check Atleast One Resource

                        if (sum == 0)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "ShowAlert", "alert('Resources Are Not allocated For the Selected Employee Please Check');", true);
                            return;
                        }
                        else if (sum > 0)
                        {
                            string paidamt = "";

                            if (txtPaidAmnt.Text.Length > 0)
                            {
                                paidamt = txtPaidAmnt.Text;
                            }
                            else
                            {
                                paidamt = "0";

                            }

                            loanamnt = issuemode == 0 ? sum - float.Parse(paidamt) : 0;
                            //var paidamt = issuemode == 0 ? float.Parse(txtPaidAmnt.Text) : 0;
                            if (issuemode == 1)
                            {
                                sum = 0;
                            }

                            //swathi 28/05/2015 add ReferredBy,{10} after noofinstalments 

                            SqlqryForResourceAlloc = string.Format("insert into Emploanmaster(LoanDt,EmpId,LoanAmount,NoInstalments, " +
                                "LoanStatus,TypeOfLoan,IssueMode,LoanIssuedDate,PaidAmnt) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}')",
                                 LoanDate, Empid, loanamnt, NoofInstallments, "0", "1", issuemode, LoanIssuedDate, paidamt);
                            InsertStatus = config.ExecuteNonQueryWithQueryAsync(SqlqryForResourceAlloc).Result;

                            if (InsertStatus != 0)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "ShowAlert", "alert('Resources Are  allocated For the Selected Employee.');", true);
                                lblTotalamt.Text = "Total Loan Amount Rs. : " + loanamnt;
                                ClearData();
                                LoadResourcedetails();
                                return;
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "ShowAlert", "alert('Resources Are Not  allocated For the Selected Employee.');", true);
                                return;
                            }

                        }

                        #endregion   //End  Else block for the Check Atleast One Resource
                    }
                }//
            }//end Gridview No Of Reows

            gvresources.PageIndex = 0;


        }

        protected void GetEmpid()
        {

            if (ddlempmname.SelectedIndex > 0)
            {
                ddlEmpID.SelectedValue = ddlempmname.SelectedValue;
            }
            else
            {
                ddlEmpID.SelectedIndex = 0;
            }

        }

        protected void GetEmpaname()
        {
            if (ddlEmpID.SelectedIndex > 0)
            {
                ddlempmname.SelectedValue = ddlEmpID.SelectedValue;
            }
            else
            {
                ddlempmname.SelectedIndex = 0;
            }
        }

        protected void ClearData()
        {
            txtnoofinstallments.Text = string.Empty;
            txtPaidAmnt.Text = string.Empty;
            loanidauto();

            if (gvresources.Rows.Count > 0)
            {

                foreach (GridViewRow gvrow in gvresources.Rows)
                {
                    CheckBox CheckRow = gvrow.FindControl("CbChecked") as CheckBox;
                    CheckRow.Checked = false;
                }
            }
        }

        protected void ddlEmpID_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblTotalamt.Text = "";
            txtPaidAmnt.Text = "";
            ddlFreepaid.SelectedIndex = 0;

            if (ddlEmpID.SelectedIndex > 0)
            {
                GetEmpaname();
                gvresources.PageIndex = 0;
            }
            else
            {
                ClearData();
            }
        }

        protected void ddlempmname_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblTotalamt.Text = "";
            txtPaidAmnt.Text = "";
            ddlFreepaid.SelectedIndex = 0;
            if (ddlempmname.SelectedIndex > 0)
            {
                GetEmpid();
                gvresources.PageIndex = 0;
            }
            else
            {
                ClearData();
            }

        }

        protected void LoadResourcedetails()
        {
            string Sqlqry = "select R.ResourceID,SI.ItemName,R.Price from Resources R inner join StockItemList SI on R.ResourceID=SI.ItemId order by R.ResourceID";
            DataTable dt = config.ExecuteAdaptorAsyncWithQueryParams(Sqlqry).Result;
            if (dt.Rows.Count > 0)
            {
                gvresources.DataSource = dt;
                gvresources.DataBind();
            }
            else
            {
                gvresources.DataSource = null;
                gvresources.DataBind();
            }
        }

        protected void gvresources_databound(object sender, GridViewRowEventArgs e)
        {

            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    Label lbl = (e.Row.FindControl("lblresourceid") as Label);
            //    string rid = lbl.Text;

            //    string Sqlqry = "select COALESCE( SUM(Quantity),0) Quantity from InflowDetails WHERE ItemId='" + rid + "' and TransactionId like '" + CmpIDPrefix + "' %' ";
            //    DataTable dt = SqlHelper.Instance.GetTableByQuery(Sqlqry);

            //    string Sqlqry1 = "select COALESCE( SUM(ApprovedQty),0) Quantity from MRF WHERE ItemId='" + rid + "'  and Transactionid like '" + CmpIDPrefix + "' %'";
            //    DataTable dt1 = SqlHelper.Instance.GetTableByQuery(Sqlqry1);

            //    string Sqlqry2 = "select COALESCE( SUM(qty),0) Quantity  from EmpResourceDetails  WHERE ResourceId='" + rid + "'  and Transactionid like '" + CmpIDPrefix + "' %'";
            //    DataTable dt2 = SqlHelper.Instance.GetTableByQuery(Sqlqry2);

            //    Int64 availqty = Int64.Parse(dt.Rows[0][0].ToString()) - Int64.Parse(dt.Rows[0][0].ToString()) - Int64.Parse(dt.Rows[0][0].ToString());


            //    CheckBox CheckBox1 = (e.Row.FindControl("CbChecked") as CheckBox);
            //    if (availqty <= 0)
            //    {
            //        CheckBox1.Enabled = false;
            //    }
            //}
        }

        protected void txtQty_Textchanged(object sender, EventArgs e)
        {
            int qty = 0;
            float Price = 0;
            float totalPrice = 0;

            foreach (GridViewRow gvr in gvresources.Rows)
            {
                CheckBox cbcheck = sender as CheckBox;
                Control ctrlone = gvr.FindControl("CbChecked") as CheckBox;
                CheckBox chkresource = (CheckBox)ctrlone;
                if (chkresource != null)
                {
                    if (chkresource.Checked)
                    {

                        TextBox txtprice = (TextBox)gvr.FindControl("txtresourceprice");
                        Label resourcename = (Label)gvr.FindControl("lblresourcename");
                        Label lblresourceid = (Label)gvr.FindControl("lblresourceid");
                        TextBox txtQty = (TextBox)gvr.FindControl("txtQty");
                        qty = int.Parse(txtQty.Text);
                        Price = float.Parse(txtprice.Text);
                        totalPrice = qty * Price;

                        txtprice.Text = totalPrice.ToString();

                    }
                }
            }
        }

        private void loanidauto()
        {
            //getloandata();
            int loanid;
            string selectqueryclientid = "select max(cast(LoanNo as int )) as Loanno from EmpLoanMaster ";
            DataTable dt = config.ExecuteAdaptorAsyncWithQueryParams(selectqueryclientid).Result;

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
                    txtloanid.Text = loanid.ToString("000001");
                }
            }
        }

        protected void gvresources_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvresources.PageIndex = e.NewPageIndex;
            LoadResourcedetails();
        }

        protected void CbChecked_CheckedChanged(object sender, EventArgs e)
        {
            //  CheckBox chkbx = sender as CheckBox;
            //  GridViewRow row = null;
            //  if (chkbx == null)
            //      return;

            //  row = (GridViewRow)chkbx.NamingContainer;
            //  if (row == null)
            //      return;

            //  TextBox txttotalqty = row.FindControl("txtQty") as TextBox;
            //  TextBox txttotalamt = row.FindControl("txtresourceprice") as TextBox;


            //  float TotalAmount = 0;
            float sum1 = 0;

            foreach (GridViewRow gvrow in gvresources.Rows)
            {

                CheckBox CbChecked = gvrow.FindControl("CbChecked") as CheckBox;
                TextBox txttotalqty = gvrow.FindControl("txtQty") as TextBox;
                TextBox txttotalamt = gvrow.FindControl("txtresourceprice") as TextBox;

                if (CbChecked.Checked)
                {
                    int c = 3;
                    sum1 = (Convert.ToSingle(txttotalqty.Text) * Convert.ToSingle(txttotalamt.Text));
                }
            }

            //for (int i = 0; i < gvresources.Rows.Count; i++)
            //{
            //    if (CbChecked.Checked == true)
            //    {
            //  TotalAmount  = Convert.ToSingle(txttotalqty.Text) * Convert.ToSingle(txttotalamt.Text);
            //        //sum += TotalAmount;
            //    }
            //}

            txttotal.Text = sum1.ToString();
        }

        protected void gvresources_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        float totakprice = 0;
        float totalquantity = 0;
        float totalamount = 0;

        //protected void gvresources_RowDatabound(object sender, GridViewRowEventArgs e)
        //{
        //    CheckBox CbChecked = gvresources.FindControl("CbChecked") as CheckBox;
        //    TextBox txttotalqty = gvresources.FindControl("txtQty") as TextBox;
        //    TextBox txttotalamt = gvresources.FindControl("txtresourceprice") as TextBox;

        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        if (CbChecked.Checked == true)
        //        {
        //            float price = float.Parse(((Label)e.Row.FindControl("txtresourceprice")).Text);
        //            totakprice += price;
        //            float quantity = float.Parse(((Label)e.Row.FindControl("txtQty")).Text);
        //            totalquantity += quantity;
        //            totalamount = price * quantity;
        //        }
        //        txttotal.Text = totalamount.ToString();
        //    }
        //}
    }
}