using Microsoft.EntityFrameworkCore;
using Sauces.Core.Model;

namespace Sauces.Core;

public class SaucesContext : DbContext
{
    public DbSet<Sauce> Sauces { get; set; }
    public DbSet<FermentationRecipe> FermentationRecipes { get; set; }
    public DbSet<Ingredient> Ingredients { get; set; }
    public string DbPath { get; init; }

    public SaucesContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = Path.Join(path, "Sauces");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlite($"Data Source={DbPath}");
}