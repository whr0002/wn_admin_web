var jq14 = jQuery.noConflict(true);
window.onload = function () {
    jq14('#tree').jstree({
        'core': {
            'data': {
                'url': function (node) {
                    if (node.id === '#') {
                        return "/apitree/root";
                    } else {
                        return node.data;
                    }
 
                    return "/apitree/root";
                },
                'data': function (node) {
                    return {parent:node.id};
                }
            }
        },
        "plugins": [
        "sort","search"
        ]
    });

    var to = false;
    jq14('#treeSearch').keyup(function () {
        if (to) { clearTimeout(to); }
        to = setTimeout(function () {
            var v = jq14('#treeSearch').val();
            jq14('#tree').jstree(true).search(v);
        }, 250);
    });
};


