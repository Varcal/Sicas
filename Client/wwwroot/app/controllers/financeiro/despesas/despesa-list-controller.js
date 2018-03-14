(function () {
    'use strict';

    angular.module('app').controller('despesaListController', despesaListController);

    despesaListController.$inject = ['$uibModal', 'seguradoraService', 'processoService', "SETTINGS"];

    function despesaListController($uibModal, seguradoraService, processoService, SETTINGS) {
        var vm = this;

        vm.processos = [];
        vm.seguradoras = [];
        vm.habilitaBtn = false;
        vm.url = SETTINGS.SERVICE_URL + "api/processo/downloadArquivo/";

        activate();

        vm.pesquisar = pesquisar;
        vm.finalizarDespesas = finalizarDespesas;
        vm.openModal = openModal;


        function activate() {
            carregarSeguradoras();
            pesquisar();
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
            
                vm.habilitaBtn = true;
                processoService.selecionarPorSeguradoraParaDespesas(vm.seguradoraId)
                .success(success)
                .catch(fail);
            

            function success(response) {
                if (response.length <= 0) {
                    toastr.info('Nenhum processo cadastrado', "INFORMAÇÂO");
                }
                vm.processos = response;
            }

            function fail() {
                toastr.error("Erro ao carregar processos", "ERRO");
            }
        }

        function finalizarDespesas(id) {
            processoService.finalizarDespesas(id)
                .success(success)
                .catch(fail);

            function success(response) {
                toastr.success("Despesa finalizada com sucesso", "Sucesso");
                pesquisar();
            }

            function fail(error) {
                toastr.error("Ocorreu um erro ao finalizar análise", "ERRO");
            }
        }

        function cancelarProcesso(id) {
            processoService.cancelarProcesso(id)
                .success(success)
                .catch(fail);

            function success(response) {
                toastr.success("Processo cancelado com sucesso", "Sucessso");
                pesquisar();
            }

            function fail(error) {
                toastr.error("Erro ao cancelar o processo", "ERRO");
            }
        }

        vm.animationsEnabled = true;

        function openModal(processoId, size, parentSelector) {
            var parentElem = parentSelector ?
              angular.element($document[0].querySelector('.modal-demo ' + parentSelector)) : undefined;
            var modalInstance = $uibModal.open({
                animation: vm.animationsEnabled,
                ariaLabelledBy: 'modal-title',
                ariaDescribedBy: 'modal-body',
                templateUrl: 'ProcessoModal.html',
                controller: 'ProcessoModalCtrl',
                controllerAs: 'vm',
                size: size,
                appendTo: parentElem,
                resolve: {
                    processoId: function () {
                        return processoId;
                    }
                }
            });

            modalInstance.result.then(function (processoId) {
                cancelarProcesso(processoId);
            });
        }
    }
})();