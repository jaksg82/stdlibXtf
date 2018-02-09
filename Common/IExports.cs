using System;
using System.Xml.Linq;

namespace stdlibXtf.Common
{
    /// <summary>
    /// Interface that provide export methods.
    /// </summary>
    public interface IExports
    {
        #region Functions

        /// <summary>
        /// A method to serialize the objects in to xml language.
        /// </summary>
        /// <returns>An XElement representation of the object.</returns>
        XElement ToXElement();

        #endregion Functions
    }
}