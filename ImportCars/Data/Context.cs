using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ImportCars.Models;

namespace ImportCars.Data
{
    public class Context : DbContext
    {
        public Context (DbContextOptions<Context> options)
            : base(options)
        {
        }

        public DbSet<ImportCars.Models.Auctions> Auctions { get; set; } = default!;
        public DbSet<ImportCars.Areas.Admin.Models.Admin> Admin { get; set; } = default!;
        public DbSet<ImportCars.Models.Images> Images { get; set; } = default!;
        public DbSet<ImportCars.Models.Questions>? Questions { get; set; }
    }
}
