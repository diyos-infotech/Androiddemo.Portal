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
using Jawan.Portal.DAL;
namespace Jawan.Portal
{
    public partial class Minimum_Wages : System.Web.UI.Page
    {
        AppConfiguration config = new AppConfiguration();
        GridViewExportUtil gve = new GridViewExportUtil();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
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
                    Displaydata();
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void PreviligeUsers(int previligerid)
        {

            switch (previligerid)
            {

                case 1:
                    break;
                case 2:


                    ClientsLink.Visible = true;
                    CompanyInfoLink.Visible = false;
                    InventoryLink.Visible = false;
                    ReportsLink.Visible = true;
                    SettingsLink.Visible = true;
                    break;

                case 3:
                    ClientsLink.Visible = true;
                    CompanyInfoLink.Visible = false;
                    InventoryLink.Visible = false;
                    ReportsLink.Visible = true;
                    SettingsLink.Visible = true;
                    Response.Redirect("BillingAndSalary.aspx");
                    break;

                case 4:

                    EmployeesLink.Visible = true;
                    ClientsLink.Visible = true;
                    CompanyInfoLink.Visible = true;
                    InventoryLink.Visible = true;
                    ReportsLink.Visible = true;
                    SettingsLink.Visible = true;
                    Response.Redirect("Segment.aspx");
                    break;
                case 5:
                    SettingsLink.Visible = true;


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
                    InventoryLink.Visible = false;
                    ReportsLink.Visible = false;
                    SettingsLink.Visible = false;

                    break;
                default:
                    break;


            }
        }

        protected void btnsave_Click(object sender, EventArgs e)
        {

        }

        protected void btncancel_Click(object sender, EventArgs e)
        {

        }

        private void Displaydata()
        {
            DataTable DtMinimum_Wages_Categories = GlobalData.Instance.LoadMinimumWagesCategories();
            if (DtMinimum_Wages_Categories.Rows.Count > 0)
            {
                Gv_Minimum_Wages.DataSource = DtMinimum_Wages_Categories;
                Gv_Minimum_Wages.DataBind();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Records Are Not Avialable');", true);
                return;
            }
        }


        protected void Gv_Minimum_Wages_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

