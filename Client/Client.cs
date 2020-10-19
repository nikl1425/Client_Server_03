using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using ClassLibrary1;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Client
{
    class ClientProgram
    {
        private StreamWriter _streamWriter;
        private StreamReader _streamReader;
        public string Status;
       
        static void Main(string[] args)
        {
            string path = @"C:\Users\45535\Desktop\RAWDATA\C#\Projects\Assignment_3_Vers01\ClassLibrary1\JsonFiles\Test.json";

           // Define and start TCP client
           using var tcpClient = new TcpClient();
            
            // Client connect to local via. port 5000.
            tcpClient.Connect(IPAddress.Loopback, 5000);

           
            // "sWriter" = how we write to server. 
            var sWriter = new BinaryWriter(tcpClient.GetStream(), Encoding.UTF8);
            
            // "sReader" = how we read responses
            var sReader = new StreamReader(tcpClient.GetStream(), Encoding.UTF8);
            
            //Define our request
            Request request = new Request("update", "/bla", 1, "Ole");
            
            //Send request to server - see Util.cs

            Util.SendRequest(tcpClient, request.ToJson());
            
            //Console.WriteLine($"Message from the server: {}");
            
           Console.Write(request.ToJson());
           

        }
        
        public string IntepretStatus()
        {
            const string ok = "ok";
            const string created = "created";
            const string updated = "Updated";
            const string badRequest = "badRequest";
            const string notFound = "notFound";
            const string error = "error";

            if (Status == "1")
            {
                Console.WriteLine(ok);
            }
            else if (Status == "2")
            {
                Console.WriteLine(created);
            }
            else if (Status == "3")
            {
                Console.WriteLine(updated);
            }
            else if (Status == "4")
            {
                Console.WriteLine(badRequest);
            }
            else if (Status == "5")
            {
                Console.WriteLine(notFound);
            }
            else if (Status == "6")
            {
                Console.WriteLine(error);
            }

            return Status;
        }
        
        
    }
}