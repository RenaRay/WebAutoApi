(function () {
    'use strict';

    angular
        .module('app.chat')
        .controller('app.chat.list.controller', [
            'app.config',
            'app.services.conversation',
            main
        ]);

    function main(config, conversationService) {
        var vm = this;
        vm.conversations = [];
        
        conversationService
            .getAll()
            .then(function (conversations) {
                vm.conversations = conversations;
            });
    }
})();