(function() {
    'use strict';

    angular.module("app").factory("perfilService", perfilService);

    perfilService.$inject = ['$http', '$rootScope', 'SETTINGS'];

    function perfilService($http, $rootScope, SETTINGS) {

        return {
            selecionarTodos: selecionarTodos
        }

        function selecionarTodos() {
            return $http.get(SETTINGS.SERVICE_URL + 'api/perfil', $rootScope.header );
        }

    }
})();