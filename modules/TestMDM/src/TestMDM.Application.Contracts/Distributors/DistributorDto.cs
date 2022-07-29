using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace TestMDM.Distributors
{
    public class DistributorDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public string CompanyName { get; set; }
        public string TaxId { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}