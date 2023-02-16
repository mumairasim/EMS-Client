SMSHO.controller('schoolCreateCtrl', ['$scope', 'apiService', '$cookies', function ($scope, apiService, $cookies) {
    'use strict';
 
    $scope.School = {
        Name: '',
        Location: ''
    };
    $scope.SchoolCreate = function () {
        var data = $scope.SchoolModel;
        var formData = new FormData();
        formData.append('schoolModel', JSON.stringify(data));
        var responsedata = apiService.post('/api/v1/School/Create', formData);
        responsedata.then(function mySucces(response) {
            $scope.response = response.data;
            $scope.growltext("School created successfully.", false);
            window.location = "#!/schoolBase";
        },
            function myError(response) {
                $scope.response = response.data;
                $scope.growltext("Class creation failed", true);
            });
    };
    $scope.Cancel = function () {
        window.location = "#!/schoolBase";
    };
    
}]);