using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Validators
{
    public static class EntityValidators
    {
        public static bool IsObjectNull(this IEntity entity)
        {
            return entity == null;
        }
    }
}
