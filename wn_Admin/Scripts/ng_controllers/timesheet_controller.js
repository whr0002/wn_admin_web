var projects = [];
$(document).on('change', "#ClientName", function () {
    
    $('#ProjectID').empty();
    projects = [];

    var client = $('#ClientName').find(":selected").text();
    var projects = getProjectByClient(client);


});

function getProjectByClient(client) {
    $('#ProjectID').val("");
    var mySelect = $('#ProjectID');
    mySelect.append('<option value="">Choose ...</option>');

    $.getJSON("/api/projects/" + client, function (data) {

        projects = data;

        
        for (var i = 0; i < data.length; i++) {
            //console.log(data[i]);
            var pID = data[i]["ProjectID"];
            var pname = data[i]["ProjectName"];
            mySelect.append('<option value="' + pID + '">' + pname + '</option>');
        }


    });
}

$(document).on('change', '#Date', function () {
    $('#PPYr').val("");
    $('#PP').val("");

    var date = $('#Date').val();

    $.getJSON("/api/PayPeriods/" + date, function (result) {
        $('#PPYr').val(result["PPYear"]);
        $('#PP').val(result["PPNumber"]);
    });
    
});


