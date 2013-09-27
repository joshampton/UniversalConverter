using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalConverter
{
    public interface IConverter
    {
        bool TryConvert(ConverterContext context, out object result);
    }
}
