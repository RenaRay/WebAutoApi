(function () {
    'use strict';

    angular
        .module('app')
        .factory('app.interceptors.globalization', [
            'app.config',
            main
        ]);

    function main(config) {

        var interceptor = {
            request: request
        };

        function request(r) {
            r.headers = r.headers || {};
            r.headers['Accept-Language'] = config.defaultLanguage;
            return r;
        }

        return interceptor;
    }
})();