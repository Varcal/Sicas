(function () {
    'use strict';

    angular.module('app').controller('loginController', loginController);

    loginController.$inject = ['$scope','$rootScope', '$location', 'SETTINGS', 'accountService','Profile'];

    function loginController($scope, $rootScope, $location, SETTINGS, accountService, Profile) {
        var vm = this;
       
        vm.loading = false;
        

        vm.usuario = {
            login: '',
            password: ''
        }

        vm.submit = login;
        

        activate();

        function activate() {
            year();
        }

        function login() {
            vm.loading = true;
            accountService.login(vm.usuario)
                .success(success)
                .catch(fail);

            function success(response) {
                $rootScope.token = response.access_token;
                sessionStorage.setItem(SETTINGS.AUTH_TOKEN, response.access_token);
                          
                $rootScope.header = {
                    headers: {
                        'Authorization': 'Bearer ' + $rootScope.token
                    }
                }

                accountService.obterUsuario(vm.usuario, $rootScope.header)
                    .success(success)
                    .catch(fail);

                function success(response) {
                    $rootScope.user = response;
                    sessionStorage.setItem(SETTINGS.AUTH_USER, JSON.stringify(response));
                    $rootScope.roles = response.perfis;
                    $rootScope.profile = Profile;
                    $location.path('/');
                }

                function fail(error) {
                    vm.usuario = {};
                    console.log(error);
                    toastr.error("Usuário e/ou senha inválido", "Falha na autenticação");
                    vm.loading = false;
                }
            }

            function fail(error) {
                vm.usuario = {};
                console.log(error);
                toastr.error("Usuário e/ou senha inválido", "Falha na autenticação");
                vm.loading = false;
            }

            
        }

        function year() {
            vm.ano = new Date().getYear() + 1900;
        }
    };
})();