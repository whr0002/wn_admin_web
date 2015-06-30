var projects = [];
$(document).on('change', "#ClientName", function () {
    
    $('#ProjectID').empty();
    projects = [];

    var client = $('#ClientName').find(":selected").text();
    var projects = getProjectByClient(client);


});

$(document).on('change', "#ProjectID", function () {
    var pidSpan = $('#PID');
    pidSpan.empty();

    var pid = $('#ProjectID option:selected').val();

    if(pid != null && pid.length > 0)
        pidSpan.text("Project ID: " + pid);
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

function toggleSearchPanel(checkbox) {
    if (checkbox.checked) {
        $('#SearchPanel').slideDown();
    } else {
        $('#SearchPanel').slideUp();
    }
}


function checkAll(checkbox) {


    var subBoxes = $('.reviewBox');

    if (checkbox.checked) {

        subBoxes.prop('checked', true);
        //subBoxes.each(function (index) {
        //    if(subBoxes[index].checked){
        //        var id = $(this).val();
        //        console.log(id);
        //    }  
        //});
    } else {
        subBoxes.prop('checked', false);
    }

}

function submitReviews() {
    var subBoxes = $('.reviewBox');
    var selection = [];
    subBoxes.each(function (index) {
        if (subBoxes[index].checked) {
            var id = $(this).val();
            selection.push(id);
        }
    });

    console.log(selection);

    if (selection.length > 0) {
        // submit 
        var json = JSON.stringify(selection);
        var ids = "";
        for(var i=0;i<selection.length;i++){
            if (i == 0) {
                ids += selection[i];
            } else {
                ids += "," + selection[i];
            }
        }
        //console.log(ids);
        window.location.href = "/workings/review?ids=" + encodeURIComponent(ids);
    }

}

function submitApproves() {
    var selection = [];
    var subBoxes = $('.reviewBox');
    subBoxes.each(function (index) {
        if (subBoxes[index].checked) {
            var id = $(this).val();
            selection.push(id);
        }
    });

    //console.log(selection.join(","));
    window.location.href = "/expenses/review?ids=" + encodeURIComponent(selection.join(","));
}
