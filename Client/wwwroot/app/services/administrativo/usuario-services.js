(function () {

    angular.module("app").factory("usuarioService", usuarioService);

    usuarioService.$inject = ['$http', '$rootScope','SETTINGS'];

    function usuarioService($http, $rootScope, SETTINGS) {
        return {
            selecionarTodos: selecionarTodos,
            adicionar: adicionar,
            excluir: excluir,
            editar: editar,
            verificarSeLoginExiste: verificarSeLoginExiste,
            obterPorId: obterPorId,
            verificarLoginSenha: verificarLoginSenha,
            alterarSenha: alterarSenha,
            verificarSenhaPadrao: verificarSenhaPadrao,
            resetarSenha: resetarSenha
        }


        function adicionar(usuario) {
            return $http.post(SETTINGS.SERVICE_URL + "api/usuario/registrar", usuario, $rootScope.header);
        }
        
        function editar(usuario) {
            return $http.post(SETTINGS.SERVICE_URL + "api/usuario/editar/" + usuario.id, usuario, $rootScope.header);
        }

        function excluir(usuarioId) {
            return $http.post(SETTINGS.SERVICE_URL + "api/usuario/excluir", usuarioId,  $rootScope.header);
        }

        function obterPorId(id) {
            return $http.get(SETTINGS.SERVICE_URL + 'api/usuario/obterPorId/' + id, $rootScope.header);
        }

        function selecionarTodos() {
            return $http.get(SETTINGS.SERVICE_URL + 'api/usuario/', $rootScope.header);
        }

        function verificarSeLoginExiste(login) {
            return $http.get(SETTINGS.SERVICE_URL + 'api/usuario/verificarLogin', {
                params: {
                    login: login
                },
                headers: $rootScope.header.headers
            });
        }

        function verificarLoginSenha(login, senha) {
            return $http.get(SETTINGS.SERVICE_URL + 'api/usuario/verificarLoginSenha', {
                params: {
                    login: login,
                    senha: senha
                },
                headers: $rootScope.header.headers
            });
        }

        function verificarSenhaPadrao(id) {
            return $http.get(SETTINGS.SERVICE_URL + "api/usuario/verificarSenhaPadrao/" + id, $rootScope.header);
        }

        function alterarSenha(usuario) {
            return $http.post(SETTINGS.SERVICE_URL + "api/usuario/alterarSenha", usuario, $rootScope.header);
        }

        function resetarSenha(usuario) {
            return $http.post(SETTINGS.SERVICE_URL + "api/usuario/resetarSenha", usuario.id, $rootScope.header);
        }
    }

})();