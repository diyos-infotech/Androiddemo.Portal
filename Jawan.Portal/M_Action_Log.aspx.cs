using System;
using System.Collections;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Data;
using KLTS.Data;
using System.Globalization;

namespace Jawan.Portal
{
    public partial class M_Action_Log : System.Web.UI.Page
    {
        Marketinghelper MH = new Marketinghelper();
        DataTable dtcalender;
        LinkButton lnkDelete1 = new LinkButton();
        private LinkButton lb;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                LoadLeadId();
                LoadLeadName();
                LoadStatus();

                if (Request.QueryString["Leadid"] != null)
                {
                    string Leadid = Request.QueryString["Leadid"].ToString();
                    DropLeadID.SelectedValue = (Leadid);
                    DropLeadID_SelectedIndexChanged(sender, e);
                    bindActionData();
                    BuildSocialEventTable();
                }
            }
        }

        protected void LoadStatus()
        {
            string queryStatus = "select * from M_LeadStatusMaster";
            DataTable Dtstatus = MH.ExecuteAdaptorAsyncWithQueryParams(queryStatus);
            if (Dtstatus.Rows.Count > 0)
            {
                ddlStatus.DataValueField = "Id";
                ddlStatus.DataTextField = "LeadStatus";
                ddlStatus.DataSource = Dtstatus;
                ddlStatus.DataBind();
            }
            ddlStatus.Items.Insert(0, new ListItem("-Select-", "0"));
        }

        protected void LoadLeadId()
        {
            DataTable DtLeadid = MH.LoadLeadids();
            if (DtLeadid.Rows.Count > 0)
            {
                DropLeadID.DataValueField = "LeadID";
                DropLeadID.DataTextField = "LeadID";
                DropLeadID.DataSource = DtLeadid;
                DropLeadID.DataBind();
            }
            DropLeadID.Items.Insert(0, "-Select-");

        }

        protected void LoadLeadName()
        {
            DataTable DtLeadname = MH.LoadLeadids();
            if (DtLeadname.Rows.Count > 0)
            {
                DropLeadName.DataValueField = "LeadID";
                DropLeadName.DataTextField = "LeadName";
                DropLeadName.DataSource = DtLeadname;
                DropLeadName.DataBind();
            }
            DropLeadName.Items.Insert(0, "-Select-");

        }

        protected void DropLeadID_SelectedIndexChanged(object sender, EventArgs e)
        {
            Gvactionlog.DataSource = null;
            Gvactionlog.DataBind();

            if (DropLeadID.SelectedIndex > 0)
            {

                DropLeadName.SelectedValue = DropLeadID.SelectedValue;

            }
            else
            {
                DropLeadName.SelectedIndex = 0;
            }
            bindActionData();


        }

        protected void DropLeadName_SelectedIndexChanged(object sender, EventArgs e)
        {
            Gvactionlog.DataSource = null;
            Gvactionlog.DataBind();
            if (DropLeadName.SelectedIndex > 0)
            {
                DropLeadID.SelectedValue = DropLeadName.SelectedValue;

            }
            else
            {
                DropLeadID.SelectedIndex = 0;
            }
            bindActionData();
        }

        protected void btnActionSave_Click(object sender, EventArgs e)
        {
            if (DropLeadID.SelectedIndex == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Select LeadId')", true);
                return;
            }
            if (DropLeadName.SelectedIndex == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Select LeadName')", true);
                return;
            }
            if (txtaction.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Enter Action')", true);
                return;

            }
            if (txtactionDate.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Select Action Date')", true);
                return;

            }

            int sno = 1;
            string snoquery = "select isnull(max(id),0) as ID from M_ActionLogDetails where Leadid='" + DropLeadID.SelectedValue + "'";
            DataTable dtsno = SqlHelper.Instance.GetTableByQuery(snoquery);

            if (dtsno.Rows.Count > 0)
            {
                sno = int.Parse(dtsno.Rows[0]["ID"].ToString()) + 1;
            }

            string date = txtactionDate.Text;
            DateTime actiondate = DateTime.Parse(txtactionDate.Text, CultureInfo.GetCultureInfo("en-gb"));
            string spname = "M_InsertActionLogDetails";
            Hashtable hs = new Hashtable();
            hs.Add("@id", sno);
            hs.Add("@leadid", DropLeadID.SelectedValue);
            hs.Add("@action", txtaction.Text);
            hs.Add("@Dateofaction", actiondate);
            hs.Add("@status", "");
            hs.Add("@addedon", DateTime.Now);
            int dtleadrequirement = MH.ExecuteNonQueryAsyncWithSPParams(spname, hs);
            if (dtleadrequirement > 0)
            {
                bindActionData();
            }

            txtaction.Text = "";
            txtactionDate.Text = "";
        }

        protected void bindActionData()
        {
            string gridquery = "select DateofAction,Action,LeadId,Status,Id,Action from M_ActionLogDetails where Leadid='" + DropLeadID.SelectedItem.Text + "' and status='' order by status asc,ID asc";
            DataTable dtgridresult = SqlHelper.Instance.GetTableByQuery(gridquery);
            if (dtgridresult.Rows.Count > 0)
            {
                Gvactionlog.DataSource = dtgridresult;
                Gvactionlog.DataBind();
            }
        }

        protected void btnsaves_Click(object sender, EventArgs e)
        {

            string queryresult = "delete from M_ActionLogDetails where Leadid='" + DropLeadID.SelectedValue + "'";
            int dtresult = SqlHelper.Instance.ExecuteDMLQry(queryresult);
            for (int i = 0; i < Gvactionlog.Rows.Count; i++)
            {
                Label txtgridsno = Gvactionlog.Rows[i].FindControl("lblSno") as Label;
                TextBox txtgridaction = Gvactionlog.Rows[i].FindControl("txtaction") as TextBox;
                TextBox txtgriddateaction = Gvactionlog.Rows[i].FindControl("txtdateaction") as TextBox;
                TextBox txtstatus = Gvactionlog.Rows[i].FindControl("txtstatus") as TextBox;
                TextBox txtgridleadid = Gvactionlog.Rows[i].FindControl("txtleadid") as TextBox;
                TextBox status = Gvactionlog.Rows[i].FindControl("txtstatus") as TextBox;


                string date = txtactionDate.Text;
                DateTime actiondate = DateTime.Parse(txtgriddateaction.Text, CultureInfo.GetCultureInfo("en-gb"));
                string spname = "M_InsertActionLogDetails";
                Hashtable hs = new Hashtable();
                hs.Add("@id", txtgridsno.Text);
                hs.Add("@leadid", DropLeadID.SelectedValue);
                hs.Add("@action", txtgridaction.Text);
                hs.Add("@Dateofaction", actiondate);
                hs.Add("@status", status.Text);
                hs.Add("@addedon", DateTime.Now);
                int dtleadrequirement = MH.ExecuteNonQueryAsyncWithSPParams(spname, hs);

                if (dtleadrequirement > 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "show alert()", "alert('Your Date is Successfully Saved')", true);
                }
            }

            bindActionData();
        }

        private void BuildSocialEventTable()
        {
            string calenderquery = "select * from M_ActionLogDetails where Leadid='" + DropLeadID.SelectedValue + "'";
            dtcalender = SqlHelper.Instance.GetTableByQuery(calenderquery);

        }

        string dt = "";

        protected void myCalendar_DayRender(object sender, DayRenderEventArgs e)
        {
            Literal l = new Literal(); //Creating a literal  
            l.Visible = true;
            l.Text = "<br/>"; //for breaking the line in cell  
            e.Cell.Controls.Add(l);

            string dt = e.Day.Date.ToString();

            string calenderquery = "select LeadID,Action from M_ActionLogDetails where Leadid='" + DropLeadID.SelectedValue + "' and dateofaction='" + e.Day.Date.ToString() + "' and status='' ";
            dtcalender = SqlHelper.Instance.GetTableByQuery(calenderquery);



            if (dtcalender.Rows.Count > 0)
            {
                string Action = "";

                if (dtcalender.Rows[0]["Action"].ToString().Length > 25)
                {
                    Action = 1 + ") " + dtcalender.Rows[0]["Action"].ToString().Substring(0, 25) + "..." + "<br>";
                }
                else
                {
                    Action = 1 + ") " + dtcalender.Rows[0]["Action"].ToString() + "<br>";

                }

                e.Cell.CssClass = "cellback";

                Panel span = new Panel();
                span.CssClass = "cellcontent";

                Label lbtext = new Label();
                lbtext.Visible = true;
                lbtext.Text = Action;
                span.Controls.Add(lbtext);


                Label lbtmore = new Label();
                lbtmore.Visible = true;
                lbtmore.Text = (dtcalender.Rows.Count - 1).ToString() + " More Events" + "<br>";
                if (dtcalender.Rows.Count > 1)
                {
                    span.Controls.Add(lbtmore);
                }

                HyperLink lbview = new HyperLink();
                lbview.Visible = true;
                lbview.Text = "View Details >>";
                lbview.CssClass = "cellview";
                lbview.NavigateUrl =
                Page.ClientScript.GetPostBackClientHyperlink(lblbtn, dt.ToString(), true);
                span.Controls.Add(lbview);

                e.Cell.Controls.Add(span);


            }

        }

        public void lbview_Click(object sender, EventArgs e)
        {
            string id = (Request.Form["__EVENTARGUMENT"]).ToString();


            string calenderquery = "select l.LeadName,convert(nvarchar(10),Dateofaction,103) as Actiondate,* from M_ActionLogDetails ald inner join M_leads l on l.LeadId=ald.LeadId where ald.Leadid='" + DropLeadID.SelectedValue + "' and dateofaction='" + id + "' and status='' ";
            dtcalender = SqlHelper.Instance.GetTableByQuery(calenderquery);

            string Leadname = "";
            string Action = "";
            string Dateofaction = "";
            string date = "";

            if (id != "")
            {
                date = Convert.ToDateTime(id).ToString("dd/MMMM/yyyy");
            }


            if (dtcalender.Rows.Count > 0)
            {
                string message = "";



                for (int k = 0; k < dtcalender.Rows.Count; k++)
                {
                    int s = k + 1;
                    Leadname = dtcalender.Rows[0]["LeadName"].ToString();
                    Dateofaction = dtcalender.Rows[0]["Actiondate"].ToString();
                    Action = Action + s + ". " + dtcalender.Rows[k]["Action"].ToString() + "<br>";

                    message = "<B>Lead &nbsp;: </B>" + Leadname + "<br>" + "<B>Events : </B>  " + "<br>" + Action;

                }



                ScriptManager.RegisterStartupScript((sender as Control), this.GetType(), "Popup", "ShowPopup('" + message + "','" + date + "');", true);


            }
        }

        protected void myCalendar_SelectionChanged(object sender, EventArgs e)
        {

            DateTime dt = new DateTime();
            dt = myCalendar.SelectedDate;

            string calenderquery = "select l.LeadName,convert(nvarchar(10),Dateofaction,103) as Actiondate,* from M_ActionLogDetails ald inner join M_leads l on l.LeadId=ald.LeadId where ald.Leadid='" + DropLeadID.SelectedValue + "' and dateofaction='" + dt + "' and status='' ";
            dtcalender = SqlHelper.Instance.GetTableByQuery(calenderquery);

            string Leadname = "";
            string Action = "";
            string Dateofaction = "";

            string date = dt.ToString("dd MMMM yyyy");

            if (dtcalender.Rows.Count > 0)
            {
                string message = "";

                for (int k = 0; k < dtcalender.Rows.Count; k++)
                {
                    int s = k + 1;
                    Leadname = dtcalender.Rows[0]["LeadName"].ToString();
                    Dateofaction = dtcalender.Rows[0]["Actiondate"].ToString();
                    Action = Action + s + ". " + dtcalender.Rows[k]["Action"].ToString() + "<br>";
                    message = "<B>Lead : </B>" + Leadname + "<br>" + "<B>Events : </B>  " + "<br>" + Action;

                }

                ScriptManager.RegisterStartupScript((sender as Control), this.GetType(), "Popup", "ShowPopup('" + message + "','" + date + "');", true);
            }
        }

        protected void Gvactionlog_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Gvactionlog.PageIndex = e.NewPageIndex;
            bindActionData();
        }

        protected void but_send_Click(object sender, EventArgs e)
        {
            string leadid = DropLeadID.SelectedValue;
            string status = hnd_drp_status.Value;
            DateTime Createdon = DateTime.Now;
            string userid = Session["UserId"].ToString();

            string linksave = "insert into M_LeadStatusDetails(LeadId,Status,CreatedOn,CreatedBy) values('" + leadid + "','" + status + "','" + Createdon + "','" + userid + "')";
            int dtLeadStatusDetails = SqlHelper.Instance.ExecuteDMLQry(linksave);

            string updateleadstatus = "update M_leads set LeadStatus='" + status + "' where leadid='" + leadid + "'";
            int dtupdateleadstatus = SqlHelper.Instance.ExecuteDMLQry(updateleadstatus);



        }
    }
}