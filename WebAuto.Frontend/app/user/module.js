(function () {
    'use strict';

    angular
        .module('app.user', [
            'ngRoute',
            'ngFileUpload',
            'ui.bootstrap'
        ])
        .config(routes);

    function routes($routeProvider) {
        $routeProvider.when("/login", {
            templateUrl: "/app/user/login.view.html"
        });
        $routeProvider.when("/register", {
            templateUrl: "/app/user/register.view.html"
        });
        $routeProvider.when("/profile", {
            templateUrl: "/app/user/profile.edit.html"
        });
        $routeProvider.when("/profile/:login", {
            templateUrl: "/app/user/profile.view.html"
        });
    }
})();