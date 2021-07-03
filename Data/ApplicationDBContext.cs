using Microsoft.EntityFrameworkCore;

using dotnet_rest_api.Models;

namespace dotnet_rest_api.Data
{
    public class ApplicationDBContext : DbContext
    {

        public DbSet<Product> Products { get; set; }
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) { }

    }
}