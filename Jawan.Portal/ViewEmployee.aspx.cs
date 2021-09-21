using System;
using System.Collections;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data;
using System.Data.SqlClient;
using KLTS.Data;
using System.IO;
using System.Globalization;
using System.Collections.Generic;
using Jawan.Portal.DAL;
namespace Jawan.Portal
{
    public partial class ViewEmployee : System.Web.UI.Page
    {
        string EmpIDPrefix = "";
        AppConfiguration config = new AppConfiguration();
        GridViewExportUtil gve = new GridViewExportUtil();

        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                GetWebConfigdata();
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

                    if (Request.QueryString["Empid"] != null)
                    {

                        string username = Request.QueryString["Empid"].ToString();
                        DisplayData(username);

                    }

                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "show alert()", "alert('Your Session Expired Please Login');", true);
            }
        }
        protected void PreviligeUsers(int previligerid)
        {

            switch (previligerid)
            {

                case 1:
                    break;
                case 2:
                    //AddEmployeeLink.Visible = true;
                    //ModifyEmployeeLink.Visible = true;
                    //DeleteEmployeeLink.Visible = true;
                    AssigningWorkerLink.Visible = true;
                    AttendanceLink.Visible = true;
                    LoanLink.Visible = false;
                    PaymentLink.Visible = false;
                    //TrainingEmployeeLink.Visible = true;
                    PostingOrderListLink.Visible = true;
                    //JobLeavingReasonsLink.Visible = true;

                    ClientsLink.Visible = true;
                    CompanyInfoLink.Visible = false;
                    InventoryLink.Visible = false;
                    ReportsLink.Visible = true;
                    SettingsLink.Visible = true;
                    break;

                case 3:

                    InventoryLink.Visible = false;
                    ReportsLink.Visible = false;
                    SettingsLink.Visible = false;

                    break;

                case 4:

                    //AddEmployeeLink.Visible = true;
                    //ModifyEmployeeLink.Visible = false;
                    //DeleteEmployeeLink.Visible = false;
                    AssigningWorkerLink.Visible = false;
                    AttendanceLink.Visible = false;
                    LoanLink.Visible = false;
                    PaymentLink.Visible = false;
                    //TrainingEmployeeLink.Visible = false;
                    //JobLeavingReasonsLink.Visible = false;


                    CompanyInfoLink.Visible = false;
                    InventoryLink.Visible = false;
                    ReportsLink.Visible = false;
                    SettingsLink.Visible = false;


                    break;
                case 5:
                    //AddEmployeeLink.Visible = false;
                    //ModifyEmployeeLink.Visible = false;
                    //DeleteEmployeeLink.Visible = false;
                    AttendanceLink.Visible = true;
                    LoanLink.Visible = false;
                    PaymentLink.Visible = false;
                    //TrainingEmployeeLink.Visible = false;
                    //JobLeavingReasonsLink.Visible = false;

                    CompanyInfoLink.Visible = false;
                    InventoryLink.Visible = false;
                    ReportsLink.Visible = false;
                    SettingsLink.Visible = false;
                    break;
                case 6:
                    //AddEmployeeLink.Visible = false;
                    //ModifyEmployeeLink.Visible = false;
                    //DeleteEmployeeLink.Visible = false;
                    AttendanceLink.Visible = true;
                    LoanLink.Visible = false;
                    PaymentLink.Visible = false;
                    //TrainingEmployeeLink.Visible = false;
                    //JobLeavingReasonsLink.Visible = false;


                    CompanyInfoLink.Visible = false;
                    InventoryLink.Visible = false;
                    SettingsLink.Visible = false;

                    break;
                default:
                    break;


            }
        }

        public void DisplayData(string empid)
        {

            try
            {


                #region  Begin Variable Declaration
                var SPNAme = "";
                Hashtable HTViewEmployee = new Hashtable();
                DataTable dtViewEmployee = null;
                #endregion end Variable Declaration

                #region Begin Assign Values to Hash table And Calling Stored Procedure
                SPNAme = "IMViewEmployee";
                HTViewEmployee.Add("@empid", empid);
                HTViewEmployee.Add("@empidprefix", EmpIDPrefix);
                dtViewEmployee =config.ExecuteAdaptorAsyncWithParams(SPNAme, HTViewEmployee).Result;
                #endregion End Assign Values to Hash table And Calling Stored Procedure

                #region Begin Code For Display Message
                if (dtViewEmployee.Rows.Count == 0 || String.IsNullOrEmpty(dtViewEmployee.Rows[0]["empid"].ToString()))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "show alert()", "alert('Employee Details Are Not Avaialable For This Employee.');", true);
                    return;
                }
                #endregion End Code For Dispaly Message

                lblEmpid.Text = dtViewEmployee.Rows[0]["empid"].ToString();
                lblNameemp.Text = dtViewEmployee.Rows[0]["FullName"].ToString();
                lblGender.Text = dtViewEmployee.Rows[0]["EmpSex"].ToString();
                lblDateofInterview.Text = dtViewEmployee.Rows[0]["EmpDtofInterview"].ToString();
                lblDateofBirth.Text = dtViewEmployee.Rows[0]["empdtofbirth"].ToString();
                lblQualification.Text = dtViewEmployee.Rows[0]["EmpQualification"].ToString();
                lblFaSpName.Text = dtViewEmployee.Rows[0]["EmpFatherName"].ToString();
                lblFaSpAge.Text = dtViewEmployee.Rows[0]["EmpFatherAge"].ToString();
                lblMotherName.Text = dtViewEmployee.Rows[0]["MotherName"].ToString();
                lblMotherOccu.Text = dtViewEmployee.Rows[0]["Moccupation"].ToString();
                lblPhoneNo.Text = dtViewEmployee.Rows[0]["EmpPhone"].ToString();

                lblEsiDeduct.Text = dtViewEmployee.Rows[0]["EmpESIDeduct"].ToString();
                lblPFDeduct.Text = dtViewEmployee.Rows[0]["EmpPFDeduct"].ToString();
                lblExService.Text = dtViewEmployee.Rows[0]["EmpExservice"].ToString();
                lblPtDeduct.Text = dtViewEmployee.Rows[0]["EmpPTDeduct"].ToString();
                lblSitePosted.Text = dtViewEmployee.Rows[0]["UnitId"].ToString();

                lblMaritalStatus.Text = dtViewEmployee.Rows[0]["EmpMaritalStatus"].ToString();
                lblDateofJoining.Text = dtViewEmployee.Rows[0]["EmpDtofJoining"].ToString();
                lblDateofLeaving.Text = dtViewEmployee.Rows[0]["EmpDtofLeaving"].ToString();
                lblDesig.Text = dtViewEmployee.Rows[0]["design"].ToString();
                lblFaSpOccu.Text = dtViewEmployee.Rows[0]["EmpFatherOccupation"].ToString();
                lblFaSpRel.Text = dtViewEmployee.Rows[0]["EmpFatherSpouseRelation"].ToString();
                lblPreviousEmp.Text = dtViewEmployee.Rows[0]["EmpPreviousExp"].ToString();
                lblMotherTongue.Text = dtViewEmployee.Rows[0]["MotherTongue"].ToString();
                lblNationality.Text = dtViewEmployee.Rows[0]["Nationality"].ToString();
                lblReligion.Text = dtViewEmployee.Rows[0]["Religion"].ToString();

                lblLanguges.Text = dtViewEmployee.Rows[0]["EmpLanguagesKnown"].ToString();
                lblRefaddr1.Text = dtViewEmployee.Rows[0]["EmpRefAddr1"].ToString();
                lblBloodG.Text = dtViewEmployee.Rows[0]["BloodGroupName"].ToString();
                lblPhyRema.Text = dtViewEmployee.Rows[0]["EmpPhysicalRemarks"].ToString();
                lblImarks1.Text = dtViewEmployee.Rows[0]["pelmark"].ToString();

                lblHeight.Text = dtViewEmployee.Rows[0]["EmpHeight"].ToString();
                lblWeight.Text = dtViewEmployee.Rows[0]["EmpWeight"].ToString();
                lblDoorno.Text = dtViewEmployee.Rows[0]["prDoorno"].ToString();
                lblStreet.Text = dtViewEmployee.Rows[0]["prStreet"].ToString();

                lblLandMark.Text = dtViewEmployee.Rows[0]["prLmark"].ToString();
                lblArea.Text = dtViewEmployee.Rows[0]["prArea"].ToString();
                lblCity.Text = dtViewEmployee.Rows[0]["prCity"].ToString();
                lblDistrict.Text = dtViewEmployee.Rows[0]["prDistrict"].ToString();

                lblPincode.Text = dtViewEmployee.Rows[0]["prPincode"].ToString();
                lblState.Text = dtViewEmployee.Rows[0]["prState"].ToString();
                lblPhone1.Text = dtViewEmployee.Rows[0]["prphone"].ToString();
                lblRefaddr2.Text = dtViewEmployee.Rows[0]["EmpRefAddr2"].ToString();
                lblRemarks.Text = dtViewEmployee.Rows[0]["empremarks"].ToString();

                lblImarks2.Text = dtViewEmployee.Rows[0]["empidmark2"].ToString();
                lblUnexpand.Text = dtViewEmployee.Rows[0]["EmpChestunex"].ToString();
                lblExpand.Text = dtViewEmployee.Rows[0]["empchestexp"].ToString();
                lblDoorno1.Text = dtViewEmployee.Rows[0]["pedoor"].ToString();
                lblStreet1.Text = dtViewEmployee.Rows[0]["peStreet"].ToString();
                lblLandMark1.Text = dtViewEmployee.Rows[0]["pelmark"].ToString();


                lblArea1.Text = dtViewEmployee.Rows[0]["pearea"].ToString();
                lblCity1.Text = dtViewEmployee.Rows[0]["peCity"].ToString();
                lblPermdistrict.Text = dtViewEmployee.Rows[0]["peDistrict"].ToString();
                lblPincode1.Text = dtViewEmployee.Rows[0]["pepincode"].ToString();
                lblState1.Text = dtViewEmployee.Rows[0]["pestate"].ToString();
                lblPhone2.Text = dtViewEmployee.Rows[0]["pephone"].ToString();

                lblBankname.Text = dtViewEmployee.Rows[0]["bankname"].ToString();
                lblBranchName.Text = dtViewEmployee.Rows[0]["Empbankbrabchname"].ToString();
                lblBranchcode.Text = dtViewEmployee.Rows[0]["empbranchcode"].ToString();
                lblBankAppno.Text = dtViewEmployee.Rows[0]["empbankappno"].ToString();
                lblIsuNominee.Text = dtViewEmployee.Rows[0]["empinsnominee"].ToString();
                lblNomDob.Text = dtViewEmployee.Rows[0]["empnomineedtofbirth"].ToString();
                lblInsuCover.Text = dtViewEmployee.Rows[0]["empinscover"].ToString();


                lblAadhaarNo.Text = dtViewEmployee.Rows[0]["aadhaarid"].ToString();

                lblBankAcno.Text = dtViewEmployee.Rows[0]["empbankacno"].ToString();
                lblIfsccode.Text = dtViewEmployee.Rows[0]["empifsccode"].ToString();
                lblBankcodeno.Text = dtViewEmployee.Rows[0]["empbankcode"].ToString();
                lblRegionCode.Text = dtViewEmployee.Rows[0]["empregioncode"].ToString();
                lblBankCardRef.Text = dtViewEmployee.Rows[0]["empbankcardref"].ToString();
                lblNomiRelation.Text = dtViewEmployee.Rows[0]["empnomineerel"].ToString();
                lblInsdebtam.Text = dtViewEmployee.Rows[0]["empinsdedamt"].ToString();
                lblSsno.Text = dtViewEmployee.Rows[0]["empssnumber"].ToString();



                lblNameAdSc.Text = dtViewEmployee.Rows[0]["sscschool"].ToString();
                lblBoardUni.Text = dtViewEmployee.Rows[0]["sscbduniversity"].ToString();
                lblYearofStudy.Text = dtViewEmployee.Rows[0]["sscstdyear"].ToString();
                lblPassFail.Text = dtViewEmployee.Rows[0]["sscpassfail"].ToString();
                lblPerMarks.Text = dtViewEmployee.Rows[0]["sscmarks"].ToString();

                lblNameAddr.Text = dtViewEmployee.Rows[0]["imschool"].ToString();
                lblBoardUniver.Text = dtViewEmployee.Rows[0]["imbduniversity"].ToString();
                lblYearofstudy1.Text = dtViewEmployee.Rows[0]["imstdyear"].ToString();
                lblPassFail1.Text = dtViewEmployee.Rows[0]["impassfail"].ToString();
                lblPerMarks1.Text = dtViewEmployee.Rows[0]["immarks"].ToString();

                lblDnameaddr.Text = dtViewEmployee.Rows[0]["dgschool"].ToString();
                lblDboardUni.Text = dtViewEmployee.Rows[0]["dgbduniversity"].ToString();
                lblDyearstu.Text = dtViewEmployee.Rows[0]["dgstdyear"].ToString();
                lblDpassfail.Text = dtViewEmployee.Rows[0]["dgpassfail"].ToString();
                lblDperce.Text = dtViewEmployee.Rows[0]["dgmarks"].ToString();

                lblPgnameaddr.Text = dtViewEmployee.Rows[0]["pgschool"].ToString();
                lblPgboard.Text = dtViewEmployee.Rows[0]["pgbduniversity"].ToString();
                lblPgYear.Text = dtViewEmployee.Rows[0]["pgstdyear"].ToString();
                lblPgPass.Text = dtViewEmployee.Rows[0]["pgpassfail"].ToString();
                lblPgPerc.Text = dtViewEmployee.Rows[0]["pgmarks"].ToString();


                lblSerNo.Text = dtViewEmployee.Rows[0]["serviceno"].ToString();
                lblRank.Text = dtViewEmployee.Rows[0]["rank"].ToString();
                lblDtofenroll.Text = dtViewEmployee.Rows[0]["DtofEnrolment"].ToString();
                lblDateofdisc.Text = dtViewEmployee.Rows[0]["DtofDischarge"].ToString();
                lblCrops.Text = dtViewEmployee.Rows[0]["crops"].ToString();
                lblTrade.Text = dtViewEmployee.Rows[0]["trade"].ToString();
                lblMedicalCat.Text = dtViewEmployee.Rows[0]["MedcalCategoryBloodGroup"].ToString();
                lblReasonDisc.Text = dtViewEmployee.Rows[0]["reasonsofdischarge"].ToString();
                lblConduct.Text = dtViewEmployee.Rows[0]["conduct"].ToString();

                lblEpfno.Text = dtViewEmployee.Rows[0]["empepfno"].ToString();
                lblPfNominee.Text = dtViewEmployee.Rows[0]["EmpNominee"].ToString();
                lblPfendatee.Text = dtViewEmployee.Rows[0]["EmpPFEnrolDt"].ToString();
                lblPfNomiRelation.Text = dtViewEmployee.Rows[0]["EmpRelation"].ToString();

                lblEsiNO.Text = dtViewEmployee.Rows[0]["EmpESINo"].ToString();
                lblEsInominee.Text = dtViewEmployee.Rows[0]["EmpESINominee"].ToString();
                lblEsiDisname.Text = dtViewEmployee.Rows[0]["EmpESIDispName"].ToString();
                lblEsiNomRelation.Text = dtViewEmployee.Rows[0]["EMPESIRelation"].ToString();

            }
            catch (Exception ex)
            {

            }
        }

        protected void GetWebConfigdata()
        {
            EmpIDPrefix = Session["EmpIDPrefix"].ToString();
        }
    }
}