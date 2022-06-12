using Microsoft.EntityFrameworkCore;

namespace Water.Models
{
    public class WaterContext:DbContext
    {
        public WaterContext(DbContextOptions<WaterContext> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-Q0IKKRN\\SQLEXPRESS;Initial Catalog=Water;Integrated Security=True");
        }

        public DbSet<Brand> Brands { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<Product> Products { get; set; }
       
    }
}
