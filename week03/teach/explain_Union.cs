public static class SetOperations
{
    public static HashSet<int> GetUnion(int[] set1, int[] set2)
{
    // Step 01: Initialize the HashSet with the first set
    var resultSet = new HashSet<int>(set1);

    // Step 02: Add all elements from the second set
    foreach (var item in set2)
    {
        // HashSet handles duplicates automatically
        resultSet.Add(item);
    }

    return resultSet; // Performance: O(n + m)
}
}