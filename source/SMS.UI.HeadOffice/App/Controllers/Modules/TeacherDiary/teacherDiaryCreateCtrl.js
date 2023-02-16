SMSHO.controller('teacherDiaryCreateCtrl', ['$scope', 'apiService', '$cookies', function ($scope, apiService, $cookies) {
    'use strict';
    $scope.TeacherDiaryModel = {
        Name: '',
        DairyText: '',
        DairyDate:'',
        School: $scope.School,
        Employee: $scope.Employee
    };
    $scope.Employee = {
        Name: ''
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
            $scope.TeacherDiaryModel.School = $scope.Schools[0];
        },
            function myError(response) {
                $scope.response = response.data;
            });
    };

    $scope.GetEmployeeByDesignation = function () {
        var responsedata = apiService.masterget('/api/v1/Employee/GetDesignationTeacher');
        responsedata.then(function mySucces(response) {
            $scope.Employees = response.data;
            $scope.TeacherDiaryModel.Employee = $scope.Employees[0];
        },
            function myError(response) {
                $scope.response = response.data;
            });
    };

    $scope.TeacherDiaryCreate = function () {
        if ($scope.IsValid()) {
            var data = $scope.TeacherDiaryModel;
            var formData = new FormData();
            formData.append('teacherDiaryModel', JSON.stringify(data));
            var responsedata = apiService.post('/api/v1/TeacherDiary/Create', formData);
            responsedata.then(function mySucces(response) {
                $scope.response = response.data;
                $scope.growltext("Teacher Diary created successfully.", false);
                window.location = "#!/teacherDiaryBase";
            },
                function myError(response) {
                    $scope.response = response.data;
                    $scope.growltext("Teacher Diary creation failed", true);
                });
        }
    };

    $scope.IsValid = function () {
        if ($scope.TeacherDiaryModel == undefined) {
            $scope.growltext("Invalid Teacher Diary data", true);
            return false;
        }
        if ($scope.TeacherDiaryModel.Name == null || $scope.TeacherDiaryModel.Name == "" || $scope.TeacherDiaryModel.Name.length > 100) {
            $scope.IsError = "Name cannot be null";
            $scope.growltext("Name cannot be null", true);
            return false;
        }
        if ($scope.TeacherDiaryModel.DairyText == null || $scope.TeacherDiaryModel.DairyText == "") {
            $scope.IsError = "Text field cannot be null";
            $scope.growltext("Text field cannot be null", true);
            return false;
        }
        if ($scope.TeacherDiaryModel.DairyDate == null) {
            $scope.IsError = "Date  cannot be null";
            $scope.growltext("Date cannot be null", true);
            return false;
        }
        if ($scope.TeacherDiaryModel.Employee == null) {
            $scope.IsError = "Employee cannot be null";
            $scope.growltext("Employee cannot be null", true);
            return false;
        }
        if ($scope.TeacherDiaryModel.School == null) {
            $scope.IsError = "School cannot be null";
            $scope.growltext("School cannot be null", true);
            return false;
        }
        return true;s
    }

    $scope.GetSchools();
    $scope.GetEmployeeByDesignation();
}]);