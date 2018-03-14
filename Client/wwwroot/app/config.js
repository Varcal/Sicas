(function() {
    'use strict';

    angular.module('app').constant('SETTINGS', {  
        'VERSION': '1.0.0',
        'CURR_ENV': 'prod',
        'AUTH_TOKEN': 'sicas-token',
        'AUTH_USER': 'sicas-user',
       
        'IMAGES_URL': '../dev/assets/images', /*dev*/
        'SERVICE_URL': 'http://localhost/SicasApi/', /*dev*/
        'SERVICE_URL_WEB': null /*dev*/

        //'IMAGES_URL': '../assets/images', /*producao*/
        //'SERVICE_URL': 'http://portalfc.com.br/', /*producao*/
        //'SERVICE_URL_WEB': null /*producao*/

    });

    angular.module('app').run(function ($rootScope, $location, $window, SETTINGS, Profile) {

        var token = sessionStorage.getItem(SETTINGS.AUTH_TOKEN);
        var user = sessionStorage.getItem(SETTINGS.AUTH_USER);
        

        if (user !== 'undefined' && user !== '')
            user = JSON.parse(user);

        $rootScope.images = SETTINGS.IMAGES_URL;
        $rootScope.user = null;
        $rootScope.token = null;
        $rootScope.header = null;
        $rootScope.roles = null;

        if (token && user) {
            $rootScope.user = user;
            $rootScope.token = token;
            $rootScope.roles = user.perfis;
            $rootScope.profile = Profile;
            $rootScope.header = {
                headers: {
                    'Authorization': 'Bearer ' + $rootScope.token
                }
            }
        }

        $rootScope.$on("$routeChangeStart", function (event, next, current) {
            if ($rootScope.user == null) {
                if (SETTINGS.SERVICE_URL_WEB == null) {
                    $location.path('/login');
                   } 
                 else {
                    $window.location.href = SETTINGS.SERVICE_URL_WEB + '#/login';
                }
            }
        });
    });

    angular.module('app').config(function ($httpProvider) {
      $httpProvider.interceptors.push('errorInterceptor');
    });
})();