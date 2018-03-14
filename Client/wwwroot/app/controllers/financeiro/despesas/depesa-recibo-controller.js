(function() {
    "use strict";

    angular.module("app").controller("despesaReciboController", despesaReciboController);

    despesaReciboController.$inject = ["$rootScope","$window","seguradoraService", "despesaService", "processoService","SETTINGS"];

    function despesaReciboController($rootScope, $window, seguradoraService, despesaService, processoService, SETTINGS) {
        var vm = this;

        vm.processos = [];
        vm.seguradoras = [];
        vm.desabilitaBtn = true;
        
        vm.habilitarBtnPesquisa = habilitarBtnPesquisa;
        vm.pesquisar = pesquisar;
        vm.emitirRecibo = emitirRecibo;

        activate();


        function activate() {
            carregarSeguradoras();
        }

        function carregarSeguradoras() {
            seguradoraService.selecionarTodos()
                .success(success)
                .catch(fail);

            function success(response) {
                vm.seguradoras = response;
            }

            function fail(error) {
                toastr.error("Erro ao carregar seguradoras", "ERRO");
                console.log(error);
            }
        }

        function habilitarBtnPesquisa() {
            if (vm.seguradoraId) {
                vm.desabilitaBtn = false;
            }
            else {
                vm.desabilitaBtn = true;
            }
        }

        function pesquisar() {         
            processoService.selecionarPorSeguradoraData(vm.seguradoraId, vm.dataInicio, vm.dataFim)
                .success(success)
                .catch(fail);
            
            function success(response) {
                if (response.length <= 0) {
                    toastr.info('Nenhum processo encontrado', "INFORMAÇÂO");
                }
                vm.processos = response;
            }

            function fail() {
                toastr.error("Erro ao carregar processos", "ERRO");
            }
        }

        function emitirRecibo(id) {
            $window.open(SETTINGS.SERVICE_URL + "api/despesa/emitirRecibo/" + id + "?access_token=" + $rootScope.token, "_blank");
            
            pesquisar();
        }
    }

})();