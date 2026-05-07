using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;

namespace ItAcademy.Samples.WebAPI.Infrastructure;

public sealed class FluentValidatorActionFilter(IOptions<ApiBehaviorOptions> apiBehaviourOptions) 
    : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        foreach(var (argumentName, argumentValue) in context.ActionArguments)
        {
            if (argumentValue is null)
            {
                continue;
            }

            var validatorType = typeof(IValidator<>).MakeGenericType(argumentValue.GetType());
            if (context.HttpContext.RequestServices.GetService(validatorType) is not IValidator validator)
            {
                continue;
            }

            var validationContext = new ValidationContext<object>(argumentValue);
            var validationResult = await validator.ValidateAsync(validationContext, 
                context.HttpContext.RequestAborted);

            foreach (var error in validationResult.Errors)
            {
                var key = string.IsNullOrEmpty(error.PropertyName) 
                    ? argumentName 
                    : $"{argumentName}.{error.PropertyName}";
                context.ModelState.TryAddModelError(key, error.ErrorMessage);
            }
        }

        if (!context.ModelState.IsValid)
        {
            context.Result = apiBehaviourOptions.Value.InvalidModelStateResponseFactory(context);
            return;
        }

        await next();
    }
}