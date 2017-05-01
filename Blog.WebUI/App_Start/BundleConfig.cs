using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace Blog.WebUI.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
           // bundles.Add(new ScriptBundle("~/bundles/scripts")
           //.IncludeDirectory("~/Assets/js", "*.js",true));

           // bundles.Add(new ScriptBundle("~/bundles/scripts")
           //.IncludeDirectory("~/Scripts", "*.js", true));

            // bundles.Add(new StyleBundle("~/bundles/css")
            //.IncludeDirectory("~/Assets/css", "*.css", true));


            //BundleTable.EnableOptimizations = true;

            //bundles.Add(new ScriptBundle("~/Scripts/knockout").Include(
            //     "~/Scripts/Lib/knockout/knockout-{version}.js",
            //     "~/Scripts/Lib/knockout/knockout-deferred-updates.js")
            //);
        }
    }
}