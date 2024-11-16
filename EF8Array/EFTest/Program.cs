// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;

Console.WriteLine("Hello, World!");

// create a new instance of MyContext
MyContext context = new MyContext();


context.Database.EnsureCreated();

var cc = context.Pokemons.Select(p => new { p.Def}).ToList();

// insert 1000 rows 
// for (int i = 0; i < 1000; i++)
// {
//     var pokemon = new Pokemon
//     {
//         Stats = 100,
//         Atk = 100,
//         Def = Random.Shared.Next(100),
//         SpAtk = 100,
//         SpDef = 100,
//         Speed = 100,
//         HP = 100,
//         Name = "Pikachu"
//     };

//     context.Pokemons.Add(pokemon);
// }

// context.SaveChanges();

// update 1000 rows

// context.Pokemons.ToList().ForEach(p => p.Name = "Pikachu2");

// context.SaveChanges();

//  context.Set<Pokemon>().ExecuteUpdate(s => s.SetProperty(x => x.Def, s => Math.Sqrt(s.Def)));



public class MyContext : DbContext
{
    public DbSet<Pokemon> Pokemons { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlite("Data Source=test8c.db");
        options.LogTo(Console.WriteLine);
    }
}


public class Pokemon
{
    public int PokemonId { get; set; }
    public int Stats { get; set; }
    public int Atk { get; set; }
    public int Def { get; set; }
    public int SpAtk { get; set; }
    public int SpDef { get; set; }
    public int Speed { get; set; }
    public int HP { get; set; }
    public string Name { get; set; }
}