(function () {
    'use strict';

    angular.module('app').factory('despesaService', despesaService);

    despesaService.$inject = ['$http', '$q', '$rootScope', "SETTINGS"];


    function despesaService($http, $q, $rootScope, SETTINGS) {
        return {
            registrar: registrar,
            excluir: excluir,
            calcularDistancia: calcularDistancia,
            obterPorId: obterPorId,
            alterarDespesa: alterarDespesa,
            emitirRecibo: emitirRecibo
        }

        function registrar(processo) {
            return $http.post(SETTINGS.SERVICE_URL + 'api/despesa/registrar', processo, $rootScope.header);
        }

        function excluir(despesa) {
            return $http.post(SETTINGS.SERVICE_URL + 'api/despesa/excluir', despesa.id, $rootScope.header);
        }

        function alterarDespesa(despesa) {
            return $http.post(SETTINGS.SERVICE_URL + 'api/despesa/alterarDespesa/' + despesa.id, despesa, $rootScope.header);
        }

        function obterPorId(id) {
            return $http.get(SETTINGS.SERVICE_URL + 'api/despesa/obterPorId/' + id, $rootScope.header);
        }

        function calcularDistancia(orig, dest) {
            var service = new google.maps.DistanceMatrixService;
            var deferred = $q.defer();

            service.getDistanceMatrix({
                origins: [orig],
                destinations: [dest],
                travelMode: google.maps.TravelMode.DRIVING,
                unitSystem: google.maps.UnitSystem.METRIC,
                avoidHighways: false,
                avoidTolls: false
            }, function callback(response, status) {
                if (status === google.maps.DistanceMatrixStatus.OK) {
                    deferred.resolve(response.rows[0].elements[0].distance.value);

                } else {
                    deferred.reject(status);

                }
            });

            return deferred.promise;
        }

        function emitirRecibo(processoId) {
            return $http.get(SETTINGS.SERVICE_URL + 'api/despesa/emitirRecibo/' + processoId, $rootScope.header);
        }
    }
})();