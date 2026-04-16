namespace DebitosTributarios.Api.Endpoints;

public static class DebitoEndpoints
{
    public static RouteGroupBuilder MapDebitoEndpoints(this RouteGroupBuilder group)
    {
        group.MapPost("/debitos", async (CancellationToken cancellationToken) =>
        {


            return Results.Ok(true);
        });

        return group;
    }
}