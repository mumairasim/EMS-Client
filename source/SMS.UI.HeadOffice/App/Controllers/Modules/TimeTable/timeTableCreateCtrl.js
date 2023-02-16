SMSHO.controller('timeTableCreateCtrl', ['$scope', 'apiService', '$cookies', '$filter', function ($scope, apiService, $cookies, $filter) {
    'use strict';
    $scope.TimeTable = {
        TimeTableDetails: [{
            Day: "",
            Periods: [{
                Course: "",
                Employee: "",
                StartTime: "",
                EndTime: ""
            }]
        }]
    };
    $scope.Period = {
        Course: "",
        Employee: "",
        StartTime: "",
        EndTime: ""

    };
    $scope.Initializer = function () {
        $scope.monday = true;
        $scope.tuesday = true;
        $scope.wednesday = true;
        $scope.thursday = true;
        $scope.friday = true;
        $scope.saturday = false;
        $scope.sunday = false;
        $scope.RemoveEmptySpace();
        $scope.PopulateRows();

    };
    $scope.GetClasses = function () {
        var responsedata = apiService.masterget('/api/v1/Class/GetBySchool?schoolId=' + $scope.TimeTable.School.Id);
        responsedata.then(function mySucces(response) {
            $scope.Classes = response.data;
            $scope.TimeTable.Class = $scope.Classes[0];
            $scope.GetCourses();
        },
            function myError(response) {
                $scope.response = response.data;
            });
    };
    $scope.GetSchools = function () {
        var responsedata = apiService.masterget('/api/v1/School/Get');
        responsedata.then(function mySucces(response) {
            $scope.Schools = response.data.Schools;
            $scope.TimeTable.School = $scope.Schools[0];
            $scope.GetClasses();
        },
            function myError(response) {
                $scope.response = response.data;
            });
    };
    $scope.GetCourses = function () {
        var responsedata = apiService.masterget('/api/v1/Course/GetAllBySchool?schoolId=' + $scope.TimeTable.School.Id);
        responsedata.then(function mySucces(response) {
            $scope.Courses = response.data;
            $scope.Course = $scope.Courses[0];
            $scope.GetEmployeeByDesignation();
        },
            function myError(response) {
                $scope.response = response.data;
            });
    };
    $scope.GetEmployeeByDesignation = function () {
        var responsedata = apiService.masterget('/api/v1/Employee/GetDesignationTeacher');
        responsedata.then(function mySucces(response) {
            $scope.Employees = response.data;
            $scope.Employee = $scope.Employees[0];
            $scope.Initializer();
        },
            function myError(response) {
                $scope.response = response.data;
            });
    };
    $scope.TimeTableCreate = function () {
        var data = $scope.TimeTable;
        var formData = new FormData();
        formData.append('timeTableModel', JSON.stringify(data));
        var responsedata = apiService.post('/api/v1/TimeTable/Create', formData);
        responsedata.then(function mySucces(response) {
            $scope.response = response.data;
            $scope.growltext("TimeTable created successfully.", false);
            //window.location = "#!/lessonPlanBase";
        },
            function myError(response) {
                $scope.response = response.data;
                $scope.growltext("Lesson Plan creation failed", true);
            });
    };
    $scope.AddPeriod = function () {
        for (var i = 0; i < $scope.TimeTable.TimeTableDetails.length; i++) {
            if (($scope.TimeTable.TimeTableDetails[i] !== null) && ($scope.TimeTable.TimeTableDetails[i] !== undefined)) {
                if ($scope.TimeTable.TimeTableDetails[i].Day == $scope.SelectedDay.Day) {
                    if ($scope.TimeTable.TimeTableDetails[i].Periods == undefined) {
                        $scope.TimeTable.TimeTableDetails[i].Periods = [];
                    }

                    $scope.TimeTable.TimeTableDetails[i].Periods.push({
                        Course: $scope.Course,
                        Employee: $scope.Employee,
                        StartTime: $filter('date')($scope.StartTime, 'HH:mm'),
                        EndTime: $filter('date')($scope.EndTime, 'HH:mm')
                    });
                }
            }
        }
    };
    $scope.PopulateRows = function () {

        if ($scope.monday) {
            if (!$scope.IsAlreadyExist("Monday")) {
                $scope.TimeTable.TimeTableDetails.push(
                    {
                        Day: "Monday"
                    });
            }
        } else {
            $scope.remove("Monday");
        }
        if ($scope.tuesday) {
            if (!$scope.IsAlreadyExist("Tuesday")) {
                $scope.TimeTable.TimeTableDetails.push(
                    {
                        Day: "Tuesday"
                    });
            }
        } else {
            $scope.remove("Tuesday");
        }
        if ($scope.wednesday) {
            if (!$scope.IsAlreadyExist("Wednesday")) {
                $scope.TimeTable.TimeTableDetails.push(
                    {
                        Day: "Wednesday"
                    });
            }
        } else {
            $scope.remove("Wednesday");
        }
        if ($scope.thursday) {
            if (!$scope.IsAlreadyExist("Thursday")) {
                $scope.TimeTable.TimeTableDetails.push(
                    {
                        Day: "Thursday"
                    });
            }
        } else {
            $scope.remove("Thursday");
        }
        if ($scope.friday) {
            if (!$scope.IsAlreadyExist("Friday")) {
                $scope.TimeTable.TimeTableDetails.push(
                    {
                        Day: "Friday"
                    });
            }
        } else {
            $scope.remove("Friday");
        }
        if ($scope.saturday) {
            if (!$scope.IsAlreadyExist("Saturday")) {
                $scope.TimeTable.TimeTableDetails.push(
                    {
                        Day: "Saturday"
                    });
            }
        } else {
            $scope.remove("Saturday");
        }
        if ($scope.sunday) {
            if (!$scope.IsAlreadyExist("Sunday")) {
                $scope.TimeTable.TimeTableDetails.push(
                    {
                        Day: "Sunday"
                    });
            }
        } else {
            $scope.remove("Sunday");
        }
        $scope.RemoveEmptySpace();
        $scope.SelectedDay = $scope.TimeTable.TimeTableDetails[0];
    };
    $scope.remove = function (item) {
        for (var i = 0; i < $scope.TimeTable.TimeTableDetails.length; i++) {
            if (($scope.TimeTable.TimeTableDetails[i] !== null) && ($scope.TimeTable.TimeTableDetails[i] !== undefined)) {
                if ($scope.TimeTable.TimeTableDetails[i].Day == item) {
                    $scope.TimeTable.TimeTableDetails.splice(i, 1);
                }
            }
        }
    };
    $scope.IsAlreadyExist = function (item) {
        for (var i = 0; i < $scope.TimeTable.TimeTableDetails.length; i++) {
            if (($scope.TimeTable.TimeTableDetails[i] !== null) && ($scope.TimeTable.TimeTableDetails[i] !== undefined)) {
                if ($scope.TimeTable.TimeTableDetails[i].Day == item) {
                    return true;
                }
            }
        }
        return false;
    };
    $scope.RemoveEmptySpace = function () {
        for (var i = 0; i < $scope.TimeTable.TimeTableDetails.length; i++) {
            if ($scope.TimeTable.TimeTableDetails[i].Day === '') {
                $scope.TimeTable.TimeTableDetails.splice(i, 1);
                break;
            }
        }
    };
    $scope.GuidGenerator = function uuidv4() {
        return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
            var r = Math.random() * 16 | 0, v = c == 'x' ? r : (r & 0x3 | 0x8);
            return v.toString(16);
        });
    }
    $scope.GetSchools();
}]);