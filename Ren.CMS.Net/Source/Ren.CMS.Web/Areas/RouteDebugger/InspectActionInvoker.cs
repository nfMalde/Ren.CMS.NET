namespace Ren.CMS.Areas.RouteDebugger
{
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Http.Controllers;

    using Ren.CMS.Areas.RouteDebugger.Models;

    /// <summary>
    /// Hijacks the original invoker. It examines the header before 
    /// executing the action. If the inspect header exists, returns the inspection data in a 200 response.
    /// If the inspection header does not exist, the delegate calls the default InvokeActionAsync method.
    /// 
    /// The inspection data saved in the request property are collected when the request is passed
    /// along the stack.
    /// </summary>
    public class InspectActionInvoker : IHttpActionInvoker
    {
        #region Fields

        private IHttpActionInvoker _innerInvoker;

        #endregion Fields

        #region Constructors

        public InspectActionInvoker(IHttpActionInvoker innerInvoker)
        {
            _innerInvoker = innerInvoker;
        }

        #endregion Constructors

        #region Methods

        public Task<HttpResponseMessage> InvokeActionAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            if (actionContext.Request.IsInspectRequest())
            {
                var inspectData = new InspectData(actionContext.Request);
                inspectData.RealHttpStatus = HttpStatusCode.OK;
                return Task.FromResult<HttpResponseMessage>(actionContext.Request.CreateResponse<InspectData>(
                    HttpStatusCode.OK, inspectData));
            }
            else
            {
                return _innerInvoker.InvokeActionAsync(actionContext, cancellationToken);
            }
        }

        #endregion Methods
    }
}