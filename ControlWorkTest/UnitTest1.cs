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

    // здесь мы проверяем выполнение принципа FIFO для очереди
    [Test]
    public void OrderTest()
    {
        var q = new ControlWork.ThreadSafetyQueue<string>();

        q.Enqueue("a", 1);
        q.Enqueue("b", 2);
        q.Enqueue("c", 2);
        q.Enqueue("d", 3);

        var actual = new List<string>();
        while (q.Size() > 0)
        {
            actual.Add(q.Dequeue());
        }

        var expected = new List<string>();
        expected.Add("d");
        expected.Add("b");
        expected.Add("c");
        expected.Add("a");

        Assert.That(actual, Is.EqualTo(expected));
    }

    // здесь мы проверяем корректное поведение при вызове Dequeue() для пустой очереди
    [Test]
    public async Task MultiThreadTest()
    {
        var q = new ControlWork.ThreadSafetyQueue<string>();

        var thread1 = new Task<string>(() => q.Dequeue());
        var thread2 = new Task(() =>
        {
            q.Enqueue("a", 1);
            q.Enqueue("b", 2);
            q.Enqueue("c", 3);
        });

        thread1.Start();
        thread2.Start();

        var s1 = await thread1;
        await thread2;

        Assert.That(s1, Is.EqualTo("a"));
    }


    [Test]
    public async Task MultiThreadTest2()
    {
        var q = new ControlWork.ThreadSafetyQueue<string>();

        var thread1 = new Task<string>(() =>
        {
            var result = q.Dequeue();

            q.Enqueue("a", 10);
            q.Enqueue("b", 2);
            q.Enqueue("c", 2);

            return result;
        });

        var thread2 = new Task<string>(() =>
        {
            q.Enqueue("d", 1);
            q.Enqueue("e", 3);
            q.Dequeue();
            q.Dequeue();
            var result = q.Dequeue();

            return result;
        });

        thread1.Start();
        thread2.Start();

        var (result1, result2) = (await thread1, await thread2);

        Assert.Multiple(() =>
        {
            Assert.That(result1, Is.EqualTo("d"));
            Assert.That(result2, Is.EqualTo("b"));
        });
    }
}