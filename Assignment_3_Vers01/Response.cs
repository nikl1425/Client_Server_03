using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Server
{
    public class Response
    {
        public string Status;
        public string Reason;
        

        public Response(string status, string reason)
        {
            status = Status;
            reason = Reason;
        }
        
         public string ToJson()
        {
            var jsonRequest = JsonConvert.SerializeObject(this);
            return jsonRequest;
        }


       
    }
}