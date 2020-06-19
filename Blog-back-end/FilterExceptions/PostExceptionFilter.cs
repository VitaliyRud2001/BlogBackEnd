using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog_back_end.FilterExceptions
{
    public class PostExpceptionFilter : Attribute, IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var exceptionType = context.Exception;
            string message = "Server error";
            if (exceptionType is KeyNotFoundException)
            {
                message = exceptionType.Message;
                context.HttpContext.Response.StatusCode = 404;
            }
            context.Result = new ObjectResult(new { message = message });
        }
    }
}
