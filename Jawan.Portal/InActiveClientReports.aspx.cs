using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using KLTS.Data;
using Jawan.Portal.DAL;
namespace Jawan.Portal
{
    public partial class InActiveClientReports : System.Web.UI.Page
    {
        DataTable dt;
        AppConfiguration config = new AppConfiguration();
        GridViewExportUtil gve = new GridViewExportUtil();
        protected void Page_Load(object sender, EventArgs e)
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
            }
        }

        protected void PreviligeUsers(int previligerid)
        {
            switch (previligerid)
            {
                case 1:
                    break;
                case 2:

                    break;

                case 3:
                    break;

                case 4:
                    break;
                case 5:

                    break;
                default:
                    break;
            }
        }
        protected void showdata()
        {
            string selectQuery = "select Clients.ClientId,Clients.ClientName,Clients.ClientAddrHno,Clients.ClientAddrCity,Clients,ClientAddrState,Clients.ClientSegment,Clients.ClientPersonDesgn,Clients.ClientPhonenumbers,Clients.ClientDesc from Clients INNER JOIN Contracts ON Clients.ClientId=Contracts.ClientId where ContractStartDate Between '" + txtStrtDate.Text + "' and '" + txtEndDate.Text + "'";
            dt = config.ExecuteAdaptorAsyncWithQueryParams(selectQuery).Result;
            GVListClients.DataSource = dt;
            GVListClients.DataBind();

        }
        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            showdata();
        }
    }
}