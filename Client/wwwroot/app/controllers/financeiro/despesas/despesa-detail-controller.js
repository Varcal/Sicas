(function() {
    "use strict";

    angular.module("app").controller("despesaDetailController", despesaDetailController);

    despesaDetailController.$inject = ["$uibModal","$routeParams","despesaService"];

    function despesaDetailController($uibModal,$routeParams, despesaService) {
        var vm = this;

        var id = $routeParams.id;

        vm.processo = [];
        vm.totalValorKm = 0;
        vm.totalTransportePedagio = 0;
        vm.totalEstadiaAlimentacao = 0;
        vm.total = 0;

        vm.openModal = openModal;

        activate();


        function activate() {
            obterPorId();
        }

        function obterPorId() {
            despesaService.obterPorId(id)
                .success(success)
                .catch(fail);

            function success(response) {
                vm.processo = response;


                angular.forEach(response.despesas, function (value, key) {
                    vm.totalValorKm += value.valorKm;
                    vm.totalTransportePedagio += value.pedagioTransporte;
                    vm.totalEstadiaAlimentacao += value.estadiaAlimentacao;
                });

                vm.total = vm.totalValorKm + vm.totalTransportePedagio + vm.totalEstadiaAlimentacao;
            }

            function fail(error) {
                toastr.error("Erro ao carregar processo", "ERRO");
                console.log(error);
            }
        }

        function openModal(despesasAdicionais, size, parentSelector) {
            var parentElem = parentSelector ?
              angular.element($document[0].querySelector('.modal-demo ' + parentSelector)) : undefined;
            var modalInstance = $uibModal.open({
                animation: vm.animationsEnabled,
                ariaLabelledBy: 'modal-title',
                ariaDescribedBy: 'modal-body',
                templateUrl: 'despesaAdicionalModal.html',
                controller: 'despesaAdicionalModalCtrl',
                controllerAs: 'vm',
                size: size,
                appendTo: parentElem,
                resolve: {
                    despesasAdicionais: function () {
                        return despesasAdicionais;
                    }
                }
            });

            modalInstance.result.then(function () {
                
            });
        }
    }

})();