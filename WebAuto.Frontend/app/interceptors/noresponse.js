(function () {
    'use strict';

    angular
        .module('app')
        .factory('app.interceptors.noresponse', [
            '$q',
            main
        ]);

    function main($q) {

        var interceptor = {
            responseError: responseError
        };

        function responseError(rejection) {
            if (rejection.status === 0) {
                rejection.data = {
                    error_description: 'Сервис недоступен'
                };
            }
            return $q.reject(rejection);
        }

        return interceptor;
    }
})();