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
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using KLTS.Data;
using System.Globalization;
using System.Collections.Generic;
using Jawan.Portal.DAL;
namespace Jawan.Portal
{
    public partial class TaxComponents : System.Web.UI.Page
    {
        AppConfiguration config = new AppConfiguration();
        GridViewExportUtil gve = new GridViewExportUtil();
        string EmpIDPrefix = "";
        string CmpIDPrefix = "";
        string Fontstyle = "";
        string CFontstyle = "";
        string Created_By = "";

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


                    }
                    else
                    {
                        Response.Redirect("Login.aspx");
                    }

                    GetTaxComponentsdata();

                }
            }
            catch (Exception ex)
            {

            }
        }

        public void DisplayLinks(string PID)
        {
            int value = 0;
            bool PreviligerStatus = int.TryParse(PID, out  value);
            if (PreviligerStatus == true && value != 0)
            {
                switch (value)
                {
                    case 1:
                        break;
                    case 2:

                        ClientsLink.Visible = false;
                        CompanyInfoLink.Visible = false;
                        InventoryLink.Visible = true;
                        ReportsLink.Visible = true;

                        SettingsLink.Visible = false;
                        LogOutLink.Visible = true;



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
            Created_By = Session["UserID"].ToString();
        }



        public void GetTaxComponentsdata()
        {

            string qry = "select TaxCmpID,TaxCmpName,TaxCmpPer,case visibility when 'Y' then cast('True' as bit) else '' end Visibility from TaxComponentsMaster ";
            DataTable dt = config.ExecuteAdaptorAsyncWithQueryParams(qry).Result;

            if (dt.Rows.Count > 0)
            {
                GVTaxComponents.DataSource = dt;
                GVTaxComponents.DataBind();
            }
            else
            {
                GVTaxComponents.DataSource = null;
                GVTaxComponents.DataBind();
            }

        }




        protected void GVTaxComponents_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GVTaxComponents.EditIndex = e.NewEditIndex;
            GetTaxComponentsdata();

        }
        protected void GVTaxComponents_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GVTaxComponents.EditIndex = -1;
            GetTaxComponentsdata();
        }
        protected void GVTaxComponents_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string CmpName = "";
            string Visibility = "N";
            string CmpID = "";
            string CmpPer = "0";
            var IRecordStatus = 0;

            Hashtable HtSPParameters = new Hashtable();
            var ProcedureName = string.Empty;

            Label lblEditCmpID = GVTaxComponents.Rows[e.RowIndex].FindControl("lblEditCmpID") as Label;
            TextBox txtComponent = GVTaxComponents.Rows[e.RowIndex].FindControl("txtComponent") as TextBox;
            TextBox txtCmpper = GVTaxComponents.Rows[e.RowIndex].FindControl("txtCmpper") as TextBox;
            CheckBox ChkEditVisibility = GVTaxComponents.Rows[e.RowIndex].FindControl("ChkEditVisibility") as CheckBox;

            CmpName = txtComponent.Text;
            CmpID = lblEditCmpID.Text;
            CmpPer = txtCmpper.Text;

            if (ChkEditVisibility.Checked)
            {
                Visibility = "Y";
            }

            ProcedureName = "ModifyTaxComponentsMaster";
            HtSPParameters.Add("@TaxCmpname", CmpName);
            HtSPParameters.Add("@Visibility", Visibility);
            HtSPParameters.Add("@Taxcmpid", CmpID);
            HtSPParameters.Add("@TaxCmpPer", CmpPer);
            IRecordStatus = config.ExecuteNonQueryParamsAsync(ProcedureName, HtSPParameters).Result;

            if (IRecordStatus > 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Component data updated sucessfully.');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Component data is not updated sucessfully.');", true);
            }

            GVTaxComponents.EditIndex = -1;
            GetTaxComponentsdata();
        }
    }
}