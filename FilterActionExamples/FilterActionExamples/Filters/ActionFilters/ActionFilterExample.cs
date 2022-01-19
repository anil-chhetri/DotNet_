using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace FilterActionExamples.Filters.ActionFilters
{
    public class ActionFilterExample : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            Debug.Flush();
            Debug.WriteLine("Action Executing Ended. ==> ");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            Debug.WriteLine("Action Executing started. ==> ");

        }
    }
}
