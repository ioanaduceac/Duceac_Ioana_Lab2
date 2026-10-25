using Microsoft.EntityFrameworkCore;


    public class Duceac_Ioana_Lab2Context(DbContextOptions<Duceac_Ioana_Lab2Context> options) : DbContext(options)
    {
        public DbSet<Duceac_Ioana_Lab2.Models.Book> Book { get; set; } = default!;
        public DbSet<Duceac_Ioana_Lab2.Models.Publisher> Publisher { get; set; } = default!;
        public DbSet<Duceac_Ioana_Lab2.Models.Authors> Authors { get; set; } = default!;
        public DbSet<Duceac_Ioana_Lab2.Models.Category> Category { get; set; } = default!;
        public DbSet<Duceac_Ioana_Lab2.Models.BookCategory> BookCategory { get; set; } = default!;
        public DbSet<Duceac_Ioana_Lab2.Models.AssignedCategoryData> AssignedCategoryData { get; set; } = default!;
    }

