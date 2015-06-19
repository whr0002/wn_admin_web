var projects = [];
$(document).on('change', "#Client", function () {
    
    $('#Project').empty();
    projects = [];

    var client = $('#Client').find(":selected").text();
    var projects = getProjectByClient(client);


});

$(document).on('change', "#Project", function () {
    $('#ProjectID').val("");

    var selectedPj = $('#Project').find(":selected").text();

    for (var i = 0; i < projects.length; i++) {
        
        var pname = projects[i]["ProjectName"];

        if (selectedPj === pname) {
            $('#ProjectID').val(projects[i]["ProjectID"]);
        }
        
    }

});

function getProjectByClient(client) {
    $('#ProjectID').val("");
    $.getJSON("/api/projects/" + client, function (data) {
        
        projects = data;

        var mySelect = $('#Project');
        mySelect.append('<option value="">Select ...</option>');
        for (var i = 0; i < data.length;i++) {
            console.log(data[i]);
            var pname = data[i]["ProjectName"];
            mySelect.append('<option value="' + pname + '">' +pname+'</option>');
        }

  
    });
}