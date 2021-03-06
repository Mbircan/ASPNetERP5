﻿/// <reference path="angular.js" />

var app = angular.module("northApp", []);
app.controller("SiparisCtrl",
    function ($scope, $http) {       
        $scope.urunler = null;
        init();
        function init() {
            $http({
                url: '../Siparis/Urunler',
                method:'GET'
            }).then(function (response) {
                if (response.data.success)
                    $scope.urunler = response.data.data;
                else
                    alert(response.data.message);
            })
        }
        $scope.sepet = [];
        $scope.toplam = 0;
        $scope.sepeteekle = function (urun) {
            if ($scope.sepet.length === 0) {
                urun.Quantity = 1;
                $scope.sepet.push(urun);
            }
            else {
                var varmi = false;
                for (var i = 0; i < $scope.sepet.length; i++) {
                    if ($scope.sepet[i].ProductID === urun.ProductID) {
                        varmi = true;
                        $scope.sepet[i].Quantity++;
                        break;
                    }
                }
                if (!varmi) {
                    urun.Quantity = 1;
                    $scope.sepet.push(urun);
                }
            }
            sepethesapla();
        }
        function sepethesapla() {
            $scope.toplam = 0;
            for (var i = 0; i < $scope.sepet.length; i++) {
                $scope.toplam += $scope.sepet[i].Quantity * $scope.sepet[i].UnitPrice;

            }
        }
        $scope.sepettemizle=function(){
            $scope.sepet = [];
            $scope.toplam = 0;
        }
        $scope.sepetionayla=function() {
            $http({
                url: '../Siparis/Urunler',
                method:'POST',
                data: $scope.sepet
            }).then(function (response) {
                console.log(response);
                alert(response.data.message)
                if (response.data.success)
                    sepettemizle();
            })
        }
        $scope.git=function(sayfa) {
            $http({
                url: '../Siparis/Urunler?sayfa='+sayfa,
                method: 'GET'
            }).then(function (response) {
                if (response.data.success)
                    $scope.urunler = response.data.data;
                else
                    alert(response.data.message);
            })
        }
});