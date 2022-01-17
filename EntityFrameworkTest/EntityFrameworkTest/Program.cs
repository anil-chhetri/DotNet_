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

var factory = new CookBookContextFactory();
using var context = factory.CreateDbContext(args);


#region part1

//Console.WriteLine("Adding porridge to database.");
//var porridge = new Dish() { Title = "Breakfast Porridge", Notes = "This is soo good!.", Stars = 4 };

//context.Dishes.Add(porridge);
//await context.SaveChangesAsync();

//Console.WriteLine($"complete adding porridge (ID = {porridge.Id}).");



//Console.WriteLine("checking the star of porridge");

//var dishes = context.Dishes.Where(d => d.Title.Contains("Porridge"));

//Console.WriteLine(dishes.ToQueryString()); 

//var dishes1 = dishes.ToList<Dish>();

//if (dishes1.Count != 1) Console.Error.WriteLine("something bad happened.");

//Console.WriteLine($"Porridge dish has stars: {dishes1[0].Stars}");
//Console.WriteLine("deleting the data.");



//Console.WriteLine("Updating the stars");
//porridge.Stars = 5;

////context.Update(porridge);
//await context.SaveChangesAsync();




//context.Dishes.Remove(porridge);
//await context.SaveChangesAsync();

//Console.WriteLine("deletig completed.");


#endregion


#region part2

//var newDish = new Dish() { Title = "foo", Notes = "bar" };
//context.Dishes.Add(newDish);
//await context.SaveChangesAsync();

////updating
//newDish.Notes = "baz";
//await context.SaveChangesAsync();


//await EntityState(factory, args);

////understanding tracking of EF 
//static async Task EntityState(CookBookContextFactory factory, string[] args)
//{
//    using var dbcontext = factory.CreateDbContext(args);

//    var newDish = new Dish() { Title = "foo", Notes = "bar" };
//    var state = dbcontext.Entry(newDish).State;  //Detached.

//    dbcontext.Dishes.Add(newDish);
//    state = dbcontext.Entry(newDish).State; //Added


//    await dbcontext.SaveChangesAsync();
//    state = dbcontext.Entry(newDish).State; //UnChanged.


//    newDish.Notes = "baz";
//    state = dbcontext.Entry(newDish).State;  //modified.


//    await dbcontext.SaveChangesAsync();
//    state = dbcontext.Entry(newDish).State;  //Unchanged.

//    dbcontext.Remove(newDish);
//    state = dbcontext.Entry(newDish).State;  //Deleted.

//    await dbcontext.SaveChangesAsync();
//    state = dbcontext.Entry(newDish).State;  //Detached.


//}


#endregion

#region Inheritance and Relationship

#endregion


//create the model classes.

class Dish
{
    public int Id { get; set; }

    [MaxLength(100)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(1000)]
    public string? Notes { get; set; }

    public int? Stars { get; set; }

    public List<DishIngredient> Ingredients { get; set; } = new();

}


class DishIngredient
{
    public int Id { get; set; }

    [MaxLength(100)]
    public string Descriptions { get; set; } = string.Empty;

    [MaxLength(50)]
    public string UnitOfMeasure { get; set; } = string.Empty;

    [Column(TypeName = "decimal(5,2)")]
    public decimal Amount { get; set; }

    public Dish? Dish { get; set; }

    public int DishId { get; set; }


}


class CookBookContext : DbContext
{
    public DbSet<Dish> Dishes { get; set; }

    public DbSet<DishIngredient> Ingredients { get; set; }
    public CookBookContext(DbContextOptions<CookBookContext> options) : base(options)
    { }
}


class CookBookContextFactory : IDesignTimeDbContextFactory<CookBookContext>
{
    public CookBookContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

        var optionsBuilder = new DbContextOptionsBuilder<CookBookContext>();

        optionsBuilder
            .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()))
            //.UseSqlServer(configuration["ConnectionStrings:DefaultConnection"]);
            .UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

        return new CookBookContext(optionsBuilder.Options);
    }
}
