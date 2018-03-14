(function () {

    angular.module('app').controller('reciboModalCtrl', reciboModalCtrl);

    reciboModalCtrl.$inject = ['$uibModalInstance', 'lancamentos'];

    function reciboModalCtrl($uibModalInstance, lancamentos) {
        var vm = this;

        vm.lancamentos = lancamentos;


       vm.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        };
    }

})();