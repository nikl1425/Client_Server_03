using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading;
using ClassLibrary1;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Server
{
    class ServerProgram
    {
        static void Main(string[] args)
        {
            // set server to TCP listener on local
            var server = new TcpListener(IPAddress.Loopback, 5000);
            server.Start();
            Console.WriteLine("Server started!");


            while (true)
            {
                // set client = new tcp client. 
                var connection = server.AcceptTcpClient();

                Console.WriteLine("Accepted client!");

                var stream = connection.GetStream();
                byte[] data = new byte[connection.ReceiveBufferSize];
                var cnt = stream.Read(data);

                var msg = Encoding.UTF8.GetString(data, 0, cnt);
                Console.WriteLine($"Message from client:  {msg}");

                // Desiralize the request into r and use methods. 

                RequestContainer r = Util.FromJson<RequestContainer>(msg);
                //RequestContainer r = Util.FromJson<RequestContainer>(msg);
                Console.WriteLine("the method of msg is: \n" + r.Method);

                // RESPOND TYPES:
                Response perfect = new Response("1 OK", "Well done.");
                Response created = new Response("2 Created: ", " ");
                Response updated = new Response("3 Updated: ", " ");
                Response badRequest = new Response("4 missing date, missing method, illegal method ", " ");
                Response missingRessourse = new Response("4 missing resource", " ");
                Response notFound = new Response("5 Not found: ", " ");
                Response error = new Response("6 error: ", " ");
                
                // Legal methods:
                HashSet<string> serverMethods = new HashSet<string>();
                serverMethods.Add("create");
                serverMethods.Add("read");
                serverMethods.Add("update");
                serverMethods.Add("delete");
                serverMethods.Add("echo");



                if (r.Method == null && r.Date == null)
                {
                    var missedmethodJson = Util.ToJson(badRequest);
                    Console.WriteLine(missedmethodJson);
                    byte[] response = Encoding.UTF8.GetBytes(missedmethodJson.ToUpper());
                    stream.Write(response);
                    connection.Close();
                    stream.Flush();
                    continue;
                }
                else if (!serverMethods.Contains(r.Method))
                {
                    var illegalMethodJson = Util.ToJson(badRequest);
                    Console.WriteLine("Illegal method: " + illegalMethodJson);
                    byte[] response = Encoding.UTF8.GetBytes(illegalMethodJson.ToUpper());
                    stream.Write(response);
                    connection.Close();
                    stream.Flush();
                    continue;
                }
                else if (serverMethods.Contains(r.Method) && r.Date != null && r.Path == null)
                {
                    var missingResources = Util.ToJson(missingRessourse);
                    Console.WriteLine("Ressource doesn't exists: " + missingResources);
                    byte[] response = Encoding.UTF8.GetBytes(missingResources);
                    stream.Write(response);
                    connection.Close();
                    stream.Flush();
                } else if ()
                {
                    
                }
                else
                {
                    var perfectJson = Util.ToJson(perfect);
                    Console.WriteLine(perfectJson);
                    byte[] response = Encoding.UTF8.GetBytes(perfectJson.ToUpper());
                    stream.Write(response);
                    connection.Close();
                    stream.Flush();
                    //stream.Write(Encoding.UTF8.GetBytes(methodExist));
                    Console.WriteLine("method is: " + r.Method);
                }
                connection.Close();

            }
        }
    }
}