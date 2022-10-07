using System.Diagnostics;

namespace Task_2._1
{
    public class MainClass
    {
        private static double f() => Math.Exp(100);

        private static Int64 g() => (Int64)Int32.MaxValue * Int32.MaxValue;

        public static void Main()
        {
            var stopwatch = new Stopwatch();

            var lazy1 = LazyFactory<double>.CreateSingleThreadLazy(f);
            Console.WriteLine("Первая сессия");
            stopwatch.Start();
            var e1 = lazy1.Get();
            stopwatch.Stop();
            Console.WriteLine(e1);
            Console.WriteLine(stopwatch.Elapsed);

            Console.WriteLine("Вторая сессия");
            stopwatch.Restart();
            var e2 = lazy1.Get();
            stopwatch.Stop();
            Console.WriteLine(e2);
            Console.WriteLine(stopwatch.Elapsed);

            Console.WriteLine("Третья сессия");
            var lazy2 = LazyFactory<Int64>.CreateSingleThreadLazy(g);
            stopwatch.Restart();
            var i1 = lazy2.Get();
            stopwatch.Stop();
            Console.WriteLine(i1);
            Console.WriteLine(stopwatch.Elapsed);

            Console.WriteLine("Четвёртая сессия");
            stopwatch.Restart();
            var i2 = lazy2.Get();
            stopwatch.Stop();
            Console.WriteLine(i2);
            Console.WriteLine(stopwatch.Elapsed);
        }
    }
}