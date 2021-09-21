<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClientAttendancePage.aspx.cs" Inherits="Jawan.Portal.ClientAttendancePage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>EMPLOYEE ATTENDANCE</title>
    <link rel="shortcut icon" href="assets/Mushroom.ico" />
    <link rel="stylesheet" href="css/global.css" />
    <link href="css/boostrap/css/bootstrap.css" rel="stylesheet" />
    <link rel="stylesheet" href="//code.jquery.com/ui/1.11.2/themes/smoothness/jquery-ui.css" />

    <script type="text/javascript" src="//code.jquery.com/jquery-1.10.2.js"></script>

    <script type="text/javascript" src="//code.jquery.com/ui/1.11.2/jquery-ui.js"></script>

    <script type="text/javascript">
        //<![CDATA[
        window.onbeforeunload = function () {
            return '';
        };
        //]]>
        (function ($) {
            $.widget("custom.combobox", {
                _create: function () {
                    this.wrapper = $("<span>")
          .addClass("custom-combobox")
          .insertAfter(this.element);

                    this.element.hide();
                    this._createAutocomplete();
                    this._createShowAllButton();
                    this.input.attr("placeholder", this.element.attr('data-placeholder'));
                },

                _createAutocomplete: function () {
                    var selected = this.element.children(":selected"),
          value = selected.val() ? selected.text() : "";

                    this.input = $("<input>")
          .appendTo(this.wrapper)
          .val(value)
          .attr("title", "")
          .addClass("custom-combobox-input ui-widget ui-widget-content ui-state-default ui-corner-left")
          .autocomplete({
              delay: 0,
              minLength: 0,
              source: $.proxy(this, "_source")
          })
          .tooltip({
              tooltipClass: "ui-state-highlight"
          });

                    this._on(this.input, {
                        autocompleteselect: function (event, ui) {
                            ui.item.option.selected = true;
                            this._trigger("select", event, {
                                item: ui.item.option
                            });
                        },

                        autocompletechange: "_removeIfInvalid"
                    });
                },

                _createShowAllButton: function () {
                    var input = this.input,
          wasOpen = false;

                    $("<a>")
          .attr("tabIndex", -1)
          .attr("title", "Show All")
          .tooltip()
          .appendTo(this.wrapper)
          .button({
              icons: {
                  primary: "ui-icon-triangle-1-s"
              },
              text: false
          })
          .removeClass("ui-corner-all")
          .addClass("custom-combobox-toggle ui-corner-right btnhgtwt")
          .mousedown(function () {
              wasOpen = input.autocomplete("widget").is(":visible");
          })
          .click(function () {
              input.focus();

              // Close if already visible
              if (wasOpen) {
                  return;
              }

              // Pass empty string as value to search for, displaying all results
              input.autocomplete("search", "");
          });
                },

                _source: function (request, response) {
                    var matcher = new RegExp($.ui.autocomplete.escapeRegex(request.term), "i");
                    response(this.element.children("option").map(function () {
                        var text = $(this).text();
                        if (this.value && (!request.term || matcher.test(text)))
                            return {
                                label: text,
                                value: text,
                                option: this
                            };
                    }));
                },

                _removeIfInvalid: function (event, ui) {

                    // Selected an item, nothing to do
                    if (ui.item) {
                        return;
                    }

                    // Search for a match (case-insensitive)
                    var value = this.input.val(),
          valueLowerCase = value.toLowerCase(),
          valid = false;
                    this.element.children("option").each(function () {
                        if ($(this).text().toLowerCase() === valueLowerCase) {
                            this.selected = valid = true;
                            return false;
                        }
                    });

                    // Found a match, nothing to do
                    if (valid) {
                        return;
                    }

                    // Remove invalid value
                    this.input
          .val("")
          .attr("title", value + " didn't match any item")
          .tooltip("open");
                    this.element.val("");
                    this._delay(function () {
                        this.input.tooltip("close").attr("title", "");
                    }, 2500);
                    this.input.autocomplete("instance").term = "";
                },

                _destroy: function () {
                    this.wrapper.remove();
                    this.element.show();
                }
            });
        })(jQuery);

        // forceNumeric() plug-in implementation
        jQuery.fn.forceNumeric = function () {

            return this.each(function () {
                $(this).keydown(function (e) {
                    var key = e.which || e.keyCode;

                    if (!e.shiftKey && !e.altKey && !e.ctrlKey &&
                        // numbers   
                         key >= 48 && key <= 57 ||
                        // Numeric keypad
                         key >= 96 && key <= 105 ||
                        // comma, period and minus, . on keypad
                        key == 190 || key == 188 || key == 109 || key == 110 ||
                        // Backspace and Tab and Enter
                        key == 8 || key == 9 || key == 13 ||
                        // Home and End
                        key == 35 || key == 36 ||
                        // left and right arrows
                        key == 37 || key == 39 ||
                        // Del and Ins
                        key == 46 || key == 45)
                        return true;

                    return false;
                });
                $(this).keydown(function (e) {
                    CalculateTotals();
                    var linetotal = 0;
                    $(this).parent().parent().find(".num-txt").each(function () {
                        linetotal += parseInt($(this).val());
                    });
                    $(this).parent().parent().find(".txt-linetotal").text(linetotal);
                });
            });
        }

        $(document).ready(function () {
            $(".num-txt").forceNumeric();
            $(".txt-calender").datepicker({ defaultDate: new Date(), dateFormat: 'dd/mm/yy' });
            var tdate = new Date();
            $(".txt-calender").val(getFormattedDate(tdate));
            GetClientsValues();
            $(".ddlautocomplete").combobox({
                select: function (event, ui) { $("#divClient").attr("data-clientId", ui.item.value); OnAutoCompleteDDLchange(event, ui); },
                minLength: 4
            });

            $("#txtEmpId").autocomplete({
                source: function (request, response) {
                    var ajaxUrl = window.location.href.substring(0, window.location.href.lastIndexOf('/')) + "/FameService.asmx/GetEmployessData";
                    $.ajax({
                        type: "POST",
                        url: ajaxUrl,
                        data: "{strid:" + request.term + "}",
                        async: false,
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (json) {
                            if (json != "") {
                                // var data = eval(json.d);
                                response($.map(json, function (item) {
                                    var obj = { value: item.EmpDesg + "|<>|" + item.EmpName, label: item.EmpId };
                                    return obj;
                                }));
                            }
                        },
                        error: function (json) { InvalidEmpData(); }
                    });
                },
                minLength: 4,
                select: function (event, ui) {
                    $("#trAddData").removeAttr("data-emp-id");
                    $("#trAddData").removeAttr("data-emp-name");
                    $("#trAddData").removeAttr("data-emp-desg");
                    var vals = ui.item.value.split('|<>|');
                    $("#txtEmpName").val(vals[1]);
                    $("#ddlEmpDesg").val(vals[0]);
                    $("#trAddData").attr("data-emp-id", ui.item.label);
                    $("#trAddData").attr("data-emp-name", vals[1]);
                    $("#trAddData").attr("data-emp-desg", vals[0]);
                    this.value = ui.item.label
                    return false;
                }
            });

            $("#txtEmpName").autocomplete({
                source: function (request, response) {
                    var ajaxUrl = window.location.href.substring(0, window.location.href.lastIndexOf('/')) + "/FameService.asmx/GetEmployessNameData";
                    var dataitem = JSON.stringify({ "strname": request.term });
                    $.ajax({
                        type: "POST",
                        url: ajaxUrl,
                        data: { strname: request.term },
                        data: dataitem,
                        async: false,
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (json) {
                            if (json != "") {
                                // var data = eval(json.d);
                                response($.map(json, function (item) {
                                    var obj = { value: item.EmpDesg + "|<>|" + item.EmpId, label: item.EmpName };
                                    return obj;
                                }));
                            }
                        },
                        error: function (json) { InvalidEmpData(); }
                    });
                },
                minLength: 4,
                select: function (event, ui) {
                    $("#trAddData").removeAttr("data-emp-id");
                    $("#trAddData").removeAttr("data-emp-name");
                    $("#trAddData").removeAttr("data-emp-desg");
                    var vals = ui.item.value.split('|<>|');
                    $("#txtEmpId").val(vals[1]);
                    $("#ddlEmpDesg").val(vals[0]);
                    $("#trAddData").attr("data-emp-id", vals[1]);
                    $("#trAddData").attr("data-emp-name", ui.item.label);
                    $("#trAddData").attr("data-emp-desg", vals[0]);
                    this.value = ui.item.label
                    return false;
                }
            });


        });

        function InvalidEmpData() {
            $("#txtEmpName").val("");
            $("#txtEmpId").val("");
            $("#trAddData").attr("data-emp-id", "");
            $("#trAddData").attr("data-emp-name", "");
            $("#trAddData").attr("data-emp-desg", "");
            alert("invalid!!");
        }

        function GetClientsValues() {
            var json = JSON.parse($("#hdClientData").val());
            $("#divClient").attr("data", JSON.stringify(json));
            var data = json;
            BindClientIdDDL(data);
            BindClientNameDDL(data);
        }

        function BindClientIdDDL(data) {
            $("#ddlClientID").html("");
            $("#ddlClientID").append("<option value='-1'></option>");

            var databs = [];
            $.each(data, function (index, element) {
                databs.push(element.ClientId);
            });
            databs.sort();
            $.each(databs, function (index, element) {
                $("#ddlClientID").append("<option value=" + element + ">" + element + "</option>");
            });
        }

        function BindClientNameDDL(data) {
            $("#ddlClientName").html("");
            $("#ddlClientName").append("<option value='-1'></option>");
            $.each(data, function (index, element) {
                $("#ddlClientName").append("<option value=" + element.ClientId + ">" + element.ClientName + "</option>");
            });
        }

        function SetAutoCompleteValue(ddlid, defValue) {
            var selector = "#" + ddlid + " option";
            $("#" + ddlid).combobox("destroy");
            $(selector).each(function (indx, ele) {
                if (($(ele).attr('value')) === defValue) {
                    $(ele).attr('selected', 'selected');
                }
            });
            $("#" + ddlid).combobox({
                select: function (event, ui) { OnAutoCompleteDDLchange(event, ui); }
            });
        }

        function OnAutoCompleteDDLchange(event, ui) {
            var targetddlid = "";
            if (event.target.id === "ddlClientID") { targetddlid = "ddlClientName"; }
            else if (event.target.id === "ddlClientName") targetddlid = "ddlClientID";
            SetAutoCompleteValue(targetddlid, ui.item.value);
            if (targetddlid == "ddlClientName" || targetddlid == "ddlClientID")
            { ChangeClientValues(ui.item.value); }
        }

        function ChangeClientValues(cid) {
            var datastr = $("#divClient").attr("data");
            var data = eval(datastr);
            $.each(data, function (indx, ele) {
                if (ele.ClientId == cid) {
                    $("#txtphonenumbers").val(ele.PhoneNumber);
                    $("#txtocp").val(ele.ContactPerson);
                    $("#ddlMonth").val(0);
                }
            });
            $("#tblattendancegrid >tbody").html("");
            $(".num-txt").forceNumeric();
            CalculateTotals();
        }

        function AddNewEmp(ele) {
            var empid = $("#txtEmpId").val();
            var empname = $("#txtEmpName").val();
            var empdesgid = $("#ddlEmpDesg").val();
            var empdesgname = $("#ddlEmpDesg option:selected").text();
            var empttype = $("#ddlTransfertype option:selected").val();
            var jdate = $("#txtJoingingDate").val();
            var rdate = $("#txtRelievingDate").val();
            var esi = $("#chkESI").is(":checked");
            var pt = $("#chkPT").is(":checked");
            var pf = $("#chkPF").is(":checked");
            var nod = $("#txt-add-nod").val();
            var ot = $("#txt-add-ot").val();
            var wo = $("#txt-add-wo").val();
            var nhs = $("#txt-add-nhs").val();
            var npots = $("#txt-add-npots").val();
            var ldays = $("#txt-add-ldays").val();
            var canadv = $("#txt-add-canadv").val();
            var pen = $("#txt-add-pen").val();
            var inctvs = $("#txt-add-inctvs").val();
            var updated = false;
            if ($('#tblattendancegrid > tbody > tr').length > 0) {
                $('#tblattendancegrid > tbody > tr').each(function (i, row) {
                    var trempid = $(row).attr("data-emp-id");
                    var trempdesg = $(row).attr("data-emp-desg");
                    if (empid == trempid && empdesgid == trempdesg) {
                        $(row).find(".txt-nod").val(nod);
                        $(row).find(".txt-ot").val(ot);
                        $(row).find(".txt-wo").val(wo);
                        $(row).find(".txt-nhs").val(nhs);
                        $(row).find(".txt-nposts").val(npots);
                        $(row).find(".txt-ldays").val(ldays);
                        $(row).find(".txt-candav").val(canadv);
                        $(row).find(".txt-pen").val(pen);
                        $(row).find(".txt-inctvs").val(inctvs);
                        alert("Employee attendance updated.");
                        updated = true;
                    }
                });
            }
            if (!updated) {
                var nr = "<tr class='tr-emp-att new-row' data-emp-id='##EMPID##' data-emp-desg='##EMPDESG##' data-emp-ttype='##EMPTTYPE##' data-emp-jdate='##EMPJDATE##' data-emp-rdate='##EMPRDATE##' data-emp-pf='##EMPPF##' data-emp-pt='##EMPPT##' data-emp-esi='##EMPESI##' >" +
                                 "<td>##EMPID##</td><td>##EMPNAME##</td><td>##EMPDESGNAME##</td>" +
                                 "<td><input type='text' class='form-control num-txt txt-nod line-cal' value='##NOD##'></td>" +
                                " <td><input type='text' class='form-control num-txt txt-ot line-cal' value='##OT##'></td>" +
                                 "<td><input type='text' class='form-control num-txt txt-wo line-cal' value='##WO##'></td>" +
                                " <td><input type='text' class='form-control num-txt txt-nhs line-cal' value='##NHS##'></td>" +
                                " <td><input type='text' class='form-control num-txt txt-nposts line-cal' value='##NPOSTS##'></td>" +
                                 " <td><input type='text' class='form-control num-txt txt-ldays line-cal' value='##LDAYS##'></td>" +
                                " <td><input type='text' class='form-control num-txt txt-candav' value='##CANADV##'></td>" +
                                " <td><input type='text' class='form-control num-txt txt-pen' value='##PEN##'></td>" +
                                " <td><input type='text' class='form-control num-txt txt-inctvs' value='##INCTVS##'></td>  " +
                                " <td><label class='txt-linetotal'/> " +
                                " <td><button type='button' class='btn btn-danger' onclick='DeleteRow(this); return false;'><i class='glyphicon glyphicon-trash'></i></button></td>" +
                               " </tr>";
                if (empid != "" && empdesgid != "0") {
                    var newrow = nr.replace("##EMPID##", empid).replace("##EMPID##", empid)
                              .replace('##EMPNAME##', empname)
                              .replace('##EMPDESG##', empdesgid)
                              .replace('##EMPDESGNAME##', empdesgname)
                              .replace('##EMPJDATE##', jdate)
                              .replace('##EMPRDATE##', rdate)
                              .replace('##EMPPF##', pf)
                              .replace('##EMPPT##', pt)
                              .replace('##EMPESI##', esi)
                              .replace('##EMPTTYPE##', empttype)
                              .replace('##NOD##', nod)
                              .replace('##OT##', ot)
                              .replace('##WO##', wo)
                              .replace('##NHS##', nhs)
                              .replace('##NPOSTS##', npots)
                              .replace('##LDAYS##', ldays)
                              .replace('##CANADV##', canadv)
                              .replace('##PEN##', pen)
                              .replace('##INCTVS##', inctvs);
                    $("#tblattendancegrid >tbody").append(newrow);
                    alert("Employee added.");
                }
                else {
                    alert("Select Employee and Designation");
                }
            }
            $(".num-txt").forceNumeric();
            CalculateTotals();
            ClearEmpAddValues();
        }

        function ClearEmpAddValues() {
            $("#txtEmpId").val("");
            $("#txtEmpName").val("");
            $("#ddlEmpDesg").val(0);
            $("#ddlTransfertype").val(0);
            var tdate = new Date();
            $(".txt-calender").val(getFormattedDate(tdate));
            $("#chkESI")[0].checked = true;
            $("#chkPT")[0].checked = true;
            $("#chkPF")[0].checked = true;
            $("#txt-add-nod").val("0");
            $("#txt-add-ot").val("0");
            $("#txt-add-wo").val("0");
            $("#txt-add-nhs").val("0");
            $("#txt-add-npots").val("0");
            $("#txt-add-ldays").val("0");
            $("#txt-add-canadv").val("0");
            $("#txt-add-pen").val("0");
            $("#txt-add-inctvs").val("0");
            $("#txtEmpId").focus();
        }

        function DeleteRow(ele) {
            if (confirm("Are you sure you want to remove the employee from this unit?")) {
                if ($(ele).parent().parent().hasClass("new-row")) {
                    $(ele).parent().parent().remove();
                    alert("Employee deleted for current month.");
                }
                else {
                    var trclientId = $("#divClient").attr("data-clientId");
                    var trmonth = $("#ddlMonth option:selected").index();
                    var trempid = $(ele).parent().parent().attr("data-emp-id");
                    var trempdesg = $(ele).parent().parent().attr("data-emp-desg");
                    var ajaxUrl = window.location.href.substring(0, window.location.href.lastIndexOf('/')) + "/FameService.asmx/DeleteAttendance";
                    if (trclientId != undefined && trclientId != "0" && trclientId != "" && trmonth != undefined && trmonth != "0") {
                        var dataparam = JSON.stringify({ empId: trempid, empDesgId: trempdesg, clientId: trclientId, month: trmonth });
                        $.ajax({
                            type: "POST",
                            url: ajaxUrl,
                            data: dataparam,
                            async: false,
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (json) {
                                if (json != "") {
                                    $(ele).parent().parent().remove();
                                    alert("Employee deleted for current month.");
                                }
                            },
                            error: function (json) { alert(json); }
                        });
                    } else {
                        alert('select ClientID');
                    }
                }
                CalculateTotals();
            }
        }

        function Empddlchange(ele) {
            var id = $(ele).attr("id");
            if (id == "ddlEmpId") {
                var val = $("#ddlEmpId option:selected").val();
                var txt = $("#ddlEmpId option:selected").text();
                $("#ddlEmpName").val(txt);
                $("#ddlEmpDesg").val(val);
            }
            if (id == "ddlEmpName") {
                $("#ddlEmpId option").removeAttr("selected");
                var val = $("#ddlEmpName option:selected").val();
                var empdes = $("#ddlEmpId option:contains(" + val + ")").val();
                var empid = $("#ddlEmpId option:contains(" + val + ")").text();
                $("#ddlEmpId option").each(function () {
                    if ($(this).text() == empid) {
                        $(this).attr('selected', 'selected');
                    }
                });
                $("#ddlEmpDesg").val(empdes);
            }
        }

        function GetEmpAttendanceData() {
            openModal();
            var clientId = $("#divClient").attr("data-clientId");
            var month = $("#ddlMonth option:selected").index();
            var ajaxUrl = window.location.href.substring(0, window.location.href.lastIndexOf('/')) + "/FameService.asmx/GetAttendanceGrid";
            if (clientId != undefined && clientId != "0" && clientId != "" && month != undefined && month != "0") {
                $.ajax({
                    type: "POST",
                    url: ajaxUrl,
                    data: "{clientId:'" + clientId + "',month:'" + month + "'}",
                    async: false,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (json) {
                        if (json != "") {
                            if (json.msg == "success") {
                                AddrowstoTbl(json.Obj);
                            }
                            else if (json.msg == "fail") {
                                alert(json.Obj);
                                $("#tblattendancegrid >tbody").html("");
                            }
                            else if (json.msg == "nodata") {
                                $("#tblattendancegrid >tbody").html("");
                            }
                        }
                    },
                    error: function (json) { alert('fail'); }
                });
            } else {
                alert("Select ClientId and month");
            }
            closeModal();
            GetEmpAttendanceDataSummarry();
        }

        function AddrowstoTbl(data) {
            data = eval(data);
            $("#tblattendancegrid >tbody").html("");
            $.each(data, function (i, item) {
                var nr = "<tr class='tr-emp-att' data-emp-id='##EMPID##' data-emp-desg='##EMPDESG##' data-emp-ttype='##EMPTTYPE##' data-emp-jdate='##EMPJDATE##' data-emp-rdate='##EMPRDATE##' data-emp-pf='##EMPPF##' data-emp-pt='##EMPPT##' data-emp-esi='##EMPESI##' >" +
                                 "<td>##EMPID##</td><td>##EMPNAME##</td><td>##EMPDESGNAME##</td>" +
                                 "<td><input type='text' class='form-control num-txt txt-nod line-cal' value='##NOD##'></td>" +
                                " <td><input type='text' class='form-control num-txt txt-ot line-cal' value='##OT##'></td>" +
                                 "<td><input type='text' class='form-control num-txt txt-wo line-cal' value='##WO##'></td>" +
                                " <td><input type='text' class='form-control num-txt txt-nhs line-cal' value='##NHS##'></td>" +
                                " <td><input type='text' class='form-control num-txt txt-nposts line-cal' value='##NPOSTS##'></td>" +
                                 " <td><input type='text' class='form-control num-txt txt-ldays line-cal' value='##LDAYS##'></td>" +
                                " <td><input type='text' class='form-control num-txt txt-candav' value='##CANADV##'></td>" +
                                " <td><input type='text' class='form-control num-txt txt-pen' value='##PEN##'></td>" +
                                " <td><input type='text' class='form-control num-txt txt-inctvs' value='##INCTVS##'></td>  " +
                                " <td><label class='txt-linetotal'/> " +
                                " <td><button type='button' class='btn btn-danger' onclick='DeleteRow(this); return false;'><i class='glyphicon glyphicon-trash'></i></button></td>" +
                               " </tr>";

                var newrow = nr.replace("##EMPID##", item.EmpId).replace("##EMPID##", item.EmpId)
                          .replace('##EMPNAME##', item.EmpName)
                          .replace('##EMPDESG##', item.DesgId)
                          .replace('##EMPDESGNAME##', item.DesgName)
                          .replace('##NOD##', item.NoOfDuties)
                          .replace('##OT##', item.OT)
                          .replace('##WO##', item.WO)
                          .replace('##NHS##', item.NHS)
                          .replace('##NPOSTS##', item.NPosts)
                          .replace('##LDAYS##', item.ldays)
                          .replace('##CANADV##', item.CanteenAdv)
                          .replace('##PEN##', item.Penalty)
                          .replace('##INCTVS##', item.Incentivs);
                $("#tblattendancegrid >tbody").append(newrow);
            });

            $(".num-txt").forceNumeric();
            CalculateTotals();
        }

        function GetEmpAttendanceDataSummarry() {
            openModal();
            var clientId = $("#divClient").attr("data-clientId");
            var month = $("#ddlMonth option:selected").index();
            var ajaxUrl = window.location.href.substring(0, window.location.href.lastIndexOf('/')) + "/FameService.asmx/GetAttendanceSummary";
            if (clientId != undefined && clientId != "0" && clientId != "" && month != undefined && month != "0") {
                $.ajax({
                    type: "POST",
                    url: ajaxUrl,
                    data: "{clientId:'" + clientId + "',month:'" + month + "'}",
                    async: false,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (json, b, c) {
                        if (json != "") {
                            //var res = JSON.parse(json.d);
                            if (json.msg == "success") {
                                AddSummaryTbl(json.Obj);
                                $("#divSummary").show();
                            }
                            else if (json.msg == "nodata") {
                                $("#tblSummary >tbody").html("");
                                $("#divSummary").hide();
                            }
                        }
                    },
                    error: function (json) { alert('fail'); }
                });
            }
            closeModal();
        }

        function AddSummaryTbl(data) {
            data = eval(data);
            $("#tblSummary >tbody").html("");
            $.each(data, function (i, item) {
                var strr = "<tr class='tr-emp-summary'>" +
                   " <td><label class='lbl-tdesg lbl-thin'>##Designation##</label></td>" +
                    "<td><label class='lbl-tnod lbl-thin lbl-tots'>##TNOD##</label></td>" +
                    "<td><label class='lbl-tot lbl-thin lbl-tots'>##TOT##</label></td>" +
                    "<td><label class='lbl-two lbl-thin lbl-tots'>##TWO##</label></td>" +
                    "<td><label class='lbl-tnhs lbl-thin lbl-tots'>##TNHS##</label></td>" +
                    "<td><label class='lbl-tnpots lbl-thin lbl-tots'>##TNPOTS##</label></td>" +
                     "<td><label class='lbl-tldays lbl-thin lbl-tots'>##TLDAYS##</label></td>" +
                        "<td><label class='lbl-Totals'></label></td>" +
                    "<td><label class='lbl-tcadv lbl-thin'>##TCADV##</label></td>" +
                    "<td><label class='lbl-tpen lbl-thin'>##TPEN##</label></td>" +
                    "<td><label class='lbl-tinctvs lbl-thin'>##TINTVS##</label></td></tr>";
                var newrow = strr.replace("##Designation##", item.DesgName)
                            .replace('##TNOD##', item.NODTotal)
                            .replace('##TOT##', item.OTTotal)
                            .replace('##TWO##', item.WOTotal)
                            .replace('##TNHS##', item.NHSTotal)
                            .replace('##TNPOTS##', item.NpotsTotal)
                            .replace('##TLDAYS##', item.ldaystotal)
                            .replace('##TCADV##', item.PenTotal)
                            .replace('##TPEN##', item.InctvsTotal)
                            .replace('##TINTVS##', item.CanAdvTotal);
                $("#tblSummary >tbody").append(newrow);
            });
            CalculateSummaryTotals();
        }

        function CalculateTotals() {
            var nodtotal = 0;
            $('.txt-nod').each(function () {
                if ($(this).val() != "" && $(this).val() != undefined) {
                    nodtotal += parseFloat($(this).val());
                }
            });
            $("#lblNOD").text(nodtotal);

            var ottotal = 0;
            $('.txt-ot').each(function () {
                if ($(this).val() != "" && $(this).val() != undefined) {
                    ottotal += parseFloat($(this).val());
                }
            });
            $("#lblOT").text(ottotal);

            var wototal = 0;
            $('.txt-wo').each(function () {
                if ($(this).val() != "" && $(this).val() != undefined) {
                    wototal += parseFloat($(this).val());
                }
            });
            $("#lblWO").text(wototal);

            var nhstotal = 0;
            $('.txt-nhs').each(function () {
                if ($(this).val() != "" && $(this).val() != undefined) {
                    nhstotal += parseFloat($(this).val());
                }
            });
            $("#lblNHS").text(nhstotal);

            var npoststotal = 0;
            $('.txt-nposts').each(function () {
                if ($(this).val() != "" && $(this).val() != undefined) {
                    npoststotal += parseFloat($(this).val());
                }
            });
            $("#lblNpots").text(npoststotal);


            var ldaystotal = 0;
            $('.txt-ldays').each(function () {
                if ($(this).val() != "" && $(this).val() != undefined) {
                    ldaystotal += parseFloat($(this).val());
                }
            });
            $("#lblLDays").text(ldaystotal);

            var candavtotal = 0;
            $('.txt-candav').each(function () {
                if ($(this).val() != "" && $(this).val() != undefined) {
                    candavtotal += parseFloat($(this).val());
                }
            });
            $("#lblCanAdv").text(candavtotal);

            var pentotal = 0;
            $('.txt-pen').each(function () {
                if ($(this).val() != "" && $(this).val() != undefined) {
                    pentotal += parseFloat($(this).val());
                }
            });
            $("#lblPen").text(pentotal);
            var inctvstotal = 0;
            $('.txt-candav').each(function () {
                if ($(this).val() != "" && $(this).val() != undefined) {
                    inctvstotal += parseFloat($(this).val());
                }
            });
            $("#lblInctvs").text(inctvstotal);

            $(".tr-emp-att").each(function () {
                var linetotal = 0;
                $(this).find(".line-cal").each(function () {
                    if ($(this).val() != "" && $(this).val() != undefined) {
                        linetotal += parseFloat($(this).val());
                    }
                });
                $(this).find(".txt-linetotal").text(linetotal);
            });
            var ttls = 0;
            $('.txt-linetotal').each(function () {
                if ($(this).val() != "" && $(this).val() != undefined) {
                    ttls += parseFloat($(this).text());
                }
            });
            $("#lblTTotals").text(ttls);

        }

        function CalculateLineTotals() {

        }

        function CalculateSummaryTotals() {
            var nodtotal = 0;
            $('.lbl-tnod').each(function () {
                nodtotal += parseFloat($(this).text());
            });
            $("#lblTNOD").text(nodtotal);

            var ottotal = 0;
            $('.lbl-tot').each(function () {
                ottotal += parseFloat($(this).text());
            });
            $("#lblTOT").text(ottotal);

            var wototal = 0;
            $('.lbl-two').each(function () {
                wototal += parseFloat($(this).text());
            });
            $("#lblTWO").text(wototal);

            var nhstotal = 0;
            $('.lbl-tnhs').each(function () {
                nhstotal += parseFloat($(this).text());
            });
            $("#lblTNHS").text(nhstotal);

            var npoststotal = 0;
            $('.lbl-tnpots').each(function () {
                npoststotal += parseFloat($(this).text());
            });
            $("#lblTNPOTS").text(npoststotal);


            var ldaystotal = 0;
            $('.lbl-tldays').each(function () {
                ldaystotal += parseFloat($(this).text());
            });
            $("#lblTLDAYS").text(ldaystotal);

            var candavtotal = 0;
            $('.lbl-tcadv').each(function () {
                candavtotal += parseFloat($(this).text());
            });
            $("#lblTCADV").text(candavtotal);

            var pentotal = 0;
            $('.lbl-tpen').each(function () {
                pentotal += parseFloat($(this).text());
            });
            $("#lblTPEN").text(pentotal);
            var inctvstotal = 0;
            $('.lbl-tinctvs').each(function () {
                inctvstotal += parseFloat($(this).text());
            });
            $("#lblTInctvs").text(inctvstotal);

            $(".tr-emp-summary").each(function () {
                var linetotal = 0;
                $(this).find(".lbl-tots").each(function () {
                    linetotal += parseFloat($(this).text());
                });
                $(this).find(".lbl-Totals").text(linetotal);
            });
            var ttls = 0;
            $('.lbl-Totals').each(function () {
                ttls += parseFloat($(this).text());
            });
            $("#lblTotals").text(ttls);
        }

        function SaveAttendance() {
            var datalst = [];
            openModal();
            var clientId = $("#divClient").attr("data-clientId");
            var month = $("#ddlMonth option:selected").index();
            var ottype = parseInt($("#ddlOTtype").val());
            if ($('#tblattendancegrid > tbody > tr').length != undefined && $('#tblattendancegrid > tbody > tr').length > 0) {
                $('#tblattendancegrid > tbody > tr').each(function (i, row) {
                    var isnewrow = $(row).hasClass("new-row");
                    var EmpAttendance = {
                        ClientId: clientId,
                        MonthIndex: month,
                        NewAdd: isnewrow,
                        EmpId: $(row).attr("data-emp-id"),
                        EmpDesg: $(row).attr("data-emp-desg"),
                        JoiningDate: (isnewrow) ? $(row).attr("data-emp-jdate") : "",
                        RelievingDate: (isnewrow) ? $(row).attr("data-emp-rdate") : "",
                        PF: (isnewrow) ? $(row).attr("data-emp-pf") : false,
                        PT: (isnewrow) ? $(row).attr("data-emp-pt") : false,
                        ESI: (isnewrow) ? $(row).attr("data-emp-esi") : false,
                        TransferType: (isnewrow) ? $(row).attr("data-emp-ttype") : 1,
                        NOD: parseFloat($(row).find(".txt-nod").val()),
                        OT: parseFloat($(row).find(".txt-ot").val()),
                        WO: parseFloat($(row).find(".txt-wo").val()),
                        NHS: parseFloat($(row).find(".txt-nhs").val()),
                        Nposts: parseFloat($(row).find(".txt-nposts").val()),
                        ldays: parseFloat($(row).find(".txt-ldays").val()),
                        CanAdv: parseFloat($(row).find(".txt-candav").val()),
                        Penality: parseFloat($(row).find(".txt-pen").val()),
                        Incentives: parseFloat($(row).find(".txt-inctvs").val()),
                        OTtype: ottype
                    };
                    datalst.push(EmpAttendance);
                });
                var ajaxUrl = window.location.href.substring(0, window.location.href.lastIndexOf('/')) + "/FameService.asmx/SaveAttendance";
                if (clientId != undefined && clientId != "0" && clientId != "" && month != undefined && month != "0") {
                    if (datalst.length > 200) {
                        var lstdata = [];
                        var startindx = 0; var looplength = 200; var nxtlooplength = 200;
                        do {
                            if (startindx > 0 && looplength < datalst.length) {
                                nxtlooplength = datalst.length - looplength;
                                looplength += nxtlooplength
                            }
                            lstdata = datalst.slice(startindx, looplength);
                            var dataparam = JSON.stringify({ lst: lstdata });
                            $.ajax({
                                type: "POST",
                                url: ajaxUrl,
                                data: dataparam,
                                async: false,
                                contentType: "application/json; charset=utf-8",
                                dataType: "json",
                                success: function (json) {
                                    if (json != "") {
                                        if (json.msg == "success") {
                                            console.log("startindx:" + startindx + " looplenth:" + looplength);
                                        }
                                        else {
                                            console.log("startindx:" + startindx + " looplenth:" + looplength);
                                            console.log(json.Obj);
                                        }
                                    }
                                },
                                error: function (json) { alert('fail'); }
                            });
                            startindx += looplength;

                        } while (startindx < datalst.length);

                        alert("Employees Attendance Saved.");
                        GetEmpAttendanceData();
                    }
                    else if (datalst.length > 0) {
                        var dataparam = JSON.stringify({ lst: datalst });
                        $.ajax({
                            type: "POST",
                            url: ajaxUrl,
                            data: dataparam,
                            async: false,
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (json) {
                                if (json != "") {
                                    if (json.msg == "success") {
                                        alert("Employees Attendance Saved.");
                                        GetEmpAttendanceData();
                                    }
                                    else {
                                        alert(json.Obj);
                                    }
                                }
                            },
                            error: function (json) { alert('fail'); }
                        });
                    }
                } else {
                    alert("Select ClientId and month.");
                }
            }
            else {
                alert("Enter Employee to Save Attendance.");
            }
            closeModal();
        }

        function openModal() {
            document.getElementById('modal').style.display = 'block';
            document.getElementById('fade').style.display = 'block';
        }

        function closeModal() {
            document.getElementById('modal').style.display = 'none';
            document.getElementById('fade').style.display = 'none';
        }

        function getFormattedDate(date) {
            var year = date.getFullYear();
            var month = (1 + date.getMonth()).toString();
            month = month.length > 1 ? month : '0' + month;
            var day = date.getDate().toString();
            day = day.length > 1 ? day : '0' + day;
            return day + '/' + month + '/' + year;
        }
    </script>

    <style type="text/css">
        .lbl-thin {
            font-weight: 100 !important;
        }

        #fade {
            display: none;
            position: absolute;
            top: 0%;
            left: 0%;
            width: 100%;
            height: 2000px;
            background-color: #ababab;
            z-index: 1001;
            -moz-opacity: 0.8;
            opacity: .70;
            filter: alpha(opacity=80);
        }

        #modal {
            display: none;
            position: absolute;
            top: 45%;
            left: 45%;
            width: 100px;
            height: 100px;
            padding: 30px 15px 0px;
            border: 3px solid #ababab;
            box-shadow: 1px 1px 10px #ababab;
            border-radius: 20px;
            background-color: white;
            z-index: 1002;
            text-align: center;
            overflow: auto;
        }

        #results {
            font-size: 1.25em;
            color: red;
        }

        .ui-autocomplete {
            max-height: 200px;
            overflow-y: auto; /* prevent horizontal scrollbar */
            overflow-x: hidden;
        }
        /* IE 6 doesn't support max-height
   * we use height instead, but this forces the menu to always be this tall
   */ * html .ui-autocomplete {
            height: 200px;
        }

        .custom-combobox {
            position: relative;
            display: inline-block;
            width: 84%;
        }

        .custom-combobox-toggle {
            position: absolute;
            top: 0;
            bottom: 0;
            margin-left: -1px;
            padding: 0;
        }

        .custom-combobox-input {
            margin: 0;
            padding: 5px 10px;
            width: 100%;
        }

        .btnhgtwt {
            top: 0px;
            height: 31px;
        }

        .num-txt {
            padding: 0 5px;
            width: 40px;
        }
    </style>
