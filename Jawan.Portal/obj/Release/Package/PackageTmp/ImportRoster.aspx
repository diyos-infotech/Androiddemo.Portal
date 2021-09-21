<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ImportRoster.aspx.cs" Inherits="Jawan.Portal.ImportRoster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title></title>
    <link href="css/global.css" rel="stylesheet" type="text/css" />


    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.1.3/css/bootstrap.min.css" />
    <script src="//ajax.googleapis.com/ajax/libs/jquery/1.9.1/jquery.min.js"></script>

    <style type="text/css">
        .col-md-12 {
            max-width: 98%;
        }
    </style>
    <script type="text/javascript">



        function onCalendarShown() {

            var cal = $find("calendar1");
            //Setting the default mode to month
            cal._switchMode("months", true);

            //Iterate every month Item and attach click event to it
            if (cal._monthsBody) {
                for (var i = 0; i < cal._monthsBody.rows.length; i++) {
                    var row = cal._monthsBody.rows[i];
                    for (var j = 0; j < row.cells.length; j++) {
                        Sys.UI.DomEvent.addHandler(row.cells[j].firstChild, "click", call);
                    }
                }
            }
        }

        function onCalendarHidden() {
            var cal = $find("calendar1");
            //Iterate every month Item and remove click event from it
            if (cal._monthsBody) {
                for (var i = 0; i < cal._monthsBody.rows.length; i++) {
                    var row = cal._monthsBody.rows[i];
                    for (var j = 0; j < row.cells.length; j++) {
                        Sys.UI.DomEvent.removeHandler(row.cells[j].firstChild, "click", call);
                    }
                }
            }

        }

        function call(eventElement) {
            var target = eventElement.target;
            switch (target.mode) {
                case "month":
                    var cal = $find("calendar1");
                    cal._visibleDate = target.date;
                    cal.set_selectedDate(target.date);
                    cal._switchMonth(target.date);
                    cal._blur.post(true);
                    cal.raiseDateSelectionChanged();
                    break;
            }
        }
    </script>


</head>
<body>
    <form id="form1" runat="server">
        <div>

            <script type="text/javascript">
                $(document).ready(function () {
                    $('#lnkDownloadRoster').on('click', function (e) {

                        e.preventDefault();

                        if ($("#<%=txtMonth.ClientID %>").val() == "") {

                            alert("Enter month");
                        }
                        else {

                            $("#<%=btnhidden.ClientID %>").click();

                        }
                    })


                    $('#btnImport').on('click', function (e) {

                        e.preventDefault();

                        if ($("#<%=txtMonth.ClientID %>").val() == "") {

                            alert("Enter month");
                        }
                        else if (document.getElementById("fileupload").files.length == 0) {

                            alert("Please choose file");
                        }
                        else {

                            $("#<%=btnhide.ClientID %>").click();

                        }
                    })
                });
            </script>

            <asp:ScriptManager ID="ScriptManager" runat="server"></asp:ScriptManager>
            <div id="headerouter">
                <!-- LOGO AND MAIN MENU SECTION BEGIN -->
                <div id="header">
                    <!-- LOGO BEGIN -->
                    <div id="logo">
                        <a href="Default.aspx">
                            <img border="0" src="assets/logo.png" alt="Facility Management Software" title="Facility Management Software" /></a>
                    </div>
                    <!-- LOGO END -->
                    <!-- TOP INFO BEGIN -->
                    <div id="toplinks">
                        <ul>
                            <li>Welcome <b>
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
                        <li><a href="Reports.aspx" id="ReportsLink" runat="server" class="current"><span>Reports</span></a></li>
                        <li class="after"><a href="CreateLogin.aspx" id="SettingsLink" runat="server"><span>Settings</span></a></li>
                        <li class="last"><a href="login.aspx" id="LogOutLink" runat="server"><span><span>Logout</span></span></a></li>
                    </ul>
                </div>
                    <!-- MAIN MENU SECTION END -->
                </div>
                <!-- LOGO AND MAIN MENU SECTION END -->

            </div>
            <div class="cotainer" style="margin-top: 10px">
                <div class="row justify-content-center">
                    <div class="col-md-12">
                        <div class="card">

                            <div class="card-header">Import Roster</div>
                            <div class="card-body">

                                <table style="margin: 0px auto" width="87%">
                                    <tr>


                                        <td>
                                            <asp:Label ID="lblMonth" runat="server" Text="Month"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtMonth" runat="server" class="sinput" autocomplete="off"></asp:TextBox>
                                            <cc1:CalendarExtender ID="Txt_Month_CalendarExtender" runat="server" BehaviorID="calendar1"
                                                Enabled="true" Format="MMM-yyyy" TargetControlID="txtMonth" DefaultView="Months" OnClientHidden="onCalendarHidden" OnClientShown="onCalendarShown">
                                            </cc1:CalendarExtender>
                                        </td>

                                        <td>
                                            <asp:FileUpload ID="fileupload" runat="server" />
                                        </td>

                                        <td>
                                            <asp:Button ID="btnImport" runat="server" Text="Import" class=" btn save" ToolTip="Submit"  />
                                        </td>

                                        <td>
                                            <asp:Button ID="btnNotInsert" runat="server" Text="Not Imported Data" class=" btn save" ToolTip="Submit" OnClick="btnNotInsert_Click" Visible="false" />
                                        </td>

                                        <td>
                                            <asp:LinkButton ID="btnExport" runat="server" Text="Export Sample Sheet" class=" btn save"
                                                OnClick="btnExport_Click" />
                                        </td>

                                        <td>
                                            <asp:LinkButton ID="lnkDownloadRoster" runat="server" Text="Download Roster" class=" btn save" />

                                        </td>

                                    </tr>
                                </table>

                                            <asp:Button ID="btnhidden" runat="server" Text="Submit" class="btn cnt-create-btn" Style="font-size: 12px; visibility: hidden" OnClick="lnkDownloadRoster_Click"></asp:Button>
                                            <asp:Button ID="btnhide" runat="server" Text="Submit" class="btn cnt-create-btn" Style="font-size: 12px; visibility: hidden" OnClick="btnImport_Click"></asp:Button>


                                <div>
                                    <asp:GridView ID="GVAttendanceData" runat="server" AutoGenerateColumns="true" CssClass="table table-striped table-bordered table-condensed table-hover" Visible="false">
                                        <Columns>
                                        </Columns>
                                    </asp:GridView>

                                </div>

                                <div style="margin-top: 10px">
                                    <asp:GridView ID="GvEmpList" runat="server" AutoGenerateColumns="true" CssClass="table table-striped table-bordered table-condensed table-hover">
                                        <Columns>
                                        </Columns>
                                    </asp:GridView>
                                </div>

                            </div>
                        </div>

                    </div>
                </div>
                <div class="clear">
                </div>
            </div>
            <!-- DASHBOARD CONTENT END -->
            <%-- </div> </div>--%>
            <!-- CONTENT AREA END -->
            <!-- FOOTER BEGIN -->
        </div>

    </form>
</body>
</html>
