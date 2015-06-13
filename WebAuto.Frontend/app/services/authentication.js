 (function () {
    'use strict';

    angular
        .module('app')
        .factory('app.services.authentication', [
            'app.config',
            '$http',
            'localStorageService',
            '$q',
            '$location',
            main
        ]);

    function main(config, $http, localStorageService, $q, $location) {

        var service = {
            login: login,
            logout: logout,
            getToken: getToken,
            status: {
                isAuthenticated: false,
                login: null,
                token: null
            }
        };
        var status = service.status;

        function login(data) {
            var requestUrl = config.backendUrl + 'token';
            var requestBody = 'grant_type=password&username=' + data.login + '&password=' + data.password;
            var requestHeaders = {
                'Content-Type': 'application/x-www-form-urlencoded'
            };

            return $http
                .post(
                    requestUrl,
                    requestBody,
                    {
                        headers: requestHeaders
                    })
                .then(
                    function (response) {
                        status.isAuthenticated = true;
                        status.login = data.login;
                        status.token = response.data.access_token;
                        localStorageService.set('authentication', status);
                        return response;
                    },
                    function (response) {
                        logout();
                        return $q.reject(response);
                    });
        }

        function logout() {
            localStorageService.remove('authentication');

            status.isAuthenticated = false;
            status.login = null;
            status.token = null;
        }

        function getToken() {
            return service.status.token;
        }

        var statusFromLocalStorage = localStorageService.get('authentication');
        if (statusFromLocalStorage) {
            status.isAuthenticated = statusFromLocalStorage.isAuthenticated;
            status.login = statusFromLocalStorage.login;
            status.token = statusFromLocalStorage.token;
        }

        return service;
    }
})();