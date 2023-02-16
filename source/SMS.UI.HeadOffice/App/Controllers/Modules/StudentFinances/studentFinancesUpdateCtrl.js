SMSHO.controller('financeTypeUpdateCtrl', ['$scope', 'apiService', '$cookies', '$routeParams', function ($scope, apiService, $cookies, $routeParams) {
    'use strict';
    $scope.FinanceTypeModel = {
        Type: ''
    };

    $scope.GetEmployees = function () {
        var responsedata = apiService.masterget('/api/v1/Employee/Get');
        responsedata.then(function mySucces(response) {
            $scope.Employees = response.data.Employees;

        },
            function myError(response) {
                $scope.response = response.data;
            });
    };

    $scope.FinanceTypeUpdate = function () {
        var data = $scope.FinanceTypeModel;
        var formData = new FormData();
        formData.append('financeTypeModel', JSON.stringify(data));
        var responsedata = apiService.masterput('/api/v1/FinanceType/Update', formData);
        responsedata.then(function mySucces(response) {
            $scope.response = response.data;
            $scope.growltext("FinanceType updated successfully.", false);
            window.location = "#!/financeTypeBase";
        },
            function myError(response) {
                $scope.response = response.data;
                $scope.growltext("FinanceType updation failed", true);
            });
    };
    $scope.FetchFinanceType = function () {
        var id = $routeParams.Id;
        var url = '/api/v1/FinanceType/Get?id=' + id;
        var responsedata = apiService.masterget(url);
        responsedata.then(function mySucces(response) {
            $scope.FinanceTypeModel = response.data;
        },
            function myError(response) {
                $scope.response = response.data;
            });
    };



    $scope.Cancel = function () {
        window.location = "#!/financeTypeBase";
    };
    $scope.FetchFinanceType();
    $scope.GetEmployees();

}]);