using System;
using System.IO;
using System.IO.Pipes;

class PipeClient
{
    static void Main()
    {
        Console.WriteLine("Connecting to Pipe Server...");

        using (NamedPipeClientStream pipeClient = new NamedPipeClientStream(".", "mypipe", PipeDirection.In))
        {
            pipeClient.Connect();
            Console.WriteLine("Connected to server!");

            using (StreamReader reader = new StreamReader(pipeClient))
            {
                string message = reader.ReadLine();
                Console.WriteLine("Received from server: " + message);
            }
        }

        Console.WriteLine("Pipe Client has closed.");
    }
}
