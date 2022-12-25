namespace Task_4._1;

public class Client
{
    private Server? _connectedServer;
    private int _connectedPort;

    public Client()
    {
        _connectedServer = null;
        _connectedPort = -1;
    }

    public Client(Server server, int port)
    {
        _connectedServer = server;
        _connectedPort = port;
    }
    
    private void Connect(Server server, int port)
    {
        _connectedServer = server;
        _connectedPort = port;
        if (!server.TakePort(port))
        {
            throw new ConnectException("Подключение к занятому порту");
        }
    }

    private void Disconnect()
    {
        _connectedServer?.ReleasePort(_connectedPort);
        _connectedServer = null;
        _connectedPort = -1;
    }

    public Tuple<int, List<ListAnswer>> List(Server server, int port, string path)
    {
        if (server != _connectedServer || port != _connectedPort)
        {
            Disconnect();
            Connect(server, port);
        }
        if (_connectedServer == null || _connectedPort == -1)
        {
            throw new ConnectException("Вы пытаетесь выполнить операцию List для несуществующего сервера");
        }

        return _connectedServer.List(path);
    }

    public async Task<GetAnswer> Get(Server server, int port, string path)
    {
        if (server != _connectedServer || port != _connectedPort)
        {
            Disconnect();
            Connect(server, port);
        }
        if (_connectedServer == null || _connectedPort == -1)
        {
            throw new ConnectException("Вы пытаетесь выполнить операцию Get для несуществующего сервера");
        }

        return await _connectedServer.Get(path);
    }
}