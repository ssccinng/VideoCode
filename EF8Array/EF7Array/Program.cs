// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

System.Console.WriteLine("Hello, World!");

MyContext context = new MyContext();

context.Database.EnsureDeleted();
context.Database.EnsureCreated();

var blog = new Blog { Url = "http://sample.com", PostIds = new List<int> { 1, 2, 3 } };

context.Blogs.Add(blog);

context.SaveChanges();

blog.PostIds.Add(4);

context.SaveChanges();


public class MyContext : DbContext
{
    public DbSet<Blog> Blogs  { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite("Data Source=test7.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Blog>()
            .Property(e => e.PostIds)
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList()
            ,
            new ValueComparer<List<int>>(
                (c1, c2) => c1.SequenceEqual(c2),
                c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                c => c.ToList()
            ));
        base.OnModelCreating(modelBuilder);
    }

}

public class Blog
{
    public int BlogId { get; set; }
    public string Url { get; set; }
    public List<int> PostIds { get; set; }
}