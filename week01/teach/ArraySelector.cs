public static class ArraySelector
{
    public static void Run()
    {
        var l1 = new[] { 1, 2, 3, 4, 5 };
        var l2 = new[] { 2, 4, 6, 8, 10};
        var select = new[] { 1, 1, 1, 2, 2, 1, 2, 2, 2, 1};
        var intResult = ListSelector(l1, l2, select);
        Console.WriteLine("<int[]>{" + string.Join(", ", intResult) + "}"); // <int[]>{1, 2, 3, 2, 4, 4, 6, 8, 10, 5}
    }

    private static int[] ListSelector(int[] list1, int[] list2, int[] select)
    {
        // TODO problem 2
        var result = new int[select.Length];
        var listIdx1 = 0;
        var listIdx2 = 0;

        for(var index = 0; index < select.Length; index++)
        {
            if (select[index] == 1)
            {
                result[index] = list1[listIdx1++];
            }
            else
            {
                result[index] = list2[listIdx2++]; 
            }
        }
        return result;
    }
}