﻿(function () {
    'use strict';

    angular
        .module('app.user')
        .controller('app.user.profile.view.controller', [
            '$routeParams',
            'app.services.authentication',
            'app.services.profile',
            main
        ]);

    function main($routeParams, authentication, profile) {
        var vm = this;
        vm.data = {
            firstName: '',
            lastName: '',
            email: '',
            phone: '',
            avatar: null,
            cars: []
        };
        vm.isMine = false;

        profile
            .get($routeParams.login)
            .then(function (results) {
                vm.data = results.data;
                vm.isMine = authentication.status.login == $routeParams.login;
            }, function (error) {

            });
    }
})();