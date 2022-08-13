
using Volo.Abp.Application.Dtos;
using System;

namespace Inquiry.Distributors
{
    public class GetDistributorsInput : PagedAndSortedResultRequestDto
    {
        public string FilterText { get; set; }

        public string CompanyName { get; set; }
        public string TaxId { get; set; }
        public Guid? IdentityUserId { get; set; }

        public GetDistributorsInput()
        {

        }
    }
}