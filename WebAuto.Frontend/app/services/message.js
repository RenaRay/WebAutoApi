﻿(function () {
    'use strict';

    angular
        .module('app')
        .factory('app.services.message', [
            'app.config',
            'app.services.authentication',
            '$rootScope',
            '$http',
            main
        ]);

    function main(config, authentication, $rootScope, $http) {

        var service = {
            state: {
                unreadCount: 0
            },
            send: send
        };

        $rootScope.$watch(
            function () {
                return authentication.status.login;
            },
            function (newVal, oldVal, scope) {
                updateUnreadCount();
            });

        function updateUnreadCount() {
            var url = config.backendUrl + 'messages/unread';
            $http.get(url)
                .then(function (response) {
                    service.state.unreadCount = response.data.count;
                });
        }
        updateUnreadCount();

        function send(data) {
            var url = config.backendUrl + 'message';
            return $http.post(url, data);
        }

        return service;
    }
})();