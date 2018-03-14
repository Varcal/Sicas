(function() {
    'use strict';

    angular.module('app').factory('seguradoraService', seguradoraService);

    seguradoraService.$inject = ['$http', '$rootScope', 'SETTINGS'];

    function seguradoraService($http, $rootScope, SETTINGS) {

        return {
            adicionar: adicionar,
            editar: editar,
            excluir: excluir,
            selecionarTodos: selecionarTodos,
            obterPorId: obterPorId
        };


        function adicionar(data) {
            return $http.post(SETTINGS.SERVICE_URL + 'api/seguradora/registrar', data, $rootScope.header);
        }

        function editar(data) {
            return $http.post(SETTINGS.SERVICE_URL + 'api/seguradora/editar/' + data.id, data, $rootScope.header);
        }

        function excluir(id) {
            return $http.post(SETTINGS.SERVICE_URL + 'api/seguradora/excluir', id, $rootScope.header);
        }

        function selecionarTodos() {
            return $http.get(SETTINGS.SERVICE_URL + 'api/seguradora', $rootScope.header);
        }

        function obterPorId(id) {
            return $http.get(SETTINGS.SERVICE_URL + 'api/seguradora/obterPorId/'+id, $rootScope.header);
        }
    }
})();