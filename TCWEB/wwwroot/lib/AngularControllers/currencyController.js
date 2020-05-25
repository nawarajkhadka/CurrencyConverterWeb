
var app = angular.module('c_app', ["chart.js"]);

//factory section

; (function () {
    app.factory('currencyService', ['$http', function ($http) {

        var cServiceFactory = {};


        //currency list ddl
        var _loadCurrencyList = function () {
            var request = $http({
                url: baseUrl + 'api/Currencies/loadCurrencyList',
                method: 'GET'
            });
            return request;
        }

        //get currency rates

        var _getCurrencies = function (c_params) {
            var request = $http({
                url: baseUrl + 'api/Currencies/GetCurrency',
                method: 'GET',
                params: c_params
            });
            return request;
            
        }
        
        
        cServiceFactory.LoadCurrencyList = _loadCurrencyList;

        cServiceFactory.GetCurrencies = _getCurrencies;

        return cServiceFactory;

    }]);

}());



//controller section

; (function () {

    'use strict';

    app.controller('ccontroller', ['$scope', 'currencyService', '$http', '$filter', function ($scope, currencyService, $http, $filter) {

        //loader
        $scope.loading = false;
        $scope.errorresponse = { data:'',statuscode:''}

        //for graph
        $scope.labels = [];
        $scope.series = ['Series A', 'Series B'];
        $scope.data = [];

        //currency related
        $scope.currency = { sourcecurrency: '', destinationcurrency: '', amount: 0, date: new Date(),todate:new Date() };
        $scope.resultcurrency = {};

        //drop down list
        $scope.ddlItems = [];

        //for loading in graph
        $scope.c_data = [];

        //date set for today
        $scope.maxdate = $filter('date')(new Date(), 'yyyy-MM-dd');     

        //call drop down list at first
        LoadDdl();

        function LoadDdl() {
            $scope.loading = true;
            currencyService.LoadCurrencyList().then(function (results) {
                $scope.ddlItems = results.data;
                $scope.loading = false;
                
            }, function (error) {
                    $scope.ddlItems = [];
                    $scope.loading = false;
            })
        }

        //search function for currency exchange rate
        $scope.search = function (currency) {
            $scope.errorresponse.data = '';
            $scope.loading = true;
            if (typeof (currency.date) == undefined || currency.date == null) {
                $scope.currency.date = $filter('date')(new Date(), 'yyyy-MM-dd');
            }
            else {
                const formatteddate = $filter('date')(new Date(currency.date), 'yyyy-MM-dd');
                $scope.currency.date = formatteddate;
            }
            //default parameter for historic is false
            $scope.currency.todate = $filter('date')(new Date(currency.date), 'yyyy-MM-dd');
            $scope.currency.ishistoric = false;

            currencyService.GetCurrencies($scope.currency).then(function (results) {                
                if (results.data.length > 0) {

                    $scope.resultcurrency = {
                        amt: results.data[0].convertedAmount,
                        src: currency.sourcecurrency,
                        dst: currency.destinationcurrency,
                        date: currency.date,
                        srcamt: currency.amount
                    };
                }
                else {
                    $scope.errorresponse.data = "No exchange rates found for current date";
                }
                $scope.loading = false;
                //call load graph after this one executes
                $scope.labels = [];
                $scope.c_data = [];
                $scope.data = [];
                $scope.loadData(currency);
            }, function (error) {
                    $scope.errorresponse.data = error.data;                    
                $scope.loading = false;

            })
        }

        //load data for graph
        $scope.loadData = function (c_param) {
            $scope.loading = true;
            c_param.ishistoric = true;
            
            $scope.c_data = [];
            currencyService.GetCurrencies(c_param).then(function (results) {
               
                $scope.c_data = results.data;
                //loopo through result and load in graph data
                angular.forEach($scope.c_data, function (value, key) {
                    $scope.data.push(value.convertedAmount);
                    $scope.labels.push($filter('date')(new Date(value.date), 'yyyy-MM-dd'));
                });
                $scope.loading = true;
            }, function (error) {
                    console.log(error);
                    $scope.loading = true;
            })
        }

    }]);


}());

