(function () {

    angular.module('app').controller('ServicoSeguradoraModalCtrl', ServicoSeguradoraModalCtrl);

    ServicoSeguradoraModalCtrl.$inject = ['$uibModalInstance', 'servicoSeguradoraId'];

    function ServicoSeguradoraModalCtrl($uibModalInstance, servicoSeguradoraId) {
        var vm = this;

        vm.servicoSeguradoraId = servicoSeguradoraId;


        vm.ok = function () {
            $uibModalInstance.close(vm.servicoSeguradoraId);
        };

        vm.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        };
    }
})();