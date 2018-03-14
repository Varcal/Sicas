(function() {

    angular.module('app').factory('homeService', homeService);

    homeService.$inject = ['$http', '$rootScope', 'SETTINGS'];


    function homeService($http, $rootScope, SETTINGS) {
        return {
            obterAlerta: obterAlerta,
            selecionarSindicantes: selecionarSindicantes
        }

        function obterAlerta(usuarioId) {
            return $http.get(SETTINGS.SERVICE_URL + "api/home/alertas/" + usuarioId, $rootScope.header);
        }

        function selecionarSindicantes() {
            return $http.get(SETTINGS.SERVICE_URL + "api/home/sindicantesProcesosMes", $rootScope.header);
        }
    }

})();