using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Data.SqlClient;
using PharmaCity.Domain.DTO;
using System;
using System.Collections.Generic;

namespace PharmaCity.WebApi.Filters
{
    public class ExceptionFilter : Attribute, IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
			List<Type> errors422 = new List<Type>()
			{
				typeof(ArgumentNullException)
			};
            List<Type> errors404 = new List<Type>()
            {
                typeof(NullReferenceException)
            };
            List<Type> errors400 = new List<Type>()
            {
                typeof(InvalidOperationException)
            };
			ErrorDTO response = new ErrorDTO()
			{
				IsSuccess = false,
				ErrorMessage = context.Exception.Message
			};

			Type errorType = context.Exception.GetType();

            if (context.Exception is SqlException)
            {
                response.Code = 500;
                response.Content = "Error al conectar con la base de datos, intente mas tarde...";
            }
            else if(errors422.Contains(errorType))
			{
				response.Content = context.Exception.Message;
				response.Code = 422;
            }
			else if (errors404.Contains(errorType))
            {
                response.Content = context.Exception.Message;
                response.Code = 404;
			}
			else if (errors400.Contains(errorType))
            {
                response.Content = context.Exception.Message;
                response.Code = 400;
			}
            else
            {
				response.Content = context.Exception.Message;
				response.Code = 500;
				Console.WriteLine(context.Exception);
            }

			context.Result = new ObjectResult(response)
			{
				StatusCode = response.Code
			};
		}
    }
}
