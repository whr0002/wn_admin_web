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
function getProjectByClient(client) {
    $('#projectId').val("");
    var mySelect = $('#projectId');
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

    //console.log(selection);

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

function setDatetiempicker($, formBlock) {
    $('.b-datepicker').datetimepicker({
        format: 'MM/DD/YYYY'

    });

    $('#startDate', formBlock).on("dp.change", function (e) {
        setDateLimits(e, formBlock);
    });
}

function setDateLimits(e, formBlock) {
    var endDate = new Date(e.date);
    var endDatepicker = $('#endDate', formBlock).data("DateTimePicker");

    endDate.setHours(23, 59, 59, 999);
    //endDatepicker.clear();
    if (e.date > endDatepicker.maxDate()) {
        //endDatepicker.maxDate(endDate);
        endDatepicker.minDate(e.date);
    } else {
        endDatepicker.minDate(e.date);
        endDatepicker.maxDate(endDate);
    }
}