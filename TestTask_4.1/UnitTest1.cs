using Task_4._1;

public class Tests
{
    [Test]
    public void ServerListTest1()
    {
        var server = new Server(1, 16, "/home/artyom");
        var result = server.List("/home/artyom/Документы");
        Assert.That(result.Item1, Is.EqualTo(29));
    }

    [Test]
    public void ServerListTest2()
    {
        var server = new Server(1, 16, "/home/artyom");
        var result = server.List("Документы");
        Assert.That(result.Item1, Is.EqualTo(29));
    }

    [Test]
    public async Task ServerGetTest1()
    {
        var server = new Server(1, 16, "/home/artyom");
        var result = await server.Get("/home/artyom/Документы/Game of Thrones (EN).pdf");
        Assert.That(result.Size, Is.EqualTo(4921287));
    }

    [Test]
    public async Task ServerGetTest2()
    {
        var server = new Server(1, 16, "/home/artyom");
        var result = await server.Get("Документы/Game of Thrones (EN).pdf");
        Assert.That(result.Size, Is.EqualTo(4921287));
    }

    [Test]
    public async Task ClientGetTest1()
    {
        var server = new Server(1, 16, "/home/artyom");
        var client = new Client();
        var result = await client.Get(server, 1, "/home/artyom/Документы/Game of Thrones (EN).pdf");
        Assert.That(result.Size, Is.EqualTo(4921287));
    }

    [Test]
    public async Task ClientGetTest2()
    {
        var server = new Server(1, 16, "/home/artyom");
        var client = new Client();
        var result = await client.Get(server, 1, "Документы/Game of Thrones (EN).pdf");
        Assert.That(result.Size, Is.EqualTo(4921287));
    }

    [Test]
    public void ClientListTest1()
    {
        var server = new Server(1, 16, "/home/artyom");
        var client = new Client();
        var result = client.List(server, 1, "/home/artyom/Документы");
        Assert.That(result.Item1, Is.EqualTo(29));
    }

    [Test]
    public void ClientListTest2()
    {
        var server = new Server(1, 16, "/home/artyom");
        var client = new Client();
        var result = client.List(server, 1, "Документы");
        Assert.That(result.Item1, Is.EqualTo(29));
    }
}