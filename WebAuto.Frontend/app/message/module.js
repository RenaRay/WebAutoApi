(function () {
    'use strict';

    angular
        .module('app.message', [
            'ngRoute',
            'ui.bootstrap'
        ])
        .config(routes);

    function routes($routeProvider) {
        $routeProvider.when("/inbox", {
            templateUrl: "/app/message/inbox.view.html"
        });
    }
})();