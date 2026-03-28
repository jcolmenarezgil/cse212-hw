public static class SetUtils
{
    public static HashSet<int> GetIntersection(int[] set1, int[] set2)
{
    var result = new HashSet<int>();
    // Step 01: Convert the first set to a HashSet for O(1) lookups
    var set1Store = new HashSet<int>(set1);

    // Step 02: Iterate through the second set
    foreach (var item in set2)
    {
        // Step 03: Check if the item exists in the first set
        if (set1Store.Contains(item))
        {
            result.Add(item);
        }
    }
    return result; // Performance: O(n)
}
}