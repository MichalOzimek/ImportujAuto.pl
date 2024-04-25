using ImportCars.Models;

namespace ImportCars.ViewModels;

public class HomePageModel
{
    public IEnumerable<Auctions>? Auctions { get; set; }
    public IEnumerable<Questions>? Questions { get; set; }
    public bool IsHomePage { get; set; }
}
