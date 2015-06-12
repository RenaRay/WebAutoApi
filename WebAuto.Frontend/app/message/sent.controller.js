(function () {
    'use strict';

    angular
        .module('app.user')
        .controller('app.message.sent.controller', [
            'app.services.message',
            main
        ]);

    function main(messageService) {
        var vm = this;
        vm.messages = [];

        messageService
            .getSentMessages()
            .then(function (results) {
                vm.messages = results.data;
            }, function (error) {

            });
    }
})();