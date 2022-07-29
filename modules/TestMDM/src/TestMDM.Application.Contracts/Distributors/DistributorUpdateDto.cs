using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace TestMDM.Distributors
{
    public class DistributorUpdateDto : IHasConcurrencyStamp
    {
        [Required]
        [StringLength(int.MaxValue, MinimumLength = DistributorConsts.NameMinLength)]
        public string Name { get; set; }
        [Required]
        [StringLength(int.MaxValue, MinimumLength = DistributorConsts.TaxIdMinLength)]
        public string TaxId { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}