angular.module('blogAdmin').controller('FriendLinksController', ["$rootScope", "$scope", "$location", "$http", "$filter", "dataService", function ($rootScope, $scope, $location, $http, $filter, dataService) {
    $scope.items = [];
    $scope.lookups = [];
    $scope.category = {};
    $scope.id = ($location.search()).id;
    $scope.focusInput = false;

    if ($scope.id) {
        alert('ok!!');
    }

    $scope.addNew = function () {
        $scope.clear();
        $("#modal-add-cat").modal();
        $scope.focusInput = true;


    }

    $scope.load = function () {

    }

    $scope.load();

    $scope.save = function () {

        if (!$('#form').valid()) {
            alert('f');
            return false;
        }
        alert('true');
        //================
        dataService.addItem("/api/friendlinks", $scope.link)
           .success(function (data) {
               
           })
           .error(function (data) { toastr.error(data); });

        ///====================

    }

    $scope.processChecked = function (action) {

    }

    $scope.clear = function () {

    }

    $(document).ready(function () {

    });
}]);