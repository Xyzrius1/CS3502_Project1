using System;
using System.Threading;

class ThreadingExample
{
    static Mutex mutex = new Mutex();
    static object lock1 = new object();
    static object lock2 = new object();

    static void Worker(object id)
    {
        Console.WriteLine($"Thread {id} is starting...");
        Thread.Sleep(500);
        Console.WriteLine($"Thread {id} is done.");
    }

    static void SafeWorker(object id)
    {
        mutex.WaitOne();
        Console.WriteLine($"Thread {id} is entering critical section.");
        Thread.Sleep(1000);
        Console.WriteLine($"Thread {id} is leaving critical section.");
        mutex.ReleaseMutex();
    }

    static void DeadlockThread1()
    {
        lock (lock1)
        {
            Thread.Sleep(100);
            lock (lock2)
            {
                Console.WriteLine("Thread 1 is executing...");
            }
        }
    }

    static void DeadlockThread2()
    {
        lock (lock2)
        {
            Thread.Sleep(100);
            lock (lock1)
            {
                Console.WriteLine("Thread 2 is executing...");
            }
        }
    }

    static void Main()
    {
        Console.WriteLine("Starting Multi-Threading Demonstration");

        // Basic Multi-Threading
        const int numThreads = 10;
        Thread[] threads = new Thread[numThreads];
        for (int i = 0; i < numThreads; i++)
        {
            threads[i] = new Thread(Worker);
            threads[i].Start(i);
        }
        foreach (var t in threads) t.Join();

        // Synchronization with Mutex
        Console.WriteLine("\nStarting Synchronization Demonstration");
        Thread[] syncThreads = new Thread[numThreads];
        for (int i = 0; i < numThreads; i++)
        {
            syncThreads[i] = new Thread(SafeWorker);
            syncThreads[i].Start(i);
        }
        foreach (var t in syncThreads) t.Join();

        // Deadlock Demonstration
        Console.WriteLine("\nStarting Deadlock Demonstration (Will Freeze)");
        Thread t1 = new Thread(DeadlockThread1);
        Thread t2 = new Thread(DeadlockThread2);
        t1.Start();
        t2.Start();
        t1.Join();
        t2.Join();
    }
}
