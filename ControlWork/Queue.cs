namespace ControlWork;

public class ThreadSafetyQueue<T>
{
    private readonly List<QueueElement<T>> _elements = new();
    private int _size = 0;

    public void Enqueue(T element, int priority)
    {
        lock (_elements)
        {
            _elements.Add(new QueueElement<T>(element, priority));
        }

        Interlocked.Increment(ref _size);
        Thread.Sleep(100);
    }

    public T Dequeue()
    {
        lock (_elements)
        {
            if (_elements.Count == 0)
            {
                Monitor.PulseAll(_elements);
                // каждые 100 мс проверяем, есть ли что-нибудь в очереди
                while (true)
                {
                    if (_elements.Count > 0)
                    {
                        break;
                    }

                    Thread.Sleep(100);
                }

                Monitor.Wait(_elements);
            }

            var returningElement = _elements[0];
            for (var i = 1; i < _elements.Count; ++i)
            {
                if (_elements[i].Priority > returningElement.Priority)
                {
                    returningElement = _elements[i];
                }
            }

            _elements.Remove(returningElement);

            Interlocked.Decrement(ref _size);

            return returningElement.Value;
        }
    }

    public int Size() => _size;
}