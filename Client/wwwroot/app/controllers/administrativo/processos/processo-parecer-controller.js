(function () {
    'use strict';
    
    angular.module('app').controller('processoParecerController', processoParecerController);

    processoParecerController.$inject = ['$location', '$routeParams', '$filter', 'processoService', 'enderecoService'];

    function processoParecerController($location, $routeParams, $filter, processoService, enderecoService) {
        var vm = this;

        var id = $routeParams.id;

        vm.notificacoes = [];
        vm.situacaoList = [{ id: 1, nome: 'Regular' }, { id: 2, nome: 'Irregular' }, { id: 3, nome: 'A Critério' }];
        vm.extensionInvalid = false;
        vm.nomeArquivo = 'Nenhum arquivo selecionado';
        vm.loading = false;

        activate();


        vm.salvar = salvar;
        vm.verfificarExtensao = verfificarExtensao;
        

        function activate() {
            obterParaParecer();
            angular.element("#seguradora").focus();
        }


        function salvar() {
            vm.loading = true;
            vm.processo.arquivo = document.getElementById("arquivos").files[0];
            processoService.finalizarParecer(vm.processo)
                .success(success)
                .catch(fail);

            function success(response) {
                toastr.success("Parecer finalizado com sucesso", "SUCESSO");
                vm.loading = false;
                $location.path('/processo');

            }

            function fail(error) {
                if (error.data) {
                    vm.notificacoes = error.data.errors;
                }
                toastr.error("Não foi possível finalizar o parecer", "ERRO");
                console.log(error);
                vm.loading = false;
            }
        }

        function obterParaParecer() {
            processoService.obterParaParecer(id)
                .success(success)
                .catch(fail);

            function success(response) {
                vm.processo = response;
                vm.processo.dataCadastro = $filter('date')(response.dataCadastro, "dd/MM/yyyy");
            }

            function fail(error) {

                toastr.error("Erro ao obter processo", "Erro");
                console.log(error);
            }
        }

        function verfificarExtensao() {

            var extensoesPermitidas = new Array(".rar", ".zip");
            var extensao = (vm.arquivo.substring(vm.arquivo.lastIndexOf("."))).toLowerCase();
            vm.nomeArquivo = (vm.arquivo.substring(vm.arquivo.lastIndexOf("\\"))).replace("\\","");

            for (var i = 0; i < extensoesPermitidas.length; i++) {
                if (extensoesPermitidas[i] === extensao) {
                    vm.invalidExtension = false;
                    return;
                }
            }

            vm.invalidExtension = true;
        }     
    }
})();