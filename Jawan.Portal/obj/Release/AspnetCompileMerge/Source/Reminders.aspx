<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Reminders.aspx.cs" Inherits="Jawan.Portal.Reminders" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>REMINDERS</title>
    <link rel="shortcut icon" href="assets/Mushroom.ico" />
    <link href="css/global.css" rel="stylesheet" type="text/css" />
    <link href="css/reminders.css" rel="stylesheet" type="text/css" />

    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.1/jquery.min.js" type="text/javascript"></script>

    <script src="css/js/jquery.bpopup-x.x.x.min.js" type="text/javascript"></script>

    <script type="text/javascript">

        ; (function($) {


            $(function() {


                $('#my-button').bind('click', function(e) {
                    e.preventDefault();
                    $('#element_to_pop_up').bPopup();
                });


            });

            $(function() {


                $('#my-button1').bind('click', function(f) {
                    f.preventDefault();
                    $('#element_to_pop_up1').bPopup();
                });


            });

            $(function() {


                $('#my-button2').bind('click', function(f) {
                    f.preventDefault();
                    $('#element_to_pop_up2').bPopup();
                });


            });

            $(function() {


                $('#my-button3').bind('click', function(f) {
                    f.preventDefault();
                    $('#element_to_pop_up3').bPopup();
                });


            });

            $(function() {


                $('#my-button4').bind('click', function(f) {
                    f.preventDefault();
                    $('#element_to_pop_up4').bPopup();
                });


            });

            $(function() {


                $('#my-button5').bind('click', function(f) {
                    f.preventDefault();
                    $('#element_to_pop_up5').bPopup();
                });


            });

            $(function() {


                $('#my-button6').bind('click', function(f) {
                    f.preventDefault();
                    $('#element_to_pop_up6').bPopup();
                });


            });

            $(function() {


                $('#my-button7').bind('click', function(f) {
                    f.preventDefault();
                    $('#element_to_pop_up7').bPopup();
                });


            });

        })(jQuery);
    </script>

    <script src="css/js/jquery.min.js" type="text/javascript"></script>

    <script type="text/javascript" src="css/js/jquery.easing.min.js"></script>

    <script type="text/javascript" src="css/js/jquery.easy-ticker.js"></script>

    <script type="text/javascript">



        $(document).ready(function() {

            var dd = $('.vticker1').easyTicker({
                direction: 'up',
                easing: 'swing',
                speed: 'slow',
                interval: 2500,
                height: 'auto',
                visible: 3,
                mousePause: 1,
                controls: {
                    up: '.up',
                    down: '.down',
                    toggle: '.toggle',
                    stopText: 'Stop !!!'
                }
            }).data('easyTicker');
        });
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <!-- HEADER SECTION BEGIN -->
    <div id="headerouter">
        <!-- LOGO AND MAIN MENU SECTION BEGIN -->
        <div id="header">
            <!-- LOGO BEGIN -->
            <div id="logo">
                <a href="default.aspx">
                    <img border="0" src="assets/logo.png" alt="FACILITY MANAGEMENT SOFTWARE" title="FACILITY MANAGEMENT SOFTWARE" /></a>
            </div>
            <!-- LOGO END -->
            <!-- TOP INFO BEGIN -->
            <div id="toplinks">
                <ul>
                    <li><a href="Reminders.aspx">Reminders</a></li><li>Welcome <b>
                        <asp:Label ID="lblDisplayUser" runat="server" Text="Label" Font-Bold="true"></asp:Label></b></li>
                    <li class="lang"><a href="Login.aspx">Logout</a></li>
                </ul>
            </div>
            <!-- TOP INFO END -->
            <!-- MAIN MENU BEGIN -->
            <div id="mainmenu">
                <ul>
                    <li class="first"><a href="Employees.aspx" id="EmployeesLink" runat="server"><span>Employees</span></a></li>
                    <li><a href="clients.aspx" id="ClientsLink" runat="server"><span>Clients</span></a></li>
                    <li><a href="companyinfo.aspx" id="CompanyInfoLink" runat="server"><span>Company Info</span></a></li>
                    <li><a href="ViewItems.aspx" id="InventoryLink" runat="server"><span>Inventory</span></a></li>
                    <li><a href="Reports.aspx" id="ReportsLink" runat="server"><span>Reports</span></a></li>
                    <li><a href="Settings.aspx" id="SettingsLink" runat="server"><span>Settings</span></a></li>
                    <li class="last"><a href="M_Leads.aspx" id="LinkMarketting" visible="false" runat="server"><span><span>Marketting</span></span></a></li>
                     <li class="last"><a href="Login.aspx" id="LinkLogout" runat="server"><span><span>Logout</span></span></a></li>
                </ul>
            </div>
            <!-- MAIN MENU SECTION END -->
        </div>
        <!-- LOGO AND MAIN MENU SECTION END -->
        <!-- SUB NAVIGATION SECTION BEGIN -->
        <!--  <div id="submenu"> <img width="1" height="5" src="assets/spacer.gif"> </div> -->
        <div id="submenu">
            <table width="100%" cellspacing="0" cellpadding="0" border="0">
                <tbody>
                    <tr>
                        <td>
                            <div style="display: inline;">
                            </div>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <!-- SUBNAVIGATION SECTION END -->
    </div>
    <!-- HEADER SECTION END -->
    <!-- CONTENT AREA BEGIN -->
    <div id="content-holder" style="background: #ffffff;">
        <div class="content-holder">
            <h1>
                Reminders</h1>
            <!-- DASHBOARD CONTENT BEGIN -->
            <div class="contentarea" id="contentarea">
                <div class="row">
                    <!-- Birthdays Section -->
                    <article class="col-sm-4">
        <div class="data-block turquoise">
          <header>
            <h2>Birthdays</h2>
           
          </header>
          <section>
           <asp:Label ID="lblheadingone" runat="server"
            Text="DIYOS Technologies wishes " style="font-weight:bold;line-height:2;height:200px"></asp:Label>
            <br />
      
		
		<asp:ListView ID="GVBirthday" runat="server"  >
   <LayoutTemplate  >
   
   <div class="vticker1">
	<ul>
	<asp:PlaceHolder runat="server" ID="itemPlaceholder"  />
	</ul>
	</div>
   </LayoutTemplate>
   <ItemTemplate >
     <li>
      <div style="float:left;padding-right:8px">
      <img src="css/assets/birthday.png" width="20" height="20" />
      </div>
			<div>
            <%#(Eval("Name").ToString())%>
            </div>
           
            </li> 
            
   </ItemTemplate> 
   </asp:ListView> 
   
   
    </section>
    
    <div id="viewl"><a href="#" id="my-button">View More</a></div> 
   </div> 
   </article>
                    <div id="element_to_pop_up">
                        <asp:ListView ID="GVBirthday_Viewmore" runat="server">
                            <LayoutTemplate>
                                <div class="viewmore">
                                    <h2>
                                        Birthdays <a href="#" class="bClose" style="float: right">
                                            <img src="css/assets/close-button.png" width="30" height="30"></a></h2>
                                    <ul>
                                        <asp:PlaceHolder runat="server" ID="itemPlaceholder" />
                                    </ul>
                                </div>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <li>
                                    <div style="float: left; padding-right: 8px">
                                        <img src="css/assets/birthday.png" width="20" height="20" />
                                    </div>
                                    <div>
                                        <%#(Eval("Name").ToString())%>
                                    </div>
                                </li>
                            </ItemTemplate>
                        </asp:ListView>
                    </div>
                    <!-- Birthdays Section End -->
                    <!-- Contract Renewals -->
                    <article class="col-sm-4">
        <div class="data-block turquoise">
          <header>
            <h2>Contract Renewals</h2>
          
          </header>
          <section>
         
          	
		<asp:ListView ID="GVContract" runat="server">
   <LayoutTemplate>
   <div class="vticker1">
	<ul>
	<asp:PlaceHolder runat="server" ID="itemPlaceholder" />
	</ul>
	</div>
   </LayoutTemplate>
   <ItemTemplate>
     <li>
     <div style="float:left;padding-right:8px;height: 50px;"><img src="css/assets/contract.png" width="20" height="20" /></div>
			<div>
			<%#(Eval("ElapsedTime").ToString())%>
            </div>
        </li>
        
   </ItemTemplate>
