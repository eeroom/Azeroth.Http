﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
namespace KlzApiNext
{
    class App
    {
        static void Main(string[] args)
        {
            var hostBuilder =new WebHostBuilder();
            hostBuilder.UseKestrel()
                .UseStartup<Startup>();
            var host = hostBuilder.Build();
            host.Run();
            
        }
    }
}
