
SMSHO.factory('apiService', ['$http', '$cookies', function ($http, $cookies) {
    "use strict";

    var baseUrl = 'http://localhost:44358/';

    function login(url, datatosend) {
        return $http({
            method: "POST",
            url: baseUrl + url,
            data: datatosend,
            headers: { 'Content-Type': "application/x-www-form-urlencoded" }
        });
    }
    function logout(url) {

        return $http({
            method: "POST",
            url: baseUrl + url,
            headers: {
                'Content-Type': "application/json; charset=utf-8",
                'Authorization': "Bearer " + $cookies.get('SMS_token'),
                'UserId': $cookies.get('SMS_userId'),
                'UserName': $cookies.get('SMS_user')
            }
        });
    }

    function register(url, datatosend) {
        return $http({
            method: "POST",
            url: baseUrl + url,
            data: JSON.stringify(datatosend),
            headers: {
                'Content-Type': "application/json; charset=utf-8",
                'Authorization': "Bearer " + $cookies.get('SMS_token')
            }
        });
    }
    function masterpost(url, datatosend) {
        return $http({
            method: "POST",
            url: baseUrl + url,
            data: JSON.stringify(datatosend),
            headers: {
                'Content-Type': "application/json; charset=utf-8",
                'Authorization': "Bearer " + $cookies.get('SMS_token'),
                'UserId': $cookies.get('SMS_userId'),
                'UserName': $cookies.get('SMS_user')
            }
        });
    }
    function post(url, datatosend) {
        return $http({
            method: "POST",
            url: baseUrl + url,
            data: datatosend,
            headers: {
                'Content-Type': undefined,
                'Authorization': "Bearer " + $cookies.get('SMS_token'),
                'UserId': $cookies.get('SMS_userId'),
                'UserName': $cookies.get('SMS_user')
            }
        });
    }
    function masterput(url, datatosend) {
        return $http({
            method: "PUT",
            url: baseUrl + url,
            data: datatosend,
            headers: {
                'Content-Type': undefined,
                'Authorization': "Bearer " + $cookies.get('SMS_token'),
                'UserId': $cookies.get('SMS_userId'),
                'UserName': $cookies.get('SMS_user')
            }
        });
    }

    function put(url, datatosend) {
        return $http({
            method: "PUT",
            url: baseUrl + url,
            data: datatosend,
            headers: {
                'Content-Type': "application/json; charset=utf-8",
                'Authorization': "Bearer " + $cookies.get('SMS_token'),
                'UserId': $cookies.get('SMS_userId'),
                'UserName': $cookies.get('SMS_user')
            }
        });
    }
    function masterdelete(url) {
        return $http({
            method: "DELETE",
            url: baseUrl + url,
            headers: {
                'Content-Type': "application/json; charset=utf-8",
                'Authorization': "Bearer " + $cookies.get('SMS_token'),
                'UserId': $cookies.get('SMS_userId'),
                'UserName': $cookies.get('SMS_user')
            }
        });
    }
    function masterget(url) {
        return $http({
            method: "GET",
            url: baseUrl + url,
            headers: {
                'Content-Type': "application/json; charset=utf-8",
                'Authorization': "Bearer " + $cookies.get('SMS_token'),
                'UserId': $cookies.get('SMS_userId'),
                'UserName': $cookies.get('SMS_user')
            }
        });
    }

    //auto loader



    return {
        login: login,
        logout: logout,
        register: register,
        masterpost: masterpost,
        masterget: masterget,
        masterput: masterput,
        put: put,
        masterdelete: masterdelete,
        post: post
    };
}]);