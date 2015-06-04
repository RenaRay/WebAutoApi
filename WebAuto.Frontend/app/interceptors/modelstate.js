(function () {
    'use strict';

    angular
        .module('app')
        .factory('app.interceptors.modelstate', [
            '$q',
            main
        ]);

    function main($q) {

        var interceptor = {
            responseError: responseError
        };

        function responseError(rejection) {
            if (rejection.status === 400 &&
                rejection.data.modelState) {
                var errors = [rejection.data.error_description];
                for (var key in rejection.data.modelState) {
                    for (var i = 0; i < rejection.data.modelState[key].length; i++) {
                        var errorMessage = rejection.data.modelState[key][i];
                        if (errors.indexOf(errorMessage) === -1) {
                            errors.push(errorMessage);
                        }
                    }
                }
                rejection.data.error_description = errors.join(' ');
            }
            return $q.reject(rejection);
        }

        return interceptor;
    }
})();