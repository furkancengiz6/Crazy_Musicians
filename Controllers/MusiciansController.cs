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
            // Müzisyenlerin tamamını döndürüyoruz. 
            return Ok(musicians);
        }

        // GET isteği ile belirli bir müzisyeni almak için kullanılır. 
        // "/api/musicians/{id}" yoluna yapılan bir GET isteği ile tetiklenir.
        [HttpGet("{id}")]
        public IActionResult GetMusicianById(int id)
        {
            // Verilen ID'ye sahip müzisyeni arıyoruz.
            var musician = musicians.FirstOrDefault(m => m.Id == id);

            // Müzisyen bulunmazsa, 404 Not Found hatası döneriz.
            if (musician == null)
                return NotFound("Musician not found.");

            // Müzisyen bulunduysa, 200 OK ile müzisyeni döndürüyoruz.
            return Ok(musician);
        }

        // GET isteği ile müzisyenleri adıyla aramak için kullanılır.
        // "/api/musicians/search?name=John" şeklinde yapılan bir GET isteği ile tetiklenir.
        [HttpGet("search")]
        public IActionResult SearchMusicians([FromQuery] string name)
        {
            // "name" parametresine göre müzisyenleri filtreliyoruz.
            var filteredMusicians = musicians.Where(m => m.Name.Contains(name)).ToList();

            // Arama sonucu olan müzisyenler başarıyla döndürülür.
            return Ok(filteredMusicians);
        }

        // POST isteği ile yeni bir müzisyen eklemek için kullanılır. 
        // "/api/musicians" yoluna yapılan bir POST isteği ile tetiklenir.
        [HttpPost]
        public IActionResult CreateMusician([FromBody] Musician musician)
        {
            // Model doğrulama işlemi yapıyoruz. Eğer model geçersizse, 400 Bad Request hatası döneriz.
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Yeni müzisyen eklerken, ID'yi mevcut en yüksek ID'den bir artırarak veriyoruz.
            musician.Id = musicians.Max(m => m.Id) + 1;

            // Yeni müzisyen listeye ekleniyor.
            musicians.Add(musician);

            // Yeni oluşturulan müzisyeni, 201 Created statüsüyle döndürüyoruz.
            // Ayrıca, müzisyenin detaylarına ulaşılabilmesi için "CreatedAtAction" kullanıyoruz.
            return CreatedAtAction(nameof(GetMusicianById), new { id = musician.Id }, musician);
        }

        // PATCH isteği ile belirli bir müzisyeni kısmi olarak güncellemek için JSON Patch kullanıyoruz.
        // "/api/musicians/{id}" yoluna yapılan bir PATCH isteği ile tetiklenir.
        [HttpPatch("{id}")]
        public IActionResult UpdateMusicianPartialWithPatch(int id, [FromBody] JsonPatchDocument<Musician> patchDoc)
        {
            // Güncellenmek istenen müzisyen listede var mı diye kontrol ediyoruz.
            var musician = musicians.FirstOrDefault(m => m.Id == id);

            // Eğer müzisyen bulunamazsa, 404 Not Found hatası döneriz.
            if (musician == null)
                return NotFound("Musician not found.");

            // Eğer patch dokümanı (JSON Patch) geçerli ise, patch işlemi uygularız.
            if (patchDoc != null)
            {
                // JSON Patch işlemi ile müzisyenin sadece belirtilen alanlarını güncelliyoruz.
                patchDoc.ApplyTo(musician);
            }
            else
            {
                return BadRequest("Invalid patch document.");
            }

            // Güncellenen müzisyen bilgisi başarıyla döndürülür.
            return Ok(musician);
        }

        // PUT isteği ile müzisyenin tüm bilgilerini güncellemek için kullanılır. 
        // "/api/musicians/{id}" yoluna yapılan bir PUT isteği ile tetiklenir.
        [HttpPut("{id}")]
        public IActionResult UpdateMusician(int id, [FromBody] Musician musician)
        {
            // Model doğrulama işlemi yapıyoruz. Eğer model geçersizse, 400 Bad Request hatası döneriz.
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Müzisyen listede var mı diye kontrol ediyoruz.
            var existingMusician = musicians.FirstOrDefault(m => m.Id == id);

            // Müzisyen bulunmazsa 404 Not Found hatası döneriz.
            if (existingMusician == null)
                return NotFound("Musician not found.");

            // Müzisyen bilgilerini tamamen güncelliyoruz.
            existingMusician.Name = musician.Name;
            existingMusician.Profession = musician.Profession;
            existingMusician.FunFeature = musician.FunFeature;

            // Güncellenen müzisyen bilgisi başarıyla döndürülür.
            return Ok(existingMusician);
        }

        // DELETE isteği ile bir müzisyeni silmek için kullanılır. 
        // "/api/musicians/{id}" yoluna yapılan bir DELETE isteği ile tetiklenir.
        [HttpDelete("{id}")]
        public IActionResult DeleteMusician(int id)
        {
            // Silinecek müzisyen listede var mı diye kontrol ediyoruz.
            var musician = musicians.FirstOrDefault(m => m.Id == id);

            // Eğer müzisyen bulunamazsa 404 Not Found hatası döneriz.
            if (musician == null)
                return NotFound("Musician not found.");

            // Müzisyen bulunursa, listeden silinir.
            musicians.Remove(musician);

            // Silme işlemi başarıyla gerçekleştiğinde, 204 No Content döndürürüz.
            return NoContent();
        }
    }
}
