using System.ComponentModel;

namespace ImportCars.Models
{
    public class Auctions
    {
        public int Id { get; set; }
        [DisplayName("Nazwa")]
        public string Name { get; set; } = default!;
        [DisplayName("Data końcowa")]
        public DateTime EndDate { get; set; }
        [DisplayName("Rok produkcji")]
        public int ProductionYear { get; set; }
        [DisplayName("Rodzaj silnika")]
        public string EngineType { get; set; } = default!;
        [DisplayName("Pojemność silnika")]
        public string EngineCapacity { get; set; } = default!;
        [DisplayName("Przewidywana cena")]
        public int Price { get; set; }
        [DisplayName("Adres strony")]
        public string Url { get; set; } = default!;
        [DisplayName("Zdjęcia")]
        public List<Images>? Images { get; set; }
    }
}
