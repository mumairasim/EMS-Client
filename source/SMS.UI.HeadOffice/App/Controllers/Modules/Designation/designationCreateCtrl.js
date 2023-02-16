SMSHO.controller('designationCreateCtrl', ['$scope', 'apiService', '$cookies', function ($scope, apiService, $cookies) {
    'use strict';
    $scope.DesignationModel = {
        Name: ''
    };


    $scope.DesignationCreate = function () {

        var data = $scope.DesignationModel;
        var formData = new FormData();
        formData.append('designationModel', JSON.stringify(data));
        var responsedata = apiService.post('/api/v1/Designation/Create', formData);
        responsedata.then(function mySucces(response) {
            $scope.response = response.data;
            $scope.growltext("Designation created successfully.", false);
            window.location = "#!/designationBase";
        },
            function myError(response) {
                $scope.response = response.data;
                $scope.growltext("Designation creation failed", true);
            });
    };

}]);