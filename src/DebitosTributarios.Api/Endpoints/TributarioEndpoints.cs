namespace DebitosTributarios.Api.Endpoints;

public static class TributarioEndpoints
{
    public static WebApplication MapTributarioEndpoints(this WebApplication app)
    {
        var api = app.MapGroup("/api");

        api.MapContribuinteEndpoints();
        api.MapDebitoEndpoints();

        return app;
    }
}
