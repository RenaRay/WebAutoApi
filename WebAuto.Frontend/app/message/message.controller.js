(function () {
    'use strict';

    angular
        .module('app.user')
        .controller('app.message.controller', [
            'app.services.message',
            main
        ]);

    function main(messageService) {
        var vm = this;
        vm.messages = [];

        messageService
            .getInboxMessages()
            .then(function (results) {
                vm.messages = results.data;

                messageService.readInboxMessages();
            }, function (error) {

            });
    }
})();