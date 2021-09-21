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
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using KLTS.Data;
using Jawan.Portal.DAL;
namespace Jawan.Portal
{
    public partial class ModifyCompanyInfo : System.Web.UI.Page
    {
        DataTable dt;
        AppConfiguration config = new AppConfiguration();
        GridViewExportUtil gve = new GridViewExportUtil();
        protected void Page_Load(object sender, EventArgs e)
        {
            int i = 0;
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
                string selectquery = "select CompanyName from  companyinfo";
                dt = config.ExecuteAdaptorAsyncWithQueryParams(selectquery).Result;
                for (i = 0; i < dt.Rows.Count; i++)
                {
                    ddlcname.Items.Add(dt.Rows[i][0].ToString());
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
                    AddCompanyInfoLink.Visible = true;
                    ModifyCompanyInfoLink.Visible = false;
                    DeleteCompanyInfoLink.Visible = false;
                    InventoryLink.Visible = false;
                    ReportsLink.Visible = false;
                    SettingsLink.Visible = false;

                    break;

                case 3:

                    InventoryLink.Visible = false;
                    ReportsLink.Visible = false;
                    SettingsLink.Visible = false;
                    break;
                case 4:
                    AddCompanyInfoLink.Visible = true;
                    ModifyCompanyInfoLink.Visible = true;
                    DeleteCompanyInfoLink.Visible = true;

                    EmployeesLink.Visible = true;
                    ClientsLink.Visible = true;
                    CompanyInfoLink.Visible = true;
                    InventoryLink.Visible = true;
                    ReportsLink.Visible = true;
                    SettingsLink.Visible = true;
                    break;
                case 5:
                    CompanyInfoLink.Visible = false;
                    AddCompanyInfoLink.Visible = false;
                    ModifyCompanyInfoLink.Visible = false;
                    DeleteCompanyInfoLink.Visible = false;
                    break;
                case 6:

                    CompanyInfoLink.Visible = false;
                    AddCompanyInfoLink.Visible = false;
                    ModifyCompanyInfoLink.Visible = false;
                    DeleteCompanyInfoLink.Visible = false;

                    InventoryLink.Visible = false;
                    ReportsLink.Visible = false;
                    SettingsLink.Visible = false;




                    break;
                default:
                    break;


            }
        }

        protected void btnaddclint_Click(object sender, EventArgs e)
        {
            try
            {
                Insert();
            }
            catch (Exception ex)
            {
                lblresult.Visible = true;
                lblresult.Text = "*  Please Select The Photo";

            }
        }
        protected void txtcname_TextChanged(object sender, EventArgs e)
        {
        }

        static string fileName;
        static int fileLength;
        static byte[] imageBytes;
        public void SavePicture(String Fname, String img)
        {
            if (img == "jpg" || img == "jpeg" || img == "png")
            {
                fileName = fcpicture.PostedFile.FileName;

                // Session["image"] = fileName; 
                fileLength = fcpicture.PostedFile.ContentLength;

                imageBytes = new byte[fileLength];
                fcpicture.PostedFile.InputStream.Read(imageBytes, 0, fileLength);
                if (Fname != String.Empty)
                {
                    string rootpath = Server.MapPath("Images");
                    if (File.Exists(rootpath + "\\" + Fname))
                    {
                        File.Delete(rootpath + "\\" + Fname);
                        fcpicture.SaveAs(rootpath + "\\" + Fname);
                        imglogo.Src = "Images/" + Fname;
                    }
                    else
                    {
                        fcpicture.SaveAs(rootpath + "\\" + Fname);
                        imglogo.Src = "Images/" + Fname;
                    }
                }
            }
            else
            {
                imglogo.Src = "";

                // lblError.Text = "Please Upload a Image in proper format";
            }
        }
        public void Insert()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KLTSConnectionString"].ToString());

            con.Open();
            //SqlDataAdapter da = new SqlDataAdapter(insertquery,con);

