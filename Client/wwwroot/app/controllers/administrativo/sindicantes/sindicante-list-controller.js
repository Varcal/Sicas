(function () {
    'use strict';

    angular.module('app').controller('sindicanteListController', sindicanteListController);

    sindicanteListController.$inject = ['$uibModal', 'DTOptionsBuilder', 'DTColumnDefBuilder', 'sindicanteService'];

    function sindicanteListController($uibModal, DTOptionsBuilder, DTColumnDefBuilder, sindicanteService) {
        var vm = this;

        vm.sindicantes = [];


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
            sindicanteService.selecionarTodos()
                .success(success)
                .catch(fail);

            function success(response) {
                vm.sindicantes = response;
            }

            function fail(error) {
                toastr.error("Erro ao selecionar sindicantes", "ERRO");
                console.log(error);
            }
        }

        vm.animationsEnabled = true;

        function openModal(sindicante, size, parentSelector) {
            var parentElem = parentSelector ?
              angular.element($document[0].querySelector('.modal-demo ' + parentSelector)) : undefined;
            var modalInstance = $uibModal.open({
                animation: vm.animationsEnabled,
                ariaLabelledBy: 'modal-title',
                ariaDescribedBy: 'modal-body',
                templateUrl: 'sindicanteModal.html',
                controller: 'SindicanteModalCtrl',
                controllerAs: 'vm',
                size: size,
                appendTo: parentElem,
                resolve: {
                    sindicante: function () {
                        return sindicante;
                    }
                }
            });

            modalInstance.result.then(function (sindicante) {
                excluir(sindicante);
            });
        }

        function excluir(sindicante) {
            sindicanteService.excluir(sindicante)
                .success(success)
                .catch(fail);

            function success(response) {
                selecionarTodos();
                toastr.success("Sindicante excluído com sucesso", "SUCESSO");
            }

            function fail(error) {
                toastr.error("Erro ao excluir sindicante", "ERRO");
                console.log(error);
            }
        }
    }
})();
