(function () {
    'use strict';

    angular.module('app').controller('processoHistoricoController', processoHistoricoController);

    processoHistoricoController.$inject = ['seguradoraService', 'processoService'];

    function processoHistoricoController(seguradoraService, processoService) {
        var vm = this;

        vm.historicoList = [];
        vm.seguradoras = [];
        vm.habilitaBtn = false;
        

        activate();

        vm.pesquisar = pesquisar;
        vm.habilitarPesquisa = habilitarPesquisa;

        function activate() {
            carregarSeguradoras();
        }

        function carregarSeguradoras() {
            seguradoraService.selecionarTodos()
                .success(success)
                .catch(fail);

            function success(response) {
                vm.seguradoras = response;
            }

            function fail(error) {
                toastr.error("Erro ao carregar seguradoras", "ERRO");
                console.log(error);
            }
        }

        function pesquisar() {
            if (vm.seguradoraId && vm.numeroSinistro !== null) {
               processoService.selecionarComHistorico(vm.seguradoraId, vm.numeroSinistro)
                .success(success)
                .catch(fail);
            } else {
                vm.historicoList = [];
            }


            function success(response) {
                if (response) {                  
                    vm.historicoList = response;
                    return;
                }
                toastr.info('Nenhum processo cadastrado', "INFORMAÇÂO");
            }

            function fail() {
                toastr.error("Erro ao carregar históricos do processos", "ERRO");
            }
        }

        function habilitarPesquisa() {
            if (vm.seguradoraId && vm.numeroSinistro !== undefined) {
                vm.habilitaBtn = true;
            } else {
                vm.habilitaBtn = false;
            }
        }
    }
})();