using System;
using Volo.Abp.Domain.Entities;

namespace Inquiry.Distributors
{
    public class DistributorIdentityUser : Entity
    {

        public Guid DistributorId { get; protected set; }

        public Guid IdentityUserId { get; protected set; }

        private DistributorIdentityUser()
        {

        }

        public DistributorIdentityUser(Guid distributorId, Guid identityUserId)
        {
            DistributorId = distributorId;
            IdentityUserId = identityUserId;
        }

        public override object[] GetKeys()
        {
            return new object[]
                {
                    DistributorId,
                    IdentityUserId
                };
        }
    }
}