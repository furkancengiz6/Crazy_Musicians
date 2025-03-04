using System.ComponentModel.DataAnnotations;

namespace Crazy_Musicians.Models
{
    // Müzisyen modelini tanımlıyoruz. Bu model API üzerinden veritabanı yerine statik listeye kaydedilecek.
    public class Musician
    {
        // Müzisyen ID'si
        public int Id { get; set; }

        // Müzisyen adı
        public string Name { get; set; }

        // Müzisyenin mesleği
        public string Profession { get; set; }

        // Müzisyenin eğlenceli özelliği
        public string FunFeature { get; set; }
    }
}
