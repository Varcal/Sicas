(function () {
    'use strict';

    angular.module('app').controller('lancamentoController', lancamentoController);

    lancamentoController.$inject = ['$filter', '$uibModal', 'sindicanteService', 'processoService', 'seguradoraService', 'lancamentoService'];

    function lancamentoController($filter, $uibModal, sindicanteService, processoService, seguradoraService, lancamentoService) {
        var vm = this;

        vm.processos = [];
        vm.sindicantes = [];
        vm.seguradoras = [];
        vm.habilitaBtn = false;
        vm.desabitaProcesso = true;
        vm.desabilitaSindicante = true;
        vm.formHabilitado = false;

        vm.recibo = {
            lancamentos: []
        }

        //vm.processo = {
        //    statusId : 0
        //}

        vm.lancamentoTipoList = [{ "id": 3, "nome": "Vale" }, { "id": 4, "nome": "Outros" }];
        vm.tipoFinanceriotList = [{ "id": 1, "nome": "Crédito" }, { "id": 2, "nome": "Débito" }];

        activate();

        vm.pesquisar = pesquisar;
        vm.openModal = openModal;
        vm.carregarProcessos = carregarProcessos;
        vm.carregarSindicantes = carregarSindicantes;
        vm.habilitarForm = habilitarForm;
        vm.desabilitarForm = desabilitarForm;
        vm.selcionarTipo = selcionarTipo;
        vm.salvar = salvar;
        vm.fecharLancamento = fecharLancamento;
        vm.reabrirLancamento = reabrirLancamento;


        function activate() {
            carregarSeguradoras();
        }

        function salvar(form) {
            vm.lancamento.processoId = vm.processo.id;
            vm.lancamento.sindicanteId = vm.sindicanteId;
            vm.lancamento.reciboId = vm.recibo.id;
            lancamentoService.registrar(vm.lancamento)
                .success(success)
                .catch(fail);

            function success(response) {
                pesquisar();
                vm.formHabilitado = false;
                limparForm(form);
                toastr.success("Lançamento efetuado com sucesso", "SUCESSO");
            }

            function fail(error) {
                toastr.error("Não foi possível salvar o lançamento", "ERRO");
                console.log(error);
            }
        }

        function excluir(id) {
            lancamentoService.excluir(id)
                .success(success)
                .catch(fail);

            function success(response) {
                toastr.success("Lançamento excluído com sucesso", "SUCESSO");
                pesquisar();
            }

            function fail(error) {
                console.log(error);
                toastr.error("Erro ao excluir lançamento", "ERRO");
            }
        }

        function fecharLancamento() {
            lancamentoService.fecharLancamento(vm.recibo.id)
                .success(success)
                .catch(fail);

            function success(response) {
                toastr.success("Recibo fechado com sucesso", "SUCESSO");
                pesquisar();
            }

            function fail(error) {
                console.log(error);
                toastr.error("Erro ao fechar recibo", "ERRO");
            }
        }

        function reabrirLancamento() {
            lancamentoService.reabrirLancamento(vm.recibo.id)
                .success(success)
                .catch(fail);

            function success(response) {
                toastr.success("Recibo reaberto com sucesso", "SUCESSO");
                pesquisar();
            }

            function fail(error) {
                console.log(error);
                toastr.error("Erro ao fechar recibo", "ERRO");
            }
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

        function carregarProcessos() {
            if (vm.seguradoraId) {
                processoService.selecionarPorSeguradoraCombo(vm.seguradoraId)
                    .success(success)
                    .catch(fail);

                function success(response) {
                    if (response.length > 0) {
                        resetDropDownProcesso();
                        vm.processos = response;
                        vm.desabitaProcesso = false;
                    } else {
                        toastr.info("Não existem processos para essa seguradora", "INFORMAÇÃO");
                    }
                }

                function fail(error) {
                    console.log(error);
                }
            } else {
                vm.processos = [];
                vm.sindicantes = [];
                vm.lancamentos = [];
                vm.desabitaProcesso = true;
                vm.desabilitaSindicante = true;
            }
        }

        function carregarSindicantes() {
            if (vm.processo) {
                sindicanteService.selecionarPorProcesso(vm.processo.id)
                    .success(success)
                    .catch(fail);

                function success(response) {
                    vm.lancamentos = [];
                    if (response.length > 0) {
                        resetDropDownSindicante();
                        vm.sindicantes = response;
                        vm.desabilitaSindicante = false;
                    }

                }

                function fail(error) {
                    toastr.error("Erro ao carregar sindicantes", "ERRO");
                    console.log(error);
                }
            } else {
                vm.sindicantes = [];
                vm.lancamentos = [];
                vm.desabilitaSindicante = true;
            }
        }

        function pesquisar() {
            if (vm.sindicanteId) {
                vm.habilitaBtn = true;
                lancamentoService.selecionarLancamentos(vm.sindicanteId, vm.processo.id)
                    .success(success)
                    .catch(fail);

                function success(response) {
                    var total;
                    var descontos;
                    if (response.lancamentos.length <= 0) {
                        vm.recibo = {};
                        toastr.info('Nenhum lançamento para o sindicante', "INFORMAÇÂO");
                    } else {
                        total = 0;
                        descontos = 0;
                        angular.forEach(response.lancamentos,
                            function (value, key) {
                                if (value.tipoFinanceiroId === 1) {
                                    total += value.valor;
                                } else {
                                    descontos += value.valor;
                                }


                            });
                    }
                    vm.total = total.toFixed(2);
                    vm.descontos = (descontos * -1).toFixed(2);
                    vm.totalReceber = (total + (descontos * -1)).toFixed(2);
                    vm.recibo = response;
                }

                function fail() {
                    toastr.error("Erro ao carregar lançamentos", "ERRO");
                }
            } else {
                vm.recibo = {
                    lancamentos: []
                };
            }

        }

        vm.animationsEnabled = true;

        function openModal(lancamentoId, size, parentSelector) {
            var parentElem = parentSelector ?
              angular.element($document[0].querySelector('.modal-demo ' + parentSelector)) : undefined;
            var modalInstance = $uibModal.open({
                animation: vm.animationsEnabled,
                ariaLabelledBy: 'modal-title',
                ariaDescribedBy: 'modal-body',
                templateUrl: 'LancamentoModal.html',
                controller: 'lancamentoModalCtrl',
                controllerAs: 'vm',
                size: size,
                appendTo: parentElem,
                resolve: {
                    lancamentoId: function () {
                        return lancamentoId;
                    }
                }
            });

            modalInstance.result.then(function (lancamentoId) {
                excluir(lancamentoId);
            });
        }

        function resetDropDownSindicante() {
            if (angular.isDefined(vm.sindicanteId)) {
                delete vm.sindicanteId;
            }
        }

        function resetDropDownProcesso() {
            if (angular.isDefined(vm.processo)) {
                delete vm.processo;
            }
        }

        function habilitarForm() {
            vm.formHabilitado = true;
        }

        function desabilitarForm() {
            vm.formHabilitado = false;
        }

        function selcionarTipo() {
            if (vm.lancamento.lancamentoTipo === 3) {
                vm.lancamento.tipoFinanceiro = 2;
            }
        }

        function resetDropDownTipoLancamento() {
            if (angular.isDefined(vm.lancamento.lancamentoTipo)) {
                delete vm.lancamento.lancamentoTipo;
            }
        }

        function resetDropDownTipo() {
            if (angular.isDefined(vm.lancamento.tipoFinanceiro)) {
                delete vm.lancamento.tipoFinanceiro;
            }
        }

        function limparForm(form) {
            resetDropDownTipoLancamento();
            resetDropDownTipo();
            vm.lancamento.descricao = "";
            vm.lancamento.valor = "";
            vm.lancamento.observacao = "";
            form.$setPristine();
        }
    }
})();