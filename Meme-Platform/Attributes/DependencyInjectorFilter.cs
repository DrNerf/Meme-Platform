using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using System.Reflection;

namespace Meme_Platform.Attributes
{
    public class DependencyInjectorFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            ResolveAnnotatedDependencies(context.HttpContext.RequestServices, context.Controller);
        }

        private void ResolveAnnotatedDependencies(IServiceProvider serviceProvider, object target)
        {

            var fields = target.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(f => f.IsDefined(typeof(InjectAttribute), false));
            foreach (var field in fields)
            {
                field.SetValue(target, serviceProvider.GetService(field.FieldType));
            }
        }
    }
}
