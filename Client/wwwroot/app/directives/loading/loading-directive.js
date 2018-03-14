(function() {
    angular.module('app').directive('loading', function () {
        return {
            restrict: 'E',
            replace: true,
            requere: 'ngModel',
            template: '<div class="loading"><img src="{{images}}/support-loading.gif" width=20 height=20 /> CARREGANDO...</div>',
            link: function (scope, element, ngModel) {
                scope.$watch('loading', function (val) {
                    if (ngModel)
                        $(element).show();
                    else
                        $(element).hide();
                });
            }
        }
    });
})();