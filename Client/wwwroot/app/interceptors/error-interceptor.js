(function() {
    'use strict';

    angular.module('app').factory('errorInterceptor', errorInterceptor);

    errorInterceptor.$inject = ['$q', '$location',"$rootScope"];

    function errorInterceptor($q, $location, $rootScope) {
        return {
            responseError: responseError
        }

        function responseError(rejection) {
            if (rejection.status === 401) {
                $rootScope.user = null;
                sessionStorage.clear();
                $location.path('/login');
                return $q.reject(rejection);
            }            
             return $q.reject(rejection);
        }
    }

})();