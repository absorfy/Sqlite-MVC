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
            _dbContext.Heroes.Add(HeroMapper.ToEntity(model, await _dbContext.Skills.ToListAsync()));
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        
        PutSelectClassesToViewBag();
        PutSelectSkillsToViewBag();
        return View(model);
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

    public IActionResult Update(HeroViewModel model)
    {
        if (ModelState.IsValid)
        {
            try
            {
                _dbContext.Update(HeroMapper.ToEntity(model));
                _dbContext.SaveChangesAsync();
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