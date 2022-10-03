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
            insertionSort(ref suffixArray);
        }

        private void insertionSort(ref List<int> a)
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
        }

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
    }
}