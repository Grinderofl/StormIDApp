using System.Web.Mvc;
using Microsoft.Web.Mvc;

namespace Web.App_Start
{
    public class ViewEngineConfig
    {
        public static void RegisterViewEngines()
        {
            // Remove Webforms viewengine for performance purposes
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new FixedRazorViewEngine());
        }
    }
}