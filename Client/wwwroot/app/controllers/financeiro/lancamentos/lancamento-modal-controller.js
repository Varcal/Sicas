(function () {

    angular.module('app').controller('lancamentoModalCtrl', lancamentoModalCtrl);

    lancamentoModalCtrl.$inject = ['$uibModalInstance', 'lancamentoId'];

    function lancamentoModalCtrl($uibModalInstance, lancamentoId) {
        var vm = this;

        vm.lancamentoId = lancamentoId;


        vm.ok = function () {
            $uibModalInstance.close(vm.lancamentoId);
        };

        vm.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        };
    }

})();