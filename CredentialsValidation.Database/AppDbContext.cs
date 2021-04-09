using CredentialsValidation.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace CredentialsValidation.Database
{
    public class AppDbContext : DbContext
    {
        public DbSet<ICredentials> Credentials { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
    }
}
