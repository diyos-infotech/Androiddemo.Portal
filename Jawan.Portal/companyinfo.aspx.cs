using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KLTS.Data;
using System.Windows.Forms;
using System.IO;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using KLTS.Data;
using Jawan.Portal.DAL;
namespace Jawan.Portal
{
    public partial class companyinfo : System.Web.UI.Page
    {
        AppConfiguration config = new AppConfiguration();
        GridViewExportUtil gve = new GridViewExportUtil();
        string CmpIDPrefix = "";
        string BranchID = "";


        protected void Page_Load(object sender, EventArgs e)
        {
            GetWebConfigdata();

            if (!IsPostBack)
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
                            ExpensesLink.Visible = false;
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    Response.Redirect("login.aspx");
                }



                LoadPreviousData();
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
            lblresult.Visible = true;
            try
            {
                //if (txtcsname.Text.Trim().Length > 3)
                //{
                //    ErrorMessage1 = ErrorMessage1 + "* Company Short Name Only 3 Characters<br>";

                //}
                //if (txtcsname.Text.Trim().Length > 4)
                //{
                //    ErrorMessage1 = ErrorMessage1 + "*  Bill Seq Only Allow  4 Characters <br>";
                //}

                //if (txtcsname.Text.Trim().Length > 3 || txtcsname.Text.Trim().Length > 4)
                //{
                //    lblresult.Text = ErrorMessage1;
                //    return;
                //}

                if (txtcsname.Text.Trim().Length == 0)
                {
                    ErrorMessage1 = ErrorMessage1 + "* Company Short Name Don't Leave Empty <br>";

                }
                if (txtcsname.Text.Trim().Length == 0)
                {
                    ErrorMessage1 = ErrorMessage1 + "*  Bill Seq Don't Leave Empty <br>";
                }

                Modify();
            }
            catch (Exception ex)
            {
                //lblresult.Visible = true;
                // lblresult.Text = ex.Message;
            }
        }

        #region for company info image

        //public void SavePicture(String Fname, String img)
        //{
        //    string fileName = fcpicture.PostedFile.FileName;
        //    int fileLength = fcpicture.PostedFile.ContentLength;
        //    byte[] imageBytes = new byte[fileLength];

        //    Session["imagebytes"] = imageBytes;
        //    fcpicture.PostedFile.InputStream.Read(imageBytes, 0, fileLength);
        //    if (img == "jpg" || img == "jpeg" || img == "png")
        //    {
        //        if (Fname != String.Empty)
        //        {
        //            string rootpath = Server.MapPath("Images");
        //            if (File.Exists(rootpath + "\\" + Fname))
        //            {
        //                File.Delete(rootpath + "\\" + Fname);
        //                fcpicture.SaveAs(rootpath + "\\" + Fname);
        //                imglogo.Src = "Images/" + Fname;
        //            }
        //            else
        //            {
        //                fcpicture.SaveAs(rootpath + "\\" + Fname);
        //                imglogo.Src = "Images/" + Fname;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        imglogo.Src = "";
        //    }
        //}


        #endregion for company info image

        public void Modify()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KLTSConnectionString"].ToString());
            con.Open();
            SqlCommand cmd = new SqlCommand();
            string cname = txtcname.Text;
            string csname = txtcsname.Text;
            string address = txtaddress.Text;
            string pfno = txtpfno.Text;
            string esino = txtesino.Text;
            string billdesc = txtbilldesc.Text;
            string cinfo = txtcinfo.Text;
            string billnotes = txtbnotes.Text;
            string billseq = txtbillsq.Text;
            string labourrule = txtlabour.Text;
            string category = txtCategory.Text;

            string GSTNo = txtGSTNo.Text;
            string HSNNumber = txtHsnNummber.Text;
            string SACCode = txtSacCode.Text;
            string POContactPerson = txtPOContactPerson.Text;
            string POContactNumber = txtPOContactNumber.Text;

