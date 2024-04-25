using System.ComponentModel.DataAnnotations.Schema;

namespace ImportCars.Models
{
    public class Images
    {
        public int Id { get; set; }
        public int? AuctionId { get; set; }
        public virtual Auctions? Auction { get; set; }
        public string Path { get; set; }

        public Images(string path)
        {
            Path = path;
        }
    }
}
