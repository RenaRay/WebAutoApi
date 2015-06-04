(function () {
    'use strict';

    angular
        .module('app')
        .filter('avatar', ['app.config', function (config) {

            function avatarFilter(input) {
                if (input == null) {
                    return 'content/images/guest.png';
                }
                return config.backendUrl + 'avatar/' + input;
            }
            avatarFilter.$stateful = true;

            return avatarFilter;
        }]);

})();