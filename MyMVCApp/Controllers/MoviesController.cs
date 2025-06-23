using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyMVCApp.Database;
using MyMVCApp.Mappers;
using MyMVCApp.Models;
using MyMVCApp.Repositories;

namespace MyMVCApp.Controllers;

public class MoviesController : Controller
{
    private readonly ILogger<MoviesController> _logger;
    private readonly SqlLiteDbContext _dbContext;
    
    public MoviesController(ILogger<MoviesController> logger, SqlLiteDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    public IActionResult MovieDetails(string title)
    {
        var movie = MovieRepository.GetMovieByTitle(_dbContext, title);
        if(movie == null) return NotFound();
        return View(movie);
    }
    
    public IActionResult AllMovies()
    {
        return View(MovieRepository.GetAllMovies(_dbContext));
    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}