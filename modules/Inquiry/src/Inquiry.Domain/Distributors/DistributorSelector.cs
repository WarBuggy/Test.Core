using System;

namespace Inquiry.Distributors
{
    public class DistributorSelector
    {

        public Guid DistributorId { get; set; }

        public bool IsActive { get; set; }

        public string CompanyName { get; set; }

        public DistributorSelector()
        {

        }

        public DistributorSelector(Guid distributorId, bool isActive, string companyName)
        {
            DistributorId = distributorId;
            CompanyName = companyName;
            IsActive = isActive;
        }
    }
}