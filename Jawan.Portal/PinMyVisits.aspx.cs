using System;
using System.Collections;
using System.Data;
using System.Web.UI;
using KLTS.Data;
using System.Globalization;
using Jawan.Portal.DAL;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.Services;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Script.Serialization;
using iTextSharp.text.pdf;
using System.Web;
using iTextSharp.text;
using System.Net.Mail;
using System.Net;
using System.Text;


namespace Jawan.Portal
{
    public partial class PinMyVisits : System.Web.UI.Page
    {
        GridViewExportUtil GVUtil = new GridViewExportUtil();
        AppConfiguration config = new AppConfiguration();
        DataTable dt;
        string EmpIDPrefix = "";
        string CmpIDPrefix = "";
        string BranchID = "";


        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                GetWebConfigdata();
                if (!IsPostBack)
                {
                    if (Session["UserId"] != null && Session["AccessLevel"] != null)
                    {
                    }
                    else
                    {
                        Response.Redirect("login.aspx");
                    }



                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Show alert", "alert('Your Session Expired');", true);
                Response.Redirect("~/Login.aspx");
            }
        }


        protected void GetWebConfigdata()
        {
            EmpIDPrefix = Session["EmpIDPrefix"].ToString();
            CmpIDPrefix = Session["CmpIDPrefix"].ToString();
            BranchID = Session["BranchID"].ToString();
        }





        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (txtMonth.Text.Trim().Length == 0)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "showlalert", "alert('Please Select Month');", true);
                return;
            }

            if (txtEmpIDName.Text.Trim().Length == 0)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "showlalert", "alert('Please Select Emp ID');", true);
                return;
            }

            string date = string.Empty;

            if (txtMonth.Text.Trim().Length > 0)
            {
                date = DateTime.Parse(txtMonth.Text.Trim(), CultureInfo.GetCultureInfo("en-gb")).ToString("yyyy/MM/dd");
            }

            string Day = DateTime.Parse(date).Day.ToString();
            string month = DateTime.Parse(date).Month.ToString();
            string Year = DateTime.Parse(date).Year.ToString();

            string Empid = "";

            if (txtEmpIDName.Text.Length > 9)
            {
                Empid = txtEmpIDName.Text.Substring(0, 9);
            }
            else
            {
                Empid = txtEmpIDName.Text;
            }


            string Spname = "GetPinMyvisitImages";
            Hashtable ht = new Hashtable();
            ht.Add("@Day", Day);
            ht.Add("@month", month);
            ht.Add("@Year", Year);
            ht.Add("@CompanyID", "6");
            ht.Add("@Empid", Empid);
            ht.Add("@Type", "GetData");

            DataTable dt = config.ExecuteAdaptorAsyncWithParams(Spname, ht).Result;
            if (dt.Rows.Count > 0)
            {
                GVpinmyvisit.DataSource = dt;
                GVpinmyvisit.DataBind();



                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Button btnview = GVpinmyvisit.Rows[i].FindControl("btnview") as Button;

                    if (dt.Rows[i]["pitstopImage"].ToString() != "0")
                    {
                        btnview.Visible = true;
                    }
                    else
                    {
                        btnview.Visible = false;

                    }

                }



            }
            else
            {
                GVpinmyvisit.DataSource = null;
                GVpinmyvisit.DataBind();
            }
        }

        public string Getmonthval()
        {
            string date = string.Empty;
            string month = "";
            string Year = "";
            string monthval = "";


            if (txtMonth.Text.Trim().Length > 0)
            {
                date = DateTime.Parse(txtMonth.Text.Trim(), CultureInfo.GetCultureInfo("en-gb")).ToString();
                month = DateTime.Parse(date).Month.ToString();
                Year = DateTime.Parse(date).Year.ToString();
                monthval = month + Year.Substring(2, 2);
            }

            return monthval;

        }


        protected void gvdata_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                //add the thead and tbody section programatically
                e.Row.TableSection = TableRowSection.TableHeader;
            }
        }

        protected void btnGetImage_Click(object sender, EventArgs e)
        {
            if (txtMonth.Text.Trim().Length == 0)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "showlalert", "alert('Please Select Month');", true);
                return;
            }

            string date = string.Empty;

            if (txtMonth.Text.Trim().Length > 0)
            {
                date = DateTime.Parse(txtMonth.Text.Trim(), CultureInfo.GetCultureInfo("en-gb")).ToString("yyyy/MM/dd");
            }

            string Day = DateTime.Parse(date).Day.ToString();
            string month = DateTime.Parse(date).Month.ToString();
            string Year = DateTime.Parse(date).Year.ToString();


            string Empid = "";

            if (txtEmpIDName.Text.Length > 9)
            {
                Empid = txtEmpIDName.Text.Substring(0, 9);
            }
            else
            {
                Empid = txtEmpIDName.Text;
            }

            string Spname = "GetPinMyvisitImages";

            Hashtable ht = new Hashtable();
            ht.Add("@Day", Day);
            ht.Add("@month", month);
            ht.Add("@Year", Year);
            ht.Add("@CompanyID", "6");
            ht.Add("@Empid", Empid);
            ht.Add("@PitstopAttachmentId", hfPitstopAttachmentId.Value);
            ht.Add("@Type", "GetImageData");

            DataTable dt = config.ExecuteAdaptorAsyncWithParams(Spname, ht).Result;

            if (dt.Rows.Count > 0)
            {
                string imageUrl = dt.Rows[0]["pitstopImage"].ToString();

                if (dt.Rows[0]["pitstopImage"].ToString().Length > 0)
                {
                    imgphoto.ImageUrl = imageUrl;
                }


                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup();", true);
            }
        }

        protected void lbtn_Export_Click(object sender, EventArgs e)
        {
            if (txtMonth.Text.Trim().Length == 0)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "showlalert", "alert('Please Select Month');", true);
                return;
            }

            if (txtEmpIDName.Text.Trim().Length == 0)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "showlalert", "alert('Please Select Emp ID');", true);
                return;
            }

            string date = string.Empty;

            if (txtMonth.Text.Trim().Length > 0)
            {
                date = DateTime.Parse(txtMonth.Text.Trim(), CultureInfo.GetCultureInfo("en-gb")).ToString("yyyy/MM/dd");
            }

            string Day = DateTime.Parse(date).Day.ToString();
            string month = DateTime.Parse(date).Month.ToString();
            string Year = DateTime.Parse(date).Year.ToString();

            string Empid = "";
            string DType = "Excel";

            Empid = txtEmpIDName.Text;


            string Spname = "GetPinMyvisitImages";
            Hashtable ht = new Hashtable();
            ht.Add("@Day", Day);
            ht.Add("@month", month);
            ht.Add("@Year", Year);
            ht.Add("@CompanyID", "6");
            ht.Add("@Empid", Empid);
            ht.Add("@Type", "GetData");
            ht.Add("@DType", DType);


            DataTable dt = config.ExecuteAdaptorAsyncWithParams(Spname, ht).Result;
            if (dt.Rows.Count > 0)
            {
                GVUtil.NewExportExcel("PinMyvisit.xlsx", dt);
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string FontStyle = "calibri";

            string date = string.Empty;

            if (txtMonth.Text.Trim().Length > 0)
            {
                date = DateTime.Parse(txtMonth.Text.Trim(), CultureInfo.GetCultureInfo("en-gb")).ToString("yyyy/MM/dd");
            }

            string Day = DateTime.Parse(date).Day.ToString();
            string month = DateTime.Parse(date).Month.ToString();
            string Year = DateTime.Parse(date).Year.ToString();


            string Empid = "";


            Empid = txtEmpIDName.Text;



            string Spname = "GetPinMyvisitImages";

            Hashtable ht = new Hashtable();
            ht.Add("@Day", Day);
            ht.Add("@month", month);
            ht.Add("@Year", Year);
            ht.Add("@CompanyID", "6");
            ht.Add("@Empid", Empid);
            ht.Add("@Type", "GetImageDataByEmpid");

            DataTable dt = config.ExecuteAdaptorAsyncWithParams(Spname, ht).Result;

            if (dt.Rows.Count > 0)
            {

                byte[] bytes = null;

                System.IO.MemoryStream memoryStream = new System.IO.MemoryStream();

                Document document = new Document(PageSize.A4);
                PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
                writer.CloseStream = false;
                document.Open();


                for (int k = 0; k < dt.Rows.Count; k++)
                {

                    if (dt.Rows[k]["pitstopImage"].ToString().Length > 0)
                    {


                        //string imageUrl = dt.Rows[k]["pitstopImage"].ToString();
                        //byte[] imageBytes = null;
                        //imageBytes = Convert.FromBase64String(imageUrl);
                        //iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(imageBytes);
                        //image.ScalePercent(20f);
                        //image.ScaleAbsolute(120f, 129f);
                        //document.Add(image);


                        PdfPTable table2 = new PdfPTable(2);
                        table2.TotalWidth = 500f;
                        table2.LockedWidth = true;
                        float[] width2 = new float[] { 2f, 2f };
                        table2.SetWidths(width2);


                        PdfPTable tempTable1 = new PdfPTable(1);
                        tempTable1.TotalWidth = 400f;
                        tempTable1.LockedWidth = true;
                        float[] tempWidth1 = new float[] { 2f };
                        tempTable1.SetWidths(tempWidth1);

                        PdfPCell cell = new PdfPCell(new Paragraph("Created On : " + dt.Rows[k]["Updatedon"].ToString(), FontFactory.GetFont(FontStyle, 11, Font.BOLD, BaseColor.BLACK)));
                        cell.HorizontalAlignment = 0;
                        cell.Border = 0;
                        cell.Colspan = 1;
                        tempTable1.AddCell(cell);

                         cell = new PdfPCell(new Paragraph("Emp ID : " + dt.Rows[k]["UpdatedBy"].ToString(), FontFactory.GetFont(FontStyle, 11, Font.NORMAL, BaseColor.BLACK)));
                        cell.HorizontalAlignment = 0;
                        cell.Border = 0;
                        cell.Colspan = 1;
                        tempTable1.AddCell(cell);

                        cell = new PdfPCell(new Paragraph("Name : " + dt.Rows[k]["Name"].ToString(), FontFactory.GetFont(FontStyle, 11, Font.NORMAL, BaseColor.BLACK)));
                        cell.HorizontalAlignment = 0;
                        cell.Border = 0;
                        cell.Colspan = 1;
                        tempTable1.AddCell(cell);

                        cell = new PdfPCell(new Paragraph("Client ID : " + dt.Rows[k]["Clientid"].ToString(), FontFactory.GetFont(FontStyle, 11, Font.NORMAL, BaseColor.BLACK)));
                        cell.HorizontalAlignment = 0;
                        cell.Border = 0;
                        cell.Colspan = 1;
                        tempTable1.AddCell(cell);


                        cell = new PdfPCell(new Paragraph("Activity : " + dt.Rows[k]["Activity"].ToString(), FontFactory.GetFont(FontStyle, 11, Font.NORMAL, BaseColor.BLACK)));
                        cell.HorizontalAlignment = 0;
                        cell.Border = 0;
                        cell.Colspan = 1;
                        tempTable1.AddCell(cell);

                       

                        cell = new PdfPCell(new Paragraph("Remarks : " + dt.Rows[k]["Remarks"].ToString(), FontFactory.GetFont(FontStyle, 11, Font.NORMAL, BaseColor.BLACK)));
                        cell.HorizontalAlignment = 0;
                        cell.Border = 0;
                        cell.Colspan = 1;
                        tempTable1.AddCell(cell);

                        cell = new PdfPCell(new Paragraph("Checkin Lat : " + dt.Rows[k]["CheckinLat"].ToString(), FontFactory.GetFont(FontStyle, 11, Font.NORMAL, BaseColor.BLACK)));
                        cell.HorizontalAlignment = 0;
                        cell.Border = 0;
                        cell.Colspan = 1;
                        tempTable1.AddCell(cell);

                        cell = new PdfPCell(new Paragraph("Checkin Lng : " + dt.Rows[k]["CheckinLng"].ToString(), FontFactory.GetFont(FontStyle, 11, Font.NORMAL, BaseColor.BLACK)));
                        cell.HorizontalAlignment = 0;
                        cell.Border = 0;
                        cell.Colspan = 1;
                        tempTable1.AddCell(cell);

                        cell = new PdfPCell(new Paragraph("Checkin Address : " + dt.Rows[k]["Address"].ToString(), FontFactory.GetFont(FontStyle, 11, Font.NORMAL, BaseColor.BLACK)));
                        cell.HorizontalAlignment = 0;
                        cell.Border = 0;
                        cell.Colspan = 1;
                        tempTable1.AddCell(cell);

                        PdfPCell childTable1 = new PdfPCell(tempTable1);
                        childTable1.Border = 1;
                        childTable1.Colspan = 1;
                        childTable1.HorizontalAlignment = 0;
                        childTable1.FixedHeight = 150;
                        childTable1.PaddingLeft = 210;
                        childTable1.PaddingTop = 10;
                        tempTable1.AddCell(childTable1);

                        table2.AddCell(childTable1);

                        PdfPTable tempTable2 = new PdfPTable(1);
                        tempTable2.TotalWidth = 100f;
                        tempTable2.LockedWidth = true;
                        float[] tempWidth2 = new float[] { 2f };
                        tempTable2.SetWidths(tempWidth2);

                        string imageUrl = dt.Rows[k]["pitstopImage"].ToString();
                        byte[] imageBytes = null;
                        imageBytes = Convert.FromBase64String(imageUrl);
                        iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(imageBytes);
                        image.ScalePercent(15f);
                        image.ScaleAbsolute(80f, 80f);
                        PdfPCell imageCell = new PdfPCell(image);
                        imageCell.Colspan = 1; // either 1 if you need to insert one cell
                        imageCell.Border = 0;
                        imageCell.HorizontalAlignment = 2;
                        tempTable2.AddCell(imageCell);

                        PdfPCell childTable2 = new PdfPCell(tempTable2);
                        childTable2.Border = 1;
                        childTable2.Colspan = 1;
                        childTable2.HorizontalAlignment = 0;
                        childTable2.FixedHeight = 100;
                        childTable2.PaddingLeft = 100;
                        childTable2.PaddingTop = 10;
                        tempTable2.AddCell(childTable2);

                        table2.AddCell(childTable2);


                        document.Add(table2);


                    }

                }

                document.Close();

                bytes = memoryStream.ToArray();
                memoryStream.Close();



                string filename = txtEmpIDName.Text + ".pdf";

                document.Close();
                Response.Clear();
                Response.ContentType = "application/pdf";
                Response.AddHeader("Content-Disposition", "attachment; filename=\"" + filename + "\"");
                Response.ContentType = "application/pdf";
                Response.Buffer = true;
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.BinaryWrite(bytes);
                Response.End();

            }


        }
    }
}