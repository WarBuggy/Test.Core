using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace TestMDM.Distributors
{
    public class DistributorUpdateDto : IHasConcurrencyStamp
    {
        [Required]
        [StringLength(DistributorConsts.CompanyNameMaxLength, MinimumLength = DistributorConsts.CompanyNameMinLength)]
        public string CompanyName { get; set; }
        [Required]
        [StringLength(DistributorConsts.TaxIdMaxLength, MinimumLength = DistributorConsts.TaxIdMinLength)]
        public string TaxId { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}