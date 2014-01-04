// <auto-generated />
// This file was generated by a T4 template.
// Don't change it directly as your change would get overwritten.  Instead, make changes
// to the .tt file (i.e. the T4 template) and save it to regenerate this file.

// Make sure the compiler doesn't complain about missing Xml comments
#pragma warning disable 1591
#region T4MVC

using System;
using System.Diagnostics;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Mvc.Html;
using System.Web.Routing;
using T4MVC;
namespace Tripod.Web.Controllers
{
    public partial class EmailAddressesController
    {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public EmailAddressesController() { }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected EmailAddressesController(Dummy d) { }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToAction(ActionResult result)
        {
            var callInfo = result.GetT4MVCResult();
            return RedirectToRoute(callInfo.RouteValueDictionary);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToActionPermanent(ActionResult result)
        {
            var callInfo = result.GetT4MVCResult();
            return RedirectToRoutePermanent(callInfo.RouteValueDictionary);
        }

        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult ValidateSignUp()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.ValidateSignUp);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public EmailAddressesController Actions { get { return MVC.EmailAddresses; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "EmailAddresses";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "EmailAddresses";

        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass
        {
            public readonly string SignUp = "SignUp";
            public readonly string ValidateSignUp = "ValidateSignUp";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants
        {
            public const string SignUp = "SignUp";
            public const string ValidateSignUp = "ValidateSignUp";
        }


        static readonly ActionParamsClass_SignUp s_params_SignUp = new ActionParamsClass_SignUp();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_SignUp SignUpParams { get { return s_params_SignUp; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_SignUp
        {
            public readonly string command = "command";
        }
        static readonly ActionParamsClass_ValidateSignUp s_params_ValidateSignUp = new ActionParamsClass_ValidateSignUp();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_ValidateSignUp ValidateSignUpParams { get { return s_params_ValidateSignUp; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_ValidateSignUp
        {
            public readonly string command = "command";
        }
        static readonly ViewsClass s_views = new ViewsClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ViewsClass Views { get { return s_views; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ViewsClass
        {
            static readonly _ViewNamesClass s_ViewNames = new _ViewNamesClass();
            public _ViewNamesClass ViewNames { get { return s_ViewNames; } }
            public class _ViewNamesClass
            {
                public readonly string SignUp = "SignUp";
            }
            public readonly string SignUp = "~/Views/EmailAddresses/SignUp.cshtml";
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public partial class T4MVC_EmailAddressesController : Tripod.Web.Controllers.EmailAddressesController
    {
        public T4MVC_EmailAddressesController() : base(Dummy.Instance) { }

        partial void SignUpOverride(T4MVC_System_Web_Mvc_ViewResult callInfo);

        public override System.Web.Mvc.ViewResult SignUp()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ViewResult(Area, Name, ActionNames.SignUp);
            SignUpOverride(callInfo);
            return callInfo;
        }

        partial void SignUpOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, Tripod.Domain.Security.SendConfirmationEmail command);

        public override System.Web.Mvc.ActionResult SignUp(Tripod.Domain.Security.SendConfirmationEmail command)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.SignUp);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "command", command);
            SignUpOverride(callInfo, command);
            return callInfo;
        }

        partial void ValidateSignUpOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, Tripod.Domain.Security.SendConfirmationEmail command);

        public override System.Web.Mvc.ActionResult ValidateSignUp(Tripod.Domain.Security.SendConfirmationEmail command)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.ValidateSignUp);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "command", command);
            ValidateSignUpOverride(callInfo, command);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591
