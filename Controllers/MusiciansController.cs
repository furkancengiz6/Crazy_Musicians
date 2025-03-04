using Crazy_Musicians.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch; // JSON Patch işlemleri için gerekli namespace
using System.Collections.Generic;
using System.Linq;

namespace Crazy_Musicians.Controllers
{
    // API için rota tanımlaması. "/api/musicians" ile başlayacak olan tüm istekler bu controller'a yönlendirilir.
    [Route("api/[controller]")]
    [ApiController]
    public class MusiciansController : ControllerBase
    {
        // Statik bir liste kullanarak müzisyen verilerini saklıyoruz. Bu, gerçek bir veritabanı yerine geçiyor.
        private static List<Musician> musicians = new List<Musician>
        {
            new Musician { Id = 1, Name = "John Doe", Profession = "Guitarist", FunFeature = "Plays blindfolded" },
            new Musician { Id = 2, Name = "Jane Smith", Profession = "Drummer", FunFeature = "Can play 5 rhythms at once" }
        };

        // GET isteği ile tüm müzisyenleri almak için kullanılır. 
        // "/api/musicians" yoluna yapılan bir GET isteği ile tetiklenir.
        [HttpGet]
        public IActionResult GetAllMusicians()
        {
            return Ok(musicians); // Tüm müzisyenleri döndürüyoruz.
        }

        // GET isteği ile belirli bir müzisyeni almak için kullanılır. 
        // "/api/musicians/{id}" yoluna yapılan bir GET isteği ile tetiklenir.
        [HttpGet("{id}")]
        public IActionResult GetMusicianById(int id)
        {
            var musician = musicians.FirstOrDefault(m => m.Id == id);

            if (musician == null)
                return NotFound("Musician not found.");

            return Ok(musician); // Müzisyen bulunduysa döndürüyoruz.
        }

        // GET isteği ile müzisyenleri adıyla aramak için kullanılır.
        // "/api/musicians/search?name=John" şeklinde yapılan bir GET isteği ile tetiklenir.
        [HttpGet("search")]
        public IActionResult SearchMusicians([FromQuery] string name)
        {
            // "name" parametresine göre müzisyenleri filtreliyoruz.
            var filteredMusicians = musicians.Where(m => m.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();

            if (!filteredMusicians.Any())
                return NotFound("No musicians found matching the search criteria.");

            return Ok(filteredMusicians); // Arama sonucu döndürüyoruz.
        }

        // POST isteği ile yeni bir müzisyen eklemek için kullanılır. 
        // "/api/musicians" yoluna yapılan bir POST isteği ile tetiklenir.
        [HttpPost]
        public IActionResult CreateMusician([FromBody] Musician musician)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState); // Model doğrulaması başarısızsa hata döneriz.

            musician.Id = musicians.Max(m => m.Id) + 1; // Yeni müzisyenin ID'sini belirliyoruz.
            musicians.Add(musician); // Yeni müzisyen ekliyoruz.

            return CreatedAtAction(nameof(GetMusicianById), new { id = musician.Id }, musician); // 201 Created ile yanıt veriyoruz.
        }

        // PATCH isteği ile belirli bir müzisyeni kısmi olarak güncellemek için JSON Patch kullanıyoruz.
        // "/api/musicians/{id}" yoluna yapılan bir PATCH isteği ile tetiklenir.
        [HttpPatch("{id}")]
        public IActionResult UpdateMusicianPartialWithPatch(int id, [FromBody] JsonPatchDocument<Musician> patchDoc)
        {
            var musician = musicians.FirstOrDefault(m => m.Id == id);

            if (musician == null)
                return NotFound("Musician not found.");

            if (patchDoc != null)
            {
                patchDoc.ApplyTo(musician); // JSON Patch işlemini uyguluyoruz.
            }
            else
            {
                return BadRequest("Invalid patch document.");
            }

            return Ok(musician); // Güncellenen müzisyeni döndürüyoruz.
        }

        // PUT isteği ile müzisyenin tüm bilgilerini güncellemek için kullanılır. 
        // "/api/musicians/{id}" yoluna yapılan bir PUT isteği ile tetiklenir.
        [HttpPut("{id}")]
        public IActionResult UpdateMusician(int id, [FromBody] Musician musician)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState); // Model doğrulaması başarısızsa hata döneriz.

            var existingMusician = musicians.FirstOrDefault(m => m.Id == id);

            if (existingMusician == null)
                return NotFound("Musician not found.");

            // Mevcut müzisyenin bilgilerini güncelliyoruz.
            existingMusician.Name = musician.Name;
            existingMusician.Profession = musician.Profession;
            existingMusician.FunFeature = musician.FunFeature;

            return Ok(existingMusician); // Güncellenen müzisyeni döndürüyoruz.
        }

        // DELETE isteği ile bir müzisyeni silmek için kullanılır. 
        // "/api/musicians/{id}" yoluna yapılan bir DELETE isteği ile tetiklenir.
        [HttpDelete("{id}")]
        public IActionResult DeleteMusician(int id)
        {
            var musician = musicians.FirstOrDefault(m => m.Id == id);

            if (musician == null)
                return NotFound("Musician not found.");

            musicians.Remove(musician); // Müzisyen listeden siliniyor.

            return NoContent(); // Silme işlemi başarılıysa, 204 No Content döneriz.
        }
    }
}
