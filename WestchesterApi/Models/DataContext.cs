using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace WestchesterApi.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<DBEgg> Eggs { get; set; }

        public DbSet<DBRaid> Raids { get; set; }

        public DbSet<CurrentEvent> CurrentEvent { get; set; }
    }
}
