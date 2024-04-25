using System.ComponentModel;

namespace ImportCars.Models;

public class Questions
{
    public int Id { get; set; }
    [DisplayName("Pytanie")]
    public string Question { get; set; } = default!;
    [DisplayName("Odpowiedz")]
    public  string Answer { get; set; } = default!;
}
