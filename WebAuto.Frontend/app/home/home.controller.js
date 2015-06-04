(function () {
    'use strict';

    angular
        .module('app.home')
        .controller('app.home.controller', [
            '$location',
            '$log',
            'app.services.authentication',
            'app.services.profile',
            'app.services.conversation',
            main
        ]);

    function main($location, $log, authenticationService, profileService, conversationService) {
        var vm = this;
        vm.plates = [];
        vm.message = '';
        vm.savedSuccessfully = false;
        vm.fromPlate = null,
        vm.to = null;
        vm.text = null;

        vm.send = send;
        vm.findUsers = findUsers;

        if (!authenticationService.status.isAuthenticated) {
            $location.path('/welcome');
            return;
        }

        profileService
            .get(authenticationService.status.login)
            .then(function (response) {
                vm.plates = response.data
                    .cars
                    .map(function (car) {
                        return car.plate;
                    })
                    .reduce(function (plates, plate) {
                        if (plates.indexOf(plate) < 0) {
                            plates.push(plate);
                        }
                        return plates;
                    }, []);
                if (vm.plates.length > 0) {
                    vm.fromPlate = vm.plates[0];
                }
            }, function (error) {
                vm.message = error.data.error_description;
                vm.savedSuccessfully = false;
            });

        function send() {
            var body = {
                fromPlate: vm.fromPlate,
                message: vm.text
            };
            if (typeof vm.to === 'object') {
                body.toPlate = vm.to.car.plate;
                body.toUser = vm.to.login;
            }
            else {
                body.toPlate = vm.to;
            }
            conversationService
                .newConversation(body)
                .then(function (response) {
                    $location.path('/chats/' + response.data.id);
                }, function (error) {
                    vm.message = error.data.error_description;
                    vm.savedSuccessfully = false;
                });
        }

        function findUsers(platePart) {
            return profileService.findByPlatePart(platePart)
                .then(function (conversationMembers) {
                    return conversationMembers
                        .map(function (conversationMember) {
                            var user = conversationMember.user;
                            return {
                                login: user.login,
                                name: user.firstName + ' ' + user.lastName,
                                avatar: user.avatar,
                                car: conversationMember.car
                            }
                        });
                });
        }
    }
})();