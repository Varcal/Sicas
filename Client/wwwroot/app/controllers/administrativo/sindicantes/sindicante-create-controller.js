(function() {
    'use strict';

    angular.module('app').controller('sindicanteCreateController', sindicanteCreateController);

    sindicanteCreateController.$inject = ['$location','enderecoService', 'sindicanteService', 'bancoService', 'servicoSeguradoraService','servicoSindicanteService'];

    function sindicanteCreateController($location, enderecoService, sindicanteService, bancoService, servicoSeguradoraService, servicoSindicanteService) {
        var vm = this;

        vm.emailPattern = /^([a-zA-Z0-9])+([a-zA-Z0-9._%+-])+@([a-zA-Z0-9_.-])+\.(([a-zA-Z]){2,6})$/;
        vm.agenciaPattern = /^[0-9]+$/;
        vm.rgPattern = /^([0-9]{4,12})(-([xX0-9]{1}))?$/;


        vm.desabilitaCidade = true;
        vm.desabilitaDropEvento = true;
        vm.desabilitada = true;
        vm.desabilitaNovaPesquisa = true;
        vm.loading = false;

        vm.bancoList = [];
        vm.eventoTipoList = [];
        vm.servicoSeguradoraList = [];
        vm.servicoSindicanteList = [];
        vm.ufs = [];

        vm.sindicanteTipoList = [{ id: 1, nome: 'Sindicante Interno' }, { id: 2, nome: 'Sindicante Externo' }];
        vm.contaTipoList = [{ id: 1, nome: 'Conta Corrente' }, { id: 2, nome: 'Conta Poupança' }];

        vm.sindicante = {
            sindicanteTipo: 0,
            endereco: {
                estado: {
                    id: 0,
                    nome: "",
                    uf: ""
                }
            },
            honorarios: []
        };
      


        var honorario = function (servicoSeguradora, eventoTipo, servicoSindicante, valor) {
            this.servicoSeguradora = servicoSeguradora;
            this.servicoSeguradoraId = servicoSeguradora.id;
            this.eventoTipo = eventoTipo;
            this.eventoTipoId = eventoTipo.id;
            this.servicoSindicante = servicoSindicante;
            this.servicoSindicanteId = servicoSindicante.id;
            this.valor = valor;
        }
    

        vm.salvar = salvar;
        vm.pesquisarEnderecoPorCep = pesquisarEnderecoPorCep;
        vm.novaPesquisa = novaPesquisa;
        vm.carregarDropCidade = carregarDropCidade;
        vm.carregarEventoTipo = carregarEventoTipo;
        vm.addHonorario = addHonorario;
        vm.removeHonorario = removeHonorario;

        activate();

        function activate() {
            buscarEstados();
            carregarDropBanco();
            carregarServicosSegurardora();
            carregarServicosSindicantes();
        }

        function salvar() {
            vm.loading = true;
            sindicanteService.adicionar(vm.sindicante)
                .success(success)
                .catch(fail);

            function success(response) {
                if (response) {
                    vm.notificacoes = response.errors;
                    vm.loading = false;
                } else {
                    toastr.success("Sindicante salvo com sucesso", "SUCESSO");
                    vm.loading = false;
                    $location.path('/sindicante');
                }
            }

            function fail(error) {
                toastr.error('Ocorreu um erro ao salvar o sindicante', 'ERRO');
                console.log(error);
                vm.loading = false;
            }
        }
        
        function pesquisarEnderecoPorCep() {
            enderecoService.pesquisarEnderecoPorCep(vm.sindicante.endereco.cep)
                .success(success)
                .catch(fail);

            function success(response) {
                if (response) {
                    vm.sindicante.endereco.logradouro = response.logradouro;
                    vm.sindicante.endereco.bairro = response.bairro;
                    
                    
                    for (var i = 0; i < vm.estados.length; i++) {
                        if (vm.estados[i].uf === response.estado) {
                            vm.sindicante.endereco.estado = vm.estados[i];
                            break;
                        }
                    }

                    enderecoService.selecionarCidades(vm.sindicante.endereco.estado.id)
                        .success(success)
                        .catch(fail);

                    function success(cidades) {
                        vm.cidades = cidades;
                        for (var i = 0; i < vm.cidades.length; i++) {
                            if (vm.cidades[i].nome === response.cidade) {
                                vm.sindicante.endereco.cidade = vm.cidades[i];
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
                toastr.error("Preencha manualmente ou tente outro CEP", "CEP "+vm.sindicante.endereco.cep+" Inválido");                  
                limparEndereco();
                vm.desabilitada = false;
                vm.desabilitaNovaPesquisa = false;
            }
        }

        function buscarCidades() {
            enderecoService.selecionarCidades(vm.sindicante.endereco.estado.id)
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
            if (vm.sindicante.endereco.estado) {              
                vm.cidades = buscarCidades(vm.sindicante.endereco.estado.id);
                vm.desabilitaCidade = false;
            } else {
                vm.sindicante.endereco.cidade = {};
                vm.desabilitaCidade = true;
            }               
        }

        function carregarDropBanco() {
            bancoService.selecionarTodos()
                .success(success)
                .catch(fail);

            function success(response) {
                vm.bancoList = response;
            }

            function fail(error) {
                toastr.error("Erro ao carregar bancos", "ERRO");
                console.log(error);
            }
        }

        function carregarEventoTipo() {
            if (vm.honorario.servicoSeguradora == null) {
                vm.desabilitaDropEvento = true;
                vm.eventoTipoList = [];
                return;
            }

            vm.eventoTipoList = vm.honorario.servicoSeguradora.eventosTipos;
            vm.desabilitaDropEvento = false;

        }

        function carregarServicosSegurardora() {
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

        function carregarServicosSindicantes() {
            servicoSindicanteService.selecionarTodos()
                .success(success)
                .catch(fail);

            function success(response) {
                vm.servicoSindicanteList = response;
            }

            function fail(error) {
                toastr.error("Erro ao carregar serviços sindicantes");
                console.log(error);
            }
        }

        function addHonorario(data) {
            var flag = false;
            if (data !== null && data !== undefined) {
                var h = new honorario(data.servicoSeguradora, data.eventoTipo, data.servicoSindicante, data.valor);
                if (vm.sindicante.honorarios.length > 0) {
                    angular.forEach(vm.sindicante.honorarios, function (value, key) {
                        if (value.servicoSeguradora.id === data.servicoSeguradora.id && value.eventoTipo.id === data.eventoTipo.id && value.servicoSindicante.id === data.servicoSindicante.id) {
                            toastr.warning('Honorário já foi adicionado', 'Aviso');
                            flag = true;
                        }
                    });

                    if (!flag) {

                        vm.sindicante.honorarios.push(h);
                    }

                } else {

                    vm.sindicante.honorarios.push(h);
                }
            } else {
                toastr.warning('Nenhum honorário selcionado!!!', "Aviso");
            }

            resetDropdown();
            resetInputs();
        }

        function removeHonorario(data) {
            var index = vm.sindicante.honorarios.indexOf(data);
            vm.sindicante.honorarios.splice(index, 1);
        }

        function limparEndereco() {         
            vm.sindicante.endereco.logradouro = "";
            vm.sindicante.endereco.bairro = "";
            vm.sindicante.endereco.cidade = {};
            vm.sindicante.endereco.estado = {};
        }

        function novaPesquisa() {
            vm.sindicante.endereco.cep = "";
            limparEndereco();
            vm.desabilitada = true;
            vm.desabilitaNovaPesquisa = true;
            vm.desabilitaCidade = true;
        }

        function resetDropdown() {
            if (angular.isDefined(vm.honorario.servicoSeguradora)) {
                delete vm.honorario.servicoSeguradora;
            }
            if (angular.isDefined(vm.honorario.eventoTipo)) {
                delete vm.honorario.eventoTipo;

            }
            if (angular.isDefined(vm.honorario.servicoSindicante)) {
                delete vm.honorario.servicoSindicante;

            }

            vm.desabilitaDropEvento = true;
        }

        function resetInputs() {
            vm.honorario.valor = "";
        }
    }
})();