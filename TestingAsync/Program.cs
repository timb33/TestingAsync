﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestingAsync
{
    class Program
    {


        static async Task Main(string[] args)
        {
            Foo();


            //Console.WriteLine("both are now running, but same 1111");
            //await Test1Async("a"); //start thread/Task now. thread is blocked.
            //await Test2Async("b"); //start thread/Task now. thread is blocked.
            //Console.WriteLine("longer hand, but same 22222");

            //Console.WriteLine("longer hand, but same thing");
            //var tskA1 = Test1Async("a");//this starts the thread without blocking, and returns a handle to the thread
            //Console.WriteLine("21");
            //var tskB1 = Test2Async("b");//this starts the thread without blocking
            //Console.WriteLine("23");
            //await tskA1;//do this,
            //Console.WriteLine("25");
            //await tskB1;//then this
            //Console.WriteLine("27");

            //Console.WriteLine("Now run both at same time");
            //var tskA = Test1Async("a"); //setup and start a thread/Task. return handle. run async.
            //var tskB = Test2Async("b"); //setup and start a thread/Task

            //await Task.WhenAll(tskA, tskB);//This is synchonisation/join
            ////var tskC = Task.WhenAll(tskA, tskB);//this is a third task, which completes when the other two complete. A task completes when it returns, not when it's finished doing its job 
            //Console.WriteLine("wait for everything to finish");

            Console.Read();
        }

        private static void Foo()
        {
            MakeCoffeeAsync("a").Wait(); //this is now sync
            Running("a");
            MakeCoffeeAsync("b"); //this is async but is awaitable so warning.
            Running("b");
            var t = MakeCoffeeAsync("c"); //start thread/Task now. don't block this thread
            Running("c");
            _ = FryEggsAsync("d"); //start thread/Task now. don't block this thread
            Running("d");
        }

        private static void Running(string v)
        {
            Console.WriteLine(v);
        }

        const int iDelay = 1000;

        #region

        private class Coffee
        {
        }

        private class Egg
        {
        }

        private class Bacon
        {
        }

        private class Toast
        {
        }

        private class Juice
        {
        }
        static async Task Main3(string[] args)
        {
            Coffee cup = PourCoffee();
            Console.WriteLine("coffee is ready");
            Egg eggs = FryEggs(2);
            Console.WriteLine("eggs are ready");
            Bacon bacon = FryBacon(3);
            Console.WriteLine("bacon is ready");
            Toast toast = ToastBread(2);
            ApplyButter(toast);
            ApplyJam(toast);
            Console.WriteLine("toast is ready");
            Juice oj = PourOJ();
            Console.WriteLine("oj is ready");

            Console.WriteLine("Breakfast is ready!");
            Console.Read();
        }

        private static Juice PourOJ()
        {
            Thread.Sleep(iDelay);
            return new Juice();
        }

        private static void ApplyJam(Toast toast)
        {
            //
            Thread.Sleep(iDelay);
        }

        private static void ApplyButter(Toast toast)
        {
            Console.WriteLine("ApplyButter");
            Thread.Sleep(iDelay);
        }

        private static Toast ToastBread(int v)
        {
            Console.WriteLine("ToastBread");
            Thread.Sleep(iDelay);
            return new Toast();
        }

        private static Bacon FryBacon(int v)
        {
            Console.WriteLine("FryBacon");
            Thread.Sleep(iDelay);
            return new Bacon();
        }

        private static Egg FryEggs(int v)
        {
            Console.WriteLine("FryEggs");
            Thread.Sleep(iDelay);
            return new Egg();
        }

        private static Coffee PourCoffee()
        {
            Console.WriteLine("PourCoffee");
            Thread.Sleep(iDelay);
            return new Coffee();
        }


        static void Main2(string[] args)
        {
            TestLoopsAsync();



            Console.Read();
        }

        private static async void TestLoopsAsync()
        {
            var tskA = MakeCoffeeAsync("a"); //setup but don't start a thread/Task
            var tskB = FryEggsAsync("b"); //setup but don't start a thread/Task

            await Task.WhenAll(tskA, tskB);

            Console.WriteLine("A & B both complete - both ran at same time-ish");
            Console.WriteLine("");

            _ = MakeCoffeeAsync("c"); //an underscore is a "discard". see: https://docs.microsoft.com/en-us/dotnet/csharp/discards
            _ = Task.Delay(10);
            await FryEggsAsync("d");

            Console.WriteLine("D done, C still running");
            Console.WriteLine("");

            await MakeCoffeeAsync("e");
            _ = Task.Delay(10);
            _ = FryEggsAsync("f");

            Console.WriteLine("main done 3");
            Console.WriteLine("");
            Console.Read();
        }

        private static async Task MakeCoffeeAsync(string id)
        {
            Console.WriteLine($"MakeCoffee[{id}] - started. tid:{Thread.CurrentThread.ManagedThreadId}");
            //_ = Task.Delay(10); //pause calling thread

            await Task.Run(() => //run this thread/Task now
            {
                for (int i = 0; i < 3; i++)
                {
                    Console.WriteLine($"MakeCoffee[{id}] - {i}. tid:{Thread.CurrentThread.ManagedThreadId}"); ;
                    Thread.Sleep(iDelay);
                }
            }  ) ;


            Console.WriteLine($"this is the continuation! MakeCoffee [{id}] - done. tid:{Thread.CurrentThread.ManagedThreadId}");
        }

        #endregion


        private static async Task FryEggsAsync(string id)
        {
            Console.WriteLine($"FryEggsAsync[{id}] - started. tid:{Thread.CurrentThread.ManagedThreadId}");
            //_ = Task.Delay(10); //pause calling thread
            await Task.Run(() => //spawn new thread
            {
                for (int i = 22; i < 25; i++)
                {
                    Console.WriteLine($"FryEggsAsync[{id}] - {i}. tid:{Thread.CurrentThread.ManagedThreadId}");
                    Thread.Sleep(iDelay);
                }
            });

            Console.WriteLine($"this is the continuation! FryEggsAsync[{id}] - done. tid:{Thread.CurrentThread.ManagedThreadId}"); ;
        }


    }
}
