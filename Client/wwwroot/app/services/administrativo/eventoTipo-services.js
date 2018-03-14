(function() {
    'use strict';

    angular.module('app').factory('eventoTipoService', eventoTipoService);

    eventoTipoService.$inject = ['$http', '$rootScope', 'SETTINGS'];

    function eventoTipoService($http, $rootScope, SETTINGS) {
        return{
            selecionarTodos: selecionarTodos,
            registrar: registrar,
            excluir: excluir,
            alterar: alterar,
            obterPorId: obterPorId
        }


        function selecionarTodos() {
            return $http.get(SETTINGS.SERVICE_URL + "api/eventoTipo", $rootScope.header);
        }

        function obterPorId(id) {
            return $http.get(SETTINGS.SERVICE_URL + "api/eventoTipo/obterPorId/" + id, $rootScope.header);
        }
        
        function registrar(eventoTipo) {
            return $http.post(SETTINGS.SERVICE_URL + 'api/eventoTipo/registrar', eventoTipo, $rootScope.header);
        }

        function alterar(eventoTipo) {
            return $http.post(SETTINGS.SERVICE_URL + 'api/eventoTipo/editar/' + eventoTipo.id, eventoTipo, $rootScope.header);
        }

        function excluir(id) {
            return $http.post(SETTINGS.SERVICE_URL + 'api/eventoTipo/excluir', id, $rootScope.header);
        }
    }
})();