[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("PhotoMetadata.UnitTests")]

namespace PhotoMetadata;

using System.Collections.Generic;
using System.Linq;
using PhotoMetadata.Models;

internal static partial class Helpers
{
    /// <summary>
    /// Compare stores to see if anything has changed
    /// Determines the items that have changed or been added
    /// between the currentStore items and the previousStore
    /// items
    /// </summary>
    public static List<InputMetadata> GetNewAndUpdatedMetadata(List<InputMetadata> first, List<InputMetadata> second)
    {
        var result = first;

        if (second != null)
        {
            result = (from item in first
                      join other in second on item.PhotoId equals other.PhotoId into gj
                      from subItem in gj.DefaultIfEmpty()
                      where item.Equals(subItem) == false
                      select item)
                     .ToList();
        }

        return result;
    }
}
