using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DevTest_TaskApi.Models;

namespace DevTest_TaskApi.Data
{
    public class DevTest_TaskApiContext : DbContext
    {
        public DevTest_TaskApiContext (DbContextOptions<DevTest_TaskApiContext> options)
            : base(options)
        {
        }

        public DbSet<DevTest_TaskApi.Models.Tasks> Tasks { get; set; } = default!;
    }
}
