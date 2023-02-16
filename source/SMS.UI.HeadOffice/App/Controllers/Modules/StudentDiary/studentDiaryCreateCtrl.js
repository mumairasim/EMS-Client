SMSHO.controller('studentDiaryCreateCtrl', ['$scope', 'apiService', '$cookies', function ($scope, apiService, $cookies) {
    'use strict';
    $scope.StudentDiaryDiaryModel = {
        Diarytext: '',
        DairyDate: '',
        Employee: $scope.Employee,
        School: $scope.School
    };
    $scope.Employee = {
        Name: ''
    };
    $scope.School = {
        Id: '',
        Name: '',
        Location: ''
    };
    $scope.GetSchools = function () {
        var responsedata = apiService.masterget('/api/v1/School/Get');
        responsedata.then(function mySucces(response) {
            $scope.Schools = response.data;
            $scope.StudentDiaryModel.School = $scope.Schools.Schools[0];
        },
            function myError(response) {
                $scope.response = response.data;
            });
    };
    $scope.GetEmployees = function () {
        var responsedata = apiService.masterget('/api/v1/Employee/Get');
        responsedata.then(function mySucces(response) {
            $scope.Employees = response.data;
            $scope.StudentDiaryModel.Employee = $scope.Employees.Employees[0];
        },
            function myError(response) {
                $scope.response = response.data;
            });
    };
    $scope.StudentDiaryCreate = function () {
        var data = $scope.StudentDiaryModel;
        var formData = new FormData();
        formData.append('studentDiaryModel', JSON.stringify(data));
        var responsedata = apiService.post('/api/v1/StudentDiary/Create', formData);
        responsedata.then(function mySucces(response) {
            $scope.response = response.data;
            $scope.growltext("StudentDiary created successfully.", false);
            window.location = "#!/studentDiaryBase";
        },
            function myError(response) {
                $scope.response = response.data;
                $scope.growltext("StudentDiary creation failed", true);
            });
    };
    $scope.Cancel = function () {
        window.location = "#!/studentDiaryBase";
    };
    $scope.GetEmployees();
    $scope.GetSchools();
}]);