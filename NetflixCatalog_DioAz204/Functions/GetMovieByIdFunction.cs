using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using NetflixCatalog_DioAz204.Func.Services;

namespace NetflixCatalog_DioAz204.Func.Functions;

public class GetMovieByIdFunction
{
    private readonly AppDbContext _context;

    public GetMovieByIdFunction(AppDbContext context)
    {
        _context = context;
    }

    [Function("GetMovieById")]
    public async Task<IActionResult> GetMovieById(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "movies/{id}")] HttpRequestData req,
        string id)
    {
        var movie = await _context.Movies.FindAsync(id);
        if (movie == null)
            return new NotFoundObjectResult("Movie not found");

        return new OkObjectResult(movie);
    }
}
