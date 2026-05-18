using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace SettlementGame.Domain
{
    public class GameDbContextFactory : IDesignTimeDbContextFactory<GameDbContext>
    {
        public GameDbContext CreateDbContext(string[] args)
        {
            // создаём билдер опций для DbContext
            var optionsBuilder = new DbContextOptionsBuilder<GameDbContext>();

            // указываем какую БД использовать (пример — SQL Server)
            optionsBuilder.UseSqlite("Data Source=game.db");

            // возвращаем готовый контекст с настройками
            return new GameDbContext(optionsBuilder.Options);
        }
    }
}
