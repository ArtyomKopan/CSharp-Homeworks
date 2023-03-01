using System.Runtime.CompilerServices;

namespace Task_4._1;

using System.IO;

public class Server
{
    public Server(int address, int portsCount, string rootDirectory) //(int address, string rootDirectory)
    {
        Address = address;
        _rootDirectory = rootDirectory;
        _ports = new bool[portsCount];
    }

    public readonly int Address;
    private readonly bool[] _ports; // true -> порт занят, false -> порт свободен
    private readonly string _rootDirectory;

    public bool TakePort(int port)
    {
        if (port < 0 || port >= _ports.Length || _ports[port])
        {
            return false;
        }

        _ports[port] = true;
        return true;
    }

    public void ReleasePort(int port)
    {
        if (port >= 0 && port < _ports.Length)
        {
            _ports[port] = false;
        }
    }

    private bool _isDirectory(string path) =>
        Directory.Exists(Directory.GetDirectoryRoot(_rootDirectory)[1..] + _rootDirectory + path) ||
        Directory.Exists(Directory.GetDirectoryRoot(path)[1..] + path);

    public Tuple<int, List<ListAnswer>> List(string path)
    {
        if (path[0] != '/')
        {
            path = "/" + path;
        }

        if (!_isDirectory(path))
        {
            return new Tuple<int, List<ListAnswer>>(-1, new List<ListAnswer>());
        }

        var dirs = Directory.Exists(Directory.GetDirectoryRoot(path)[1..] + path)
            ? Directory.GetDirectories(Directory.GetDirectoryRoot(path)[1..] + path)
            : Directory.GetDirectories(Directory.GetDirectoryRoot(_rootDirectory)[1..] + _rootDirectory + path);

        var files = Directory.Exists(Directory.GetDirectoryRoot(path)[1..] + path)
            ? Directory.GetFiles(Directory.GetDirectoryRoot(path)[1..] + path)
            : Directory.GetFiles(Directory.GetDirectoryRoot(_rootDirectory)[1..] + _rootDirectory + path);

        var resultSize = dirs.Length + files.Length;
        var resultList = dirs.Select(dir => new ListAnswer(dir, true)).ToList();
        resultList.AddRange(files.Select(file => new ListAnswer(file, false)));

        return new Tuple<int, List<ListAnswer>>(resultSize, resultList);
    }

    public async Task<GetAnswer> Get(string path)
    {
        if (path[0] != '/')
        {
            path = "/" + path;
        }

        if (!File.Exists(Directory.GetDirectoryRoot(_rootDirectory)[1..] + _rootDirectory + path) &&
            !File.Exists(Directory.GetDirectoryRoot(path)[1..] + path))
        {
            return new GetAnswer(-1, Array.Empty<byte>());
        }

        path = File.Exists(Directory.GetDirectoryRoot(path)[1..] + path)
            ? Directory.GetDirectoryRoot(path)[1..] + path
            : Directory.GetDirectoryRoot(_rootDirectory)[1..] + _rootDirectory + path;

        var fileData = await File.ReadAllBytesAsync(path);

        return new GetAnswer(fileData.Length, fileData);
    }
}