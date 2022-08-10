using Volo.Abp.Identity;

using System;
using System.Collections.Generic;

namespace TestMDM.Distributors
{
    public class DistributorWithNavigationProperties
    {
        public Distributor Distributor { get; set; }

        

        public List<IdentityUser> IdentityUsers { get; set; }
        
    }
}