(function () {
    'use strict';

   
    angular.module('app').controller('usuarioSenhaController', usuarioSenhaController);

    usuarioSenhaController.$inject = ['$rootScope', '$location', 'usuarioService'];

    function usuarioSenhaController($rootScope, $location, usuarioService) {
        var vm = this;

        vm.usuario = {};
        vm.senhaValida = true;


        vm.salvar = salvar;
        vm.verificarLoginSenha = verificarLoginSenha;


        activate();

        function activate() {
            obterPorId();
        }

        function salvar() {
            usuarioService.alterarSenha(vm.usuario)
                .success(success)
                .catch(fail);

            function success() {
                toastr.success('Senha alterar com sucesso!', 'Sucesso');
                limparCampos();
                $location.path('/');
            }

            function fail(error) {
                toastr.error('Erro ao alterar senha', 'Erro');
                console.log(error);
            }
        }

        function obterPorId() {
            
               var id = $rootScope.user.id;
            
            usuarioService.obterPorId(id)
                .success(success)
                .catch(fail);

            function success(response) {
                vm.usuario = response;

            }

            function fail(error) {
                toastr.error('Não foi possível carregar usuário', 'ERRO');
            }
        }

        function verificarLoginSenha() {
            usuarioService.verificarLoginSenha(vm.usuario.login, vm.usuario.senhaAtual)
                .success(success)
                .catch(fail);

            function success(response) {
                vm.senhaValida = response;
            }

            function fail() {
                
            }
        }

        function limparCampos() {
            vm.usuario = {
                senhaAtual: '',
                novaSenha: '',
                confirmaSenha:''
            }
        }
    }
})();