</asp:ListView>

		<br />
         
          
           </section>
           <div id="viewl"><a href="#" id="my-button1">View More</a></div>
        </div>
      </article>
                    <div id="element_to_pop_up1">
                        <asp:ListView ID="GVContract_Viewmore" runat="server">
                            <LayoutTemplate>
                                <div class="viewmore">
                                    <h2>
                                        Contract Renewals <a href="#" class="bClose" style="float: right">
                                            <img src="css/assets/close-button.png" width="30" height="30"></a></h2>
                                    <ul>
                                        <asp:PlaceHolder runat="server" ID="itemPlaceholder" />
                                    </ul>
                                </div>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <li>
                                    <div style="float: left; padding-right: 8px">
                                        <img src="css/assets/contract.png" width="20" height="20" />
                                    </div>
                                    <div>
                                        <%#(Eval("ElapsedTime").ToString())%>
                                    </div>
                                </li>
                            </ItemTemplate>
                        </asp:ListView>
                    </div>
                    <!-- Contract Renewals End-->
                    <!-- License Renewals Renewals -->
                    <article class="col-sm-4">
        <div class="data-block red">
          <header>
            <h2><span class="elusive icon-fire"></span> License Renewals</h2>
          
          </header>
          <section>
            
            
            
            <asp:ListView ID="ListView1" runat="server">
   <LayoutTemplate>
   <div class="vticker">
	<ul>
	<asp:PlaceHolder runat="server" ID="itemPlaceholder" />
	</ul>
	</div>
   </LayoutTemplate>
   <ItemTemplate>
     <li>
     <div style="float:left;padding-right:8px"><img src="css/assets/renewal.png" width="20" height="20" /></div>
			<div>
			<%#(Eval("").ToString())%>
            </div>
        </li>
        
   </ItemTemplate>
