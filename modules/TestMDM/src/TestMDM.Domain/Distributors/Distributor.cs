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

        public Distributor()
        {

        }

        public Distributor(Guid id, string companyName, string taxId)
        {

            Id = id;
            Check.NotNull(companyName, nameof(companyName));
            Check.NotNull(taxId, nameof(taxId));
            CompanyName = companyName;
            TaxId = taxId;
        }

    }
}