SMSHO.controller('employeeUpdateCtrl', ['$scope', 'apiService', '$cookies', '$routeParams', function ($scope, apiService, $cookies, $routeParams) {
    'use strict';
    $scope.EmployeeModel = {
        Person: $scope.Person,
        Designation: $scope.Designation,
        School: $scope.School
    };
    $scope.Person = {
        FirstName: '',
        LastName: '',
        ParentName: '',
        Age: '',
        DOB: '',
        Cnic: '',
        ParentCnic: '',
        ParentCity: '',
        ParentEmail: '',
        ParentRelation: '',
        ParentOccupation: '',
        ParentHighestEducation: '',
        ParentNationality: '',
        ParentOfficeAddress: '',
        ParentMobile1: '',
        ParentMobile2: '',
        ParentEmergencyName: '',
        ParentEmergencyRelation: '',
        ParentEmergencyMobile: '',
        Nationality: '',
        Religion: '',
        PresentAddress: '',
        PermanentAddress: '',
        Phone: ''
    };
    $scope.Designation = {
        Name: ''
    };
    $scope.School = {
        Id: '',
        Name: '',
        Location: ''
    };

    $scope.GetDesignations = function () {
        var responsedata = apiService.masterget('/api/v1/Designation/Get');
        responsedata.then(function mySucces(response) {
            $scope.Designations = response.data;
            $scope.FetchEmployee();
        },
            function myError(response) {
                $scope.response = response.data;
            });
    };
    $scope.GetSchools = function () {
        var responsedata = apiService.masterget('/api/v1/School/Get');
        responsedata.then(function mySucces(response) {
            $scope.Schools = response.data.Schools;
            $scope.GetDesignations();
        },
            function myError(response) {
                $scope.response = response.data;
            });
    };

    $scope.EmployeeUpdate = function () {
        if ($scope.IsValid()) {
            var data = $scope.EmployeeModel;
            var formData = new FormData();
            formData.append('employeeModel', JSON.stringify(data));
            var responsedata = apiService.masterput('/api/v1/Employee/Update', formData);
            responsedata.then(function mySucces(response) {
                $scope.response = response.data;
                $scope.growltext("Employee updated successfully.", false);
                window.location = "#!/employeeBase";
            },
                function myError(response) {
                    $scope.response = response.data;
                    $scope.growltext("Employee updation failed", true);
                });
        }
    };
    $scope.AssignGender = function (gender) {
        $scope.StudentModel.Person.Gender = gender;
    };
    $scope.FetchEmployee = function () {
        var id = $routeParams.Id;
        var url = '/api/v1/Employee/Get?id=' + id;
        var responsedata = apiService.masterget(url);
        responsedata.then(function mySucces(response) {
            $scope.EmployeeModel = response.data;
            if ($scope.EmployeeModel.Person.Gender == 1)
                $('#male')[0].checked = true;
            else if ($scope.EmployeeModel.Person.Gender == 2)
                $('#female')[0].checked = true;
            else if ($scope.EmployeeModel.Person.Gender == 0)
                $('#other')[0].checked = true;
            $scope.EmployeeModel.Person.DOB = new Date(response.data.Person.DOB);
        },
            function myError(response) {
                $scope.response = response.data;
            });
    };

    $scope.Cancel = function () {
        window.location = "#!/employeeBase";
    };

    $scope.IsValid = function () {
        if ($scope.EmployeeModel.Person == undefined) {
            $scope.growltext("Invalid student data", true);
            return false;
        }
        if ($scope.EmployeeModel.Person.FirstName == null || $scope.EmployeeModel.Person.FirstName == "" || $scope.EmployeeModel.Person.FirstName.length > 100) {
            $scope.IsError = "Name cannot be null";
            $scope.growltext("FirstName cannot be null", true);
            return false;
        }
        if ($scope.EmployeeModel.Person.LastName == null || $scope.EmployeeModel.Person.LastName == "" || $scope.EmployeeModel.Person.LastName.length > 100) {
            $scope.IsError = "Name cannot be null";
            $scope.growltext("LastName cannot be null", true);
            return false;
        }
        if ($scope.EmployeeModel.Person.Cnic == null || $scope.EmployeeModel.Person.Cnic.length != 13) {
            $scope.IsError = "Cnic must be of 13 digits";
            $scope.growltext("Cnic must be of 13 digits", true);
            return false;
        }
        if ($scope.EmployeeModel.Person.Phone == null || $scope.EmployeeModel.Person.Phone == "" || $scope.EmployeeModel.Person.Phone.length > 15) {
            $scope.IsError = "Phone cannot exceed from 15 digits";
            $scope.growltext("Phone cannot be null or cannot exceed from 15 digits", true);
            return false;
        }
        if ($scope.EmployeeModel.Person.Nationality == null || $scope.EmployeeModel.Person.Nationality == "") {
            $scope.IsError = "Nationality cannot be null";
            $scope.growltext("Nationality cannot be null", true);
            return false;
        }
        if ($scope.EmployeeModel.School == null) {
            $scope.IsError = "School cannot be null";
            $scope.growltext("School cannot be null", true);
            return false;
        }
        if ($scope.EmployeeModel.Designation == null) {
            $scope.IsError = "Designation cannot be null";
            $scope.growltext("Designation cannot be null", true);
            return false;
        }
        if ($scope.EmployeeModel.Person.ParentName == null || $scope.EmployeeModel.Person.ParentName == "" || $scope.EmployeeModel.Person.ParentName.length > 100) {
            $scope.IsError = "Name cannot be null";
            $scope.growltext("ParentName cannot be null", true);
            return false;
        }
        if ($scope.EmployeeModel.Person.ParentRelation == null || $scope.EmployeeModel.Person.ParentRelation == "") {
            $scope.IsError = "Relation cannot be null";
            $scope.growltext("ParentRelation cannot be null", true);
            return false;
        }
        if ($scope.EmployeeModel.Person.ParentMobile1 == null || $scope.EmployeeModel.Person.ParentMobile1 == "" || $scope.EmployeeModel.Person.ParentMobile1.length > 15) {
            $scope.IsError = "Number cannot be null";
            $scope.growltext("ParentMobile1 cannot be null", true);
            return false;
        }
        if ($scope.EmployeeModel.Person.ParentEmergencyName == null || $scope.EmployeeModel.Person.ParentEmergencyName == "" || $scope.EmployeeModel.Person.ParentEmergencyName.length > 100) {
            $scope.IsError = "Name cannot be null";
            $scope.growltext("ParnetName cannot be null", true);
            return false;
        }
        if ($scope.EmployeeModel.Person.ParentEmergencyRelation == null || $scope.EmployeeModel.Person.ParentEmergencyRelation == "") {
            $scope.IsError = "Relation cannot be null";
            $scope.growltext("ParentRelation cannot be null", true);
            return false;
        }
        if ($scope.EmployeeModel.Person.ParentEmergencyMobile == null || $scope.EmployeeModel.Person.ParentEmergencyMobile == "" || $scope.EmployeeModel.Person.ParentEmergencyMobile.length > 15) {
            $scope.IsError = "Number cannot be null";
            $scope.growltext("parentEmergencyNumber cannot be null", true);
            return false;
        }
        return true;
    }

    $scope.GetSchools();
}]);