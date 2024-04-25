using BabaNaplo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Web.Http.Cors;
namespace BabaNaplo.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [Route("api/[controller]")]
    [ApiController]
    public class NovekedesController : ControllerBase
    {
        private readonly BabanaploContext _context;  //AMÁ

        public NovekedesController(BabanaploContext context)  //AMÁ
        {
            _context = context;
        }



        [HttpGet]
        public IActionResult Get()
        {

            try
            {
                return Ok(_context.Novekedes.ToList());
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
            .Include(x => x.Novekedess)
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
        public IActionResult Post(Novekedes novekedes)
        {

            try
            {
                _context.Add(novekedes);
                _context.SaveChanges();
                return Ok(_context.Novekedes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        public IActionResult Put(Novekedes novekedes)
        {

            try
            {
                _context.Update(novekedes);
                _context.SaveChanges();
                return Ok(_context.Novekedes);
            }
            catch (Exception ex)

            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("deletenovekedes/{id}")]
        public IActionResult Delete(int id)
        {

            try
            {
                Novekedes novekedes = new Novekedes();
                novekedes.Id = id;
                _context.Remove(novekedes);
                _context.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, "Sikeres törlés.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }

        }

        [HttpGet("SearchNovekedesId/{id}")] //AMÁ
        public async Task<ActionResult<Novekedes>> SearchNovekedesId(int id)
        {
            var novekedesIds = await _context.Novekedes.FindAsync(id);

            if (novekedesIds == null)
            {
                return NotFound();
            }

            return novekedesIds;
        }


    }
}
