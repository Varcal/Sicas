(function() {
    'use strict';

    angular.module('app').controller('servicoSeguradoraEditController', servicoSeguradoraEditController);

    servicoSeguradoraEditController.$inject = ['$routeParams','$location', 'eventoTipoService', 'servicoSeguradoraService'];

    function servicoSeguradoraEditController($routeParams, $location, eventoTipoService, servicoSeguradoraService) {
        var vm = this;

        var id = $routeParams.id;

        vm.eventoTipoList = [];
        vm.loading = false;

        vm.servicoSeguradora = {
            id: 0,
            nome: '',
            eventosTipos: []
        }

        vm.alterar = alterar;
        vm.addEventoTipo = addEventoTipo;
        vm.removeEventoTipo = removeEventoTipo;

        activate();


        function activate() {
            selecionarEventoTipos();
            obterPorId();
        }

        function alterar() {
            vm.loading = true;
            servicoSeguradoraService.alterar(vm.servicoSeguradora)
                .success(success)
                .catch(fail);

            function success() {
                toastr.success('Serviço foi alterado!', 'Sucesso');
                limparCampos();
                vm.loading = false;
                $location.path('/servicoSeguradora');
            }

            function fail(error) {
                toastr.error('Erro ao alterar o serviço', 'Erro');
                console.log(error);
                vm.loading = false;
            }
        }

        function obterPorId() {
            servicoSeguradoraService.obterPorId(id)
                .success(success)
                .catch(fail);

            function success(response) {
                vm.servicoSeguradora = response;
            }

            function fail(error) {
                toastr.error('Não foi possível carregar usuário', 'ERRO');
                console.log(error);
            }
        }

        function selecionarEventoTipos() {
            eventoTipoService.selecionarTodos()
                .success(success)
                .catch(fail);

            function success(response) {
                vm.eventoTipoList = response;
            }

            function fail(error) {
                toastr.error('Erro ao carregar Tipos de Evento', 'ERRO');
                console.log(error);
            }
        }

        function addEventoTipo(data) {
            var flag = false;
            if (data != null && data.id > 0) {
                if (vm.servicoSeguradora.eventosTipos.length > 0) {
                    angular.forEach(vm.servicoSeguradora.eventosTipos, function (value, key) {
                        if (value.nome === data.nome) {
                            toastr.warning('Tipo de Evento já adicionado', 'Aviso');
                            flag = true;
                        }
                    });

                    if (!flag) {

                        vm.servicoSeguradora.eventosTipos.push(data);
                    }

                } else {

                    vm.servicoSeguradora.eventosTipos.push(data);
                }
            } else {
                toastr.warning('Nenhum tipo de evento selcionado!!!', "Aviso");
            }

            resetDropDown();
        }

        function removeEventoTipo(data) {
            var index = vm.servicoSeguradora.eventosTipos.indexOf(data);
            vm.servicoSeguradora.eventosTipos.splice(index, 1);
        }

        function limparCampos() {
            vm.servicoSeguradora = {
                id: 0,
                nome: '',
                eventosTipos: []
            }
        }

        function resetDropDown() {
            if (angular.isDefined(vm.eventoTipo)) {
                delete vm.eventoTipo;
            }
        }
    }

})();