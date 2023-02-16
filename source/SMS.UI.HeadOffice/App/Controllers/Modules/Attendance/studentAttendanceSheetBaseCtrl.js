
SMSHO.controller('studentAttendanceSheetBaseCtrl', ['$scope', 'apiService', '$cookies', function($scope, apiService, $cookies) {
    'use strict';
    $scope.pageSize = "10";
    $scope.pageNumber = 1;
    $scope.searchedText = "";
    $scope.GetStudentsAttendance = function() {
        var responsedata = apiService.masterget('/api/v1/StudentAttendance/Get?searchString=' + $scope.searchedText + '&pageNumber=' + $scope.pageNumber + '&pageSize=' + $scope.pageSize);
        responsedata.then(function mySucces(response) {
            $scope.studentAttendanceList = response.data.Items;
            $scope.Count = response.data.Count;
            $scope.NextAndPreviousButtonsEnablingAndDisabling();
        },
            function myError(response) {
                $scope.response = response.data;
            });
    };
    $scope.NextAndPreviousButtonsEnablingAndDisabling = function() {
        if ($scope.TotalStudentAttendanceCount > $scope.pageNumber * $scope.pageSize) {
            $("#nextButton").removeClass('disabled');
        } else {
            $("#nextButton").addClass('disabled');
        }
        if ($scope.pageNumber > 1) {
            $("#previousButton").removeClass('disabled');
        } else {
            $("#previousButton").addClass('disabled');
        }
    };
    $scope.nextPage = function() {
        if ($scope.TotalStudentAttendanceCount > $scope.pageNumber * $scope.pageSize) {
            $scope.pageNumber++;
            $scope.GetStudents();

        }
    };
    $scope.MoveToPage = function(page) {
        if ($scope.TotalStudentAttendanceCount > (page - 1) * $scope.pageSize) {
            $scope.pageNumber = page;
            $scope.GetStudents();
        } else {
            $scope.growltext("Page " + page + " doesn't exist.", true);
        }

    }
    $scope.previousPage = function() {
        if ($scope.pageNumber > 1)
            $scope.pageNumber--;
        $scope.GetStudents();
    };
    $scope.ConfirmDelete = function(id) {
        $scope.StudentToDelete = id;
    };
    $scope.NoDelete = function() {
        $scope.StudentToDelete = 0;
    };
    $scope.StudentDelete = function() {
        var url = '/api/v1/StudentAttendance/Delete?id=' + $scope.StudentToDelete;
        var responsedata = apiService.masterdelete(url);
        responsedata.then(function mySucces(response) {
            $scope.response = response.data;
            $scope.growltext("Student deleted successfully.", false);
            window.location.reload();
            $scope.StudentToDelete = 0;
        },
            function myError(response) {
                $scope.response = response.data;
                $scope.growltext("Student deletion failed", true);
                $scope.StudentToDelete = 0;
            });
    };

    $scope.GetClasses = function() {
        var responsedata = apiService.masterget('/api/v1/Class/GetBySchool?schoolId=' + $scope.School.Id);
        responsedata.then(function mySucces(response) {
            $scope.Classes = response.data;
            $scope.Class = $scope.Classes[0];
            $scope.AttendanceDate = new Date();
        },
            function myError(response) {
                $scope.response = response.data;
            });
    };
    $scope.GetSchools = function() {
        var responsedata = apiService.masterget('/api/v1/School/Get');
        responsedata.then(function mySucces(response) {
            $scope.Schools = response.data.Schools;
            $scope.School = $scope.Schools[0];
            $scope.GetClasses();
        },
            function myError(response) {
                $scope.response = response.data;
            });
    };
    $scope.GetStudentsAttendance();
    $scope.GetSchools();
}]);

