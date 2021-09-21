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
using System.Globalization;
using System.IO;
using System.Data.OleDb;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Jawan.Portal.DAL;
namespace Jawan.Portal
{
    public partial class PostingOrderList : System.Web.UI.Page
    {
        AppConfiguration config = new AppConfiguration();
        GridViewExportUtil gve = new GridViewExportUtil();
        int oderid = 0;
        string EmpIDPrefix = "";
        string CmpIDPrefix = "";
        string Elength = "";
        string Clength = "";

        private void OrderId()
        {
            string selectqueryoderid = "select max(cast(OrderId as int)) as OrderId from EmpTransfer ";
            DataTable dtable = config.ExecuteAdaptorAsyncWithQueryParams(selectqueryoderid).Result;
            if (dtable.Rows.Count > 0)
            {
                if (String.IsNullOrEmpty(dtable.Rows[0]["OrderId"].ToString()) == false)
                {
                    oderid = (Convert.ToInt32(dtable.Rows[0]["OrderId"].ToString())) + 1;
                    txtorderid.Text = oderid.ToString();
                }

                else
                {
                    txtorderid.Text = "1";
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            txtmonth.Text = DateTime.Now.ToString("dd/MM/yyyy");
            try
            {

                GetWebConfigdata();
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
                    OrderId();
                    LoadClientList();
                    LoadDesignations();
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void GetWebConfigdata()
        {

            EmpIDPrefix = Session["EmpIDPrefix"].ToString();
            CmpIDPrefix = Session["CmpIDPrefix"].ToString();
        }

        protected void LoadClientList()
        {
            // DataTable DtClientNames = GlobalData.Instance.LoadCIds(CmpIDPrefix);

            string Qry = "select ClientId,ClientId+'-'+ClientName as Name from Clients ";

            DataTable DtClientNames = config.ExecuteAdaptorAsyncWithQueryParams(Qry).Result;


            if (DtClientNames.Rows.Count > 0)
            {
                ddlFromClientid.DataValueField = "Clientid";
                ddlFromClientid.DataTextField = "Name";

                ddlToClientid.DataValueField = "Clientid";
                ddlToClientid.DataTextField = "Name";

                ddlFromClientid.DataSource = DtClientNames;
                ddlFromClientid.DataBind();

                ddlToClientid.DataSource = DtClientNames;
                ddlToClientid.DataBind();

            }
            ddlFromClientid.Items.Insert(0, "--Select--");
            ddlToClientid.Items.Insert(0, "--Select--");
        }

        protected void LoadDesignations()
        {
            string Sqlqry = "select DesignId,Design from designations";
            DataTable dt = config.ExecuteAdaptorAsyncWithQueryParams(Sqlqry).Result;
            if (dt.Rows.Count > 0)
            {
                ddlDesignation.DataValueField = "DesignId";
                ddlDesignation.DataTextField = "Design";

                ddlDesignation.DataSource = dt;
                ddlDesignation.DataBind();
            }
            ddlDesignation.Items.Insert(0, "--Select--");
        }

        protected void PreviligeUsers(int previligerid)
        {

            switch (previligerid)
            {

                case 1:

                    AddEmployeeLink.Visible = false;
                    ModifyEmployeeLink.Visible = false;
                    DeleteEmployeeLink.Visible = false;
                    AssigningWorkerLink.Visible = false;
                    break;
                case 2:

                    AddEmployeeLink.Visible = false;
                    ModifyEmployeeLink.Visible = false;
                    DeleteEmployeeLink.Visible = false;
                    AssigningWorkerLink.Visible = false;
                    ClientsLink.Visible = true;
                    CompanyInfoLink.Visible = false;
                    InventoryLink.Visible = false;
                    ReportsLink.Visible = true;
                    SettingsLink.Visible = true;
                    //TempTransferLink.Visible = false;
                    //DummyTransferLink.Visible = false;
                    break;

                case 3:

                    AddEmployeeLink.Visible = false;
                    ModifyEmployeeLink.Visible = false;
                    DeleteEmployeeLink.Visible = false;
                    AssigningWorkerLink.Visible = false;
                    ClientsLink.Visible = true;
                    CompanyInfoLink.Visible = false;
                    InventoryLink.Visible = false;
                    ReportsLink.Visible = true;
                    SettingsLink.Visible = true;
                    //PostingOrderLink.Visible = false;
                    //TempTransferLink.Visible = false;

                    break;

                case 4:
                    AddEmployeeLink.Visible = false;
                    ModifyEmployeeLink.Visible = false;
                    DeleteEmployeeLink.Visible = false;
                    AssigningWorkerLink.Visible = false;
                    ClientsLink.Visible = true;
                    CompanyInfoLink.Visible = true;
                    InventoryLink.Visible = true;
                    ReportsLink.Visible = true;
                    SettingsLink.Visible = true;
                    break;
                case 5:
                    AddEmployeeLink.Visible = false;
                    ModifyEmployeeLink.Visible = false;
                    DeleteEmployeeLink.Visible = false;
                    AssigningWorkerLink.Visible = false;
                    EmployeesLink.Visible = true;
                    ClientsLink.Visible = true;
                    CompanyInfoLink.Visible = false;
                    InventoryLink.Visible = false;
                    ReportsLink.Visible = true;
                    ReportsLink.Visible = true;
                    SettingsLink.Visible = true;
                    break;
                case 6:
                    AddEmployeeLink.Visible = false;
                    ModifyEmployeeLink.Visible = false;
                    DeleteEmployeeLink.Visible = false;
                    AssigningWorkerLink.Visible = false;
                    InventoryLink.Visible = false;
                    CompanyInfoLink.Visible = false;
                    ReportsLink.Visible = false;
                    SettingsLink.Visible = false;

                    break;
                default:
                    break;


            }
        }


        public void GetEmpData()
        {
            string Sqlqry = "select (empfname+' '+empmname+' '+emplname) as empname,convert(nvarchar(20),e.empdtofjoining,103) as empdtofjoining,e.unitid,d.design,e.EmpDesgn from empdetails e inner join designations d on e.empdesgn=d.designid  where empid='" + txtEmpid.Text + "' ";
            DataTable dt = config.ExecuteAdaptorAsyncWithQueryParams(Sqlqry).Result;
            if (dt.Rows.Count > 0)
            {
                try
                {
                    ddlDesignation.SelectedValue = dt.Rows[0]["EmpDesgn"].ToString();
                    txtDOJ.Text = dt.Rows[0]["empdtofjoining"].ToString();
                    ddlFromClientid.SelectedValue = dt.Rows[0]["unitid"].ToString();
                    ddlToClientid.SelectedValue = dt.Rows[0]["unitid"].ToString();

                }
                catch (Exception ex)
                {
                    // MessageLabel.Text = ex.Message;
                }
            }
            else
            {
                // MessageLabel.Text = "There Is No Name For The Selected Employee";
            }


        }


        protected void txtEmpid_TextChanged(object sender, EventArgs e)
        {
            GetEmpName();

        }

        protected void GetEmpName()
        {
            string Sqlqry = "select (empfname+' '+empmname+' '+emplname) as empname,convert(nvarchar(20),e.empdtofjoining,103) as empdtofjoining,e.unitid,d.design,e.EmpDesgn from empdetails e inner join designations d on e.empdesgn=d.designid  where empid='" + txtEmpid.Text + "' ";
            DataTable dt = config.ExecuteAdaptorAsyncWithQueryParams(Sqlqry).Result;
            if (dt.Rows.Count > 0)
            {
                try
                {
                    txtName.Text = dt.Rows[0]["empname"].ToString();
                    ddlDesignation.SelectedValue = dt.Rows[0]["EmpDesgn"].ToString();
                    txtDOJ.Text = dt.Rows[0]["empdtofjoining"].ToString();
                    ddlFromClientid.SelectedValue = dt.Rows[0]["unitid"].ToString();
                    ddlToClientid.SelectedValue = dt.Rows[0]["unitid"].ToString();

                }
                catch (Exception ex)
                {
                    // MessageLabel.Text = ex.Message;
                }
            }
            else
            {
                // MessageLabel.Text = "There Is No Name For The Selected Employee";
            }



        }

        protected void GetEmpid()
        {

            #region  Old Code
            string Sqlqry = "select e.Empid, convert(nvarchar(20),e.empdtofjoining,103) as empdtofjoining,e.unitid,d.design,e.EmpDesgn from empdetails e inner join designations d on e.empdesgn=d.designid where (e.empfname+' '+e.empmname+' '+e.emplname)  like '" + txtName.Text + "' ";
            DataTable dt = config.ExecuteAdaptorAsyncWithQueryParams(Sqlqry).Result;
            if (dt.Rows.Count > 0)
            {
                try
                {
                    txtEmpid.Text = dt.Rows[0]["Empid"].ToString();
                    ddlDesignation.SelectedValue = dt.Rows[0]["EmpDesgn"].ToString();
                    txtDOJ.Text = dt.Rows[0]["empdtofjoining"].ToString();
                    ddlFromClientid.SelectedValue = dt.Rows[0]["unitid"].ToString();
                    ddlToClientid.SelectedValue = dt.Rows[0]["unitid"].ToString();

                }
                catch (Exception ex)
                {
                    // MessageLabel.Text = ex.Message;
                }
            }
            else
            {
                // MessageLabel.Text = "There Is No Name For The Selected Employee";
            }
            #endregion // End Old Code


        }

        protected void txtName_TextChanged(object sender, EventArgs e)
        {
            GetEmpid();
        }

        protected void ClearData()
        {
            //ddlempid.SelectedIndex = 0;
            //ddlempname.SelectedIndex = 0;
            //ddlUnit.SelectedIndex = 0;
            //ddlcname.SelectedIndex = 0;
            //ddlDesignation.SelectedIndex = 0;
            //gvemppostingorder.DataSource = null;
            //gvemppostingorder.DataBind();

            txtEmpid.Text = "";
            txtName.Text = "";
            txtDOJ.Text = "";
            ddlDesignation.SelectedIndex = 0;
            ddlFromClientid.SelectedIndex = 0;
            ddlToClientid.SelectedIndex = 0;
            txtReason.Text = "";

        }


        public void MovementOrder(string Empid)
        {
            int Fontsize = 11;
            string fontsyle = "TimesNewroman";

            string CompanyName = "";

            string id = Empid;
            string name = "";
            string Design = "";
            string fromUnit = "";
            string toUnit = "";
            string Address = "";
            string phno = "";
            string compMail = "";
            string title = "";
            var DtOfTransfer = string.Empty;
            var FromPeriod = string.Empty;
            var ToPeriod = string.Empty;
            string Reason = "";
            string CreatedBy = "";
            var CreatedOn = string.Empty;
            int OrderId = 0;
            string OrderIDParam = "";
            Hashtable htParams = new Hashtable();

            if (txtEmpid.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Show alert", "alert('Please select Employee');", true);
                return;
            }


            //string query = "select * from CompanyInfo";
            //DataTable dtcompnyInfo = config.ExecuteAdaptorAsyncWithQueryParams(query).Result;

            //if (dtcompnyInfo.Rows.Count > 0)
            //{
            //    CompanyName = dtcompnyInfo.Rows[0]["CompanyName"].ToString();
            //    Address = dtcompnyInfo.Rows[0]["Address"].ToString();
            //   // phno = dtcompnyInfo.Rows[0]["CompanyPhone"].ToString();
            //   // compMail = dtcompnyInfo.Rows[0]["CompanyEmail"].ToString();

            //}

          

            MemoryStream ms = new MemoryStream();
            string fileheader2 = Empid + "MovementOrder" + " .Pdf";
            Document document = new Document(PageSize.LEGAL);
            // var output = new FileStream(fileheader2, FileMode., FileAccess.Write, FileShare.None);
            var writer = PdfWriter.GetInstance(document, ms);
            document.Open();
            BaseFont bf = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            string strQry = "Select * from CompanyInfo";
            DataTable compInfo = config.ExecuteAdaptorAsyncWithQueryParams(strQry).Result;
            //string companyName = "Your Company Name";
            //string companyAddress = "Your Company Address";

            CompanyName = compInfo.Rows[0]["CompanyName"].ToString();
              Address = compInfo.Rows[0]["Address"].ToString();


            string OrderQry = "select max(OrderId) as OrderId from EmpTransfer where empid='"+id+"'";
            DataTable dtOrder = config.ExecuteAdaptorAsyncWithQueryParams(OrderQry).Result;
            if (dtOrder.Rows.Count > 0)
            {
                OrderIDParam = dtOrder.Rows[0]["OrderId"].ToString();
            }


            string SPName = "MovementOrder";
            htParams.Add("@Empid", id);
            htParams.Add("@OrderId", OrderIDParam);

            DataTable dt = config.ExecuteAdaptorAsyncWithParams(SPName, htParams).Result;

            //DataTable dt = config.ExecuteAdaptorAsyncWithQueryParams(Qry).Result;

            if (dt.Rows.Count > 0)
            {

                name = dt.Rows[0]["empname"].ToString();
                Design = dt.Rows[0]["design"].ToString();
                fromUnit = dt.Rows[0]["Fromunit"].ToString();
                toUnit = dt.Rows[0]["tounit"].ToString();
                DtOfTransfer = dt.Rows[0]["DtofTransfer"].ToString();
                FromPeriod = dt.Rows[0]["FromPeriodFormat"].ToString();
                ToPeriod = dt.Rows[0]["ToPeriod"].ToString();
                Reason = dt.Rows[0]["reason"].ToString();
                OrderId = int.Parse(dt.Rows[0]["orderid"].ToString());
                title = dt.Rows[0]["EmpTitle"].ToString();
                if(title=="")
                {
                    title = "";
                }
                else
                {
                    title = title + ".";
                }


                #region For PDF
                document.AddTitle(CompanyName);
                document.AddAuthor("DIYOS");
                document.AddSubject("Invoice");
                document.AddKeywords("Keyword1, keyword2, …");
                string imagepath = Server.MapPath("~/assets/JGSlogoBW.jpg");
                string imagepath2 = Server.MapPath("assets/Images/sign.jpg");
                //if (File.Exists(imagepath))
                //{
                //    iTextSharp.text.Image gif2 = iTextSharp.text.Image.GetInstance(imagepath);
                //    gif2.Alignment = (iTextSharp.text.Image.ALIGN_LEFT | iTextSharp.text.Image.UNDERLYING);
                //    //  gif2.ScalePercent(10f);
                //    //  gif2.SetAbsolutePosition(12f, 740f);
                //    gif2.ScaleAbsolute(80, 70);
                   
                //    document.Add(gif2);
                //}
                PdfContentByte content = writer.DirectContent;
                for (int i = 0; i < 2; i++)
                {

                    PdfPTable table1 = new PdfPTable(6);
                    table1.TotalWidth = 570f;
                    table1.LockedWidth = true;
                    float[] width1 = new float[] { 1.5f, 2f, 2f, 2f, 1.5f, 2f };
                    table1.SetWidths(width1);

                    PdfPCell cellspace = new PdfPCell(new Phrase("  ", FontFactory.GetFont(fontsyle, Fontsize - 2, Font.BOLD, BaseColor.BLACK)));
                    cellspace.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    cellspace.Colspan = 6;
                    cellspace.Border = 0;
                    cellspace.PaddingTop = -5;

                    PdfPCell cellHead = new PdfPCell(new Phrase(CompanyName, FontFactory.GetFont(fontsyle, Fontsize + 6, Font.BOLD, BaseColor.BLACK)));
                    cellHead.HorizontalAlignment = 0;
                    cellHead.Colspan = 4;
                    cellHead.Border = 0;
                    cellHead.SetLeading(0f, 1.0f);
                    cellHead.PaddingTop = 10;
                    cellHead.PaddingLeft = 40;
                    table1.AddCell(cellHead);

                    if(File.Exists(imagepath))
                    { 
                    iTextSharp.text.Image icici = iTextSharp.text.Image.GetInstance(imagepath);
                    //icici.Alignment = (iTextSharp.text.Image.ALIGN_RIGHT | iTextSharp.text.Image.UNDERLYING);
                    icici.ScaleAbsolute(150f, 90f);
                    PdfPCell companylogo = new PdfPCell();
                    Paragraph cmplogo = new Paragraph();
                    cmplogo.Add(new Chunk(icici, 0, 0));
                    companylogo.PaddingLeft = -10;
                    companylogo.AddElement(cmplogo);
                    companylogo.HorizontalAlignment = 0;
                    companylogo.Colspan = 2;
                    companylogo.Border = 0;
                    table1.AddCell(companylogo);
                    }


                    cellHead = new PdfPCell(new Phrase(Address, FontFactory.GetFont(fontsyle, Fontsize, Font.NORMAL, BaseColor.BLACK)));
                    cellHead.HorizontalAlignment = 1;
                    cellHead.Colspan = 6;
                    cellHead.Border = 0;
                    cellHead.SetLeading(0f, 1.2f);
                    cellHead.PaddingTop = -65;
                    cellHead.PaddingLeft = -140;
                    //cellHead.PaddingLeft =124;
                    table1.AddCell(cellHead);

                    document.Add(table1);

                    PdfPTable table = new PdfPTable(6);
                    table.TotalWidth = 570f;
                    table.LockedWidth = true;
                    float[] width = new float[] { 2f, 4f, 1.5f, 4f, 5f, 5f };
                    table.SetWidths(width);

                    PdfPCell cell = new PdfPCell();

                    cell = new PdfPCell(new Phrase("MOVEMENT ORDER", FontFactory.GetFont(fontsyle, Fontsize+2, Font.BOLD | Font.UNDERLINE, BaseColor.BLACK)));
                    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    cell.Colspan = 6;
                    cell.BorderWidthBottom = 0;
                    cell.BorderWidthRight = 0;
                    cell.BorderWidthLeft = 0;
                    cell.BorderWidthTop = -10;
                    cell.PaddingTop = 30f;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Reference No" + " :", FontFactory.GetFont(fontsyle, Fontsize, Font.BOLD, BaseColor.BLACK)));
                    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    cell.Colspan = 3;
                    cell.Border = 0;
                    cell.PaddingTop = 30f;
                    cell.PaddingLeft = 20;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Date" + ": " + DtOfTransfer, FontFactory.GetFont(fontsyle, Fontsize, Font.BOLD, BaseColor.BLACK)));
                    cell.HorizontalAlignment = 2; //0=Left, 1=Centre, 2=Right
                    cell.Colspan = 3;
                    cell.Border = 0;
                    cell.PaddingTop = 20f;
                    cell.PaddingRight = 40;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("To, ", FontFactory.GetFont(fontsyle, Fontsize, Font.NORMAL, BaseColor.BLACK)));
                    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    cell.Colspan = 6;
                    cell.Border = 0;
                    cell.PaddingTop = 15f;
                    cell.PaddingLeft = 20;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("The Site Manager", FontFactory.GetFont(fontsyle, Fontsize, Font.NORMAL, BaseColor.BLACK)));
                    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    cell.Colspan = 6;
                    cell.Border = 0;
                    cell.PaddingTop = 5f;
                    cell.PaddingLeft = 20;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(" " + toUnit, FontFactory.GetFont(fontsyle, Fontsize, Font.NORMAL, BaseColor.BLACK)));
                    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    cell.Colspan = 6;
                    cell.Border = 0;
                    cell.PaddingTop = 5f;
                    cell.PaddingLeft = 20;
                    table.AddCell(cell);

                    //cell = new PdfPCell(new Phrase("We are directing Mr."+ name+ "ID No" + "." + id +" "+ "for the post of  Security Gard/ASO to perform the Duties WEF:- "+ FromPeriod + " " +"at your Organization.in place of "+" "+ "Mr." +"                  "+" "+ "to report to HR for further Instructions.", FontFactory.GetFont(fontsyle, Fontsize, Font.NORMAL, BaseColor.BLACK)));
                    //cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right

                    Paragraph CcellHead52 = new Paragraph();
                    CcellHead52.Add(new Chunk("This is to inform you that we are directing "+title+"" + name + "with reference ID Number" + "." + id + " " + "for the post of (Security Guard/SO/ASO/Others)", FontFactory.GetFont(fontsyle, Fontsize, Font.NORMAL, BaseColor.BLACK)));
                    CcellHead52.Add(new Chunk(" With effect from " + FromPeriod + " " + "at your Organization.", FontFactory.GetFont(fontsyle, Fontsize, Font.NORMAL, BaseColor.BLACK)));
                    cell.AddElement(CcellHead52);
                    cell.Colspan = 6;
                    cell.Border = 0;
                    cell.PaddingTop = 15f;
                    cell.PaddingLeft = 20;
                    cell.PaddingRight = 20;
                    table.AddCell(cell);

                    Paragraph ccellhead5 = new Paragraph();
                    ccellhead5.Add(new Chunk("Request you to kindly check his ID Proof like (Aadhaar Card/Voter ID),and allow him to take charge at your site.", FontFactory.GetFont(fontsyle, Fontsize, Font.NORMAL, BaseColor.BLACK)));

                    PdfPCell cell1 = new PdfPCell();
                    ccellhead5.Add(new Chunk("Please send confirmation on the same to our HR department on his/her joining at your site.", FontFactory.GetFont(fontsyle, Fontsize, Font.NORMAL, BaseColor.BLACK)));
                   cell1.AddElement(ccellhead5);
                    cell1.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    cell1.Colspan = 6;
                    cell1.Border = 0;
                    cell1.PaddingTop = 15f;
                    cell1.PaddingLeft = 20;
                    table.AddCell(cell1);

                    cell = new PdfPCell(new Phrase("  ", FontFactory.GetFont(fontsyle, Fontsize, Font.NORMAL, BaseColor.BLACK)));
                    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    cell.Colspan = 6;
                    cell.Border = 0;
                    cell.PaddingTop = 20f;
                    table.AddCell(cell);

                    //iTextSharp.text.Image IssuingAuth = iTextSharp.text.Image.GetInstance(imagepath2);
                    //// IssuingAuth.ScalePercent(50f);
                    //IssuingAuth.ScaleAbsolute(40f, 20f);
                    //PdfPCell Authority = new PdfPCell();
                    //Paragraph Authoritylogo = new Paragraph();
                    //Authoritylogo.Add(new Chunk(IssuingAuth, 45f, -4f));
                    //Authority.AddElement(Authoritylogo);
                    ////Authority.HorizontalAlignment = 1;
                    //Authority.HorizontalAlignment = Element.ALIGN_CENTER;
                    //Authority.Colspan = 6;
                    //Authority.Border = 0;
                    //Authority.PaddingLeft = 350;
                    ////Authority.PaddingTop = -12;
                    //table.AddCell(Authority);

                    cell = new PdfPCell(new Phrase("For Jawan Guarding Services Pvt Ltd", FontFactory.GetFont(fontsyle, Fontsize, Font.BOLD, BaseColor.BLACK)));
                    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    cell.Colspan = 4;
                    cell.Border = 0;
                    //cell.PaddingLeft = 250;
                    cell.PaddingTop = 10f;
                    table.AddCell(cell);


                    cell = new PdfPCell(new Phrase("Site Manager in Acceptance of His/Her Joining", FontFactory.GetFont(fontsyle, Fontsize, Font.BOLD, BaseColor.BLACK)));
                    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    cell.Colspan = 2;
                    cell.Border = 0;
                    //cell.PaddingLeft = 230;
                    cell.PaddingTop = 10f;
                    table.AddCell(cell);

                    
                    cell = new PdfPCell(new Phrase(" ", FontFactory.GetFont(fontsyle, Fontsize, Font.BOLD, BaseColor.BLACK)));
                    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    cell.Colspan = 6;
                    cell.Border = 0;
                    cell.PaddingLeft = 230;
                    cell.FixedHeight = 50f;
                    table.AddCell(cell);
                    document.Add(table);

                }

                document.Close();
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment;filename=Transfer.pdf");
                Response.Buffer = true;
                Response.Clear();
                Response.OutputStream.Write(ms.GetBuffer(), 0, ms.GetBuffer().Length);
                Response.OutputStream.Flush();
                Response.End();

                #endregion for PDF

            }

        }

        protected void btntransfer_Click(object sender, EventArgs e)
        {
            try
            {

                string Empid = "";
                int Design = 0;
                string fromUnit = "";
                string toUnit = "";
                var DtOfTransfer = string.Empty;
                var FromPeriod = string.Empty;
                var ToPeriod = string.Empty;
                string Reason = "";
                string CreatedBy = "";
                var CreatedOn = string.Empty;
                int OrderId = 0;

                var testDate = 0;

                if (txtEmpid.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('Please Select EmpId');", true);
                    return;
                }

                #region Begin validating dateformat
                if (txtmonth.Text.Trim().Length > 0)
                {
                    testDate = GlobalData.Instance.CheckEnteredDate(txtmonth.Text);
                    if (testDate > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('You Are Entered Invalid Transfer DATE.Date Format Should be [DD/MM/YYYY].Ex.01/01/1990');", true);
                        return;
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please select Transfer Date');", true);
                    return;
                }

                if (txtFromPeriod.Text.Trim().Length > 0)
                {
                    testDate = GlobalData.Instance.CheckEnteredDate(txtFromPeriod.Text);
                    if (testDate > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('You Are Entered Invalid From Period Date.Date Format Should be [DD/MM/YYYY].Ex.01/01/1990');", true);
                        return;
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please select From Period Date');", true);
                    return;
                }

                if (txtToPeriod.Text.Trim().Length > 0)
                {
                    testDate = GlobalData.Instance.CheckEnteredDate(txtToPeriod.Text);
                    if (testDate > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "show alert", "alert('You Are Entered Invalid To Period Date.Date Format Should be [DD/MM/YYYY].Ex.01/01/1990');", true);
                        return;
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please select To Period Date');", true);
                    return;
                }

                #endregion

                Empid = txtEmpid.Text;
                Design = int.Parse(ddlDesignation.SelectedValue);
                if (ddlFromClientid.SelectedIndex == 0)
                {
                    fromUnit = "0";
                }
                else
                {
                    fromUnit = ddlFromClientid.SelectedValue;
                }
                if (ddlToClientid.SelectedIndex == 0)
                {
                    toUnit = "0";
                }
                else
                {
                    toUnit = ddlToClientid.SelectedValue;
                }
                //  DtOfTransfer = Timings.Instance.CheckDateFormat(txtmonth.Text);
                Reason = txtReason.Text;
                CreatedBy = lblDisplayUser.Text;


                if (txtmonth.Text.Trim().Length != 0)
                {
                    DtOfTransfer = Timings.Instance.CheckDateFormat(txtmonth.Text);

                }
                else
                {
                    DtOfTransfer = "01/01/1900";
                }

                if (txtFromPeriod.Text.Trim().Length != 0)
                {
                    FromPeriod = Timings.Instance.CheckDateFormat(txtFromPeriod.Text);
                }
                else
                {
                    FromPeriod = "01/01/1900";
                }

                if (txtToPeriod.Text.Trim().Length != 0)
                {
                    ToPeriod = Timings.Instance.CheckDateFormat(txtToPeriod.Text);
                }
                else
                {
                    ToPeriod = "01/01/1900";
                }


                CreatedOn = Timings.Instance.CheckDateFormat(DateTime.Now.ToString("dd/MM/yyyy"));

                OrderId = int.Parse(txtorderid.Text);

                string sqlqry = string.Format("insert into EmpTransfer(Empid,Design,FromUnit,ToUnit,DtofTransfer,Reason,CreatedBy,CreatedOn,OrderId,FromPeriod,ToPeriod) values('{0}',{1},'{2}','{3}','{4}','{5}','{6}','{7}',{8},'{9}','{10}')", Empid, Design, fromUnit, toUnit, DtOfTransfer, Reason, CreatedBy, DateTime.Now, OrderId, FromPeriod, ToPeriod);
                int Status = config.ExecuteNonQueryWithQueryAsync(sqlqry).Result;





                Clear();

            }
            catch (Exception ex)
            {


            }
        }



        protected void btnPDF_Click(object sender, EventArgs e)
        {
            MovementOrder(txtEmpid.Text);
        }

        public void Clear()
        {
            ddlDesignation.SelectedIndex = 0;
            txtDOJ.Text = "";
            txtFromPeriod.Text = "";
            txtToPeriod.Text = "";
            ddlFromClientid.SelectedIndex = 0;
            ddlToClientid.SelectedIndex = 0;
            txtReason.Text = "";
            OrderId();

        }
    }
}