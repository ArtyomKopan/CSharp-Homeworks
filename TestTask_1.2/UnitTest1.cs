using Task_1._2;

public class Tests
{
    private bool ComparePairs(KeyValuePair<string, int> a, KeyValuePair<string, int> b)
    {
        return a.Key == b.Key && a.Value == b.Value;
    }

    [Test]
    public void StraightBWTTest1()
    {
        var s = "abracadabra";
        var answer = new KeyValuePair<string, int>("rdarcaaaabb", 2);
        Assert.That(ComparePairs(answer, BWT.StraightBWT(s)), Is.EqualTo(true));
    }

    [Test]
    public void StraightBWTTest2()
    {
        var s = "ABACABA";
        var answer = new KeyValuePair<string, int>("BCABAAA", 2);
        Assert.That(ComparePairs(answer, BWT.StraightBWT(s)), Is.EqualTo(true));
    }

    [Test]
    public void InverseBWTTest1()
    {
        var result = new KeyValuePair<string, int>("rdarcaaaabb", 2);
        Assert.That(BWT.LinearInverseBWT(result), Is.EqualTo("abracadabra"));
    }

    [Test]
    public void InverseBWTTest2()
    {
        var result = new KeyValuePair<string, int>("BCABAAA", 2);
        Assert.That(BWT.LinearInverseBWT(result), Is.EqualTo("ABACABA"));
    }
}