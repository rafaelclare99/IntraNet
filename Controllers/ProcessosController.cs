using IntraNet.Data;
using IntraNet.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Authorize]
public class ProcessosController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IWebHostEnvironment _env;

    public ProcessosController(ApplicationDbContext context, UserManager<IdentityUser> userManager, IWebHostEnvironment env)
    {
        _context = context;
        _userManager = userManager;
        _env = env;
    }

    public async Task<IActionResult> Index()
    {
        var user = await _userManager.GetUserAsync(User);
        var roles = await _userManager.GetRolesAsync(user!);
        var setor = roles.FirstOrDefault();

        IQueryable<Processo> query = _context.Processos;

        // 🔐 Admin vê tudo
        if (!User.IsInRole("Admin"))
        {
            query = query.Where(p => p.Setor == setor);
            // se quiser permitir processos gerais:
            // query = query.Where(p => p.Setor == null || p.Setor == setor);
        }

        var processos = await query
            .OrderByDescending(p => p.DataCriacao)
            .ToListAsync();

        return View(processos);
    }

    [Authorize(Roles = "Admin")]
    public IActionResult Criar() => View();

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Criar(Processo processo, IFormFile arquivo)
    {
        var pasta = Path.Combine(_env.WebRootPath, "uploads");
        Directory.CreateDirectory(pasta);

        var nomeArquivo = $"{Guid.NewGuid()}_{arquivo.FileName}";
        var caminho = Path.Combine(pasta, nomeArquivo);

        using var stream = new FileStream(caminho, FileMode.Create);
        await arquivo.CopyToAsync(stream);

        processo.ArquivoPath = "/uploads/" + nomeArquivo;
        processo.DataCriacao = DateTime.Now;

        _context.Processos.Add(processo);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }
}
