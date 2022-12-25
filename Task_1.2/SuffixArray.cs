namespace Task_1._2
{
    public class SuffixArray
    {
        private string s;
        private List<int> suffixArray = new();

        public List<int> Array => suffixArray;

        public SuffixArray(string input)
        {
            s = input;
            CreateSuffixArray();
        }

        private void CreateSuffixArray()
        {
            for (var i = 0; i < s.Length; ++i)
                suffixArray.Add(i);
            QuickSort(ref suffixArray, 0, suffixArray.Count - 1);
        }

        /*private void insertionSort(ref List<int> a)
        {
            for (var i = 1; i < s.Length; ++i)
            {
                var j = i - 1;
                while (j >= 0 && specialIntComparer(a[j], a[j + 1]) == 1)
                {
                    (a[j], a[j + 1]) = (a[j + 1], a[j]);
                    j--;
                }
            }
        }*/

        private int specialIntComparer(int j, int k)
        {
            for (var i = 0; i < s.Length; ++i)
            {
                if (s[(j + i) % s.Length] > s[(k + i) % s.Length])
                    return 1;
                if (s[(j + i) % s.Length] < s[(k + i) % s.Length])
                    return -1;
            }

            return 0;
        }

        private int Partition(ref List<int> array, int start, int end)
        {
            var marker = start;
            for (var i = start; i < end; ++i)
            {
                if (specialIntComparer(array[i], array[end]) < 0)
                {
                    (array[marker], array[i]) = (array[i], array[marker]);
                    marker++;
                }
            }

            (array[marker], array[end]) = (array[end], array[marker]);
            return marker;
        }

        private void QuickSort(ref List<int> array, int start, int end)
        {
            if (start >= end)
                return;

            var pivot = Partition(ref array, start, end);
            QuickSort(ref array, start, pivot - 1);
            QuickSort(ref array, pivot + 1, end);
        }
    }
}