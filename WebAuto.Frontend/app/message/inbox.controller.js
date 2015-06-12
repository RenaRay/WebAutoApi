(function () {
    'use strict';

    angular
        .module('app.user')
        .controller('app.message.inbox.controller', [
            'app.services.message',
            main
        ]);

    function main(messageService) {
        var vm = this;
        vm.messages = [];
        vm.like = like;

        messageService
            .getInboxMessages()
            .then(function (results) {
                vm.messages = results.data;

                messageService.readInboxMessages();
            }, function (error) {

            });

        function like(messageId) {
            messageService
                .like(messageId)
                .then(function () {
                    for (var i in vm.messages) {
                        var message = vm.messages[i];
                        if (message.id == messageId) {
                            message.isLiked = true;
                            break;
                        }
                    }
                });
        }
    }
})();