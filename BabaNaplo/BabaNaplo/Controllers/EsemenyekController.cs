using BabaNaplo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Web.Http.Cors;

namespace BabaNaplo.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [Route("api/[controller]")]
    [ApiController]
    //[System.Web.Http.Authorize(Roles = "ADMIN,USER")]

    public class EsemenyekController : ControllerBase
    {

        private readonly BabanaploContext _context; //AMÁ

        public EsemenyekController(BabanaploContext context)  //AMÁ
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_context.Esemenyeks.ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }

        [HttpPost]
        public IActionResult Post(Esemenyek esemenyek)
        {
            try
            {
                _context.Add(esemenyek);
                _context.SaveChanges();
                return Ok(_context.Esemenyeks);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        public IActionResult Put(Esemenyek esemenyek)
        {
            try
            {
                _context.Update(esemenyek);
                _context.SaveChanges();
                return Ok(_context.Esemenyeks);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                Esemenyek esemenyek = new Esemenyek();
                esemenyek.Id = id;
                _context.Remove(esemenyek);
                _context.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, "Sikeres törlés.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }

        }

        [HttpGet("SearchEsemeny/{searchText}")] //AMÁ
        public async Task<ActionResult<IEnumerable<Esemenyek>>> SearchEsemeny(string searchText)
        {
            var esemeny = await _context.Esemenyeks
                .Where(e => EF.Functions.Like(e.Megnevezes, $"%{searchText}%"))
                .OrderBy(e => e.Megnevezes)
                .ToListAsync();
            return esemeny;
        }

        [HttpGet("SearchEsemenyId/{id}")] //AMÁ
        public async Task<ActionResult<Esemenyek>> SearchEsemenyId(int id)
        {
            var esemenyIds = await _context.Esemenyeks.FindAsync(id);

            if (esemenyIds == null)
            {
                return NotFound();
            }

            return esemenyIds;
        }

        [HttpGet("SearchEsemenyDatum/{egydatum}")] //AMÁ
        public async Task<ActionResult<IEnumerable<Esemenyek>>> SearchEsemenyDatum(string egydatum)
        {
            var esemeny = await _context.Esemenyeks
                .Where(e => EF.Functions.Like(e.Datum, $"{egydatum}"))
                .OrderBy(e => e.Megnevezes)
                .ToListAsync();
            return esemeny;
        }

        [HttpGet("SearchEsemenyDatumId/{egydatum}")] //AMÁ
        public async Task<ActionResult<IEnumerable<int>>> SearchEsemenyDatumId(string egydatum)
        {
            var esemenyIds = await _context.Esemenyeks
                .Where(e => EF.Functions.Like(e.Datum, $"{egydatum}"))
                .OrderBy(e => e.Megnevezes)
                .Select(e => e.Id)
                .ToListAsync();
            return esemenyIds;
        }

        [HttpGet("SearchEsemenyIntervallum/{egydatum},{ketdatum}")] //AMÁ
        public async Task<ActionResult<IEnumerable<Esemenyek>>> SearchEsemenyIntervallum(string egydatum, string ketdatum)
        {
            var esemeny = await _context.Esemenyeks
                .Where(e => e.Datum >= DateOnly.FromDateTime(Convert.ToDateTime(egydatum)) && e.Datum <= DateOnly.FromDateTime(Convert.ToDateTime(ketdatum)))
                .OrderBy(e => e.Datum)
                .ToListAsync();
            return esemeny;
        }

        [HttpGet("SearchEsemenyIntervallumId/{egydatum},{ketdatum}")] //AMÁ
        public async Task<ActionResult<IEnumerable<int>>> SearchEsemenyIntervallumId(string egydatum, string ketdatum)
        {
            var esemenyIds = await _context.Esemenyeks
                .Where(e => e.Datum >= DateOnly.FromDateTime(Convert.ToDateTime(egydatum)) && e.Datum <= DateOnly.FromDateTime(Convert.ToDateTime(ketdatum)))
                .OrderBy(e => e.Datum)
                .Select(e => e.Id)
                .ToListAsync();
            return esemenyIds;
        }



    }
}
