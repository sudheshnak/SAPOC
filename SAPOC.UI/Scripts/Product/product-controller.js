var app = angular.module('productModule', []);
app.factory('ProductService', function ($http) {
    var fact = {};
    fact.GetAllEmployees = function () {
        return $http.get(productServiceUrl);
    }
    return fact;
});


app.controller('productCtrl', function ($scope, $http, ProductService) {

    $scope.productDetails = null;

    // Fetching records from the factory
    
    ProductService.GetAllEmployees().then(function (d) {
        $scope.productDetails = d.data; //success        
    }, function () {
        alert('Error Occured....'); //Failed
    });

});

