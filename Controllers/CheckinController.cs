using Microsoft.AspNetCore.Mvc;
using NousPainelAPI.Domain;

namespace NousPainelAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CheckinController : ControllerBase
{
    private static readonly List<Checkin> Checkins = new();

    [HttpGet]
    public IActionResult GetAll() => Ok(Checkins);

    [HttpPost]
    public IActionResult Create([FromBody] Checkin novo)
    {
        novo.Id = Checkins.Count + 1;
        Checkins.Add(novo);
        return CreatedAtAction(nameof(GetAll), novo);
    }
}
