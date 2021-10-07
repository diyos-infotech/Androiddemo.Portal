using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using KLTS.Data;
using Jawan.Portal.DAL;

namespace Jawan.Portal
{
    public partial class Create_Tab_App_Passwords : System.Web.UI.Page
    {
        AppConfiguration config = new AppConfiguration();
        GridViewExportUtil gve = new GridViewExportUtil();
        DataTable dt;
        string EmpIDPrefix = "";
        string CmpIDPrefix = "";
        string BranchID = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                EmpIDPrefix = Session["EmpIDPrefix"].ToString();
                CmpIDPrefix = Session["CmpIDPrefix"].ToString();
                BranchID = Session["BranchID"].ToString();
                if (!IsPostBack)
                {
                    LoadClientids();
                }
            }

            catch (Exception ex)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Your Session Expired.Please Login');", true);
                Response.Redirect("~/login.aspx");
            }
        }



        protected void LoadClientids()
        {
            DataTable dtBranch = GlobalData.Instance.LoadBranchOnUserID(BranchID);

            DataTable dt = GlobalData.Instance.LoadCIds(CmpIDPrefix, BranchID);
            if (dt.Rows.Count > 0)
            {
                ddlsiteposted.DataValueField = "clientid";
                ddlsiteposted.DataTextField = "clientid";
                ddlsiteposted.DataSource = dt;
                ddlsiteposted.DataBind();
            }

            ddlsiteposted.Items.Insert(0, new ListItem("-Select-", "0"));
        }




        protected void BtnSave_Click(object sender, EventArgs e)
        {

           

            if (txtusrname.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Please fill User Name');", true);
                return;
            }

            if (txtPassword.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Please fill Password');", true);
                return;
            }

            if (txtConfirmPassword.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Please fill Confirm password');", true);
                return;
            }
            if (txtPassword.Text.Trim() != txtConfirmPassword.Text.Trim())
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Password does not match');", true);
                return;
            }

            string alphabets = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string numbers = "1234567890";

            string characters = numbers;

            characters += alphabets + numbers;

            int length = 36;
            string RandomID = string.Empty;
            for (int i = 0; i < length; i++)
            {
                string character = string.Empty;
                do
                {
                    int index = new Random().Next(0, characters.Length);
                    character = characters.ToCharArray()[index].ToString();
                } while (RandomID.IndexOf(character) != -1);
                RandomID += character;
            }


          
                string ChkQry = "select * from Logindetails_Android where companyid=6  ";
                DataTable chkresult = config.PocketFameExecuteAdaptorAsyncWithQueryParams(ChkQry).Result;
                if (chkresult.Rows.Count > 0)
                {
                    string updatequery = "Update Logindetails_Android set UserName='" + txtusrname.Text.Trim() + "',Password='" + txtConfirmPassword.Text.Trim() + "',UpdatedDateTime=getdate() where clientid='" + ddlsiteposted.SelectedValue + "' and companyid=6";
                    int updateresult = config.PocketFameExecuteNonQueryWithQueryAsync(updatequery).Result;
                    if (updateresult > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Details modified successfully');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Details modified Unsuccessfully');", true);
                        return;
                    }

                }
                else
                {
                    string insertquery = "insert into Logindetails_Android (UserName,	Password,	CompanyId,	CreatedDateTime	,UpdatedDateTime,ClientId) values ('" + txtusrname.Text.Trim() + "','" + txtConfirmPassword.Text.Trim() + "',6,Getdate(),Getdate(),'"+ddlsiteposted.SelectedValue+"')";
                    int insertresult = config.PocketFameExecuteNonQueryWithQueryAsync(insertquery).Result;
                    if (insertresult > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Details saved successfully');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Details saved Unsuccessfully');", true);
                        return;
                    }
                }

                txtPassword.Text = "";
                txtConfirmPassword.Text = "";
        }

    }
}