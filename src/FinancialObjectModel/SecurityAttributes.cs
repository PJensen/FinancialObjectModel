using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinancialObjectModel
{
    /// <summary>
    /// SecurityAttributes
    /// </summary>
    public class SecurityAttributes : HashSet<SecurityAttribute>
    {
        /// <summary>
        /// Attributes indexer
        /// <remarks>allows for dynamically getting and setting without having 
        /// to initialize each attribute first. That is to say; the getter also
        /// functions as a setter</remarks>
        /// <example>
        /// <code>
        /// // this works
        /// security.Attributes["Attr_1"] = 100;
        /// 
        /// // as opposed to
        /// security.Attributes.Add("Attr_1", 100);
        /// </code>
        /// </example>
        /// </summary>
        /// <param name="name">the name of the attribute</param>
        /// <returns></returns>
        public object this[string name]
        {
            get
            {
                return (this.SingleOrDefault(s => s.Name == name)
                    ?? new SecurityAttribute(name, null)).Value;
            }
            set
            {
                Add(this.SingleOrDefault(s => s.Name == name)
                    ?? new SecurityAttribute(name, value));
            }
        }
    }
}
