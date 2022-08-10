using Volo.Abp.Identity;

using System;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;

namespace TestMDM.Distributors
{
    public class DistributorWithNavigationPropertiesDto
    {
        public DistributorDto Distributor { get; set; }

        public List<IdentityUserDto> IdentityUsers { get; set; }

    }
}