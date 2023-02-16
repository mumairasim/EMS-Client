
SMSHO.controller('studentAttendanceSheetCtrl', ['$scope', 'apiService', '$cookies', function ($scope, apiService, $cookies) {
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
    //$scope.StudentAttendanceDetailModel = {
    //    AttendanceStatusId: '',
    //    StudentId: ''
    //};
    $scope.showfailureInfo = false;
    $scope.showSuccessInfo = false;
    $scope.GetStudents = function () {
        var responsedata = apiService.masterget('/api/v1/Student/Get?classId=' + $scope.Class.Id + '&schoolId=' + $scope.School.Id);
        responsedata.then(function mySucces(response) {
            $scope.studentList = response.data.Students;
            $scope.TotalStudents = response.data.StudentsCount;

            $scope.StudentAttendanceModel.SchoolId = $scope.School.Id;
            $scope.StudentAttendanceModel.ClassId = $scope.Class.Id;
            for (var i = 0; i < response.data.Students.length; i++) {
                var studentAttendanceDetailModel = {
                    AttendanceStatusId: '',
                    StudentId: ''
                };
                for (var j = 0; j < $scope.AttendanceStatusList.length; j++) {
                    if ($scope.AttendanceStatusList[j].Status == 'Present') {
                        studentAttendanceDetailModel.AttendanceStatusId = $scope.AttendanceStatusList[j].Id;
                        break;
                    }
                }
                studentAttendanceDetailModel.StudentId = response.data.Students[i].Id;
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
            $scope.GetStudents();
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
            $scope.Schools = response.data.Schools;
            $scope.School = $scope.Schools[0];
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
                    }
                }
            }
        }
    };
    $scope.SubmitAttendance = function () {
        var data = $scope.StudentAttendanceModel;
        var formData = new FormData();
        formData.append('studentAttendanceModel', JSON.stringify(data));
        var responsedata = apiService.post('api/v1/StudentAttendance/Create', formData);
        responsedata.then(function mySucces(response) {
            $scope.attendanceCreationResponse = response.data;
            if ($scope.attendanceCreationResponse.StatusCode == '200') {
                //$scope.growltext($scope.attendanceCreationResponse.Description, false);
                $scope.showSuccessInfo = true;
                $scope.showfailureInfo = false;
                $scope.growltext("Attendance marked successfully.", false);
                window.location = "#!/studentAttendanceSheetBase";
            }
            if ($scope.attendanceCreationResponse.StatusCode == '400') {
                $scope.showSuccessInfo = false;
                $scope.showfailureInfo = true;
                //$scope.growltext($scope.attendanceCreationResponse.Description, true);
            }
        },
            function myError(response) {
                $scope.response = response.data;
                $scope.growltext("Attendance marking failed.", true);
            });
    };
    $scope.GetSchools();
}]);

