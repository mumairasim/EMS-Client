SMSHO.controller('timeTableViewCtrl', ['$scope', 'apiService', '$cookies', '$filter', '$routeParams', function ($scope, apiService, $cookies, $filter, $routeParams) {
    'use strict';

    $scope.FetchTimeTable = function () {
        var id = $routeParams.Id;
        var url = '/api/v1/TimeTable/View?id=' + id;
        var responsedata = apiService.masterget(url);
        responsedata.then(function mySucces(response) {
            debugger;
            $scope.TimeTable = response.data;
        },
            function myError(response) {
                $scope.response = response.data;
            });
    };
    $scope.Cancel = function () {
        window.location = "#!/timeTableBase";
    };
    $scope.FetchTimeTable();
}]);