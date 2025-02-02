using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.EntityFrameworkCore;
using NetflixCatalog_DioAz204.Func.Services;

namespace NetflixCatalog_DioAz204.Func.Functions;

public class GetMoviesFunction
{
    private readonly AppDbContext _context;

    public GetMoviesFunction(AppDbContext context)
    {
        _context = context;
    }

    [Function("GetMovies")]
    public async Task<IActionResult> GetMovies(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "movies")] HttpRequestData req)
    {
        var movies = await _context.Movies.ToListAsync();
        return new OkObjectResult(movies);
    }
}
