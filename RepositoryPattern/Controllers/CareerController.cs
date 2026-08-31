using Microsoft.AspNetCore.Mvc;
using RepositoryPattern.Models;
using RepositoryPattern.Services.Interfaces;

namespace RepositoryPattern.Controllers;

[Route("careers")]
public class CareerController : Controller
{
    private readonly ICareerService _careerService;

    public CareerController(ICareerService careerService)
    {
        _careerService = careerService;
    }

    [HttpGet("")]
    public async Task<IActionResult> Index()
    {
        IReadOnlyList<Career> careers = await _careerService.GetAllAsync();

        return View(careers);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Details(int id)
    {
        Career? career = await _careerService.GetByIdAsync(id);

        if (career is null)
        {
            return NotFound();
        }

        return View(career);
    }

    [HttpGet("create")]
    public IActionResult Create()
    {
        return View(new Career());
    }

    [HttpPost("create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Career model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        Career? createdCareer = await _careerService.CreateAsync(model);

        if (createdCareer is null)
        {
            ModelState.AddModelError(nameof(model.Title), "A career with this title already exists.");

            return View(model);
        }

        TempData["SuccessMessage"] = "Career created successfully.";

        return RedirectToAction(nameof(Index));
    }

    [HttpGet("{id:int}/edit")]
    public async Task<IActionResult> Edit(int id)
    {
        Career? career =
            await _careerService.GetByIdAsync(id);

        if (career is null)
        {
            return NotFound();
        }

        return View(career);
    }

    [HttpPost("{id:int}/edit")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
        int id,
        Career model)
    {
        if (id != model.Id)
        {
            return BadRequest();
        }

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        Career? updatedCareer = await _careerService.UpdateAsync(model);

        if (updatedCareer is null)
        {
            ModelState.AddModelError(nameof(model.Title), "The career could not be updated. The title may already be in use or the career may no longer exist.");

            return View(model);
        }

        TempData["SuccessMessage"] = "Career updated successfully.";

        return RedirectToAction(nameof(Index));
    }

    [HttpGet("{id:int}/delete")]
    public async Task<IActionResult> Delete(int id)
    {
        Career? career =
            await _careerService.GetByIdAsync(id);

        if (career is null)
        {
            return NotFound();
        }

        return View(career);
    }

    [HttpPost("{id:int}/delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        bool deleted =
            await _careerService.DeleteAsync(id);

        if (!deleted)
        {
            TempData["ErrorMessage"] = "The career could not be deleted because it does not exist.";

            return RedirectToAction(nameof(Index));
        }

        TempData["SuccessMessage"] = "Career deleted successfully.";

        return RedirectToAction(nameof(Index));
    }
}