(function() {
    'use strict';

    angular.module("app").controller('servicoSeguradoraCreateController', servicoSeguradoraCreateController);

    servicoSeguradoraCreateController.$inject = ['$location','eventoTipoService','servicoSeguradoraService'];

    function servicoSeguradoraCreateController($location, eventoTipoService, servicoSeguradoraService) {
        var vm = this;

        vm.eventoTipoList = [];
        vm.loading = false;

        vm.servicoSeguradora = {
            id: 0,
            nome: '',
            eventosTipos: []
        }

        vm.adicionar = adicionar;
        vm.addEventoTipo = addEventoTipo;
        vm.removeEventoTipo = removeEventoTipo;

        activate();


        function activate() {
            selecionarEventoTipos();
        }

        function adicionar() {
            vm.loading = true;
            servicoSeguradoraService.adicionar(vm.servicoSeguradora)
                .success(success)
                .catch(fail);

            function success() {
                toastr.success('Serviço foi cadastrado!', 'Sucesso');
                limparCampos();
                vm.loading = false;
                $location.path('/servicoSeguradora');
            }

            function fail(error) {
                toastr.error('Erro ao cadastrar usuário', 'Erro');
                console.log(error);
                vm.loading = false;
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