using Volo.Abp.Application.Dtos;
using System;

namespace Test.Core.Distributors
{
    public class GetDistributorsInput : PagedAndSortedResultRequestDto
    {
        public string FilterText { get; set; }

        public string CompanyName { get; set; }
        public string TaxID { get; set; }

        public GetDistributorsInput()
        {

        }
    }
}