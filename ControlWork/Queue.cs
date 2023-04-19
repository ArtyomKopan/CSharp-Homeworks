namespace ControlWork;

public class ThreadSafetyQueue<T, P>
    where P : IComparable
{
    private readonly List<QueueElement<T, P>> _elements = new();
    private readonly object _locker = new();
    private int _size = 0;

    public void Enqueue(T element, P priority)
    {
        lock (_locker)
        {
            _elements.Add(new QueueElement<T, P>(element, priority));
        }

        Interlocked.Increment(ref _size);
        Thread.Sleep(100);
    }

    public T Dequeue()
    {
        lock (_locker)
        {
            if (_elements.Count == 0)
            {
                Monitor.PulseAll(_locker);
                // каждые 100 мс проверяем, есть ли что-нибудь в очереди
                while (true)
                {
                    if (_elements.Count > 0)
                    {
                        break;
                    }

                    Thread.Sleep(100);
                }

                Monitor.Wait(_locker);
            }

            var returningElement = _elements[0];
            for (var i = 1; i < _elements.Count; ++i)
            {
                if (_elements[i].Priority.CompareTo(returningElement.Priority) > 0)
                    //if (_elements[i].Priority > returningElement.Priority)
                {
                    returningElement = _elements[i];
                }
            }

            _elements.Remove(returningElement);

            Interlocked.Decrement(ref _size);

            return returningElement.Value;
        }
    }

    public int Size()
    {
        lock (_locker)
        {
            return _size;
        }
    }
}