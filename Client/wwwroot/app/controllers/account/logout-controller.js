(function () {
    'use strict';

    angular.module('app').controller('logoutController', logoutController);

    logoutController.$inject = ['$rootScope', '$location', 'SETTINGS'];

    function logoutController($rootScope, $location, SETTINGS) {
        var vm = this;

        activate();

        function activate() {
            $rootScope.user = null;
            $rootScope.token = null;
            $rootScope.header = null;
            sessionStorage.removeItem(SETTINGS.AUTH_TOKEN);
            sessionStorage.removeItem(SETTINGS.AUTH_USER);
            sessionStorage.removeItem(SETTINGS.AUTH_ROLES);
            $location.path('/login');
        }
    }

})();