﻿using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Tripod.Domain.Security;
using Tripod.Web.Models;

namespace Tripod.Web.Controllers
{
    [Authorize]
    public partial class UserLoginsController : Controller
    {
        private readonly IProcessQueries _queries;
        private readonly IProcessValidation _validation;
        private readonly IProcessCommands _commands;

        public UserLoginsController(IProcessQueries queries, IProcessValidation validation, IProcessCommands commands)
        {
            _queries = queries;
            _validation = validation;
            _commands = commands;
        }

        [HttpGet, Route("settings/logins")]
        public virtual async Task<ActionResult> Index()
        {
            var user = await _queries.Execute(new UserViewBy(User.Identity.GetAppUserId()));
            var logins = await _queries.Execute(new RemoteMembershipViewsBy(User.Identity.GetAppUserId()));

            var model = new LoginSettingsModel
            {
                UserView = user,
                Logins = logins.ToArray(),
            };

            ViewBag.ReturnUrl = Url.Action(MVC.UserLogins.Index());
            return View(MVC.Security.Views.UserLogins, model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost, Route("settings/logins")]
        public virtual ActionResult Post(string provider, string returnUrl)
        {
            if (string.IsNullOrWhiteSpace(provider)) return View(MVC.Errors.Views.BadRequest);

            // Request a redirect to the external login provider
            string redirectUri = Url.Action(MVC.UserLogins.LinkLoginCallback(provider, returnUrl));
            string userId = User.Identity.GetUserId();
            return new ChallengeResult(provider, redirectUri, userId);
        }

        [HttpGet, Route("settings/logins/callback")]
        public virtual async Task<ActionResult> LinkLoginCallback(string provider, string returnUrl)
        {
            string alert = null;
            var loginInfo = await _queries.Execute(new PrincipalRemoteMembershipTicket(User));
            if (loginInfo == null)
            {
                alert = string.Format("There was an error adding your **{0}** login, please try again.",
                    provider);
            }
            var command = new CreateRemoteMembership { Principal = User };
            var validationResult = _validation.Validate(command);
            if (!validationResult.IsValid)
            {
                alert = string.Format("There was an error adding your **{0}** login: {1}",
                    provider, validationResult.Errors[0].ErrorMessage);
            }
            if (!string.IsNullOrWhiteSpace(alert))
            {
                TempData.Alerts(alert, AlertFlavor.Danger, true);
                return RedirectToAction(await MVC.UserLogins.Index());
            }

            await _commands.Execute(command);
            return RedirectToAction(await MVC.UserLogins.Index());
        }

        [ValidateAntiForgeryToken]
        [HttpDelete, Route("settings/logins/{loginProvider}")]
        public virtual async Task<ActionResult> Delete(string loginProvider, DeleteRemoteMembership command)
        {
            if (ModelState.IsValid)
            {
                TempData.Alerts("Command should be executed.", AlertFlavor.Success);
                return RedirectToAction(await MVC.UserLogins.Index());
            }

            TempData.Alerts("Model did not pass validation.", AlertFlavor.Danger);
            return RedirectToAction(await MVC.UserLogins.Index());
        }
    }
}