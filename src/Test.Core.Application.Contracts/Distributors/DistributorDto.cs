using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace Test.Core.Distributors
{
    public class DistributorDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public string CompanyName { get; set; }
        public string TaxID { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}