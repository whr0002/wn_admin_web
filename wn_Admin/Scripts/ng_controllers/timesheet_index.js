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