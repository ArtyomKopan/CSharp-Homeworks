namespace Task_3._1;

public class MyTask<TResult> : IMyTask<TResult>
{
    private readonly Func<TResult> _function;
    private readonly MyThreadPool _threadPool;
    private readonly AutoResetEvent _event = new(true);
    private TResult _result;

    public bool IsCompleted { get; private set; }

    public TResult Result
    {
        get
        {
            _event.WaitOne();

            if (_hasException)
            {
                throw _taskException;
            }

            if (!IsCompleted)
            {
                Start();
            }

            _event.Set();
            return _result;
        }
    }

    private AggregateException _taskException;
    private bool _hasException = false;

    public MyTask(Func<TResult> function, MyThreadPool threadPool)
    {
        _function = function;
        _threadPool = threadPool;
        IsCompleted = false;
    }

    public IMyTask<TNewResult> ContinueWith<TNewResult>(Func<TResult, TNewResult> function)
    {
        if (_threadPool.IsTerminated)
        {
            _taskException = new AggregateException("Thread pool was terminate",
                new ThreadPoolException("Thread pool was terminate"));
            _hasException = true;
        }

        return _threadPool.Submit(() => function(Result));
    }

    public void Start()
    {
        try
        {
            _result = _function();
            IsCompleted = true;
        }
        catch (Exception e)
        {
            _hasException = true;
            _taskException = new AggregateException("Результат не был подсчитан", e);
        }
    }
}