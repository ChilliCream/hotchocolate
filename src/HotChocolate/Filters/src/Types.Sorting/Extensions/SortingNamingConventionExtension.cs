using System;
using HotChocolate.Types.Descriptors;

namespace HotChocolate.Types.Sorting
{
    [Obsolete("Use HotChocolate.Data.")]
    public static class SortingNamingConventionExtension
    {
        public static ISortingNamingConvention GetSortingNamingConvention(
           this IDescriptorContext context)
        {
            return context.GetConventionOrDefault(SortingNamingConventionBase.Default);
        }
    }
}
