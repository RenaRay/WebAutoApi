(function () {
    'use strict';

    angular
        .module('app.user')
        .controller('app.user.login.controller', [
            'app.services.authentication',
            '$location',
            main
        ]);

    function main(authentication, $location) {
        var vm = this;
        vm.data = {
            login: '',
            password: ''
        };
        vm.message = '';
        vm.doLogin = login;

        function login() {
            authentication
                .login(vm.data)
                .then(
                    function (response) {
                        $location.path('/home');
                    },
                    function (err) {
                        vm.message = err.data.error_description;
                    }
                );
        }
    }
})();