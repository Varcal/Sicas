(function() {
    'use strict';

    angular.module('app').factory('servicoSindicanteService', servicoSindicanteService);

    servicoSindicanteService.$inject = ["$http", "$rootScope", "SETTINGS"];

    function servicoSindicanteService($http, $rootScope, SETTINGS) {
        return {
            selecionarTodos: selecionarTodos,
            selecionarPorSindicante: selecionarPorSindicante
        }

        function selecionarTodos() {
            return $http.get(SETTINGS.SERVICE_URL + 'api/servicoSindicante', $rootScope.header);
        }

        function selecionarPorSindicante(id) {
            return $http.get(SETTINGS.SERVICE_URL + 'api/servicoSindicante/selecionarPorSindicante/'+id, $rootScope.header);
        }
    }
})();