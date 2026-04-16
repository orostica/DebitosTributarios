namespace DebitosTributarios.Api.Endpoints;

public static class ContribuinteEndpoints
{
    public static RouteGroupBuilder MapContribuinteEndpoints(this RouteGroupBuilder group)
    {


        group.MapPost("/contribuintes", async (CancellationToken cancellationToken) =>
        {
            
        });

        group.MapGet("/contribuintes/{id:guid}", async (Guid id) =>
        {
            return Results.Ok(true);
        });

        return group;
    }
}