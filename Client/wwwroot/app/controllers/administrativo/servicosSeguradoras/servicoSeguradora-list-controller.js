(function () {
    'use strict';

    angular.module("app").controller('servicoSeguradoraListController', servicoSeguradoraListController);

    servicoSeguradoraListController.$inject = ['$uibModal', 'servicoSeguradoraService', 'DTOptionsBuilder', 'DTColumnDefBuilder'];

    function servicoSeguradoraListController($uibModal, servicoSeguradoraService, DTOptionsBuilder, DTColumnDefBuilder) {
        var vm = this;

        vm.servicosSeguradoras = [];

        vm.dtOptions = DTOptionsBuilder.newOptions()
            .withPaginationType('full_numbers')
            .withDisplayLength(10);
        vm.dtColumnDefs = [
            DTColumnDefBuilder.newColumnDef(1).notSortable()
        ];

        vm.openModal = openModal;

        activate();


        function activate() {
            selecionarTodos();
        }

        function selecionarTodos() {
            servicoSeguradoraService.selecionarTodos()
                .success(success)
                .catch(fail);

            function success(response) {
                vm.servicosSeguradoras = response;
            }

            function fail(error) {
                toastr.error('Não foi possivel carregar os serviços de seguradoras', 'ERRO');
                console.log(error);
            }
        }

        vm.animationsEnabled = true;

        function openModal(servicoSeguradoraId, size, parentSelector) {
            var parentElem = parentSelector ?
              angular.element($document[0].querySelector('.modal-demo ' + parentSelector)) : undefined;
            var modalInstance = $uibModal.open({
                animation: vm.animationsEnabled,
                ariaLabelledBy: 'modal-title',
                ariaDescribedBy: 'modal-body',
                templateUrl: 'ServicoSeguradoraModal.html',
                controller: 'ServicoSeguradoraModalCtrl',
                controllerAs: 'vm',
                size: size,
                appendTo: parentElem,
                resolve: {
                    servicoSeguradoraId: function () {
                        return servicoSeguradoraId;
                    }
                }
            });

            modalInstance.result.then(function (servicoSeguradoraId) {
                excluir(servicoSeguradoraId);
            });
        }

        function excluir(servicoSeguradoraId) {
            servicoSeguradoraService.excluir(servicoSeguradoraId)
                .success(success)
                .catch(fail);

            function success(response) {
                selecionarTodos();
                toastr.success("Serviço seguradora excluído com sucesso", "SUCESSO");
            }

            function fail(error) {
                toastr.error("Erro ao excluir serviço seguradora", "ERRO");
                console.log(error);
            }
        }
    }

})();