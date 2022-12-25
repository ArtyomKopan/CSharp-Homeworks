namespace Task_4._1;

public class GetAnswer
{
    public GetAnswer(long size, Byte[] content)
    {
        Size = size;
        Content = content;
    }

    public readonly long Size;
    public readonly byte[] Content;
}