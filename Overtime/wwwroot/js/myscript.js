$(document).ready(function () {
    $('#forWhom').change(function() {
        if (!$(this).is(":checked")) {
            var returnVal = confirm("Are you sure?");
            $(this).attr("uncheck", returnVal);
        }
        else {
            alert();
        }
              
    });
    $('[data-toggle="tooltip"]').tooltip(); 
    if ($("#demo").length) {
        setInterval(myTimer, 1000);
    }
    if ($("#liveMonitoring").length) {
       setInterval(loadTimeForAllTr, 1000);
        //loadTimeForAllTr();
    }
    if ($("#mytable").length) {
        $('#mytable').DataTable({
            dom: 'Bfrtip',
            buttons: [
                'copyHtml5',
                'excelHtml5',
                'csvHtml5',
                'pdfHtml5'
            ]
        } );
    }
    if ($("#table2").length) {
        $('#table2').DataTable({
            dom: 'Bfrtip',
            buttons: [
                'copyHtml5',
                'excelHtml5',
                'csvHtml5',
                'pdfHtml5'
            ]
        });
    }
    
    $("#name").autocomplete({

        source: function (req, resp) {
            var data = new FormData();
            data.append('name', $("#name").val());
            $.ajax({
                url: "/OvertimeRequest/UsersName",
                type: "POST",
                contentType: false,
                processData: false,
                cache: false,
                data: data,
                success: function (data) {
                    resp(data);
                },
                error: function () {
                }
            });
        },
        select: function (event, ui) {

            var TABKEY = 9;
            this.value = ui.item.value;

            if (event.keyCode == TABKEY) {
                event.preventDefault();
                $('#name').focus();
            }

            return false;
        },
        position: {
            offset: '1000 4' // Shift 20px to the left, 4px down.
        }
    });
   
    $('input[type=radio][name=frmTypes]').change(function () {

        load_user_menus();
    });
    $('#role').change(function () {
        load_user_menus()
    });
    $('#saveBtn').click(function () {
        var reqRow = [];
        $("#multiselect_to option").each(function () {
            reqRow.push(parseInt($(this).val()));
        });
        console.log(reqRow);
        var $option = $('#username').find('option:selected');
        var value = $option.val();
        var type = $('input[name=frmTypes]:checked').val();
        var data = new FormData();

        data.append('userId', value);
        data.append('type', type);
        data.append('docsList', reqRow);
        $.ajax({
            url: "/grand/adm/updateUserMenu",
            type: "POST",
            contentType: false,
            processData: false,
            cache: false,
            data: data,
            success: function (response) {
                if (response.status == "Y") {
                    showSnackBar("snackbar-green", response.message);
                }
                else {
                    showSnackBar("snackbar-red", response.message);
                }
                $("#multiselect_to").empty();
                $("#multiselect").empty();
            },
            error: function () {
                $("#multiselect_to").empty();
                $("#multiselect").empty();
                showSnackBar("snackbar-red", "Contact Developer");
            }
        });
    });
});


function openWorkflowDetl(id) {
        var data = new FormData();
        data.append('ID', id);
        $.ajax({
            url: "/Workflow/Details",
            type: "POST",
            contentType: false,
            processData: false,
            cache: false,
            data: data,
            success: function (response) {
                $("#modalContainer").html(response);
                $("#wd_workflow_id").val(id);
                $('#myModal').modal('show');

            },
            error: function () {
            }
        });
}
function saveWorkFlowDetails() {
    var workflowid = $("#wd_workflow_id").val();
    var role_id = $("#wd_role_id").val();
    var priority = $("#wd_priority").val();

    var data = new FormData();
    data.append('wd_workflow_id', workflowid);
    data.append('wd_role_id', role_id);
    data.append('wd_priority', priority);
    $.ajax({
        url: "/WorkflowDetail/Create",
        type: "POST",
        contentType: false,
        processData: false,
        cache: false,
        data: data,
        success: function (response) {
            $("#modalContainer").html(response);
            $('#myModal').modal('show');
            $("#wd_priority").val("");

        },
        error: function () {
        }
    });
}
function overTimeRequestReport() {
    var data = new FormData();
    alert($("#reportrange").val());
    data.append("no_of_hours", $("#rq_no_of_hours").val());
    data.append("rq_dep_id", $("#rq_dep_id").val());
    data.append("reportrange", $("#reportrange").val());
    data.append("rq_remarks", $("#rq_remarks").val());
    data.append("rq_role_id", $("#role_id").val());
    data.append("rq_cre_by", $("#rq_cre_by").val());
    data.append("rq_cre_date", $("#rq_cre_date").val());
    $.ajax({
        url: "/OvertimeRequest/CustomReport",
        type: "POST",
        contentType: false,
        processData: false,
        cache: false,
        data: data,
        success: function (response) {
            $("#container").html(response);
            $('#mytable').DataTable({
                dom: 'Bfrtip',
                buttons: [
                    'copyHtml5',
                    'excelHtml5',
                    'csvHtml5',
                    'pdfHtml5'
                ]
            });
        },
        error: function () {
        }
    });
}
function overTimeRequestReport() {
    var data = new FormData();
    data.append("no_of_hours", $("#rq_no_of_hours").val());
    data.append("rq_dep_id", $("#rq_dep_id").val());
    data.append("reportrange", $("#reportrange").val());
    data.append("rq_cre_by", $("#rq_cre_by").val());
    data.append("rq_cre_date", $("#rq_cre_date").val());
    $.ajax({
        url: "/OvertimeRequest/CustomReport",
        type: "POST",
        contentType: false,
        processData: false,
        cache: false,
        data: data,
        success: function (response) {
            $("#container").html(response);
            $('#mytable').DataTable({
                dom: 'Bfrtip',
                buttons: [
                    'copyHtml5',
                    'excelHtml5',
                    'csvHtml5',
                    'pdfHtml5'
                ]
            });
        },
        error: function () {
        }
    });
}
function workflowHistory(rowid,doc_id,workflow,status) {
    var data = new FormData();
    data.append("rowid", rowid);
    data.append("doc_id", doc_id);
    data.append("workflow", workflow);
    
    $.ajax({
        url: "/WorkflowTracker/History",
        type: "POST",
        contentType: false,
        processData: false,
        cache: false,
        data: data,
        success: function (response) {
            workflowDetailStatus(workflow, status);
            $("#modalContainer").html(response);
            $('#historyModal').modal('show');

        },
        error: function () {
        }
    });
}

