namespace Test.Core.Distributors
{
    public static class DistributorConsts
    {
        private const string DefaultSorting = "{0}CompanyName asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "Distributor." : string.Empty);
        }

        public const int CompanyNameMinLength = 1;
        public const int TaxIDMinLength = 1;
    }
}