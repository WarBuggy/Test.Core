using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Inquiry.Distributors;

namespace Inquiry.Web.Pages.Distributors
{
    public class IndexModel : AbpPageModel
    {
        public string CompanyNameFilter { get; set; }
        public string TaxIdFilter { get; set; }

        private readonly IDistributorsAppService _distributorsAppService;

        public IndexModel(IDistributorsAppService distributorsAppService)
        {
            _distributorsAppService = distributorsAppService;
        }

        public async Task OnGetAsync()
        {

            await Task.CompletedTask;
        }
    }
}