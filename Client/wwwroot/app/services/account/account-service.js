(function () {
    'use strict';

    angular.module('app').factory('accountService', accountService);

    accountService.$inject = ['$http', 'SETTINGS'];

    function accountService($http, SETTINGS) {

        return {

            login: login,
            obterUsuario: obterUsuario

        };


        function login(data) {

            var dt = 'grant_type=password&username=' + data.login + '&password=' + data.password;
            var url = SETTINGS.SERVICE_URL + '/api/security/token';
            var header = { headers: { 'Content-type': 'application/x-www-form-urlencoded' } }

            return $http.post(url, dt, header);
        }

        function obterUsuario(user, header) {
            return $http.post(SETTINGS.SERVICE_URL + "api/usuario/obterUsuarioLogado", user, header);
        }
    }

})();