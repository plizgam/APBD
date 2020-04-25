using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kolokwium1.Middlewares
{
    public class IndexMiddleware
    {
        private readonly RequestDelegate _next;

        public IndexMiddleware(RequestDelegate next)
        {
            _next = next;
        }


        public async Task InvokeAsync(HttpContext context)
        {
            context.Response.Headers.Add("StudentIndex", "s18536");
            await _next(context);
        }

    }
}
