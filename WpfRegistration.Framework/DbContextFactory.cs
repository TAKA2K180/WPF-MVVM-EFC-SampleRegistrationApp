using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace WpfRegistration.EntityFramework
{
    public class DbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        private readonly ApplicationDbContext _context;
        public ApplicationDbContext CreateDbContext(string[] args = null)
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>();
            options.UseSqlServer(DbConnection.cs);
            return new ApplicationDbContext(options.Options);
        }
    }
}
