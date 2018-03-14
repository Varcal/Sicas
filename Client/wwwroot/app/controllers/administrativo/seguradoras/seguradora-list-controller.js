(function () {
    'use strict';

    angular.module('app').controller('seguradoraListController', seguradoraListController);

    seguradoraListController.$inject = ['$uibModal', 'seguradoraService', 'DTOptionsBuilder', 'DTColumnDefBuilder'];

    function seguradoraListController($uibModal,seguradoraService, DTOptionsBuilder, DTColumnDefBuilder) {
        var vm = this;

        vm.seguradoras = [];

        vm.dtOptions = DTOptionsBuilder.newOptions()
            .withPaginationType('full_numbers')
            .withDisplayLength(10);
        vm.dtColumnDefs = [
           DTColumnDefBuilder.newColumnDef(0).notSortable()
        ];


        vm.selecionarTodos = selecionarTodos;
        vm.openModal = openModal;

        activate();


        function activate() {
            selecionarTodos();
        }

        function selecionarTodos() {
            seguradoraService.selecionarTodos()
                .success(success)
                .catch(fail);

            function success(response) {
                if (response)
                    vm.seguradoras = response;
                else
                    toastr.info('Nenhuma seguradora cadastrada');
            }

            function fail(error) {
                toastr.error("Erro ao conectar com o servidor");
            }
        }

        vm.animationsEnabled = true;

        function openModal(seguradoraId, size, parentSelector) {
            var parentElem = parentSelector ?
              angular.element($document[0].querySelector('.modal-demo ' + parentSelector)) : undefined;
            var modalInstance = $uibModal.open({
                animation: vm.animationsEnabled,
                ariaLabelledBy: 'modal-title',
                ariaDescribedBy: 'modal-body',
                templateUrl: 'seguradoraModal.html',
                controller: 'SeguradoraModalCtrl',
                controllerAs: 'vm',
                size: size,
                appendTo: parentElem,
                resolve: {
                    seguradoraId: function () {
                        return seguradoraId;
                    }
                }
            });

            modalInstance.result.then(function (seguradoraId) {
                excluir(seguradoraId);
            });
        }

        function excluir(seguradoraId) {
            seguradoraService.excluir(seguradoraId)
                .success(success)
                .catch(fail);

            function success(response) {
                selecionarTodos();
                toastr.success("Seguradora excluída com sucesso", "SUCESSO");
            }

            function fail(error) {
                toastr.error("Erro ao excluir seguradora", "ERRO");
                console.log(error);
            }
        }

    }
})();