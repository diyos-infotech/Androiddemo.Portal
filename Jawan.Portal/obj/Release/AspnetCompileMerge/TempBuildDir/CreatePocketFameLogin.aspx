<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreatePocketFameLogin.aspx.cs" Inherits="Jawan.Portal.CreatePocketFameLogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../css/global.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="//code.jquery.com/ui/1.11.2/themes/smoothness/jquery-ui.css" />
    <script type="text/javascript" src="//code.jquery.com/jquery-1.10.2.js"></script>
    <script type="text/javascript" src="//code.jquery.com/ui/1.11.2/jquery-ui.js"></script>
    <style type="text/css">
        .style1 {
            width: 135px;
        }
    </style>

    <script type="text/javascript">

        function dtval(d, e) {
            var pK = e ? e.which : window.event.keyCode;
            if (pK == 8) { d.value = substr(0, d.value.length - 1); return; }
            var dt = d.value;
            var da = dt.split('/');
            for (var a = 0; a < da.length; a++) { if (da[a] != +da[a]) da[a] = da[a].substr(0, da[a].length - 1); }
            if (da[0] > 31) { da[1] = da[0].substr(da[0].length - 1, 1); da[0] = '0' + da[0].substr(0, da[0].length - 1); }
            if (da[1] > 12) { da[2] = da[1].substr(da[1].length - 1, 1); da[1] = '0' + da[1].substr(0, da[1].length - 1); }
            if (da[2] > 9999) da[1] = da[2].substr(0, da[2].length - 1);
            dt = da.join('/');
            if (dt.length == 2 || dt.length == 5) dt += '/';
            d.value = dt;
        }

        function bindautofilldesgs() {
            $(".txtautofillempid").autocomplete({
                source: eval($("#hdempid").val()),
                minLength: 4
            });
        }

    </script>
    <script type="text/javascript">

        function GetEmpid() {

            $('#txtEmpid').autocomplete({
                source: function (request, response) {


                    $.ajax({
                        url: 'Autocompletion.asmx/GetFormEmpIDs',
                        method: 'post',
                        contentType: 'application/json;charset=utf-8',
                        data: JSON.stringify({
                            term: request.term,
                        }),
                        datatype: 'json',
                        success: function (data) {
                            response(data.d);
                        },
                        error: function (err) {
                            alert(err);
                        }
                    });
                },
                minLength: 4,
                select: function (event, ui) {
                    $("#txtEmpid").attr("data-EmpName", ui.item.value); OnAutoCompletetxtEmpidchange(event, ui);
                }
            });

        }

        function GetEmpName() {

            $('#txtName').autocomplete({
                source: function (request, response) {
                    $.ajax({

                        url: 'Autocompletion.asmx/GetFormEmpNames',
                        method: 'post',
                        contentType: 'application/json;charset=utf-8',
                        data: JSON.stringify({
                            term: request.term
                        }),
                        datatype: 'json',
                        success: function (data) {
                            response(data.d);
                        },
                        error: function (err) {
                            alert(err);
                        }
                    });
                },
                minLength: 4,
                select: function (event, ui) {
                    $("#txtName").attr("data-EmpName", ui.item.value); OnAutoCompletetxtEmpNamechange(event, ui);
                }
            });

        }

        function OnAutoCompletetxtEmpidchange(event, ui) {
            $("#<%=txtEmpid.ClientID %>").trigger('change');

        }
        function OnAutoCompletetxtEmpNamechange(event, ui) {
            $("#<%=txtName.ClientID %>").trigger('change');

        }

        $(document).ready(function () {

            GetEmpid();
            GetEmpName();
        });



    </script>

    <style type="text/css">
        .style1 {
            width: 135px;
        }

        .completionList {
            background: white;
            border: 1px solid #DDD;
            border-radius: 3px;
            box-shadow: 0 0 5px rgba(0, 0, 0, 0.1);
            min-width: 165px;
            height: 120px;
            overflow: auto;
        }

        .listItem {
            display: block;
            padding: 5px 5px;
            border-bottom: 1px solid #DDD;
        }

        .itemHighlighted {
            color: black;
            background-color: rgba(0, 0, 0, 0.1);
            text-decoration: none;
            box-shadow: 0 0 5px rgba(0, 0, 0, 0.1);
            border-bottom: 1px solid #DDD;
            display: block;
            padding: 5px 5px;
        }
    </style>

    <script type="text/javascript">

        $(document).ready(function () {
            $("#<%=txtConfirmPassword.ClientID %>").keyup(validate);
        });


        function validate() {
            var password1 = $("#<%=txtPassword.ClientID %>").val();
            var password2 = $("#<%=txtConfirmPassword.ClientID %>").val();



            if (password1 == password2) {
                $("#<%=lblerror.ClientID %>").text("");
            }
            else {
                $("#<%=lblerror.ClientID %>").text("invalid");
            }

        }

        function Check(evt) {
            if (evt.keyCode == 32) {
                alert("Space not allowed");
                return false;
            }
            return true;
        }


        function onChangeTest(evt) {
            debugger;
            if (evt.value == "") {
                alert("Error: Username cannot be blank!");
                document.getElementById("<%=txtPassword.ClientID %>").value = "";
                form.username.focus();
                return false;
            }

            if (evt.value != "") {
                if (evt.value.length < 8) {
                    alert("Error: Password must contain at least eight characters!");
                    document.getElementById("<%=txtPassword.ClientID %>").value = "";
                    evt.focus();
                    return false;
                }

                re = /[0-9]/;
                if (!re.test(evt.value)) {
                    alert("Error: password must contain at least one number (0-9)!");
                    document.getElementById("<%=txtPassword.ClientID %>").value = "";
                    evt.focus();
                    return false;
                }
                re = /^[a-zA-Z]/;
                if (!re.test(evt.value)) {
                    alert("Error: password must contain at least one or more letter characters (A-Za-z)!");
                    document.getElementById("<%=txtPassword.ClientID %>").value = "";
                    evt.focus();
                    return false;
                }
            } else {
                alert("Error: Please check that you've entered and confirmed your password!");
                form.pwd1.focus();
                return false;
            }

            alert("You entered a valid password: " + evt.value);
            return true;
        }

    </script>

