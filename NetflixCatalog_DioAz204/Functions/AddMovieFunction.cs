using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Newtonsoft.Json;
using NetflixCatalog_DioAz204.Func.Model;
using NetflixCatalog_DioAz204.Func.Services;

namespace NetflixCatalog_DioAz204.Func.Functions;

public class AddMovieFunction
{
    private readonly AppDbContext _context;

    public AddMovieFunction(AppDbContext context)
    {
        _context = context;
    }

    [Function("AddMovie")]
    public async Task<IActionResult> AddMovie(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = "movies")] HttpRequestData req)
    {
        string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        var movie = JsonConvert.DeserializeObject<MovieModel>(requestBody);
        
        if (movie == null)
            return new BadRequestObjectResult("Invalid movie data");

        _context.Movies.Add(movie);
        await _context.SaveChangesAsync();
        
        return new OkObjectResult(movie);
    }
}
