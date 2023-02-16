
SMSHO.controller('UserProfileCtrl', ['$scope', 'apiService', '$cookies', '$routeParams', function ($scope, apiService, $cookies, $routeParams) {
    'use strict';
    $scope.UserModel = {
        UserName: '',
        Email: ''
    };
    $scope.ImageBase = '';

    $scope.IsFormSubimtted = false;
    $scope.IsFileValid = false;
    $scope.IsFormValid = false;
    $scope.Message = "";
    $scope.FileDescription = "";
    $scope.selectedFileForUpload = null;
    $scope.imageLoaded = false;
    $scope.$watch("userForm.$valid", function (isValid) {
        $scope.isFormValid = isValid;
    });

    $scope.CheckIsFileValid = function (file) {
        if ($scope.SelectedFileForUpload != null) {
            if ((file.type == 'image/png' || file.type == 'image/jpeg' || file.type == 'image/gif') &&
                file.size <= (512 * 1024)) {
                $scope.IsFileValid = true;
            } else {
                $scope.IsFileValid = false;
            }
        }
    };

    $scope.SelectFileForUpload = function (file) {
        $scope.SelectedFileForUpload = file[0];

        //preview the uploaded image
        var reader = new FileReader();

        reader.onload = function (event) {
            $scope.UserModel.Image = event.target.result;
            $scope.ImageBase = '';
            $scope.$apply();

        };
        // when the file is read it triggers the onload event above.
        reader.readAsDataURL(file[0]);
    };


    $scope.UserUpdate = function () {
        var data = $scope.UserModel;

        $scope.UserModel.Image = '';
        var formData = new FormData();
        formData.append('userModel', JSON.stringify(data));
        formData.append('file', $scope.SelectedFileForUpload);

        var responsedata = apiService.post('api/Account/UpdateUserInfo', formData);
        responsedata.then(function mySucces(response) {
            $scope.response = response.data;
            $scope.growltext("User updated successfully.", false);
            $scope.GetUser();
        },
            function myError(response) {
                $scope.response = response.data;
                $scope.growltext("User updation failed", true);
            });
    };


    $scope.GetUser = function () {
        var id = $routeParams.Id;
        var url = '/api/Account/GetUserDetailedInfo';
        var responsedata = apiService.masterget(url);
        responsedata.then(function mySucces(response) {
            $scope.UserModel = response.data;
            $scope.UserModel.CreationDate = new Date(response.data.CreationDate);

            if ($scope.UserModel.ImageExtension !== "") {

                $scope.ImageBase = 'data:image/' + $scope.UserModel.ImageExtension + ';base64,';
                localStorage.setItem('SMS_UserImage', $scope.ImageBase + $scope.UserModel.Image);
                $scope.UserImage = $scope.ImageBase + $scope.UserModel.Image;
                $scope.imageLoaded = true;
                $scope.$apply();
            }
        },
            function myError(response) {
                $scope.response = response.data;
            });
    };
    $scope.Cancel = function () {
        window.location = "#!/";
    };

    $scope.GetUser();
}]);
