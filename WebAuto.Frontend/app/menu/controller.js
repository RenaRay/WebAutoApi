(function () {
    'use strict';

    angular
        .module('app')
        .controller('app.menu.controller', [
            '$location',
            'app.services.authentication',
            'app.services.message',
            main
        ]);

    function main($location, authentication, message) {
        var vm = this;
        vm.authentication = authentication.status;
        vm.message = message.state;
        vm.logout = logout;

        
        function logout() {
            authentication.logout();
            $location.path('/welcome');
        }
    }
})();