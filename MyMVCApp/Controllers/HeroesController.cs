using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyMVCApp.Database;
using MyMVCApp.Entities.Heroes;
using MyMVCApp.Mappers;
using MyMVCApp.Models;

namespace MyMVCApp.Controllers;

public class HeroesController : Controller
{
    private readonly SqlLiteDbContext _dbContext;
    
    public HeroesController(SqlLiteDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var heroes = (await _dbContext.GetHeroesAsync()).Select(HeroMapper.ToViewModel);
        
        var classMap = _dbContext.Classes.ToDictionary(c => c.Id, c => c.Name);
        var skillMap = _dbContext.Skills.ToDictionary(s => s.Id, s => s.Name);

        ViewData["ClassMap"] = classMap;
        ViewData["SkillMap"] = skillMap;
        return View(heroes);
    }

    [HttpGet]
    public IActionResult Create()
    {
        PutSelectClassesToViewBag();
        PutSelectSkillsToViewBag();
        return View(new HeroViewModel());
    }

    
    
    private void PutSelectClassesToViewBag()
    {
        var classes = _dbContext.Classes.ToList();
        ViewBag.SelectClasses = new SelectList(classes, "Id", "Name");
    }
    
    private void PutSelectSkillsToViewBag()
    {
        var skills = _dbContext.Skills.ToList();
        ViewBag.SelectSkills = new SelectList(skills, "Id", "Name");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(HeroViewModel model)
    {
        if (ModelState.IsValid)
        {
            await TrySaveHeroImage(model);
            _dbContext.Heroes.Add(HeroMapper.ToEntity(model, await _dbContext.Skills.ToListAsync()));
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        
        PutSelectClassesToViewBag();
        PutSelectSkillsToViewBag();
        return View(model);
    }

    private async Task TrySaveHeroImage(HeroViewModel model)
    {
        if (model.HeroImageFile != null)
        {
            var fileName = Guid.NewGuid() + "_" + model.HeroImageFile.FileName;
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "heroes", fileName);
            await using var stream = new FileStream(filePath, FileMode.Create);
            await model.HeroImageFile.CopyToAsync(stream);
            model.ImageUrl = "/images/heroes/" + fileName;
        }
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
    public async Task<IActionResult> Update(int id)
    {
        var hero = (await _dbContext.GetHeroesAsync()).Find(h => h.Id == id);
        if (hero == null)
        {
            return NotFound();
        }
        
        PutSelectClassesToViewBag();
        PutSelectSkillsToViewBag();
        return View(HeroMapper.ToViewModel(hero));
    }

    public async Task<IActionResult> Update(HeroViewModel model)
    {
        if (ModelState.IsValid)
        {
            Console.WriteLine("Old image URL: " + model.ImageUrl);
            try
            {
                await TrySaveHeroImage(model);
                _dbContext.Update(HeroMapper.ToEntity(model));
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_dbContext.Heroes.Any(h => h.Id == model.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToAction("Index");
        }
        return View(model);
    }
}