(function() {
    'use strict';

    angular.module('app').factory('servicoSeguradoraService', servicoSeguradoraService);

    servicoSeguradoraService.$inject = ['$http', '$rootScope', 'SETTINGS'];

    function servicoSeguradoraService($http, $rootScope, SETTINGS) {
        return {
            adicionar: adicionar,
            alterar: alterar,
            selecionarTodos: selecionarTodos,
            selecionarTodosComEventos: selecionarTodosComEventos,
            obterPorId: obterPorId,
            excluir: excluir
        }


        function adicionar(data) {
            return $http.post(SETTINGS.SERVICE_URL + "api/servicoSeguradora/Registrar", data, $rootScope.header);
        }

        function alterar(data) {
            return $http.post(SETTINGS.SERVICE_URL + 'api/servicoSeguradora/Alterar/' + data.id, data, $rootScope.header);
        }

        function excluir(id) {
            return $http.post(SETTINGS.SERVICE_URL + 'api/servicoSeguradora/Excluir', id,  $rootScope.header);
        }
        
        function selecionarTodos() {
            return $http.get(SETTINGS.SERVICE_URL + "api/servicoSeguradora", $rootScope.header);
        }

        function selecionarTodosComEventos() {
            return $http.get(SETTINGS.SERVICE_URL + "api/servicoSeguradoraComEventos", $rootScope.header);
        }
        
        function obterPorId(id) {
            return $http.get(SETTINGS.SERVICE_URL + "api/servicoSeguradora/obterPorId/" + id, $rootScope.header);
        }
    }
})();

