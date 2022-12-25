using System.Collections.Concurrent;

namespace Task_3._1;

public class MyThreadPool
{
    private readonly int _maxThreadsCount;
    private readonly Thread[] _threads;
    private readonly ConcurrentQueue<Action> _tasksQueue = new();
    private readonly AutoResetEvent _event = new(true);
    private readonly CancellationTokenSource _cancellationTokenSource = new();
    public bool IsTerminated { get; private set; }

    public MyThreadPool(int maxThreadsCount)
    {
        if (maxThreadsCount <= 0)
        {
            throw new ArgumentException("Число потоков в пуле должно быть положительным!");
        }

        _maxThreadsCount = maxThreadsCount;
        IsTerminated = false;
        _threads = new Thread[maxThreadsCount];
        StartThreads();
    }

    private void StartThreads()
    {
        for (var i = 0; i < _maxThreadsCount; ++i)
        {
            _threads[i] = new Thread(() =>
                {
                    while (!_cancellationTokenSource.Token.IsCancellationRequested)
                    {
                        if (!_tasksQueue.IsEmpty)
                        {
                            _tasksQueue.TryDequeue(out var result);
                            result?.Invoke();
                        }
                    }
                }
            );

            _threads[i].Start();
        }
    }

    public MyTask<TResult> Submit<TResult>(Func<TResult> function)
    {
        _event.WaitOne();
        var task = new MyTask<TResult>(function, this);
        _tasksQueue.Enqueue(() => task.Start());
        _event.Set();
        return task;
    }

    public void Shutdown()
    {
        if (!IsTerminated)
        {
            _cancellationTokenSource.Cancel();
            for (var i = 0; i < _maxThreadsCount; ++i)
            {
                _threads[i].Join();
            }
        }

        IsTerminated = true;
    }
}