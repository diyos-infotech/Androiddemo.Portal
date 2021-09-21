using System;
using System.Collections;
using KLTS.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using Jawan.Portal.DAL;
namespace Jawan.Portal
{
    public partial class M_Add_Lead_Requirement : System.Web.UI.Page
    {
        string Username = "";
        AppConfiguration config = new AppConfiguration();
        Marketinghelper MH = new Marketinghelper();
        string constr = ConfigurationManager.ConnectionStrings["KLTSConnectionString"].ConnectionString;

        
        protected void Page_Load(object sender, EventArgs e)
        {
            Username = Session["UserId"].ToString();

            if (!IsPostBack)
            {
                LoadDesignationNames();
                LoadLeadId();
                LoadLeadName();
                LoadStatenames();
                LoadZones();
                LoadTypes();
                LoadCategories();
                Loadcomponenets();
                BindGrid();

                if (Request.QueryString["LeadID"] != null)
                {
                    string Leadid = Request.QueryString["LeadID"].ToString();
                    DropLeadID.SelectedValue = (Leadid);
                    DropLeadID_SelectedIndexChanged(sender, e);
                }
            }
        }

        private void BindGrid()
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SELECT Id, Name,data,contenttype FROM M_Tblfiles";
                    cmd.Connection = con;
                    con.Open();
                    GridView1.DataSource = cmd.ExecuteReader();
                    GridView1.DataBind();
                    con.Close();
                }
            }

        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }

        protected void LoadLeadId()
        {
            DataTable DtLeadid = MH.LoadLeadids();
            if (DtLeadid.Rows.Count > 0)
            {
                DropLeadID.DataValueField = "LeadID";
                DropLeadID.DataTextField = "LeadID";
                DropLeadID.DataSource = DtLeadid;
                DropLeadID.DataBind();
            }
            DropLeadID.Items.Insert(0, "-Select-");

        }

        protected void LoadLeadName()
        {
            DataTable DtLeadname = MH.LoadLeadids();
            if (DtLeadname.Rows.Count > 0)
            {
                DropLeadName.DataValueField = "LeadID";
                DropLeadName.DataTextField = "LeadName";
                DropLeadName.DataSource = DtLeadname;
                DropLeadName.DataBind();
            }
            DropLeadName.Items.Insert(0, "-Select-");

        }

        protected void DropLeadID_SelectedIndexChanged(object sender, EventArgs e)
        {
            GVLeadsamount.DataSource = null;
            GVLeadsamount.DataBind();

            if (DropLeadID.SelectedIndex > 0)
            {

                DropLeadName.SelectedValue = DropLeadID.SelectedValue;
                GetGridAmounts();
            }
            else
            {
                DropLeadName.SelectedIndex = 0;
            }

        }

        protected void DropLeadName_SelectedIndexChanged(object sender, EventArgs e)
        {
            GVLeadsamount.DataSource = null;
            GVLeadsamount.DataBind();
            if (DropLeadName.SelectedIndex > 0)
            {
                DropLeadID.SelectedValue = DropLeadName.SelectedValue;
                GetGridAmounts();
            }
            else
            {
                DropLeadID.SelectedIndex = 0;
            }
        }

        protected void GetGridAmounts()
        {

            GVLeadsamount.DataSource = null;
            GVLeadsamount.DataBind();

            string spleadbyid = "M_GetLeadsIdRequirementAmounts";
            Hashtable hsId = new Hashtable();
            hsId.Add("@leadid", DropLeadID.SelectedValue);
            DataTable dt = SqlHelper.Instance.ExecuteStoredProcedureWithParams(spleadbyid, hsId);

            if (dt.Rows.Count > 0)
            {
                GVLeadsamount.DataSource = dt;
                GVLeadsamount.DataBind();

                float totalBasic = 0;
                float totalDA = 0;
                float totalHRA = 0;
                float totalCCA = 0;
                float totalla = 0;
                float totalConveyance = 0;
                float totalWA = 0;
                float totalOA = 0;
                float totalPF = 0;
                float totalESI = 0;
                float totalGratuity = 0;
                float totalBonus = 0;
                float totalnfhs = 0;
                float totalRC = 0;
                float totalfoodallowance = 0;
                float totaltravelallowance = 0;
                float totalperfomallowance = 0;
                float totalmobileallowance = 0;
                float totalSGSTamount = 0;
                float totalIGSTamount = 0;
                float totalCGSTamount = 0;
                float totalservicecharge = 0;
                float totalGrandTotal = 0;
                float totalamount = 0;

                for (int i = 0; i < dt.Rows.Count; i++)
                {


                    string Total = dt.Rows[i]["TotalAmount"].ToString();
                    if (Total.Trim().Length > 0)
                    {
                        totalamount += Convert.ToSingle(Total);
                    }

                    string servicecharge = dt.Rows[i]["servicecharge"].ToString();
                    if (servicecharge.Trim().Length > 0)
                    {
                        totalservicecharge += Convert.ToSingle(servicecharge);
                    }

                    string CGSTamount = dt.Rows[i]["CGSTamount"].ToString();
                    if (CGSTamount.Trim().Length > 0)
                    {
                        totalCGSTamount += Convert.ToSingle(CGSTamount);
                    }

                    string SGSTamount = dt.Rows[i]["SGSTamount"].ToString();
                    if (SGSTamount.Trim().Length > 0)
                    {
                        totalSGSTamount += Convert.ToSingle(SGSTamount);
                    }

                    string IGSTamount = dt.Rows[i]["IGSTamount"].ToString();
                    if (IGSTamount.Trim().Length > 0)
                    {
                        totalIGSTamount += Convert.ToSingle(IGSTamount);
                    }

                    string GrandTotal = dt.Rows[i]["GrandTotal"].ToString();
                    if (GrandTotal.Trim().Length > 0)
                    {
                        totalGrandTotal += Convert.ToSingle(GrandTotal);
                    }

                    string strBasic = dt.Rows[i]["Basic"].ToString();
                    if (strBasic.Trim().Length > 0)
                    {
                        totalBasic += Convert.ToSingle(strBasic);
                    }

                    string strDA = dt.Rows[i]["DA"].ToString();
                    if (strDA.Trim().Length > 0)
                    {
                        totalDA += Convert.ToSingle(strDA);
                    }

                    string strhHRA = dt.Rows[i]["HRA"].ToString();
                    if (strhHRA.Trim().Length > 0)
                    {
                        totalHRA += Convert.ToSingle(strhHRA);
                    }


                    string strConveyance = dt.Rows[i]["Conveyence"].ToString();
                    if (strConveyance.Trim().Length > 0)
                    {
                        totalConveyance += Convert.ToSingle(strConveyance);
                    }

                    string strCCA = dt.Rows[i]["CCA"].ToString();
                    if (strCCA.Trim().Length > 0)
                    {
                        totalCCA += Convert.ToSingle(strCCA);
                    }

                    string LAE = dt.Rows[i]["LA"].ToString();
                    if (LAE.Trim().Length > 0)
                    {
                        totalla += Convert.ToSingle(LAE);
                    }

                    string strGratuity = dt.Rows[i]["Gratuity"].ToString();
                    if (strGratuity.Trim().Length > 0)
                    {
                        totalGratuity += Convert.ToSingle(strGratuity);
                    }

                    string strBonus = dt.Rows[i]["Bonus"].ToString();
                    if (strBonus.Trim().Length > 0)
                    {
                        totalBonus += Convert.ToSingle(strBonus);
                    }

                    string strWA = dt.Rows[i]["WA"].ToString();
                    if (strWA.Trim().Length > 0)
                    {
                        totalWA += Convert.ToSingle(strWA);
                    }

                    string strOA = dt.Rows[i]["OA"].ToString();
                    if (strOA.Trim().Length > 0)
                    {
                        totalOA += Convert.ToSingle(strOA);
                    }

                    string strNfhs = dt.Rows[i]["Nfhs"].ToString();
                    if (strNfhs.Trim().Length > 0)
                    {
                        totalnfhs += Convert.ToSingle(strNfhs);
                    }

                    string RCC = dt.Rows[i]["RC"].ToString();
                    if (RCC.Trim().Length > 0)
                    {
                        totalRC += Convert.ToSingle(RCC);
                    }

                    string FoodAllowance = dt.Rows[i]["FoodAllowance"].ToString();
                    if (FoodAllowance.Trim().Length > 0)
                    {
                        totalfoodallowance += Convert.ToSingle(FoodAllowance);
                    }

                    string TravelAllowance = dt.Rows[i]["TravelAllowance"].ToString();
                    if (TravelAllowance.Trim().Length > 0)
                    {
                        totaltravelallowance += Convert.ToSingle(TravelAllowance);
                    }

                    string PerformanceAllowance = dt.Rows[i]["PerformanceAllowance"].ToString();
                    if (TravelAllowance.Trim().Length > 0)
                    {
                        totalperfomallowance += Convert.ToSingle(PerformanceAllowance);
                    }

                    string MoboleAllowance = dt.Rows[i]["MoboleAllowance"].ToString();
                    if (TravelAllowance.Trim().Length > 0)
                    {
                        totalmobileallowance += Convert.ToSingle(MoboleAllowance);
                    }

                    string PFEmplr = dt.Rows[i]["pftotalamount"].ToString();
                    if (PFEmplr.Trim().Length > 0)
                    {
                        totalPF += Convert.ToSingle(PFEmplr);
                    }

                    string strESI = dt.Rows[i]["esitotalamount"].ToString();
                    if (strESI.Trim().Length > 0)
                    {
                        totalESI += Convert.ToSingle(strESI);
                    }
                }



                //6
                Label lblTotalAmount = GVLeadsamount.FooterRow.FindControl("lblTotalAmount") as Label;
                lblTotalAmount.Text = Math.Round(totalamount).ToString();
                if (totalamount > 0)
                {
                    GVLeadsamount.Columns[6].Visible = true;
                }
                else
                {
                    GVLeadsamount.Columns[6].Visible = false;

                }
                //7
                Label lblschargeamount = GVLeadsamount.FooterRow.FindControl("lblschargeamount") as Label;
                lblschargeamount.Text = Math.Round(totalservicecharge).ToString();

                if (totalservicecharge > 0)
                {
                    GVLeadsamount.Columns[7].Visible = true;
                }
                else
                {
                    GVLeadsamount.Columns[7].Visible = false;
                }
                //8
                Label lblcgstamunt = GVLeadsamount.FooterRow.FindControl("lblcgstamunt") as Label;
                lblcgstamunt.Text = Math.Round(totalCGSTamount).ToString();

                if (totalCGSTamount > 0)
                {
                    GVLeadsamount.Columns[8].Visible = true;
                }
                else
                {
                    GVLeadsamount.Columns[8].Visible = false;
                }


                //9
                Label lblsgstamunt = GVLeadsamount.FooterRow.FindControl("lblsgstamunt") as Label;
                lblsgstamunt.Text = Math.Round(totalSGSTamount).ToString();

                if (totalSGSTamount > 0)
                {
                    GVLeadsamount.Columns[9].Visible = true;
                }
                else
                {
                    GVLeadsamount.Columns[9].Visible = false;
                }

                Label lbligstamunt = GVLeadsamount.FooterRow.FindControl("lbligstamunt") as Label;
                lbligstamunt.Text = Math.Round(totalIGSTamount).ToString();

                if (totalIGSTamount > 0)
                {
                    GVLeadsamount.Columns[10].Visible = true;
                }
                else
                {
                    GVLeadsamount.Columns[10].Visible = false;
                }

                //11
                Label lblgrandtotalamount = GVLeadsamount.FooterRow.FindControl("lblgrandtotalamount") as Label;
                lblgrandtotalamount.Text = Math.Round(totalGrandTotal).ToString();

                if (totalGrandTotal > 0)
                {
                    GVLeadsamount.Columns[11].Visible = true;
                }
                else
                {
                    GVLeadsamount.Columns[11].Visible = false;
                }
                //12
                Label lblbasictotal = GVLeadsamount.FooterRow.FindControl("lblbasictotal") as Label;
                lblbasictotal.Text = Math.Round(totalBasic).ToString();

                if (totalBasic > 0)
                {
                    GVLeadsamount.Columns[12].Visible = true;
                }
                else
                {
                    GVLeadsamount.Columns[12].Visible = false;
                }

                //13
                Label lbldatotal = GVLeadsamount.FooterRow.FindControl("lbldatotal") as Label;
                lbldatotal.Text = Math.Round(totalDA).ToString();

                if (totalDA > 0)
                {
                    GVLeadsamount.Columns[13].Visible = true;
                }
                else
                {
                    GVLeadsamount.Columns[13].Visible = false;
                }
                //14
                Label lblhratotal = GVLeadsamount.FooterRow.FindControl("lblhratotal") as Label;
                lblhratotal.Text = Math.Round(totalHRA).ToString();

                if (totalHRA > 0)
                {
                    GVLeadsamount.Columns[14].Visible = true;
                }
                else
                {
                    GVLeadsamount.Columns[14].Visible = false;
                }
                //15
                Label lblconvtotal = GVLeadsamount.FooterRow.FindControl("lblconvtotal") as Label;
                lblconvtotal.Text = Math.Round(totalConveyance).ToString();

                if (totalConveyance > 0)
                {
                    GVLeadsamount.Columns[15].Visible = true;
                }
                else
                {
                    GVLeadsamount.Columns[15].Visible = false;
                }
                //16
                Label lblccatotal = GVLeadsamount.FooterRow.FindControl("lblccatotal") as Label;
                lblccatotal.Text = Math.Round(totalCCA).ToString();

                if (totalCCA > 0)
                {
                    GVLeadsamount.Columns[16].Visible = true;
                }
                else
                {
                    GVLeadsamount.Columns[16].Visible = false;
                }

                //17
                Label lbllatotal = GVLeadsamount.FooterRow.FindControl("lbllatotal") as Label;
                lbllatotal.Text = Math.Round(totalla).ToString();

                if (totalla > 0)
                {
                    GVLeadsamount.Columns[17].Visible = true;
                }
                else
                {
                    GVLeadsamount.Columns[17].Visible = false;
                }
                //18
                Label lblgratuitytotal = GVLeadsamount.FooterRow.FindControl("lblgratuitytotal") as Label;
                lblgratuitytotal.Text = Math.Round(totalGratuity).ToString();

                if (totalGratuity > 0)
                {
                    GVLeadsamount.Columns[18].Visible = true;
                }
                else
                {
                    GVLeadsamount.Columns[18].Visible = false;
                }
                //19
                Label lblbonustotal = GVLeadsamount.FooterRow.FindControl("lblbonustotal") as Label;
                lblbonustotal.Text = Math.Round(totalBonus).ToString();

                if (totalBonus > 0)
                {
                    GVLeadsamount.Columns[19].Visible = true;
                }
                else
                {
                    GVLeadsamount.Columns[19].Visible = false;
                }
                //20
                Label lblWAtotal = GVLeadsamount.FooterRow.FindControl("lblWAtotal") as Label;
                lblWAtotal.Text = Math.Round(totalWA).ToString();

                if (totalWA > 0)
                {
                    GVLeadsamount.Columns[20].Visible = true;
                }
                else
                {
                    GVLeadsamount.Columns[20].Visible = false;
                }
                //21
                Label lbloatotal = GVLeadsamount.FooterRow.FindControl("lbloatotal") as Label;
                lbloatotal.Text = Math.Round(totalOA).ToString();
                if (totalOA > 0)
                {
                    GVLeadsamount.Columns[21].Visible = true;
                }
                else
                {
                    GVLeadsamount.Columns[21].Visible = false;
                }

                //22
                Label lblnfhstotal = GVLeadsamount.FooterRow.FindControl("lblnfhstotal") as Label;
                lblnfhstotal.Text = Math.Round(totalnfhs).ToString();

                if (totalnfhs > 0)
                {
                    GVLeadsamount.Columns[22].Visible = true;
                }
                else
                {
                    GVLeadsamount.Columns[22].Visible = false;
                }

                //23
                Label lblrctotal = GVLeadsamount.FooterRow.FindControl("lblrctotal") as Label;
                lblrctotal.Text = Math.Round(totalRC).ToString();

                if (totalRC > 0)
                {
                    GVLeadsamount.Columns[23].Visible = true;
                }
                else
                {
                    GVLeadsamount.Columns[23].Visible = false;
                }

                //24
                Label lblfawtotal = GVLeadsamount.FooterRow.FindControl("lblfawtotal") as Label;
                lblfawtotal.Text = Math.Round(totalfoodallowance).ToString();

                if (totalfoodallowance > 0)
                {
                    GVLeadsamount.Columns[24].Visible = true;
                }
                else
                {
                    GVLeadsamount.Columns[24].Visible = false;
                }
                //25
                Label lbtatotal = GVLeadsamount.FooterRow.FindControl("lbtatotal") as Label;
                lbtatotal.Text = Math.Round(totaltravelallowance).ToString();

                if (totaltravelallowance > 0)
                {
                    GVLeadsamount.Columns[25].Visible = true;
                }
                else
                {
                    GVLeadsamount.Columns[25].Visible = false;
                }
                //26
                Label lblpfatotal = GVLeadsamount.FooterRow.FindControl("lblpfatotal") as Label;
                lblpfatotal.Text = Math.Round(totalperfomallowance).ToString();

                if (totalperfomallowance > 0)
                {
                    GVLeadsamount.Columns[26].Visible = true;
                }
                else
                {
                    GVLeadsamount.Columns[26].Visible = false;
                }
                //27
                Label lblmatotal = GVLeadsamount.FooterRow.FindControl("lblmatotal") as Label;
                lblmatotal.Text = Math.Round(totalmobileallowance).ToString();
                if (totalmobileallowance > 0)
                {
                    GVLeadsamount.Columns[27].Visible = true;
                }
                else
                {
                    GVLeadsamount.Columns[27].Visible = false;
                }
                //28
                Label lblpftotal = GVLeadsamount.FooterRow.FindControl("lblpftotal") as Label;
                lblpftotal.Text = Math.Round(totalPF).ToString();
                if (totalPF > 0)
                {
                    GVLeadsamount.Columns[28].Visible = true;
                }
                else
                {
                    GVLeadsamount.Columns[28].Visible = false;
                }
                //29
                Label lblesitotal = GVLeadsamount.FooterRow.FindControl("lblesitotal") as Label;
                lblesitotal.Text = Math.Round(totalESI).ToString();
                if (totalESI > 0)
                {
                    GVLeadsamount.Columns[29].Visible = true;
                }
                else
                {
                    GVLeadsamount.Columns[29].Visible = false;
                }
            }

        }

        protected void LoadDesignationNames()
        {
            string qry = "select DesignID,Designation from Designations";
            DataTable DtDesignations = MH.CentralExecuteAdaptorAsyncWithQueryParams(qry);
            if (DtDesignations.Rows.Count > 0)
            {
                dropDesignation.DataValueField = "DesignId";
                dropDesignation.DataTextField = "Designation";
                dropDesignation.DataSource = DtDesignations;
                dropDesignation.DataBind();
            }
            dropDesignation.Items.Insert(0, "-Select-");

        }

        protected void LoadStatenames()
        {
            string qry = "select StateID,state from states";
            DataTable DtStateNames = MH.CentralExecuteAdaptorAsyncWithQueryParams(qry);
            if (DtStateNames.Rows.Count > 0)
            {
                ddlstate.DataValueField = "StateID";
                ddlstate.DataTextField = "State";
                ddlstate.DataSource = DtStateNames;
                ddlstate.DataBind();
            }
            ddlstate.Items.Insert(0, new ListItem("-Select-", "0"));
        }

        protected void LoadZones()
        {
            string qry = "select ZoneId,ZoneName from Zone";
            DataTable Dtzones = MH.CentralExecuteAdaptorAsyncWithQueryParams(qry);
            if (Dtzones.Rows.Count > 0)
            {
                dropZone.DataValueField = "ZoneId";
                dropZone.DataTextField = "ZoneName";
                dropZone.DataSource = Dtzones;
                dropZone.DataBind();
            }
            dropZone.Items.Insert(0, new ListItem("-Select-", "0"));
        }

        protected void LoadTypes()
        {
            string qry = "select typeid,typename from type";
            DataTable DtType = MH.CentralExecuteAdaptorAsyncWithQueryParams(qry);
            if (DtType.Rows.Count > 0)
            {
                dropType.DataValueField = "typeid";
                dropType.DataTextField = "typename";
                dropType.DataSource = DtType;
                dropType.DataBind();
            }
            dropType.Items.Insert(0, new ListItem("-Select-", "0"));
        }

        protected void LoadCategories()
        {
            string qry = "select categoryid,categoryname from category";
            DataTable Dtcatagory = MH.CentralExecuteAdaptorAsyncWithQueryParams(qry);
            if (Dtcatagory.Rows.Count > 0)
            {
                dropcategory.DataValueField = "categoryid";
                dropcategory.DataTextField = "categoryname";
                dropcategory.DataSource = Dtcatagory;
                dropcategory.DataBind();
            }
            dropcategory.Items.Insert(0, new ListItem("-Select-", "0"));
        }


        protected void btncalculate_Click(object sender, EventArgs e)
        {
            PfEsiCalculation();
        }

        #region for variables

        decimal Basic = 0;
        decimal DA = 0;
        decimal HRA = 0;
        decimal Conv = 0;
        decimal CCA = 0;
        decimal LA = 0;
        decimal gratuity = 0;
        decimal bonus = 0;
        decimal WA = 0;
        decimal OA = 0;
        decimal nfhs = 0;
        decimal RC = 0;
        decimal foodallowance = 0;
        decimal MedicalAllw = 0;
        decimal travelallowance = 0;
        decimal perfomanceallowance = 0;
        decimal mobileallowance = 0;
        decimal PF = 0;
        decimal ESI = 0;
        decimal result = 0;

        #endregion

        protected void btnadd_Click(object sender, EventArgs e)
        {
            try
            {


                if (DropLeadID.SelectedIndex == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Select Lead ID')", true);
                    return;
                }
                if (DropLeadName.SelectedIndex == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Select Lead Name')", true);
                    return;
                }
                if (dropDesignation.SelectedIndex == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Select Designation')", true);
                    return;
                }

                string designationquerycheck = "delete M_LeadRequirementAmounts where leadid='" + DropLeadID.SelectedValue + "' and DesignationId='" + dropDesignation.SelectedValue + "' and type='" + dropType.SelectedValue + "' and category='" + dropcategory.SelectedValue + "' and zone='" + dropZone.SelectedValue + "'  ";
                int dtdesignationquerycheck = SqlHelper.Instance.ExecuteDMLQry(designationquerycheck);

                int sno = 1;
                string query = "select sno from M_TempLeadRequirementAmounts";
                DataTable dtquery = SqlHelper.Instance.GetTableByQuery(query);
                if (dtquery.Rows.Count > 0)
                {
                    sno = int.Parse(dtquery.Rows[0]["Sno"].ToString());
                    sno = sno + 1;
                }

                #region for Amounts

                if (txtBasic.Text.Length > 0)
                {
                    Basic = decimal.Parse(txtBasic.Text);
                }

                if (txtDA.Text.Length > 0)
                {

                    DA = decimal.Parse(txtDA.Text);
                }
                if (txtHRA.Text.Length > 0)
                {
                    HRA = decimal.Parse(txtHRA.Text);
                }
                if (txtconv.Text.Length > 0)
                {
                    Conv = decimal.Parse(txtconv.Text);
                }
                if (txtCCA.Text.Length > 0)
                {
                    CCA = decimal.Parse(txtCCA.Text);
                }
                if (txtLA.Text.Length > 0)
                {
                    LA = decimal.Parse(txtLA.Text);
                }
                if (txtGratuity.Text.Length > 0)
                {
                    gratuity = decimal.Parse(txtGratuity.Text);
                }
                if (txtBonus.Text.Length > 0)
                {
                    bonus = decimal.Parse(txtBonus.Text);
                }
                if (txtWA.Text.Length > 0)
                {
                    WA = decimal.Parse(txtWA.Text);
                }
                if (txtOA.Text.Length > 0)
                {
                    OA = decimal.Parse(txtOA.Text);
                }
                if (txtNFHS.Text.Length > 0)
                {
                    nfhs = decimal.Parse(txtNFHS.Text);
                }
                if (txtRC.Text.Length > 0)
                {
                    RC = decimal.Parse(txtRC.Text);
                }
                if (txtFoodAllowance.Text.Length > 0)
                {
                    foodallowance = decimal.Parse(txtFoodAllowance.Text);
                }
                if (txtMedicalAllowance.Text.Length > 0)
                {
                    MedicalAllw = decimal.Parse(txtMedicalAllowance.Text);
                }
                if (txtTravelAllowance.Text.Length > 0)
                {
                    travelallowance = decimal.Parse(txtTravelAllowance.Text);
                }
                if (txtPerfomanceAllowance.Text.Length > 0)
                {
                    perfomanceallowance = decimal.Parse(txtPerfomanceAllowance.Text);
                }
                if (txtMobileAllowance.Text.Length > 0)
                {
                    mobileallowance = decimal.Parse(txtMobileAllowance.Text);
                }
                if (txtPF.Text.Length > 0)
                {
                    PF = decimal.Parse(txtPF.Text);
                }
                if (txtEsi.Text.Length > 0)
                {
                    ESI = decimal.Parse(txtEsi.Text);
                }
                #region Checkboxes initialisation
                var chkbaval = 0;
                var chkdaval = 0;
                var chkhraval = 0;
                var chkconvval = 0;
                var chkccaval = 0;
                var chkbonusval = 0;
                var chkgratuityval = 0;
                var chklaval = 0;
                var chknfhsval = 0;
                var chkrcval = 0;
                var chkwaval = 0;
                var chkoaval = 0;
                var chkfoodallwval = 0;
                var chkmedicalallwval = 0;
                var chktravelallwval = 0;
                var chkperfmallwval = 0;
                var chkmobileallval = 0;
                var chkpfval = 0;
                var chkesival = 0;
                var chkservicechargeval = 0;
                var chkcgstval = 0;
                var chkigstval = 0;
                var chksgstval = 0;
                var chkbapf = 0;
                var chkdapf = 0;
                var chkhrapf = 0;
                var chkconveypf = 0;
                var chkccapf = 0;
                var chkbonuspf = 0;
                var chkgratuitypf = 0;
                var chklapf = 0;
                var chknfhspf = 0;
                var chkrcpf = 0;
                var chkwapf = 0;
                var chkoapf = 0;
                var chkfoodallwpf = 0;
                var chkmedicalallwpf = 0;
                var chktravelallwpf = 0;
                var chkperfmallwpf = 0;
                var chkmobileallwpf = 0;
                var chkbaesi = 0;
                var chkdaesi = 0;
                var chkhraesi = 0;
                var chkconveyesi = 0;
                var chkccaesi = 0;
                var chkbonusesi = 0;
                var chkgratuityesi = 0;
                var chklaesi = 0;
                var chknfhsesi = 0;
                var chkrcesi = 0;
                var chkwaesi = 0;
                var chkoaesi = 0;
                var chkfoodallwesi = 0;
                var chkmedicalallwesi = 0;
                var chktravelallwesi = 0;
                var chkperfmallwesi = 0;
                var chkmobileallwesi = 0;
                var chkbasc = 0;
                var chkdasc = 0;
                var chkhrasc = 0;
                var chkconveysc = 0;
                var chkccasc = 0;
                var chkbonussc = 0;
                var chkgratuitysc = 0;
                var chklasc = 0;
                var chknfhssc = 0;
                var chkrcsc = 0;
                var chkwasc = 0;
                var chkoasc = 0;
                var chkfoodallwsc = 0;
                var chkmedicalallwsc = 0;
                var chktravelallwsc = 0;
                var chkperfmallwsc = 0;
                var chkmobileallwsc = 0;
                #endregion checkboxes initialisation

                #region Code for Checkboxes
                if (chkba.Checked)
                {
                    chkbaval = 1;
                }
                if (chkda.Checked)
                {
                    chkdaval = 1;
                }
                if (chkhra.Checked)
                {
                    chkhraval = 1;
                }
                if (chkconv.Checked)
                {
                    chkconvval = 1;

                }
                if (chkcca.Checked)
                {
                    chkccaval = 1;
                }
                if (chkbonus.Checked)
                {
                    chkbonusval = 1;
                }

                if (chkgratuity.Checked)
                {
                    chkgratuityval = 1;
                }
                if (chkla.Checked)
                {
                    chklaval = 1;
                }
                if (chknfhs.Checked)
                {
                    chknfhsval = 1;

                }
                if (chkrc.Checked)
                {
                    chkrcval = 1;
                }
                if (chkwa.Checked)
                {
                    chkwaval = 1;
                }
                if (chkoa.Checked)
                {
                    chkoaval = 1;
                }
                if (chkfoodallw.Checked)
                {
                    chkfoodallwval = 1;
                }
                if (chkmedicalaalw.Checked)
                {
                    chkmedicalallwval = 1;

                }
                if (chktravelallw.Checked)
                {
                    chktravelallwval = 1;
                }
                if (chkperfmallw.Checked)
                {
                    chkperfmallwval = 1;
                }
                if (chkmobileallw.Checked)
                {
                    chkmobileallval = 1;
                }
                if (chkpf.Checked)
                {
                    chkpfval = 1;

                }
                if (chkesi.Checked)
                {
                    chkesival = 1;
                }
                if (chkservicecharge.Checked)
                {
                    chkservicechargeval = 1;
                }
                if (chkcgst.Checked)
                {
                    chkcgstval = 1;
                }
                if (chkSgst.Checked)
                {
                    chksgstval = 1;
                }
                if (chkigst.Checked)
                {
                    chkigstval = 1;
                }
                if (checkbapf.Checked)
                {
                    chkbapf = 1;
                }
                if (checkdapf.Checked)
                {
                    chkdapf = 1;
                }
                if (checkhrapf.Checked)
                {
                    chkhrapf = 1;
                }
                if (checkconvpf.Checked)
                {
                    chkconveypf = 1;
                }
                if (checkccapf.Checked)
                {
                    chkccapf = 1;
                }
                if (checkbonuspf.Checked)
                {
                    chkbonuspf = 1;
                }

                if (checkgratuitypf.Checked)
                {
                    chkgratuitypf = 1;

                }
                if (checklapf.Checked)
                {
                    chklapf = 1;
                }
                if (checknfhspf.Checked)
                {
                    chknfhspf = 1;

                }
                if (checkrcpf.Checked)
                {
                    chkrcpf = 1;
                }
                if (checkwapf.Checked)
                {
                    chkwapf = 1;

                }
                if (checkoapf.Checked)
                {
                    chkoapf = 1;
                }
                if (checkfoodpf.Checked)
                {
                    chkfoodallwpf = 1;
                }
                if (checkmedicalpf.Checked)
                {
                    chkmedicalallwpf = 1;

                }
                if (checkmedicalpf.Checked)
                {
                    chkmedicalallwpf = 1;
                }
                if (checktravelpf.Checked)
                {
                    chktravelallwpf = 1;
                }
                if (checkperfmpf.Checked)
                {
                    chkperfmallwpf = 1;
                }
                if (checkmobilepf.Checked)
                {
                    chkmobileallwpf = 1;
                }
                if (checkbaesi.Checked)
                {
                    chkbaesi = 1;
                }
                if (checkdaesi.Checked)
                {
                    chkdaesi = 1;
                }
                if (checkhraesi.Checked)
                {
                    chkhraesi = 1;
                }
                if (checkconvesi.Checked)
                {
                    chkconveyesi = 1;
                }
                if (checkccaesi.Checked)
                {
                    chkccaesi = 1;
                }
                if (checkbonusesi.Checked)
                {
                    chkbonusesi = 1;
                }
                if (checkgratuityesi.Checked)
                {
                    chkgratuityesi = 1;
                }
                if (checklaesi.Checked)
                {
                    chklaesi = 1;
                }
                if (checknfhsesi.Checked)
                {
                    chknfhsesi = 1;
                }
                if (checkrcesi.Checked)
                {
                    chkrcesi = 1;
                }
                if (checkwaesi.Checked)
                {
                    chkwaesi = 1;
                }
                if (checkoaesi.Checked)
                {
                    chknfhsesi = 1;
                }
                if (checkrcesi.Checked)
                {
                    chkoaesi = 1;
                }
                if (checkfoodesi.Checked)
                {
                    chkfoodallwesi = 1;
                }
                if (checkmedicalesi.Checked)
                {
                    chkmedicalallwesi = 1;
                }
                if (checktravelesi.Checked)
                {
                    chktravelallwesi = 1;
                }
                if (checkperfmesi.Checked)
                {
                    chkperfmallwesi = 1;
                }
                if (checkmobileesi.Checked)
                {
                    chkmobileallwesi = 1;
                }
                if (checkbasc.Checked)
                {
                    chkbasc = 1;
                }
                if (checkdasc.Checked)
                {
                    chkdasc = 1;
                }
                if (checkhrasc.Checked)
                {
                    chkhrasc = 1;
                }
                if (checkconvsc.Checked)
                {
                    chkconveysc = 1;
                }
                if (checkccasc.Checked)
                {
                    chkccasc = 1;
                }
                if (checkbonussc.Checked)
                {
                    chkbonussc = 1;
                }
                if (checkgratuitysc.Checked)
                {
                    chkgratuitysc = 1;
                }
                if (checklasc.Checked)
                {
                    chklasc = 1;
                }
                if (checknfhssc.Checked)
                {
                    chknfhssc = 1;
                }
                if (checkrcsc.Checked)
                {
                    chkrcsc = 1;
                }
                if (checkwasc.Checked)
                {
                    chkwasc = 1;

                }
                if (checkoasc.Checked)
                {
                    chkoasc = 1;
                }
                if (checkfoodsc.Checked)
                {
                    chkfoodallwsc = 1;
                }
                if (checkmedicalsc.Checked)
                {
                    chkmedicalallwsc = 1;
                }
                if (checktravelsc.Checked)
                {
                    chktravelallwsc = 1;
                }
                if (checkperfmsc.Checked)
                {
                    chkperfmallwsc = 1;
                }
                if (checkmobilesc.Checked)
                {
                    chkmobileallwsc = 1;
                }
                #endregion Code for Checkboxes

                result = Basic + DA + HRA + Conv + CCA + LA + gratuity + bonus + WA + OA + nfhs + RC + foodallowance + MedicalAllw + travelallowance + perfomanceallowance + mobileallowance + PF + ESI;
                lblresult.Visible = true;
                lblresult.Text = result.ToString();
                string spname = "M_InsertTempLeadRequirementAmounts";
                Hashtable hs = new Hashtable();
                hs.Add("@sno", sno);
                hs.Add("@leadid", DropLeadID.SelectedValue);
                hs.Add("@DesignationId", dropDesignation.SelectedValue);
                hs.Add("@BasicAmount", Basic);
                hs.Add("@DA", DA);
                hs.Add("@HRA", HRA);
                hs.Add("@Conveyence", Conv);
                hs.Add("@CCA", CCA);
                hs.Add("@LA", LA);
                hs.Add("@Gratuity", gratuity);
                hs.Add("@Bonus", bonus);
                hs.Add("@WA", WA);
                hs.Add("@OA", OA);
                hs.Add("@NFHS", nfhs);
                hs.Add("@RC", RC);
                hs.Add("@FoodAllowance", foodallowance);
                hs.Add("@MedicalAllowance", MedicalAllw);
                hs.Add("@TravelAllowance", travelallowance);
                hs.Add("@PerformanceAllowance", perfomanceallowance);
                hs.Add("@MoboleAllowance", mobileallowance);
                hs.Add("@PF", PF);
                hs.Add("@ESI", ESI);
                hs.Add("@result", result);
                hs.Add("@state", ddlstate.SelectedValue);
                hs.Add("@Zone", dropZone.SelectedValue);
                hs.Add("@type", dropType.SelectedValue);
                hs.Add("@category", dropcategory.SelectedValue);
                hs.Add("@servicecharge", txtschargeamount.Text);
                hs.Add("@cgstper", txtcgst.Text);
                hs.Add("@sgst", txtsgstper.Text);
                hs.Add("@igstper", txtigst.Text);
                hs.Add("@cgstamount", txtcgstamount.Text);
                hs.Add("@sgstamount", txtsgstamount.Text);
                hs.Add("@igstamount", txtigstamount.Text);
                hs.Add("@GrandTotal", txtGrandTotal.Text);
                hs.Add("@servicechargeper", txtschargeprc.Text);
                hs.Add("@chkbasic", chkbaval);
                hs.Add("@chkda", chkdaval);
                hs.Add("@chkhra", chkhraval);
                hs.Add("@chkconvey", chkconvval);
                hs.Add("@chkcca", chkccaval);
                hs.Add("@chkbonus", chkbonusval);
                hs.Add("@chkgratuity", chkgratuityval);
                hs.Add("@chkla", chklaval);
                hs.Add("@chknfhs", chknfhsval);
                hs.Add("@chkrc", chkrcval);
                hs.Add("@chkwa", chkwaval);
                hs.Add("@chkoa", chkoaval);
                hs.Add("@chkfoodallw", chkfoodallwval);
                hs.Add("@chkmedicalallow", chkmedicalallwval);
                hs.Add("@chktravelallw", chktravelallwval);
                hs.Add("@chkmobileallw", chkmobileallval);
                hs.Add("@chkperform", chkperfmallwval);
                hs.Add("@chkpf", chkpfval);
                hs.Add("@chkesi", chkesival);
                hs.Add("@chkservicecharge", chkservicechargeval);
                hs.Add("@chkcgst", chkcgstval);
                hs.Add("@chksgst", chksgstval);
                hs.Add("@chkigst", chkigstval);
                hs.Add("@chkbapf", chkbapf);
                hs.Add("@chkdapf", chkdapf);
                hs.Add("@chkhrapf", chkhrapf);
                hs.Add("@chkccapf", chkccapf);
                hs.Add("@chkconveypf", chkconveypf);
                hs.Add("@chkbonuspf", chkbonuspf);
                hs.Add("@chkgratuitypf", chkgratuitypf);
                hs.Add("@chklapf", chklapf);
                hs.Add("@chknfhspf", chknfhspf);
                hs.Add("@chkrcpf", chkrcpf);
                hs.Add("@chkwapf", chkwapf);
                hs.Add("@chkoapf", chkoapf);
                hs.Add("@chkfoodallwpf", chkfoodallwpf);
                hs.Add("@chkmedicalallwpf", chkmedicalallwpf);
                hs.Add("@chktravelallwpf", chktravelallwpf);
                hs.Add("@chkperfmallwpf", chkperfmallwpf);
                hs.Add("@chkmobileallwpf", chkmobileallwpf);
                hs.Add("@chkbaesi", chkbaesi);
                hs.Add("@chkdaesi", chkdaesi);
                hs.Add("@chkhraesi", chkhraesi);
                hs.Add("@chkconveyesi ", chkconveyesi);
                hs.Add("@chkccaesi", chkccaesi);
                hs.Add("@chkbonusesi", chkbonusesi);
                hs.Add("@chkgratuityesi", chkgratuityesi);
                hs.Add("@chklaesi", chklaesi);
                hs.Add("@chknfhsesi ", chknfhsesi);
                hs.Add("@chkrcesi", chkrcesi);
                hs.Add("@chkwaesi", chkwaesi);
                hs.Add("@chkoaesi", chkoaesi);
                hs.Add("@chkfoodallwesi", chkfoodallwesi);
                hs.Add("@chkmedicalallwesi", chkmedicalallwesi);
                hs.Add("@chktravelallwesi ", chktravelallwesi);
                hs.Add("@chkperfmallwesi", chkperfmallwesi);
                hs.Add("@chkmobileallwesi", chkmobileallwesi);
                hs.Add("@chkbasc", chkbasc);
                hs.Add("@chkdasc", chkdasc);
                hs.Add("@chkhrasc", chkhrasc);
                hs.Add("@chkconveysc", chkconveysc);
                hs.Add("@chkccasc", chkccasc);
                hs.Add("@chkbonussc", chkbonussc);
                hs.Add("@chkgratuitysc", chkgratuitysc);
                hs.Add("@chklasc", chklasc);
                hs.Add("@chknfhssc", chknfhssc);
                hs.Add("@chkrcsc", chkrcsc);
                hs.Add("@chkwasc", chkwasc);
                hs.Add("@chkoasc", chkoasc);
                hs.Add("@chkfoodallwsc", chkfoodallwsc);
                hs.Add("@chkmedicalallwsc", chkmedicalallwsc);
                hs.Add("@chktravelallwsc", chktravelallwsc);
                hs.Add("@chkperfmallwsc", chkperfmallwsc);
                hs.Add("@chkmobileallwsc", chkmobileallwsc);
                hs.Add("@pflimit", txtesipflimit.Text);
                hs.Add("@esilimit", txtesiesilimit.Text);
                hs.Add("@pftotalamount", pfamount.Text);
                hs.Add("@esitotalamount", esiamount.Text);
                hs.Add("@Materialcomponent", txtmaterialcomponent.Text);
                hs.Add("@Machinerycomponent", txtmachinerycomponent.Text);
                hs.Add("@Created_By", Username);

                int leadrequirementstatus = config.ExecuteNonQueryParamsAsync(spname, hs).Result;

                if (leadrequirementstatus > 0)
                {
                    string spnameLead = "GetLeadRequirementAmounts";
                    Hashtable hsleadid = new Hashtable();
                    hsleadid.Add("@id", DropLeadID.SelectedValue);
                    DataTable dt = config.ExecuteAdaptorAsyncWithParams(spnameLead, hsleadid).Result;
                    if (dt.Rows.Count > 0)
                    {
                        GVLeadsamount.DataSource = dt;
                        GVLeadsamount.DataBind();

                        float totalBasic = 0;
                        float totalDA = 0;
                        float totalHRA = 0;
                        float totalCCA = 0;
                        float totalla = 0;
                        float totalConveyance = 0;
                        float totalWA = 0;
                        float totalOA = 0;
                        float totalPF = 0;
                        float totalESI = 0;
                        float totalGratuity = 0;
                        float totalBonus = 0;
                        float totalnfhs = 0;
                        float totalRC = 0;
                        float totalfoodallowance = 0;
                        float totaltravelallowance = 0;
                        float totalperfomallowance = 0;
                        float totalmobileallowance = 0;
                        float totalSGSTamount = 0;
                        float totalIGSTamount = 0;
                        float totalCGSTamount = 0;
                        float totalservicecharge = 0;
                        float totalGrandTotal = 0;
                        float totalamount = 0;

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {


                            string Total = dt.Rows[i]["TotalAmount"].ToString();
                            if (Total.Trim().Length > 0)
                            {
                                totalamount += Convert.ToSingle(Total);
                            }

                            string servicecharge = dt.Rows[i]["servicecharge"].ToString();
                            if (servicecharge.Trim().Length > 0)
                            {
                                totalservicecharge += Convert.ToSingle(servicecharge);
                            }

                            string CGSTamount = dt.Rows[i]["CGSTamount"].ToString();
                            if (CGSTamount.Trim().Length > 0)
                            {
                                totalCGSTamount += Convert.ToSingle(CGSTamount);
                            }

                            string SGSTamount = dt.Rows[i]["SGSTamount"].ToString();
                            if (SGSTamount.Trim().Length > 0)
                            {
                                totalSGSTamount += Convert.ToSingle(SGSTamount);
                            }

                            string IGSTamount = dt.Rows[i]["IGSTamount"].ToString();
                            if (IGSTamount.Trim().Length > 0)
                            {
                                totalIGSTamount += Convert.ToSingle(IGSTamount);
                            }

                            string GrandTotal = dt.Rows[i]["GrandTotal"].ToString();
                            if (GrandTotal.Trim().Length > 0)
                            {
                                totalGrandTotal += Convert.ToSingle(GrandTotal);
                            }

                            string strBasic = dt.Rows[i]["Basic"].ToString();
                            if (strBasic.Trim().Length > 0)
                            {
                                totalBasic += Convert.ToSingle(strBasic);
                            }

                            string strDA = dt.Rows[i]["DA"].ToString();
                            if (strDA.Trim().Length > 0)
                            {
                                totalDA += Convert.ToSingle(strDA);
                            }

                            string strhHRA = dt.Rows[i]["HRA"].ToString();
                            if (strhHRA.Trim().Length > 0)
                            {
                                totalHRA += Convert.ToSingle(strhHRA);
                            }


                            string strConveyance = dt.Rows[i]["Conveyence"].ToString();
                            if (strConveyance.Trim().Length > 0)
                            {
                                totalConveyance += Convert.ToSingle(strConveyance);
                            }

                            string strCCA = dt.Rows[i]["CCA"].ToString();
                            if (strCCA.Trim().Length > 0)
                            {
                                totalCCA += Convert.ToSingle(strCCA);
                            }

                            string LAE = dt.Rows[i]["LA"].ToString();
                            if (LAE.Trim().Length > 0)
                            {
                                totalla += Convert.ToSingle(LAE);
                            }

                            string strGratuity = dt.Rows[i]["Gratuity"].ToString();
                            if (strGratuity.Trim().Length > 0)
                            {
                                totalGratuity += Convert.ToSingle(strGratuity);
                            }

                            string strBonus = dt.Rows[i]["Bonus"].ToString();
                            if (strBonus.Trim().Length > 0)
                            {
                                totalBonus += Convert.ToSingle(strBonus);
                            }

                            string strWA = dt.Rows[i]["WA"].ToString();
                            if (strWA.Trim().Length > 0)
                            {
                                totalWA += Convert.ToSingle(strWA);
                            }

                            string strOA = dt.Rows[i]["OA"].ToString();
                            if (strOA.Trim().Length > 0)
                            {
                                totalOA += Convert.ToSingle(strOA);
                            }

                            string strNfhs = dt.Rows[i]["Nfhs"].ToString();
                            if (strNfhs.Trim().Length > 0)
                            {
                                totalnfhs += Convert.ToSingle(strNfhs);
                            }

                            string RCC = dt.Rows[i]["RC"].ToString();
                            if (RCC.Trim().Length > 0)
                            {
                                totalRC += Convert.ToSingle(RCC);
                            }

                            string FoodAllowance = dt.Rows[i]["FoodAllowance"].ToString();
                            if (FoodAllowance.Trim().Length > 0)
                            {
                                totalfoodallowance += Convert.ToSingle(FoodAllowance);
                            }

                            string TravelAllowance = dt.Rows[i]["TravelAllowance"].ToString();
                            if (TravelAllowance.Trim().Length > 0)
                            {
                                totaltravelallowance += Convert.ToSingle(TravelAllowance);
                            }

                            string PerformanceAllowance = dt.Rows[i]["PerformanceAllowance"].ToString();
                            if (TravelAllowance.Trim().Length > 0)
                            {
                                totalperfomallowance += Convert.ToSingle(PerformanceAllowance);
                            }

                            string MoboleAllowance = dt.Rows[i]["MoboleAllowance"].ToString();
                            if (TravelAllowance.Trim().Length > 0)
                            {
                                totalmobileallowance += Convert.ToSingle(MoboleAllowance);
                            }

                            string PFEmplr = dt.Rows[i]["PF"].ToString();
                            if (PFEmplr.Trim().Length > 0)
                            {
                                totalPF += Convert.ToSingle(PFEmplr);
                            }

                            string strESI = dt.Rows[i]["ESI"].ToString();
                            if (strESI.Trim().Length > 0)
                            {
                                totalESI += Convert.ToSingle(strESI);
                            }
                        }



                        //6
                        Label lblTotalAmount = GVLeadsamount.FooterRow.FindControl("lblTotalAmount") as Label;
                        lblTotalAmount.Text = Math.Round(totalamount).ToString();
                        if (totalamount > 0)
                        {
                            GVLeadsamount.Columns[6].Visible = true;
                        }
                        else
                        {
                            GVLeadsamount.Columns[6].Visible = false;

                        }
                        //7
                        Label lblschargeamount = GVLeadsamount.FooterRow.FindControl("lblschargeamount") as Label;
                        lblschargeamount.Text = Math.Round(totalservicecharge).ToString();

                        if (totalservicecharge > 0)
                        {
                            GVLeadsamount.Columns[7].Visible = true;
                        }
                        else
                        {
                            GVLeadsamount.Columns[7].Visible = false;
                        }
                        //8
                        Label lblcgstamunt = GVLeadsamount.FooterRow.FindControl("lblcgstamunt") as Label;
                        lblcgstamunt.Text = Math.Round(totalCGSTamount).ToString();

                        if (totalCGSTamount > 0)
                        {
                            GVLeadsamount.Columns[8].Visible = true;
                        }
                        else
                        {
                            GVLeadsamount.Columns[8].Visible = false;
                        }


                        //9
                        Label lblsgstamunt = GVLeadsamount.FooterRow.FindControl("lblsgstamunt") as Label;
                        lblsgstamunt.Text = Math.Round(totalSGSTamount).ToString();

                        if (totalSGSTamount > 0)
                        {
                            GVLeadsamount.Columns[9].Visible = true;
                        }
                        else
                        {
                            GVLeadsamount.Columns[9].Visible = false;
                        }

                        Label lbligstamunt = GVLeadsamount.FooterRow.FindControl("lbligstamunt") as Label;
                        lbligstamunt.Text = Math.Round(totalIGSTamount).ToString();

                        if (totalIGSTamount > 0)
                        {
                            GVLeadsamount.Columns[10].Visible = true;
                        }
                        else
                        {
                            GVLeadsamount.Columns[10].Visible = false;
                        }

                        //11
                        Label lblgrandtotalamount = GVLeadsamount.FooterRow.FindControl("lblgrandtotalamount") as Label;
                        lblgrandtotalamount.Text = Math.Round(totalGrandTotal).ToString();

                        if (totalGrandTotal > 0)
                        {
                            GVLeadsamount.Columns[11].Visible = true;
                        }
                        else
                        {
                            GVLeadsamount.Columns[11].Visible = false;
                        }
                        //12
                        Label lblbasictotal = GVLeadsamount.FooterRow.FindControl("lblbasictotal") as Label;
                        lblbasictotal.Text = Math.Round(totalBasic).ToString();

                        if (totalBasic > 0)
                        {
                            GVLeadsamount.Columns[12].Visible = true;
                        }
                        else
                        {
                            GVLeadsamount.Columns[12].Visible = false;
                        }

                        //13
                        Label lbldatotal = GVLeadsamount.FooterRow.FindControl("lbldatotal") as Label;
                        lbldatotal.Text = Math.Round(totalDA).ToString();

                        if (totalDA > 0)
                        {
                            GVLeadsamount.Columns[13].Visible = true;
                        }
                        else
                        {
                            GVLeadsamount.Columns[13].Visible = false;
                        }
                        //14
                        Label lblhratotal = GVLeadsamount.FooterRow.FindControl("lblhratotal") as Label;
                        lblhratotal.Text = Math.Round(totalHRA).ToString();

                        if (totalHRA > 0)
                        {
                            GVLeadsamount.Columns[14].Visible = true;
                        }
                        else
                        {
                            GVLeadsamount.Columns[14].Visible = false;
                        }
                        //15
                        Label lblconvtotal = GVLeadsamount.FooterRow.FindControl("lblconvtotal") as Label;
                        lblconvtotal.Text = Math.Round(totalConveyance).ToString();

                        if (totalConveyance > 0)
                        {
                            GVLeadsamount.Columns[15].Visible = true;
                        }
                        else
                        {
                            GVLeadsamount.Columns[15].Visible = false;
                        }
                        //16
                        Label lblccatotal = GVLeadsamount.FooterRow.FindControl("lblccatotal") as Label;
                        lblccatotal.Text = Math.Round(totalCCA).ToString();

                        if (totalCCA > 0)
                        {
                            GVLeadsamount.Columns[16].Visible = true;
                        }
                        else
                        {
                            GVLeadsamount.Columns[16].Visible = false;
                        }

                        //17
                        Label lbllatotal = GVLeadsamount.FooterRow.FindControl("lbllatotal") as Label;
                        lbllatotal.Text = Math.Round(totalla).ToString();

                        if (totalla > 0)
                        {
                            GVLeadsamount.Columns[17].Visible = true;
                        }
                        else
                        {
                            GVLeadsamount.Columns[17].Visible = false;
                        }
                        //18
                        Label lblgratuitytotal = GVLeadsamount.FooterRow.FindControl("lblgratuitytotal") as Label;
                        lblgratuitytotal.Text = Math.Round(totalGratuity).ToString();

                        if (totalGratuity > 0)
                        {
                            GVLeadsamount.Columns[18].Visible = true;
                        }
                        else
                        {
                            GVLeadsamount.Columns[18].Visible = false;
                        }
                        //19
                        Label lblbonustotal = GVLeadsamount.FooterRow.FindControl("lblbonustotal") as Label;
                        lblbonustotal.Text = Math.Round(totalBonus).ToString();

                        if (totalBonus > 0)
                        {
                            GVLeadsamount.Columns[19].Visible = true;
                        }
                        else
                        {
                            GVLeadsamount.Columns[19].Visible = false;
                        }
                        //20
                        Label lblWAtotal = GVLeadsamount.FooterRow.FindControl("lblWAtotal") as Label;
                        lblWAtotal.Text = Math.Round(totalWA).ToString();

                        if (totalWA > 0)
                        {
                            GVLeadsamount.Columns[20].Visible = true;
                        }
                        else
                        {
                            GVLeadsamount.Columns[20].Visible = false;
                        }
                        //21
                        Label lbloatotal = GVLeadsamount.FooterRow.FindControl("lbloatotal") as Label;
                        lbloatotal.Text = Math.Round(totalOA).ToString();
                        if (totalOA > 0)
                        {
                            GVLeadsamount.Columns[21].Visible = true;
                        }
                        else
                        {
                            GVLeadsamount.Columns[21].Visible = false;
                        }

                        //22
                        Label lblnfhstotal = GVLeadsamount.FooterRow.FindControl("lblnfhstotal") as Label;
                        lblnfhstotal.Text = Math.Round(totalnfhs).ToString();

                        if (totalnfhs > 0)
                        {
                            GVLeadsamount.Columns[22].Visible = true;
                        }
                        else
                        {
                            GVLeadsamount.Columns[22].Visible = false;
                        }

                        //23
                        Label lblrctotal = GVLeadsamount.FooterRow.FindControl("lblrctotal") as Label;
                        lblrctotal.Text = Math.Round(totalRC).ToString();

                        if (totalRC > 0)
                        {
                            GVLeadsamount.Columns[23].Visible = true;
                        }
                        else
                        {
                            GVLeadsamount.Columns[23].Visible = false;
                        }

                        //24
                        Label lblfawtotal = GVLeadsamount.FooterRow.FindControl("lblfawtotal") as Label;
                        lblfawtotal.Text = Math.Round(totalfoodallowance).ToString();

                        if (totalfoodallowance > 0)
                        {
                            GVLeadsamount.Columns[24].Visible = true;
                        }
                        else
                        {
                            GVLeadsamount.Columns[24].Visible = false;
                        }
                        //25
                        Label lbtatotal = GVLeadsamount.FooterRow.FindControl("lbtatotal") as Label;
                        lbtatotal.Text = Math.Round(totaltravelallowance).ToString();

                        if (totaltravelallowance > 0)
                        {
                            GVLeadsamount.Columns[25].Visible = true;
                        }
                        else
                        {
                            GVLeadsamount.Columns[25].Visible = false;
                        }
                        //26
                        Label lblpfatotal = GVLeadsamount.FooterRow.FindControl("lblpfatotal") as Label;
                        lblpfatotal.Text = Math.Round(totalperfomallowance).ToString();

                        if (totalperfomallowance > 0)
                        {
                            GVLeadsamount.Columns[26].Visible = true;
                        }
                        else
                        {
                            GVLeadsamount.Columns[26].Visible = false;
                        }
                        //27
                        Label lblmatotal = GVLeadsamount.FooterRow.FindControl("lblmatotal") as Label;
                        lblmatotal.Text = Math.Round(totalmobileallowance).ToString();
                        if (totalmobileallowance > 0)
                        {
                            GVLeadsamount.Columns[27].Visible = true;
                        }
                        else
                        {
                            GVLeadsamount.Columns[27].Visible = false;
                        }
                        //28
                        Label lblpftotal = GVLeadsamount.FooterRow.FindControl("lblpftotal") as Label;
                        lblpftotal.Text = Math.Round(totalPF).ToString();
                        if (totalPF > 0)
                        {
                            GVLeadsamount.Columns[28].Visible = true;
                        }
                        else
                        {
                            GVLeadsamount.Columns[28].Visible = false;
                        }
                        //29
                        Label lblesitotal = GVLeadsamount.FooterRow.FindControl("lblesitotal") as Label;
                        lblesitotal.Text = Math.Round(totalESI).ToString();
                        if (totalESI > 0)
                        {
                            GVLeadsamount.Columns[29].Visible = true;
                        }
                        else
                        {
                            GVLeadsamount.Columns[29].Visible = false;
                        }

                    }

                }

                if (DropLeadID.SelectedIndex == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Select Leadid')", true);
                    return;
                }
                if (DropLeadName.SelectedIndex == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Select LeadName')", true);
                    return;
                }


                AddGridDetails();
                Cleardata();
                #endregion
            }
            catch (Exception ex)
            {

            }
        }

        protected void AddGridDetails()
        {
            string queryresult = "delete from M_LeadRequirementAmounts where Leadid='" + DropLeadID.SelectedValue + "'";
            int dtresult = SqlHelper.Instance.ExecuteDMLQry(queryresult);


            int sno = 1;

            for (int i = 0; i < GVLeadsamount.Rows.Count; i++)
            {
                Label lbldesgnation = GVLeadsamount.Rows[i].FindControl("lbldesgnation") as Label;
                Label lblbasicamount = GVLeadsamount.Rows[i].FindControl("lblbasicamount") as Label;
                Label lblda = GVLeadsamount.Rows[i].FindControl("lblda") as Label;
                Label lblhra = GVLeadsamount.Rows[i].FindControl("lblhra") as Label;
                Label lblconveyence = GVLeadsamount.Rows[i].FindControl("lblconveyence") as Label;
                Label lblcca = GVLeadsamount.Rows[i].FindControl("lblcca") as Label;
                Label lblLA = GVLeadsamount.Rows[i].FindControl("lblLA") as Label;
                Label lblgratuity = GVLeadsamount.Rows[i].FindControl("lblgratuity") as Label;
                Label lblbonus = GVLeadsamount.Rows[i].FindControl("lblbonus") as Label;
                Label lblWA = GVLeadsamount.Rows[i].FindControl("lblWA") as Label;
                Label lblOA = GVLeadsamount.Rows[i].FindControl("lblOA") as Label;
                Label lblnfhs = GVLeadsamount.Rows[i].FindControl("lblnfhs") as Label;
                Label lblRc = GVLeadsamount.Rows[i].FindControl("lblRc") as Label;
                Label lblFAW = GVLeadsamount.Rows[i].FindControl("lblFAW") as Label;
                Label lblTA = GVLeadsamount.Rows[i].FindControl("lblTA") as Label;
                Label lblpfa = GVLeadsamount.Rows[i].FindControl("lblpfa") as Label;
                Label lblMA = GVLeadsamount.Rows[i].FindControl("lblMA") as Label;
                Label lblPF = GVLeadsamount.Rows[i].FindControl("lblPF") as Label;
                Label lblESi = GVLeadsamount.Rows[i].FindControl("lblESi") as Label;
                Label lblresult = GVLeadsamount.Rows[i].FindControl("lblresult") as Label;
                Label lblstate = GVLeadsamount.Rows[i].FindControl("lblstate") as Label;
                Label lblzone = GVLeadsamount.Rows[i].FindControl("lblzone") as Label;
                Label lbltype = GVLeadsamount.Rows[i].FindControl("lbltype") as Label;
                Label lblcategory = GVLeadsamount.Rows[i].FindControl("lblcategory") as Label;
                Label lblschargeamount = GVLeadsamount.Rows[i].FindControl("lblscharge") as Label;
                //Label lblgstamount = GVLeadsamount.Rows[i].FindControl("lblgst") as Label;
                Label lblgrandtotalamount = GVLeadsamount.Rows[i].FindControl("lblgrandtotal") as Label;
                Label lblschargepercentamount = GVLeadsamount.Rows[i].FindControl("lblschargepercent") as Label;
                Label lblcgstamount = GVLeadsamount.Rows[i].FindControl("lblgridcgst") as Label;
                Label lblsgstamount = GVLeadsamount.Rows[i].FindControl("lblgridSgst") as Label;
                Label lbligstamount = GVLeadsamount.Rows[i].FindControl("lblgridIgst") as Label;
                Label lblcgstper = GVLeadsamount.Rows[i].FindControl("lblcgstper") as Label;
                Label lblsgstper = GVLeadsamount.Rows[i].FindControl("lblsgstper") as Label;
                Label lbligstper = GVLeadsamount.Rows[i].FindControl("lbligstper") as Label;
                Label lblgridchkbasic = GVLeadsamount.Rows[i].FindControl("lblgridchkbasic") as Label;
                Label lblgridchkda = GVLeadsamount.Rows[i].FindControl("lblgridchkda") as Label;
                Label lblgridchkhra = GVLeadsamount.Rows[i].FindControl("lblgridchkhra") as Label;
                Label lblgridchkconvey = GVLeadsamount.Rows[i].FindControl("lblgridchkconvey") as Label;
                Label lblgridchkcca = GVLeadsamount.Rows[i].FindControl("lblgridchkcca") as Label;
                Label lblgridchkbonus = GVLeadsamount.Rows[i].FindControl("lblgridchkbonus") as Label;
                Label lblgridchkgratuity = GVLeadsamount.Rows[i].FindControl("lblgridchkgratuity") as Label;
                Label lblgridchkla = GVLeadsamount.Rows[i].FindControl("lblgridchkla") as Label;
                Label lblgridchknfhs = GVLeadsamount.Rows[i].FindControl("lblgridchknfhs") as Label;
                Label lblgridchkrc = GVLeadsamount.Rows[i].FindControl("lblgridchkrc") as Label;
                Label lblgridchkwa = GVLeadsamount.Rows[i].FindControl("lblgridchkwa") as Label;
                Label lblgridchkoa = GVLeadsamount.Rows[i].FindControl("lblgridchkoa") as Label;
                Label lblgridchkfoodallw = GVLeadsamount.Rows[i].FindControl("lblgridchkfoodallw") as Label;
                Label lblgridchkmedicalallow = GVLeadsamount.Rows[i].FindControl("lblgridchkmedicalallow") as Label;
                Label lblgridchktravelallw = GVLeadsamount.Rows[i].FindControl("lblgridchktravelallw") as Label;
                Label lblgridchkperform = GVLeadsamount.Rows[i].FindControl("lblgridchkperform") as Label;
                Label lblgridchkmobileallw = GVLeadsamount.Rows[i].FindControl("lblgridchkmobileallw") as Label;
                Label lblgridchkpf = GVLeadsamount.Rows[i].FindControl("lblgridchkpf") as Label;
                Label lblgridchkesi = GVLeadsamount.Rows[i].FindControl("lblgridchkesi") as Label;
                Label lblgridchkservicecharge = GVLeadsamount.Rows[i].FindControl("lblgridchkservicecharge") as Label;
                Label lblgridchkcgst = GVLeadsamount.Rows[i].FindControl("lblgridchkcgst") as Label;
                Label lblgridchksgst = GVLeadsamount.Rows[i].FindControl("lblgridchksgst") as Label;
                Label lblgridchkigst = GVLeadsamount.Rows[i].FindControl("lblgridchkigst") as Label;
                Label lblchkbapf = GVLeadsamount.Rows[i].FindControl("lblchkbapf") as Label;
                Label lblchkdapf = GVLeadsamount.Rows[i].FindControl("lblchkdapf") as Label;
                Label lblchkhrapf = GVLeadsamount.Rows[i].FindControl("lblchkhrapf") as Label;
                Label lblchkconveypf = GVLeadsamount.Rows[i].FindControl("lblchkconveypf") as Label;
                Label lblchkccapf = GVLeadsamount.Rows[i].FindControl("lblchkccapf") as Label;
                Label lblchkbonuspf = GVLeadsamount.Rows[i].FindControl("lblchkbonuspf") as Label;
                Label lblchkgratuitypf = GVLeadsamount.Rows[i].FindControl("lblchkgratuitypf") as Label;
                Label lblchklapf = GVLeadsamount.Rows[i].FindControl("lblchklapf") as Label;
                Label lblchknfhspf = GVLeadsamount.Rows[i].FindControl("lblchknfhspf") as Label;
                Label lblchkrcpf = GVLeadsamount.Rows[i].FindControl("lblchkrcpf") as Label;
                Label lblchkwapf = GVLeadsamount.Rows[i].FindControl("lblchkwapf") as Label;
                Label lblchkoapf = GVLeadsamount.Rows[i].FindControl("lblchkoapf") as Label;
                Label lblchkfoodallwpf = GVLeadsamount.Rows[i].FindControl("lblchkfoodallwpf") as Label;
                Label lblchkmedicalallwpf = GVLeadsamount.Rows[i].FindControl("lblchkmedicalallwpf") as Label;
                Label lblchktravelallwpf = GVLeadsamount.Rows[i].FindControl("lblchktravelallwpf") as Label;
                Label lblchkperfmallwpf = GVLeadsamount.Rows[i].FindControl("lblchkperfmallwpf") as Label;
                Label lblchkmobileallwpf = GVLeadsamount.Rows[i].FindControl("lblchkmobileallwpf") as Label;
                Label lblchkbaesi = GVLeadsamount.Rows[i].FindControl("lblchkbaesi") as Label;
                Label lblchkdaesi = GVLeadsamount.Rows[i].FindControl("lblchkdaesi") as Label;
                Label lblchkhraesi = GVLeadsamount.Rows[i].FindControl("lblchkhraesi") as Label;
                Label lblchkconveyesi = GVLeadsamount.Rows[i].FindControl("lblchkconveyesi") as Label;
                Label lblchkccaesi = GVLeadsamount.Rows[i].FindControl("lblchkccaesi") as Label;
                Label lblchkbonusesi = GVLeadsamount.Rows[i].FindControl("lblchkbonusesi") as Label;
                Label lblchkgratuityesi = GVLeadsamount.Rows[i].FindControl("lblchkgratuityesi") as Label;
                Label lblchklaesi = GVLeadsamount.Rows[i].FindControl("lblchklaesi") as Label;
                Label lblchknfhsesi = GVLeadsamount.Rows[i].FindControl("lblchknfhsesi") as Label;
                Label lblchkrcesi = GVLeadsamount.Rows[i].FindControl("lblchkrcesi") as Label;
                Label lblchkwaesi = GVLeadsamount.Rows[i].FindControl("lblchkwaesi") as Label;
                Label lblchkoaesi = GVLeadsamount.Rows[i].FindControl("lblchkoaesi") as Label;
                Label lblchkfoodallwesi = GVLeadsamount.Rows[i].FindControl("lblchkfoodallwesi") as Label;
                Label lblchkmedicalallwesi = GVLeadsamount.Rows[i].FindControl("lblchkmedicalallwesi") as Label;
                Label lblchktravelallwesi = GVLeadsamount.Rows[i].FindControl("lblchktravelallwesi") as Label;
                Label lblchkperfmallwesi = GVLeadsamount.Rows[i].FindControl("lblchkperfmallwpf") as Label;
                Label lblchkmobileallwesi = GVLeadsamount.Rows[i].FindControl("lblchkmobileallwesi") as Label;
                Label lblchkbasc = GVLeadsamount.Rows[i].FindControl("lblchkbasc") as Label;
                Label lblchkdasc = GVLeadsamount.Rows[i].FindControl("lblchkdasc") as Label;
                Label lblchkhrasc = GVLeadsamount.Rows[i].FindControl("lblchkhrasc") as Label;
                Label lblchkconveysc = GVLeadsamount.Rows[i].FindControl("lblchkconveysc") as Label;
                Label lblchkccasc = GVLeadsamount.Rows[i].FindControl("lblchkccasc") as Label;
                Label lblchkbonussc = GVLeadsamount.Rows[i].FindControl("lblchkbonussc") as Label;
                Label lblchkgratuitysc = GVLeadsamount.Rows[i].FindControl("lblchkgratuitysc") as Label;
                Label lblchklasc = GVLeadsamount.Rows[i].FindControl("lblchklasc") as Label;
                Label lblchknfhssc = GVLeadsamount.Rows[i].FindControl("lblchknfhssc") as Label;
                Label lblchkrcsc = GVLeadsamount.Rows[i].FindControl("lblchkrcsc") as Label;
                Label lblchkwasc = GVLeadsamount.Rows[i].FindControl("lblchkwasc") as Label;
                Label lblchkoasc = GVLeadsamount.Rows[i].FindControl("lblchkoasc") as Label;
                Label lblchkfoodallwsc = GVLeadsamount.Rows[i].FindControl("lblchkfoodallwsc") as Label;
                Label lblchkmedicalallwsc = GVLeadsamount.Rows[i].FindControl("lblchkmedicalallwesi") as Label;
                Label lblchktravelallwsc = GVLeadsamount.Rows[i].FindControl("lblchktravelallwsc") as Label;
                Label lblchkperfmallwsc = GVLeadsamount.Rows[i].FindControl("lblchkperfmallwsc") as Label;
                Label lblchkmobileallwsc = GVLeadsamount.Rows[i].FindControl("lblchkmobileallwsc") as Label;
                Label lblpflimits = GVLeadsamount.Rows[i].FindControl("lblpflimit") as Label;
                Label lblesilimits = GVLeadsamount.Rows[i].FindControl("lblesilimit") as Label;
                Label lblpftotalamounts = GVLeadsamount.Rows[i].FindControl("lblpftotalamount") as Label;
                Label lblesitotalamounts = GVLeadsamount.Rows[i].FindControl("lblesitotalamount") as Label;
                Label lblMaterialcomponents = GVLeadsamount.Rows[i].FindControl("lblMaterialcomponent") as Label;
                Label lblMachinerycomponents = GVLeadsamount.Rows[i].FindControl("lblMachinerycomponent") as Label;


                if (lblbasicamount.Text.Length > 0)
                {
                    Basic = decimal.Parse(lblbasicamount.Text);
                }
                if (lblda.Text.Length > 0)
                {
                    DA = decimal.Parse(lblda.Text);
                }
                if (lblhra.Text.Length > 0)
                {
                    HRA = decimal.Parse(lblhra.Text);
                }
                if (lblconveyence.Text.Length > 0)
                {
                    Conv = decimal.Parse(lblconveyence.Text);
                }
                if (lblcca.Text.Length > 0)
                {
                    CCA = decimal.Parse(lblcca.Text);
                }
                if (lblLA.Text.Length > 0)
                {
                    LA = decimal.Parse(lblLA.Text);
                }
                if (lblgratuity.Text.Length > 0)
                {
                    gratuity = decimal.Parse(lblgratuity.Text);
                }
                if (lblbonus.Text.Length > 0)
                {
                    bonus = decimal.Parse(lblbonus.Text);
                }
                if (lblWA.Text.Length > 0)
                {
                    WA = decimal.Parse(lblWA.Text);
                }
                if (lblOA.Text.Length > 0)
                {
                    OA = decimal.Parse(lblOA.Text);
                }
                if (lblnfhs.Text.Length > 0)
                {
                    nfhs = decimal.Parse(lblnfhs.Text);
                }
                if (lblRc.Text.Length > 0)
                {
                    RC = decimal.Parse(lblRc.Text);
                }
                if (lblWA.Text.Length > 0)
                {
                    WA = decimal.Parse(lblWA.Text);
                }
                if (lblFAW.Text.Length > 0)
                {
                    foodallowance = decimal.Parse(lblFAW.Text);
                }
                if (lblTA.Text.Length > 0)
                {
                    travelallowance = decimal.Parse(lblTA.Text);
                }
                if (lblpfa.Text.Length > 0)
                {
                    perfomanceallowance = decimal.Parse(lblpfa.Text);
                }
                if (lblMA.Text.Length > 0)
                {
                    mobileallowance = decimal.Parse(lblMA.Text);
                }
                if (lblPF.Text.Length > 0)
                {
                    PF = decimal.Parse(lblPF.Text);
                }
                if (lblESi.Text.Length > 0)
                {
                    ESI = decimal.Parse(lblESi.Text);
                }
                if (lblresult.Text.Length > 0)
                {
                    result = decimal.Parse(lblresult.Text);
                }


                string query = "select sno from M_LeadRequirementAmounts where LeadID='" + DropLeadID.SelectedValue + "'";
                DataTable dtquery = SqlHelper.Instance.GetTableByQuery(query);
                if (dtquery.Rows.Count > 0)
                {
                    sno = int.Parse(dtquery.Rows[0]["Sno"].ToString());
                    sno = sno + 1;
                }

                Hashtable hsdetails = new Hashtable();
                hsdetails.Add("@sno", sno);
                hsdetails.Add("@DesignationId", lbldesgnation.Text);
                hsdetails.Add("@leadid", DropLeadID.SelectedValue);
                hsdetails.Add("@BasicAmount", Basic);
                hsdetails.Add("@DA", DA);
                hsdetails.Add("@HRA", HRA);
                hsdetails.Add("@Conveyence", Conv);
                hsdetails.Add("@CCA", CCA);
                hsdetails.Add("@LA", LA);
                hsdetails.Add("@Gratuity", gratuity);
                hsdetails.Add("@Bonus", bonus);
                hsdetails.Add("@WA", WA);
                hsdetails.Add("@OA", OA);
                hsdetails.Add("@NFHS", nfhs);
                hsdetails.Add("@RC", RC);
                hsdetails.Add("@FoodAllowance", foodallowance);
                hsdetails.Add("@MedicalAllowance", MedicalAllw);
                hsdetails.Add("@TravelAllowance", travelallowance);
                hsdetails.Add("@PerformanceAllowance", perfomanceallowance);
                hsdetails.Add("@MoboleAllowance", mobileallowance);
                hsdetails.Add("@PF", PF);
                hsdetails.Add("@ESI", ESI);
                hsdetails.Add("@result", result);
                hsdetails.Add("@state", ddlstate.SelectedValue);
                hsdetails.Add("@Zone", dropZone.SelectedValue);
                hsdetails.Add("@type", dropType.SelectedValue);
                hsdetails.Add("@category", dropcategory.SelectedValue);
                hsdetails.Add("@servicecharge", lblschargeamount.Text);
                hsdetails.Add("@GrandTotal", lblgrandtotalamount.Text);
                hsdetails.Add("@servicechargeper", lblschargepercentamount.Text);
                hsdetails.Add("@cgstamount", lblcgstamount.Text);
                hsdetails.Add("@sgstamount", lblsgstamount.Text);
                hsdetails.Add("@igstamount", lbligstamount.Text);
                hsdetails.Add("@cgstper", lblcgstper.Text);
                hsdetails.Add("@sgst", lblsgstper.Text);
                hsdetails.Add("@igstper", lbligstper.Text);
                hsdetails.Add("@chkbasic", lblgridchkbasic.Text);
                hsdetails.Add("@chkda", lblgridchkda.Text);
                hsdetails.Add("@chkhra", lblgridchkhra.Text);
                hsdetails.Add("@chkconvey", lblgridchkconvey.Text);
                hsdetails.Add("@chkcca", lblgridchkcca.Text);
                hsdetails.Add("@chkbonus", lblgridchkbonus.Text);
                hsdetails.Add("@chkgratuity", lblgridchkgratuity.Text);
                hsdetails.Add("@chkla", lblgridchkla.Text);
                hsdetails.Add("@chknfhs", lblgridchknfhs.Text);
                hsdetails.Add("@chkrc", lblgridchkrc.Text);
                hsdetails.Add("@chkwa", lblgridchkwa.Text);
                hsdetails.Add("@chkoa", lblgridchkoa.Text);
                hsdetails.Add("@chkfoodallw", lblgridchkfoodallw.Text);
                hsdetails.Add("@chkmedicalallow", lblgridchkmedicalallow.Text);
                hsdetails.Add("@chktravelallw", lblgridchktravelallw.Text);
                hsdetails.Add("@chkmobileallw", lblgridchkmobileallw.Text);
                hsdetails.Add("@chkperform", lblgridchkperform.Text);
                hsdetails.Add("@chkpf", lblgridchkpf.Text);
                hsdetails.Add("@chkesi", lblgridchkesi.Text);
                hsdetails.Add("@chkservicecharge", lblgridchkservicecharge.Text);
                hsdetails.Add("@chkcgst", lblgridchkcgst.Text);
                hsdetails.Add("@chksgst", lblgridchksgst.Text);
                hsdetails.Add("@chkigst", lblgridchksgst.Text);
                hsdetails.Add("@chkbapf", lblchkbapf.Text);
                hsdetails.Add("@chkdapf", lblchkdapf.Text);
                hsdetails.Add("@chkhrapf", lblchkhrapf.Text);
                hsdetails.Add("@chkccapf", lblchkccapf.Text);
                hsdetails.Add("@chkconveypf", lblchkconveypf.Text);
                hsdetails.Add("@chkbonuspf", lblchkbonuspf.Text);
                hsdetails.Add("@chkgratuitypf", lblchkgratuitypf.Text);
                hsdetails.Add("@chklapf", lblchklapf.Text);
                hsdetails.Add("@chknfhspf", lblchknfhspf.Text);
                hsdetails.Add("@chkrcpf", lblchkrcpf.Text);
                hsdetails.Add("@chkwapf", lblchkwapf.Text);
                hsdetails.Add("@chkoapf", lblchkoapf.Text);
                hsdetails.Add("@chkfoodallwpf", lblchkfoodallwpf.Text);
                hsdetails.Add("@chkmedicalallwpf", lblchkmedicalallwpf.Text);
                hsdetails.Add("@chktravelallwpf", lblchktravelallwpf.Text);
                hsdetails.Add("@chkperfmallwpf", lblchkperfmallwpf.Text);
                hsdetails.Add("@chkmobileallwpf", lblchkmobileallwpf.Text);
                hsdetails.Add("@chkbaesi", lblchkbaesi.Text);
                hsdetails.Add("@chkdaesi", lblchkdaesi.Text);
                hsdetails.Add("@chkhraesi", lblchkhraesi.Text);
                hsdetails.Add("@chkconveyesi ", lblchkconveyesi.Text);
                hsdetails.Add("@chkccaesi", lblchkccaesi.Text);
                hsdetails.Add("@chkbonusesi", lblchkbonusesi.Text);
                hsdetails.Add("@chkgratuityesi", lblchkgratuityesi.Text);
                hsdetails.Add("@chklaesi", lblchklaesi.Text);
                hsdetails.Add("@chknfhsesi ", lblchknfhsesi.Text);
                hsdetails.Add("@chkrcesi", lblchkrcesi.Text);
                hsdetails.Add("@chkwaesi", lblchkwaesi.Text);
                hsdetails.Add("@chkoaesi", lblchkoaesi.Text);
                hsdetails.Add("@chkfoodallwesi", lblchkfoodallwesi.Text);
                hsdetails.Add("@chkmedicalallwesi", lblchkmedicalallwesi.Text);
                hsdetails.Add("@chktravelallwesi ", lblchktravelallwesi.Text);
                hsdetails.Add("@chkperfmallwesi", lblchkperfmallwesi.Text);
                hsdetails.Add("@chkmobileallwesi", lblchkmobileallwesi.Text);
                hsdetails.Add("@chkbasc", lblchkbasc.Text);
                hsdetails.Add("@chkdasc", lblchkdasc.Text);
                hsdetails.Add("@chkhrasc", lblchkhrasc.Text);
                hsdetails.Add("@chkconveysc", lblchkconveysc.Text);
                hsdetails.Add("@chkccasc", lblchkccasc.Text);
                hsdetails.Add("@chkbonussc", lblchkbonussc.Text);
                hsdetails.Add("@chkgratuitysc", lblchkgratuitysc.Text);
                hsdetails.Add("@chklasc", lblchklasc.Text);
                hsdetails.Add("@chknfhssc", lblchknfhssc.Text);
                hsdetails.Add("@chkrcsc", lblchkrcsc.Text);
                hsdetails.Add("@chkwasc", lblchkwasc.Text);
                hsdetails.Add("@chkoasc", lblchkoasc.Text);
                hsdetails.Add("@chkfoodallwsc", lblchkfoodallwsc.Text);
                hsdetails.Add("@chkmedicalallwsc", lblchkmedicalallwsc.Text);
                hsdetails.Add("@chktravelallwsc", lblchktravelallwsc.Text);
                hsdetails.Add("@chkperfmallwsc", lblchkperfmallwsc.Text);
                hsdetails.Add("@chkmobileallwsc", lblchkmobileallwsc.Text);
                hsdetails.Add("@pflimit", lblpflimits.Text);
                hsdetails.Add("@esilimit", lblesilimits.Text);
                hsdetails.Add("@pftotalamount", lblpftotalamounts.Text);
                hsdetails.Add("@esitotalamount", lblesitotalamounts.Text);
                hsdetails.Add("@Materialcomponent", lblMaterialcomponents.Text);
                hsdetails.Add("@Machinerycomponent", lblMachinerycomponents.Text);
                hsdetails.Add("@Created_By", Username);

                string spnamedetails = "M_InsertLeadRequirementAmounts";
                int dtleadrequirement = config.ExecuteNonQueryParamsAsync(spnamedetails, hsdetails).Result;
            }
        }

        protected void Cleardata()
        {
            txtBasic.Text = txtDA.Text = txtHRA.Text = txtconv.Text = txtCCA.Text = txtLA.Text = txtGratuity.Text = txtBonus.Text =
                txtWA.Text = txtOA.Text = txtNFHS.Text = txtRC.Text = txtFoodAllowance.Text = txtMedicalAllowance.Text = txtTravelAllowance.Text =
                txtPerfomanceAllowance.Text = txtMobileAllowance.Text = txtPF.Text = txtEsi.Text = txtschargeprc.Text = lblresult.Text = txtschargeamount.Text = txtGrandTotal.Text = txtcgst.Text = txtcgstamount.Text = txtsgstper.Text = txtsgstamount.Text = txtigst.Text = txtigstamount.Text = txtesipflimit.Text = txtesiesilimit.Text = pfamount.Text = esiamount.Text = txtmachinerycomponent.Text = txtmaterialcomponent.Text = string.Empty;
            dropDesignation.SelectedIndex = ddlstate.SelectedIndex = dropZone.SelectedIndex =
            dropType.SelectedIndex = dropcategory.SelectedIndex = 0;


        }

        protected void GetValuesCleardata()
        {
            txtBasic.Text = txtDA.Text = txtHRA.Text = txtconv.Text = txtCCA.Text = txtLA.Text = txtGratuity.Text = txtBonus.Text =
            txtWA.Text = txtOA.Text = txtNFHS.Text = txtRC.Text = txtFoodAllowance.Text = txtMedicalAllowance.Text = txtTravelAllowance.Text =
            txtPerfomanceAllowance.Text = txtMobileAllowance.Text = txtPF.Text = txtEsi.Text = txtschargeprc.Text = lblresult.Text = txtschargeamount.Text = /*txtgstamount.Text =*/string.Empty;
        }








        protected void dropDesignation_SelectedIndexChanged(object sender, EventArgs e)
        {
            string desigquery = "select * from  M_LeadRequirementAmounts where LeadId='" + DropLeadID.SelectedValue + "'and DesignationId='" + dropDesignation.SelectedValue + "'";
            DataTable dtdesignation = SqlHelper.Instance.GetTableByQuery(desigquery);
            if (dtdesignation.Rows.Count > 0)
            {
                txtBasic.Text = dtdesignation.Rows[0]["Basic"].ToString();
                txtDA.Text = dtdesignation.Rows[0]["DA"].ToString();
                txtHRA.Text = dtdesignation.Rows[0]["HRA"].ToString();
                txtCCA.Text = dtdesignation.Rows[0]["CCA"].ToString();
                txtconv.Text = dtdesignation.Rows[0]["Conveyence"].ToString();
                txtLA.Text = dtdesignation.Rows[0]["LA"].ToString();
                txtGratuity.Text = dtdesignation.Rows[0]["Gratuity"].ToString();
                txtBonus.Text = dtdesignation.Rows[0]["Bonus"].ToString();
                txtWA.Text = dtdesignation.Rows[0]["WA"].ToString();
                txtOA.Text = dtdesignation.Rows[0]["OA"].ToString();
                txtNFHS.Text = dtdesignation.Rows[0]["NFHS"].ToString();
                txtRC.Text = dtdesignation.Rows[0]["RC"].ToString();
                txtFoodAllowance.Text = dtdesignation.Rows[0]["FoodAllowance"].ToString();
                txtMedicalAllowance.Text = dtdesignation.Rows[0]["MedicalAllowance"].ToString();
                txtTravelAllowance.Text = dtdesignation.Rows[0]["TravelAllowance"].ToString();
                txtPerfomanceAllowance.Text = dtdesignation.Rows[0]["PerformanceAllowance"].ToString();
                txtMobileAllowance.Text = dtdesignation.Rows[0]["MoboleAllowance"].ToString();
                txtPF.Text = "100";
                txtEsi.Text = "100";
                lblresult.Text = dtdesignation.Rows[0]["TotalAmount"].ToString();
                txtschargeamount.Text = dtdesignation.Rows[0]["servicecharge"].ToString();
                txtcgst.Text = dtdesignation.Rows[0]["CGSTper"].ToString();
                txtsgstper.Text = dtdesignation.Rows[0]["SGSTper"].ToString();
                txtigst.Text = dtdesignation.Rows[0]["IGSTper"].ToString();
                txtcgstamount.Text = dtdesignation.Rows[0]["CGSTamount"].ToString();
                txtsgstamount.Text = dtdesignation.Rows[0]["SGSTamount"].ToString();
                txtigstamount.Text = dtdesignation.Rows[0]["IGSTamount"].ToString();
                txtGrandTotal.Text = dtdesignation.Rows[0]["GrandTotal"].ToString();
                txtschargeprc.Text = dtdesignation.Rows[0]["servicechargeper"].ToString();
                lblresult.Visible = true;

                #region code for amount checkboxes retrieving

                string chkbasic = dtdesignation.Rows[0]["chkbasic"].ToString();
                if (chkbasic == "True")
                {
                    chkba.Checked = true;
                }
                else
                {
                    chkba.Checked = false;
                }
                string checkda = dtdesignation.Rows[0]["chkda"].ToString();
                if (checkda == "True")
                {
                    chkda.Checked = true;
                }
                else
                {
                    chkda.Checked = false;
                }
                string checkhra = dtdesignation.Rows[0]["chkhra"].ToString();
                if (checkhra == "True")
                {
                    chkhra.Checked = true;
                }
                else
                {
                    chkhra.Checked = false;
                }
                string checkconvey = dtdesignation.Rows[0]["chkconvey"].ToString();
                if (checkconvey == "True")
                {
                    chkconv.Checked = true;
                }
                else
                {
                    chkconv.Checked = false;
                }
                string checkcca = dtdesignation.Rows[0]["chkcca"].ToString();
                if (checkcca == "True")
                {
                    chkcca.Checked = true;
                }
                else
                {
                    chkcca.Checked = false;
                }
                string checkbonus = dtdesignation.Rows[0]["chkbonus"].ToString();
                if (checkbonus == "True")
                {
                    chkbonus.Checked = true;
                }
                else
                {
                    chkbonus.Checked = false;
                }
                string checkgratuity = dtdesignation.Rows[0]["chkgratuity"].ToString();
                if (checkgratuity == "True")
                {
                    chkgratuity.Checked = true;
                }
                else
                {
                    chkgratuity.Checked = false;
                }
                string checkla = dtdesignation.Rows[0]["chkla"].ToString();
                if (checkla == "True")
                {
                    chkla.Checked = true;
                }
                else
                {
                    chkla.Checked = false;
                }
                string checknfhs = dtdesignation.Rows[0]["chknfhs"].ToString();
                if (checknfhs == "True")
                {
                    chknfhs.Checked = true;
                }
                else
                {
                    chknfhs.Checked = false;
                }
                string checkrc = dtdesignation.Rows[0]["chkrc"].ToString();
                if (checkrc == "True")
                {
                    chkrc.Checked = true;
                }
                else
                {
                    chkrc.Checked = false;
                }
                string checkwa = dtdesignation.Rows[0]["chkwa"].ToString();
                if (checkwa == "True")
                {
                    chkwa.Checked = true;
                }
                else
                {
                    chkwa.Checked = false;
                }
                string checkoa = dtdesignation.Rows[0]["chkoa"].ToString();
                if (checkoa == "True")
                {
                    chkoa.Checked = true;
                }
                else
                {
                    chkoa.Checked = false;
                }
                string checkfoodallw = dtdesignation.Rows[0]["chkfoodallw"].ToString();
                if (checkfoodallw == "True")
                {
                    chkfoodallw.Checked = true;
                }
                else
                {
                    chkfoodallw.Checked = false;
                }
                string checkmedicalallow = dtdesignation.Rows[0]["chkmedicalallow"].ToString();
                if (checkmedicalallow == "True")
                {
                    chkmedicalaalw.Checked = true;
                }
                else
                {
                    chkmedicalaalw.Checked = false;
                }
                string checktravelallw = dtdesignation.Rows[0]["chktravelallw"].ToString();
                if (checktravelallw == "True")
                {
                    chktravelallw.Checked = true;
                }
                else
                {
                    chktravelallw.Checked = false;
                }
                string chkperform = dtdesignation.Rows[0]["chkperform"].ToString();
                if (chkperform == "True")
                {
                    chkperfmallw.Checked = true;
                }
                else
                {
                    chkperfmallw.Checked = false;
                }
                string checkmobileallw = dtdesignation.Rows[0]["chkmobileallw"].ToString();
                if (checkmobileallw == "True")
                {
                    chkmobileallw.Checked = true;
                }
                else
                {
                    chkmobileallw.Checked = false;
                }
                string checkpf = dtdesignation.Rows[0]["chkpf"].ToString();
                if (checkpf == "True")
                {
                    chkpf.Checked = true;
                }
                else
                {
                    chkpf.Checked = false;
                }
                string checkesi = dtdesignation.Rows[0]["chkesi"].ToString();
                if (checkesi == "True")
                {
                    chkesi.Checked = true;
                }
                else
                {
                    chkesi.Checked = false;
                }
                string checkservicecharge = dtdesignation.Rows[0]["chkservicecharge"].ToString();
                if (checkservicecharge == "True")
                {
                    chkservicecharge.Checked = true;
                }
                else
                {
                    chkservicecharge.Checked = false;
                }
                string cgst = dtdesignation.Rows[0]["chkcgst"].ToString();
                if (cgst == "True")
                {
                    chkcgst.Checked = true;
                }
                else
                {
                    chkcgst.Checked = false;
                }
                string sgst = dtdesignation.Rows[0]["chksgst"].ToString();
                if (sgst == "True")
                {
                    chkSgst.Checked = true;
                }
                else
                {
                    chkSgst.Checked = false;
                }
                string igst = dtdesignation.Rows[0]["chkigst"].ToString();
                if (igst == "True")
                {
                    chkigst.Checked = true;
                }
                else
                {
                    chkigst.Checked = false;
                }
                #endregion code for amount checkboxes retrieving

                #region code for pf checkboxes retrieving
                string chkbapf = dtdesignation.Rows[0]["chkbapf"].ToString();
                if (chkbapf == "True")
                {
                    checkbapf.Checked = true;
                }
                else
                {
                    checkbapf.Checked = false;
                }
                string chkdapf = dtdesignation.Rows[0]["chkdapf"].ToString();
                if (chkdapf == "True")
                {
                    checkdapf.Checked = true;
                }
                else
                {
                    checkdapf.Checked = false;
                }
                string chkhrapf = dtdesignation.Rows[0]["chkhrapf"].ToString();
                if (chkhrapf == "True")
                {
                    checkhrapf.Checked = true;
                }
                else
                {
                    checkhrapf.Checked = false;
                }
                string chkconveypf = dtdesignation.Rows[0]["chkconveypf"].ToString();
                if (chkconveypf == "True")
                {
                    checkconvpf.Checked = true;
                }
                else
                {
                    checkconvpf.Checked = false;
                }
                string chkccapf = dtdesignation.Rows[0]["chkccapf"].ToString();
                if (chkccapf == "True")
                {
                    checkccapf.Checked = true;
                }
                else
                {
                    checkccapf.Checked = false;
                }
                string chkbonuspf = dtdesignation.Rows[0]["chkbonuspf"].ToString();
                if (chkbonuspf == "True")
                {
                    checkbonuspf.Checked = true;
                }
                else
                {
                    checkbonuspf.Checked = false;
                }
                string chkgratuitypf = dtdesignation.Rows[0]["chkgratuitypf"].ToString();
                if (chkgratuitypf == "True")
                {
                    checkgratuitypf.Checked = true;
                }
                else
                {
                    checkgratuitypf.Checked = false;
                }
                string chklapf = dtdesignation.Rows[0]["chklapf"].ToString();
                if (chklapf == "True")
                {
                    checklapf.Checked = true;
                }
                else
                {
                    checklapf.Checked = false;
                }
                string chknfhspf = dtdesignation.Rows[0]["chknfhspf"].ToString();
                if (chknfhspf == "True")
                {
                    checknfhspf.Checked = true;
                }
                else
                {
                    checknfhspf.Checked = false;
                }
                string chkrcpf = dtdesignation.Rows[0]["chkrcpf"].ToString();
                if (chkrcpf == "True")
                {
                    checkrcpf.Checked = true;
                }
                else
                {
                    checkrcpf.Checked = false;
                }
                string chkwapf = dtdesignation.Rows[0]["chkwapf"].ToString();
                if (chkwapf == "True")
                {
                    checkwapf.Checked = true;
                }
                else
                {
                    checkwapf.Checked = false;
                }
                string chkoapf = dtdesignation.Rows[0]["chkoapf"].ToString();
                if (chkoapf == "True")
                {
                    checkoapf.Checked = true;
                }
                else
                {
                    checkoapf.Checked = false;
                }
                string chkfoodallwpf = dtdesignation.Rows[0]["chkfoodallwpf"].ToString();
                if (chkfoodallwpf == "True")
                {
                    checkfoodpf.Checked = true;
                }
                else
                {
                    checkfoodpf.Checked = false;
                }
                string chkmedicalallwpf = dtdesignation.Rows[0]["chkmedicalallwpf"].ToString();
                if (chkmedicalallwpf == "True")
                {
                    checkmedicalpf.Checked = true;
                }
                else
                {
                    checkmedicalpf.Checked = false;
                }
                string chktravelallwpf = dtdesignation.Rows[0]["chktravelallwpf"].ToString();
                if (chktravelallwpf == "True")
                {
                    checktravelpf.Checked = true;
                }
                else
                {
                    checktravelpf.Checked = false;
                }
                string chkperfmallwpf = dtdesignation.Rows[0]["chkperfmallwpf"].ToString();
                if (chkperfmallwpf == "True")
                {
                    checkperfmpf.Checked = true;
                }
                else
                {
                    checkperfmpf.Checked = false;
                }
                string chkmobileallwpf = dtdesignation.Rows[0]["chkmobileallwpf"].ToString();
                if (chkmobileallwpf == "True")
                {
                    checkmobilepf.Checked = true;
                }
                else
                {
                    checkmobilepf.Checked = false;
                }
                #endregion code for pf checkboxes retrieving

                #region code for ESI checkboxes retrieving
                string chkbaesi = dtdesignation.Rows[0]["chkbaesi"].ToString();
                if (chkbaesi == "True")
                {
                    checkbaesi.Checked = true;
                }
                else
                {
                    checkbaesi.Checked = false;
                }
                string chkdaesi = dtdesignation.Rows[0]["chkdaesi"].ToString();
                if (chkdaesi == "True")
                {
                    checkdaesi.Checked = true;
                }
                else
                {
                    checkdaesi.Checked = false;
                }
                string chkhraesi = dtdesignation.Rows[0]["chkhraesi"].ToString();
                if (chkhraesi == "True")
                {
                    checkhraesi.Checked = true;
                }
                else
                {
                    checkhraesi.Checked = false;
                }
                string chkconveyesi = dtdesignation.Rows[0]["chkconveyesi"].ToString();
                if (chkconveyesi == "True")
                {
                    checkconvesi.Checked = true;
                }
                else
                {
                    checkconvesi.Checked = false;
                }
                string chkccaesi = dtdesignation.Rows[0]["chkccaesi"].ToString();
                if (chkccaesi == "True")
                {
                    checkccaesi.Checked = true;
                }
                else
                {
                    checkccaesi.Checked = false;
                }
                string chkbonusesi = dtdesignation.Rows[0]["chkbonusesi"].ToString();
                if (chkbonusesi == "True")
                {
                    checkbonusesi.Checked = true;
                }
                else
                {
                    checkbonusesi.Checked = false;
                }
                string chkgratuityesi = dtdesignation.Rows[0]["chkgratuityesi"].ToString();
                if (chkgratuityesi == "True")
                {
                    checkgratuityesi.Checked = true;
                }
                else
                {
                    checkgratuityesi.Checked = false;
                }
                string chklaesi = dtdesignation.Rows[0]["chklaesi"].ToString();
                if (chklaesi == "True")
                {
                    checklaesi.Checked = true;
                }
                else
                {
                    checklaesi.Checked = false;
                }
                string chknfhsesi = dtdesignation.Rows[0]["chknfhsesi"].ToString();
                if (chknfhsesi == "True")
                {
                    checknfhsesi.Checked = true;
                }
                else
                {
                    checknfhsesi.Checked = false;
                }
                string chkrcesi = dtdesignation.Rows[0]["chkrcesi"].ToString();
                if (chkrcesi == "True")
                {
                    checkrcesi.Checked = true;
                }
                else
                {
                    checkrcesi.Checked = false;
                }
                string chkwaesi = dtdesignation.Rows[0]["chkwaesi"].ToString();
                if (chkwaesi == "True")
                {
                    checkwaesi.Checked = true;
                }
                else
                {
                    checkwaesi.Checked = false;
                }
                string chkoaesi = dtdesignation.Rows[0]["chkoaesi"].ToString();
                if (chkoaesi == "True")
                {
                    checkoaesi.Checked = true;
                }
                else
                {
                    checkoaesi.Checked = false;
                }
                string chkfoodallwesi = dtdesignation.Rows[0]["chkfoodallwesi"].ToString();
                if (chkfoodallwesi == "True")
                {
                    checkfoodesi.Checked = true;
                }
                else
                {
                    checkfoodesi.Checked = false;
                }
                string chkmedicalallwesi = dtdesignation.Rows[0]["chkmedicalallwesi"].ToString();
                if (chkmedicalallwesi == "True")
                {
                    checkmedicalesi.Checked = true;
                }
                else
                {
                    checkmedicalesi.Checked = false;
                }
                string chktravelallwesi = dtdesignation.Rows[0]["chktravelallwesi"].ToString();
                if (chktravelallwesi == "True")
                {
                    checktravelesi.Checked = true;
                }
                else
                {
                    checktravelesi.Checked = false;
                }
                string chkperfmallwesi = dtdesignation.Rows[0]["chkperfmallwesi"].ToString();
                if (chkperfmallwesi == "True")
                {
                    checkperfmesi.Checked = true;
                }
                else
                {
                    checkperfmesi.Checked = false;
                }
                string chkmobileallwesi = dtdesignation.Rows[0]["chkmobileallwesi"].ToString();
                if (chkmobileallwesi == "True")
                {
                    checkmobileesi.Checked = true;
                }
                else
                {
                    checkmobileesi.Checked = false;
                }
                #endregion code for ESI checkboxes retrieving

                #region code for SC checkboxes retrieving
                string chkbasc = dtdesignation.Rows[0]["chkbasc"].ToString();
                if (chkbasc == "True")
                {
                    checkbasc.Checked = true;
                }
                else
                {
                    checkbasc.Checked = false;
                }
                string chkdasc = dtdesignation.Rows[0]["chkdasc"].ToString();
                if (chkdasc == "True")
                {
                    checkdasc.Checked = true;
                }
                else
                {
                    checkdasc.Checked = false;
                }
                string chkhrasc = dtdesignation.Rows[0]["chkhrasc"].ToString();
                if (chkhrasc == "True")
                {
                    checkhrasc.Checked = true;
                }
                else
                {
                    checkhrasc.Checked = false;
                }
                string chkconveysc = dtdesignation.Rows[0]["chkconveysc"].ToString();
                if (chkconveysc == "True")
                {
                    checkconvsc.Checked = true;
                }
                else
                {
                    checkconvsc.Checked = false;
                }
                string chkccasc = dtdesignation.Rows[0]["chkccasc"].ToString();
                if (chkccasc == "True")
                {
                    checkccasc.Checked = true;
                }
                else
                {
                    checkccasc.Checked = false;
                }
                string chkbonussc = dtdesignation.Rows[0]["chkbonussc"].ToString();
                if (chkbonussc == "True")
                {
                    checkbonussc.Checked = true;
                }
                else
                {
                    checkbonussc.Checked = false;
                }
                string chkgratuitysc = dtdesignation.Rows[0]["chkgratuitysc"].ToString();
                if (chkgratuitysc == "True")
                {
                    checkgratuitysc.Checked = true;
                }
                else
                {
                    checkgratuitysc.Checked = false;
                }
                string chklasc = dtdesignation.Rows[0]["chklasc"].ToString();
                if (chklasc == "True")
                {
                    checklasc.Checked = true;
                }
                else
                {
                    checklasc.Checked = false;
                }
                string chknfhssc = dtdesignation.Rows[0]["chknfhssc"].ToString();
                if (chknfhssc == "True")
                {
                    checknfhssc.Checked = true;
                }
                else
                {
                    checknfhssc.Checked = false;
                }
                string chkrcsc = dtdesignation.Rows[0]["chkrcsc"].ToString();
                if (chkrcsc == "True")
                {
                    checkrcsc.Checked = true;
                }
                else
                {
                    checkrcsc.Checked = false;
                }

                string chkwasc = dtdesignation.Rows[0]["chkwasc"].ToString();
                if (chkwasc == "True")
                {
                    checkwasc.Checked = true;
                }
                else
                {
                    checkwasc.Checked = false;
                }
                string chkoasc = dtdesignation.Rows[0]["chkoasc"].ToString();
                if (chkoasc == "True")
                {
                    checkoasc.Checked = true;
                }
                else
                {
                    checkoasc.Checked = false;
                }
                string chkfoodallwsc = dtdesignation.Rows[0]["chkfoodallwsc"].ToString();
                if (chkfoodallwsc == "True")
                {
                    checkfoodsc.Checked = true;
                }
                else
                {
                    checkfoodsc.Checked = false;
                }
                string chkmedicalallwsc = dtdesignation.Rows[0]["chkmedicalallwsc"].ToString();
                if (chkmedicalallwsc == "True")
                {
                    checkmedicalsc.Checked = true;
                }
                else
                {
                    checkmedicalsc.Checked = false;
                }
                string chktravelallwsc = dtdesignation.Rows[0]["chktravelallwsc"].ToString();
                if (chktravelallwsc == "True")
                {
                    checktravelsc.Checked = true;
                }
                else
                {
                    checktravelsc.Checked = false;
                }
                string chkperfmallwsc = dtdesignation.Rows[0]["chkperfmallwsc"].ToString();
                if (chkperfmallwsc == "True")
                {
                    checkperfmsc.Checked = true;
                }
                else
                {
                    checkperfmsc.Checked = false;
                }
                string chkmobileallwsc = dtdesignation.Rows[0]["chkmobileallwsc"].ToString();
                if (chkmobileallwsc == "True")
                {
                    checkmobilesc.Checked = true;
                }
                else
                {
                    checkmobilesc.Checked = false;
                }
                #endregion code for SC checkboxes retrieving
            }
        }

        protected void btnGetvalues_Click(object sender, EventArgs e)
        {
            txtschargeamount.Text = string.Empty;
            txtschargeprc.Text = string.Empty;

            if (ddlstate.SelectedIndex == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Select State')", true);
                return;
            }
            if (dropZone.SelectedIndex == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Select Zone')", true);
                return;
            }
            if (dropType.SelectedIndex == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Select Type')", true);
                return;
            }
            if (dropcategory.SelectedIndex == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Select Category')", true);
                return;
            }


            string SPName = "GetValues";
            string Type = "GetLeadAmts";
            Hashtable ht = new Hashtable();
            ht.Add("@State", ddlstate.SelectedValue);
            ht.Add("@type", dropType.SelectedValue);
            ht.Add("@Zone", dropZone.SelectedValue);
            ht.Add("@category", dropcategory.SelectedValue);
            ht.Add("@Designation", dropDesignation.SelectedValue);
            ht.Add("@SPType", Type);
            DataTable dtleadamount = MH.CentralExecuteAdaptorAsyncWithSPParams(SPName, ht);

            if (dtleadamount.Rows.Count > 0)
            {
                txtBasic.Text = dtleadamount.Rows[0]["Basic"].ToString();
                txtDA.Text = dtleadamount.Rows[0]["DA"].ToString();
                txtHRA.Text = dtleadamount.Rows[0]["HRA"].ToString();
                txtCCA.Text = dtleadamount.Rows[0]["CCA"].ToString();
                txtconv.Text = dtleadamount.Rows[0]["Conveyence"].ToString();
                txtLA.Text = dtleadamount.Rows[0]["LA"].ToString();
                txtGratuity.Text = dtleadamount.Rows[0]["Gratuity"].ToString();
                txtBonus.Text = dtleadamount.Rows[0]["Bonus"].ToString();
                txtWA.Text = dtleadamount.Rows[0]["WA"].ToString();
                txtOA.Text = dtleadamount.Rows[0]["OA"].ToString();
                txtNFHS.Text = dtleadamount.Rows[0]["NFHS"].ToString();
                txtRC.Text = dtleadamount.Rows[0]["RC"].ToString();
                txtFoodAllowance.Text = dtleadamount.Rows[0]["FoodAllowance"].ToString();
                txtMedicalAllowance.Text = dtleadamount.Rows[0]["MedicalAllowance"].ToString();
                txtTravelAllowance.Text = dtleadamount.Rows[0]["TravelAllowance"].ToString();
                txtPerfomanceAllowance.Text = dtleadamount.Rows[0]["PerformanceAllowance"].ToString();
                txtMobileAllowance.Text = dtleadamount.Rows[0]["MoboleAllowance"].ToString();
                txtcgst.Text = dtleadamount.Rows[0]["CGST"].ToString();
                txtsgstper.Text = dtleadamount.Rows[0]["SGST"].ToString();
                txtigst.Text = dtleadamount.Rows[0]["IGST"].ToString();

            }
            else
            {
                GetValuesCleardata();
            }

        }



        public void ResetValues()
        {
            #region Code for Amounts

            GVLeadsamount.DataSource = null;
            GVLeadsamount.DataBind();

            float fulltotalamount = 0;
            float fulltotalservicecharge = 0;
            float fullgst = 0;
            float fullgrandtotal = 0;
            float fullbasictotal = 0;
            float fullDAtotal = 0;
            float fullHRA = 0;
            float fullConv = 0;
            float fullCCA = 0;
            float fullLA = 0;
            float fullGratuity = 0;
            float fullbonus = 0;
            float fullOA = 0;
            float fullWA = 0;
            float fullNFHS = 0;
            float fullRC = 0;
            float fullfoodallw = 0;
            float fulltarvelallw = 0;
            float fullperfallw = 0;
            float fullmobileallw = 0;
            float fullPF = 0;
            float fullESI = 0;
            float fullcgst = 0;
            float fullsgst = 0;
            float fulligst = 0;
            #endregion Code for amounts
        }

        #region Code for Amounts
        float fulltotalamount = 0;
        float fulltotalservicecharge = 0;
        float fullgst = 0;
        float fullgrandtotal = 0;
        float fullbasictotal = 0;
        float fullDAtotal = 0;
        float fullHRA = 0;
        float fullConv = 0;
        float fullCCA = 0;
        float fullLA = 0;
        float fullGratuity = 0;
        float fullbonus = 0;
        float fullOA = 0;
        float fullWA = 0;
        float fullNFHS = 0;
        float fullRC = 0;
        float fullfoodallw = 0;
        float fulltarvelallw = 0;
        float fullperfallw = 0;
        float fullmobileallw = 0;
        float fullPF = 0;
        float fullESI = 0;
        float fullcgst = 0;
        float fullsgst = 0;
        float fulligst = 0;
        #endregion Code for amounts


        protected void GVLeadsamount_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                float totalamount = float.Parse(((Label)e.Row.FindControl("lblresult")).Text);
                fulltotalamount += totalamount;
                float totalservicecharge = float.Parse(((Label)e.Row.FindControl("lblscharge")).Text);
                fulltotalservicecharge += totalservicecharge;
                //float totalgst = float.Parse(((Label)e.Row.FindControl("lblgst")).Text);
                //fullgst += totalgst;
                float grandtotal = float.Parse(((Label)e.Row.FindControl("lblgrandtotal")).Text);
                fullgrandtotal += grandtotal;
                float basictotal = float.Parse(((Label)e.Row.FindControl("lblbasicamount")).Text);
                fullbasictotal += basictotal;
                float totalDA = float.Parse(((Label)e.Row.FindControl("lblda")).Text);
                fullDAtotal += totalDA;
                float HRAtotal = float.Parse(((Label)e.Row.FindControl("lblhra")).Text);
                fullHRA += HRAtotal;
                float Covtotal = float.Parse(((Label)e.Row.FindControl("lblconveyence")).Text);
                fullConv += Covtotal;
                float totalCCA = float.Parse(((Label)e.Row.FindControl("lblcca")).Text);
                fullCCA += totalCCA;
                float totalLA = float.Parse(((Label)e.Row.FindControl("lblLA")).Text);
                fullLA += totalLA;
                float totalgratuity = float.Parse(((Label)e.Row.FindControl("lblgratuity")).Text);
                fullGratuity += totalgratuity;
                float bonustotal = float.Parse(((Label)e.Row.FindControl("lblbonus")).Text);
                fullbonus += bonustotal;
                float totalWA = float.Parse(((Label)e.Row.FindControl("lblWA")).Text);
                fullWA += totalWA;
                float OAtotal = float.Parse(((Label)e.Row.FindControl("lblOA")).Text);
                fullOA += OAtotal;
                float totalNFHS = float.Parse(((Label)e.Row.FindControl("lblnfhs")).Text);
                fullNFHS += totalNFHS;
                float totalRC = float.Parse(((Label)e.Row.FindControl("lblRc")).Text);
                fullRC += totalRC;
                float totalfoodallw = float.Parse(((Label)e.Row.FindControl("lblFAW")).Text);
                fullfoodallw += totalfoodallw;
                float travelallwtotal = float.Parse(((Label)e.Row.FindControl("lblTA")).Text);
                fulltarvelallw += bonustotal;
                float totalperfallw = float.Parse(((Label)e.Row.FindControl("lblpfa")).Text);
                fullperfallw += totalperfallw;
                float totalmobileallw = float.Parse(((Label)e.Row.FindControl("lblMA")).Text);
                fullmobileallw += totalmobileallw;
                float travelpfamount = float.Parse(((Label)e.Row.FindControl("lblPF")).Text);
                fullPF += travelpfamount;
                float totalESI = float.Parse(((Label)e.Row.FindControl("lblESi")).Text);
                fullESI += totalESI;
                float totalcgst = float.Parse(((Label)e.Row.FindControl("lblgridcgst")).Text);
                fullcgst += totalcgst;
                float totalsgst = float.Parse(((Label)e.Row.FindControl("lblgridsgst")).Text);
                fullsgst += totalsgst;
                float totaligst = float.Parse(((Label)e.Row.FindControl("lblgridigst")).Text);
                fulligst += totaligst;
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                ((Label)e.Row.FindControl("lblTotalAmount")).Text = fulltotalamount.ToString();
                ((Label)e.Row.FindControl("lblschargeamount")).Text = fulltotalservicecharge.ToString();
                //((Label)e.Row.FindControl("lblgstamunt")).Text = fullgst.ToString();
                ((Label)e.Row.FindControl("lblgrandtotalamount")).Text = fullgrandtotal.ToString();
                ((Label)e.Row.FindControl("lblbasictotal")).Text = fullbasictotal.ToString();
                ((Label)e.Row.FindControl("lbldatotal")).Text = fullDAtotal.ToString();
                ((Label)e.Row.FindControl("lblhratotal")).Text = fullHRA.ToString();
                ((Label)e.Row.FindControl("lblconvtotal")).Text = fullConv.ToString();
                ((Label)e.Row.FindControl("lblccatotal")).Text = fullCCA.ToString();
                ((Label)e.Row.FindControl("lbllatotal")).Text = fullLA.ToString();
                ((Label)e.Row.FindControl("lblgratuitytotal")).Text = fullGratuity.ToString();
                ((Label)e.Row.FindControl("lblbonustotal")).Text = fullbonus.ToString();
                ((Label)e.Row.FindControl("lblWAtotal")).Text = fullWA.ToString();
                ((Label)e.Row.FindControl("lbloatotal")).Text = fullOA.ToString();
                ((Label)e.Row.FindControl("lblnfhstotal")).Text = fullNFHS.ToString();
                ((Label)e.Row.FindControl("lblrctotal")).Text = fullRC.ToString();
                ((Label)e.Row.FindControl("lblfawtotal")).Text = fullfoodallw.ToString();
                ((Label)e.Row.FindControl("lbtatotal")).Text = fulltarvelallw.ToString();
                ((Label)e.Row.FindControl("lblpfatotal")).Text = fullperfallw.ToString();
                ((Label)e.Row.FindControl("lblmatotal")).Text = fullmobileallw.ToString();
                ((Label)e.Row.FindControl("lblpftotal")).Text = fullPF.ToString();
                ((Label)e.Row.FindControl("lblesitotal")).Text = fullESI.ToString();
                ((Label)e.Row.FindControl("lblcgstamunt")).Text = fullcgst.ToString();
                ((Label)e.Row.FindControl("lblsgstamunt")).Text = fullsgst.ToString();
                ((Label)e.Row.FindControl("lbligstamunt")).Text = fulligst.ToString();
            }
        }

        public void Loadcomponenets()
        {

            string sourcequery = "select * from M_LeadComponentMaster";
            DataTable dtsourceofhead = SqlHelper.Instance.GetTableByQuery(sourcequery);
            if (dtsourceofhead.Rows.Count > 0)
            {
                centarlbasic.Text = dtsourceofhead.Rows[0]["ComponentName"].ToString();
                string centralbasicvis = dtsourceofhead.Rows[0]["visibility"].ToString();
                if (centralbasicvis == "y")
                {
                    trbasic.Visible = true;
                }
                else
                {
                    trbasic.Visible = false;
                }


                centralDA.Text = dtsourceofhead.Rows[1]["ComponentName"].ToString();
                string centraldavis = dtsourceofhead.Rows[1]["visibility"].ToString();
                if (centraldavis == "y")
                {
                    trDA.Visible = true;
                }
                else
                {
                    trDA.Visible = false;
                }

                centarlhra.Text = dtsourceofhead.Rows[2]["ComponentName"].ToString();
                string centralhravis = dtsourceofhead.Rows[2]["visibility"].ToString();
                if (centralhravis == "y")
                {
                    trhra.Visible = true;
                }
                else
                {
                    trhra.Visible = false;
                }

                centralconv.Text = dtsourceofhead.Rows[3]["ComponentName"].ToString();
                string centralconvvis = dtsourceofhead.Rows[3]["visibility"].ToString();
                if (centralconvvis == "y")
                {
                    trconv.Visible = true;
                }
                else
                {
                    trconv.Visible = false;
                }
                centarlcca.Text = dtsourceofhead.Rows[4]["ComponentName"].ToString();
                string centralccavis = dtsourceofhead.Rows[4]["visibility"].ToString();
                if (centralccavis == "y")
                {
                    trcca.Visible = true;
                }
                else
                {
                    trcca.Visible = false;
                }
                centralLA.Text = dtsourceofhead.Rows[5]["ComponentName"].ToString();
                string centrallavis = dtsourceofhead.Rows[5]["visibility"].ToString();
                if (centrallavis == "y")
                {
                    trla.Visible = true;
                }
                else
                {
                    trla.Visible = false;
                }

                centralgratuity.Text = dtsourceofhead.Rows[6]["ComponentName"].ToString();
                string centralgratuityvis = dtsourceofhead.Rows[6]["visibility"].ToString();
                if (centralgratuityvis == "y")
                {
                    trgratuity.Visible = true;
                }
                else
                {
                    trgratuity.Visible = false;
                }
                centralbonus.Text = dtsourceofhead.Rows[7]["ComponentName"].ToString();
                string centralbonusvis = dtsourceofhead.Rows[7]["visibility"].ToString();
                if (centralbonusvis == "y")
                {
                    trbonus.Visible = true;
                }
                else
                {
                    trbonus.Visible = false;
                }
                centralwa.Text = dtsourceofhead.Rows[8]["ComponentName"].ToString();
                string centralwavis = dtsourceofhead.Rows[8]["visibility"].ToString();
                if (centralwavis == "y")
                {
                    trwa.Visible = true;
                }
                else
                {
                    trwa.Visible = false;
                }
                centraloa.Text = dtsourceofhead.Rows[9]["ComponentName"].ToString();
                string centraloavis = dtsourceofhead.Rows[9]["visibility"].ToString();
                if (centraloavis == "y")
                {
                    troa.Visible = true;
                }
                else
                {
                    troa.Visible = false;
                }

                centralnfhs.Text = dtsourceofhead.Rows[10]["ComponentName"].ToString();
                string centralnfhsvis = dtsourceofhead.Rows[10]["visibility"].ToString();
                if (centralnfhsvis == "y")
                {
                    trnfhs.Visible = true;
                }
                else
                {
                    trnfhs.Visible = false;
                }
                centralrc.Text = dtsourceofhead.Rows[11]["ComponentName"].ToString();
                string centralrcvis = dtsourceofhead.Rows[11]["visibility"].ToString();
                if (centralrcvis == "y")
                {
                    trrc.Visible = true;
                }
                else
                {
                    trrc.Visible = false;
                }
                centralfoodallw.Text = dtsourceofhead.Rows[12]["ComponentName"].ToString();
                string centralfoodallwvis = dtsourceofhead.Rows[12]["visibility"].ToString();
                if (centralfoodallwvis == "y")
                {
                    trfoodallw.Visible = true;
                }
                else
                {
                    trfoodallw.Visible = false;
                }

                centralmedicalallw.Text = dtsourceofhead.Rows[13]["ComponentName"].ToString();
                string centralmedicalallwvis = dtsourceofhead.Rows[13]["visibility"].ToString();
                if (centralmedicalallwvis == "y")
                {
                    trmedicallw.Visible = true;
                }
                else
                {
                    trmedicallw.Visible = false;
                }
                centraltravelallw.Text = dtsourceofhead.Rows[14]["ComponentName"].ToString();
                string centraltravelallwvis = dtsourceofhead.Rows[14]["visibility"].ToString();
                if (centraltravelallwvis == "y")
                {
                    trtravelallw.Visible = true;
                }
                else
                {
                    trtravelallw.Visible = false;
                }
                centralperformallw.Text = dtsourceofhead.Rows[15]["ComponentName"].ToString();
                string centralperfmallwvis = dtsourceofhead.Rows[15]["visibility"].ToString();
                if (centralperfmallwvis == "y")
                {
                    trperformallw.Visible = true;
                }
                else
                {
                    trperformallw.Visible = false;
                }
                centraltxtmobileallw.Text = dtsourceofhead.Rows[16]["ComponentName"].ToString();
                string centralmobileallwvis = dtsourceofhead.Rows[16]["visibility"].ToString();
                if (centralmobileallwvis == "y")
                {
                    trmobileallw.Visible = true;
                }
                else
                {
                    trmobileallw.Visible = false;
                }
                centralpf.Text = dtsourceofhead.Rows[17]["ComponentName"].ToString();
                string centralpfvis = dtsourceofhead.Rows[17]["visibility"].ToString();
                if (centralpfvis == "y")
                {
                    trpf.Visible = true;
                }
                else
                {
                    trpf.Visible = false;
                }
                centralesi.Text = dtsourceofhead.Rows[18]["ComponentName"].ToString();
                string centralesivis = dtsourceofhead.Rows[18]["visibility"].ToString();
                if (centralesivis == "y")
                {
                    tresi.Visible = true;
                }
                else
                {
                    tresi.Visible = false;
                }
                //centraltotal.Text = dtsourceofhead.Rows[19]["ComponentName"].ToString();
                centralservicecharge.Text = dtsourceofhead.Rows[20]["ComponentName"].ToString();
                //centralgst.Text = dtsourceofhead.Rows[21]["ComponentName"].ToString();
                //centralgrandtotal.Text = dtsourceofhead.Rows[22]["ComponentName"].ToString();
            }

        }
        protected void chkigst_CheckedChanged(object sender, EventArgs e)
        {
            if (chkigst.Checked == true)
            {
                chkcgst.Checked = false;
                chkSgst.Checked = false;
            }
            else
            {
                chkcgst.Checked = true;
                chkSgst.Checked = true;

            }
        }
        public void PfEsiCalculation()
        {

            #region Checkboxes initialisation

            var chkbapf = 0;
            var chkdapf = 0;
            var chkhrapf = 0;
            var chkconveypf = 0;
            var chkccapf = 0;
            var chkbonuspf = 0;
            var chkgratuitypf = 0;
            var chklapf = 0;
            var chknfhspf = 0;
            var chkrcpf = 0;
            var chkwapf = 0;
            var chkoapf = 0;
            var chkfoodallwpf = 0;
            var chkmedicalallwpf = 0;
            var chktravelallwpf = 0;
            var chkperfmallwpf = 0;
            var chkmobileallwpf = 0;
            var chkbaesi = 0;
            var chkdaesi = 0;
            var chkhraesi = 0;
            var chkconveyesi = 0;
            var chkccaesi = 0;
            var chkbonusesi = 0;
            var chkgratuityesi = 0;
            var chklaesi = 0;
            var chknfhsesi = 0;
            var chkrcesi = 0;
            var chkwaesi = 0;
            var chkoaesi = 0;
            var chkfoodallwesi = 0;
            var chkmedicalallwesi = 0;
            var chktravelallwesi = 0;
            var chkperfmallwesi = 0;
            var chkmobileallwesi = 0;
            var chkbasc = 0;
            var chkdasc = 0;
            var chkhrasc = 0;
            var chkconveysc = 0;
            var chkccasc = 0;
            var chkbonussc = 0;
            var chkgratuitysc = 0;
            var chklasc = 0;
            var chknfhssc = 0;
            var chkrcsc = 0;
            var chkwasc = 0;
            var chkoasc = 0;
            var chkfoodallwsc = 0;
            var chkmedicalallwsc = 0;
            var chktravelallwsc = 0;
            var chkperfmallwsc = 0;
            var chkmobileallwsc = 0;
            var chkcgstval = 0;
            var chkigstval = 0;
            var chksgstval = 0;
            #endregion checkboxes initialisation

            #region Texboxes amounts initialisation
            decimal Basic = 0;
            decimal DA = 0;
            decimal HRA = 0;
            decimal Conv = 0;
            decimal CCA = 0;
            decimal LA = 0;
            decimal gratuity = 0;
            decimal bonus = 0;
            decimal WA = 0;
            decimal OA = 0;
            decimal nfhs = 0;
            decimal RC = 0;
            decimal foodallowance = 0;
            decimal MedicalAllw = 0;
            decimal travelallowance = 0;
            decimal perfomanceallowance = 0;
            decimal mobileallowance = 0;
            decimal PF = 0;
            decimal ESI = 0;
            decimal pflimit = 0;
            decimal esilimit = 0;
            decimal scper = 0;
            decimal cgstper = 0;
            decimal sgstper = 0;
            decimal igstper = 0;
            decimal MachineryCost = 0;
            decimal MaterialCost = 0;


            #endregion  Texboxes amounts initialisation

            #region code for textboxes

            if (txtBasic.Text.Length > 0)
            {
                Basic = decimal.Parse(txtBasic.Text);
            }

            if (txtDA.Text.Length > 0)
            {

                DA = decimal.Parse(txtDA.Text);
            }
            if (txtHRA.Text.Length > 0)
            {
                HRA = decimal.Parse(txtHRA.Text);
            }
            if (txtconv.Text.Length > 0)
            {
                Conv = decimal.Parse(txtconv.Text);
            }
            if (txtCCA.Text.Length > 0)
            {
                CCA = decimal.Parse(txtCCA.Text);
            }
            if (txtLA.Text.Length > 0)
            {
                LA = decimal.Parse(txtLA.Text);
            }
            if (txtGratuity.Text.Length > 0)
            {
                gratuity = decimal.Parse(txtGratuity.Text);
            }
            if (txtBonus.Text.Length > 0)
            {
                bonus = decimal.Parse(txtBonus.Text);
            }
            if (txtWA.Text.Length > 0)
            {
                WA = decimal.Parse(txtWA.Text);
            }
            if (txtOA.Text.Length > 0)
            {
                OA = decimal.Parse(txtOA.Text);
            }
            if (txtNFHS.Text.Length > 0)
            {
                nfhs = decimal.Parse(txtNFHS.Text);
            }
            if (txtRC.Text.Length > 0)
            {
                RC = decimal.Parse(txtRC.Text);
            }
            if (txtFoodAllowance.Text.Length > 0)
            {
                foodallowance = decimal.Parse(txtFoodAllowance.Text);
            }
            if (txtMedicalAllowance.Text.Length > 0)
            {
                MedicalAllw = decimal.Parse(txtMedicalAllowance.Text);
            }
            if (txtTravelAllowance.Text.Length > 0)
            {
                travelallowance = decimal.Parse(txtTravelAllowance.Text);
            }
            if (txtPerfomanceAllowance.Text.Length > 0)
            {
                perfomanceallowance = decimal.Parse(txtPerfomanceAllowance.Text);
            }
            if (txtMobileAllowance.Text.Length > 0)
            {
                mobileallowance = decimal.Parse(txtMobileAllowance.Text);
            }
            if (txtPF.Text.Length > 0)
            {
                PF = decimal.Parse(txtPF.Text);
            }
            if (txtEsi.Text.Length > 0)
            {
                ESI = decimal.Parse(txtEsi.Text);
            }
            if (txtesiesilimit.Text.Length > 0)
            {
                esilimit = decimal.Parse(txtesiesilimit.Text);
            }
            if (txtesipflimit.Text.Length > 0)
            {
                pflimit = decimal.Parse(txtesipflimit.Text);
            }
            if (txtschargeprc.Text.Length > 0)
            {
                scper = decimal.Parse(txtschargeprc.Text);
            }
            if (txtcgst.Text.Length > 0)
            {
                cgstper = decimal.Parse(txtcgst.Text);
            }
            if (txtsgstper.Text.Length > 0)
            {
                sgstper = decimal.Parse(txtsgstper.Text);
            }
            if (txtigst.Text.Length > 0)
            {
                igstper = decimal.Parse(txtigst.Text);
            }
            if (txtmachinerycomponent.Text.Length > 0)
            {
                MachineryCost = decimal.Parse(txtmachinerycomponent.Text);
            }
            if (txtmaterialcomponent.Text.Length > 0)
            {
                MaterialCost = decimal.Parse(txtmaterialcomponent.Text);
            }

            #endregion code for textboxes

            #region Code for Checkboxes

            if (checkbapf.Checked)
            {
                chkbapf = 1;
            }
            if (checkdapf.Checked)
            {
                chkdapf = 1;
            }
            if (checkhrapf.Checked)
            {
                chkhrapf = 1;
            }
            if (checkconvpf.Checked)
            {
                chkconveypf = 1;
            }
            if (checkccapf.Checked)
            {
                chkccapf = 1;
            }
            if (checkbonuspf.Checked)
            {
                chkbonuspf = 1;
            }

            if (checkgratuitypf.Checked)
            {
                chkgratuitypf = 1;

            }
            if (checklapf.Checked)
            {
                chklapf = 1;
            }
            if (checknfhspf.Checked)
            {
                chknfhspf = 1;

            }
            if (checkrcpf.Checked)
            {
                chkrcpf = 1;
            }
            if (checkwapf.Checked)
            {
                chkwapf = 1;

            }
            if (checkoapf.Checked)
            {
                chkoapf = 1;
            }
            if (checkfoodpf.Checked)
            {
                chkfoodallwpf = 1;
            }
            if (checkmedicalpf.Checked)
            {
                chkmedicalallwpf = 1;

            }
            if (checkmedicalpf.Checked)
            {
                chkmedicalallwpf = 1;
            }
            if (checktravelpf.Checked)
            {
                chktravelallwpf = 1;
            }
            if (checkperfmpf.Checked)
            {
                chkperfmallwpf = 1;
            }
            if (checkmobilepf.Checked)
            {
                chkmobileallwpf = 1;
            }
            if (checkbaesi.Checked)
            {
                chkbaesi = 1;
            }
            if (checkdaesi.Checked)
            {
                chkdaesi = 1;
            }
            if (checkhraesi.Checked)
            {
                chkhraesi = 1;
            }
            if (checkconvesi.Checked)
            {
                chkconveyesi = 1;
            }
            if (checkccaesi.Checked)
            {
                chkccaesi = 1;
            }
            if (checkbonusesi.Checked)
            {
                chkbonusesi = 1;
            }
            if (checkgratuityesi.Checked)
            {
                chkgratuityesi = 1;
            }
            if (checklaesi.Checked)
            {
                chklaesi = 1;
            }
            if (checknfhsesi.Checked)
            {
                chknfhsesi = 1;
            }
            if (checkrcesi.Checked)
            {
                chkrcesi = 1;
            }
            if (checkwaesi.Checked)
            {
                chkwaesi = 1;
            }
            if (checkoaesi.Checked)
            {
                chkoaesi = 1;
            }
            if (checkrcesi.Checked)
            {
                chkoaesi = 1;
            }
            if (checkfoodesi.Checked)
            {
                chkfoodallwesi = 1;
            }
            if (checkmedicalesi.Checked)
            {
                chkmedicalallwesi = 1;
            }
            if (checktravelesi.Checked)
            {
                chktravelallwesi = 1;
            }
            if (checkperfmesi.Checked)
            {
                chkperfmallwesi = 1;
            }
            if (checkmobileesi.Checked)
            {
                chkmobileallwesi = 1;
            }
            if (checkbasc.Checked)
            {
                chkbasc = 1;
            }
            if (checkdasc.Checked)
            {
                chkdasc = 1;
            }
            if (checkhrasc.Checked)
            {
                chkhrasc = 1;
            }
            if (checkconvsc.Checked)
            {
                chkconveysc = 1;
            }
            if (checkccasc.Checked)
            {
                chkccasc = 1;
            }
            if (checkbonussc.Checked)
            {
                chkbonussc = 1;
            }
            if (checkgratuitysc.Checked)
            {
                chkgratuitysc = 1;
            }
            if (checklasc.Checked)
            {
                chklasc = 1;
            }
            if (checknfhssc.Checked)
            {
                chknfhssc = 1;
            }
            if (checkrcsc.Checked)
            {
                chkrcsc = 1;
            }
            if (checkwasc.Checked)
            {
                chkwasc = 1;

            }
            if (checkoasc.Checked)
            {
                chkoasc = 1;
            }
            if (checkfoodsc.Checked)
            {
                chkfoodallwsc = 1;
            }
            if (checkmedicalsc.Checked)
            {
                chkmedicalallwsc = 1;
            }
            if (checktravelsc.Checked)
            {
                chktravelallwsc = 1;
            }
            if (checkperfmsc.Checked)
            {
                chkperfmallwsc = 1;
            }
            if (checkmobilesc.Checked)
            {
                chkmobileallwsc = 1;
            }
            if (chkcgst.Checked)
            {
                chkcgstval = 1;
            }
            if (chkSgst.Checked)
            {
                chksgstval = 1;
            }
            if (chkigst.Checked)
            {
                chkigstval = 1;
            }
            #endregion Code for Checkboxes



            string spname = "MarketingBasicCalculationDetails";
            Hashtable hs = new Hashtable();
            hs.Add("@chkbapf", chkbapf);
            hs.Add("@chkdapf", chkdapf);
            hs.Add("@chkhrapf", chkhrapf);
            hs.Add("@chkccapf", chkccapf);
            hs.Add("@chkconveypf", chkconveypf);
            hs.Add("@chkbonuspf", chkbonuspf);
            hs.Add("@chkgratuitypf", chkgratuitypf);
            hs.Add("@chklapf", chklapf);
            hs.Add("@chknfhspf", chknfhspf);
            hs.Add("@chkrcpf", chkrcpf);
            hs.Add("@chkwapf", chkwapf);
            hs.Add("@chkoapf", chkoapf);
            hs.Add("@chkfoodallwpf", chkfoodallwpf);
            hs.Add("@chkmedicalallwpf", chkmedicalallwpf);
            hs.Add("@chktravelallwpf", chktravelallwpf);
            hs.Add("@chkperfmallwpf", chkperfmallwpf);
            hs.Add("@chkmobileallwpf", chkmobileallwpf);
            hs.Add("@chkbaesi", chkbaesi);
            hs.Add("@chkdaesi", chkdaesi);
            hs.Add("@chkhraesi", chkhraesi);
            hs.Add("@chkconveyesi ", chkconveyesi);
            hs.Add("@chkccaesi", chkccaesi);
            hs.Add("@chkbonusesi", chkbonusesi);
            hs.Add("@chkgratuityesi", chkgratuityesi);
            hs.Add("@chklaesi", chklaesi);
            hs.Add("@chknfhsesi ", chknfhsesi);
            hs.Add("@chkrcesi", chkrcesi);
            hs.Add("@chkwaesi", chkwaesi);
            hs.Add("@chkoaesi", chkoaesi);
            hs.Add("@chkfoodallwesi", chkfoodallwesi);
            hs.Add("@chkmedicalallwesi", chkmedicalallwesi);
            hs.Add("@chktravelallwesi ", chktravelallwesi);
            hs.Add("@chkperfmallwesi", chkperfmallwesi);
            hs.Add("@chkmobileallwesi", chkmobileallwesi);
            hs.Add("@chkbasc", chkbasc);
            hs.Add("@chkdasc", chkdasc);
            hs.Add("@chkhrasc", chkhrasc);
            hs.Add("@chkconveysc", chkconveysc);
            hs.Add("@chkccasc", chkccasc);
            hs.Add("@chkbonussc", chkbonussc);
            hs.Add("@chkgratuitysc", chkgratuitysc);
            hs.Add("@chklasc", chklasc);
            hs.Add("@chknfhssc", chknfhssc);
            hs.Add("@chkrcsc", chkrcsc);
            hs.Add("@chkwasc", chkwasc);
            hs.Add("@chkoasc", chkoasc);
            hs.Add("@chkfoodallwsc", chkfoodallwsc);
            hs.Add("@chkmedicalallwsc", chkmedicalallwsc);
            hs.Add("@chktravelallwsc", chktravelallwsc);
            hs.Add("@chkperfmallwsc", chkperfmallwsc);
            hs.Add("@chkmobileallwsc", chkmobileallwsc);
            hs.Add("@BasicAmount", Basic);
            hs.Add("@DA", DA);
            hs.Add("@HRA", HRA);
            hs.Add("@Conveyence", Conv);
            hs.Add("@CCA", CCA);
            hs.Add("@LA", LA);
            hs.Add("@Gratuity", gratuity);
            hs.Add("@Bonus", bonus);
            hs.Add("@WA", WA);
            hs.Add("@OA", OA);
            hs.Add("@NFHS", nfhs);
            hs.Add("@RC", RC);
            hs.Add("@FoodAllowance", foodallowance);
            hs.Add("@MedicalAllowance", MedicalAllw);
            hs.Add("@TravelAllowance", travelallowance);
            hs.Add("@PerformanceAllowance", perfomanceallowance);
            hs.Add("@MoboleAllowance", mobileallowance);
            hs.Add("@PF", PF);
            hs.Add("@ESI", ESI);
            hs.Add("@PFlimit", pflimit);
            hs.Add("@ESIlimit", esilimit);
            hs.Add("@Servicechargeper", scper);
            hs.Add("@cgstper", cgstper);
            hs.Add("@sgstper", sgstper);
            hs.Add("@igstper", igstper);
            hs.Add("@chkcgst", chkcgstval);
            hs.Add("@chksgst", chksgstval);
            hs.Add("@chkigst", chkigstval);
            hs.Add("@Machinerycost", MachineryCost);
            hs.Add("@Materialcost", MaterialCost);
            DataTable dtBasicCalculationDetails =config.ExecuteAdaptorAsyncWithParams(spname, hs).Result;

            if (dtBasicCalculationDetails.Rows.Count > 0)
            {
                lblresult.Text = dtBasicCalculationDetails.Rows[0]["TotalAmount"].ToString();
                txtGrandTotal.Text = dtBasicCalculationDetails.Rows[0]["GrandTotal"].ToString();
                txtschargeamount.Text = dtBasicCalculationDetails.Rows[0]["SC"].ToString();
                txtcgstamount.Text = dtBasicCalculationDetails.Rows[0]["CGST"].ToString();
                txtsgstamount.Text = dtBasicCalculationDetails.Rows[0]["SGST"].ToString();
                txtigstamount.Text = dtBasicCalculationDetails.Rows[0]["IGST"].ToString();
                pfamount.Text = dtBasicCalculationDetails.Rows[0]["PF"].ToString();
                esiamount.Text = dtBasicCalculationDetails.Rows[0]["ESI"].ToString();
            }
        }



        protected void DownloadFile(object sender, EventArgs e)
        {
            int id = int.Parse((sender as LinkButton).CommandArgument);
            byte[] bytes;
            string fileName, contentType;
            string constr = ConfigurationManager.ConnectionStrings["KLTSConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select Name, Data, ContentType,filename from M_tblFiles where Id=@Id";
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        sdr.Read();
                        bytes = (byte[])sdr["Data"];
                        contentType = sdr["ContentType"].ToString();
                        fileName = sdr["filename"].ToString();
                    }
                    con.Close();
                }
            }
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = contentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();
        }
    }
}