            SqlCommand cmd = new SqlCommand();
            string cname = ddlcname.SelectedItem.ToString();
            string csname = txtcsname.Text;
            string address = txtaddress.Text;
            string pfno = txtpfno.Text;
            string esino = txtesino.Text;
            string billdesc = txtbilldesc.Text;
            string cinfo = txtcinfo.Text;
            string billnotes = txtbnotes.Text;
            string billseq = txtbillsq.Text;
            string labourrule = txtlabour.Text;
            cmd.Parameters.Clear();
            cmd.CommandText = "modifyaddcompanyinfo";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            cmd.Parameters.Add("@pcompanyname", cname);
            cmd.Parameters.Add("@pshortname", csname);
            cmd.Parameters.Add("@paddress", address);
            cmd.Parameters.Add("@ppfno", pfno);
            cmd.Parameters.Add("@pesino", esino);
            cmd.Parameters.Add("@pbilldesc", billdesc);

            cmd.Parameters.Add("@pcompanyinfo", cinfo);
            cmd.Parameters.Add("@pbillnotes", billnotes);
            cmd.Parameters.Add("@pbillseq", billseq);
            cmd.Parameters.Add("@plabourrule", labourrule);

            //if(imageBytes==0)


            cmd.Parameters.Add("@plogo", Session["image"]);
            int status = cmd.ExecuteNonQuery();
            if (status != 0)
            {
                lblresult.Visible = true;
                lblresult.Text = "Record Updated Successfully";
            }
            else
            {
                lblresult.Visible = true;
                lblresult.Text = "Record Not Updated";

            }
            con.Close();
        }
        protected void ddlcname_SelectedIndexChanged(object sender, EventArgs e)
        {
            string cname = ddlcname.SelectedItem.Text;
            string selectquery = "select * from companyinfo where companyname= '" + cname + "'";
            DataTable dt = config.ExecuteAdaptorAsyncWithQueryParams(selectquery).Result;

            if (dt.Rows.Count > 0)
            {
                ddlcname.SelectedItem.Text = dt.Rows[0][0].ToString();
                txtcsname.Text = dt.Rows[0][1].ToString();
                txtaddress.Text = dt.Rows[0][2].ToString();
                txtpfno.Text = dt.Rows[0][3].ToString();
                txtesino.Text = dt.Rows[0][4].ToString();
                txtbilldesc.Text = dt.Rows[0][5].ToString();
                txtcinfo.Text = dt.Rows[0][6].ToString();
                txtbnotes.Text = dt.Rows[0][7].ToString();
                txtbillsq.Text = dt.Rows[0][8].ToString();
                txtlabour.Text = dt.Rows[0][9].ToString();
                Byte[] barr = new Byte[100000];

                if (String.IsNullOrEmpty(dt.Rows[0]["logo"].ToString()) == false)
                {
                    barr = (Byte[])dt.Rows[0]["logo"];
                    Session["image"] = barr;
                    MemoryStream a = new MemoryStream(barr, false);
                    System.Drawing.Image image = System.Drawing.Image.FromStream(a);
                    Random rnd = new Random();
                    string imagename = rnd.Next() + ".jpg";
                    image.Save(Server.MapPath("Images" + "\\" + "\\" + imagename), System.Drawing.Imaging.ImageFormat.Jpeg);
                    imglogo.Src = "~/Images/" + imagename;
                    // Session["Image"] = "~/Images/" + imagename;
                }
            }

            else

                lblresult.Text = "Choose Company Name";

        }
        protected void btnphoto_Click(object sender, EventArgs e)
        {

            if (btnphoto.Text == "Select Photo")
            {
                fcpicture.Visible = true;
                btnphoto.Text = "Save";

            }
            else
            {
                btnphoto.Text = "Select Photo";

                if (fcpicture.HasFile)
                {
                    string filename = fcpicture.FileName;
                    string ext = filename.Substring(filename.IndexOf('.') + 1);
                    SavePicture(filename, ext.ToLower());

                }
                fcpicture.Visible = false;

            }
        }
        protected void btncancel_Click(object sender, EventArgs e)
        {
            ddlcname.SelectedValue = "1";
            ddlcname.SelectedItem.Text = " --Select Company Name";

            txtcsname.Text = txtaddress.Text = txtpfno.Text = txtesino.Text = txtbilldesc.Text = txtcinfo.Text = string.Empty;
            txtbnotes.Text = txtbillsq.Text = txtlabour.Text = string.Empty;
            imglogo.Src = "";

            lblresult.Text = "";
        }
    }
}