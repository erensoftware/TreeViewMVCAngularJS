/// <reference path="https://ajax.googleapis.com/ajax/libs/angularjs/1.7.8/angular.js" />


function unselectNode() {
    $(".tree-node-selected").each(function (idx) {
        $(this).removeClass("tree-node-selected");
    });
}

var positionApp = angular.module("positionApp", []);
positionApp.component("treeviewNode", {
    templateUrl: "../../../../Scripts/Angular/UI/TreeView/treeView.html",
    controller: treeViewController,
    bindings: {
        treeList: '<',
        selectedFirm: '<',
        editBaseUrl: '<',
        url: '='
        //,
    }
});
var positionController = positionApp.controller("positionController", function ($scope, $http, $templateCache) {
    $scope.vars = {
        selectedFirm: "",
        selectedPosition: "",
        addanchorhref: "",
        editanchorhref: "",
        selectedNodeId: ""
    };
    $scope.data = {
        firms: [],
        positions: []
    }
    $http({
        method: "GET",
        url: "/Position/GetFirm"
    }).then(function success(response) {
        if (response.data != null && response.data.result) {
            $scope.data.firms = response.data.data;
        }
        else {
            if (response.data == null)
                console.log("Error retrieving GetFirm, response.data is null");
            else
                console.log("Error retrieving GetFirm, response.data: " + JSON.stringify(response.data));
        }
    }, function error(response) {
        console.log("Error retrieving GetFirm, get response error. " + response.statusText);
    });

    $scope.onFirmChange = function () {
        $http.get("/Position/GetPositions?firmId=" + $scope.vars.selectedFirm)
            .then(function success(response) {
                if (response.data != null && response.data.result) {
                    $scope.data.positions = response.data.data;
                }
                else {
                    if (response.data == null)
                        console.log("Error retrieving GetPositions, response.data is null");
                    else
                        console.log("Error retrieving GetPositions, response.data: " + JSON.stringify(response.data));
                }
            }, function error(response) {
                console.log("Error retrieving GetPositions, get response error. " + response.statusText);
            });
    };
    $scope.addClick = function (item) {
        var node = $(".tree-node-selected").children(":first-child");
        var urlExt = "";
        if (node) {
            urlExt = "&supPosId=" + $(node).attr("nodeid");
            //$scope.vars.selectedPosition = $(node).attr("nodeid");
        }
        $scope.vars.addanchorhref = item.currentTarget.getAttribute("anchorhref") + urlExt;
        $scope.vars.editanchorhref = "";
    };
    $scope.editClick = function (url) {
        alert(url);
        $scope.vars.editanchorhref = url;
        $scope.vars.addanchorhref = "";
    };
    $scope.deleteClick = function (item) {
        var delUrl = item.currentTarget.getAttribute("anchorhref");
        var parentId = item.currentTarget.getAttribute("parent-id");
        $templateCache.removeAll();
        $http({
            url: delUrl,
            method: 'POST'
        }).then(function success(response) {
            if (response.data != null && response.data.result) {
                $scope.vars.editanchorhref = "";
                $scope.vars.addanchorhref = "";
                unselectNode();
                $scope.vars.selectedNodeId = parentId;
                $scope.onFirmChange();
            }
            else {
                if (response.data == null)
                    console.log("Error delete position post, response.data is null");
                else
                    console.log("Error delete position post, response.data: " + JSON.stringify(response.data));
            }
        }, function error(response) {
            console.log("Error delete position post, response error. " + response.statusText);
        });
    };
    $scope.submitAddForm = function () {
        var data = $('#addPositionForm').serialize();
        $http({
            url: '/Position/Add',
            method: 'POST',
            data: data,
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded'
            }
        }).then(function success(response) {
            if (response.data != null && response.data.result) {
                $scope.vars.addanchorhref = "";
                $scope.vars.editanchorhref = "/Position/Edit?positionId=" + response.data.data + "&firmId=" + $scope.vars.selectedFirm;
                unselectNode();
                $scope.vars.selectedNodeId = response.data.data;
                $scope.onFirmChange();
            }
            else {
                if (response.data == null)
                    console.log("Error add position post, response.data is null");
                else
                    console.log("Error add position post, response.data: " + JSON.stringify(response.data));
            }
        }, function error(response) {
            console.log("Error add position post, response error. " + response.statusText);
        });
    };

    $scope.submitEditForm = function () {
        var data = $('#editPositionForm').serialize();
        $http({
            url: '/Position/Edit',
            method: 'POST',
            data: data,
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded'
            }
        }).then(function success(response) {
            if (response.data != null && response.data.result) {
                $templateCache.removeAll();
                unselectNode();
                $scope.vars.selectedNodeId = response.data.data.PositionId;
                $scope.onFirmChange();
            }
            else {
                if (response.data == null)
                    console.log("Error edit position post, response.data is null");
                else
                    console.log("Error edit position post, response.data: " + JSON.stringify(response.data));
            }
        }, function error(response) {
            console.log("Error edit position post, response error. " + response.statusText);
        });
    };
});