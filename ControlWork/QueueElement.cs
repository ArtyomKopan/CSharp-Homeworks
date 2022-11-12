namespace ControlWork;

public class QueueElement<T>
{
    public readonly T Value;
    public readonly int Priority;

    public QueueElement(T value, int priority)
    {
        Value = value;
        Priority = priority;
    }
}