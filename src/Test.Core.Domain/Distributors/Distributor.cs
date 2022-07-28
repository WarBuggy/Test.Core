using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace Test.Core.Distributors
{
    public class Distributor : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; set; }

        [NotNull]
        public virtual string CompanyName { get; set; }

        [NotNull]
        public virtual string TaxID { get; set; }

        public Distributor()
        {

        }

        public Distributor(Guid id, string companyName, string taxID)
        {

            Id = id;
            Check.NotNull(companyName, nameof(companyName));
            Check.NotNull(taxID, nameof(taxID));
            CompanyName = companyName;
            TaxID = taxID;
        }

    }
}