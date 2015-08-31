
var token;
function setDatetiempicker($, formBlock) {
    $('.b-datepicker').datetimepicker();
    //var startDate = $('#Date', formBlock).data('DateTimePicker');
    //startDate.clear();
    //startDate.setStartDate(new Date());
    //setDateLimits(startDate, formBlock);
    $('#Date', formBlock).on("dp.change", function (e) {

        setDateLimits(e, formBlock);


        
    });
}

function setDateLimits(e, formBlock) {
    var endDate = new Date(e.date);
    var endDatepicker = $('#EndDate', formBlock).data("DateTimePicker");

    endDate.setHours(23, 59, 59, 999);
    //endDatepicker.clear();
    if (e.date > endDatepicker.maxDate()) {
        endDatepicker.maxDate(endDate);
        endDatepicker.minDate(e.date);
    } else {
        endDatepicker.minDate(e.date);
        endDatepicker.maxDate(endDate);
    }
    refreshPayPeriod(formBlock);
}

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

    $('#close', formBlock).click({param: formBlock}, closeForm);
   

    token = $('input[name="__RequestVerificationToken"]', formBlock).val();
    // Add change event to date
    //$('#Date', formBlock).change(refreshPayPeriod(formBlock));


    toastr.options = {
        "closeButton": true,
        "debug": false,
        "newestOnTop": false,
        "progressBar": false,
        "positionClass": "toast-bottom-right",
        "preventDuplicates": true,
        "onclick": null,
        "showDuration": "300",
        "hideDuration": "1000",
        "timeOut": "5000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    }
}

function refreshPayPeriod(formBlock) {
    var ppyr = $('#PPYr', formBlock).val("");
    var pp = $('#PP', formBlock).val("");

    var date = $('#Date', formBlock).val();
    //console.log(date);

    $.ajax({
        url: "/api/payperiods",
        type: "POST",
        data: { date: date },
        success: function (result) {
            ppyr.val(result["PPYear"]);
            pp.val(result["PPNumber"]);
        }
    });
}



// Initializing calendar
function initializeCalendar(events) {

    $('#mCldr').fullCalendar({
        header: {
            left: 'prev,next today',
            center: 'title',
            right: 'month,agendaWeek,agendaDay'
        },
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

    var formBlockLast = $('div[id^="formBlock"]:last');
    var num = parseInt(formBlockLast.prop("id").match(/\d+/g), 10) + 1;
    //var token = $('input[name="__RequestVerificationToken"]', formBlockLast).val();

    var formBlock = formBlockLast.clone();
    formBlock.prop('id', "formBlock" + num);
    
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


    $('#close', formBlock).click({param: formBlock},closeForm);
    $('#close', formBlock).show();


    formRow.append(formBlock);

    formBlock = jq14('#formBlock' + num);
    setDatetiempicker(jq14, formBlock);

    clearForm(formBlock);
}

function closeForm(event) {
    var result = confirm("Do you want to delete this form?");
    if(result){
        event.data.param.remove();
    }
}

function clearForm(formBlock) {
    $(':input', formBlock)
        .not('#EmployeeID, #Date, #EndDate, #PPYr, #PP')
        .val('')
        .removeAttr('selected');
    $('#PID', formBlock).empty();
}

// Form Submission
function ajaxSubmit() {
    //var form = $('#__AjaxAntiForgeryForm');
    //var token = $('input[name="__RequestVerificationToken"]', form).val();

    var forms = $('div[id^="formBlock"]');
    forms.each(function (index) {
        var formBlock = forms[index];
        var formData =
            {
                __RequestVerificationToken: token,
                EmployeeID: $('#EmployeeID option:selected', formBlock).val(),
                Date: $('#Date', formBlock).val(),
                EndDate: $('#EndDate', formBlock).val(),
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
            url: "/workings/ajaxCreate",
            type: 'POST',
            data: formData,
            success: function (result) {
                if (result === "valid") {
                    toastr["success"]("Your time sheet has been submitted.", "Success");
                    $(formBlock).empty();
                } else {
                    toastr["error"]("Fail to submit your time sheet", "Fail");
                    $("#validationErrors", formBlock).empty();
                    $("#validationErrors", formBlock).append(result);
                }
            },
            async: false
        });

    });

}

