using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KLTS.Data;
using System.Data;
using System.Collections;
using System.Globalization;
using Jawan.Portal.DAL;
namespace Jawan.Portal
{
    public partial class BankUploadFormat : System.Web.UI.Page
    {
        //DataTable dt;
        string EmpIDPrefix = "";
        string CmpIDPrefix = "";
        string BranchID = "";
        string Accountno = "";
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
                        switch (SqlHelper.Instance.GetCompanyValue())
                        {
                            case 0:// Write Omulance Invisible Links
                                break;
                            case 1://Write KLTS Invisible Links
                                ExpensesReportsLink.Visible = false;
                                break;
                            case 2://write Fames Link
                                ExpensesReportsLink.Visible = true;
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        Response.Redirect("login.aspx");
                    }
                    //  FillClientList();
                    // FillClientNameList();
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Show alert", "alert('Your Session Expired');", true);
                Response.Redirect("~/Login.aspx");
            }
        }

        public void GetEmpDetail()
        {

        }

        protected void GetWebConfigdata()
        {
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
                    EmployeeReportLink.Visible = true;
                    ClientsReportLink.Visible = true;
                    InventoryReportLink.Visible = true;

                    EmployeesLink.Visible = true;
                    ClientsLink.Visible = true;
                    CompanyInfoLink.Visible = false;
                    InventoryLink.Visible = false;
                    ReportsLink.Visible = true;
                    SettingsLink.Visible = true;

                    break;

                case 4:
                    EmployeeReportLink.Visible = true;
                    ClientsReportLink.Visible = true;
                    InventoryReportLink.Visible = true;

                    EmployeesLink.Visible = true;
                    ClientsLink.Visible = true;
                    CompanyInfoLink.Visible = true;
                    InventoryLink.Visible = true;
                    ReportsLink.Visible = true;
                    SettingsLink.Visible = true;

                    break;
                case 5:
                    EmployeeReportLink.Visible = true;
                    ClientsReportLink.Visible = true;
                    InventoryReportLink.Visible = true;
                    EmployeesLink.Visible = true;
                    ClientsLink.Visible = true;
                    CompanyInfoLink.Visible = false;
                    InventoryLink.Visible = false;
                    ReportsLink.Visible = true;
                    ReportsLink.Visible = true;
                    SettingsLink.Visible = true;

                    break;
                case 6:
                    EmployeesLink.Visible = false;
                    ClientsLink.Visible = false;
                    CompanyInfoLink.Visible = false;
                    InventoryLink.Visible = true;
                    ReportsLink.Visible = true;
                    SettingsLink.Visible = false;
                    EmployeeReportLink.Visible = false;
                    break;
                default:
                    break;
            }
        }

        public string GetMonth()
        {
            string month = "";
            string year = "";
            string DateVal = "";
            DateTime date;


            if (txtmonth.Text != "")
            {
                date = DateTime.ParseExact(txtmonth.Text, "MMM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
                month = DateTime.Parse(txtmonth.Text.Trim(), CultureInfo.GetCultureInfo("en-GB")).Month.ToString();
                year = DateTime.Parse(txtmonth.Text.Trim(), CultureInfo.GetCultureInfo("en-GB")).Year.ToString();

            }

            DateVal = month + year.Substring(2, 2);
            return DateVal;

        }
     
        public void GetClientsData()
        {
            string date = string.Empty;

            GVListEmployees.DataSource = null;
            GVListEmployees.DataBind();

            GVListClients.DataSource = null;
            GVListClients.DataBind();

            if (txtmonth.Text.Trim().Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Please Select The Month');", true);
                return;
            }

            if (txtmonth.Text.Trim().Length > 0)
            {
                date = DateTime.Parse(txtmonth.Text.Trim(), CultureInfo.GetCultureInfo("en-gb")).ToString();
            }
            string Month = GetMonth();

            string query = "select distinct(eps.clientid),clientname from emppaysheet eps inner join clients C on C.Clientid=eps.clientid where month='" + Month + "' and eps.clientid like '%" + CmpIDPrefix + "%'";
            DataTable dtClients = config.ExecuteAdaptorAsyncWithQueryParams(query).Result;
            if (dtClients.Rows.Count > 0)
            {
                GVListEmployees.DataSource = dtClients;
                GVListEmployees.DataBind();
            }
            else
            {
                GVListEmployees.DataSource = null;
                GVListEmployees.DataBind();

            }


            lbtn_Export.Visible = true;
        }

        protected void ClearData()
        {
            GVListEmployees.DataSource = null;
            GVListEmployees.DataBind();
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

        public int GetMonthBasedOnSelectionDateorMonth()
        {

            var testDate = 0;
            string EnteredDate = "";

            #region Validation

            if (txtmonth.Text.Trim().Length > 0)
            {

                try
                {

                    testDate = GlobalData.Instance.CheckEnteredDate(txtmonth.Text);
                    if (testDate > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('You Are Entered Invalid  DATE.Date Format Should be [DD/MM/YYYY].Ex.01/01/1990');", true);
                        return 0;
                    }
                    EnteredDate = DateTime.Parse(txtmonth.Text, CultureInfo.GetCultureInfo("en-gb")).ToString();
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('You Are Entered Invalid  DATE.Date Format Should be [DD/MM/YYYY].Ex.01/01/1990');", true);
                    return 0;
                }
            }
            #endregion


            #region  Month Get Based on the Control Selection
            int month = 0;

            DateTime date = DateTime.Parse(txtmonth.Text, CultureInfo.GetCultureInfo("en-gb"));
            month = Timings.Instance.GetIdForEnteredMOnth(date);
            //return month;

            return month;

            #endregion
        }

        protected int GetMonth(string NameOfmonth)
        {
            int month = -1;
            var formatInfoinfo = new DateTimeFormatInfo();
            string[] monthName = formatInfoinfo.MonthNames;
            for (int i = 0; i < monthName.Length; i++)
            {
                if (monthName[i].CompareTo(NameOfmonth) == 0)
                {
                    month = i + 1;
                    break;
                }
            }
            return month;
        }

        public string GetMonthName()
        {
            string monthname = string.Empty;
            int payMonth = 0;
            DateTimeFormatInfo mfi = new DateTimeFormatInfo();



            DateTime date = Convert.ToDateTime(txtmonth.Text, CultureInfo.GetCultureInfo("en-gb"));
            monthname = mfi.GetMonthName(date.Month).ToString();
            //payMonth = GetMonth(monthname);

            return monthname;
        }

        protected void lbtn_Export_Click(object sender, EventArgs e)
        {
            DataTable DtListOfEmployees = new DataTable();

            string Month = GetMonth();
            int option = ddlOptions.SelectedIndex;
            var list = new List<string>();

            string clientname = "";


            GVListClients.DataSource = null;
            GVListClients.DataBind();

            if (GVListEmployees.Rows.Count > 0)
            {
                string strQry = "Select * from CompanyInfo  where   branchid='" + BranchID + "'";
                DataTable compInfo = config.ExecuteAdaptorAsyncWithQueryParams(strQry).Result;
                string companyName = "Your Company Name";
                //   string companyAddress = "Your Company Address";

                string heading1 = "";
                string heading = "";

                if (compInfo.Rows.Count > 0)
                {
                    companyName = compInfo.Rows[0]["CompanyName"].ToString();
                    //companyAddress = compInfo.Rows[0]["Address"].ToString();
                }



                for (int i = 0; i < GVListEmployees.Rows.Count; i++)
                {
                    CheckBox chkclientid = GVListEmployees.Rows[i].FindControl("chkindividual") as CheckBox;
                    if (chkclientid.Checked == true)
                    {
                        Label lblclientid = GVListEmployees.Rows[i].FindControl("lblclientid") as Label;
                        Label lblclientname = GVListEmployees.Rows[i].FindControl("lblclientname") as Label;

                        if (chkclientid.Checked == true)
                        {
                            list.Add(lblclientid.Text);
                        }

                    }

                }


            }

            string Clientids = string.Join(",", list.ToArray());

            DataTable dtClientList = new DataTable();
            dtClientList.Columns.Add("Clientid");
            if (list.Count != 0)
            {
                foreach (string s in list)
                {
                    DataRow row = dtClientList.NewRow();
                    row["Clientid"] = s;
                    dtClientList.Rows.Add(row);
                }
            }


            Hashtable HtsearchEmp = new Hashtable();
            string sp = "";
            //string search = "";
            // search = txtmonth.Text;
            sp = "BankUpLoadFormate";

            HtsearchEmp.Add("@month", Month);
            HtsearchEmp.Add("@ClientId", dtClientList);
            HtsearchEmp.Add("@option",option);

            DtListOfEmployees = config.ExecuteAdaptorAsyncWithParams(sp, HtsearchEmp).Result;
            if (DtListOfEmployees.Rows.Count > 0)
            {
                GVListClients.Visible = false;
                GVListClients.DataSource = DtListOfEmployees;
                GVListClients.DataBind();
                for (int i = 0; i < GVListClients.Rows.Count; i++)
                {
                    if (ddlOptions.SelectedIndex==2)
                    {
                        GVListClients.Columns[0].Visible = false;
                    }
                    else
                    {
                        GVListClients.Columns[0].Visible = true;
                    }
                   
                }
                gve.Export("BankUploadFormat.xls", this.GVListClients);
            }

           


        }

        string currentId = "";
        int subTotalRowIndex = 0;

        protected void GVListEmployees_RowCreated(object sender, GridViewRowEventArgs e)
        {
            DataTable dt = new DataTable();

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.DataItem is DataRowView
                      && (e.Row.DataItem as DataRowView).DataView.Table != null)
                {
                    dt = (e.Row.DataItem as DataRowView).DataView.Table;
                    string clientid = (dt.Rows[e.Row.RowIndex]["clientid"].ToString());
                    string clientname = (dt.Rows[e.Row.RowIndex]["clientname"].ToString());

                    if (clientid != currentId)
                    {
                        if (e.Row.RowIndex > 0)
                        {

                        }


                        string strQry = "Select * from CompanyInfo  where   branchid='" + BranchID + "'";
                        DataTable compInfo = config.ExecuteAdaptorAsyncWithQueryParams(strQry).Result;
                        string companyName = "Your Company Name";

                        //clientname = DtListOfEmployees.Rows[i]["clientname"].ToString();
                        string heading1 = "";

                        if (compInfo.Rows.Count > 0)
                        {
                            companyName = compInfo.Rows[0]["CompanyName"].ToString();

                        }
                        string heading = companyName + "<br>" + "SALARIES FOR THE MONTH OF  " + GetMonthName() + "-" + GetMonthOfYear() + " ON " + txtmonth.Text + " THROUGH BANK " + "<br>" + "SITE NAME: " + clientname;


                        if (clientid != "")
                        {
                            this.AddTotalRow(heading, clientid);
                        }
                        else
                        {
                            this.AddTotalRow("", "");
                        }


                        subTotalRowIndex = e.Row.RowIndex;
                    }

                    currentId = clientid;
                }
            }
        }
        private void AddTotalRow(string labelText, string clientid)
        {

            GridViewRow row = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal);
            GridViewRow row1 = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal);

            if (clientid != "")
            {
                TableCell HeaderCell = new TableCell();
                HeaderCell.Text = labelText;
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell.ColumnSpan = 4; // For merging first, second row cells to one
                HeaderCell.CssClass = "SubTotalRowStyle";
                row.Cells.Add(HeaderCell);

                TableCell HeaderCell1 = new TableCell();
                HeaderCell1.Text = "S.No";
                HeaderCell1.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell1.ColumnSpan = 1; // For merging first, second row cells to one
                HeaderCell1.CssClass = "SubTotalRowStyle";
                row1.Cells.Add(HeaderCell1);

                TableCell HeaderCell2 = new TableCell();
                HeaderCell2.Text = "NAME OF THE EMPLOYEE";
                HeaderCell2.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell2.ColumnSpan = 1; // For merging first, second row cells to one
                HeaderCell2.CssClass = "SubTotalRowStyle";
                row1.Cells.Add(HeaderCell2);


                TableCell HeaderCell3 = new TableCell();
                HeaderCell3.Text = "A/C NO";
                HeaderCell3.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell3.ColumnSpan = 1; // For merging first, second row cells to one
                HeaderCell3.CssClass = "SubTotalRowStyle";
                row1.Cells.Add(HeaderCell3);


                TableCell HeaderCell4 = new TableCell();
                HeaderCell4.Text = "PAYABLE AMOUNT";
                HeaderCell4.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell4.ColumnSpan = 1; // For merging first, second row cells to one
                HeaderCell4.CssClass = "SubTotalRowStyle";
                row1.Cells.Add(HeaderCell4);

                GVListEmployees.Controls[0].Controls.Add(row);
                GVListEmployees.Controls[0].Controls.Add(row1);
            }


        }
        protected void GVListEmployees_DataBound(object sender, EventArgs e)
        {
            if (GVListEmployees.Rows.Count > 0)
            {


                string strQry = "Select * from CompanyInfo  where   branchid='" + BranchID + "'";
                DataTable compInfo = config.ExecuteAdaptorAsyncWithQueryParams(strQry).Result;
                string companyName = "Your Company Name";
                string clientid = "";

                string heading1 = "";

                if (compInfo.Rows.Count > 0)
                {
                    companyName = compInfo.Rows[0]["CompanyName"].ToString();

                }


                string heading = companyName + "<br>" + "SALARIES FOR THE MONTH OF  " + GetMonthName() + "-" + GetMonthOfYear() + " ON " + txtmonth.Text + " THROUGH BANK ";
                //heading1 = "SITE NAME: " + lblclientname.Text;

                if (clientid != "")
                {
                    this.AddTotalRow(heading, clientid);
                }
                else
                {
                    this.AddTotalRow("", "");
                }


            }
        }
        protected void GVListEmployees_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[4].Attributes.Add("class", "text");


            }

        }
        protected void GVListClients_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Attributes.Add("class", "text");
                e.Row.Cells[4].Attributes.Add("class", "text");
                e.Row.Cells[7].Attributes.Add("class", "text");
                e.Row.Cells[2].Attributes.Add("class", "text");
                e.Row.Cells[5].Attributes.Add("class", "text");

                e.Row.Cells[2].Style.Add("text-align", "right");

            }
        }

        protected void txtmonth_TextChanged(object sender, EventArgs e)
        {
            GetClientsData();
        }
    }
}