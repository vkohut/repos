var app = angular.module('myApp', []);
app.controller('browseController', function ($scope, $http) {

    $scope.dirData = [];
    $scope.dirPath = "";
    $scope.smallFileCount = 0; /* less 10mb */
    $scope.mediumFileCount = 0; /* 10mb - 50mb */
    $scope.bigFileCount = 0; /* More 100mb */

    $scope.getDrivesList = function () {
        $scope.nullFileCount();
        $http({
            method: 'GET',
            url: "/api/Home/getDrivers",
            cache: false
        }).success(function (response) {
            $scope.dirPath = response.Dir.path;
            $scope.dirData = response.Dir.data;
        })
    };
    $scope.open = function (dir) {
        if (dir.IsFolder == 0) {
            return;
        }
        var path = (dir.FullName == "...") ? dir.Name : dir.FullName;

        $scope.getFolderTree(path);
    };
    $scope.back = function (dirPath) {
        if (dirPath == "...") {
            return;
        }
        if (!dirPath[dirPath.indexOf("\\") + 1]) {
            $scope.getDrivesList();
            return;
        }
        dirPath = dirPath.substring(0, dirPath.lastIndexOf("\\"));
        if (!dirPath[dirPath.indexOf(":") + 1]) {
            dirPath = dirPath + "\\";
        }

        $scope.getFolderTree(dirPath);
    }
    $scope.getFolderTree = function (path) {
        var req = {
            method: 'POST',
            url: '/api/Home/getFolderTree',
            headers: {
                'Content-Type': 'application / json'
            },
            data: { FullName: path }
        }
        $http(req).success(function (response) {
            if (response.Dir) {
                $scope.dirPath = response.Dir.path;
                $scope.dirData = response.Dir.data;

                $scope.getFileSize(response.Dir.data);
            }
            else {
                alert("Access denied!");
            }
        })
    }

    $scope.getFileSize = function (data) {
        $scope.nullFileCount();
        data.forEach(function (item) {
            if (!item.IsFolder) {
                if (item.Size < 10 * 1024 * 1024) {
                    $scope.smallFileCount++;
                }
                else if (item.Size >= 10 * 1024 * 1024 && item.Size <= 50 * 1024 * 1024) {
                    $scope.mediumFileCount++;
                }
                else if (item.Size > 100 * 1024 * 1024) {
                    $scope.bigFileCount++;
                }

            }
        });
    }
    $scope.nullFileCount = function () {
        $scope.smallFileCount = 0;
        $scope.mediumFileCount = 0;
        $scope.bigFileCount = 0;
    }

    $scope.getDrivesList();
});