(function () {
    'use strict';

    angular
        .module('app')
        .factory('app.interceptors.internal-error', [
            '$q',
            main
        ]);

    function main($q) {

        var interceptor = {
            responseError: responseError
        };

        function responseError(rejection) {
            if (rejection.status === 500) {
                rejection.data = {
                    error_description: 'Внутренняя ошибка приложения'
                };
            }
            return $q.reject(rejection);
        }

        return interceptor;
    }
})();