using System;
using System.Collections.Generic;
using System.Text;

namespace Incognitum.Helpers
{
    internal static class Validations
    {
        internal static void ParameterIsNotNullOrWhiteSpace(String parameterName, String parameterValue)
        {
            if (parameterValue == null)
            {
                throw new ArgumentNullException(parameterName);
            }
            if (String.IsNullOrWhiteSpace(parameterValue))
            {
                throw new ArgumentException("Argument cannot be whitespace", parameterName);
            }
        }
    }
}
