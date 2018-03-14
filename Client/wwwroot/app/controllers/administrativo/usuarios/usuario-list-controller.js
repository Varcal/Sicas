(function() {
    'use strict';

    //$(document).ready(function() {
    //    $(".container").show();
    //});


    angular.module('app').controller('usuarioListController', usuarioListController);

    usuarioListController.$inject = ['$uibModal', 'usuarioService', 'DTOptionsBuilder', 'DTColumnDefBuilder'];

    function usuarioListController($uibModal, usuarioService, DTOptionsBuilder, DTColumnDefBuilder) {
        var vm = this;

        vm.usuarios = [];

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

            usuarioService.selecionarTodos()
                .success(success);
                //.catch(fail);

            function success(retorno) {
                vm.usuarios = retorno;
            }
         
        }

       
        vm.animationsEnabled = true;

        function openModal (usuarioId, size, parentSelector) {
            var parentElem = parentSelector ?
              angular.element($document[0].querySelector('.modal-demo ' + parentSelector)) : undefined;
            var modalInstance = $uibModal.open({
                animation: vm.animationsEnabled,
                ariaLabelledBy: 'modal-title',
                ariaDescribedBy: 'modal-body',
                templateUrl: 'usuarioModal.html',
                controller: 'UsuarioModalCtrl',
                controllerAs: 'vm',
                size: size,
                appendTo: parentElem,
                resolve: {
                    usuarioId: function () {
                        return usuarioId;
                    }
                }
            });

            modalInstance.result.then(function (usuarioId) {
                excluir(usuarioId);
            });
        };

        function excluir(usuarioId) {
            usuarioService.excluir(usuarioId)
                .success(success)
                .catch(fail);

            function success(response) {
                selecionarTodos();
                toastr.success("Usuário excluído com sucesso", "SUCESSO");
            }

            function fail(error) {
                toastr.error("Erro ao excluir usuário", "ERRO");
                console.log(error);
            }
        }
       
    }

})();