function workflowDetailStatus( workflow,status) {
    var data = new FormData();
    data.append("workflow", workflow);
    data.append("status", status); 
    $.ajax({
        url: "/WorkflowDetail/Status",
        type: "POST",
        contentType: false,
        processData: false,
        cache: false,
        data: data,
        success: function (response) {
            $("#cont").html(response);
          
        },
        error: function () {
        }
    });
}
function OverTimeConsolidatedReport() {
    var data = new FormData();
    data.append("rq_dep_id", $("#rq_dep_id").val());
    data.append("rq_cre_for", $("#rq_cre_for").val());
    data.append("reportrange", $("#reportrange").val());
    $.ajax({
        url: "/OvertimeRequest/ConsolidatedReports",
        type: "POST",
        contentType: false,
        processData: false,
        cache: false,
        data: data,
        success: function (response) {
            $("#container").html(response);
            $('#mytable').DataTable({
                dom: 'Bfrtip',
                buttons: [
                    'copyHtml5',
                    'excelHtml5',
                    'csvHtml5',
                    'pdfHtml5'
                ]
            });
        },
        error: function () {
        }
    });
}
$(function () {

    var start = moment().subtract(29, 'days');
    var end = moment();

    function cb(start, end) {
        $('#reportrange span').html(start.format('MMMM D, YYYY') + ' - ' + end.format('MMMM D, YYYY'));
    }

    $('#reportrange').daterangepicker({
        startDate: start,
        endDate: end,
        ranges: {
            'Today': [moment(), moment()],
            'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
            'Last 7 Days': [moment().subtract(6, 'days'), moment()],
            'Last 30 Days': [moment().subtract(29, 'days'), moment()],
            'This Month': [moment().startOf('month'), moment().endOf('month')],
            'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')]
        }
    }, cb);

    cb(start, end);

});



function myTimer() {
 var date1 = new Date();
  //  var date2 = Date.parse("2020/05/29 21:59:00");
    var date2 = Date.parse($("#lbl_start").text());
var delta = Math.abs(date1 - date2) / 1000;

// calculate (and subtract) whole days
var days = Math.floor(delta / 86400);
delta -= days * 86400;

// calculate (and subtract) whole hours
var hours = Math.floor(delta / 3600) % 24;
delta -= hours * 3600;

// calculate (and subtract) whole minutes
var minutes = Math.floor(delta / 60) % 60;
delta -= minutes * 60;

// what's left is seconds
var seconds = delta % 60;
    document.getElementById("demo").innerHTML = days + ": " + hours + ":" + minutes + ":" + Math.round(seconds);
}


function loadTimeForAllTr() {
    if ($("#mytable").length) {
        
        $("tr.a").each(function (i, tr) {
            var value = $(this).find("input.b").val();
           
            var date1 = new Date();
            //  var date2 = Date.parse("2020/05/29 21:59:00");
            var date2 = Date.parse($(this).find("#starttime" + value).val() );
            var delta = Math.abs(date1 - date2) / 1000;

            // calculate (and subtract) whole days
            var days = Math.floor(delta / 86400);
            delta -= days * 86400;

            // calculate (and subtract) whole hours
            var hours = Math.floor(delta / 3600) % 24;
            delta -= hours * 3600;

            // calculate (and subtract) whole minutes
            var minutes = Math.floor(delta / 60) % 60;
            delta -= minutes * 60;

            // what's left is seconds
            var seconds = delta % 60;
            $(this).find("#Duration" + value).text(n(days) + ": " + n(hours) + ":" + n(minutes) + ":" + n(Math.round(seconds)));
        });
    }

}

function n(n) {
    return n > 9 ? "" + n : "0" + n;
}

function holdHistory(rowid, doc_id,from) {
    var data = new FormData();
    data.append("rowid", rowid);
    data.append("doc_id", doc_id);
    data.append("from", from);

    $.ajax({
        url: "/Hold/History",
        type: "POST",
        contentType: false,
        processData: false,
        cache: false,
        data: data,
        success: function (response) {
            $("#modalContainer").html(response);
            $('#historyModal').modal('show');
        },
        error: function () {
        }
    });
}
function replayForHold(id) {
    var data = new FormData();
    data.append("id", id);
    data.append("replay", $("#replay" + id).val().replace(/[\n\r]/g, ''));
    $.ajax({
        url: "/Hold/Replay",
        type: "POST",
        contentType: false,
        processData: false,
        cache: false,
        data: data,
        success: function (response) {
        },
        error: function () {
        }
    });
}
function hold(id) {
    var data = prompt("Reason For Blocking?? ", "");
    if (data != null) {
        $("#reason" + id).val(data);
        $("#hold" + id).submit();
    }
   
}
function unhold(id) {
    var data = prompt("Reason For Blocking?? ", "");
    if (data != null) {
        $("#reason" + id).val(data);
        $("#unhold" + id).submit();
    }
   
}
