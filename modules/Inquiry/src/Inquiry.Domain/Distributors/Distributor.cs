using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Volo.Abp;

namespace Inquiry.Distributors
{
    public class Distributor : TestMDM.Distributors.Distributor
    {
        public ICollection<DistributorIdentityUser> IdentityUsers { get; private set; }

        public Distributor() : base()
        {
        }

        public Distributor(Guid id, string companyName, string taxId) : base(id, companyName, taxId)
        {
            IdentityUsers = new Collection<DistributorIdentityUser>();
        }

        public void AddIdentityUser(Guid identityUserId)
        {
            Check.NotNull(identityUserId, nameof(identityUserId));

            if (IsInIdentityUsers(identityUserId))
            {
                return;
            }

            IdentityUsers.Add(new DistributorIdentityUser(Id, identityUserId));
        }

        public void RemoveIdentityUser(Guid identityUserId)
        {
            Check.NotNull(identityUserId, nameof(identityUserId));

            if (!IsInIdentityUsers(identityUserId))
            {
                return;
            }

            IdentityUsers.RemoveAll(x => x.IdentityUserId == identityUserId);
        }

        public void RemoveAllIdentityUsersExceptGivenIds(List<Guid> identityUserIds)
        {
            Check.NotNullOrEmpty(identityUserIds, nameof(identityUserIds));

            IdentityUsers.RemoveAll(x => !identityUserIds.Contains(x.IdentityUserId));
        }

        public void RemoveAllIdentityUsers()
        {
            IdentityUsers.RemoveAll(x => x.DistributorId == Id);
        }

        private bool IsInIdentityUsers(Guid identityUserId)
        {
            return IdentityUsers.Any(x => x.IdentityUserId == identityUserId);
        }
    }
}
