(function () {
    var app = angular.module("Main", []);

    var enrollStudentController = app.controller("enrollStudentCtrl", function ($scope, $http, $window, $timeout) {
        $scope.studentFound = false;
        $scope.studentNotFound = false;
        $scope.enrollSuccessful = false;
        $scope.searchDetails = {
            resultFormat: "json",
            email: ""
        };
        $scope.student = {
            name: '',
            email: '',
            id: ''
        };


        var searchStudentByEmail = function () {
            //Load student
            
            $http({
                method: 'GET',
                url: '/Search/SearchUserByEmail',
                params: $scope.searchDetails
            }).then(function successCallback(response) {
                if(response.data.error !== undefined)
                {
                    $scope.studentNotFound = true;
                    return;
                }
                $scope.student.name = response.data.Name;
                $scope.student.email = response.data.Email;
                $scope.student.id = response.data.ID;
                $scope.studentFound = true;

                console.log(response.data);
            }, function errorCallback(response) {
                $scope.studentNotFound = true;
                console.log(response);
            });

        };

        var enrollStudent = function (courseID) {
            $http({
                method: 'GET',
                url: '/Course/EnrollStudent',
                params: {
                    studentID: $scope.student.id,
                    courseID: courseID,
                    resultFormat: 'json'
                }
            }).then(function successCallback(response) {
                $scope.studentFound = false;
                $scope.enrollSuccessful = true;

                //To-Do: Fix this timeout callback
                $timeout($window.location.reload, 3000);

                console.log(response.data);
            }, function errorCallback(response) {
                console.log(response);
            });
        };

        $scope.enrollStudent = enrollStudent;
        $scope.searchStudentByEmail = searchStudentByEmail;
    });
})();