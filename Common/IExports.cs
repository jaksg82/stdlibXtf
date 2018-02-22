using System;
using System.Collections.Generic;
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

        /// <summary>
        /// A method to get all the object info in a name/description list.
        /// </summary>
        /// <returns>A list of name / description objects. The ID value should be ignored.</returns>
        List<TypeEntry> GetList();

        #endregion Functions
    }
}