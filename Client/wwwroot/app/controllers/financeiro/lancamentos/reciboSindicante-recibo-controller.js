(function () {
    "use strict";

    angular.module("app").controller("reciboSindicanteReciboController", reciboSindicanteReciboController);

    reciboSindicanteReciboController.$inject = ["$uibModal", "$rootScope", "$window", "sindicanteService", "reciboSindicanteService", "SETTINGS"];

    function reciboSindicanteReciboController($uibModal, $rootScope, $window, sindicanteService, reciboSindicanteService, SETTINGS) {
        var vm = this;

        vm.recibos = [];
        vm.sindicantes = [];
        vm.desabilitaBtn = true;
        vm.habilitaBtnGerarTodos = false;

        vm.habilitarBtnPesquisa = habilitarBtnPesquisa;
        vm.pesquisar = pesquisar;
        vm.emitirRecibo = emitirRecibo;
        vm.emitirTodos = emitirTodos;

        activate();


        function activate() {
            carregarSindicantes();
        }


        function carregarSindicantes() {
            sindicanteService.selecionarTodos()
                .success(success)
                .catch(fail);

            function success(response) {
                vm.sindicantes = response;
            }

            function fail(error) {
                toastr.error("Erro ao carregar seguradoras", "ERRO");
                console.log(error);
            }
        }

        function habilitarBtnPesquisa() {
            if (vm.sindicanteId) {
                vm.desabilitaBtn = false;
            } else {
                vm.desabilitaBtn = true;
            }
        }

        function pesquisar() {
            reciboSindicanteService.selecionarReciboPorSindicanteData(vm.dataInicio, vm.dataFim)
                .success(success)
                .catch(fail);

            function success(response) {
                if (response.length <= 0) {
                    toastr.info('Nenhum encontrado encontrado', "INFORMAÇÂO");
                }

                vm.recibos = response;

                habilitarBtnGerarTodos();
            }

            function fail() {
                toastr.error("Erro ao carregar processos", "ERRO");
            }
        }

        function emitirRecibo(id) {
            $window.open(SETTINGS.SERVICE_URL +
                "api/reciboSindicante/emitirRecibo/" +
                id +
                "?access_token=" +
                $rootScope.token,
                "_blank");
            setTimeout(pesquisar(), 2000);
        }

        function habilitarBtnGerarTodos() {

            var count = 0;

            angular.forEach(vm.recibos,
                function(value, key) {
                    if (!value.emitido) {
                        count++;
                    }
                });


            if (count > 1) {
                vm.habilitaBtnGerarTodos = true;
            } else {
                vm.habilitaBtnGerarTodos = false;
            }
        }

        function emitirTodos() {
            reciboSindicanteService.emitirTodos(vm.dataInicio, vm.dataFim)
                .success(success)
                .catch(fail);

            function success(data, status, headers) {
                headers = headers();

                var filename = headers['x-filename'];
                var contentType = headers['content-type'];
                var linkElement = document.createElement('a');

                try {
                    var blob = new Blob([data], { type: contentType });
                    var url = window.URL.createObjectURL(blob);

                    linkElement.setAttribute('href', url);
                    linkElement.setAttribute("download", filename);

                    var clickEvent = new MouseEvent("click", {
                        "view": window,
                        "bubbles": true,
                        "cancelable": false
                    });
                    linkElement.dispatchEvent(clickEvent);
                    
                    pesquisar();
                } catch (ex) {
                    toastr.error("Erro ao gerar recibos", "ERRO");
                    console.log(ex);
                }
            }

            function fail(error) {
                toastr.error("Erro ao gerar recibos", "ERRO");
                console.log(error);
            }
        }
    }
})();