</head>
<body>
    <form id="frmClientAttendancePage" runat="server">
        <div id="headerouter">
            <div id="header" style="height: 60px;">
                <div id="logo" style="padding: 5px 0 0;">
                    <a href="Default.aspx">
                        <img border="0" src="assets/logo.png" alt="Facility Management Software" title="Facility Management Software" /></a>
                </div>
                <div id="toplinks">
                    <ul>
                        </li><li><a href="Reminders.aspx">Reminders</a></li>
                        <li>Welcome <b>
                            <asp:Label ID="lblDisplayUser" runat="server" Text="Label" Font-Bold="true"></asp:Label></b></li>
                        <li class="lang"><a href="Login.aspx">Logout</a></li>
                    </ul>
                </div>
                <div id="mainmenu">
                    <ul>
                        <li class="first"><a href="Employees.aspx" id="EmployeesLink" runat="server"><span>Employees</span></a></li>
                        <li><a href="clients.aspx" id="ClientsLink" runat="server" class="current"><span>Clients</span></a></li>
                        <li class="after"><a href="companyinfo.aspx" id="CompanyInfoLink" runat="server"><span>Company Info</span></a></li>
                        <li><a href="ViewItems.aspx" id="InventoryLink" runat="server"><span>Inventory</span></a></li>
                        <li><a href="Reports.aspx" id="ReportsLink" runat="server"><span>Reports</span></a></li>
                        <li><a href="Settings.aspx" id="SettingsLink" runat="server"><span>Settings</span></a></li>
                        <li class="last"><a href="login.aspx" id="LogOutLink" runat="server"><span><span>Logout</span></span></a></li>
                    </ul>
                </div>
            </div>
            <div id="submenu">
                <table width="100%" cellspacing="0" cellpadding="0" border="0">
                    <tbody>
                        <tr>
                            <td>
                                <div style="display: inline;">
                                    <div id="submenu" class="submenu">
                                        <%--   <div class="submenubeforegap">
                                        &nbsp;</div>
                                  <div class="submenuactions">
                                        &nbsp;</div> --%>
                                        <ul>
                                            <li><a href="clients.aspx" id="AddClientLink" runat="server"><span>Add</span></a></li>
                                            <li><a href="ModifyClient.aspx" id="ModifyClientLink" runat="server"><span>Modify</span></a></li>
                                            <li><a href="DeleteClient.aspx" id="DeleteClientLink" runat="server"><span>Delete</span></a></li>
                                            <li><a href="contracts.aspx" id="ContractLink" runat="server"><span>Contracts</span></a></li>
                                            <li><a href="ClientLicenses.aspx" id="LicensesLink" runat="server"><span>Licenses</span></a></li>
                                            <li class="current"><a href="clientattendance.aspx" id="ClientAttendanceLink" runat="server">
                                                <span>Attendance</span></a></li>
                                            <li><a href="AssigningClients.aspx" id="Operationlink" runat="server"><span>Operations</span></a></li>
                                            <li><a href="ClientBilling.aspx" id="BillingLink" runat="server"><span>Billing</span></a></li>
                                            <li><a href="MaterialRequisitForm.aspx" id="MRFLink" runat="server"><span>MRF</span></a></li>
                                            <li><a href="ApproveMRF.aspx" id="ApproveMRFLink" runat="server"><span>ApproveMRF</span></a></li>
                                            <li><a href="Receipts.aspx" id="ReceiptsLink" runat="server"><span>Receipts</span></a></li>
                                        </ul>
                                    </div>
                                </div>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
        <div id="content-holder">
            <div class="content-holder">
                <h1 class="dashboard_heading">Clients Dashboard</h1>
                <div class="row">
                    <div class="row">
                        <div id="divClient">
                            <asp:HiddenField ID="hdClientData" runat="server" />
                            <table class="table">
                                <tr>
                                    <td>
                                        <label for="exampleInputEmail1">
                                            Client Id</label>
                                    </td>
                                    <td>
                                        <select id="ddlClientID" data-placeholder="select" class="ddlautocomplete chosen-select"
                                            style="width: 350px;">
                                        </select>
                                    </td>
                                    <td>
                                        <label for="exampleInputEmail1">
                                            Client Name</label>
                                    </td>
                                    <td>
                                        <select id="ddlClientName" data-placeholder="select" class="ddlautocomplete chosen-select"
                                            style="width: 350px;">
                                        </select>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label for="exampleInputEmail1">
                                            Phone N0(s)
                                        </label>
                                    </td>
                                    <td>
                                        <input id="txtphonenumbers" type="text" class="form-control" id="dd" style="width: 350px;">
                                    </td>
                                    <td>
                                        <label for="exampleInputEmail1">
                                            Our Contact Person</label>
                                    </td>
                                    <td>
                                        <input id="txtocp" type="text" class="form-control" id="dd" style="width: 350px;">
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label for="exampleInputEmail1">
                                            Month</label>
                                    </td>
                                    <td>
                                        <select id="ddlMonth" runat="server" class="form-control" onchange="GetEmpAttendanceData();"
                                            style="width: 350px;">
                                        </select>
                                    </td>
                                    <td>
                                        <label for="exampleInputEmail1">
                                            OT in terms of</label>
                                    </td>
                                    <td>
                                        <select id="ddlOTtype" class="form-control" style="width: 350px;">
                                            <option value="0">Days</option>
                                            <option value="1">Hours</option>
                                        </select>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <div id="divSummary" class="row" style="display: none;">
                        <table id="tblSummary" class="table table-striped table-bordered table-condensed table-hover">
                            <thead>
                                <tr class="warning">
                                    <th>Designation
                                    </th>
                                    <th>Number of Duties
                                    </th>
                                    <th>Ot's
                                    </th>
                                    <th>WO's
                                    </th>
                                    <th>NHS's
                                    </th>
                                    <th>Npots's
                                    </th>
                                    <th>LDays
                                    </th>
                                    <th>Totals
                                    </th>
                                    <th>Canteen Advance
                                    </th>
                                    <th>Penality
                                    </th>
                                    <th>Incentives
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                            </tbody>
                            <tfoot class="active">
                                <tr>
                                    <td>
                                        <label id="lblHead">
                                            Total :</label>
                                    </td>
                                    <td>
                                        <label id="lblTNOD">
                                        </label>
                                    </td>
                                    <td>
                                        <label id="lblTOT">
                                        </label>
                                    </td>
                                    <td>
                                        <label id="lblTWO">
                                        </label>
                                    </td>
                                    <td>
                                        <label id="lblTNHS">
                                        </label>
                                    </td>
                                    <td>
                                        <label id="lblTNPOTS">
                                        </label>
                                    </td>
                                    <td>
                                        <label id="lblTLDAYS">
                                        </label>
                                    </td>
                                    <td>
                                        <label id="lblTotals">
                                        </label>
                                    </td>
                                    <td>
                                        <label id="lblTCADV">
                                        </label>
                                    </td>
                                    <td>
                                        <label id="lblTPEN">
                                        </label>
                                    </td>
                                    <td>
                                        <label id="lblTInctvs">
                                        </label>
                                    </td>
                                </tr>
                            </tfoot>
                        </table>
                    </div>
                    <div id="divAttendanceGrid" class="row">
                        <div>
                            <table id="tblattendancegrid" class="table table-striped table-bordered table-condensed table-hover">
                                <thead>
                                    <tr id="trAddData" data-emp-id='' data-emp-desg='' data-emp-name="" class="active">
                                        <td>
                                            <input id="txtEmpId" class="form-control" placeholder="Employee Id" />
                                        </td>
                                        <td>
                                            <input id="txtEmpName" class="form-control" placeholder="Employee Name" />
                                        </td>
                                        <td>
                                            <select id="ddlEmpDesg" runat="server" class="form-control emp-ddl" style="width: 150px;">
                                            </select>
                                        </td>
                                        <td>
                                            <input type="text" class="form-control num-txt line-cal" id="txt-add-nod" value="0" />
                                        </td>
                                        <td>
                                            <input type="text" class="form-control num-txt line-cal" id="txt-add-ot" value="0" />
                                        </td>
                                        <td>
                                            <input type="text" class="form-control num-txt line-cal" id="txt-add-wo" value="0" />
                                        </td>
                                        <td>
                                            <input type="text" class="form-control num-txt line-cal" id="txt-add-nhs" value="0" />
                                        </td>
                                        <td>
                                            <input type="text" class="form-control num-txt line-cal" id="txt-add-npots" value="0" />
                                        </td>
                                        <td>
                                            <input type="text" class="form-control num-txt line-cal" id="txt-add-ldays" value="0" />
                                        </td>
                                        <td>
                                            <input type="text" class="form-control num-txt" id="txt-add-canadv" value="0" />
                                        </td>
                                        <td>
                                            <input type="text" class="form-control num-txt" id="txt-add-pen" value="0" />
                                        </td>
                                        <td>
                                            <input type="text" class="form-control num-txt" id="txt-add-inctvs" value="0" />
                                        </td>
                                        <td rowspan="2"></td>
                                        <td rowspan="2">
                                            <button class="btn btn-primary" onclick="AddNewEmp(this);return false;" style="height: 60px;">
                                                <i class="glyphicon glyphicon-plus"></i>
                                            </button>
                                        </td>
                                    </tr>
                                    <tr class="active">
                                        <td>
                                            <input type="text" class="form-control txt-calender" id="txtJoingingDate" placeholder="JoiningDate" />
                                        </td>
                                        <td>
                                            <input type="text" class="form-control txt-calender" id="txtRelievingDate" placeholder="RevlievingDate" />
                                        </td>
                                        <td>
                                            <select id="ddlTransfertype" class="form-control" style="width: 150px;">
                                                <option value="1">PostingOrder</option>
                                                <option value="0" selected="selected">Temporary Transfer</option>
                                                <option value="-1">Dumy Transfer</option>
                                            </select>
                                        </td>
                                        <td>
                                            <input type="checkbox" id="chkESI" checked="checked" />
                                            &nbsp; ESI
                                        </td>
                                        <td>
                                            <input type="checkbox" id="chkPF" checked="checked" />
                                            &nbsp; PF
                                        </td>
                                        <td>
                                            <input type="checkbox" id="chkPT" checked="checked" />
                                            &nbsp; PT
                                        </td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                </thead>
                                <thead>
                                    <tr class="warning">
                                        <th>Emp Id
                                        </th>
                                        <th>Emp Name
                                        </th>
                                        <th>Emp Designation
                                        </th>
                                        <th>NoOfDuties
                                        </th>
                                        <th>OT's
                                        </th>
                                        <th>WO's
                                        </th>
                                        <th>NHS
                                        </th>
                                        <th>Npots
                                        </th>
                                        <th>LDays
                                        </th>
                                        <th>CanteenAdv
                                        </th>
                                        <th>Penalty
                                        </th>
                                        <th>Incentives
                                        </th>
                                        <th>Totals
                                        </th>
                                        <th></th>
                                    </tr>
                                </thead>
                                <tbody>
                                </tbody>
                                <tfoot class="active">
                                    <tr>
                                        <th></th>
                                        <th></th>
                                        <th>
                                            <label>
                                                Totals :</label>
                                        </th>
                                        <th>
                                            <label id="lblNOD">
                                            </label>
                                        </th>
                                        <th>
                                            <label id="lblOT">
                                            </label>
                                        </th>
                                        <th>
                                            <label id="lblWO">
                                            </label>
                                        </th>
                                        <th>
                                            <label id="lblNHS">
                                            </label>
                                        </th>
                                        <th>
                                            <label id="lblNpots">
                                            </label>
                                        </th>
                                         <th>
                                            <label id="lblLDays">
                                            </label>
                                        </th>
                                        <th>
                                            <label id="lblCanAdv">
                                            </label>
                                        </th>
                                        <th>
                                            <label id="lblPen">
                                            </label>
                                        </th>
                                        <th>
                                            <label id="lblInctvs">
                                            </label>
                                        </th>
                                        <th>
                                            <label id="lblTTotals">
                                            </label>
                                        </th>
                                        
                                    </tr>
                                </tfoot>
                            </table>
                        </div>
                        <div class="row">
                            <div class="col-md-10">
                            </div>
                            <div class="col-md-1">
                                <button type='button' runat="server" id="btnSave" class="btn btn-success" onclick="SaveAttendance();return false;">
                                    Save</button>

                                 
                            </div>
                            <div class="col-md-1">
                                <button type='button' id="btnCancel" class="btn btn-default" onclick="Cancel();return false;">
                                    Cancel</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div id="footerouter">
                <div class="footer">
                    <div class="footerlogo">
                        <a href="http://www.diyostech.in" target="_blank">Powered by DIYOS </a>
                    </div>
                    <div class="footercontent">
                        <a href="#">Terms &amp; Conditions</a> | <a href="#">Privacy Policy</a> | ©
                    <asp:Label ID="lblcname" runat="server"></asp:Label>.
                    </div>
                    <div class="clear">
                    </div>
                </div>
            </div>
        </div>
        <div id="fade">
        </div>
        <div id="modal">
            <img id="loader" src="css/ajax-loader.gif" />
        </div>
    </form>
</body>
</html>
