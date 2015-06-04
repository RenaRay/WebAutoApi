(function () {
    'use strict';

    angular
        .module('app.user')
        .controller('app.user.register.controller', [
            'app.config',
            'app.services.authentication',
            '$http',
            '$location',
            '$timeout',
            main
        ]);

    function main(config, authentication, $http, $location, $timeout) {
        var vm = this;
        vm.data = {
            firstName: '',
            lastName: '',
            email: '',
            phone: '',
            contactsVisibleTo: 'Friends',
            login: '',
            password: '',
            passwordConfirmation: ''
        };
        vm.message = '';
        vm.savedSuccessfully = false;
        vm.register = register;

        function register() {
            authentication.logout();
            var requestUrl = config.backendUrl + 'user/register';
            $http
                .post(requestUrl, vm.data)
                .then(
                    function (response) {
                        vm.savedSuccessfully = true;
                        vm.message = "Регистрация прошла успешно. Через 2 секунды Вы будете перенаправлены на страницу входа.";
                        redirectToLogin();
                    },
                    function (response) {
                        vm.message = "Не удалось завершить регистрацию из-за ошибок: " + response.data.error_description;
                    });
        }

        function redirectToLogin() {
            var timer = $timeout(function () {
                $timeout.cancel(timer);
                $location.path('/login');
            }, 2000);
        }
    }
})();