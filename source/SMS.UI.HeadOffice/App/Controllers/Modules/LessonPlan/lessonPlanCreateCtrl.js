SMSHO.controller('lessonPlanCreateCtrl', ['$scope', 'apiService', '$cookies', function ($scope, apiService, $cookies) {
    'use strict';
    $scope.LessonPlanModel = {
        Name: '',
        Text: '',
        FromDate: '',
        ToDate: '',
    };

    $scope.GetSchools = function () {
        var responsedata = apiService.masterget('/api/v1/School/Get');
        responsedata.then(function mySucces(response) {
            $scope.Schools = response.data.Schools;
            $scope.LessonPlanModel.School = $scope.Schools[0];
        },
            function myError(response) {
                $scope.response = response.data;
            });
    };
    $scope.LessonPlanCreate = function () {
        if ($scope.IsValid()) {
            var data = $scope.LessonPlanModel;
            var formData = new FormData();
            formData.append('lessonPlanModel', JSON.stringify(data));
            var responsedata = apiService.post('/api/v1/LessonPlan/Create', formData);
            responsedata.then(function mySucces(response) {
                $scope.response = response.data;
                $scope.growltext("Lesson Plan created successfully.", false);
                window.location = "#!/lessonPlanBase";
            },
                function myError(response) {
                    $scope.response = response.data;
                    $scope.growltext("Lesson Plan creation failed", true);
                });
        }
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
            $scope.IsError = "Date  cannot be null";
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

