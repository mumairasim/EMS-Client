SMSHO.controller('worksheetUpdateCtrl', ['$scope', 'apiService', '$cookies', '$routeParams', function ($scope, apiService, $cookies, $routeParams) {
    'use strict';
    $scope.WorksheetModel = {
        Text: '',
        ForDate: '',
        InstructorId: ''
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

    $scope.WorksheetUpdate = function () {
        var data = $scope.WorksheetModel;
        $scope.WorksheetModel.InstructorId = $scope.WorksheetModel.Employee.Id;
        var formData = new FormData();
        formData.append('worksheetModel', JSON.stringify(data));
        var responsedata = apiService.masterput('/api/v1/Worksheet/Update', formData);
        responsedata.then(function mySucces(response) {
            $scope.response = response.data;
            $scope.growltext("Worksheet updated successfully.", false);
            window.location = "#!/worksheetBase";
        },
            function myError(response) {
                $scope.response = response.data;
                $scope.growltext("Worksheet updation failed", true);
            });
    };
    $scope.FetchWorksheet = function () {
        var id = $routeParams.Id;
        var url = '/api/v1/Worksheet/Get?id=' + id;
        var responsedata = apiService.masterget(url);
        responsedata.then(function mySucces(response) {
            $scope.WorksheetModel = response.data;
            $scope.WorksheetModel.ForDate = new Date(response.data.ForDate);
        },
            function myError(response) {
                $scope.response = response.data;
            });
    };



    $scope.Cancel = function () {
        window.location = "#!/worksheetBase";
    };
    $scope.FetchWorksheet();
    $scope.GetEmployees();

}]);