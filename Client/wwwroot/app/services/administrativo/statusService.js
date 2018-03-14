(function() {

    angular.module("app").factory("statusService", statusService);

    statusService.$inject = ["$http","SETTINGS"];

    function statusService($http, SETTINGS) {
        return {
            selecionarTodos: selecionarTodos
        };

        function selecionarTodos() {
            return $http.get(SETTINGS.SERVICE_URL + "api/status");
        }
    }

})();