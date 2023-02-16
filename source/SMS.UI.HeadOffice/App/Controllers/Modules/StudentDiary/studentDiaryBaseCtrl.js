

SMSHO.controller('studentDiaryBaseCtrl', ['$scope', 'apiService', '$cookies', function ($scope, apiService, $cookies) {
    'use strict';
    $scope.pageSize = "10";
    $scope.pageNumber = 1;
    $scope.searchedText = "";

    $scope.GetStudentDiary = function () {
        $scope.loader(true);
        var responsedata = apiService.masterget('/api/v1/StudentDiary/Get?searchString=' + $scope.searchedText + '&pageNumber=' + $scope.pageNumber + '&pageSize=' + $scope.pageSize);
        responsedata.then(function mySucces(response) {
            $scope.StudentDiariesList = response.data.Items;
            $scope.TotalStudentDiary = response.data.Count;
            $scope.NextAndPreviousButtonsEnablingAndDisabling();
            $scope.loader(false);
        },
            function myError(response) {
                $scope.response = response.data;
            });
    };
    $scope.NextAndPreviousButtonsEnablingAndDisabling = function () {
        if ($scope.TotalStudentDiary > $scope.pageNumber * $scope.pageSize) {
            $("#nextButton").removeStudentDiary('disabled');
        } else {
            $("#nextButton").addStudentDiary('disabled');
        }
        if ($scope.pageNumber > 1) {
            $("#previousButton").removeStudentDiary('disabled');
        } else {
            $("#previousButton").addStudentDiary('disabled');
        }
    };
    $scope.nextPage = function () {
        if ($scope.TotalStudentDiary > $scope.pageNumber * $scope.pageSize) {
            $scope.pageNumber++;
            $scope.GetStudentDiary();

        }
    };
    $scope.MoveToPage = function (page) {
        if ($scope.TotalStudentDiary > (page - 1) * $scope.pageSize) {
            $scope.pageNumber = page;
            $scope.GetStudentDiary();
        } else {
            $scope.growltext("Page " + page + " doesn't exist.", true);
        }

    }
    $scope.previousPage = function () {
        if ($scope.pageNumber > 1)
            $scope.pageNumber--;
        $scope.GetStudentDiary();
    };
    $scope.ConfirmDelete = function (id) {
        $scope.StudentDiaryToDelete = id;
    };
    $scope.NoDelete = function () {
        $scope.StudentDiaryToDelete = 0;
    };
    $scope.StudentDiaryDelete = function () {
        var url = '/api/v1/StudentDiary/Delete?id=' + $scope.StudentDiaryToDelete;
        var responsedata = apiService.masterdelete(url);
        responsedata.then(function mySucces(response) {
            $scope.response = response.data;
            $scope.growltext("StudentDiary deleted successfully.", false);
            window.location.reload();
            $scope.StudentDiaryToDelete = 0;
        },
            function myError(response) {
                $scope.response = response.data;
                $scope.growltext("StudentDiary deletion failed", true);
                $scope.StudentDiaryToDelete = 0;
            });
    };
    $scope.GetStudentDiary();
}]);

