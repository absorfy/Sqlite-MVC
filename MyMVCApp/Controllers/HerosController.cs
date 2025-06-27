using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyMVCApp.Database;
using MyMVCApp.Entities.Heros;

namespace MyMVCApp.Controllers;

public class HerosController : Controller
{
    private readonly SqlLiteDbContext _dbContext;
    
    public HerosController(SqlLiteDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View(_dbContext.Heroes.Include(h => h.Class).ToList());
    }

    [HttpGet]
    public IActionResult Create()
    {
        PutClassesToViewBag();
        return View(new HeroEntity());
    }
    
    private void PutClassesToViewBag()
    {
        var classes = _dbContext.Classes.ToList();
        ViewBag.Classes = new SelectList(classes, "Id", "Name");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(HeroEntity entity)
    {
        if (ModelState.IsValid)
        {
            _dbContext.Heroes.Add(entity);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
        
        PutClassesToViewBag();
        return View(entity);
    }

    public IActionResult Delete(int id)
    {
        var hero = _dbContext.Heroes.Find(id);
        if (hero != null)
        {
            _dbContext.Heroes.Remove(hero);
            _dbContext.SaveChanges();
        }

        return RedirectToAction("Index");
    }
    
    [HttpGet]
    public IActionResult Update(int id)
    {
        var hero = _dbContext.Heroes.Include(h => h.Class).FirstOrDefault(h => h.Id == id);
        if (hero == null)
        {
            return NotFound();
        }
        
        PutClassesToViewBag();
        return View(hero);
    }

    public IActionResult Update(HeroEntity entity)
    {
        if (ModelState.IsValid)
        {
            var existingHero = _dbContext.Heroes.Find(entity.Id);
            if (existingHero != null)
            {
                existingHero.Name = entity.Name;
                existingHero.ClassId = entity.ClassId;
                existingHero.Class = entity.Class;

                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return NotFound();
        }

        return View(entity);
    }
}