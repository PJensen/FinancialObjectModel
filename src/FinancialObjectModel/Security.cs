﻿using System;
using System.Xml.Serialization;

namespace FinancialObjectModel
{
    /// <summary>
    ///   Security
    /// </summary>
    [Serializable]
    public abstract class Security
    {
        /// <summary>
        /// collection of security attributes
        /// </summary>
        private readonly SecurityAttributes _securityAttributes = new SecurityAttributes();

        /// <summary>
        /// Initializes a new instance of the <see cref="Security"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="ticker">The ticker.</param>
        protected Security(string name, string ticker)
        {
            Name = name;
            Ticker = ticker;
        }

        /// <summary>
        ///   Gets or sets the ticker.
        /// </summary>
        /// <value> The ticker. </value>
        [XmlAttribute]
        public string Ticker { get; protected set; }

        #region ISecurity Members


        /// <summary>
        ///   Gets or sets the name of the security.
        /// </summary>
        /// <value> The name. </value>
        [XmlAttribute]
        public string Name { get; protected set; }


        #endregion

        /// <summary>
        /// Attributes for this security.
        /// </summary>
        public SecurityAttributes Attributes { get { return _securityAttributes; } }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return string.Format("{0}", Name);
        }
    }
}

