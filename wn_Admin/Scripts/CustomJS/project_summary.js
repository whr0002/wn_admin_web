window.onload = function () {
    // Add change listener on client field
    $("#ClientID").change({ param: $("#ClientID").find(":selected").text() }, getProjectByClient);

    $("#export").click(exportToExcel);
}

// Export data into Excel
function exportToExcel() {
    // Since export function is used in time sheet management already, call corresponding function directly.
    var client = $('#ClientID').find(":selected").val();
    var project = $('#ProjectID').find(":selected").val();
    var startDate = $('#startDate').val();
    var endDate = $('#endDate').val();

    window.location.replace("/workings?clientId=" + client +
        "&projectId=" + project +
        "&startDate=" + startDate +
        "&endDate=" + endDate +
        "&exportToExcel=true");

}

// Get a list of projects by a given client
function getProjectByClient() {

    var client = $("#ClientID").find(":selected").text();
    var mySelect = $('#ProjectID');
    mySelect.empty();
    mySelect.val("");
    mySelect.append('<option value="">Choose ...</option>');

    $.getJSON("/api/projects/" + client, function (data) {

        for (var i = 0; i < data.length; i++) {
            //console.log(data[i]);
            var pID = data[i]["ProjectID"];
            var pname = data[i]["ProjectName"];
            mySelect.append('<option value="' + pID + '">' + pname + '</option>');
        }


    });
}