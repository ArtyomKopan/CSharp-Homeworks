namespace Task_1._2
{
    public class SuffixArray
    {
        private readonly string _originalString;

        public List<int> Array { get; } = new();

        public SuffixArray(string inputOriginalString)
        {
            _originalString = inputOriginalString;
            CreateSuffixArray();
        }

        private void CreateSuffixArray()
        {
            for (var i = 0; i < _originalString.Length; ++i)
                Array.Add(i);
            Array.Sort((i, j) => string.Compare(_originalString[i..] + _originalString[..i],
                _originalString[j..] + _originalString[..j], StringComparison.Ordinal));
        }
    }
}