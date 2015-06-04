(function () {
    'use strict';

    angular
        .module('app')
        .controller('app.menu.controller', [
            '$location',
            'app.services.authentication',
            'app.services.conversation',
            main
        ]);

    function main($location, authentication, conversation) {
        var vm = this;
        vm.authentication = authentication.status;
        vm.conversation = conversation.state;
        vm.logout = logout;

        
        function logout() {
            authentication.logout();
            $location.path('/welcome');
        }
    }
})();