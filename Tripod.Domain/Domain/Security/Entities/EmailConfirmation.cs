﻿using System;

namespace Tripod.Domain.Security
{
    public class EmailConfirmation : EntityWithId<int>
    {
        protected internal EmailConfirmation() { }

        public int OwnerId { get; protected internal set; }
        public virtual EmailAddress Owner { get; protected internal set; }

        public string Token { get; protected internal set; }
        public string Stamp { get; protected internal set; }
        public string Ticket { get; protected internal set; }
        public string Code { get; protected internal set; }
        public DateTime ExpiresOnUtc { get; protected internal set; }
        public DateTime? RedeemedOnUtc { get; protected internal set; }
        public EmailConfirmationPurpose Purpose { get; protected internal set; }

        public static class Constraints
        {
            public const string Label = "Email confirmation";
        }
    }
}