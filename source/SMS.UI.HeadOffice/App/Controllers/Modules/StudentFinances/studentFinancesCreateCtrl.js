SMSHO.controller('financeTypeCreateCtrl', ['$scope', 'apiService', '$cookies', function ($scope, apiService, $cookies) {
    'use strict';
    $scope.FinanceTypeModel = {
        Type: ''
    };

    $scope.GetEmployees = function () {
        var responsedata = apiService.masterget('/api/v1/Employee/Get');

        responsedata.then(function mySucces(response) {

            $scope.Employees = response.data.Employees;
            $scope.FinanceTypeModel.Employee = $scope.Employees[0];
        },
            function myError(response) {
                $scope.response = response.data;
            });
    };

    $scope.FinanceTypeCreate = function () {

        var data = $scope.FinanceTypeModel;
        var formData = new FormData();
        formData.append('financeTypeModel', JSON.stringify(data));
        var responsedata = apiService.post('/api/v1/FinanceType/Create', formData);
        responsedata.then(function mySucces(response) {

            $scope.response = response.data;
            $scope.growltext("FinanceType created successfully.", false);
            window.location = "#!/financeTypeBase";
        },
            function myError(response) {
                $scope.response = response.data;
                $scope.growltext("FinanceType creation failed", true);
            });
    };
    $scope.Cancel = function () {
        window.location = "#!/financeTypeBase";
    };
    $scope.GetEmployees();

}]);