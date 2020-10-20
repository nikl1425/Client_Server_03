using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Server
{
    public class Response
    {
        public int Status;
        public string Reason;
        

        public Response(int status, string reason)
        {
            this.Status = status;
            this.Reason = reason;
        }
        
         public string ToJson()
        {
            var jsonRequest = JsonConvert.SerializeObject(this);
            return jsonRequest;
        }


       
    }
}