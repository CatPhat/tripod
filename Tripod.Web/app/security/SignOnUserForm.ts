'use strict';

module App.Security.SignOnUserForm {

    export interface Model {
        userName?: string;
        token?: string;
    }

    export interface Form extends ng.IFormController {
        userName: ng.INgModelController;
        token: ng.INgModelController;
    }

    export interface Contrib extends Directives.FormContrib.Controller {
        userName: Directives.ModelContrib.Controller;
        token: Directives.ModelContrib.Controller;
    }

    export interface Scope extends ViewModelScope<Model> {
        signOnUserForm: Form;
        signOnUserCtrb: Contrib;
    }

    export class Controller implements Model { // BUG make sure to come back and check this

        userName: string = '';
        token: string = '';
        ticket: string = '';

        static $inject = ['$scope'];
        constructor(public scope: Scope) {
            scope.vm = this;
        }

        userNameInputGroupValidationAddOnCssClass(): string {
            return this.scope.signOnUserCtrb.userName.hasFeedback() ? null : 'hide';
        }

        isUserNameRequiredError(): boolean {
            return this.scope.signOnUserForm.userName.$error.required
                && this.scope.signOnUserCtrb.userName.hasError;
        }

        isUserNameServerError(): boolean {
            return this.scope.signOnUserForm.userName.$error.server
                && this.scope.signOnUserCtrb.userName.hasError;
        }

        tokenInputGroupValidationAddOnCssClass(): string {
            return this.scope.signOnUserCtrb.token.hasFeedback() ? null : 'hide';
        }

        isTokenRequiredError(): boolean {
            return this.scope.signOnUserForm.token.$error.required
                && this.scope.signOnUserCtrb.token.hasError;
        }

        isTokenServerError(): boolean {
            return this.scope.signOnUserForm.token.$error.server
                && this.scope.signOnUserCtrb.token.hasError;
        }

        isSubmitWaiting(): boolean {
            return this.scope.signOnUserCtrb.isSubmitWaiting;
        }

        isSubmitError(): boolean {
            return !this.isSubmitWaiting() && this.scope.signOnUserCtrb.hasError;
        }

        isSubmitReady(): boolean {
            return !this.isSubmitWaiting() && !this.isSubmitError();
        }

        isSubmitDisabled(): boolean {
            return this.isSubmitWaiting() || this.isSubmitError();
        }

        submitCssClass(): string {
            return this.isSubmitError() ? 'btn-danger' : null;
        }
    }

    export var moduleName = 'sign-on-register-form';

    export var ngModule = angular.module(moduleName, [Modules.Tripod.moduleName]);
}
