using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace TestMDM.Distributors
{
    public class DistributorCreateDto
    {
        [Required]
        [StringLength(int.MaxValue, MinimumLength = DistributorConsts.CompanyNameMinLength)]
        public string CompanyName { get; set; }
        [Required]
        [StringLength(int.MaxValue, MinimumLength = DistributorConsts.TaxIdMinLength)]
        public string TaxId { get; set; }
    }
}