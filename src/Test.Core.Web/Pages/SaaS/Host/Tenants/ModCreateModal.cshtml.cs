using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using TestMDM.Distributors;
using Volo.Abp.ObjectExtending;
using Volo.Saas.Host;

namespace Test.Core.Web.Pages.Saas.Host.Tenants;
public class ModCreateModalModel : Volo.Saas.Host.Pages.Saas.Host.Tenants.CreateModalModel
{
    [BindProperty]
    public DistributorHQInfoModel DistributorHQ { get; set; }

    public ModCreateModalModel(ITenantAppService tenantAppService, IEditionAppService editionAppService) :
        base(tenantAppService, editionAppService)
    {
    }

    public class DistributorHQInfoModel : ExtensibleObject
    {
        [Required]
        [StringLength(DistributorConsts.CompanyNameMaxLength)]
        [DisplayName("DisplayName:DistributorHQName")]
        public string Name { get; set; }

        [Required]
        [StringLength(DistributorConsts.TaxIdMaxLength)]
        [DisplayName("DisplayName:DistributorHQTaxId")]
        public string TaxId { get; set; }
    }
}
