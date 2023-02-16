SMSHO.controller('requestMetaBaseCtrl', ['$scope', 'apiService', '$cookies', function ($scope, apiService, $cookies) {
    'use strict';
    $scope.pageSize = "10";
    $scope.pageNumber = 1;
    $scope.searchedText = "";
    $scope.GetRequestMetas = function () {
        debugger;
        var responsedata = apiService.masterget('/api/v1/RequestMeta/Get?searchString=' + $scope.searchedText + '&pageNumber=' + $scope.pageNumber + '&pageSize=' + $scope.pageSize);
        responsedata.then(function mySucces(response) {
            $scope.requestMetaList = response.data.Items;
            $scope.Count = response.data.Count;
        },
            function myError(response) {
                $scope.response = response.data;
            });

    };
    $scope.RequestMetaDelete = function () {
        var url = '/api/v1/RequestMeta/Delete?id=' + $scope.RequestMetaToDelete;
        var responsedata = apiService.masterdelete(url);
        responsedata.then(function mySucces(response) {
            $scope.response = response.data;
            $scope.growltext("RequestMeta deleted successfully.", false);
            window.location.reload();
            $scope.RequestMetaToDelete = 0;
        },
            function myError(response) {
                $scope.response = response.data;
                $scope.growltext("RequestMeta deletion failed", true);
                $scope.RequestMetaToDelete = 0;
            });
    };

    $scope.ConfirmDelete = function (id) {
        $scope.RequestMetaToDelete = id;
    };

    $scope.GetRequestMetas();
}]);








