using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NousPainelAPI.DTOs;
using NousPainelAPI.Services;
using NousPainelAPI.Utils;
using NousPainelAPI.Data;

namespace NousPainelAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlunoController : ControllerBase
    {
        private readonly IAlunoService _service;
        private readonly AppDbContext _db;

        public AlunoController(IAlunoService service, AppDbContext db)
        {
            _service = service;
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var alunos = await _service.GetAllAsync();
            var alunosComLinks = alunos.Select(a => HateoasHelper.GenerateLinks(a, Url));
            return Ok(alunosComLinks);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var a = await _service.GetByIdAsync(id);
            if (a == null) return NotFound();
            return Ok(HateoasHelper.GenerateLinks(a, Url));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AlunoCreateDto dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, HateoasHelper.GenerateLinks(created, Url));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] AlunoCreateDto dto)
        {
            var ok = await _service.UpdateAsync(id, dto);
            return ok ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ok = await _service.DeleteAsync(id);
            return ok ? NoContent() : NotFound();
        }

        [HttpPost("{id}/checkin")]
        public async Task<IActionResult> Checkin(int id, [FromBody] CheckinCreateDto dto)
        {
            var ok = await _service.AddCheckinAsync(id, dto);
            return ok ? Ok() : NotFound();
        }

        // GET /api/aluno/search?nome=...&email=...&sortBy=nome|email&order=asc|desc&page=1&pageSize=10
        [HttpGet("search")]
        public async Task<IActionResult> Search(
            [FromQuery] string? nome,
            [FromQuery] string? email,
            [FromQuery] string? sortBy = "nome",
            [FromQuery] string? order = "asc",
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            if (page < 1) page = 1;
            if (pageSize < 1) pageSize = 10;

            var query = _db.Alunos.Include(a => a.Checkins).AsQueryable();

            if (!string.IsNullOrWhiteSpace(nome))
                query = query.Where(a => a.Nome.Contains(nome));

            if (!string.IsNullOrWhiteSpace(email))
                query = query.Where(a => a.Email.Contains(email));

            sortBy = (sortBy ?? "nome").ToLower();
            order = (order ?? "asc").ToLower();

            query = (sortBy, order) switch
            {
                ("email", "desc") => query.OrderByDescending(a => a.Email),
                ("email", _)      => query.OrderBy(a => a.Email),
                ("nome", "desc")  => query.OrderByDescending(a => a.Nome),
                _                  => query.OrderBy(a => a.Nome)
            };

            var totalItems = await query.CountAsync();
            var items = await query.Skip((page - 1) * pageSize)
                                   .Take(pageSize)
                                   .ToListAsync();

            var data = items.Select(a => HateoasHelper.GenerateLinks(a, Url));

            var result = new
            {
                page,
                pageSize,
                totalItems,
                totalPages = (int)Math.Ceiling(totalItems / (double)pageSize),
                data
            };

            return Ok(result);
        }
    }
}
