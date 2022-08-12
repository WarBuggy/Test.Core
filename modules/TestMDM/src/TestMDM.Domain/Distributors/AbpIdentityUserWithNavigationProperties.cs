using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Identity;

namespace TestMDM.Distributors
{
    public class AbpIdentityUserWithNavigationProperties
    {
        public IdentityUser IdentityUser { get; set; }
        public List<Distributor> Distributors { get; set; }
    }
}
