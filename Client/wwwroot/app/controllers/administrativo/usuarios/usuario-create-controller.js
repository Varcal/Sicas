(function() {
    'use strict';

    $(document).ready(function() {
        $('#container').show();
    });

    angular.module('app').controller('usuarioCreateController', usuarioCreateController);

    usuarioCreateController.$inject = ['$location', 'usuarioService', 'perfilService'];

    function usuarioCreateController($location, usuarioService, perfilService) {

        var vm = this;

        vm.emailPattern = /^([a-zA-Z0-9])+([a-zA-Z0-9._%+-])+@([a-zA-Z0-9_.-])+\.(([a-zA-Z]){2,6})$/;

        vm.perfilList = [];
        vm.confirmaSenha = '';
        vm.verificarSeLoginExiste = false;
        vm.loading = false;

        vm.usuario = {
            id: 0,
            nome: '',
            login: '',
            senha: '',
            perfis: []
        }

        vm.perfil = { id: 0, nome: "" };

             
        vm.adicionar = adicionar;
        vm.addPerfil = addPerfil;
        vm.removePerfil = removePerfil;
        vm.verificarLogin = verificarLogin;

        activate();

        function activate() {
            selecionarPerfis();
        }

        function adicionar() {
            vm.loading = true;
            usuarioService.adicionar(vm.usuario)
                .success(success)
                .catch(fail);

            function success() {
                toastr.success('Usuário foi cadastrado!', 'Sucesso');
                limparCampos();
                vm.loading = false;
                $location.path('/usuario');
            }

            function fail(error) {
                toastr.error('Erro ao cadastrar usuário', 'Erro');
                console.log(error);
                vm.loading = false;
            }
        }

        function selecionarPerfis() {
            perfilService.selecionarTodos()
                .success(success)
                .catch(fail);

            function success(retorno) {
                vm.perfilList = retorno;
            }

            function fail() {
                toastr.error("Erro ao carregar os pefis");
            }
        }

        function addPerfil(data) {
            var flag = false;
            if (data != null && data.id > 0) {
                if (vm.usuario.perfis.length > 0) {
                    angular.forEach(vm.usuario.perfis, function (value, key) {
                        if (value.nome === data.nome) {
                            toastr.warning('Perfil já foi adicionado','Aviso');
                            flag = true;
                        }
                    });

                    if (!flag) {

                        vm.usuario.perfis.push(data);
                    }

                } else {
                   
                    vm.usuario.perfis.push(data);
                }
            } else {
                toastr.warning('Nenhum perfil selcionado!!!', "Aviso");
            }

            resetDropDown();
        }

        function removePerfil(data) {
            var index = vm.usuario.perfis.indexOf(data);
            vm.usuario.perfis.splice(index, 1);
        }

        function limparCampos() {
            vm.usuario = {
                id: 0,
                nome: '',
                login: '',
                senha: '',
                perfis: []
            }       
        }

        function resetDropDown() {
            if (angular.isDefined(vm.perfil)) {
                delete vm.perfil;
            }
        }

        function verificarLogin() {
            if (vm.usuario.login) {
                usuarioService.verificarSeLoginExiste(vm.usuario.login)
                .success(success)
                .catch(fail);

                function success(response) {
                    vm.loginExiste = response;
                }

                function fail(error) {
                    toastr.error("Erro ao verificar login", "ERRO");
                    console.log(error);
                }
            }         
        }
    }
})();