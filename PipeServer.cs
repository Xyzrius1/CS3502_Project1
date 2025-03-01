using System;
using System.IO;
using System.IO.Pipes;
using System.Threading;

class PipeServer
{
    static void HandleClient(object obj)
    {
        using (NamedPipeServerStream pipeServer = new NamedPipeServerStream("mypipe"))
        {
            pipeServer.WaitForConnection();
            Console.WriteLine("Client connected!");

            using (StreamWriter writer = new StreamWriter(pipeServer))
            {
                writer.AutoFlush = true;
                writer.WriteLine("Hello from the server!");
                Console.WriteLine("Message sent to client.");
            }
        }
    }

    static void Main()
    {
        Console.WriteLine("Pipe Server is running...");

        for (int i = 0; i < 3; i++)  // Accept up to 3 clients
        {
            Thread clientThread = new Thread(HandleClient);
            clientThread.Start();
        }

        Console.ReadLine(); // Keep server running
    }
}
