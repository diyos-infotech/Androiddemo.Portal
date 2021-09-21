using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using KLTS.Data;

namespace Jawan.Portal
{
    public partial class M_All_Action_Log_Scheduler : System.Web.UI.Page
    {
        Marketinghelper MH = new Marketinghelper();
        DataTable dtcalender;
        LinkButton lnkDelete1 = new LinkButton();
        private LinkButton lb;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                bindActionData();
            }
        }

        protected void bindActionData()
        {
            string gridquery = "select DateofAction,Action,ald.LeadId,Status,Id,Action,leadname from M_ActionLogDetails ald inner join m_leads l on l.leadid=ald.leadid  where ald.status='' order by status asc,ID asc";
            DataTable dtgridresult = SqlHelper.Instance.GetTableByQuery(gridquery);
            if (dtgridresult.Rows.Count > 0)
            {
                Gvactionlog.DataSource = dtgridresult;
                Gvactionlog.DataBind();
            }
        }

        string dt = "";

        protected void myCalendar_DayRender(object sender, DayRenderEventArgs e)
        {
            Literal l = new Literal(); //Creating a literal  
            l.Visible = true;
            l.Text = "<br/>"; //for breaking the line in cell  
            e.Cell.Controls.Add(l);

            string dt = e.Day.Date.ToString();

            string calenderquery = "select LeadID,Action from M_ActionLogDetails where  dateofaction='" + e.Day.Date.ToString() + "' and status='' ";
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


            string calenderquery = "select ald.leadid,l.LeadName,convert(nvarchar(10),Dateofaction,103) as Actiondate,* from M_ActionLogDetails ald inner join M_leads l on l.LeadId=ald.LeadId where  dateofaction='" + id + "' and status='' ";
            dtcalender = SqlHelper.Instance.GetTableByQuery(calenderquery);

            DataTable dtgroupby = dtcalender.DefaultView.ToTable(true, "leadid", "LeadName");

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

                for (int k = 0; k < dtgroupby.Rows.Count; k++)
                {

                    int s = k + 1;

                    Leadname = dtgroupby.Rows[k]["LeadName"].ToString();
                    Action = "";

                    for (int x = 0; x < dtcalender.Rows.Count; x++)
                    {
                        if (dtgroupby.Rows[k]["LeadName"].ToString() == dtcalender.Rows[x]["LeadName"].ToString())
                        {
                            Dateofaction = dtcalender.Rows[0]["Actiondate"].ToString();
                            Action = Action + "<B>* </B>" + dtcalender.Rows[x]["Action"].ToString() + "<br>";

                        }
                    }

                    message = message + "<B>Lead Name : </B>" + Leadname + "<br>" + "<B>Events : </B>  " + "<br>" + Action + "<br>";


                    s++;
                }

                ScriptManager.RegisterStartupScript((sender as Control), this.GetType(), "Popup", "ShowPopup('" + message + "','" + date + "');", true);


            }
        }

        protected void Gvactionlog_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Gvactionlog.PageIndex = e.NewPageIndex;
            bindActionData();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {

            if (txtFromDate.Text.Trim().Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showlalert", "alert('Please Fill the From Date');", true);
                return;
            }
            if (txtToDate.Text.Trim().Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showlalert", "alert('Please Fill the To Date');", true);
                return;
            }

            var FromDate = DateTime.Now;
            var ToDate = DateTime.Now;

            FromDate = DateTime.Parse(txtFromDate.Text.Trim(), System.Globalization.CultureInfo.GetCultureInfo("en-GB"));
            ToDate = DateTime.Parse(txtToDate.Text.Trim(), System.Globalization.CultureInfo.GetCultureInfo("en-GB"));


            string gridquery = "select DateofAction,Action,ald.LeadId,Status,Id,Action,leadname from M_ActionLogDetails ald inner join m_leads l on l.leadid=ald.leadid where ald.status='' and cast(Dateofaction as date)>='" + FromDate + "' and cast(Dateofaction as date)<='" + ToDate + "' order by cast(Dateofaction as date) asc,status asc,ID asc";
            DataTable dtgridresult = SqlHelper.Instance.GetTableByQuery(gridquery);
            if (dtgridresult.Rows.Count > 0)
            {
                Gvactionlog.DataSource = dtgridresult;
                Gvactionlog.DataBind();
            }
        }
    }
}