SMSHO.controller('worksheetCreateCtrl', ['$scope', 'apiService', '$cookies', function ($scope, apiService, $cookies) {
    'use strict';
    $scope.WorksheetModel = {
        Text: '',
        ForDate: '',
        InstructorId: '',
    };

    $scope.GetEmployees = function () {
        var responsedata = apiService.masterget('/api/v1/Employee/Get');

        responsedata.then(function mySucces(response) {

            $scope.Employees = response.data.Employees;
            $scope.WorksheetModel.Employee = $scope.Employees[0];
        },
            function myError(response) {
                $scope.response = response.data;
            });
    };

    $scope.WorksheetCreate = function () {

        var data = $scope.WorksheetModel;
        data.InstructorId = $scope.WorksheetModel.Employee.Id;
        var formData = new FormData();
        formData.append('worksheetModel', JSON.stringify(data));
        var responsedata = apiService.post('/api/v1/Worksheet/Create', formData);
        responsedata.then(function mySucces(response) {

            $scope.response = response.data;
            $scope.growltext("Worksheet created successfully.", false);
            window.location = "#!/worksheetBase";
        },
            function myError(response) {
                $scope.response = response.data;
                $scope.growltext("Worksheet creation failed", true);
            });
    };
    $scope.Cancel = function () {
        window.location = "#!/worksheetBase";
    };
    $scope.GetEmployees();

}]);