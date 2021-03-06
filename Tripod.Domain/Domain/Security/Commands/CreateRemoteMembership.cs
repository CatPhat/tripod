﻿using System;
using System.Linq.Expressions;
using System.Security.Principal;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNet.Identity;

namespace Tripod.Domain.Security
{
    public class CreateRemoteMembership : BaseCreateEntityCommand<RemoteMembership>, IDefineSecuredCommand
    {
        public IPrincipal Principal { get; set; }
        public string UserName { get; [UsedImplicitly] set; }
        public string Token { get; [UsedImplicitly] set; }
        public string Ticket { get; [UsedImplicitly] set; }
        internal User User { get; set; }
    }

    [UsedImplicitly]
    public class ValidateCreateRemoteMembershipCommand : AbstractValidator<CreateRemoteMembership>
    {
        public ValidateCreateRemoteMembershipCommand(IProcessQueries queries)
        {
            RuleFor(x => x.Principal)
                .MustFindUserByPrincipal(queries)
                    .When(x => x.Principal.Identity.IsAuthenticated,
                        ApplyConditionTo.CurrentValidator)
                .MustFindRemoteMembershipTicket(queries)
                .WithName(User.Constraints.Label)
            ;

            RuleFor(x => x.UserName)
                .MustBeValidUserName()
                .MustNotFindUserByName(queries)
                .MustNotBeUnverifiedEmailUserName(queries, x => x.Ticket)

                // username is not required when signed-on user adds a social login
                .When(x => !x.Principal.Identity.IsAuthenticated && x.User == null)
                .WithName(User.Constraints.NameLabel)
            ;

            RuleFor(x => x.Ticket)
                .MustBeRedeemableVerifyEmailTicket(queries)
                .MustBePurposedVerifyEmailTicket(queries, x => EmailVerificationPurpose.CreateRemoteUser)
                .MustHaveValidVerifyEmailToken(queries, x => x.Token)

                // ticket is not required when signed-on user adds a social login
                .When(x => !x.Principal.Identity.IsAuthenticated && x.User == null)
                .WithName(EmailVerification.Constraints.Label)
            ;
        }
    }

    [UsedImplicitly]
    public class HandleCreateRemoteMembershipCommand : IHandleCommand<CreateRemoteMembership>
    {
        private readonly IProcessQueries _queries;
        private readonly IProcessCommands _commands;
        private readonly IWriteEntities _entities;

        public HandleCreateRemoteMembershipCommand(IProcessQueries queries, IProcessCommands commands, IWriteEntities entities)
        {
            _commands = commands;
            _entities = entities;
            _queries = queries;
        }

        public async Task Handle(CreateRemoteMembership command)
        {
            // does user already exist?
            var hasUserId = command.Principal.Identity.HasAppUserId();
            var user = hasUserId ? await _entities.Get<User>()
                .EagerLoad(new Expression<Func<User, object>>[]
                {
                    x => x.RemoteMemberships,
                })
                .ByIdAsync(command.Principal.Identity.GetUserId<int>()) : command.User;

            if (user == null)
            {
                var createUser = new CreateUser { Name = command.UserName };
                await _commands.Execute(createUser);
                user = createUser.CreatedEntity;

                // verify & associate email address
                await _commands.Execute(new RedeemEmailVerification(user)
                {
                    Commit = false,
                    Token = command.Token,
                    Ticket = command.Ticket,
                });
            }

            var ticket = await _queries.Execute(new PrincipalRemoteMembershipTicket(command.Principal));

            // do not add this login if it already exists
            if (user.RemoteMemberships.ByUserLoginInfo(ticket.Login) != null) return;

            var entity = new RemoteMembership
            {
                User = user,
                UserId = user.Id,
                Id =
                {
                    LoginProvider = ticket.Login.LoginProvider,
                    ProviderKey = ticket.Login.ProviderKey
                },
            };
            user.RemoteMemberships.Add(entity);
            user.SecurityStamp = Guid.NewGuid().ToString();

            if (command.Commit) await _entities.SaveChangesAsync();
            command.CreatedEntity = entity;
        }
    }
}
