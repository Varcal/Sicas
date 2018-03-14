(function() {

    angular.module('app').controller('UsuarioModalCtrl', UsuarioModalCtrl);

    UsuarioModalCtrl.$inject = ['$uibModalInstance', 'usuarioId'];

    function UsuarioModalCtrl($uibModalInstance, usuarioId) {
        var vm = this;

        vm.usuarioId = usuarioId;
        

        vm.ok = function () {
            $uibModalInstance.close(vm.usuarioId);
        };

        vm.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        };
    }

})();