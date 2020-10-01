using Microsoft.EntityFrameworkCore;
using PorductOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PorductOnline.Data
{
    public class MyContext:DbContext
    {
        public MyContext(DbContextOptions<MyContext>options):base(options)
        {

        }
        public DbSet<Product> productDb { get; set; }
    }
}
