using System;
using KLTS.Data;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;


/// <summary>
/// Summary description for Marketinghelper
/// </summary>
public class Marketinghelper
{
    string centralConnectionString = ConfigurationManager.ConnectionStrings["centralConnectionString"].ConnectionString;
    string connectionString = ConfigurationManager.ConnectionStrings["KLTSConnectionString"].ConnectionString;

    public Marketinghelper()
    {

    }

    public string LoadMaxleadid(string Clientidprefix)
    {
        string SqlqryForCId = "select max(right(LeadId,4)) as LeadId From M_Leads   where LeadId like '" + Clientidprefix + "%'";
        System.Data.DataTable dtCId = SqlHelper.Instance.GetTableByQuery(SqlqryForCId);
        int CId = 0001;
        string ClientPrefix = string.Empty;
        if (dtCId.Rows.Count > 0)
        {
            if (String.IsNullOrEmpty(dtCId.Rows[0]["LeadId"].ToString()) == false)
            {
                CId = int.Parse(dtCId.Rows[0]["LeadId"].ToString()) + 1;
            }
        }

        return "LE" + (CId).ToString("0000");
    }

    public DataTable LoadLeadids()
    {
        string ProcedureName = "GetLeadIDandNames";
        System.Data.DataTable DtLeadid = SqlHelper.Instance.ExecuteSPWithoutParams(ProcedureName);
        return DtLeadid;
    }


    public DataTable LoadFinancialYears()
    {

        string ProcedureName = "FinancialYears";
        System.Data.DataTable DtHSNNos = SqlHelper.Instance.ExecuteSPWithoutParams(ProcedureName);
        return DtHSNNos;
    }


    public DataTable CentralExecuteAdaptorAsyncWithQueryParams(string query)
    {
        try
        {
            using (SqlConnection con = new System.Data.SqlClient.SqlConnection(centralConnectionString))
            {
                DataTable _table = new DataTable();
                SqlCommand command = new SqlCommand();
                command.Connection = con;
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                command = new SqlCommand(query, con);
                command.CommandTimeout = 0;
                command.CommandType = CommandType.Text;
                SqlDataAdapter adaptor = new SqlDataAdapter(command);
                adaptor.Fill(_table);
                return _table;
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    public DataTable CentralExecuteAdaptorAsyncWithSPParams(string SPName, Hashtable ht)
    {
        try
        {
            using (SqlConnection con = new System.Data.SqlClient.SqlConnection(centralConnectionString))
            {
                DataTable _table = new DataTable();
                SqlCommand command = new SqlCommand();
                command.Connection = con;
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                command = new SqlCommand(SPName, con);
                command.CommandTimeout = 0;
                command.CommandType = CommandType.StoredProcedure;
                foreach (DictionaryEntry de in ht)
                {
                    command.Parameters.AddWithValue(de.Key.ToString(), de.Value);
                }
                SqlDataAdapter adaptor = new SqlDataAdapter(command);
                adaptor.Fill(_table);
                return _table;
            }
        }
        catch (Exception)
        {
            throw;
        }
    }


    public int ExecuteNonQueryAsyncWithSPParams(string SPName, Hashtable ht)
    {
        try
        {
            int result = 0;
            DataTable _table = new DataTable();
            using (SqlConnection con = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = con;
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                command.CommandTimeout = 80;
                command = new SqlCommand(SPName, con);
                command.CommandType = CommandType.StoredProcedure;
                foreach (DictionaryEntry de in ht)
                {
                    command.Parameters.AddWithValue(de.Key.ToString(), de.Value);
                }

                result = command.ExecuteNonQuery();
            }
            return result;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public DataTable ExecuteAdaptorAsyncWithQueryParams(string query)
    {
        try
        {
            using (SqlConnection con = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                DataTable _table = new DataTable();
                SqlCommand command = new SqlCommand();
                command.Connection = con;
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                command = new SqlCommand(query, con);
                command.CommandTimeout = 80;
                command.CommandType = CommandType.Text;
                SqlDataAdapter adaptor = new SqlDataAdapter(command);
                adaptor.Fill(_table);
                return _table;
            }
        }
        catch (Exception)
        {
            throw;
        }
    }






}