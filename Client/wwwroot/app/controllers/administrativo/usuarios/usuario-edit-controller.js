(function () {
    'use strict';

    $(document).ready(function () {
        $('#container').show();
    });

    angular.module('app').controller('usuarioEditController', usuarioEditController);

    usuarioEditController.$inject = ['$rootScope', '$routeParams','$location', 'usuarioService', 'perfilService'];

    function usuarioEditController($rootScope, $routeParams, $location, usuarioService, perfilService) {
        var vm = this;

        var id = $routeParams.id;

        vm.emailPattern = /^([a-zA-Z0-9])+([a-zA-Z0-9._%+-])+@([a-zA-Z0-9_.-])+\.(([a-zA-Z]){2,6})$/;
        vm.perfilList = [];
        vm.loading = false;
        vm.usuario = {};


        vm.salvar = salvar;
        vm.addPerfil = addPerfil;
        vm.removePerfil = removePerfil;
        vm.resetarSenha = resetarSenha;

        activate();

        function activate() {
            obterPorId();
            selecionarPerfis();
        }

        function salvar() {
            vm.loading = true;
            usuarioService.editar(vm.usuario)
                .success(success)
                .catch(fail);

            function success() {
                toastr.success('Usuário foi alterado!', 'Sucesso');
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

        function resetarSenha() {
            usuarioService.resetarSenha(vm.usuario)
                .success(success)
                .catch(fail);

            function success(response) {
                toastr.success("Senha resetada com sucesso", "SUCESSO");
            }

            function fail(parameters) {
                toastr.error("Erro ao resetar senha", "ERRO");
            }
        }

        function obterPorId() {
            usuarioService.obterPorId(id)
                .success(success)
                .catch(fail);

            function success(response) {
                vm.usuario = response;
                
            }

            function fail( error) {
                toastr.error('Não foi possível carregar usuário', 'ERRO');
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
                        if (value.nome == data.nome) {
                            toastr.warning('Esse perfil já adicionado', 'Aviso');
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
    }
})();