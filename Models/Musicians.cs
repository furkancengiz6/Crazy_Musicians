using System.ComponentModel.DataAnnotations;

namespace Crazy_Musicians.Models
{
    // Müzisyen modelini tanımlıyoruz. Bu model API üzerinden veritabanı yerine statik listeye kaydedilecek.
    public class Musician
    {
        // Müzisyen ID'si
        public int Id { get; set; }

        // Müzisyen adı
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name length can't be more than 100 characters.")]
        public string Name { get; set; }

        // Müzisyenin mesleği
        [Required(ErrorMessage = "Profession is required.")]
        [StringLength(50, ErrorMessage = "Profession length can't be more than 50 characters.")]
        public string Profession { get; set; }

        // Müzisyenin eğlenceli özelliği
        [StringLength(200, ErrorMessage = "Fun feature length can't be more than 200 characters.")]
        public string FunFeature { get; set; }
    }
}
