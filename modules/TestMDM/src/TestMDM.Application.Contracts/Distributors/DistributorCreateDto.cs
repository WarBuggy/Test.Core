using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace TestMDM.Distributors
{
    public class DistributorCreateDto
    {
        [Required]
        [StringLength(int.MaxValue, MinimumLength = DistributorConsts.NameMinLength)]
        public string Name { get; set; }
        [Required]
        [StringLength(int.MaxValue, MinimumLength = DistributorConsts.TaxIdMinLength)]
        public string TaxId { get; set; }
    }
}