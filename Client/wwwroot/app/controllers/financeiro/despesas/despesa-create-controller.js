(function () {
    'use strict';

    angular.module('app').controller('despesaCreateController', despesaCreateController);

    despesaCreateController.$inject = ['$routeParams', '$filter','$location', 'despesaService', 'enderecoService'];

    function despesaCreateController($routeParams, $filter, $location, despesaService, enderecoService) {
        var vm = this;

        var id = $routeParams.id;

        var DespesaAdicional = function (d) {
            this.id = d.id;
            this.despesaTipoId = d.despesaTipo.id;
            this.despesaId = 0;
            this.descricao = d.despesaTipo.descricao;
            this.valor = d.valor;
        }

        var Despesa = function(desp) {
            this.id = desp.id;
            this.processoId = vm.processo.id;
            this.descricao = desp.descricao;
            this.data = desp.data;
            this.origem = new Endereco(desp.origem);
            this.destino = new Endereco(desp.destino);
            this.kms = desp.kms;
            this.valorKm = parseFloat(desp.valorKm);
            this.despesasAdicionais = desp.despesasAdicionais;

            var pedTrans = 0;
            var estAlim = 0;

            if (desp.despesasAdicionais) {
                angular.forEach(desp.despesasAdicionais, function (value, key) {
                    if (value.id === 1 || value.id === 2) {
                        pedTrans += value.valor;
                        
                    }
                    else {
                        estAlim += value.valor;
                    }
                });
            }

            this.pedagioTransporte = pedTrans;
            this.estadiaAlimentacao = estAlim;
            this.total = this.valorKm + pedTrans + estAlim;

        }
        
        var Endereco = function(end) {
            this.logradouro = end.logradouro;
            this.numero = end.numero;
            this.bairro = end.bairro;
            this.cep = end.cep;
            this.cidade = new Cidade(end.cidade, end.estado);
        }

        var Cidade = function(c, e) {
            this.id = c.id;
            this.nome = c.nome;
            this.estado = new Estado(e);
        }

        var Estado = function(e) {
            this.id = e.id;
            this.nome = e.nome;
            this.uf = e.uf;
        }

        vm.desabilitadaOrigem = true;
        vm.desabilitadaDestino = true;
        vm.desabilitaNovaPesquisaOrigem = true;
        vm.desabilitaNovaPesquisaDestino = true;
        vm.habilitadoAdicionais = false;
        vm.loading = false;


        vm.processo = {
            id: 0,
            despesas: []
        }

        vm.despesa = {
            id: 0,
            despesasAdicionais: []
        }

        vm.tipoDespesaList = [{ 'id': 1, 'descricao': 'Estadia' }, { 'id': 2, 'descricao': 'Alimentação' }, { 'id': 3, 'descricao': 'Transporte' }, { 'id': 4, 'descricao': 'Pedágio' }];

        vm.tipoDespesa = {
            id: 0,
            descricao: ''
        }

        vm.calcularDistancia = calcularDistancia;
        vm.selecionarTipoDespesa = selecionarTipoDespesa;
        vm.pesquisar = pesquisar;
        vm.salvar = salvar;
        vm.pesquisarOrigemPorCep = pesquisarOrigemPorCep;
        vm.pesquisarDestinoPorCep = pesquisarDestinoPorCep;
        vm.carregarDropCidadeOrigem = carregarDropCidadeOrigem
        vm.carregarDropCidadeDestino = carregarDropCidadeDestino;
        vm.novaPesquisaOrigem = novaPesquisaOrigem;
        vm.novaPesquisaDestino = novaPesquisaDestino;
        vm.adicionarDespesa = adicionarDespesa;
        vm.habilitarAdicionais = habilitarAdicionais;
        vm.incluirDespesaAdicional = incluirDespesaAdicional;
        vm.excluirDespesaAdicional = excluirDespesaAdicional;
        vm.excluirDespesa = excluirDespesa;


        activate();

        function activate() {
            obterPorId();
            buscarEstadosOrigem();
            buscarEstadosDestino();
            angular.element("#descricao").focus();
        }

        function salvar() {
            vm.loading = true;
            despesaService.registrar(vm.processo)
                .success(success)
                .catch(fail);

            function success(response) {
                toastr.success("Despesas registradas com sucesso", "SUCESSO");
                vm.loading = false;
                $location.path('/despesa');
            }

            function fail(error) {
                toastr.error("Erro ao registrar despesas", "ERRO");
                vm.loading = false;
                console.log(error);
            }
        }

        function pesquisar() {
            if (vm.numProcesso !== "") {
                vm.habilitado = true;
            } else {
                vm.habilitado = false;
            }
        }

        function selecionarTipoDespesa(id) {
            vm.tipoDespesa.id = id;
            vm.valorPorKm = 0.56;
        }

        function calcularDistancia() {

            //var bairroOrigem = vm.despesa.origem.bairro == undefined ? "" : vm.despesa.origem.bairro;
            //var bairroDestino = vm.despesa.destino.bairro == undefined ? "" : vm.despesa.destino.bairro;

            var orig = vm.despesa.origem.logradouro + ', ' + vm.despesa.origem.numero + ' - ' + vm.despesa.origem.bairro + ', ' + vm.despesa.origem.cidade.nome + ' - ' + vm.despesa.origem.estado.uf + ', ' + vm.despesa.origem.cep;
            var dest = vm.despesa.destino.logradouro + ', ' + vm.despesa.destino.numero + ' - ' + vm.despesa.destino.bairro + ', ' + vm.despesa.destino.cidade.nome + ' - ' + vm.despesa.destino.estado.uf + ', ' + vm.despesa.destino.cep;


            despesaService.calcularDistancia(orig, dest)
                .then(function (response) {
                    vm.despesa.kms = (response / 1000).toFixed(1);
                    vm.despesa.valorKm = (vm.despesa.kms * vm.processo.valorKm).toFixed(2);

                    cadastrarEndereco(vm.despesa.origem);
                    cadastrarEndereco(vm.despesa.destino);
                });
        }

        function obterPorId() {
            despesaService.obterPorId(id)
                .success(success)
                .catch(fail);

            function success(response) {
                vm.processo = response;
            }

            function fail(error) {

                toastr.error("Erro ao obter processo", "Erro");
                console.log(error);
            }
        }

        function pesquisarOrigemPorCep() {
            enderecoService.pesquisarEnderecoPorCep(vm.despesa.origem.cep)
                .success(success)
                .catch(fail);

            function success(response) {
                if (response) {
                    vm.despesa.origem.logradouro = response.logradouro;
                    vm.despesa.origem.bairro = response.bairro == undefined ? "" : response.bairro;  


                    for (var i = 0; i < vm.estadosOrigem.length; i++) {
                        if (vm.estadosOrigem[i].uf === response.estado) {
                            vm.despesa.origem.estado = vm.estadosOrigem[i];
                            break;
                        }
                    }

                    enderecoService.selecionarCidades(vm.despesa.origem.estado.id)
                        .success(success)
                        .catch(fail);

                    function success(cidades) {
                        vm.cidadesOrigem = cidades;
                        for (var i = 0; i < vm.cidadesOrigem.length; i++) {
                            if (vm.cidadesOrigem[i].nome === response.cidade) {
                                vm.despesa.origem.cidade = vm.cidadesOrigem[i];
                                break;
                            }
                        }

                        vm.desabilitaNovaPesquisaOrigem = false;
                        vm.desabilitadaOrigem = true;

                        //if (vm.despesa.origem.logradouro !== "");
                        //    cadastrarEndereco(vm.despesa.origem);
                    }

                    function fail(error) {
                        console.log(error);
                    }
                }


            }

            function fail() {
                toastr.error("Cep não encontrado", "CEP " + vm.despesa.origem.cep + " Inválido");
                limparOrigem();
                vm.desabilitadaOrigem = false;
                vm.desabilitaNovaPesquisaOrigem = false;
            }
        }

        function buscarCidadesOrigem() {
            enderecoService.selecionarCidades(vm.despesa.origem.estado.id)
                .success(success)
                .catch(fail);

            function success(response) {
                return vm.cidadesOrigem = response;
            }

            function fail(error) {
                toastr.error("Ocorreu um erro inesperado tente novamente", "ERRO")
                console.log(error);
            }

        }

        function buscarEstadosOrigem() {
            enderecoService.selecionarEstados()
                .success(success)
                .catch(fail);

            function success(response) {
                vm.estadosOrigem = response;
            }

            function fail(error) {
                console.log(error);
            }
        }

        function pesquisarDestinoPorCep() {
            enderecoService.pesquisarEnderecoPorCep(vm.despesa.destino.cep)
                .success(success)
                .catch(fail);

            function success(response) {

                if (response) {
                    vm.despesa.destino.logradouro = response.logradouro;
                    vm.despesa.destino.bairro = response.bairro == undefined ? "" : response.bairro;

                    for (var i = 0; i < vm.estadosDestino.length; i++) {
                        if (vm.estadosDestino[i].uf === response.estado) {
                            vm.despesa.destino.estado = vm.estadosDestino[i];
                            break;
                        }
                    }

                    enderecoService.selecionarCidades(vm.despesa.destino.estado.id)
                        .success(success)
                        .catch(fail);

                    function success(cidades) {
                        vm.cidadesDestino = cidades;
                        for (var i = 0; i < vm.cidadesDestino.length; i++) {
                            if (vm.cidadesDestino[i].nome === response.cidade) {
                                vm.despesa.destino.cidade = vm.cidadesDestino[i];
                                break;
                            }
                        }

                        vm.desabilitaNovaPesquisaDestino = false;
                        vm.desabilitadaDestino = true;

                        //if (vm.despesa.destino.logradouro !== "")
                        //    cadastrarEndereco(vm.despesa.destino);
                    }

                    function fail(error) {
                        console.log(error);
                    }
                }
            }

            function fail() {
                toastr.error("Cep não encontrado", "CEP " + vm.despesa.destino.cep + " Inválido");
                limparDestino();
                vm.desabilitadaDestino = false;
                vm.desabilitaNovaPesquisaDestino = false;
            }
        }    

        function buscarCidadesDestino() {
            enderecoService.selecionarCidades(vm.despesa.destino.estado.id)
                .success(success)
                .catch(fail);

            function success(response) {
                return vm.cidadesDestino = response;
            }

            function fail(error) {
                toastr.error("Ocorreu um erro inesperado tente novamente", "ERRO")
                console.log(error);
            }

        }

        function buscarEstadosDestino() {
            enderecoService.selecionarEstados()
                .success(success)
                .catch(fail);

            function success(response) {
                vm.estadosDestino = response;
            }

            function fail(error) {
                console.log(error);
            }
        }

        function carregarDropCidadeOrigem() {
            if (vm.despesa.origem.estado) {
                vm.cidadesOrigem = buscarCidadesOrigem(vm.despesa.origem.estado.id);
                vm.desabilitaCidade = false;
            } else {
                vm.despesa.origem.cidade = {};
                vm.desabilitaCidade = true;
            }
        }

        function carregarDropCidadeDestino() {
            if (vm.despesa.destino.estado) {
                vm.cidadesDestino = buscarCidadesDestino(vm.despesa.destino.estado.id);
                vm.desabilitaCidade = false;
            } else {
                vm.despesa.origem.cidade = {};
                vm.desabilitaCidade = true;
            }
        }

        function novaPesquisaOrigem() {
            vm.despesa.origem.cep = "";
            vm.despesa.origem.numero = "";
            limparOrigem();
            vm.desabilitadaOrigem = true;
            vm.desabilitaNovaPesquisaOrigem = true;
            vm.desabilitaCidade = true;
        }

        function novaPesquisaDestino() {
            vm.despesa.destino.cep = "";
            vm.despesa.destino.numero = "";
            limparDestino();
            vm.desabilitadaDestino = true;
            vm.desabilitaNovaPesquisaDestino = true;
            vm.desabilitaCidade = true;
        }

        function limparOrigem() {
            vm.despesa.origem.logradouro = "";
            vm.despesa.origem.bairro = "";
            vm.despesa.origem.cidade = {};
            vm.despesa.origem.estado = {};
        }

        function limparDestino() {
            vm.despesa.destino.logradouro = "";
            vm.despesa.destino.bairro = "";
            vm.despesa.destino.cidade = {};
            vm.despesa.destino.estado = {};
        }

        function cadastrarEndereco(endereco) {
            enderecoService.cadastrar(endereco);
        }

        function adicionarDespesa(form) {
            var desp = new Despesa(vm.despesa);
            vm.processo.despesas.push(desp);
            limparTela();

            form.$setPristine();
            angular.element("#descricao").focus();
        }

        function excluirDespesa(data) {
            var index = vm.processo.despesas.indexOf(data);
            vm.processo.despesas.splice(index, 1);
        }

        function habilitarAdicionais() {
            if (vm.habilitadoAdicionais) {
                vm.habilitadoAdicionais = false;
            } else {
                vm.habilitadoAdicionais = true;
            }
        }

        function incluirDespesaAdicional() {
            vm.despesaAdicional.id = vm.despesa.despesasAdicionais.length + 1;
            var da = new DespesaAdicional(vm.despesaAdicional);
            vm.despesa.despesasAdicionais.push(da);

            vm.despesaAdicional.despesaTipo = null;
            vm.despesaAdicional.valor = "";
        }

        function excluirDespesaAdicional(data) {
            var index = vm.despesa.despesasAdicionais.indexOf(data);
            vm.despesa.despesasAdicionais.splice(index, 1);
        }

        function limparTela() {
            vm.despesa.descricao = "";
            vm.despesa.data = "";
            vm.despesa.kms = "";
            vm.despesa.valorKm = "";
            vm.despesa.despesasAdicionais = [];
            vm.habilitadoAdicionais = false;
            novaPesquisaOrigem();
            novaPesquisaDestino();
        }
    }
})();