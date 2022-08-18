using System;

namespace Inquiry.Distributors
{
    public class DistributorSelectorDto
    {
        public Guid DistributorId { get;  set; }

        public bool IsActive { get;  set; }

        public string CompanyName { get; set; }
    }
}