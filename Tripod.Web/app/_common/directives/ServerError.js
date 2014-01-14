'use strict';
var App;
(function (App) {
    (function (Directives) {
        (function (ServerError) {
            ServerError.directiveName = 'serverError';

            var directiveFactory = function () {
                return function () {
                    var directive = {
                        name: ServerError.directiveName,
                        restrict: 'A',
                        require: ['ngModel', 'modelContrib'],
                        link: function (scope, element, attr, ctrls) {
                            var serverError = attr['serverError'];
                            if (!serverError)
                                return;

                            var inputType = attr['type'];
                            var isPassword = inputType && inputType.toLowerCase() == 'password';

                            var modelCtrl = ctrls[0];
                            var helpCtrl = ctrls[1];

                            helpCtrl.serverError = serverError;

                            var initialValue;
                            var initWatch = scope.$watch(function () {
                                return modelCtrl.$error;
                            }, function (value) {
                                initialValue = modelCtrl.$viewValue;
                                if (value.required && isPassword) {
                                    modelCtrl.$setValidity('required', true);
                                }
                                modelCtrl.$setValidity('server', false);
                                initWatch();
                            });

                            scope.$watch(function () {
                                return modelCtrl.$viewValue;
                            }, function (value) {
                                if (modelCtrl.$dirty) {
                                    modelCtrl.$setValidity('server', true);
                                }

                                if (!isPassword && value === initialValue) {
                                    modelCtrl.$setValidity('server', false);
                                }
                            });
                        }
                    };
                    return directive;
                };
            };

            ServerError.directive = directiveFactory();
        })(Directives.ServerError || (Directives.ServerError = {}));
        var ServerError = Directives.ServerError;
    })(App.Directives || (App.Directives = {}));
    var Directives = App.Directives;
})(App || (App = {}));