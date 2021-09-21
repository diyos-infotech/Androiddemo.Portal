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
using System.Data.SqlClient;
using Jawan.Portal.DAL;
namespace Jawan.Portal
{
    public partial class getdata : System.Web.UI.Page
    {
        AppConfiguration config = new AppConfiguration();
        GridViewExportUtil gve = new GridViewExportUtil();
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnget_Click(object sender, EventArgs e)
        {
            int status = 0;
            SqlConnection NewKLTSCon = new SqlConnection(ConfigurationManager.ConnectionStrings["KLTSConnectionString"].ToString());
            SqlConnection OldKLTSCon = new SqlConnection(ConfigurationManager.ConnectionStrings["KLTS1ConnectionString"].ToString());
            SqlDataAdapter NewCmd = new SqlDataAdapter("Select * from " + txttname.Text.Trim(), NewKLTSCon);
            SqlDataAdapter OldCmd = new SqlDataAdapter("Select * from " + txttname.Text.Trim(), OldKLTSCon);
            DataSet newds = new DataSet();
            DataSet oldds = new DataSet();
            OldCmd.Fill(oldds, "old");
            // NewCmd.Fill(newds,"new");

            //column Names
            string allcolname = "";

            string[] Colname = new string[oldds.Tables[0].Columns.Count];
            for (int i = 0; i < oldds.Tables[0].Columns.Count; i++)
            {
                Colname[i] = oldds.Tables[0].Columns[i].ToString();
                if (i != oldds.Tables[0].Columns.Count - 1)
                {
                    allcolname += Colname[i] + ",";
                }
                else
                {
                    allcolname += Colname[i];
                }
            }
            for (int rowindex = 0; rowindex < oldds.Tables[0].Rows.Count; rowindex++)
            {

                string Allcolumnvalues = "";
                string[] colmnvalue = new string[oldds.Tables[0].Columns.Count];
                for (int colindex = 0; colindex < oldds.Tables[0].Columns.Count; colindex++)
                {
                    colmnvalue[colindex] = oldds.Tables[0].Rows[rowindex][colindex].ToString();
                    if (colindex != oldds.Tables[0].Columns.Count - 1)
                    {
                        Allcolumnvalues = Allcolumnvalues + "'" + colmnvalue[colindex] + "',";

                    }
                    else
                    {
                        Allcolumnvalues = Allcolumnvalues + "'" + colmnvalue[colindex] + "'";
                    }
                }

                if (txttname.Text.Trim() == "empbiodata")
                {
                    txttname.Text = "EmpDetails";
                }
                string Sqlqry = "insert into " + txttname.Text.Trim() + "(" + allcolname + ") values(" + Allcolumnvalues + ")";
                SqlCommand cmd = new SqlCommand(Sqlqry, NewKLTSCon);
                NewKLTSCon.Open();
                cmd.ExecuteNonQuery();
                status++;
                NewKLTSCon.Close();
            }
            if (status == oldds.Tables[0].Rows.Count)
            {
                Response.Write("Data Imported Successfully");
            }
            else
            {
                Response.Write("Data Not Imported ");

            }






            //int colindex =0;
            //string Value1 = oldds.Tables[0].Rows[Rowindex][0].ToString();
            //if (colindex == 0  )
            //{
            //    string colname = oldds.Tables[0].Columns[colindex].ToString();
            //    string Sqlqry = "insert into " + txttname.Text.Trim() + "(" + colname + ") values('" + Value1 + "')";
            //    SqlCommand cmd = new SqlCommand(Sqlqry,NewKLTSCon);
            //    NewKLTSCon.Open();
            //    cmd.ExecuteNonQuery();
            //}
            //else
            //{
            //     for (colindex = 1; colindex<oldds.Tables[0].Columns.Count; colindex++)
            //    {
            //        string colname = oldds.Tables[0].Columns[colindex].ToString();
            //        string Value2 = oldds.Tables[0].Rows[Rowindex][colindex].ToString();
            //        string Sqlqry = "update " + txttname.Text.Trim() + " set " + colname + "='" + Value2 + "'";
            //        SqlCommand cmd = new SqlCommand(Sqlqry, NewKLTSCon);
            //        NewKLTSCon.Open();
            //         cmd.ExecuteNonQuery();
            //    }

            //}






        }
    }
}