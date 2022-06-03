using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace TCPExercise2ServerMoreAdvanced
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("More advanced TCP communication Server!");

            // REFACTORING 

            //First you will change the server to be able to keep listening for clients.
            //Then you will change the server to handle clients at the same time.
            //Then you will change the server to understand a longer conversation
            //Finally you will change the client for a longer conversation as well
            //Bonus adding a protocol with more features to your server

            //The current version of your TCP server stops executing code when
            //the first client finishes the communication.
            //This is not normally not desirable for a server.
            //A server should keep listening for clients.

            //Create a method (that doesn’t return anything) called HandleClient.
            //This method must take 1 parameter, your socket variable (TcpClient)

            //Remember to call the HandleClient method with the socket after
            //the AcceptTcpClient call.

            //initialize an object of the TcpListener class
            TcpListener listener = new TcpListener(IPAddress.Any, 7);
            listener.Start();

            while (true)
            {
                TcpClient socket = listener.AcceptTcpClient();
                //Because TCP holds the connection open,
                //in order to handle several clients at the same time
                //it starts a new thread for each client
                //instead of having all the code here, it
                //is moved (refactored) to a seperate method HandleClient
                Task.Run(() => HandleClient(socket));
                //The server can now keep accepting clients,
                //until you either forcefully close it, or an exception is thrown.
            }

            //this line will never be reached because of the while loop
            //instead it will only stop when the program is stopped
            listener.Stop();
        }

            //Create a method (that doesn’t return anything) called HandleClient.
            //This method must take 1 parameter, your socket variable (TcpClient)
            //And your HandleClient method should look something like:
            public static void HandleClient(TcpClient socket)
            {
            //streams - read and write to the connection
            //These streams are accessed by first getting a stream
            //containing both:
                NetworkStream networkStream = socket.GetStream();
                //then splitting it into two streams
                StreamReader reader = new StreamReader(networkStream);
                StreamWriter writer = new StreamWriter(networkStream);
                //read what the client sends by calling the method
                //ReadLine on the reader object:
                string message = reader.ReadLine();
                Console.WriteLine("Client sent: " + message);
                //Herfra bliver det ECHO:
                //Then write the line back to the client, use the writer object:
                writer.WriteLine(message);
                //skyl ud!
                writer.Flush();
               
            }
        }
    }
