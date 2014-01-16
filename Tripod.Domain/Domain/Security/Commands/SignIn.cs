﻿using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNet.Identity;

namespace Tripod.Domain.Security
{
    /// <summary>
    /// Authenticate user's local membership.
    /// </summary>
    public class SignIn : IDefineCommand
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsPersistent { get; set; }
    }

    public class ValidateSignInCommand : AbstractValidator<SignIn>
    {
        public ValidateSignInCommand(IProcessQueries queries)
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithName(string.Format("{0} or {1}", User.Constraints.NameLabel, EmailAddress.Constraints.Label))
                .MustFindUserByNameOrEmail(queries)
            ;
            
            RuleFor(x => x.Password)
                .NotEmpty().WithName(LocalMembership.Constraints.PasswordLabel)
                .MustBeVerifiedPassword(queries, x => x.UserName)
                    .When(x => queries.Execute(new UserBy(x.UserName)).Result != null, ApplyConditionTo.CurrentValidator)
            ;
        }
    }

    public class HandleSignInCommand : IHandleCommand<SignIn>
    {
        private readonly UserManager<User, int> _userManager;
        private readonly IAuthenticate _authenticator;

        public HandleSignInCommand(UserManager<User, int> userManager, IAuthenticate authenticator)
        {
            _userManager = userManager;
            _authenticator = authenticator;
        }

        public async Task Handle(SignIn command)
        {
            var user = await _userManager.FindAsync(command.UserName, command.Password);
            await _authenticator.SignOn(user, command.IsPersistent);
        }
    }
}
