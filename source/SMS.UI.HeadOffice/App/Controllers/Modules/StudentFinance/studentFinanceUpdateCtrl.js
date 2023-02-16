
SMSHO.controller('studentFinanceUpdateCtrl', ['$scope', 'apiService', 'financeService', '$cookies', function ($scope, apiService, financeService, $cookies) {
    'use strict';
    $scope.financeRecord = financeService.financeRecord;

    $scope.Save = function () {
        var data = $scope.financeRecord;
        var formData = new FormData();
        formData.append('studentFinanceModel', JSON.stringify(data));
        var responsedata = apiService.masterput('/api/v1/StudentFinance/Update', formData);
        responsedata.then(function mySucces(response) {
                $scope.growltext("Record saved successfully.", false);
                window.location = "#!/studentFinanceCreate";
            },
            function myError(response) {
                $scope.growltext("Record saving failed.", true);
                $scope.response = response.data;
            });
    };
    $scope.Cancel = function () {
        window.location = "#!/studentFinanceCreate";
    };
}]);

