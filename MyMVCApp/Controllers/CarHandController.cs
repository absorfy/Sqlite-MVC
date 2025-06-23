using Microsoft.AspNetCore.Mvc;
using MyMVCApp.Database;
using MyMVCApp.Entities;

namespace MyMVCApp.Controllers;

public class CarHandController : Controller
{
    private readonly SqlLiteDbContext _dbContext;
    
    public CarHandController(SqlLiteDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    [HttpGet]
    public IActionResult Create()
    {
        CarEntity newCar = new CarEntity();
        return View(newCar);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(CarEntity newCar)
    {
        if (ModelState.IsValid)
        {
            _dbContext.Cars.Add(newCar);
            _dbContext.SaveChanges();

            return RedirectToAction("Index");
        }
        
        return View(newCar);
    }
    
    [HttpGet]
    public IActionResult Update(int id)
    {
        var car = _dbContext.Cars.FirstOrDefault(c => c.Id == id);
        if (car == null)
        {
            return NotFound();
        }
        return View(car);
    }

    [HttpPost]
    public IActionResult Update(CarEntity updatedCar)
    {
        if (ModelState.IsValid)
        {
            var existingCar = _dbContext.Cars.FirstOrDefault(c => c.Id == updatedCar.Id);
            if (existingCar != null)
            {
                existingCar.ModelName = updatedCar.ModelName;
                existingCar.Manufacturer = updatedCar.Manufacturer;
                existingCar.Year = updatedCar.Year;
                existingCar.Color = updatedCar.Color;
                existingCar.Price = updatedCar.Price;

                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return NotFound();
        }
        
        return View(updatedCar);
    }
    
    public IActionResult DeleteById(int id)
    {
        var car = _dbContext.Cars.FirstOrDefault(c => c.Id == id);
        if (car != null)
        {
            _dbContext.Cars.Remove(car);
            _dbContext.SaveChanges();
        }
        return RedirectToAction("Index");
    }
    
    [HttpGet]
    public IActionResult Index(string searchString, string sortOrder)
    {
        ViewData["CurrentFilter"] = searchString;
        ViewData["ModelNameSort"] = sortOrder switch
        {
            "model" => "model_desc",
            "model_desc" => "model_asc",
            _ => "model"
        };

        var cars = _dbContext.Cars.AsQueryable();

        if (!string.IsNullOrEmpty(searchString))
        {
            cars = cars.Where(c => c.ModelName.Contains(searchString) ||
                                   c.Manufacturer.Contains(searchString) ||
                                   c.Color.Contains(searchString));
        }
        
        switch (ViewData["ModelNameSort"])
        {
            case "model_asc":
                cars = cars.OrderBy(c => c.ModelName);
                break;
            case "model_desc":
                cars = cars.OrderByDescending(c => c.ModelName);
                break;
            default:
                break;
        }

        return View(cars.ToList());
    }
    
    public IActionResult ReadById(int id)
    {
        return View(_dbContext.Cars.FirstOrDefault(c => c.Id == id));
    }
}