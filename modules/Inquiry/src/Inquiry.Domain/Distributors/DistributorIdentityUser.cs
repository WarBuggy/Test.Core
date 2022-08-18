using System;
using Volo.Abp.Domain.Entities;

namespace Inquiry.Distributors
{
    public class DistributorIdentityUser : Entity
    {

        public Guid DistributorId { get; protected set; }

        public Guid IdentityUserId { get; protected set; }

        public bool IsActive { get; set; }

        private DistributorIdentityUser()
        {

        }

        public DistributorIdentityUser(Guid distributorId, Guid identityUserId, bool isActive = false)
        {
            DistributorId = distributorId;
            IdentityUserId = identityUserId;
            IsActive = isActive;
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