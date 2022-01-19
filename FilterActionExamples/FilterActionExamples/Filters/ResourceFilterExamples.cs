using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace FilterActionExamples.Filters
{

    /*
     * Resource filters:
        Implement either the IResourceFilter or IAsyncResourceFilter interface.
        Execution wraps most of the filter pipeline.
        Only Authorization filters run before resource filters.


        Resource filters are useful to short-circuit most of the pipeline. 
        For example, a caching filter can avoid the rest of the pipeline on a cache hit.
     */

    public class ResourceFilterExamples : Attribute, IResourceFilter
    {
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            Debug.WriteLine("Should come at last - Resouce Executed.");
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            Debug.WriteLine("Should come at First - Resource is Executing.");
            Debug.WriteLine($"called came for controller: {context.RouteData.Values["controller"]} ");
        }
    }
}
