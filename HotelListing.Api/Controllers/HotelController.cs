using HotelListing_Api.Data;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HotelListing_Api.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase {
        private static List<Hotel> hotels = new List<Hotel> { new Hotel { Id = 1, Name = "Grand Plaza", Address = "123 Main St", Rating = 4.5 }, new Hotel { Id = 2, Name = "Ocean View", Address = "456 Beach Rd", Rating = 4.8 } };

        // GET: api/<HotelController>
        [HttpGet]
        public ActionResult<IEnumerable<Hotel>> Get() {
            return Ok(hotels);
        }

        // GET api/<HotelController>/5
        [HttpGet("{id}")]
        public ActionResult<Hotel> Get(int id) {
            var hotel = hotels.FirstOrDefault(h => h.Id == id);

            if (hotel == null) {
                return NotFound();
            }
            return Ok(hotel);
        }

        // POST api/<HotelController>
        [HttpPost]
        public ActionResult<Hotel> Post([FromBody] Hotel newHotel) {
            if (hotels.Any(h => h.Id == newHotel.Id)) {
                return BadRequest("Hotel with the same Id already exists!");
            }

            hotels.Add(newHotel);
            return CreatedAtAction(nameof(Get), new { id = newHotel.Id }, newHotel);
        }

        // PUT api/<HotelController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Hotel updatedHotel) {
            var existingHotel = hotels.FirstOrDefault(h => h.Id == id);
            if (existingHotel != null) {
                existingHotel.Name = updatedHotel.Name;
                existingHotel.Address = updatedHotel.Address;
                existingHotel.Rating = updatedHotel.Rating;

                return NoContent();
            } else {
                return NotFound(new { message = "Hotel not found!" });
            }
        }

        // DELETE api/<HotelController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id) {
            var existingHotel = hotels.FirstOrDefault(h => h.Id == id);
            if (existingHotel != null) {
                hotels.Remove(existingHotel);
                return NoContent();
            } else {
                return NotFound(new { message = "Hotel not found!" });
            }
        }
    }
}
