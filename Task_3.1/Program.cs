using Task_3._1;

var threadPool = new MyThreadPool(8);

var taskList = new List<MyTask<int>>();

for (var i = 16; i >= 1; --i)
{
    var i1 = i;
    var function = new Func<int>(() =>
    {
        Console.WriteLine($"Задача № {i1} принята к исполнению");
        return (int)Math.Pow(2, i1);
    });

    var task = new MyTask<int>(function, threadPool);
    taskList.Add(task);
}

foreach (var task in taskList)
{
    var result = task.Result;
    Console.WriteLine($"Result {(int)Math.Log2(result)} = {result}");
    var newTask = task.ContinueWith(r =>
    {
        var newArgument = (int)Math.Log2(r) / 2;
        Console.WriteLine($"Задача № {newArgument} принята к исполнению");
        return (int)Math.Pow(2, newArgument);
    });
    var newResult = newTask.Result;
    Console.WriteLine($"Continuation {(int)Math.Log2(result) / 2} Result = {newResult}");
}

threadPool.Shutdown();