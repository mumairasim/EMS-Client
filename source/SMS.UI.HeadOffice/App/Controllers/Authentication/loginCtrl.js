//(function (app) {
//    'use strict';

SMSHO.controller('loginCtrl', ['$scope', 'apiService', '$cookies', function ($scope, apiService, $cookies) {
    'use strict';
    //$scope.identitydate = $cookies.get('token-expire-identity');
    $scope.Setisloggedinfalse();

    if ($cookies.get('SMS_token') != null) {
        window.location = "/#!/dashboard";
    }

    $scope.GetUser = function () {
        var url = '/api/Account/GetUserDetailedInfo';
        var responsedata = apiService.masterget(url);
        responsedata.then(function mySucces(response) {
            $scope.UserModel = response.data;
            $scope.ImageBase = '';
            if ($scope.UserModel.Image !== null && $scope.UserModel.ImageExtension !== "") {
                var imageHeader = 'data:image/' + $scope.UserModel.ImageExtension + ';base64,';
                localStorage.setItem('SMS_UserImage', imageHeader + response.data.Image);
            }
            else {
                localStorage.setItem('SMS_UserImage', null);
            }
            window.location = "/#!/dashboard";
        },
            function myError(response) {
                $scope.response = response.data;
            });
    };

    $scope.logincall = function () {
        var data = "grant_type=password&username=" + $scope.logindata.userName + "&password=" + $scope.logindata.password;
        var responsedata = apiService.login('/Token', data);
        responsedata.then(function mySucces(response) {
            $cookies.put('SMS_token', response.data.access_token);
            $cookies.put('SMS_Isloggedin', 'true');
            $cookies.put('SMS_user', response.data.userName);
            $cookies.put('SMS_userId', response.data.Id);
            $cookies.put('SMS_token-expire-identity', response.data[".expires"]);
            $cookies.put('SMS_logout', false);
            $scope.Setisloggedin();
            $scope.iserror = false;
            $scope.GetUser();
            $scope.growltext("Login sucessfull.", false);
        }, function myError(response) {
            if (response.status != 200) {
                $scope.iserror = true;
                //$scope.responsemsg = response.data.error_description;
                $scope.growltext("Login failed.", true);
            }

        });
    };
    $scope.test = function () {
        $scope.growltext("login notification test.", false);
    }
    $scope.loginEnterKey = function (event) {
        if (event.keyCode == '13') {
            //$scope.Action = 'Search';
            $scope.logincall();
        }
    }

    $scope.IsSchoolAlreadyRegistered = function () {
        var url = '/api/v1/School/IsSchoolRegistered';
        var responsedata = apiService.masterget(url);
        responsedata.then(function mySucces(response) {
            if (response.data == true) {
                $scope.ShowSchoolRegisteration = false;
            }
        },
            function myError(response) {
                $scope.response = response.data;
            });
    }
    $scope.ShowSchoolRegisteration = true;
    ///SuperAdmin
    $scope.Register = {
        Username: '',
        Email: '',
        FirstName: '',
        LastName: '',
        Password: '',
        ConfirmPassword: ''
    };
    $scope.response = '';
    $scope.registercall = function () {
        var data = $scope.Register;
        var responsedata = apiService.register('/api/Account/Register', data);
        responsedata.then(function mySucces(response) {
            $scope.response = response.data;
        }, function myError(response) {
            $scope.response = response.data;
        });
    };
    $scope.IsSchoolAlreadyRegistered();
    $scope.registercall();
}]);

