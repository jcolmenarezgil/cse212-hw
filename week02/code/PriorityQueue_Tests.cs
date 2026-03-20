using Microsoft.VisualStudio.TestTools.UnitTesting;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Enqueue items with different priorities (Low: 1, High: 5, Medium: 3).
    // Expected Result: High (5) is returned first, then Medium (3), then Low (1).
    // Defect(s) Found: The item is never removed from the queue, causing Dequeue to return the same value repeatedly. 
    // Also, the loop limit skips the last item if it has the highest priority.
    public void TestPriorityQueue_Standard()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("Low", 1);
        priorityQueue.Enqueue("High", 5);
        priorityQueue.Enqueue("Medium", 3);

        Assert.AreEqual("High", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Enqueue multiple items with the same highest priority (A: 10, B: 10, C: 10) to test FIFO requirement.
    // Expected Result: A (The first one added with priority 10).
    // Defect(s) Found: The code uses '>=' which picks the LAST item added (C) instead of the first (A), violating FIFO for ties.
    public void TestPriorityQueue_FIFO_Ties()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("A", 10);
        priorityQueue.Enqueue("B", 10);
        priorityQueue.Enqueue("C", 10);

        Assert.AreEqual("A", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Test if the queue correctly identifies the highest priority when it is the last item added.
    // Expected Result: "LastItem" (Priority 20).
    // Defect(s) Found: The loop condition `index < _queue.Count - 1` causes the algorithm to ignore the last element of the list.
    public void TestPriorityQueue_LastElement()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("First", 5);
        priorityQueue.Enqueue("LastItem", 20);

        Assert.AreEqual("LastItem", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Attempt to Dequeue from an empty queue.
    // Expected Result: InvalidOperationException with message "The queue is empty."
    // Defect(s) Found: None (This part of the code was correctly implemented as a guard clause).
    public void TestPriorityQueue_Empty()
    {
        var priorityQueue = new PriorityQueue();
        try
        {
            priorityQueue.Dequeue();
            Assert.Fail("Exception should have been thrown.");
        }
        catch (InvalidOperationException e)
        {
            Assert.AreEqual("The queue is empty.", e.Message);
        }
    }
}