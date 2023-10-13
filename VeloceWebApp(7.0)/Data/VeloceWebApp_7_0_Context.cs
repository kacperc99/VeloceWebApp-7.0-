using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VeloceWebApp_7._0_.Models;

namespace VeloceWebApp_7._0_.Data
{
    public class VeloceWebApp_7_0_Context : DbContext
    {
        public VeloceWebApp_7_0_Context (DbContextOptions<VeloceWebApp_7_0_Context> options)
            : base(options)
        {
        }

        public DbSet<VeloceWebApp_7._0_.Models.PersonModel> PersonModel { get; set; } = default!;
    }
}
