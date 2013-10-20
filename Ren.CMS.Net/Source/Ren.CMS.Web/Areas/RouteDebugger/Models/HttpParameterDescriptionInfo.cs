namespace Ren.CMS.Areas.RouteDebugger.Models
{
    using System;
    using System.Web.Http.Controllers;

    /// <summary>
    /// Represents the parameters.
    /// </summary>
    public class HttpParameterDescriptorInfo
    {
        #region Constructors

        public HttpParameterDescriptorInfo(HttpParameterDescriptor descriptor)
        {
            ParameterName = descriptor.ParameterName;
            ParameterType = descriptor.ParameterType;
            ParameterTypeName = descriptor.ParameterType.Name;
        }

        #endregion Constructors

        #region Properties

        public string ParameterName
        {
            get; set;
        }

        public Type ParameterType
        {
            get; set;
        }

        public string ParameterTypeName
        {
            get; set;
        }

        #endregion Properties
    }
}