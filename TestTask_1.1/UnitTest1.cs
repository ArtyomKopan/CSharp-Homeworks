using Task_1._1;
 
public class Tests
{
    [Test]
    [Repeat(100)]
    public void CorrectTest()
    {
        var randomGenerator = new Random();
        var size = randomGenerator.Next(500);
        var array = new int[size];
        for (var i = 0; i < size; ++i)
            array[i] = randomGenerator.Next(1000);
        var sortedArray = Sort.SelectionSort(array);
        var sortedList = array.ToList();
        sortedList.Sort();
        Assert.That(sortedList.ToArray(), Is.EqualTo(sortedArray));
    }
 
    [Test]
    [Repeat(10)]
    public void IncorrectTest()
    {
        var randomGenerator = new Random();
        var size = randomGenerator.Next(500);
        var array = new int[size];
        var incorrectArray = new int[size];
        for (var i = 0; i < size; ++i)
        {
            array[i] = randomGenerator.Next(1000);
            incorrectArray[i] = i;
        }
 
        var sortedArray = Sort.SelectionSort(array);
        Assert.That(sortedArray.SequenceEqual(incorrectArray), Is.EqualTo(false));
    }
}