</asp:ListView>


          </section>
          <div id="viewl"><a href="#" id="A1">View More</a></div>
        </div>
      </article>
                    <div id="Div1">
                        <asp:ListView ID="ListView3" runat="server">
                            <LayoutTemplate>
                                <div class="viewmore">
                                    <h2>
                                        License Renewals</h2>
                                    <ul>
                                        <asp:PlaceHolder runat="server" ID="itemPlaceholder" />
                                    </ul>
                                </div>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <li>
                                    <div style="float: left; padding-right: 8px">
                                        <img src="css/assets/renewal.png" width="20" height="20" />
                                    </div>
                                    <div>
                                        <%#(Eval("ElapsedTime").ToString())%>
                                    </div>
                                </li>
                            </ItemTemplate>
                        </asp:ListView>
                    </div>
                </div>
                <div class="row">
                    <!-- License Renewals Section End -->
                    <!-- Latest Bills Generated Section -->
                    <article class="col-sm-4">
        <div class="data-block turquoise">
          <header>
            <h2>Latest Bills Generated</h2>
           
          </header>
          <section>
          
          
           <asp:ListView ID="gvLatestbills" runat="server">
   <LayoutTemplate>
   <div class="vticker">
	<ul style="overflow:hidden;height:170px">
	<asp:PlaceHolder runat="server" ID="itemPlaceholder" />
	</ul>
	</div>
   </LayoutTemplate>
   <ItemTemplate>
     <li>
     <div style="float:left;padding-right:8px;height:70px">
     <img src="css/assets/billing.png" width="20" height="20" /></div>
			<div>
			<%#(Eval("BillStaus").ToString())%>
            </div>
        </li>
        
   </ItemTemplate>
</asp:ListView>

          </section>
          <div id="viewl"><a href="#" id="my-button2">View More</a></div>
        </div>
      </article>
                    <div id="element_to_pop_up2">
                        <asp:ListView ID="gvLatestbills_Viewmore" runat="server">
                            <LayoutTemplate>
                                <div class="viewmore">
                                    <h2>
                                        Latest Bills Generated <a href="#" class="bClose" style="float: right">
                                            <img src="css/assets/close-button.png" width="30" height="30"></a></h2>
                                    <ul>
                                        <asp:PlaceHolder runat="server" ID="itemPlaceholder" />
                                    </ul>
                                </div>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <li>
                                    <div style="float: left; padding-right: 8px">
                                        <img src="css/assets/billing.png" width="20" height="20" />
                                    </div>
                                    <div>
                                        <%#(Eval("BillStaus").ToString())%>
                                    </div>
                                </li>
                            </ItemTemplate>
                        </asp:ListView>
                    </div>
                    <!-- Latest Bills Generated Section End -->
                    <!-- Latest Paysheets Generated Section -->
                    <article class="col-sm-4">
        <div class="data-block turquoise">
          <header>
            <h2>Latest Paysheets Generated</h2>
          
          </header>
          <section>
          
           <asp:ListView ID="gvlatesPaysheet" runat="server">
   <LayoutTemplate>
   <div class="vticker">
	<ul style="overflow:hidden;height:170px">
	<asp:PlaceHolder runat="server" ID="itemPlaceholder" />
	</ul>
	</div>
   </LayoutTemplate>
   <ItemTemplate>
     <li>
     <div style="float:left;padding-right:8px;height:70px"><img src="css/assets/paysheet.png" width="20" height="20" /></div>
			<div>
			<%#(Eval("Paysheetstatus").ToString())%>
            </div>
        </li>
        
   </ItemTemplate>
   
