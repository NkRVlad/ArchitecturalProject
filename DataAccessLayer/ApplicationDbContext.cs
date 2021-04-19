using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
namespace DataAccessLayer
{
    public class ApplicationDbContext: DbContext, IApplicationDbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): 
            base(options)
        {

        }
        public DbSet<ApiRequestsLogs> ApiRequestsLogs { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Employees> Employees { get; set; }
        public DbSet<HiringHistories> HiringHistories { get; set; }
        public DbSet<Achievements> Achievements { get; set; }
    }
}
