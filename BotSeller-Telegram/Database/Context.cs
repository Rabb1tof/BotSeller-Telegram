using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace BotSeller_Telegram.Database
{
    public class Context : DbContext
    {
        public DbSet<UserProps> UserProps { get; set; }
        public DbSet<ProductProps> ProductProps { get; set; }

        public override void Dispose()
        {
            SaveChanges();
            base.Dispose();
        }

        public override ValueTask DisposeAsync()
        {
            SaveChanges();
            return base.DisposeAsync();
        }

        public Context(DbContextOptions<Context> ctx) : base(ctx)
        {
            Database.Migrate();
        }
    }
}
