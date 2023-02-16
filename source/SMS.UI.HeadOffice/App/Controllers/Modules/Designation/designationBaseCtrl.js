
SMSHO.controller('designationBaseCtrl', ['$scope', 'apiService', '$cookies', function ($scope, apiService, $cookies) {
    'use strict';
    $scope.pageSize = "10";
    $scope.pageNumber = 1;
    $scope.searchedText = "";

    $scope.GetDesignation = function () {
        $scope.loader(true);
        var responsedata = apiService.masterget('/api/v1/Designation/Get?searchString=' + $scope.searchedText + '&pageNumber=' + $scope.pageNumber + '&pageSize=' + $scope.pageSize);
        responsedata.then(function mySucces(response) {
            $scope.designationList = response.data.Items;
            $scope.Count = response.data.Count;
            $scope.NextAndPreviousButtonsEnablingAndDisabling();
            $scope.loader(false);
        },
            function myError(response) {
                $scope.response = response.data;
            });
    };
    $scope.NextAndPreviousButtonsEnablingAndDisabling = function () {
        if ($scope.Count > $scope.pageNumber * $scope.pageSize) {
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
        if ($scope.Count > $scope.pageNumber * $scope.pageSize) {
            $scope.pageNumber++;
            $scope.GetDesignation();

        }
    };
    $scope.MoveToPage = function (page) {
        if ($scope.Count > (page - 1) * $scope.pageSize) {
            $scope.pageNumber = page;
            $scope.GetDesignation();
        } else {
            $scope.growltext("Page " + page + " doesn't exist.", true);
        }

    }
    $scope.previousPage = function () {
        if ($scope.pageNumber > 1)
            $scope.pageNumber--;
        $scope.GetDesignation();
    };
    $scope.ConfirmDelete = function (id) {
        $scope.DesignationToDelete = id;
    };
    $scope.NoDelete = function () {
        $scope.DesignationToDelete = 0;
    };
    $scope.DesignationDelete = function () {
        var url = '/api/v1/Designation/Delete?id=' + $scope.DesignationToDelete;
        var responsedata = apiService.masterdelete(url);
        responsedata.then(function mySucces(response) {
            $scope.response = response.data;
            $scope.growltext("Designation deleted successfully.", false);
            window.location.reload();
            $scope.DesignationToDelete = 0;
        },
            function myError(response) {
                $scope.response = response.data;
                $scope.growltext("Designation deletion failed", true);
                $scope.DesignationToDelete = 0;
            });
    };
    $scope.GetDesignation();
}]);

