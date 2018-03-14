(function () {

    angular.module('app').controller('SindicanteModalCtrl', SindicanteModalCtrl);

    SindicanteModalCtrl.$inject = ['$uibModalInstance', 'sindicante'];

    function SindicanteModalCtrl($uibModalInstance, sindicante) {
        var vm = this;

        vm.sindicante = sindicante;


        vm.ok = function () {
            $uibModalInstance.close(vm.sindicante);
        };

        vm.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        };
    }
})();