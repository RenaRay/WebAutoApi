(function () {
    'use strict';

    angular
        .module('app.home', [
            'ngRoute'
        ])
        .config(routes);

    function routes($routeProvider) {
        $routeProvider.when("/home", {
            templateUrl: "/app/home/home.view.html"
        });
        $routeProvider.when("/welcome", {
            controller: "app.welcome.controller",
            templateUrl: "/app/home/welcome.view.html"
        });
    }
})();