(function () {
    'use strict';

    angular.module('app').factory('enderecoService', enderecoService);

    enderecoService.$inject = ['$http','$rootScope','SETTINGS'];

    function enderecoService($http, $rootScope, SETTINGS) {
        return {
            pesquisarEnderecoPorCep: pesquisarEnderecoPorCep,
            selecionarEstados: selecionarEstados,
            selecionarCidades: selecionarCidades,
            cadastrar: cadastrar
        }

        function cadastrar(endereco) {
            return $http.post(SETTINGS.SERVICE_URL + 'api/endereco/cadastrar', endereco, $rootScope.header);
        }

        function pesquisarEnderecoPorCep(cep) {
            return $http.get('https://api.postmon.com.br/v1/cep/' + cep);
        }

        function selecionarEstados() {
            return $http.get(SETTINGS.SERVICE_URL + 'api/estados', $rootScope.header);
        }

        function selecionarCidades(ufId) {
            return $http.get(SETTINGS.SERVICE_URL + 'api/cidades/' + ufId, $rootScope.header);
        }
    }
})();