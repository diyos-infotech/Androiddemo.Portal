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
using System.Data.SqlClient;
using System.Web.Script.Serialization;
using System.IO;
using Jawan.Portal.DAL;
namespace Jawan.Portal
{
    public partial class Login : System.Web.UI.Page
    {
        AppConfiguration config = new AppConfiguration();
        GridViewExportUtil gve = new GridViewExportUtil();
        protected void Page_Load(object sender, EventArgs e)
        {
            lblerror.Text = "";
            if (!IsPostBack)
            {

                Session["UserId"] = string.Empty;
                Session["AccessLevel"] = string.Empty;

                Session["EmpIDPrefix"] = string.Empty;
                Session["CmpIDPrefix"] = string.Empty;
              

                Session["BillnoWithoutST"] = string.Empty;
                Session["BillnoWithST"] = string.Empty;

                Session["BillprefixWithST"] = string.Empty;
                Session["BillprefixWithoutST"] = string.Empty;

                // InitDatabase();
                lblcname.Text = SqlHelper.Instance.GetCompanyname();

                Session["EmpIDPrefix"] = string.Empty;
                Session["CmpIDPrefix"] = string.Empty;
                Session["BillnoWithoutST"] = string.Empty;
                Session["BillnoWithST"] = string.Empty;
                Session["BillprefixWithST"] = string.Empty;
                Session["BillprefixWithoutST"] = string.Empty;
                Session["InvPrefix"] = string.Empty;
                Session["POPrefix"] = string.Empty;
                Session["GRVPrefix"] = string.Empty;
                Session["DCPrefix"] = string.Empty;
                Session["BranchID"] = string.Empty;
                Session["BranchIDHome"] = string.Empty;
                Session["HomeBranch"] = string.Empty;
            }
          
            
            //  InitDatabase();
        }

