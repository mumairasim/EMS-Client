SMSHO.controller('timeTableBaseCtrl', ['$scope', 'apiService', '$cookies', '$filter', function ($scope, apiService, $cookies, $filter) {
    'use strict';
    $scope.TimeTable = {
        TimeTableDetails: [{
            Day: "",
            Periods: [{
                Course: "",
                Employee: "",
                StartTime: "",
                EndTime: ""
            }]
        }]
    };
    $scope.GetClasses = function () {
        var responsedata = apiService.masterget('/api/v1/Class/GetBySchool?schoolId=' + $scope.TimeTable.School.Id);
        responsedata.then(function mySucces(response) {
            $scope.Classes = response.data;
            $scope.TimeTable.Class = $scope.Classes[0];
            $scope.GetTimeTables();
        },
            function myError(response) {
                $scope.response = response.data;
            });
    };
    $scope.GetSchools = function () {
        var responsedata = apiService.masterget('/api/v1/School/Get');
        responsedata.then(function mySucces(response) {
            $scope.Schools = response.data.Schools;
            $scope.TimeTable.School = $scope.Schools[0];
            $scope.GetClasses();
        },
            function myError(response) {
                $scope.response = response.data;
            });
    };
    $scope.GetTimeTables = function () {
        var responsedata = apiService.masterget('/api/v1/TimeTable/Get?schoolId=' + $scope.TimeTable.School.Id + '&&classId=' + $scope.TimeTable.Class.Id + '&&pageNumber=1&&pageSize=10');
        responsedata.then(function mySucces(response) {
            $scope.TimeTableList = response.data;
        },
            function myError(response) {
                $scope.response = response.data;
            });
    };

    $scope.GetSchools();
}]);