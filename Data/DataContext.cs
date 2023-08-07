using System;
using Microsoft.EntityFrameworkCore;

namespace Estudos.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) 
        {

        }

        public DbSet<Character> Characters => Set<Character>();
    
    }
    
}
