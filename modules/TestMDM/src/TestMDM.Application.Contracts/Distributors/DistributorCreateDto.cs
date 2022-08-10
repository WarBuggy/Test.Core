using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace TestMDM.Distributors
{
    public class DistributorCreateDto
    {
        [Required]
        [StringLength(DistributorConsts.CompanyNameMaxLength, MinimumLength = DistributorConsts.CompanyNameMinLength)]
        public string CompanyName { get; set; }
        [Required]
        [StringLength(DistributorConsts.TaxIdMaxLength, MinimumLength = DistributorConsts.TaxIdMinLength)]
        public string TaxId { get; set; }
        public List<Guid> IdentityUserIds { get; set; }
    }
}