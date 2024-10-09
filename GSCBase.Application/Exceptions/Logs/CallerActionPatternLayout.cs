using log4net.Layout;
using log4net.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GSCBase.Application.Exceptions.Logs
{
    public sealed class CallerActionPatternLayout : PatternLayout
    {
        public CallerActionPatternLayout()
        {
            AddConverter(new ConverterInfo { Name = "callerinfo", Type = typeof(CallerActionPatternConverter) });
        }
    }
}
