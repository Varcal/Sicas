(function () {
    'use strict';

    angular.module('app').factory('reciboSindicanteService', reciboSindicanteService);

    reciboSindicanteService.$inject = ['$http', '$q', '$rootScope', "SETTINGS"];


    function reciboSindicanteService($http, $q, $rootScope, SETTINGS) {
        return {
            selecionarReciboPorSindicanteData: selecionarReciboPorSindicanteData,
            emitirTodos: emitirTodos
        }

        
        function selecionarReciboPorSindicanteData(dataInicio, dataFim) {
            return $http.get(SETTINGS.SERVICE_URL + 'api/reciboSindicante/selecionarReciboSindicantePorData/', {
                params: {
                    dataInicio: dataInicio,
                    dataFim: dataFim
                },
                headers: $rootScope.header.headers
            });
        }

        function emitirTodos(dataInicio, dataFim) {
            return $http.get(SETTINGS.SERVICE_URL + "api/reciboSindicante/emitirTodos/", {
                params: {
                    dataInicio: dataInicio,
                    dataFim: dataFim
                },
                headers: $rootScope.header.headers,
                responseType: "arraybuffer"
            });
        }
    }
})();