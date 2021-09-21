using System;
using System.Collections;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Data.OleDb;
using Jawan.Portal.DAL;
using System.IO;

namespace Jawan.Portal
{
    public partial class ImportRoster : System.Web.UI.Page
    {

        AppConfiguration config = new AppConfiguration();
        GridViewExportUtil gve = new GridViewExportUtil();
        string Username = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            GetWebConfigdata();

            if (!IsPostBack)
            {
                if (Session["UserId"] != null )
                {
                    lblDisplayUser.Text = Session["UserId"].ToString();
                }
            }
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            string SPName = "ImportEmpRoster";
            Hashtable ht = new Hashtable();
            ht.Add("@Type", "SampleSheet");
            DataTable dt = config.ExecuteAdaptorAsyncWithParams(SPName, ht).Result;
            if (dt.Rows.Count > 0)
            {
                gve.NewExportExcel("SampleAttendance.xlsx", dt);
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

        public void chknotinsert()
        {
            string qry = "select clientid as 'Client ID',empid as 'Emp ID',Remarks from NotInsert";
            DataTable dt = config.ExecuteAdaptorAsyncWithQueryParams(qry).Result;
            if (dt.Rows.Count > 0)
            {
                btnNotInsert.Visible = true;
                GVAttendanceData.DataSource = dt;
                GVAttendanceData.DataBind();
            }
            else
            {
                btnNotInsert.Visible = false;
                GVAttendanceData.DataSource = null;
                GVAttendanceData.DataBind();
            }
        }


        
        protected void GetWebConfigdata()
        {
            Username = Session["UserId"].ToString();
        }

        protected void btnImport_Click(object sender, EventArgs e)
        {
            GvEmpList.DataSource = null;
            GvEmpList.DataBind();


            if (fileupload.PostedFile != null)
            {
                try
                {

                    if (txtMonth.Text.Trim().Length == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showlalert", "alert('Please Select Month');", true);
                        return;
                    }

                    string month = Getmonthval();

                    string qry = "delete from notinsert";
                    int qrystatus = config.ExecuteNonQueryWithQueryAsync(qry).Result;

                    string excelPath = Server.MapPath("~/ImportDocuments/") + Path.GetFileName(fileupload.PostedFile.FileName);
                    fileupload.SaveAs(excelPath);

                    string conString = string.Empty;
                    string extension = Path.GetExtension(fileupload.PostedFile.FileName);
                    switch (extension)
                    {
                        case ".xls": //Excel 97-03
                            conString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties='Excel 8.0;HDR=YES'";

                            break;
                        case ".xlsx": //Excel 07 or higher
                            conString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 8.0;HDR=YES'";
                            break;

                    }
                    conString = string.Format(conString, excelPath);
                    using (OleDbConnection excel_con = new OleDbConnection(conString))
                    {
                        excel_con.Open();
                        string sheet1 = excel_con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null).Rows[0]["TABLE_NAME"].ToString();
                        DataTable dtExcelData = new DataTable();


                        using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT * FROM [" + sheet1 + "] where len([Emp ID])>0", excel_con))
                        {
                            oda.Fill(dtExcelData);
                        }
                        excel_con.Close();

                        int status = 0;

                        int Excel_No = 0;
                        string selectquery = "select max(cast(Excel_No as int )) as Id from emproster ";
                        DataTable dtExcelID = config.ExecuteAdaptorAsyncWithQueryParams(selectquery).Result;

                        if (dtExcelID.Rows.Count > 0)
                        {
                            if (String.IsNullOrEmpty(dtExcelID.Rows[0]["Id"].ToString()) == false)
                            {
                                Excel_No = Convert.ToInt32(dtExcelID.Rows[0]["Id"].ToString()) + 1;
                            }
                            else
                            {
                                Excel_No = int.Parse("1");
                            }
                        }



                        for (int i = 0; i < dtExcelData.Rows.Count; i++)
                        {

                            string day1, day2, day3, day4, day5, day6, day7, day8, day9, day10, day11, day12, day13,
                                day14, day15, day16, day17, day18, day19, day20, day21, day22, day23, day24,
                                day25, day26, day27, day28, day29, day30, day31;

                            day1 = day2 = day3 = day4 = day5 = day6 = day7 = day8 = day9 = day10 = day11 = day12 = day13 =
                                day14 = day15 = day16 = day17 = day18 = day19 = day20 = day21 = day22 = day23 = day24 =
                                day25 = day26 = day27 = day28 = day29 = day30 = day31 = "";

                            string Clientid = dtExcelData.Rows[i]["Client ID"].ToString().Trim();
                            string EmpID = dtExcelData.Rows[i]["Emp ID"].ToString().Trim();
                            string Design = dtExcelData.Rows[i]["Design"].ToString().Trim();
                            day1 = dtExcelData.Rows[i]["1"].ToString().Trim();
                            day2 = dtExcelData.Rows[i]["2"].ToString().Trim();
                            day3 = dtExcelData.Rows[i]["3"].ToString().Trim();
                            day4 = dtExcelData.Rows[i]["4"].ToString().Trim();
                            day5 = dtExcelData.Rows[i]["5"].ToString().Trim();
                            day6 = dtExcelData.Rows[i]["6"].ToString().Trim();
                            day7 = dtExcelData.Rows[i]["7"].ToString().Trim();
                            day8 = dtExcelData.Rows[i]["8"].ToString().Trim();
                            day9 = dtExcelData.Rows[i]["9"].ToString().Trim();
                            day10 = dtExcelData.Rows[i]["10"].ToString().Trim();
                            day11 = dtExcelData.Rows[i]["11"].ToString().Trim();
                            day12 = dtExcelData.Rows[i]["12"].ToString().Trim();
                            day13 = dtExcelData.Rows[i]["13"].ToString().Trim();
                            day14 = dtExcelData.Rows[i]["14"].ToString().Trim();
                            day15 = dtExcelData.Rows[i]["15"].ToString().Trim();
                            day16 = dtExcelData.Rows[i]["16"].ToString().Trim();
                            day17 = dtExcelData.Rows[i]["17"].ToString().Trim();
                            day18 = dtExcelData.Rows[i]["18"].ToString().Trim();
                            day19 = dtExcelData.Rows[i]["19"].ToString().Trim();
                            day20 = dtExcelData.Rows[i]["20"].ToString().Trim();
                            day21 = dtExcelData.Rows[i]["21"].ToString().Trim();
                            day22 = dtExcelData.Rows[i]["22"].ToString().Trim();
                            day23 = dtExcelData.Rows[i]["23"].ToString().Trim();
                            day24 = dtExcelData.Rows[i]["24"].ToString().Trim();
                            day25 = dtExcelData.Rows[i]["25"].ToString().Trim();
                            day26 = dtExcelData.Rows[i]["26"].ToString().Trim();
                            day27 = dtExcelData.Rows[i]["27"].ToString().Trim();
                            day28 = dtExcelData.Rows[i]["28"].ToString().Trim();
                            day29 = dtExcelData.Rows[i]["29"].ToString().Trim();
                            day30 = dtExcelData.Rows[i]["30"].ToString().Trim();
                            day31 = dtExcelData.Rows[i]["31"].ToString().Trim();



                            string SPName = "ImportEmpRoster";
                            Hashtable ht = new Hashtable();
                            ht.Add("@Type", "InsertRoster");
                            ht.Add("@EmpID", EmpID);
                            ht.Add("@Clientid", Clientid);
                            ht.Add("@Design", Design);
                            ht.Add("@month", month);
                            ht.Add("@day1", day1);
                            ht.Add("@day2", day2);
                            ht.Add("@day3", day3);
                            ht.Add("@day4", day4);
                            ht.Add("@day5", day5);
                            ht.Add("@day6", day6);
                            ht.Add("@day7", day7);
                            ht.Add("@day8", day8);
                            ht.Add("@day9", day9);
                            ht.Add("@day10", day10);
                            ht.Add("@day11", day11);
                            ht.Add("@day12", day12);
                            ht.Add("@day13", day13);
                            ht.Add("@day14", day14);
                            ht.Add("@day15", day15);
                            ht.Add("@day16", day16);
                            ht.Add("@day17", day17);
                            ht.Add("@day18", day18);
                            ht.Add("@day19", day19);
                            ht.Add("@day20", day20);
                            ht.Add("@day21", day21);
                            ht.Add("@day22", day22);
                            ht.Add("@day23", day23);
                            ht.Add("@day24", day24);
                            ht.Add("@day25", day25);
                            ht.Add("@day26", day26);
                            ht.Add("@day27", day27);
                            ht.Add("@day28", day28);
                            ht.Add("@day29", day29);
                            ht.Add("@day30", day30);
                            ht.Add("@day31", day31);
                            ht.Add("@Excel_No", Excel_No);
                            ht.Add("@Created_By", Username);


                            status = config.ExecuteNonQueryParamsAsync(SPName, ht).Result;

                        }

                        if (status > 0)
                        {

                            chknotinsert();

                            string SPName = "ImportEmpRoster";
                            Hashtable ht = new Hashtable();
                            ht.Add("@Type", "GetRosterbyExcelNo");
                            ht.Add("@month", month);
                            ht.Add("@Excel_No", Excel_No);

                            DataTable dtstatus = config.ExecuteAdaptorAsyncWithParams(SPName, ht).Result;

                            if (dtstatus.Rows.Count > 0)
                            {
                                GvEmpList.DataSource = dtstatus;
                                GvEmpList.DataBind();

                            }

                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Details are not uploaded.')", true);
                            chknotinsert();

                        }
                    }
                }
                catch (Exception ex)
                {
                    string msg = ex.ToString();
                    Response.Write("Error: " + ex.Message);
                }
            }
        }

        protected void btnNotInsert_Click(object sender, EventArgs e)
        {
            gve.NewExport("NotImportedData.xlsx", this.GVAttendanceData);
        }

        protected void lnkDownloadRoster_Click(object sender, EventArgs e)
        {
            if (txtMonth.Text.Trim().Length == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showlalert", "alert('Please Select Month');", true);
                return;
            }

            string month = Getmonthval();

            string SPName = "ImportEmpRoster";
            Hashtable ht = new Hashtable();
            ht.Add("@Type", "GetRoster");
            ht.Add("@month", month);

            DataTable dtstatus = config.ExecuteAdaptorAsyncWithParams(SPName, ht).Result;

            if (dtstatus.Rows.Count > 0)
            {
                gve.NewExportExcel(txtMonth.Text+"-Roster.xlsx", dtstatus);

            }
        }
    }
}