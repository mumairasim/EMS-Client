//(function (app) {
//    'use strict';

SMSHO.controller('classBaseCtrl', ['$scope', 'apiService', '$cookies', function ($scope, apiService, $cookies) {
    'use strict';
    $scope.pageSize = "10";
    $scope.pageNumber = 1;
    $scope.searchedText = "";
    $scope.GetClass = function () {
        $scope.loader(true);
        var responsedata = apiService.masterget('/api/v1/Class/Get?searchString=' + $scope.searchedText + '&pageNumber=' + $scope.pageNumber + '&pageSize=' + $scope.pageSize);
        debugger;
        responsedata.then(function mySucces(response) {
            $scope.ClassList = response.data.Classes;
            $scope.TotalClass = response.data.classesCount;
            $scope.NextAndPreviousButtonsEnablingAndDisabling();
            $scope.loader(false);
        },
            function myError(response) {
                $scope.response = response.data;
            });
    };
    $scope.NextAndPreviousButtonsEnablingAndDisabling = function () {
        if ($scope.TotalClass > $scope.pageNumber * $scope.pageSize) {
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
        if ($scope.TotalClass > $scope.pageNumber * $scope.pageSize) {
            $scope.pageNumber++;
            $scope.GetClass();

        }
    };
    $scope.MoveToPage = function (page) {
        if ($scope.TotalClass > (page - 1) * $scope.pageSize) {
            $scope.pageNumber = page;
            $scope.GetClass();
        } else {
            $scope.growltext("Page " + page + " doesn't exist.", true);
        }

    }
    $scope.previousPage = function () {
        if ($scope.pageNumber > 1)
            $scope.pageNumber--;
        $scope.GetClass();
    };
    $scope.ConfirmDelete = function (id) {
        $scope.ClassToDelete = id;
    };
    $scope.NoDelete = function () {
        $scope.ClassToDelete = 0;
    };
    $scope.ClassDelete = function () {
        var url = '/api/v1/Class/Delete?id=' + $scope.ClassToDelete;
        var responsedata = apiService.masterdelete(url);
        responsedata.then(function mySucces(response) {
            $scope.response = response.data;
            $scope.growltext("Class deleted successfully.", false);
            window.location.reload();
            $scope.ClassToDelete = 0;
        },
            function myError(response) {
                $scope.response = response.data;
                $scope.growltext("Class deletion failed", true);
                $scope.ClassToDelete = 0;
            });
    };
    $scope.GetClass();

}]);

