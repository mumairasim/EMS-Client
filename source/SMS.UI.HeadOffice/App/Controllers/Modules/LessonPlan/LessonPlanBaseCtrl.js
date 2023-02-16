
SMSHO.controller('lessonPlanBaseCtrl', ['$scope', 'apiService', '$cookies', function ($scope, apiService, $cookies) {
    'use strict';
    $scope.pageSize = "10";
    $scope.pageNumber = 1;
    $scope.GetLessonPlans = function () {
        $scope.loader(true);
        var responsedata = apiService.masterget('/api/v1/LessonPlan/Get?pageNumber=' + $scope.pageNumber + '&pageSize=' + $scope.pageSize);
        responsedata.then(function mySucces(response) {
            $scope.lessonPlanList = response.data.LessonPlans;
            $scope.TotalLessonPlans = response.data.LessonPlansCount;
            $scope.NextAndPreviousButtonsEnablingAndDisabling();
            $scope.loader(false);
        },
            function myError(response) {
                $scope.response = response.data;
            });
    };
    $scope.NextAndPreviousButtonsEnablingAndDisabling = function () {
        if ($scope.TotalLessonPlans > $scope.pageNumber * $scope.pageSize) {
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
        if ($scope.TotalLessonPlans > $scope.pageNumber * $scope.pageSize) {
            $scope.pageNumber++;
            $scope.GetLessonPlans();

        }
    };
    $scope.MoveToPage = function (page) {
        if ($scope.TotalLessonPlans > (page - 1) * $scope.pageSize) {
            $scope.pageNumber = page;
            $scope.GetLessonPlans();
        } else {
            $scope.growltext("Page " + page + " doesn't exist.", true);
        }

    }
    $scope.previousPage = function () {
        if ($scope.pageNumber > 1)
            $scope.pageNumber--;
        $scope.GetLessonPlans();
    };
    $scope.ConfirmDelete = function (id) {
        $scope.LessonPlanToDelete = id;
    };
    $scope.NoDelete = function () {
        $scope.LessonPlanToDelete = 0;
    };
    $scope.LessonPlanDelete = function () {
        var url = '/api/v1/LessonPlan/Delete?id=' + $scope.LessonPlanToDelete;
        var responsedata = apiService.masterdelete(url);
        responsedata.then(function mySucces(response) {
            $scope.response = response.data;
            $scope.growltext("Lesson Plan deleted successfully.", false);
            window.location.reload();
            $scope.LessonPlanToDelete = 0;
        },
            function myError(response) {
                $scope.response = response.data;
                $scope.growltext("Lesson Plan deletion failed", true);
                $scope.LessonPlanToDelete = 0;
            });
    };
    $scope.GetLessonPlans();
}]);