</head>
<body>
    <form id="CreateLogin1" runat="server">
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
                        <li><a href="Reminders.aspx">Reminders</a></li>
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
                        <li><a href="Reports.aspx" id="ReportsLink" runat="server"><span>Reports</span></a></li>
                        <li><a href="Settings.aspx" id="SettingsLink" runat="server" class="current"><span>Settings</span></a></li>
                        <%--    <li class="current"><a href="companyinfo.aspx" id="CompanyInfoLink" runat="server"><span>Company Info</span></a></li>
                        --%>
                        <li class="after last"><a href="login.aspx" id="LogOutLink" runat="server"><span><span>Logout</span></span></a></li>
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
                                    <div id="submenu" class="submenu">
                                        <div class="submenubeforegap">
                                            &nbsp;
                                        </div>
                                        <div class="submenuactions">
                                            &nbsp;
                                        </div>
                                        <ul>
                                            <li class="current"><a href="Settings.aspx" id="creak" runat="server"><span>Main</span></a>
                                            </li>
                                        </ul>
                                    </div>
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
        <div id="content-holder">
            <div class="content-holder">
                <div id="breadcrumb">
                    <ul class="crumbs">
                        <li class="first"><a href="Settings.aspx" style="z-index: 9;"><span></span>Settings</a></li>
                        <li class="active"><a href="#" style="z-index: 7;" class="active_bread">Create Pocket FaME Login</a></li>
                    </ul>
                </div>
                <!-- DASHBOARD CONTENT BEGIN -->
                <div class="contentarea" id="contentarea">

                    <div class="dashboard_center">
                        <div class="sidebox">
                            <div class="boxhead">
                                <h2 style="text-align: center">Create Pocket FaME Login
                                </h2>
                            </div>
                            <div class="boxbody" style="padding: 5px 5px 5px 5px;">
                                <div class="boxin">

                                    <asp:ScriptManager runat="server" ID="ScriptEmployReports">
                                    </asp:ScriptManager>

                                    <asp:HiddenField ID="hdempid" runat="server" />

                                    <table style="width: 100%">

                                        <tr style="height: 40px">

                                            <td>Emp ID :<span style="color: Red">*</span>
                                            </td>

                                            <td>Emp Name :<span style="color: Red">*</span>
                                            </td>

                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtEmpid" runat="server" Width="50%" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtemplyid_TextChanged"></asp:TextBox>
                                            </td>

                                            <td>
                                                <asp:TextBox ID="txtName" runat="server" Width="50%" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtFname_TextChanged"></asp:TextBox>
                                            </td>
                                        </tr>








                                        <tr style="height: 40px">
                                            <td>Role<span style="color: Red">*</span>
                                            </td>
                                            <td>Shift<span style="color: Red">*</span>
                                            </td>
                                        </tr>
                                        <tr style="height: 40px">
                                            <td>
                                                <asp:DropDownList ID="ddlrole" runat="server" CssClass="form-control" Width="55%"></asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlshift" runat="server" CssClass="form-control" Width="55%" AutoPostBack="true" OnSelectedIndexChanged="ddlShift_SelectedIndexChanged"></asp:DropDownList>
                                            </td>
                                        </tr>


                                        <tr style="height: 40px">
                                            <td>Shift Start Time :<span style="color: Red">*</span>
                                            </td>

                                            <td>Shift End Time :<span style="color: Red">*</span>
                                            </td>

                                        </tr>
                                        <tr style="height: 40px">
                                            <td>
                                                <asp:TextBox ID="txtShiftstarttime" TabIndex="2" runat="server" Enabled="false" class="form-control" Width="50%"></asp:TextBox>
                                            </td>

                                            <td>
                                                <asp:TextBox ID="txtShiftEndtime" TabIndex="2" runat="server" Enabled="false" class="form-control" Width="50%"></asp:TextBox>
                                            </td>
                                        </tr>


                                        <tr style="height: 40px" runat="server">


                                            <td>Site Posted To :<span style="color: Red">*</span>
                                            </td>

                                            <td>User Name :<span style="color: Red">*</span>
                                            </td>

                                        </tr>
                                        <tr style="height: 40px" runat="server">
                                            <td>
                                                <asp:DropDownList ID="ddlsiteposted" runat="server" CssClass="form-control" Width="55%" AutoPostBack="true"></asp:DropDownList>
                                            </td>

                                            <td>
                                                <asp:TextBox ID="txtusrname" runat="server" Text="" onkeydown="return Check(event)" onpaste="return false;" CssClass="form-control" Width="50%"></asp:TextBox>
                                            </td>

                                        </tr>


                                        <tr style="height: 40px">
                                            <td>Password:<span style="color: Red">*</span></td>

                                            <td>Confirm Password :<span style="color: Red">*</span>
                                            </td>

                                        </tr>
                                        <tr style="height: 40px">
                                            <td>
                                                <asp:TextBox ID="txtPassword" runat="server" onchange="onChangeTest(this)" onkeydown="return Check(event)" onpaste="return false;" CssClass="form-control" Width="50%"></asp:TextBox>

                                            </td>

                                            <td>
                                                <asp:TextBox ID="txtConfirmPassword" TextMode="Password" runat="server" onpaste="return false;" CssClass="form-control" Width="50%"></asp:TextBox>
                                                <asp:Label ID="lblerror" runat="server" ForeColor="Red"></asp:Label>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td></td>
                                            <td>
                                                <asp:Button ID="btnsave" runat="server" Text="Save" OnClick="BtnSave_Click" />
                                            </td>
                                        </tr>

                                    </table>


                                </div>
                            </div>
                        </div>
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
                    <asp:Label ID="lblcname" runat="server"></asp:Label>.
                    </div>
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
