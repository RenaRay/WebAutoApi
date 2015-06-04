(function () {
    'use strict';

    angular
        .module('app')
        .factory('app.interceptors.authentication', [
            '$injector',
            '$location',
            '$q',
            main
        ]);

    function main($injector, $location, $q) {

        var interceptor = {
            request: request,
            responseError: responseError
        };

        function request(config) {
            var authentication = $injector.get('app.services.authentication');
            var token = authentication.getToken();
            if (token) {
                config.headers.Authorization = 'Bearer ' + token;
            }
            return config;
        }

        function responseError(rejection) {
            if (rejection.status === 401) {
                $location.path('/login');
            }
            //TODO: we should try to automatically refresh token if it is expired
            return $q.reject(rejection);
        }

        return interceptor;
    }
})();