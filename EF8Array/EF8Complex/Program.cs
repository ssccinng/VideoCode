using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

MyContext context = new MyContext();

context.Database.EnsureDeleted();
context.Database.EnsureCreated();
var name = new Names { NameDetail = new NameDetail {
    NameDescription = "Electric Mouse Pokemon",
    NameType = "Electric"
}, English = "Pikachu", Japanese = "ピカチュウ", Chinese = "皮卡丘" };
var pokemon = new Pokemon { Names =  name};
var move = new Move { Names = name };

context.Pokemons.Add(pokemon);
context.Moves.Add(move);

context.SaveChanges();

name.English = "Pikachu2";

context.SaveChanges();

class MyContext : DbContext
{
    public DbSet<Pokemon> Pokemons { get; set; }
    public DbSet<Move> Moves { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite("Data Source=test8c.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Pokemon>()
            .ComplexProperty(e => e.Names, e => e.ComplexProperty(e => e.NameDetail));

        base.OnModelCreating(modelBuilder);
    }

}

[ComplexType]
public class Names {
    // public int NamesId { get; set; }
    public string English { get; set; }
    public string Japanese { get; set; }
    public string Chinese { get; set; }

    public NameDetail NameDetail { get; set; }
}
[ComplexType]

public class NameDetail {
    public string NameDescription { get; set; }
    public string NameType { get; set;  }
}

public class Pokemon {
    public int PokemonId { get; set; }
    public Names Names { get; set; }

    //..
}

public class Move {
    public int MoveId { get; set; }
    public Names Names { get; set; }

}