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
    public partial class ViewItems : System.Web.UI.Page
    {
        AppConfiguration config = new AppConfiguration();
        GridViewExportUtil gve = new GridViewExportUtil();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] != null && Session["AccessLevel"] != null)
            {

                lblDisplayUser.Text = Session["UserId"].ToString();
                PreviligeUsers(Convert.ToInt32(Session["AccessLevel"]));
                lblcname.Text = SqlHelper.Instance.GetCompanyname();

                switch (SqlHelper.Instance.GetCompanyValue())
                {
                    case 0:// Write Frames Invisible Links
                        break;
                    case 1://Write KLTS Invisible Links
                        break;
                    default:
                        break;
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

                    CompanyInfoLink.Visible = false;
                    ReportsLink.Visible = false;
                    SettingsLink.Visible = false;

                    break;

                case 3:



                    break;

                case 4:




                    break;
                case 5:
                    CompanyInfoLink.Visible = false;
                    ReportsLink.Visible = true;
                    SettingsLink.Visible = false;
                    ClientsLink.Visible = false;

                    break;
                case 6:

                    break;
                case 7:


                    break;
                case 8:
                    EmployeesLink.Visible = false;
                    ClientsLink.Visible = false;
                    CompanyInfoLink.Visible = false;
                    ReportsLink.Visible = false;
                    SettingsLink.Visible = false;

                    break;
                default:
                    break;


            }
        }

    }
}