public class Tests
{
    [Test]
    public void SingleThreadTest()
    {
        var q = new ControlWork.ThreadSafetyQueue<int>();

        for (var i = 1; i < 11; ++i)
        {
            q.Enqueue(i, i);
        }

        var actual = new List<int>();
        while (q.Size() > 0)
        {
            actual.Add(q.Dequeue());
        }

        var expected = new List<int>();
        for (var i = 10; i > 0; --i)
        {
            expected.Add(i);
        }

        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void MultiThreadTest()
    {
        var q = new ControlWork.ThreadSafetyQueue<string>();

        var s1 = "";

        var thread1 = new Thread(() => s1 = q.Dequeue());
        thread1.Start();
        var thread2 = new Thread(() => q.Enqueue("a", 2));
        thread2.Start();

        thread2.Join();
        thread1.Join();

        Assert.That(s1, Is.EqualTo("a"));
    }
}