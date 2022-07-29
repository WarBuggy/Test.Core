namespace TestMDM.Distributors
{
    public static class DistributorConsts
    {
        private const string DefaultSorting = "{0}Name asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "Distributor." : string.Empty);
        }

        public const int NameMinLength = 1;
        public const int TaxIdMinLength = 1;
    }
}