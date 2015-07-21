window.onload = function () {
    var reviewBtn = $('#reviewbtn');

    reviewBtn.click(function () {
        
        var boxes = $('.reviewbox');

        var ids = [];

        boxes.each(function(i){
            if (boxes[i].checked) {
                ids.push($(this).val());
            }
        })
            

        

        console.log(ids.length);

        var idsSplit = ids.join(",");

        window.location.href = "/safetymeetings/review?ids=" + encodeURIComponent(ids);
    });
}