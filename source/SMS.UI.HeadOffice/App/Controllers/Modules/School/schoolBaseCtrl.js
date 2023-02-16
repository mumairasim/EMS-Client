//(function (app) {
//    'use strict';

SMSHO.controller('schoolBaseCtrl', ['$scope', 'apiService', '$cookies', function ($scope, apiService, $cookies) {
    'use strict';
    $scope.pageSize = "10";
    $scope.pageNumber = 1;
    $scope.searchedText = "";
    $scope.GetSchool = function () {
        $scope.loader(true);
        var responsedata = apiService.masterget('/api/v1/School/Get?searchString=' + $scope.searchedText + '&pageNumber=' + $scope.pageNumber + '&pageSize=' + $scope.pageSize);
        responsedata.then(function mySucces(response) {
            $scope.SchoolsList = response.data.Schools;
            $scope.TotalSchool = response.data.SchoolsCount;
            $scope.NextAndPreviousButtonsEnablingAndDisabling();
            $scope.loader(false);
        },
            function myError(response) {
                $scope.response = response.data;
            });
    };
    $scope.NextAndPreviousButtonsEnablingAndDisabling = function () {
        //if ($scope.TotalSchool > $scope.pageNumber * $scope.pageSize) {
        //    $("#nextButton").removeSchool('disabled');
        //} else {
        //    $("#nextButton").addSchool('disabled');
        //}
        //if ($scope.pageNumber > 1) {
        //    $("#previousButton").removeSchool('disabled');
        //} else {
        //    $("#previousButton").addSchool('disabled');
        //}
    };
    $scope.nextPage = function () {
        if ($scope.TotalSchool > $scope.pageNumber * $scope.pageSize) {
            $scope.pageNumber++;
            $scope.GetSchool();

        }
    };
    $scope.MoveToPage = function (page) {
        if ($scope.TotalSchool > (page - 1) * $scope.pageSize) {
            $scope.pageNumber = page;
            $scope.GetSchool();
        } else {
            $scope.growltext("Page " + page + " doesn't exist.", true);
        }

    }
    $scope.previousPage = function () {
        if ($scope.pageNumber > 1)
            $scope.pageNumber--;
        $scope.GetSchool();
    };
    $scope.ConfirmDelete = function (id) {
        $scope.SchoolToDelete = id;
    };
    $scope.NoDelete = function () {
        $scope.SchoolToDelete = 0;
    };
    $scope.SchoolDelete = function () {
        var url = '/api/v1/School/Delete?id=' + $scope.SchoolToDelete;
        var responsedata = apiService.masterdelete(url);
        responsedata.then(function mySucces(response) {
            $scope.response = response.data;
            $scope.growltext("School deleted successfully.", false);
            window.location.reload();
            $scope.SchoolToDelete = 0;
        },
            function myError(response) {
                $scope.response = response.data;
                $scope.growltext("School deletion failed", true);
                $scope.SchoolToDelete = 0;
            });
    };
    $scope.GetSchool();
}]);

