SMSHO.controller('requestManagementCtrl', ['$scope', 'apiService', '$cookies', '$routeParams', function ($scope, apiService, $cookies, $routeParams) {
    'use strict';
    $scope.FetchRequests = function () {
        var url = '/api/v1/RequestManagement/GetAll';
        var responseData = apiService.masterget(url);
        responseData.then(function mySuccess(response) {
            $scope.RequestList = response.data;
        },
            function myError(response) {
                $scope.response = response.data;
            });
    };
    $scope.ApproveRequest = function (request) {
        var responsedata = apiService.masterpost('/api/v1/' + request.RequestFor + '/ApproveRequest', request);
        responsedata.then(function mySucces(response) {
            $scope.response = response.data;
            $scope.growltext("Request approved successfully.", false);
            //window.location = "#!/lessonPlanBase";
        },
            function myError(response) {
                $scope.response = response.data;
                $scope.growltext("Request approval failed", true);
            });
    };
    $scope.FetchRequests();
}]);