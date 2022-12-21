using Task_3._1;
using AggregateException = Task_3._1.AggregateException;

public class Tests
{
    private const int ThreadsCount = 8;

    [Test]
    public void SubmitTest()
    {
        var threadPool = new MyThreadPool(ThreadsCount);
        var task = threadPool.Submit(() => 1000 * 1000);
        Thread.Sleep(1000);
        threadPool.Shutdown();
        Assert.That(task.IsCompleted && task.Result == 1000000, Is.True);
    }

    [Test]
    public void MyTaskTest()
    {
        var threadPool = new MyThreadPool(ThreadsCount);
        var task = new MyTask<int>(() => 1000 * 1000, threadPool);
        threadPool.Shutdown();
        Assert.That(task.Result, Is.EqualTo(1000000));
    }

    [Test]
    public void EarlyStartMyTaskTest()
    {
        var threadPool = new MyThreadPool(ThreadsCount);
        var task = new MyTask<int>(() => 1000 * 1000, threadPool);
        task.Start();
        Thread.Sleep(500);
        threadPool.Shutdown();
        Assert.That(task.IsCompleted && task.Result == 1000000, Is.True);
    }

    [Test]
    public void ContinuationWithSameTypeTest()
    {
        var threadPool = new MyThreadPool(ThreadsCount);
        var task = threadPool.Submit(() => 1000 * 1000);
        var continuation = task.ContinueWith(result => result * 2);
        var result = continuation.Result;
        threadPool.Shutdown();
        Assert.That(continuation.IsCompleted && result == 2000000, Is.True);
    }

    [Test]
    public void ContinuationWithDifferentTypeTest()
    {
        var threadPool = new MyThreadPool(ThreadsCount);
        var task = threadPool.Submit(() => 1000 * 1000);
        var continuation = task.ContinueWith(result => result.ToString());
        var result = continuation.Result;
        threadPool.Shutdown();
        Assert.That(continuation.IsCompleted && result == "1000000", Is.True);
    }

    [Test]
    public void AggregateExceptionTest()
    {
        var threadPool = new MyThreadPool(ThreadsCount);
        threadPool.Shutdown();
        try
        {
            var task = threadPool.Submit(() => 1);
        }
        catch (Exception e)
        {
            Assert.That(e is AggregateException { InnerException: ThreadPoolException }, Is.True);
        }
    }

    [Test]
    public void CorrectShutdownTest()
    {
        var threadPool = new MyThreadPool(ThreadsCount);
        var task1 = new MyTask<int>(() => 1000 * 1000, threadPool);
        var task2 = new MyTask<int>(() => 2 * 2, threadPool);
        task1.Start();
        task2.Start();
        threadPool.Shutdown();
        Assert.That(task1.IsCompleted && task2.IsCompleted, Is.True);
    }

    [Test]
    public void LargeCountTasksTest()
    {
        var threadPool = new MyThreadPool(ThreadsCount);
        var tasks = new List<MyTask<int>>();
        for (var i = 0; i < 100; ++i)
        {
            var i1 = i;
            tasks.Add(threadPool.Submit(() => 2 * i1));
        }

        Thread.Sleep(1000);

        threadPool.Shutdown();

        for (var i = 0; i < 100; ++i)
        {
            Assert.That(tasks[i].IsCompleted && tasks[i].Result == 2 * i, Is.True);
        }
    }

    [Test]
    public void ThreadsSplitTest()
    {
        var threadPool = new MyThreadPool(ThreadsCount);
        MyTask<int>? task1 = null;
        MyTask<int>? task2 = null;
        var thread1 = new Thread(() => task1 = threadPool.Submit(() => 1000 * 1000));
        var thread2 = new Thread(() => task2 = threadPool.Submit(() => 2 * 2));
        thread1.Start();
        thread2.Start();
        thread1.Join();
        thread2.Join();
        threadPool.Shutdown();
        Assert.That(task1!.IsCompleted && task2!.IsCompleted, Is.True);
    }

    [Test]
    public void ThreadsCountTest()
    {
        var threadPool = new MyThreadPool(ThreadsCount);
        var tasks = new List<MyTask<int>>();

        for (var i = 0; i < 8; ++i)
        {
            tasks.Add(threadPool.Submit(GetThreadId));
        }

        Thread.Sleep(2000);

        var results = tasks.Select(task => task.Result).ToList();

        threadPool.Shutdown();

        // Assert.That(results.Distinct().Count(), Is.EqualTo(results.Count));
        var threadIdsCount = results.Distinct().Count();
        Assert.That(threadIdsCount == results.Count && threadIdsCount == ThreadsCount, Is.True);
    }

    private static int GetThreadId()
    {
        Thread.Sleep(300);
        return Environment.CurrentManagedThreadId;
    }
}