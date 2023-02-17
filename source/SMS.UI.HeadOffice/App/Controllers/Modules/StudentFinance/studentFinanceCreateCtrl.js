//(function (app) {
//    'use strict';

SMSHO.controller('studentFinanceCreateCtrl', ['$scope', 'apiService', 'financeService', '$cookies', function ($scope, apiService, financeService, $cookies) {
    'use strict';
    var fullDate = new Date();
    $scope.Year = fullDate.getFullYear();
    $scope.Schools = {};
    $scope.Class = {};

    $scope.Months = [{ value: '0', text: 'January' }, { value: '1', text: 'February' }, { value: '2', text: 'March' },
    { value: '3', text: 'April' }, { value: '4', text: 'May' }, { value: '5', text: 'June' }, { value: '6', text: 'July' },
    { value: '7', text: 'August' }, { value: '8', text: 'September' }, { value: '9', text: 'October' }, { value: '10', text: 'November' }, { value: '11', text: 'December' }];

    $scope.Month = $scope.Months[fullDate.getMonth()];

    $scope.StudentFinanceDelete = function () {
        var url = '/api/v1/StudentFinance/Delete?id=' + $scope.StudentFinanceToDelete;
        var responsedata = apiService.masterdelete(url);
        responsedata.then(function mySucces(response) {
            $scope.response = response.data;
            $scope.growltext("StudentFinance deleted successfully.", false);
            window.location.reload();
            $scope.StudentFinanceToDelete = 0;
        },
            function myError(response) {
                $scope.response = response.data;
                $scope.growltext("StudentFinance deletion failed", true);
                $scope.StudentFinanceToDelete = 0;
            });
    };

    $scope.ConfirmDelete = function (id) {
        $scope.StudentFinanceToDelete = id;
    };


    $scope.GetSchools = function () {
        var responsedata = apiService.masterget('/api/v1/School/Get');
        responsedata.then(function mySucces(response) {
            $scope.Schools = response.data.Schools;
            $scope.School = $scope.Schools[0];
            $scope.GetFinances();
        },
            function myError(response) {

                $scope.response = response.data;
            });
    };
    $scope.GetClasses = function () {
        var responsedata = apiService.masterget('/api/v1/Class/Get');
        responsedata.then(function mySucces(response) {
            $scope.Classes = response.data.Classes;
            var tempClass = {
                ClassName: '-- Ignore --',
                Id: '0'
            };
            $scope.Classes.unshift(tempClass);
            $scope.Class = $scope.Classes[0];
        },
            function myError(response) {
                $scope.response = response.data;
            });
    };


    $scope.GetFinances = function () {
        var responsedata = apiService.masterget('/api/v1/StudentFinance/GetDetailByFilter?schoolId=' + $scope.School.Id
            + '&classId=' + $scope.Class.Id
            + '&regno=' + $scope.regNo
            + '&month=' + $scope.Month.text
            + '&year=' + $scope.Year);
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
            data[i].FeeMonth = $scope.Months[$scope.Month.value].text;
            data[i].FeeYear = $scope.Year;
        }
        var formData = new FormData();
        formData.append('form', JSON.stringify(data));
        var responsedata = apiService.post('/api/v1/StudentFinance/Create', formData);
        responsedata.then(function mySucces(response) {
            $scope.FinanceList = response.data;
        },
            function myError(response) {
                $scope.response = response.data;
            });
    };


    $scope.currentRecord = function (currentFinance) {
        currentFinance.Month = $scope.Month;
        currentFinance.Year = $scope.Year;
        financeService.financeRecord = currentFinance;
    };

    $scope.GetSchools();


}]);

