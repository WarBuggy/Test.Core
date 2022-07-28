using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Test.Core.Distributors
{
    public class DistributorCreateDto
    {
        [Required]
        [StringLength(int.MaxValue, MinimumLength = DistributorConsts.CompanyNameMinLength)]
        public string CompanyName { get; set; }
        [Required]
        [StringLength(int.MaxValue, MinimumLength = DistributorConsts.TaxIDMinLength)]
        public string TaxID { get; set; }
    }
}