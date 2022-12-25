using Task_1._2;

public class Tests
{
    [Test]
    public void StraightBwtTest1()
    {
        var originalString = "abracadabra";
        var answer = ("rdarcaaaabb", 2);
        Assert.That(Bwt.StraightBwt(originalString), Is.EqualTo(answer));
    }

    [Test]
    public void StraightBwtTest2()
    {
        var originalString = "ABACABA";
        var answer = ("BCABAAA", 2);
        Assert.That(Bwt.StraightBwt(originalString), Is.EqualTo(answer));
    }

    [Test]
    public void InverseBwtTest1()
    {
        var resultString = ("rdarcaaaabb", 2);
        Assert.That(Bwt.LinearInverseBwt(resultString), Is.EqualTo("abracadabra"));
    }

    [Test]
    public void InverseBwtTest2()
    {
        var result = ("BCABAAA", 2);
        Assert.That(Bwt.LinearInverseBwt(result), Is.EqualTo("ABACABA"));
    }
}