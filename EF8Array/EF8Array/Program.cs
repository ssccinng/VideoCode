// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

System.Console.WriteLine("Hello, World!");

MyContext context = new MyContext();

context.Database.EnsureDeleted();
context.Database.EnsureCreated();

var blog = new Blog { Url = "http://sample.com", PostIds =["1", "2发", "3"] };

context.Blogs.Add(blog);



context.SaveChanges();

var blog1 = new Blog { Url = "http://sample1.com", PostIds = ["5", "6米", "7"]  };

context.Blogs.Add(blog1);

context.SaveChanges();

// var findBlog = context.Blogs.Where(b => b.PostIds.Contains(4));

// System.Console.WriteLine(JsonSerializer.Serialize(findBlog));

public class MyContext : DbContext
{
    public DbSet<Blog> Blogs  { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite("Data Source=test8.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
     
        base.OnModelCreating(modelBuilder);
    }

}

public class Blog
{
    public int BlogId { get; set; }
    public string Url { get; set; }
[Unicode(true)]
    public List<string> PostIds { get; set; }
}