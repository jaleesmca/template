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
    data.append("no_of_hours", $("#rq_no_of_hours").val());
    data.append("rq_dep_id", $("#rq_dep_id").val());
    data.append("rq_start_time", $("#rq_start_time").val());
    data.append("rq_remarks", $("#rq_remarks").val());
    data.append("rq_role_id", $("#role_id").val());
    data.append("rq_cre_by", $("#rq_cre_by").val());
    $.ajax({
        url: "/OvertimeRequest/CustomReport",
        type: "POST",
        contentType: false,
        processData: false,
        cache: false,
        data: data,
        success: function (response) {
            $("#container").html(response);
            $('#mytable').DataTable();
        },
        error: function () {
        }
    });
}
