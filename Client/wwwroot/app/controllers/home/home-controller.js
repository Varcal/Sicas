(function() {
    'use strict';

    angular.module('app').controller('homeController', homeController);

    homeController.$inject = ['$rootScope', '$location', '$window', '$uibModal', '$filter', 'Profile', 'usuarioService', 'homeService', 'DTOptionsBuilder', 'DTColumnDefBuilder'];

    function homeController($rootScope, $location, $window, $uibModal, $filter, Profile, usuarioService, homeService, DTOptionsBuilder, DTColumnDefBuilder) {
        var vm = this;

        vm.animationsEnabled = true;
        vm.sindicantes = [];

        vm.alerta = {
            totalAbertoMais5Dias: 0,
            totalAberto3Dias: 0,
            totalAguradandoParecer: 0,
            totalFinalizadosNoMes: 0,
            totalAguardandoEmitirNf: 0,
            totalEnviadoSeguradora:0
        };

        vm.dtOptions = DTOptionsBuilder.newOptions()
            .withPaginationType('full_numbers')
            .withDisplayLength(10);
        vm.dtColumnDefs = [
            DTColumnDefBuilder.newColumnDef(1).notSortable()
        ];

        vm.profile = Profile;
        vm.redirecionar = redirecionar;


        activate();


        function activate() {
            verificarSenhaPadrao();
            mesAno();

            if(Profile.isGestor() || Profile.isAdmin())
                carregarTabelaSindicante();
        }


        function verificarSenhaPadrao() {
            usuarioService.verificarSenhaPadrao($rootScope.user.id)
                .success(success)
                .catch(fail);

            function success(response) {
                if (response) {
                    openModal();
                    return;
                }

                carregarTelaInicial();
            }

            function fail(error) {
                toastr.error("Ocorreu um erro ao tentar verificar a senha", "ERRO");
                console.log(error);
            }
        }


        function carregarTelaInicial() {
            homeService.obterAlerta($rootScope.user.id)
                .success(success)
                .catch(fail);

            function success(response) {
                vm.alerta = response;
            }

            function fail(error) {
                toastr.error("Erro ao consultar alertas", "ERRO");
                console.log(error);
            }
        }

        function carregarTabelaSindicante() {
            homeService.selecionarSindicantes()
                .success(success)
                .catch(fail);

            function success(response) {
                vm.sindicantes = response;
            }

            function fail(error) {
                toastr.error("Erro ao carregar tabela de sindicantes", "ERRO");
                console.log(error);
            }
        }

        function redirecionar(alerta) {
            $window.localStorage.setItem("alerta", alerta);
            $location.path("/processo");
        }


        function openModal(seguradoraId, size, parentSelector) {
            var parentElem = parentSelector ?
              angular.element($document[0].querySelector('.modal-demo ' + parentSelector)) : undefined;
            var modalInstance = $uibModal.open({
                animation: vm.animationsEnabled,
                ariaLabelledBy: 'modal-title',
                ariaDescribedBy: 'modal-body',
                templateUrl: 'HomeModal.html',
                controller: 'HomeModalCtrl',
                controllerAs: 'vm',
                size: size,
                backdrop: 'static',
                keyboard: false,
                appendTo: parentElem
            });

            modalInstance.result.then(function () {
                $location.path('/usuario/alterarSenha');
            });
        }

        function mesAno() {
            var data = new Date();
            var ano = data.getFullYear();
            var mes = padLeft((data.getMonth() + 1), 2);
            vm.data =  [mes, ano].join('/');
        }

        function padLeft(number, length) {

            var my_string = '' + number;
            while (my_string.length < length) {
                my_string = '0' + my_string;
            }

            return my_string;

        }
    }

})();