(function() {
    'use strict';

    angular.module('app').factory('processoService', processoService);

    processoService.$inject = ['$http', '$rootScope', 'SETTINGS'];

    function processoService($http, $rootScope, SETTINGS) {

        return {
            registrar: registrar,
            selecionarPorSeguradora: selecionarPorSeguradora,
            obterPorId: obterPorId,
            editar: editar,
            selecionarComHistorico: selecionarComHistorico,
            obterParaParecer: obterParaParecer,
            finalizarParecer: finalizarParecer,
            finalizarAnalise: finalizarAnalise,
            cancelarProcesso: cancelarProcesso,
            enviarSindicanteExterno: enviarSindicanteExterno,
            selecionarPorSeguradoraParaDespesas: selecionarPorSeguradoraParaDespesas,
            selecionarPorSeguradoraData: selecionarPorSeguradoraData,
            finalizar: finalizar,
            finalizarDespesas: finalizarDespesas,
            selecionarPorSeguradoraCombo: selecionarPorSeguradoraCombo
        }


        function registrar(processo) {
            return $http.post(SETTINGS.SERVICE_URL + "api/processo/registrar", processo, $rootScope.header);
        }

        function editar(processo) {
            return $http.post(SETTINGS.SERVICE_URL + "api/processo/editar/" + processo.id, processo, $rootScope.header);
        }

        function selecionarPorSeguradora(seguradoraId, statusId, alerta) {
            return $http.get(SETTINGS.SERVICE_URL + 'api/processo/selecionarPorSeguradora',
            {
                params: {
                    seguradoraId: seguradoraId,
                    statusId: statusId,
                    alerta: alerta
                },
                headers: $rootScope.header.headers
            });
        }

        function selecionarPorSeguradoraCombo(seguradoraId) {
            return $http.get(SETTINGS.SERVICE_URL + 'api/processo/selecionarPorSeguradoraCombo/' + seguradoraId, $rootScope.header);
        }

        function selecionarPorSeguradoraParaDespesas(seguradoraId) {
            return $http.get(SETTINGS.SERVICE_URL + 'api/processo/selecionarPorSeguradoraParaDespesas/' + seguradoraId, $rootScope.header);
        }

        function obterPorId(id) {
            return $http.get(SETTINGS.SERVICE_URL + "api/processo/obterPorId/" + id, $rootScope.header);
        }

        function selecionarComHistorico(seguradoraId, numeroSinistro) {
            return $http.get(SETTINGS.SERVICE_URL + 'api/processo/selecionarComHistorico', {
                params: {
                    seguradoraId: seguradoraId,
                    numeroSinistro: numeroSinistro
                },
                headers: $rootScope.header.headers
            });
        }

        function selecionarPorSeguradoraData(seguradoraId, dataInicio, dataFim) {
            return $http.get(SETTINGS.SERVICE_URL + 'api/processo/selecionarPorSeguradoraData', {
                params: {
                    seguradoraId: seguradoraId,
                    dataInicio: dataInicio,
                    dataFim: dataFim
                },
                headers: $rootScope.header.headers
            });
        }

        function obterParaParecer(id) {
            return $http.get(SETTINGS.SERVICE_URL + "api/processo/obterParaParecer/" + id, $rootScope.header);
        }

       
        function finalizarParecer(processo) {
            var formData = new FormData();
            formData.append('id', processo.id);
            formData.append('situacaoId', processo.situacaoId);
            formData.append('comentario', processo.comentario);
            formData.append('file', processo.arquivo);

            return $http.post(SETTINGS.SERVICE_URL + "api/processo/finalizarParecer", formData, {
                transformRequest: angular.identity,
                headers: {
                    "Content-Type": undefined,
                    'Authorization': 'Bearer ' + $rootScope.token
                }
            });
        }

        function finalizarAnalise(id) {
            return $http.post(SETTINGS.SERVICE_URL + 'api/processo/finalizarAnalise', id, $rootScope.header);
        }

        function finalizarDespesas(id) {
            return $http.post(SETTINGS.SERVICE_URL + 'api/processo/finalizarDespesas', id, $rootScope.header);
        }

        function cancelarProcesso(id) {
            return $http.post(SETTINGS.SERVICE_URL + 'api/processo/cancelarProcesso', id, $rootScope.header);
        }

        function enviarSindicanteExterno(processo) {
            return $http.post(SETTINGS.SERVICE_URL + 'api/processo/enviarSindicanteExterno', processo, $rootScope.header);
        }

        function finalizar(processo) {
            return $http.post(SETTINGS.SERVICE_URL + "api/processo/finalizar", processo, $rootScope.header);
        }

    }
    
})();