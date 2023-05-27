using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MVCCitybike.Models;

namespace MvcBiketripsMay2021.Data
{
    public class MvcBiketripsMay2021Context : DbContext
    {
        public MvcBiketripsMay2021Context (DbContextOptions<MvcBiketripsMay2021Context> options)
            : base(options)
        {
        }

        public DbSet<MVCCitybike.Models.BiketripsMay2021> BiketripsMay2021 { get; set; } = default!;
    }
}
