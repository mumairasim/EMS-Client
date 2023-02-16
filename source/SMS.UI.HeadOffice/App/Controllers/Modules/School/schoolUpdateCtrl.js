SMSHO.controller('schoolUpdateCtrl', ['$scope', 'apiService', '$cookies', '$routeParams', function ($scope, apiService, $cookies, $routeParams) {
    'use strict';
    $scope.School = {
        Name: '',
        Location: ''
    };

    $scope.SchoolUpdate = function () {
        var data = $scope.SchoolModel;
        var formData = new FormData();
        formData.append('schoolModel', JSON.stringify(data));
        var responsedata = apiService.masterput('/api/v1/School/Update', formData);
        responsedata.then(function mySucces(response) {
            $scope.response = response.data;
            $scope.growltext("School updated successfully.", false);
            window.location = "#!/schoolBase";
        },
            function myError(response) {
                $scope.response = response.data;
                $scope.growltext("School updation failed", true);
            });
    };
    $scope.FetchSchool = function () {
        var id = $routeParams.Id;
        var url = '/api/v1/School/Get?id=' + id;
        var responsedata = apiService.masterget(url);
        responsedata.then(function mySucces(response) {
            $scope.SchoolModel = response.data;
        },
            function myError(response) {
                $scope.response = response.data;
            });
    };
    $scope.Cancel = function () {
        window.location = "#!/schoolBase";
    };

    $scope.FetchSchool();
}]);