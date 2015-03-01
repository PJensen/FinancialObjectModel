using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinancialObjectModel
{
    /// <summary>
    /// A singlular attribute tied to a <see cref="Security"/>.
    /// </summary>
    public class SecurityAttribute
    {
        public string Name;
        public object Value;

        public SecurityAttribute(string name, object value)
        {
            Name = name;
            Value = value;
        }
    }
}
