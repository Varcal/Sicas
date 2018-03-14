(function () {
    'use strict';

    angular.module('app').controller('processoCreateController', processoCreateController);

    processoCreateController.$inject = ['$location', 'seguradoraService', 'servicoSeguradoraService', 'sindicanteService', 'servicoSindicanteService', 'processoService', 'enderecoService'];

    function processoCreateController($location, seguradoraService, servicoSeguradoraService, sindicanteService, servicoSindicanteService, processoService, enderecoService) {
        var vm = this;

        vm.placaPattern = /^([A-Z]{3})(-([0-9]{4}))$/;
        vm.numeroPontoPattern = /^[0-9]$/;

        vm.seguradoras = [];
        vm.notificacoes = [];
        vm.servicoSeguradoraList = [];
        vm.eventoTipoList = [];
        vm.servicoSindicanteList = [];
        vm.sindicanteList = [];
        vm.desabilitada = true;
        vm.desabilitaDropEvento = true;
        vm.desabilitaDropServicoSindicante = true;
        vm.loading = false;
        vm.desabilitaCidade = true;
        vm.desabilitaNovaPesquisa = true;
        vm.desabilitaCidadeFatos = true;
        vm.desabilitaCidadeProcesso = true;
        vm.desabilitaNovaPesquisaFatos = true;
        vm.desabilitadaFatos = true;

        vm.processo = {
            sindicante: {},
            servicoSindicante: {},
            servicoSeguradora: {},
            eventoTipo: {},
            numeroReferencia: "",
            usuarioResponsavelId: 0,
            numeroSinistro: "",
            analista: "",
            segurado: "",
            arquivo: null
        }

        activate();

        vm.habilitar = habilitar;
        vm.registrar = registrar;
        vm.carregarEventoTipo = carregarEventoTipo;
        vm.carregarServicoSindicante = carregarServicoSindicante;
        //vm.pesquisarEnderecoPorCep = pesquisarEnderecoPorCep;
        //vm.novaPesquisa = novaPesquisa;
        //vm.carregarDropCidade = carregarDropCidade;
        //vm.pesquisarEnderecoPorCepFatos = pesquisarEnderecoPorCepFatos;
        ////vm.novaPesquisaFatos = novaPesquisaFatos;
        //vm.carregarDropCidadeFatos = carregarDropCidadeFatos;
        vm.carregarDropCidadeProcesso = carregarDropCidadeProcesso;
        vm.verfificarExtensao = verfificarExtensao;


        function activate() {
            //buscarEstados();
            //buscarEstadosFatos();
            buscarEstadosProcesso();
            selecionarSeguradoras();
            carregarTipoServicos();
            carregarSindicantes();
            angular.element("#dataCadastro").focus();
        }

        function habilitar() {
            vm.desabilitada = false;
        }

        function registrar() {
            vm.loading = true;
            processoService.registrar(vm.processo)
                .success(success)
                .catch(fail);

            function success(response) {
                if (response) {
                    vm.notificacoes = response.errors;
                } else {
                    toastr.success("Processo registrado com sucesso", "SUCESSO");
                    $location.path('/processo');
                }

                vm.loading = false;
            }

            function fail(error) {
                toastr.error("Não foi possível registrar o processo", "ERRO");
                console.log(error);
                vm.loading = false;
            }
        }

        function selecionarSeguradoras() {
            seguradoraService.selecionarTodos()
                .success(success)
                .catch(fail);

            function success(response) {
                vm.seguradoras = response;
            }

            function fail(error) {
                toastr.error("Erro ao selecionar processos", "ERRO");
                console.log(error);
            }
        }

        function carregarTipoServicos() {
            servicoSeguradoraService.selecionarTodosComEventos()
                .success(success)
                .catch(fail);

            function success(response) {
                vm.servicoSeguradoraList = response;
            }

            function fail(error) {
                toastr.error("Erro ao carregar tipos de serviço", "ERRO");
                console.log(error);
            }
        }

        function carregarEventoTipo() {
            if (vm.processo.servicoSeguradora == null) {
                vm.desabilitaDropEvento = true;
                vm.eventoTipoList = [];
                return;
            }

            vm.eventoTipoList = vm.processo.servicoSeguradora.eventosTipos;
            vm.desabilitaDropEvento = false;

        }

        function carregarSindicantes() {
            sindicanteService.selecionarTodos()
                .success(success)
                .catch(fail);

            function success(response) {
                if (response) {
                    vm.sindicanteList = response;
                } else {
                    toastr.info("Nenhum sindicante encontrado", "INFORMAÇÃ");
                }
            }

            function fail(error) {
                toastr.error("Erro ao carregar sindicantes", "ERRO");
                console.log(error);
            }
        }

        function carregarServicoSindicante() {
            if (vm.processo.sindicante == null) {
                vm.desabilitaDropServicoSindicante = true;
                vm.servicoSindicanteList = [];
                return;
            }
            servicoSindicanteService.selecionarPorSindicante(vm.processo.sindicante.id)
                .success(success)
                .catch(fail);

            function success(response) {
                if (response) {
                    vm.desabilitaDropServicoSindicante = false;
                    vm.servicoSindicanteList = response;
                } else {
                    toastr.info("Nenhuma serviço encontrado para o sindicante", "INFORMAÇÂO");
                }

            }

            function fail(error) {
                toastr.error('Erro ao carregar serviços do sindicante', "ERRO");
                console.log(error);
            }
        }

        //function pesquisarEnderecoPorCep() {
        //    enderecoService.pesquisarEnderecoPorCep(vm.processo.segurado.enderecoSegurado.cep)
        //        .success(success)
        //        .catch(fail);

        //    function success(response) {
        //        if (response) {
        //            vm.processo.segurado.enderecoSegurado.logradouro = response.logradouro;
        //            vm.processo.segurado.enderecoSegurado.bairro = response.bairro;


        //            for (var i = 0; i < vm.estados.length; i++) {
        //                if (vm.estados[i].uf === response.estado) {
        //                    vm.processo.segurado.enderecoSegurado.estado = vm.estados[i];
        //                    break;
        //                }
        //            }

        //            enderecoService.selecionarCidades(vm.processo.segurado.enderecoSegurado.estado.id)
        //                .success(success)
        //                .catch(fail);

        //            function success(cidades) {
        //                vm.cidades = cidades;
        //                for (var i = 0; i < vm.cidades.length; i++) {
        //                    if (vm.cidades[i].nome === response.cidade) {
        //                        vm.processo.segurado.enderecoSegurado.cidade = vm.cidades[i];
        //                        break;
        //                    }
        //                }

        //                vm.desabilitaNovaPesquisa = false;
        //                vm.desabilitada = true;
        //            }

        //            function fail(error) {
        //                console.log(error);
        //            }
        //        }
        //    }

        //    function fail() {
        //        toastr.error("Preencha manualmente ou tente outro CEP", "CEP " + vm.processo.segurado.enderecoSegurado.cep + " Inválido");
        //        limparEndereco();
        //        vm.desabilitada = false;
        //        vm.desabilitaNovaPesquisa = false;
        //    }
        //}

        //function buscarCidades() {
        //    enderecoService.selecionarCidades(vm.processo.segurado.enderecoSegurado.estado.id)
        //        .success(success)
        //        .catch(fail);

        //    function success(response) {
        //        return vm.cidades = response;
        //    }

        //    function fail(error) {
        //        toastr.error("Ocorreu um erro inesperado tente novamente", "ERRO")
        //        console.log(error);
        //    }

        //}

        //function buscarEstados() {
        //    enderecoService.selecionarEstados()
        //        .success(success)
        //        .catch(fail);

        //    function success(response) {
        //        vm.estados = response;
        //    }

        //    function fail(error) {
        //        console.log(error);
        //    }
        //}

        //function carregarDropCidade() {
        //    if (vm.processo.segurado.enderecoSegurado.estado) {
        //        vm.cidades = buscarCidades(vm.processo.segurado.enderecoSegurado.estado.id);
        //        vm.desabilitaCidade = false;
        //    } else {
        //        vm.processo.segurado.enderecoSegurado.cidade = {};
        //        vm.desabilitaCidade = true;
        //    }
        //}

        //function novaPesquisa() {
        //    vm.processo.segurado.enderecoSegurado.cep = "";
        //    limparEndereco();
        //    vm.desabilitada = true;
        //    vm.desabilitaNovaPesquisa = true;
        //    vm.desabilitaCidade = true;
        //}

        //function limparEndereco() {
        //    vm.processo.segurado.enderecoSegurado.logradouro = "";
        //    vm.processo.segurado.enderecoSegurado.bairro = "";
        //    vm.processo.segurado.enderecoSegurado.cidade = {};
        //    vm.processo.segurado.enderecoSegurado.estado = {};
        //}

        //function pesquisarEnderecoPorCepFatos() {
        //    enderecoService.pesquisarEnderecoPorCep(vm.processo.localFatos.cep)
        //        .success(success)
        //        .catch(fail);

        //    function success(response) {
        //        if (response) {
        //            vm.processo.localFatos.logradouro = response.logradouro;
        //            vm.processo.localFatos.bairro = response.bairro;


        //            for (var i = 0; i < vm.estadosFatos.length; i++) {
        //                if (vm.estadosFatos[i].uf === response.estado) {
        //                    vm.processo.localFatos.estado = vm.estadosFatos[i];
        //                    break;
        //                }
        //            }

        //            enderecoService.selecionarCidades(vm.processo.localFatos.estado.id)
        //                .success(success)
        //                .catch(fail);

        //            function success(cidades) {
        //                vm.cidadesFatos = cidades;
        //                for (var i = 0; i < vm.cidadesFatos.length; i++) {
        //                    if (vm.cidadesFatos[i].nome === response.cidade) {
        //                        vm.processo.localFatos.cidade = vm.cidadesFatos[i];
        //                        break;
        //                    }
        //                }

        //                vm.desabilitaNovaPesquisaFatos = false;
        //                vm.desabilitadaFatos = true;
        //            }

        //            function fail(error) {
        //                console.log(error);
        //            }
        //        }
        //    }

        //    function fail() {
        //        toastr.error("Preencha manualmente ou tente outro CEP", "CEP " + vm.processo.localFatos.cep + " Inválido");
        //        limparEnderecoFatos();
        //        vm.desabilitadaFatos = false;
        //        vm.desabilitaNovaPesquisaFatos = false;
        //    }
        //}

        //function buscarCidadesFatos() {
        //    enderecoService.selecionarCidades(vm.processo.localFatos.estado.id)
        //        .success(success)
        //        .catch(fail);

        //    function success(response) {
        //        return vm.cidadesFatos = response;
        //    }

        //    function fail(error) {
        //        toastr.error("Ocorreu um erro inesperado tente novamente", "ERRO")
        //        console.log(error);
        //    }

        //}

        //function buscarEstadosFatos() {
        //    enderecoService.selecionarEstados()
        //        .success(success)
        //        .catch(fail);

        //    function success(response) {
        //        vm.estadosFatos = response;
        //    }

        //    function fail(error) {
        //        console.log(error);
        //    }
        //}

        //function carregarDropCidadeFatos() {
        //    if (vm.processo.localFatos.estado) {
        //        vm.cidadesFatos = buscarCidadesFatos(vm.processo.localFatos.estado.id);
        //        vm.desabilitaCidadeFatos = false;
        //    } else {
        //        vm.processo.localFatos.cidade = {};
        //        vm.desabilitaCidadeFatos = true;
        //    }
        //}

        //function novaPesquisaFatos() {
        //    vm.processo.localFatos.cep = "";
        //    limparEnderecoFatos();
        //    vm.desabilitadaFatos = true;
        //    vm.desabilitaNovaPesquisaFatos = true;
        //    vm.desabilitaCidadeFatos = true;
        //}

        //function limparEnderecoFatos() {
        //    vm.processo.localFatos.logradouro = "";
        //    vm.processo.localFatos.bairro = "";
        //    vm.processo.localFatos.cidade = {};
        //    vm.processo.localFatos.estado = {};
        //}

        function verfificarExtensao() {

            var extensoesPermitidas = new Array(".zip", ".rar");
            var extensao = (vm.arquivo.substring(vm.arquivo.lastIndexOf("."))).toLowerCase();
            vm.nomeArquivo = (vm.arquivo.substring(vm.arquivo.lastIndexOf("\\"))).replace("\\", "");

            for (var i = 0; i < extensoesPermitidas.length; i++) {
                if (extensoesPermitidas[i] === extensao) {
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
                //console.log(base64);

            }, false);

            //Fires the load event
            if (file) {
                reader.readAsDataURL(file);
            }
        }

        function buscarCidadesProcesso() {
            enderecoService.selecionarCidades(vm.processo.estado.id)
                .success(success)
                .catch(fail);

            function success(response) {
                return vm.cidadesProcesso = response;
            }

            function fail(error) {
                toastr.error("Ocorreu um erro inesperado tente novamente", "ERRO");
                console.log(error);
            }

        }

        function buscarEstadosProcesso() {
            enderecoService.selecionarEstados()
                .success(success)
                .catch(fail);

            function success(response) {
                vm.estadosProcesso = response;
            }

            function fail(error) {
                console.log(error);
            }
        }

        function carregarDropCidadeProcesso() {
            if (vm.processo.estado) {
                vm.cidades = buscarCidadesProcesso(vm.processo.estado.id);
                vm.desabilitaCidadeProcesso = false;
            } else {
                vm.processo.cidade = {};
                vm.desabilitaCidadeProcesso = true;
            }
        }       
    }

})();