
SMSHO.controller('ChangePasswordCtrl', ['$scope', 'apiService', '$cookies', '$routeParams', function ($scope, apiService, $cookies, $routeParams) {
    'use strict';

    $scope.ChangePasswordModel = {
        OldPassword: '',
        NewPassword: '',
        ConfirmPassword: ''
    };

    $scope.Submit = function () {
        var responsedata = apiService.masterpost('api/Account/ChangePassword', $scope.ChangePasswordModel);
        responsedata.then(function mySucces(response) {
            $scope.response = response.data;
            $scope.growltext("Password changed successfully.", false);
            window.location = "#!/userProfile";
        },
            function myError(response) {
                $scope.response = response.data;
                $scope.growltext("password change failed", true);
            });
    }

    $scope.Cancel = function () {
        window.location = "#!/userProfile";
    }

}]);
