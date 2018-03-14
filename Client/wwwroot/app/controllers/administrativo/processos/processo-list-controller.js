(function () {
    'use strict';

    angular.module('app').controller('processoListController', processoListController);

    processoListController.$inject = ['$uibModal', '$location', '$window', '$rootScope', 'seguradoraService', 'processoService', "statusService", "SETTINGS", 'DTOptionsBuilder', 'DTColumnDefBuilder'];

    function processoListController($uibModal, $location, $window, $rootScope, seguradoraService, processoService, statusService, SETTINGS, DTOptionsBuilder, DTColumnDefBuilder) {
        var vm = this;

        var alerta;

        vm.index = true;

        vm.processos = [];
        vm.seguradoras = [];
        vm.habilitaBtn = false;
        vm.habilitado = true;
        vm.animationsEnabled = true;
        activate();

        vm.pesquisar = pesquisar;
        vm.finalizarAnalise = finalizarAnalise;
        vm.openModal = openModal;
        vm.baixarArquivo = baixarArquivo;
        vm.baixarAcionamento = baixarAcionamento;
        vm.mostrarHistorico = mostrarHistorico;
        vm.mostrarIndex = mostrarIndex;

        vm.dtOptions = DTOptionsBuilder.newOptions()
            .withPaginationType('full_numbers')
            .withDisplayLength(10);
        vm.dtColumnDefs = [
            DTColumnDefBuilder.newColumnDef(0).notSortable(),
            DTColumnDefBuilder.newColumnDef(1).notSortable(),
            DTColumnDefBuilder.newColumnDef(2).notSortable(),
            DTColumnDefBuilder.newColumnDef(5).notSortable()
        ];


        function activate() {

            alerta = $window.localStorage.getItem("alerta") ? $window.localStorage.getItem("alerta") : 0;

            if ($window.localStorage.getItem("alerta")) {
                $window.localStorage.removeItem("alerta");
            }

            carregarSeguradoras();

            if (!$rootScope.profile.isAnalista()) {
                carregarStatus();
            }

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

        function carregarStatus() {
            statusService.selecionarTodos()
                .success(success)
                .catch(fail);

            function success(response) {
                vm.statusList = response;
            }

            function fail(error) {
                toastr.error("Erro ao carregar status", "ERRO");
                console.log(error);
            }
        }

        function pesquisar() {

            vm.seguradoraId = vm.seguradoraId == undefined || vm.seguradoraId === "" ? 0 : vm.seguradoraId;
            vm.statusId = vm.statusId == undefined || vm.statusId === "" ? 0 : vm.statusId;


            vm.habilitaBtn = true;
            processoService.selecionarPorSeguradora(vm.seguradoraId, vm.statusId, alerta)
                .success(success)
                .catch(fail);


            function success(response) {
                if (response.length <= 0) {
                    toastr.info('Nenhum processo cadastrado', "INFORMAÇÂO");
                }
                vm.processos = response;
                if (alerta > 0) alerta = 0;
            }

            function fail() {
                toastr.error("Erro ao carregar processos", "ERRO");
            }
        }

        function finalizarAnalise(id) {
            vm.habilitado = false;
            processoService.finalizarAnalise(id)
                .success(success)
                .catch(fail);

            function success(response) {
                toastr.success("Análise finalizado com sucesso", "Sucesso");
                pesquisar();
                vm.habilitado = true;
            }

            function fail(error) {
                toastr.error("Ocorreu um erro ao finalizar análise", "ERRO");
                vm.habilitado = true;
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

        function baixarArquivo(id) {
            $window.open(SETTINGS.SERVICE_URL + "api/processo/downloadArquivo/" + id + "?access_token=" + $rootScope.token, "_blank");
        }

        function baixarAcionamento(id) {
            $window.open(SETTINGS.SERVICE_URL + "api/processo/downloadAcionamento/" + id + "?access_token=" + $rootScope.token, "_blank");
        }


        function mostrarIndex() {
            vm.processo = {};
            vm.index = true;
        }

        function mostrarHistorico(processo) {
            vm.processo = processo;
            if (vm.processo.seguradoraId && vm.processo.numeroSinistro !== null) {
                processoService.selecionarComHistorico(vm.processo.seguradoraId, vm.processo.numeroSinistro)
                    .success(success)
                    .catch(fail);
            } else {
                vm.historicoList = [];
            }

            function success(response) {
                if (response) {
                    vm.historicoList = response;
                    vm.index = false;
                    return;
                }
                toastr.info('Nenhum processo cadastrado', "INFORMAÇÂO");
            }

            function fail() {
                toastr.error("Erro ao carregar históricos do processos", "ERRO");
            }
        }
    }
})();