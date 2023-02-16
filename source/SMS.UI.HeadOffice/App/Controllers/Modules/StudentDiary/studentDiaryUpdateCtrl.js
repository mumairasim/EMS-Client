SMSHO.controller('studentDiaryUpdateCtrl', ['$scope', 'apiService', '$routeParams', '$cookies', function ($scope, apiService, $routeParams,$cookies ) {
    'use strict';
    $scope.StudentDiaryModel = {
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
   
    $scope.GetEmployees = function () {
        var responsedata = apiService.masterget('/api/v1/Employee/Get');
        responsedata.then(function mySucces(response) {
            $scope.Employees = response.data;
            $scope.FetchStudentDiary();
        },
            function myError(response) {
                $scope.response = response.data;
            });
    };
    $scope.GetSchools = function () {
        var responsedata = apiService.masterget('/api/v1/School/Get');
        responsedata.then(function mySucces(response) {
            $scope.Schools = response.data;
            $scope.FetchStudentDiary();
        },
            function myError(response) {
                $scope.response = response.data;
            });
    };
    $scope.StudentDiaryUpdate = function () {
        var data = $scope.StudentDiaryModel;
        var formData = new FormData();
        formData.append('studentDiaryModel', JSON.stringify(data));
        var responsedata = apiService.masterput('/api/v1/StudentDiary/Update', formData);
        responsedata.then(function mySucces(response) {
            $scope.response = response.data;
            $scope.growltext("StudentDiary updated successfully.", false);
            window.location = "#!/studentDiaryBase";
        },
            function myError(response) {
                $scope.response = response.data;
                $scope.growltext("StudentDiary updation failed", true);
            });
    };
    $scope.FetchStudentDiary = function () {
        var id = $routeParams.Id;
        var url = '/api/v1/StudentDiary/Get?id=' + id;
        var responsedata = apiService.masterget(url);
        responsedata.then(function mySucces(response) {
            $scope.StudentDiaryModel = response.data;
        },
            function myError(response) {
                $scope.response = response.data;
            });
    };
    $scope.Cancel = function () {
        window.location = "#!/studentDiaryBase";
    };
    $scope.GetEmployees();
    $scope.GetSchools();
}]);