using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using KLTS.Data;
using System.Globalization;
using Jawan.Portal.DAL;

namespace Jawan.Portal
{
    public partial class Create_UserFeature : System.Web.UI.Page
    {
        AppConfiguration config = new AppConfiguration();
        GridViewExportUtil gve = new GridViewExportUtil();
        string EmpIDPrefix = "";
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
                        lblDisplayUser.Text = Session["UserId"].ToString();
                        // PreviligeUsers(Convert.ToInt32(Session["AccessLevel"]));
                        lblcname.Text = SqlHelper.Instance.GetCompanyname();
                    }
                    else
                    {
                        Response.Redirect("login.aspx");
                    }


                    var formatInfoinfo = new DateTimeFormatInfo();
                    string[] monthName = formatInfoinfo.MonthNames;
                    string month = monthName[DateTime.Now.Month - 1];


                }
            }
            catch (Exception ex)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "Show alert", "alert('Your Session Expired');", true);
                Response.Redirect("~/Login.aspx");
            }
        }
        protected void txtemplyid_TextChanged(object sender, EventArgs e)
        {

            GetEmpName();

        }

        protected void GetEmpName()
        {
            string Sqlqry = "select (empfname+' '+empmname+' '+emplname) as empname from empdetails where empid='" + txtemplyid.Text + "' ";
            DataTable dt = config.ExecuteAdaptorAsyncWithQueryParams(Sqlqry).Result;
            if (dt.Rows.Count > 0)
            {
                try
                {
                    txtFname.Text = dt.Rows[0]["empname"].ToString();

                }
                catch (Exception ex)
                {
                    // MessageLabel.Text = ex.Message;
                }
            }
            else
            {
                // MessageLabel.Text = "There Is No Name For The Selected Employee";
            }


        }

        protected void Getempid()
        {

            string Sqlqry = "select  empid from empdetails where empfname+' '+empmname+' '+emplname like '%" + txtFname.Text + "%' ";
            DataTable dt = config.ExecuteAdaptorAsyncWithQueryParams(Sqlqry).Result;
            if (dt.Rows.Count > 0)
            {
                try
                {
                    txtemplyid.Text = dt.Rows[0]["empid"].ToString();

                }
                catch (Exception ex)
                {
                    // MessageLabel.Text = ex.Message;
                }
            }
            else
            {
                // MessageLabel.Text = "There Is No Name For The Selected Employee";
            }

        }


        protected void txtFname_TextChanged(object sender, EventArgs e)
        {
            Getempid();

        }


        protected void GetWebConfigdata()
        {
            EmpIDPrefix = Session["EmpIDPrefix"].ToString();
            CmpIDPrefix = Session["CmpIDPrefix"].ToString();
        }
        protected void btnsave_Click(object sender, EventArgs e)
        {
            var Empid = txtemplyid.Text;
            var PinMyVisit = "0";
            var RoutePlan = "0";
            var Attendance = "0";
            var UserFeatureId = 0;
            if (rdbpinYes.Checked==true)
            {
                PinMyVisit = "1";
            }
            else
            {
                PinMyVisit = "0";
            }
            if (rdbryes.Checked == true)
            {
                RoutePlan = "1";
            }
            else
            {
                RoutePlan = "0";
            }
            if (rdbAttandanceYes.Checked == true)
            {
                Attendance = "1";
            }
            else
            {
                Attendance = "0";
            }


            string query = "select empid from UserFeature where empid='" + Empid + "'";
            DataTable dt = config.ExecuteAdaptorAsyncWithQueryParams(query).Result;
            if (dt.Rows.Count>0)
            {
                string updatequery = "update UserFeature set PinMyVisit='" + PinMyVisit + "',RoutePlan='" + RoutePlan + "',Attendance='" + Attendance + "' where empid='"+ Empid + "' ";
                int Result = config.PocketFameExecuteNonQueryWithQueryAsync(updatequery).Result;
                if (Result>0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Showalert()", "alert('User Feature Updated Successfully ')", true);
                    return;
                }
            }
            else
            {
                string insertquery = "insert into UserFeature(Empid,PinMyVisit,RoutePlan,Attendance) values ('"+ Empid + "','"+ PinMyVisit + "','"+ RoutePlan + "','"+ Attendance + "') ";
                int insertResult = config.PocketFameExecuteNonQueryWithQueryAsync(insertquery).Result;
                if (insertResult > 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Showalert()", "alert('User Feature Inserted Successfully ')", true);
                    return;
                }
            }
        }
    }
}