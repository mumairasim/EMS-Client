SMSHO.controller('designationUpdateCtrl', ['$scope', 'apiService', '$cookies', '$routeParams', function ($scope, apiService, $cookies, $routeParams) {
    'use strict';
    $scope.DesignationModel = {
        Name: '',
        DairyText: '',
        DairyDate: '',
        School: $scope.School,
        Employee: $scope.Employee
    };
    $scope.Employee = {
        Name: ''
    };
    $scope.School = {
        Id: '',
        Name: '',
        Location: ''
    };

    $scope.GetEmployeeByDesignation = function () {
        var responsedata = apiService.masterget('/api/v1/Employee/GetDesignation');
        responsedata.then(function mySucces(response) {
            $scope.Employees = response.data;
            $scope.FetchDesignation();
        },
            function myError(response) {
                $scope.response = response.data;
            });
    };
    $scope.GetSchools = function () {
        var responsedata = apiService.masterget('/api/v1/School/Get');
        responsedata.then(function mySucces(response) {
            $scope.Schools = response.data.Schools;
            $scope.GetEmployeeByDesignation();
        },
            function myError(response) {
                $scope.response = response.data;
            });
    };

    $scope.DesignationUpdate = function () {
        var data = $scope.DesignationModel;
        var formData = new FormData();
        formData.append('designationModel', JSON.stringify(data));
        var responsedata = apiService.masterput('/api/v1/Designation/Update', formData);
        responsedata.then(function mySucces(response) {
            $scope.response = response.data;
            $scope.growltext("Designation updated successfully.", false);
            window.location = "#!/designationBase";
        },
            function myError(response) {
                $scope.response = response.data;
                $scope.growltext("Designation updation failed", true);
            });

    };
    $scope.FetchDesignation = function () {
        var id = $routeParams.Id;
        var url = '/api/v1/Designation/Get?id=' + id;
        var responsedata = apiService.masterget(url);
        responsedata.then(function mySucces(response) {
            $scope.DesignationModel = response.data;
        },
            function myError(response) {
                $scope.response = response.data;
            });
    };

    $scope.Cancel = function () {
        window.location = "#!/DesignationBase";
    };

    $scope.FetchDesignation();
}]);