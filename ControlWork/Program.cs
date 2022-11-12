using ControlWork;

var q = new ThreadSafetyQueue<string>();

q.Enqueue("a", 1);
q.Enqueue("b", 2);
q.Enqueue("c", 2);
q.Enqueue("d", 3);

while (q.Size() > 0)
{
    Console.WriteLine(q.Dequeue());
}