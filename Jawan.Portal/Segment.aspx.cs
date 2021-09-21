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

namespace Jawan.Portal
{
    public partial class Segment : System.Web.UI.Page
    {
        private void Displaydata()
    {
        Txt_Segment.Text = "";
        DataTable DtSegments = GlobalData.Instance.LoadSegments();
        if (DtSegments.Rows.Count > 0)
        {
            gvSegment.DataSource = DtSegments;
            gvSegment.DataBind();
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Segment Names Are Not Avialable');", true);
            return;
        }
    }

    protected void PreviligeUsers(int previligerid)
    {

        switch (previligerid)
        {

            case 1:
                break;
            case 2:
                EmployeesLink.Visible = false;
                ClientsLink.Visible = false;
                CompanyInfoLink.Visible = false;
                InventoryLink.Visible = false;
                ReportsLink.Visible = false;
                SettingsLink.Visible = false;
              

                break;

            case 3:
                EmployeesLink.Visible = false;
                ClientsLink.Visible = false;
                CompanyInfoLink.Visible = false;
                InventoryLink.Visible = false;
                ReportsLink.Visible = false;
                SettingsLink.Visible = false;
                
                break;

            case 4:      
                EmployeesLink.Visible = true;
                ClientsLink.Visible = true;
                CompanyInfoLink.Visible = true;
                InventoryLink.Visible = true;
                ReportsLink.Visible = true;
                SettingsLink.Visible = true;
                
               
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

    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {

            if (!IsPostBack)
            {
                if (Session["UserId"] != null && Session["AccessLevel"] != null)
                {
                    lblDisplayUser.Text = Session["UserId"].ToString();
                    PreviligeUsers(Convert.ToInt32(Session["AccessLevel"]));
                    lblcname.Text = SqlHelper.Instance.GetCompanyname();
                }
                else
                {
                    Response.Redirect("login.aspx");
                }

                Displaydata();
            }
        }
           
        catch(Exception ex)
         {

        }
    }



    protected void Btn_Segment_Click(object sender, EventArgs e)
    {
        try
        {
            #region Begin Code For  Validations   as [12-10-2013]
            if (Txt_Segment.Text.Trim().Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Please Enter Segment Name.');", true);
                return;
            }
            #endregion Begin Code For  Validations as [12-10-2013]

            #region Begin Code For Variable Declaration as [12-10-2013]
                    var SegmentName = string.Empty;
                    var IRecordStatus = 0;

            #endregion Begin Code For Variable Declaration as [12-10-2013]

            #region Begin Code For  Assign Values to Variable  as [12-10-2013]
                     SegmentName = Txt_Segment.Text.Trim().ToUpper();
            #endregion Begin Code For Assign Values to Variable as [12-10-2013]


            #region  Begin Code For Stored Procedure Parameters  as on [12-10-2013]
                    Hashtable HtSPParameters = new Hashtable();
                    var ProcedureName = "AddSegments";
            #endregion End Code For Stored Procedure Parameters  as on [12-10-2013]

            #region  Begin Code For Assign Values to the Stored Procedure Parameters as on [12-10-2013]
                    HtSPParameters.Add("@SegName", SegmentName);
            #endregion  End  Code For Assign Values to the Stored Procedure Parameters as on [12-10-2013]

            #region  Begin Code For Calling Stored Procedure As on [12-10-2013]
                    IRecordStatus = SqlHelper.Instance.ExecuteQuery(ProcedureName, HtSPParameters);
            #endregion  End Code For Calling Stored Procedure As on [12-10-2013]

            #region  Begin Code For Display Status Of the Record as on [12-10-2013]
            if (IRecordStatus > 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Segment Name Added SucessFully.');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Segment Name Not  Added.Because  The Name Already Exist. NOTE:Department Names Are UNIQUE');", true);
            }
            #endregion  End Code For Display Status Of the Record as on [12-10-2013]

            #region  Begin Code For Re-Call All the Departments As on [12-10-2013]
                     Displaydata();
            #endregion End Code For Re-Call All the Departments As on [12-10-2013]

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert(' Please Contact Your Admin..');", true);
            return;
        }
  
    }
   
    protected void gvSegment_RowUpdating1(object sender, GridViewUpdateEventArgs e)
    {
       
        try
        {

            #region  Begin  Code  for  Retrive  Data From  Gridview as on [14-10-2013]
                    Label segid = gvSegment.Rows[e.RowIndex].FindControl("lblSegid") as Label;
                    TextBox segname = gvSegment.Rows[e.RowIndex].FindControl("txtSegName") as TextBox;
            #endregion End  Code  for  Retrive  Data From  Gridview as on [14-10-2013]


            #region  Begin  Code  for  validaton as on [14-10-2013]
                    if (segname.Text.Trim().Length == 0)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Please Enter the Segment  Name');", true);
                            return;
                        }
            #endregion End  Code  for  validaton as on [14-10-2013]


            #region  Begin  Code  for  Variable  Declaration as on [14-10-2013]
            var Segmentname = string.Empty;
            var Segmentid = string.Empty;
            var ProcedureName = string.Empty;
            var IRecordStatus = 0;
            Hashtable HtSPParameters = new Hashtable();
            #endregion End  Code  for  Variable  Declaration as on [14-10-2013]

            #region  Begin  Code  for  Assign Values to  Variables as on [14-10-2013]
            Segmentname = segname.Text.Trim().ToUpper();
            Segmentid = segid.Text;
            ProcedureName = "ModifySegments";
            #endregion End  Code  for  Assign Values to  Variables as on [14-10-2013]

            #region  Begin  Code  for  Assign Values to  SP Parameters as on [14-10-2013]
            HtSPParameters.Add("@SegName", Segmentname);
            HtSPParameters.Add("@SegId", Segmentid);
            #endregion End  Code  for  Assign Values to  SP Parameters as on [14-10-2013]

            #region  Begin Code For Calling Stored Procedure As on [14-10-2013]
            IRecordStatus = SqlHelper.Instance.ExecuteQuery(ProcedureName, HtSPParameters);
            #endregion  End Code For Calling Stored Procedure As on [14-10-2013]

            #region  Begin Code For Display Status Of the Record as on [14-10-2013]
            if (IRecordStatus > 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Segment Name  Updated  SucessFully.');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Segment Name Not  Updated.Because  The Name Already Exist. NOTE:Department Names Are UNIQUE');", true);
            }
            #endregion  End Code For Display Status Of the Record as on [14-10-2013]

            #region  Begin Code For Re-Call All the Departments As on [14-10-2013]
            gvSegment.EditIndex = -1;
            Displaydata();
            #endregion End Code For Re-Call All the Departments As on [14-10-2013]

        }

        catch (Exception ex)
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Please Contact Admin.');", true);
            return;
        }
      
   
    }

    protected void gvSegment_RowEditing1(object sender, GridViewEditEventArgs e)
    {
        gvSegment.EditIndex = e.NewEditIndex;
        Displaydata();       
    }

    protected void gvSegment_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvSegment.EditIndex = -1;
        Displaydata();   
    }

    protected void gvSegment_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvSegment.PageIndex = e.NewPageIndex;
        Displaydata(); 
    }
    }
}