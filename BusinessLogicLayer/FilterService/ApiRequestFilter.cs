using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc.Filters;
namespace BusinessLogicLayer.FilterService
{
    public class ApiRequestFilter: ActionFilterAttribute
    {
        private readonly IApplicationDbContext _dbContext;
        public ApiRequestFilter(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var deskriptor = context.ActionDescriptor as Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor;

            DataAccessLayer.Entities.ApiRequestsLogs apiRequestsLog = new DataAccessLayer.Entities.ApiRequestsLogs()
            {
                EndpointName = deskriptor.ActionName.ToString(),
                ResourceName = deskriptor.ControllerName.ToString(),
                RequestTime = DateTime.Now
            };
            _dbContext.ApiRequestsLogs.Add(apiRequestsLog);
           


            _dbContext.SaveChanges();
            base.OnActionExecuting(context);
        }
    }
}
