using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

Console.WriteLine("Hello World!");

var factory = new BrickFactory();
using var context = factory.CreateDbContext();


//await AddData();

await QueryData();

async Task AddData()
{
    Vendor BrickKings, helDerStine;
    await context.AddRangeAsync(new[]
    {
        BrickKings = new Vendor() {VendorName="Brick Kings" },
        helDerStine = new Vendor() {VendorName="Hel Der Stine "},
    });

    await context.SaveChangesAsync();

    Tag rare, ninjago, minecraft;
    await context.AddRangeAsync(new[]
    {
        rare = new Tag() {Title="Rare"},
        ninjago = new Tag() { Title="Ninjago" },
        minecraft = new Tag() { Title = "Mine Craft" }
    });

    await context.SaveChangesAsync();


    await context.AddAsync(new BasePlate(){
            Title = "Base palate 16 X 16 with blue water pattern",
            Color = color.Green,
            Tags = new() { rare, ninjago },
            Length = 16,
            Width = 16,
            Avialability = new()
            {
                new() { Vendor = BrickKings, AvailableAmount = 5, PriceEur=6.5m  },
                new() { Vendor = helDerStine, AvailableAmount = 5, PriceEur=9m  },
            }
            
    });

    await context.SaveChangesAsync();
}

async Task QueryData()
{
    var avialableBricks = await context.BrickAvailability
                            .Include(b => b.Brick)
                            .Include(v => v.Vendor)
                            .ToArrayAsync();

    foreach (var item in avialableBricks)
    {
        Console.WriteLine($"Bricks {item.Brick.Title} aviable at {item.Vendor.VendorName} at price {item.PriceEur}");
    }



    var BricksWithVendorAndTags = await context.Bricks
                                    .Include(nameof(Brick.Avialability) + "." + nameof(BrickAvailability.Vendor))
                                    .Include(t => t.Tags)
                                    .ToArrayAsync();


    foreach (var item in BricksWithVendorAndTags)
    {
        Console.WriteLine($"Bricks: {item.Title}");
        if (item.Tags.Any()) Console.WriteLine("Tags are: " + string.Join(",", item.Tags.Select(t => t.Title)));
        if (item.Avialability.Any()) Console.WriteLine("Vendor are : " + string.Join(',', item.Avialability.Select(v => v.Vendor.VendorName)));
    }
    
                                    

}

Console.WriteLine("done.");


enum color
{
    Black,
    White,
    Red,
    Yellow,
    Orange,
    Green
}

#region Model
class Brick
{
    public int Id { get; set; }

    [StringLength(255)]
    public string Title { get; set; } = string.Empty;

    public color? Color { get; set; }

    public List<Tag> Tags { get; set; }

    public List<BrickAvailability> Avialability { get; set; } = new();

}
class BasePlate : Brick
{
    public int Length { get; set; }

    public int Width { get; set; }

}
class MinifigHead : Brick
{
    public bool IsDualSided { get; set; }

}
class Tag
{
    public int Id { get; set; }

    [StringLength(250)]
    public string Title { get; set; } = string.Empty;

    public List<Brick> Bricks { get; set; }

}
class Vendor
{
    public int Id { get; set; }

    [MaxLength(250)]
    public string VendorName { get; set; }

    public List<BrickAvailability> Availability { get; set; } = new();

}
class BrickAvailability
{
    public int Id { get; set; }

    public Vendor Vendor { get; set; }

    public int VendorId { get; set; }

    public int BrickId { get; set; }

    public Brick Brick { get; set; }

    public int AvailableAmount { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal PriceEur { get; set; }


}
#endregion

#region DbContext 
class BrickDbContext : DbContext
{
    public BrickDbContext(DbContextOptions<BrickDbContext> options)
        : base(options)
    { }


    public DbSet<Brick> Bricks { get; set; }
    public DbSet<Vendor> Vendors { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<BrickAvailability> BrickAvailability { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BasePlate>().HasBaseType<Brick>();
        modelBuilder.Entity<MinifigHead>().HasBaseType<Brick>();
    }

}


class BrickFactory : IDesignTimeDbContextFactory<BrickDbContext>
{
    public BrickDbContext CreateDbContext(string[] args = null)
    {
        var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

        var optionsBuilder = new DbContextOptionsBuilder<BrickDbContext>();

        optionsBuilder
            .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()))
            .UseSqlServer(configuration.GetConnectionString("default"));

        return new BrickDbContext(optionsBuilder.Options);
    }
}


#endregion

