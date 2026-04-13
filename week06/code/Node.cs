public class Node
{
    public int Data { get; set; }
    public Node? Right { get; private set; }
    public Node? Left { get; private set; }

    public Node(int data)
    {
        this.Data = data;
    }

    public void Insert(int value)
    {
        if (value == Data)
        {
            return;
        }

        if (value < Data)
        {
            // Insert to the left
            if (Left is null)
                Left = new Node(value);
            else
                Left.Insert(value);
        }
        else
        {
            // Insert to the right
            if (Right is null)
                Right = new Node(value);
            else
                Right.Insert(value);
        }
    }

    public bool Contains(int value)
    {
        // Base Case
        if (value == Data)
        {
            return true;
        }

        // Recursive Step: Navigate Left
        if (value < Data)
        {
            return Left != null && Left.Contains(value);
        }
        // Recursive Step: Navigate Right
        else
        {
            return Right != null && Right.Contains(value);
        }
    }

    public int GetHeight()
    {
        // Recursive Step: Get the height of the left and right subtrees
        int leftHeight = Left?.GetHeight() ?? 0;
        int rightHeight = Right?.GetHeight() ?? 0;

        // Calculation
        return 1 + Math.Max(leftHeight, rightHeight);
    }
}