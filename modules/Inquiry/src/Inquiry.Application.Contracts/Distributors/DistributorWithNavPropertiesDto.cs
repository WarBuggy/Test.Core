using Volo.Abp.Identity;
using System.Collections.Generic;

namespace Inquiry.Distributors
{
    public class DistributorWithNavPropertiesDto
    {
        public DistributorDto Distributor { get; set; }

        public List<IdentityUserDto> IdentityUsers { get; set; }

    }
}
