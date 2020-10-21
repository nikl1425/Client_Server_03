using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Server
{
    public class Response
    {
        public string status { get; set; }
        public string body { get; set;  }
        

        public Response(string Status, string Body)
        {
            status = Status;
            body = Body;
        }
        
         


       
    }
}