using Task_4._1;

var server = new Server(1, 16, "/home/artyom");

var client = new Client();

var listResult = client.List(server, 1,"/home/artyom/Документы");
Console.WriteLine(listResult.Item1);
foreach (var listAnswer in listResult.Item2)
{
    Console.WriteLine($"{listAnswer.Name} {listAnswer.IsDir}");
}

var getResult = await client.Get(server, 1, "Документы/Game of Thrones (EN).pdf");
Console.WriteLine(getResult.Size);
