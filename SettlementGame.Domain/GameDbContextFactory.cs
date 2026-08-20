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
            //Create an options builder for DbContext
            var optionsBuilder = new DbContextOptionsBuilder<GameDbContext>();

            //specify which database to use (SQLite)
            optionsBuilder.UseSqlite("Data Source=game.db");

            //return the context with the configured options
            return new GameDbContext(optionsBuilder.Options);
        }
    }
}