using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ex2_server.Models;

namespace ex2_server.Data
{
    public class ex2_serverContext : DbContext
    {
        public ex2_serverContext (DbContextOptions<ex2_serverContext> options)
            : base(options)
        {
        }

        public DbSet<ex2_server.Models.Ratings>? Ratings { get; set; }
    }
}
