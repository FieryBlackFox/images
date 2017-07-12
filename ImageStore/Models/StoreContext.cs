using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ImageStore.Models
{
    public class StoreContext : DbContext
    {
        public DbSet<Image> Images { get; set; }

        public StoreContext(DbContextOptions<StoreContext> options)
            : base(options)
        { }

    }
}
