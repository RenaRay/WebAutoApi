(function () {
    'use strict';

    angular
        .module('app')
        .factory('app.services.conversation', [
            'app.config',
            'app.services.authentication',
            '$rootScope',
            '$http',
            main
        ]);

    function main(config, authentication, $rootScope, $http) {

        var service = {
            state: {
                unreadCount: 0
            },
            newConversation: newConversation,
            getAll: getAll,
            getById: getById,
            postMessage: postMessage,
            readMessages: readMessages
        };

        $rootScope.$watch(
            function () {
                return authentication.status.login;
            },
            function (newVal, oldVal, scope) {
                updateUnreadCount();
            });

        function updateUnreadCount() {
            var url = config.backendUrl + 'messages/unread';
            $http.get(url)
                .then(function (response) {
                    service.state.unreadCount = response.data.count;
                });
        }
        updateUnreadCount();

        function newConversation(data) {
            var url = config.backendUrl + 'conversation';
            return $http.post(url, data);
        }

        function getAll() {
            var url = config.backendUrl + 'conversation';
            return $http
                .get(url)
                .then(function (response) {
                    var conversations = response.data;
                    var currentUserLogin = authentication.status.login;
                    conversations.forEach(function (conversation) {
                        conversation.members = conversation.members
                            .filter(function (member) {
                                return member.user.login != currentUserLogin;
                            });
                    });
                    return conversations;
                });
        }

        function getById(id) {
            var url = config.backendUrl + 'conversation/' + id;
            return $http
                .get(url)
                .then(function (response) {
                    var conversation = response.data;
                    var membersByLogins = mapMembersToLogins(conversation.members);

                    return {
                        name : getConversationName(conversation),
                        messages: getMessages(conversation.messages, membersByLogins),
                        members: membersByLogins,
                        unreadCount: conversation.unreadCount
                    };
                });
        }

        function mapMembersToLogins(members) {
            var membersByLogins = {};
            members.forEach(function (member) {
                membersByLogins[member.user.login] = member;
            });
            return membersByLogins;
        }

        function getConversationName(conversation) {
            var currentUserLogin = authentication.status.login;
            return conversation.members
                .filter(function (member) {
                    return member.user.login != currentUserLogin;
                })
                .map(function (member) {
                    return member.car.plate;
                })
                .join(', ');
        }

        function getMessages(messages, members) {
            var currentUserLogin = authentication.status.login;
            return messages
                .map(function (message) {
                    return {
                        author: members[message.author],
                        text: message.text,
                        posted: message.posted,
                        isMine: message.author == currentUserLogin
                    };
                });
        }

        function postMessage(conversationId, message) {
            var url = config.backendUrl + 'messages/';
            return $http.post(url, {
                conversationId : conversationId,
                message : message
            });
        }

        function readMessages(conversationId, messageCount) {
            var url = config.backendUrl + 'messages/read';
            return $http.post(url, {
                conversationId: conversationId,
                messageCount: messageCount
            }).then(function(){
                updateUnreadCount();
            });
        }

        return service;
    }
})();