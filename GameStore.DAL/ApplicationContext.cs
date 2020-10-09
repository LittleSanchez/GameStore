namespace GameStore.DAL
{
    using GameStore.DAL.Entities;
    using GameStore.DAL.Initializer;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class ApplicationContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<Genre> Genres{ get; set; }
        public DbSet<Developer> Developers { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<ArchiveGame> ArchiveGames { get; set; }


        public ApplicationContext()
            : base("name=ApplicationContext")
        {
           //Database.SetInitializer(new GamesInitializer());
        }

        }
}