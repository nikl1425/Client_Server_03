using System;

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


       
    }
}