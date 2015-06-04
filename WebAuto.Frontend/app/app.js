(function() {
    'use strict';

    angular
        .module('app', [
            'ngRoute',
            'LocalStorageModule',
            'angular-loading-bar',

            'app.home',
            'app.user'
        ])
        .config(routes)
        .config(http)
        .run([
            main
        ]);
    
    function routes($routeProvider) {
        $routeProvider.otherwise({ redirectTo: "/home" });
    }

    function http($httpProvider) {
        $httpProvider.interceptors.push(
            'app.interceptors.noresponse',
            'app.interceptors.globalization',
            'app.interceptors.authentication',
            'app.interceptors.modelstate',
            'app.interceptors.internal-error');
    }

    function main() {
    }
})();