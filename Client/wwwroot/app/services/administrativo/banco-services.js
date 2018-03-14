(function() {
    'use strict';

    angular.module('app').factory('bancoService', bancoService);

    bancoService.$inject = ['$http','$rootScope', 'SETTINGS'];

    function bancoService($http, $rootScope, SETTINGS) {
        return {
            selecionarTodos: selecionarTodos
        }

        function selecionarTodos() {
            return $http.get(SETTINGS.SERVICE_URL + 'api/banco', $rootScope.header);
        }
    }

})();