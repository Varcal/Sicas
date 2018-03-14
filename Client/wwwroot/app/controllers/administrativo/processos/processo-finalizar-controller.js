(function () {
    'use strict';

    angular.module('app').controller('processoFinalizarController', processoFinalizarController);

    processoFinalizarController.$inject = ['$location', '$routeParams', '$filter', 'processoService', 'enderecoService'];

    function processoFinalizarController($location, $routeParams, $filter, processoService, enderecoService) {
        var vm = this;

        var id = $routeParams.id;

        vm.notificacoes = [];
        vm.situacaoList = [{ id: 1, nome: 'Regular' }, { id: 2, nome: 'Irregular' }, { id: 3, nome: 'A Critério' }];
        vm.extensionInvalid = false;
        vm.nomeArquivo = 'Nenhum arquivo selecionado';
        vm.loading = false;

        activate();


        vm.finalizar = finalizar;
        vm.verfificarExtensao = verfificarExtensao;


        function activate() {
            buscarEstados();
            buscarEstadosFatos();
            buscarEstadosProcesso();
            angular.element("#seguradora").focus();
        }

        function finalizar() {
            processoService.finalizar(vm.processo)
                .success(success)
                .catch(fail);

            function success(response) {
                toastr.success("Ptrocesso finalizado com sucesso", "SUCESSO");
                $location.path("/despesa");
            }

            function fail(error) {
                toastr.error("Erro ao finalizar processo", "ERRO");
                console.log(error);
            }
        }

        function obterParaParecer() {
            processoService.obterParaParecer(id)
                .success(success)
                .catch(fail);

            function success(response) {
                vm.processo = response;
                vm.processo.dataCadastro = $filter('date')(response.dataCadastro, "dd/MM/yyyy");
                pesquisarEnderecoPorCep();
                pesquisarEnderecoPorCepFatos();
                carregarDropEstadoProcessoInicial();
            }

            function fail(error) {

                toastr.error("Erro ao obter processo", "Erro");
                console.log(error);
            }
        }

        function pesquisarEnderecoPorCep() {
            enderecoService.pesquisarEnderecoPorCep(vm.processo.segurado.enderecoSegurado.cep)
                .success(success)
                .catch(fail);

            function success(response) {
                if (response) {
                    vm.processo.segurado.enderecoSegurado.logradouro = response.logradouro;
                    vm.processo.segurado.enderecoSegurado.bairro = response.bairro;


                    for (var i = 0; i < vm.estados.length; i++) {
                        if (vm.estados[i].uf === response.estado) {
                            vm.processo.segurado.enderecoSegurado.estado = vm.estados[i];
                            break;
                        }
                    }

                    enderecoService.selecionarCidades(vm.processo.segurado.enderecoSegurado.estado.id)
                        .success(success)
                        .catch(fail);

                    function success(cidades) {
                        vm.cidades = cidades;
                        for (var i = 0; i < vm.cidades.length; i++) {
                            if (vm.cidades[i].nome === response.cidade) {
                                vm.processo.segurado.enderecoSegurado.cidade = vm.cidades[i];
                                break;
                            }
                        }

                        vm.desabilitaNovaPesquisa = false;
                        vm.desabilitada = true;
                    }

                    function fail(error) {
                        console.log(error);
                    }
                }
            }

            function fail() {
                toastr.error("Preencha manualmente ou tente outro CEP", "CEP " + vm.processo.segurado.enderecoSegurado.cep + " Inválido");
                limparEndereco();
                vm.desabilitada = false;
                vm.desabilitaNovaPesquisa = false;
            }
        }

        function buscarCidades() {
            enderecoService.selecionarCidades(vm.processo.segurado.enderecoSegurado.estado.id)
                .success(success)
                .catch(fail);

            function success(response) {
                return vm.cidades = response;
            }

            function fail(error) {
                toastr.error("Ocorreu um erro inesperado tente novamente", "ERRO")
                console.log(error);
            }

        }

        function buscarEstados() {
            enderecoService.selecionarEstados()
                .success(success)
                .catch(fail);

            function success(response) {
                vm.estados = response;
            }

            function fail(error) {
                console.log(error);
            }
        }

        function carregarDropCidade() {
            if (vm.processo.segurado.enderecoSegurado.estado) {
                vm.cidades = buscarCidades(vm.processo.segurado.enderecoSegurado.estado.id);
                vm.desabilitaCidade = false;
            } else {
                vm.processo.segurado.enderecoSegurado.cidade = {};
                vm.desabilitaCidade = true;
            }
        }

        function limparEndereco() {
            vm.processo.segurado.enderecoSegurado.logradouro = "";
            vm.processo.segurado.enderecoSegurado.bairro = "";
            vm.processo.segurado.enderecoSegurado.cidade = {};
            vm.processo.segurado.enderecoSegurado.estado = {};
        }

        function pesquisarEnderecoPorCepFatos() {
            enderecoService.pesquisarEnderecoPorCep(vm.processo.localFatos.cep)
                .success(success)
                .catch(fail);

            function success(response) {
                if (response) {
                    vm.processo.localFatos.logradouro = response.logradouro;
                    vm.processo.localFatos.bairro = response.bairro;


                    for (var i = 0; i < vm.estadosFatos.length; i++) {
                        if (vm.estadosFatos[i].uf === response.estado) {
                            vm.processo.localFatos.estado = vm.estadosFatos[i];
                            break;
                        }
                    }

                    enderecoService.selecionarCidades(vm.processo.localFatos.estado.id)
                        .success(success)
                        .catch(fail);

                    function success(cidades) {
                        vm.cidadesFatos = cidades;
                        for (var i = 0; i < vm.cidadesFatos.length; i++) {
                            if (vm.cidadesFatos[i].nome === response.cidade) {
                                vm.processo.localFatos.cidade = vm.cidadesFatos[i];
                                break;
                            }
                        }

                        vm.desabilitaNovaPesquisaFatos = false;
                        vm.desabilitadaFatos = true;
                    }

                    function fail(error) {
                        console.log(error);
                    }
                }
            }

            function fail() {
                toastr.error("Preencha manualmente ou tente outro CEP", "CEP " + vm.processo.localFatos.cep + " Inválido");
                limparEnderecoFatos();
                vm.desabilitadaFatos = false;
                vm.desabilitaNovaPesquisaFatos = false;
            }
        }

        function buscarCidadesFatos() {
            enderecoService.selecionarCidades(vm.processo.localFatos.estado.id)
                .success(success)
                .catch(fail);

            function success(response) {
                return vm.cidadesFatos = response;
            }

            function fail(error) {
                toastr.error("Ocorreu um erro inesperado tente novamente", "ERRO")
                console.log(error);
            }

        }

        function buscarEstadosFatos() {
            enderecoService.selecionarEstados()
                .success(success)
                .catch(fail);

            function success(response) {
                vm.estadosFatos = response;
            }

            function fail(error) {
                console.log(error);
            }
        }

        function carregarDropCidadeFatos() {
            if (vm.processo.localFatos.estado) {
                vm.cidadesFatos = buscarCidadesFatos(vm.processo.localFatos.estado.id);
                vm.desabilitaCidadeFatos = false;
            } else {
                vm.processo.localFatos.cidade = {};
                vm.desabilitaCidadeFatos = true;
            }
        }

        function limparEnderecoFatos() {
            vm.processo.localFatos.logradouro = "";
            vm.processo.localFatos.bairro = "";
            vm.processo.localFatos.cidade = {};
            vm.processo.localFatos.estado = {};
        }

        function buscarCidadesProcesso() {
            enderecoService.selecionarCidades(vm.processo.estado.id)
                .success(success)
                .catch(fail);

            function success(response) {
                vm.cidadesProcesso = response;
                carregarDropCidadeProcessoInicial();
            }

            function fail(error) {
                toastr.error("Ocorreu um erro inesperado tente novamente", "ERRO")
                console.log(error);
            }

        }

        function buscarEstadosProcesso() {
            enderecoService.selecionarEstados()
                .success(success)
                .catch(fail);

            function success(response) {
                vm.estadosProcesso = response;
                obterParaParecer();
            }

            function fail(error) {
                console.log(error);
            }
        }

        function carregarDropCidadeProcesso() {
            if (vm.processo.estado) {
                buscarCidadesProcesso(vm.processo.estado.id);
                vm.desabilitaCidadeProcesso = false;
            } else {
                vm.processo.cidade = {};
                vm.desabilitaCidadeProcesso = true;
            }
        }

        function carregarDropCidadeProcessoInicial() {
            if (vm.processo.cidade == null) {
                return;
            }

            for (var i = 0; i < vm.cidadesProcesso.length; i++) {
                if (vm.cidadesProcesso[i].id === vm.processo.cidade.id) {
                    vm.processo.cidade = vm.cidadesProcesso[i];
                    break;
                }

                vm.desabilitaCidadeProcesso = false;
            }
        }

        function carregarDropEstadoProcessoInicial() {
            if (vm.processo.estado == null) {
                return;
            }

            for (var i = 0; i < vm.estadosProcesso.length; i++) {
                if (vm.estadosProcesso[i].id === vm.processo.estado.id) {
                    vm.processo.estado = vm.estadosProcesso[i];
                    break;
                }
            }

            buscarCidadesProcesso();
        }

        function verfificarExtensao() {

            var extensoesPermitidas = new Array(".rar", ".zip");
            var extensao = (vm.arquivo.substring(vm.arquivo.lastIndexOf("."))).toLowerCase();
            vm.nomeArquivo = (vm.arquivo.substring(vm.arquivo.lastIndexOf("\\"))).replace("\\", "");

            for (var i = 0; i < extensoesPermitidas.length; i++) {
                if (extensoesPermitidas[i] == extensao) {
                    vm.invalidExtension = false;
                    converToBase64();
                    return;
                }
            }

            vm.invalidExtension = true;
        }

        function converToBase64() {
            var file = document.getElementById("arquivos").files[0];
            var reader = new FileReader();

            //Event handler
            reader.addEventListener("load", function () {
                vm.processo.arquivo = reader.result;
            }, false);

            if (file) {
                reader.readAsDataURL(file);
            }
        }
    }
})();