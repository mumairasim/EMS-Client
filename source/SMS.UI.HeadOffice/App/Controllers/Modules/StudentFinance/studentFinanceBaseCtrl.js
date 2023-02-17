//(function (app) {
//    'use strict';

SMSHO.controller('studentFinanceBaseCtrl', ['$scope', 'apiService', '$cookies', function ($scope, apiService, $cookies) {
    'use strict';

    $scope.classLoaded = false;
    $scope.Month = "0";
    $scope.Class = ""
    $scope.GetStudentFinances = function () {
        var responsedata = apiService.masterget('/api/v1/StudentFinance/GetAll');
        responsedata.then(function mySucces(response) {
            $scope.studentFinanceList = response.data;
        },
            function myError(response) {
                $scope.response = response.data;
            });

    };
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
            $scope.GetClasses();
        },
            function myError(response) {

                $scope.response = response.data;
            });
    };

    $scope.GetClasses = function () {
        var responsedata = apiService.masterget('/api/v1/Class/GetBySchool?schoolId=' + $scope.School.Id);
        responsedata.then(function mySucces(response) {
            var temp = {
                ClassName: '-- Ignore --',
                Id: '0'
            }
            $scope.Classes = response.data;
            $scope.Classes.unshift(temp);
            $scope.Class = $scope.Classes[0];
            $scope.classLoaded = true;
        },
            function myError(response) {
                $scope.classLoaded = false;
                $scope.response = response.data;
            });
    };
    $scope.test = function (e) {
        debugger;
    }
    $scope.GetFinances = function () {
        var responsedata = apiService.masterget('/api/v1/StudentFinance/GetByFilter/' + $scope.School.Id +
            '/' + $scope.Class.Id + '/' + $scope.Month);
        responsedata.then(function mySucces(response) {
            $scope.FinanceList = response.data;
        },
            function myError(response) {
                $scope.response = response.data;
            });
    };


    $scope.GetSchools();





}]);

