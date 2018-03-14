(function () {

    angular.module('app').controller('EventoTipoModalCtrl', EventoTipoModalCtrl);

    EventoTipoModalCtrl.$inject = ['$uibModalInstance', 'eventoTipoId'];

    function EventoTipoModalCtrl($uibModalInstance, eventoTipoId) {
        var vm = this;

        vm.eventoTipoId = eventoTipoId;


        vm.ok = function () {
            $uibModalInstance.close(vm.eventoTipoId);
        };

        vm.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        };
    }
})();