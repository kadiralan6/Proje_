var app = angular.module("Productapp", []);



app.controller("ProductController", function ($scope, $http) {


    // Tüm  Verileri görüntüleme

    $http.get("/Product/GetAllData").then(function (d) {

        $scope.products = d.data;

    });

    // Ekleme
    $scope.saveproduct = function () {

        $('#Modal1').modal('hide');

        $http({

            method: 'POST',

            url: '/Product/AddProduct',

            data: $scope.product

        }).then(function (d) {

            if (d.data == true) {
                $('#BasariliMesaj').show();
                $http.get("/Product/GetAllData").then(function (d) {

                    $scope.products = d.data;

                });
            }
            $scope.product = null;

        });

    };


    // Modal için veriyi getirip gösterme

    $scope.getDataById = function (id) {

        $http.get("/Product/GetDataById?id=" + id).then(function (d) {

            $scope.product = d.data;

        });

    };

    // Veriyi güncelleme

    $scope.updatedata = function () {

        $http({

            method: 'POST',

            url: '/Product/UpdateData',

            data: $scope.product

        }).then(function (d) {
            $('#Modal2').modal('hide');

            if (d.data == true) {
                $http.get("/Product/GetAllData").then(function (d) {

                    $scope.products = d.data;

                });

            }

            $scope.product = null;

        });


    };
    $scope.clear_data = function () {
        $scope.product = null;
    };
    // Veriyi arama
    $scope.search_data = function () {


        $http({

            method: 'POST',

            url: '/Product/SearchData?name=' + $scope.search_name,

        }).then(function (d) {

            $scope.products = d.data;

        });

    };

});