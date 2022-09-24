using Task_1._1;
 
public class MainSortClass
{
    public static void Main()
    {
        Console.WriteLine("Введите элементы массива через пробел: ");
        var array = Console.ReadLine()?.Split(' ').Select(int.Parse).ToArray();
 
        if (array == null)
        {
            Console.WriteLine("Массив введён неправильно. Попробуйте ещё раз");
        }
        else
        {
            var sortedArray = Sort.SelectionSort(array);
            Console.WriteLine("Отсортированный массив: ");
            foreach (var element in sortedArray)
            {
                Console.Write(element);
                Console.Write(" ");
            }
        }
    }
}