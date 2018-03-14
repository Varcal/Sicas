(function () {
    'use strict';

    angular.module('app').factory('lancamentoService', lancamentoService);

    lancamentoService.$inject = ['$http', '$q', '$rootScope', "SETTINGS"];


    function lancamentoService($http, $q, $rootScope, SETTINGS) {
        return {
            selecionarLancamentos: selecionarLancamentos,
            registrar: registrar,
            excluir: excluir,
            fecharLancamento: fecharLancamento,
            reabrirLancamento: reabrirLancamento,
            selecionarPorSindicanteData: selecionarPorSindicanteData,
            fecharPagamento: fecharPagamento
    }

        function selecionarLancamentos(sindicanteId, processoId) {
            return $http.get(SETTINGS.SERVICE_URL + 'api/lancamento/selecionarLancamentos',
            {
                params: {
                    sindicanteId: sindicanteId,
                    processoId: processoId
                },
                headers: $rootScope.header.headers
            });
        }

        function registrar(lancamento) {
            return $http.post(SETTINGS.SERVICE_URL + "api/lancamento/registrar", lancamento, $rootScope.header);
        }

        function excluir(id) {
            return $http.post(SETTINGS.SERVICE_URL + 'api/lancamento/excluir', id, $rootScope.header);
        }

        function fecharLancamento(id) {
            return $http.post(SETTINGS.SERVICE_URL + "api/lancamento/fecharLancamento", id, $rootScope.header);
        }

        function reabrirLancamento(id) {
            return $http.post(SETTINGS.SERVICE_URL + "api/lancamento/reabrirLancamento", id, $rootScope.header);
        }

        function selecionarPorSindicanteData(sindicanteId, dataInicio, dataFim) {
            return $http.get(SETTINGS.SERVICE_URL + 'api/lancamento/selecionarPorSindicanteData', {
                params: {
                    sindicanteId: sindicanteId,
                    dataInicio: dataInicio,
                    dataFim: dataFim
                },
                headers: $rootScope.header.headers
            });
        }

        function fecharPagamento(recibos) {
            return $http.post(SETTINGS.SERVICE_URL + "api/lancamento/fecharPagamento", recibos, $rootScope.header);
        }

    }
})();