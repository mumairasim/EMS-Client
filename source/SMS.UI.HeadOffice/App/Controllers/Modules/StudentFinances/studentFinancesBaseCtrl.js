//(function (app) {
//    'use strict';

SMSHO.controller('financeTypeBaseCtrl', ['$scope', 'apiService', '$cookies', function ($scope, apiService, $cookies) {
    'use strict';
    $scope.GetFinanceTypes = function () {
        var responsedata = apiService.masterget('/api/v1/FinanceType/GetAll');
        responsedata.then(function mySucces(response) {
            $scope.financeTypeList = response.data;
        },
            function myError(response) {
                $scope.response = response.data;
            });

    };
    $scope.FinanceTypeDelete = function () {
        var url = '/api/v1/FinanceType/Delete?id=' + $scope.FinanceTypeToDelete;
        var responsedata = apiService.masterdelete(url);
        responsedata.then(function mySucces(response) {
            $scope.response = response.data;
            $scope.growltext("FinanceType deleted successfully.", false);
            window.location.reload();
            $scope.FinanceTypeToDelete = 0;
        },
            function myError(response) {
                $scope.response = response.data;
                $scope.growltext("FinanceType deletion failed", true);
                $scope.FinanceTypeToDelete = 0;
            });
    };

    $scope.ConfirmDelete = function (id) {
        $scope.FinanceTypeToDelete = id;
    };

    $scope.GetFinanceTypes();
}]);

