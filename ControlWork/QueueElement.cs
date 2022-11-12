namespace ControlWork;

public class QueueElement<T, P>
    where P : IComparable
{
    public readonly T Value;
    public readonly P Priority;

    public QueueElement(T value, P priority)
    {
        Value = value;
        Priority = priority;
    }
}