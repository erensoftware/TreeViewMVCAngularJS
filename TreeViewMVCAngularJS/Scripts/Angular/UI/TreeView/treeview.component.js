/// <reference path="https://ajax.googleapis.com/ajax/libs/angularjs/1.7.8/angular.js" />

function expandTreeNode(nodeid) {
    $('a[nodeid="' + nodeid + '"]').removeClass("fa-angle-right");
    $('a[nodeid="' + nodeid + '"]').addClass("fa-angle-down");
    $('treeview-node[nodeid="' + nodeid + '"]').removeClass("tree-collapsed");
    $('treeview-node[nodeid="' + nodeid + '"]').addClass("tree-expanded");
}
function collapseTreeNode(nodeid) {
    $('a[nodeid="' + nodeid + '"]').removeClass("fa-angle-down");
    $('a[nodeid="' + nodeid + '"]').addClass("fa-angle-right");
    $('treeview-node[nodeid="' + nodeid + '"]').removeClass("tree-expanded");
    $('treeview-node[nodeid="' + nodeid + '"]').addClass("tree-collapsed");
}
function unselectNode() {
    $(".tree-node-selected").each(function (idx) {
        $(this).removeClass("tree-node-selected");
    });
}
function treeViewController($timeout) {
    var ctrl = this;
    ctrl.click = function (item) {
        //alert(item.currentTarget.getAttribute("anchorhref"));
        ctrl.url = item.currentTarget.getAttribute("anchorhref");
        unselectNode();
        $(item.currentTarget).parent().addClass("tree-node-selected");
        //ctrl.onClick(ctrl.url);
    };
    ctrl.collapsed = false;
    ctrl.nodeSelected = false;
    ctrl.nodeId = $(".tree-container").children(":first-child").attr("selected-node-id");
    ctrl.selectNode = function (node_id) {
        //ctrl.nodeSelected = false;
        if (node_id != '' && !isNaN(node_id)) {
            //alert('a[nodeid="' + node_id + '"]');
            var parent = $('a[nodeid="' + node_id + '"]').parent();
            //alert($(parent).html());
            $(parent).removeClass("tree-node-selected");
            $(parent).addClass("tree-node-selected");
        }
    };
    ctrl.findAndOpenSelected = function () {
        var node = $(".tree-node-selected");
        if ($(node).html()) {
            while (!$(node).hasClass("tree-container")) {
                //alert(node.html());
                node = $(node).parent();
                if ($(node).hasClass("tree-node")) {
                    var node_id = $(node).children(':first-child').children(':first-child').attr("nodeid");
                    if (node_id) {
                        expandTreeNode(node_id);
                    }
                }
            }
        }
    };
    ctrl.collapseClick = function (item) {
        ctrl.collapsed = !ctrl.collapsed;
        ctrl.nodeId = item.currentTarget.getAttribute("nodeid");
        if ($('a[nodeid="' + ctrl.nodeId + '"]').hasClass("fa-angle-right")) {
            expandTreeNode(ctrl.nodeId);
        }
        else {
            collapseTreeNode(ctrl.nodeId);
        }
    };
    $timeout(function () {
        $timeout(function () {
            ctrl.selectNode(ctrl.nodeId);
            ctrl.findAndOpenSelected();
        }, 0);
    }, 0);
    //this.$postLink = function () {
    //    //alert(ctrl.nodeId);
    //    ctrl.selectNode(ctrl.nodeId);
    //    ctrl.findAndOpenSelected();
    //};
}
