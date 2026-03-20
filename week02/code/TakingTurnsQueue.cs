/// <summary>
/// This queue is circular.  When people are added via AddPerson, then they are added to the 
/// back of the queue (per FIFO rules).  When GetNextPerson is called, the next person
/// in the queue is saved to be returned and then they are placed back into the back of the queue.  Thus,
/// each person stays in the queue and is given turns.  When a person is added to the queue, 
/// a turns parameter is provided to identify how many turns they will be given.  If the turns is 0 or
/// less than they will stay in the queue forever.  If a person is out of turns then they will 
/// not be added back into the queue.
/// </summary>
public class TakingTurnsQueue
{
    private readonly PersonQueue _people = new();

    public int Length => _people.Length;

    /// <summary>
    /// Add new people to the queue with a name and number of turns
    /// </summary>
    /// <param name="name">Name of the person</param>
    /// <param name="turns">Number of turns remaining</param>
    public void AddPerson(string name, int turns)
    {
        var person = new Person(name, turns);
        _people.Enqueue(person);
    }

    /// <summary>
    /// Get the next person in the queue and return them. The person should
    /// go to the back of the queue again unless the turns variable shows that they 
    /// have no more turns left.  Note that a turns value of 0 or less means the 
    /// person has an infinite number of turns.  An error exception is thrown 
    /// if the queue is empty.
    /// </summary>
    public Person GetNextPerson()
    {
        // Step 1: Verfify if someone is waiting
        if (_people.IsEmpty())
        {
            // If empty: inform and stop excecution
            throw new InvalidOperationException("No one in the queue.");
        }

        // Step 2: Get a single person
        Person person = _people.Dequeue();

        // Step 3: Decide if person nedded return to the end queue

        // RULE #1: If person have infinite turns (0 or less)
        if (person.Turns <= 0)
        {
            // Person come back queue again
            _people.Enqueue(person);
        }

        // RULE: If person have more than 1 turn
        else if (person.Turns > 1)
        {
            // Decrement turn for the person
            person.Turns -= 1;
            _people.Enqueue(person);
        }

        // If persona have only one turn, do not come back to queue
        
        // Return the person queue state
        return person;
    }

    public override string ToString()
    {
        return _people.ToString();
    }
}