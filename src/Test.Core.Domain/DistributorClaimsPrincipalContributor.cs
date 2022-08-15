using Inquiry;
using Inquiry.InquiryUses;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TestMDM.Distributors;
using Volo.Abp.DependencyInjection;
using Volo.Abp.ObjectMapping;
using Volo.Abp.Security.Claims;
using Volo.Abp.Users;
using static System.Net.Mime.MediaTypeNames;

namespace Test.Core
{
    public class DistributorClaimsPrincipalContributor : IAbpClaimsPrincipalContributor, ITransientDependency
    {
        public async Task ContributeAsync(AbpClaimsPrincipalContributorContext context)
        {
            var identity = context.ClaimsPrincipal.Identities.FirstOrDefault();
            var userId = identity?.FindFirst("sub");
            if (userId == null || userId.Value == null)
            {
                return;
            }
            var inquiryService = context.ServiceProvider.GetRequiredService<IInquiryUsersAppService>();
            var ObjectMapper = context.ServiceProvider.GetRequiredService<IObjectMapper>();
            var distributor = await inquiryService.GetListDistributorDtoAsync(new Guid(userId.Value), ObjectMapper);
            if (distributor == null || distributor.Items.Count < 1)
            {
                identity.AddClaim(new Claim(DistributorConsts.DistributorClaimName, ""));
            }
            List<string> ClaimValues = new List<string>();
            foreach (var i in distributor.Items)
            {
                ClaimValues.Add(i.Id.ToString());
            }
            identity.AddClaim(new Claim(DistributorConsts.DistributorClaimName, 
                string.Join(DistributorConsts.DistributorClaimSeparator, ClaimValues.ToArray())));
        }
    }


    public static class CurrentUserExtensions
    {
        public static List<Guid> GetDistributorClaim(this ICurrentUser currentUser)
        {
            List<Guid> DistributorIds = new List<Guid>();
            string DistributorIdString = currentUser.FindClaimValue(DistributorConsts.DistributorClaimName);
            if (DistributorIdString == null)
            {
                return DistributorIds;
            }
            string[] Guids = DistributorIdString.Split(DistributorConsts.DistributorClaimSeparator);
            foreach (var guid in Guids)
            {
                DistributorIds.Add(Guid.Parse(guid));
            }
            return DistributorIds;
        }
    }
}
