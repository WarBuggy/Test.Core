using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace TestMDM.Distributors
{
    public class Distributor : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; set; }

        [NotNull]
        public virtual string CompanyName { get; set; }

        [NotNull]
        public virtual string TaxId { get; set; }

        public ICollection<DistributorIdentityUser> IdentityUsers { get; private set; }

        public Distributor()
        {

        }

        public Distributor(Guid id, string companyName, string taxId)
        {

            Id = id;
            Check.NotNull(companyName, nameof(companyName));
            Check.Length(companyName, nameof(companyName), DistributorConsts.CompanyNameMaxLength, DistributorConsts.CompanyNameMinLength);
            Check.NotNull(taxId, nameof(taxId));
            Check.Length(taxId, nameof(taxId), DistributorConsts.TaxIdMaxLength, DistributorConsts.TaxIdMinLength);
            CompanyName = companyName;
            TaxId = taxId;
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