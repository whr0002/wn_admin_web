// Initializing change events on some input fields
window.onload = function () {
    var formBlock = $('#formBlock0');
    // Add change event to clientName
    $('#ClientName', formBlock).change(function () {

        $('#ProjectID', formBlock).empty();

        var client = $('#ClientName').find(":selected").text();

        getProjectByClient(client, formBlock);
    });

    // Add change event to projectName
    $('#ProjectID', formBlock).change(function () {
        var pidSpan = $('#PID', formBlock);
        pidSpan.empty();

        var pid = $('#ProjectID option:selected', formBlock).val();

        if (pid != null && pid.length > 0)
            pidSpan.text("Project ID: " + pid);

    });

    // Add change event to date
    $('#Date', formBlock).change(function () {

        var ppyr = $('#PPYr', formBlock).val("");
        var pp = $('#PP', formBlock).val("");

        var date = $('#Date', formBlock).val();

        $.getJSON("/api/PayPeriods/" + date, function (result) {
            ppyr.val(result["PPYear"]);
            pp.val(result["PPNumber"]);
        });
    });



}

// Initializing calendar
function initializeCalendar(events) {

    $('#mCldr').fullCalendar({
        defaultDate: new Date(),
        editable: false,
        eventLimit: true, // allow "more" link when too many events
        events: events // var 'events' is initialized on 'Index' page.
    });
}

// Get a list of projects by a given client
function getProjectByClient(client , formBlock) {
    $('#ProjectID', formBlock).val("");
    var mySelect = $('#ProjectID', formBlock);
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

// Show the date of returning
$(document).on('change', '#EndDate', function () {
    var dateTo = new Date($('#EndDate').val());
    dateTo.setDate(dateTo.getDate() + 2);
    $('#returnDate').empty();
    $('#returnDate').append("Return to work on: " + (dateTo.getMonth()+1) + "/" + dateTo.getDate() + "/" + dateTo.getFullYear());
});

// Hide/Show Search Area
function toggleSearchPanel(checkbox) {
    if (checkbox.checked) {
        $('#SearchPanel').slideDown();
    } else {
        $('#SearchPanel').slideUp();
    }
}

// Check all review boxes
function checkAll(checkbox) {
    var subBoxes = $('.reviewBox');

    if (checkbox.checked) {

        subBoxes.prop('checked', true);

    } else {
        subBoxes.prop('checked', false);
    }

}

// Review time sheets
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
        for (var i = 0; i < selection.length; i++) {
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




// Clone a form in the same page.
function addForm() {
    var formRow = $('#formRow');

    var formBlock = $('div[id^="formBlock"]:last');
    var num = parseInt(formBlock.prop("id").match(/\d+/g), 10) + 1;



    var formBlock = formBlock.clone();
    formBlock.prop('id', "formBlock" + num);
    //$(cloneForm).off("change", "#ClientName");

    // Add change event to clientName
    $('#ClientName', formBlock).change(function () {

        $('#ProjectID', formBlock).empty();

        var client = $('#ClientName', formBlock).find(":selected").text();

        getProjectByClient(client, formBlock);
    });

    // Add change event to projectName
    $('#ProjectID', formBlock).change(function () {
        var pidSpan = $('#PID', formBlock);
        pidSpan.empty();

        var pid = $('#ProjectID option:selected', formBlock).val();

        if (pid != null && pid.length > 0)
            pidSpan.text("Project ID: " + pid);

    });

    // Add change event to Date to get PPYr and PP
    $('#Date', formBlock).change(function () {

        var ppyr = $('#PPYr', formBlock).val("");
        var pp = $('#PP', formBlock).val("");

        var date = $('#Date', formBlock).val();

        $.getJSON("/api/PayPeriods/" + date, function (result) {
            ppyr.val(result["PPYear"]);
            pp.val(result["PPNumber"]);
        });
    });

    formRow.append(formBlock);


}

// Form Submission
function ajaxSubmit() {
    var form = $('#__AjaxAntiForgeryForm');
    var token = $('input[name="__RequestVerificationToken"]', form).val();

    var forms = $('div[id^="formBlock"]');
    forms.each(function (index) {
        var formBlock = forms[index];
        var formData =
            {
                __RequestVerificationToken: token,
                EmployeeID: $('#EmployeeID option:selected', formBlock).val(),
                Date: $('#Date', formBlock).val(),
                PPYr: $('#PPYr', formBlock).val(),
                PP: $('#PP', formBlock).val(),
                ProjectID: $('#ProjectID option:selected', formBlock).val(),
                Task: $('#Task option:selected', formBlock).val(),
                identifier: $('#Identifier', formBlock).val(),
                Veh: $('#Veh option:selected', formBlock).val(),
                Crew: $('#Crew', formBlock).val(),
                StartKm: $('#StartKm', formBlock).val(),
                EndKm: $('#EndKm', formBlock).val(),
                Equipment: $('#Equipment', formBlock).val(),
                Field: $('#Field option:selected', formBlock).val(),
                PD: $('#PD', formBlock).prop('checked'),
                JobDescription: $('#JobDescription', formBlock).val(),
                OffReason: $('#OffReason option:selected', formBlock).val(),
                Hours: $('#Hours', formBlock).val()
                


            };

        $.ajax({
            //url: $(this).data('url'),
            url: "https://localhost:44300/workings/ajaxCreate",
            type: 'POST',
            data: formData,
            success: function (result) {
                if (result === "valid") {
                    $(formBlock).empty();
                } else {
                    $("#validationErrors", formBlock).empty();
                    $("#validationErrors", formBlock).append(result);
                }
            }
        });

    });

}

