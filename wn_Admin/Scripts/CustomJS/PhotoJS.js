window.onload = function () {
    $('#photoWrapper').hide();
    $("#PhotoTaken").on('click', toggleAddPhotoBtn);
    $("#photoAdd").on('click', addPhotoItem);

}

function addPhotoItem() {
    var photoInputs = $('.accident_photos');
    var total = photoInputs.length
    var section = $('#photos');
    var content = "<div class=\"col-md-3\">"
        + "<input type=\"file\" class=\"accident_photos\" name=\"aphotos\" id=\"aPhoto_" + total + "\"/></div>";
        //+ "<div class=\"col-md-7\">"
        //+ "<input type=\"text\" id=\"aPhoto_desc_"+total+"\" /> </div>"
        //+ "";

    section.append(content);
}

function toggleAddPhotoBtn() {
    var box = $("#PhotoTaken");
    if (box.is(':checked')) {
        $('#photoWrapper').show();
    } else {
        $('#photoWrapper').hide();
        $('#photos').empty();
    }

}