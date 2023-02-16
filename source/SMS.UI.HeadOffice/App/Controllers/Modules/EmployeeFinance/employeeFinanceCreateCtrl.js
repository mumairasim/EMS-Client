//(function (app) {
//    'use strict';

SMSHO.controller('employeeFinanceCreateCtrl', ['$scope', 'apiService', '$cookies', function ($scope, apiService, $cookies) {
    'use strict';
    var fullDate = new Date();
    $scope.Year = fullDate.getFullYear();
    $scope.Month = 0;
    $scope.Months = [{ id: '0', name: 'January' }, { id: '1', name: 'February' }, { id: '2', name: 'March' },
    { id: '3', name: 'April' }, { id: '4', name: 'May' }, { id: '5', name: 'June' }, { id: '6', name: 'July' },
    { id: '7', name: 'August' }, { id: '8', name: 'September' }, { id: '9', name: 'October' }, { id: '10', name: 'November' }, { id: '11', name: 'December' }];

    $scope.Month = $scope.Months[fullDate.getMonth()];

    $scope.EmployeeFinanceDelete = function () {
        var url = '/api/v1/EmployeeFinance/Delete?id=' + $scope.EmployeeFinanceToDelete;
        var responsedata = apiService.masterdelete(url);
        responsedata.then(function mySucces(response) {
            $scope.response = response.data;
            $scope.growltext("EmployeeFinance deleted successfully.", false);
            window.location.reload();
            $scope.EmployeeFinanceToDelete = 0;
        },
            function myError(response) {
                $scope.response = response.data;
                $scope.growltext("EmployeeFinance deletion failed", true);
                $scope.EmployeeFinanceToDelete = 0;
            });
    };

    $scope.ConfirmDelete = function (id) {
        $scope.EmployeeFinanceToDelete = id;
    };


    $scope.GetSchools = function () {
        var responsedata = apiService.masterget('/api/v1/School/Get');
        responsedata.then(function mySucces(response) {
            $scope.Schools = response.data.Schools;
            var temp = {
                Name: '-- Ignore --',
                Id: '0'
            }
            $scope.Schools.unshift(temp);
            $scope.School = $scope.Schools[0];
        },
            function myError(response) {

                $scope.response = response.data;
            });
    };



    $scope.GetFinances = function () {
        var responsedata = apiService.masterget('/api/v1/EmployeeFinance/GetDetailByFilter/' + $scope.School.Id);
        responsedata.then(function mySucces(response) {
            $scope.FinanceList = response.data;
        },
            function myError(response) {
                $scope.response = response.data;
            });
    };

    $scope.SaveFinances = function () {
        var data = $scope.FinanceList;
        for (var i = 0; i < data.length; i++) {
            data[i].SalaryMonth = $scope.Months[$scope.Month.id].name;
            data[i].SalaryYear = $scope.Year;
        }
        var formData = new FormData();
        formData.append('form', JSON.stringify(data));
        var responsedata = apiService.post('/api/v1/EmployeeFinance/Create', formData);
        responsedata.then(function mySucces(response) {
            $scope.FinanceList = response.data;
        },
            function myError(response) {
                $scope.response = response.data;
            });
    };




    $scope.GetSchools();

}]);

