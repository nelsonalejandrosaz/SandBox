using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using SandBox.Data;

namespace SandBox.Attributes;

[AttributeUsage(validOn: AttributeTargets.Class)]
public class PersonalPolicyAttribute : Attribute, IAsyncActionFilter
{
    private readonly string _policy;

    public PersonalPolicyAttribute(string policy)
    {
        _policy = policy;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var dbcontext = context.HttpContext.RequestServices.GetService(typeof(ApplicationDbContext)) as ApplicationDbContext;
        var userRole = context.HttpContext.User.FindFirstValue(ClaimTypes.Role);
        var role = dbcontext?.Roles
            .Include(r => r.Policies)
            .FirstOrDefault(r => r.Name == userRole);
        var policy = dbcontext?.Policies.FirstOrDefault(p => p.Nombre == _policy);

        if (role is not null && policy is not null)
        {
            var result = role.Policies?.Contains(policy);
            if (result.HasValue && result.Value)
            {
                await next();
            } else
            {
                context.Result = new UnauthorizedResult();
            }

        }
        else
        {
            context.Result = new UnauthorizedResult();
        }

    }
}