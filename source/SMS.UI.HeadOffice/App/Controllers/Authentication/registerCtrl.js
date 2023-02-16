//(function (app) {
//    'use strict';

SMSHO.controller('registerCtrl', ['$scope', 'apiService', '$cookies', function ($scope, apiService, $cookies) {
    'use strict';
    $scope.Setisloggedinfalse();
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
            $scope.growltext("User created successfully.", false);
        }, function myError(response) {
            $scope.response = response.data;
        });
    };

    $scope.cancel = function () {
        window.location = "#!/";
    };

}]);

