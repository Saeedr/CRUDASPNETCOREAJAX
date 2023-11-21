using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;

namespace JQueryAjaxCRUDInASPNETCore
{
    public class Helper
    {
        public static string RenderRazorViewToString(Controller controller, string viewName, object modal = null)
        {
            controller.ViewData.Model = modal;
            using (var sw = new StringWriter())
            {
                IViewEngine viewEngine = controller.HttpContext.RequestServices.GetService(typeof(ICompositeViewEngine)) as ICompositeViewEngine;
                ViewEngineResult viewEngineResult = viewEngine.FindView(controller.ControllerContext, viewName, false);
                ViewContext viewContext = new ViewContext(
                    controller.ControllerContext,
                    viewEngineResult.View,
                    controller.ViewData,
                    controller.TempData,
                    sw,
                    new Microsoft.AspNetCore.Mvc.ViewFeatures.HtmlHelperOptions()
                    );
                viewEngineResult.View.RenderAsync(viewContext);
                return sw.GetStringBuilder().ToString();

            }
        }

        [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
        public class NoDirectAccessAttribute : ActionFilterAttribute
        {
            public override void OnActionExecuting(ActionExecutingContext filterContext)
            {
                var canAcess = false;

                // check the refer
                var referer = filterContext.HttpContext.Request.Headers["Referer"].ToString();
                if (!string.IsNullOrEmpty(referer))
                {
                    var rUri = new System.UriBuilder(referer).Uri;
                    var req = filterContext.HttpContext.Request;
                    if (req.Host.Host == rUri.Host && req.Host.Port == rUri.Port && req.Scheme == rUri.Scheme)
                    {
                        canAcess = true;
                    }
                }

                // ... check other requirements

                if (!canAcess)
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Home", action = "Index", area = "" }));
                }
            }
        }
    }

}
