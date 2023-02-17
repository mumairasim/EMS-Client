//(function (app) {
//    'use strict';

SMSHO.controller('employeeFinanceBaseCtrl', ['$scope', 'apiService', '$cookies', function ($scope, apiService, $cookies) {
    'use strict';

    $scope.Month = "0";
    $scope.Months = ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'];

    $scope.EmployeeFinanceDelete = function () {
        var url = '/api/v1/EmployeeFinance/Delete?id=' + $scope.EmployeeFinanceToDelete;
        var responsedata = apiService.masterdelete(url);
        responsedata.then(function mySucces(response) {
            $scope.response = response.data;
            $scope.growltext("EmployeeFinance deleted successfully.", false);
            window.location.reload();
            $scope.EmployeeFinanceToDelete = 0;
        },
            function myError(response) {
                $scope.response = response.data;
                $scope.growltext("EmployeeFinance deletion failed", true);
                $scope.EmployeeFinanceToDelete = 0;
            });
    };

    $scope.ConfirmDelete = function (id) {
        $scope.EmployeeFinanceToDelete = id;
    };


    $scope.GetSchools = function () {
        var responsedata = apiService.masterget('/api/v1/School/Get');
        responsedata.then(function mySucces(response) {
            $scope.Schools = response.data.Schools;
            $scope.School = $scope.Schools[0];
        },
            function myError(response) {

                $scope.response = response.data;
            });
    };

    $scope.GetFinances = function () {
        var responsedata = apiService.masterget('/api/v1/EmployeeFinance/GetByFilter/' + $scope.School.Id + '/' + $scope.Month);
        responsedata.then(function mySucces(response) {
            $scope.FinanceList = response.data;
        },
            function myError(response) {
                $scope.response = response.data;
            });
    };

    $scope.GetSchools();

}]);

