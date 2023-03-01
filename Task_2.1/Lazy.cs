namespace Task_2._1
{
    public interface ILazy<T>
    {
        public T? Get();
    }

    public class SingleThreadLazy<T> : ILazy<T>
    {
        private T? value;
        private bool isComputed;
        private Func<T> function;

        public SingleThreadLazy(Func<T> f)
        {
            function = f;
            value = default(T);
            isComputed = false;
        }

        public T? Get()
        {
            if (!isComputed)
            {
                value = function();
                isComputed = true;
            }

            return value;
        }
    }

    public class MultiThreadLazy<T> : ILazy<T>
    {
        private T value;
        private volatile bool isComputed;
        private Func<T> function;
        private readonly object locker;

        public MultiThreadLazy(Func<T> f)
        {
            function = f;
            isComputed = false;
            value = default(T);
            locker = new object();
        }

        public T? Get()
        {
            if (!isComputed)
            {
                lock (locker)
                {
                    value = function();
                    isComputed = true;
                    return value;
                }
            }

            return value;
        }
    }

    public class LazyFactory<T>
    {
        public static SingleThreadLazy<T> CreateSingleThreadLazy(Func<T> f) => new SingleThreadLazy<T>(f);

        public static MultiThreadLazy<T> CreateMultiThreadLazy(Func<T> f) => new MultiThreadLazy<T>(f);
    }
}