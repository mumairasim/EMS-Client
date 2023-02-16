SMSHO.controller('courseUpdateCtrl', ['$scope', 'apiService', '$cookies', '$routeParams', function ($scope, apiService, $cookies, $routeParams) {
    'use strict';
    $scope.CourseModel = {
        Id: '',
        CourseModel: '',
        CourseName: ''
    };


    $scope.CourseUpdate = function () {
        var data = $scope.CourseModel;
        var formData = new FormData();
        formData.append('courseModel', JSON.stringify(data));
        var responsedata = apiService.masterput('/api/v1/Course/Update', formData);
        responsedata.then(function mySucces(response) {
            $scope.response = response.data;
            $scope.growltext("Course updated successfully.", false);
            window.location = "#!/courseBase";
        },
            function myError(response) {
                $scope.response = response.data;
                $scope.growltext("Course updation failed", true);
            });
    };
    $scope.FetchCourse = function () {
        var id = $routeParams.Id;
        var url = '/api/v1/Course/Get?id=' + id;
        var responsedata = apiService.masterget(url);
        responsedata.then(function mySucces(response) {
            $scope.CourseModel = response.data;
        },
            function myError(response) {
                $scope.response = response.data;
            });
    };



    $scope.Cancel = function () {
        window.location = "#!/courseBase";
    };
    $scope.FetchCourse();

    $scope.GetSchools = function () {
        var responsedata = apiService.masterget('/api/v1/School/Get');
        responsedata.then(function mySucces(response) {
            $scope.Schools = response.data.Schools;
            $scope.CourseModel.School = $scope.Schools[0];
        },
            function myError(response) {
                $scope.response = response.data;
            });
    };
    $scope.GetSchools()
}]);