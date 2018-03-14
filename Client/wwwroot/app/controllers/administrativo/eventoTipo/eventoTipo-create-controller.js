(function() {
    'use strict';

    angular.module('app').controller('eventoTipoCreateController', eventoTipoCreateController);

    eventoTipoCreateController.$inject = ['$location', 'eventoTipoService'];

    function eventoTipoCreateController($location, eventoTipoService) {
        var vm = this;

        vm.loading = false;


        vm.adicionar = adicionar;

        activate();

        function activate() {
            
        }


        function adicionar() {
            vm.loading = true;
            eventoTipoService.registrar(vm.eventoTipo)
                .success(success)
                .catch(fail);

            function success(response) {
                toastr.success("Tipo de evento salvo com sucesso", "SUCESSO");
                vm.loading = false;
                $location.path('/eventoTipo');               
            }

            function fail(error) {
                toastr.error("Erro ao registrar tipo de eventos", "ERRO");
                console.log(error);
                vm.loading = false;
            }
        }

    }


})();