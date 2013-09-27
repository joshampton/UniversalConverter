using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalConverter
{
    public static class ObjectExtensions
    {
        public static bool IsNull(this object target)
        {
            return target == null || target == DBNull.Value;
        }
    }
}
