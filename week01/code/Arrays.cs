public static class Arrays
{
    /// <summary>
    /// This function will produce an array of size 'length' starting with 'number' followed by multiples of 'number'.  For 
    /// example, MultiplesOf(7, 5) will result in: {7, 14, 21, 28, 35}.  Assume that length is a positive
    /// integer greater than 0.
    /// </summary>
    /// <returns>array of doubles that are the multiples of the supplied number</returns>
    public static double[] MultiplesOf(double number, int length)
    {
        // 1. Declare and initializar vars
        double[] multiples = new double[length];

        // 2. Use a for loop to play array
        for (int index = 0; index < length; index++)
        {
            // 3. To calculate the multiple
            multiples[index] = number * (index + 1);

        }

        // 4. Return the array
        return multiples;
    }

    /// <summary>
    /// Rotate the 'data' to the right by the 'amount'.  For example, if the data is 
    /// List<int>{1, 2, 3, 4, 5, 6, 7, 8, 9} and an amount is 3 then the list after the function runs should be 
    /// List<int>{7, 8, 9, 1, 2, 3, 4, 5, 6}.  The value of amount will be in the range of 1 to data.Count, inclusive.
    ///
    /// Because a list is dynamic, this function will modify the existing data list rather than returning a new list.
    /// </summary>
    public static void RotateListRight(List<int> data, int amount)
    {
        // 1. Calculate the starting index by subtracting the rotation amount from the total count
        int startIndex = data.Count - amount;

        // 2. Create a temporary list containing the segment of elements to be rotated.
        List<int> pivotPart = data.GetRange(startIndex, amount);

        // 3. Delete the range of elements that have already been copied.
        data.RemoveRange(startIndex, amount);

        // 4. Insert the saved segment at the beginning of the list to complete the rotation
        data.InsertRange(0, pivotPart);
    }
}
