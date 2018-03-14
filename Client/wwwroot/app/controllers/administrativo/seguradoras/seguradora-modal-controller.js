(function () {

    angular.module('app').controller('SeguradoraModalCtrl', SeguradoraModalCtrl);

    SeguradoraModalCtrl.$inject = ['$uibModalInstance', 'seguradoraId'];

    function SeguradoraModalCtrl($uibModalInstance, seguradoraId) {
        var vm = this;

        vm.seguradoraId = seguradoraId;


        vm.ok = function () {
            $uibModalInstance.close(vm.seguradoraId);
        };

        vm.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        };
    }
})();