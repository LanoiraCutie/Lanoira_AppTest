using Microsoft.AspNetCore.Mvc;

namespace LanoiraM_3rd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BeenieController : ControllerBase
    {
        private static readonly List<Beenie> _beenie = new List<Beenie>();

        [HttpGet] //api/beenie || GET
        public ActionResult<IEnumerable<Beenie>> GetAll()
        {
            return Ok(_beenie); //200 ok
        }

        [HttpGet("{id}")] //api/beenie/{id} || GET
        public ActionResult<Beenie> GetById(int id)
        {
            var beenie = _beenie.FirstOrDefault(b => b.Id == id);
            if (beenie == null)
                return NotFound(); //404 notfound

            return Ok(beenie); //200 ok
        }

        [HttpPost] //api/beenie || POST
        public ActionResult<Beenie> Create(Beenie beenie)
        {
            if (beenie == null)
                return BadRequest(); //400 badrequest
            beenie.Id = _beenie.Count > 0 ? _beenie.Max(b => b.Id) + 1 : 1;
            _beenie.Add(beenie);

            //201 created w loc header
            return CreatedAtAction(nameof(GetById), new { id = beenie.Id }, beenie);
        }

        [HttpPut("{id}")] //api/beenie/{id} || PUT
        public IActionResult Update(int id, Beenie updatedBeenie)
        {
            if (updatedBeenie == null || updatedBeenie.Id != id)
                return BadRequest(); //400 br

            var existingBeenie = _beenie.FirstOrDefault(b => b.Id == id);
            if (existingBeenie == null)
                return NotFound(); //404 nf

            existingBeenie.Name = updatedBeenie.Name;
            existingBeenie.Age = updatedBeenie.Age;
            existingBeenie.Color = updatedBeenie.Color;
            return NoContent(); //204 no content
        }

        [HttpDelete("{id}")] //api/beenie/{id} || DELETE
        public IActionResult Delete(int id)
        {
                var beenie = _beenie.FirstOrDefault(be => be.Id == id);
                if (beenie == null) return NotFound();

                _beenie.Remove(beenie);
                return NoContent(); //204 nc
        }
    }
    public class Beenie
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Color { get; set; }
        public required int Age { get; set; }
    }
}
