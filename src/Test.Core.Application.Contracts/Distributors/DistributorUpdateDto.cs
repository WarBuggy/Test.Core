using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace Test.Core.Distributors
{
    public class DistributorUpdateDto : IHasConcurrencyStamp
    {
        [Required]
        [StringLength(int.MaxValue, MinimumLength = DistributorConsts.CompanyNameMinLength)]
        public string CompanyName { get; set; }
        [Required]
        [StringLength(int.MaxValue, MinimumLength = DistributorConsts.TaxIDMinLength)]
        public string TaxID { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}