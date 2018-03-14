(function () {
    'use strict';

    angular.module('app').controller('eventoTipoEditController', eventoTipoEditController);

    eventoTipoEditController.$inject = ['$routeParams', '$location', 'eventoTipoService'];

    function eventoTipoEditController($routeParams, $location, eventoTipoService) {
        var vm = this;

        var id = $routeParams.id;

        vm.loading = false;
        vm.eventoTipo = {
            id: 0,
            nome: ''
        }

        vm.salvar = salvar;
       
        activate();


        function activate() {
            obterPorId();
        }

        function salvar() {
            vm.loading = true;
            eventoTipoService.alterar(vm.eventoTipo)
                .success(success)
                .catch(fail);

            function success() {
                toastr.success('Tipo de Evento foi alterado!', 'Sucesso');
                limparCampos();
                vm.loading = false;
                $location.path('/eventoTipo');                
            }

            function fail(error) {
                toastr.error('Erro ao alterar o tipo de evento', 'Erro');
                console.log(error);
                vm.loading = false;
            }
        }

        function obterPorId() {
            eventoTipoService.obterPorId(id)
                .success(success)
                .catch(fail);

            function success(response) {
                vm.eventoTipo = response;
            }

            function fail(error) {
                toastr.error('Não foi possível carregar tipo evento', 'ERRO');
                console.log(error);
            }
        }


        function limparCampos() {
            vm.servicoSeguradora = {
                id: 0,
                nome: ''
            }
        }
    }

})();