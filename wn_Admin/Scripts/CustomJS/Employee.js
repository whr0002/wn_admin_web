window.onload = function () {
    $("#addEmployee").on('click', addEmployee);
}

function addEmployee(){
    
    var listBlock = $('#attendeeList');
    var dropdown = $('#People');
    var eName = dropdown.find(":selected").text();
    var eid = dropdown.find(":selected").val();
    //console.log(dropdown.find(":selected").val());

    var html = "<input type='hidden' name='attendees' value='" + eid + "' />[" + eName + "]";
    listBlock.append(html);
}