

function setDatetiempicker($, formBlock) {
    $('.b-datepicker').datetimepicker({
        format: 'MM/DD/YYYY'

    });

    $('#Date', formBlock).on("dp.change", function (e) {
        setDateLimits(e, formBlock);
    });

    $('#EndDate', formBlock).on('dp.change', function (e) {
        var dateTo = new Date(e.date);
        dateTo.setDate(dateTo.getDate() + 1);
        $('#returnDate').empty();
        $('#returnDate').append("Return to work on: " + (dateTo.getMonth() + 1) + "/" + dateTo.getDate() + "/" + dateTo.getFullYear());
    });
}

function setDateLimits(e, formBlock) {
    var endDate = new Date(e.date);
    var endDatepicker = $('#EndDate', formBlock).data("DateTimePicker");

    endDate.setHours(23, 59, 59, 999);
    //endDatepicker.clear();
    if (e.date > endDatepicker.maxDate()) {
        //endDatepicker.maxDate(endDate);
        endDatepicker.minDate(e.date);
    } else {
        endDatepicker.minDate(e.date);
        endDatepicker.maxDate(endDate);
    }
    refreshPayPeriod(formBlock);
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