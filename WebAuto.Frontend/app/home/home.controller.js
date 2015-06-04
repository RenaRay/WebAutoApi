(function () {
    'use strict';

    angular
        .module('app.home')
        .controller('app.home.controller', [
            '$location',
            '$log',
            'app.services.authentication',
            'app.services.message',
            main
        ]);

    function main($location, $log, authenticationService, messageService) {
        var vm = this;
        vm.message = '';
        vm.savedSuccessfully = false;
        vm.data = {
            toPlate: '',
            text: ''
        };
        vm.send = send;

        if (!authenticationService.status.isAuthenticated) {
            $location.path('/welcome');
            return;
        }

        function send() {
            messageService
                .send(vm.data)
                .then(function (response) {
                    vm.message = 'Сообщение отправлено';
                    vm.savedSuccessfully = true;
                    vm.data.toPlate = '';
                    vm.data.text = '';
                }, function (error) {
                    vm.message = error.data.error_description;
                    vm.savedSuccessfully = false;
                });
        }
    }
})();