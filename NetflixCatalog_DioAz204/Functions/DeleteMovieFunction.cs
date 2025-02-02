using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using NetflixCatalog_DioAz204.Func.Services;

namespace NetflixCatalog_DioAz204.Func.Functions;

public class DeleteMovieFunction
{
    private readonly AppDbContext _context;

    public DeleteMovieFunction(AppDbContext context)
    {
        _context = context;
    }

    [Function("DeleteMovie")]
    public async Task<IActionResult> DeleteMovie(
        [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "movies/{id}")] HttpRequestData req,
        string id)
    {
        var movie = await _context.Movies.FindAsync(id);
        if (movie == null)
            return new NotFoundObjectResult("Movie not found");

        _context.Movies.Remove(movie);
        await _context.SaveChangesAsync();

        return new OkObjectResult("Movie deleted");
    }
}
