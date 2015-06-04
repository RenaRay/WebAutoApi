(function () {
    'use strict';

    angular
        .module('app')
        .factory('app.services.profile', [
            'app.config',
            'app.services.authentication',
            '$http',
            main
        ]);

    function main(config, authentication, $http) {

        var service = {
            get: get,
            post: post,
            findByPlatePart: findByPlatePart
        };

        function get(login) {
            var url = config.backendUrl + 'profile/'+login;
            return $http
                .get(url)
                .then(function (results) {
                    return results;
                });
        }

        function post(data) {
            var url = config.backendUrl + 'profile';
            return $http
                .post(url, data)
                .then(function (results) {
                    return results;
                });
        }

        function findByPlatePart(platePart) {
            var url = config.backendUrl + 'user/find';
            return $http.get(url, {
                params: {
                    plate: platePart,
                    matchExact: false
                }
            }).then(function (response) {
                var currentUserLogin = authentication.status.login;
                return response.data.filter(function (profile) {
                    return profile.user.login != currentUserLogin;
                });
            });
        }

        return service;
    }
})();