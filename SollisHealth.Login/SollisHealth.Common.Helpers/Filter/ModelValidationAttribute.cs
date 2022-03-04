using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using System.Collections.Generic;
using System.Linq;

namespace SollisHealth.Common.Helpers.Filter
{
    public class ModelValidationAttribute: ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                List<string> list = (from modelState in context.ModelState.Values from error in modelState.Errors select error.ErrorMessage).ToList();
                context.Result = new BadRequestObjectResult(list);
            }

           // base.OnActionExecuting(context);

            //if (!context.ModelState.IsValid)
            //{
            //    //APIResponse exResponse = new APIResponse();
            //    //exResponse.message = "Please check validation faild.";
            //    //context.Result = new BadRequestObjectResult(context.ModelState); // returns 400 with error
            //}
        }
    }
}
