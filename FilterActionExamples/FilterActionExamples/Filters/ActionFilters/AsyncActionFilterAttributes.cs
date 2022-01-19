using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace FilterActionExamples.Filters.ActionFilters
{
    public class AsyncActionFilterAttributes : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            Debug.WriteLine("this line is executed.");
            await next();
            Debug.WriteLine("Is this line Executed?");
        }
    }
}