            //string Accountno = txtAccountno.Text;
            //string IFSCCOde = txtifsccode.Text;
            //string ChequePREPARE = txtPREPARE.Text;
            //string Bank = txtBANK.Text;
            //string Addresslineone = txtaddresslineone.Text;
            //string Addresslinetwo = txtaddresslinetwo.Text;
            //string SASTC = txtsastcc.Text;

            string Phoneno = txtPhoneno.Text;
            string Faxno = txtFaxno.Text;
            string Emailid = txtEmail.Text;
            string Website = txtWebsite.Text;
            string notes = txtNotes.Text;
            string CorporateIDNO = txtcorporateIDNo.Text;
            string RegNo = txtregno.Text;
            string ESICNoForms = txtESICNoForms.Text;
            string BranchOffice = txtBranchOffice.Text;
            //string ISOCertNo = txtISOCertNo.Text;
            //string PsaraAct = txtPsaraAct.Text;
            //string KSSAMemberShipNo = txtKSSAMemberShipNo.Text;

            string SqlQry = "Select * From CompanyInfo   where  branchid  ='" + BranchID + "'";
            DataTable dt = config.ExecuteAdaptorAsyncWithQueryParams(SqlQry).Result;
            cmd.Parameters.Clear();
            if (dt.Rows.Count > 0)
            {
                cmd.CommandText = "modifyaddcompanyinfo";
            }
            else
            {
                cmd.CommandText = "addcompanyinfo";
            }
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
            //cmd.Parameters.Add("@plogo", Session["imagebytes"]  );
            //cmd.Parameters.Add("@ChequePrepare", ChequePREPARE);
            //cmd.Parameters.Add("@Bankname", Bank);
            //cmd.Parameters.Add("@bankaccountno", Accountno);
            //cmd.Parameters.Add("@Addresslineone", Addresslineone);
            //cmd.Parameters.Add("@Addresslinetwo",Addresslinetwo);
            //cmd.Parameters.Add("@IfscCode", IFSCCOde);
            //cmd.Parameters.Add("@SASTC", SASTC);
            cmd.Parameters.Add("@ClientidPrefix", CmpIDPrefix);
            cmd.Parameters.Add("@Phoneno", Phoneno);
            cmd.Parameters.Add("@Faxno", Faxno);
            cmd.Parameters.Add("@Emailid", Emailid);
            cmd.Parameters.Add("@Website", Website);
            cmd.Parameters.Add("@Notes", notes);
            cmd.Parameters.Add("@CorporateIDNo", CorporateIDNO);
            cmd.Parameters.Add("@RegNo", RegNo);
            cmd.Parameters.Add("@ESICNoForms", ESICNoForms);
            cmd.Parameters.Add("@BranchOffice", BranchOffice);
            cmd.Parameters.Add("@Category", category);
            cmd.Parameters.Add("@branchid", BranchID);

            cmd.Parameters.Add("@GSTNo", GSTNo);
            cmd.Parameters.Add("@HSNNumber", HSNNumber);
            cmd.Parameters.Add("@SACCode", SACCode);
            cmd.Parameters.Add("@POContactPerson", POContactPerson);
            cmd.Parameters.Add("@POContactNumber", POContactNumber);

            //cmd.Parameters.Add("@ISOCertfNo", ISOCertNo);
            //cmd.Parameters.Add("@PSARARegNo", PsaraAct);
            //cmd.Parameters.Add("@KSSAMembershipNo", KSSAMemberShipNo);

            int status = cmd.ExecuteNonQuery();
            con.Close();

