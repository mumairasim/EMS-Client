SMSHO.controller('studentAttendanceSheetUpdateCtrl', ['$scope', 'apiService', '$cookies', '$routeParams', function ($scope, apiService, $cookies, $routeParams) {
    'use strict';
    $scope.StudentAttendanceModel = {
        AttendanceDate: '',
        Class: $scope.Class,
        School: $scope.School,
        StudentAttendanceDetail: []
    };
    $scope.Class = {
        Id: '',
        ClassName: ''
    };
    $scope.School = {
        Id: '',
        Name: '',
        Location: ''
    };
    $scope.showfailureInfo = false;
    $scope.showSuccessInfo = false;
    $scope.GetAttendance = function () {
        var id = $routeParams.Id;
        var responsedata = apiService.masterget('/api/v1/StudentAttendance/Get?id=' + id);
        responsedata.then(function mySucces(response) {
            $scope.studentAttendanceList = response.data.StudentAttendanceDetail;
            $scope.Class = response.data.Class;
            $scope.School = response.data.School.Schools;
            $scope.StudentAttendanceModel.Id = response.data.Id;
            $scope.StudentAttendanceModel.AttendanceDate = new Date(response.data.AttendanceDate);
            $scope.StudentAttendanceModel.SchoolId = $scope.School.Id;
            $scope.StudentAttendanceModel.ClassId = $scope.Class.Id;
            for (var i = 0; i < $scope.studentAttendanceList.length; i++) {
                var studentAttendanceDetailModel = {
                    Id: '',
                    AttendanceStatusId: '',
                    StudentId: ''
                };
                studentAttendanceDetailModel.Id = response.data.StudentAttendanceDetail[i].Id;
                studentAttendanceDetailModel.AttendanceStatusId = response.data.StudentAttendanceDetail[i].AttendanceStatus.Id;
                studentAttendanceDetailModel.StudentId = response.data.StudentAttendanceDetail[i].StudentId;
                $scope.StudentAttendanceModel.StudentAttendanceDetail.push(studentAttendanceDetailModel);
            }
        },
            function myError(response) {
                $scope.response = response.data;
            });
    };
    $scope.GetAttendanceStatus = function () {
        var responsedata = apiService.masterget('/api/v1/AttendanceStatus/Get');
        responsedata.then(function mySucces(response) {
            $scope.AttendanceStatusList = response.data;
        },
            function myError(response) {
                $scope.response = response.data;
            });
    };
    $scope.GetClasses = function () {
        var responsedata = apiService.masterget('/api/v1/Class/GetBySchool?schoolId=' + $scope.School.Id);
        responsedata.then(function mySucces(response) {
            $scope.Classes = response.data;
            $scope.Class = $scope.Classes[0];
            $scope.GetAttendance();
            $scope.GetAttendanceStatus();
            $scope.StudentAttendanceModel.AttendanceDate = new Date();
        },
            function myError(response) {
                $scope.response = response.data;
            });
    };
    $scope.GetSchools = function () {
        var responsedata = apiService.masterget('/api/v1/School/Get');
        responsedata.then(function mySucces(response) {
            $scope.Schools = response.data;
            $scope.School = $scope.Schools.Schools[0];
            $scope.GetClasses();
        },
            function myError(response) {
                $scope.response = response.data;
            });
    };
    $scope.StatusUpdation = function (studentId, status) {
        for (var i = 0; i < $scope.StudentAttendanceModel.StudentAttendanceDetail.length; i++) {
            if ($scope.StudentAttendanceModel.StudentAttendanceDetail[i].StudentId == studentId) {
                for (var j = 0; j < $scope.AttendanceStatusList.length; j++) {
                    if ($scope.AttendanceStatusList[j].Status == status) {
                        $scope.StudentAttendanceModel.StudentAttendanceDetail[i].AttendanceStatusId = $scope.AttendanceStatusList[j].Id;
                        break;
                    }
                }
            }
        }
    };
    $scope.UpdateAttendance = function () {
        var data = $scope.StudentAttendanceModel;
        var formData = new FormData();
        formData.append('studentAttendanceModel', JSON.stringify(data));
        var responsedata = apiService.masterput('api/v1/StudentAttendance/Update', formData);
        responsedata.then(function mySucces(response) {
            $scope.attendanceCreationResponse = response.data;
            if ($scope.attendanceCreationResponse.StatusCode == '200') {
                //$scope.growltext($scope.attendanceCreationResponse.Description, false);
                $scope.showSuccessInfo = true;
                $scope.showfailureInfo = false;
                $scope.growltext("Attendance updated successfully.", false);
                window.location = "#!/dashboard";
            }
            if ($scope.attendanceCreationResponse.StatusCode == '400') {
                $scope.showSuccessInfo = false;
                $scope.showfailureInfo = true;
                //$scope.growltext($scope.attendanceCreationResponse.Description, true);
            }
        },
            function myError(response) {
                $scope.response = response.data;
                $scope.growltext("Attendance updation failed.", true);
            });
    };
    $scope.GetSchools();
}]);