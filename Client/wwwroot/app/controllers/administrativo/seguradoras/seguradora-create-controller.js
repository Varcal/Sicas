(function() {
    'use strict';

    angular.module('app').controller('seguradoraCreateController', seguradoraCreateController);

    seguradoraCreateController.$inject = ['$location', 'servicoSeguradoraService', 'seguradoraService'];

    function seguradoraCreateController($location, servicoSeguradoraService, seguradoraService) {
        var vm = this;

        vm.emailPattern = /^([a-zA-Z0-9])+([a-zA-Z0-9._%+-])+@([a-zA-Z0-9_.-])+\.(([a-zA-Z]){2,6})$/;

        vm.servicoSeguradoraList = [];
        vm.eventoTipoList = [];
        vm.desabilitaDropEvento = true;
        vm.notificacoes = [];
        vm.loading = false;

        vm.seguradora = {
            eventos: [],
            valorPorKm: 0
        }

        vm.evento = {
            honorario: 0,
            franquiaKm: 0
        }

        vm.carregarEventoTipo = carregarEventoTipo;
        vm.addEvento = addEvento;
        vm.removeEvento = removeEvento;
        vm.salvar = salvar;

        activate();


        var Evento = function(servicoSeguradora, eventoTipo, franquiaKm, honorario) {
            this.servicoSeguradora = servicoSeguradora;
            this.servicoSeguradoraId = servicoSeguradora.id;
            this.eventoTipo = eventoTipo;
            this.eventoTipoId = eventoTipo.id;
            this.franquiaKm = franquiaKm;
            this.honorario = honorario;
        }
          
        function activate() {           
            carregarTipoServicos();
        }

        function salvar() {
            vm.loading = true;
            seguradoraService.adicionar(vm.seguradora)
                .success(success)
                .catch(fail);

            function success(response) {
                
                toastr.success('seguradora cadastrada', 'SUCESSO');
                limparCampos();
                vm.loading = false;
                $location.path('/seguradora');
            }

            function fail(error) {
                if (error.data.errors.length > 0) {
                    vm.loading = false;
                    vm.notificacoes = error.data.errors;
                    return;
                }
                toastr.error("Erro ao inserir seguradora", "ERRO");
                vm.loading = false;
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
                            toastr.warning('Evento já foi adicionado', 'Aviso');
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