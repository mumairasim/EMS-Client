SMSHO.controller('schoolRegisterCtrl', ['$scope', 'apiService', '$cookies', function ($scope, apiService, $cookies) {
    'use strict';

    $scope.Setisloggedinfalse();
    $scope.School = {
        Id: '',
        Name: ''

    };
    $scope.SchoolRegister = function () {
        var data = $scope.SchoolModel;
        var formData = new FormData();
        formData.append('schoolModel', JSON.stringify(data));
        var responsedata = apiService.post('/api/v1/School/Register', formData);
        responsedata.then(function mySucces(response) {
            $scope.response = response.data;
            $scope.growltext("School regisetered successfully.", false);
            window.location = "#!/register";
        },
            function myError(response) {
                $scope.response = response.data;
                $scope.growltext(response.data.Message, true);
            });
    };
    $scope.Cancel = function () {
        window.location = "#!/";
    };
}]);