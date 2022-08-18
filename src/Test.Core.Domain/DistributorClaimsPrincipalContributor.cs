using Inquiry;
using Inquiry.Distributors;
using Inquiry.InquiryUses;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using TestMDM.Distributors;
using Volo.Abp.DependencyInjection;
using Volo.Abp.ObjectMapping;
using Volo.Abp.Security.Claims;
using Volo.Abp.Uow;
using Volo.Abp.Users;
using static System.Net.Mime.MediaTypeNames;

namespace Test.Core
{
    public class DistributorClaimsPrincipalContributor : IAbpClaimsPrincipalContributor, ITransientDependency
    {
        [UnitOfWork]
        public async Task ContributeAsync(AbpClaimsPrincipalContributorContext context)
        {
            var identity = context.ClaimsPrincipal.Identities.FirstOrDefault();
            var userId = identity?.FindFirst("sub");
            if (userId == null || userId.Value == null)
            {
                return;
            }
            var inquiryService = context.ServiceProvider.GetRequiredService<IInquiryUsersAppService>();
            var distributorSelector = await inquiryService.GetListDistributorIdentityUserAsync(new Guid(userId.Value));
            string distributorsClaimString = "";
            string currentDistributorClaimString = "";
            if (distributorSelector != null && distributorSelector.Items.Count > 0)
            {
                List<string> ClaimValues = new List<string>();
                foreach (var i in distributorSelector.Items)
                {
                    ClaimValues.Add(i.DistributorId.ToString());
                    if (i.IsActive)
                    {
                        currentDistributorClaimString = i.DistributorId.ToString();
                    }
                }
                distributorsClaimString = string.Join(DistributorConsts.DistributorClaimSeparator, ClaimValues.ToArray());
            }
            if (currentDistributorClaimString.IsNullOrEmpty())
            {
                currentDistributorClaimString = distributorSelector.Items.First().DistributorId.ToString();
            }
            identity.AddIfNotContains(new Claim(DistributorConsts.CurrentDistributorClaimName, currentDistributorClaimString));
            identity.AddIfNotContains(new Claim(DistributorConsts.DistributorClaimName, distributorsClaimString));
        }
    }


    public static class CurrentUserExtensions
    {
        public static List<Guid> GetDistributorClaim(this ICurrentUser currentUser)
        {
            List<Guid> DistributorIds = new List<Guid>();
            string DistributorIdString = currentUser.FindClaimValue(DistributorConsts.DistributorClaimName);
            if (DistributorIdString != null)
            {
                string[] Guids = DistributorIdString.Split(DistributorConsts.DistributorClaimSeparator);
                foreach (var guid in Guids)
                {
                    DistributorIds.Add(Guid.Parse(guid));
                }
            }
            return DistributorIds;
        }
    }
}
