(function () {
    'use strict';

    angular
        .module('app.chat')
        .controller('app.chat.controller', [
            'app.config',
            'app.services.authentication',
            'app.services.conversation',
            '$routeParams',
            main
        ]);

    function main(config, authentication, conversationService, $routeParams) {
        var vm = this;
        vm.conversation = {
            name: null,
            messages: []
        };
        vm.text = null;
        vm.send = send;
        vm.message = '';
        vm.savedSuccessfully = false;

        conversationService
            .getById($routeParams.id)
            .then(function(conversation){
                vm.conversation = conversation;

                var currentUserLogin = authentication.status.login;
                conversationService.readMessages(
                    $routeParams.id, vm.conversation.unreadCount);
            });

        function send() {
            conversationService
                .postMessage($routeParams.id, vm.text)
                .then(function (response) {
                    var currentUserLogin = authentication.status.login;
                    vm.conversation.messages.push({
                        author: vm.conversation.members[currentUserLogin],
                        text: vm.text,
                        posted: Date.now(),
                        isMine: true
                    });
                    vm.message = '';
                    vm.savedSuccessfully = true;
                    vm.text = null;
                }, function (error) {
                    vm.message = error.data.error_description;
                    vm.savedSuccessfully = false;
                });
        }
    }
})();