</asp:ListView><br />
 
          </section>
         <div id="viewl"><a href="#" id="my-button3">View More</a></div>
        </div>
      </article>
                    <div id="element_to_pop_up3">
                        <asp:ListView ID="gvlatesPaysheet_Viewmore" runat="server">
                            <LayoutTemplate>
                                <div class="viewmore">
                                    <h2>
                                        Latest Paysheets Generated <a href="#" class="bClose" style="float: right">
                                            <img src="css/assets/close-button.png" width="30" height="30"></a></h2>
                                    <ul>
                                        <asp:PlaceHolder runat="server" ID="itemPlaceholder" />
                                    </ul>
                                </div>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <li>
                                    <div style="float: left; padding-right: 8px">
                                        <img src="css/assets/paysheet.png" width="20" height="20" />
                                    </div>
                                    <div>
                                        <%#(Eval("Paysheetstatus").ToString())%>
                                    </div>
                                </li>
                            </ItemTemplate>
                        </asp:ListView>
                    </div>
                    <!-- Latest Paysheets Generated Section End-->
                    <!-- Latest Receipts Section -->
                    <article class="col-sm-4">
        <div class="data-block red">
          <header>
            <h2><span class="elusive icon-fire"></span> Latest Receipts</h2>
          
          </header>
          <section>
          
            <asp:ListView ID="gvReciepts" runat="server">
   <LayoutTemplate>
   <div class="vticker">
	<ul>
	<asp:PlaceHolder runat="server" ID="itemPlaceholder" />
	</ul>
	</div>
   </LayoutTemplate>
   <ItemTemplate>
     <li>
     <div style="float:left;padding-right:8px;height:40px"><img src="css/assets/receipt.gif" width="20" height="20" /></div>
			<div>
			<%#(Eval("Reciepts").ToString())%>
            </div>
        </li>
        
   </ItemTemplate>
</asp:ListView>

          </section>
         <div id="viewl"><a href="#" id="my-button4">View More</a></div>
        </div>
      </article>
                    <div id="element_to_pop_up4">
                        <asp:ListView ID="gvReciepts_Viewmore" runat="server">
                            <LayoutTemplate>
                                <div class="viewmore">
                                    <h2>
                                        Latest Paysheets Generated <a href="#" class="bClose" style="float: right">
                                            <img src="css/assets/close-button.png" width="30" height="30"></a></h2>
                                    <ul>
                                        <asp:PlaceHolder runat="server" ID="itemPlaceholder" />
                                    </ul>
                                </div>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <li>
                                    <div style="float: left; padding-right: 8px">
                                        <img src="css/assets/receipt.gif" width="20" height="20" />
                                    </div>
                                    <div>
                                        <%#(Eval("Reciepts").ToString())%>
                                    </div>
                                </li>
                            </ItemTemplate>
                        </asp:ListView>
                    </div>
                    <!-- Latest Receipts Section End-->
                </div>
                <div class="row">
                    <!-- Billing Section -->
                    <article class="col-sm-4">
        <div class="data-block turquoise">
          <header>
            <h2> Billing</h2>
           
          </header>
          <section>
            <asp:Label ID="Label9" runat="server"
            Text="" style="font-weight:bold;line-height:2;height:200px"></asp:Label>
          
          
          
          
           <asp:ListView ID="gvBills" runat="server">
   <LayoutTemplate>
   <div class="vticker">
	<ul>
	<asp:PlaceHolder runat="server" ID="itemPlaceholder" />
	</ul>
	</div>
   </LayoutTemplate>
   <ItemTemplate>
    <li>
     <div style="float:left;padding-right:8px;height:40px"><img src="css/assets/billing.png" width="20" height="20" /></div>
			<div>
			<%#(Eval("BillingDeatils2").ToString())%>
            </div>
        </li>
        
   </ItemTemplate>
