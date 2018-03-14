(function () {

    angular.module('app').controller('HomeModalCtrl', HomeModalCtrl);

    HomeModalCtrl.$inject = ['$uibModalInstance'];

    function HomeModalCtrl($uibModalInstance) {
        var vm = this;

        
        vm.ok = function () {
            $uibModalInstance.close();
        };

    }
})();