SMSHO.controller('classUpdateCtrl', ['$scope', 'apiService', '$cookies', '$routeParams', function ($scope, apiService, $cookies, $routeParams) {
    'use strict';
    $scope.ClassModel = {
        Name: '',
        School: $scope.School
    };

    $scope.School = {
        Id: '',
        Name: '',
        Location: ''
    };


    $scope.GetSchools = function () {
        var responsedata = apiService.masterget('/api/v1/School/Get');
        responsedata.then(function mySucces(response) {
            $scope.Schools = response.data;

            $scope.FetchClass();
        },
            function myError(response) {
                $scope.response = response.data;
            });
    };
    $scope.ClassUpdate = function () {
        var data = $scope.ClassModel;
        var formData = new FormData();
        formData.append('classModel', JSON.stringify(data));
        var responsedata = apiService.masterput('/api/v1/Class/Update', formData);
        responsedata.then(function mySucces(response) {
            $scope.response = response.data;
            $scope.growltext("Class updated successfully.", false);
            window.location = "#!/classBase";
        },
            function myError(response) {
                $scope.response = response.data;
                $scope.growltext("Class updation failed", true);
            });
    };
    $scope.FetchClass = function () {
        var id = $routeParams.Id;
        var url = '/api/v1/Class/Get?id=' + id;
        var responsedata = apiService.masterget(url);
        responsedata.then(function mySucces(response) {
            $scope.ClassModel = response.data;
        },
            function myError(response) {
                $scope.response = response.data;
            });
    };
    $scope.Cancel = function () {
        window.location = "#!/classBase";
    }; 
    $scope.GetSchools();
}]);