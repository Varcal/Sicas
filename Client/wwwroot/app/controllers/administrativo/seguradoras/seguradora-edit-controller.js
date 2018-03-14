(function() {
    'use strict';

    angular.module('app').controller('seguradoraEditController', seguradoraEditController);

    seguradoraEditController.$inject = ['$routeParams', '$location', 'seguradoraService', 'servicoSeguradoraService'];

    function seguradoraEditController($routeParams, $location, seguradoraService, servicoSeguradoraService) {
        var vm = this;
        
        vm.emailPattern = /^([a-zA-Z0-9])+([a-zA-Z0-9._%+-])+@([a-zA-Z0-9_.-])+\.(([a-zA-Z]){2,6})$/;
        vm.agenciaPattern = /^[0-9-]+$/;
        vm.notificacoes = [];
        vm.loading = false;

        var id = $routeParams.id;

        var Evento = function (servicoSeguradora, eventoTipo, franquiaKm, honorario) {
            this.servicoSeguradora = servicoSeguradora;
            this.servicoSeguradoraId = servicoSeguradora.id;
            this.eventoTipo = eventoTipo;
            this.eventoTipoId = eventoTipo.id;
            this.franquiaKm = franquiaKm;
            this.honorario = honorario;
        }


        vm.carregarEventoTipo = carregarEventoTipo;
        vm.addEvento = addEvento;
        vm.removeEvento = removeEvento;
        vm.salvar = salvar;

        activate();


        function activate() {
            obterPorId();
            carregarTipoServicos();
        }


        function salvar() {
            vm.loading = true;
            seguradoraService.editar(vm.seguradora)
                .success(success)
                .catch(fail);

            function success() {
                toastr.success('seguradora alterada com sucesso', 'SUCESSO');
                limparCampos();
                vm.loading = false;
                $location.path('/seguradora');
            }

            function fail(error) {
                if (error.data.errors.length > 0) {
                    vm.notificacoes = error.data.errors;
                    return;
                }
                toastr.error("Não foi possível alterar seguradora", "ERRO");
                console.log(error);
                vm.loading = false;
            }

        }

        function obterPorId() {
            seguradoraService.obterPorId(id)
                .success(success)
                .catch(fail);

            function success(response) {
                vm.seguradora = response;
            }

            function fail(error) {
                toastr.error("Não foi possível obter a seguradora", "ERRO");
                console.log(error);
            }
        }

        function carregarTipoServicos() {
            servicoSeguradoraService.selecionarTodosComEventos()
                .success(success)
                .catch(fail);

            function success(response) {
                vm.servicoSeguradoraList = response;
            }

            function fail(error) {
                toastr.error("Erro ao carregar tipos de serviço", "ERRO");
                console.log(error);
            }
        }

        function carregarEventoTipo() {
            resetInputs();
            if (vm.evento.servicoSeguradora == null) {
                vm.desabilitaDropEvento = true;
                vm.eventoTipoList = [];
                return;
            }

            vm.eventoTipoList = vm.evento.servicoSeguradora.eventosTipos;
            vm.desabilitaDropEvento = false;

        }

        function addEvento(data) {

            var flag = false;
            if (data !== null && data !== undefined) {
                var e = new Evento(data.servicoSeguradora, data.eventoTipo, data.franquiaKm, data.honorario);
                if (vm.seguradora.eventos.length > 0) {
                    angular.forEach(vm.seguradora.eventos, function (value, key) {
                        if (value.servicoSeguradora.id === data.servicoSeguradora.id && value.eventoTipo.id === data.eventoTipo.id) {
                            toastr.warning('Perfil já foi adicionado', 'Aviso');
                            flag = true;
                        }
                    });

                    if (!flag) {

                        vm.seguradora.eventos.push(e);
                    }

                } else {

                    vm.seguradora.eventos.push(e);
                }
            } else {
                toastr.warning('Nenhum evento selcionado!!!', "Aviso");
            }

            resetDropdown();
            resetInputs();
        }

        function removeEvento(data) {
            var index = vm.seguradora.eventos.indexOf(data);
            vm.seguradora.eventos.splice(index, 1);
        }

        function resetDropdown() {
            if (angular.isDefined(vm.evento.servicoSeguradora)) {
                delete vm.evento.servicoSeguradora;
            }
            if (angular.isDefined(vm.evento.eventoTipo)) {
                delete vm.evento.eventoTipo;

            }

            vm.desabilitaDropEvento = true;
        }

        function resetInputs() {

            vm.evento.franquiaKm = 0;
            vm.evento.honorario = 0;
        }

        function limparCampos() {
            vm.seguradora = {};
        }
    }

})();