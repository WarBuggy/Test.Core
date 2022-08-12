using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace Inquiry.IdentityUserDistributor
{
    public class IdentityUserDistributor : Entity
    {
        public Guid IdentityUserId { get; protected set; }

        public Guid DistributorId { get; protected set; }


        private IdentityUserDistributor()
        {

        }

        public IdentityUserDistributor(Guid identityUserId, Guid distributorId)
        {
            IdentityUserId = identityUserId;
            DistributorId = distributorId;
        }

        public override object[] GetKeys()
        {
            return new object[]
                {
                    IdentityUserId,
                    DistributorId,
                };
        }
    }
}
