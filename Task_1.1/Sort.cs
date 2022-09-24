namespace Task_1._1
{
    public class Sort
    {
        public static int[] SelectionSort(int[] array)
        {
            var n = array.Length;
            for (var i = 0; i < n - 1; ++i)
                for (var j = i + 1; j < n; ++j)
                    if (array[i] > array[j])
                        (array[i], array[j]) = (array[j], array[i]);
            return array;
        }
    }
}