            try
            {

                #region  Begin  Code  for  Retrive  Data From  Gridview as on [14-10-2013]
                Label Lbl_CategoryId = Gv_Minimum_Wages.Rows[e.RowIndex].FindControl("lbl_Category") as Label;
                Label Lbl_Category_Name = Gv_Minimum_Wages.Rows[e.RowIndex].FindControl("lbl_Category_Name") as Label;


                TextBox Txt_Basic = Gv_Minimum_Wages.Rows[e.RowIndex].FindControl("Txt_Basic") as TextBox;
                TextBox Txt_Da = Gv_Minimum_Wages.Rows[e.RowIndex].FindControl("Txt_Da") as TextBox;
                TextBox Txt_Hra = Gv_Minimum_Wages.Rows[e.RowIndex].FindControl("Txt_Hra") as TextBox;
                TextBox Txt_Conv = Gv_Minimum_Wages.Rows[e.RowIndex].FindControl("Txt_Conv") as TextBox;

                TextBox Txt_Cca = Gv_Minimum_Wages.Rows[e.RowIndex].FindControl("Txt_Cca") as TextBox;
                TextBox Txt_LeaveAmount = Gv_Minimum_Wages.Rows[e.RowIndex].FindControl("Txt_LeaveAmount") as TextBox;
                TextBox Txt_Gratuity = Gv_Minimum_Wages.Rows[e.RowIndex].FindControl("Txt_Gratuity") as TextBox;
                TextBox Txt_Bonus = Gv_Minimum_Wages.Rows[e.RowIndex].FindControl("Txt_Bonus") as TextBox;

                TextBox Txt_WashAllownce = Gv_Minimum_Wages.Rows[e.RowIndex].FindControl("Txt_WashAllownce") as TextBox;
                TextBox Txt_OtherAllowance = Gv_Minimum_Wages.Rows[e.RowIndex].FindControl("Txt_OtherAllowance") as TextBox;
                TextBox Txt_Nfhs = Gv_Minimum_Wages.Rows[e.RowIndex].FindControl("Txt_Nfhs") as TextBox;
                TextBox Txt_Rc = Gv_Minimum_Wages.Rows[e.RowIndex].FindControl("Txt_Rc") as TextBox;

                TextBox Txt_Cs = Gv_Minimum_Wages.Rows[e.RowIndex].FindControl("Txt_Cs") as TextBox;

                #endregion End  Code  for  Retrive  Data From  Gridview as on [14-10-2013]


                #region  Begin  Code  for  Variable  Declaration as on [14-10-2013]

                var Basic = 0.00;
                var Da = 0.00;
                var Hra = 0.00;
                var Conv = 0.00;

                var Cca = 0.00;
                var LeaveAmount = 0.00;
                var Gratuity = 0.00;
                var Bonus = 0.00;

                var WashAllownce = 0.00;
                var OtherAllowance = 0.00;
                var Nfhs = 0.00;
                var Rc = 0.00;
                var Cs = 0.00;

                var Categories_name = string.Empty;
                var Categories_Id = string.Empty;
                var ProcedureName = string.Empty;
                var IRecordStatus = 0;
                Hashtable HtSPParameters = new Hashtable();
                #endregion End  Code  for  Variable  Declaration as on [14-10-2013]

                #region  Begin  Code  for  Assign Values to  Variables as on [14-10-2013]
                Categories_Id = Lbl_CategoryId.Text.Trim().ToUpper();
                Categories_name = Lbl_Category_Name.Text.Trim().ToUpper();
                ProcedureName = "AMMinimumWagesCategories";

                #region  Basic
                if (Txt_Basic.Text.Trim().Length > 0)
                {
                    if (float.Parse(Txt_Basic.Text) > 0)
                    {
                        Basic = float.Parse(Txt_Basic.Text);
                    }
                }
                #endregion

                #region Da
                if (Txt_Da.Text.Trim().Length > 0)
                {
                    if (float.Parse(Txt_Da.Text) > 0)
                    {
                        Da = float.Parse(Txt_Da.Text);
                    }
                }
                #endregion
                #region HRA
                if (Txt_Hra.Text.Trim().Length > 0)
                {
                    if (float.Parse(Txt_Hra.Text) > 0)
                    {
                        Hra = float.Parse(Txt_Hra.Text);
                    }
                }
                #endregion
                #region Conveyance
                if (Txt_Conv.Text.Trim().Length > 0)
                {
                    if (float.Parse(Txt_Conv.Text) > 0)
                    {
                        Conv = float.Parse(Txt_Conv.Text);
                    }
                }
                #endregion
                #region  CCA
                if (Txt_Cca.Text.Trim().Length > 0)
                {
                    if (float.Parse(Txt_Cca.Text) > 0)
                    {
                        Cca = float.Parse(Txt_Cca.Text);
                    }
                }
                #endregion
                #region Leave Amount
                if (Txt_LeaveAmount.Text.Trim().Length > 0)
                {
                    if (float.Parse(Txt_LeaveAmount.Text) > 0)
                    {
                        LeaveAmount = float.Parse(Txt_LeaveAmount.Text);
                    }
                }
                #endregion
                #region Gratutity
                if (Txt_Gratuity.Text.Trim().Length > 0)
                {
                    if (float.Parse(Txt_Gratuity.Text) > 0)
                    {
                        Gratuity = float.Parse(Txt_Gratuity.Text);
                    }
                }
                #endregion

                #region Bonus
                if (Txt_Bonus.Text.Trim().Length > 0)
                {
                    if (float.Parse(Txt_Bonus.Text) > 0)
                    {
                        Bonus = float.Parse(Txt_Bonus.Text);
                    }
                }
                #endregion Bonus
                #region Wash Allowance
                if (Txt_WashAllownce.Text.Trim().Length > 0)
                {
                    if (float.Parse(Txt_WashAllownce.Text) > 0)
                    {
                        WashAllownce = float.Parse(Txt_WashAllownce.Text);
                    }
                }
                #endregion
                #region Other Allowance
                if (Txt_OtherAllowance.Text.Trim().Length > 0)
                {
                    if (float.Parse(Txt_OtherAllowance.Text) > 0)
                    {
                        OtherAllowance = float.Parse(Txt_OtherAllowance.Text);
                    }
                }
                #endregion
                #region NFHS
                if (Txt_Nfhs.Text.Trim().Length > 0)
                {
                    if (float.Parse(Txt_Nfhs.Text) > 0)
                    {
                        Nfhs = float.Parse(Txt_Nfhs.Text);
                    }
                }
                #endregion
                #region  RC
                if (Txt_Rc.Text.Trim().Length > 0)
                {
                    if (float.Parse(Txt_Rc.Text) > 0)
                    {
                        Rc = float.Parse(Txt_Rc.Text);
                    }
                }
                #endregion
                #region CS
                if (Txt_Cs.Text.Trim().Length > 0)
                {
                    if (float.Parse(Txt_Cs.Text) > 0)
                    {
                        Cs = float.Parse(Txt_Cs.Text);
                    }
                }
                #endregion

                #endregion End  Code  for  Assign Values to  Variables as on [14-10-2013]

                #region  Begin  Code  for  Assign Values to  SP Parameters as on [14-10-2013]
                HtSPParameters.Add("@Id", Categories_Id);
                HtSPParameters.Add("@Name", Categories_name);
                HtSPParameters.Add("@Basic", Basic);
                HtSPParameters.Add("@Da", Da);
                HtSPParameters.Add("@HRA", Hra);



                HtSPParameters.Add("@Conv", Conv);
                HtSPParameters.Add("@Cca", Cca);
                HtSPParameters.Add("@LeaveAmount", LeaveAmount);
                HtSPParameters.Add("@Gratuity", Gratuity);
                HtSPParameters.Add("@Bonus", Bonus);

                HtSPParameters.Add("@WashAllownce", WashAllownce);
                HtSPParameters.Add("@OtherAllowance", OtherAllowance);
                HtSPParameters.Add("@Nfhs", Nfhs);
                HtSPParameters.Add("@Rc", Rc);
                HtSPParameters.Add("@CS", Cs);

                #endregion End  Code  for  Assign Values to  SP Parameters as on [14-10-2013]

                #region  Begin Code For Calling Stored Procedure As on [14-10-2013]
                IRecordStatus = config.ExecuteNonQueryParamsAsync(ProcedureName, HtSPParameters).Result;
                #endregion  End Code For Calling Stored Procedure As on [14-10-2013]

                #region  Begin Code For Display Status Of the Record as on [14-10-2013]
                if (IRecordStatus > 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Record  Updated  SucessFully.');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Record Not  Updated.Because  The Name Already Exist. NOTE: Names Are UNIQUE');", true);
                }
                #endregion  End Code For Display Status Of the Record as on [14-10-2013]

                #region  Begin Code For Re-Call All the Departments As on [14-10-2013]
                Gv_Minimum_Wages.EditIndex = -1;
                Displaydata();
                #endregion End Code For Re-Call All the Departments As on [14-10-2013]

            }

            catch (Exception ex)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Your session time out...');", true);
                return;
            }


        }

        protected void Gv_Minimum_Wages_RowEditing(object sender, GridViewEditEventArgs e)
        {
            Gv_Minimum_Wages.EditIndex = e.NewEditIndex;
            Displaydata();
        }

        protected void Gv_Minimum_Wages_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            Gv_Minimum_Wages.EditIndex = -1;
            Displaydata();
        }
    }
}