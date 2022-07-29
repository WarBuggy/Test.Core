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
        public virtual string Name { get; set; }

        [NotNull]
        public virtual string TaxId { get; set; }

        public Distributor()
        {

        }

        public Distributor(Guid id, string name, string taxId)
        {

            Id = id;
            Check.NotNull(name, nameof(name));
            Check.NotNull(taxId, nameof(taxId));
            Name = name;
            TaxId = taxId;
        }

    }
}