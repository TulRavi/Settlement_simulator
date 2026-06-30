using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace SettlementGame.Domain
{
    public class GameDbContext : DbContext //наследуемся от DbContext (это ядро EF)
    {
        public DbSet <WorkerEntity> Workers { get; set; } //аналог Excel лист Workers
        public DbSet<BuildingEntity> BuildedBuildings { get; set; }

        public DbSet<UserEntity> Users { get; set; }

        //public DbSet<Building> PossibleBuildings { get; set; }
        public GameDbContext(DbContextOptions<GameDbContext> options)// настройки БД приходят извне
       : base(options) // а тут передаём настройки базовому классу
        {
        }
    }
}
