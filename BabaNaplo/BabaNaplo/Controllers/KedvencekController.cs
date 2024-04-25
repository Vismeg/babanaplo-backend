using BabaNaplo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Web.Http.Cors;

namespace BabaNaplo.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [Route("api/[controller]")]
    [ApiController]
    public class KedvencekController : ControllerBase
    {
        private readonly BabanaploContext _context;  //AMÁ

        public KedvencekController(BabanaploContext context)  //AMÁ
        {
            _context = context;
        }



        [HttpGet]
        public IActionResult Get()
        {

            try
            {
                return Ok(_context.Kedvenceks.ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }

        [HttpGet("{felhsznaloId}")]
        public IActionResult Get(Guid felhsznaloId)
        {
            var result = _context.Szuletes
            .Include(x => x.Kedvenceks)
            .Where(x => x.FelhasznaloId == felhsznaloId);
            var context = new BabanaploContext();
            try
            {
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Post(Kedvencek kedvencek)
        {

            try
            {
                _context.Add(kedvencek);
                _context.SaveChanges();
                return Ok(_context.Kedvenceks);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public IActionResult Put(Kedvencek kedvencek)
        {

            try
            {
                _context.Update(kedvencek);
                _context.SaveChanges();
                return Ok(_context.Kedvenceks);
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
                Kedvencek kedvencek = new Kedvencek();
                kedvencek.Id = id;
                _context.Remove(kedvencek);
                _context.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, "Sikeres törlés.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }

        }

        [HttpGet("SearchKedvencId/{id}")] //AMÁ
        public async Task<ActionResult<Kedvencek>> SearchKedvencId(int id)
        {
            var kedvencIds = await _context.Kedvenceks.FindAsync(id);

            if (kedvencIds == null)
            {
                return NotFound();
            }

            return kedvencIds;
        }


    }
}
