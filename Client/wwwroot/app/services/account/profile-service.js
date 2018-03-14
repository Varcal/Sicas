(function() {
    'use strict';

    angular.module('app').factory('Profile', Profile);
    
    Profile.$inject = ['$rootScope'];
   

    function Profile($rootScope) {      

        return {
            isAdmin: isAdmin,
            isGestor: isGestor,
            isAdministrativo: isAdministrativo,
            isFinanceiro: isFinanceiro,
            isAnalista: isAnalista
        }


        function isAdmin() {
            return contains($rootScope.roles, 'Administrador');
        }

        function isGestor() {
            return contains($rootScope.roles, 'Gestor');
        }

        function isAdministrativo() {
            return contains($rootScope.roles, 'Administrativo');
        }

        function isFinanceiro() {
            return contains($rootScope.roles, 'Financeiro');
        }

        function isAnalista() {
            return contains($rootScope.roles, 'Analista');
        }

        function contains(array, element) {
            return array && array.indexOf(element) > -1;
        }
    }
})();