using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Todo.Utils
{
    public static class EnumUtils
    {
        public static int GetDisplayOrder(this Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var displayAttribute = field.CustomAttributes.FirstOrDefault(attr => attr.AttributeType == typeof(DisplayAttribute));
            var orderArgument = displayAttribute?.NamedArguments.FirstOrDefault(arg => arg.MemberName == "Order");

            if (orderArgument == null)
            {
                throw new ArgumentException($"The provided enum value {value.ToString()} doesn't have a DisplayAttribute or is missing it's Order value. Add the attribute with Order value.");
            }
            else
            {
                return (int)orderArgument.Value.TypedValue.Value;
            }
        }
    }
}