</asp:ListView>

          </section>
         <div id="viewl"><a href="#" id="my-button5">View More</a></div>
        </div>
      </article>
                    <div id="element_to_pop_up5">
                        <asp:ListView ID="gvBills_Viewmore" runat="server">
                            <LayoutTemplate>
                                <div class="viewmore">
                                    <h2>
                                        Billing <a href="#" class="bClose" style="float: right">
                                            <img src="css/assets/close-button.png" width="30" height="30"></a></h2>
                                    <ul>
                                        <asp:PlaceHolder runat="server" ID="itemPlaceholder" />
                                    </ul>
                                </div>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <li>
                                    <div style="float: left; padding-right: 8px">
                                        <img src="css/assets/billing.png" width="20" height="20" />
                                    </div>
                                    <div>
                                        <%#(Eval("BillingDeatils2").ToString())%>
                                    </div>
                                </li>
                            </ItemTemplate>
                        </asp:ListView>
                    </div>
                    <!-- Billing Section End-->
                    <!-- Paysheets Section -->
                    <article class="col-sm-4">
        <div class="data-block turquoise">
          <header>
            <h2>Paysheets</h2>
          
          </header>
          <section>
           
           <asp:ListView ID="gvPaysheets" runat="server">
   <LayoutTemplate>
    <div class="vticker">
	<ul>
	<asp:PlaceHolder runat="server" ID="itemPlaceholder" />
	</ul>
	</div>
   </LayoutTemplate>
   <ItemTemplate>
    <li>
     <div style="float:left;padding-right:8px;height:40px"><img src="css/assets/paysheet.png" width="20" height="20" /></div>
			<div>
			<%#(Eval("EmppaysheetAmount").ToString())%>
            </div>
        </li>
        
   </ItemTemplate>
</asp:ListView>


 
          </section>
         <div id="viewl"><a href="#" id="my-button6">View More</a></div>
        </div>
      </article>
                    <div id="element_to_pop_up6">
                        <asp:ListView ID="gvPaysheets_Viewmore" runat="server">
                            <LayoutTemplate>
                                <div class="viewmore">
                                    <h2>
                                        Paysheets <a href="#" class="bClose" style="float: right">
                                            <img src="css/assets/close-button.png" width="30" height="30"></a></h2>
                                    <ul>
                                        <asp:PlaceHolder runat="server" ID="itemPlaceholder" />
                                    </ul>
                                </div>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <li>
                                    <div style="float: left; padding-right: 8px">
                                        <img src="css/assets/paysheet.png" width="20" height="20" />
                                    </div>
                                    <div>
                                        <%#(Eval("EmppaysheetAmount").ToString())%>
                                    </div>
                                </li>
                            </ItemTemplate>
                        </asp:ListView>
                    </div>
                    <!-- Paysheets Section End -->
                    <!-- Receipts Section -->
                    <article class="col-sm-4">
        <div class="data-block red">
          <header>
            <h2><span class="elusive icon-fire"></span> Receipts</h2>
          
          </header>
          <section>
            <asp:ListView ID="ListView2" runat="server">
   <LayoutTemplate>
    <div class="vticker">
	<ul>
	<asp:PlaceHolder runat="server" ID="itemPlaceholder" />
	</ul>
	</div>
   </LayoutTemplate>
   <ItemTemplate>
    <li>
     <div style="float:left;padding-right:8px;height:40px"><img src="css/assets/receipt.gif" width="20" height="20" /></div>
			<div>
			<%#(Eval("").ToString())%>
            </div>
        </li>
        
   </ItemTemplate>
</asp:ListView>


 
          </section>
         <div id="viewl"><a href="#" id="my-button7">View More</a></div>
        </div>
      </article>
                    <div id="element_to_pop_up7">
                        <asp:ListView ID="ListView4" runat="server">
                            <LayoutTemplate>
                                <div class="viewmore">
                                    <h2>
                                        Paysheets <a href="#" class="bClose" style="float: right">
                                            <img src="css/assets/close-button.png" width="30" height="30"></a></h2>
                                    <ul>
                                        <asp:PlaceHolder runat="server" ID="itemPlaceholder" />
                                    </ul>
                                </div>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <li>
                                    <div style="float: left; padding-right: 8px">
                                        <img src="css/assets/receipt.gif" width="20" height="20" />
                                    </div>
                                    <div>
                                        <%#(Eval("EmppaysheetAmount").ToString())%>
                                    </div>
                                </li>
                            </ItemTemplate>
                        </asp:ListView>
                    </div>
                    <!-- Receipts Section End-->
                </div>
                <div class="clear">
                </div>
            </div>
        </div>
        <!-- DASHBOARD CONTENT END -->
        <!-- FOOTER BEGIN -->
        <div id="footerouter">
            <div class="footer">
                <div class="footerlogo">
                    <a href="http://www.diyostech.in" target="_blank">Powered by DIYOS </a>
                </div>
                <!--    <div class="footerlogo">&nbsp;</div> -->
                <div class="footercontent">
                    <a href="#">Terms &amp; Conditions</a> | <a href="#">Privacy Policy</a> | ©
                    <asp:Label ID="lblcname" runat="server"></asp:Label>.</div>
                <div class="clear">
                </div>
            </div>
        </div>
        <!-- FOOTER END -->
        <!-- CONTENT AREA END -->
    </div>
    </form>
</body>
</html>
