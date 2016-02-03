using Gecko;

namespace SharpUtility.GeckoFX
{
    public static class GeckoWebBrowserExtentions
    {
        public static string EvaluateScript(this GeckoWebBrowser browser, string jsString)
        {
            using (var autoJSContext = new AutoJSContext(browser.Window.JSContext))
            {
                var jsVal = autoJSContext.EvaluateScript(jsString, browser.Window.DomWindow);
                return jsVal.ToString();
            }
        }
    }
}
