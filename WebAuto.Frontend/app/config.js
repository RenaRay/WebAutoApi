(function () {
    'use strict';

    var config = {
        backendUrl: 'http://localhost:9665/api/',
        defaultLanguage: 'ru'
    };

    var injector = angular.injector(['ng']);
    var $http = injector.get('$http');
    $http.get(config.backendUrl + 'config').then(
      function (response) {
          angular.extend(config, response.data);

          angular
              .module('app')
              .constant('app.config', config);

          angular.element(document).ready(function () {
              angular.bootstrap(document, ['app']);
          });
      }
    );
})();