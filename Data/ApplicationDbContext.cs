using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CutFileWeb.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace CutFileWeb.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> User { get; set; } 

        public DbSet<CutFileWeb.Models.Brand>? Brands { get; set; }

        public DbSet<CutFileWeb.Models.Category>? Categories { get; set; }

        public DbSet<CutFileWeb.Models.Product>? Products { get; set; }
    }
}
