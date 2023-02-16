

SMSHO.controller('employeeBaseCtrl', ['$scope', 'apiService', '$cookies', function ($scope, apiService, $cookies) {
    'use strict';
    $scope.pageSize = "10";
    $scope.pageNumber = 1;
    $scope.searchedText = "";
    $scope.employeeNumber = "";
    $scope.GetEmployees = function () {
        var responsedata = apiService.masterget('/api/v1/Employee/Get?searchString=' + $scope.searchedText + '&pageNumber=' + $scope.pageNumber + '&pageSize=' + $scope.pageSize);
        responsedata.then(function mySucces(response) {
            $scope.employeeList = response.data.Employees;
            $scope.TotalEmployees = response.data.EmployeesCount;
            $scope.NextAndPreviousButtonsEnablingAndDisabling();
        },
            function myError(response) {
                $scope.response = response.data;
            });
    };
    $scope.GetEmployeesforGetEmpNo = function () {
        var responsedata = apiService.masterget('/api/v1/Employee/GetEmpNo?employeeNumber=' + $scope.employeeNumber + '&pageNumber=' + $scope.pageNumber + '&pageSize=' + $scope.pageSize);
        responsedata.then(function mySucces(response) {
                $scope.employeeList = response.data.Employees;
                $scope.TotalEmployees = response.data.EmployeesCount;
                $scope.NextAndPreviousButtonsEnablingAndDisabling();
            },
            function myError(response) {
                $scope.response = response.data;
            });
    };
    
    $scope.NextAndPreviousButtonsEnablingAndDisabling = function () {
        if ($scope.TotalEmployees > $scope.pageNumber * $scope.pageSize) {
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
        if ($scope.TotalEmployees > $scope.pageNumber * $scope.pageSize) {
            $scope.pageNumber++;
            $scope.GetEmployees();

        }
    };
    $scope.MoveToPage = function (page) {
        if ($scope.TotalEmployees > (page - 1) * $scope.pageSize) {
            $scope.pageNumber = page;
            $scope.GetEmployees();
        } else {
            $scope.growltext("Page " + page + " doesn't exist.", true);
        }
    }
    $scope.previousPage = function () {
        if ($scope.pageNumber > 1)
            $scope.pageNumber--;
        $scope.GetEmployees();
    };
    $scope.ConfirmDelete = function (id) {
        $scope.EmployeeToDelete = id;
    };
    $scope.NoDelete = function () {
        $scope.EmployeeToDelete = 0;
    };
    $scope.EmployeeDelete = function () {
        var url = '/api/v1/Employee/Delete?id=' + $scope.EmployeeToDelete;
        var responsedata = apiService.masterdelete(url);
        responsedata.then(function mySucces(response) {
            $scope.response = response.data;
            $scope.growltext("Employee deleted successfully.", false);
            window.location.reload();
            $scope.EmployeeToDelete = 0;
        },
            function myError(response) {
                $scope.response = response.data;
                $scope.growltext("Employee deletion failed", true);
                $scope.EmployeeToDelete = 0;
            });
    };
    $scope.GetEmployees();
}]);







