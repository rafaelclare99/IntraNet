using IntraNet.Data;
using IntraNet.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Authorize]
public class AvisosController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;

    public AvisosController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {
        var user = await _userManager.GetUserAsync(User);
        var roles = await _userManager.GetRolesAsync(user!);
        var setor = roles.FirstOrDefault();

        IQueryable<Avisos> query = _context.Avisos;

        if (!User.IsInRole("Admin"))
        {
            query = query.Where(a => a.Setor == null || a.Setor == setor);
        }

        var avisos = await query
            .OrderByDescending(a => a.DataCriacao)
            .ToListAsync();

        return View(avisos);
    }

    [Authorize(Roles = "Admin")]
    public IActionResult Criar()
    {
        return View("CriarAvisos");
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Criar(Avisos aviso)
    {
        if (!ModelState.IsValid)
            return View("CriarAvisos", aviso);

        aviso.AutorId = _userManager.GetUserId(User)!;
        aviso.DataCriacao = DateTime.Now;

        _context.Avisos.Add(aviso);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }
}
