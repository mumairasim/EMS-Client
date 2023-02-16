SMSHO.controller('lessonPlanUpdateCtrl', ['$scope', 'apiService', '$cookies', '$routeParams', function ($scope, apiService, $cookies, $routeParams) {
    'use strict';
    $scope.LessonPlanModel = {
        Name: '',
        Text: '',
        FromDate: '',
        ToDate: '',
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
            $scope.Schools = response.data.Schools;
            $scope.FetchLessonPlan();
        },
            function myError(response) {
                $scope.response = response.data;
            });
    };
    $scope.LessonPlanUpdate = function () {
        if ($scope.IsValid()) {
            var data = $scope.LessonPlanModel;
            var formData = new FormData();
            formData.append('lessonPlanModel', JSON.stringify(data));
            var responsedata = apiService.masterput('/api/v1/LessonPlan/Update', formData);
            responsedata.then(function mySucces(response) {
                $scope.response = response.data;
                $scope.growltext("Lesson Plan updated successfully.", false);
                window.location = "#!/lessonPlanBase";
            },
                function myError(response) {
                    $scope.response = response.data;
                    $scope.growltext("Lesson Plan updation failed", true);
                });
        }
    };
    $scope.FetchLessonPlan = function () {
        var id = $routeParams.Id;
        var url = '/api/v1/LessonPlan/Get?id=' + id;
        var responsedata = apiService.masterget(url);
        responsedata.then(function mySucces(response) {
            $scope.LessonPlanModel = response.data;
            $scope.LessonPlanModel.FromDate = new Date(response.data.FromDate);
            $scope.LessonPlanModel.ToDate = new Date(response.data.ToDate);

        },
            function myError(response) {
                $scope.response = response.data;
            });
    };
    $scope.Cancel = function () {
        window.location = "#!/lessonPlanBase";
    };

    $scope.IsValid = function () {
        if ($scope.LessonPlanModel == undefined) {
            $scope.growltext("Invalid lessonPlan data", true);
            return false;
        }
        if ($scope.LessonPlanModel.Name == null || $scope.LessonPlanModel.Name == "" || $scope.LessonPlanModel.Name.length > 100) {
            $scope.IsError = "Name cannot be null";
            $scope.growltext("Name cannot be null", true);
            return false;
        }
        if ($scope.LessonPlanModel.Text == null || $scope.LessonPlanModel.Text == "") {
            $scope.IsError = "Text field cannot be null";
            $scope.growltext("Text field cannot be null", true);
            return false;
        }
        if ($scope.LessonPlanModel.FromDate == null) {
            $scope.IsError = "Date cannot be null";
            $scope.growltext("Date cannot be null", true);
            return false;
        }
        if ($scope.LessonPlanModel.ToDate == null) {
            $scope.IsError = "Date cannot be null";
            $scope.growltext("Date cannot be null", true);
            return false;
        }
        if ($scope.LessonPlanModel.School == null) {
            $scope.IsError = "School cannot be null";
            $scope.growltext("School cannot be null", true);
            return false;
        }
        return true;
    }

    $scope.GetSchools();
}]);