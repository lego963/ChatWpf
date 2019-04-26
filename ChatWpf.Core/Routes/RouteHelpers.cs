namespace ChatWpf.Core.Routes
{
    public static class RouteHelpers
    {
        public static string GetAbsoluteRoute(string relativeUrl)
        {
            var host = @"http://localhost:5000";
            //FrameworkDI.Configuration["SynthesisServer:HostUrl"];

            if (string.IsNullOrEmpty(relativeUrl))
                return host;

            if (!relativeUrl.StartsWith("/"))
                relativeUrl = $"/{relativeUrl}";

            //return FrameworkDI.Configuration["SynthesisServer:HostUrl"] + relativeUrl;
            return host + relativeUrl;
        }
    }
}
