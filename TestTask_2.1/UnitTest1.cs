using Task_2._1;

public class Tests
{
    private static double f() => Math.Exp(100);

    private static Int64 g() => (Int64)Int32.MaxValue * Int32.MaxValue;

    [Test]
    public void SingleThreadTest1()
    {
        var lazy = LazyFactory<double>.CreateSingleThreadLazy(f);
        var e1 = lazy.Get();
        var e2 = lazy.Get();
        Assert.That(e2, Is.EqualTo(e1));
    }

    [Test]
    public void SingleThreadTest2()
    {
        var lazy = LazyFactory<Int64>.CreateSingleThreadLazy(g);
        var e1 = lazy.Get();
        var e2 = lazy.Get();
        Assert.That(e2, Is.EqualTo(e1));
    }

    [Test]
    public void MultiThreadTest1()
    {
        var e1 = 0.0;
        var e2 = 0.0;
        var lazy = LazyFactory<double>.CreateMultiThreadLazy(f);
        var thread1 = new Thread(() => e1 = lazy.Get());
        var thread2 = new Thread(() => e2 = lazy.Get());
        thread1.Start();
        thread2.Start();
        thread1.Join();
        thread2.Join();
        Assert.That(e2, Is.EqualTo(e1));
    }

    [Test]
    public void MultiThreadTest2()
    {
        Int64 e1 = 0;
        Int64 e2 = 0;
        var lazy = LazyFactory<Int64>.CreateMultiThreadLazy(g);
        var thread1 = new Thread(() => e1 = lazy.Get());
        var thread2 = new Thread(() => e2 = lazy.Get());
        thread1.Start();
        thread2.Start();
        thread1.Join();
        thread2.Join();
        Assert.That(e2, Is.EqualTo(e1));
    }
}