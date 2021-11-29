using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Util.Exceptions;
using Util.Models;

namespace api.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IWebHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next,IWebHostEnvironment env)
        {
            _next = next;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(Exception e)
            {
                await HandleExceptionAsync(context,e,_env); 
            }
            // Call the next delegate/middleware in the pipeline
           
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception e, IWebHostEnvironment env)
        {
            var code = HttpStatusCode.InternalServerError;
            var error = new ApiErrorResponse()
            {
                StatusCode = (int)code
            };
            if(env.IsDevelopment())
            {
                error.Details = e.StackTrace;
            }
            else
            {
                error.Details = e.Message;
            }
            switch(e)
            {
                case ApplicationValidationException exception:
                    error.Message = exception.Message;
                    error.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
                    break;
                default:
                    error.Message = "Something went wrong";
                    break;

            }

            var result = JsonConvert.SerializeObject(error);
            context.Response.ContentType = "Application/Json";
            context.Response.StatusCode = error.StatusCode;
            await context.Response.WriteAsync(result);
        }
    }
}
