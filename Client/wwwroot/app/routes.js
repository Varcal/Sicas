(function () {
    "use strict";

    angular.module("app").config(function ($routeProvider) {
        $routeProvider
            .when('/', {
                controller: 'homeController',
                controllerAs: 'vm',
                templateUrl: 'pages/home/index.html'
            })
            .when('/login', {
                controller: 'loginController',
                controllerAs: 'vm',
                templateUrl: 'pages/account/login.html'
            })
            .when('/logout', {
                controller: 'logoutController',
                controllerAs: 'vm',
                templateUrl: "pages/account/login.html"
            })
            .when('/seguradora', {
                controller: 'seguradoraListController',
                controllerAs: 'vm',
                templateUrl: 'pages/administrativo/seguradoras/index.html'
            })
            .when('/seguradora/registrar', {
                controller: 'seguradoraCreateController',
                controllerAs: 'vm',
                templateUrl: 'pages/administrativo/seguradoras/registrar.html'
            })
            .when('/seguradora/editar/:id', {
                controller: 'seguradoraEditController',
                controllerAs: 'vm',
                templateUrl: 'pages/administrativo/seguradoras/editar.html'
            })
            .when('/usuario', {
                controller: 'usuarioListController',
                controllerAs: 'vm',
                templateUrl: 'pages/administrativo/usuarios/index.html'
            })
            .when('/usuario/registrar', {
                controller: 'usuarioCreateController',
                controllerAs: 'vm',
                templateUrl: 'pages/administrativo/usuarios/registrar.html'
            })
            .when('/usuario/editar/:id', {
                controller: 'usuarioEditController',
                controllerAs: 'vm',
                templateUrl: "pages/administrativo/usuarios/editar.html"
            })
            .when('/usuario/alterarSenha', {
                controller: 'usuarioSenhaController',
                controllerAs: 'vm',
                templateUrl: "pages/administrativo/usuarios/alterarSenha.html"
            })
            .when('/sindicante', {
                controller: 'sindicanteListController',
                controllerAs: 'vm',
                templateUrl: "pages/administrativo/sindicantes/index.html"
            })
            .when('/sindicante/registrar', {
                controller: 'sindicanteCreateController',
                controllerAs: 'vm',
                templateUrl: "pages/administrativo/sindicantes/registrar.html"
            })
            .when('/sindicante/editar/:id', {
                controller: 'sindicanteEditController',
                controllerAs: 'vm',
                templateUrl: "pages/administrativo/sindicantes/editar.html"
            })
            .when('/processo?', {
                controller: 'processoListController',
                controllerAs: 'vm',
                templateUrl: "pages/administrativo/processos/index.html"
            })
            .when('/processo/registrar', {
                controller: 'processoCreateController',
                controllerAs: 'vm',
                templateUrl: "pages/administrativo/processos/registrar.html"
            })
            .when('/processo/historico', {
                controller: 'processoHistoricoController',
                controllerAs: 'vm',
                templateUrl: "pages/administrativo/processos/historico.html"
            })
            .when('/processo/editar/:id', {
                controller: 'processoEditController',
                controllerAs: 'vm',
                templateUrl: "pages/administrativo/processos/editar.html"
            })
            .when('/processo/parecer/:id', {
                controller: 'processoParecerController',
                controllerAs: 'vm',
                templateUrl: "pages/administrativo/processos/parecer.html"
            })
            .when('/processo/finalizar/:id', {
                controller: 'processoFinalizarController',
                controllerAs: 'vm',
                templateUrl: "pages/administrativo/processos/finalizar.html"
            })
            .when('/servicoSeguradora', {
                controller: 'servicoSeguradoraListController',
                controllerAs: 'vm',
                templateUrl: "pages/administrativo/servicosSeguradoras/index.html"
            })
            .when('/servicoSeguradora/registrar', {
                controller: 'servicoSeguradoraCreateController',
                controllerAs: 'vm',
                templateUrl: "pages/administrativo/servicosSeguradoras/registrar.html"
            })
            .when('/servicoSeguradora/editar/:id', {
                controller: 'servicoSeguradoraEditController',
                controllerAs: 'vm',
                templateUrl: "pages/administrativo/servicosSeguradoras/editar.html"
            })
            .when('/eventoTipo', {
                controller: 'eventoTipoListController',
                controllerAs: 'vm',
                templateUrl: "pages/administrativo/eventosTipos/index.html"
            })
            .when('/eventoTipo/registrar', {
                controller: 'eventoTipoCreateController',
                controllerAs: 'vm',
                templateUrl: "pages/administrativo/eventosTipos/registrar.html"
            })
            .when('/eventoTipo/editar/:id', {
                controller: 'eventoTipoEditController',
                controllerAs: 'vm',
                templateUrl: "pages/administrativo/eventosTipos/editar.html"
            })
            .when('/despesa', {
                controller: 'despesaListController',
                controllerAs: 'vm',
                templateUrl: "pages/financeiro/despesas/index.html"
            })
            .when('/despesa/registrar/:id', {
                controller: 'despesaCreateController',
                controllerAs: 'vm',
                templateUrl: "pages/financeiro/despesas/registrar.html"
            })
            .when('/despesa/editar/:id', {
                controller: 'despesaEditController',
                controllerAs: 'vm',
                templateUrl: "pages/financeiro/despesas/editar.html"
            })
            .when('/despesa/recibo', {
                controller: 'despesaReciboController',
                controllerAs: 'vm',
                templateUrl: "pages/financeiro/despesas/recibo.html"
            })
            .when('/despesa/detalhes/:id', {
                controller: 'despesaDetailController',
                controllerAs: 'vm',
                templateUrl: "pages/financeiro/despesas/visualizar.html"
            })
            .when('/lancamento', {
                controller: 'lancamentoController',
                controllerAs: 'vm',
                templateUrl: "pages/financeiro/lancamentos/index.html"
            })
            .when('/pagamento', {
                controller: 'reciboSindicanteController',
                controllerAs: 'vm',
                templateUrl: "pages/financeiro/lancamentos/pagamento.html"
            })
            .when('/reciboSindicante/recibo', {
                controller: 'reciboSindicanteReciboController',
                controllerAs: 'vm',
                templateUrl: "pages/financeiro/lancamentos/recibo.html"
            });
    });
})();