(function () {
    "use strict";

    angular.module("app").controller("reciboSindicanteController", reciboSindicanteController);

    reciboSindicanteController.$inject = ["$uibModal","$rootScope", "$window", "sindicanteService", "lancamentoService","SETTINGS"];

    function reciboSindicanteController($uibModal, $rootScope, $window, sindicanteService,lancamentoService, SETTINGS) {
        var vm = this;

        vm.recibos = [];
        vm.sindicantes = [];
        vm.desabilitaBtn = true;
        vm.habilitaFechamentoBtn = false;

        vm.habilitarBtnPesquisa = habilitarBtnPesquisa;
        vm.pesquisar = pesquisar;
        vm.emitirRecibo = emitirRecibo;
        vm.openModal = openModal;
        vm.fecharPagamento = fecharPagamento;

        activate();


        function activate() {
            carregarSindicantes();
        }

        function fecharPagamento() {
            var obj = {
                dataInicio: vm.dataInicio,
                dataFim: vm.dataFim,
                recibos: vm.recibos
            };

            lancamentoService.fecharPagamento(obj)
                .success(success)
                .catch(fail);

            function success(response) {
                toastr.success("Pagamento fechado com sucesso", "SUCESSO");
                pesquisar();
            }

            function fail(error) {
                toastr.error("Erro ao fechar pagamento", "ERRO");
                console.log(error);
            }
        }

        function carregarSindicantes() {
            sindicanteService.selecionarTodos()
                .success(success)
                .catch(fail);

            function success(response) {
                vm.sindicantes = response;
            }

            function fail(error) {
                toastr.error("Erro ao carregar seguradoras", "ERRO");
                console.log(error);
            }
        }

        function habilitarBtnPesquisa() {
            if (vm.sindicanteId) {
                vm.desabilitaBtn = false;
            }
            else {
                vm.desabilitaBtn = true;
            }
        }

        function pesquisar() {
            lancamentoService.selecionarPorSindicanteData(vm.sindicanteId, vm.dataInicio, vm.dataFim)
                .success(success)
                .catch(fail);

            function success(response) {
                if (response.length <= 0) {
                    toastr.info('Nenhum encontrado encontrado', "INFORMAÇÂO");
                }
                vm.recibos = response;

                angular.forEach(vm.recibos,
                            function (value, key) {
                                if (value.statusId !== 3) {
                                    vm.habilitaFechamentoBtn = true;
                                    return;
                                }

                                vm.habilitaFechamentoBtn = false;
                            });
            }

            function fail() {
                toastr.error("Erro ao carregar processos", "ERRO");
            }
        }

        function emitirRecibo(id) {
            $window.open(SETTINGS.SERVICE_URL + "api/despesa/emitirRecibo/" + id + "?access_token=" + $rootScope.token, "_blank");

            pesquisar();
        }

        vm.animationsEnabled = true;

        function openModal(lancamentos, size, parentSelector) {
            var parentElem = parentSelector ?
              angular.element($document[0].querySelector('.modal-demo ' + parentSelector)) : undefined;
            var modalInstance = $uibModal.open({
                animation: vm.animationsEnabled,
                ariaLabelledBy: 'modal-title',
                ariaDescribedBy: 'modal-body',
                templateUrl: 'reciboModal.html',
                controller: 'reciboModalCtrl',
                controllerAs: 'vm',
                size: size,
                appendTo: parentElem,
                resolve: {
                    lancamentos: function () {
                        return lancamentos;
                    }
                }
            });

            modalInstance.result.then(function (reciboId) {
                //excluir(lancamentoId);
            });
        }
    }

})();