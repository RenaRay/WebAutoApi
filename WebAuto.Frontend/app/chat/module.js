(function () {
    'use strict';

    angular
        .module('app.chat', [
            'ngRoute',
            'ui.bootstrap'
        ])
        .config(routes);

    function routes($routeProvider) {
        $routeProvider.when("/chats", {
            templateUrl: "/app/chat/list.view.html"
        });
        $routeProvider.when("/chats/:id", {
            templateUrl: "/app/chat/chat.view.html"
        });
    }
})();