using System.Collections;

public static class Recursion
{
    /// <summary>
    /// #############
    /// # Problem 1 #
    /// #############
    /// Using recursion, find the sum of 1^2 + 2^2 + 3^2 + ... + n^2
    /// and return it.  Remember to both express the solution 
    /// in terms of recursive call on a smaller problem and 
    /// to identify a base case (terminating case).  If the value of
    /// n <= 0, just return 0.   A loop should not be used.
    /// </summary>
    public static int SumSquaresRecursive(int n)
    {
        // 1. Base Case (Emergency Exit): 
        // If n reaches 0 or less, the recursion terminates and we return 0.
        if (n <= 0)
        {
            return 0;
        }

        // 2. Recursive Step:
        // Calculate the square of the current value (n * n) 
        // and add the result of the smaller sub-problem (n - 1).
        return (n * n) + SumSquaresRecursive(n - 1);
    }

    /// <summary>
    /// #############
    /// # Problem 2 #
    /// #############
    /// Using recursion, insert permutations of length
    /// 'size' from a list of 'letters' into the results list.  This function
    /// should assume that each letter is unique (i.e. the 
    /// function does not need to find unique permutations).
    ///
    /// In mathematics, we can calculate the number of permutations
    /// using the formula: len(letters)! / (len(letters) - size)!
    ///
    /// For example, if letters was [A,B,C] and size was 2 then
    /// the following would the contents of the results array after the function ran: AB, AC, BA, BC, CA, CB (might be in 
    /// a different order).
    ///
    /// You can assume that the size specified is always valid (between 1 
    /// and the length of the letters list).
    /// </summary>
    public static void PermutationsChoose(List<string> results, string letters, int size, string word = "")
    {
        // 1. Base Case: 
        if (word.Length == size)
        {
            results.Add(word);
            return;
        }

        // 2. Recursive Step (Backtracking):
        for (int i = 0; i < letters.Length; i++)
        {
            // Extract the chosen letter to prevent duplicate use in the current branch.
            char chosenLetter = letters[i];
            string remainingLetters = letters.Remove(i, 1);

            // Recursive call with the sub-problem:
            PermutationsChoose(results, remainingLetters, size, word + chosenLetter);
        }
    }

    /// <summary>
    /// #############
    /// # Problem 3 #
    /// #############
    /// Imagine that there was a staircase with 's' stairs.  
    /// We want to count how many ways there are to climb 
    /// the stairs.  If the person could only climb one 
    /// stair at a time, then the total would be just one.  
    /// However, if the person could choose to climb either 
    /// one, two, or three stairs at a time (in any order), 
    /// then the total possibilities become much more 
    /// complicated.  If there were just three stairs,
    /// the possible ways to climb would be four as follows:
    ///
    ///     1 step, 1 step, 1 step
    ///     1 step, 2 step
    ///     2 step, 1 step
    ///     3 step
    ///
    /// With just one step to go, the ways to get
    /// to the top of 's' stairs is to either:
    ///
    /// - take a single step from the second to last step, 
    /// - take a double step from the third to last step, 
    /// - take a triple step from the fourth to last step
    ///
    /// We don't need to think about scenarios like taking two 
    /// single steps from the third to last step because this
    /// is already part of the first scenario (taking a single
    /// step from the second to last step).
    ///
    /// These final leaps give us a sum:
    ///
    /// CountWaysToClimb(s) = CountWaysToClimb(s-1) + 
    ///                       CountWaysToClimb(s-2) +
    ///                       CountWaysToClimb(s-3)
    ///
    /// To run this function for larger values of 's', you will need
    /// to update this function to use memoization.  The parameter
    /// 'remember' has already been added as an input parameter to 
    /// the function for you to complete this task.
    /// </summary>
    public static decimal CountWaysToClimb(int s, Dictionary<int, decimal>? remember = null)
    {
        // Initialize memoization storage if not provided in the initial call
        remember ??= new Dictionary<int, decimal>();

        // Define base cases for the recurrence relation to prevent infinite recursion
        if (s <= 0) return 0;
        if (s == 1) return 1;
        if (s == 2) return 2;
        if (s == 3) return 4;

        // Check if the result for the current input has already been computed
        // Returns the cached value to ensure O(1) retrieval time
        if (remember.ContainsKey(s))
        {
            return remember[s];
        }

        // Perform recursive branching to calculate the sum of the three possible steps
        // Pass the memoization dictionary to maintain state across the call stack
        decimal ways = CountWaysToClimb(s - 1, remember) +
                       CountWaysToClimb(s - 2, remember) +
                       CountWaysToClimb(s - 3, remember);

        // Cache the computed result in the dictionary for future O(1) access
        remember[s] = ways;

        return ways;
    }