        //void InitDatabase()
        //{
        //    try
        //    {
        //        string _Connectionstring = System.Configuration.ConfigurationManager.ConnectionStrings["KLTSConnectionString"].ConnectionString;
        //        ConnectionStrings.ConnectionString = _Connectionstring;
        //    }
        //    catch (Exception e)
        //    {
        //        //GlobalData.Instance.AppendLog(e.Message);
        //        lblerror.Text = e.Message;
        //    }
        //}

        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            try
            {
                #region Begin Code For  Variable Decalration as on  [01-10-2013]
                var UserName = string.Empty;
                var password = string.Empty;
                var SPName = string.Empty;
                #endregion  Begin Code For  Variable Decalration as on  [01-10-2013]

                #region Begin Code For  Assign Values To   Variables   as on  [01-10-2013]
                UserName = txtUserName.Text.Trim();
                password = txtPassword.Text.Trim();
                SPName = "CheckCredentials";
                #endregion  Begin Code For Assign Values To   Variables   as on  [01-10-2013]

                #region    Begin Old  Code  Before  [01-10-2013]
                //AppendLog("In Submit button");
                // string userid = txtUserName.Text.Trim();
                //  string Pwd = txtPassword.Text.Trim();
                // //   String ipAddress = "* IP * - " + System.Web.HttpContext.Current.Request.UserHostAddress;   on  03/19/2013
                //String ipAddress = "";
                //  //  GlobalData.Instance.AppendLog( ipAddress);
                //  //string selectquery = "select * from LoginDetails where UserName = '" + userid + "' and password ='" + Pwd + "'";
                #endregion  End  Old  Code  Before  [01-10-2013]

                #region  Begin Code For  SP PArameters / Calling Stored Procedure as on [01-10-2013]

                Hashtable HTSpParameters = new Hashtable();
                HTSpParameters.Add("@UserName", UserName);
                HTSpParameters.Add("@password", password);
                DataTable DtCheckCredentials = config.ExecuteAdaptorAsyncWithParams(SPName, HTSpParameters).Result;

                #endregion  End  Code For SP PArameters / Calling Stored Procedure as on [01-10-2013]


                #region Begin Code For Display Message If Invalid Credentials as on [01-10-2013]
                if (DtCheckCredentials.Rows.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Invalid UserName/Password');", true);
                    return;
                }
                #endregion  End Code For Display Message If Invalid Credentials as on [01-10-2013]


                if (DtCheckCredentials.Rows.Count > 0)
                {
                    //AppendLog("got user details");
                    Session["Emp_Id"] = DtCheckCredentials.Rows[0]["Emp_Id"].ToString();
                    Session["UserId"] = DtCheckCredentials.Rows[0]["username"].ToString();
                    Session["BranchID"] = DtCheckCredentials.Rows[0]["CompanyBranch"].ToString();
                    Session["AccessLevel"] = DtCheckCredentials.Rows[0]["previligeid"].ToString();
                    Session["BranchIDHome"] = DtCheckCredentials.Rows[0]["CompanyBranch"].ToString();
                    Session["HomeBranch"] = DtCheckCredentials.Rows[0]["CompanyBranch"].ToString();
                    GetBranchwise();



                    #region for Payment Alert

                    string UpdateLogin = "update logindetails set LastLoggedIn='" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "' ";
                    int UpdateStatus = config.ExecuteNonQueryWithQueryAsync(UpdateLogin).Result;

                    string qry = "select Loginstatus,LoginStatusRemarks,LoginTypeRemarks,LoginType from logindetails";
                    DataTable dt = SqlHelper.Instance.GetTableByQuery(qry);


                    string LoginStatusRemarks = "";
                    string Loginstatus = "";
                    string LoginTypeRemarks = "";
                    bool LoginType = false;

                    if (dt.Rows.Count > 0)
                    {
                        Loginstatus = dt.Rows[0]["Loginstatus"].ToString().ToUpper();
                        LoginStatusRemarks = dt.Rows[0]["LoginStatusRemarks"].ToString();
                        LoginTypeRemarks = dt.Rows[0]["LoginTypeRemarks"].ToString();
                        LoginType = bool.Parse(dt.Rows[0]["LoginType"].ToString());
                    }

                    if (Loginstatus == "INACTIVE")
                    {

                        string title = "Alert!";
                        hfv.Value = Loginstatus;
                        string body = LoginStatusRemarks;
                        string BtnText = "Ok";
                        string Width = "50";
                        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + title + "', '" + body + "','" + BtnText + "','" + Width + "');", true);
                    }
                    else
                    {
                        if (LoginType == true)
                        {
                            string title = "Immediate Action Required!";
                            hfv.Value = Loginstatus;
                            string body = LoginTypeRemarks;
                            string BtnText = "Ok, Proceed";
                            string Width = "120";
                            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + title + "', '" + body + "','" + BtnText + "','" + Width + "');", true);
                        }
                        else
                        {

                            switch (int.Parse(Session["AccessLevel"].ToString()))
                            {
                                case 1: //Admin
                                    Session["homepage"] = "Employees.aspx";
                                    //Response.Redirect("Employees.aspx");
                                    Response.Redirect("Reminders.aspx");

                                    //AppendLog("Redirecting to Employees");
                                    break;
                                case 2: //RO
                                    Session["homepage"] = "TemproryEmployeeTransferList.aspx";
                                    Response.Redirect("TemproryEmployeeTransferList.aspx");
                                    break;

                                case 3: //Accounts
                                    Session["homepage"] = "EmployeeAttendance.aspx";
                                    Response.Redirect("EmployeeAttendance.aspx");
                                    break;
                                case 4: //AdminDept
                                    Session["homepage"] = "PostingOrderList.aspx";
                                    Response.Redirect("PostingOrderList.aspx");
                                    break;
                                case 5: //OPM
                                    Session["homepage"] = "EmployeeAttendance.aspx";
                                    Response.Redirect("EmployeeAttendance.aspx");
                                    break;
                                case 6: //InventoryManagers
                                    Session["homepage"] = "MaterialRequisitForm.aspx";
                                    Response.Redirect("MaterialRequisitForm.aspx");
                                    break;
                                default:
                                    break;

                            }
                        }

                        #endregion
                    }

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Invalid UserName/Password');", true);
                    Response.Redirect("~/login.aspx");
                }
                
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Invalid UserName/Password.Your session expiered');", true);
            }



        }

     public void GetBranchwise()

        {
            string SqlQryBranchPrefix = "Select * from Branchdetails  Where branchid='" + Session["BranchID"] + "'";
            DataTable DtBranchPrefix = config.ExecuteAdaptorAsyncWithQueryParams(SqlQryBranchPrefix).Result;
            if (DtBranchPrefix.Rows.Count > 0)
            {
                Session["EmpIDPrefix"] = DtBranchPrefix.Rows[0]["EmpPrefix"].ToString();
                Session["CmpIDPrefix"] = DtBranchPrefix.Rows[0]["ClientIDPrefix"].ToString();
                Session["BillnoWithST"] = DtBranchPrefix.Rows[0]["BillnoWithServicetax"].ToString();
                Session["BillnoWithoutST"] = DtBranchPrefix.Rows[0]["BillNoWithoutServiceTax"].ToString();
                Session["BillprefixWithST"] = DtBranchPrefix.Rows[0]["BillprefixWithST"].ToString();
                Session["BillprefixWithoutST"] = DtBranchPrefix.Rows[0]["BillprefixWithoutST"].ToString();
                Session["InvPrefix"] = DtBranchPrefix.Rows[0]["InvPrefix"].ToString();
                Session["POPrefix"] = DtBranchPrefix.Rows[0]["POPrefix"].ToString();
                Session["GRVPrefix"] = DtBranchPrefix.Rows[0]["GRVPrefix"].ToString();
                Session["DCPrefix"] = DtBranchPrefix.Rows[0]["DCPrefix"].ToString();
                Session["BranchID"] = DtBranchPrefix.Rows[0]["BranchID"].ToString();
                Session["BranchIDHome"] = DtBranchPrefix.Rows[0]["BranchID"].ToString();
                Session["HomeBranch"] = DtBranchPrefix.Rows[0]["BranchID"].ToString();
            }
        }  
            
        
        protected string GetOrderID()
        {
            string id = "1";
            string selectqueryoderid = "select max(cast(OrderId as int)) as OrderId from EmpPostingOrder ";
            DataTable dtable = config.ExecuteAdaptorAsyncWithQueryParams(selectqueryoderid).Result;
            if (dtable.Rows.Count > 0)
            {
                if (String.IsNullOrEmpty(dtable.Rows[0]["OrderId"].ToString()) == false)
                {
                    int oderid = (Convert.ToInt32(dtable.Rows[0]["OrderId"].ToString())) + 1;
                    id = oderid.ToString();
                }
            }
            return id;
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }

    }
}