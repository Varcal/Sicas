(function () {

    angular.module('app').controller('despesaAdicionalModalCtrl', despesaAdicionalModalCtrl);

    despesaAdicionalModalCtrl.$inject = ['$uibModalInstance', 'despesasAdicionais'];

    function despesaAdicionalModalCtrl($uibModalInstance, despesasAdicionais) {
        var vm = this;

        vm.despesasAdicionais = despesasAdicionais;


        vm.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        };
    }
})();