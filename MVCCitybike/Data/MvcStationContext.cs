using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MVCCitybike.Models;

namespace MvcStation.Data
{
    public class MvcStationContext : DbContext
    {
        public MvcStationContext (DbContextOptions<MvcStationContext> options)
            : base(options)
        {
        }

        public DbSet<MVCCitybike.Models.Station> Station { get; set; } = default!;
    }
}
