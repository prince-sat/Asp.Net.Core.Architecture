using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Asp.Net.Core.WebApi.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class CustomSwaggerOperationAttribute : SwaggerOperationAttribute
    {
        public CustomSwaggerOperationAttribute(Type controllerType, [CallerMemberName] string actionName = null)
            : base()
        {
            if (controllerType != null)
            {
                string controllerName = controllerType.Name.Replace("Controller", "");
                this.OperationId = $"{controllerName}_{actionName}";
                SwaggerUITagAttribute swaggerUITagAttribute = controllerType.GetTypeInfo().GetCustomAttribute<SwaggerUITagAttribute>();
                if (swaggerUITagAttribute != null && !string.IsNullOrWhiteSpace(swaggerUITagAttribute.Tag))
                {
                    this.Tags = new string[] { swaggerUITagAttribute.Tag };
                }
            }
        }
    }
}
