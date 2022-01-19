using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilterActionExamples.Filters
{
    public class ResourceFilterAsync : IAsyncResourceFilter
    {
        public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
        {
            context.HttpContext.Response.Headers.Add("additional_Information", "completed.");

            //await
            //
            await HttpResponseWritingExtensions.WriteAsync(context.HttpContext.Response, "Nobody can access this resources");
                 

            //await context.HttpContext.Response.Body.WriteAsync();
            await Task.CompletedTask;

            //await next();
        }
    }
}
