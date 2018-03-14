(function () {
    'use strict';

    angular.module('app').controller('processoEditController', processoEditController);

    processoEditController.$inject = ['$location', '$routeParams','$filter', 'seguradoraService', 'servicoSeguradoraService', 'sindicanteService', 'servicoSindicanteService', 'processoService','enderecoService'];

    function processoEditController($location, $routeParams, $filter, seguradoraService, servicoSeguradoraService, sindicanteService, servicoSindicanteService, processoService, enderecoService) {
        var vm = this;

        var id = $routeParams.id;

        vm.seguradoras = [];
        vm.notificacoes = [];
        vm.servicoSeguradoraList = [];
        vm.eventoTipoList = [];
        vm.servicoSindicanteList = [];
        vm.sindicanteList = [];
        vm.desabilitada = true;
        vm.desabilitaDropEvento = true;
        vm.desabilitaDropServicoSindicante = true;
        vm.parecerHabilitado = false;
        vm.loading = false;
        vm.desabilitaCidade = true;
        vm.desabilitaNovaPesquisa = true;
        vm.desabilitaCidadeFatos = true;
        vm.desabilitaCidadeProcesso = true;
        vm.desabilitaNovaPesquisaFatos = true;
        vm.desabilitadaFatos = true;

        vm.processo = {
            localFatos: {
                estado: {
                    id: 0,
                    nome: "",
                    uf:""
                }
            },
            enderecoSegurado: {
                estado: {
                    id: 0,
                    nome: "",
                    uf: ""
                }
            }
        }

        
        activate();


        vm.habilitar = habilitar;
        vm.salvar = salvar;
        vm.carregarEventoTipo = carregarEventoTipo;
        vm.carregarServicoSindicante = carregarServicoSindicante;
        vm.habilitarParecer = habilitarParecer;
        vm.enviar = enviar;
        //vm.pesquisarEnderecoPorCep = pesquisarEnderecoPorCep;
        //vm.novaPesquisa = novaPesquisa;
        //vm.carregarDropCidade = carregarDropCidade;
        //vm.pesquisarEnderecoPorCepFatos = pesquisarEnderecoPorCepFatos;
        //vm.novaPesquisaFatos = novaPesquisaFatos;
        //vm.carregarDropCidadeFatos = carregarDropCidadeFatos;
        vm.carregarDropCidadeProcesso = carregarDropCidadeProcesso;
        vm.verfificarExtensao = verfificarExtensao;


        function activate() {
            //buscarEstados();
            //buscarEstadosFatos();
            buscarEstadosProcesso();
            angular.element("#seguradora").focus();
        }

        function habilitar() {
            vm.desabilitada = false;
            obterPorId();
        }

        function salvar() {
            vm.loading = true;
            processoService.editar(vm.processo)
                .success(success)
                .catch(fail);

            function success(response) {
               toastr.success("Processo alterado com sucesso", "SUCESSO");
               vm.loading = false;
               $location.path('/processo');
                
            }

            function fail(error) {
                if (error.data) {
                    vm.notificacoes = error.data.errors;
                }
                toastr.error("Não foi possível alterrar o processo", "ERRO");
                console.log(error);
                vm.loading = false;
            }
        }

        function obterPorId() {
            processoService.obterPorId(id)
                .success(success)
                .catch(fail);

            function success(response) {

                vm.processo = response;
                vm.processo.dataCadastro = $filter('date')(response.dataCadastro, "dd/MM/yyyy");
                carregarSeguradoras();
                carregarServicosSeguradoras();
                carregarSindicantes();
                //carregamentoInicialEnderecoSegurado(response.segurado);
                //carregamentoInicialLocalFatos(response.localFatos);
                carregarDropEstadoProcessoInicial();


                if (!vm.processo.sindicanteEnviado)
                    carregarSindicanteExterno();
            }

            function fail(error) {
                
                toastr.error("Erro ao obter processo", "Erro");
                console.log(error);
            }
        }

        //function carregamentoInicialEnderecoSegurado(response) {
        //    vm.processo.segurado.enderecoSegurado = response.enderecoSegurado;

        //    for (var i = 0; i < vm.estados.length; i++) {
        //        if (vm.estados[i].uf === vm.processo.segurado.enderecoSegurado.estado.uf) {
        //            vm.processo.segurado.enderecoSegurado.estado = vm.estados[i];
        //            break;
        //        }
        //    }

        //    enderecoService.selecionarCidades(vm.processo.segurado.enderecoSegurado.estado.id)
        //        .success(success);

        //    function success(response) {
        //        vm.cidades = response;
        //        for (var i = 0; i < vm.cidades.length; i++) {
        //            if (vm.cidades[i].nome === vm.processo.segurado.enderecoSegurado.cidade.nome) {
        //                vm.processo.segurado.enderecoSegurado.cidade = vm.cidades[i];
        //                break;
        //            }
        //        }
        //    }


        //    vm.desabilitaNovaPesquisaOrigem = false;
        //    vm.desabilitadaOrigem = true;
        //}

        function carregamentoInicialLocalFatos(response) {
            vm.processo.localFatos = response;

            for (var i = 0; i < vm.estadosFatos.length; i++) {
                if (vm.estadosFatos[i].uf === vm.processo.localFatos.estado.uf) {
                    vm.processo.localFatos.estado = vm.estadosFatos[i];
                    break;
                }
            }

            enderecoService.selecionarCidades(vm.processo.localFatos.estado.id)
                .success(success);

            function success(response) {
                vm.cidadesFatos = response;
                for (var i = 0; i < vm.cidadesFatos.length; i++) {
                    if (vm.cidadesFatos[i].nome === vm.processo.localFatos.cidade.nome) {
                        vm.processo.localFatos.cidade = vm.cidadesFatos[i];
                        break;
                    }
                }
            }


            vm.desabilitaNovaPesquisaDestino = false;
            vm.desabilitadaDestino = true;
        }

        function carregarSeguradoras() {
            seguradoraService.selecionarTodos()
                .success(success)
                .catch(fail);

            function success(response) {
                vm.seguradoras = response;

                for (var i = 0; i < vm.seguradoras.length; i++) {
                    if (vm.seguradoras[i].id === vm.processo.seguradoraId) {
                        vm.processo.seguradora = vm.seguradoras[i];
                        break;
                    }
                }
            }

            function fail(error) {
                toastr.error("Erro ao selecionar processos", "ERRO");
                console.log(error);
            }
        }

        function carregarServicosSeguradoras() {
            servicoSeguradoraService.selecionarTodosComEventos()
                .success(success)
                .catch(fail);

            function success(response) {

                vm.servicoSeguradoraList = response;

                for (var i = 0; i < vm.servicoSeguradoraList.length; i++) {
                    if (vm.servicoSeguradoraList[i].id === vm.processo.servicoSeguradoraId) {
                        vm.processo.servicoSeguradora = vm.servicoSeguradoraList[i];
                        break;
                    }
                }

                carregarEventoTipoInicial();
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

        function carregarEventoTipoInicial() {
            if (vm.processo.servicoSeguradora == null) {
                vm.desabilitaDropEvento = true;
                vm.eventoTipoList = [];
                return;
            }

            vm.eventoTipoList = vm.processo.servicoSeguradora.eventosTipos;

            for (var i = 0; i < vm.eventoTipoList.length; i++) {
                if (vm.eventoTipoList[i].id === vm.processo.eventoTipoId) {
                    vm.processo.eventoTipo = vm.eventoTipoList[i];
                    break;
                }
            }

            vm.desabilitaDropEvento = false;

        }

        function carregarSindicantes() {
            sindicanteService.selecionarTodos()
                .success(success)
                .catch(fail);

            function success(response) {
                if (response) {
                    vm.sindicanteList = response;

                    for (var i = 0; i < vm.sindicanteList.length; i++) {
                        if (vm.sindicanteList[i].id === vm.processo.sindicanteId) {
                            vm.processo.sindicante = vm.sindicanteList[i];
                            break;
                        }
                    }

                    carregarServicoSindicanteInicial();

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

        function carregarServicoSindicanteInicial() {
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

                    for (var i = 0; i < vm.servicoSindicanteList.length; i++) {
                        if (vm.servicoSindicanteList[i].id === vm.processo.servicoSindicanteId) {
                            vm.processo.servicoSindicante = vm.servicoSindicanteList[i];
                            break;
                        }
                    }

                } else {
                    toastr.info("Nenhuma serviço encontrado para o sindicante", "INFORMAÇÂO");
                }

            }

            function fail(error) {
                toastr.error('Erro ao carregar serviços do sindicante', "ERRO");
                console.log(error);
            }
        }

        function habilitarParecer() {
            vm.parecerHabilitado = true;
        }

        function carregarSindicanteExterno() {
            sindicanteService.selecionarSindicanteExterno()
                .success(success)
                .catch(fail);

            function success(response) {
                vm.sindicanteExternoList = response;
            }

            function fail(error) {
                toastr.error("Erro ao carregar sindicantes externos", "ERRO");
                console.log(error);
            }
        }

        function enviar() {
            vm.loading = true;
            vm.processo.sindicanteExternoId = vm.sindicanteExterno.id;

            processoService.enviarSindicanteExterno(vm.processo)
                .success(success)
                .catch(fail);

            function success(response) {
                vm.loading = false;
                toastr.success('Processo enviado com sucesso', "SUCESSO");
                obterPorId(id);
            }

            function fail(error) {
                toastr.error("Erro ao enviar processo", "ERRO");
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

        function buscarCidadesProcesso() {
            enderecoService.selecionarCidades(vm.processo.estado.id)
                .success(success)
                .catch(fail);

            function success(response) {
                vm.cidadesProcesso = response;
                carregarDropCidadeProcessoInicial();
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
                obterPorId();
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
            if (vm.processo.estado === null) {
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
      }

})();