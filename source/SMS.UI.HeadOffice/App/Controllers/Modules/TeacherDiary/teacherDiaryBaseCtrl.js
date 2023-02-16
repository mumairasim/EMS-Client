
SMSHO.controller('teacherDiaryBaseCtrl', ['$scope', 'apiService', '$cookies', function ($scope, apiService, $cookies) {
    'use strict';
    $scope.pageSize = "10";
    $scope.pageNumber = 1;
    $scope.searchedText = "";

    $scope.GetTeacherDiaries = function () {
        $scope.loader(true);
        var responsedata = apiService.masterget('/api/v1/TeacherDiary/Get?searchString=' + $scope.searchedText + '&pageNumber=' + $scope.pageNumber + '&pageSize=' + $scope.pageSize);
        responsedata.then(function mySucces(response) {
            $scope.teacherDiaryList = response.data.Items;
            $scope.TotalTeacherDiaries = response.data.Count;
            $scope.NextAndPreviousButtonsEnablingAndDisabling();
            $scope.loader(false);
        },
            function myError(response) {
                $scope.response = response.data;
            });
    };
    $scope.NextAndPreviousButtonsEnablingAndDisabling = function () {
        if ($scope.TotalTeacherDiaries > $scope.pageNumber * $scope.pageSize) {
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
    $scope.nextPage = function () {
        if ($scope.TotalTeacherDiaries > $scope.pageNumber * $scope.pageSize) {
            $scope.pageNumber++;
            $scope.GetTeacherDiaries();

        }
    };
    $scope.MoveToPage = function (page) {
        if ($scope.TotalTeacherDiaries > (page - 1) * $scope.pageSize) {
            $scope.pageNumber = page;
            $scope.GetTeacherDiaries();
        } else {
            $scope.growltext("Page " + page + " doesn't exist.", true);
        }

    }
    $scope.previousPage = function () {
        if ($scope.pageNumber > 1)
            $scope.pageNumber--;
        $scope.GetTeacherDiaries();
    };
    $scope.ConfirmDelete = function (id) {
        $scope.TeacherDiaryToDelete = id;
    };
    $scope.NoDelete = function () {
        $scope.TeacherDiaryToDelete = 0;
    };
    $scope.TeacherDiaryDelete = function () {
        var url = '/api/v1/TeacherDiary/Delete?id=' + $scope.TeacherDiaryToDelete;
        var responsedata = apiService.masterdelete(url);
        responsedata.then(function mySucces(response) {
            $scope.response = response.data;
            $scope.growltext("Teacher Diary deleted successfully.", false);
            window.location.reload();
            $scope.TeacherDiaryToDelete = 0;
        },
            function myError(response) {
                $scope.response = response.data;
                $scope.growltext("Teacher Diary deletion failed", true);
                $scope.TeacherDiaryToDelete = 0;
            });
    };
    $scope.GetTeacherDiaries();
}]);

