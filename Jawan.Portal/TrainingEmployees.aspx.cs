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
using System.Data;
using KLTS.Data;
using Jawan.Portal.DAL;
namespace Jawan.Portal
{
    public partial class TrainingEmployees : System.Web.UI.Page
    {
        AppConfiguration config = new AppConfiguration();
        GridViewExportUtil gve = new GridViewExportUtil();
        static int activeindex = 0;
        static int activeindextwo = 0;
        static int rowindexvisible = 0;
        DataTable dt;
        DropDownList bind_dropdownlist;
        int TotalStatus;
        string EmpIDPrefix = "";
        string CmpIDPrefix = "";
        string Elength = "";
        string Clength = "";
        public void binddata()
        {
            string selectqueryempid = "select empid, empmname  from EmpDetails Order By cast(substring(empid," + Elength + ", 6) as int)";

            dt = config.ExecuteAdaptorAsyncWithQueryParams(selectqueryempid).Result;
            gvtraining.DataSource = dt;
            gvtraining.DataBind();
            foreach (GridViewRow grdRow in gvtraining.Rows)
            {
                bind_dropdownlist = (DropDownList)(gvtraining.Rows[grdRow.RowIndex].Cells[0].FindControl("ddlempid"));
                bind_dropdownlist.DataSource = dt;
                bind_dropdownlist.DataValueField = "empid";
                bind_dropdownlist.DataTextField = "empid";
                bind_dropdownlist.DataBind();

                //bind_dropdownlist.SelectedValue = "0";
                //bind_dropdownlist.SelectedItem.Text = "--Select Emp Id--";
            }
            if (dt != null)
            {
            }
            else
            {
                lblreslt.Text = " There are no employees";

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
                    LoanLink.Visible = false;
                    PaymentLink.Visible = false;
                    //TrainingEmployeeLink.Visible = true;
                    //JobLeavingReasonsLink.Visible = false;
                    AttendanceLink.Visible = true;

                    Employeeslink.Visible = true;
                    ClientsLink.Visible = true;
                    CompanyInfoLink.Visible = false;
                    InventoryLink.Visible = false;
                    ReportsLink.Visible = true;
                    ReportsLink.Visible = true;
                    SettingsLink.Visible = true;
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
                    binddata();
                    trainerid();
                    TrainingID();





                    //if (Session["UserId"] != null && Session["AccessLevel"] != null)
                    //{
                    //    //lblDisplayUser.Text = Session["UserId"].ToString();
                    //    PreviligeUsers(Convert.ToInt32(Session["AccessLevel"]));
                    //}
                    //else
                    //{
                    //    Response.Redirect("login.aspx");
                    //}
                    for (rowindexvisible = 0; rowindexvisible < gvtraining.Rows.Count; rowindexvisible++)
                    {
                        if (rowindexvisible < 1)
                        {
                            activeindex = activeindex + 1;
                            gvtraining.Rows[rowindexvisible].Visible = true;
                        }
                        else
                            gvtraining.Rows[rowindexvisible].Visible = false;

                    }
                }
            }
            catch (Exception ex)
            {
                lblreslt.Visible = true;
                lblreslt.Text = "There is no Record";
            }
        }

        protected void GetWebConfigdata()
        {
            EmpIDPrefix = GlobalData.Instance.GetEmployeeIDPrefix();
            Elength = (EmpIDPrefix.Trim().Length + 1).ToString();
            CmpIDPrefix = GlobalData.Instance.GetClientIDPrefix();
            Clength = (CmpIDPrefix.Trim().Length + 1).ToString();
        }

        private void trainerid()
        {
            string selectqueryclientid = "select empid from EmpDetails cast(substring(empid," + Elength + ", 6) as int)";
            dt = config.ExecuteAdaptorAsyncWithQueryParams(selectqueryclientid).Result;
            int TrainerIndex = 0;
            for (TrainerIndex = 0; TrainerIndex < dt.Rows.Count; TrainerIndex++)
            {
                ddltrainerid.Items.Add(dt.Rows[TrainerIndex][0].ToString());

            }
        }

        private void TrainingID()
        {

            DataTable DtTrainingId;

            string selectqueryclientid = "select max(cast(id as int) as id from EmpTrainingMaster";
            DtTrainingId = config.ExecuteAdaptorAsyncWithQueryParams(selectqueryclientid).Result;
            if (DtTrainingId.Rows.Count != 0)
            {

                if (String.IsNullOrEmpty(DtTrainingId.Rows[0][0].ToString()) == false)
                {
                    string id = DtTrainingId.Rows[DtTrainingId.Rows.Count - 1][0].ToString();
                    txttrainingid.Text = (Convert.ToInt64(id) + 1).ToString();
                }
                else
                {
                    txttrainingid.Text = "1";
                }
            }

        }

