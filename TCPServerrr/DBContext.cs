using Microsoft.EntityFrameworkCore;

namespace TCPServerrr;

public class DBContext : DbContext
{
    public DbSet<Car> Cars { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=Car;Integrated Security=True;Trust Server Certificate=True;");
    }

}
