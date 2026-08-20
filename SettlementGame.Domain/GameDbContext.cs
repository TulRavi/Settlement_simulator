using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace SettlementGame.Domain
{
    public class GameDbContext : DbContext //inherit from DbContext (this is the core of EF)
    {
        public DbSet<WorkerEntity> Workers { get; set; } //similar to an Excel sheet called Workers
        public DbSet<BuildingEntity> BuiltBuildings { get; set; }

        public DbSet<UserEntity> Users { get; set; }

        public GameDbContext(DbContextOptions<GameDbContext> options) //database settings are passed from outside
            : base(options) //pass the settings to the base class
        {
        }
    }
}