    /// <summary>
    /// #############
    /// # Problem 4 #
    /// #############
    /// A binary string is a string consisting of just 1's and 0's.  For example, 1010111 is 
    /// a binary string.  If we introduce a wildcard symbol * into the string, we can say that 
    /// this is now a pattern for multiple binary strings.  For example, 101*1 could be used 
    /// to represent 10101 and 10111.  A pattern can have more than one * wildcard.  For example, 
    /// 1**1 would result in 4 different binary strings: 1001, 1011, 1101, and 1111.
    ///	
    /// Using recursion, insert all possible binary strings for a given pattern into the results list.  You might find 
    /// some of the string functions like IndexOf and [..X] / [X..] to be useful in solving this problem.
    /// </summary>
    public static void WildcardBinary(string pattern, List<string> results)
    {
        // Locate the index of the first occurrence of the wildcard character
        int index = pattern.IndexOf('*');

        // Base Case: If no wildcards remain, the string is fully resolved
        // Add the literal binary string to the results collection and terminate the branch
        if (index == -1)
        {
            results.Add(pattern);
            return;
        }

        // Recursive Step: Execute binary branching by replacing the wildcard
        // Slice and concatenate the string to inject '0' at the current index, then recurse
        string patternWithZero = pattern[..index] + "0" + pattern[(index + 1)..];
        WildcardBinary(patternWithZero, results);

        // Slice and concatenate the string to inject '1' at the current index, then recurse
        string patternWithOne = pattern[..index] + "1" + pattern[(index + 1)..];
        WildcardBinary(patternWithOne, results);
    }

    /// <summary>
    /// Use recursion to insert all paths that start at (0,0) and end at the
    /// 'end' square into the results list.
    /// </summary>
    public static void SolveMaze(List<string> results, Maze maze, int x = 0, int y = 0, List<ValueTuple<int, int>>? currPath = null)
    {
        // Initialize the path collection if this is the entry point of the recursion
        if (currPath == null)
        {
            currPath = new List<ValueTuple<int, int>>();
        }

        // 1. Validation Gateway:
        // Invoke IsValidMove to verify boundaries, collision data, and cycle prevention.
        // Must be evaluated BEFORE adding to currPath to avoid self-collision detection.
        if (!maze.IsValidMove(currPath, x, y))
        {
            return;
        }

        // 2. State Progression:
        // Push the current coordinate into the path stack
        currPath.Add((x, y));

        // 3. Termination Logic (Target Found):
        if (maze.IsEnd(x, y))
        {
            // Snapshot the current valid path state to the results list
            results.Add(currPath.AsString());
        }
        else
        {
            // 4. Recursive Exploration:
            // Branch out into all adjacent cardinal coordinates
            SolveMaze(results, maze, x + 1, y, currPath); // East
            SolveMaze(results, maze, x - 1, y, currPath); // West
            SolveMaze(results, maze, x, y + 1, currPath); // South
            SolveMaze(results, maze, x, y - 1, currPath); // North
        }

        // 5. Memory Recovery (Backtracking):
        // Pop the current coordinate to restore the stack state for sibling branches
        currPath.RemoveAt(currPath.Count - 1);
    }
}