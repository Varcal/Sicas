(function () {

    angular.module('app').controller('ProcessoModalCtrl', ProcessoModalCtrl);

    ProcessoModalCtrl.$inject = ['$uibModalInstance', 'processoId'];

    function ProcessoModalCtrl($uibModalInstance, processoId) {
        var vm = this;

        vm.processoId = processoId;


        vm.ok = function () {
            $uibModalInstance.close(vm.processoId);
        };

        vm.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        };
    }
})();