using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using KLTS.Data;
using Jawan.Portal.DAL;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

namespace Jawan.Portal
{
    public partial class Individual_Geo_Fence : System.Web.UI.Page
    {
        AppConfiguration config = new AppConfiguration();
        GridViewExportUtil gve = new GridViewExportUtil();
        string EmpIDPrefix = "";
        string CmpIDPrefix = "";
        string BranchID = "";
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

                    LoadClientids();
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

        protected void GetWebConfigdata()
        {
            EmpIDPrefix = Session["EmpIDPrefix"].ToString();
            CmpIDPrefix = Session["CmpIDPrefix"].ToString();
            BranchID = Session["BranchID"].ToString();
        }
        protected void btnsave_Click(object sender, EventArgs e)
        {
          
            var hUBname = txthUBname.Text;
            var Lat = txtLat.Text;
            var Lng = txtLng.Text;
            var Distance = txtDistance.Text;
            var ClientID = ddlsiteposted.SelectedValue;
            var Address = txtAddress.Text;


            string insertquery = "insert into Hubs (HubName,Lat,Lng,CheckinDistance,clientid,Address) values ('" + hUBname + "','" + Lat + "','" + Lng + "','" + Distance + "','" + ClientID + "','"+ Address + "')";
            int insertResult = config.ExecuteNonQueryWithQueryAsync(insertquery).Result;
            if (insertResult > 0)
            {
                tblempdetails.Visible = true;
                tblhubs.Visible = false;
                string getdata = "select distinct hubid from Hubs where HubName='" + hUBname + "' and clientid='" + ClientID + "' ";
                DataTable dtdata = config.ExecuteAdaptorAsyncWithQueryParams(getdata).Result;
                if (dtdata.Rows.Count>0)
                {
                    txthubid.Text = dtdata.Rows[0]["hubid"].ToString();
                }
            }

        }

        protected void btnempsave_Click(object sender, EventArgs e)
        {
            var Empid = txtemplyid.Text;
            var hubid = txthubid.Text;
            string insertquery = "insert into EmpHub (Empid,hubid) values ('" + Empid + "','"+ hubid + "')";
            int insertResult = config.ExecuteNonQueryWithQueryAsync(insertquery).Result;
            if (insertResult>0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Details saved successfully');", true);
                return;
            }
        }
    }
}