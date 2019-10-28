using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp7
{
    class Program
    {
        static void Main(string[] args)
        {
            var chopstics = new Dictionary<int, object>(5);
            for (int i = 0; i < 5; i++)
            {
                chopstics[i] = new object();
            }
            Task[] tasks = new Task[5];
            tasks[0] = new Task(() => Philosopher.Eat(chopstics[4], chopstics[0], 0, 4, 0));
            tasks[1] = new Task(() => Philosopher.Eat(chopstics[0], chopstics[1], 1, 0, 1));
            tasks[2] = new Task(() => Philosopher.Eat(chopstics[1], chopstics[2], 2, 1, 2));
            tasks[3] = new Task(() => Philosopher.Eat(chopstics[2], chopstics[3], 3, 2, 3));
            tasks[4] = new Task(() => Philosopher.Eat(chopstics[3], chopstics[4], 4, 3, 4));
            Parallel.ForEach(tasks, t => t.Start());
            Task.WaitAll(tasks);
            Console.ReadKey();
        }
    }
    
    static class Philosopher
    {
        public static void Eat(object firstChopstic, object secondChopstic, int numPhil, int chopF, int chopS)
        {
            lock (firstChopstic)
            {
                Console.WriteLine($"Философ {numPhil} взял {chopF} вилку");
                lock (secondChopstic)
                {
                    Console.WriteLine($"Философ {numPhil} взял {chopS} вилку");
                    Console.WriteLine($"Философ {numPhil} начал есть");
                }
                Console.WriteLine($"Философ {numPhil} положил {chopS} вилку");
            }
            Console.WriteLine($"Философ {numPhil} положил {chopF} вилку");
        }
    }
}