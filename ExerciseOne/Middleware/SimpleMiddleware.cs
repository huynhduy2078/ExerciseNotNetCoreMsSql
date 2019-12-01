using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;


namespace ExerciseOne.Middleware
{
    public class SimpleMiddleware : ControllerBase
    {
        private readonly RequestDelegate _next;
        private readonly IOptions<ConfigIp> _ipAccept;

        public string IPAddress { get; private set; }

        public  SimpleMiddleware(RequestDelegate next , IOptions<ConfigIp> options)
        {
            _next = next;
            _ipAccept = options;
        }


        public  async Task Invoke(HttpContext context)
        {
            string IPAddress = GetIPAddress();
            string[] listIpAccept = new string[] { _ipAccept.Value.IpAccept };
            int pos = Array.IndexOf(listIpAccept, IPAddress);

            if (pos > -1)
            {
                context.Response.StatusCode = 401; //Bad Request                
                await context.Response.WriteAsync("ip not allowed to connect");
                return;
            }
            await _next(context);
        }


        public string GetIPAddress()
        {
            IPHostEntry Host = default(IPHostEntry);
            string Hostname = null;
            Hostname = System.Environment.MachineName;
            Host = Dns.GetHostEntry(Hostname);
            foreach (IPAddress IP in Host.AddressList)
            {
                if (IP.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    IPAddress = Convert.ToString(IP);
                }
            }
            return IPAddress;
        }


    }
}