            if (status != 0)
            {
                lblresult.Visible = true;
                lblresult.Text = "Record  added Successfully";
                Disablefields();
            }
            else
            {
                lblresult.Visible = true;
                lblresult.Text = "Record Not Inserted";
            }
        }

        public void Insert()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KLTSConnectionString"].ToString());
            con.Open();
            SqlCommand cmd = new SqlCommand();
            string cname = txtcname.Text;
            string csname = txtcsname.Text;
            string address = txtaddress.Text;
            string pfno = txtpfno.Text;
            string esino = txtesino.Text;
            string billdesc = txtbilldesc.Text;
            string cinfo = txtcinfo.Text;
            string Category = txtCategory.Text;
            string billnotes = txtbnotes.Text;
            string billseq = txtbillsq.Text;
            string labourrule = txtlabour.Text;
            string Phoneno = txtPhoneno.Text;
            string Faxno = txtFaxno.Text;
            string Emailid = txtEmail.Text;
            string Website = txtWebsite.Text;
            string notes = txtNotes.Text;
            string CorporateIDNO = txtcorporateIDNo.Text;
            string RegNo = txtregno.Text;
            string ESICNoForms = txtESICNoForms.Text;
            string BranchOffice = txtBranchOffice.Text;
            //string ISOCertNo = txtISOCertNo.Text ;
            //string PsaraAct = txtPsaraAct.Text ;
            //string KSSAMemberShipNo = txtKSSAMemberShipNo.Text ;
            string GSTNo = txtGSTNo.Text;
            string HSNNumber = txtHsnNummber.Text;
            string SACCode = txtSacCode.Text;
            string POContactPerson = txtPOContactPerson.Text;
            string POContactNumber = txtPOContactNumber.Text;

            cmd.CommandText = "addcompanyinfo";
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
            // cmd.Parameters.Add("@plogo", Session["imagebytes"]);
            cmd.Parameters.Add("@notes", notes);
            cmd.Parameters.Add("@Phoneno", Phoneno);
            cmd.Parameters.Add("@Faxno", Faxno);
            cmd.Parameters.Add("@Emailid", Emailid);
            cmd.Parameters.Add("@Website", Website);
            cmd.Parameters.Add("@CorporateIDNo", CorporateIDNO);
            cmd.Parameters.Add("@RegNo", RegNo);
            cmd.Parameters.Add("@ESICNoForms", ESICNoForms);
            cmd.Parameters.Add("@BranchOffice", BranchOffice);
            cmd.Parameters.Add("@Category", Category);
            cmd.Parameters.Add("@branchid", BranchID);
            //cmd.Parameters.Add("@ISOCertfNo", ISOCertNo);
            //cmd.Parameters.Add("@PSARARegNo", PsaraAct);
            //cmd.Parameters.Add("@KSSAMembershipNo", KSSAMemberShipNo);
            cmd.Parameters.Add("@GSTNo", GSTNo);
            cmd.Parameters.Add("@HSNNumber", HSNNumber);
            cmd.Parameters.Add("@SACCode", SACCode);
            cmd.Parameters.Add("@POContactPerson", POContactPerson);
            cmd.Parameters.Add("@POContactNumber", POContactNumber);



            int status = cmd.ExecuteNonQuery();
            con.Close();

            if (status != 0)
            {
                lblresult.Visible = true;
                lblresult.Text = "Record  Added Successfully";
                Disablefields();
            }
            else
            {
                lblresult.Visible = true;
                lblresult.Text = "Record Not Inserted";
            }
        }

        //protected void btnphoto_Click(object sender, EventArgs e)
        //{
        //    if (btnphoto.Text == "Select Photo")
        //    {
        //        btnphoto.Text = "Save";
        //        fcpicture.Visible = true;
        //    }
        //    else
        //    {
        //        btnphoto.Text = "Select Photo";
        //        fcpicture.Visible = true;
        //        if (fcpicture.HasFile)
        //        {
        //            string filename = fcpicture.FileName;
        //            string ext = filename.Substring(filename.IndexOf('.') + 1);
        //            SavePicture(filename, ext.ToLower());
        //        }
        //        fcpicture.Visible = false;
        //    }
        //}

        private void clearData()
        {
            txtcname.Text = txtcsname.Text = txtaddress.Text = txtpfno.Text = txtesino.Text = txtbilldesc.Text = string.Empty;
            txtcinfo.Text = txtbnotes.Text = txtCategory.Text = txtbillsq.Text = txtlabour.Text = txtGSTNo.Text = txtHsnNummber.Text = txtSacCode.Text = 
               txtPOContactPerson.Text=txtPOContactNumber.Text= string.Empty;
            //imglogo.Src = "";
            txtPhoneno.Text = txtFaxno.Text = txtEmail.Text = txtWebsite.Text = txtNotes.Text = txtregno.Text = txtcorporateIDNo.Text = string.Empty;
        }

        protected void btncancel_Click(object sender, EventArgs e)
        {
            clearData();
        }

        string ErrorMessage1 = "<br> Please Fill Check The Following Errors <br>";

        protected void GetWebConfigdata()
        {

            CmpIDPrefix = Session["CmpIDPrefix"].ToString();
            BranchID = Session["BranchID"].ToString();

        }



        protected void LoadPreviousData()
        {
            string selectquery = "select * from companyinfo   where  branchid  ='" + BranchID + "'";
            DataTable dt = config.ExecuteAdaptorAsyncWithQueryParams(selectquery).Result;

            if (dt.Rows.Count > 0)
            {
                txtcname.Text = dt.Rows[0]["companyname"].ToString();
                txtcsname.Text = dt.Rows[0]["shortname"].ToString();
                txtaddress.Text = dt.Rows[0]["address"].ToString();
                txtpfno.Text = dt.Rows[0]["pfno"].ToString();
                txtesino.Text = dt.Rows[0]["esino"].ToString();
                txtbilldesc.Text = dt.Rows[0]["billdesc"].ToString();
                txtcinfo.Text = dt.Rows[0]["companyinfo"].ToString();
                txtbnotes.Text = dt.Rows[0]["billnotes"].ToString();
                txtbillsq.Text = dt.Rows[0]["billseq"].ToString();
                txtlabour.Text = dt.Rows[0]["labourrule"].ToString();
                txtCategory.Text = dt.Rows[0]["Category"].ToString();
                txtGSTNo.Text = dt.Rows[0]["GSTNo"].ToString();
                txtHsnNummber.Text = dt.Rows[0]["HSNNumber"].ToString();
                txtSacCode.Text = dt.Rows[0]["SACCode"].ToString();
                txtPOContactPerson.Text = dt.Rows[0]["POContactPerson"].ToString();
                txtPOContactNumber.Text = dt.Rows[0]["POContactNumber"].ToString();

                //txtPREPARE.Text = dt.Rows[0]["ChequePrepare"].ToString();
                //txtBANK.Text = dt.Rows[0]["Bankname"].ToString();
                //txtAccountno.Text = dt.Rows[0]["bankaccountno"].ToString();
                //txtaddresslineone.Text = dt.Rows[0]["Addresslineone"].ToString();
                //txtaddresslinetwo.Text = dt.Rows[0]["Addresslinetwo"].ToString();
                //txtifsccode.Text = dt.Rows[0]["IfscCode"].ToString();

                txtPhoneno.Text = dt.Rows[0]["Phoneno"].ToString();
                txtFaxno.Text = dt.Rows[0]["Faxno"].ToString();
                txtEmail.Text = dt.Rows[0]["Emailid"].ToString();
                txtWebsite.Text = dt.Rows[0]["Website"].ToString();
                txtNotes.Text = dt.Rows[0]["Notes"].ToString();
                txtcorporateIDNo.Text = dt.Rows[0]["CorporateIDNo"].ToString();
                txtregno.Text = dt.Rows[0]["RegNo"].ToString();
                txtESICNoForms.Text = dt.Rows[0]["ESICNoForms"].ToString();
                txtBranchOffice.Text = dt.Rows[0]["BranchOffice"].ToString();
                //txtISOCertNo.Text = dt.Rows[0]["ISOCertfNo"].ToString();
                //txtPsaraAct.Text = dt.Rows[0]["PSARARegNo"].ToString();
                //txtKSSAMemberShipNo.Text = dt.Rows[0]["KSSAMembershipNo"].ToString();

                Byte[] barr = new Byte[100000];

                //if (String.IsNullOrEmpty(dt.Rows[0]["logo"].ToString()) == false)
                //{
                //    barr = (Byte[])dt.Rows[0]["logo"];
                //    Session["image"] = barr;
                //    MemoryStream a = new MemoryStream(barr, false);
                //    System.Drawing.Image image = System.Drawing.Image.FromStream(a);
                //    Random rnd = new Random();
                //    string imagename = rnd.Next() + ".jpg";
                //    image.Save(Server.MapPath("Images" + "\\" + "\\" + imagename), System.Drawing.Imaging.ImageFormat.Jpeg);
                //    imglogo.Src = "~/Images/" + imagename;
                //    // Session["Image"] = "~/Images/" + imagename;
                //}
            }

            else
            {

                Enabledfields();
            }
        }

        public void Enabledfields()
        {
            txtcname.Enabled = true;
            txtcsname.Enabled = true;
            txtaddress.Enabled = true;
            txtpfno.Enabled = true;
            txtesino.Enabled = true;
            txtbilldesc.Enabled = true;
            txtcinfo.Enabled = true;
            txtCategory.Enabled = true;
            txtbnotes.Enabled = true;
            txtBANK.Enabled = true;
            txtbillsq.Enabled = true;
            txtlabour.Enabled = true;
            txtPhoneno.Enabled = true;
            txtFaxno.Enabled = true;
            txtEmail.Enabled = true;
            txtWebsite.Enabled = true;
            txtNotes.Enabled = true;
            txtpfno.Enabled = true;
            txtcorporateIDNo.Enabled = true;
            txtregno.Enabled = true;
            txtESICNoForms.Enabled = true;
            txtBranchOffice.Enabled = true;
            txtISOCertNo.Enabled = true;
            txtPsaraAct.Enabled = true;
            txtKSSAMemberShipNo.Enabled = true;
            btnaddclint.Enabled = true;
            btncancel.Enabled = true;
            btnEdit.Enabled = false;
            lblresult.Visible = false;
            txtGSTNo.Enabled = true;
            txtHsnNummber.Enabled = true;
            txtSacCode.Enabled = true;
            txtPOContactPerson.Enabled = true;
            txtPOContactNumber.Enabled = true;


        }

        public void Disablefields()
        {

            txtESICNoForms.Enabled = false;
            txtBranchOffice.Enabled = false;
            txtISOCertNo.Enabled = false;
            txtPsaraAct.Enabled = false;
            txtKSSAMemberShipNo.Enabled = false;
            txtcname.Enabled = false;
            txtcsname.Enabled = false;
            txtaddress.Enabled = false;
            txtpfno.Enabled = false;
            txtesino.Enabled = false;
            txtbilldesc.Enabled = false;
            txtBANK.Enabled = false;
            txtcinfo.Enabled = false;
            txtCategory.Enabled = false;
            txtbnotes.Enabled = false;
            txtbillsq.Enabled = false;
            txtlabour.Enabled = false;
            txtPhoneno.Enabled = false;
            txtFaxno.Enabled = false;
            txtEmail.Enabled = false;
            txtWebsite.Enabled = false;
            txtNotes.Enabled = false;
            txtpfno.Enabled = false;
            txtcorporateIDNo.Enabled = false;
            txtregno.Enabled = false;
            btnaddclint.Enabled = false;
            btncancel.Enabled = false;
            btnEdit.Enabled = true;
            txtGSTNo.Enabled = false;
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            string selectquery = "select * from companyinfo  where  branchid  ='" + BranchID + "'";
            DataTable dt = config.ExecuteAdaptorAsyncWithQueryParams(selectquery).Result;

            if (dt.Rows.Count > 0)
            {
                Enabledfields();
            }
        }
    }
}