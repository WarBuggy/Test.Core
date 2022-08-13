using System.Collections.Generic;
using Volo.Abp.Identity;

namespace Inquiry.Distributors
{
    public class DistributorWithNavProperties
    {
        public Distributor Distributor { get; set; }

        public List<IdentityUser> IdentityUsers { get; set; }

    }
}
