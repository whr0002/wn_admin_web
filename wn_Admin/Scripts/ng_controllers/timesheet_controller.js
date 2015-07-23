
window.onload = function () {
    
}


function initializeCalendar(events) {
    var calendar = $('#mCldr');
    calendar.fullCalendar({
        defaultDate: new Date(),
        editable: false,
        eventLimit: true, // allow "more" link when too many events
        //events: [
        //    {
        //        title: 'All Day Event',
        //        start: '2015-02-01'
        //    }
        //]
        events: events
    });



}

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

    if (pid != null && pid.length > 0)
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

$(document).on('change', '#EndDate', function () {
    var dateTo = new Date($('#EndDate').val());
    dateTo.setDate(dateTo.getDate() + 2);
    $('#returnDate').empty();
    $('#returnDate').append("Return to work on: " + (dateTo.getMonth()+1) + "/" + dateTo.getDate() + "/" + dateTo.getYear());
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





function addForm() {
    var formRow = $('#formRow');

    var formBlock = $('div[id^="formBlock"]:last');
    var num = parseInt(formBlock.prop("id").match(/\d+/g), 10) + 1;



    var cloneForm = formBlock.clone();
    cloneForm.prop('id', "formBlock" + num);

    formRow.append(cloneForm);


}

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

