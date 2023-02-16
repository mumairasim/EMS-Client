//(function (app) {
//    'use strict';

SMSHO.controller('courseBaseCtrl', ['$scope', 'apiService', '$cookies', function ($scope, apiService, $cookies) {
    'use strict';
    $scope.pageSize = "10";
    $scope.pageNumber = 1;
    $scope.searchedText = "";
    $scope.GetCourses = function () {
        var responsedata = apiService.masterget('/api/v1/Course/Get?searchString=' + $scope.searchedText + '&pageNumber=' + $scope.pageNumber + '&pageSize=' + $scope.pageSize);
        responsedata.then(function mySucces(response) {
            $scope.courseList = response.data.Courses;
            $scope.Count = response.data.Count;
            $scope.NextAndPreviousButtonsEnablingAndDisabling();
            $scope.loader(false);
        },
            function myError(response) {
                $scope.response = response.data;
            });

    };
    $scope.CourseDelete = function () {
        var url = '/api/v1/Course/Delete?id=' + $scope.CourseToDelete;
        var responsedata = apiService.masterdelete(url);
        responsedata.then(function mySucces(response) {
            $scope.response = response.data;
            $scope.growltext("Course deleted successfully.", false);
            window.location.reload();
            $scope.CourseToDelete = 0;
        },
            function myError(response) {
                $scope.response = response.data;
                $scope.growltext("Course deletion failed", true);
                $scope.CourseToDelete = 0;
            });
    };

    $scope.ConfirmDelete = function (id) {
        $scope.CourseToDelete = id;
    };

    $scope.GetCourses();
}]);

