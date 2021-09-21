using System;
using System.Collections;
using KLTS.Data;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI;

namespace Jawan.Portal
{
    public partial class M_Leads_Report : System.Web.UI.Page
    {
        Marketinghelper MH = new Marketinghelper();
        GridViewExportUtil gvu = new GridViewExportUtil();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserId"] != null && Session["AccessLevel"] != null)
                {

                    lblDisplayUser.Text = Session["UserId"].ToString();
                    lblcname.Text = SqlHelper.Instance.GetCompanyname();

                }
                else
                {
                    Response.Redirect("login.aspx");
                }

            }
        }



        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            LnkExcel.Visible = true;
            GVLeadDetails.DataSource = null;
            GVLeadDetails.DataBind();

            if (txtFromDate.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please select From Date')", true);
                return;
            }

            if (txtToDate.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please select To Date')", true);
                return;
            }


            var FromDate = "01/01/1900";
            if (txtFromDate.Text.Trim().Length != 0)
            {
                FromDate = Timings.Instance.CheckDateFormat(txtFromDate.Text);
            }

            var ToDate = "01/01/1900";
            if (txtToDate.Text.Trim().Length != 0)
            {
                ToDate = Timings.Instance.CheckDateFormat(txtToDate.Text);
            }

            string SPName = "M_Reports";
            string Type = "";

            if (ddlType.SelectedIndex == 1)
            {
                Type = "GetLeadDetails";
            }
            else
            {
                Type = "GetLeadStatusDetails";
            }

            Hashtable ht = new Hashtable();
            ht.Add("@Type", Type);
            ht.Add("@FromDate", FromDate);
            ht.Add("@ToDate", ToDate);

            DataTable dt = SqlHelper.Instance.ExecuteStoredProcedureWithParams(SPName, ht);

            if (dt.Rows.Count > 0)
            {
                LnkExcel.Visible = true;
                GVLeadDetails.DataSource = dt;
                GVLeadDetails.DataBind();
            }

        }

        protected void LnkExcel_Click(object sender, EventArgs e)
        {
            gvu.Export("Lead Details.xls", this.GVLeadDetails);
        }
    }
}