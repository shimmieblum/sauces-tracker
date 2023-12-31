using Microsoft.EntityFrameworkCore;
using Sauces.Core.Model;

namespace Sauces.Core;

public class SaucesContext : DbContext
{
    public virtual DbSet<Sauce> Sauces { get; init; }
    public virtual DbSet<FermentationRecipe> FermentationRecipes { get; init; }
    public virtual DbSet<Ingredient> Ingredients { get; init; }

    public SaucesContext()
    { }
    
    public SaucesContext(DbContextOptions<SaucesContext> options) : base(options)
    { }
    

    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //     => optionsBuilder.UseSqlite($"Data Source={DbPath}");
}