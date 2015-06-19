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
        dataService.getItems('/api/lookups')
            .success(function (data) { angular.copy(data, $scope.lookups); })
            .error(function () { toastr.error("Error loading lookups"); });

        var p = { skip: 0, take: 0 };
        dataService.getItems('/api/friendlinks', p)
            .success(function (data) {
                angular.copy(data, $scope.items);
                gridInit($scope, $filter);
                rowSpinOff($scope.items);
            })
            .error(function () {
                toastr.error($rootScope.lbl.errorLoadingCategories);
            });
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