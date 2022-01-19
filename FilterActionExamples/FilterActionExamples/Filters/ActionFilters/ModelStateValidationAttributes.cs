using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace FilterActionExamples.Filters.ActionFilters
{
    public class ModelStateValidationAttributes : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {


            foreach (var item in context.ModelState.Keys) 
            {
                if(item == "DateOfBirth")
                {
                    if(Convert.ToDateTime(context.ModelState[item].RawValue).ToShortDateString() == DateTime.MinValue.ToShortDateString())
                    {
                        context.ModelState.AddModelError("DateOfBirth", "Provide Valid Date of Birth");
                    }
                }

                if(!context.ModelState.IsValid)
                {
                    context.Result = new ViewResult()
                    {
                        ViewName = "Create"
                    };
                }
                //Debug.Write(item + " : ");
                //Debug.WriteLine(context.ModelState[item].RawValue);

            }
        }
    }
}
