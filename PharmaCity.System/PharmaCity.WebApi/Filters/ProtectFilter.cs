using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using PharmaCity.Domain;
using PharmaCity.IBusinessLogic;
using System;

namespace PharmaCity.WebApi.Filters
{
    public class ProtectFilter : Attribute, IAuthorizationFilter
    {
        private ISessionService service;
        private readonly RoleType _role;

        public ProtectFilter(RoleType role)
        {
            this._role = role;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            service = context.HttpContext.RequestServices.GetService<ISessionService>();
            string token = context.HttpContext.Request.Headers["token"];

            Exception exception = new InvalidOperationException("No tienes permisos para esta acción");

            if (token == null)
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = exception.Message
                };
            }
            else
            {
                if (!service.IsAllowed(_role, token))
                {
                    context.Result = new ContentResult()
                    {
                        StatusCode = 403,
                        Content = exception.Message
                    };
                }
            }
        }
    }
}
