<%@ Application Language="C#" %>
<%@ Import Namespace="BlogEngine.NET.App_Start" %>

<script RunAt="server">
    void Application_BeginRequest(object sender, EventArgs e)
    {
        var app = (HttpApplication)sender;
        BlogEngineConfig.Initialize(app.Context);
    }
        
    void Application_PreRequestHandlerExecute(object sender, EventArgs e)
    {
        BlogEngineConfig.SetCulture(sender, e);
    }

    void Application_Error(object sender, EventArgs e)
    {
        var ex = Server.GetLastError();
        BlogEngine.Core.Log.Error(ex);
    }
    
    void Application_Start(object sender, EventArgs e)
    {
        BlogEngine.Core.Log.LogConfig(Server.MapPath(@"~\App_Data\log4net.config"));
    }
</script>