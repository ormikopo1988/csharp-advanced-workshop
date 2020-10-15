using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelVersusNormalExecution
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = Directory.GetCurrentDirectory();

            var files = Directory.GetFiles(path + @"\pictures", "*.jpg");

            var alteredPathParallel = path + @"\alteredPathParallel";
            var alteredPathNormal = path + @"\alteredPathNormal";

            Directory.CreateDirectory(alteredPathParallel);
            Directory.CreateDirectory(alteredPathNormal);

            ParallelExecution(files, alteredPathParallel);

            NormalExecution(files, alteredPathNormal);

            Console.ReadLine();
        }

        private static void NormalExecution(string[] files, string alteredPath)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            foreach (var currentFile in files) 
            {
                var file = Path.GetFileName(currentFile);

                using (var fileBitmap = new Bitmap(currentFile))
                {
                    fileBitmap.RotateFlip(RotateFlipType.Rotate90FlipX);
                    fileBitmap.Save(Path.Combine(alteredPath, file));

                    Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId}");
                }
            };

            stopwatch.Stop();
            Console.WriteLine("Time passed in normal execution: " + stopwatch.ElapsedMilliseconds);
        }

        private static void ParallelExecution(string[] files, string alteredPath)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            Parallel.ForEach(files, currentFile =>
            {
                var file = Path.GetFileName(currentFile);

                using (var fileBitmap = new Bitmap(currentFile))
                {
                    fileBitmap.RotateFlip(RotateFlipType.Rotate90FlipX);
                    fileBitmap.Save(Path.Combine(alteredPath, file));

                    Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId}");
                }
            });

            stopwatch.Stop();
            Console.WriteLine("Time passed in parallel execution: " + stopwatch.ElapsedMilliseconds);
        }
    }
}
