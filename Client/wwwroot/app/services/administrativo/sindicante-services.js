(function () {
    'use strict';

    angular.module('app').factory('sindicanteService', sindicanteService);

    sindicanteService.$inject = ['$http', '$rootScope', 'SETTINGS'];

    function sindicanteService($http, $rootScope, SETTINGS) {

        return {
            adicionar: adicionar,
            editar: editar,
            excluir: excluir,
            selecionarTodos: selecionarTodos,
            obterPorId: obterPorId,
            selecionarSindicanteExterno: selecionarSindicanteExterno,
            obterProcessos: obterProcessos,
            selecionarPorProcesso: selecionarPorProcesso
        };


        function adicionar(data) {
            return $http.post(SETTINGS.SERVICE_URL + 'api/sindicante/registrar', data, $rootScope.header);
        }

        function editar(data) {
            return $http.post(SETTINGS.SERVICE_URL + 'api/sindicante/editar/' + data.id, data, $rootScope.header);
        }

        function excluir(data) {
            return $http.post(SETTINGS.SERVICE_URL + 'api/sindicante/excluir', data, $rootScope.header);
        }

        function selecionarTodos() {
            return $http.get(SETTINGS.SERVICE_URL + 'api/sindicantes', $rootScope.header);
        }

        function obterPorId(id) {
            return $http.get(SETTINGS.SERVICE_URL + 'api/sindicante/obterPorId/' + id, $rootScope.header);
        }

        function selecionarSindicanteExterno() {
            return $http.get(SETTINGS.SERVICE_URL + 'api/sindicantes/TodosExternosAtivos', $rootScope.header);
        }

        function obterProcessos(sindicanteId) {
            return $http.get(SETTINGS.SERVICE_URL + 'api/sindicante/obterProcessos/'+ sindicanteId, $rootScope.header);
        }

        function selecionarPorProcesso(processoId) {
            return $http.get(SETTINGS.SERVICE_URL + 'api/sindicante/selecionarPorProcesso/' + processoId, $rootScope.header);
        }
    }
})();