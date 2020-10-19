using System;
using Newtonsoft.Json;

namespace Server
{
    public class Response
    {
        public string Status;
        public string Body;
        public string Reason;

        public Response(string status, string reason, string body)
        {
            Status = status;
            Reason = reason;
            Body = body;
        }
        
         public string ToJson()
        {
            var jsonRequest = JsonConvert.SerializeObject(this);
            return jsonRequest;
        }


       
    }
}