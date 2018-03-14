(function () {
    'use strict';

    angular.module('app').controller('eventoTipoListController', eventoTipoListController);

    eventoTipoListController.$inject = ['$uibModal', 'eventoTipoService', 'DTOptionsBuilder', 'DTColumnDefBuilder'];

    function eventoTipoListController($uibModal, eventoTipoService, DTOptionsBuilder, DTColumnDefBuilder) {
        var vm = this;

        vm.eventoTipoList = [];

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
            eventoTipoService.selecionarTodos()
                .success(success)
                .catch(fail);

            function success(response) {
                if (response)
                    vm.eventoTipoList = response;
                else
                    toastr.info('Nenhum tipo de evento cadastrado');
            }

            function fail(error) {
                toastr.error("Erro ao conectar com o servidor");
            }
        }

        vm.animationsEnabled = true;

        function openModal(eventoTipoId, size, parentSelector) {
            var parentElem = parentSelector ?
              angular.element($document[0].querySelector('.modal-demo ' + parentSelector)) : undefined;
            var modalInstance = $uibModal.open({
                animation: vm.animationsEnabled,
                ariaLabelledBy: 'modal-title',
                ariaDescribedBy: 'modal-body',
                templateUrl: 'EventoTipoModal.html',
                controller: 'EventoTipoModalCtrl',
                controllerAs: 'vm',
                size: size,
                appendTo: parentElem,
                resolve: {
                    eventoTipoId: function () {
                        return eventoTipoId;
                    }
                }
            });

            modalInstance.result.then(function (eventoTipoId) {
                excluir(eventoTipoId);
            });
        }

        function excluir(eventoTipoId) {
            eventoTipoService.excluir(eventoTipoId)
                .success(success)
                .catch(fail);

            function success(response) {
                selecionarTodos();
                toastr.success("Tipo de Evento excluído com sucesso", "SUCESSO");
            }

            function fail(error) {
                toastr.error("Erro ao excluir tipo de evento", "ERRO");
                console.log(error);
            }
        }

    }
})();