        protected void btnsave_Click(object sender, EventArgs e)
        {
            lblreslt.Visible = true;
            try
            {
                string trainingid = txttrainingid.Text;
                string trinerid = ddltrainerid.SelectedItem.Text;
                string trainername = txttrainername.Text;
                string site = txtsite.Text;
                string date = txtdate.Text;
                string starttime = txtstarttime.Text;
                string endtime = txtendingtime.Text;
                string totalstaff = txtstaff.Text;
                string noofstaffattend = txtnoofstaffattend.Text;
                string TopicCovered = txttopiccoverd.Text;
                string InsertTrainingQuery = string.Format("insert into EmpTrainingMaster values('{0}','{1}','{2}','{3}','{4}','{5}',{6},{7},'{8}')",
                    trainingid, trinerid, site, date, starttime, endtime, totalstaff, noofstaffattend, TopicCovered);
                int status1 =config.ExecuteNonQueryWithQueryAsync(InsertTrainingQuery).Result;
                if (status1 != 0)
                {
                    TotalStatus = TotalStatus + 1;

                }
                int rowindex = 0;
                for (rowindex = 0; rowindex < activeindex; rowindex++)
                {
                    //DropDownList selectedempid = gvtraining.Rows[rowindex].FindControl("ddlempid") as DropDownList;
                    string empid = ((DropDownList)gvtraining.Rows[rowindex].FindControl("ddlempid")).SelectedValue;
                    string selectedempid = ((DropDownList)gvtraining.Rows[rowindex].FindControl("ddlempid")).SelectedItem.ToString();
                    if (selectedempid != "--Select EmpId--")
                    {
                        Label tempname = gvtraining.Rows[rowindex].FindControl("lblempname") as Label;
                        string empname = tempname.Text;
                        TextBox tremarks = gvtraining.Rows[rowindex].FindControl("txtremarks") as TextBox;
                        string remarks = tremarks.Text;
                        TextBox thostemployee = gvtraining.Rows[rowindex].FindControl("txtpta") as TextBox;
                        string hostemployee = thostemployee.Text;

                        string insertquery = string.Format(" insert into EmpTrainingDetails values('{0}','{1}','{2}','{3}')", trainingid, empid, remarks, hostemployee);
                        int status =config.ExecuteNonQueryWithQueryAsync(insertquery).Result;

                        if (status != 0)
                        {
                            TotalStatus = TotalStatus + 1;
                        }
                    }
                }


                if (TotalStatus == 2)
                {
                    lblreslt.Text = "Record Inserted Successfully";
                    TrainingID();

                }

                else
                {
                    lblreslt.Text = "Record Not Inserted Properly";

                }
            }
            catch (Exception ex)
            {
                lblreslt.Text = "Incorrect  Data Please Check";
            }
        }

        protected void gvtraining_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvtraining.PageIndex = e.NewPageIndex;
            gvtraining.DataBind();
        }

        protected void gvtraining_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            //foreach (GridViewRow dr in gvtraining.Rows)
            //{
            //    dr.Visible = false;

            //}





            //    int rowIndex = 0;

            //    if (ViewState["CurrentTable"] != null)
            //    {
            //        DataTable dt = (DataTable)ViewState["CurrentTable"];
            //        if (dt.Rows.Count > 0)
            //        {
            //            for (int i = 0; i < dt.Rows.Count; i++)
            //            {
            //                TextBox box1 = (TextBox)gv1.Rows[rowIndex].Cells[1].FindControl("txtempid");
            //                TextBox box2 = (TextBox)gv1.Rows[rowIndex].Cells[2].FindControl("txtempname");
            //               // TextBox box3 = (TextBox)gv1.Rows[rowIndex].Cells[3].FindControl("lblEmployee_email");
            //                box1.Text = dt.Rows[i]["Column1"].ToString();
            //                box2.Text = dt.Rows[i]["Column2"].ToString();
            //               // box3.Text = dt.Rows[i]["Column3"].ToString();
            //                rowIndex++;

            //        }
            //    }
            //}
        }

        protected void btnadddesgn_Click(object sender, EventArgs e)
        {
            if (activeindex < gvtraining.Rows.Count)
            {


                int rowindex = 0;
                activeindextwo = activeindex + 1;
                gvtraining.Rows[activeindex].Visible = true;
                string selectquery = "Select empid from EmpDetails Order By cast(substring(empid," + Elength + ", 6) as int)";
                DataTable DtDesignation = config.ExecuteAdaptorAsyncWithQueryParams(selectquery).Result;
                //GridViewRow dr = gvdesignation.Rows[activeindex];
                DropDownList ddldrow = gvtraining.Rows[activeindex].FindControl("ddlempid") as DropDownList;
                ddldrow.DataSource = DtDesignation;
                ddldrow.DataValueField = "Empid";
                ddldrow.DataTextField = "Empid";
                ddldrow.DataBind();
                //ddldrow.SelectedValue = "0";
                // ddldrow.SelectedItem.Text = "--Select Empid Id--";
                activeindex = activeindex + 1;
            }
            else
            {
                lblreslt.Text = "Theres Are No more Employees ";

            }

        }

        string selecteditem;
        protected void ddlempid_getempname(object sender, EventArgs e)
        {
            DropDownList ddl = sender as DropDownList;

            foreach (GridViewRow row in gvtraining.Rows)
            {
                Control ctrl = row.FindControl("ddlempid") as DropDownList;
                Control ctrlitemname = row.FindControl("lblempname") as Label;
                if (ctrl != null)
                {
                    DropDownList ddl1 = (DropDownList)ctrl;
                    if (ddl.ClientID == ddl1.ClientID)
                    {
                        Label itemname = (Label)ctrlitemname;
                        selecteditem = ddl1.SelectedValue;
                        string selectquery = " select empmname from EmpDetails where empid = " + selecteditem;
                        DataTable dtitemanme = config.ExecuteAdaptorAsyncWithQueryParams(selectquery).Result;
                        itemname.Text = dtitemanme.Rows[0][0].ToString();
                        break;
                    }
                }
            }
        }
    }
}