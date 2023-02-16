//(function (app) {
//    'use strict';

SMSHO.controller('worksheetBaseCtrl', ['$scope', 'apiService', '$cookies', function ($scope, apiService, $cookies) {
    'use strict';
    $scope.pageSize = "10";
    $scope.pageNumber = 1;
    $scope.searchedText = "";
    $scope.GetWorksheets = function () {
        var responsedata = apiService.masterget('/api/v1/Worksheet/Get?searchString=' + $scope.searchedText + '&pageNumber=' + $scope.pageNumber + '&pageSize=' + $scope.pageSize);
        responsedata.then(function mySucces(response) {
            $scope.worksheetList = response.data.Items;
            $scope.Count = response.data.Count;
        },
            function myError(response) {
                $scope.response = response.data;
            });

    };
    $scope.WorksheetDelete = function () {
        var url = '/api/v1/Worksheet/Delete?id=' + $scope.WorksheetToDelete;
        var responsedata = apiService.masterdelete(url);
        responsedata.then(function mySucces(response) {
            $scope.response = response.data;
            $scope.growltext("Worksheet deleted successfully.", false);
            window.location.reload();
            $scope.WorksheetToDelete = 0;
        },
            function myError(response) {
                $scope.response = response.data;
                $scope.growltext("Worksheet deletion failed", true);
                $scope.WorksheetToDelete = 0;
            });
    };

    $scope.ConfirmDelete = function (id) {
        $scope.WorksheetToDelete = id;
    };

    $scope.GetWorksheets();
}]);

